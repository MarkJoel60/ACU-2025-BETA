// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAllocator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Common.Parser;
using PX.CS;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN;
using PX.Reports.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Creates allocation transaction based on the allocation rules for the given project task(s).
/// </summary>
[Serializable]
public class PMAllocator : PXGraph<
#nullable disable
PMAllocator>, IRateTable
{
  public PXSelect<PMRegister> Document;
  public PXSelect<PMTran, Where<PMTran.tranType, Equal<Current<PMRegister.module>>, And<PMTran.refNbr, Equal<Current<PMRegister.refNbr>>>>> Transactions;
  public PXSelect<PMAllocationSourceTran> SourceTran;
  public PXSelect<PMAllocationAuditTran> AuditTran;
  public PXSelect<PMTaskAllocTotalAccum> TaskTotals;
  public PXSetupOptional<INSetup> Insetup;
  public PXSetupOptional<CommonSetup> commonsetup;
  public PXSetup<CMSetup> cmsetup;
  public Dictionary<int, List<PMAllocator.PMTranWithTrace>> stepResults;
  public Dictionary<int, Dictionary<int, List<PXResult<PMTran>>>> transactions;
  public RateEngineV2 rateEngine;
  public Dictionary<string, PMAllocator.AllocationInfo> allocationInfo;
  public Dictionary<int, PMAllocator.AccountGroup> accountGroups;
  public Dictionary<string, List<PMRateDefinition>> rateDefinitions;
  protected bool IsBudgetAllocation;

  [PXDefault]
  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.projectID> e)
  {
  }

  [PXSelector(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMTran.projectID>>>>))]
  [PXDBInt]
  [PXDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.taskID> e)
  {
  }

  [PXDecimal]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.tranCuryAmountCopy> e)
  {
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IProjectMultiCurrency MultiCurrencyService { get; set; }

  /// <summary>
  /// Get or sets Restriction on Transactions that can be allocated.
  /// If set only those transactions get allocated that have the Date greater or equal to FilterStartDate.
  /// </summary>
  public DateTime? FilterStartDate { get; set; }

  /// <summary>
  /// Get or sets Restriction on Transactions that can be allocated.
  /// If set only those transactions get allocated that have the Date less then or equal to FilterEndDate.
  /// </summary>
  public DateTime? FilterEndDate { get; set; }

  /// <summary>
  /// This Date is used as source for PMTran.Date for newly created Allocation transaction (given that Allocation Rules is setup to use Allocation Date)
  /// </summary>
  public DateTime? PostingDate { get; set; }

  public PMAllocator()
  {
    ((PXGraph) this).Caches[typeof (PMAllocationDetail)].AllowInsert = false;
    ((PXGraph) this).Caches[typeof (PMAllocationDetail)].AllowUpdate = false;
    ((PXGraph) this).Caches[typeof (PMAllocationDetail)].AllowDelete = false;
    this.stepResults = new Dictionary<int, List<PMAllocator.PMTranWithTrace>>();
    this.transactions = new Dictionary<int, Dictionary<int, List<PXResult<PMTran>>>>();
    this.allocationInfo = new Dictionary<string, PMAllocator.AllocationInfo>();
    this.rateDefinitions = new Dictionary<string, List<PMRateDefinition>>();
  }

  /// <summary>Executes Allocation for the list of tasks.</summary>
  /// <param name="tasks"></param>
  public virtual void Execute(List<PMTask> tasks)
  {
    this.PreselectAccountGroups();
    if (!this.PreSelectTasksTransactions(tasks))
      return;
    foreach (PMTask task in tasks)
      this.Execute(task, false);
  }

  public virtual bool ExecuteContinueOnError(List<PMTask> tasks)
  {
    bool flag = false;
    this.PreselectAccountGroups();
    if (this.PreSelectTasksTransactions(tasks))
    {
      foreach (PMTask task in tasks)
      {
        try
        {
          this.Execute(task, false);
        }
        catch (PXException ex)
        {
          PXTrace.WriteError((Exception) ex);
          flag = true;
        }
      }
    }
    return !flag;
  }

  /// <summary>Executes Allocation for the given Task.</summary>
  /// <param name="task">Task</param>
  public virtual void Execute(PMTask task) => this.Execute(task, true);

  public virtual void Execute(PMTask task, bool preselectTransactions)
  {
    this.stepResults.Clear();
    this.PreselectAccountGroups();
    if (preselectTransactions)
      this.PreSelectTaskTransactions(task);
    PMProject project = PMProject.PK.Find((PXGraph) this, task.ProjectID);
    if (project == null)
      throw new PXException("Task '{0}' has invalid Project associated with it. Project with the ID '{1}' was not found in the system.", new object[2]
      {
        (object) task.TaskCD,
        (object) task.ProjectID
      });
    foreach (PXResult<PMAllocationDetail> pxResult in PXSelectBase<PMAllocationDetail, PXSelect<PMAllocationDetail, Where<PMAllocationDetail.allocationID, Equal<Required<PMAllocationDetail.allocationID>>>, OrderBy<Asc<PMAllocationDetail.stepID>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) task.AllocationID
    }))
    {
      PMAllocationDetail step = PXResult<PMAllocationDetail>.op_Implicit(pxResult);
      try
      {
        this.ProcessStep(task, project, step);
      }
      catch (PMAllocationNotFoundException ex)
      {
        throw ex;
      }
      catch (PMAllocationException ex)
      {
        throw new PXException((Exception) ex, "The {0} allocation step cannot be completed for the {1} project task of the {2} project. Review the settings of the {3} allocation rule on the Allocation Settings tab of the Allocation Rules (PM207500) form, and the details of the {4} project transaction, which is used as the source of this allocation step.", new object[5]
        {
          (object) step.StepID,
          (object) task.TaskCD?.Trim(),
          (object) project.ContractCD?.Trim(),
          (object) step.AllocationID,
          (object) ex.RefNbr
        });
      }
      catch (PXException ex)
      {
        object[] objArray = new object[2]
        {
          (object) step.StepID,
          (object) task.TaskCD?.Trim()
        };
        throw new PXException((Exception) ex, "Failed to Process Step: {0} during Allocation for Task: {1}. Check Trace for details.", objArray);
      }
    }
    if (this.stepResults.Count <= 0)
      return;
    foreach (KeyValuePair<int, List<PMAllocator.PMTranWithTrace>> stepResult in this.stepResults)
    {
      foreach (PMAllocator.PMTranWithTrace pmTranWithTrace in stepResult.Value)
        this.AddAuditTran(task.AllocationID, pmTranWithTrace.Tran.TranID, pmTranWithTrace.OriginalTrans);
    }
  }

  public virtual void PreselectAccountGroups()
  {
    if (this.accountGroups != null)
      return;
    this.accountGroups = new Dictionary<int, PMAllocator.AccountGroup>();
    PXSelectBase<PMAccountGroup> pxSelectBase = (PXSelectBase<PMAccountGroup>) new PXSelect<PMAccountGroup, Where<PMAccountGroup.isActive, Equal<True>>>((PXGraph) this);
    using (new PXFieldScope(((PXSelectBase) pxSelectBase).View, new System.Type[2]
    {
      typeof (PMAccountGroup.groupID),
      typeof (PMAccountGroup.groupCD)
    }))
    {
      foreach (PXResult<PMAccountGroup> pxResult in pxSelectBase.Select(Array.Empty<object>()))
      {
        PMAccountGroup pmAccountGroup = PXResult<PMAccountGroup>.op_Implicit(pxResult);
        Dictionary<int, PMAllocator.AccountGroup> accountGroups = this.accountGroups;
        int? groupId = pmAccountGroup.GroupID;
        int key = groupId.Value;
        groupId = pmAccountGroup.GroupID;
        PMAllocator.AccountGroup accountGroup = new PMAllocator.AccountGroup(groupId.Value, pmAccountGroup.GroupCD);
        accountGroups.Add(key, accountGroup);
      }
    }
  }

  public virtual void PreSelectTaskTransactions(PMTask task)
  {
    if (task.AllocationID == null)
      return;
    this.transactions.Clear();
    Dictionary<int, List<PXResult<PMTran>>> dictionary = new Dictionary<int, List<PXResult<PMTran>>>();
    HashSet<int> accountGroups = new HashSet<int>();
    HashSet<string> stringSet = new HashSet<string>();
    PMAllocator.AllocationInfo allocationInfo;
    if (!this.allocationInfo.TryGetValue(task.AllocationID, out allocationInfo))
    {
      List<PMAllocationDetail> steps = new List<PMAllocationDetail>();
      allocationInfo = new PMAllocator.AllocationInfo(accountGroups, stringSet, steps);
      this.allocationInfo.Add(task.AllocationID, allocationInfo);
      foreach (PXResult<PMAllocationDetail> pxResult in PXSelectBase<PMAllocationDetail, PXSelect<PMAllocationDetail, Where<PMAllocationDetail.allocationID, Equal<Required<PMAllocationDetail.allocationID>>>, OrderBy<Asc<PMAllocationDetail.stepID>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) task.AllocationID
      }))
      {
        PMAllocationDetail step = PXResult<PMAllocationDetail>.op_Implicit(pxResult);
        if (step.Method == "B")
          allocationInfo.ContainsBudgetStep = true;
        foreach (int accountGroup in this.GetAccountGroups(step))
          accountGroups.Add(accountGroup);
        stringSet.Add(step.RateTypeID);
        steps.Add(step);
      }
    }
    else
    {
      accountGroups = allocationInfo.AccountGroups;
      stringSet = allocationInfo.RateTypes;
    }
    foreach (int num in accountGroups)
      dictionary.Add(num, new List<PXResult<PMTran>>((IEnumerable<PXResult<PMTran>>) this.GetTranFromDatabase(task.ProjectID, task.TaskID, num)));
    this.transactions.Add(task.TaskID.Value, dictionary);
    this.rateEngine = this.CreateRateEngineV2((IList<string>) new string[1]
    {
      task.RateTableID
    }, (IList<string>) stringSet.ToList<string>());
  }

  public virtual bool PreSelectTasksTransactions(List<PMTask> tasks)
  {
    this.transactions.Clear();
    if (tasks.Count == 0)
      return false;
    bool flag = false;
    HashSet<string> stringSet1 = new HashSet<string>();
    HashSet<string> source = new HashSet<string>();
    HashSet<int> intSet = new HashSet<int>();
    foreach (PMTask task in tasks)
    {
      if (task.AllocationID != null)
      {
        stringSet1.Add(task.AllocationID);
        this.transactions.Add(task.TaskID.Value, new Dictionary<int, List<PXResult<PMTran>>>());
        source.Add(task.RateTableID);
        intSet.Add(task.ProjectID.Value);
      }
    }
    HashSet<int> accountGroups = new HashSet<int>();
    HashSet<string> stringSet2 = new HashSet<string>();
    foreach (string key in stringSet1)
    {
      PMAllocator.AllocationInfo allocationInfo;
      if (!this.allocationInfo.TryGetValue(key, out allocationInfo))
      {
        List<PMAllocationDetail> steps = new List<PMAllocationDetail>();
        allocationInfo = new PMAllocator.AllocationInfo(accountGroups, stringSet2, steps);
        this.allocationInfo.Add(key, allocationInfo);
        foreach (PXResult<PMAllocationDetail> pxResult in PXSelectBase<PMAllocationDetail, PXSelect<PMAllocationDetail, Where<PMAllocationDetail.allocationID, Equal<Required<PMAllocationDetail.allocationID>>>, OrderBy<Asc<PMAllocationDetail.stepID>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) key
        }))
        {
          PMAllocationDetail step = PXResult<PMAllocationDetail>.op_Implicit(pxResult);
          if (step.Method == "B")
            allocationInfo.ContainsBudgetStep = true;
          foreach (int accountGroup in this.GetAccountGroups(step))
            accountGroups.Add(accountGroup);
          stringSet2.Add(step.RateTypeID);
          steps.Add(step);
        }
      }
      else
      {
        accountGroups = allocationInfo.AccountGroups;
        stringSet2 = allocationInfo.RateTypes;
      }
      flag = allocationInfo.ContainsBudgetStep;
    }
    foreach (int num1 in accountGroups)
    {
      foreach (int num2 in intSet)
      {
        foreach (PXResult<PMTran> pxResult in this.GetTranFromDatabase(new int?(num2), num1))
        {
          flag = true;
          Dictionary<int, List<PXResult<PMTran>>> dictionary;
          if (this.transactions.TryGetValue(PXResult<PMTran>.op_Implicit(pxResult).TaskID.Value, out dictionary))
          {
            List<PXResult<PMTran>> pxResultList;
            if (!dictionary.TryGetValue(num1, out pxResultList))
            {
              pxResultList = new List<PXResult<PMTran>>();
              dictionary.Add(num1, pxResultList);
            }
            pxResultList.Add(pxResult);
          }
        }
      }
    }
    if (!flag)
      return false;
    this.rateEngine = this.CreateRateEngineV2((IList<string>) source.ToList<string>(), (IList<string>) stringSet2.ToList<string>());
    return true;
  }

  /// <summary>
  /// When overriding in customization return null in order for system to use RateEngineV1
  /// </summary>
  public virtual RateEngineV2 CreateRateEngineV2(IList<string> rateTables, IList<string> rateTypes)
  {
    return new RateEngineV2((PXGraph) this, rateTables, rateTypes);
  }

  /// <summary>
  /// Returns distinct account groups for the given step.
  /// Method relies on pre-selected data stored in allocationInfo and accountGroups collections.
  /// Method do not query database.
  /// </summary>
  /// <param name="step">Allocation step</param>
  /// <returns></returns>
  public virtual HashSet<int> GetAccountGroups(PMAllocationDetail step)
  {
    HashSet<int> accountGroups = new HashSet<int>();
    if (step.SelectOption == "S")
    {
      foreach (PMAllocationDetail step1 in (IEnumerable<PMAllocationDetail>) this.GetSteps(step.AllocationID, step.RangeStart, step.RangeEnd))
      {
        foreach (int accountGroup in this.GetAccountGroups(step1))
          accountGroups.Add(accountGroup);
      }
    }
    else
    {
      foreach (int num in (IEnumerable<int>) this.GetAccountGroupsRange(step))
        accountGroups.Add(num);
    }
    return accountGroups;
  }

  /// <summary>
  /// Returns distinct account groups for the Range defined in the given step.
  /// Method relies on pre-selected data stored in accountGroups collection.
  /// Method do not query database.
  /// </summary>
  public virtual IList<int> GetAccountGroupsRange(PMAllocationDetail step)
  {
    List<int> accountGroupsRange = new List<int>();
    int? nullable1;
    if (!step.AccountGroupFrom.HasValue || step.AccountGroupTo.HasValue)
    {
      if (step.AccountGroupFrom.HasValue)
      {
        int? accountGroupTo = step.AccountGroupTo;
        nullable1 = step.AccountGroupFrom;
        if (!(accountGroupTo.GetValueOrDefault() == nullable1.GetValueOrDefault() & accountGroupTo.HasValue == nullable1.HasValue))
          goto label_4;
      }
      else
        goto label_4;
    }
    List<int> intList1 = accountGroupsRange;
    nullable1 = step.AccountGroupFrom;
    int num1 = nullable1.Value;
    intList1.Add(num1);
label_4:
    nullable1 = step.AccountGroupFrom;
    if (!nullable1.HasValue)
    {
      nullable1 = step.AccountGroupTo;
      if (nullable1.HasValue)
      {
        List<int> intList2 = accountGroupsRange;
        nullable1 = step.AccountGroupFrom;
        int num2 = nullable1.Value;
        intList2.Add(num2);
      }
    }
    nullable1 = step.AccountGroupTo;
    if (nullable1.HasValue)
    {
      nullable1 = step.AccountGroupTo;
      int? nullable2 = step.AccountGroupFrom;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      {
        Dictionary<int, PMAllocator.AccountGroup> accountGroups1 = this.accountGroups;
        nullable2 = step.AccountGroupFrom;
        int key1 = nullable2.Value;
        PMAllocator.AccountGroup accountGroup1;
        ref PMAllocator.AccountGroup local1 = ref accountGroup1;
        if (!accountGroups1.TryGetValue(key1, out local1))
          throw new PXException("The {0} account group specified as the From setting of the {1} step of the {2} allocation rule has not been found in the system.", new object[3]
          {
            (object) step.AccountGroupFrom,
            (object) step.StepID,
            (object) step.AllocationID
          });
        Dictionary<int, PMAllocator.AccountGroup> accountGroups2 = this.accountGroups;
        nullable2 = step.AccountGroupTo;
        int key2 = nullable2.Value;
        PMAllocator.AccountGroup accountGroup2;
        ref PMAllocator.AccountGroup local2 = ref accountGroup2;
        if (!accountGroups2.TryGetValue(key2, out local2))
          throw new PXException("The {0} account group specified as the To setting of the {1} step of the {2} allocation rule has not been found in the system.", new object[3]
          {
            (object) step.AccountGroupTo,
            (object) step.StepID,
            (object) step.AllocationID
          });
        foreach (PMAllocator.AccountGroup accountGroup3 in this.accountGroups.Values)
        {
          if (string.Compare(accountGroup3.GroupCD, accountGroup1.GroupCD, StringComparison.InvariantCultureIgnoreCase) >= 0 && string.Compare(accountGroup3.GroupCD, accountGroup2.GroupCD, StringComparison.InvariantCultureIgnoreCase) <= 0)
            accountGroupsRange.Add(accountGroup3.GroupID);
        }
      }
    }
    return (IList<int>) accountGroupsRange;
  }

  /// <summary>
  /// Returns list of inner steps for the given Range.
  /// Method relies on pre-selected data stored in allocationInfo collection.
  /// Method do not query database.
  /// </summary>
  public virtual IList<PMAllocationDetail> GetSteps(
    string allocationID,
    int? rangeStart,
    int? rangeEnd)
  {
    List<PMAllocationDetail> steps = new List<PMAllocationDetail>();
    foreach (PMAllocationDetail step in this.allocationInfo[allocationID].Steps)
    {
      int? nullable1 = step.StepID;
      int? nullable2 = rangeStart;
      if (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
      {
        nullable2 = step.StepID;
        nullable1 = rangeEnd;
        if (nullable2.GetValueOrDefault() <= nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
          steps.Add(step);
      }
    }
    return (IList<PMAllocationDetail>) steps;
  }

  /// <summary>
  /// Returns RateDefinitions from Cached rateDefinitions collection or from database if not found.
  /// </summary>
  public virtual IList<PMRateDefinition> GetRateDefinitions(string rateTable)
  {
    List<PMRateDefinition> rateDefinitions;
    if (!string.IsNullOrEmpty(rateTable))
    {
      if (!this.rateDefinitions.TryGetValue(rateTable, out rateDefinitions))
      {
        rateDefinitions = new List<PMRateDefinition>(GraphHelper.RowCast<PMRateDefinition>((IEnumerable) ((PXSelectBase<PMRateDefinition>) new PXSelect<PMRateDefinition, Where<PMRateDefinition.rateTableID, Equal<Required<PMRateDefinition.rateTableID>>>, OrderBy<Asc<PMRateDefinition.rateTypeID, Asc<PMRateDefinition.sequence>>>>((PXGraph) this)).Select(new object[1]
        {
          (object) rateTable
        })));
        this.rateDefinitions.Add(rateTable, rateDefinitions);
      }
    }
    else
      rateDefinitions = new List<PMRateDefinition>();
    return (IList<PMRateDefinition>) rateDefinitions;
  }

  public virtual bool ProcessStep(PMTask task, PMProject project, PMAllocationDetail step)
  {
    if (step.Post.GetValueOrDefault())
    {
      if (step.Method == "T")
      {
        List<PMTran> sourceList = this.Select(step, task.ProjectID, task.TaskID);
        if (sourceList.Count > 0)
        {
          List<PMTran> pmTranList = new List<PMTran>();
          this.Post(step, project, task, sourceList, pmTranList);
          this.AddSourceTrans(step, pmTranList);
          foreach (PMTran pmTran in pmTranList)
          {
            pmTran.Allocated = new bool?(true);
            pmTran.ExcludedFromAllocation = new bool?(false);
            ((PXSelectBase<PMTran>) this.Transactions).Update(pmTran);
          }
        }
      }
      else
      {
        List<PMAllocator.PMTranWithTrace> pmTranWithTraceList = this.ProcessBudgetStep(step, project, task);
        this.stepResults.Add(step.StepID.Value, pmTranWithTraceList);
        return pmTranWithTraceList.Count > 0;
      }
    }
    return false;
  }

  public virtual void Post(
    PMAllocationDetail step,
    PMProject project,
    PMTask task,
    List<PMTran> sourceList,
    List<PMTran> allocatedList)
  {
    PMAllocator.AllocatedService allocService = new PMAllocator.AllocatedService((PXGraph) this, task);
    List<PMAllocator.PMTranWithTrace> allocated = !step.FullDetail.GetValueOrDefault() ? this.PostSummary(step, task, sourceList, allocatedList, allocService) : this.PostFullDetail(step, task, sourceList, allocatedList, allocService);
    this.PostAllocatedTrans(step, project, allocated);
  }

  public virtual List<PMAllocator.PMTranWithTrace> PostFullDetail(
    PMAllocationDetail step,
    PMTask task,
    List<PMTran> list,
    List<PMTran> allocatedList,
    PMAllocator.AllocatedService allocService)
  {
    List<PMAllocator.PMTranWithTrace> pmTranWithTraceList = new List<PMAllocator.PMTranWithTrace>(list.Count);
    foreach (PMTran pmTran in list)
    {
      bool? nullable = step.AllocateNonBillable;
      if (!nullable.GetValueOrDefault())
      {
        nullable = pmTran.Billable;
        if (!nullable.GetValueOrDefault())
          continue;
      }
      pmTran.Rate = this.GetRate(step, pmTran, task.RateTableID);
      if (pmTran.Rate.HasValue)
      {
        foreach (PMTran tran in (IEnumerable<PMTran>) this.Transform(step, task, pmTran, true, this.IsWipStep(step, task)))
        {
          if (this.CanAllocate(step, tran))
          {
            PMAllocator.PMTranWithTrace pmTranWithTrace = new PMAllocator.PMTranWithTrace(tran, new List<long>((IEnumerable<long>) new long[1]
            {
              pmTran.TranID.Value
            }));
            string note = PXNoteAttribute.GetNote(((PXSelectBase) this.Transactions).Cache, (object) pmTran);
            Guid[] fileNotes = PXNoteAttribute.GetFileNotes(((PXSelectBase) this.Transactions).Cache, (object) pmTran);
            pmTranWithTrace.NoteText = note;
            pmTranWithTrace.Files = fileNotes;
            pmTranWithTraceList.Add(pmTranWithTrace);
            allocatedList.Add(pmTran);
          }
        }
      }
    }
    return pmTranWithTraceList;
  }

  public bool IsWipStep(PMAllocationDetail step, PMTask task)
  {
    int? nullable1 = new int?();
    if (step.AccountGroupOrigin == "C")
      nullable1 = step.AccountGroupID;
    else if (step.AccountGroupOrigin == "F")
    {
      PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) step.AccountID
      }));
      if (account != null)
        nullable1 = account.AccountGroupID;
    }
    if (nullable1.HasValue)
    {
      int? nullable2 = task.WipAccountGroupID;
      if (nullable2.HasValue)
      {
        nullable2 = nullable1;
        int? wipAccountGroupId = task.WipAccountGroupID;
        return nullable2.GetValueOrDefault() == wipAccountGroupId.GetValueOrDefault() & nullable2.HasValue == wipAccountGroupId.HasValue;
      }
    }
    return false;
  }

  public virtual List<PMAllocator.PMTranWithTrace> PostSummary(
    PMAllocationDetail step,
    PMTask task,
    List<PMTran> fullList,
    List<PMTran> allocatedList,
    PMAllocator.AllocatedService allocService)
  {
    List<Group> groupList = this.BreakIntoGroups(step, fullList);
    List<PMAllocator.PMTranWithTrace> pmTranWithTraceList = new List<PMAllocator.PMTranWithTrace>();
    foreach (Group group in groupList)
    {
      PMAllocator.PMDataNavigator navigator = new PMAllocator.PMDataNavigator((IRateTable) this, group.List);
      List<long> list = new List<long>();
      Decimal num1 = 0M;
      Decimal num2 = 0M;
      Decimal num3 = 0M;
      string str = (string) null;
      DateTime? nullable1 = new DateTime?();
      DateTime? nullable2 = new DateTime?();
      foreach (PMTran tran in group.List)
      {
        if (!nullable1.HasValue)
        {
          nullable1 = tran.StartDate;
        }
        else
        {
          DateTime? nullable3 = nullable1;
          DateTime? startDate = tran.StartDate;
          if ((nullable3.HasValue & startDate.HasValue ? (nullable3.GetValueOrDefault() > startDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            nullable1 = tran.StartDate;
        }
        if (!nullable2.HasValue)
        {
          nullable2 = tran.EndDate;
        }
        else
        {
          DateTime? nullable4 = nullable2;
          DateTime? endDate = tran.EndDate;
          if ((nullable4.HasValue & endDate.HasValue ? (nullable4.GetValueOrDefault() < endDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            nullable2 = tran.EndDate;
        }
        Decimal? qty = new Decimal?(0M);
        Decimal? billableQty = new Decimal?(0M);
        Decimal? amt = new Decimal?(0M);
        string description = (string) null;
        tran.Rate = this.GetRate(step, tran, task.RateTableID);
        if (tran.Rate.HasValue)
        {
          this.CalculateFormulas(navigator, step, tran, out qty, out billableQty, out amt, out description);
          str = description;
          qty.GetValueOrDefault();
          if (tran.InventoryID.HasValue)
          {
            int? inventoryId = tran.InventoryID;
            int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
            if (!(inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue) && !string.IsNullOrEmpty(tran.UOM))
              this.ConvertQtyToBase(tran.InventoryID, tran.UOM, qty.GetValueOrDefault());
          }
          list.Add(tran.TranID.Value);
          num1 += qty.GetValueOrDefault();
          num2 += billableQty.GetValueOrDefault();
          num3 += amt.GetValueOrDefault();
        }
        else
          break;
      }
      if (group.List.Count > 0)
      {
        PMProject pmProject = PMProject.PK.Find((PXGraph) this, task.ProjectID);
        foreach (PMTran tran1 in (IEnumerable<PMTran>) this.Transform(step, task, group.List[0], false, this.IsWipStep(step, task)))
        {
          PMTran pmTran1 = tran1;
          bool? isInverted = tran1.IsInverted;
          Decimal? nullable5 = new Decimal?(isInverted.GetValueOrDefault() ? -num1 : num1);
          pmTran1.Qty = nullable5;
          PMTran pmTran2 = tran1;
          isInverted = tran1.IsInverted;
          Decimal? nullable6 = new Decimal?(isInverted.GetValueOrDefault() ? -num2 : num2);
          pmTran2.BillableQty = nullable6;
          tran1.StartDate = nullable1;
          tran1.EndDate = nullable2;
          PMProject project = pmProject;
          PMTran tran2 = tran1;
          isInverted = tran1.IsInverted;
          Decimal amt = isInverted.GetValueOrDefault() ? -PX.Objects.CM.PXCurrencyAttribute.BaseRound((PXGraph) this, num3) : PX.Objects.CM.PXCurrencyAttribute.BaseRound((PXGraph) this, num3);
          Decimal num4;
          ref Decimal local = ref num4;
          this.SetCalculatedAmount(project, tran2, amt, out local);
          Decimal? nullable7 = tran1.BillableQty;
          Decimal num5 = 0M;
          if (!(nullable7.GetValueOrDefault() == num5 & nullable7.HasValue))
          {
            nullable7 = tran1.BillableQty;
            Decimal num6 = PXDBQuantityAttribute.Round(new Decimal?(nullable7.Value));
            Decimal num7 = PXDBPriceCostAttribute.Round(num4 / num6);
            tran1.UnitRate = new Decimal?(num7);
          }
          else
          {
            nullable7 = tran1.Qty;
            Decimal num8 = 0M;
            if (!(nullable7.GetValueOrDefault() == num8 & nullable7.HasValue))
            {
              nullable7 = tran1.Qty;
              Decimal num9 = PXDBQuantityAttribute.Round(new Decimal?(nullable7.Value));
              Decimal num10 = PXDBPriceCostAttribute.Round(num4 / num9);
              tran1.TranCuryUnitRate = new Decimal?(num10);
              tran1.UnitRate = new Decimal?(num10);
            }
          }
          if (group.HasMixedInventory)
            tran1.InventoryID = new int?(PMInventorySelectorAttribute.EmptyInventoryID);
          if (group.HasMixedUOM)
          {
            tran1.Qty = new Decimal?(0M);
            tran1.BillableQty = new Decimal?(0M);
            tran1.UOM = (string) null;
            tran1.UnitRate = new Decimal?(0M);
          }
          if (group.HasMixedBAccount)
          {
            tran1.BAccountID = new int?();
            tran1.LocationID = new int?();
          }
          if (group.HasMixedBAccountLoc)
            tran1.LocationID = new int?();
          tran1.Description = str != null || !group.HasMixedDescription ? str : this.GetConcatenatedDescription(step, tran1, task);
          if (this.CanAllocate(step, tran1))
          {
            PMAllocator.PMTranWithTrace pmTranWithTrace = new PMAllocator.PMTranWithTrace(tran1, list);
            List<Guid> guidList = new List<Guid>();
            foreach (PMTran pmTran3 in group.List)
            {
              guidList.AddRange((IEnumerable<Guid>) PXNoteAttribute.GetFileNotes(((PXSelectBase) this.Transactions).Cache, (object) pmTran3));
              allocatedList.Add(pmTran3);
            }
            pmTranWithTrace.Files = guidList.ToArray();
            pmTranWithTraceList.Add(pmTranWithTrace);
          }
        }
      }
    }
    return pmTranWithTraceList;
  }

  public virtual void PostAllocatedTrans(
    PMAllocationDetail step,
    PMProject project,
    List<PMAllocator.PMTranWithTrace> allocated)
  {
    foreach (PMAllocator.PMTranWithTrace pmTranWithTrace in allocated)
    {
      if (((PXSelectBase<PMRegister>) this.Document).Current == null)
        this.AddAllocationDocument(project);
      pmTranWithTrace.Tran = ((PXSelectBase<PMTran>) this.Transactions).Insert(pmTranWithTrace.Tran);
      pmTranWithTrace.Tran.BatchNbr = (string) null;
      if (pmTranWithTrace.NoteText != null)
        PXNoteAttribute.SetNote(((PXSelectBase) this.Transactions).Cache, (object) pmTranWithTrace.Tran, pmTranWithTrace.NoteText);
      if (pmTranWithTrace.Files != null && pmTranWithTrace.Files.Length != 0)
        PXNoteAttribute.SetFileNotes(((PXSelectBase) this.Transactions).Cache, (object) pmTranWithTrace.Tran, pmTranWithTrace.Files);
      int? nullable1 = pmTranWithTrace.Tran.OrigAccountGroupID;
      if (nullable1.HasValue)
      {
        nullable1 = pmTranWithTrace.Tran.InventoryID;
        if (nullable1.HasValue)
        {
          PMTaskAllocTotalAccum taskAllocTotalAccum1 = new PMTaskAllocTotalAccum();
          taskAllocTotalAccum1.ProjectID = pmTranWithTrace.Tran.OrigProjectID;
          taskAllocTotalAccum1.TaskID = pmTranWithTrace.Tran.OrigTaskID;
          taskAllocTotalAccum1.AccountGroupID = pmTranWithTrace.Tran.OrigAccountGroupID;
          PMTaskAllocTotalAccum taskAllocTotalAccum2 = taskAllocTotalAccum1;
          nullable1 = pmTranWithTrace.Tran.InventoryID;
          int? nullable2 = new int?(nullable1 ?? PMInventorySelectorAttribute.EmptyInventoryID);
          taskAllocTotalAccum2.InventoryID = nullable2;
          PMTaskAllocTotalAccum taskAllocTotalAccum3 = taskAllocTotalAccum1;
          nullable1 = pmTranWithTrace.Tran.CostCodeID;
          int? nullable3 = new int?(nullable1 ?? CostCodeAttribute.GetDefaultCostCode());
          taskAllocTotalAccum3.CostCodeID = nullable3;
          nullable1 = taskAllocTotalAccum1.ProjectID;
          nullable1 = nullable1.HasValue ? taskAllocTotalAccum1.TaskID : throw new PXException("In Step {0} Transaction that is processed has a null ProjectID. Please check the allocation rules in the preceding steps.", new object[1]
          {
            (object) step.StepID
          });
          nullable1 = nullable1.HasValue ? taskAllocTotalAccum1.AccountGroupID : throw new PXException("In Step {0} Transaction that is processed has a null TaskID. Please check the allocation rules in the preceding steps.", new object[1]
          {
            (object) step.StepID
          });
          if (!nullable1.HasValue)
            throw new PXException("In Step {0} Transaction that is processed has a null AllocationID. Please check the allocation rules in the preceding steps.", new object[1]
            {
              (object) step.StepID
            });
          PMTaskAllocTotalAccum taskAllocTotalAccum4 = ((PXSelectBase<PMTaskAllocTotalAccum>) this.TaskTotals).Insert(taskAllocTotalAccum1);
          PMTaskAllocTotalAccum taskAllocTotalAccum5 = taskAllocTotalAccum4;
          Decimal? nullable4 = taskAllocTotalAccum5.Amount;
          Decimal? nullable5 = pmTranWithTrace.Tran.ProjectCuryAmount;
          taskAllocTotalAccum5.Amount = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
          PMTaskAllocTotalAccum taskAllocTotalAccum6 = taskAllocTotalAccum4;
          nullable5 = taskAllocTotalAccum6.Quantity;
          nullable4 = pmTranWithTrace.Tran.BillableQty;
          taskAllocTotalAccum6.Quantity = nullable5.HasValue & nullable4.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
        }
      }
    }
    this.stepResults.Add(step.StepID.Value, allocated);
  }

  public virtual List<PMAllocator.PMTranWithTrace> ProcessBudgetStep(
    PMAllocationDetail step,
    PMProject project,
    PMTask task)
  {
    List<PMAllocator.PMTranWithTrace> pmTranWithTraceList = new List<PMAllocator.PMTranWithTrace>();
    Decimal? nullable1 = task.CompletedPctMethod != "M" ? new Decimal?(PMTaskCompletedAttribute.CalculateTaskCompletionPercentage((PXGraph) this, task)) : task.CompletedPercent;
    PMAllocator.AllocatedService allocatedService = new PMAllocator.AllocatedService((PXGraph) this, task);
    PXSelectBase<PMBudget> pxSelectBase = (PXSelectBase<PMBudget>) new PXSelectGroupBy<PMBudget, Where<PMBudget.projectID, Equal<Required<PMBudget.projectID>>, And<PMBudget.projectTaskID, Equal<Required<PMBudget.projectTaskID>>, And<PMBudget.accountGroupID, Equal<Required<PMBudget.accountGroupID>>>>>, Aggregate<GroupBy<PMBudget.accountGroupID, GroupBy<PMBudget.projectID, GroupBy<PMBudget.projectTaskID, GroupBy<PMBudget.inventoryID, GroupBy<PMBudget.costCodeID, Sum<PMBudget.curyAmount, Sum<PMBudget.qty, Sum<PMBudget.curyRevisedAmount, Sum<PMBudget.revisedQty, Sum<PMBudget.actualAmount, Sum<PMBudget.actualQty>>>>>>>>>>>>>((PXGraph) this);
    List<PMAllocator.AllocData> allocDataList = new List<PMAllocator.AllocData>();
    Dictionary<int, Decimal> dictionary1 = new Dictionary<int, Decimal>();
    Dictionary<int, Decimal> dictionary2 = new Dictionary<int, Decimal>();
    Decimal num1 = 0M;
    foreach (int num2 in (IEnumerable<int>) this.GetAccountGroupsRange(step))
    {
      foreach (PXResult<PMBudget> pxResult in pxSelectBase.Select(new object[3]
      {
        (object) task.ProjectID,
        (object) task.TaskID,
        (object) num2
      }))
      {
        PMBudget pmBudget = PXResult<PMBudget>.op_Implicit(pxResult);
        Decimal? nullable2 = nullable1;
        Decimal num3 = 0M;
        if (nullable2.GetValueOrDefault() >= num3 & nullable2.HasValue)
        {
          nullable2 = pmBudget.CuryRevisedAmount;
          if (nullable2.GetValueOrDefault() != 0M)
          {
            int accountGroupID = pmBudget.AccountGroupID.Value;
            int? nullable3 = pmBudget.InventoryID;
            int inventoryID = nullable3.Value;
            nullable3 = pmBudget.CostCodeID;
            int costCodeID = nullable3 ?? CostCodeAttribute.GetDefaultCostCode();
            PMAllocator.AllocData allocData1 = new PMAllocator.AllocData(accountGroupID, inventoryID, costCodeID);
            PMAllocator.AllocData allocData2 = allocData1;
            nullable2 = pmBudget.CuryRevisedAmount;
            Decimal num4 = PX.Objects.CM.PXCurrencyAttribute.BaseRound((PXGraph) this, nullable2.Value * nullable1.Value * 0.01M);
            allocData2.Amount = num4;
            PMAllocator.AllocData allocData3 = allocData1;
            nullable2 = pmBudget.RevisedQty;
            Decimal num5 = nullable2.Value * nullable1.Value * 0.01M;
            allocData3.Quantity = num5;
            allocData1.UOM = pmBudget.UOM;
            allocDataList.Add(allocData1);
            num1 += allocData1.Amount;
            Dictionary<int, Decimal> dictionary3 = dictionary1;
            nullable3 = pmBudget.InventoryID;
            int key1 = nullable3.Value;
            if (dictionary3.ContainsKey(key1))
            {
              Dictionary<int, Decimal> dictionary4 = dictionary1;
              nullable3 = pmBudget.InventoryID;
              int key2 = nullable3.Value;
              dictionary4[key2] += allocData1.Amount;
            }
            else
            {
              Dictionary<int, Decimal> dictionary5 = dictionary1;
              nullable3 = pmBudget.InventoryID;
              int key3 = nullable3.Value;
              Decimal amount = allocData1.Amount;
              dictionary5.Add(key3, amount);
            }
            Decimal quantity = allocData1.Quantity;
            if (allocData1.InventoryID != PMInventorySelectorAttribute.EmptyInventoryID && allocData1.UOM != null)
              quantity = this.ConvertQtyToBase(new int?(allocData1.InventoryID), allocData1.UOM, allocData1.Quantity);
            Dictionary<int, Decimal> dictionary6 = dictionary2;
            nullable3 = pmBudget.InventoryID;
            int key4 = nullable3.Value;
            if (dictionary6.ContainsKey(key4))
            {
              Dictionary<int, Decimal> dictionary7 = dictionary2;
              nullable3 = pmBudget.InventoryID;
              int key5 = nullable3.Value;
              dictionary7[key5] += quantity;
            }
            else
            {
              Dictionary<int, Decimal> dictionary8 = dictionary2;
              nullable3 = pmBudget.InventoryID;
              int key6 = nullable3.Value;
              Decimal num6 = quantity;
              dictionary8.Add(key6, num6);
            }
          }
        }
      }
    }
    foreach (PMAllocator.AllocData allocData in allocDataList)
    {
      Decimal num7 = 1M;
      Decimal amount = PX.Objects.CM.PXCurrencyAttribute.BaseRound((PXGraph) this, allocData.Amount * num7 - allocatedService.GetAllocatedAmt(allocData.AccountGroupID, allocData.InventoryID, allocData.CostCodeID));
      Decimal quantity = 0M;
      if (allocData.InventoryID != PMInventorySelectorAttribute.EmptyInventoryID)
        quantity = allocData.Quantity * num7 - allocatedService.GetAllocatedQty(allocData.AccountGroupID, allocData.InventoryID, allocData.CostCodeID);
      if (amount != 0M)
        pmTranWithTraceList.AddRange((IEnumerable<PMAllocator.PMTranWithTrace>) this.AllocateBudget(task, project, step, allocData.AccountGroupID, allocData.InventoryID, allocData.CostCodeID, allocData.UOM, amount, quantity));
    }
    return pmTranWithTraceList;
  }

  public virtual List<PMAllocator.PMTranWithTrace> AllocateBudget(
    PMTask task,
    PMProject project,
    PMAllocationDetail step,
    int origAccountGroupID,
    int inventoryID,
    int costCodeID,
    string UOM,
    Decimal amount,
    Decimal quantity)
  {
    List<PMAllocator.PMTranWithTrace> pmTranWithTraceList = new List<PMAllocator.PMTranWithTrace>();
    int num1 = 1;
    if (((PXSelectBase<PMRegister>) this.Document).Current == null)
      this.AddAllocationDocument(project);
    PMTran tran1 = new PMTran();
    if (this.PostingDate.HasValue)
      tran1.Date = this.PostingDate;
    this.SetTransactionBranch(tran1, step, project.DefaultBranchID, task.DefaultBranchID);
    PMTran tran2 = ((PXSelectBase<PMTran>) this.Transactions).Insert(tran1);
    tran2.ProjectID = task.ProjectID;
    tran2.TaskID = task.TaskID;
    tran2.UOM = UOM;
    tran2.BAccountID = task.CustomerID;
    tran2.Billable = new bool?(true);
    tran2.UseBillableQty = new bool?(true);
    tran2.BillableQty = new Decimal?(quantity);
    tran2.InventoryID = new int?(inventoryID);
    tran2.CostCodeID = new int?(costCodeID);
    tran2.LocationID = task.LocationID;
    tran2.Qty = tran2.BillableQty;
    tran2.ProjectCuryID = project.CuryID;
    tran2.TranCuryID = project.CuryID;
    tran2.TranCuryAmount = new Decimal?(amount);
    tran2.ProjectCuryAmount = new Decimal?(amount);
    if (PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>())
    {
      CMSetup cmSetup = PXResultset<CMSetup>.op_Implicit(PXSelectBase<CMSetup, PXSelect<CMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) ((PXGraph) this).GetExtension<PMAllocator.MultiCurrency>().currencyinfo).Insert(new PX.Objects.CM.Extensions.CurrencyInfo()
      {
        ModuleCode = "PM",
        BaseCuryID = project.CuryID,
        CuryID = project.CuryID,
        CuryRateTypeID = project.RateTypeID ?? cmSetup?.PMRateTypeDflt,
        CuryEffDate = tran2.TranDate,
        CuryRate = new Decimal?((Decimal) 1),
        RecipRate = new Decimal?((Decimal) 1)
      });
      tran2.ProjectCuryInfoID = currencyInfo.CuryInfoID;
    }
    Decimal? nullable1 = tran2.Qty;
    if (nullable1.HasValue)
    {
      nullable1 = tran2.Qty;
      Decimal num2 = 0M;
      if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
      {
        nullable1 = tran2.Amount;
        Decimal valueOrDefault = nullable1.GetValueOrDefault();
        nullable1 = tran2.Qty;
        Decimal num3 = nullable1.Value;
        Decimal num4 = PXDBPriceCostAttribute.Round(valueOrDefault / num3);
        tran2.TranCuryUnitRate = new Decimal?(num4);
        tran2.UnitRate = new Decimal?(num4);
      }
    }
    if (project.CuryID == project.BaseCuryID)
    {
      tran2.Amount = tran2.TranCuryAmount;
      tran2.UnitRate = tran2.TranCuryUnitRate;
    }
    tran2.AllocationID = step.AllocationID;
    PMTran pmTran1 = tran2;
    bool? nullable2 = step.UpdateGL;
    bool flag = false;
    bool? nullable3 = new bool?(nullable2.GetValueOrDefault() == flag & nullable2.HasValue);
    pmTran1.IsNonGL = nullable3;
    tran2.AccountGroupID = new int?(origAccountGroupID);
    tran2.Reverse = step.Reverse;
    tran2.CreatedByCurrentAllocation = new bool?(true);
    nullable2 = step.UpdateGL;
    if (nullable2.GetValueOrDefault())
    {
      if (step.AccountGroupOrigin == "C")
        tran2.AccountGroupID = step.AccountGroupID;
      else if (step.AccountGroupOrigin == "F")
      {
        PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) step.AccountID
        }));
        if (account != null)
          tran2.AccountGroupID = account.AccountGroupID;
      }
      tran2.ProjectID = !(step.ProjectOrigin == "C") ? task.ProjectID : step.ProjectID;
      int? nullable4;
      if (step.TaskOrigin == "C")
      {
        nullable4 = step.ProjectID;
        if (!nullable4.HasValue)
        {
          PMTask taskAndThrowIfNull = this.GetAllocationStepTaskAndThrowIfNull(tran2.ProjectID, step.TaskCD, step);
          tran2.TaskID = taskAndThrowIfNull.TaskID;
        }
        else
          tran2.TaskID = step.TaskID;
      }
      if (step.CostCodeOrigin == "C")
        tran2.CostCodeID = step.CostCodeID;
      if (step.AccountOrigin == "C")
        tran2.AccountID = step.AccountID;
      PMTran pmTran2 = tran2;
      nullable4 = step.SubID;
      int? nullable5 = nullable4 ?? task.DefaultSalesSubID;
      pmTran2.SubID = nullable5;
      if (step.OffsetAccountOrigin == "C")
        tran2.OffsetAccountID = step.OffsetAccountID;
      tran2.OffsetSubID = step.OffsetSubID;
      nullable4 = tran2.OffsetAccountID;
      if (nullable4.HasValue)
      {
        nullable4 = tran2.OffsetSubID;
        if (!nullable4.HasValue)
          tran2.OffsetSubID = tran2.SubID;
      }
      object obj1 = (object) PMSubAccountMaskAttribute.MakeSub((PXGraph) this, step, tran2, project, task);
      nullable4 = tran2.OffsetAccountID;
      if (nullable4.HasValue)
      {
        object obj2 = (object) PMOffsetSubAccountMaskAttribute.MakeSub((PXGraph) this, step, tran2, project, task);
        ((PXSelectBase) this.Transactions).Cache.RaiseFieldUpdating<PMTran.offsetSubID>((object) tran2, ref obj2);
        tran2.OffsetSubID = (int?) obj2;
      }
      ((PXSelectBase) this.Transactions).Cache.RaiseFieldUpdating<PMTran.subID>((object) tran2, ref obj1);
      tran2.SubID = (int?) obj1;
    }
    else if (step.AccountGroupOrigin == "N")
    {
      if (step.OffsetAccountGroupOrigin == "C")
        tran2.AccountGroupID = step.OffsetAccountGroupID;
      tran2.ProjectID = !(step.OffsetProjectOrigin == "C") ? task.ProjectID : step.OffsetProjectID;
      if (step.OffsetTaskOrigin == "C")
      {
        if (!step.OffsetProjectID.HasValue)
        {
          PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.taskCD, Equal<Required<PMTask.taskCD>>, And<PMTask.projectID, Equal<Required<PMTask.projectID>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) step.OffsetTaskCD,
            (object) tran2.ProjectID
          }));
          tran2.TaskID = pmTask.TaskID;
        }
        else
          tran2.TaskID = step.OffsetTaskID;
      }
      if (step.OffsetCostCodeOrigin == "C")
        tran2.CostCodeID = step.OffsetCostCodeID;
      num1 = -1;
    }
    else
    {
      if (step.AccountGroupOrigin == "C")
        tran2.AccountGroupID = step.AccountGroupID;
      tran2.ProjectID = !(step.ProjectOrigin == "C") ? task.ProjectID : step.ProjectID;
      int? nullable6;
      if (step.TaskOrigin == "C")
      {
        nullable6 = step.ProjectID;
        if (!nullable6.HasValue)
        {
          PMTask taskAndThrowIfNull = this.GetAllocationStepTaskAndThrowIfNull(tran2.ProjectID, step.TaskCD, step);
          tran2.TaskID = taskAndThrowIfNull.TaskID;
        }
        else
          tran2.TaskID = step.TaskID;
      }
      if (step.CostCodeOrigin == "C")
        tran2.CostCodeID = step.CostCodeID;
      nullable6 = step.OffsetAccountGroupID;
      if (nullable6.HasValue)
        tran2.OffsetAccountGroupID = step.OffsetAccountGroupID;
    }
    PMTran pmTran3 = tran2;
    nullable2 = step.MarkAsNotAllocated;
    bool? nullable7 = new bool?(!nullable2.GetValueOrDefault());
    pmTran3.ExcludedFromAllocation = nullable7;
    tran2.OrigProjectID = task.ProjectID;
    tran2.OrigTaskID = task.TaskID;
    tran2.OrigAccountGroupID = new int?(origAccountGroupID);
    PMAllocator.PMDataNavigator navigator = new PMAllocator.PMDataNavigator((IRateTable) this, new List<PMTran>((IEnumerable<PMTran>) new PMTran[1]
    {
      tran2
    }));
    string description;
    try
    {
      this.IsBudgetAllocation = true;
      this.CalculateFormulas(navigator, step, tran2, out Decimal? _, out Decimal? _, out Decimal? _, out description);
    }
    finally
    {
      this.IsBudgetAllocation = false;
    }
    tran2.Description = description;
    PMTran tran3;
    try
    {
      tran3 = ((PXSelectBase<PMTran>) this.Transactions).Update(tran2);
      pmTranWithTraceList.Add(new PMAllocator.PMTranWithTrace(tran3, new List<long>()));
    }
    catch (PXFieldProcessingException ex)
    {
      if (ex.FieldName.Equals(((PXSelectBase) this.Transactions).Cache.GetField(typeof (PMTran.locationID)), StringComparison.InvariantCultureIgnoreCase))
        throw new PXException(PXMessages.LocalizeFormatNoPrefix("Failed to create an allocation transaction. The location specified for the task is not valid. Check the following error for more details. {0}", new object[1]
        {
          (object) ((Exception) ex).Message
        }));
      throw new PXException(PXMessages.LocalizeFormatNoPrefix("Failed to create an allocation transaction. Check the following error for more details. {0}", new object[1]
      {
        (object) ((Exception) ex).Message
      }));
    }
    PMTaskAllocTotalAccum taskAllocTotalAccum1 = new PMTaskAllocTotalAccum();
    taskAllocTotalAccum1.ProjectID = tran3.OrigProjectID;
    taskAllocTotalAccum1.TaskID = tran3.OrigTaskID;
    taskAllocTotalAccum1.AccountGroupID = tran3.OrigAccountGroupID;
    PMTaskAllocTotalAccum taskAllocTotalAccum2 = taskAllocTotalAccum1;
    int? nullable8 = tran3.InventoryID;
    int? nullable9 = new int?(nullable8 ?? PMInventorySelectorAttribute.EmptyInventoryID);
    taskAllocTotalAccum2.InventoryID = nullable9;
    PMTaskAllocTotalAccum taskAllocTotalAccum3 = taskAllocTotalAccum1;
    nullable8 = tran3.CostCodeID;
    int? nullable10 = new int?(nullable8 ?? CostCodeAttribute.GetDefaultCostCode());
    taskAllocTotalAccum3.CostCodeID = nullable10;
    PMTaskAllocTotalAccum taskAllocTotalAccum4 = ((PXSelectBase<PMTaskAllocTotalAccum>) this.TaskTotals).Insert(taskAllocTotalAccum1);
    PMTaskAllocTotalAccum taskAllocTotalAccum5 = taskAllocTotalAccum4;
    Decimal? amount1 = taskAllocTotalAccum5.Amount;
    Decimal? nullable11 = tran3.ProjectCuryAmount;
    taskAllocTotalAccum5.Amount = amount1.HasValue & nullable11.HasValue ? new Decimal?(amount1.GetValueOrDefault() + nullable11.GetValueOrDefault()) : new Decimal?();
    PMTaskAllocTotalAccum taskAllocTotalAccum6 = taskAllocTotalAccum4;
    nullable11 = taskAllocTotalAccum6.Quantity;
    Decimal? nullable12 = tran3.BillableQty;
    taskAllocTotalAccum6.Quantity = nullable11.HasValue & nullable12.HasValue ? new Decimal?(nullable11.GetValueOrDefault() + nullable12.GetValueOrDefault()) : new Decimal?();
    PMTran pmTran4 = tran3;
    nullable12 = pmTran4.Amount;
    Decimal num5 = (Decimal) num1;
    Decimal? nullable13;
    if (!nullable12.HasValue)
    {
      nullable11 = new Decimal?();
      nullable13 = nullable11;
    }
    else
      nullable13 = new Decimal?(nullable12.GetValueOrDefault() * num5);
    pmTran4.Amount = nullable13;
    PMTran pmTran5 = tran3;
    nullable12 = pmTran5.TranCuryAmount;
    Decimal num6 = (Decimal) num1;
    Decimal? nullable14;
    if (!nullable12.HasValue)
    {
      nullable11 = new Decimal?();
      nullable14 = nullable11;
    }
    else
      nullable14 = new Decimal?(nullable12.GetValueOrDefault() * num6);
    pmTran5.TranCuryAmount = nullable14;
    PMTran pmTran6 = tran3;
    nullable12 = pmTran6.ProjectCuryAmount;
    Decimal num7 = (Decimal) num1;
    Decimal? nullable15;
    if (!nullable12.HasValue)
    {
      nullable11 = new Decimal?();
      nullable15 = nullable11;
    }
    else
      nullable15 = new Decimal?(nullable12.GetValueOrDefault() * num7);
    pmTran6.ProjectCuryAmount = nullable15;
    PMTran pmTran7 = tran3;
    nullable12 = pmTran7.Qty;
    Decimal num8 = (Decimal) num1;
    Decimal? nullable16;
    if (!nullable12.HasValue)
    {
      nullable11 = new Decimal?();
      nullable16 = nullable11;
    }
    else
      nullable16 = new Decimal?(nullable12.GetValueOrDefault() * num8);
    pmTran7.Qty = nullable16;
    PMTran pmTran8 = tran3;
    nullable12 = pmTran8.BillableQty;
    Decimal num9 = (Decimal) num1;
    Decimal? nullable17;
    if (!nullable12.HasValue)
    {
      nullable11 = new Decimal?();
      nullable17 = nullable11;
    }
    else
      nullable17 = new Decimal?(nullable12.GetValueOrDefault() * num9);
    pmTran8.BillableQty = nullable17;
    return pmTranWithTraceList;
  }

  public virtual void SetTransactionBranch(
    PMTran tran,
    PMAllocationDetail step,
    int? ProjectBranchID,
    int? TaskBranchID)
  {
    if (step.Method == "B" && step.OffsetBranchOrigin == "S")
      tran.BranchID = TaskBranchID ?? ProjectBranchID ?? ((PXGraph) this).Accessinfo.BranchID;
    else
      tran.BranchID = step.TargetBranchID ?? ((PXGraph) this).Accessinfo.BranchID;
  }

  public virtual void AddSourceTrans(PMAllocationDetail step, List<PMTran> sourceTrans)
  {
    foreach (PMTran sourceTran in sourceTrans)
      ((PXSelectBase<PMAllocationSourceTran>) this.SourceTran).Insert(this.CreateAllocationTran(step.AllocationID, step.StepID, sourceTran));
  }

  public virtual void AddAuditTran(string allocationID, long? tranID, List<long> sourceTrans)
  {
    foreach (long sourceTran in sourceTrans)
      ((PXSelectBase<PMAllocationAuditTran>) this.AuditTran).Insert(new PMAllocationAuditTran()
      {
        AllocationID = allocationID,
        SourceTranID = new long?(sourceTran),
        TranID = tranID
      });
  }

  public virtual void AddAllocationDocument(PMProject project)
  {
    ((PXSelectBase) this.Document).Cache.Insert();
    ((PXSelectBase<PMRegister>) this.Document).Current.OrigDocType = "AL";
    ((PXSelectBase<PMRegister>) this.Document).Current.Description = PXMessages.LocalizeFormatNoPrefix("Allocation for {0}", new object[1]
    {
      (object) project.ContractCD
    });
    ((PXSelectBase<PMRegister>) this.Document).Current.IsAllocation = new bool?(true);
  }

  public virtual string GetConcatenatedDescription(
    PMAllocationDetail step,
    PMTran tran,
    PMTask task)
  {
    string concatenatedDescription = "";
    if (step.GroupByDate.GetValueOrDefault())
      concatenatedDescription = $"{tran.Date}: ";
    if (step.GroupByItem.GetValueOrDefault())
    {
      PX.Objects.AR.Customer customer = (PX.Objects.AR.Customer) null;
      if (task != null && task.CustomerID.HasValue)
        customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelectReadonly<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) task.CustomerID
        }));
      PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) tran.InventoryID
      }));
      if (inventoryItem != null)
      {
        using (new PXLocaleScope(customer?.LocaleName))
          concatenatedDescription += $"{inventoryItem.InventoryCD} {((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)].GetValueExt<PX.Objects.IN.InventoryItem.descr>((object) inventoryItem)}";
      }
    }
    else if (step.GroupByEmployee.GetValueOrDefault())
    {
      PX.Objects.CR.BAccount baccount = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) tran.ResourceID
      }));
      if (baccount != null)
        concatenatedDescription += $"{baccount.AcctCD}";
    }
    return concatenatedDescription;
  }

  public virtual List<Group> BreakIntoGroups(PMAllocationDetail step, List<PMTran> fullList)
  {
    List<PMTran> list;
    if (!step.AllocateNonBillable.GetValueOrDefault())
    {
      list = new List<PMTran>(fullList.Count);
      foreach (PMTran full in fullList)
      {
        if (full.Billable.GetValueOrDefault())
          list.Add(full);
      }
    }
    else
      list = fullList;
    return this.CreateGrouping(step).BreakIntoGroups(list);
  }

  public virtual Grouping CreateGrouping(PMAllocationDetail step)
  {
    return new Grouping((IComparer<PMTran>) new PMTranComparer(step.GroupByItem, step.GroupByVendor, step.GroupByDate, step.GroupByEmployee, step.AccountGroupOrigin == "S" || step.OffsetAccountGroupOrigin == "S"));
  }

  public virtual bool CanAllocate(PMAllocationDetail step, PMTran tran)
  {
    bool flag = false;
    if (!step.AllocateZeroQty.GetValueOrDefault())
    {
      Decimal? qty = tran.Qty;
      Decimal num = 0M;
      if (qty.GetValueOrDefault() == num & qty.HasValue)
        goto label_3;
    }
    flag = true;
label_3:
    if (!step.AllocateZeroAmount.GetValueOrDefault())
    {
      Decimal? amount = tran.Amount;
      Decimal num = 0M;
      if (amount.GetValueOrDefault() == num & amount.HasValue)
        goto label_6;
    }
    flag = true;
label_6:
    return flag;
  }

  public virtual object Evaluate(
    PMObjectType objectName,
    string fieldName,
    string attribute,
    PMTran row)
  {
    switch (objectName)
    {
      case PMObjectType.PMTran:
        return this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PMTran)].GetValueExt((object) row, fieldName));
      case PMObjectType.PMProject:
        PMProject pmProject = PMProject.PK.Find((PXGraph) this, row.ProjectID);
        if (pmProject != null)
          return attribute != null ? this.EvaluateAttribute(attribute, pmProject.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PMProject)].GetValueExt((object) pmProject, fieldName));
        break;
      case PMObjectType.PMTask:
        PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, row.ProjectID, row.TaskID);
        if (dirty != null)
          return attribute != null ? this.EvaluateAttribute(attribute, dirty.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PMTask)].GetValueExt((object) dirty, fieldName));
        break;
      case PMObjectType.PMAccountGroup:
        PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, row.AccountGroupID);
        if (pmAccountGroup != null)
          return attribute != null ? this.EvaluateAttribute(attribute, pmAccountGroup.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PMAccountGroup)].GetValueExt((object) pmAccountGroup, fieldName));
        break;
      case PMObjectType.EPEmployee:
        EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.ResourceID
        }));
        if (epEmployee != null)
          return attribute != null ? this.EvaluateAttribute(attribute, epEmployee.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (EPEmployee)].GetValueExt((object) epEmployee, fieldName));
        break;
      case PMObjectType.Customer:
        PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.BAccountID
        }));
        if (customer != null)
          return attribute != null ? this.EvaluateAttribute(attribute, customer.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PX.Objects.AR.Customer)].GetValueExt((object) customer, fieldName));
        break;
      case PMObjectType.Vendor:
        VendorR vendorR = PXResultset<VendorR>.op_Implicit(PXSelectBase<VendorR, PXSelect<VendorR, Where<VendorR.bAccountID, Equal<Required<VendorR.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.BAccountID
        }));
        if (vendorR != null)
          return attribute != null ? this.EvaluateAttribute(attribute, vendorR.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (VendorR)].GetValueExt((object) vendorR, fieldName));
        break;
      case PMObjectType.InventoryItem:
        PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.InventoryID
        }));
        if (inventoryItem != null)
          return attribute != null ? this.EvaluateAttribute(attribute, inventoryItem.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)].GetValueExt((object) inventoryItem, fieldName));
        break;
      case PMObjectType.PMBudget:
        PXSelect<PMBudget, Where<PMBudget.accountGroupID, Equal<Required<PMBudget.accountGroupID>>, And<PMBudget.projectID, Equal<Required<PMBudget.projectID>>, And<PMBudget.projectTaskID, Equal<Required<PMBudget.projectTaskID>>, And<PMBudget.inventoryID, Equal<Required<PMBudget.inventoryID>>, And<PMBudget.costCodeID, Equal<Required<PMBudget.costCodeID>>>>>>>> pxSelect = new PXSelect<PMBudget, Where<PMBudget.accountGroupID, Equal<Required<PMBudget.accountGroupID>>, And<PMBudget.projectID, Equal<Required<PMBudget.projectID>>, And<PMBudget.projectTaskID, Equal<Required<PMBudget.projectTaskID>>, And<PMBudget.inventoryID, Equal<Required<PMBudget.inventoryID>>, And<PMBudget.costCodeID, Equal<Required<PMBudget.costCodeID>>>>>>>>((PXGraph) this);
        int? accountGroupID = row.AccountGroupID;
        if (this.IsBudgetAllocation && row.OrigAccountGroupID.HasValue)
          accountGroupID = row.OrigAccountGroupID;
        PMProject project = PMProject.PK.Find((PXGraph) this, row.ProjectID);
        PX.Objects.PM.Lite.PMBudget pmBudget1 = new BudgetService((PXGraph) this).SelectProjectBalance(PMAccountGroup.PK.Find((PXGraph) this, accountGroupID), project, row.TaskID, row.InventoryID, row.CostCodeID, out bool _);
        object[] objArray = new object[5]
        {
          (object) accountGroupID,
          (object) row.ProjectID,
          (object) row.TaskID,
          (object) pmBudget1.InventoryID,
          (object) pmBudget1.CostCodeID
        };
        PMBudget pmBudget2 = PXResultset<PMBudget>.op_Implicit(((PXSelectBase<PMBudget>) pxSelect).Select(objArray));
        if (pmBudget2 != null)
          return this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PMBudget)].GetValueExt((object) pmBudget2, fieldName));
        break;
    }
    return (object) null;
  }

  public virtual Decimal? GetPrice(PMTran tran)
  {
    Decimal? price = new Decimal?();
    if (tran.InventoryID.HasValue)
    {
      int? inventoryId1 = tran.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (!(inventoryId1.GetValueOrDefault() == emptyInventoryId & inventoryId1.HasValue))
      {
        string str1 = "BASE";
        PMProject project = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Required<PMProject.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) tran.ProjectID
        }));
        PMTask pmTask = (PMTask) PXSelectorAttribute.Select(((PXGraph) this).Caches[typeof (PMTran)], (object) tran, "TaskID");
        PX.Objects.CR.Location location = (PX.Objects.CR.Location) PXSelectorAttribute.Select(((PXGraph) this).Caches[typeof (PMTask)], (object) pmTask, "LocationID");
        if (location != null && !string.IsNullOrEmpty(location.CPriceClassID))
          str1 = location.CPriceClassID;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.GetCurrencyInfo(project, tran.TranCuryID, tran.Date);
        bool flag = false;
        string str2 = location?.CTaxCalcMode ?? "T";
        PXCache cach1 = ((PXGraph) this).Caches[typeof (PMTran)];
        string custPriceClass1 = str1;
        int? customerId1 = pmTask.CustomerID;
        int? inventoryId2 = tran.InventoryID;
        PX.Objects.CM.CurrencyInfo cm1 = currencyInfo.GetCM();
        Decimal? qty1 = tran.Qty;
        string uom1 = tran.UOM;
        DateTime? date1 = tran.Date;
        DateTime date2 = date1.Value;
        int num = flag ? 1 : 0;
        string taxCalcMode1 = str2;
        price = ARSalesPriceMaint.CalculateSalesPrice(cach1, custPriceClass1, customerId1, inventoryId2, cm1, qty1, uom1, date2, num != 0, taxCalcMode1);
        if (!flag && !price.HasValue)
        {
          PXCache cach2 = ((PXGraph) this).Caches[typeof (PMTran)];
          string custPriceClass2 = str1;
          int? customerId2 = pmTask.CustomerID;
          int? inventoryId3 = tran.InventoryID;
          PX.Objects.CM.CurrencyInfo cm2 = currencyInfo.GetCM();
          Decimal? qty2 = tran.Qty;
          string uom2 = tran.UOM;
          date1 = tran.Date;
          DateTime date3 = date1.Value;
          string taxCalcMode2 = str2;
          price = ARSalesPriceMaint.CalculateSalesPrice(cach2, custPriceClass2, customerId2, inventoryId3, cm2, qty2, uom2, date3, true, taxCalcMode2);
        }
      }
    }
    return price;
  }

  public virtual PX.Objects.CM.Extensions.CurrencyInfo GetCurrencyInfo(
    PMProject project,
    string curyID,
    DateTime? effectiveDate)
  {
    PX.Objects.CM.Extensions.CurrencyInfo info = new PX.Objects.CM.Extensions.CurrencyInfo()
    {
      ModuleCode = "PM",
      BaseCuryID = project.BaseCuryID
    };
    if (curyID == project.BaseCuryID)
    {
      info.CuryID = project.BaseCuryID;
      info.CuryRate = new Decimal?((Decimal) 1);
      info.RecipRate = new Decimal?((Decimal) 1);
      info.CuryMultDiv = "M";
    }
    else if (project.CuryID == curyID && curyID != project.BaseCuryID)
    {
      info = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) project.CuryInfoID
      }));
    }
    else
    {
      CMSetup cmSetup = PXResultset<CMSetup>.op_Implicit(PXSelectBase<CMSetup, PXSelect<CMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      string str = project.RateTableID ?? cmSetup?.PMRateTypeDflt;
      if (!effectiveDate.HasValue)
        effectiveDate = new DateTime?(((PXGraph) this).Accessinfo.BusinessDate ?? DateTime.Now);
      info.CuryID = curyID;
      info.CuryRateTypeID = str;
      info.CuryEffDate = effectiveDate;
      IPXCurrencyRate rate = info.SearchForNewRate((PXGraph) this);
      if (rate != null)
        rate.Populate(info);
    }
    return info;
  }

  public virtual object ConvertFromExtValue(object extValue)
  {
    return extValue is PXFieldState pxFieldState ? pxFieldState.Value : extValue;
  }

  public virtual object EvaluateAttribute(string attribute, Guid? refNoteID)
  {
    PXResultset<CSAnswers> pxResultset = PXSelectBase<CSAnswers, PXSelectJoin<CSAnswers, InnerJoin<CSAttribute, On<CSAttribute.attributeID, Equal<CSAnswers.attributeID>>>, Where<CSAnswers.refNoteID, Equal<Required<CSAnswers.refNoteID>>, And<CSAnswers.attributeID, Equal<Required<CSAnswers.attributeID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) refNoteID,
      (object) attribute
    });
    CSAnswers csAnswers = (CSAnswers) null;
    CSAttribute csAttribute = (CSAttribute) null;
    if (pxResultset.Count > 0)
    {
      csAnswers = (CSAnswers) ((PXResult) pxResultset[0])[0];
      csAttribute = (CSAttribute) ((PXResult) pxResultset[0])[1];
    }
    if (csAnswers == null || csAnswers.AttributeID == null)
    {
      csAttribute = PXResultset<CSAttribute>.op_Implicit(PXSelectBase<CSAttribute, PXSelect<CSAttribute, Where<CSAttribute.attributeID, Equal<Required<CSAttribute.attributeID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) attribute
      }));
      if (csAttribute != null)
      {
        object defaultValueFor = KeyValueHelper.GetDefaultValueFor(csAttribute.ControlType);
        if (defaultValueFor != null)
          return defaultValueFor;
      }
    }
    if (csAnswers != null)
    {
      if (csAnswers.Value != null)
        return (object) csAnswers.Value;
      if (csAttribute != null)
      {
        object defaultValueFor = KeyValueHelper.GetDefaultValueFor(csAttribute.ControlType);
        if (defaultValueFor != null)
          return defaultValueFor;
      }
    }
    return (object) string.Empty;
  }

  public virtual IList<PMTran> Transform(
    PMAllocationDetail step,
    PMTask task,
    PMTran original,
    bool calculateFormulas,
    bool isWipStep)
  {
    List<PMTran> pmTranList = new List<PMTran>();
    int? accountGroupFromStep1 = this.GetDebitAccountGroupFromStep(step, original);
    int? accountGroupFromStep2 = this.GetCreditAccountGroupFromStep(step, original);
    int? debitProjectFromStep = this.GetDebitProjectFromStep(step, original);
    int? debitTaskFromStep = this.GetDebitTaskFromStep(step, task, debitProjectFromStep);
    int? costCodeFromStep = this.GetCostCodeFromStep(step, original);
    Decimal? qty = new Decimal?();
    Decimal? billableQty = new Decimal?();
    Decimal? amt = new Decimal?();
    string description = (string) null;
    if (!accountGroupFromStep1.HasValue)
    {
      string message = PXLocalizer.LocalizeFormat("Allocation Step '{0}' is not valid. When applied to transactions in Task '{1}' failed to set Account Group. Please correct your Allocation rules and try again.", new object[2]
      {
        (object) step.StepID,
        (object) task.TaskCD
      });
      PXTrace.WriteError(message);
      throw new PMAllocationException(original.RefNbr, message);
    }
    if (calculateFormulas)
      this.CalculateFormulas(new PMAllocator.PMDataNavigator((IRateTable) this, new List<PMTran>((IEnumerable<PMTran>) new PMTran[1]
      {
        original
      })), step, original, out qty, out billableQty, out amt, out description);
    if (!accountGroupFromStep2.HasValue)
    {
      bool? updateGl = step.UpdateGL;
      bool flag = false;
      if (updateGl.GetValueOrDefault() == flag & updateGl.HasValue)
      {
        PMProject project = PMProject.PK.Find((PXGraph) this, debitProjectFromStep);
        PMTran fromTemplate = this.CreateFromTemplate(step, original, isWipStep);
        fromTemplate.ExcludedFromAllocation = new bool?(!step.MarkAsNotAllocated.GetValueOrDefault());
        fromTemplate.AccountGroupID = accountGroupFromStep1;
        fromTemplate.ProjectID = debitProjectFromStep;
        fromTemplate.TaskID = debitTaskFromStep;
        fromTemplate.CostCodeID = costCodeFromStep;
        fromTemplate.CreatedByCurrentAllocation = new bool?(true);
        this.SetCurrencyInfoOnProjectChange(fromTemplate, original);
        if (CostCodeAttribute.UseCostCode() && !fromTemplate.CostCodeID.HasValue)
          fromTemplate.CostCodeID = CostCodeAttribute.DefaultCostCode;
        if (step.UpdateGL.GetValueOrDefault())
        {
          switch (step.AccountOrigin)
          {
            case "C":
              fromTemplate.AccountID = step.AccountID;
              fromTemplate.SubID = original.SubID ?? step.SubID;
              break;
            case "X":
              fromTemplate.AccountID = original.OffsetAccountID;
              fromTemplate.SubID = original.OffsetSubID;
              break;
            default:
              fromTemplate.AccountID = original.AccountID;
              fromTemplate.SubID = original.SubID;
              break;
          }
          if (original.SubID.HasValue)
          {
            PMSubAccountMaskAttribute.VerifyMask(step, original, project, task);
            object obj = (object) PMSubAccountMaskAttribute.MakeSub((PXGraph) this, step, original, project, task);
            ((PXSelectBase) this.Transactions).Cache.RaiseFieldUpdating<PMTran.subID>((object) fromTemplate, ref obj);
            fromTemplate.SubID = (int?) obj;
          }
          else
            fromTemplate.SubID = step.SubID;
        }
        if (calculateFormulas)
        {
          fromTemplate.Description = description;
          fromTemplate.BillableQty = billableQty;
          fromTemplate.Qty = qty;
          Decimal rawAmount;
          this.SetCalculatedAmount(project, fromTemplate, amt.GetValueOrDefault(), out rawAmount);
          if (billableQty.HasValue)
          {
            Decimal? nullable = billableQty;
            Decimal num = 0M;
            if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
            {
              fromTemplate.Billable = new bool?(true);
              fromTemplate.UseBillableQty = new bool?(true);
              fromTemplate.UnitRate = new Decimal?(rawAmount / billableQty.Value);
              fromTemplate.TranCuryUnitRate = new Decimal?(amt.GetValueOrDefault() / billableQty.Value);
            }
          }
        }
        pmTranList.Add(fromTemplate);
        goto label_45;
      }
    }
    PMProject project1 = PMProject.PK.Find((PXGraph) this, debitProjectFromStep);
    PMTran fromTemplate1 = this.CreateFromTemplate(step, original, isWipStep);
    fromTemplate1.ExcludedFromAllocation = new bool?(!step.MarkAsNotAllocated.GetValueOrDefault());
    fromTemplate1.AccountGroupID = accountGroupFromStep1;
    fromTemplate1.OffsetAccountGroupID = accountGroupFromStep2;
    fromTemplate1.ProjectID = debitProjectFromStep;
    fromTemplate1.TaskID = debitTaskFromStep;
    fromTemplate1.CostCodeID = costCodeFromStep;
    fromTemplate1.CreatedByCurrentAllocation = new bool?(true);
    this.SetCurrencyInfoOnProjectChange(fromTemplate1, original);
    if (CostCodeAttribute.UseCostCode() && !fromTemplate1.CostCodeID.HasValue)
      fromTemplate1.CostCodeID = CostCodeAttribute.DefaultCostCode;
    if (step.UpdateGL.GetValueOrDefault())
    {
      switch (step.AccountOrigin)
      {
        case "C":
          fromTemplate1.AccountID = step.AccountID;
          fromTemplate1.SubID = original.SubID ?? step.SubID;
          break;
        case "X":
          fromTemplate1.AccountID = original.OffsetAccountID;
          fromTemplate1.SubID = original.OffsetSubID;
          break;
        default:
          fromTemplate1.AccountID = original.AccountID;
          fromTemplate1.SubID = original.SubID;
          break;
      }
      switch (step.OffsetAccountOrigin)
      {
        case "C":
          fromTemplate1.OffsetAccountID = step.OffsetAccountID;
          fromTemplate1.OffsetSubID = original.OffsetSubID ?? step.OffsetSubID ?? original.SubID;
          break;
        case "X":
          if (!original.AccountID.HasValue)
            throw new PXException("Allocation rule is configured to take Debit Account from the source transaction and use it as a Credit Account of allocated transaction but the Debit Account is not set for the source transaction. Rule:{0} Step:{1} Transaction Description:{2}", new object[3]
            {
              (object) step.AllocationID,
              (object) step.StepID,
              (object) original.Description
            });
          fromTemplate1.OffsetAccountID = original.AccountID;
          fromTemplate1.OffsetSubID = original.SubID;
          break;
        default:
          fromTemplate1.OffsetAccountID = original.OffsetAccountID ?? original.AccountID;
          fromTemplate1.OffsetSubID = original.OffsetSubID ?? original.SubID;
          break;
      }
      object obj1 = (object) null;
      if (step.AccountOrigin != "N")
      {
        PMSubAccountMaskAttribute.VerifyMask(step, fromTemplate1, project1, task);
        obj1 = (object) PMSubAccountMaskAttribute.MakeSub((PXGraph) this, step, fromTemplate1, project1, task);
      }
      if (fromTemplate1.OffsetAccountID.HasValue)
      {
        PMOffsetSubAccountMaskAttribute.VerifyMask(step, fromTemplate1, project1, task);
        object obj2 = (object) PMOffsetSubAccountMaskAttribute.MakeSub((PXGraph) this, step, fromTemplate1, project1, task);
        ((PXSelectBase) this.Transactions).Cache.RaiseFieldUpdating<PMTran.offsetSubID>((object) fromTemplate1, ref obj2);
        fromTemplate1.OffsetSubID = (int?) obj2;
      }
      if (obj1 != null)
      {
        ((PXSelectBase) this.Transactions).Cache.RaiseFieldUpdating<PMTran.subID>((object) fromTemplate1, ref obj1);
        fromTemplate1.SubID = (int?) obj1;
      }
    }
    if (calculateFormulas)
    {
      fromTemplate1.Description = description;
      fromTemplate1.BillableQty = billableQty;
      fromTemplate1.Qty = qty;
      Decimal rawAmount;
      this.SetCalculatedAmount(project1, fromTemplate1, amt.GetValueOrDefault(), out rawAmount);
      if (billableQty.HasValue)
      {
        Decimal? nullable = billableQty;
        Decimal num = 0M;
        if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        {
          fromTemplate1.Billable = new bool?(true);
          fromTemplate1.UseBillableQty = new bool?(true);
          fromTemplate1.UnitRate = new Decimal?(rawAmount / billableQty.Value);
          fromTemplate1.TranCuryUnitRate = new Decimal?(amt.GetValueOrDefault() / billableQty.Value);
        }
      }
    }
    pmTranList.Add(fromTemplate1);
label_45:
    return (IList<PMTran>) pmTranList;
  }

  protected virtual void SetCurrencyInfoOnProjectChange(PMTran tran, PMTran original)
  {
    int? projectId1 = tran.ProjectID;
    int? projectId2 = original.ProjectID;
    if (projectId1.GetValueOrDefault() == projectId2.GetValueOrDefault() & projectId1.HasValue == projectId2.HasValue)
      return;
    PMProject pmProject1 = PMProject.PK.Find((PXGraph) this, original.ProjectID);
    PMProject pmProject2 = PMProject.PK.Find((PXGraph) this, tran.ProjectID);
    string str1 = string.IsNullOrEmpty(pmProject1.BaseCuryID) ? ((PXGraph) this).Accessinfo.BaseCuryID : pmProject1.BaseCuryID;
    string str2 = string.IsNullOrEmpty(pmProject2.BaseCuryID) ? ((PXGraph) this).Accessinfo.BaseCuryID : pmProject2.BaseCuryID;
    string str3 = string.IsNullOrEmpty(pmProject1.CuryID) ? ((PXGraph) this).Accessinfo.BaseCuryID : pmProject1.CuryID;
    string baseCuryID = string.IsNullOrEmpty(pmProject2.CuryID) ? ((PXGraph) this).Accessinfo.BaseCuryID : pmProject2.CuryID;
    string str4 = str2;
    if (str1 != str4)
    {
      if (tran.TranCuryID == str2)
      {
        PX.Objects.CM.Extensions.CurrencyInfo directRate = this.MultiCurrencyService.CreateDirectRate((PXGraph) this, str2, tran.Date, "PM");
        tran.BaseCuryInfoID = directRate.CuryInfoID;
      }
      else
      {
        PX.Objects.CM.Extensions.CurrencyInfo rate = this.MultiCurrencyService.CreateRate((PXGraph) this, tran.TranCuryID, str2, tran.Date, pmProject2.RateTypeID, "PM");
        tran.BaseCuryInfoID = rate.CuryInfoID;
      }
    }
    if (!(str3 != baseCuryID))
      return;
    if (baseCuryID == tran.TranCuryID)
    {
      PX.Objects.CM.Extensions.CurrencyInfo directRate = this.MultiCurrencyService.CreateDirectRate((PXGraph) this, tran.TranCuryID, tran.Date, "PM");
      tran.ProjectCuryInfoID = directRate.CuryInfoID;
    }
    else
    {
      PX.Objects.CM.Extensions.CurrencyInfo rate = this.MultiCurrencyService.CreateRate((PXGraph) this, tran.TranCuryID, baseCuryID, tran.Date, pmProject2.RateTypeID, "PM");
      tran.ProjectCuryInfoID = rate.CuryInfoID;
    }
  }

  protected virtual int? GetDebitAccountGroupFromStep(PMAllocationDetail step, PMTran original)
  {
    int? accountGroupFromStep = new int?();
    if (step.AccountGroupOrigin == "C")
      accountGroupFromStep = step.AccountGroupID;
    else if (step.AccountGroupOrigin == "F")
    {
      if (step.AccountOrigin == "S")
      {
        PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) original.AccountID
        }));
        if (account != null)
          accountGroupFromStep = account.AccountGroupID;
      }
      else if (step.AccountOrigin == "X")
      {
        PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) original.OffsetAccountID
        }));
        if (account != null)
          accountGroupFromStep = account.AccountGroupID;
      }
      else
      {
        PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) step.AccountID
        }));
        if (account != null)
          accountGroupFromStep = account.AccountGroupID;
      }
    }
    else
    {
      if (step.AccountID.HasValue)
        PXTrace.WriteWarning("Step {0} is Debit Account Group configured as {1} but an Account is supplied.", new object[2]
        {
          (object) step.StepID,
          (object) step.AccountGroupOrigin
        });
      accountGroupFromStep = original.AccountGroupID;
    }
    return accountGroupFromStep;
  }

  protected virtual int? GetCreditAccountGroupFromStep(PMAllocationDetail step, PMTran original)
  {
    int? accountGroupFromStep = new int?();
    if (step.OffsetAccountGroupOrigin == "C")
      accountGroupFromStep = step.OffsetAccountGroupID;
    else if (step.OffsetAccountGroupOrigin == "F")
    {
      if (step.OffsetAccountOrigin == "S")
      {
        PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) original.OffsetAccountID
        }));
        if (account != null)
          accountGroupFromStep = account.AccountGroupID;
      }
      else if (step.OffsetAccountOrigin == "X")
      {
        PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) original.AccountID
        }));
        if (account != null)
          accountGroupFromStep = account.AccountGroupID;
      }
      else
      {
        PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) step.OffsetAccountID
        }));
        if (account != null)
          accountGroupFromStep = account.AccountGroupID;
      }
    }
    else if (step.OffsetAccountGroupOrigin == "S")
    {
      if (step.OffsetAccountID.HasValue)
        PXTrace.WriteWarning("Step {0} is Credit Account Group configured as {1} but an Account is supplied.", new object[2]
        {
          (object) step.StepID,
          (object) step.OffsetAccountGroupOrigin
        });
      accountGroupFromStep = original.AccountGroupID;
    }
    return accountGroupFromStep;
  }

  protected virtual int? GetDebitProjectFromStep(PMAllocationDetail step, PMTran original)
  {
    return step.ProjectOrigin == "C" ? step.ProjectID : original.ProjectID;
  }

  protected virtual int? GetDebitTaskFromStep(
    PMAllocationDetail step,
    PMTask task,
    int? debitProjectID)
  {
    if (ProjectDefaultAttribute.IsNonProject(debitProjectID))
      return new int?();
    int? nullable = new int?();
    int? debitTaskFromStep;
    if (step.TaskOrigin == "C")
    {
      debitTaskFromStep = step.ProjectID.HasValue ? step.TaskID : this.GetAllocationStepTaskAndThrowIfNull(debitProjectID, step.TaskCD, step).TaskID;
    }
    else
    {
      PMTask taskByTaskCd = this.GetTaskByTaskCD(debitProjectID, task.TaskCD);
      if (taskByTaskCd == null)
      {
        PMProject pmProject = PMProject.PK.Find((PXGraph) this, debitProjectID);
        if (pmProject == null)
          throw new PXException("Step '{0}': Debit Project was not found in the system.", new object[1]
          {
            (object) step.StepID
          });
        throw new PXException("The {0} step of the {1} allocation rule cannot assign the debit task. The {2} task has not been found for the {3} project.", new object[4]
        {
          (object) step.StepID,
          (object) step.AllocationID,
          (object) task.TaskCD,
          (object) pmProject.ContractCD
        });
      }
      debitTaskFromStep = taskByTaskCd.TaskID;
    }
    return debitTaskFromStep;
  }

  protected virtual int? GetCostCodeFromStep(PMAllocationDetail step, PMTran original)
  {
    return step.CostCodeOrigin == "C" ? step.CostCodeID : original.CostCodeID;
  }

  protected PMTask GetAllocationStepTaskAndThrowIfNull(
    int? projectID,
    string taskCD,
    PMAllocationDetail step)
  {
    return this.GetTaskByTaskCD(projectID, taskCD) ?? throw new PMAllocationNotFoundException("The {0} allocation step cannot be completed because the {1} project does not include the {2} project task which is specified in the settings of the {3} allocation rule. Review and correct the allocation rule settings on the Allocation Settings tab of the Allocation Rules (PM207500) form.", new object[4]
    {
      (object) step.StepID,
      (object) PMProject.PK.Find((PXGraph) this, projectID)?.ContractCD,
      (object) taskCD,
      (object) step.AllocationID
    });
  }

  protected PMTask GetTaskByTaskCD(int? projectID, string taskCD)
  {
    if (!projectID.HasValue || string.IsNullOrWhiteSpace(taskCD))
      return (PMTask) null;
    return PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskCD, Equal<Required<PMTask.taskCD>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) projectID,
      (object) taskCD
    }));
  }

  protected virtual void SetCalculatedAmount(
    PMProject project,
    PMTran tran,
    Decimal amt,
    out Decimal rawAmount)
  {
    PMAllocator.MultiCurrency extension = ((PXGraph) this).GetExtension<PMAllocator.MultiCurrency>();
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = extension.GetCurrencyInfo(tran.BaseCuryInfoID);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = extension.GetCurrencyInfo(tran.ProjectCuryInfoID);
    if (tran.TranCuryID == project.CuryID && tran.TranCuryID == project.BaseCuryID)
    {
      rawAmount = amt;
      tran.TranCuryAmount = new Decimal?(currencyInfo1.RoundBase(amt));
      tran.ProjectCuryAmount = tran.TranCuryAmount;
      tran.Amount = tran.TranCuryAmount;
    }
    else if (tran.TranCuryID != project.CuryID && tran.TranCuryID == project.BaseCuryID)
    {
      rawAmount = amt;
      tran.TranCuryAmount = new Decimal?(currencyInfo1.RoundBase(amt));
      tran.Amount = tran.TranCuryAmount;
      tran.ProjectCuryAmount = new Decimal?(currencyInfo2.CuryConvBase(tran.TranCuryAmount.GetValueOrDefault()));
    }
    else if (tran.TranCuryID == project.CuryID && tran.TranCuryID != project.BaseCuryID)
    {
      rawAmount = currencyInfo1.CuryConvBase(amt);
      tran.TranCuryAmount = new Decimal?(currencyInfo1.RoundBase(amt));
      tran.ProjectCuryAmount = tran.TranCuryAmount;
      tran.Amount = new Decimal?(currencyInfo1.CuryConvBase(tran.TranCuryAmount.GetValueOrDefault()));
    }
    else
    {
      rawAmount = currencyInfo1.CuryConvBase(amt);
      tran.TranCuryAmount = new Decimal?(currencyInfo1.RoundBase(amt));
      PMTran pmTran1 = tran;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = currencyInfo2;
      Decimal? tranCuryAmount = tran.TranCuryAmount;
      Decimal valueOrDefault1 = tranCuryAmount.GetValueOrDefault();
      Decimal? nullable1 = new Decimal?(currencyInfo3.CuryConvBase(valueOrDefault1));
      pmTran1.ProjectCuryAmount = nullable1;
      PMTran pmTran2 = tran;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo4 = currencyInfo1;
      tranCuryAmount = tran.TranCuryAmount;
      Decimal valueOrDefault2 = tranCuryAmount.GetValueOrDefault();
      Decimal? nullable2 = new Decimal?(currencyInfo4.CuryConvBase(valueOrDefault2));
      pmTran2.Amount = nullable2;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMTran, PMTran.offsetAccountID> e)
  {
    RegisterEntry.UpdateOffsetAccountId((PXGraph) this, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.offsetAccountID>>) e).Cache, e.Row);
  }

  public virtual PMTran CreateFromTemplate(
    PMAllocationDetail step,
    PMTran original,
    bool isWipStep)
  {
    PMTran fromTemplate = new PMTran();
    fromTemplate.BranchID = step.TargetBranchID ?? original.BranchID;
    fromTemplate.UOM = original.UOM;
    fromTemplate.BAccountID = original.BAccountID;
    fromTemplate.Billable = original.Billable;
    fromTemplate.BillableQty = original.BillableQty;
    fromTemplate.InventoryID = original.InventoryID;
    fromTemplate.LocationID = original.LocationID;
    fromTemplate.ResourceID = original.ResourceID;
    fromTemplate.CostCodeID = original.CostCodeID;
    fromTemplate.AllocationID = step.AllocationID;
    fromTemplate.TranCuryID = original.TranCuryID;
    fromTemplate.BaseCuryInfoID = original.BaseCuryInfoID;
    fromTemplate.ProjectCuryInfoID = original.ProjectCuryInfoID;
    if (!isWipStep)
    {
      fromTemplate.OrigAccountGroupID = original.AccountGroupID;
      fromTemplate.OrigProjectID = original.ProjectID;
      fromTemplate.OrigTaskID = original.TaskID;
    }
    PMTran pmTran = fromTemplate;
    bool? updateGl = step.UpdateGL;
    bool flag = false;
    bool? nullable = new bool?(updateGl.GetValueOrDefault() == flag & updateGl.HasValue);
    pmTran.IsNonGL = nullable;
    if (step.DateSource == "T")
    {
      fromTemplate.Date = original.Date;
      fromTemplate.FinPeriodID = !PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>() ? original.FinPeriodID : this.FinPeriodRepository.GetFinPeriodByBranchAndMasterPeriodID(fromTemplate.BranchID, original.TranPeriodID);
    }
    else if (this.PostingDate.HasValue)
    {
      fromTemplate.Date = this.PostingDate;
      fromTemplate.FinPeriodID = (string) null;
    }
    fromTemplate.StartDate = original.StartDate;
    fromTemplate.EndDate = original.EndDate;
    fromTemplate.OrigRefID = original.OrigRefID;
    fromTemplate.UseBillableQty = new bool?(true);
    fromTemplate.Reverse = step.Reverse;
    return fromTemplate;
  }

  public virtual void CalculateFormulas(
    PMAllocator.PMDataNavigator navigator,
    PMAllocationDetail step,
    PMTran tran,
    out Decimal? qty,
    out Decimal? billableQty,
    out Decimal? amt,
    out string description)
  {
    qty = new Decimal?();
    billableQty = new Decimal?();
    amt = new Decimal?();
    description = (string) null;
    if (!string.IsNullOrEmpty(step.QtyFormula))
    {
      try
      {
        ExpressionNode expressionNode = PMExpressionParser.Parse((IRateTable) this, step.QtyFormula);
        expressionNode.Bind((object) navigator);
        object obj = expressionNode.Eval((object) tran);
        if (obj != null)
          qty = new Decimal?(Convert.ToDecimal(obj));
      }
      catch (Exception ex)
      {
        throw new PXException("Failed to calculate Quantity formula in the allocation rule:{0}, Step{1}. Formula:{2} Error:{3}", new object[4]
        {
          (object) step.AllocationID,
          (object) step.StepID,
          (object) step.QtyFormula,
          (object) ex.Message
        });
      }
    }
    else
      qty = tran.Qty;
    if (!string.IsNullOrEmpty(step.BillableQtyFormula))
    {
      try
      {
        ExpressionNode expressionNode = PMExpressionParser.Parse((IRateTable) this, step.BillableQtyFormula);
        expressionNode.Bind((object) navigator);
        object obj = expressionNode.Eval((object) tran);
        if (obj != null)
          billableQty = new Decimal?(Convert.ToDecimal(obj));
      }
      catch (Exception ex)
      {
        throw new PXException("Failed to calculate Billable Quantity formula in the allocation rule:{0}, Step{1}. Formula:{2} Error:{3}", new object[4]
        {
          (object) step.AllocationID,
          (object) step.StepID,
          (object) step.BillableQtyFormula,
          (object) ex.Message
        });
      }
    }
    else
      billableQty = tran.BillableQty;
    if (!string.IsNullOrEmpty(step.AmountFormula))
    {
      try
      {
        ExpressionNode expressionNode = PMExpressionParser.Parse((IRateTable) this, step.AmountFormula);
        expressionNode.Bind((object) navigator);
        object obj = expressionNode.Eval((object) tran);
        if (obj != null)
          amt = new Decimal?(Convert.ToDecimal(obj));
      }
      catch (Exception ex)
      {
        throw new PXException("Failed to calculate Amount formula in the allocation rule:{0}, Step{1}. Formula:{2} Error:{3}", new object[4]
        {
          (object) step.AllocationID,
          (object) step.StepID,
          (object) step.AmountFormula,
          (object) ex.Message
        });
      }
    }
    else
      amt = tran.TranCuryAmount;
    if (!string.IsNullOrEmpty(step.DescriptionFormula))
    {
      try
      {
        ExpressionNode expressionNode = PMExpressionParser.Parse((IRateTable) this, step.DescriptionFormula);
        expressionNode.Bind((object) navigator);
        object obj = expressionNode.Eval((object) tran);
        if (obj == null)
          return;
        description = obj.ToString();
      }
      catch (Exception ex)
      {
        throw new PXException("Failed to calculate Description formula in the allocation rule:{0}, Step{1}. Formula:{2} Error:{3}", new object[4]
        {
          (object) step.AllocationID,
          (object) step.StepID,
          (object) step.DescriptionFormula,
          (object) ex.Message
        });
      }
    }
    else
      description = tran.Description;
  }

  /// <summary>
  /// Selects Source Transactions for the given step.
  /// Method relies on pre-selected data stored in allocationInfo, accountGroups and transactions collections.
  /// Method do not query database.
  /// </summary>
  public virtual List<PMTran> Select(PMAllocationDetail step, int? projectID, int? taskID)
  {
    if (step.Post.GetValueOrDefault())
    {
      Dictionary<int, List<PMAllocator.PMTranWithTrace>> stepResults1 = this.stepResults;
      int? stepId = step.StepID;
      int key1 = stepId.Value;
      if (stepResults1.ContainsKey(key1))
      {
        Dictionary<int, List<PMAllocator.PMTranWithTrace>> stepResults2 = this.stepResults;
        stepId = step.StepID;
        int key2 = stepId.Value;
        List<PMTran> pmTranList = new List<PMTran>(stepResults2[key2].Count);
        Dictionary<int, List<PMAllocator.PMTranWithTrace>> stepResults3 = this.stepResults;
        stepId = step.StepID;
        int key3 = stepId.Value;
        foreach (PMAllocator.PMTranWithTrace pmTranWithTrace in stepResults3[key3])
          pmTranList.Add(pmTranWithTrace.Tran);
        return pmTranList;
      }
    }
    List<PMTran> pmTranList1 = new List<PMTran>();
    if (step.SelectOption == "S")
    {
      foreach (PMAllocationDetail step1 in (IEnumerable<PMAllocationDetail>) this.GetSteps(step.AllocationID, step.RangeStart, step.RangeEnd))
        pmTranList1.AddRange((IEnumerable<PMTran>) this.Select(step1, projectID, taskID));
    }
    else
    {
      foreach (PMTran pmTran in this.GetTranByStep(step, projectID, taskID))
        pmTranList1.Add(pmTran);
    }
    return pmTranList1;
  }

  /// <summary>
  /// Selects Source Transactions for the given inner step (step with From/To AccountGroups).
  /// Method relies on pre-selected data stored in allocationInfo, accountGroups and transactions collections.
  /// Method do not query database.
  /// </summary>
  public virtual List<PMTran> GetTranByStep(PMAllocationDetail step, int? projectID, int? taskID)
  {
    List<PMTran> tranByStep = new List<PMTran>();
    foreach (int key in (IEnumerable<int>) this.GetAccountGroupsRange(step))
    {
      Dictionary<int, List<PXResult<PMTran>>> dictionary;
      List<PXResult<PMTran>> pxResultList;
      if (this.transactions.TryGetValue(taskID.Value, out dictionary) && dictionary.TryGetValue(key, out pxResultList))
      {
        foreach (PXResult<PMTran> pxResult in pxResultList)
        {
          PMTran pmTran = PXResult<PMTran>.op_Implicit(pxResult);
          int? sourceBranchId = step.SourceBranchID;
          if (sourceBranchId.HasValue)
          {
            sourceBranchId = step.SourceBranchID;
            int? branchId = pmTran.BranchID;
            if (!(sourceBranchId.GetValueOrDefault() == branchId.GetValueOrDefault() & sourceBranchId.HasValue == branchId.HasValue))
              continue;
          }
          tranByStep.Add(pmTran);
        }
      }
    }
    return tranByStep;
  }

  public virtual PXResultset<PMTran> GetTranFromDatabase(int? projectID, int? taskID, int groupID)
  {
    PXSelectBase<PMTran> pxSelectBase = (PXSelectBase<PMTran>) new PXSelectReadonly<PMTran, Where<PMTran.allocated, Equal<False>, And<PMTran.excludedFromAllocation, Equal<False>, And<PMTran.released, Equal<True>, And<PMTran.accountGroupID, Equal<Required<PMTran.accountGroupID>>, And<PMTran.projectID, Equal<Required<PMTran.projectID>>, And<PMTran.taskID, Equal<Required<PMTran.taskID>>>>>>>>>((PXGraph) this);
    PXResultset<PMTran> tranFromDatabase;
    if (this.FilterStartDate.HasValue && this.FilterEndDate.HasValue)
    {
      DateTime? filterStartDate = this.FilterStartDate;
      DateTime? filterEndDate = this.FilterEndDate;
      if ((filterStartDate.HasValue == filterEndDate.HasValue ? (filterStartDate.HasValue ? (filterStartDate.GetValueOrDefault() == filterEndDate.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      {
        pxSelectBase.WhereAnd<Where<PMTran.date, Equal<Required<PMTran.date>>>>();
        tranFromDatabase = pxSelectBase.Select(new object[4]
        {
          (object) groupID,
          (object) projectID,
          (object) taskID,
          (object) this.FilterStartDate
        });
      }
      else
      {
        pxSelectBase.WhereAnd<Where<PMTran.date, Between<Required<PMTran.date>, Required<PMTran.date>>>>();
        tranFromDatabase = pxSelectBase.Select(new object[5]
        {
          (object) groupID,
          (object) projectID,
          (object) taskID,
          (object) this.FilterStartDate,
          (object) this.FilterEndDate
        });
      }
    }
    else if (this.FilterStartDate.HasValue)
    {
      pxSelectBase.WhereAnd<Where<PMTran.date, GreaterEqual<Required<PMTran.date>>>>();
      tranFromDatabase = pxSelectBase.Select(new object[4]
      {
        (object) groupID,
        (object) projectID,
        (object) taskID,
        (object) this.FilterStartDate
      });
    }
    else if (this.FilterEndDate.HasValue)
    {
      pxSelectBase.WhereAnd<Where<PMTran.date, LessEqual<Required<PMTran.date>>>>();
      tranFromDatabase = pxSelectBase.Select(new object[4]
      {
        (object) groupID,
        (object) projectID,
        (object) taskID,
        (object) this.FilterEndDate
      });
    }
    else
      tranFromDatabase = pxSelectBase.Select(new object[3]
      {
        (object) groupID,
        (object) projectID,
        (object) taskID
      });
    return tranFromDatabase;
  }

  public virtual PXResultset<PMTran> GetTranFromDatabase(int? projectID, int groupID)
  {
    PXSelectBase<PMTran> pxSelectBase = (PXSelectBase<PMTran>) new PXSelectReadonly<PMTran, Where<PMTran.allocated, Equal<False>, And<PMTran.excludedFromAllocation, Equal<False>, And<PMTran.released, Equal<True>, And<PMTran.accountGroupID, Equal<Required<PMTran.accountGroupID>>, And<PMTran.projectID, Equal<Required<PMTran.projectID>>>>>>>>((PXGraph) this);
    PXResultset<PMTran> tranFromDatabase;
    if (this.FilterStartDate.HasValue && this.FilterEndDate.HasValue)
    {
      DateTime? filterStartDate = this.FilterStartDate;
      DateTime? filterEndDate = this.FilterEndDate;
      if ((filterStartDate.HasValue == filterEndDate.HasValue ? (filterStartDate.HasValue ? (filterStartDate.GetValueOrDefault() == filterEndDate.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      {
        pxSelectBase.WhereAnd<Where<PMTran.date, Equal<Required<PMTran.date>>>>();
        tranFromDatabase = pxSelectBase.Select(new object[3]
        {
          (object) groupID,
          (object) projectID,
          (object) this.FilterStartDate
        });
      }
      else
      {
        pxSelectBase.WhereAnd<Where<PMTran.date, Between<Required<PMTran.date>, Required<PMTran.date>>>>();
        tranFromDatabase = pxSelectBase.Select(new object[4]
        {
          (object) groupID,
          (object) projectID,
          (object) this.FilterStartDate,
          (object) this.FilterEndDate
        });
      }
    }
    else if (this.FilterStartDate.HasValue)
    {
      pxSelectBase.WhereAnd<Where<PMTran.date, GreaterEqual<Required<PMTran.date>>>>();
      tranFromDatabase = pxSelectBase.Select(new object[3]
      {
        (object) groupID,
        (object) projectID,
        (object) this.FilterStartDate
      });
    }
    else if (this.FilterEndDate.HasValue)
    {
      pxSelectBase.WhereAnd<Where<PMTran.date, LessEqual<Required<PMTran.date>>>>();
      tranFromDatabase = pxSelectBase.Select(new object[3]
      {
        (object) groupID,
        (object) projectID,
        (object) this.FilterEndDate
      });
    }
    else
      tranFromDatabase = pxSelectBase.Select(new object[2]
      {
        (object) groupID,
        (object) projectID
      });
    return tranFromDatabase;
  }

  public virtual PMAllocationSourceTran CreateAllocationTran(
    string allocationID,
    int? stepID,
    PMTran tran)
  {
    return new PMAllocationSourceTran()
    {
      AllocationID = allocationID,
      StepID = stepID,
      TranID = tran.TranID,
      Qty = tran.Qty,
      Rate = tran.Rate,
      Amount = tran.ProjectCuryAmount
    };
  }

  public virtual Decimal? GetRate(PMAllocationDetail step, PMTran tran, string rateTableID)
  {
    if (string.IsNullOrEmpty(step.RateTypeID))
    {
      switch (step.NoRateOption)
      {
        case "0":
          return new Decimal?(0M);
        case "E":
          throw new PXException("Rate Type is not defined for step {0}", new object[1]
          {
            (object) step.StepID
          });
        case "N":
          return new Decimal?();
        default:
          return new Decimal?((Decimal) 1);
      }
    }
    else
    {
      Decimal? rate = new Decimal?();
      string str = (string) null;
      if (!string.IsNullOrEmpty(rateTableID))
      {
        if (this.rateEngine != null)
        {
          rate = this.rateEngine.GetRate(rateTableID, step.RateTypeID, tran);
          str = this.rateEngine.GetTrace(tran);
        }
        else
        {
          RateEngine rateEngine = this.CreateRateEngine(step.RateTypeID, tran);
          rate = rateEngine.GetRate(rateTableID);
          str = rateEngine.GetTrace();
        }
      }
      if (rate.HasValue)
        return rate;
      switch (step.NoRateOption)
      {
        case "0":
          return new Decimal?(0M);
        case "E":
          PXTrace.WriteInformation(str);
          PXTrace.WriteError("The @Rate is not defined for the {1} step of the {0} allocation rule. Check Trace for details.", new object[2]
          {
            (object) step.AllocationID,
            (object) step.StepID
          });
          throw new PXException("The @Rate is not defined for the {1} step of the {0} allocation rule. Check Trace for details.", new object[2]
          {
            (object) step.AllocationID,
            (object) step.StepID
          });
        case "N":
          PXTrace.WriteInformation(str);
          return new Decimal?();
        default:
          return new Decimal?((Decimal) 1);
      }
    }
  }

  public Decimal? ConvertAmountToCurrency(
    string fromCuryID,
    string toCuryID,
    string rateType,
    DateTime? effectiveDate,
    Decimal? value)
  {
    if (string.IsNullOrEmpty(fromCuryID))
      throw new ArgumentNullException(nameof (fromCuryID), "From CuryID is null or an empty string.");
    if (string.IsNullOrEmpty(toCuryID))
      throw new ArgumentNullException(nameof (toCuryID), "To CuryID is null or an empty string.");
    if (string.IsNullOrEmpty(rateType))
      throw new ArgumentNullException(nameof (rateType), "RateType is null or an empty string.");
    if (!effectiveDate.HasValue)
      throw new ArgumentNullException(nameof (effectiveDate), "Effective Date is required.");
    if (!value.HasValue)
      return new Decimal?();
    if (value.Value == 0M)
      return new Decimal?(0M);
    if (string.Equals(fromCuryID, toCuryID, StringComparison.InvariantCultureIgnoreCase))
      return new Decimal?(value.Value);
    IPXCurrencyService pxCurrencyService = ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()((PXGraph) this);
    return new Decimal?(PMCommitmentAttribute.CuryConvCury(pxCurrencyService.GetRate(fromCuryID, toCuryID, rateType, effectiveDate) ?? throw new PXException("Please define a conversion rate from the {0} to {1} currency within the {2} currency rate type and the {3:d} effective date on the Currency Rates (CM301000) form.", new object[4]
    {
      (object) fromCuryID,
      (object) toCuryID,
      (object) rateType,
      (object) effectiveDate
    }), value.GetValueOrDefault(), new int?(pxCurrencyService.CuryDecimalPlaces(toCuryID))));
  }

  public virtual RateEngine CreateRateEngine(string rateTypeID, PMTran tran)
  {
    return new RateEngine((PXGraph) this, rateTypeID, tran);
  }

  public virtual Decimal ConvertQtyToBase(int? inventoryID, string UOM, Decimal qty)
  {
    try
    {
      return INUnitAttribute.ConvertToBase(((PXSelectBase) this.Transactions).Cache, inventoryID, UOM, qty, INPrecision.QUANTITY);
    }
    catch (PXException ex)
    {
      PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) inventoryID
      }));
      if (inventoryItem != null)
        PXTrace.WriteError("Failed to convert the Inventory Item '{0}' FROM '{1}' TO '{2}'. Error: {3}", new object[4]
        {
          (object) inventoryItem.InventoryCD,
          (object) UOM,
          (object) inventoryItem.BaseUnit,
          (object) ((Exception) ex).Message
        });
      throw;
    }
  }

  public virtual void Clear()
  {
    ((PXGraph) this).Clear();
    this.MultiCurrencyService.Clear();
  }

  public class MultiCurrency : PMTranMultiCurrencyPM<PMAllocator>
  {
    protected override CurySource CurrentSourceSelect() => new CurySource();

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[1]
      {
        (PXSelectBase) this.Base.Transactions
      };
    }

    protected override void DocumentRowInserting<CuryInfoID, CuryID>(PXCache sender, object row)
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.GetCurrencyInfo((long?) sender.GetValue<CuryInfoID>(row));
      if (currencyInfo == null)
        base.DocumentRowInserting<CuryInfoID, CuryID>(sender, row);
      else
        sender.SetValue<CuryID>(row, (object) currencyInfo.CuryID);
    }
  }

  public class AllocData
  {
    public int AccountGroupID { get; private set; }

    public int InventoryID { get; private set; }

    public int CostCodeID { get; private set; }

    public Decimal Amount { get; set; }

    public Decimal Quantity { get; set; }

    public string UOM { get; set; }

    public AllocData(int accountGroupID, int inventoryID, int costCodeID)
    {
      this.AccountGroupID = accountGroupID;
      this.InventoryID = inventoryID;
      this.CostCodeID = costCodeID;
    }
  }

  public class PMBudgetStat
  {
    public int AccountGroupID { get; private set; }

    public int InventoryID { get; private set; }

    public int CostCodeID { get; private set; }

    public Decimal? Amount { get; set; }

    public Decimal? Quantity { get; set; }

    public PMBudgetStat(int accountGroupID, int inventoryID, int costCodeID)
    {
      this.AccountGroupID = accountGroupID;
      this.InventoryID = inventoryID;
      this.CostCodeID = costCodeID;
    }
  }

  [Serializable]
  public class AllocatedService
  {
    private Dictionary<string, PMAllocator.PMBudgetStat> list = new Dictionary<string, PMAllocator.PMBudgetStat>();

    public AllocatedService(PXGraph graph, PMTask task)
    {
      foreach (PXResult<PMAllocator.AllocatedService.PMTaskAllocTotalEx> pxResult in ((PXSelectBase<PMAllocator.AllocatedService.PMTaskAllocTotalEx>) new PXSelectReadonly<PMAllocator.AllocatedService.PMTaskAllocTotalEx, Where<PMAllocator.AllocatedService.PMTaskAllocTotalEx.projectID, Equal<Required<PMAllocator.AllocatedService.PMTaskAllocTotalEx.projectID>>, And<PMAllocator.AllocatedService.PMTaskAllocTotalEx.taskID, Equal<Required<PMAllocator.AllocatedService.PMTaskAllocTotalEx.taskID>>>>>(graph)).Select(new object[2]
      {
        (object) task.ProjectID,
        (object) task.TaskID
      }))
      {
        PMAllocator.AllocatedService.PMTaskAllocTotalEx taskAllocTotalEx = PXResult<PMAllocator.AllocatedService.PMTaskAllocTotalEx>.op_Implicit(pxResult);
        int? nullable = taskAllocTotalEx.AccountGroupID;
        int accountGroupID = nullable.Value;
        nullable = taskAllocTotalEx.InventoryID;
        int inventoryID = nullable.Value;
        nullable = taskAllocTotalEx.CostCodeID;
        int costCodeID = nullable.Value;
        this.list.Add(PMAllocator.AllocatedService.GetKey(taskAllocTotalEx.AccountGroupID.Value, taskAllocTotalEx.InventoryID.Value, taskAllocTotalEx.CostCodeID.Value), new PMAllocator.PMBudgetStat(accountGroupID, inventoryID, costCodeID)
        {
          Amount = taskAllocTotalEx.Amount,
          Quantity = taskAllocTotalEx.Quantity
        });
      }
      foreach (PMTaskAllocTotalAccum taskAllocTotalAccum in graph.Caches[typeof (PMTaskAllocTotalAccum)].Inserted)
      {
        int? nullable1 = taskAllocTotalAccum.AccountGroupID;
        int accountGroupID = nullable1.Value;
        nullable1 = taskAllocTotalAccum.InventoryID;
        int inventoryID = nullable1.Value;
        nullable1 = taskAllocTotalAccum.CostCodeID;
        int costCodeID = nullable1.Value;
        string key = PMAllocator.AllocatedService.GetKey(accountGroupID, inventoryID, costCodeID);
        if (this.list.ContainsKey(key))
        {
          nullable1 = taskAllocTotalAccum.TaskID;
          int? taskId = task.TaskID;
          if (nullable1.GetValueOrDefault() == taskId.GetValueOrDefault() & nullable1.HasValue == taskId.HasValue)
          {
            PMAllocator.PMBudgetStat pmBudgetStat1 = this.list[key];
            Decimal? nullable2 = pmBudgetStat1.Amount;
            Decimal? nullable3 = taskAllocTotalAccum.Amount;
            pmBudgetStat1.Amount = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
            PMAllocator.PMBudgetStat pmBudgetStat2 = this.list[key];
            nullable3 = pmBudgetStat2.Quantity;
            nullable2 = taskAllocTotalAccum.Quantity;
            pmBudgetStat2.Quantity = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          }
        }
      }
    }

    public Decimal GetAllocatedAmt(int accountGroupID, int inventoryID, int costCodeID)
    {
      PMAllocator.PMBudgetStat pmBudgetStat;
      return this.list.TryGetValue(PMAllocator.AllocatedService.GetKey(accountGroupID, inventoryID, costCodeID), out pmBudgetStat) ? pmBudgetStat.Amount.GetValueOrDefault() : 0M;
    }

    public Decimal GetAllocatedAmt(int accountGroupID)
    {
      Decimal allocatedAmt = 0M;
      foreach (PMAllocator.PMBudgetStat pmBudgetStat in this.list.Values)
      {
        if (pmBudgetStat.AccountGroupID == accountGroupID)
          allocatedAmt += pmBudgetStat.Amount.GetValueOrDefault();
      }
      return allocatedAmt;
    }

    public Decimal GetAllocatedQty(int accountGroupID, int inventoryID, int costCodeID)
    {
      PMAllocator.PMBudgetStat pmBudgetStat;
      return this.list.TryGetValue(PMAllocator.AllocatedService.GetKey(accountGroupID, inventoryID, costCodeID), out pmBudgetStat) ? pmBudgetStat.Quantity.GetValueOrDefault() : 0M;
    }

    private static string GetKey(int accountGroupID, int inventoryID, int costCodeID)
    {
      return $"{accountGroupID}.{inventoryID}.{costCodeID}";
    }

    [PXHidden]
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class PMTaskAllocTotalEx : PMTaskAllocTotal
    {
      public new abstract class projectID : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Field<
        #nullable disable
        PMAllocator.AllocatedService.PMTaskAllocTotalEx.projectID>
      {
      }

      public new abstract class taskID : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Field<
        #nullable disable
        PMAllocator.AllocatedService.PMTaskAllocTotalEx.taskID>
      {
      }

      public new abstract class accountGroupID : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Field<
        #nullable disable
        PMAllocator.AllocatedService.PMTaskAllocTotalEx.accountGroupID>
      {
      }

      public new abstract class inventoryID : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Field<
        #nullable disable
        PMAllocator.AllocatedService.PMTaskAllocTotalEx.inventoryID>
      {
      }

      public new abstract class costCodeID : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Field<
        #nullable disable
        PMAllocator.AllocatedService.PMTaskAllocTotalEx.costCodeID>
      {
      }
    }
  }

  public class PMTranWithTrace
  {
    public PMTran Tran;
    public Guid[] Files;
    public string NoteText;
    public List<long> OriginalTrans = new List<long>();

    public PMTranWithTrace(PMTran tran, long? originalTran)
    {
      this.Tran = tran;
      this.OriginalTrans.Add(originalTran.Value);
    }

    public PMTranWithTrace(PMTran tran, List<long> list)
    {
      this.Tran = tran;
      this.OriginalTrans.AddRange((IEnumerable<long>) list);
    }
  }

  public class PMDataNavigator : IDataNavigator, ICloneable
  {
    protected List<PMTran> list;
    protected IRateTable engine;

    public PMDataNavigator(IRateTable engine, List<PMTran> list)
    {
      this.engine = engine;
      this.list = list;
    }

    public void Clear()
    {
    }

    public void Refresh()
    {
    }

    public object Current => throw new NotImplementedException();

    public IDataNavigator GetChildNavigator(object record) => (IDataNavigator) null;

    public object GetItem(object dataItem, string dataField) => throw new NotImplementedException();

    public IList GetList() => (IList) this.list;

    public object GetValue(object dataItem, string dataField, ref string format, bool valueOnly = false)
    {
      PMNameNode pmNameNode = new PMNameNode((ExpressionNode) null, dataField, (ParserContext) null);
      return pmNameNode.IsAttribute ? this.engine.Evaluate(pmNameNode.ObjectName, (string) null, pmNameNode.FieldName, (PMTran) dataItem) : this.engine.Evaluate(pmNameNode.ObjectName, pmNameNode.FieldName, (string) null, (PMTran) dataItem);
    }

    public bool MoveNext() => throw new NotImplementedException();

    public void Reset() => throw new NotImplementedException();

    public ReportSelectArguments SelectArguments => throw new NotImplementedException();

    public object this[string dataField] => throw new NotImplementedException();

    public string CurrentlyProcessingParam
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    public int[] GetFieldSegments(string field) => throw new NotImplementedException();

    public object Clone()
    {
      return (object) new PMAllocator.PMDataNavigator(this.engine, this.list == null ? (List<PMTran>) null : new List<PMTran>((IEnumerable<PMTran>) this.list));
    }
  }

  public class AllocationInfo
  {
    public HashSet<int> AccountGroups { get; private set; }

    public HashSet<string> RateTypes { get; private set; }

    public List<PMAllocationDetail> Steps { get; private set; }

    public bool ContainsBudgetStep { get; set; }

    public AllocationInfo(
      HashSet<int> accountGroups,
      HashSet<string> rateTypes,
      List<PMAllocationDetail> steps)
    {
      this.AccountGroups = accountGroups;
      this.RateTypes = rateTypes;
      this.Steps = steps;
    }
  }

  public class AccountGroup
  {
    public int GroupID { get; private set; }

    public string GroupCD { get; private set; }

    public AccountGroup(int id, string cd)
    {
      this.GroupID = id;
      this.GroupCD = cd;
    }
  }
}

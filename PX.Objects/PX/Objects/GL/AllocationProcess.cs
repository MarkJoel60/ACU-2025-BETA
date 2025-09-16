// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AllocationProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.BQLConstants;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL.Constants;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.GL;

[TableAndChartDashboardType]
[Serializable]
public class AllocationProcess : PXGraph<
#nullable disable
AllocationProcess>
{
  public PXFilter<AllocationProcess.AllocationFilter> Filter;
  public PXCancel<AllocationProcess.AllocationFilter> Cancel;
  public PXAction<AllocationProcess.AllocationFilter> EditDetail;
  private string _subMask = "";
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<AllocationProcess.AllocationExt, AllocationProcess.AllocationFilter> Allocations;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  public PXAction<AllocationProcess.AllocationFilter> ViewBatch;
  protected static Decimal MinAmountToDistribute = 0.01M;
  private const Decimal epsilon = 0.0001M;

  public AllocationProcess()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AllocationProcess.\u003C\u003Ec__DisplayClass4_0 cDisplayClass40 = new AllocationProcess.\u003C\u003Ec__DisplayClass4_0();
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass40.filter = ((PXSelectBase<AllocationProcess.AllocationFilter>) this.Filter).Current;
    // ISSUE: method pointer
    ((PXProcessingBase<AllocationProcess.AllocationExt>) this.Allocations).SetProcessDelegate<AllocationProcess>(new PXProcessingBase<AllocationProcess.AllocationExt>.ProcessItemDelegate<AllocationProcess>((object) cDisplayClass40, __methodptr(\u003C\u002Ector\u003Eb__0)));
    OpenPeriodAttribute.SetValidatePeriod<AllocationProcess.AllocationFilter.finPeriodID>(((PXSelectBase) this.Filter).Cache, (object) null, ((PXGraph) this).IsContractBasedAPI || ((PXGraph) this).IsImport || ((PXGraph) this).IsExport || ((PXGraph) this).UnattendedMode ? PeriodValidation.DefaultUpdate : PeriodValidation.DefaultSelectUpdate);
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<FeaturesSet.branch>>.Or<FeatureInstalled<FeaturesSet.multiCompany>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<AllocationProcess.AllocationExt.branchID> e)
  {
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable editDetail(PXAdapter adapter)
  {
    if (((PXSelectBase<AllocationProcess.AllocationExt>) this.Allocations).Current != null)
      PXRedirectHelper.TryRedirect(((PXSelectBase) this.Allocations).Cache, (object) ((PXSelectBase<AllocationProcess.AllocationExt>) this.Allocations).Current, "Allocation", (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewBatch(PXAdapter adapter)
  {
    if (((PXSelectBase<AllocationProcess.AllocationExt>) this.Allocations).Current == null)
      return adapter.Get();
    Batch batch = PXResultset<Batch>.op_Implicit(PXSelectBase<Batch, PXSelect<Batch, Where<Batch.module, Equal<Current<AllocationProcess.AllocationExt.module>>, And<Batch.batchNbr, Equal<Current<AllocationProcess.AllocationExt.batchNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<AllocationProcess.AllocationExt>) this.Allocations).Current
    }, Array.Empty<object>()));
    if (batch == null)
      throw new PXException("Last batch for the selected allocation cannot be found.");
    JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
    ((PXSelectBase<Batch>) instance.BatchModule).Current = batch;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Batch");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  protected virtual IEnumerable allocations()
  {
    AllocationProcess allocationProcess = this;
    AllocationProcess.AllocationFilter filter = ((PXSelectBase<AllocationProcess.AllocationFilter>) allocationProcess.Filter).Current;
    PXSelectBase<AllocationProcess.AllocationExt> pxSelectBase = (PXSelectBase<AllocationProcess.AllocationExt>) new PXSelect<AllocationProcess.AllocationExt, Where<GLAllocation.active, Equal<BitOn>>, OrderBy<Asc<GLAllocation.sortOrder>>>((PXGraph) allocationProcess);
    PXSelectBase<Batch> selBatch = (PXSelectBase<Batch>) new PXSelectJoin<Batch, InnerJoin<GLAllocationHistory, On<Batch.batchNbr, Equal<GLAllocationHistory.batchNbr>, And<Batch.module, Equal<GLAllocationHistory.module>>>>, Where<GLAllocationHistory.gLAllocationID, Equal<Required<GLAllocationHistory.gLAllocationID>>>, OrderBy<Desc<Batch.tranPeriodID, Desc<Batch.batchNbr>>>>((PXGraph) allocationProcess);
    PXSelectBase<Batch> selBatchInPeriod = (PXSelectBase<Batch>) new PXSelectJoin<Batch, InnerJoin<GLAllocationHistory, On<Batch.batchNbr, Equal<GLAllocationHistory.batchNbr>, And<Batch.module, Equal<GLAllocationHistory.module>>>>, Where<GLAllocationHistory.gLAllocationID, Equal<Required<GLAllocationHistory.gLAllocationID>>, And<Batch.tranPeriodID, Equal<Required<Batch.tranPeriodID>>>>, OrderBy<Desc<Batch.tranPeriodID, Desc<Batch.batchNbr>>>>((PXGraph) allocationProcess);
    foreach (PXResult<AllocationProcess.AllocationExt> pxResult in pxSelectBase.Select(Array.Empty<object>()))
    {
      AllocationProcess.AllocationExt allocationExt = PXResult<AllocationProcess.AllocationExt>.op_Implicit(pxResult);
      if (allocationProcess.isApplicable((GLAllocation) allocationExt, filter.FinPeriodID))
      {
        Batch aBatch;
        if (allocationExt.AllocCollectMethod == "P")
          aBatch = PXResultset<Batch>.op_Implicit(selBatchInPeriod.SelectWindowed(0, 1, new object[2]
          {
            (object) allocationExt.GLAllocationID,
            (object) filter.FinPeriodID
          }));
        else
          aBatch = PXResultset<Batch>.op_Implicit(selBatch.SelectWindowed(0, 1, new object[1]
          {
            (object) allocationExt.GLAllocationID
          }));
        if (aBatch != null)
        {
          if (AllocationProcess.ComparePeriods(aBatch.TranPeriodID, ((PXSelectBase<AllocationProcess.AllocationFilter>) allocationProcess.Filter).Current.FinPeriodID, false) <= 0)
            AllocationProcess.AllocationExt.CopyFrom(allocationExt, aBatch);
          else
            continue;
        }
        yield return (object) allocationExt;
      }
    }
    ((PXSelectBase) allocationProcess.Allocations).Cache.IsDirty = false;
  }

  protected virtual void ProcessAllocation(
    GLAllocation allocation,
    AllocationProcess.AllocationFilter filter)
  {
    this.ProcessAllocation(allocation, filter.FinPeriodID, filter.DateEntered);
  }

  protected virtual void ProcessAllocation(
    GLAllocation allocation,
    string masterPostPeriod,
    DateTime? aDate)
  {
    if (string.IsNullOrEmpty(masterPostPeriod) || !aDate.HasValue)
      throw new PXException("You must provide the Date and Financial Period for processing.");
    if (!this.isApplicable(allocation, masterPostPeriod))
      throw new PXException("This GL Allocation is not configured to be executed for the selected Financial Period.");
    string str1 = !this.isOutOfQueue(allocation, masterPostPeriod) ? masterPostPeriod : throw new PXException("This GL Allocation cannot be processed. There are GL Allocations with higher priority that must be executed first.");
    if (allocation.AllocCollectMethod == "V")
    {
      if (this.FindFutureAllocationApplication(allocation.GLAllocationID, masterPostPeriod) != null)
        throw new PXException("GL Allocation {0} was already executed after the selected Financial Period.", new object[1]
        {
          (object) allocation.GLAllocationID
        });
      str1 = this.FindLastAllocationPeriod(allocation.GLAllocationID, masterPostPeriod);
      string aPeriod2 = AllocationProcess.FirstPeriodOfYear(masterPostPeriod);
      string masterPeriodId = this.GetMasterPeriodID(allocation, allocation.StartFinPeriodID);
      if (allocation.Recurring.Value)
        aPeriod2 = AllocationProcess.ConvertToSameYear(masterPeriodId, masterPostPeriod);
      else if (!string.IsNullOrEmpty(masterPeriodId) && AllocationProcess.ComparePeriods(masterPeriodId, aPeriod2, false) > 0)
        aPeriod2 = masterPeriodId;
      if (string.IsNullOrEmpty(str1))
        str1 = aPeriod2;
      else if (AllocationProcess.ComparePeriods(str1, aPeriod2, false) < 0)
        str1 = aPeriod2;
    }
    if (this.HasUnpostedBatch(allocation, str1, masterPostPeriod))
      throw new PXException("There are un-posted GL Batches for this GL Allocation for the selected Financial Period. You must post these batches before executing this GL Allocation.");
    PXSelect<GLAllocationSource, Where<GLAllocationSource.gLAllocationID, Equal<Required<GLAllocationSource.gLAllocationID>>>> pxSelect = new PXSelect<GLAllocationSource, Where<GLAllocationSource.gLAllocationID, Equal<Required<GLAllocationSource.gLAllocationID>>>>((PXGraph) this);
    PXSelectBase<GLHistory> pxSelectBase = (PXSelectBase<GLHistory>) new PXSelectJoin<GLHistory, InnerJoin<Account, On<GLHistory.accountID, Equal<Account.accountID>, And<Account.active, Equal<True>>>, InnerJoin<Sub, On<GLHistory.subID, Equal<Sub.subID>>>>, Where<GLHistory.ledgerID, Equal<Required<GLHistory.ledgerID>>, And<GLHistory.branchID, Equal<Required<GLHistory.branchID>>, And<GLHistory.finPeriodID, GreaterEqual<Required<GLHistory.finPeriodID>>, And<GLHistory.finPeriodID, LessEqual<Required<GLHistory.finPeriodID>>, And<Account.accountCD, Like<Required<Account.accountCD>>, And<Sub.subCD, Like<Required<Sub.subCD>>>>>>>>>((PXGraph) this);
    JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
    Batch row1 = new Batch();
    row1.DateEntered = aDate;
    row1.BranchID = allocation.BranchID;
    FinPeriodIDAttribute.SetPeriodsByMaster<Batch.finPeriodID>(((PXSelectBase) instance.BatchModule).Cache, (object) row1, masterPostPeriod);
    row1.LedgerID = allocation.AllocLedgerID;
    row1.Module = "GL";
    row1.NumberCode = "GLALC";
    row1.BatchType = "A";
    row1.Description = string.IsNullOrEmpty(allocation.Descr) ? allocation.GLAllocationID : allocation.Descr;
    int? nullable1 = allocation.SourceLedgerID.HasValue ? allocation.SourceLedgerID : allocation.AllocLedgerID;
    Ledger ledger = this.FindLedger(allocation.AllocLedgerID);
    PX.Objects.CM.CurrencyInfo currencyInfo = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) instance.currencyinfo).Insert(new PX.Objects.CM.CurrencyInfo()
    {
      CuryID = ledger.BaseCuryID,
      BaseCuryID = ledger.BaseCuryID
    });
    row1.CuryInfoID = currencyInfo.CuryInfoID;
    Batch row2 = ((PXSelectBase<Batch>) instance.BatchModule).Insert(row1);
    Decimal totalCredit = 0M;
    Decimal totalDebit = 0M;
    Dictionary<AllocationProcess.BranchAccountSubKey, GLAllocationAccountHistory> dictionary1 = new Dictionary<AllocationProcess.BranchAccountSubKey, GLAllocationAccountHistory>();
    Dictionary<AllocationProcess.BranchAccountSubKey, int> dictionary2 = new Dictionary<AllocationProcess.BranchAccountSubKey, int>();
    Dictionary<int, Account> aAcctDefs = new Dictionary<int, Account>();
    object[] objArray = new object[1]
    {
      (object) allocation.GLAllocationID
    };
    foreach (PXResult<GLAllocationSource> pxResult1 in ((PXSelectBase<GLAllocationSource>) pxSelect).Select(objArray))
    {
      GLAllocationSource allocationSource = PXResult<GLAllocationSource>.op_Implicit(pxResult1);
      string subCdWildcard1 = SubCDUtils.CreateSubCDWildcard(allocationSource.AccountCD, "ACCOUNT");
      string subCdWildcard2 = SubCDUtils.CreateSubCDWildcard(allocationSource.SubCD, "SUBACCOUNT");
      int? contrAccountId = allocationSource.ContrAccountID;
      int? contrSubId = allocationSource.ContrSubID;
      if (contrAccountId.HasValue && !aAcctDefs.ContainsKey(contrAccountId.Value))
      {
        Account account = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Required<Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) contrAccountId
        }));
        aAcctDefs[contrAccountId.Value] = account == null || account.Active.GetValueOrDefault() ? account : throw new PXException("One or more of allocation source contains inactive contra accounts");
      }
      FinPeriod result1 = this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(allocationSource.BranchID), str1).Result;
      FinPeriod result2 = this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(allocationSource.BranchID), masterPostPeriod).Result;
      foreach (PXResult<GLHistory, Account, Sub> pxResult2 in pxSelectBase.Select(new object[6]
      {
        (object) nullable1,
        (object) allocationSource.BranchID,
        (object) result1.FinPeriodID,
        (object) result2.FinPeriodID,
        (object) subCdWildcard1,
        (object) subCdWildcard2
      }))
      {
        GLHistory aSrcAcctHistory = PXResult<GLHistory, Account, Sub>.op_Implicit(pxResult2);
        Account aAcct = PXResult<GLHistory, Account, Sub>.op_Implicit(pxResult2);
        Decimal num1 = 0M;
        if (AllocationProcess.IsDenominationMatch(aAcct, ledger))
        {
          Decimal num2 = (aSrcAcctHistory.FinPtdDebit ?? 0.0M) - (aSrcAcctHistory.FinPtdCredit ?? 0.0M);
          Decimal num3 = !contrAccountId.HasValue ? (AccountRules.IsCreditBalance(aAcct.Type) ? -num2 : num2) : (AccountRules.IsCreditBalance(aAcctDefs[contrAccountId.Value].Type) ? -num2 : num2);
          Decimal aAllocated;
          Decimal aAllocatedForPriorPeriods;
          this.FindAllocatedAmountDetailed(allocation.GLAllocationID, aSrcAcctHistory, out aAllocated, out aAllocatedForPriorPeriods);
          int? nullable2;
          if (contrAccountId.HasValue)
          {
            nullable2 = aSrcAcctHistory.AccountID;
            int? nullable3 = contrAccountId;
            if (!(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue))
              goto label_35;
          }
          if (contrSubId.HasValue)
          {
            int? subId = aSrcAcctHistory.SubID;
            int? nullable4 = contrSubId;
            if (!(subId.GetValueOrDefault() == nullable4.GetValueOrDefault() & subId.HasValue == nullable4.HasValue))
              goto label_35;
          }
          int num4;
          if (allocation.SourceLedgerID.HasValue)
          {
            int? sourceLedgerId = allocation.SourceLedgerID;
            int? allocLedgerId = allocation.AllocLedgerID;
            num4 = sourceLedgerId.GetValueOrDefault() == allocLedgerId.GetValueOrDefault() & sourceLedgerId.HasValue == allocLedgerId.HasValue ? 1 : 0;
            goto label_36;
          }
          num4 = 1;
          goto label_36;
label_35:
          num4 = 0;
label_36:
          bool flag = num4 != 0;
          Decimal? limitAmount = allocationSource.LimitAmount;
          Decimal num5 = 0M;
          Decimal val;
          if (!(limitAmount.GetValueOrDefault() == num5 & limitAmount.HasValue))
          {
            Decimal num6 = Math.Abs(allocationSource.LimitAmount.Value);
            Decimal num7 = AccountRules.IsCreditBalance(aAcct.Type) ? -num6 : num6;
            Decimal num8 = aAllocated - aAllocatedForPriorPeriods;
            Decimal num9 = flag ? num3 + aAllocated : num3;
            val = (num7 > 0M ? (num9 < num7 ? num9 : num7) : (num9 > num7 ? num9 : num7)) - num8;
          }
          else if (string.IsNullOrEmpty(allocationSource.PercentLimitType) || allocationSource.PercentLimitType == "P")
            val = (flag ? allocationSource.LimitPercent.Value * (num3 + aAllocated) / 100.00M : allocationSource.LimitPercent.Value * num3 / 100.00M) - (aAllocated - aAllocatedForPriorPeriods);
          else
            throw new PXException("Algorithm for the Percent Limit Type {0} is not implemented", new object[1]
            {
              (object) allocationSource.PercentLimitType
            });
          Decimal num10 = PXCurrencyAttribute.Round<Batch.curyInfoID>(((PXSelectBase) instance.BatchModule).Cache, (object) row2, val, CMPrecision.TRANCURY);
          if (aSrcAcctHistory.FinPeriodID != result2.FinPeriodID)
            num1 += num10;
          if (Math.Abs(num10) >= 0.0001M)
          {
            int aFirst = aSrcAcctHistory.BranchID.Value;
            int? nullable5 = aSrcAcctHistory.AccountID;
            int aSecond = nullable5.Value;
            nullable5 = aSrcAcctHistory.SubID;
            int aThird = nullable5.Value;
            AllocationProcess.BranchAccountSubKey key1 = new AllocationProcess.BranchAccountSubKey(aFirst, aSecond, aThird);
            if (dictionary2.ContainsKey(key1))
            {
              int num11 = dictionary2[key1];
              nullable5 = allocationSource.LineID;
              int num12 = nullable5.Value;
              if (num11 != num12)
                continue;
            }
            else
            {
              Dictionary<AllocationProcess.BranchAccountSubKey, int> dictionary3 = dictionary2;
              AllocationProcess.BranchAccountSubKey key2 = key1;
              nullable5 = allocationSource.LineID;
              int num13 = nullable5.Value;
              dictionary3[key2] = num13;
            }
            Dictionary<int, Account> dictionary4 = aAcctDefs;
            nullable5 = aAcct.AccountID;
            int key3 = nullable5.Value;
            if (!dictionary4.ContainsKey(key3))
            {
              Dictionary<int, Account> dictionary5 = aAcctDefs;
              nullable5 = aAcct.AccountID;
              int key4 = nullable5.Value;
              Account account = aAcct;
              dictionary5[key4] = account;
            }
            if (!dictionary1.ContainsKey(key1))
            {
              GLAllocationAccountHistory allocationAccountHistory1 = new GLAllocationAccountHistory();
              allocationAccountHistory1.BranchID = aSrcAcctHistory.BranchID;
              allocationAccountHistory1.AccountID = aSrcAcctHistory.AccountID;
              allocationAccountHistory1.SubID = aSrcAcctHistory.SubID;
              allocationAccountHistory1.ContrAccontID = contrAccountId;
              allocationAccountHistory1.ContrSubID = contrSubId;
              allocationAccountHistory1.AllocatedAmount = new Decimal?(num10);
              allocationAccountHistory1.PriorPeriodsAllocAmount = new Decimal?(num1);
              GLAllocationAccountHistory allocationAccountHistory2 = allocationAccountHistory1;
              nullable5 = nullable1;
              nullable2 = allocation.AllocLedgerID;
              int? nullable6;
              if (nullable5.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable5.HasValue == nullable2.HasValue)
              {
                nullable2 = new int?();
                nullable6 = nullable2;
              }
              else
                nullable6 = nullable1;
              allocationAccountHistory2.SourceLedgerID = nullable6;
              dictionary1.Add(key1, allocationAccountHistory1);
            }
            else
            {
              GLAllocationAccountHistory allocationAccountHistory = dictionary1[key1];
              Decimal? nullable7 = allocationAccountHistory.AllocatedAmount;
              Decimal num14 = num10;
              allocationAccountHistory.AllocatedAmount = nullable7.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + num14) : new Decimal?();
              nullable7 = allocationAccountHistory.PriorPeriodsAllocAmount;
              Decimal num15 = num1;
              allocationAccountHistory.PriorPeriodsAllocAmount = nullable7.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + num15) : new Decimal?();
            }
          }
        }
      }
    }
    AllocationProcess.AllocateAmount(instance, allocation, (IEnumerable<GLAllocationAccountHistory>) dictionary1.Values, aAcctDefs, ledger, ref totalDebit, ref totalCredit);
    Decimal num16 = Math.Abs(totalDebit - totalCredit);
    if ((!(totalDebit == 0M) ? 0 : (totalCredit == 0M ? 1 : 0)) != 0 || num16 != 0M && num16 < AllocationProcess.MinAmountToDistribute)
      return;
    if (allocation.AllocateSeparately.GetValueOrDefault())
    {
      this._subMask = ((PXGraph) this).Caches[typeof (Sub)].GetStateExt<Sub.subCD>((object) null) is PXStringState stateExt ? stateExt.InputMask : (string) null;
      List<string> source = new List<string>();
      foreach (GLAllocationAccountHistory srcAcc in dictionary1.Values)
      {
        List<string> notCreatedSubAccounts = new List<string>();
        AccountWeightedList aAccounts;
        switch (allocation.AllocMethod)
        {
          case "W":
          case "C":
            this.RetrievePredefinedDestribution(out aAccounts, allocation, srcAcc, out notCreatedSubAccounts);
            break;
          case "P":
          case "Y":
            this.RetrieveDestributionByAcctState(out aAccounts, allocation, masterPostPeriod, srcAcc, out notCreatedSubAccounts);
            break;
          case "E":
            this.RetrieveExternalDestribution(out aAccounts, allocation, masterPostPeriod);
            break;
          default:
            aAccounts = (AccountWeightedList) null;
            break;
        }
        if (Math.Abs(aAccounts.totalWeight) < 0.0001M)
          throw new PXException("GL Allocation cannot be completed. Allocated amount cannot be fully distributed to destination GL Accounts. Please verify the GL Allocation settings.");
        if (notCreatedSubAccounts.Count > 0)
        {
          foreach (string str2 in notCreatedSubAccounts)
          {
            if (!source.Contains(str2))
              source.Add(str2);
          }
        }
        else
        {
          int? nullable8 = srcAcc.ContrAccontID ?? srcAcc.AccountID;
          Decimal num17 = AccountRules.IsCreditBalance(aAcctDefs[nullable8.Value].Type) ? -1M : 1M;
          AllocationProcess.DistributeAmount(instance, allocation, srcAcc.AllocatedAmount.Value * num17, aAccounts, ledger);
        }
      }
      if (source.Count > 5)
      {
        PXTrace.WriteError("GL allocation cannot be completed. The following destination subaccounts cannot be found in the system: ({0}).", new object[1]
        {
          (object) source.Aggregate<string>((Func<string, string, string>) ((a, b) => $"{a}, {b}"))
        });
        throw new PXException("GL allocation cannot be completed. Destination subaccounts cannot be found in the system. Check Trace for details.");
      }
      if (source.Count > 0)
        throw new PXException("GL allocation cannot be completed. The following destination subaccounts cannot be found in the system: ({0}).", new object[1]
        {
          (object) source.Aggregate<string>((Func<string, string, string>) ((a, b) => $"{a}, {b}"))
        });
    }
    else
    {
      AccountWeightedList aAccounts;
      switch (allocation.AllocMethod)
      {
        case "W":
        case "C":
          this.RetrievePredefinedDestribution(out aAccounts, allocation);
          break;
        case "P":
        case "Y":
          this.RetrieveDestributionByAcctState(out aAccounts, allocation, masterPostPeriod);
          break;
        case "E":
          this.RetrieveExternalDestribution(out aAccounts, allocation, masterPostPeriod);
          break;
        default:
          aAccounts = (AccountWeightedList) null;
          break;
      }
      if (Math.Abs(aAccounts.totalWeight) < 0.0001M)
        throw new PXException("GL Allocation cannot be completed. Allocated amount cannot be fully distributed to destination GL Accounts. Please verify the GL Allocation settings.");
      Decimal aAmount = totalCredit - totalDebit;
      AllocationProcess.DistributeAmount(instance, allocation, aAmount, aAccounts, ledger);
    }
    GLAllocationHistory allocationHistory = (GLAllocationHistory) ((PXSelectBase) instance.AllocationHistory).Cache.Insert((object) new GLAllocationHistory()
    {
      GLAllocationID = allocation.GLAllocationID
    });
    ((PXAction) instance.Save).Press();
    if (((PXSelectBase<Batch>) instance.BatchModule).Current == null || !(allocation is AllocationProcess.AllocationExt))
      return;
    AllocationProcess.AllocationExt.CopyFrom((AllocationProcess.AllocationExt) allocation, ((PXSelectBase<Batch>) instance.BatchModule).Current);
  }

  protected virtual string FindLastAllocationPeriod(
    string aAllocationID,
    string masterBeforePeriodID)
  {
    foreach (PXResult<Batch, GLAllocationHistory, AllocationProcess.ReversingBatch> pxResult in ((PXSelectBase<Batch>) new PXSelectJoin<Batch, InnerJoin<GLAllocationHistory, On<GLAllocationHistory.module, Equal<Batch.module>, And<GLAllocationHistory.batchNbr, Equal<Batch.batchNbr>>>, LeftJoin<AllocationProcess.ReversingBatch, On<AllocationProcess.ReversingBatch.origBatchNbr, Equal<Batch.batchNbr>, And<AllocationProcess.ReversingBatch.origModule, Equal<Batch.module>, And<AllocationProcess.ReversingBatch.autoReverseCopy, Equal<BitOn>>>>>>, Where<Batch.autoReverseCopy, NotEqual<BitOn>, And<Batch.origBatchNbr, IsNull, And<GLAllocationHistory.gLAllocationID, Equal<Required<GLAllocationHistory.gLAllocationID>>, And<Batch.tranPeriodID, LessEqual<Required<Batch.tranPeriodID>>>>>>, OrderBy<Desc<Batch.tranPeriodID>>>((PXGraph) this)).Select(new object[2]
    {
      (object) aAllocationID,
      (object) masterBeforePeriodID
    }))
    {
      Batch batch = PXResult<Batch, GLAllocationHistory, AllocationProcess.ReversingBatch>.op_Implicit(pxResult);
      if (string.IsNullOrEmpty(PXResult<Batch, GLAllocationHistory, AllocationProcess.ReversingBatch>.op_Implicit(pxResult).BatchNbr))
        return batch.TranPeriodID;
    }
    return (string) null;
  }

  protected virtual Batch FindFutureAllocationApplication(
    string aAllocationID,
    string masterRefPeriod)
  {
    foreach (PXResult<Batch, GLAllocationHistory, AllocationProcess.ReversingBatch> pxResult in ((PXSelectBase<Batch>) new PXSelectJoin<Batch, InnerJoin<GLAllocationHistory, On<GLAllocationHistory.module, Equal<Batch.module>, And<GLAllocationHistory.batchNbr, Equal<Batch.batchNbr>>>, LeftJoin<AllocationProcess.ReversingBatch, On<AllocationProcess.ReversingBatch.origBatchNbr, Equal<Batch.batchNbr>, And<AllocationProcess.ReversingBatch.origModule, Equal<Batch.module>, And<AllocationProcess.ReversingBatch.autoReverseCopy, Equal<BitOn>>>>>>, Where<Batch.autoReverseCopy, NotEqual<BitOn>, And<Batch.origBatchNbr, IsNull, And<GLAllocationHistory.gLAllocationID, Equal<Required<GLAllocationHistory.gLAllocationID>>, And<Batch.tranPeriodID, Greater<Required<Batch.tranPeriodID>>>>>>, OrderBy<Desc<Batch.tranPeriodID>>>((PXGraph) this)).Select(new object[2]
    {
      (object) aAllocationID,
      (object) masterRefPeriod
    }))
    {
      Batch allocationApplication = PXResult<Batch, GLAllocationHistory, AllocationProcess.ReversingBatch>.op_Implicit(pxResult);
      if (string.IsNullOrEmpty(PXResult<Batch, GLAllocationHistory, AllocationProcess.ReversingBatch>.op_Implicit(pxResult).BatchNbr))
        return allocationApplication;
    }
    return (Batch) null;
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R2.")]
  protected virtual Batch FindLastAllocationBatchAfter(
    string aAllocID,
    string aFinPeriodID,
    DateTime aAfter)
  {
    PXResultset<Batch> pxResultset = ((PXSelectBase<Batch>) new PXSelectJoin<Batch, InnerJoin<GLAllocationHistory, On<GLAllocationHistory.module, Equal<Batch.module>, And<GLAllocationHistory.batchNbr, Equal<Batch.batchNbr>>>, LeftJoin<Branch, On<Batch.branchID, Equal<Branch.branchID>>, LeftJoin<FinPeriod, On<Batch.finPeriodID, Equal<FinPeriod.finPeriodID>, And<Branch.organizationID, Equal<FinPeriod.organizationID>>>>>>, Where<GLAllocationHistory.gLAllocationID, Equal<Required<GLAllocationHistory.gLAllocationID>>, And<FinPeriod.masterFinPeriodID, Equal<Required<Batch.finPeriodID>>, And<Batch.dateEntered, Greater<Required<Batch.dateEntered>>>>>, OrderBy<Desc<Batch.dateEntered>>>((PXGraph) this)).SelectWindowed(0, 1, new object[3]
    {
      (object) aAllocID,
      (object) aFinPeriodID,
      (object) aAfter
    });
    return pxResultset.Count > 0 ? PXResult<Batch>.op_Implicit(pxResultset[0]) : (Batch) null;
  }

  protected virtual void RetrievePredefinedDestribution(
    out AccountWeightedList aAccounts,
    GLAllocation allocation)
  {
    aAccounts = new AccountWeightedList();
    foreach (PXResult<GLAllocationDestination> pxResult in ((PXSelectBase<GLAllocationDestination>) new PXSelect<GLAllocationDestination, Where<GLAllocationDestination.gLAllocationID, Equal<Required<GLAllocationDestination.gLAllocationID>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) allocation.GLAllocationID
    }))
    {
      GLAllocationDestination allocationDestination = PXResult<GLAllocationDestination>.op_Implicit(pxResult);
      Account account = Account.UK.Find((PXGraph) this, allocationDestination.AccountCD);
      Sub sub = Sub.UK.Find((PXGraph) this, allocationDestination.SubCD);
      if (!account.Active.GetValueOrDefault())
        throw new PXException("One or more of destination accounts is inactive");
      AccountWeightedList accountWeightedList = aAccounts;
      int? nullable = allocationDestination.BranchID;
      int branchID = nullable.Value;
      nullable = account.AccountID;
      int accountId = nullable.Value;
      nullable = sub.SubID;
      int subID = nullable.Value;
      Decimal weight = allocationDestination.Weight.Value;
      accountWeightedList.Add(branchID, accountId, subID, weight);
    }
  }

  protected virtual void RetrievePredefinedDestribution(
    out AccountWeightedList aAccounts,
    GLAllocation allocation,
    GLAllocationAccountHistory srcAcc,
    out List<string> notCreatedSubAccounts)
  {
    aAccounts = new AccountWeightedList();
    notCreatedSubAccounts = new List<string>();
    PXSelect<GLAllocationDestination, Where<GLAllocationDestination.gLAllocationID, Equal<Required<GLAllocationDestination.gLAllocationID>>>> pxSelect = new PXSelect<GLAllocationDestination, Where<GLAllocationDestination.gLAllocationID, Equal<Required<GLAllocationDestination.gLAllocationID>>>>((PXGraph) this);
    SubAccountMaint instance = PXGraph.CreateInstance<SubAccountMaint>();
    object[] objArray = new object[1]
    {
      (object) allocation.GLAllocationID
    };
    foreach (PXResult<GLAllocationDestination> pxResult in ((PXSelectBase<GLAllocationDestination>) pxSelect).Select(objArray))
    {
      GLAllocationDestination allocationDestination = PXResult<GLAllocationDestination>.op_Implicit(pxResult);
      Account account = Account.UK.Find((PXGraph) this, allocationDestination.AccountCD);
      Sub sub1 = Sub.UK.Find((PXGraph) this, allocationDestination.SubCD);
      if (account == null)
        account = Account.PK.Find((PXGraph) this, srcAcc.AccountID);
      if (!account.Active.GetValueOrDefault())
        throw new PXException("One or more of destination accounts is inactive");
      int? nullable;
      Decimal? weight1;
      if (sub1 != null)
      {
        AccountWeightedList accountWeightedList = aAccounts;
        nullable = allocationDestination.BranchID;
        int branchID = nullable.Value;
        nullable = account.AccountID;
        int accountId = nullable.Value;
        nullable = sub1.SubID;
        int subID = nullable.Value;
        weight1 = allocationDestination.Weight;
        Decimal weight2 = weight1.Value;
        accountWeightedList.Add(branchID, accountId, subID, weight2);
      }
      else
      {
        Sub sub2 = Sub.PK.Find((PXGraph) this, srcAcc.SubID);
        string subCD = "";
        PXDimensionAttribute.Definition slot = PXContext.GetSlot<PXDimensionAttribute.Definition>();
        int num = ((IEnumerable<PXSegment>) slot.Dimensions["SUBACCOUNT"]).Count<PXSegment>();
        int startIndex = 0;
        for (int index = 1; index <= num; ++index)
        {
          List<string> list = ((IEnumerable<string>) PXDimensionAttribute.GetSegmentValues("SUBACCOUNT", index)).ToList<string>();
          string str1 = allocationDestination.SubCD == null ? "" : allocationDestination.SubCD.Substring(startIndex, allocationDestination.SubCD.Length < startIndex + list[0].Length ? allocationDestination.SubCD.Length - startIndex : list[0].Length);
          if (str1.Trim() == "")
          {
            if (list.Count > 0)
            {
              string str2 = sub2.SubCD.Substring(startIndex, sub2.SubCD.Length < startIndex + list[0].Length ? sub2.SubCD.Length - startIndex : list[0].Length);
              subCD += str2;
              startIndex += list[0].Length;
            }
            else
            {
              subCD = sub2.SubCD;
              startIndex = sub2.SubCD.Length;
            }
          }
          else
          {
            subCD += str1;
            startIndex += list[0].Length;
          }
        }
        int? subIdByCd = SubAccountMaint.FindSubIDByCD((PXGraph) this, subCD);
        if (!subIdByCd.HasValue)
        {
          if (slot.LookupModes["SUBACCOUNT"] == 1)
          {
            ((PXSelectBase<Sub>) instance.SubRecords).Insert(new Sub()
            {
              SubCD = subCD,
              Active = new bool?(true)
            });
            ((PXAction) instance.Save).Press();
            int? subId = Sub.UK.Find((PXGraph) this, subCD).SubID;
            AccountWeightedList accountWeightedList = aAccounts;
            nullable = allocationDestination.BranchID;
            int branchID = nullable.Value;
            nullable = account.AccountID;
            int accountId = nullable.Value;
            int subID = subId.Value;
            weight1 = allocationDestination.Weight;
            Decimal weight3 = weight1.Value;
            accountWeightedList.Add(branchID, accountId, subID, weight3);
          }
          else
          {
            aAccounts = new AccountWeightedList();
            notCreatedSubAccounts.Add(this._subMask == null ? subCD : Mask.Format(this._subMask, subCD));
          }
        }
        else
        {
          AccountWeightedList accountWeightedList = aAccounts;
          nullable = allocationDestination.BranchID;
          int branchID = nullable.Value;
          nullable = account.AccountID;
          int accountId = nullable.Value;
          int subID = subIdByCd.Value;
          weight1 = allocationDestination.Weight;
          Decimal weight4 = weight1.Value;
          accountWeightedList.Add(branchID, accountId, subID, weight4);
        }
      }
    }
  }

  protected virtual bool RetrieveDestributionByAcctState(
    out AccountWeightedList aAccounts,
    GLAllocation allocation,
    string masterPostPeriodID,
    GLAllocationAccountHistory srcAcc,
    out List<string> notCreatedSubAccounts)
  {
    PXSelect<GLAllocationDestination, Where<GLAllocationDestination.gLAllocationID, Equal<Required<GLAllocationDestination.gLAllocationID>>>> pxSelect = new PXSelect<GLAllocationDestination, Where<GLAllocationDestination.gLAllocationID, Equal<Required<GLAllocationDestination.gLAllocationID>>>>((PXGraph) this);
    int? nullable1 = allocation.BasisLederID.HasValue ? allocation.BasisLederID : allocation.AllocLedgerID;
    PXSelectBase<GLHistoryByPeriod> pxSelectBase = (PXSelectBase<GLHistoryByPeriod>) new PXSelectJoinGroupBy<GLHistoryByPeriod, InnerJoin<Account, On<GLHistoryByPeriod.accountID, Equal<Account.accountID>, And<Match<Account, Current<AccessInfo.userName>>>>, InnerJoin<Sub, On<GLHistoryByPeriod.subID, Equal<Sub.subID>, And<Match<Sub, Current<AccessInfo.userName>>>>, LeftJoin<GLHistory, On<GLHistoryByPeriod.accountID, Equal<GLHistory.accountID>, And<GLHistoryByPeriod.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryByPeriod.branchID, Equal<GLHistory.branchID>, And<GLHistoryByPeriod.subID, Equal<GLHistory.subID>, And<GLHistoryByPeriod.finPeriodID, Equal<GLHistory.finPeriodID>>>>>>, LeftJoin<AH, On<GLHistoryByPeriod.ledgerID, Equal<AH.ledgerID>, And<GLHistoryByPeriod.branchID, Equal<AH.branchID>, And<GLHistoryByPeriod.accountID, Equal<AH.accountID>, And<GLHistoryByPeriod.subID, Equal<AH.subID>, And<GLHistoryByPeriod.lastActivityPeriod, Equal<AH.finPeriodID>>>>>>>>>>, Where<GLHistoryByPeriod.branchID, Equal<Required<GLHistoryByPeriod.branchID>>, And<Account.accountCD, Like<Required<Account.accountCD>>, And<Sub.subCD, Like<Required<Sub.subCD>>, And<GLHistoryByPeriod.finPeriodID, Equal<Required<GLHistoryByPeriod.finPeriodID>>, And<GLHistoryByPeriod.ledgerID, Equal<Required<GLHistoryByPeriod.ledgerID>>, And<Where2<Where<Account.accountID, NotEqual<Current<PX.Objects.GL.GLSetup.ytdNetIncAccountID>>, And<Where<Account.type, Equal<AccountType.asset>, Or<Account.type, Equal<AccountType.liability>>>>>, Or<Where<GLHistoryByPeriod.lastActivityPeriod, GreaterEqual<Required<GLHistoryByPeriod.lastActivityPeriod>>, And<Where<Account.type, Equal<AccountType.expense>, Or<Account.type, Equal<AccountType.income>, Or<Account.accountID, Equal<Current<PX.Objects.GL.GLSetup.ytdNetIncAccountID>>>>>>>>>>>>>>>, Aggregate<Sum<GLHistory.finPtdDebit, Sum<GLHistory.finPtdCredit, Sum<AH.finYtdBalance, GroupBy<GLHistoryByPeriod.accountID, GroupBy<GLHistoryByPeriod.subID, GroupBy<Account.active>>>>>>>>((PXGraph) this);
    aAccounts = new AccountWeightedList();
    bool flag1 = allocation.AllocMethod == "P";
    SubAccountMaint instance = PXGraph.CreateInstance<SubAccountMaint>();
    notCreatedSubAccounts = new List<string>();
    object[] objArray = new object[1]
    {
      (object) allocation.GLAllocationID
    };
    foreach (PXResult<GLAllocationDestination> pxResult1 in ((PXSelectBase<GLAllocationDestination>) pxSelect).Select(objArray))
    {
      GLAllocationDestination allocationDestination = PXResult<GLAllocationDestination>.op_Implicit(pxResult1);
      Account account1 = Account.UK.Find((PXGraph) this, allocationDestination.AccountCD);
      Sub sub1 = Sub.UK.Find((PXGraph) this, allocationDestination.SubCD);
      if (account1 == null)
        account1 = Account.PK.Find((PXGraph) this, srcAcc.AccountID);
      if (!account1.Active.GetValueOrDefault())
        throw new PXException("One or more of destination accounts is inactive");
      if (sub1 == null)
      {
        Sub sub2 = Sub.PK.Find((PXGraph) this, srcAcc.SubID);
        string subCD = "";
        PXDimensionAttribute.Definition slot = PXContext.GetSlot<PXDimensionAttribute.Definition>();
        int num = ((IEnumerable<PXSegment>) slot.Dimensions["SUBACCOUNT"]).Count<PXSegment>();
        int startIndex = 0;
        for (int index = 1; index <= num; ++index)
        {
          List<string> list = ((IEnumerable<string>) PXDimensionAttribute.GetSegmentValues("SUBACCOUNT", index)).ToList<string>();
          string str1 = allocationDestination.SubCD == null ? "" : allocationDestination.SubCD.Substring(startIndex, allocationDestination.SubCD.Length < startIndex + list[0].Length ? allocationDestination.SubCD.Length - startIndex : list[0].Length);
          if (str1.Trim() == "")
          {
            string str2 = sub2.SubCD.Substring(startIndex, sub2.SubCD.Length < startIndex + list[0].Length ? sub2.SubCD.Length - startIndex : list[0].Length);
            subCD += str2;
          }
          else
            subCD += str1;
          startIndex += list[0].Length;
        }
        if (!SubAccountMaint.FindSubIDByCD((PXGraph) this, subCD).HasValue)
        {
          if (slot.LookupModes["SUBACCOUNT"] == 1)
          {
            ((PXSelectBase<Sub>) instance.SubRecords).Insert(new Sub()
            {
              SubCD = subCD,
              Active = new bool?(true)
            });
            ((PXAction) instance.Save).Press();
            sub1 = Sub.UK.Find((PXGraph) this, subCD);
          }
          else
          {
            aAccounts = new AccountWeightedList();
            notCreatedSubAccounts.Add(this._subMask == null ? subCD : Mask.Format(this._subMask, subCD));
            return false;
          }
        }
        else
          sub1 = Sub.UK.Find((PXGraph) this, subCD);
      }
      int num1 = string.IsNullOrEmpty(allocationDestination.BasisAccountCD) ? 0 : (!string.IsNullOrEmpty(allocationDestination.BasisSubCD) ? 1 : 0);
      int? nullable2 = allocationDestination.BasisBranchID.HasValue ? allocationDestination.BasisBranchID : allocationDestination.BranchID;
      string aSub1 = num1 != 0 ? allocationDestination.BasisAccountCD : account1.AccountCD;
      string aSub2 = num1 != 0 ? allocationDestination.BasisSubCD : sub1.SubCD;
      string subCdWildcard1 = SubCDUtils.CreateSubCDWildcard(aSub1, "ACCOUNT");
      string subCdWildcard2 = SubCDUtils.CreateSubCDWildcard(aSub2, "SUBACCOUNT");
      AccountWeight accountWeight = new AccountWeight(allocationDestination.BranchID.Value, account1.AccountID.Value, sub1.SubID.Value, 0.0M);
      bool flag2 = false;
      bool flag3 = false;
      using (new PXFieldScope(((PXSelectBase) pxSelectBase).View, new Type[6]
      {
        typeof (GLHistory.finPtdDebit),
        typeof (GLHistory.finPtdCredit),
        typeof (AH.finYtdBalance),
        typeof (Account.accountID),
        typeof (Account.active),
        typeof (Account.type)
      }))
      {
        FinPeriod result = this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(nullable2), masterPostPeriodID).Result;
        string str = AllocationProcess.FirstPeriodOfYear(FinPeriodUtils.FiscalYear(result.FinPeriodID));
        foreach (PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH> pxResult2 in pxSelectBase.Select(new object[6]
        {
          (object) nullable2,
          (object) subCdWildcard1,
          (object) subCdWildcard2,
          (object) result.FinPeriodID,
          (object) nullable1,
          (object) str
        }))
        {
          flag3 = true;
          GLHistory glHistory = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult2);
          Account account2 = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult2);
          AH ah = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult2);
          if (account2 != null && account2.AccountID.HasValue && account2.Active.GetValueOrDefault())
          {
            flag2 = true;
            Decimal num2 = ah.YtdBalance ?? 0.0M;
            Decimal num3 = AccountRules.IsCreditBalance(account2.Type) ? -1M : 1M;
            if (flag1)
              num2 = glHistory.PtdDebit.GetValueOrDefault() - glHistory.PtdCredit.GetValueOrDefault();
            accountWeight.Weight += num2 * num3;
          }
        }
      }
      if (!flag2 && flag3)
        throw new PXException("All basis accounts are inactive for one or more destination accounts");
      aAccounts.Add(accountWeight);
    }
    return true;
  }

  protected virtual bool RetrieveDestributionByAcctState(
    out AccountWeightedList aAccounts,
    GLAllocation allocation,
    string masterPostPeriodID)
  {
    PXSelect<GLAllocationDestination, Where<GLAllocationDestination.gLAllocationID, Equal<Required<GLAllocationDestination.gLAllocationID>>>> pxSelect = new PXSelect<GLAllocationDestination, Where<GLAllocationDestination.gLAllocationID, Equal<Required<GLAllocationDestination.gLAllocationID>>>>((PXGraph) this);
    int? nullable1 = allocation.BasisLederID.HasValue ? allocation.BasisLederID : allocation.AllocLedgerID;
    PXSelectBase<GLHistoryByPeriod> pxSelectBase = (PXSelectBase<GLHistoryByPeriod>) new PXSelectJoinGroupBy<GLHistoryByPeriod, InnerJoin<Account, On<GLHistoryByPeriod.accountID, Equal<Account.accountID>, And<Match<Account, Current<AccessInfo.userName>>>>, InnerJoin<Sub, On<GLHistoryByPeriod.subID, Equal<Sub.subID>, And<Match<Sub, Current<AccessInfo.userName>>>>, LeftJoin<GLHistory, On<GLHistoryByPeriod.accountID, Equal<GLHistory.accountID>, And<GLHistoryByPeriod.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryByPeriod.branchID, Equal<GLHistory.branchID>, And<GLHistoryByPeriod.subID, Equal<GLHistory.subID>, And<GLHistoryByPeriod.finPeriodID, Equal<GLHistory.finPeriodID>>>>>>, LeftJoin<AH, On<GLHistoryByPeriod.ledgerID, Equal<AH.ledgerID>, And<GLHistoryByPeriod.branchID, Equal<AH.branchID>, And<GLHistoryByPeriod.accountID, Equal<AH.accountID>, And<GLHistoryByPeriod.subID, Equal<AH.subID>, And<GLHistoryByPeriod.lastActivityPeriod, Equal<AH.finPeriodID>>>>>>>>>>, Where<GLHistoryByPeriod.branchID, Equal<Required<GLHistoryByPeriod.branchID>>, And<Account.accountCD, Like<Required<Account.accountCD>>, And<Sub.subCD, Like<Required<Sub.subCD>>, And<GLHistoryByPeriod.finPeriodID, Equal<Required<GLHistoryByPeriod.finPeriodID>>, And<GLHistoryByPeriod.ledgerID, Equal<Required<GLHistoryByPeriod.ledgerID>>, And<Where2<Where<Account.accountID, NotEqual<Current<PX.Objects.GL.GLSetup.ytdNetIncAccountID>>, And<Where<Account.type, Equal<AccountType.asset>, Or<Account.type, Equal<AccountType.liability>>>>>, Or<Where<GLHistoryByPeriod.lastActivityPeriod, GreaterEqual<Required<GLHistoryByPeriod.lastActivityPeriod>>, And<Where<Account.type, Equal<AccountType.expense>, Or<Account.type, Equal<AccountType.income>, Or<Account.accountID, Equal<Current<PX.Objects.GL.GLSetup.ytdNetIncAccountID>>>>>>>>>>>>>>>, Aggregate<Sum<GLHistory.finPtdDebit, Sum<GLHistory.finPtdCredit, Sum<AH.finYtdBalance, GroupBy<GLHistoryByPeriod.accountID, GroupBy<GLHistoryByPeriod.subID, GroupBy<Account.active>>>>>>>>((PXGraph) this);
    aAccounts = new AccountWeightedList();
    bool flag1 = allocation.AllocMethod == "P";
    object[] objArray = new object[1]
    {
      (object) allocation.GLAllocationID
    };
    foreach (PXResult<GLAllocationDestination> pxResult1 in ((PXSelectBase<GLAllocationDestination>) pxSelect).Select(objArray))
    {
      GLAllocationDestination allocationDestination = PXResult<GLAllocationDestination>.op_Implicit(pxResult1);
      Account account1 = Account.UK.Find((PXGraph) this, allocationDestination.AccountCD);
      Sub sub = Sub.UK.Find((PXGraph) this, allocationDestination.SubCD);
      bool? active = account1.Active;
      if (!active.GetValueOrDefault())
        throw new PXException("One or more of destination accounts is inactive");
      int num1 = string.IsNullOrEmpty(allocationDestination.BasisAccountCD) ? 0 : (!string.IsNullOrEmpty(allocationDestination.BasisSubCD) ? 1 : 0);
      int? nullable2 = allocationDestination.BasisBranchID.HasValue ? allocationDestination.BasisBranchID : allocationDestination.BranchID;
      string aSub1 = num1 != 0 ? allocationDestination.BasisAccountCD : account1.AccountCD;
      string aSub2 = num1 != 0 ? allocationDestination.BasisSubCD : sub.SubCD;
      string subCdWildcard1 = SubCDUtils.CreateSubCDWildcard(aSub1, "ACCOUNT");
      string subCdWildcard2 = SubCDUtils.CreateSubCDWildcard(aSub2, "SUBACCOUNT");
      AccountWeight accountWeight = new AccountWeight(allocationDestination.BranchID.Value, account1.AccountID.Value, sub.SubID.Value, 0.0M);
      bool flag2 = false;
      bool flag3 = false;
      using (new PXFieldScope(((PXSelectBase) pxSelectBase).View, new Type[6]
      {
        typeof (GLHistory.finPtdDebit),
        typeof (GLHistory.finPtdCredit),
        typeof (AH.finYtdBalance),
        typeof (Account.accountID),
        typeof (Account.active),
        typeof (Account.type)
      }))
      {
        FinPeriod result = this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(nullable2), masterPostPeriodID).Result;
        string str = AllocationProcess.FirstPeriodOfYear(FinPeriodUtils.FiscalYear(result.FinPeriodID));
        foreach (PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH> pxResult2 in pxSelectBase.Select(new object[6]
        {
          (object) nullable2,
          (object) subCdWildcard1,
          (object) subCdWildcard2,
          (object) result.FinPeriodID,
          (object) nullable1,
          (object) str
        }))
        {
          flag3 = true;
          GLHistory glHistory = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult2);
          Account account2 = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult2);
          AH ah = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult2);
          if (account2 != null && account2.AccountID.HasValue)
          {
            active = account2.Active;
            if (active.GetValueOrDefault())
            {
              flag2 = true;
              Decimal num2 = ah.YtdBalance ?? 0.0M;
              Decimal num3 = AccountRules.IsCreditBalance(account2.Type) ? -1M : 1M;
              if (flag1)
                num2 = glHistory.PtdDebit.GetValueOrDefault() - glHistory.PtdCredit.GetValueOrDefault();
              accountWeight.Weight += num2 * num3;
            }
          }
        }
      }
      if (!flag2 && flag3)
        throw new PXException("All basis accounts are inactive for one or more destination accounts");
      aAccounts.Add(accountWeight);
    }
    return true;
  }

  protected virtual bool RetrieveExternalDestribution(
    out AccountWeightedList aAccounts,
    GLAllocation allocation,
    string masterPostPeriodID)
  {
    aAccounts = (AccountWeightedList) null;
    return false;
  }

  protected virtual bool HasUnpostedBatch(
    GLAllocation aAlloc,
    string aStartPeriod,
    string aEndPeriod)
  {
    return ((PXSelectBase) new PXSelectJoin<Batch, InnerJoin<GLAllocationHistory, On<GLAllocationHistory.module, Equal<Batch.module>, And<GLAllocationHistory.batchNbr, Equal<Batch.batchNbr>>>>, Where<Batch.posted, NotEqual<BitOn>, And<GLAllocationHistory.gLAllocationID, Equal<Required<GLAllocationHistory.gLAllocationID>>, And<Batch.tranPeriodID, GreaterEqual<Required<Batch.tranPeriodID>>, And<Batch.tranPeriodID, LessEqual<Required<Batch.tranPeriodID>>>>>>>((PXGraph) this)).View.SelectSingle(new object[3]
    {
      (object) aAlloc.GLAllocationID,
      (object) aStartPeriod,
      (object) aEndPeriod
    }) != null;
  }

  protected static void AllocateAmount(
    JournalEntry aBatchGraph,
    GLAllocation aAlloc,
    IEnumerable<GLAllocationAccountHistory> aSources,
    Dictionary<int, Account> aAcctDefs,
    Ledger aAllocLedger,
    ref Decimal totalDebit,
    ref Decimal totalCredit)
  {
    Batch batch = ((PXSelectBase<Batch>) aBatchGraph.BatchModule).Current;
    aSources = aSources.Where<GLAllocationAccountHistory>((Func<GLAllocationAccountHistory, bool>) (s => Math.Abs(s.AllocatedAmount.Value) > 0.0001M));
    aBatchGraph.FinPeriodUtils.ValidateFinPeriod<GLAllocationAccountHistory>(aSources, (Func<GLAllocationAccountHistory, string>) (m => batch.FinPeriodID), (Func<GLAllocationAccountHistory, int?[]>) (m => m.BranchID.SingleToArray<int?>()));
    foreach (GLAllocationAccountHistory aSource in aSources)
    {
      GLTran glTran1 = new GLTran();
      Decimal num1 = aSource.AllocatedAmount.Value;
      glTran1.BranchID = aSource.BranchID;
      GLTran glTran2 = glTran1;
      int? nullable1 = aSource.ContrAccontID;
      int? nullable2 = nullable1 ?? aSource.AccountID;
      glTran2.AccountID = nullable2;
      GLTran glTran3 = glTran1;
      nullable1 = aSource.ContrSubID;
      int? nullable3 = nullable1 ?? aSource.SubID;
      glTran3.SubID = nullable3;
      glTran1.TranDesc = PXMessages.LocalizeFormatNoPrefix("Src: {0}", new object[1]
      {
        string.IsNullOrEmpty(aAlloc.Descr) ? (object) aAlloc.GLAllocationID : (object) aAlloc.Descr
      });
      Dictionary<int, Account> dictionary = aAcctDefs;
      nullable1 = glTran1.AccountID;
      int key = nullable1.Value;
      Account aAcct = dictionary[key];
      if (!AllocationProcess.IsDenominationMatch(aAcct, aAllocLedger))
        throw new PXException("One or more source GL accounts are denominated in foreign currencies. These GL accounts cannot be used for GL allocation.");
      Decimal num2 = PXCurrencyAttribute.Round<Batch.curyInfoID>(((PXSelectBase) aBatchGraph.BatchModule).Cache, (object) batch, Math.Abs(num1), CMPrecision.TRANCURY);
      aSource.AllocatedAmount = new Decimal?(num2 * (Decimal) Math.Sign(aSource.AllocatedAmount.Value));
      if (AccountRules.IsCreditBalance(aAcct.Type))
      {
        if (num1 > 0M)
        {
          glTran1.CuryDebitAmt = new Decimal?(num2);
          totalDebit += num2;
        }
        else
        {
          glTran1.CuryCreditAmt = new Decimal?(num2);
          totalCredit += num2;
        }
      }
      else if (num1 > 0M)
      {
        glTran1.CuryCreditAmt = new Decimal?(num2);
        totalCredit += num2;
      }
      else
      {
        glTran1.CuryDebitAmt = new Decimal?(num2);
        totalDebit += num2;
      }
      ((PXSelectBase<GLTran>) aBatchGraph.GLTranModuleBatNbr).Insert(glTran1);
      ((PXSelectBase<GLAllocationAccountHistory>) aBatchGraph.AllocationAccountHistory).Insert(aSource);
    }
  }

  protected static void DistributeAmount(
    JournalEntry aBatchGraph,
    GLAllocation aAlloc,
    Decimal aAmount,
    AccountWeightedList aDestAccounts,
    Ledger aAllocLedger)
  {
    if (aAlloc.AllocMethod == "C" && !aDestAccounts.isPercent())
      throw new PXException("The total percentage for destination GL Accounts must equal 100%.");
    Batch batch = ((PXSelectBase<Batch>) aBatchGraph.BatchModule).Current;
    bool flag = aAmount > 0M;
    aAmount = Math.Abs(aAmount);
    PX.Objects.CM.Currency currency = PXResultset<PX.Objects.CM.Currency>.op_Implicit(PXSelectBase<PX.Objects.CM.Currency, PXSelectReadonly<PX.Objects.CM.Currency, Where<PX.Objects.CM.Currency.curyID, Equal<Required<PX.Objects.CM.Currency.curyID>>>>.Config>.Select((PXGraph) aBatchGraph, new object[1]
    {
      (object) batch.CuryID
    }));
    int precision = currency == null || !currency.DecimalPlaces.HasValue ? 2 : (int) currency.DecimalPlaces.Value;
    Decimal num1 = aAmount;
    GLTran glTran1 = (GLTran) null;
    IEnumerable<Tuple<AccountWeight, Decimal>> source = aDestAccounts.List.Select<AccountWeight, Tuple<AccountWeight, Decimal>>((Func<AccountWeight, Tuple<AccountWeight, Decimal>>) (x =>
    {
      Decimal aValue = PXCurrencyAttribute.Round<Batch.curyInfoID>(((PXSelectBase) aBatchGraph.BatchModule).Cache, (object) batch, AllocationProcess.calcShareValue(aAmount, aDestAccounts.totalWeight, x.Weight, precision), CMPrecision.TRANCURY);
      return !AllocationProcess.isOutOfDbPrecision(aValue) ? new Tuple<AccountWeight, Decimal>(x, aValue) : throw new NotFiniteNumberException();
    })).Where<Tuple<AccountWeight, Decimal>>((Func<Tuple<AccountWeight, Decimal>, bool>) (x => Math.Abs(x.Item2) >= 0.0001M));
    aBatchGraph.FinPeriodUtils.ValidateFinPeriod<AccountWeight>(source.Select<Tuple<AccountWeight, Decimal>, AccountWeight>((Func<Tuple<AccountWeight, Decimal>, AccountWeight>) (x => x.Item1)), (Func<AccountWeight, string>) (m => batch.FinPeriodID), (Func<AccountWeight, int?[]>) (m => new int?(m.BranchID).SingleToArray<int?>()));
    foreach (Tuple<AccountWeight, Decimal> tuple in source)
    {
      AccountWeight accountWeight = tuple.Item1;
      Decimal num2 = tuple.Item2;
      try
      {
        GLTran glTran2 = new GLTran();
        glTran2.BranchID = new int?(accountWeight.BranchID);
        glTran2.AccountID = new int?(accountWeight.AccountID);
        glTran2.SubID = new int?(accountWeight.SubID);
        glTran2.LedgerID = aAlloc.AllocLedgerID;
        if (!AllocationProcess.IsDenominationMatch(PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account>.Config>.Search<Account.accountID>((PXGraph) aBatchGraph, (object) glTran2.AccountID, Array.Empty<object>())), aAllocLedger))
          throw new PXException("One or more source GL accounts are denominated in foreign currencies. These GL accounts cannot be used for GL allocation.");
        if (flag)
        {
          if (num2 > 0M)
            glTran2.CuryDebitAmt = new Decimal?(num2);
          else
            glTran2.CuryCreditAmt = new Decimal?(-num2);
        }
        else if (num2 > 0M)
          glTran2.CuryCreditAmt = new Decimal?(num2);
        else
          glTran2.CuryDebitAmt = new Decimal?(-num2);
        glTran2.TranDesc = PXMessages.LocalizeFormatNoPrefix("Dst: {0}", new object[1]
        {
          string.IsNullOrEmpty(aAlloc.Descr) ? (object) aAlloc.GLAllocationID : (object) aAlloc.Descr
        });
        GLTran glTran3 = (GLTran) ((PXSelectBase) aBatchGraph.GLTranModuleBatNbr).Cache.Insert((object) glTran2);
        if (glTran1 != null)
        {
          Decimal? curyDebitAmt = glTran1.CuryDebitAmt;
          Decimal num3 = num2;
          if (curyDebitAmt.GetValueOrDefault() < num3 & curyDebitAmt.HasValue)
          {
            Decimal? curyCreditAmt = glTran1.CuryCreditAmt;
            Decimal num4 = num2;
            if (!(curyCreditAmt.GetValueOrDefault() < num4 & curyCreditAmt.HasValue))
              goto label_18;
          }
          else
            goto label_18;
        }
        glTran1 = glTran3;
label_18:
        num1 -= num2;
      }
      catch (OverflowException ex)
      {
        throw new PXException("Allocation cannot be completed - Distribution algorithm produced too large number for Account{0} Sub {1}. Most probable reason - total weight of the Destination Accounts is too small, giving exception 0/0", new object[2]
        {
          (object) accountWeight.AccountID,
          (object) accountWeight.SubID
        });
      }
      catch (NotFiniteNumberException ex)
      {
        throw new PXException("Allocation cannot be completed - Distribution algorithm produced too large number for Account{0} Sub {1}. Most probable reason - total weight of the Destination Accounts is too small, giving exception 0/0", new object[2]
        {
          (object) accountWeight.AccountID,
          (object) accountWeight.SubID
        });
      }
    }
    if (num1 != 0M && glTran1 != null)
    {
      if (flag)
      {
        GLTran glTran4 = glTran1;
        Decimal? curyDebitAmt = glTran4.CuryDebitAmt;
        Decimal num5 = num1;
        glTran4.CuryDebitAmt = curyDebitAmt.HasValue ? new Decimal?(curyDebitAmt.GetValueOrDefault() + num5) : new Decimal?();
      }
      else
      {
        GLTran glTran5 = glTran1;
        Decimal? curyCreditAmt = glTran5.CuryCreditAmt;
        Decimal num6 = num1;
        glTran5.CuryCreditAmt = curyCreditAmt.HasValue ? new Decimal?(curyCreditAmt.GetValueOrDefault() + num6) : new Decimal?();
      }
      ((PXSelectBase<GLTran>) aBatchGraph.GLTranModuleBatNbr).Update(glTran1);
    }
    Batch copy = (Batch) ((PXSelectBase) aBatchGraph.BatchModule).Cache.CreateCopy((object) ((PXSelectBase<Batch>) aBatchGraph.BatchModule).Current);
    copy.CuryControlTotal = copy.CuryCreditTotal;
    copy.ControlTotal = copy.CreditTotal;
    if (!((PXSelectBase<PX.Objects.GL.GLSetup>) aBatchGraph.glsetup).Current.HoldEntry.GetValueOrDefault() && ((PXSelectBase<Batch>) aBatchGraph.BatchModule).Current.CreditTotal.GetValueOrDefault() == ((PXSelectBase<Batch>) aBatchGraph.BatchModule).Current.DebitTotal.GetValueOrDefault())
      copy.Hold = new bool?(false);
    Batch batch1 = (Batch) ((PXSelectBase) aBatchGraph.BatchModule).Cache.Update((object) copy);
  }

  protected virtual bool isOutOfQueue(GLAllocation aAlloc, string masterPostPeriodID)
  {
    foreach (PXResult<GLAllocation> pxResult in PXSelectBase<GLAllocation, PXSelectReadonly<GLAllocation, Where<GLAllocation.active, Equal<True>, And<GLAllocation.sortOrder, Less<Required<GLAllocation.sortOrder>>, And<GLAllocation.gLAllocationID, NotEqual<Required<GLAllocation.gLAllocationID>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) aAlloc.SortOrder,
      (object) aAlloc.GLAllocationID
    }))
    {
      GLAllocation aAllocation = PXResult<GLAllocation>.op_Implicit(pxResult);
      Batch batch = PXResultset<Batch>.op_Implicit(PXSelectBase<Batch, PXSelectReadonly2<Batch, InnerJoin<GLAllocationHistory, On<GLAllocationHistory.batchNbr, Equal<Batch.batchNbr>, And<GLAllocationHistory.module, Equal<Batch.module>>>>, Where<GLAllocationHistory.gLAllocationID, Equal<Required<GLAllocationHistory.gLAllocationID>>, And<Batch.tranPeriodID, Equal<Required<Batch.tranPeriodID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
      {
        (object) aAllocation.GLAllocationID,
        (object) masterPostPeriodID
      }));
      if ((batch == null || batch.BatchNbr == null) && this.isApplicable(aAllocation, masterPostPeriodID))
        return true;
    }
    return false;
  }

  protected virtual bool isApplicable(GLAllocation aAllocation, string masterPostPeriodID)
  {
    if (masterPostPeriodID == null)
      return false;
    bool aIgnoreYear = aAllocation.Recurring.HasValue && aAllocation.Recurring.Value;
    string masterPeriodId1 = this.GetMasterPeriodID(aAllocation, aAllocation.StartFinPeriodID);
    if (masterPeriodId1 != null && (AllocationProcess.ComparePeriods(masterPeriodId1, masterPostPeriodID, false) > 0 || aIgnoreYear && AllocationProcess.ComparePeriods(masterPeriodId1, masterPostPeriodID, aIgnoreYear) > 0))
      return false;
    string masterPeriodId2 = this.GetMasterPeriodID(aAllocation, aAllocation.EndFinPeriodID);
    return masterPeriodId2 == null || AllocationProcess.ComparePeriods(masterPeriodId2, masterPostPeriodID, aIgnoreYear) >= 0;
  }

  protected virtual string GetMasterPeriodID(GLAllocation aAllocation, string finPeriodID)
  {
    return finPeriodID == null ? (string) null : this.FinPeriodRepository.FindByID(PXAccess.GetParentOrganizationID(aAllocation.BranchID), finPeriodID).FinPeriodID;
  }

  protected virtual Ledger FindLedger(int? aLedgerID) => Ledger.PK.Find((PXGraph) this, aLedgerID);

  protected virtual void FindAllocatedAmountDetailed(
    string aAllocationID,
    GLHistory aSrcAcctHistory,
    out Decimal aAllocated,
    out Decimal aAllocatedForPriorPeriods)
  {
    aAllocated = 0M;
    aAllocatedForPriorPeriods = 0M;
    foreach (PXResult<GLAllocationAccountHistory, Batch, GLAllocationHistory> pxResult in PXSelectBase<GLAllocationAccountHistory, PXSelectJoin<GLAllocationAccountHistory, InnerJoin<Batch, On<GLAllocationAccountHistory.module, Equal<Batch.module>, And<GLAllocationAccountHistory.batchNbr, Equal<Batch.batchNbr>>>, InnerJoin<GLAllocationHistory, On<GLAllocationHistory.module, Equal<Batch.module>, And<GLAllocationHistory.batchNbr, Equal<Batch.batchNbr>>>>>, Where<GLAllocationHistory.gLAllocationID, Equal<Required<GLAllocationHistory.gLAllocationID>>, And<Batch.finPeriodID, Equal<Required<Batch.finPeriodID>>, And<GLAllocationAccountHistory.branchID, Equal<Required<GLAllocationAccountHistory.branchID>>, And<GLAllocationAccountHistory.accountID, Equal<Required<GLAllocationAccountHistory.accountID>>, And<GLAllocationAccountHistory.subID, Equal<Required<GLAllocationAccountHistory.subID>>>>>>>>.Config>.Select((PXGraph) this, new object[5]
    {
      (object) aAllocationID,
      (object) aSrcAcctHistory.FinPeriodID,
      (object) aSrcAcctHistory.BranchID,
      (object) aSrcAcctHistory.AccountID,
      (object) aSrcAcctHistory.SubID
    }))
    {
      GLAllocationAccountHistory allocationAccountHistory = PXResult<GLAllocationAccountHistory, Batch, GLAllocationHistory>.op_Implicit(pxResult);
      ref Decimal local1 = ref aAllocated;
      Decimal num1 = aAllocated;
      Decimal? nullable = allocationAccountHistory.AllocatedAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      Decimal num2 = num1 + valueOrDefault1;
      local1 = num2;
      ref Decimal local2 = ref aAllocatedForPriorPeriods;
      Decimal num3 = aAllocatedForPriorPeriods;
      nullable = allocationAccountHistory.PriorPeriodsAllocAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num4 = num3 + valueOrDefault2;
      local2 = num4;
    }
  }

  public static bool IsDenominationMatch(Account aAcct, Ledger aLedger)
  {
    return string.IsNullOrEmpty(aAcct.CuryID) || aAcct.CuryID == aLedger.BaseCuryID;
  }

  protected static Decimal calcShareValue(
    Decimal aAmount,
    Decimal aBasis,
    Decimal aWeight,
    int aPrecision)
  {
    return Decimal.Round(aAmount * aWeight / aBasis, aPrecision, MidpointRounding.ToEven);
  }

  private static int ComparePeriods(string aPeriod1, string aPeriod2, bool aIgnoreYear)
  {
    int year1;
    int periodNbr1;
    FinPeriodUtils.TryParse(aPeriod1, out year1, out periodNbr1);
    int year2;
    int periodNbr2;
    FinPeriodUtils.TryParse(aPeriod2, out year2, out periodNbr2);
    if (!aIgnoreYear)
    {
      int num = Math.Sign(year1 - year2);
      if (num != 0)
        return num;
    }
    return Math.Sign(periodNbr1 - periodNbr2);
  }

  private static string ConvertToSameYear(string aPeriod, string aRefPeriod)
  {
    string aYear = FinPeriodUtils.FiscalYear(aRefPeriod);
    string aPeriod1 = "01";
    if (!string.IsNullOrEmpty(aPeriod))
    {
      if (FinPeriodUtils.FiscalYear(aPeriod) == aYear)
        return aPeriod;
      aPeriod1 = FinPeriodUtils.PeriodInYear(aPeriod);
    }
    return FinPeriodUtils.Assemble(aYear, aPeriod1);
  }

  private static string FirstPeriodOfYear(string aRefPeriod)
  {
    return FinPeriodUtils.Assemble(FinPeriodUtils.FiscalYear(aRefPeriod), "01");
  }

  private static bool isOutOfDbPrecision(Decimal aValue)
  {
    Decimal num = 10000000000000000M;
    return Math.Abs(aValue) >= num;
  }

  protected virtual void AllocationFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    ((PXSelectBase) this.Allocations).Cache.IsDirty = false;
    cache.IsDirty = false;
  }

  [Serializable]
  public class AllocationFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _DateEntered;
    protected string _FinPeriodID;

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? DateEntered
    {
      get => this._DateEntered;
      set => this._DateEntered = value;
    }

    [OpenPeriod(typeof (AllocationProcess.AllocationFilter.dateEntered))]
    [PXUIField]
    public virtual string FinPeriodID
    {
      get => this._FinPeriodID;
      set => this._FinPeriodID = value;
    }

    public abstract class dateEntered : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      AllocationProcess.AllocationFilter.dateEntered>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcess.AllocationFilter.finPeriodID>
    {
    }
  }

  [Serializable]
  public class AllocationExt : GLAllocation
  {
    protected bool? _Selected = new bool?(false);
    protected string _Module;
    protected string _BatchNbr;
    protected string _BatchPeriod;
    protected string _Status;
    protected Decimal? _ControlTotal;

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Selected")]
    public bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    [PXDBString(1, IsFixed = true)]
    [PXDefault("C")]
    [PXUIField(DisplayName = "Distribution Method")]
    [AllocationMethod.List]
    public override string AllocMethod
    {
      get => this._AllocMethod;
      set => this._AllocMethod = value;
    }

    [PXString(2)]
    [PXDefault]
    [PXUIField]
    [BatchModule.List]
    public virtual string Module
    {
      get => this._Module;
      set => this._Module = value;
    }

    [PXString(15, IsUnicode = true)]
    [PXUIField]
    public virtual string BatchNbr
    {
      get => this._BatchNbr;
      set => this._BatchNbr = value;
    }

    [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsDBField = false)]
    [PXUIField(DisplayName = "Batch Period")]
    public virtual string BatchPeriod
    {
      get => this._BatchPeriod;
      set => this._BatchPeriod = value;
    }

    [PXString(1, IsFixed = true)]
    [PXDefault]
    [PXUIField]
    [BatchStatus.List]
    public virtual string Status
    {
      get => this._Status;
      set => this._Status = value;
    }

    [PXBaseCury]
    [PXUIField(DisplayName = "Batch Total")]
    public virtual Decimal? ControlTotal
    {
      get => this._ControlTotal;
      set => this._ControlTotal = value;
    }

    public static void CopyFrom(AllocationProcess.AllocationExt aAi, Batch aBatch)
    {
      aAi.Module = aBatch.Module;
      aAi.BatchNbr = aBatch.BatchNbr;
      aAi.BatchPeriod = aBatch.FinPeriodID;
      aAi.Status = aBatch.Status;
      aAi.ControlTotal = aBatch.ControlTotal;
    }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      AllocationProcess.AllocationExt.selected>
    {
    }

    public abstract class module : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcess.AllocationExt.module>
    {
    }

    public abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcess.AllocationExt.batchNbr>
    {
    }

    public abstract class batchPeriod : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcess.AllocationExt.batchPeriod>
    {
    }

    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcess.AllocationExt.status>
    {
    }

    public abstract class controlTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      AllocationProcess.AllocationExt.controlTotal>
    {
    }

    public new abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AllocationProcess.AllocationExt.branchID>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class ReversingBatch : Batch
  {
    public new abstract class module : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcess.ReversingBatch.module>
    {
    }

    public new abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcess.ReversingBatch.batchNbr>
    {
    }

    public new abstract class autoReverseCopy : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      AllocationProcess.ReversingBatch.autoReverseCopy>
    {
    }

    public new abstract class origModule : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcess.ReversingBatch.origModule>
    {
    }

    public new abstract class origBatchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcess.ReversingBatch.origBatchNbr>
    {
    }
  }

  public class BranchAccountSubKey(int aFirst, int aSecond, int aThird) : Triplet<int, int, int>(aFirst, aSecond, aThird)
  {
    public override int GetHashCode() => this.first + 13 * (this.second + 67 * this.third);
  }
}

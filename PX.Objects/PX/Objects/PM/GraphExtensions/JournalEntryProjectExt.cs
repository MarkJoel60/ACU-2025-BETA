// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.JournalEntryProjectExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common.Extensions;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class JournalEntryProjectExt : PXGraphExtension<JournalEntry>
{
  public PXSelect<PMRegister> ProjectDocs;
  public PXSelect<PMTran> ProjectTrans;
  public PXSelect<PMTaskTotal> ProjectTaskTotals;
  public PXSelect<PMBudgetAccum> ProjectBudget;
  public PXSelect<PMForecastHistoryAccum> ForecastHistory;
  public PXSelect<PMHistoryByDateAccum> ProjectHistoryByDate;
  public PXSetup<PX.Objects.EP.EPSetup> EPSetup;
  public PXSetup<PX.Objects.PR.Standalone.PRSetup> PRSetup;
  private List<PMTask> autoAllocateTasks;
  public PXAction<Batch> ViewPMTran;

  [InjectDependency]
  public IProjectMultiCurrency MultiCurrencyService { get; set; }

  [PXDBLongIdentity(IsKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.tranID> e)
  {
  }

  [PXDBString(15, IsUnicode = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.refNbr> e)
  {
  }

  [PXDBDefault(typeof (Batch.batchNbr))]
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "BatchNbr")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.batchNbr> e)
  {
  }

  [PXDBDate]
  [PXDefault(typeof (PMRegister.date))]
  public virtual void _(PX.Data.Events.CacheAttached<PMTran.date> e)
  {
  }

  [FinPeriodID(typeof (PMRegister.date), typeof (PMTran.branchID), null, null, null, null, true, false, null, null, null, true, true)]
  [PXUIField]
  public virtual void _(PX.Data.Events.CacheAttached<PMTran.finPeriodID> e)
  {
  }

  [PXDBLong]
  [CurrencyInfoDBDefault(typeof (PX.Objects.CM.CurrencyInfo.curyInfoID))]
  public virtual void _(PX.Data.Events.CacheAttached<PMTran.baseCuryInfoID> e)
  {
  }

  [PXDBLong]
  [CurrencyInfoDBDefault(typeof (PX.Objects.CM.CurrencyInfo.curyInfoID))]
  public virtual void _(PX.Data.Events.CacheAttached<PMTran.projectCuryInfoID> e)
  {
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  public virtual void Initialize()
  {
    ((PXGraph) this.Base).OnBeforePersist += new Action<PXGraph>(this.OnBeforeGraphPersist);
    ((PXGraph) this.Base).OnAfterPersist += new Action<PXGraph>(this.OnAfterGraphPersist);
  }

  private void OnBeforeGraphPersist(PXGraph obj)
  {
    this.autoAllocateTasks = this.CreateProjectTrans();
  }

  private void OnAfterGraphPersist(PXGraph obj)
  {
    ((PXGraph) this.Base).Persist(typeof (PMTask), (PXDBOperation) 1);
    List<PMTask> autoAllocateTasks = this.autoAllocateTasks;
    // ISSUE: explicit non-virtual call
    if ((autoAllocateTasks != null ? (__nonvirtual (autoAllocateTasks.Count) > 0 ? 1 : 0) : 0) == 0)
      return;
    try
    {
      this.AutoAllocateTasks(this.autoAllocateTasks);
    }
    catch (Exception ex)
    {
      object[] objArray = Array.Empty<object>();
      throw new PXException(ex, "Auto-allocation of Project Transactions failed.", objArray);
    }
  }

  protected virtual void PMRegister_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    PMRegister row = (PMRegister) e.Row;
    if (e.Operation != 2 || e.TranStatus != null)
      return;
    foreach (PMTran pmTran in ((PXSelectBase) this.ProjectTrans).Cache.Inserted)
    {
      if (pmTran.TranType == row.Module)
        pmTran.RefNbr = row.RefNbr;
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewPMTran(PXAdapter adapter)
  {
    PX.Objects.GL.GLTran current = ((PXSelectBase<PX.Objects.GL.GLTran>) this.Base.GLTranModuleBatNbr).Current;
    if (current != null && current.PMTranID.HasValue)
    {
      TransactionInquiry instance = PXGraph.CreateInstance<TransactionInquiry>();
      ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Insert().TranID = current.PMTranID;
      ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    }
    return adapter.Get();
  }

  protected virtual void AutoAllocateTasks(List<PMTask> tasks)
  {
    bool valueOrDefault = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).AutoReleaseAllocation.GetValueOrDefault();
    PMAllocator instance = PXGraph.CreateInstance<PMAllocator>();
    ((PXGraph) instance).Clear();
    ((PXGraph) instance).TimeStamp = ((PXGraph) this.Base).TimeStamp;
    instance.Execute(tasks);
    ((PXGraph) instance).Actions.PressSave();
    if (!(((PXSelectBase<PMRegister>) instance.Document).Current != null & valueOrDefault))
      return;
    List<ProcessInfo<Batch>> infoList;
    if (!RegisterRelease.ReleaseWithoutPost(new List<PMRegister>()
    {
      ((PXSelectBase<PMRegister>) instance.Document).Current
    }, false, out infoList))
      throw new PXException("Auto-release of allocated Project Transactions failed. Please try to release this document manually.");
    foreach (ProcessInfo<Batch> processInfo in infoList)
      this.Base.created.AddRange((IEnumerable<Batch>) processInfo.Batches);
  }

  protected virtual bool IsReverseTransaction(PX.Objects.GL.GLTran tran)
  {
    if (!string.IsNullOrWhiteSpace(tran.OrigBatchNbr))
    {
      int? nullable = tran.OrigLineNbr;
      if (nullable.HasValue)
      {
        PXResultset<PX.Objects.GL.GLTran> pxResultset = ((PXSelectBase<PX.Objects.GL.GLTran>) new PXSelect<PX.Objects.GL.GLTran, Where<PX.Objects.GL.GLTran.batchNbr, Equal<Required<PX.Objects.GL.GLTran.batchNbr>>, And<PX.Objects.GL.GLTran.lineNbr, Equal<Required<PX.Objects.GL.GLTran.lineNbr>>>>>((PXGraph) this.Base)).Select(new object[2]
        {
          (object) tran.OrigBatchNbr,
          (object) tran.OrigLineNbr
        });
        if (pxResultset.Count != 1)
          return false;
        PX.Objects.GL.GLTran glTran = PXResult.Unwrap<PX.Objects.GL.GLTran>((object) pxResultset[0]);
        nullable = glTran.AccountID;
        int? accountId = tran.AccountID;
        if (nullable.GetValueOrDefault() == accountId.GetValueOrDefault() & nullable.HasValue == accountId.HasValue)
        {
          Decimal? curyCreditAmt = glTran.CuryCreditAmt;
          Decimal? curyDebitAmt = tran.CuryDebitAmt;
          if (curyCreditAmt.GetValueOrDefault() == curyDebitAmt.GetValueOrDefault() & curyCreditAmt.HasValue == curyDebitAmt.HasValue)
          {
            curyDebitAmt = glTran.CuryDebitAmt;
            curyCreditAmt = tran.CuryCreditAmt;
            return curyDebitAmt.GetValueOrDefault() == curyCreditAmt.GetValueOrDefault() & curyDebitAmt.HasValue == curyCreditAmt.HasValue;
          }
        }
        return false;
      }
    }
    return false;
  }

  public virtual List<PMTask> CreateProjectTrans()
  {
    List<PMTask> projectTrans = new List<PMTask>();
    if (((PXSelectBase<Batch>) this.Base.BatchModule).Current != null && ((PXSelectBase<Batch>) this.Base.BatchModule).Current.Module != "GL")
    {
      PXResultset<PX.Objects.GL.GLTran> pxResultset = ((PXSelectBase<PX.Objects.GL.GLTran>) new PXSelect<PX.Objects.GL.GLTran, Where<PX.Objects.GL.GLTran.module, Equal<Current<Batch.module>>, And<PX.Objects.GL.GLTran.batchNbr, Equal<Current<Batch.batchNbr>>, And<PX.Objects.GL.GLTran.pMTranID, IsNull, And<PX.Objects.GL.GLTran.isNonPM, NotEqual<True>>>>>>((PXGraph) this.Base)).Select(Array.Empty<object>());
      if (pxResultset.Count > 0)
      {
        ProjectBalance projectBalance = this.CreateProjectBalance();
        Dictionary<string, PMTask> tasksToAutoAllocate = new Dictionary<string, PMTask>();
        List<PMTran> pmTranList = new List<PMTran>();
        PX.Objects.GL.GLTran glTran1 = (PX.Objects.GL.GLTran) null;
        PMRegister doc = new PMRegister();
        doc.Module = ((PXSelectBase<Batch>) this.Base.BatchModule).Current.Module;
        doc.Date = ((PXSelectBase<Batch>) this.Base.BatchModule).Current.DateEntered;
        doc.Description = ((PXSelectBase<Batch>) this.Base.BatchModule).Current.Description;
        doc.Released = new bool?(true);
        doc.Status = "R";
        bool flag1 = false;
        JournalEntryTranRef instance = PXGraph.CreateInstance<JournalEntryTranRef>();
        HashSet<string> stringSet = new HashSet<string>();
        foreach (PXResult<PX.Objects.GL.GLTran> pxResult in pxResultset)
        {
          PX.Objects.GL.GLTran glTran2 = PXResult<PX.Objects.GL.GLTran>.op_Implicit(pxResult);
          if (glTran1 == null)
            glTran1 = glTran2;
          PX.Objects.GL.Account acc = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.GLTran.accountID>>, And<PX.Objects.GL.Account.accountGroupID, IsNotNull>>>.Config>.Select((PXGraph) this.Base, new object[1]
          {
            (object) glTran2.AccountID
          }));
          if (acc != null)
          {
            PMAccountGroup ag = PXResultset<PMAccountGroup>.op_Implicit(PXSelectBase<PMAccountGroup, PXSelect<PMAccountGroup, Where<PMAccountGroup.groupID, Equal<Required<PX.Objects.GL.Account.accountGroupID>>, And<PMAccountGroup.type, NotEqual<PMAccountType.offBalance>>>>.Config>.Select((PXGraph) this.Base, new object[1]
            {
              (object) acc.AccountGroupID
            }));
            if (ag != null)
            {
              PMProject project = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Required<PX.Objects.GL.GLTran.projectID>>, And<PMProject.nonProject, Equal<False>>>>.Config>.Select((PXGraph) this.Base, new object[1]
              {
                (object) glTran2.ProjectID
              }));
              if (project != null)
              {
                PMTask task = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PX.Objects.GL.GLTran.projectID>>, And<PMTask.taskID, Equal<Required<PX.Objects.GL.GLTran.taskID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
                {
                  (object) glTran2.ProjectID,
                  (object) glTran2.TaskID
                }));
                if (task != null)
                {
                  object obj = (object) null;
                  bool flag2 = false;
                  bool flag3 = false;
                  APTran apTran = (APTran) null;
                  PX.Objects.AP.APInvoice apDoc = (PX.Objects.AP.APInvoice) null;
                  if (((PXSelectBase<Batch>) this.Base.BatchModule).Current.Module == "AP")
                  {
                    apTran = PXResultset<APTran>.op_Implicit(PXSelectBase<APTran, PXSelect<APTran, Where<APTran.refNbr, Equal<Required<PX.Objects.GL.GLTran.refNbr>>, And<APTran.lineNbr, Equal<Required<PX.Objects.GL.GLTran.tranLineNbr>>, And<APTran.tranType, Equal<Required<PX.Objects.GL.GLTran.tranType>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
                    {
                      (object) glTran2.RefNbr,
                      (object) glTran2.TranLineNbr,
                      (object) glTran2.TranType
                    }));
                    apDoc = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(PXSelectBase<PX.Objects.AP.APInvoice, PXSelect<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.GL.GLTran.tranType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.GL.GLTran.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
                    {
                      (object) glTran2.TranType,
                      (object) glTran2.RefNbr
                    }));
                    if (apTran != null)
                    {
                      string str = apTran.TranType + apTran.RefNbr;
                      if (!stringSet.Contains(str))
                      {
                        PX.Objects.AP.APRegister apRegister = PXResultset<PX.Objects.AP.APRegister>.op_Implicit(PXSelectBase<PX.Objects.AP.APRegister, PXSelect<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.refNbr, Equal<Required<APTran.refNbr>>, And<PX.Objects.AP.APRegister.docType, Equal<Required<APTran.tranType>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
                        {
                          (object) apTran.RefNbr,
                          (object) apTran.TranType
                        }));
                        if (apRegister.OrigDocType == "ECL" || apRegister.OrigDocType == "ECD")
                        {
                          obj = (object) apRegister;
                          PX.Objects.EP.EPSetup current1 = ((PXSelectBase<PX.Objects.EP.EPSetup>) this.EPSetup).Current;
                          bool? nullable;
                          int num1;
                          if (current1 == null)
                          {
                            num1 = 0;
                          }
                          else
                          {
                            nullable = current1.CopyFilesPM;
                            num1 = nullable.GetValueOrDefault() ? 1 : 0;
                          }
                          flag2 = num1 != 0;
                          PX.Objects.EP.EPSetup current2 = ((PXSelectBase<PX.Objects.EP.EPSetup>) this.EPSetup).Current;
                          int num2;
                          if (current2 == null)
                          {
                            num2 = 0;
                          }
                          else
                          {
                            nullable = current2.CopyNotesPM;
                            num2 = nullable.GetValueOrDefault() ? 1 : 0;
                          }
                          flag3 = num2 != 0;
                        }
                        stringSet.Add(str);
                      }
                    }
                  }
                  ARTran arTran = (ARTran) null;
                  PX.Objects.AR.ARInvoice arDoc = (PX.Objects.AR.ARInvoice) null;
                  if (((PXSelectBase<Batch>) this.Base.BatchModule).Current.Module == "AR")
                  {
                    arTran = PXResultset<ARTran>.op_Implicit(PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.refNbr, Equal<Required<PX.Objects.GL.GLTran.refNbr>>, And<ARTran.lineNbr, Equal<Required<PX.Objects.GL.GLTran.tranLineNbr>>, And<ARTran.tranType, Equal<Required<PX.Objects.GL.GLTran.tranType>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
                    {
                      (object) glTran2.RefNbr,
                      (object) glTran2.TranLineNbr,
                      (object) glTran2.TranType
                    }));
                    arDoc = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<ARRegister.docType, Equal<Required<PX.Objects.GL.GLTran.tranType>>, And<ARRegister.refNbr, Equal<Required<PX.Objects.GL.GLTran.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
                    {
                      (object) glTran2.TranType,
                      (object) glTran2.RefNbr
                    }));
                  }
                  if (!flag1)
                  {
                    doc = ((PXSelectBase<PMRegister>) this.ProjectDocs).Insert(doc);
                    flag1 = true;
                  }
                  doc.OrigDocType = instance.GetDocType(apDoc, arDoc, glTran2);
                  doc.OrigNoteID = instance.GetNoteID(apDoc, arDoc, glTran2);
                  if (obj != null)
                    PXNoteAttribute.CopyNoteAndFiles(((PXGraph) this.Base).Caches[obj.GetType()], obj, ((PXSelectBase) this.ProjectDocs).Cache, (object) doc, new bool?(flag3), new bool?(flag2));
                  PMTran pmTran = this.InsertProjectTransaction(project, task, ag, acc, glTran2, apTran, arTran);
                  if (this.IsReverseTransaction(glTran2))
                  {
                    bool flag4 = glTran2.Module == "PR" && ((PXSelectBase<PX.Objects.PR.Standalone.PRSetup>) this.PRSetup).Current?.TimePostingOption == "G";
                    pmTran.ExcludedFromAllocation = new bool?(!flag4);
                    pmTran.ExcludedFromBilling = new bool?(!flag4);
                  }
                  instance.AssignCustomerVendorEmployee(glTran2, pmTran);
                  ((PXSelectBase<PX.Objects.GL.GLTran>) this.Base.GLTranModuleBatNbr).SetValueExt<PX.Objects.GL.GLTran.pMTranID>(glTran2, (object) pmTran.TranID);
                  this.ProcessProjectTransaction(projectBalance, tasksToAutoAllocate, pmTran, acc, ag, project, task, arTran);
                  pmTranList.Add(pmTran);
                  instance.AssignAdditionalFields(glTran2, pmTran);
                  this.AddTranAmountAndQty(doc, pmTran);
                }
              }
            }
          }
        }
        foreach (TranWithInfo additionalProjectTran in instance.GetAdditionalProjectTrans(glTran1.Module, glTran1.TranType, glTran1.RefNbr))
        {
          if (!flag1)
          {
            doc = ((PXSelectBase<PMRegister>) this.ProjectDocs).Insert(doc);
            doc.OrigDocType = instance.GetDocType((PX.Objects.AP.APInvoice) null, (PX.Objects.AR.ARInvoice) null, glTran1);
            doc.OrigNoteID = instance.GetNoteID((PX.Objects.AP.APInvoice) null, (PX.Objects.AR.ARInvoice) null, glTran1);
            flag1 = true;
          }
          ((PXSelectBase) this.ProjectTrans).Cache.Insert((object) additionalProjectTran.Tran);
          this.ProcessProjectTransaction(projectBalance, tasksToAutoAllocate, additionalProjectTran.Tran, additionalProjectTran.Account, additionalProjectTran.AccountGroup, additionalProjectTran.Project, additionalProjectTran.Task, (ARTran) null);
          this.AddTranAmountAndQty(doc, additionalProjectTran.Tran);
        }
        projectTrans.AddRange((IEnumerable<PMTask>) tasksToAutoAllocate.Values);
      }
    }
    return projectTrans;
  }

  protected virtual void AddTranAmountAndQty(PMRegister doc, PMTran pmt)
  {
    PMRegister pmRegister1 = doc;
    Decimal? nullable1 = doc.AmtTotal;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = pmt.Amount;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal? nullable2 = new Decimal?(valueOrDefault1 + valueOrDefault2);
    pmRegister1.AmtTotal = nullable2;
    PMRegister pmRegister2 = doc;
    Decimal? nullable3 = doc.QtyTotal;
    Decimal valueOrDefault3 = nullable3.GetValueOrDefault();
    nullable3 = pmt.Qty;
    Decimal valueOrDefault4 = nullable3.GetValueOrDefault();
    Decimal? nullable4 = new Decimal?(valueOrDefault3 + valueOrDefault4);
    pmRegister2.QtyTotal = nullable4;
    PMRegister pmRegister3 = doc;
    Decimal? nullable5 = doc.BillableQtyTotal;
    Decimal valueOrDefault5 = nullable5.GetValueOrDefault();
    nullable5 = pmt.BillableQty;
    Decimal valueOrDefault6 = nullable5.GetValueOrDefault();
    Decimal? nullable6 = new Decimal?(valueOrDefault5 + valueOrDefault6);
    pmRegister3.BillableQtyTotal = nullable6;
  }

  public virtual (Decimal? CuryAmount, Decimal? Amount) GetInclusiveTaxAmount(
    PXGraph graph,
    ARTran tran)
  {
    return ProjectRevenueTaxAmountProvider.GetInclusiveTaxAmount(graph, tran);
  }

  public virtual (Decimal? CuryAmount, Decimal? Amount) GetRetainedInclusiveTaxAmount(
    PXGraph graph,
    ARTran tran)
  {
    return ProjectRevenueTaxAmountProvider.GetRetainedInclusiveTaxAmount(graph, tran);
  }

  public virtual void ProcessProjectTransaction(
    ProjectBalance pb,
    Dictionary<string, PMTask> tasksToAutoAllocate,
    PMTran pmt,
    PX.Objects.GL.Account acc,
    PMAccountGroup ag,
    PMProject project,
    PMTask task,
    ARTran arTran)
  {
    int amountSign = 1;
    if (ag?.Type == "I" || ag?.Type == "L")
      amountSign = -1;
    ProjectBalance.Result result = pb.Calculate(project, pmt, ag, acc?.Type, amountSign, 1);
    Decimal? nullable1 = ARDocType.SignAmount(arTran?.TranType);
    (Decimal? CuryAmount, Decimal? Amount) inclusiveTaxAmount1 = this.GetInclusiveTaxAmount((PXGraph) this.Base, arTran);
    (Decimal? CuryAmount, Decimal? Amount) inclusiveTaxAmount2 = this.GetRetainedInclusiveTaxAmount((PXGraph) this.Base, arTran);
    ARTran tran1 = arTran;
    PMProject project1 = project;
    Decimal? nullable2 = inclusiveTaxAmount1.CuryAmount;
    Decimal? nullable3 = inclusiveTaxAmount2.CuryAmount;
    Decimal? nullable4 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable5 = this.GetAmountInProjectCurrency(tran1, project1, nullable4);
    Decimal? nullable6 = nullable1;
    Decimal? nullable7;
    if (!(nullable5.HasValue & nullable6.HasValue))
    {
      nullable3 = new Decimal?();
      nullable7 = nullable3;
    }
    else
      nullable7 = new Decimal?(nullable5.GetValueOrDefault() * nullable6.GetValueOrDefault());
    Decimal? nullable8 = nullable7;
    nullable3 = inclusiveTaxAmount1.Amount;
    nullable2 = inclusiveTaxAmount2.Amount;
    nullable6 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    nullable5 = nullable1;
    Decimal? nullable9;
    if (!(nullable6.HasValue & nullable5.HasValue))
    {
      nullable2 = new Decimal?();
      nullable9 = nullable2;
    }
    else
      nullable9 = new Decimal?(nullable6.GetValueOrDefault() * nullable5.GetValueOrDefault());
    Decimal? nullable10 = nullable9;
    if (result.Status != null)
    {
      PMBudgetAccum budget = this.ExtractBudget(result.Status, pmt);
      if (budget.Type == "E" && task != null && task.Type == "CostRev")
        budget.RevenueTaskID = budget.ProjectTaskID;
      PMBudgetAccum pmBudgetAccum1 = ((PXSelectBase<PMBudgetAccum>) this.ProjectBudget).Insert(budget);
      PMBudgetAccum pmBudgetAccum2 = pmBudgetAccum1;
      nullable5 = pmBudgetAccum2.ActualQty;
      nullable6 = result.Status.ActualQty;
      Decimal valueOrDefault1 = nullable6.GetValueOrDefault();
      Decimal? nullable11;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable11 = nullable6;
      }
      else
        nullable11 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault1);
      pmBudgetAccum2.ActualQty = nullable11;
      PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum1;
      nullable5 = pmBudgetAccum3.CuryActualAmount;
      nullable6 = result.Status.CuryActualAmount;
      Decimal valueOrDefault2 = nullable6.GetValueOrDefault();
      Decimal? nullable12;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable12 = nullable6;
      }
      else
        nullable12 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault2);
      pmBudgetAccum3.CuryActualAmount = nullable12;
      PMBudgetAccum pmBudgetAccum4 = pmBudgetAccum1;
      nullable5 = pmBudgetAccum4.ActualAmount;
      nullable6 = result.Status.ActualAmount;
      Decimal valueOrDefault3 = nullable6.GetValueOrDefault();
      Decimal? nullable13;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable13 = nullable6;
      }
      else
        nullable13 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault3);
      pmBudgetAccum4.ActualAmount = nullable13;
      PMBudgetAccum pmBudgetAccum5 = pmBudgetAccum1;
      nullable5 = pmBudgetAccum5.CuryInclTaxAmount;
      Decimal valueOrDefault4 = nullable8.GetValueOrDefault();
      Decimal? nullable14;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable14 = nullable6;
      }
      else
        nullable14 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault4);
      pmBudgetAccum5.CuryInclTaxAmount = nullable14;
      PMBudgetAccum pmBudgetAccum6 = pmBudgetAccum1;
      nullable5 = pmBudgetAccum6.InclTaxAmount;
      Decimal valueOrDefault5 = nullable10.GetValueOrDefault();
      Decimal? nullable15;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable15 = nullable6;
      }
      else
        nullable15 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault5);
      pmBudgetAccum6.InclTaxAmount = nullable15;
      int? nullable16;
      if (arTran != null)
      {
        nullable16 = arTran.LineNbr;
        if (nullable16.HasValue && ag.Type == "I" && task.Type != "Cost")
        {
          ARTran tran2 = arTran;
          PMProject project2 = project;
          nullable6 = arTran.CuryTranAmt;
          Decimal valueOrDefault6 = nullable6.GetValueOrDefault();
          nullable6 = arTran.CuryRetainageAmt;
          Decimal valueOrDefault7 = nullable6.GetValueOrDefault();
          Decimal num1 = valueOrDefault6 + valueOrDefault7;
          nullable5 = nullable1;
          Decimal? nullable17;
          if (!nullable5.HasValue)
          {
            nullable6 = new Decimal?();
            nullable17 = nullable6;
          }
          else
            nullable17 = new Decimal?(num1 * nullable5.GetValueOrDefault());
          Decimal? inProjectCurrency = this.GetAmountInProjectCurrency(tran2, project2, nullable17);
          nullable5 = result.Status.CuryActualAmount;
          int num2 = nullable5.GetValueOrDefault() < 0M ? -1 : 1;
          PMBudgetAccum pmBudgetAccum7 = pmBudgetAccum1;
          nullable5 = pmBudgetAccum7.CuryInvoicedAmount;
          Decimal num3 = (Decimal) num2 * Math.Abs(inProjectCurrency.GetValueOrDefault());
          Decimal? nullable18;
          if (!nullable5.HasValue)
          {
            nullable6 = new Decimal?();
            nullable18 = nullable6;
          }
          else
            nullable18 = new Decimal?(nullable5.GetValueOrDefault() - num3);
          pmBudgetAccum7.CuryInvoicedAmount = nullable18;
          PMBudgetAccum pmBudgetAccum8 = pmBudgetAccum1;
          nullable5 = pmBudgetAccum8.InvoicedAmount;
          Decimal num4 = (Decimal) num2;
          nullable6 = arTran.TranAmt;
          Decimal valueOrDefault8 = nullable6.GetValueOrDefault();
          nullable6 = arTran.RetainageAmt;
          Decimal valueOrDefault9 = nullable6.GetValueOrDefault();
          Decimal num5 = Math.Abs(valueOrDefault8 + valueOrDefault9);
          Decimal num6 = num4 * num5;
          Decimal? nullable19;
          if (!nullable5.HasValue)
          {
            nullable6 = new Decimal?();
            nullable19 = nullable6;
          }
          else
            nullable19 = new Decimal?(nullable5.GetValueOrDefault() - num6);
          pmBudgetAccum8.InvoicedAmount = nullable19;
          PMBudgetAccum pmBudgetAccum9 = pmBudgetAccum1;
          nullable5 = pmBudgetAccum9.InvoicedQty;
          nullable6 = result.Status.ActualQty;
          Decimal valueOrDefault10 = nullable6.GetValueOrDefault();
          Decimal? nullable20;
          if (!nullable5.HasValue)
          {
            nullable6 = new Decimal?();
            nullable20 = nullable6;
          }
          else
            nullable20 = new Decimal?(nullable5.GetValueOrDefault() - valueOrDefault10);
          pmBudgetAccum9.InvoicedQty = nullable20;
        }
      }
      PMHistoryByDateAccum historyByDateAccum1 = new PMHistoryByDateAccum();
      historyByDateAccum1.ProjectID = pmt.ProjectID;
      historyByDateAccum1.ProjectTaskID = pmt.TaskID;
      PMHistoryByDateAccum historyByDateAccum2 = historyByDateAccum1;
      nullable16 = pmt.AccountGroupID;
      int? nullable21 = nullable16 ?? pmBudgetAccum1.AccountGroupID;
      historyByDateAccum2.AccountGroupID = nullable21;
      PMHistoryByDateAccum historyByDateAccum3 = historyByDateAccum1;
      nullable16 = pmt.InventoryID;
      int? nullable22 = nullable16 ?? pmBudgetAccum1.InventoryID;
      historyByDateAccum3.InventoryID = nullable22;
      PMHistoryByDateAccum historyByDateAccum4 = historyByDateAccum1;
      nullable16 = pmt.CostCodeID;
      int? nullable23 = nullable16 ?? pmBudgetAccum1.CostCodeID;
      historyByDateAccum4.CostCodeID = nullable23;
      historyByDateAccum1.Date = pmt.Date;
      historyByDateAccum1.PeriodID = pmt.TranPeriodID;
      PMHistoryByDateAccum historyByDateAccum5 = ((PXSelectBase<PMHistoryByDateAccum>) this.ProjectHistoryByDate).Insert(historyByDateAccum1);
      PMHistoryByDateAccum historyByDateAccum6 = historyByDateAccum5;
      nullable5 = historyByDateAccum6.ActualQty;
      nullable6 = result.Status.ActualQty;
      Decimal valueOrDefault11 = nullable6.GetValueOrDefault();
      Decimal? nullable24;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable24 = nullable6;
      }
      else
        nullable24 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault11);
      historyByDateAccum6.ActualQty = nullable24;
      PMHistoryByDateAccum historyByDateAccum7 = historyByDateAccum5;
      nullable5 = historyByDateAccum7.CuryActualAmount;
      nullable6 = result.Status.CuryActualAmount;
      Decimal valueOrDefault12 = nullable6.GetValueOrDefault();
      Decimal? nullable25;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable25 = nullable6;
      }
      else
        nullable25 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault12);
      historyByDateAccum7.CuryActualAmount = nullable25;
      PMHistoryByDateAccum historyByDateAccum8 = historyByDateAccum5;
      nullable5 = historyByDateAccum8.ActualAmount;
      nullable6 = result.Status.ActualAmount;
      Decimal valueOrDefault13 = nullable6.GetValueOrDefault();
      Decimal? nullable26;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable26 = nullable6;
      }
      else
        nullable26 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault13);
      historyByDateAccum8.ActualAmount = nullable26;
    }
    if (result.ForecastHistory != null)
    {
      PMForecastHistoryAccum forecastHistoryAccum1 = new PMForecastHistoryAccum();
      forecastHistoryAccum1.ProjectID = result.ForecastHistory.ProjectID;
      forecastHistoryAccum1.ProjectTaskID = result.ForecastHistory.ProjectTaskID;
      forecastHistoryAccum1.AccountGroupID = result.ForecastHistory.AccountGroupID;
      forecastHistoryAccum1.InventoryID = result.ForecastHistory.InventoryID;
      forecastHistoryAccum1.CostCodeID = result.ForecastHistory.CostCodeID;
      forecastHistoryAccum1.PeriodID = result.ForecastHistory.PeriodID;
      PMForecastHistoryAccum forecastHistoryAccum2 = ((PXSelectBase<PMForecastHistoryAccum>) this.ForecastHistory).Insert(forecastHistoryAccum1);
      PMForecastHistoryAccum forecastHistoryAccum3 = forecastHistoryAccum2;
      nullable5 = forecastHistoryAccum3.ActualQty;
      nullable6 = result.ForecastHistory.ActualQty;
      Decimal valueOrDefault14 = nullable6.GetValueOrDefault();
      Decimal? nullable27;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable27 = nullable6;
      }
      else
        nullable27 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault14);
      forecastHistoryAccum3.ActualQty = nullable27;
      PMForecastHistoryAccum forecastHistoryAccum4 = forecastHistoryAccum2;
      nullable5 = forecastHistoryAccum4.CuryActualAmount;
      nullable6 = result.ForecastHistory.CuryActualAmount;
      Decimal valueOrDefault15 = nullable6.GetValueOrDefault();
      Decimal? nullable28;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable28 = nullable6;
      }
      else
        nullable28 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault15);
      forecastHistoryAccum4.CuryActualAmount = nullable28;
      PMForecastHistoryAccum forecastHistoryAccum5 = forecastHistoryAccum2;
      nullable5 = forecastHistoryAccum5.ActualAmount;
      nullable6 = result.ForecastHistory.ActualAmount;
      Decimal valueOrDefault16 = nullable6.GetValueOrDefault();
      Decimal? nullable29;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable29 = nullable6;
      }
      else
        nullable29 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault16);
      forecastHistoryAccum5.ActualAmount = nullable29;
      PMForecastHistoryAccum forecastHistoryAccum6 = forecastHistoryAccum2;
      nullable5 = forecastHistoryAccum6.CuryInclTaxAmount;
      Decimal valueOrDefault17 = nullable8.GetValueOrDefault();
      Decimal? nullable30;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable30 = nullable6;
      }
      else
        nullable30 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault17);
      forecastHistoryAccum6.CuryInclTaxAmount = nullable30;
      PMForecastHistoryAccum forecastHistoryAccum7 = forecastHistoryAccum2;
      nullable5 = forecastHistoryAccum7.InclTaxAmount;
      Decimal valueOrDefault18 = nullable10.GetValueOrDefault();
      Decimal? nullable31;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable31 = nullable6;
      }
      else
        nullable31 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault18);
      forecastHistoryAccum7.InclTaxAmount = nullable31;
      PMForecastHistoryAccum forecastHistoryAccum8 = forecastHistoryAccum2;
      nullable5 = forecastHistoryAccum8.CuryArAmount;
      nullable6 = result.ForecastHistory.CuryArAmount;
      Decimal valueOrDefault19 = nullable6.GetValueOrDefault();
      Decimal? nullable32;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable32 = nullable6;
      }
      else
        nullable32 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault19);
      forecastHistoryAccum8.CuryArAmount = nullable32;
    }
    if (result.TaskTotal != null)
    {
      PMTaskTotal pmTaskTotal1 = ((PXSelectBase<PMTaskTotal>) this.ProjectTaskTotals).Insert(new PMTaskTotal()
      {
        ProjectID = result.TaskTotal.ProjectID,
        TaskID = result.TaskTotal.TaskID
      });
      PMTaskTotal pmTaskTotal2 = pmTaskTotal1;
      nullable5 = pmTaskTotal2.CuryAsset;
      nullable6 = result.TaskTotal.CuryAsset;
      Decimal valueOrDefault20 = nullable6.GetValueOrDefault();
      Decimal? nullable33;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable33 = nullable6;
      }
      else
        nullable33 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault20);
      pmTaskTotal2.CuryAsset = nullable33;
      PMTaskTotal pmTaskTotal3 = pmTaskTotal1;
      nullable5 = pmTaskTotal3.Asset;
      nullable6 = result.TaskTotal.Asset;
      Decimal valueOrDefault21 = nullable6.GetValueOrDefault();
      Decimal? nullable34;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable34 = nullable6;
      }
      else
        nullable34 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault21);
      pmTaskTotal3.Asset = nullable34;
      PMTaskTotal pmTaskTotal4 = pmTaskTotal1;
      nullable5 = pmTaskTotal4.CuryLiability;
      nullable6 = result.TaskTotal.CuryLiability;
      Decimal valueOrDefault22 = nullable6.GetValueOrDefault();
      Decimal? nullable35;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable35 = nullable6;
      }
      else
        nullable35 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault22);
      pmTaskTotal4.CuryLiability = nullable35;
      PMTaskTotal pmTaskTotal5 = pmTaskTotal1;
      nullable5 = pmTaskTotal5.Liability;
      nullable6 = result.TaskTotal.Liability;
      Decimal valueOrDefault23 = nullable6.GetValueOrDefault();
      Decimal? nullable36;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable36 = nullable6;
      }
      else
        nullable36 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault23);
      pmTaskTotal5.Liability = nullable36;
      PMTaskTotal pmTaskTotal6 = pmTaskTotal1;
      nullable5 = pmTaskTotal6.CuryIncome;
      nullable6 = result.TaskTotal.CuryIncome;
      Decimal valueOrDefault24 = nullable6.GetValueOrDefault();
      Decimal? nullable37;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable37 = nullable6;
      }
      else
        nullable37 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault24);
      pmTaskTotal6.CuryIncome = nullable37;
      PMTaskTotal pmTaskTotal7 = pmTaskTotal1;
      nullable5 = pmTaskTotal7.Income;
      nullable6 = result.TaskTotal.Income;
      Decimal valueOrDefault25 = nullable6.GetValueOrDefault();
      Decimal? nullable38;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable38 = nullable6;
      }
      else
        nullable38 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault25);
      pmTaskTotal7.Income = nullable38;
      PMTaskTotal pmTaskTotal8 = pmTaskTotal1;
      nullable5 = pmTaskTotal8.CuryExpense;
      nullable6 = result.TaskTotal.CuryExpense;
      Decimal valueOrDefault26 = nullable6.GetValueOrDefault();
      Decimal? nullable39;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable39 = nullable6;
      }
      else
        nullable39 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault26);
      pmTaskTotal8.CuryExpense = nullable39;
      PMTaskTotal pmTaskTotal9 = pmTaskTotal1;
      nullable5 = pmTaskTotal9.Expense;
      nullable6 = result.TaskTotal.Expense;
      Decimal valueOrDefault27 = nullable6.GetValueOrDefault();
      Decimal? nullable40;
      if (!nullable5.HasValue)
      {
        nullable6 = new Decimal?();
        nullable40 = nullable6;
      }
      else
        nullable40 = new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault27);
      pmTaskTotal9.Expense = nullable40;
    }
    RegisterReleaseProcess.AddToUnbilledSummary((PXGraph) this.Base, pmt);
    bool? nullable41 = pmt.Allocated;
    if (nullable41.GetValueOrDefault())
      return;
    nullable41 = pmt.ExcludedFromAllocation;
    if (nullable41.GetValueOrDefault())
      return;
    nullable41 = project.AutoAllocate;
    if (!nullable41.GetValueOrDefault() || tasksToAutoAllocate.ContainsKey($"{task.ProjectID}.{task.TaskID}"))
      return;
    tasksToAutoAllocate.Add($"{task.ProjectID}.{task.TaskID}", task);
  }

  public virtual PMTran InsertProjectTransaction(
    PMProject project,
    PMTask task,
    PMAccountGroup ag,
    PX.Objects.GL.Account acc,
    PX.Objects.GL.GLTran tran,
    APTran apTran,
    ARTran arTran)
  {
    PMTran pmt = (PMTran) ((PXSelectBase) this.ProjectTrans).Cache.Insert();
    pmt.BranchID = tran.BranchID;
    pmt.AccountGroupID = acc.AccountGroupID;
    pmt.AccountID = tran.AccountID;
    pmt.SubID = tran.SubID;
    pmt.Date = tran.TranDate;
    pmt.TranDate = tran.TranDate;
    pmt.Description = StringExtensions.Truncate(tran.TranDesc, 256 /*0x0100*/);
    pmt.FinPeriodID = tran.FinPeriodID;
    pmt.TranPeriodID = tran.TranPeriodID;
    pmt.InventoryID = new int?(tran.InventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID);
    pmt.OrigLineNbr = tran.TranLineNbr;
    pmt.OrigModule = tran.Module;
    pmt.OrigRefNbr = tran.RefNbr;
    pmt.OrigTranType = tran.TranType;
    pmt.ProjectID = tran.ProjectID;
    pmt.TaskID = tran.TaskID;
    pmt.CostCodeID = tran.CostCodeID;
    if (arTran != null)
    {
      pmt.Billable = new bool?(false);
      pmt.ExcludedFromBilling = new bool?(true);
      PMTran pmTran = pmt;
      string str;
      if (!(arTran.TranType == "CRM"))
        str = PXMessages.LocalizeFormatNoPrefix("Result of AR Invoice {0}", new object[1]
        {
          (object) arTran.RefNbr
        });
      else
        str = PXMessages.LocalizeFormatNoPrefix("Result of Credit Memo {0}", new object[1]
        {
          (object) arTran.RefNbr
        });
      pmTran.ExcludedFromBillingReason = str;
    }
    else
      pmt.Billable = new bool?(!tran.NonBillable.GetValueOrDefault());
    pmt.Released = new bool?(true);
    if (apTran != null)
    {
      if (apTran.Date.HasValue)
        pmt.Date = apTran.Date;
      PMTran pmTran = PXResultset<PMTran>.op_Implicit(((PXSelectBase<PMTran>) new PXSelectReadonly<PMTran, Where<PMTran.origModule, Equal<Required<PMTran.origModule>>, And<PMTran.origTranType, Equal<Required<PMTran.origTranType>>, And<PMTran.origRefNbr, Equal<Required<PMTran.origRefNbr>>, And<PMTran.origLineNbr, Equal<Required<PMTran.origLineNbr>>>>>>, OrderBy<Desc<PMTran.tranID>>>((PXGraph) this.Base)).SelectWindowed(0, 1, new object[4]
      {
        (object) "AP",
        (object) apTran.TranType,
        (object) apTran.RefNbr,
        (object) apTran.LineNbr
      }));
      if (pmTran != null)
      {
        pmt.ProformaRefNbr = pmTran.ProformaRefNbr;
        pmt.ProformaLineNbr = pmTran.ProformaLineNbr;
      }
    }
    pmt.UseBillableQty = new bool?(true);
    pmt.UOM = tran.UOM;
    this.MultiCurrencyService.CalculateCurrencyValues((PXGraph) this.Base, tran, pmt, ((PXSelectBase<Batch>) this.Base.BatchModule).Current, project, PX.Objects.GL.Ledger.PK.Find((PXGraph) this.Base, ((PXSelectBase<Batch>) this.Base.BatchModule).Current.LedgerID));
    pmt.Qty = tran.Qty;
    if (ProjectBalance.IsFlipRequired(acc.Type, ag.Type))
    {
      PMTran pmTran1 = pmt;
      Decimal? nullable1 = pmt.ProjectCuryAmount;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      pmTran1.ProjectCuryAmount = nullable2;
      PMTran pmTran2 = pmt;
      nullable1 = pmt.TranCuryAmount;
      Decimal? nullable3 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      pmTran2.TranCuryAmount = nullable3;
      PMTran pmTran3 = pmt;
      nullable1 = pmt.Amount;
      Decimal? nullable4 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      pmTran3.Amount = nullable4;
      PMTran pmTran4 = pmt;
      nullable1 = pmt.Qty;
      Decimal? nullable5 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      pmTran4.Qty = nullable5;
    }
    pmt.BillableQty = pmt.Qty;
    if (apTran != null && apTran.NoteID.HasValue)
      PXNoteAttribute.CopyNoteAndFiles(((PXGraph) this.Base).Caches[typeof (APTran)], (object) apTran, ((PXSelectBase) this.ProjectTrans).Cache, (object) pmt, (PXNoteAttribute.IPXCopySettings) null);
    else if (arTran != null && arTran.NoteID.HasValue)
      PXNoteAttribute.CopyNoteAndFiles(((PXGraph) this.Base).Caches[typeof (ARTran)], (object) arTran, ((PXSelectBase) this.ProjectTrans).Cache, (object) pmt, (PXNoteAttribute.IPXCopySettings) null);
    ((PXSelectBase) this.ProjectTrans).Cache.GetExtension<PX.Objects.Extensions.MultiCurrency.Document>((object) pmt).CuryInfoID = pmt.BaseCuryInfoID;
    return pmt;
  }

  public virtual PMBudgetAccum ExtractBudget(PMBudget targetBudget, PMTran tran)
  {
    PMBudgetAccum budget = new PMBudgetAccum();
    budget.ProjectID = targetBudget.ProjectID;
    budget.ProjectTaskID = targetBudget.ProjectTaskID;
    budget.AccountGroupID = targetBudget.AccountGroupID;
    budget.InventoryID = targetBudget.InventoryID;
    budget.CostCodeID = targetBudget.CostCodeID;
    budget.UOM = targetBudget.UOM;
    budget.IsProduction = targetBudget.IsProduction;
    budget.Type = targetBudget.Type;
    budget.Description = targetBudget.Description;
    budget.CuryInfoID = targetBudget.CuryInfoID;
    return budget;
  }

  private Decimal? GetAmountInProjectCurrency(ARTran tran, PMProject project, Decimal? value)
  {
    if (tran == null)
      return new Decimal?();
    PX.Objects.AR.ARInvoice arInvoice = PX.Objects.AR.ARInvoice.PK.Find((PXGraph) this.Base, tran.TranType, tran.RefNbr);
    return new Decimal?(this.MultiCurrencyService.GetValueInProjectCurrency((PXGraph) this.Base, project, arInvoice.CuryID, arInvoice.DocDate, value));
  }

  public virtual ProjectBalance CreateProjectBalance() => new ProjectBalance((PXGraph) this.Base);

  public abstract class baseCuryInfoID : IBqlField, IBqlOperand
  {
  }

  public abstract class projectCuryInfoID : IBqlField, IBqlOperand
  {
  }
}

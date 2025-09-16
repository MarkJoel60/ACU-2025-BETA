// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PostGraphExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Extensions;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PM.GraphExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

public class PostGraphExt : PXGraphExtension<
#nullable disable
PostGraph>
{
  public PXSelect<PMRegister> ProjectDocs;
  public PXSelect<PMTran> ProjectTrans;
  public PXSelect<PMHistoryAccum> ProjectHistory;
  private Dictionary<string, PMTask> tasksToAutoAllocate = new Dictionary<string, PMTask>();
  private List<Batch> created = new List<Batch>();

  [InjectDependency]
  public IProjectMultiCurrency MultiCurrencyService { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXDBLongIdentity(IsKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.tranID> e)
  {
  }

  [PXDBDefault(typeof (PMRegister.refNbr))]
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

  [PXMergeAttributes]
  [OpenPeriod(null, typeof (PMTran.date), typeof (PMTran.branchID), null, null, null, null, false, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (PMTran.tranPeriodID), false)]
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

  [PXOverride]
  public virtual void UpdateAllocationBalance(Batch b) => this.UpdateProjectBalance(b);

  [PXOverride]
  public virtual void ReleaseBatchProc(Batch b, bool unholdBatch, Action<Batch, bool> baseMethod)
  {
    this.tasksToAutoAllocate.Clear();
    this.created.Clear();
    baseMethod(b, unholdBatch);
    if (this.tasksToAutoAllocate.Count > 0)
    {
      try
      {
        this.AutoAllocateTasks(new List<PMTask>((IEnumerable<PMTask>) this.tasksToAutoAllocate.Values));
      }
      catch (Exception ex)
      {
        object[] objArray = Array.Empty<object>();
        throw new PXException(ex, "Auto-allocation of Project Transactions failed.", objArray);
      }
    }
    if (!this.Base.AutoPost)
      return;
    foreach (Batch b1 in this.created)
      this.Base.PostBatchProc(b1);
  }

  [PXOverride]
  public virtual void CreateProjectTransactions(Batch b)
  {
    ProjectBalance projectBalance = this.CreateProjectBalance();
    if (!(b.Module == "GL"))
      return;
    PXResultset<PX.Objects.GL.GLTran> glTrans = ((PXSelectBase<PX.Objects.GL.GLTran>) new PXSelectJoin<PX.Objects.GL.GLTran, InnerJoin<PX.Objects.GL.Account, On<PX.Objects.GL.GLTran.accountID, Equal<PX.Objects.GL.Account.accountID>>, InnerJoin<PMAccountGroup, On<PMAccountGroup.groupID, Equal<PX.Objects.GL.Account.accountGroupID>>, InnerJoin<PMProject, On<PMProject.contractID, Equal<PX.Objects.GL.GLTran.projectID>, And<PMProject.nonProject, Equal<False>>>, InnerJoin<PMTask, On<PMTask.projectID, Equal<PX.Objects.GL.GLTran.projectID>, And<PMTask.taskID, Equal<PX.Objects.GL.GLTran.taskID>>>>>>>, Where<PX.Objects.GL.GLTran.module, Equal<BatchModule.moduleGL>, And<PX.Objects.GL.GLTran.batchNbr, Equal<Required<PX.Objects.GL.GLTran.batchNbr>>, And<PX.Objects.GL.Account.accountGroupID, IsNotNull, And<PX.Objects.GL.GLTran.isNonPM, NotEqual<True>>>>>>((PXGraph) this.Base)).Select(new object[1]
    {
      (object) b.BatchNbr
    });
    if (glTrans.Count > 0)
    {
      PMRegister doc = new PMRegister();
      doc.Module = b.Module;
      doc.Date = b.DateEntered;
      doc.Description = b.Description;
      doc.Released = new bool?(true);
      doc.Status = "R";
      this.SetOrigDocLink(doc, glTrans);
      PMRegister pmRegister1 = ((PXSelectBase<PMRegister>) this.ProjectDocs).Insert(doc);
      ((PXSelectBase) this.ProjectDocs).Cache.Persist((PXDBOperation) 2);
      foreach (PXResult<PX.Objects.GL.GLTran, PX.Objects.GL.Account, PMAccountGroup, PMProject, PMTask> pxResult in glTrans)
      {
        PX.Objects.GL.GLTran tran = PXResult<PX.Objects.GL.GLTran, PX.Objects.GL.Account, PMAccountGroup, PMProject, PMTask>.op_Implicit(pxResult);
        PX.Objects.GL.Account account = PXResult<PX.Objects.GL.GLTran, PX.Objects.GL.Account, PMAccountGroup, PMProject, PMTask>.op_Implicit(pxResult);
        PMAccountGroup ag = PXResult<PX.Objects.GL.GLTran, PX.Objects.GL.Account, PMAccountGroup, PMProject, PMTask>.op_Implicit(pxResult);
        PMProject project = PXResult<PX.Objects.GL.GLTran, PX.Objects.GL.Account, PMAccountGroup, PMProject, PMTask>.op_Implicit(pxResult);
        PMTask pmTask = PXResult<PX.Objects.GL.GLTran, PX.Objects.GL.Account, PMAccountGroup, PMProject, PMTask>.op_Implicit(pxResult);
        PMTran pmTran1 = (PMTran) ((PXSelectBase) this.ProjectTrans).Cache.Insert();
        pmTran1.BranchID = tran.BranchID;
        pmTran1.AccountGroupID = account.AccountGroupID;
        pmTran1.AccountID = tran.AccountID;
        pmTran1.SubID = tran.SubID;
        pmTran1.BAccountID = tran.ReferenceID;
        pmTran1.BatchNbr = tran.BatchNbr;
        pmTran1.Date = tran.TranDate;
        pmTran1.Description = StringExtensions.Truncate(tran.TranDesc, 256 /*0x0100*/);
        pmTran1.FinPeriodID = tran.FinPeriodID;
        pmTran1.TranPeriodID = tran.TranPeriodID;
        pmTran1.InventoryID = tran.InventoryID;
        pmTran1.OrigLineNbr = tran.LineNbr;
        pmTran1.OrigModule = tran.Module;
        pmTran1.OrigRefNbr = tran.RefNbr;
        pmTran1.OrigTranType = tran.TranType;
        pmTran1.ProjectID = tran.ProjectID;
        pmTran1.TaskID = tran.TaskID;
        pmTran1.CostCodeID = tran.CostCodeID;
        PMTran pmTran2 = pmTran1;
        bool? nullable1 = tran.NonBillable;
        bool? nullable2 = new bool?(!nullable1.GetValueOrDefault());
        pmTran2.Billable = nullable2;
        pmTran1.UseBillableQty = new bool?(true);
        pmTran1.UOM = tran.UOM;
        this.MultiCurrencyService.CalculateCurrencyValues((PXGraph) this.Base, tran, pmTran1, ((PXSelectBase<Batch>) this.Base.BatchModule).Current, project, PXResultset<PX.Objects.GL.Ledger>.op_Implicit(((PXSelectBase<PX.Objects.GL.Ledger>) this.Base.Ledger_LedgerID).Select(Array.Empty<object>())));
        pmTran1.Qty = tran.Qty;
        int amountSign = 1;
        if (account.Type == "I" || account.Type == "L")
          amountSign = -1;
        Decimal? nullable3;
        Decimal? nullable4;
        if (ProjectBalance.IsFlipRequired(account.Type, ag.Type))
        {
          amountSign *= -1;
          PMTran pmTran3 = pmTran1;
          nullable3 = pmTran1.ProjectCuryAmount;
          Decimal? nullable5;
          if (!nullable3.HasValue)
          {
            nullable4 = new Decimal?();
            nullable5 = nullable4;
          }
          else
            nullable5 = new Decimal?(-nullable3.GetValueOrDefault());
          pmTran3.ProjectCuryAmount = nullable5;
          PMTran pmTran4 = pmTran1;
          nullable3 = pmTran1.TranCuryAmount;
          Decimal? nullable6;
          if (!nullable3.HasValue)
          {
            nullable4 = new Decimal?();
            nullable6 = nullable4;
          }
          else
            nullable6 = new Decimal?(-nullable3.GetValueOrDefault());
          pmTran4.TranCuryAmount = nullable6;
          PMTran pmTran5 = pmTran1;
          nullable3 = pmTran1.Amount;
          Decimal? nullable7;
          if (!nullable3.HasValue)
          {
            nullable4 = new Decimal?();
            nullable7 = nullable4;
          }
          else
            nullable7 = new Decimal?(-nullable3.GetValueOrDefault());
          pmTran5.Amount = nullable7;
          PMTran pmTran6 = pmTran1;
          nullable3 = pmTran1.Qty;
          Decimal? nullable8;
          if (!nullable3.HasValue)
          {
            nullable4 = new Decimal?();
            nullable8 = nullable4;
          }
          else
            nullable8 = new Decimal?(-nullable3.GetValueOrDefault());
          pmTran6.Qty = nullable8;
        }
        pmTran1.BillableQty = tran.Qty;
        pmTran1.Released = new bool?(true);
        PMRegister pmRegister2 = pmRegister1;
        nullable3 = pmRegister1.AmtTotal;
        Decimal valueOrDefault1 = nullable3.GetValueOrDefault();
        nullable3 = pmTran1.Amount;
        Decimal valueOrDefault2 = nullable3.GetValueOrDefault();
        Decimal? nullable9 = new Decimal?(valueOrDefault1 + valueOrDefault2);
        pmRegister2.AmtTotal = nullable9;
        PMRegister pmRegister3 = pmRegister1;
        nullable3 = pmRegister1.QtyTotal;
        Decimal valueOrDefault3 = nullable3.GetValueOrDefault();
        nullable3 = pmTran1.Qty;
        Decimal valueOrDefault4 = nullable3.GetValueOrDefault();
        Decimal? nullable10 = new Decimal?(valueOrDefault3 + valueOrDefault4);
        pmRegister3.QtyTotal = nullable10;
        PMRegister pmRegister4 = pmRegister1;
        nullable3 = pmRegister1.BillableQtyTotal;
        Decimal valueOrDefault5 = nullable3.GetValueOrDefault();
        nullable3 = pmTran1.BillableQty;
        Decimal valueOrDefault6 = nullable3.GetValueOrDefault();
        Decimal? nullable11 = new Decimal?(valueOrDefault5 + valueOrDefault6);
        pmRegister4.BillableQtyTotal = nullable11;
        PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.Base.GLTran_Module_BatNbr).Cache, (object) tran, ((PXSelectBase) this.ProjectTrans).Cache, (object) pmTran1, (PXNoteAttribute.IPXCopySettings) null);
        try
        {
          ((PXSelectBase<PMTran>) this.ProjectTrans).Update(pmTran1);
        }
        catch (PXFieldValueProcessingException ex)
        {
          if (((Exception) ex).InnerException is PXTaskIsCompletedException || ((Exception) ex).InnerException is PXTaskIsCanceledException || ((Exception) ex).InnerException is PXTaskIsInactiveException)
            throw new PXSetPropertyException<PX.Objects.GL.GLTran.taskID>("At least one of the documents cannot be processed because it includes a line or lines with the inactive project task (the {0} project task of the {1} project). To be able to process documents that include lines with the project tasks that have the Completed, Canceled, or In Planning status, the user must have the Project Accountant role assigned.", new object[3]
            {
              (object) pmTask.TaskCD.Trim(),
              (object) project.ContractCD.Trim(),
              (object) (PXErrorLevel) 4
            });
          throw;
        }
        ((PXSelectBase) this.Base.CurrencyInfo_ID).Cache.Persist((PXDBOperation) 2);
        ((PXSelectBase) this.ProjectTrans).Cache.Persist((object) pmTran1, (PXDBOperation) 2);
        tran.PMTranID = pmTran1.TranID;
        ((PXGraph) this.Base).Caches[typeof (PX.Objects.GL.GLTran)].Update((object) tran);
        int? nullable12 = pmTran1.TaskID;
        if (nullable12.HasValue)
        {
          nullable3 = pmTran1.Qty;
          Decimal num1 = 0M;
          if (nullable3.GetValueOrDefault() == num1 & nullable3.HasValue)
          {
            nullable3 = pmTran1.Amount;
            Decimal num2 = 0M;
            if (nullable3.GetValueOrDefault() == num2 & nullable3.HasValue)
              continue;
          }
          ProjectBalance.Result result = projectBalance.Calculate(project, pmTran1, ag, account.Type, amountSign, 1);
          if (result.Status != null)
          {
            PMBudgetAccum pmBudgetAccum1 = new PMBudgetAccum();
            pmBudgetAccum1.ProjectID = result.Status.ProjectID;
            pmBudgetAccum1.ProjectTaskID = result.Status.ProjectTaskID;
            pmBudgetAccum1.AccountGroupID = result.Status.AccountGroupID;
            pmBudgetAccum1.InventoryID = result.Status.InventoryID;
            pmBudgetAccum1.CostCodeID = result.Status.CostCodeID;
            pmBudgetAccum1.UOM = result.Status.UOM;
            pmBudgetAccum1.Type = result.Status.Type;
            pmBudgetAccum1.CuryInfoID = result.Status.CuryInfoID;
            pmBudgetAccum1.Description = result.Status.Description;
            if (pmBudgetAccum1.Type == "E" && pmTask != null && pmTask.Type == "CostRev")
              pmBudgetAccum1.RevenueTaskID = pmBudgetAccum1.ProjectTaskID;
            PMBudgetAccum pmBudgetAccum2 = (PMBudgetAccum) ((PXGraph) this.Base).Caches[typeof (PMBudgetAccum)].Insert((object) pmBudgetAccum1);
            PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum2;
            nullable3 = pmBudgetAccum3.ActualQty;
            nullable4 = result.Status.ActualQty;
            Decimal valueOrDefault7 = nullable4.GetValueOrDefault();
            Decimal? nullable13;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable13 = nullable4;
            }
            else
              nullable13 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault7);
            pmBudgetAccum3.ActualQty = nullable13;
            PMBudgetAccum pmBudgetAccum4 = pmBudgetAccum2;
            nullable3 = pmBudgetAccum4.CuryActualAmount;
            nullable4 = result.Status.CuryActualAmount;
            Decimal valueOrDefault8 = nullable4.GetValueOrDefault();
            Decimal? nullable14;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable14 = nullable4;
            }
            else
              nullable14 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault8);
            pmBudgetAccum4.CuryActualAmount = nullable14;
            PMBudgetAccum pmBudgetAccum5 = pmBudgetAccum2;
            nullable3 = pmBudgetAccum5.ActualAmount;
            nullable4 = result.Status.ActualAmount;
            Decimal valueOrDefault9 = nullable4.GetValueOrDefault();
            Decimal? nullable15;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable15 = nullable4;
            }
            else
              nullable15 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault9);
            pmBudgetAccum5.ActualAmount = nullable15;
            ((PXGraph) this.Base).Views.Caches.Add(typeof (PMBudgetAccum));
            PMHistoryByDateAccum historyByDateAccum1 = new PMHistoryByDateAccum();
            historyByDateAccum1.ProjectID = pmTran1.ProjectID;
            historyByDateAccum1.ProjectTaskID = pmTran1.TaskID;
            PMHistoryByDateAccum historyByDateAccum2 = historyByDateAccum1;
            nullable12 = pmTran1.AccountGroupID;
            int? nullable16 = nullable12 ?? pmBudgetAccum2.AccountGroupID;
            historyByDateAccum2.AccountGroupID = nullable16;
            PMHistoryByDateAccum historyByDateAccum3 = historyByDateAccum1;
            nullable12 = pmTran1.InventoryID;
            int? nullable17 = nullable12 ?? pmBudgetAccum2.InventoryID;
            historyByDateAccum3.InventoryID = nullable17;
            PMHistoryByDateAccum historyByDateAccum4 = historyByDateAccum1;
            nullable12 = pmTran1.CostCodeID;
            int? nullable18 = nullable12 ?? pmBudgetAccum2.CostCodeID;
            historyByDateAccum4.CostCodeID = nullable18;
            historyByDateAccum1.Date = pmTran1.Date;
            historyByDateAccum1.PeriodID = pmTran1.TranPeriodID;
            PMHistoryByDateAccum historyByDateAccum5 = (PMHistoryByDateAccum) ((PXGraph) this.Base).Caches[typeof (PMHistoryByDateAccum)].Insert((object) historyByDateAccum1);
            PMHistoryByDateAccum historyByDateAccum6 = historyByDateAccum5;
            nullable3 = historyByDateAccum6.ActualQty;
            nullable4 = result.Status.ActualQty;
            Decimal valueOrDefault10 = nullable4.GetValueOrDefault();
            Decimal? nullable19;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable19 = nullable4;
            }
            else
              nullable19 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault10);
            historyByDateAccum6.ActualQty = nullable19;
            PMHistoryByDateAccum historyByDateAccum7 = historyByDateAccum5;
            nullable3 = historyByDateAccum7.CuryActualAmount;
            nullable4 = result.Status.CuryActualAmount;
            Decimal valueOrDefault11 = nullable4.GetValueOrDefault();
            Decimal? nullable20;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable20 = nullable4;
            }
            else
              nullable20 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault11);
            historyByDateAccum7.CuryActualAmount = nullable20;
            PMHistoryByDateAccum historyByDateAccum8 = historyByDateAccum5;
            nullable3 = historyByDateAccum8.ActualAmount;
            nullable4 = result.Status.ActualAmount;
            Decimal valueOrDefault12 = nullable4.GetValueOrDefault();
            Decimal? nullable21;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable21 = nullable4;
            }
            else
              nullable21 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault12);
            historyByDateAccum8.ActualAmount = nullable21;
            ((PXGraph) this.Base).Views.Caches.Add(typeof (PMHistoryByDateAccum));
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
            PMForecastHistoryAccum forecastHistoryAccum2 = (PMForecastHistoryAccum) ((PXGraph) this.Base).Caches[typeof (PMForecastHistoryAccum)].Insert((object) forecastHistoryAccum1);
            PMForecastHistoryAccum forecastHistoryAccum3 = forecastHistoryAccum2;
            nullable3 = forecastHistoryAccum3.ActualQty;
            nullable4 = result.ForecastHistory.ActualQty;
            Decimal valueOrDefault13 = nullable4.GetValueOrDefault();
            Decimal? nullable22;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable22 = nullable4;
            }
            else
              nullable22 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault13);
            forecastHistoryAccum3.ActualQty = nullable22;
            PMForecastHistoryAccum forecastHistoryAccum4 = forecastHistoryAccum2;
            nullable3 = forecastHistoryAccum4.CuryActualAmount;
            nullable4 = result.ForecastHistory.CuryActualAmount;
            Decimal valueOrDefault14 = nullable4.GetValueOrDefault();
            Decimal? nullable23;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable23 = nullable4;
            }
            else
              nullable23 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault14);
            forecastHistoryAccum4.CuryActualAmount = nullable23;
            PMForecastHistoryAccum forecastHistoryAccum5 = forecastHistoryAccum2;
            nullable3 = forecastHistoryAccum5.ActualAmount;
            nullable4 = result.ForecastHistory.ActualAmount;
            Decimal valueOrDefault15 = nullable4.GetValueOrDefault();
            Decimal? nullable24;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable24 = nullable4;
            }
            else
              nullable24 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault15);
            forecastHistoryAccum5.ActualAmount = nullable24;
            PMForecastHistoryAccum forecastHistoryAccum6 = forecastHistoryAccum2;
            nullable3 = forecastHistoryAccum6.CuryArAmount;
            nullable4 = result.ForecastHistory.CuryArAmount;
            Decimal valueOrDefault16 = nullable4.GetValueOrDefault();
            Decimal? nullable25;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable25 = nullable4;
            }
            else
              nullable25 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault16);
            forecastHistoryAccum6.CuryArAmount = nullable25;
            ((PXGraph) this.Base).Views.Caches.Add(typeof (PMForecastHistoryAccum));
          }
          if (result.TaskTotal != null)
          {
            PMTaskTotal pmTaskTotal1 = (PMTaskTotal) ((PXGraph) this.Base).Caches[typeof (PMTaskTotal)].Insert((object) new PMTaskTotal()
            {
              ProjectID = result.TaskTotal.ProjectID,
              TaskID = result.TaskTotal.TaskID
            });
            PMTaskTotal pmTaskTotal2 = pmTaskTotal1;
            nullable3 = pmTaskTotal2.CuryAsset;
            nullable4 = result.TaskTotal.CuryAsset;
            Decimal valueOrDefault17 = nullable4.GetValueOrDefault();
            Decimal? nullable26;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable26 = nullable4;
            }
            else
              nullable26 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault17);
            pmTaskTotal2.CuryAsset = nullable26;
            PMTaskTotal pmTaskTotal3 = pmTaskTotal1;
            nullable3 = pmTaskTotal3.Asset;
            nullable4 = result.TaskTotal.Asset;
            Decimal valueOrDefault18 = nullable4.GetValueOrDefault();
            Decimal? nullable27;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable27 = nullable4;
            }
            else
              nullable27 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault18);
            pmTaskTotal3.Asset = nullable27;
            PMTaskTotal pmTaskTotal4 = pmTaskTotal1;
            nullable3 = pmTaskTotal4.CuryLiability;
            nullable4 = result.TaskTotal.CuryLiability;
            Decimal valueOrDefault19 = nullable4.GetValueOrDefault();
            Decimal? nullable28;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable28 = nullable4;
            }
            else
              nullable28 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault19);
            pmTaskTotal4.CuryLiability = nullable28;
            PMTaskTotal pmTaskTotal5 = pmTaskTotal1;
            nullable3 = pmTaskTotal5.Liability;
            nullable4 = result.TaskTotal.Liability;
            Decimal valueOrDefault20 = nullable4.GetValueOrDefault();
            Decimal? nullable29;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable29 = nullable4;
            }
            else
              nullable29 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault20);
            pmTaskTotal5.Liability = nullable29;
            PMTaskTotal pmTaskTotal6 = pmTaskTotal1;
            nullable3 = pmTaskTotal6.CuryIncome;
            nullable4 = result.TaskTotal.CuryIncome;
            Decimal valueOrDefault21 = nullable4.GetValueOrDefault();
            Decimal? nullable30;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable30 = nullable4;
            }
            else
              nullable30 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault21);
            pmTaskTotal6.CuryIncome = nullable30;
            PMTaskTotal pmTaskTotal7 = pmTaskTotal1;
            nullable3 = pmTaskTotal7.Income;
            nullable4 = result.TaskTotal.Income;
            Decimal valueOrDefault22 = nullable4.GetValueOrDefault();
            Decimal? nullable31;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable31 = nullable4;
            }
            else
              nullable31 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault22);
            pmTaskTotal7.Income = nullable31;
            PMTaskTotal pmTaskTotal8 = pmTaskTotal1;
            nullable3 = pmTaskTotal8.CuryExpense;
            nullable4 = result.TaskTotal.CuryExpense;
            Decimal valueOrDefault23 = nullable4.GetValueOrDefault();
            Decimal? nullable32;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable32 = nullable4;
            }
            else
              nullable32 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault23);
            pmTaskTotal8.CuryExpense = nullable32;
            PMTaskTotal pmTaskTotal9 = pmTaskTotal1;
            nullable3 = pmTaskTotal9.Expense;
            nullable4 = result.TaskTotal.Expense;
            Decimal valueOrDefault24 = nullable4.GetValueOrDefault();
            Decimal? nullable33;
            if (!nullable3.HasValue)
            {
              nullable4 = new Decimal?();
              nullable33 = nullable4;
            }
            else
              nullable33 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault24);
            pmTaskTotal9.Expense = nullable33;
            ((PXGraph) this.Base).Views.Caches.Add(typeof (PMTaskTotal));
          }
          RegisterReleaseProcess.AddToUnbilledSummary((PXGraph) this.Base, pmTran1);
          nullable1 = pmTran1.Allocated;
          if (!nullable1.GetValueOrDefault())
          {
            nullable1 = pmTran1.ExcludedFromAllocation;
            if (!nullable1.GetValueOrDefault())
            {
              nullable1 = project.AutoAllocate;
              if (nullable1.GetValueOrDefault() && !this.tasksToAutoAllocate.ContainsKey($"{pmTask.ProjectID}.{pmTask.TaskID}"))
                this.tasksToAutoAllocate.Add($"{pmTask.ProjectID}.{pmTask.TaskID}", pmTask);
            }
          }
        }
      }
      ((PXSelectBase) this.ProjectDocs).Cache.Persisted(false);
      PMRegister pmRegister5 = PXResultset<PMRegister>.op_Implicit(PXSelectBase<PMRegister, PXViewOf<PMRegister>.BasedOn<SelectFromBase<PMRegister, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMRegister.module, Equal<BqlField<PMRegister.module, IBqlString>.FromCurrent>>>>>.And<BqlOperand<PMRegister.refNbr, IBqlString>.IsEqual<BqlField<PMRegister.refNbr, IBqlString>.FromCurrent>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
      pmRegister5.AmtTotal = pmRegister1.AmtTotal;
      pmRegister5.QtyTotal = pmRegister1.QtyTotal;
      pmRegister5.BillableQtyTotal = pmRegister1.BillableQtyTotal;
      ((PXSelectBase<PMRegister>) this.ProjectDocs).Update(pmRegister5);
      ((PXSelectBase) this.ProjectDocs).Cache.Persist((PXDBOperation) 1);
    }
    ((PXGraph) this.Base).Caches[typeof (PMUnbilledDailySummaryAccum)].Persist((PXDBOperation) 2);
    ((PXGraph) this.Base).Caches[typeof (PMBudgetAccum)].Persist((PXDBOperation) 2);
    ((PXGraph) this.Base).Caches[typeof (PMForecastHistoryAccum)].Persist((PXDBOperation) 2);
    ((PXGraph) this.Base).Caches[typeof (PMTaskTotal)].Persist((PXDBOperation) 2);
    ((PXGraph) this.Base).Caches[typeof (PMTask)].Persist((PXDBOperation) 1);
  }

  public virtual ProjectBalance CreateProjectBalance() => new ProjectBalance((PXGraph) this.Base);

  private void SetOrigDocLink(PMRegister doc, PXResultset<PX.Objects.GL.GLTran> glTrans)
  {
    JournalEntryTranRef instance = PXGraph.CreateInstance<JournalEntryTranRef>();
    foreach (PXResult<PX.Objects.GL.GLTran> glTran1 in glTrans)
    {
      PX.Objects.GL.GLTran glTran2 = PXResult<PX.Objects.GL.GLTran>.op_Implicit(glTran1);
      if (glTran2.RefNbr != null)
      {
        PX.Objects.AP.APInvoice apDoc = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(PXSelectBase<PX.Objects.AP.APInvoice, PXSelect<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.GL.GLTran.tranType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.GL.GLTran.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
        {
          (object) glTran2.TranType,
          (object) glTran2.RefNbr
        }));
        if (apDoc != null)
        {
          doc.OrigDocType = instance.GetDocType(apDoc, (PX.Objects.AR.ARInvoice) null, glTran2);
          doc.OrigNoteID = instance.GetNoteID(apDoc, (PX.Objects.AR.ARInvoice) null, glTran2);
          break;
        }
      }
    }
  }

  protected virtual void UpdateProjectBalance(Batch b)
  {
    PXSelectJoin<PX.Objects.GL.GLTran, InnerJoin<PMProject, On<PX.Objects.GL.GLTran.projectID, Equal<PMProject.contractID>>, InnerJoin<PMTran, On<PX.Objects.GL.GLTran.pMTranID, Equal<PMTran.tranID>>, InnerJoin<PMAccountGroup, On<PMTran.accountGroupID, Equal<PMAccountGroup.groupID>>, LeftJoin<PostGraphExt.OffsetPMAccountGroup, On<PMTran.offsetAccountGroupID, Equal<PostGraphExt.OffsetPMAccountGroup.groupID>>>>>>, Where<PX.Objects.GL.GLTran.module, Equal<Required<PX.Objects.GL.GLTran.module>>, And<PX.Objects.GL.GLTran.batchNbr, Equal<Required<PX.Objects.GL.GLTran.batchNbr>>, And<PX.Objects.GL.GLTran.pMTranID, IsNotNull, And<PX.Objects.GL.GLTran.projectID, NotEqual<Required<PX.Objects.GL.GLTran.projectID>>>>>>> pxSelectJoin = new PXSelectJoin<PX.Objects.GL.GLTran, InnerJoin<PMProject, On<PX.Objects.GL.GLTran.projectID, Equal<PMProject.contractID>>, InnerJoin<PMTran, On<PX.Objects.GL.GLTran.pMTranID, Equal<PMTran.tranID>>, InnerJoin<PMAccountGroup, On<PMTran.accountGroupID, Equal<PMAccountGroup.groupID>>, LeftJoin<PostGraphExt.OffsetPMAccountGroup, On<PMTran.offsetAccountGroupID, Equal<PostGraphExt.OffsetPMAccountGroup.groupID>>>>>>, Where<PX.Objects.GL.GLTran.module, Equal<Required<PX.Objects.GL.GLTran.module>>, And<PX.Objects.GL.GLTran.batchNbr, Equal<Required<PX.Objects.GL.GLTran.batchNbr>>, And<PX.Objects.GL.GLTran.pMTranID, IsNotNull, And<PX.Objects.GL.GLTran.projectID, NotEqual<Required<PX.Objects.GL.GLTran.projectID>>>>>>>((PXGraph) this.Base);
    ProjectBalance projectBalance = this.CreateProjectBalance();
    HashSet<long> longSet1 = new HashSet<long>();
    object[] objArray = new object[3]
    {
      (object) b.Module,
      (object) b.BatchNbr,
      (object) ProjectDefaultAttribute.NonProject()
    };
    foreach (PXResult<PX.Objects.GL.GLTran, PMProject, PMTran, PMAccountGroup, PostGraphExt.OffsetPMAccountGroup> pxResult in ((PXSelectBase<PX.Objects.GL.GLTran>) pxSelectJoin).Select(objArray))
    {
      PX.Objects.GL.GLTran glTran = PXResult<PX.Objects.GL.GLTran, PMProject, PMTran, PMAccountGroup, PostGraphExt.OffsetPMAccountGroup>.op_Implicit(pxResult);
      PMProject project = PXResult<PX.Objects.GL.GLTran, PMProject, PMTran, PMAccountGroup, PostGraphExt.OffsetPMAccountGroup>.op_Implicit(pxResult);
      PMTran tran = PXResult<PX.Objects.GL.GLTran, PMProject, PMTran, PMAccountGroup, PostGraphExt.OffsetPMAccountGroup>.op_Implicit(pxResult);
      PMAccountGroup ag = PXResult<PX.Objects.GL.GLTran, PMProject, PMTran, PMAccountGroup, PostGraphExt.OffsetPMAccountGroup>.op_Implicit(pxResult);
      PostGraphExt.OffsetPMAccountGroup offsetAg = PXResult<PX.Objects.GL.GLTran, PMProject, PMTran, PMAccountGroup, PostGraphExt.OffsetPMAccountGroup>.op_Implicit(pxResult);
      long? nullable1 = tran.RemainderOfTranID;
      if (!nullable1.HasValue)
      {
        HashSet<long> longSet2 = longSet1;
        nullable1 = glTran.PMTranID;
        long num1 = nullable1.Value;
        if (!longSet2.Contains(num1))
        {
          HashSet<long> longSet3 = longSet1;
          nullable1 = glTran.PMTranID;
          long num2 = nullable1.Value;
          longSet3.Add(num2);
          foreach (ProjectBalance.Result result in projectBalance.Calculate(project, tran, ag, (PMAccountGroup) offsetAg))
          {
            foreach (PMHistory pmHistory in (IEnumerable<PMHistory>) result.History)
            {
              PMHistoryAccum pmHistoryAccum1 = new PMHistoryAccum();
              pmHistoryAccum1.ProjectID = pmHistory.ProjectID;
              pmHistoryAccum1.ProjectTaskID = pmHistory.ProjectTaskID;
              pmHistoryAccum1.AccountGroupID = pmHistory.AccountGroupID;
              pmHistoryAccum1.InventoryID = pmHistory.InventoryID;
              pmHistoryAccum1.CostCodeID = pmHistory.CostCodeID;
              pmHistoryAccum1.PeriodID = pmHistory.PeriodID;
              pmHistoryAccum1.BranchID = pmHistory.BranchID;
              PMHistoryAccum pmHistoryAccum2 = ((PXSelectBase<PMHistoryAccum>) this.ProjectHistory).Insert(pmHistoryAccum1);
              PMHistoryAccum pmHistoryAccum3 = pmHistoryAccum2;
              Decimal? nullable2 = pmHistoryAccum3.FinPTDCuryAmount;
              Decimal? nullable3 = pmHistory.FinPTDCuryAmount;
              Decimal valueOrDefault1 = nullable3.GetValueOrDefault();
              Decimal? nullable4;
              if (!nullable2.HasValue)
              {
                nullable3 = new Decimal?();
                nullable4 = nullable3;
              }
              else
                nullable4 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault1);
              pmHistoryAccum3.FinPTDCuryAmount = nullable4;
              PMHistoryAccum pmHistoryAccum4 = pmHistoryAccum2;
              nullable2 = pmHistoryAccum4.FinPTDAmount;
              nullable3 = pmHistory.FinPTDAmount;
              Decimal valueOrDefault2 = nullable3.GetValueOrDefault();
              Decimal? nullable5;
              if (!nullable2.HasValue)
              {
                nullable3 = new Decimal?();
                nullable5 = nullable3;
              }
              else
                nullable5 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault2);
              pmHistoryAccum4.FinPTDAmount = nullable5;
              PMHistoryAccum pmHistoryAccum5 = pmHistoryAccum2;
              nullable2 = pmHistoryAccum5.FinYTDCuryAmount;
              nullable3 = pmHistory.FinYTDCuryAmount;
              Decimal valueOrDefault3 = nullable3.GetValueOrDefault();
              Decimal? nullable6;
              if (!nullable2.HasValue)
              {
                nullable3 = new Decimal?();
                nullable6 = nullable3;
              }
              else
                nullable6 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault3);
              pmHistoryAccum5.FinYTDCuryAmount = nullable6;
              PMHistoryAccum pmHistoryAccum6 = pmHistoryAccum2;
              nullable2 = pmHistoryAccum6.FinYTDAmount;
              nullable3 = pmHistory.FinYTDAmount;
              Decimal valueOrDefault4 = nullable3.GetValueOrDefault();
              Decimal? nullable7;
              if (!nullable2.HasValue)
              {
                nullable3 = new Decimal?();
                nullable7 = nullable3;
              }
              else
                nullable7 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault4);
              pmHistoryAccum6.FinYTDAmount = nullable7;
              PMHistoryAccum pmHistoryAccum7 = pmHistoryAccum2;
              nullable2 = pmHistoryAccum7.FinPTDQty;
              nullable3 = pmHistory.FinPTDQty;
              Decimal valueOrDefault5 = nullable3.GetValueOrDefault();
              Decimal? nullable8;
              if (!nullable2.HasValue)
              {
                nullable3 = new Decimal?();
                nullable8 = nullable3;
              }
              else
                nullable8 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault5);
              pmHistoryAccum7.FinPTDQty = nullable8;
              PMHistoryAccum pmHistoryAccum8 = pmHistoryAccum2;
              nullable2 = pmHistoryAccum8.FinYTDQty;
              nullable3 = pmHistory.FinYTDQty;
              Decimal valueOrDefault6 = nullable3.GetValueOrDefault();
              Decimal? nullable9;
              if (!nullable2.HasValue)
              {
                nullable3 = new Decimal?();
                nullable9 = nullable3;
              }
              else
                nullable9 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault6);
              pmHistoryAccum8.FinYTDQty = nullable9;
              PMHistoryAccum pmHistoryAccum9 = pmHistoryAccum2;
              nullable2 = pmHistoryAccum9.TranPTDCuryAmount;
              nullable3 = pmHistory.TranPTDCuryAmount;
              Decimal valueOrDefault7 = nullable3.GetValueOrDefault();
              Decimal? nullable10;
              if (!nullable2.HasValue)
              {
                nullable3 = new Decimal?();
                nullable10 = nullable3;
              }
              else
                nullable10 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault7);
              pmHistoryAccum9.TranPTDCuryAmount = nullable10;
              PMHistoryAccum pmHistoryAccum10 = pmHistoryAccum2;
              nullable2 = pmHistoryAccum10.TranPTDAmount;
              nullable3 = pmHistory.TranPTDAmount;
              Decimal valueOrDefault8 = nullable3.GetValueOrDefault();
              Decimal? nullable11;
              if (!nullable2.HasValue)
              {
                nullable3 = new Decimal?();
                nullable11 = nullable3;
              }
              else
                nullable11 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault8);
              pmHistoryAccum10.TranPTDAmount = nullable11;
              PMHistoryAccum pmHistoryAccum11 = pmHistoryAccum2;
              nullable2 = pmHistoryAccum11.TranYTDCuryAmount;
              nullable3 = pmHistory.TranYTDCuryAmount;
              Decimal valueOrDefault9 = nullable3.GetValueOrDefault();
              Decimal? nullable12;
              if (!nullable2.HasValue)
              {
                nullable3 = new Decimal?();
                nullable12 = nullable3;
              }
              else
                nullable12 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault9);
              pmHistoryAccum11.TranYTDCuryAmount = nullable12;
              PMHistoryAccum pmHistoryAccum12 = pmHistoryAccum2;
              nullable2 = pmHistoryAccum12.TranYTDAmount;
              nullable3 = pmHistory.TranYTDAmount;
              Decimal valueOrDefault10 = nullable3.GetValueOrDefault();
              Decimal? nullable13;
              if (!nullable2.HasValue)
              {
                nullable3 = new Decimal?();
                nullable13 = nullable3;
              }
              else
                nullable13 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault10);
              pmHistoryAccum12.TranYTDAmount = nullable13;
              PMHistoryAccum pmHistoryAccum13 = pmHistoryAccum2;
              nullable2 = pmHistoryAccum13.TranPTDQty;
              nullable3 = pmHistory.TranPTDQty;
              Decimal valueOrDefault11 = nullable3.GetValueOrDefault();
              Decimal? nullable14;
              if (!nullable2.HasValue)
              {
                nullable3 = new Decimal?();
                nullable14 = nullable3;
              }
              else
                nullable14 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault11);
              pmHistoryAccum13.TranPTDQty = nullable14;
              PMHistoryAccum pmHistoryAccum14 = pmHistoryAccum2;
              nullable2 = pmHistoryAccum14.TranYTDQty;
              nullable3 = pmHistory.TranYTDQty;
              Decimal valueOrDefault12 = nullable3.GetValueOrDefault();
              Decimal? nullable15;
              if (!nullable2.HasValue)
              {
                nullable3 = new Decimal?();
                nullable15 = nullable3;
              }
              else
                nullable15 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault12);
              pmHistoryAccum14.TranYTDQty = nullable15;
            }
          }
        }
      }
    }
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
      this.created.AddRange((IEnumerable<Batch>) processInfo.Batches);
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  [PXBreakInheritance]
  [Serializable]
  public class OffsetAccount : PX.Objects.GL.Account
  {
    public new abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PostGraphExt.OffsetAccount.accountID>
    {
    }

    public new abstract class accountCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PostGraphExt.OffsetAccount.accountCD>
    {
    }

    public new abstract class accountGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PostGraphExt.OffsetAccount.accountGroupID>
    {
    }
  }

  [PXHidden]
  [PXBreakInheritance]
  [Serializable]
  public class OffsetPMAccountGroup : PMAccountGroup
  {
    public new abstract class groupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PostGraphExt.OffsetPMAccountGroup.groupID>
    {
    }

    public new abstract class groupCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PostGraphExt.OffsetPMAccountGroup.groupCD>
    {
    }

    public new abstract class type : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PostGraphExt.OffsetPMAccountGroup.type>
    {
    }
  }

  public abstract class baseCuryInfoID : IBqlField, IBqlOperand
  {
  }

  public abstract class projectCuryInfoID : IBqlField, IBqlOperand
  {
  }
}

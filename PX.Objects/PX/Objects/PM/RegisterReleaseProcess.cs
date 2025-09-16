// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.RegisterReleaseProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.Objects.PM.GraphExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

public class RegisterReleaseProcess : PXGraph<
#nullable disable
RegisterReleaseProcess>
{
  private PMSetup setup;

  [InjectDependency]
  public IProjectMultiCurrency MultiCurrencyService { get; set; }

  public bool AutoReleaseAllocation
  {
    get
    {
      if (this.setup == null)
        this.setup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      return this.setup.AutoReleaseAllocation.GetValueOrDefault();
    }
  }

  public virtual PMRegister OnBeforeRelease(PMRegister doc) => doc;

  public virtual List<Batch> Release(JournalEntry je, PMRegister doc, out List<PMTask> allocTasks)
  {
    doc = this.OnBeforeRelease(doc);
    allocTasks = new List<PMTask>();
    List<Batch> batchList = new List<Batch>();
    Dictionary<string, PMTask> dictionary = new Dictionary<string, PMTask>();
    List<PMTran> pmTranList = new List<PMTran>();
    Dictionary<string, List<RegisterReleaseProcess.TranInfo>> branchAndFinPeriod = this.GetTransByBranchAndFinPeriod(doc);
    ProjectBalance projectBalance = this.CreateProjectBalance();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (KeyValuePair<string, List<RegisterReleaseProcess.TranInfo>> keyValuePair in branchAndFinPeriod)
      {
        string[] strArray = keyValuePair.Key.Split('.');
        int? nullable1 = strArray[0] == "0" ? new int?() : new int?(int.Parse(strArray[0]));
        ((PXGraph) je).Clear((PXClearOption) 3);
        this.MultiCurrencyService.Clear();
        PX.Objects.CM.CurrencyInfo currencyInfo = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Insert(new PX.Objects.CM.CurrencyInfo()
        {
          CuryID = strArray[2],
          CuryEffDate = ((PXGraph) this).Accessinfo.BusinessDate
        });
        ((PXSelectBase<Batch>) je.BatchModule).Insert(new Batch()
        {
          Module = doc.Module,
          Status = "U",
          Released = new bool?(true),
          Hold = new bool?(false),
          BranchID = nullable1,
          FinPeriodID = strArray[1],
          CuryID = strArray[2],
          CuryInfoID = currencyInfo.CuryInfoID,
          Description = doc.Description
        });
        bool flag = false;
        foreach (RegisterReleaseProcess.TranInfo tranInfo in keyValuePair.Value)
        {
          bool isGL = false;
          if (!tranInfo.Tran.Released.GetValueOrDefault() && !tranInfo.Tran.IsNonGL.GetValueOrDefault() && tranInfo.Project.BaseType == "P" && !string.IsNullOrEmpty(tranInfo.AccountGroup.Type) && tranInfo.AccountGroup.Type != "O" && !ProjectDefaultAttribute.IsNonProject(tranInfo.Tran.ProjectID) && tranInfo.Tran.AccountID.HasValue && tranInfo.Tran.SubID.HasValue && tranInfo.Tran.OffsetAccountID.HasValue && tranInfo.Tran.OffsetSubID.HasValue)
          {
            GLTran glTran1 = new GLTran();
            glTran1.TranDate = tranInfo.Tran.Date;
            glTran1.TranPeriodID = tranInfo.Tran.TranPeriodID;
            glTran1.SummPost = new bool?(false);
            glTran1.BranchID = tranInfo.Tran.BranchID;
            glTran1.PMTranID = tranInfo.Tran.TranID;
            glTran1.ProjectID = tranInfo.Tran.ProjectID;
            glTran1.TaskID = tranInfo.Tran.TaskID;
            glTran1.CostCodeID = tranInfo.Tran.CostCodeID;
            glTran1.TranDesc = tranInfo.Tran.Description;
            glTran1.ReferenceID = tranInfo.Tran.BAccountID;
            GLTran glTran2 = glTran1;
            int? nullable2 = tranInfo.Tran.InventoryID;
            int emptyInventoryId1 = PMInventorySelectorAttribute.EmptyInventoryID;
            int? nullable3;
            if (!(nullable2.GetValueOrDefault() == emptyInventoryId1 & nullable2.HasValue))
            {
              nullable3 = tranInfo.Tran.InventoryID;
            }
            else
            {
              nullable2 = new int?();
              nullable3 = nullable2;
            }
            glTran2.InventoryID = nullable3;
            glTran1.Qty = tranInfo.Tran.Qty;
            glTran1.UOM = tranInfo.Tran.UOM;
            glTran1.TranType = tranInfo.Tran.TranType;
            glTran1.CuryCreditAmt = new Decimal?(0M);
            glTran1.CreditAmt = new Decimal?(0M);
            glTran1.CuryDebitAmt = tranInfo.Tran.TranCuryAmount;
            long? curyInfoID1;
            glTran1.DebitAmt = this.CalculateCurrency(je, tranInfo.Tran, out curyInfoID1);
            glTran1.CuryInfoID = curyInfoID1;
            glTran1.AccountID = tranInfo.Tran.AccountID;
            glTran1.SubID = tranInfo.Tran.SubID;
            glTran1.Released = new bool?(true);
            ((PXSelectBase<GLTran>) je.GLTranModuleBatNbr).Insert(glTran1);
            GLTran glTran3 = new GLTran();
            glTran3.TranDate = tranInfo.Tran.Date;
            glTran3.TranPeriodID = tranInfo.Tran.TranPeriodID;
            glTran3.SummPost = new bool?(false);
            glTran3.BranchID = tranInfo.Tran.BranchID;
            glTran3.PMTranID = tranInfo.Tran.TranID;
            GLTran glTran4 = glTran3;
            nullable2 = tranInfo.OffsetAccountGroup.GroupID;
            int? nullable4 = nullable2.HasValue ? tranInfo.Tran.ProjectID : ProjectDefaultAttribute.NonProject();
            glTran4.ProjectID = nullable4;
            GLTran glTran5 = glTran3;
            nullable2 = tranInfo.OffsetAccountGroup.GroupID;
            int? nullable5;
            if (!nullable2.HasValue)
            {
              nullable2 = new int?();
              nullable5 = nullable2;
            }
            else
              nullable5 = tranInfo.Tran.TaskID;
            glTran5.TaskID = nullable5;
            GLTran glTran6 = glTran3;
            nullable2 = glTran3.TaskID;
            int? nullable6;
            if (!nullable2.HasValue)
            {
              nullable2 = new int?();
              nullable6 = nullable2;
            }
            else
              nullable6 = tranInfo.Tran.CostCodeID;
            glTran6.CostCodeID = nullable6;
            glTran3.TranDesc = tranInfo.Tran.Description;
            glTran3.ReferenceID = tranInfo.Tran.BAccountID;
            GLTran glTran7 = glTran3;
            nullable2 = tranInfo.Tran.InventoryID;
            int emptyInventoryId2 = PMInventorySelectorAttribute.EmptyInventoryID;
            int? nullable7;
            if (!(nullable2.GetValueOrDefault() == emptyInventoryId2 & nullable2.HasValue))
            {
              nullable7 = tranInfo.Tran.InventoryID;
            }
            else
            {
              nullable2 = new int?();
              nullable7 = nullable2;
            }
            glTran7.InventoryID = nullable7;
            glTran3.Qty = tranInfo.Tran.Qty;
            glTran3.UOM = tranInfo.Tran.UOM;
            glTran3.TranType = tranInfo.Tran.TranType;
            glTran3.CuryCreditAmt = tranInfo.Tran.TranCuryAmount;
            long? curyInfoID2;
            glTran3.CreditAmt = this.CalculateCurrency(je, tranInfo.Tran, out curyInfoID2);
            glTran3.CuryDebitAmt = new Decimal?(0M);
            glTran3.DebitAmt = new Decimal?(0M);
            glTran3.CuryInfoID = curyInfoID2;
            glTran3.AccountID = tranInfo.Tran.OffsetAccountID;
            glTran3.SubID = tranInfo.Tran.OffsetSubID;
            glTran3.Released = new bool?(true);
            ((PXSelectBase<GLTran>) je.GLTranModuleBatNbr).Insert(glTran3);
            flag = true;
            isGL = true;
            tranInfo.Tran.BatchNbr = ((PXSelectBase<Batch>) je.BatchModule).Current.BatchNbr;
          }
          if (!isGL && !tranInfo.Tran.AccountGroupID.HasValue && tranInfo.Project.BaseType == "P" && !tranInfo.Project.NonProject.GetValueOrDefault())
            throw new PXException("Failed to Release PM Transaction '{0}': Account Group is required.", new object[1]
            {
              (object) doc.RefNbr
            });
          this.UpdateBalance(je, projectBalance, tranInfo.Tran, tranInfo.Project, tranInfo.Task, tranInfo.Account, tranInfo.AccountGroup, (PX.Objects.GL.Account) tranInfo.OffsetAccount, (PMAccountGroup) tranInfo.OffsetAccountGroup, isGL);
          RegisterReleaseProcess.AddToUnbilledSummary((PXGraph) je, tranInfo.Tran);
          tranInfo.Tran.Released = new bool?(true);
          ((PXGraph) je).Caches[typeof (PMTran)].Update((object) tranInfo.Tran);
          if (tranInfo.Project.BaseType == "P" && !tranInfo.Project.NonProject.GetValueOrDefault())
          {
            pmTranList.Add(tranInfo.Tran);
            bool? nullable8 = tranInfo.Tran.Allocated;
            if (!nullable8.GetValueOrDefault())
            {
              nullable8 = tranInfo.Tran.ExcludedFromAllocation;
              if (!nullable8.GetValueOrDefault())
              {
                nullable8 = tranInfo.Project.AutoAllocate;
                if (nullable8.GetValueOrDefault() && !dictionary.ContainsKey($"{tranInfo.Task.ProjectID}.{tranInfo.Task.TaskID}"))
                  dictionary.Add($"{tranInfo.Task.ProjectID}.{tranInfo.Task.TaskID}", tranInfo.Task);
              }
            }
          }
        }
        if (flag)
        {
          ((PXGraph) je).Views.Caches.Add(typeof (PMHistoryAccum));
          ((PXAction) je.Save).Press();
          batchList.Add(((PXSelectBase<Batch>) je.BatchModule).Current);
        }
        else
        {
          ((PXGraph) je).Persist(typeof (PMTran), (PXDBOperation) 1);
          ((PXGraph) je).Persist(typeof (PMBudgetAccum), (PXDBOperation) 2);
          ((PXGraph) je).SelectTimeStamp();
          ((PXGraph) je).Persist(typeof (PMTask), (PXDBOperation) 1);
          ((PXGraph) je).Persist(typeof (PMForecastHistoryAccum), (PXDBOperation) 2);
          ((PXGraph) je).Persist(typeof (PMHistoryByDateAccum), (PXDBOperation) 2);
          ((PXGraph) je).Persist(typeof (PMTaskTotal), (PXDBOperation) 2);
          ((PXGraph) je).Persist(typeof (PMTaskAllocTotalAccum), (PXDBOperation) 2);
          ((PXGraph) je).Persist(typeof (PMHistoryAccum), (PXDBOperation) 2);
          ((PXGraph) je).Persist(typeof (PMUnbilledDailySummaryAccum), (PXDBOperation) 2);
          ((PXGraph) je).SelectTimeStamp();
        }
      }
      allocTasks.AddRange((IEnumerable<PMTask>) dictionary.Values);
      doc.Released = new bool?(true);
      doc.Status = "R";
      ((PXGraph) je).Caches[typeof (PMRegister)].Update((object) doc);
      ((PXGraph) je).Persist(typeof (PMTran), (PXDBOperation) 1);
      ((PXGraph) je).Persist(typeof (PMRegister), (PXDBOperation) 1);
      ((PXGraph) je).Persist(typeof (PMBudgetAccum), (PXDBOperation) 2);
      ((PXGraph) je).Persist(typeof (PMTask), (PXDBOperation) 1);
      ((PXGraph) je).Persist(typeof (PMForecastHistoryAccum), (PXDBOperation) 2);
      ((PXGraph) je).Persist(typeof (PMHistoryByDateAccum), (PXDBOperation) 2);
      ((PXGraph) je).Persist(typeof (PMTaskAllocTotalAccum), (PXDBOperation) 2);
      ((PXGraph) je).Persist(typeof (PMTaskTotal), (PXDBOperation) 2);
      transactionScope.Complete();
    }
    return batchList;
  }

  private Decimal? CalculateCurrency(JournalEntry je, PMTran tran, out long? curyInfoID)
  {
    PX.Objects.GL.Ledger ledger = PX.Objects.GL.Ledger.PK.Find((PXGraph) je, ((PXSelectBase<Batch>) je.BatchModule).Current.LedgerID);
    if (tran.TranCuryID == ledger.BaseCuryID)
    {
      PX.Objects.CM.Extensions.CurrencyInfo directRate = this.MultiCurrencyService.CreateDirectRate((PXGraph) je, ledger.BaseCuryID, tran.Date, "GL");
      curyInfoID = directRate.CuryInfoID;
      return tran.TranCuryAmount;
    }
    PX.Objects.CM.Extensions.CurrencyInfo rate = this.MultiCurrencyService.CreateRate((PXGraph) je, tran.TranCuryID, ledger.BaseCuryID, tran.Date, (string) null, "GL");
    curyInfoID = rate.CuryInfoID;
    return new Decimal?(rate.CuryConvBase(tran.TranCuryAmount.GetValueOrDefault()));
  }

  protected virtual void UpdateBalance(
    JournalEntry je,
    ProjectBalance pb,
    PMTran tran,
    PMProject project,
    PMTask task,
    PX.Objects.GL.Account account,
    PMAccountGroup accountGroup,
    PX.Objects.GL.Account offsetAccount,
    PMAccountGroup offsetAccountGroup,
    bool isGL)
  {
    if (tran.ExcludedFromBalance.GetValueOrDefault())
      return;
    JournalEntryProjectExt extension = ((PXGraph) je).GetExtension<JournalEntryProjectExt>();
    foreach (ProjectBalance.Result result in (IEnumerable<ProjectBalance.Result>) pb.Calculate(project, tran, accountGroup, offsetAccountGroup))
    {
      if (result.Status != null)
        this.UpdateActuals(extension, result.Status, tran, project, task);
      Decimal? nullable1;
      Decimal? nullable2;
      if (result.ForecastHistory != null)
      {
        PMForecastHistoryAccum forecastHistoryAccum1 = new PMForecastHistoryAccum();
        forecastHistoryAccum1.ProjectID = result.ForecastHistory.ProjectID;
        forecastHistoryAccum1.ProjectTaskID = result.ForecastHistory.ProjectTaskID;
        forecastHistoryAccum1.AccountGroupID = result.ForecastHistory.AccountGroupID;
        forecastHistoryAccum1.InventoryID = result.ForecastHistory.InventoryID;
        forecastHistoryAccum1.CostCodeID = result.ForecastHistory.CostCodeID;
        forecastHistoryAccum1.PeriodID = result.ForecastHistory.PeriodID;
        PMForecastHistoryAccum forecastHistoryAccum2 = ((PXSelectBase<PMForecastHistoryAccum>) extension.ForecastHistory).Insert(forecastHistoryAccum1);
        PMForecastHistoryAccum forecastHistoryAccum3 = forecastHistoryAccum2;
        nullable1 = forecastHistoryAccum3.ActualQty;
        nullable2 = result.ForecastHistory.ActualQty;
        Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
        Decimal? nullable3;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable3 = nullable2;
        }
        else
          nullable3 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault1);
        forecastHistoryAccum3.ActualQty = nullable3;
        PMForecastHistoryAccum forecastHistoryAccum4 = forecastHistoryAccum2;
        nullable1 = forecastHistoryAccum4.CuryActualAmount;
        nullable2 = result.ForecastHistory.CuryActualAmount;
        Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
        Decimal? nullable4;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable4 = nullable2;
        }
        else
          nullable4 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault2);
        forecastHistoryAccum4.CuryActualAmount = nullable4;
        PMForecastHistoryAccum forecastHistoryAccum5 = forecastHistoryAccum2;
        nullable1 = forecastHistoryAccum5.ActualAmount;
        nullable2 = result.ForecastHistory.ActualAmount;
        Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
        Decimal? nullable5;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable5 = nullable2;
        }
        else
          nullable5 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault3);
        forecastHistoryAccum5.ActualAmount = nullable5;
        PMForecastHistoryAccum forecastHistoryAccum6 = forecastHistoryAccum2;
        nullable1 = forecastHistoryAccum6.CuryArAmount;
        nullable2 = result.ForecastHistory.CuryArAmount;
        Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
        Decimal? nullable6;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable6 = nullable2;
        }
        else
          nullable6 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault4);
        forecastHistoryAccum6.CuryArAmount = nullable6;
      }
      if (result.TaskTotal != null)
      {
        PMTaskTotal pmTaskTotal1 = ((PXSelectBase<PMTaskTotal>) extension.ProjectTaskTotals).Insert(new PMTaskTotal()
        {
          ProjectID = result.TaskTotal.ProjectID,
          TaskID = result.TaskTotal.TaskID
        });
        PMTaskTotal pmTaskTotal2 = pmTaskTotal1;
        nullable1 = pmTaskTotal2.CuryAsset;
        nullable2 = result.TaskTotal.CuryAsset;
        Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
        Decimal? nullable7;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable7 = nullable2;
        }
        else
          nullable7 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault5);
        pmTaskTotal2.CuryAsset = nullable7;
        PMTaskTotal pmTaskTotal3 = pmTaskTotal1;
        nullable1 = pmTaskTotal3.Asset;
        nullable2 = result.TaskTotal.Asset;
        Decimal valueOrDefault6 = nullable2.GetValueOrDefault();
        Decimal? nullable8;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable8 = nullable2;
        }
        else
          nullable8 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault6);
        pmTaskTotal3.Asset = nullable8;
        PMTaskTotal pmTaskTotal4 = pmTaskTotal1;
        nullable1 = pmTaskTotal4.CuryLiability;
        nullable2 = result.TaskTotal.CuryLiability;
        Decimal valueOrDefault7 = nullable2.GetValueOrDefault();
        Decimal? nullable9;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable9 = nullable2;
        }
        else
          nullable9 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault7);
        pmTaskTotal4.CuryLiability = nullable9;
        PMTaskTotal pmTaskTotal5 = pmTaskTotal1;
        nullable1 = pmTaskTotal5.Liability;
        nullable2 = result.TaskTotal.Liability;
        Decimal valueOrDefault8 = nullable2.GetValueOrDefault();
        Decimal? nullable10;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable10 = nullable2;
        }
        else
          nullable10 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault8);
        pmTaskTotal5.Liability = nullable10;
        PMTaskTotal pmTaskTotal6 = pmTaskTotal1;
        nullable1 = pmTaskTotal6.CuryIncome;
        nullable2 = result.TaskTotal.CuryIncome;
        Decimal valueOrDefault9 = nullable2.GetValueOrDefault();
        Decimal? nullable11;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable11 = nullable2;
        }
        else
          nullable11 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault9);
        pmTaskTotal6.CuryIncome = nullable11;
        PMTaskTotal pmTaskTotal7 = pmTaskTotal1;
        nullable1 = pmTaskTotal7.Income;
        nullable2 = result.TaskTotal.Income;
        Decimal valueOrDefault10 = nullable2.GetValueOrDefault();
        Decimal? nullable12;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable12 = nullable2;
        }
        else
          nullable12 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault10);
        pmTaskTotal7.Income = nullable12;
        PMTaskTotal pmTaskTotal8 = pmTaskTotal1;
        nullable1 = pmTaskTotal8.CuryExpense;
        nullable2 = result.TaskTotal.CuryExpense;
        Decimal valueOrDefault11 = nullable2.GetValueOrDefault();
        Decimal? nullable13;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable13 = nullable2;
        }
        else
          nullable13 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault11);
        pmTaskTotal8.CuryExpense = nullable13;
        PMTaskTotal pmTaskTotal9 = pmTaskTotal1;
        nullable1 = pmTaskTotal9.Expense;
        nullable2 = result.TaskTotal.Expense;
        Decimal valueOrDefault12 = nullable2.GetValueOrDefault();
        Decimal? nullable14;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable14 = nullable2;
        }
        else
          nullable14 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault12);
        pmTaskTotal9.Expense = nullable14;
      }
      if (!isGL)
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
          PMHistoryAccum pmHistoryAccum2 = (PMHistoryAccum) ((PXGraph) je).Caches[typeof (PMHistoryAccum)].Insert((object) pmHistoryAccum1);
          PMHistoryAccum pmHistoryAccum3 = pmHistoryAccum2;
          nullable1 = pmHistoryAccum3.FinPTDCuryAmount;
          nullable2 = pmHistory.FinPTDCuryAmount;
          Decimal valueOrDefault13 = nullable2.GetValueOrDefault();
          Decimal? nullable15;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable15 = nullable2;
          }
          else
            nullable15 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault13);
          pmHistoryAccum3.FinPTDCuryAmount = nullable15;
          PMHistoryAccum pmHistoryAccum4 = pmHistoryAccum2;
          nullable1 = pmHistoryAccum4.FinPTDAmount;
          nullable2 = pmHistory.FinPTDAmount;
          Decimal valueOrDefault14 = nullable2.GetValueOrDefault();
          Decimal? nullable16;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable16 = nullable2;
          }
          else
            nullable16 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault14);
          pmHistoryAccum4.FinPTDAmount = nullable16;
          PMHistoryAccum pmHistoryAccum5 = pmHistoryAccum2;
          nullable1 = pmHistoryAccum5.FinYTDCuryAmount;
          nullable2 = pmHistory.FinYTDCuryAmount;
          Decimal valueOrDefault15 = nullable2.GetValueOrDefault();
          Decimal? nullable17;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable17 = nullable2;
          }
          else
            nullable17 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault15);
          pmHistoryAccum5.FinYTDCuryAmount = nullable17;
          PMHistoryAccum pmHistoryAccum6 = pmHistoryAccum2;
          nullable1 = pmHistoryAccum6.FinYTDAmount;
          nullable2 = pmHistory.FinYTDAmount;
          Decimal valueOrDefault16 = nullable2.GetValueOrDefault();
          Decimal? nullable18;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable18 = nullable2;
          }
          else
            nullable18 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault16);
          pmHistoryAccum6.FinYTDAmount = nullable18;
          PMHistoryAccum pmHistoryAccum7 = pmHistoryAccum2;
          nullable1 = pmHistoryAccum7.FinPTDQty;
          nullable2 = pmHistory.FinPTDQty;
          Decimal valueOrDefault17 = nullable2.GetValueOrDefault();
          Decimal? nullable19;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable19 = nullable2;
          }
          else
            nullable19 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault17);
          pmHistoryAccum7.FinPTDQty = nullable19;
          PMHistoryAccum pmHistoryAccum8 = pmHistoryAccum2;
          nullable1 = pmHistoryAccum8.FinYTDQty;
          nullable2 = pmHistory.FinYTDQty;
          Decimal valueOrDefault18 = nullable2.GetValueOrDefault();
          Decimal? nullable20;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable20 = nullable2;
          }
          else
            nullable20 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault18);
          pmHistoryAccum8.FinYTDQty = nullable20;
          PMHistoryAccum pmHistoryAccum9 = pmHistoryAccum2;
          nullable1 = pmHistoryAccum9.TranPTDCuryAmount;
          nullable2 = pmHistory.TranPTDCuryAmount;
          Decimal valueOrDefault19 = nullable2.GetValueOrDefault();
          Decimal? nullable21;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable21 = nullable2;
          }
          else
            nullable21 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault19);
          pmHistoryAccum9.TranPTDCuryAmount = nullable21;
          PMHistoryAccum pmHistoryAccum10 = pmHistoryAccum2;
          nullable1 = pmHistoryAccum10.TranPTDAmount;
          nullable2 = pmHistory.TranPTDAmount;
          Decimal valueOrDefault20 = nullable2.GetValueOrDefault();
          Decimal? nullable22;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable22 = nullable2;
          }
          else
            nullable22 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault20);
          pmHistoryAccum10.TranPTDAmount = nullable22;
          PMHistoryAccum pmHistoryAccum11 = pmHistoryAccum2;
          nullable1 = pmHistoryAccum11.TranYTDCuryAmount;
          nullable2 = pmHistory.TranYTDCuryAmount;
          Decimal valueOrDefault21 = nullable2.GetValueOrDefault();
          Decimal? nullable23;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable23 = nullable2;
          }
          else
            nullable23 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault21);
          pmHistoryAccum11.TranYTDCuryAmount = nullable23;
          PMHistoryAccum pmHistoryAccum12 = pmHistoryAccum2;
          nullable1 = pmHistoryAccum12.TranYTDAmount;
          nullable2 = pmHistory.TranYTDAmount;
          Decimal valueOrDefault22 = nullable2.GetValueOrDefault();
          Decimal? nullable24;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable24 = nullable2;
          }
          else
            nullable24 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault22);
          pmHistoryAccum12.TranYTDAmount = nullable24;
          PMHistoryAccum pmHistoryAccum13 = pmHistoryAccum2;
          nullable1 = pmHistoryAccum13.TranPTDQty;
          nullable2 = pmHistory.TranPTDQty;
          Decimal valueOrDefault23 = nullable2.GetValueOrDefault();
          Decimal? nullable25;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable25 = nullable2;
          }
          else
            nullable25 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault23);
          pmHistoryAccum13.TranPTDQty = nullable25;
          PMHistoryAccum pmHistoryAccum14 = pmHistoryAccum2;
          nullable1 = pmHistoryAccum14.TranYTDQty;
          nullable2 = pmHistory.TranYTDQty;
          Decimal valueOrDefault24 = nullable2.GetValueOrDefault();
          Decimal? nullable26;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable26 = nullable2;
          }
          else
            nullable26 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault24);
          pmHistoryAccum14.TranYTDQty = nullable26;
        }
      }
    }
  }

  protected virtual PMBudgetAccum ExtractBudget(
    PMBudget targetBudget,
    PMTran tran,
    PMProject project,
    PMTask task)
  {
    PMBudgetAccum budget = this.ExtractBudget(targetBudget, tran);
    if (budget.Type == "E" && task != null && task.Type == "CostRev")
      budget.RevenueTaskID = budget.ProjectTaskID;
    return budget;
  }

  /// Keeping for backward compatibility - Used by customization see AC-192409.
  protected virtual PMBudgetAccum ExtractBudget(PMBudget targetBudget, PMTran tran)
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

  protected virtual void UpdateActuals(
    JournalEntryProjectExt journalEntryExt,
    PMBudget targetBudget,
    PMTran tran,
    PMProject project,
    PMTask task)
  {
    PMBudgetAccum budget = this.ExtractBudget(targetBudget, tran, project, task);
    PMBudgetAccum pmBudgetAccum1 = ((PXSelectBase<PMBudgetAccum>) journalEntryExt.ProjectBudget).Insert(budget);
    PMBudgetAccum pmBudgetAccum2 = pmBudgetAccum1;
    Decimal? nullable1 = pmBudgetAccum2.ActualQty;
    Decimal valueOrDefault1 = targetBudget.ActualQty.GetValueOrDefault();
    pmBudgetAccum2.ActualQty = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
    PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum1;
    nullable1 = pmBudgetAccum3.CuryActualAmount;
    Decimal valueOrDefault2 = targetBudget.CuryActualAmount.GetValueOrDefault();
    pmBudgetAccum3.CuryActualAmount = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
    PMBudgetAccum pmBudgetAccum4 = pmBudgetAccum1;
    nullable1 = pmBudgetAccum4.ActualAmount;
    Decimal valueOrDefault3 = targetBudget.ActualAmount.GetValueOrDefault();
    pmBudgetAccum4.ActualAmount = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault3) : new Decimal?();
    PMHistoryByDateAccum historyByDateAccum1 = new PMHistoryByDateAccum();
    historyByDateAccum1.ProjectID = tran.ProjectID;
    historyByDateAccum1.ProjectTaskID = tran.TaskID;
    PMHistoryByDateAccum historyByDateAccum2 = historyByDateAccum1;
    int? nullable2 = tran.AccountGroupID;
    int? nullable3 = nullable2 ?? pmBudgetAccum1.AccountGroupID;
    historyByDateAccum2.AccountGroupID = nullable3;
    PMHistoryByDateAccum historyByDateAccum3 = historyByDateAccum1;
    nullable2 = tran.InventoryID;
    int? nullable4 = nullable2 ?? pmBudgetAccum1.InventoryID;
    historyByDateAccum3.InventoryID = nullable4;
    PMHistoryByDateAccum historyByDateAccum4 = historyByDateAccum1;
    nullable2 = tran.CostCodeID;
    int? nullable5 = nullable2 ?? pmBudgetAccum1.CostCodeID;
    historyByDateAccum4.CostCodeID = nullable5;
    historyByDateAccum1.Date = tran.Date;
    historyByDateAccum1.PeriodID = tran.TranPeriodID;
    PMHistoryByDateAccum historyByDateAccum5 = ((PXSelectBase<PMHistoryByDateAccum>) journalEntryExt.ProjectHistoryByDate).Insert(historyByDateAccum1);
    PMHistoryByDateAccum historyByDateAccum6 = historyByDateAccum5;
    nullable1 = historyByDateAccum6.ActualQty;
    Decimal? nullable6 = targetBudget.ActualQty;
    Decimal valueOrDefault4 = nullable6.GetValueOrDefault();
    Decimal? nullable7;
    if (!nullable1.HasValue)
    {
      nullable6 = new Decimal?();
      nullable7 = nullable6;
    }
    else
      nullable7 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault4);
    historyByDateAccum6.ActualQty = nullable7;
    PMHistoryByDateAccum historyByDateAccum7 = historyByDateAccum5;
    nullable1 = historyByDateAccum7.CuryActualAmount;
    nullable6 = targetBudget.CuryActualAmount;
    Decimal valueOrDefault5 = nullable6.GetValueOrDefault();
    Decimal? nullable8;
    if (!nullable1.HasValue)
    {
      nullable6 = new Decimal?();
      nullable8 = nullable6;
    }
    else
      nullable8 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault5);
    historyByDateAccum7.CuryActualAmount = nullable8;
    PMHistoryByDateAccum historyByDateAccum8 = historyByDateAccum5;
    nullable1 = historyByDateAccum8.ActualAmount;
    nullable6 = targetBudget.ActualAmount;
    Decimal valueOrDefault6 = nullable6.GetValueOrDefault();
    Decimal? nullable9;
    if (!nullable1.HasValue)
    {
      nullable6 = new Decimal?();
      nullable9 = nullable6;
    }
    else
      nullable9 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault6);
    historyByDateAccum8.ActualAmount = nullable9;
  }

  public virtual ProjectBalance CreateProjectBalance() => new ProjectBalance((PXGraph) this);

  /// <summary>
  /// The key of the dictionary is a BranchID.FinPeriodID key.
  /// </summary>
  private Dictionary<string, List<RegisterReleaseProcess.TranInfo>> GetTransByBranchAndFinPeriod(
    PMRegister doc)
  {
    Dictionary<string, List<RegisterReleaseProcess.TranInfo>> branchAndFinPeriod = new Dictionary<string, List<RegisterReleaseProcess.TranInfo>>();
    foreach (PXResult<PMTran, PX.Objects.GL.Account, PMProject, PMTask, RegisterReleaseProcess.OffsetAccount, PMAccountGroup, RegisterReleaseProcess.OffsetPMAccountGroup> res in ((PXSelectBase<PMTran>) new PXSelectJoin<PMTran, LeftJoin<PX.Objects.GL.Account, On<PMTran.accountID, Equal<PX.Objects.GL.Account.accountID>>, InnerJoin<PMProject, On<PMProject.contractID, Equal<PMTran.projectID>>, LeftJoin<PMTask, On<PMTask.projectID, Equal<PMTran.projectID>, And<PMTask.taskID, Equal<PMTran.taskID>>>, LeftJoin<RegisterReleaseProcess.OffsetAccount, On<PMTran.offsetAccountID, Equal<RegisterReleaseProcess.OffsetAccount.accountID>>, LeftJoin<PMAccountGroup, On<PMAccountGroup.groupID, Equal<PMTran.accountGroupID>>, LeftJoin<RegisterReleaseProcess.OffsetPMAccountGroup, On<RegisterReleaseProcess.OffsetPMAccountGroup.groupID, Equal<PMTran.offsetAccountGroupID>>>>>>>>, Where<PMTran.tranType, Equal<Required<PMTran.tranType>>, And<PMTran.refNbr, Equal<Required<PMTran.refNbr>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) doc.Module,
      (object) doc.RefNbr
    }))
    {
      RegisterReleaseProcess.TranInfo tranInfo = new RegisterReleaseProcess.TranInfo(res);
      BaseProjectTaskAttribute.CheckPermissionForInactiveTask(tranInfo.Task);
      string key = $"{tranInfo.Tran.BranchID.GetValueOrDefault()}.{tranInfo.Tran.FinPeriodID}.{tranInfo.Tran.TranCuryID}";
      if (branchAndFinPeriod.ContainsKey(key))
        branchAndFinPeriod[key].Add(tranInfo);
      else
        branchAndFinPeriod.Add(key, new List<RegisterReleaseProcess.TranInfo>()
        {
          tranInfo
        });
    }
    return branchAndFinPeriod;
  }

  /// <summary>
  /// Increases the counter of unbilled transactions.
  /// Billed and Reversed transactions are ignored.
  /// Note: Although the method will add all necessary caches to the views collection of the graph to be saved on persist.
  /// This will work only if the graph is persisted within the context of current request. If there will be graph load/unload
  /// between this call and the Persist please add the required Caches to the graph manualy.
  /// </summary>
  public static void AddToUnbilledSummary(PXGraph graph, PMTran tran)
  {
    if (tran.Billed.GetValueOrDefault() || tran.ExcludedFromBilling.GetValueOrDefault())
      return;
    RegisterReleaseProcess.UpdateUnbilledSummary(graph, tran, false);
  }

  /// <summary>
  /// Decreases the counter of unbilled transactions.
  /// Only Billed or Reversed transactions are processed.
  /// Note: Although the method will add all necessary caches to the views collection of the graph to be saved on persist.
  /// This will work only if the graph is persisted within the context of current request. If there will be graph load/unload
  /// between this call and the Persist please add the required Caches to the graph manualy.
  /// </summary>
  public static void SubtractFromUnbilledSummary(PXGraph graph, PMTran tran)
  {
    if (!tran.Billed.GetValueOrDefault() && !tran.ExcludedFromBilling.GetValueOrDefault())
      return;
    RegisterReleaseProcess.UpdateUnbilledSummary(graph, tran, true);
  }

  private static void UpdateUnbilledSummary(PXGraph graph, PMTran tran, bool reverse)
  {
    if (!tran.ProjectID.HasValue || !tran.TaskID.HasValue || !tran.AccountGroupID.HasValue || !tran.Date.HasValue)
      return;
    graph.Views.Caches.Add(typeof (PMUnbilledDailySummaryAccum));
    int num1 = reverse ? -1 : 1;
    PMUnbilledDailySummaryAccum dailySummaryAccum1 = new PMUnbilledDailySummaryAccum();
    dailySummaryAccum1.ProjectID = tran.ProjectID;
    dailySummaryAccum1.TaskID = tran.TaskID;
    dailySummaryAccum1.AccountGroupID = tran.AccountGroupID;
    dailySummaryAccum1.Date = new DateTime?(tran.Date.Value.Date);
    PMUnbilledDailySummaryAccum dailySummaryAccum2 = (PMUnbilledDailySummaryAccum) graph.Caches[typeof (PMUnbilledDailySummaryAccum)].Insert((object) dailySummaryAccum1);
    PMUnbilledDailySummaryAccum dailySummaryAccum3 = dailySummaryAccum2;
    int? billable1 = dailySummaryAccum3.Billable;
    bool? billable2 = tran.Billable;
    int num2 = billable2.GetValueOrDefault() ? num1 : 0;
    dailySummaryAccum3.Billable = billable1.HasValue ? new int?(billable1.GetValueOrDefault() + num2) : new int?();
    PMUnbilledDailySummaryAccum dailySummaryAccum4 = dailySummaryAccum2;
    int? nonBillable = dailySummaryAccum4.NonBillable;
    billable2 = tran.Billable;
    int num3 = billable2.GetValueOrDefault() ? 0 : num1;
    dailySummaryAccum4.NonBillable = nonBillable.HasValue ? new int?(nonBillable.GetValueOrDefault() + num3) : new int?();
  }

  private class TranInfo
  {
    public PMTran Tran { get; private set; }

    public PX.Objects.GL.Account Account { get; private set; }

    public PMAccountGroup AccountGroup { get; private set; }

    public RegisterReleaseProcess.OffsetAccount OffsetAccount { get; private set; }

    public RegisterReleaseProcess.OffsetPMAccountGroup OffsetAccountGroup { get; private set; }

    public PMProject Project { get; private set; }

    public PMTask Task { get; private set; }

    public TranInfo(
      PXResult<PMTran, PX.Objects.GL.Account, PMProject, PMTask, RegisterReleaseProcess.OffsetAccount, PMAccountGroup, RegisterReleaseProcess.OffsetPMAccountGroup> res)
    {
      this.Tran = PXResult<PMTran, PX.Objects.GL.Account, PMProject, PMTask, RegisterReleaseProcess.OffsetAccount, PMAccountGroup, RegisterReleaseProcess.OffsetPMAccountGroup>.op_Implicit(res);
      this.Account = PXResult<PMTran, PX.Objects.GL.Account, PMProject, PMTask, RegisterReleaseProcess.OffsetAccount, PMAccountGroup, RegisterReleaseProcess.OffsetPMAccountGroup>.op_Implicit(res);
      this.AccountGroup = PXResult<PMTran, PX.Objects.GL.Account, PMProject, PMTask, RegisterReleaseProcess.OffsetAccount, PMAccountGroup, RegisterReleaseProcess.OffsetPMAccountGroup>.op_Implicit(res);
      this.OffsetAccount = PXResult<PMTran, PX.Objects.GL.Account, PMProject, PMTask, RegisterReleaseProcess.OffsetAccount, PMAccountGroup, RegisterReleaseProcess.OffsetPMAccountGroup>.op_Implicit(res);
      this.OffsetAccountGroup = PXResult<PMTran, PX.Objects.GL.Account, PMProject, PMTask, RegisterReleaseProcess.OffsetAccount, PMAccountGroup, RegisterReleaseProcess.OffsetPMAccountGroup>.op_Implicit(res);
      this.Project = PXResult<PMTran, PX.Objects.GL.Account, PMProject, PMTask, RegisterReleaseProcess.OffsetAccount, PMAccountGroup, RegisterReleaseProcess.OffsetPMAccountGroup>.op_Implicit(res);
      this.Task = PXResult<PMTran, PX.Objects.GL.Account, PMProject, PMTask, RegisterReleaseProcess.OffsetAccount, PMAccountGroup, RegisterReleaseProcess.OffsetPMAccountGroup>.op_Implicit(res);
    }
  }

  [PXHidden]
  [PXBreakInheritance]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class OffsetAccount : PX.Objects.GL.Account
  {
    public new abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RegisterReleaseProcess.OffsetAccount.accountID>
    {
    }

    public new abstract class accountCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RegisterReleaseProcess.OffsetAccount.accountCD>
    {
    }

    public new abstract class accountGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RegisterReleaseProcess.OffsetAccount.accountGroupID>
    {
    }
  }

  [PXHidden]
  [PXBreakInheritance]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class OffsetPMAccountGroup : PMAccountGroup
  {
    public new abstract class groupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RegisterReleaseProcess.OffsetPMAccountGroup.groupID>
    {
    }

    public new abstract class groupCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RegisterReleaseProcess.OffsetPMAccountGroup.groupCD>
    {
    }

    public new abstract class type : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RegisterReleaseProcess.OffsetPMAccountGroup.type>
    {
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRSingleProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.Standalone;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Discount.Mappers;
using PX.Objects.DR.Descriptor;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.DR;

public class DRSingleProcess : 
  DRProcess,
  ISalesPriceProvider,
  ISingleScheduleViewProvider,
  IDRDataProvider
{
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<DRSchedule.curyInfoID>>>> CuryInfo;
  public PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.locationID, Equal<Current<DRSchedule.bAccountLocID>>>> BAccountLocation;
  public PXSelectJoin<PX.Objects.AR.ARTran, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.AR.ARTran.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>, InnerJoin<DRDeferredCode, On<PX.Objects.AR.ARTran.deferredCode, Equal<DRDeferredCode.deferredCodeID>>, LeftJoin<INComponent, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<INComponent.inventoryID>>, LeftJoin<DRSingleProcess.ComponentINItem, On<INComponent.componentID, Equal<DRSingleProcess.ComponentINItem.inventoryID>>, LeftJoin<DRSingleProcess.ComponentDeferredCode, On<INComponent.deferredCode, Equal<DRSingleProcess.ComponentDeferredCode.deferredCodeID>>>>>>>, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<DRSchedule.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<DRSchedule.refNbr>>>>, OrderBy<Asc<PX.Objects.AR.ARTran.tranType, Asc<PX.Objects.AR.ARTran.refNbr, Asc<PX.Objects.AR.ARTran.lineNbr, Asc<INComponent.amtOptionASC606>>>>>> ARTransactionWithItems;

  public virtual void CreateSingleSchedule(
    PX.Objects.AR.ARInvoice originalDocument,
    ARReleaseProcess.Amount lineTotal,
    int? defScheduleID,
    bool isDraft)
  {
    DRProcess.DRScheduleParameters scheduleParameters = DRSingleProcess.GetSingleScheduleParameters(originalDocument);
    this.CreateSingleSchedule<PX.Objects.AR.ARInvoice>(originalDocument, lineTotal, defScheduleID, isDraft, scheduleParameters);
  }

  public virtual void CreateSingleSchedule(
    ARCashSale originalDocument,
    ARReleaseProcess.Amount lineTotal,
    int? defScheduleID,
    bool isDraft)
  {
    DRProcess.DRScheduleParameters scheduleParameters = DRSingleProcess.GetSingleScheduleParameters(originalDocument);
    this.CreateSingleSchedule<ARCashSale>(originalDocument, lineTotal, defScheduleID, isDraft, scheduleParameters);
  }

  private void CreateSingleSchedule<T>(
    T originalDocument,
    ARReleaseProcess.Amount lineTotal,
    int? DefScheduleID,
    bool isDraft,
    DRProcess.DRScheduleParameters scheduleParameters)
    where T : PX.Objects.AR.ARRegister
  {
    PXResultset<ARSetup>.op_Implicit(PXSelectBase<ARSetup, PXSelect<ARSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    DRSchedule schedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.module, Equal<BatchModule.moduleAR>, And<DRSchedule.docType, Equal<Required<PX.Objects.AR.ARRegister.docType>>, And<DRSchedule.refNbr, Equal<Required<PX.Objects.AR.ARRegister.refNbr>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[3]
    {
      (object) originalDocument.DocType,
      (object) originalDocument.RefNbr,
      (object) 1
    }));
    if (schedule != null && schedule.LineNbr.HasValue)
      throw new PXException("The operation cannot be completed because the Revenue Recognition by IFRS 15/ASC606 feature is enabled. To continue, re-configure the system as described in the Recognition of Revenue from Customer Contracts section in Help or do one of the following:~- Delete the DR codes and DR schedules generated before the feature was enabled.~- Disable the feature.");
    if (!DefScheduleID.HasValue)
    {
      PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.locationID, Equal<Required<DRSchedule.bAccountLocID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        (object) scheduleParameters.BAccountLocID
      }));
      PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<DRSchedule.curyInfoID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        (object) scheduleParameters.CuryInfoID
      }));
      scheduleParameters.BaseCuryID = currencyInfo.BaseCuryID;
      SingleScheduleCreator singleScheduleCreator = new SingleScheduleCreator((IDREntityStorage) this, (ISubaccountProvider) new DRProcess.ARSubaccountProvider((PXGraph) this), (IBusinessAccountProvider) this, (IInventoryItemProvider) this, (ISingleScheduleViewProvider) this, (ISalesPriceProvider) this, (IDRDataProvider) this, this.FinPeriodRepository, (Func<Decimal, Decimal>) (x => PXDBCurrencyAttribute.Round(((PXSelectBase) this.Schedule).Cache, (object) ((PXSelectBase<DRSchedule>) this.Schedule).Current, x, CMPrecision.TRANCURY)), originalDocument.BranchID, isDraft, location, currencyInfo);
      if (schedule == null)
        singleScheduleCreator.CreateOriginalSchedule(scheduleParameters, lineTotal);
      else
        singleScheduleCreator.ReevaluateSchedule(schedule, scheduleParameters, lineTotal, false);
    }
    else
    {
      if (!(originalDocument.DocType == "CRM") && !(originalDocument.DocType == "DRM") && (!(originalDocument.OrigModule == "SO") || !(originalDocument.DocType == "INV") || string.IsNullOrEmpty(originalDocument.OrigRefNbr)))
        return;
      bool accountForPostedTransactions = originalDocument.DocType == "CRM";
      if (schedule == null)
      {
        DRSchedule copy = ((IDREntityStorage) this).CreateCopy((DRSchedule) scheduleParameters);
        copy.IsDraft = new bool?(isDraft);
        copy.IsCustom = new bool?(false);
        this.FillRelatedSingleSchedule(((PXSelectBase<DRSchedule>) this.Schedule).Insert(copy), scheduleParameters, DefScheduleID, lineTotal, isDraft, accountForPostedTransactions);
      }
      else
      {
        PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.locationID, Equal<Required<DRSchedule.bAccountLocID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
        {
          (object) scheduleParameters.BAccountLocID
        }));
        PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<DRSchedule.curyInfoID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
        {
          (object) scheduleParameters.CuryInfoID
        }));
        scheduleParameters.BaseCuryID = currencyInfo.BaseCuryID;
        new SingleScheduleCreator((IDREntityStorage) this, (ISubaccountProvider) new DRProcess.ARSubaccountProvider((PXGraph) this), (IBusinessAccountProvider) this, (IInventoryItemProvider) this, (ISingleScheduleViewProvider) this, (ISalesPriceProvider) this, (IDRDataProvider) this, this.FinPeriodRepository, (Func<Decimal, Decimal>) (x => PXCurrencyAttribute.BaseRound((PXGraph) this, x)), originalDocument.BranchID, isDraft, location, currencyInfo).ReevaluateSchedule(schedule, scheduleParameters, lineTotal, true);
        this.FillRelatedSingleSchedule(this.GetDeferralSchedule(schedule.ScheduleID), scheduleParameters, DefScheduleID, lineTotal, isDraft, accountForPostedTransactions);
      }
    }
  }

  public static DRProcess.DRScheduleParameters GetSingleScheduleParameters(PX.Objects.AR.ARInvoice document)
  {
    DRProcess.DRScheduleParameters scheduleParameters = new DRProcess.DRScheduleParameters();
    scheduleParameters.Module = "AR";
    scheduleParameters.DocType = document.DocType;
    scheduleParameters.RefNbr = document.RefNbr;
    scheduleParameters.DocDate = document.DocDate;
    scheduleParameters.BAccountID = document.CustomerID;
    scheduleParameters.BAccountLocID = document.CustomerLocationID;
    scheduleParameters.FinPeriodID = document.FinPeriodID;
    scheduleParameters.TranDesc = document.DocDesc;
    scheduleParameters.CuryID = document.CuryID;
    scheduleParameters.CuryInfoID = document.CuryInfoID;
    scheduleParameters.ProjectID = document.ProjectID;
    scheduleParameters.BaseCuryID = PXAccess.GetBranch(document.BranchID).BaseCuryID;
    return scheduleParameters;
  }

  public static DRProcess.DRScheduleParameters GetSingleScheduleParameters(ARCashSale document)
  {
    DRProcess.DRScheduleParameters scheduleParameters = new DRProcess.DRScheduleParameters();
    scheduleParameters.Module = "AR";
    scheduleParameters.DocType = document.DocType;
    scheduleParameters.RefNbr = document.RefNbr;
    scheduleParameters.DocDate = document.DocDate;
    scheduleParameters.BAccountID = document.CustomerID;
    scheduleParameters.BAccountLocID = document.CustomerLocationID;
    scheduleParameters.FinPeriodID = document.TranPeriodID;
    scheduleParameters.TranDesc = document.DocDesc;
    scheduleParameters.CuryID = document.CuryID;
    scheduleParameters.CuryInfoID = document.CuryInfoID;
    scheduleParameters.ProjectID = document.ProjectID;
    scheduleParameters.BaseCuryID = PXAccess.GetBranch(document.BranchID).BaseCuryID;
    return scheduleParameters;
  }

  private DRSchedule GetDeferralSchedule(int? scheduleID)
  {
    return PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.scheduleID, Equal<Required<DRSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) scheduleID
    }));
  }

  private void FillRelatedSingleSchedule(
    DRSchedule relatedSchedule,
    DRProcess.DRScheduleParameters scheduleParameters,
    int? defScheduleID,
    ARReleaseProcess.Amount tranAmt,
    bool isDraft,
    bool accountForPostedTransactions)
  {
    Decimal valueOrDefault = PXResultset<DRScheduleDetail>.op_Implicit(PXSelectBase<DRScheduleDetail, PXSelectGroupBy<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRScheduleDetail.scheduleID>>>, Aggregate<Sum<DRScheduleDetail.curyTotalAmt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) this.GetDeferralSchedule(defScheduleID).ScheduleID
    })).CuryTotalAmt.GetValueOrDefault();
    Decimal num1 = tranAmt.Cury.Value;
    Decimal num2 = 0M;
    foreach (PXResult<DRScheduleDetail, PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, INComponent, DRDeferredCode> pxResult in PXSelectBase<DRScheduleDetail, PXSelectJoin<DRScheduleDetail, LeftJoin<PX.Objects.AR.ARTran, On<DRScheduleDetail.lineNbr, Equal<PX.Objects.AR.ARTran.lineNbr>>, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.AR.ARTran.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>, LeftJoin<INComponent, On<DRScheduleDetail.componentID, Equal<INComponent.inventoryID>>, InnerJoin<DRDeferredCode, On<PX.Objects.AR.ARTran.deferredCode, Equal<DRDeferredCode.deferredCodeID>>>>>>, Where<DRScheduleDetail.scheduleID, Equal<Required<DRSchedule.scheduleID>>, And<PX.Objects.AR.ARTran.tranType, Equal<Required<DRSchedule.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<DRSchedule.refNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) defScheduleID,
      (object) scheduleParameters.DocType,
      (object) scheduleParameters.RefNbr
    }))
    {
      DRScheduleDetail drScheduleDetail1 = PXResult<DRScheduleDetail, PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, INComponent, DRDeferredCode>.op_Implicit(pxResult);
      PX.Objects.AR.ARTran arTran = PXResult<DRScheduleDetail, PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, INComponent, DRDeferredCode>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem1 = PXResult<DRScheduleDetail, PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, INComponent, DRDeferredCode>.op_Implicit(pxResult);
      INComponent inComponent = PXResult<DRScheduleDetail, PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, INComponent, DRDeferredCode>.op_Implicit(pxResult);
      DRDeferredCode defCode1 = PXResult<DRScheduleDetail, PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, INComponent, DRDeferredCode>.op_Implicit(pxResult);
      Decimal? nullable;
      Decimal num3;
      if (!(valueOrDefault == 0M))
      {
        nullable = drScheduleDetail1.CuryTotalAmt;
        num3 = nullable.Value * num1 / valueOrDefault;
      }
      else
        num3 = 0M;
      Decimal num4 = num3;
      Decimal amount = PXDBCurrencyAttribute.BaseRound((PXGraph) this, num4);
      Decimal num5 = 0M;
      if (accountForPostedTransactions)
      {
        nullable = drScheduleDetail1.CuryTotalAmt;
        if (nullable.Value != 0M)
        {
          Decimal num6 = num4;
          nullable = drScheduleDetail1.CuryTotalAmt;
          Decimal num7 = nullable.Value;
          nullable = drScheduleDetail1.CuryDefAmt;
          Decimal num8 = nullable.Value;
          Decimal num9 = num7 - num8;
          Decimal num10 = num6 * num9;
          nullable = drScheduleDetail1.CuryTotalAmt;
          Decimal num11 = nullable.Value;
          num5 = num10 / num11;
        }
      }
      Decimal amountToDistributeForPosted = PXDBCurrencyAttribute.BaseRound((PXGraph) this, num5);
      Decimal amountToDistributeForUnposted = PXDBCurrencyAttribute.BaseRound((PXGraph) this, num4 - amountToDistributeForPosted);
      INComponent component = (INComponent) null;
      DRDeferredCode defCode2 = (DRDeferredCode) null;
      if (inventoryItem1 != null && inComponent != null)
      {
        component = this.GetInventoryItemComponent(inventoryItem1.InventoryID, drScheduleDetail1.ComponentID);
        if (component != null)
          defCode2 = PXResultset<DRDeferredCode>.op_Implicit(PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Required<DRDeferredCode.deferredCodeID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) component.DeferredCode
          }));
      }
      PX.Objects.IN.InventoryItem inventoryItem2 = this.GetInventoryItem(drScheduleDetail1.ComponentID);
      DRScheduleDetail drScheduleDetail2 = defCode2 == null ? this.InsertScheduleDetail(arTran.BranchID, relatedSchedule, inventoryItem2 == null ? new int?(0) : inventoryItem2.InventoryID, defCode1, amount, drScheduleDetail1.DefAcctID, drScheduleDetail1.DefSubID, arTran.AccountID, arTran.SubID, isDraft, arTran.DRTermStartDate, arTran.DRTermEndDate) : this.InsertScheduleDetail(arTran.BranchID, relatedSchedule, component, inventoryItem2, defCode2, amount, drScheduleDetail1.DefAcctID, drScheduleDetail1.DefSubID, isDraft, arTran.DRTermStartDate, arTran.DRTermEndDate);
      num2 += amount;
      IList<DRScheduleTran> tranList = (IList<DRScheduleTran>) new List<DRScheduleTran>();
      DRDeferredCode deferralCode = defCode2 ?? defCode1;
      IEnumerable<DRScheduleTran> originalPostedTransactions = (IEnumerable<DRScheduleTran>) null;
      if (accountForPostedTransactions)
        originalPostedTransactions = GraphHelper.RowCast<DRScheduleTran>((IEnumerable) PXSelectBase<DRScheduleTran, PXSelect<DRScheduleTran, Where<DRScheduleTran.status, Equal<DRScheduleTranStatus.PostedStatus>, And<DRScheduleTran.scheduleID, Equal<Required<DRScheduleTran.scheduleID>>, And<DRScheduleTran.componentID, Equal<Required<DRScheduleTran.componentID>>, And<DRScheduleTran.detailLineNbr, Equal<Required<DRScheduleTran.detailLineNbr>>, And<DRScheduleTran.lineNbr, NotEqual<Required<DRScheduleTran.lineNbr>>>>>>>>.Config>.Select((PXGraph) this, new object[4]
        {
          (object) drScheduleDetail1.ScheduleID,
          (object) drScheduleDetail1.ComponentID,
          (object) drScheduleDetail1.DetailLineNbr,
          (object) drScheduleDetail1.CreditLineNbr
        }));
      if (amountToDistributeForUnposted != 0M || accountForPostedTransactions && amountToDistributeForPosted != 0M)
      {
        IEnumerable<DRScheduleTran> originalOpenTransactions = GraphHelper.RowCast<DRScheduleTran>((IEnumerable) PXSelectBase<DRScheduleTran, PXSelect<DRScheduleTran, Where<DRScheduleTran.status, Equal<Required<DRScheduleTran.status>>, And<DRScheduleTran.scheduleID, Equal<Required<DRScheduleTran.scheduleID>>, And<DRScheduleTran.componentID, Equal<Required<DRScheduleTran.componentID>>, And<DRScheduleTran.detailLineNbr, Equal<Required<DRScheduleTran.detailLineNbr>>>>>>>.Config>.Select((PXGraph) this, new object[4]
        {
          (object) (deferralCode.Method == "C" ? "J" : "O"),
          (object) drScheduleDetail1.ScheduleID,
          (object) drScheduleDetail1.ComponentID,
          (object) drScheduleDetail1.DetailLineNbr
        }));
        foreach (DRScheduleTran relatedTransaction in (IEnumerable<DRScheduleTran>) this.GetTransactionsGenerator(deferralCode).GenerateRelatedTransactions(drScheduleDetail2, originalOpenTransactions, originalPostedTransactions, amountToDistributeForUnposted, amountToDistributeForPosted, arTran.BranchID))
        {
          ((PXSelectBase<DRScheduleTran>) this.Transactions).Insert(relatedTransaction);
          tranList.Add(relatedTransaction);
        }
      }
      this.UpdateBalanceProjection((IEnumerable<DRScheduleTran>) tranList, drScheduleDetail2, defCode1.AccountType);
    }
  }

  private void UpdateOriginalSingleSchedule(PX.Objects.AR.ARRegister originalDocument, Decimal amount)
  {
    foreach (PXResult<PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, INComponent, DRDeferredCode> pxResult in ((PXSelectBase<PX.Objects.AR.ARTran>) this.ARTransactionWithItems).Select(Array.Empty<object>()))
    {
      PX.Objects.AR.ARTran tran = PXResult<PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, INComponent, DRDeferredCode>.op_Implicit(pxResult);
      PXResult<PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, INComponent, DRDeferredCode>.op_Implicit(pxResult);
      PXResult<PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, INComponent, DRDeferredCode>.op_Implicit(pxResult);
      DRDeferredCode defCode = PXResult<PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, INComponent, DRDeferredCode>.op_Implicit(pxResult);
      this.UpdateOriginalSchedule(tran, defCode, amount, originalDocument.DocDate, originalDocument.FinPeriodID, originalDocument.CustomerID, originalDocument.CustomerLocationID);
    }
  }

  public PXResultset<DRScheduleDetail> GetScheduleDetailsResultset(int? scheduleID)
  {
    return PXSelectBase<DRScheduleDetail, PXSelectJoin<DRScheduleDetail, InnerJoin<DRDeferredCode, On<DRScheduleDetail.defCode, Equal<DRDeferredCode.deferredCodeID>>, LeftJoin<INComponent, On<DRScheduleDetail.componentID, Equal<INComponent.inventoryID>>>>, Where<DRScheduleDetail.scheduleID, Equal<Required<DRScheduleDetail.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) scheduleID
    });
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2022R2.")]
  public virtual void SetFairValueSalesPrice(
    DRScheduleDetail scheduleDetail,
    PX.Objects.CR.Location location,
    PX.Objects.CM.CurrencyInfo currencyInfo)
  {
    DRSingleProcess.SetFairValueSalesPrice(((PXSelectBase<DRSchedule>) this.Schedule).Current, scheduleDetail, (PXSelectBase<DRScheduleDetail>) this.ScheduleDetail, location, currencyInfo, ((PXSelectBase<DRSetup>) this.Setup).Current.UseFairValuePricesInBaseCurrency.Value);
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2022R2.")]
  public static void SetFairValueSalesPrice(
    DRSchedule schedule,
    DRScheduleDetail scheduleDetail,
    PXSelectBase<DRScheduleDetail> scheduleDetailsView,
    PX.Objects.CR.Location location,
    PX.Objects.CM.CurrencyInfo currencyInfo,
    bool takeInBaseCurrency)
  {
    if (takeInBaseCurrency)
      currencyInfo.CuryID = currencyInfo.BaseCuryID;
    string taxCalcMode = location?.CTaxCalcMode ?? "T";
    ARSalesPriceMaint.SalesPriceItem fairValueSalesPrice = ARSalesPriceMaint.CalculateFairValueSalesPrice(((PXSelectBase) scheduleDetailsView).Cache, location?.CPriceClassID, schedule.BAccountID, scheduleDetail.ComponentID, currencyInfo, scheduleDetail.Qty, scheduleDetail.UOM, schedule.DocDate.Value, takeInBaseCurrency, taxCalcMode);
    Decimal price = fairValueSalesPrice.Price;
    if (price == 0M)
    {
      PX.Objects.IN.InventoryItem inventoryItem1 = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select(((PXSelectBase) scheduleDetailsView).Cache.Graph, new object[1]
      {
        (object) scheduleDetail.ComponentID
      }));
      if (!scheduleDetail.ParentInventoryID.HasValue)
        throw new NoFairValuePriceFoundException(inventoryItem1.InventoryCD, scheduleDetail.UOM, currencyInfo.CuryID, schedule.DocDate.Value);
      PX.Objects.IN.InventoryItem inventoryItem2 = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select(((PXSelectBase) scheduleDetailsView).Cache.Graph, new object[1]
      {
        (object) scheduleDetail.ParentInventoryID
      }));
      throw new NoFairValuePriceFoundException(inventoryItem1.InventoryCD, inventoryItem2.InventoryCD, scheduleDetail.UOM, currencyInfo.CuryID, schedule.DocDate.Value);
    }
    scheduleDetail.FairValueCuryID = fairValueSalesPrice.CuryID;
    scheduleDetail.FairValuePrice = new Decimal?(price);
    DRScheduleDetail drScheduleDetail = scheduleDetail;
    Decimal num = price;
    Decimal? nullable1 = fairValueSalesPrice.Prorated ? scheduleDetail.CoTermRate : new Decimal?((Decimal) 1);
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num * nullable1.GetValueOrDefault()) : new Decimal?();
    drScheduleDetail.EffectiveFairValuePrice = nullable2;
  }

  public virtual void SetFairValueSalesPrice(
    DRScheduleDetail scheduleDetail,
    PX.Objects.AR.ARTran artran,
    PX.Objects.CR.Location location,
    PX.Objects.CM.CurrencyInfo currencyInfo)
  {
    DRSingleProcess.SetFairValueSalesPrice(((PXSelectBase<DRSchedule>) this.Schedule).Current, scheduleDetail, (PXSelectBase<DRScheduleDetail>) this.ScheduleDetail, artran, (PXSelectBase<PX.Objects.AR.ARTran>) this.ARTransactionWithItems, location, currencyInfo, ((PXSelectBase<DRSetup>) this.Setup).Current.UseFairValuePricesInBaseCurrency.Value);
  }

  public static void SetFairValueSalesPrice(
    DRSchedule schedule,
    DRScheduleDetail scheduleDetail,
    PXSelectBase<DRScheduleDetail> scheduleDetailsView,
    PX.Objects.AR.ARTran artran,
    PXSelectBase<PX.Objects.AR.ARTran> artranView,
    PX.Objects.CR.Location location,
    PX.Objects.CM.CurrencyInfo currencyInfo,
    bool takeInBaseCurrency)
  {
    if (takeInBaseCurrency)
      currencyInfo.CuryID = currencyInfo.BaseCuryID;
    string taxCalcMode = location?.CTaxCalcMode ?? "T";
    ARSalesPriceMaint.SalesPriceItem fairValueSalesPrice = ARSalesPriceMaint.CalculateFairValueSalesPrice(((PXSelectBase) scheduleDetailsView).Cache, location?.CPriceClassID, schedule.BAccountID, scheduleDetail.ComponentID, currencyInfo, scheduleDetail.Qty, scheduleDetail.UOM, schedule.DocDate.Value, takeInBaseCurrency, taxCalcMode);
    Decimal price = fairValueSalesPrice.Price;
    if (price == 0M)
    {
      PX.Objects.IN.InventoryItem inventoryItem1 = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select(((PXSelectBase) scheduleDetailsView).Cache.Graph, new object[1]
      {
        (object) scheduleDetail.ComponentID
      }));
      if (!scheduleDetail.ParentInventoryID.HasValue)
        throw new NoFairValuePriceFoundException(inventoryItem1.InventoryCD, scheduleDetail.UOM, currencyInfo.CuryID, schedule.DocDate.Value);
      PX.Objects.IN.InventoryItem inventoryItem2 = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select(((PXSelectBase) scheduleDetailsView).Cache.Graph, new object[1]
      {
        (object) scheduleDetail.ParentInventoryID
      }));
      throw new NoFairValuePriceFoundException(inventoryItem1.InventoryCD, inventoryItem2.InventoryCD, scheduleDetail.UOM, currencyInfo.CuryID, schedule.DocDate.Value);
    }
    Decimal num1 = 0M;
    Decimal? nullable1;
    if (fairValueSalesPrice.Discountable)
    {
      if (scheduleDetail.Qty.Value == 0M)
        throw new PXException("The sum of fair value prices is equal to zero and cannot be distributed.");
      PX.Objects.AR.ARTran row = new PX.Objects.AR.ARTran();
      row.CustomerID = schedule.BAccountID;
      row.InventoryID = scheduleDetail.ComponentID;
      row.BranchID = scheduleDetail.BranchID;
      row.SiteID = artran.SiteID;
      DiscountLineFields<DiscountLineFields.skipDisc, DiscountLineFields.curyDiscAmt, DiscountLineFields.discPct, DiscountLineFields.discountID, DiscountLineFields.discountSequenceID, DiscountLineFields.discountsAppliedToLine, DiscountLineFields.manualDisc, DiscountLineFields.manualPrice, DiscountLineFields.lineType, DiscountLineFields.isFree, DiscountLineFields.calculateDiscountsOnImport, DiscountLineFields.automaticDiscountsDisabled, DiscountLineFields.skipLineDiscounts> discountLineFields = new DiscountLineFields<DiscountLineFields.skipDisc, DiscountLineFields.curyDiscAmt, DiscountLineFields.discPct, DiscountLineFields.discountID, DiscountLineFields.discountSequenceID, DiscountLineFields.discountsAppliedToLine, DiscountLineFields.manualDisc, DiscountLineFields.manualPrice, DiscountLineFields.lineType, DiscountLineFields.isFree, DiscountLineFields.calculateDiscountsOnImport, DiscountLineFields.automaticDiscountsDisabled, DiscountLineFields.skipLineDiscounts>(((PXSelectBase) artranView).Cache, (object) row);
      PXCache cache = ((PXSelectBase) artranView).Cache;
      PX.Objects.AR.ARTran line = row;
      DiscountLineFields<DiscountLineFields.skipDisc, DiscountLineFields.curyDiscAmt, DiscountLineFields.discPct, DiscountLineFields.discountID, DiscountLineFields.discountSequenceID, DiscountLineFields.discountsAppliedToLine, DiscountLineFields.manualDisc, DiscountLineFields.manualPrice, DiscountLineFields.lineType, DiscountLineFields.isFree, DiscountLineFields.calculateDiscountsOnImport, DiscountLineFields.automaticDiscountsDisabled, DiscountLineFields.skipLineDiscounts> dLine = discountLineFields;
      Decimal? unitPrice = new Decimal?(price);
      Decimal num2 = price;
      Decimal? qty1 = scheduleDetail.Qty;
      Decimal num3 = qty1.Value;
      Decimal? extPrice = new Decimal?(num2 * num3);
      qty1 = scheduleDetail.Qty;
      Decimal? qty2 = new Decimal?(qty1.Value);
      int? locationId = location?.LocationID;
      int? baccountId = schedule.BAccountID;
      string curyId = fairValueSalesPrice.CuryID;
      DateTime? date = new DateTime?(schedule.DocDate.Value);
      int? branchID = new int?();
      int? componentId = scheduleDetail.ComponentID;
      Decimal lineDiscountOnly = DiscountEngine.GetLineDiscountOnly<PX.Objects.AR.ARTran>(cache, line, (DiscountLineFields) dLine, unitPrice, extPrice, qty2, locationId, baccountId, curyId, date, branchID, componentId);
      nullable1 = scheduleDetail.Qty;
      Decimal num4 = nullable1.Value;
      num1 = lineDiscountOnly / num4;
    }
    scheduleDetail.FairValueCuryID = fairValueSalesPrice.CuryID;
    scheduleDetail.FairValuePrice = new Decimal?(price);
    scheduleDetail.DiscountPercent = new Decimal?(price == 0M ? 0M : num1 / price * 100M);
    DRScheduleDetail drScheduleDetail = scheduleDetail;
    Decimal num5 = price - num1;
    nullable1 = fairValueSalesPrice.Prorated ? scheduleDetail.CoTermRate : new Decimal?((Decimal) 1);
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num5 * nullable1.GetValueOrDefault()) : new Decimal?();
    drScheduleDetail.EffectiveFairValuePrice = nullable2;
  }

  public PXResultset<PX.Objects.AR.ARTran> GetParentDocumentDetails()
  {
    return ((PXSelectBase<PX.Objects.AR.ARTran>) this.ARTransactionWithItems).Select(Array.Empty<object>());
  }

  public Decimal GetQuantityInBaseUOMs(PX.Objects.AR.ARTran tran)
  {
    return INUnitAttribute.ConvertToBase(((PXGraph) this).Caches[typeof (PX.Objects.AR.ARTran)], tran.InventoryID, tran.UOM, tran.Qty.GetValueOrDefault(), INPrecision.QUANTITY);
  }

  public void DeleteAllDetails(int? scheduleID)
  {
    foreach (PXResult<DRScheduleDetail> pxResult in PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) scheduleID
    }))
      ((PXSelectBase<DRScheduleDetail>) this.ScheduleDetail).Delete(PXResult<DRScheduleDetail>.op_Implicit(pxResult));
  }

  public class ComponentINItem : PX.Objects.IN.InventoryItem
  {
    public new abstract class inventoryID : IBqlField, IBqlOperand
    {
    }
  }

  public class ComponentDeferredCode : DRDeferredCode
  {
    public new abstract class deferredCodeID : IBqlField, IBqlOperand
    {
    }
  }
}

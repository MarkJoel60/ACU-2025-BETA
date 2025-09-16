// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.SingleScheduleCreator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.DR.Descriptor;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.DR;

public class SingleScheduleCreator
{
  private DRSchedule _schedule;
  private readonly IDREntityStorage _drEntityStorage;
  private readonly ISubaccountProvider _subaccountProvider;
  private readonly IBusinessAccountProvider _businessAccountProvider;
  private readonly IInventoryItemProvider _inventoryItemProvider;
  private readonly ISingleScheduleViewProvider _singleScheduleViewProvider;
  private readonly ISalesPriceProvider _salesPriceProvider;
  private readonly IDRDataProvider _dataProvider;
  private readonly Func<Decimal, Decimal> _roundingFunction;
  private readonly bool _isDraft;
  private readonly int? _branchID;
  private readonly PX.Objects.CR.Location _location;
  private readonly PX.Objects.CM.CurrencyInfo _currencyInfo;

  public IFinPeriodRepository FinPeriodRepository { get; }

  /// <param name="roundingFunction">
  /// The function that should be used for rounding transaction amounts.
  /// </param>
  /// <param name="isDraft">
  /// Indicates whether the schedule to be created or reevaluated is a draft schedule.
  /// In particular, it affects whether credit line / deferral transactions would be
  /// generated for the schedule.
  /// </param>
  public SingleScheduleCreator(
    IDREntityStorage drEntityStorage,
    ISubaccountProvider subaccountProvider,
    IBusinessAccountProvider businessAccountProvider,
    IInventoryItemProvider inventoryItemProvider,
    IFinPeriodRepository finPeriodRepository,
    Func<Decimal, Decimal> roundingFunction,
    int? branchID,
    bool isDraft,
    PX.Objects.CR.Location location,
    PX.Objects.CM.CurrencyInfo currencyInfo)
  {
    if (drEntityStorage == null)
      throw new ArgumentNullException(nameof (drEntityStorage));
    if (subaccountProvider == null)
      throw new ArgumentNullException(nameof (subaccountProvider));
    if (businessAccountProvider == null)
      throw new ArgumentNullException(nameof (businessAccountProvider));
    if (inventoryItemProvider == null)
      throw new ArgumentNullException(nameof (inventoryItemProvider));
    if (roundingFunction == null)
      throw new ArgumentNullException(nameof (roundingFunction));
    this.FinPeriodRepository = finPeriodRepository;
    this._drEntityStorage = drEntityStorage;
    this._subaccountProvider = subaccountProvider;
    this._businessAccountProvider = businessAccountProvider;
    this._inventoryItemProvider = inventoryItemProvider;
    this._roundingFunction = roundingFunction;
    this._isDraft = isDraft;
    this._branchID = branchID;
    this._location = location;
    this._currencyInfo = currencyInfo;
  }

  public SingleScheduleCreator(
    IDREntityStorage drEntityStorage,
    ISubaccountProvider subaccountProvider,
    IBusinessAccountProvider businessAccountProvider,
    IInventoryItemProvider inventoryItemProvider,
    ISingleScheduleViewProvider singleScheduleViewProvider,
    ISalesPriceProvider salesPriceProvider,
    IDRDataProvider dataProvider,
    IFinPeriodRepository finPeriodRepository,
    Func<Decimal, Decimal> roundingFunction,
    int? branchID,
    bool isDraft,
    PX.Objects.CR.Location location,
    PX.Objects.CM.CurrencyInfo currencyInfo)
    : this(drEntityStorage, subaccountProvider, businessAccountProvider, inventoryItemProvider, finPeriodRepository, roundingFunction, branchID, isDraft, location, currencyInfo)
  {
    this._salesPriceProvider = salesPriceProvider ?? throw new ArgumentNullException(nameof (salesPriceProvider));
    this._singleScheduleViewProvider = singleScheduleViewProvider ?? throw new ArgumentNullException(nameof (singleScheduleViewProvider));
    this._dataProvider = dataProvider ?? throw new ArgumentNullException(nameof (dataProvider));
  }

  private void ValidateMDAConsistency(PX.Objects.IN.InventoryItem inventoryItem, DRDeferredCode deferralCode)
  {
    if (inventoryItem != null && inventoryItem.IsSplitted.GetValueOrDefault())
    {
      bool? deliverableArrangement = deferralCode.MultiDeliverableArrangement;
      bool flag = false;
      if (deliverableArrangement.GetValueOrDefault() == flag & deliverableArrangement.HasValue)
        throw new PXException("Cannot split the line amount into deferral components: for bundle items, the item-level deferral code should be marked as a Multiple Deliverable Arrangement.");
    }
    if (inventoryItem == null && deferralCode.MultiDeliverableArrangement.GetValueOrDefault())
      throw new PXException("Cannot split the line amount into deferral components: an MDA deferral code is specified, but the document line contains no inventory item.");
  }

  public void CreateOriginalSchedule(
    DRProcess.DRScheduleParameters scheduleParameters,
    ARReleaseProcess.Amount lineTotal)
  {
    this._schedule = this._drEntityStorage.CreateCopy((DRSchedule) scheduleParameters);
    this._schedule.IsDraft = new bool?(this._isDraft);
    this._schedule.IsCustom = new bool?(false);
    this._schedule.IsSuspense = new bool?(false);
    this._schedule = this._drEntityStorage.Insert(this._schedule);
    this.CreateScheduleDetails(scheduleParameters, lineTotal);
  }

  private void CreateScheduleDetails(
    DRProcess.DRScheduleParameters scheduleParameters,
    ARReleaseProcess.Amount lineTotal)
  {
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    Decimal num3 = 0M;
    int num4 = 0;
    List<string> stringList = new List<string>();
    foreach (PXResult<PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, DRDeferredCode, INComponent, DRSingleProcess.ComponentINItem, DRSingleProcess.ComponentDeferredCode> parentDocumentDetail in this._singleScheduleViewProvider.GetParentDocumentDetails())
    {
      PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, DRDeferredCode, INComponent, DRSingleProcess.ComponentINItem, DRSingleProcess.ComponentDeferredCode>.op_Implicit(parentDocumentDetail);
      PX.Objects.IN.InventoryItem componentItem = PXResult<PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, DRDeferredCode, INComponent, DRSingleProcess.ComponentINItem, DRSingleProcess.ComponentDeferredCode>.op_Implicit(parentDocumentDetail);
      DRDeferredCode deferralCode = PXResult<PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, DRDeferredCode, INComponent, DRSingleProcess.ComponentINItem, DRSingleProcess.ComponentDeferredCode>.op_Implicit(parentDocumentDetail);
      INComponent component = PXResult<PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, DRDeferredCode, INComponent, DRSingleProcess.ComponentINItem, DRSingleProcess.ComponentDeferredCode>.op_Implicit(parentDocumentDetail);
      DRSingleProcess.ComponentINItem componentInItem = PXResult<PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, DRDeferredCode, INComponent, DRSingleProcess.ComponentINItem, DRSingleProcess.ComponentDeferredCode>.op_Implicit(parentDocumentDetail);
      DRSingleProcess.ComponentDeferredCode componentDeferredCode = PXResult<PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, DRDeferredCode, INComponent, DRSingleProcess.ComponentINItem, DRSingleProcess.ComponentDeferredCode>.op_Implicit(parentDocumentDetail);
      int? nullable1;
      if (num4 == 0)
      {
        nullable1 = arTran.LineNbr;
        num4 = nullable1.Value;
      }
      nullable1 = arTran.LineNbr;
      int num5 = num4;
      if (!(nullable1.GetValueOrDefault() == num5 & nullable1.HasValue))
      {
        num2 = 0M;
        nullable1 = arTran.LineNbr;
        num4 = nullable1.Value;
      }
      bool? nullable2 = deferralCode.MultiDeliverableArrangement;
      bool valueOrDefault = nullable2.GetValueOrDefault();
      AccountSubaccountPair accountSubaccount1 = this.GetDeferralAccountSubaccount(valueOrDefault ? (DRDeferredCode) componentDeferredCode : deferralCode, valueOrDefault ? (PX.Objects.IN.InventoryItem) componentInItem : componentItem, scheduleParameters, arTran.SubID);
      AccountSubaccountPair accountSubaccount2 = this.GetSalesAccountSubaccount(deferralCode, componentItem, component, arTran);
      bool isFlexibleMethod = deferralCode.Method == "F" || deferralCode.Method == "L" || componentDeferredCode?.Method == "F" || componentDeferredCode?.Method == "L";
      try
      {
        if (valueOrDefault && component.AmtOptionASC606 == "R")
        {
          Decimal? nullable3 = arTran.DiscPctDR;
          Decimal num6 = 1.0M - (nullable3 ?? 0.0M) * 0.01M;
          nullable3 = arTran.CuryUnitPriceDR;
          Decimal num7 = nullable3 ?? 0.0M;
          Decimal num8 = num6 * num7;
          nullable3 = arTran.Qty;
          Decimal num9 = nullable3.Value;
          Decimal num10 = num8 * num9 - num2;
          DRScheduleDetail scheduleDetail1;
          if (componentDeferredCode == null || componentDeferredCode.DeferredCodeID == null)
          {
            AccountSubaccountPair accountSubaccount3 = this.GetSalesOrExpenseAccountSubaccount(component, componentItem);
            nullable2 = componentItem.UseParentSubID;
            nullable1 = nullable2.GetValueOrDefault() ? new int?() : arTran.SubID;
            int? subID = nullable1 ?? accountSubaccount3.SubID;
            AccountSubaccountPair accountSubaccountPair = new AccountSubaccountPair(accountSubaccount3.AccountID, subID);
            scheduleDetail1 = this.CreateScheduleDetail(arTran, component, (string) null, accountSubaccountPair, accountSubaccountPair, isFlexibleMethod, true);
          }
          else
            scheduleDetail1 = this.CreateScheduleDetail(arTran, component, componentDeferredCode.DeferredCodeID, accountSubaccount1, accountSubaccount2, isFlexibleMethod, true);
          scheduleDetail1.ComponentID = component.ComponentID;
          DRScheduleDetail scheduleDetail2 = this._drEntityStorage.Insert(scheduleDetail1);
          scheduleDetail2.AllocationWeightResidual = new Decimal?(num10);
          this._drEntityStorage.Update(scheduleDetail2);
          num3 += num10;
        }
        else
        {
          string defCodeID = component?.DeferredCode ?? deferralCode.DeferredCodeID;
          DRScheduleDetail scheduleDetail3 = this.CreateScheduleDetail(arTran, component, defCodeID, accountSubaccount1, accountSubaccount2, isFlexibleMethod);
          DRScheduleDetail drScheduleDetail1 = scheduleDetail3;
          nullable1 = (int?) component?.ComponentID;
          int? nullable4 = nullable1 ?? arTran.InventoryID;
          drScheduleDetail1.ComponentID = nullable4;
          DRScheduleDetail drScheduleDetail2 = scheduleDetail3;
          int? nullable5;
          if (component != null)
          {
            nullable1 = component.ComponentID;
            if (nullable1.HasValue)
            {
              nullable5 = arTran.InventoryID;
              goto label_16;
            }
          }
          nullable1 = new int?();
          nullable5 = nullable1;
label_16:
          drScheduleDetail2.ParentInventoryID = nullable5;
          DRScheduleDetail scheduleDetail4 = this._drEntityStorage.Insert(scheduleDetail3);
          this.SetFairValuePrice(scheduleDetail4, arTran);
          DRScheduleDetail drScheduleDetail3 = this._drEntityStorage.Update(scheduleDetail4);
          Decimal num11 = num2;
          Decimal num12 = drScheduleDetail3.EffectiveFairValuePrice.Value;
          Decimal? nullable6 = drScheduleDetail3.Qty;
          Decimal num13 = nullable6.Value;
          Decimal num14 = num12 * num13;
          num2 = num11 + num14;
          Decimal num15 = num1;
          nullable6 = drScheduleDetail3.EffectiveFairValuePrice;
          Decimal num16 = nullable6.Value * drScheduleDetail3.Qty.Value;
          num1 = num15 + num16;
        }
      }
      catch (NoFairValuePriceFoundException ex)
      {
        stringList.Add(((Exception) ex).Message);
      }
    }
    if (stringList.Any<string>())
      throw new NoFairValuePricesFoundException(string.Join(Environment.NewLine, (IEnumerable<string>) stringList));
    IEnumerable<DRScheduleDetail> list = (IEnumerable<DRScheduleDetail>) GraphHelper.RowCast<DRScheduleDetail>((IEnumerable) this._drEntityStorage.GetScheduleDetails(this._schedule.ScheduleID)).ToList<DRScheduleDetail>();
    if (list.IsSingleElement<DRScheduleDetail>())
    {
      DRScheduleDetail scheduleDetail = list.Single<DRScheduleDetail>();
      scheduleDetail.CuryTotalAmt = lineTotal.Cury;
      scheduleDetail.CuryDefAmt = lineTotal.Cury;
      scheduleDetail.Percentage = new Decimal?(1M);
      this._drEntityStorage.Update(scheduleDetail);
    }
    else if (list.HasAtLeastTwoItems<DRScheduleDetail>())
    {
      Decimal num17 = 0M;
      Decimal num18 = 0M;
      Decimal num19 = 0M;
      DRScheduleDetail scheduleDetail5 = (DRScheduleDetail) null;
      IEnumerable<DRScheduleDetail> source1 = list.Where<DRScheduleDetail>((Func<DRScheduleDetail, bool>) (x => x.IsResidual.GetValueOrDefault()));
      foreach (DRScheduleDetail scheduleDetail6 in source1)
      {
        Decimal? nullable7 = scheduleDetail6.AllocationWeightResidual;
        Decimal num20 = nullable7.Value;
        if ((num20 <= 0M || num3 <= 0M) && source1.Count<DRScheduleDetail>() > 1)
          throw new ScheduleCuryTotalAmtLessOrEqualZeroException("The total residual amount cannot be allocated between the residual components because the calculated allocation weight of the {0} component is less than or equal to zero. Thus, a deferral schedule cannot be created and the revenue will be posted to a suspense account.", new object[1]
          {
            (object) this._inventoryItemProvider.GetInventoryItemByID(scheduleDetail6.ComponentID)?.InventoryCD
          });
        Decimal? nullable8 = lineTotal.Cury;
        Decimal num21 = num1;
        Decimal? nullable9 = nullable8.HasValue ? new Decimal?(nullable8.GetValueOrDefault() - num21) : new Decimal?();
        Decimal num22 = num20;
        Decimal? nullable10;
        if (!nullable9.HasValue)
        {
          nullable8 = new Decimal?();
          nullable10 = nullable8;
        }
        else
          nullable10 = new Decimal?(nullable9.GetValueOrDefault() * num22);
        nullable7 = nullable10;
        Decimal num23 = num3;
        Decimal? nullable11;
        if (!nullable7.HasValue)
        {
          nullable9 = new Decimal?();
          nullable11 = nullable9;
        }
        else
          nullable11 = new Decimal?(nullable7.GetValueOrDefault() / num23);
        Decimal? nullable12 = new Decimal?(this._roundingFunction(nullable11.Value));
        scheduleDetail6.CuryTotalAmt = nullable12;
        scheduleDetail6.CuryDefAmt = scheduleDetail6.DefCode == null ? new Decimal?(0M) : scheduleDetail6.CuryTotalAmt;
        Decimal num24 = num19;
        nullable7 = scheduleDetail6.CuryTotalAmt;
        Decimal num25 = nullable7.Value;
        num19 = num24 + num25;
        DRScheduleDetail drScheduleDetail = this._drEntityStorage.Update(scheduleDetail6);
        Decimal? nullable13;
        if (scheduleDetail5 == null)
        {
          nullable9 = new Decimal?();
          nullable13 = nullable9;
        }
        else
          nullable13 = scheduleDetail5.CuryTotalAmt;
        nullable9 = nullable13;
        Decimal valueOrDefault1 = nullable9.GetValueOrDefault();
        nullable7 = drScheduleDetail.CuryTotalAmt;
        Decimal valueOrDefault2 = nullable7.GetValueOrDefault();
        if (valueOrDefault1 < valueOrDefault2 & nullable7.HasValue)
          scheduleDetail5 = drScheduleDetail;
      }
      if (num19 <= 0M && source1.Any<DRScheduleDetail>())
        throw new ScheduleCuryTotalAmtLessOrEqualZeroException("The total residual amount is less than or equal to zero and cannot be allocated between the residual components. Thus, a deferral schedule cannot be created and the revenue will be posted to a suspense account.");
      IEnumerable<DRScheduleDetail> source2 = list.Where<DRScheduleDetail>((Func<DRScheduleDetail, bool>) (x => !x.IsResidual.GetValueOrDefault()));
      if (num1 == 0M && source2.Any<DRScheduleDetail>())
        throw new PXException("The sum of fair value prices is equal to zero and cannot be distributed.");
      foreach (DRScheduleDetail scheduleDetail7 in source2)
      {
        DRScheduleDetail drScheduleDetail4 = scheduleDetail7;
        Decimal? effectiveFairValuePrice = scheduleDetail7.EffectiveFairValuePrice;
        Decimal? nullable14 = scheduleDetail7.Qty;
        Decimal? nullable15 = effectiveFairValuePrice.HasValue & nullable14.HasValue ? new Decimal?(effectiveFairValuePrice.GetValueOrDefault() * nullable14.GetValueOrDefault()) : new Decimal?();
        Decimal num26 = num1;
        Decimal? nullable16;
        if (!nullable15.HasValue)
        {
          nullable14 = new Decimal?();
          nullable16 = nullable14;
        }
        else
          nullable16 = new Decimal?(nullable15.GetValueOrDefault() / num26);
        drScheduleDetail4.Percentage = nullable16;
        Decimal? cury = lineTotal.Cury;
        Decimal num27 = num19;
        nullable15 = cury.HasValue ? new Decimal?(cury.GetValueOrDefault() - num27) : new Decimal?();
        nullable14 = scheduleDetail7.Percentage;
        Decimal? nullable17 = new Decimal?(this._roundingFunction((nullable15.HasValue & nullable14.HasValue ? new Decimal?(nullable15.GetValueOrDefault() * nullable14.GetValueOrDefault()) : new Decimal?()).Value));
        num18 += nullable17.GetValueOrDefault();
        scheduleDetail7.CuryTotalAmt = nullable17;
        scheduleDetail7.CuryDefAmt = nullable17;
        Decimal num28 = num17;
        nullable14 = scheduleDetail7.Percentage;
        Decimal valueOrDefault3 = nullable14.GetValueOrDefault();
        num17 = num28 + valueOrDefault3;
        DRScheduleDetail drScheduleDetail5 = this._drEntityStorage.Update(scheduleDetail7);
        Decimal? nullable18;
        if (scheduleDetail5 == null)
        {
          nullable15 = new Decimal?();
          nullable18 = nullable15;
        }
        else
          nullable18 = scheduleDetail5.CuryTotalAmt;
        nullable15 = nullable18;
        Decimal valueOrDefault4 = nullable15.GetValueOrDefault();
        nullable14 = drScheduleDetail5.CuryTotalAmt;
        Decimal valueOrDefault5 = nullable14.GetValueOrDefault();
        if (valueOrDefault4 < valueOrDefault5 & nullable14.HasValue)
          scheduleDetail5 = drScheduleDetail5;
      }
      Decimal? nullable19;
      if (!(num17 != 1M))
      {
        Decimal num29 = num18;
        Decimal? cury = lineTotal.Cury;
        Decimal num30 = num19;
        nullable19 = cury.HasValue ? new Decimal?(cury.GetValueOrDefault() - num30) : new Decimal?();
        Decimal valueOrDefault = nullable19.GetValueOrDefault();
        if (num29 == valueOrDefault & nullable19.HasValue)
          goto label_74;
      }
      Decimal? nullable20 = lineTotal.Cury;
      Decimal num31 = num19;
      nullable19 = nullable20.HasValue ? new Decimal?(nullable20.GetValueOrDefault() - num31) : new Decimal?();
      Decimal num32 = num18;
      Decimal? nullable21;
      if (!nullable19.HasValue)
      {
        nullable20 = new Decimal?();
        nullable21 = nullable20;
      }
      else
        nullable21 = new Decimal?(nullable19.GetValueOrDefault() - num32);
      Decimal? nullable22 = nullable21;
      DRScheduleDetail drScheduleDetail6 = scheduleDetail5;
      nullable19 = drScheduleDetail6.CuryTotalAmt;
      nullable20 = nullable22;
      drScheduleDetail6.CuryTotalAmt = nullable19.HasValue & nullable20.HasValue ? new Decimal?(nullable19.GetValueOrDefault() + nullable20.GetValueOrDefault()) : new Decimal?();
      DRScheduleDetail drScheduleDetail7 = scheduleDetail5;
      nullable20 = drScheduleDetail7.CuryDefAmt;
      nullable19 = nullable22;
      drScheduleDetail7.CuryDefAmt = nullable20.HasValue & nullable19.HasValue ? new Decimal?(nullable20.GetValueOrDefault() + nullable19.GetValueOrDefault()) : new Decimal?();
      DRScheduleDetail drScheduleDetail8 = scheduleDetail5;
      nullable19 = drScheduleDetail8.Percentage;
      Decimal num33 = 1M - num17;
      Decimal? nullable23;
      if (!nullable19.HasValue)
      {
        nullable20 = new Decimal?();
        nullable23 = nullable20;
      }
      else
        nullable23 = new Decimal?(nullable19.GetValueOrDefault() + num33);
      drScheduleDetail8.Percentage = nullable23;
      this._drEntityStorage.Update(scheduleDetail5);
    }
label_74:
    if (this._isDraft)
      return;
    foreach (PXResult<DRScheduleDetail, DRDeferredCode, INComponent> pxResult in this._dataProvider.GetScheduleDetailsResultset(this._schedule.ScheduleID))
    {
      DRScheduleDetail scheduleDetail = PXResult<DRScheduleDetail, DRDeferredCode, INComponent>.op_Implicit(pxResult);
      DRDeferredCode deferralCode = PXResult<DRScheduleDetail, DRDeferredCode, INComponent>.op_Implicit(pxResult);
      INComponent inComponent = PXResult<DRScheduleDetail, DRDeferredCode, INComponent>.op_Implicit(pxResult);
      if (!deferralCode.MultiDeliverableArrangement.GetValueOrDefault() || inComponent.AmtOptionASC606 != "R" || deferralCode != null && deferralCode.DeferredCodeID != null)
        this._drEntityStorage.CreateCreditLineTransaction(scheduleDetail, deferralCode, this._branchID);
      IEnumerable<DRScheduleTran> deferralTransactions = this._drEntityStorage.CreateDeferralTransactions(this._schedule, scheduleDetail, deferralCode, this._branchID);
      this._drEntityStorage.NonDraftDeferralTransactionsPrepared(scheduleDetail, deferralCode, deferralTransactions);
    }
  }

  private AccountSubaccountPair GetSalesOrExpenseAccountSubaccount(
    INComponent component,
    PX.Objects.IN.InventoryItem componentItem)
  {
    return !(this._schedule.Module == "AP") ? new AccountSubaccountPair(component.SalesAcctID, component.SalesSubID) : new AccountSubaccountPair(componentItem.COGSAcctID, componentItem.COGSSubID);
  }

  private void SetFairValuePrice(DRScheduleDetail scheduleDetail, PX.Objects.AR.ARTran artran)
  {
    this._salesPriceProvider.SetFairValueSalesPrice(scheduleDetail, artran, this._location, this._currencyInfo);
  }

  /// <param name="attachedToOriginalSchedule">
  /// Flag added to handle <see cref="T:PX.Objects.DR.DRScheduleDetail" />'s status
  /// in the same way as <see cref="T:PX.Objects.DR.DRProcess" /> had done for documents
  /// attached to original schedule.
  /// </param>
  public void ReevaluateSchedule(
    DRSchedule schedule,
    DRProcess.DRScheduleParameters scheduleParameters,
    ARReleaseProcess.Amount lineAmount,
    bool attachedToOriginalSchedule)
  {
    if (schedule.IsOverridden.GetValueOrDefault() && !this._isDraft)
    {
      Decimal? nullable1 = this._drEntityStorage.GetScheduleDetails(schedule.ScheduleID).Sum<DRScheduleDetail>((Func<DRScheduleDetail, Decimal?>) (s => s.TotalAmt));
      Decimal? nullable2 = nullable1;
      Decimal? cury = lineAmount.Cury;
      if (!(nullable2.GetValueOrDefault() == cury.GetValueOrDefault() & nullable2.HasValue == cury.HasValue))
        throw new PXException("The deferral schedule cannot be released because its Component Total amount ({0} {2}) is greater than the Net Transaction Price ({1} {2}) of the document.", new object[3]
        {
          (object) nullable1,
          (object) lineAmount.Base,
          (object) this._currencyInfo.BaseCuryID
        });
    }
    this._dataProvider.DeleteAllDetails(schedule.ScheduleID);
    this._schedule = schedule;
    this._schedule.DocDate = scheduleParameters.DocDate;
    this._schedule.BAccountID = scheduleParameters.BAccountID;
    this._schedule.BAccountLocID = scheduleParameters.BAccountLocID;
    this._schedule.FinPeriodID = scheduleParameters.FinPeriodID;
    this._schedule.TranDesc = scheduleParameters.TranDesc;
    this._schedule.IsCustom = new bool?(false);
    this._schedule.IsDraft = new bool?(this._isDraft);
    this._schedule.IsSuspense = new bool?(false);
    this._schedule.BAccountType = this._schedule.Module == "AP" ? "VE" : "CU";
    this._schedule.TermStartDate = scheduleParameters.TermStartDate;
    this._schedule.TermEndDate = scheduleParameters.TermEndDate;
    this._schedule.ProjectID = scheduleParameters.ProjectID;
    this._schedule.TaskID = scheduleParameters.TaskID;
    this._schedule = this._drEntityStorage.Update(this._schedule);
    this._schedule = this._drEntityStorage.UpdateCuryInfo(this._schedule, scheduleParameters.CuryInfoID);
    if (attachedToOriginalSchedule)
      return;
    this.CreateScheduleDetails(scheduleParameters, lineAmount);
  }

  public static void RecalculateSchedule(DraftScheduleMaint graph)
  {
    try
    {
      graph.CurrentContext = DraftScheduleMaint.Context.Recalculate;
      SingleScheduleCreator.RecalculateScheduleProc(graph);
    }
    finally
    {
      graph.CurrentContext = DraftScheduleMaint.Context.Normal;
    }
  }

  private static void RecalculateScheduleProc(DraftScheduleMaint graph)
  {
    DRSchedule current = ((PXSelectBase<DRSchedule>) graph.Schedule).Current;
    if ((((PXSelectBase) graph.Schedule).Cache.GetStatus((object) ((PXSelectBase<DRSchedule>) graph.Schedule).Current) == null ? 0 : (((PXSelectBase) graph.Schedule).Cache.GetStatus((object) ((PXSelectBase<DRSchedule>) graph.Schedule).Current) != 1 ? 1 : 0)) != 0 || current.IsRecalculated.GetValueOrDefault() || !((PXSelectBase<DRScheduleDetail>) graph.Components).Any<DRScheduleDetail>())
      return;
    PX.Objects.AR.ARInvoice arInvoice = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Required<PX.Objects.AR.ARRegister.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PX.Objects.AR.ARRegister.refNbr>>>>>.Config>.Select((PXGraph) graph, new object[2]
    {
      (object) current.DocType,
      (object) current.RefNbr
    }));
    PXResultset<DRSchedule>.op_Implicit(((PXSelectBase<DRSchedule>) new PXSelect<DRSchedule, Where<DRSchedule.module, Equal<BatchModule.moduleAR>, And<DRSchedule.docType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<DRSchedule.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>>>>>((PXGraph) graph)).Select(new object[2]
    {
      (object) arInvoice.DocType,
      (object) arInvoice.RefNbr
    }));
    List<DRScheduleDetail> drScheduleDetailList = new List<DRScheduleDetail>();
    ARReleaseProcess.Amount netAmount = ASC606Helper.CalculateNetAmount((PXGraph) graph, (PX.Objects.AR.ARRegister) arInvoice);
    int? defScheduleID = new int?();
    DRSchedule drSchedule = (DRSchedule) null;
    Decimal? cury = netAmount.Cury;
    Decimal num = 0M;
    if (!(cury.GetValueOrDefault() == num & cury.HasValue))
    {
      DRSingleProcess instance = PXGraph.CreateInstance<DRSingleProcess>();
      instance.CreateSingleSchedule(arInvoice, netAmount, defScheduleID, true);
      drSchedule = ((PXSelectBase<DRSchedule>) instance.Schedule).Current;
      foreach (DRScheduleDetail drScheduleDetail in ((PXSelectBase) instance.ScheduleDetail).Cache.Inserted)
        drScheduleDetailList.Add(drScheduleDetail);
    }
    foreach (PXResult<DRScheduleDetail, DRSchedule> pxResult in PXSelectBase<DRScheduleDetail, PXSelectJoin<DRScheduleDetail, LeftJoin<DRSchedule, On<DRScheduleDetail.scheduleID, Equal<DRSchedule.scheduleID>>>, Where<DRScheduleDetail.scheduleID, Equal<Current<DRSchedule.scheduleID>>>>.Config>.Select((PXGraph) graph, Array.Empty<object>()))
    {
      DRScheduleDetail drScheduleDetail = PXResult<DRScheduleDetail, DRSchedule>.op_Implicit(pxResult);
      ((PXSelectBase<DRScheduleDetail>) graph.Components).Delete(drScheduleDetail);
    }
    current.DocDate = drSchedule.DocDate;
    current.BAccountID = drSchedule.BAccountID;
    current.BAccountLocID = drSchedule.BAccountLocID;
    current.FinPeriodID = drSchedule.FinPeriodID;
    current.TranDesc = drSchedule.TranDesc;
    current.IsCustom = new bool?(false);
    current.BAccountType = current.Module == "AP" ? "VE" : "CU";
    current.TermStartDate = drSchedule.TermStartDate;
    current.TermEndDate = drSchedule.TermEndDate;
    current.ProjectID = drSchedule.ProjectID;
    current.TaskID = drSchedule.TaskID;
    current.CuryInfoID = drSchedule.CuryInfoID;
    current.IsRecalculated = new bool?(true);
    ((PXSelectBase<DRSchedule>) graph.Schedule).Update(current);
    foreach (DRScheduleDetail drScheduleDetail in drScheduleDetailList)
      ((PXSelectBase<DRScheduleDetail>) graph.Components).Insert(drScheduleDetail);
  }

  /// <summary>
  /// Reevaluates the provided schedule details' amounts, prorating
  /// them in order for their sum to match the given line amount.
  /// </summary>
  private void ReevaluateComponentAmounts(
    IEnumerable<DRScheduleDetail> scheduleDetails,
    Decimal? lineAmount)
  {
    Decimal? detailsTotal = scheduleDetails.Sum<DRScheduleDetail>((Func<DRScheduleDetail, Decimal?>) (scheduleDetail => scheduleDetail.TotalAmt));
    Decimal? nullable1 = lineAmount;
    Decimal? nullable2 = detailsTotal;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    if (scheduleDetails.IsSingleElement<DRScheduleDetail>())
    {
      scheduleDetails.Single<DRScheduleDetail>().CuryTotalAmt = lineAmount;
      scheduleDetails.Single<DRScheduleDetail>().CuryDefAmt = lineAmount;
    }
    else
    {
      if (!scheduleDetails.HasAtLeastTwoItems<DRScheduleDetail>())
        return;
      Decimal correctedTotal = 0M;
      EnumerableExtensions.ForEach<DRScheduleDetail>(scheduleDetails.SkipLast<DRScheduleDetail>(1), (Action<DRScheduleDetail>) (scheduleDetail =>
      {
        Decimal num = this._roundingFunction(scheduleDetail.TotalAmt.Value * lineAmount.Value / detailsTotal.Value);
        correctedTotal += num;
        scheduleDetail.CuryTotalAmt = new Decimal?(num);
        scheduleDetail.CuryDefAmt = new Decimal?(num);
      }));
      DRScheduleDetail drScheduleDetail1 = scheduleDetails.Last<DRScheduleDetail>();
      Decimal? nullable3 = lineAmount;
      Decimal num1 = correctedTotal;
      Decimal? nullable4;
      if (!nullable3.HasValue)
      {
        nullable1 = new Decimal?();
        nullable4 = nullable1;
      }
      else
        nullable4 = new Decimal?(nullable3.GetValueOrDefault() - num1);
      drScheduleDetail1.CuryTotalAmt = nullable4;
      DRScheduleDetail drScheduleDetail2 = scheduleDetails.Last<DRScheduleDetail>();
      Decimal? nullable5 = lineAmount;
      Decimal num2 = correctedTotal;
      Decimal? nullable6;
      if (!nullable5.HasValue)
      {
        nullable1 = new Decimal?();
        nullable6 = nullable1;
      }
      else
        nullable6 = new Decimal?(nullable5.GetValueOrDefault() - num2);
      drScheduleDetail2.CuryDefAmt = nullable6;
    }
  }

  /// <summary>
  /// For a given schedule detail, reevaluates deferral transactions if they exist.
  /// </summary>
  private void ReevaluateTransactionAmounts(
    DRScheduleDetail scheduleComponent,
    DRDeferredCode componentDeferralCode,
    IEnumerable<DRScheduleTran> componentTransactions)
  {
    Decimal? totalTransactionAmount = componentTransactions.Sum<DRScheduleTran>((Func<DRScheduleTran, Decimal?>) (transaction => transaction.Amount));
    Decimal? nullable1 = totalTransactionAmount;
    Decimal? totalAmt = scheduleComponent.TotalAmt;
    if (nullable1.GetValueOrDefault() == totalAmt.GetValueOrDefault() & nullable1.HasValue == totalAmt.HasValue)
      return;
    if (componentTransactions.IsSingleElement<DRScheduleTran>())
    {
      this.UpdateTransactionAmount(scheduleComponent, componentDeferralCode, componentTransactions.Single<DRScheduleTran>(), scheduleComponent.TotalAmt);
    }
    else
    {
      if (!componentTransactions.HasAtLeastTwoItems<DRScheduleTran>())
        return;
      Decimal correctedTotal = 0M;
      EnumerableExtensions.ForEach<DRScheduleTran>(componentTransactions.SkipLast<DRScheduleTran>(1), (Action<DRScheduleTran>) (transaction =>
      {
        Decimal? nullable2 = transaction.Amount;
        Decimal num1 = nullable2.Value;
        nullable2 = scheduleComponent.TotalAmt;
        Decimal num2 = nullable2.Value;
        Decimal num3 = this._roundingFunction(num1 * num2 / totalTransactionAmount.Value);
        correctedTotal += num3;
        this.UpdateTransactionAmount(scheduleComponent, componentDeferralCode, transaction, new Decimal?(num3));
      }));
      DRScheduleDetail scheduleDetail = scheduleComponent;
      DRDeferredCode detailDeferralCode = componentDeferralCode;
      DRScheduleTran transaction1 = componentTransactions.Last<DRScheduleTran>();
      totalAmt = scheduleComponent.TotalAmt;
      Decimal num = correctedTotal;
      Decimal? newAmount = totalAmt.HasValue ? new Decimal?(totalAmt.GetValueOrDefault() - num) : new Decimal?();
      this.UpdateTransactionAmount(scheduleDetail, detailDeferralCode, transaction1, newAmount);
    }
  }

  /// <summary>
  /// Updates the transaction amount and performs the callback to
  /// <see cref="T:PX.Objects.DR.Descriptor.IDREntityStorage" />, notifying about the update.
  /// </summary>
  private void UpdateTransactionAmount(
    DRScheduleDetail scheduleDetail,
    DRDeferredCode detailDeferralCode,
    DRScheduleTran transaction,
    Decimal? newAmount)
  {
    DRScheduleTran copy = this._drEntityStorage.CreateCopy(transaction);
    transaction.Amount = newAmount;
    this._drEntityStorage.ScheduleTransactionModified(scheduleDetail, detailDeferralCode, copy, transaction);
  }

  private DRScheduleDetail CreateScheduleDetail(
    PX.Objects.AR.ARTran artran,
    INComponent component,
    string defCodeID,
    AccountSubaccountPair deferralAccountSubaccount,
    AccountSubaccountPair salesAccountSubaccount,
    bool isFlexibleMethod,
    bool isResidual = false)
  {
    FinPeriod valueOrRaiseError = this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(artran.BranchID), artran.TranPeriodID).GetValueOrRaiseError();
    DRScheduleDetail scheduleDetail = new DRScheduleDetail();
    scheduleDetail.ScheduleID = this._schedule.ScheduleID;
    scheduleDetail.BranchID = artran.BranchID;
    scheduleDetail.Module = this._schedule.Module;
    scheduleDetail.DocType = this._schedule.DocType;
    scheduleDetail.RefNbr = this._schedule.RefNbr;
    scheduleDetail.LineNbr = artran.LineNbr;
    scheduleDetail.FinPeriodID = valueOrRaiseError.FinPeriodID;
    scheduleDetail.TranPeriodID = valueOrRaiseError.MasterFinPeriodID;
    scheduleDetail.BAccountID = this._schedule.BAccountID;
    scheduleDetail.BAccountType = this._schedule.Module == "AP" ? "VE" : "CU";
    scheduleDetail.CreditLineNbr = new int?(0);
    scheduleDetail.DocDate = this._schedule.DocDate;
    scheduleDetail.AccountID = salesAccountSubaccount.AccountID;
    scheduleDetail.SubID = salesAccountSubaccount.SubID;
    scheduleDetail.DefAcctID = deferralAccountSubaccount.AccountID;
    scheduleDetail.DefSubID = deferralAccountSubaccount.SubID;
    scheduleDetail.IsCustom = new bool?(false);
    scheduleDetail.IsResidual = new bool?(isResidual);
    scheduleDetail.DefCode = defCodeID;
    if (string.IsNullOrEmpty(defCodeID))
    {
      scheduleDetail.IsOpen = new bool?(false);
      scheduleDetail.Status = this._isDraft ? "D" : "C";
    }
    else
    {
      string uom = artran.UOM;
      Decimal? nullable1 = artran.Qty;
      Decimal num = 1M;
      DateTime? drTermStartDate = artran.DRTermStartDate;
      DateTime? nullable2 = artran.DRTermEndDate;
      if (component != null && component.ComponentID.HasValue)
      {
        uom = component.UOM;
        if (artran.UOM == component.UOM)
        {
          Decimal? qty1 = artran.Qty;
          Decimal? qty2 = component.Qty;
          nullable1 = qty1.HasValue & qty2.HasValue ? new Decimal?(qty1.GetValueOrDefault() * qty2.GetValueOrDefault()) : new Decimal?();
        }
        else
        {
          Decimal quantityInBaseUoMs = this._salesPriceProvider.GetQuantityInBaseUOMs(artran);
          Decimal? qty = component.Qty;
          nullable1 = qty.HasValue ? new Decimal?(quantityInBaseUoMs * qty.GetValueOrDefault()) : new Decimal?();
        }
        if (isFlexibleMethod && !component.OverrideDefaultTerm.GetValueOrDefault())
          nullable2 = new DateTime?(new DRTerms.Term(component.DefaultTerm, component.DefaultTermUOM).Delay(drTermStartDate).Value);
      }
      if (isFlexibleMethod)
        num = ((Decimal) (nullable2.Value - drTermStartDate.Value).Days + 1.0M) / 365.0M;
      scheduleDetail.UOM = uom;
      scheduleDetail.Qty = nullable1;
      scheduleDetail.TermStartDate = drTermStartDate;
      scheduleDetail.TermEndDate = nullable2;
      scheduleDetail.CoTermRate = new Decimal?(num);
      scheduleDetail.IsOpen = new bool?(true);
      scheduleDetail.Status = this._isDraft ? "D" : "O";
    }
    return scheduleDetail;
  }

  private AccountSubaccountPair GetDeferralAccountSubaccount(
    DRDeferredCode deferralCode,
    PX.Objects.IN.InventoryItem item,
    DRProcess.DRScheduleParameters scheduleParameters,
    int? origSubID)
  {
    int? accountID = deferralCode.AccountID;
    string subaccountCD = (string) null;
    int? subID = new int?();
    if (deferralCode.AccountSource == "I")
      accountID = item != null ? item.DeferralAcctID : subID;
    if (deferralCode.CopySubFromSourceTran.GetValueOrDefault())
      subID = origSubID;
    else if (scheduleParameters.Module == "AP")
    {
      int? deferralSubId = (int?) item?.DeferralSubID;
      int? vexpenseSubId = (int?) this._businessAccountProvider.GetLocation(scheduleParameters.BAccountID, scheduleParameters.BAccountLocID)?.VExpenseSubID;
      int? expenseSubId = (int?) this._businessAccountProvider.GetEmployee(scheduleParameters.EmployeeID)?.ExpenseSubID;
      subaccountCD = this._subaccountProvider.MakeSubaccount<DRScheduleDetail.subID>(deferralCode.DeferralSubMaskAP, new object[4]
      {
        (object) vexpenseSubId,
        (object) deferralSubId,
        (object) expenseSubId,
        (object) deferralCode.SubID
      }, new System.Type[4]
      {
        typeof (PX.Objects.CR.Location.vExpenseSubID),
        typeof (PX.Objects.IN.InventoryItem.deferralSubID),
        typeof (EPEmployee.expenseSubID),
        typeof (DRDeferredCode.subID)
      });
    }
    else if (scheduleParameters.Module == "AR")
    {
      int? deferralSubId = (int?) item?.DeferralSubID;
      int? csalesSubId = (int?) this._businessAccountProvider.GetLocation(scheduleParameters.BAccountID, scheduleParameters.BAccountLocID)?.CSalesSubID;
      int? salesSubId1 = (int?) this._businessAccountProvider.GetEmployee(scheduleParameters.EmployeeID)?.SalesSubID;
      int? salesSubId2 = (int?) this._businessAccountProvider.GetSalesPerson(scheduleParameters.SalesPersonID)?.SalesSubID;
      subaccountCD = this._subaccountProvider.MakeSubaccount<DRScheduleDetail.subID>(deferralCode.DeferralSubMaskAR, new object[5]
      {
        (object) csalesSubId,
        (object) deferralSubId,
        (object) salesSubId1,
        (object) deferralCode.SubID,
        (object) salesSubId2
      }, new System.Type[5]
      {
        typeof (PX.Objects.CR.Location.cSalesSubID),
        typeof (PX.Objects.IN.InventoryItem.deferralSubID),
        typeof (EPEmployee.salesSubID),
        typeof (DRDeferredCode.subID),
        typeof (PX.Objects.AR.SalesPerson.salesSubID)
      });
    }
    if (subaccountCD != null)
      subID = this._subaccountProvider.GetSubaccountID(subaccountCD);
    return new AccountSubaccountPair(accountID, subID);
  }

  private AccountSubaccountPair GetSalesAccountSubaccount(
    DRDeferredCode deferralCode,
    PX.Objects.IN.InventoryItem item,
    INComponent component,
    PX.Objects.AR.ARTran transaction)
  {
    int? accountID = transaction.AccountID;
    int? subID = transaction.SubID;
    if (deferralCode.MultiDeliverableArrangement.GetValueOrDefault())
      accountID = component.SalesAcctID;
    if (deferralCode.MultiDeliverableArrangement.GetValueOrDefault() && item.UseParentSubID.GetValueOrDefault())
      subID = component.SalesSubID;
    return new AccountSubaccountPair(accountID, subID);
  }
}

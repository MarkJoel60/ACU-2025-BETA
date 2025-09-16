// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.ScheduleCreator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.DR.Descriptor;
using PX.Objects.EP;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.DR;

/// <summary>
/// Entity responsible for creating and re-evaluating deferral schedules.
/// </summary>
public class ScheduleCreator
{
  private DRSchedule _schedule;
  protected readonly IFinPeriodRepository FinPeriodRepository;
  private readonly IDREntityStorage _drEntityStorage;
  private readonly ISubaccountProvider _subaccountProvider;
  private readonly IBusinessAccountProvider _businessAccountProvider;
  private readonly IInventoryItemProvider _inventoryItemProvider;
  private readonly Func<Decimal, Decimal> _roundingFunction;
  private readonly bool _isDraft;
  private readonly int? _branchID;

  /// <param name="roundingFunction">
  /// The function that should be used for rounding transaction amounts.
  /// </param>
  /// <param name="isDraft">
  /// Indicates whether the schedule to be created or reevaluated is a draft schedule.
  /// In particular, it affects whether credit line / deferral transactions would be
  /// generated for the schedule.
  /// </param>
  public ScheduleCreator(
    IDREntityStorage drEntityStorage,
    ISubaccountProvider subaccountProvider,
    IBusinessAccountProvider businessAccountProvider,
    IInventoryItemProvider inventoryItemProvider,
    IFinPeriodRepository finPeriodRepository,
    Func<Decimal, Decimal> roundingFunction,
    int? branchID,
    bool isDraft)
  {
    if (drEntityStorage == null)
      throw new ArgumentNullException(nameof (drEntityStorage));
    if (subaccountProvider == null)
      throw new ArgumentNullException(nameof (subaccountProvider));
    if (businessAccountProvider == null)
      throw new ArgumentNullException(nameof (businessAccountProvider));
    if (inventoryItemProvider == null)
      throw new ArgumentNullException(nameof (inventoryItemProvider));
    if (finPeriodRepository == null)
      throw new ArgumentNullException(nameof (finPeriodRepository));
    if (roundingFunction == null)
      throw new ArgumentNullException(nameof (roundingFunction));
    this._drEntityStorage = drEntityStorage;
    this._subaccountProvider = subaccountProvider;
    this._businessAccountProvider = businessAccountProvider;
    this._inventoryItemProvider = inventoryItemProvider;
    this.FinPeriodRepository = finPeriodRepository;
    this._roundingFunction = roundingFunction;
    this._isDraft = isDraft;
    this._branchID = branchID;
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

  /// <param name="deferralCode">The item-level deferral code for <paramref name="inventoryItem" />.</param>
  /// <param name="inventoryItem">The inventory item from the document line.</param>
  /// <param name="transactionAmount">Total transaction amount (with ALL discounts applied).</param>
  /// <param name="fairUnitPrice">The item price from the price list.</param>
  /// <param name="compoundDiscountRate"> Compound discount rate of all discounts
  /// (including line, group and document discounts) that are applicable to deferred components.</param>
  public void CreateOriginalSchedule(
    DRProcess.DRScheduleParameters scheduleParameters,
    DRDeferredCode deferralCode,
    PX.Objects.IN.InventoryItem inventoryItem,
    AccountSubaccountPair salesOrExpenseAccountSubaccount,
    Decimal? transactionAmount,
    Decimal? fairUnitPrice,
    Decimal? compoundDiscountRate,
    Decimal? quantityInBaseUnit)
  {
    this.ValidateMDAConsistency(inventoryItem, deferralCode);
    this._schedule = this._drEntityStorage.CreateCopy((DRSchedule) scheduleParameters);
    this._schedule.IsDraft = new bool?(this._isDraft);
    this._schedule.IsCustom = new bool?(false);
    this._schedule = this._drEntityStorage.Insert(this._schedule);
    this.CreateDetails(scheduleParameters, deferralCode, inventoryItem, salesOrExpenseAccountSubaccount, transactionAmount, fairUnitPrice, compoundDiscountRate, quantityInBaseUnit);
  }

  /// <param name="attachedToOriginalSchedule">
  /// Flag added to handle <see cref="T:PX.Objects.DR.DRScheduleDetail" />'s status
  /// in the same way as <see cref="T:PX.Objects.DR.DRProcess" /> had done for documents
  /// attached to original schedule.
  /// </param>
  public void ReevaluateSchedule(
    DRSchedule schedule,
    DRProcess.DRScheduleParameters scheduleParameters,
    DRDeferredCode deferralCode,
    Decimal? lineAmount,
    bool attachedToOriginalSchedule)
  {
    this._schedule = schedule;
    this._schedule.DocDate = scheduleParameters.DocDate;
    this._schedule.BAccountID = scheduleParameters.BAccountID;
    this._schedule.BAccountLocID = scheduleParameters.BAccountLocID;
    this._schedule.FinPeriodID = scheduleParameters.FinPeriodID;
    this._schedule.TranDesc = scheduleParameters.TranDesc;
    this._schedule.IsCustom = new bool?(false);
    this._schedule.IsDraft = new bool?(this._isDraft);
    this._schedule.BAccountType = this._schedule.Module == "AP" ? "VE" : "CU";
    this._schedule.TermStartDate = scheduleParameters.TermStartDate;
    this._schedule.TermEndDate = scheduleParameters.TermEndDate;
    this._schedule.ProjectID = scheduleParameters.ProjectID;
    this._schedule.TaskID = scheduleParameters.TaskID;
    this._schedule.BaseCuryID = scheduleParameters.BaseCuryID;
    this._schedule = this._drEntityStorage.Update(this._schedule);
    IList<DRScheduleDetail> scheduleDetails = this._drEntityStorage.GetScheduleDetails(this._schedule.ScheduleID);
    this.ReevaluateComponentAmounts((IEnumerable<DRScheduleDetail>) scheduleDetails, lineAmount);
    foreach (DRScheduleDetail drScheduleDetail1 in (IEnumerable<DRScheduleDetail>) scheduleDetails)
    {
      drScheduleDetail1.DocDate = this._schedule.DocDate;
      drScheduleDetail1.BAccountID = this._schedule.BAccountID;
      FinPeriod valueOrRaiseError = this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(drScheduleDetail1.BranchID), this._schedule.FinPeriodID).GetValueOrRaiseError();
      drScheduleDetail1.FinPeriodID = valueOrRaiseError.FinPeriodID;
      drScheduleDetail1.TranPeriodID = valueOrRaiseError.MasterFinPeriodID;
      bool? nullable;
      if (!attachedToOriginalSchedule)
      {
        DRScheduleDetail drScheduleDetail2 = drScheduleDetail1;
        string str;
        if (!this._isDraft)
        {
          nullable = drScheduleDetail1.IsResidual;
          str = !nullable.HasValue ? "C" : "O";
        }
        else
          str = "D";
        drScheduleDetail2.Status = str;
      }
      else
      {
        DRScheduleDetail drScheduleDetail3 = drScheduleDetail1;
        string str;
        if (!this._isDraft)
        {
          nullable = drScheduleDetail1.IsOpen;
          str = nullable.GetValueOrDefault() ? "O" : "C";
        }
        else
          str = "D";
        drScheduleDetail3.Status = str;
      }
      this._drEntityStorage.Update(drScheduleDetail1);
      nullable = drScheduleDetail1.IsResidual;
      if (!nullable.GetValueOrDefault())
      {
        DRDeferredCode deferralCode1 = this._drEntityStorage.GetDeferralCode(drScheduleDetail1.DefCode);
        IEnumerable<DRScheduleTran> deferralTransactions = (IEnumerable<DRScheduleTran>) this._drEntityStorage.GetDeferralTransactions(drScheduleDetail1.ScheduleID, drScheduleDetail1.ComponentID, drScheduleDetail1.DetailLineNbr);
        if (deferralTransactions.Any<DRScheduleTran>())
          this.ReevaluateTransactionAmounts(drScheduleDetail1, deferralCode1, deferralTransactions);
        if (!this._isDraft)
        {
          if (!deferralTransactions.Any<DRScheduleTran>())
            deferralTransactions = this._drEntityStorage.CreateDeferralTransactions(this._schedule, drScheduleDetail1, deferralCode1, this._branchID);
          this._drEntityStorage.CreateCreditLineTransaction(drScheduleDetail1, deferralCode, this._branchID);
          this._drEntityStorage.NonDraftDeferralTransactionsPrepared(drScheduleDetail1, deferralCode1, deferralTransactions);
        }
      }
    }
  }

  /// <summary>
  /// Reevaluates the provided schedule details' amounts, prorating
  /// them in order for their sum to match the given line amount.
  /// </summary>
  private void ReevaluateComponentAmounts(
    IEnumerable<DRScheduleDetail> scheduleDetails,
    Decimal? lineAmount)
  {
    Decimal? detailsTotal = scheduleDetails.Sum<DRScheduleDetail>((Func<DRScheduleDetail, Decimal?>) (scheduleDetail => scheduleDetail.CuryTotalAmt));
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
        Decimal num = this._roundingFunction(scheduleDetail.CuryTotalAmt.Value * lineAmount.Value / detailsTotal.Value);
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
    Decimal? curyTotalAmt = scheduleComponent.CuryTotalAmt;
    if (nullable1.GetValueOrDefault() == curyTotalAmt.GetValueOrDefault() & nullable1.HasValue == curyTotalAmt.HasValue)
      return;
    if (componentTransactions.IsSingleElement<DRScheduleTran>())
    {
      this.UpdateTransactionAmount(scheduleComponent, componentDeferralCode, componentTransactions.Single<DRScheduleTran>(), scheduleComponent.CuryTotalAmt);
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
        nullable2 = scheduleComponent.CuryTotalAmt;
        Decimal num2 = nullable2.Value;
        Decimal num3 = this._roundingFunction(num1 * num2 / totalTransactionAmount.Value);
        correctedTotal += num3;
        this.UpdateTransactionAmount(scheduleComponent, componentDeferralCode, transaction, new Decimal?(num3));
      }));
      DRScheduleDetail scheduleDetail = scheduleComponent;
      DRDeferredCode detailDeferralCode = componentDeferralCode;
      DRScheduleTran transaction1 = componentTransactions.Last<DRScheduleTran>();
      curyTotalAmt = scheduleComponent.CuryTotalAmt;
      Decimal num = correctedTotal;
      Decimal? newAmount = curyTotalAmt.HasValue ? new Decimal?(curyTotalAmt.GetValueOrDefault() - num) : new Decimal?();
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

  private void CreateDetails(
    DRProcess.DRScheduleParameters scheduleParameters,
    DRDeferredCode deferralCode,
    PX.Objects.IN.InventoryItem inventoryItem,
    AccountSubaccountPair salesOrExpenseAccountSubaccount,
    Decimal? transactionAmount,
    Decimal? fairUnitPrice,
    Decimal? compoundDiscountRate,
    Decimal? quantityInBaseUnit)
  {
    if (deferralCode.MultiDeliverableArrangement.GetValueOrDefault() && inventoryItem != null && inventoryItem.IsSplitted.GetValueOrDefault())
    {
      this.CreateDetailsForSplitted(scheduleParameters, inventoryItem, salesOrExpenseAccountSubaccount.SubID, transactionAmount, fairUnitPrice, compoundDiscountRate ?? 1.0M, quantityInBaseUnit ?? 0.0M);
    }
    else
    {
      AccountSubaccountPair accountSubaccount = this.GetDeferralAccountSubaccount(deferralCode, inventoryItem, scheduleParameters);
      DRScheduleDetail scheduleDetail = this.InsertScheduleDetail(inventoryItem == null ? new int?(0) : inventoryItem.InventoryID, deferralCode, transactionAmount.Value, accountSubaccount, salesOrExpenseAccountSubaccount);
      if (this._isDraft)
        return;
      IEnumerable<DRScheduleTran> deferralTransactions = this._drEntityStorage.CreateDeferralTransactions(this._schedule, scheduleDetail, deferralCode, this._branchID);
      this._drEntityStorage.NonDraftDeferralTransactionsPrepared(scheduleDetail, deferralCode, deferralTransactions);
    }
  }

  private void CreateDetailsForSplitted(
    DRProcess.DRScheduleParameters scheduleParameters,
    PX.Objects.IN.InventoryItem inventoryItem,
    int? subaccountID,
    Decimal? transactionAmount,
    Decimal? fairUnitPrice,
    Decimal compoundDiscountRate,
    Decimal qtyInBaseUnit)
  {
    int? overridenSubID1 = inventoryItem.UseParentSubID.GetValueOrDefault() ? new int?() : subaccountID;
    IEnumerable<InventoryItemComponentInfo> inventoryItemComponents1 = this._inventoryItemProvider.GetInventoryItemComponents(inventoryItem.InventoryID, "F");
    IEnumerable<InventoryItemComponentInfo> inventoryItemComponents2 = this._inventoryItemProvider.GetInventoryItemComponents(inventoryItem.InventoryID, "P");
    IEnumerable<InventoryItemComponentInfo> inventoryItemComponents3 = this._inventoryItemProvider.GetInventoryItemComponents(inventoryItem.InventoryID, "R");
    bool flag = !inventoryItemComponents3.Any<InventoryItemComponentInfo>() || inventoryItemComponents3.IsSingleElement<InventoryItemComponentInfo>() ? inventoryItemComponents3.Any<InventoryItemComponentInfo>() : throw new PXException("Cannot split the line amount into deferral components: the item contains more than one residual component.");
    Decimal num1 = 0M;
    foreach (InventoryItemComponentInfo itemComponentInfo in inventoryItemComponents1)
    {
      Decimal num2 = this._roundingFunction(itemComponentInfo.Component.FixedAmt.GetValueOrDefault() * qtyInBaseUnit * compoundDiscountRate);
      num1 += num2;
      this.AddComponentScheduleDetail(itemComponentInfo.Component, itemComponentInfo.DeferralCode, itemComponentInfo.Item, (Decimal) Math.Sign(transactionAmount.Value) * num2, scheduleParameters, overridenSubID1);
    }
    Decimal fixedPerUnit = inventoryItemComponents1.Sum<InventoryItemComponentInfo>((Func<InventoryItemComponentInfo, Decimal>) (componentInfo => componentInfo.Component.FixedAmt.GetValueOrDefault()));
    Decimal num3 = transactionAmount.Value - num1;
    Decimal? nullable = transactionAmount;
    Decimal num4 = 0M;
    if (!(nullable.GetValueOrDefault() >= num4 & nullable.HasValue) || !(num3 < 0M))
    {
      nullable = transactionAmount;
      Decimal num5 = 0M;
      if (!(nullable.GetValueOrDefault() < num5 & nullable.HasValue) || !(num3 > 0M))
      {
        if (!inventoryItemComponents2.Any<InventoryItemComponentInfo>() && num3 != 0M && !flag)
          throw new PXException("Cannot split the line amount into deferral components: the sum of fixed component amounts is not equal to the line amount, and there is no residual component.");
        if (inventoryItemComponents2.Any<InventoryItemComponentInfo>())
        {
          int num6;
          if (flag && fairUnitPrice.HasValue)
          {
            nullable = fairUnitPrice;
            num5 = 0.0M;
            num6 = !(nullable.GetValueOrDefault() == num5 & nullable.HasValue) ? 1 : 0;
          }
          else
            num6 = 0;
          IEnumerable<Tuple<InventoryItemComponentInfo, Decimal>> source = (num6 == 0 ? new PercentageDistribution(this._inventoryItemProvider, inventoryItemComponents2, this._roundingFunction) : (PercentageDistribution) new PercentageWithResidualDistribution(this._inventoryItemProvider, inventoryItemComponents2, fairUnitPrice.Value, fixedPerUnit, compoundDiscountRate, qtyInBaseUnit, this._roundingFunction)).Distribute(transactionAmount.Value, num3);
          foreach (Tuple<InventoryItemComponentInfo, Decimal> tuple in source)
          {
            InventoryItemComponentInfo itemComponentInfo = tuple.Item1;
            INComponent component = itemComponentInfo.Component;
            itemComponentInfo = tuple.Item1;
            DRDeferredCode deferralCode = itemComponentInfo.DeferralCode;
            itemComponentInfo = tuple.Item1;
            PX.Objects.IN.InventoryItem inventoryItem1 = itemComponentInfo.Item;
            Decimal amount = tuple.Item2;
            DRProcess.DRScheduleParameters tranInfo = scheduleParameters;
            int? overridenSubID2 = overridenSubID1;
            this.AddComponentScheduleDetail(component, deferralCode, inventoryItem1, amount, tranInfo, overridenSubID2);
          }
          num3 -= source.Sum<Tuple<InventoryItemComponentInfo, Decimal>>((Func<Tuple<InventoryItemComponentInfo, Decimal>, Decimal>) (componentAmount => componentAmount.Item2));
        }
        if (!flag || !(num3 > 0M))
          return;
        INComponent component1 = inventoryItemComponents3.Single<InventoryItemComponentInfo>().Component;
        PX.Objects.IN.InventoryItem componentItem = inventoryItemComponents3.Single<InventoryItemComponentInfo>().Item;
        AccountSubaccountPair accountSubaccount = this.GetSalesOrExpenseAccountSubaccount(component1, componentItem);
        this.InsertResidualScheduleDetail(component1.ComponentID, num3, accountSubaccount.AccountID, overridenSubID1 ?? accountSubaccount.SubID);
        return;
      }
    }
    throw new PXException("The sum of fixed component amounts is greater than the line amount. Please correct this and try again.");
  }

  private void AddComponentScheduleDetail(
    INComponent inventoryItemComponent,
    DRDeferredCode componentDeferralCode,
    PX.Objects.IN.InventoryItem inventoryItem,
    Decimal amount,
    DRProcess.DRScheduleParameters tranInfo,
    int? overridenSubID)
  {
    if (amount == 0M)
      return;
    DRScheduleDetail scheduleDetail = this.InsertScheduleDetail(inventoryItem.InventoryID, componentDeferralCode, amount, this.GetDeferralAccountSubaccount(componentDeferralCode, inventoryItem, tranInfo), this.GetSalesOrExpenseAccountSubaccount(inventoryItemComponent, inventoryItem), overridenSubID);
    if (this._isDraft)
      return;
    IEnumerable<DRScheduleTran> deferralTransactions = this._drEntityStorage.CreateDeferralTransactions(this._schedule, scheduleDetail, componentDeferralCode, this._branchID);
    this._drEntityStorage.NonDraftDeferralTransactionsPrepared(scheduleDetail, componentDeferralCode, deferralTransactions);
  }

  private AccountSubaccountPair GetSalesOrExpenseAccountSubaccount(
    INComponent component,
    PX.Objects.IN.InventoryItem componentItem)
  {
    return !(this._schedule.Module == "AP") ? new AccountSubaccountPair(component.SalesAcctID, component.SalesSubID) : new AccountSubaccountPair(componentItem.COGSAcctID, componentItem.COGSSubID);
  }

  private DRScheduleDetail InsertScheduleDetail(
    int? componentID,
    DRDeferredCode defCode,
    Decimal amount,
    AccountSubaccountPair deferralAccountSubaccount,
    AccountSubaccountPair salesOrExpenseAccountSubaccount,
    int? overridenSubID = null)
  {
    FinPeriod valueOrRaiseError = this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(this._branchID), this._schedule.FinPeriodID).GetValueOrRaiseError();
    DRScheduleDetail scheduleDetail = this._drEntityStorage.Insert(new DRScheduleDetail()
    {
      ScheduleID = this._schedule.ScheduleID,
      BranchID = this._branchID,
      ComponentID = componentID,
      CuryTotalAmt = new Decimal?(amount),
      CuryDefAmt = new Decimal?(amount),
      DefCode = defCode.DeferredCodeID,
      Status = this._isDraft ? "D" : "O",
      IsCustom = new bool?(false),
      IsOpen = new bool?(true),
      Module = this._schedule.Module,
      DocType = this._schedule.DocType,
      RefNbr = this._schedule.RefNbr,
      LineNbr = this._schedule.LineNbr,
      FinPeriodID = valueOrRaiseError.FinPeriodID,
      TranPeriodID = valueOrRaiseError.MasterFinPeriodID,
      BAccountID = this._schedule.BAccountID,
      AccountID = salesOrExpenseAccountSubaccount.AccountID,
      SubID = overridenSubID ?? salesOrExpenseAccountSubaccount.SubID,
      DefAcctID = deferralAccountSubaccount.AccountID,
      DefSubID = deferralAccountSubaccount.SubID,
      CreditLineNbr = new int?(0),
      DocDate = this._schedule.DocDate,
      BAccountType = this._schedule.Module == "AP" ? "VE" : "CU"
    });
    if (!this._isDraft)
      this._drEntityStorage.CreateCreditLineTransaction(scheduleDetail, defCode, this._branchID);
    return scheduleDetail;
  }

  private void InsertResidualScheduleDetail(
    int? componentID,
    Decimal amount,
    int? acctID,
    int? subID)
  {
    FinPeriod valueOrRaiseError = this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(this._branchID), this._schedule.FinPeriodID).GetValueOrRaiseError();
    DRScheduleDetail scheduleDetail = new DRScheduleDetail()
    {
      ScheduleID = this._schedule.ScheduleID,
      BranchID = this._branchID,
      ComponentID = componentID,
      CuryTotalAmt = new Decimal?(amount),
      CuryDefAmt = new Decimal?(0.0M),
      DefCode = (string) null,
      IsCustom = new bool?(false),
      IsResidual = new bool?(true),
      IsOpen = new bool?(false),
      Module = this._schedule.Module,
      DocType = this._schedule.DocType,
      RefNbr = this._schedule.RefNbr,
      LineNbr = this._schedule.LineNbr,
      FinPeriodID = valueOrRaiseError.FinPeriodID,
      TranPeriodID = valueOrRaiseError.MasterFinPeriodID,
      BAccountID = this._schedule.BAccountID,
      AccountID = acctID,
      SubID = subID,
      DefAcctID = acctID,
      DefSubID = subID,
      CreditLineNbr = new int?(0),
      DocDate = this._schedule.DocDate,
      BAccountType = this._schedule.Module == "AP" ? "VE" : "CU",
      Status = this._isDraft ? "D" : "C"
    };
    scheduleDetail.CloseFinPeriodID = scheduleDetail.FinPeriodID;
    this._drEntityStorage.Insert(scheduleDetail);
  }

  private AccountSubaccountPair GetDeferralAccountSubaccount(
    DRDeferredCode deferralCode,
    PX.Objects.IN.InventoryItem item,
    DRProcess.DRScheduleParameters scheduleParameters)
  {
    int? accountID = deferralCode.AccountID;
    string subaccountCD = (string) null;
    int? subID = new int?();
    if (deferralCode.AccountSource == "I")
      accountID = item != null ? item.DeferralAcctID : subID;
    if (deferralCode.CopySubFromSourceTran.GetValueOrDefault())
      subID = scheduleParameters.SubID;
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
}

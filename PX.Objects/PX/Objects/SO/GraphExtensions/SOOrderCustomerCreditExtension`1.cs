// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderCustomerCreditExtension`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Automation;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.Extensions.CustomerCreditHold;
using System;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO.GraphExtensions;

/// <summary>A mapped generic graph extension that defines the SO credit helper functionality.</summary>
public abstract class SOOrderCustomerCreditExtension<TGraph> : 
  CustomerCreditExtension<TGraph, PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.customerID, PX.Objects.SO.SOOrder.creditHold, PX.Objects.SO.SOOrder.completed, PX.Objects.SO.SOOrder.status>
  where TGraph : PXGraph
{
  protected override bool? GetReleasedValue(PXCache sender, PX.Objects.SO.SOOrder row)
  {
    return new bool?(row != null && row.Cancelled.GetValueOrDefault() || row != null && row.Completed.GetValueOrDefault());
  }

  protected override bool? GetHoldValue(PXCache sender, PX.Objects.SO.SOOrder row)
  {
    int num;
    if ((row == null || !row.Hold.GetValueOrDefault()) && (row == null || !row.CreditHold.GetValueOrDefault()))
    {
      if (row == null)
      {
        num = 0;
      }
      else
      {
        bool? inclCustOpenOrders = row.InclCustOpenOrders;
        bool flag = false;
        num = inclCustOpenOrders.GetValueOrDefault() == flag & inclCustOpenOrders.HasValue ? 1 : 0;
      }
    }
    else
      num = 1;
    return new bool?(num != 0);
  }

  protected override bool? GetCreditCheckError(PXCache sender, PX.Objects.SO.SOOrder row)
  {
    return new bool?(((bool?) SOOrderType.PK.Find(sender.Graph, row.OrderType)?.CreditHoldEntry).GetValueOrDefault() || this.EnsureCustomer(sender, row)?.Status == "C");
  }

  private bool IsLongOperationProcessing(PXGraph graph) => PXLongOperation.Exists(graph);

  public override void Verify(PXCache sender, PX.Objects.SO.SOOrder Row, EventArgs e)
  {
    if (this.IsLongOperationProcessing(sender.Graph) && !this.VerifyOnLongRun(sender, Row, e))
      return;
    PX.Objects.AR.Customer customer;
    if (e is PXRowPersistingEventArgs || (Row != null ? (!Row.PrepaymentReqSatisfied.GetValueOrDefault() ? 1 : 0) : 1) != 0 || (customer = this.EnsureCustomer(sender, Row))?.Status != "C")
      base.Verify(sender, Row, e);
    else
      this.ApplyCreditVerificationResult(sender, Row, e, customer, new CreditVerificationResult()
      {
        Enforce = CreditVerificationResult.EnforceType.AdminHold,
        Failed = true,
        ErrorMessage = "The customer status is 'Credit Hold'."
      });
  }

  public virtual bool VerifyOnLongRun(PXCache sender, PX.Objects.SO.SOOrder Row, EventArgs e)
  {
    if (!(e is PXRowUpdatedEventArgs updatedEventArgs))
      return false;
    bool? holdValue1 = this.GetHoldValue(sender, Row);
    bool? holdValue2 = this.GetHoldValue(sender, (PX.Objects.SO.SOOrder) updatedEventArgs.OldRow);
    return !(holdValue1.GetValueOrDefault() == holdValue2.GetValueOrDefault() & holdValue1.HasValue == holdValue2.HasValue);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.SO.SOOrder> e)
  {
    if (e.Row == null)
      return;
    this.UpdateARBalances(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PX.Objects.SO.SOOrder>>) e).Cache, e.Row, (PX.Objects.SO.SOOrder) null);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.UpdateCustomerBalances(PX.Data.PXCache,PX.Objects.SO.SOOrder,PX.Objects.SO.SOOrder)" />
  /// </summary>
  [PXOverride]
  public virtual void UpdateCustomerBalances(
    PXCache cache,
    PX.Objects.SO.SOOrder row,
    PX.Objects.SO.SOOrder oldRow,
    Action<PXCache, PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder> baseMethod)
  {
    if (row == null || oldRow == null)
      return;
    this.UpdateARBalances(cache, row, oldRow);
  }

  protected override void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder> e)
  {
    if (this._InternalCall)
      return;
    base._(e);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.SO.SOOrder> e)
  {
    if (e.Row == null)
      return;
    this.UpdateARBalances(((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<PX.Objects.SO.SOOrder>>) e).Cache, (PX.Objects.SO.SOOrder) null, e.Row);
  }

  protected override Decimal? GetDocumentBalance(PXCache cache, PX.Objects.SO.SOOrder row)
  {
    Decimal? documentBalance = base.GetDocumentBalance(cache, row);
    Decimal? nullable = documentBalance;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() > num & nullable.HasValue && this.IsFullAmountApproved(row))
      documentBalance = new Decimal?(0M);
    return documentBalance;
  }

  protected override bool IsPutOnCreditHoldAllowed(PXCache cache, PX.Objects.SO.SOOrder row)
  {
    return !row.ApprovedCreditByPayment.GetValueOrDefault() ? base.IsPutOnCreditHoldAllowed(cache, row) : !row.IsFullyPaid.GetValueOrDefault();
  }

  protected override void PlaceOnHold(
    PXCache cache,
    PX.Objects.SO.SOOrder order,
    CreditVerificationResult.EnforceType enforceType)
  {
    if (!order.Hold.GetValueOrDefault())
      ((SelectedEntityEvent<PX.Objects.SO.SOOrder>) PXEntityEventBase<PX.Objects.SO.SOOrder>.Container<PX.Objects.SO.SOOrder.Events>.Select((Expression<Func<PX.Objects.SO.SOOrder.Events, PXEntityEvent<PX.Objects.SO.SOOrder.Events>>>) (e => e.CreditLimitViolated))).FireOn((PXGraph) this.Base, order);
    base.PlaceOnHold(cache, order, enforceType);
    cache.SetValue<PX.Objects.SO.SOOrder.approvedCredit>((object) order, (object) false);
    cache.SetValue<PX.Objects.SO.SOOrder.approvedCreditAmt>((object) order, (object) 0M);
  }

  protected override void ApplyCreditVerificationResult(
    PXCache sender,
    PX.Objects.SO.SOOrder row,
    CreditVerificationResult res,
    PXCache arbalancescache)
  {
    if (row.IsFullyPaid.GetValueOrDefault())
    {
      SOOrderType soOrderType = SOOrderType.PK.Find((PXGraph) this.Base, row.OrderType);
      if ((soOrderType != null ? (soOrderType.RemoveCreditHoldByPayment.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        row.SatisfyCreditLimitByPayment((PXGraph) this.Base);
        return;
      }
    }
    base.ApplyCreditVerificationResult(sender, row, res, arbalancescache);
  }

  protected override Decimal? GetDocumentSign(PXCache sender, PX.Objects.SO.SOOrder row)
  {
    return ARDocType.SignBalance(row.ARDocType);
  }

  public override void UpdateARBalances(PXCache cache, PX.Objects.SO.SOOrder newRow, PX.Objects.SO.SOOrder oldRow)
  {
    if (oldRow != null && newRow != null && cache.ObjectsEqualBy<TypeArrayOf<IBqlField>.FilledWith<PX.Objects.SO.SOOrder.unbilledOrderTotal, PX.Objects.SO.SOOrder.openOrderTotal, PX.Objects.SO.SOOrder.inclCustOpenOrders, PX.Objects.SO.SOOrder.hold, PX.Objects.SO.SOOrder.creditHold, PX.Objects.SO.SOOrder.customerID, PX.Objects.SO.SOOrder.customerLocationID, PX.Objects.SO.SOOrder.aRDocType, PX.Objects.SO.SOOrder.branchID, PX.Objects.SO.SOOrder.shipmentCntr, PX.Objects.SO.SOOrder.cancelled>>((object) oldRow, (object) newRow))
      return;
    if (oldRow != null)
    {
      PXGraph graph = cache.Graph;
      PX.Objects.SO.SOOrder order = oldRow;
      Decimal? unbilledOrderTotal = oldRow.UnbilledOrderTotal;
      Decimal? UnbilledAmount = unbilledOrderTotal.HasValue ? new Decimal?(-unbilledOrderTotal.GetValueOrDefault()) : new Decimal?();
      Decimal? openOrderTotal = oldRow.OpenOrderTotal;
      Decimal? UnshippedAmount = openOrderTotal.HasValue ? new Decimal?(-openOrderTotal.GetValueOrDefault()) : new Decimal?();
      ARReleaseProcess.UpdateARBalances(graph, order, UnbilledAmount, UnshippedAmount);
    }
    if (newRow == null)
      return;
    ARReleaseProcess.UpdateARBalances(cache.Graph, newRow, newRow.UnbilledOrderTotal, newRow.OpenOrderTotal);
  }

  protected virtual bool IsFullAmountApproved(PX.Objects.SO.SOOrder row)
  {
    if (!row.ApprovedCredit.GetValueOrDefault())
      return false;
    Decimal? approvedCreditAmt = row.ApprovedCreditAmt;
    Decimal? orderTotal = row.OrderTotal;
    return approvedCreditAmt.GetValueOrDefault() >= orderTotal.GetValueOrDefault() & approvedCreditAmt.HasValue & orderTotal.HasValue;
  }

  protected override bool IsCreditVerificationEnabled()
  {
    return this.WorkflowService.GetPossibleTransition((PXGraph) this.Base, "R").ToList<TransitionInfo>().Any<TransitionInfo>((Func<TransitionInfo, bool>) (t => ((ActionInfo) t).ActionName == "OnCreditLimitViolated"));
  }
}

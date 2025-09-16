// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.DisabledTaxCalculationExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.TX;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class DisabledTaxCalculationExtension : PXGraphExtension<SOOrderEntry>
{
  protected virtual void _(Events.RowSelected<SOOrder> e)
  {
    if (e.Row == null)
      return;
    bool? nullable = e.Row.DisableAutomaticTaxCalculation;
    if (!nullable.GetValueOrDefault())
      return;
    if (((IEnumerable<PXResult<SOTaxTran>>) ((PXSelectBase<SOTaxTran>) this.Base.Taxes).Select(Array.Empty<object>())).ToList<PXResult<SOTaxTran>>().Count > 0)
    {
      PXCache cache = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<SOOrder>>) e).Cache;
      SOOrder row = e.Row;
      nullable = e.Row.IsManualTaxesValid;
      bool flag = false;
      string str = nullable.GetValueOrDefault() == flag & nullable.HasValue ? "Tax is not up-to-date." : (string) null;
      PXUIFieldAttribute.SetWarning<SOOrder.curyTaxTotal>(cache, (object) row, str);
    }
    else
    {
      PXCache cache = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<SOOrder>>) e).Cache;
      SOOrder row = e.Row;
      nullable = e.Row.IsManualTaxesValid;
      bool flag = false;
      string str = nullable.GetValueOrDefault() == flag & nullable.HasValue ? "Taxes will not be calculated automatically because the Disable Automatic Tax Calculation check box is selected on the Financial tab. Enter taxes manually on the Taxes tab or clear this check box to automatically calculate taxes." : (string) null;
      PXUIFieldAttribute.SetWarning<SOOrder.curyTaxTotal>(cache, (object) row, str);
    }
  }

  protected virtual void _(Events.RowUpdated<SOTaxTran> e)
  {
    if (e.Row == null)
      return;
    SOOrder current = ((PXSelectBase<SOOrder>) this.Base.Document).Current;
    if ((current != null ? (current.DisableAutomaticTaxCalculation.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ((PXSelectBase<SOOrder>) this.Base.Document).Current.IsManualTaxesValid = new bool?(true);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<SOOrder>) this.Base.Document).Current);
  }

  protected virtual void _(Events.RowUpdated<SOOrder> e)
  {
    if (!((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<SOOrder>>) e).Cache.ObjectsEqual<SOOrder.disableAutomaticTaxCalculation>((object) e.Row, (object) e.OldRow))
      e.Row.IsManualTaxesValid = new bool?(true);
    if (!e.Row.DisableAutomaticTaxCalculation.GetValueOrDefault())
      return;
    if (!((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<SOOrder>>) e).Cache.ObjectsEqual<SOOrder.orderDate, SOOrder.taxZoneID, SOOrder.customerID, SOOrder.shipAddressID, SOOrder.willCall, SOOrder.curyFreightTot, SOOrder.freightTaxCategoryID>((object) e.Row, (object) e.OldRow) || !((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<SOOrder>>) e).Cache.ObjectsEqual<SOOrder.externalTaxExemptionNumber, SOOrder.avalaraCustomerUsageType, SOOrder.curyDiscTot, SOOrder.branchID>((object) e.Row, (object) e.OldRow))
      e.Row.IsManualTaxesValid = new bool?(false);
    if (((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<SOOrder>>) e).Cache.ObjectsEqual<SOOrder.taxZoneID>((object) e.Row, (object) e.OldRow))
      return;
    foreach (PXResult<SOLine> pxResult in ((PXSelectBase<SOLine>) this.Base.Transactions).Select(Array.Empty<object>()))
    {
      SOLine soLine = PXResult<SOLine>.op_Implicit(pxResult);
      ((PXSelectBase) this.Base.Transactions).Cache.SetValue<SOLine.taxZoneID>((object) soLine, (object) e.Row?.TaxZoneID);
      GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Transactions).Cache, (object) soLine);
    }
  }

  protected virtual void _(Events.RowInserted<SOLine> e)
  {
    this.InvalidateManualTaxes(((PXSelectBase<SOOrder>) this.Base.Document).Current);
  }

  protected virtual void _(Events.RowUpdated<SOLine> e)
  {
    SOOrder current = ((PXSelectBase<SOOrder>) this.Base.Document).Current;
    if ((current != null ? (current.DisableAutomaticTaxCalculation.GetValueOrDefault() ? 1 : 0) : 0) == 0 || ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<SOLine>>) e).Cache.ObjectsEqual<SOLine.avalaraCustomerUsageType, SOLine.salesAcctID, SOLine.inventoryID, SOLine.tranDesc, SOLine.lineAmt, SOLine.orderDate, SOLine.taxCategoryID, SOLine.siteID>((object) e.Row, (object) e.OldRow) && e.Row.POSource == "D" == (e.OldRow.POSource == "D"))
      return;
    this.InvalidateManualTaxes(((PXSelectBase<SOOrder>) this.Base.Document).Current);
  }

  protected virtual void _(Events.RowDeleted<SOLine> e)
  {
    SOOrder current = ((PXSelectBase<SOOrder>) this.Base.Document).Current;
    if ((current != null ? (current.DisableAutomaticTaxCalculation.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    this.InvalidateManualTaxes(((PXSelectBase<SOOrder>) this.Base.Document).Current);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<SOOrder>) this.Base.Document).Current);
  }

  protected virtual void _(Events.RowUpdated<SOShippingAddress> e)
  {
    if (e.Row == null || ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<SOShippingAddress>>) e).Cache.ObjectsEqual<SOShippingAddress.postalCode, SOShippingAddress.countryID, SOShippingAddress.state, SOShippingAddress.latitude, SOShippingAddress.longitude>((object) e.Row, (object) e.OldRow))
      return;
    this.InvalidateManualTaxes(((PXSelectBase<SOOrder>) this.Base.Document).Current);
  }

  protected virtual void _(Events.RowInserted<SOShippingAddress> e)
  {
    if (e.Row == null)
      return;
    this.InvalidateManualTaxes(((PXSelectBase<SOOrder>) this.Base.Document).Current);
  }

  protected virtual void _(Events.RowDeleted<SOShippingAddress> e)
  {
    if (e.Row == null)
      return;
    this.InvalidateManualTaxes(((PXSelectBase<SOOrder>) this.Base.Document).Current);
  }

  protected virtual void _(
    Events.FieldUpdating<SOShippingAddress, SOShippingAddress.overrideAddress> e)
  {
    if (e.Row == null)
      return;
    this.InvalidateManualTaxes(((PXSelectBase<SOOrder>) this.Base.Document).Current);
  }

  public void InvalidateManualTaxes(SOOrder order)
  {
    if (order == null || !order.DisableAutomaticTaxCalculation.GetValueOrDefault())
      return;
    order.IsManualTaxesValid = new bool?(false);
  }

  protected virtual void _(
    Events.FieldUpdated<SOOrder, SOOrder.disableAutomaticTaxCalculation> e)
  {
    if (e.Row == null)
      return;
    foreach (PXResult<SOLine> pxResult in ((PXSelectBase<SOLine>) this.Base.Transactions).Select(Array.Empty<object>()))
    {
      SOLine soLine = PXResult<SOLine>.op_Implicit(pxResult);
      soLine.DisableAutomaticTaxCalculation = new bool?(e.Row.DisableAutomaticTaxCalculation.GetValueOrDefault());
      ((PXSelectBase<SOLine>) this.Base.Transactions).Update(soLine);
    }
    if (e.Row.DisableAutomaticTaxCalculation.GetValueOrDefault())
      return;
    TaxBaseAttribute.SetTaxCalc<SOLine.taxCategoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, TaxCalc.ManualLineCalc | TaxCalc.RecalculateAlways);
    ((PXSelectBase) this.Base.Document).Cache.RaiseFieldUpdated<SOOrder.taxZoneID>((object) e.Row, (object) null);
  }

  protected virtual void _(
    Events.FieldVerifying<SOOrder, SOOrder.disableAutomaticTaxCalculation> e)
  {
    if (e.Row == null || !((bool?) ((Events.FieldVerifyingBase<Events.FieldVerifying<SOOrder, SOOrder.disableAutomaticTaxCalculation>, SOOrder, object>) e).NewValue).GetValueOrDefault())
      return;
    if (e.Row.TaxCalcMode == "G")
    {
      ((Events.FieldVerifyingBase<Events.FieldVerifying<SOOrder, SOOrder.disableAutomaticTaxCalculation>, SOOrder, object>) e).NewValue = (object) false;
      throw new PXSetPropertyException("The Disable Automatic Tax Calculation check box and the Gross tax calculation mode on the Financial tab of the Sales Orders (SO301000) form cannot be selected simultaneously for a sales order.");
    }
    if (!(e.Row.TaxCalcMode != "N"))
      return;
    foreach (PXResult<SOTaxTran, PX.Objects.TX.Tax> pxResult in ((PXSelectBase<SOTaxTran>) this.Base.Taxes).Select(Array.Empty<object>()))
    {
      if (PXResult<SOTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult)?.TaxCalcLevel == "0")
      {
        ((Events.FieldVerifyingBase<Events.FieldVerifying<SOOrder, SOOrder.disableAutomaticTaxCalculation>, SOOrder, object>) e).NewValue = (object) false;
        throw new PXSetPropertyException("The Disable Automatic Tax Calculation check box cannot be selected on the Financial tab of the Sales Orders (SO301000) form for a sales order that has at least one tax with the Inclusive Line-Level or Inclusive Document-Level calculation rule specified on the Taxes (TX205000) form.");
      }
    }
  }

  protected virtual void _(
    Events.FieldVerifying<SOOrder, SOOrder.taxCalcMode> e)
  {
    if (e.Row != null && (string) ((Events.FieldVerifyingBase<Events.FieldVerifying<SOOrder, SOOrder.taxCalcMode>, SOOrder, object>) e).NewValue == "G" && e.Row.DisableAutomaticTaxCalculation.GetValueOrDefault())
      throw new PXSetPropertyException("The Disable Automatic Tax Calculation check box and the Gross tax calculation mode on the Financial tab of the Sales Orders (SO301000) form cannot be selected simultaneously for a sales order.");
  }

  protected virtual void _(
    Events.FieldVerifying<SOTaxTran, SOTaxTran.taxID> e)
  {
    if (e.Row == null)
      return;
    SOOrder current = ((PXSelectBase<SOOrder>) this.Base.Document).Current;
    if ((current != null ? (current.DisableAutomaticTaxCalculation.GetValueOrDefault() ? 1 : 0) : 0) == 0 || ((Events.FieldVerifyingBase<Events.FieldVerifying<SOTaxTran, SOTaxTran.taxID>, SOTaxTran, object>) e).NewValue == null || !(((PXSelectBase<SOOrder>) this.Base.Document).Current?.TaxCalcMode != "N") || !(PX.Objects.TX.Tax.PK.Find((PXGraph) this.Base, (string) ((Events.FieldVerifyingBase<Events.FieldVerifying<SOTaxTran, SOTaxTran.taxID>, SOTaxTran, object>) e).NewValue)?.TaxCalcLevel == "0"))
      return;
    ((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<SOTaxTran, SOTaxTran.taxID>>) e).Cache.RaiseExceptionHandling<SOTaxTran.taxID>((object) e.Row, (object) e.Row.TaxID, (Exception) new PXSetPropertyException("The tax cannot be added because the Inclusive Line-Level or Inclusive Document-Level calculation rule is selected for it on the Taxes (TX205000) form and the Disable Automatic Tax Calculation check box is selected on the Financial tab of the current form. Clear this check box to allow automatic tax calculation of inclusive taxes.", (PXErrorLevel) 5));
  }
}

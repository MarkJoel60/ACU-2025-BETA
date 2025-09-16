// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderEntryExternalTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.TX;
using PX.TaxProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

public class SOOrderEntryExternalTax : ExternalTax<SOOrderEntry, SOOrder>
{
  public PXAction<SOOrder> recalcExternalTax;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();

  public virtual bool CalculateTaxesUsingExternalProvider(string taxZoneID)
  {
    bool? nullable;
    int num1;
    if (((PXSelectBase<SOOrder>) this.Base.Document).Current != null)
    {
      nullable = ((PXSelectBase<SOOrder>) this.Base.Document).Current.ExternalTaxesImportInProgress;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag1 = num1 != 0;
    SOOrder current = ((PXSelectBase<SOOrder>) this.Base.Document).Current;
    int num2;
    if (current == null)
    {
      num2 = 1;
    }
    else
    {
      nullable = current.DisableAutomaticTaxCalculation;
      num2 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool flag2 = num2 != 0;
    return ((!ExternalTaxBase<SOOrderEntry>.IsExternalTax((PXGraph) this.Base, taxZoneID) ? 0 : (!flag1 ? 1 : 0)) & (flag2 ? 1 : 0)) != 0;
  }

  public override SOOrder CalculateExternalTax(SOOrder order)
  {
    if (!this.CalculateTaxesUsingExternalProvider(order.TaxZoneID))
      return order;
    using (new ExternalTaxRecalculationScope())
      return this.CalculateExternalTax(order, false);
  }

  public virtual SOOrder CalculateExternalTax(SOOrder order, bool forceRecalculate)
  {
    IAddressLocation toAddress = this.GetToAddress(order);
    Stopwatch stopwatch1 = new Stopwatch();
    stopwatch1.Start();
    ITaxProvider itaxProvider = ExternalTaxBase<SOOrderEntry>.TaxProviderFactory((PXGraph) this.Base, order.TaxZoneID);
    GetTaxRequest x = (GetTaxRequest) null;
    GetTaxRequest y1 = (GetTaxRequest) null;
    GetTaxRequest y2 = (GetTaxRequest) null;
    bool flag1 = true;
    SOOrderType soOrderType = (SOOrderType) ((PXSelectBase) this.Base.soordertype).View.SelectSingleBound(new object[1]
    {
      (object) order
    }, Array.Empty<object>());
    bool? nullable;
    int num;
    if (order.IsTaxValid.GetValueOrDefault())
    {
      nullable = order.IsOpenTaxValid;
      if (nullable.GetValueOrDefault())
      {
        nullable = order.IsUnbilledTaxValid;
        if (nullable.GetValueOrDefault() && !forceRecalculate && soOrderType.INDocType != "TRX")
        {
          num = !this.IsNonTaxable((IAddressBase) toAddress) ? 1 : 0;
          goto label_5;
        }
      }
    }
    num = 0;
label_5:
    if (num != 0)
      return order;
    if (soOrderType.INDocType != "TRX" && !this.IsNonTaxable((IAddressBase) toAddress))
    {
      nullable = order.IsTaxValid;
      if (!nullable.GetValueOrDefault() | forceRecalculate)
      {
        x = this.BuildGetTaxRequest(order);
        if (x.CartItems.Count > 0)
          flag1 = false;
        else
          x = (GetTaxRequest) null;
      }
      nullable = order.IsOpenTaxValid;
      if (!nullable.GetValueOrDefault() | forceRecalculate)
      {
        y1 = this.BuildGetTaxRequestOpen(order);
        if (y1.CartItems.Count > 0)
          flag1 = false;
        else
          y1 = (GetTaxRequest) null;
      }
      nullable = order.IsUnbilledTaxValid;
      if (!nullable.GetValueOrDefault() | forceRecalculate)
      {
        y2 = this.BuildGetTaxRequestUnbilled(order);
        if (y2.CartItems.Count > 0)
          flag1 = false;
        else
          y2 = (GetTaxRequest) null;
      }
    }
    if (flag1)
    {
      order.CuryTaxTotal = new Decimal?(0M);
      order.CuryOpenTaxTotal = new Decimal?(0M);
      order.CuryUnbilledTaxTotal = new Decimal?(0M);
      order.IsTaxValid = new bool?(true);
      order.IsOpenTaxValid = new bool?(true);
      order.IsUnbilledTaxValid = new bool?(true);
      order = ((PXSelectBase<SOOrder>) this.Base.Document).Update(order);
      foreach (PXResult<SOTaxTran> pxResult in ((PXSelectBase<SOTaxTran>) this.Base.Taxes).Select(Array.Empty<object>()))
        ((PXSelectBase<SOTaxTran>) this.Base.Taxes).Delete(PXResult<SOTaxTran>.op_Implicit(pxResult));
      this.SkipTaxCalcAndSave();
      return order;
    }
    GetTaxResult result1 = (GetTaxResult) null;
    GetTaxResult resultOpen = (GetTaxResult) null;
    GetTaxResult resultUnbilled = (GetTaxResult) null;
    bool flag2 = false;
    if (x != null)
    {
      Stopwatch stopwatch2 = new Stopwatch();
      stopwatch2.Start();
      result1 = itaxProvider.GetTax(x);
      stopwatch2.Stop();
      if (!((ResultBase) result1).IsSuccess)
        flag2 = true;
    }
    if (y1 != null)
    {
      if (x != null && this.IsSame(x, y1))
      {
        resultOpen = result1;
      }
      else
      {
        Stopwatch stopwatch3 = new Stopwatch();
        stopwatch3.Start();
        resultOpen = itaxProvider.GetTax(y1);
        stopwatch3.Stop();
        if (!((ResultBase) resultOpen).IsSuccess)
          flag2 = true;
      }
    }
    if (y2 != null)
    {
      if (x != null && this.IsSame(x, y2))
      {
        resultUnbilled = result1;
      }
      else
      {
        Stopwatch stopwatch4 = new Stopwatch();
        stopwatch4.Start();
        resultUnbilled = itaxProvider.GetTax(y2);
        stopwatch4.Stop();
        if (!((ResultBase) resultUnbilled).IsSuccess)
          flag2 = true;
      }
    }
    if (!flag2)
    {
      this.ApplyExternalTaxes(order, result1, resultOpen, resultUnbilled);
      this.Base.RecalcUnbilledTax();
      stopwatch1.Stop();
      return order;
    }
    ResultBase result2 = (ResultBase) (result1 ?? resultOpen ?? resultUnbilled);
    if (result2 != null)
      this.LogMessages(result2);
    throw new PXException("Failed to get taxes from the external tax provider. Check Trace Log for details.");
  }

  public void ApplyExternalTaxes(
    SOOrder order,
    GetTaxResult result,
    GetTaxResult resultOpen,
    GetTaxResult resultUnbilled)
  {
    this.ApplyExternalTaxes(order, result, resultOpen, resultUnbilled, true);
  }

  public void ApplyExternalTaxes(
    SOOrder order,
    GetTaxResult result,
    GetTaxResult resultOpen,
    GetTaxResult resultUnbilled,
    bool catchException)
  {
    try
    {
      this.ApplyTax(order, result, resultOpen, resultUnbilled);
    }
    catch (PXOuterException ex) when (catchException)
    {
      string str = "The tax amount calculated by the external tax provider cannot be applied to the document.";
      foreach (string innerMessage in ex.InnerMessages)
        str = str + Environment.NewLine + innerMessage;
      throw new PXException((Exception) ex, str, Array.Empty<object>());
    }
    catch (Exception ex) when (catchException)
    {
      string str = $"The tax amount calculated by the external tax provider cannot be applied to the document.{Environment.NewLine}{ex.Message}";
      throw new PXException(ex, str, Array.Empty<object>());
    }
  }

  [PXOverride]
  public virtual void RecalculateExternalTaxes()
  {
    if (((PXSelectBase<SOOrder>) this.Base.Document).Current == null || !this.CalculateTaxesUsingExternalProvider(((PXSelectBase<SOOrder>) this.Base.Document).Current.TaxZoneID) || this.skipExternalTaxCalcOnSave)
      return;
    bool? nullable = ((PXSelectBase<SOOrder>) this.Base.Document).Current.IsTransferOrder;
    if (nullable.GetValueOrDefault())
      return;
    nullable = ((PXSelectBase<SOOrder>) this.Base.Document).Current.ExternalTaxesImportInProgress;
    if (nullable.GetValueOrDefault())
      return;
    nullable = ((PXSelectBase<SOOrder>) this.Base.Document).Current.IsTaxValid;
    if (nullable.GetValueOrDefault())
    {
      nullable = ((PXSelectBase<SOOrder>) this.Base.Document).Current.IsOpenTaxValid;
      if (nullable.GetValueOrDefault())
      {
        nullable = ((PXSelectBase<SOOrder>) this.Base.Document).Current.IsUnbilledTaxValid;
        if (nullable.GetValueOrDefault())
          return;
      }
    }
    if (this.Base.RecalculateExternalTaxesSync)
    {
      this.CalculateExternalTax(((PXSelectBase<SOOrder>) this.Base.Document).Current);
    }
    else
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) new SOOrderEntryExternalTax.\u003C\u003Ec__DisplayClass6_0()
      {
        doc = new SOOrder()
        {
          OrderType = ((PXSelectBase<SOOrder>) this.Base.Document).Current.OrderType,
          OrderNbr = ((PXSelectBase<SOOrder>) this.Base.Document).Current.OrderNbr
        }
      }, __methodptr(\u003CRecalculateExternalTaxes\u003Eb__0)));
    }
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable RecalcExternalTax(PXAdapter adapter)
  {
    SOOrderEntryExternalTax entryExternalTax = this;
    SOOrder current1 = ((PXSelectBase<SOOrder>) entryExternalTax.Base.Document).Current;
    if ((current1 != null ? (current1.DisableAutomaticTaxCalculation.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXException("Taxes cannot be recalculated because the Disable Automatic Tax Calculation check box is selected on the Financial tab. To recalculate taxes, clear this check box.");
    if (((PXSelectBase<SOOrder>) entryExternalTax.Base.Document).Current != null && entryExternalTax.CalculateTaxesUsingExternalProvider(((PXSelectBase<SOOrder>) entryExternalTax.Base.Document).Current.TaxZoneID))
    {
      SOOrder current2 = ((PXSelectBase<SOOrder>) entryExternalTax.Base.Document).Current;
      entryExternalTax.CalculateExternalTax(((PXSelectBase<SOOrder>) entryExternalTax.Base.Document).Current, true);
      ((PXGraph) entryExternalTax.Base).Clear((PXClearOption) 3);
      ((PXSelectBase<SOOrder>) entryExternalTax.Base.Document).Current = PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) entryExternalTax.Base.Document).Search<SOOrder.orderNbr>((object) current2.OrderNbr, new object[1]
      {
        (object) current2.OrderType
      }));
      yield return (object) ((PXSelectBase<SOOrder>) entryExternalTax.Base.Document).Current;
    }
    else
    {
      foreach (object obj in adapter.Get())
        yield return obj;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<SOOrder> e)
  {
    if (e.Row == null)
      return;
    bool externalProvider = this.CalculateTaxesUsingExternalProvider(e.Row.TaxZoneID);
    bool? nullable;
    if (externalProvider)
    {
      nullable = e.Row.IsTaxValid;
      if (!nullable.GetValueOrDefault())
      {
        nullable = e.Row.IsTransferOrder;
        if (!nullable.GetValueOrDefault())
          PXUIFieldAttribute.SetWarning<SOOrder.curyTaxTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOOrder>>) e).Cache, (object) e.Row, "Tax is not up-to-date.");
      }
    }
    if (externalProvider)
    {
      nullable = e.Row.IsOpenTaxValid;
      if (!nullable.GetValueOrDefault())
      {
        nullable = e.Row.IsTransferOrder;
        if (!nullable.GetValueOrDefault())
          PXUIFieldAttribute.SetWarning<SOOrder.curyOpenTaxTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOOrder>>) e).Cache, (object) e.Row, "Tax is not up-to-date.");
      }
    }
    if (externalProvider)
    {
      nullable = e.Row.IsUnbilledTaxValid;
      if (!nullable.GetValueOrDefault())
      {
        nullable = e.Row.IsTransferOrder;
        if (!(nullable.HasValue ? new bool?(!nullable.GetValueOrDefault()) : new bool?()).GetValueOrDefault())
        {
          PXUIFieldAttribute.SetWarning<SOOrder.curyUnbilledTaxTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOOrder>>) e).Cache, (object) e.Row, "Tax is not up-to-date.");
          PXUIFieldAttribute.SetWarning<SOOrder.curyUnbilledOrderTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOOrder>>) e).Cache, (object) e.Row, "Balance does not include Tax. Unbilled Tax is not up-to-date.");
        }
      }
    }
    nullable = e.Row.Completed;
    int num1;
    if (!nullable.GetValueOrDefault())
    {
      nullable = e.Row.Cancelled;
      if (!nullable.GetValueOrDefault() && (((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOOrder>>) e).Cache.Graph.IsContractBasedAPI || !externalProvider))
      {
        nullable = e.Row.DisableAutomaticTaxCalculation;
        if (nullable.GetValueOrDefault())
        {
          int? billedCntr = e.Row.BilledCntr;
          int num2 = 0;
          if (billedCntr.GetValueOrDefault() == num2 & billedCntr.HasValue)
          {
            int? releasedCntr = e.Row.ReleasedCntr;
            int num3 = 0;
            num1 = releasedCntr.GetValueOrDefault() == num3 & releasedCntr.HasValue ? 1 : 0;
            goto label_21;
          }
          num1 = 0;
          goto label_21;
        }
        num1 = 1;
        goto label_21;
      }
    }
    num1 = 0;
label_21:
    ((PXSelectBase) this.Base.Taxes).Cache.SetAllEditPermissions(num1 != 0);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SOOrder> e)
  {
    if (!this.CalculateTaxesUsingExternalProvider(e.Row.TaxZoneID) || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOOrder>>) e).Cache.ObjectsEqual<SOOrder.orderDate, SOOrder.taxZoneID, SOOrder.customerID, SOOrder.shipAddressID, SOOrder.willCall, SOOrder.curyFreightTot, SOOrder.freightTaxCategoryID>((object) e.Row, (object) e.OldRow) && ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOOrder>>) e).Cache.ObjectsEqual<SOOrder.externalTaxExemptionNumber, SOOrder.avalaraCustomerUsageType, SOOrder.curyDiscTot, SOOrder.branchID, SOOrder.disableAutomaticTaxCalculation, SOOrder.taxCalcMode>((object) e.Row, (object) e.OldRow))
      return;
    e.Row.IsTaxValid = new bool?(false);
    e.Row.IsOpenTaxValid = new bool?(false);
    e.Row.IsUnbilledTaxValid = new bool?(false);
  }

  protected virtual void _(PX.Data.Events.RowInserted<SOLine> e)
  {
    this.InvalidateExternalTax(((PXSelectBase<SOOrder>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SOLine> e)
  {
    if (((PXSelectBase<SOOrder>) this.Base.Document).Current == null || !this.CalculateTaxesUsingExternalProvider(((PXSelectBase<SOOrder>) this.Base.Document).Current.TaxZoneID))
      return;
    if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOLine>>) e).Cache.ObjectsEqual<SOLine.avalaraCustomerUsageType, SOLine.salesAcctID, SOLine.inventoryID, SOLine.tranDesc, SOLine.lineAmt, SOLine.orderDate, SOLine.taxCategoryID, SOLine.siteID>((object) e.Row, (object) e.OldRow) || e.Row.POSource == "D" != (e.OldRow.POSource == "D"))
      this.InvalidateExternalTax(((PXSelectBase<SOOrder>) this.Base.Document).Current);
    if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOLine>>) e).Cache.ObjectsEqual<SOLine.openAmt>((object) e.Row, (object) e.OldRow))
      ((PXSelectBase<SOOrder>) this.Base.Document).Current.IsOpenTaxValid = new bool?(false);
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOLine>>) e).Cache.ObjectsEqual<SOLine.unbilledAmt>((object) e.Row, (object) e.OldRow))
      return;
    ((PXSelectBase<SOOrder>) this.Base.Document).Current.IsUnbilledTaxValid = new bool?(false);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<SOLine> e)
  {
    if (((PXSelectBase<SOOrder>) this.Base.Document).Current == null || !this.CalculateTaxesUsingExternalProvider(((PXSelectBase<SOOrder>) this.Base.Document).Current.TaxZoneID))
      return;
    this.InvalidateExternalTax(((PXSelectBase<SOOrder>) this.Base.Document).Current);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<SOOrder>) this.Base.Document).Current, true);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SOShippingAddress> e)
  {
    if (e.Row == null || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOShippingAddress>>) e).Cache.ObjectsEqual<SOShippingAddress.postalCode, SOShippingAddress.countryID, SOShippingAddress.state, SOShippingAddress.latitude, SOShippingAddress.longitude>((object) e.Row, (object) e.OldRow))
      return;
    this.InvalidateExternalTax(((PXSelectBase<SOOrder>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowInserted<SOShippingAddress> e)
  {
    if (e.Row == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<SOOrder>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<SOShippingAddress> e)
  {
    if (e.Row == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<SOOrder>) this.Base.Document).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<SOShippingAddress, SOShippingAddress.overrideAddress> e)
  {
    if (e.Row == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<SOOrder>) this.Base.Document).Current);
  }

  private GetTaxRequest BuildGetTaxRequest<TLineAmt, TLineQty, TDocDiscount, TFreightAmt>(
    SOOrder order,
    string docCode,
    string debugMethodName)
    where TLineAmt : IBqlField
    where TLineQty : IBqlField
    where TDocDiscount : IBqlField
    where TFreightAmt : IBqlField
  {
    Stopwatch stopwatch1 = new Stopwatch();
    stopwatch1.Start();
    if (order == null)
      throw new PXArgumentException(nameof (order));
    PX.Objects.AR.Customer customer = (PX.Objects.AR.Customer) ((PXSelectBase) this.Base.customer).View.SelectSingleBound(new object[1]
    {
      (object) order
    }, Array.Empty<object>());
    PX.Objects.CR.Location location = (PX.Objects.CR.Location) ((PXSelectBase) this.Base.location).View.SelectSingleBound(new object[1]
    {
      (object) order
    }, Array.Empty<object>());
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) ((PXSelectBase) this.Base.taxzone).View.SelectSingleBound(new object[1]
    {
      (object) order
    }, Array.Empty<object>());
    IAddressLocation fromAddress = this.GetFromAddress(order);
    IAddressLocation toAddress = this.GetToAddress(order);
    if (fromAddress == null)
      throw new PXException("The system failed to get the From address from the sales order.");
    if (toAddress == null)
      throw new PXException("The system failed to get the To address from the sales order.");
    GetTaxRequest taxRequest = new GetTaxRequest();
    taxRequest.CompanyCode = this.CompanyCodeFromBranch(order.TaxZoneID, order.BranchID);
    taxRequest.CurrencyCode = order.CuryID;
    taxRequest.CustomerCode = customer.AcctCD;
    taxRequest.BAccountClassID = customer.ClassID;
    taxRequest.TaxRegistrationID = location?.TaxRegistrationID;
    taxRequest.OriginAddress = AddressConverter.ConvertTaxAddress(fromAddress);
    taxRequest.DestinationAddress = AddressConverter.ConvertTaxAddress(toAddress);
    taxRequest.DocCode = docCode;
    taxRequest.DocDate = order.OrderDate.GetValueOrDefault();
    taxRequest.LocationCode = this.GetExternalTaxProviderLocationCode(order);
    taxRequest.APTaxType = taxZone.ExternalAPTaxType;
    taxRequest.TaxCalculationMode = !PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>() || !(order.TaxCalcMode != "T") ? this.GetTaxProviderTaxCalcMode(order.TaxZoneID) : order.TaxCalcMode;
    Sign sign1 = Sign.Plus;
    taxRequest.CustomerUsageType = order.AvalaraCustomerUsageType;
    if (!string.IsNullOrEmpty(order.ExternalTaxExemptionNumber))
      taxRequest.ExemptionNo = order.ExternalTaxExemptionNumber;
    if (((SOOrderType) ((PXSelectBase) this.Base.soordertype).View.SelectSingleBound(new object[1]
    {
      (object) order
    }, Array.Empty<object>())).DefaultOperation == "R")
    {
      taxRequest.DocType = (TaxDocumentType) 5;
      sign1 = Sign.Minus;
    }
    else
      taxRequest.DocType = (TaxDocumentType) 1;
    PXSelectJoin<SOLine, LeftJoin<PX.Objects.IN.InventoryItem, On<SOLine.FK.InventoryItem>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<SOLine.salesAcctID>>>>, Where<SOLine.orderType, Equal<Current<SOOrder.orderType>>, And<SOLine.orderNbr, Equal<Current<SOOrder.orderNbr>>>>, OrderBy<Asc<SOLine.orderType, Asc<SOLine.orderNbr, Asc<SOLine.lineNbr>>>>> pxSelectJoin = new PXSelectJoin<SOLine, LeftJoin<PX.Objects.IN.InventoryItem, On<SOLine.FK.InventoryItem>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<SOLine.salesAcctID>>>>, Where<SOLine.orderType, Equal<Current<SOOrder.orderType>>, And<SOLine.orderNbr, Equal<Current<SOOrder.orderNbr>>>>, OrderBy<Asc<SOLine.orderType, Asc<SOLine.orderNbr, Asc<SOLine.lineNbr>>>>>((PXGraph) this.Base);
    PXCache cach1 = ((PXGraph) this.Base).Caches[typeof (SOOrder)];
    taxRequest.Discount = Sign.op_Multiply(sign1, (cach1.GetValue<TDocDiscount>((object) order) as Decimal?).GetValueOrDefault());
    Stopwatch stopwatch2 = new Stopwatch();
    stopwatch2.Start();
    Decimal valueOrDefault1 = (cach1.GetValue<TFreightAmt>((object) order) as Decimal?).GetValueOrDefault();
    if (valueOrDefault1 != 0M)
      taxRequest.CartItems.Add(new TaxCartItem()
      {
        Index = (int) short.MinValue,
        Quantity = 1M,
        UOM = "EA",
        UnitPrice = 0M,
        Amount = Sign.op_Multiply(sign1, valueOrDefault1),
        Description = PXMessages.LocalizeNoPrefix("Freight"),
        DestinationAddress = taxRequest.DestinationAddress,
        OriginAddress = taxRequest.OriginAddress,
        ItemCode = "N/A",
        Discounted = new bool?(false),
        TaxCode = order.FreightTaxCategoryID
      });
    PXCache cach2 = ((PXGraph) this.Base).Caches[typeof (SOLine)];
    PXView view = ((PXSelectBase) pxSelectJoin).View;
    object[] objArray1 = new object[1]{ (object) order };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<SOLine, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account> pxResult in view.SelectMultiBound(objArray1, objArray2))
    {
      SOLine line = PXResult<SOLine, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<SOLine, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PXResult<SOLine, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      Decimal? nullable1;
      int num1;
      if (taxRequest.Discount != 0M)
      {
        nullable1 = line.DocumentDiscountRate;
        if (!((nullable1 ?? 1M) != 1M))
        {
          nullable1 = line.GroupDiscountRate;
          num1 = (nullable1 ?? 1M) != 1M ? 1 : 0;
        }
        else
          num1 = 1;
      }
      else
        num1 = 0;
      bool flag = num1 != 0;
      TaxCartItem taxCartItem1 = new TaxCartItem();
      taxCartItem1.Index = line.LineNbr.GetValueOrDefault();
      nullable1 = cach2.GetValue<TLineAmt>((object) line) as Decimal?;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      nullable1 = cach2.GetValue<TLineQty>((object) line) as Decimal?;
      Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
      TaxCartItem taxCartItem2 = taxCartItem1;
      Sign sign2 = sign1;
      nullable1 = line.CuryUnitPrice;
      Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
      Decimal num2 = Sign.op_Multiply(sign2, valueOrDefault4);
      taxCartItem2.UnitPrice = num2;
      taxCartItem1.Amount = Sign.op_Multiply(sign1, valueOrDefault2);
      taxCartItem1.Description = line.TranDesc;
      taxCartItem1.DestinationAddress = AddressConverter.ConvertTaxAddress(this.GetToAddress(order, line));
      taxCartItem1.OriginAddress = AddressConverter.ConvertTaxAddress(this.GetFromAddress(order, line));
      taxCartItem1.ItemCode = inventoryItem.InventoryCD;
      taxCartItem1.Quantity = Math.Abs(valueOrDefault3);
      taxCartItem1.UOM = line.UOM;
      taxCartItem1.Discounted = new bool?(flag);
      taxCartItem1.RevAcct = account.AccountCD;
      taxCartItem1.TaxCode = line.TaxCategoryID;
      taxCartItem1.CustomerUsageType = line.AvalaraCustomerUsageType;
      if (!string.IsNullOrEmpty(inventoryItem.HSTariffCode))
        taxCartItem1.CommodityCode = new CommodityCode(inventoryItem.CommodityCodeType, inventoryItem.HSTariffCode);
      if (line.Operation == "R")
      {
        DateTime? invoiceDate = line.InvoiceDate;
        if (invoiceDate.HasValue)
        {
          taxCartItem1.TaxOverride.Reason = "Return";
          TaxOverride taxOverride = taxCartItem1.TaxOverride;
          invoiceDate = line.InvoiceDate;
          DateTime? nullable2 = new DateTime?(invoiceDate.Value);
          taxOverride.TaxDate = nullable2;
          taxCartItem1.TaxOverride.TaxOverrideType = new TaxOverrideType?((TaxOverrideType) 3);
        }
      }
      taxRequest.CartItems.Add(taxCartItem1);
    }
    stopwatch2.Stop();
    stopwatch1.Stop();
    return taxRequest;
  }

  protected virtual GetTaxRequest BuildGetTaxRequest(SOOrder order)
  {
    return this.BuildGetTaxRequest<SOLine.curyLineAmt, SOLine.orderQty, SOOrder.curyDiscTot, SOOrder.curyFreightTot>(order, $"SO.{order.OrderType}.{order.OrderNbr}", nameof (BuildGetTaxRequest));
  }

  protected virtual GetTaxRequest BuildGetTaxRequestOpen(SOOrder order)
  {
    return this.BuildGetTaxRequest<SOLine.curyOpenAmt, SOLine.openQty, SOOrder.curyOpenDiscTotal, SOOrder.curyFreightTot>(order, $"SO.{order.OrderType}.{order.OrderNbr}", nameof (BuildGetTaxRequestOpen));
  }

  protected virtual GetTaxRequest BuildGetTaxRequestUnbilled(SOOrder order)
  {
    return this.BuildGetTaxRequest<SOLine.curyUnbilledAmt, SOLine.unbilledQty, SOOrder.curyUnbilledDiscTotal, SOOrder.curyUnbilledFreightTot>(order, $"{order.OrderType}.{order.OrderNbr}.Open", nameof (BuildGetTaxRequestUnbilled));
  }

  protected virtual void ApplyTax(
    SOOrder order,
    GetTaxResult result,
    GetTaxResult resultOpen,
    GetTaxResult resultUnbilled)
  {
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) ((PXSelectBase) this.Base.taxzone).View.SelectSingleBound(new object[1]
    {
      (object) order
    }, Array.Empty<object>());
    PX.Objects.AP.Vendor taxAgency = this.GetTaxAgency(this.Base, taxZone);
    Sign sign = ((SOOrderType) ((PXSelectBase) this.Base.soordertype).View.SelectSingleBound(new object[1]
    {
      (object) order
    }, Array.Empty<object>())).DefaultOperation == "R" ? Sign.Minus : Sign.Plus;
    PXSelectBase<SOTaxTran> pxSelectBase = (PXSelectBase<SOTaxTran>) new PXSelectJoin<SOTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<SOTaxTran.taxID>>>, Where<SOTaxTran.orderType, Equal<Current<SOOrder.orderType>>, And<SOTaxTran.orderNbr, Equal<Current<SOOrder.orderNbr>>>>>((PXGraph) this.Base);
    if (result != null)
    {
      this.ClearExistingTaxes(order);
      ((PXGraph) this.Base).Views.Caches.Add(typeof (PX.Objects.TX.Tax));
      bool valueOrDefault = ((PXSelectBase<SOOrderType>) this.Base.soordertype).Current.RequireControlTotal.GetValueOrDefault();
      if (!order.Hold.GetValueOrDefault())
        ((PXSelectBase<SOOrderType>) this.Base.soordertype).Current.RequireControlTotal = new bool?(false);
      this.GetFreightTaxDetails(resultUnbilled);
      TaxDetail[] freightTaxDetails = this.GetFreightTaxDetails(resultOpen);
      TaxCalc taxCalc1 = TaxBaseAttribute.GetTaxCalc<SOLine.taxCategoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null);
      TaxCalc taxCalc2 = TaxBaseAttribute.GetTaxCalc<SOOrder.freightTaxCategoryID>(((PXSelectBase) this.Base.Document).Cache, (object) null);
      try
      {
        TaxBaseAttribute.SetTaxCalc<SOLine.taxCategoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
        TaxBaseAttribute.SetTaxCalc<SOOrder.freightTaxCategoryID>(((PXSelectBase) this.Base.Document).Cache, (object) null, TaxCalc.ManualCalc);
        foreach (TaxDetail taxDetail1 in result.TaxSummary)
        {
          string taxUniqueId = this.GetTaxUniqueID(taxDetail1);
          TaxDetail detailWithoutFreight = this.GetTaxDetailWithoutFreight(taxUniqueId, resultOpen?.TaxSummary, freightTaxDetails, taxDetail1);
          TaxDetail taxDetail2 = this.GetTaxDetail(taxUniqueId, resultUnbilled?.TaxSummary, taxDetail1);
          taxDetail1.TaxType = "S";
          PX.Objects.TX.Tax tax = this.CreateTax(this.Base, taxZone, taxAgency, taxDetail1);
          if (tax != null)
          {
            SOTaxTran soTaxTran = new SOTaxTran();
            soTaxTran.OrderType = order.OrderType;
            soTaxTran.OrderNbr = order.OrderNbr;
            soTaxTran.TaxZoneID = order.TaxZoneID;
            soTaxTran.TaxID = tax?.TaxID;
            soTaxTran.CuryTaxAmt = new Decimal?(Sign.op_Multiply(sign, taxDetail1.TaxAmount));
            soTaxTran.CuryTaxableAmt = new Decimal?(Sign.op_Multiply(sign, taxDetail1.TaxableAmount));
            soTaxTran.CuryUnshippedTaxAmt = new Decimal?(Sign.op_Multiply(sign, detailWithoutFreight.TaxAmount));
            soTaxTran.CuryUnshippedTaxableAmt = new Decimal?(Sign.op_Multiply(sign, detailWithoutFreight.TaxableAmount));
            soTaxTran.CuryUnbilledTaxAmt = new Decimal?(Sign.op_Multiply(sign, taxDetail2.TaxAmount));
            soTaxTran.CuryUnbilledTaxableAmt = new Decimal?(Sign.op_Multiply(sign, taxDetail2.TaxableAmount));
            soTaxTran.TaxRate = new Decimal?(Convert.ToDecimal(taxDetail1.Rate) * 100M);
            soTaxTran.JurisType = taxDetail1.JurisType;
            soTaxTran.JurisName = taxDetail1.JurisName;
            soTaxTran.IsTaxInclusive = new bool?(taxDetail1.TaxCalculationLevel == 1);
            ((PXSelectBase<SOTaxTran>) this.Base.Taxes).Insert(soTaxTran);
          }
        }
      }
      finally
      {
        ((PXSelectBase<SOOrderType>) this.Base.soordertype).Current.RequireControlTotal = new bool?(valueOrDefault);
        TaxBaseAttribute.SetTaxCalc<SOLine.taxCategoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, taxCalc1);
        TaxBaseAttribute.SetTaxCalc<SOOrder.freightTaxCategoryID>(((PXSelectBase) this.Base.Document).Cache, (object) null, taxCalc2);
      }
    }
    if (result == null)
    {
      TaxDetail[] freightTaxDetails1 = this.GetFreightTaxDetails(resultUnbilled);
      TaxDetail[] freightTaxDetails2 = this.GetFreightTaxDetails(resultOpen);
      PXView view = ((PXSelectBase) pxSelectBase).View;
      object[] objArray1 = new object[1]{ (object) order };
      object[] objArray2 = Array.Empty<object>();
      foreach (PXResult<SOTaxTran, PX.Objects.TX.Tax> pxResult in view.SelectMultiBound(objArray1, objArray2))
      {
        SOTaxTran taxDetail3 = PXResult<SOTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult);
        PX.Objects.TX.Tax tax = PXResult<SOTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult);
        string taxUniqueId = this.GetTaxUniqueID(taxDetail3);
        TaxDetail taxDetail4 = this.GetTaxDetail(taxUniqueId, resultUnbilled?.TaxSummary);
        bool flag = false;
        if (taxDetail4 != null)
        {
          taxDetail3.CuryUnbilledTaxAmt = new Decimal?(Sign.op_Multiply(sign, taxDetail4.TaxAmount));
          taxDetail3.CuryUnbilledTaxableAmt = new Decimal?(Sign.op_Multiply(sign, taxDetail4.TaxableAmount));
          flag = true;
        }
        else if (resultUnbilled != null && resultUnbilled.TotalTaxAmount == 0M && freightTaxDetails1.Length == 0 && tax.IsExternal.GetValueOrDefault())
        {
          taxDetail3.CuryUnbilledTaxAmt = new Decimal?(0M);
          taxDetail3.CuryUnbilledTaxableAmt = new Decimal?(0M);
          flag = true;
        }
        TaxDetail detailWithoutFreight = this.GetTaxDetailWithoutFreight(taxUniqueId, resultOpen?.TaxSummary, freightTaxDetails2);
        if (detailWithoutFreight != null)
        {
          taxDetail3.CuryUnshippedTaxAmt = new Decimal?(Sign.op_Multiply(sign, detailWithoutFreight.TaxAmount));
          taxDetail3.CuryUnshippedTaxableAmt = new Decimal?(Sign.op_Multiply(sign, detailWithoutFreight.TaxableAmount));
          flag = true;
        }
        if (flag)
          ((PXSelectBase<SOTaxTran>) this.Base.Taxes).Update(taxDetail3);
      }
    }
    if (resultUnbilled != null)
    {
      TaxLine[] taxLines = resultUnbilled.TaxLines;
      (taxLines != null ? ((IEnumerable<TaxLine>) taxLines).FirstOrDefault<TaxLine>((Func<TaxLine, bool>) (taxLine => taxLine.Index == (int) short.MinValue))?.TaxAmount : new Decimal?()).GetValueOrDefault();
      ((PXSelectBase<SOOrder>) this.Base.Document).SetValueExt<SOOrder.curyUnbilledTaxTotal>(order, (object) Sign.op_Multiply(sign, resultUnbilled.TotalTaxAmount));
    }
    if (resultOpen != null)
    {
      TaxLine[] taxLines = resultOpen.TaxLines;
      Decimal valueOrDefault = (taxLines != null ? ((IEnumerable<TaxLine>) taxLines).FirstOrDefault<TaxLine>((Func<TaxLine, bool>) (taxLine => taxLine.Index == (int) short.MinValue))?.TaxAmount : new Decimal?()).GetValueOrDefault();
      ((PXSelectBase<SOOrder>) this.Base.Document).SetValueExt<SOOrder.curyOpenTaxTotal>(order, (object) Sign.op_Multiply(sign, resultOpen.TotalTaxAmount - valueOrDefault));
    }
    order.IsTaxValid = new bool?(true);
    order.IsOpenTaxValid = new bool?(true);
    order.IsUnbilledTaxValid = new bool?(true);
    if (order.DisableAutomaticTaxCalculation.GetValueOrDefault())
      order.IsManualTaxesValid = new bool?(true);
    order = ((PXSelectBase<SOOrder>) this.Base.Document).Update(order);
    this.SkipTaxCalcAndSave();
  }

  public virtual TaxDetail[] GetFreightTaxDetails(GetTaxResult result)
  {
    TaxLine taxLine1;
    if (result == null)
    {
      taxLine1 = (TaxLine) null;
    }
    else
    {
      TaxLine[] taxLines = result.TaxLines;
      taxLine1 = taxLines != null ? ((IEnumerable<TaxLine>) taxLines).FirstOrDefault<TaxLine>((Func<TaxLine, bool>) (taxLine => taxLine.Index == (int) short.MinValue)) : (TaxLine) null;
    }
    return taxLine1?.TaxDetails ?? new TaxDetail[0];
  }

  public virtual TaxDetail GetTaxDetailWithoutFreight(
    string taxUniqueID,
    TaxDetail[] summaryDetails,
    TaxDetail[] freightParts,
    TaxDetail fallbackSummaryDetail = null)
  {
    // ISSUE: unable to decompile the method.
  }

  public virtual TaxDetail GetTaxDetail(
    string taxUniqueID,
    TaxDetail[] summaryDetails,
    TaxDetail fallbackSummaryDetail = null)
  {
    TaxDetail summaryDetail = (summaryDetails != null ? ((IEnumerable<TaxDetail>) summaryDetails).FirstOrDefault<TaxDetail>((Func<TaxDetail, bool>) (d => this.GetTaxUniqueID(d) == taxUniqueID)) : (TaxDetail) null) ?? fallbackSummaryDetail;
    return summaryDetail == null ? summaryDetail : SOOrderEntryExternalTax.CreateTaxDetail(summaryDetail);
  }

  private static TaxDetail CreateTaxDetail(TaxDetail summaryDetail)
  {
    return new TaxDetail()
    {
      TaxName = summaryDetail.TaxName,
      JurisName = summaryDetail.JurisName,
      JurisType = summaryDetail.JurisType,
      JurisCode = summaryDetail.JurisCode,
      TaxAmount = summaryDetail.TaxAmount,
      TaxableAmount = summaryDetail.TaxableAmount,
      Rate = summaryDetail.Rate,
      TaxCalculationLevel = summaryDetail.TaxCalculationLevel
    };
  }

  public virtual string GetSOTaxID(TaxDetail taxDetail)
  {
    return string.IsNullOrEmpty(taxDetail.TaxName) ? taxDetail.JurisCode : taxDetail.TaxName;
  }

  public virtual string GetTaxUniqueID(TaxDetail taxDetail)
  {
    return string.Join("-", this.GetSOTaxID(taxDetail), taxDetail.JurisType ?? "", taxDetail.JurisName ?? "", taxDetail.Rate.ToString("F6", (IFormatProvider) CultureInfo.InvariantCulture) ?? "");
  }

  public virtual string GetTaxUniqueID(SOTaxTran taxDetail)
  {
    Decimal num = taxDetail.TaxRate.GetValueOrDefault() / 100M;
    return string.Join("-", taxDetail.TaxID, taxDetail.JurisType ?? "", taxDetail.JurisName ?? "", num.ToString("F6", (IFormatProvider) CultureInfo.InvariantCulture) ?? "");
  }

  /// <summary>//Clears all existing Tax transactions</summary>
  /// <param name="order"></param>
  protected virtual void ClearExistingTaxes(SOOrder order)
  {
    if (order.ExternalTaxesImportInProgress.GetValueOrDefault() && ((PXGraph) this.Base).IsContractBasedAPI)
    {
      foreach (SOTaxTran soTaxTran in ((PXSelectBase) new PXSelect<SOTaxTran, Where<SOTaxTran.orderType, Equal<Current<SOOrder.orderType>>, And<SOTaxTran.orderNbr, Equal<Current<SOOrder.orderNbr>>>>>((PXGraph) this.Base)).View.SelectMultiBound(new object[1]
      {
        (object) order
      }, Array.Empty<object>()))
        ((PXSelectBase<SOTaxTran>) this.Base.Taxes).Delete(soTaxTran);
    }
    else
    {
      foreach (PXResult<SOTaxTran, PX.Objects.TX.Tax> pxResult in ((PXSelectBase) new PXSelectJoin<SOTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<SOTaxTran.taxID>>>, Where<SOTaxTran.orderType, Equal<Current<SOOrder.orderType>>, And<SOTaxTran.orderNbr, Equal<Current<SOOrder.orderNbr>>>>>((PXGraph) this.Base)).View.SelectMultiBound(new object[1]
      {
        (object) order
      }, Array.Empty<object>()))
        ((PXSelectBase<SOTaxTran>) this.Base.Taxes).Delete(PXResult<SOTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult));
    }
  }

  protected virtual bool IsSame(GetTaxRequest x, GetTaxRequest y)
  {
    if (x.CartItems.Count != y.CartItems.Count)
      return false;
    for (int index = 0; index < x.CartItems.Count; ++index)
    {
      if (x.CartItems[index].Amount != y.CartItems[index].Amount)
        return false;
    }
    return true;
  }

  protected override string GetExternalTaxProviderLocationCode(SOOrder order)
  {
    return this.GetExternalTaxProviderLocationCode<SOLine, KeysRelation<CompositeKey<Field<SOLine.orderType>.IsRelatedTo<SOOrder.orderType>, Field<SOLine.orderNbr>.IsRelatedTo<SOOrder.orderNbr>>.WithTablesOf<SOOrder, SOLine>, SOOrder, SOLine>.SameAsCurrent, SOLine.siteID>(order);
  }

  protected virtual IAddressLocation GetFromAddress(SOOrder order)
  {
    return GraphHelper.RowCast<PX.Objects.CR.Address>((IEnumerable) PXSelectBase<PX.Objects.GL.Branch, PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) order.BranchID
    })).FirstOrDefault<PX.Objects.CR.Address>().With<PX.Objects.CR.Address, IAddressLocation>((Func<PX.Objects.CR.Address, IAddressLocation>) (e => this.ValidAddressFrom<BAccountR.defAddressID>((IAddressLocation) e, order)));
  }

  public virtual IAddressLocation GetFromAddress(SOOrder order, SOLine line)
  {
    return GraphHelper.RowCast<PX.Objects.CR.Address>((IEnumerable) PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.FK.Address>>, Where<PX.Objects.IN.INSite.siteID, Equal<Current<SOLine.siteID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new SOLine[1]
    {
      line
    }, Array.Empty<object>())).FirstOrDefault<PX.Objects.CR.Address>().With<PX.Objects.CR.Address, IAddressLocation>((Func<PX.Objects.CR.Address, IAddressLocation>) (e => this.ValidAddressFrom<PX.Objects.IN.INSite.addressID>((IAddressLocation) e, order))) ?? this.GetFromAddress(order);
  }

  protected virtual IAddressLocation GetToAddress(SOOrder order)
  {
    if (order.WillCall.GetValueOrDefault())
      return this.GetFromAddress(order);
    return PXResultset<SOShippingAddress>.op_Implicit(PXSelectBase<SOShippingAddress, PXSelect<SOShippingAddress, Where<SOShippingAddress.addressID, Equal<Required<SOOrder.shipAddressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) order.ShipAddressID
    })).With<SOShippingAddress, IAddressLocation>((Func<SOShippingAddress, IAddressLocation>) (e => this.ValidAddressFrom<SOOrder.shipAddressID>((IAddressLocation) e, order)));
  }

  public virtual IAddressLocation GetToAddress(SOOrder order, SOLine line)
  {
    if (order.WillCall.GetValueOrDefault() && (!line.POCreate.GetValueOrDefault() || !(line.POSource == "D")))
      return this.GetFromAddress(order, line);
    return PXResultset<SOShippingAddress>.op_Implicit(PXSelectBase<SOShippingAddress, PXSelect<SOShippingAddress, Where<SOShippingAddress.addressID, Equal<Required<SOOrder.shipAddressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) order.ShipAddressID
    })).With<SOShippingAddress, IAddressLocation>((Func<SOShippingAddress, IAddressLocation>) (e => this.ValidAddressFrom<SOOrder.shipAddressID>((IAddressLocation) e, order)));
  }

  private IAddressLocation ValidAddressFrom<TFieldSource>(IAddressLocation address, SOOrder order) where TFieldSource : IBqlField
  {
    return !ExternalTaxBase<SOOrderEntry>.IsEmptyAddress(address) ? address : throw new PXException(this.PickAddressError<TFieldSource>((IAddressBase) address, order));
  }

  private string PickAddressError<TFieldSource>(IAddressBase address, SOOrder order) where TFieldSource : IBqlField
  {
    if (typeof (TFieldSource) == typeof (SOOrder.shipAddressID))
      return PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
      {
        (object) EntityHelper.GetFriendlyEntityName<SOOrder>(),
        (object) new EntityHelper((PXGraph) this.Base).GetRowID<SOOrder>(order, ", ")
      });
    if (typeof (TFieldSource) == typeof (PX.Objects.AP.Vendor.defLocationID))
      return ((PXResult) ((IQueryable<PXResult<PX.Objects.AP.Vendor>>) PXSelectBase<PX.Objects.AP.Vendor, PXSelectReadonly2<PX.Objects.AP.Vendor, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<PX.Objects.AP.Vendor.defLocationID>>>, Where<PX.Objects.CR.Location.defAddressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
      {
        (object) ((PX.Objects.CR.Address) address).AddressID
      })).First<PXResult<PX.Objects.AP.Vendor>>()).GetItem<PX.Objects.AP.Vendor>().With<PX.Objects.AP.Vendor, string>((Func<PX.Objects.AP.Vendor, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
      {
        (object) EntityHelper.GetFriendlyEntityName<PX.Objects.AP.Vendor>(),
        (object) new EntityHelper((PXGraph) this.Base).GetRowID<PX.Objects.AP.Vendor>(e, ", ")
      })));
    if (typeof (TFieldSource) == typeof (PX.Objects.IN.INSite.addressID))
      return ((PXResult) ((IQueryable<PXResult<PX.Objects.IN.INSite>>) PXSelectBase<PX.Objects.IN.INSite, PXSelectReadonly<PX.Objects.IN.INSite, Where<PX.Objects.IN.INSite.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
      {
        (object) ((PX.Objects.CR.Address) address).AddressID
      })).First<PXResult<PX.Objects.IN.INSite>>()).GetItem<PX.Objects.IN.INSite>().With<PX.Objects.IN.INSite, string>((Func<PX.Objects.IN.INSite, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
      {
        (object) EntityHelper.GetFriendlyEntityName<PX.Objects.IN.INSite>(),
        (object) new EntityHelper((PXGraph) this.Base).GetRowID<PX.Objects.IN.INSite>(e, ", ")
      })));
    if (!(typeof (TFieldSource) == typeof (BAccountR.defAddressID)))
      throw new ArgumentOutOfRangeException("Unknown address source used");
    return ((PXResult) ((IQueryable<PXResult<BAccountR>>) PXSelectBase<BAccountR, PXSelectReadonly<BAccountR, Where<BAccountR.defAddressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
    {
      (object) ((PX.Objects.CR.Address) address).AddressID
    })).First<PXResult<BAccountR>>()).GetItem<BAccountR>().With<BAccountR, string>((Func<BAccountR, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
    {
      (object) EntityHelper.GetFriendlyEntityName<BAccountR>(),
      (object) new EntityHelper((PXGraph) this.Base).GetRowID<BAccountR>(e, ", ")
    })));
  }

  protected virtual bool IsCommonCarrier(string carrierID)
  {
    if (string.IsNullOrEmpty(carrierID))
      return false;
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this.Base, carrierID);
    return carrier != null && carrier.IsCommonCarrier.GetValueOrDefault();
  }

  public void InvalidateExternalTax(SOOrder order)
  {
    if (order == null || !this.CalculateTaxesUsingExternalProvider(order.TaxZoneID))
      return;
    order.IsTaxValid = new bool?(false);
    order.IsOpenTaxValid = new bool?(false);
    order.IsUnbilledTaxValid = new bool?(false);
  }
}

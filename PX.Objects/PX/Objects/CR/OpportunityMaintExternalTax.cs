// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OpportunityMaintExternalTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.TX;
using PX.TaxProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR;

public class OpportunityMaintExternalTax : ExternalTaxBase<OpportunityMaint, CROpportunity>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();

  [PXOverride]
  public void Persist()
  {
    if (((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current == null || !this.IsExternalTax(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current.TaxZoneID) || this.skipExternalTaxCalcOnSave || ((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current.IsTaxValid.GetValueOrDefault())
      return;
    if (!PXLongOperation.IsLongOperationContext())
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      OpportunityMaintExternalTax.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10 = new OpportunityMaintExternalTax.\u003C\u003Ec__DisplayClass1_0()
      {
        graph = this.Base.CloneGraphState<OpportunityMaint>()
      };
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass10.ext = ((PXGraph) cDisplayClass10.graph).GetProcessingExtension<OpportunityMaintExternalTax>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass10, __methodptr(\u003CPersist\u003Eb__0)));
    }
    else
      this.CalculateExternalTax(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current);
  }

  public override void SkipTaxCalcAndSave()
  {
    try
    {
      this.skipExternalTaxCalcOnSave = true;
      ((PXAction) this.Base.Save).Press();
    }
    finally
    {
      this.skipExternalTaxCalcOnSave = false;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<CROpportunity> e)
  {
    if (e.Row == null || !this.IsExternalTax(e.Row.TaxZoneID) || e.Row.IsTaxValid.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetWarning<CROpportunity.curyTaxTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CROpportunity>>) e).Cache, (object) e.Row, "Tax is not up-to-date.");
  }

  protected void _(PX.Data.Events.RowUpdated<CROpportunity> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<CROpportunity>>) e).Cache.ObjectsEqual<CROpportunity.taxCalcMode>((object) e.Row, (object) e.OldRow))
      return;
    this.InvalidateExternalTax(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current);
  }

  protected virtual void _(PX.Data.Events.RowInserted<CROpportunityProducts> e)
  {
    this.InvalidateExternalTax(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<CROpportunityProducts> e)
  {
    this.InvalidateExternalTax(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CROpportunityProducts> e)
  {
    this.InvalidateExternalTax(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CROpportunityDiscountDetail> e)
  {
    this.InvalidateExternalTax(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<CROpportunityDiscountDetail> e)
  {
    this.InvalidateExternalTax(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current);
  }

  protected virtual void _(PX.Data.Events.RowInserted<CROpportunityDiscountDetail> e)
  {
    this.InvalidateExternalTax(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CRShippingAddress> e)
  {
    if (e.Row == null || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<CRShippingAddress>>) e).Cache.ObjectsEqual<CRShippingAddress.postalCode, CRShippingAddress.countryID, CRShippingAddress.state, CRShippingAddress.latitude, CRShippingAddress.longitude>((object) e.Row, (object) e.OldRow))
      return;
    this.InvalidateExternalTax(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current);
  }

  protected virtual void _(PX.Data.Events.RowInserted<CRShippingAddress> e)
  {
    if (e.Row == null || ((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<CRShippingAddress> e)
  {
    if (e.Row == null || ((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CRShippingAddress.overrideAddress> e)
  {
    if (e.Row == null || ((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current);
  }

  public virtual void InvalidateExternalTax(CROpportunity doc)
  {
    if (!this.IsExternalTax(doc.TaxZoneID))
      return;
    doc.IsTaxValid = new bool?(false);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Opportunity).Cache, (object) doc);
  }

  public override CROpportunity CalculateExternalTax(CROpportunity order)
  {
    if (!order.BAccountID.HasValue || this.IsNonTaxable((IAddressBase) this.GetToAddress(order)))
    {
      this.ApplyTax(order, GetTaxResult.Empty);
      order.IsTaxValid = new bool?(true);
      order = ((PXSelectBase<CROpportunity>) this.Base.Opportunity).Update(order);
      this.SkipTaxCalcAndSave();
      return order;
    }
    ITaxProvider itaxProvider = ExternalTaxBase<OpportunityMaint>.TaxProviderFactory((PXGraph) this.Base, order.TaxZoneID);
    GetTaxRequest getTaxRequest = (GetTaxRequest) null;
    bool flag = true;
    if (!order.IsTaxValid.GetValueOrDefault())
    {
      getTaxRequest = this.BuildGetTaxRequest(order);
      if (getTaxRequest.CartItems.Count > 0)
        flag = false;
      else
        getTaxRequest = (GetTaxRequest) null;
    }
    if (flag)
    {
      order.IsTaxValid = new bool?(true);
      order = ((PXSelectBase<CROpportunity>) this.Base.Opportunity).Update(order);
      this.SkipTaxCalcAndSave();
      return order;
    }
    GetTaxResult tax = itaxProvider.GetTax(getTaxRequest);
    if (((ResultBase) tax).IsSuccess)
    {
      try
      {
        this.ApplyTax(order, tax);
        order.IsTaxValid = new bool?(true);
        order = ((PXSelectBase<CROpportunity>) this.Base.Opportunity).Update(order);
        this.SkipTaxCalcAndSave();
      }
      catch (PXOuterException ex)
      {
        string str = "The tax amount calculated by the external tax provider cannot be applied to the document.";
        foreach (string innerMessage in ex.InnerMessages)
          str = str + Environment.NewLine + innerMessage;
        throw new PXException((Exception) ex, str, Array.Empty<object>());
      }
      catch (Exception ex)
      {
        object[] objArray = Array.Empty<object>();
        throw new PXException(ex, "The tax amount calculated by the external tax provider cannot be applied to the document.", objArray);
      }
      return order;
    }
    this.LogMessages((ResultBase) tax);
    throw new PXException("Failed to get taxes from the external tax provider. Check Trace Log for details.");
  }

  protected virtual GetTaxRequest BuildGetTaxRequest(CROpportunity order)
  {
    if (order == null)
      throw new PXArgumentException(nameof (order));
    BAccount baccount = PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) order.BAccountID
    }));
    Location location = PXResultset<Location>.op_Implicit(PXSelectBase<Location, PXSelect<Location, Where<Location.bAccountID, Equal<Required<Location.bAccountID>>, And<Location.locationID, Equal<Required<Location.locationID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) order.BAccountID,
      (object) order.LocationID
    }));
    PX.Objects.TX.TaxZone taxZone = PXResultset<PX.Objects.TX.TaxZone>.op_Implicit(PXSelectBase<PX.Objects.TX.TaxZone, PXSelectReadonly<PX.Objects.TX.TaxZone, Where<PX.Objects.TX.TaxZone.taxZoneID, Equal<Required<CROpportunity.taxZoneID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) order.TaxZoneID
    }));
    IAddressLocation fromAddress = this.GetFromAddress(order);
    IAddressLocation toAddress = this.GetToAddress(order);
    if (fromAddress == null)
      throw new PXException("The system cannot obtain the From address from the document.");
    if (toAddress == null)
      throw new PXException("The system cannot obtain the To address from the document.");
    int num1 = 1;
    GetTaxRequest taxRequest = new GetTaxRequest();
    taxRequest.CompanyCode = this.CompanyCodeFromBranch(order.TaxZoneID, ((PXGraph) this.Base).Accessinfo.BranchID);
    taxRequest.CurrencyCode = order.CuryID;
    taxRequest.CustomerCode = baccount?.AcctCD;
    taxRequest.BAccountClassID = baccount?.ClassID;
    taxRequest.TaxRegistrationID = location?.TaxRegistrationID;
    taxRequest.OriginAddress = AddressConverter.ConvertTaxAddress(fromAddress);
    taxRequest.DestinationAddress = AddressConverter.ConvertTaxAddress(toAddress);
    taxRequest.DocCode = "CR." + order.OpportunityID;
    taxRequest.DocDate = order.CloseDate.GetValueOrDefault();
    taxRequest.Discount = (Decimal) num1 * order.CuryLineDocDiscountTotal.GetValueOrDefault();
    taxRequest.APTaxType = taxZone.ExternalAPTaxType;
    taxRequest.CustomerUsageType = order.AvalaraCustomerUsageType;
    taxRequest.ExemptionNo = order.ExternalTaxExemptionNumber;
    taxRequest.DocType = (TaxDocumentType) 1;
    taxRequest.TaxCalculationMode = !PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>() || !(order.TaxCalcMode != "T") ? this.GetTaxProviderTaxCalcMode(order.TaxZoneID) : order.TaxCalcMode;
    PXView view = ((PXSelectBase) new PXSelectJoin<CROpportunityProducts, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<CROpportunityProducts.inventoryID>>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<PX.Objects.IN.InventoryItem.salesAcctID>>>>, Where<CROpportunityProducts.quoteID, Equal<Current<CROpportunity.quoteNoteID>>>, OrderBy<Asc<CROpportunityProducts.lineNbr>>>((PXGraph) this.Base)).View;
    object[] objArray1 = new object[1]{ (object) order };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<CROpportunityProducts, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account> pxResult in view.SelectMultiBound(objArray1, objArray2))
    {
      CROpportunityProducts line = PXResult<CROpportunityProducts, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<CROpportunityProducts, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PXResult<CROpportunityProducts, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      Decimal? nullable;
      int num2;
      if (taxRequest.Discount != 0M)
      {
        nullable = line.DocumentDiscountRate;
        if (!((nullable ?? 1M) != 1M))
        {
          nullable = line.GroupDiscountRate;
          num2 = (nullable ?? 1M) != 1M ? 1 : 0;
        }
        else
          num2 = 1;
      }
      else
        num2 = 0;
      bool flag = num2 != 0;
      TaxCartItem taxCartItem1 = new TaxCartItem();
      taxCartItem1.Index = line.LineNbr.GetValueOrDefault();
      TaxCartItem taxCartItem2 = taxCartItem1;
      Decimal num3 = (Decimal) num1;
      nullable = line.CuryUnitPrice;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      Decimal num4 = num3 * valueOrDefault1;
      taxCartItem2.UnitPrice = num4;
      TaxCartItem taxCartItem3 = taxCartItem1;
      Decimal num5 = (Decimal) num1;
      nullable = line.CuryAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num6 = num5 * valueOrDefault2;
      taxCartItem3.Amount = num6;
      taxCartItem1.Description = line.Descr;
      taxCartItem1.DestinationAddress = AddressConverter.ConvertTaxAddress(this.GetToAddress(order, line));
      taxCartItem1.OriginAddress = AddressConverter.ConvertTaxAddress(this.GetFromAddress(order, line));
      taxCartItem1.ItemCode = inventoryItem.InventoryCD;
      TaxCartItem taxCartItem4 = taxCartItem1;
      nullable = line.Qty;
      Decimal num7 = Math.Abs(nullable.GetValueOrDefault());
      taxCartItem4.Quantity = num7;
      taxCartItem1.UOM = line.UOM;
      taxCartItem1.Discounted = new bool?(flag);
      taxCartItem1.RevAcct = account.AccountCD;
      taxCartItem1.TaxCode = line.TaxCategoryID;
      if (!string.IsNullOrEmpty(inventoryItem.HSTariffCode))
        taxCartItem1.CommodityCode = new CommodityCode(inventoryItem.CommodityCodeType, inventoryItem.HSTariffCode);
      taxRequest.CartItems.Add(taxCartItem1);
    }
    return taxRequest;
  }

  protected virtual void CalcCuryProductsAmount(
    CROpportunity order,
    ref Decimal? curyProductsAmount)
  {
  }

  protected virtual void ApplyTax(CROpportunity order, GetTaxResult result)
  {
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) null;
    PX.Objects.AP.Vendor taxAgency = (PX.Objects.AP.Vendor) null;
    if (result.TaxSummary.Length != 0)
    {
      taxZone = PXResultset<PX.Objects.TX.TaxZone>.op_Implicit(PXSelectBase<PX.Objects.TX.TaxZone, PXSelectReadonly<PX.Objects.TX.TaxZone, Where<PX.Objects.TX.TaxZone.taxZoneID, Equal<Required<CROpportunity.taxZoneID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) order.TaxZoneID
      }));
      taxAgency = this.GetTaxAgency(this.Base, taxZone);
    }
    PXView view = ((PXSelectBase) this.Base.Taxes).View;
    object[] objArray1 = new object[1]{ (object) order };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<CRTaxTran, PX.Objects.TX.Tax> pxResult in view.SelectMultiBound(objArray1, objArray2))
      ((PXSelectBase<CRTaxTran>) this.Base.Taxes).Delete(PXResult<CRTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult));
    ((PXGraph) this.Base).Views.Caches.Add(typeof (PX.Objects.TX.Tax));
    TaxCalc taxCalc1 = TaxBaseAttribute.GetTaxCalc<CROpportunityProducts.taxCategoryID>(((PXSelectBase) this.Base.Products).Cache, (object) null);
    PX.Objects.Extensions.SalesTax.Document extension = ((PXSelectBase) this.Base.OpportunityCurrent).Cache.GetExtension<PX.Objects.Extensions.SalesTax.Document>((object) ((PXSelectBase<CROpportunity>) this.Base.OpportunityCurrent).Current);
    TaxCalc taxCalc2 = extension.TaxCalc.Value;
    try
    {
      TaxBaseAttribute.SetTaxCalc<CROpportunityProducts.taxCategoryID>(((PXSelectBase) this.Base.Products).Cache, (object) null, TaxCalc.ManualCalc);
      extension.TaxCalc = new TaxCalc?(TaxCalc.ManualCalc);
      for (int index = 0; index < result.TaxSummary.Length; ++index)
      {
        result.TaxSummary[index].TaxType = "S";
        PX.Objects.TX.Tax tax = this.CreateTax(this.Base, taxZone, taxAgency, result.TaxSummary[index]);
        if (tax != null)
        {
          CRTaxTran crTaxTran = new CRTaxTran();
          crTaxTran.QuoteID = order.QuoteNoteID;
          crTaxTran.TaxID = tax?.TaxID;
          crTaxTran.LineNbr = new int?(index + 1);
          crTaxTran.CuryTaxAmt = new Decimal?(Math.Abs(result.TaxSummary[index].TaxAmount));
          crTaxTran.CuryTaxableAmt = new Decimal?(Math.Abs(result.TaxSummary[index].TaxableAmount));
          crTaxTran.TaxRate = new Decimal?(Convert.ToDecimal(result.TaxSummary[index].Rate) * 100M);
          crTaxTran.JurisType = result.TaxSummary[index].JurisType;
          crTaxTran.JurisName = result.TaxSummary[index].JurisName;
          crTaxTran.TaxZoneID = taxZone.TaxZoneID;
          crTaxTran.IsTaxInclusive = new bool?(result.TaxSummary[index].TaxCalculationLevel == 1);
          ((PXSelectBase<CRTaxTran>) this.Base.Taxes).Insert(crTaxTran);
        }
      }
    }
    finally
    {
      TaxBaseAttribute.SetTaxCalc<CROpportunityProducts.taxCategoryID>(((PXSelectBase) this.Base.Products).Cache, (object) null, taxCalc1);
      extension.TaxCalc = new TaxCalc?(taxCalc2);
    }
    Decimal? nullable1;
    if (!order.ManualTotalEntry.GetValueOrDefault())
    {
      Decimal? curyLineTotal = order.CuryLineTotal;
      Decimal? nullable2 = order.CuryDiscTot;
      Decimal? nullable3 = curyLineTotal.HasValue & nullable2.HasValue ? new Decimal?(curyLineTotal.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal? curyTaxTotal = order.CuryTaxTotal;
      if (!(nullable3.HasValue & curyTaxTotal.HasValue))
      {
        nullable2 = new Decimal?();
        nullable1 = nullable2;
      }
      else
        nullable1 = new Decimal?(nullable3.GetValueOrDefault() + curyTaxTotal.GetValueOrDefault());
    }
    else
    {
      Decimal? curyAmount = order.CuryAmount;
      Decimal? curyDiscTot = order.CuryDiscTot;
      nullable1 = curyAmount.HasValue & curyDiscTot.HasValue ? new Decimal?(curyAmount.GetValueOrDefault() - curyDiscTot.GetValueOrDefault()) : new Decimal?();
    }
    Decimal? curyProductsAmount = nullable1;
    this.CalcCuryProductsAmount(order, ref curyProductsAmount);
  }

  [Obsolete]
  protected IAddressLocation GetFromAddress()
  {
    using (IEnumerator<PXResult<PX.Objects.GL.Branch>> enumerator = ((PXSelectBase<PX.Objects.GL.Branch>) new PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<BAccount, On<BAccount.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<Address, On<Address.addressID, Equal<BAccount.defAddressID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>((PXGraph) this.Base)).Select(new object[1]
    {
      (object) ((PXGraph) this.Base).Accessinfo.BranchID
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
        return (IAddressLocation) PXResult<PX.Objects.GL.Branch, BAccount, Address>.op_Implicit((PXResult<PX.Objects.GL.Branch, BAccount, Address>) enumerator.Current);
    }
    return (IAddressLocation) null;
  }

  protected virtual IAddressLocation GetFromAddress(CROpportunity opportunity)
  {
    return GraphHelper.RowCast<Address>((IEnumerable) PXSelectBase<PX.Objects.GL.Branch, PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<BAccount, On<BAccount.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<Address, On<Address.addressID, Equal<BAccount.defAddressID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) opportunity.BranchID
    })).FirstOrDefault<Address>().With<Address, IAddressLocation>(new Func<Address, IAddressLocation>(this.ValidAddressFrom<BAccount.defAddressID>));
  }

  protected virtual IAddressLocation GetFromAddress(
    CROpportunity opportunity,
    CROpportunityProducts line)
  {
    return GraphHelper.RowCast<Address>((IEnumerable) PXSelectBase<Address, PXSelectJoin<Address, InnerJoin<INSite, On<INSite.FK.Address>>, Where<INSite.siteID, Equal<Current<CROpportunityProducts.siteID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new CROpportunityProducts[1]
    {
      line
    }, Array.Empty<object>())).FirstOrDefault<Address>().With<Address, IAddressLocation>(new Func<Address, IAddressLocation>(this.ValidAddressFrom<INSite.addressID>)) ?? this.GetFromAddress(opportunity);
  }

  protected virtual IAddressLocation GetToAddress(CROpportunity opportunity)
  {
    IAddressLocation toAddress;
    if (this.IsCommonCarrier(opportunity.CarrierID))
      toAddress = ((CRShippingAddress) ((PXSelectBase) this.Base.Shipping_Address).View.SelectSingleBound(new object[1]
      {
        (object) opportunity
      }, Array.Empty<object>())).With<CRShippingAddress, IAddressLocation>(new Func<CRShippingAddress, IAddressLocation>(this.ValidAddressFrom<CROpportunity.shipAddressID>));
    else
      toAddress = this.GetFromAddress(opportunity);
    return toAddress;
  }

  protected virtual IAddressLocation GetToAddress(
    CROpportunity opportunity,
    CROpportunityProducts line)
  {
    IAddressLocation toAddress;
    if (this.IsCommonCarrier(opportunity.CarrierID))
      toAddress = ((CRShippingAddress) ((PXSelectBase) this.Base.Shipping_Address).View.SelectSingleBound(new object[1]
      {
        (object) opportunity
      }, Array.Empty<object>())).With<CRShippingAddress, IAddressLocation>(new Func<CRShippingAddress, IAddressLocation>(this.ValidAddressFrom<CROpportunity.shipAddressID>));
    else
      toAddress = this.GetFromAddress(opportunity, line);
    return toAddress;
  }

  private bool IsCommonCarrier(string carrierID)
  {
    return ((bool?) Carrier.PK.Find((PXGraph) this.Base, carrierID)?.IsCommonCarrier).GetValueOrDefault();
  }

  private IAddressLocation ValidAddressFrom<TFieldSource>(IAddressLocation address) where TFieldSource : IBqlField
  {
    return !ExternalTaxBase<OpportunityMaint>.IsEmptyAddress(address) ? address : throw new PXException(this.PickAddressError<TFieldSource>((IAddressBase) address));
  }

  private string PickAddressError<TFieldSource>(IAddressBase address) where TFieldSource : IBqlField
  {
    if (typeof (TFieldSource) == typeof (BAccount.defAddressID))
      return ((PXResult) ((IQueryable<PXResult<BAccount>>) PXSelectBase<BAccount, PXSelectReadonly<BAccount, Where<BAccount.defAddressID, Equal<Required<Address.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
      {
        (object) ((Address) address).AddressID
      })).First<PXResult<BAccount>>()).GetItem<BAccount>().With<BAccount, string>((Func<BAccount, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
      {
        (object) EntityHelper.GetFriendlyEntityName<BAccount>(),
        (object) new EntityHelper((PXGraph) this.Base).GetRowID<BAccount>(e, ", ")
      })));
    if (typeof (TFieldSource) == typeof (CROpportunity.shipAddressID))
      return ((PXResult) ((IQueryable<PXResult<CROpportunity>>) PXSelectBase<CROpportunity, PXSelect<CROpportunity, Where<CROpportunity.shipAddressID, Equal<Required<Address.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
      {
        (object) ((CRAddress) address).AddressID
      })).First<PXResult<CROpportunity>>()).GetItem<CROpportunity>().With<CROpportunity, string>((Func<CROpportunity, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
      {
        (object) EntityHelper.GetFriendlyEntityName<CROpportunity>(),
        (object) new EntityHelper((PXGraph) this.Base).GetRowID<CROpportunity>(e, ", ")
      })));
    if (!(typeof (TFieldSource) == typeof (INSite.addressID)))
      throw new ArgumentOutOfRangeException("Address source unidentified.");
    return ((PXResult) ((IQueryable<PXResult<INSite>>) PXSelectBase<INSite, PXSelectReadonly<INSite, Where<INSite.addressID, Equal<Required<Address.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
    {
      (object) ((Address) address).AddressID
    })).First<PXResult<INSite>>()).GetItem<INSite>().With<INSite, string>((Func<INSite, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
    {
      (object) EntityHelper.GetFriendlyEntityName<INSite>(),
      (object) new EntityHelper((PXGraph) this.Base).GetRowID<INSite>(e, ", ")
    })));
  }
}

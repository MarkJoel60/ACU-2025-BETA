// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateSalesOrder.CRCreateSalesOrder`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.Discount;
using PX.Objects.CR.Extensions.CRConvertLinkedEntityActions;
using PX.Objects.CS;
using PX.Objects.Extensions.Discount;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.SOOrderEntryExt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.Extensions.CRCreateSalesOrder;

public abstract class CRCreateSalesOrder<TDiscountExt, TGraph, TMaster> : PXGraphExtension<TGraph>
  where TDiscountExt : DiscountGraph<TGraph, TMaster>, new()
  where TGraph : PXGraph, new()
  where TMaster : class, IBqlTable, new()
{
  public PXSelectExtension<Document> DocumentView;
  [PXCopyPasteHiddenView]
  [PXViewName("Create Sales Order")]
  public CRValidationFilter<CreateSalesOrderFilter> CreateOrderParams;
  public PXAction<TMaster> CreateSalesOrder;
  public PXAction<TMaster> CreateSalesOrderInPanel;

  public TDiscountExt DiscountExtension { get; private set; }

  protected static bool IsExtensionActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();
  }

  public CRConvertBAccountToCustomerExt<TGraph, TMaster> ConvertBAccountToCustomerExt { get; private set; }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.DiscountExtension = this.Base.GetExtension<TDiscountExt>() ?? throw new PXException("The graph does not have defined extension: {0}.", new object[1]
    {
      (object) typeof (TDiscountExt).Name
    });
    this.ConvertBAccountToCustomerExt = this.Base.FindImplementation<CRConvertBAccountToCustomerExt<TGraph, TMaster>>();
    CRPopupValidator.Generic<CreateSalesOrderFilter> generic;
    if (this.ConvertBAccountToCustomerExt != null)
      generic = CRPopupValidator.Create<CreateSalesOrderFilter>(this.CreateOrderParams, (ICRValidationFilter) this.ConvertBAccountToCustomerExt.PopupValidator);
    else
      generic = CRPopupValidator.Create<CreateSalesOrderFilter>(this.CreateOrderParams);
    this.PopupValidator = generic;
  }

  protected virtual PX.Objects.CR.Extensions.CRCreateSalesOrder.CRCreateSalesOrder<TDiscountExt, TGraph, TMaster>.DocumentMapping GetDocumentMapping()
  {
    return new PX.Objects.CR.Extensions.CRCreateSalesOrder.CRCreateSalesOrder<TDiscountExt, TGraph, TMaster>.DocumentMapping(typeof (TMaster));
  }

  public CRPopupValidator.Generic<CreateSalesOrderFilter> PopupValidator { get; private set; }

  public virtual void _(PX.Data.Events.RowUpdated<CreateSalesOrderFilter> e)
  {
    CreateSalesOrderFilter row = e.Row;
    if (row == null)
      return;
    if (!row.RecalculatePrices.GetValueOrDefault())
      ((PXSelectBase) this.CreateOrderParams).Cache.SetValue<CreateSalesOrderFilter.overrideManualPrices>((object) row, (object) false);
    if (row.RecalculateDiscounts.GetValueOrDefault())
      return;
    ((PXSelectBase) this.CreateOrderParams).Cache.SetValue<CreateSalesOrderFilter.overrideManualDiscounts>((object) row, (object) false);
    ((PXSelectBase) this.CreateOrderParams).Cache.SetValue<CreateSalesOrderFilter.overrideManualDocGroupDiscounts>((object) row, (object) false);
  }

  public virtual void _(PX.Data.Events.RowSelected<CreateSalesOrderFilter> e)
  {
    CreateSalesOrderFilter row = e.Row;
    Document masterEntity = ((PXSelectBase<Document>) this.DocumentView).Current;
    if (row == null || masterEntity == null)
      return;
    Numbering numbering = Numbering.PK.Find((PXGraph) this.Base, PX.Objects.SO.SOOrderType.PK.Find((PXGraph) this.Base, row.OrderType)?.OrderNumberingID);
    bool isManualNumbering = (numbering != null ? (numbering.UserNumbering.GetValueOrDefault() ? 1 : 0) : 0) != 0;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CreateSalesOrderFilter>>) e).Cache, (object) e.Row).For<CreateSalesOrderFilter.overrideManualPrices>((Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      bool? recalculatePrices = e.Row.RecalculatePrices;
      int num = !recalculatePrices.HasValue ? 0 : (recalculatePrices.GetValueOrDefault() ? 1 : 0);
      pxuiFieldAttribute.Enabled = num != 0;
    }));
    chained = chained.For<CreateSalesOrderFilter.overrideManualDiscounts>((Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      bool? recalculateDiscounts = e.Row.RecalculateDiscounts;
      int num = !recalculateDiscounts.HasValue ? 0 : (recalculateDiscounts.GetValueOrDefault() ? 1 : 0);
      pxuiFieldAttribute.Enabled = num != 0;
    }));
    chained = chained.SameFor<CreateSalesOrderFilter.overrideManualDocGroupDiscounts>();
    chained = chained.For<CreateSalesOrderFilter.confirmManualAmount>((Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      bool? manualTotalEntry = masterEntity.ManualTotalEntry;
      int num = !manualTotalEntry.HasValue ? 0 : (manualTotalEntry.GetValueOrDefault() ? 1 : 0);
      pxuiFieldAttribute.Visible = num != 0;
    }));
    chained = chained.For<CreateSalesOrderFilter.makeQuotePrimary>((Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute1 = ui;
      PXUIFieldAttribute pxuiFieldAttribute2 = ui;
      bool? isPrimary = masterEntity.IsPrimary;
      int num1;
      bool flag = (num1 = !isPrimary.HasValue ? 0 : (!isPrimary.GetValueOrDefault() ? 1 : 0)) != 0;
      pxuiFieldAttribute2.Visible = num1 != 0;
      int num2 = flag ? 1 : 0;
      pxuiFieldAttribute1.Enabled = num2 != 0;
    }));
    chained.For<CreateSalesOrderFilter.orderNbr>((Action<PXUIFieldAttribute>) (ui => ui.Visible = isManualNumbering));
    PXDefaultAttribute.SetPersistingCheck<CreateSalesOrderFilter.orderNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CreateSalesOrderFilter>>) e).Cache, (object) row, isManualNumbering ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<CreateSalesOrderFilter, CreateSalesOrderFilter.confirmManualAmount> e)
  {
    CreateSalesOrderFilter row = e.Row;
    Document current = ((PXSelectBase<Document>) this.DocumentView).Current;
    if (row == null || current == null)
      return;
    bool? nullable = current.ManualTotalEntry;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CreateSalesOrderFilter, CreateSalesOrderFilter.confirmManualAmount>, CreateSalesOrderFilter, object>) e).NewValue as bool?;
    if (!nullable.GetValueOrDefault())
    {
      ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<CreateSalesOrderFilter, CreateSalesOrderFilter.confirmManualAmount>>) e).Cache.RaiseExceptionHandling<CreateSalesOrderFilter.confirmManualAmount>((object) row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CreateSalesOrderFilter, CreateSalesOrderFilter.confirmManualAmount>, CreateSalesOrderFilter, object>) e).NewValue, (Exception) new PXSetPropertyException("Select this check box to confirm that you want to ignore the manual amount specified for the opportunity and create a sales order for the opportunity products.", (PXErrorLevel) 4));
      throw new PXSetPropertyException("Select this check box to confirm that you want to ignore the manual amount specified for the opportunity and create a sales order for the opportunity products.", (PXErrorLevel) 4);
    }
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<CreateSalesOrderFilter, CreateSalesOrderFilter.confirmManualAmount> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CreateSalesOrderFilter, CreateSalesOrderFilter.confirmManualAmount>>) e).Cache.RaiseExceptionHandling<CreateSalesOrderFilter.confirmManualAmount>((object) e.Row, (object) null, (Exception) null);
  }

  protected virtual void _(PX.Data.Events.RowSelected<CustomerFilter> e)
  {
    bool? nullable = this.ConvertBAccountToCustomerExt?.CanConvert();
    ((PXAction) this.CreateSalesOrderInPanel).SetEnabled((!nullable.HasValue ? 0 : (nullable.GetValueOrDefault() ? 1 : 0)) == 0 || e.Row?.WarningMessage == null);
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<CreateSalesOrderFilter, CreateSalesOrderFilter.orderType> e)
  {
    if (e.Row != null && string.IsNullOrWhiteSpace(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CreateSalesOrderFilter, CreateSalesOrderFilter.orderType>, CreateSalesOrderFilter, object>) e).NewValue as string))
      throw new PXSetPropertyException("Order Type cannot be empty.", (PXErrorLevel) 4);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable createSalesOrder(PXAdapter adapter)
  {
    bool flag1 = ((PXSelectBase) this.PopupValidator.Filter).View.Answer == 0;
    bool flag2 = ((PXSelectBase) this.PopupValidator.Filter).View.Answer == 3;
    bool flag3 = ((PXSelectBase) this.PopupValidator.Filter).View.Answer == 4;
    bool flag4 = ((PXSelectBase<CustomerFilter>) this.ConvertBAccountToCustomerExt?.CustomerInfo).Current.WarningMessage != null;
    if (flag2 | flag3)
    {
      ((PXSelectBase) this.PopupValidator.Filter).ClearAnswers(true);
      CRConvertBAccountToCustomerExt<TGraph, TMaster> baccountToCustomerExt = this.ConvertBAccountToCustomerExt;
      if (baccountToCustomerExt != null)
        ((PXSelectBase) baccountToCustomerExt.CustomerInfo).ClearAnswers(true);
      if (flag2)
        return adapter.Get();
    }
    else if (flag4)
    {
      ((PXSelectBase) this.PopupValidator.Filter).ClearAnswers();
      this.PopupValidator.AskExt(reset: false);
      return adapter.Get();
    }
    if (flag1 && PX.Objects.AR.Customer.PK.Find((PXGraph) this.Base, ((PXSelectBase<Document>) this.DocumentView).Current.BAccountID) == null)
    {
      if (PX.Objects.CR.BAccount.PK.Find((PXGraph) this.Base, ((PXSelectBase<Document>) this.DocumentView).Current.BAccountID) == null)
        throw new PXException("You must specify a business account to create a sales order.");
      if (this.ConvertBAccountToCustomerExt == null)
        throw new PXInvalidOperationException("Cannot convert the business account to a customer. The conversion extension is not found.");
      if (!this.ConvertBAccountToCustomerExt.HasAccessToCreateCustomer())
        throw new PXException("To create a sales order, you need to create a customer first. You do not have access rights to create a customer. Please contact your system administrator.");
      ((PXSelectBase) this.PopupValidator.Filter).ClearAnswers(true);
    }
    if (WebDialogResultExtension.IsPositive(this.PopupValidator.AskExt()) && this.IsPopupFilterValid())
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PX.Objects.CR.Extensions.CRCreateSalesOrder.CRCreateSalesOrder<TDiscountExt, TGraph, TMaster>.\u003C\u003Ec__DisplayClass25_0 cDisplayClass250 = new PX.Objects.CR.Extensions.CRCreateSalesOrder.CRCreateSalesOrder<TDiscountExt, TGraph, TMaster>.\u003C\u003Ec__DisplayClass25_0();
      this.Base.Actions.PressSave();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass250.graph = this.Base.CloneGraphState<TGraph>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass250, __methodptr(\u003CcreateSalesOrder\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Create & Review")]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable createSalesOrderInPanel(PXAdapter adapter)
  {
    if (!this.IsPopupFilterValid())
      return adapter.Get();
    this.Base.Actions.PressSave();
    bool? nullable = this.ConvertBAccountToCustomerExt?.CanConvert();
    if (nullable.HasValue && nullable.GetValueOrDefault())
    {
      CRConvertBAccountToCustomerExt<TGraph, TMaster> baccountToCustomerExt = this.ConvertBAccountToCustomerExt;
      CustomerConversionOptions options = new CustomerConversionOptions();
      options.DoNotCancelAfterConvert = true;
      baccountToCustomerExt.TryConvertInPanel(options);
    }
    return adapter.Get();
  }

  public virtual void DoCreateSalesOrder()
  {
    CreateSalesOrderFilter current1 = ((PXSelectBase<CreateSalesOrderFilter>) this.CreateOrderParams).Current;
    Document current2 = ((PXSelectBase<Document>) this.DocumentView).Current;
    if (current1 == null || current2 == null)
      return;
    this.DoCreateSalesOrder(PXGraph.CreateInstance<SOOrderEntry>(), current2, current1);
  }

  public virtual void DoCreateSalesOrder(
    SOOrderEntry docgraph,
    Document masterEntity,
    CreateSalesOrderFilter filter)
  {
    this.InitializeSalesOrder(docgraph, masterEntity, filter);
    CRQuote workflowProcessing = this.GetQuoteForWorkflowProcessing();
    if (workflowProcessing != null)
      GraphHelper.Hold(((PXGraph) docgraph).Caches[typeof (CRQuote)], (object) workflowProcessing);
    if (!this.Base.IsContractBasedAPI)
      throw new PXRedirectRequiredException((PXGraph) docgraph, "");
    ((PXAction) docgraph.Save).Press();
  }

  public virtual PX.Objects.AR.Customer GetCustomer()
  {
    return PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<Document.bAccountID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
  }

  public virtual bool NeedRecalculate(CreateSalesOrderFilter filter)
  {
    return filter.RecalculatePrices.GetValueOrDefault() || filter.RecalculateDiscounts.GetValueOrDefault() || filter.OverrideManualDiscounts.GetValueOrDefault() || filter.OverrideManualDocGroupDiscounts.GetValueOrDefault() || filter.OverrideManualPrices.GetValueOrDefault();
  }

  public virtual PX.Objects.SO.SOOrder InitializeSalesOrder(
    SOOrderEntry docgraph,
    Document masterEntity,
    CreateSalesOrderFilter filter)
  {
    bool needRecalculate = this.NeedRecalculate(filter);
    PX.Objects.AR.Customer customer = this.GetCustomer();
    PX.Objects.SO.SOOrder salesOrderEntity = this.CreateSalesOrderEntity(docgraph, masterEntity, filter, customer);
    this.FillShippingDetails(docgraph, salesOrderEntity);
    this.FillBillingDetails(docgraph, salesOrderEntity);
    this.FillSalesOrderEntity(docgraph, salesOrderEntity, masterEntity, needRecalculate, customer);
    this.FillRelations(docgraph, salesOrderEntity);
    this.FillLines(docgraph, salesOrderEntity, filter);
    this.FillNotesAndFiles(docgraph, salesOrderEntity, masterEntity);
    this.FillDiscounts(docgraph, salesOrderEntity, masterEntity, filter);
    this.FillUDFs(docgraph, salesOrderEntity, masterEntity);
    PX.Objects.SO.SOOrder salesOrder = ((PXSelectBase<PX.Objects.SO.SOOrder>) docgraph.Document).Update(salesOrderEntity);
    this.FillTaxes(docgraph, salesOrder, masterEntity, needRecalculate);
    this.RecalculateSalesOrderDiscounts(docgraph, filter, needRecalculate);
    return salesOrder;
  }

  public virtual PX.Objects.SO.SOOrder CreateSalesOrderEntity(
    SOOrderEntry docgraph,
    Document masterEntity,
    CreateSalesOrderFilter filter,
    PX.Objects.AR.Customer customer)
  {
    PX.Objects.SO.SOOrder soOrder1 = new PX.Objects.SO.SOOrder();
    soOrder1.OrderType = ((PXSelectBase<CreateSalesOrderFilter>) this.CreateOrderParams).Current.OrderType;
    if (!string.IsNullOrWhiteSpace(filter.OrderNbr))
      soOrder1.OrderNbr = filter.OrderNbr;
    PX.Objects.SO.SOOrder soOrder2 = ((PXSelectBase<PX.Objects.SO.SOOrder>) docgraph.Document).Insert(soOrder1);
    PX.Objects.SO.SOOrder copy = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) docgraph.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) soOrder2.OrderNbr, Array.Empty<object>())));
    copy.CuryID = masterEntity.CuryID;
    copy.CuryInfoID = this.CopyCurrenfyInfo((PXGraph) docgraph, masterEntity.CuryInfoID);
    copy.OrderDate = this.Base.Accessinfo.BusinessDate;
    copy.OrderDesc = masterEntity.Subject;
    copy.TermsID = masterEntity.TermsID ?? customer.TermsID;
    copy.CustomerID = masterEntity.BAccountID;
    copy.CustomerLocationID = masterEntity.LocationID ?? customer.DefLocationID;
    copy.ContactID = masterEntity.ContactID;
    return ((PXSelectBase<PX.Objects.SO.SOOrder>) docgraph.Document).Update(copy);
  }

  public virtual void FillShippingDetails(SOOrderEntry docgraph, PX.Objects.SO.SOOrder salesOrder)
  {
    CRShippingContact current1 = this.Base.Caches[typeof (CRShippingContact)].Current as CRShippingContact;
    SOShippingContact soShippingContact = PXResultset<SOShippingContact>.op_Implicit(((PXSelectBase<SOShippingContact>) docgraph.Shipping_Contact).Select(Array.Empty<object>()));
    int? nullable1;
    if (soShippingContact != null && current1 != null)
    {
      current1.RevisionID = current1.RevisionID ?? soShippingContact.RevisionID;
      int? revisionId = soShippingContact.RevisionID;
      nullable1 = current1.RevisionID;
      if (!(revisionId.GetValueOrDefault() == nullable1.GetValueOrDefault() & revisionId.HasValue == nullable1.HasValue))
        current1.IsDefaultContact = new bool?(false);
      CRShippingContact crShippingContact = current1;
      nullable1 = current1.BAccountContactID;
      int? nullable2 = nullable1 ?? soShippingContact.CustomerContactID;
      crShippingContact.BAccountContactID = nullable2;
      SharedRecordAttribute.CopyRecord<PX.Objects.SO.SOOrder.shipContactID>(((PXSelectBase) docgraph.Document).Cache, (object) salesOrder, (object) current1, true);
    }
    CRShippingAddress current2 = this.Base.Caches[typeof (CRShippingAddress)].Current as CRShippingAddress;
    SOShippingAddress soShippingAddress = PXResultset<SOShippingAddress>.op_Implicit(((PXSelectBase<SOShippingAddress>) docgraph.Shipping_Address).Select(Array.Empty<object>()));
    if (soShippingAddress == null || current2 == null)
      return;
    CRShippingAddress crShippingAddress1 = current2;
    nullable1 = current2.RevisionID;
    int? nullable3 = nullable1 ?? soShippingAddress.RevisionID;
    crShippingAddress1.RevisionID = nullable3;
    nullable1 = soShippingAddress.RevisionID;
    int? nullable4 = current2.RevisionID;
    if (!(nullable1.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable1.HasValue == nullable4.HasValue))
      current2.IsDefaultAddress = new bool?(false);
    CRShippingAddress crShippingAddress2 = current2;
    nullable4 = current2.BAccountAddressID;
    int? nullable5 = nullable4 ?? soShippingAddress.CustomerAddressID;
    crShippingAddress2.BAccountAddressID = nullable5;
    SharedRecordAttribute.CopyRecord<PX.Objects.SO.SOOrder.shipAddressID>(((PXSelectBase) docgraph.Document).Cache, (object) salesOrder, (object) current2, true);
  }

  public virtual void FillBillingDetails(SOOrderEntry docgraph, PX.Objects.SO.SOOrder salesOrder)
  {
    CRBillingContact current1 = this.Base.Caches[typeof (CRBillingContact)].Current as CRBillingContact;
    SOBillingContact soBillingContact = PXResultset<SOBillingContact>.op_Implicit(((PXSelectBase<SOBillingContact>) docgraph.Billing_Contact).Select(Array.Empty<object>()));
    int? nullable1;
    if (soBillingContact != null && current1 != null)
    {
      current1.RevisionID = current1.RevisionID ?? soBillingContact.RevisionID;
      int? revisionId = soBillingContact.RevisionID;
      nullable1 = current1.RevisionID;
      if (!(revisionId.GetValueOrDefault() == nullable1.GetValueOrDefault() & revisionId.HasValue == nullable1.HasValue))
        current1.OverrideContact = new bool?(true);
      CRBillingContact crBillingContact = current1;
      nullable1 = current1.BAccountContactID;
      int? nullable2 = nullable1 ?? soBillingContact.BAccountContactID;
      crBillingContact.BAccountContactID = nullable2;
      SharedRecordAttribute.CopyRecord<PX.Objects.AR.ARInvoice.billContactID>(((PXSelectBase) docgraph.Document).Cache, (object) salesOrder, (object) current1, true);
    }
    CRBillingAddress current2 = this.Base.Caches[typeof (CRBillingAddress)].Current as CRBillingAddress;
    SOBillingAddress soBillingAddress = PXResultset<SOBillingAddress>.op_Implicit(((PXSelectBase<SOBillingAddress>) docgraph.Billing_Address).Select(Array.Empty<object>()));
    if (soBillingAddress == null || current2 == null)
      return;
    CRBillingAddress crBillingAddress1 = current2;
    nullable1 = current2.RevisionID;
    int? nullable3 = nullable1 ?? soBillingAddress.RevisionID;
    crBillingAddress1.RevisionID = nullable3;
    nullable1 = soBillingAddress.RevisionID;
    int? nullable4 = current2.RevisionID;
    if (!(nullable1.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable1.HasValue == nullable4.HasValue))
      current2.OverrideAddress = new bool?(true);
    CRBillingAddress crBillingAddress2 = current2;
    nullable4 = current2.BAccountAddressID;
    int? nullable5 = nullable4 ?? soBillingAddress.BAccountAddressID;
    crBillingAddress2.BAccountAddressID = nullable5;
    SharedRecordAttribute.CopyRecord<PX.Objects.AR.ARInvoice.billAddressID>(((PXSelectBase) docgraph.Document).Cache, (object) salesOrder, (object) current2, true);
  }

  public virtual void FillSalesOrderEntity(
    SOOrderEntry docgraph,
    PX.Objects.SO.SOOrder salesOrder,
    Document masterEntity,
    bool needRecalculate,
    PX.Objects.AR.Customer customer)
  {
    if (masterEntity.TaxZoneID != null)
      salesOrder.TaxZoneID = masterEntity.TaxZoneID;
    salesOrder.TaxCalcMode = masterEntity.TaxCalcMode;
    salesOrder.ExternalTaxExemptionNumber = masterEntity.ExternalTaxExemptionNumber;
    salesOrder.AvalaraCustomerUsageType = masterEntity.AvalaraCustomerUsageType;
    salesOrder.ProjectID = masterEntity.ProjectID;
    salesOrder.BranchID = masterEntity.BranchID;
    salesOrder.DefaultSiteID = masterEntity.SiteID;
    salesOrder.ShipVia = masterEntity.CarrierID;
    salesOrder.ShipTermsID = masterEntity.ShipTermsID;
    salesOrder.ShipZoneID = masterEntity.ShipZoneID;
    salesOrder.FOBPoint = masterEntity.FOBPointID;
    salesOrder.Resedential = masterEntity.Resedential;
    salesOrder.SaturdayDelivery = masterEntity.SaturdayDelivery;
    salesOrder.Insurance = masterEntity.Insurance;
    salesOrder.ShipComplete = masterEntity.ShipComplete;
    salesOrder = ((PXSelectBase<PX.Objects.SO.SOOrder>) docgraph.Document).Update(salesOrder);
    ((PXSelectBase<PX.Objects.AR.Customer>) docgraph.customer).Current.CreditRule = customer.CreditRule;
  }

  public virtual void FillLines(
    SOOrderEntry docgraph,
    PX.Objects.SO.SOOrder salesOrder,
    CreateSalesOrderFilter filter)
  {
    bool flag = false;
    foreach (PXResult<CROpportunityProducts> pxResult in PXSelectBase<CROpportunityProducts, PXSelectJoin<CROpportunityProducts, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<CROpportunityProducts.inventoryID>>>, Where<CROpportunityProducts.quoteID, Equal<Current<Document.quoteID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
    {
      CROpportunityProducts opportunityProducts = PXResult<CROpportunityProducts>.op_Implicit(pxResult);
      bool? nullable;
      if (!opportunityProducts.SiteID.HasValue)
      {
        PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<CROpportunityProducts.inventoryID>(this.Base.Caches[typeof (CROpportunityProducts)], (object) opportunityProducts);
        if (inventoryItem != null)
        {
          nullable = inventoryItem.NonStockShip;
          if (nullable.GetValueOrDefault())
          {
            this.Base.Caches[typeof (CROpportunityProducts)].RaiseExceptionHandling<CROpportunityProducts.siteID>((object) opportunityProducts, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
            {
              (object) "[siteID]"
            }));
            flag = true;
          }
        }
      }
      PX.Objects.SO.SOLine soLine1 = ((PXSelectBase<PX.Objects.SO.SOLine>) docgraph.Transactions).Insert();
      if (soLine1 != null)
      {
        soLine1.InventoryID = opportunityProducts.InventoryID;
        soLine1.SubItemID = opportunityProducts.SubItemID;
        soLine1.TranDesc = opportunityProducts.Descr;
        soLine1.OrderQty = opportunityProducts.Quantity;
        soLine1.UOM = opportunityProducts.UOM;
        soLine1.TaxCategoryID = opportunityProducts.TaxCategoryID;
        soLine1.SiteID = opportunityProducts.SiteID;
        soLine1.IsFree = opportunityProducts.IsFree;
        soLine1.ManualPrice = new bool?(true);
        nullable = filter.RecalculatePrices;
        if (!nullable.GetValueOrDefault())
        {
          soLine1.ManualPrice = new bool?(true);
        }
        else
        {
          nullable = filter.OverrideManualPrices;
          soLine1.ManualPrice = nullable.GetValueOrDefault() ? new bool?(false) : opportunityProducts.ManualPrice;
        }
        soLine1.ManualDisc = new bool?(true);
        soLine1.CuryDiscAmt = opportunityProducts.CuryDiscAmt;
        soLine1.DiscAmt = opportunityProducts.DiscAmt;
        soLine1.DiscPct = opportunityProducts.DiscPct;
        soLine1 = ((PXSelectBase<PX.Objects.SO.SOLine>) docgraph.Transactions).Update(soLine1);
        soLine1.CuryUnitPrice = opportunityProducts.CuryUnitPrice;
        soLine1.CuryExtPrice = opportunityProducts.CuryExtPrice;
        soLine1.ProjectID = opportunityProducts.ProjectID;
        soLine1.TaskID = opportunityProducts.TaskID;
        soLine1.CostCodeID = opportunityProducts.CostCodeID;
        soLine1.POCreate = opportunityProducts.POCreate;
        soLine1.VendorID = opportunityProducts.VendorID;
        soLine1.SortOrder = opportunityProducts.SortOrder;
        nullable = filter.RecalculateDiscounts;
        if (!nullable.GetValueOrDefault())
        {
          soLine1.ManualDisc = new bool?(true);
        }
        else
        {
          nullable = filter.OverrideManualDiscounts;
          soLine1.ManualDisc = nullable.GetValueOrDefault() ? new bool?(false) : opportunityProducts.ManualDisc;
        }
        soLine1.DiscountID = opportunityProducts.DiscountID;
      }
      PX.Objects.SO.SOLine soLine2 = ((PXSelectBase<PX.Objects.SO.SOLine>) docgraph.Transactions).Update(soLine1);
      PXNoteAttribute.CopyNoteAndFiles(this.Base.Caches[typeof (CROpportunityProducts)], (object) opportunityProducts, ((PXSelectBase) docgraph.Transactions).Cache, (object) soLine2, this.Base.Caches[typeof (CRSetup)].Current as PXNoteAttribute.IPXCopySettings);
    }
    if (flag)
      throw new PXException("The warehouse isn't specified for some stock items in the product list.");
  }

  public virtual void FillNotesAndFiles(
    SOOrderEntry docgraph,
    PX.Objects.SO.SOOrder salesOrder,
    Document masterEntity)
  {
    PXNoteAttribute.CopyNoteAndFiles(this.Base.Caches[typeof (TMaster)], masterEntity.Base, ((PXSelectBase) docgraph.Document).Cache, (object) salesOrder, this.Base.Caches[typeof (CRSetup)].Current as PXNoteAttribute.IPXCopySettings);
  }

  public virtual void FillRelations(SOOrderEntry docgraph, PX.Objects.SO.SOOrder salesOrder)
  {
    SOOrderEntry_CRRelationDetailsExt extension = ((PXGraph) docgraph).GetExtension<SOOrderEntry_CRRelationDetailsExt>();
    CROpportunity crOpportunity = PXResultset<CROpportunity>.op_Implicit(PXSelectBase<CROpportunity, PXSelectReadonly<CROpportunity, Where<CROpportunity.opportunityID, Equal<Current<Document.opportunityID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    CRRelation crRelation1 = ((PXSelectBase<CRRelation>) extension.Relations).Insert();
    crRelation1.RefNoteID = salesOrder.NoteID;
    crRelation1.RefEntityType = ((object) salesOrder).GetType().FullName;
    crRelation1.Role = "SR";
    crRelation1.TargetType = "PX.Objects.CR.CROpportunity";
    crRelation1.TargetNoteID = crOpportunity.NoteID;
    crRelation1.DocNoteID = crOpportunity.NoteID;
    crRelation1.EntityID = crOpportunity.BAccountID;
    crRelation1.ContactID = crOpportunity.ContactID;
    ((PXSelectBase<CRRelation>) extension.Relations).Update(crRelation1);
    CRQuote crQuote = PXResultset<CRQuote>.op_Implicit(PXSelectBase<CRQuote, PXSelectReadonly<CRQuote, Where<CRQuote.quoteID, Equal<Current<Document.quoteID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    if (crQuote == null)
      return;
    CRRelation crRelation2 = ((PXSelectBase<CRRelation>) extension.Relations).Insert();
    crRelation2.RefNoteID = salesOrder.NoteID;
    crRelation2.RefEntityType = ((object) salesOrder).GetType().FullName;
    crRelation2.Role = "SR";
    crRelation2.TargetType = "PX.Objects.CR.CRQuote";
    crRelation2.TargetNoteID = crQuote.NoteID;
    crRelation2.DocNoteID = crQuote.NoteID;
    crRelation2.EntityID = crQuote.BAccountID;
    crRelation2.ContactID = crQuote.ContactID;
    ((PXSelectBase<CRRelation>) extension.Relations).Update(crRelation2);
  }

  public virtual void FillDiscounts(
    SOOrderEntry docgraph,
    PX.Objects.SO.SOOrder salesOrder,
    Document masterEntity,
    CreateSalesOrderFilter filter)
  {
    salesOrder.CuryDocumentDiscTotal = masterEntity.ManualTotalEntry.GetValueOrDefault() ? new Decimal?(0M) : masterEntity.CuryDiscTot;
    if (filter.RecalculateDiscounts.GetValueOrDefault() || filter.OverrideManualDiscounts.GetValueOrDefault())
      return;
    Dictionary<string, SOOrderDiscountDetail> dictionary = new Dictionary<string, SOOrderDiscountDetail>();
    foreach (PXResult<SOOrderDiscountDetail> pxResult in ((PXSelectBase<SOOrderDiscountDetail>) docgraph.DiscountDetails).Select(Array.Empty<object>()))
    {
      SOOrderDiscountDetail orderDiscountDetail = PXResult<SOOrderDiscountDetail>.op_Implicit(pxResult);
      ((PXSelectBase<SOOrderDiscountDetail>) docgraph.DiscountDetails).SetValueExt<SOOrderDiscountDetail.skipDiscount>(orderDiscountDetail, (object) true);
      string key = $"{orderDiscountDetail.Type}:{orderDiscountDetail.DiscountID}:{orderDiscountDetail.DiscountSequenceID}";
      dictionary.Add(key, orderDiscountDetail);
    }
    foreach (PXResult<PX.Objects.Extensions.Discount.Discount> pxResult in ((PXSelectBase<PX.Objects.Extensions.Discount.Discount>) this.DiscountExtension.Discounts).Select(Array.Empty<object>()))
    {
      CROpportunityDiscountDetail opportunityDiscountDetail = PXResult<PX.Objects.Extensions.Discount.Discount>.op_Implicit(pxResult).Base as CROpportunityDiscountDetail;
      string key = $"{opportunityDiscountDetail.Type}:{opportunityDiscountDetail.DiscountID}:{opportunityDiscountDetail.DiscountSequenceID}";
      SOOrderDiscountDetail orderDiscountDetail;
      bool? isManual;
      if (dictionary.TryGetValue(key, out orderDiscountDetail))
      {
        ((PXSelectBase<SOOrderDiscountDetail>) docgraph.DiscountDetails).SetValueExt<SOOrderDiscountDetail.skipDiscount>(orderDiscountDetail, (object) false);
        isManual = opportunityDiscountDetail.IsManual;
        if (isManual.GetValueOrDefault() && opportunityDiscountDetail.Type == "D")
        {
          ((PXSelectBase<SOOrderDiscountDetail>) docgraph.DiscountDetails).SetValueExt<SOOrderDiscountDetail.extDiscCode>(orderDiscountDetail, (object) opportunityDiscountDetail.ExtDiscCode);
          ((PXSelectBase<SOOrderDiscountDetail>) docgraph.DiscountDetails).SetValueExt<SOOrderDiscountDetail.description>(orderDiscountDetail, (object) opportunityDiscountDetail.Description);
          ((PXSelectBase<SOOrderDiscountDetail>) docgraph.DiscountDetails).SetValueExt<SOOrderDiscountDetail.isManual>(orderDiscountDetail, (object) opportunityDiscountDetail.IsManual);
          ((PXSelectBase<SOOrderDiscountDetail>) docgraph.DiscountDetails).SetValueExt<SOOrderDiscountDetail.curyDiscountAmt>(orderDiscountDetail, (object) opportunityDiscountDetail.CuryDiscountAmt);
        }
      }
      else
      {
        orderDiscountDetail = (SOOrderDiscountDetail) ((PXSelectBase) docgraph.DiscountDetails).Cache.CreateInstance();
        orderDiscountDetail.Type = opportunityDiscountDetail.Type;
        orderDiscountDetail.DiscountID = opportunityDiscountDetail.DiscountID;
        orderDiscountDetail.DiscountSequenceID = opportunityDiscountDetail.DiscountSequenceID;
        orderDiscountDetail.ExtDiscCode = opportunityDiscountDetail.ExtDiscCode;
        orderDiscountDetail.Description = opportunityDiscountDetail.Description;
        orderDiscountDetail = (SOOrderDiscountDetail) ((PXSelectBase) docgraph.DiscountDetails).Cache.Insert((object) orderDiscountDetail);
        isManual = opportunityDiscountDetail.IsManual;
        if (isManual.GetValueOrDefault() && (opportunityDiscountDetail.Type == "D" || opportunityDiscountDetail.Type == "B"))
        {
          orderDiscountDetail.CuryDiscountAmt = opportunityDiscountDetail.CuryDiscountAmt;
          orderDiscountDetail.IsManual = opportunityDiscountDetail.IsManual;
          ((PXSelectBase) docgraph.DiscountDetails).Cache.Update((object) orderDiscountDetail);
        }
      }
    }
    PX.Objects.SO.SOOrder copy = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) docgraph.Document).Current);
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>();
    if (!flag && !masterEntity.ManualTotalEntry.GetValueOrDefault())
    {
      ((PXSelectBase) docgraph.Document).Cache.SetValueExt<PX.Objects.SO.SOOrder.curyDiscTot>((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) docgraph.Document).Current, (object) masterEntity.CuryDiscTot);
      ((PXSelectBase) docgraph.Document).Cache.SetValueExt<PX.Objects.SO.SOOrder.curyDocumentDiscTotal>((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) docgraph.Document).Current, (object) masterEntity.CuryDiscTot);
    }
    else if (flag)
      ((PXSelectBase) docgraph.Document).Cache.SetValueExt<PX.Objects.SO.SOOrder.curyDiscTot>((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) docgraph.Document).Current, (object) DiscountEngineProvider.GetEngineFor<PX.Objects.SO.SOLine, SOOrderDiscountDetail>().GetTotalGroupAndDocumentDiscount<SOOrderDiscountDetail>((PXSelectBase<SOOrderDiscountDetail>) docgraph.DiscountDetails));
    ((PXSelectBase) docgraph.Document).Cache.RaiseRowUpdated((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) docgraph.Document).Current, (object) copy);
  }

  public virtual void FillUDFs(SOOrderEntry docgraph, PX.Objects.SO.SOOrder salesOrder, Document masterEntity)
  {
    UDFHelper.CopyAttributes(this.Base.Caches[typeof (TMaster)], masterEntity.Base, ((PXSelectBase) docgraph.Document).Cache, ((PXSelectBase) docgraph.Document).Cache.Current, salesOrder.OrderType);
  }

  public virtual void FillTaxes(
    SOOrderEntry docgraph,
    PX.Objects.SO.SOOrder salesOrder,
    Document masterEntity,
    bool needRecalculate)
  {
    if (masterEntity.TaxZoneID == null || needRecalculate)
      return;
    if (salesOrder.OrderType == "BL")
      salesOrder.TaxZoneID = (string) null;
    foreach (PXResult<CRTaxTran> pxResult in PXSelectBase<CRTaxTran, PXSelect<CRTaxTran, Where<CRTaxTran.quoteID, Equal<Current<Document.quoteID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
    {
      CRTaxTran crTaxTran = PXResult<CRTaxTran>.op_Implicit(pxResult);
      SOTaxTran newtax = new SOTaxTran();
      newtax.OrderType = salesOrder.OrderType;
      newtax.OrderNbr = salesOrder.OrderNbr;
      newtax.TaxID = crTaxTran.TaxID;
      newtax.TaxRate = crTaxTran.TaxRate;
      newtax.CuryTaxableAmt = crTaxTran.CuryTaxableAmt;
      newtax.CuryTaxAmt = crTaxTran.CuryTaxAmt;
      if (salesOrder.OrderType == "BL")
        newtax.TaxZoneID = masterEntity.TaxZoneID;
      foreach (SOTaxTran soTaxTran in GraphHelper.RowCast<SOTaxTran>(((PXSelectBase) docgraph.Taxes).Cache.Cached).Where<SOTaxTran>((Func<SOTaxTran, bool>) (a => !EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) docgraph.Taxes).Cache.GetStatus((object) a), (PXEntryStatus) 3, (PXEntryStatus) 4) && ((PXSelectBase) docgraph.Taxes).Cache.ObjectsEqual<SOTaxTran.orderNbr, SOTaxTran.orderType, SOTaxTran.taxID>((object) newtax, (object) a))))
        ((PXSelectBase<SOTaxTran>) docgraph.Taxes).Delete(soTaxTran);
      newtax = ((PXSelectBase<SOTaxTran>) docgraph.Taxes).Insert(newtax);
    }
  }

  public virtual void RecalculateSalesOrderDiscounts(
    SOOrderEntry docgraph,
    CreateSalesOrderFilter filter,
    bool needRecalculate)
  {
    if (!needRecalculate)
      return;
    ((PXSelectBase<RecalcDiscountsParamFilter>) docgraph.recalcdiscountsfilter).Current.OverrideManualPrices = new bool?(filter.OverrideManualPrices.GetValueOrDefault());
    ((PXSelectBase<RecalcDiscountsParamFilter>) docgraph.recalcdiscountsfilter).Current.RecalcDiscounts = new bool?(filter.RecalculateDiscounts.GetValueOrDefault());
    ((PXSelectBase<RecalcDiscountsParamFilter>) docgraph.recalcdiscountsfilter).Current.RecalcUnitPrices = new bool?(filter.RecalculatePrices.GetValueOrDefault());
    ((PXSelectBase<RecalcDiscountsParamFilter>) docgraph.recalcdiscountsfilter).Current.OverrideManualDiscounts = new bool?(filter.OverrideManualDiscounts.GetValueOrDefault());
    ((PXSelectBase<RecalcDiscountsParamFilter>) docgraph.recalcdiscountsfilter).Current.OverrideManualDocGroupDiscounts = new bool?(filter.OverrideManualDocGroupDiscounts.GetValueOrDefault());
    ((PXGraph) docgraph).Actions["RecalculateDiscountsAction"].Press();
  }

  public virtual long? CopyCurrenfyInfo(PXGraph graph, long? SourceCuryInfoID)
  {
    PX.Objects.CM.CurrencyInfo currencyInfo = PX.Objects.CM.CurrencyInfo.PK.Find(graph, SourceCuryInfoID);
    currencyInfo.CuryInfoID = new long?();
    graph.Caches[typeof (PX.Objects.CM.CurrencyInfo)].Clear();
    return ((PX.Objects.CM.CurrencyInfo) graph.Caches[typeof (PX.Objects.CM.CurrencyInfo)].Insert((object) currencyInfo)).CuryInfoID;
  }

  public abstract CRQuote GetQuoteForWorkflowProcessing();

  public virtual bool IsPopupFilterValid()
  {
    bool? nullable = this.ConvertBAccountToCustomerExt?.CanConvert();
    return !nullable.HasValue || !nullable.GetValueOrDefault() ? this.CreateOrderParams.TryValidate() : this.PopupValidator.TryValidate();
  }

  protected class DocumentMapping : IBqlMapping
  {
    protected System.Type _table;
    public System.Type OpportunityID = typeof (Document.opportunityID);
    public System.Type QuoteID = typeof (Document.quoteID);
    public System.Type Subject = typeof (Document.subject);
    public System.Type BAccountID = typeof (Document.bAccountID);
    public System.Type LocationID = typeof (Document.locationID);
    public System.Type ContactID = typeof (Document.contactID);
    public System.Type TaxZoneID = typeof (Document.taxZoneID);
    public System.Type TaxCalcMode = typeof (Document.taxCalcMode);
    public System.Type ManualTotalEntry = typeof (Document.manualTotalEntry);
    public System.Type CuryID = typeof (Document.curyID);
    public System.Type CuryInfoID = typeof (Document.curyInfoID);
    public System.Type CuryDiscTot = typeof (Document.curyDiscTot);
    public System.Type ProjectID = typeof (Document.projectID);
    public System.Type BranchID = typeof (Document.branchID);
    public System.Type NoteID = typeof (Document.noteID);
    public System.Type TermsID = typeof (Document.termsID);
    public System.Type ExternalTaxExemptionNumber = typeof (Document.externalTaxExemptionNumber);
    public System.Type AvalaraCustomerUsageType = typeof (Document.avalaraCustomerUsageType);
    public System.Type IsPrimary = typeof (Document.isPrimary);
    public System.Type SiteID = typeof (Document.siteID);
    public System.Type CarrierID = typeof (Document.carrierID);
    public System.Type ShipTermsID = typeof (Document.shipTermsID);
    public System.Type ShipZoneID = typeof (Document.shipZoneID);
    public System.Type FOBPointID = typeof (Document.fOBPointID);
    public System.Type Resedential = typeof (Document.resedential);
    public System.Type SaturdayDelivery = typeof (Document.saturdayDelivery);
    public System.Type Insurance = typeof (Document.insurance);
    public System.Type ShipComplete = typeof (Document.shipComplete);

    public System.Type Extension => typeof (Document);

    public System.Type Table => this._table;

    public DocumentMapping(System.Type table) => this._table = table;
  }
}

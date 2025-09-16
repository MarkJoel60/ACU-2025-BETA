// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRConvertLinkedEntityActions.CRConvertBAccountToCustomerExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CR.Extensions.CRCreateActions;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR.Extensions.CRConvertLinkedEntityActions;

public abstract class CRConvertBAccountToCustomerExt<TGraph, TMain> : 
  CRCreateActionBase<TGraph, TMain, CustomerMaint, Customer, CustomerFilter, CustomerConversionOptions>
  where TGraph : PXGraph, new()
  where TMain : class, IBqlTable, new()
{
  [PXHidden]
  [PXCopyPasteHiddenView]
  public CRValidationFilter<CustomerFilter> CustomerInfo;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public CRValidationFilter<PopupAttributes> CustomerInfoAttributes;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public CRValidationFilter<PopupUDFAttributes> CustomerInfoUDF;
  public PXAction<TMain> CreateCustomerInPanel;

  [InjectDependency]
  internal IPXPageIndexingService PageService { get; private set; }

  protected override CRValidationFilter<CustomerFilter> FilterInfo => this.CustomerInfo;

  protected virtual IEnumerable customerInfoAttributes()
  {
    return (IEnumerable) this.GetFilledAttributes();
  }

  protected virtual IEnumerable<PopupUDFAttributes> customerInfoUDF()
  {
    return this.GetRequiredUDFFields();
  }

  protected override ICRValidationFilter[] AdditionalFilters
  {
    get
    {
      return new ICRValidationFilter[2]
      {
        (ICRValidationFilter) this.CustomerInfoAttributes,
        (ICRValidationFilter) this.CustomerInfoUDF
      };
    }
  }

  [PXUIField(DisplayName = "Create Customer")]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual IEnumerable createCustomerInPanel(PXAdapter adapter)
  {
    ConversionResult<Customer> conversionResult = this.TryConvert((CustomerConversionOptions) null);
    ((PXSelectBase) this.CustomerInfo).ClearAnswers(true);
    if (conversionResult.Graph == null)
      return adapter.Get();
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException(conversionResult.Graph, "Edit Customer");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
    throw requiredException;
  }

  protected override CustomerMaint CreateTargetGraph()
  {
    BusinessAccountMaint instance = PXGraph.CreateInstance<BusinessAccountMaint>();
    ((PXSelectBase<PX.Objects.CR.BAccount>) instance.BAccount).Current = PX.Objects.CR.BAccount.PK.Find((PXGraph) this.Base, (int?) ((PXSelectBase<PX.Objects.CR.Extensions.CRCreateActions.Document>) this.Documents).Current?.BAccountID);
    return (((PXGraph) instance).GetExtension<BusinessAccountMaint.ExtendToCustomer>() ?? throw new PXException("Cannot convert the business account to a customer. The conversion extension is not found.")).Extend();
  }

  protected override Customer CreateMaster(CustomerMaint graph, CustomerConversionOptions options)
  {
    Customer current = ((PXSelectBase<Customer>) graph.BAccount).Current;
    current.CustomerClassID = ((PXSelectBase<CustomerFilter>) this.FilterInfo).Current.ClassID;
    ((PXSelectBase) graph.BAccount).View.Answer = (WebDialogResult) 6;
    ((PXSelectBase<Customer>) graph.BAccount).UpdateCurrent();
    PX.Objects.CR.Contact contact = ((PXSelectBase<PX.Objects.CR.Contact>) ((PXGraph) graph).GetProcessingExtension<CustomerMaint.DefContactAddressExt>().DefContact).SelectSingle(Array.Empty<object>());
    contact.EMail = ((PXSelectBase<CustomerFilter>) this.FilterInfo).Current.Email;
    ((PXSelectBase<PX.Objects.CR.Contact>) graph.ContactDummy).Update(contact);
    this.FillAttributes(graph.Answers, current);
    this.FillUDF(((PXSelectBase) this.CustomerInfoUDF).Cache, (object) this.GetMainCurrent(), ((PXSelectBase) graph.BAccount).Cache, current, current.ClassID);
    return current;
  }

  public virtual void TryConvertInPanel(CustomerConversionOptions options = null)
  {
    if (this.TryConvert(options).Exception == null)
      return;
    ((PXSelectBase<CustomerFilter>) this.CustomerInfo).Current.WarningMessage = PXMessages.LocalizeNoPrefix("You need to extend the business account to be a customer. Click Create Customer, and fill in the required settings of the class on the Customers (AR303000) form. Then you can create the sales order.");
    ((PXSelectBase<CustomerFilter>) this.CustomerInfo).UpdateCurrent();
  }

  public override ConversionResult<Customer> TryConvert(CustomerConversionOptions options = null)
  {
    Customer customer = Customer.PK.Find((PXGraph) this.Base, (int?) ((PXSelectBase<PX.Objects.CR.Extensions.CRCreateActions.Document>) this.Documents).Current?.BAccountID);
    if (customer == null)
      return base.TryConvert(options);
    ConversionResult<Customer> conversionResult = new ConversionResult<Customer>();
    conversionResult.Converted = false;
    conversionResult.Entity = customer;
    return conversionResult;
  }

  public virtual bool CanConvert()
  {
    return Customer.PK.Find((PXGraph) this.Base, (int?) ((PXSelectBase<PX.Objects.CR.Extensions.CRCreateActions.Document>) this.Documents).Current?.BAccountID) == null;
  }

  public virtual bool HasAccessToCreateCustomer()
  {
    System.Type type = typeof (CustomerMaint);
    PXSiteMapNode siteMapNode = PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, type);
    if (siteMapNode == null)
      return false;
    string primaryView = this.PageService.GetPrimaryView(type.FullName);
    PXCacheInfo cache = GraphHelper.GetGraphView(type, primaryView).Cache;
    PXCacheRights pxCacheRights;
    List<string> stringList1;
    List<string> stringList2;
    PXAccess.Provider.GetRights(siteMapNode.ScreenID, type.Name, cache.CacheType, ref pxCacheRights, ref stringList1, ref stringList2);
    return pxCacheRights >= 3;
  }

  protected virtual void _(PX.Data.Events.RowSelected<CustomerFilter> e)
  {
    bool canConvert = this.CanConvert();
    bool warningVisible = canConvert && e.Row?.WarningMessage != null;
    int num;
    if (canConvert && e.Row?.ClassID != null)
    {
      CustomerClass customerClass = CustomerClass.PK.Find((PXGraph) this.Base, e.Row.ClassID);
      if (customerClass != null)
      {
        bool? statementByEmail = customerClass.SendStatementByEmail;
        if (!statementByEmail.HasValue || !statementByEmail.GetValueOrDefault())
        {
          bool? mailInvoices = customerClass.MailInvoices;
          if (!mailInvoices.HasValue || !mailInvoices.GetValueOrDefault())
          {
            bool? mailDunningLetters = customerClass.MailDunningLetters;
            num = !mailDunningLetters.HasValue || !mailDunningLetters.GetValueOrDefault() ? 0 : (PXAccess.FeatureInstalled<FeaturesSet.dunningLetter>() ? 1 : 0);
            goto label_7;
          }
        }
        num = 1;
        goto label_7;
      }
    }
    num = 0;
label_7:
    bool emailRequired = num != 0;
    PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerFilter>>) e).Cache, (object) e.Row).ForAllFields((Action<PXUIFieldAttribute>) (ui => ui.Visible = canConvert)).For<CustomerFilter.warningMessage>((Action<PXUIFieldAttribute>) (ui => ui.Visible = warningVisible)).For<CustomerFilter.classID>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = !warningVisible)).For<CustomerFilter.email>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = !warningVisible));
    PXCacheEx.Adjust<PXDefaultAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerFilter>>) e).Cache, (object) e.Row).For<CustomerFilter.email>((Action<PXDefaultAttribute>) (d => d.PersistingCheck = emailRequired ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2));
    ((PXAction) this.CreateCustomerInPanel).SetVisible(warningVisible);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CustomerFilter, CustomerFilter.acctCD> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CustomerFilter, CustomerFilter.acctCD>, CustomerFilter, object>) e).NewValue = (object) PX.Objects.CR.BAccount.PK.Find((PXGraph) this.Base, (int?) ((PXSelectBase<PX.Objects.CR.Extensions.CRCreateActions.Document>) this.Documents).Current?.BAccountID)?.AcctCD;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CustomerFilter, CustomerFilter.email> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CustomerFilter, CustomerFilter.email>, CustomerFilter, object>) e).NewValue = (object) ((PX.Objects.CR.Contact) PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.defContactID>.FindParent((PXGraph) this.Base, (PX.Objects.CR.BAccount.defContactID) PX.Objects.CR.BAccount.PK.Find((PXGraph) this.Base, (int?) ((PXSelectBase<PX.Objects.CR.Extensions.CRCreateActions.Document>) this.Documents).Current?.BAccountID), (PKFindOptions) 0))?.EMail;
  }
}

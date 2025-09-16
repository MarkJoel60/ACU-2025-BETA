// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOReleaseInvoice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.SQLTree;
using PX.Objects.AR;
using PX.Objects.AR.MigrationMode;
using PX.Objects.AR.Standalone;
using PX.Objects.CA;
using PX.Objects.CM.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.SO;

[TableAndChartDashboardType]
public class SOReleaseInvoice : PXGraph<SOReleaseInvoice>
{
  public PXCancel<SOInvoiceFilter> Cancel;
  public PXAction<SOInvoiceFilter> viewDocument;
  public PXFilter<SOInvoiceFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<PX.Objects.AR.ARInvoice, SOInvoiceFilter> SOInvoiceList;
  protected bool _ActionChanged;

  public virtual IEnumerable soInvoiceList() => (IEnumerable) this.GetInvoices();

  [PXRemoveBaseAttribute(typeof (ARDocType.ListAttribute))]
  [PXMergeAttributes]
  [ARDocType.SOEntryList]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.docType> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (ARInvoiceType.RefNbrAttribute))]
  [PXMergeAttributes]
  [ARInvoiceType.RefNbr(typeof (Search2<SOInvoice.refNbr, InnerJoin<ARRegisterAlias, On<ARRegisterAlias.docType, Equal<SOInvoice.docType>, And<ARRegisterAlias.refNbr, Equal<SOInvoice.refNbr>>>, InnerJoinSingleTable<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARInvoice.docType, Equal<ARRegisterAlias.docType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<ARRegisterAlias.refNbr>>>, InnerJoinSingleTable<PX.Objects.AR.Customer, On<ARRegisterAlias.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>>>, Where<SOInvoice.docType, Equal<Optional<SOInvoice.docType>>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>, OrderBy<Desc<SOInvoice.refNbr>>>), Filterable = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.refNbr> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXRemoveBaseAttribute(typeof (AROpenPeriodAttribute))]
  [PXMergeAttributes]
  [SOOpenPeriod(typeof (PX.Objects.AR.ARRegister.docDate), typeof (PX.Objects.AR.ARRegister.branchID), null, null, null, null, true, false, typeof (PX.Objects.AR.ARRegister.tranPeriodID), IsHeader = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.finPeriodID> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (ARTermsSelectorAttribute))]
  [PXRemoveBaseAttribute(typeof (TermsAttribute))]
  [PXMergeAttributes]
  [SOInvoiceTerms]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<BqlOperand<PX.Objects.CS.Terms.visibleTo, IBqlString>.IsIn<TermsVisibleTo.all, TermsVisibleTo.customer>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.termsID> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXDBCurrencyAttribute))]
  [PXMergeAttributes]
  [PXDBCurrency(typeof (PX.Objects.AR.ARInvoice.curyInfoID), typeof (PX.Objects.AR.ARInvoice.origDocAmt))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.curyOrigDocAmt> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXDBCurrencyAttribute))]
  [PXMergeAttributes]
  [PXDBCurrency(typeof (PX.Objects.AR.ARInvoice.curyInfoID), typeof (PX.Objects.AR.ARInvoice.docBal), BaseCalc = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.curyDocBal> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXDBCurrencyAttribute))]
  [PXMergeAttributes]
  [PXDBCurrency(typeof (PX.Objects.AR.ARInvoice.curyInfoID), typeof (PX.Objects.AR.ARInvoice.origDiscAmt))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.curyOrigDiscAmt> e)
  {
  }

  public SOReleaseInvoice()
  {
    ARSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    ((PXProcessingBase<PX.Objects.AR.ARInvoice>) this.SOInvoiceList).SetSelected<PX.Objects.AR.ARInvoice.selected>();
    PXCacheEx.AdjustUI(((PXGraph) this).Caches[typeof (PX.Objects.AR.ARInvoice)], (object) null).For<PX.Objects.AR.ARInvoice.paymentTotal>((Action<PXUIFieldAttribute>) (a => a.Visible = false));
  }

  [PXEditDetailButton]
  [PXUIField]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.SOInvoiceList).Current != null)
    {
      SOInvoiceEntry instance = PXGraph.CreateInstance<SOInvoiceEntry>();
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.SOInvoiceList).Current.RefNbr, new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.SOInvoiceList).Current.DocType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Order");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  public virtual void _(PX.Data.Events.RowSelected<SOInvoiceFilter> e)
  {
    PXUIFieldAttribute.SetVisible<SOInvoiceFilter.showPrinted>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOInvoiceFilter>>) e).Cache, (object) e.Row, e.Row?.Action == "SO303000$printInvoice");
    if (string.IsNullOrEmpty(e.Row?.Action))
      return;
    Dictionary<string, object> dictionary = ((PXSelectBase) this.Filter).Cache.ToDictionary((object) e.Row);
    ((PXProcessingBase<PX.Objects.AR.ARInvoice>) this.SOInvoiceList).SetProcessWorkflowAction(e.Row.Action, dictionary);
    bool showPrintSettings = this.IsPrintingAllowed(e.Row);
    bool usingEmailAction = e.Row.Action == "SO303000$emailInvoice";
    bool? aggregateEmails = e.Row.AggregateEmails;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained1 = PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOInvoiceFilter>>) e).Cache, (object) e.Row).For<SOInvoiceFilter.printWithDeviceHub>((Action<PXUIFieldAttribute>) (a => a.Visible = showPrintSettings)).For<SOInvoiceFilter.definePrinterManually>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Visible = showPrintSettings;
      if (PXContext.GetSlot<AUSchedule>() != null)
        return;
      a.Enabled = e.Row.PrintWithDeviceHub.GetValueOrDefault();
    }));
    chained1 = chained1.SameFor<SOInvoiceFilter.numberOfCopies>();
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained2 = chained1.For<SOInvoiceFilter.printerID>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Visible = showPrintSettings;
      if (PXContext.GetSlot<AUSchedule>() != null)
        return;
      PXUIFieldAttribute pxuiFieldAttribute = a;
      bool? nullable = e.Row.PrintWithDeviceHub;
      int num;
      if (nullable.GetValueOrDefault())
      {
        nullable = e.Row.DefinePrinterManually;
        num = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num = 0;
      pxuiFieldAttribute.Enabled = num != 0;
    })).For<SOInvoiceFilter.aggregateEmails>((Action<PXUIFieldAttribute>) (ui => ui.Visible = usingEmailAction));
    chained2 = chained2.SameFor<SOInvoiceFilter.aggregateAttachments>();
    chained2.For<SOInvoiceFilter.aggregateAttachments>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = usingEmailAction && aggregateEmails.GetValueOrDefault()));
    PXCacheEx.AdjustUI(((PXSelectBase) this.SOInvoiceList).Cache, (object) null).For<PX.Objects.AR.ARInvoice.curyPaymentTotal>((Action<PXUIFieldAttribute>) (attr => attr.Visible = e.Row.Action == "SO303000$createAndCapturePayment")).SameFor<PX.Objects.AR.ARInvoice.curyUnpaidBalance>();
  }

  public virtual bool IsPrintingAllowed(SOInvoiceFilter filter)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() && filter?.Action == "SO303000$printInvoice";
  }

  public virtual void _(PX.Data.Events.RowUpdated<SOInvoiceFilter> e)
  {
    this._ActionChanged = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOInvoiceFilter>>) e).Cache.ObjectsEqual<SOInvoiceFilter.action>((object) e.Row, (object) e.OldRow);
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOInvoiceFilter>>) e).Cache.ObjectsEqual<SOInvoiceFilter.action, SOInvoiceFilter.definePrinterManually, SOInvoiceFilter.printWithDeviceHub>((object) e.Row, (object) e.OldRow) || !PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() || !((PXSelectBase<SOInvoiceFilter>) this.Filter).Current.With<SOInvoiceFilter, bool>((Func<SOInvoiceFilter, bool>) (r =>
    {
      if (!r.PrintWithDeviceHub.GetValueOrDefault() || !r.DefinePrinterManually.GetValueOrDefault())
        return false;
      return PXContext.GetSlot<AUSchedule>() == null || !r.PrinterID.HasValue || e.OldRow.PrinterID.HasValue;
    })))
      return;
    ((PXSelectBase<SOInvoiceFilter>) this.Filter).Current.PrinterID = new NotificationUtility((PXGraph) this).SearchPrinter("Customer", "SO643000", ((PXGraph) this).Accessinfo.BranchID);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<SOInvoiceFilter, SOInvoiceFilter.printerID> e)
  {
    if (e.Row == null || this.IsPrintingAllowed(e.Row))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOInvoiceFilter, SOInvoiceFilter.printerID>, SOInvoiceFilter, object>) e).NewValue = (object) null;
  }

  public virtual IEnumerable<PX.Objects.AR.ARInvoice> GetInvoices()
  {
    SOReleaseInvoice soReleaseInvoice1 = this;
    if (!(((PXSelectBase<SOInvoiceFilter>) soReleaseInvoice1.Filter).Current.Action == "<SELECT>"))
    {
      if (soReleaseInvoice1._ActionChanged)
        ((PXSelectBase) soReleaseInvoice1.SOInvoiceList).Cache.Clear();
      PXSelectBase<PX.Objects.AR.ARInvoice> listSelectCommand = soReleaseInvoice1.GetInvoiceListSelectCommand(((PXSelectBase<SOInvoiceFilter>) soReleaseInvoice1.Filter).Current);
      soReleaseInvoice1.ApplyAdditionalFilters(listSelectCommand, ((PXSelectBase<SOInvoiceFilter>) soReleaseInvoice1.Filter).Current);
      int startRow = PXView.StartRow;
      int num = 0;
      PXGraph.CommandPreparingEvents commandPreparing1 = ((PXGraph) soReleaseInvoice1).CommandPreparing;
      SOReleaseInvoice soReleaseInvoice2 = soReleaseInvoice1;
      // ISSUE: virtual method pointer
      PXCommandPreparing commandPreparing2 = new PXCommandPreparing((object) soReleaseInvoice2, __vmethodptr(soReleaseInvoice2, ARInvoiceDocTypeCommandPreparing));
      commandPreparing1.AddHandler<PX.Objects.AR.ARInvoice.docType>(commandPreparing2);
      PXGraph.CommandPreparingEvents commandPreparing3 = ((PXGraph) soReleaseInvoice1).CommandPreparing;
      SOReleaseInvoice soReleaseInvoice3 = soReleaseInvoice1;
      // ISSUE: virtual method pointer
      PXCommandPreparing commandPreparing4 = new PXCommandPreparing((object) soReleaseInvoice3, __vmethodptr(soReleaseInvoice3, ARInvoiceRefNbrCommandPreparing));
      commandPreparing3.AddHandler<PX.Objects.AR.ARInvoice.refNbr>(commandPreparing4);
      foreach (PXResult<PX.Objects.AR.ARInvoice> pxResult in ((PXSelectBase) listSelectCommand).View.Select(PXView.Currents, (object[]) null, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
      {
        PX.Objects.AR.ARInvoice invoice = PXResult<PX.Objects.AR.ARInvoice>.op_Implicit(pxResult);
        PX.Objects.AR.ARInvoice arInvoice = (PX.Objects.AR.ARInvoice) ((PXSelectBase) soReleaseInvoice1.SOInvoiceList).Cache.Locate((object) invoice);
        if (arInvoice != null)
          invoice.Selected = arInvoice.Selected;
        yield return invoice;
        PXView.StartRow = 0;
      }
      PXGraph.CommandPreparingEvents commandPreparing5 = ((PXGraph) soReleaseInvoice1).CommandPreparing;
      SOReleaseInvoice soReleaseInvoice4 = soReleaseInvoice1;
      // ISSUE: virtual method pointer
      PXCommandPreparing commandPreparing6 = new PXCommandPreparing((object) soReleaseInvoice4, __vmethodptr(soReleaseInvoice4, ARInvoiceRefNbrCommandPreparing));
      commandPreparing5.RemoveHandler<PX.Objects.AR.ARInvoice.refNbr>(commandPreparing6);
      PXGraph.CommandPreparingEvents commandPreparing7 = ((PXGraph) soReleaseInvoice1).CommandPreparing;
      SOReleaseInvoice soReleaseInvoice5 = soReleaseInvoice1;
      // ISSUE: virtual method pointer
      PXCommandPreparing commandPreparing8 = new PXCommandPreparing((object) soReleaseInvoice5, __vmethodptr(soReleaseInvoice5, ARInvoiceDocTypeCommandPreparing));
      commandPreparing7.RemoveHandler<PX.Objects.AR.ARInvoice.docType>(commandPreparing8);
    }
  }

  protected virtual PXSelectBase<PX.Objects.AR.ARInvoice> GetInvoiceListSelectCommand(
    SOInvoiceFilter filter)
  {
    switch (filter.Action)
    {
      case "SO303000$post":
        return (PXSelectBase<PX.Objects.AR.ARInvoice>) new FbqlSelect<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOOrderShipment>.On<SOOrderShipment.FK.ARInvoice>>, FbqlJoins.Inner<SOOrderType>.On<SOOrderShipment.FK.OrderType>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.released, Equal<True>>>>, And<BqlOperand<SOOrderType.iNDocType, IBqlString>.IsNotEqual<INTranType.noUpdate>>>, And<BqlOperand<SOOrderShipment.invtRefNbr, IBqlString>.IsNull>>>.And<BqlOperand<SOOrderShipment.createINDoc, IBqlBool>.IsEqual<True>>>.Aggregate<To<GroupBy<PX.Objects.AR.ARInvoice.docType>, GroupBy<PX.Objects.AR.ARInvoice.refNbr>, GroupBy<PX.Objects.AR.ARInvoice.released>>>, PX.Objects.AR.ARInvoice>.View((PXGraph) this);
      case "SO303000$createAndCapturePayment":
        return (PXSelectBase<PX.Objects.AR.ARInvoice>) new FbqlSelect<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CA.PaymentMethod>.On<BqlOperand<PX.Objects.CA.PaymentMethod.paymentMethodID, IBqlString>.IsEqual<PX.Objects.AR.ARInvoice.paymentMethodID>>>, FbqlJoins.Inner<PX.Objects.AR.CustomerPaymentMethod>.On<BqlOperand<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, IBqlInt>.IsEqual<PX.Objects.AR.ARInvoice.pMInstanceID>>>, FbqlJoins.Inner<CCProcessingCenter>.On<BqlOperand<CCProcessingCenter.processingCenterID, IBqlString>.IsEqual<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CA.PaymentMethod.paymentType, In3<PaymentMethodType.creditCard, PaymentMethodType.eft>>>>, And<BqlOperand<PX.Objects.CA.PaymentMethod.isActive, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PX.Objects.CA.PaymentMethod.useForAR, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PX.Objects.AR.ARInvoice.pMInstanceID, IBqlInt>.IsNotNull>>, And<BqlOperand<PX.Objects.AR.ARInvoice.curyUnpaidBalance, IBqlDecimal>.IsGreater<decimal0>>>>.And<BqlOperand<CCProcessingCenter.isExternalAuthorizationOnly, IBqlBool>.IsNotEqual<True>>>, PX.Objects.AR.ARInvoice>.View((PXGraph) this);
      case "SO303000$emailInvoice":
        return (PXSelectBase<PX.Objects.AR.ARInvoice>) new FbqlSelect<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARRegister.emailInvoice, Equal<True>>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.released, IBqlBool>.IsEqual<False>>>, PX.Objects.AR.ARInvoice>.View((PXGraph) this);
      case "SO303000$printInvoice":
        return !filter.ShowPrinted.GetValueOrDefault() ? (PXSelectBase<PX.Objects.AR.ARInvoice>) new FbqlSelect<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.printInvoice, Equal<True>>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.released, IBqlBool>.IsEqual<False>>>, PX.Objects.AR.ARInvoice>.View((PXGraph) this) : (PXSelectBase<PX.Objects.AR.ARInvoice>) new FbqlSelect<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARRegister.dontPrint, NotEqual<True>>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.released, IBqlBool>.IsEqual<False>>>, PX.Objects.AR.ARInvoice>.View((PXGraph) this);
      default:
        return (PXSelectBase<PX.Objects.AR.ARInvoice>) new FbqlSelect<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.AR.ARInvoice>.View((PXGraph) this);
    }
  }

  protected virtual void ApplyAdditionalFilters(
    PXSelectBase<PX.Objects.AR.ARInvoice> command,
    SOInvoiceFilter filter)
  {
    command.WhereAnd<Where<BqlOperand<PX.Objects.AR.ARInvoice.origModule, IBqlString>.IsEqual<BatchModule.moduleSO>>>();
    command.WhereAnd<Where<WorkflowAction.IsEnabled<PX.Objects.AR.ARInvoice, SOInvoiceFilter.action>>>();
    command.Join<InnerJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.ARInvoice.FK.Customer>>>();
    command.WhereAnd<Where<Match<PX.Objects.AR.Customer, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>();
    command.WhereAnd<Where<BqlOperand<PX.Objects.AR.ARInvoice.docDate, IBqlDateTime>.IsLessEqual<BqlField<SOInvoiceFilter.endDate, IBqlDateTime>.FromCurrent>>>();
    if (filter.StartDate.HasValue)
      command.WhereAnd<Where<BqlOperand<PX.Objects.AR.ARInvoice.docDate, IBqlDateTime>.IsGreaterEqual<BqlField<SOInvoiceFilter.startDate, IBqlDateTime>.FromCurrent>>>();
    if (!filter.CustomerID.HasValue)
      return;
    command.WhereAnd<Where<BqlOperand<PX.Objects.AR.ARInvoice.customerID, IBqlInt>.IsEqual<BqlField<SOInvoiceFilter.customerID, IBqlInt>.FromCurrent>>>();
  }

  protected virtual void ARInvoiceDocTypeCommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 16 /*0x10*/) != 16 /*0x10*/ || e.Value != null)
      return;
    e.Expr = (SQLExpression) new Column<PX.Objects.AR.ARInvoice.docType>((Table) null);
    e.Value = (object) 0;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARInvoiceRefNbrCommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 16 /*0x10*/) != 16 /*0x10*/ || e.Value != null)
      return;
    e.Expr = (SQLExpression) new Column<PX.Objects.AR.ARInvoice.refNbr>((Table) null);
    e.Value = (object) "";
    ((CancelEventArgs) e).Cancel = true;
  }

  public class WellKnownActions
  {
    public class SOInvoiceScreen
    {
      public const string ScreenID = "SO303000";
      public const string ReleaseInvoice = "SO303000$release";
      public const string PostInvoiceToInventory = "SO303000$post";
      public const string EmailInvoice = "SO303000$emailInvoice";
      public const string PrintInvoice = "SO303000$printInvoice";
      public const string CreateAndCapturePayment = "SO303000$createAndCapturePayment";
      public const string ReleaseFromCreditHold = "SO303000$releaseFromCreditHold";
    }
  }
}

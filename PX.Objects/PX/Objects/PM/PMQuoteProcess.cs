// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMQuoteProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.SM;
using PX.TM;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.PM;

public class PMQuoteProcess : PXGraph<
#nullable disable
PMQuoteProcess>
{
  public PXCancel<PMQuoteProcessFilter> Cancel;
  public PXAction<PMQuoteProcessFilter> viewDocument;
  public PXFilter<PMQuoteProcessFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<PMQuote, PMQuoteProcessFilter> Records;

  public virtual IEnumerable records(PXAdapter adapter)
  {
    if (!(((PXSelectBase<PMQuoteProcessFilter>) this.Filter).Current.Action == "<SELECT>"))
    {
      PXSelectBase<PMQuote> bqlStatement = this.GetBQLStatement();
      int startRow = PXView.StartRow;
      int num = 0;
      PXView view = ((PXSelectBase) bqlStatement).View;
      object[] objArray = new object[1]
      {
        (object) ((PXSelectBase<PMQuoteProcessFilter>) this.Filter).Current
      };
      object[] parameters = PXView.Parameters;
      object[] searches = PXView.Searches;
      string[] sortColumns = PXView.SortColumns;
      bool[] descendings = PXView.Descendings;
      PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
      ref int local1 = ref startRow;
      int maximumRows = PXView.MaximumRows;
      ref int local2 = ref num;
      foreach (object obj in view.Select(objArray, parameters, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2))
        yield return obj;
      PXView.StartRow = 0;
    }
  }

  public virtual PXSelectBase<PMQuote> GetBQLStatement()
  {
    System.Type type1 = OwnedFilter.ProjectionAttribute.ComposeWhere(typeof (PMQuoteProcessFilter), typeof (PMQuote.workgroupID), typeof (PMQuote.ownerID));
    System.Type type2 = typeof (Where<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMQuote.status, Equal<CRQuoteStatusAttribute.approved>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMQuote.status, Equal<CRQuoteStatusAttribute.sent>>>>>.Or<BqlOperand<PMQuote.status, IBqlString>.IsEqual<CRQuoteStatusAttribute.quoteApproved>>>>>);
    System.Type type3 = typeof (Where<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMQuote.status, Equal<CRQuoteStatusAttribute.approved>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMQuote.status, Equal<CRQuoteStatusAttribute.sent>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMQuote.status, Equal<CRQuoteStatusAttribute.accepted>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMQuote.status, Equal<CRQuoteStatusAttribute.converted>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMQuote.status, Equal<CRQuoteStatusAttribute.declined>>>>>.Or<BqlOperand<PMQuote.status, IBqlString>.IsEqual<CRQuoteStatusAttribute.quoteApproved>>>>>>>>);
    PXSelectBase<PMQuote> bqlStatement = (PXSelectBase<PMQuote>) new FbqlSelect<SelectFromBase<PMQuote, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<CRContact>.On<BqlOperand<CRContact.contactID, IBqlInt>.IsEqual<PMQuote.opportunityContactID>>>, FbqlJoins.Left<PX.Objects.CR.BAccount>.On<BqlOperand<PX.Objects.CR.BAccount.bAccountID, IBqlInt>.IsEqual<PMQuote.bAccountID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Match<PX.Objects.CR.BAccount, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>, And<BqlOperand<PMQuote.documentDate, IBqlDateTime>.IsLessEqual<BqlField<PMQuoteProcessFilter.endDate, IBqlDateTime>.FromCurrent>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<PMQuoteProcessFilter.startDate>, IsNull>>>>.Or<BqlOperand<PMQuote.documentDate, IBqlDateTime>.IsGreaterEqual<BqlField<PMQuoteProcessFilter.startDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<PMQuoteProcessFilter.bAccountID>, IsNull>>>>.Or<BqlOperand<Current<PMQuoteProcessFilter.bAccountID>, IBqlInt>.IsEqual<PMQuote.bAccountID>>>>, PMQuote>.View((PXGraph) this);
    if (((PXSelectBase<PMQuoteProcessFilter>) this.Filter).Current.Action == "PM304500$sendQuote")
      bqlStatement.WhereAnd(type2);
    if (((PXSelectBase<PMQuoteProcessFilter>) this.Filter).Current.Action == "PM304500$printQuote")
      bqlStatement.WhereAnd(type3);
    bqlStatement.WhereAnd(type1);
    return bqlStatement;
  }

  [PXBool]
  [PXUIField(DisplayName = "Primary", Enabled = false)]
  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.CR.Standalone.CROpportunity.defQuoteID, IsNotNull, And<PX.Objects.CR.Standalone.CRQuote.quoteID, Equal<PX.Objects.CR.Standalone.CROpportunity.defQuoteID>>>, True>, False>), typeof (bool))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PMQuote.isPrimary> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMQuoteProcessFilter> e)
  {
    if (string.IsNullOrEmpty(e.Row?.Action))
      return;
    ((PXProcessingBase<PMQuote>) this.Records).SetProcessWorkflowAction(e.Row.Action, ((PXSelectBase) this.Filter).Cache.ToDictionary((object) e.Row));
    bool showPrintSettings = this.IsPrintingAllowed(e.Row);
    bool usingEmailAction = e.Row.Action == "PM304500$sendQuote";
    bool? aggregateEmails = (bool?) e.Row?.AggregateEmails;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMQuoteProcessFilter>>) e).Cache, (object) e.Row).For<PMQuoteProcessFilter.printWithDeviceHub>((Action<PXUIFieldAttribute>) (ui => ui.Visible = showPrintSettings));
    chained = chained.For<PMQuoteProcessFilter.definePrinterManually>((Action<PXUIFieldAttribute>) (ui =>
    {
      ui.Visible = showPrintSettings;
      if (PXContext.GetSlot<AUSchedule>() != null)
        return;
      ui.Enabled = e.Row.PrintWithDeviceHub.GetValueOrDefault();
    }));
    chained = chained.SameFor<PMQuoteProcessFilter.numberOfCopies>();
    chained = chained.For<PMQuoteProcessFilter.printerID>((Action<PXUIFieldAttribute>) (ui =>
    {
      ui.Visible = showPrintSettings;
      if (PXContext.GetSlot<AUSchedule>() != null)
        return;
      PXUIFieldAttribute pxuiFieldAttribute = ui;
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
    }));
    chained = chained.For<PrintInvoicesFilter.aggregateEmails>((Action<PXUIFieldAttribute>) (ui => ui.Visible = usingEmailAction));
    chained = chained.SameFor<PrintInvoicesFilter.aggregateAttachments>();
    chained.For<PrintInvoicesFilter.aggregateAttachments>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = usingEmailAction && aggregateEmails.GetValueOrDefault()));
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMQuoteProcessFilter> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PMQuoteProcessFilter>>) e).Cache.ObjectsEqual<PMQuoteProcessFilter.action, PMQuoteProcessFilter.definePrinterManually, PMQuoteProcessFilter.printWithDeviceHub>((object) e.Row, (object) e.OldRow) || !PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() || ((PXSelectBase<PMQuoteProcessFilter>) this.Filter).Current == null || !((PXSelectBase<PMQuoteProcessFilter>) this.Filter).Current.PrintWithDeviceHub.GetValueOrDefault() || !((PXSelectBase<PMQuoteProcessFilter>) this.Filter).Current.DefinePrinterManually.GetValueOrDefault() || PXContext.GetSlot<AUSchedule>() != null && ((PXSelectBase<PMQuoteProcessFilter>) this.Filter).Current.PrinterID.HasValue && !e.OldRow.PrinterID.HasValue)
      return;
    ((PXSelectBase<PMQuoteProcessFilter>) this.Filter).Current.PrinterID = new NotificationUtility((PXGraph) this).SearchPrinter("BAccount", "PM604500", ((PXGraph) this).Accessinfo.BranchID);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMQuoteProcessFilter, PMQuoteProcessFilter.printerID> e)
  {
    if (e.Row == null || this.IsPrintingAllowed(e.Row))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMQuoteProcessFilter, PMQuoteProcessFilter.printerID>, PMQuoteProcessFilter, object>) e).NewValue = (object) null;
  }

  public virtual bool IsPrintingAllowed(PMQuoteProcessFilter filter)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() && filter.Action == "PM304500$printQuote";
  }

  [PXEditDetailButton]
  [PXUIField]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<PMQuote>) this.Records).Current != null)
    {
      PMQuoteMaint instance = PXGraph.CreateInstance<PMQuoteMaint>();
      ((PXSelectBase<PMQuote>) instance.Quote).Current = PMQuote.PK.Find((PXGraph) instance, ((PXSelectBase<PMQuote>) this.Records).Current.QuoteNbr);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Quote");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  public class WellKnownActions
  {
    public class PMQuoteScreen
    {
      public const string ScreenID = "PM304500";
      public const string PrintQuote = "PM304500$printQuote";
      public const string EmailQuote = "PM304500$sendQuote";

      public class printQuote : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        PMQuoteProcess.WellKnownActions.PMQuoteScreen.printQuote>
      {
        public printQuote()
          : base("PM304500$printQuote")
        {
        }
      }

      public class emailQuote : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        PMQuoteProcess.WellKnownActions.PMQuoteScreen.emailQuote>
      {
        public emailQuote()
          : base("PM304500$sendQuote")
        {
        }
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRQuoteProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.SM;
using PX.TM;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.CR;

public class CRQuoteProcess : PXGraph<
#nullable disable
CRQuoteProcess>
{
  public PXCancel<CRQuoteProcessFilter> Cancel;
  public PXAction<CRQuoteProcessFilter> viewDocument;
  public PXFilter<CRQuoteProcessFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<CRQuote, CRQuoteProcessFilter> Records;

  public virtual IEnumerable records(PXAdapter adapter)
  {
    if (!(((PXSelectBase<CRQuoteProcessFilter>) this.Filter).Current.Action == "<SELECT>"))
    {
      PXSelectBase<CRQuote> bqlStatement = this.GetBQLStatement();
      int startRow = PXView.StartRow;
      int num = 0;
      PXView view = ((PXSelectBase) bqlStatement).View;
      object[] objArray = new object[1]
      {
        (object) ((PXSelectBase<CRQuoteProcessFilter>) this.Filter).Current
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

  public virtual PXSelectBase<CRQuote> GetBQLStatement()
  {
    System.Type type1 = OwnedFilter.ProjectionAttribute.ComposeWhere(typeof (CRQuoteProcessFilter), typeof (CRQuote.workgroupID), typeof (CRQuote.ownerID));
    System.Type type2 = typeof (Where<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRQuote.status, Equal<CRQuoteStatusAttribute.approved>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRQuote.status, Equal<CRQuoteStatusAttribute.sent>>>>>.Or<BqlOperand<CRQuote.status, IBqlString>.IsEqual<CRQuoteStatusAttribute.quoteApproved>>>>>);
    System.Type type3 = typeof (Where<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRQuote.status, Equal<CRQuoteStatusAttribute.approved>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRQuote.status, Equal<CRQuoteStatusAttribute.sent>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRQuote.status, Equal<CRQuoteStatusAttribute.accepted>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRQuote.status, Equal<CRQuoteStatusAttribute.converted>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRQuote.status, Equal<CRQuoteStatusAttribute.declined>>>>>.Or<BqlOperand<CRQuote.status, IBqlString>.IsEqual<CRQuoteStatusAttribute.quoteApproved>>>>>>>>);
    PXSelectBase<CRQuote> bqlStatement = (PXSelectBase<CRQuote>) new FbqlSelect<SelectFromBase<CRQuote, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<CRContact>.On<BqlOperand<CRContact.contactID, IBqlInt>.IsEqual<CRQuote.opportunityContactID>>>, FbqlJoins.Left<BAccount>.On<BqlOperand<BAccount.bAccountID, IBqlInt>.IsEqual<CRQuote.bAccountID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Match<BAccount, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>, And<BqlOperand<CRQuote.quoteType, IBqlString>.IsEqual<CRQuoteTypeAttribute.distribution>>>, And<BqlOperand<CRQuote.documentDate, IBqlDateTime>.IsLessEqual<BqlField<CRQuoteProcessFilter.endDate, IBqlDateTime>.FromCurrent>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CRQuoteProcessFilter.startDate>, IsNull>>>>.Or<BqlOperand<CRQuote.documentDate, IBqlDateTime>.IsGreaterEqual<BqlField<CRQuoteProcessFilter.startDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CRQuoteProcessFilter.bAccountID>, IsNull>>>>.Or<BqlOperand<Current<CRQuoteProcessFilter.bAccountID>, IBqlInt>.IsEqual<CRQuote.bAccountID>>>>, CRQuote>.View((PXGraph) this);
    if (((PXSelectBase<CRQuoteProcessFilter>) this.Filter).Current.Action == "CR304500$sendQuote")
      bqlStatement.WhereAnd(type2);
    if (((PXSelectBase<CRQuoteProcessFilter>) this.Filter).Current.Action == "CR304500$printQuote")
      bqlStatement.WhereAnd(type3);
    bqlStatement.WhereAnd(type1);
    return bqlStatement;
  }

  [PXBool]
  [PXUIField(DisplayName = "Primary", Enabled = false)]
  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.CR.Standalone.CRQuote.quoteID, Equal<PX.Objects.CR.Standalone.CROpportunity.defQuoteID>>, True>, False>), typeof (bool))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRQuote.isPrimary> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<CRQuoteProcessFilter> e)
  {
    if (string.IsNullOrEmpty(e.Row?.Action))
      return;
    ((PXProcessingBase<CRQuote>) this.Records).SetProcessWorkflowAction(e.Row.Action, ((PXSelectBase) this.Filter).Cache.ToDictionary((object) e.Row));
    bool showPrintSettings = this.IsPrintingAllowed(e.Row);
    bool usingEmailAction = e.Row.Action == "CR304500$sendQuote";
    bool? aggregateEmails = (bool?) e.Row?.AggregateEmails;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRQuoteProcessFilter>>) e).Cache, (object) e.Row).For<CRQuoteProcessFilter.printWithDeviceHub>((Action<PXUIFieldAttribute>) (ui => ui.Visible = showPrintSettings));
    chained = chained.For<CRQuoteProcessFilter.definePrinterManually>((Action<PXUIFieldAttribute>) (ui =>
    {
      ui.Visible = showPrintSettings;
      if (PXContext.GetSlot<AUSchedule>() != null)
        return;
      ui.Enabled = e.Row.PrintWithDeviceHub.GetValueOrDefault();
    }));
    chained = chained.SameFor<CRQuoteProcessFilter.numberOfCopies>();
    chained = chained.For<CRQuoteProcessFilter.printerID>((Action<PXUIFieldAttribute>) (ui =>
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

  protected virtual void _(PX.Data.Events.RowUpdated<CRQuoteProcessFilter> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<CRQuoteProcessFilter>>) e).Cache.ObjectsEqual<CRQuoteProcessFilter.action, CRQuoteProcessFilter.definePrinterManually, CRQuoteProcessFilter.printWithDeviceHub>((object) e.Row, (object) e.OldRow) || !PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() || ((PXSelectBase<CRQuoteProcessFilter>) this.Filter).Current == null || !((PXSelectBase<CRQuoteProcessFilter>) this.Filter).Current.PrintWithDeviceHub.GetValueOrDefault() || !((PXSelectBase<CRQuoteProcessFilter>) this.Filter).Current.DefinePrinterManually.GetValueOrDefault() || PXContext.GetSlot<AUSchedule>() != null && ((PXSelectBase<CRQuoteProcessFilter>) this.Filter).Current.PrinterID.HasValue && !e.OldRow.PrinterID.HasValue)
      return;
    ((PXSelectBase<CRQuoteProcessFilter>) this.Filter).Current.PrinterID = new NotificationUtility((PXGraph) this).SearchPrinter("BAccount", "CR604500", ((PXGraph) this).Accessinfo.BranchID);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<CRQuoteProcessFilter, CRQuoteProcessFilter.printerID> e)
  {
    if (e.Row == null || this.IsPrintingAllowed(e.Row))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CRQuoteProcessFilter, CRQuoteProcessFilter.printerID>, CRQuoteProcessFilter, object>) e).NewValue = (object) null;
  }

  public virtual bool IsPrintingAllowed(CRQuoteProcessFilter filter)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() && filter.Action == "CR304500$printQuote";
  }

  [PXEditDetailButton]
  [PXUIField]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<CRQuote>) this.Records).Current != null)
    {
      QuoteMaint instance = PXGraph.CreateInstance<QuoteMaint>();
      ((PXSelectBase<CRQuote>) instance.Quote).Current = CRQuote.PK.Find((PXGraph) instance, ((PXSelectBase<CRQuote>) this.Records).Current.OpportunityID, ((PXSelectBase<CRQuote>) this.Records).Current.QuoteNbr);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Quote");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  public class WellKnownActions
  {
    public class CRQuoteScreen
    {
      public const string ScreenID = "CR304500";
      public const string PrintQuote = "CR304500$printQuote";
      public const string EmailQuote = "CR304500$sendQuote";

      public class printQuote : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        CRQuoteProcess.WellKnownActions.CRQuoteScreen.printQuote>
      {
        public printQuote()
          : base("CR304500$printQuote")
        {
        }
      }

      public class emailQuote : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        CRQuoteProcess.WellKnownActions.CRQuoteScreen.emailQuote>
      {
        public emailQuote()
          : base("CR304500$sendQuote")
        {
        }
      }
    }
  }
}

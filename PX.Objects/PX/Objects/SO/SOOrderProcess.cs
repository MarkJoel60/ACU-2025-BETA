// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.AR.MigrationMode;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.SO;

public class SOOrderProcess : PXGraph<
#nullable disable
SOOrderProcess>
{
  public PXCancel<SOProcessFilter> Cancel;
  public PXAction<SOProcessFilter> viewDocument;
  public PXFilter<SOProcessFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public SOEmailProcessing Records;

  public SOOrderProcess() => ARSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);

  public virtual void _(PX.Data.Events.RowSelected<SOProcessFilter> e)
  {
    if (string.IsNullOrEmpty(e.Row?.Action))
      return;
    ((PXProcessingBase<SOOrderProcessSelected>) this.Records).SetProcessWorkflowAction(e.Row.Action, ((PXSelectBase) this.Filter).Cache.ToDictionary((object) e.Row));
    bool showPrintSettings = this.IsPrintingAllowed(e.Row);
    bool usingEmailAction = EnumerableExtensions.IsIn<string>(e.Row.Action, "SO301000$emailSalesOrder", "SO301000$emailQuote");
    bool? aggregateEmails = (bool?) e.Row?.AggregateEmails;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOProcessFilter>>) e).Cache, (object) e.Row).For<SOProcessFilter.printWithDeviceHub>((Action<PXUIFieldAttribute>) (ui => ui.Visible = showPrintSettings));
    chained = chained.For<SOProcessFilter.definePrinterManually>((Action<PXUIFieldAttribute>) (ui =>
    {
      ui.Visible = showPrintSettings;
      if (PXContext.GetSlot<AUSchedule>() != null)
        return;
      ui.Enabled = e.Row.PrintWithDeviceHub.GetValueOrDefault();
    }));
    chained = chained.SameFor<SOProcessFilter.numberOfCopies>();
    chained = chained.For<SOProcessFilter.printerID>((Action<PXUIFieldAttribute>) (ui =>
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

  public virtual bool IsPrintingAllowed(SOProcessFilter filter)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() && EnumerableExtensions.IsIn<string>(filter.Action, "SO301000$printSalesOrder", "SO301000$printQuote", "SO301000$printBlanket");
  }

  [PXEditDetailButton]
  [PXUIField]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<SOOrderProcessSelected>) this.Records).Current != null)
    {
      SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
      ((PXSelectBase<SOOrder>) instance.Document).Current = PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) instance.Document).Search<SOOrder.orderNbr>((object) ((PXSelectBase<SOOrderProcessSelected>) this.Records).Current.OrderNbr, new object[1]
      {
        (object) ((PXSelectBase<SOOrderProcessSelected>) this.Records).Current.OrderType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Order");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  public virtual void _(PX.Data.Events.RowUpdated<SOProcessFilter> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOProcessFilter>>) e).Cache.ObjectsEqual<SOProcessFilter.action, SOProcessFilter.definePrinterManually, SOProcessFilter.printWithDeviceHub>((object) e.Row, (object) e.OldRow) || !PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() || ((PXSelectBase<SOProcessFilter>) this.Filter).Current == null || !((PXSelectBase<SOProcessFilter>) this.Filter).Current.PrintWithDeviceHub.GetValueOrDefault() || !((PXSelectBase<SOProcessFilter>) this.Filter).Current.DefinePrinterManually.GetValueOrDefault() || PXContext.GetSlot<AUSchedule>() != null && ((PXSelectBase<SOProcessFilter>) this.Filter).Current.PrinterID.HasValue && !e.OldRow.PrinterID.HasValue)
      return;
    ((PXSelectBase<SOProcessFilter>) this.Filter).Current.PrinterID = new NotificationUtility((PXGraph) this).SearchPrinter("Customer", "SO641010", ((PXGraph) this).Accessinfo.BranchID);
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<SOProcessFilter, SOProcessFilter.printerID> e)
  {
    if (e.Row == null || this.IsPrintingAllowed(e.Row))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOProcessFilter, SOProcessFilter.printerID>, SOProcessFilter, object>) e).NewValue = (object) null;
  }

  public class WellKnownActions
  {
    public class SOOrderScreen
    {
      public const string ScreenID = "SO301000";
      public const string PrintSalesOrder = "SO301000$printSalesOrder";
      public const string PrintQuote = "SO301000$printQuote";
      public const string PrintBlanket = "SO301000$printBlanket";
      public const string EmailSalesOrder = "SO301000$emailSalesOrder";
      public const string EmailQuote = "SO301000$emailQuote";
      public const string EmailBlanket = "SO301000$emailBlanket";

      public class printSalesOrder : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        SOOrderProcess.WellKnownActions.SOOrderScreen.printSalesOrder>
      {
        public printSalesOrder()
          : base("SO301000$printSalesOrder")
        {
        }
      }

      public class printQuote : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        SOOrderProcess.WellKnownActions.SOOrderScreen.printQuote>
      {
        public printQuote()
          : base("SO301000$printQuote")
        {
        }
      }

      public class printBlanket : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        SOOrderProcess.WellKnownActions.SOOrderScreen.printBlanket>
      {
        public printBlanket()
          : base("SO301000$printBlanket")
        {
        }
      }

      public class emailSalesOrder : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        SOOrderProcess.WellKnownActions.SOOrderScreen.emailSalesOrder>
      {
        public emailSalesOrder()
          : base("SO301000$emailSalesOrder")
        {
        }
      }

      public class emailQuote : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        SOOrderProcess.WellKnownActions.SOOrderScreen.emailQuote>
      {
        public emailQuote()
          : base("SO301000$emailQuote")
        {
        }
      }

      public class emailBlanket : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        SOOrderProcess.WellKnownActions.SOOrderScreen.emailBlanket>
      {
        public emailBlanket()
          : base("SO301000$emailBlanket")
        {
        }
      }
    }
  }
}

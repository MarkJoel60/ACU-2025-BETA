// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POPrintOrder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP.MigrationMode;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.SM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.PO;

[TableAndChartDashboardType]
public class POPrintOrder : PXGraph<
#nullable disable
POPrintOrder>
{
  public PXFilter<POPrintOrderFilter> Filter;
  public PXSelect<PX.Objects.AP.Vendor> vendors;
  public PXSelect<PX.Objects.EP.EPEmployee> employees;
  public PXCancel<POPrintOrderFilter> Cancel;
  public PXAction<POPrintOrderFilter> details;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessingJoin<POPrintOrder.POPrintOrderOwned, POPrintOrderFilter, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<POOrder.vendorID>>, LeftJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.defContactID, Equal<POOrder.ownerID>>>>, Where<WhereWorkflowActionEnabled<POPrintOrder.POPrintOrderOwned, POPrintOrderFilter.action>>> Records;
  public PXSetup<PX.Objects.EP.EPSetup> EPSetup;

  public POPrintOrder()
  {
    APSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    PXUIFieldAttribute.SetRequired<POOrder.orderDate>(((PXSelectBase) this.Records).Cache, false);
    PXUIFieldAttribute.SetRequired<POOrder.curyID>(((PXSelectBase) this.Records).Cache, false);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Records).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<POOrder.selected>(((PXSelectBase) this.Records).Cache, (object) null, true);
    ((PXProcessingBase<POPrintOrder.POPrintOrderOwned>) this.Records).SetSelected<POOrder.selected>();
    ((PXProcessing<POPrintOrder.POPrintOrderOwned>) this.Records).SetProcessCaption("Process");
    ((PXProcessing<POPrintOrder.POPrintOrderOwned>) this.Records).SetProcessAllCaption("Process All");
    ((PXSelectBase) this.Records).Cache.AllowInsert = false;
    ((PXSelectBase) this.Records).Cache.AllowDelete = false;
  }

  public virtual IEnumerable records(PXAdapter adapter)
  {
    if (string.IsNullOrEmpty(((PXSelectBase<POPrintOrderFilter>) this.Filter).Current.Action) || ((PXSelectBase<POPrintOrderFilter>) this.Filter).Current.Action == "<SELECT>")
      return (IEnumerable) Array<object>.Empty;
    BqlCommand bqlSelect = ((PXSelectBase) this.Records).View.BqlSelect;
    return (IEnumerable) new PXView((PXGraph) this, false, !Str.Contains(((PXSelectBase<POPrintOrderFilter>) this.Filter).Current.Action, "email", StringComparison.InvariantCultureIgnoreCase) ? bqlSelect.WhereAnd<Where<POOrder.hold, Equal<False>, And<POOrder.dontPrint, Equal<False>, And<POOrder.printed, NotEqual<True>>>>>() : bqlSelect.WhereAnd<Where<POOrder.hold, Equal<False>, And<POOrder.dontEmail, Equal<False>, And<POOrder.emailed, NotEqual<True>>>>>()).SelectMulti(Array.Empty<object>());
  }

  public virtual bool IsDirty => false;

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable Details(PXAdapter adapter)
  {
    if (((PXSelectBase<POPrintOrder.POPrintOrderOwned>) this.Records).Current != null && ((PXSelectBase<POPrintOrderFilter>) this.Filter).Current != null)
    {
      POOrderEntry instance = PXGraph.CreateInstance<POOrderEntry>();
      ((PXSelectBase<POOrder>) instance.Document).Current = (POOrder) ((PXSelectBase<POPrintOrder.POPrintOrderOwned>) this.Records).Current;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View PO");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  protected virtual void POPrintOrderFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    POPrintOrderFilter row = (POPrintOrderFilter) e.Row;
    PXCache pxCache1 = sender;
    POPrintOrderFilter printOrderFilter1 = row;
    bool? nullable;
    int num1;
    if (row != null)
    {
      nullable = row.MyOwner;
      bool flag = false;
      num1 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 1;
    PXUIFieldAttribute.SetEnabled<POPrintOrderFilter.ownerID>(pxCache1, (object) printOrderFilter1, num1 != 0);
    PXCache pxCache2 = sender;
    POPrintOrderFilter printOrderFilter2 = row;
    int num2;
    if (row != null)
    {
      nullable = row.MyWorkGroup;
      bool flag = false;
      num2 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    else
      num2 = 1;
    PXUIFieldAttribute.SetEnabled<POPrintOrderFilter.workGroupID>(pxCache2, (object) printOrderFilter2, num2 != 0);
    if (row == null || string.IsNullOrEmpty(row.Action))
      return;
    this.SetProcessTarget(row);
    bool flag1 = this.IsPrintingAllowed(row);
    PXUIFieldAttribute.SetVisible<POPrintOrderFilter.printWithDeviceHub>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<POPrintOrderFilter.definePrinterManually>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<POPrintOrderFilter.printerID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<POPrintOrderFilter.numberOfCopies>(sender, (object) row, flag1);
    if (PXContext.GetSlot<AUSchedule>() == null)
    {
      PXCache pxCache3 = sender;
      POPrintOrderFilter printOrderFilter3 = row;
      nullable = row.PrintWithDeviceHub;
      int num3 = nullable.GetValueOrDefault() ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<POPrintOrderFilter.definePrinterManually>(pxCache3, (object) printOrderFilter3, num3 != 0);
      PXCache pxCache4 = sender;
      POPrintOrderFilter printOrderFilter4 = row;
      nullable = row.PrintWithDeviceHub;
      int num4 = nullable.GetValueOrDefault() ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<POPrintOrderFilter.numberOfCopies>(pxCache4, (object) printOrderFilter4, num4 != 0);
      PXCache pxCache5 = sender;
      POPrintOrderFilter printOrderFilter5 = row;
      nullable = row.PrintWithDeviceHub;
      int num5;
      if (nullable.GetValueOrDefault())
      {
        nullable = row.DefinePrinterManually;
        num5 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num5 = 0;
      PXUIFieldAttribute.SetEnabled<POPrintOrderFilter.printerID>(pxCache5, (object) printOrderFilter5, num5 != 0);
    }
    nullable = row.PrintWithDeviceHub;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.DefinePrinterManually;
      if (nullable.GetValueOrDefault())
        return;
    }
    row.PrinterID = new Guid?();
  }

  public virtual bool IsPrintingAllowed(POPrintOrderFilter filter)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() && filter?.Action == "PO301000$printPurchaseOrder";
  }

  public virtual void POPrintOrderFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<POPrintOrderFilter.action>(e.Row, e.OldRow) && sender.ObjectsEqual<POPrintOrderFilter.definePrinterManually>(e.Row, e.OldRow) && sender.ObjectsEqual<POPrintOrderFilter.printWithDeviceHub>(e.Row, e.OldRow) || ((PXSelectBase<POPrintOrderFilter>) this.Filter).Current == null || !PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() || !((PXSelectBase<POPrintOrderFilter>) this.Filter).Current.PrintWithDeviceHub.GetValueOrDefault() || !((PXSelectBase<POPrintOrderFilter>) this.Filter).Current.DefinePrinterManually.GetValueOrDefault() || PXContext.GetSlot<AUSchedule>() != null && ((PXSelectBase<POPrintOrderFilter>) this.Filter).Current.PrinterID.HasValue && !((POPrintOrderFilter) e.OldRow).PrinterID.HasValue)
      return;
    ((PXSelectBase<POPrintOrderFilter>) this.Filter).Current.PrinterID = new NotificationUtility((PXGraph) this).SearchPrinter("Vendor", "PO641000", ((PXGraph) this).Accessinfo.BranchID);
  }

  protected virtual void POPrintOrderOwned_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    sender.IsDirty = false;
  }

  protected virtual void POPrintOrderFilter_PrinterName_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    POPrintOrderFilter row = (POPrintOrderFilter) e.Row;
    if (row == null || this.IsPrintingAllowed(row))
      return;
    e.NewValue = (object) null;
  }

  protected virtual void SetProcessTarget(POPrintOrderFilter orderFilter)
  {
    Dictionary<string, object> dictionary = ((PXSelectBase) this.Filter).Cache.ToDictionary((object) orderFilter);
    ((PXProcessingBase<POPrintOrder.POPrintOrderOwned>) this.Records).SetProcessWorkflowAction(orderFilter.Action, dictionary);
  }

  [PXProjection(typeof (Select5<POOrder, LeftJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.defContactID, Equal<POOrder.ownerID>>>, Where2<Where<CurrentValue<POPrintOrderFilter.ownerID>, IsNull, Or<CurrentValue<POPrintOrderFilter.ownerID>, Equal<PX.Objects.EP.EPEmployee.defContactID>>>, And2<Where<CurrentValue<POPrintOrderFilter.workGroupID>, IsNull, Or<CurrentValue<POPrintOrderFilter.workGroupID>, Equal<POOrder.ownerWorkgroupID>>>, And2<Where<CurrentValue<POPrintOrderFilter.myWorkGroup>, Equal<boolFalse>, Or<POOrder.ownerWorkgroupID, IsWorkgroupOfContact<CurrentValue<POPrintOrderFilter.currentOwnerID>>>>, And<Where<POOrder.ownerWorkgroupID, IsNull, Or<POOrder.ownerWorkgroupID, IsWorkgroupOrSubgroupOfContact<CurrentValue<POPrintOrderFilter.currentOwnerID>>>>>>>>, Aggregate<GroupBy<POOrder.orderNbr, GroupBy<POOrder.hold, GroupBy<POOrder.approved, GroupBy<POOrder.emailed, GroupBy<POOrder.dontEmail, GroupBy<POOrder.cancelled, GroupBy<POOrder.isUnbilledTaxValid, GroupBy<POOrder.isTaxValid, GroupBy<POOrder.ownerWorkgroupID, GroupBy<POOrder.createdByID, GroupBy<POOrder.lastModifiedByID, GroupBy<POOrder.dontPrint, GroupBy<POOrder.noteID, GroupBy<POOrder.printed>>>>>>>>>>>>>>>>))]
  [Serializable]
  public class POPrintOrderOwned : POOrder
  {
    public new abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POPrintOrder.POPrintOrderOwned.orderType>
    {
    }

    public new abstract class orderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POPrintOrder.POPrintOrderOwned.orderNbr>
    {
    }

    public new abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POPrintOrder.POPrintOrderOwned.status>
    {
    }

    public new abstract class hasMultipleProjects : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POPrintOrder.POPrintOrderOwned.hasMultipleProjects>
    {
    }
  }

  public class WellKnownActions
  {
    public class POOrderScreen
    {
      public const string ScreenID = "PO301000";
      public const string PrintPurchaseOrder = "PO301000$printPurchaseOrder";
      public const string EmailPurchaseOrder = "PO301000$emailPurchaseOrder";
      public const string MarkAsDontPrint = "PO301000$markAsDontPrint";
      public const string MarkAsDontEmail = "PO301000$markAsDontEmail";
    }
  }
}

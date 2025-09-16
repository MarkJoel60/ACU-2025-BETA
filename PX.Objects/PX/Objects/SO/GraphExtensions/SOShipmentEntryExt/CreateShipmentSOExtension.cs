// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.CreateShipmentSOExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO.Interfaces;
using PX.Objects.SO.Models;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

[PXProtectedAccess(typeof (SOOrderExtension))]
public abstract class CreateShipmentSOExtension : 
  PXGraphExtension<CreateShipmentExtension, SOOrderExtension, SOShipmentEntry>
{
  /// Uses <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.SOOrderExtension.ValidateLineType(PX.Objects.SO.SOLine,PX.Objects.IN.InventoryItem,System.String)" />
  /// .
  [PXProtectedAccess(null)]
  protected abstract void ValidateLineType(PX.Objects.SO.SOLine line, PX.Objects.IN.InventoryItem item, string message);

  /// Uses <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.SOOrderExtension.AllocateGroupFreeItems(PX.Objects.SO.SOOrder)" />
  /// .
  [PXProtectedAccess(null)]
  protected abstract void AllocateGroupFreeItems(PX.Objects.SO.SOOrder order);

  /// Uses <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.SOOrderExtension.AdjustFreeItemLines" />
  /// .
  [PXProtectedAccess(null)]
  protected abstract void AdjustFreeItemLines();

  /// Uses <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.SOOrderExtension.UpdateShipmentCntr(PX.Data.PXCache,PX.Objects.SO.SOOrderShipment,System.Nullable{System.Int16})" />
  /// .
  [PXProtectedAccess(null)]
  protected abstract void UpdateShipmentCntr(PXCache sender, PX.Objects.SO.SOOrderShipment row, short? counter);

  /// Extends <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.CreateShipmentExtension.ValidateCreateShipmentArgs(PX.Objects.SO.CreateShipmentArgs)" />
  /// .
  [PXOverride]
  public void ValidateCreateShipmentArgs(
    CreateShipmentArgs args,
    Action<CreateShipmentArgs> base_ValidateCreateShipmentArgs)
  {
    base_ValidateCreateShipmentArgs(args);
    if (args.Order == null)
      return;
    PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrderType>) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.soordertype).Select(new object[1]
    {
      (object) args.Order.OrderType
    }));
    bool? nullable;
    if (soOrderType != null)
    {
      CreateShipmentArgs createShipmentArgs1 = args;
      if (createShipmentArgs1.Operation == null)
      {
        string defaultOperation;
        createShipmentArgs1.Operation = defaultOperation = soOrderType.DefaultOperation;
      }
      CreateShipmentArgs createShipmentArgs2 = args;
      PXNoteAttribute.IPXCopySettings ipxCopySettings;
      if (createShipmentArgs2.CopyLineNotesAndFilesSettings == null)
        createShipmentArgs2.CopyLineNotesAndFilesSettings = ipxCopySettings = (PXNoteAttribute.IPXCopySettings) new CreateShipmentSOExtension.CopySettings(soOrderType.CopyLineFilesToShipment, soOrderType.CopyLineNotesToShipment);
      CreateShipmentArgs createShipmentArgs3 = args;
      if (createShipmentArgs3.CopyNotesAndFilesSettings == null)
      {
        CreateShipmentArgs createShipmentArgs4 = createShipmentArgs3;
        bool? headerFilesToShipment = soOrderType.CopyHeaderFilesToShipment;
        nullable = soOrderType.CopyHeaderNotesToShipment;
        bool? copyNotes = new bool?(nullable.GetValueOrDefault());
        CreateShipmentSOExtension.CopySettings copySettings;
        ipxCopySettings = (PXNoteAttribute.IPXCopySettings) (copySettings = new CreateShipmentSOExtension.CopySettings(headerFilesToShipment, copyNotes));
        createShipmentArgs4.CopyNotesAndFilesSettings = (PXNoteAttribute.IPXCopySettings) copySettings;
      }
      args.FilesAndNotesSource = (object) args.Order;
    }
    SOOrderTypeOperation orderTypeOperation = SOOrderTypeOperation.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, args.Order.OrderType, args.Operation);
    if (orderTypeOperation != null)
    {
      nullable = orderTypeOperation.Active;
      if (nullable.GetValueOrDefault() && string.IsNullOrEmpty(orderTypeOperation.ShipmentPlanType))
      {
        object stateExt = ((PXCache) GraphHelper.Caches<SOOrderTypeOperation>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base)).GetStateExt<SOOrderTypeOperation.operation>((object) orderTypeOperation);
        throw new PXException("For order type '{0}' and operation '{1}', no shipment plan type is specified on the Order Types (SO.20.20.00) form.", new object[2]
        {
          (object) args.Order.OrderType,
          stateExt
        });
      }
    }
    args.ShipmentType = INTranType.DocType(orderTypeOperation.INDocType);
    if (args.ShipmentList == null)
      return;
    bool? selected = args.Order.Selected;
    PX.Objects.SO.SOOrder soOrder = this.ActualizeAndValidateOrder(args.Graph, args.Order, args.Operation);
    PXCache<PX.Objects.SO.SOOrder>.RestoreCopy(args.Order, soOrder);
    args.Order.Selected = selected;
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.CreateShipmentExtension.GetShipmentDate(PX.Objects.SO.CreateShipmentArgs)" />
  /// .
  [PXOverride]
  public DateTime? GetShipmentDate(
    CreateShipmentArgs args,
    Func<CreateShipmentArgs, DateTime?> base_GetShipmentDate)
  {
    DateTime? shipmentDate = base_GetShipmentDate(args);
    if (!args.UseOptimalShipDate.GetValueOrDefault() || args.Order == null)
      return shipmentDate;
    PX.Objects.SO.SOOrder order = args.Order;
    PXResultset<SOShipmentPlan> pxResultset;
    if (!(order.ShipComplete == "B"))
      pxResultset = PXSelectBase<SOShipmentPlan, PXSelectJoinGroupBy<SOShipmentPlan, InnerJoin<PX.Objects.SO.SOLineSplit, On<PX.Objects.SO.SOLineSplit.planID, Equal<SOShipmentPlan.planID>>>, Where<SOShipmentPlan.siteID, Equal<Required<SOOrderFilter.siteID>>, And<SOShipmentPlan.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<SOShipmentPlan.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>, And<PX.Objects.SO.SOLineSplit.operation, Equal<Required<PX.Objects.SO.SOLineSplit.operation>>>>>>, Aggregate<Max<SOShipmentPlan.planDate>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[4]
      {
        (object) args.SiteID,
        (object) order.OrderType,
        (object) order.OrderNbr,
        (object) args.Operation
      });
    else
      pxResultset = PXSelectBase<SOShipmentPlan, PXSelectJoinGroupBy<SOShipmentPlan, InnerJoin<PX.Objects.SO.SOLineSplit, On<PX.Objects.SO.SOLineSplit.planID, Equal<SOShipmentPlan.planID>>>, Where<SOShipmentPlan.siteID, Equal<Required<SOOrderFilter.siteID>>, And<SOShipmentPlan.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<SOShipmentPlan.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>, And<PX.Objects.SO.SOLineSplit.operation, Equal<Required<PX.Objects.SO.SOLineSplit.operation>>>>>>, Aggregate<Min<SOShipmentPlan.planDate>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[4]
      {
        (object) args.SiteID,
        (object) order.OrderType,
        (object) order.OrderNbr,
        (object) args.Operation
      });
    SOShipmentPlan soShipmentPlan = PXResultset<SOShipmentPlan>.op_Implicit(pxResultset);
    DateTime? planDate = soShipmentPlan.PlanDate;
    DateTime? nullable = shipmentDate;
    return (planDate.HasValue & nullable.HasValue ? (planDate.GetValueOrDefault() > nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0 ? shipmentDate : soShipmentPlan.PlanDate;
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.CreateShipmentExtension.FindOrCreateShipment(PX.Objects.SO.CreateShipmentArgs)" />
  /// .
  [PXOverride]
  public PX.Objects.SO.SOShipment FindOrCreateShipment(
    CreateShipmentArgs args,
    Func<CreateShipmentArgs, PX.Objects.SO.SOShipment> base_FindOrCreateShipment)
  {
    PX.Objects.SO.SOOrder order = args.Order;
    if ((order != null ? (order.ShipSeparately.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return base_FindOrCreateShipment(args);
    return new PX.Objects.SO.SOShipment() { Hidden = new bool?(true) };
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.CreateShipmentExtension.GetShipmentFieldLookups(PX.Objects.SO.CreateShipmentArgs)" />
  /// .
  [PXOverride]
  public FieldLookup[] GetShipmentFieldLookups(
    CreateShipmentArgs args,
    Func<CreateShipmentArgs, FieldLookup[]> base_GetShipmentFieldLookups)
  {
    FieldLookup[] collection = base_GetShipmentFieldLookups(args);
    PX.Objects.SO.SOOrder order = args.Order;
    if (order == null)
      return collection;
    return new List<FieldLookup>((IEnumerable<FieldLookup>) collection)
    {
      (FieldLookup) new FieldLookup<PX.Objects.SO.SOShipment.customerID>((object) order.CustomerID),
      (FieldLookup) new FieldLookup<PX.Objects.SO.SOShipment.shipAddressID>((object) order.ShipAddressID),
      (FieldLookup) new FieldLookup<PX.Objects.SO.SOShipment.shipContactID>((object) order.ShipContactID),
      (FieldLookup) new FieldLookup<PX.Objects.SO.SOShipment.fOBPoint>((object) order.FOBPoint),
      (FieldLookup) new FieldLookup<PX.Objects.SO.SOShipment.shipVia>((object) order.ShipVia),
      (FieldLookup) new FieldLookup<PX.Objects.SO.SOShipment.shipTermsID>((object) order.ShipTermsID),
      (FieldLookup) new FieldLookup<PX.Objects.SO.SOShipment.shipZoneID>((object) order.ShipZoneID),
      (FieldLookup) new FieldLookup<PX.Objects.SO.SOShipment.createARDoc>((object) (order.ARDocType != "UND")),
      (FieldLookup) new FieldLookup<PX.Objects.SO.SOShipment.useCustomerAccount>((object) order.UseCustomerAccount),
      (FieldLookup) new FieldLookup<PX.Objects.SO.SOShipment.freightAmountSource>((object) order.FreightAmountSource),
      (FieldLookup) new FieldLookup<PX.Objects.SO.SOShipment.isManualPackage>((object) order.IsManualPackage)
    }.ToArray();
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.CreateShipmentExtension.SetShipmentFieldsFromOrigDocument(PX.Objects.SO.SOShipment,PX.Objects.SO.CreateShipmentArgs,System.Boolean)" />
  /// .
  [PXOverride]
  public bool SetShipmentFieldsFromOrigDocument(
    PX.Objects.SO.SOShipment shipment,
    CreateShipmentArgs args,
    bool newlyCreated,
    Func<PX.Objects.SO.SOShipment, CreateShipmentArgs, bool, bool> base_SetShipmentFieldsFromOrigDocument)
  {
    bool flag = base_SetShipmentFieldsFromOrigDocument(shipment, args, newlyCreated);
    PX.Objects.SO.SOOrder order = args.Order;
    if (order == null)
      return flag;
    if (newlyCreated)
    {
      shipment.CustomerID = order.CustomerID;
      shipment.CustomerLocationID = order.CustomerLocationID;
      shipment.UseCustomerAccount = order.UseCustomerAccount;
      shipment.CustomerOrderNbr = order.CustomerOrderNbr;
      shipment.Resedential = order.Resedential;
      shipment.SaturdayDelivery = order.SaturdayDelivery;
      shipment.Insurance = order.Insurance;
      shipment.GroundCollect = order.GroundCollect;
      shipment.TaxCategoryID = order.FreightTaxCategoryID;
      shipment.DestinationSiteID = order.DestinationSiteID;
      shipment.FreightAmountSource = order.FreightAmountSource;
      shipment.IsManualPackage = order.IsManualPackage;
      if (order.FOBPoint != null && (shipment.FOBPoint == null || !((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).IsContractBasedAPI))
        shipment.FOBPoint = order.FOBPoint;
      if (order.ShipTermsID != null && (shipment.ShipTermsID == null || !((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).IsContractBasedAPI))
        shipment.ShipTermsID = order.ShipTermsID;
      if (order.ShipVia != null && (shipment.ShipVia == null || !((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).IsContractBasedAPI))
        shipment.ShipVia = order.ShipVia;
      if (order.ShipZoneID != null && (shipment.ShipZoneID == null || !((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).IsContractBasedAPI))
        shipment.ShipZoneID = order.ShipZoneID;
      if (string.IsNullOrEmpty(shipment.ShipmentDesc))
        shipment.ShipmentDesc = order.OrderDesc;
      return true;
    }
    if (shipment.FreightAmountSource != order.FreightAmountSource)
      throw new PXException();
    if (((IEnumerable<PX.Objects.SO.SOOrderShipment>) ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.OrderList).Select<PX.Objects.SO.SOOrderShipment>(Array.Empty<object>())).Count<PX.Objects.SO.SOOrderShipment>() == 1)
    {
      if (args.MassProcess && args.ShipmentList != null)
      {
        shipment.ShipmentDesc = PXMessages.LocalizeNoPrefix("Multi-order shipment");
        flag = true;
      }
      PX.Objects.SO.SOOrder soOrder = PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>.op_Implicit((PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>) PXResultset<PX.Objects.SO.SOOrderShipment>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrderShipment>) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.OrderList).Select(Array.Empty<object>())));
      if ((soOrder.OrderNbr != order.OrderNbr || soOrder.OrderType != order.OrderType) && !string.IsNullOrEmpty(shipment.CustomerOrderNbr))
      {
        shipment.CustomerOrderNbr = (string) null;
        flag = true;
      }
    }
    return flag;
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.CreateShipmentExtension.SetShipAddressAndContactFromArgs(PX.Objects.SO.SOShipment,PX.Objects.SO.CreateShipmentArgs)" />
  /// .
  [PXOverride]
  public void SetShipAddressAndContactFromArgs(
    PX.Objects.SO.SOShipment shipment,
    CreateShipmentArgs args,
    Action<PX.Objects.SO.SOShipment, CreateShipmentArgs> base_SetShipAddressAndContactFromArgs)
  {
    base_SetShipAddressAndContactFromArgs(shipment, args);
    if (args.Order == null)
      return;
    this.SetShipAddressAndContact(shipment, args.Order.ShipAddressID, args.Order.ShipContactID);
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.CreateShipmentExtension.CreateShipmentDetails(PX.Objects.SO.CreateShipmentArgs)" />
  /// .
  [PXOverride]
  public void CreateShipmentDetails(
    CreateShipmentArgs args,
    Action<CreateShipmentArgs> base_CreateShipmentDetails)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CreateShipmentSOExtension.\u003C\u003Ec__DisplayClass10_0 cDisplayClass100 = new CreateShipmentSOExtension.\u003C\u003Ec__DisplayClass10_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass100.\u003C\u003E4__this = this;
    base_CreateShipmentDetails(args);
    PX.Objects.SO.SOOrder order = args.Order;
    if (order == null)
      return;
    int? openShipmentCntr = order.OpenShipmentCntr;
    int num1 = 0;
    if (openShipmentCntr.GetValueOrDefault() > num1 & openShipmentCntr.HasValue)
    {
      PX.Objects.SO.SOOrderShipment soOrderShipment = PXResultset<PX.Objects.SO.SOOrderShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrderShipment, PXSelectReadonly<PX.Objects.SO.SOOrderShipment, Where<PX.Objects.SO.SOOrderShipment.orderType, Equal<Required<PX.Objects.SO.SOOrderShipment.orderType>>, And<PX.Objects.SO.SOOrderShipment.orderNbr, Equal<Required<PX.Objects.SO.SOOrderShipment.orderNbr>>, And<PX.Objects.SO.SOOrderShipment.siteID, Equal<Required<PX.Objects.SO.SOOrderShipment.siteID>>, And<PX.Objects.SO.SOOrderShipment.shipmentNbr, NotEqual<Required<PX.Objects.SO.SOOrderShipment.shipmentNbr>>, And<PX.Objects.SO.SOOrderShipment.confirmed, Equal<boolFalse>>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[4]
      {
        (object) order.OrderType,
        (object) order.OrderNbr,
        (object) args.SiteID,
        (object) ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.ShipmentNbr
      }));
      if (soOrderShipment != null)
        throw new PXException("New shipment cannot be created for the sales order. The {0} {1} sales order already has the {2} open shipment.", new object[3]
        {
          (object) order.OrderType,
          (object) order.OrderNbr,
          (object) soOrderShipment.ShipmentNbr
        });
    }
    PX.Objects.SO.SOOrderShipment soOrderShipment1 = new PX.Objects.SO.SOOrderShipment()
    {
      OrderType = order.OrderType,
      OrderNbr = order.OrderNbr,
      OrderNoteID = order.NoteID,
      ShipmentNbr = ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.ShipmentNbr,
      ShipmentType = ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.ShipmentType,
      ShippingRefNoteID = ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.NoteID,
      Operation = ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.Operation,
      ProjectID = order.ProjectID
    };
    GraphHelper.Hold(((PXSelectBase) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.soorder).Cache, (object) order);
    PXParentAttribute.SetParent(((PXSelectBase) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.OrderList).Cache, (object) soOrderShipment1, typeof (PX.Objects.SO.SOOrder), (object) order);
    ((IEnumerable<PXResult<PX.Objects.SO.SOOrderShipment>>) ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.OrderListSimple).Select(Array.Empty<object>())).ToList<PXResult<PX.Objects.SO.SOOrderShipment>>();
    PX.Objects.SO.SOOrderShipment soOrderShipment2 = ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.OrderList).Locate(soOrderShipment1);
    PX.Objects.SO.SOOrderShipment self = soOrderShipment2 == null || EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.OrderList).Cache.GetStatus((object) soOrderShipment2), (PXEntryStatus) 3, (PXEntryStatus) 4) ? ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.OrderList).Insert(soOrderShipment2 ?? soOrderShipment1) : soOrderShipment2;
    // ISSUE: method pointer
    ((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).RowDeleting.AddHandler<PX.Objects.SO.SOOrderShipment>(new PXRowDeleting((object) null, __methodptr(\u003CCreateShipmentDetails\u003Eg__SOOrderShipment_RowDeleting\u007C10_0)));
    // ISSUE: reference to a compiler-generated field
    cDisplayClass100.anyDeleted = false;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass100.lineships = new Dictionary<SOLine2, SOShipmentEntry.LineShipment>();
    // ISSUE: method pointer
    ((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).RowDeleted.AddHandler<SOShipLine>(new PXRowDeleted((object) cDisplayClass100, __methodptr(\u003CCreateShipmentDetails\u003Eg__SOShipLine_RowDeleted\u007C1)));
    foreach (PXResult<SOLine2> pxResult in PXSelectBase<SOLine2, PXSelect<SOLine2, Where<SOLine2.orderType, Equal<Required<SOLine2.orderType>>, And<SOLine2.orderNbr, Equal<Required<SOLine2.orderNbr>>, And<SOLine2.siteID, Equal<Required<SOLine2.siteID>>, And<SOLine2.operation, Equal<Required<SOLine2.operation>>, And<SOLine2.completed, NotEqual<True>>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[4]
    {
      (object) order.OrderType,
      (object) order.OrderNbr,
      (object) args.SiteID,
      (object) args.Operation
    }))
      PXParentAttribute.SetParent(((PXSelectBase) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.soline).Cache, (object) PXResult<SOLine2>.op_Implicit(pxResult), typeof (PX.Objects.SO.SOOrder), (object) order);
    foreach (PXResult<SOLineSplit2> pxResult in PXSelectBase<SOLineSplit2, PXSelect<SOLineSplit2, Where<SOLineSplit2.orderType, Equal<Required<SOLineSplit2.orderType>>, And<SOLineSplit2.orderNbr, Equal<Required<SOLineSplit2.orderNbr>>, And<SOLineSplit2.siteID, Equal<Required<SOLineSplit2.siteID>>, And<SOLineSplit2.operation, Equal<Required<SOLineSplit2.operation>>, And<SOLineSplit2.completed, NotEqual<True>>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[4]
    {
      (object) order.OrderType,
      (object) order.OrderNbr,
      (object) args.SiteID,
      (object) args.Operation
    }))
      PXResult<SOLineSplit2>.op_Implicit(pxResult);
    foreach (PXResult<SOShipLine> pxResult in PXSelectBase<SOShipLine, PXSelect<SOShipLine, Where<SOShipLine.shipmentType, Equal<Current<SOShipLine.shipmentType>>, And<SOShipLine.shipmentNbr, Equal<Current<SOShipLine.shipmentNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, Array.Empty<object>()))
      PXParentAttribute.SetParent(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache, (object) PXResult<SOShipLine>.op_Implicit(pxResult), typeof (PX.Objects.SO.SOOrder), (object) order);
    bool flag1 = false;
    using (new SOOrderExtension.SkipAdjustFreeItemLinesScope(((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1))
    {
      List<CreateShipmentSOExtension.ShipmentSchedule> shipmentScheduleList = new List<CreateShipmentSOExtension.ShipmentSchedule>();
      foreach (PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite> pxResult in ((PXSelectBase<SOShipmentPlan>) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.ShipmentScheduleSelect).Select(new object[7]
      {
        (object) args.SiteID,
        (object) args.EndDate,
        (object) order.OrderType,
        (object) order.OrderNbr,
        (object) args.OrderLineNbr,
        (object) args.OrderLineNbr,
        (object) args.Operation
      }))
      {
        SOShipmentPlan soShipmentPlan = PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(pxResult);
        PX.Objects.SO.SOLineSplit soLineSplit = PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(pxResult);
        bool? nullable1 = soShipmentPlan.RequireAllocation;
        if (nullable1.GetValueOrDefault() && soLineSplit.LineType != "GN" && soLineSplit.Operation != "R")
        {
          short? inclQtySoShipping = soShipmentPlan.InclQtySOShipping;
          int? nullable2;
          int? nullable3;
          if (!inclQtySoShipping.HasValue)
          {
            nullable2 = new int?();
            nullable3 = nullable2;
          }
          else
            nullable3 = new int?((int) inclQtySoShipping.GetValueOrDefault());
          nullable2 = nullable3;
          if (nullable2.GetValueOrDefault() != 1)
          {
            short? inclQtySoShipped = soShipmentPlan.InclQtySOShipped;
            int? nullable4;
            if (!inclQtySoShipped.HasValue)
            {
              nullable2 = new int?();
              nullable4 = nullable2;
            }
            else
              nullable4 = new int?((int) inclQtySoShipped.GetValueOrDefault());
            nullable2 = nullable4;
            if (nullable2.GetValueOrDefault() != 1)
            {
              flag1 = true;
              nullable1 = ((PXSelectBase<SOSetup>) ((PXGraphExtension<SOShipmentEntry>) this).Base.sosetup).Current.AddAllToShipment;
              if (!nullable1.GetValueOrDefault())
                continue;
            }
          }
        }
        shipmentScheduleList.Add(new CreateShipmentSOExtension.ShipmentSchedule(new PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>(soShipmentPlan, soLineSplit, PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(pxResult), PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(pxResult), PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(pxResult), PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(pxResult)), new SOShipLine()
        {
          OrigSplitLineNbr = soLineSplit.SplitLineNbr
        }));
      }
      shipmentScheduleList.Sort();
      foreach (CreateShipmentSOExtension.ShipmentSchedule shipmentSchedule in shipmentScheduleList)
      {
        shipmentSchedule.ShipLine.ShipmentType = ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.ShipmentType;
        shipmentSchedule.ShipLine.ShipmentNbr = ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.ShipmentNbr;
        shipmentSchedule.ShipLine.LineNbr = (int?) PXLineNbrAttribute.NewLineNbr<SOShipLine.lineNbr>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache, (object) ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current);
        PXParentAttribute.SetParent(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache, (object) shipmentSchedule.ShipLine, typeof (PX.Objects.SO.SOOrder), (object) order);
        PX.Objects.SO.SOLine soLine = PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(shipmentSchedule.Result);
        PX.Objects.SO.SOLineSplit soLineSplit = PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(shipmentSchedule.Result);
        SOLine2 soLine2 = ((PXSelectBase<SOLine2>) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.soline).Locate(new SOLine2()
        {
          OrderType = soLine.OrderType,
          OrderNbr = soLine.OrderNbr,
          LineNbr = soLine.LineNbr
        });
        if (soLine2 != null)
          PXParentAttribute.SetParent(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache, (object) shipmentSchedule.ShipLine, typeof (SOLine2), (object) soLine2);
        else if (soLine.Completed.GetValueOrDefault() && !soLineSplit.Completed.GetValueOrDefault())
          throw new PXException("The shipment cannot be created because the {0} order has the completed line {1} with item {2} and incomplete splits in the Line Details dialog box. Change the order status to On Hold and clear the Completed check box in line {1}.", new object[3]
          {
            (object) soLine.OrderNbr,
            (object) soLine.LineNbr,
            (object) PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(shipmentSchedule.Result).InventoryCD
          });
        // ISSUE: reference to a compiler-generated field
        SOShipmentEntry.LineShipment lineShipment = EnumerableEx.Ensure<SOLine2, SOShipmentEntry.LineShipment>((IDictionary<SOLine2, SOShipmentEntry.LineShipment>) cDisplayClass100.lineships, soLine2, (Func<SOShipmentEntry.LineShipment>) (() => new SOShipmentEntry.LineShipment()));
        lineShipment.Add(shipmentSchedule.ShipLine);
        SOLineSplit2 soLineSplit2 = ((PXSelectBase<SOLineSplit2>) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.solinesplit).Locate(new SOLineSplit2()
        {
          OrderType = soLineSplit.OrderType,
          OrderNbr = soLineSplit.OrderNbr,
          LineNbr = soLineSplit.LineNbr,
          SplitLineNbr = soLineSplit.SplitLineNbr
        });
        if (soLineSplit2 != null)
          PXParentAttribute.SetParent(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache, (object) shipmentSchedule.ShipLine, typeof (SOLineSplit2), (object) soLineSplit2);
        PXParentAttribute.SetParent(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache, (object) shipmentSchedule.ShipLine, typeof (PX.Objects.SO.SOOrderShipment), (object) self);
        if (args.ShipmentList == null || soLine2.ShipComplete != "C" || !lineShipment.AnyDeleted)
          this.Base2.CreateShipmentFromSchedules(args, (IShipLineSource) new SOLineShipLineSource(shipmentSchedule.Result), shipmentSchedule.ShipLine);
        if (args.ShipmentList != null && soLine2.ShipComplete == "C" && lineShipment.AnyDeleted)
        {
          foreach (SOShipLine soShipLine in lineShipment)
            ((PXSelectBase<SOShipLine>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Delete(soShipLine);
          lineShipment.Clear();
        }
      }
      // ISSUE: reference to a compiler-generated field
      foreach (KeyValuePair<SOLine2, SOShipmentEntry.LineShipment> lineship in cDisplayClass100.lineships)
      {
        if (lineship.Key.ShipComplete == "C")
        {
          Decimal? shippedQty = lineship.Key.ShippedQty;
          Decimal? orderQty = lineship.Key.OrderQty;
          Decimal? completeQtyMin = lineship.Key.CompleteQtyMin;
          Decimal? nullable = orderQty.HasValue & completeQtyMin.HasValue ? new Decimal?(orderQty.GetValueOrDefault() * completeQtyMin.GetValueOrDefault() / 100M) : new Decimal?();
          if (shippedQty.GetValueOrDefault() < nullable.GetValueOrDefault() & shippedQty.HasValue & nullable.HasValue)
          {
            foreach (SOShipLine shipline in lineship.Value)
              this.Base2.RemoveLineFromShipment(shipline, args.ShipmentList != null);
          }
        }
      }
    }
    if (args.QuickProcessFlow != null && ((PXSelectBase<SOSetup>) ((PXGraphExtension<SOShipmentEntry>) this).Base.sosetup).Current.RequireShipmentTotal.GetValueOrDefault())
      ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.ControlQty = ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.ShipmentQty;
    this.AllocateGroupFreeItems(order);
    this.AdjustFreeItemLines();
    // ISSUE: method pointer
    ((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).RowDeleting.RemoveHandler<PX.Objects.SO.SOOrderShipment>(new PXRowDeleting((object) null, __methodptr(\u003CCreateShipmentDetails\u003Eg__SOOrderShipment_RowDeleting\u007C10_0)));
    // ISSUE: method pointer
    ((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).RowDeleted.RemoveHandler<SOShipLine>(new PXRowDeleted((object) cDisplayClass100, __methodptr(\u003CCreateShipmentDetails\u003Eg__SOShipLine_RowDeleted\u007C1)));
    foreach (PX.Objects.SO.SOOrderShipment row in ((PXSelectBase) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.OrderList).Cache.Inserted)
    {
      if (args.ShipmentList == null)
      {
        Decimal? shipmentQty = row.ShipmentQty;
        Decimal num2 = 0M;
        if (shipmentQty.GetValueOrDefault() == num2 & shipmentQty.HasValue)
        {
          if (PXResultset<SOShipLine>.op_Implicit(PXSelectBase<SOShipLine, PXSelect<SOShipLine, Where<SOShipLine.shipmentType, Equal<Required<PX.Objects.SO.SOOrderShipment.shipmentType>>, And<SOShipLine.shipmentNbr, Equal<Required<PX.Objects.SO.SOOrderShipment.shipmentNbr>>, And<SOShipLine.origOrderType, Equal<Required<PX.Objects.SO.SOOrderShipment.orderType>>, And<SOShipLine.origOrderNbr, Equal<Required<PX.Objects.SO.SOOrderShipment.orderNbr>>>>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, (object[]) null, new object[4]
          {
            (object) row.ShipmentType,
            (object) row.ShipmentNbr,
            (object) row.OrderType,
            (object) row.OrderNbr
          })) == null)
            ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.OrderList).Delete(row);
        }
      }
      try
      {
        if (args.ShipmentList != null)
        {
          int? lineCntr = row.LineCntr;
          int num3 = 0;
          if (lineCntr.GetValueOrDefault() > num3 & lineCntr.HasValue)
          {
            Decimal? shipmentQty = row.ShipmentQty;
            Decimal num4 = 0M;
            if (shipmentQty.GetValueOrDefault() == num4 & shipmentQty.HasValue && ((PXSelectBase<SOSetup>) ((PXGraphExtension<SOShipmentEntry>) this).Base.sosetup).Current.AddAllToShipment.GetValueOrDefault() && !((PXSelectBase<SOSetup>) ((PXGraphExtension<SOShipmentEntry>) this).Base.sosetup).Current.CreateZeroShipments.GetValueOrDefault())
              throw new SOShipmentException(SOShipmentException.ErrorCode.CannotShipTraced, row, "Order {0} {1} does not contain any available items. Check previous warnings.", new object[2]
              {
                (object) row.OrderType,
                (object) row.OrderNbr
              });
          }
        }
        if (args.ShipmentList != null)
        {
          int? lineCntr = row.LineCntr;
          int num5 = 0;
          if (lineCntr.GetValueOrDefault() == num5 & lineCntr.HasValue)
          {
            if (flag1)
              throw new SOShipmentException(SOShipmentException.ErrorCode.NotAllocatedLines, row, "One or more lines contain items that are not allocated.", Array.Empty<object>());
            // ISSUE: reference to a compiler-generated field
            if (cDisplayClass100.anyDeleted)
              throw new SOShipmentException(SOShipmentException.ErrorCode.CannotShipCompleteTraced, row, "Order {0} {1} cannot be shipped in full. Check Trace for more details.", new object[2]
              {
                (object) row.OrderType,
                (object) row.OrderNbr
              });
            if (args.Operation == "I")
              throw new SOShipmentException(SOShipmentException.ErrorCode.NothingToShipTraced, row, "Order {0} {1} does not contain any items planned for shipment on '{2}'.", new object[3]
              {
                (object) row.OrderType,
                (object) row.OrderNbr,
                (object) row.ShipDate
              });
            throw new SOShipmentException(SOShipmentException.ErrorCode.NothingToReceiveTraced, row, "Order {0} {1} does not contain any items planned for receipt on '{2}'.", new object[3]
            {
              (object) row.OrderType,
              (object) row.OrderNbr,
              (object) row.ShipDate
            });
          }
        }
        if (args.ShipmentList != null)
        {
          if (row.ShipComplete == "C")
          {
            bool flag2 = false;
            bool flag3 = false;
            foreach (PXResult<SOLine2> pxResult in PXSelectBase<SOLine2, PXSelect<SOLine2, Where<SOLine2.orderType, Equal<Required<SOLine2.orderType>>, And<SOLine2.orderNbr, Equal<Required<SOLine2.orderNbr>>, And<SOLine2.siteID, Equal<Required<SOLine2.siteID>>, And<SOLine2.operation, Equal<Required<SOLine2.operation>>, And<SOLine2.completed, NotEqual<True>>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[4]
            {
              (object) row.OrderType,
              (object) row.OrderNbr,
              (object) row.SiteID,
              (object) row.Operation
            }))
            {
              SOLine2 soLine2 = PXResult<SOLine2>.op_Implicit(pxResult);
              SOLine2 original = GraphHelper.Caches<SOLine2>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).GetOriginal(soLine2);
              if (soLine2.LineType == "GI")
              {
                Decimal? shippedQty1 = soLine2.ShippedQty;
                Decimal? shippedQty2 = (Decimal?) original?.ShippedQty;
                Decimal? nullable = shippedQty1.HasValue & shippedQty2.HasValue ? new Decimal?(shippedQty1.GetValueOrDefault() - shippedQty2.GetValueOrDefault()) : new Decimal?();
                Decimal num6 = 0M;
                if (nullable.GetValueOrDefault() == num6 & nullable.HasValue && soLine2.POSource == "O")
                {
                  PXTrace.WriteError("The {0} item in line {1} that has the Marked for PO check box selected has not yet been fully received in the {2} warehouse.", new object[3]
                  {
                    (object) PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, soLine2.InventoryID)?.InventoryCD,
                    (object) soLine2.LineNbr,
                    (object) PX.Objects.IN.INSite.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, soLine2.SiteID)?.SiteCD
                  });
                  flag2 = true;
                }
              }
              if (soLine2.LineType == "GI")
              {
                Decimal? shippedQty3 = soLine2.ShippedQty;
                Decimal? shippedQty4 = (Decimal?) original?.ShippedQty;
                Decimal? nullable = shippedQty3.HasValue & shippedQty4.HasValue ? new Decimal?(shippedQty3.GetValueOrDefault() - shippedQty4.GetValueOrDefault()) : new Decimal?();
                Decimal num7 = 0M;
                if (nullable.GetValueOrDefault() == num7 & nullable.HasValue && DateTime.Compare(soLine2.ShipDate.Value, row.ShipDate.Value) <= 0 && soLine2.POSource != "D")
                  flag3 = true;
              }
            }
            if (flag2)
              throw new SOShipmentException("The {0} sales order with the {1} type cannot be shipped in full. For details, see the trace log: Click Tools > Trace on the form title bar.", new object[2]
              {
                (object) order.OrderNbr,
                (object) order.OrderType
              });
            if (flag3)
              throw new SOShipmentException("Order {0} {1} cannot be shipped in full. Check Trace for more details.", new object[2]
              {
                (object) order.OrderType,
                (object) order.OrderNbr
              });
          }
        }
      }
      catch (SOShipmentException ex)
      {
        this.UpdateShipmentCntr(((PXSelectBase) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.OrderList).Cache, row, new short?((short) -1));
        this.UpdateShipmentCntr(((PXSelectBase) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.OrderList).Cache, row, new short?((short) 0));
        throw;
      }
    }
    if (!(args.Operation == "I"))
      return;
    self.LinkShipment(((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current, (PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base);
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.CreateShipmentExtension.FillShipLineFromSource(PX.Objects.SO.SOShipLine,PX.Objects.SO.Interfaces.IShipLineSource)" />
  /// .
  [PXOverride]
  public void FillShipLineFromSource(
    SOShipLine newline,
    IShipLineSource lineSource,
    Action<SOShipLine, IShipLineSource> base_FillShipLineFromSource)
  {
    if (!(lineSource is SOLineShipLineSource lineShipLineSource))
    {
      base_FillShipLineFromSource(newline, lineSource);
    }
    else
    {
      PX.Objects.SO.SOLine soLine = lineShipLineSource.SOLine;
      PX.Objects.SO.SOLineSplit soLineSplit = lineShipLineSource.SOLineSplit;
      this.ValidateLineBeforeShipment(soLine);
      base_FillShipLineFromSource(newline, lineSource);
      newline.OrigOrderType = soLine.OrderType;
      newline.OrigOrderNbr = soLine.OrderNbr;
      newline.OrigPlanType = soLineSplit.POCreate.GetValueOrDefault() || soLineSplit.IsAllocated.GetValueOrDefault() ? lineSource.PlanType : soLineSplit.PlanType;
      newline.CustomerID = soLine.CustomerID;
      SOShipLine soShipLine = newline;
      Decimal? orderQty = soLine.OrderQty;
      Decimal num = 0M;
      short? nullable;
      if (!(orderQty.GetValueOrDefault() < num & orderQty.HasValue))
      {
        nullable = soLine.InvtMult;
      }
      else
      {
        short? invtMult = soLine.InvtMult;
        nullable = invtMult.HasValue ? new short?(-invtMult.GetValueOrDefault()) : new short?();
      }
      soShipLine.InvtMult = nullable;
      newline.SOLineSign = soLine.LineSign;
      newline.Operation = soLine.Operation;
      newline.LineType = soLine.LineType;
      newline.ReasonCode = soLine.ReasonCode;
      newline.IsFree = soLine.IsFree;
      newline.ManualDisc = soLine.ManualDisc;
      newline.DiscountID = soLine.DiscountID;
      newline.DiscountSequenceID = soLine.DiscountSequenceID;
      newline.AlternateID = soLine.AlternateID;
      newline.BlanketType = soLine.BlanketType;
      newline.BlanketNbr = soLine.BlanketNbr;
      newline.BlanketLineNbr = soLine.BlanketLineNbr;
      newline.BlanketSplitLineNbr = soLine.BlanketSplitLineNbr;
      newline.IsSpecialOrder = soLine.IsSpecialOrder;
      ((PXGraphExtension<SOShipmentEntry>) this).Base.UpdateOrigValues(newline, soLine, lineSource.PlanQty);
      this.ValidateLineType(soLine, lineSource.InventoryItem, "The shipment cannot be created because the settings of the {0} line have been changed. Delete the line from the sales order and add it again to update the line details.");
    }
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.CreateShipmentExtension.ShipFullIfNegQtyAllowed(PX.Objects.SO.SOShipLine)" />
  /// . 
  ///             <returns><see cref="P:PX.Objects.SO.SOOrderType.ShipFullIfNegQtyAllowed" /> value of related order type.</returns>
  [PXOverride]
  public bool? ShipFullIfNegQtyAllowed(
    SOShipLine newline,
    Func<SOShipLine, bool?> base_ShipFullIfNegQtyAllowed)
  {
    PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrderType>) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.soordertype).Select(new object[1]
    {
      (object) newline.OrigOrderType
    }));
    return new bool?(soOrderType != null && soOrderType.ShipFullIfNegQtyAllowed.GetValueOrDefault());
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.CreateShipmentExtension.ShouldSaveAfterCreateShipment(PX.Objects.SO.CreateShipmentArgs)" />
  /// .
  [PXOverride]
  public bool ShouldSaveAfterCreateShipment(
    CreateShipmentArgs args,
    Func<CreateShipmentArgs, bool> base_ShouldSaveAfterCreateShipment)
  {
    return base_ShouldSaveAfterCreateShipment(args) || ((PXSelectBase) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.OrderList).Cache.Inserted.Count() > 0L || ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.OrderList).SelectWindowed(0, 1, Array.Empty<object>()) != null;
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.CreateShipmentExtension.AfterSaveCreateShipment(PX.Objects.SO.CreateShipmentArgs)" />
  /// .
  [PXOverride]
  public void AfterSaveCreateShipment(
    CreateShipmentArgs args,
    Action<CreateShipmentArgs> base_AfterSaveCreateShipment)
  {
    base_AfterSaveCreateShipment(args);
    PX.Objects.SO.SOOrder order = args.Order;
    if (order == null)
      return;
    PX.Objects.SO.SOOrder soOrder = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.soorder).Locate(order);
    if (soOrder == null)
      return;
    bool? selected = args.Order.Selected;
    PXCache<PX.Objects.SO.SOOrder>.RestoreCopy(args.Order, soOrder);
    args.Order.Selected = selected;
  }

  public virtual bool ValidateLineBeforeShipment(PX.Objects.SO.SOLine line) => true;

  protected virtual PX.Objects.SO.SOOrder ActualizeAndValidateOrder(
    SOOrderEntry orderEntry,
    PX.Objects.SO.SOOrder order,
    string operation)
  {
    order = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.soorder).Select(new object[2]
    {
      (object) order.OrderType,
      (object) order.OrderNbr
    }));
    if (orderEntry == null)
      return order;
    bool? nullable1;
    if (!(operation == "R"))
      nullable1 = WorkflowAction.HasWorkflowActionEnabled<SOOrderEntry, PX.Objects.SO.SOOrder>(orderEntry, (Expression<Func<SOOrderEntry, PXAction<PX.Objects.SO.SOOrder>>>) (g => g.createShipmentIssue), order);
    else
      nullable1 = WorkflowAction.HasWorkflowActionEnabled<SOOrderEntry, PX.Objects.SO.SOOrder>(orderEntry, (Expression<Func<SOOrderEntry, PXAction<PX.Objects.SO.SOOrder>>>) (g => g.createShipmentReceipt), order);
    bool? nullable2 = nullable1;
    bool flag = false;
    if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
      throw new PXInvalidOperationException("The {0} action is not available in the {1} document at the moment. The document is being used by another process.", new object[2]
      {
        (object) ((PXAction) (operation == "R" ? orderEntry.createShipmentReceipt : orderEntry.createShipmentIssue)).GetCaption(),
        (object) ((PXSelectBase) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.soorder).Cache.GetRowDescription((object) order)
      });
    return order;
  }

  protected virtual void SetShipAddressAndContact(
    PX.Objects.SO.SOShipment shipment,
    int? shipAddressID,
    int? shipContactID)
  {
    foreach (PXResult<SOShipmentAddress> pxResult in ((PXSelectBase<SOShipmentAddress>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Shipping_Address).Select(Array.Empty<object>()))
    {
      SOShipmentAddress soShipmentAddress = PXResult<SOShipmentAddress>.op_Implicit(pxResult);
      int? addressId = soShipmentAddress.AddressID;
      int num = 0;
      if (addressId.GetValueOrDefault() < num & addressId.HasValue)
        ((PXSelectBase<SOShipmentAddress>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Shipping_Address).Delete(soShipmentAddress);
    }
    foreach (PXResult<SOShipmentContact> pxResult in ((PXSelectBase<SOShipmentContact>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Shipping_Contact).Select(Array.Empty<object>()))
    {
      SOShipmentContact soShipmentContact = PXResult<SOShipmentContact>.op_Implicit(pxResult);
      int? contactId = soShipmentContact.ContactID;
      int num = 0;
      if (contactId.GetValueOrDefault() < num & contactId.HasValue)
        ((PXSelectBase<SOShipmentContact>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Shipping_Contact).Delete(soShipmentContact);
    }
    SOAddress source1 = SOAddress.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, shipAddressID);
    if (source1.IsDefaultAddress.GetValueOrDefault())
    {
      shipment.ShipAddressID = shipAddressID;
    }
    else
    {
      SOShipmentAddress dest = new SOShipmentAddress();
      AddressAttribute.Copy((IAddress) dest, (IAddress) source1);
      SOShipmentAddress soShipmentAddress = ((PXSelectBase<SOShipmentAddress>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Shipping_Address).Insert(dest);
      shipment.ShipAddressID = soShipmentAddress.AddressID;
    }
    SOContact source2 = SOContact.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, shipContactID);
    if (source2.IsDefaultContact.GetValueOrDefault())
    {
      shipment.ShipContactID = shipContactID;
    }
    else
    {
      SOShipmentContact dest = new SOShipmentContact();
      ContactAttribute.CopyContact((IContact) dest, (IContact) source2);
      SOShipmentContact soShipmentContact = ((PXSelectBase<SOShipmentContact>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Shipping_Contact).Insert(dest);
      shipment.ShipContactID = soShipmentContact.ContactID;
    }
  }

  private class ShipmentSchedule : IComparable<CreateShipmentSOExtension.ShipmentSchedule>
  {
    private readonly int sortOrder;
    private readonly int soLineNbr;
    private readonly int splitLineNbr;
    public SOShipLine ShipLine;

    public ShipmentSchedule(
      PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite> result,
      SOShipLine shipLine)
    {
      this.sortOrder = PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(result).SortOrder ?? 1000;
      this.soLineNbr = PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(result).LineNbr ?? int.MaxValue;
      this.splitLineNbr = PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(result).SplitLineNbr ?? int.MaxValue;
      this.Result = result;
      this.ShipLine = shipLine;
    }

    public PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite> Result { get; private set; }

    public int CompareTo(CreateShipmentSOExtension.ShipmentSchedule other)
    {
      int num = this.sortOrder.CompareTo(other.sortOrder);
      if (num == 0)
      {
        num = this.soLineNbr.CompareTo(other.soLineNbr);
        if (num == 0)
          num = this.splitLineNbr.CompareTo(other.splitLineNbr);
      }
      return num;
    }
  }

  private class CopySettings : PXNoteAttribute.IPXCopySettings
  {
    public CopySettings(bool? copyFiles, bool? copyNotes)
    {
      this.CopyFiles = copyFiles;
      this.CopyNotes = copyNotes;
    }

    public bool? CopyNotes { get; }

    public bool? CopyFiles { get; }
  }
}

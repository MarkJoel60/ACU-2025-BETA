// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderLineSplittingExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class SOOrderLineSplittingExtension : 
  LineSplittingExtension<SOOrderEntry, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>
{
  protected PX.Objects.SO.SOOrder _LastSelected;

  public bool IsLocationEnabled
  {
    get
    {
      PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select((PXGraph) this.Base, Array.Empty<object>()));
      if (soOrderType == null)
        return true;
      bool? nullable = soOrderType.RequireShipping;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        nullable = soOrderType.RequireLocation;
        if (nullable.GetValueOrDefault())
          return soOrderType.INDocType != "UND";
      }
      return false;
    }
  }

  public bool IsLotSerialRequired
  {
    get
    {
      PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select((PXGraph) this.Base, Array.Empty<object>()));
      return soOrderType == null || soOrderType.RequireLotSerial.GetValueOrDefault();
    }
  }

  public bool IsLSEntryEnabled
  {
    get
    {
      PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select((PXGraph) this.Base, Array.Empty<object>()));
      return soOrderType == null || soOrderType.RequireLocation.GetValueOrDefault() || soOrderType.RequireLotSerial.GetValueOrDefault();
    }
  }

  public bool IsBlanketOrder
  {
    get
    {
      return PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select((PXGraph) this.Base, Array.Empty<object>()))?.Behavior == "BL";
    }
  }

  protected override Type SplitsToDocumentCondition
  {
    get
    {
      return typeof (KeysRelation<CompositeKey<Field<PX.Objects.SO.SOLineSplit.orderType>.IsRelatedTo<PX.Objects.SO.SOOrder.orderType>, Field<PX.Objects.SO.SOLineSplit.orderNbr>.IsRelatedTo<PX.Objects.SO.SOOrder.orderNbr>>.WithTablesOf<PX.Objects.SO.SOOrder, PX.Objects.SO.SOLineSplit>, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLineSplit>.SameAsCurrent);
    }
  }

  protected override Type LineQtyField => typeof (PX.Objects.SO.SOLine.orderQty);

  public override PX.Objects.SO.SOLineSplit LineToSplit(PX.Objects.SO.SOLine line)
  {
    using (this.InvtMultModeScope(line))
    {
      PX.Objects.SO.SOLineSplit split = PX.Objects.SO.SOLineSplit.FromSOLine(line);
      Decimal? baseQty = line.BaseQty;
      Decimal? unassignedQty = line.UnassignedQty;
      split.BaseQty = baseQty.HasValue & unassignedQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() - unassignedQty.GetValueOrDefault()) : new Decimal?();
      return split;
    }
  }

  public override void Initialize()
  {
    base.Initialize();
    ((RowSelectedEvents) ((PXGraph) this.Base).RowSelected).AddAbstractHandler<PX.Objects.SO.SOOrder>(new Action<AbstractEvents.IRowSelected<PX.Objects.SO.SOOrder>>(this.EventHandler));
    ((RowUpdatedEvents) ((PXGraph) this.Base).RowUpdated).AddAbstractHandler<PX.Objects.SO.SOOrder>(new Action<AbstractEvents.IRowUpdated<PX.Objects.SO.SOOrder>>(this.EventHandler));
  }

  public override IEnumerable ShowSplits(PXAdapter adapter)
  {
    PX.Objects.SO.SOLine lineCurrent = this.LineCurrent;
    if (lineCurrent?.LineType == "MI" && !this.IsBlanketOrder)
      throw new PXSetPropertyException("Shipment Scheduling and Bin/Lot/Serial assignment are not possible for non-stock items.");
    if (this.IsLSEntryEnabled && lineCurrent != null && lineCurrent.LineType != "GI")
      throw new PXSetPropertyException("Shipment Scheduling and Bin/Lot/Serial assignment are not possible for non-stock items.");
    return base.ShowSplits(adapter);
  }

  protected virtual void EventHandler(AbstractEvents.IRowSelected<PX.Objects.SO.SOOrder> e)
  {
    if (this._LastSelected == null || this._LastSelected != e.Row)
    {
      PXUIFieldAttribute.SetRequired<PX.Objects.SO.SOLine.locationID>((PXCache) this.LineCache, this.IsLocationEnabled);
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLine.locationID>((PXCache) this.LineCache, (object) null, this.IsLocationEnabled);
      PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLine.locationID>((PXCache) this.LineCache, (object) null, this.IsLocationEnabled);
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLine.lotSerialNbr>((PXCache) this.LineCache, (object) null, this.IsLSEntryEnabled);
      PXCacheEx.Adjust<INLotSerialNbrAttribute>((PXCache) this.LineCache, (object) null).For<PX.Objects.SO.SOLine.lotSerialNbr>((Action<INLotSerialNbrAttribute>) (a => a.ForceDisable = !this.IsLSEntryEnabled));
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLine.expireDate>((PXCache) this.LineCache, (object) null, this.IsLSEntryEnabled);
      PXCacheEx.Adjust<INExpireDateAttribute>((PXCache) this.LineCache, (object) null).For<PX.Objects.SO.SOLine.expireDate>((Action<INExpireDateAttribute>) (a => a.ForceDisable = !this.IsLSEntryEnabled));
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.inventoryID>((PXCache) this.SplitCache, (object) null, this.IsLSEntryEnabled);
      PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLineSplit.inventoryID>((PXCache) this.SplitCache, (object) null, this.IsLSEntryEnabled);
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.locationID>((PXCache) this.SplitCache, (object) null, this.IsLocationEnabled);
      PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLineSplit.locationID>((PXCache) this.SplitCache, (object) null, this.IsLocationEnabled);
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.lotSerialNbr>((PXCache) this.SplitCache, (object) null, !this.IsBlanketOrder);
      PXCacheEx.Adjust<INLotSerialNbrAttribute>((PXCache) this.SplitCache, (object) null).For<PX.Objects.SO.SOLineSplit.lotSerialNbr>((Action<INLotSerialNbrAttribute>) (a => a.ForceDisable = this.IsBlanketOrder));
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.expireDate>((PXCache) this.SplitCache, (object) null, this.IsLSEntryEnabled);
      PXCacheEx.Adjust<INExpireDateAttribute>((PXCache) this.SplitCache, (object) null).For<PX.Objects.SO.SOLineSplit.expireDate>((Action<INExpireDateAttribute>) (a => a.ForceDisable = !this.IsLSEntryEnabled));
      PXView pxView;
      if (((Dictionary<string, PXView>) ((PXGraph) this.Base).Views).TryGetValue(this.TypePrefixed("LotSerOptions"), out pxView))
        pxView.AllowSelect = this.IsLSEntryEnabled;
      if (e.Row != null)
        this._LastSelected = e.Row;
    }
    this.showSplits.SetEnabled(false);
    if (!this.IsLSEntryEnabled)
      return;
    this.showSplits.SetEnabled(true);
  }

  protected virtual void EventHandler(AbstractEvents.IRowUpdated<PX.Objects.SO.SOOrder> e)
  {
    if (!this.IsLSEntryEnabled && !this.IsBlanketOrder)
      return;
    bool? cancelled1 = e.Row.Cancelled;
    bool flag1 = false;
    if (!(cancelled1.GetValueOrDefault() == flag1 & cancelled1.HasValue))
      return;
    bool? hold1 = e.Row.Hold;
    bool? hold2 = e.OldRow.Hold;
    if (!(hold1.GetValueOrDefault() == hold2.GetValueOrDefault() & hold1.HasValue == hold2.HasValue))
    {
      bool? hold3 = e.Row.Hold;
      bool flag2 = false;
      if (hold3.GetValueOrDefault() == flag2 & hold3.HasValue)
        goto label_5;
    }
    bool? cancelled2 = e.Row.Cancelled;
    bool? cancelled3 = e.OldRow.Cancelled;
    if (cancelled2.GetValueOrDefault() == cancelled3.GetValueOrDefault() & cancelled2.HasValue == cancelled3.HasValue)
      return;
label_5:
    foreach (PX.Objects.SO.SOLine selectSibling in PXParentAttribute.SelectSiblings((PXCache) this.LineCache, (object) null, typeof (PX.Objects.SO.SOOrder)))
    {
      if (Math.Abs(selectSibling.BaseQty.Value) >= 0.0000005M)
      {
        Decimal? unassignedQty1 = selectSibling.UnassignedQty;
        Decimal num = 0.0000005M;
        if (!(unassignedQty1.GetValueOrDefault() >= num & unassignedQty1.HasValue))
        {
          Decimal? unassignedQty2 = selectSibling.UnassignedQty;
          num = -0.0000005M;
          if (!(unassignedQty2.GetValueOrDefault() <= num & unassignedQty2.HasValue))
            continue;
        }
        string str = this.IsBlanketOrder ? "The total quantity of the item in the Line Details dialog box cannot differ from the quantity of the item in the sales order line." : "One or more lines have unassigned Location and/or Lot/Serial Number";
        ((PXCache) this.LineCache).RaiseExceptionHandling<PX.Objects.SO.SOLine.orderQty>((object) selectSibling, (object) selectSibling.Qty, (Exception) new PXSetPropertyException(str));
        GraphHelper.MarkUpdated((PXCache) this.LineCache, (object) selectSibling, true);
      }
    }
  }

  protected override void EventHandler(AbstractEvents.IRowUpdated<PX.Objects.SO.SOLine> e)
  {
    try
    {
      using (this.ResolveNotDecimalUnitErrorRedirectorScope<PX.Objects.SO.SOLineSplit.qty>((object) e.Row))
        base.EventHandler(e);
    }
    catch (PXUnitConversionException ex)
    {
      if (PXUIFieldAttribute.GetErrors(((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache, (object) e.Row, new PXErrorLevel[1]
      {
        (PXErrorLevel) 4
      }).Keys.Any<string>(new Func<string, bool>(isUomField)))
        return;
      ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.uOM>((object) e.Row, (object) null, (Exception) ex);
    }

    static bool isUomField(string f)
    {
      return string.Equals(f, "uOM", StringComparison.InvariantCultureIgnoreCase);
    }
  }

  protected bool IsAutoCreateIssuePreCheck(PX.Objects.SO.SOLine row)
  {
    if (!(row.Operation == "R") || !row.AutoCreateIssueLine.GetValueOrDefault() || !(row.LotSerialNbr != string.Empty))
      return true;
    INLotSerClass inLotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(this.ReadInventoryItem(row.InventoryID));
    PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select((PXGraph) this.Base, Array.Empty<object>()));
    return !(soOrderType?.Behavior == "RM") || soOrderType == null || !soOrderType.RequireLotSerial.GetValueOrDefault() || !(inLotSerClass.LotSerTrack != "N");
  }

  protected override void EventHandler(AbstractEvents.IRowPersisting<PX.Objects.SO.SOLine> e)
  {
    if (EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
    {
      if (!this.IsAutoCreateIssuePreCheck(e.Row))
      {
        object valueExt = ((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache.GetValueExt<PX.Objects.SO.SOLine.inventoryID>((object) e.Row);
        if (((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.autoCreateIssueLine>((object) e.Row, (object) e.Row.AutoCreateIssueLine, (Exception) new PXSetPropertyException("The order cannot be saved because entering a lot or serial number is required in the settings of this order's type for a line with a lot- or serial-tracked item, and the system cannot insert this number for the automatically generated replacement line with the Issue operation. To save the order, clear the Auto Create Issue check box in the line with the {0} item.", new object[1]
        {
          valueExt
        })))
          throw new PXRowPersistingException("autoCreateIssueLine", (object) e.Row.AutoCreateIssueLine, "The order cannot be saved because entering a lot or serial number is required in the settings of this order's type for a line with a lot- or serial-tracked item, and the system cannot insert this number for the automatically generated replacement line with the Issue operation. To save the order, clear the Auto Create Issue check box in the line with the {0} item.", new object[1]
          {
            valueExt
          });
      }
      if (this.IsLSEntryEnabled || this.IsBlanketOrder)
      {
        PX.Objects.SO.SOOrder soOrder = PXParentAttribute.SelectParent<PX.Objects.SO.SOOrder>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row) ?? ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
        bool? nullable = e.Row.Cancelled;
        bool flag1 = false;
        if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
        {
          nullable = soOrder.Hold;
          bool flag2 = false;
          if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue && Math.Abs(e.Row.BaseQty.Value) >= 0.0000005M)
          {
            Decimal? unassignedQty = e.Row.UnassignedQty;
            Decimal num = 0.0000005M;
            if (!(unassignedQty.GetValueOrDefault() >= num & unassignedQty.HasValue))
            {
              unassignedQty = e.Row.UnassignedQty;
              num = -0.0000005M;
              if (!(unassignedQty.GetValueOrDefault() <= num & unassignedQty.HasValue))
                goto label_11;
            }
            string str = this.IsBlanketOrder ? "The total quantity of the item in the Line Details dialog box cannot differ from the quantity of the item in the sales order line." : "One or more lines have unassigned Location and/or Lot/Serial Number";
            if (((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.orderQty>((object) e.Row, (object) e.Row.Qty, (Exception) new PXSetPropertyException(str)))
              throw new PXRowPersistingException("orderQty", (object) e.Row.Qty, str);
          }
        }
      }
    }
label_11:
    base.EventHandler(e);
  }

  protected override void SubscribeForSplitEvents()
  {
    base.SubscribeForSplitEvents();
    ((RowSelectedEvents) ((PXGraph) this.Base).RowSelected).AddAbstractHandler<PX.Objects.SO.SOLineSplit>(new Action<AbstractEvents.IRowSelected<PX.Objects.SO.SOLineSplit>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.invtMult, short>(new Action<AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.invtMult, short?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.subItemID, int>(new Action<AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.subItemID, int?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.locationID, int>(new Action<AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.locationID, int?>>(this.EventHandler));
  }

  protected virtual void EventHandler(AbstractEvents.IRowSelected<PX.Objects.SO.SOLineSplit> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.SO.SOLine soLine = PXParentAttribute.SelectParent<PX.Objects.SO.SOLine>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row);
    bool flag1 = e.Row.LineType == "GI";
    object valueExt = ((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache.GetValueExt<PX.Objects.SO.SOLineSplit.isAllocated>((object) e.Row);
    bool? nullable;
    int num1;
    if (!e.Row.IsAllocated.GetValueOrDefault())
    {
      nullable = (bool?) PXFieldState.UnwrapValue(valueExt);
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 1;
    bool flag2 = num1 != 0;
    nullable = e.Row.Completed;
    bool isCompleted = (nullable.GetValueOrDefault() ? 1 : 0) != 0;
    int num2;
    if (e.Row.PONbr == null)
    {
      if (e.Row.SOOrderNbr != null)
      {
        nullable = e.Row.IsAllocated;
        num2 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num2 = 0;
    }
    else
      num2 = 1;
    bool flag3 = num2 != 0;
    nullable = e.Row.POCreate;
    int num3;
    if (nullable.GetValueOrDefault())
    {
      int num4;
      if (soLine == null)
      {
        num4 = 1;
      }
      else
      {
        nullable = soLine.IsSpecialOrder;
        num4 = !nullable.GetValueOrDefault() ? 1 : 0;
      }
      if (num4 != 0)
      {
        num3 = 1;
        goto label_16;
      }
    }
    nullable = e.Row.POCompleted;
    num3 = nullable.GetValueOrDefault() ? 1 : 0;
label_16:
    bool isPOSchedule = num3 != 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLineSplit.subItemID>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLineSplit.completed>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLineSplit.shippedQty>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLineSplit.shipmentNbr>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLineSplit.siteID>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, !isCompleted & flag1 & flag2 && !flag3 && !this.IsBlanketOrder);
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLineSplit.qty>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, !isCompleted && !flag3);
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLineSplit.shipDate>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, !isCompleted && soLine?.ShipComplete == "B");
    PXCache cache = ((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache;
    PX.Objects.SO.SOLineSplit row = e.Row;
    int num5;
    if (this.IsBlanketOrder && !isCompleted && !flag2 && soLine != null)
    {
      nullable = soLine.POCreate;
      if (nullable.GetValueOrDefault())
      {
        num5 = e.Row.PONbr == null ? 1 : 0;
        goto label_20;
      }
    }
    num5 = 0;
label_20:
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLineSplit.pOCreate>(cache, (object) row, num5 != 0);
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLineSplit.pONbr>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLineSplit.pOReceiptNbr>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, false);
    PXCacheEx.Adjust<INLotSerialNbrAttribute>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row).For<PX.Objects.SO.SOLineSplit.lotSerialNbr>((Action<INLotSerialNbrAttribute>) (a => a.ForceDisable = isCompleted | isPOSchedule));
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.invtMult, short?> e)
  {
    if (this.LineCurrent == null)
      return;
    if (e.Row != null)
    {
      int? lineNbr1 = this.LineCurrent.LineNbr;
      int? lineNbr2 = e.Row.LineNbr;
      if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue))
        return;
    }
    using (this.InvtMultModeScope(this.LineCurrent))
    {
      e.NewValue = this.LineCurrent.InvtMult;
      ((ICancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.subItemID, int?> e)
  {
    if (this.LineCurrent == null)
      return;
    if (e.Row != null)
    {
      int? lineNbr1 = this.LineCurrent.LineNbr;
      int? lineNbr2 = e.Row.LineNbr;
      if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue) || !e.Row.IsStockItem.GetValueOrDefault())
        return;
    }
    e.NewValue = this.LineCurrent.SubItemID;
    ((ICancelEventArgs) e).Cancel = true;
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.locationID, int?> e)
  {
    if (this.LineCurrent == null)
      return;
    int? nullable;
    if (e.Row != null)
    {
      int? lineNbr = this.LineCurrent.LineNbr;
      nullable = e.Row.LineNbr;
      if (!(lineNbr.GetValueOrDefault() == nullable.GetValueOrDefault() & lineNbr.HasValue == nullable.HasValue) || !e.Row.IsStockItem.GetValueOrDefault())
        return;
    }
    e.NewValue = this.LineCurrent.LocationID;
    AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.locationID, int?> ifieldDefaulting = e;
    int num;
    if (!this.SuppressedMode)
    {
      nullable = e.NewValue;
      if (!nullable.HasValue)
      {
        num = !this.IsLocationEnabled ? 1 : 0;
        goto label_7;
      }
    }
    num = 1;
label_7:
    ((ICancelEventArgs) ifieldDefaulting).Cancel = num != 0;
  }

  protected override void EventHandler(AbstractEvents.IRowInserting<PX.Objects.SO.SOLineSplit> e)
  {
    if (!this.IsLSEntryEnabled)
      return;
    if (e.ExternalCall && e.Row.LineType != "GI")
      throw new PXSetPropertyException("The record cannot be inserted.");
    if (e.Row != null && !this.IsLocationEnabled && e.Row.LocationID.HasValue)
      e.Row.LocationID = new int?();
    base.EventHandler(e);
  }

  public override void EventHandler(AbstractEvents.IRowPersisting<PX.Objects.SO.SOLineSplit> e)
  {
    base.EventHandler(e);
    if (e.Row == null || !EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    int num1;
    if (e.Row.RequireLocation.GetValueOrDefault() && e.Row.IsStockItem.GetValueOrDefault())
    {
      Decimal? baseQty = e.Row.BaseQty;
      Decimal num2 = 0M;
      num1 = !(baseQty.GetValueOrDefault() == num2 & baseQty.HasValue) ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag = num1 != 0;
    PXDefaultAttribute.SetPersistingCheck<PX.Objects.SO.SOLineSplit.subItemID>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<PX.Objects.SO.SOLineSplit.locationID>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  public override void EventHandlerUOM(
    AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOLineSplit, IBqlField, string> e)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(e.Row.InventoryID);
    if (this.UseBaseUnitInSplit(e.Row, ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Current, pxResult))
    {
      e.NewValue = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).BaseUnit;
      ((ICancelEventArgs) e).Cancel = true;
    }
    else
      base.EventHandlerUOM(e);
  }

  internal PX.Objects.SO.SOLineSplit[] GetSplits(PX.Objects.SO.SOLine line)
  {
    return this.SelectSplits(line, true);
  }

  protected override PX.Objects.SO.SOLineSplit[] SelectSplits(PX.Objects.SO.SOLineSplit split)
  {
    return this.SelectSplits(split, true);
  }

  protected virtual PX.Objects.SO.SOLineSplit[] SelectSplits(
    PX.Objects.SO.SOLineSplit split,
    bool excludeCompleted = true)
  {
    return this.Availability.IsOptimizationEnabled ? ((IEnumerable<PX.Objects.SO.SOLineSplit>) this.SelectAllSplits(PX.Objects.SO.SOLine.FromSOLineSplit(split))).Where<PX.Objects.SO.SOLineSplit>(new Func<PX.Objects.SO.SOLineSplit, bool>(NotCompleted)).ToArray<PX.Objects.SO.SOLineSplit>() : ((IEnumerable<PX.Objects.SO.SOLineSplit>) base.SelectSplits(split)).Where<PX.Objects.SO.SOLineSplit>(new Func<PX.Objects.SO.SOLineSplit, bool>(NotCompleted)).ToArray<PX.Objects.SO.SOLineSplit>();

    bool NotCompleted(PX.Objects.SO.SOLineSplit a)
    {
      bool? completed = a.Completed;
      bool flag1 = false;
      if (!(completed.GetValueOrDefault() == flag1 & completed.HasValue))
      {
        bool? requireShipping = a.RequireShipping;
        bool flag2 = false;
        if (!(requireShipping.GetValueOrDefault() == flag2 & requireShipping.HasValue) || this.IsBlanketOrder)
          return !excludeCompleted && a.PONbr == null && a.SOOrderNbr == null;
      }
      return true;
    }
  }

  protected override PX.Objects.SO.SOLineSplit[] SelectSplits(PX.Objects.SO.SOLine line, bool compareInventoryID = true)
  {
    return ((IEnumerable<PX.Objects.SO.SOLineSplit>) this.SelectAllSplits(line, compareInventoryID)).Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s =>
    {
      bool? completed = s.Completed;
      bool flag1 = false;
      if (completed.GetValueOrDefault() == flag1 & completed.HasValue)
        return true;
      bool? requireShipping = s.RequireShipping;
      bool flag2 = false;
      return requireShipping.GetValueOrDefault() == flag2 & requireShipping.HasValue && !this.IsBlanketOrder;
    })).ToArray<PX.Objects.SO.SOLineSplit>();
  }

  protected virtual PX.Objects.SO.SOLineSplit[] SelectAllSplits(
    PX.Objects.SO.SOLine line,
    bool compareInventoryID = true)
  {
    return !((PXGraph) this.Base).IsContractBasedAPI && this.Availability.IsOptimizationEnabled ? this.SelectAllSplits(this.LineToSplit(line), compareInventoryID) : base.SelectSplits(line, compareInventoryID);
  }

  private PX.Objects.SO.SOLineSplit[] SelectAllSplits(PX.Objects.SO.SOLineSplit split, bool compareInventoryID)
  {
    return PXParentAttribute.SelectSiblings((PXCache) this.SplitCache, (object) split, typeof (PX.Objects.SO.SOOrder)).Cast<PX.Objects.SO.SOLineSplit>().Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (a =>
    {
      if (compareInventoryID && !this.SameInventoryItem((ILSMaster) a, (ILSMaster) split))
        return false;
      int? lineNbr1 = a.LineNbr;
      int? lineNbr2 = split.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
    })).ToArray<PX.Objects.SO.SOLineSplit>();
  }

  protected override PX.Objects.SO.SOLineSplit[] SelectSplitsOrdered(PX.Objects.SO.SOLineSplit split)
  {
    return this.SelectSplitsOrdered(split, true);
  }

  protected virtual PX.Objects.SO.SOLineSplit[] SelectSplitsOrdered(
    PX.Objects.SO.SOLineSplit split,
    bool excludeCompleted = true)
  {
    return ((IEnumerable<PX.Objects.SO.SOLineSplit>) this.SelectSplits(split, excludeCompleted)).OrderBy<PX.Objects.SO.SOLineSplit, int>((Func<PX.Objects.SO.SOLineSplit, int>) (s =>
    {
      if (s.Completed.GetValueOrDefault())
        return 0;
      if (s.POCreate.GetValueOrDefault())
        return 1;
      return !s.IsAllocated.GetValueOrDefault() ? 3 : 2;
    })).ThenBy<PX.Objects.SO.SOLineSplit, int?>((Func<PX.Objects.SO.SOLineSplit, int?>) (s => s.SplitLineNbr)).ToArray<PX.Objects.SO.SOLineSplit>();
  }

  protected override PX.Objects.SO.SOLineSplit[] SelectSplitsReversed(PX.Objects.SO.SOLineSplit split)
  {
    return this.SelectSplitsReversed(split, true);
  }

  protected virtual PX.Objects.SO.SOLineSplit[] SelectSplitsReversed(
    PX.Objects.SO.SOLineSplit split,
    bool excludeCompleted = true)
  {
    return ((IEnumerable<PX.Objects.SO.SOLineSplit>) this.SelectSplits(split, excludeCompleted)).OrderByDescending<PX.Objects.SO.SOLineSplit, int>((Func<PX.Objects.SO.SOLineSplit, int>) (s =>
    {
      if (s.Completed.GetValueOrDefault())
        return 0;
      if (s.POCreate.GetValueOrDefault())
        return 1;
      return !s.IsAllocated.GetValueOrDefault() ? 3 : 2;
    })).ThenByDescending<PX.Objects.SO.SOLineSplit, int?>((Func<PX.Objects.SO.SOLineSplit, int?>) (s => s.SplitLineNbr)).ToArray<PX.Objects.SO.SOLineSplit>();
  }

  protected virtual PX.Objects.SO.SOLineSplit[] SelectSplitsReversedforTruncate(
    PX.Objects.SO.SOLineSplit split,
    bool excludeCompleted = true)
  {
    return ((IEnumerable<PX.Objects.SO.SOLineSplit>) this.SelectSplits(split, excludeCompleted)).OrderByDescending<PX.Objects.SO.SOLineSplit, int>((Func<PX.Objects.SO.SOLineSplit, int>) (s =>
    {
      if (s.Completed.GetValueOrDefault())
        return 0;
      if (s.IsAllocated.GetValueOrDefault())
        return 1;
      return !s.POCreate.GetValueOrDefault() ? 3 : 2;
    })).ThenByDescending<PX.Objects.SO.SOLineSplit, int?>((Func<PX.Objects.SO.SOLineSplit, int?>) (s => s.SplitLineNbr)).ToArray<PX.Objects.SO.SOLineSplit>();
  }

  protected override PXSelectBase<INLotSerialStatusByCostCenter> GetSerialStatusCmdBase(
    PX.Objects.SO.SOLine line,
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> item)
  {
    return (PXSelectBase<INLotSerialStatusByCostCenter>) new FbqlSelect<SelectFromBase<INLotSerialStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocation>.On<INLotSerialStatusByCostCenter.FK.Location>>, FbqlJoins.Inner<INSiteLotSerial>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteLotSerial.inventoryID, Equal<INLotSerialStatusByCostCenter.inventoryID>>>>, And<BqlOperand<INSiteLotSerial.siteID, IBqlInt>.IsEqual<INLotSerialStatusByCostCenter.siteID>>>>.And<BqlOperand<INSiteLotSerial.lotSerialNbr, IBqlString>.IsEqual<INLotSerialStatusByCostCenter.lotSerialNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLotSerialStatusByCostCenter.inventoryID, Equal<BqlField<INLotSerialStatusByCostCenter.inventoryID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<INLotSerialStatusByCostCenter.siteID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.siteID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INLotSerialStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.costCenterID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INLotSerialStatusByCostCenter.qtyOnHand, IBqlDecimal>.IsGreater<decimal0>>>, INLotSerialStatusByCostCenter>.View((PXGraph) this.Base);
  }

  protected override void AppendSerialStatusCmdWhere(
    PXSelectBase<INLotSerialStatusByCostCenter> cmd,
    PX.Objects.SO.SOLine line,
    INLotSerClass lotSerClass)
  {
    if (line.SubItemID.HasValue)
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.FromCurrent>>>();
    if (line.LocationID.HasValue)
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.locationID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.locationID, IBqlInt>.FromCurrent>>>();
    else if (line.TranType == "TRX")
      cmd.WhereAnd<Where<BqlOperand<INLocation.transfersValid, IBqlBool>.IsEqual<True>>>();
    else
      cmd.WhereAnd<Where<BqlOperand<INLocation.salesValid, IBqlBool>.IsEqual<True>>>();
    if (!lotSerClass.IsManualAssignRequired.GetValueOrDefault())
      return;
    if (string.IsNullOrEmpty(line.LotSerialNbr))
      cmd.WhereAnd<Where<BqlOperand<True, IBqlBool>.IsEqual<False>>>();
    else
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.lotSerialNbr, IBqlString>.IsEqual<BqlField<INLotSerialStatusByCostCenter.lotSerialNbr, IBqlString>.FromCurrent>>>();
  }

  protected override void UpdateCounters(LSSelect.Counters counters, PX.Objects.SO.SOLineSplit split)
  {
    base.UpdateCounters(counters, split);
    if (!split.POCreate.GetValueOrDefault() && !split.AMProdCreate.GetValueOrDefault())
      return;
    LSSelect.Counters counters1 = counters;
    Decimal baseQty = counters1.BaseQty;
    Decimal? nullable = split.BaseReceivedQty;
    Decimal num1 = nullable.Value;
    nullable = split.BaseShippedQty;
    Decimal num2 = nullable.Value;
    Decimal num3 = num1 + num2;
    counters1.BaseQty = baseQty - num3;
  }

  public virtual bool UseBaseUnitInSplit(
    PX.Objects.SO.SOLineSplit split,
    PX.Objects.SO.SOLine line,
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> item)
  {
    if (this.IsBlanketOrder || item == null || !(PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(item).LotSerTrack == "S"))
      return false;
    return line == null || !line.IsSpecialOrder.GetValueOrDefault();
  }

  protected override bool GenerateLotSerialNumberOnPersist(PX.Objects.SO.SOLine line)
  {
    return base.GenerateLotSerialNumberOnPersist(line) && this.IsLSEntryEnabled;
  }

  protected override Decimal? GetSerialStatusAvailableQty(ILotSerial lsmaster, IStatus accumavail)
  {
    Decimal? statusAvailableQty = base.GetSerialStatusAvailableQty(lsmaster, accumavail);
    INSiteLotSerial inSiteLotSerial = INSiteLotSerial.PK.Find((PXGraph) this.Base, lsmaster.InventoryID, lsmaster.SiteID, lsmaster.LotSerialNbr);
    PXCache cach = ((PXGraph) this.Base).Caches[typeof (SiteLotSerial)];
    SiteLotSerial siteLotSerial1 = new SiteLotSerial();
    siteLotSerial1.InventoryID = lsmaster.InventoryID;
    siteLotSerial1.SiteID = lsmaster.SiteID;
    siteLotSerial1.LotSerialNbr = lsmaster.LotSerialNbr;
    SiteLotSerial siteLotSerial2 = (SiteLotSerial) cach.Locate((object) siteLotSerial1);
    Decimal val2 = ((Decimal?) inSiteLotSerial?.QtyAvail).GetValueOrDefault() + ((Decimal?) siteLotSerial2?.QtyAvail).GetValueOrDefault();
    return new Decimal?(Math.Min(statusAvailableQty.GetValueOrDefault(), val2));
  }

  protected override Decimal? GetSerialStatusQtyOnHand(ILotSerial lsmaster)
  {
    Decimal? serialStatusQtyOnHand = base.GetSerialStatusQtyOnHand(lsmaster);
    INSiteLotSerial inSiteLotSerial = INSiteLotSerial.PK.Find((PXGraph) this.Base, lsmaster.InventoryID, lsmaster.SiteID, lsmaster.LotSerialNbr);
    PXCache cach = ((PXGraph) this.Base).Caches[typeof (SiteLotSerial)];
    SiteLotSerial siteLotSerial1 = new SiteLotSerial();
    siteLotSerial1.InventoryID = lsmaster.InventoryID;
    siteLotSerial1.SiteID = lsmaster.SiteID;
    siteLotSerial1.LotSerialNbr = lsmaster.LotSerialNbr;
    SiteLotSerial siteLotSerial2 = (SiteLotSerial) cach.Locate((object) siteLotSerial1);
    Decimal val2 = ((Decimal?) inSiteLotSerial?.QtyHardAvail).GetValueOrDefault() + ((Decimal?) siteLotSerial2?.QtyHardAvail).GetValueOrDefault();
    return new Decimal?(Math.Min(serialStatusQtyOnHand.GetValueOrDefault(), val2));
  }

  internal override List<ILotSerial> PerformSelectSerial<TLotSerialStatus>(
    PXSelectBase cmd,
    object[] pars)
  {
    List<object> objectList = cmd.View.SelectMultiBound(pars, Array.Empty<object>());
    List<ILotSerial> lotSerialList = new List<ILotSerial>(objectList.Count);
    foreach (object obj in objectList)
    {
      TLotSerialStatus lotSerialStatus;
      if (obj is PXResult pxResult)
      {
        lotSerialStatus = pxResult.GetItem<TLotSerialStatus>();
        INSiteLotSerial inSiteLotSerial = pxResult.GetItem<INSiteLotSerial>();
        if (inSiteLotSerial != null && inSiteLotSerial.SiteID.HasValue)
          PrimaryKeyOf<INSiteLotSerial>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<INSiteLotSerial.inventoryID, INSiteLotSerial.siteID, INSiteLotSerial.lotSerialNbr>>.StoreResult((PXGraph) this.Base, (TypeArrayOf<IBqlField>.IFilledWith<INSiteLotSerial.inventoryID, INSiteLotSerial.siteID, INSiteLotSerial.lotSerialNbr>) inSiteLotSerial, false);
      }
      else
        lotSerialStatus = (TLotSerialStatus) obj;
      lotSerialList.Add((ILotSerial) lotSerialStatus);
    }
    return lotSerialList;
  }

  protected override IDisposable InvtMultModeScope(PX.Objects.SO.SOLine line)
  {
    return (IDisposable) new SOOrderLineSplittingExtension.SOLineInvtMultScope(line);
  }

  protected override IDisposable InvtMultModeScope(PX.Objects.SO.SOLine line, PX.Objects.SO.SOLine oldLine)
  {
    return (IDisposable) new SOOrderLineSplittingExtension.SOLineInvtMultScope(line, oldLine);
  }

  public class SOLineInvtMultScope : 
    LineSplittingExtension<SOOrderEntry, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>.InvtMultScope
  {
    private static readonly Dictionary<PX.Objects.SO.SOLine, SOOrderLineSplittingExtension.SOLineInvtMultScope> LinesReversedInScope = new Dictionary<PX.Objects.SO.SOLine, SOOrderLineSplittingExtension.SOLineInvtMultScope>();

    public SOLineInvtMultScope(PX.Objects.SO.SOLine line)
      : base(line)
    {
      if (!this._reverse.GetValueOrDefault())
        return;
      PX.Objects.SO.SOLine line1 = this._line;
      Decimal? nullable1 = this._line.OpenQty;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      line1.OpenQty = nullable2;
      PX.Objects.SO.SOLine line2 = this._line;
      nullable1 = this._line.BaseOpenQty;
      Decimal? nullable3 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      line2.BaseOpenQty = nullable3;
      PX.Objects.SO.SOLine line3 = this._line;
      nullable1 = this._line.ClosedQty;
      Decimal? nullable4 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      line3.ClosedQty = nullable4;
      PX.Objects.SO.SOLine line4 = this._line;
      nullable1 = this._line.BaseClosedQty;
      Decimal? nullable5 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      line4.BaseClosedQty = nullable5;
      this.AddReversedLineScope(this._line);
    }

    public SOLineInvtMultScope(PX.Objects.SO.SOLine line, PX.Objects.SO.SOLine oldLine)
      : base(line, oldLine)
    {
      Decimal? nullable1;
      if (this._reverse.GetValueOrDefault())
      {
        PX.Objects.SO.SOLine line1 = this._line;
        Decimal? openQty = this._line.OpenQty;
        Decimal? nullable2 = openQty.HasValue ? new Decimal?(-openQty.GetValueOrDefault()) : new Decimal?();
        line1.OpenQty = nullable2;
        PX.Objects.SO.SOLine line2 = this._line;
        nullable1 = this._line.BaseOpenQty;
        Decimal? nullable3 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
        line2.BaseOpenQty = nullable3;
        PX.Objects.SO.SOLine line3 = this._line;
        nullable1 = this._line.ClosedQty;
        Decimal? nullable4 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
        line3.ClosedQty = nullable4;
        PX.Objects.SO.SOLine line4 = this._line;
        nullable1 = this._line.BaseClosedQty;
        Decimal? nullable5 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
        line4.BaseClosedQty = nullable5;
        this.AddReversedLineScope(this._line);
      }
      if (!this._reverseOld.GetValueOrDefault())
        return;
      PX.Objects.SO.SOLine oldLine1 = this._oldLine;
      nullable1 = this._oldLine.OpenQty;
      Decimal? nullable6 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      oldLine1.OpenQty = nullable6;
      PX.Objects.SO.SOLine oldLine2 = this._oldLine;
      nullable1 = this._oldLine.BaseOpenQty;
      Decimal? nullable7 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      oldLine2.BaseOpenQty = nullable7;
      PX.Objects.SO.SOLine oldLine3 = this._oldLine;
      nullable1 = this._oldLine.ClosedQty;
      Decimal? nullable8 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      oldLine3.ClosedQty = nullable8;
      PX.Objects.SO.SOLine oldLine4 = this._oldLine;
      nullable1 = this._oldLine.BaseClosedQty;
      Decimal? nullable9 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      oldLine4.BaseClosedQty = nullable9;
      this.AddReversedLineScope(this._oldLine);
    }

    protected override bool IsReverse(PX.Objects.SO.SOLine line)
    {
      if (this.Reversed(line))
        return false;
      short? lineSign = line.LineSign;
      int? nullable = lineSign.HasValue ? new int?((int) lineSign.GetValueOrDefault()) : new int?();
      int num = 0;
      return nullable.GetValueOrDefault() < num & nullable.HasValue;
    }

    public override void Dispose()
    {
      base.Dispose();
      Decimal? nullable1;
      if (this._reverse.GetValueOrDefault())
      {
        PX.Objects.SO.SOLine line1 = this._line;
        nullable1 = this._line.OpenQty;
        Decimal? nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
        line1.OpenQty = nullable2;
        PX.Objects.SO.SOLine line2 = this._line;
        nullable1 = this._line.BaseOpenQty;
        Decimal? nullable3 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
        line2.BaseOpenQty = nullable3;
        PX.Objects.SO.SOLine line3 = this._line;
        nullable1 = this._line.ClosedQty;
        Decimal? nullable4 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
        line3.ClosedQty = nullable4;
        PX.Objects.SO.SOLine line4 = this._line;
        nullable1 = this._line.BaseClosedQty;
        Decimal? nullable5 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
        line4.BaseClosedQty = nullable5;
      }
      if (this._reverseOld.GetValueOrDefault())
      {
        PX.Objects.SO.SOLine oldLine1 = this._oldLine;
        nullable1 = this._oldLine.OpenQty;
        Decimal? nullable6 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
        oldLine1.OpenQty = nullable6;
        PX.Objects.SO.SOLine oldLine2 = this._oldLine;
        nullable1 = this._oldLine.BaseOpenQty;
        Decimal? nullable7 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
        oldLine2.BaseOpenQty = nullable7;
        PX.Objects.SO.SOLine oldLine3 = this._oldLine;
        nullable1 = this._oldLine.ClosedQty;
        Decimal? nullable8 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
        oldLine3.ClosedQty = nullable8;
        PX.Objects.SO.SOLine oldLine4 = this._oldLine;
        nullable1 = this._oldLine.BaseClosedQty;
        Decimal? nullable9 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
        oldLine4.BaseClosedQty = nullable9;
      }
      this.DisposeReversedLineScope(this._line);
      this.DisposeReversedLineScope(this._oldLine);
    }

    private bool Reversed(PX.Objects.SO.SOLine line)
    {
      return line != null && SOOrderLineSplittingExtension.SOLineInvtMultScope.LinesReversedInScope.ContainsKey(line);
    }

    private void AddReversedLineScope(PX.Objects.SO.SOLine line)
    {
      if (line == null || SOOrderLineSplittingExtension.SOLineInvtMultScope.LinesReversedInScope.ContainsKey(line))
        return;
      SOOrderLineSplittingExtension.SOLineInvtMultScope.LinesReversedInScope[line] = this;
    }

    private void DisposeReversedLineScope(PX.Objects.SO.SOLine line)
    {
      SOOrderLineSplittingExtension.SOLineInvtMultScope lineInvtMultScope;
      if (line == null || !SOOrderLineSplittingExtension.SOLineInvtMultScope.LinesReversedInScope.TryGetValue(line, out lineInvtMultScope) || lineInvtMultScope != this)
        return;
      SOOrderLineSplittingExtension.SOLineInvtMultScope.LinesReversedInScope.Remove(line);
    }
  }
}

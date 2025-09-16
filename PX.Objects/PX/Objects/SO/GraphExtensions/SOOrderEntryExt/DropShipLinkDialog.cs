// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.DropShipLinkDialog
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.DAC;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class DropShipLinkDialog : PXGraphExtension<POLinkDialog, PurchaseSupplyBaseExt, SOOrderEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.dropShipments>();

  public virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SupplyPOLine.selected> e)
  {
    PX.Objects.SO.SupplyPOLine row = (PX.Objects.SO.SupplyPOLine) e.Row;
    PX.Objects.SO.SOLine soLine = PXResultset<PX.Objects.SO.SOLine>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOLine>) this.Base2.SOLineDemand).Select(Array.Empty<object>()));
    if (row == null)
      return;
    bool? oldValue = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.SO.SupplyPOLine.selected>, object, object>) e).OldValue;
    bool? selected = row.Selected;
    if (oldValue.GetValueOrDefault() == selected.GetValueOrDefault() & oldValue.HasValue == selected.HasValue || soLine?.POSource != "D")
      return;
    bool? nullable = soLine.IsLegacyDropShip;
    if (nullable.GetValueOrDefault())
      return;
    nullable = row.Selected;
    if (nullable.GetValueOrDefault())
      row.SelectedSOLines = row.SelectedSOLines.SparseArrayAddDistinct<int>(new int?(soLine.LineNbr.Value));
    else
      row.SelectedSOLines.SparseArrayRemove<int>(new int?(soLine.LineNbr.Value));
  }

  public virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SupplyPOLine> e)
  {
    PX.Objects.SO.SOLine current = ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current;
    if (current == null || e.Row == null || current.POSource != "D" || current.IsLegacyDropShip.GetValueOrDefault())
      return;
    DropShipLink dropShipLink = ((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.GetDropShipLink(current);
    int num1;
    if (dropShipLink != null)
    {
      Decimal? baseReceivedQty = dropShipLink.BaseReceivedQty;
      Decimal num2 = 0M;
      num1 = baseReceivedQty.GetValueOrDefault() > num2 & baseReceivedQty.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag = num1 != 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SupplyPOLine.selected>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SupplyPOLine>>) e).Cache, (object) e.Row, !flag && (dropShipLink == null || !dropShipLink.InReceipt.GetValueOrDefault()));
    if (!(e.Row.Selected.GetValueOrDefault() & flag))
      return;
    PXUIFieldAttribute.SetWarning<PX.Objects.SO.SupplyPOLine.selected>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SupplyPOLine>>) e).Cache, (object) e.Row, "The purchase order cannot be deselected because it has been fully or partially received.");
  }

  public virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SupplyPOLine> e)
  {
    PX.Objects.SO.SOLine soLine = PXResultset<PX.Objects.SO.SOLine>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOLine>) this.Base2.SOLineDemand).Select(Array.Empty<object>()));
    bool? selected1 = (bool?) e.OldRow?.Selected;
    bool? selected2 = e.Row.Selected;
    if (selected1.GetValueOrDefault() == selected2.GetValueOrDefault() & selected1.HasValue == selected2.HasValue || soLine?.POSource != "D" || soLine.IsLegacyDropShip.GetValueOrDefault())
      return;
    int? lineNbr1;
    int num1;
    if (!e.Row.Selected.GetValueOrDefault())
    {
      int?[] selectedSoLines = e.Row.SelectedSOLines;
      lineNbr1 = soLine.LineNbr;
      int? nullable = new int?(lineNbr1.Value);
      num1 = ((IEnumerable<int?>) selectedSoLines).Contains<int?>(nullable) ? 1 : 0;
    }
    else
      num1 = 1;
    if (num1 == 0)
      return;
    foreach (PXResult<PX.Objects.SO.SupplyPOLine> pxResult in ((PXSelectBase<PX.Objects.SO.SupplyPOLine>) this.Base2.SupplyPOLines).Select(Array.Empty<object>()))
    {
      PX.Objects.SO.SupplyPOLine supplyPoLine = PXResult<PX.Objects.SO.SupplyPOLine>.op_Implicit(pxResult);
      int num2;
      if (!supplyPoLine.Selected.GetValueOrDefault())
      {
        int?[] selectedSoLines = supplyPoLine.SelectedSOLines;
        lineNbr1 = soLine.LineNbr;
        int? nullable = new int?(lineNbr1.Value);
        num2 = ((IEnumerable<int?>) selectedSoLines).Contains<int?>(nullable) ? 1 : 0;
      }
      else
        num2 = 1;
      if (num2 != 0)
      {
        if (!(e.Row.OrderType != supplyPoLine.OrderType) && !(e.Row.OrderNbr != supplyPoLine.OrderNbr))
        {
          lineNbr1 = e.Row.LineNbr;
          int? lineNbr2 = supplyPoLine.LineNbr;
          if (lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue)
            continue;
        }
        supplyPoLine.Selected = new bool?(false);
        supplyPoLine.SelectedSOLines.SparseArrayRemove<int>(new int?(soLine.LineNbr.Value));
        ((PXSelectBase<PX.Objects.SO.SupplyPOLine>) this.Base2.SupplyPOLines).Update(supplyPoLine);
      }
    }
    ((PXSelectBase<PX.Objects.SO.SupplyPOLine>) this.Base2.SupplyPOLines).Current = e.Row;
    ((PXSelectBase) this.Base2.SupplyPOLines).View.RequestRefresh();
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOOrderEntryExt.POLinkDialog.CollectSupplyPOLines(PX.Objects.SO.SOLine,System.Collections.Generic.ICollection{PX.Objects.SO.SupplyPOLine})" />
  /// </summary>
  [PXOverride]
  public virtual void CollectSupplyPOLines(
    PX.Objects.SO.SOLine currentSOLine,
    ICollection<PX.Objects.SO.SupplyPOLine> supplyPOLines)
  {
    if (currentSOLine.POSource != "D" || currentSOLine.IsLegacyDropShip.GetValueOrDefault())
      return;
    PXSelectJoin<PX.Objects.SO.SupplyPOLine, InnerJoin<PX.Objects.SO.SupplyPOOrder, On<PX.Objects.SO.SupplyPOLine.FK.SupplyOrder>, LeftJoin<DropShipLink, On<DropShipLink.FK.SupplyPOLine>>>, Where<PX.Objects.SO.SupplyPOLine.lineType, In3<POLineType.goodsForDropShip, POLineType.nonStockForDropShip>, And2<Where<PX.Objects.SO.SupplyPOLine.vendorID, Equal<Current<PX.Objects.SO.SOLine.vendorID>>, Or<Current<PX.Objects.SO.SOLine.vendorID>, IsNull>>, And<PX.Objects.SO.SupplyPOOrder.orderType, Equal<POOrderType.dropShip>, And2<Where<PX.Objects.SO.SupplyPOOrder.shipDestType, NotEqual<POShippingDestination.customer>, Or<PX.Objects.SO.SupplyPOOrder.shipToBAccountID, Equal<Current<PX.Objects.SO.SOLine.customerID>>>>, And<Where<DropShipLink.sOOrderType, Equal<Current<PX.Objects.SO.SOLine.orderType>>, And<DropShipLink.sOOrderNbr, Equal<Current<PX.Objects.SO.SOLine.orderNbr>>, Or<DropShipLink.sOOrderNbr, IsNull, And<PX.Objects.SO.SupplyPOLine.inventoryID, Equal<Current<PX.Objects.SO.SOLine.inventoryID>>, And2<Where<PX.Objects.SO.SupplyPOLine.subItemID, Equal<Current<PX.Objects.SO.SOLine.subItemID>>, Or<Current<PX.Objects.SO.SOLine.subItemID>, IsNull>>, And<PX.Objects.SO.SupplyPOLine.baseOrderQty, Equal<Minus<Mult<Current<PX.Objects.SO.SOLine.baseOrderQty>, Current<PX.Objects.SO.SOLine.invtMult>>>>, And<PX.Objects.SO.SupplyPOLine.siteID, Equal<Current<PX.Objects.SO.SOLine.pOSiteID>>, And<PX.Objects.SO.SupplyPOLine.completed, Equal<False>, And<PX.Objects.SO.SupplyPOLine.cancelled, Equal<False>>>>>>>>>>>>>>>> pxSelectJoin = new PXSelectJoin<PX.Objects.SO.SupplyPOLine, InnerJoin<PX.Objects.SO.SupplyPOOrder, On<PX.Objects.SO.SupplyPOLine.FK.SupplyOrder>, LeftJoin<DropShipLink, On<DropShipLink.FK.SupplyPOLine>>>, Where<PX.Objects.SO.SupplyPOLine.lineType, In3<POLineType.goodsForDropShip, POLineType.nonStockForDropShip>, And2<Where<PX.Objects.SO.SupplyPOLine.vendorID, Equal<Current<PX.Objects.SO.SOLine.vendorID>>, Or<Current<PX.Objects.SO.SOLine.vendorID>, IsNull>>, And<PX.Objects.SO.SupplyPOOrder.orderType, Equal<POOrderType.dropShip>, And2<Where<PX.Objects.SO.SupplyPOOrder.shipDestType, NotEqual<POShippingDestination.customer>, Or<PX.Objects.SO.SupplyPOOrder.shipToBAccountID, Equal<Current<PX.Objects.SO.SOLine.customerID>>>>, And<Where<DropShipLink.sOOrderType, Equal<Current<PX.Objects.SO.SOLine.orderType>>, And<DropShipLink.sOOrderNbr, Equal<Current<PX.Objects.SO.SOLine.orderNbr>>, Or<DropShipLink.sOOrderNbr, IsNull, And<PX.Objects.SO.SupplyPOLine.inventoryID, Equal<Current<PX.Objects.SO.SOLine.inventoryID>>, And2<Where<PX.Objects.SO.SupplyPOLine.subItemID, Equal<Current<PX.Objects.SO.SOLine.subItemID>>, Or<Current<PX.Objects.SO.SOLine.subItemID>, IsNull>>, And<PX.Objects.SO.SupplyPOLine.baseOrderQty, Equal<Minus<Mult<Current<PX.Objects.SO.SOLine.baseOrderQty>, Current<PX.Objects.SO.SOLine.invtMult>>>>, And<PX.Objects.SO.SupplyPOLine.siteID, Equal<Current<PX.Objects.SO.SOLine.pOSiteID>>, And<PX.Objects.SO.SupplyPOLine.completed, Equal<False>, And<PX.Objects.SO.SupplyPOLine.cancelled, Equal<False>>>>>>>>>>>>>>>>((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base);
    List<PX.Objects.SO.SupplyPOLine> supplyPoLineList = new List<PX.Objects.SO.SupplyPOLine>();
    PX.Objects.SO.SupplyPOLine supplyPoLine1 = (PX.Objects.SO.SupplyPOLine) null;
    object[] objArray = Array.Empty<object>();
    foreach (PXResult<PX.Objects.SO.SupplyPOLine, PX.Objects.SO.SupplyPOOrder, DropShipLink> pxResult in ((PXSelectBase<PX.Objects.SO.SupplyPOLine>) pxSelectJoin).Select(objArray))
    {
      PX.Objects.SO.SupplyPOLine supplyLine = PXResult<PX.Objects.SO.SupplyPOLine, PX.Objects.SO.SupplyPOOrder, DropShipLink>.op_Implicit(pxResult);
      PX.Objects.SO.SupplyPOOrder supplyPoOrder = ((PXSelectBase<PX.Objects.SO.SupplyPOOrder>) ((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.SupplyPOOrders).Locate(PXResult<PX.Objects.SO.SupplyPOLine, PX.Objects.SO.SupplyPOOrder, DropShipLink>.op_Implicit(pxResult)) ?? PXResult<PX.Objects.SO.SupplyPOLine, PX.Objects.SO.SupplyPOOrder, DropShipLink>.op_Implicit(pxResult);
      DropShipLink dropShipLink = this.LocateActualLinkForSupplyLine(PXResult<PX.Objects.SO.SupplyPOLine, PX.Objects.SO.SupplyPOOrder, DropShipLink>.op_Implicit(pxResult), supplyLine);
      int? nullable1;
      if (dropShipLink != null)
      {
        if (!(dropShipLink.SOOrderType != currentSOLine.OrderType) && !(dropShipLink.SOOrderNbr != currentSOLine.OrderNbr))
        {
          int? soLineNbr = dropShipLink.SOLineNbr;
          nullable1 = currentSOLine.LineNbr;
          if (!(soLineNbr.GetValueOrDefault() == nullable1.GetValueOrDefault() & soLineNbr.HasValue == nullable1.HasValue))
            continue;
        }
        else
          continue;
      }
      string[] strArray = new string[4]
      {
        "H",
        "B",
        "D",
        "E"
      };
      bool flag = ((PXGraphExtension<SOOrderEntry>) this).Base.SOPOLinkShowDocumentsOnHold && EnumerableExtensions.IsIn<string>(supplyPoOrder.Status, (IEnumerable<string>) strArray);
      if ((supplyPoOrder.SOOrderNbr != null || !flag && !(supplyPoOrder.Status == "A") ? (!(supplyPoOrder.SOOrderNbr == currentSOLine.OrderNbr) ? 0 : (supplyPoOrder.SOOrderType == currentSOLine.OrderType ? 1 : 0)) : 1) != 0)
      {
        if (((PXSelectBase) this.Base2.SupplyPOLines).Cache.GetStatus((object) supplyLine) == null)
        {
          PX.Objects.SO.SupplyPOLine supplyPoLine2 = supplyLine;
          int?[] nullableArray1;
          if (dropShipLink == null)
          {
            nullableArray1 = new int?[0];
          }
          else
          {
            nullableArray1 = new int?[1];
            nullable1 = dropShipLink.SOLineNbr;
            nullableArray1[0] = new int?(nullable1.Value);
          }
          supplyPoLine2.SelectedSOLines = nullableArray1;
          PX.Objects.SO.SupplyPOLine supplyPoLine3 = supplyLine;
          int?[] nullableArray2;
          if (dropShipLink == null)
          {
            nullableArray2 = new int?[0];
          }
          else
          {
            nullableArray2 = new int?[1];
            nullable1 = dropShipLink.SOLineNbr;
            nullableArray2[0] = new int?(nullable1.Value);
          }
          supplyPoLine3.LinkedSOLines = nullableArray2;
        }
        else if (dropShipLink != null)
        {
          PX.Objects.SO.SupplyPOLine supplyPoLine4 = supplyLine;
          int?[] nullableArray = new int?[1];
          nullable1 = dropShipLink.SOLineNbr;
          nullableArray[0] = new int?(nullable1.Value);
          supplyPoLine4.LinkedSOLines = nullableArray;
        }
        else
          supplyLine.LinkedSOLines.SparseArrayClear<int>();
        PX.Objects.SO.SupplyPOLine supplyPoLine5 = supplyLine;
        int?[] selectedSoLines = supplyLine.SelectedSOLines;
        nullable1 = currentSOLine.LineNbr;
        int? nullable2 = new int?(nullable1.Value);
        bool? nullable3 = new bool?(((IEnumerable<int?>) selectedSoLines).Contains<int?>(nullable2));
        supplyPoLine5.Selected = nullable3;
        GraphHelper.Hold(((PXSelectBase) this.Base2.SupplyPOLines).Cache, (object) supplyLine);
        if (supplyLine.Selected.GetValueOrDefault() && dropShipLink != null)
        {
          Decimal? baseReceivedQty = dropShipLink.BaseReceivedQty;
          Decimal num = 0M;
          if (baseReceivedQty.GetValueOrDefault() > num & baseReceivedQty.HasValue)
          {
            supplyPoLine1 = supplyLine;
            break;
          }
        }
        supplyPoLineList.Add(supplyLine);
      }
    }
    if (supplyPoLine1 != null)
      supplyPOLines.Add(supplyPoLine1);
    foreach (PX.Objects.SO.SupplyPOLine supplyPoLine6 in supplyPoLineList)
      supplyPOLines.Add(supplyPoLine6);
  }

  protected virtual DropShipLink LocateActualLinkForSupplyLine(
    DropShipLink link,
    PX.Objects.SO.SupplyPOLine supplyLine)
  {
    DropShipLink dropShipLink = ((PXSelectBase) ((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.DropShipLinks).Cache.Inserted.Cast<DropShipLink>().FirstOrDefault<DropShipLink>((Func<DropShipLink, bool>) (l =>
    {
      if (!(l.POOrderType == supplyLine.OrderType) || !(l.POOrderNbr == supplyLine.OrderNbr))
        return false;
      int? poLineNbr = l.POLineNbr;
      int? lineNbr = supplyLine.LineNbr;
      return poLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & poLineNbr.HasValue == lineNbr.HasValue;
    }));
    if (dropShipLink != null)
      return dropShipLink;
    return (link != null ? (!link.SOLineNbr.HasValue ? 1 : 0) : 1) != 0 || EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) ((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.DropShipLinks).Cache.GetStatus((object) link), (PXEntryStatus) 3, (PXEntryStatus) 4) ? (DropShipLink) null : ((PXSelectBase<DropShipLink>) ((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.DropShipLinks).Locate(link) ?? link;
  }

  [PXOverride]
  public virtual void LinkPOSupply(PX.Objects.SO.SOLine currentSOLine)
  {
    if (currentSOLine.POSource != "D" || currentSOLine.IsLegacyDropShip.GetValueOrDefault())
      return;
    List<PX.Objects.SO.SOLineSplit> list = ((IEnumerable<PX.Objects.SO.SOLineSplit>) ((PXGraphExtension<SOOrderEntry>) this).Base.LineSplittingAllocatedExt.SelectSplitsForDropShip(currentSOLine)).ToList<PX.Objects.SO.SOLineSplit>();
    PX.Objects.SO.SOLineSplit split = list.FirstOrDefault<PX.Objects.SO.SOLineSplit>();
    if (split == null)
      return;
    if (((PXGraphExtension<SOOrderEntry>) this).Base.LineSplittingAllocatedExt.HasMultipleSplitsOrAllocation(currentSOLine))
      throw new PXException("The line cannot be drop-shipped because it is split into multiple lines or allocated in the Line Details dialog box.");
    bool flag1 = false;
    foreach (PX.Objects.SO.SupplyPOLine supply in ((PXSelectBase) this.Base2.SupplyPOLines).Cache.Updated)
    {
      int?[] selectedSoLines = supply.SelectedSOLines;
      int? nullable1 = currentSOLine.LineNbr;
      int? nullable2 = new int?(nullable1.Value);
      int num1 = ((IEnumerable<int?>) selectedSoLines).Contains<int?>(nullable2) ? 1 : 0;
      DropShipLink dropShipLink = ((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.GetDropShipLink(currentSOLine);
      int num2;
      if (dropShipLink != null && dropShipLink.POOrderType == supply.OrderType && dropShipLink.POOrderNbr == supply.OrderNbr)
      {
        nullable1 = dropShipLink.POLineNbr;
        int? lineNbr = supply.LineNbr;
        num2 = nullable1.GetValueOrDefault() == lineNbr.GetValueOrDefault() & nullable1.HasValue == lineNbr.HasValue ? 1 : 0;
      }
      else
        num2 = 0;
      bool flag2 = num2 != 0;
      if (num1 == 0 & flag2)
      {
        this.UnlinkFromSupplyLine(supply, dropShipLink, list, currentSOLine.POCreate);
        flag1 = true;
      }
    }
    bool flag3 = false;
    foreach (PX.Objects.SO.SupplyPOLine supply in ((PXSelectBase) this.Base2.SupplyPOLines).Cache.Updated)
    {
      int?[] selectedSoLines = supply.SelectedSOLines;
      int? lineNbr = currentSOLine.LineNbr;
      int? nullable3 = new int?(lineNbr.Value);
      int num3 = ((IEnumerable<int?>) selectedSoLines).Contains<int?>(nullable3) ? 1 : 0;
      DropShipLink dropShipLink = ((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.GetDropShipLink(currentSOLine);
      if (num3 != 0 && dropShipLink == null)
      {
        bool? isAllocated = split.IsAllocated;
        bool flag4 = false;
        if (isAllocated.GetValueOrDefault() == flag4 & isAllocated.HasValue)
        {
          bool? completed = split.Completed;
          bool flag5 = false;
          if (completed.GetValueOrDefault() == flag5 & completed.HasValue)
          {
            Decimal? baseQty1 = split.BaseQty;
            Decimal num4 = 0M;
            if (baseQty1.GetValueOrDefault() > num4 & baseQty1.HasValue && string.IsNullOrEmpty(split.SOOrderNbr) && string.IsNullOrEmpty(split.PONbr))
            {
              Decimal? baseQty2 = split.BaseQty;
              short? invtMult = currentSOLine.InvtMult;
              Decimal? nullable4 = invtMult.HasValue ? new Decimal?((Decimal) (int) -invtMult.GetValueOrDefault()) : new Decimal?();
              Decimal? baseQty3 = currentSOLine.BaseQty;
              Decimal? nullable5 = nullable4.HasValue & baseQty3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * baseQty3.GetValueOrDefault()) : new Decimal?();
              if (baseQty2.GetValueOrDefault() == nullable5.GetValueOrDefault() & baseQty2.HasValue == nullable5.HasValue)
              {
                if (PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Required<INItemPlan.planID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[1]
                {
                  (object) supply.PlanID
                })) != null)
                {
                  this.LinkToSupplyLine(split, supply);
                  flag3 = true;
                  PX.Objects.SO.SupplyPOLine supplyPoLine = supply;
                  int?[] linkedSoLines = supply.LinkedSOLines;
                  lineNbr = currentSOLine.LineNbr;
                  int? nullable6 = new int?(lineNbr.Value);
                  int?[] nullableArray = linkedSoLines.SparseArrayAddDistinct<int>(nullable6);
                  supplyPoLine.LinkedSOLines = nullableArray;
                }
              }
            }
          }
        }
      }
    }
    PX.Objects.SO.SOLine current = ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current;
    if (flag3)
    {
      if (current.POCreated.GetValueOrDefault())
        return;
      ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache.SetValue<PX.Objects.SO.SOLine.pOCreated>((object) current, (object) true);
    }
    else
    {
      if (!flag1 || !current.POCreated.GetValueOrDefault())
        return;
      bool flag6 = list.Any<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (x => x.POCreate.GetValueOrDefault() && x.PONbr != null));
      if (flag6)
        return;
      ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache.SetValue<PX.Objects.SO.SOLine.pOCreated>((object) current, (object) flag6);
    }
  }

  public virtual void LinkToSupplyLine(PX.Objects.SO.SOLineSplit split, PX.Objects.SO.SupplyPOLine supply, bool linkActive = true)
  {
    INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Required<INItemPlan.planID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[1]
    {
      (object) split.PlanID
    }));
    if (inItemPlan != null && !inItemPlan.SupplyPlanID.HasValue)
    {
      inItemPlan.SupplyPlanID = supply.PlanID;
      ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (INItemPlan)].Update((object) inItemPlan);
    }
    split.POCreate = new bool?(true);
    split.VendorID = supply.VendorID;
    split.POType = supply.OrderType;
    split.PONbr = supply.OrderNbr;
    split.POLineNbr = supply.LineNbr;
    split = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Update(split);
    PX.Objects.SO.SOLine soLine = PXParentAttribute.SelectParent<PX.Objects.SO.SOLine>(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache, (object) split);
    soLine.POCreate = new bool?(true);
    soLine.VendorID = supply.VendorID;
    soLine.POOrderType = supply.OrderType;
    soLine.POOrderNbr = supply.OrderNbr;
    soLine.POLineNbr = supply.LineNbr;
    soLine.POLinkActive = new bool?(linkActive);
    ((PXSelectBase<DropShipLink>) ((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.DropShipLinks).Insert(new DropShipLink()
    {
      SOOrderType = split.OrderType,
      SOOrderNbr = split.OrderNbr,
      SOLineNbr = split.LineNbr,
      POOrderType = supply.OrderType,
      POOrderNbr = supply.OrderNbr,
      POLineNbr = supply.LineNbr,
      Active = new bool?(linkActive),
      SOInventoryID = supply.InventoryID,
      SOSiteID = supply.SiteID,
      SOBaseOrderQty = supply.BaseOrderQty,
      POInventoryID = supply.InventoryID,
      POSiteID = supply.SiteID,
      POBaseOrderQty = supply.BaseOrderQty
    });
    PX.Objects.SO.SupplyPOOrder parent = KeysRelation<CompositeKey<Field<PX.Objects.SO.SupplyPOLine.orderType>.IsRelatedTo<PX.Objects.SO.SupplyPOOrder.orderType>, Field<PX.Objects.SO.SupplyPOLine.orderNbr>.IsRelatedTo<PX.Objects.SO.SupplyPOOrder.orderNbr>>.WithTablesOf<PX.Objects.SO.SupplyPOOrder, PX.Objects.SO.SupplyPOLine>, PX.Objects.SO.SupplyPOOrder, PX.Objects.SO.SupplyPOLine>.FindParent((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, supply, (PKFindOptions) 0);
    PX.Objects.SO.SupplyPOOrder supplyPoOrder = ((PXSelectBase<PX.Objects.SO.SupplyPOOrder>) ((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.SupplyPOOrders).Locate(parent) ?? parent;
    if (supplyPoOrder.SOOrderNbr != null)
      return;
    supplyPoOrder.SOOrderType = split.OrderType;
    supplyPoOrder.SOOrderNbr = split.OrderNbr;
    ((PXSelectBase<PX.Objects.SO.SupplyPOOrder>) ((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.SupplyPOOrders).Update(supplyPoOrder);
  }

  protected virtual void UnlinkFromSupplyLine(
    PX.Objects.SO.SupplyPOLine supply,
    DropShipLink link,
    List<PX.Objects.SO.SOLineSplit> splits,
    bool? poCreate)
  {
    if (splits.Count == 0)
      return;
    PX.Objects.SO.SupplyPOOrder parent = KeysRelation<CompositeKey<Field<PX.Objects.SO.SupplyPOLine.orderType>.IsRelatedTo<PX.Objects.SO.SupplyPOOrder.orderType>, Field<PX.Objects.SO.SupplyPOLine.orderNbr>.IsRelatedTo<PX.Objects.SO.SupplyPOOrder.orderNbr>>.WithTablesOf<PX.Objects.SO.SupplyPOOrder, PX.Objects.SO.SupplyPOLine>, PX.Objects.SO.SupplyPOOrder, PX.Objects.SO.SupplyPOLine>.FindParent((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, supply, (PKFindOptions) 0);
    PX.Objects.SO.SOLineSplit soLineSplit1 = splits.First<PX.Objects.SO.SOLineSplit>();
    foreach (PX.Objects.SO.SOLineSplit split in splits)
    {
      bool? nullable1 = split.POCompleted;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = split.Completed;
        if (!nullable1.GetValueOrDefault() && split.POType == supply.OrderType && split.PONbr == supply.OrderNbr)
        {
          int? poLineNbr = split.POLineNbr;
          int? lineNbr = supply.LineNbr;
          if (poLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & poLineNbr.HasValue == lineNbr.HasValue)
          {
            if (parent != null)
            {
              Guid? refNoteId = split.RefNoteID;
              Guid? nullable2 = parent.NoteID;
              if ((refNoteId.HasValue == nullable2.HasValue ? (refNoteId.HasValue ? (refNoteId.GetValueOrDefault() == nullable2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
              {
                PX.Objects.SO.SOLineSplit soLineSplit2 = split;
                nullable2 = new Guid?();
                Guid? nullable3 = nullable2;
                soLineSplit2.RefNoteID = nullable3;
              }
            }
            split.ClearPOReferences();
            split.POCreate = poCreate;
            split.POCompleted = new bool?(false);
            split.ReceivedQty = new Decimal?(0M);
            split.ShippedQty = new Decimal?(0M);
            split.Completed = new bool?(false);
            soLineSplit1 = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Update(split);
          }
        }
      }
    }
    PX.Objects.SO.SOLine soLine = PXParentAttribute.SelectParent<PX.Objects.SO.SOLine>(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache, (object) soLineSplit1);
    soLine.POCreate = poCreate;
    soLine.POOrderType = (string) null;
    soLine.POOrderNbr = (string) null;
    soLine.POLineNbr = new int?();
    soLine.POLinkActive = new bool?(false);
    ((PXSelectBase<DropShipLink>) ((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.DropShipLinks).Delete(link);
    int?[] linkedSoLines = supply.LinkedSOLines;
    int? nullable;
    if (linkedSoLines != null)
    {
      nullable = soLineSplit1.LineNbr;
      linkedSoLines.SparseArrayRemove<int>(new int?(nullable.Value));
    }
    PX.Objects.SO.SupplyPOOrder supplyPoOrder = ((PXSelectBase<PX.Objects.SO.SupplyPOOrder>) ((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.SupplyPOOrders).Locate(parent) ?? parent;
    nullable = supplyPoOrder.DropShipLinkedLinesCount;
    int num = 0;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      return;
    supplyPoOrder.SOOrderType = (string) null;
    supplyPoOrder.SOOrderNbr = (string) null;
    ((PXSelectBase<PX.Objects.SO.SupplyPOOrder>) ((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.SupplyPOOrders).Update(supplyPoOrder);
  }

  public virtual void UnlinkSOLineFromSupplyLine(DropShipLink link, PX.Objects.SO.SOLine soLine)
  {
    PX.Objects.SO.SupplyPOLine parent = KeysRelation<CompositeKey<Field<DropShipLink.pOOrderType>.IsRelatedTo<PX.Objects.SO.SupplyPOLine.orderType>, Field<DropShipLink.pOOrderNbr>.IsRelatedTo<PX.Objects.SO.SupplyPOLine.orderNbr>, Field<DropShipLink.pOLineNbr>.IsRelatedTo<PX.Objects.SO.SupplyPOLine.lineNbr>>.WithTablesOf<PX.Objects.SO.SupplyPOLine, DropShipLink>, PX.Objects.SO.SupplyPOLine, DropShipLink>.FindParent((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, link, (PKFindOptions) 0);
    PX.Objects.SO.SupplyPOLine supply = ((PXSelectBase<PX.Objects.SO.SupplyPOLine>) this.Base2.SupplyPOLines).Locate(parent) ?? parent;
    if (link == null || supply == null)
      return;
    if (soLine == null)
      throw new ArgumentNullException(nameof (soLine));
    List<PX.Objects.SO.SOLineSplit> list = GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) PXSelectBase<PX.Objects.SO.SOLineSplit, PXSelect<PX.Objects.SO.SOLineSplit, Where<PX.Objects.SO.SOLineSplit.orderType, Equal<Required<DropShipLink.sOOrderType>>, And<PX.Objects.SO.SOLineSplit.orderNbr, Equal<Required<DropShipLink.sOOrderNbr>>, And<PX.Objects.SO.SOLineSplit.lineNbr, Equal<Required<DropShipLink.sOLineNbr>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[3]
    {
      (object) link.SOOrderType,
      (object) link.SOOrderNbr,
      (object) link.SOLineNbr
    })).ToList<PX.Objects.SO.SOLineSplit>();
    soLine = ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Locate(soLine) ?? soLine;
    this.UnlinkFromSupplyLine(supply, link, list, soLine.POCreate);
    int?[] selectedSoLines = supply.SelectedSOLines;
    if (selectedSoLines != null)
      selectedSoLines.SparseArrayRemove<int>(new int?(link.SOLineNbr.Value));
    if (!soLine.POCreate.GetValueOrDefault())
      ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache.SetValueExt<PX.Objects.SO.SOLine.pOSource>((object) soLine, (object) null);
    if (!soLine.POCreated.GetValueOrDefault())
      return;
    ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache.SetValueExt<PX.Objects.SO.SOLine.pOCreated>((object) soLine, (object) false);
  }

  public virtual PX.Objects.SO.SOLine CreateSOLineFromDropShipLine(PX.Objects.PO.POLine line, bool isLegacy)
  {
    PX.Objects.SO.SOLine copy1 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Insert());
    copy1.IsStockItem = line.IsStockItem;
    copy1.InventoryID = line.InventoryID;
    copy1.SubItemID = line.SubItemID;
    copy1.TranDesc = line.TranDesc;
    copy1.OrderQty = line.OrderQty;
    copy1.UOM = line.UOM;
    copy1.TaxCategoryID = line.TaxCategoryID;
    copy1.SiteID = line.SiteID;
    copy1.ProjectID = line.ProjectID;
    copy1.TaskID = line.TaskID;
    copy1.CostCodeID = line.CostCodeID;
    copy1.IsLegacyDropShip = new bool?(isLegacy);
    PX.Objects.SO.SOLine copy2 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Update(copy1));
    copy2.POCreate = new bool?(true);
    copy2.POSource = "D";
    copy2.POSiteID = line.SiteID;
    copy2.VendorID = line.VendorID;
    return PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Update(copy2));
  }
}

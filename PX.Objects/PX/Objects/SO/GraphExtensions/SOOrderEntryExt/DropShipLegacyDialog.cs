// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.DropShipLegacyDialog
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

[Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1. Use DropShipLinkDialog instead.")]
public class DropShipLegacyDialog : 
  PXGraphExtension<POLinkDialog, PurchaseSupplyBaseExt, SOOrderEntry>
{
  public PXSelect<PX.Objects.SO.SOLine, Where<PX.Objects.SO.SOLine.orderType, Equal<Optional<PX.Objects.SO.SOLine.orderType>>, And<PX.Objects.SO.SOLine.orderNbr, Equal<Optional<PX.Objects.SO.SOLine.orderNbr>>, And<PX.Objects.SO.SOLine.lineNbr, Equal<Optional<PX.Objects.SO.SOLine.lineNbr>>>>>> currentposupply;
  [PXCopyPasteHiddenView]
  public PXSelect<POLine3> posupply;
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.PO.POOrder> poorderlink;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.dropShipments>();

  public virtual IEnumerable POSupply()
  {
    PX.Objects.SO.SOLine soLine = PXResultset<PX.Objects.SO.SOLine>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOLine>) this.currentposupply).Select(Array.Empty<object>()));
    List<POLine3> poLine3List = new List<POLine3>();
    if (soLine == null || soLine.POSource != "D" || !soLine.IsLegacyDropShip.GetValueOrDefault())
      return (IEnumerable) poLine3List;
    List<DropShipLegacyDialog.POSupplyResult> source1 = new List<DropShipLegacyDialog.POSupplyResult>();
    foreach (PXResult<POLine3, PX.Objects.PO.POOrder, PX.Objects.SO.SOLineSplit> pxResult in PXSelectBase<POLine3, PXSelectReadonly2<POLine3, InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.orderNbr, Equal<POLine3.orderNbr>, And<PX.Objects.PO.POOrder.orderType, Equal<POLine3.orderType>>>, LeftJoin<PX.Objects.SO.SOLineSplit, On<PX.Objects.SO.SOLineSplit.pOType, Equal<POLine3.orderType>, And<PX.Objects.SO.SOLineSplit.pONbr, Equal<POLine3.orderNbr>, And<PX.Objects.SO.SOLineSplit.pOLineNbr, Equal<POLine3.lineNbr>>>>>>, Where2<Where<Current<PX.Objects.SO.SOLine.pOSource>, Equal<INReplenishmentSource.purchaseToOrder>, And<POLine3.orderType, In3<POOrderType.regularOrder, POOrderType.blanket>, Or<Current<PX.Objects.SO.SOLine.pOSource>, Equal<INReplenishmentSource.dropShipToOrder>, And<Where<POLine3.orderType, Equal<POOrderType.dropShip>, And2<Where<Current<PX.Objects.SO.SOLine.customerID>, Equal<PX.Objects.PO.POOrder.shipToBAccountID>, Or<PX.Objects.PO.POOrder.shipDestType, NotEqual<POShippingDestination.customer>>>, Or<POLine3.orderType, Equal<POOrderType.blanket>>>>>>>>, And<POLine3.lineType, In3<POLineType.goodsForInventory, POLineType.nonStock, POLineType.goodsForDropShip, POLineType.nonStockForDropShip, POLineType.goodsForSalesOrder, POLineType.goodsForServiceOrder, POLineType.nonStockForSalesOrder, POLineType.nonStockForServiceOrder, POLineType.goodsForReplenishment>, And<POLine3.inventoryID, Equal<Current<PX.Objects.SO.SOLine.inventoryID>>, And2<Where<Current<PX.Objects.SO.SOLine.subItemID>, IsNull, Or<POLine3.subItemID, Equal<Current<PX.Objects.SO.SOLine.subItemID>>>>, And<POLine3.siteID, Equal<Current<PX.Objects.SO.SOLine.pOSiteID>>, And2<Where<Current<PX.Objects.SO.SOLine.vendorID>, IsNull, Or<POLine3.vendorID, Equal<Current<PX.Objects.SO.SOLine.vendorID>>>>, And<Current2<PX.Objects.SO.SOLine.pOSource>, IsNotNull, And<PX.Objects.PO.POOrder.isLegacyDropShip, Equal<boolTrue>>>>>>>>>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[1]
    {
      (object) soLine
    }, Array.Empty<object>()))
    {
      POLine3 copy = PXCache<POLine3>.CreateCopy(PXResult<POLine3, PX.Objects.PO.POOrder, PX.Objects.SO.SOLineSplit>.op_Implicit(pxResult));
      PX.Objects.PO.POOrder poOrder = (PX.Objects.PO.POOrder) ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (PX.Objects.PO.POOrder)].CreateCopy(((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (PX.Objects.PO.POOrder)].Locate((object) PXResult<POLine3, PX.Objects.PO.POOrder, PX.Objects.SO.SOLineSplit>.op_Implicit(pxResult))) ?? PXResult<POLine3, PX.Objects.PO.POOrder, PX.Objects.SO.SOLineSplit>.op_Implicit(pxResult);
      PX.Objects.SO.SOLineSplit split = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(PXResult<POLine3, PX.Objects.PO.POOrder, PX.Objects.SO.SOLineSplit>.op_Implicit(pxResult));
      PX.Objects.SO.SOLineSplit foreignsplit = new PX.Objects.SO.SOLineSplit();
      PX.Objects.SO.SOLineSplit soLineSplit = (PX.Objects.SO.SOLineSplit) ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (PX.Objects.SO.SOLineSplit)].Locate((object) PXResult<POLine3, PX.Objects.PO.POOrder, PX.Objects.SO.SOLineSplit>.op_Implicit(pxResult));
      int? nullable;
      int? lineNbr1;
      if (soLineSplit != null)
      {
        if (soLineSplit.PONbr != null && ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (PX.Objects.SO.SOLineSplit)].GetStatus((object) soLineSplit) != 3 && !(soLineSplit.POType != copy.OrderType) && !(soLineSplit.PONbr != copy.OrderNbr))
        {
          nullable = soLineSplit.POLineNbr;
          lineNbr1 = copy.LineNbr;
          if (nullable.GetValueOrDefault() == lineNbr1.GetValueOrDefault() & nullable.HasValue == lineNbr1.HasValue)
          {
            split = (PX.Objects.SO.SOLineSplit) ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (PX.Objects.SO.SOLineSplit)].CreateCopy((object) soLineSplit);
            goto label_9;
          }
        }
        split = new PX.Objects.SO.SOLineSplit();
      }
label_9:
      if (split.PONbr == null)
      {
        split = new PX.Objects.SO.SOLineSplit();
      }
      else
      {
        if (!(split.OrderType != soLine.OrderType) && !(split.OrderNbr != soLine.OrderNbr))
        {
          lineNbr1 = split.LineNbr;
          nullable = soLine.LineNbr;
          if (lineNbr1.GetValueOrDefault() == nullable.GetValueOrDefault() & lineNbr1.HasValue == nullable.HasValue)
            goto label_14;
        }
        foreignsplit = (PX.Objects.SO.SOLineSplit) ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (PX.Objects.SO.SOLineSplit)].CreateCopy((object) split);
        split = new PX.Objects.SO.SOLineSplit();
      }
label_14:
      DropShipLegacyDialog.POSupplyResult poSupplyResult1 = new DropShipLegacyDialog.POSupplyResult();
      poSupplyResult1.POOrderType = copy.OrderType;
      poSupplyResult1.POOrderNbr = copy.OrderNbr;
      poSupplyResult1.POLineNbr = copy.LineNbr;
      poSupplyResult1.POLine = copy;
      poSupplyResult1.POOrder = poOrder;
      ref DropShipLegacyDialog.POSupplyResult local1 = ref poSupplyResult1;
      nullable = split.SplitLineNbr;
      List<PX.Objects.SO.SOLineSplit> soLineSplitList1;
      if (!nullable.HasValue)
      {
        soLineSplitList1 = new List<PX.Objects.SO.SOLineSplit>();
      }
      else
      {
        soLineSplitList1 = new List<PX.Objects.SO.SOLineSplit>();
        soLineSplitList1.Add(split);
      }
      local1.CurrentSOLineSplits = soLineSplitList1;
      ref DropShipLegacyDialog.POSupplyResult local2 = ref poSupplyResult1;
      nullable = foreignsplit.SplitLineNbr;
      List<PX.Objects.SO.SOLineSplit> soLineSplitList2;
      if (!nullable.HasValue)
      {
        soLineSplitList2 = new List<PX.Objects.SO.SOLineSplit>();
      }
      else
      {
        soLineSplitList2 = new List<PX.Objects.SO.SOLineSplit>();
        soLineSplitList2.Add(foreignsplit);
      }
      local2.ForeignSOLineSplits = soLineSplitList2;
      DropShipLegacyDialog.POSupplyResult result = poSupplyResult1;
      DropShipLegacyDialog.POSupplyResult poSupplyResult2 = source1.FirstOrDefault<DropShipLegacyDialog.POSupplyResult>((Func<DropShipLegacyDialog.POSupplyResult, bool>) (x =>
      {
        if (!(x.POOrderType == result.POOrderType) || !(x.POOrderNbr == result.POOrderNbr))
          return false;
        int? poLineNbr1 = x.POLineNbr;
        int? poLineNbr2 = result.POLineNbr;
        return poLineNbr1.GetValueOrDefault() == poLineNbr2.GetValueOrDefault() & poLineNbr1.HasValue == poLineNbr2.HasValue;
      }));
      if (poSupplyResult2.POOrderNbr != null)
      {
        if (!poSupplyResult2.CurrentSOLineSplits.Any<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (x =>
        {
          if (x.OrderType == split.OrderType && x.OrderNbr == split.OrderNbr)
          {
            int? lineNbr2 = x.LineNbr;
            int? lineNbr3 = split.LineNbr;
            if (lineNbr2.GetValueOrDefault() == lineNbr3.GetValueOrDefault() & lineNbr2.HasValue == lineNbr3.HasValue)
            {
              int? splitLineNbr1 = x.SplitLineNbr;
              int? splitLineNbr2 = split.SplitLineNbr;
              return splitLineNbr1.GetValueOrDefault() == splitLineNbr2.GetValueOrDefault() & splitLineNbr1.HasValue == splitLineNbr2.HasValue;
            }
          }
          return false;
        })))
          poSupplyResult2.CurrentSOLineSplits.Add(split);
        if (!poSupplyResult2.ForeignSOLineSplits.Any<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (x =>
        {
          if (x.OrderType == foreignsplit.OrderType && x.OrderNbr == foreignsplit.OrderNbr)
          {
            int? lineNbr4 = x.LineNbr;
            int? lineNbr5 = foreignsplit.LineNbr;
            if (lineNbr4.GetValueOrDefault() == lineNbr5.GetValueOrDefault() & lineNbr4.HasValue == lineNbr5.HasValue)
            {
              int? splitLineNbr3 = x.SplitLineNbr;
              int? splitLineNbr4 = foreignsplit.SplitLineNbr;
              return splitLineNbr3.GetValueOrDefault() == splitLineNbr4.GetValueOrDefault() & splitLineNbr3.HasValue == splitLineNbr4.HasValue;
            }
          }
          return false;
        })))
          poSupplyResult2.ForeignSOLineSplits.Add(foreignsplit);
      }
      else
        source1.Add(result);
    }
    bool flag1 = true;
    foreach (PXResult<PX.Objects.SO.SOLineSplit> pxResult in ((PXSelectBase<PX.Objects.SO.SOLineSplit>) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Select(Array.Empty<object>()))
    {
      if (!PXResult<PX.Objects.SO.SOLineSplit>.op_Implicit(pxResult).Completed.GetValueOrDefault())
      {
        flag1 = false;
        break;
      }
    }
    PXResultset<PX.Objects.SO.SOLineSplit> source2 = PXSelectBase<PX.Objects.SO.SOLineSplit, PXSelect<PX.Objects.SO.SOLineSplit, Where<PX.Objects.SO.SOLineSplit.orderType, Equal<Required<PX.Objects.SO.SOLineSplit.orderType>>, And<PX.Objects.SO.SOLineSplit.orderNbr, Equal<Required<PX.Objects.SO.SOLineSplit.orderNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[2]
    {
      (object) soLine.OrderType,
      (object) soLine.OrderNbr
    });
    Expression<Func<PXResult<PX.Objects.SO.SOLineSplit>, bool>> predicate = (Expression<Func<PXResult<PX.Objects.SO.SOLineSplit>, bool>>) (x => ((PX.Objects.SO.SOLineSplit) x).PONbr != default (string));
    foreach (PXResult<PX.Objects.SO.SOLineSplit> pxResult in (IEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>) ((IQueryable<PXResult<PX.Objects.SO.SOLineSplit>>) source2).Where<PXResult<PX.Objects.SO.SOLineSplit>>(predicate))
    {
      PX.Objects.SO.SOLineSplit splitFromCache = PXResult<PX.Objects.SO.SOLineSplit>.op_Implicit(pxResult);
      DropShipLegacyDialog.POSupplyResult poSupplyResult = source1.FirstOrDefault<DropShipLegacyDialog.POSupplyResult>((Func<DropShipLegacyDialog.POSupplyResult, bool>) (x =>
      {
        if (!(x.POOrderType == splitFromCache.POType) || !(x.POOrderNbr == splitFromCache.PONbr))
          return false;
        int? poLineNbr3 = x.POLineNbr;
        int? poLineNbr4 = splitFromCache.POLineNbr;
        return poLineNbr3.GetValueOrDefault() == poLineNbr4.GetValueOrDefault() & poLineNbr3.HasValue == poLineNbr4.HasValue;
      }));
      if (poSupplyResult.POOrderNbr != null)
      {
        int? lineNbr6 = splitFromCache.LineNbr;
        int? lineNbr7 = soLine.LineNbr;
        if (lineNbr6.GetValueOrDefault() == lineNbr7.GetValueOrDefault() & lineNbr6.HasValue == lineNbr7.HasValue)
        {
          if (!poSupplyResult.CurrentSOLineSplits.Any<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (x =>
          {
            int? splitLineNbr5 = x.SplitLineNbr;
            int? splitLineNbr6 = splitFromCache.SplitLineNbr;
            return splitLineNbr5.GetValueOrDefault() == splitLineNbr6.GetValueOrDefault() & splitLineNbr5.HasValue == splitLineNbr6.HasValue;
          })))
            poSupplyResult.CurrentSOLineSplits.Add((PX.Objects.SO.SOLineSplit) ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (PX.Objects.SO.SOLineSplit)].CreateCopy((object) splitFromCache));
        }
        else if (!poSupplyResult.ForeignSOLineSplits.Any<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (x =>
        {
          int? lineNbr8 = x.LineNbr;
          int? lineNbr9 = splitFromCache.LineNbr;
          if (!(lineNbr8.GetValueOrDefault() == lineNbr9.GetValueOrDefault() & lineNbr8.HasValue == lineNbr9.HasValue))
            return false;
          int? splitLineNbr7 = x.SplitLineNbr;
          int? splitLineNbr8 = splitFromCache.SplitLineNbr;
          return splitLineNbr7.GetValueOrDefault() == splitLineNbr8.GetValueOrDefault() & splitLineNbr7.HasValue == splitLineNbr8.HasValue;
        })))
          poSupplyResult.ForeignSOLineSplits.Add((PX.Objects.SO.SOLineSplit) ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (PX.Objects.SO.SOLineSplit)].CreateCopy((object) splitFromCache));
      }
    }
    foreach (DropShipLegacyDialog.POSupplyResult poSupplyResult in source1)
    {
      POLine3 copy = PXCache<POLine3>.CreateCopy(poSupplyResult.POLine);
      PX.Objects.PO.POOrder poOrder = poSupplyResult.POOrder;
      Decimal num1 = 0M;
      PX.Objects.SO.SOLineSplit soLineSplit = (PX.Objects.SO.SOLineSplit) null;
      bool? nullable1;
      Decimal? nullable2;
      foreach (PX.Objects.SO.SOLineSplit currentSoLineSplit in poSupplyResult.CurrentSOLineSplits)
      {
        if (currentSoLineSplit.PONbr != null)
        {
          if (currentSoLineSplit.PlanID.HasValue)
          {
            nullable1 = currentSoLineSplit.POCompleted;
            if (!nullable1.GetValueOrDefault())
            {
              Decimal num2 = num1;
              nullable2 = currentSoLineSplit.BaseQty;
              Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
              nullable2 = currentSoLineSplit.BaseReceivedQty;
              Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
              Decimal num3 = valueOrDefault1 - valueOrDefault2;
              num1 = num2 + num3;
            }
          }
          if (soLineSplit == null)
            soLineSplit = currentSoLineSplit;
        }
      }
      bool flag2 = soLineSplit != null;
      bool flag3 = false;
      foreach (PX.Objects.SO.SOLineSplit foreignSoLineSplit in poSupplyResult.ForeignSOLineSplits)
      {
        if (foreignSoLineSplit.PONbr != null)
        {
          if (foreignSoLineSplit.PlanID.HasValue && !foreignSoLineSplit.POCompleted.GetValueOrDefault())
            num1 += foreignSoLineSplit.BaseQty.GetValueOrDefault() - foreignSoLineSplit.BaseReceivedQty.GetValueOrDefault();
          flag3 = true;
        }
      }
      if ((!(soLine.POSource == "D") || !(copy.OrderType == "RO")) && (flag2 || (!poOrder.Hold.GetValueOrDefault() || ((PXGraphExtension<SOOrderEntry>) this).Base.SOPOLinkShowDocumentsOnHold) && !flag1 && !soLine.Completed.GetValueOrDefault()))
      {
        if (!flag2)
        {
          if (copy.OrderType != "DP")
          {
            nullable1 = copy.Completed;
            bool flag4 = false;
            if (nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue)
            {
              nullable1 = copy.Cancelled;
              bool flag5 = false;
              if (nullable1.GetValueOrDefault() == flag5 & nullable1.HasValue)
              {
                Decimal? baseOpenQty = copy.BaseOpenQty;
                Decimal num4 = num1;
                nullable2 = baseOpenQty.HasValue ? new Decimal?(baseOpenQty.GetValueOrDefault() - num4) : new Decimal?();
                Decimal num5 = 0M;
                if (nullable2.GetValueOrDefault() > num5 & nullable2.HasValue)
                  goto label_81;
              }
            }
          }
          if (copy.OrderType == "DP" && !flag3)
          {
            nullable1 = copy.Completed;
            bool flag6 = false;
            if (nullable1.GetValueOrDefault() == flag6 & nullable1.HasValue)
            {
              nullable1 = copy.Cancelled;
              bool flag7 = false;
              if (nullable1.GetValueOrDefault() == flag7 & nullable1.HasValue)
              {
                nullable2 = copy.BaseOrderQty;
                Decimal num6 = 0M;
                if (nullable2.GetValueOrDefault() >= num6 & nullable2.HasValue)
                  goto label_81;
              }
            }
            nullable2 = copy.BaseReceivedQty;
            Decimal num7 = 0M;
            if (!(nullable2.GetValueOrDefault() > num7 & nullable2.HasValue))
              continue;
          }
          else
            continue;
        }
label_81:
        POLine3 poLine3 = ((PXSelectBase<POLine3>) this.posupply).Locate(copy);
        if (poLine3 == null)
        {
          GraphHelper.Hold(((PXSelectBase) this.posupply).Cache, (object) copy);
          poLine3 = copy;
        }
        if (!(poLine3.SOOrderType != soLine.OrderType) && !(poLine3.SOOrderNbr != soLine.OrderNbr))
        {
          int? soOrderLineNbr = poLine3.SOOrderLineNbr;
          int? lineNbr = soLine.LineNbr;
          if (soOrderLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & soOrderLineNbr.HasValue == lineNbr.HasValue)
            goto label_86;
        }
        poLine3.Selected = new bool?(flag2);
label_86:
        poLine3.SOOrderType = soLine.OrderType;
        poLine3.SOOrderNbr = soLine.OrderNbr;
        poLine3.SOOrderLineNbr = soLine.LineNbr;
        poLine3.LinkedToCurrentSOLine = new bool?(flag2);
        poLine3.VendorRefNbr = poOrder.VendorRefNbr;
        poLine3.DemandQty = new Decimal?(num1);
        poLine3List.Add(poLine3);
      }
    }
    return (IEnumerable) poLine3List;
  }

  protected virtual void SOLine_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PX.Objects.SO.SOLine row = (PX.Objects.SO.SOLine) e.Row;
    if (row == null)
      return;
    PXSelect<SupplyPOLine> supplyPoLines = this.Base2.SupplyPOLines;
    bool? nullable = row.IsLegacyDropShip;
    int num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    ((PXSelectBase) supplyPoLines).AllowSelect = num1 != 0;
    PXSelect<POLine3> posupply = this.posupply;
    nullable = row.IsLegacyDropShip;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    ((PXSelectBase) posupply).AllowSelect = num2 != 0;
    if (row.POSource != "D")
      return;
    nullable = row.IsLegacyDropShip;
    if (!nullable.GetValueOrDefault())
      return;
    bool flag = false;
    if (((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.IsPOCreateEnabled(row))
    {
      nullable = row.POCreate;
      if (nullable.GetValueOrDefault())
        flag = PXResultset<PX.Objects.AR.ARTran>.op_Implicit(PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.sOOrderType, Equal<Required<PX.Objects.AR.ARTran.sOOrderType>>, And<PX.Objects.AR.ARTran.sOOrderNbr, Equal<Required<PX.Objects.AR.ARTran.sOOrderNbr>>, And<PX.Objects.AR.ARTran.sOOrderLineNbr, Equal<Required<PX.Objects.AR.ARTran.sOOrderLineNbr>>>>>>.Config>.SelectWindowed((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, 0, 1, new object[3]
        {
          (object) row.OrderType,
          (object) row.OrderNbr,
          (object) row.LineNbr
        })) == null;
    }
    PXUIFieldAttribute.SetEnabled<POLine3.selected>(((PXSelectBase) this.posupply).Cache, (object) null, flag);
  }

  protected virtual void POLine3_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is POLine3 row1))
      return;
    PX.Objects.SO.SOLine soLine = PXResultset<PX.Objects.SO.SOLine>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLine, PXSelect<PX.Objects.SO.SOLine, Where<PX.Objects.SO.SOLine.orderType, Equal<Current<POLine3.sOOrderType>>, And<PX.Objects.SO.SOLine.orderNbr, Equal<Current<POLine3.sOOrderNbr>>, And<PX.Objects.SO.SOLine.lineNbr, Equal<Current<POLine3.sOOrderLineNbr>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, Array.Empty<object>()));
    Decimal? nullable1;
    Decimal? nullable2;
    Decimal? nullable3;
    Decimal? nullable4;
    int num1;
    if (row1 != null && soLine != null && row1.OrderType != "DP")
    {
      nullable1 = row1.BaseOpenQty;
      nullable2 = row1.DemandQty;
      nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal num2 = 0M;
      if (!(nullable3.GetValueOrDefault() > num2 & nullable3.HasValue) || !(soLine.ShipComplete != "C"))
      {
        nullable1 = row1.BaseOpenQty;
        nullable4 = row1.DemandQty;
        nullable3 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
        nullable4 = soLine.BaseOrderQty;
        nullable1 = soLine.CompleteQtyMin;
        nullable2 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable1.GetValueOrDefault() / 100M) : new Decimal?();
        num1 = nullable3.GetValueOrDefault() >= nullable2.GetValueOrDefault() & nullable3.HasValue & nullable2.HasValue ? 1 : 0;
      }
      else
        num1 = 1;
    }
    else
      num1 = 0;
    bool flag1 = num1 != 0;
    bool? nullable5;
    int num3;
    if (row1 != null && soLine != null && row1.OrderType == "DP")
    {
      nullable5 = row1.Completed;
      bool flag2 = false;
      if (nullable5.GetValueOrDefault() == flag2 & nullable5.HasValue)
      {
        nullable2 = row1.BaseOrderQty;
        nullable1 = soLine.BaseOpenQty;
        nullable4 = soLine.CompleteQtyMin;
        nullable3 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable4.GetValueOrDefault() / 100M) : new Decimal?();
        if (nullable2.GetValueOrDefault() >= nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue || soLine.ShipComplete != "C")
        {
          nullable3 = row1.BaseOrderQty;
          nullable4 = soLine.BaseOpenQty;
          nullable1 = soLine.CompleteQtyMax;
          nullable2 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable1.GetValueOrDefault() / 100M) : new Decimal?();
          if (nullable3.GetValueOrDefault() <= nullable2.GetValueOrDefault() & nullable3.HasValue & nullable2.HasValue)
          {
            num3 = 1;
            goto label_16;
          }
        }
      }
      nullable2 = row1.BaseReceivedQty;
      nullable1 = soLine.BaseOpenQty;
      nullable4 = soLine.CompleteQtyMin;
      nullable3 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable4.GetValueOrDefault() / 100M) : new Decimal?();
      if (nullable2.GetValueOrDefault() >= nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue || soLine.ShipComplete == "L")
      {
        nullable3 = row1.BaseReceivedQty;
        nullable4 = soLine.BaseOpenQty;
        nullable1 = soLine.CompleteQtyMax;
        nullable2 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable1.GetValueOrDefault() / 100M) : new Decimal?();
        num3 = nullable3.GetValueOrDefault() <= nullable2.GetValueOrDefault() & nullable3.HasValue & nullable2.HasValue ? 1 : 0;
      }
      else
        num3 = 0;
    }
    else
      num3 = 0;
label_16:
    bool flag3 = num3 != 0;
    int num4;
    if (row1 != null && soLine != null)
    {
      nullable5 = row1.Selected;
      if (nullable5.GetValueOrDefault())
      {
        nullable3 = row1.BaseOrderQty;
        nullable1 = row1.BaseOpenQty;
        Decimal? nullable6;
        if (!(nullable3.HasValue & nullable1.HasValue))
        {
          nullable4 = new Decimal?();
          nullable6 = nullable4;
        }
        else
          nullable6 = new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault());
        nullable2 = nullable6;
        Decimal num5 = 0M;
        if (nullable2.GetValueOrDefault() > num5 & nullable2.HasValue)
        {
          nullable5 = row1.LinkedToCurrentSOLine;
          num4 = nullable5.GetValueOrDefault() ? 1 : 0;
          goto label_24;
        }
      }
    }
    num4 = 0;
label_24:
    bool flag4 = num4 != 0;
    PXCache pxCache = sender;
    object row2 = e.Row;
    int num6;
    if (!(flag1 | flag3))
    {
      nullable5 = row1.LinkedToCurrentSOLine;
      if (!nullable5.GetValueOrDefault())
      {
        num6 = 0;
        goto label_28;
      }
    }
    num6 = !flag4 ? 1 : 0;
label_28:
    PXUIFieldAttribute.SetEnabled<POLine3.selected>(pxCache, row2, num6 != 0);
    if (!flag4)
      return;
    PXUIFieldAttribute.SetWarning<POLine3.selected>(sender, e.Row, "The purchase order cannot be deselected because it has been fully or partially received.");
  }

  [PXOverride]
  public virtual void POSupplyDialogInitializer(PXGraph graph, string viewName)
  {
    foreach (POLine3 poLine3 in ((PXSelectBase) this.posupply).Cache.Updated)
    {
      poLine3.Selected = new bool?(false);
      poLine3.SOOrderType = (string) null;
      poLine3.SOOrderNbr = (string) null;
      poLine3.SOOrderLineNbr = new int?();
    }
  }

  [PXOverride]
  public virtual void LinkPOSupply(PX.Objects.SO.SOLine currentSOLine)
  {
    if (currentSOLine.POSource != "D" || !currentSOLine.IsLegacyDropShip.GetValueOrDefault())
      return;
    this.LinkSupplyDemand();
  }

  public virtual void LinkSupplyDemand()
  {
    List<PX.Objects.SO.SOLineSplit> list = GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) ((PXSelectBase<PX.Objects.SO.SOLineSplit>) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Select(Array.Empty<object>())).ToList<PX.Objects.SO.SOLineSplit>();
    bool flag1 = false;
    foreach (POLine3 poLine3 in ((PXSelectBase) this.posupply).Cache.Updated)
    {
      PX.Objects.SO.SOLine copy = PXCache<PX.Objects.SO.SOLine>.CreateCopy(PXResultset<PX.Objects.SO.SOLine>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOLine>) this.currentposupply).Select(new object[3]
      {
        (object) poLine3.SOOrderType,
        (object) poLine3.SOOrderNbr,
        (object) poLine3.SOOrderLineNbr
      })));
      bool? selected = poLine3.Selected;
      bool flag2 = false;
      if (selected.GetValueOrDefault() == flag2 & selected.HasValue && poLine3.LinkedToCurrentSOLine.GetValueOrDefault())
      {
        foreach (PX.Objects.SO.SOLineSplit split in list)
        {
          if (((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache.GetStatus((object) split) != 3 && ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache.GetStatus((object) split) != 4)
          {
            if (split.POType == poLine3.OrderType && split.PONbr == poLine3.OrderNbr)
            {
              int? poLineNbr = split.POLineNbr;
              int? lineNbr = poLine3.LineNbr;
              if (poLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & poLineNbr.HasValue == lineNbr.HasValue)
              {
                bool? nullable = split.POCompleted;
                bool flag3 = false;
                if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
                {
                  nullable = split.Completed;
                  bool flag4 = false;
                  if (nullable.GetValueOrDefault() == flag4 & nullable.HasValue)
                    goto label_11;
                }
              }
            }
            if (!(poLine3.OrderType == "DP"))
              continue;
label_11:
            if (split.POType != null && split.PONbr != null && split.POType == poLine3.OrderType && split.PONbr == poLine3.OrderNbr)
            {
              PX.Objects.PO.POOrder poOrder = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(PXSelectBase<PX.Objects.PO.POOrder, PXSelect<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[2]
              {
                (object) poLine3.OrderType,
                (object) poLine3.OrderNbr
              }));
              if (poOrder != null)
              {
                Guid? refNoteId = split.RefNoteID;
                Guid? nullable1 = poOrder.NoteID;
                if ((refNoteId.HasValue == nullable1.HasValue ? (refNoteId.HasValue ? (refNoteId.GetValueOrDefault() == nullable1.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
                {
                  PX.Objects.SO.SOLineSplit soLineSplit = split;
                  nullable1 = new Guid?();
                  Guid? nullable2 = nullable1;
                  soLineSplit.RefNoteID = nullable2;
                }
                if (poOrder.SOOrderType == split.OrderType && poOrder.SOOrderNbr == split.OrderNbr)
                {
                  poOrder.SOOrderType = (string) null;
                  poOrder.SOOrderNbr = (string) null;
                  ((PXSelectBase<PX.Objects.PO.POOrder>) this.poorderlink).Update(poOrder);
                }
              }
            }
            if (split.PONbr != null)
            {
              split.ClearPOReferences();
              split.POCompleted = new bool?(false);
              flag1 = true;
            }
            split.ReceivedQty = new Decimal?(0M);
            split.ShippedQty = new Decimal?(0M);
            split.Completed = new bool?(false);
            ((PXSelectBase<PX.Objects.SO.SOLineSplit>) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Update(split);
            if (poLine3.OrderType == "DP")
            {
              copy.ShippedQty = new Decimal?(0M);
              copy.UnbilledQty = copy.OrderQty;
              copy.OpenQty = copy.OrderQty;
              copy.ClosedQty = new Decimal?(0M);
              copy.Completed = new bool?(false);
              ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Update(copy);
            }
          }
        }
        poLine3.LinkedToCurrentSOLine = new bool?(false);
      }
    }
    bool addedLink = false;
    bool poLineCompleted = false;
    foreach (POLine3 supply in ((PXSelectBase) this.posupply).Cache.Updated)
    {
      PX.Objects.SO.SOLine copy = PXCache<PX.Objects.SO.SOLine>.CreateCopy(PXResultset<PX.Objects.SO.SOLine>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOLine>) this.currentposupply).Select(new object[3]
      {
        (object) supply.SOOrderType,
        (object) supply.SOOrderNbr,
        (object) supply.SOOrderLineNbr
      })));
      if (supply.Selected.GetValueOrDefault() && !supply.LinkedToCurrentSOLine.GetValueOrDefault())
        this.LinkToSuplyLine(copy, list, supply, ref addedLink, ref poLineCompleted);
    }
    PX.Objects.SO.SOLine current = ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current;
    if (addedLink)
    {
      if (!current.POCreated.GetValueOrDefault())
        ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache.SetValue<PX.Objects.SO.SOLine.pOCreated>((object) current, (object) true);
      if (!poLineCompleted)
        return;
      ((PXGraphExtension<SOOrderEntry>) this).Base.LineSplittingAllocatedExt.CompleteSchedules(current);
    }
    else
    {
      if (!flag1 || !current.POCreated.GetValueOrDefault())
        return;
      bool flag5 = list.Any<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (x => x.POCreate.GetValueOrDefault() && x.PONbr != null));
      if (flag5)
        return;
      ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache.SetValue<PX.Objects.SO.SOLine.pOCreated>((object) current, (object) flag5);
    }
  }

  public virtual void LinkToSuplyLine(
    PX.Objects.SO.SOLine line,
    List<PX.Objects.SO.SOLineSplit> splits,
    POLine3 supply,
    ref bool addedLink,
    ref bool poLineCompleted)
  {
    Decimal? baseOrderQty = supply.BaseOrderQty;
    Decimal? nullable1 = supply.DemandQty;
    Decimal? nullable2 = baseOrderQty.HasValue & nullable1.HasValue ? new Decimal?(baseOrderQty.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    for (int index = 0; index < splits.Count; ++index)
    {
      PX.Objects.SO.SOLineSplit copy1 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(splits[index]);
      Decimal? nullable3;
      if (string.IsNullOrEmpty(copy1.SOOrderNbr) && string.IsNullOrEmpty(copy1.PONbr))
      {
        bool? nullable4 = copy1.IsAllocated;
        bool flag1 = false;
        if (nullable4.GetValueOrDefault() == flag1 & nullable4.HasValue)
        {
          nullable4 = copy1.Completed;
          bool flag2 = false;
          if (nullable4.GetValueOrDefault() == flag2 & nullable4.HasValue)
          {
            nullable1 = copy1.BaseQty;
            Decimal num1 = 0M;
            if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
            {
              if (supply.OrderType != "BL")
                supply.LineType = line.POSource == "D" ? (line.LineType == "GI" ? "GP" : "NP") : (line.LineType == "GI" ? "GS" : "NO");
              supply.LinkedToCurrentSOLine = new bool?(true);
              nullable4 = supply.Completed;
              bool flag3 = false;
              if (nullable4.GetValueOrDefault() == flag3 & nullable4.HasValue)
              {
                INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Required<INItemPlan.planID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[1]
                {
                  (object) supply.PlanID
                }));
                if (inItemPlan != null)
                {
                  if (supply.OrderType != "BL")
                  {
                    INItemPlan copy2 = PXCache<INItemPlan>.CreateCopy(inItemPlan);
                    copy2.PlanType = line.POSource == "D" ? "74" : "76";
                    ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (INItemPlan)].Update((object) copy2);
                  }
                  if (supply.OrderType == "DP")
                  {
                    PX.Objects.PO.POReceiptLine poReceiptLine = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceiptLine, PXSelectJoinGroupBy<PX.Objects.PO.POReceiptLine, InnerJoin<PX.Objects.PO.POReceipt, On<PX.Objects.PO.POReceiptLine.FK.Receipt>>, Where<PX.Objects.PO.POReceiptLine.pOType, Equal<Current<POLine3.orderType>>, And<PX.Objects.PO.POReceiptLine.pONbr, Equal<Current<POLine3.orderNbr>>, And<PX.Objects.PO.POReceiptLine.pOLineNbr, Equal<Current<POLine3.lineNbr>>, And<PX.Objects.PO.POReceipt.released, Equal<True>>>>>, Aggregate<Sum<PX.Objects.PO.POReceiptLine.baseReceiptQty>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[1]
                    {
                      (object) supply
                    }, Array.Empty<object>()));
                    PX.Objects.SO.SOLineSplit soLineSplit1 = copy1;
                    nullable1 = poReceiptLine.BaseReceiptQty;
                    Decimal? nullable5 = new Decimal?(nullable1.GetValueOrDefault());
                    soLineSplit1.BaseShippedQty = nullable5;
                    PXDBQuantityAttribute.CalcTranQty<PX.Objects.SO.SOLineSplit.shippedQty>(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache, (object) copy1);
                    PX.Objects.SO.SOLineSplit soLineSplit2 = copy1;
                    nullable1 = copy1.Qty;
                    nullable3 = copy1.ShippedQty;
                    Decimal? nullable6 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
                    soLineSplit2.OpenQty = nullable6;
                    PX.Objects.SO.SOLine soLine1 = line;
                    nullable3 = soLine1.BaseShippedQty;
                    nullable1 = copy1.BaseShippedQty;
                    soLine1.BaseShippedQty = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
                    PXDBQuantityAttribute.CalcTranQty<PX.Objects.SO.SOLine.shippedQty>(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache, (object) line);
                    PX.Objects.SO.SOLine soLine2 = line;
                    nullable1 = line.OrderQty;
                    nullable3 = line.ShippedQty;
                    Decimal? nullable7 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
                    soLine2.OpenQty = nullable7;
                    line.ClosedQty = line.ShippedQty;
                    ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Update(line);
                  }
                }
                else
                  continue;
              }
              INItemPlan inItemPlan1 = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Required<INItemPlan.planID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[1]
              {
                (object) copy1.PlanID
              }));
              if (supply.OrderType == "DP")
              {
                foreach (PXResult<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceipt> pxResult in PXSelectBase<PX.Objects.PO.POReceiptLine, PXSelectJoin<PX.Objects.PO.POReceiptLine, InnerJoin<PX.Objects.PO.POReceipt, On2<PX.Objects.PO.POReceiptLine.FK.Receipt, And<PX.Objects.PO.POReceipt.released, Equal<True>>>>, Where<PX.Objects.PO.POReceiptLine.pOType, Equal<Required<PX.Objects.PO.POReceiptLine.pOType>>, And<PX.Objects.PO.POReceiptLine.pONbr, Equal<Required<PX.Objects.PO.POReceiptLine.pONbr>>, And<PX.Objects.PO.POReceiptLine.pOLineNbr, Equal<Required<PX.Objects.PO.POReceiptLine.pOLineNbr>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[3]
                {
                  (object) supply.OrderType,
                  (object) supply.OrderNbr,
                  (object) supply.LineNbr
                }))
                {
                  PX.Objects.PO.POReceiptLine porl = PXResult<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceipt>.op_Implicit(pxResult);
                  PX.Objects.SO.SOOrderShipment soOrderShipment1 = PXResult<PX.Objects.SO.SOOrderShipment>.op_Implicit(((IQueryable<PXResult<PX.Objects.SO.SOOrderShipment>>) ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) ((PXGraphExtension<SOOrderEntry>) this).Base.shipmentlist).Select(Array.Empty<object>())).Where<PXResult<PX.Objects.SO.SOOrderShipment>>((Expression<Func<PXResult<PX.Objects.SO.SOOrderShipment>, bool>>) (s => ((PX.Objects.SO.SOOrderShipment) s).ShipmentNbr == porl.ReceiptNbr)).FirstOrDefault<PXResult<PX.Objects.SO.SOOrderShipment>>());
                  if (soOrderShipment1 == null)
                  {
                    PX.Objects.SO.SOOrderShipment soOrderShipment2 = new PX.Objects.SO.SOOrderShipment()
                    {
                      OrderType = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current.OrderType,
                      OrderNbr = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current.OrderNbr,
                      ShipAddressID = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current.ShipAddressID,
                      ShipContactID = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current.ShipContactID,
                      ShipmentType = "H",
                      ShipmentNbr = porl.ReceiptNbr,
                      ShippingRefNoteID = PXResult<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceipt>.op_Implicit(pxResult).NoteID,
                      Operation = "I",
                      ShipDate = porl.ReceiptDate,
                      CustomerID = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current.CustomerID,
                      CustomerLocationID = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current.CustomerLocationID,
                      SiteID = new int?(),
                      ShipmentWeight = porl.ExtWeight,
                      ShipmentVolume = porl.ExtVolume,
                      ShipmentQty = porl.ReceiptQty,
                      LineTotal = new Decimal?(0M),
                      Confirmed = new bool?(true),
                      CreateINDoc = new bool?(true)
                    };
                    soOrderShipment2.OrderType = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current.OrderType;
                    soOrderShipment2.OrderNbr = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current.OrderNbr;
                    soOrderShipment2.OrderNoteID = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current.NoteID;
                    ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) ((PXGraphExtension<SOOrderEntry>) this).Base.shipmentlist).Insert(soOrderShipment2);
                    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current;
                    int? shipmentCntr = current.ShipmentCntr;
                    current.ShipmentCntr = shipmentCntr.HasValue ? new int?(shipmentCntr.GetValueOrDefault() + 1) : new int?();
                  }
                  else
                  {
                    PX.Objects.SO.SOOrderShipment soOrderShipment3 = soOrderShipment1;
                    nullable3 = soOrderShipment3.ShipmentWeight;
                    nullable1 = porl.ExtWeight;
                    soOrderShipment3.ShipmentWeight = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
                    PX.Objects.SO.SOOrderShipment soOrderShipment4 = soOrderShipment1;
                    nullable1 = soOrderShipment4.ShipmentVolume;
                    nullable3 = porl.ExtVolume;
                    soOrderShipment4.ShipmentVolume = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                    PX.Objects.SO.SOOrderShipment soOrderShipment5 = soOrderShipment1;
                    nullable3 = soOrderShipment5.ShipmentQty;
                    nullable1 = porl.ReceiptQty;
                    soOrderShipment5.ShipmentQty = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
                    ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) ((PXGraphExtension<SOOrderEntry>) this).Base.shipmentlist).Update(soOrderShipment1);
                  }
                }
                if (supply.Completed.GetValueOrDefault())
                {
                  ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (INItemPlan)].Delete((object) inItemPlan1);
                  inItemPlan1 = (INItemPlan) null;
                  copy1.BaseShippedQty = supply.BaseReceivedQty;
                  PXDBQuantityAttribute.CalcTranQty<PX.Objects.SO.SOLineSplit.shippedQty>(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache, (object) copy1);
                  copy1.Completed = new bool?(true);
                  copy1.PlanID = new long?();
                  PX.Objects.SO.SOLine soLine3 = line;
                  Decimal? baseShippedQty = soLine3.BaseShippedQty;
                  Decimal? nullable8 = copy1.BaseShippedQty;
                  soLine3.BaseShippedQty = baseShippedQty.HasValue & nullable8.HasValue ? new Decimal?(baseShippedQty.GetValueOrDefault() + nullable8.GetValueOrDefault()) : new Decimal?();
                  PXDBQuantityAttribute.CalcTranQty<PX.Objects.SO.SOLine.shippedQty>(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache, (object) line);
                  PX.Objects.SO.SOLine soLine4 = line;
                  nullable8 = soLine4.UnbilledQty;
                  Decimal? orderQty = line.OrderQty;
                  Decimal? nullable9 = line.ShippedQty;
                  Decimal? nullable10 = orderQty.HasValue & nullable9.HasValue ? new Decimal?(orderQty.GetValueOrDefault() - nullable9.GetValueOrDefault()) : new Decimal?();
                  Decimal? nullable11;
                  if (!(nullable8.HasValue & nullable10.HasValue))
                  {
                    nullable9 = new Decimal?();
                    nullable11 = nullable9;
                  }
                  else
                    nullable11 = new Decimal?(nullable8.GetValueOrDefault() - nullable10.GetValueOrDefault());
                  soLine4.UnbilledQty = nullable11;
                  line.OpenQty = new Decimal?(0M);
                  line.ClosedQty = line.OrderQty;
                  line.Completed = new bool?(true);
                  poLineCompleted = true;
                  using (((PXGraphExtension<SOOrderEntry>) this).Base.LineSplittingExt.SuppressedModeScope(true))
                    ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Update(line);
                }
                if (inItemPlan1 != null && !inItemPlan1.SupplyPlanID.HasValue)
                {
                  INItemPlan copy3 = PXCache<INItemPlan>.CreateCopy(inItemPlan1);
                  copy3.SupplyPlanID = supply.PlanID;
                  ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (INItemPlan)].Update((object) copy3);
                }
              }
              else
              {
                INItemPlan copy4 = PXCache<INItemPlan>.CreateCopy(inItemPlan1);
                copy4.PlanType = supply.OrderType == "BL" ? (line.POSource == "O" ? "6B" : "6E") : (line.POSource == "D" ? "6D" : "66");
                copy4.FixedSource = "P";
                copy4.SupplyPlanID = supply.PlanID;
                PX.Objects.PO.POOrder poOrder = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(PXSelectBase<PX.Objects.PO.POOrder, PXSelect<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[2]
                {
                  (object) supply.OrderType,
                  (object) supply.OrderNbr
                }));
                if (poOrder != null)
                {
                  copy4.VendorID = poOrder.VendorID;
                  copy4.VendorLocationID = poOrder.VendorLocationID;
                }
                ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (INItemPlan)].Update((object) copy4);
              }
              copy1.POCreate = new bool?(true);
              copy1.VendorID = supply.VendorID;
              copy1.POType = supply.OrderType;
              copy1.PONbr = supply.OrderNbr;
              copy1.POLineNbr = supply.LineNbr;
              addedLink = true;
              nullable1 = copy1.BaseQty;
              nullable3 = nullable2;
              PX.Objects.SO.SOLineSplit soLineSplit3;
              if (nullable1.GetValueOrDefault() <= nullable3.GetValueOrDefault() & nullable1.HasValue & nullable3.HasValue)
              {
                nullable3 = nullable2;
                nullable1 = copy1.BaseQty;
                nullable2 = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
                soLineSplit3 = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Update(copy1);
              }
              else
              {
                PX.Objects.SO.SOLineSplit copy5 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(copy1);
                copy5.SplitLineNbr = new int?();
                copy5.IsAllocated = new bool?(false);
                copy5.ClearPOFlags();
                copy5.ClearPOReferences();
                copy5.VendorID = new int?();
                copy5.POCreate = new bool?(true);
                PX.Objects.SO.SOLineSplit soLineSplit4 = copy5;
                nullable1 = copy5.BaseQty;
                nullable3 = nullable2;
                Decimal? nullable12 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
                soLineSplit4.BaseQty = nullable12;
                PX.Objects.SO.SOLineSplit soLineSplit5 = copy5;
                PXCache cache1 = ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache;
                int? inventoryId1 = copy5.InventoryID;
                string uom1 = copy5.UOM;
                nullable3 = copy5.BaseQty;
                Decimal num2 = nullable3.Value;
                Decimal? nullable13 = new Decimal?(INUnitAttribute.ConvertFromBase(cache1, inventoryId1, uom1, num2, INPrecision.QUANTITY));
                soLineSplit5.Qty = nullable13;
                copy5.ShippedQty = new Decimal?(0M);
                copy5.ReceivedQty = new Decimal?(0M);
                copy5.UnreceivedQty = copy5.BaseQty;
                copy5.PlanID = new long?();
                copy5.Completed = new bool?(false);
                copy1.BaseQty = nullable2;
                PX.Objects.SO.SOLineSplit soLineSplit6 = copy1;
                PXCache cache2 = ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache;
                int? inventoryId2 = copy1.InventoryID;
                string uom2 = copy1.UOM;
                nullable3 = copy1.BaseQty;
                Decimal num3 = nullable3.Value;
                Decimal? nullable14 = new Decimal?(INUnitAttribute.ConvertFromBase(cache2, inventoryId2, uom2, num3, INPrecision.QUANTITY));
                soLineSplit6.Qty = nullable14;
                nullable2 = new Decimal?(0M);
                soLineSplit3 = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Update(copy1);
                PX.Objects.SO.SOLineSplit soLineSplit7;
                if ((soLineSplit7 = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Insert(copy5)) != null)
                  splits.Insert(index + 1, soLineSplit7);
              }
              splits[index] = soLineSplit3;
            }
          }
        }
      }
      nullable3 = nullable2;
      Decimal num = 0M;
      if (nullable3.GetValueOrDefault() <= num & nullable3.HasValue)
        break;
    }
  }

  private struct POSupplyResult
  {
    public string POOrderType;
    public string POOrderNbr;
    public int? POLineNbr;
    public POLine3 POLine;
    public PX.Objects.PO.POOrder POOrder;
    public List<PX.Objects.SO.SOLineSplit> CurrentSOLineSplits;
    public List<PX.Objects.SO.SOLineSplit> ForeignSOLineSplits;
  }
}

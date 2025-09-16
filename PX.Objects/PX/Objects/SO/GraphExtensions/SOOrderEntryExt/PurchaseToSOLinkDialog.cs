// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.PurchaseToSOLinkDialog
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class PurchaseToSOLinkDialog : 
  PXGraphExtension<POLinkDialog, PurchaseSupplyBaseExt, SOOrderEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.sOToPOLink>() || PXAccess.FeatureInstalled<FeaturesSet.dropShipments>() || PXAccess.FeatureInstalled<FeaturesSet.purchaseRequisitions>();
  }

  public virtual void _(PX.Data.Events.FieldUpdated<SupplyPOLine.selected> e)
  {
    SupplyPOLine row = (SupplyPOLine) e.Row;
    PX.Objects.SO.SOLine soLine = PXResultset<PX.Objects.SO.SOLine>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOLine>) this.Base2.SOLineDemand).Select(Array.Empty<object>())) ?? ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current;
    if (row == null)
      return;
    bool? oldValue = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<SupplyPOLine.selected>, object, object>) e).OldValue;
    bool? selected = row.Selected;
    if (oldValue.GetValueOrDefault() == selected.GetValueOrDefault() & oldValue.HasValue == selected.HasValue || !((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.IsPoToSoOrBlanket(soLine?.POSource))
      return;
    if (row.Selected.GetValueOrDefault())
      row.SelectedSOLines = row.SelectedSOLines.SparseArrayAddDistinct<int>(new int?(soLine.LineNbr.Value));
    else
      row.SelectedSOLines.SparseArrayRemove<int>(new int?(soLine.LineNbr.Value));
  }

  public virtual void _(PX.Data.Events.RowSelected<SupplyPOLine> e)
  {
    SupplyPOLine row = e.Row;
    PX.Objects.SO.SOLine soLine = PXResultset<PX.Objects.SO.SOLine>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOLine>) this.Base2.SOLineDemand).Select(Array.Empty<object>()));
    if (row == null || !((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.IsPoToSoOrBlanket(soLine?.POSource))
      return;
    bool allowUpdate = ((PXSelectBase) this.Base2.SOLineDemand).Cache.AllowUpdate;
    Decimal? nullable1 = row.BaseOpenQty;
    Decimal? nullable2 = row.BaseDemandQty;
    Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    Decimal num1 = 0M;
    Decimal? nullable4;
    int num2;
    if (!(nullable3.GetValueOrDefault() > num1 & nullable3.HasValue) || !(soLine.ShipComplete != "C"))
    {
      nullable1 = row.BaseOpenQty;
      nullable4 = row.BaseDemandQty;
      Decimal? nullable5 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
      nullable4 = soLine.BaseOrderQty;
      nullable1 = soLine.CompleteQtyMin;
      nullable2 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable1.GetValueOrDefault() / 100M) : new Decimal?();
      num2 = nullable5.GetValueOrDefault() >= nullable2.GetValueOrDefault() & nullable5.HasValue & nullable2.HasValue ? 1 : 0;
    }
    else
      num2 = 1;
    nullable1 = row.BaseOpenQty;
    nullable4 = row.BaseDemandQty;
    nullable2 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable6 = soLine.BaseOrderQty;
    bool flag1 = nullable2.GetValueOrDefault() >= nullable6.GetValueOrDefault() & nullable2.HasValue & nullable6.HasValue;
    bool flag2 = ((num2 == 0 ? 0 : (soLine.POSource != "L" ? 1 : 0)) | (flag1 ? 1 : 0)) != 0;
    int?[] linkedSoLines = row.LinkedSOLines;
    bool flag3 = linkedSoLines != null && ((IEnumerable<int?>) linkedSoLines).Contains<int?>(new int?(soLine.LineNbr.Value));
    nullable6 = row.BaseReceivedQty;
    Decimal num3 = 0M;
    bool flag4 = ((!(nullable6.GetValueOrDefault() > num3 & nullable6.HasValue) ? 0 : (row.OrderType != "BL" ? 1 : 0)) & (flag3 ? 1 : 0)) != 0;
    PXUIFieldAttribute.SetEnabled<SupplyPOLine.selected>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SupplyPOLine>>) e).Cache, (object) e.Row, ((!(flag2 | flag3) ? 0 : (!flag4 ? 1 : 0)) & (allowUpdate ? 1 : 0)) != 0);
    if (!(flag4 & allowUpdate))
      return;
    PXUIFieldAttribute.SetWarning<SupplyPOLine.selected>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SupplyPOLine>>) e).Cache, (object) e.Row, "The purchase order cannot be deselected because it has been fully or partially received.");
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOOrderEntryExt.POLinkDialog.CollectSupplyPOLines(PX.Objects.SO.SOLine,System.Collections.Generic.ICollection{PX.Objects.SO.SupplyPOLine})" />
  /// </summary>
  [PXOverride]
  public virtual void CollectSupplyPOLines(
    PX.Objects.SO.SOLine currentSOLine,
    ICollection<SupplyPOLine> supplyPOLines)
  {
    if (!((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.IsPoToSoOrBlanket(currentSOLine.POSource))
      return;
    PXSelectJoin<SupplyPOLine, LeftJoin<PX.Objects.SO.SOLineSplit, On<PX.Objects.SO.SOLineSplit.FK.SupplyLine>>, Where<SupplyPOLine.lineType, In3<POLineType.goodsForInventory, POLineType.nonStock, POLineType.goodsForSalesOrder, POLineType.goodsForServiceOrder, POLineType.nonStockForSalesOrder, POLineType.nonStockForServiceOrder, POLineType.goodsForReplenishment>, And2<Where<SupplyPOLine.orderType, Equal<POOrderType.blanket>, And<Current<PX.Objects.SO.SOLine.pOSource>, In3<INReplenishmentSource.blanketDropShipToOrder, INReplenishmentSource.blanketPurchaseToOrder>, Or<SupplyPOLine.orderType, Equal<POOrderType.regularOrder>, And<Current<PX.Objects.SO.SOLine.pOSource>, Equal<INReplenishmentSource.purchaseToOrder>>>>>, And2<Where<PX.Objects.SO.SOLineSplit.pOLineNbr, IsNotNull, Or<SupplyPOLine.completed, Equal<False>, And<SupplyPOLine.cancelled, Equal<False>>>>, And<SupplyPOLine.inventoryID, Equal<Current<PX.Objects.SO.SOLine.inventoryID>>, And2<Where<SupplyPOLine.subItemID, Equal<Current<PX.Objects.SO.SOLine.subItemID>>, Or<Current<PX.Objects.SO.SOLine.subItemID>, IsNull>>, And<SupplyPOLine.siteID, Equal<Current<PX.Objects.SO.SOLine.pOSiteID>>, And2<Where<SupplyPOLine.vendorID, Equal<Current<PX.Objects.SO.SOLine.vendorID>>, Or<Current<PX.Objects.SO.SOLine.vendorID>, IsNull>>, And<SupplyPOLine.vendorID, NotEqual<Current<PX.Objects.SO.SOOrder.customerID>>, And2<Where<Required<PX.Objects.GL.Branch.bAccountID>, IsNull, Or<SupplyPOLine.vendorID, NotEqual<Required<PX.Objects.GL.Branch.bAccountID>>>>, And<Where2<Where<Current<PX.Objects.SO.SOLine.isSpecialOrder>, NotEqual<True>, And<SupplyPOLine.isSpecialOrder, NotEqual<True>>>, Or<Where<Current<PX.Objects.SO.SOLine.isSpecialOrder>, Equal<True>, And<SupplyPOLine.curyID, Equal<Current<PX.Objects.SO.SOOrder.curyID>>, And<SupplyPOLine.uOM, Equal<Current<PX.Objects.SO.SOLine.uOM>>, And<SupplyPOLine.projectID, Equal<Current<PX.Objects.SO.SOLine.projectID>>, And2<Where<SupplyPOLine.taskID, Equal<Current<PX.Objects.SO.SOLine.taskID>>, Or<SupplyPOLine.taskID, IsNull, And<Current<PX.Objects.SO.SOLine.taskID>, IsNull>>>, And<Where<SupplyPOLine.costCodeID, Equal<Current<PX.Objects.SO.SOLine.costCodeID>>, Or<SupplyPOLine.costCodeID, IsNull, And<Current<PX.Objects.SO.SOLine.costCodeID>, IsNull>>>>>>>>>>>>>>>>>>>>>> pxSelectJoin = new PXSelectJoin<SupplyPOLine, LeftJoin<PX.Objects.SO.SOLineSplit, On<PX.Objects.SO.SOLineSplit.FK.SupplyLine>>, Where<SupplyPOLine.lineType, In3<POLineType.goodsForInventory, POLineType.nonStock, POLineType.goodsForSalesOrder, POLineType.goodsForServiceOrder, POLineType.nonStockForSalesOrder, POLineType.nonStockForServiceOrder, POLineType.goodsForReplenishment>, And2<Where<SupplyPOLine.orderType, Equal<POOrderType.blanket>, And<Current<PX.Objects.SO.SOLine.pOSource>, In3<INReplenishmentSource.blanketDropShipToOrder, INReplenishmentSource.blanketPurchaseToOrder>, Or<SupplyPOLine.orderType, Equal<POOrderType.regularOrder>, And<Current<PX.Objects.SO.SOLine.pOSource>, Equal<INReplenishmentSource.purchaseToOrder>>>>>, And2<Where<PX.Objects.SO.SOLineSplit.pOLineNbr, IsNotNull, Or<SupplyPOLine.completed, Equal<False>, And<SupplyPOLine.cancelled, Equal<False>>>>, And<SupplyPOLine.inventoryID, Equal<Current<PX.Objects.SO.SOLine.inventoryID>>, And2<Where<SupplyPOLine.subItemID, Equal<Current<PX.Objects.SO.SOLine.subItemID>>, Or<Current<PX.Objects.SO.SOLine.subItemID>, IsNull>>, And<SupplyPOLine.siteID, Equal<Current<PX.Objects.SO.SOLine.pOSiteID>>, And2<Where<SupplyPOLine.vendorID, Equal<Current<PX.Objects.SO.SOLine.vendorID>>, Or<Current<PX.Objects.SO.SOLine.vendorID>, IsNull>>, And<SupplyPOLine.vendorID, NotEqual<Current<PX.Objects.SO.SOOrder.customerID>>, And2<Where<Required<PX.Objects.GL.Branch.bAccountID>, IsNull, Or<SupplyPOLine.vendorID, NotEqual<Required<PX.Objects.GL.Branch.bAccountID>>>>, And<Where2<Where<Current<PX.Objects.SO.SOLine.isSpecialOrder>, NotEqual<True>, And<SupplyPOLine.isSpecialOrder, NotEqual<True>>>, Or<Where<Current<PX.Objects.SO.SOLine.isSpecialOrder>, Equal<True>, And<SupplyPOLine.curyID, Equal<Current<PX.Objects.SO.SOOrder.curyID>>, And<SupplyPOLine.uOM, Equal<Current<PX.Objects.SO.SOLine.uOM>>, And<SupplyPOLine.projectID, Equal<Current<PX.Objects.SO.SOLine.projectID>>, And2<Where<SupplyPOLine.taskID, Equal<Current<PX.Objects.SO.SOLine.taskID>>, Or<SupplyPOLine.taskID, IsNull, And<Current<PX.Objects.SO.SOLine.taskID>, IsNull>>>, And<Where<SupplyPOLine.costCodeID, Equal<Current<PX.Objects.SO.SOLine.costCodeID>>, Or<SupplyPOLine.costCodeID, IsNull, And<Current<PX.Objects.SO.SOLine.costCodeID>, IsNull>>>>>>>>>>>>>>>>>>>>>>((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base);
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current.BranchID);
    Dictionary<SupplyPOLine, HashSet<PX.Objects.SO.SOLineSplit>> supplyLineDemand = new Dictionary<SupplyPOLine, HashSet<PX.Objects.SO.SOLineSplit>>((IEqualityComparer<SupplyPOLine>) new KeyValuesComparer<SupplyPOLine>(((PXSelectBase) this.Base2.SupplyPOLines).Cache, (IEnumerable<Type>) ((PXSelectBase) this.Base2.SupplyPOLines).Cache.BqlKeys));
    object[] objArray = new object[2]
    {
      (object) branch.BAccountID,
      (object) branch.BAccountID
    };
    foreach (PXResult<SupplyPOLine, PX.Objects.SO.SOLineSplit> pxResult in ((PXSelectBase<SupplyPOLine>) pxSelectJoin).Select(objArray))
    {
      SupplyPOLine supplyPoLine = PXResult<SupplyPOLine, PX.Objects.SO.SOLineSplit>.op_Implicit(pxResult);
      PX.Objects.SO.SOLineSplit split = PXResult<SupplyPOLine, PX.Objects.SO.SOLineSplit>.op_Implicit(pxResult);
      if (!supplyLineDemand.ContainsKey(supplyPoLine))
        supplyLineDemand.Add(supplyPoLine, new HashSet<PX.Objects.SO.SOLineSplit>((IEqualityComparer<PX.Objects.SO.SOLineSplit>) new KeyValuesComparer<PX.Objects.SO.SOLineSplit>(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache, (IEnumerable<Type>) ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache.BqlKeys)));
      this.GatherSODemandFromDB(supplyPoLine, split, supplyLineDemand);
    }
    this.GatherSODemandFromCache(supplyLineDemand);
    bool allowUpdate = ((PXSelectBase) this.Base2.SOLineDemand).Cache.AllowUpdate;
    foreach (SupplyPOLine key in supplyLineDemand.Keys)
    {
      HashSet<PX.Objects.SO.SOLineSplit> source = supplyLineDemand[key];
      PXEntryStatus status = ((PXSelectBase) this.Base2.SupplyPOLines).Cache.GetStatus((object) key);
      bool? nullable1 = currentSOLine.IsSpecialOrder;
      if (!nullable1.GetValueOrDefault() || !source.Any<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s =>
      {
        if (s.OrderType != currentSOLine.OrderType || s.OrderNbr != currentSOLine.OrderNbr)
          return true;
        int? lineNbr3 = s.LineNbr;
        int? lineNbr4 = currentSOLine.LineNbr;
        return !(lineNbr3.GetValueOrDefault() == lineNbr4.GetValueOrDefault() & lineNbr3.HasValue == lineNbr4.HasValue);
      })))
      {
        if (status == null)
        {
          key.SelectedSOLines = new int?[0];
          key.LinkedSOLines = new int?[0];
        }
        key.BaseDemandQty = new Decimal?(0M);
        key.LinkedSOLines.SparseArrayClear<int>();
        Decimal num1 = 0M;
        bool flag = false;
        int? lineNbr1;
        foreach (PX.Objects.SO.SOLineSplit soLineSplit in source)
        {
          if (soLineSplit.OrderType == currentSOLine.OrderType && soLineSplit.OrderNbr == currentSOLine.OrderNbr)
          {
            key.LinkedSOLines = key.LinkedSOLines.SparseArrayAddDistinct<int>(new int?(soLineSplit.LineNbr.Value));
            int? lineNbr2;
            if (status == null)
            {
              SupplyPOLine supplyPoLine = key;
              int?[] selectedSoLines = key.SelectedSOLines;
              lineNbr2 = soLineSplit.LineNbr;
              int? nullable2 = new int?(lineNbr2.Value);
              int?[] nullableArray = selectedSoLines.SparseArrayAddDistinct<int>(nullable2);
              supplyPoLine.SelectedSOLines = nullableArray;
            }
            lineNbr2 = soLineSplit.LineNbr;
            lineNbr1 = currentSOLine.LineNbr;
            if (lineNbr2.GetValueOrDefault() == lineNbr1.GetValueOrDefault() & lineNbr2.HasValue == lineNbr1.HasValue)
              flag = true;
          }
          if (soLineSplit.PlanID.HasValue)
          {
            nullable1 = soLineSplit.POCompleted;
            if (!nullable1.GetValueOrDefault())
              num1 += soLineSplit.BaseQty.GetValueOrDefault();
          }
        }
        key.BaseDemandQty = new Decimal?(num1);
        SupplyPOLine supplyPoLine1 = key;
        int?[] selectedSoLines1 = key.SelectedSOLines;
        lineNbr1 = currentSOLine.LineNbr;
        int? nullable3 = new int?(lineNbr1.Value);
        bool? nullable4 = new bool?(((IEnumerable<int?>) selectedSoLines1).Contains<int?>(nullable3));
        supplyPoLine1.Selected = nullable4;
        GraphHelper.Hold(((PXSelectBase) this.Base2.SupplyPOLines).Cache, (object) key);
        if ((currentSOLine.POSource != "O" || key.OrderType != "RO") && (EnumerableExtensions.IsNotIn<string>(currentSOLine.POSource, "L", "B") || key.OrderType != "BL"))
          break;
        Decimal? nullable5;
        if (!(currentSOLine.Behavior != "BL"))
        {
          nullable5 = new Decimal?(-1M);
        }
        else
        {
          short? invtMult = currentSOLine.InvtMult;
          nullable5 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
        }
        Decimal? nullable6 = nullable5;
        Decimal? openQty = currentSOLine.OpenQty;
        Decimal? nullable7 = nullable6.HasValue & openQty.HasValue ? new Decimal?(nullable6.GetValueOrDefault() * openQty.GetValueOrDefault()) : new Decimal?();
        Decimal num2 = 0M;
        int num3;
        if (nullable7.GetValueOrDefault() < num2 & nullable7.HasValue)
        {
          Decimal? baseOpenQty = key.BaseOpenQty;
          nullable6 = key.BaseDemandQty;
          Decimal? nullable8 = baseOpenQty.HasValue & nullable6.HasValue ? new Decimal?(baseOpenQty.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
          Decimal num4 = 0M;
          if (nullable8.GetValueOrDefault() > num4 & nullable8.HasValue)
          {
            num3 = !key.Hold.GetValueOrDefault() ? 1 : (((PXGraphExtension<SOOrderEntry>) this).Base.SOPOLinkShowDocumentsOnHold ? 1 : 0);
            goto label_37;
          }
        }
        num3 = 0;
label_37:
        int num5 = allowUpdate ? 1 : 0;
        if ((num3 & num5) != 0 || flag)
          supplyPOLines.Add(key);
      }
    }
  }

  protected virtual void GatherSODemandFromDB(
    SupplyPOLine supplyLine,
    PX.Objects.SO.SOLineSplit split,
    Dictionary<SupplyPOLine, HashSet<PX.Objects.SO.SOLineSplit>> supplyLineDemand)
  {
    split = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Locate(split) ?? split;
    if (split == null || !split.SplitLineNbr.HasValue || !split.POLineNbr.HasValue || ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache.GetStatus((object) split) == 3 || !(split.POType == supplyLine.OrderType) || !(split.PONbr == supplyLine.OrderNbr))
      return;
    int? poLineNbr = split.POLineNbr;
    int? lineNbr = supplyLine.LineNbr;
    if (!(poLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & poLineNbr.HasValue == lineNbr.HasValue))
      return;
    supplyLineDemand[supplyLine].Add(split);
  }

  protected virtual void GatherSODemandFromCache(
    Dictionary<SupplyPOLine, HashSet<PX.Objects.SO.SOLineSplit>> supplyLineDemand)
  {
    foreach (PX.Objects.SO.SOLineSplit soLineSplit in ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache.Updated.Cast<PX.Objects.SO.SOLineSplit>().Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s => s.POLineNbr.HasValue)))
    {
      SupplyPOLine supplyPoLine1 = new SupplyPOLine()
      {
        OrderType = (string) ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache.GetValueOriginal<PX.Objects.SO.SOLineSplit.pOType>((object) soLineSplit),
        OrderNbr = (string) ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache.GetValueOriginal<PX.Objects.SO.SOLineSplit.pONbr>((object) soLineSplit),
        LineNbr = (int?) ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache.GetValueOriginal<PX.Objects.SO.SOLineSplit.pOLineNbr>((object) soLineSplit)
      };
      SupplyPOLine supplyPoLine2 = new SupplyPOLine()
      {
        OrderType = soLineSplit.POType,
        OrderNbr = soLineSplit.PONbr,
        LineNbr = soLineSplit.POLineNbr
      };
      if (!supplyLineDemand.Comparer.Equals(supplyPoLine1, supplyPoLine2))
      {
        if (supplyLineDemand.ContainsKey(supplyPoLine1))
          supplyLineDemand[supplyPoLine1].Remove(soLineSplit);
        if (supplyLineDemand.ContainsKey(supplyPoLine2))
          supplyLineDemand[supplyPoLine2].Add(soLineSplit);
      }
    }
    foreach (PX.Objects.SO.SOLineSplit soLineSplit in ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache.Inserted.Cast<PX.Objects.SO.SOLineSplit>().Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s => s.POLineNbr.HasValue)))
    {
      SupplyPOLine key = new SupplyPOLine()
      {
        OrderType = soLineSplit.POType,
        OrderNbr = soLineSplit.PONbr,
        LineNbr = soLineSplit.POLineNbr
      };
      if (supplyLineDemand.ContainsKey(key))
        supplyLineDemand[key].Add(soLineSplit);
    }
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOOrderEntryExt.POLinkDialog.LinkPOSupply(PX.Objects.SO.SOLine)" />
  /// </summary>
  [PXOverride]
  public virtual void LinkPOSupply(PX.Objects.SO.SOLine currentSOLine)
  {
    if (!((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.IsPoToSoOrBlanket(currentSOLine.POSource))
      return;
    bool deleted = EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache.GetStatus((object) currentSOLine), (PXEntryStatus) 3, (PXEntryStatus) 4);
    List<PX.Objects.SO.SOLineSplit> list = ((IEnumerable<PX.Objects.SO.SOLineSplit>) ((PXGraphExtension<SOOrderEntry>) this).Base.LineSplittingAllocatedExt.SelectSplitsForDropShip(currentSOLine)).ToList<PX.Objects.SO.SOLineSplit>();
    if (list.FirstOrDefault<PX.Objects.SO.SOLineSplit>() == null)
      return;
    if (currentSOLine.POSource == "L" && ((PXGraphExtension<SOOrderEntry>) this).Base.LineSplittingAllocatedExt.HasMultipleSplitsOrAllocation(currentSOLine))
      throw new PXException("The line cannot be drop-shipped because it is split into multiple lines or allocated in the Line Details dialog box.");
    bool flag1 = false;
    foreach (SupplyPOLine supply in ((PXSelectBase) this.Base2.SupplyPOLines).Cache.Updated)
    {
      if ((((IEnumerable<int?>) supply.SelectedSOLines).Contains<int?>(new int?(currentSOLine.LineNbr.Value)) ? 1 : 0) == 0 & ((IEnumerable<int?>) supply.LinkedSOLines).Contains<int?>(new int?(currentSOLine.LineNbr.Value)))
        flag1 |= this.UnlinkSupply(supply, currentSOLine, (IList<PX.Objects.SO.SOLineSplit>) list, deleted);
    }
    bool flag2 = false;
    foreach (SupplyPOLine supply in ((PXSelectBase) this.Base2.SupplyPOLines).Cache.Updated)
    {
      int num = ((IEnumerable<int?>) supply.SelectedSOLines).Contains<int?>(new int?(currentSOLine.LineNbr.Value)) ? 1 : 0;
      bool flag3 = ((IEnumerable<int?>) supply.LinkedSOLines).Contains<int?>(new int?(currentSOLine.LineNbr.Value));
      if (num != 0 && !flag3)
        flag2 |= this.LinkSupply(supply, currentSOLine, (IList<PX.Objects.SO.SOLineSplit>) list);
    }
    PX.Objects.SO.SOLine current = ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current;
    if (flag2)
    {
      if (current.POCreated.GetValueOrDefault())
        return;
      ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache.SetValue<PX.Objects.SO.SOLine.pOCreated>((object) current, (object) true);
    }
    else
    {
      if (!flag1 || !current.POCreated.GetValueOrDefault())
        return;
      bool flag4 = list.Any<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (x => x.POCreate.GetValueOrDefault() && x.PONbr != null));
      if (flag4)
        return;
      ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache.SetValue<PX.Objects.SO.SOLine.pOCreated>((object) current, (object) flag4);
    }
  }

  protected virtual bool UnlinkSupply(
    SupplyPOLine supply,
    PX.Objects.SO.SOLine currentSOLine,
    IList<PX.Objects.SO.SOLineSplit> splits,
    bool deleted)
  {
    bool flag1 = false;
    foreach (PX.Objects.SO.SOLineSplit split in (IEnumerable<PX.Objects.SO.SOLineSplit>) splits)
    {
      if (((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache.GetStatus((object) split) != 3 && ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache.GetStatus((object) split) != 4 && split.POType == supply.OrderType && split.PONbr == supply.OrderNbr)
      {
        int? poLineNbr = split.POLineNbr;
        int? lineNbr = supply.LineNbr;
        if (poLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & poLineNbr.HasValue == lineNbr.HasValue)
        {
          bool? nullable1 = split.POCompleted;
          bool flag2 = false;
          if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue)
          {
            nullable1 = split.Completed;
            bool flag3 = false;
            if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue)
            {
              if (split.POType != null && split.PONbr != null && split.POType == supply.OrderType && split.PONbr == supply.OrderNbr)
              {
                SupplyPOOrder parent = KeysRelation<CompositeKey<Field<SupplyPOLine.orderType>.IsRelatedTo<SupplyPOOrder.orderType>, Field<SupplyPOLine.orderNbr>.IsRelatedTo<SupplyPOOrder.orderNbr>>.WithTablesOf<SupplyPOOrder, SupplyPOLine>, SupplyPOOrder, SupplyPOLine>.FindParent((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, supply, (PKFindOptions) 0);
                if (parent != null)
                {
                  Guid? refNoteId = split.RefNoteID;
                  Guid? nullable2 = parent.NoteID;
                  if ((refNoteId.HasValue == nullable2.HasValue ? (refNoteId.HasValue ? (refNoteId.GetValueOrDefault() == nullable2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
                  {
                    PX.Objects.SO.SOLineSplit soLineSplit = split;
                    nullable2 = new Guid?();
                    Guid? nullable3 = nullable2;
                    soLineSplit.RefNoteID = nullable3;
                  }
                  if (parent.SOOrderType == split.OrderType && parent.SOOrderNbr == split.OrderNbr)
                  {
                    parent.SOOrderType = (string) null;
                    parent.SOOrderNbr = (string) null;
                    ((PXSelectBase<SupplyPOOrder>) ((PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>) this).Base1.SupplyPOOrders).Update(parent);
                  }
                }
              }
              if (!deleted)
              {
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
              }
            }
          }
        }
      }
    }
    supply.LinkedSOLines.SparseArrayRemove<int>(new int?(currentSOLine.LineNbr.Value));
    return flag1;
  }

  protected virtual bool LinkSupply(
    SupplyPOLine supply,
    PX.Objects.SO.SOLine currentSOLine,
    IList<PX.Objects.SO.SOLineSplit> splits)
  {
    bool flag1 = false;
    Decimal? baseOpenQty = supply.BaseOpenQty;
    Decimal? baseDemandQty = supply.BaseDemandQty;
    Decimal? nullable1 = baseOpenQty.HasValue & baseDemandQty.HasValue ? new Decimal?(baseOpenQty.GetValueOrDefault() - baseDemandQty.GetValueOrDefault()) : new Decimal?();
    for (int index = 0; index < splits.Count; ++index)
    {
      PX.Objects.SO.SOLineSplit copy1 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(splits[index]);
      Decimal? nullable2;
      if (string.IsNullOrEmpty(copy1.SOOrderNbr) && string.IsNullOrEmpty(copy1.PONbr))
      {
        bool? nullable3 = copy1.IsAllocated;
        bool flag2 = false;
        if (nullable3.GetValueOrDefault() == flag2 & nullable3.HasValue)
        {
          nullable3 = copy1.Completed;
          bool flag3 = false;
          if (nullable3.GetValueOrDefault() == flag3 & nullable3.HasValue)
          {
            nullable2 = copy1.BaseQty;
            Decimal num1 = 0M;
            Decimal? baseQty;
            if (!(nullable2.GetValueOrDefault() > num1 & nullable2.HasValue) || !(currentSOLine.POSource != "L"))
            {
              nullable2 = nullable1;
              baseQty = currentSOLine.BaseQty;
              if (!(nullable2.GetValueOrDefault() >= baseQty.GetValueOrDefault() & nullable2.HasValue & baseQty.HasValue))
                goto label_20;
            }
            if (supply.OrderType != "BL")
              supply.LineType = currentSOLine.LineType == "GI" ? "GS" : "NO";
            supply.LinkedSOLines = supply.LinkedSOLines.SparseArrayAddDistinct<int>(new int?(currentSOLine.LineNbr.Value));
            INItemPlan inItemPlan1 = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Required<INItemPlan.planID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[1]
            {
              (object) supply.PlanID
            }));
            if (inItemPlan1 != null)
            {
              if (supply.OrderType != "BL")
              {
                inItemPlan1.PlanType = "76";
                nullable3 = currentSOLine.IsSpecialOrder;
                if (nullable3.GetValueOrDefault())
                  inItemPlan1.CostCenterID = currentSOLine.CostCenterID;
                ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (INItemPlan)].Update((object) inItemPlan1);
              }
              INItemPlan inItemPlan2 = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Required<INItemPlan.planID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[1]
              {
                (object) copy1.PlanID
              }));
              inItemPlan2.PlanType = currentSOLine.POSource == "B" ? "6B" : (currentSOLine.POSource == "L" ? "6E" : (currentSOLine.POSource == "D" ? "6D" : "66"));
              inItemPlan2.FixedSource = "P";
              inItemPlan2.SupplyPlanID = supply.PlanID;
              SupplyPOOrder parent = KeysRelation<CompositeKey<Field<SupplyPOLine.orderType>.IsRelatedTo<SupplyPOOrder.orderType>, Field<SupplyPOLine.orderNbr>.IsRelatedTo<SupplyPOOrder.orderNbr>>.WithTablesOf<SupplyPOOrder, SupplyPOLine>, SupplyPOOrder, SupplyPOLine>.FindParent((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, supply, (PKFindOptions) 0);
              if (parent != null)
              {
                inItemPlan2.VendorID = parent.VendorID;
                inItemPlan2.VendorLocationID = parent.VendorLocationID;
              }
              ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (INItemPlan)].Update((object) inItemPlan2);
              copy1.POCreate = new bool?(true);
              copy1.VendorID = supply.VendorID;
              copy1.POType = supply.OrderType;
              copy1.PONbr = supply.OrderNbr;
              copy1.POLineNbr = supply.LineNbr;
              flag1 = true;
              baseQty = copy1.BaseQty;
              nullable2 = nullable1;
              PX.Objects.SO.SOLineSplit soLineSplit1;
              if (baseQty.GetValueOrDefault() <= nullable2.GetValueOrDefault() & baseQty.HasValue & nullable2.HasValue)
              {
                nullable2 = nullable1;
                baseQty = copy1.BaseQty;
                nullable1 = nullable2.HasValue & baseQty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - baseQty.GetValueOrDefault()) : new Decimal?();
                soLineSplit1 = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Update(copy1);
              }
              else
              {
                PX.Objects.SO.SOLineSplit copy2 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(copy1);
                copy2.SplitLineNbr = new int?();
                copy2.IsAllocated = new bool?(false);
                copy2.ClearPOFlags();
                copy2.ClearPOReferences();
                copy2.VendorID = new int?();
                copy2.POCreate = new bool?(true);
                PX.Objects.SO.SOLineSplit soLineSplit2 = copy2;
                baseQty = copy2.BaseQty;
                nullable2 = nullable1;
                Decimal? nullable4 = baseQty.HasValue & nullable2.HasValue ? new Decimal?(baseQty.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
                soLineSplit2.BaseQty = nullable4;
                PX.Objects.SO.SOLineSplit soLineSplit3 = copy2;
                PXCache cache1 = ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache;
                int? inventoryId1 = copy2.InventoryID;
                string uom1 = copy2.UOM;
                nullable2 = copy2.BaseQty;
                Decimal num2 = nullable2.Value;
                Decimal? nullable5 = new Decimal?(INUnitAttribute.ConvertFromBase(cache1, inventoryId1, uom1, num2, INPrecision.QUANTITY));
                soLineSplit3.Qty = nullable5;
                copy2.ShippedQty = new Decimal?(0M);
                copy2.ReceivedQty = new Decimal?(0M);
                copy2.UnreceivedQty = copy2.BaseQty;
                copy2.PlanID = new long?();
                copy2.Completed = new bool?(false);
                copy1.BaseQty = nullable1;
                PX.Objects.SO.SOLineSplit soLineSplit4 = copy1;
                PXCache cache2 = ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache;
                int? inventoryId2 = copy1.InventoryID;
                string uom2 = copy1.UOM;
                nullable2 = copy1.BaseQty;
                Decimal num3 = nullable2.Value;
                Decimal? nullable6 = new Decimal?(INUnitAttribute.ConvertFromBase(cache2, inventoryId2, uom2, num3, INPrecision.QUANTITY));
                soLineSplit4.Qty = nullable6;
                nullable1 = new Decimal?(0M);
                soLineSplit1 = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Update(copy1);
                PX.Objects.SO.SOLineSplit soLineSplit5;
                if ((soLineSplit5 = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Insert(copy2)) != null)
                  splits.Insert(index + 1, soLineSplit5);
              }
              splits[index] = soLineSplit1;
            }
            else
              continue;
          }
        }
      }
label_20:
      nullable2 = nullable1;
      Decimal num = 0M;
      if (nullable2.GetValueOrDefault() <= num & nullable2.HasValue)
        break;
    }
    return flag1;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.Intercompany
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.IN;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class Intercompany : 
  PXGraphExtension<CreateShipmentExtension, ConfirmShipmentExtension, SOShipmentEntry>
{
  public PXAction<PX.Objects.SO.SOShipment> generatePOReceipt;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.interBranch>() && PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable GeneratePOReceipt(PXAdapter adapter)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    Intercompany intercompany = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    Intercompany.\u003C\u003Ec__DisplayClass2_0 cDisplayClass20 = new Intercompany.\u003C\u003Ec__DisplayClass2_0();
    ((PXAction) ((PXGraphExtension<SOShipmentEntry>) intercompany).Base.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.shipment = ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) intercompany).Base.Document).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.shipLines = ((IEnumerable<PXResult<SOShipLine>>) PXSelectBase<SOShipLine, PXViewOf<SOShipLine>.BasedOn<SelectFromBase<SOShipLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOLine>.On<SOShipLine.FK.OrderLine>>>.Where<KeysRelation<CompositeKey<Field<SOShipLine.shipmentType>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentType>, Field<SOShipLine.shipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>>.WithTablesOf<PX.Objects.SO.SOShipment, SOShipLine>, PX.Objects.SO.SOShipment, SOShipLine>.SameAsCurrent>>.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) intercompany).Base, Array.Empty<object>())).AsEnumerable<PXResult<SOShipLine>>().Cast<PXResult<SOShipLine, PX.Objects.SO.SOLine>>().ToList<PXResult<SOShipLine, PX.Objects.SO.SOLine>>();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) ((PXGraphExtension<SOShipmentEntry>) intercompany).Base, new PXToggleAsyncDelegate((object) cDisplayClass20, __methodptr(\u003CGeneratePOReceipt\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) cDisplayClass20.shipment;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }

  public virtual PX.Objects.PO.POReceipt GenerateIntercompanyPOReceipt(
    PX.Objects.SO.SOShipment shipment,
    List<PXResult<SOShipLine, PX.Objects.SO.SOLine>> shipLines,
    bool? holdValue,
    DateTime? receiptDate)
  {
    if (!string.IsNullOrEmpty(shipment.IntercompanyPOReceiptNbr) || shipment.ShipmentType != "I" || shipment.Operation != "I")
      throw new PXInvalidOperationException();
    PX.Objects.SO.SOOrder[] array = GraphHelper.RowCast<PX.Objects.SO.SOOrder>((IEnumerable) PXSelectBase<PX.Objects.SO.SOOrder, PXViewOf<PX.Objects.SO.SOOrder>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOOrderShipment>.On<PX.Objects.SO.SOOrderShipment.FK.Order>>>.Where<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOOrderShipment.shipmentType>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentType>, Field<PX.Objects.SO.SOOrderShipment.shipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>>.WithTablesOf<PX.Objects.SO.SOShipment, PX.Objects.SO.SOOrderShipment>, PX.Objects.SO.SOShipment, PX.Objects.SO.SOOrderShipment>.SameAsCurrent>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, (object[]) new PX.Objects.SO.SOShipment[1]
    {
      shipment
    }, Array.Empty<object>())).ToArray<PX.Objects.SO.SOOrder>();
    Dictionary<PX.Objects.SO.SOOrder, PX.Objects.PO.POOrder> dictionary = new Dictionary<PX.Objects.SO.SOOrder, PX.Objects.PO.POOrder>(PXCacheEx.GetComparer<PX.Objects.SO.SOOrder>(GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base)));
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, shipment.OrderBranchID);
    PX.Objects.AP.Vendor vendor = PX.Objects.AP.Vendor.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, (int?) branch?.BAccountID);
    if (vendor == null)
      throw new PXException("The {0} company or branch has not been extended to a vendor. To create an intercompany purchase receipt, extend the company or branch to a vendor on the Companies (CS101500) or Branches (CS102000) form, respectively.", new object[1]
      {
        (object) branch?.BranchCD.TrimEnd()
      });
    PXAccess.MasterCollection.Branch branchByBaccountId = PXAccess.GetBranchByBAccountID(shipment.CustomerID);
    foreach (PX.Objects.SO.SOOrder key in array)
    {
      PX.Objects.PO.POOrder poOrder = (PX.Objects.PO.POOrder) null;
      if (key.Behavior == "SO")
        poOrder = PX.Objects.PO.POOrder.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, key.IntercompanyPOType, key.IntercompanyPONbr);
      else if (key.Behavior == "RM")
        poOrder = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(PXSelectBase<PX.Objects.PO.POOrder, PXViewOf<PX.Objects.PO.POOrder>.BasedOn<SelectFromBase<PX.Objects.PO.POOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POOrderReceipt>.On<PX.Objects.PO.POOrderReceipt.FK.Order>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POOrderReceipt.receiptType, Equal<POReceiptType.poreturn>>>>>.And<BqlOperand<PX.Objects.PO.POOrderReceipt.receiptNbr, IBqlString>.IsEqual<P.AsString>>>>.ReadOnly.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[1]
        {
          (object) key.IntercompanyPOReturnNbr
        }));
      if (poOrder != null)
      {
        bool? nullable = poOrder.Cancelled;
        if (nullable.GetValueOrDefault())
          throw new PXException("The purchase receipt cannot be created because the related {0} purchase order has been canceled.", new object[1]
          {
            (object) poOrder.OrderNbr
          });
        if (poOrder.OrderType == "DP")
        {
          if (poOrder.Status != "N")
          {
            string localizedLabel = PXStringListAttribute.GetLocalizedLabel<PX.Objects.PO.POOrder.status>(((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).Caches[typeof (PX.Objects.PO.POOrder)], (object) poOrder);
            throw new PXException("The purchase receipt cannot be created because the related {0} purchase order has the {1} status.", new object[2]
            {
              (object) poOrder.OrderNbr,
              (object) localizedLabel
            });
          }
          PX.Objects.SO.SOOrder soOrder = PX.Objects.SO.SOOrder.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, poOrder.SOOrderType, poOrder.SOOrderNbr);
          nullable = soOrder.Hold;
          if (nullable.GetValueOrDefault())
            throw new PXException("The purchase receipt cannot be created because the linked {0} sales order has the On Hold status.", new object[1]
            {
              (object) soOrder.OrderNbr
            });
        }
        dictionary.Add(key, poOrder);
      }
    }
    POReceiptEntry instance = PXGraph.CreateInstance<POReceiptEntry>();
    PX.Objects.PO.POOrder poOrder1 = dictionary.Values.FirstOrDefault<PX.Objects.PO.POOrder>();
    PX.Objects.SO.SOOrder soOrder1 = ((IEnumerable<PX.Objects.SO.SOOrder>) array).First<PX.Objects.SO.SOOrder>();
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = instance.MultiCurrencyExt.GetCurrencyInfo((long?) poOrder1?.CuryInfoID ?? soOrder1.CuryInfoID);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = instance.MultiCurrencyExt.CloneCurrencyInfo(currencyInfo1);
    PX.Objects.PO.POReceipt poReceipt = new PX.Objects.PO.POReceipt()
    {
      ReceiptType = "RT",
      ReceiptDate = receiptDate,
      BranchID = new int?(branchByBaccountId.BranchID),
      CuryID = currencyInfo2.CuryID,
      CuryInfoID = currencyInfo2.CuryInfoID
    };
    PX.Objects.PO.POReceipt copy1 = PXCache<PX.Objects.PO.POReceipt>.CreateCopy(((PXSelectBase<PX.Objects.PO.POReceipt>) instance.Document).Insert(poReceipt));
    copy1.VendorID = vendor.BAccountID;
    PX.Objects.PO.POReceipt copy2 = PXCache<PX.Objects.PO.POReceipt>.CreateCopy(((PXSelectBase<PX.Objects.PO.POReceipt>) instance.Document).Update(copy1));
    copy2.BranchID = new int?(branchByBaccountId.BranchID);
    PX.Objects.PO.POReceipt copy3 = PXCache<PX.Objects.PO.POReceipt>.CreateCopy(((PXSelectBase<PX.Objects.PO.POReceipt>) instance.Document).Update(copy2));
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = instance.MultiCurrencyExt.GetCurrencyInfo(copy3.CuryInfoID);
    currencyInfo3.CuryID = currencyInfo1.CuryID;
    if (string.Equals(currencyInfo1.BaseCuryID, currencyInfo3.BaseCuryID, StringComparison.OrdinalIgnoreCase))
      currencyInfo3.CuryRateTypeID = currencyInfo1.CuryRateTypeID;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo4 = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) instance.MultiCurrencyExt.currencyinfo).Update(currencyInfo3);
    copy3.CuryID = currencyInfo4.CuryID;
    copy3.ProjectID = (int?) poOrder1?.ProjectID ?? soOrder1.ProjectID;
    copy3.AutoCreateInvoice = new bool?(false);
    copy3.InvoiceNbr = shipment.ShipmentNbr;
    copy3.IntercompanyShipmentNbr = shipment.ShipmentNbr;
    ((PXSelectBase<PX.Objects.PO.POReceipt>) instance.Document).Update(copy3);
    foreach (PXResult<SOShipLine, PX.Objects.SO.SOLine> shipLine in shipLines)
    {
      PX.Objects.SO.SOLine soLine = PXResult<SOShipLine, PX.Objects.SO.SOLine>.op_Implicit(shipLine);
      PX.Objects.SO.SOOrder soOrder2 = ((IEnumerable<PX.Objects.SO.SOOrder>) array).FirstOrDefault<PX.Objects.SO.SOOrder>((Func<PX.Objects.SO.SOOrder, bool>) (order => order.OrderNbr == soLine.OrderNbr && order.OrderType == soLine.OrderType));
      PX.Objects.PO.POOrder po;
      dictionary.TryGetValue(soOrder2, out po);
      this.GeneratePOReceiptLine(instance, po, PXResult<SOShipLine, PX.Objects.SO.SOLine>.op_Implicit(shipLine), soOrder2, soLine);
    }
    if (((PXSelectBase<PX.Objects.PO.POReceipt>) instance.Document).Current != null)
    {
      PX.Objects.PO.POReceipt copy4 = PXCache<PX.Objects.PO.POReceipt>.CreateCopy(((PXSelectBase<PX.Objects.PO.POReceipt>) instance.Document).Current);
      copy4.ControlQty = copy4.OrderQty;
      ((PXSelectBase<PX.Objects.PO.POReceipt>) instance.Document).Update(copy4);
    }
    UniquenessChecker<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<PX.Objects.PO.POReceipt.intercompanyShipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>.AsSimpleKey.WithTablesOf<PX.Objects.SO.SOShipment, PX.Objects.PO.POReceipt>, PX.Objects.SO.SOShipment, PX.Objects.PO.POReceipt>.SameAsCurrent.And<BqlOperand<PX.Objects.PO.POReceipt.canceled, IBqlBool>.IsEqual<False>>>> uniquenessChecker = new UniquenessChecker<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<PX.Objects.PO.POReceipt.intercompanyShipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>.AsSimpleKey.WithTablesOf<PX.Objects.SO.SOShipment, PX.Objects.PO.POReceipt>, PX.Objects.SO.SOShipment, PX.Objects.PO.POReceipt>.SameAsCurrent.And<BqlOperand<PX.Objects.PO.POReceipt.canceled, IBqlBool>.IsEqual<False>>>>((IBqlTable) shipment);
    ((PXGraph) instance).OnBeforeCommit += new Action<PXGraph>(uniquenessChecker.OnBeforeCommitImpl);
    try
    {
      ((PXAction) instance.Save).Press();
      if (holdValue.HasValue)
      {
        if (holdValue.GetValueOrDefault())
          ((PXAction) instance.putOnHold).Press();
        else
          ((PXAction) instance.releaseFromHold).Press();
      }
    }
    finally
    {
      ((PXGraph) instance).OnBeforeCommit -= new Action<PXGraph>(uniquenessChecker.OnBeforeCommitImpl);
    }
    return ((PXSelectBase<PX.Objects.PO.POReceipt>) instance.Document).Current;
  }

  protected virtual PX.Objects.PO.POReceiptLine GeneratePOReceiptLine(
    POReceiptEntry graph,
    PX.Objects.PO.POOrder po,
    SOShipLine shipLine,
    PX.Objects.SO.SOOrder so,
    PX.Objects.SO.SOLine soLine)
  {
    PX.Objects.PO.POLine aLine = (PX.Objects.PO.POLine) null;
    if (po != null)
    {
      if (so.Behavior == "SO")
        aLine = PX.Objects.PO.POLine.PK.Find((PXGraph) graph, po?.OrderType, po?.OrderNbr, soLine.IntercompanyPOLineNbr);
      else if (so.Behavior == "RM")
      {
        PX.Objects.SO.SOLine parent = KeysRelation<CompositeKey<Field<PX.Objects.SO.SOLine.origOrderType>.IsRelatedTo<PX.Objects.SO.SOLine.orderType>, Field<PX.Objects.SO.SOLine.origOrderNbr>.IsRelatedTo<PX.Objects.SO.SOLine.orderNbr>, Field<PX.Objects.SO.SOLine.origLineNbr>.IsRelatedTo<PX.Objects.SO.SOLine.lineNbr>>.WithTablesOf<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine>, PX.Objects.SO.SOLine, PX.Objects.SO.SOLine>.FindParent((PXGraph) graph, soLine, (PKFindOptions) 0);
        if (parent != null)
          aLine = PXResultset<PX.Objects.PO.POLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POLine, PXViewOf<PX.Objects.PO.POLine>.BasedOn<SelectFromBase<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POReceiptLine>.On<PX.Objects.PO.POReceiptLine.FK.OrderLine>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptType, Equal<POReceiptType.poreturn>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly.Config>.Select((PXGraph) graph, new object[2]
          {
            (object) so.IntercompanyPOReturnNbr,
            (object) parent.IntercompanyPOLineNbr
          }));
      }
    }
    PX.Objects.PO.POReceiptLine poReceiptLine1;
    if (aLine != null)
    {
      bool isLSEntryBlocked = !POLineType.IsNonStockNonServiceNonDropShip(aLine.LineType);
      poReceiptLine1 = graph.AddPOLine(aLine, isLSEntryBlocked);
      poReceiptLine1.IsLSEntryBlocked = new bool?(isLSEntryBlocked);
      graph.AddPOOrderReceipt(aLine.OrderType, aLine.OrderNbr);
    }
    else
    {
      PX.Objects.PO.POReceiptLine poReceiptLine2 = new PX.Objects.PO.POReceiptLine()
      {
        IsStockItem = shipLine.IsStockItem,
        InventoryID = shipLine.InventoryID,
        SubItemID = shipLine.SubItemID,
        IsLSEntryBlocked = new bool?(true)
      };
      PX.Objects.PO.POReceiptLine copy = PXCache<PX.Objects.PO.POReceiptLine>.CreateCopy(((PXSelectBase<PX.Objects.PO.POReceiptLine>) graph.transactions).Insert(poReceiptLine2));
      copy.ProjectID = soLine.ProjectID;
      copy.TaskID = soLine.TaskID;
      copy.CostCodeID = soLine.CostCodeID;
      poReceiptLine1 = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) graph.transactions).Update(copy);
      poReceiptLine1.IsLSEntryBlocked = new bool?(!POLineType.IsNonStockNonServiceNonDropShip(poReceiptLine1.LineType));
    }
    PX.Objects.PO.POReceiptLine copy1 = PXCache<PX.Objects.PO.POReceiptLine>.CreateCopy(poReceiptLine1);
    copy1.UOM = shipLine.UOM;
    copy1.TranDesc = shipLine.TranDesc;
    PX.Objects.PO.POReceiptLine copy2 = PXCache<PX.Objects.PO.POReceiptLine>.CreateCopy(((PXSelectBase<PX.Objects.PO.POReceiptLine>) graph.transactions).Update(copy1));
    copy2.Qty = shipLine.ShippedQty;
    PX.Objects.PO.POReceiptLine copy3 = PXCache<PX.Objects.PO.POReceiptLine>.CreateCopy(((PXSelectBase<PX.Objects.PO.POReceiptLine>) graph.transactions).Update(copy2));
    PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = ((PXGraph) graph).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo();
    copy3.CuryUnitCost = soLine.CuryUnitPrice;
    copy3.ManualPrice = new bool?(true);
    PX.Objects.PO.POReceiptLine copy4 = PXCache<PX.Objects.PO.POReceiptLine>.CreateCopy(((PXSelectBase<PX.Objects.PO.POReceiptLine>) graph.transactions).Update(copy3));
    Decimal? baseQty1 = soLine.BaseQty;
    Decimal num = 0M;
    Decimal? nullable1;
    Decimal? nullable2;
    if (!(baseQty1.GetValueOrDefault() == num & baseQty1.HasValue))
    {
      Decimal? baseQty2 = shipLine.BaseQty;
      nullable1 = soLine.BaseQty;
      nullable2 = baseQty2.HasValue & nullable1.HasValue ? new Decimal?(baseQty2.GetValueOrDefault() / nullable1.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable2 = new Decimal?(1M);
    Decimal? nullable3 = nullable2;
    PX.Objects.PO.POReceiptLine poReceiptLine3 = copy4;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = defaultCurrencyInfo;
    nullable1 = soLine.CuryExtPrice;
    Decimal? nullable4 = nullable3;
    Decimal? nullable5 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal valueOrDefault1 = nullable5.GetValueOrDefault();
    Decimal? nullable6 = new Decimal?(currencyInfo1.RoundCury(valueOrDefault1));
    poReceiptLine3.CuryExtCost = nullable6;
    PX.Objects.PO.POReceiptLine copy5 = PXCache<PX.Objects.PO.POReceiptLine>.CreateCopy(((PXSelectBase<PX.Objects.PO.POReceiptLine>) graph.transactions).Update(copy4));
    copy5.DiscPct = soLine.DiscPct;
    PX.Objects.PO.POReceiptLine copy6 = PXCache<PX.Objects.PO.POReceiptLine>.CreateCopy(((PXSelectBase<PX.Objects.PO.POReceiptLine>) graph.transactions).Update(copy5));
    PX.Objects.PO.POReceiptLine poReceiptLine4 = copy6;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = defaultCurrencyInfo;
    nullable1 = soLine.CuryDiscAmt;
    Decimal? nullable7 = nullable3;
    Decimal? nullable8;
    if (!(nullable1.HasValue & nullable7.HasValue))
    {
      nullable5 = new Decimal?();
      nullable8 = nullable5;
    }
    else
      nullable8 = new Decimal?(nullable1.GetValueOrDefault() * nullable7.GetValueOrDefault());
    nullable5 = nullable8;
    Decimal valueOrDefault2 = nullable5.GetValueOrDefault();
    Decimal? nullable9 = new Decimal?(currencyInfo2.RoundCury(valueOrDefault2));
    poReceiptLine4.CuryDiscAmt = nullable9;
    PX.Objects.PO.POReceiptLine copy7 = PXCache<PX.Objects.PO.POReceiptLine>.CreateCopy(((PXSelectBase<PX.Objects.PO.POReceiptLine>) graph.transactions).Update(copy6));
    copy7.IsLSEntryBlocked = new bool?(false);
    copy7.IntercompanyShipmentLineNbr = shipLine.LineNbr;
    PX.Objects.PO.POReceiptLine line = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) graph.transactions).Update(copy7);
    ((PXSelectBase) graph.transactions).Cache.SetDefaultExt<PX.Objects.PO.POReceiptLine.locationID>((object) line);
    if (line.IsStockItem.GetValueOrDefault() && (line.LineType != "GP" || this.IsLotSerNumberedRequiredForDropship(line)))
    {
      POReceiptEntry poReceiptEntry = graph;
      object[] objArray1 = (object[]) new SOShipLine[1]
      {
        shipLine
      };
      object[] objArray2 = Array.Empty<object>();
      foreach (SOShipLineSplit soShipLineSplit in GraphHelper.RowCast<SOShipLineSplit>((IEnumerable) PXSelectBase<SOShipLineSplit, PXViewOf<SOShipLineSplit>.BasedOn<SelectFromBase<SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<SOShipLineSplit.shipmentNbr>.IsRelatedTo<SOShipLine.shipmentNbr>, Field<SOShipLineSplit.lineNbr>.IsRelatedTo<SOShipLine.lineNbr>>.WithTablesOf<SOShipLine, SOShipLineSplit>, SOShipLine, SOShipLineSplit>.SameAsCurrent>>.Config>.SelectMultiBound((PXGraph) poReceiptEntry, objArray1, objArray2)).ToList<SOShipLineSplit>())
      {
        POReceiptLineSplit receiptLineSplit = new POReceiptLineSplit()
        {
          InventoryID = soShipLineSplit.InventoryID,
          SubItemID = soShipLineSplit.SubItemID,
          LotSerialNbr = soShipLineSplit.LotSerialNbr,
          ExpireDate = soShipLineSplit.ExpireDate
        };
        POReceiptLineSplit copy8 = PXCache<POReceiptLineSplit>.CreateCopy(((PXSelectBase<POReceiptLineSplit>) graph.splits).Insert(receiptLineSplit));
        copy8.Qty = soShipLineSplit.Qty;
        ((PXSelectBase<POReceiptLineSplit>) graph.splits).Update(copy8);
      }
    }
    return line;
  }

  private bool IsLotSerNumberedRequiredForDropship(PX.Objects.PO.POReceiptLine line)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, line.InventoryID);
    if (inventoryItem != null)
    {
      bool? stkItem = inventoryItem.StkItem;
      bool flag = false;
      if (stkItem.GetValueOrDefault() == flag & stkItem.HasValue)
        return false;
    }
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, inventoryItem.LotSerClassID);
    return EnumerableExtensions.IsIn<string>(inLotSerClass.LotSerTrack, "S", "L") && inLotSerClass.RequiredForDropship.GetValueOrDefault();
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.CreateShipmentExtension.SetShipmentFieldsFromOrigDocument(PX.Objects.SO.SOShipment,PX.Objects.SO.CreateShipmentArgs,System.Boolean)" />
  [PXOverride]
  public virtual bool SetShipmentFieldsFromOrigDocument(
    PX.Objects.SO.SOShipment shipment,
    CreateShipmentArgs args,
    bool newlyCreated,
    Func<PX.Objects.SO.SOShipment, CreateShipmentArgs, bool, bool> baseMethod)
  {
    bool flag = baseMethod(shipment, args, newlyCreated);
    if (args.Order == null || !((bool?) PXFormulaAttribute.Evaluate<PX.Objects.SO.SOShipment.isIntercompany>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Cache, (object) shipment)).GetValueOrDefault())
      return flag;
    if (!shipment.OrderBranchID.HasValue)
    {
      shipment.OrderBranchID = args.Order.BranchID;
    }
    else
    {
      int? orderBranchId = shipment.OrderBranchID;
      int? branchId = args.Order.BranchID;
      if (!(orderBranchId.GetValueOrDefault() == branchId.GetValueOrDefault() & orderBranchId.HasValue == branchId.HasValue))
        throw new PXException("You cannot add the sales order to the shipment because the shipment already has a related sales order with a different branch ID.");
    }
    return flag;
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.CreateShipmentExtension.GetShipmentFieldLookups(PX.Objects.SO.CreateShipmentArgs)" />
  [PXOverride]
  public virtual FieldLookup[] GetShipmentFieldLookups(
    CreateShipmentArgs args,
    Func<CreateShipmentArgs, FieldLookup[]> baseMethod)
  {
    if (args.Order == null)
      return baseMethod(args);
    List<FieldLookup> fieldLookupList = new List<FieldLookup>((IEnumerable<FieldLookup>) baseMethod(args));
    int num;
    if ("I" == args.ShipmentType)
    {
      PX.Objects.AR.Customer customer = PX.Objects.AR.Customer.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, args.Order.CustomerID);
      num = customer != null ? (customer.IsBranch.GetValueOrDefault() ? 1 : 0) : 0;
    }
    else
      num = 0;
    if (num != 0)
      fieldLookupList.Add((FieldLookup) new FieldLookup<PX.Objects.SO.SOShipment.orderBranchID>((object) args.Order.BranchID));
    return fieldLookupList.ToArray();
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.ConfirmShipmentExtension.GroupShipLinesForInvoicing(PX.Objects.SO.SOShipment)" />
  [PXOverride]
  public virtual void GroupShipLinesForInvoicing(PX.Objects.SO.SOShipment ship, Action<PX.Objects.SO.SOShipment> baseMethod)
  {
    if (!ship.IsIntercompany.GetValueOrDefault())
    {
      baseMethod(ship);
    }
    else
    {
      foreach (IGrouping<\u003C\u003Ef__AnonymousType102<string, string, int?>, SOShipLine> grouping in GraphHelper.RowCast<SOShipLine>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache.Updated).Where<SOShipLine>((Func<SOShipLine, bool>) (sl => sl.ShipmentNbr == ship.ShipmentNbr && sl.ShipmentType == ship.ShipmentType)).GroupBy(sl => new
      {
        OrigOrderType = sl.OrigOrderType,
        OrigOrderNbr = sl.OrigOrderNbr,
        OrigLineNbr = sl.OrigLineNbr
      }))
      {
        int cnt = 0;
        Action<SOShipLine> action = (Action<SOShipLine>) (sl => sl.InvoiceGroupNbr = new int?(++cnt));
        EnumerableExtensions.ForEach<SOShipLine>((IEnumerable<SOShipLine>) grouping, action);
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PX.Objects.SO.SOShipment> eventArgs)
  {
    if (eventArgs.Row == null || eventArgs.Row.ShipmentType != "I" || eventArgs.Row.Operation != "I" || !eventArgs.Row.IsIntercompany.GetValueOrDefault())
      return;
    using (new PXReadBranchRestrictedScope())
    {
      PX.Objects.PO.POReceipt poReceipt = PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceipt, PXViewOf<PX.Objects.PO.POReceipt>.BasedOn<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<PX.Objects.PO.POReceipt.intercompanyShipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>.AsSimpleKey.WithTablesOf<PX.Objects.SO.SOShipment, PX.Objects.PO.POReceipt>, PX.Objects.SO.SOShipment, PX.Objects.PO.POReceipt>.SameAsCurrent.And<BqlOperand<PX.Objects.PO.POReceipt.canceled, IBqlBool>.IsEqual<False>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, (object[]) new PX.Objects.SO.SOShipment[1]
      {
        eventArgs.Row
      }, Array.Empty<object>()));
      eventArgs.Row.IntercompanyPOReceiptNbr = poReceipt?.ReceiptNbr;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOShipment> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    bool isIntercompanyIssue = eventArgs.Row.ShipmentType == "I" && eventArgs.Row.IsIntercompany.GetValueOrDefault();
    PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOShipment>>) eventArgs).Cache, (object) eventArgs.Row).For<PX.Objects.SO.SOShipment.intercompanyPOReceiptNbr>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Visible = isIntercompanyIssue;
      a.Enabled = false;
    })).For<PX.Objects.SO.SOShipment.excludeFromIntercompanyProc>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Visible = isIntercompanyIssue;
      a.Enabled = true;
    }));
    if (!isIntercompanyIssue)
      return;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOShipment>>) eventArgs).Cache.AllowUpdate = true;
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOShipment.shipmentNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOShipment>>) eventArgs).Cache, (object) eventArgs.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.SO.SOOrderShipment> eventArgs)
  {
    if (((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current == null || !((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.IsIntercompany.GetValueOrDefault() || ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.OrderBranchID.HasValue)
      return;
    ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.OrderBranchID = PX.Objects.SO.SOOrder.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, eventArgs.Row.OrderType, eventArgs.Row.OrderNbr).BranchID;
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.SO.SOOrderShipment> eventArgs)
  {
    if (((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current == null || !((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.IsIntercompany.GetValueOrDefault() || ((PXSelectBase<SOShipLine>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Any<SOShipLine>())
      return;
    ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.OrderBranchID = new int?();
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current<PX.Objects.SO.SOShipment.orderBranchID>, IsNull, Or<PX.Objects.SO.SOOrder.branchID, Equal<Current<PX.Objects.SO.SOShipment.orderBranchID>>>>), "You cannot add the sales order to the shipment because the shipment already has a related sales order with a different branch ID.", new Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<AddSOFilter.orderNbr> e)
  {
  }
}

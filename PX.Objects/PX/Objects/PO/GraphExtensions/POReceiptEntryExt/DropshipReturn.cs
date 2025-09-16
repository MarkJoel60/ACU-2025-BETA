// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.DropshipReturn
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
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.IN;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class DropshipReturn : PXGraphExtension<POReceiptEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.dropShipments>();

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBDefaultAttribute))]
  [PXDBDefault(typeof (PX.Objects.PO.POReceipt.receiptNbr), DefaultForUpdate = false)]
  [PXParent(typeof (Select<PX.Objects.PO.POReceipt, Where<PX.Objects.PO.POReceipt.receiptType, Equal<Switch<Case<Where<Current<PX.Objects.SO.SOOrderShipment.operation>, Equal<SOOperation.receipt>>, POReceiptType.poreturn>, POReceiptType.poreceipt>>, And<PX.Objects.PO.POReceipt.receiptNbr, Equal<Current<PX.Objects.SO.SOOrderShipment.shipmentNbr>>, And<Current<PX.Objects.SO.SOOrderShipment.shipmentType>, Equal<INDocType.dropShip>>>>>))]
  public virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.SO.SOOrderShipment.shipmentNbr> e)
  {
  }

  public virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POReceipt> e)
  {
    PXUIFieldAttribute.SetDisplayName<PX.Objects.PO.POReceiptLine.sOOrderType>(((PXSelectBase) this.Base.transactions).Cache, e.Row?.ReceiptType == "RN" ? "SO Return Type" : "Transfer Order Type");
    PXUIFieldAttribute.SetDisplayName<PX.Objects.PO.POReceiptLine.sOOrderNbr>(((PXSelectBase) this.Base.transactions).Cache, e.Row?.ReceiptType == "RN" ? "SO Return" : "Transfer Order Nbr.");
    PXUIFieldAttribute.SetDisplayName<PX.Objects.PO.POReceiptLine.sOOrderLineNbr>(((PXSelectBase) this.Base.transactions).Cache, e.Row?.ReceiptType == "RN" ? "SO Return Line" : "Transfer Line Nbr.");
    if (e.Row == null)
      return;
    bool flag = !string.IsNullOrEmpty(e.Row.SOOrderNbr);
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POReceipt.sOOrderNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceipt>>) e).Cache, (object) e.Row, flag);
    if (!flag)
      return;
    ((PXSelectBase) this.Base.transactions).Cache.AllowInsert = ((PXSelectBase) this.Base.transactions).Cache.AllowUpdate = ((PXSelectBase) this.Base.transactions).Cache.AllowDelete = false;
    ((PXAction) this.Base.addPOReceiptReturn).SetEnabled(false);
    ((PXAction) this.Base.addPOReceiptLineReturn).SetEnabled(false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POReceipt.returnInventoryCostMode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceipt>>) e).Cache, (object) e.Row, false);
  }

  [PXOverride]
  public void UpdateReturnLineOnRelease(
    PXResult<PX.Objects.PO.POReceiptLine> row,
    PX.Objects.PO.POLine poLine,
    Action<PXResult<PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POLine> base_UpdateReturnLineOnRelease)
  {
    base_UpdateReturnLineOnRelease(row, poLine);
    this.UpdateReturnOrdersMarkedForDropship(PXResult<PX.Objects.PO.POReceiptLine>.op_Implicit(row), poLine);
  }

  protected virtual void UpdateReturnOrdersMarkedForDropship(PX.Objects.PO.POReceiptLine line, PX.Objects.PO.POLine poLine)
  {
    if (POLineType.IsDropShip(line.LineType))
    {
      SOLine4 soLine4 = PXResultset<SOLine4>.op_Implicit(PXSelectBase<SOLine4, PXViewOf<SOLine4>.BasedOn<SelectFromBase<SOLine4, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLine4.orderType, Equal<BqlField<PX.Objects.PO.POReceiptLine.sOOrderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<SOLine4.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.sOOrderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<SOLine4.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.sOOrderLineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
      {
        (object) line
      }, Array.Empty<object>()));
      if (soLine4 != null && !(soLine4.UOM != line.UOM))
      {
        short? lineSign1 = soLine4.LineSign;
        Decimal? nullable1 = lineSign1.HasValue ? new Decimal?((Decimal) lineSign1.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable2 = soLine4.OrderQty;
        Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable4 = line.ReceiptQty;
        if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
        {
          lineSign1 = soLine4.LineSign;
          nullable2 = lineSign1.HasValue ? new Decimal?((Decimal) lineSign1.GetValueOrDefault()) : new Decimal?();
          nullable1 = soLine4.OpenQty;
          nullable4 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
          nullable3 = line.ReceiptQty;
          if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue && !(soLine4.Operation != "R"))
          {
            bool? nullable5 = soLine4.POCreate;
            if (nullable5.GetValueOrDefault() && !(soLine4.POSource != "D"))
            {
              nullable5 = soLine4.OpenLine;
              if (nullable5.GetValueOrDefault())
              {
                nullable5 = soLine4.Completed;
                if (!nullable5.GetValueOrDefault())
                {
                  SO2POSync extension = ((PXGraph) this.Base).GetExtension<SO2POSync>();
                  SOLine4 copy1 = PXCache<SOLine4>.CreateCopy(soLine4);
                  copy1.ShippedQty = copy1.OrderQty;
                  copy1.BaseShippedQty = copy1.BaseOrderQty;
                  copy1.OpenQty = new Decimal?(0M);
                  copy1.CuryOpenAmt = new Decimal?(0M);
                  copy1.Completed = new bool?(true);
                  copy1.OpenLine = new bool?(false);
                  SOLine4 soLine = extension.SOLineCache.Update(copy1);
                  foreach (PXResult<PX.Objects.SO.SOLineSplit> pxResult in PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.orderType, Equal<BqlField<PX.Objects.PO.POReceiptLine.sOOrderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.sOOrderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.sOOrderLineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
                  {
                    (object) line
                  }, Array.Empty<object>()))
                  {
                    PX.Objects.SO.SOLineSplit copy2 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(PXResult<PX.Objects.SO.SOLineSplit>.op_Implicit(pxResult));
                    copy2.ReceivedQty = copy2.Qty;
                    copy2.ShippedQty = copy2.Qty;
                    copy2.Completed = new bool?(true);
                    copy2.POReceiptType = line.ReceiptType;
                    copy2.POReceiptNbr = line.ReceiptNbr;
                    copy2.POCompleted = new bool?(true);
                    copy2.PlanID = new long?();
                    extension.SOSplitCache.Update(copy2);
                  }
                  PX.Objects.SO.SOOrder order1 = PXParentAttribute.SelectParent<PX.Objects.SO.SOOrder>((PXCache) extension.SOLineCache, (object) soLine);
                  if (!order1.Approved.GetValueOrDefault())
                  {
                    object valueExt = ((PXCache) extension.SOOrderCache).GetValueExt<PX.Objects.SO.SOOrder.ownerID>((object) order1);
                    throw new PXException("The {0} {1} sales order related to this drop-ship receipt is not approved. Contact {2} for details.", new object[3]
                    {
                      (object) order1.OrderType,
                      (object) order1.OrderNbr,
                      valueExt
                    });
                  }
                  extension.PopulateDropshipFields(order1);
                  PX.Objects.SO.SOOrder soOrder = order1;
                  int? openLineCntr1 = soOrder.OpenLineCntr;
                  soOrder.OpenLineCntr = openLineCntr1.HasValue ? new int?(openLineCntr1.GetValueOrDefault() - 1) : new int?();
                  PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = this.Base.MultiCurrencyExt.GetCurrencyInfo(order1.CuryInfoID);
                  Decimal? actualDiscUnitPrice = extension.GetActualDiscUnitPrice(soLine);
                  Decimal? nullable6;
                  ref Decimal? local = ref nullable6;
                  PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = currencyInfo1;
                  short? lineSign2 = soLine.LineSign;
                  Decimal? nullable7 = lineSign2.HasValue ? new Decimal?((Decimal) lineSign2.GetValueOrDefault()) : new Decimal?();
                  Decimal? nullable8 = soLine.OrderQty;
                  Decimal? nullable9 = nullable7.HasValue & nullable8.HasValue ? new Decimal?(nullable7.GetValueOrDefault() * nullable8.GetValueOrDefault()) : new Decimal?();
                  Decimal? nullable10 = actualDiscUnitPrice;
                  Decimal? nullable11;
                  if (!(nullable9.HasValue & nullable10.HasValue))
                  {
                    nullable8 = new Decimal?();
                    nullable11 = nullable8;
                  }
                  else
                    nullable11 = new Decimal?(nullable9.GetValueOrDefault() * nullable10.GetValueOrDefault());
                  nullable8 = nullable11;
                  Decimal val = nullable8.Value;
                  Decimal num1 = currencyInfo2.RoundBase(val);
                  local = new Decimal?(num1);
                  SO2POSync so2PoSync = extension;
                  PX.Objects.SO.SOOrder order2 = order1;
                  PX.Objects.PO.POReceiptLine line1 = line;
                  short? lineSign3 = soLine.LineSign;
                  Decimal? nullable12;
                  if (!lineSign3.HasValue)
                  {
                    nullable8 = new Decimal?();
                    nullable12 = nullable8;
                  }
                  else
                    nullable12 = new Decimal?((Decimal) lineSign3.GetValueOrDefault());
                  nullable9 = nullable12;
                  nullable10 = soLine.BaseOrderQty;
                  Decimal? qty;
                  if (!(nullable9.HasValue & nullable10.HasValue))
                  {
                    nullable8 = new Decimal?();
                    qty = nullable8;
                  }
                  else
                    qty = new Decimal?(nullable9.GetValueOrDefault() * nullable10.GetValueOrDefault());
                  Decimal? amt = nullable6;
                  so2PoSync.CreateUpdateOrderShipment(order2, line1, (POAddress) null, true, qty, amt);
                  int? openShipmentCntr = order1.OpenShipmentCntr;
                  int num2 = 0;
                  if (!(openShipmentCntr.GetValueOrDefault() == num2 & openShipmentCntr.HasValue))
                    return;
                  int? openLineCntr2 = order1.OpenLineCntr;
                  int num3 = 0;
                  if (!(openLineCntr2.GetValueOrDefault() == num3 & openLineCntr2.HasValue))
                    return;
                  order1.MarkCompleted();
                  return;
                }
              }
            }
          }
        }
      }
      throw new PXException("The corresponding line of the RMA order has been modified, the vendor return cannot be released.");
    }
  }

  public virtual void CreatePOReturn(PX.Objects.SO.SOOrder currentDoc, DocumentList<PX.Objects.PO.POReceipt> receiptList)
  {
    List<PXResult<PX.Objects.SO.SOLine, PX.Objects.SO.SOOrder, PX.Objects.AR.ARTran, PX.Objects.PO.POReceipt, POReceiptLineReturn>> list = ((IEnumerable<PXResult<PX.Objects.SO.SOLine>>) PXSelectBase<PX.Objects.SO.SOLine, PXViewOf<PX.Objects.SO.SOLine>.BasedOn<SelectFromBase<PX.Objects.SO.SOLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<PX.Objects.SO.SOLine.FK.Order>>, FbqlJoins.Inner<PX.Objects.AR.ARTran>.On<PX.Objects.SO.SOLine.FK.InvoiceLine>>, FbqlJoins.Inner<PX.Objects.PO.POReceipt>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.receiptType, Equal<POReceiptType.poreceipt>>>>>.And<BqlOperand<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.IsEqual<PX.Objects.AR.ARTran.sOShipmentNbr>>>>, FbqlJoins.Left<POReceiptLineReturn>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLineReturn.receiptNbr, Equal<PX.Objects.AR.ARTran.sOShipmentNbr>>>>>.And<BqlOperand<POReceiptLineReturn.lineNbr, IBqlInt>.IsEqual<PX.Objects.AR.ARTran.sOShipmentLineNbr>>>>, FbqlJoins.Left<PX.Objects.PO.POReceiptLine>.On<PX.Objects.PO.POReceiptLine.FK.SOLine>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOLine.orderType>.IsRelatedTo<PX.Objects.SO.SOOrder.orderType>, Field<PX.Objects.SO.SOLine.orderNbr>.IsRelatedTo<PX.Objects.SO.SOOrder.orderNbr>>.WithTablesOf<PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine>, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine>.SameAsCurrent>, And<BqlOperand<PX.Objects.SO.SOLine.operation, IBqlString>.IsEqual<SOOperation.receipt>>>, And<BqlOperand<PX.Objects.SO.SOLine.origShipmentType, IBqlString>.IsEqual<INDocType.dropShip>>>, And<BqlOperand<PX.Objects.SO.SOLine.pOCreate, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PX.Objects.SO.SOLine.pOSource, IBqlString>.IsEqual<INReplenishmentSource.dropShipToOrder>>>, And<BqlOperand<PX.Objects.SO.SOLine.completed, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsNull>>.Order<By<Asc<PX.Objects.PO.POReceipt.receiptNbr>>>>.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) new PX.Objects.SO.SOOrder[1]
    {
      currentDoc
    }, Array.Empty<object>())).AsEnumerable<PXResult<PX.Objects.SO.SOLine>>().Cast<PXResult<PX.Objects.SO.SOLine, PX.Objects.SO.SOOrder, PX.Objects.AR.ARTran, PX.Objects.PO.POReceipt, POReceiptLineReturn>>().ToList<PXResult<PX.Objects.SO.SOLine, PX.Objects.SO.SOOrder, PX.Objects.AR.ARTran, PX.Objects.PO.POReceipt, POReceiptLineReturn>>();
    SO2POSync extension = ((PXGraph) this.Base).GetExtension<SO2POSync>();
    for (int index = 0; index < list.Count; ++index)
    {
      PXResult<PX.Objects.SO.SOLine, PX.Objects.SO.SOOrder, PX.Objects.AR.ARTran, PX.Objects.PO.POReceipt, POReceiptLineReturn> pxResult = list[index];
      PX.Objects.SO.SOLine soLine1 = PXResult<PX.Objects.SO.SOLine, PX.Objects.SO.SOOrder, PX.Objects.AR.ARTran, PX.Objects.PO.POReceipt, POReceiptLineReturn>.op_Implicit(pxResult);
      PX.Objects.SO.SOOrder soOrder = PXResult<PX.Objects.SO.SOLine, PX.Objects.SO.SOOrder, PX.Objects.AR.ARTran, PX.Objects.PO.POReceipt, POReceiptLineReturn>.op_Implicit(pxResult);
      PX.Objects.PO.POReceipt poReceipt1 = PXResult<PX.Objects.SO.SOLine, PX.Objects.SO.SOOrder, PX.Objects.AR.ARTran, PX.Objects.PO.POReceipt, POReceiptLineReturn>.op_Implicit(pxResult);
      POReceiptLineReturn receiptLine = PXResult<PX.Objects.SO.SOLine, PX.Objects.SO.SOOrder, PX.Objects.AR.ARTran, PX.Objects.PO.POReceipt, POReceiptLineReturn>.op_Implicit(pxResult);
      if (string.IsNullOrEmpty(receiptLine.ReceiptNbr))
        throw new PXInvalidOperationException("The {0} line was not found in the originating PO receipt.", new object[1]
        {
          PXForeignSelectorAttribute.GetValueExt<PX.Objects.SO.SOLine.inventoryID>(((PXGraph) this.Base).Caches[typeof (PX.Objects.SO.SOLine)], (object) soLine1)
        });
      if (((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current == null)
      {
        PX.Objects.PO.POReceipt poReceipt2 = ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Insert(new PX.Objects.PO.POReceipt()
        {
          ReceiptType = "RN"
        });
        poReceipt2.ReceiptType = "RN";
        poReceipt2.BranchID = poReceipt1.BranchID;
        poReceipt2.VendorID = poReceipt1.VendorID;
        poReceipt2.VendorLocationID = poReceipt1.VendorLocationID;
        poReceipt2.ProjectID = receiptLine.ProjectID;
        poReceipt2.CuryID = poReceipt1.CuryID;
        poReceipt2.AutoCreateInvoice = new bool?(false);
        poReceipt2.ReturnInventoryCostMode = "O";
        poReceipt2.SOOrderType = soLine1.OrderType;
        poReceipt2.SOOrderNbr = soLine1.OrderNbr;
        poReceipt2.IsIntercompany = poReceipt1.IsIntercompany;
        this.Base.CopyReceiptCurrencyInfoToReturn(poReceipt1.CuryInfoID, (long?) ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current?.CuryInfoID, ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current?.ReturnInventoryCostMode == "O");
      }
      PX.Objects.PO.POReceiptLine poReceiptLine = this.AddReturnLine(receiptLine, soLine1);
      PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = ((PXGraph) this.Base).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo();
      SOLine4 soLine2 = PropertyTransfer.Transfer<PX.Objects.SO.SOLine, SOLine4>(soLine1, new SOLine4());
      Decimal? actualDiscUnitPrice = extension.GetActualDiscUnitPrice(soLine2);
      Decimal? nullable1;
      ref Decimal? local = ref nullable1;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = defaultCurrencyInfo;
      short? lineSign = soLine1.LineSign;
      Decimal? nullable2 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable3 = soLine1.OrderQty;
      Decimal? nullable4 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable5 = actualDiscUnitPrice;
      Decimal? nullable6;
      if (!(nullable4.HasValue & nullable5.HasValue))
      {
        nullable3 = new Decimal?();
        nullable6 = nullable3;
      }
      else
        nullable6 = new Decimal?(nullable4.GetValueOrDefault() * nullable5.GetValueOrDefault());
      nullable3 = nullable6;
      Decimal val = nullable3.Value;
      Decimal num = currencyInfo.RoundBase(val);
      local = new Decimal?(num);
      SO2POSync so2PoSync = extension;
      PX.Objects.SO.SOOrder order = soOrder;
      PX.Objects.PO.POReceiptLine line = poReceiptLine;
      lineSign = soLine1.LineSign;
      Decimal? nullable7;
      if (!lineSign.HasValue)
      {
        nullable3 = new Decimal?();
        nullable7 = nullable3;
      }
      else
        nullable7 = new Decimal?((Decimal) lineSign.GetValueOrDefault());
      nullable4 = nullable7;
      nullable5 = soLine1.BaseOrderQty;
      Decimal? qty;
      if (!(nullable4.HasValue & nullable5.HasValue))
      {
        nullable3 = new Decimal?();
        qty = nullable3;
      }
      else
        qty = new Decimal?(nullable4.GetValueOrDefault() * nullable5.GetValueOrDefault());
      Decimal? amt = nullable1;
      so2PoSync.CreateUpdateOrderShipment(order, line, (POAddress) null, false, qty, amt);
      if (index + 1 >= list.Count || !((PXSelectBase) this.Base.Document).Cache.ObjectsEqual<PX.Objects.PO.POReceipt.receiptNbr>((object) poReceipt1, (object) PXResult<PX.Objects.SO.SOLine, PX.Objects.SO.SOOrder, PX.Objects.AR.ARTran, PX.Objects.PO.POReceipt, POReceiptLineReturn>.op_Implicit(list[index + 1])))
      {
        ((PXAction) this.Base.Save).Press();
        receiptList.Add(((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current);
        ((PXGraph) this.Base).Clear();
      }
    }
  }

  protected virtual PX.Objects.PO.POReceiptLine AddReturnLine(
    POReceiptLineReturn receiptLine,
    PX.Objects.SO.SOLine line)
  {
    bool flag = receiptLine.LotSerialNbrRequiredForDropship.Value;
    PX.Objects.PO.POReceiptLine destLine = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Base.transactions).Insert(new PX.Objects.PO.POReceiptLine()
    {
      IsLSEntryBlocked = new bool?(flag)
    });
    this.Base.CopyFromOrigReceiptLine(destLine, (IPOReturnLineSource) receiptLine, true, new bool?(((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current?.ReturnInventoryCostMode == "O"));
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, receiptLine.InventoryID);
    int num;
    if (inventoryItem != null && inventoryItem.IsConverted.GetValueOrDefault())
    {
      bool? isStockItem = destLine.IsStockItem;
      bool? stkItem = inventoryItem.StkItem;
      num = !(isStockItem.GetValueOrDefault() == stkItem.GetValueOrDefault() & isStockItem.HasValue == stkItem.HasValue) ? 1 : 0;
    }
    else
      num = 0;
    if (num != 0 && EnumerableExtensions.IsIn<string>(destLine.LineType, "GP", "NP"))
      destLine.LineType = inventoryItem.StkItem.GetValueOrDefault() ? "GP" : "NP";
    PX.Objects.PO.POReceiptLine poReceiptLine1 = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Base.transactions).Update(destLine);
    poReceiptLine1.SOOrderType = line.OrderType;
    poReceiptLine1.SOOrderNbr = line.OrderNbr;
    poReceiptLine1.SOOrderLineNbr = line.LineNbr;
    poReceiptLine1.UOM = line.UOM;
    PX.Objects.PO.POReceiptLine poReceiptLine2 = poReceiptLine1;
    short? lineSign1 = line.LineSign;
    Decimal? nullable1 = lineSign1.HasValue ? new Decimal?((Decimal) lineSign1.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable2 = line.OrderQty;
    Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
    poReceiptLine2.ReceiptQty = nullable3;
    PX.Objects.PO.POReceiptLine poReceiptLine3 = poReceiptLine1;
    short? lineSign2 = line.LineSign;
    nullable2 = lineSign2.HasValue ? new Decimal?((Decimal) lineSign2.GetValueOrDefault()) : new Decimal?();
    Decimal? baseOrderQty = line.BaseOrderQty;
    Decimal? nullable4 = nullable2.HasValue & baseOrderQty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * baseOrderQty.GetValueOrDefault()) : new Decimal?();
    poReceiptLine3.BaseReceiptQty = nullable4;
    PX.Objects.PO.POReceiptLine poReceiptLine4 = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Base.transactions).Update(poReceiptLine1);
    if (flag)
    {
      poReceiptLine4.IsLSEntryBlocked = new bool?(false);
      poReceiptLine4 = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Base.transactions).Update(poReceiptLine4);
      if (receiptLine.LocationID.HasValue)
        ((PXSelectBase) this.Base.transactions).Cache.SetValueExt<PX.Objects.PO.POReceiptLine.locationID>((object) poReceiptLine4, (object) receiptLine.LocationID);
      else
        ((PXSelectBase) this.Base.transactions).Cache.SetDefaultExt<PX.Objects.PO.POReceiptLine.locationID>((object) poReceiptLine4);
      POReceiptEntry poReceiptEntry = this.Base;
      object[] objArray1 = (object[]) new PX.Objects.SO.SOLine[1]{ line };
      object[] objArray2 = Array.Empty<object>();
      foreach (PX.Objects.SO.SOLineSplit soLineSplit in GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOLineSplit.orderType>.IsRelatedTo<PX.Objects.SO.SOLine.orderType>, Field<PX.Objects.SO.SOLineSplit.orderNbr>.IsRelatedTo<PX.Objects.SO.SOLine.orderNbr>, Field<PX.Objects.SO.SOLineSplit.lineNbr>.IsRelatedTo<PX.Objects.SO.SOLine.lineNbr>>.WithTablesOf<PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>, PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>.SameAsCurrent>>.Config>.SelectMultiBound((PXGraph) poReceiptEntry, objArray1, objArray2)).ToList<PX.Objects.SO.SOLineSplit>())
      {
        POReceiptLineSplit copy = PXCache<POReceiptLineSplit>.CreateCopy(((PXSelectBase<POReceiptLineSplit>) this.Base.splits).Insert(new POReceiptLineSplit()
        {
          InventoryID = soLineSplit.InventoryID,
          SubItemID = soLineSplit.SubItemID,
          LotSerialNbr = soLineSplit.LotSerialNbr,
          ExpireDate = soLineSplit.ExpireDate
        }));
        copy.Qty = soLineSplit.Qty;
        ((PXSelectBase<POReceiptLineSplit>) this.Base.splits).Update(copy);
      }
    }
    if (!string.IsNullOrEmpty(poReceiptLine4.POType) && !string.IsNullOrEmpty(poReceiptLine4.PONbr))
      this.Base.AddPOOrderReceipt(poReceiptLine4.POType, poReceiptLine4.PONbr);
    return poReceiptLine4;
  }
}

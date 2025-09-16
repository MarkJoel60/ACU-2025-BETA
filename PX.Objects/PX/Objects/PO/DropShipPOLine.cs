// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.DropShipPOLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common.DAC;
using PX.Objects.IN;
using System;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("PO Drop-Ship Line")]
[PXProjection(typeof (Select2<POLine, LeftJoin<DropShipLink, On<DropShipLink.FK.POLine>>, Where<POLine.orderType, Equal<POOrderType.dropShip>, And<POLine.lineType, In3<POLineType.goodsForDropShip, POLineType.nonStockForDropShip>>>>))]
public class DropShipPOLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (POLine.orderType))]
  [PXUIField(DisplayName = "Order Type")]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (POLine.orderNbr))]
  [PXSelector(typeof (Search<POOrder.orderNbr, Where<POOrder.orderType, Equal<Current<DropShipPOLine.orderType>>>>))]
  [PXUIField(DisplayName = "Order Nbr.")]
  public virtual string OrderNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (POLine.lineNbr))]
  [PXUIField(DisplayName = "Line Nbr.")]
  public virtual int? LineNbr { get; set; }

  [POLineInventoryItem(Filterable = true, BqlField = typeof (POLine.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (POLine.tranDesc))]
  [PXUIField(DisplayName = "Description")]
  public virtual string TranDesc { get; set; }

  [INUnit(typeof (DropShipPOLine.inventoryID), DisplayName = "UOM", BqlField = typeof (POLine.uOM))]
  public virtual string UOM { get; set; }

  [PXDBQuantity(BqlField = typeof (POLine.orderQty))]
  [PXUIField(DisplayName = "Not Linked Qty.")]
  public virtual Decimal? OrderQty { get; set; }

  [PXNote(BqlField = typeof (POLine.noteID))]
  public virtual Guid? NoteID { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (DropShipLink.sOOrderType))]
  public virtual string SOOrderType { get; set; }

  [PXUIField(DisplayName = "Sales Order Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<DropShipPOLine.sOOrderType>>>>))]
  [PXDBString(15, IsUnicode = true, BqlField = typeof (DropShipLink.sOOrderNbr))]
  public virtual string SOOrderNbr { get; set; }

  [PXUIField(DisplayName = "Sales Order Line Nbr.")]
  [PXDBInt(BqlField = typeof (DropShipLink.sOLineNbr))]
  public virtual int? SOLineNbr { get; set; }

  [PXDBBool(BqlField = typeof (DropShipLink.active))]
  [PXUIField(DisplayName = "SO Linked")]
  public virtual bool? SOLinkActive { get; set; }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DropShipPOLine.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DropShipPOLine.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DropShipPOLine.lineNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DropShipPOLine.inventoryID>
  {
    public class PreventEditIfPOReceiptLineExist : 
      EditPreventor<TypeArrayOf<IBqlField>.FilledWith<PX.Objects.IN.InventoryItem.lotSerClassID>>.On<InventoryItemMaint>.IfExists<Select<POReceiptLine, Where<POReceiptLine.inventoryID, Equal<Current<PX.Objects.IN.InventoryItem.inventoryID>>, And<POReceiptLine.pOType, Equal<POOrderType.dropShip>, And<POReceiptLine.iNReleased, Equal<False>, And<POReceiptLine.lotSerialNbrRequiredForDropship, Equal<True>>>>>>>
    {
      protected virtual void OnPreventEdit(GetEditPreventingReasonArgs args)
      {
        if (((PX.Objects.IN.InventoryItem) args.Row).OrigLotSerClassID != null)
          return;
        ((CancelEventArgs) args).Cancel = true;
      }

      public static bool IsActive() => true;

      protected virtual string CreateEditPreventingReason(
        GetEditPreventingReasonArgs arg,
        object firstPreventingEntity,
        string fieldName,
        string currentTableName,
        string foreignTableName)
      {
        POReceiptLine poReceiptLine = (POReceiptLine) firstPreventingEntity;
        DropShipLink dropShipLink = PXResultset<DropShipLink>.op_Implicit(PXSelectBase<DropShipLink, PXSelect<DropShipLink, Where<DropShipLink.pOOrderType, Equal<Required<POLine.orderType>>, And<DropShipLink.pOOrderNbr, Equal<Required<POLine.orderNbr>>, And<DropShipLink.pOLineNbr, Equal<Required<POLine.lineNbr>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<InventoryItemMaint>) this).Base, new object[3]
        {
          (object) poReceiptLine.POType,
          (object) poReceiptLine.PONbr,
          (object) poReceiptLine.POLineNbr
        }));
        return PXMessages.LocalizeFormat("The lot/serial class of the {0} item cannot be changed because this item is included in the {1} {2} document that has not been processed completely.", new object[3]
        {
          (object) PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<InventoryItemMaint>) this).Base, poReceiptLine.InventoryID).InventoryCD,
          (object) (poReceiptLine.ReceiptType == "RN" ? poReceiptLine.SOOrderNbr : dropShipLink?.SOOrderNbr),
          (object) "Sales Order"
        });
      }
    }
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DropShipPOLine.tranDesc>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DropShipPOLine.uOM>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DropShipPOLine.orderQty>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  DropShipPOLine.noteID>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DropShipPOLine.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DropShipPOLine.sOOrderNbr>
  {
  }

  public abstract class sOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DropShipPOLine.sOLineNbr>
  {
  }

  public abstract class sOLinkActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DropShipPOLine.sOLinkActive>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptReturnFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[Serializable]
public class POReceiptReturnFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsFixed = true)]
  [POOrderType.RegularDropShipList]
  [PXUIField(DisplayName = "Order Type")]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Order Nbr.")]
  [PX.Objects.PO.PO.RefNbr(typeof (Search2<POOrder.orderNbr, CrossJoin<APSetup>, Where2<Where<Current<POReceiptReturnFilter.orderType>, IsNull, Or<POOrder.orderType, Equal<Current<POReceiptReturnFilter.orderType>>>>, And<POOrder.curyID, Equal<Current<POReceipt.curyID>>, And<POOrder.hold, Equal<boolFalse>, And2<Where<Current<POReceipt.vendorID>, IsNull, Or<POOrder.vendorID, Equal<Current<POReceipt.vendorID>>>>, And<Where<Current<POReceipt.vendorLocationID>, IsNull, Or<POOrder.vendorLocationID, Equal<Current<POReceipt.vendorLocationID>>>>>>>>>, OrderBy<Asc<POOrder.orderType, Desc<POOrder.orderNbr>>>>), Filterable = true)]
  public virtual string OrderNbr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [POReceiptType.RefNbr(typeof (Search<POReceipt.receiptNbr, Where<POReceipt.receiptType, Equal<POReceiptType.poreceipt>, And<POReceipt.released, Equal<True>>>, OrderBy<Desc<POReceipt.receiptNbr>>>), Filterable = true)]
  [PXUIField(DisplayName = "Receipt Nbr.")]
  public virtual string ReceiptNbr { get; set; }

  [Inventory]
  public virtual int? InventoryID { get; set; }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptReturnFilter.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptReturnFilter.orderNbr>
  {
  }

  public abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptReturnFilter.receiptNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptReturnFilter.inventoryID>
  {
  }
}

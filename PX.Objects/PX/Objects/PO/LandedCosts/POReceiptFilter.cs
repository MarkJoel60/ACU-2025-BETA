// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCosts.POReceiptFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.AP;
using PX.Objects.IN;

#nullable enable
namespace PX.Objects.PO.LandedCosts;

public class POReceiptFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [VendorActive]
  public virtual int? VendorID { get; set; }

  [PXString(2, IsFixed = true, InputMask = "")]
  [PXDefault("RT")]
  [PXStringList(new string[] {"RT", "RX"}, new string[] {"Receipt", "Transfer Receipt"})]
  [PXUIField(DisplayName = "Type")]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string ReceiptType { get; set; }

  [PXString(15, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [POReceiptType.RefNbr(typeof (Search2<PX.Objects.PO.POReceipt.receiptNbr, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.PO.POReceipt.vendorID>>>, Where<PX.Objects.PO.POReceipt.receiptType, Equal<Optional<POReceiptFilter.receiptType>>, And2<Where<Current<POReceiptFilter.vendorID>, IsNull, Or<PX.Objects.PO.POReceipt.vendorID, Equal<Current<POReceiptFilter.vendorID>>>>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>>, OrderBy<Desc<PX.Objects.PO.POReceipt.receiptNbr>>>), Filterable = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string ReceiptNbr { get; set; }

  [PXString(2, IsFixed = true)]
  [POOrderType.List]
  [PXUIField(DisplayName = "Type")]
  public virtual string OrderType { get; set; }

  [PXString(15, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PX.Objects.PO.PO.RefNbr(typeof (Search2<PX.Objects.PO.POOrder.orderNbr, CrossJoin<APSetup, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.PO.POOrder.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>>, Where<Current<POReceiptFilter.orderType>, IsNull, Or<PX.Objects.PO.POOrder.orderType, Equal<Current<POReceiptFilter.orderType>>, And<Where<Current<POReceiptFilter.vendorID>, IsNull, Or<PX.Objects.PO.POOrder.vendorID, Equal<Current<POReceiptFilter.vendorID>>>>>>>>), Filterable = true)]
  public virtual string OrderNbr { get; set; }

  [Inventory]
  public virtual int? InventoryID { get; set; }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptFilter.vendorID>
  {
  }

  public abstract class receiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptFilter.receiptType>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptFilter.receiptNbr>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptFilter.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptFilter.orderNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptFilter.inventoryID>
  {
  }
}

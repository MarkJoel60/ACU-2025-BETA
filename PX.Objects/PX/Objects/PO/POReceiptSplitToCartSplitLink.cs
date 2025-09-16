// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptSplitToCartSplitLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName]
public class POReceiptSplitToCartSplitLink : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string ReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  public virtual string ReceiptNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? ReceiptLineNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXParent(typeof (POReceiptSplitToCartSplitLink.FK.ReceiptLineSplit))]
  public virtual int? ReceiptSplitLineNbr { get; set; }

  [Site(IsKey = true, Visible = false)]
  [PXParent(typeof (POReceiptSplitToCartSplitLink.FK.Site))]
  public int? SiteID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXSelector(typeof (Search<INCart.cartID, Where<INCart.active, Equal<True>>>), SubstituteKey = typeof (INCart.cartCD), DescriptionField = typeof (INCart.descr))]
  [PXParent(typeof (POReceiptSplitToCartSplitLink.FK.Cart))]
  public int? CartID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXParent(typeof (POReceiptSplitToCartSplitLink.FK.CartSplit))]
  public virtual int? CartSplitLineNbr { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity", Enabled = false)]
  public virtual Decimal? Qty { get; set; }

  public class PK : 
    PrimaryKeyOf<POReceiptSplitToCartSplitLink>.By<POReceiptSplitToCartSplitLink.receiptType, POReceiptSplitToCartSplitLink.receiptNbr, POReceiptSplitToCartSplitLink.receiptLineNbr, POReceiptSplitToCartSplitLink.receiptSplitLineNbr, POReceiptSplitToCartSplitLink.siteID, POReceiptSplitToCartSplitLink.cartID, POReceiptSplitToCartSplitLink.cartSplitLineNbr>
  {
    public static POReceiptSplitToCartSplitLink Find(
      PXGraph graph,
      string receiptType,
      string receiptNbr,
      int? receiptLineNbr,
      int? receiptSplitLineNbr,
      int? siteID,
      int? cartID,
      int? cartSplitLineNbr,
      PKFindOptions options = 0)
    {
      return (POReceiptSplitToCartSplitLink) PrimaryKeyOf<POReceiptSplitToCartSplitLink>.By<POReceiptSplitToCartSplitLink.receiptType, POReceiptSplitToCartSplitLink.receiptNbr, POReceiptSplitToCartSplitLink.receiptLineNbr, POReceiptSplitToCartSplitLink.receiptSplitLineNbr, POReceiptSplitToCartSplitLink.siteID, POReceiptSplitToCartSplitLink.cartID, POReceiptSplitToCartSplitLink.cartSplitLineNbr>.FindBy(graph, (object) receiptType, (object) receiptNbr, (object) receiptLineNbr, (object) receiptSplitLineNbr, (object) siteID, (object) cartID, (object) cartSplitLineNbr, options);
    }
  }

  public static class FK
  {
    public class Receipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<POReceiptSplitToCartSplitLink>.By<POReceiptSplitToCartSplitLink.receiptType, POReceiptSplitToCartSplitLink.receiptNbr>
    {
    }

    public class ReceiptLine : 
      PrimaryKeyOf<POReceiptLine>.By<POReceiptLine.receiptType, POReceiptLine.receiptNbr, POReceiptLine.lineNbr>.ForeignKeyOf<POReceiptSplitToCartSplitLink>.By<POReceiptSplitToCartSplitLink.receiptType, POReceiptSplitToCartSplitLink.receiptNbr, POReceiptSplitToCartSplitLink.receiptLineNbr>
    {
    }

    public class ReceiptLineSplit : 
      PrimaryKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.receiptType, POReceiptLineSplit.receiptNbr, POReceiptLineSplit.lineNbr, POReceiptLineSplit.splitLineNbr>.ForeignKeyOf<POReceiptSplitToCartSplitLink>.By<POReceiptSplitToCartSplitLink.receiptType, POReceiptSplitToCartSplitLink.receiptNbr, POReceiptSplitToCartSplitLink.receiptLineNbr, POReceiptSplitToCartSplitLink.receiptSplitLineNbr>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<POReceiptSplitToCartSplitLink>.By<POReceiptSplitToCartSplitLink.siteID>
    {
    }

    public class Cart : 
      PrimaryKeyOf<INCart>.By<INCart.siteID, INCart.cartID>.ForeignKeyOf<POReceiptSplitToCartSplitLink>.By<POReceiptSplitToCartSplitLink.siteID, POReceiptSplitToCartSplitLink.cartID>
    {
    }

    public class CartSplit : 
      PrimaryKeyOf<INCartSplit>.By<INCartSplit.siteID, INCartSplit.cartID, INCartSplit.splitLineNbr>.ForeignKeyOf<POReceiptSplitToCartSplitLink>.By<POReceiptSplitToCartSplitLink.siteID, POReceiptSplitToCartSplitLink.cartID, POReceiptSplitToCartSplitLink.cartSplitLineNbr>
    {
    }
  }

  public abstract class receiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptSplitToCartSplitLink.receiptType>
  {
  }

  public abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptSplitToCartSplitLink.receiptNbr>
  {
  }

  public abstract class receiptLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptSplitToCartSplitLink.receiptLineNbr>
  {
  }

  public abstract class receiptSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptSplitToCartSplitLink.receiptSplitLineNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptSplitToCartSplitLink.siteID>
  {
  }

  public abstract class cartID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptSplitToCartSplitLink.cartID>
  {
  }

  public abstract class cartSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptSplitToCartSplitLink.cartSplitLineNbr>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptSplitToCartSplitLink.qty>
  {
  }
}

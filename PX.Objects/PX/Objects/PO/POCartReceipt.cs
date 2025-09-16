// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POCartReceipt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName]
public class POCartReceipt : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Site(IsKey = true, Visible = false)]
  [PXParent(typeof (POCartReceipt.FK.Site))]
  public int? SiteID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXSelector(typeof (Search<INCart.cartID, Where<INCart.active, Equal<True>>>), SubstituteKey = typeof (INCart.cartCD), DescriptionField = typeof (INCart.descr))]
  [PXParent(typeof (POCartReceipt.FK.Cart))]
  public int? CartID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string ReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  [PXParent(typeof (POCartReceipt.FK.Receipt))]
  public virtual string ReceiptNbr { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXParent(typeof (Select<PX.Objects.IN.INRegister, Where<PX.Objects.IN.INRegister.refNbr, Equal<Current<POCartReceipt.transferNbr>>, And<PX.Objects.IN.INRegister.docType, Equal<INDocType.transfer>>>>))]
  public virtual string TransferNbr { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<POCartReceipt>.By<POCartReceipt.siteID, POCartReceipt.cartID>
  {
    public static POCartReceipt Find(
      PXGraph graph,
      int? siteID,
      int? cartID,
      PKFindOptions options = 0)
    {
      return (POCartReceipt) PrimaryKeyOf<POCartReceipt>.By<POCartReceipt.siteID, POCartReceipt.cartID>.FindBy(graph, (object) siteID, (object) cartID, options);
    }
  }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<POCartReceipt>.By<POCartReceipt.siteID>
    {
    }

    public class Cart : 
      PrimaryKeyOf<INCart>.By<INCart.siteID, INCart.cartID>.ForeignKeyOf<POCartReceipt>.By<POCartReceipt.siteID, POCartReceipt.cartID>
    {
    }

    public class Receipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<POCartReceipt>.By<POCartReceipt.receiptType, POCartReceipt.receiptNbr>
    {
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POCartReceipt.siteID>
  {
  }

  public abstract class cartID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POCartReceipt.cartID>
  {
  }

  public abstract class receiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POCartReceipt.receiptType>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POCartReceipt.receiptNbr>
  {
  }

  public abstract class transferNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POCartReceipt.transferNbr>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POCartReceipt.Tstamp>
  {
  }
}

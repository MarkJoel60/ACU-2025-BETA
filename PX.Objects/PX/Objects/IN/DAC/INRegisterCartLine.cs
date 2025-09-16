// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.INRegisterCartLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN.DAC;

[PXCacheName]
public class INRegisterCartLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Site(IsKey = true, Visible = false)]
  [PXDefault(typeof (INCart.siteID))]
  [PXParent(typeof (INRegisterCartLine.FK.Site))]
  public int? SiteID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXSelector(typeof (Search<INCart.cartID, Where<INCart.active, Equal<True>>>), SubstituteKey = typeof (INCart.cartCD), DescriptionField = typeof (INCart.descr))]
  [PXDefault(typeof (INCart.cartID))]
  [PXParent(typeof (INRegisterCartLine.FK.Cart))]
  public int? CartID { get; set; }

  [PXUIField(DisplayName = "Document Type")]
  [PXDBString(1, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (PX.Objects.IN.INRegister.docType))]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXDBDefault(typeof (PX.Objects.IN.INRegister.refNbr))]
  [PXParent(typeof (INRegisterCartLine.FK.Register))]
  [PXUIField(DisplayName = "Reference Nbr.")]
  public virtual string RefNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXForeignReference(typeof (INRegisterCartLine.FK.CartSplit))]
  public virtual int? CartSplitLineNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (INTran.lineNbr))]
  [PXForeignReference(typeof (INRegisterCartLine.FK.Tran))]
  public virtual int? LineNbr { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity", Enabled = false)]
  public virtual Decimal? Qty { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<INRegisterCartLine>.By<INRegisterCartLine.siteID, INRegisterCartLine.cartID, INRegisterCartLine.docType, INRegisterCartLine.refNbr, INRegisterCartLine.lineNbr>
  {
  }

  public static class FK
  {
    public class RegisterCart : 
      PrimaryKeyOf<INRegisterCart>.By<INRegisterCart.siteID, INRegisterCart.cartID, INRegisterCart.docType, INRegisterCart.refNbr>.ForeignKeyOf<INRegisterCartLine>.By<INRegisterCartLine.siteID, INRegisterCartLine.cartID, INRegisterCartLine.docType, INRegisterCartLine.refNbr>
    {
    }

    public class Register : 
      PrimaryKeyOf<PX.Objects.IN.INRegister>.By<PX.Objects.IN.INRegister.docType, PX.Objects.IN.INRegister.refNbr>.ForeignKeyOf<INRegisterCartLine>.By<INRegisterCartLine.docType, INRegisterCartLine.refNbr>
    {
    }

    public class Tran : 
      PrimaryKeyOf<INTran>.By<INTran.docType, INTran.refNbr, INTran.lineNbr>.ForeignKeyOf<INRegisterCartLine>.By<INRegisterCartLine.docType, INRegisterCartLine.refNbr, INRegisterCartLine.lineNbr>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INRegisterCartLine>.By<INRegisterCartLine.siteID>
    {
    }

    public class Cart : 
      PrimaryKeyOf<INCart>.By<INCart.siteID, INCart.cartID>.ForeignKeyOf<INRegisterCartLine>.By<INRegisterCartLine.siteID, INRegisterCartLine.cartID>
    {
    }

    public class CartSplit : 
      PrimaryKeyOf<INCartSplit>.By<INCartSplit.siteID, INCartSplit.cartID, INCartSplit.splitLineNbr>.ForeignKeyOf<INRegisterCartLine>.By<INRegisterCartLine.siteID, INRegisterCartLine.cartID, INRegisterCartLine.cartSplitLineNbr>
    {
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INRegisterCartLine.siteID>
  {
  }

  public abstract class cartID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INRegisterCartLine.cartID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegisterCartLine.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegisterCartLine.refNbr>
  {
  }

  public abstract class cartSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INRegisterCartLine.cartSplitLineNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INRegisterCartLine.lineNbr>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INRegisterCartLine.qty>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INRegisterCartLine.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INRegisterCartLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRegisterCartLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INRegisterCartLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INRegisterCartLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRegisterCartLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INRegisterCartLine.lastModifiedDateTime>
  {
  }
}

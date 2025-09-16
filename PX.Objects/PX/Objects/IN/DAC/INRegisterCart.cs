// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.INRegisterCart
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
public class INRegisterCart : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Site(IsKey = true, Visible = false)]
  [PXDefault(typeof (INCart.siteID))]
  [PXParent(typeof (INRegisterCart.FK.Site))]
  public int? SiteID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXSelector(typeof (Search<INCart.cartID, Where<INCart.active, Equal<True>>>), SubstituteKey = typeof (INCart.cartCD), DescriptionField = typeof (INCart.descr))]
  [PXDefault(typeof (INCart.cartID))]
  [PXParent(typeof (INRegisterCart.FK.Cart))]
  public int? CartID { get; set; }

  [PXUIField(DisplayName = "Document Type")]
  [PXDBString(1, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (PX.Objects.IN.INRegister.docType))]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXDBDefault(typeof (PX.Objects.IN.INRegister.refNbr))]
  [PXParent(typeof (INRegisterCart.FK.Register))]
  [PXUIField(DisplayName = "Reference Nbr.")]
  public virtual string RefNbr { get; set; }

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
    PrimaryKeyOf<INRegisterCart>.By<INRegisterCart.siteID, INRegisterCart.cartID, INRegisterCart.docType, INRegisterCart.refNbr>
  {
    public static INRegisterCart Find(
      PXGraph graph,
      int? siteID,
      int? cartID,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (INRegisterCart) PrimaryKeyOf<INRegisterCart>.By<INRegisterCart.siteID, INRegisterCart.cartID, INRegisterCart.docType, INRegisterCart.refNbr>.FindBy(graph, (object) siteID, (object) cartID, (object) docType, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INRegisterCart>.By<INRegisterCart.siteID>
    {
    }

    public class Cart : 
      PrimaryKeyOf<INCart>.By<INCart.siteID, INCart.cartID>.ForeignKeyOf<INRegisterCart>.By<INRegisterCart.siteID, INRegisterCart.cartID>
    {
    }

    public class Register : 
      PrimaryKeyOf<PX.Objects.IN.INRegister>.By<PX.Objects.IN.INRegister.docType, PX.Objects.IN.INRegister.refNbr>.ForeignKeyOf<INRegisterCart>.By<INRegisterCart.docType, INRegisterCart.refNbr>
    {
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INRegisterCart.siteID>
  {
  }

  public abstract class cartID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INRegisterCart.cartID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegisterCart.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegisterCart.refNbr>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INRegisterCart.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INRegisterCart.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRegisterCart.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INRegisterCart.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INRegisterCart.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRegisterCart.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INRegisterCart.lastModifiedDateTime>
  {
  }
}

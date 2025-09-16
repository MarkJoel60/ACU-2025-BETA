// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INReplenishmentOrder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("Replenishment Order")]
[Serializable]
public class INReplenishmentOrder : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _RefNbr;
  protected DateTime? _OrderDate;
  protected int? _SiteID;
  protected int? _LineCntr;
  protected int? _VendorID;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(15, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC", IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<INReplenishmentOrder.refNbr>), Filterable = true)]
  [AutoNumber(typeof (INSetup.replenishmentNumberingID), typeof (INReplenishmentOrder.orderDate))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Order Date")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? OrderDate
  {
    get => this._OrderDate;
    set => this._OrderDate = value;
  }

  [Site(DisplayName = "Warehouse")]
  [PXForeignReference(typeof (INReplenishmentOrder.FK.Site))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  [VendorNonEmployeeActive]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<INReplenishmentOrder>.By<INReplenishmentOrder.refNbr>
  {
    public static INReplenishmentOrder Find(PXGraph graph, string refNbr, PKFindOptions options = 0)
    {
      return (INReplenishmentOrder) PrimaryKeyOf<INReplenishmentOrder>.By<INReplenishmentOrder.refNbr>.FindBy(graph, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INReplenishmentOrder>.By<INReplenishmentOrder.siteID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<INReplenishmentOrder>.By<INReplenishmentOrder.vendorID>
    {
    }
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INReplenishmentOrder.refNbr>
  {
  }

  public abstract class orderDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INReplenishmentOrder.orderDate>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReplenishmentOrder.siteID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReplenishmentOrder.lineCntr>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReplenishmentOrder.vendorID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INReplenishmentOrder.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INReplenishmentOrder.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentOrder.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INReplenishmentOrder.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INReplenishmentOrder.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentOrder.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INReplenishmentOrder.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INReplenishmentOrder.Tstamp>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTransitLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXPrimaryGraph(typeof (INTransferEntry))]
[PXCacheName("Transfer Line")]
[Serializable]
public class INTransitLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _CostSiteID;
  protected 
  #nullable disable
  string _TransferNbr;
  protected int? _TransferLineNbr;
  protected string _SOOrderType;
  protected string _SOOrderNbr;
  protected int? _SOOrderLineNbr;
  protected string _SOShipmentType;
  protected string _SOShipmentNbr;
  protected int? _SOShipmentLineNbr;
  protected int? _SiteID;
  protected int? _ToSiteID;
  protected string _OrigModule;
  protected int? _ToLocationID;
  protected Guid? _NoteID;
  protected Guid? _RefNoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBForeignIdentity(typeof (INCostSite))]
  public virtual int? CostSiteID
  {
    get => this._CostSiteID;
    set => this._CostSiteID = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXFieldDescription]
  public virtual string TransferNbr
  {
    get => this._TransferNbr;
    set => this._TransferNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXFieldDescription]
  public virtual int? TransferLineNbr
  {
    get => this._TransferLineNbr;
    set => this._TransferLineNbr = value;
  }

  [PXDBString(2, IsFixed = true)]
  public virtual string SOOrderType
  {
    get => this._SOOrderType;
    set => this._SOOrderType = value;
  }

  [PXDBString(15, InputMask = "", IsUnicode = true)]
  public virtual string SOOrderNbr
  {
    get => this._SOOrderNbr;
    set => this._SOOrderNbr = value;
  }

  [PXDBInt]
  public virtual int? SOOrderLineNbr
  {
    get => this._SOOrderLineNbr;
    set => this._SOOrderLineNbr = value;
  }

  [PXDBString(1, IsFixed = true)]
  public virtual string SOShipmentType
  {
    get => this._SOShipmentType;
    set => this._SOShipmentType = value;
  }

  [PXDBString(15, InputMask = "", IsUnicode = true)]
  public virtual string SOShipmentNbr
  {
    get => this._SOShipmentNbr;
    set => this._SOShipmentNbr = value;
  }

  [PXDBInt]
  public virtual int? SOShipmentLineNbr
  {
    get => this._SOShipmentLineNbr;
    set => this._SOShipmentLineNbr = value;
  }

  [Site(DisplayName = "Warehouse ID", DescriptionField = typeof (INSite.descr))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [ToSite(DisplayName = "To Warehouse ID", DescriptionField = typeof (INSite.descr))]
  public virtual int? ToSiteID
  {
    get => this._ToSiteID;
    set => this._ToSiteID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("IN")]
  [INTransitLine.origModule.List]
  public virtual string OrigModule
  {
    get => this._OrigModule;
    set => this._OrigModule = value;
  }

  [Location(typeof (INTransitLine.toSiteID), DisplayName = "To Location ID")]
  public virtual int? ToLocationID
  {
    get => this._ToLocationID;
    set => this._ToLocationID = value;
  }

  /// <summary>
  /// Denormalization of <see cref="P:PX.Objects.IN.INTranSplit.LotSerialNbr" />
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsLotSerial { get; set; }

  [PXNote(DescriptionField = typeof (INTransitLine.transferNbr), Selector = typeof (INTransitLine.transferNbr))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBGuid(false)]
  public virtual Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  /// <summary>
  /// Denormalization of <see cref="P:PX.Objects.IN.INTranSplit.IsFixedInTransit" />
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsFixedInTransit { get; set; }

  /// <summary>The date of the transaction.</summary>
  [PXDBDate]
  [PXDefault]
  public virtual DateTime? TranDate { get; set; }

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

  public class PK : PrimaryKeyOf<INTransitLine>.By<INTransitLine.costSiteID>
  {
    public static INTransitLine Find(PXGraph graph, int? costSiteID, PKFindOptions options = 0)
    {
      return (INTransitLine) PrimaryKeyOf<INTransitLine>.By<INTransitLine.costSiteID>.FindBy(graph, (object) costSiteID, options);
    }
  }

  public static class FK
  {
    public class CostSite : 
      PrimaryKeyOf<INCostSite>.By<INCostSite.costSiteID>.ForeignKeyOf<INTransitLine>.By<INTransitLine.costSiteID>
    {
    }

    public class ToSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INTransitLine>.By<INTransitLine.toSiteID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INTransitLine>.By<INTransitLine.siteID>
    {
    }

    public class ToLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INTransitLine>.By<INTransitLine.toLocationID>
    {
    }

    public class SOOrderType : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<INTransitLine>.By<INTransitLine.sOOrderType>
    {
    }

    public class SOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrder>.By<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>.ForeignKeyOf<INTransitLine>.By<INTransitLine.sOOrderType, INTransitLine.sOOrderNbr>
    {
    }

    public class SOLine : 
      PrimaryKeyOf<PX.Objects.SO.SOLine>.By<PX.Objects.SO.SOLine.orderType, PX.Objects.SO.SOLine.orderNbr, PX.Objects.SO.SOLine.lineNbr>.ForeignKeyOf<INTransitLine>.By<INTransitLine.sOOrderType, INTransitLine.sOOrderNbr, INTransitLine.sOOrderLineNbr>
    {
    }

    public class SOShipment : 
      PrimaryKeyOf<PX.Objects.SO.SOShipment>.By<PX.Objects.SO.SOShipment.shipmentType, PX.Objects.SO.SOShipment.shipmentNbr>.ForeignKeyOf<INTransitLine>.By<INTransitLine.sOShipmentType, INTransitLine.sOShipmentNbr>
    {
    }

    public class SOShipmentLine : 
      PrimaryKeyOf<SOShipLine>.By<SOShipLine.shipmentType, SOShipLine.shipmentNbr, SOShipLine.lineNbr>.ForeignKeyOf<INTransitLine>.By<INTransitLine.sOShipmentType, INTransitLine.sOShipmentNbr, INTransitLine.sOShipmentLineNbr>
    {
    }
  }

  public abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransitLine.costSiteID>
  {
  }

  public abstract class transferNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTransitLine.transferNbr>
  {
  }

  public abstract class transferLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransitLine.transferLineNbr>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTransitLine.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTransitLine.sOOrderNbr>
  {
  }

  public abstract class sOOrderLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransitLine.sOOrderLineNbr>
  {
  }

  public abstract class sOShipmentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransitLine.sOShipmentType>
  {
  }

  public abstract class sOShipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransitLine.sOShipmentNbr>
  {
  }

  public abstract class sOShipmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTransitLine.sOShipmentLineNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransitLine.siteID>
  {
  }

  public abstract class toSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransitLine.toSiteID>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTransitLine.origModule>
  {
    public const string PI = "PI";

    public class List : PXStringListAttribute
    {
      public List()
        : base(new Tuple<string, string>[5]
        {
          PXStringListAttribute.Pair("SO", "SO"),
          PXStringListAttribute.Pair("PO", "PO"),
          PXStringListAttribute.Pair("IN", "IN"),
          PXStringListAttribute.Pair("PI", "PI"),
          PXStringListAttribute.Pair("AP", "AP")
        })
      {
      }
    }
  }

  public abstract class toLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransitLine.toLocationID>
  {
  }

  public abstract class isLotSerial : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTransitLine.isLotSerial>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTransitLine.noteID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTransitLine.refNoteID>
  {
  }

  public abstract class isFixedInTransit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INTransitLine.isFixedInTransit>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INTransitLine.tranDate>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTransitLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransitLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTransitLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INTransitLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransitLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTransitLine.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INTransitLine.Tstamp>
  {
  }
}

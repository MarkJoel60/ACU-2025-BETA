// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPackageDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("SO Package Detail")]
[Serializable]
public class SOPackageDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ShipmentNbr;
  protected int? _LineNbr;
  protected string _BoxID;
  protected Decimal? _Weight;
  protected string _WeightUOM;
  protected int? _InventoryID;
  protected string _Description;
  protected Decimal? _Qty;
  protected string _QtyUOM;
  protected string _TrackNumber;
  protected string _TrackUrl;
  protected string _TrackData;
  protected Decimal? _DeclaredValue;
  protected Decimal? _COD;
  protected bool? _Confirmed;
  protected string _CustomRefNbr1;
  protected string _CustomRefNbr2;
  protected string _PackageType;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXParent(typeof (SOPackageDetail.FK.Shipment))]
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDBDefault(typeof (SOShipment.shipmentNbr))]
  [PXUIField]
  public virtual string ShipmentNbr
  {
    get => this._ShipmentNbr;
    set => this._ShipmentNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (SOShipment.packageLineCntr))]
  [PXFormula(null, typeof (CountCalc<SOShipment.packageCount>))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  [PXSelector(typeof (Search5<PX.Objects.CS.CSBox.boxID, LeftJoin<CarrierPackage, On<PX.Objects.CS.CSBox.boxID, Equal<CarrierPackage.boxID>>>, Where<Current<SOShipment.shipVia>, IsNull, Or<Where<Current<SOShipment.shipVia>, IsNotNull, And<Where<CarrierPackage.carrierID, Equal<Current<SOShipment.shipVia>>, Or<PX.Objects.CS.CSBox.activeByDefault, Equal<True>>>>>>>, Aggregate<GroupBy<PX.Objects.CS.CSBox.boxID>>>))]
  [PXUIField(DisplayName = "Box ID")]
  public virtual string BoxID
  {
    get => this._BoxID;
    set => this._BoxID = value;
  }

  /// <summary>
  /// Gross (Brutto) Weight. Weight of a box with contents. (includes weight of the box itself).
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Weight")]
  [PXFormula(null, typeof (SumCalc<SOShipment.packageWeight>))]
  public virtual Decimal? Weight
  {
    get => this._Weight;
    set => this._Weight = value;
  }

  [PXUIField(DisplayName = "UOM", Enabled = false)]
  [PXString]
  public virtual string WeightUOM
  {
    get => this._WeightUOM;
    set => this._WeightUOM = value;
  }

  [Inventory(Visible = false)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty", Enabled = false)]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXUIField(DisplayName = "Qty. UOM", Enabled = false)]
  [PXDBString]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.baseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<SOPackageDetail.inventoryID>>>>))]
  public virtual string QtyUOM
  {
    get => this._QtyUOM;
    set => this._QtyUOM = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Tracking Number")]
  public virtual string TrackNumber
  {
    get => this._TrackNumber;
    set => this._TrackNumber = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Tracking URL")]
  public virtual string TrackUrl
  {
    get => this._TrackUrl;
    set => this._TrackUrl = value;
  }

  [PXDBString(4000)]
  public virtual string TrackData
  {
    get => this._TrackData;
    set => this._TrackData = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Declared Value")]
  public virtual Decimal? DeclaredValue
  {
    get => this._DeclaredValue;
    set => this._DeclaredValue = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "C.O.D. Amount")]
  public virtual Decimal? COD
  {
    get => this._COD;
    set => this._COD = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Confirmed
  {
    get => this._Confirmed;
    set => this._Confirmed = value;
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Custom Ref. Nbr. 1")]
  public virtual string CustomRefNbr1
  {
    get => this._CustomRefNbr1;
    set => this._CustomRefNbr1 = value;
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Custom Ref. Nbr. 2")]
  public virtual string CustomRefNbr2
  {
    get => this._CustomRefNbr2;
    set => this._CustomRefNbr2 = value;
  }

  [PXDefault("M")]
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  [SOPackageType.List]
  public virtual string PackageType
  {
    get => this._PackageType;
    set => this._PackageType = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Return Tracking Number", Enabled = false)]
  public virtual string ReturnTrackNumber { get; set; }

  [SOPackageNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
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

  /// <summary>
  /// A Boolean value that specifies whether the <see cref="P:PX.Objects.SO.SOPackageDetail.Width" />, <see cref="P:PX.Objects.SO.SOPackageDetail.Height" />, and <see cref="P:PX.Objects.SO.SOPackageDetail.Length" /> dimension values of the package can be overridden.
  /// </summary>
  /// <value>
  /// The field always returns the value of <see cref="P:PX.Objects.CS.CSBox.AllowOverrideDimension" />.
  /// </value>
  [PXBool]
  [PXUnboundDefault(typeof (Search<PX.Objects.CS.CSBox.allowOverrideDimension, Where<PX.Objects.CS.CSBox.boxID, Equal<Current<SOPackageDetail.boxID>>>>))]
  [PXFormula(typeof (Default<SOPackageDetail.boxID>))]
  [PXUIField(DisplayName = "Editable Dimensions", Enabled = false)]
  public virtual bool? AllowOverrideDimension { get; set; }

  [PXDefault(typeof (Search<PX.Objects.CS.CSBox.length, Where<PX.Objects.CS.CSBox.boxID, Equal<Current<SOPackageDetail.boxID>>>>))]
  [PXDBDecimal(2, MinValue = 0.0)]
  [PXUIField(DisplayName = "Length")]
  [PXUIEnabled(typeof (SOPackageDetail.allowOverrideDimension))]
  public virtual Decimal? Length { get; set; }

  [PXDefault(typeof (Search<PX.Objects.CS.CSBox.width, Where<PX.Objects.CS.CSBox.boxID, Equal<Current<SOPackageDetail.boxID>>>>))]
  [PXDBDecimal(2, MinValue = 0.0)]
  [PXUIField(DisplayName = "Width")]
  [PXUIEnabled(typeof (SOPackageDetail.allowOverrideDimension))]
  public virtual Decimal? Width { get; set; }

  [PXDefault(typeof (Search<PX.Objects.CS.CSBox.height, Where<PX.Objects.CS.CSBox.boxID, Equal<Current<SOPackageDetail.boxID>>>>))]
  [PXDBDecimal(2, MinValue = 0.0)]
  [PXUIField(DisplayName = "Height")]
  [PXUIEnabled(typeof (SOPackageDetail.allowOverrideDimension))]
  public virtual Decimal? Height { get; set; }

  public class PK : 
    PrimaryKeyOf<SOPackageDetail>.By<SOPackageDetail.shipmentNbr, SOPackageDetail.lineNbr>
  {
    public static SOPackageDetail Find(
      PXGraph graph,
      string shipmentNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (SOPackageDetail) PrimaryKeyOf<SOPackageDetail>.By<SOPackageDetail.shipmentNbr, SOPackageDetail.lineNbr>.FindBy(graph, (object) shipmentNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Box : 
      PrimaryKeyOf<PX.Objects.CS.CSBox>.By<PX.Objects.CS.CSBox.boxID>.ForeignKeyOf<SOPackageDetail>.By<SOPackageDetail.boxID>
    {
    }

    public class Shipment : 
      PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentNbr>.ForeignKeyOf<SOPackageDetail>.By<SOPackageDetail.shipmentNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<SOPackageDetail>.By<SOPackageDetail.inventoryID>
    {
    }
  }

  public abstract class shipmentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageDetail.shipmentNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPackageDetail.lineNbr>
  {
  }

  public abstract class boxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageDetail.boxID>
  {
  }

  public abstract class weight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPackageDetail.weight>
  {
  }

  public abstract class weightUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageDetail.weightUOM>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPackageDetail.inventoryID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageDetail.description>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPackageDetail.qty>
  {
  }

  public abstract class qtyUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageDetail.qtyUOM>
  {
  }

  public abstract class trackNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageDetail.trackNumber>
  {
  }

  public abstract class trackUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageDetail.trackUrl>
  {
  }

  public abstract class trackData : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageDetail.trackData>
  {
  }

  public abstract class declaredValue : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPackageDetail.declaredValue>
  {
  }

  public abstract class cOD : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPackageDetail.cOD>
  {
  }

  public abstract class confirmed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOPackageDetail.confirmed>
  {
  }

  public abstract class customRefNbr1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPackageDetail.customRefNbr1>
  {
  }

  public abstract class customRefNbr2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPackageDetail.customRefNbr2>
  {
  }

  public abstract class packageType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageDetail.packageType>
  {
  }

  public abstract class returnTrackNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPackageDetail.returnTrackNumber>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOPackageDetail.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOPackageDetail.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOPackageDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPackageDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPackageDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPackageDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPackageDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPackageDetail.lastModifiedDateTime>
  {
  }

  public abstract class allowOverrideDimension : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPackageDetail.allowOverrideDimension>
  {
  }

  public abstract class length : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPackageDetail.length>
  {
  }

  public abstract class width : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPackageDetail.width>
  {
  }

  public abstract class height : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPackageDetail.height>
  {
  }
}

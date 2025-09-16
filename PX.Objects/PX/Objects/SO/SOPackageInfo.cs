// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPackageInfo
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

[PXCacheName("SO Package Info")]
[Serializable]
public class SOPackageInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _LineNbr;
  protected string _Operation;
  protected int? _SiteID;
  protected int? _InventoryID;
  protected string _BoxID;
  protected Decimal? _Weight;
  public const int BoxWeightPrecision = 4;
  protected Decimal? _GrossWeight;
  protected string _WeightUOM;
  protected Decimal? _Qty;
  protected string _QtyUOM;
  protected Decimal? _DeclaredValue;
  protected bool? _COD = new bool?(false);
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(2, IsFixed = true, IsKey = true, InputMask = ">aa")]
  [PXDBDefault(typeof (SOOrder.orderType))]
  [PXSelector(typeof (Search<SOOrderType.orderType, Where<SOOrderType.active, Equal<boolTrue>>>))]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXParent(typeof (SOPackageInfo.FK.Order))]
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (SOOrder.orderNbr))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (SOOrder.packageLineCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBString(1, IsFixed = true, InputMask = ">a")]
  [PXUIField]
  [PXDefault(typeof (SOOrderType.defaultOperation))]
  [SOOperation.List]
  public virtual string Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXDefault(typeof (SOOrder.defaultSiteID))]
  [Site(DisplayName = "Ship from Warehouse", DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  [PXForeignReference(typeof (SOPackageInfo.FK.Site))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [Inventory]
  [PXForeignReference(typeof (SOPackageInfo.FK.InventoryItem))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXSelector(typeof (Search5<PX.Objects.CS.CSBox.boxID, LeftJoin<CarrierPackage, On<PX.Objects.CS.CSBox.boxID, Equal<CarrierPackage.boxID>>>, Where<Current<SOOrder.shipVia>, IsNull, Or<Where<Current<SOOrder.shipVia>, IsNotNull, And<Where<CarrierPackage.carrierID, Equal<Current<SOOrder.shipVia>>, Or<PX.Objects.CS.CSBox.activeByDefault, Equal<True>>>>>>>, Aggregate<GroupBy<PX.Objects.CS.CSBox.boxID>>>))]
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  public virtual string BoxID
  {
    get => this._BoxID;
    set => this._BoxID = value;
  }

  /// <summary>Net weight</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Net Weight", Enabled = false)]
  public virtual Decimal? Weight
  {
    get => this._Weight;
    set => this._Weight = value;
  }

  /// <summary>Gross weight</summary>
  [PXDBDecimal(4, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Gross Weight")]
  [PXFormula(null, typeof (SumCalc<SOOrder.packageWeight>))]
  public virtual Decimal? GrossWeight
  {
    get => this._GrossWeight;
    set => this._GrossWeight = value;
  }

  /// <summary>Constant from INSetup.</summary>
  [PXUIField(DisplayName = "Weight UOM", Enabled = false)]
  [PXString]
  [PXDefault(typeof (CommonSetup.weightUOM))]
  public virtual string WeightUOM
  {
    get => this._WeightUOM;
    set => this._WeightUOM = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty")]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXUIField(DisplayName = "Qty. UOM", Enabled = false)]
  [PXDBString]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.baseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<SOPackageInfo.inventoryID>>>>))]
  public virtual string QtyUOM
  {
    get => this._QtyUOM;
    set => this._QtyUOM = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Declared Value")]
  public virtual Decimal? DeclaredValue
  {
    get => this._DeclaredValue;
    set => this._DeclaredValue = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? COD
  {
    get => this._COD;
    set => this._COD = value;
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
  /// A Boolean value that specifies whether the <see cref="P:PX.Objects.SO.SOPackageInfo.Width" />, <see cref="P:PX.Objects.SO.SOPackageInfo.Height" /> and <see cref="P:PX.Objects.SO.SOPackageInfo.Length" /> dimension values of the package can be overridden.
  /// </summary>
  /// <value>
  /// The field always returns the value of <see cref="P:PX.Objects.CS.CSBox.AllowOverrideDimension" />.
  /// </value>
  [PXBool]
  [PXUnboundDefault(typeof (Search<PX.Objects.CS.CSBox.allowOverrideDimension, Where<PX.Objects.CS.CSBox.boxID, Equal<Current<SOPackageInfo.boxID>>>>))]
  [PXFormula(typeof (Default<SOPackageInfo.boxID>))]
  [PXUIField(DisplayName = "Editable Dimensions", Enabled = false)]
  public virtual bool? AllowOverrideDimension { get; set; }

  [PXDefault(typeof (Search<PX.Objects.CS.CSBox.length, Where<PX.Objects.CS.CSBox.boxID, Equal<Current<SOPackageInfo.boxID>>>>))]
  [PXDBDecimal(2, MinValue = 0.0)]
  [PXUIField(DisplayName = "Length")]
  [PXUIEnabled(typeof (SOPackageInfo.allowOverrideDimension))]
  public virtual Decimal? Length { get; set; }

  [PXDefault(typeof (Search<PX.Objects.CS.CSBox.width, Where<PX.Objects.CS.CSBox.boxID, Equal<Current<SOPackageInfo.boxID>>>>))]
  [PXDBDecimal(2, MinValue = 0.0)]
  [PXUIField(DisplayName = "Width")]
  [PXUIEnabled(typeof (SOPackageInfo.allowOverrideDimension))]
  public virtual Decimal? Width { get; set; }

  [PXDefault(typeof (Search<PX.Objects.CS.CSBox.height, Where<PX.Objects.CS.CSBox.boxID, Equal<Current<SOPackageInfo.boxID>>>>))]
  [PXDBDecimal(2, MinValue = 0.0)]
  [PXUIField(DisplayName = "Height")]
  [PXUIEnabled(typeof (SOPackageInfo.allowOverrideDimension))]
  public virtual Decimal? Height { get; set; }

  public class PK : 
    PrimaryKeyOf<SOPackageInfo>.By<SOPackageInfo.orderType, SOPackageInfo.orderNbr, SOPackageInfo.lineNbr>
  {
    public static SOPackageInfo Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (SOPackageInfo) PrimaryKeyOf<SOPackageInfo>.By<SOPackageInfo.orderType, SOPackageInfo.orderNbr, SOPackageInfo.lineNbr>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Order : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOPackageInfo>.By<SOPackageInfo.orderType, SOPackageInfo.orderNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<SOPackageInfo>.By<SOPackageInfo.inventoryID>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOPackageInfo>.By<SOPackageInfo.siteID>
    {
    }

    public class Box : 
      PrimaryKeyOf<PX.Objects.CS.CSBox>.By<PX.Objects.CS.CSBox.boxID>.ForeignKeyOf<SOPackageInfo>.By<SOPackageInfo.boxID>
    {
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageInfo.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageInfo.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPackageInfo.lineNbr>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageInfo.operation>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPackageInfo.siteID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPackageInfo.inventoryID>
  {
  }

  public abstract class boxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageInfo.boxID>
  {
  }

  public abstract class weight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPackageInfo.weight>
  {
  }

  public abstract class grossWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPackageInfo.grossWeight>
  {
  }

  public abstract class weightUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageInfo.weightUOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPackageInfo.qty>
  {
  }

  public abstract class qtyUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageInfo.qtyUOM>
  {
  }

  public abstract class declaredValue : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPackageInfo.declaredValue>
  {
  }

  public abstract class cOD : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOPackageInfo.cOD>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOPackageInfo.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOPackageInfo.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPackageInfo.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPackageInfo.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPackageInfo.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPackageInfo.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPackageInfo.lastModifiedDateTime>
  {
  }

  public abstract class allowOverrideDimension : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPackageInfo.allowOverrideDimension>
  {
  }

  public abstract class length : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPackageInfo.length>
  {
  }

  public abstract class width : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPackageInfo.width>
  {
  }

  public abstract class height : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPackageInfo.height>
  {
  }
}

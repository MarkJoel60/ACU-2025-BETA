// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INKitSpecNonStkDet
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("Non-Stock Component of Kit Specification")]
[Serializable]
public class INKitSpecNonStkDet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _KitInventoryID;
  protected 
  #nullable disable
  string _RevisionID;
  protected int? _LineNbr;
  protected int? _CompInventoryID;
  protected Decimal? _DfltCompQty;
  protected string _UOM;
  protected bool? _AllowQtyVariation;
  protected Decimal? _MinCompQty;
  protected Decimal? _MaxCompQty;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [Inventory(IsKey = true, DisplayName = "Inventory ID")]
  [PXRestrictor(typeof (Where<InventoryItem.kitItem, Equal<boolTrue>>), "The inventory item is not a kit.", new Type[] {})]
  [PXDefault(typeof (INKitSpecHdr.kitInventoryID))]
  public virtual int? KitInventoryID
  {
    get => this._KitInventoryID;
    set => this._KitInventoryID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (INKitSpecHdr.revisionID))]
  [PXParent(typeof (INKitSpecNonStkDet.FK.KitSpecification))]
  public virtual string RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (INKitSpecHdr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [NonStockItem(DisplayName = "Component ID")]
  [PXRestrictor(typeof (Where<InventoryItem.kitItem, Equal<boolFalse>>), "It is not allowed to add non-stock kits as components to a stock kit or to a  non-stock kit.", new Type[] {})]
  [PXDefault]
  [PXForeignReference(typeof (INKitSpecNonStkDet.FK.ComponentInventoryItem))]
  public virtual int? CompInventoryID
  {
    get => this._CompInventoryID;
    set => this._CompInventoryID = value;
  }

  [PXDBQuantity(typeof (INKitSpecNonStkDet.uOM), typeof (INKitSpecNonStkDet.baseDfltCompQty), InventoryUnitType.BaseUnit, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Component Qty.")]
  public virtual Decimal? DfltCompQty
  {
    get => this._DfltCompQty;
    set => this._DfltCompQty = value;
  }

  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseDfltCompQty { get; set; }

  [PXDefault(typeof (Search<InventoryItem.baseUnit, Where<InventoryItem.inventoryID, Equal<Current<INKitSpecNonStkDet.compInventoryID>>>>))]
  [INUnit(typeof (INKitSpecNonStkDet.compInventoryID))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Component Qty. Variance")]
  public virtual bool? AllowQtyVariation
  {
    get => this._AllowQtyVariation;
    set => this._AllowQtyVariation = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Min. Component Qty.")]
  public virtual Decimal? MinCompQty
  {
    get => this._MinCompQty;
    set => this._MinCompQty = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Max. Component Qty.")]
  public virtual Decimal? MaxCompQty
  {
    get => this._MaxCompQty;
    set => this._MaxCompQty = value;
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

  public class PK : 
    PrimaryKeyOf<INKitSpecNonStkDet>.By<INKitSpecNonStkDet.kitInventoryID, INKitSpecNonStkDet.revisionID, INKitSpecNonStkDet.lineNbr>
  {
    public static INKitSpecNonStkDet Find(
      PXGraph graph,
      int? kitInventoryID,
      string revisionID,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (INKitSpecNonStkDet) PrimaryKeyOf<INKitSpecNonStkDet>.By<INKitSpecNonStkDet.kitInventoryID, INKitSpecNonStkDet.revisionID, INKitSpecNonStkDet.lineNbr>.FindBy(graph, (object) kitInventoryID, (object) revisionID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class KitInventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INKitSpecNonStkDet>.By<INKitSpecNonStkDet.kitInventoryID>
    {
    }

    public class ComponentInventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INKitSpecNonStkDet>.By<INKitSpecNonStkDet.compInventoryID>
    {
    }

    public class KitSpecification : 
      PrimaryKeyOf<INKitSpecHdr>.By<INKitSpecHdr.kitInventoryID, INKitSpecHdr.revisionID>.ForeignKeyOf<INKitSpecNonStkDet>.By<INKitSpecNonStkDet.kitInventoryID, INKitSpecNonStkDet.revisionID>
    {
    }
  }

  public abstract class kitInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INKitSpecNonStkDet.kitInventoryID>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitSpecNonStkDet.revisionID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitSpecNonStkDet.lineNbr>
  {
  }

  public abstract class compInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INKitSpecNonStkDet.compInventoryID>
  {
  }

  public abstract class dfltCompQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INKitSpecNonStkDet.dfltCompQty>
  {
  }

  public abstract class baseDfltCompQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INKitSpecNonStkDet.baseDfltCompQty>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitSpecNonStkDet.uOM>
  {
  }

  public abstract class allowQtyVariation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INKitSpecNonStkDet.allowQtyVariation>
  {
  }

  public abstract class minCompQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INKitSpecNonStkDet.minCompQty>
  {
  }

  public abstract class maxCompQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INKitSpecNonStkDet.maxCompQty>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INKitSpecNonStkDet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitSpecNonStkDet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INKitSpecNonStkDet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INKitSpecNonStkDet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitSpecNonStkDet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INKitSpecNonStkDet.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INKitSpecNonStkDet.Tstamp>
  {
  }
}

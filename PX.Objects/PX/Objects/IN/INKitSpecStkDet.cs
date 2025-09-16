// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INKitSpecStkDet
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

[PXCacheName("Stock Component of Kit Specification")]
[Serializable]
public class INKitSpecStkDet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _KitInventoryID;
  protected 
  #nullable disable
  string _RevisionID;
  protected int? _LineNbr;
  protected int? _CompInventoryID;
  protected int? _CompSubItemID;
  protected Decimal? _DfltCompQty;
  protected string _UOM;
  protected bool? _AllowQtyVariation;
  protected Decimal? _MinCompQty;
  protected Decimal? _MaxCompQty;
  protected Decimal? _DisassemblyCoeff;
  protected bool? _AllowSubstitution;
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
  [PXParent(typeof (INKitSpecStkDet.FK.KitSpecification))]
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

  [StockItem(DisplayName = "Component ID")]
  [PXDefault]
  [PXForeignReference(typeof (INKitSpecStkDet.FK.ComponentInventoryItem))]
  public virtual int? CompInventoryID
  {
    get => this._CompInventoryID;
    set => this._CompInventoryID = value;
  }

  [SubItem(typeof (INKitSpecStkDet.compInventoryID))]
  [PXDefault(typeof (Search<InventoryItem.defaultSubItemID, Where<InventoryItem.inventoryID, Equal<Current<INKitSpecStkDet.compInventoryID>>, And<InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>))]
  [PXFormula(typeof (Default<INKitSpecStkDet.compInventoryID>))]
  [PXUIRequired(typeof (IIf<Where<Selector<INKitSpecStkDet.compInventoryID, InventoryItem.stkItem>, Equal<True>, And<FeatureInstalled<FeaturesSet.subItem>>>, True, False>))]
  public virtual int? CompSubItemID
  {
    get => this._CompSubItemID;
    set => this._CompSubItemID = value;
  }

  [PXDBQuantity(typeof (INKitSpecStkDet.uOM), typeof (INKitSpecStkDet.baseDfltCompQty), InventoryUnitType.BaseUnit, MinValue = 0.0)]
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

  [PXDefault(typeof (Search<InventoryItem.baseUnit, Where<InventoryItem.inventoryID, Equal<Current<INKitSpecStkDet.compInventoryID>>>>))]
  [INUnit(typeof (INKitSpecStkDet.compInventoryID))]
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

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 1.0)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  [PXUIField(DisplayName = "Disassembly Coeff.")]
  public virtual Decimal? DisassemblyCoeff
  {
    get => this._DisassemblyCoeff;
    set => this._DisassemblyCoeff = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Component Substitution")]
  public virtual bool? AllowSubstitution
  {
    get => this._AllowSubstitution;
    set => this._AllowSubstitution = value;
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
    PrimaryKeyOf<INKitSpecStkDet>.By<INKitSpecStkDet.kitInventoryID, INKitSpecStkDet.revisionID, INKitSpecStkDet.lineNbr>
  {
    public static INKitSpecStkDet Find(
      PXGraph graph,
      int? kitInventoryID,
      string revisionID,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (INKitSpecStkDet) PrimaryKeyOf<INKitSpecStkDet>.By<INKitSpecStkDet.kitInventoryID, INKitSpecStkDet.revisionID, INKitSpecStkDet.lineNbr>.FindBy(graph, (object) kitInventoryID, (object) revisionID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class KitInventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INKitSpecStkDet>.By<INKitSpecStkDet.kitInventoryID>
    {
    }

    public class ComponentInventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INKitSpecStkDet>.By<INKitSpecStkDet.compInventoryID>
    {
    }

    public class KitSpecification : 
      PrimaryKeyOf<INKitSpecHdr>.By<INKitSpecHdr.kitInventoryID, INKitSpecHdr.revisionID>.ForeignKeyOf<INKitSpecStkDet>.By<INKitSpecStkDet.kitInventoryID, INKitSpecStkDet.revisionID>
    {
    }

    public class ComponentSubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INKitSpecStkDet>.By<INKitSpecStkDet.compSubItemID>
    {
    }
  }

  public abstract class kitInventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitSpecStkDet.kitInventoryID>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitSpecStkDet.revisionID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitSpecStkDet.lineNbr>
  {
  }

  public abstract class compInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INKitSpecStkDet.compInventoryID>
  {
  }

  public abstract class compSubItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitSpecStkDet.compSubItemID>
  {
  }

  public abstract class dfltCompQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INKitSpecStkDet.dfltCompQty>
  {
  }

  public abstract class baseDfltCompQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INKitSpecStkDet.baseDfltCompQty>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitSpecStkDet.uOM>
  {
  }

  public abstract class allowQtyVariation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INKitSpecStkDet.allowQtyVariation>
  {
  }

  public abstract class minCompQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INKitSpecStkDet.minCompQty>
  {
  }

  public abstract class maxCompQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INKitSpecStkDet.maxCompQty>
  {
  }

  public abstract class disassemblyCoeff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INKitSpecStkDet.disassemblyCoeff>
  {
  }

  public abstract class allowSubstitution : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INKitSpecStkDet.allowSubstitution>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INKitSpecStkDet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitSpecStkDet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INKitSpecStkDet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INKitSpecStkDet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitSpecStkDet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INKitSpecStkDet.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INKitSpecStkDet.Tstamp>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INUnit
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

[PXPrimaryGraph(typeof (UnitOfMeasureMaint))]
[PXCacheName]
[Serializable]
public class INUnit : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected short? _UnitType;
  protected int? _ItemClassID;
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _ToUnit;
  protected string _SampleToUnit;
  protected string _FromUnit;
  protected string _UnitMultDiv;
  protected Decimal? _UnitRate;
  protected Decimal? _PriceAdjustmentMultiplier;
  protected long? _RecordID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBShort(IsKey = true)]
  [PXDefault(3)]
  [PXUIField]
  [PXIntList(new int[] {1, 2, 3}, new string[] {"Inventory Item", "Item Class", "Global"})]
  public virtual short? UnitType
  {
    get => this._UnitType;
    set => this._UnitType = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(0)]
  [PXUIField]
  [PXParent(typeof (INUnit.FK.ItemClass))]
  public virtual int? ItemClassID
  {
    get => this._ItemClassID;
    set => this._ItemClassID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(0)]
  [PXUIField]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDefault]
  [INUnit]
  public virtual string ToUnit
  {
    get => this._ToUnit;
    set => this._ToUnit = value;
  }

  [PXString(6, IsUnicode = true, InputMask = ">aaaaaa")]
  [PXUIField(DisplayName = "To Unit", Visible = false)]
  public virtual string SampleToUnit
  {
    [PXDependsOnFields(new Type[] {typeof (INUnit.toUnit)})] get
    {
      return this._SampleToUnit ?? this._ToUnit;
    }
    set => this._SampleToUnit = value;
  }

  [PXDefault]
  [INUnit]
  [UnitOfMeasure]
  public virtual string FromUnit
  {
    get => this._FromUnit;
    set => this._FromUnit = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("M")]
  [PXUIField]
  [MultDiv.List]
  public virtual string UnitMultDiv
  {
    get => this._UnitMultDiv;
    set => this._UnitMultDiv = value;
  }

  [PXDBDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  [PXUIField]
  [PXFormula(typeof (Validate<INUnit.unitMultDiv>))]
  public virtual Decimal? UnitRate
  {
    get => this._UnitRate;
    set => this._UnitRate = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  [PXUIField]
  [PXUIVerify]
  public virtual Decimal? PriceAdjustmentMultiplier
  {
    get => this._PriceAdjustmentMultiplier;
    set => this._PriceAdjustmentMultiplier = value;
  }

  [PXDBLongIdentity]
  public virtual long? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
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

  public class PK : PrimaryKeyOf<INUnit>.By<INUnit.recordID>
  {
    public static INUnit Find(PXGraph graph, long? recordID, PKFindOptions options = 0)
    {
      return (INUnit) PrimaryKeyOf<INUnit>.By<INUnit.recordID>.FindBy(graph, (object) recordID, options);
    }
  }

  public abstract class UK
  {
    public class ByGlobal : PrimaryKeyOf<INUnit>.By<INUnit.unitType, INUnit.fromUnit, INUnit.toUnit>
    {
      public static INUnit Find(PXGraph graph, string fromUnit, string toUnit)
      {
        return (INUnit) PrimaryKeyOf<INUnit>.By<INUnit.unitType, INUnit.fromUnit, INUnit.toUnit>.FindBy(graph, (object) (short) 3, (object) fromUnit, (object) toUnit, (PKFindOptions) 0);
      }

      internal static INUnit FindDirty(PXGraph graph, string fromUnit, string toUnit)
      {
        return PXResultset<INUnit>.op_Implicit(PXSelectBase<INUnit, PXSelect<INUnit, Where<INUnit.unitType, Equal<INUnitType.global>, And<INUnit.fromUnit, Equal<Required<INUnit.fromUnit>>, And<INUnit.toUnit, Equal<Required<INUnit.toUnit>>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
        {
          (object) fromUnit,
          (object) toUnit
        }));
      }
    }

    public class ByInventory : 
      PrimaryKeyOf<INUnit>.By<INUnit.unitType, INUnit.inventoryID, INUnit.fromUnit>
    {
      public static INUnit Find(PXGraph graph, int? inventoryID, string fromUnit)
      {
        return (INUnit) PrimaryKeyOf<INUnit>.By<INUnit.unitType, INUnit.inventoryID, INUnit.fromUnit>.FindBy(graph, (object) (short) 1, (object) inventoryID, (object) fromUnit, (PKFindOptions) 0);
      }

      public static INUnit FindDirty(PXGraph graph, int? inventoryID, string fromUnit)
      {
        return PXResultset<INUnit>.op_Implicit(PXSelectBase<INUnit, PXSelect<INUnit, Where<INUnit.unitType, Equal<INUnitType.inventoryItem>, And<INUnit.inventoryID, Equal<Required<INUnit.inventoryID>>, And<INUnit.fromUnit, Equal<Required<INUnit.fromUnit>>>>>>.Config>.Select(graph, new object[2]
        {
          (object) inventoryID,
          (object) fromUnit
        }));
      }
    }

    public class ByItemClass : 
      PrimaryKeyOf<INUnit>.By<INUnit.unitType, INUnit.itemClassID, INUnit.fromUnit>
    {
      public static INUnit Find(PXGraph graph, int? itemClassID, string fromUnit)
      {
        return (INUnit) PrimaryKeyOf<INUnit>.By<INUnit.unitType, INUnit.itemClassID, INUnit.fromUnit>.FindBy(graph, (object) (short) 2, (object) itemClassID, (object) fromUnit, (PKFindOptions) 0);
      }

      internal static INUnit FindDirty(PXGraph graph, int? itemClassID, string fromUnit)
      {
        return PXResultset<INUnit>.op_Implicit(PXSelectBase<INUnit, PXSelect<INUnit, Where<INUnit.unitType, Equal<INUnitType.itemClass>, And<INUnit.itemClassID, Equal<Required<INUnit.itemClassID>>, And<INUnit.fromUnit, Equal<Required<INUnit.fromUnit>>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
        {
          (object) itemClassID,
          (object) fromUnit
        }));
      }
    }
  }

  public static class FK
  {
    public class ItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<INUnit>.By<INUnit.itemClassID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INUnit>.By<INUnit.inventoryID>
    {
    }
  }

  public abstract class unitType : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INUnit.unitType>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INUnit.itemClassID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INUnit.inventoryID>
  {
  }

  public abstract class toUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INUnit.toUnit>
  {
  }

  public abstract class sampleToUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INUnit.sampleToUnit>
  {
  }

  public abstract class fromUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INUnit.fromUnit>
  {
  }

  public abstract class unitMultDiv : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INUnit.unitMultDiv>
  {
  }

  public abstract class unitRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INUnit.unitRate>
  {
  }

  public abstract class priceAdjustmentMultiplier : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INUnit.priceAdjustmentMultiplier>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  INUnit.recordID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INUnit.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INUnit.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INUnit.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INUnit.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INUnit.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INUnit.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INUnit.Tstamp>
  {
  }
}

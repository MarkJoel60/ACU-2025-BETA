// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemBoxEx
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXProjection(typeof (Select2<INItemBox, InnerJoin<PX.Objects.CS.CSBox, On<PX.Objects.CS.CSBox.boxID, Equal<INItemBox.boxID>>>>), new Type[] {typeof (INItemBox)})]
[Serializable]
public class INItemBoxEx : INItemBox
{
  protected 
  #nullable disable
  string _Description;
  protected Decimal? _MaxWeight;
  protected Decimal? _BoxWeight;
  protected Decimal? _MaxVolume;
  protected Decimal? _Length;
  protected Decimal? _Width;
  protected Decimal? _Height;

  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (PX.Objects.CS.CSBox.description))]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBDecimal(4, MinValue = 0.0, BqlField = typeof (PX.Objects.CS.CSBox.maxWeight))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? MaxWeight
  {
    get => this._MaxWeight;
    set => this._MaxWeight = value;
  }

  [PXDBDecimal(4, MinValue = 0.0, BqlField = typeof (PX.Objects.CS.CSBox.boxWeight))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? BoxWeight
  {
    get => this._BoxWeight;
    set => this._BoxWeight = value;
  }

  [PXDBDecimal(4, MinValue = 0.0, BqlField = typeof (PX.Objects.CS.CSBox.maxVolume))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? MaxVolume
  {
    get => this._MaxVolume;
    set => this._MaxVolume = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, BqlField = typeof (PX.Objects.CS.CSBox.length))]
  [PXUIField]
  public virtual Decimal? Length
  {
    get => this._Length;
    set => this._Length = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, BqlField = typeof (PX.Objects.CS.CSBox.width))]
  [PXUIField]
  public virtual Decimal? Width
  {
    get => this._Width;
    set => this._Width = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, BqlField = typeof (PX.Objects.CS.CSBox.height))]
  [PXUIField]
  public virtual Decimal? Height
  {
    get => this._Height;
    set => this._Height = value;
  }

  public virtual Decimal MaxNetWeight
  {
    get
    {
      Decimal? nullable = this.MaxWeight;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.BoxWeight;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return valueOrDefault1 - valueOrDefault2;
    }
  }

  public new class PK : PrimaryKeyOf<INItemBoxEx>.By<INItemBoxEx.inventoryID, INItemBoxEx.boxID>
  {
    public static INItemBoxEx Find(
      PXGraph graph,
      int? inventoryID,
      string boxID,
      PKFindOptions options = 0)
    {
      return (INItemBoxEx) PrimaryKeyOf<INItemBoxEx>.By<INItemBoxEx.inventoryID, INItemBoxEx.boxID>.FindBy(graph, (object) inventoryID, (object) boxID, options);
    }
  }

  public new static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemBoxEx>.By<INItemBoxEx.inventoryID>
    {
    }

    public class CSBox : 
      PrimaryKeyOf<PX.Objects.CS.CSBox>.By<PX.Objects.CS.CSBox.boxID>.ForeignKeyOf<INItemBoxEx>.By<INItemBoxEx.boxID>
    {
    }
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemBoxEx.inventoryID>
  {
  }

  public new abstract class boxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemBoxEx.boxID>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemBoxEx.uOM>
  {
  }

  public new abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemBoxEx.qty>
  {
  }

  public new abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemBoxEx.baseQty>
  {
  }

  public new abstract class maxQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemBoxEx.maxQty>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemBoxEx.description>
  {
  }

  public abstract class maxWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemBoxEx.maxWeight>
  {
  }

  public abstract class boxWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemBoxEx.boxWeight>
  {
  }

  public abstract class maxVolume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemBoxEx.maxVolume>
  {
  }

  public abstract class length : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemBoxEx.length>
  {
  }

  public abstract class width : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemBoxEx.width>
  {
  }

  public abstract class height : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemBoxEx.height>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PIPreliminaryResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class PIPreliminaryResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _LineNbr;
  protected int? _TagNumber;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _LocationID;
  protected 
  #nullable disable
  string _LotSerialNbr;
  protected DateTime? _ExpireDate;
  protected Decimal? _BookQty;
  protected string _BaseUnit;
  protected string _Descr;

  [PXDBInt(IsKey = true)]
  [PXUIField]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXInt(MinValue = 0)]
  [PXUIField]
  public virtual int? TagNumber
  {
    get => this._TagNumber;
    set => this._TagNumber = value;
  }

  [Inventory]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [Site]
  public int? SiteID { get; set; }

  [Location(typeof (PIPreliminaryResult.siteID))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Expiration Date")]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [PXDBQuantity]
  [PXUIField]
  public virtual Decimal? BookQty
  {
    get => this._BookQty;
    set => this._BookQty = value;
  }

  [INUnit]
  public virtual string BaseUnit
  {
    get => this._BaseUnit;
    set => this._BaseUnit = value;
  }

  [PXString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBInt]
  [PXUIField]
  [PXDimensionSelector("INITEMCLASS", typeof (INItemClass.itemClassID), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), ValidComboRequired = true)]
  public virtual int? ItemClassID { get; set; }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PIPreliminaryResult.lineNbr>
  {
  }

  public abstract class tagNumber : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PIPreliminaryResult.tagNumber>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PIPreliminaryResult.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PIPreliminaryResult.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PIPreliminaryResult.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PIPreliminaryResult.locationID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PIPreliminaryResult.lotSerialNbr>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PIPreliminaryResult.expireDate>
  {
  }

  public abstract class bookQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PIPreliminaryResult.bookQty>
  {
  }

  public abstract class baseUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PIPreliminaryResult.baseUnit>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PIPreliminaryResult.descr>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PIPreliminaryResult.itemClassID>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INBarCodeItem
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

[Serializable]
public class INBarCodeItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _BarCode;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected string _UOM;
  protected int? _SiteID;
  protected int? _LocationID;
  protected string _LotSerialNbr;
  protected DateTime? _ExpireDate;
  protected string _ReasonCode;
  protected Decimal? _Qty;
  protected Decimal? _BaseQty;
  protected bool? _ByOne;
  protected bool? _AutoAddLine;
  protected string _Description;

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Bar Code")]
  public virtual string BarCode
  {
    get => this._BarCode;
    set => this._BarCode = value;
  }

  [StockItem(Filterable = true)]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(typeof (INBarCodeItem.inventoryID))]
  [PXDefault(typeof (Search<InventoryItem.defaultSubItemID, Where<InventoryItem.inventoryID, Equal<Current2<INBarCodeItem.inventoryID>>, And<InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>))]
  [PXFormula(typeof (Default<INBarCodeItem.inventoryID>))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDefault(typeof (Search<InventoryItem.baseUnit, Where<InventoryItem.inventoryID, Equal<Current<INBarCodeItem.inventoryID>>>>))]
  [INUnit(typeof (INBarCodeItem.inventoryID))]
  [PXFormula(typeof (Default<INBarCodeItem.inventoryID>))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [POSiteAvail(typeof (INBarCodeItem.inventoryID), typeof (INBarCodeItem.subItemID), typeof (CostCenter.freeStock))]
  [PXFormula(typeof (Default<INBarCodeItem.inventoryID>))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDefault]
  [Location(typeof (INBarCodeItem.siteID))]
  [PXFormula(typeof (Default<INBarCodeItem.siteID>))]
  [PXFormula(typeof (Default<INBarCodeItem.inventoryID>))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDefault]
  [PX.Objects.IN.LotSerialNbr]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXDefault(typeof (Search<INItemLotSerial.expireDate, Where<INItemLotSerial.inventoryID, Equal<Current<INBarCodeItem.inventoryID>>, And<INItemLotSerial.lotSerialNbr, Equal<Current<INBarCodeItem.lotSerialNbr>>>>>))]
  [PXDBDate(InputMask = "d", DisplayMask = "d")]
  [PXUIField(DisplayName = "Expiration Date")]
  [PXFormula(typeof (Default<INBarCodeItem.inventoryID>))]
  [PXFormula(typeof (Default<INBarCodeItem.lotSerialNbr>))]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [PXDefault]
  [PXDBString(20, IsUnicode = true)]
  public virtual string ReasonCode
  {
    get => this._ReasonCode;
    set => this._ReasonCode = value;
  }

  [PXDBQuantity(typeof (INBarCodeItem.uOM), typeof (INBarCodeItem.baseQty), HandleEmptyKey = true, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  [PXUIField]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseQty
  {
    get => this._BaseQty;
    set => this._BaseQty = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Add One Unit Per Bar Code")]
  [PXDefault(typeof (INSetup.addByOneBarcode))]
  public virtual bool? ByOne
  {
    get => this._ByOne;
    set => this._ByOne = value;
  }

  [PXDBBool]
  [PXDefault(typeof (INSetup.autoAddLineBarcode))]
  [PXUIField(DisplayName = "Auto Add Line")]
  public virtual bool? AutoAddLine
  {
    get => this._AutoAddLine;
    set => this._AutoAddLine = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INBarCodeItem>.By<INBarCodeItem.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INBarCodeItem>.By<INBarCodeItem.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INBarCodeItem>.By<INBarCodeItem.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INBarCodeItem>.By<INBarCodeItem.locationID>
    {
    }

    public class ReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<INBarCodeItem>.By<INBarCodeItem.reasonCode>
    {
    }
  }

  public abstract class barCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INBarCodeItem.barCode>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INBarCodeItem.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INBarCodeItem.subItemID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INBarCodeItem.uOM>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INBarCodeItem.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INBarCodeItem.locationID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INBarCodeItem.lotSerialNbr>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INBarCodeItem.expireDate>
  {
  }

  public abstract class reasonCode : IBqlField, IBqlOperand
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INBarCodeItem.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INBarCodeItem.baseQty>
  {
  }

  public abstract class byOne : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INBarCodeItem.byOne>
  {
  }

  public abstract class autoAddLine : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INBarCodeItem.autoAddLine>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INBarCodeItem.description>
  {
  }
}

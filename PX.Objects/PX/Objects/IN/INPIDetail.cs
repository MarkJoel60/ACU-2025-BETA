// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPIDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.IN.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("IN Physical count Detail")]
[Serializable]
public class INPIDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _PIID;
  protected int? _LineNbr;
  protected int? _TagNumber;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _LocationID;
  protected string _LotSerialNbr;
  protected DateTime? _ExpireDate;
  protected Decimal? _BookQty;
  protected Decimal? _PhysicalQty;
  protected Decimal? _VarQty;
  protected Decimal? _UnitCost;
  protected string _ReasonCode;
  protected Decimal? _ExtVarCost;
  protected string _Status;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;
  protected string _LineType;

  [PXDBDefault(typeof (INPIHeader.pIID))]
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXParent(typeof (INPIDetail.FK.PIHeader))]
  public virtual string PIID
  {
    get => this._PIID;
    set => this._PIID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (INPIHeader.lineCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Enabled = false)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt(MinValue = 0)]
  [PXUIField(DisplayName = "Tag Nbr.", Enabled = false)]
  public virtual int? TagNumber
  {
    get => this._TagNumber;
    set => this._TagNumber = value;
  }

  [StockItem(DisplayName = "Inventory ID")]
  [PXForeignReference(typeof (INPIDetail.FK.InventoryItem))]
  [ConvertedInventoryItem(true)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDefault(typeof (Search<InventoryItem.defaultSubItemID, Where<InventoryItem.inventoryID, Equal<Current<INPIDetail.inventoryID>>, And<InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>))]
  [PXFormula(typeof (Default<INTranSplit.inventoryID>))]
  [SubItem(typeof (INPIDetail.inventoryID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDefault(typeof (INPIHeader.siteID))]
  [Site]
  public int? SiteID { get; set; }

  [Location(typeof (INPIDetail.siteID))]
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
  [PXUIField(DisplayName = "Expiration Date", FieldClass = "LotSerial")]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Cost", Enabled = false)]
  public virtual bool? ManualCost { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Book Quantity", Enabled = false)]
  public virtual Decimal? BookQty
  {
    get => this._BookQty;
    set => this._BookQty = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Physical Quantity")]
  public virtual Decimal? PhysicalQty
  {
    get => this._PhysicalQty;
    set => this._PhysicalQty = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Variance Quantity", Enabled = false)]
  public virtual Decimal? VarQty
  {
    get => this._VarQty;
    set => this._VarQty = value;
  }

  [PXDBPriceCost(true)]
  [PXUIField]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.adjustment>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXDefault(typeof (Coalesce<Search2<INPostClass.pIReasonCode, InnerJoin<InventoryItem, On<InventoryItem.FK.PostClass>>, Where<InventoryItem.inventoryID, Equal<Current<INPIDetail.inventoryID>>, And<INPostClass.pIReasonCode, IsNotNull>>>, Search<INSetup.pIReasonCode>>))]
  [PXFormula(typeof (Default<INPIDetail.inventoryID>))]
  [PXUIField(DisplayName = "Reason Code")]
  [PXForeignReference(typeof (INPIDetail.FK.ReasonCode))]
  public virtual string ReasonCode
  {
    get => this._ReasonCode;
    set => this._ReasonCode = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField]
  public virtual Decimal? ExtVarCost
  {
    get => this._ExtVarCost;
    set => this._ExtVarCost = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Final Ext. Variance Cost", Enabled = false)]
  public virtual Decimal? FinalExtVarCost { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PXUIField]
  [INPIDetStatus.List]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
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

  [PXDBString(1, IsFixed = true)]
  [PXDefault("U")]
  [PXUIField]
  [INPIDetLineType.List]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  public virtual bool? IsCostDefaulting { get; set; }

  public class PK : PrimaryKeyOf<INPIDetail>.By<INPIDetail.pIID, INPIDetail.lineNbr>
  {
    public static INPIDetail Find(PXGraph graph, string pIID, int? lineNbr, PKFindOptions options = 0)
    {
      return (INPIDetail) PrimaryKeyOf<INPIDetail>.By<INPIDetail.pIID, INPIDetail.lineNbr>.FindBy(graph, (object) pIID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class PIHeader : 
      PrimaryKeyOf<INPIHeader>.By<INPIHeader.pIID>.ForeignKeyOf<INPIDetail>.By<INPIDetail.pIID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INPIDetail>.By<INPIDetail.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INPIDetail>.By<INPIDetail.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INPIDetail>.By<INPIDetail.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INPIDetail>.By<INPIDetail.locationID>
    {
    }

    public class ReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<INPIDetail>.By<INPIDetail.reasonCode>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<INPIDetail>.By<INPIDetail.inventoryID, INPIDetail.subItemID, INPIDetail.siteID, INPIDetail.locationID>
    {
    }

    public class LotSerialStatus : 
      PrimaryKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.subItemID, INLotSerialStatus.siteID, INLotSerialStatus.locationID, INLotSerialStatus.lotSerialNbr>.ForeignKeyOf<INPIDetail>.By<INPIDetail.inventoryID, INPIDetail.subItemID, INPIDetail.siteID, INPIDetail.locationID, INPIDetail.lotSerialNbr>
    {
    }
  }

  public abstract class pIID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIDetail.pIID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIDetail.lineNbr>
  {
  }

  public abstract class tagNumber : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIDetail.tagNumber>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIDetail.inventoryID>
  {
    public class InventoryBaseUnitRule : 
      InventoryItem.baseUnit.PreventEditIfExists<Select2<INPIDetail, InnerJoin<INPIHeader, On<INPIDetail.FK.PIHeader>>, Where<INPIDetail.inventoryID, Equal<Current<InventoryItem.inventoryID>>, And<INPIHeader.status, NotIn3<INPIHdrStatus.completed, INPIHdrStatus.cancelled>>>>>
    {
    }
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIDetail.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIDetail.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIDetail.locationID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIDetail.lotSerialNbr>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INPIDetail.expireDate>
  {
  }

  public abstract class manualCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INPIDetail.manualCost>
  {
  }

  public abstract class bookQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INPIDetail.bookQty>
  {
  }

  public abstract class physicalQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INPIDetail.physicalQty>
  {
  }

  public abstract class varQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INPIDetail.varQty>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INPIDetail.unitCost>
  {
  }

  public abstract class reasonCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIDetail.reasonCode>
  {
  }

  public abstract class extVarCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INPIDetail.extVarCost>
  {
  }

  public abstract class finalExtVarCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INPIDetail.finalExtVarCost>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIDetail.status>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INPIDetail.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INPIDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPIDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INPIDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INPIDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPIDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INPIDetail.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INPIDetail.Tstamp>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIDetail.lineType>
  {
  }
}

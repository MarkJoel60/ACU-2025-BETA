// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptLineSplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("Purchase Receipt Line Split")]
[POReceiptLineSplitProjection(typeof (Select<POReceiptLineSplit, Where<POReceiptLineSplit.isReverse, Equal<False>>>), typeof (POReceiptLineSplit.isUnassigned), false)]
public class POReceiptLineSplit : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILSDetail,
  ILSMaster,
  IItemPlanMaster,
  IItemPlanPOReceiptSource,
  IItemPlanSource,
  ILotSerialTrackableLineSplit
{
  protected 
  #nullable disable
  string _ReceiptType;
  protected string _ReceiptNbr;
  protected int? _LineNbr;
  protected int? _SplitLineNbr;
  protected string _PONbr;
  protected string _LineType;
  protected DateTime? _ReceiptDate;
  protected short? _InvtMult;
  protected int? _InventoryID;
  protected int? _SiteID;
  protected int? _LocationID;
  protected int? _SubItemID;
  protected string _OrigPlanType;
  protected long? _PlanID;
  protected string _LotSerialNbr;
  protected string _LotSerClassID;
  protected string _AssignedNbr;
  protected DateTime? _ExpireDate;
  protected string _UOM;
  protected Decimal? _Qty;
  protected Decimal? _BaseQty;
  protected Decimal? _MaxTransferBaseQty;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDBDefault(typeof (POReceipt.receiptType))]
  [PXUIField(DisplayName = "Type")]
  public virtual string ReceiptType
  {
    get => this._ReceiptType;
    set => this._ReceiptType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (POReceipt.receiptNbr))]
  [PXUIField]
  [PXParent(typeof (POReceiptLineSplit.FK.ReceiptLine))]
  public virtual string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (POReceiptLine.lineNbr))]
  [PXUIField]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (POReceipt.lineCntr), DecrementOnDelete = false)]
  public virtual int? SplitLineNbr
  {
    get => this._SplitLineNbr;
    set => this._SplitLineNbr = value;
  }

  [PXDefault(typeof (Search<POReceiptLine.pONbr, Where<POReceiptLine.receiptType, Equal<Current<POReceiptLineSplit.receiptType>>, And<POReceiptLine.receiptNbr, Equal<Current<POReceiptLineSplit.receiptNbr>>, And<POReceiptLine.lineNbr, Equal<Current<POReceiptLineSplit.lineNbr>>>>>>))]
  [PXDBString(15, IsUnicode = true)]
  public string PONbr
  {
    get => this._PONbr;
    set => this._PONbr = value;
  }

  [PXDefault(typeof (Search<POReceiptLine.lineType, Where<POReceiptLine.receiptType, Equal<Current<POReceiptLineSplit.receiptType>>, And<POReceiptLine.receiptNbr, Equal<Current<POReceiptLineSplit.receiptNbr>>, And<POReceiptLine.lineNbr, Equal<Current<POReceiptLineSplit.lineNbr>>>>>>))]
  [PXDBString(2, IsFixed = true)]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [PXDBDate]
  [PXDefault(typeof (POReceipt.receiptDate))]
  [PXUIField]
  public virtual DateTime? ReceiptDate
  {
    get => this._ReceiptDate;
    set => this._ReceiptDate = value;
  }

  [PXDBShort]
  [PXDefault(typeof (POReceiptLine.invtMult))]
  public virtual short? InvtMult
  {
    get => this._InvtMult;
    set => this._InvtMult = value;
  }

  [Inventory(Visible = false, Enabled = false)]
  [PXDefault(typeof (POReceiptLine.inventoryID))]
  [PXForeignReference(typeof (POReceiptLineSplit.FK.InventoryItem))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  public bool? IsStockItem
  {
    get => new bool?(true);
    set
    {
    }
  }

  [PXDBBool]
  [PXDefault(typeof (POReceiptLine.isIntercompany))]
  public virtual bool? IsIntercompany { get; set; }

  [PXString]
  public string TranType
  {
    [PXDependsOnFields(new Type[] {typeof (POReceiptLineSplit.receiptType)})] get
    {
      return POReceiptType.GetINTranType(this._ReceiptType);
    }
  }

  public virtual DateTime? TranDate => this._ReceiptDate;

  [Site]
  [PXDefault(typeof (POReceiptLine.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [LocationAvail(typeof (POReceiptLineSplit.inventoryID), typeof (POReceiptLineSplit.subItemID), typeof (POReceiptLine.costCenterID), typeof (POReceiptLineSplit.siteID), typeof (POReceiptLineSplit.tranType), typeof (POReceiptLineSplit.invtMult), KeepEntry = false)]
  [PXDefault]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [SubItem(typeof (POReceiptLineSplit.inventoryID))]
  [PXDefault]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXSelector(typeof (Search<INPlanType.planType>), CacheGlobal = true)]
  [PXDefault(typeof (POReceiptLine.origPlanType))]
  public virtual string OrigPlanType
  {
    get => this._OrigPlanType;
    set => this._OrigPlanType = value;
  }

  [PXDBLong(IsImmutable = true)]
  public virtual long? PlanID
  {
    get => this._PlanID;
    set => this._PlanID = value;
  }

  [POLotSerialNbr(typeof (POReceiptLineSplit.inventoryID), typeof (POReceiptLineSplit.subItemID), typeof (POReceiptLineSplit.locationID), typeof (POReceiptLine.lotSerialNbr), typeof (POReceiptLine.costCenterID))]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXString(10, IsUnicode = true)]
  public virtual string LotSerClassID
  {
    get => this._LotSerClassID;
    set => this._LotSerClassID = value;
  }

  [PXString(30, IsUnicode = true)]
  public virtual string AssignedNbr
  {
    get => this._AssignedNbr;
    set => this._AssignedNbr = value;
  }

  /// <exclude />
  [PXString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Manufacturer Lot/Serial Nbr.", FieldClass = "LotSerialAttributes")]
  public string MfgLotSerialNbr { get; set; }

  [POExpireDate(typeof (POReceiptLineSplit.inventoryID))]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [INUnit(typeof (POReceiptLineSplit.inventoryID), DisplayName = "UOM", Enabled = false)]
  [PXDefault]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (POReceiptLineSplit.uOM), typeof (POReceiptLineSplit.baseQty), InventoryUnitType.BaseUnit, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
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

  [PXDBQuantity(typeof (POReceiptLineSplit.uOM), typeof (POReceiptLineSplit.baseReceivedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Received to Date", Enabled = false)]
  public Decimal? ReceivedQty { get; set; }

  [PXDBDecimal(6)]
  public virtual Decimal? BaseReceivedQty { get; set; }

  [PXDBQuantity(typeof (POReceiptLineSplit.uOM), typeof (POReceiptLineSplit.basePutAwayQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Putaway Qty.", Enabled = false, Visible = false)]
  public Decimal? PutAwayQty { get; set; }

  [PXDBDecimal(6)]
  public virtual Decimal? BasePutAwayQty { get; set; }

  [PXDBQuantity]
  public virtual Decimal? MaxTransferBaseQty
  {
    get => this._MaxTransferBaseQty;
    set => this._MaxTransferBaseQty = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsUnassigned { get; set; }

  [PXInt]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXInt]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
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

  /// <summary>
  /// A stored field indicating if the record is reversing an original PO Receipt
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsReverse { get; set; }

  public static POReceiptLineSplit FromPOReceiptLine(POReceiptLine item)
  {
    POReceiptLineSplit receiptLineSplit = new POReceiptLineSplit()
    {
      ReceiptType = item.ReceiptType,
      ReceiptNbr = item.ReceiptNbr,
      LineType = item.LineType,
      LineNbr = item.LineNbr,
      SplitLineNbr = new int?(1),
      InventoryID = item.InventoryID,
      SiteID = item.SiteID,
      SubItemID = item.SubItemID,
      LocationID = item.LocationID,
      LotSerialNbr = item.LotSerialNbr,
      ExpireDate = item.ExpireDate,
      Qty = item.Qty,
      UOM = item.UOM
    };
    receiptLineSplit.ExpireDate = item.ExpireDate;
    receiptLineSplit.BaseQty = item.BaseQty;
    receiptLineSplit.InvtMult = item.InvtMult;
    receiptLineSplit.OrigPlanType = item.OrigPlanType;
    receiptLineSplit.ProjectID = item.ProjectID;
    receiptLineSplit.TaskID = item.TaskID;
    return receiptLineSplit;
  }

  public class PK : 
    PrimaryKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.receiptType, POReceiptLineSplit.receiptNbr, POReceiptLineSplit.lineNbr, POReceiptLineSplit.splitLineNbr>
  {
    public static POReceiptLineSplit Find(
      PXGraph graph,
      string receiptType,
      string receiptNbr,
      int? lineNbr,
      int? splitLineNbr,
      PKFindOptions options = 0)
    {
      return (POReceiptLineSplit) PrimaryKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.receiptType, POReceiptLineSplit.receiptNbr, POReceiptLineSplit.lineNbr, POReceiptLineSplit.splitLineNbr>.FindBy(graph, (object) receiptType, (object) receiptNbr, (object) lineNbr, (object) splitLineNbr, options);
    }
  }

  public static class FK
  {
    public class Receipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.receiptType, POReceiptLineSplit.receiptNbr>
    {
    }

    public class ReceiptLine : 
      PrimaryKeyOf<POReceiptLine>.By<POReceiptLine.receiptType, POReceiptLine.receiptNbr, POReceiptLine.lineNbr>.ForeignKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.receiptType, POReceiptLineSplit.receiptNbr, POReceiptLineSplit.lineNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.siteID>
    {
    }

    public class SiteStatus : 
      PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.ForeignKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.inventoryID, POReceiptLineSplit.subItemID, POReceiptLineSplit.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.locationID>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.inventoryID, POReceiptLineSplit.subItemID, POReceiptLineSplit.siteID, POReceiptLineSplit.locationID>
    {
    }

    public class LotSerialStatus : 
      PrimaryKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.subItemID, INLotSerialStatus.siteID, INLotSerialStatus.locationID, INLotSerialStatus.lotSerialNbr>.ForeignKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.inventoryID, POReceiptLineSplit.subItemID, POReceiptLineSplit.siteID, POReceiptLineSplit.locationID, POReceiptLineSplit.lotSerialNbr>
    {
    }

    public class OriginalPlanType : 
      PrimaryKeyOf<INPlanType>.By<INPlanType.planType>.ForeignKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.origPlanType>
    {
    }

    public class ItemPlan : 
      PrimaryKeyOf<INItemPlan>.By<INItemPlan.planID>.ForeignKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.planID>
    {
    }

    public class ReceiptItemLotSerialAttributesHeader : 
      PrimaryKeyOf<POReceiptItemLotSerialAttributesHeader>.By<POReceiptItemLotSerialAttributesHeader.receiptType, POReceiptItemLotSerialAttributesHeader.receiptNbr, POReceiptItemLotSerialAttributesHeader.inventoryID, POReceiptItemLotSerialAttributesHeader.lotSerialNbr>.ForeignKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.receiptType, POReceiptLineSplit.receiptNbr, POReceiptLineSplit.inventoryID, POReceiptLineSplit.lotSerialNbr>
    {
    }
  }

  public abstract class receiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineSplit.receiptType>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineSplit.receiptNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineSplit.lineNbr>
  {
  }

  public abstract class splitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineSplit.splitLineNbr>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineSplit.pONbr>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineSplit.lineType>
  {
  }

  public abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLineSplit.receiptDate>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  POReceiptLineSplit.invtMult>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineSplit.inventoryID>
  {
  }

  public abstract class isIntercompany : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceiptLineSplit.isIntercompany>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineSplit.tranType>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineSplit.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineSplit.locationID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineSplit.subItemID>
  {
  }

  public abstract class origPlanType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineSplit.origPlanType>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POReceiptLineSplit.planID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineSplit.lotSerialNbr>
  {
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineSplit.lotSerClassID>
  {
  }

  public abstract class assignedNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineSplit.assignedNbr>
  {
  }

  public abstract class mfgLotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineSplit.mfgLotSerialNbr>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLineSplit.expireDate>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineSplit.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineSplit.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineSplit.baseQty>
  {
  }

  public abstract class receivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineSplit.receivedQty>
  {
  }

  public abstract class baseReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineSplit.baseReceivedQty>
  {
  }

  public abstract class putAwayQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineSplit.putAwayQty>
  {
  }

  public abstract class basePutAwayQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineSplit.basePutAwayQty>
  {
  }

  public abstract class maxTransferBaseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineSplit.maxTransferBaseQty>
  {
  }

  public abstract class isUnassigned : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLineSplit.isUnassigned>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineSplit.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineSplit.taskID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POReceiptLineSplit.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineSplit.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLineSplit.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POReceiptLineSplit.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineSplit.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLineSplit.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POReceiptLineSplit.Tstamp>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLineSplit.IsReverse" />
  public abstract class isReverse : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLineSplit.isReverse>
  {
  }
}

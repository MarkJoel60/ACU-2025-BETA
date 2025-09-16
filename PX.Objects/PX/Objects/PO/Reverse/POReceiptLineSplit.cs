// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.Reverse.POReceiptLineSplit
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
namespace PX.Objects.PO.Reverse;

/// <summary>
/// Is exact copy of POReceiptLineSplit except PXProjection Where clause.
/// </summary>
[POReceiptLineSplitProjection(typeof (Select<POReceiptLineSplit, Where<POReceiptLineSplit.isReverse, Equal<True>>>), typeof (POReceiptLineSplit.isUnassigned), false)]
[PXHidden]
public class POReceiptLineSplit : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILSMaster,
  IItemPlanMaster,
  IItemPlanPOReceiptSource,
  IItemPlanSource
{
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.PO.POReceipt.receiptType))]
  public virtual 
  #nullable disable
  string ReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (PX.Objects.PO.POReceipt.receiptNbr))]
  [PXParent(typeof (POReceiptLineSplit.FK.ReceiptLine))]
  public virtual string ReceiptNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (PX.Objects.PO.POReceiptLine.lineNbr))]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (PX.Objects.PO.POReceipt.lineCntr), DecrementOnDelete = false)]
  public virtual int? SplitLineNbr { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public string PONbr { get; set; }

  [PXDefault]
  [PXDBString(2, IsFixed = true)]
  public virtual string LineType { get; set; }

  [PXDBDate]
  [PXDefault]
  public virtual DateTime? ReceiptDate { get; set; }

  [PXDBShort]
  [PXDefault]
  public virtual short? InvtMult { get; set; }

  [StockItem(Visible = false)]
  [PXDefault(typeof (PX.Objects.PO.POReceiptLine.inventoryID))]
  [PXForeignReference(typeof (POReceiptLineSplit.FK.InventoryItem))]
  public virtual int? InventoryID { get; set; }

  [PXDBBool]
  [PXDefault(typeof (PX.Objects.PO.POReceiptLine.isIntercompany))]
  public virtual bool? IsIntercompany { get; set; }

  public string TranType
  {
    [PXDependsOnFields(new Type[] {typeof (POReceiptLineSplit.receiptType)})] get
    {
      return POReceiptType.GetINTranType(this.ReceiptType);
    }
  }

  public virtual DateTime? TranDate => this.ReceiptDate;

  [Site]
  [PXDefault(typeof (PX.Objects.PO.POReceiptLine.siteID))]
  public virtual int? SiteID { get; set; }

  [LocationAvail(typeof (POReceiptLineSplit.inventoryID), typeof (POReceiptLineSplit.subItemID), typeof (PX.Objects.PO.POReceiptLine.costCenterID), typeof (POReceiptLineSplit.siteID), typeof (POReceiptLineSplit.tranType), typeof (POReceiptLineSplit.invtMult), KeepEntry = false)]
  [PXDefault]
  public virtual int? LocationID { get; set; }

  [SubItem(typeof (POReceiptLineSplit.inventoryID))]
  [PXDefault]
  public virtual int? SubItemID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXSelector(typeof (Search<INPlanType.planType>), CacheGlobal = true)]
  [PXDefault(typeof (PX.Objects.PO.POReceiptLine.origPlanType))]
  public virtual string OrigPlanType { get; set; }

  [PXDBLong(IsImmutable = true)]
  public virtual long? PlanID { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXDefault("")]
  public virtual string LotSerialNbr { get; set; }

  [PXString(30, IsUnicode = true)]
  public virtual string AssignedNbr { get; set; }

  [POExpireDate(typeof (POReceiptLineSplit.inventoryID))]
  public virtual DateTime? ExpireDate { get; set; }

  [INUnit(typeof (POReceiptLineSplit.inventoryID), DisplayName = "UOM", Enabled = false)]
  [PXDefault]
  public virtual string UOM { get; set; }

  [PXDBQuantity(typeof (POReceiptLineSplit.uOM), typeof (POReceiptLineSplit.baseQty), InventoryUnitType.BaseUnit, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Qty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseQty { get; set; }

  [PXDBQuantity(typeof (POReceiptLineSplit.uOM), typeof (POReceiptLineSplit.baseReceivedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Received to Date", Enabled = false)]
  public Decimal? ReceivedQty { get; set; }

  [PXDBDecimal(6)]
  public virtual Decimal? BaseReceivedQty { get; set; }

  [PXDBQuantity(typeof (POReceiptLineSplit.uOM), typeof (POReceiptLineSplit.basePutAwayQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PutAwayQty { get; set; }

  [PXDBDecimal(6)]
  public virtual Decimal? BasePutAwayQty { get; set; }

  [PXDBQuantity]
  public virtual Decimal? MaxTransferBaseQty { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsUnassigned { get; set; }

  [PXInt]
  public virtual int? ProjectID { get; set; }

  [PXInt]
  public virtual int? TaskID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? IsReverse { get; set; }

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
      PrimaryKeyOf<PX.Objects.PO.POReceipt>.By<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>.ForeignKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.receiptType, POReceiptLineSplit.receiptNbr>
    {
    }

    public class ReceiptLine : 
      PrimaryKeyOf<PX.Objects.PO.POReceiptLine>.By<PX.Objects.PO.POReceiptLine.receiptType, PX.Objects.PO.POReceiptLine.receiptNbr, PX.Objects.PO.POReceiptLine.lineNbr>.ForeignKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.receiptType, POReceiptLineSplit.receiptNbr, POReceiptLineSplit.lineNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<POReceiptLineSplit>.By<POReceiptLineSplit.inventoryID>
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

  public abstract class assignedNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineSplit.assignedNbr>
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

  public abstract class isReverse : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLineSplit.isReverse>
  {
  }
}

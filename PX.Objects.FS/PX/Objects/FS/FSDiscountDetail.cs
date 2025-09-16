// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSDiscountDetail
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Discount;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSDiscountDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IDiscountDetail
{
  [PXDBIdentity]
  public virtual int? RecordID { get; set; }

  [PXDBUShort]
  public virtual ushort? LineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (Where<FSDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>, And<FSDiscountDetail.discountID, IsNotNull>>))]
  [PXUIField(DisplayName = "Skip Discount", Enabled = true)]
  public virtual bool? SkipDiscount { get; set; }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault]
  [ListField_PostDoc_EntityType.ListAtrribute]
  [PXUIField]
  public virtual 
  #nullable disable
  string EntityType { get; set; }

  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type", Enabled = false)]
  public virtual string SrvOrdType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (FSAppointment.refNbr))]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  public virtual string RefNbr { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Discount Code")]
  [PXUIEnabled(typeof (Where<FSDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>>))]
  [PXSelector(typeof (Search<ARDiscount.discountID, Where<ARDiscount.type, NotEqual<DiscountType.LineDiscount>>>))]
  public virtual string DiscountID { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Sequence ID")]
  [PXUIEnabled(typeof (Where<FSDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>>))]
  [PXSelector(typeof (Search<DiscountSequence.discountSequenceID, Where<DiscountSequence.isActive, Equal<True>, And<DiscountSequence.discountID, Equal<Current<FSDiscountDetail.discountID>>>>>))]
  public virtual string DiscountSequenceID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [DiscountType.List]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public virtual string Type { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  public virtual short? ManualOrder { get; set; }

  [PXDBLong]
  public virtual long? CuryInfoID { get; set; }

  [PXDBDecimal(4)]
  public virtual Decimal? DiscountableAmt { get; set; }

  [PXDBCurrency(typeof (FSDiscountDetail.curyInfoID), typeof (FSDiscountDetail.discountableAmt))]
  [PXUIField(DisplayName = "Discountable Amt.", Enabled = false)]
  public virtual Decimal? CuryDiscountableAmt { get; set; }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Discountable Qty.", Enabled = false)]
  public virtual Decimal? DiscountableQty { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscountAmt { get; set; }

  [PXDBCurrency(typeof (FSDiscountDetail.curyInfoID), typeof (FSDiscountDetail.discountAmt))]
  [PXUIEnabled(typeof (Where<FSDiscountDetail.type, Equal<DiscountType.DocumentDiscount>, Or<FSDiscountDetail.type, Equal<DiscountType.ExternalDocumentDiscount>>>))]
  [PXUIField(DisplayName = "Discount Amt.")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscountAmt { get; set; }

  [PXDBDecimal(6)]
  [PXUIEnabled(typeof (Where<FSDiscountDetail.type, Equal<DiscountType.DocumentDiscount>, Or<FSDiscountDetail.type, Equal<DiscountType.ExternalDocumentDiscount>>>))]
  [PXUIField(DisplayName = "Discount Percent")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscountPct { get; set; }

  [Inventory(DisplayName = "Free Item", Enabled = false)]
  [PXForeignReference(typeof (FSDiscountDetail.FK.FreeItem))]
  public virtual int? FreeItemID { get; set; }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Free Item Qty.", Enabled = false)]
  public virtual Decimal? FreeItemQty { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Discount", Enabled = false)]
  public virtual bool? IsManual { get; set; }

  [PXBool]
  [PXFormula(typeof (False))]
  public virtual bool? IsOrigDocDiscount { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "External Discount Code")]
  public virtual string ExtDiscCode { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  [PXDefault(typeof (Search<DiscountSequence.description, Where<DiscountSequence.discountID, Equal<Current<FSDiscountDetail.discountID>>, And<DiscountSequence.discountSequenceID, Equal<Current<FSDiscountDetail.discountSequenceID>>>>>))]
  public virtual string Description { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

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

  public class PK : 
    PrimaryKeyOf<FSDiscountDetail>.By<FSDiscountDetail.srvOrdType, FSDiscountDetail.refNbr, FSDiscountDetail.recordID>
  {
    public static FSDiscountDetail Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? recordID,
      PKFindOptions options = 0)
    {
      return (FSDiscountDetail) PrimaryKeyOf<FSDiscountDetail>.By<FSDiscountDetail.srvOrdType, FSDiscountDetail.refNbr, FSDiscountDetail.recordID>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) recordID, options);
    }
  }

  public static class FK
  {
    public class FreeItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSDiscountDetail>.By<FSDiscountDetail.freeItemID>
    {
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSDiscountDetail.recordID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSDiscountDetail.lineNbr>
  {
  }

  public abstract class skipDiscount : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSDiscountDetail.skipDiscount>
  {
  }

  public abstract class entityType : ListField_PostDoc_EntityType
  {
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSDiscountDetail.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSDiscountDetail.refNbr>
  {
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSDiscountDetail.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSDiscountDetail.discountSequenceID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSDiscountDetail.type>
  {
  }

  public abstract class manualOrder : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSDiscountDetail.manualOrder>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSDiscountDetail.curyInfoID>
  {
  }

  public abstract class discountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSDiscountDetail.discountableAmt>
  {
  }

  public abstract class curyDiscountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSDiscountDetail.curyDiscountableAmt>
  {
  }

  public abstract class discountableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSDiscountDetail.discountableQty>
  {
  }

  public abstract class discountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSDiscountDetail.discountAmt>
  {
  }

  public abstract class curyDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSDiscountDetail.curyDiscountAmt>
  {
  }

  public abstract class discountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSDiscountDetail.discountPct>
  {
  }

  public abstract class freeItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSDiscountDetail.freeItemID>
  {
  }

  public abstract class freeItemQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSDiscountDetail.freeItemQty>
  {
  }

  public abstract class isManual : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSDiscountDetail.isManual>
  {
  }

  public abstract class isOrigDocDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSDiscountDetail.isOrigDocDiscount>
  {
  }

  public abstract class extDiscCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSDiscountDetail.extDiscCode>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSDiscountDetail.description>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSDiscountDetail.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSDiscountDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSDiscountDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSDiscountDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSDiscountDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSDiscountDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSDiscountDetail.lastModifiedDateTime>
  {
  }
}

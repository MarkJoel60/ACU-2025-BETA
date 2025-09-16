// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentDiscountDetail
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

[PXProjection(typeof (Select<FSDiscountDetail, Where<FSDiscountDetail.entityType, Equal<ListField_PostDoc_EntityType.Appointment>>>), Persistent = true)]
[PXBreakInheritance]
[Serializable]
public class FSAppointmentDiscountDetail : FSDiscountDetail, IDiscountDetail
{
  [PXDBIdentity]
  public override int? RecordID { get; set; }

  [PXDBUShort]
  [PXLineNbr(typeof (FSAppointment))]
  public override ushort? LineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (Where<FSAppointmentDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>, And<FSAppointmentDiscountDetail.discountID, IsNotNull>>))]
  [PXUIField(DisplayName = "Skip Discount", Enabled = true)]
  public override bool? SkipDiscount { get; set; }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault]
  [ListField_PostDoc_EntityType.ListAtrribute]
  [PXUIField]
  public override 
  #nullable disable
  string EntityType { get; set; }

  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (FSAppointment.srvOrdType))]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  public override string SrvOrdType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (FSAppointment.refNbr))]
  [PXParent(typeof (Select<FSAppointment, Where<FSAppointment.srvOrdType, Equal<Current<FSAppointmentDiscountDetail.srvOrdType>>, And<FSAppointment.refNbr, Equal<Current<FSAppointmentDiscountDetail.refNbr>>>>>))]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  public override string RefNbr { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Discount Code")]
  [PXUIEnabled(typeof (Where<FSAppointmentDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>>))]
  [PXSelector(typeof (Search<ARDiscount.discountID, Where<ARDiscount.type, NotEqual<DiscountType.LineDiscount>>>))]
  public override string DiscountID { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Sequence ID")]
  [PXUIEnabled(typeof (Where<FSAppointmentDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>>))]
  [PXSelector(typeof (Search<DiscountSequence.discountSequenceID, Where<DiscountSequence.isActive, Equal<True>, And<DiscountSequence.discountID, Equal<Current<FSAppointmentDiscountDetail.discountID>>>>>))]
  public override string DiscountSequenceID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [DiscountType.List]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public override string Type { get; set; }

  [PXDBShort]
  [PXLineNbr(typeof (FSAppointment))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  public override short? ManualOrder { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (FSAppointment.curyInfoID))]
  public override long? CuryInfoID { get; set; }

  [PXDBDecimal(4)]
  public override Decimal? DiscountableAmt { get; set; }

  [PXDBCurrency(typeof (FSAppointmentDiscountDetail.curyInfoID), typeof (FSAppointmentDiscountDetail.discountableAmt))]
  [PXUIField(DisplayName = "Discountable Amt.", Enabled = false)]
  public override Decimal? CuryDiscountableAmt { get; set; }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Discountable Qty.", Enabled = false)]
  public override Decimal? DiscountableQty { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? DiscountAmt { get; set; }

  [PXDBCurrency(typeof (FSAppointmentDiscountDetail.curyInfoID), typeof (FSAppointmentDiscountDetail.discountAmt))]
  [PXUIEnabled(typeof (Where<FSAppointmentDiscountDetail.type, Equal<DiscountType.DocumentDiscount>, Or<FSAppointmentDiscountDetail.type, Equal<DiscountType.ExternalDocumentDiscount>>>))]
  [PXUIField(DisplayName = "Discount Amt.")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? CuryDiscountAmt { get; set; }

  [PXDBDecimal(6)]
  [PXUIEnabled(typeof (Where<FSAppointmentDiscountDetail.type, Equal<DiscountType.DocumentDiscount>, Or<FSAppointmentDiscountDetail.type, Equal<DiscountType.ExternalDocumentDiscount>>>))]
  [PXUIField(DisplayName = "Discount Percent")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? DiscountPct { get; set; }

  [Inventory(DisplayName = "Free Item", Enabled = false)]
  [PXForeignReference(typeof (FSAppointmentDiscountDetail.FK.FreeItem))]
  public override int? FreeItemID { get; set; }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Free Item Qty.", Enabled = false)]
  public override Decimal? FreeItemQty { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Discount", Enabled = false)]
  public override bool? IsManual { get; set; }

  [PXBool]
  [PXFormula(typeof (False))]
  public override bool? IsOrigDocDiscount { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "External Discount Code")]
  public override string ExtDiscCode { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  [PXDefault(typeof (Search<DiscountSequence.description, Where<DiscountSequence.discountID, Equal<Current<FSAppointmentDiscountDetail.discountID>>, And<DiscountSequence.discountSequenceID, Equal<Current<FSAppointmentDiscountDetail.discountSequenceID>>>>>))]
  public override string Description { get; set; }

  [PXDBTimestamp]
  public override byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public override Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public override string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public override DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public override Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public override string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public override DateTime? LastModifiedDateTime { get; set; }

  public new class PK : 
    PrimaryKeyOf<FSAppointmentDiscountDetail>.By<FSAppointmentDiscountDetail.srvOrdType, FSAppointmentDiscountDetail.refNbr, FSAppointmentDiscountDetail.recordID>
  {
    public static FSAppointmentDiscountDetail Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? recordID,
      PKFindOptions options = 0)
    {
      return (FSAppointmentDiscountDetail) PrimaryKeyOf<FSAppointmentDiscountDetail>.By<FSAppointmentDiscountDetail.srvOrdType, FSAppointmentDiscountDetail.refNbr, FSAppointmentDiscountDetail.recordID>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) recordID, options);
    }
  }

  public new static class FK
  {
    public class FreeItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSAppointmentDiscountDetail>.By<FSAppointmentDiscountDetail.freeItemID>
    {
    }
  }

  public new abstract class recordID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.recordID>
  {
  }

  public new abstract class lineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.lineNbr>
  {
  }

  public new abstract class skipDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.skipDiscount>
  {
  }

  public new abstract class entityType : ListField_PostDoc_EntityType
  {
  }

  public new abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.srvOrdType>
  {
  }

  public new abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.refNbr>
  {
  }

  public new abstract class discountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.discountID>
  {
  }

  public new abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.discountSequenceID>
  {
  }

  public new abstract class type : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.type>
  {
  }

  public new abstract class manualOrder : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.manualOrder>
  {
  }

  public new abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.curyInfoID>
  {
  }

  public new abstract class discountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.discountableAmt>
  {
  }

  public new abstract class curyDiscountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.curyDiscountableAmt>
  {
  }

  public new abstract class discountableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.discountableQty>
  {
  }

  public new abstract class discountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.discountAmt>
  {
  }

  public new abstract class curyDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.curyDiscountAmt>
  {
  }

  public new abstract class discountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.discountPct>
  {
  }

  public new abstract class freeItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.freeItemID>
  {
  }

  public new abstract class freeItemQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.freeItemQty>
  {
  }

  public new abstract class isManual : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.isManual>
  {
  }

  public new abstract class isOrigDocDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.isOrigDocDiscount>
  {
  }

  public new abstract class extDiscCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.extDiscCode>
  {
  }

  public new abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.description>
  {
  }

  public new abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.Tstamp>
  {
  }

  public new abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.createdDateTime>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentDiscountDetail.lastModifiedDateTime>
  {
  }
}

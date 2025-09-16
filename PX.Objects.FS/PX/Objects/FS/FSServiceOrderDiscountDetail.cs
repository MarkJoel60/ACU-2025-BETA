// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSServiceOrderDiscountDetail
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

[PXProjection(typeof (Select<FSDiscountDetail, Where<FSDiscountDetail.entityType, Equal<ListField_PostDoc_EntityType.Service_Order>>>), Persistent = true)]
[PXBreakInheritance]
[Serializable]
public class FSServiceOrderDiscountDetail : FSDiscountDetail, IDiscountDetail
{
  [PXDBIdentity]
  public override int? RecordID { get; set; }

  [PXDBUShort]
  [PXLineNbr(typeof (FSServiceOrder))]
  public override ushort? LineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (Where<FSServiceOrderDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>, And<FSServiceOrderDiscountDetail.discountID, IsNotNull>>))]
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
  [PXDBDefault(typeof (FSServiceOrder.srvOrdType))]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  public override string SrvOrdType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (FSServiceOrder.refNbr))]
  [PXParent(typeof (Select<FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Current<FSServiceOrderDiscountDetail.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Current<FSServiceOrderDiscountDetail.refNbr>>>>>))]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  public override string RefNbr { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Discount Code")]
  [PXUIEnabled(typeof (Where<FSServiceOrderDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>>))]
  [PXSelector(typeof (Search<ARDiscount.discountID, Where<ARDiscount.type, NotEqual<DiscountType.LineDiscount>>>))]
  public override string DiscountID { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Sequence ID")]
  [PXUIEnabled(typeof (Where<FSServiceOrderDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>>))]
  [PXSelector(typeof (Search<DiscountSequence.discountSequenceID, Where<DiscountSequence.isActive, Equal<True>, And<DiscountSequence.discountID, Equal<Current<FSServiceOrderDiscountDetail.discountID>>>>>))]
  public override string DiscountSequenceID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [DiscountType.List]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public override string Type { get; set; }

  [PXDBShort]
  [PXLineNbr(typeof (FSServiceOrder))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  public override short? ManualOrder { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (FSServiceOrder.curyInfoID))]
  public override long? CuryInfoID { get; set; }

  [PXDBDecimal(4)]
  public override Decimal? DiscountableAmt { get; set; }

  [PXDBCurrency(typeof (FSServiceOrderDiscountDetail.curyInfoID), typeof (FSServiceOrderDiscountDetail.discountableAmt))]
  [PXUIField(DisplayName = "Discountable Amt.", Enabled = false)]
  public override Decimal? CuryDiscountableAmt { get; set; }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Discountable Qty.", Enabled = false)]
  public override Decimal? DiscountableQty { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? DiscountAmt { get; set; }

  [PXDBCurrency(typeof (FSServiceOrderDiscountDetail.curyInfoID), typeof (FSServiceOrderDiscountDetail.discountAmt))]
  [PXUIEnabled(typeof (Where<FSServiceOrderDiscountDetail.type, Equal<DiscountType.DocumentDiscount>, Or<FSServiceOrderDiscountDetail.type, Equal<DiscountType.ExternalDocumentDiscount>>>))]
  [PXUIField(DisplayName = "Discount Amt.")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? CuryDiscountAmt { get; set; }

  [PXDBDecimal(6)]
  [PXUIEnabled(typeof (Where<FSServiceOrderDiscountDetail.type, Equal<DiscountType.DocumentDiscount>, Or<FSServiceOrderDiscountDetail.type, Equal<DiscountType.ExternalDocumentDiscount>>>))]
  [PXUIField(DisplayName = "Discount Percent")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? DiscountPct { get; set; }

  [Inventory(DisplayName = "Free Item", Enabled = false)]
  [PXForeignReference(typeof (FSServiceOrderDiscountDetail.FK.FreeItem))]
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
  [PXDefault(typeof (Search<DiscountSequence.description, Where<DiscountSequence.discountID, Equal<Current<FSServiceOrderDiscountDetail.discountID>>, And<DiscountSequence.discountSequenceID, Equal<Current<FSServiceOrderDiscountDetail.discountSequenceID>>>>>))]
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
    PrimaryKeyOf<FSServiceOrderDiscountDetail>.By<FSServiceOrderDiscountDetail.srvOrdType, FSServiceOrderDiscountDetail.refNbr, FSServiceOrderDiscountDetail.recordID>
  {
    public static FSServiceOrderDiscountDetail Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? recordID,
      PKFindOptions options = 0)
    {
      return (FSServiceOrderDiscountDetail) PrimaryKeyOf<FSServiceOrderDiscountDetail>.By<FSServiceOrderDiscountDetail.srvOrdType, FSServiceOrderDiscountDetail.refNbr, FSServiceOrderDiscountDetail.recordID>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) recordID, options);
    }
  }

  public new static class FK
  {
    public class FreeItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSServiceOrderDiscountDetail>.By<FSServiceOrderDiscountDetail.freeItemID>
    {
    }
  }

  public new abstract class recordID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.recordID>
  {
  }

  public new abstract class lineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.lineNbr>
  {
  }

  public new abstract class skipDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.skipDiscount>
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
    FSServiceOrderDiscountDetail.srvOrdType>
  {
  }

  public new abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.refNbr>
  {
  }

  public new abstract class discountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.discountID>
  {
  }

  public new abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.discountSequenceID>
  {
  }

  public new abstract class type : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.type>
  {
  }

  public new abstract class manualOrder : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.manualOrder>
  {
  }

  public new abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.curyInfoID>
  {
  }

  public new abstract class discountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.discountableAmt>
  {
  }

  public new abstract class curyDiscountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.curyDiscountableAmt>
  {
  }

  public new abstract class discountableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.discountableQty>
  {
  }

  public new abstract class discountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.discountAmt>
  {
  }

  public new abstract class curyDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.curyDiscountAmt>
  {
  }

  public new abstract class discountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.discountPct>
  {
  }

  public new abstract class freeItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.freeItemID>
  {
  }

  public new abstract class freeItemQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.freeItemQty>
  {
  }

  public new abstract class isManual : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.isManual>
  {
  }

  public new abstract class isOrigDocDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.isOrigDocDiscount>
  {
  }

  public new abstract class extDiscCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.extDiscCode>
  {
  }

  public new abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.description>
  {
  }

  public new abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.Tstamp>
  {
  }

  public new abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.createdDateTime>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSServiceOrderDiscountDetail.lastModifiedDateTime>
  {
  }
}

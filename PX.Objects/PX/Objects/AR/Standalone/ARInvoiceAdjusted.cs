// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Standalone.ARInvoiceAdjusted
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR.Standalone;

[PXHidden]
[PXProjection(typeof (SelectFrom<ARInvoice, TypeArrayOf<IFbqlJoin>.Empty>.InnerJoin<ARRegister>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoice.refNbr, Equal<ARRegister.refNbr>>>>>.And<BqlOperand<ARInvoice.docType, IBqlString>.IsEqual<ARRegister.docType>>>), Persistent = true)]
[PXBreakInheritance]
public class ARInvoiceAdjusted : ARRegister
{
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (ARInvoice.docType))]
  [PXDefault]
  public override 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (ARInvoice.refNbr))]
  [PXDefault]
  public override string RefNbr { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (ARRegister.docType))]
  [PXDefault]
  public virtual string ARRegisterDocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (ARRegister.refNbr))]
  [PXDefault]
  public virtual string ARRegisterRefNbr { get; set; }

  [PXNote(BqlField = typeof (PX.Objects.AR.ARRegister.noteID))]
  public override Guid? NoteID { get; set; }

  [Branch(null, null, true, true, true, BqlField = typeof (ARRegister.branchID))]
  public override int? BranchID { get; set; }

  [PXDBCurrency(typeof (ARInvoiceAdjusted.curyInfoID), typeof (ARInvoiceAdjusted.paymentTotal), BqlField = typeof (ARInvoice.curyPaymentTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryPaymentTotal { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (ARInvoice.paymentTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PaymentTotal { get; set; }

  [PXDBCurrency(typeof (ARInvoiceAdjusted.curyInfoID), typeof (ARInvoiceAdjusted.balanceWOTotal), BqlField = typeof (ARInvoice.curyBalanceWOTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryBalanceWOTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARInvoice.balanceWOTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BalanceWOTotal { get; set; }

  [PXDBLong(BqlField = typeof (ARRegister.curyInfoID))]
  [CurrencyInfo]
  public override long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (ARInvoiceAdjusted.curyInfoID), typeof (ARInvoiceAdjusted.unpaidBalance), BqlField = typeof (ARInvoice.curyUnpaidBalance))]
  [PXFormula(typeof (Sub<ARInvoiceAdjusted.curyOrigDocAmt, Add<ARInvoiceAdjusted.curyPaymentTotal, Add<ARInvoiceAdjusted.curyDiscAppliedAmt, ARInvoiceAdjusted.curyBalanceWOTotal>>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? CuryUnpaidBalance { get; set; }

  [PXDBBaseCury(typeof (ARInvoiceAdjusted.branchID), BqlField = typeof (ARInvoice.unpaidBalance))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? UnpaidBalance { get; set; }

  [PXDBCurrency(typeof (ARInvoiceAdjusted.curyInfoID), typeof (ARInvoiceAdjusted.discAppliedAmt), BqlField = typeof (ARInvoice.curyDiscAppliedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscAppliedAmt { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (ARInvoice.discAppliedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAppliedAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (PX.Objects.AR.ARRegister.origDocAmt), BqlField = typeof (PX.Objects.AR.ARRegister.curyOrigDocAmt))]
  public override Decimal? CuryOrigDocAmt { get; set; }

  [PXDBBaseCury(typeof (ARInvoiceAdjusted.branchID), BqlField = typeof (PX.Objects.AR.ARRegister.origDocAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? OrigDocAmt { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.ARRegister.isUnderCorrection))]
  [PXDefault(false)]
  public override bool? IsUnderCorrection { get; set; }

  [PXDBCreatedByID(BqlField = typeof (PX.Objects.AR.ARRegister.createdByID))]
  public override Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID(BqlField = typeof (PX.Objects.AR.ARRegister.createdByScreenID))]
  public override string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime(BqlField = typeof (PX.Objects.AR.ARRegister.createdDateTime))]
  public override DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (PX.Objects.AR.ARRegister.lastModifiedByID))]
  public override Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (PX.Objects.AR.ARRegister.lastModifiedByScreenID))]
  public override string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (PX.Objects.AR.ARRegister.lastModifiedDateTime))]
  public override DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp(BqlField = typeof (PX.Objects.AR.ARRegister.Tstamp))]
  public override byte[] tstamp { get; set; }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoiceAdjusted.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoiceAdjusted.refNbr>
  {
  }

  public abstract class aRRegisterDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoiceAdjusted.docType>
  {
  }

  public abstract class aRRegisterRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoiceAdjusted.refNbr>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARInvoiceAdjusted.noteID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoiceAdjusted.branchID>
  {
  }

  public abstract class curyPaymentTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceAdjusted.curyPaymentTotal>
  {
  }

  public abstract class paymentTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceAdjusted.paymentTotal>
  {
  }

  public abstract class curyBalanceWOTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceAdjusted.curyBalanceWOTotal>
  {
  }

  public abstract class balanceWOTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceAdjusted.balanceWOTotal>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARInvoiceAdjusted.curyInfoID>
  {
  }

  public abstract class curyUnpaidBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceAdjusted.curyUnpaidBalance>
  {
  }

  public abstract class unpaidBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceAdjusted.unpaidBalance>
  {
  }

  public abstract class curyDiscAppliedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceAdjusted.curyDiscAppliedAmt>
  {
  }

  public abstract class discAppliedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceAdjusted.discAppliedAmt>
  {
  }

  public new abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceAdjusted.curyOrigDocAmt>
  {
  }

  public new abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceAdjusted.origDocAmt>
  {
  }

  public new abstract class isUnderCorrection : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoiceAdjusted.isUnderCorrection>
  {
  }

  public new abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARInvoiceAdjusted.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoiceAdjusted.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARInvoiceAdjusted.createdDateTime>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARInvoiceAdjusted.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoiceAdjusted.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARInvoiceAdjusted.lastModifiedDateTime>
  {
  }

  public new abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARInvoiceAdjusted.Tstamp>
  {
  }
}

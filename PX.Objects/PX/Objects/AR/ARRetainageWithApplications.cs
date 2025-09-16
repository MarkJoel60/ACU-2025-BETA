// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARRetainageWithApplications
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// All retainage documents (released and not released) related to the specified origin document
/// with its released retainage and paid amount (accumulated from ARTranPost DAC)
/// </summary>
[PXCacheName("AR Retainage documents with released/paid amount")]
[PXProjection(typeof (Select5<ARRetainageInvoice, LeftJoin<ARTran, On<ARTran.refNbr, Equal<ARRetainageInvoice.refNbr>, And<ARTran.tranType, Equal<ARRetainageInvoice.docType>, And<ARTran.lineNbr, Equal<IIf<Where<ARRetainageInvoice.paymentsByLinesAllowed, Equal<True>>, ARTran.lineNbr, int1>>>>>, InnerJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<ARRetainageInvoice.docType>, And<ARInvoice.refNbr, Equal<ARRetainageInvoice.refNbr>>>, LeftJoin<ARTranPost, On<ARTranPost.sourceRefNbr, Equal<ARRetainageInvoice.refNbr>, And<ARTranPost.sourceDocType, Equal<ARRetainageInvoice.docType>, And<ARTranPost.type, Equal<ARTranPost.type.retainage>, And<ARTranPost.refNbr, Equal<ARTran.origRefNbr>, And<ARTranPost.docType, Equal<ARTran.origDocType>>>>>>, LeftJoin<ARTranPostAlias, On<ARTranPostAlias.refNbr, Equal<ARTran.refNbr>, And<ARTranPostAlias.docType, Equal<ARTran.tranType>, And<ARTranPostAlias.lineNbr, Equal<IIf<Where<ARRetainageInvoice.paymentsByLinesAllowed, Equal<True>>, ARTran.lineNbr, int0>>, And<Where<ARTranPostAlias.type, Equal<ARTranPost.type.application>, Or<ARTranPostAlias.type, Equal<ARTranPost.type.adjustment>>>>>>>>>>>, Where<ARRetainageInvoice.isRetainageDocument, Equal<True>>, Aggregate<GroupBy<ARRetainageInvoice.docType, GroupBy<ARRetainageInvoice.refNbr, GroupBy<ARTran.origDocType, GroupBy<ARTran.origRefNbr, Sum<ARTranPostAlias.curyAmt, Sum<ARTranPost.amt, Sum<ARTranPostAlias.curyDiscAmt, Sum<ARTranPost.discAmt, Sum<ARTranPostAlias.curyWOAmt, Sum<ARTranPost.wOAmt>>>>>>>>>>>>))]
[Serializable]
public class ARRetainageWithApplications : ARRetainageInvoice
{
  protected 
  #nullable disable
  string _PaymentMethodID;
  protected string _InvoiceNbr;

  /// <summary>
  /// The type of the original retainage document.
  /// Depends on the <see cref="P:PX.Objects.AR.ARTran.OrigDocType" /> field.
  /// </summary>
  [PXDBString(IsKey = true, BqlField = typeof (ARTran.origDocType))]
  [PXUIField(DisplayName = "Doc. Type")]
  public override string OrigDocType { get; set; }

  /// <summary>
  /// The reference number of the original retainage document.
  /// Depends on the <see cref="P:PX.Objects.AR.ARTran.OrigRefNbr" /> field.
  /// </summary>
  [PXDBString(IsKey = true, BqlField = typeof (ARTran.origRefNbr))]
  [PXUIField]
  public override string OrigRefNbr { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">CurrencyInfo</see> object associated with the document.
  /// </summary>
  [PXDBLong(BqlField = typeof (ARTranPost.curyInfoID))]
  [CurrencyInfo(typeof (ARRegister.curyInfoID))]
  public override long? CuryInfoID { get; set; }

  /// <summary>
  /// Indicates the sign of the document's impact on AR balance.
  /// Depends on the <see cref="P:PX.Objects.AR.ARTranPost.GLSign" /> field.
  /// </summary>
  [PXDBShort(BqlField = typeof (ARTranPost.glSign))]
  public virtual short? GlSign { get; set; }

  /// <summary>
  /// Indicates the sign of the document's impact on AR balance.
  /// Depends on the <see cref="P:PX.Objects.AR.ARTranPost.BalanceSign" /> field.
  /// </summary>
  [PXDBShort(BqlField = typeof (ARTranPost.balanceSign))]
  public virtual short? BalanceSign { get; set; }

  /// <summary>
  /// Retainage amount. Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// Depends on the <see cref="P:PX.Objects.AR.ARTranPost.RetainageAmt" /> field.
  /// </summary>
  [PXDBBaseCury(BqlField = typeof (ARTranPost.retainageAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainageAmt { get; set; }

  /// <summary>
  /// Retainage amount.
  /// Depends on the <see cref="P:PX.Objects.AR.ARTranPost.CuryRetainageAmt" /> field.
  /// </summary>
  [PXDBCurrency(typeof (ARRetainageWithApplications.curyInfoID), typeof (ARRetainageWithApplications.retainageAmt), BaseCalc = false, BqlField = typeof (ARTranPost.curyRetainageAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRetainageAmt { get; set; }

  /// <summary>
  /// Paid retainage. Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// Depends on the <see cref="P:PX.Objects.AR.ARTranPost.Amt" /> field.
  /// </summary>
  [PXDBBaseCury(BqlField = typeof (ARTranPost.amt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Amt { get; set; }

  /// <summary>
  /// Paid retainage.
  /// Depends on the <see cref="P:PX.Objects.AR.ARTranPost.CuryAmt" /> field.
  /// </summary>
  [PXDBCurrency(typeof (ARTranPost.curyInfoID), typeof (ARTranPost.amt), BaseCalc = false, BqlField = typeof (ARTranPostAlias.curyAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Paid Retainage")]
  public virtual Decimal? CuryAmt { get; set; }

  /// <summary>
  /// Paid retainage discount. Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// Depends on the <see cref="P:PX.Objects.AR.ARTranPost.Amt" /> field.
  /// </summary>
  [PXDBBaseCury(BqlField = typeof (ARTranPost.discAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAmt { get; set; }

  /// <summary>
  /// Paid retainage discount.
  /// Depends on the <see cref="P:PX.Objects.AR.ARTranPost.CuryDiscAmt" /> field.
  /// </summary>
  [PXDBCurrency(typeof (ARTranPost.curyInfoID), typeof (ARTranPost.discAmt), BaseCalc = false, BqlField = typeof (ARTranPostAlias.curyDiscAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscAmt { get; set; }

  /// <summary>
  /// Paid retainage write off. Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// Depends on the <see cref="P:PX.Objects.AR.ARTranPost.Amt" /> field.
  /// </summary>
  [PXDBBaseCury(BqlField = typeof (ARTranPost.wOAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? WOAmt { get; set; }

  /// <summary>
  /// Paid retainage write off.
  /// Depends on the <see cref="P:PX.Objects.AR.ARTranPost.CuryWOAmt" /> field.
  /// </summary>
  [PXDBCurrency(typeof (ARTranPost.curyInfoID), typeof (ARTranPost.wOAmt), BaseCalc = false, BqlField = typeof (ARTranPostAlias.curyWOAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryWOAmt { get; set; }

  /// <summary>
  /// The identifier of the payment method that is used for the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARInvoice.PaymentMethodID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true, BqlField = typeof (ARInvoice.paymentMethodID))]
  [PXUIField(DisplayName = "Payment Method")]
  public virtual string PaymentMethodID { get; set; }

  /// <summary>
  /// The original reference number or ID assigned by the customer to the customer document.
  /// Depends on the <see cref="P:PX.Objects.AR.ARInvoice.InvoiceNbr" /> field.
  /// </summary>
  [PXDBString(40, IsUnicode = true, BqlField = typeof (ARInvoice.invoiceNbr))]
  [PXUIField]
  public virtual string InvoiceNbr { get; set; }

  /// <summary>
  /// Released Retainage.
  /// Given in the currency of the document.
  /// </summary>
  [PXFormula(typeof (IIf<Where<ARRetainageWithApplications.curyRetainageAmt, IsNull>, decimal0, Mult<ARRetainageWithApplications.curyRetainageAmt, Mult<ARRetainageWithApplications.glSign, Minus<ARRetainageWithApplications.balanceSign>>>>))]
  [PXDecimal]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Released Retainage")]
  public virtual Decimal? CuryRetainageReleasedAmt { get; set; }

  /// <summary>
  /// Paid Retainage with the document sign.
  /// Given in the currency of the document.
  /// </summary>
  [PXFormula(typeof (IIf<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRetainageWithApplications.curyAmt, IsNull>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRetainageWithApplications.curyDiscAmt, IsNull>>>>.Or<BqlOperand<ARRetainageWithApplications.curyWOAmt, IBqlDecimal>.IsNull>>>, decimal0, Mult<BqlOperand<ARRetainageWithApplications.curyAmt, IBqlDecimal>.Add<BqlOperand<ARRetainageWithApplications.curyDiscAmt, IBqlDecimal>.Add<ARRetainageWithApplications.curyWOAmt>>, Mult<ARRetainageWithApplications.glSign, ARRetainageWithApplications.balanceSign>>>))]
  [PXDecimal]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Paid Retainage")]
  public virtual Decimal? CuryRetainagePaidAmt { get; set; }

  /// <summary>
  /// Total amount of the original retainage document. Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBBaseCury(BqlField = typeof (ARRegister.origDocAmt))]
  [PXUIField]
  public override Decimal? OrigDocAmt { get; set; }

  /// <summary>
  /// Total amount of the original retainage document.
  /// Given in the currency of the document.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARRetainageWithApplications.curyInfoID), typeof (ARRetainageWithApplications.origDocAmt), BqlField = typeof (ARRegister.curyOrigDocAmt))]
  [PXUIField]
  public override Decimal? CuryOrigDocAmt { get; set; }

  public new abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRetainageWithApplications.origDocType>
  {
  }

  public new abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRetainageWithApplications.origRefNbr>
  {
  }

  public new abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    ARRetainageWithApplications.curyInfoID>
  {
  }

  public abstract class glSign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARRetainageWithApplications.glSign>
  {
  }

  public abstract class balanceSign : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    ARRetainageWithApplications.balanceSign>
  {
  }

  public abstract class retainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRetainageWithApplications.retainageAmt>
  {
  }

  public abstract class curyRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRetainageWithApplications.curyRetainageAmt>
  {
  }

  public abstract class amt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRetainageWithApplications.amt>
  {
  }

  public abstract class curyAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRetainageWithApplications.curyAmt>
  {
  }

  public abstract class discAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRetainageWithApplications.discAmt>
  {
  }

  public abstract class curyDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRetainageWithApplications.curyDiscAmt>
  {
  }

  public abstract class wOAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRetainageWithApplications.wOAmt>
  {
  }

  public abstract class curyWOAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRetainageWithApplications.curyWOAmt>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRetainageWithApplications.paymentMethodID>
  {
  }

  public abstract class invoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRetainageWithApplications.invoiceNbr>
  {
  }

  public abstract class curyRetainageReleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRetainageWithApplications.curyRetainageReleasedAmt>
  {
  }

  public abstract class curyRetainagePaidAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRetainageWithApplications.curyRetainagePaidAmt>
  {
  }

  public new abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRetainageWithApplications.origDocAmt>
  {
  }

  public new abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRetainageWithApplications.curyOrigDocAmt>
  {
  }
}

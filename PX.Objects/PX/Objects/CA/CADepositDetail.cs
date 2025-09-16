// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CADepositDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("CA Deposit Detail")]
[Serializable]
public class CADepositDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  /// <summary>
  /// The type of the deposit.
  /// This field is a part of the compound key of the deposit.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"CDT"</c>: CA Deposit;
  /// <c>"CVD"</c>: CA Void Deposit.
  /// </value>
  [PXDBString(3, IsFixed = true, IsKey = true)]
  [CATranType.DepositList]
  [PXDefault(typeof (CADeposit.tranType))]
  [PXUIField]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  /// <summary>
  /// The reference number of the deposit.
  /// This field is a part of the compound key of<see cref="!:DepositDetail" />.
  /// </summary>
  [PXDBString(15, IsKey = true, InputMask = "", IsUnicode = true)]
  [PXDBDefault(typeof (CADeposit.refNbr))]
  [PXUIField]
  [PXParent(typeof (Select<CADeposit, Where<CADeposit.tranType, Equal<Current<CADepositDetail.tranType>>, And<CADeposit.refNbr, Equal<Current<CADepositDetail.refNbr>>>>>))]
  public virtual string RefNbr { get; set; }

  /// <summary>
  /// The line number of the detail line.
  /// The <see cref="T:PX.Objects.CA.CADeposit.lineCntr" /> field depends on this field.
  /// This field is a part of the compound key of <see cref="!:DepositDetail" />.
  /// </summary>
  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXLineNbr(typeof (CADeposit.lineCntr), DecrementOnDelete = false)]
  public virtual int? LineNbr { get; set; }

  /// <summary>
  /// The type of the delail, which is one of the following options:
  /// <c>"CHD"</c>: Check Deposit;
  /// <c>"VCD"</c>: Void Check Deposit;
  /// <c>"CSD"</c>: Cash Deposit;
  /// <c>"VSD"</c>: Void Cash Deposit.
  /// </summary>
  [PXDBString(3, IsFixed = true)]
  [CADepositDetailType.List]
  [PXDefault("CHD")]
  [PXUIField]
  public virtual string DetailType { get; set; }

  /// <summary>The module of the origin for the payment document.</summary>
  /// 
  ///             /// <value>
  /// The field can have one of the following values:
  /// <c>"AP"</c>: Accounts Payable;
  /// <c>"AR"</c>: Accounts Receivable.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXDefault("AR")]
  [PXUIField]
  public virtual string OrigModule { get; set; }

  /// <summary>
  /// The type of the payment, which is one of the following options: Payment, Credit Memo, Prepayment, or Refund.
  /// </summary>
  [PXDBString(3, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Doc.Type", Visible = true)]
  [CAAPARTranType.ListByModule(typeof (CADepositDetail.origModule))]
  public virtual string OrigDocType { get; set; }

  /// <summary>
  /// The reference number of the payment as assigned by the system.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Reference Nbr.")]
  public virtual string OrigRefNbr { get; set; }

  /// <summary>
  /// The payment method used by the customer for the payment.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault("")]
  [PXUIField(DisplayName = "Payment Method", Visible = false)]
  [PXSelector(typeof (PaymentMethod.paymentMethodID))]
  public virtual string PaymentMethodID { get; set; }

  /// <summary>The cash account to hold the payment.</summary>
  [PXDefault]
  [CashAccount]
  public virtual int? CashAccountID { get; set; }

  /// <summary>
  /// The balance type of the <see cref="!:DepositDetail" /> line.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"D"</c>: Receipt;
  /// <c>"C"</c>: Disbursement.
  /// </value>
  [PXDefault("C")]
  [PXDBString(1, IsFixed = true)]
  [CADrCr.List]
  [PXUIField(DisplayName = "Disb. / Receipt")]
  public virtual string DrCr { get; set; }

  /// <summary>
  /// The description of the deposit detail.
  /// This field is copied to the <see cref="P:PX.Objects.CA.CATran.TranDesc" /> field.
  /// </summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string TranDesc { get; set; }

  /// <summary>
  /// A flag that indicates (if selected) that the deposit is released.
  /// </summary>
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R1.")]
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released { get; set; }

  /// <summary>
  /// The identifier of the exchange rate record for the deposit.
  /// Corresponds to the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryInfoID" /> field.
  /// </summary>
  [PXDBLong]
  [CurrencyInfo(typeof (CADeposit.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>
  /// The amount of the payment to be deposited in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CADepositDetail.curyInfoID), typeof (CADepositDetail.tranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(null, typeof (SumCalc<CADeposit.curyDetailTotal>))]
  [PXUIField]
  public virtual Decimal? CuryTranAmt { get; set; }

  /// <summary>
  /// The amount of the payment to be deposited in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tran Amount")]
  public virtual Decimal? TranAmt { get; set; }

  /// <summary>
  /// The currency of the deposit detail, which corresponds to the currency of the cash account (<see cref="P:PX.Objects.CA.CashAccount.CashAccountID" />).
  /// </summary>
  [SlaveCuryID(typeof (CADepositDetail.origCuryInfoID))]
  public virtual string OrigCuryID { get; set; }

  /// <summary>
  /// The currency of the original payment document, which corresponds to the currency of the cash account (<see cref="P:PX.Objects.CA.CashAccount.CuryID" />).
  /// </summary>
  [PXDBLong]
  public virtual long? OrigCuryInfoID { get; set; }

  /// <summary>The balance type of the original document</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"D"</c>: Receipt;
  /// <c>"C"</c>: Disbursement.
  /// </value>
  [PXDefault]
  [PXDBString(1, IsFixed = true)]
  [CADrCr.List]
  public virtual string OrigDrCr { get; set; }

  /// <summary>
  /// The amount of the original payment document in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CADepositDetail.origCuryInfoID), typeof (CADepositDetail.origAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Amount", Visible = false)]
  public virtual Decimal? CuryOrigAmt { get; set; }

  /// <summary>
  /// The amount of the original payment document in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigAmt { get; set; }

  /// <summary>
  /// The identifier of the corresponding cash transaction, which corresponds to the <see cref="P:PX.Objects.CA.CATran.TranID" /> field.
  /// </summary>
  [PXDBLong]
  [PXUIField(DisplayName = "CA Tran ID")]
  [DepositDetailTranID]
  public virtual long? TranID { get; set; }

  /// <summary>
  /// The entry type of the charges that apply to the payment included in the deposit.
  /// An entry type is a type of entered cash transaction that your site uses to classify the transactions for appropriate processing.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Charge Type")]
  [PXSelector(typeof (CAEntryType.entryTypeId))]
  public virtual string ChargeEntryTypeID { get; set; }

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

  /// <summary>The sign of the amount.</summary>
  public Decimal OrigDocSign
  {
    [PXDependsOnFields(new Type[] {typeof (CADepositDetail.drCr), typeof (CADepositDetail.origDocType), typeof (CADepositDetail.origModule)})] get
    {
      return !(this.OrigDrCr == "C") ? 1M : -1M;
    }
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the original payment document created in the AP module.
  /// </summary>
  public bool IsAP
  {
    [PXDependsOnFields(new Type[] {typeof (CADepositDetail.origModule)})] get
    {
      return this.OrigModule == "AP";
    }
  }

  /// <summary>
  /// The signed amount of the original payment document in the selected currency.
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? CuryOrigAmtSigned
  {
    [PXDependsOnFields(new Type[] {typeof (CADepositDetail.curyOrigAmt), typeof (CADepositDetail.origDocSign)})] get
    {
      Decimal? curyOrigAmt = this.CuryOrigAmt;
      Decimal origDocSign = this.OrigDocSign;
      return !curyOrigAmt.HasValue ? new Decimal?() : new Decimal?(curyOrigAmt.GetValueOrDefault() * origDocSign);
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? OrigAmtSigned
  {
    [PXDependsOnFields(new Type[] {typeof (CADepositDetail.origAmt), typeof (CADepositDetail.origDocSign)})] get
    {
      Decimal? origAmt = this.OrigAmt;
      Decimal origDocSign = this.OrigDocSign;
      return !origAmt.HasValue ? new Decimal?() : new Decimal?(origAmt.GetValueOrDefault() * origDocSign);
    }
  }

  public class PK : 
    PrimaryKeyOf<CADepositDetail>.By<CADepositDetail.tranType, CADepositDetail.refNbr, CADepositDetail.lineNbr>
  {
    public static CADepositDetail Find(
      PXGraph graph,
      string tranType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (CADepositDetail) PrimaryKeyOf<CADepositDetail>.By<CADepositDetail.tranType, CADepositDetail.refNbr, CADepositDetail.lineNbr>.FindBy(graph, (object) tranType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class CashAccountDeposit : 
      PrimaryKeyOf<CADeposit>.By<CADeposit.tranType, CADeposit.refNbr>.ForeignKeyOf<CADepositDetail>.By<CADepositDetail.tranType, CADepositDetail.refNbr>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PaymentMethod>.By<PaymentMethod.paymentMethodID>.ForeignKeyOf<CADepositDetail>.By<CADepositDetail.paymentMethodID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CADepositDetail>.By<CADepositDetail.cashAccountID>
    {
    }

    public class DepositCurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CADepositDetail>.By<CADepositDetail.curyInfoID>
    {
    }

    public class CashAccountCurrency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CADepositDetail>.By<CADepositDetail.origCuryID>
    {
    }

    public class CashAccountCurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CADepositDetail>.By<CADepositDetail.origCuryInfoID>
    {
    }

    public class CashAccountTransaction : 
      PrimaryKeyOf<CATran>.By<CATran.cashAccountID, CATran.tranID>.ForeignKeyOf<CADepositDetail>.By<CADepositDetail.cashAccountID, CADepositDetail.tranID>
    {
    }

    public class ChargeEntryType : 
      PrimaryKeyOf<CAEntryType>.By<CAEntryType.entryTypeId>.ForeignKeyOf<CADepositDetail>.By<CADepositDetail.entryTypeID>
    {
    }

    public class ARPayment : 
      PrimaryKeyOf<PX.Objects.AR.ARPayment>.By<PX.Objects.AR.ARPayment.docType, PX.Objects.AR.ARPayment.refNbr>.ForeignKeyOf<CADepositDetail>.By<CADepositDetail.origDocType, CADepositDetail.origRefNbr>
    {
    }

    public class APPayment : 
      PrimaryKeyOf<PX.Objects.AP.APPayment>.By<PX.Objects.AP.APPayment.docType, PX.Objects.AP.APPayment.refNbr>.ForeignKeyOf<CADepositDetail>.By<CADepositDetail.origDocType, CADepositDetail.origRefNbr>
    {
    }

    public class CashTransaction : 
      PrimaryKeyOf<CAAdj>.By<CAAdj.adjTranType, CAAdj.adjRefNbr>.ForeignKeyOf<CADepositDetail>.By<CADepositDetail.origDocType, CADepositDetail.origRefNbr>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CADepositDetail.selected>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADepositDetail.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADepositDetail.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CADepositDetail.lineNbr>
  {
  }

  public abstract class detailType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADepositDetail.detailType>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADepositDetail.origModule>
  {
  }

  public abstract class origDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADepositDetail.origDocType>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADepositDetail.origRefNbr>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CADepositDetail.paymentMethodID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CADepositDetail.cashAccountID>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADepositDetail.drCr>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADepositDetail.tranDesc>
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R1.")]
  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CADepositDetail.released>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CADepositDetail.curyInfoID>
  {
  }

  public abstract class curyTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADepositDetail.curyTranAmt>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CADepositDetail.tranAmt>
  {
  }

  public abstract class origCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADepositDetail.origCuryID>
  {
  }

  public abstract class origCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    CADepositDetail.origCuryInfoID>
  {
  }

  public abstract class origDrCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADepositDetail.origDrCr>
  {
  }

  public abstract class curyOrigAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADepositDetail.curyOrigAmt>
  {
  }

  public abstract class origAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CADepositDetail.origAmt>
  {
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CADepositDetail.tranID>
  {
  }

  public abstract class entryTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADepositDetail.entryTypeID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CADepositDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CADepositDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CADepositDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CADepositDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CADepositDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CADepositDetail.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CADepositDetail.Tstamp>
  {
  }

  public abstract class origDocSign : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADepositDetail.origDocSign>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTaxTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXBreakInheritance]
[PXCacheName("CA Bank Tax Transaction")]
[Serializable]
public class CABankTaxTran : TaxDetail, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _JurisName;

  /// <summary>
  /// The reference to the <see cref="T:PX.Objects.GL.Branch" /> record to which the record belongs.
  /// </summary>
  /// <value>The value is copied from the document from which the record is created.</value>
  [Branch(null, null, true, true, true, Enabled = false)]
  public virtual int? BranchID { get; set; }

  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault("CA")]
  [PXUIField(DisplayName = "Module", Enabled = false, Visible = false)]
  public virtual string Module { get; set; }

  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault(typeof (CABankTran.tranType))]
  [CABankTranType.List]
  [PXUIField]
  [PXParent(typeof (CABankTaxTran.FK.BankTransaction))]
  public virtual string BankTranType { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (CABankTran.tranID))]
  public virtual int? BankTranID { get; set; }

  [PXDBDate]
  [PXDBDefault(typeof (CABankTran.tranDate))]
  public virtual DateTime? TranDate { get; set; }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr), DirtyRead = true)]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  /// <summary>
  /// This is an auto-numbered field, which is a part of the primary key.
  /// </summary>
  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Search<PX.Objects.TX.Tax.taxVendorID, Where<PX.Objects.TX.Tax.taxID, Equal<Current<CABankTaxTran.taxID>>>>))]
  public virtual int? VendorID { get; set; }

  [Account]
  [PXDefault]
  public virtual int? AccountID { get; set; }

  [SubAccount]
  [PXDefault]
  public virtual int? SubID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  public virtual string TaxPeriodID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, typeof (CABankTaxTran.branchID), null, null, null, null, true, false, null, null, typeof (CABankTran.tranPeriodID), true, true)]
  [PXDefault]
  public virtual string FinPeriodID { get; set; }

  /// <summary>
  /// The last day (<see cref="!:PX.Objects.GL.Obsolete.FinPeriod.FinDate" />) of the financial period of the document to which the record belongs.
  /// </summary>
  [PXDBDate]
  [PXDBDefault(typeof (Search2<OrganizationFinPeriod.finDate, InnerJoin<PX.Objects.GL.Branch, On<OrganizationFinPeriod.organizationID, Equal<PX.Objects.GL.Branch.organizationID>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current2<CABankTaxTran.branchID>>, And<OrganizationFinPeriod.finPeriodID, Equal<Current2<CABankTaxTran.finPeriodID>>>>>))]
  public virtual DateTime? FinDate { get; set; }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the record has been released.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released { get; set; }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the record has been voided.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Voided { get; set; }

  /// <summary>
  /// The tax jurisdiction type. The field is used for the taxes from Avalara.
  /// </summary>
  [PXDBString(9, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Jurisdiction Type")]
  public virtual string JurisType { get; set; }

  /// <summary>
  /// The tax jurisdiction name. The field is used for the taxes from Avalara.
  /// </summary>
  [PXDBString(200, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Jurisdiction Name")]
  public virtual string JurisName
  {
    get => this._JurisName;
    set => this._JurisName = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (CABankTran.curyInfoID))]
  public override long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (CABankTaxTran.curyInfoID), typeof (CABankTaxTran.taxDiscountAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxDiscountAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxDiscountAmt { get; set; }

  /// <summary>
  /// The original taxable amount (before truncation by minimal or maximal value) in the record currency.
  /// </summary>
  [PXDBCurrency(typeof (CABankTaxTran.curyInfoID), typeof (CABankTaxTran.origTaxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Orig. Taxable Amount")]
  public virtual Decimal? CuryOrigTaxableAmt { get; set; }

  /// <summary>
  /// The original taxable amount (before truncation by minimal or maximal value) in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Orig. Taxable Amount")]
  public virtual Decimal? OrigTaxableAmt { get; set; }

  /// <summary>The taxable amount in the record currency.</summary>
  [PXDBCurrency(typeof (CABankTaxTran.curyInfoID), typeof (CABankTaxTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxableAmt { get; set; }

  /// <summary>The taxable amount in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxableAmt { get; set; }

  /// <summary>The exempted amount in the record currency.</summary>
  [PXDBCurrency(typeof (CABankTaxTran.curyInfoID), typeof (CABankTaxTran.exemptedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryExemptedAmt { get; set; }

  /// <summary>The exempted amount in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ExemptedAmt { get; set; }

  [PXDBCurrency(typeof (CABankTaxTran.curyInfoID), typeof (CABankTaxTran.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt { get; set; }

  /// <summary>The tax amount in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxAmt { get; set; }

  /// <summary>
  /// The reference to the vendor record (<see cref="!:Vendor.BAccountID" />) or customer record (<see cref="!:Customer.BAccountID" />).
  /// The field is used for the records that have been created in the AP or AR module.
  /// </summary>
  [PXDBInt]
  public virtual int? BAccountID { get; set; }

  [PXDBCurrency(typeof (CABankTaxTran.curyInfoID), typeof (CABankTaxTran.taxAmtSumm))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxAmtSumm { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxAmtSumm { get; set; }

  [PXDBCurrency(typeof (CABankTaxTran.curyInfoID), typeof (CABankTaxTran.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExpenseAmt { get; set; }

  /// <summary>
  /// The reference to the currency (<see cref="!:Currency.CuryID" />) of the document to which the record belongs.
  /// </summary>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency")]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  [PXDefault(typeof (CABankTran.curyID))]
  public virtual string CuryID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  public virtual string TaxType { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  public virtual string TaxZoneID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Search<TaxRev.taxBucketID, Where<TaxRev.taxID, Equal<Current<CABankTaxTran.taxID>>, And<Current<CABankTaxTran.tranDate>, Between<TaxRev.startDate, TaxRev.endDate>, And2<Where<TaxRev.taxType, Equal<Current<CABankTaxTran.taxType>>, Or<TaxRev.taxType, Equal<PX.Objects.TX.TaxType.sales>, And<Current<CABankTaxTran.taxType>, Equal<PX.Objects.TX.TaxType.pendingSales>, Or<TaxRev.taxType, Equal<PX.Objects.TX.TaxType.purchase>, And<Current<CABankTaxTran.taxType>, Equal<PX.Objects.TX.TaxType.pendingPurchase>>>>>>, And<TaxRev.outdated, Equal<False>>>>>>))]
  public virtual int? TaxBucketID { get; set; }

  /// <summary>
  /// The tax rate of the relevant <see cref="T:PX.Objects.TX.Tax" /> record.
  /// </summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? TaxRate { get; set; }

  /// <summary>
  /// The reference number of the tax invoice. The field is used for recognized SVAT records.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Invoice Nbr.")]
  public virtual string TaxInvoiceNbr { get; set; }

  /// <summary>
  /// The date of the tax invoice. The field is used for recognized SVAT records.
  /// </summary>
  [PXDBDate(InputMask = "d", DisplayMask = "d")]
  [PXUIField(DisplayName = "Tax Invoice Date")]
  public virtual DateTime? TaxInvoiceDate { get; set; }

  /// <summary>
  /// The original document type for which the tax amount has been entered.
  /// The field is used for the records that are created on the Tax Bills and Adjustments (TX303000) form.
  /// </summary>
  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "Orig. Tran. Type")]
  [PXDefault("")]
  public virtual string OrigTranType { get; set; }

  /// <summary>
  /// The original document reference number for which the tax amount has been entered.
  /// The field is used for the records that are created on the Tax Bills and Adjustments (TX303000) form.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Orig. Doc. Number")]
  [PXDefault("")]
  public virtual string OrigRefNbr { get; set; }

  /// <summary>
  /// The reference number of the transaction to which the record is related.
  /// The field is used for the records that are created from GL.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Line Ref. Number")]
  [PXDefault("")]
  public virtual string LineRefNbr { get; set; }

  /// <summary>
  /// The revision of the tax report to which the record was included.
  /// </summary>
  [PXDBInt]
  public virtual int? RevisionID { get; set; }

  /// <summary>
  /// Link to <see cref="!:APPayment" /> (Check) application. Used for withholding taxes.
  /// </summary>
  [PXDBString(3)]
  public virtual string AdjdDocType { get; set; }

  /// <summary>
  /// Link to <see cref="!:APPayment" /> (Check) application. Used for withholding taxes.
  /// </summary>
  [PXDBString(15)]
  public virtual string AdjdRefNbr { get; set; }

  /// <summary>
  /// Link to <see cref="!:APPayment" /> (Check) application. Used for withholding taxes.
  /// </summary>
  [PXDBInt]
  public virtual int? AdjNbr { get; set; }

  /// <summary>The description of the transaction.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<CABankTaxTran>.By<CABankTaxTran.module, CABankTaxTran.tranDate, CABankTaxTran.recordID>
  {
    public static CABankTaxTran Find(
      PXGraph graph,
      string module,
      DateTime? tranDate,
      int? recordID,
      PKFindOptions options = 0)
    {
      return (CABankTaxTran) PrimaryKeyOf<CABankTaxTran>.By<CABankTaxTran.module, CABankTaxTran.tranDate, CABankTaxTran.recordID>.FindBy(graph, (object) module, (object) tranDate, (object) recordID, options);
    }
  }

  public static class FK
  {
    public class BankTransaction : 
      PrimaryKeyOf<CABankTran>.By<CABankTran.tranID>.ForeignKeyOf<CABankTax>.By<CABankTaxTran.bankTranID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTaxTran.branchID>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTaxTran.module>
  {
  }

  public abstract class bankTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTaxTran.bankTranType>
  {
  }

  public abstract class bankTranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTaxTran.bankTranID>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CABankTaxTran.tranDate>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTaxTran.taxID>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTaxTran.recordID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTaxTran.vendorID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTaxTran.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTaxTran.subID>
  {
  }

  public abstract class taxPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTaxTran.taxPeriodID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTaxTran.finPeriodID>
  {
  }

  public abstract class finDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CABankTaxTran.finDate>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTaxTran.released>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTaxTran.voided>
  {
  }

  public abstract class jurisType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTaxTran.jurisType>
  {
  }

  public abstract class jurisName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTaxTran.jurisName>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CABankTaxTran.curyInfoID>
  {
  }

  public abstract class curyTaxDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTaxTran.curyTaxDiscountAmt>
  {
  }

  public abstract class taxDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTaxTran.taxDiscountAmt>
  {
  }

  public abstract class curyOrigTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTaxTran.curyOrigTaxableAmt>
  {
  }

  public abstract class origTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTaxTran.origTaxableAmt>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTaxTran.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTaxTran.taxableAmt>
  {
  }

  public abstract class curyExemptedAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class exemptedAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTaxTran.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTaxTran.taxAmt>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTaxTran.bAccountID>
  {
  }

  public abstract class curyTaxAmtSumm : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTaxTran.curyTaxAmtSumm>
  {
  }

  public abstract class taxAmtSumm : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTaxTran.taxAmtSumm>
  {
  }

  public abstract class nonDeductibleTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTaxTran.nonDeductibleTaxRate>
  {
  }

  public abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTaxTran.curyExpenseAmt>
  {
  }

  public abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTaxTran.expenseAmt>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTaxTran.curyID>
  {
  }

  public abstract class taxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTaxTran.taxType>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTaxTran.taxZoneID>
  {
  }

  public abstract class taxBucketID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTaxTran.taxBucketID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTaxTran.taxRate>
  {
  }

  public abstract class taxInvoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTaxTran.taxInvoiceNbr>
  {
  }

  public abstract class taxInvoiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTaxTran.taxInvoiceDate>
  {
  }

  public abstract class origTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTaxTran.origTranType>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTaxTran.origRefNbr>
  {
  }

  public abstract class lineRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTaxTran.lineRefNbr>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTaxTran.revisionID>
  {
  }

  public abstract class adjdDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTaxTran.adjdDocType>
  {
  }

  public abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTaxTran.adjdRefNbr>
  {
  }

  public abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTaxTran.adjNbr>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTaxTran.description>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CABankTaxTran.Tstamp>
  {
  }
}

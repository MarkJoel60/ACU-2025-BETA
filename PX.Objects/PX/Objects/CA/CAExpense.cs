// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAExpense
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>A CA transfer expense.</summary>
[PXCacheName("CAExpense")]
[Serializable]
public class CAExpense : PXBqlTable, IPaymentCharge, IBqlTable, IBqlTableSystemDataStorage
{
  public 
  #nullable disable
  string DocType
  {
    get => "CTE";
    set
    {
    }
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (CATransfer.transferNbr))]
  [PXParent(typeof (Select<CATransfer, Where<CATransfer.transferNbr, Equal<Current<CAExpense.refNbr>>>>))]
  public string RefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (CATransfer.expenseCntr), DecrementOnDelete = false)]
  [PXUIField]
  public int? LineNbr { get; set; }

  [CashAccount(null, typeof (Search<CashAccount.cashAccountID, Where<Match<Current<AccessInfo.userName>>>>))]
  [PXDefault]
  public int? CashAccountID { get; set; }

  [Branch(typeof (Search<CashAccount.branchID, Where<CashAccount.cashAccountID, Equal<Current<CAExpense.cashAccountID>>>>), null, true, true, true)]
  [PXFormula(typeof (Default<CAExpense.cashAccountID>))]
  public virtual int? BranchID { get; set; }

  [Obsolete("Will be removed in Acumatica 2019R2")]
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Ref. Nbr.", Enabled = false, Visible = false)]
  public string AdjRefNbr { get; set; }

  [PXDBDate]
  [PXDefault(typeof (CATransfer.outDate))]
  [PXUIField(DisplayName = "Doc. Date")]
  public virtual DateTime? TranDate { get; set; }

  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  [CAOpenPeriod(typeof (CAExpense.tranDate), typeof (CAExpense.cashAccountID), typeof (Selector<CAExpense.cashAccountID, CashAccount.branchID>), null, null, null, true, typeof (CAExpense.tranPeriodID), ValidatePeriod = PeriodValidation.DefaultSelectUpdate)]
  [PXUIField(DisplayName = "Fin. Period")]
  [PXFormula(typeof (Default<CAExpense.cashAccountID>))]
  public virtual string FinPeriodID { get; set; }

  [PXDBLong]
  [CurrencyInfo]
  public long? CuryInfoID { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  [PXDefault(typeof (Search<CashAccount.curyID, Where<CashAccount.cashAccountID, Equal<Current<CAExpense.cashAccountID>>>>))]
  public virtual string CuryID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDecimal(8)]
  [PXUIField]
  public virtual Decimal? AdjCuryRate { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>, Where<CashAccountETDetail.cashAccountID, Equal<Current<CAExpense.cashAccountID>>, And<CAEntryType.module, Equal<BatchModule.moduleCA>>>>), DescriptionField = typeof (CAEntryType.descr), DirtyRead = false)]
  [PXDefault(typeof (Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>, Where<CashAccountETDetail.cashAccountID, Equal<Current<CAExpense.cashAccountID>>, And<CAEntryType.module, Equal<BatchModule.moduleCA>, And<CashAccountETDetail.isDefault, Equal<True>>>>>))]
  [PXFormula(typeof (Default<CAExpense.cashAccountID>))]
  [PXUIField(DisplayName = "Entry Type")]
  public virtual string EntryTypeID { get; set; }

  [Account(typeof (CAExpense.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Where<PX.Objects.GL.Account.curyID, Equal<Current<CAExpense.curyID>>, Or<PX.Objects.GL.Account.curyID, IsNull>>, And<Match<Current<AccessInfo.userName>>>>>))]
  [PXDefault(typeof (Coalesce<Search<CashAccountETDetail.offsetAccountID, Where<CashAccountETDetail.entryTypeID, Equal<Current<CAExpense.entryTypeID>>, And<CashAccountETDetail.cashAccountID, Equal<Current<CAExpense.cashAccountID>>>>>, Search<CAEntryType.accountID, Where<CAEntryType.entryTypeId, Equal<Current<CAExpense.entryTypeID>>>>>))]
  [PXFormula(typeof (Default<CAExpense.cashAccountID, CAExpense.entryTypeID>))]
  public virtual int? AccountID { get; set; }

  [PXDefault(typeof (Coalesce<Search<CashAccountETDetail.offsetSubID, Where<CashAccountETDetail.entryTypeID, Equal<Current<CAExpense.entryTypeID>>, And<CashAccountETDetail.cashAccountID, Equal<Current<CAExpense.cashAccountID>>>>>, Search<CAEntryType.subID, Where<CAEntryType.entryTypeId, Equal<Current<CAExpense.entryTypeID>>>>>))]
  [SubAccount(typeof (CAExpense.accountID), DisplayName = "Offset Subaccount", Required = true)]
  [PXFormula(typeof (Default<CAExpense.cashAccountID, CAExpense.entryTypeID>))]
  public virtual int? SubID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (Search<CAEntryType.drCr, Where<CAEntryType.entryTypeId, Equal<Current<CAExpense.entryTypeID>>>>))]
  [CADrCr.List]
  [PXUIField]
  [PXFormula(typeof (Default<CAExpense.entryTypeID>))]
  public string DrCr { get; set; }

  [PXDBLong]
  [ExpenseCashTranID]
  public long? CashTranID { get; set; }

  [PXDBCurrency(typeof (CAExpense.curyInfoID), typeof (CAExpense.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount", Enabled = true, Visible = true)]
  public Decimal? CuryTaxableAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount", Enabled = false, Visible = true)]
  public virtual Decimal? TaxableAmt { get; set; }

  [PXDependsOnFields(new Type[] {typeof (CAExpense.curyTaxableAmt), typeof (CAExpense.curyTaxAmt)})]
  [PXDBCurrency(typeof (CAExpense.curyInfoID), typeof (CAExpense.tranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (CAExpense.curyTaxableAmt))]
  [PXUIField(DisplayName = "Total Amount", Enabled = false, Visible = true)]
  public Decimal? CuryTranAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(null, typeof (SumCalc<CATransfer.totalExpenses>))]
  [PXUIField(DisplayName = "Total Amount", Enabled = false)]
  public virtual Decimal? TranAmt { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released")]
  public bool? Released { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cleared")]
  public virtual bool? Cleared { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Clear Date")]
  public virtual DateTime? ClearDate { get; set; }

  [PXDBString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Document Ref.")]
  public virtual string ExtRefNbr { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  [PXDefault(typeof (Search<CAEntryType.descr, Where<CAEntryType.entryTypeId, Equal<Current<CAExpense.entryTypeID>>>>))]
  [PXFormula(typeof (Default<CAExpense.entryTypeID>))]
  public virtual string TranDesc { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXUIField]
  [PXDBScalar(typeof (Search<CATran.batchNbr, Where<CATran.tranID, Equal<CAExpense.cashTranID>>>))]
  public virtual string BatchNbr { get; set; }

  /// <summary>The tax zone that applies to the transaction.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.TX.TaxZone.TaxZoneID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXDefault(typeof (Search<CashAccountETDetail.taxZoneID, Where<CashAccountETDetail.cashAccountID, Equal<Current<CAExpense.cashAccountID>>, And<CashAccountETDetail.entryTypeID, Equal<Current<CAExpense.entryTypeID>>>>>))]
  [PXFormula(typeof (Default<CAExpense.cashAccountID, CAExpense.entryTypeID>))]
  public virtual string TaxZoneID { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXDefault(typeof (Search<PX.Objects.TX.TaxZone.dfltTaxCategoryID, Where<PX.Objects.TX.TaxZone.taxZoneID, Equal<Current<CAExpense.taxZoneID>>>>))]
  [PXFormula(typeof (Default<CAExpense.cashAccountID, CAExpense.entryTypeID, CAExpense.taxZoneID>))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  public virtual string TaxCategoryID { get; set; }

  /// <summary>
  /// The total amount of tax paid on the document in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CAExpense.curyInfoID), typeof (CAExpense.taxTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxTotal { get; set; }

  /// <summary>
  /// The total amount of tax paid on the document in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal { get; set; }

  /// <summary>
  /// The document total that is exempt from VAT in the selected currency.
  /// This total is calculated as the taxable amount for the tax
  /// with the <see cref="P:PX.Objects.TX.Tax.ExemptTax" /> field set to <c>true</c> (that is, the Include in VAT Exempt Total check box selected on the Taxes (TX205000) form).
  /// </summary>
  [PXDBCurrency(typeof (CAExpense.curyInfoID), typeof (CAExpense.vatExemptTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal { get; set; }

  /// <summary>
  /// The document total that is exempt from VAT in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal { get; set; }

  /// <summary>
  /// The document total that is subjected to VAT in the selected currency.
  /// The field is displayed only if
  /// the <see cref="P:PX.Objects.TX.Tax.IncludeInTaxable" /> field is set to <c>true</c> (that is, the Include in VAT Exempt Total check box is selected on the Taxes (TX205000) form).
  /// </summary>
  [PXDBCurrency(typeof (CAExpense.curyInfoID), typeof (CAExpense.vatTaxableTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal { get; set; }

  /// <summary>
  /// The document total that is subjected to VAT in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal { get; set; }

  /// <summary>
  /// The tax amount to be paid for the document in the selected currency.
  /// This field is enable and visible only if the <see cref="P:PX.Objects.CA.CASetup.RequireControlTaxTotal" /> field
  /// and the <see cref="P:PX.Objects.CS.FeaturesSet.NetGrossEntryMode" /> field are set to <c>true</c>.
  /// </summary>
  [PXDBCurrency(typeof (CAExpense.curyInfoID), typeof (CAExpense.taxAmt))]
  [PXUIField(DisplayName = "Tax Amount")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxAmt { get; set; }

  /// <summary>
  /// The tax amount to be paid for the document in the base currency.
  /// </summary>
  [PXDBDecimal(4, BqlField = typeof (CAExpense.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxAmt { get; set; }

  /// <summary>
  /// The sum of amounts of all detail lines in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CAExpense.curyInfoID), typeof (CAExpense.splitTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CurySplitTotal { get; set; }

  /// <summary>
  /// The sum of amounts of all detail lines in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? SplitTotal { get; set; }

  /// <summary>
  /// The control total of the transaction in the selected currency.
  /// A user enters this amount manually.
  /// This amount should be equal to the <see cref="P:PX.Objects.CA.CAExpense.CurySplitTotal">sum of amounts of all detail lines</see> of the transaction.
  /// </summary>
  [PXDBCurrency(typeof (CAExpense.curyInfoID), typeof (CAExpense.controlAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Total")]
  public virtual Decimal? CuryControlAmt { get; set; }

  /// <summary>
  /// The control total of the transaction in the base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ControlAmt { get; set; }

  /// <summary>
  /// The tax calculation mode, which defines which amounts (tax-inclusive or tax-exclusive)
  /// should be entered in the detail lines of a document.
  /// This field is displayed only if the <see cref="P:PX.Objects.CS.FeaturesSet.NetGrossEntryMode" /> field is set to <c>true</c>.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"T"</c> (Tax Settings): The tax amount for the document is calculated according to the settings of the applicable tax or taxes.
  /// <c>"G"</c> (Gross): The amount in the document detail line includes a tax or taxes.
  /// <c>"N"</c> (Net): The amount in the document detail line does not include taxes.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("T", typeof (Search<CashAccountETDetail.taxCalcMode, Where<CashAccountETDetail.cashAccountID, Equal<Current<CAExpense.cashAccountID>>, And<CashAccountETDetail.entryTypeID, Equal<Current<CAExpense.entryTypeID>>>>>))]
  [PXFormula(typeof (Default<CAExpense.cashAccountID, CAExpense.entryTypeID>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  /// <summary>
  /// The difference between the original document amount and the rounded amount in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CAExpense.curyInfoID), typeof (CAExpense.taxRoundDiff), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? CuryTaxRoundDiff { get; set; }

  /// <summary>
  /// The difference between the original document amount and the rounded amount in the base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? TaxRoundDiff { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that withholding taxes are applied to the document.
  /// This is a technical field, which is calculated on the fly and is used to restrict the values of the <see cref="P:PX.Objects.CA.CAExpense.TaxCalcMode" /> field.
  /// </summary>
  [PXBool]
  [RestrictWithholdingTaxCalcMode(typeof (CAExpense.taxZoneID), typeof (CAExpense.taxCalcMode))]
  public virtual bool? HasWithHoldTax { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that use taxes are applied to the document.
  /// This is a technical field, which is calculated on the fly and is used to restrict the values of the <see cref="P:PX.Objects.CA.CAExpense.TaxCalcMode" /> field.
  /// </summary>
  [PXBool]
  [RestrictUseTaxCalcMode(typeof (CAExpense.taxZoneID), typeof (CAExpense.taxCalcMode))]
  public virtual bool? HasUseTax { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that this transaction is used for payments reclassification.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.CAEntryType.UseToReclassifyPayments" /> field of the selected <see cref="T:PX.Objects.CA.CAEntryType">entry type</see>.
  /// </value>
  [PXBool]
  [PXDBScalar(typeof (Search<CAEntryType.useToReclassifyPayments, Where<CAEntryType.entryTypeId, Equal<CAExpense.entryTypeID>>>))]
  public virtual bool? PaymentsReclassification { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

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

  public class PK : PrimaryKeyOf<CAExpense>.By<CAExpense.refNbr, CAExpense.lineNbr>
  {
    public static CAExpense Find(
      PXGraph graph,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (CAExpense) PrimaryKeyOf<CAExpense>.By<CAExpense.refNbr, CAExpense.lineNbr>.FindBy(graph, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class CashTransfer : 
      PrimaryKeyOf<CATransfer>.By<CATransfer.transferNbr>.ForeignKeyOf<CAExpense>.By<CAExpense.refNbr>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CAExpense>.By<CAExpense.cashAccountID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CAExpense>.By<CAExpense.branchID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CAExpense>.By<CAExpense.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CAExpense>.By<CAExpense.curyID>
    {
    }

    public class EntryType : 
      PrimaryKeyOf<CAEntryType>.By<CAEntryType.entryTypeId>.ForeignKeyOf<CAExpense>.By<CAExpense.entryTypeID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CAExpense>.By<CAExpense.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<CAExpense>.By<CAExpense.subID>
    {
    }

    public class CashAccountTran : 
      PrimaryKeyOf<CATran>.By<CATran.cashAccountID, CATran.tranID>.ForeignKeyOf<CAExpense>.By<CAExpense.cashAccountID, CAExpense.cashTranID>
    {
    }
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpense.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAExpense.lineNbr>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAExpense.cashAccountID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAExpense.branchID>
  {
  }

  [Obsolete("Will be removed in Acumatica 2019R2")]
  public abstract class adjRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpense.adjRefNbr>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CAExpense.tranDate>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpense.tranPeriodID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpense.finPeriodID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CAExpense.curyInfoID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpense.curyID>
  {
  }

  public abstract class adjCuryRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAExpense.adjCuryRate>
  {
  }

  public abstract class entryTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpense.entryTypeID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAExpense.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAExpense.subID>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpense.drCr>
  {
  }

  public abstract class cashTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CAExpense.cashTranID>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpense.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAExpense.taxableAmt>
  {
  }

  public abstract class curyTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAExpense.curyTranAmt>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAExpense.tranAmt>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAExpense.released>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAExpense.cleared>
  {
  }

  public abstract class clearDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CAExpense.clearDate>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpense.extRefNbr>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpense.tranDesc>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpense.batchNbr>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpense.taxZoneID>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpense.taxCategoryID>
  {
  }

  public abstract class curyTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAExpense.curyTaxTotal>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAExpense.taxTotal>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpense.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpense.vatExemptTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpense.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpense.vatTaxableTotal>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAExpense.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAExpense.taxAmt>
  {
  }

  public abstract class curySplitTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpense.curySplitTotal>
  {
  }

  public abstract class splitTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAExpense.splitTotal>
  {
  }

  public abstract class curyControlAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpense.curyControlAmt>
  {
  }

  public abstract class controlAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAExpense.controlAmt>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpense.taxCalcMode>
  {
  }

  public abstract class curyTaxRoundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpense.curyTaxRoundDiff>
  {
  }

  public abstract class taxRoundDiff : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAExpense.taxRoundDiff>
  {
  }

  public abstract class hasWithHoldTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAExpense.hasWithHoldTax>
  {
  }

  public abstract class hasUseTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAExpense.hasUseTax>
  {
  }

  public abstract class paymentsReclassification : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CAExpense.paymentsReclassification>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CAExpense.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CAExpense.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CAExpense.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CAExpense.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CAExpense.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CAExpense.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CAExpense.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CAExpense.Tstamp>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CADeposit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.CM.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXPrimaryGraph(typeof (CADepositEntry))]
[PXCacheName("CA Deposit")]
[Serializable]
public class CADeposit : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ICADocument
{
  /// <summary>Implementation of the ICADocument interface.</summary>
  public 
  #nullable disable
  string DocType => this.TranType;

  /// <summary>
  /// The type of the deposit.
  /// This field is a part of the compound key of the deposit.
  /// The field can have one of the following values:
  /// <c>"CDT"</c>: CA Deposit,
  /// <c>"CVD"</c>: CA Void Deposit.
  /// </summary>
  [PXDBString(3, IsFixed = true, IsKey = true)]
  [CATranType.DepositList]
  [PXDefault("CDT")]
  [PXUIField]
  public virtual string TranType { get; set; }

  /// <summary>
  /// The reference number of the deposit.
  /// This field is a part of the compound key of the deposit.
  /// </summary>
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [CADepositType.Numbering]
  [CADepositType.RefNbr(typeof (Search<CADeposit.refNbr, Where<CADeposit.tranType, Equal<Current<CADeposit.tranType>>>>))]
  public virtual string RefNbr { get; set; }

  /// <summary>The external reference number of the deposit.</summary>
  [PXDBString(40, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string ExtRefNbr { get; set; }

  /// <summary>
  /// The cash account (usually a bank account) to which the deposit will be posted.
  /// </summary>
  [PXDefault]
  [CashAccount(null, typeof (Search<CashAccount.cashAccountID, Where2<Match<Current<AccessInfo.userName>>, And<CashAccount.clearingAccount, Equal<boolFalse>>>>))]
  public virtual int? CashAccountID { get; set; }

  [PXFormula(typeof (Default<CADeposit.cashAccountID>))]
  [Branch(typeof (Search<CashAccount.branchID, Where<CashAccount.cashAccountID, Equal<Current<CADeposit.cashAccountID>>>>), null, true, true, true)]
  [PXUIField(DisplayName = "Branch")]
  public virtual int? BranchID { get; set; }

  /// <summary>The date of the deposit.</summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? TranDate { get; set; }

  /// <summary>The balance type of the deposit.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"D"</c>: Receipt,
  /// <c>"C"</c>: Disbursement.
  /// </value>
  [PXDefault("D")]
  [PXDBString(1, IsFixed = true)]
  [CADrCr.List]
  public virtual string DrCr { get; set; }

  /// <summary>A detailed description of the deposit.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string TranDesc { get; set; }

  /// <summary>
  /// The<see cref="T:PX.Objects.GL.FinPeriods.OrganizationFinPeriod"> financial period</see> of the document.
  /// </summary>
  /// <value>
  /// Is determined by the <see cref="P:PX.Objects.CA.CADeposit.TranDate">date of the document</see>.
  /// A user can override the value of this field (unlike <see cref="P:PX.Objects.CA.CADeposit.FinPeriodID" />).
  /// </value>
  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  /// <summary>
  /// <see cref="T:PX.Objects.GL.FinPeriods.OrganizationFinPeriod">Financial Period</see> of the document.
  /// </summary>
  /// <value>
  /// Defaults to the period to which the <see cref="P:PX.Objects.CA.CADeposit.TranDate" /> belongs, but can be overridden by a user.
  /// </value>
  [CAOpenPeriod(typeof (CADeposit.tranDate), typeof (CADeposit.cashAccountID), typeof (Selector<CADeposit.cashAccountID, CashAccount.branchID>), null, null, null, true, typeof (CADeposit.tranPeriodID))]
  [PXDefault]
  [PXUIField(DisplayName = "Fin. Period")]
  public virtual string FinPeriodID { get; set; }

  /// <summary>
  /// A check box that indicates (if selected) that the deposit is on hold, which means it may be edited but cannot be released.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (Search<CASetup.holdEntry>))]
  [PXUIField(DisplayName = "Hold")]
  public virtual bool? Hold { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the deposit is released.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the deposit is voided.
  /// </summary>
  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? Voided { get; set; }

  /// <summary>
  /// The status of the deposit, which the system assigns automatically.
  /// This is virtual field and has no representation in the database.
  /// The field can have one of the following values:
  /// <c>"H"</c>: On Hold;
  /// <c>"B"</c>: Balanced;
  /// <c>"R"</c>: Released;
  /// <c>"V"</c>: Voided.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("B")]
  [PXUIField]
  [CADepositStatus.List]
  public virtual string Status { get; set; }

  /// <summary>The currency of the deposit.</summary>
  /// <value>
  /// Corresponds to the currency of the cash account <see cref="P:PX.Objects.CA.CashAccount.CuryID" />.
  /// </value>
  [PXDBString(5, InputMask = ">LLLLL", IsUnicode = true)]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  [PXDefault(typeof (Search<CashAccount.curyID, Where<CashAccount.cashAccountID, Equal<Current<CADeposit.cashAccountID>>>>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID { get; set; }

  /// <summary>
  /// The identifier of the exchange rate record for the deposit.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>
  /// The total amount of the deposit, including the cash amount minus the charge total amount in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CADeposit.curyInfoID), typeof (CADeposit.tranAmt))]
  [PXFormula(typeof (Add<CADeposit.curyDetailTotal, Add<CADeposit.curyExtraCashTotal, Mult<CADeposit.curyChargeTotal, CADeposit.chargeMult>>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Amount", Enabled = false)]
  public virtual Decimal? CuryTranAmt { get; set; }

  /// <summary>
  /// The total amount of the deposit, including the cash amount minus the charge total amount in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tran Amount", Enabled = false)]
  public virtual Decimal? TranAmt { get; set; }

  /// <summary>
  /// The line total amount of the deposit in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CADeposit.curyInfoID), typeof (CADeposit.detailTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDetailTotal { get; set; }

  /// <summary>
  /// The line total amount of the deposit in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DetailTotal { get; set; }

  /// <summary>
  /// The total amount of any charges that apply to the deposit in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CADeposit.curyInfoID), typeof (CADeposit.chargeTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryChargeTotal { get; set; }

  /// <summary>
  /// The total amount of any charges that apply to the deposit in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ChargeTotal { get; set; }

  /// <summary>
  /// The total amount of cash to be deposited through this deposit in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CADeposit.curyInfoID), typeof (CADeposit.extraCashTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryExtraCashTotal { get; set; }

  /// <summary>
  /// The total amount of cash to be deposited through this deposit in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtraCashTotal { get; set; }

  /// <summary>
  /// The cash account (usually a Cash On Hand account) from which you want to also deposit some amount on the bank account.
  /// </summary>
  [CashAccount(null, typeof (Search<CashAccount.cashAccountID, Where<CashAccount.curyID, Equal<Current<CADeposit.curyID>>, And<CashAccount.cashAccountID, NotEqual<Current<CADeposit.cashAccountID>>>>>))]
  public virtual int? ExtraCashAccountID { get; set; }

  /// <summary>
  /// The control total of the deposit, which should be equal to <see cref="P:PX.Objects.CA.CADeposit.CuryTranAmt">the total amount in the selected currency</see>.
  /// </summary>
  [PXDBCurrency(typeof (CADeposit.curyInfoID), typeof (CADeposit.controlAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Total")]
  public virtual Decimal? CuryControlAmt { get; set; }

  /// <summary>
  /// The control total of the deposit, which should be equal to <see cref="P:PX.Objects.CA.CADeposit.TranAmt">the total amount in the base currency</see>.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ControlAmt { get; set; }

  /// <summary>
  /// The counter of detail lines.
  /// The field depends on <see cref="P:PX.Objects.CA.CADepositDetail.LineNbr" />.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  /// <summary>
  /// The counter of Charges detail lines.
  /// The field depends on <see cref="P:PX.Objects.CA.CADepositCharge.LineNbr" />.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntrCharge { get; set; }

  /// <summary>
  /// The identifier of the deposit-related cash transaction that recorded to the cash account (usually a bank account).
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CATran.TranID" /> field.
  /// </value>
  [PXDBLong]
  [DepositTranID]
  [PXSelector(typeof (Search<CATran.tranID>), DescriptionField = typeof (CATran.batchNbr))]
  public virtual long? TranID { get; set; }

  /// <summary>The identifier of the cash drop transaction.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CATran.TranID" /> field.
  /// The field can be empty.
  /// </value>
  [PXDBLong]
  [DepositCashTranID]
  [PXSelector(typeof (Search<CATran.tranID>), DescriptionField = typeof (CATran.batchNbr))]
  public virtual long? CashTranID { get; set; }

  /// <summary>
  /// The identifier of the cash transaction made for deposit charges.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CATran.TranID" /> field.
  /// </value>
  [PXDBLong]
  [DepositChargeTranID]
  [PXSelector(typeof (Search<CATran.tranID>), DescriptionField = typeof (CATran.batchNbr))]
  public virtual long? ChargeTranID { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the charges for the deposit will be booked as a separate transaction in GL.
  /// If the field is set to <c>false</c>, charges will be deducted from the deposit amount.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Separate Charges")]
  public virtual bool? ChargesSeparate { get; set; }

  /// <summary>
  /// A check box that indicates (if selected) that the deposit was cleared with the bank.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cleared")]
  public virtual bool? Cleared { get; set; }

  /// <summary>
  /// The date when the deposit has been cleared with the bank reconciliation statement.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Clear Date")]
  public virtual DateTime? ClearDate { get; set; }

  /// <summary>
  /// The workgroup to which the deposit is assigned for processing.
  /// </summary>
  [PXDBInt]
  [PXDefault]
  [PXCompanyTreeSelector]
  [PXUIField]
  public virtual int? WorkgroupID { get; set; }

  /// <summary>
  /// The owner of the deposit, which is the employee who controls the processing.
  /// </summary>
  [PXDefault]
  [Owner(typeof (CADeposit.workgroupID))]
  public virtual int? OwnerID { get; set; }

  [PXSearchable(4, "{0} {1}", new System.Type[] {typeof (CADeposit.tranType), typeof (CADeposit.refNbr)}, new System.Type[] {typeof (CADeposit.tranDesc), typeof (CADeposit.extRefNbr)}, NumberFields = new System.Type[] {typeof (CADeposit.refNbr)}, Line1Format = "{0}{1:d}{3}", Line1Fields = new System.Type[] {typeof (CADeposit.extRefNbr), typeof (CADeposit.tranDate), typeof (CADeposit.cashAccountID), typeof (PX.Objects.GL.Account.accountCD)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (CADeposit.tranDesc)})]
  [PXNote(DescriptionField = typeof (CADeposit.refNbr))]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <summary>
  /// The coefficient of the charge total amount in the calculation of the deposit total amount.
  /// The field depends on the <see cref="P:PX.Objects.CA.CADeposit.ChargesSeparate" /> field.
  /// The field is read-only; it is not displayed on any Acumatica ERP form.
  /// This is virtual field and has no representation in the database.
  /// </summary>
  /// <value>
  /// If the charges for the deposit are specified by a separate transaction (that is, if the <see cref="P:PX.Objects.CA.CADeposit.ChargesSeparate" /> field is set to <c>true</c>), the value of the field is <c>"decimal.Zero"</c>.
  /// If the charges for the deposit are deducted from the deposit amount (that is, if the <see cref="P:PX.Objects.CA.CADeposit.ChargesSeparate" /> field is set to <c>false</c>), the value of the field is <c>"decimal.MinusOne"</c>.
  /// </value>
  [PXDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Total")]
  public virtual Decimal? ChargeMult
  {
    [PXDependsOnFields(new System.Type[] {typeof (CADeposit.chargesSeparate)})] get
    {
      return new Decimal?(this.ChargesSeparate.GetValueOrDefault() ? 0M : -1M);
    }
  }

  [PXString]
  [PXFormula(typeof (SmartJoin<Space, Selector<CADeposit.cashAccountID, CashAccount.cashAccountCD>, Selector<CADeposit.cashAccountID, CashAccount.descr>>))]
  public string FormCaptionDescription { get; set; }

  public class PK : PrimaryKeyOf<CADeposit>.By<CADeposit.tranType, CADeposit.refNbr>
  {
    public static CADeposit Find(
      PXGraph graph,
      string tranType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (CADeposit) PrimaryKeyOf<CADeposit>.By<CADeposit.tranType, CADeposit.refNbr>.FindBy(graph, (object) tranType, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CADeposit>.By<CADeposit.cashAccountID>
    {
    }

    public class CashDropAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CADeposit>.By<CADeposit.extraCashAccountID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CADeposit>.By<CADeposit.branchID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CADeposit>.By<CADeposit.curyID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CADeposit>.By<CADeposit.curyInfoID>
    {
    }

    public class DepositRelatedCashTransaction : 
      PrimaryKeyOf<CATran>.By<CATran.cashAccountID, CATran.tranID>.ForeignKeyOf<CADeposit>.By<CADeposit.cashAccountID, CADeposit.tranID>
    {
    }

    public class CashDropTransaction : 
      PrimaryKeyOf<CATran>.By<CATran.cashAccountID, CATran.tranID>.ForeignKeyOf<CADeposit>.By<CADeposit.cashAccountID, CADeposit.cashTranID>
    {
    }

    public class DepositChargeCashTransaction : 
      PrimaryKeyOf<CATran>.By<CATran.cashAccountID, CATran.tranID>.ForeignKeyOf<CADeposit>.By<CADeposit.cashAccountID, CADeposit.chargeTranID>
    {
    }

    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<CADeposit>.By<CADeposit.workgroupID>
    {
    }

    public class Owner : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CADeposit>.By<CADeposit.ownerID>
    {
    }
  }

  public class Events : PXEntityEventBase<CADeposit>.Container<CADeposit.Events>
  {
    public PXEntityEvent<CADeposit> ReleaseDocument;
    public PXEntityEvent<CADeposit> VoidDocument;
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADeposit.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADeposit.refNbr>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADeposit.extRefNbr>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CADeposit.cashAccountID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CADeposit.branchID>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CADeposit.tranDate>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADeposit.drCr>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADeposit.tranDesc>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADeposit.tranPeriodID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADeposit.finPeriodID>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CADeposit.hold>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CADeposit.released>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CADeposit.voided>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADeposit.status>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADeposit.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CADeposit.curyInfoID>
  {
  }

  public abstract class curyTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CADeposit.curyTranAmt>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CADeposit.tranAmt>
  {
  }

  public abstract class curyDetailTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADeposit.curyDetailTotal>
  {
  }

  public abstract class detailTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CADeposit.detailTotal>
  {
  }

  public abstract class curyChargeTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADeposit.curyChargeTotal>
  {
  }

  public abstract class chargeTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CADeposit.chargeTotal>
  {
  }

  public abstract class curyExtraCashTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADeposit.curyExtraCashTotal>
  {
  }

  public abstract class extraCashTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADeposit.extraCashTotal>
  {
  }

  public abstract class extraCashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CADeposit.extraCashAccountID>
  {
  }

  public abstract class curyControlAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADeposit.curyControlAmt>
  {
  }

  public abstract class controlAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CADeposit.controlAmt>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CADeposit.lineCntr>
  {
  }

  public abstract class lineCntrCharge : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CADeposit.lineCntrCharge>
  {
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CADeposit.tranID>
  {
  }

  public abstract class cashTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CADeposit.cashTranID>
  {
  }

  public abstract class chargeTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CADeposit.chargeTranID>
  {
  }

  public abstract class chargesSeparate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CADeposit.chargesSeparate>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CADeposit.cleared>
  {
  }

  public abstract class clearDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CADeposit.clearDate>
  {
  }

  /// <summary>
  /// The reference number of the batch.
  /// The field is used as a navigation link to the Journal Transactions(GL301000) form, where you can view batch details.
  /// The value of the field is filled in by the <see cref="M:PX.Objects.CA.CADepositEntry.CADeposit_TranID_CATran_BatchNbr_FieldSelecting(PX.Data.PXCache,PX.Data.PXFieldSelectingEventArgs)" /> method.
  /// </summary>
  public abstract class tranID_CATran_batchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CADeposit.tranID_CATran_batchNbr>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CADeposit.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CADeposit.ownerID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CADeposit.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CADeposit.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CADeposit.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CADeposit.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CADeposit.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CADeposit.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CADeposit.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CADeposit.Tstamp>
  {
  }

  public abstract class chargeMult : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CADeposit.chargeMult>
  {
  }
}

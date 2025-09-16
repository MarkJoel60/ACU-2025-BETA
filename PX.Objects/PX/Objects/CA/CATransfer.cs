// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATransfer
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
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// The main properties of funds transfers and their classes.
/// Funds transfers are edited on the Funds Transfers (CA301000) form
/// (which corresponds to the <see cref="T:PX.Objects.CA.CashTransferEntry" /> graph).
/// </summary>
[PXCacheName("Transfer")]
[PXPrimaryGraph(typeof (CashTransferEntry))]
[PXProjection(typeof (Select2<CATransfer, InnerJoin<CashAccount, On<CATransfer.outAccountID, Equal<CashAccount.cashAccountID>>, InnerJoin<PX.Objects.CA.Alias.CashAccountAlias, On<CATransfer.inAccountID, Equal<PX.Objects.CA.Alias.CashAccountAlias.cashAccountID>>>>>), Persistent = true)]
[Serializable]
public class CATransfer : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ICADocument
{
  protected bool? _Hold;
  protected bool? _Released;

  /// <summary>
  /// The type of the document.
  /// The field implements the member of the <see cref="T:PX.Objects.CA.ICADocument" /> interface.
  /// </summary>
  /// <value>
  /// Always return <see cref="F:PX.Objects.CA.CATranType.CATransfer" />
  /// </value>
  public 
  #nullable disable
  string DocType => "CT%";

  /// <summary>
  /// The unique identifier of the transfer.
  /// The field implements the member of the <see cref="T:PX.Objects.CA.ICADocument" /> interface.
  /// </summary>
  public string RefNbr => this.TransferNbr;

  /// <summary>
  /// The user-friendly unique identifier of the transfer.
  /// This field is the auto-numbering key field.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (CATransfer.transferNbr))]
  [AutoNumber(typeof (CASetup.transferNumberingID), typeof (CATransfer.inDate))]
  public virtual string TransferNbr { get; set; }

  /// <summary>
  /// A detailed description for the transfer transaction. An alphanumeric string of up to 60 characters may be used.
  /// </summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Descr { get; set; }

  /// <summary>
  /// The identifier of the source <see cref="T:PX.Objects.CA.CashAccount">cash account</see> from which the funds are transferred.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.CashAccount.CashAccountID" /> field.
  /// </value>
  [PXDefault]
  [TransferCashAccount(PairCashAccount = typeof (CATransfer.inAccountID), DescriptionField = typeof (CashAccount.descr))]
  public virtual int? OutAccountID { get; set; }

  /// <summary>
  /// The identifier of the destination <see cref="T:PX.Objects.CA.CashAccount">cash account</see> to which the funds are transferred.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.CashAccount.CashAccountID" /> field.
  /// </value>
  [PXDefault]
  [TransferCashAccount(PairCashAccount = typeof (CATransfer.outAccountID), DisplayName = "Destination Account", DescriptionField = typeof (CashAccount.descr))]
  public virtual int? InAccountID { get; set; }

  /// <summary>
  /// The identifier of the exchange rate record for the outcoming amount.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo]
  [PXVirtualSelector(typeof (PX.Objects.CM.CurrencyInfo.curyInfoID))]
  public virtual long? OutCuryInfoID { get; set; }

  /// <summary>
  /// The identifier of the exchange rate record for the incoming amount.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo]
  public virtual long? InCuryInfoID { get; set; }

  /// <summary>
  /// The currency of denomination for the destination cash account.
  /// </summary>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Destination Currency", Enabled = false)]
  [PXDefault(typeof (Search<CashAccount.curyID, Where<CashAccount.cashAccountID, Equal<Current<CATransfer.inAccountID>>>>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string InCuryID { get; set; }

  /// <summary>
  /// The currency of denomination for the source cash account.
  /// </summary>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Source Currency", Enabled = false)]
  [PXDefault(typeof (Search<CashAccount.curyID, Where<CashAccount.cashAccountID, Equal<Current<CATransfer.outAccountID>>>>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string OutCuryID { get; set; }

  /// <summary>
  /// The amount of the transfer outcomes from the source cash account (in the specified currency).
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Source Amount")]
  [PXDBCurrency(typeof (CATransfer.outCuryInfoID), typeof (CATransfer.tranOut))]
  public virtual Decimal? CuryTranOut { get; set; }

  /// <summary>
  /// The amount of the transfer incomes to the destination cash account (in the specified currency).
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Destination Amount")]
  [PXDBCurrency(typeof (CATransfer.inCuryInfoID), typeof (CATransfer.tranIn))]
  public virtual Decimal? CuryTranIn { get; set; }

  /// <summary>
  /// The amount of the transfer outcomes from the source cash account (in the base currency).
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Currency Amount", Enabled = false)]
  public virtual Decimal? TranOut { get; set; }

  /// <summary>
  /// The amount of the transfer incomes to the destination cash account (in the base currency).
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Currency Amount", Enabled = false)]
  public virtual Decimal? TranIn { get; set; }

  /// <summary>The date of the transfer receipt.</summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? InDate { get; set; }

  /// <summary>
  /// The date of the transaction (when funds were withdrawn from the source cash account).
  /// </summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Transfer Date")]
  public virtual DateTime? OutDate { get; set; }

  [PXFormula(typeof (Default<CATransfer.inAccountID>))]
  [Branch(typeof (Search<CashAccount.branchID, Where<CashAccount.cashAccountID, Equal<Current<CATransfer.inAccountID>>>>), null, true, true, true)]
  [PXUIField(DisplayName = "Destination Branch")]
  public virtual int? InBranchID { get; set; }

  [PXFormula(typeof (Default<CATransfer.outAccountID>))]
  [Branch(typeof (Search<CashAccount.branchID, Where<CashAccount.cashAccountID, Equal<Current<CATransfer.outAccountID>>>>), null, true, true, true)]
  [PXUIField(DisplayName = "Source Branch")]
  public virtual int? OutBranchID { get; set; }

  [PeriodID(null, null, null, true)]
  public virtual string InTranPeriodID { get; set; }

  [CAOpenPeriod(typeof (CATransfer.inDate), typeof (CATransfer.inAccountID), typeof (Selector<CATransfer.inAccountID, CashAccount.branchID>), null, null, null, true, typeof (CATransfer.inTranPeriodID))]
  [PXUIField]
  public virtual string InPeriodID { get; set; }

  [PeriodID(null, null, null, true)]
  public virtual string OutTranPeriodID { get; set; }

  [CAOpenPeriod(typeof (CATransfer.outDate), typeof (CATransfer.outAccountID), typeof (Selector<CATransfer.outAccountID, CashAccount.branchID>), null, null, null, true, typeof (CATransfer.outTranPeriodID))]
  [PXUIField]
  public virtual string OutPeriodID { get; set; }

  /// <summary>
  /// The reference number of the transfer for the source cash account.
  /// This is a number provided by an external bank or organization.
  /// This field is entered manually.
  /// </summary>
  [PXDBString(40, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Document Ref.")]
  public virtual string OutExtRefNbr { get; set; }

  /// <summary>
  /// The reference number of the transfer for the target cash account.
  /// This is a number provided by an external bank or organization.
  /// The value of the field is entered by a user.
  /// </summary>
  [PXDBString(40, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Document Ref.")]
  public virtual string InExtRefNbr { get; set; }

  /// <summary>
  /// The unique identifier of the outcoming CA transaction.
  /// </summary>
  /// 
  ///             /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.CATran.TranID" /> field.
  /// </value>
  [PXDBLong]
  [TransferCashTranID]
  [PXSelector(typeof (Search<CATran.tranID>), DescriptionField = typeof (CATran.batchNbr))]
  public virtual long? TranIDOut { get; set; }

  /// <summary>The unique identifier of the incoming CA transaction.</summary>
  /// 
  ///             /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.CATran.TranID" /> field.
  /// </value>
  [PXDBLong]
  [TransferCashTranID]
  [PXSelector(typeof (Search<CATran.tranID>), DescriptionField = typeof (CATran.batchNbr))]
  public virtual long? TranIDIn { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ExpenseCntr { get; set; }

  /// <summary>
  /// A read-only box that displays the difference between the amount in the base currency specified for the source account
  /// and the amount in the base currency resulting for the destination cash account,
  /// for cases when the source and destination currencies are different.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "RGOL", Enabled = false)]
  public virtual Decimal? RGOLAmt { get; set; }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the transfer is on hold. The value of the field can be set to <c>false</c> only for balanced transfers.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (Search<CASetup.holdEntry>))]
  [PXUIField(DisplayName = "Hold")]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the transfer is released.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>The number of the original transfer.</summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (CATransfer.transferNbr))]
  public virtual string OrigTransferNbr { get; set; }

  /// <summary>
  /// The read-only field, reflecting the number of transactions in the system, which reverse this transaction.
  /// </summary>
  /// <value>
  /// This field is populated only by the <see cref="T:PX.Objects.CA.CashTransferEntry" /> graph, which corresponds to the Funds Transfers CA301000 form.
  /// </value>
  [PXInt]
  [PXUIField(DisplayName = "Reversing Transactions", Visible = false, Enabled = false, IsReadOnly = true)]
  public int? ReverseCount { get; set; }

  /// <summary>
  /// A global unique identifier of the transfer in Acumatica ERP.
  /// The field is used for attachments for the transfer (such as notes, files).
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Data.Note.NoteID" /> field.
  /// </value>
  [PXSearchable(4, "CA Transfer: {0}", new Type[] {typeof (CATransfer.transferNbr)}, new Type[] {typeof (CATransfer.descr), typeof (CATransfer.outExtRefNbr), typeof (CATransfer.inExtRefNbr)}, NumberFields = new Type[] {typeof (CATransfer.transferNbr)}, Line1Format = "{0}{1}", Line1Fields = new Type[] {typeof (CATransfer.outExtRefNbr), typeof (CATransfer.inExtRefNbr)}, Line2Format = "{0}", Line2Fields = new Type[] {typeof (CATransfer.descr)})]
  [PXNote(DescriptionField = typeof (CATransfer.transferNbr))]
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

  /// <summary>The status of the transfer.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.CA.CATransferStatus.ListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [CATransferStatus.List]
  public virtual string Status { get; set; }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that this outcoming transaction has been cleared.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Cleared")]
  [PXDefault(false)]
  public virtual bool? ClearedOut { get; set; }

  /// <summary>
  /// The date when the outcoming transaction was cleared in the process of reconciliation.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Clear Date", Required = false)]
  public virtual DateTime? ClearDateOut { get; set; }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that this incoming transaction has been cleared.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Cleared")]
  [PXDefault(false)]
  public virtual bool? ClearedIn { get; set; }

  /// <summary>
  /// The date when the incoming transaction was cleared in the process of reconciliation.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Clear Date", Required = false)]
  public virtual DateTime? ClearDateIn { get; set; }

  /// <summary>The actual balance of the target cash account.</summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (CATransfer.inCuryInfoID))]
  [PXUIField(DisplayName = "Available Balance", Enabled = false)]
  [CashBalance(typeof (CATransfer.inAccountID))]
  public virtual Decimal? CashBalanceIn { get; set; }

  /// <summary>The actual balance of the source cash account.</summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (CATransfer.outCuryInfoID))]
  [PXUIField(DisplayName = "Available Balance", Enabled = false)]
  [CashBalance(typeof (CATransfer.outAccountID))]
  public virtual Decimal? CashBalanceOut { get; set; }

  /// <summary>
  /// The balance of the target account, as recorded in the General Ledger.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (CATransfer.inCuryInfoID))]
  [PXUIField(DisplayName = "GL Balance", Enabled = false)]
  [GLBalance(typeof (CATransfer.inAccountID), null, typeof (CATransfer.inDate))]
  public virtual Decimal? InGLBalance { get; set; }

  /// <summary>
  /// A read-only box displaying the balance of the source account recorded in the General Ledger
  /// for the financial period that includes the transfer date.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (CATransfer.outCuryInfoID))]
  [PXUIField(DisplayName = "GL Balance", Enabled = false)]
  [GLBalance(typeof (CATransfer.outAccountID), null, typeof (CATransfer.outDate))]
  public virtual Decimal? OutGLBalance { get; set; }

  /// <summary>
  /// The batch number for the transfer. Only released transfers have batch numbers.
  /// </summary>
  [PXFormula(typeof (Selector<CATransfer.tranIDOut, CATran.batchNbr>))]
  [PXSelector(typeof (Search<PX.Objects.GL.Batch.batchNbr, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleCA>>>))]
  [PXString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Batch Number", Enabled = false)]
  public virtual string TranIDOut_CATran_batchNbr { get; set; }

  /// <summary>
  /// The number of the batch that contains the transaction for the target account in the General Ledger.
  /// </summary>
  [PXFormula(typeof (Selector<CATransfer.tranIDIn, CATran.batchNbr>))]
  [PXSelector(typeof (Search<PX.Objects.GL.Batch.batchNbr, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleCA>>>))]
  [PXString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Batch Number", Enabled = false)]
  public virtual string TranIDIn_CATran_batchNbr { get; set; }

  /// <summary>
  /// The special multi-currency asset account used (when necessary) as an intermediate account for currency conversions performed during funds transfers.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="T:PX.Objects.GL.Account.accountID" /> field.
  /// </value>
  [PXDefault(typeof (CASetup.transitAcctId))]
  [PXNonCashAccount(DisplayName = "Cash-In-Transit Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  public virtual int? TransitAcctID { get; set; }

  /// <summary>
  /// The special multi-currency asset subaccount used (when necessary) as an intermediate account for currency conversions performed during funds transfers.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault(typeof (CASetup.transitSubID))]
  [SubAccount(typeof (CASetup.transitAcctId), DisplayName = "Cash-In-Transit Subaccount", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  public virtual int? TransitSubID { get; set; }

  /// <summary>The identifier of the base currency for the transfer.</summary>
  [PXFormula(typeof (Selector<CATransfer.outCuryInfoID, PX.Objects.CM.CurrencyInfo.baseCuryID>), Persistent = true)]
  [PXString(5, IsUnicode = true)]
  [PXUIField(Enabled = false)]
  public virtual string BaseCuryID { get; set; }

  /// <summary>
  /// The sum of total amounts of the charges linked to the transfer (in the base currency).
  /// </summary>
  [PXBaseCury]
  [PXUIField(DisplayName = "Total Charges", Enabled = false)]
  public virtual Decimal? TotalExpenses { get; set; }

  public class Events : PXEntityEventBase<CATransfer>.Container<CATransfer.Events>
  {
    public PXEntityEvent<CATransfer> ReleaseDocument;
  }

  public class PK : PrimaryKeyOf<CATransfer>.By<CATransfer.transferNbr>
  {
    public static CATransfer Find(PXGraph graph, string transferNbr, PKFindOptions options = 0)
    {
      return (CATransfer) PrimaryKeyOf<CATransfer>.By<CATransfer.transferNbr>.FindBy(graph, (object) transferNbr, options);
    }
  }

  public static class FK
  {
    public class SourceCashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CATransfer>.By<CATransfer.outAccountID>
    {
    }

    public class DestinationCashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CATransfer>.By<CATransfer.inAccountID>
    {
    }

    public class SourceCurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CATransfer>.By<CATransfer.outCuryInfoID>
    {
    }

    public class DestinationCurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CATransfer>.By<CATransfer.inCuryInfoID>
    {
    }

    public class SourceCurrency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CATransfer>.By<CATransfer.inCuryID>
    {
    }

    public class DestinationCurrency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CATransfer>.By<CATransfer.outCuryID>
    {
    }

    public class SourceBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CATransfer>.By<CATransfer.outBranchID>
    {
    }

    public class DestinationBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CATransfer>.By<CATransfer.inBranchID>
    {
    }

    public class SourceCashAccountTransaction : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CATransfer>.By<CATransfer.tranIDOut>
    {
    }

    public class DestinationCashAccountTransaction : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CATransfer>.By<CATransfer.tranIDIn>
    {
    }

    public class CashInTransitAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CATransfer>.By<CATransfer.transitAcctID>
    {
    }

    public class CashInTransitSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CATransfer>.By<CATransfer.transitSubID>
    {
    }
  }

  public abstract class transferNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATransfer.transferNbr>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATransfer.descr>
  {
  }

  public abstract class outAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATransfer.outAccountID>
  {
  }

  public abstract class inAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATransfer.inAccountID>
  {
  }

  public abstract class outCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CATransfer.outCuryInfoID>
  {
  }

  public abstract class inCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CATransfer.inCuryInfoID>
  {
  }

  public abstract class inCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATransfer.inCuryID>
  {
  }

  public abstract class outCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATransfer.outCuryID>
  {
  }

  public abstract class curyTranOut : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATransfer.curyTranOut>
  {
  }

  public abstract class curyTranIn : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATransfer.curyTranIn>
  {
  }

  public abstract class tranOut : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATransfer.tranOut>
  {
  }

  public abstract class tranIn : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATransfer.tranIn>
  {
  }

  public abstract class inDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CATransfer.inDate>
  {
  }

  public abstract class outDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CATransfer.outDate>
  {
  }

  public abstract class inBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATransfer.inBranchID>
  {
  }

  public abstract class outBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATransfer.outBranchID>
  {
  }

  public abstract class inTranPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class inPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATransfer.inPeriodID>
  {
  }

  public abstract class outTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CATransfer.outTranPeriodID>
  {
  }

  public abstract class outPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATransfer.outPeriodID>
  {
  }

  public abstract class outExtRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATransfer.outExtRefNbr>
  {
  }

  public abstract class inExtRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATransfer.inExtRefNbr>
  {
  }

  public abstract class tranIDOut : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CATransfer.tranIDOut>
  {
  }

  public abstract class tranIDIn : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CATransfer.tranIDIn>
  {
  }

  public abstract class expenseCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATransfer.expenseCntr>
  {
  }

  public abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATransfer.rGOLAmt>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CATransfer.hold>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CATransfer.released>
  {
  }

  public abstract class origTransferNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CATransfer.origTransferNbr>
  {
  }

  public abstract class reverseCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATransfer.reverseCount>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CATransfer.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CATransfer.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CATransfer.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CATransfer.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CATransfer.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CATransfer.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CATransfer.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CATransfer.Tstamp>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATransfer.status>
  {
  }

  public abstract class clearedOut : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CATransfer.clearedOut>
  {
  }

  public abstract class clearDateOut : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CATransfer.clearDateOut>
  {
  }

  public abstract class clearedIn : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CATransfer.clearedIn>
  {
  }

  public abstract class clearDateIn : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CATransfer.clearDateIn>
  {
  }

  public abstract class cashBalanceIn : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATransfer.cashBalanceIn>
  {
  }

  public abstract class cashBalanceOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CATransfer.cashBalanceOut>
  {
  }

  public abstract class inGLBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATransfer.inGLBalance>
  {
  }

  public abstract class outGLBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATransfer.outGLBalance>
  {
  }

  /// <summary>
  /// The batch number for the transfer. Only released transfers have batch numbers.
  /// The field is used as a link to the batch that contains the transaction for the source account in the General Ledger.
  /// This is a virtual field, which is filled in from the <see cref="T:PX.Objects.CA.CATran.batchNbr" /> field (<see cref="!:CashTransferEntry.CATransfer_TranIDOut_CATran_BatchNbr_FieldSelecting" />).
  /// </summary>
  public abstract class tranIDOut_CATran_batchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CATransfer.tranIDOut_CATran_batchNbr>
  {
  }

  /// <summary>
  /// The number of the batch that contains the transaction for the target account in the General Ledger.
  /// Only released transfers have batch numbers.
  /// This is a virtual field, which is filled in from the <see cref="T:PX.Objects.CA.CATran.batchNbr" /> field (<see cref="!:CashTransferEntry.CATransfer_TranIDIn_CATran_BatchNbr_FieldSelecting" />).
  /// </summary>
  public abstract class tranIDIn_CATran_batchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CATransfer.tranIDIn_CATran_batchNbr>
  {
  }

  public abstract class transitAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATransfer.transitAcctID>
  {
  }

  public abstract class transitSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATransfer.transitSubID>
  {
  }

  /// <summary>
  /// The identifier of the base currency for the transfer.
  /// This is a virtual field, which is filled in from the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo.baseCuryID" /> field.
  /// </summary>
  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATransfer.baseCuryID>
  {
  }

  /// <summary>
  /// The sum of total amounts of the charges linked to the transfer (in the base currency).
  /// </summary>
  public abstract class totalExpenses : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATransfer.totalExpenses>
  {
  }
}

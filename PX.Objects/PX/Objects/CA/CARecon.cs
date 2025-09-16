// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CARecon
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA.Descriptor;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// The header of the reconciliation statement.
/// Reconciliation statements are edited on the Reconciliation Statements (CA302000) form
/// (which corresponds to the <see cref="T:PX.Objects.CA.CAReconEntry" /> graph).
/// </summary>
[PXCacheName("Reconciliation Statement")]
[PXPrimaryGraph(typeof (CAReconEntry))]
[PXGroupMask(typeof (InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<CARecon.cashAccountID>>, InnerJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<CashAccount.accountID>, And<Match<PX.Objects.GL.Account, Current<AccessInfo.userName>>>>, InnerJoin<PX.Objects.GL.Sub, On<PX.Objects.GL.Sub.subID, Equal<CashAccount.subID>, And<Match<PX.Objects.GL.Sub, Current<AccessInfo.userName>>>>>>>))]
[Serializable]
public class CARecon : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssign, INotable
{
  protected bool? _Reconciled;
  protected bool? _Voided;
  protected bool? _Hold;
  protected 
  #nullable disable
  string _Status;

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.CA.CashAccount">cash account</see> under reconciliation.
  /// </summary>
  [PXDefault]
  [CashAccount(null, typeof (Search<CashAccount.cashAccountID, Where<CashAccount.reconcile, Equal<boolTrue>, And<Match<Current<AccessInfo.userName>>>>>))]
  public virtual int? CashAccountID { get; set; }

  /// <summary>
  /// The identification number of the reconciliation statement,
  /// which the system assigns when the user saves the statement.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [AutoNumber(typeof (Search<CashAccount.reconNumberingID, Where<CashAccount.cashAccountID, Equal<Current<CARecon.cashAccountID>>>>), typeof (CARecon.reconDate), "The number of the next reconciliation statement cannot be generated, because the reconciliation date is not set.")]
  [PXUIField]
  [PXSelector(typeof (Search<CARecon.reconNbr, Where<CARecon.cashAccountID, Equal<Optional<CARecon.cashAccountID>>>, OrderBy<Desc<CARecon.reconNbr>>>))]
  public virtual string ReconNbr { get; set; }

  /// <summary>
  /// The date when the reconciliation statement was released and closed. A user can change the date up to the release.
  /// </summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? ReconDate { get; set; }

  /// <summary>
  /// The date of the most recent <see cref="T:PX.Objects.CA.CARecon" /> for this cash account, if one exists.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Last Reconciliation Date", Enabled = false)]
  public virtual DateTime? LastReconDate { get; set; }

  /// <summary>
  /// The latest date for <see cref="T:PX.Objects.CA.CAReconEntry.CATranExt">documents</see> to be loaded to the list.
  /// </summary>
  [PXDate]
  [CAOptimizeLoadDate(TransactionsLimit = 5000)]
  [PXUIField]
  public virtual DateTime? LoadDocumentsTill { get; set; }

  /// <summary>
  /// The field indicates if user set LoadDocumentsTill, or was it set by <see cref="T:PX.Objects.CA.Descriptor.CAOptimizeLoadDateAttribute" />.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (False))]
  public virtual bool? IsUserLoadDocumentsTill { get; set; }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the reconciliation statement was completed and cannot be edited anymore.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Reconciled", Enabled = false)]
  [PXDefault(false)]
  public virtual bool? Reconciled
  {
    get => this._Reconciled;
    set => this._Reconciled = value;
  }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the reconciliation statement was voided.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Voided", Enabled = false)]
  [PXDefault(false)]
  public virtual bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the reconciliation statement is on hold and can be saved unbalanced.
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
  /// The balance of the previous <see cref="T:PX.Objects.CA.CARecon">reconciliation statement</see>
  /// in the <see cref="P:PX.Objects.CA.CARecon.CuryID">selected currency</see>.
  /// </summary>
  /// <value>
  /// The value of the field defaults to the <see cref="P:PX.Objects.CA.CARecon.CuryReconciledBalance">reconciled balance</see>
  /// of the previous <see cref="T:PX.Objects.CA.CARecon">statement</see>.
  /// </value>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCury(typeof (CARecon.curyID))]
  [PXUIField(DisplayName = "Beginning Balance", Enabled = false)]
  public virtual Decimal? CuryBegBalance { get; set; }

  /// <summary>
  /// The balance of the current bank statement in the selected currency,
  /// which user should enter manually for the current reconciliation statement.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCury(typeof (CARecon.curyID), typeof (CashAccount.branchID))]
  [PXUIField(DisplayName = "Statement Balance")]
  public virtual Decimal? CuryBalance { get; set; }

  /// <summary>
  /// The total amount of reconciled <see cref="P:PX.Objects.CA.CAReconEntry.CATranExt.CuryReconciledDebit">receipts</see>
  /// on the statement in the selected currency.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (CARecon.curyInfoID), typeof (CARecon.reconciledDebits))]
  [PXUIField(DisplayName = "Reconciled Receipts", Enabled = false)]
  public virtual Decimal? CuryReconciledDebits { get; set; }

  /// <summary>
  /// The total amount of reconciled <see cref="P:PX.Objects.CA.CAReconEntry.CATranExt.ReconciledDebit">receipts</see>
  /// on the statement in the base currency.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Reconciled Receipts", Enabled = false, Required = false)]
  public virtual Decimal? ReconciledDebits { get; set; }

  /// <summary>
  /// The total amount of reconciled <see cref="P:PX.Objects.CA.CAReconEntry.CATranExt.CuryReconciledCredit">disbursements</see>
  /// on the statement in the selected currency.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (CARecon.curyInfoID), typeof (CARecon.reconciledCredits))]
  [PXUIField(DisplayName = "Reconciled Disb.", Enabled = false)]
  public virtual Decimal? CuryReconciledCredits { get; set; }

  /// <summary>
  /// The total amount of reconciled <see cref="P:PX.Objects.CA.CAReconEntry.CATranExt.ReconciledCredit">disbursements</see>
  /// on the statement in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Reconciled Disb.", Enabled = false, Required = false)]
  public virtual Decimal? ReconciledCredits { get; set; }

  /// <summary>
  /// The beginning balance of the statement plus the cleared receipts minus the cleared disbursements.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Reconciled Balance", Enabled = false)]
  [PXDBCury(typeof (CARecon.curyID))]
  [PXFormula(typeof (Add<CARecon.curyBegBalance, CARecon.curyReconciledTurnover>))]
  public virtual Decimal? CuryReconciledBalance { get; set; }

  /// <summary>
  /// Turnover of the reconciliation statement in the selected currency.
  /// </summary>
  /// <value>
  /// Equals the following value: <see cref="P:PX.Objects.CA.CARecon.CuryReconciledDebits" /> minus <see cref="P:PX.Objects.CA.CARecon.CuryReconciledCredits" />.
  /// </value>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Reconciled Turnover", Enabled = false)]
  [PXFormula(typeof (Sub<CARecon.curyReconciledDebits, CARecon.curyReconciledCredits>))]
  public virtual Decimal? CuryReconciledTurnover { get; set; }

  /// <summary>
  /// Turnover of the reconciliation statement in the base currency.
  /// </summary>
  /// <value>
  /// Equals the following value: <see cref="P:PX.Objects.CA.CARecon.ReconciledDebits" /> minus <see cref="P:PX.Objects.CA.CARecon.ReconciledCredits" />.
  /// </value>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Reconciled Turnover", Enabled = false)]
  [PXFormula(typeof (Sub<CARecon.reconciledDebits, CARecon.reconciledCredits>))]
  public virtual Decimal? ReconciledTurnover { get; set; }

  /// <summary>
  /// The difference between the <see cref="P:PX.Objects.CA.CARecon.CuryReconciledBalance">reconciled balance</see>
  /// and the <see cref="P:PX.Objects.CA.CARecon.CuryBalance">statement balance</see>.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCury(typeof (CARecon.curyID))]
  [PXUIField(DisplayName = "Difference", Enabled = false)]
  [PXFormula(typeof (Sub<CARecon.curyBalance, CARecon.curyReconciledBalance>))]
  public virtual Decimal? CuryDiffBalance { get; set; }

  /// <summary>
  /// The currency of the <see cref="T:PX.Objects.CA.CashAccount">cash account</see>.
  /// </summary>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the reconciliation statement.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:PX.Objects.CM.CurrencyInfo.CurrencyInfoID" /> field. The value is generated automatically.
  /// </value>
  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>The current status of the statement.</summary>
  /// <value>
  /// This field can have one of the values defined
  /// by <see cref="T:PX.Objects.CA.CADocStatus.ListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [CADocStatus.List]
  [PXUIField]
  [PXDependsOnFields(new Type[] {typeof (CARecon.hold), typeof (CARecon.approved), typeof (CARecon.rejected), typeof (CARecon.reconciled), typeof (CARecon.voided)})]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  /// <summary>
  /// The number of <see cref="P:PX.Objects.CA.CAReconEntry.CATranExt.CountDebit">reconciled receipts</see>.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Receipt Count", Enabled = false)]
  public virtual int? CountDebit { get; set; }

  /// <summary>
  /// The number of <see cref="P:PX.Objects.CA.CAReconEntry.CATranExt.CountCredit">reconciled disbursements</see>.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Disbursement Count", Enabled = false)]
  public virtual int? CountCredit { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the direct <see cref="T:PX.Objects.CA.CATran">transaction</see>
  /// and the reversing transaction are considered as a single transaction.
  /// </summary>
  /// <value>
  /// Defaults to the value of <see cref="P:PX.Objects.CA.CASetup.SkipVoided" />.
  /// </value>
  /// <remarks>
  /// The <see cref="!:CAReconEntry.Skip(CATran, CATran, bool)" /> method is used to determine
  /// whether both transactions (<see cref="T:PX.Objects.CA.CATran" />) are shown on the form or only the aggregated one is shown.
  /// </remarks>
  [PXDBBool]
  [PXDefault(typeof (CASetup.skipVoided))]
  [PXUIField(DisplayName = "Voided Transactions Are Skipped", Enabled = false, Visible = false)]
  public virtual bool? SkipVoided { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that all <see cref="T:PX.Objects.CA.CATran">transactions</see>
  /// included in the <see cref="T:PX.Objects.CA.CABatch">batch</see> are considered as a single transaction.
  /// </summary>
  /// <value>
  /// Defaults to the value of <see cref="P:PX.Objects.CA.CashAccount.MatchToBatch" />.
  /// </value>
  [PXDBBool]
  [PXDefault(typeof (Search<CashAccount.matchToBatch, Where<CashAccount.cashAccountID, Equal<Current<CARecon.cashAccountID>>>>))]
  [PXUIField(DisplayName = "Bank Transactions Are Matched to Batch Payments", Enabled = false, Visible = false)]
  public virtual bool? ShowBatchPayments { get; set; }

  /// <summary>
  /// An indicator of whether the current reconciliation statement has been approved.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Approved { get; set; }

  /// <summary>
  /// An indicator of whether the current reconciliation statement has been rejected.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Rejected { get; set; }

  /// <summary>
  /// An indicator of whether the current reconciliation statement should be excluded from the approval process.
  /// This property is maintained on the graph level.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? ExcludeFromApproval { get; set; }

  /// <summary>The ID of the user who created the cash transaction.</summary>
  [PXDBInt]
  [PXDefault(typeof (Search<PX.Objects.EP.EPEmployee.bAccountID, Where<PX.Objects.EP.EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>))]
  [PXSubordinateSelector]
  [PXUIField]
  public virtual int? EmployeeID { get; set; }

  /// <summary>
  /// The ID of the workgroup that was assigned to approve the transaction.
  /// </summary>
  [PXInt]
  [PXSelector(typeof (Search<EPCompanyTree.workGroupID>), SubstituteKey = typeof (EPCompanyTree.description))]
  [PXUIField(DisplayName = "Approval Workgroup ID", Enabled = false)]
  public virtual int? WorkgroupID { get; set; }

  /// <summary>
  /// The ID of the employee who was assigned to approve the transaction.
  /// </summary>
  [Owner(IsDBField = false, DisplayName = "Approver", Enabled = false)]
  public virtual int? OwnerID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXNote(DescriptionField = typeof (CARecon.reconNbr))]
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

  private void SetStatus()
  {
    if (this._Reconciled.GetValueOrDefault())
      this._Status = "C";
    else if (this._Voided.GetValueOrDefault())
      this._Status = "V";
    else if (this._Hold.GetValueOrDefault())
      this._Status = "H";
    else
      this._Status = "B";
  }

  public class PK : PrimaryKeyOf<CARecon>.By<CARecon.cashAccountID, CARecon.reconNbr>
  {
    public static CARecon Find(
      PXGraph graph,
      int? cashAccountID,
      string reconNbr,
      PKFindOptions options = 0)
    {
      return (CARecon) PrimaryKeyOf<CARecon>.By<CARecon.cashAccountID, CARecon.reconNbr>.FindBy(graph, (object) cashAccountID, (object) reconNbr, options);
    }
  }

  public static class FK
  {
    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CARecon>.By<CARecon.cashAccountID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CARecon>.By<CARecon.curyID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CARecon>.By<CARecon.curyInfoID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CARecon.selected>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CARecon.cashAccountID>
  {
  }

  public abstract class reconNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CARecon.reconNbr>
  {
  }

  public abstract class reconDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CARecon.reconDate>
  {
  }

  public abstract class lastReconDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CARecon.lastReconDate>
  {
  }

  public abstract class loadDocumentsTill : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CARecon.loadDocumentsTill>
  {
  }

  public abstract class isUserLoadDocumentsTill : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CARecon.isUserLoadDocumentsTill>
  {
  }

  public abstract class reconciled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CARecon.reconciled>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CARecon.voided>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CARecon.hold>
  {
  }

  public abstract class curyBegBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CARecon.curyBegBalance>
  {
  }

  public abstract class curyBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CARecon.curyBalance>
  {
  }

  public abstract class curyReconciledDebits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CARecon.curyReconciledDebits>
  {
  }

  public abstract class reconciledDebits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CARecon.reconciledDebits>
  {
  }

  public abstract class curyReconciledCredits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CARecon.curyReconciledCredits>
  {
  }

  public abstract class reconciledCredits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CARecon.reconciledCredits>
  {
  }

  public abstract class curyReconciledBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CARecon.curyReconciledBalance>
  {
  }

  public abstract class curyReconciledTurnover : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CARecon.curyReconciledTurnover>
  {
  }

  public abstract class reconciledTurnover : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CARecon.reconciledTurnover>
  {
  }

  public abstract class curyDiffBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CARecon.curyDiffBalance>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CARecon.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CARecon.curyInfoID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CARecon.status>
  {
  }

  public abstract class countDebit : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CARecon.countDebit>
  {
  }

  public abstract class countCredit : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CARecon.countCredit>
  {
  }

  public abstract class skipVoided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CARecon.skipVoided>
  {
  }

  public abstract class showBatchPayments : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CARecon.showBatchPayments>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CARecon.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CARecon.rejected>
  {
  }

  public abstract class excludeFromApproval : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CARecon.excludeFromApproval>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CARecon.employeeID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CARecon.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CARecon.ownerID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CARecon.Tstamp>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CARecon.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CARecon.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CARecon.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CARecon.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CARecon.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CARecon.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CARecon.lastModifiedDateTime>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRScheduleTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.GL;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.DR;

/// <summary>
/// A deferred revenue or expense recognition transaction.
/// The entity encapsulates the amount to be recognized (or projected to be recognized)
/// on a particular date and in a particular financial period.
/// The entities of this type are created by <see cref="T:PX.Objects.DR.TransactionsGenerator" />
/// upon the release of <see cref="T:PX.Objects.AR.ARTran" /> or <see cref="T:PX.Objects.AP.APTran" /> document lines
/// containing a <see cref="T:PX.Objects.DR.DRDeferredCode">deferral code</see>.
/// Deferral transactions can be added or edited by the user on the Deferral Schedule
/// (DR201500) form, which corresponds to the <see cref="T:PX.Objects.DR.DraftScheduleMaint" /> graph.
/// </summary>
[DebuggerDisplay("SheduleID={ScheduleID} LineNbr={LineNbr} Amount={Amount} RecDate={RecDate}")]
[PXCacheName("DRScheduleTran")]
[Serializable]
public class DRScheduleTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ScheduleID;
  protected int? _ComponentID;
  protected int? _LineNbr;
  protected int? _BranchID;
  protected 
  #nullable disable
  string _Status;
  protected DateTime? _RecDate;
  protected DateTime? _TranDate;
  protected Decimal? _Amount;
  protected int? _AccountID;
  protected int? _SubID;
  protected string _FinPeriodID;
  protected string _BatchNbr;
  protected string _AdjgDocType;
  protected string _AdjgRefNbr;
  protected int? _AdjNbr;
  protected bool? _IsSamePeriod;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The identifier of the parent <see cref="T:PX.Objects.DR.DRSchedule">deferral schedule</see>.
  /// This field is a part of the compound key of the record and is a part of
  /// the foreign key reference to <see cref="T:PX.Objects.DR.DRScheduleDetail" />.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.DR.DRScheduleDetail.ScheduleID" /> field.
  /// </value>
  [PXParent(typeof (Select<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Current<DRScheduleTran.scheduleID>>, And<DRScheduleDetail.componentID, Equal<Current<DRScheduleTran.componentID>>>>>))]
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (DRScheduleDetail.scheduleID))]
  [PXUIField]
  [PXSelector(typeof (DRSchedule.scheduleID), SubstituteKey = typeof (DRSchedule.scheduleNbr), DirtyRead = true)]
  public virtual int? ScheduleID
  {
    get => this._ScheduleID;
    set => this._ScheduleID = value;
  }

  [PXDBDefault(typeof (DRScheduleDetail.componentID))]
  [PXDBInt(IsKey = true)]
  public virtual int? ComponentID
  {
    get => this._ComponentID;
    set => this._ComponentID = value;
  }

  [PXDBDefault(typeof (DRScheduleDetail.detailLineNbr))]
  [PXDBInt(IsKey = true)]
  public virtual int? DetailLineNbr { get; set; }

  /// <summary>
  /// The line number of the deferral transaction.
  /// This field is defaulted from the current value of
  /// the <see cref="P:PX.Objects.DR.DRScheduleDetail.LineCntr" /> field
  /// of the parent schedule component.
  /// </summary>
  /// <remarks>
  /// If the value of this field is equal to the value of
  /// <see cref="P:PX.Objects.DR.DRScheduleDetail.CreditLineNbr" /> of the parent schedule
  /// component, the deferral transaction is a "credit line transaction",
  /// which means that the whole <see cref="P:PX.Objects.DR.DRScheduleDetail.DefTotal">
  /// deferral amount</see> of the parent component should be posted to
  /// the deferred revenue or expense account.
  /// Otherwise, the deferral transaction is a normal deferred revenue
  /// or expense recognition transaction.
  /// </remarks>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (DRScheduleDetail.lineCntr))]
  [PXUIField(DisplayName = "Tran. Nbr.", Enabled = false)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see>
  /// associated with the deferral transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(null, null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>The status of the deferral transaction.</summary>
  /// <value>
  /// This field can have one of the values defined by
  /// <see cref="T:PX.Objects.DR.DRScheduleTranStatus.ListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("O")]
  [DRScheduleTranStatus.List]
  [PXUIField]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  /// <summary>
  /// The date on which the associated deferred revenue
  /// or deferred expense <see cref="P:PX.Objects.DR.DRScheduleTran.Amount">amount</see>
  /// is expected to be recognized.
  /// </summary>
  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Rec. Date")]
  public virtual DateTime? RecDate
  {
    get => this._RecDate;
    set => this._RecDate = value;
  }

  /// <summary>
  /// The date on which the recognition <see cref="T:PX.Objects.GL.GLTran">
  /// journal transaction</see> was released for the
  /// deferral transaction.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Tran. Date", Enabled = false)]
  public virtual DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  /// <summary>
  /// The deferred revenue or expense amount to
  /// be recognized (in base currency).
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? Amount
  {
    get => this._Amount;
    set => this._Amount = value;
  }

  [PXString(15, IsUnicode = true)]
  [PXFormula(typeof (Search<APTran.receiptNbr, Where<APTran.tranType, Equal<Current<DRSchedule.docType>>, And<APTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<APTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>>>>>))]
  [PXDefault(typeof (Search<APTran.receiptNbr, Where<APTran.tranType, Equal<Current<DRSchedule.docType>>, And<APTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<APTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>>>>>))]
  public virtual string ReceiptNbr { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXFormula(typeof (Search<APTran.pONbr, Where<APTran.tranType, Equal<Current<DRSchedule.docType>>, And<APTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<APTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>>>>>))]
  [PXDefault(typeof (Search<APTran.pONbr, Where<APTran.tranType, Equal<Current<DRSchedule.docType>>, And<APTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<APTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>>>>>))]
  public virtual string PONbr { get; set; }

  [PXString]
  [PXFormula(typeof (Switch<Case<Where<DRScheduleTran.receiptNbr, IsNotNull, Or<DRScheduleTran.pONbr, IsNotNull>>, ControlAccountModule.pO>, Empty>))]
  public virtual string AllowControlAccountForModule { get; set; }

  /// <summary>
  /// The identifier of the income or expense account
  /// associated with the transaction.
  /// </summary>
  /// <value>
  /// Corresponds to <see cref="P:PX.Objects.DR.DRScheduleDetail.AccountID" />.
  /// </value>
  [PXDefault(typeof (DRScheduleDetail.accountID))]
  [PXForeignReference(typeof (Field<DRScheduleTran.accountID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  [Account]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  /// <summary>
  /// The identifier of the income or expense subaccount
  /// associated with the transaction.
  /// </summary>
  /// <value>
  /// Corresponds to <see cref="P:PX.Objects.DR.DRScheduleDetail.SubID" />.
  /// </value>
  [PXDefault(typeof (DRScheduleDetail.subID))]
  [SubAccount(typeof (DRScheduleTran.accountID))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="!:OrganizationFinPeriod">
  /// financial period</see> in which the transaction
  /// is expected to be recognized.
  /// </summary>
  [PX.Objects.GL.FinPeriodID(null, typeof (DRScheduleTran.branchID), null, null, null, null, true, false, null, typeof (DRScheduleTran.tranPeriodID), typeof (DRScheduleDetail.tranPeriodID), true, true)]
  [PXUIField(DisplayName = "Fin. Period", Enabled = false)]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  /// <summary>
  /// The number of <see cref="T:PX.Objects.GL.Batch">journal entry batch</see>,
  /// which contains recognition entries for the deferral transaction.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Batch Nbr.", Enabled = false)]
  public virtual string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  /// <summary>
  /// Represents the adjusting document type for deferral transactions
  /// created on payment application (if the parent <see cref="T:PX.Objects.DR.DRScheduleDetail">
  /// schedule component</see> uses a deferred code with the
  /// <see cref="F:PX.Objects.DR.DeferredMethodType.CashReceipt">"on payment"</see>
  /// recognition method).
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:ARAdjust.AdjgDocType" /> field.
  /// </value>
  [PXDBString(3, IsFixed = true, InputMask = "")]
  public virtual string AdjgDocType
  {
    get => this._AdjgDocType;
    set => this._AdjgDocType = value;
  }

  /// <summary>
  /// Represents the adjusting document reference number for deferral transactions
  /// created on payment application (if the parent <see cref="T:PX.Objects.DR.DRScheduleDetail">
  /// schedule component</see> uses a deferred code with the
  /// <see cref="F:PX.Objects.DR.DeferredMethodType.CashReceipt">"on payment"</see>
  /// recognition method).
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:ARAdjust.AdjgRefNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  public virtual string AdjgRefNbr
  {
    get => this._AdjgRefNbr;
    set => this._AdjgRefNbr = value;
  }

  /// <summary>
  /// Represents the associated <see cref="T:PX.Objects.AR.ARAdjust">adjustment</see> number
  /// for deferral transactions created on payment application (if the parent
  /// <see cref="T:PX.Objects.DR.DRScheduleDetail">schedule component</see> uses a deferred code
  /// with the <see cref="F:PX.Objects.DR.DeferredMethodType.CashReceipt">"on payment"</see>
  /// recognition method).
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:ARAdjust.AdjNbr" /> field.
  /// </value>
  [PXDBInt]
  public virtual int? AdjNbr
  {
    get => this._AdjNbr;
    set => this._AdjNbr = value;
  }

  /// <summary>
  /// The property is obsolete and is not used anywhere.
  /// It cannot be removed at the moment due to the bug in the Copy-Paste
  /// functionality (AC-77988), but should be removed once the source bug
  /// is fixed.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Same Period", Enabled = true, Visible = false)]
  [Obsolete("This property is obsolete and will be removed in Acumatica 6.0. To check whether a transaction is in the same period as the incoming transaction, perform an explicit check.", false)]
  public virtual bool? IsSamePeriod
  {
    get => this._IsSamePeriod;
    set => this._IsSamePeriod = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : 
    PrimaryKeyOf<DRScheduleTran>.By<DRScheduleTran.scheduleID, DRScheduleTran.componentID, DRScheduleTran.detailLineNbr, DRScheduleTran.lineNbr>
  {
    public static DRScheduleTran Find(
      PXGraph graph,
      int? scheduleID,
      int? componentID,
      int? detailLineNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (DRScheduleTran) PrimaryKeyOf<DRScheduleTran>.By<DRScheduleTran.scheduleID, DRScheduleTran.componentID, DRScheduleTran.detailLineNbr, DRScheduleTran.lineNbr>.FindBy(graph, (object) scheduleID, (object) componentID, (object) detailLineNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Schedule : 
      PrimaryKeyOf<DRSchedule>.By<DRSchedule.scheduleID>.ForeignKeyOf<DRScheduleTran>.By<DRScheduleTran.scheduleID>
    {
    }

    public class Component : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<DRScheduleTran>.By<DRScheduleTran.componentID>
    {
    }

    public class ScheduleDetail : 
      PrimaryKeyOf<DRScheduleDetail>.By<DRScheduleDetail.scheduleID, DRScheduleDetail.componentID, DRScheduleDetail.detailLineNbr>.ForeignKeyOf<DRScheduleTran>.By<DRScheduleTran.scheduleID, DRScheduleTran.componentID, DRScheduleTran.detailLineNbr>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<DRScheduleTran>.By<DRScheduleTran.branchID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<DRScheduleTran>.By<DRScheduleTran.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<DRScheduleTran>.By<DRScheduleTran.subID>
    {
    }
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleTran.scheduleID>
  {
  }

  public abstract class componentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleTran.componentID>
  {
  }

  public abstract class detailLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleTran.detailLineNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleTran.lineNbr>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleTran.branchID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRScheduleTran.status>
  {
  }

  public abstract class recDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  DRScheduleTran.recDate>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  DRScheduleTran.tranDate>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DRScheduleTran.amount>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRScheduleTran.receiptNbr>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRScheduleTran.pONbr>
  {
  }

  public abstract class allowControlAccountForModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRScheduleTran.allowControlAccountForModule>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleTran.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleTran.subID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRScheduleTran.finPeriodID>
  {
  }

  public abstract class tranPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRScheduleTran.batchNbr>
  {
  }

  public abstract class adjgDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRScheduleTran.adjgDocType>
  {
  }

  public abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRScheduleTran.adjgRefNbr>
  {
  }

  public abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRScheduleTran.adjNbr>
  {
  }

  [Obsolete("This BQL field is obsolete and will be removed in Acumatica 6.0. To check whether a transaction is in the same period as the incoming transaction, perform an explicit check.", false)]
  public abstract class isSamePeriod : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DRScheduleTran.isSamePeriod>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  DRScheduleTran.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  DRScheduleTran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRScheduleTran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DRScheduleTran.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DRScheduleTran.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRScheduleTran.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DRScheduleTran.lastModifiedDateTime>
  {
  }
}

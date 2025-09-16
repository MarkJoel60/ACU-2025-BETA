// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLDocBatch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// Represents a batch of documents edited through the Journal Vouchers (GL304000) form.
/// </summary>
[PXCacheName("GL Document Batch")]
[PXPrimaryGraph(typeof (JournalWithSubEntry))]
[Serializable]
public class GLDocBatch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected int? _BranchID;
  protected 
  #nullable disable
  string _Module;
  protected string _BatchNbr;
  protected int? _LedgerID;
  protected DateTime? _DateEntered;
  protected string _FinPeriodID;
  protected string _BatchType;
  protected string _Status;
  protected Decimal? _CuryDebitTotal;
  protected Decimal? _CuryCreditTotal;
  protected Decimal? _CuryControlTotal;
  protected Decimal? _DebitTotal;
  protected Decimal? _CreditTotal;
  protected Decimal? _ControlTotal;
  protected long? _CuryInfoID;
  protected string _OrigModule;
  protected string _OrigBatchNbr;
  protected bool? _Released;
  protected bool? _Posted;
  protected string _TranPeriodID;
  protected int? _LineCntr;
  protected string _CuryID;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected bool? _Hold;
  protected bool? _Voided;
  protected string _Description;

  /// <summary>
  /// Indicates whether the record is selected for mass processing or not.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch" />, to which the batch belongs.
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

  /// <summary>The code of the module, to which the batch belongs.</summary>
  /// <value>
  /// Defaults to "GL".
  /// Allowed values are:
  /// "GL", "AP", "AR", "CM", "CA", "IN", "DR", "FA", "PM", "TX", "SO", "PO".
  /// </value>
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault("GL")]
  [PXUIField]
  [BatchModule.List]
  [PXFieldDescription]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  /// <summary>
  /// Key field.
  /// Auto-generated unique number of the document batch.
  /// </summary>
  /// <value>
  /// The number is generated from the <see cref="T:PX.Objects.CS.Numbering">Numbering Sequence</see> specified in the
  /// <see cref="P:PX.Objects.GL.GLSetup.DocBatchNumberingID" /> field of the GL preferences record.
  /// </value>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXSelector(typeof (Search<GLDocBatch.batchNbr, Where<GLDocBatch.module, Equal<Current<GLDocBatch.module>>>, OrderBy<Desc<GLDocBatch.batchNbr>>>), Filterable = true)]
  [PXUIField]
  [AutoNumber(typeof (GLSetup.docBatchNumberingID), typeof (GLDocBatch.dateEntered))]
  [PXFieldDescription]
  public virtual string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Ledger" />, to which the batch belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Ledger.LedgerID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (Search<Branch.ledgerID, Where<Branch.branchID, Equal<Current<GLDocBatch.branchID>>>>))]
  [PXUIField]
  [PXSelector(typeof (Search2<Ledger.ledgerID, LeftJoin<Branch, On<Branch.ledgerID, Equal<Ledger.ledgerID>>>, Where<Ledger.balanceType, NotEqual<BudgetLedger>, And<Where<Ledger.balanceType, NotEqual<LedgerBalanceType.actual>, Or<Branch.branchID, Equal<Current<GLDocBatch.branchID>>>>>>>), SubstituteKey = typeof (Ledger.ledgerCD))]
  public virtual int? LedgerID
  {
    get => this._LedgerID;
    set => this._LedgerID = value;
  }

  /// <summary>The date of the batch, specified by user.</summary>
  /// <value>
  /// Defaults to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">Business Date</see>.
  /// </value>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DateEntered
  {
    get => this._DateEntered;
    set => this._DateEntered = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.FinPeriods.OrganizationFinPeriod">Financial Period</see>, to which the batch belongs.
  /// </summary>
  /// <value>
  /// By default the period is deducted from the <see cref="P:PX.Objects.GL.GLDocBatch.DateEntered">date of the batch</see>.
  /// Can be overriden by user.
  /// </value>
  [OpenPeriod(null, typeof (GLDocBatch.dateEntered), typeof (GLDocBatch.branchID), null, null, null, null, true, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (GLDocBatch.tranPeriodID), true, IsHeader = true)]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  /// <summary>The type of the batch.</summary>
  /// <value>
  /// Allowed values are:
  /// <c>"H"</c> - Normal,
  /// <c>"R"</c> - Recurring,
  /// <c>"C"</c> - Consolidation,
  /// <c>"T"</c> - Trial Balance,
  /// <c>"RCL"</c> - Reclassification.
  /// Defaults to <c>"H"</c> - Normal.
  /// </value>
  [PXDBString(3)]
  [PXDefault("H")]
  [PXUIField(DisplayName = "Type")]
  [BatchTypeCode.List]
  public virtual string BatchType
  {
    get => this._BatchType;
    set => this._BatchType = value;
  }

  /// <summary>Read-only field reflecting the status of the batch.</summary>
  /// <value>
  /// The value of the field can not be changed directly and depends on the status flags of the batch:
  /// <see cref="P:PX.Objects.GL.GLDocBatch.Posted" />, <see cref="P:PX.Objects.GL.GLDocBatch.Voided" />, <see cref="P:PX.Objects.GL.GLDocBatch.Released" />, <see cref="P:PX.Objects.GL.GLDocBatch.Hold" />.
  /// Possible values are:
  /// <c>"H"</c> - Hold,
  /// <c>"B"</c> - Balanced,
  /// <c>"C"</c> - Completed,
  /// <c>"V"</c> - Voided,
  /// <c>"R"</c> - Released.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("H")]
  [PXUIField]
  [GLDocBatchStatus.List]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  /// <summary>
  /// The total debit amount of the batch in its <see cref="P:PX.Objects.GL.GLDocBatch.CuryID">currency</see>.
  /// </summary>
  /// <value>
  /// See also <see cref="P:PX.Objects.GL.GLDocBatch.DebitTotal" />.
  /// </value>
  [PXDBCurrency(typeof (GLDocBatch.curyInfoID), typeof (GLDocBatch.debitTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryDebitTotal
  {
    get => this._CuryDebitTotal;
    set => this._CuryDebitTotal = value;
  }

  /// <summary>
  /// The total credit amount of the batch in its <see cref="P:PX.Objects.GL.GLDocBatch.CuryID">currency</see>.
  /// </summary>
  /// <value>
  /// See also <see cref="P:PX.Objects.GL.GLDocBatch.CreditTotal" />.
  /// </value>
  [PXDBCurrency(typeof (GLDocBatch.curyInfoID), typeof (GLDocBatch.creditTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryCreditTotal
  {
    get => this._CuryCreditTotal;
    set => this._CuryCreditTotal = value;
  }

  /// <summary>
  /// The control total of the batch in its <see cref="P:PX.Objects.GL.GLDocBatch.CuryID">currency</see>.
  /// </summary>
  /// 
  ///             See also <see cref="P:PX.Objects.GL.GLDocBatch.ControlTotal" />
  /// .
  [PXDBCurrency(typeof (GLDocBatch.curyInfoID), typeof (GLDocBatch.controlTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Total")]
  public virtual Decimal? CuryControlTotal
  {
    get => this._CuryControlTotal;
    set => this._CuryControlTotal = value;
  }

  /// <summary>
  /// The total debit amount of the batch in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// </summary>
  /// <value>
  /// See also <see cref="P:PX.Objects.GL.GLDocBatch.CuryDebitTotal" />.
  /// </value>
  [PXDBBaseCury(typeof (GLDocBatch.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DebitTotal
  {
    get => this._DebitTotal;
    set => this._DebitTotal = value;
  }

  /// <summary>
  /// The total credit amount of the batch in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// </summary>
  /// <value>
  /// See also <see cref="P:PX.Objects.GL.GLDocBatch.CuryCreditTotal" />.
  /// </value>
  [PXDBBaseCury(typeof (GLDocBatch.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CreditTotal
  {
    get => this._CreditTotal;
    set => this._CreditTotal = value;
  }

  /// <summary>
  /// The control total of the batch in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// </summary>
  /// <value>
  /// See also <see cref="P:PX.Objects.GL.GLDocBatch.CuryControlTotal" />.
  /// </value>
  [PXDBBaseCury(typeof (GLDocBatch.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Total")]
  public virtual Decimal? ControlTotal
  {
    get => this._ControlTotal;
    set => this._ControlTotal = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> record associated with the batch.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:PCurrencyInfo.CurrencyInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>
  /// The code of the module, from which the batch originates.
  /// </summary>
  [PXDBString(2, IsFixed = true)]
  public virtual string OrigModule
  {
    get => this._OrigModule;
    set => this._OrigModule = value;
  }

  /// <summary>
  /// The number of the batch, from which this batch originates.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  public virtual string OrigBatchNbr
  {
    get => this._OrigBatchNbr;
    set => this._OrigBatchNbr = value;
  }

  /// <summary>Indicates whether the batch has been released.</summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>Indicates whether the batch has been posted.</summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Posted
  {
    get => this._Posted;
    set => this._Posted = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.FinPeriods.OrganizationFinPeriod">Financial Period</see> of the batch.
  /// </summary>
  /// <value>
  /// Determined by the <see cref="P:PX.Objects.GL.GLDocBatch.DateEntered">date of the batch</see>. Unlike the <see cref="P:PX.Objects.GL.GLDocBatch.FinPeriodID" /> field,
  /// the value of this field can't be overriden by user and always reflects the period corresponding to the date of the batch.
  /// </value>
  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID
  {
    get => this._TranPeriodID;
    set => this._TranPeriodID = value;
  }

  /// <summary>
  /// The counter of the document lines, used <i>internally</i> to assign consistent numbers to newly created lines.
  /// It is not recommended to rely on this field to determine the exact count of lines, because it might not reflect the latter under some conditions.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  /// <summary>
  /// The code of the <see cref="T:PX.Objects.CM.Currency">Currency</see> of the batch.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote(DescriptionField = typeof (GLDocBatch.batchNbr))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <summary>Indicates whether the batch is on hold.</summary>
  /// <value>
  /// Defaults to <c>true</c>, if the <see cref="P:PX.Objects.GL.GLSetup.VouchersHoldEntry" /> flag is set in the preferences of the module,
  /// and to <c>false</c> otherwise.
  /// </value>
  [PXDBBool]
  [PXUIField]
  [PXDefault(true, typeof (Search<GLSetup.vouchersHoldEntry>))]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  /// <summary>Indicates whether the batch has been voided.</summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  /// <summary>The description of the batch.</summary>
  [PXUIField]
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  public class PK : PrimaryKeyOf<GLDocBatch>.By<GLDocBatch.module, GLDocBatch.batchNbr>
  {
    public static GLDocBatch Find(
      PXGraph graph,
      string module,
      string batchNbr,
      PKFindOptions options = 0)
    {
      return (GLDocBatch) PrimaryKeyOf<GLDocBatch>.By<GLDocBatch.module, GLDocBatch.batchNbr>.FindBy(graph, (object) module, (object) batchNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<GLDocBatch>.By<GLDocBatch.branchID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<GLDocBatch>.By<GLDocBatch.ledgerID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<GLDocBatch>.By<GLDocBatch.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<GLDocBatch>.By<GLDocBatch.curyID>
    {
    }

    public class OriginalJournalVoucher : 
      PrimaryKeyOf<Batch>.By<Batch.module, Batch.batchNbr>.ForeignKeyOf<GLDocBatch>.By<GLDocBatch.origModule, GLDocBatch.origBatchNbr>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLDocBatch.selected>
  {
  }

  public class Events : PXEntityEventBase<GLDocBatch>.Container<GLDocBatch.Events>
  {
    public PXEntityEvent<GLDocBatch> ReleaseVoucher;
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLDocBatch.branchID>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLDocBatch.module>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLDocBatch.batchNbr>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLDocBatch.ledgerID>
  {
  }

  public abstract class dateEntered : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  GLDocBatch.dateEntered>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLDocBatch.finPeriodID>
  {
  }

  public abstract class batchType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLDocBatch.batchType>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLDocBatch.status>
  {
  }

  public abstract class curyDebitTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLDocBatch.curyDebitTotal>
  {
  }

  public abstract class curyCreditTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLDocBatch.curyCreditTotal>
  {
  }

  public abstract class curyControlTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLDocBatch.curyControlTotal>
  {
  }

  public abstract class debitTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLDocBatch.debitTotal>
  {
  }

  public abstract class creditTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLDocBatch.creditTotal>
  {
  }

  public abstract class controlTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLDocBatch.controlTotal>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLDocBatch.curyInfoID>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLDocBatch.origModule>
  {
  }

  public abstract class origBatchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLDocBatch.origBatchNbr>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLDocBatch.released>
  {
  }

  public abstract class posted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLDocBatch.posted>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLDocBatch.tranPeriodID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLDocBatch.lineCntr>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLDocBatch.curyID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLDocBatch.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  GLDocBatch.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLDocBatch.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLDocBatch.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLDocBatch.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLDocBatch.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLDocBatch.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLDocBatch.lastModifiedDateTime>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLDocBatch.hold>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLDocBatch.voided>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLDocBatch.description>
  {
  }
}

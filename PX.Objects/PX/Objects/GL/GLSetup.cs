// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// The preferences of the general ledger functionality.
/// A user edits the records of this type on the General Ledger Preferences (GL102000) form.
/// </summary>
[PXPrimaryGraph(typeof (GLSetupMaint))]
[PXCacheName("General Ledger Preferences")]
[Serializable]
public class GLSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _YtdNetIncAccountID;
  protected int? _RetEarnAccountID;
  protected 
  #nullable disable
  string _AutoRevOption;
  protected bool? _AutoRevEntry;
  protected bool? _AutoPostOption;
  protected short? _COAOrder;
  protected bool? _RequireControlTotal;
  protected bool? _RequireRefNbrForTaxEntry;
  protected bool? _PostClosedPeriods;
  protected string _BatchNumberingID;
  protected string _DocBatchNumberingID;
  protected string _TBImportNumberingID;
  protected string _AllocationNumberingID;
  protected string _ScheduleNumberingID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected bool? _HoldEntry;
  protected bool? _VouchersHoldEntry;
  protected short? _ConsolSegmentId;
  protected short? _PerRetainTran;
  protected int? _DefaultSubID;
  protected string _TrialBalanceSign;
  protected bool? _ReuseRefNbrsInVouchers;
  protected bool? _AutoReleaseReclassBatch;

  /// <summary>
  /// Identifier of the Year-to-Date Net Income <see cref="T:PX.Objects.GL.Account" />.
  /// The account tracks net income accumulated since the beginning of the financial year. It is updated by every transaction posted in the system.
  /// The account must not be changed after any journal transactions have been posted.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// Only Accounts of <see cref="P:PX.Objects.GL.Account.Type">type</see> Liability (<c>"L"</c>) can be specified as YTD Net Income account.
  /// </value>
  [PXDefault]
  [Account(null, typeof (Search<Account.accountID, Where<Match<Current<AccessInfo.userName>>>>))]
  [PXRestrictor(typeof (Where<Account.type, Equal<AccountType.liability>>), "The selected account must have the Liability type.", new Type[] {})]
  public virtual int? YtdNetIncAccountID
  {
    get => this._YtdNetIncAccountID;
    set => this._YtdNetIncAccountID = value;
  }

  /// <summary>
  /// Identifier of the Retained Earnings <see cref="T:PX.Objects.GL.Account" />.
  /// The account is updated by the amount accumulated on the <see cref="P:PX.Objects.GL.GLSetup.YtdNetIncAccountID">YTD Net Income</see> account during the financial year closing.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null, typeof (Search<Account.accountID, Where<Match<Current<AccessInfo.userName>>>>))]
  [PXRestrictor(typeof (Where<Account.type, Equal<AccountType.liability>>), "The selected account must have the Liability type.", new Type[] {})]
  public virtual int? RetEarnAccountID
  {
    get => this._RetEarnAccountID;
    set => this._RetEarnAccountID = value;
  }

  /// <summary>
  /// Determines when the system generates reversing batches.
  /// </summary>
  /// <value>
  /// Possible values are: <c>"P"</c> - On Post and <c>"C"</c> - On Period Closing.
  /// Default value is On Post.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("P")]
  [PXUIField]
  [AutoRevOptions.List]
  public virtual string AutoRevOption
  {
    get => this._AutoRevOption;
    set => this._AutoRevOption = value;
  }

  /// <summary>
  /// Determines whether the system creates negative entries when reversing a <see cref="T:PX.Objects.GL.Batch" />.
  /// If set to <c>true</c>, the transactions of the reversing batch will retain the Debit/Credit amounts of the original transactions, but with opposite signs.
  /// If set to <c>false</c>, the transactions of the reversing batch will retain the sign of the original amounts, but will have them on the opposite sides of Debit/Credit.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Create Negative Entries on Reversal")]
  public virtual bool? AutoRevEntry
  {
    get => this._AutoRevEntry;
    set => this._AutoRevEntry = value;
  }

  /// <summary>
  /// Determines whether the system must automatically post <see cref="T:PX.Objects.GL.Batch">Batches</see> once they are released.
  /// When this option is set to <c>true</c> there won't be any batches with status Unposted in the system.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? AutoPostOption
  {
    get => this._AutoPostOption;
    set => this._AutoPostOption = value;
  }

  /// <summary>
  /// Determines the order of <see cref="T:PX.Objects.GL.Account">Accounts</see> in the reports of the General Ledger module.
  /// </summary>
  /// <value>
  /// Allowed values are:
  /// <c>0</c> - 1.Assets, 2.Liabilities, 3.Income and Expenses
  /// <c>1</c> - 1.Assets, 2.Liabilities, 3.Income, 4.Expenses
  /// <c>2</c> - 1.Income, 2. Expenses, 3. Assets, 4.Liabilities
  /// <c>3</c> - 1.Income and Expenses, 2. Assets, 3.Liabilities
  /// <c>128</c> - Custom order.
  /// If the last option is selected, the order of accounts is determined by the <see cref="P:PX.Objects.GL.Account.COAOrder">COAOrder</see> field of the individual <see cref="T:PX.Objects.GL.Account">Accounts</see>.
  /// Defaults to <c>0</c>.
  /// </value>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Chart of Accounts Order")]
  [GLSetup.cOAOrder.List]
  public virtual short? COAOrder
  {
    get => this._COAOrder;
    set => this._COAOrder = value;
  }

  /// <summary>
  /// Indicates whether the system must enforce validation of batch control totals on Journal Transactions (GL301000) form.
  /// </summary>
  /// <value>
  /// When set to <c>true</c> the <see cref="P:PX.Objects.GL.Batch.CuryControlTotal">control total</see> and <see cref="P:PX.Objects.GL.Batch.CuryDebitTotal">debit</see>/<see cref="P:PX.Objects.GL.Batch.CuryCreditTotal">credit total</see>
  /// of batches must match and user won't be able to save the batch until this condition is met.
  /// Otherwise, validation is performed only after clearing the <see cref="P:PX.Objects.GL.Batch.Hold">Hold</see> flag on the Batch.
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Validate Batch Control Totals on Entry")]
  public virtual bool? RequireControlTotal
  {
    get => this._RequireControlTotal;
    set => this._RequireControlTotal = value;
  }

  /// <summary>
  /// Indicates whether the Reference Number fields are required on Journal Transactions (GL301000) form in case of entering Gl transactions with taxes.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Ref. Numbers for GL Documents with Taxes")]
  public virtual bool? RequireRefNbrForTaxEntry
  {
    get => this._RequireRefNbrForTaxEntry;
    set => this._RequireRefNbrForTaxEntry = value;
  }

  /// <summary>
  /// Determines whether the system allows to post transactions to closed <see cref="!:PX.Objects.GL.Obsolete.FinPeriod">Financial Periods</see>.
  /// If set to <c>false</c>, the system raises an error on attempts to enter or post transactions into a closed period.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [Obsolete]
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Posting to Closed Periods")]
  public virtual bool? PostClosedPeriods
  {
    get => this._PostClosedPeriods;
    set => this._PostClosedPeriods = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Restrict Access to Closed Periods")]
  public virtual bool? RestrictAccessToClosedPeriods { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Numbering">Numbering Sequence</see> used for batches.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.Numbering.NumberingID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault("BATCH")]
  [PXUIField(DisplayName = "Batch Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string BatchNumberingID
  {
    get => this._BatchNumberingID;
    set => this._BatchNumberingID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Numbering">Numbering Sequence</see> used for batches of documents
  /// created on the Journal Vouchers (GL304000) form. See the <see cref="T:PX.Objects.GL.GLDocBatch" /> DAC.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.Numbering.NumberingID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault("BATCH")]
  [PXUIField(DisplayName = "Document Batch Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string DocBatchNumberingID
  {
    get => this._DocBatchNumberingID;
    set => this._DocBatchNumberingID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Numbering">Numbering Sequence</see> used for imports of trial balances.
  /// See the <see cref="T:PX.Objects.GL.GLTrialBalanceImportMap" /> DAC.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.Numbering.NumberingID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault("TBIMPORT")]
  [PXUIField(DisplayName = "Import Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string TBImportNumberingID
  {
    get => this._TBImportNumberingID;
    set => this._TBImportNumberingID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Numbering">Numbering Sequence</see> used for allocations.
  /// See the <see cref="T:PX.Objects.GL.GLAllocation" /> DAC.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.Numbering.NumberingID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault("ALLOCATION")]
  [PXUIField(DisplayName = "Allocation Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string AllocationNumberingID
  {
    get => this._AllocationNumberingID;
    set => this._AllocationNumberingID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Numbering">Numbering Sequence</see> used for schedules.
  /// See the <see cref="T:PX.Objects.GL.Schedule" /> DAC.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.Numbering.NumberingID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault("SCHEDULE")]
  [PXUIField(DisplayName = "Schedule Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string ScheduleNumberingID
  {
    get => this._ScheduleNumberingID;
    set => this._ScheduleNumberingID = value;
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

  /// <summary>
  /// Determines whether the system must set the <see cref="P:PX.Objects.GL.Batch.Hold">Hold</see> flag and <see cref="P:PX.Objects.GL.Batch.Status">Status</see> On Hold
  /// when creating new <see cref="T:PX.Objects.GL.Batch">batches</see>.
  /// </summary>
  /// <value>
  /// Defaults to <c>true</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Hold Batches on Entry")]
  public virtual bool? HoldEntry
  {
    get => this._HoldEntry;
    set => this._HoldEntry = value;
  }

  /// <summary>
  /// Determines whether the system must set the <see cref="P:PX.Objects.GL.GLDocBatch.Hold">Hold</see> flag and <see cref="P:PX.Objects.GL.GLDocBatch.Status">Status</see> On Hold
  /// when creating new <see cref="T:PX.Objects.GL.GLDocBatch">vouchers</see>.
  /// </summary>
  /// <value>
  /// Defaults to <c>true</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Hold Vouchers on Entry")]
  public virtual bool? VouchersHoldEntry
  {
    get => this._VouchersHoldEntry;
    set => this._VouchersHoldEntry = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Segment">Segment</see> of the <see cref="T:PX.Objects.GL.Sub">Subaccount</see> segmented key,
  /// whose values denote the consolidation unit in the subaccounts of the parent company.
  /// Controls the selector on the <see cref="P:PX.Objects.GL.GLConsolSetup.SegmentValue" /> field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.Segment.SegmentID" /> field.
  /// </value>
  [PXDBShort]
  [PXUIField(DisplayName = "Consolidation Segment Number")]
  [GLSetup.consolSegmentId.PXConsolSegmentSelector]
  public virtual short? ConsolSegmentId
  {
    get => this._ConsolSegmentId;
    set => this._ConsolSegmentId = value;
  }

  /// <summary>
  /// Determines the number of periods the system will keep the transactions in the database.
  /// </summary>
  /// <value>
  /// Defaults to <c>99</c>.
  /// </value>
  [PXDBShort]
  [PXDefault(99)]
  [PXUIField]
  public virtual short? PerRetainTran
  {
    get => this._PerRetainTran;
    set => this._PerRetainTran = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Sub">Subaccount</see> to be used as the default one with <see cref="T:PX.Objects.GL.Account">Accounts</see>,
  /// which have the <see cref="P:PX.Objects.GL.Account.NoSubDetail">NoSubDetail</see> field set to <c>true</c>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(DisplayName = "Default Subaccount")]
  public virtual int? DefaultSubID
  {
    get => this._DefaultSubID;
    set => this._DefaultSubID = value;
  }

  /// <summary>
  /// Determines whether the system should automatically change the sign of the trial balance when importing data.
  /// Also affects the way trial balance is displayed on reports and inquiries.
  /// </summary>
  /// <value>
  /// Allowed values are:
  /// <c>"N"</c> - Normal - the system doesn't change the signs of balances of expense and income accounts on import.
  /// On reports and inquiries all the accounts are grouped by account type.
  /// <c>"R"</c> - Reversed - the system reverses the signs of balances of expense and income accounts on import.
  /// On reports and inquiries all the accounts except for the <see cref="P:PX.Objects.GL.GLSetup.YtdNetIncAccountID">Year-to-Date Net Income account</see> are listed.
  /// Defaults to <c>"N"</c>.
  /// </value>
  [PXDBString]
  [GLSetup.trialBalanceSign.List]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Sign of the Trial Balance")]
  public virtual string TrialBalanceSign
  {
    get => this._TrialBalanceSign;
    set => this._TrialBalanceSign = value;
  }

  /// <summary>
  /// Determines whether the reference numbers for documents created on the Journal Vouchers (GL304000) form are reused
  /// in case the number was allocated for the document that was not persisted, or the document was deleted thus freeing the reference number.
  /// When set to <c>true</c>, the system will pick up the free numbers from such documents when creating new entries on the mentioned form.
  /// (Note: this may lead to non-linear numbering.)
  /// Otherwise the form will always assign new numbers to the documents even if there are reusable ones.
  /// </summary>
  /// <value>
  /// Defaults to <c>true</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Reuse reference numbers in Journal Vouchers")]
  public virtual bool? ReuseRefNbrsInVouchers
  {
    get => this._ReuseRefNbrsInVouchers;
    set => this._ReuseRefNbrsInVouchers = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Generate Consolidated Batches")]
  public virtual bool? ConsolidatedPosting { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Automatically Release Reclassification Batches")]
  public virtual bool? AutoReleaseReclassBatch
  {
    get => this._AutoReleaseReclassBatch;
    set => this._AutoReleaseReclassBatch = value;
  }

  public static class FK
  {
    public class YTDNetIncomeAccount : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<GLSetup>.By<GLSetup.ytdNetIncAccountID>
    {
    }

    public class RetainedEarningsAccount : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<GLSetup>.By<GLSetup.retEarnAccountID>
    {
    }

    public class DefaultSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLSetup>.By<GLSetup.defaultSubID>
    {
    }

    public class BatchNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<GLSetup>.By<GLSetup.batchNumberingID>
    {
    }

    public class DocumentBatchNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<GLSetup>.By<GLSetup.docBatchNumberingID>
    {
    }

    public class ImportNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<GLSetup>.By<GLSetup.tBImportNumberingID>
    {
    }

    public class AllocationNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<GLSetup>.By<GLSetup.allocationNumberingID>
    {
    }

    public class ScheduleNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<GLSetup>.By<GLSetup.scheduleNumberingID>
    {
    }
  }

  public abstract class ytdNetIncAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLSetup.ytdNetIncAccountID>
  {
  }

  public abstract class retEarnAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLSetup.retEarnAccountID>
  {
  }

  public abstract class autoRevOption : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLSetup.autoRevOption>
  {
  }

  public abstract class autoRevEntry : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLSetup.autoRevEntry>
  {
  }

  public abstract class autoPostOption : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLSetup.autoPostOption>
  {
  }

  public abstract class cOAOrder : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  GLSetup.cOAOrder>
  {
    public class ListAttribute : PXIntListAttribute
    {
      public ListAttribute()
        : base(new int[5]{ 0, 1, 2, 3, 128 /*0x80*/ }, new string[5]
        {
          "1:Assets 2:Liabilities 3:Income and Expenses",
          "1:Assets 2:Liabilities 3:Income 4:Expenses",
          "1:Income 2:Expenses 3:Assets 4:Liabilities",
          "1:Income and Expenses 2:Assets 3:Liabilities",
          "Custom Chart of Accounts Order"
        })
      {
      }
    }
  }

  public abstract class requireControlTotal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLSetup.requireControlTotal>
  {
  }

  public abstract class requireRefNbrForTaxEntry : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLSetup.requireRefNbrForTaxEntry>
  {
  }

  [Obsolete]
  public abstract class postClosedPeriods : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLSetup.postClosedPeriods>
  {
  }

  public abstract class restrictAccessToClosedPeriods : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLSetup.restrictAccessToClosedPeriods>
  {
  }

  public abstract class batchNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLSetup.batchNumberingID>
  {
  }

  public abstract class docBatchNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLSetup.docBatchNumberingID>
  {
  }

  public abstract class tBImportNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLSetup.tBImportNumberingID>
  {
  }

  public abstract class allocationNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLSetup.allocationNumberingID>
  {
  }

  public abstract class scheduleNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLSetup.scheduleNumberingID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  GLSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLSetup.lastModifiedDateTime>
  {
  }

  public abstract class holdEntry : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLSetup.holdEntry>
  {
  }

  public abstract class vouchersHoldEntry : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLSetup.vouchersHoldEntry>
  {
  }

  public abstract class consolSegmentId : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  GLSetup.consolSegmentId>
  {
    public class PXConsolSegmentSelectorAttribute : PXSelectorAttribute
    {
      public PXConsolSegmentSelectorAttribute()
        : base(typeof (Search<Segment.segmentID, Where<Segment.dimensionID, Equal<SubAccountAttribute.dimensionName>>>), new Type[2]
        {
          typeof (Segment.segmentID),
          typeof (Segment.descr)
        })
      {
        this.DescriptionField = typeof (Segment.descr);
        this._UnconditionalSelect = (BqlCommand) new Search<Segment.segmentID, Where<Segment.dimensionID, Equal<SubAccountAttribute.dimensionName>, And<Segment.segmentID, Equal<Required<Segment.segmentID>>>>>();
      }
    }
  }

  public abstract class perRetainTran : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  GLSetup.perRetainTran>
  {
  }

  public abstract class defaultSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLSetup.defaultSubID>
  {
  }

  public abstract class trialBalanceSign : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLSetup.trialBalanceSign>
  {
    public const string Normal = "N";
    public const string Reversed = "R";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "N", "R" }, new string[2]
        {
          "Normal",
          "Reversed"
        })
      {
      }
    }

    public class normal : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GLSetup.trialBalanceSign.normal>
    {
      public normal()
        : base("N")
      {
      }
    }

    public class reversed : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GLSetup.trialBalanceSign.reversed>
    {
      public reversed()
        : base("R")
      {
      }
    }
  }

  public abstract class reuseRefNbrsInVouchers : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLSetup.reuseRefNbrsInVouchers>
  {
  }

  public abstract class consolidatedPosting : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLSetup.consolidatedPosting>
  {
  }

  public abstract class autoReleaseReclassBatch : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLSetup.autoReleaseReclassBatch>
  {
  }
}

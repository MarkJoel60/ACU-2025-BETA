// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRDeferredCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.DR;

/// <summary>
/// A deferral code, which determines the method
/// and schedule by which deferred revenue or expense is recognized.
/// </summary>
[PXPrimaryGraph(typeof (DeferredCodeMaint))]
[PXCacheName("Deferral Code")]
[Serializable]
public class DRDeferredCode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DeferredCodeID;
  protected string _Description;
  protected string _AccountType;
  protected int? _AccountID;
  protected int? _SubID;
  protected Decimal? _ReconNowPct;
  protected short? _StartOffset;
  protected short? _Occurrences;
  protected short? _Frequency;
  protected short? _FixedDay;
  protected bool? _MultiDeliverableArrangement;
  protected string _AccountSource;
  protected bool? _CopySubFromSourceTran;
  protected string _DeferralSubMaskAR;
  protected string _DeferralSubMaskAP;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The unique identifier of the deferral code.
  /// This field is the key field.
  /// </summary>
  [PXDefault]
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField]
  [PXSelector(typeof (Search<DRDeferredCode.deferredCodeID>))]
  [PXFieldDescription]
  [PXReferentialIntegrityCheck]
  public virtual string DeferredCodeID
  {
    get => this._DeferredCodeID;
    set => this._DeferredCodeID = value;
  }

  /// <summary>A user-friendly description of the deferral code.</summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>The type of the deferral code.</summary>
  /// <value>
  /// This field can have one of the values defined in the
  /// <see cref="T:PX.Objects.DR.DeferredAccountType" /> class.
  /// </value>
  [PXDBString(1)]
  [PXDefault("I")]
  [LabelList(typeof (DeferredAccountType))]
  [PXUIField]
  public virtual string AccountType
  {
    get => this._AccountType;
    set => this._AccountType = value;
  }

  /// <summary>
  /// The identifier of the deferral <see cref="T:PX.Objects.GL.Account" />.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  [PXDefault]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? Active { get; set; }

  /// <summary>
  /// The identifier of the deferral <see cref="T:PX.Objects.GL.Sub">subaccount</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (DRDeferredCode.accountID))]
  [PXDefault]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  /// <summary>
  /// The percentage of deferred revenue or expense
  /// that should be recognized immediately upon the release of
  /// the <see cref="T:PX.Objects.AP.APTran">AP</see> or <see cref="T:PX.Objects.AR.ARTran">AR</see> document line
  /// containing the deferral code.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0)]
  [PXUIField]
  public virtual Decimal? ReconNowPct
  {
    get => this._ReconNowPct;
    set => this._ReconNowPct = value;
  }

  /// <summary>
  /// The number of <see cref="T:PX.Objects.GL.FinPeriods.OrganizationFinPeriod">financial periods</see>
  /// that should be skipped prior to the first recognition transaction.
  /// </summary>
  /// <remarks>
  /// This field is not applicable in case of a flexible
  /// deferral code. For details, see <see cref="T:PX.Objects.DR.DeferredMethodType" />.
  /// </remarks>
  [PXDefault(0)]
  [PXDBShort]
  [PXUIField(DisplayName = "Start Offset")]
  public virtual short? StartOffset
  {
    get => this._StartOffset;
    set => this._StartOffset = value;
  }

  /// <summary>
  /// The total number of required deferred revenue
  /// recognition transactions.
  /// </summary>
  /// <remarks>
  /// This field is not applicable in case of a flexible
  /// deferral code. For details, see <see cref="T:PX.Objects.DR.DeferredMethodType" />.
  /// </remarks>
  [PXDBShort(MinValue = 0)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Occurrences")]
  public virtual short? Occurrences
  {
    get => this._Occurrences;
    set => this._Occurrences = value;
  }

  /// <summary>
  /// The number of <see cref="T:PX.Objects.GL.FinPeriods.OrganizationFinPeriod">financial periods</see>
  /// that should pass between revenue or expense recognitions.
  /// </summary>
  /// <remarks>
  /// This field is not applicable in case of a flexible
  /// deferral code. For details, see <see cref="T:PX.Objects.DR.DeferredMethodType" />.
  /// </remarks>
  [PXDBShort(MinValue = 1)]
  [PXUIField]
  [PXDefault(1)]
  public virtual short? Frequency
  {
    get => this._Frequency;
    set => this._Frequency = value;
  }

  /// <summary>
  /// The position of the revenue or expense recognition date
  /// within the financial period.
  /// </summary>
  /// <value>
  /// This field can have one of the values defined by the
  /// <see cref="T:PX.Objects.DR.DRScheduleOption" /> class.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [PXDefault("S")]
  [LabelList(typeof (DRScheduleOption))]
  public virtual string ScheduleOption { get; set; }

  /// <summary>
  /// If <see cref="P:PX.Objects.DR.DRDeferredCode.ScheduleOption" /> is set to
  /// <see cref="F:PX.Objects.DR.DRScheduleOption.ScheduleOptionFixedDate" />,
  /// defines the number of day within the financial period
  /// on which revenue or expense recognition should occur.
  /// </summary>
  [PXDBShort(MinValue = 1, MaxValue = 31 /*0x1F*/)]
  [PXUIField]
  [PXDefault(1)]
  public virtual short? FixedDay
  {
    get => this._FixedDay;
    set => this._FixedDay = value;
  }

  /// <summary>
  /// The revenue recognition method associated with
  /// the deferral code.
  /// </summary>
  /// <value>
  /// This field can have one of the values defined by
  /// the <see cref="T:PX.Objects.DR.DeferredMethodType" /> class.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [PXDefault("E")]
  [LabelList(typeof (DeferredMethodType))]
  public virtual string Method { get; set; }

  /// <summary>
  /// When set to <c>true</c> indicates that this deferral code is used for items
  /// that represent multiple deliverable arrangements and
  /// for which the revenue should be split into multiple components.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Multiple-Deliverable Arrangement")]
  public virtual bool? MultiDeliverableArrangement
  {
    get => this._MultiDeliverableArrangement;
    set => this._MultiDeliverableArrangement = value;
  }

  /// <summary>
  /// Determines the source from which the deferral account
  /// should be taken during deferral schedule creation.
  /// </summary>
  /// <value>
  /// This field can have one of the values defined by
  /// <see cref="T:PX.Objects.DR.DeferralAccountSource.ListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("D")]
  [DeferralAccountSource.List]
  [PXUIField(DisplayName = "Use Deferral Account From")]
  public virtual string AccountSource
  {
    get => this._AccountSource;
    set => this._AccountSource = value;
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that the deferral
  /// subaccount should be copied from the income or expense
  /// subaccount of the document line that contains the
  /// deferral code.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Sub. from Sales/Expense Sub.")]
  public virtual bool? CopySubFromSourceTran
  {
    get => this._CopySubFromSourceTran;
    set => this._CopySubFromSourceTran = value;
  }

  /// <summary>
  /// When <see cref="P:PX.Objects.DR.DRDeferredCode.CopySubFromSourceTran" /> is set to <c>false</c>,
  /// specifies the subaccount mask that defines the structure
  /// of the deferral subaccount for the Accounts Receivable module.
  /// </summary>
  [PXDefault]
  [SubAccountMaskAR(DisplayName = "Combine Deferral Sub. From")]
  public virtual string DeferralSubMaskAR
  {
    get => this._DeferralSubMaskAR;
    set => this._DeferralSubMaskAR = value;
  }

  /// <summary>
  /// When <see cref="P:PX.Objects.DR.DRDeferredCode.CopySubFromSourceTran" /> is set to <c>false</c>,
  /// specifies the subaccount mask that defines the structure
  /// of the deferral subaccount for the Accounts Payable module.
  /// </summary>
  [PXDefault]
  [SubAccountMaskAP(DisplayName = "Combine Deferral Sub. From")]
  public virtual string DeferralSubMaskAP
  {
    get => this._DeferralSubMaskAP;
    set => this._DeferralSubMaskAP = value;
  }

  /// <summary>The field is used for UI rendering purpose only.</summary>
  [PXString]
  [PXUIField(DisplayName = "Period(s) ")]
  public virtual string Periods { get; set; }

  [PXNote(DescriptionField = typeof (DRDeferredCode.deferredCodeID))]
  public virtual Guid? NoteID { get; set; }

  /// <summary>
  /// For flexible recognition methods, specifies if the system should
  /// allow recognition in periods that are earlier than the document date.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Recognition in Previous Periods")]
  public virtual bool? RecognizeInPastPeriods { get; set; }

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

  public class PK : PrimaryKeyOf<DRDeferredCode>.By<DRDeferredCode.deferredCodeID>
  {
    public static DRDeferredCode Find(PXGraph graph, string deferredCodeID, PKFindOptions options = 0)
    {
      return (DRDeferredCode) PrimaryKeyOf<DRDeferredCode>.By<DRDeferredCode.deferredCodeID>.FindBy(graph, (object) deferredCodeID, options);
    }
  }

  public static class FK
  {
    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<DRDeferredCode>.By<DRDeferredCode.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<DRDeferredCode>.By<DRDeferredCode.subID>
    {
    }
  }

  public abstract class deferredCodeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRDeferredCode.deferredCodeID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRDeferredCode.description>
  {
  }

  public abstract class accountType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRDeferredCode.accountType>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRDeferredCode.accountID>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DRDeferredCode.active>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRDeferredCode.subID>
  {
  }

  public abstract class reconNowPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DRDeferredCode.reconNowPct>
  {
  }

  public abstract class startOffset : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  DRDeferredCode.startOffset>
  {
  }

  public abstract class occurrences : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  DRDeferredCode.occurrences>
  {
  }

  public abstract class frequency : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  DRDeferredCode.frequency>
  {
  }

  public abstract class scheduleOption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRDeferredCode.scheduleOption>
  {
  }

  public abstract class fixedDay : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  DRDeferredCode.fixedDay>
  {
  }

  public abstract class method : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRDeferredCode.method>
  {
  }

  public abstract class multiDeliverableArrangement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DRDeferredCode.multiDeliverableArrangement>
  {
  }

  public abstract class accountSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRDeferredCode.accountSource>
  {
  }

  public abstract class copySubFromSourceTran : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DRDeferredCode.copySubFromSourceTran>
  {
  }

  public abstract class deferralSubMaskAR : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRDeferredCode.deferralSubMaskAR>
  {
  }

  public abstract class deferralSubMaskAP : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRDeferredCode.deferralSubMaskAP>
  {
  }

  public abstract class periods : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRDeferredCode.periods>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  DRDeferredCode.noteID>
  {
  }

  public abstract class recognizeInPastPeriods : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DRDeferredCode.recognizeInPastPeriods>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  DRDeferredCode.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  DRDeferredCode.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRDeferredCode.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DRDeferredCode.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DRDeferredCode.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRDeferredCode.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DRDeferredCode.lastModifiedDateTime>
  {
  }
}

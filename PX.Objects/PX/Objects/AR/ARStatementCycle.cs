// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARStatementCycle
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXCacheName("Statement Cycle")]
[PXPrimaryGraph(typeof (ARStatementMaint))]
[Serializable]
public class ARStatementCycle : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// A non-DB field indicating whether the current statement
  /// cycle has been selected for processing by a user.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; } = new bool?(false);

  /// <summary>
  /// A non-DB calculated field indicating the next date
  /// on which the customer statements will be generated.
  /// </summary>
  [PXDate]
  [PXUIField]
  public virtual DateTime? NextStmtDate { get; set; }

  /// <summary>
  /// Key field. A human-readable unique string identifier
  /// of the statement cycle.
  /// </summary>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (ARStatementCycle.statementCycleId))]
  [PXReferentialIntegrityCheck]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string StatementCycleId { get; set; }

  /// <summary>The statement cycle description.</summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  [PXDefault]
  [PXFieldDescription]
  public virtual string Descr { get; set; }

  /// <summary>
  /// A boolean value indicating whether financial periods should be used instead
  /// of user-defined aging periods. If <c>true</c>, the fields <see cref="P:PX.Objects.AR.ARStatementCycle.AgeDays00" />,
  /// <see cref="P:PX.Objects.AR.ARStatementCycle.AgeDays01" />, and <see cref="P:PX.Objects.AR.ARStatementCycle.AgeDays02" /> will not be used for aging.
  /// Instead, the current (corresponding to the statement/aging date) and the three
  /// preceding financial periods will be used.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Use Financial Periods for Aging")]
  [PXDefault(false)]
  public virtual bool? UseFinPeriodForAging { get; set; }

  /// <summary>
  /// An integer value indicating the upper inclusive bound, in days, of the first
  /// aging period. For example, if <see cref="P:PX.Objects.AR.ARStatementCycle.AgeDays00" /> is equal to 7, then the first
  /// aging period will correspond to documents that are 1-7 days past due.
  /// </summary>
  [PXDBShort(MinValue = 0)]
  [PXDefault(0)]
  [PXUIField]
  [PXUIEnabled(typeof (Where<ARStatementCycle.useFinPeriodForAging, Equal<False>>))]
  public virtual short? AgeDays00 { get; set; }

  /// <summary>
  /// An integer value indicating the upper inclusive bound, in days, of the second
  /// aging period. For example, if <see cref="P:PX.Objects.AR.ARStatementCycle.AgeDays00" /> is equal to 7, and
  /// <see cref="P:PX.Objects.AR.ARStatementCycle.AgeDays01" /> is equal to 14, then the second aging period will
  /// correspond to documents that are 8-14 days past due.
  /// </summary>
  [PXDBShort(MinValue = 0)]
  [PXDefault(0)]
  [PXUIField]
  [PXUIEnabled(typeof (Where<ARStatementCycle.useFinPeriodForAging, Equal<False>>))]
  public virtual short? AgeDays01 { get; set; }

  /// <summary>
  /// An integer value indicating the upper inclusive bound, in days, of the third
  /// non-current aging period. For example, if <see cref="P:PX.Objects.AR.ARStatementCycle.AgeDays01" /> is equal to 14,
  /// and <see cref="P:PX.Objects.AR.ARStatementCycle.AgeDays02" /> is equal to 21, then the third aging period will
  /// correspond to documents that are 15-21 days past due.
  /// </summary>
  [PXDBShort(MinValue = 0)]
  [PXDefault(0)]
  [PXUIField]
  [PXUIEnabled(typeof (Where<ARStatementCycle.useFinPeriodForAging, Equal<False>>))]
  public virtual short? AgeDays02 { get; set; }

  /// <summary>
  /// A display-only integer field indicating the lower inclusive bound, in days,
  /// of the first aging period.
  /// </summary>
  [PXInt]
  [PXDefault(0)]
  [PXUIField(Enabled = false)]
  [PXFormula(typeof (int1))]
  public virtual int? Bucket01LowerInclusiveBound { get; set; }

  /// <summary>
  /// A display-only integer field indicating the lower inclusive bound, in days,
  /// of the second aging period.
  /// </summary>
  [PXInt]
  [PXDefault(0)]
  [PXUIField(Enabled = false)]
  [PXFormula(typeof (Add<ARStatementCycle.ageDays00, int1>))]
  public virtual int? Bucket02LowerInclusiveBound { get; set; }

  /// <summary>
  /// A display-only integer field indicating the lower inclusive bound, in days,
  /// of the third aging period.
  /// </summary>
  [PXInt]
  [PXDefault(0)]
  [PXUIField(Enabled = false)]
  [PXFormula(typeof (Add<ARStatementCycle.ageDays01, int1>))]
  public virtual int? Bucket03LowerInclusiveBound { get; set; }

  /// <summary>
  /// A display-only integer field indicating the lower exclusive bound, in days,
  /// of the last aging period (the "over" period).
  /// </summary>
  [PXInt]
  [PXDefault(0)]
  [PXUIField(Enabled = false)]
  [PXFormula(typeof (Add<ARStatementCycle.ageDays02, int0>))]
  public virtual int? Bucket04LowerExclusiveBound { get; set; }

  /// <summary>
  /// The description of the zeroth (current) aging period, which incorporates
  /// documents that are not overdue.
  /// </summary>
  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXUIEnabled(typeof (Where<ARStatementCycle.useFinPeriodForAging, Equal<False>>))]
  public virtual string AgeMsgCurrent { get; set; }

  /// <summary>
  /// The description of the first aging period, which incorporates documents
  /// that are from 1 to <see cref="P:PX.Objects.AR.ARStatementCycle.AgeDays00" /> days past due.
  /// </summary>
  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField]
  [PXUIEnabled(typeof (Where<ARStatementCycle.useFinPeriodForAging, Equal<False>>))]
  public virtual string AgeMsg00 { get; set; }

  /// <summary>
  /// The description of the second aging period, which incorporates documents
  /// that are from <see cref="P:PX.Objects.AR.ARStatementCycle.AgeDays00" /> + 1 to <see cref="P:PX.Objects.AR.ARStatementCycle.AgeDays01" />
  /// days past due.
  /// </summary>
  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField]
  [PXUIEnabled(typeof (Where<ARStatementCycle.useFinPeriodForAging, Equal<False>>))]
  public virtual string AgeMsg01 { get; set; }

  /// <summary>
  /// The description of the third aging period that incorporates documents
  /// that are from <see cref="P:PX.Objects.AR.ARStatementCycle.AgeDays01" /> + 1 to <see cref="P:PX.Objects.AR.ARStatementCycle.AgeDays02" />
  /// days past due.
  /// </summary>
  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField]
  [PXUIEnabled(typeof (Where<ARStatementCycle.useFinPeriodForAging, Equal<False>>))]
  public virtual string AgeMsg02 { get; set; }

  /// <summary>
  /// The description of the last aging period that incorporates documents
  /// that are over <see cref="P:PX.Objects.AR.ARStatementCycle.AgeDays02" /> days past due.
  /// </summary>
  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField]
  [PXUIEnabled(typeof (Where<ARStatementCycle.useFinPeriodForAging, Equal<False>>))]
  public virtual string AgeMsg03 { get; set; }

  /// <summary>Obsolete field.</summary>
  [PXDBDate]
  [Obsolete("This field is not used anymore and will be removed in Acumatica 7.0")]
  public virtual DateTime? LastAgeDate { get; set; }

  /// <summary>
  /// Indicates the date on which financial charges were last generated for
  /// the current statement cycle.
  /// </summary>
  [PXDBDate]
  [PXUIField]
  public virtual DateTime? LastFinChrgDate { get; set; }

  /// <summary>
  /// Indicates the date on which customer statements were last generated for
  /// the current statement cycle.
  /// </summary>
  [PXDBDate]
  [PXUIField]
  public virtual DateTime? LastStmtDate { get; set; }

  /// <summary>
  /// Indicates the type of schedule, according to which customer statements
  /// are generated within the current statement cycle. See <see cref="T:PX.Objects.AR.ARStatementScheduleType" />.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [LabelList(typeof (ARStatementScheduleType))]
  [PXDefault("E")]
  [PXUIField(DisplayName = "Schedule Type")]
  public virtual string PrepareOn { get; set; }

  /// <summary>
  /// For <see cref="F:PX.Objects.AR.ARStatementScheduleType.TwiceAMonth" /> and
  /// <see cref="F:PX.Objects.AR.ARStatementScheduleType.FixedDayOfMonth" /> schedule types,
  /// indicates the first (or the only, correspondingly) day of month, on
  /// which customer statements are generated.
  /// </summary>
  [PXDBShort]
  [PXUIField(DisplayName = "Day of Month 1")]
  [PXUIVisible(typeof (Where<ARStatementCycle.prepareOn, Equal<ARStatementScheduleType.fixedDayOfMonth>, Or<ARStatementCycle.prepareOn, Equal<ARStatementScheduleType.twiceAMonth>>>))]
  public virtual short? Day00 { get; set; }

  /// <summary>
  /// For <see cref="F:PX.Objects.AR.ARStatementScheduleType.TwiceAMonth" /> schedule type,
  /// indicates the second day of month, on which bi-monthly customer
  /// statements are generated.
  /// </summary>
  [PXDBShort]
  [PXUIField(DisplayName = "Day of Month 2")]
  [PXUIVisible(typeof (Where<ARStatementCycle.prepareOn, Equal<ARStatementScheduleType.twiceAMonth>>))]
  public virtual short? Day01 { get; set; }

  /// <summary>
  /// For <see cref="F:PX.Objects.AR.ARStatementScheduleType.Weekly" /> schedule type,
  /// indicates the day of the week, on which weekly customer statements
  /// are generated.
  /// </summary>
  [PXDBInt]
  [PX.Objects.EP.DayOfWeek]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Day of Week")]
  [PXUIVisible(typeof (Where<ARStatementCycle.prepareOn, Equal<ARStatementScheduleType.weekly>>))]
  public virtual int? DayOfWeek { get; set; }

  /// <summary>
  /// A boolean value indicating whether financial charges should be applied
  /// to customers belonging to the current statement cycle.
  /// </summary>
  [PXDBBool]
  [PXDefault(false, typeof (Search<CustomerClass.finChargeApply, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  [PXUIField(DisplayName = "Apply Overdue Charges")]
  public virtual bool? FinChargeApply { get; set; }

  /// <summary>
  /// The reference to the overdue charge that should be calculated
  /// for customers belonging to the current statement cycle.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Overdue Charge ID")]
  [PXSelector(typeof (ARFinCharge.finChargeID), DescriptionField = typeof (ARFinCharge.finChargeDesc))]
  public virtual string FinChargeID { get; set; }

  /// <summary>
  /// A boolean value indicating whether the system should require
  /// all open customer payments to be applied in full before generating
  /// customer statements.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Payment Application Before Statement")]
  public virtual bool? RequirePaymentApplication { get; set; }

  /// <summary>
  /// A boolean value indicating whether the system should require
  /// the overdue charges calculation before generating customer statements.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Overdue Charges Calculation Before Statement")]
  public virtual bool? RequireFinChargeProcessing { get; set; }

  /// <summary>
  /// Indicates whether documents of customers belonging to the current
  /// statement cycle should be aged based on their document date,
  /// or their due date.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [LabelList(typeof (AgeBasedOnType))]
  [PXDefault("U")]
  [PXUIField(DisplayName = "Age Based On")]
  public virtual string AgeBasedOn { get; set; }

  /// <summary>
  /// If <c>true</c>, the system will generate but not print or email
  /// Open Item statements without open documents, and Balance Brought
  /// Forward statements if there was no activity in the period and
  /// the balance brought from the previous statement is zero.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Print Empty Statements")]
  public virtual bool? PrintEmptyStatements { get; set; }

  /// <summary>
  /// The unique identifier of the note associated with the current
  /// statement cycle.
  /// </summary>
  [PXNote(DescriptionField = typeof (ARStatementCycle.statementCycleId))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

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

  /// <exclude />
  public class PK : PrimaryKeyOf<ARStatementCycle>.By<ARStatementCycle.statementCycleId>
  {
    public static ARStatementCycle Find(
      PXGraph graph,
      string statementCycleId,
      PKFindOptions options = 0)
    {
      return (ARStatementCycle) PrimaryKeyOf<ARStatementCycle>.By<ARStatementCycle.statementCycleId>.FindBy(graph, (object) statementCycleId, options);
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARStatementCycle.selected>
  {
  }

  public abstract class nextStmtDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARStatementCycle.nextStmtDate>
  {
  }

  public abstract class statementCycleId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementCycle.statementCycleId>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatementCycle.descr>
  {
  }

  public abstract class useFinPeriodForAging : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARStatementCycle.useFinPeriodForAging>
  {
  }

  public abstract class ageDays00 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARStatementCycle.ageDays00>
  {
  }

  public abstract class ageDays01 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARStatementCycle.ageDays01>
  {
  }

  public abstract class ageDays02 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARStatementCycle.ageDays02>
  {
  }

  public abstract class bucket01LowerInclusiveBound : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARStatementCycle.bucket01LowerInclusiveBound>
  {
  }

  public abstract class bucket02LowerInclusiveBound : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARStatementCycle.bucket02LowerInclusiveBound>
  {
  }

  public abstract class bucket03LowerInclusiveBound : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARStatementCycle.bucket03LowerInclusiveBound>
  {
  }

  public abstract class bucket04LowerExclusiveBound : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARStatementCycle.bucket04LowerExclusiveBound>
  {
  }

  public abstract class ageMsgCurrent : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementCycle.ageMsgCurrent>
  {
  }

  public abstract class ageMsg00 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatementCycle.ageMsg00>
  {
  }

  public abstract class ageMsg01 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatementCycle.ageMsg01>
  {
  }

  public abstract class ageMsg02 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatementCycle.ageMsg02>
  {
  }

  public abstract class ageMsg03 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatementCycle.ageMsg03>
  {
  }

  [Obsolete("This field is not used anymore and will be removed in Acumatica 7.0")]
  public abstract class lastAgeDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARStatementCycle.lastAgeDate>
  {
  }

  public abstract class lastFinChrgDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARStatementCycle.lastFinChrgDate>
  {
  }

  public abstract class lastStmtDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARStatementCycle.lastStmtDate>
  {
  }

  public abstract class prepareOn : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatementCycle.prepareOn>
  {
  }

  public abstract class day00 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARStatementCycle.day00>
  {
  }

  public abstract class day01 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARStatementCycle.day01>
  {
  }

  public abstract class dayOfWeek : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARStatementCycle.dayOfWeek>
  {
  }

  public abstract class finChargeApply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARStatementCycle.finChargeApply>
  {
  }

  public abstract class finChargeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatementCycle.finChargeID>
  {
  }

  public abstract class requirePaymentApplication : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARStatementCycle.requirePaymentApplication>
  {
  }

  public abstract class requireFinChargeProcessing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARStatementCycle.requireFinChargeProcessing>
  {
  }

  public abstract class ageBasedOn : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatementCycle.ageBasedOn>
  {
  }

  public abstract class printEmptyStatements : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARStatementCycle.printEmptyStatements>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARStatementCycle.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARStatementCycle.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARStatementCycle.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementCycle.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARStatementCycle.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARStatementCycle.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementCycle.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARStatementCycle.lastModifiedDateTime>
  {
  }
}

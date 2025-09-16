// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.EPTimeLog
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using System;

#nullable enable
namespace PX.Objects.EP.ClockInClockOut;

/// <summary>Represents the clock-in and clock-out time log.</summary>
[PXCacheName("Time Log")]
[PXInternalUseOnly]
public class EPTimeLog : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? TimeLogID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.EP.EPEmployee">employee</see> who uses the time logging.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (Search<PX.Objects.EP.EPEmployee.bAccountID, Where<PX.Objects.EP.EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>))]
  [PXUIField(DisplayName = "Employee")]
  [PXSubordinateAndWingmenSelector(typeof (EPDelegationOf.timeEntries))]
  [PXFieldDescription]
  public virtual int? EmployeeID { get; set; }

  /// <summary>
  /// Contains the type of the related entity that is specified in <see cref="P:PX.Objects.EP.ClockInClockOut.EPTimeLog.RelatedEntityID" />.
  /// </summary>
  [PXDBString(255 /*0xFF*/)]
  [PXEntityTypeList]
  [PXUIField(DisplayName = "Document Type", Enabled = false, IsReadOnly = true)]
  public virtual 
  #nullable disable
  string RelatedEntityType { get; set; }

  /// <summary>
  /// Contains the <see cref="P:PX.Data.INotable.NoteID" /> value of the time log-related entity.
  /// </summary>
  [PXDBGuid(false)]
  public virtual Guid? RelatedEntityID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.EP.ClockInClockOut.EPTimeLogType">time log type</see> associated with the time log record.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.EP.ClockInClockOut.EPTimeLogType.TimeLogTypeID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXSelector(typeof (EPTimeLogType.timeLogTypeID), DescriptionField = typeof (EPTimeLogType.description), ValidateValue = false)]
  [PXUIField(DisplayName = "Type")]
  public virtual string TimeLogTypeID { get; set; }

  /// <summary>The document number of the time log record.</summary>
  /// <value>
  /// The value of this field corresponds to the unique key values of the entity specified in <see cref="P:PX.Objects.EP.ClockInClockOut.EPTimeLog.RelatedEntityID" />.
  /// </value>
  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Document Nbr.", Enabled = false, IsReadOnly = true)]
  public virtual string DocumentNbr { get; set; }

  /// <summary>The summary of the time log record.</summary>
  /// <value>
  /// The value of this field corresponds to the description of the entity specified in <see cref="P:PX.Objects.EP.ClockInClockOut.EPTimeLog.RelatedEntityID" />.
  /// </value>
  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Summary { get; set; }

  /// <summary>The timezone of the time log record.</summary>
  [PXUIField(DisplayName = "Reported in Time Zone", Enabled = false, Visible = false)]
  [PXDBString(32 /*0x20*/)]
  [PXTimeZone(true)]
  public virtual string ReportedInTimeZoneID { get; set; }

  /// <summary>The start date and time of the time log record.</summary>
  [PXDBDateAndTime(DisplayNameDate = "Start Date", DisplayNameTime = "Start Time", UseTimeZone = true)]
  [PXUIField(DisplayName = "Start Date")]
  public virtual System.DateTime? StartDate { get; set; }

  /// <summary>The end date and time of the time log record.</summary>
  [PXDBDateAndTime(DisplayNameDate = "End Date", DisplayNameTime = "End Time", UseTimeZone = true)]
  [PXUIField(DisplayName = "End Date")]
  public virtual System.DateTime? EndDate { get; set; }

  /// <summary>The time spent of the time log record.</summary>
  /// <value>
  /// The interval in minutes between <see cref="P:PX.Objects.EP.ClockInClockOut.EPTimeLog.StartDate" /> and <see cref="P:PX.Objects.EP.ClockInClockOut.EPTimeLog.EndDate" />.
  /// </value>
  [PXDBInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Time Spent")]
  public virtual int? TimeSpent { get; set; }

  /// <summary>
  /// The field is equal to True if at least one <see cref="T:PX.Objects.CR.PMTimeActivity" /> has been created from this record.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsLocked { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "Created At", Enabled = false, IsReadOnly = true)]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By", Enabled = false, IsReadOnly = true)]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "Last Modified At", Enabled = false, IsReadOnly = true)]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  public abstract class timeLogID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeLog.timeLogID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeLog.employeeID>
  {
  }

  public abstract class relatedEntityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeLog.relatedEntityType>
  {
  }

  public abstract class relatedEntityID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPTimeLog.relatedEntityID>
  {
  }

  public abstract class timeLogTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTimeLog.timeLogTypeID>
  {
  }

  public abstract class documentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTimeLog.documentNbr>
  {
  }

  public abstract class summary : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeLog.summary>
  {
  }

  public abstract class reportedInTimeZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeLog.reportedInTimeZoneID>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  EPTimeLog.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  EPTimeLog.endDate>
  {
  }

  public abstract class timeSpent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeLog.timeSpent>
  {
  }

  public abstract class isLocked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPTimeLog.isLocked>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPTimeLog.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPTimeLog.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeLog.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPTimeLog.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPTimeLog.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeLog.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPTimeLog.lastModifiedDateTime>
  {
  }
}

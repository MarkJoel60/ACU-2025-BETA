// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.EPClockInTimerData
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

/// <summary>Represents the clock-in and clock-out timer data.</summary>
[PXCacheName("Timer")]
[PXInternalUseOnly]
public class EPClockInTimerData : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The unique identifier of the clock-in and clock-out timer record.
  /// </summary>
  [PXDBIdentity(IsKey = true)]
  public virtual int? TimerDataID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.EP.EPEmployee">employee</see> associated with the timer record.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.EP.EPEmployee.bAccountID" /> field.
  /// </value>
  [PXDBInt]
  public virtual int? EmployeeID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.EP.ClockInClockOut.EPTimeLogType">time log type</see> associated with the timer record.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.EP.ClockInClockOut.EPTimeLogType.TimeLogTypeID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault(typeof (Search<EPSetupClockIn.defTimeLogTypeID>), PersistingCheck = PXPersistingCheck.Nothing)]
  [TimeLogTypeList]
  [PXUIField(DisplayName = "Type")]
  public virtual 
  #nullable disable
  string TimeLogTypeID { get; set; }

  /// <summary>The summary of the timer record.</summary>
  /// <value>
  /// The value of this field corresponds to the description of the entity specified in <see cref="P:PX.Objects.EP.ClockInClockOut.EPClockInTimerData.RelatedEntityID" />.
  /// </value>
  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Summary")]
  public virtual string Summary { get; set; }

  /// <summary>The document number of the timer record.</summary>
  /// <value>
  /// The value of this field corresponds to the unique key values of the entity specified in <see cref="P:PX.Objects.EP.ClockInClockOut.EPClockInTimerData.RelatedEntityID" />.
  /// </value>
  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Document Nbr.")]
  public virtual string DocumentNbr { get; set; }

  /// <summary>
  /// Contains the type of the related entity that is specified in <see cref="P:PX.Objects.EP.ClockInClockOut.EPClockInTimerData.RelatedEntityID" />.
  /// </summary>
  [PXDBString(255 /*0xFF*/)]
  [PXEntityTypeList]
  public virtual string RelatedEntityType { get; set; }

  /// <summary>
  /// Contains the <see cref="P:PX.Data.INotable.NoteID" /> value of the timer-related entity.
  /// </summary>
  [PXDBGuid(false)]
  public virtual Guid? RelatedEntityID { get; set; }

  /// <summary>The start date and time of the timer record.</summary>
  [PXDBDateAndTime(UseTimeZone = false)]
  [PXUIField(DisplayName = "Start Date")]
  public virtual System.DateTime? StartDate { get; set; }

  /// <summary>The time spent when the timer was running.</summary>
  /// <value>The value of this field is in seconds.</value>
  [PXDBInt]
  [PXUIField(DisplayName = "Time Spent")]
  public virtual int? TimeSpent { get; set; }

  /// <summary>The status of the timer record.</summary>
  /// <value>
  /// The field can have one of the values listed in <see cref="T:PX.Objects.EP.ClockInClockOut.EPTimerStatus" />.
  /// </value>
  [PXString(1, IsFixed = true)]
  [EPTimerStatus.List]
  [ClockInTimerStatus(typeof (EPClockInTimerData.timerDataID), typeof (EPClockInTimerData.startDate))]
  [PXUIField(DisplayName = "Status")]
  public virtual string Status { get; set; }

  /// <summary>The user-friendly name of the timer-related entity.</summary>
  /// <value>
  /// The value of this field corresponds to the entity type specified in <see cref="P:PX.Objects.EP.ClockInClockOut.EPClockInTimerData.RelatedEntityID" />.
  /// </value>
  [PXString]
  [DisplayEntityName(typeof (EPClockInTimerData.relatedEntityType))]
  [PXUIField(DisplayName = "Entity Name")]
  public virtual string EntityName { get; set; }

  /// <summary>The start date of the timer.</summary>
  /// <value>
  /// The value of this field is compatible with JavaScript and TypeScript UTC date/time string format.
  /// </value>
  [PXString]
  public virtual string StartDateUTC
  {
    get
    {
      return !this.StartDate.HasValue ? (string) null : this.StartDate.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
    }
  }

  /// <summary>
  /// The placeholder field to show the elapsed time of the timer in the UI.
  /// </summary>
  /// <value>The value of this field is set in the UI only.</value>
  [PXString]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIVisible(typeof (Where<EPClockInTimerData.status, NotEqual<EPTimerStatus.stopped>, PX.Data.And<BqlOperand<EPClockInTimerData.startDate, IBqlDateTime>.IsNotNull>>))]
  public virtual string TimerDisplay => string.Empty;

  /// <summary>
  /// A Boolean value that indicates (if set to True) that the Clock-In button should be displayed.
  /// </summary>
  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? IsClockIn
  {
    [PXDependsOnFields(new System.Type[] {typeof (EPClockInTimerData.status)})] get
    {
      return new bool?(this.Status == "S");
    }
  }

  /// <summary>
  /// A Boolean value that indicates (if set to True) that the Pause button should be displayed.
  /// </summary>
  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? IsPause
  {
    [PXDependsOnFields(new System.Type[] {typeof (EPClockInTimerData.status)})] get
    {
      return new bool?(this.Status == "R");
    }
  }

  /// <summary>
  /// A Boolean value that indicates (if set to True) that the Start button should be displayed.
  /// </summary>
  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? IsStart
  {
    [PXDependsOnFields(new System.Type[] {typeof (EPClockInTimerData.status)})] get
    {
      return new bool?(this.Status == "P");
    }
  }

  /// <summary>
  /// A Boolean value that indicates (if set to True) that the Stop button should be displayed.
  /// </summary>
  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? IsStop
  {
    [PXDependsOnFields(new System.Type[] {typeof (EPClockInTimerData.status)})] get
    {
      return new bool?(this.Status == "P" || this.Status == "R");
    }
  }

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

  public abstract class timerDataID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPClockInTimerData.timerDataID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPClockInTimerData.employeeID>
  {
  }

  public abstract class timeLogTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPClockInTimerData.timeLogTypeID>
  {
  }

  public abstract class summary : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPClockInTimerData.summary>
  {
  }

  public abstract class documentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPClockInTimerData.documentNbr>
  {
  }

  public abstract class relatedEntityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPClockInTimerData.relatedEntityType>
  {
  }

  public abstract class relatedEntityID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPClockInTimerData.relatedEntityID>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPClockInTimerData.startDate>
  {
  }

  public abstract class timeSpent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPClockInTimerData.timeSpent>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPClockInTimerData.status>
  {
  }

  public abstract class entityName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPClockInTimerData.entityName>
  {
  }

  public abstract class startDateUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPClockInTimerData.startDateUTC>
  {
  }

  public abstract class timerDisplay : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPClockInTimerData.timerDisplay>
  {
  }

  public abstract class isClockIn : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPClockInTimerData.isClockIn>
  {
  }

  public abstract class isPause : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPClockInTimerData.isPause>
  {
  }

  public abstract class isStart : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPClockInTimerData.isStart>
  {
  }

  public abstract class isStop : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPClockInTimerData.isStop>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPClockInTimerData.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPClockInTimerData.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPClockInTimerData.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPClockInTimerData.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPClockInTimerData.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPClockInTimerData.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPClockInTimerData.lastModifiedDateTime>
  {
  }
}

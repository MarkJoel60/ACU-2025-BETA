// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSTimeSlot
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSTimeSlot : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _TimeStart;
  protected DateTime? _TimeEnd;

  [PXDBIdentity(IsKey = true)]
  public virtual int? TimeSlotID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Branch ID")]
  [PXSelector(typeof (Search<Branch.branchID>), SubstituteKey = typeof (Branch.branchCD))]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Branch Location ID")]
  public virtual int? BranchLocationID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Employee ID")]
  public virtual int? EmployeeID { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true)]
  [PXUIField(DisplayName = "Time Start")]
  public virtual DateTime? TimeStart
  {
    get => this._TimeStart;
    set
    {
      this.TimeStartUTC = value;
      this._TimeStart = value;
    }
  }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true)]
  [PXUIField(DisplayName = "Time End")]
  public virtual DateTime? TimeEnd
  {
    get => this._TimeEnd;
    set
    {
      this.TimeEndUTC = value;
      this._TimeEnd = value;
    }
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Schedule ID")]
  public virtual int? ScheduleID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Record Count")]
  public virtual int? RecordCount { get; set; }

  [PXDBString(1)]
  [ListField_ScheduleType.ListAtrribute]
  public virtual 
  #nullable disable
  string ScheduleType { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Generation ID")]
  public virtual int? GenerationID { get; set; }

  [PXDBDecimal(2)]
  [PXUIField(DisplayName = "Time Difference", Enabled = false)]
  public virtual Decimal? TimeDiff { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Slot Level")]
  public virtual int? SlotLevel { get; set; }

  [PXString]
  public virtual string CustomID { get; set; }

  [PXString]
  public virtual string CustomDateID { get; set; }

  [PXString]
  public virtual string WrkEmployeeScheduleID { get; set; }

  [PXString]
  public virtual string BranchLocationDesc { get; set; }

  [PXString]
  public virtual string BranchLocationCD { get; set; }

  [PXString]
  public virtual string CustomDateTimeStart
  {
    get => this.TimeStart.HasValue ? this.TimeStart.ToString() : string.Empty;
  }

  [PXString]
  public virtual string CustomDateTimeEnd
  {
    get => this.TimeEnd.HasValue ? this.TimeEnd.ToString() : string.Empty;
  }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "CreatedByID")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "CreatedDateTime")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "LastModifiedByID")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "LastModifiedDateTime")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "tstamp")]
  public virtual byte[] tstamp { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true)]
  [PXUIField(DisplayName = "Time Start")]
  public virtual DateTime? TimeStartUTC { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true)]
  [PXUIField(DisplayName = "Time End")]
  public virtual DateTime? TimeEndUTC { get; set; }

  public abstract class timeSlotID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSTimeSlot.timeSlotID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSTimeSlot.branchID>
  {
  }

  public abstract class branchLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSTimeSlot.branchLocationID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSTimeSlot.employeeID>
  {
  }

  public abstract class timeStart : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSTimeSlot.timeStart>
  {
  }

  public abstract class timeEnd : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSTimeSlot.timeEnd>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSTimeSlot.scheduleID>
  {
  }

  public abstract class recordCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSTimeSlot.recordCount>
  {
  }

  public abstract class scheduleType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSTimeSlot.scheduleType>
  {
  }

  public abstract class generationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSTimeSlot.generationID>
  {
  }

  public abstract class timeDiff : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSTimeSlot.timeDiff>
  {
  }

  public abstract class slotLevel : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSTimeSlot.slotLevel>
  {
  }

  public abstract class customID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSTimeSlot.customID>
  {
  }

  public abstract class customDateID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSTimeSlot.customID>
  {
  }

  public abstract class wrkEmployeeScheduleID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSTimeSlot.wrkEmployeeScheduleID>
  {
  }

  public abstract class branchLocationDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSTimeSlot.branchLocationDesc>
  {
  }

  public abstract class branchLocationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSTimeSlot.branchLocationCD>
  {
  }

  public abstract class customDateTimeStart : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSTimeSlot.customDateTimeStart>
  {
  }

  public abstract class customDateTimeEnd : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSTimeSlot.customDateTimeEnd>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSTimeSlot.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSTimeSlot.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSTimeSlot.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSTimeSlot.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSTimeSlot.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSTimeSlot.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSTimeSlot.Tstamp>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSTimeSlot.noteID>
  {
  }

  public abstract class timeStartUTC : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSTimeSlot.timeStartUTC>
  {
  }

  public abstract class timeEndUTC : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSTimeSlot.timeEndUTC>
  {
  }
}

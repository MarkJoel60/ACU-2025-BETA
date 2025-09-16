// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSWrkEmployeeSchedule
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
public class FSWrkEmployeeSchedule : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _TimeStart;
  protected DateTime? _TimeEnd;

  [PXDBInt]
  [PXDefault(typeof (AccessInfo.branchID))]
  [PXUIField(DisplayName = "Branch ID")]
  [PXSelector(typeof (Search<Branch.branchID>), SubstituteKey = typeof (Branch.branchCD))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Branch Location ID")]
  public virtual int? BranchLocationID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Employee ID")]
  public virtual int? EmployeeID { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, IsKey = true)]
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
  [PXUIField(DisplayName = "Record ID")]
  public virtual int? RecordID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Record Count")]
  public virtual int? RecordCount { get; set; }

  [PXDBString(1)]
  [ListField_ScheduleType.ListAtrribute]
  public virtual 
  #nullable disable
  string ScheduleType { get; set; }

  [PXDBDecimal(2)]
  [PXUIField(DisplayName = "Time Difference", Enabled = false)]
  public virtual Decimal? TimeDiff { get; set; }

  [PXString]
  public virtual string CustomID { get; set; }

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

  [PXString]
  public virtual string ScheduleRefNbr { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, IsKey = true)]
  [PXUIField(DisplayName = "Time Start")]
  public virtual DateTime? TimeStartUTC { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true)]
  [PXUIField(DisplayName = "Time End")]
  public virtual DateTime? TimeEndUTC { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSWrkEmployeeSchedule.branchID>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSWrkEmployeeSchedule.branchLocationID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSWrkEmployeeSchedule.employeeID>
  {
  }

  public abstract class timeStart : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSWrkEmployeeSchedule.timeStart>
  {
  }

  public abstract class timeEnd : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSWrkEmployeeSchedule.timeEnd>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSWrkEmployeeSchedule.recordID>
  {
  }

  public abstract class recordCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSWrkEmployeeSchedule.recordCount>
  {
  }

  public abstract class scheduleType : ListField_ScheduleType
  {
  }

  public abstract class timeDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSWrkEmployeeSchedule.timeDiff>
  {
  }

  public abstract class customID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSWrkEmployeeSchedule.customID>
  {
  }

  public abstract class wrkEmployeeScheduleID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSWrkEmployeeSchedule.wrkEmployeeScheduleID>
  {
  }

  public abstract class branchLocationDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSWrkEmployeeSchedule.branchLocationDesc>
  {
  }

  public abstract class branchLocationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSWrkEmployeeSchedule.branchLocationCD>
  {
  }

  public abstract class customDateTimeStart : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSWrkEmployeeSchedule.customDateTimeStart>
  {
  }

  public abstract class customDateTimeEnd : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSWrkEmployeeSchedule.customDateTimeEnd>
  {
  }

  public abstract class scheduleRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSWrkEmployeeSchedule.scheduleRefNbr>
  {
  }

  public abstract class timeStartUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSWrkEmployeeSchedule.timeStartUTC>
  {
  }

  public abstract class timeEndUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSWrkEmployeeSchedule.timeEndUTC>
  {
  }
}

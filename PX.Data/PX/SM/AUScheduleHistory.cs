// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScheduleHistory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class AUScheduleHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(8, IsFixed = true)]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Schedule ID")]
  [PXSelector(typeof (Search<AUSchedule.scheduleID>), DescriptionField = typeof (AUSchedule.description))]
  public virtual int? ScheduleID { get; set; }

  [PXDBDateWithTicks(IsKey = true, PreserveTime = true, UseTimeZone = true, UseSmallDateTime = false)]
  [PXUIField(DisplayName = "Execution Date", Visible = false)]
  [PXDefault]
  public virtual System.DateTime? ExecutionDate { get; set; }

  [PXDependsOnFields(new System.Type[] {typeof (AUScheduleHistory.executionDate)})]
  [PXDate(InputMask = "g")]
  [PXUIField(DisplayName = "Execution Date", Enabled = false)]
  public virtual System.DateTime? ExecutionDateToDisplay
  {
    get => this.ExecutionDate;
    set
    {
    }
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(Visible = false)]
  public virtual short? ErrorLevel { get; set; }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Execution Result")]
  public virtual string ExecutionResult { get; set; }

  [PXDBTimestamp]
  public virtual byte[] TStamp { get; set; }

  [PXBool]
  [PXUIField(Visible = false)]
  public bool? Selected { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public virtual string ExecutionStatus { get; set; }

  [PXLong]
  [PXUIField(Visible = false)]
  public long? Ticks
  {
    [PXDependsOnFields(new System.Type[] {typeof (AUScheduleHistory.executionDate)})] get
    {
      return this.ExecutionDate.HasValue ? new long?(this.ExecutionDate.Value.Ticks) : new long?();
    }
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScheduleHistory.screenID>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScheduleHistory.scheduleID>
  {
  }

  public abstract class executionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUScheduleHistory.executionDate>
  {
  }

  public abstract class executionDateToDisplay : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUScheduleHistory.executionDateToDisplay>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScheduleHistory.refNoteID>
  {
  }

  public abstract class errorLevel : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUScheduleHistory.errorLevel>
  {
  }

  public abstract class executionResult : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScheduleHistory.executionResult>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUScheduleHistory.tStamp>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScheduleHistory.selected>
  {
  }

  public abstract class executionStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScheduleHistory.executionStatus>
  {
  }

  public abstract class ticks : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  AUScheduleHistory.ticks>
  {
  }
}

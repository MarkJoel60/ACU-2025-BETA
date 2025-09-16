// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScheduleExecution
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
public class AUScheduleExecution : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Screen ID", Enabled = false)]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Schedule", Enabled = false)]
  [PXSelector(typeof (Search<AUSchedule.scheduleID>), SubstituteKey = typeof (AUSchedule.description))]
  public virtual int? ScheduleID { get; set; }

  [PXDBDateWithTicks(IsKey = true, PreserveTime = true, UseTimeZone = true, UseSmallDateTime = false, InputMask = "g", DisplayMask = "g")]
  [PXUIField(DisplayName = "Execution Date", Enabled = false, Visible = false)]
  [PXDefault]
  public virtual System.DateTime? ExecutionDate { get; set; }

  [PXDependsOnFields(new System.Type[] {typeof (AUScheduleExecution.executionDate)})]
  [PXDate(InputMask = "g")]
  [PXUIField(DisplayName = "Execution Date", Enabled = false)]
  public virtual System.DateTime? ExecutionDateToDisplay
  {
    get => this.ExecutionDate;
    set
    {
    }
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Processed", Enabled = false)]
  public virtual int? ProcessedCount { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Warnings", Enabled = false)]
  public virtual int? WarningsCount { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Errors", Enabled = false)]
  public virtual int? ErrorsCount { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Total Records", Enabled = false)]
  public virtual int? TotalCount { get; set; }

  [PXDBTimestamp]
  public virtual byte[] TStamp { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public virtual string Status { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Execution Result", Enabled = false)]
  public virtual string Result { get; set; }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScheduleExecution.screenID>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScheduleExecution.scheduleID>
  {
  }

  public abstract class executionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUScheduleExecution.executionDate>
  {
  }

  public abstract class executionDateToDisplay : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUScheduleExecution.executionDateToDisplay>
  {
  }

  public abstract class processedCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUScheduleExecution.processedCount>
  {
  }

  public abstract class warningsCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUScheduleExecution.warningsCount>
  {
  }

  public abstract class errorsCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScheduleExecution.errorsCount>
  {
  }

  public abstract class totalCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScheduleExecution.totalCount>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUScheduleExecution.tStamp>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScheduleExecution.selected>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScheduleExecution.status>
  {
  }

  public abstract class result : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScheduleExecution.result>
  {
  }
}

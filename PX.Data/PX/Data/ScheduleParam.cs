// Decompiled with JetBrains decompiler
// Type: PX.Data.ScheduleParam
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Data;

/// <exclude />
[Serializable]
public class ScheduleParam : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _ShowLog;
  protected int? _ScheduleID;
  protected System.DateTime? _FromDateTime;
  protected System.DateTime? _ToDateTime;

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? ShowLog
  {
    get => this._ShowLog;
    set => this._ShowLog = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Schedule ID")]
  [PXSelector(typeof (Search<AUSchedule.scheduleID, Where<AUSchedule.screenID, Equal<Optional<AUSchedule.screenID>>>>), DescriptionField = typeof (AUSchedule.description))]
  public virtual int? ScheduleID
  {
    get => this._ScheduleID;
    set => this._ScheduleID = value;
  }

  [PXDBDate(PreserveTime = true, DisplayMask = "g", InputMask = "g")]
  [PXUIField(DisplayName = "From")]
  public virtual System.DateTime? FromDateTime
  {
    get => this._FromDateTime;
    set => this._FromDateTime = value;
  }

  [PXDBDate(PreserveTime = true, DisplayMask = "g", InputMask = "g")]
  [PXUIField(DisplayName = "To")]
  public virtual System.DateTime? ToDateTime
  {
    get => this._ToDateTime;
    set => this._ToDateTime = value;
  }

  /// <exclude />
  public abstract class showLog : BqlType<IBqlBool, bool>.Field<
  #nullable disable
  ScheduleParam.showLog>
  {
  }

  /// <exclude />
  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ScheduleParam.scheduleID>
  {
  }

  /// <exclude />
  public abstract class fromDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    ScheduleParam.fromDateTime>
  {
  }

  /// <exclude />
  public abstract class toDateTime : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  ScheduleParam.toDateTime>
  {
  }
}

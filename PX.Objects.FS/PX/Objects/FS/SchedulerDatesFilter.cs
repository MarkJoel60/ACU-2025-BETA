// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SchedulerDatesFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXHidden]
[Serializable]
public class SchedulerDatesFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDateAndTime(UseTimeZone = true)]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? DateSelected { get; set; }

  [PXBool]
  public virtual bool? FilterBusinessHours { get; set; }

  [PXInt]
  public virtual int? PeriodKind { get; set; }

  [PXDateAndTime(UseTimeZone = true)]
  public virtual DateTime? DateBegin { get; set; }

  [PXDateAndTime(UseTimeZone = true)]
  public virtual DateTime? DateEnd { get; set; }

  public abstract class dateSelected : 
    BqlType<IBqlDateTime, DateTime>.Field<
    #nullable disable
    SchedulerDatesFilter.dateSelected>
  {
  }

  public abstract class filterBusinessHours : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SchedulerDatesFilter.filterBusinessHours>
  {
  }

  public abstract class periodKind : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SchedulerDatesFilter.periodKind>
  {
  }

  public abstract class dateBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SchedulerDatesFilter.dateBegin>
  {
  }

  public abstract class dateEnd : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SchedulerDatesFilter.dateEnd>
  {
  }
}

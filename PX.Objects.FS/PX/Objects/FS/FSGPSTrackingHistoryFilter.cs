// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSGPSTrackingHistoryFilter
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
public class FSGPSTrackingHistoryFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDate(UseTimeZone = true)]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? Date { get; set; }

  [PXDate(UseTimeZone = true)]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate
  {
    get
    {
      if (!this.Date.HasValue)
        return this.Date;
      int year = this.Date.Value.Year;
      DateTime? date = this.Date;
      DateTime dateTime = date.Value;
      int month = dateTime.Month;
      date = this.Date;
      dateTime = date.Value;
      int day = dateTime.Day;
      return new DateTime?(new DateTime(year, month, day, 0, 0, 0));
    }
  }

  [PXDate(UseTimeZone = true)]
  [PXUIField(DisplayName = "End Date")]
  public virtual DateTime? EndDate
  {
    get
    {
      if (!this.Date.HasValue)
        return this.Date;
      int year = this.Date.Value.Year;
      DateTime? date = this.Date;
      DateTime dateTime = date.Value;
      int month = dateTime.Month;
      date = this.Date;
      dateTime = date.Value;
      int day = dateTime.Day;
      return new DateTime?(new DateTime(year, month, day, 23, 59, 59));
    }
  }

  public abstract class date : BqlType<IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSGPSTrackingHistoryFilter.date>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSGPSTrackingHistoryFilter.startDate>
  {
  }

  public abstract class endDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSGPSTrackingHistoryFilter.endDate>
  {
  }
}

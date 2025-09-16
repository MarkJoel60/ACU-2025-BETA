// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SchedulerInitData
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
public class SchedulerInitData : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  public virtual 
  #nullable disable
  string MapAPIKey { get; set; }

  [PXInt]
  public virtual int? GPSRefreshTrackingTime { get; set; }

  [PXBool]
  public virtual bool? EnableGPSTracking { get; set; }

  public abstract class mapAPIKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerInitData.mapAPIKey>
  {
  }

  public abstract class gPSRefreshTrackingTime : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerInitData.gPSRefreshTrackingTime>
  {
  }

  public abstract class enableGPSTracking : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SchedulerInitData.enableGPSTracking>
  {
  }
}

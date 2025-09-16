// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SchedulerAppointmentFilter
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
public class SchedulerAppointmentFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  public virtual int? AppointmentID { get; set; }

  /// <summary>
  /// Current appointment ID, that is the appointment that is highlighted in the UI
  /// </summary>
  [PXInt]
  public virtual int? SearchAppointmentID { get; set; }

  [PXInt]
  public virtual int? AssignableAppointmentID { get; set; }

  [PXInt]
  public virtual int? AssignableSOID { get; set; }

  [PXInt]
  public virtual int? AssignableSODetID { get; set; }

  public abstract class appointmentID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    SchedulerAppointmentFilter.appointmentID>
  {
  }

  public abstract class searchAppointmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerAppointmentFilter.searchAppointmentID>
  {
  }

  /// <summary>
  /// Passed from UI to the server for testing assignability from this appointment to other employees
  /// </summary>
  public abstract class assignableAppointmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerAppointmentFilter.assignableAppointmentID>
  {
  }

  /// <summary>
  /// Passed from UI to the server for testing assignability of this server order to employees
  /// </summary>
  public abstract class assignableSOID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerAppointmentFilter.assignableSOID>
  {
  }

  /// <summary>
  /// Passed from UI to the server for testing assignability of this service to employees
  /// </summary>
  public abstract class assignableSODetID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerAppointmentFilter.assignableSODetID>
  {
  }
}

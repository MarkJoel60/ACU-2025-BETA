// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.UpdateAppointmentFilter
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
public class UpdateAppointmentFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  public virtual int? AppointmentID { get; set; }

  [PXInt]
  public virtual int? NewResourceID { get; set; }

  [PXInt]
  public virtual int? OldResourceID { get; set; }

  [PXDateAndTime(UseTimeZone = true)]
  public virtual DateTime? NewBegin { get; set; }

  [PXDateAndTime(UseTimeZone = true)]
  public virtual DateTime? NewEnd { get; set; }

  [PXBool]
  public virtual bool? Confirmed { get; set; }

  [PXBool]
  public virtual bool? ValidatedByDispatcher { get; set; }

  public abstract class appointmentID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    UpdateAppointmentFilter.appointmentID>
  {
  }

  public abstract class newResourceID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    UpdateAppointmentFilter.newResourceID>
  {
  }

  public abstract class oldResourceID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    UpdateAppointmentFilter.oldResourceID>
  {
  }

  public abstract class newBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    UpdateAppointmentFilter.newBegin>
  {
  }

  public abstract class newEnd : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    UpdateAppointmentFilter.newEnd>
  {
  }

  public abstract class confirmed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UpdateAppointmentFilter.confirmed>
  {
  }

  public abstract class validatedByDispatcher : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    UpdateAppointmentFilter.validatedByDispatcher>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SchedulerAppointmentEmployee
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

/// <exclude />
[PXProjection(typeof (Select<FSAppointmentEmployee>))]
[PXHidden]
[Serializable]
public class SchedulerAppointmentEmployee : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlField = typeof (FSAppointmentEmployee.employeeID))]
  public virtual int? EmployeeID { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointmentEmployee.appointmentID))]
  public virtual int? AppointmentID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "", Enabled = false, Visible = false)]
  public virtual bool? IsFilteredOut { get; set; }

  public abstract class employeeID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    SchedulerAppointmentEmployee.employeeID>
  {
  }

  public abstract class appointmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerAppointmentEmployee.appointmentID>
  {
  }

  public abstract class isFilteredOut : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SchedulerAppointmentEmployee.isFilteredOut>
  {
  }
}

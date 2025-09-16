// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentStaffScheduleBoard
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXProjection(typeof (Select5<FSAppointmentEmployee, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentEmployee.appointmentID>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>, InnerJoin<FSContact, On<FSContact.contactID, Equal<FSServiceOrder.serviceOrderContactID>>, LeftJoin<FSWFStage, On<FSWFStage.wFStageID, Equal<FSAppointment.wFStageID>>>>>>>>, Aggregate<GroupBy<FSAppointmentEmployee.appointmentID, GroupBy<FSAppointmentEmployee.employeeID, GroupBy<FSAppointment.validatedByDispatcher, GroupBy<FSAppointment.confirmed>>>>>>))]
[Serializable]
public class FSAppointmentStaffScheduleBoard : FSAppointmentScheduleBoard
{
  public virtual 
  #nullable disable
  FSAppointmentStaffScheduleBoard Clone()
  {
    return (FSAppointmentStaffScheduleBoard) ((object) this).MemberwiseClone();
  }

  [PXDBInt(BqlField = typeof (FSAppointmentEmployee.employeeID))]
  public override int? EmployeeID { get; set; }

  public new abstract class employeeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentStaffScheduleBoard.employeeID>
  {
  }
}

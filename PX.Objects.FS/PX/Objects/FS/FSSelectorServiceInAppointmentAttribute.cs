// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorServiceInAppointmentAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorServiceInAppointmentAttribute : PXSelectorAttribute
{
  public FSSelectorServiceInAppointmentAttribute()
    : base(typeof (Search<FSAppointmentDet.lineRef, Where<FSAppointmentDet.appointmentID, Equal<Current<FSAppointment.appointmentID>>, And<FSAppointmentDet.lineType, Equal<FSLineType.Service>, And<Where<FSAppointmentDet.serviceType, Equal<ListField_Appointment_Service_Action_Type.Delivered_Items>, Or<FSAppointmentDet.serviceType, Equal<ListField_Appointment_Service_Action_Type.Picked_Up_Items>>>>>>>), new Type[5]
    {
      typeof (FSAppointmentDet.lineRef),
      typeof (FSAppointmentDet.lineType),
      typeof (FSAppointmentDet.status),
      typeof (FSAppointmentDet.inventoryID),
      typeof (FSAppointmentDet.tranDesc)
    })
  {
    this.SubstituteKey = typeof (FSAppointmentDet.lineRef);
    this.DescriptionField = typeof (FSAppointmentDet.inventoryID);
    this.DirtyRead = true;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorAppointmentPostingINAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

internal class FSSelectorAppointmentPostingINAttribute : PXSelectorAttribute
{
  public FSSelectorAppointmentPostingINAttribute()
    : base(typeof (Search5<FSAppointment.appointmentID, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSAppointment.srvOrdType>>, InnerJoin<FSAppointmentDet, On<FSAppointmentDet.appointmentID, Equal<FSAppointment.appointmentID>>>>, Where<FSAppointmentDet.lineType, Equal<ListField_LineType_Pickup_Delivery.Pickup_Delivery>, And<FSAppointment.closed, Equal<True>, And<FSAppointment.executionDate, LessEqual<Current<UpdateInventoryFilter.cutOffDate>>, And<FSSrvOrdType.enableINPosting, Equal<True>, And<Where<FSAppointment.routeDocumentID, Equal<Current<UpdateInventoryFilter.routeDocumentID>>, Or<Current<UpdateInventoryFilter.routeDocumentID>, IsNull>>>>>>>, Aggregate<GroupBy<FSAppointment.appointmentID>>>))
  {
    this.SubstituteKey = typeof (FSAppointment.refNbr);
  }
}

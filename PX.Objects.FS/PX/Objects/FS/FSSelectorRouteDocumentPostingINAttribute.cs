// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorRouteDocumentPostingINAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

internal class FSSelectorRouteDocumentPostingINAttribute : PXSelectorAttribute
{
  public FSSelectorRouteDocumentPostingINAttribute()
    : base(typeof (Search5<FSRouteDocument.routeDocumentID, InnerJoin<FSAppointment, On<FSAppointment.routeDocumentID, Equal<FSRouteDocument.routeDocumentID>>, InnerJoin<FSAppointmentDet, On<FSAppointment.appointmentID, Equal<FSAppointmentDet.appointmentID>>, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSAppointment.srvOrdType>>>>>, Where<FSAppointmentDet.lineType, Equal<ListField_LineType_Pickup_Delivery.Pickup_Delivery>>, Aggregate<GroupBy<FSRouteDocument.routeDocumentID>>>))
  {
    this.SubstituteKey = typeof (FSRouteDocument.refNbr);
  }
}

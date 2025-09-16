// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorSORefNbr_AppointmentAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorSORefNbr_AppointmentAttribute : PXSelectorAttribute
{
  public FSSelectorSORefNbr_AppointmentAttribute()
    : base(typeof (Search2<FSServiceOrder.refNbr, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where<FSServiceOrder.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>, And<Where2<Where<Current<FSAppointment.appointmentID>, Greater<Zero>, Or<FSServiceOrder.openDoc, Equal<True>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Where<PX.Objects.AR.Customer.bAccountID, IsNotNull, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>>>>, OrderBy<Desc<FSServiceOrder.refNbr>>>), new Type[11]
    {
      typeof (FSServiceOrder.refNbr),
      typeof (FSServiceOrder.srvOrdType),
      typeof (FSServiceOrder.customerID),
      typeof (FSServiceOrder.status),
      typeof (FSServiceOrder.priority),
      typeof (FSServiceOrder.severity),
      typeof (FSServiceOrder.orderDate),
      typeof (FSServiceOrder.sLAETA),
      typeof (FSServiceOrder.assignedEmpID),
      typeof (FSServiceOrder.sourceType),
      typeof (FSServiceOrder.sourceRefNbr)
    })
  {
    this.Filterable = true;
  }
}

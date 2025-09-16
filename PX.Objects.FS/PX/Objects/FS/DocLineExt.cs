// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.DocLineExt
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.FS;

public class DocLineExt
{
  public IDocLine docLine;
  public FSPostDoc fsPostDoc;
  public FSSrvOrdType fsSrvOrdType;
  public FSServiceOrder fsServiceOrder;
  public FSAppointment fsAppointment;
  public FSPostInfo fsPostInfo;
  public FSSODet fsSODet;
  public FSAppointmentDet fsAppointmentDet;
  public PMTask pmTask;

  public DocLineExt(
    PXResult<FSSODet, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask> soDetLine)
  {
    this.docLine = (IDocLine) PXResult<FSSODet, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask>.op_Implicit(soDetLine);
    this.fsPostDoc = PXResult<FSSODet, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask>.op_Implicit(soDetLine);
    this.fsServiceOrder = PXResult<FSSODet, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask>.op_Implicit(soDetLine);
    this.fsSrvOrdType = PXResult<FSSODet, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask>.op_Implicit(soDetLine);
    this.fsAppointment = (FSAppointment) null;
    this.fsPostInfo = PXResult<FSSODet, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask>.op_Implicit(soDetLine);
    this.fsSODet = PXResult<FSSODet, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask>.op_Implicit(soDetLine);
    this.fsAppointmentDet = (FSAppointmentDet) null;
    this.pmTask = PXResult<FSSODet, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask>.op_Implicit(soDetLine);
  }

  public DocLineExt(
    PXResult<FSSODet, FSServiceOrder, FSSrvOrdType, PMTask> soDetLine)
  {
    this.docLine = (IDocLine) PXResult<FSSODet, FSServiceOrder, FSSrvOrdType, PMTask>.op_Implicit(soDetLine);
    this.fsPostDoc = (FSPostDoc) null;
    this.fsServiceOrder = PXResult<FSSODet, FSServiceOrder, FSSrvOrdType, PMTask>.op_Implicit(soDetLine);
    this.fsSrvOrdType = PXResult<FSSODet, FSServiceOrder, FSSrvOrdType, PMTask>.op_Implicit(soDetLine);
    this.fsAppointment = (FSAppointment) null;
    this.fsPostInfo = (FSPostInfo) null;
    this.fsSODet = PXResult<FSSODet, FSServiceOrder, FSSrvOrdType, PMTask>.op_Implicit(soDetLine);
    this.fsAppointmentDet = (FSAppointmentDet) null;
    this.pmTask = PXResult<FSSODet, FSServiceOrder, FSSrvOrdType, PMTask>.op_Implicit(soDetLine);
  }

  public DocLineExt(
    PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, FSSODet, PMTask> appointmentDetLine)
  {
    this.docLine = (IDocLine) PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, FSSODet, PMTask>.op_Implicit(appointmentDetLine);
    this.fsPostDoc = PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, FSSODet, PMTask>.op_Implicit(appointmentDetLine);
    this.fsServiceOrder = PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, FSSODet, PMTask>.op_Implicit(appointmentDetLine);
    this.fsSrvOrdType = PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, FSSODet, PMTask>.op_Implicit(appointmentDetLine);
    this.fsAppointment = PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, FSSODet, PMTask>.op_Implicit(appointmentDetLine);
    this.fsPostInfo = PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, FSSODet, PMTask>.op_Implicit(appointmentDetLine);
    this.fsSODet = PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, FSSODet, PMTask>.op_Implicit(appointmentDetLine);
    this.fsAppointmentDet = PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, FSSODet, PMTask>.op_Implicit(appointmentDetLine);
    this.pmTask = PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, FSSODet, PMTask>.op_Implicit(appointmentDetLine);
  }

  public DocLineExt(
    PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask> appointmentDetLine)
  {
    this.docLine = (IDocLine) PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask>.op_Implicit(appointmentDetLine);
    this.fsPostDoc = PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask>.op_Implicit(appointmentDetLine);
    this.fsServiceOrder = PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask>.op_Implicit(appointmentDetLine);
    this.fsSrvOrdType = PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask>.op_Implicit(appointmentDetLine);
    this.fsAppointment = PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask>.op_Implicit(appointmentDetLine);
    this.fsPostInfo = PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask>.op_Implicit(appointmentDetLine);
    this.fsSODet = (FSSODet) null;
    this.fsAppointmentDet = PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask>.op_Implicit(appointmentDetLine);
    this.pmTask = PXResult<FSAppointmentDet, FSAppointment, FSServiceOrder, FSSrvOrdType, FSPostDoc, FSPostInfo, PMTask>.op_Implicit(appointmentDetLine);
  }
}

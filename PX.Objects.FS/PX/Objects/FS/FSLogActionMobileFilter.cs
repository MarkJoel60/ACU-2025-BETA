// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSLogActionMobileFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSLogActionMobileFilter : FSLogActionFilter
{
  [PXString(4, IsFixed = true, InputMask = ">AAAA")]
  [PXDefault(typeof (Coalesce<Search<FSxUserPreferences.dfltSrvOrdType, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>>>, Search<FSSetup.dfltSrvOrdType>>))]
  [PXUIField(DisplayName = "Service Order Type")]
  [FSSelectorSrvOrdTypeNOTQuote]
  [PXUIVerify]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Appointment Nbr.")]
  [PXSelector(typeof (Search2<FSAppointment.appointmentID, LeftJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>>, Where<FSAppointment.srvOrdType, Equal<Optional<FSLogActionMobileFilter.srvOrdType>>>, OrderBy<Desc<FSAppointment.refNbr>>>), new System.Type[] {typeof (FSAppointment.refNbr), typeof (FSAppointment.docDesc), typeof (FSAppointment.status), typeof (FSAppointment.scheduledDateTimeBegin)}, SubstituteKey = typeof (FSAppointment.refNbr))]
  public virtual int? AppointmentID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Service Order Nbr.")]
  [PXSelector(typeof (Search2<FSServiceOrder.sOID, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where<FSServiceOrder.srvOrdType, Equal<Current<FSLogActionMobileFilter.srvOrdType>>>, OrderBy<Desc<FSServiceOrder.refNbr>>>), SubstituteKey = typeof (FSServiceOrder.refNbr))]
  public virtual int? SOID { get; set; }

  [PXString(3, IsFixed = true)]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual string EmployeeLineRef { get; set; }

  [PXString(4, IsFixed = true)]
  [PXDefault]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<FSLogActionFilter.type, NotEqual<FSLogTypeAction.StaffAssignment>, And<FSLogActionFilter.type, NotEqual<FSLogTypeAction.SrvBasedOnAssignment>>>>))]
  [PXUIRequired(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<FSLogActionFilter.type, NotEqual<FSLogTypeAction.StaffAssignment>, And<FSLogActionFilter.type, NotEqual<FSLogTypeAction.SrvBasedOnAssignment>, And<FSLogActionFilter.type, NotEqual<FSLogTypeAction.Travel>>>>>))]
  [PXSelector(typeof (Search2<FSAppointmentDet.lineRef, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<FSAppointmentDet.inventoryID>>>, Where<FSAppointmentDet.appointmentID, Equal<Current<FSLogActionMobileFilter.appointmentID>>, And<FSAppointmentDet.lineType, Equal<FSLineType.Service>, And<FSAppointmentDet.lineRef, IsNotNull, And<FSxService.isTravelItem, Equal<Current<FSLogActionFilter.isTravelAction>>>>>>>), new System.Type[] {typeof (FSAppointmentDet.lineRef), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (FSAppointmentDet.tranDesc), typeof (FSAppointmentDet.estimatedDuration)}, DescriptionField = typeof (FSAppointmentDet.tranDesc))]
  [PXUIField(DisplayName = "Detail Ref. Nbr.")]
  public override string DetLineRef { get; set; }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSLogActionMobileFilter.srvOrdType>
  {
  }

  public abstract class appointmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLogActionMobileFilter.sOID>
  {
  }

  public abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLogActionMobileFilter.sOID>
  {
  }

  public abstract class employeeLineRef : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSLogActionMobileFilter.employeeLineRef>
  {
  }

  public new abstract class detLineRef : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSLogActionMobileFilter.detLineRef>
  {
  }
}

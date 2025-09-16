// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentStaffExtItemLine
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.EP;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXProjection(typeof (Select2<FSAppointmentEmployee, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<FSAppointmentEmployee.employeeID>>, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<FSAppointmentEmployee.employeeID>>, LeftJoin<FSAppointmentDet, On<FSAppointmentDet.lineRef, Equal<FSAppointmentEmployee.serviceLineRef>, And<FSAppointmentDet.appointmentID, Equal<FSAppointmentEmployee.appointmentID>>>>>>>))]
[Serializable]
public class FSAppointmentStaffExtItemLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(4, IsKey = true, IsFixed = true, BqlField = typeof (FSAppointmentEmployee.srvOrdType))]
  [PXUIField(DisplayName = "Service Order Type", Visible = false, Enabled = false)]
  [PXDefault(typeof (FSAppointment.srvOrdType))]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType>), CacheGlobal = true)]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "", BqlField = typeof (FSAppointmentEmployee.refNbr))]
  [PXUIField(DisplayName = "Appointment Nbr.", Visible = false, Enabled = false)]
  [PXDBDefault(typeof (FSAppointment.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (FSAppointmentStaffExtItemLine.FK.Appointment))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointmentEmployee.appointmentID))]
  [PXDBDefault(typeof (FSAppointment.appointmentID))]
  [PXUIField(DisplayName = "Appointment Ref. Nbr.", Visible = false, Enabled = false)]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<Current<FSLogActionFilter.type>, Equal<FSLogTypeAction.StaffAssignment>>>))]
  public virtual int? DocID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (FSAppointmentEmployee.lineNbr))]
  [PXLineNbr(typeof (FSAppointment))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointmentEmployee.employeeID))]
  [PXUIField(DisplayName = "Staff Member")]
  [FSSelector_StaffMember_ServiceOrderProjectID]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<Current<FSLogActionFilter.type>, Equal<FSLogTypeAction.StaffAssignment>, And<Current<FSLogActionFilter.me>, Equal<False>>>>))]
  public virtual int? BAccountID { get; set; }

  [PXDBString(3, IsFixed = true, BqlField = typeof (FSAppointmentEmployee.lineRef))]
  [PXUIField]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<Current<FSLogActionFilter.type>, Equal<FSLogTypeAction.StaffAssignment>>>))]
  public virtual string LineRef { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointmentDet.inventoryID))]
  [PXDefault]
  [PXSelector(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID>), SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXUIField(DisplayName = "Inventory ID", Enabled = false)]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<Current<FSLogActionFilter.type>, Equal<FSLogTypeAction.StaffAssignment>>>))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (FSAppointmentDet.tranDesc))]
  [PXDefault]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<Current<FSLogActionFilter.type>, Equal<FSLogTypeAction.StaffAssignment>>>))]
  public virtual string Descr { get; set; }

  [FSDBTimeSpanLong(BqlField = typeof (FSAppointmentDet.estimatedDuration))]
  [PXUIField(DisplayName = "Estimated Duration")]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<Current<FSLogActionFilter.type>, Equal<FSLogTypeAction.StaffAssignment>>>))]
  public virtual int? EstimatedDuration { get; set; }

  [PXDBGuid(false, BqlField = typeof (EPEmployee.userID))]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual Guid? UserID { get; set; }

  [PXDBString(4, IsFixed = true, BqlField = typeof (FSAppointmentDet.lineRef))]
  [PXUIField]
  public virtual string DetLineRef { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  [PXFormula(typeof (Switch<Case<Where<FSAppointmentStaffExtItemLine.userID, Equal<Current<AccessInfo.userID>>>, True>, False>))]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<Current<FSLogActionFilter.type>, Equal<FSLogTypeAction.StaffAssignment>>>))]
  public virtual bool? Selected { get; set; }

  [PXDBBool(BqlField = typeof (FSAppointmentDet.isTravelItem))]
  [PXUIField]
  public virtual bool? IsTravelItem { get; set; }

  public class PK : 
    PrimaryKeyOf<FSAppointmentStaffExtItemLine>.By<FSAppointmentStaffExtItemLine.srvOrdType, FSAppointmentStaffExtItemLine.refNbr, FSAppointmentStaffExtItemLine.lineNbr>
  {
    public static FSAppointmentStaffExtItemLine Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (FSAppointmentStaffExtItemLine) PrimaryKeyOf<FSAppointmentStaffExtItemLine>.By<FSAppointmentStaffExtItemLine.srvOrdType, FSAppointmentStaffExtItemLine.refNbr, FSAppointmentStaffExtItemLine.lineNbr>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSAppointmentStaffExtItemLine>.By<FSAppointmentStaffExtItemLine.srvOrdType>
    {
    }

    public class Appointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.ForeignKeyOf<FSAppointmentStaffExtItemLine>.By<FSAppointmentStaffExtItemLine.srvOrdType, FSAppointmentStaffExtItemLine.refNbr>
    {
    }

    public class Staff : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<FSAppointmentStaffExtItemLine>.By<FSAppointmentStaffExtItemLine.bAccountID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSAppointmentStaffExtItemLine>.By<FSAppointmentStaffExtItemLine.inventoryID>
    {
    }

    public class User : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<FSAppointmentStaffExtItemLine>.By<FSAppointmentStaffExtItemLine.userID>
    {
    }
  }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentStaffExtItemLine.srvOrdType>
  {
  }

  public abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentStaffExtItemLine.refNbr>
  {
  }

  public abstract class docID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentStaffExtItemLine.docID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentStaffExtItemLine.lineNbr>
  {
  }

  public abstract class bAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentStaffExtItemLine.bAccountID>
  {
  }

  public abstract class lineRef : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentStaffExtItemLine.lineRef>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentStaffExtItemLine.inventoryID>
  {
  }

  public abstract class descr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentStaffExtItemLine.descr>
  {
  }

  public abstract class estimatedDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentStaffExtItemLine.estimatedDuration>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSAppointmentStaffExtItemLine.userID>
  {
  }

  public abstract class detLineRef : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentStaffExtItemLine.detLineRef>
  {
  }

  public abstract class selected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentStaffExtItemLine.selected>
  {
  }

  public abstract class isTravelItem : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentStaffExtItemLine.isTravelItem>
  {
  }
}

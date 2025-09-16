// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentStaffDistinct
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

[PXProjection(typeof (Select5<FSAppointmentEmployee, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<FSAppointmentEmployee.employeeID>>, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<FSAppointmentEmployee.employeeID>>>>, Aggregate<GroupBy<FSAppointmentEmployee.srvOrdType, GroupBy<FSAppointmentEmployee.refNbr, GroupBy<FSAppointmentEmployee.employeeID>>>>>))]
[Serializable]
public class FSAppointmentStaffDistinct : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
  [PXParent(typeof (FSAppointmentStaffDistinct.FK.Appointment))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointmentEmployee.appointmentID))]
  [PXDBDefault(typeof (FSAppointment.appointmentID))]
  [PXUIField(DisplayName = "Appointment Ref. Nbr.", Visible = false, Enabled = false)]
  public virtual int? DocID { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointmentEmployee.employeeID), IsKey = true)]
  [PXUIField(DisplayName = "Staff Member", Enabled = false)]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<Current<FSLogActionFilter.type>, NotEqual<FSLogTypeAction.StaffAssignment>, And<Current<FSLogActionFilter.type>, NotEqual<FSLogTypeAction.SrvBasedOnAssignment>, And<Current<FSLogActionFilter.me>, Equal<False>>>>>))]
  [FSSelector_StaffMember_ServiceOrderProjectID]
  public virtual int? BAccountID { get; set; }

  [PXDBGuid(false, BqlField = typeof (EPEmployee.userID))]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual Guid? UserID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  [PXFormula(typeof (Switch<Case<Where<FSAppointmentStaffDistinct.userID, Equal<Current<AccessInfo.userID>>>, True>, False>))]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<Current<FSLogActionFilter.type>, NotEqual<FSLogTypeAction.StaffAssignment>, And<Current<FSLogActionFilter.type>, NotEqual<FSLogTypeAction.SrvBasedOnAssignment>, And<Current<FSLogActionFilter.me>, Equal<False>>>>>))]
  public virtual bool? Selected { get; set; }

  public class PK : 
    PrimaryKeyOf<FSAppointmentStaffDistinct>.By<FSAppointmentStaffDistinct.srvOrdType, FSAppointmentStaffDistinct.refNbr, FSAppointmentStaffDistinct.bAccountID>
  {
    public static FSAppointmentStaffDistinct Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? bAccountID,
      PKFindOptions options = 0)
    {
      return (FSAppointmentStaffDistinct) PrimaryKeyOf<FSAppointmentStaffDistinct>.By<FSAppointmentStaffDistinct.srvOrdType, FSAppointmentStaffDistinct.refNbr, FSAppointmentStaffDistinct.bAccountID>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) bAccountID, options);
    }
  }

  public static class FK
  {
    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSAppointmentStaffDistinct>.By<FSAppointmentStaffDistinct.srvOrdType>
    {
    }

    public class Appointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.ForeignKeyOf<FSAppointmentStaffDistinct>.By<FSAppointmentStaffDistinct.srvOrdType, FSAppointmentStaffDistinct.refNbr>
    {
    }

    public class Staff : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<FSAppointmentStaffDistinct>.By<FSAppointmentStaffDistinct.bAccountID>
    {
    }

    public class User : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<FSAppointmentStaffDistinct>.By<FSAppointmentStaffDistinct.userID>
    {
    }
  }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentStaffDistinct.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentStaffDistinct.refNbr>
  {
  }

  public abstract class docID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentStaffDistinct.docID>
  {
  }

  public abstract class bAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentStaffDistinct.bAccountID>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSAppointmentStaffDistinct.userID>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentStaffDistinct.selected>
  {
  }
}

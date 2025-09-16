// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentStaffMember
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.EP;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXProjection(typeof (Select2<BAccountStaffMember, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<BAccountStaffMember.bAccountID>, And<PX.Objects.AP.Vendor.vStatus, NotEqual<VendorStatus.inactive>>>, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<BAccountStaffMember.bAccountID>, And<EPEmployee.vStatus, NotEqual<VendorStatus.inactive>>>, LeftJoin<EPEmployeePosition, On<EPEmployeePosition.employeeID, Equal<EPEmployee.bAccountID>, And<EPEmployeePosition.isActive, Equal<True>>>>>>, Where2<Where<FSxVendor.sDEnabled, Equal<True>>, Or<Where<FSxEPEmployee.sDEnabled, Equal<True>>>>>))]
[Serializable]
public class FSAppointmentStaffMember : BAccountStaffMember
{
  [PXDBBool(BqlField = typeof (FSxVendor.sDEnabled))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Staff Member in Service Management")]
  public virtual bool? VendorSDEnabled { get; set; }

  [PXDBBool(BqlField = typeof (FSxEPEmployee.sDEnabled))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Staff Member in Service Management")]
  public virtual bool? EmployeeSDEnabled { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (EPEmployeePosition.positionID))]
  [PXDefault]
  [PXSelector(typeof (EPPosition.positionID), DescriptionField = typeof (EPPosition.description))]
  [PXUIField]
  public virtual 
  #nullable disable
  string PositionID { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (EPEmployee.calendarID))]
  [PXUIField]
  [PXSelector(typeof (Search<CSCalendar.calendarID>), DescriptionField = typeof (CSCalendar.description))]
  public virtual string CalendarID { get; set; }

  public new abstract class bAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentStaffMember.bAccountID>
  {
  }

  public new abstract class acctCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentStaffMember.acctCD>
  {
  }

  public abstract class vendorSDEnabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentStaffMember.vendorSDEnabled>
  {
  }

  public abstract class employeeSDEnabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentStaffMember.employeeSDEnabled>
  {
  }

  public abstract class positionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentStaffMember.positionID>
  {
  }

  public abstract class calendarID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentStaffMember.calendarID>
  {
  }
}

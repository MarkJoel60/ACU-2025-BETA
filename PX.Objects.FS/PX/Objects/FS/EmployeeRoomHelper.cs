// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.EmployeeRoomHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class EmployeeRoomHelper : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt(IsKey = true)]
  [PXUIField(DisplayName = "Distance")]
  public virtual int? Distance { get; set; }

  [PXInt(IsKey = true)]
  [FSSelector_StaffMember_All(typeof (BAccountSelectorBase.acctName))]
  [PXUIField(DisplayName = "Staff Member ID")]
  public virtual int? EmployeeID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Branch Location ID")]
  [PXSelector(typeof (FSBranchLocation.branchLocationID), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  public virtual int? BranchLocationID { get; set; }

  [PXDate(IsKey = true)]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? DateStart { get; set; }

  [PXDateAndTime(UseTimeZone = true, DisplayMask = "t", InputMask = "t", IsKey = true)]
  [PXUIField(DisplayName = "Start Time")]
  public virtual DateTime? TimeStart { get; set; }

  [PXInt(IsKey = true)]
  [PXUIField(DisplayName = "Record ID")]
  public virtual int? RecordID { get; set; }

  [PXString(60)]
  [PXUIField(DisplayName = "Room Description")]
  public virtual 
  #nullable disable
  string Descr { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Exclusive")]
  public virtual bool? SpecificUse { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Employee Name")]
  public virtual string EmployeeName { get; set; }

  [PXString(10)]
  [PXUIField(DisplayName = "Room")]
  [PXSelector(typeof (FSRoom.roomID))]
  public virtual string RoomID { get; set; }

  [PXDateAndTime(UseTimeZone = true, DisplayMask = "t", InputMask = "t")]
  [PXUIField(DisplayName = "End Time")]
  public virtual DateTime? TimeEnd { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Fri")]
  public virtual bool? ValidOnFriday { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Mon")]
  public virtual bool? ValidOnMonday { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Sat")]
  public virtual bool? ValidOnSaturday { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Sun")]
  public virtual bool? ValidOnSunday { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Thr")]
  public virtual bool? ValidOnThursday { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Tue")]
  public virtual bool? ValidOnTuesday { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Wed")]
  public virtual bool? ValidOnWednesday { get; set; }

  [PXInt]
  public virtual int? ServiceEstimatedDuration { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Distance")]
  public virtual string DistanceInMiles { get; set; }

  public abstract class distance : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EmployeeRoomHelper.distance>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EmployeeRoomHelper.employeeID>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EmployeeRoomHelper.branchLocationID>
  {
  }

  public abstract class dateStart : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EmployeeRoomHelper.dateStart>
  {
  }

  public abstract class timeStart : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EmployeeRoomHelper.timeStart>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EmployeeRoomHelper.recordID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EmployeeRoomHelper.descr>
  {
  }

  public abstract class specificUse : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EmployeeRoomHelper.specificUse>
  {
  }

  public abstract class employeeName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EmployeeRoomHelper.employeeName>
  {
  }

  public abstract class roomID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EmployeeRoomHelper.roomID>
  {
  }

  public abstract class timeEnd : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EmployeeRoomHelper.timeEnd>
  {
  }

  public abstract class validOnFriday : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EmployeeRoomHelper.validOnFriday>
  {
  }

  public abstract class validOnMonday : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EmployeeRoomHelper.validOnMonday>
  {
  }

  public abstract class validOnSaturday : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EmployeeRoomHelper.validOnSaturday>
  {
  }

  public abstract class validOnSunday : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EmployeeRoomHelper.validOnSunday>
  {
  }

  public abstract class validOnThursday : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EmployeeRoomHelper.validOnThursday>
  {
  }

  public abstract class validOnTuesday : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EmployeeRoomHelper.validOnTuesday>
  {
  }

  public abstract class validOnWednesday : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EmployeeRoomHelper.validOnWednesday>
  {
  }

  public abstract class serviceEstimatedDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EmployeeRoomHelper.serviceEstimatedDuration>
  {
  }

  public abstract class distanceInMiles : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EmployeeRoomHelper.distanceInMiles>
  {
  }
}

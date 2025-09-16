// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSStaffSchedule
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXPrimaryGraph(typeof (StaffContractScheduleEntry))]
[Serializable]
public class FSStaffSchedule : FSSchedule
{
  [PXDBInt]
  public override int? CustomerID { get; set; }

  [PXDBString(4, IsFixed = true)]
  public override 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public override int? BranchID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Branch Location ID")]
  [FSSelectorBranchLocationByFSSchedule]
  public override int? BranchLocationID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = false)]
  [PXUIField]
  public virtual string StaffScheduleDescription { get; set; }

  [PXDBInt]
  [PXDefault]
  [FSSelector_Employee_All]
  [PXUIField(DisplayName = "Staff Member")]
  public override int? EmployeeID { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Start Date", DisplayNameTime = "Start Time")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public override DateTime? StartDate { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "End Date", DisplayNameTime = "End Time")]
  [PXDefault]
  [PXUIField]
  public override DateTime? EndDate { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [AutoNumber(typeof (Search<FSSetup.empSchdlNumberingID>), typeof (AccessInfo.businessDate))]
  [PXSelector(typeof (Search<FSSchedule.refNbr, Where<FSSchedule.entityType, Equal<ListField_Schedule_EntityType.Employee>>, OrderBy<Desc<FSSchedule.refNbr>>>), new Type[] {typeof (FSSchedule.refNbr), typeof (FSSchedule.employeeID), typeof (FSSchedule.branchID), typeof (FSSchedule.scheduleType), typeof (FSSchedule.startDate), typeof (FSSchedule.endDate), typeof (FSStaffSchedule.mem_StartTime_Time), typeof (FSStaffSchedule.mem_EndTime_Time)})]
  public override string RefNbr { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("E")]
  [ListField_Schedule_EntityType.ListAtrribute]
  [PXUIField(DisplayName = "Entity Type", Enabled = false)]
  public override string EntityType { get; set; }

  [ListField_ScheduleType.ListAtrribute]
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Schedule Type")]
  [PXDefault("A")]
  public override string ScheduleType { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameTime = "Start Time", InputMask = "t")]
  [PXUIField(DisplayName = "Start Time")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? StartTime { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameTime = "End Time", InputMask = "t")]
  [PXUIField(DisplayName = "End Time")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? EndTime { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Start Time")]
  public virtual string Mem_StartTime_Time => SharedFunctions.GetTimeStringFromDate(this.StartTime);

  [PXString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "End Time")]
  public virtual string Mem_EndTime_Time => SharedFunctions.GetTimeStringFromDate(this.EndTime);

  public new class PK : PrimaryKeyOf<FSStaffSchedule>.By<FSSchedule.refNbr>
  {
    public static FSStaffSchedule Find(PXGraph graph, string refNbr, PKFindOptions options = 0)
    {
      return (FSStaffSchedule) PrimaryKeyOf<FSStaffSchedule>.By<FSSchedule.refNbr>.FindBy(graph, (object) refNbr, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSStaffSchedule>.By<FSSchedule.branchID>
    {
    }

    public class BranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.ForeignKeyOf<FSStaffSchedule>.By<FSSchedule.branchLocationID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSStaffSchedule>.By<FSSchedule.customerID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<FSStaffSchedule>.By<FSSchedule.vendorID>
    {
    }

    public class VehicleType : 
      PrimaryKeyOf<FSVehicleType>.By<FSVehicleType.vehicleTypeID>.ForeignKeyOf<FSStaffSchedule>.By<FSSchedule.vehicleTypeID>
    {
    }

    public class Employee : 
      PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>.ForeignKeyOf<FSStaffSchedule>.By<FSSchedule.employeeID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<FSStaffSchedule>.By<FSSchedule.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<FSStaffSchedule>.By<FSSchedule.projectID, FSSchedule.dfltProjectTaskID>
    {
    }
  }

  public abstract class staffScheduleDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSStaffSchedule.staffScheduleDescription>
  {
  }

  public abstract class startTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSStaffSchedule.startTime>
  {
  }

  public abstract class endTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSStaffSchedule.endTime>
  {
  }

  public abstract class mem_StartTime_Time : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSStaffSchedule.mem_StartTime_Time>
  {
  }

  public abstract class mem_EndTime_Time : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSStaffSchedule.mem_EndTime_Time>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxEquipment
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;

#nullable enable
namespace PX.Objects.FS;

public class FSxEquipment : PXCacheExtension<
#nullable disable
EPEquipment>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDBInt]
  [PXUIField(DisplayName = "Branch location ID", Enabled = false)]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<FSServiceOrder.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  [PXFormula(typeof (Default<FSxEquipment.branchID>))]
  [PXDefault]
  public virtual int? BranchLocationID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Staff Member ID", Enabled = false)]
  [PXDefault]
  [FSSelector_StaffMember_All(null)]
  public virtual int? EmployeeID { get; set; }

  [PXDBString(10)]
  [PXUIField(DisplayName = "Room", Enabled = false)]
  [PXSelector(typeof (Search<FSRoom.roomID, Where<FSRoom.branchLocationID, Equal<Current<FSxEquipment.branchLocationID>>>>), DescriptionField = typeof (FSRoom.descr))]
  [PXFormula(typeof (Default<FSxEquipment.branchLocationID>))]
  [PXDefault]
  public virtual string RoomID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Warehouse ID", Enabled = false)]
  [PXSelector(typeof (INSite.siteID), SubstituteKey = typeof (INSite.siteCD))]
  [PXDefault]
  public virtual int? SiteID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show in Calendars and Route Maps")]
  public virtual bool? AllowSchedule { get; set; }

  [PXDBInt]
  [PXDefault(typeof (AccessInfo.branchID))]
  [PXUIField(DisplayName = "Branch ID", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.GL.Branch.branchID>), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? BranchID { get; set; }

  [PXString]
  [PXUIField(Visible = false)]
  [PXDefault]
  public virtual string VehicleDescr { get; set; }

  public abstract class branchLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxEquipment.branchLocationID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxEquipment.employeeID>
  {
  }

  public abstract class roomID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSxEquipment.roomID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxEquipment.siteID>
  {
  }

  public abstract class allowSchedule : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSxEquipment.allowSchedule>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxEquipment.branchID>
  {
  }

  public abstract class vehicleDescr : IBqlField, IBqlOperand
  {
  }
}

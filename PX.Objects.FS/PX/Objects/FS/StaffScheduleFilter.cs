// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.StaffScheduleFilter
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
public class StaffScheduleFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [PXUIField(DisplayName = "Employee Name")]
  [FSSelector_Employee_All]
  public virtual int? BAccountID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? BranchID { get; set; }

  [PXInt]
  [PXDefault(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<ServiceContractFilter.branchID>>>>))]
  [PXUIField(DisplayName = "Branch Location")]
  [FSSelectorBranchLocation]
  public virtual int? BranchLocationID { get; set; }

  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? ToDate { get; set; }

  [PXInt]
  public virtual int? ScheduleID { get; set; }

  public abstract class bAccountID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  StaffScheduleFilter.bAccountID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StaffScheduleFilter.branchID>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    StaffScheduleFilter.branchLocationID>
  {
  }

  public abstract class toDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  StaffScheduleFilter.toDate>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StaffScheduleFilter.scheduleID>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSCreateServiceOrderFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSCreateServiceOrderFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(4, IsFixed = true, InputMask = ">AAAA")]
  [PXUIField(DisplayName = "Service Order Type", Required = true)]
  [PXDefault]
  [FSSelectorActiveSrvOrdType]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXInt]
  [PXDefault(typeof (Search<FSxUserPreferences.dfltBranchLocationID, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>, And<UserPreferences.defBranchID, Equal<Current<FSServiceOrder.branchID>>>>>))]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<AccessInfo.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  [PXUIField(DisplayName = "Branch Location", Required = true)]
  public virtual int? BranchLocationID { get; set; }

  [PXInt]
  [FSSelector_StaffMember_All(null)]
  [PXUIField(DisplayName = "Assigned To")]
  public virtual int? AssignedEmpID { get; set; }

  [PXDateAndTime(UseTimeZone = true)]
  [PXUIField(DisplayName = "Deadline - SLA")]
  public virtual DateTime? SLAETA { get; set; }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCreateServiceOrderFilter.srvOrdType>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSCreateServiceOrderFilter.branchLocationID>
  {
  }

  public abstract class assignedEmpID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSCreateServiceOrderFilter.assignedEmpID>
  {
  }

  public abstract class sLAETA : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSCreateServiceOrderFilter.sLAETA>
  {
  }
}

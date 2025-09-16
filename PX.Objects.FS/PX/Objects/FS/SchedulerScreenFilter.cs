// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SchedulerScreenFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

/// <exclude />
[Serializable]
public class SchedulerScreenFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Organization(false, Required = false)]
  public int? OrganizationID { get; set; }

  [BranchOfOrganization(typeof (SchedulerScreenFilter.organizationID), true, null, null)]
  public int? BranchID { get; set; }

  [OrganizationTree(typeof (SchedulerScreenFilter.organizationID), typeof (SchedulerScreenFilter.branchID), null, true)]
  public int? OrgBAccountID { get; set; }

  [PXUnboundDefault(typeof (Search<FSxUserPreferences.dfltBranchLocationID, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>, And<UserPreferences.defBranchID, Equal<Current<SchedulerScreenFilter.branchID>>>>>))]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<SchedulerScreenFilter.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  [PXFormula(typeof (Default<SchedulerScreenFilter.branchID>))]
  public int? BranchLocationID { get; set; }

  [PXInt]
  public int? BranchLocationCount { get; set; }

  public abstract class organizationID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    SchedulerScreenFilter.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SchedulerScreenFilter.branchID>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerScreenFilter.branchLocationID>
  {
  }

  public abstract class branchLocationCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerScreenFilter.branchLocationCount>
  {
  }
}

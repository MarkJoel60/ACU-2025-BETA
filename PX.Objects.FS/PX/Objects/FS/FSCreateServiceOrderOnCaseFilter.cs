// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSCreateServiceOrderOnCaseFilter
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
public class FSCreateServiceOrderOnCaseFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [FSSelector_StaffMember_All(null)]
  [PXUIField(DisplayName = "Assigned To")]
  public virtual int? AssignedEmpID { get; set; }

  [PXString(4, IsFixed = true, InputMask = ">AAAA")]
  [PXUIField(DisplayName = "Service Order Type", Required = true)]
  [PXDefault(typeof (Coalesce<Search<FSxUserPreferences.dfltSrvOrdType, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>>>, Search<FSSetup.dfltCasesSrvOrdType>>))]
  [FSSelectorActiveSrvOrdType]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Problem")]
  [PXSelector(typeof (Search2<FSProblem.problemID, InnerJoin<FSSrvOrdTypeProblem, On<FSSrvOrdTypeProblem.problemID, Equal<FSProblem.problemID>>>, Where<FSSrvOrdTypeProblem.srvOrdType, Equal<Current<FSCreateServiceOrderOnCaseFilter.srvOrdType>>>>), SubstituteKey = typeof (FSProblem.problemCD), DescriptionField = typeof (FSProblem.descr))]
  public virtual int? ProblemID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Branch Location", Required = true)]
  [PXDefault(typeof (Search<FSxUserPreferences.dfltBranchLocationID, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>, And<UserPreferences.defBranchID, Equal<Current<FSServiceOrder.branchID>>>>>))]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<AccessInfo.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  public virtual int? BranchLocationID { get; set; }

  public abstract class assignedEmpID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSCreateServiceOrderOnCaseFilter.assignedEmpID>
  {
  }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCreateServiceOrderOnCaseFilter.srvOrdType>
  {
  }

  public abstract class problemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSCreateServiceOrderOnCaseFilter.problemID>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSCreateServiceOrderOnCaseFilter.branchLocationID>
  {
  }
}

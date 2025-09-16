// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxCRCase
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.FS;

public class FSxCRCase : PXCacheExtension<
#nullable disable
CRCase>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDBInt]
  [FSSelector_StaffMember_All(null)]
  [PXUIField(DisplayName = "Assigned To")]
  [PXDefault]
  public virtual int? AssignedEmpID { get; set; }

  [PXDBString(4, IsFixed = true, InputMask = ">AAAA")]
  [PXUIField(DisplayName = "Service Order Type", Enabled = false, Required = true)]
  [PXDefault]
  [FSSelectorActiveSrvOrdType]
  public virtual string SrvOrdType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Service Order Nbr.", Enabled = false)]
  [PXDefault]
  [PXSelector(typeof (Search<FSServiceOrder.refNbr, Where<FSServiceOrder.srvOrdType, Equal<Current<FSxCRCase.srvOrdType>>>>))]
  public virtual string ServiceOrderRefNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Problem", Enabled = false)]
  [PXDefault]
  [PXSelector(typeof (Search2<FSProblem.problemID, InnerJoin<FSSrvOrdTypeProblem, On<FSSrvOrdTypeProblem.problemID, Equal<FSProblem.problemID>>>, Where<FSSrvOrdTypeProblem.srvOrdType, Equal<Current<FSxCRCase.srvOrdType>>>>), SubstituteKey = typeof (FSProblem.problemCD), DescriptionField = typeof (FSProblem.descr))]
  public virtual int? ProblemID { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Branch Location", Required = true)]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<AccessInfo.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  public virtual int? BranchLocationID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Create Service Order")]
  public virtual bool? SDEnabled { get; set; }

  public abstract class assignedEmpID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxCRCase.assignedEmpID>
  {
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSxCRCase.srvOrdType>
  {
  }

  public abstract class serviceOrderRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxCRCase.serviceOrderRefNbr>
  {
  }

  public abstract class problemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxCRCase.problemID>
  {
  }

  public abstract class branchLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxCRCase.branchLocationID>
  {
  }

  public abstract class sDEnabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSxCRCase.sDEnabled>
  {
  }
}

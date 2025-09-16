// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LocationVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.GL;

#nullable disable
namespace PX.Objects.CR;

public sealed class LocationVisibilityRestriction : PXCacheExtension<Location>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [RestrictCustomerByUserBranches]
  public int? BAccountID { get; set; }

  [PXMergeAttributes]
  [Branch(null, typeof (Search2<PX.Objects.GL.Branch.branchID, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<Current<BAccountR.bAccountID>>>>>, Where<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>), true, true, false)]
  [PXRestrictor(typeof (Where<PX.Objects.GL.Branch.branchID, Inside<Current<BAccountR.cOrgBAccountID>>>), "The usage of the {0} customer is restricted in the {1} branch.", new System.Type[] {typeof (BAccountR.acctCD), typeof (PX.Objects.GL.Branch.branchCD)})]
  [PXDefault(typeof (Search2<PX.Objects.GL.Branch.branchID, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<Current<Location.bAccountID>>, And<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.cOrgBAccountID>>>>>))]
  public int? CBranchID { get; set; }

  [PXMergeAttributes]
  [Branch(null, typeof (Search2<PX.Objects.GL.Branch.branchID, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<Current<BAccountR.bAccountID>>>>>, Where<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>), true, true, false)]
  [PXRestrictor(typeof (Where<PX.Objects.GL.Branch.branchID, Inside<Current<BAccountR.vOrgBAccountID>>>), "The usage of the {0} vendor is restricted in the {1} branch.", new System.Type[] {typeof (BAccountR.acctCD), typeof (PX.Objects.GL.Branch.branchCD)})]
  [PXDefault(typeof (Search2<PX.Objects.GL.Branch.branchID, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<Current<Location.bAccountID>>, And<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.vOrgBAccountID>>>>>))]
  public int? VBranchID { get; set; }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Helpers.BranchHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.Helpers;

public sealed class BranchHelper
{
  /// <summary>
  /// The function returns BranchIDs of documents which can be applied if feature Inter-Branch Transactions is off.
  /// </summary>
  /// <param name="graph">PXGraph</param>
  /// <param name="branchID">BranchID of a document</param>
  /// <returns>BranchIDs of applicable documents. Returns NULL if the function is not applicable</returns>
  public static int[] GetBranchesToApplyDocuments(PXGraph graph, int? branchID)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.interBranch>() || !branchID.HasValue)
      return (int[]) null;
    int? organizationId = (int?) BranchMaint.FindBranchByID(graph, branchID)?.OrganizationID;
    PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID(graph, organizationId);
    if (organizationById != null && !(organizationById?.OrganizationType == "Balancing"))
      return BranchMaint.GetChildBranches(graph, organizationId).Select<PX.Objects.GL.Branch, int>((Func<PX.Objects.GL.Branch, int>) (x => x.BranchID.Value)).ToArray<int>();
    return new int[1]{ branchID.Value };
  }
}

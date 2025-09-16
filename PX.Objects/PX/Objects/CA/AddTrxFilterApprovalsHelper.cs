// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.AddTrxFilterApprovalsHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.EP;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public static class AddTrxFilterApprovalsHelper
{
  public static bool IsApprovalRequired(PXGraph graph)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() & AddTrxFilterApprovalsHelper.AssignedMapsExist(graph);
  }

  private static bool AssignedMapsExist(PXGraph graph)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && ((IQueryable<PXResult<CASetupApproval>>) PXSelectBase<CASetupApproval, PXViewOf<CASetupApproval>.BasedOn<SelectFromBase<CASetupApproval, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<EPAssignmentMap>.On<BqlOperand<CASetupApproval.assignmentMapID, IBqlInt>.IsEqual<EPAssignmentMap.assignmentMapID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CASetupApproval.isActive, Equal<True>>>>>.And<BqlOperand<EPAssignmentMap.entityType, IBqlString>.IsEqual<AssignmentMapType.AssignmentMapTypeCashTransaction>>>>.Config>.Select(graph, Array.Empty<object>())).Any<PXResult<CASetupApproval>>();
  }
}

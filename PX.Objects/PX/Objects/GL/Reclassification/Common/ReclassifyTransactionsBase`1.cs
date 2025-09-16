// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Reclassification.Common.ReclassifyTransactionsBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL.Reclassification.Common;

public abstract class ReclassifyTransactionsBase<TGraph> : PXGraph<TGraph> where TGraph : PXGraph
{
  public PXFilter<ReclassGraphState> StateView;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;

  protected ReclassGraphState State
  {
    get => ((PXSelectBase<ReclassGraphState>) this.StateView).Current;
    set => ((PXSelectBase<ReclassGraphState>) this.StateView).Current = value;
  }

  protected ReclassifyTransactionsBase()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
  }

  public static bool IsReclassAttrChanged(GLTranForReclassification tranForReclass)
  {
    int? newBranchId = tranForReclass.NewBranchID;
    int? branchId = tranForReclass.BranchID;
    if (newBranchId.GetValueOrDefault() == branchId.GetValueOrDefault() & newBranchId.HasValue == branchId.HasValue)
    {
      int? newAccountId = tranForReclass.NewAccountID;
      int? accountId = tranForReclass.AccountID;
      if (newAccountId.GetValueOrDefault() == accountId.GetValueOrDefault() & newAccountId.HasValue == accountId.HasValue)
      {
        int? newSubId = tranForReclass.NewSubID;
        int? subId = tranForReclass.SubID;
        if (newSubId.GetValueOrDefault() == subId.GetValueOrDefault() & newSubId.HasValue == subId.HasValue)
          return ReclassifyTransactionsBase<TGraph>.IsReclassProjectAttrChanged(tranForReclass);
      }
    }
    return true;
  }

  public static bool IsReclassProjectAttrChanged(GLTranForReclassification tranForReclass)
  {
    int? newProjectId = tranForReclass.NewProjectID;
    int? projectId = tranForReclass.ProjectID;
    if (newProjectId.GetValueOrDefault() == projectId.GetValueOrDefault() & newProjectId.HasValue == projectId.HasValue)
    {
      int? newTaskId = tranForReclass.NewTaskID;
      int? taskId = tranForReclass.TaskID;
      if (newTaskId.GetValueOrDefault() == taskId.GetValueOrDefault() & newTaskId.HasValue == taskId.HasValue)
      {
        int? newCostCodeId = tranForReclass.NewCostCodeID;
        int? costCodeId = tranForReclass.CostCodeID;
        if (newCostCodeId.GetValueOrDefault() == costCodeId.GetValueOrDefault() & newCostCodeId.HasValue == costCodeId.HasValue || !PXAccess.FeatureInstalled<FeaturesSet.costCodes>())
          return false;
      }
    }
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();
  }

  public GLTranForReclassification GetGLTranForReclassByKey(GLTranKey key)
  {
    GLTranForReclassification tranForReclassByKey = new GLTranForReclassification();
    tranForReclassByKey.Module = key.Module;
    tranForReclassByKey.BatchNbr = key.BatchNbr;
    tranForReclassByKey.LineNbr = key.LineNbr;
    return tranForReclassByKey;
  }

  protected virtual IEnumerable<GLTran> GetTransReclassTypeSorted(
    PXGraph graph,
    string module,
    string batchNbr)
  {
    return GraphHelper.RowCast<GLTran>((IEnumerable) PXSelectBase<GLTran, PXSelect<GLTran, Where<GLTran.module, Equal<Required<GLTran.module>>, And<GLTran.batchNbr, Equal<Required<GLTran.batchNbr>>>>, OrderBy<Desc<GLTran.reclassType>>>.Config>.Select(graph, new object[2]
    {
      (object) module,
      (object) batchNbr
    }));
  }
}

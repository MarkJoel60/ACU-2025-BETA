// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMTaskValidator`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable disable
namespace PX.Objects.PM;

public abstract class PMTaskValidator<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public virtual void VerifyCostCodeActive(PMTask task)
  {
    if (task == null)
      return;
    if (PXResultset<PMBudget>.op_Implicit(PXSelectBase<PMBudget, PXViewOf<PMBudget>.BasedOn<SelectFromBase<PMBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMCostCode>.On<BqlOperand<PMBudget.costCodeID, IBqlInt>.IsEqual<PMCostCode.costCodeID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMBudget.projectID, Equal<P.AsInt>>>>, And<BqlOperand<PMBudget.projectTaskID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<PMCostCode.isActive, IBqlBool>.IsNotEqual<True>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[2]
    {
      (object) task.ProjectID,
      (object) task.TaskID
    })) == null)
      return;
    Exception validationException = this.GetCostCodeValidationException(task);
    if (validationException != null)
      throw validationException;
  }

  public abstract Exception GetCostCodeValidationException(PMTask task);
}

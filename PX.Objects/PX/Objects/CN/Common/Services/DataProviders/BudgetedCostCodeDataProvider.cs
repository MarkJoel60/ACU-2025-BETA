// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Services.DataProviders.BudgetedCostCodeDataProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.PM;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CN.Common.Services.DataProviders;

public class BudgetedCostCodeDataProvider
{
  public static IEnumerable<PMBudgetedCostCode> GetBudgetedCostCodes(
    PXGraph graph,
    int? projectId,
    int? projectTaskId)
  {
    return PXSelectBase<PMBudgetedCostCode, PXViewOf<PMBudgetedCostCode>.BasedOn<SelectFromBase<PMBudgetedCostCode, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMBudgetedCostCode.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<PMBudgetedCostCode.projectTaskID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select(graph, new object[2]
    {
      (object) projectId,
      (object) projectTaskId
    }).FirstTableItems;
  }
}

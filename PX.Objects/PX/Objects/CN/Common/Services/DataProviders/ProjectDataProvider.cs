// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Services.DataProviders.ProjectDataProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CT;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CN.Common.Services.DataProviders;

public class ProjectDataProvider : IProjectDataProvider
{
  public PMProject GetProject(PXGraph graph, int? projectId)
  {
    return graph.Select<PMProject>().SingleOrDefault<PMProject>((Expression<Func<PMProject, bool>>) (p => p.ContractID == projectId));
  }

  public IEnumerable<PMProject> GetProjects(PXGraph graph)
  {
    return PXSelectBase<PMProject, PXViewOf<PMProject>.BasedOn<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>>.And<BqlOperand<PMProject.baseType, IBqlString>.IsEqual<CTPRType.project>>>>.Config>.Select(graph, Array.Empty<object>()).FirstTableItems;
  }

  public bool IsActiveProject(PXGraph graph, int? projectId)
  {
    PMProject project = this.GetProject(graph, projectId);
    return project != null && EnumerableExtensions.IsIn<string>(project.Status, "A", "D");
  }
}

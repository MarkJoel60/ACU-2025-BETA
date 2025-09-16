// Decompiled with JetBrains decompiler
// Type: PX.Api.PXDependToCacheAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Api;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = false)]
public class PXDependToCacheAttribute : PXViewExtensionAttribute
{
  public System.Type[] ViewTypes { get; }

  public PXDependToCacheAttribute(params System.Type[] viewTypes) => this.ViewTypes = viewTypes;

  public override void ViewCreated(PXGraph graph, string viewName)
  {
    graph.Views[viewName].SetDependToCacheTypes((IEnumerable<System.Type>) this.ViewTypes);
  }

  public static void AddDependencies(PXView view, System.Type[] viewTypes)
  {
    view.SetDependToCacheTypes(view.GetDependToCacheTypes() ?? Enumerable.Empty<System.Type>().Concat<System.Type>((IEnumerable<System.Type>) viewTypes));
  }
}

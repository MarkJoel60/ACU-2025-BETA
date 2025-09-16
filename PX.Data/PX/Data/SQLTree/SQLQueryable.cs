// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLQueryable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Remotion.Linq.Parsing.ExpressionVisitors.Transformation;
using Remotion.Linq.Parsing.Structure;
using Remotion.Linq.Parsing.Structure.NodeTypeProviders;
using System;
using System.Linq;

#nullable disable
namespace PX.Data.SQLTree;

internal static class SQLQueryable
{
  internal static Lazy<CompoundNodeTypeProvider> DefaultNodeTypeProvider = new Lazy<CompoundNodeTypeProvider>(new Func<CompoundNodeTypeProvider>(ExpressionTreeParser.CreateDefaultNodeTypeProvider));
  internal static Lazy<ExpressionTransformerRegistry> DefaultTransformerRegistry = new Lazy<ExpressionTransformerRegistry>(new Func<ExpressionTransformerRegistry>(ExpressionTransformerRegistry.CreateDefault));

  public static IQueryable<T> Create<T>() where T : IBqlTable
  {
    return (IQueryable<T>) new SQLQueryable<T>();
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Async.MethodRegisterHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Access.ActiveDirectory;
using PX.Data.SQLTree.Linq.Async;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using Remotion.Linq.Parsing.Structure.NodeTypeProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Data.SQLTree.Async;

internal static class MethodRegisterHelper
{
  private static readonly ReadOnlyCollection<MethodInfo> AsyncQueryableMethods = new ReadOnlyCollection<MethodInfo>((IEnumerable<MethodInfo>) typeof (AsyncQueryable).GetRuntimeMethods().ToList<MethodInfo>());

  internal static MethodInfoBasedNodeTypeRegistry RegisterAsyncQueryableMethods(
    this MethodInfoBasedNodeTypeRegistry nodeTypeProvider)
  {
    nodeTypeProvider.Register(MethodRegisterHelper.AsyncQueryableMethods.WhereNameMatches("Select").WithoutIndexSelector(1), typeof (SelectExpressionNode));
    nodeTypeProvider.Register(MethodRegisterHelper.AsyncQueryableMethods.WhereNameMatches("Where").WithoutIndexSelector(1), typeof (WhereExpressionNode));
    nodeTypeProvider.Register(MethodRegisterHelper.AsyncQueryableMethods.WhereNameMatches("FirstAsync").WithoutIndexSelector(1), typeof (FirstAsyncExpressionNode));
    nodeTypeProvider.Register(MethodRegisterHelper.AsyncQueryableMethods.WhereNameMatches("FirstOrDefaultAsync").WithoutIndexSelector(1), typeof (FirstAsyncExpressionNode));
    return nodeTypeProvider;
  }

  private static IEnumerable<MethodInfo> WhereNameMatches(
    this IEnumerable<MethodInfo> input,
    string name)
  {
    return input.Where<MethodInfo>((Func<MethodInfo, bool>) (mi => mi.Name == name));
  }

  public static IEnumerable<MethodInfo> WithoutIndexSelector(
    this IEnumerable<MethodInfo> input,
    int parameterPosition)
  {
    return input.Where<MethodInfo>((Func<MethodInfo, bool>) (mi => !MethodRegisterHelper.HasIndexSelectorParameter(mi, parameterPosition)));
  }

  private static bool HasIndexSelectorParameter(MethodInfo methodInfo, int parameterPosition)
  {
    ParameterInfo[] parameters = methodInfo.GetParameters();
    return parameters.Length > parameterPosition && parameters[parameterPosition].ParameterType.GetTypeInfo().UnwrapEnumerable().GenericTypeArguments[1] == typeof (int);
  }

  private static TypeInfo UnwrapEnumerable(this TypeInfo typeInfo)
  {
    TypeInfo typeInfo1 = typeInfo;
    while (typeInfo1.ContainsGenericParameters)
      typeInfo1 = typeInfo1.BaseType.GetTypeInfo();
    return !typeof (Expression).GetTypeInfo().IsAssignableFrom(typeInfo1) ? typeInfo : typeInfo.GenericTypeArguments[0].GetTypeInfo();
  }
}

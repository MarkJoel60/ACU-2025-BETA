// Decompiled with JetBrains decompiler
// Type: PX.Data.AutomapperExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using AutoMapper;
using AutoMapper.Internal;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data;

public static class AutomapperExtensions
{
  /// <summary>
  /// Allows you to define parameter name using an expression instead of a hardcoded string.
  /// </summary>
  public static IMappingExpression<TSource, TDestination> ForCtorParamMatching<TSource, TDestination, TMember>(
    this IMappingExpression<TSource, TDestination> prev,
    Expression<Func<TDestination, TMember>> destinationMember,
    System.Action<ICtorParamConfigurationExpression<TSource>> memberOptions)
  {
    string name = ReflectionHelper.FindProperty((LambdaExpression) destinationMember).Name;
    string ctorParamName = name;
    if (!string.IsNullOrEmpty(name) && !char.IsLower(name, 0))
      ctorParamName = char.ToLowerInvariant(name[0]).ToString() + name.Substring(1);
    return prev.ForCtorParam(ctorParamName, memberOptions);
  }

  public static void AfterMap<TSource, TDestination>(
    this IMappingOperationOptions options,
    Action<TSource, TDestination> action)
  {
    options.AfterMap((Action<object, object>) ((source, destination) => action((TSource) source, (TDestination) destination)));
  }

  public static void AfterMap<TDestination>(
    this IMappingOperationOptions options,
    System.Action<TDestination> action)
  {
    options.AfterMap((Action<object, object>) ((source, destination) => action((TDestination) destination)));
  }
}

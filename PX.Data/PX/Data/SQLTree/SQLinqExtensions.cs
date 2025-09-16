// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLinqExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using Remotion.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Data.SQLTree;

public static class SQLinqExtensions
{
  public static T AsParam<T>(this T p) => p;

  internal static string GetQueryText(this IQueryable q)
  {
    IQueryExecutor executor = q.Provider is QueryProviderBase provider ? provider.Executor : (IQueryExecutor) null;
    return executor == null ? q.ToString() : executor.ToString();
  }

  [PXInternalUseOnly]
  public static IQueryable<T> SkipRestrictions<T>(this IQueryable<T> query)
  {
    if (!(query is SQLQueryable<T> sqlQueryable))
      throw new NotSupportedException("SkipRestrictions() is defined for SQLQuerable only. ");
    sqlQueryable.SQLinqExecutor?.SkipRestrictions();
    return query;
  }

  public static IQueryable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
    this IQueryable<TOuter> outer,
    IEnumerable<TInner> inner,
    Expression<Func<TOuter, TKey>> outerKeySelector,
    Expression<Func<TInner, TKey>> innerKeySelector,
    Expression<Func<TOuter, TInner, TResult>> resultSelector)
  {
    Expression<Func<TInner, TKey>> innerKeySelector1 = (Expression<Func<TInner, TKey>>) (() => Expression.Call(JoinSubstitutions.GetLeftJoinMethod(typeof (TKey)), innerKeySelector.Body));
    return outer.Join<TOuter, TInner, TKey, TResult>(inner, outerKeySelector, innerKeySelector1, resultSelector);
  }

  public static IQueryable<TResult> FullJoin<TOuter, TInner, TKey, TResult>(
    this IQueryable<TOuter> outer,
    IEnumerable<TInner> inner,
    Expression<Func<TOuter, TKey>> outerKeySelector,
    Expression<Func<TInner, TKey>> innerKeySelector,
    Expression<Func<TOuter, TInner, TResult>> resultSelector)
  {
    Expression<Func<TInner, TKey>> innerKeySelector1 = (Expression<Func<TInner, TKey>>) (() => Expression.Call(JoinSubstitutions.GetFullJoinMethod(typeof (TKey)), innerKeySelector.Body));
    return outer.Join<TOuter, TInner, TKey, TResult>(inner, outerKeySelector, innerKeySelector1, resultSelector);
  }

  public static IQueryable<TResult> RightJoin<TOuter, TInner, TKey, TResult>(
    this IQueryable<TOuter> outer,
    IEnumerable<TInner> inner,
    Expression<Func<TOuter, TKey>> outerKeySelector,
    Expression<Func<TInner, TKey>> innerKeySelector,
    Expression<Func<TOuter, TInner, TResult>> resultSelector)
  {
    Expression<Func<TInner, TKey>> innerKeySelector1 = (Expression<Func<TInner, TKey>>) (() => Expression.Call(JoinSubstitutions.GetRightJoinMethod(typeof (TKey)), innerKeySelector.Body));
    return outer.Join<TOuter, TInner, TKey, TResult>(inner, outerKeySelector, innerKeySelector1, resultSelector);
  }

  public static IQueryable<TResult> CrossJoin<TOuter, TInner, TResult>(
    this IQueryable<TOuter> outer1,
    IEnumerable<TInner> inner1,
    Expression<Func<TOuter, TInner, TResult>> resultSelector)
  {
    Expression<Func<TOuter, int>> outerKeySelector = (Expression<Func<TOuter, int>>) (outer2 => 0);
    Expression<Func<TInner, int>> innerKeySelector = (Expression<Func<TInner, int>>) (inner2 => Expression.Call(JoinSubstitutions.GetCrossJoinMethod(typeof (int)), 0));
    return outer1.Join<TOuter, TInner, int, TResult>(inner1, outerKeySelector, innerKeySelector, resultSelector);
  }

  public static IEnumerable<T> Merge<T, TDac>(
    this IQueryable<T> source,
    Expression<Func<T, TDac>> dacSelector)
    where TDac : IBqlTable
  {
    if (!(source is SQLQueryable<T> sqlQueryable))
      throw new ArgumentException("Merge is defined for SQLQueryable only.");
    sqlQueryable.SQLinqExecutor.DacToMerge = typeof (TDac);
    sqlQueryable.SQLinqExecutor.MergeCache = new bool?(true);
    return (IEnumerable<T>) sqlQueryable;
  }

  public static IQueryable<T> ReadOnly<T>(this IQueryable<T> source)
  {
    if (!(source is SQLQueryable<T> sqlQueryable))
      throw new ArgumentException("ReadOnly is defined for SQLQueryable only.");
    sqlQueryable.SQLinqExecutor.DacToMerge = (System.Type) null;
    sqlQueryable.SQLinqExecutor.MergeCache = new bool?(false);
    return (IQueryable<T>) sqlQueryable;
  }

  internal static System.Type GetDacTypeToMerge(this SQLinqExecutor linqExecutor)
  {
    if (linqExecutor != null)
    {
      bool? mergeCache = linqExecutor.MergeCache;
      bool flag = false;
      if (!(mergeCache.GetValueOrDefault() == flag & mergeCache.HasValue))
        return linqExecutor.DacToMerge;
    }
    return (System.Type) null;
  }

  internal static bool IsNotAggregatedJoinedAttrQuery(this SQLExpression expr)
  {
    Query query = expr is SubQuery subQuery ? subQuery.Query() : (Query) null;
    return query is JoinedAttrQuery && (query?.GetGroupBy() == null || query.GetGroupBy().Count == 0);
  }

  public static IQueryable<TResult> PXCast<TResult>(this IQueryable<PXResult> source) where TResult : PXResult
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    return source.Select<PXResult, TResult>(Expression.Lambda<Func<PXResult, TResult>>((Expression) Expression.Call(x, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.Convert)), Array.Empty<Expression>()), parameterExpression));
  }
}

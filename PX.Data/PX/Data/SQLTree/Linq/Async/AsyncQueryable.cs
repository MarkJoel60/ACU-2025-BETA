// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Linq.Async.AsyncQueryable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

#nullable disable
namespace PX.Data.SQLTree.Linq.Async;

internal static class AsyncQueryable
{
  private static MethodInfo GetMethodInfo<T1, T2>(Func<T1, T2> f, T1 unused1) => f.Method;

  private static MethodInfo GetMethodInfo<T1, T2, T3>(Func<T1, T2, T3> f, T1 unused1, T2 unused2)
  {
    return f.Method;
  }

  public static IAsyncQueryable<TSource> Where<TSource>(
    this IAsyncQueryable<TSource> source,
    Expression<Func<TSource, bool>> predicate)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    if (predicate == null)
      throw new ArgumentNullException(nameof (predicate));
    return source.Provider.CreateAsyncQuery<TSource>((Expression) Expression.Call((Expression) null, AsyncQueryable.GetMethodInfo<IAsyncQueryable<TSource>, Expression<Func<TSource, bool>>, IAsyncQueryable<TSource>>(new Func<IAsyncQueryable<TSource>, Expression<Func<TSource, bool>>, IAsyncQueryable<TSource>>(AsyncQueryable.Where<TSource>), source, predicate), new Expression[2]
    {
      ((IAsyncQueryable) source).Expression,
      (Expression) Expression.Quote((Expression) predicate)
    }));
  }

  public static IAsyncQueryable<TSource> Where<TSource>(
    this IAsyncQueryable<TSource> source,
    Expression<Func<TSource, int, bool>> predicate)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    if (predicate == null)
      throw new ArgumentNullException(nameof (predicate));
    return source.Provider.CreateAsyncQuery<TSource>((Expression) Expression.Call((Expression) null, AsyncQueryable.GetMethodInfo<IAsyncQueryable<TSource>, Expression<Func<TSource, int, bool>>, IAsyncQueryable<TSource>>(new Func<IAsyncQueryable<TSource>, Expression<Func<TSource, int, bool>>, IAsyncQueryable<TSource>>(AsyncQueryable.Where<TSource>), source, predicate), new Expression[2]
    {
      ((IAsyncQueryable) source).Expression,
      (Expression) Expression.Quote((Expression) predicate)
    }));
  }

  public static IAsyncQueryable<TResult> Select<TSource, TResult>(
    this IAsyncQueryable<TSource> source,
    Expression<Func<TSource, TResult>> selector)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    if (selector == null)
      throw new ArgumentNullException(nameof (selector));
    return source.Provider.CreateAsyncQuery<TResult>((Expression) Expression.Call((Expression) null, AsyncQueryable.GetMethodInfo<IAsyncQueryable<TSource>, Expression<Func<TSource, TResult>>, IAsyncQueryable<TResult>>(new Func<IAsyncQueryable<TSource>, Expression<Func<TSource, TResult>>, IAsyncQueryable<TResult>>(AsyncQueryable.Select<TSource, TResult>), source, selector), new Expression[2]
    {
      ((IAsyncQueryable) source).Expression,
      (Expression) Expression.Quote((Expression) selector)
    }));
  }

  public static IAsyncQueryable<TResult> Select<TSource, TResult>(
    this IAsyncQueryable<TSource> source,
    Expression<Func<TSource, int, TResult>> selector)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    if (selector == null)
      throw new ArgumentNullException(nameof (selector));
    return source.Provider.CreateAsyncQuery<TResult>((Expression) Expression.Call((Expression) null, AsyncQueryable.GetMethodInfo<IAsyncQueryable<TSource>, Expression<Func<TSource, int, TResult>>, IAsyncQueryable<TResult>>(new Func<IAsyncQueryable<TSource>, Expression<Func<TSource, int, TResult>>, IAsyncQueryable<TResult>>(AsyncQueryable.Select<TSource, TResult>), source, selector), new Expression[2]
    {
      ((IAsyncQueryable) source).Expression,
      (Expression) Expression.Quote((Expression) selector)
    }));
  }

  public static TSource FirstAsync<TSource>(this IAsyncQueryable<TSource> source)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    return (TSource) source.Provider.ExecuteAsync<TSource>((Expression) Expression.Call((Expression) null, AsyncQueryable.GetMethodInfo<IAsyncQueryable<TSource>, TSource>(new Func<IAsyncQueryable<TSource>, TSource>(AsyncQueryable.FirstAsync<TSource>), source), ((IAsyncQueryable) source).Expression), new CancellationToken()).GetAwaiter().GetResult();
  }

  public static TSource FirstOrDefaultAsync<TSource>(this IAsyncQueryable<TSource> source)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    object result = source.Provider.ExecuteAsync<TSource>((Expression) Expression.Call((Expression) null, AsyncQueryable.GetMethodInfo<IAsyncQueryable<TSource>, TSource>(new Func<IAsyncQueryable<TSource>, TSource>(AsyncQueryable.FirstOrDefaultAsync<TSource>), source), ((IAsyncQueryable) source).Expression), new CancellationToken()).GetAwaiter().GetResult();
    return (TSource) (result ?? (AsyncQueryable.IsAsyncQueryable(typeof (TSource)) ? AsyncEnumerableQuery.CreateEmpty(typeof (TSource).GenericTypeArguments[0]) : result));
  }

  private static bool IsAsyncQueryable(System.Type clrType)
  {
    if (clrType == (System.Type) null)
      return false;
    return ((IEnumerable<System.Type>) clrType.GetInterfaces()).Union<System.Type>((IEnumerable<System.Type>) new System.Type[1]
    {
      clrType
    }).FirstOrDefault<System.Type>((Func<System.Type, bool>) (t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof (IAsyncQueryable<>))) != (System.Type) null;
  }
}

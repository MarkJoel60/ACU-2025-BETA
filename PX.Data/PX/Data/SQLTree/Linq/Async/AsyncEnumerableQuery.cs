// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Linq.Async.AsyncEnumerableQuery
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Data.SQLTree.Linq.Async;

internal abstract class AsyncEnumerableQuery
{
  /// <summary>
  /// Gets the enumerable sequence obtained from evaluating the expression tree.
  /// </summary>
  internal abstract object Enumerable { get; }

  /// <summary>
  /// Gets the expression tree representing the asynchronous enumerable sequence.
  /// </summary>
  internal abstract Expression Expression { get; }

  internal static object Create(System.Type elementType, IEnumerable sequence)
  {
    return Activator.CreateInstance(typeof (AsyncEnumerableQuery<>).MakeGenericType(elementType), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, new object[1]
    {
      (object) sequence
    }, (CultureInfo) null);
  }

  internal static object CreateEmpty(System.Type elementType)
  {
    return Activator.CreateInstance(typeof (AsyncEnumerableQuery<>).MakeGenericType(elementType), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, (object[]) null, (CultureInfo) null);
  }

  internal static Expression CreateExpressionForEmpty(System.Type elementType)
  {
    return (Expression) Expression.New(typeof (AsyncEnumerableQuery<>).MakeGenericType(elementType));
  }
}

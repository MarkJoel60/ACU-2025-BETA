// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.BqlExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common;

public static class BqlExtensions
{
  public static TTable SelectSingle<TTable>(
    this BqlCommand command,
    PXGraph graph,
    bool isReadonly,
    params object[] parameters)
    where TTable : IBqlTable
  {
    object obj = command.CreateView(graph, mergeCache: !isReadonly).SelectSingle(parameters);
    return !(obj is PXResult pxResult) ? (TTable) obj : (TTable) pxResult[typeof (TTable)];
  }

  public static TTable SelectSingle<TTable>(
    this BqlCommand command,
    PXGraph graph,
    bool isReadonly,
    IBqlTable[] currents,
    params object[] parameters)
    where TTable : IBqlTable
  {
    object obj = command.CreateView(graph, mergeCache: !isReadonly).SelectSingleBound((object[]) currents, parameters);
    return !(obj is PXResult pxResult) ? (TTable) obj : (TTable) pxResult[typeof (TTable)];
  }

  public static TTable SelectSingle<TTable>(
    this BqlCommand command,
    PXGraph graph,
    params object[] parameters)
    where TTable : IBqlTable
  {
    return command.SelectSingle<TTable>(graph, false, parameters);
  }

  public static TTable SelectSingleReadonly<TTable>(
    this BqlCommand command,
    PXGraph graph,
    params object[] parameters)
    where TTable : IBqlTable
  {
    return command.SelectSingle<TTable>(graph, true, parameters);
  }

  public static TTable SelectSingleReadonly<TTable>(
    this BqlCommand command,
    PXGraph graph,
    IBqlTable[] currents,
    params object[] parameters)
    where TTable : IBqlTable
  {
    return command.SelectSingle<TTable>(graph, true, currents, parameters);
  }

  public static IEnumerable<TTable> Select<TTable>(
    this BqlCommand command,
    PXGraph graph,
    bool isReadonly,
    params object[] parameters)
    where TTable : IBqlTable
  {
    return GraphHelper.RowCast<TTable>((IEnumerable) command.CreateView(graph, mergeCache: !isReadonly).SelectMulti(parameters));
  }

  public static IEnumerable<TTable> Select<TTable>(
    this BqlCommand command,
    PXGraph graph,
    params object[] parameters)
    where TTable : IBqlTable
  {
    return command.Select<TTable>(graph, false, parameters);
  }

  public static IEnumerable<TTable> SelectReadonly<TTable>(
    this BqlCommand command,
    PXGraph graph,
    params object[] parameters)
    where TTable : IBqlTable
  {
    return command.Select<TTable>(graph, true, parameters);
  }

  public static bool Any(
    this BqlCommand command,
    PXGraph graph,
    bool isReadonly,
    params object[] parameters)
  {
    return command.CreateView(graph, mergeCache: !isReadonly).SelectSingle(parameters) != null;
  }

  public static bool Any(
    this BqlCommand command,
    PXGraph graph,
    bool isReadonly,
    IBqlTable[] currents,
    params object[] parameters)
  {
    return command.CreateView(graph, mergeCache: !isReadonly).SelectSingleBound((object[]) currents, parameters) != null;
  }

  public static bool Any(this BqlCommand command, PXGraph graph, params object[] parameters)
  {
    return command.Any(graph, false, parameters);
  }

  public static bool Any(
    this BqlCommand command,
    PXGraph graph,
    IBqlTable[] currents,
    params object[] parameters)
  {
    return command.Any(graph, false, currents, parameters);
  }

  public static bool AnyReadonly(
    this BqlCommand command,
    PXGraph graph,
    params object[] parameters)
  {
    return command.Any(graph, true, parameters);
  }

  public static bool AnyReadonly(
    this BqlCommand command,
    PXGraph graph,
    IBqlTable[] currents,
    params object[] parameters)
  {
    return command.Any(graph, true, currents, parameters);
  }

  public static PXView CreateView(
    this BqlCommand command,
    PXGraph graph,
    bool clearQueryCache = false,
    bool mergeCache = false)
  {
    PXView view = new PXView(graph, !mergeCache, command);
    if (clearQueryCache)
      view.Clear();
    return view;
  }

  public static object SelectFirst(
    this IBqlSearch command,
    PXGraph graph,
    object data,
    bool isReadOnly = true)
  {
    object obj = graph.TypedViews.GetView((BqlCommand) command, isReadOnly).SelectSingleBound(new object[1]
    {
      data
    }, Array.Empty<object>());
    return obj is PXResult ? ((PXResult) obj)[command.GetField()] : (object) null;
  }
}

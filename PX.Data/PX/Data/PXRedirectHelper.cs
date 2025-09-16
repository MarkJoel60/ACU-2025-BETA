// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRedirectHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#nullable disable
namespace PX.Data;

public static class PXRedirectHelper
{
  public static System.Type GetGraphType(PXCache cache)
  {
    System.Type graphType = (System.Type) null;
    PXPrimaryGraphAttribute.FindPrimaryGraph(cache, out graphType);
    return graphType;
  }

  public static void TryOpenPopup(PXCache cache, object row, string message)
  {
    PXRedirectHelper.TryRedirect(cache, row, message, PXRedirectHelper.WindowMode.Popup);
  }

  public static void TryRedirect(PXCache cache, object row, string message)
  {
    PXRedirectHelper.TryRedirect(cache, row, message, PXRedirectHelper.WindowMode.Same);
  }

  public static void TryRedirect(PXGraph graph, object row, PXRedirectHelper.WindowMode mode)
  {
    if (graph == null)
      throw new ArgumentNullException(nameof (graph));
    if (row == null)
      throw new ArgumentNullException(nameof (row));
    PXRedirectHelper.TryRedirect(graph.Caches[row.GetType()] ?? throw new Exception($"Cache for the '{row.GetType()}' DAC cannot be found."), row, string.Empty, mode);
  }

  public static void TryRedirect(PXGraph graph)
  {
    PXRedirectHelper.TryRedirect(graph, PXRedirectHelper.WindowMode.NewWindow);
  }

  public static void TryRedirect(PXGraph graph, PXRedirectHelper.WindowMode mode)
  {
    switch (mode)
    {
      case PXRedirectHelper.WindowMode.Same:
        throw new PXRedirectRequiredException(graph, string.Empty);
      case PXRedirectHelper.WindowMode.New:
        PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException(graph, true, string.Empty);
        requiredException1.Mode = PXBaseRedirectException.WindowMode.New;
        throw requiredException1;
      case PXRedirectHelper.WindowMode.Popup:
        throw new PXPopupRedirectException(graph, string.Empty, true);
      case PXRedirectHelper.WindowMode.NewWindow:
        PXRedirectRequiredException requiredException2 = new PXRedirectRequiredException(graph, true, string.Empty);
        requiredException2.Mode = PXBaseRedirectException.WindowMode.NewWindow;
        throw requiredException2;
      case PXRedirectHelper.WindowMode.InlineWindow:
        PXRedirectRequiredException requiredException3 = new PXRedirectRequiredException(graph, true, string.Empty);
        requiredException3.Mode = PXBaseRedirectException.WindowMode.InlineWindow;
        throw requiredException3;
      case PXRedirectHelper.WindowMode.Layer:
        PXRedirectRequiredException requiredException4 = new PXRedirectRequiredException(graph, true, string.Empty);
        requiredException4.Mode = PXBaseRedirectException.WindowMode.Layer;
        throw requiredException4;
    }
  }

  public static void TryRedirect(
    PXCache cache,
    object row,
    string message,
    PXRedirectHelper.WindowMode mode)
  {
    PXRedirectHelper.TryRedirect(cache, row, message, mode, (System.Type) null);
  }

  public static void TryRedirect(
    PXCache cache,
    object row,
    string message,
    PXRedirectHelper.WindowMode mode,
    System.Type preferedType)
  {
    if (row == null)
      return;
    object obj1 = row;
    object obj2 = row;
    System.Type graphType;
    System.Type declaredType;
    PXPrimaryGraphBaseAttribute primaryGraph = PXPrimaryGraphAttribute.FindPrimaryGraph(cache, preferedType, ref row, out graphType, out declaredType, out cache);
    if (primaryGraph == null || !(graphType != (System.Type) null))
      return;
    PXEntryStatus status = PXEntryStatus.Notchanged;
    try
    {
      status = cache.GetStatus(row);
    }
    catch
    {
    }
    PXGraph instance1 = PXGraph.CreateInstance(graphType);
    if (HttpContext.Current != null && HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath != null && HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/rest/"))
      instance1.IsExport = true;
    System.Type filter = primaryGraph is PXPrimaryGraphAttribute ? ((PXPrimaryGraphAttribute) primaryGraph).Filter : (System.Type) null;
    if (filter == (System.Type) null)
    {
      if (row != obj2)
        declaredType = row.GetType();
      PXCache primary = instance1.Caches[declaredType];
      primary.IsDirty = cache.IsDirty;
      if (primary.GetItemType().IsAssignableFrom(row.GetType()))
      {
        instance1.Caches[declaredType].Current = row;
        if (status == PXEntryStatus.Inserted)
        {
          primary.SetStatus(primary.Current, status);
        }
        else
        {
          long num = 1;
          try
          {
            if (instance1.PrimaryItemType == primary.GetItemType())
            {
              if (primary.Keys.Count > 0)
              {
                List<string> stringList = new List<string>((IEnumerable<string>) primary.Keys);
                List<object> objectList = new List<object>(primary.Keys.Select<string, object>((Func<string, object>) (_ => PXFieldState.UnwrapValue(primary.GetValueExt(row, _)))));
                int startRow = 0;
                int totalRows = 0;
                num = instance1.ExecuteSelect(instance1.PrimaryView, (object[]) null, objectList.ToArray(), stringList.ToArray(), (bool[]) null, (PXFilterRow[]) null, ref startRow, 1, ref totalRows).Count();
              }
            }
          }
          catch
          {
          }
          if (num == 0L)
          {
            object stateExt = primary.GetStateExt(row, primary.Keys.Last<string>());
            throw new PXSetPropertyException(PXMessages.LocalizeFormat("{0} '{1}' cannot be found in the system. Please verify whether you have proper access rights to this object.", stateExt is PXFieldState ? (object) ((PXFieldState) stateExt).DisplayName : (object) primary.Keys.Last<string>(), PXFieldState.UnwrapValue(stateExt)));
          }
        }
      }
      else if (row.GetType().IsAssignableFrom(primary.GetItemType()))
      {
        object instance2 = primary.CreateInstance();
        ((PXCache) Activator.CreateInstance(typeof (PXCache<>).MakeGenericType(row.GetType()), (object) primary.Graph)).RestoreCopy(instance2, row);
        primary.Current = instance2;
        row = instance2;
        if (status == PXEntryStatus.Inserted)
        {
          primary.SetStatus(primary.Current, status);
        }
        else
        {
          long num = 1;
          try
          {
            if (instance1.PrimaryItemType == primary.GetItemType())
            {
              if (primary.Keys.Count > 0)
              {
                List<string> stringList = new List<string>((IEnumerable<string>) primary.Keys);
                List<object> objectList = new List<object>(primary.Keys.Select<string, object>((Func<string, object>) (_ => PXFieldState.UnwrapValue(primary.GetValueExt(row, _)))));
                int startRow = 0;
                int totalRows = 0;
                num = instance1.ExecuteSelect(instance1.PrimaryView, (object[]) null, objectList.ToArray(), stringList.ToArray(), (bool[]) null, (PXFilterRow[]) null, ref startRow, 1, ref totalRows).Count();
              }
            }
          }
          catch
          {
          }
          if (num == 0L)
          {
            object stateExt = primary.GetStateExt(row, primary.Keys.Last<string>());
            throw new PXSetPropertyException(PXMessages.LocalizeFormat("{0} '{1}' cannot be found in the system. Please verify whether you have proper access rights to this object.", stateExt is PXFieldState ? (object) ((PXFieldState) stateExt).DisplayName : (object) primary.Keys.Last<string>(), PXFieldState.UnwrapValue(stateExt)));
          }
        }
      }
      else
      {
        object instance3 = primary.CreateInstance();
        ((PXCache) Activator.CreateInstance(typeof (PXCache<>).MakeGenericType(declaredType), (object) primary.Graph)).RestoreCopy(instance3, row);
        primary.Current = instance3;
        if (status == PXEntryStatus.Inserted)
          primary.SetStatus(primary.Current, status);
      }
    }
    else
    {
      PXCache cach = instance1.Caches[filter];
      object data = cach._Clone(cach.Current);
      foreach (string field in (List<string>) cache.Fields)
      {
        object valueExt = cache.GetValueExt(row, field);
        if (valueExt is PXFieldState)
          valueExt = ((PXFieldState) valueExt).Value;
        if (valueExt != null)
          cach.SetValueExt(data, field, valueExt);
      }
      cach.Update(data);
      foreach (PXView pxView in new List<PXView>((IEnumerable<PXView>) instance1.Views.Values))
      {
        if (pxView.GetItemType() == filter)
          pxView.SelectSingle();
      }
    }
    instance1.EnsureIfArchived();
    switch (mode)
    {
      case PXRedirectHelper.WindowMode.Same:
        throw new PXRedirectRequiredException(instance1, message);
      case PXRedirectHelper.WindowMode.New:
        PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException(instance1, true, message);
        requiredException1.Mode = PXBaseRedirectException.WindowMode.New;
        throw requiredException1;
      case PXRedirectHelper.WindowMode.Popup:
        throw new PXPopupRedirectException(instance1, message, true);
      case PXRedirectHelper.WindowMode.NewWindow:
        PXRedirectRequiredException requiredException2 = new PXRedirectRequiredException(instance1, true, message);
        requiredException2.Mode = PXBaseRedirectException.WindowMode.NewWindow;
        throw requiredException2;
      case PXRedirectHelper.WindowMode.InlineWindow:
        PXRedirectRequiredException requiredException3 = new PXRedirectRequiredException(instance1, true, message);
        requiredException3.Mode = PXBaseRedirectException.WindowMode.InlineWindow;
        throw requiredException3;
      case PXRedirectHelper.WindowMode.Layer:
        PXRedirectRequiredException requiredException4 = new PXRedirectRequiredException(instance1, true, message);
        requiredException4.Mode = PXBaseRedirectException.WindowMode.Layer;
        throw requiredException4;
    }
  }

  /// <summary>Return "RedirectX"-type prefix.</summary>
  internal static string GetRedirectPrefix(
    PXBaseRedirectException.WindowMode windowMode,
    bool suppressFrameset = false,
    PXGraph dataGraph = null)
  {
    bool flag = dataGraph != null && dataGraph.IsDirty;
    int num;
    switch (windowMode)
    {
      case PXBaseRedirectException.WindowMode.New:
        num = suppressFrameset ? 4 : 3;
        break;
      case PXBaseRedirectException.WindowMode.NewWindow:
      case PXBaseRedirectException.WindowMode.InlineWindow:
      case PXBaseRedirectException.WindowMode.Layer:
        num = flag ? 8 : 7;
        break;
      default:
        num = !suppressFrameset ? (flag ? 1 : 0) : (flag ? 6 : 5);
        break;
    }
    return $"Redirect{num}:";
  }

  public enum WindowMode
  {
    Same,
    New,
    Popup,
    NewWindow,
    InlineWindow,
    Layer,
  }
}

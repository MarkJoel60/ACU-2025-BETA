// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.GraphHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.FS;

internal static class GraphHelper
{
  public static void RaiseRowPersistingException<Field>(PXCache cache, object row) where Field : IBqlField
  {
    string name = typeof (Field).Name;
    string displayName = PXUIFieldAttribute.GetDisplayName<Field>(cache);
    cache.RaiseExceptionHandling<Field>(row, (object) null, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("'{0}' cannot be empty.", new object[1]
    {
      (object) displayName
    })));
    throw new PXRowPersistingException(name, (object) null, "'{0}' cannot be empty.", new object[1]
    {
      (object) displayName
    });
  }

  public static PXAction GetSaveAction(this PXGraph graph)
  {
    PXAction pxAction1 = (PXAction) null;
    Type itemType = !string.IsNullOrEmpty(graph.PrimaryView) ? graph.Views[graph.PrimaryView].GetItemType() : (Type) null;
    foreach (PXAction pxAction2 in (IEnumerable) ((OrderedDictionary) graph.Actions).Values)
    {
      if (pxAction2.GetState((object) null) is PXButtonState state && state.SpecialType == 1 && (itemType == (Type) null || state.ItemType == (Type) null || itemType == state.ItemType || itemType.IsSubclassOf(state.ItemType)))
      {
        pxAction1 = pxAction2;
        break;
      }
    }
    return pxAction1 != null ? pxAction1 : throw new PXException("There is not a Save action in the graph " + graph.GetType()?.ToString());
  }

  public static PXAction GetDeleteAction(this PXGraph graph)
  {
    PXAction pxAction1 = (PXAction) null;
    Type itemType = !string.IsNullOrEmpty(graph.PrimaryView) ? graph.Views[graph.PrimaryView].GetItemType() : (Type) null;
    foreach (PXAction pxAction2 in (IEnumerable) ((OrderedDictionary) graph.Actions).Values)
    {
      if (pxAction2.GetState((object) null) is PXButtonState state && state.SpecialType == 12 && (itemType == (Type) null || state.ItemType == (Type) null || itemType == state.ItemType || itemType.IsSubclassOf(state.ItemType)))
      {
        pxAction1 = pxAction2;
        break;
      }
    }
    return pxAction1 != null && pxAction1.GetEnabled() ? pxAction1 : throw new PXException("The appointment cannot be deleted because either its status or the workflow stage prohibits deletion.");
  }

  public static void SetValueExtIfDifferent<Field>(
    this PXCache cache,
    object data,
    object newValue,
    bool verifyAcceptanceOfNewValue = true)
    where Field : IBqlField
  {
    object obj = cache.GetValue<Field>(data);
    if ((obj != null || newValue == null) && (obj == null || newValue != null) && (obj == null || obj.Equals(newValue)))
      return;
    cache.SetValueExt<Field>(data, newValue);
    if (verifyAcceptanceOfNewValue && !GraphHelper.AreEquivalentValues(cache.GetValue<Field>(data), newValue))
    {
      string str = string.Empty;
      PXFieldState pxFieldState;
      try
      {
        pxFieldState = (PXFieldState) cache.GetStateExt<Field>(data);
      }
      catch
      {
        pxFieldState = (PXFieldState) null;
      }
      if (pxFieldState != null && pxFieldState.Error != null)
        str = pxFieldState.Error;
      throw new PXException("The following error occurred on specifying a value in {0}: {1}", new object[2]
      {
        (object) PXUIFieldAttribute.GetDisplayName<Field>(cache),
        (object) str
      });
    }
  }

  public static bool AreEquivalentValues(object value1, object value2)
  {
    if (value1 != null)
      return GraphHelper.AreEquivalentValuesBasedOnValue1Type(value1, value2);
    return value2 == null || GraphHelper.AreEquivalentValuesBasedOnValue1Type(value2, value1);
  }

  public static bool AreEquivalentValuesBasedOnValue1Type(object value1, object value2)
  {
    switch (value1)
    {
      case null:
        throw new ArgumentException();
      case string _:
        return GraphHelper.AreEquivalentStrings((string) value1, value2);
      case Decimal _:
      case double _:
        return GraphHelper.AreEquivalentDecimals((Decimal) value1, value2);
      default:
        return value1.Equals(value2);
    }
  }

  public static bool AreEquivalentStrings(string value1, object value2)
  {
    string str1 = value1 != null ? value1.Trim() : throw new ArgumentException();
    string str2 = string.Empty;
    if (value2 != null)
    {
      if (!(value2 is string))
        return false;
      str2 = ((string) value2).Trim();
    }
    return str1.Equals(str2);
  }

  public static bool AreEquivalentDecimals(Decimal value1, object value2)
  {
    Decimal d1 = value1;
    switch (value2)
    {
      case null:
        return false;
      case Decimal _:
      case double _:
        Decimal d2 = (Decimal) value2;
        return Math.Round(d1, 2, MidpointRounding.AwayFromZero) == Math.Round(d2, 2, MidpointRounding.AwayFromZero);
      default:
        return false;
    }
  }

  public static bool IsValueChanging(PXCache cache, object oldValue, object newValue)
  {
    if (oldValue == null && newValue == null)
      return false;
    if (oldValue != null && newValue != null)
    {
      if (newValue is string && (oldValue is DateTime || oldValue is DateTime?))
      {
        DateTime? handlingDateTime = SharedFunctions.TryParseHandlingDateTime(cache, newValue);
        if (!handlingDateTime.HasValue)
          return true;
        DateTime? nullable = handlingDateTime;
        DateTime dateTime = (DateTime) oldValue;
        if ((nullable.HasValue ? (nullable.GetValueOrDefault() == dateTime ? 1 : 0) : 0) != 0)
          return false;
      }
      else if (oldValue.Equals(newValue))
        return false;
    }
    return true;
  }

  public static PXSelect<Table> AddViewToPersist<Table>(PXGraph graph) where Table : class, IBqlTable, new()
  {
    PXSelect<Table> persist = new PXSelect<Table>(graph);
    if (graph.Views.Caches.Contains(typeof (Table)))
      return persist;
    graph.Views.Caches.Add(typeof (Table));
    return persist;
  }

  internal static PXGraph LoadGraph(string graphName, string stateId = null)
  {
    Type type = PXBuildManager.GetType(graphName, true);
    using (new PXPreserveScope())
    {
      PXGraph instance = PXGraph.CreateInstance(type, stateId ?? "");
      if (type == typeof (PXGraph))
        instance.Caches[typeof (AccessInfo)].Current = (object) instance.Accessinfo;
      instance.Load();
      return instance;
    }
  }
}

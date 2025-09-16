// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.PlacementHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.WorkflowAPI;

internal static class PlacementHelper
{
  public static bool HasPlacement(Placement placement, string relativeAction)
  {
    return (placement == Placement.After || placement == Placement.Before) && !string.IsNullOrEmpty(relativeAction) || placement == Placement.First || placement == Placement.Last;
  }

  public static IEnumerable<T> SortBeforeAfter<T>(
    IEnumerable<T> actions,
    Func<T, string> idSelector,
    Func<T, Placement> placementSelector,
    Func<T, string> placeRelativeIdSelector)
  {
    LinkedList<T> target1 = new LinkedList<T>();
    Dictionary<string, List<T>> placeBefore = new Dictionary<string, List<T>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    Dictionary<string, List<T>> placeAfter = new Dictionary<string, List<T>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    List<T> objList = new List<T>();
    LinkedList<T> target2 = new LinkedList<T>();
    foreach (T action in actions)
    {
      string key = placeRelativeIdSelector(action);
      switch (placementSelector(action))
      {
        case Placement.After:
          if (!string.IsNullOrEmpty(key))
          {
            PlacementHelper.AddToListDict<string, T>((IDictionary<string, List<T>>) placeAfter, key, action);
            continue;
          }
          target1.AddLast(action);
          continue;
        case Placement.Before:
          if (!string.IsNullOrEmpty(key))
          {
            PlacementHelper.AddToListDict<string, T>((IDictionary<string, List<T>>) placeBefore, key, action);
            continue;
          }
          target1.AddLast(action);
          continue;
        case Placement.First:
          objList.Add(action);
          continue;
        case Placement.Last:
          target2.AddLast(action);
          continue;
        default:
          target1.AddLast(action);
          continue;
      }
    }
    foreach (T obj in objList)
      target1.AddFirst(obj);
    do
      ;
    while (MaybeInsertBeforeAfters(target1) || MaybeInsertBeforeAfters(target2));
    foreach (string key in (IEnumerable<string>) placeBefore.Keys.OrderBy<string, string>((Func<string, string>) (s => s), (IComparer<string>) StringComparer.OrdinalIgnoreCase))
    {
      placeBefore[key].Reverse();
      foreach (T obj in placeBefore[key])
        target1.AddLast(obj);
    }
    foreach (string key in (IEnumerable<string>) placeAfter.Keys.OrderBy<string, string>((Func<string, string>) (s => s), (IComparer<string>) StringComparer.OrdinalIgnoreCase))
    {
      foreach (T obj in placeAfter[key])
        target1.AddLast(obj);
    }
    foreach (T obj in target2)
      target1.AddLast(obj);
    return (IEnumerable<T>) target1;

    bool MaybeInsertBeforeAfters(LinkedList<T> target)
    {
      LinkedListNode<T> node = target.First;
      if (node == null)
        return false;
      bool flag = false;
      LinkedListNode<T> next;
      for (; node != null; node = next)
      {
        next = node.Next;
        string key = idSelector(node.Value);
        List<T> objList1;
        if (placeBefore.TryGetValue(key, out objList1))
        {
          flag = true;
          objList1.Reverse();
          foreach (T obj in objList1)
            target.AddBefore(node, obj);
          placeBefore.Remove(key);
        }
        List<T> objList2;
        if (placeAfter.TryGetValue(key, out objList2))
        {
          flag = true;
          objList2.Reverse();
          foreach (T obj in objList2)
            target.AddAfter(node, obj);
          placeAfter.Remove(key);
        }
      }
      return flag;
    }
  }

  private static void AddToListDict<TKey, TValue>(
    IDictionary<TKey, List<TValue>> dictionary,
    TKey key,
    TValue value)
  {
    List<TValue> objList;
    if (!dictionary.TryGetValue(key, out objList))
    {
      objList = new List<TValue>();
      dictionary[key] = objList;
    }
    objList.Add(value);
  }
}

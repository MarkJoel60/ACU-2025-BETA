// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.OrderableWorkflowElementHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.WorkflowAPI;

internal static class OrderableWorkflowElementHelper
{
  internal static IEnumerable<T> GetSortedWorkflowElements<T>(this IEnumerable<T> steps) where T : IOrderableWorkflowElement
  {
    List<T> list1 = steps.Where<T>((Func<T, bool>) (it => !string.IsNullOrEmpty(it.NearKey) && it.MoveBefore == MoveObjectInCollection.Before)).ToList<T>();
    List<T> list2 = steps.Where<T>((Func<T, bool>) (it => !string.IsNullOrEmpty(it.NearKey) && it.MoveBefore == MoveObjectInCollection.After)).Reverse<T>().ToList<T>();
    List<T> list3 = steps.Where<T>((Func<T, bool>) (it => it.MoveBefore == MoveObjectInCollection.First)).Reverse<T>().ToList<T>();
    LinkedList<T> linkedList = EnumerableExtensions.ToLinkedList<T>(list3.Union<T>(steps.Except<T>(list1.Union<T>((IEnumerable<T>) list2).Union<T>((IEnumerable<T>) list3))));
    do
    {
      int count = linkedList.Count;
      T[] array1 = list1.ToArray();
      list1.Clear();
      foreach (T obj in array1)
      {
        T step = obj;
        LinkedListNode<T> node = EnumerableExtensions.Find<T>(linkedList, (Func<T, bool>) (f => StringComparer.OrdinalIgnoreCase.Equals(f.Key, step.NearKey)));
        if (node == null)
          list1.Add(step);
        else
          linkedList.AddBefore(node, step);
      }
      T[] array2 = list2.ToArray();
      list2.Clear();
      foreach (T obj in array2)
      {
        T step = obj;
        LinkedListNode<T> last = EnumerableExtensions.FindLast<T>(linkedList, (Func<T, bool>) (f => StringComparer.OrdinalIgnoreCase.Equals(f.Key, step.NearKey)));
        if (last == null)
          list2.Add(step);
        else
          linkedList.AddAfter(last, step);
      }
      if (linkedList.Count == count && (list1.Any<T>() || list2.Any<T>()))
        throw new PXInvalidOperationException("Broken order");
    }
    while (list1.Any<T>() || list2.Any<T>());
    return (IEnumerable<T>) linkedList;
  }
}

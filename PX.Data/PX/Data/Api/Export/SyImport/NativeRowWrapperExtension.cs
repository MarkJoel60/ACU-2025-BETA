// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Export.SyImport.NativeRowWrapperExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Api.Export.SyImport;

internal static class NativeRowWrapperExtension
{
  public static ICollection<string> GetKeys(
    this IDictionary<string, List<NativeRowWrapperDictionary>> target)
  {
    List<string> list = target.Keys.ToList<string>();
    foreach (string key in (IEnumerable<string>) target.Keys)
    {
      List<string> stringList = list;
      NativeRowWrapperDictionary target1 = target[key].FirstOrDefault<NativeRowWrapperDictionary>();
      IEnumerable<string> collection = (target1 != null ? (IEnumerable<string>) target1.GetKeys() : (IEnumerable<string>) null) ?? Enumerable.Empty<string>();
      stringList.AddRange(collection);
    }
    return (ICollection<string>) list;
  }

  public static bool TryGetValue(
    this IDictionary<string, List<NativeRowWrapperDictionary>> target,
    string key,
    out List<NativeRowWrapperDictionary> value,
    Stack<string> viewsHierarchy)
  {
    IDictionary<string, List<NativeRowWrapperDictionary>> dictionary = target;
    while (viewsHierarchy.Count > 1)
    {
      if (dictionary == null)
      {
        value = (List<NativeRowWrapperDictionary>) null;
        return false;
      }
      List<NativeRowWrapperDictionary> source;
      if (dictionary.TryGetValue(viewsHierarchy.Pop(), out source))
        dictionary = (IDictionary<string, List<NativeRowWrapperDictionary>>) source.FirstOrDefault<NativeRowWrapperDictionary>();
    }
    List<NativeRowWrapperDictionary> wrapperDictionaryList;
    if (dictionary != null && dictionary.TryGetValue(viewsHierarchy.Pop(), out wrapperDictionaryList))
    {
      value = wrapperDictionaryList;
      return true;
    }
    value = (List<NativeRowWrapperDictionary>) null;
    return false;
  }

  public static void Add(
    this IDictionary<string, List<NativeRowWrapperDictionary>> target,
    Stack<string> keysHierarchy,
    List<NativeRowWrapperDictionary> value)
  {
    string str;
    while (true)
    {
      str = keysHierarchy.Pop();
      if (keysHierarchy.Count != 0)
      {
        List<NativeRowWrapperDictionary> orAdd = target.GetOrAdd<string, List<NativeRowWrapperDictionary>>(str, new List<NativeRowWrapperDictionary>());
        if (orAdd.Count == 0)
        {
          if (value.Count > 0 && keysHierarchy.Peek().StartsWith(str + ":"))
          {
            IEnumerable<object> objects = value.Select<NativeRowWrapperDictionary, object>((Func<NativeRowWrapperDictionary, object>) (x => x.NativeRowWrapper.Result));
            ViewSelectResults source = new ViewSelectResults(str);
            int v = 0;
            foreach (object result in objects)
            {
              source.AddRow(result);
              source.AddCell(SyExportContext.GetFieldID(str), (object) v);
              ++v;
            }
            List<NativeRowWrapperDictionary> list = source.Select<NativeRowWrapper, NativeRowWrapperDictionary>((Func<NativeRowWrapper, NativeRowWrapperDictionary>) (c => new NativeRowWrapperDictionary(c))).ToList<NativeRowWrapperDictionary>();
            orAdd.AddRange((IEnumerable<NativeRowWrapperDictionary>) list);
          }
          else
            orAdd.Add(new NativeRowWrapperDictionary(new NativeRowWrapper(new ViewSelectResults(str), (object) null)));
        }
        target = (IDictionary<string, List<NativeRowWrapperDictionary>>) orAdd.First<NativeRowWrapperDictionary>();
      }
      else
        break;
    }
    target.Add(str, value);
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.PXExtensionComparer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXExtensionComparer : IComparer<System.Type>
{
  public int Compare(System.Type x, System.Type y)
  {
    this.Check(x, y);
    switch (this.RelationDegree(x, y))
    {
      case PXExtensionComparer.DegreeOfRelation.SameType:
        return 0;
      case PXExtensionComparer.DegreeOfRelation.SameLevel:
        return string.Compare(x.FullName, y.FullName);
      case PXExtensionComparer.DegreeOfRelation.XClotherToAncestor:
        return 1;
      case PXExtensionComparer.DegreeOfRelation.YClotherToAncestor:
        return -1;
      case PXExtensionComparer.DegreeOfRelation.XIsAncestor:
        return 1;
      case PXExtensionComparer.DegreeOfRelation.YIsAncestor:
        return -1;
      default:
        return string.Compare(x.FullName, y.FullName);
    }
  }

  private void Check(System.Type x, System.Type y)
  {
    List<System.Type> source = !(x == (System.Type) null) && !(y == (System.Type) null) ? this.GetTypes(x) : throw new ArgumentNullException();
    List<System.Type> types = this.GetTypes(y);
    if (!source.Any<System.Type>() || !types.Any<System.Type>())
      throw new ArgumentException("Sorter class does not support sorting of pure Object type");
  }

  private List<System.Type> GetTypes(System.Type type, List<System.Type> types = null)
  {
    if (types == null)
      types = new List<System.Type>();
    if (type == typeof (object) || types.Contains(type))
      return types;
    types.Add(type);
    foreach (System.Type genericArgument in type.GetGenericArguments())
      types.AddRange(this.GetTypes(genericArgument, types).Where<System.Type>((Func<System.Type, bool>) (_ => !types.Contains(_))));
    types.AddRange(this.GetTypes(type.BaseType, types).Where<System.Type>((Func<System.Type, bool>) (_ => !types.Contains(_))));
    types = types.Distinct<System.Type>().ToList<System.Type>();
    return types;
  }

  private PXExtensionComparer.DegreeOfRelation RelationDegree(System.Type x, System.Type y)
  {
    if (x == y)
      return PXExtensionComparer.DegreeOfRelation.SameType;
    if (x.IsSubclassOf(y))
      return PXExtensionComparer.DegreeOfRelation.YIsAncestor;
    if (y.IsSubclassOf(x))
      return PXExtensionComparer.DegreeOfRelation.XIsAncestor;
    List<System.Type> types1 = this.GetTypes(x);
    List<System.Type> types2 = this.GetTypes(y);
    if (types1.Count == types2.Count)
      return this.ScrambledEquals<System.Type>((IEnumerable<System.Type>) types1, (IEnumerable<System.Type>) types2) || types1.Intersect<System.Type>((IEnumerable<System.Type>) types2).Any<System.Type>() ? PXExtensionComparer.DegreeOfRelation.SameLevel : PXExtensionComparer.DegreeOfRelation.NotRelatives;
    if (!types1.Intersect<System.Type>((IEnumerable<System.Type>) types2).Any<System.Type>())
      return PXExtensionComparer.DegreeOfRelation.NotRelatives;
    return types1.Count > types2.Count ? PXExtensionComparer.DegreeOfRelation.YClotherToAncestor : PXExtensionComparer.DegreeOfRelation.XClotherToAncestor;
  }

  private bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2)
  {
    Dictionary<T, int> dictionary = new Dictionary<T, int>();
    foreach (T key in list1)
    {
      if (dictionary.ContainsKey(key))
        dictionary[key]++;
      else
        dictionary.Add(key, 1);
    }
    foreach (T key in list2)
    {
      if (!dictionary.ContainsKey(key))
        return false;
      dictionary[key]--;
    }
    return dictionary.Values.All<int>((Func<int, bool>) (c => c == 0));
  }

  /// <exclude />
  private enum DegreeOfRelation
  {
    NotRelatives = -1, // 0xFFFFFFFF
    SameType = 0,
    SameLevel = 1,
    XClotherToAncestor = 2,
    YClotherToAncestor = 3,
    XIsAncestor = 4,
    YIsAncestor = 5,
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class PXFieldCollection : List<string>
{
  private Dictionary<string, int> _searchOrig;
  private Dictionary<string, int> _searchAdd;
  private Dictionary<string, string> _viewNames;

  public PXFieldCollection(IEnumerable<string> collection, Dictionary<string, int> dict)
    : base(collection)
  {
    this._searchOrig = dict;
  }

  internal void AddWithFullName(string item, string fullName)
  {
    this.Add(item);
    this.SetFullName(item, fullName);
  }

  internal void SetFullName(string item, string fullName)
  {
    if (this._viewNames == null)
      this._viewNames = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this._viewNames[item] = fullName;
  }

  internal string GetFullName(string item)
  {
    string str;
    return this._viewNames == null || !this._viewNames.TryGetValue(item, out str) ? (string) null : str;
  }

  public new void Add(string item)
  {
    base.Add(item);
    if (this._searchAdd == null)
      this._searchAdd = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this._searchAdd[item] = 0;
  }

  public new void Insert(int index, string item)
  {
    base.Insert(index, item);
    if (this._searchAdd == null)
      this._searchAdd = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this._searchAdd[item] = 0;
  }

  public new void AddRange(IEnumerable<string> collection)
  {
    base.AddRange(collection);
    if (this._searchAdd == null)
      this._searchAdd = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (string key in collection)
      this._searchAdd[key] = 0;
  }

  public new bool Remove(string item)
  {
    if (this._searchAdd != null)
      this._searchAdd.Remove(item);
    return base.Remove(item);
  }

  public new void RemoveAt(int index)
  {
    if (index < this.Count && this._searchAdd != null)
      this._searchAdd.Remove(this[index]);
    base.RemoveAt(index);
  }

  public new void RemoveRange(int index, int count)
  {
    if (this._searchAdd != null)
    {
      for (int index1 = index; index1 < this.Count && index1 < index + count; ++index1)
        this._searchAdd.Remove(this[index1]);
    }
    base.RemoveRange(index, count);
  }

  public new bool Contains(string item)
  {
    if (this._searchOrig.ContainsKey(item))
      return true;
    return this._searchAdd != null && this._searchAdd.ContainsKey(item);
  }
}

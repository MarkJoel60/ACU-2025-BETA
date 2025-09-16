// Decompiled with JetBrains decompiler
// Type: PX.Data.KeysCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class KeysCollection : IEnumerable<string>, IEnumerable
{
  internal HashSet<string> variableLengthStrings = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  private List<string> _Keys = new List<string>();

  public void Add(string field) => this._Keys.Add(field);

  public int Count => this._Keys.Count;

  public string this[int index] => this._Keys[index];

  public bool Contains(string field) => CompareIgnoreCase.IsInList(this._Keys, field);

  IEnumerator<string> IEnumerable<string>.GetEnumerator()
  {
    return (IEnumerator<string>) this._Keys.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._Keys.GetEnumerator();

  public void CopyTo(string[] target) => this._Keys.CopyTo(target);

  public int IndexOf(string field)
  {
    for (int index = 0; index < this._Keys.Count; ++index)
    {
      if (string.Equals(field, this._Keys[index], StringComparison.OrdinalIgnoreCase))
        return index;
    }
    return -1;
  }

  public string[] ToArray() => this._Keys.ToArray();

  public bool Exists(Predicate<string> match) => this._Keys.Exists(match);
}

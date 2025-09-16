// Decompiled with JetBrains decompiler
// Type: PX.Data.RestrictedFieldsSet
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>
/// This class represents a set of fields (<tt>RestrictedField</tt> objects) used by the <tt>PXFieldScope</tt> class.
/// </summary>
public class RestrictedFieldsSet : IEnumerable<RestrictedField>, IEnumerable
{
  private readonly HashSet<RestrictedField> _inner = new HashSet<RestrictedField>();
  private readonly MultiValueDictionary<string, System.Type> _map = new MultiValueDictionary<string, System.Type>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

  /// <exclude />
  public RestrictedFieldsSet()
  {
  }

  /// <exclude />
  public RestrictedFieldsSet(IEnumerable<System.Type> fields)
    : this(fields.Select<System.Type, RestrictedField>((Func<System.Type, RestrictedField>) (t => new RestrictedField(t.DeclaringType, t.Name))))
  {
  }

  /// <exclude />
  public RestrictedFieldsSet(RestrictedFieldsSet set)
    : this((IEnumerable<RestrictedField>) set)
  {
  }

  /// <exclude />
  public RestrictedFieldsSet(IEnumerable<RestrictedField> fields) => this.AddRange(fields);

  /// <exclude />
  public void Add(RestrictedField field)
  {
    if (field == null)
      throw new ArgumentNullException(nameof (field));
    if (!this._inner.Add(field))
      return;
    this._map.Add(field.Field, field.Table);
  }

  /// <exclude />
  public void Remove(RestrictedField field)
  {
    if (field == null || !this._inner.Remove(field))
      return;
    this._map.Remove(field.Field, field.Table);
  }

  /// <exclude />
  public void AddRange(IEnumerable<RestrictedField> fields)
  {
    foreach (RestrictedField field in fields)
      this.Add(field);
  }

  /// <exclude />
  public void RemoveRange(IEnumerable<RestrictedField> fields)
  {
    foreach (RestrictedField field in fields)
      this.Remove(field);
  }

  /// <summary>Indicates whether the fields set contains any items.</summary>
  /// <returns>Returns <tt>true</tt> if the <tt>RestrictedFieldsSet</tt> object
  /// contains any items; otherwise, the method returns <tt>false</tt>.</returns>
  public bool Any() => this._inner.Count > 0;

  /// <exclude />
  public IEnumerator<RestrictedField> GetEnumerator()
  {
    return (IEnumerator<RestrictedField>) this._inner.GetEnumerator();
  }

  /// <exclude />
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  /// <summary>
  /// Gets the number of items in the <tt>RestrictedFieldsSet</tt> object.
  /// </summary>
  public int Count => this._inner.Count;

  /// <exclude />
  public bool Contains(RestrictedField item)
  {
    if (item == null)
      return false;
    bool flag = this._inner.Contains(item);
    IReadOnlyCollection<System.Type> types;
    if (!flag && this._map.TryGetValue(item.Field, ref types))
    {
      foreach (System.Type c in (IEnumerable<System.Type>) types)
      {
        if (item.Table.IsAssignableFrom(c) || c.IsAssignableFrom(item.Table))
          return true;
      }
    }
    return flag;
  }
}

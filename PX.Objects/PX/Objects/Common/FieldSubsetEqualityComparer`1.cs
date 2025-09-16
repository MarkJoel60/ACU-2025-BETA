// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.FieldSubsetEqualityComparer`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

/// <summary>
/// Compares two data records based on the equality of a subset of their fields.
/// The set of fields to compare the records on is explicitly specified in the
/// comparer's constructor.
/// </summary>
public class FieldSubsetEqualityComparer<TRecord> : IEqualityComparer<TRecord> where TRecord : class, IBqlTable, new()
{
  private PXCache _cache;
  private IEnumerable<string> _fieldNames;

  public FieldSubsetEqualityComparer(PXCache cache, IEnumerable<string> fieldNames)
  {
    if (cache == null)
      throw new ArgumentNullException(nameof (cache));
    if (fieldNames == null)
      throw new ArgumentNullException(nameof (fieldNames));
    this._cache = cache;
    this._fieldNames = fieldNames;
  }

  public FieldSubsetEqualityComparer(PXCache cache, params string[] fieldNames)
    : this(cache, (IEnumerable<string>) fieldNames)
  {
  }

  public FieldSubsetEqualityComparer(PXCache cache, IEnumerable<Type> fields)
    : this(cache, fields.Select<Type, string>((Func<Type, string>) (field => field.Name)))
  {
  }

  public FieldSubsetEqualityComparer(PXCache cache, params Type[] fields)
    : this(cache, (IEnumerable<Type>) fields)
  {
  }

  private IEnumerable<object> CollectFieldValues(TRecord record)
  {
    return this._fieldNames.Select<string, object>((Func<string, object>) (fieldName => this._cache.GetValue((object) record, fieldName)));
  }

  public bool Equals(TRecord first, TRecord second)
  {
    if ((object) first == null)
      throw new ArgumentNullException(nameof (first));
    if ((object) second == null)
      throw new ArgumentNullException(nameof (second));
    foreach (Tuple<object, object> tuple in this.CollectFieldValues(first).Zip<object, object, Tuple<object, object>>(this.CollectFieldValues(second), (Func<object, object, Tuple<object, object>>) ((x, y) => Tuple.Create<object, object>(x, y))))
    {
      if (!object.Equals(tuple.Item1, tuple.Item2))
        return false;
    }
    return true;
  }

  public int GetHashCode(TRecord record)
  {
    return (object) record != null ? HashcodeCombiner.Combine(this.CollectFieldValues(record)) : throw new ArgumentNullException(nameof (record));
  }
}

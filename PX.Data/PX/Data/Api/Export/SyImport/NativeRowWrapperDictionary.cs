// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Export.SyImport.NativeRowWrapperDictionary
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Api.Export.SyImport;

public class NativeRowWrapperDictionary : 
  IDictionary<string, List<NativeRowWrapperDictionary>>,
  ICollection<KeyValuePair<string, List<NativeRowWrapperDictionary>>>,
  IEnumerable<KeyValuePair<string, List<NativeRowWrapperDictionary>>>,
  IEnumerable
{
  private readonly IDictionary<string, List<NativeRowWrapperDictionary>> _tail = (IDictionary<string, List<NativeRowWrapperDictionary>>) new Dictionary<string, List<NativeRowWrapperDictionary>>();

  internal NativeRowWrapper NativeRowWrapper { get; }

  public NativeRowWrapperDictionary(NativeRowWrapper nativeRowWrapper)
  {
    this.NativeRowWrapper = nativeRowWrapper;
  }

  public IEnumerable<FieldValue> GetFieldValues()
  {
    foreach (FieldValue fieldValue in (IEnumerable<FieldValue>) this.NativeRowWrapper?.FieldValues ?? Enumerable.Empty<FieldValue>())
      yield return fieldValue;
    foreach (FieldValue fieldValue in this._tail.Values.SelectMany<List<NativeRowWrapperDictionary>, FieldValue>((Func<List<NativeRowWrapperDictionary>, IEnumerable<FieldValue>>) (g =>
    {
      if (g == null)
        return (IEnumerable<FieldValue>) null;
      return g.FirstOrDefault<NativeRowWrapperDictionary>()?.GetFieldValues();
    })))
      yield return fieldValue;
  }

  public IEnumerator<KeyValuePair<string, List<NativeRowWrapperDictionary>>> GetEnumerator()
  {
    return this._tail.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => this._tail.GetEnumerator();

  public void Add(
    KeyValuePair<string, List<NativeRowWrapperDictionary>> item)
  {
    this._tail.Add(item);
  }

  public void Clear() => this._tail.Clear();

  public bool Contains(
    KeyValuePair<string, List<NativeRowWrapperDictionary>> item)
  {
    List<NativeRowWrapperDictionary> wrapperDictionaryList;
    return this.TryGetValue(item.Key, out wrapperDictionaryList) && wrapperDictionaryList == item.Value;
  }

  public void CopyTo(
    KeyValuePair<string, List<NativeRowWrapperDictionary>>[] array,
    int arrayIndex)
  {
    this._tail.CopyTo(array, arrayIndex);
  }

  public bool Remove(
    KeyValuePair<string, List<NativeRowWrapperDictionary>> item)
  {
    return this.Remove(item.Key);
  }

  public int Count => this._tail.Count;

  public bool IsReadOnly => this._tail.IsReadOnly;

  public bool ContainsKey(string key) => this._tail.ContainsKey(key);

  public void Add(string key, List<NativeRowWrapperDictionary> value) => this._tail.Add(key, value);

  public bool Remove(string key) => this._tail.Remove(key);

  public bool TryGetValue(string key, out List<NativeRowWrapperDictionary> value)
  {
    return this._tail.TryGetValue(key, out value);
  }

  public List<NativeRowWrapperDictionary> this[string key]
  {
    get
    {
      List<NativeRowWrapperDictionary> wrapperDictionaryList;
      if (this.TryGetValue(key, out wrapperDictionaryList))
        return wrapperDictionaryList;
      throw new KeyNotFoundException();
    }
    set => this._tail[key] = value;
  }

  public ICollection<string> Keys => this._tail.Keys;

  public ICollection<List<NativeRowWrapperDictionary>> Values => this._tail.Values;
}

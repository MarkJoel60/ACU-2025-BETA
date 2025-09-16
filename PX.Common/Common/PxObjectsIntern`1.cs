// Decompiled with JetBrains decompiler
// Type: PX.Common.PxObjectsIntern`1
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Common;

public class PxObjectsIntern<TValue> where TValue : class
{
  private static Dictionary<int, WeakReference<TValue>> \u0002 = new Dictionary<int, WeakReference<TValue>>();
  private static Dictionary<int, object> \u000E = new Dictionary<int, object>();
  private static object \u0006 = new object();
  private bool \u0008;

  public PxObjectsIntern(bool useInternalHash = false) => this.\u0008 = useInternalHash;

  public bool TryIntern(TValue value, out TValue returnValue, Dictionary<TValue, TValue> cache)
  {
    returnValue = default (TValue);
    if ((object) value == null)
      return false;
    if (cache != null && cache.ContainsKey(value))
    {
      returnValue = cache[value] ?? value;
      return true;
    }
    int num = this.TryIntern(value, out returnValue) ? 1 : 0;
    if (num != 0 && cache != null)
      cache[value] = returnValue;
    returnValue = returnValue ?? value;
    return num != 0;
  }

  public bool TryIntern(TValue value, out TValue returnValue)
  {
    returnValue = default (TValue);
    if ((object) value == null)
      return false;
    object obj1 = this.\u0002(value);
    int key = this.\u000E(obj1) * 397 + value.GetType().FullName.GetHashCode();
    lock (PxObjectsIntern<TValue>.\u0006)
    {
      WeakReference<TValue> weakReference;
      TValue target;
      if (!PxObjectsIntern<TValue>.\u0002.TryGetValue(key, out weakReference) || !weakReference.TryGetTarget(out target))
      {
        PxObjectsIntern<TValue>.\u0002[key] = new WeakReference<TValue>(value);
        if (!this.\u0008)
          PxObjectsIntern<TValue>.\u000E[key] = obj1;
        returnValue = value;
        return true;
      }
      object obj2 = (object) target;
      if (!this.\u0008)
        PxObjectsIntern<TValue>.\u000E.TryGetValue(key, out obj2);
      if ((object) target == null || !this.\u0008(obj1, obj2))
        return false;
      returnValue = target;
      return true;
    }
  }

  private object \u0002(TValue _param1)
  {
    return this.\u0008 && ((object) _param1 is IDictionary || (object) _param1 is IList) ? (object) _param1 : (object) PXReflectionSerializer.GetHashCode((object) _param1);
  }

  private int \u000E(object _param1)
  {
    if (this.\u0008)
    {
      switch (_param1)
      {
        case IDictionary _:
          return this.\u0006((IDictionary) _param1);
        case IList _:
          return this.\u000F((IList) _param1);
      }
    }
    return this.GetByteArrayHashCode((byte[]) _param1);
  }

  private int \u0006(IDictionary _param1)
  {
    int num = 37;
    foreach (object key in _param1.Keys.ToArray<object>())
    {
      object obj = _param1[key];
      num = (num * 397 + key.GetHashCode()) * 397 + obj.GetHashCode();
    }
    return num;
  }

  private bool \u0008(object _param1, object _param2)
  {
    if (_param1 == _param2)
      return true;
    if (this.\u0008)
    {
      switch (_param1)
      {
        case IDictionary _ when _param2 is IDictionary:
          return this.\u0003((IDictionary) _param1, (IDictionary) _param2);
        case IList _ when _param2 is IList:
          return this.\u0005((IList) _param1, (IList) _param2);
      }
    }
    return this.ByteArrayEquals((byte[]) _param1, (byte[]) _param2);
  }

  public bool ByteArrayEquals(byte[] x, byte[] y)
  {
    if (x == null && y == null)
      return true;
    if (x == null || y == null || x.Length != y.Length)
      return false;
    for (int index = 0; index < x.Length; ++index)
    {
      if ((int) x[index] != (int) y[index])
        return false;
    }
    return true;
  }

  private bool \u0003(IDictionary _param1, IDictionary _param2)
  {
    if (_param1.Count != _param2.Count)
      return false;
    object[] array1 = _param1.Keys.ToArray<object>();
    object[] array2 = _param2.Keys.ToArray<object>();
    for (int index = 0; index < array1.Length; ++index)
    {
      object key1 = array1[index];
      object key2 = array2[index];
      if (!key1.Equals(key2) || !_param1[key1].Equals(_param2[key2]))
        return false;
    }
    return true;
  }

  public int GetByteArrayHashCode(byte[] arr)
  {
    int byteArrayHashCode = 37;
    for (int index = 0; index < arr.Length; ++index)
      byteArrayHashCode = byteArrayHashCode * 397 + (int) arr[index];
    return byteArrayHashCode;
  }

  private int \u000F(IList _param1)
  {
    int num = 37;
    for (int index = 0; index < _param1.Count; ++index)
      num = num * 397 + _param1[index].GetHashCode();
    return num;
  }

  private bool \u0005(IList _param1, IList _param2)
  {
    if (_param1.Count != _param2.Count)
      return false;
    for (int index = 0; index < _param1.Count; ++index)
    {
      if (!_param1[index].Equals(_param2[index]))
        return false;
    }
    return true;
  }
}

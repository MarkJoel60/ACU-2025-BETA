// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.PXRefNoteBaseAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Objects.Common;

/// <summary>
/// The class allows to display references to various documents without additional queries
/// to <see cref="T:PX.Data.Note" /> and other tables and also to create appropriate navigation links.
/// </summary>
public class PXRefNoteBaseAttribute : PXRefNoteAttribute
{
  public virtual object GetEntityRowID(PXCache cache, object[] keys)
  {
    return PXRefNoteBaseAttribute.GetEntityRowID(cache, keys, ", ");
  }

  public static object GetEntityRowID(PXCache cache, object[] keys, string separator)
  {
    StringBuilder stringBuilder = new StringBuilder();
    int num = 0;
    foreach (string key1 in (IEnumerable<string>) cache.Keys)
    {
      if (num < keys.Length)
      {
        object key2 = keys[num++];
        cache.RaiseFieldSelecting(key1, (object) null, ref key2, true);
        if (key2 != null)
        {
          if (stringBuilder.Length != 0)
            stringBuilder.Append(separator);
          stringBuilder.Append(key2.ToString().TrimEnd());
        }
      }
      else
        break;
    }
    return (object) stringBuilder.ToString();
  }

  public class PXLinkState(object value) : PXStringState(value)
  {
    protected object[] _keys;
    protected Type _target;

    public object[] keys => this._keys;

    public Type target => this._target;

    public static PXFieldState CreateInstance(object value, Type target, object[] keys)
    {
      switch (value)
      {
        case PXRefNoteBaseAttribute.PXLinkState instance1:
label_4:
          ((PXFieldState) instance1)._DataType = typeof (string);
          if (target != (Type) null)
            instance1._target = target;
          if (keys != null)
            instance1._keys = keys;
          return (PXFieldState) instance1;
        case PXFieldState instance2:
          if (instance2.DataType != typeof (object) && instance2.DataType != typeof (string))
            return instance2;
          goto default;
        default:
          instance1 = new PXRefNoteBaseAttribute.PXLinkState(value);
          goto label_4;
      }
    }
  }
}

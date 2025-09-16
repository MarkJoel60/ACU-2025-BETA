// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PXForeignSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
public class PXForeignSelectorAttribute : PXEventSubscriberAttribute
{
  protected Type _SearchType;

  public PXForeignSelectorAttribute(Type SearchType)
  {
    this._SearchType = SearchType != (Type) null && typeof (IBqlField).IsAssignableFrom(SearchType) ? SearchType : throw new PXArgumentException();
  }

  protected virtual object GetValueExt(PXCache sender, object item)
  {
    object obj = sender.GetValue(item, this._FieldOrdinal);
    object objB = obj;
    PXCache cach = sender.Graph.Caches[BqlCommand.GetItemType(this._SearchType)];
    if (cach != null && obj != null)
    {
      cach.RaiseFieldSelecting(this._SearchType.Name, (object) null, ref obj, true);
      if (obj is PXFieldState && object.Equals(((PXFieldState) obj).Value, objB))
        return (object) null;
      if (obj != null)
        return (object) obj.ToString().TrimEnd();
    }
    return (object) null;
  }

  public static object GetValueExt(PXCache cache, object data, string name)
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(name))
    {
      object valueExt;
      if (subscriberAttribute is PXForeignSelectorAttribute && (valueExt = ((PXForeignSelectorAttribute) subscriberAttribute).GetValueExt(cache, data)) != null)
        return valueExt;
    }
    object valueExt1 = cache.GetValueExt(data, name);
    return valueExt1 != null ? (object) valueExt1.ToString().TrimEnd() : (object) null;
  }

  public static object GetValueExt<Field>(PXCache cache, object data) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly<Field>())
    {
      object valueExt;
      if (subscriberAttribute is PXForeignSelectorAttribute && (valueExt = ((PXForeignSelectorAttribute) subscriberAttribute).GetValueExt(cache, data)) != null)
        return valueExt;
    }
    object valueExt1 = cache.GetValueExt<Field>(data);
    return valueExt1 != null ? (object) valueExt1.ToString().TrimEnd() : (object) null;
  }
}

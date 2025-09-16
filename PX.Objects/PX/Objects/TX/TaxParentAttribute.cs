// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxParentAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.TX;

public class TaxParentAttribute : PXParentAttribute
{
  public TaxParentAttribute(Type selectParent)
    : base(selectParent)
  {
    throw new PXArgumentException();
  }

  public static void NewChild(PXCache cache, object parentrow, Type ParentType, out object child)
  {
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes((string) null))
    {
      if (attribute is PXParentAttribute && ((PXParentAttribute) attribute).ParentType.IsAssignableFrom(ParentType))
      {
        Type itemType = cache.GetItemType();
        Type firstTable = ((PXParentAttribute) attribute).GetParentSelect(cache).BqlSelect.GetFirstTable();
        BqlCommand bqlSelect = ((PXParentAttribute) attribute).GetChildrenSelect(cache).BqlSelect;
        IBqlParameter[] parameters = bqlSelect.GetParameters();
        Type[] referencedFields = bqlSelect.GetReferencedFields(false);
        child = Activator.CreateInstance(itemType);
        PXCache cach = cache.Graph.Caches[firstTable];
        for (int index = 0; index < Math.Min(parameters.Length, referencedFields.Length); ++index)
        {
          Type referencedType = parameters[index].GetReferencedType();
          object obj = cach.GetValue(parentrow, referencedType.Name);
          cache.SetValue(child, referencedFields[index].Name, obj);
        }
        return;
      }
    }
    child = (object) null;
  }

  public static object ParentSelect(PXCache cache, object row, Type ParentType)
  {
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes((string) null))
    {
      if (attribute is PXParentAttribute && ((PXParentAttribute) attribute).ParentType.IsAssignableFrom(ParentType))
        return ((PXParentAttribute) attribute).GetParentSelect(cache).SelectSingleBound(new object[1]
        {
          row
        }, Array.Empty<object>());
    }
    return (object) null;
  }

  public static List<object> ChildSelect(PXCache cache, object row)
  {
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes((string) null))
    {
      if (attribute is PXParentAttribute)
        return ((PXParentAttribute) attribute).GetChildrenSelect(cache).SelectMultiBound(new object[1]
        {
          row
        }, Array.Empty<object>());
    }
    return (List<object>) null;
  }

  public static List<object> ChildSelect(PXCache cache, object row, Type ParentType)
  {
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes((string) null))
    {
      if (attribute is PXParentAttribute && ((PXParentAttribute) attribute).ParentType.IsAssignableFrom(ParentType))
        return ((PXParentAttribute) attribute).GetChildrenSelect(cache).SelectMultiBound(new object[1]
        {
          row
        }, Array.Empty<object>());
    }
    return (List<object>) null;
  }
}

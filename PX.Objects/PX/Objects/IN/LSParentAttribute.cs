// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.LSParentAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

public class LSParentAttribute(Type selectParent) : PXParentAttribute(selectParent)
{
  public static object SelectParent(PXCache cache, object row, Type ParentType)
  {
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>();
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly((string) null))
    {
      if (subscriberAttribute is PXParentAttribute)
      {
        if (((PXParentAttribute) subscriberAttribute).ParentType == ParentType)
          subscriberAttributeList.Insert(0, subscriberAttribute);
        else if (ParentType.IsSubclassOf(((PXParentAttribute) subscriberAttribute).ParentType))
          subscriberAttributeList.Add(subscriberAttribute);
      }
    }
    if (subscriberAttributeList.Count <= 0)
      return (object) null;
    PXView parentSelect = ((PXParentAttribute) subscriberAttributeList[0]).GetParentSelect(cache);
    if (!(parentSelect.CacheGetItemType() == ParentType) && !ParentType.IsAssignableFrom(parentSelect.CacheGetItemType()))
      return (object) null;
    parentSelect.Clear();
    return parentSelect.SelectSingleBound(new object[1]
    {
      row
    }, Array.Empty<object>());
  }
}

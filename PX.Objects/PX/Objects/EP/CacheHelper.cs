// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.CacheHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.EP;

public static class CacheHelper
{
  public static object GetValue<Field>(PXGraph graph, object data) where Field : IBqlField
  {
    return CacheHelper.GetValue(graph, data, typeof (Field));
  }

  public static object GetValue(PXGraph graph, object data, Type field)
  {
    return graph.Caches[BqlCommand.GetItemType(field)].GetValue(data, field.Name);
  }

  public static object GetCurrentValue(PXGraph graph, Type type)
  {
    PXCache cach = graph.Caches[BqlCommand.GetItemType(type)];
    return cach?.GetValue(cach.Current, type.Name);
  }

  public static object GetCurrentRecord(PXGraph graph, Type fieldType)
  {
    return graph.Caches[fieldType]?.Current;
  }
}

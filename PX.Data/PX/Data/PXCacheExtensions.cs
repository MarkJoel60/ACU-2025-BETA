// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCacheExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <exclude />
internal static class PXCacheExtensions
{
  private static MethodInfo _method = typeof (PXCache).GetMethod("ToChildEntity", BindingFlags.Instance | BindingFlags.NonPublic);

  public static object ToChildEntity(this PXCache graph, System.Type parent, object item)
  {
    if (parent == (System.Type) null)
      throw new ArgumentNullException();
    return PXCacheExtensions._method.MakeGenericMethod(parent).Invoke((object) graph, new object[1]
    {
      item
    });
  }
}

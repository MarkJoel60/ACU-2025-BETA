// Decompiled with JetBrains decompiler
// Type: PX.Data.BqlTableExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Data;

internal static class BqlTableExtensions
{
  internal static bool TryGetExtensions(this IBqlTable item, [NotNullWhen(true)] out PXCacheExtension[]? extensions)
  {
    return item.GetBqlTableSystemData().Extensions.TryGetValue(out extensions);
  }

  internal static void SetExtensions(this IBqlTable item, PXCacheExtension[] extensions)
  {
    item.GetBqlTableSystemData().Extensions = (PXBqlTableSystemData.CollectionValue<PXCacheExtension[]>) extensions;
  }

  internal static void ClearExtensions(this IBqlTable item)
  {
    item.GetBqlTableSystemData().Extensions.Remove();
  }
}

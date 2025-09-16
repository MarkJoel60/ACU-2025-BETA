// Decompiled with JetBrains decompiler
// Type: PX.Data.ProviderKeySuffixSlot
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Context;
using System;

#nullable disable
namespace PX.Data;

internal static class ProviderKeySuffixSlot
{
  private const string SlotName = "CompanyMaintenance";

  internal static void Set(Guid value) => ProviderKeySuffixSlot.Set(SlotStore.Instance, value);

  internal static void Set(ISlotStore slots, Guid value)
  {
    slots.Set("CompanyMaintenance", (object) value);
  }

  internal static void Clear() => ProviderKeySuffixSlot.Clear(SlotStore.Instance);

  internal static void Clear(ISlotStore slots) => slots.Remove("CompanyMaintenance");

  internal static Guid? Get() => PXContext.GetSlot<Guid?>("CompanyMaintenance");

  internal static bool NotSet() => !ProviderKeySuffixSlot.Get().HasValue;
}

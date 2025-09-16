// Decompiled with JetBrains decompiler
// Type: PX.Common.Context.SlotStore
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Web;

#nullable disable
namespace PX.Common.Context;

[PXInternalUseOnly]
public static class SlotStore
{
  private static ISlotStorageProvider \u0002()
  {
    HttpContext current = HttpContext.Current;
    return current == null ? (ISlotStorageProvider) (AsyncLocalSlotStorageProvider.TryGet() ?? (SlotStorageProviderBase) CallContextSlotStorageProvider.Get()) : HttpContextSlotStorageProvider.Get(current);
  }

  internal static IDisposableSlotStorageProvider AsyncLocal()
  {
    return AsyncLocalSlotStorageProvider.Activate();
  }

  internal static ISlotStore Instance => (ISlotStore) SlotStore.\u0002();

  internal static ISlotStorageProvider Provider => SlotStore.\u0002();

  internal static ISlotStore Slots(this HttpContext _param0)
  {
    return (ISlotStore) HttpContextSlotStorageProvider.Get(_param0);
  }

  [PXInternalUseOnly]
  public static ISlotStore Slots(this HttpContextBase context)
  {
    return (ISlotStore) HttpContextSlotStorageProvider.Get(context);
  }

  internal static void Remove(Func<string, object, bool> _param0)
  {
    SlotStore.\u0002().Remove(_param0);
  }

  internal static void ClearAll() => SlotStore.\u0002().Clear();
}

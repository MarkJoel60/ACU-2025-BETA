// Decompiled with JetBrains decompiler
// Type: PX.SP.PXPortalContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Context;
using PX.Data;

#nullable enable
namespace PX.SP;

internal static class PXPortalContext
{
  private const string PXPortalSlot = "PXPortal";

  internal static PortalInfo? GetPortal(this ISlotStore slots)
  {
    return slots.Get<PXPortalContext.PortalSlot>("PXPortal")?.PortalInfo;
  }

  internal static void SetPortal(this ISlotStore slots, PortalInfo portalInfo)
  {
    slots.Set("PXPortal", (object) new PXPortalContext.PortalSlot(portalInfo));
  }

  internal static PortalInfo? GetPortal() => SlotStore.Instance.GetPortal();

  /// <summary>
  /// Defines Portal state that also should be propagated to long running operations.
  /// </summary>
  [PXContextCopyingRequired]
  /// <summary>
  /// Defines Portal state that also should be propagated to long running operations.
  /// </summary>
  private sealed class PortalSlot(PortalInfo portalInfo)
  {
    public PortalInfo? PortalInfo { get; } = portalInfo;
  }
}

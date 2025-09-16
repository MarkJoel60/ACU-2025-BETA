// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.Descriptor.InventoryItemComponentInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.IN;

#nullable disable
namespace PX.Objects.DR.Descriptor;

public struct InventoryItemComponentInfo
{
  public InventoryItem Item { get; set; }

  public INComponent Component { get; set; }

  public DRDeferredCode DeferralCode { get; set; }
}

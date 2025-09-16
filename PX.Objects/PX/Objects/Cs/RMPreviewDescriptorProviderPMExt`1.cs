// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMPreviewDescriptorProviderPMExt`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;
using PX.Data;

#nullable disable
namespace PX.Objects.CS;

public abstract class RMPreviewDescriptorProviderPMExt<TGraph> : 
  RMPreviewDescriptorProviderExt<TGraph>
  where TGraph : PXGraph
{
  public static RMPreviewAttribute.PreviewItemDescriptor[] GetPreviewDescriptionStatic()
  {
    return new RMPreviewAttribute.PreviewItemDescriptor[5]
    {
      new RMPreviewAttribute.PreviewItemDescriptor("Account Group", "StartAccountGroup", "EndAccountGroup", (object) null),
      new RMPreviewAttribute.PreviewItemDescriptor("Project", "StartProject", "EndProject", (object) null),
      new RMPreviewAttribute.PreviewItemDescriptor("Project Task", "StartProjectTask", "EndProjectTask", (object) null),
      new RMPreviewAttribute.PreviewItemDescriptor("Inventory", "StartInventory", "EndInventory", (object) null),
      new RMPreviewAttribute.PreviewItemDescriptor("Amount Type", "AmountType", (string) null, (object) null)
    };
  }

  public delegate RMPreviewAttribute.PreviewItemDescriptor[] GetPreviewDescriptionDelegate() where TGraph : PXGraph;
}

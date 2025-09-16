// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMRowSetPreviewDescriptorProviderGL
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;

#nullable disable
namespace PX.Objects.CS;

public class RMRowSetPreviewDescriptorProviderGL : RMPreviewDescriptorProviderGLExt<RMRowSetMaint>
{
  public virtual RMPreviewAttribute.PreviewItemDescriptor[] GetPreviewDescription(
    string reportType,
    RMPreviewAttribute.PreviewItemDescriptor[] description)
  {
    return reportType != "GL" || description != null ? description : RMPreviewDescriptorProviderGLExt<RMRowSetMaint>.GetPreviewDescriptionStatic();
  }
}

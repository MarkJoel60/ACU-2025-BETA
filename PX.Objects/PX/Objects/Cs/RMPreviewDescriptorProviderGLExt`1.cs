// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMPreviewDescriptorProviderGLExt`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;
using PX.Data;

#nullable disable
namespace PX.Objects.CS;

public abstract class RMPreviewDescriptorProviderGLExt<TGraph> : 
  RMPreviewDescriptorProviderExt<TGraph>
  where TGraph : PXGraph
{
  public static RMPreviewAttribute.PreviewItemDescriptor[] GetPreviewDescriptionStatic()
  {
    return new RMPreviewAttribute.PreviewItemDescriptor[10]
    {
      new RMPreviewAttribute.PreviewItemDescriptor("Ledger", "LedgerID", (string) null, (object) null),
      new RMPreviewAttribute.PreviewItemDescriptor("Account Class", "AccountClassID", (string) null, (object) null),
      new RMPreviewAttribute.PreviewItemDescriptor("Acc.", "StartAccount", "EndAccount", (object) null),
      new RMPreviewAttribute.PreviewItemDescriptor("Sub.", "StartSub", "EndSub", (object) null),
      new RMPreviewAttribute.PreviewItemDescriptor("", "AmountType", (string) null, (object) (short) 0),
      new RMPreviewAttribute.PreviewItemDescriptor("Period", "StartPeriod", "EndPeriod", (object) null),
      new RMPreviewAttribute.PreviewItemDescriptor("Company", "OrganizationID", (string) null, (object) null),
      new RMPreviewAttribute.PreviewItemDescriptor("Branch", "StartBranch", "EndBranch", (object) null),
      new RMPreviewAttribute.PreviewItemDescriptor("Offset (Year)", "StartPeriodYearOffset", "EndPeriodYearOffset", (object) null),
      new RMPreviewAttribute.PreviewItemDescriptor("Offset (Period)", "StartPeriodOffset", "EndPeriodOffset", (object) null)
    };
  }

  public delegate RMPreviewAttribute.PreviewItemDescriptor[] GetPreviewDescriptionDelegate() where TGraph : PXGraph;
}

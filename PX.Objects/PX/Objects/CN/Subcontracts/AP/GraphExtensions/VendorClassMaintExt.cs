// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.AP.GraphExtensions.VendorClassMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CN.Common.Descriptor.Attributes;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CN.Subcontracts.AP.GraphExtensions;

public class VendorClassMaintExt : PXGraphExtension<VendorClassMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXMergeAttributes]
  [ConstructionReportSelector]
  protected virtual void _(
    Events.CacheAttached<NotificationSource.reportID> e)
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.AP.GraphExtensions.VendorMaintLienWaiverExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CN.Compliance.AP.CacheExtensions;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CN.Compliance.AP.GraphExtensions;

public class VendorMaintLienWaiverExtension : PXGraphExtension<VendorMaint>
{
  public PXSetup<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup> LienWaiverSetup;

  public virtual void _(Events.FieldUpdated<Vendor.vendorClassID> args)
  {
    if (!(args.Row is Vendor row))
      return;
    VendorExtension extension = PXCache<Vendor>.GetExtension<VendorExtension>(row);
    object obj;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<Vendor.vendorClassID>>) args).Cache.RaiseFieldDefaulting<VendorExtension.shouldGenerateLienWaivers>((object) row, ref obj);
    bool? nullable = obj as bool?;
    extension.ShouldGenerateLienWaivers = nullable;
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.SOShipmentEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;
using PX.Objects.SO.Models;
using System;

#nullable disable
namespace PX.Objects.PM;

public class SOShipmentEntryExt : PXGraphExtension<UpdateInventoryExtension, SOShipmentEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.UpdateInventoryExtension.PostShipment(PX.Objects.SO.Models.PostShipmentArgs)" />
  /// .
  [PXOverride]
  public virtual void PostShipment(PostShipmentArgs args, Action<PostShipmentArgs> baseMethod)
  {
    INRegisterEntryBase inRegisterEntry = args.INRegisterEntry;
    PX.Objects.SO.SOShipment soShipment = PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Search<PX.Objects.SO.SOShipment.shipmentNbr>((object) args.ShipmentNbr, Array.Empty<object>()));
    if (soShipment != null && soShipment.ShipmentType == "T")
    {
      INTransferEntryExt extension = ((PXGraph) inRegisterEntry).GetExtension<INTransferEntryExt>();
      if (extension == null)
        return;
      extension.IsShipmentPosting = true;
      try
      {
        baseMethod(args);
      }
      finally
      {
        extension.IsShipmentPosting = false;
      }
    }
    else
    {
      INIssueEntryExt extension = ((PXGraph) inRegisterEntry).GetExtension<INIssueEntryExt>();
      if (extension == null)
        return;
      extension.IsShipmentPosting = true;
      try
      {
        baseMethod(args);
      }
      finally
      {
        extension.IsShipmentPosting = false;
      }
    }
  }
}

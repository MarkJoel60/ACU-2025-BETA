// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderShipmentProcessExt.ReleaseRelatedItemHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.RelatedItems;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderShipmentProcessExt;

public class ReleaseRelatedItemHistory : ReleaseRelatedItemHistory<SOOrderShipmentProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.relatedItems>();

  [PXOverride]
  public virtual void OnInvoiceReleased(
    PX.Objects.AR.ARRegister ardoc,
    List<PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder>> orderShipments,
    Action<PX.Objects.AR.ARRegister, List<PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder>>> baseImpl)
  {
    baseImpl(ardoc, orderShipments);
    if (PXAccess.FeatureInstalled<FeaturesSet.advancedSOInvoices>())
      this.ReleaseRelatedItemHistoryFromInvoice(ardoc);
    if (orderShipments.Any<PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder>>())
      this.ReleaseRelatedItemHistoryFromOrder(ardoc);
    if (!((PXGraph) this.Base).IsDirty)
      return;
    ((PXAction) this.Base.Save).Press();
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PO.GraphExtensions.PoExternalTaxCalcExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CN.Subcontracts.PO.GraphExtensions;

public class PoExternalTaxCalcExt : PXGraphExtension<POExternalTaxCalc>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public IEnumerable Items()
  {
    return (IEnumerable) ((PXSelectBase<POOrder>) new PXProcessingJoin<POOrder, InnerJoin<PX.Objects.TX.TaxZone, On<PX.Objects.TX.TaxZone.taxZoneID, Equal<POOrder.taxZoneID>>>, Where<PX.Objects.TX.TaxZone.isExternal, Equal<True>, And<POOrder.isTaxValid, Equal<False>, And<POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>>>>((PXGraph) this.Base)).Select(Array.Empty<object>());
  }
}

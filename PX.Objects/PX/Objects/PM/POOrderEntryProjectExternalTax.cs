// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.POOrderEntryProjectExternalTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.PM;

public class POOrderEntryProjectExternalTax : PXGraphExtension<POOrderEntryExternalTax, POOrderEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>() && PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() && new ProjectSettingsManager().CalculateProjectSpecificTaxes;
  }

  [PXOverride]
  public virtual IAddressLocation? GetToAddress(
    POOrder? order,
    POLine? line,
    Func<POOrder?, POLine?, IAddressLocation?> baseMethod)
  {
    if (line != null)
    {
      int? projectId;
      int num;
      if (order == null)
      {
        num = 1;
      }
      else
      {
        projectId = order.ProjectID;
        num = !projectId.HasValue ? 1 : 0;
      }
      if (num == 0)
      {
        projectId = order.ProjectID;
        int? nullable = ProjectDefaultAttribute.NonProject();
        if (!(projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue) && !(order.OrderType != "RO") && order.ShipAddressID.HasValue)
          return (IAddressLocation) POShipAddress.PK.Find((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, order.ShipAddressID) ?? baseMethod(order, line);
      }
    }
    return baseMethod(order, line);
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN;

#nullable disable
namespace PX.Objects.SO;

public sealed class SOShipmentVisibilityRestriction : PXCacheExtension<SOShipment>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [RestrictCustomerByUserBranches(typeof (PX.Objects.AR.Customer.cOrgBAccountID), typeof (Or<Current<SOShipment.shipmentType>, Equal<INDocType.transfer>>))]
  public int? CustomerID { get; set; }
}

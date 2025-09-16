// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCosts.Attributes.POLandedCostPayToVendorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.PO.LandedCosts.Attributes;

[PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.payToVendorID, IsNull, Or<PX.Objects.AP.Vendor.bAccountID, Equal<Current<POLandedCostDoc.vendorID>>>>), "The vendor '{0}' cannot be specified as a pay-to vendor because it is already configured as a supplied-by vendor.", new System.Type[] {typeof (PX.Objects.AP.Vendor.acctCD)})]
[PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.taxAgency, NotEqual<True>, Or<PX.Objects.AP.Vendor.bAccountID, Equal<Current<POLandedCostDoc.vendorID>>>>), "The vendor '{0}' cannot be specified as a pay-to vendor because it is configured as a Tax Agency on the Vendors (AP303000) form.", new System.Type[] {typeof (PX.Objects.AP.Vendor.acctCD)})]
[PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.vendor1099, NotEqual<True>, Or<PX.Objects.AP.Vendor.bAccountID, Equal<Current<POLandedCostDoc.vendorID>>>>), "The vendor '{0}' cannot be specified as a pay-to vendor because it is configured as a 1099 Vendor on the Vendors (AP303000) form.", new System.Type[] {typeof (PX.Objects.AP.Vendor.acctCD)})]
[VerndorNonEmployeeOrOrganizationRestrictor]
public class POLandedCostPayToVendorAttribute : BasePayToVendorAttribute
{
  public POLandedCostPayToVendorAttribute()
    : base(typeof (Search<BAccountR.bAccountID, Where<True, Equal<True>>>))
  {
  }
}

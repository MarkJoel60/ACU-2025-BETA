// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.POReceiptPayToVendorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.AP;

[PXRestrictor(typeof (Where<Vendor.payToVendorID, IsNull, Or<Vendor.bAccountID, Equal<Current<PX.Objects.PO.POReceipt.vendorID>>>>), "The vendor '{0}' cannot be specified as a pay-to vendor because it is already configured as a supplied-by vendor.", new System.Type[] {typeof (Vendor.acctCD)})]
[PXRestrictor(typeof (Where<Vendor.taxAgency, NotEqual<True>, Or<Vendor.bAccountID, Equal<Current<PX.Objects.PO.POReceipt.vendorID>>>>), "The vendor '{0}' cannot be specified as a pay-to vendor because it is configured as a Tax Agency on the Vendors (AP303000) form.", new System.Type[] {typeof (Vendor.acctCD)})]
[PXRestrictor(typeof (Where<Vendor.vendor1099, NotEqual<True>, Or<Vendor.bAccountID, Equal<Current<PX.Objects.PO.POReceipt.vendorID>>>>), "The vendor '{0}' cannot be specified as a pay-to vendor because it is configured as a 1099 Vendor on the Vendors (AP303000) form.", new System.Type[] {typeof (Vendor.acctCD)})]
[VerndorNonEmployeeOrOrganizationRestrictor]
public class POReceiptPayToVendorAttribute : BasePayToVendorAttribute
{
  public POReceiptPayToVendorAttribute()
    : base(typeof (Search<BAccountR.bAccountID, Where<True, Equal<True>>>))
  {
  }
}

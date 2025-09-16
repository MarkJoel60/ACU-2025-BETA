// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POVendorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.PO;

/// <summary>
/// Specialized for POOrder version of the VendorAttribute, which defines a list of vendors, <br />
/// which may be used in the PO Order (for example, employee are filtered <br />
/// out for all order types except Transfer ) <br />
/// Depends upon POOrder current. <br />
/// <example>
/// [POVendor()]
/// </example>
/// </summary>
[PXDBInt]
[PXUIField]
[PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.vStatus, IsNull, Or<PX.Objects.AP.Vendor.vStatus, In3<VendorStatus.active, VendorStatus.oneTime, VendorStatus.holdPayments>>>), "The vendor status is '{0}'.", new System.Type[] {typeof (PX.Objects.AP.Vendor.vStatus)})]
public class POVendorAttribute : VendorAttribute
{
  public POVendorAttribute()
    : base(typeof (Search<BAccountR.bAccountID, Where<PX.Objects.AP.Vendor.type, NotEqual<BAccountType.employeeType>>>))
  {
  }
}

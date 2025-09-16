// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorActiveOrHoldPaymentsAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
/// A specialized version of the <see cref="T:PX.Objects.AP.VendorAttribute" />
/// allowing only selection of Active, One-Time, or Hold Payments
/// vendors.
/// </summary>
[PXDBInt]
[PXUIField(DisplayName = "Vendor", Visibility = PXUIVisibility.Visible)]
[PXRestrictor(typeof (Where<Vendor.vStatus, Equal<VendorStatus.active>, Or<Vendor.vStatus, Equal<VendorStatus.oneTime>, Or<Vendor.vStatus, Equal<VendorStatus.holdPayments>>>>), "The vendor status is '{0}'.", new System.Type[] {typeof (Vendor.vStatus)})]
public class VendorActiveOrHoldPaymentsAttribute : VendorAttribute
{
  public VendorActiveOrHoldPaymentsAttribute(System.Type search)
    : base(search)
  {
  }

  public VendorActiveOrHoldPaymentsAttribute()
  {
  }
}

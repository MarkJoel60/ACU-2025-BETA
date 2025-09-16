// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorActiveAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
/// This is a specialized version of the Vendor attribute.<br />
/// Displays only Active or OneTime vendors<br />
/// See VendorAttribute for description. <br />
/// </summary>
[PXDBInt]
[PXUIField(DisplayName = "Vendor", Visibility = PXUIVisibility.Visible)]
[PXRestrictor(typeof (Where<Vendor.vStatus, Equal<VendorStatus.active>, Or<Vendor.vStatus, Equal<VendorStatus.oneTime>>>), "The vendor status is '{0}'.", new System.Type[] {typeof (Vendor.vStatus)})]
public class VendorActiveAttribute : VendorAttribute
{
  public VendorActiveAttribute(System.Type search)
    : base(search)
  {
  }

  public VendorActiveAttribute()
  {
  }
}

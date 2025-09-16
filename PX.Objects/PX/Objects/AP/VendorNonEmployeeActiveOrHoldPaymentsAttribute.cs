// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorNonEmployeeActiveOrHoldPaymentsAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
/// This is a specialized version of the Vendor attribute.<br />
/// Displays only Active, OneTime or Hold Payments  vendors, filtering out Employees<br />
/// See VendorAttribute for description. <br />
/// </summary>
[PXDBInt]
[PXUIField(DisplayName = "Vendor", Visibility = PXUIVisibility.Visible)]
[PXRestrictor(typeof (Where<Vendor.type, NotEqual<BAccountType.employeeType>>), "Vendor can not be {0}.", new System.Type[] {typeof (Vendor.type)})]
public class VendorNonEmployeeActiveOrHoldPaymentsAttribute : VendorActiveOrHoldPaymentsAttribute
{
  public VendorNonEmployeeActiveOrHoldPaymentsAttribute(System.Type search)
    : base(search)
  {
  }

  public VendorNonEmployeeActiveOrHoldPaymentsAttribute()
  {
  }
}

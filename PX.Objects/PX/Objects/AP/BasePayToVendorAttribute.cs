// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.BasePayToVendorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.AP;

/// exclude
[PXUIField(DisplayName = "Pay-to Vendor", FieldClass = "VendorRelations")]
[PXRestrictor(typeof (Where<Vendor.vStatus, Equal<VendorStatus.active>, Or<Vendor.vStatus, Equal<VendorStatus.oneTime>, Or<Vendor.vStatus, Equal<VendorStatus.holdPayments>>>>), "The vendor status is '{0}'.", new System.Type[] {typeof (Vendor.vStatus)})]
[PXRestrictor(typeof (Where<Vendor.type, NotEqual<BAccountType.employeeType>>), "Vendor can not be {0}.", new System.Type[] {typeof (Vendor.type)})]
public class BasePayToVendorAttribute : VendorAttribute
{
  public BasePayToVendorAttribute()
  {
  }

  public BasePayToVendorAttribute(System.Type search)
    : base(search)
  {
  }
}

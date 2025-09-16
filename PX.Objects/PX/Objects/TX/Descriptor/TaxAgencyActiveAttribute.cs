// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.Descriptor.TaxAgencyActiveAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using System;

#nullable disable
namespace PX.Objects.TX.Descriptor;

/// <summary>Displays only Active or OneTime tax agency</summary>
[PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.taxAgency, Equal<True>, And<Where<PX.Objects.AP.Vendor.vStatus, Equal<VendorStatus.active>, Or<PX.Objects.AP.Vendor.vStatus, Equal<VendorStatus.oneTime>>>>>), "The tax agency status is '{0}'.", new Type[] {typeof (PX.Objects.AP.Vendor.vStatus)})]
public class TaxAgencyActiveAttribute : VendorAttribute
{
  public TaxAgencyActiveAttribute(Type search)
    : base(search)
  {
  }

  public TaxAgencyActiveAttribute()
  {
  }

  protected virtual void Initialize()
  {
    base.Initialize();
    this.DisplayName = "Tax Agency";
    this.DescriptionField = typeof (PX.Objects.AP.Vendor.acctName);
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCostVendorActiveAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.PO;

/// <summary>
/// Specialized for Landed Cost version of VendorAttribute.
/// Displayes only Vendors having LandedCostVendor = true.
/// Employee and non-active vendors are filtered out
/// <example>
/// [LandedCostVendorActive(Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof(Vendor.acctName), CacheGlobal = true, Filterable = true)]
/// </example>
/// </summary>
[PXDBInt]
[PXUIField]
[PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.landedCostVendor, Equal<boolTrue>>), "Vendor is not landed cost vendor.", new Type[] {})]
public class LandedCostVendorActiveAttribute : VendorNonEmployeeActiveAttribute
{
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.Mappers.LineEntitiesFields`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Discount.Mappers;

public class LineEntitiesFields<InventoryField, CustomerField, SiteField, BranchField, VendorField>(
  PXCache cache,
  object row) : LineEntitiesFields(cache, row)
  where InventoryField : IBqlField
  where CustomerField : IBqlField
  where SiteField : IBqlField
  where BranchField : IBqlField
  where VendorField : IBqlField
{
  public override Type GetField<T>()
  {
    if (typeof (T) == typeof (LineEntitiesFields.inventoryID))
      return typeof (InventoryField);
    if (typeof (T) == typeof (LineEntitiesFields.customerID))
      return typeof (CustomerField);
    if (typeof (T) == typeof (LineEntitiesFields.siteID))
      return typeof (SiteField);
    if (typeof (T) == typeof (LineEntitiesFields.branchID))
      return typeof (BranchField);
    return typeof (T) == typeof (LineEntitiesFields.vendorID) ? typeof (VendorField) : (Type) null;
  }

  public override int? InventoryID => (int?) this.Cache.GetValue<InventoryField>(this.MappedLine);

  public override int? CustomerID => (int?) this.Cache.GetValue<CustomerField>(this.MappedLine);

  public override int? SiteID => (int?) this.Cache.GetValue<SiteField>(this.MappedLine);

  public override int? BranchID => (int?) this.Cache.GetValue<BranchField>(this.MappedLine);

  public override int? VendorID => (int?) this.Cache.GetValue<VendorField>(this.MappedLine);
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.Mappers.LineEntitiesFields`6
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Common.Discount.Mappers;

public class LineEntitiesFields<InventoryField, CustomerField, SiteField, BranchField, VendorField, SuppliedByVendorField>(
  PXCache cache,
  object row) : 
  LineEntitiesFields<InventoryField, CustomerField, SiteField, BranchField, VendorField>(cache, row)
  where InventoryField : IBqlField
  where CustomerField : IBqlField
  where SiteField : IBqlField
  where BranchField : IBqlField
  where VendorField : IBqlField
  where SuppliedByVendorField : IBqlField
{
  public override int? VendorID
  {
    get
    {
      return (int?) this.Cache.GetValue<SuppliedByVendorField>(this.MappedLine) ?? (int?) this.Cache.GetValue<VendorField>(this.MappedLine);
    }
  }
}

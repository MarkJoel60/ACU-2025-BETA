// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.Mappers.LineEntitiesFields
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.Common.Discount.Mappers;

public abstract class LineEntitiesFields(
#nullable disable
PXCache cache, object row) : DiscountedLineMapperBase(cache, row)
{
  public virtual int? InventoryID { get; set; }

  public virtual int? CustomerID { get; set; }

  public virtual int? SiteID { get; set; }

  public virtual int? BranchID { get; set; }

  public virtual int? VendorID { get; set; }

  public static LineEntitiesFields GetMapFor<TLine>(TLine line, PXCache cache)
  {
    return (LineEntitiesFields) new LineEntitiesFields<LineEntitiesFields.inventoryID, LineEntitiesFields.customerID, LineEntitiesFields.siteID, LineEntitiesFields.branchID, LineEntitiesFields.vendorID, LineEntitiesFields.suppliedByVendorID>(cache, (object) line);
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LineEntitiesFields.inventoryID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LineEntitiesFields.customerID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LineEntitiesFields.siteID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LineEntitiesFields.branchID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LineEntitiesFields.vendorID>
  {
  }

  public abstract class suppliedByVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LineEntitiesFields.suppliedByVendorID>
  {
  }
}

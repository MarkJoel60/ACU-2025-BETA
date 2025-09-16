// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POVendorInventoryRepo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXHidden]
[PXProjection(typeof (Select5<POVendorInventory, LeftJoin<POVendorInventoryAll, On<POVendorInventory.inventoryID, Equal<POVendorInventoryAll.inventoryID>, And<IsNull<POVendorInventory.subItemID, int_1>, Equal<IsNull<POVendorInventoryAll.subItemID, int_1>>, And<POVendorInventory.vendorID, Equal<POVendorInventoryAll.vendorID>, And<POVendorInventoryAll.vendorLocationID, IsNull>>>>>, Aggregate<GroupBy<POVendorInventory.inventoryID, GroupBy<POVendorInventory.subItemID, GroupBy<POVendorInventory.vendorID, GroupBy<POVendorInventory.vendorLocationID>>>>>>))]
[Serializable]
public class POVendorInventoryRepo : POVendorInventory
{
  protected Decimal? _NullERQ;

  [PXDBQuantity(BqlField = typeof (POVendorInventoryAll.eRQ))]
  public virtual Decimal? NullERQ
  {
    get => this._NullERQ;
    set => this._NullERQ = value;
  }

  public new abstract class recordID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  POVendorInventoryRepo.recordID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POVendorInventoryRepo.vendorID>
  {
  }

  public new abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POVendorInventoryRepo.vendorLocationID>
  {
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POVendorInventoryRepo.inventoryID>
  {
  }

  public new abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POVendorInventoryRepo.subItemID>
  {
  }

  public new abstract class eRQ : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POVendorInventoryRepo.eRQ>
  {
  }

  public abstract class nullERQ : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POVendorInventoryRepo.nullERQ>
  {
  }
}

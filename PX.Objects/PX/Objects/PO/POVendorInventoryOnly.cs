// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POVendorInventoryOnly
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.PO;

[PXHidden]
[PXProjection(typeof (Select4<POVendorInventory, Where<POVendorInventory.vendorID, Equal<CurrentValue<POSiteStatusFilter.vendorID>>>, Aggregate<GroupBy<POVendorInventory.vendorID, GroupBy<POVendorInventory.inventoryID, GroupBy<POVendorInventory.subItemID>>>>>), Persistent = false)]
public class POVendorInventoryOnly : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _VendorID;
  protected int? _InventoryID;
  protected int? _SubItemID;

  [PXDBInt(IsKey = true, BqlField = typeof (POVendorInventory.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (POVendorInventory.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (POVendorInventory.subItemID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  public abstract class vendorID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  POVendorInventoryOnly.vendorID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POVendorInventoryOnly.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POVendorInventoryOnly.subItemID>
  {
  }
}

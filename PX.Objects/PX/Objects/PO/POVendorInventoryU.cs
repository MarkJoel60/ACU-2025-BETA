// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POVendorInventoryU
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXHidden]
[Serializable]
public class POVendorInventoryU : POVendorInventory
{
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.purchaseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<POVendorInventory.inventoryID>>>>))]
  [INUnit(null, typeof (PX.Objects.IN.InventoryItem.baseUnit))]
  [PXCheckUnique(new Type[] {typeof (POVendorInventory.vendorID), typeof (POVendorInventory.vendorLocationID), typeof (POVendorInventory.inventoryID), typeof (POVendorInventory.purchaseUnit)})]
  public override 
  #nullable disable
  string PurchaseUnit
  {
    get => this._PurchaseUnit;
    set => this._PurchaseUnit = value;
  }

  public new class PK : PrimaryKeyOf<POVendorInventoryU>.By<POVendorInventoryU.recordID>
  {
    public static POVendorInventoryU Find(PXGraph graph, int? recordID, PKFindOptions options = 0)
    {
      return (POVendorInventoryU) PrimaryKeyOf<POVendorInventoryU>.By<POVendorInventoryU.recordID>.FindBy(graph, (object) recordID, options);
    }
  }

  public new static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<POVendorInventoryU>.By<POVendorInventoryU.inventoryID>
    {
    }
  }

  public new abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POVendorInventoryU.recordID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POVendorInventoryU.vendorID>
  {
  }

  public new abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POVendorInventoryU.vendorLocationID>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POVendorInventoryU.inventoryID>
  {
  }

  public new abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POVendorInventoryU.subItemID>
  {
  }

  public new abstract class purchaseUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POVendorInventoryU.purchaseUnit>
  {
  }
}

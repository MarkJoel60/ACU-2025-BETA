// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POVendorInventoryAll
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXHidden]
[Serializable]
public class POVendorInventoryAll : POVendorInventory
{
  public new class PK : PrimaryKeyOf<
  #nullable disable
  POVendorInventoryAll>.By<POVendorInventoryAll.recordID>
  {
    public static POVendorInventoryAll Find(PXGraph graph, int? recordID, PKFindOptions options = 0)
    {
      return (POVendorInventoryAll) PrimaryKeyOf<POVendorInventoryAll>.By<POVendorInventoryAll.recordID>.FindBy(graph, (object) recordID, options);
    }
  }

  public new static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<POVendorInventoryAll>.By<POVendorInventoryAll.inventoryID>
    {
    }
  }

  public new abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POVendorInventoryAll.recordID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POVendorInventoryAll.vendorID>
  {
  }

  public new abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POVendorInventoryAll.vendorLocationID>
  {
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POVendorInventoryAll.inventoryID>
  {
  }

  public new abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POVendorInventoryAll.subItemID>
  {
  }

  public new abstract class eRQ : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POVendorInventoryAll.eRQ>
  {
  }
}

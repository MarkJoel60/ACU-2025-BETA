// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.ItemLookupScanHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN.WMS;

public sealed class ItemLookupScanHeader : PXCacheExtension<
#nullable disable
ScanHeader>
{
  [Site(Enabled = false)]
  public int? SiteID { get; set; }

  [Inventory(Enabled = false)]
  public int? InventoryID { get; set; }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemLookupScanHeader.siteID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemLookupScanHeader.inventoryID>
  {
  }
}

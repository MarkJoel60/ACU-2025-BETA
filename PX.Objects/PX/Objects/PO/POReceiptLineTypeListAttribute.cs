// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptLineTypeListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.PO;

/// <summary>
/// Specialized PXStringList attribute for Receipt Line types.<br />
/// Provides a list of possible values for line types depending upon InventoryID <br />
/// specified in the row. For stock- and not-stock inventory items the allowed values <br />
/// are different. If item is changed and old value is not compatible with inventory item <br />
/// - it will defaulted to the applicable value.<br />
/// <example>
/// [POReceiptLineTypeList(typeof(POLine.inventoryID))]
/// </example>
/// </summary>
public class POReceiptLineTypeListAttribute(Type inventoryID) : POLineTypeListAttribute(inventoryID, new string[4]
{
  "GI",
  "NS",
  "SV",
  "FT"
}, new string[4]
{
  "Goods for IN",
  "Non-Stock",
  "Service",
  "Freight"
})
{
}

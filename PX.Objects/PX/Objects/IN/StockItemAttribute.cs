// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.StockItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.IN;

[PXDBInt]
[PXUIField]
[PXRestrictor(typeof (Where<InventoryItem.stkItem, Equal<boolTrue>>), "The inventory item is not a stock item.", new Type[] {})]
public class StockItemAttribute : InventoryAttribute
{
}

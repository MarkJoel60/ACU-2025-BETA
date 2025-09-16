// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.SC.DAC.SubcontractInventoryItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.CN.Subcontracts.SC.DAC;

/// <summary>
/// This class is required as Acumatica use Filters with SELECTOR type across all system.
/// We needed to override ViewName of the <see cref="T:PX.Data.FilterHeader" /> to specific entity type.
/// </summary>
[Obsolete("This class has been deprecated and will be removed in Acumatica ERP 2021R1.")]
[PXCacheName("Subcontract Inventory Item")]
public class SubcontractInventoryItem : InventoryItem
{
  public new abstract class inventoryID : IBqlField, IBqlOperand
  {
  }

  public new abstract class inventoryCD : IBqlField, IBqlOperand
  {
  }

  public new abstract class descr : IBqlField, IBqlOperand
  {
  }
}

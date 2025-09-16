// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Interfaces.IShipLineSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.SO.Interfaces;

public interface IShipLineSource : ICloneable
{
  Decimal? PlanQty { get; set; }

  string PlanType { get; }

  long? PlanID { get; }

  string LotSerialNbr { get; }

  INLotSerClass INLotSerClass { get; }

  PX.Objects.IN.InventoryItem InventoryItem { get; }

  PX.Objects.IN.INSite INSite { get; }

  bool Selected { get; }

  bool RequireAllocationUnallocated { get; }

  bool RequireINItemPlanUpdate { get; }

  string NewPlanType { get; }

  DateTime? ExpireDate { get; }

  object FilesAndNotesSource { get; }

  Decimal? MinRequiredBaseShippedQty { get; }

  string ShippingRule { get; }

  bool? IsStockItem { get; }

  int? InventoryID { get; }

  int? SubItemID { get; }

  int? SiteID { get; }

  int? CostCenterID { get; }

  string TranDesc { get; }

  int? ProjectID { get; }

  int? TaskID { get; }

  int? CostCodeID { get; }

  string UOM { get; }

  int? LineNbr { get; }
}

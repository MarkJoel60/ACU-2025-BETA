// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Models.OrderOrchestration.WarehouseInventoryDetails
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.SO.Models.OrderOrchestration;

public class WarehouseInventoryDetails
{
  public int? SiteID { get; set; }

  public int? InventoryID { get; set; }

  public virtual string UOM { get; set; }

  public virtual string BaseUOM { get; set; }

  public Decimal? QtyHardAvail { get; set; }

  public Decimal? SafetyStock { get; set; }

  public bool? MaintainSafetyStock { get; set; }
}

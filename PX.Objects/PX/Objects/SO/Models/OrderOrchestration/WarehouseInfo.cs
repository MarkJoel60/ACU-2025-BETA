// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Models.OrderOrchestration.WarehouseInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.Models.OrderOrchestration;

public class WarehouseInfo
{
  public int? SiteID { get; set; }

  public string SiteCD { get; set; }

  public int? SitePriority { get; set; }

  public List<WarehouseInventoryDetails> InventoryDetails { get; set; }

  public string PlanID { get; set; }
}

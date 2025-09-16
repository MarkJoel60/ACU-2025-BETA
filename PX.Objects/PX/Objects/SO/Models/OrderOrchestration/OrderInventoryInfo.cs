// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Models.OrderOrchestration.OrderInventoryInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.SO.Models.OrderOrchestration;

public class OrderInventoryInfo
{
  public virtual int? InventoryID { get; set; }

  public virtual Decimal? LineQty { get; set; }

  public virtual string BaseUOM { get; set; }

  public virtual bool IsAllocated { get; set; }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.IItemPlanSOShipSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.SO;

public interface IItemPlanSOShipSource : IItemPlanSource, IItemPlanMaster
{
  bool? Released { get; }

  bool? IsStockItem { get; }

  bool? Confirmed { get; }

  string PlanType { get; }

  string OrigPlanType { get; }

  bool? IsComponentItem { get; }

  string Operation { get; }

  int? SubItemID { get; }

  int? LocationID { get; }

  string LotSerialNbr { get; }

  string AssignedNbr { get; }

  DateTime? ShipDate { get; }

  Decimal? BaseQty { get; }
}

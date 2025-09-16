// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.IItemPlanPOSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.PO;

public interface IItemPlanPOSource : IItemPlanSource, IItemPlanMaster
{
  string OrderType { get; }

  string OrderNbr { get; }

  int? LineNbr { get; }

  string LineType { get; }

  int? VendorID { get; }

  int? SubItemID { get; }

  int? ProjectID { get; }

  int? TaskID { get; }

  int? CostCenterID { get; }

  bool? Cancelled { get; }

  bool? Completed { get; }

  DateTime? PromisedDate { get; }

  string UOM { get; }

  Decimal? BaseOpenQty { get; }

  bool? ClearPlanID { get; set; }
}

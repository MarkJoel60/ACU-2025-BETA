// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.IPOReturnLineSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.PO;

public interface IPOReturnLineSource
{
  string ReceiptType { get; }

  string ReceiptNbr { get; }

  int? BranchID { get; }

  int? LineNbr { get; }

  string LineType { get; }

  string POType { get; }

  string PONbr { get; }

  int? POLineNbr { get; }

  bool? IsStockItem { get; }

  int? InventoryID { get; }

  bool? AccrueCost { get; }

  int? SubItemID { get; }

  int? SiteID { get; }

  int? LocationID { get; }

  string LotSerialNbr { get; }

  DateTime? ExpireDate { get; }

  string UOM { get; }

  Decimal? ReceiptQty { get; }

  Decimal? BaseReceiptQty { get; }

  Decimal? ReturnedQty { get; set; }

  Decimal? BaseReturnedQty { get; }

  long? CuryInfoID { get; }

  int? ExpenseAcctID { get; }

  int? ExpenseSubID { get; }

  int? POAccrualAcctID { get; }

  int? POAccrualSubID { get; }

  string TranDesc { get; }

  int? CostCodeID { get; }

  int? ProjectID { get; }

  int? TaskID { get; }

  bool? AllowEditUnitCost { get; }

  bool? ManualPrice { get; }

  Decimal? DiscPct { get; }

  Decimal? CuryDiscAmt { get; }

  Decimal? DiscAmt { get; }

  Decimal? UnitCost { get; }

  Decimal? CuryUnitCost { get; }

  Decimal? ExtCost { get; }

  Decimal? CuryExtCost { get; }

  Decimal? TranCostFinal { get; }

  Decimal? TranCost { get; }

  Decimal? CuryTranCost { get; }

  string DropshipExpenseRecording { get; }

  bool? IsSpecialOrder { get; }

  int? CostCenterID { get; }

  Decimal? TranUnitCost { get; }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.IAPTranSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.PO;

public interface IAPTranSource
{
  int? BranchID { get; }

  int? ExpenseAcctID { get; }

  int? ExpenseSubID { get; }

  int? POAccrualAcctID { get; }

  int? POAccrualSubID { get; }

  string LineType { get; }

  int? SiteID { get; }

  bool? IsStockItem { get; }

  int? InventoryID { get; }

  bool? AccrueCost { get; }

  string OrigUOM { get; }

  string UOM { get; }

  long? CuryInfoID { get; }

  Decimal? OrigQty { get; }

  Decimal? BaseOrigQty { get; }

  Decimal? BillQty { get; }

  Decimal? BaseBillQty { get; }

  Decimal? BilledQty { get; }

  Decimal? CuryBilledAmt { get; }

  Decimal? BilledAmt { get; }

  Decimal? CuryUnitCost { get; }

  Decimal? UnitCost { get; }

  Decimal? CuryDiscAmt { get; }

  Decimal? DiscAmt { get; }

  Decimal? DiscPct { get; }

  Decimal? CuryRetainageAmt { get; }

  Decimal? RetainageAmt { get; }

  Decimal? RetainagePct { get; }

  Decimal? CuryLineAmt { get; }

  Decimal? LineAmt { get; }

  string TaxCategoryID { get; }

  string TranDesc { get; }

  string TaxID { get; }

  int? ProjectID { get; }

  int? TaskID { get; }

  string POAccrualType { get; }

  Guid? POAccrualRefNoteID { get; }

  int? POAccrualLineNbr { get; }

  string OrderType { get; }

  string CompletePOLine { get; }

  Decimal? CuryUnbilledAmt { get; }

  Decimal? UnbilledAmt { get; }

  int? CostCodeID { get; }

  bool IsReturn { get; }

  bool IsPartiallyBilled { get; }

  bool? AllowEditUnitCost { get; }

  bool AggregateWithExistingTran { get; }

  string DiscountID { get; }

  string DiscountSequenceID { get; }

  Decimal? GroupDiscountRate { get; }

  Decimal? DocumentDiscountRate { get; }

  DateTime? DRTermStartDate { get; }

  DateTime? DRTermEndDate { get; }

  string DropshipExpenseRecording { get; }

  bool CompareReferenceKey(PX.Objects.AP.APTran aTran);

  void SetReferenceKeyTo(PX.Objects.AP.APTran aTran);
}

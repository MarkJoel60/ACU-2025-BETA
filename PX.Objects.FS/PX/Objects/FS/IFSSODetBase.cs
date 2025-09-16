// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.IFSSODetBase
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public interface IFSSODetBase
{
  int? DocID { get; }

  int? LineID { get; }

  int? LineNbr { get; }

  string LineRef { get; set; }

  int? BranchID { get; set; }

  long? CuryInfoID { get; set; }

  string LineType { get; set; }

  bool? IsPrepaid { get; set; }

  int? InventoryID { get; set; }

  int? SubItemID { get; set; }

  string UOM { get; set; }

  string TranDesc { get; set; }

  bool? ManualPrice { get; set; }

  bool? IsBillable { get; set; }

  bool? IsFree { get; set; }

  string BillingRule { get; set; }

  Decimal? CuryUnitPrice { get; set; }

  Decimal? UnitPrice { get; set; }

  Decimal? CuryBillableExtPrice { get; set; }

  Decimal? BillableExtPrice { get; set; }

  int? SiteID { get; set; }

  int? SiteLocationID { get; set; }

  int? EstimatedDuration { get; set; }

  Decimal? EstimatedQty { get; set; }

  Decimal? Qty { get; set; }

  int? GetDuration(FieldType fieldType);

  int? GetApptDuration();

  Decimal? GetQty(FieldType fieldType);

  Decimal? GetApptQty();

  void SetDuration(FieldType fieldType, int? duration, PXCache cache, bool raiseEvents);

  void SetQty(FieldType fieldType, Decimal? qty, PXCache cache, bool raiseEvents);

  Decimal? GetTranAmt(FieldType fieldType);

  int? GetPrimaryDACDuration();

  Decimal? GetPrimaryDACQty();

  Decimal? GetPrimaryDACTranAmt();

  int? ProjectID { get; set; }

  int? ProjectTaskID { get; set; }

  int? CostCodeID { get; set; }

  int? AcctID { get; set; }

  int? SubID { get; set; }

  string Status { get; set; }

  string EquipmentAction { get; set; }

  int? SMEquipmentID { get; set; }

  int? ComponentID { get; set; }

  int? EquipmentLineRef { get; set; }

  bool? Warranty { get; set; }

  bool IsService { get; }

  bool IsInventoryItem { get; }

  bool? ContractRelated { get; set; }

  bool? EnablePO { get; set; }

  string POType { get; set; }

  string PONbr { get; set; }

  string POStatus { get; set; }

  bool? POCompleted { get; set; }

  int? POLineNbr { get; set; }

  string POSource { get; set; }

  int? POVendorID { get; set; }

  int? POVendorLocationID { get; set; }

  Decimal? UnitCost { get; set; }

  Decimal? CuryUnitCost { get; set; }

  Decimal? ExtCost { get; set; }

  Decimal? CuryExtCost { get; set; }

  string TaxCategoryID { get; set; }

  string LotSerialNbr { get; set; }

  Decimal? DiscPct { get; set; }

  Decimal? CuryDiscAmt { get; set; }

  string LinkedEntityType { get; set; }

  string LinkedDocType { get; set; }

  string LinkedDocRefNbr { get; set; }

  int? LinkedLineNbr { get; set; }

  bool IsExpenseReceiptItem { get; }

  bool IsAPBillItem { get; }

  bool IsLinkedItem { get; }
}

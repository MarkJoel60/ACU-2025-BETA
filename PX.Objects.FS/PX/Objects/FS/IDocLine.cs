// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.IDocLine
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;

#nullable disable
namespace PX.Objects.FS;

public interface IDocLine
{
  string LineType { get; }

  string LineRef { get; }

  int? LineNbr { get; }

  string SrvOrdType { get; }

  string RefNbr { get; }

  int? InventoryID { get; set; }

  int? SubItemID { get; }

  string UOM { get; }

  string TranDesc { get; }

  bool? IsFree { get; }

  bool? ManualPrice { get; }

  Decimal? CuryUnitPrice { get; }

  Decimal? CuryBillableExtPrice { get; }

  int? SiteID { get; }

  int? SiteLocationID { get; }

  int? ProjectID { get; }

  int? ProjectTaskID { get; }

  int? CostCenterID { get; }

  int? CostCodeID { get; }

  int? AcctID { get; set; }

  int? SubID { get; }

  int? PostID { get; }

  string EquipmentAction { get; }

  int? SMEquipmentID { get; }

  int? ComponentID { get; }

  int? EquipmentLineRef { get; }

  string NewTargetEquipmentLineNbr { get; }

  string Comment { get; }

  string LotSerialNbr { get; }

  string TaxCategoryID { get; }

  int? BranchID { get; }

  int? TabOrigin { get; }

  int? SortOrder { get; }

  Decimal? CuryDiscAmt { get; }

  Decimal? DiscPct { get; }

  int? DocID { get; }

  int? LineID { get; }

  int? PostAppointmentID { get; }

  int? PostSODetID { get; }

  int? PostAppDetID { get; }

  string BillingBy { get; }

  string SourceTable { get; }

  bool IsService { get; }

  Decimal? GetQty(FieldType fieldType);

  Decimal? GetBaseQty(FieldType fieldType);

  Decimal? GetTranAmt(FieldType fieldType);
}

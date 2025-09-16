// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.IPostLine
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

#nullable disable
namespace PX.Objects.FS;

public interface IPostLine
{
  int? SOID { get; set; }

  int? AppointmentID { get; set; }

  string PostTo { get; set; }

  string PostOrderType { get; set; }

  string PostOrderTypeNegativeBalance { get; set; }

  bool? PostNegBalanceToAP { get; set; }

  string DfltTermIDARSO { get; set; }

  int? BillingCycleID { get; set; }

  int? ProjectID { get; set; }

  string FrequencyType { get; set; }

  int? WeeklyFrequency { get; set; }

  int? MonthlyFrequency { get; set; }

  string SendInvoicesTo { get; set; }

  string BillingBy { get; set; }

  bool? GroupBillByLocations { get; set; }

  string BillingCycleCD { get; set; }

  string BillingCycleType { get; set; }

  bool? InvoiceOnlyCompletedServiceOrder { get; set; }

  string TimeCycleType { get; set; }

  int? TimeCycleWeekDay { get; set; }

  int? TimeCycleDayOfMonth { get; set; }

  int? BillLocationID { get; set; }

  string CustWorkOrderRefNbr { get; set; }

  string CustPORefNbr { get; set; }

  int? BillCustomerID { get; set; }

  int? BranchID { get; set; }

  string DocType { get; set; }

  string CuryID { get; set; }

  string TaxZoneID { get; set; }

  int? RowIndex { get; set; }

  string GroupKey { get; set; }

  int? BatchID { get; set; }

  string EntityType { get; }
}

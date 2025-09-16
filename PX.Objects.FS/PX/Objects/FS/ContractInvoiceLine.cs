// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ContractInvoiceLine
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class ContractInvoiceLine
{
  public ContractInvoiceLine(IDocLine row)
  {
    this.InventoryID = row.InventoryID;
    this.UOM = row.UOM;
    this.SMEquipmentID = row.SMEquipmentID;
    this.CuryUnitPrice = row.CuryUnitPrice;
    this.ManualPrice = row.ManualPrice;
    this.CuryBillableExtPrice = row.CuryBillableExtPrice;
    this.DiscPct = row.DiscPct;
    this.SubItemID = row.SubItemID;
    this.SiteID = row.SiteID;
    this.SiteLocationID = row.SiteLocationID;
    this.IsFree = row.IsFree;
    this.AcctID = row.AcctID;
    this.SubID = row.SubID;
    this.EquipmentAction = row.EquipmentAction;
    this.EquipmentLineRef = row.EquipmentLineRef;
    this.NewTargetEquipmentLineNbr = row.NewTargetEquipmentLineNbr;
    this.ComponentID = row.ComponentID;
    this.LineRef = row.LineRef;
    this.LineNbr = row.LineNbr;
    this.TranDescPrefix = string.Empty;
    this.ProjectTaskID = row.ProjectTaskID;
    this.CostCodeID = row.CostCodeID;
    this.Processed = new bool?(false);
  }

  public ContractInvoiceLine(
    PXResult<FSContractPeriodDet, FSContractPeriod, FSServiceContract, FSBranchLocation> row)
  {
    FSServiceContract fsServiceContract = PXResult<FSContractPeriodDet, FSContractPeriod, FSServiceContract, FSBranchLocation>.op_Implicit(row);
    FSContractPeriodDet contractPeriodDet = PXResult<FSContractPeriodDet, FSContractPeriod, FSServiceContract, FSBranchLocation>.op_Implicit(row);
    FSBranchLocation fsBranchLocation = PXResult<FSContractPeriodDet, FSContractPeriod, FSServiceContract, FSBranchLocation>.op_Implicit(row);
    this.ServiceContractID = fsServiceContract.ServiceContractID;
    this.ContractType = fsServiceContract.RecordType;
    this.ContractPeriodID = contractPeriodDet.ContractPeriodDetID;
    this.ContractPeriodDetID = contractPeriodDet.ContractPeriodID;
    this.BillingRule = contractPeriodDet.BillingRule;
    this.InventoryID = contractPeriodDet.InventoryID;
    this.UOM = contractPeriodDet.UOM;
    this.SMEquipmentID = contractPeriodDet.SMEquipmentID;
    this.CuryUnitPrice = contractPeriodDet.RecurringUnitPrice;
    this.ManualPrice = new bool?(true);
    this.ContractRelated = new bool?(true);
    this.SubItemID = (int?) fsBranchLocation?.DfltSubItemID;
    this.SiteID = (int?) fsBranchLocation?.DfltSiteID;
    this.SiteLocationID = new int?();
    this.IsFree = new bool?(false);
    int? nullable;
    if (this.BillingRule == "TIME")
    {
      nullable = contractPeriodDet.Time;
      this.Qty = new Decimal?(Decimal.Divide((Decimal) nullable.GetValueOrDefault(), 60M));
    }
    else
      this.Qty = contractPeriodDet.Qty;
    this.OverageItemPrice = contractPeriodDet.OverageItemPrice;
    nullable = new int?();
    this.AcctID = nullable;
    nullable = new int?();
    this.SubID = nullable;
    this.EquipmentAction = "NO";
    nullable = new int?();
    this.EquipmentLineRef = nullable;
    this.NewTargetEquipmentLineNbr = (string) null;
    nullable = new int?();
    this.ComponentID = nullable;
    this.LineRef = string.Empty;
    this.SalesPersonID = fsServiceContract.SalesPersonID;
    this.Commissionable = fsServiceContract.Commissionable;
    this.TranDescPrefix = string.Empty;
    this.ProjectTaskID = contractPeriodDet.ProjectTaskID;
    this.CostCodeID = contractPeriodDet.CostCodeID;
    this.DeferredCode = contractPeriodDet.DeferredCode;
    this.BillingType = fsServiceContract.BillingType;
    this.Processed = new bool?(false);
  }

  public ContractInvoiceLine(
    PXResult<FSAppointmentDet, FSSODet, FSAppointment> row)
    : this((IDocLine) PXResult<FSAppointmentDet, FSSODet, FSAppointment>.op_Implicit(row))
  {
    FSAppointmentDet fsAppointmentDet = PXResult<FSAppointmentDet, FSSODet, FSAppointment>.op_Implicit(row);
    FSAppointment fsAppointment = PXResult<FSAppointmentDet, FSSODet, FSAppointment>.op_Implicit(row);
    FSSODet fssoDet = PXResult<FSAppointmentDet, FSSODet, FSAppointment>.op_Implicit(row);
    this.fsSODet = fssoDet;
    this.fsAppointmentDet = fsAppointmentDet;
    this.SOID = fssoDet.SOID;
    this.SODetID = fssoDet.SODetID;
    this.AppointmentID = fsAppointmentDet.AppointmentID;
    this.AppDetID = fsAppointmentDet.AppDetID;
    this.SrvOrdType = fsAppointmentDet.SrvOrdType;
    this.RefNbr = fsAppointmentDet.RefNbr;
    this.BillingRule = fssoDet.BillingRule;
    this.ContractRelated = fsAppointmentDet.ContractRelated;
    this.Qty = fsAppointmentDet.ContractRelated.GetValueOrDefault() ? fsAppointmentDet.ActualQty : fsAppointmentDet.BillableQty;
    this.OverageItemPrice = fsAppointmentDet.OverageItemPrice;
    this.ManualPrice = fsAppointmentDet.ContractRelated.GetValueOrDefault() ? new bool?(true) : fsAppointmentDet.ManualPrice;
    this.SalesPersonID = fsAppointment.SalesPersonID;
    this.Commissionable = fsAppointment.Commissionable;
  }

  public ContractInvoiceLine(PXResult<FSSODet, FSServiceOrder> row)
    : this((IDocLine) PXResult<FSSODet, FSServiceOrder>.op_Implicit(row))
  {
    FSSODet fssoDet = PXResult<FSSODet, FSServiceOrder>.op_Implicit(row);
    FSServiceOrder fsServiceOrder = PXResult<FSSODet, FSServiceOrder>.op_Implicit(row);
    this.fsSODet = fssoDet;
    this.SOID = fssoDet.SOID;
    this.SODetID = fssoDet.SODetID;
    this.SrvOrdType = fssoDet.SrvOrdType;
    this.RefNbr = fssoDet.RefNbr;
    this.BillingRule = fssoDet.BillingRule;
    this.ContractRelated = fssoDet.ContractRelated;
    this.Qty = fssoDet.ContractRelated.GetValueOrDefault() ? fssoDet.EstimatedQty : fssoDet.BillableQty;
    this.OverageItemPrice = fssoDet.CuryExtraUsageUnitPrice;
    this.ManualPrice = fssoDet.ContractRelated.GetValueOrDefault() ? new bool?(true) : fssoDet.ManualPrice;
    this.SalesPersonID = fsServiceOrder.SalesPersonID;
    this.Commissionable = fsServiceOrder.Commissionable;
  }

  public ContractInvoiceLine(ContractInvoiceLine row, Decimal? qty)
    : this(row)
  {
    this.Qty = qty;
  }

  public ContractInvoiceLine(ContractInvoiceLine row)
  {
    this.ServiceContractID = row.ServiceContractID;
    this.ContractType = row.ContractType;
    this.ContractPeriodID = row.ContractPeriodDetID;
    this.ContractPeriodDetID = row.ContractPeriodID;
    this.SOID = row.SOID;
    this.SODetID = row.SODetID;
    this.AppointmentID = row.AppointmentID;
    this.AppDetID = row.AppDetID;
    this.SrvOrdType = row.SrvOrdType;
    this.RefNbr = row.RefNbr;
    this.BillingRule = row.BillingRule;
    this.InventoryID = row.InventoryID;
    this.UOM = row.UOM;
    this.SMEquipmentID = row.SMEquipmentID;
    this.CuryUnitPrice = row.CuryUnitPrice;
    this.ManualPrice = row.ManualPrice;
    this.DiscPct = row.DiscPct;
    this.ContractRelated = row.ContractRelated;
    this.SubItemID = row.SubItemID;
    this.SiteID = row.SiteID;
    this.SiteLocationID = row.SiteLocationID;
    this.IsFree = row.IsFree;
    this.Qty = row.Qty;
    this.CuryBillableExtPrice = row.CuryBillableExtPrice;
    this.OverageItemPrice = row.OverageItemPrice;
    this.AcctID = row.AcctID;
    this.SubID = row.SubID;
    this.EquipmentAction = row.EquipmentAction;
    this.EquipmentLineRef = row.EquipmentLineRef;
    this.NewTargetEquipmentLineNbr = row.NewTargetEquipmentLineNbr;
    this.ComponentID = row.ComponentID;
    this.LineRef = row.LineRef;
    this.LineNbr = row.LineNbr;
    this.SalesPersonID = row.SalesPersonID;
    this.Commissionable = row.Commissionable;
    this.TranDescPrefix = string.Empty;
    this.ProjectTaskID = row.ProjectTaskID;
    this.CostCodeID = row.CostCodeID;
    this.DeferredCode = row.DeferredCode;
    this.BillingType = row.BillingType;
    this.Processed = new bool?(false);
    this.fsSODet = row.fsSODet;
    this.fsAppointmentDet = row.fsAppointmentDet;
  }

  public int? ServiceContractID { get; set; }

  public int? ContractPeriodID { get; set; }

  public int? ContractPeriodDetID { get; set; }

  public string ContractType { get; set; }

  public string BillingType { get; set; }

  public int? AppointmentID { get; set; }

  public int? AppDetID { get; set; }

  public int? SOID { get; set; }

  public int? SODetID { get; set; }

  public string SrvOrdType { get; set; }

  public string RefNbr { get; set; }

  public string BillingRule { get; set; }

  public int? InventoryID { get; set; }

  public string UOM { get; set; }

  public int? SMEquipmentID { get; set; }

  public Decimal? CuryUnitPrice { get; set; }

  public bool? ManualPrice { get; set; }

  public Decimal? CuryBillableExtPrice { get; set; }

  public Decimal? DiscPct { get; set; }

  public bool? ContractRelated { get; set; }

  public int? SubItemID { get; set; }

  public int? SiteID { get; set; }

  public int? SiteLocationID { get; set; }

  public bool? IsFree { get; set; }

  public Decimal? OverageItemPrice { get; set; }

  public Decimal? Qty { get; set; }

  public int? AcctID { get; set; }

  public int? SubID { get; set; }

  public string EquipmentAction { get; set; }

  public int? EquipmentLineRef { get; set; }

  public string NewTargetEquipmentLineNbr { get; set; }

  public int? ComponentID { get; set; }

  public string LineRef { get; set; }

  public int? LineNbr { get; set; }

  public int? SalesPersonID { get; set; }

  public bool? Commissionable { get; set; }

  public int? ProjectTaskID { get; set; }

  public int? CostCodeID { get; set; }

  public string DeferredCode { get; set; }

  public string TranDescPrefix { get; set; }

  public bool? Processed { get; set; }

  public FSSODet fsSODet { get; set; }

  public FSAppointmentDet fsAppointmentDet { get; set; }
}

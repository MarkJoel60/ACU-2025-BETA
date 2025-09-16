// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.Attributes.PlanType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.IN.Matrix.Attributes;

public class PlanType
{
  public const string Available = "AA";
  public const string AvailableforShipment = "AS";
  public const string NotAvailable = "NA";
  public const string SOBooked = "SB";
  public const string SOShipping = "SS";
  public const string SOShipped = "SD";
  public const string SOBackOrdered = "SO";
  public const string INIssues = "II";
  public const string INReceipts = "IR";
  public const string InTransit = "IT";
  public const string InAssemblyDemand = "IA";
  public const string InAssemblySupply = "IS";
  public const string PurchasePrepared = "PP";
  public const string PurchaseOrders = "PO";
  public const string POReceipts = "PR";
  public const string Expired = "EX";
  public const string OnHand = "OH";
  public const string SOtoPurchase = "SP";
  public const string PurchaseforSO = "PS";
  public const string PurchaseforSOPrepared = "PU";
  public const string PurchaseReceiptsForSO = "PC";
  public const string SOtoDropShip = "DS";
  public const string DropShipforSO = "DH";
  public const string DropShipforSOPrepared = "DI";
  public const string DropShipforSOReceipts = "DP";
  public const string FSSrvOrdPrepared = "FP";
  public const string FSSrvOrdBooked = "FB";
  public const string FSSrvOrdAllocated = "FA";
  public const string FixedFSSrvOrd = "FF";
  public const string POFixedFSSrvOrd = "FO";
  public const string POFixedFSSrvOrdPrepared = "FW";
  public const string POFixedFSSrvOrdReceipts = "FR";
  public const string InTransitToProduction = "PT";
  public const string ProductionSupplyPrepared = "PZ";
  public const string ProductionSupply = "PX";
  public const string POFixedProductionPrepared = "PF";
  public const string POFixedProductionOrders = "PQ";
  public const string ProductionDemandPrepared = "PW";
  public const string ProductionDemand = "PD";
  public const string ProductionAllocated = "PA";
  public const string SOFixedProduction = "PE";
  public const string ProdFixedPurchase = "PJ";
  public const string ProdFixedProduction = "PY";
  public const string ProdFixedProdOrdersPrepared = "PN";
  public const string ProdFixedProdOrders = "PL";
  public const string ProdFixedSalesOrdersPrepared = "PG";
  public const string ProdFixedSalesOrders = "PH";

  [PXLocalizable]
  public class UI
  {
    public const string Available = "Available";
    public const string AvailableforShipment = "Available for Shipment";
    public const string NotAvailable = "Not Available";
    public const string SOBooked = "SO Booked";
    public const string SOShipping = "SO Shipping";
    public const string SOShipped = "SO Shipped";
    public const string SOBackOrdered = "SO Back Ordered";
    public const string INIssues = "IN Issues";
    public const string INReceipts = "IN Receipts";
    public const string InTransit = "In Transit";
    public const string InAssemblyDemand = "Kit Assembly Demand";
    public const string InAssemblySupply = "Kit Assembly Supply";
    public const string PurchasePrepared = "Purchase Prepared";
    public const string PurchaseOrders = "Purchase Orders";
    public const string POReceipts = "PO Receipts";
    public const string Expired = "Expired";
    public const string OnHand = "On Hand";
    public const string SOtoPurchase = "SO to Purchase";
    public const string PurchaseforSO = "Purchase for SO";
    public const string PurchaseforSOPrepared = "Purchase for SO Prepared";
    public const string PurchaseReceiptsForSO = "Receipts for SO";
    public const string SOtoDropShip = "SO to Drop-Ship";
    public const string DropShipforSO = "Drop-Ship for SO";
    public const string DropShipforSOPrepared = "Drop-Ship for SO Prepared";
    public const string DropShipforSOReceipts = "Drop-Ship for SO Receipts";
    public const string FSSrvOrdPrepared = "FS Prepared";
    public const string FSSrvOrdBooked = "FS Booked";
    public const string FSSrvOrdAllocated = "FS Allocated";
    public const string FixedFSSrvOrd = "FS to Purchase";
    public const string POFixedFSSrvOrd = "Purchase for FS";
    public const string POFixedFSSrvOrdPrepared = "Purchase for FS Prepared";
    public const string POFixedFSSrvOrdReceipts = "Receipts for FS";
    public const string InTransitToProduction = "In Transit to Production";
    public const string ProductionSupplyPrepared = "Production Supply Prepared";
    public const string ProductionSupply = "Production Supply";
    public const string POFixedProductionPrepared = "Purchase for Prod. Prepared";
    public const string POFixedProductionOrders = "Purchase for Production";
    public const string ProductionDemandPrepared = "Production Demand Prepared";
    public const string ProductionDemand = "Production Demand";
    public const string ProductionAllocated = "Production Allocated";
    public const string SOFixedProduction = "SO to Production";
    public const string ProdFixedPurchase = "Production to Purchase";
    public const string ProdFixedProduction = "Production to Production";
    public const string ProdFixedProdOrdersPrepared = "Production for Prod. Prepared";
    public const string ProdFixedProdOrders = "Production for Production";
    public const string ProdFixedSalesOrdersPrepared = "Production for SO Prepared";
    public const string ProdFixedSalesOrders = "Production for SO";
  }

  public class ListAttribute : PXStringListAttribute
  {
    public virtual void CacheAttached(PXCache sender)
    {
      this._AllowedValues = new string[25]
      {
        "AA",
        "AS",
        "NA",
        "SB",
        "SS",
        "SD",
        "SO",
        "II",
        "IR",
        "IT",
        "IA",
        "IS",
        "PP",
        "PO",
        "PR",
        "EX",
        "OH",
        "SP",
        "PS",
        "PU",
        "PC",
        "DS",
        "DH",
        "DI",
        "DP"
      };
      this._AllowedLabels = new string[25]
      {
        "Available",
        "Available for Shipment",
        "Not Available",
        "SO Booked",
        "SO Shipping",
        "SO Shipped",
        "SO Back Ordered",
        "IN Issues",
        "IN Receipts",
        "In Transit",
        "Kit Assembly Demand",
        "Kit Assembly Supply",
        "Purchase Prepared",
        "Purchase Orders",
        "PO Receipts",
        "Expired",
        "On Hand",
        "SO to Purchase",
        "Purchase for SO",
        "Purchase for SO Prepared",
        "Receipts for SO",
        "SO to Drop-Ship",
        "Drop-Ship for SO",
        "Drop-Ship for SO Prepared",
        "Drop-Ship for SO Receipts"
      };
      if (PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>())
      {
        this._AllowedValues = EnumerableExtensions.Append<string>(this._AllowedValues, new string[7]
        {
          "FP",
          "FB",
          "FA",
          "FF",
          "FO",
          "FW",
          "FR"
        });
        this._AllowedLabels = EnumerableExtensions.Append<string>(this._AllowedLabels, new string[7]
        {
          "FS Prepared",
          "FS Booked",
          "FS Allocated",
          "FS to Purchase",
          "Purchase for FS",
          "Purchase for FS Prepared",
          "Receipts for FS"
        });
      }
      if (PXAccess.FeatureInstalled<FeaturesSet.manufacturing>())
      {
        this._AllowedValues = EnumerableExtensions.Append<string>(this._AllowedValues, new string[15]
        {
          "PT",
          "PZ",
          "PX",
          "PF",
          "PQ",
          "PW",
          "PD",
          "PA",
          "PE",
          "PJ",
          "PY",
          "PN",
          "PL",
          "PG",
          "PH"
        });
        this._AllowedLabels = EnumerableExtensions.Append<string>(this._AllowedLabels, new string[15]
        {
          "In Transit to Production",
          "Production Supply Prepared",
          "Production Supply",
          "Purchase for Prod. Prepared",
          "Purchase for Production",
          "Production Demand Prepared",
          "Production Demand",
          "Production Allocated",
          "SO to Production",
          "Production to Purchase",
          "Production to Production",
          "Production for Prod. Prepared",
          "Production for Production",
          "Production for SO Prepared",
          "Production for SO"
        });
      }
      this._NeutralAllowedLabels = this._AllowedLabels;
      base.CacheAttached(sender);
    }
  }
}

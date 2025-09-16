// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.Utility.Level3Helper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Wrappers;
using PX.Objects.AR.Standalone;
using PX.Objects.CR;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CC.Utility;

internal static class Level3Helper
{
  internal static void SetL3StatusExternalTransaction(
    ExternalTransaction externalTransaction,
    L3TranStatus? l3Status,
    string l3Error)
  {
    string l3Status1 = V2Converter.GetL3Status(l3Status.GetValueOrDefault());
    switch (externalTransaction.L3Status)
    {
      case null:
      case "NA ":
        externalTransaction.L3Status = "PEN";
        break;
      case "PEN":
      case "FLD":
        externalTransaction.L3Status = l3Status.HasValue ? l3Status1 : "PEN";
        externalTransaction.L3Error = l3Error;
        break;
      case "RFL":
        switch (l3Status1)
        {
          case "REJ":
            externalTransaction.L3Status = "RRJ";
            externalTransaction.L3Error = (string) null;
            return;
          case "FLD":
            externalTransaction.L3Status = "RFL";
            externalTransaction.L3Error = l3Error;
            return;
          case "SNT":
            externalTransaction.L3Status = "SNT";
            externalTransaction.L3Error = (string) null;
            return;
          default:
            return;
        }
      case "SNT":
        switch (l3Status1)
        {
          case "REJ":
            externalTransaction.L3Status = "REJ";
            return;
          case "SNT":
            externalTransaction.L3Status = "SNT";
            externalTransaction.L3Error = (string) null;
            return;
          case "FLD":
            externalTransaction.L3Status = "RFL";
            externalTransaction.L3Error = l3Error;
            return;
          default:
            return;
        }
      case "REJ":
        if (!(l3Status1 == "SNT"))
          break;
        externalTransaction.L3Status = "SNT";
        externalTransaction.L3Error = (string) null;
        break;
      default:
        switch (l3Status1)
        {
          case "SNT":
            externalTransaction.L3Status = "SNT";
            externalTransaction.L3Error = (string) null;
            return;
          case "REJ":
            externalTransaction.L3Status = "REJ";
            return;
          default:
            return;
        }
    }
  }

  internal static void FillL3Header(
    ARPaymentEntry arPaymentEntry,
    PX.Objects.Extensions.PaymentTransaction.Payment payment,
    Decimal taxAmount)
  {
    PX.Objects.AR.Customer current1 = ((PXSelectBase<PX.Objects.AR.Customer>) arPaymentEntry.customer).Current;
    PX.Objects.CR.Location current2 = ((PXSelectBase<PX.Objects.CR.Location>) arPaymentEntry.location).Current;
    PX.Objects.GL.Branch parent = (PX.Objects.GL.Branch) PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<PX.Objects.AR.ARPayment>.By<PX.Objects.AR.ARPayment.branchID>.FindParent((PXGraph) arPaymentEntry, (PX.Objects.AR.ARPayment.branchID) ((PXSelectBase<PX.Objects.AR.ARPayment>) arPaymentEntry.CurrentDocument).Current, (PKFindOptions) 0);
    Level3Helper.FillL3Header((PXGraph) arPaymentEntry, payment, taxAmount, current1, current2, parent);
  }

  internal static void FillL3Header(
    ARCashSaleEntry arCashSaleEntry,
    PX.Objects.Extensions.PaymentTransaction.Payment payment,
    Decimal taxAmount)
  {
    PX.Objects.AR.Customer current1 = ((PXSelectBase<PX.Objects.AR.Customer>) arCashSaleEntry.customer).Current;
    PX.Objects.CR.Location current2 = ((PXSelectBase<PX.Objects.CR.Location>) arCashSaleEntry.location).Current;
    PX.Objects.GL.Branch parent = (PX.Objects.GL.Branch) PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARCashSale>.By<ARCashSale.branchID>.FindParent((PXGraph) arCashSaleEntry, (ARCashSale.branchID) ((PXSelectBase<ARCashSale>) arCashSaleEntry.CurrentDocument).Current, (PKFindOptions) 0);
    Level3Helper.FillL3Header((PXGraph) arCashSaleEntry, payment, taxAmount, current1, current2, parent);
  }

  private static void FillL3Header(
    PXGraph graph,
    PX.Objects.Extensions.PaymentTransaction.Payment payment,
    Decimal taxAmount,
    PX.Objects.AR.Customer customer,
    PX.Objects.CR.Location customerLocation,
    PX.Objects.GL.Branch branch)
  {
    PX.Objects.CR.Address customerLocationAddress = (PX.Objects.CR.Address) PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.defAddressID>.FindParent(graph, (PX.Objects.CR.Location.defAddressID) customerLocation, (PKFindOptions) 0);
    payment.L3Data.DestinationCountryCode = customerLocationAddress != null ? ((IEnumerable<ISO3166.Country>) ISO3166.Country.List).FirstOrDefault<ISO3166.Country>((Func<ISO3166.Country, bool>) (c => c.TwoLetterCode.Equals(customerLocationAddress.CountryID))).NumericCode : (string) null;
    ExternalTransaction externalTransaction = ExternalTransaction.PK.Find(graph, payment.CCActualExternalTransactionID);
    payment.L3Data.TransactionId = externalTransaction.TranNumber;
    payment.L3Data.CardType = CardType.GetCardTypeEnumByCode(externalTransaction.CardType);
    payment.L3Data.CustomerVatRegistration = customerLocation?.TaxRegistrationID ?? customer?.TaxRegistrationID;
    PX.Objects.CR.Address parent = (PX.Objects.CR.Address) PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.defAddressID>.FindParent(graph, (PX.Objects.AR.Customer.defAddressID) customer, (PKFindOptions) 0);
    payment.L3Data.ShiptoZipCode = customerLocationAddress?.PostalCode ?? parent?.PostalCode;
    PX.Objects.CR.BAccount baccount = (PX.Objects.CR.BAccount) BAccount2.PK.Find(graph, branch.BAccountID.Value);
    PX.Objects.CR.Address address = PX.Objects.CR.Address.PK.Find(graph, baccount.DefContactID);
    payment.L3Data.ShipfromZipCode = address?.PostalCode;
    payment.L3Data.TaxAmount = new Decimal?(Math.Round(taxAmount, 2, MidpointRounding.AwayFromZero));
    TranProcessingL3DataInput l3Data = payment.L3Data;
    Decimal? taxAmount1 = payment.L3Data.TaxAmount;
    Decimal num1 = 0M;
    int num2 = taxAmount1.GetValueOrDefault() == num1 & taxAmount1.HasValue ? 1 : 0;
    l3Data.TaxExempt = num2 != 0;
    payment.L3Data.MerchantVatRegistration = baccount?.TaxRegistrationID;
  }

  public static void RetriveInventoryInfo(
    PXGraph graph,
    int? inventoryID,
    TranProcessingL3DataLineItemInput item)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(graph, inventoryID);
    item.ProductCode = inventoryItem?.InventoryCD;
    item.CommodityCode = inventoryItem?.HSTariffCode;
  }
}

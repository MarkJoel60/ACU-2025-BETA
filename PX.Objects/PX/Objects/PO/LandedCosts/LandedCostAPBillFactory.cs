// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCosts.LandedCostAPBillFactory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.LandedCosts;

public class LandedCostAPBillFactory
{
  private readonly PXGraph _pxGraph;

  public LandedCostAPBillFactory(PXGraph pxGraph) => this._pxGraph = pxGraph;

  public PX.Objects.AP.APInvoice CreateLandedCostBillHeader(
    POLandedCostDoc doc,
    IEnumerable<POLandedCostDetail> details,
    PX.Objects.AP.APInvoice newdoc)
  {
    Decimal valueOrDefault = details.Sum<POLandedCostDetail>((Func<POLandedCostDetail, Decimal?>) (d => d.CuryLineAmt)).GetValueOrDefault();
    newdoc.DocType = valueOrDefault >= 0M ? "INV" : "ADR";
    int? payToVendorId = doc.PayToVendorID;
    if (payToVendorId.HasValue)
    {
      payToVendorId = doc.PayToVendorID;
      int? vendorId = doc.VendorID;
      if (!(payToVendorId.GetValueOrDefault() == vendorId.GetValueOrDefault() & payToVendorId.HasValue == vendorId.HasValue) && PXAccess.FeatureInstalled<FeaturesSet.vendorRelations>())
      {
        newdoc.VendorID = doc.PayToVendorID;
        newdoc.SuppliedByVendorID = doc.VendorID;
        newdoc.SuppliedByVendorLocationID = doc.VendorLocationID;
        goto label_4;
      }
    }
    newdoc.VendorID = doc.VendorID;
    newdoc.VendorLocationID = doc.VendorLocationID;
    newdoc.SuppliedByVendorID = doc.VendorID;
    newdoc.SuppliedByVendorLocationID = doc.VendorLocationID;
label_4:
    newdoc.DocDate = doc.BillDate ?? newdoc.DocDate;
    newdoc.TaxCalcMode = "T";
    newdoc.TermsID = doc.TermsID;
    newdoc.InvoiceNbr = doc.VendorRefNbr;
    newdoc.BranchID = doc.BranchID;
    newdoc.TaxCalcMode = "T";
    newdoc.TaxZoneID = doc.TaxZoneID;
    newdoc.CuryOrigDocAmt = doc.CuryDocTotal;
    newdoc.DueDate = doc.DueDate;
    newdoc.DiscDate = doc.DiscDate;
    newdoc.CuryOrigDiscAmt = doc.CuryDiscAmt;
    return newdoc;
  }

  public APInvoiceWrapper CreateLandedCostBill(
    POLandedCostDoc doc,
    IEnumerable<POLandedCostDetail> details,
    IEnumerable<POLandedCostTaxTran> taxes,
    PX.Objects.AP.APInvoice newdoc)
  {
    newdoc = this.CreateLandedCostBillHeader(doc, details, newdoc);
    Decimal mult = newdoc.DocType == "INV" ? 1M : -1M;
    PX.Objects.AP.APTran[] transactions = this.CreateTransactions(doc, details, mult);
    List<APTaxTran> list = taxes.Select<POLandedCostTaxTran, APTaxTran>((Func<POLandedCostTaxTran, APTaxTran>) (tax =>
    {
      APTaxTran landedCostBill = new APTaxTran();
      landedCostBill.Module = "AP";
      landedCostBill.TaxID = tax.TaxID;
      landedCostBill.JurisType = tax.JurisType;
      landedCostBill.JurisName = tax.JurisName;
      landedCostBill.TaxRate = tax.TaxRate;
      landedCostBill.CuryID = doc.CuryID;
      landedCostBill.CuryInfoID = doc.CuryInfoID;
      Decimal num1 = mult;
      Decimal? curyTaxableAmt = tax.CuryTaxableAmt;
      landedCostBill.CuryTaxableAmt = curyTaxableAmt.HasValue ? new Decimal?(num1 * curyTaxableAmt.GetValueOrDefault()) : new Decimal?();
      Decimal num2 = mult;
      Decimal? nullable = tax.TaxableAmt;
      landedCostBill.TaxableAmt = nullable.HasValue ? new Decimal?(num2 * nullable.GetValueOrDefault()) : new Decimal?();
      Decimal num3 = mult;
      nullable = tax.CuryTaxAmt;
      landedCostBill.CuryTaxAmt = nullable.HasValue ? new Decimal?(num3 * nullable.GetValueOrDefault()) : new Decimal?();
      Decimal num4 = mult;
      nullable = tax.NonDeductibleTaxRate;
      landedCostBill.NonDeductibleTaxRate = nullable.HasValue ? new Decimal?(num4 * nullable.GetValueOrDefault()) : new Decimal?();
      Decimal num5 = mult;
      nullable = tax.TaxAmt;
      landedCostBill.TaxAmt = nullable.HasValue ? new Decimal?(num5 * nullable.GetValueOrDefault()) : new Decimal?();
      Decimal num6 = mult;
      nullable = tax.CuryExpenseAmt;
      landedCostBill.CuryExpenseAmt = nullable.HasValue ? new Decimal?(num6 * nullable.GetValueOrDefault()) : new Decimal?();
      Decimal num7 = mult;
      nullable = tax.ExpenseAmt;
      landedCostBill.ExpenseAmt = nullable.HasValue ? new Decimal?(num7 * nullable.GetValueOrDefault()) : new Decimal?();
      landedCostBill.TaxZoneID = tax.TaxZoneID;
      landedCostBill.IsTaxInclusive = tax.IsTaxInclusive;
      return landedCostBill;
    })).ToList<APTaxTran>();
    return new APInvoiceWrapper(newdoc, (ICollection<PX.Objects.AP.APTran>) transactions, (ICollection<APTaxTran>) list);
  }

  public virtual PX.Objects.AP.APTran[] CreateTransactions(
    POLandedCostDoc doc,
    IEnumerable<POLandedCostDetail> landedCostDetail,
    Decimal mult)
  {
    List<PX.Objects.AP.APTran> apTranList = new List<PX.Objects.AP.APTran>();
    foreach (POLandedCostDetail landedCostDetail1 in landedCostDetail)
    {
      this.GetLandedCostCode(landedCostDetail1.LandedCostCodeID);
      PX.Objects.AP.APTran apTran1 = new PX.Objects.AP.APTran();
      apTran1.AccountID = landedCostDetail1.LCAccrualAcct;
      apTran1.SubID = landedCostDetail1.LCAccrualSub;
      apTran1.UOM = (string) null;
      apTran1.Qty = new Decimal?(1M);
      PX.Objects.AP.APTran apTran2 = apTran1;
      Decimal num1 = mult;
      Decimal? curyLineAmt = landedCostDetail1.CuryLineAmt;
      Decimal? nullable1;
      Decimal? nullable2;
      if (!curyLineAmt.HasValue)
      {
        nullable1 = new Decimal?();
        nullable2 = nullable1;
      }
      else
        nullable2 = new Decimal?(num1 * curyLineAmt.GetValueOrDefault());
      nullable1 = nullable2;
      Decimal? nullable3 = new Decimal?(nullable1.GetValueOrDefault());
      apTran2.CuryUnitCost = nullable3;
      PX.Objects.AP.APTran apTran3 = apTran1;
      Decimal num2 = mult;
      curyLineAmt = landedCostDetail1.CuryLineAmt;
      Decimal? nullable4;
      if (!curyLineAmt.HasValue)
      {
        nullable1 = new Decimal?();
        nullable4 = nullable1;
      }
      else
        nullable4 = new Decimal?(num2 * curyLineAmt.GetValueOrDefault());
      nullable1 = nullable4;
      Decimal? nullable5 = new Decimal?(nullable1.GetValueOrDefault());
      apTran3.CuryTranAmt = nullable5;
      apTran1.TranDesc = landedCostDetail1.Descr;
      apTran1.InventoryID = new int?();
      apTran1.TaxCategoryID = landedCostDetail1.TaxCategoryID;
      apTran1.LCDocType = doc.DocType;
      apTran1.LCRefNbr = doc.RefNbr;
      apTran1.LCLineNbr = landedCostDetail1.LineNbr;
      apTran1.RetainagePct = new Decimal?(0M);
      apTran1.CuryRetainageAmt = new Decimal?(0M);
      apTran1.RetainageAmt = new Decimal?(0M);
      apTran1.BranchID = landedCostDetail1.BranchID;
      apTran1.ReceiptLineNbr = new int?();
      apTran1.LandedCostCodeID = landedCostDetail1.LandedCostCodeID;
      apTranList.Add(apTran1);
    }
    return apTranList.ToArray();
  }

  protected virtual LandedCostCode GetLandedCostCode(string landedCostCodeID)
  {
    return LandedCostCode.PK.Find(this._pxGraph, landedCostCodeID);
  }
}

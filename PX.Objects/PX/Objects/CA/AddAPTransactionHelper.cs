// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.AddAPTransactionHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CA.Descriptor;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public static class AddAPTransactionHelper
{
  public static void TryToSetBranch(
    this PX.Objects.AP.APPayment payment,
    APPaymentEntry graph,
    ICADocSource parameters,
    BranchSource branchSource)
  {
    int? nullable;
    switch (branchSource)
    {
      case BranchSource.CustomerVendorLocation:
        CashAccount cashAccount = CashAccount.PK.Find((PXGraph) graph, parameters.CashAccountID);
        PX.Objects.CR.Location location = PX.Objects.CR.Location.PK.Find((PXGraph) graph, parameters.BAccountID, parameters.LocationID);
        if (cashAccount.RestrictVisibilityWithBranch.GetValueOrDefault())
        {
          int? branchId = cashAccount.BranchID;
          nullable = location.CBranchID;
          if (!(branchId.GetValueOrDefault() == nullable.GetValueOrDefault() & branchId.HasValue == nullable.HasValue))
            break;
        }
        payment.BranchID = location.VBranchID;
        break;
      case BranchSource.AccessInfo:
        payment.BranchID = ((PXGraph) graph).Accessinfo.BranchID;
        break;
    }
    nullable = payment.BranchID;
    if (nullable.HasValue)
      return;
    payment.BranchID = ((PXGraph) graph).Accessinfo.BranchID;
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R2.")]
  public static PX.Objects.AP.APPayment InitializeAPPayment(
    APPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    IList<ICADocAdjust> aAdjustments,
    bool aOnHold)
  {
    return AddAPTransactionHelper.InitializeAPPayment(graph, parameters, aCuryInfo, aAdjustments, aOnHold, BranchSource.AccessInfo);
  }

  public static PX.Objects.AP.APPayment InitializeAPPayment(
    APPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    IList<ICADocAdjust> aAdjustments,
    bool aOnHold,
    BranchSource branchSource)
  {
    PX.Objects.AP.APPayment apPayment1 = new PX.Objects.AP.APPayment();
    Decimal? nullable1;
    if (!(parameters.DrCr == "C" ^ Math.Sign(parameters.CuryOrigDocAmt.Value) > 0))
    {
      Decimal? nullable2 = aAdjustments == null ? new Decimal?(0.0M) : aAdjustments.Select<ICADocAdjust, APAdjust>((Func<ICADocAdjust, APAdjust>) (adj => new APAdjust()
      {
        AdjgDocType = "CHK",
        AdjdDocType = adj.AdjdDocType,
        CuryAdjgAmt = adj.CuryAdjgAmount
      })).Sum<APAdjust>((Func<APAdjust, Decimal?>) (adj =>
      {
        Decimal? adjgBalSign = adj.AdjgBalSign;
        Decimal? curyAdjgAmt = adj.CuryAdjgAmt;
        return !(adjgBalSign.HasValue & curyAdjgAmt.HasValue) ? new Decimal?() : new Decimal?(adjgBalSign.GetValueOrDefault() * curyAdjgAmt.GetValueOrDefault());
      }));
      Decimal? curyOrigDocAmt = parameters.CuryOrigDocAmt;
      Decimal num = (Decimal) (parameters.ChargeDrCr == parameters.DrCr ? 1 : -1);
      nullable1 = parameters.CuryChargeAmt;
      Decimal? nullable3 = nullable1.HasValue ? new Decimal?(num * nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal valueOrDefault = nullable3.GetValueOrDefault();
      Decimal? nullable4;
      if (!curyOrigDocAmt.HasValue)
      {
        nullable3 = new Decimal?();
        nullable4 = nullable3;
      }
      else
        nullable4 = new Decimal?(curyOrigDocAmt.GetValueOrDefault() - valueOrDefault);
      Decimal? nullable5 = nullable4;
      if (nullable2.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable2.HasValue == nullable5.HasValue)
      {
        apPayment1.DocType = "CHK";
      }
      else
      {
        apPayment1.DocType = "PPM";
        if (aAdjustments != null && aAdjustments.Any<ICADocAdjust>((Func<ICADocAdjust, bool>) (adj => adj.AdjdDocType == "ADR" || adj.AdjdDocType == "PPM")))
          throw new PXException("Can't apply Prepayment to Debit Adjustment or Prepayment. Please remove the Debit Adjustments and Prepayments from the list of applications or apply the entire payment amount so that a Check is created.");
      }
    }
    else
      apPayment1.DocType = "REF";
    PX.Objects.AP.APPayment copy1 = PXCache<PX.Objects.AP.APPayment>.CreateCopy(((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Insert(apPayment1));
    copy1.TryToSetBranch(graph, parameters, branchSource);
    copy1.VendorID = parameters.BAccountID;
    copy1.VendorLocationID = parameters.LocationID;
    copy1.CashAccountID = parameters.CashAccountID;
    copy1.PaymentMethodID = parameters.PaymentMethodID;
    copy1.AdjDate = parameters.MatchingPaymentDate;
    PX.Objects.AP.APPayment copy2 = PXCache<PX.Objects.AP.APPayment>.CreateCopy(((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Update(copy1));
    copy2.CuryID = parameters.CuryID;
    PX.Objects.AP.APPayment apPayment2 = copy2;
    nullable1 = parameters.CuryOrigDocAmt;
    Decimal num1 = Math.Abs(nullable1.GetValueOrDefault());
    Decimal num2 = (Decimal) (parameters.ChargeDrCr == "D" ? -1 : 1);
    nullable1 = parameters.CuryChargeAmt;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    Decimal num3 = num2 * valueOrDefault1;
    Decimal? nullable6 = new Decimal?(num1 - num3);
    apPayment2.CuryOrigDocAmt = nullable6;
    copy2.DocDesc = parameters.TranDesc;
    copy2.Cleared = parameters.Cleared;
    if (copy2.Cleared.GetValueOrDefault())
      copy2.ClearDate = parameters.ClearDate;
    copy2.PrintCheck = new bool?(false);
    copy2.Printed = new bool?(true);
    copy2.FinPeriodID = parameters.FinPeriodID;
    copy2.AdjFinPeriodID = parameters.FinPeriodID;
    copy2.ExtRefNbr = parameters.ExtRefNbr?.Trim();
    copy2.Hold = new bool?(aOnHold);
    copy2.CARefTranAccountID = parameters.CARefTranAccountID;
    copy2.CARefTranID = parameters.CARefTranID;
    copy2.CARefSplitLineNbr = parameters.CARefSplitLineNbr;
    copy2.RefNoteID = parameters.NoteID;
    PX.Objects.AP.APPayment apPayment3 = ((PXSelectBase<PX.Objects.AP.APPayment>) graph.Document).Update(copy2);
    PXNoteAttribute.CopyNoteAndFiles(((PXGraph) graph).Caches[parameters.GetType()], (object) parameters, ((PXSelectBase) graph.Document).Cache, (object) apPayment3, (PXNoteAttribute.IPXCopySettings) null);
    return PXCache<PX.Objects.AP.APPayment>.CreateCopy(apPayment3);
  }

  public static void InitializeCurrencyInfo(
    APPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    PX.Objects.AP.APPayment doc)
  {
    PX.Objects.CM.CurrencyInfo currencyInfo = aCuryInfo;
    if (currencyInfo == null)
      currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelectReadonly<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) graph, new object[1]
      {
        (object) parameters.CuryInfoID
      }));
    PX.Objects.CM.CurrencyInfo info = currencyInfo;
    if (info == null)
      return;
    PX.Objects.CM.Extensions.CurrencyInfo ex = PX.Objects.CM.Extensions.CurrencyInfo.GetEX(info);
    ex.CuryInfoID = ((PXGraph) graph).GetExtension<APPaymentEntry.MultiCurrency>().GetDefaultCurrencyInfo().CuryInfoID;
    ((PXSelectBase) graph.currencyinfo).Cache.Update((object) ex);
  }

  public static APAdjust InitializeAPAdjustment(APPaymentEntry graph, ICADocAdjust adjustment)
  {
    APAdjust apAdjust1 = (APAdjust) null;
    if (adjustment.PaymentsByLinesAllowed.GetValueOrDefault())
    {
      foreach (PXResult<APTran> pxResult in PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Current<CABankTranAdjustment.adjdDocType>>, And<APTran.refNbr, Equal<Current<CABankTranAdjustment.adjdRefNbr>>, And<APTran.curyTranBal, Greater<Zero>>>>>.Config>.SelectMultiBound((PXGraph) graph, new object[1]
      {
        (object) adjustment
      }, Array.Empty<object>()))
      {
        APTran apTran = PXResult<APTran>.op_Implicit(pxResult);
        apAdjust1 = ((PXSelectBase<APAdjust>) graph.Adjustments).Insert(new APAdjust()
        {
          AdjdRefNbr = adjustment.AdjdRefNbr,
          AdjdDocType = adjustment.AdjdDocType,
          AdjdLineNbr = apTran.LineNbr
        });
      }
    }
    else
    {
      APAdjust apAdjust2 = new APAdjust();
      apAdjust2.AdjdDocType = adjustment.AdjdDocType;
      apAdjust2.AdjdRefNbr = adjustment.AdjdRefNbr;
      apAdjust2.AdjdLineNbr = new int?(0);
      APAdjust apAdjust3;
      try
      {
        apAdjust3 = ((PXSelectBase<APAdjust>) graph.Adjustments).Insert(apAdjust2);
      }
      catch (PXFieldValueProcessingException ex)
      {
        APAdjust apAdjust4 = PXResultset<APAdjust>.op_Implicit(PXSelectBase<APAdjust, PXSelectReadonly<APAdjust, Where<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, And<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.released, Equal<False>>>>>.Config>.Select((PXGraph) graph, new object[2]
        {
          (object) adjustment.AdjdRefNbr,
          (object) adjustment.AdjdDocType
        }));
        PX.Objects.AP.APInvoice apInvoice = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(PXSelectBase<PX.Objects.AP.APInvoice, PXSelectReadonly<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APInvoice.refNbr, Equal<Required<APAdjust.adjdRefNbr>>, And<PX.Objects.AP.APInvoice.docType, Equal<Required<APAdjust.adjdDocType>>>>>.Config>.Select((PXGraph) graph, new object[2]
        {
          (object) adjustment.AdjdRefNbr,
          (object) adjustment.AdjdDocType
        }));
        if (apAdjust4 != null || apInvoice.Status == "C")
          throw new PXException("Could not add application of '{0}' invoice. Possibly it is already used in another application", new object[1]
          {
            (object) adjustment.AdjdRefNbr
          });
        throw ex;
      }
      apAdjust3.AdjdCuryRate = adjustment.AdjdCuryRate;
      APAdjust apAdjust5 = ((PXSelectBase<APAdjust>) graph.Adjustments).Update(apAdjust3);
      if (adjustment.CuryAdjgAmount.HasValue)
        apAdjust5.CuryAdjgAmt = adjustment.CuryAdjgAmount;
      apAdjust5.CuryAdjgDiscAmt = adjustment.CuryAdjgDiscAmt;
      apAdjust5.CuryAdjgWhTaxAmt = adjustment.CuryAdjgWhTaxAmt;
      apAdjust1 = ((PXSelectBase<APAdjust>) graph.Adjustments).Update(apAdjust5);
    }
    return apAdjust1;
  }
}

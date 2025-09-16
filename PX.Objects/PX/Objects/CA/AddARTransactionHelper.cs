// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.AddARTransactionHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CA.Descriptor;
using System;

#nullable disable
namespace PX.Objects.CA;

public static class AddARTransactionHelper
{
  public static void TryToSetBranch(
    this PX.Objects.AR.ARPayment payment,
    ARPaymentEntry graph,
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
        payment.BranchID = location.CBranchID;
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
  public static PX.Objects.AR.ARPayment InitializeARPayment(
    ARPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    bool aOnHold)
  {
    return AddARTransactionHelper.InitializeARPayment(graph, parameters, aCuryInfo, aOnHold, BranchSource.AccessInfo);
  }

  public static PX.Objects.AR.ARPayment InitializeARPayment(
    ARPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    bool aOnHold,
    BranchSource branchSource)
  {
    PX.Objects.AR.ARPayment arPayment1 = new PX.Objects.AR.ARPayment();
    if (!(parameters.DrCr == "C" ^ Math.Sign(parameters.CuryOrigDocAmt.Value) > 0))
      arPayment1.DocType = "REF";
    else
      arPayment1.DocType = "PMT";
    PX.Objects.AR.ARPayment copy = PXCache<PX.Objects.AR.ARPayment>.CreateCopy(((PXSelectBase<PX.Objects.AR.ARPayment>) graph.Document).Insert(arPayment1));
    copy.TryToSetBranch(graph, parameters, branchSource);
    copy.CustomerID = parameters.BAccountID;
    copy.CustomerLocationID = parameters.LocationID;
    copy.PaymentMethodID = parameters.PaymentMethodID;
    copy.PMInstanceID = parameters.PMInstanceID;
    copy.CashAccountID = parameters.CashAccountID;
    copy.CuryOrigDocAmt = new Decimal?(Math.Abs(parameters.CuryOrigDocAmt.Value - (Decimal) (parameters.ChargeDrCr == parameters.DrCr ? 1 : -1) * parameters.CuryChargeAmt.GetValueOrDefault()));
    copy.DocDesc = parameters.TranDesc;
    copy.Cleared = parameters.Cleared;
    if (copy.Cleared.GetValueOrDefault())
      copy.ClearDate = parameters.ClearDate;
    copy.AdjDate = parameters.MatchingPaymentDate;
    copy.FinPeriodID = parameters.FinPeriodID;
    copy.AdjFinPeriodID = parameters.FinPeriodID;
    copy.ExtRefNbr = parameters.ExtRefNbr?.Trim();
    copy.CARefTranAccountID = parameters.CARefTranAccountID;
    copy.CARefTranID = parameters.CARefTranID;
    copy.CARefSplitLineNbr = parameters.CARefSplitLineNbr;
    copy.Hold = new bool?(aOnHold);
    if (aCuryInfo == null)
      copy.CuryID = parameters.CuryID;
    copy.RefNoteID = parameters.NoteID;
    PX.Objects.AR.ARPayment arPayment2 = ((PXSelectBase<PX.Objects.AR.ARPayment>) graph.Document).Update(copy);
    PXNoteAttribute.CopyNoteAndFiles(((PXGraph) graph).Caches[parameters.GetType()], (object) parameters, ((PXSelectBase) graph.Document).Cache, (object) arPayment2, (PXNoteAttribute.IPXCopySettings) null);
    return PXCache<PX.Objects.AR.ARPayment>.CreateCopy(arPayment2);
  }

  public static void InitializeCurrencyInfo(
    ARPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    PX.Objects.AR.ARPayment doc)
  {
    PX.Objects.CM.CurrencyInfo currencyInfo1 = aCuryInfo;
    if (currencyInfo1 == null)
      currencyInfo1 = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelectReadonly<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) graph, new object[1]
      {
        (object) parameters.CuryInfoID
      }));
    PX.Objects.CM.CurrencyInfo info = currencyInfo1;
    if (info == null)
      return;
    foreach (PXResult<PX.Objects.CM.Extensions.CurrencyInfo> pxResult in ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) graph.currencyinfo).Select(Array.Empty<object>()))
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = PXResult<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
      PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.RestoreCopy(currencyInfo2, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(info));
      currencyInfo2.CuryInfoID = doc.CuryInfoID;
      doc.CuryID = currencyInfo2.CuryID;
    }
  }

  public static Decimal InitializeARAdjustment(
    ARPaymentEntry graph,
    ARAdjust adjustment,
    Decimal curyAppliedAmt)
  {
    ARAdjust arAdjust1 = new ARAdjust();
    arAdjust1.AdjdDocType = adjustment.AdjdDocType;
    arAdjust1.AdjdRefNbr = adjustment.AdjdRefNbr;
    arAdjust1.AdjdLineNbr = adjustment.AdjdLineNbr;
    ARAdjust arAdjust2;
    try
    {
      arAdjust2 = ((PXSelectBase<ARAdjust>) graph.Adjustments).Insert(arAdjust1);
    }
    catch (PXFieldValueProcessingException ex)
    {
      ARAdjust arAdjust3 = PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXSelectReadonly<ARAdjust, Where<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.released, Equal<False>>>>>.Config>.Select((PXGraph) graph, new object[2]
      {
        (object) adjustment.AdjdRefNbr,
        (object) adjustment.AdjdDocType
      }));
      PX.Objects.AR.ARInvoice arInvoice = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelectReadonly<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<PX.Objects.AR.ARInvoice.docType, Equal<Required<ARAdjust.adjdDocType>>>>>.Config>.Select((PXGraph) graph, new object[2]
      {
        (object) adjustment.AdjdRefNbr,
        (object) adjustment.AdjdDocType
      }));
      if (arAdjust3 != null || arInvoice.Status == "C")
        throw new PXException("Could not add application of '{0}' invoice. Possibly it is already used in another application", new object[1]
        {
          (object) adjustment.AdjdRefNbr
        });
      throw ex;
    }
    arAdjust2.AdjdCuryRate = adjustment.AdjdCuryRate;
    ARAdjust arAdjust4 = ((PXSelectBase<ARAdjust>) graph.Adjustments).Update(arAdjust2);
    if (arAdjust4.CuryAdjgAmt.HasValue)
    {
      arAdjust4.CuryAdjgAmt = new Decimal?(adjustment.CuryAdjgAmt ?? arAdjust4.CuryAdjgAmt.GetValueOrDefault());
      curyAppliedAmt += arAdjust4.CuryAdjgAmt.GetValueOrDefault();
    }
    arAdjust4.WriteOffReasonCode = adjustment.WriteOffReasonCode ?? arAdjust4.WriteOffReasonCode;
    ARAdjust arAdjust5 = arAdjust4;
    Decimal? nullable1 = adjustment.WOBal;
    Decimal? nullable2 = nullable1 ?? arAdjust4.WOBal;
    arAdjust5.WOBal = nullable2;
    ARAdjust arAdjust6 = arAdjust4;
    nullable1 = adjustment.AdjWOAmt;
    Decimal? nullable3 = nullable1 ?? arAdjust4.AdjWOAmt;
    arAdjust6.AdjWOAmt = nullable3;
    ARAdjust arAdjust7 = arAdjust4;
    nullable1 = adjustment.CuryAdjdWOAmt;
    Decimal? nullable4 = nullable1 ?? arAdjust4.CuryAdjdWOAmt;
    arAdjust7.CuryAdjdWOAmt = nullable4;
    ARAdjust arAdjust8 = arAdjust4;
    nullable1 = adjustment.CuryAdjgWOAmt;
    Decimal? nullable5 = nullable1 ?? arAdjust4.CuryAdjgWOAmt;
    arAdjust8.CuryAdjgWOAmt = nullable5;
    ARAdjust arAdjust9 = arAdjust4;
    nullable1 = adjustment.CuryWOBal;
    Decimal? nullable6 = nullable1 ?? arAdjust4.CuryWOBal;
    arAdjust9.CuryWOBal = nullable6;
    ARAdjust arAdjust10 = arAdjust4;
    nullable1 = adjustment.CuryAdjgDiscAmt;
    Decimal? nullable7 = new Decimal?(nullable1 ?? arAdjust4.CuryAdjgDiscAmt.GetValueOrDefault());
    arAdjust10.CuryAdjgDiscAmt = nullable7;
    ARAdjust arAdjust11 = arAdjust4;
    nullable1 = adjustment.CuryAdjgWOAmt;
    Decimal? nullable8 = new Decimal?(nullable1 ?? arAdjust4.CuryAdjgWOAmt.GetValueOrDefault());
    arAdjust11.CuryAdjgWOAmt = nullable8;
    ((PXSelectBase<ARAdjust>) graph.Adjustments).Update(arAdjust4);
    return curyAppliedAmt;
  }
}

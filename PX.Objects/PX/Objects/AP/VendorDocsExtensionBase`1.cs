// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorDocsExtensionBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.AP;

public abstract class VendorDocsExtensionBase<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public virtual PXResultset<APInvoice> GetVendDocs(
    APPayment currentAPPayment,
    APSetup currentAPSetup)
  {
    PXSelectBase<APInvoice> cmd = (PXSelectBase<APInvoice>) new PXSelectReadonly2<APInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APInvoice.docType>, And<APTran.refNbr, Equal<APInvoice.refNbr>, And<APTran.curyTranBal, NotEqual<decimal0>>>>>, LeftJoin<APAdjust, On<APAdjust.adjdDocType, Equal<APInvoice.docType>, And<APAdjust.adjdRefNbr, Equal<APInvoice.refNbr>, And<APAdjust.released, Equal<False>>>>, LeftJoin<APAdjust2, On<APAdjust2.adjgDocType, Equal<APInvoice.docType>, And<APAdjust2.adjgRefNbr, Equal<APInvoice.refNbr>, And<APAdjust2.released, Equal<False>, And<APAdjust2.voided, Equal<False>>>>>, LeftJoin<APPayment, On<APPayment.docType, Equal<APInvoice.docType>, And<APPayment.refNbr, Equal<APInvoice.refNbr>, And<APPayment.docType, Equal<APDocType.prepayment>>>>>>>>>, Where<APInvoice.vendorID, Equal<Required<APPayment.vendorID>>, And<PX.Objects.CM.Extensions.CurrencyInfo.baseCuryID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.baseCuryID>>, And2<Where<APInvoice.released, Equal<True>, Or<APInvoice.prebooked, Equal<True>>>, And<APInvoice.openDoc, Equal<True>, And<APInvoice.hold, Equal<False>, And<APAdjust.adjgRefNbr, PX.Data.IsNull, And<APAdjust2.adjdRefNbr, PX.Data.IsNull, And<APInvoice.pendingPPD, NotEqual<True>, And<APPayment.refNbr, PX.Data.IsNull, PX.Data.And<Where<APInvoice.docDate, LessEqual<Required<APPayment.adjDate>>, And<APInvoice.tranPeriodID, LessEqual<Required<APPayment.tranPeriodID>>, Or<Required<APPayment.docType>, Equal<APDocType.check>, And<Required<APSetup.earlyChecks>, Equal<True>, Or<Required<APPayment.docType>, Equal<APDocType.voidCheck>, And<Required<APSetup.earlyChecks>, Equal<True>, Or<Required<APPayment.docType>, Equal<APDocType.prepayment>, And<Required<APSetup.earlyChecks>, Equal<True>>>>>>>>>>>>>>>>>>>, OrderBy<Asc<APInvoice.dueDate, Asc<APInvoice.refNbr, Asc<APTran.refNbr>>>>>((PXGraph) this.Base);
    this.AddFiters(currentAPPayment, cmd);
    string baseCuryId = PXAccess.GetBranch(currentAPPayment.BranchID).BaseCuryID;
    PXResultset<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, APTran> vendDocs = new PXResultset<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, APTran>();
    foreach (PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, APTran> pxResult in cmd.Select((object) currentAPPayment.VendorID, (object) baseCuryId, (object) currentAPPayment.AdjDate, (object) currentAPPayment.AdjTranPeriodID, (object) currentAPPayment.DocType, (object) currentAPSetup.EarlyChecks, (object) currentAPPayment.DocType, (object) currentAPSetup.EarlyChecks, (object) currentAPPayment.DocType, (object) currentAPSetup.EarlyChecks))
      vendDocs.Add((PXResult<APInvoice>) pxResult);
    vendDocs.Sort((Comparison<PXResult<APInvoice>>) ((a, b) => this.CompareVendorDocs(currentAPPayment, PXResult.Unwrap<APInvoice>((object) a), PXResult.Unwrap<APInvoice>((object) b), PXResult.Unwrap<APTran>((object) a), PXResult.Unwrap<APTran>((object) b))));
    return (PXResultset<APInvoice>) vendDocs;
  }

  public virtual void AddFiters(APPayment currentAPPayment, PXSelectBase<APInvoice> cmd)
  {
    switch (currentAPPayment.DocType)
    {
      case "REF":
        cmd.WhereAnd<Where<APInvoice.docType, Equal<APDocType.debitAdj>>>();
        break;
      case "PPM":
        cmd.WhereAnd<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.docType, Equal<APDocType.invoice>>>>, PX.Data.Or<BqlOperand<APInvoice.docType, IBqlString>.IsEqual<APDocType.creditAdj>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APInvoice.pendingPayment, IBqlBool>.IsEqual<True>>>>>();
        break;
      case "CHK":
        cmd.WhereAnd<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.docType, Equal<APDocType.invoice>>>>, PX.Data.Or<BqlOperand<APInvoice.docType, IBqlString>.IsEqual<APDocType.debitAdj>>>, PX.Data.Or<BqlOperand<APInvoice.docType, IBqlString>.IsEqual<APDocType.creditAdj>>>, PX.Data.Or<BqlOperand<APInvoice.docType, IBqlString>.IsEqual<APDocType.prepayment>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APInvoice.pendingPayment, IBqlBool>.IsEqual<True>>>>>();
        break;
      default:
        cmd.WhereAnd<Where<True, Equal<False>>>();
        break;
    }
  }

  public virtual int CompareVendorDocs(
    APPayment currentAPPayment,
    APInvoice aInvoice,
    APInvoice bInvoice,
    APTran aTran,
    APTran bTran)
  {
    int num1 = 0;
    int num2 = 0;
    Decimal? curyOrigDocAmt1 = currentAPPayment.CuryOrigDocAmt;
    Decimal num3 = 0M;
    int num4;
    int num5;
    if (curyOrigDocAmt1.GetValueOrDefault() > num3 & curyOrigDocAmt1.HasValue)
    {
      num4 = num1 + (aInvoice.DocType == "ADR" ? 0 : 10000);
      num5 = num2 + (bInvoice.DocType == "ADR" ? 0 : 10000);
    }
    else
    {
      num4 = num1 + (aInvoice.DocType == "ADR" ? 10000 : 0);
      num5 = num2 + (bInvoice.DocType == "ADR" ? 10000 : 0);
    }
    System.DateTime? dueDate = aInvoice.DueDate;
    System.DateTime dateTime1 = dueDate ?? System.DateTime.MinValue;
    dueDate = bInvoice.DueDate;
    System.DateTime dateTime2 = dueDate ?? System.DateTime.MinValue;
    int num6 = num4 + (1 + dateTime1.CompareTo(dateTime2)) / 2 * 1000;
    int num7 = num5 + (1 - dateTime1.CompareTo(dateTime2)) / 2 * 1000;
    object refNbr1 = (object) aInvoice.RefNbr;
    object refNbr2 = (object) bInvoice.RefNbr;
    int num8 = num6 + (1 + ((IComparable) refNbr1).CompareTo(refNbr2)) / 2 * 100;
    int num9 = num7 + (1 - ((IComparable) refNbr1).CompareTo(refNbr2)) / 2 * 100;
    Decimal? curyOrigDocAmt2 = currentAPPayment.CuryOrigDocAmt;
    Decimal num10 = 0M;
    if (curyOrigDocAmt2.GetValueOrDefault() <= num10 & curyOrigDocAmt2.HasValue && aInvoice.PaymentsByLinesAllowed.GetValueOrDefault() && aInvoice.DocType == bInvoice.DocType && aInvoice.RefNbr == bInvoice.RefNbr)
    {
      Decimal? nullable = aInvoice.SignBalance;
      if (nullable.HasValue)
      {
        nullable = aInvoice.SignBalance;
        Decimal num11 = 0M;
        if (!(nullable.GetValueOrDefault() == num11 & nullable.HasValue))
        {
          nullable = aTran.CuryTranAmt;
          object valueOrDefault1 = (object) nullable.GetValueOrDefault();
          nullable = bTran.CuryTranAmt;
          object valueOrDefault2 = (object) nullable.GetValueOrDefault();
          nullable = aInvoice.SignBalance;
          Decimal num12 = 1M;
          if (nullable.GetValueOrDefault() == num12 & nullable.HasValue)
          {
            num8 += (1 - ((IComparable) valueOrDefault1).CompareTo(valueOrDefault2)) / 2 * 10;
            num9 += (1 + ((IComparable) valueOrDefault1).CompareTo(valueOrDefault2)) / 2 * 10;
          }
          else
          {
            num8 += (1 + ((IComparable) valueOrDefault1).CompareTo(valueOrDefault2)) / 2 * 10;
            num9 += (1 - ((IComparable) valueOrDefault1).CompareTo(valueOrDefault2)) / 2 * 10;
          }
        }
      }
    }
    int? lineNbr = aTran.LineNbr;
    object valueOrDefault3 = (object) lineNbr.GetValueOrDefault();
    lineNbr = bTran.LineNbr;
    object valueOrDefault4 = (object) lineNbr.GetValueOrDefault();
    return (num8 + (1 + ((IComparable) valueOrDefault3).CompareTo(valueOrDefault4)) / 2).CompareTo(num9 + (1 - ((IComparable) valueOrDefault3).CompareTo(valueOrDefault4)) / 2);
  }
}

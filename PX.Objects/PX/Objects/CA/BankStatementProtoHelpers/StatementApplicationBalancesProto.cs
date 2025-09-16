// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankStatementProtoHelpers.StatementApplicationBalancesProto
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA.MultiCurrency;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Extensions.MultiCurrency;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA.BankStatementProtoHelpers;

public class StatementApplicationBalancesProto : PXGraphExtension<CABankTransactionsMaint>
{
  private PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo> curyInfoSelect
  {
    get => (PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.Base.CurrencyInfo_CuryInfoID;
  }

  private IPXCurrencyHelper CuryHelper
  {
    get
    {
      return (IPXCurrencyHelper) ((PXGraph) this.Base).GetExtension<CABankTransactionsMaintMultiCurrency>();
    }
  }

  private APPaymentBalanceCalculator APPaymentBalanceCalculator
  {
    get => new APPaymentBalanceCalculator(this.CuryHelper);
  }

  public void UpdateBalance(CABankTran currentDetail, CABankTranAdjustment adj, bool isCalcRGOL)
  {
    if (currentDetail.OrigModule == "AP")
    {
      using (IEnumerator<PXResult<PX.Objects.AP.APInvoice>> enumerator = PXSelectBase<PX.Objects.AP.APInvoice, PXSelectJoin<PX.Objects.AP.APInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<PX.Objects.AP.APInvoice.curyInfoID>>>, Where<PX.Objects.AP.APInvoice.docType, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<PX.Objects.AP.APInvoice.refNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) adj.AdjdDocType,
        (object) adj.AdjdRefNbr
      }).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          this.UpdateBalanceFromAPDocument<PX.Objects.AP.APInvoice>(PXResult<PX.Objects.AP.APInvoice, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit((PXResult<PX.Objects.AP.APInvoice, PX.Objects.CM.Extensions.CurrencyInfo>) enumerator.Current), adj, isCalcRGOL);
          return;
        }
      }
      foreach (PXResult<PX.Objects.AP.APPayment, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in PXSelectBase<PX.Objects.AP.APPayment, PXSelectJoin<PX.Objects.AP.APPayment, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<PX.Objects.AP.APPayment.curyInfoID>>>, Where<PX.Objects.AP.APPayment.docType, Equal<Required<PX.Objects.AP.APPayment.docType>>, And<PX.Objects.AP.APPayment.refNbr, Equal<Required<PX.Objects.AP.APPayment.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) adj.AdjdDocType,
        (object) adj.AdjdRefNbr
      }))
        this.UpdateBalanceFromAPDocument<PX.Objects.AP.APPayment>(PXResult<PX.Objects.AP.APPayment, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult), adj, isCalcRGOL);
    }
    else
    {
      if (!(currentDetail.OrigModule == "AR"))
        return;
      using (IEnumerator<PXResult<PX.Objects.AR.ARInvoice>> enumerator = PXSelectBase<PX.Objects.AR.ARInvoice, PXSelectJoin<PX.Objects.AR.ARInvoice, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.ARInvoice.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>, Where2<Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>, Or<PX.Objects.AR.Customer.parentBAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>, And<PX.Objects.AR.ARInvoice.docType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>>>>>.Config>.Select((PXGraph) this.Base, new object[4]
      {
        (object) currentDetail.PayeeBAccountID,
        (object) currentDetail.PayeeBAccountID,
        (object) adj.AdjdDocType,
        (object) adj.AdjdRefNbr
      }).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PX.Objects.AR.ARInvoice invoice = PXResult<PX.Objects.AR.ARInvoice>.op_Implicit(enumerator.Current);
          this.UpdateBalanceFromARDocument<PX.Objects.AR.ARInvoice>(adj, invoice, isCalcRGOL);
          return;
        }
      }
      foreach (PXResult<PX.Objects.AR.ARPayment> pxResult in PXSelectBase<PX.Objects.AR.ARPayment, PXSelectJoin<PX.Objects.AR.ARPayment, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.ARPayment.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>, Where2<Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>, Or<PX.Objects.AR.Customer.parentBAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>, And<PX.Objects.AR.ARPayment.docType, Equal<Required<PX.Objects.AR.ARPayment.docType>>, And<PX.Objects.AR.ARPayment.refNbr, Equal<Required<PX.Objects.AR.ARPayment.refNbr>>>>>>.Config>.Select((PXGraph) this.Base, new object[4]
      {
        (object) currentDetail.PayeeBAccountID,
        (object) currentDetail.PayeeBAccountID,
        (object) adj.AdjdDocType,
        (object) adj.AdjdRefNbr
      }))
      {
        PX.Objects.AR.ARPayment invoice = PXResult<PX.Objects.AR.ARPayment>.op_Implicit(pxResult);
        this.UpdateBalanceFromARDocument<PX.Objects.AR.ARPayment>(adj, invoice, isCalcRGOL);
      }
    }
  }

  private void UpdateBalanceFromAPDocument<T>(T invoice, CABankTranAdjustment adj, bool isCalcRGOL) where T : APRegister, IInvoice, new()
  {
    APAdjust apAdjust = new APAdjust()
    {
      AdjdRefNbr = adj.AdjdRefNbr,
      AdjdDocType = adj.AdjdDocType
    };
    StatementApplicationBalancesProto.CopyToAdjust((IAdjustment) apAdjust, adj);
    this.APPaymentBalanceCalculator.CalcBalances<T>(apAdjust, invoice, isCalcRGOL, true, (APTran) null);
    StatementApplicationBalancesProto.CopyToAdjust(adj, (IAdjustment) apAdjust);
    adj.AdjdCuryRate = apAdjust.AdjdCuryRate;
  }

  private void UpdateBalanceFromARDocument<TInvoice>(
    CABankTranAdjustment adj,
    TInvoice invoice,
    bool isCalcRGOL)
    where TInvoice : IInvoice
  {
    ARAdjust arAdjust = new ARAdjust()
    {
      AdjdRefNbr = adj.AdjdRefNbr,
      AdjdDocType = adj.AdjdDocType
    };
    StatementApplicationBalancesProto.CopyToAdjust((IAdjustment) arAdjust, adj);
    this.CalculateBalancesAR<TInvoice>(arAdjust, invoice, isCalcRGOL, false);
    StatementApplicationBalancesProto.CopyToAdjust(adj, (IAdjustment) arAdjust);
    adj.AdjdCuryRate = arAdjust.AdjdCuryRate;
  }

  public void PopulateAdjustmentFieldsAP(CABankTran currentDetail, CABankTranAdjustment adj)
  {
    using (IEnumerator<PXResult<PX.Objects.AP.APInvoice>> enumerator = PXSelectBase<PX.Objects.AP.APInvoice, PXSelectJoin<PX.Objects.AP.APInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<PX.Objects.AP.APInvoice.curyInfoID>>>, Where<PX.Objects.AP.APInvoice.docType, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<PX.Objects.AP.APInvoice.refNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) adj.AdjdDocType,
      (object) adj.AdjdRefNbr
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXResult<PX.Objects.AP.APInvoice, PX.Objects.CM.Extensions.CurrencyInfo> current = (PXResult<PX.Objects.AP.APInvoice, PX.Objects.CM.Extensions.CurrencyInfo>) enumerator.Current;
        this.PopulateAP<PX.Objects.AP.APInvoice>(PXResult<PX.Objects.AP.APInvoice, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(current), PXResult<PX.Objects.AP.APInvoice, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(current), currentDetail, adj);
        return;
      }
    }
    foreach (PXResult<PX.Objects.AP.APPayment, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in PXSelectBase<PX.Objects.AP.APPayment, PXSelectJoin<PX.Objects.AP.APPayment, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<PX.Objects.AP.APPayment.curyInfoID>>>, Where<PX.Objects.AP.APPayment.docType, Equal<Required<PX.Objects.AP.APPayment.docType>>, And<PX.Objects.AP.APPayment.refNbr, Equal<Required<PX.Objects.AP.APPayment.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) adj.AdjdDocType,
      (object) adj.AdjdRefNbr
    }))
      this.PopulateAP<PX.Objects.AP.APPayment>(PXResult<PX.Objects.AP.APPayment, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult), PXResult<PX.Objects.AP.APPayment, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult), currentDetail, adj);
  }

  private void PopulateAP<T>(
    T invoice,
    PX.Objects.CM.Extensions.CurrencyInfo info,
    CABankTran currentDetail,
    CABankTranAdjustment adj)
    where T : APRegister, IInvoice, new()
  {
    PX.Objects.CM.Extensions.CurrencyInfo copy = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(info);
    copy.CuryInfoID = adj.AdjdCuryInfoID;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = (PX.Objects.CM.Extensions.CurrencyInfo) ((PXSelectBase) this.curyInfoSelect).Cache.Update((object) copy);
    currencyInfo1.CuryEffDate = currentDetail.TranDate;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = (PX.Objects.CM.Extensions.CurrencyInfo) ((PXSelectBase) this.curyInfoSelect).Cache.Update((object) currencyInfo1);
    adj.AdjdBranchID = invoice.BranchID;
    adj.AdjdDocDate = invoice.DocDate;
    adj.AdjdFinPeriodID = invoice.FinPeriodID;
    adj.AdjdCuryInfoID = currencyInfo2.CuryInfoID;
    adj.AdjdOrigCuryInfoID = info.CuryInfoID;
    adj.AdjgDocDate = currentDetail.TranDate;
    adj.AdjdAPAcct = invoice.APAccountID;
    adj.AdjdAPSub = invoice.APSubID;
    adj.PaymentsByLinesAllowed = invoice.PaymentsByLinesAllowed;
    adj.CuryOrigDocAmt = invoice.CuryOrigDocAmt;
    APAdjust apAdjust = new APAdjust()
    {
      AdjdRefNbr = adj.AdjdRefNbr,
      AdjdDocType = adj.AdjdDocType,
      AdjdAPAcct = invoice.APAccountID,
      AdjdAPSub = invoice.APSubID
    };
    StatementApplicationBalancesProto.CopyToAdjust((IAdjustment) apAdjust, adj);
    apAdjust.AdjgDocType = !(currentDetail.DrCr == "C") ? "REF" : "CHK";
    adj.AdjgBalSign = apAdjust.AdjgBalSign;
    this.APPaymentBalanceCalculator.CalcBalances<T>(apAdjust, invoice, false, true, (APTran) null);
    Decimal? nullable1 = apAdjust.AdjgDocType == "ADR" ? new Decimal?(0M) : apAdjust.CuryDiscBal;
    Decimal? curyDocBal = apAdjust.CuryDocBal;
    Decimal? nullable2 = apAdjust.CuryWhTaxBal;
    Decimal? nullable3 = curyDocBal.HasValue & nullable2.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable4 = nullable1;
    Decimal? nullable5;
    if (!(nullable3.HasValue & nullable4.HasValue))
    {
      nullable2 = new Decimal?();
      nullable5 = nullable2;
    }
    else
      nullable5 = new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault());
    Decimal? nullable6 = nullable5;
    Decimal? curyUnappliedBal = currentDetail.CuryUnappliedBal;
    if (currentDetail != null)
    {
      nullable4 = apAdjust.AdjgBalSign;
      Decimal num1 = 0M;
      if (nullable4.GetValueOrDefault() < num1 & nullable4.HasValue)
      {
        nullable4 = curyUnappliedBal;
        Decimal num2 = 0M;
        if (nullable4.GetValueOrDefault() < num2 & nullable4.HasValue)
        {
          nullable6 = new Decimal?(Math.Min(nullable6.Value, Math.Abs(curyUnappliedBal.Value)));
          goto label_20;
        }
        goto label_20;
      }
    }
    Decimal? nullable7;
    if (currentDetail != null)
    {
      nullable4 = curyUnappliedBal;
      Decimal num3 = 0M;
      if (nullable4.GetValueOrDefault() > num3 & nullable4.HasValue)
      {
        nullable4 = apAdjust.AdjgBalSign;
        Decimal num4 = 0M;
        if (nullable4.GetValueOrDefault() > num4 & nullable4.HasValue)
        {
          nullable4 = curyUnappliedBal;
          nullable7 = nullable1;
          if (nullable4.GetValueOrDefault() < nullable7.GetValueOrDefault() & nullable4.HasValue & nullable7.HasValue)
          {
            nullable6 = curyUnappliedBal;
            nullable1 = new Decimal?(0M);
            goto label_20;
          }
        }
      }
    }
    if (currentDetail != null)
    {
      nullable7 = curyUnappliedBal;
      Decimal num5 = 0M;
      if (nullable7.GetValueOrDefault() > num5 & nullable7.HasValue)
      {
        nullable7 = apAdjust.AdjgBalSign;
        Decimal num6 = 0M;
        if (nullable7.GetValueOrDefault() > num6 & nullable7.HasValue)
        {
          nullable6 = new Decimal?(Math.Min(nullable6.Value, curyUnappliedBal.Value));
          goto label_20;
        }
      }
    }
    if (currentDetail != null)
    {
      nullable7 = curyUnappliedBal;
      Decimal num7 = 0M;
      if (nullable7.GetValueOrDefault() <= num7 & nullable7.HasValue)
      {
        nullable7 = currentDetail.CuryOrigDocAmt;
        Decimal num8 = 0M;
        if (nullable7.GetValueOrDefault() > num8 & nullable7.HasValue)
          nullable6 = new Decimal?(0M);
      }
    }
label_20:
    apAdjust.CuryAdjgAmt = nullable6;
    apAdjust.CuryAdjgDiscAmt = nullable1;
    apAdjust.CuryAdjgWhTaxAmt = apAdjust.CuryWhTaxBal;
    this.APPaymentBalanceCalculator.CalcBalances<T>(apAdjust, invoice, true, true, (APTran) null);
    StatementApplicationBalancesProto.CopyToAdjust(adj, (IAdjustment) apAdjust);
    adj.AdjdCuryRate = apAdjust.AdjdCuryRate;
  }

  public void PopulateAdjustmentFieldsAR(CABankTran currentDetail, CABankTranAdjustment adj)
  {
    using (IEnumerator<PXResult<PX.Objects.AR.ARInvoice>> enumerator = PXSelectBase<PX.Objects.AR.ARInvoice, PXSelectJoin<PX.Objects.AR.ARInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<PX.Objects.AR.ARInvoice.curyInfoID>>>, Where<PX.Objects.AR.ARInvoice.docType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) adj.AdjdDocType,
      (object) adj.AdjdRefNbr
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXResult<PX.Objects.AR.ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo> current = (PXResult<PX.Objects.AR.ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo>) enumerator.Current;
        this.PopulateAR<PX.Objects.AR.ARInvoice>(PXResult<PX.Objects.AR.ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(current), PXResult<PX.Objects.AR.ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(current), currentDetail, adj);
        return;
      }
    }
    foreach (PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in PXSelectBase<PX.Objects.AR.ARPayment, PXSelectJoin<PX.Objects.AR.ARPayment, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<PX.Objects.AR.ARPayment.curyInfoID>>>, Where<PX.Objects.AR.ARPayment.docType, Equal<Required<PX.Objects.AR.ARPayment.docType>>, And<PX.Objects.AR.ARPayment.refNbr, Equal<Required<PX.Objects.AR.ARPayment.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) adj.AdjdDocType,
      (object) adj.AdjdRefNbr
    }))
      this.PopulateAR<PX.Objects.AR.ARPayment>(PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult), PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult), currentDetail, adj);
  }

  private void PopulateAR<TInvoice>(
    TInvoice invoice,
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo,
    CABankTran currentDetail,
    CABankTranAdjustment adj)
    where TInvoice : ARRegister, IInvoice, new()
  {
    PX.Objects.CM.Extensions.CurrencyInfo copy = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(currencyInfo);
    copy.CuryInfoID = adj.AdjdCuryInfoID;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = (PX.Objects.CM.Extensions.CurrencyInfo) ((PXSelectBase) this.curyInfoSelect).Cache.Update((object) copy);
    currencyInfo1.CuryEffDate = currentDetail.TranDate;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = (PX.Objects.CM.Extensions.CurrencyInfo) ((PXSelectBase) this.curyInfoSelect).Cache.Update((object) currencyInfo1);
    adj.AdjdCuryInfoID = currencyInfo2.CuryInfoID;
    adj.AdjdOrigCuryInfoID = invoice.CuryInfoID;
    adj.AdjdBranchID = invoice.BranchID;
    adj.AdjdDocDate = invoice.DocDate;
    adj.AdjdFinPeriodID = invoice.FinPeriodID;
    adj.AdjdARAcct = invoice.ARAccountID;
    adj.AdjdARSub = invoice.ARSubID;
    Decimal? nullable1 = !(adj.AdjdDocType == "PPI") || invoice.PendingPayment.GetValueOrDefault() ? ARDocType.SignBalance(adj.AdjdDocType) : new Decimal?(-1M);
    CABankTranAdjustment bankTranAdjustment = adj;
    Decimal? nullable2 = ARDocType.SignBalance(currentDetail.DocType);
    Decimal? nullable3 = nullable2.HasValue ? new Decimal?(-nullable2.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable4 = nullable1;
    Decimal? nullable5;
    if (!(nullable3.HasValue & nullable4.HasValue))
    {
      nullable2 = new Decimal?();
      nullable5 = nullable2;
    }
    else
      nullable5 = new Decimal?(nullable3.GetValueOrDefault() * nullable4.GetValueOrDefault());
    bankTranAdjustment.AdjgBalSign = nullable5;
    adj.PaymentsByLinesAllowed = invoice.PaymentsByLinesAllowed;
    adj.CuryOrigDocAmt = invoice.CuryOrigDocAmt;
    ARAdjust arAdjust = new ARAdjust()
    {
      AdjdRefNbr = adj.AdjdRefNbr,
      AdjdDocType = adj.AdjdDocType,
      AdjdARAcct = invoice.ARAccountID,
      AdjdARSub = invoice.ARSubID
    };
    StatementApplicationBalancesProto.CopyToAdjust((IAdjustment) arAdjust, adj);
    this.CalculateBalancesAR<TInvoice>(arAdjust, invoice, false, true);
    nullable4 = arAdjust.CuryDocBal;
    Decimal? nullable6 = arAdjust.CuryDiscBal;
    Decimal? nullable7;
    if (!(nullable4.HasValue & nullable6.HasValue))
    {
      nullable2 = new Decimal?();
      nullable7 = nullable2;
    }
    else
      nullable7 = new Decimal?(nullable4.GetValueOrDefault() - nullable6.GetValueOrDefault());
    Decimal? nullable8 = nullable7;
    Decimal? nullable9 = arAdjust.CuryDiscBal;
    Decimal? curyUnappliedBal = currentDetail.CuryUnappliedBal;
    if (currentDetail != null)
    {
      nullable6 = adj.AdjgBalSign;
      Decimal num1 = 0M;
      if (nullable6.GetValueOrDefault() < num1 & nullable6.HasValue)
      {
        nullable6 = curyUnappliedBal;
        Decimal num2 = 0M;
        if (nullable6.GetValueOrDefault() < num2 & nullable6.HasValue)
        {
          nullable8 = new Decimal?(Math.Min(nullable8.Value, Math.Abs(curyUnappliedBal.Value)));
          goto label_19;
        }
        goto label_19;
      }
    }
    if (currentDetail != null)
    {
      nullable6 = curyUnappliedBal;
      Decimal num3 = 0M;
      if (nullable6.GetValueOrDefault() > num3 & nullable6.HasValue)
      {
        nullable6 = adj.AdjgBalSign;
        Decimal num4 = 0M;
        if (nullable6.GetValueOrDefault() > num4 & nullable6.HasValue)
        {
          nullable8 = new Decimal?(Math.Min(nullable8.Value, curyUnappliedBal.Value));
          nullable2 = nullable8;
          Decimal? nullable10 = nullable9;
          nullable6 = nullable2.HasValue & nullable10.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable10.GetValueOrDefault()) : new Decimal?();
          nullable4 = arAdjust.CuryDocBal;
          if (nullable6.GetValueOrDefault() < nullable4.GetValueOrDefault() & nullable6.HasValue & nullable4.HasValue)
          {
            nullable9 = new Decimal?(0M);
            goto label_19;
          }
          goto label_19;
        }
      }
    }
    if (currentDetail != null)
    {
      nullable4 = curyUnappliedBal;
      Decimal num5 = 0M;
      if (nullable4.GetValueOrDefault() <= num5 & nullable4.HasValue)
      {
        nullable4 = currentDetail.CuryOrigDocAmt;
        Decimal num6 = 0M;
        if (nullable4.GetValueOrDefault() > num6 & nullable4.HasValue)
        {
          nullable8 = new Decimal?(0M);
          nullable9 = new Decimal?(0M);
        }
      }
    }
label_19:
    arAdjust.CuryAdjgAmt = nullable8;
    arAdjust.CuryAdjgDiscAmt = nullable9;
    arAdjust.CuryAdjgWOAmt = new Decimal?(0M);
    this.CalculateBalancesAR<TInvoice>(arAdjust, invoice, true, true);
    StatementApplicationBalancesProto.CopyToAdjust(adj, (IAdjustment) arAdjust);
    adj.AdjdCuryRate = arAdjust.AdjdCuryRate;
  }

  public void CalculateBalancesAR<TInvoice>(
    ARAdjust adj,
    TInvoice invoice,
    bool isCalcRGOL,
    bool DiscOnDiscDate)
    where TInvoice : IInvoice
  {
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Optional<CABankTran.payeeBAccountID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    PaymentBalanceCalculator balanceCalculator = new PaymentBalanceCalculator(this.CuryHelper);
    balanceCalculator.CalcBalances(adj.AdjgCuryInfoID, adj.AdjdCuryInfoID, (IInvoice) invoice, (IAdjustment) adj);
    if (DiscOnDiscDate)
      PaymentEntry.CalcDiscount(adj.AdjgDocDate, (IInvoice) invoice, (IAdjustment) adj);
    PaymentEntry.WarnDiscount<TInvoice, ARAdjust>((PXGraph) this.Base, adj.AdjgDocDate, invoice, adj);
    adj.AdjdCuryRate = new Decimal?(balanceCalculator.GetAdjdCuryRate((IAdjustment) adj));
    if (customer != null && customer.SmallBalanceAllow.GetValueOrDefault() && adj.AdjgDocType != "REF" && adj.AdjdDocType != "CRM")
    {
      Decimal num = this.CuryHelper.GetCurrencyInfo(adj.AdjgCuryInfoID).CuryConvCury(customer.SmallBalanceLimit.GetValueOrDefault());
      adj.CuryWOBal = new Decimal?(num);
      adj.WOBal = customer.SmallBalanceLimit;
    }
    else
    {
      adj.CuryWOBal = new Decimal?(0M);
      adj.WOBal = new Decimal?(0M);
    }
    new PaymentBalanceAjuster(this.CuryHelper).AdjustBalance((IAdjustment) adj);
    if (!isCalcRGOL || adj.Voided.GetValueOrDefault())
      return;
    new PaymentRGOLCalculator(this.CuryHelper, (IAdjustment) adj, adj.ReverseGainLoss).Calculate((IInvoice) invoice);
  }

  public static CABankTranAdjustment CopyToAdjust(CABankTranAdjustment bankAdj, IAdjustment iAdjust)
  {
    bankAdj.AdjgCuryInfoID = iAdjust.AdjgCuryInfoID;
    bankAdj.AdjdCuryInfoID = iAdjust.AdjdCuryInfoID;
    bankAdj.AdjgDocDate = iAdjust.AdjgDocDate;
    bankAdj.DocBal = iAdjust.DocBal;
    bankAdj.CuryDocBal = iAdjust.CuryDocBal;
    bankAdj.CuryDiscBal = iAdjust.CuryDiscBal;
    bankAdj.CuryWhTaxBal = iAdjust.CuryWhTaxBal;
    bankAdj.CuryAdjgAmt = iAdjust.CuryAdjgAmt;
    bankAdj.CuryAdjdAmt = iAdjust.CuryAdjdAmt;
    bankAdj.CuryAdjgDiscAmt = iAdjust.CuryAdjgDiscAmt;
    bankAdj.CuryAdjdDiscAmt = iAdjust.CuryAdjdDiscAmt;
    bankAdj.CuryAdjgWhTaxAmt = iAdjust.CuryAdjgWhTaxAmt;
    bankAdj.AdjdOrigCuryInfoID = iAdjust.AdjdOrigCuryInfoID;
    return bankAdj;
  }

  public static IAdjustment CopyToAdjust(IAdjustment iAdjust, CABankTranAdjustment bankAdj)
  {
    iAdjust.AdjgCuryInfoID = bankAdj.AdjgCuryInfoID;
    iAdjust.AdjdCuryInfoID = bankAdj.AdjdCuryInfoID;
    iAdjust.AdjgDocDate = bankAdj.AdjgDocDate;
    iAdjust.DocBal = bankAdj.DocBal;
    iAdjust.CuryDocBal = bankAdj.CuryDocBal;
    iAdjust.CuryDiscBal = bankAdj.CuryDiscBal;
    iAdjust.CuryWhTaxBal = bankAdj.CuryWhTaxBal;
    iAdjust.CuryAdjgAmt = bankAdj.CuryAdjgAmt;
    iAdjust.CuryAdjdAmt = bankAdj.CuryAdjdAmt;
    iAdjust.CuryAdjgDiscAmt = bankAdj.CuryAdjgDiscAmt;
    iAdjust.CuryAdjdDiscAmt = bankAdj.CuryAdjdDiscAmt;
    iAdjust.CuryAdjgWhTaxAmt = bankAdj.CuryAdjgWhTaxAmt;
    iAdjust.AdjdOrigCuryInfoID = bankAdj.AdjdOrigCuryInfoID;
    return iAdjust;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PaymentMethodAccountHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CA;

public class PaymentMethodAccountHelper
{
  public static void VerifyQuickBatchGenerationOnRowPersisting(
    PXCache sender,
    PaymentMethodAccount row)
  {
    if (PaymentMethodAccountHelper.CheckIfErrorExists<PaymentMethodAccount.aPQuickBatchGeneration>(sender, row, (PXErrorLevel) 4))
      return;
    if (row.APQuickBatchGeneration.GetValueOrDefault() && !row.APAutoNextNbr.GetValueOrDefault())
    {
      sender.RaiseExceptionHandling<PaymentMethodAccount.aPQuickBatchGeneration>((object) row, (object) row.APQuickBatchGeneration, (Exception) new PXSetPropertyException("The AP Suggest Next Number check box cannot be cleared if the Quick Batch Generation check box is selected."));
    }
    else
    {
      string nextNumber = row.APLastRefNbr;
      if (row.APQuickBatchGeneration.GetValueOrDefault() && string.IsNullOrEmpty(nextNumber))
      {
        sender.RaiseExceptionHandling<PaymentMethodAccount.aPQuickBatchGeneration>((object) row, (object) row.APQuickBatchGeneration, (Exception) new PXSetPropertyException("The AP Last Reference Number cannot be empty if the Quick Batch Generation check box is selected."));
      }
      else
      {
        if (!row.APQuickBatchGeneration.GetValueOrDefault() || AutoNumberAttribute.TryToGetNextNumber(nextNumber, 1, out nextNumber))
          return;
        if (AutoNumberAttribute.CheckIfNumberEndsDigit(row.APLastRefNbr))
          sender.RaiseExceptionHandling<PaymentMethodAccount.aPQuickBatchGeneration>((object) row, (object) row.APQuickBatchGeneration, (Exception) new PXSetPropertyException("AP/PR Last Reference Number cannot be incremented because the last possible value of the sequence was reached."));
        else
          sender.RaiseExceptionHandling<PaymentMethodAccount.aPQuickBatchGeneration>((object) row, (object) row.APQuickBatchGeneration, (Exception) new PXSetPropertyException("The value in the AP Last Reference Number box must end with a number."));
      }
    }
  }

  public static void APQuickBatchGenerationFieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.NewValue as bool?).GetValueOrDefault())
      return;
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    try
    {
      PaymentMethodAccountHelper.VerifyAPLastReferenceNumber<PaymentMethodAccount.aPQuickBatchGeneration>((bool?) row?.APAutoNextNbr, row?.APLastRefNbr, (string) null);
    }
    catch (PXSetPropertyException ex)
    {
      cache.RaiseExceptionHandling<PaymentMethodAccount.aPQuickBatchGeneration>(e.Row, e.NewValue, (Exception) ex);
    }
  }

  public static bool TryToVerifyAPLastReferenceNumber<TField>(
    bool? suggestNextNbr,
    string aPLastRefNbr,
    string cashAccountCD,
    out PXException error)
    where TField : IBqlField
  {
    try
    {
      PaymentMethodAccountHelper.VerifyAPLastReferenceNumber<TField>(suggestNextNbr, aPLastRefNbr, cashAccountCD);
      error = (PXException) null;
      return true;
    }
    catch (PXSetPropertyException ex)
    {
      error = (PXException) ex;
      return false;
    }
  }

  public static void VerifyAPLastReferenceNumber<TField>(
    bool? suggestNextNbr,
    string aPLastRefNbr,
    string cashAccountCD)
    where TField : IBqlField
  {
    string nextNumber = aPLastRefNbr;
    bool flag = !string.IsNullOrEmpty(cashAccountCD);
    if (!suggestNextNbr.GetValueOrDefault())
    {
      string str;
      if (!flag)
        str = "The AP Suggest Next Number check box cannot be cleared if the Quick Batch Generation check box is selected.";
      else
        str = PXLocalizer.LocalizeFormat("To generate a batch automatically, select the Suggest Next Number check box for the {0} cash account on the Payment Methods (CA204000) form.", new object[1]
        {
          (object) cashAccountCD
        });
      throw new PXSetPropertyException<TField>(str);
    }
    if (string.IsNullOrEmpty(aPLastRefNbr))
    {
      string str;
      if (!flag)
        str = "The AP Last Reference Number cannot be empty if the Quick Batch Generation check box is selected.";
      else
        str = PXLocalizer.LocalizeFormat("To generate a batch automatically, specify AP Last Reference Number for the {0} cash account on the Payment Methods (CA204000) form.", new object[1]
        {
          (object) cashAccountCD
        });
      throw new PXSetPropertyException<TField>(str);
    }
    if (AutoNumberAttribute.TryToGetNextNumber(aPLastRefNbr, 1, out nextNumber))
      return;
    if (AutoNumberAttribute.CheckIfNumberEndsDigit(aPLastRefNbr))
    {
      string str;
      if (!flag)
        str = "AP/PR Last Reference Number cannot be incremented because the last possible value of the sequence was reached.";
      else
        str = PXLocalizer.LocalizeFormat("To generate a batch automatically, correct AP Last Reference Number for the {0} cash account on the Payment Methods (CA204000) form. AP Last Reference Number cannot be incremented because the last possible value of the sequence was reached.", new object[1]
        {
          (object) cashAccountCD
        });
      throw new PXSetPropertyException<TField>(str);
    }
    string str1;
    if (!flag)
      str1 = "The value in the AP Last Reference Number box must end with a number.";
    else
      str1 = PXLocalizer.LocalizeFormat("To generate a batch automatically, correct AP Last Reference Number for the {0} cash account on the Payment Methods (CA204000) form. AP Last Reference Number must end with a number.", new object[1]
      {
        (object) cashAccountCD
      });
    throw new PXSetPropertyException<TField>(str1);
  }

  public static bool TryToVerifyAPLastReferenceNumber(
    PXGraph graph,
    string paymentMethodID,
    int? cashAccountID,
    out string errorMessage,
    int count = 1,
    string filterLastRefNbr = "")
  {
    try
    {
      PaymentMethodAccountHelper.VerifyAPLastReferenceNumber(graph, paymentMethodID, cashAccountID, count, filterLastRefNbr, true);
      errorMessage = string.Empty;
      return true;
    }
    catch (PXException ex)
    {
      errorMessage = ((Exception) ex).Message;
      return false;
    }
  }

  public static void VerifyAPLastReferenceNumber(
    PXGraph graph,
    string paymentMethodID,
    int? cashAccountID,
    int count = 1,
    string filterLastRefNbr = "",
    bool skipSuggestNextNbrCheck = false)
  {
    if (string.IsNullOrEmpty(paymentMethodID) || !cashAccountID.HasValue)
      return;
    PXResult<PaymentMethodAccount, CashAccount> pxResult = (PXResult<PaymentMethodAccount, CashAccount>) PXResultset<PaymentMethodAccount>.op_Implicit(PXSelectBase<PaymentMethodAccount, PXSelectReadonly2<PaymentMethodAccount, InnerJoin<CashAccount, On<PaymentMethodAccount.cashAccountID, Equal<CashAccount.cashAccountID>>>, Where<PaymentMethodAccount.cashAccountID, Equal<Required<PaymentMethodAccount.cashAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Required<PaymentMethodAccount.paymentMethodID>>>>>.Config>.Select(graph, new object[2]
    {
      (object) cashAccountID,
      (object) paymentMethodID
    }));
    PaymentMethodAccount paymentMethodAccount = PXResult<PaymentMethodAccount, CashAccount>.op_Implicit(pxResult);
    CashAccount cashAccount = PXResult<PaymentMethodAccount, CashAccount>.op_Implicit(pxResult);
    bool? suggestNextNbr = (bool?) paymentMethodAccount?.APAutoNextNbr;
    if (skipSuggestNextNbrCheck)
      suggestNextNbr = new bool?(true);
    PaymentMethodAccountHelper.VerifyAPLastRefNbr(suggestNextNbr, string.IsNullOrEmpty(filterLastRefNbr) ? paymentMethodAccount.APLastRefNbr : filterLastRefNbr, cashAccount.CashAccountCD, count);
  }

  public static void VerifyAPLastReferenceNumberSettings(
    PXGraph graph,
    string paymentMethodID,
    int? cashAccountID,
    int count,
    string filterLastRefNbr)
  {
    PXResult<PaymentMethodAccount, CashAccount> pxResult = (PXResult<PaymentMethodAccount, CashAccount>) PXResultset<PaymentMethodAccount>.op_Implicit(PXSelectBase<PaymentMethodAccount, PXSelectReadonly2<PaymentMethodAccount, InnerJoin<CashAccount, On<PaymentMethodAccount.cashAccountID, Equal<CashAccount.cashAccountID>>>, Where<PaymentMethodAccount.cashAccountID, Equal<Required<PaymentMethodAccount.cashAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Required<PaymentMethodAccount.paymentMethodID>>>>>.Config>.Select(graph, new object[2]
    {
      (object) cashAccountID,
      (object) paymentMethodID
    }));
    PaymentMethodAccount paymentMethodAccount = PXResult<PaymentMethodAccount, CashAccount>.op_Implicit(pxResult);
    CashAccount cashAccount = PXResult<PaymentMethodAccount, CashAccount>.op_Implicit(pxResult);
    PaymentMethodAccountHelper.VerifyAPLastRefNbr(new bool?(true), paymentMethodAccount.APAutoNextNbr.GetValueOrDefault() ? paymentMethodAccount.APLastRefNbr : filterLastRefNbr, cashAccount.CashAccountCD, count);
  }

  public static void VerifyAPLastRefNbr(
    bool? suggestNextNbr,
    string aPLastRefNbr,
    string cashAccountCD,
    int count = 1)
  {
    string nextNumber = aPLastRefNbr;
    if (!suggestNextNbr.GetValueOrDefault())
      throw new PXException("To generate a batch automatically, select the Suggest Next Number check box for the {0} cash account on the Payment Methods (CA204000) form.", new object[1]
      {
        (object) cashAccountCD
      });
    if (string.IsNullOrEmpty(aPLastRefNbr))
      throw new PXException("To generate a batch automatically, specify AP Last Reference Number for the {0} cash account on the Payment Methods (CA204000) form.", new object[1]
      {
        (object) cashAccountCD
      });
    if (AutoNumberAttribute.TryToGetNextNumber(aPLastRefNbr, count, out nextNumber))
      return;
    if (AutoNumberAttribute.CheckIfNumberEndsDigit(aPLastRefNbr))
      throw new PXException("To generate a batch automatically, correct AP Last Reference Number for the {0} cash account on the Payment Methods (CA204000) form. AP Last Reference Number cannot be incremented because the last possible value of the sequence was reached.", new object[1]
      {
        (object) cashAccountCD
      });
    throw new PXException("To generate a batch automatically, correct AP Last Reference Number for the {0} cash account on the Payment Methods (CA204000) form. AP Last Reference Number must end with a number.", new object[1]
    {
      (object) cashAccountCD
    });
  }

  public static bool CheckIfErrorExists<TField>(
    PXCache cache,
    PaymentMethodAccount paymentMethodAccount,
    PXErrorLevel expectedErrorLevel)
    where TField : IBqlField
  {
    return PXUIFieldAttribute.GetErrorWithLevel<TField>(cache, (object) paymentMethodAccount).Item2 > expectedErrorLevel;
  }

  public static void VerifyAPAutoNextNbr(
    PXCache cache,
    PaymentMethodAccount paymentMethodAccount,
    PaymentMethod paymentMethod)
  {
    if (PaymentMethodAccountHelper.CheckIfErrorExists<PaymentMethodAccount.aPAutoNextNbr>(cache, paymentMethodAccount, (PXErrorLevel) 2))
      return;
    PXException pxException = (PXException) null;
    if (!paymentMethodAccount.APAutoNextNbr.GetValueOrDefault() && paymentMethod?.APAdditionalProcessing == "B")
      pxException = (PXException) new PXSetPropertyException<PaymentMethodAccount.aPAutoNextNbr>("The AP/PR Suggest Next Number check box should be selected.", (PXErrorLevel) 2);
    string error = PXUIFieldAttribute.GetError<PaymentMethodAccount.aPAutoNextNbr>(cache, (object) paymentMethodAccount);
    if (pxException != null)
    {
      cache.RaiseExceptionHandling<PaymentMethodAccount.aPAutoNextNbr>((object) paymentMethodAccount, (object) paymentMethodAccount.APAutoNextNbr, (Exception) pxException);
    }
    else
    {
      if (!(error == PXLocalizer.Localize("The AP/PR Suggest Next Number check box should be selected.")))
        return;
      cache.RaiseExceptionHandling<PaymentMethodAccount.aPAutoNextNbr>((object) paymentMethodAccount, (object) paymentMethodAccount.APAutoNextNbr, (Exception) null);
    }
  }
}

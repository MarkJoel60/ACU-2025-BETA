// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTransactionsHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.CA;

public class CABankTransactionsHelper
{
  public static bool IsMatchedToExpenseReceipt(CABankTranMatch match)
  {
    return match != null && match.DocModule == "EP" && match.DocType == "ECD";
  }

  public static bool IsMatchedToInvoice(CABankTran tran, CABankTranMatch match)
  {
    if (match == null)
      return true;
    return !match.CATranID.HasValue && (!(match.DocType == "CBT") || !(match.DocModule == "AP")) && !CABankTransactionsHelper.IsMatchedToExpenseReceipt(match);
  }
}

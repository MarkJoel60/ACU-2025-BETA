// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountRules
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL;

public static class AccountRules
{
  public static bool IsCreditBalance(string accountType)
  {
    if (AccountRules.IsGIRLSAccount(accountType))
      return true;
    if (AccountRules.IsDEALAccount(accountType))
      return false;
    throw new PXException("Unknown account type {0}", new object[1]
    {
      (object) accountType
    });
  }

  /// <summary>
  /// Returns <c>true</c> if the provided Account Type has Credit balance,
  /// that is belongs to the Gains, Income, Revenues, Liabilities and Stock-holder equity group.
  /// In Acumatica, Gains, Revenues and Stock-holder equity account types are not implemented.
  /// </summary>
  /// <param name="accountType"><see cref="P:PX.Objects.GL.Account.Type" /></param>
  public static bool IsGIRLSAccount(string accountType) => accountType == "I" || accountType == "L";

  /// <summary>
  /// Returns <c>true</c> if the provided Account Type has Debit balance,
  /// that is belongs to the Dividends, Expenses, Assets and Losses group.
  /// In Acumatica, Dividends and Losses account types are not implemented.
  /// </summary>
  /// <param name="accountType"><see cref="P:PX.Objects.GL.Account.Type" /></param>
  public static bool IsDEALAccount(string accountType) => accountType == "E" || accountType == "A";

  public static Decimal CalcSaldo(string aAcctType, Decimal aDebitAmt, Decimal aCreditAmt)
  {
    return !AccountRules.IsCreditBalance(aAcctType) ? aDebitAmt - aCreditAmt : aCreditAmt - aDebitAmt;
  }
}

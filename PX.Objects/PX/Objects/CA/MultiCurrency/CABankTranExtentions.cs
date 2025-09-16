// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.MultiCurrency.CABankTranExtentions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.CA.MultiCurrency;

public static class CABankTranExtentions
{
  public static bool CanCreateCurrencyInfo(this CABankTran cABankTran)
  {
    if (cABankTran == null)
      return false;
    bool? nullable = cABankTran.CreateDocument;
    if (nullable.GetValueOrDefault())
      return true;
    nullable = cABankTran.MultipleMatching;
    return nullable.GetValueOrDefault();
  }
}

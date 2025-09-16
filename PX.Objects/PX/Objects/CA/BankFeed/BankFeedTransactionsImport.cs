// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.BankFeedTransactionsImport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;

#nullable disable
namespace PX.Objects.CA.BankFeed;

[PXHidden]
[PXInternalUseOnly]
public class BankFeedTransactionsImport : CABankTransactionsImport
{
  [PXMergeAttributes]
  [PXDefault]
  [CashAccountDisabledBranchRestrictions(null, typeof (Search<CashAccount.cashAccountID, Where<CashAccount.active, Equal<True>, And<Match<Current<AccessInfo.userName>>>>>))]
  protected override void CABankTranHeader_CashAccountID_CacheAttached(PXCache sender)
  {
  }
}

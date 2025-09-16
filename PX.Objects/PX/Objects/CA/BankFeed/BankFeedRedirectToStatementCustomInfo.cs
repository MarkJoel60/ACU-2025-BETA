// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.BankFeedRedirectToStatementCustomInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA.BankFeed;

internal class BankFeedRedirectToStatementCustomInfo : IPXCustomInfo
{
  private readonly Dictionary<int, string> _lastUpdatedStatements;

  public BankFeedRedirectToStatementCustomInfo(Dictionary<int, string> lastUpdatedStatements)
  {
    this._lastUpdatedStatements = lastUpdatedStatements;
  }

  public void Complete(PXLongRunStatus status, PXGraph graph)
  {
    if (!(graph is CABankTransactionsImport transactionsImport))
      return;
    CABankTranHeader current = ((PXSelectBase<CABankTranHeader>) transactionsImport.Header).Current;
    if (current == null || !current.CashAccountID.HasValue || status != 2)
      return;
    int key = current.CashAccountID.Value;
    if (this._lastUpdatedStatements == null || !this._lastUpdatedStatements.ContainsKey(key))
      return;
    string updatedStatement = this._lastUpdatedStatements[key];
    if (updatedStatement != null && current.RefNbr != updatedStatement)
    {
      ((PXGraph) transactionsImport).Clear();
      CABankTranHeader caBankTranHeader = PXResultset<CABankTranHeader>.op_Implicit(PXSelectBase<CABankTranHeader, PXSelect<CABankTranHeader, Where<CABankTranHeader.refNbr, Equal<Required<CABankTranHeader.refNbr>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
      {
        (object) updatedStatement
      }));
      ((PXSelectBase<CABankTranHeader>) transactionsImport.Header).Current = caBankTranHeader;
      throw new PXRedirectRequiredException((PXGraph) transactionsImport, string.Empty);
    }
  }
}

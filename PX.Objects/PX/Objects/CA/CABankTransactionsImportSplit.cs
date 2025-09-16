// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTransactionsImportSplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CA;

public class CABankTransactionsImportSplit : PXGraphExtension<CABankTransactionsImport>
{
  public PXSelect<CABankTran, Where<CABankTran.headerRefNbr, Equal<Current<CABankTranHeader.refNbr>>, And<CABankTran.tranType, Equal<Current<CABankTranHeader.tranType>>, And<CABankTranSplit.parentTranID, IsNull>>>> Details;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.bankTransactionSplits>();

  [PXMergeAttributes]
  [PXUIField]
  public virtual void _(Events.CacheAttached<CABankTran.curyCreditAmt> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  public virtual void _(Events.CacheAttached<CABankTran.curyDebitAmt> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  public virtual void _(Events.CacheAttached<CABankTran.processed> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  public virtual void _(Events.CacheAttached<CABankTran.documentMatched> e)
  {
  }

  [PXOverride]
  public virtual void CABankTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CABankTran row = (CABankTran) e.Row;
    CABankTran current = ((PXSelectBase<CABankTran>) this.Base.Details).Current;
    CABankTranSplit extension = current != null ? PXCacheEx.GetExtension<CABankTranSplit>((IBqlTable) current) : (CABankTranSplit) null;
    if ((extension != null ? (extension.Splitted.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
  }

  [PXOverride]
  public virtual void CABankTran_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    object row = e.Row;
    CABankTran current = ((PXSelectBase<CABankTran>) this.Base.Details).Current;
    CABankTranSplit extension = current != null ? PXCacheEx.GetExtension<CABankTranSplit>((IBqlTable) current) : (CABankTranSplit) null;
    if ((extension != null ? (extension.Splitted.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException("The transaction cannot be deleted because it has been split. To delete the transaction, delete its child transactions first.");
  }
}

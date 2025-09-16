// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FASplitsInq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.FA;

[TableAndChartDashboardType]
public class FASplitsInq : PXGraph<FASplitsInq>
{
  public PXCancel<AccountFilter> Cancel;
  public PXFilter<AccountFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelectJoin<Transact, LeftJoin<FixedAsset, On<FATran.assetID, Equal<FixedAsset.assetID>>>, Where<FATran.origin, Equal<FARegister.origin.split>, And<FATran.released, Equal<True>, And<Where<FixedAsset.assetID, Equal<Current<AccountFilter.assetID>>, And<FATran.tranType, Equal<FATran.tranType.purchasingMinus>, Or<FixedAsset.splittedFrom, Equal<Current<AccountFilter.assetID>>, And<FATran.tranType, Equal<FATran.tranType.purchasingPlus>>>>>>>>> Transactions;

  public FASplitsInq()
  {
    ((PXSelectBase) this.Transactions).Cache.AllowInsert = false;
    ((PXSelectBase) this.Transactions).Cache.AllowUpdate = false;
    ((PXSelectBase) this.Transactions).Cache.AllowDelete = false;
  }

  public virtual IEnumerable transactions(PXAdapter adapter)
  {
    FASplitsInq faSplitsInq = this;
    AccountFilter current = ((PXSelectBase<AccountFilter>) faSplitsInq.Filter).Current;
    if (current != null)
    {
      PXSelectBase<Transact> pxSelectBase = (PXSelectBase<Transact>) new PXSelectJoin<Transact, LeftJoin<FixedAsset, On<FATran.assetID, Equal<FixedAsset.assetID>>>, Where<FATran.origin, Equal<FARegister.origin.split>, And<FATran.released, Equal<True>, And<Where<FixedAsset.assetID, Equal<Current<AccountFilter.assetID>>, And<FATran.tranType, Equal<FATran.tranType.purchasingMinus>, Or<FixedAsset.splittedFrom, Equal<Current<AccountFilter.assetID>>, And<FATran.tranType, Equal<FATran.tranType.purchasingPlus>>>>>>>>>((PXGraph) faSplitsInq);
      if (current.BookID.HasValue)
        pxSelectBase.WhereAnd<Where<FATran.bookID, Equal<Current<AccountFilter.bookID>>>>();
      foreach (PXResult<Transact> pxResult in pxSelectBase.Select(Array.Empty<object>()))
      {
        Transact transact = PXResult<Transact>.op_Implicit(pxResult);
        if (transact.TranType == "P+")
        {
          transact.DebitAmt = transact.TranAmt;
          transact.CreditAmt = new Decimal?(0M);
          transact.AccountID = transact.DebitAccountID;
          transact.SubID = transact.DebitSubID;
        }
        else if (transact.TranType == "P-")
        {
          transact.CreditAmt = transact.TranAmt;
          transact.DebitAmt = new Decimal?(0M);
          transact.AccountID = transact.CreditAccountID;
          transact.SubID = transact.CreditSubID;
        }
        else
          continue;
        yield return (object) transact;
      }
    }
  }

  public virtual void AccountFilter_AssetID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<AccountFilter.bookID>(e.Row);
  }
}

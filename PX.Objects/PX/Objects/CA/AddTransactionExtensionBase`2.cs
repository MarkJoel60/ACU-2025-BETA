// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.AddTransactionExtensionBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections;

#nullable disable
namespace PX.Objects.CA;

public abstract class AddTransactionExtensionBase<Graph, PrimaryEntity> : PXGraphExtension<Graph>
  where Graph : PXGraph
  where PrimaryEntity : class, IBqlTable, new()
{
  public PXFilter<AddTrxFilter> AddFilter;
  public PXAction<PrimaryEntity> ReleaseTransaction;

  [PXUIField]
  [PXButton]
  public virtual IEnumerable releaseTransaction(PXAdapter adapter) => adapter.Get();

  public virtual void AddTrxFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    AddTrxFilter row = (AddTrxFilter) e.Row;
    if (row == null)
      return;
    PXAction<PrimaryEntity> releaseTransaction = this.ReleaseTransaction;
    bool? nullable = row.Hold;
    int num;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.ExcludeFromApproval;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    ((PXAction) releaseTransaction).SetEnabled(num != 0);
  }

  public virtual void AddTrxFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    AddTrxFilter row = e.Row as AddTrxFilter;
    AddTrxFilter oldRow = e.OldRow as AddTrxFilter;
    if (!(row.OrigModule == "CA"))
      return;
    bool? hold = oldRow.Hold;
    if (!hold.GetValueOrDefault())
      return;
    hold = row.Hold;
    if (hold.GetValueOrDefault())
      return;
    AddTrxFilter copy = (AddTrxFilter) cache.CreateCopy((object) row);
    copy.ExcludeFromApproval = new bool?(!this.IsApprovalRequired());
    cache.Update((object) copy);
  }

  protected virtual bool IsApprovalRequired()
  {
    return AddTrxFilterApprovalsHelper.IsApprovalRequired((PXGraph) this.Base);
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Reclassification.UI.ReclassificationHistoryInq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.Reclassification.UI;

public class ReclassificationHistoryInq : PXGraph<ReclassificationHistoryInq>
{
  public PXAction<GLTranReclHist> reclassify;
  public PXAction<GLTranReclHist> reclassifyAll;
  public PXAction<GLTranReclHist> reclassificationHistory;
  public PXAction<GLTranReclHist> viewBatch;
  public PXAction<GLTranReclHist> viewOrigBatch;
  [PXFilterable(new Type[] {})]
  public PXSelectOrderBy<GLTranReclHist, OrderBy<Asc<GLTranReclHist.sortOrder, Asc<GLTranReclHist.lineNbr>>>> TransView;
  public PXSelect<GLTran> CurrentReclassTranView;
  public PXSelect<GLTranKey> SrcOfReclassTranView;

  public ReclassificationHistoryInq()
  {
    ((PXSelectBase) this.TransView).AllowDelete = false;
    ((PXSelectBase) this.TransView).AllowInsert = false;
    ((PXSelectBase) this.TransView).AllowUpdate = true;
    PXUIFieldAttribute.SetVisible<GLTranReclHist.batchNbr>(((PXSelectBase) this.TransView).Cache, (object) null);
    PXDefaultAttribute.SetPersistingCheck<GLTranReclHist.branchID>(((PXSelectBase) this.TransView).Cache, (object) null, (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<GLTranReclHist.accountID>(((PXSelectBase) this.TransView).Cache, (object) null, (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<GLTranReclHist.subID>(((PXSelectBase) this.TransView).Cache, (object) null, (PXPersistingCheck) 2);
    GLTranKey glTranKey = this.SrcTranOfReclassKey();
    if (glTranKey == null)
      return;
    GLTranReclHist glTranReclHist = PXResultset<GLTranReclHist>.op_Implicit(PXSelectBase<GLTranReclHist, PXSelect<GLTranReclHist, Where<GLTranReclHist.module, Equal<Required<GLTran.module>>, And<GLTranReclHist.batchNbr, Equal<Required<GLTran.batchNbr>>, And<GLTranReclHist.lineNbr, Equal<Required<GLTran.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) glTranKey.Module,
      (object) glTranKey.BatchNbr,
      (object) glTranKey.LineNbr
    }));
    ((PXSelectBase) this.TransView).Cache.SetStatus((object) glTranReclHist, (PXEntryStatus) 1);
    bool flag1 = GraphHelper.RowCast<GLTranReclHist>((IEnumerable) PXSelectBase<GLTranReclHist, PXSelectReadonly<GLTranReclHist, Where<GLTranReclHist.reclassSourceTranModule, Equal<Required<GLTranReclHist.module>>, And<GLTranReclHist.reclassSourceTranBatchNbr, Equal<Required<GLTranReclHist.batchNbr>>, And<GLTranReclHist.reclassSourceTranLineNbr, Equal<Required<GLTranReclHist.lineNbr>>>>>, OrderBy<Asc<GLTranReclHist.reclassSeqNbr>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) glTranReclHist.Module,
      (object) glTranReclHist.BatchNbr,
      (object) glTranReclHist.LineNbr
    })).Any<GLTranReclHist>((Func<GLTranReclHist, bool>) (m => m.ReclassType == "S"));
    ((PXAction) this.reclassifyAll).SetEnabled(flag1);
    ((PXAction) this.reclassifyAll).SetVisible(flag1);
    ((PXAction) this.reclassificationHistory).SetEnabled(flag1);
    ((PXAction) this.reclassificationHistory).SetVisible(flag1);
    PXUIFieldAttribute.SetVisible<GLTranReclHist.selected>(((PXSelectBase) this.TransView).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<GLTranReclHist.origBatchNbr>(((PXSelectBase) this.TransView).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<GLTranReclHist.actionDesc>(((PXSelectBase) this.TransView).Cache, (object) null, flag1);
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();
    PXUIFieldAttribute.SetVisibility<GLTran.projectID>(((PXSelectBase) this.TransView).Cache, (object) null, flag2 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<GLTran.projectID>(((PXSelectBase) this.TransView).Cache, (object) null, flag2);
  }

  protected GLTranKey SrcTranOfReclassKey()
  {
    return EnumerableEx.Select<GLTranKey>(((PXSelectBase) this.SrcOfReclassTranView).Cache.Inserted).SingleOrDefault<GLTranKey>();
  }

  protected GLTranKey CurrentNodeOfReclass()
  {
    GLTran tran = EnumerableEx.Select<GLTran>(((PXSelectBase) this.CurrentReclassTranView).Cache.Inserted).SingleOrDefault<GLTran>();
    if (tran.IsReclassReverse.GetValueOrDefault())
    {
      int? nullable1 = tran.LineNbr;
      int? nullable2 = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() % 2) : new int?();
      int num = 0;
      if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
      {
        GLTran glTran = tran;
        int? lineNbr = glTran.LineNbr;
        int? nullable3;
        if (!lineNbr.HasValue)
        {
          nullable1 = new int?();
          nullable3 = nullable1;
        }
        else
          nullable3 = new int?(lineNbr.GetValueOrDefault() + 1);
        glTran.LineNbr = nullable3;
      }
    }
    return new GLTranKey(tran);
  }

  protected bool IsCurrentTran(GLTranReclHist tran)
  {
    GLTran glTran = EnumerableEx.Select<GLTran>(((PXSelectBase) this.CurrentReclassTranView).Cache.Inserted).SingleOrDefault<GLTran>();
    if (!(tran.Module == glTran.Module) || !(tran.BatchNbr == glTran.BatchNbr))
      return false;
    int? lineNbr1 = tran.LineNbr;
    int? lineNbr2 = glTran.LineNbr;
    return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
  }

  protected virtual IEnumerable transView()
  {
    GLTranKey glTranKey = this.SrcTranOfReclassKey();
    if (glTranKey == null)
      return (IEnumerable) new GLTranReclHist[0];
    GLTranReclHist glTranReclHist1 = PXResultset<GLTranReclHist>.op_Implicit(PXSelectBase<GLTranReclHist, PXSelect<GLTranReclHist, Where<GLTranReclHist.module, Equal<Required<GLTran.module>>, And<GLTranReclHist.batchNbr, Equal<Required<GLTran.batchNbr>>, And<GLTranReclHist.lineNbr, Equal<Required<GLTran.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) glTranKey.Module,
      (object) glTranKey.BatchNbr,
      (object) glTranKey.LineNbr
    }));
    ((PXSelectBase) this.TransView).Cache.SetStatus((object) glTranReclHist1, (PXEntryStatus) 1);
    GLTranKey key = this.CurrentNodeOfReclass();
    PXResultset<GLTranReclHist> pxResultset = PXSelectBase<GLTranReclHist, PXSelect<GLTranReclHist, Where<GLTranReclHist.reclassSourceTranModule, Equal<Required<GLTranReclHist.module>>, And<GLTranReclHist.reclassSourceTranBatchNbr, Equal<Required<GLTranReclHist.batchNbr>>, And<GLTranReclHist.reclassSourceTranLineNbr, Equal<Required<GLTranReclHist.lineNbr>>>>>, OrderBy<Asc<GLTranReclHist.reclassSeqNbr>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) glTranReclHist1.Module,
      (object) glTranReclHist1.BatchNbr,
      (object) glTranReclHist1.LineNbr
    });
    bool hasSplitsInChain = false;
    foreach (PXResult<GLTranReclHist> pxResult in pxResultset)
    {
      GLTranReclHist glTranReclHist2 = PXResult<GLTranReclHist>.op_Implicit(pxResult);
      glTranReclHist2.ParentTran = (GLTranReclHist) null;
      glTranReclHist2.ChildTrans = new List<GLTranReclHist>();
      ((PXSelectBase) this.TransView).Cache.SetStatus((object) glTranReclHist2, (PXEntryStatus) 1);
    }
    foreach (PXResult<GLTranReclHist> pxResult in pxResultset)
    {
      GLTranReclHist glTranReclHist3 = PXResult<GLTranReclHist>.op_Implicit(pxResult);
      bool? nullable1 = glTranReclHist3.IsReclassReverse;
      if (!nullable1.GetValueOrDefault())
      {
        GLTranReclHist glTranReclHist4 = (GLTranReclHist) ((PXSelectBase) this.TransView).Cache.Locate((object) new GLTranReclHist(glTranReclHist3.OrigModule, glTranReclHist3.OrigBatchNbr, glTranReclHist3.OrigLineNbr));
        if (glTranReclHist4 != null)
        {
          glTranReclHist3.ParentTran = glTranReclHist4;
          glTranReclHist4.ChildTrans.Add(glTranReclHist3);
          hasSplitsInChain |= glTranReclHist3.ReclassType == "S";
          GLTranReclHist glTranReclHist5 = glTranReclHist3;
          nullable1 = glTranReclHist4.IsSplited;
          bool? nullable2 = new bool?(nullable1.GetValueOrDefault() || glTranReclHist3.ReclassType == "S");
          glTranReclHist5.IsSplited = nullable2;
        }
      }
    }
    GLTranReclHist currentTran = !glTranKey.Equals(key) ? (GLTranReclHist) ((PXSelectBase) this.TransView).Cache.Locate((object) new GLTranReclHist(key.Module, key.BatchNbr, key.LineNbr)) : glTranReclHist1;
    currentTran.SortOrder = new int?(0);
    currentTran.IsCurrent = new bool?(true);
    if (JournalEntry.GetReleasedReversingBatches((PXGraph) this, glTranReclHist1.Module, glTranReclHist1.BatchNbr).Any<Batch>())
      ((PXSelectBase) this.TransView).Cache.RaiseExceptionHandling<GLTranReclHist.batchNbr>((object) glTranReclHist1, (object) null, (Exception) new PXSetPropertyException("Batch of transaction has been reversed.", (PXErrorLevel) 3));
    List<GLTranReclHist> glTranReclHistList = new List<GLTranReclHist>();
    this.BuildParentList(currentTran, glTranReclHistList);
    this.SortChildTrans(currentTran, hasSplitsInChain);
    glTranReclHistList.Add(currentTran);
    if (!glTranKey.Equals((GLTran) currentTran))
    {
      PXCache cache = ((PXSelectBase) this.TransView).Cache;
      string module = key.Module;
      string batchNbr = key.BatchNbr;
      int? lineNbr1 = key.LineNbr;
      int? lineNbr2 = lineNbr1.HasValue ? new int?(lineNbr1.GetValueOrDefault() - 1) : new int?();
      GLTranReclHist glTranReclHist6 = new GLTranReclHist(module, batchNbr, lineNbr2);
      GLTranReclHist glTranReclHist7 = (GLTranReclHist) cache.Locate((object) glTranReclHist6);
      glTranReclHist7.SortOrder = new int?(0);
      glTranReclHistList.Add(glTranReclHist7);
    }
    glTranReclHistList.AddRange(GraphHelper.RowCast<GLTranReclHist>(((PXSelectBase) this.TransView).Cache.Updated).Where<GLTranReclHist>((Func<GLTranReclHist, bool>) (m =>
    {
      int? reclassSeqNbr = m.ReclassSeqNbr;
      int valueOrDefault = currentTran.ReclassSeqNbr.GetValueOrDefault();
      return reclassSeqNbr.GetValueOrDefault() > valueOrDefault & reclassSeqNbr.HasValue && m.SortOrder.HasValue;
    })));
    PXUIFieldAttribute.SetVisible<GLTranReclHist.curyReclassRemainingAmt>(((PXSelectBase) this.TransView).Cache, (object) null, glTranReclHistList.Any<GLTranReclHist>((Func<GLTranReclHist, bool>) (m =>
    {
      Decimal? reclassRemainingAmt = m.CuryReclassRemainingAmt;
      Decimal num = 0M;
      return reclassRemainingAmt.GetValueOrDefault() > num & reclassRemainingAmt.HasValue;
    })));
    foreach (GLTranReclHist tran in glTranReclHistList)
    {
      if (!glTranKey.Equals((GLTran) tran))
      {
        if (tran.IsParent.GetValueOrDefault())
          tran.SplitIcon = "~/Icons/parent_cc.svg";
        if (tran.IsSplited.GetValueOrDefault())
          tran.SplitIcon = "~/Icons/subdirectory_arrow_right_cc.svg";
        tran.ActionDesc = !(tran.ReclassType == "S") ? "C" : "S";
      }
    }
    return (IEnumerable) glTranReclHistList;
  }

  protected virtual int? SortChildTrans(GLTranReclHist tran, bool hasSplitsInChain)
  {
    bool flag1 = tran.ReclassRemainingAmt.GetValueOrDefault() == 0M & hasSplitsInChain && !this.IsCurrentTran(tran);
    PXCache cache = ((PXSelectBase) this.TransView).Cache;
    string module = tran.Module;
    string batchNbr = tran.BatchNbr;
    int? lineNbr1 = tran.LineNbr;
    int? lineNbr2 = lineNbr1.HasValue ? new int?(lineNbr1.GetValueOrDefault() - 1) : new int?();
    GLTranReclHist glTranReclHist1 = new GLTranReclHist(module, batchNbr, lineNbr2);
    GLTranReclHist glTranReclHist2 = (GLTranReclHist) cache.Locate((object) glTranReclHist1);
    if (glTranReclHist2 != null)
    {
      glTranReclHist2.SortOrder = tran.SortOrder;
      glTranReclHist2.ParentTran = tran.ParentTran;
      glTranReclHist2.IsSplited = tran.IsSplited;
    }
    else if (tran.ParentTran != null)
      throw new PXException("The reversing entry has not been found.");
    if (!tran.ChildTrans.Any<GLTranReclHist>())
      return tran.SortOrder;
    int? nullable1 = tran.SortOrder;
    bool flag2 = false;
    foreach (GLTranReclHist tran1 in tran.ChildTrans.Where<GLTranReclHist>((Func<GLTranReclHist, bool>) (m => m.ReclassType == "S")))
    {
      int? nullable2 = nullable1;
      nullable1 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + 1) : new int?();
      tran1.SortOrder = nullable1;
      flag2 |= tran1.ReclassType == "S";
      nullable1 = this.SortChildTrans(tran1, hasSplitsInChain);
    }
    foreach (GLTranReclHist tran2 in tran.ChildTrans.Where<GLTranReclHist>((Func<GLTranReclHist, bool>) (m => m.ReclassType == "C")))
    {
      int? nullable3 = nullable1;
      nullable1 = nullable3.HasValue ? new int?(nullable3.GetValueOrDefault() + 1) : new int?();
      tran2.SortOrder = nullable1;
      flag2 |= tran2.ReclassType == "S";
      nullable1 = this.SortChildTrans(tran2, hasSplitsInChain);
    }
    if (flag1)
    {
      tran.SortOrder = new int?();
      glTranReclHist2.SortOrder = new int?();
    }
    tran.IsParent = new bool?(flag2);
    return nullable1;
  }

  protected virtual void BuildParentList(GLTranReclHist tran, List<GLTranReclHist> result)
  {
    if (tran.ParentTran == null)
      return;
    GLTranReclHist parentTran1 = tran.ParentTran;
    int? nullable1 = tran.SortOrder;
    int? nullable2 = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() - 1) : new int?();
    parentTran1.SortOrder = nullable2;
    tran.ParentTran.IsParent = new bool?(tran.ReclassType == "S");
    this.BuildParentList(tran.ParentTran, result);
    result.Add(tran.ParentTran);
    PXCache cache = ((PXSelectBase) this.TransView).Cache;
    string module = tran.ParentTran.Module;
    string batchNbr = tran.ParentTran.BatchNbr;
    nullable1 = tran.ParentTran.LineNbr;
    int? lineNbr = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() - 1) : new int?();
    GLTranReclHist glTranReclHist1 = new GLTranReclHist(module, batchNbr, lineNbr);
    GLTranReclHist glTranReclHist2 = (GLTranReclHist) cache.Locate((object) glTranReclHist1);
    if (glTranReclHist2 == null)
      return;
    glTranReclHist2.SortOrder = tran.ParentTran.SortOrder;
    GLTranReclHist glTranReclHist3 = glTranReclHist2;
    GLTranReclHist parentTran2 = tran.ParentTran;
    bool? nullable3 = new bool?(parentTran2 != null && parentTran2.IsSplited.GetValueOrDefault());
    glTranReclHist3.IsSplited = nullable3;
    result.Add(glTranReclHist2);
  }

  public virtual bool IsDirty => false;

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Reclassify(PXAdapter adapter)
  {
    IEnumerable<GLTranReclHist> source1 = GraphHelper.RowCast<GLTranReclHist>(((PXSelectBase) this.TransView).Cache.Updated);
    if (source1 == null)
      return (IEnumerable) new GLTran[0];
    if (source1.Any<GLTranReclHist>((Func<GLTranReclHist, bool>) (m => m.ReclassType == "S")))
    {
      IEnumerable<GLTranReclHist> source2 = source1.Where<GLTranReclHist>((Func<GLTranReclHist, bool>) (m => m.Selected.GetValueOrDefault()));
      if (source2.Count<GLTranReclHist>() == 0)
        return adapter.Get();
      ReclassifyTransactionsProcess.OpenForReclassification((IReadOnlyCollection<GLTran>) source2.ToList<GLTranReclHist>().AsReadOnly());
      return adapter.Get();
    }
    GLTranReclHist glTranReclHist = source1.LastOrDefault<GLTranReclHist>();
    bool? released = glTranReclHist.Released;
    bool flag = false;
    if (released.GetValueOrDefault() == flag & released.HasValue)
      throw new PXException("The transaction cannot be reclassified because it is not released.");
    ReclassifyTransactionsProcess.OpenForReclassification((IReadOnlyCollection<GLTran>) new GLTranReclHist[1]
    {
      glTranReclHist
    });
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ReclassifyAll(PXAdapter adapter)
  {
    IEnumerable<GLTranReclHist> trans = GraphHelper.RowCast<GLTranReclHist>(((PXSelectBase) this.TransView).Cache.Updated).Where<GLTranReclHist>((Func<GLTranReclHist, bool>) (m => m.SortOrder.HasValue));
    if (trans == null)
    {
      ((PXSelectBase) this.TransView).Cache.Clear();
      return adapter.Get();
    }
    ReclassifyTransactionsProcess.TryOpenForReclassification<GLTran>((PXGraph) this, (IEnumerable<GLTran>) trans, "H", ((PXSelectBase) this.TransView).View, "Some transactions of the batch cannot be reclassified. These transactions will not be loaded.", "No transactions, for which the reclassification can be performed, have been found in the batch.");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    GLTranReclHist current = ((PXSelectBase<GLTranReclHist>) this.TransView).Current;
    if (current != null)
      JournalEntry.RedirectToBatch((PXGraph) this, current.Module, current.BatchNbr);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewOrigBatch(PXAdapter adapter)
  {
    GLTranReclHist current = ((PXSelectBase<GLTranReclHist>) this.TransView).Current;
    if (current != null)
      JournalEntry.RedirectToBatch((PXGraph) this, current.OrigModule, current.OrigBatchNbr);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ReclassificationHistory(PXAdapter adapter)
  {
    if (((PXSelectBase<GLTranReclHist>) this.TransView).Current != null)
      ReclassificationHistoryInq.OpenForTransaction((GLTran) ((PXSelectBase<GLTranReclHist>) this.TransView).Current);
    return adapter.Get();
  }

  public static void OpenForTransaction(GLTran tran)
  {
    if (tran == null)
      throw new ArgumentNullException(nameof (tran));
    ReclassificationHistoryInq instance = PXGraph.CreateInstance<ReclassificationHistoryInq>();
    ((PXSelectBase<GLTran>) instance.CurrentReclassTranView).Insert(tran);
    ((PXSelectBase<GLTranKey>) instance.SrcOfReclassTranView).Insert(instance.GetSrcTranOfReclassKey(tran));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, string.Empty);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
    throw requiredException;
  }

  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  protected virtual void GLTran_CuryCreditAmt_CacheAttached(PXCache sender)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  protected virtual void GLTran_CuryDebitAmt_CacheAttached(PXCache sender)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  protected virtual void GLTran_CreditAmt_CacheAttached(PXCache sender)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  protected virtual void GLTran_DebitAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (InventoryAttribute))]
  [AnyInventory]
  protected virtual void _(PX.Data.Events.CacheAttached<GLTran.inventoryID> e)
  {
  }

  protected virtual void GLTranReclHist_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    GLTranReclHist row = (GLTranReclHist) e.Row;
    if (row == null)
      return;
    bool flag1 = JournalEntry.IsTransactionReclassifiable((GLTran) row, "H", (string) null, ProjectDefaultAttribute.NonProject());
    PXUIFieldAttribute.SetEnabled<GLTranReclHist.selected>(cache, (object) row, flag1);
    bool? nullable = row.Released;
    bool flag2 = false;
    if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
      return;
    nullable = row.IsReclassReverse;
    bool flag3 = false;
    if (!(nullable.GetValueOrDefault() == flag3 & nullable.HasValue))
      return;
    cache.RaiseExceptionHandling<GLTranReclHist.batchNbr>((object) row, (object) null, (Exception) new PXSetPropertyException("The transaction cannot be reclassified because it is not released.", (PXErrorLevel) 3));
  }

  private GLTranKey GetSrcTranOfReclassKey(GLTran tran)
  {
    if (JournalEntry.IsReclassifacationTran(tran))
      return new GLTranKey()
      {
        Module = tran.ReclassSourceTranModule,
        BatchNbr = tran.ReclassSourceTranBatchNbr,
        LineNbr = tran.ReclassSourceTranLineNbr
      };
    return new GLTranKey()
    {
      Module = tran.Module,
      BatchNbr = tran.BatchNbr,
      LineNbr = tran.LineNbr
    };
  }
}

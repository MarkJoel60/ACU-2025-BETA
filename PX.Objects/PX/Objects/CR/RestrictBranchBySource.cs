// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.RestrictBranchBySource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

public abstract class RestrictBranchBySource : PXRestrictorAttribute
{
  public bool ResetBranch;
  protected System.Type SourceType;
  protected System.Type BranchCD;

  protected RestrictBranchBySource(System.Type restriction, System.Type source, System.Type branchCD, string message)
    : base(restriction, message, Array.Empty<System.Type>())
  {
    this.SourceType = source;
    this.BranchCD = branchCD;
  }

  public virtual object[] GetMessageParameters(PXCache sender, object itemres, object row)
  {
    List<object> objectList = new List<object>();
    PXCache cach = sender.Graph.Caches[row.GetType()];
    objectList.Add(cach.GetStateExt((object) PXResult.Unwrap(row, BqlCommand.GetItemType(this.SourceType)) ?? cach.Current, this.SourceType.Name));
    objectList.Add(sender.Graph.Caches[BqlCommand.GetItemType(this.BranchCD)].GetStateExt((object) PXResult.Unwrap(itemres, BqlCommand.GetItemType(this.BranchCD)), "branchCD"));
    return objectList.ToArray();
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!(this.SourceType != (System.Type) null))
      return;
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    System.Type itemType = BqlCommand.GetItemType(this.SourceType);
    string name = this.SourceType.Name;
    RestrictBranchBySource restrictBranchBySource = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) restrictBranchBySource, __vmethodptr(restrictBranchBySource, SourceChanged));
    fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
  }

  protected virtual void SourceChanged(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    this.SyncBAccountCache(cache, e.Row);
    if (this.ResetBranch)
    {
      if (cache.GetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) != PXCache.NotSetValue)
        return;
      try
      {
        object obj = cache.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
        cache.RaiseFieldVerifying(((PXEventSubscriberAttribute) this)._FieldName, e.Row, ref obj);
      }
      catch (PXSetPropertyException ex)
      {
        cache.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
      }
    }
    else
    {
      object obj = cache.GetValuePending(e.Row, this.SourceType.Name) == PXCache.NotSetValue || cache.GetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) != null ? cache.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) : (object) null;
      try
      {
        cache.RaiseFieldVerifying(((PXEventSubscriberAttribute) this)._FieldName, e.Row, ref obj);
      }
      catch (PXSetPropertyException ex)
      {
        string str = cache.GetValueExt(e.Row, this.SourceType.Name)?.ToString();
        cache.SetValue(e.Row, this.SourceType.Name, e.OldValue);
        cache.RaiseExceptionHandling(this.SourceType.Name, e.Row, (object) str, (Exception) ex);
      }
    }
  }

  protected virtual void OnRowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    this.SyncBAccountCache(sender, e.Row);
  }

  protected void SyncBAccountCache(PXCache cache, object row)
  {
    object objA = cache.GetValue(row, this.SourceType.Name);
    PXCache cach = cache.Graph.Caches[typeof (BAccount)];
    if (cach.Current != null)
    {
      object objB = cach.GetValue(cach.Current, typeof (BAccount.bAccountID).Name);
      if (object.Equals(objA, objB))
        return;
    }
    BAccount baccount = PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>>>.Config>.SelectWindowed(cache.Graph, 0, 1, new object[1]
    {
      objA
    }));
    cach.Current = (object) baccount;
  }
}

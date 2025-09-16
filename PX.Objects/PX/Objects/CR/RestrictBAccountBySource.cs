// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.RestrictBAccountBySource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

public abstract class RestrictBAccountBySource : PXRestrictorAttribute
{
  protected bool ResetBAccount;
  protected System.Type SourceType;
  protected System.Type AcctCD;

  protected RestrictBAccountBySource(System.Type restriction, System.Type source, System.Type acctCD, string message)
    : base(restriction, message, Array.Empty<System.Type>())
  {
    this.SourceType = source;
    this.AcctCD = acctCD;
  }

  public virtual object[] GetMessageParameters(PXCache sender, object itemres, object row)
  {
    List<object> objectList = new List<object>();
    objectList.Add(sender.Graph.Caches[BqlCommand.GetItemType(this.AcctCD)].GetStateExt((object) PXResult.Unwrap(itemres, BqlCommand.GetItemType(this.AcctCD)), "acctCD"));
    System.Type itemType = BqlCommand.GetItemType(this.SourceType);
    PXCache cach = sender.Graph.Caches[itemType];
    IBqlTable ibqlTable = PXResult.Unwrap(row, itemType);
    if (ibqlTable != null && !itemType.IsAssignableFrom(ibqlTable.GetType()))
      ibqlTable = (IBqlTable) null;
    objectList.Add(cach.GetStateExt((object) ibqlTable ?? cach.Current, this.SourceType.Name));
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
    RestrictBAccountBySource baccountBySource = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) baccountBySource, __vmethodptr(baccountBySource, SourceChanged));
    fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
  }

  protected virtual void SourceChanged(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    if (this.ResetBAccount)
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
        this.ClearErrorValueIfTheValueForAnotherField(ex);
        cache.RaiseExceptionHandling(this.SourceType.Name, e.Row, (object) str, (Exception) ex);
      }
    }
  }

  protected virtual void ClearErrorValueIfTheValueForAnotherField(PXSetPropertyException ex)
  {
    if (ex.ErrorValue == null || string.Equals(((PXOverridableException) ex).MapErrorTo, this.SourceType.Name, StringComparison.OrdinalIgnoreCase))
      return;
    ex.ErrorValue = (object) null;
  }
}

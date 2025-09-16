// Decompiled with JetBrains decompiler
// Type: PX.CS.RMDataSourceAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.CS;

[PXDBInt]
[PXDBChildIdentity(typeof (RMDataSource.dataSourceID))]
[PXUIField(DisplayName = "Data Source")]
public class RMDataSourceAttribute : 
  PXAggregateAttribute,
  IPXRowSelectedSubscriber,
  IPXRowDeletedSubscriber,
  IPXRowInsertedSubscriber
{
  protected PXCache _Cache;
  protected PXCache _RowCache;
  protected PXCache _RowParentCache;

  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    object objB = sender.GetValue(e.Row, this._FieldOrdinal);
    if (objB == null)
      return;
    foreach (RMDataSource rmDataSource in sender.Graph.Caches[typeof (RMDataSource)].Inserted)
    {
      if (object.Equals((object) rmDataSource.DataSourceID, objB))
        return;
    }
    RMDataSource rmDataSource1 = (RMDataSource) sender.Graph.Caches[typeof (RMDataSource)].Insert();
    if (rmDataSource1 == null)
      return;
    sender.SetValue(e.Row, this._FieldOrdinal, (object) rmDataSource1.DataSourceID);
  }

  public virtual void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    object obj = sender.GetValue(e.Row, this._FieldOrdinal);
    if (obj == null)
      return;
    RMDataSource rmDataSource = (RMDataSource) PXSelectBase<RMDataSource, PXSelect<RMDataSource, Where<RMDataSource.dataSourceID, Equal<Required<RMDataSource.dataSourceID>>>>.Config>.Select(sender.Graph, obj);
    if (rmDataSource == null)
      return;
    sender.Graph.Caches[typeof (RMDataSource)].Delete((object) rmDataSource);
  }

  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    PXCache cach = sender.Graph.Caches[typeof (RMDataSource)];
    object obj = sender.GetValue(e.Row, this._FieldOrdinal);
    if (obj != null)
    {
      RMDataSource rmDataSource1 = new RMDataSource()
      {
        DataSourceID = (int?) obj
      };
      if (cach.Locate((object) rmDataSource1) is RMDataSource rmDataSource2 && EnumerableExtensions.IsNotIn<PXEntryStatus>(cach.GetStatus((object) rmDataSource2), PXEntryStatus.Deleted, PXEntryStatus.InsertedDeleted))
        return;
      if ((RMDataSource) PXSelectBase<RMDataSource, PXSelect<RMDataSource, Where<RMDataSource.dataSourceID, Equal<Required<RMDataSource.dataSourceID>>>>.Config>.Select(sender.Graph, obj) == null)
        obj = (object) null;
    }
    if (obj != null)
      return;
    RMDataSource rmDataSource;
    using (new ReadOnlyScope(new PXCache[1]{ cach }))
      rmDataSource = (RMDataSource) cach.Insert();
    if (rmDataSource == null)
      return;
    if (!EnumerableExtensions.IsIn<PXEntryStatus>(sender.GetStatus(e.Row), PXEntryStatus.Inserted, PXEntryStatus.InsertedDeleted))
      sender.Remove(e.Row);
    sender.SetValue(e.Row, this._FieldOrdinal, (object) rmDataSource.DataSourceID);
    sender.MarkUpdated(e.Row);
  }

  public virtual void SuppressFieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  public virtual void TextFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
  }

  public virtual void DataSourceSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
  }

  protected virtual void DataSourceExpandSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
  }

  protected virtual void DataSourceRowDescriptionSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
  }

  protected virtual void DataSourceAmountTypeSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
  }

  public override void CacheAttached(PXCache sender)
  {
    if (!sender.Graph.Views.Caches.Contains(sender.GetItemType()))
      sender.Graph.Views.Caches.Add(sender.GetItemType());
    base.CacheAttached(sender);
    string field = this._FieldName + "Text";
    if (!sender.Fields.Contains(field))
    {
      sender.Fields.Add(field);
      sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), field, new PXFieldSelecting(this.TextFieldSelecting));
      sender.Graph.Views["DataSource"] = new PXView(sender.Graph, false, (BqlCommand) new Select<RMDataSource, Where<RMDataSource.dataSourceID, Equal<Argument<int?>>>>(), (Delegate) new PXPrepareDelegate<int?>(this.GetDataSource));
      if (!sender.Graph.Views.Caches.Contains(typeof (RMDataSource)))
        sender.Graph.Views.Caches.Add(typeof (RMDataSource));
    }
    this._RowCache = sender;
    System.Type parentType = PXParentAttribute.GetParentType(this._RowCache);
    if (parentType != (System.Type) null)
      this._RowParentCache = sender.Graph.Caches[parentType];
    this._Cache = sender.Graph.Caches[typeof (RMDataSource)];
    if (!this._Cache.Fields.Contains(field))
    {
      this._Cache.Fields.Add(field);
      this._Cache.Graph.FieldSelecting.AddHandler(typeof (RMDataSource), field, new PXFieldSelecting(this.TextFieldSelecting));
    }
    this._Cache.Graph.RowSelected.AddHandler<RMDataSource>(new PXRowSelected(this.DataSourceSelected));
    this._Cache.Graph.FieldSelecting.AddHandler<RMDataSource.amountType>(new PXFieldSelecting(this.DataSourceAmountTypeSelecting));
    this._Cache.Graph.FieldSelecting.AddHandler<RMDataSource.expand>(new PXFieldSelecting(this.DataSourceExpandSelecting));
    this._Cache.Graph.FieldSelecting.AddHandler<RMDataSource.rowDescription>(new PXFieldSelecting(this.DataSourceRowDescriptionSelecting));
  }

  protected virtual void GetDataSource([PXDBInt] ref int? DataSourceID)
  {
    if (DataSourceID.HasValue)
    {
      int? nullable1 = DataSourceID;
      int num = 0;
      if (!(nullable1.GetValueOrDefault() < num & nullable1.HasValue))
        return;
      bool flag = false;
      foreach (RMDataSource rmDataSource in this._Cache.Inserted)
      {
        nullable1 = rmDataSource.DataSourceID;
        int? nullable2 = DataSourceID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      this._Cache.Insert((object) new RMDataSource()
      {
        DataSourceID = DataSourceID
      });
    }
    else
    {
      if (this._RowCache.Current == null)
        return;
      DataSourceID = (int?) this._RowCache.GetValue(this._RowCache.Current, this._FieldOrdinal);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDynamicAggregateAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public sealed class PXDynamicAggregateAttribute : 
  PXEventSubscriberAttribute,
  IPXRowSelectingSubscriber,
  IPXRowSelectedSubscriber,
  IPXRowUpdatingSubscriber,
  IPXRowUpdatedSubscriber,
  IPXRowInsertingSubscriber,
  IPXRowInsertedSubscriber,
  IPXRowDeletingSubscriber,
  IPXRowDeletedSubscriber,
  IPXRowPersistingSubscriber,
  IPXRowPersistedSubscriber,
  IPXFieldDefaultingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXFieldUpdatingSubscriber,
  IPXFieldUpdatedSubscriber,
  IPXFieldVerifyingSubscriber
{
  private readonly PXDynamicAggregateAttribute.GetAttributes _getAttributesHandler;

  public PXDynamicAggregateAttribute(PXDynamicAggregateAttribute.GetAttributes handler)
  {
    this._getAttributesHandler = handler != null ? handler : throw new ArgumentNullException(nameof (handler));
  }

  public void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    foreach (IPXRowSelectingSubscriber attribute in this.Attributes<IPXRowSelectingSubscriber>())
      attribute.RowSelecting(sender, e);
  }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    foreach (IPXRowSelectedSubscriber attribute in this.Attributes<IPXRowSelectedSubscriber>())
      attribute.RowSelected(sender, e);
  }

  public void RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    foreach (IPXRowUpdatingSubscriber attribute in this.Attributes<IPXRowUpdatingSubscriber>())
      attribute.RowUpdating(sender, e);
  }

  public void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    foreach (IPXRowUpdatedSubscriber attribute in this.Attributes<IPXRowUpdatedSubscriber>())
      attribute.RowUpdated(sender, e);
  }

  public void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    foreach (IPXRowInsertingSubscriber attribute in this.Attributes<IPXRowInsertingSubscriber>())
      attribute.RowInserting(sender, e);
  }

  public void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    foreach (IPXRowInsertedSubscriber attribute in this.Attributes<IPXRowInsertedSubscriber>())
      attribute.RowInserted(sender, e);
  }

  public void RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    foreach (IPXRowDeletingSubscriber attribute in this.Attributes<IPXRowDeletingSubscriber>())
      attribute.RowDeleting(sender, e);
  }

  public void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    foreach (IPXRowDeletedSubscriber attribute in this.Attributes<IPXRowDeletedSubscriber>())
      attribute.RowDeleted(sender, e);
  }

  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    foreach (IPXRowPersistingSubscriber attribute in this.Attributes<IPXRowPersistingSubscriber>())
      attribute.RowPersisting(sender, e);
  }

  public void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    foreach (IPXRowPersistedSubscriber attribute in this.Attributes<IPXRowPersistedSubscriber>())
      attribute.RowPersisted(sender, e);
  }

  public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    foreach (IPXFieldDefaultingSubscriber attribute in this.Attributes<IPXFieldDefaultingSubscriber>())
      attribute.FieldDefaulting(sender, e);
  }

  public void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    foreach (IPXFieldSelectingSubscriber attribute in this.Attributes<IPXFieldSelectingSubscriber>())
      attribute.FieldSelecting(sender, e);
  }

  public void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    foreach (IPXFieldUpdatingSubscriber attribute in this.Attributes<IPXFieldUpdatingSubscriber>())
      attribute.FieldUpdating(sender, e);
  }

  public void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    foreach (IPXFieldUpdatedSubscriber attribute in this.Attributes<IPXFieldUpdatedSubscriber>())
      attribute.FieldUpdated(sender, e);
  }

  public void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    foreach (IPXFieldVerifyingSubscriber attribute in this.Attributes<IPXFieldVerifyingSubscriber>())
      attribute.FieldVerifying(sender, e);
  }

  private IEnumerable<T> Attributes<T>() where T : class
  {
    PXDynamicAggregateAttribute aggregateAttribute = this;
    foreach (PXEventSubscriberAttribute subscriberAttribute in aggregateAttribute._getAttributesHandler(aggregateAttribute._FieldName))
    {
      if (subscriberAttribute is T obj)
        yield return obj;
    }
  }

  public delegate IEnumerable<PXEventSubscriberAttribute> GetAttributes(string fieldName);
}

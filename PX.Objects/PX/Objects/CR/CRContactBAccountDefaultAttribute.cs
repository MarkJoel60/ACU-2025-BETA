// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRContactBAccountDefaultAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

internal sealed class CRContactBAccountDefaultAttribute : 
  PXEventSubscriberAttribute,
  IPXRowInsertingSubscriber,
  IPXRowUpdatingSubscriber,
  IPXRowPersistingSubscriber,
  IPXRowPersistedSubscriber
{
  private Dictionary<object, object> _persistedItems;

  public virtual void CacheAttached(PXCache sender)
  {
    this._persistedItems = new Dictionary<object, object>();
    // ISSUE: method pointer
    sender.Graph.RowPersisting.AddHandler(typeof (BAccount), new PXRowPersisting((object) this, __methodptr(SourceRowPersisting)));
  }

  public void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    this.SetDefaultValue(sender, e.Row);
  }

  public void RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    this.SetDefaultValue(sender, e.Row);
  }

  private void SetDefaultValue(PXCache sender, object row)
  {
    if (this.IsLeadOrPerson(sender, row) || sender.GetValue(row, this._FieldOrdinal) != null)
      return;
    PXCache cach = sender.Graph.Caches[typeof (BAccount)];
    if (cach.Current == null)
      return;
    object obj = cach.GetValue(cach.Current, typeof (BAccount.bAccountID).Name);
    sender.SetValue(row, this._FieldOrdinal, obj);
  }

  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (this.IsLeadOrPerson(sender, e.Row))
      return;
    if ((e.Operation & 3) == 2 || (e.Operation & 3) == 1)
    {
      object key1 = sender.GetValue(e.Row, this._FieldOrdinal);
      object obj;
      if (key1 != null && this._persistedItems.TryGetValue(key1, out obj))
      {
        object key2 = sender.Graph.Caches[typeof (BAccount)].GetValue(obj, typeof (BAccount.bAccountID).Name);
        sender.SetValue(e.Row, this._FieldOrdinal, key2);
        if (key2 != null)
          this._persistedItems[key2] = obj;
      }
    }
    if (((e.Operation & 3) == 2 || (e.Operation & 3) == 1) && sender.GetValue(e.Row, this._FieldOrdinal) == null)
      throw new PXRowPersistingException(this._FieldName, (object) null, PXMessages.LocalizeFormatNoPrefixNLA("'{0}' may not be empty.", new object[1]
      {
        (object) this._FieldName
      }));
  }

  public void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (this.IsLeadOrPerson(sender, e.Row) || (e.Operation & 3) != 2 || e.TranStatus != 2)
      return;
    object key = sender.GetValue(e.Row, this._FieldOrdinal);
    object obj;
    if (key == null || !this._persistedItems.TryGetValue(key, out obj))
      return;
    string name = typeof (BAccount.bAccountID).Name;
    sender.SetValue(e.Row, this._FieldOrdinal, sender.Graph.Caches[typeof (BAccount)].GetValue(obj, name));
  }

  private void SourceRowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (this.IsLeadOrPerson(sender, e.Row))
      return;
    string name = typeof (BAccount.bAccountID).Name;
    object key = sender.GetValue(e.Row, name);
    if (key == null)
      return;
    this._persistedItems[key] = e.Row;
  }

  private bool IsLeadOrPerson(PXCache sender, object row)
  {
    object obj = sender.GetValue(row, typeof (Contact.contactType).Name);
    if (obj == null)
      return false;
    return obj.Equals((object) "LD") || obj.Equals((object) "PN");
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.SharedRecordAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CS;

public abstract class SharedRecordAttribute : 
  PXEventSubscriberAttribute,
  IPXRowInsertedSubscriber,
  IPXRowPersistingSubscriber,
  IPXRowPersistedSubscriber,
  IPXRowSelectedSubscriber,
  IPXRowDeletedSubscriber
{
  protected BqlCommand _Select;
  protected Type _ItemType;
  protected Type _RecordType;
  protected object _KeyToAbort;
  protected object _SelfKeyToAbort;
  protected string _RecordID;
  protected string _IsDefault;
  protected PXView _ClearView;
  protected bool _Required;
  protected Dictionary<object, object> _persistedItems;

  public bool Required
  {
    get => this._Required;
    set => this._Required = value;
  }

  public SharedRecordAttribute(Type RecordIDType, Type IsDefaultType, Type SelectType)
  {
    this._RecordType = BqlCommand.GetItemType(RecordIDType);
    this._RecordID = RecordIDType.Name;
    this._IsDefault = IsDefaultType.Name;
    this._Required = true;
    this._Select = typeof (IBqlSelect).IsAssignableFrom(SelectType) ? BqlCommand.CreateInstance(new Type[1]
    {
      SelectType
    }) : throw new PXArgumentException();
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.RowSelectedEvents rowSelected = sender.Graph.RowSelected;
    Type recordType1 = this._RecordType;
    SharedRecordAttribute sharedRecordAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) sharedRecordAttribute1, __vmethodptr(sharedRecordAttribute1, Record_RowSelected));
    rowSelected.AddHandler(recordType1, pxRowSelected);
    PXGraph.RowUpdatedEvents rowUpdated = sender.Graph.RowUpdated;
    Type recordType2 = this._RecordType;
    SharedRecordAttribute sharedRecordAttribute2 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) sharedRecordAttribute2, __vmethodptr(sharedRecordAttribute2, Record_RowUpdated));
    rowUpdated.AddHandler(recordType2, pxRowUpdated);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    Type recordType3 = this._RecordType;
    string isDefault = this._IsDefault;
    SharedRecordAttribute sharedRecordAttribute3 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) sharedRecordAttribute3, __vmethodptr(sharedRecordAttribute3, Record_IsDefault_FieldVerifying));
    fieldVerifying.AddHandler(recordType3, isDefault, pxFieldVerifying);
    PXGraph.RowPersistingEvents rowPersisting = sender.Graph.RowPersisting;
    Type recordType4 = this._RecordType;
    SharedRecordAttribute sharedRecordAttribute4 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) sharedRecordAttribute4, __vmethodptr(sharedRecordAttribute4, Record_RowPersisting));
    rowPersisting.AddHandler(recordType4, pxRowPersisting);
    PXGraph.RowPersistedEvents rowPersisted = sender.Graph.RowPersisted;
    Type recordType5 = this._RecordType;
    SharedRecordAttribute sharedRecordAttribute5 = this;
    // ISSUE: virtual method pointer
    PXRowPersisted pxRowPersisted = new PXRowPersisted((object) sharedRecordAttribute5, __vmethodptr(sharedRecordAttribute5, Record_RowPersisted));
    rowPersisted.AddHandler(recordType5, pxRowPersisted);
    this._ItemType = sender.GetItemType();
    this._persistedItems = new Dictionary<object, object>();
  }

  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!this._Required || sender.GetValue(e.Row, this._FieldOrdinal) != null)
      return;
    using (new ReadOnlyScope(new PXCache[1]
    {
      sender.Graph.Caches[this._RecordType]
    }))
    {
      object obj1 = sender.Graph.Caches[this._RecordType].Insert();
      object obj2 = sender.Graph.Caches[this._RecordType].GetValue(obj1, this._RecordID);
      sender.SetValue(e.Row, this._FieldOrdinal, obj2);
    }
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    object objA = sender.GetValue(e.Row, this._FieldOrdinal);
    if (objA == null)
      return;
    PXCache cach = sender.Graph.Caches[this._RecordType];
    if (Convert.ToInt32(objA) < 0)
    {
      foreach (object obj in cach.Inserted)
      {
        object objB = cach.GetValue(obj, this._RecordID);
        if (object.Equals(objA, objB))
        {
          if (!cach.PersistInserted(obj))
            throw new PXException("Another process has added the '{0}' record. {1}", new object[2]
            {
              (object) cach.DisplayName,
              (object) "Your changes will be lost."
            });
          this._KeyToAbort = sender.GetValue(e.Row, this._FieldOrdinal);
          int int32 = Convert.ToInt32((object) PXDatabase.SelectIdentity());
          sender.SetValue(e.Row, this._FieldOrdinal, (object) int32);
          cach.SetValue(obj, this._RecordID, (object) int32);
          break;
        }
      }
    }
    else
    {
      foreach (object obj in cach.Updated)
      {
        object objB = cach.GetValue(obj, this._RecordID);
        if (object.Equals(objA, objB))
        {
          cach.PersistUpdated(obj);
          break;
        }
      }
    }
  }

  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus == null)
      return;
    PXCache cach = sender.Graph.Caches[this._RecordType];
    if (e.TranStatus == 2)
    {
      if (this._KeyToAbort != null)
      {
        object objA = sender.GetValue(e.Row, this._FieldOrdinal);
        sender.SetValue(e.Row, this._FieldOrdinal, this._KeyToAbort);
        foreach (object obj in cach.Inserted)
        {
          object objB = cach.GetValue(obj, this._RecordID);
          if (object.Equals(objA, objB))
          {
            cach.SetValue(obj, this._RecordID, (object) Convert.ToInt32(this._KeyToAbort));
            cach.ResetPersisted(obj);
          }
        }
        this._KeyToAbort = (object) null;
      }
      else
      {
        object objA = sender.GetValue(e.Row, this._FieldOrdinal);
        foreach (object obj in cach.Updated)
        {
          object objB = cach.GetValue(obj, this._RecordID);
          if (object.Equals(objA, objB))
            cach.ResetPersisted(obj);
        }
      }
    }
    else
    {
      object objA = sender.GetValue(e.Row, this._FieldOrdinal);
      foreach (object obj in cach.Inserted)
      {
        object objB = cach.GetValue(obj, this._RecordID);
        if (object.Equals(objA, objB))
        {
          cach.RaiseRowPersisted(obj, (PXDBOperation) 2, e.TranStatus, e.Exception);
          cach.SetStatus(obj, (PXEntryStatus) 0);
          PXTimeStampScope.PutPersisted(cach, obj, new object[1]
          {
            (object) sender.Graph.TimeStamp
          });
          cach.ResetPersisted(obj);
        }
      }
      foreach (object obj in cach.Updated)
      {
        object objB = cach.GetValue(obj, this._RecordID);
        if (object.Equals(objA, objB))
        {
          cach.RaiseRowPersisted(obj, (PXDBOperation) 1, e.TranStatus, e.Exception);
          cach.SetStatus(obj, (PXEntryStatus) 0);
          PXTimeStampScope.PutPersisted(cach, obj, new object[1]
          {
            (object) sender.Graph.TimeStamp
          });
          cach.ResetPersisted(obj);
        }
      }
      cach.IsDirty = false;
    }
    cach.Normalize();
  }

  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!((Dictionary<Type, PXCache>) sender.Graph.Caches).ContainsKey(this._RecordType))
      return;
    PXCache cach = sender.Graph.Caches[this._RecordType];
    if (sender.GetValue(e.Row, this._FieldOrdinal) != null)
      return;
    PXUIFieldAttribute.SetEnabled(cach, (string) null, false);
    PXUIFieldAttribute.SetEnabled(cach, (string) null, false);
  }

  public virtual void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    this.MarkDeletedIfRelatedRecordDeleted(sender, e.Row);
  }

  protected virtual void MarkDeletedIfRelatedRecordDeleted(
    PXCache relatedRowCache,
    object relatedRow)
  {
    if (relatedRow == null || !((Dictionary<Type, PXCache>) relatedRowCache.Graph.Caches).ContainsKey(this._RecordType) || EnumerableExtensions.IsNotIn<PXEntryStatus>(relatedRowCache.GetStatus(relatedRow), (PXEntryStatus) 3, (PXEntryStatus) 4))
      return;
    object objB = relatedRowCache.GetValue(relatedRow, this.FieldOrdinal);
    PXCache cach = relatedRowCache.Graph.Caches[this._RecordType];
    foreach (object obj in cach.Inserted)
    {
      if (object.Equals(cach.GetValue(obj, this._RecordID), objB))
      {
        cach.SetStatus(obj, (PXEntryStatus) 4);
        break;
      }
    }
  }

  protected virtual bool IsDefault(PXCache cache, object row)
  {
    return ((bool?) cache.GetValue(row, this._IsDefault)).GetValueOrDefault();
  }

  protected virtual void Record_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    bool? nullable1 = (bool?) sender.GetValue(e.Row, this._IsDefault);
    PXUIFieldAttribute.SetVisible(sender, (object) null, this._RecordID, false);
    PXCache pxCache = sender;
    object row = e.Row;
    bool? nullable2 = nullable1;
    bool flag = false;
    int num = !(nullable2.GetValueOrDefault() == flag & nullable2.HasValue) ? 0 : (sender.AllowUpdate ? 1 : 0);
    PXUIFieldAttribute.SetEnabled(pxCache, row, num != 0);
    PXUIFieldAttribute.SetEnabled(sender, e.Row, this._IsDefault, sender.AllowUpdate);
  }

  protected virtual void Record_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    PXCache cach = sender.Graph.Caches[this._ItemType];
    GraphHelper.MarkUpdated(cach, cach.Current);
  }

  public virtual void Record_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Operation != 2)
      return;
    this._SelfKeyToAbort = sender.GetValue(e.Row, this._RecordID);
  }

  protected virtual void Record_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.Operation == 2 && e.TranStatus == null)
    {
      PXCache cach = sender.Graph.Caches[this._ItemType];
      object key = sender.GetValue(e.Row, this._RecordID);
      foreach (object obj in cach.Updated)
      {
        if (object.Equals(this._SelfKeyToAbort, cach.GetValue(obj, this._FieldOrdinal)))
          cach.SetValue(obj, this._FieldOrdinal, key);
      }
      this._persistedItems.Add(key, this._SelfKeyToAbort);
    }
    if (e.Operation == 2 && e.TranStatus == 2)
    {
      PXCache cach = sender.Graph.Caches[this._ItemType];
      foreach (object obj in cach.Updated)
      {
        if (this._persistedItems.TryGetValue(cach.GetValue(obj, this._FieldOrdinal), out this._SelfKeyToAbort))
          cach.SetValue(obj, this._FieldOrdinal, this._SelfKeyToAbort);
      }
    }
    if (e.TranStatus != 1 && e.TranStatus != 2)
      return;
    this._SelfKeyToAbort = (object) null;
  }

  public abstract void Record_IsDefault_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e);

  public abstract void DefaultRecord(PXCache cache, object DocumentRow, object Row);

  public abstract void CopyRecord(PXCache cache, object DocumentRow, object SourceRow, bool clone);

  protected virtual void ClearRecord(PXCache cache, object DocumentRow)
  {
    bool isDirty = cache.Graph.Caches[this._RecordType].IsDirty;
    object objA = cache.GetValue(DocumentRow, this._FieldOrdinal);
    if (objA != null)
    {
      string key = $"_{this._RecordType.Name}:Clear";
      if (this._ClearView == null)
      {
        lock (cache.Graph.Views)
        {
          if (!((Dictionary<string, PXView>) cache.Graph.Views).TryGetValue(key, out this._ClearView))
          {
            BqlCommand instance = BqlCommand.CreateInstance(new Type[1]
            {
              BqlCommand.Compose(new Type[7]
              {
                typeof (Select<,>),
                this._RecordType,
                typeof (Where<,>),
                this._RecordType.GetNestedType(this._RecordID),
                typeof (Equal<>),
                typeof (PX.Data.Required<>),
                this._RecordType.GetNestedType(this._RecordID)
              })
            });
            this._ClearView = new PXView(cache.Graph, false, instance);
            cache.Graph.Views.Add(key, this._ClearView);
          }
        }
      }
      PXView clearView = this._ClearView;
      object[] objArray = new object[1]{ objA };
      foreach (object obj in clearView.SelectMulti(objArray))
      {
        object objB = cache.Graph.Caches[this._RecordType].GetValue(obj, this._RecordID);
        if (object.Equals(objA, objB))
        {
          cache.Graph.Caches[this._RecordType].Delete(obj);
          cache.SetValue(DocumentRow, this._FieldOrdinal, (object) null);
          break;
        }
      }
    }
    cache.Graph.Caches[this._RecordType].IsDirty = isDirty;
  }

  public static void DefaultRecord<Field>(PXCache cache, object data) where Field : IBqlField
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SharedRecordAttribute.\u003C\u003Ec__DisplayClass30_0<Field> cDisplayClass300 = new SharedRecordAttribute.\u003C\u003Ec__DisplayClass30_0<Field>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass300.data = data;
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(typeof (Field).Name))
    {
      if (subscriberAttribute is SharedRecordAttribute)
      {
        try
        {
          // ISSUE: reference to a compiler-generated field
          ((SharedRecordAttribute) subscriberAttribute).DefaultRecord(cache, cDisplayClass300.data, (object) null);
        }
        catch (PXSetPropertyException ex)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          cache.Graph.RowUpdating.AddHandler(cache.GetItemType(), cDisplayClass300.\u003C\u003E9__0 ?? (cDisplayClass300.\u003C\u003E9__0 = new PXRowUpdating((object) cDisplayClass300, __methodptr(\u003CDefaultRecord\u003Eb__0))));
          throw;
        }
      }
    }
  }

  public static void DefaultRecord(PXCache cache, object data, string field)
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(field))
    {
      if (subscriberAttribute is SharedRecordAttribute)
        ((SharedRecordAttribute) subscriberAttribute).DefaultRecord(cache, data, (object) null);
    }
  }

  public static void CopyRecord<Field>(PXCache cache, object data, object source, bool clone) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(typeof (Field).Name))
    {
      if (subscriberAttribute is SharedRecordAttribute)
        ((SharedRecordAttribute) subscriberAttribute).CopyRecord(cache, data, source, clone);
    }
  }

  public static void CopyRecord(
    PXCache cache,
    object data,
    string field,
    object source,
    bool clone)
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(field))
    {
      if (subscriberAttribute is SharedRecordAttribute)
        ((SharedRecordAttribute) subscriberAttribute).CopyRecord(cache, data, source, clone);
    }
  }

  public static void ClearRecord<Field>(PXCache cache, object data) where Field : IBqlField
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SharedRecordAttribute.\u003C\u003Ec__DisplayClass34_0<Field> cDisplayClass340 = new SharedRecordAttribute.\u003C\u003Ec__DisplayClass34_0<Field>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass340.data = data;
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(typeof (Field).Name))
    {
      if (subscriberAttribute is SharedRecordAttribute)
      {
        try
        {
          // ISSUE: reference to a compiler-generated field
          ((SharedRecordAttribute) subscriberAttribute).ClearRecord(cache, cDisplayClass340.data);
        }
        catch (PXSetPropertyException ex)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          cache.Graph.RowUpdating.AddHandler(cache.GetItemType(), cDisplayClass340.\u003C\u003E9__0 ?? (cDisplayClass340.\u003C\u003E9__0 = new PXRowUpdating((object) cDisplayClass340, __methodptr(\u003CClearRecord\u003Eb__0))));
          throw;
        }
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBDefaultAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Sets the default value for a DAC field. Use to assign a value
/// from the auto-generated key field.</summary>
/// <example><para>Setting the default value that will be taken from the current POReceipt cache object and reassigned only on insertion of the data record to the database.</para>
/// <code title="Eaxmple" lang="CS">
/// public partial class LandedCostTran : PXBqlTable, PX.Data.IBqlTable
/// {
/// ...
/// [PXDBString(3, IsFixed = true)]
/// [PXDBDefault(typeof(POReceipt.receiptType),
/// DefaultForUpdate = false)]
/// public virtual string POReceiptType { get; set; }
/// ...
/// }</code>
/// <code title="Example1" description="Changing the SetDefaultForUpdate property. The method sets the property for the ShipAddressID field in all data records in the cache object associated with the OrderList view." groupname="Example" lang="CS">
/// PXDBDefaultAttribute.SetDefaultForUpdate&lt;SOOrderShipment.shipAddressID&gt;(
///     OrderList.Cache, null, false);</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
public class PXDBDefaultAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldDefaultingSubscriber,
  IPXRowPersistingSubscriber,
  IPXRowPersistedSubscriber
{
  protected System.Type _SourceType;
  protected System.Type _OriginSourceType;
  protected string _SourceField;
  protected BqlCommand _Select;
  protected bool _DefaultForInsert = true;
  protected bool _DefaultForUpdate = true;
  protected bool _DoubleDefaultAttribute;
  protected PXPersistingCheck _PersistingCheck;
  protected PXDBDefaultAttribute.FlagHandler _IsRestriction;

  internal System.Type OriginSourceType => this._OriginSourceType;

  /// <summary>Gets or sets the <see cref="T:PX.Data.PXPersistingCheck">PXPersistingCheck</see>
  /// value that defines how to check the field value before saving a data record
  /// to the database. The attribute either checks that the value is not
  /// <tt>null</tt>, checks that the value is <tt>null</tt> or a blank
  /// string (contains only whitespace characters), or doesn't check the
  /// value. If the attribute discovers that the value is in fact
  /// <tt>null</tt> or blank, it will throw the
  /// <tt>PXRowPersistingException</tt> exception. As a result, the save
  /// action will fail and the user will get an error message.</summary>
  public virtual PXPersistingCheck PersistingCheck
  {
    get => this._PersistingCheck;
    set => this._PersistingCheck = value;
  }

  public static void SetPersistingCheck<Field>(PXCache cache, object data, PXPersistingCheck check) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXDBDefaultAttribute)
        ((PXDBDefaultAttribute) attribute)._PersistingCheck = check;
    }
  }

  /// <summary>
  /// Gets or sets the <see cref="T:PX.Data.PXFieldUpdateMode" /> value that determines when the attribute should update the dependent field value.
  /// </summary>
  public virtual PXFieldUpdateMode FieldUpdateMode { get; set; }

  /// <summary>Gets or sets the value that indicates whether the default
  /// value is reassigned on a database update operation.</summary>
  public bool DefaultForUpdate
  {
    get => this._DefaultForUpdate;
    set => this._DefaultForUpdate = value;
  }

  /// <summary>Gets or sets the value that indicates whether the default
  /// value is reassigned on a database insert operation.</summary>
  public bool DefaultForInsert
  {
    get => this._DefaultForInsert;
    set => this._DefaultForInsert = value;
  }

  public bool CanDefault => this._SourceType != (System.Type) null || this._Select != null;

  protected virtual void EnsureIsRestriction(PXCache sender)
  {
    if (this._IsRestriction.Value.HasValue || this._SourceType == (System.Type) null)
      return;
    string str = this._SourceField ?? this._FieldName;
    PXCache cach = sender.Graph.Caches[this._SourceType];
    if (string.Equals(cach.Identity, str, StringComparison.OrdinalIgnoreCase))
    {
      this._IsRestriction.Value = new bool?(true);
    }
    else
    {
      PXCommandPreparingEventArgs.FieldDescription description;
      cach.RaiseCommandPreparing(str, (object) null, (object) null, PXDBOperation.Update, (System.Type) null, out description);
      this._IsRestriction.Value = new bool?(description != null && description.IsRestriction);
    }
  }

  /// <summary>Initializes a new instance with default parameters.</summary>
  public PXDBDefaultAttribute()
    : this((System.Type) null)
  {
  }

  /// <summary>
  /// Initializes a new instance of the attribute. Obtains the default value using the provided BQL query.
  /// </summary>
  /// <param name="sourceType">The BQL query that is used to calculate the default value. Accepts the types derived from: <tt>IBqlSearch</tt>IBqlSearch, <tt>IBqlSelect</tt>, <tt>IBqlField</tt>, <tt>IBqlTable</tt>.</param>
  public PXDBDefaultAttribute(System.Type sourceType)
  {
    this.FieldUpdateMode = PXFieldUpdateMode.Default;
    this._OriginSourceType = sourceType;
    this.SetSourceType((PXCache) null, (System.Type) null);
  }

  /// <summary>Provides the default value</summary>
  /// <param name="sender">Cache</param>
  /// <param name="e">Event arguments to set the NewValue</param>
  /// <exclude />
  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (this._Select != null)
    {
      List<object> objectList = sender.Graph.TypedViews.GetView(this._Select, false).SelectMultiBound(new object[1]
      {
        e.Row
      });
      if (objectList == null || objectList.Count <= 0)
        return;
      object data = objectList[objectList.Count - 1];
      if (data != null && data is PXResult)
        data = ((PXResult) data)[this._SourceType];
      e.NewValue = sender.Graph.Caches[this._SourceType].GetValue(data, this._SourceField ?? this._FieldName);
      e.Cancel = true;
    }
    else
    {
      if (!(this._SourceType != (System.Type) null))
        return;
      PXCache cach = sender.Graph.Caches[this._SourceType];
      if (cach.Current == null)
        return;
      e.NewValue = cach.GetValue(cach.Current, this._SourceField ?? this._FieldName);
      e.Cancel = true;
    }
  }

  /// <summary>
  /// Re-default the value. Check if the value was set before saving the record to the database
  /// </summary>
  /// <param name="sender">Cache</param>
  /// <param name="e">Event arguments to retrive the value from the Row</param>
  /// <exclude />
  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (this.FieldUpdateMode == PXFieldUpdateMode.Default)
      this.SetFieldValue(sender, e.Row, e.Operation);
    if (this.PersistingCheck == PXPersistingCheck.Nothing || ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert || !this._DefaultForInsert) && ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Update || !this._DefaultForUpdate) || sender.GetValue(e.Row, this._FieldOrdinal) != null)
      return;
    if (sender.RaiseExceptionHandling(this._FieldName, e.Row, (object) null, (Exception) new PXSetPropertyKeepPreviousException(PXMessages.LocalizeFormat("'{0}' cannot be empty.", (object) $"[{this._FieldName}]"))))
      throw new PXRowPersistingException(this._FieldName, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) this._FieldName
      });
  }

  protected void SetFieldValue(PXCache sender, object row, PXDBOperation operation)
  {
    operation &= PXDBOperation.Delete;
    if ((operation != PXDBOperation.Insert || !this._DefaultForInsert) && (operation != PXDBOperation.Update || !this._DefaultForUpdate) || !(this._SourceType != (System.Type) null))
      return;
    this.EnsureIsRestriction(sender);
    bool? nullable = this._IsRestriction.Value;
    bool flag = true;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
    {
      object key1 = sender.GetValue(row, this._FieldOrdinal);
      if (operation == PXDBOperation.Insert && !this._DoubleDefaultAttribute && (key1 is string && ((string) key1).StartsWith(" ", StringComparison.InvariantCultureIgnoreCase) || key1 is int num1 && num1 < 0 || key1 is long num2 && num2 < 0L))
        sender.SetValue(row, this._FieldOrdinal, (object) null);
      object data;
      if (this._IsRestriction.Persisted == null || key1 == null || !this._IsRestriction.Persisted.TryGetValue(key1, out data))
        return;
      object key2 = sender.Graph.Caches[this._SourceType].GetValue(data, this._SourceField ?? this._FieldName);
      sender.SetValue(row, this._FieldOrdinal, key2);
      if (key2 == null)
        return;
      this._IsRestriction.Persisted[key2] = data;
    }
    else
    {
      sender.SetValue(row, this._FieldOrdinal, (object) null);
      if (this._Select != null)
      {
        List<object> objectList = sender.Graph.TypedViews.GetView(this._Select, false).SelectMultiBound(new object[1]
        {
          row
        });
        if (objectList == null || objectList.Count <= 0)
          return;
        object data = objectList[objectList.Count - 1];
        if (data is PXResult)
          data = ((PXResult) data)[0];
        sender.SetValue(row, this._FieldOrdinal, sender.Graph.Caches[this._SourceType].GetValue(data, this._SourceField ?? this._FieldName));
      }
      else
      {
        if (!(this._SourceType != (System.Type) null))
          return;
        PXCache cach = sender.Graph.Caches[this._SourceType];
        if (cach.Current == null)
          return;
        sender.SetValue(row, this._FieldOrdinal, cach.GetValue(cach.Current, this._SourceField ?? this._FieldName));
      }
    }
  }

  /// <summary>
  /// Rollback changes if the record was not saved to the database
  /// </summary>
  /// <param name="sender">Cache</param>
  /// <param name="e">Event arguments to check operation and get the row to process</param>
  /// <exclude />
  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (this.FieldUpdateMode != PXFieldUpdateMode.Default || e.TranStatus != PXTranStatus.Aborted)
      return;
    this.ResetFieldValue(sender, e.Row, e.Operation);
  }

  protected void ResetFieldValue(PXCache sender, object row, PXDBOperation operation)
  {
    operation &= PXDBOperation.Delete;
    if ((operation != PXDBOperation.Insert || !this._DefaultForInsert) && (operation != PXDBOperation.Update || !this._DefaultForUpdate) || !(this._SourceType != (System.Type) null))
      return;
    this.EnsureIsRestriction(sender);
    bool? nullable = this._IsRestriction.Value;
    bool flag = true;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
    {
      object key = sender.GetValue(row, this._FieldOrdinal);
      object data;
      if (this._IsRestriction.Persisted == null || key == null || !this._IsRestriction.Persisted.TryGetValue(key, out data))
        return;
      sender.SetValue(row, this._FieldOrdinal, sender.Graph.Caches[this._SourceType].GetValue(data, this._SourceField ?? this._FieldName));
    }
    else
    {
      sender.SetValue(row, this._FieldOrdinal, (object) null);
      if (this._Select != null)
      {
        List<object> objectList = sender.Graph.TypedViews.GetView(this._Select, false).SelectMultiBound(new object[1]
        {
          row
        });
        if (objectList == null || objectList.Count <= 0)
          return;
        sender.SetValue(row, this._FieldOrdinal, sender.Graph.Caches[this._SourceType].GetValue(objectList[objectList.Count - 1], this._SourceField ?? this._FieldName));
      }
      else
      {
        if (!(this._SourceType != (System.Type) null))
          return;
        PXCache cach = sender.Graph.Caches[this._SourceType];
        if (cach.Current == null)
          return;
        sender.SetValue(row, this._FieldOrdinal, cach.GetValue(cach.Current, this._SourceField ?? this._FieldName));
      }
    }
  }

  public virtual void SourceRowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    this.StorePersisted(sender, e.Row);
  }

  public void SourceRowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.Operation == PXDBOperation.Insert && e.TranStatus == PXTranStatus.Open)
      this.StorePersisted(sender, e.Row);
    if (this.FieldUpdateMode != PXFieldUpdateMode.OnParentPersisted)
      return;
    PXCache cach = sender.Graph.Caches[this._BqlTable];
    foreach (object row in NonGenericIEnumerableExtensions.Concat_(cach.Inserted, cach.Updated))
    {
      if (e.TranStatus == PXTranStatus.Open)
        this.SetFieldValue(cach, row, e.Operation);
      if (e.TranStatus == PXTranStatus.Aborted)
        this.ResetFieldValue(cach, row, e.Operation);
    }
    if (!EnumerableExtensions.IsIn<PXTranStatus>(e.TranStatus, PXTranStatus.Open, PXTranStatus.Aborted) || !NonGenericIEnumerableExtensions.Any_(cach.Inserted))
      return;
    bool? nullable = this._IsRestriction.Value;
    bool flag = true;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    cach.Normalize();
  }

  public void StorePersisted(PXCache sender, object row)
  {
    this.EnsureIsRestriction(sender);
    bool? nullable = this._IsRestriction.Value;
    bool flag = true;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    object key = sender.GetValue(row, this._SourceField ?? this._FieldName);
    if (key == null)
      return;
    if (this._IsRestriction.Persisted == null)
      this._IsRestriction.Persisted = new Dictionary<object, object>();
    if (this._Select != null && !this._Select.Meet(sender, row))
      return;
    this._IsRestriction.Persisted[key] = row;
  }

  protected virtual void Parameter_CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if (this._SourceType == (System.Type) null || (e.Operation & PXDBOperation.ReadItem) != PXDBOperation.ReadItem && ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Select || (e.Operation & PXDBOperation.Option) == PXDBOperation.ReadOnly || e.Row != null))
      return;
    PXCache cach = sender.Graph.Caches[this._SourceType];
    string str = this._SourceField ?? this._FieldName;
    PXCommandPreparingEventArgs.FieldDescription description;
    if (!cach.IsAutoNumber(str) || cach.RaiseCommandPreparing(str, (object) null, e.Value, e.Operation, (System.Type) null, out description))
      return;
    e.DataValue = description.DataValue;
    e.Cancel = true;
  }

  /// <summary>Sets the <tt>DefaultForUpdate</tt> property for a particular
  /// data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDBDefault</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records kept in the
  /// cache object.</param>
  /// <param name="def">The new value for the property.</param>
  /// <example>
  /// The code below changes the <tt>SetDefaultForUpdate</tt> property at
  /// run time. The method sets the property for the <tt>ShipAddressID</tt> field in all data
  /// records in the cache object associated with the <tt>OrderList</tt> data view.
  /// <code>
  /// PXDBDefaultAttribute.SetDefaultForUpdate&lt;SOOrderShipment.shipAddressID&gt;(
  ///     OrderList.Cache, null, false);
  /// </code>
  /// </example>
  public static void SetDefaultForUpdate<Field>(PXCache cache, object data, bool def) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXDBDefaultAttribute defaultAttribute in cache.GetAttributes<Field>(data).OfType<PXDBDefaultAttribute>())
      defaultAttribute._DefaultForUpdate = def;
  }

  /// <summary>Sets the <tt>DefaultForInsert</tt> property for a particular
  /// data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PDBXDefault</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records kept in the
  /// cache object.</param>
  /// <param name="def">The new value for the property.</param>
  public static void SetDefaultForInsert<Field>(PXCache cache, object data, bool def) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXDBDefaultAttribute defaultAttribute in cache.GetAttributes<Field>(data).OfType<PXDBDefaultAttribute>())
      defaultAttribute._DefaultForInsert = def;
  }

  /// <exclude />
  public static void SetSourceType(PXCache cache, string field, System.Type sourceType)
  {
    cache.SetAltered(field, true);
    foreach (PXDBDefaultAttribute defaultAttribute in cache.GetAttributes(field).OfType<PXDBDefaultAttribute>())
      defaultAttribute.SetSourceType(cache, sourceType);
  }

  /// <exclude />
  public static void SetSourceType<Field>(PXCache cache, System.Type sourceType) where Field : IBqlField
  {
    PXDBDefaultAttribute.SetSourceType(cache, typeof (Field).Name, sourceType);
  }

  /// <exclude />
  public static void SetSourceType(PXCache cache, object data, string field, System.Type sourceType)
  {
    if (data == null)
      cache.SetAltered(field, true);
    foreach (PXDBDefaultAttribute defaultAttribute in cache.GetAttributes(data, field).OfType<PXDBDefaultAttribute>())
      defaultAttribute.SetSourceType(cache, sourceType);
  }

  /// <exclude />
  public static void SetSourceType<Field>(PXCache cache, object data, System.Type sourceType) where Field : IBqlField
  {
    PXDBDefaultAttribute.SetSourceType(cache, data, typeof (Field).Name, sourceType);
  }

  protected virtual void SetSourceType(PXCache cache, System.Type sourceType)
  {
    System.Type type = sourceType;
    if ((object) type == null)
      type = this._OriginSourceType;
    sourceType = type;
    System.Type sourceType1 = this._SourceType;
    if (sourceType == (System.Type) null)
    {
      this._Select = (BqlCommand) null;
      this._SourceType = (System.Type) null;
      this._SourceField = (string) null;
    }
    else if (typeof (IBqlSearch).IsAssignableFrom(sourceType))
    {
      this._Select = BqlCommand.CreateInstance(sourceType);
      this._SourceType = BqlCommand.GetItemType(((IBqlSearch) this._Select).GetField());
      this._SourceField = ((IBqlSearch) this._Select).GetField().Name;
      this._SourceField = char.ToUpper(this._SourceField[0]).ToString() + this._SourceField.Substring(1);
    }
    else if (sourceType.IsNested && typeof (IBqlField).IsAssignableFrom(sourceType))
    {
      this._SourceType = BqlCommand.GetItemType(sourceType);
      this._SourceField = sourceType.Name;
      this._SourceField = char.ToUpper(this._SourceField[0]).ToString() + this._SourceField.Substring(1);
    }
    else if (typeof (IBqlTable).IsAssignableFrom(sourceType))
    {
      this._Select = (BqlCommand) null;
      this._SourceType = sourceType;
      this._SourceField = (string) null;
    }
    else
      throw new PXArgumentException(nameof (sourceType), "A foreign key reference cannot be created from the type '{0}'.", new object[1]
      {
        (object) sourceType
      });
    if (cache == null || !(sourceType1 != this._SourceType))
      return;
    if (sourceType1 != (System.Type) null)
      cache.Graph.RowPersisting.RemoveHandler(sourceType1, new PXRowPersisting(this.SourceRowPersisting));
    if (!(this._SourceType != (System.Type) null))
      return;
    cache.Graph.RowPersisting.AddHandler(this._SourceType, new PXRowPersisting(this.SourceRowPersisting));
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    if (this._SourceType != (System.Type) null)
    {
      sender.Graph.RowPersisting.AddHandler(this._SourceType, new PXRowPersisting(this.SourceRowPersisting));
      sender.Graph.RowPersisted.AddHandler(this._SourceType, new PXRowPersisted(this.SourceRowPersisted));
    }
    this._IsRestriction = new PXDBDefaultAttribute.FlagHandler();
    if (this._Select == null)
      sender.Graph.CommandPreparing.AddHandler(sender.GetItemType(), this._FieldName, new PXCommandPreparing(this.Parameter_CommandPreparing));
    this._DoubleDefaultAttribute = sender.GetAttributesReadonly(this._FieldName, false).Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (_ => this.GetType().IsAssignableFrom(_.GetType()) && this != _));
  }

  /// <exclude />
  protected class FlagHandler
  {
    public bool? Value;
    public Dictionary<object, object> Persisted;
  }
}

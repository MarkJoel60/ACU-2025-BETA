// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBChildIdentityAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using PX.DbServices.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Indicates that a DAC field references an auto-generated key
/// field from another table and ensures the DAC field's value is correct
/// after changes are committed to the database.</summary>
/// <remarks>The attribute updates the field value once the source field is
/// assigned a real value by the database.</remarks>
/// <example>
/// <code>
/// [PXDBInt()]
/// [PXDBChildIdentity(typeof(Address.addressID))]
/// public virtual int? DefPOAddressID { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public class PXDBChildIdentityAttribute : 
  PXEventSubscriberAttribute,
  IPXRowPersistingSubscriber,
  IPXRowPersistedSubscriber
{
  protected System.Type _SourceType;
  protected string _SourceField;
  protected Dictionary<object, object> _Persisted;
  protected System.Type _SelfType;

  /// <summary>
  /// Initializes a new instance that takes the value for the field
  /// the attribute is attached to from the provided source field.
  /// </summary>
  /// <param name="sourceType">The source field type to get the value from, should
  /// be nested (defined in a DAC) and implement <tt>IBqlField</tt>.</param>
  public PXDBChildIdentityAttribute(System.Type sourceType)
  {
    if (sourceType == (System.Type) null)
      throw new ArgumentNullException(nameof (sourceType));
    this._SourceType = sourceType.IsNested && typeof (IBqlField).IsAssignableFrom(sourceType) ? BqlCommand.GetItemType(sourceType) : throw new ArgumentOutOfRangeException(nameof (sourceType), $"Cannot create a foreign key reference from the type '{sourceType}'.");
    this._SourceField = sourceType.Name;
  }

  /// <exclude />
  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Delete)
      return;
    object obj1 = sender.GetValue(e.Row, this._FieldOrdinal);
    if (obj1 == null || Convert.ToInt32(obj1) >= 0)
      return;
    this._Persisted[e.Row] = obj1;
    PXCache cach = sender.Graph.Caches[this._SourceType];
    object data;
    if (this._Persisted.TryGetValue(obj1, out data) && this._SourceType.IsAssignableFrom(data.GetType()))
      sender.SetValue(e.Row, this._FieldOrdinal, cach.GetValue(data, this._SourceField));
    foreach (object obj2 in cach.Inserted)
    {
      if (object.Equals(obj1, cach.GetValue(obj2, this._SourceField)))
      {
        if (!this._Persisted.TryGetValue(obj2, out object _))
          this._Persisted[obj2] = (object) new List<object>();
        ((List<object>) this._Persisted[obj2]).Add(e.Row);
      }
    }
  }

  /// <exclude />
  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    object obj;
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Delete || e.TranStatus != PXTranStatus.Aborted || !this._Persisted.TryGetValue(e.Row, out obj))
      return;
    sender.SetValue(e.Row, this._FieldOrdinal, obj);
  }

  /// <exclude />
  public void SourceRowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert)
      return;
    object key = sender.GetValue(e.Row, this._SourceField);
    if (key == null || Convert.ToInt32(key) >= 0)
      return;
    this._Persisted[key] = e.Row;
  }

  /// <exclude />
  public void SourceRowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    object obj1;
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert || e.TranStatus != PXTranStatus.Open || !this._Persisted.TryGetValue(e.Row, out obj1) || obj1 == null || !(obj1 is List<object>))
      return;
    foreach (object obj2 in (List<object>) obj1)
    {
      if (this._SelfType.IsAssignableFrom(obj2.GetType()))
      {
        object id = sender.GetValue(e.Row, this._SourceField);
        int num = id.Return<object, int>((Func<object, int>) (c => Convert.ToInt32(id)), 0);
        if (id != null && num > 0)
        {
          if (sender.Graph.TimeStamp == null)
            sender.Graph.SelectTimeStamp();
          PXCache cach = sender.Graph.Caches[this._SelfType];
          List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>();
          PXCommandPreparingEventArgs.FieldDescription description1 = (PXCommandPreparingEventArgs.FieldDescription) null;
          cach.RaiseCommandPreparing(this._FieldName, obj2, id, PXDBOperation.Update, this._BqlTable, out description1);
          if (description1 != null && description1.Expr != null)
          {
            PXDataFieldAssign pxDataFieldAssign = new PXDataFieldAssign((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue, cach.ValueToString(this._FieldName, id));
            pxDataFieldParamList.Add((PXDataFieldParam) pxDataFieldAssign);
          }
          KeysVerifyer keysVerifyer = new KeysVerifyer(cach);
          foreach (string field1 in (List<string>) cach.Fields)
          {
            string field = field1;
            PXCommandPreparingEventArgs.FieldDescription description2;
            cach.RaiseCommandPreparing(field, obj2, cach.GetValue(obj2, field), PXDBOperation.Update, this._BqlTable, out description2);
            if (description2 != null)
            {
              if (description2.Expr == null)
                keysVerifyer.ExcludeField(field);
              else if (description2.IsRestriction && description2.DataValue != null && description2.DataType != PXDbType.Timestamp)
              {
                keysVerifyer.ExcludeField(field);
                if (!this._BqlTable.IsDefined(typeof (PXTableAttribute), true) || PXDatabase.Provider.GetTableStructure(this._BqlTable.Name).Columns.Any<TableColumn>((Func<TableColumn, bool>) (c => string.Equals(((TableEntityBase) c).Name, field, StringComparison.OrdinalIgnoreCase))))
                  pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description2.Expr, description2.DataType, description2.DataLength, description2.DataValue));
              }
            }
          }
          pxDataFieldParamList.Add((PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed);
          keysVerifyer.Check(this._BqlTable);
          pxDataFieldParamList.Add((PXDataFieldParam) PXSelectOriginalsRestrict.SelectAllOriginalValues);
          PXDatabase.Update(this._BqlTable, pxDataFieldParamList.ToArray());
          cach.SetValue(obj2, this._FieldOrdinal, id);
        }
      }
    }
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    this._Persisted = new Dictionary<object, object>();
    this._SelfType = sender.GetItemType();
    sender.Graph.RowPersisting.AddHandler(this._SourceType, new PXRowPersisting(this.SourceRowPersisting));
    sender.Graph.RowPersisted.AddHandler(this._SourceType, new PXRowPersisted(this.SourceRowPersisted));
  }
}

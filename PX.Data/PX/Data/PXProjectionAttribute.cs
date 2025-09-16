// Decompiled with JetBrains decompiler
// Type: PX.Data.PXProjectionAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Monads;

#nullable disable
namespace PX.Data;

/// <summary>Binds the DAC to an arbitrary data set defined by the
/// <tt>Select</tt> command. The attribute thus defines a named view, but
/// implemented by the server side rather then the database.</summary>
/// <remarks>
/// <para>You can place the attribute on the DAC declaration. The
/// framework doesn't bind such DAC to a database table (that is,
/// doesn't select data from the table having the same name as the DAC).
/// Instead, you specify an arbitrary BQL <tt>Select</tt> command that is
/// executed to retrieve data for the DAC. The <tt>Select</tt> command can
/// select data from one or several commands and include any BQL
/// clauses.</para>
/// <para>By default, the projection is read-only, but you can make it
/// updatable by setting the <tt>Persistent</tt> property to
/// <tt>true</tt>. The attribute will use the <tt>Select</tt> command to
/// determine which tables needs updating. However, only the first table
/// referenced by the <tt>Select</tt> command is updated by default. If
/// the data should be committed not only into main table, but also to the
/// joined tables, the fields that connect the tables must be marked with
/// the <see cref="T:PX.Data.PXExtraKeyAttribute">PXExtraKey</see> attribute.
/// Additionally, you can use the constructor with two parameters to
/// provide the list of table explicitly. This list should include the
/// tables referenced in the <tt>Select</tt> command. This constructor
/// will also set the <tt>Persistent</tt> property to
/// <tt>true</tt>.</para>
/// <para>You should explicitly map the projection fields to the column
/// retrieved by the <tt>Select</tt> command. To map a field, set the
/// <tt>BqlField</tt> property of the attribute that binds the field to
/// the database (such as <tt>PXDBString</tt> and <tt>PXDBDecimal</tt>) to
/// the type that represents the column, as follows.</para>
/// <code>[PXDBString(15, IsUnicode = true, BqlField = typeof(Supplier.accountCD))]</code>
/// </remarks>
/// <seealso cref="T:PX.Data.PXExtraKeyAttribute"></seealso>
/// <example>
/// <code title="Example" lang="CS">
/// // In the following example, the attribute joins data from two table and projects it to the single DAC.
/// [Serializable]
/// [PXProjection(typeof(
///     Select2&lt;Supplier,
///         InnerJoin&lt;SupplierProduct,
///             On&lt;SupplierProduct.accountID, Equal&lt;Supplier.accountID&gt;&gt;&gt;&gt;))]
/// public partial class SupplierPrice : PXBqlTable, IBqlTable
/// {
///     public abstract class accountID : PX.Data.BQL.BqlInt.Field&lt;accountID&gt;
///     {
///     }
///     // The field mapped to the Supplier field (through setting of BqlField)
///     [PXDBInt(IsKey = true, BqlField = typeof(Supplier.accountID))]
///     public virtual int? AccountID { get; set; }
///     public abstract class productID : PX.Data.BQL.BqlInt.Field&lt;productID&gt;
///     {
///     }
///     // The field mapped to the SupplierProduct field
///     // (through setting of BqlField)
///     [PXDBInt(IsKey = true, BqlField = typeof(SupplierProduct.productID))]
///     [PXUIField(DisplayName = "Product ID")]
///     public virtual int? ProductID { get; set; }
///     ...
/// }</code>
/// <code title="Example2" lang="CS">
/// // Note how the DAC declares the fields. The projection defined in the example is readonly.
/// // To make it updatable, you should set the Persistent property to true,
/// // changing the attribute declaration to the following one.
/// [PXProjection(
///     typeof(Select2&lt;Supplier,
///         InnerJoin&lt;SupplierProduct,
///             On&lt;SupplierProduct.accountID, Equal&lt;Supplier.accountID&gt;&gt;&gt;&gt;),
///     Persistent = true
/// )]</code>
/// <code title="Example3" lang="CS">
/// // If the projection should be able to update both tables,
/// // you should place the PXExtraKey attribute on the field that
/// // relates the tables (the AccountID property) as follows.
/// [PXDBInt(IsKey = true, BqlField = typeof(Supplier.accountID))]
/// [PXExtraKey]
/// public virtual int? AccountID { get; set; }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class)]
public class PXProjectionAttribute : PXDBInterceptorAttribute
{
  protected System.Type _select;
  protected System.Type[] _tables;
  protected bool _persistent;
  protected BqlCommand rowselection;
  protected BqlCommand tableselection;
  protected BqlCommand rowSelectionByNoteId;
  protected bool canSelectByNoteId;

  internal System.Type Select => this._select;

  /// <summary>Gets or sets the value that indicates whether the instances
  /// of the DAC that represents the projection can be saved to the
  /// database. If the property equals <tt>true</tt>, the attribute will
  /// parse the <tt>Select</tt> command and determine the tables that should
  /// be updated. Alternatively, you can specify the list of tables in the
  /// constructor. If the property equals <tt>false</tt>, the DAC is
  /// read-only.</summary>
  /// <example>
  /// The projection defined below can update the <tt>Supplier</tt> table.
  /// <code>
  /// [PXProjection(
  ///     typeof(Select2&lt;Supplier,
  ///         InnerJoin&lt;SupplierProduct,
  ///             On&lt;SupplierProduct.accountID, Equal&lt;Supplier.accountID&gt;&gt;&gt;&gt;),
  ///     Persistent = true
  /// )]
  /// public partial class SupplierPrice : PXBqlTable, IBqlTable
  /// { ... }
  /// </code>
  /// </example>
  public bool Persistent
  {
    get => this._persistent;
    set => this._persistent = value;
  }

  public System.Type CommerceTranType { get; set; }

  /// <summary>Initializes a new instance that binds the DAC to the data set
  /// defined by the provided <tt>Select</tt> command.</summary>
  /// <param name="select">The BQL command that defines the data set, based
  /// on the <tt>Select</tt> class or any other class that implements
  /// <tt>IBqlSelect</tt>.</param>
  public PXProjectionAttribute(System.Type select)
  {
    this._select = select;
    this.tableselection = (BqlCommand) Activator.CreateInstance(this._select);
  }

  /// <summary>Initializes a new instance that binds the DAC to the
  /// specified data set and enables update saving of the DAC instances to
  /// the database. The tables that should be updated during update of the
  /// current DAC.</summary>
  /// <param name="select">The BQL command that defines the data set, based
  /// on the <tt>Select</tt> class or any other class that implements
  /// <tt>IBqlSelect</tt>.</param>
  /// <param name="persistent">The list of DACs that represent the tables to
  /// update during update of the current DAC.</param>
  public PXProjectionAttribute(System.Type select, System.Type[] persistent)
    : this(select)
  {
    this._tables = persistent;
    this.Persistent = true;
  }

  /// <exclude />
  public override System.Type[] GetTables()
  {
    return Array.FindAll<System.Type>(base.GetTables(), (Predicate<System.Type>) (a => this._tables == null || Array.IndexOf<System.Type>(this._tables, a) >= 0));
  }

  protected virtual System.Type GetSelect(PXCache sender) => this._select;

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    this.tableselection = (BqlCommand) Activator.CreateInstance(this.GetSelect(sender));
    System.Type type1 = (System.Type) null;
    foreach (System.Type bqlKey in sender.BqlKeys)
    {
      System.Type type2;
      if (!(type1 == (System.Type) null))
        type2 = BqlCommand.Compose(typeof (Where2<,>), type1, typeof (And<>), typeof (Where<,>), bqlKey, typeof (Equal<>), typeof (Required<>), bqlKey);
      else
        type2 = BqlCommand.Compose(typeof (Where<,>), bqlKey, typeof (Equal<>), typeof (Required<>), bqlKey);
      type1 = type2;
    }
    BqlCommand instance;
    if (!(type1 == (System.Type) null))
      instance = BqlCommand.CreateInstance(typeof (PX.Data.Select<,>), sender.GetItemType(), type1);
    else
      instance = BqlCommand.CreateInstance(typeof (PX.Data.Select<>), sender.GetItemType());
    this.rowselection = instance;
    System.Type bqlField = sender.GetBqlField("noteid");
    if (bqlField != (System.Type) null)
    {
      this.rowSelectionByNoteId = BqlCommand.CreateInstance(typeof (PX.Data.Select<,>), sender.GetItemType(), BqlCommand.Compose(typeof (Where<,>), bqlField, typeof (Equal<>), typeof (Required<>), bqlField));
      this.canSelectByNoteId = true;
    }
    if (this.Child == null)
      return;
    this.tableselection = (BqlCommand) new PXProjectionAttribute.BqlRowSelection(sender, this.Child, this.tableselection);
    this.rowselection = (BqlCommand) new PXProjectionAttribute.BqlRowSelection(sender, (PXDBInterceptorAttribute) this, this.rowselection);
    this.rowSelectionByNoteId = (BqlCommand) new PXProjectionAttribute.BqlRowSelection(sender, (PXDBInterceptorAttribute) this, this.rowSelectionByNoteId);
  }

  /// <exclude />
  public override BqlCommand GetRowCommand() => this.rowselection;

  /// <exclude />
  public override BqlCommand GetRowByNoteIdCommand() => this.rowSelectionByNoteId;

  /// <exclude />
  public override bool CanSelectByNoteId => this.canSelectByNoteId;

  /// <exclude />
  public override BqlCommand GetTableCommand() => this.tableselection;

  /// <exclude />
  public override bool PersistInserted(PXCache sender, object row)
  {
    ISqlDialect sqlDialect = sender.Graph.SqlDialect;
    if (!this._persistent)
      return base.PersistInserted(sender, row);
    System.Type[] collection1 = this.GetTables();
    Dictionary<System.Type, int> baseTables = new Dictionary<System.Type, int>();
    List<PXDataFieldAssign>[] array1 = new List<PXDataFieldAssign>[collection1.Length];
    List<string>[] array2 = new List<string>[collection1.Length];
    for (int index = 0; index < collection1.Length; ++index)
    {
      array1[index] = new List<PXDataFieldAssign>();
      array2[index] = new List<string>();
    }
    int length = collection1.Length;
    for (int i = 0; i < length; i++)
    {
      System.Type type = collection1[i];
      PXCache cach = sender.Graph.Caches[collection1[i]];
      collection1[i] = cach.BqlTable;
      List<System.Type> extensionTables = cach.GetExtensionTables();
      if (extensionTables != null)
      {
        if (i > 0)
          extensionTables.ForEach((System.Action<System.Type>) (_ => baseTables.Add(_, i)));
        int count = extensionTables.Count;
        List<System.Type> typeList = new List<System.Type>((IEnumerable<System.Type>) extensionTables);
        typeList.InsertRange(0, (IEnumerable<System.Type>) collection1);
        collection1 = typeList.ToArray();
        Array.Resize<List<string>>(ref array2, typeList.Count);
        Array.Resize<List<PXDataFieldAssign>>(ref array1, typeList.Count);
        if (cach.BqlSelect is IPXExtensibleTableAttribute)
        {
          for (int index = array2.Length - count; index < array2.Length; ++index)
          {
            array2[index] = new List<string>((IEnumerable<string>) ((IPXExtensibleTableAttribute) cach.BqlSelect).Keys);
            array1[index] = new List<PXDataFieldAssign>();
          }
        }
        else
        {
          for (int index = array2.Length - count; index < array2.Length; ++index)
          {
            array2[index] = new List<string>((IEnumerable<string>) cach.Keys);
            array1[index] = new List<PXDataFieldAssign>();
          }
        }
      }
    }
    System.Type table = sender.BqlTable;
    bool flag1;
    while (!(flag1 = PXDatabase.AuditRequired(table)) && table.BaseType != typeof (object))
      table = table.BaseType;
    bool flag2 = sender._HasKeyValueStored();
    foreach (string field in (List<string>) sender.Fields)
    {
      object val = sender.GetValue(row, field);
      PXCommandPreparingEventArgs.FieldDescription description;
      sender.RaiseCommandPreparing(field, row, val, PXDBOperation.Insert, (System.Type) null, out description);
      if (description?.Expr != null && !description.IsExcludedFromUpdate)
      {
        for (int index1 = 0; index1 < collection1.Length; ++index1)
        {
          if (this.tableMeet(description, collection1[index1], sqlDialect))
          {
            if (array1[index1] != null)
            {
              if (index1 > 0 && description.IsRestriction)
              {
                if (description.DataValue == null)
                {
                  array1[index1] = (List<PXDataFieldAssign>) null;
                  for (int index2 = length; index2 < collection1.Length; ++index2)
                  {
                    if (collection1[index2].BaseType.GetGenericArguments()[collection1[index2].BaseType.GetGenericArguments().Length - 1].IsAssignableFrom(collection1[index1]))
                      array1[index2] = (List<PXDataFieldAssign>) null;
                  }
                  break;
                }
                array2[index1].Add(field);
                break;
              }
              int num = flag1 ? 1 : (PXDatabase.AuditRequired(collection1[index1]) ? 1 : 0);
              PXDataFieldAssign assign = new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue, (string) null);
              if (num != 0 && val != null)
              {
                assign.IsChanged = true;
                assign.NewValue = sender.ValueToString(field, val, description.DataValue);
              }
              else
                assign.IsChanged = false;
              if (flag2 && string.Equals(field, sender._NoteIDName, StringComparison.OrdinalIgnoreCase))
              {
                if (assign.Value == null)
                {
                  assign.Value = (object) SequentialGuid.Generate();
                  sender.SetValue(row, sender._NoteIDOrdinal.Value, assign.Value);
                }
                PXDataFieldAssign pxDataFieldAssign = new PXDataFieldAssign(sender._NoteIDName, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), assign.Value);
                pxDataFieldAssign.Storage = StorageBehavior.KeyValueKey;
                array1[index1].Add(pxDataFieldAssign);
                flag2 = false;
              }
              sender._AdjustStorage(field, (PXDataFieldParam) assign);
              array1[index1].Add(assign);
              break;
            }
            break;
          }
        }
      }
    }
    for (int index = 0; index < length; ++index)
    {
      if (array1[index] != null && array1[index].Count > 0)
      {
        PXCache tc = sender.Graph.Caches[collection1[index]];
        if (tc._NoteIDName != null)
        {
          PXDataFieldAssign pxDataFieldAssign1;
          if ((pxDataFieldAssign1 = array1[index].FirstOrDefault<PXDataFieldAssign>((Func<PXDataFieldAssign, bool>) (p => string.Equals(p.Column.Name, tc._NoteIDName, StringComparison.OrdinalIgnoreCase)))) == null)
          {
            object val = (object) SequentialGuid.Generate();
            PXCommandPreparingEventArgs.FieldDescription description;
            tc.RaiseCommandPreparing(tc._NoteIDName, (object) null, val, PXDBOperation.Insert, (System.Type) null, out description);
            if (description?.Expr != null && !description.IsExcludedFromUpdate && !description.IsRestriction && this.tableMeet(description, collection1[index], sqlDialect))
            {
              int num = flag1 ? 1 : (PXDatabase.AuditRequired(collection1[index]) ? 1 : 0);
              PXDataFieldAssign pxDataFieldAssign2 = new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue, (string) null);
              if (num != 0 && val != null)
              {
                pxDataFieldAssign2.IsChanged = true;
                pxDataFieldAssign2.NewValue = tc.ValueToString(tc._NoteIDName, val);
              }
              else
                pxDataFieldAssign2.IsChanged = false;
              array1[index].Add(pxDataFieldAssign2);
              pxDataFieldAssign1 = pxDataFieldAssign2;
            }
          }
          if (pxDataFieldAssign1 != null && pxDataFieldAssign1.Value != null && sender._KeyValueAttributeNames != null)
          {
            PXDataFieldAssign pxDataFieldAssign3 = new PXDataFieldAssign(tc._NoteIDName, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), pxDataFieldAssign1.Value);
            pxDataFieldAssign3.Storage = StorageBehavior.KeyValueKey;
            array1[index].Add(pxDataFieldAssign3);
            object[] slot1 = sender.GetSlot<object[]>(row, sender._KeyValueAttributeSlotPosition);
            object[] slot2 = sender.GetSlot<object[]>(row, sender._KeyValueAttributeSlotPosition, true);
            if (slot1 != null)
            {
              foreach (KeyValuePair<string, int> valueAttributeName in sender._KeyValueAttributeNames)
              {
                if (valueAttributeName.Value < slot1.Length)
                {
                  PXDataFieldAssign pxDataFieldAssign4 = new PXDataFieldAssign(valueAttributeName.Key, sender._KeyValueAttributeTypes[valueAttributeName.Key] == StorageBehavior.KeyValueDate ? PXDbType.DateTime : (sender._KeyValueAttributeTypes[valueAttributeName.Key] == StorageBehavior.KeyValueNumeric ? PXDbType.Bit : PXDbType.NVarChar), slot1[valueAttributeName.Value]);
                  pxDataFieldAssign4.Storage = sender._KeyValueAttributeTypes[valueAttributeName.Key];
                  if (pxDataFieldAssign4.IsChanged = slot2 != null && valueAttributeName.Value < slot2.Length && !object.Equals(pxDataFieldAssign4.Value, slot2[valueAttributeName.Value]))
                  {
                    pxDataFieldAssign4.NewValue = sender.AttributeValueToString(valueAttributeName.Key, pxDataFieldAssign4.Value);
                    pxDataFieldAssign4.OldValue = slot2[valueAttributeName.Value];
                  }
                  array1[index].Add(pxDataFieldAssign4);
                }
              }
            }
          }
        }
      }
    }
    try
    {
      array1[0].Add(PXDataFieldAssign.OperationSwitchAllowed);
      sender.Graph.ProviderInsert(collection1[0], array1[0].ToArray());
    }
    catch (PXDbOperationSwitchRequiredException ex)
    {
      List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>();
      object unchanged = sender.GetOriginal(row);
      foreach (string field1 in (List<string>) sender.Fields)
      {
        string field = field1;
        PXCommandPreparingEventArgs.FieldDescription description1;
        sender.RaiseCommandPreparing(field, row, sender.GetValue(row, field), PXDBOperation.Update | PXDBOperation.Second, (System.Type) null, out description1);
        if (description1?.Expr != null && !description1.IsExcludedFromUpdate && this.tableMeet(description1, collection1[0], sqlDialect))
        {
          object obj = MaybeObjects.With<object, object>(unchanged, (Func<object, object>) (c => sender.GetValue(unchanged, field)));
          PXCommandPreparingEventArgs.FieldDescription description2 = (PXCommandPreparingEventArgs.FieldDescription) null;
          if (obj != null)
            sender.RaiseCommandPreparing(field, unchanged, obj, PXDBOperation.Select, collection1[0], out description2);
          if (description1.IsRestriction)
            pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue));
          else
            pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue)
            {
              OldValue = (description2?.DataValue ?? obj)
            });
        }
      }
      IEnumerable<PXDataFieldAssign> collection2 = array1[0].Where<PXDataFieldAssign>((Func<PXDataFieldAssign, bool>) (p => p.Storage != 0));
      pxDataFieldParamList.AddRange((IEnumerable<PXDataFieldParam>) collection2);
      sender.Graph.ProviderUpdate(collection1[0], pxDataFieldParamList.ToArray());
    }
    try
    {
      sender.RaiseRowPersisted(row, PXDBOperation.Insert, PXTranStatus.Open, (Exception) null);
      BqlTablePair bqlTablePair;
      if (sender._Originals.TryGetValue((IBqlTable) row, out bqlTablePair))
      {
        if (sender._OriginalsRemoved == null)
          sender._OriginalsRemoved = new PXCacheRemovedOriginalsCollection();
        sender._OriginalsRemoved[(IBqlTable) row] = bqlTablePair;
      }
      sender._Originals.Remove((IBqlTable) row);
    }
    catch (PXRowPersistedException ex)
    {
      sender.RaiseExceptionHandling(ex.Name, row, ex.Value, (Exception) ex);
      throw;
    }
    for (int index3 = 1; index3 < collection1.Length; ++index3)
    {
      if (array1[index3] != null)
      {
        if (array1[index3].Count > 0)
        {
          try
          {
            foreach (string str in array2[index3])
            {
              string field = str;
              object val = sender.GetValue(row, field);
              if (baseTables.ContainsKey(collection1[index3]))
              {
                int index4 = baseTables[collection1[index3]];
                PXDataFieldAssign pxDataFieldAssign;
                if ((pxDataFieldAssign = array1[index4].FirstOrDefault<PXDataFieldAssign>((Func<PXDataFieldAssign, bool>) (p => string.Equals(p.Column.Name, field, StringComparison.OrdinalIgnoreCase)))) != null)
                  val = pxDataFieldAssign.Value;
              }
              PXCommandPreparingEventArgs.FieldDescription description;
              if (index3 < length)
                sender.RaiseCommandPreparing(field, row, val, PXDBOperation.Insert, (System.Type) null, out description);
              else
                sender.RaiseCommandPreparing(field, row, val, PXDBOperation.Update, collection1[index3], out description);
              if (description?.Expr != null && !description.IsExcludedFromUpdate)
              {
                int num = flag1 ? 1 : (PXDatabase.AuditRequired(collection1[index3]) ? 1 : 0);
                PXDataFieldAssign pxDataFieldAssign = new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue, (string) null);
                if (num != 0 && val != null)
                {
                  pxDataFieldAssign.IsChanged = true;
                  pxDataFieldAssign.NewValue = sender.ValueToString(field, val, description.DataValue);
                }
                else
                  pxDataFieldAssign.IsChanged = false;
                array1[index3].Add(pxDataFieldAssign);
              }
            }
            array1[index3].Add(PXDataFieldAssign.OperationSwitchAllowed);
            try
            {
              if (this.CommerceTranType != (System.Type) null)
                PXContext.SetSlot<System.Type>("PXProjectionCommerceTranType", this.CommerceTranType);
              sender.Graph.ProviderInsert(collection1[index3], array1[index3].ToArray());
            }
            finally
            {
              PXContext.ClearSlot("PXProjectionCommerceTranType");
            }
          }
          catch (PXDbOperationSwitchRequiredException ex)
          {
            List<PXDataFieldParam>[] pxDataFieldParamListArray = new List<PXDataFieldParam>[collection1.Length];
            object unchanged = sender.GetOriginal(row);
            foreach (string field2 in (List<string>) sender.Fields)
            {
              string field = field2;
              PXCommandPreparingEventArgs.FieldDescription description3;
              sender.RaiseCommandPreparing(field, row, sender.GetValue(row, field), PXDBOperation.Update | PXDBOperation.Second, (System.Type) null, out description3);
              if (description3?.Expr != null && !description3.IsExcludedFromUpdate)
              {
                for (int index5 = 0; index5 < collection1.Length; ++index5)
                {
                  if (this.tableMeet(description3, collection1[index5], sqlDialect))
                  {
                    object obj = MaybeObjects.With<object, object>(unchanged, (Func<object, object>) (c => sender.GetValue(unchanged, field)));
                    PXCommandPreparingEventArgs.FieldDescription description4 = (PXCommandPreparingEventArgs.FieldDescription) null;
                    if (obj != null)
                      sender.RaiseCommandPreparing(field, unchanged, obj, PXDBOperation.Select, collection1[0], out description4);
                    PXDataFieldParam pxDataFieldParam1;
                    if (!description3.IsRestriction)
                      pxDataFieldParam1 = (PXDataFieldParam) new PXDataFieldAssign((Column) description3.Expr, description3.DataType, description3.DataLength, description3.DataValue)
                      {
                        OldValue = (description4?.DataValue ?? obj)
                      };
                    else
                      pxDataFieldParam1 = (PXDataFieldParam) new PXDataFieldRestrict((Column) description3.Expr, description3.DataType, description3.DataLength, description3.DataValue);
                    PXDataFieldParam pxDataFieldParam2 = pxDataFieldParam1;
                    if (pxDataFieldParamListArray[index5] == null)
                      pxDataFieldParamListArray[index5] = new List<PXDataFieldParam>();
                    pxDataFieldParamListArray[index5].Add(pxDataFieldParam2);
                    if (pxDataFieldParam2 is PXDataFieldAssign)
                      break;
                  }
                }
              }
            }
            int num = 0;
            bool sharedDelete = PXTransactionScope.GetSharedDelete();
            for (int index6 = 0; index6 < collection1.Length; ++index6)
            {
              if ((!sharedDelete || index6 == index3) && pxDataFieldParamListArray[index6] != null && sender.Graph.ProviderUpdate(collection1[index6], pxDataFieldParamListArray[index6].ToArray()))
                ++num;
            }
          }
        }
      }
    }
    try
    {
      BqlTablePair bqlTablePair;
      if (sender._Originals.TryGetValue((IBqlTable) row, out bqlTablePair))
      {
        if (sender._OriginalsRemoved == null)
          sender._OriginalsRemoved = new PXCacheRemovedOriginalsCollection();
        sender._OriginalsRemoved[(IBqlTable) row] = bqlTablePair;
      }
      sender._Originals.Remove((IBqlTable) row);
    }
    catch (PXRowPersistedException ex)
    {
      sender.RaiseExceptionHandling(ex.Name, row, ex.Value, (Exception) ex);
      throw;
    }
    return true;
  }

  /// <exclude />
  public override bool PersistUpdated(PXCache sender, object row)
  {
    ISqlDialect sqlDialect = sender.Graph.SqlDialect;
    if (!this._persistent)
      return base.PersistUpdated(sender, row);
    System.Type[] collection = this.GetTables();
    Dictionary<System.Type, int> baseTables = new Dictionary<System.Type, int>();
    string[][] array1 = new string[collection.Length][];
    List<PXDataFieldParam>[] array2 = new List<PXDataFieldParam>[collection.Length];
    for (int index = 0; index < collection.Length; ++index)
      array2[index] = new List<PXDataFieldParam>();
    int length = collection.Length;
    for (int i = 0; i < length; i++)
    {
      System.Type type = collection[i];
      PXCache cach = sender.Graph.Caches[collection[i]];
      collection[i] = cach.BqlTable;
      List<System.Type> extensionTables = cach.GetExtensionTables();
      if (extensionTables != null)
      {
        if (i > 0)
          extensionTables.ForEach((System.Action<System.Type>) (_ => baseTables.Add(_, i)));
        int count = extensionTables.Count;
        List<System.Type> typeList = new List<System.Type>((IEnumerable<System.Type>) extensionTables);
        typeList.InsertRange(0, (IEnumerable<System.Type>) collection);
        collection = typeList.ToArray();
        Array.Resize<string[]>(ref array1, typeList.Count);
        Array.Resize<List<PXDataFieldParam>>(ref array2, typeList.Count);
        if (cach.BqlSelect is IPXExtensibleTableAttribute)
        {
          for (int index = array2.Length - count; index < array2.Length; ++index)
          {
            array2[index] = new List<PXDataFieldParam>();
            array1[index] = ((IPXExtensibleTableAttribute) cach.BqlSelect).Keys;
          }
        }
        else
        {
          for (int index = array2.Length - count; index < array2.Length; ++index)
          {
            array2[index] = new List<PXDataFieldParam>();
            array1[index] = cach.Keys.ToArray();
          }
        }
      }
    }
    object unchanged = sender.GetOriginal(row);
    System.Type table = sender.BqlTable;
    bool flag1;
    while (!(flag1 = PXDatabase.AuditRequired(table)) && table.BaseType != typeof (object))
      table = table.BaseType;
    int index1 = -1;
    bool flag2 = sender._HasKeyValueStored();
    foreach (string field1 in (List<string>) sender.Fields)
    {
      string field = field1;
      object obj1 = sender.GetValue(row, field);
      PXCommandPreparingEventArgs.FieldDescription description1;
      sender.RaiseCommandPreparing(field, row, obj1, PXDBOperation.Update, (System.Type) null, out description1);
      if (description1?.Expr != null && !description1.IsExcludedFromUpdate)
      {
        if (index1 >= 0 && description1.DataType == PXDbType.Timestamp && sender.Graph._primaryRecordTimeStamp != null && sender.Graph.PrimaryItemType != (System.Type) null && sender.Graph.Caches[sender.Graph.PrimaryItemType] == sender)
        {
          byte[] primaryRecordTimeStamp = sender.Graph._primaryRecordTimeStamp;
          sender.Graph._primaryRecordTimeStamp = (byte[]) null;
          try
          {
            sender.RaiseCommandPreparing(field, row, obj1, PXDBOperation.Update, (System.Type) null, out description1);
          }
          finally
          {
            sender.Graph._primaryRecordTimeStamp = primaryRecordTimeStamp;
          }
        }
        for (int index2 = 0; index2 < collection.Length; ++index2)
        {
          if (this.tableMeet(description1, collection[index2], sqlDialect))
          {
            object obj2 = MaybeObjects.With<object, object>(unchanged, (Func<object, object>) (c => sender.GetValue(unchanged, field)));
            PXCommandPreparingEventArgs.FieldDescription description2 = (PXCommandPreparingEventArgs.FieldDescription) null;
            if (obj2 != null)
              sender.RaiseCommandPreparing(field, unchanged, obj2, PXDBOperation.Select, collection[index2], out description2);
            if (description1.IsRestriction)
            {
              if (description1.DataType == PXDbType.Timestamp)
                index1 = index2;
              else if (description1.DataValue != null && unchanged != null && !object.Equals(obj2, obj1) && obj2 != null)
              {
                string field2 = (string) null;
                if (((description1.Expr is Column expr ? expr.Table() : (Table) null) is SimpleTable simpleTable ? simpleTable.Name : (string) null) == collection[index2].Name)
                  field2 = (description1.Expr as Column).Name;
                if (field2 != null && sender.Graph.Caches[collection[index2]].Keys.Contains(field2) && description2 != null && description2.Expr != null)
                {
                  array2[index2].Add((PXDataFieldParam) new PXDataFieldAssign((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue, sender.ValueToString(field, obj1, description1.DataValue))
                  {
                    OldValue = (description2.DataValue ?? obj2),
                    IsChanged = true
                  });
                  description1 = description2;
                }
              }
              if (array2[index2] != null)
              {
                if (index2 > 0 && description1.DataValue == null)
                {
                  array2[index2] = (List<PXDataFieldParam>) null;
                  break;
                }
                array2[index2].Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue));
                break;
              }
              break;
            }
            if (array2[index2] != null)
            {
              if (description1.IsExcludedFromUpdate)
              {
                if (unchanged != null)
                {
                  PXDummyDataFieldRestrict dataFieldRestrict = new PXDummyDataFieldRestrict((Column) description1.Expr, description1.DataType, description1.DataLength, obj2);
                  array2[index2].Add((PXDataFieldParam) dataFieldRestrict);
                  break;
                }
                break;
              }
              PXDataFieldAssign assign = new PXDataFieldAssign((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue, (string) null);
              if (unchanged != null)
              {
                if (assign.IsChanged = !object.Equals(sender.GetValue(row, field), obj2))
                {
                  assign.NewValue = sender.ValueToString(field, obj1, description1.DataValue);
                  assign.OldValue = description2 == null || PXCache.IsOrigValueNewDate(sender, description2) ? obj2 : description2.DataValue;
                }
              }
              else
                assign.IsChanged = false;
              if (flag2 && string.Equals(field, sender._NoteIDName, StringComparison.OrdinalIgnoreCase))
              {
                if (assign.Value == null)
                {
                  assign.Value = (object) SequentialGuid.Generate();
                  sender.SetValue(row, sender._NoteIDOrdinal.Value, assign.Value);
                }
                PXDataFieldAssign pxDataFieldAssign = new PXDataFieldAssign(sender._NoteIDName, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), assign.Value);
                pxDataFieldAssign.Storage = StorageBehavior.KeyValueKey;
                array2[index2].Add((PXDataFieldParam) pxDataFieldAssign);
                flag2 = false;
              }
              sender._AdjustStorage(field, (PXDataFieldParam) assign);
              array2[index2].Add((PXDataFieldParam) assign);
              break;
            }
            break;
          }
        }
      }
    }
    for (int index3 = length; index3 < collection.Length; ++index3)
    {
      if (array2[index3] != null && array2[index3].Count > 0)
      {
        foreach (string str in array1[index3])
        {
          string field = str;
          object obj = sender.GetValue(row, field);
          if (baseTables.ContainsKey(collection[index3]))
          {
            int index4 = baseTables[collection[index3]];
            PXDataFieldParam pxDataFieldParam;
            if ((pxDataFieldParam = array2[index4].FirstOrDefault<PXDataFieldParam>((Func<PXDataFieldParam, bool>) (p => string.Equals(p.Column.Name, field, StringComparison.OrdinalIgnoreCase)))) != null)
              obj = pxDataFieldParam.Value;
          }
          PXCommandPreparingEventArgs.FieldDescription description;
          sender.RaiseCommandPreparing(field, row, obj, PXDBOperation.Update, collection[index3], out description);
          if (description?.Expr != null && !description.IsExcludedFromUpdate)
          {
            if (description.DataValue == null)
            {
              array2[index3] = (List<PXDataFieldParam>) null;
              break;
            }
            PXDataFieldRestrict dataFieldRestrict = new PXDataFieldRestrict((Column) description.Expr, description.DataType, description.DataLength, description.DataValue);
            array2[index3].Add((PXDataFieldParam) dataFieldRestrict);
          }
        }
      }
    }
    for (int index5 = 0; index5 < length; ++index5)
    {
      if (array2[index5] != null && array2[index5].Count > 0)
      {
        PXCache tc = sender.Graph.Caches[collection[index5]];
        if (tc._NoteIDName != null)
        {
          PXDataFieldParam pxDataFieldParam = array2[index5].FirstOrDefault<PXDataFieldParam>((Func<PXDataFieldParam, bool>) (p => string.Equals(p.Column.Name, tc._NoteIDName, StringComparison.OrdinalIgnoreCase)));
          if (pxDataFieldParam != null && pxDataFieldParam.Value != null && sender._KeyValueAttributeNames != null)
          {
            PXDataFieldAssign pxDataFieldAssign1 = new PXDataFieldAssign(tc._NoteIDName, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), pxDataFieldParam.Value);
            pxDataFieldAssign1.Storage = StorageBehavior.KeyValueKey;
            array2[index5].Add((PXDataFieldParam) pxDataFieldAssign1);
            object[] slot1 = sender.GetSlot<object[]>(row, sender._KeyValueAttributeSlotPosition);
            object[] slot2 = sender.GetSlot<object[]>(row, sender._KeyValueAttributeSlotPosition, true);
            if (slot1 != null)
            {
              foreach (KeyValuePair<string, int> valueAttributeName in sender._KeyValueAttributeNames)
              {
                if (valueAttributeName.Value < slot1.Length)
                {
                  PXDataFieldAssign pxDataFieldAssign2 = new PXDataFieldAssign(valueAttributeName.Key, sender._KeyValueAttributeTypes[valueAttributeName.Key] == StorageBehavior.KeyValueDate ? PXDbType.DateTime : (sender._KeyValueAttributeTypes[valueAttributeName.Key] == StorageBehavior.KeyValueNumeric ? PXDbType.Bit : PXDbType.NVarChar), slot1[valueAttributeName.Value]);
                  pxDataFieldAssign2.Storage = sender._KeyValueAttributeTypes[valueAttributeName.Key];
                  pxDataFieldAssign2.IsChanged = slot2 != null && valueAttributeName.Value < slot2.Length && !object.Equals(pxDataFieldAssign2.Value, slot2[valueAttributeName.Value]);
                  if (pxDataFieldAssign2.IsChanged)
                  {
                    pxDataFieldAssign2.NewValue = sender.AttributeValueToString(valueAttributeName.Key, pxDataFieldAssign2.Value);
                    pxDataFieldAssign2.OldValue = slot2[valueAttributeName.Value];
                  }
                  array2[index5].Add((PXDataFieldParam) pxDataFieldAssign2);
                }
              }
            }
          }
        }
      }
    }
    for (int index6 = 0; index6 < collection.Length; ++index6)
    {
      if (array2[index6] != null)
      {
        if (array2[index6].Count != 0)
        {
          bool flag3;
          try
          {
            array2[index6].Add((PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed);
            array2[index6].Add(unchanged == null ? (PXDataFieldParam) PXSelectOriginalsRestrict.SelectAllOriginalValues : (PXDataFieldParam) PXSelectOriginalsRestrict.SelectOriginalValues);
            flag3 = sender.Graph.ProviderUpdate(collection[index6], array2[index6].ToArray());
          }
          catch (PXDbOperationSwitchRequiredException ex)
          {
            List<PXDataFieldAssign> pxDataFieldAssignList = new List<PXDataFieldAssign>();
            foreach (string field in (List<string>) sender.Fields)
            {
              PXCommandPreparingEventArgs.FieldDescription description;
              sender.RaiseCommandPreparing(field, row, sender.GetValue(row, field), PXDBOperation.Insert, (System.Type) null, out description);
              if (description?.Expr != null && !description.IsExcludedFromUpdate && this.tableMeet(description, collection[index6], sqlDialect))
                pxDataFieldAssignList.Add(new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
            }
            sender.Graph.ProviderInsert(collection[index6], pxDataFieldAssignList.ToArray());
            flag3 = true;
          }
          if (!flag3)
          {
            if (index6 == 0 || index6 == index1 && collection[index1].IsAssignableFrom(sender.GetItemType()))
              throw PXDBInterceptorAttribute.GetLockViolationException(collection[index6], array2[index6].ToArray(), PXDBOperation.Update);
            List<PXDataFieldAssign> pxDataFieldAssignList = new List<PXDataFieldAssign>();
            foreach (string field in (List<string>) sender.Fields)
            {
              object val = sender.GetValue(row, field);
              PXCommandPreparingEventArgs.FieldDescription description;
              sender.RaiseCommandPreparing(field, row, val, PXDBOperation.Insert, (System.Type) null, out description);
              if (description?.Expr != null && !description.IsExcludedFromUpdate && this.tableMeet(description, collection[index6], sqlDialect) && pxDataFieldAssignList != null)
              {
                if (description.IsRestriction && description.DataValue == null)
                {
                  pxDataFieldAssignList = (List<PXDataFieldAssign>) null;
                }
                else
                {
                  int num = flag1 ? 1 : (PXDatabase.AuditRequired(collection[index6]) ? 1 : 0);
                  PXDataFieldAssign pxDataFieldAssign = new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue, (string) null);
                  if (num != 0 && val != null)
                  {
                    pxDataFieldAssign.IsChanged = true;
                    pxDataFieldAssign.NewValue = sender.ValueToString(field, val, description.DataValue);
                  }
                  else
                    pxDataFieldAssign.IsChanged = false;
                  pxDataFieldAssignList.Add(pxDataFieldAssign);
                }
              }
            }
            if (pxDataFieldAssignList != null && index6 >= length && array1[index6] != null)
            {
              foreach (string str in array1[index6])
              {
                string field = str;
                object obj = sender.GetValue(row, field);
                if (baseTables.ContainsKey(collection[index6]))
                {
                  int index7 = baseTables[collection[index6]];
                  PXDataFieldParam pxDataFieldParam;
                  if ((pxDataFieldParam = array2[index7].FirstOrDefault<PXDataFieldParam>((Func<PXDataFieldParam, bool>) (p => string.Equals(p.Column.Name, field, StringComparison.OrdinalIgnoreCase)))) != null)
                    obj = pxDataFieldParam.Value;
                }
                PXCommandPreparingEventArgs.FieldDescription description;
                sender.RaiseCommandPreparing(field, row, obj, PXDBOperation.Update, collection[index6], out description);
                if (description?.Expr != null && !description.IsExcludedFromUpdate)
                {
                  if (description.DataValue == null)
                  {
                    pxDataFieldAssignList = (List<PXDataFieldAssign>) null;
                    break;
                  }
                  PXDataFieldAssign pxDataFieldAssign = new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue);
                  pxDataFieldAssignList.Add(pxDataFieldAssign);
                }
              }
            }
            if (pxDataFieldAssignList != null)
              sender.Graph.ProviderInsert(collection[index6], pxDataFieldAssignList.ToArray());
          }
        }
      }
    }
    try
    {
      sender.RaiseRowPersisted(row, PXDBOperation.Update, PXTranStatus.Open, (Exception) null);
      BqlTablePair bqlTablePair;
      if (sender._Originals.TryGetValue((IBqlTable) row, out bqlTablePair))
      {
        if (sender._OriginalsRemoved == null)
          sender._OriginalsRemoved = new PXCacheRemovedOriginalsCollection();
        sender._OriginalsRemoved[(IBqlTable) row] = bqlTablePair;
      }
      sender._Originals.Remove((IBqlTable) row);
    }
    catch (PXRowPersistedException ex)
    {
      sender.RaiseExceptionHandling(ex.Name, row, ex.Value, (Exception) ex);
      throw;
    }
    return true;
  }

  /// <exclude />
  public override bool PersistDeleted(PXCache sender, object row)
  {
    ISqlDialect sqlDialect = sender.Graph.SqlDialect;
    if (!this._persistent)
      return base.PersistDeleted(sender, row);
    System.Type[] collection = this.GetTables();
    string[][] array1 = new string[collection.Length][];
    int?[] array2 = new int?[collection.Length];
    List<PXDataFieldRestrict>[] array3 = new List<PXDataFieldRestrict>[collection.Length];
    for (int index = 0; index < collection.Length; ++index)
      array3[index] = new List<PXDataFieldRestrict>();
    int length = collection.Length;
    for (int index1 = 0; index1 < length; ++index1)
    {
      System.Type type = collection[index1];
      PXCache cach = sender.Graph.Caches[collection[index1]];
      collection[index1] = cach.BqlTable;
      List<System.Type> extensionTables = cach.GetExtensionTables();
      if (extensionTables != null)
      {
        int count = extensionTables.Count;
        List<System.Type> typeList = new List<System.Type>((IEnumerable<System.Type>) extensionTables);
        typeList.InsertRange(0, (IEnumerable<System.Type>) collection);
        collection = typeList.ToArray();
        Array.Resize<string[]>(ref array1, typeList.Count);
        Array.Resize<int?>(ref array2, typeList.Count);
        Array.Resize<List<PXDataFieldRestrict>>(ref array3, typeList.Count);
        if (cach.BqlSelect is IPXExtensibleTableAttribute)
        {
          for (int index2 = array3.Length - count; index2 < array3.Length; ++index2)
          {
            array3[index2] = new List<PXDataFieldRestrict>();
            array1[index2] = ((IPXExtensibleTableAttribute) cach.BqlSelect).Keys;
            array2[index2] = new int?(index1);
          }
        }
        else
        {
          for (int index3 = array3.Length - count; index3 < array3.Length; ++index3)
          {
            array3[index3] = new List<PXDataFieldRestrict>();
            array1[index3] = cach.Keys.ToArray();
          }
        }
      }
    }
    object original = sender.GetOriginal(row);
    bool flag = sender._HasKeyValueStored();
    int num = -1;
    foreach (string field1 in (List<string>) sender.Fields)
    {
      string field = field1;
      PXCommandPreparingEventArgs.FieldDescription description1;
      sender.RaiseCommandPreparing(field, row, sender.GetValue(row, field), PXDBOperation.Delete, (System.Type) null, out description1);
      if (description1 != null && description1.Expr != null && !description1.IsExcludedFromDelete)
      {
        if (num >= 0 && description1.DataType == PXDbType.Timestamp && sender.Graph._primaryRecordTimeStamp != null && sender.Graph.PrimaryItemType != (System.Type) null && sender.Graph.Caches[sender.Graph.PrimaryItemType] == sender)
        {
          byte[] primaryRecordTimeStamp = sender.Graph._primaryRecordTimeStamp;
          sender.Graph._primaryRecordTimeStamp = (byte[]) null;
          try
          {
            sender.RaiseCommandPreparing(field, row, sender.GetValue(row, field), PXDBOperation.Delete, (System.Type) null, out description1);
          }
          finally
          {
            sender.Graph._primaryRecordTimeStamp = primaryRecordTimeStamp;
          }
        }
        for (int index = 0; index < collection.Length; ++index)
        {
          if (this.tableMeet(description1, collection[index], sqlDialect))
          {
            if (description1.IsRestriction)
            {
              if (array3[index] != null)
              {
                if (index > 0 && description1.DataValue == null)
                  array3[index] = (List<PXDataFieldRestrict>) null;
                else
                  array3[index].Add(new PXDataFieldRestrict((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue));
              }
              if (description1.DataType == PXDbType.Timestamp)
              {
                num = index;
                break;
              }
              break;
            }
            object obj = MaybeObjects.With<object, object>(original, (Func<object, object>) (c => sender.GetValue(c, field)));
            PXCommandPreparingEventArgs.FieldDescription description2 = (PXCommandPreparingEventArgs.FieldDescription) null;
            if (obj != null)
              sender.RaiseCommandPreparing(field, original, obj, PXDBOperation.Update, (System.Type) null, out description2);
            if (array3[index] != null)
            {
              PXDataFieldRestrict assign = sender.IsKvExtField(field) ? new PXDataFieldRestrict((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue) : (PXDataFieldRestrict) new PXDummyDataFieldRestrict((Column) description1.Expr, description1.DataType, description1.DataLength, original != null ? description2?.DataValue ?? obj : description1.DataValue);
              if (flag && string.Equals(field, sender._NoteIDName, StringComparison.OrdinalIgnoreCase))
              {
                if (assign.Value == null)
                {
                  assign.Value = (object) SequentialGuid.Generate();
                  sender.SetValue(row, sender._NoteIDOrdinal.Value, assign.Value);
                }
                PXDataFieldRestrict dataFieldRestrict = new PXDataFieldRestrict(sender._NoteIDName, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), assign.Value);
                dataFieldRestrict.Storage = StorageBehavior.KeyValueKey;
                array3[index].Add(dataFieldRestrict);
                flag = false;
              }
              sender._AdjustStorage(field, (PXDataFieldParam) assign);
              array3[index].Add(assign);
              break;
            }
            break;
          }
        }
      }
    }
    for (int index = length; index < collection.Length; ++index)
    {
      if (array3[index] != null && (!array2[index].HasValue || array3[array2[index].Value] != null && array3[array2[index].Value].Count<PXDataFieldRestrict>((Func<PXDataFieldRestrict, bool>) (_ => !(_ is PXDummyDataFieldRestrict))) > 0))
      {
        foreach (string str in array1[index])
        {
          object obj = sender.GetValue(row, str);
          PXCommandPreparingEventArgs.FieldDescription description;
          sender.RaiseCommandPreparing(str, row, obj, PXDBOperation.Update, collection[index], out description);
          if (description?.Expr != null && !description.IsExcludedFromUpdate)
          {
            if (description.DataValue == null)
            {
              array3[index] = (List<PXDataFieldRestrict>) null;
              break;
            }
            PXDataFieldRestrict dataFieldRestrict = new PXDataFieldRestrict((Column) description.Expr, description.DataType, description.DataLength, description.DataValue);
            array3[index].Add(dataFieldRestrict);
          }
        }
      }
    }
    for (int index = collection.Length - 1; index >= 0; --index)
    {
      if (array3[index] != null && array3[index].Count<PXDataFieldRestrict>((Func<PXDataFieldRestrict, bool>) (_ => !(_ is PXDummyDataFieldRestrict))) != 0)
      {
        array3[index].Add(original == null ? PXSelectOriginalsRestrict.SelectAllOriginalValues : PXSelectOriginalsRestrict.SelectOriginalValues);
        try
        {
          if (!sender.Graph.ProviderDelete(collection[index], array3[index].ToArray()))
          {
            if (index == 0)
              throw PXDBInterceptorAttribute.GetLockViolationException(collection[index], (PXDataFieldParam[]) array3[index].ToArray(), PXDBOperation.Delete);
          }
        }
        catch (PXDbOperationSwitchRequiredException ex)
        {
          List<PXDataFieldAssign> pxDataFieldAssignList = new List<PXDataFieldAssign>();
          foreach (string field in (List<string>) sender.Fields)
          {
            PXCommandPreparingEventArgs.FieldDescription description;
            sender.RaiseCommandPreparing(field, row, sender.GetValue(row, field), PXDBOperation.Insert, (System.Type) null, out description);
            if (description?.Expr != null && !description.IsExcludedFromUpdate && this.tableMeet(description, collection[index], sqlDialect))
              pxDataFieldAssignList.Add(new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
          }
          sender.Graph.ProviderInsert(collection[index], pxDataFieldAssignList.ToArray());
        }
      }
    }
    try
    {
      sender.RaiseRowPersisted(row, PXDBOperation.Delete, PXTranStatus.Open, (Exception) null);
    }
    catch (PXRowPersistedException ex)
    {
      sender.RaiseExceptionHandling(ex.Name, row, ex.Value, (Exception) ex);
      throw;
    }
    return true;
  }

  protected class BqlRowSelection : BqlCommand, IPXExtensibleTableAttribute
  {
    protected BqlCommand _Command;
    protected string[] _Links;
    protected System.Type _Alias;
    protected PXDBInterceptorAttribute _Child;

    string[] IPXExtensibleTableAttribute.Keys => this._Links;

    bool IPXExtensibleTableAttribute.IsSingleTableExtension { get; }

    public BqlRowSelection(PXCache cache, PXDBInterceptorAttribute child, BqlCommand command)
    {
      this._Child = child;
      this._Command = command;
      this._Alias = cache.GetItemType();
      BqlCommand tableCommand = this._Child.GetTableCommand(cache);
      if (tableCommand is IPXExtensibleTableAttribute)
        this._Links = ((IPXExtensibleTableAttribute) tableCommand).Keys;
      if (this._Links != null)
        return;
      this._Links = cache.Keys.ToArray();
    }

    public override Query GetQueryInternal(
      PXGraph graph,
      BqlCommandInfo info,
      BqlCommand.Selection selection)
    {
      Query query = this.CreateQuery(graph);
      if (graph == null || this._Child == null)
      {
        if (!info.IsEmpty)
          this._Command.GetQueryInternal(graph, info, selection);
        return query;
      }
      PXCache cach1 = graph.Caches[this._Alias];
      BqlCommand bqlSelect = cach1.BqlSelect;
      cach1.BqlSelect = this._Child.GetTableCommand(cach1);
      List<PXCache> pxCacheList = new List<PXCache>();
      if (!(this._Child is PXProjectionAttribute))
      {
        cach1.BypassCalced = true;
        System.Type type = cach1.GetItemType();
        while ((type = type.BaseType) != typeof (object) && typeof (IBqlTable).IsAssignableFrom(type))
        {
          PXCache cach2 = graph.Caches[type];
          if (!pxCacheList.Contains(cach2))
          {
            pxCacheList.Add(cach2);
            cach2.BypassCalced = true;
          }
        }
      }
      Query queryInternal = this._Command.GetQueryInternal(graph, info, selection);
      cach1.BqlSelect = bqlSelect;
      cach1.BypassCalced = false;
      foreach (PXCache pxCache in pxCacheList)
        pxCache.BypassCalced = false;
      return queryInternal;
    }

    public override void Verify(
      PXCache cache,
      object item,
      List<object> pars,
      ref bool? result,
      ref object value)
    {
      this.Verify(cache, item, pars, ref result, ref value);
    }

    public override BqlCommand OrderByNew<newOrderBy>()
    {
      throw new PXException("The method or operation is not implemented.");
    }

    public override BqlCommand OrderByNew(System.Type newOrderBy)
    {
      throw new PXException("The method or operation is not implemented.");
    }

    public override BqlCommand WhereAnd<where>()
    {
      throw new PXException("The method or operation is not implemented.");
    }

    public override BqlCommand WhereAnd(System.Type where)
    {
      throw new PXException("The method or operation is not implemented.");
    }

    public override BqlCommand WhereNew<newWhere>()
    {
      throw new PXException("The method or operation is not implemented.");
    }

    public override BqlCommand WhereNew(System.Type newWhere)
    {
      throw new PXException("The method or operation is not implemented.");
    }

    public override BqlCommand WhereNot()
    {
      throw new PXException("The method or operation is not implemented.");
    }

    public override BqlCommand WhereOr<where>()
    {
      throw new PXException("The method or operation is not implemented.");
    }

    public override BqlCommand WhereOr(System.Type where)
    {
      throw new PXException("The method or operation is not implemented.");
    }
  }
}

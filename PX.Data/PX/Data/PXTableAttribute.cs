// Decompiled with JetBrains decompiler
// Type: PX.Data.PXTableAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data.DeletedRecordsTracking;
using PX.Data.PushNotifications;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Monads;

#nullable disable
namespace PX.Data;

/// <summary>Binds a DAC that derives from another DAC to the table that has
/// the name of the derived DAC. Without the attribute, the derived DAC
/// will be bound to the same table as the DAC that starts the inheritance
/// hierarchy.</summary>
/// <remarks>
/// <para>The attribute is placed on the declaration of a DAC.</para>
/// <para>The attribute can be used in customization projects. You place it on the
/// declaration of a DAC extension to indicate that the extension fields
/// are bound to a separate table.</para>
/// </remarks>
/// <example>
/// <para>The <tt>PXTable</tt> attribute below indicates that the <tt>APInvoice</tt> DAC is bound to the <tt>APInvoice</tt> table.
/// Without the attribute, it would be bound to the <tt>APRegister</tt> table.</para>
/// <code lang="CS">
/// [PXTable()]
/// public partial class APInvoice : APRegister, IInvoice
/// {
///     ...
/// }
/// </code>
/// </example>
/// <example>
/// <para>The <tt>PXTable</tt> attribute below indicates that the <tt>RSSVLocation</tt> extension
/// of the <tt>Location</tt> DAC is bound to a separate table and the <tt>Location</tt> DAC
/// can include data records that do not have the corresponding data record
/// in the extension table.</para>
/// <para>In the <tt>PXTable</tt> attribute, you specify the key fields because the <tt>Location</tt> DAC
/// includes a surrogate-natural pair of key fields, <tt>LocationID</tt> (which is the
/// database key as well) and <tt>LocationCD</tt> (human-readable value). In the
/// <tt>PXTable</tt> attribute, you need to exclude the
/// <tt>LocationCD</tt> field and specify all other key fields of the <tt>Location</tt> DAC.</para>
/// <code>
/// [PXTable(typeof(Location.bAccountID),
///          typeof(Location.locationID),
///          IsOptional = true)]
/// public class RSSVLocation : PXCacheExtension&lt;Location&gt;
/// {
///     ...
/// }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class)]
public class PXTableAttribute : PXDBInterceptorAttribute
{
  protected BqlCommand rowselection;
  protected BqlCommand tableselection;
  protected BqlCommand rowSelectionByNoteId;
  protected bool canSelectByNoteId;
  protected List<string> keys;
  protected System.Type[] _bypassOnDelete;

  /// <summary>The value that indicates whether a record of the base DAC
  /// can exist without a record of the extension DAC. This
  /// situation corresponds to the use of the attribute on the extension DAC
  /// that is bound to a separate database table. By default, the value is
  /// <see langword="false" />, and a record in the extension table is always
  /// created for a record of the base table.</summary>
  public bool IsOptional { get; set; }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    this.rowselection = (BqlCommand) new PXTableAttribute.BqlRowSelection(sender, true, this.keys, (PXDBInterceptorAttribute) this);
    this.tableselection = (BqlCommand) new PXTableAttribute.BqlRowSelection(sender, false, this.keys, (PXDBInterceptorAttribute) this);
    if (sender.GetBqlField("noteid") != (System.Type) null)
    {
      this.rowSelectionByNoteId = (BqlCommand) new PXTableAttribute.BqlRowSelection(sender, true, this.keys, (PXDBInterceptorAttribute) this)
      {
        ByNoteId = true
      };
      this.canSelectByNoteId = true;
    }
    if (this.keys == null || this.keys.Count != 1 || sender.Keys.Count != 1)
      return;
    sender.CommandPreparingEvents[sender.Keys[0].ToLower()] += new PXCommandPreparing(this.KeysCommandPreparing);
  }

  /// <exclude />
  public override BqlCommand GetRowCommand() => this.rowselection;

  /// <exclude />
  public override BqlCommand GetRowByNoteIdCommand() => this.rowSelectionByNoteId;

  /// <exclude />
  public override BqlCommand GetTableCommand() => this.tableselection;

  public override bool CanSelectByNoteId => this.canSelectByNoteId;

  internal override BqlCommand GetTableCommand(PXCache sender)
  {
    if (this.tableselection == null)
    {
      this.rowselection = (BqlCommand) new PXTableAttribute.BqlRowSelection(sender, true, this.keys, (PXDBInterceptorAttribute) this);
      this.tableselection = (BqlCommand) new PXTableAttribute.BqlRowSelection(sender, false, this.keys, (PXDBInterceptorAttribute) this);
    }
    return this.tableselection;
  }

  /// <exclude />
  public virtual void KeysCommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Update && (e.Operation & PXDBOperation.Delete) != PXDBOperation.Delete || !(e.Table != (System.Type) null) || e.Row == null)
      return;
    System.Type[] tables = this.GetTables();
    if (!(tables[tables.Length - 1] != e.Table) || !object.Equals(sender.GetValue(e.Row, sender.Keys[0]), e.Value))
      return;
    PXCommandPreparingEventArgs.FieldDescription description;
    sender.RaiseCommandPreparing(this.keys[0], e.Row, sender.GetValue(e.Row, this.keys[0]), PXDBOperation.Select, e.Table, out description);
    if (description == null || description.Expr == null)
      return;
    e.BqlTable = description.BqlTable;
    e.Expr = description.Expr;
    e.DataType = description.DataType;
    e.DataLength = description.DataLength;
    e.DataValue = description.DataValue;
    e.IsRestriction = true;
    e.Cancel = true;
  }

  /// <summary>Initializes a new instance of the attribute.</summary>
  public PXTableAttribute()
  {
  }

  /// <summary>Initializes a new instance of the attribute when the base DAC
  /// has a pair of surrogate and natural keys. In this case, in the
  /// parameters, you should specify all key fields of the base DAC. However, from
  /// the pair of the surrogate and natural keys, you include only the
  /// surrogate key.</summary>
  /// <param name="links">The list of key fields of the base DAC.</param>
  /// <example>
  /// <para>The <tt>PXTable</tt> attribute below indicates that the <tt>RSSVLocation</tt> extension
  /// of the <tt>Location</tt> DAC is bound to a separate table and the <tt>Location</tt> DAC
  /// can include data records that do not have the corresponding data record
  /// in the extension table.</para>
  /// <para>In the <tt>PXTable</tt> attribute, you specify the key fields because the <tt>Location</tt> DAC
  /// includes a surrogate-natural pair of key fields, <tt>LocationID</tt> (which is the
  /// database key as well) and <tt>LocationCD</tt> (human-readable value). In the
  /// <tt>PXTable</tt> attribute, you need to exclude the
  /// <tt>LocationCD</tt> field and specify all other key fields of the <tt>Location</tt> DAC.</para>
  /// <code>
  /// [PXTable(typeof(Location.bAccountID),
  ///          typeof(Location.locationID),
  ///          IsOptional = true)]
  /// public class RSSVLocation : PXCacheExtension&lt;Location&gt;
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  public PXTableAttribute(params System.Type[] links)
    : this()
  {
    this.keys = new List<string>();
    foreach (System.Type link in links)
      this.keys.Add(char.ToUpper(link.Name[0]).ToString() + link.Name.Substring(1));
  }

  /// <param name="tables">Tables that should be bypassed on delete operation.</param>
  /// <exclude />
  public void BypassOnDelete(params System.Type[] tables) => this._bypassOnDelete = tables;

  internal override List<string> Keys => this.keys;

  /// <exclude />
  public override bool PersistInserted(PXCache sender, object row)
  {
    ISqlDialect sqlDialect = sender.Graph.SqlDialect;
    System.Type[] typeArray = this.GetTables();
    List<System.Type> extensionTables = sender.GetExtensionTables();
    if (extensionTables != null)
    {
      List<System.Type> typeList = new List<System.Type>((IEnumerable<System.Type>) extensionTables);
      typeList.AddRange((IEnumerable<System.Type>) typeArray);
      typeArray = typeList.ToArray();
    }
    if (this.keys == null)
      this.keys = new List<string>((IEnumerable<string>) sender.Keys);
    List<PXDataFieldAssign>[] pars = new List<PXDataFieldAssign>[typeArray.Length];
    for (int index = 0; index < typeArray.Length; ++index)
      pars[index] = new List<PXDataFieldAssign>();
    System.Type table = sender.BqlTable;
    bool audit;
    while (!(audit = PXDatabase.AuditRequired(table)) && table.BaseType != typeof (object))
      table = table.BaseType;
    this.PrepareParametersForInsert(sender, row, typeArray, sqlDialect, audit, pars);
    try
    {
      pars[typeArray.Length - 1].Add(PXDataFieldAssign.OperationSwitchAllowed);
      sender.Graph.ProviderInsert(typeArray[typeArray.Length - 1], pars[typeArray.Length - 1].ToArray());
    }
    catch (PXDbOperationSwitchRequiredException ex)
    {
      List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>();
      foreach (string field in (List<string>) sender.Fields)
      {
        PXCommandPreparingEventArgs.FieldDescription description;
        sender.RaiseCommandPreparing(field, row, sender.GetValue(row, field), PXDBOperation.Update | PXDBOperation.Second, (System.Type) null, out description);
        if (description?.Expr != null && !description.IsExcludedFromUpdate && this.tableMeet(description, typeArray[typeArray.Length - 1], sqlDialect))
        {
          if (description.IsRestriction)
            pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
          else
            pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
        }
      }
      IEnumerable<PXDataFieldAssign> collection = pars[typeArray.Length - 1].Where<PXDataFieldAssign>((Func<PXDataFieldAssign, bool>) (p => p.Storage != 0));
      pxDataFieldParamList.AddRange((IEnumerable<PXDataFieldParam>) collection);
      sender.Graph.ProviderUpdate(typeArray[typeArray.Length - 1], pxDataFieldParamList.ToArray());
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
    for (int index = 0; index < typeArray.Length - 1; ++index)
    {
      foreach (string key in this.keys)
      {
        object val = sender.GetValue(row, key);
        PXCommandPreparingEventArgs.FieldDescription description;
        sender.RaiseCommandPreparing(key, row, val, PXDBOperation.Insert, typeArray[index], out description);
        if (description == null || description.Expr == null)
          sender.RaiseCommandPreparing(key, row, sender.GetValue(row, key), PXDBOperation.Select, typeArray[index], out description);
        if (description?.Expr != null && !description.IsExcludedFromUpdate)
        {
          PXDataFieldAssign pxDataFieldAssign = new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue, (string) null);
          if (audit && val != null)
          {
            pxDataFieldAssign.IsChanged = true;
            pxDataFieldAssign.NewValue = sender.ValueToString(key, val, description.DataValue);
          }
          else
            pxDataFieldAssign.IsChanged = false;
          pars[index].Add(pxDataFieldAssign);
        }
      }
    }
    for (int index = typeArray.Length - 2; index >= 0; --index)
    {
      try
      {
        pars[index].Add(PXDataFieldAssign.OperationSwitchAllowed);
        sender.Graph.ProviderInsert(typeArray[index], pars[index].ToArray());
      }
      catch (PXDbOperationSwitchRequiredException ex)
      {
        List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>();
        foreach (string field in (List<string>) sender.Fields)
        {
          PXCommandPreparingEventArgs.FieldDescription description;
          sender.RaiseCommandPreparing(field, row, sender.GetValue(row, field), PXDBOperation.Update | PXDBOperation.Second, (System.Type) null, out description);
          if (description?.Expr != null && !description.IsExcludedFromUpdate && this.tableMeet(description, typeArray[index], sqlDialect))
          {
            if (description.IsRestriction)
              pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
            else
              pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
          }
        }
        foreach (string key in this.keys)
        {
          PXCommandPreparingEventArgs.FieldDescription description;
          sender.RaiseCommandPreparing(key, row, sender.GetValue(row, key), PXDBOperation.Update, typeArray[index], out description);
          if (description == null || description.Expr == null)
            sender.RaiseCommandPreparing(key, row, sender.GetValue(row, key), PXDBOperation.Select, typeArray[index], out description);
          if (description != null && description.Expr != null)
            pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
        }
        sender.Graph.ProviderUpdate(typeArray[index], pxDataFieldParamList.ToArray());
      }
    }
    return true;
  }

  private void PrepareParametersForInsert(
    PXCache sender,
    object row,
    System.Type[] tables,
    ISqlDialect dialect,
    bool audit,
    List<PXDataFieldAssign>[] pars)
  {
    bool flag = sender._HasKeyValueStored();
    foreach (string field in (List<string>) sender.Fields)
    {
      object val = sender.GetValue(row, field);
      PXCommandPreparingEventArgs.FieldDescription description;
      sender.RaiseCommandPreparing(field, row, val, PXDBOperation.Insert, (System.Type) null, out description);
      if (description?.Expr != null && !description.IsExcludedFromUpdate)
      {
        for (int index = 0; index < tables.Length; ++index)
        {
          if (this.tableMeet(description, tables[index], dialect))
          {
            PXDataFieldAssign assign = new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue, (string) null);
            if (audit && val != null)
            {
              assign.IsChanged = true;
              assign.NewValue = sender.ValueToString(field, val, description.DataValue);
            }
            else
              assign.IsChanged = false;
            if (flag && string.Equals(field, sender._NoteIDName, StringComparison.OrdinalIgnoreCase))
            {
              if (assign.Value == null)
              {
                assign.Value = (object) SequentialGuid.Generate();
                sender.SetValue(row, sender._NoteIDOrdinal.Value, assign.Value);
              }
              PXDataFieldAssign pxDataFieldAssign1 = new PXDataFieldAssign(sender._NoteIDName, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), assign.Value);
              pxDataFieldAssign1.Storage = StorageBehavior.KeyValueKey;
              pars[index].Add(pxDataFieldAssign1);
              flag = false;
              if (sender._KeyValueAttributeNames != null)
              {
                object[] slot = sender.GetSlot<object[]>(row, sender._KeyValueAttributeSlotPosition);
                if (slot != null)
                {
                  foreach (KeyValuePair<string, int> valueAttributeName in sender._KeyValueAttributeNames)
                  {
                    if (valueAttributeName.Value < slot.Length)
                    {
                      PXDataFieldAssign pxDataFieldAssign2 = new PXDataFieldAssign(valueAttributeName.Key, sender._KeyValueAttributeTypes[valueAttributeName.Key] == StorageBehavior.KeyValueDate ? PXDbType.DateTime : (sender._KeyValueAttributeTypes[valueAttributeName.Key] == StorageBehavior.KeyValueNumeric ? PXDbType.Bit : PXDbType.NVarChar), slot[valueAttributeName.Value]);
                      pxDataFieldAssign2.Storage = sender._KeyValueAttributeTypes[valueAttributeName.Key];
                      if (pxDataFieldAssign2.IsChanged = audit && pxDataFieldAssign2.Value != null)
                        pxDataFieldAssign2.NewValue = sender.AttributeValueToString(valueAttributeName.Key, pxDataFieldAssign2.Value);
                      pars[index].Add(pxDataFieldAssign2);
                    }
                  }
                }
              }
            }
            sender._AdjustStorage(field, (PXDataFieldParam) assign);
            pars[index].Add(assign);
            break;
          }
        }
      }
    }
  }

  /// <exclude />
  public override bool PersistUpdated(PXCache sender, object row)
  {
    ISqlDialect sqlDialect = sender.Graph.SqlDialect;
    System.Type[] typeArray = this.GetTables();
    List<System.Type> extensionTables = sender.GetExtensionTables();
    if (extensionTables != null)
    {
      List<System.Type> typeList = new List<System.Type>((IEnumerable<System.Type>) extensionTables);
      typeList.AddRange((IEnumerable<System.Type>) typeArray);
      typeArray = typeList.ToArray();
    }
    if (this.keys == null)
      this.keys = new List<string>((IEnumerable<string>) sender.Keys);
    object original = sender.GetOriginal(row);
    System.Type table = sender.BqlTable;
    bool flag1;
    while (!(flag1 = PXDatabase.AuditRequired(table)) && table.BaseType != typeof (object))
      table = table.BaseType;
    List<PXDataFieldParam>[] pars = new List<PXDataFieldParam>[typeArray.Length];
    for (int index = 0; index < typeArray.Length; ++index)
      pars[index] = new List<PXDataFieldParam>();
    this.PrepareParametersForUpdate(sender, row, typeArray, sqlDialect, original, pars);
    for (int index = 0; index < typeArray.Length - 1; ++index)
    {
      foreach (string key in this.keys)
      {
        object obj1 = sender.GetValue(row, key);
        PXCommandPreparingEventArgs.FieldDescription description1;
        sender.RaiseCommandPreparing(key, row, obj1, PXDBOperation.Update, typeArray[index], out description1);
        if (description1 == null || description1.Expr == null)
          sender.RaiseCommandPreparing(key, row, sender.GetValue(row, key), PXDBOperation.Select, typeArray[index], out description1);
        if (description1 != null && description1.Expr != null)
        {
          object obj2;
          if (description1.IsRestriction && original != null && description1.DataType != PXDbType.Timestamp && sender.Keys.Contains(key) && !object.Equals(obj2 = sender.GetValue(original, key), obj1) && obj2 != null)
          {
            PXCommandPreparingEventArgs.FieldDescription description2;
            sender.RaiseCommandPreparing(key, original, obj2, PXDBOperation.Select, typeArray[index], out description2);
            if (description2 != null && description2.Expr != null)
            {
              PXDataFieldAssign pxDataFieldAssign = new PXDataFieldAssign((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue, sender.ValueToString(key, obj1, description1.DataValue));
              pars[index].Add((PXDataFieldParam) pxDataFieldAssign);
              pars[index].Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description2.Expr, description2.DataType, description2.DataLength, description2.DataValue));
            }
            else
              pars[index].Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue));
          }
          else
            pars[index].Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue));
        }
      }
    }
    for (int index = typeArray.Length - 1; index >= 0; --index)
    {
      bool flag2;
      try
      {
        pars[index].Add((PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed);
        if (index < typeArray.Length - 1)
        {
          companySetting settings;
          PXDatabase.Provider.getCompanyID(typeArray[index].Name, out settings);
          if (settings != null && settings.Deleted != null)
            pars[index].Add((PXDataFieldParam) new PXDataFieldRestrict(settings.Deleted, PXDbType.Bit, new int?(1), (object) false));
        }
        if (original == null)
          pars[index].Add((PXDataFieldParam) PXSelectOriginalsRestrict.SelectAllOriginalValues);
        flag2 = sender.Graph.ProviderUpdate(typeArray[index], pars[index].ToArray());
      }
      catch (PXDbOperationSwitchRequiredException ex)
      {
        List<PXDataFieldAssign> pxDataFieldAssignList = new List<PXDataFieldAssign>();
        foreach (string field in (List<string>) sender.Fields)
        {
          PXCommandPreparingEventArgs.FieldDescription description;
          sender.RaiseCommandPreparing(field, row, sender.GetValue(row, field), PXDBOperation.Insert, (System.Type) null, out description);
          if (description?.Expr != null && !description.IsExcludedFromUpdate && this.tableMeet(description, typeArray[index], sqlDialect))
            pxDataFieldAssignList.Add(new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
        }
        if (index < typeArray.Length - 1)
        {
          foreach (string key in this.keys)
          {
            PXCommandPreparingEventArgs.FieldDescription description;
            sender.RaiseCommandPreparing(key, row, sender.GetValue(row, key), PXDBOperation.Update, typeArray[index], out description);
            if (description?.Expr == null || description.IsExcludedFromUpdate)
              sender.RaiseCommandPreparing(key, row, sender.GetValue(row, key), PXDBOperation.Select, typeArray[index], out description);
            if (description != null && description.Expr != null)
              pxDataFieldAssignList.Add(new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
          }
        }
        sender.Graph.ProviderInsert(typeArray[index], pxDataFieldAssignList.ToArray());
        flag2 = true;
      }
      if (!flag2)
      {
        if (index == typeArray.Length - 1)
          throw PXDBInterceptorAttribute.GetLockViolationException(typeArray[index], pars[index].ToArray(), PXDBOperation.Update);
        List<PXDataFieldAssign> pxDataFieldAssignList = new List<PXDataFieldAssign>();
        foreach (string field in (List<string>) sender.Fields)
        {
          object val = sender.GetValue(row, field);
          PXCommandPreparingEventArgs.FieldDescription description;
          sender.RaiseCommandPreparing(field, row, val, PXDBOperation.Insert, (System.Type) null, out description);
          if (description?.Expr != null && !description.IsExcludedFromUpdate && this.tableMeet(description, typeArray[index], sqlDialect))
          {
            PXDataFieldAssign pxDataFieldAssign = new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue, (string) null);
            if (flag1 && val != null)
            {
              pxDataFieldAssign.IsChanged = true;
              pxDataFieldAssign.NewValue = sender.ValueToString(field, val, description.DataValue);
            }
            else
              pxDataFieldAssign.IsChanged = false;
            pxDataFieldAssignList.Add(pxDataFieldAssign);
          }
        }
        foreach (string key in this.keys)
        {
          PXCommandPreparingEventArgs.FieldDescription description;
          sender.RaiseCommandPreparing(key, row, sender.GetValue(row, key), PXDBOperation.Update, typeArray[index], out description);
          if (description == null || description.Expr == null)
            sender.RaiseCommandPreparing(key, row, sender.GetValue(row, key), PXDBOperation.Select, typeArray[index], out description);
          if (description != null && description.Expr != null)
            pxDataFieldAssignList.Add(new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
        }
        try
        {
          pxDataFieldAssignList.Add(PXDataFieldAssign.OperationSwitchAllowed);
          sender.Graph.ProviderInsert(typeArray[index], pxDataFieldAssignList.ToArray());
        }
        catch (PXDbOperationSwitchRequiredException ex)
        {
          List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>();
          foreach (string field in (List<string>) sender.Fields)
          {
            PXCommandPreparingEventArgs.FieldDescription description;
            sender.RaiseCommandPreparing(field, row, sender.GetValue(row, field), PXDBOperation.Update | PXDBOperation.Second, (System.Type) null, out description);
            if (description != null && description.Expr != null && this.tableMeet(description, typeArray[index], sqlDialect))
            {
              if (description.IsRestriction)
                pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
              else if (!description.IsExcludedFromUpdate)
                pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
            }
          }
          foreach (string key in this.keys)
          {
            PXCommandPreparingEventArgs.FieldDescription description;
            sender.RaiseCommandPreparing(key, row, sender.GetValue(row, key), PXDBOperation.Update, typeArray[index], out description);
            if (description == null || description.Expr == null)
              sender.RaiseCommandPreparing(key, row, sender.GetValue(row, key), PXDBOperation.Select, typeArray[index], out description);
            if (description != null && description.Expr != null)
              pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
          }
          sender.Graph.ProviderUpdate(typeArray[index], pxDataFieldParamList.ToArray());
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

  private void PrepareParametersForUpdate(
    PXCache sender,
    object row,
    System.Type[] tables,
    ISqlDialect dialect,
    object unchanged,
    List<PXDataFieldParam>[] pars)
  {
    KeysVerifyer keysVerifyer = new KeysVerifyer(sender);
    bool flag = sender._HasKeyValueStored();
    foreach (string field1 in (List<string>) sender.Fields)
    {
      string field = field1;
      object obj1 = sender.GetValue(row, field);
      PXCommandPreparingEventArgs.FieldDescription description1;
      sender.RaiseCommandPreparing(field, row, obj1, PXDBOperation.Update, (System.Type) null, out description1);
      if (description1 != null)
      {
        if (description1.Expr == null)
        {
          keysVerifyer.ExcludeField(field);
        }
        else
        {
          for (int index = 0; index < tables.Length; ++index)
          {
            if (this.tableMeet(description1, tables[index], dialect))
            {
              object obj2 = MaybeObjects.With<object, object>(unchanged, (Func<object, object>) (c => sender.GetValue(unchanged, field)));
              PXCommandPreparingEventArgs.FieldDescription description2 = (PXCommandPreparingEventArgs.FieldDescription) null;
              if (obj2 != null)
                sender.RaiseCommandPreparing(field, unchanged, obj2, PXDBOperation.Update, (System.Type) null, out description2);
              if (description1.IsRestriction)
              {
                if (obj2 != null && description1.DataType != PXDbType.Timestamp && sender.Keys.Contains(field) && !object.Equals(obj2, obj1))
                {
                  if (description2 != null && !string.IsNullOrEmpty(description2.Expr.SQLQuery(sender.Graph.SqlDialect.GetConnection()).ToString()))
                  {
                    PXDataFieldAssign pxDataFieldAssign = new PXDataFieldAssign((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue, sender.ValueToString(field, obj1, description1.DataValue))
                    {
                      OldValue = description2.DataValue
                    };
                    pars[index].Add((PXDataFieldParam) pxDataFieldAssign);
                    pars[index].Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description2.Expr, description2.DataType, description2.DataLength, description2.DataValue));
                  }
                  else
                    pars[index].Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue));
                }
                else
                  pars[index].Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue));
                if (description1.DataValue != null)
                {
                  keysVerifyer.ExcludeField(field);
                  break;
                }
                break;
              }
              if (description1.IsExcludedFromUpdate)
              {
                if (unchanged != null)
                {
                  PXDummyDataFieldRestrict dataFieldRestrict = new PXDummyDataFieldRestrict((Column) description1.Expr, description1.DataType, description1.DataLength, description2?.DataValue ?? obj2);
                  pars[index].Add((PXDataFieldParam) dataFieldRestrict);
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
              if (flag && string.Equals(field, sender._NoteIDName, StringComparison.OrdinalIgnoreCase))
              {
                if (assign.Value == null)
                {
                  assign.Value = (object) SequentialGuid.Generate();
                  sender.SetValue(row, sender._NoteIDOrdinal.Value, assign.Value);
                }
                PXDataFieldAssign pxDataFieldAssign1 = new PXDataFieldAssign(sender._NoteIDName, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), assign.Value);
                pxDataFieldAssign1.Storage = StorageBehavior.KeyValueKey;
                pars[index].Add((PXDataFieldParam) pxDataFieldAssign1);
                flag = false;
                if (sender._KeyValueAttributeNames != null)
                {
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
                        if (pxDataFieldAssign2.IsChanged = slot2 != null && valueAttributeName.Value < slot2.Length && !object.Equals(pxDataFieldAssign2.Value, slot2[valueAttributeName.Value]))
                        {
                          pxDataFieldAssign2.NewValue = sender.AttributeValueToString(valueAttributeName.Key, pxDataFieldAssign2.Value);
                          pxDataFieldAssign2.OldValue = slot2[valueAttributeName.Value];
                        }
                        pars[index].Add((PXDataFieldParam) pxDataFieldAssign2);
                      }
                    }
                  }
                }
              }
              sender._AdjustStorage(field, (PXDataFieldParam) assign);
              pars[index].Add((PXDataFieldParam) assign);
              break;
            }
          }
        }
      }
    }
    keysVerifyer.Check(sender.BqlTable);
  }

  /// <exclude />
  public override bool PersistDeleted(PXCache sender, object row)
  {
    ISqlDialect sqlDialect = sender.Graph.SqlDialect;
    System.Type[] typeArray = this.GetTables();
    List<System.Type> extensionTables = sender.GetExtensionTables();
    if (extensionTables != null)
    {
      List<System.Type> typeList = new List<System.Type>((IEnumerable<System.Type>) extensionTables);
      typeList.AddRange((IEnumerable<System.Type>) typeArray);
      typeArray = typeList.ToArray();
    }
    if (this.keys == null)
      this.keys = new List<string>((IEnumerable<string>) sender.Keys);
    List<PXDataFieldRestrict>[] pars = new List<PXDataFieldRestrict>[typeArray.Length];
    for (int index = 0; index < typeArray.Length; ++index)
      pars[index] = new List<PXDataFieldRestrict>();
    object unchanged;
    this.PrepareRestrictionsForDelete(sender, row, typeArray, sqlDialect, pars, out unchanged);
    for (int index = 0; index < typeArray.Length - 1; ++index)
    {
      foreach (string key in this.keys)
      {
        PXCommandPreparingEventArgs.FieldDescription description;
        sender.RaiseCommandPreparing(key, row, sender.GetValue(row, key), PXDBOperation.Delete, typeArray[index], out description);
        if (description == null || description.Expr == null)
          sender.RaiseCommandPreparing(key, row, sender.GetValue(row, key), PXDBOperation.Select, typeArray[index], out description);
        if (description != null && description.Expr != null)
          pars[index].Add(new PXDataFieldRestrict((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
      }
    }
    if (this._bypassOnDelete != null)
      typeArray = ((IEnumerable<System.Type>) typeArray).Except<System.Type>((IEnumerable<System.Type>) this._bypassOnDelete).ToArray<System.Type>();
    for (int index = 0; index < typeArray.Length; ++index)
    {
      if (unchanged == null)
        pars[index].Add(PXSelectOriginalsRestrict.SelectAllOriginalValues);
      ServiceLocator.Current.GetInstance<IDeletedRecordsTrackingService>().AddNoteIDValueIfNeed(sender, row, typeArray[index], pars[index]);
      try
      {
        if (!sender.Graph.ProviderDelete(typeArray[index], pars[index].ToArray()))
        {
          if (index == typeArray.Length - 1)
            throw PXDBInterceptorAttribute.GetLockViolationException(typeArray[index], (PXDataFieldParam[]) pars[index].ToArray(), PXDBOperation.Delete);
        }
      }
      catch (PXDbOperationSwitchRequiredException ex)
      {
        List<PXDataFieldAssign> pxDataFieldAssignList = new List<PXDataFieldAssign>();
        foreach (string field in (List<string>) sender.Fields)
        {
          PXCommandPreparingEventArgs.FieldDescription description;
          sender.RaiseCommandPreparing(field, row, sender.GetValue(row, field), PXDBOperation.Insert, (System.Type) null, out description);
          if (description?.Expr != null && !description.IsExcludedFromUpdate && this.tableMeet(description, typeArray[index], sqlDialect))
            pxDataFieldAssignList.Add(new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
        }
        if (index < typeArray.Length - 1)
        {
          foreach (string key in this.keys)
          {
            PXCommandPreparingEventArgs.FieldDescription description;
            sender.RaiseCommandPreparing(key, row, sender.GetValue(row, key), PXDBOperation.Update, typeArray[index], out description);
            if (description?.Expr == null || description.IsExcludedFromUpdate)
              sender.RaiseCommandPreparing(key, row, sender.GetValue(row, key), PXDBOperation.Select, typeArray[index], out description);
            if (description != null && description.Expr != null)
              pxDataFieldAssignList.Add(new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
          }
        }
        sender.Graph.ProviderInsert(typeArray[index], pxDataFieldAssignList.ToArray());
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

  private void PrepareRestrictionsForDelete(
    PXCache sender,
    object row,
    System.Type[] tables,
    ISqlDialect dialect,
    List<PXDataFieldRestrict>[] pars,
    out object unchanged)
  {
    unchanged = sender.GetOriginal(row);
    bool flag = sender._HasKeyValueStored();
    foreach (string field1 in (List<string>) sender.Fields)
    {
      string field = field1;
      PXCommandPreparingEventArgs.FieldDescription description1;
      sender.RaiseCommandPreparing(field, row, sender.GetValue(row, field), PXDBOperation.Delete, (System.Type) null, out description1);
      if (description1 != null && description1.Expr != null && !description1.IsExcludedFromDelete)
      {
        if (description1.IsRestriction)
        {
          for (int index = 0; index < tables.Length; ++index)
          {
            if (this.tableMeet(description1, tables[index], dialect))
            {
              pars[index].Add(new PXDataFieldRestrict((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue));
              break;
            }
          }
        }
        else
        {
          for (int index = 0; index < tables.Length; ++index)
          {
            if (this.tableMeet(description1, tables[index], dialect))
            {
              object obj = MaybeObjects.With<object, object>(unchanged, (Func<object, object>) (c => sender.GetValue(c, field)));
              PXCommandPreparingEventArgs.FieldDescription description2 = (PXCommandPreparingEventArgs.FieldDescription) null;
              if (obj != null)
                sender.RaiseCommandPreparing(field, unchanged, obj, PXDBOperation.Update, (System.Type) null, out description2);
              PXDataFieldRestrict assign = sender.IsKvExtField(field) || sender.IsKvExtAttribute(field) ? new PXDataFieldRestrict((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue) : (PXDataFieldRestrict) new PXDummyDataFieldRestrict((Column) description1.Expr, description1.DataType, description1.DataLength, unchanged != null ? description2?.DataValue ?? obj : description1.DataValue);
              if (flag && string.Equals(field, sender._NoteIDName, StringComparison.OrdinalIgnoreCase))
              {
                if (assign.Value == null)
                {
                  assign.Value = (object) SequentialGuid.Generate();
                  sender.SetValue(row, sender._NoteIDOrdinal.Value, assign.Value);
                }
                PXDataFieldRestrict dataFieldRestrict = new PXDataFieldRestrict(sender._NoteIDName, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), assign.Value);
                dataFieldRestrict.Storage = StorageBehavior.KeyValueKey;
                pars[index].Add(dataFieldRestrict);
                flag = false;
              }
              sender._AdjustStorage(field, (PXDataFieldParam) assign);
              pars[index].Add(assign);
              break;
            }
          }
        }
      }
    }
  }

  /// <exclude />
  internal bool IsBypassedOnDelete(System.Type table)
  {
    System.Type[] bypassOnDelete = this._bypassOnDelete;
    return bypassOnDelete != null && ((IEnumerable<System.Type>) bypassOnDelete).Contains<System.Type>(table);
  }

  private sealed class BqlRowSelection : BqlCommand, IPXExtensibleTableAttribute
  {
    protected bool disableStraightJoin;
    private List<System.Type> tables;
    private List<System.Type> optional = new List<System.Type>();
    private List<string> links;
    private Dictionary<System.Type, List<string>> fields;
    private Dictionary<System.Type, int> visited;
    private const int _recursionSize = 10;
    private readonly System.Type mainTable;
    private bool single;
    private PXCache attributecache;

    public bool ByNoteId { get; set; }

    private void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e, System.Type table)
    {
      if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Select)
        return;
      string name = this.links[0];
      System.Type dac;
      if (e.Table == (System.Type) null || sender.BqlSelect == null || sender.BqlSelect.GetType() == this.GetType())
      {
        System.Type type = e.Table;
        if ((object) type == null)
          type = table;
        dac = type;
      }
      else
      {
        dac = e.Table;
        name = $"{table.Name}_{name}";
      }
      e.BqlTable = table;
      e.Expr = (SQLExpression) new Column(name, (Table) new SimpleTable(dac));
    }

    private void RowSelecting(PXCache sender, PXRowSelectingEventArgs e, System.Type table)
    {
      if (sender != this.attributecache)
        return;
      if (e.Record != null && !(e.Record is PXDataRecordMap) && e.Record.IsDBNull(e.Position) && this.visited[table] < 10)
      {
        if (this.fields[table] == null)
          return;
        try
        {
          this.visited[table]++;
          using (new PXConnectionScope())
          {
            foreach (string str in this.fields[table])
            {
              object newValue;
              if (sender.RaiseFieldDefaulting(str, e.Row, out newValue))
                sender.RaiseFieldUpdating(str, e.Row, ref newValue);
              sender.SetValue(e.Row, str, newValue);
            }
          }
        }
        finally
        {
          this.visited[table]--;
        }
      }
      ++e.Position;
    }

    string[] IPXExtensibleTableAttribute.Keys => this.links.ToArray();

    bool IPXExtensibleTableAttribute.IsSingleTableExtension { get; } = true;

    public BqlRowSelection(PXCache cache, bool single, List<string> links)
      : this(cache, single, links, (PXDBInterceptorAttribute) null)
    {
    }

    public BqlRowSelection(
      PXCache cache,
      bool single,
      List<string> links,
      PXDBInterceptorAttribute parent)
    {
      this.attributecache = cache;
      this.single = single;
      this.mainTable = cache.GetItemType();
      this.links = links != null ? new List<string>((IEnumerable<string>) links) : new List<string>((IEnumerable<string>) cache.Keys);
      this.disableStraightJoin = cache.Graph.SqlDialect is MsSqlDialect || WebConfig.DisableStraightJoin;
      this.tables = new List<System.Type>();
      for (System.Type c = this.mainTable; c != typeof (object); c = c.BaseType)
      {
        if ((c.BaseType == typeof (object) || !typeof (IBqlTable).IsAssignableFrom(c.BaseType)) && typeof (IBqlTable).IsAssignableFrom(c) || c.IsDefined(typeof (PXTableAttribute), false))
        {
          this.tables.Add(c);
          if (c.IsDefined(typeof (PXTableAttribute), false))
          {
            foreach (PXTableAttribute customAttribute in c.GetCustomAttributes(typeof (PXTableAttribute), false))
            {
              if (customAttribute.IsOptional)
                this.optional.Add(c);
            }
          }
        }
      }
      List<System.Type> extensionTables = cache.GetExtensionTables();
      if (extensionTables != null)
      {
        if (extensionTables.Count > 0 && this.tables.Count == 1 && cache.Interceptor == parent)
          cache.SingleExtended = true;
        foreach (System.Type type in extensionTables)
        {
          if (type.IsDefined(typeof (PXTableAttribute), false))
          {
            foreach (PXTableAttribute customAttribute in type.GetCustomAttributes(typeof (PXTableAttribute), false))
            {
              if (customAttribute.IsOptional)
                this.optional.Add(type);
            }
          }
        }
      }
      if (!single || this.optional.Count <= 0 || this.links.Count <= 0)
        return;
      this.fields = new Dictionary<System.Type, List<string>>();
      this.visited = new Dictionary<System.Type, int>();
      foreach (System.Type type in this.optional)
      {
        System.Type t = type;
        string field = $"{t.Name}_{this.links[0]}";
        if (!cache.Fields.Contains(field))
        {
          cache.Fields.Add(field);
          cache.Graph.CommandPreparing.AddHandler(cache.GetItemType(), field, (PXCommandPreparing) ((sender, e) => this.CommandPreparing(sender, e, t)));
          cache.RowSelectingWhileReading += (PXRowSelecting) ((sender, e) => this.RowSelecting(sender, e, t));
          List<string> stringList = new List<string>();
          this.fields.Add(t, stringList);
          this.visited.Add(t, 0);
          string str = (string) null;
          foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes((string) null))
          {
            if (!(str == attribute.FieldName) && attribute.BqlTable == t)
            {
              stringList.Add(attribute.FieldName);
              str = attribute.FieldName;
            }
          }
        }
      }
    }

    public override Query GetQueryInternal(
      PXGraph graph,
      BqlCommandInfo info,
      BqlCommand.Selection selection)
    {
      bool flag = true;
      Query queryInternal = this.CreateQuery(graph);
      int num1 = 0;
      if (!this.disableStraightJoin)
      {
        for (int index = 1; index < this.tables.Count && this.tables[index].IsAssignableFrom(this.tables[index - 1]) && !this.optional.Contains(this.tables[index]); ++index)
          num1 = index;
      }
      List<System.Type> ordered;
      if (num1 == 0)
      {
        ordered = new List<System.Type>((IEnumerable<System.Type>) this.tables);
      }
      else
      {
        ordered = new List<System.Type>();
        for (int index = num1; index >= 0; --index)
          ordered.Add(this.tables[index]);
        for (int index = num1 + 1; index < this.tables.Count; ++index)
          ordered.Add(this.tables[index]);
      }
      if (info.Tables != null && this.single)
        info.Tables.Add(this.mainTable);
      else if (info.Tables != null)
        info.Tables.AddRange((IEnumerable<System.Type>) this.tables);
      if (graph == null)
        return queryInternal;
      Query query = this.single ? new Query() : (Query) null;
      PXCache cach = graph.Caches[this.mainTable];
      ISqlDialect sqlDialect = graph.SqlDialect;
      Dictionary<string, SQLExpression> dictionary = new Dictionary<string, SQLExpression>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      List<System.Type> extensionTables = cach.GetExtensionTables();
      bool isNewInserted = false;
      foreach (string field in (List<string>) cach.Fields)
      {
        if (!selection.Restrict || BqlCommand.IsFieldRestricted(cach, selection, field))
        {
          PXCommandPreparingEventArgs.FieldDescription description;
          cach.RaiseCommandPreparing(field, (object) null, (object) null, PXDBOperation.Select, (System.Type) null, out description);
          if (description?.Expr != null)
          {
            if ((!(description.Expr is Column) || description.BqlTable == (System.Type) null || description.BqlTable.IsAssignableFrom(this.mainTable) ? 1 : (!typeof (PXCacheExtension).IsAssignableFrom(description.BqlTable) || !description.BqlTable.BaseType.IsGenericType ? 0 : (((IEnumerable<System.Type>) description.BqlTable.BaseType.GetGenericArguments()).Last<System.Type>().IsAssignableFrom(this.mainTable) ? 1 : 0))) == 0)
              description.Expr = SQLExpression.Null();
            if (this.single)
            {
              query.GetSelection().Add((SQLExpression) new Column(field, this.mainTable));
              dictionary.Add(field, description.Expr.Duplicate());
            }
            selection.AddExpr(field, description.Expr.Duplicate());
            queryInternal.GetSelection().Add(description.Expr.SetAlias(field));
            if (field.Equals("IsNew4D74EB2BAF344EFBA2F24DCAB634D145"))
              isNewInserted = true;
          }
        }
      }
      List<System.Type> bound = new List<System.Type>();
      for (int i = 0; i < ordered.Count; i++)
      {
        Joiner.JoinType jt = Joiner.JoinType.INNER_JOIN;
        Joiner.JoinHint jh = Joiner.JoinHint.NONE;
        if (i == 0)
          jt = Joiner.JoinType.MAIN_TABLE;
        else if (i == 1 && i <= num1)
          jh = Joiner.JoinHint.STRAIGHT_FOR_TPT;
        else if (this.optional.Contains(ordered[i]))
          jt = Joiner.JoinType.LEFT_JOIN;
        TableChangingScope.AddUnchangedRealName(ordered[i].Name);
        Joiner j = new Joiner(jt, TableChangingScope.GetSQLTable((Func<Table>) (() => (Table) new SimpleTable(ordered[i])), ordered[i].Name, isNewInserted), queryInternal);
        j.SetHint(jh);
        bound.Add(ordered[i]);
        if (jt != Joiner.JoinType.MAIN_TABLE)
        {
          if (this.links.Count > 0)
          {
            SQLExpression exp = this.MakeWhere(bound, cach);
            TableChangingScope.AppendRestrictionsOnIsNew(ref exp, cach.Graph, ordered.Take<System.Type>(i + 1).ToList<System.Type>(), new BqlCommand.Selection(), true);
            j.On(exp);
          }
          else
            j.On(new SQLConst((object) 1).EQ((object) 1));
          bound.Remove(ordered[i]);
        }
        queryInternal.AddJoin(j);
      }
      if (extensionTables != null)
      {
        int num2 = 0;
        foreach (System.Type type in extensionTables)
        {
          System.Type ext = type;
          TableChangingScope.AddUnchangedRealName(ext.Name);
          Joiner.JoinType jt = Joiner.JoinType.INNER_JOIN;
          if (this.optional.Contains(ext))
            jt = Joiner.JoinType.LEFT_JOIN;
          Joiner j = new Joiner(jt, TableChangingScope.GetSQLTable((Func<Table>) (() => (Table) new SimpleTable(ext)), ext.Name), queryInternal);
          bound.Add(ext);
          if (this.links.Count > 0)
          {
            SQLExpression exp = this.MakeWhere(bound, cach);
            TableChangingScope.AppendRestrictionsOnIsNew(ref exp, cach.Graph, ordered.Concat<System.Type>(extensionTables.Take<System.Type>(num2 + 1)).ToList<System.Type>(), new BqlCommand.Selection(), true);
            j.On(exp);
          }
          else
            j.On(new SQLConst((object) 1).EQ((object) 1));
          bound.Remove(ext);
          queryInternal.AddJoin(j);
          ++num2;
        }
      }
      if (bound.Count > 1)
        queryInternal.Where(this.MakeWhere(bound, cach));
      queryInternal.GetWhere();
      if (this.single)
      {
        string[] strArray;
        if (this.ByNoteId)
          strArray = new string[1]{ "noteid" };
        else
          strArray = cach.Keys.ToArray();
        SQLExpression sqlExpression = SQLExpression.None();
        SQLExpression w = SQLExpression.None();
        for (int p = 0; p < strArray.Length; ++p)
        {
          SQLExpression l;
          if (dictionary.TryGetValue(strArray[p], out l))
            sqlExpression = sqlExpression.And(SQLExpressionExt.EQ(l, (SQLExpression) Literal.NewParameter(p)));
          w = w.And(SQLExpressionExt.EQ(new Column(strArray[p], this.mainTable), (SQLExpression) Literal.NewParameter(p)));
        }
        query.Where(w);
        if (queryInternal.GetWhere() == null)
          queryInternal.Where(sqlExpression);
        else
          queryInternal.Where(queryInternal.GetWhere().And(sqlExpression));
        queryInternal.Alias = this.mainTable.Name;
        query.From((Table) queryInternal);
        queryInternal = query;
      }
      if (!flag)
        queryInternal.NotOK();
      return queryInternal;
    }

    private SQLExpression MakeWhere(List<System.Type> bound, PXCache cache)
    {
      SQLExpression sqlExpression = SQLExpression.None();
      for (int index1 = 0; index1 < this.links.Count; ++index1)
      {
        for (int index2 = 0; index2 < bound.Count; ++index2)
        {
          PXCommandPreparingEventArgs.FieldDescription description1;
          cache.RaiseCommandPreparing(this.links[index1], (object) null, (object) null, PXDBOperation.Select, bound[index2], out description1);
          if (description1?.Expr != null)
          {
            int index3 = index2 + 1;
            if (index3 < bound.Count)
            {
              PXCommandPreparingEventArgs.FieldDescription description2;
              cache.RaiseCommandPreparing(this.links[index1], (object) null, (object) null, PXDBOperation.Select, bound[index3], out description2);
              if (description2?.Expr != null)
              {
                sqlExpression = sqlExpression.And(SQLExpressionExt.EQ(description1.Expr, description2.Expr));
                ref SQLExpression local = ref sqlExpression;
                PXGraph graph = cache.Graph;
                List<System.Type> tables = new List<System.Type>();
                tables.Add(bound[index2]);
                tables.Add(bound[index3]);
                BqlCommand.Selection selection = new BqlCommand.Selection();
                TableChangingScope.AppendRestrictionsOnIsNew(ref local, graph, tables, selection, true);
              }
            }
          }
        }
      }
      return sqlExpression;
    }

    public override void Verify(
      PXCache cache,
      object item,
      List<object> pars,
      ref bool? result,
      ref object value)
    {
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

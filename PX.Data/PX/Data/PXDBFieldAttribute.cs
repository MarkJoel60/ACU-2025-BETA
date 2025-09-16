// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBFieldAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>The base class for attributes that map DAC fields to database
/// columns. The attribute should not be used directly.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
[PXAttributeFamily(typeof (PXDBFieldAttribute))]
[PXAttributeFamily(typeof (PXFieldState))]
public class PXDBFieldAttribute : 
  PXEventSubscriberAttribute,
  IPXRowSelectingSubscriber,
  IPXCommandPreparingSubscriber
{
  protected string _DatabaseFieldName;
  protected bool _IsKey;
  protected bool _IsImmutable;

  /// <summary>Gets or sets the name of the database column that is
  /// represented by the field. By default, equals the field name.</summary>
  public virtual string DatabaseFieldName
  {
    get => this._DatabaseFieldName ?? this._FieldName;
    set => this._DatabaseFieldName = value;
  }

  /// <summary>Gets or sets the value that indicates whether the field is a
  /// key field. Key fields must uniquely identify a data record. The key
  /// fields defined in the DAC should not necessarily be the same as the
  /// keys in the database.</summary>
  public virtual bool IsKey
  {
    get => this._IsKey;
    set
    {
      this._IsKey = value;
      this._IsImmutable = this.IsImmutable | value;
    }
  }

  /// <summary>Gets or sets the values that indicates that the field is
  /// immutable.</summary>
  public virtual bool IsImmutable
  {
    get => this._IsImmutable;
    set => this._IsImmutable = value;
  }

  /// <summary>Returns <tt>null</tt> on get. Sets the BQL field representing
  /// the field in BQL queries.</summary>
  public virtual System.Type BqlField
  {
    get => (System.Type) null;
    set
    {
      this._DatabaseFieldName = char.ToUpper(value.Name[0]).ToString() + value.Name.Substring(1);
      if (!value.IsNested)
        return;
      if (value.DeclaringType.IsDefined(typeof (PXTableAttribute), true))
        this.BqlTable = value.DeclaringType;
      else
        this.BqlTable = BqlCommand.GetItemType(value);
    }
  }

  /// <exclude />
  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    this.PrepareCommandImpl(this.DatabaseFieldName, e);
    if (e.Expr == null || !(e.Expr is Column expr) || expr.GetDBType() != PXDbType.Unspecified)
      return;
    expr.SetDBType(e.DataType);
  }

  protected virtual void PrepareCommandImpl(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    this.PrepareFieldName(dbFieldName, e);
    e.DataValue = e.Value;
    e.IsRestriction = e.IsRestriction || this._IsKey;
  }

  protected virtual void PrepareFieldName(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    if (dbFieldName == null)
      return;
    PXCommandPreparingEventArgs preparingEventArgs = e;
    System.Type type1 = this.prepCacheExtensionType;
    if ((object) type1 == null)
      type1 = this._BqlTable;
    preparingEventArgs.BqlTable = type1;
    System.Type type2 = this.prepCacheExtensionType;
    if ((object) type2 == null)
      type2 = e.Table ?? this._BqlTable;
    System.Type dac = type2;
    e.Expr = (SQLExpression) new Column(dbFieldName, (Table) new SimpleTable(dac), e.DataType);
  }

  /// <exclude />
  public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
    {
      object obj = e.Record.GetValue(e.Position);
      sender.SetValue(e.Row, this._FieldOrdinal, obj);
    }
    ++e.Position;
  }

  /// <exclude />
  public override string ToString()
  {
    return $"{this.GetType().Name} {this.FieldName} {this.FieldOrdinal}";
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    if (this._DatabaseFieldName == null)
      this._DatabaseFieldName = this._FieldName;
    if (this.IsKey)
      sender.Keys.Add(this._FieldName);
    if (!this.IsImmutable)
      return;
    sender.Immutables.Add(this._FieldName);
  }

  public static void ActivateDynamicFields(PXCache cache)
  {
    foreach (PXDBFieldAttribute pxdbFieldAttribute in cache.GetAttributes((string) null).OfType<PXDBFieldAttribute>())
      pxdbFieldAttribute.ActivateDynamicFields();
  }

  protected virtual void ActivateDynamicFields()
  {
  }
}

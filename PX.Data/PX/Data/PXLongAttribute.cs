// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLongAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Indicates a DAC field of <tt>long?</tt> type that is not
/// mapped to a database column.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field is not bound to a table column.</remarks>
/// <example>
/// <code>
/// [PXLong(IsKey = true)]
/// [PXUIField(DisplayName = "Transaction Num.")]
/// public virtual Int64? TranID { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
[PXAttributeFamily(typeof (PXFieldState))]
public class PXLongAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXCommandPreparingSubscriber
{
  protected long _MinValue = long.MinValue;
  protected long _MaxValue = long.MaxValue;
  protected bool _IsKey;

  /// <summary>Gets or sets the value that indicates whether the field is a
  /// key field.</summary>
  public virtual bool IsKey
  {
    get => this._IsKey;
    set => this._IsKey = value;
  }

  /// <summary>Gets or sets the minimum value for the field.</summary>
  public long MinValue
  {
    get => this._MinValue;
    set => this._MinValue = value;
  }

  /// <summary>Gets or sets the maximum value for the field.</summary>
  public long MaxValue
  {
    get => this._MaxValue;
    set => this._MaxValue = value;
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!(e.NewValue is string))
      return;
    long result;
    if (long.TryParse((string) e.NewValue, NumberStyles.Any, (IFormatProvider) sender.Graph.Culture, out result))
      e.NewValue = (object) result;
    else
      e.NewValue = (object) null;
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXLongState.CreateInstance(e.ReturnState, this._FieldName, new bool?(this._IsKey), new int?(-1), new long?(this._MinValue), new long?(this._MaxValue), typeof (long));
  }

  /// <exclude />
  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Select)
      return;
    if (e.Value != null)
    {
      e.BqlTable = this._BqlTable;
      if (e.Expr == null)
        e.Expr = (SQLExpression) new Column(this._FieldName, e.BqlTable);
      e.DataValue = e.Value;
      e.DataLength = new int?(8);
    }
    e.DataType = PXDbType.BigInt;
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    if (!this.IsKey)
      return;
    sender.Keys.Add(this._FieldName);
  }
}

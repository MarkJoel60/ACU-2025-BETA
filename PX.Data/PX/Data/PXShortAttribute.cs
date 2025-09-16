// Decompiled with JetBrains decompiler
// Type: PX.Data.PXShortAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Indicates a DAC field of <tt>short?</tt> type that is not
/// mapped to a database column.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field is not bound to a table column.</remarks>
/// <example>
/// <code>
/// [PXShort()]
/// [PXDefault((short)0)]
/// [PXUIField(DisplayName = "Overdue Days", Enabled = false)]
/// public virtual short? OverdueDays { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
[PXAttributeFamily(typeof (PXFieldState))]
public class PXShortAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXCommandPreparingSubscriber
{
  protected int _MinValue = (int) short.MinValue;
  protected int _MaxValue = (int) short.MaxValue;
  protected bool _IsKey;

  /// <summary>Gets or sets the value that indicates whether the field is a
  /// key field.</summary>
  public virtual bool IsKey
  {
    get => this._IsKey;
    set => this._IsKey = value;
  }

  /// <summary>Gets or sets the minimum value for the field.</summary>
  public int MinValue
  {
    get => this._MinValue;
    set => this._MinValue = value;
  }

  /// <summary>Gets or sets the maximum value for the field.</summary>
  public int MaxValue
  {
    get => this._MaxValue;
    set => this._MaxValue = value;
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!(e.NewValue is string))
      return;
    short result;
    if (short.TryParse((string) e.NewValue, NumberStyles.Any, (IFormatProvider) sender.Graph.Culture, out result))
      e.NewValue = (object) result;
    else
      e.NewValue = (object) null;
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXIntState.CreateInstance(e.ReturnState, this._FieldName, new bool?(this._IsKey), new int?(-1), new int?(this._MinValue), new int?(this._MaxValue), (int[]) null, (string[]) null, typeof (short), new int?());
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
      e.DataLength = new int?(2);
    }
    e.DataType = PXDbType.SmallInt;
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    if (!this.IsKey)
      return;
    sender.Keys.Add(this._FieldName);
  }
}

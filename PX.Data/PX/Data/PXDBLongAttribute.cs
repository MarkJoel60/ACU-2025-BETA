// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBLongAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>int64?</tt> type to the database
/// column of <tt>bigint</tt> type.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database column with the same
/// name.</remarks>
/// <example>
/// <code>
/// [PXDBLong()]
/// public virtual long? CuryInfoID { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBLongAttribute : 
  PXDBFieldAttribute,
  IPXRowSelectingSubscriber,
  IPXCommandPreparingSubscriber,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber
{
  protected long _MinValue = long.MinValue;
  protected long _MaxValue = long.MaxValue;

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
  protected override void PrepareCommandImpl(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    base.PrepareCommandImpl(dbFieldName, e);
    e.DataType = PXDbType.BigInt;
    e.DataLength = new int?(8);
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
      sender.SetValue(e.Row, this._FieldOrdinal, (object) e.Record.GetInt64(e.Position));
    ++e.Position;
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
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBByteAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>byte?</tt> type to the database
/// column of <tt>tinyint</tt> type.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database column with the same
/// name.</remarks>
/// <example><para>The code below shows the CacheAttached event handler, which is defined in a graph and replaces attributes of the FilterRow.Condition field within the current graph.</para>
/// <code title="Example" lang="CS">
/// [PXDefault]
/// [PXDBByte]
/// [PXUIField(DisplayName = "Condition")]
/// protected virtual void FilterRow_Condition_CacheAttached(PXCache sender)
/// {
/// }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBByteAttribute : 
  PXDBFieldAttribute,
  IPXRowSelectingSubscriber,
  IPXCommandPreparingSubscriber,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXFieldVerifyingSubscriber
{
  protected int _MinValue;
  protected int _MaxValue = (int) byte.MaxValue;

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
  protected override void PrepareCommandImpl(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    base.PrepareCommandImpl(dbFieldName, e);
    e.DataType = PXDbType.TinyInt;
    e.DataLength = new int?(1);
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
      sender.SetValue(e.Row, this._FieldOrdinal, (object) e.Record.GetByte(e.Position));
    ++e.Position;
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!(e.NewValue is string))
      return;
    byte result;
    if (byte.TryParse((string) e.NewValue, NumberStyles.Any, (IFormatProvider) sender.Graph.Culture, out result))
      e.NewValue = (object) result;
    else
      e.NewValue = (object) null;
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXIntState.CreateInstance(e.ReturnState, this._FieldName, new bool?(this._IsKey), new int?(-1), new int?(this._MinValue), new int?(this._MaxValue), (int[]) null, (string[]) null, typeof (byte), new int?());
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(e.NewValue is byte newValue))
      return;
    if ((int) newValue < this._MinValue)
      throw new PXSetPropertyException(e.Row as IBqlTable, "The value must be greater than or equal to {0}.", new object[1]
      {
        (object) this._MinValue
      });
    if ((int) newValue > this._MaxValue)
      throw new PXSetPropertyException(e.Row as IBqlTable, "The value must be less than or equal to {0}.", new object[1]
      {
        (object) this._MaxValue
      });
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBUShortAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>ushort?</tt> type to the database
/// column of <tt>int</tt> type.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database column with the same
/// name.</remarks>
/// <example>
/// <code>
/// [PXDBUShort()]
/// public virtual ushort LineNbr { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBUShortAttribute : 
  PXDBFieldAttribute,
  IPXRowSelectingSubscriber,
  IPXCommandPreparingSubscriber,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXFieldVerifyingSubscriber
{
  protected int _MinValue;
  protected int _MaxValue = (int) ushort.MaxValue;

  /// <summary>Gets or sets the minimum value for the field.</summary>
  public int MinValue
  {
    get => this._MinValue;
    set => this._MinValue = value;
  }

  /// <summary>Gets or sets the minimum value for the field.</summary>
  public int MaxValue
  {
    get => this._MaxValue;
    set => this._MaxValue = value;
  }

  /// <exclude />
  protected override void PrepareCommandImpl(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    base.PrepareCommandImpl(dbFieldName, e);
    e.DataType = PXDbType.Int;
    e.DataLength = new int?(2);
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
    {
      PXCache pxCache = sender;
      object row = e.Row;
      int fieldOrdinal = this._FieldOrdinal;
      int? int32 = e.Record.GetInt32(e.Position);
      // ISSUE: variable of a boxed type
      __Boxed<ushort?> local = (ValueType) (int32.HasValue ? new ushort?((ushort) int32.GetValueOrDefault()) : new ushort?());
      pxCache.SetValue(row, fieldOrdinal, (object) local);
    }
    ++e.Position;
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!(e.NewValue is string))
      return;
    ushort result;
    if (ushort.TryParse((string) e.NewValue, NumberStyles.Any, (IFormatProvider) sender.Graph.Culture, out result))
      e.NewValue = (object) result;
    else
      e.NewValue = (object) null;
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXIntState.CreateInstance(e.ReturnState, this._FieldName, new bool?(this._IsKey), new int?(-1), new int?(this._MinValue), new int?(this._MaxValue), (int[]) null, (string[]) null, typeof (ushort), new int?());
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(e.NewValue is ushort newValue))
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

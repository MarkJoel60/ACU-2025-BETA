// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBDoubleAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>double?</tt> type to the 8-bytes
/// floating point column in the database.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database column with the same
/// name.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBDoubleAttribute : 
  PXDBFieldAttribute,
  IPXRowSelectingSubscriber,
  IPXCommandPreparingSubscriber,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXFieldVerifyingSubscriber
{
  protected int _Precision = 2;
  protected double _MinValue = double.MinValue;
  protected double _MaxValue = double.MaxValue;

  /// <summary>Gets or sets the minimum value for the field.</summary>
  public double MinValue
  {
    get => this._MinValue;
    set => this._MinValue = value;
  }

  /// <summary>Gets or sets the maximum value for the field.</summary>
  public double MaxValue
  {
    get => this._MaxValue;
    set => this._MaxValue = value;
  }

  /// <summary>Initializes a new instance of the attribute with default
  /// parameters.</summary>
  public PXDBDoubleAttribute()
  {
  }

  /// <summary>Initializes a new instance of the attribute with the given
  /// precision. The precision is the number of digits after the comma. If a
  /// user enters a value with greater number of fractional digits, the
  /// value will be rounded.</summary>
  /// <param name="precision">The value to use as the precision.</param>
  public PXDBDoubleAttribute(int precision) => this._Precision = precision;

  /// <exclude />
  public static void SetPrecision(PXCache cache, object data, string name, int precision)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, name))
    {
      if (attribute is PXDBDoubleAttribute)
        ((PXDBDoubleAttribute) attribute)._Precision = precision;
    }
  }

  /// <exclude />
  public static void SetPrecision(PXCache cache, string name, int precision)
  {
    cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(name))
    {
      if (attribute is PXDBDoubleAttribute)
        ((PXDBDoubleAttribute) attribute)._Precision = precision;
    }
  }

  /// <exclude />
  protected override void PrepareCommandImpl(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    base.PrepareCommandImpl(dbFieldName, e);
    e.DataType = PXDbType.Float;
    e.DataLength = new int?(8);
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
      sender.SetValue(e.Row, this._FieldOrdinal, (object) e.Record.GetDouble(e.Position));
    ++e.Position;
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue is string)
    {
      double result;
      e.NewValue = !double.TryParse((string) e.NewValue, NumberStyles.Any, (IFormatProvider) sender.Graph.Culture, out result) ? (object) null : (object) result;
    }
    if (e.NewValue == null)
      return;
    e.NewValue = (object) System.Math.Round((double) e.NewValue, this._Precision);
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXDoubleState.CreateInstance(e.ReturnState, new int?(this._Precision), this._FieldName, new bool?(this._IsKey), new int?(-1), new double?(this._MinValue), new double?(this._MaxValue));
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(e.NewValue is double newValue))
      return;
    if (newValue < this._MinValue)
      throw new PXSetPropertyException(e.Row as IBqlTable, "The value must be greater than or equal to {0}.", new object[1]
      {
        (object) this._MinValue
      });
    if (newValue > this._MaxValue)
      throw new PXSetPropertyException(e.Row as IBqlTable, "The value must be less than or equal to {0}.", new object[1]
      {
        (object) this._MaxValue
      });
  }
}

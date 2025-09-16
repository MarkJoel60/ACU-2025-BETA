// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBFloatAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>float?</tt> type to the 4-bytes floating point column in the database.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBFloatAttribute : 
  PXDBFieldAttribute,
  IPXRowSelectingSubscriber,
  IPXCommandPreparingSubscriber,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXFieldVerifyingSubscriber
{
  protected int _Precision = 2;
  protected float _MinValue = float.MinValue;
  protected float _MaxValue = float.MaxValue;

  /// <summary>Gets or sets the minimum value for the field.</summary>
  public float MinValue
  {
    get => this._MinValue;
    set => this._MinValue = value;
  }

  /// <summary>Gets or sets the maximum value for the field.</summary>
  public float MaxValue
  {
    get => this._MaxValue;
    set => this._MaxValue = value;
  }

  /// <summary>Initializes a new instance with default parameters.</summary>
  public PXDBFloatAttribute()
  {
  }

  /// <summary>Initializes a new instance of the attribute with the given
  /// precision. The precision is the number of digits after the comma. If a
  /// user enters a value with greater number of fractional digits, the
  /// value will be rounded.</summary>
  /// <param name="precision">The value to use as the precision.</param>
  public PXDBFloatAttribute(int precision) => this._Precision = precision;

  /// <exclude />
  public static void SetPrecision(PXCache cache, object data, string name, int precision)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, name))
    {
      if (attribute is PXDBFloatAttribute)
        ((PXDBFloatAttribute) attribute)._Precision = precision;
    }
  }

  /// <exclude />
  public static void SetPrecision(PXCache cache, string name, int precision)
  {
    cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(name))
    {
      if (attribute is PXDBFloatAttribute)
        ((PXDBFloatAttribute) attribute)._Precision = precision;
    }
  }

  /// <exclude />
  protected override void PrepareCommandImpl(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    base.PrepareCommandImpl(dbFieldName, e);
    e.DataType = PXDbType.Real;
    e.DataLength = new int?(4);
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
      sender.SetValue(e.Row, this._FieldOrdinal, (object) e.Record.GetFloat(e.Position));
    ++e.Position;
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue is string)
    {
      float result;
      e.NewValue = !float.TryParse((string) e.NewValue, NumberStyles.Any, (IFormatProvider) sender.Graph.Culture, out result) ? (object) null : (object) result;
    }
    if (e.NewValue == null)
      return;
    e.NewValue = (object) Convert.ToSingle(System.Math.Round((double) (float) e.NewValue, this._Precision));
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXFloatState.CreateInstance(e.ReturnState, new int?(this._Precision), this._FieldName, new bool?(this._IsKey), new int?(-1), new float?(this._MinValue), new float?(this._MaxValue));
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(e.NewValue is float newValue))
      return;
    if ((double) newValue < (double) this._MinValue)
      throw new PXSetPropertyException(e.Row as IBqlTable, "The value must be greater than or equal to {0}.", new object[1]
      {
        (object) this._MinValue
      });
    if ((double) newValue > (double) this._MaxValue)
      throw new PXSetPropertyException(e.Row as IBqlTable, "The value must be less than or equal to {0}.", new object[1]
      {
        (object) this._MaxValue
      });
  }
}

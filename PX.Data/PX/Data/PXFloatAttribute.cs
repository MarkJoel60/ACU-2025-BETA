// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFloatAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Indicates a DAC field of <tt>float?</tt> type that is not
/// mapped to a database column.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field is not bound to a table column.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
[PXAttributeFamily(typeof (PXFieldState))]
public class PXFloatAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXCommandPreparingSubscriber
{
  protected int _Precision = 2;
  protected float _MinValue = float.MinValue;
  protected float _MaxValue = float.MaxValue;
  protected bool _IsKey;

  /// <summary>Gets or sets the value that indicates whether the field is a
  /// key field.</summary>
  public virtual bool IsKey
  {
    get => this._IsKey;
    set => this._IsKey = value;
  }

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

  /// <summary>Initializes a new instance of the attribute with default
  /// parameters.</summary>
  public PXFloatAttribute()
  {
  }

  /// <summary>Initializes a new instance of the attribute with the given
  /// precision. The precision is the number of digits after the comma. If a
  /// user enters a value with greater number of fractional digits, the
  /// value will be rounded.</summary>
  /// <param name="precision">The value to use as the precision.</param>
  public PXFloatAttribute(int precision) => this._Precision = precision;

  /// <exclude />
  public static void SetPrecision(PXCache cache, object data, string name, int precision)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, name))
    {
      if (attribute is PXFloatAttribute)
        ((PXFloatAttribute) attribute)._Precision = precision;
    }
  }

  /// <exclude />
  public static void SetPrecision<Field>(PXCache cache, object data, int precision) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXFloatAttribute)
        ((PXFloatAttribute) attribute)._Precision = precision;
    }
  }

  /// <exclude />
  public static void SetPrecision(PXCache cache, string name, int precision)
  {
    cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(name))
    {
      if (attribute is PXFloatAttribute)
        ((PXFloatAttribute) attribute)._Precision = precision;
    }
  }

  /// <exclude />
  public static void SetPrecision<Field>(PXCache cache, int precision) where Field : IBqlField
  {
    cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>())
    {
      if (attribute is PXFloatAttribute)
        ((PXFloatAttribute) attribute)._Precision = precision;
    }
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
      e.DataLength = new int?(4);
    }
    e.DataType = PXDbType.Real;
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    if (!this.IsKey)
      return;
    sender.Keys.Add(this._FieldName);
  }
}

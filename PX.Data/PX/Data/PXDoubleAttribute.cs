// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDoubleAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Indicates a DAC field of <tt>double?</tt> type that is not
/// mapped to a database column.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field is not bound to a table column.</remarks>
/// <example>
/// <code>
/// [PXDouble]
/// [PXUIField(Visible = false)]
/// public virtual Double? OriginalShift { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
[PXAttributeFamily(typeof (PXFieldState))]
public class PXDoubleAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXCommandPreparingSubscriber
{
  protected int _Precision = 2;
  protected double _MinValue = double.MinValue;
  protected double _MaxValue = double.MaxValue;
  protected bool _IsKey;

  /// <summary>Gets or sets the value that indicates whether the field is a
  /// key field.</summary>
  public virtual bool IsKey
  {
    get => this._IsKey;
    set => this._IsKey = value;
  }

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
  public PXDoubleAttribute()
  {
  }

  /// <summary>Initializes a new instance of the attribute with the given
  /// precision. The precision is the number of digits after the comma. If a
  /// user enters a value with greater number of fractional digits, the
  /// value will be rounded.</summary>
  /// <param name="precision">The value to use as the precision.</param>
  public PXDoubleAttribute(int precision) => this._Precision = precision;

  /// <summary>
  /// Sets the precision in the attribute instance that marks the
  /// field with the specified name in a particular data record.
  /// </summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDouble</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  /// <param name="name">The name of the field that is marked with the
  /// attribute.</param>
  /// <param name="precision">The new precision value.</param>
  public static void SetPrecision(PXCache cache, object data, string name, int precision)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, name))
    {
      if (attribute is PXDoubleAttribute)
        ((PXDoubleAttribute) attribute)._Precision = precision;
    }
  }

  /// <summary>
  /// Sets the precision in the attribute instance that marks the
  /// specified field in a particular data record.
  /// </summary>
  /// <typeparam name="Field">The field that is marked with the attribute.</typeparam>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDouble</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  /// <param name="precision">The new precision value.</param>
  public static void SetPrecision<Field>(PXCache cache, object data, int precision) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXDoubleAttribute)
        ((PXDoubleAttribute) attribute)._Precision = precision;
    }
  }

  /// <summary>Sets the precision in the attribute instance that marks the
  /// field with the specified name in all data records in the cache
  /// object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDouble</tt> type.</param>
  /// <param name="name">The name of the field that is marked with the
  /// attribute.</param>
  /// <param name="precision">The new precision value.</param>
  public static void SetPrecision(PXCache cache, string name, int precision)
  {
    cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(name))
    {
      if (attribute is PXDoubleAttribute)
        ((PXDoubleAttribute) attribute)._Precision = precision;
    }
  }

  /// <summary>Sets the precision in the attribute instance that marks the
  /// specified field in all data records in the cache
  /// object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDouble</tt> type.</param>
  /// <param name="precision">The new precision value.</param>
  /// <typeparam name="Field">The field that is marked with the attribute.</typeparam>
  public static void SetPrecision<Field>(PXCache cache, int precision) where Field : IBqlField
  {
    cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>())
    {
      if (attribute is PXDoubleAttribute)
        ((PXDoubleAttribute) attribute)._Precision = precision;
    }
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
    e.DataType = PXDbType.Float;
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    if (!this.IsKey)
      return;
    sender.Keys.Add(this._FieldName);
  }
}

// Decompiled with JetBrains decompiler
// Type: PXListedDoubleState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
/// <summary>Provides data to set up the presentation of the input control for the <tt>diuble</tt> DAC field.</summary>
public class PXListedDoubleState : PXFieldState
{
  protected double _MinValue = double.MinValue;
  protected double _MaxValue = double.MaxValue;
  protected double[] _AllowedValues;
  internal string[] _NeutralLabels;
  protected string[] _AllowedLabels;
  protected bool _ExclusiveValues;
  protected bool _EmptyPossible;

  protected PXListedDoubleState(object value)
    : base(value)
  {
    this._DataType = typeof (double);
  }

  /// <summary>The minimum value that could be set in the field input control</summary>
  public virtual double MinValue
  {
    get => this._MinValue;
    set => this._MinValue = value;
  }

  /// <summary>The maximum value that could be set in the field input control.</summary>
  public virtual double MaxValue
  {
    get => this._MaxValue;
    set => this._MaxValue = value;
  }

  /// <summary>The list of values for the field input control of the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> type.</summary>
  public virtual double[] AllowedValues
  {
    get => this._AllowedValues;
    set => this._AllowedValues = value;
  }

  /// <summary>The list of labels for the field input control of the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> type.</summary>
  public virtual string[] AllowedLabels
  {
    get
    {
      if (this._AllowedValues == null)
        return (string[]) null;
      return this._AllowedLabels != null && this._AllowedLabels.Length >= this._AllowedValues.Length ? this._AllowedLabels : ((IEnumerable<double>) this._AllowedValues).Select<double, string>((Func<double, string>) (value => value.ToString())).ToArray<string>();
    }
    set => this._AllowedLabels = value;
  }

  /// <summary>A value that enables (if set to <see langword="true" />) filtering by the empty value in the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> field input control.</summary>
  public virtual bool EmptyPossible
  {
    get => this._EmptyPossible;
    set => this._EmptyPossible = value;
  }

  /// <summary>The collection of values and labels for the field input control of the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> type.</summary>
  public Dictionary<double, string> ValueLabelDic
  {
    get
    {
      if (this._AllowedValues == null || this._AllowedLabels == null)
        return (Dictionary<double, string>) null;
      Dictionary<double, string> valueLabelDic = new Dictionary<double, string>(this._AllowedValues.Length);
      for (int index = 0; index < this._AllowedValues.Length; ++index)
        valueLabelDic.Add(this._AllowedValues[index], this._AllowedLabels[index]);
      return valueLabelDic;
    }
  }

  /// <summary>Tries to get value of the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> field input control by a label.</summary>
  internal bool TryGetListValue(string label, out object result)
  {
    double[] allowedValues = this.AllowedValues;
    string[] allowedLabels = this.AllowedLabels;
    for (int index = 0; index < allowedLabels.Length; ++index)
    {
      if (PXLocalesProvider.CollationComparer.Equals(allowedLabels[index], label))
      {
        result = (object) allowedValues[index];
        return true;
      }
    }
    if (this._NeutralLabels != null && this._NeutralLabels.Length == allowedLabels.Length)
    {
      string[] neutralLabels = this._NeutralLabels;
      for (int index = 0; index < neutralLabels.Length; ++index)
      {
        if (PXLocalesProvider.CollationComparer.Equals(neutralLabels[index], label))
        {
          result = (object) allowedValues[index];
          return true;
        }
      }
    }
    result = (object) null;
    return false;
  }

  /// <summary>Tries to get a label of the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> field input control by a value.</summary>
  internal bool TryGetListLabel(double value, out string result)
  {
    double[] allowedValues = this.AllowedValues;
    string[] allowedLabels = this.AllowedLabels;
    for (int index = 0; index < allowedValues.Length; ++index)
    {
      if (allowedValues[index] == value && index < allowedLabels.Length && !string.IsNullOrEmpty(allowedLabels[index]))
      {
        result = allowedLabels[index];
        return true;
      }
    }
    result = (string) null;
    return false;
  }

  /// <exclude />
  public static PXFieldState CreateInstance(
    object value,
    int? precision,
    string fieldName,
    bool? isKey,
    int? required,
    double? minValue,
    double? maxValue,
    double[] allowedValues,
    string[] allowedLabels,
    double? defaultValue,
    string[] neutralLabels)
  {
    return PXListedDoubleState.CreateInstance(value, precision, fieldName, isKey, required, minValue, maxValue, allowedValues, allowedLabels, defaultValue, neutralLabels, new bool?());
  }

  /// <exclude />
  public static PXFieldState CreateInstance(
    object value,
    int? precision,
    string fieldName,
    bool? isKey,
    int? required,
    double? minValue,
    double? maxValue,
    double[] allowedValues,
    string[] allowedLabels,
    double? defaultValue,
    string[] neutralLabels = null,
    bool? nullable = null)
  {
    return PXListedDoubleState.CreateInstance(value, precision, fieldName, isKey, required, minValue, maxValue, allowedValues, allowedLabels, new bool?(false), defaultValue, neutralLabels, nullable);
  }

  /// <exclude />
  public static PXFieldState CreateInstance(
    object value,
    int? precision,
    string fieldName,
    bool? isKey,
    int? required,
    double? minValue,
    double? maxValue,
    double[] allowedValues,
    string[] allowedLabels,
    bool? exclusiveValues,
    double? defaultValue,
    string[] neutralLabels = null,
    bool? nullable = null)
  {
    switch (value)
    {
      case PXListedDoubleState instance1:
label_6:
        instance1._DataType = typeof (double);
        if (precision.HasValue)
          instance1._Precision = precision.Value;
        if (fieldName != null)
          instance1._FieldName = fieldName;
        if (isKey.HasValue)
          instance1._IsKey = isKey.Value;
        if (required.HasValue)
          instance1._Required += required.Value;
        if (minValue.HasValue)
          instance1._MinValue = minValue.Value;
        if (maxValue.HasValue)
          instance1._MaxValue = maxValue.Value;
        if (allowedValues != null)
          instance1._AllowedValues = allowedValues;
        if (allowedLabels != null)
          instance1._AllowedLabels = allowedLabels;
        if (neutralLabels != null)
          instance1._NeutralLabels = neutralLabels;
        if (exclusiveValues.HasValue)
          instance1._ExclusiveValues = exclusiveValues.Value;
        if (defaultValue.HasValue)
          instance1._DefaultValue = (object) defaultValue;
        if (nullable.HasValue)
          instance1._Nullable = nullable.Value;
        return (PXFieldState) instance1;
      case PXFieldState instance2:
        if (instance2.DataType != typeof (object) && instance2.DataType != typeof (double))
        {
          if (isKey.HasValue)
            instance2._IsKey = isKey.Value;
          return instance2;
        }
        goto default;
      default:
        instance1 = new PXListedDoubleState(value);
        goto label_6;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.PXIntState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Provides data to set up the presentation of the input control for the <tt>integer</tt> DAC field.</summary>
public class PXIntState : PXFieldState
{
  protected int _MinValue = int.MinValue;
  protected int _MaxValue = int.MaxValue;
  protected int[] _AllowedValues;
  internal string[] _NeutralLabels;
  protected string[] _AllowedLabels;
  protected string[] _AllowedImages;
  protected bool _ExclusiveValues;
  protected bool _EmptyPossible;

  protected PXIntState(object value)
    : base(value)
  {
    this._DataType = typeof (int);
  }

  /// <summary>The minimum value that could be set in the field input control</summary>
  public virtual int MinValue
  {
    get => this._MinValue;
    set => this._MinValue = value;
  }

  /// <summary>The maximum value that could be set in the field input control.</summary>
  public virtual int MaxValue
  {
    get => this._MaxValue;
    set => this._MaxValue = value;
  }

  /// <summary>The list of values for the field input control of the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> type.</summary>
  public virtual int[] AllowedValues
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
      return this._AllowedLabels != null && this._AllowedLabels.Length >= this._AllowedValues.Length ? this._AllowedLabels : ((IEnumerable<int>) this._AllowedValues).Select<int, string>((Func<int, string>) (value => value.ToString())).ToArray<string>();
    }
    set => this._AllowedLabels = value;
  }

  /// <summary>The list of images for the field input control of the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> type.</summary>
  public virtual string[] AllowedImages
  {
    get
    {
      if (this._AllowedValues == null)
        return (string[]) null;
      return this._AllowedImages != null && this._AllowedImages.Length >= this._AllowedValues.Length ? this._AllowedImages : (this._AllowedImages = new string[this._AllowedValues.Length]);
    }
  }

  /// <summary>A value that enables (if set to <see langword="true" />) editing of the value in the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> field input control.</summary>
  public virtual bool ExclusiveValues => this._ExclusiveValues;

  /// <summary>A value that enables (if set to <see langword="true" />) filtering by the empty value in the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> field input control.</summary>
  public virtual bool EmptyPossible
  {
    get => this._EmptyPossible;
    set => this._EmptyPossible = value;
  }

  /// <summary>The collection of values and labels for the field input control of the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> type.</summary>
  public Dictionary<int, string> ValueLabelDic
  {
    get
    {
      if (this._AllowedValues == null || this._AllowedLabels == null)
        return (Dictionary<int, string>) null;
      Dictionary<int, string> valueLabelDic = new Dictionary<int, string>(this._AllowedValues.Length);
      for (int index = 0; index < this._AllowedValues.Length; ++index)
        valueLabelDic.Add(this._AllowedValues[index], this._AllowedLabels[index]);
      return valueLabelDic;
    }
  }

  /// <summary>Tries to get value of the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> field input control by a label.</summary>
  internal bool TryGetListValue(string label, out object result)
  {
    if (this.HasFixedValuesList())
    {
      int[] allowedValues = this.AllowedValues;
      string[] allowedLabels = this.AllowedLabels;
      bool flag = this._DataType == typeof (short);
      for (int index = 0; index < allowedLabels.Length; ++index)
      {
        if (PXLocalesProvider.CollationComparer.Equals(allowedLabels[index], label))
        {
          result = flag ? (object) (short) allowedValues[index] : (object) allowedValues[index];
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
            result = flag ? (object) (short) allowedValues[index] : (object) allowedValues[index];
            return true;
          }
        }
      }
    }
    result = (object) null;
    return false;
  }

  /// <summary>Tries to get a label of the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> field input control by a value.</summary>
  internal bool TryGetListLabel(int value, out string result)
  {
    if (this.HasFixedValuesList())
    {
      int[] allowedValues = this.AllowedValues;
      string[] allowedLabels = this.AllowedLabels;
      for (int index = 0; index < allowedValues.Length; ++index)
      {
        if (allowedValues[index] == value && index < allowedLabels.Length && !string.IsNullOrEmpty(allowedLabels[index]))
        {
          result = allowedLabels[index];
          return true;
        }
      }
    }
    result = (string) null;
    return false;
  }

  /// <exclude />
  public static PXFieldState CreateInstance(
    object value,
    string fieldName,
    bool? isKey,
    int? required,
    int? minValue,
    int? maxValue,
    int[] allowedValues,
    string[] allowedLabels,
    System.Type dataType,
    int? defaultValue,
    string[] neutralLabels)
  {
    return PXIntState.CreateInstance(value, fieldName, isKey, required, minValue, maxValue, allowedValues, allowedLabels, dataType, defaultValue, neutralLabels, new bool?());
  }

  /// <exclude />
  public static PXFieldState CreateInstance(
    object value,
    string fieldName,
    bool? isKey,
    int? required,
    int? minValue,
    int? maxValue,
    int[] allowedValues,
    string[] allowedLabels,
    System.Type dataType,
    int? defaultValue,
    string[] neutralLabels = null,
    bool? nullable = null)
  {
    return PXIntState.CreateInstance(value, fieldName, isKey, required, minValue, maxValue, allowedValues, allowedLabels, new bool?(false), dataType, defaultValue, neutralLabels, nullable);
  }

  /// <exclude />
  public static PXFieldState CreateInstance(
    object value,
    string fieldName,
    bool? isKey,
    int? required,
    int? minValue,
    int? maxValue,
    int[] allowedValues,
    string[] allowedLabels,
    bool? exclusiveValues,
    System.Type dataType,
    int? defaultValue,
    string[] neutralLabels = null,
    bool? nullable = null)
  {
    if (!(value is PXIntState instance1))
    {
      PXFieldState instance = value as PXFieldState;
      if (dataType == (System.Type) null && instance != null && instance.DataType != typeof (object))
        dataType = instance.DataType;
      if (instance != null && instance.DataType != typeof (object) && instance.DataType != dataType)
      {
        if (isKey.HasValue)
          instance._IsKey = isKey.Value;
        return instance;
      }
      instance1 = new PXIntState(value);
    }
    if (dataType != (System.Type) null)
      instance1._DataType = dataType;
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
      instance1._DefaultValue = (object) Convert.ToInt32((object) defaultValue);
    if (nullable.HasValue)
      instance1._Nullable = nullable.Value;
    return (PXFieldState) instance1;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.PXStringState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>Provides data to set up the presentation of the input control for the <tt>string</tt> DAC field.</summary>
public class PXStringState : PXFieldState
{
  protected string _InputMask;
  protected string[] _AllowedValues;
  protected string[] _AllowedLabels;
  protected string[] _AllowedImages;
  protected Dictionary<string, string> _ExtraItems;
  internal string[] _NeutralLabels;
  protected bool _ExclusiveValues;
  protected bool _EmptyPossible;
  protected bool _IsUnicode = true;
  protected bool _MultiSelect;
  protected string _Language;
  protected char? _PromptChar;

  protected PXStringState(object value)
    : base(value)
  {
    if (!(value is PXStringState pxStringState))
      return;
    this._Length = pxStringState._Length;
  }

  /// <summary>A value that specifies how users enter data and how data is displayed.</summary>
  public virtual string InputMask
  {
    get => this._InputMask;
    set => this._InputMask = value;
  }

  /// <summary>The list of labels for the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> field input control.</summary>
  public virtual Dictionary<string, string> ExtraItems
  {
    get => this._ExtraItems;
    set => this._ExtraItems = value;
  }

  /// <summary>The list of values for the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> field input control.</summary>
  public virtual string[] AllowedValues
  {
    get => this._AllowedValues;
    set => this._AllowedValues = value;
  }

  /// <summary>The list of labels for the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> field input control.</summary>
  public virtual string[] AllowedLabels
  {
    get
    {
      if (this._AllowedValues == null)
        return (string[]) null;
      return this._AllowedLabels != null && this._AllowedLabels.Length >= this._AllowedValues.Length ? this._AllowedLabels : this._AllowedValues;
    }
    set => this._AllowedLabels = value;
  }

  /// <summary>The list of images for the <see cref="!:PXDropDown" /> field input control.</summary>
  public virtual string[] AllowedImages
  {
    get
    {
      if (this._AllowedValues == null)
        return (string[]) null;
      return this._AllowedImages != null && this._AllowedImages.Length >= this._AllowedValues.Length ? this._AllowedImages : (this._AllowedImages = new string[this._AllowedValues.Length]);
    }
    set => this._AllowedImages = value;
  }

  /// <summary>A value that enables (if set to <see langword="true" />) editing of the value in the <see cref="!:PXDropDown" /> field input control.</summary>
  public virtual bool ExclusiveValues => this._ExclusiveValues;

  /// <summary>A value that enables (if set to <see langword="true" />) filtering by the empty value in the <see cref="!:PXDropDown" /> field input control.</summary>
  public virtual bool EmptyPossible
  {
    get => this._EmptyPossible;
    set => this._EmptyPossible = value;
  }

  /// <summary>A value that indicates whether Unicode string content is supported.</summary>
  public virtual bool IsUnicode
  {
    get => this._IsUnicode;
    set => this._IsUnicode = value;
  }

  public virtual bool MultiSelect
  {
    get => this._MultiSelect;
    set => this._MultiSelect = value;
  }

  public virtual string Language
  {
    get => this._Language;
    set => this._Language = value;
  }

  /// <exclude />
  public virtual char? PromptChar
  {
    get => this._PromptChar;
    set => this._PromptChar = value;
  }

  /// <summary>The collection of values and labels for the field PXDropDown input control.</summary>
  public Dictionary<string, string> ValueLabelDic
  {
    get
    {
      if (this._AllowedValues == null || this._AllowedLabels == null)
        return (Dictionary<string, string>) null;
      Dictionary<string, string> valueLabelDic = new Dictionary<string, string>(this._AllowedValues.Length);
      for (int index = 0; index < this._AllowedValues.Length; ++index)
      {
        if (this._AllowedValues[index] != null)
          valueLabelDic.Add(this._AllowedValues[index], this._AllowedLabels[index]);
      }
      return valueLabelDic;
    }
  }

  /// <summary>A value that indicates whether the control for the field contains a password.
  /// If the value is set to <see langword="true" />, the value in the corresponding control is hidden with asterisk (*) symbols.
  /// </summary>
  public virtual bool IsPassword { get; set; }

  /// <summary>Tries to get value of the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> field input control by provided label.</summary>
  internal bool TryGetListValue(string label, out string result)
  {
    if (this.HasFixedValuesList())
    {
      string[] allowedValues = this.AllowedValues;
      string[] allowedLabels = this.AllowedLabels;
      for (int index = 0; index < allowedLabels.Length; ++index)
      {
        if (PXLocalesProvider.CollationComparer.Equals(allowedLabels[index], label))
        {
          result = allowedValues[index];
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
            result = allowedValues[index];
            return true;
          }
        }
      }
    }
    result = (string) null;
    return false;
  }

  /// <summary>Tries to get label of the <see cref="T:PX.Data.PXStringListAttribute">PXStringList</see> field input control by provided value.</summary>
  internal bool TryGetListLabel(string value, out string result)
  {
    if (this.HasFixedValuesList())
    {
      string[] allowedValues = this.AllowedValues;
      string[] allowedLabels = this.AllowedLabels;
      for (int index = 0; index < allowedValues.Length; ++index)
      {
        if (PXLocalesProvider.CollationComparer.Equals(allowedValues[index], value) && index < allowedLabels.Length)
        {
          result = !string.IsNullOrEmpty(allowedLabels[index]) ? allowedLabels[index] : allowedValues[index];
          return true;
        }
      }
    }
    result = (string) null;
    return false;
  }

  /// <exclude />
  public override string ToString()
  {
    if (this._Value == null)
      return string.Empty;
    if (!string.IsNullOrEmpty(this._InputMask) && this._Value is string)
      return Mask.Format(this._InputMask, (string) this._Value);
    string str;
    return this._AllowedValues != null && this._AllowedLabels != null && this._Value is string && this.ValueLabelDic.TryGetValue((string) this._Value, out str) ? str : this._Value.ToString();
  }

  /// <exclude />
  public static PXFieldState CreateInstance(
    object value,
    int? length,
    bool? isUnicode,
    string fieldName,
    bool? isKey,
    int? required,
    string inputMask,
    string[] allowedValues,
    string[] allowedLabels,
    bool? exclusiveValues,
    string defaultValue,
    string[] neutralLabels = null)
  {
    return PXStringState.CreateInstance(value, length, isUnicode, fieldName, isKey, required, inputMask, allowedValues, allowedLabels, exclusiveValues, defaultValue, neutralLabels, new char?());
  }

  /// <exclude />
  public static PXFieldState CreateInstance(
    object value,
    int? length,
    bool? isUnicode,
    string fieldName,
    bool? isKey,
    int? required,
    string inputMask,
    string[] allowedValues,
    string[] allowedLabels,
    bool? exclusiveValues,
    string defaultValue,
    string[] neutralLabels,
    char? promptChar)
  {
    switch (value)
    {
      case PXStringState instance1:
label_6:
        instance1._DataType = typeof (string);
        if (length.HasValue)
          instance1._Length = length.Value;
        if (isUnicode.HasValue)
          instance1._IsUnicode = isUnicode.Value;
        if (fieldName != null)
          instance1._FieldName = fieldName;
        if (isKey.HasValue)
          instance1._IsKey = isKey.Value;
        if (required.HasValue)
          instance1._Required += required.Value;
        if (inputMask != null)
          instance1._InputMask = inputMask;
        if (allowedValues != null)
          instance1._AllowedValues = allowedValues;
        if (allowedLabels != null)
          instance1._AllowedLabels = allowedLabels;
        if (neutralLabels != null)
          instance1._NeutralLabels = neutralLabels;
        if (exclusiveValues.HasValue)
          instance1._ExclusiveValues = exclusiveValues.Value;
        if (defaultValue != null)
          instance1._DefaultValue = (object) defaultValue;
        if (promptChar.HasValue)
          instance1._PromptChar = promptChar;
        return (PXFieldState) instance1;
      case PXFieldState instance2:
        if (instance2.DataType != typeof (object) && instance2.DataType != typeof (string))
        {
          if (isKey.HasValue)
            instance2._IsKey = isKey.Value;
          return instance2;
        }
        goto default;
      default:
        instance1 = new PXStringState(value);
        goto label_6;
    }
  }
}

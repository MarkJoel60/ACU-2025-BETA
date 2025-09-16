// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFilterRow
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace PX.Data;

/// <exclude />
[DebuggerDisplay("[{DataField}]: {((PX.Data.PXCondition)Condition).ToString()} {Value}|{Value2}")]
[Serializable]
public class PXFilterRow : ICloneable, IEquatable<PXFilterRow>, IFilterRow
{
  private object _value;
  private object _value2;
  private object _origValue;
  private object _origValue2;
  private object _tag;

  public string DataField { get; set; }

  public PXCondition Condition { get; set; }

  public object Value
  {
    get => this._value;
    set => this._value = value;
  }

  public object Value2
  {
    get => this._value2;
    set => this._value2 = value;
  }

  /// <summary>
  /// Use to override locale for demask filters in Export And Import scenarios
  ///  </summary>
  public string LocaleOverride { get; set; }

  public int OpenBrackets { get; set; }

  public int CloseBrackets { get; set; }

  public bool OrOperator { get; set; }

  public bool UseExt { get; set; }

  public FilterVariableType? Variable { get; set; }

  public PXCommandPreparingEventArgs.FieldDescription Description { get; set; }

  public PXCommandPreparingEventArgs.FieldDescription Description2 { get; set; }

  /// <summary>
  /// Setting this property to <c>true</c> will make ETH honor this row restriction on primary view,
  /// meaning that if no rows on view match this filter, no row will be emitted in primary view.
  /// </summary>
  internal bool RequireRowForPrimary { get; set; }

  public object OrigValue
  {
    get => this._origValue;
    set => this._origValue = value;
  }

  public object OrigValue2
  {
    get => this._origValue2;
    set => this._origValue2 = value;
  }

  public object Tag
  {
    get => this._tag;
    set => this._tag = value;
  }

  public PXFilterRow()
  {
  }

  public PXFilterRow(PXFilterRow filter)
  {
    this.DataField = filter.DataField;
    this.Condition = filter.Condition;
    this.Value = filter.Value;
    this.Value2 = filter.Value2;
    this.OpenBrackets = filter.OpenBrackets;
    this.CloseBrackets = filter.CloseBrackets;
    this.OrOperator = filter.OrOperator;
    this.RequireRowForPrimary = filter.RequireRowForPrimary;
    this.LocaleOverride = filter.LocaleOverride;
    this.Tag = filter.Tag;
  }

  public PXFilterRow(string dataField, PXCondition condition)
    : this(dataField, condition, (object) null, (object) null, new FilterVariableType?())
  {
  }

  public PXFilterRow(string dataField, PXCondition condition, object value)
    : this(dataField, condition, value, (object) null, new FilterVariableType?())
  {
  }

  public PXFilterRow(string dataField, PXCondition condition, object value, object value2)
    : this(dataField, condition, value, value2, new FilterVariableType?())
  {
  }

  public PXFilterRow(
    string dataField,
    PXCondition condition,
    object value,
    object value2,
    FilterVariableType? variable)
  {
    this.DataField = dataField;
    this.Condition = condition;
    this.Variable = variable;
    this.Value = PXFieldState.UnwrapValue(value);
    this.Value2 = PXFieldState.UnwrapValue(value2);
  }

  public virtual object Clone()
  {
    return (object) new PXFilterRow()
    {
      CloseBrackets = this.CloseBrackets,
      Condition = this.Condition,
      DataField = this.DataField,
      Description = (this.Description == null ? (PXCommandPreparingEventArgs.FieldDescription) null : (PXCommandPreparingEventArgs.FieldDescription) this.Description.Clone()),
      Description2 = (this.Description2 == null ? (PXCommandPreparingEventArgs.FieldDescription) null : (PXCommandPreparingEventArgs.FieldDescription) this.Description2.Clone()),
      OpenBrackets = this.OpenBrackets,
      OrigValue = this.OrigValue,
      OrigValue2 = this.OrigValue2,
      OrOperator = this.OrOperator,
      UseExt = this.UseExt,
      Value = this.Value,
      Value2 = this.Value2,
      Tag = this.Tag,
      RequireRowForPrimary = this.RequireRowForPrimary,
      LocaleOverride = this.LocaleOverride
    };
  }

  public override bool Equals(object obj) => obj is PXFilterRow other && this.Equals(other);

  public bool Equals(PXFilterRow other)
  {
    if (other == null)
      return false;
    if (this == other)
      return true;
    if (this.CloseBrackets != other.CloseBrackets || this.Condition != other.Condition || !(this.DataField == other.DataField) || !(this.LocaleOverride == other.LocaleOverride) || this.Description != other.Description && (this.Description == null || !this.Description.Equals(other.Description)) || this.Description2 != other.Description2 && (this.Description2 == null || !this.Description2.Equals(other.Description2)) || this.OpenBrackets != other.OpenBrackets || this.OrigValue != other.OrigValue && (this.OrigValue == null || !this.OrigValue.Equals(other.OrigValue)) && !this.SequenceEqual(this.OrigValue, other.OrigValue) || this.OrigValue2 != other.OrigValue2 && (this.OrigValue2 == null || !this.OrigValue2.Equals(other.OrigValue2)) && !this.SequenceEqual(this.OrigValue, other.OrigValue2) || this.OrOperator != other.OrOperator || this.UseExt != other.UseExt || this.Value != other.Value && (this.Value == null || !this.Value.Equals(other.Value)) && !this.SequenceEqual(this.Value, other.Value) || this.Value2 != other.Value2 && (this.Value2 == null || !this.Value2.Equals(other.Value2)) && !this.SequenceEqual(this.Value, other.Value2))
      return false;
    return this.Tag == other.Tag || this.Tag != null && this.Tag.Equals(other.Tag) || this.SequenceEqual(this.Tag, other.Tag);
  }

  private bool SequenceEqual(object data, object compareTo)
  {
    if (data == null && compareTo == null)
      return true;
    if (data == null || compareTo == null)
      return false;
    IStructuralEquatable structuralEquatable = data as IStructuralEquatable;
    IStructuralEquatable other = compareTo as IStructuralEquatable;
    return structuralEquatable != null && other != null && structuralEquatable.Equals((object) other, (IEqualityComparer) EqualityComparer<object>.Default);
  }
}

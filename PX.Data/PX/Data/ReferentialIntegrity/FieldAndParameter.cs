// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.FieldAndParameter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.ComponentModel;

#nullable disable
namespace PX.Data.ReferentialIntegrity;

/// <summary>Represents a field-and-parameter pair</summary>
[ImmutableObject(true)]
internal struct FieldAndParameter : IEquatable<FieldAndParameter>
{
  /// <summary>
  /// Initialize new instance of <see cref="T:PX.Data.ReferentialIntegrity.FieldAndParameter" />
  /// </summary>
  /// <param name="field">Actual field. Must be a <see cref="T:PX.Data.IBqlField" /> implementer</param>
  /// <param name="parameter">Parameter field. Must be a <see cref="T:PX.Data.IBqlField" /> implementer</param>
  /// <param name="parameterType">Parameter wrapper. Must be a <see cref="T:PX.Data.IBqlParameter" /> implementer</param>
  internal FieldAndParameter(System.Type field, System.Type parameter, System.Type parameterType)
  {
    if (field == (System.Type) null || !typeof (IBqlField).IsAssignableFrom(field))
      throw new ArgumentException(nameof (field));
    if (parameter == (System.Type) null || !typeof (IBqlField).IsAssignableFrom(parameter))
      throw new ArgumentException(nameof (parameter));
    if (parameterType != (System.Type) null && !typeof (IBqlParameter).IsAssignableFrom(parameterType))
      throw new ArgumentException(nameof (parameterType));
    this.Field = field;
    this.Parameter = parameter;
    this.ParameterType = parameterType;
    System.Type[] typeArray;
    if (!(this.ParameterType == (System.Type) null))
      typeArray = new System.Type[4]
      {
        this.Field,
        typeof (Equal<>),
        this.ParameterType,
        this.Parameter
      };
    else
      typeArray = new System.Type[3]
      {
        this.Field,
        typeof (Equal<>),
        this.Parameter
      };
    this.EqualSequence = typeArray;
  }

  internal System.Type Field { get; }

  internal System.Type Parameter { get; }

  internal System.Type ParameterType { get; }

  internal System.Type[] EqualSequence { get; }

  public override int GetHashCode()
  {
    System.Type field = this.Field;
    int num1 = ((object) field != null ? field.GetHashCode() : 0) * 397;
    System.Type parameter = this.Parameter;
    int hashCode1 = (object) parameter != null ? parameter.GetHashCode() : 0;
    int num2 = (num1 ^ hashCode1) * 397;
    System.Type parameterType = this.ParameterType;
    int hashCode2 = (object) parameterType != null ? parameterType.GetHashCode() : 0;
    return num2 ^ hashCode2;
  }

  public override bool Equals(object obj)
  {
    return obj != null && obj is FieldAndParameter other && this.Equals(other);
  }

  public bool Equals(FieldAndParameter other)
  {
    return this.Field == other.Field && this.Parameter == other.Parameter && this.ParameterType == other.ParameterType;
  }

  public static bool operator ==(FieldAndParameter left, FieldAndParameter right)
  {
    return left.Equals(right);
  }

  public static bool operator !=(FieldAndParameter left, FieldAndParameter right)
  {
    return !left.Equals(right);
  }
}

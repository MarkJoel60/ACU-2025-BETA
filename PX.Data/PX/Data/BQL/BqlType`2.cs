// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlType`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable enable
namespace PX.Data.BQL;

/// <exclude />
public abstract class BqlType<TBqlType, TCSharpType> where TBqlType : class, IBqlDataType
{
  /// <summary>
  /// Indicates whether the <paramref name="operandType" /> corresponds to a specific BQL type (e.g. <see cref="T:PX.Data.BQL.IBqlString" />, or <see cref="T:PX.Data.BQL.IBqlDecimal" />).
  /// </summary>
  public static bool MatchType(System.Type? operandType)
  {
    return operandType != (System.Type) null && typeof (IImplement<TBqlType>).IsAssignableFrom(operandType);
  }

  /// <summary>The base class for BQL constants.</summary>
  public abstract class Constant<TSelf>(TCSharpType value) : 
    BqlConstant<TSelf, TBqlType, TCSharpType>(value)
    where TSelf : BqlType<
    #nullable disable
    TBqlType, TCSharpType>.Constant<
    #nullable enable
    TSelf>, new()
  {
  }

  /// <summary>
  /// A BQL scalar operand: an expression involving multiple fields.
  /// </summary>
  public abstract class Operand<TSelf> : BqlOperand<TSelf, TBqlType> where TSelf : BqlType<
  #nullable disable
  TBqlType, TCSharpType>.Operand<
  #nullable enable
  TSelf>
  {
  }

  /// <summary>
  /// Represents a field declared in a DAC, that allows to use fluent comparisons.
  /// The field is either bound or not bound to a database column.
  /// </summary>
  public abstract class Field<TSelf> : BqlField<TSelf, TBqlType> where TSelf : BqlType<
  #nullable disable
  TBqlType, TCSharpType>.Field<
  #nullable enable
  TSelf>
  {
  }
}

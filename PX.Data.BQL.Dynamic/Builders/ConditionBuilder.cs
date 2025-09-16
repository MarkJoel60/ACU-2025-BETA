// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Builders.ConditionBuilder
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using PX.Data.BQL.Dynamic.Statements;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Data.BQL.Dynamic.Builders;

[ImmutableObject(true)]
public class ConditionBuilder
{
  public OnStatement On(UnaryStatement unary, params BinaryStatement[] binaries)
  {
    return new OnStatement(unary, (IEnumerable<BinaryStatement>) binaries);
  }

  public OnStatement On(
    OperandStatement leftOperand,
    CompareStatement compare,
    params BinaryStatement[] binaries)
  {
    return new OnStatement(leftOperand, compare, (IEnumerable<BinaryStatement>) binaries);
  }

  public WhereStatement Where(UnaryStatement unary, params BinaryStatement[] binaries)
  {
    return new WhereStatement(unary, (IEnumerable<BinaryStatement>) binaries);
  }

  public WhereStatement Where(
    OperandStatement leftOperand,
    CompareStatement compare,
    params BinaryStatement[] binaries)
  {
    return new WhereStatement(leftOperand, compare, (IEnumerable<BinaryStatement>) binaries);
  }

  public BracketsStatement Brackets(UnaryStatement unary, params BinaryStatement[] binaries)
  {
    return new BracketsStatement(unary, binaries);
  }

  public BracketsStatement Brackets(
    OperandStatement leftOperand,
    CompareStatement compare,
    params BinaryStatement[] binaries)
  {
    return new BracketsStatement(leftOperand, compare, binaries);
  }

  public UnaryStatement RawUnary<TUnary>() where TUnary : IBqlUnary
  {
    return this.RawUnary(typeof (TUnary));
  }

  public UnaryStatement RawUnary(Type customUnary) => new UnaryStatement(customUnary);

  public BinaryStatement And(UnaryStatement unary) => new BinaryStatement(true, unary);

  public BinaryStatement And(OperandStatement leftOperand, CompareStatement compare)
  {
    return new BinaryStatement(true, leftOperand, compare);
  }

  public BinaryStatement Or(UnaryStatement unary) => new BinaryStatement(false, unary);

  public BinaryStatement Or(OperandStatement leftOperand, CompareStatement compare)
  {
    return new BinaryStatement(false, leftOperand, compare);
  }

  public ParameterStatement Current<TField>(bool withDefaulting = true) where TField : IBqlField
  {
    return this.Current(typeof (TField), withDefaulting);
  }

  public ParameterStatement Current(Type field, bool withDefaulting = true)
  {
    return new ParameterStatement(field, ParameterType.Current, withDefaulting);
  }

  public ParameterStatement Required<TField>() where TField : IBqlField
  {
    return this.Required(typeof (TField));
  }

  public ParameterStatement Required(Type param)
  {
    return new ParameterStatement(param, ParameterType.Required);
  }

  public ParameterStatement Optional<TField>(bool withDefaulting = true) where TField : IBqlField
  {
    return this.Optional(typeof (TField), withDefaulting);
  }

  public ParameterStatement Optional(Type field, bool withDefaulting = true)
  {
    return new ParameterStatement(field, ParameterType.Optional, withDefaulting);
  }

  public ConstantStatement Const<TConst>() where TConst : IConstant => this.Const(typeof (TConst));

  public ConstantStatement Const(Type constSource) => new ConstantStatement(constSource);

  public FieldStatement Field<TField>() where TField : IBqlField => this.Field(typeof (TField));

  public FieldStatement Field(Type field) => new FieldStatement(field);

  public OperandStatement RawOperand<TRawOperand>() where TRawOperand : IBqlOperand
  {
    return this.RawOperand(typeof (TRawOperand));
  }

  public OperandStatement RawOperand(Type rawOperand)
  {
    return (OperandStatement) new CustomOperand(rawOperand);
  }

  public CompareStatement Equal(OperandStatement rightOperand)
  {
    return new CompareStatement(CompareType.Equal, rightOperand);
  }

  public CompareStatement NotEqual(OperandStatement rightOperand)
  {
    return new CompareStatement(CompareType.NotEqual, rightOperand);
  }

  public CompareStatement Less(OperandStatement rightOperand)
  {
    return new CompareStatement(CompareType.Less, rightOperand);
  }

  public CompareStatement Greater(OperandStatement rightOperand)
  {
    return new CompareStatement(CompareType.Greater, rightOperand);
  }

  public CompareStatement LessEqual(OperandStatement rightOperand)
  {
    return new CompareStatement(CompareType.LessEqual, rightOperand);
  }

  public CompareStatement GreaterEqual(OperandStatement rightOperand)
  {
    return new CompareStatement(CompareType.GreaterEqual, rightOperand);
  }

  public CompareStatement IsNull()
  {
    return new CompareStatement(CompareType.IsNull, (OperandStatement) null);
  }

  public CompareStatement IsNotNull()
  {
    return new CompareStatement(CompareType.IsNotNull, (OperandStatement) null);
  }

  public CompareStatement Like(OperandStatement rightOperand)
  {
    return new CompareStatement(CompareType.Like, rightOperand);
  }

  public CompareStatement NotLike(OperandStatement rightOperand)
  {
    return new CompareStatement(CompareType.NotLike, rightOperand);
  }

  public CompareStatement RawCompare<TRawComparison>() where TRawComparison : IBqlComparison
  {
    return this.RawCompare(typeof (TRawComparison));
  }

  public CompareStatement RawCompare(Type rawComparison) => new CompareStatement(rawComparison);
}

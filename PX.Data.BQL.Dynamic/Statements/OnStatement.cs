// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.OnStatement
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Data.BQL.Dynamic.Statements;

[ImmutableObject(true)]
public class OnStatement : IStatement
{
  private readonly UnaryStatement _unary;

  private OnStatement(UnaryStatement unary) => this._unary = unary;

  internal OnStatement(UnaryStatement unary, IEnumerable<BinaryStatement> binaries)
    : this(new UnaryStatement(true, unary, binaries))
  {
  }

  internal OnStatement(
    OperandStatement leftOperandStatement,
    CompareStatement compareStatement,
    IEnumerable<BinaryStatement> binaries)
    : this(new UnaryStatement(true, leftOperandStatement, compareStatement, binaries))
  {
  }

  internal static OnStatement FromRawStatement(Type condition)
  {
    condition.VerifyRawStatement<IBqlOn>(nameof (condition));
    UnaryStatement unaryStatement = UnaryStatement.FromRawStatement(condition, true);
    return !unaryStatement.IsUnary ? new OnStatement(unaryStatement.LeftOperand, unaryStatement.Compare, unaryStatement.Binaries) : new OnStatement(unaryStatement.Unary, unaryStatement.Binaries);
  }

  internal OnStatement AppendBinary(BinaryStatement binary)
  {
    if (!this._unary.IsUnary)
    {
      OperandStatement leftOperand = this._unary.LeftOperand;
      CompareStatement compare = this._unary.Compare;
      BinaryStatement[] binaries;
      if (!this._unary.HasBinaries)
        binaries = new BinaryStatement[1]{ binary };
      else
        binaries = this._unary.Binaries.Concat<BinaryStatement>((IEnumerable<BinaryStatement>) new BinaryStatement[1]
        {
          binary
        }).ToArray<BinaryStatement>();
      return new OnStatement(leftOperand, compare, (IEnumerable<BinaryStatement>) binaries);
    }
    UnaryStatement unary = this._unary.Unary;
    BinaryStatement[] binaries1;
    if (!this._unary.HasBinaries)
      binaries1 = new BinaryStatement[1]{ binary };
    else
      binaries1 = this._unary.Binaries.Concat<BinaryStatement>((IEnumerable<BinaryStatement>) new BinaryStatement[1]
      {
        binary
      }).ToArray<BinaryStatement>();
    return new OnStatement(unary, (IEnumerable<BinaryStatement>) binaries1);
  }

  public Type Eval() => this._unary.Eval();
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.WhereStatement
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
public class WhereStatement : UnaryStatement
{
  internal WhereStatement(UnaryStatement unary, IEnumerable<BinaryStatement> binaries)
    : base(false, unary, binaries)
  {
  }

  internal WhereStatement(
    OperandStatement leftOperandStatement,
    CompareStatement compareStatement,
    IEnumerable<BinaryStatement> binaries)
    : base(false, leftOperandStatement, compareStatement, binaries)
  {
  }

  internal static WhereStatement FromRawStatement(Type condition, bool topLevel = false)
  {
    condition.VerifyRawStatement<IBqlWhere>(nameof (condition));
    UnaryStatement unaryStatement = UnaryStatement.FromRawStatement(condition, topLevel);
    return !unaryStatement.IsUnary ? new WhereStatement(unaryStatement.LeftOperand, unaryStatement.Compare, unaryStatement.Binaries) : new WhereStatement(unaryStatement.Unary, unaryStatement.Binaries);
  }

  internal WhereStatement AppendBinary(BinaryStatement binary)
  {
    if (!this.IsUnary)
    {
      OperandStatement leftOperand = this.LeftOperand;
      CompareStatement compare = this.Compare;
      BinaryStatement[] binaries;
      if (!this.HasBinaries)
        binaries = new BinaryStatement[1]{ binary };
      else
        binaries = this.Binaries.Concat<BinaryStatement>((IEnumerable<BinaryStatement>) new BinaryStatement[1]
        {
          binary
        }).ToArray<BinaryStatement>();
      return new WhereStatement(leftOperand, compare, (IEnumerable<BinaryStatement>) binaries);
    }
    UnaryStatement unary = this.Unary;
    BinaryStatement[] binaries1;
    if (!this.HasBinaries)
      binaries1 = new BinaryStatement[1]{ binary };
    else
      binaries1 = this.Binaries.Concat<BinaryStatement>((IEnumerable<BinaryStatement>) new BinaryStatement[1]
      {
        binary
      }).ToArray<BinaryStatement>();
    return new WhereStatement(unary, (IEnumerable<BinaryStatement>) binaries1);
  }
}

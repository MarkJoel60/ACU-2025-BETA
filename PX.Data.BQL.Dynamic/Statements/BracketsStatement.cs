// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.BracketsStatement
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Data.BQL.Dynamic.Statements;

[ImmutableObject(true)]
public class BracketsStatement : UnaryStatement
{
  internal BracketsStatement(UnaryStatement unary, BinaryStatement[] binaries)
    : base(false, unary, (IEnumerable<BinaryStatement>) binaries)
  {
  }

  internal BracketsStatement(
    OperandStatement leftOperandStatement,
    CompareStatement compareStatement,
    BinaryStatement[] binaries)
    : base(false, leftOperandStatement, compareStatement, (IEnumerable<BinaryStatement>) binaries)
  {
  }
}

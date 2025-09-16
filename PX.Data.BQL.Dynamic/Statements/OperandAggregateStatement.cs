// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.OperandAggregateStatement
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using System;
using System.ComponentModel;

#nullable disable
namespace PX.Data.BQL.Dynamic.Statements;

[ImmutableObject(true)]
public class OperandAggregateStatement : ChainedAggregateStatement
{
  internal OperandAggregateStatement(AggregateType function, Type operand)
    : base(function, operand)
  {
    if ((object) operand == null)
      return;
    operand.VerifyRawStatement<IBqlAggregateOperand>(nameof (operand));
  }

  protected override Type OperandType { get; } = typeof (IBqlAggregateOperand);
}

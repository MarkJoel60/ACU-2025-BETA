// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.OperandStatement
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using System;

#nullable disable
namespace PX.Data.BQL.Dynamic.Statements;

public abstract class OperandStatement : IStatement
{
  public abstract Type Eval();

  public static OperandStatement FromRawStatement(Type operand)
  {
    operand.VerifyRawStatement<IBqlOperand>(nameof (operand));
    if (typeof (IBqlField).IsAssignableFrom(operand))
      return (OperandStatement) new FieldStatement(operand);
    return typeof (IConstant).IsAssignableFrom(operand) ? (OperandStatement) new ConstantStatement(operand) : (OperandStatement) ParameterStatement.FromRawStatement(operand) ?? (OperandStatement) new CustomOperand(operand);
  }
}

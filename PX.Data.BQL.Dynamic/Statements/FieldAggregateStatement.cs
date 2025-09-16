// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.FieldAggregateStatement
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using System;
using System.ComponentModel;

#nullable disable
namespace PX.Data.BQL.Dynamic.Statements;

[ImmutableObject(true)]
public class FieldAggregateStatement : ChainedAggregateStatement
{
  internal FieldAggregateStatement(AggregateType function, Type field)
    : base(function, field)
  {
    if (function == AggregateType.Count || (object) field == null)
      return;
    field.VerifyRawStatement<IBqlField>(nameof (field));
  }

  protected override Type OperandType { get; } = typeof (IBqlField);
}

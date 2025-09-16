// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.CustomOperand
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using System;
using System.ComponentModel;

#nullable disable
namespace PX.Data.BQL.Dynamic.Statements;

[ImmutableObject(true)]
public class CustomOperand : OperandStatement
{
  private readonly Type _customOperand;

  internal CustomOperand(Type customOperand)
  {
    customOperand.VerifyRawStatement<IBqlOperand>(nameof (customOperand));
    this._customOperand = customOperand;
  }

  public override Type Eval() => this._customOperand;
}

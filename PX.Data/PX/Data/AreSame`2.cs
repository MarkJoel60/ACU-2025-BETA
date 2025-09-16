// Decompiled with JetBrains decompiler
// Type: PX.Data.AreSame`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Indicates whether operands are equal or both NULL.
/// If you use a parameter placeholder such as <see cref="T:PX.Data.Required`1" />
/// as one of operands, you must pass such parameter value twice!
/// If both operands are parameter placeholders, you must pass
/// their values as shown: operand1, operand2, operand1, operand2
/// </summary>
public sealed class AreSame<TOperand1, TOperand2> : CustomPredicate
  where TOperand1 : IBqlOperand
  where TOperand2 : IBqlOperand
{
  public AreSame()
    : base((IBqlUnary) new Where<TOperand1, Equal<TOperand2>, Or<TOperand1, IsNull, And<TOperand2, IsNull>>>())
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Compare`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.BQL;

/// <summary>
/// Turns a comparison into <see cref="T:PX.Data.IBqlUnary" /> without surrounding it with parenthesis.
/// </summary>
public sealed class Compare<TOperand, TComparison> : CustomPredicate, IBqlPredicateChain
  where TOperand : IBqlOperand
  where TComparison : IBqlComparison, new()
{
  public Compare()
    : base((IBqlUnary) new WhereNp<TOperand, TComparison>())
  {
  }

  IBqlUnary IBqlPredicateChain.GetContainedPredicate() => this.Original;

  bool IBqlPredicateChain.ContainsOperandWithComparison() => true;

  IBqlBinary IBqlPredicateChain.GetNextPredicate() => (IBqlBinary) null;

  bool IBqlPredicateChain.UseParenthesis() => false;
}

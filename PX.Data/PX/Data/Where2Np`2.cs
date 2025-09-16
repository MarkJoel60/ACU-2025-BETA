// Decompiled with JetBrains decompiler
// Type: PX.Data.Where2Np`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
internal sealed class Where2Np<UnaryOperator, NextOperator> : WhereBase<UnaryOperator, NextOperator>
  where UnaryOperator : IBqlUnary, new()
  where NextOperator : IBqlBinary, new()
{
  public Where2Np()
  {
  }

  public Where2Np(IBqlUnary un, IBqlBinary next)
    : base(un, next)
  {
  }

  public override bool UseParenthesis() => false;
}

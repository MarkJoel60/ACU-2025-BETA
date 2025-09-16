// Decompiled with JetBrains decompiler
// Type: PX.Data.LikeFullTextSubst`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
internal class LikeFullTextSubst<Operand, TopCount> : ComparisonBase<Operand>
  where Operand : IBqlOperand
  where TopCount : IBqlOperand
{
  public LikeFullTextSubst()
    : base("LIKE", true)
  {
  }

  protected override bool? verifyCore(object val, object value)
  {
    return Like<Operand>.CheckLike(val as string, value as string);
  }

  protected override bool isBypass(object val) => false;
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.LessEqual`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Checks if the preceding operand is less or equal to <tt>Operand</tt>.
/// </summary>
/// <typeparam name="Operand">The operand to compare to.</typeparam>
public class LessEqual<Operand> : ComparisonBase<Operand> where Operand : IBqlOperand
{
  protected override bool? verifyCore(object val, object value)
  {
    return new bool?(this.collationComparer.Compare(val, value) <= 0);
  }

  protected override bool isBypass(object val) => !(val is IComparable);

  /// <exclude />
  public LessEqual()
    : base("<=", true)
  {
  }
}

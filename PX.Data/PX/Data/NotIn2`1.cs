// Decompiled with JetBrains decompiler
// Type: PX.Data.NotIn2`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Checks if the preceding operand does not match any value in the results of the Search-based statement that is defined by the operand. The condition is true if
/// the <tt>Search&lt;&gt;</tt> result set does not contain a value that is equal to the preceding operand.</summary>
public class NotIn2<Operand> : InBase<Operand> where Operand : IBqlSearch, IBqlCreator
{
  protected override bool IsNegative => true;

  [Obsolete]
  protected override string SqlOperator => " NOT IN ";
}

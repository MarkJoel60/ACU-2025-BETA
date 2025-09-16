// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.IBqlHavingCondition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.BQL;

/// <summary>
/// Defines a condition that could be used only in <see cref="T:PX.Data.Having`1" /> clause,
/// but not in <see cref="T:PX.Data.Where`1" />, nor in <see cref="T:PX.Data.On`1" /> clauses.
/// </summary>
public interface IBqlHavingCondition
{
  IBqlUnary MatchingCondition { get; }
}

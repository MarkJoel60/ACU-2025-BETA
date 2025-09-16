// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Brackets`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.BQL;

/// <summary>
/// Brackets that allow to use chainable conditions in the <see cref="T:PX.Data.Where`1" /> and in the <see cref="T:PX.Data.On`1" /> clauses.
/// Use <see cref="T:PX.Data.BQL.Brackets2`1" /> to embrace conditions in the <see cref="T:PX.Data.Having`1" /> clause.
/// </summary>
public sealed class Brackets<TCondition> : BqlChainableCondition<TCondition> where TCondition : IBqlUnary, new()
{
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.UnionAll`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data.BQL;

public sealed class UnionAll<TBqlTableMap, NextUnion> : UnionBase<TBqlTableMap, NextUnion>
  where TBqlTableMap : IBqlTableMapper, new()
  where NextUnion : IBqlUnion, new()
{
  protected override Unioner.UnionType UnionType { get; } = Unioner.UnionType.UNIONALL;
}

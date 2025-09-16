// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.UnionAll`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data.BQL;

public sealed class UnionAll<TBqlTableMap> : UnionBase<TBqlTableMap, BqlNone> where TBqlTableMap : IBqlTableMapper, new()
{
  protected override Unioner.UnionType UnionType { get; } = Unioner.UnionType.UNIONALL;
}

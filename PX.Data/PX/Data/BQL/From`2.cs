// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.From`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data.BQL;

/// <summary>A <tt>From</tt> expression with the <tt>Union</tt> clause.</summary>
/// <typeparam name="TBqlTableMap">A class that contains the mapping of the fields of the original and the resulting or shared DAC class involved in the <tt>UNION</tt> or <tt>UNION ALL</tt> operation.</typeparam>
/// <typeparam name="NextUnion">The next <tt>UNION</tt> or <tt>UNION ALL</tt> clause.</typeparam>
public sealed class From<TBqlTableMap, NextUnion> : FromMappedTableBase<TBqlTableMap, NextUnion>
  where TBqlTableMap : IBqlTableMapper, new()
  where NextUnion : IBqlUnion, new()
{
  protected override Unioner.UnionType UnionType { get; }
}

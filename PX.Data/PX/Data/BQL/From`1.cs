// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.From`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data.BQL;

/// <summary>A <tt>From</tt> expression.</summary>
/// <typeparam name="TBqlTableMap">A class that contains the mapping of the fields of the original and the resulting or shared DAC class.</typeparam>
public sealed class From<TBqlTableMap> : FromMappedTableBase<TBqlTableMap, BqlNone> where TBqlTableMap : IBqlTableMapper, new()
{
  protected override Unioner.UnionType UnionType { get; }
}

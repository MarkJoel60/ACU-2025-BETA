// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlTableMapper`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.BQL;

public class BqlTableMapper<BqlTable, TMapTo> : IBqlTableMapper
  where BqlTable : IBqlTable
  where TMapTo : IBqlFieldMappingResolver, new()
{
  private IBqlFieldMappingResolver _mapTo;

  public IBqlFieldMappingResolver GetMapper()
  {
    return this._mapTo == null ? (this._mapTo = (IBqlFieldMappingResolver) new TMapTo()) : this._mapTo;
  }

  public System.Type TableType { get; } = typeof (BqlTable);
}

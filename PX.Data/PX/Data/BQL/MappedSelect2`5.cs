// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.MappedSelect2`5
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.BQL;

public class MappedSelect2<MappedToTable, TMappedTable, TJoin, TWhere, TOrderBy> : 
  MappedSelect<MappedToTable, TMappedTable, TJoin, TWhere, BqlNone, TOrderBy>
  where MappedToTable : IBqlTable, new()
  where TMappedTable : IMappedTable, new()
  where TJoin : IBqlJoin, new()
  where TWhere : IBqlWhere, new()
  where TOrderBy : IBqlOrderBy, new()
{
}

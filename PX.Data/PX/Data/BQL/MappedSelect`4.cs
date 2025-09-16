// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.MappedSelect`4
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.BQL;

public class MappedSelect<MappedToTable, TMappedTable, TWhere, TOrderBy> : 
  MappedSelect<MappedToTable, TMappedTable, BqlNone, TWhere, BqlNone, TOrderBy>
  where MappedToTable : IBqlTable, new()
  where TMappedTable : IMappedTable, new()
  where TWhere : IBqlWhere, new()
  where TOrderBy : IBqlOrderBy, new()
{
}

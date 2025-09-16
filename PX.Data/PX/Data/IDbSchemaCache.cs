// Decompiled with JetBrains decompiler
// Type: PX.Data.IDbSchemaCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.Model.Entities;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public interface IDbSchemaCache
{
  string DatabaseName { get; }

  string DatabaseHost { get; }

  IEnumerable<string> GetTableNames();

  TableHeader GetTableHeader(string tableName);

  TableIndex GetFullTextIndexOnTable(string table);

  bool TableExists(string table);

  void InvalidateAll();

  void InvalidateTables(List<string> tablesToInvalidate);

  void InvalidateTable(string singleTableName);

  companySetting getTableSetting(string tableName, bool selectablesHaveNoCompanyId);

  IList<TableHeader> getAllTableHeaders();

  bool IsSchemaLoaded();
}

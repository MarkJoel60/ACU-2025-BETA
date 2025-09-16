// Decompiled with JetBrains decompiler
// Type: PX.Data.BqlUnaryFullTextExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public static class BqlUnaryFullTextExtensions
{
  public static BqlFullTextRenderingMethod getFullTextSupportMode(
    System.Type field,
    PXDatabaseProvider pXDatabaseProvider,
    PXGraph graph,
    List<System.Type> tables,
    BqlCommand.Selection selection)
  {
    string contentFieldName = BqlUnaryFullTextExtensions.GetContentFieldName(field, graph, tables, selection);
    return pXDatabaseProvider.getFullTextRenderingMethod(BqlUnaryFullTextExtensions.getTableName(field, tables), contentFieldName);
  }

  public static string GetContentFieldName(
    System.Type contentField,
    PXGraph graph,
    List<System.Type> tables,
    BqlCommand.Selection selection)
  {
    SQLExpression singleExpression = BqlCommand.GetSingleExpression(contentField, graph, tables, selection, BqlCommand.FieldPlace.Condition);
    return singleExpression is Column column ? column.Name : singleExpression.SQLQuery(graph.SqlDialect.GetConnection()).ToString();
  }

  public static string getTableName(System.Type contentField, List<System.Type> tables)
  {
    return BqlCommand.FindRealTableForType(tables, BqlCommand.GetItemType(contentField)).Name;
  }

  public static string getTableAlias(System.Type contentField, List<System.Type> tables)
  {
    return BqlUnaryFullTextExtensions.getTableAlias(BqlUnaryFullTextExtensions.getTableName(contentField, tables));
  }

  public static string getTableAlias(string tableName) => "ftt_" + tableName;
}

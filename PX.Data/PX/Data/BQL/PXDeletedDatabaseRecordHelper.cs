// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.PXDeletedDatabaseRecordHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data.BQL;

public static class PXDeletedDatabaseRecordHelper
{
  public static void InsertDeletedDatabaseRecordIfNeeded(System.Type table, PXCache cache)
  {
    PXGraph graph = cache.Graph;
    if ((graph != null ? (!graph.IsDacBasedOdataAPI ? 1 : 0) : 1) != 0)
      return;
    companySetting settings;
    PXDatabase.Provider.getCompanyID(PXDatabaseRecordStatusHelper.GetTableName(cache) ?? table.Name, out settings);
    if (settings.Deleted == null || cache.Fields.Contains(settings.Deleted))
      return;
    cache.Fields.Add(settings.Deleted);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cache.CommandPreparingEvents.Add(settings.Deleted, PXDeletedDatabaseRecordHelper.\u003C\u003EO.\u003C0\u003E__CommandPreparing ?? (PXDeletedDatabaseRecordHelper.\u003C\u003EO.\u003C0\u003E__CommandPreparing = new PXCommandPreparing(PXDeletedDatabaseRecordHelper.CommandPreparing)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cache.RowSelectingWhileReading += PXDeletedDatabaseRecordHelper.\u003C\u003EO.\u003C1\u003E__RowSelecting ?? (PXDeletedDatabaseRecordHelper.\u003C\u003EO.\u003C1\u003E__RowSelecting = new PXRowSelecting(PXDeletedDatabaseRecordHelper.RowSelecting));
  }

  private static void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    string tableName = e.Table?.Name ?? PXDatabaseRecordStatusHelper.GetTableName(sender);
    if (sender.BqlSelect != null)
      e.BqlTable = sender.BqlTable;
    companySetting settings;
    PXDatabase.Provider.getCompanyID(tableName, out settings);
    e.Expr = (SQLExpression) new Column(settings.Deleted ?? "DeletedDatabaseRecord", tableName, PXDbType.Bit);
    e.ExcludeFromInsertUpdateDeleteStatements();
  }

  private static void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null && e.Record != null)
    {
      bool? boolean = e.Record.GetBoolean(e.Position);
      if (boolean.HasValue)
        sender.SetDeletedRecord(e.Row, boolean.Value);
    }
    ++e.Position;
  }

  private static bool HasDeletedDatabaseRecordSupport(
    this PXCache cache,
    System.Type table,
    out string deletedDatabaseRecordFieldName)
  {
    System.Type type = table;
    if ((object) type == null)
      type = cache.GetItemType();
    table = type;
    companySetting settings;
    PXDatabase.Provider.getCompanyID(PXDatabaseRecordStatusHelper.GetTableName(cache) ?? table.Name, out settings);
    deletedDatabaseRecordFieldName = settings.Deleted;
    return settings.Deleted != null;
  }

  public static bool IsDeletedDatabaseRecordNeeded(
    System.Type table,
    PXCache cache,
    out string deletedDatabaseRecordFieldName)
  {
    return cache.HasDeletedDatabaseRecordSupport(table, out deletedDatabaseRecordFieldName) && cache.CommandPreparingEvents.ContainsKey(deletedDatabaseRecordFieldName);
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.PXDatabaseRecordStatusHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.BQL;

internal static class PXDatabaseRecordStatusHelper
{
  public static bool HasRecordStatusSupport(this PXCache cache, System.Type table = null)
  {
    return cache.HasRecordStatusSupport(table, out string _);
  }

  public static bool HasRecordStatusSupport(
    this PXCache cache,
    System.Type table,
    out string recordStatusFieldName)
  {
    System.Type type = table;
    if ((object) type == null)
      type = cache.GetItemType();
    table = type;
    companySetting settings;
    PXDatabase.Provider.getCompanyID(PXDatabaseRecordStatusHelper.GetTableName(cache) ?? table.Name, out settings);
    recordStatusFieldName = settings.RecordStatus;
    return settings.RecordStatus != null;
  }

  public static void InsertDatabaseRecordStatusIfNeeded(
    System.Type table,
    PXCache cache,
    BqlCommand.Selection selection)
  {
    companySetting settings;
    PXDatabase.Provider.getCompanyID(PXDatabaseRecordStatusHelper.GetTableName(cache) ?? table.Name, out settings);
    if (settings.RecordStatus == null || cache.CommandPreparingEvents.ContainsKey(settings.RecordStatus))
      return;
    cache.Fields.Add(settings.RecordStatus);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cache.CommandPreparingEvents.Add(settings.RecordStatus, PXDatabaseRecordStatusHelper.\u003C\u003EO.\u003C0\u003E__CommandPreparing ?? (PXDatabaseRecordStatusHelper.\u003C\u003EO.\u003C0\u003E__CommandPreparing = new PXCommandPreparing(PXDatabaseRecordStatusHelper.CommandPreparing)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cache.RowSelectingWhileReading += PXDatabaseRecordStatusHelper.\u003C\u003EO.\u003C1\u003E__RowSelecting ?? (PXDatabaseRecordStatusHelper.\u003C\u003EO.\u003C1\u003E__RowSelecting = new PXRowSelecting(PXDatabaseRecordStatusHelper.RowSelecting));
  }

  public static void InsertDatabaseRecordStatusIfNeeded(System.Type table, PXCache cache)
  {
    PXDatabaseRecordStatusHelper.InsertDatabaseRecordStatusIfNeeded(table, cache, (BqlCommand.Selection) null);
  }

  public static bool IsDatabaseRecordStatusNeeded(
    System.Type table,
    PXCache cache,
    out string recordStatusFieldName)
  {
    return cache.HasRecordStatusSupport(table, out recordStatusFieldName) && cache.CommandPreparingEvents.ContainsKey(recordStatusFieldName);
  }

  private static void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    string tableName = e.Table?.Name ?? PXDatabaseRecordStatusHelper.GetTableName(sender);
    if (sender.BqlSelect != null)
      e.BqlTable = sender.BqlTable;
    companySetting settings;
    PXDatabase.Provider.getCompanyID(tableName, out settings);
    e.Expr = (SQLExpression) new Column(settings.RecordStatus ?? "DatabaseRecordStatus", tableName, PXDbType.Int);
    e.ExcludeFromInsertUpdateDeleteStatements();
  }

  private static void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null && e.Record != null)
    {
      int? int32 = e.Record.GetInt32(e.Position);
      if (int32.HasValue)
        sender.SetArchived(e.Row, int32.Value == 1);
    }
    ++e.Position;
  }

  internal static string GetTableName(PXCache cache)
  {
    return cache.BqlSelect == null ? PXDatabaseRecordStatusHelper.GetFirstChangedTableName(cache) : PXDatabaseRecordStatusHelper.GetFirstChangedTableNameForProjection(cache.BqlSelect);
  }

  private static string GetFirstChangedTableNameForProjection(BqlCommand projection)
  {
    return ((IEnumerable<System.Type>) projection.GetTables()).Select<System.Type, string>((Func<System.Type, string>) (t => BqlCommand.GetTableName(t))).FirstOrDefault<string>();
  }

  private static string GetFirstChangedTableName(PXCache sender)
  {
    return PXDatabaseRecordStatusHelper.GetAllTablesNames(sender.BqlTable, sender.Graph).FirstOrDefault<string>();
  }

  private static IEnumerable<string> GetAllTablesNames(System.Type type, PXGraph graph)
  {
    if (!typeof (IBqlTable).IsAssignableFrom(type))
      return (IEnumerable<string>) new string[1]
      {
        type.Name
      };
    PXCache cach = graph.Caches[type];
    if (cach.BqlSelect != null)
      return ((IEnumerable<System.Type>) cach.BqlSelect.GetTables()).Select<System.Type, string>((Func<System.Type, string>) (t => BqlCommand.GetTableName(t)));
    System.Type itemType = cach.GetItemType();
    return (IEnumerable<string>) new string[1]
    {
      BqlCommand.GetTableName(itemType == type || !itemType.IsDefined(typeof (PXTableNameAttribute), false) || !((PXTableNameAttribute) itemType.GetCustomAttributes(typeof (PXTableNameAttribute), false)[0]).IsActive ? type : itemType)
    };
  }
}

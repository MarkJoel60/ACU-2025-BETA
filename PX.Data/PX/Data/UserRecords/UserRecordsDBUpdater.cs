// Decompiled with JetBrains decompiler
// Type: PX.Data.UserRecords.UserRecordsDBUpdater
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.UserRecords.FavoriteRecords;
using PX.Data.UserRecords.RecentlyVisitedRecords;
using PX.DbServices.Commands;
using PX.DbServices.Commands.Data;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using Serilog;
using Serilog.Events;
using SerilogTimings.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

#nullable disable
namespace PX.Data.UserRecords;

/// <summary>
/// A user records database updater class. Updates a batch of user records in the database when <see cref="T:PX.Data.PXTransactionScope" /> completes.
/// This functionality works together with <see cref="T:PX.Data.PXSearchableAttribute" /> functionality. Inside the attribute on DAC's row persisted event we add an entry to
/// a special list inside the root transaction scope. This entry <see cref="T:PX.Data.UserRecords.ModifiedDacEntryForUserRecordsUpdate" /> contains information required to update user records at the end of transaction,
/// similar to a mechanism Audit uses. The reason for a special treatment is a high number of DB deadlocks caused by the concurrent access to the VisitedRecord DB table
/// when we process each record separately inside <see cref="T:PX.Data.PXSearchableAttribute" />.
/// </summary>
internal class UserRecordsDBUpdater : IUserRecordsDBUpdater
{
  private readonly ILogger _logger;

  public UserRecordsDBUpdater(ILogger logger) => this._logger = logger;

  public void UpdateUserRecords(
    IReadOnlyCollection<ModifiedDacEntryForUserRecordsUpdate> modifiedDacEntries)
  {
    if (modifiedDacEntries == null || modifiedDacEntries.Count == 0)
      return;
    ILookup<DacModificationType, ModifiedDacEntryForUserRecordsUpdate> lookup = modifiedDacEntries.ToLookup<ModifiedDacEntryForUserRecordsUpdate, DacModificationType>((System.Func<ModifiedDacEntryForUserRecordsUpdate, DacModificationType>) (entry => entry.ModificationType));
    Guid? rootUid = PXTransactionScope.RootUID;
    if (!rootUid.HasValue)
      return;
    this.DeleteUserRecordsForDeletedDACs(rootUid, lookup[DacModificationType.Delete]);
    this.UpdateUserRecordsForChangedDACs(rootUid, lookup[DacModificationType.Update]);
  }

  private void DeleteUserRecordsForDeletedDACs(
    Guid? transactionID,
    IEnumerable<ModifiedDacEntryForUserRecordsUpdate> deletedDacEntries)
  {
    IEnumerable<IGrouping<System.Type, ModifiedDacEntryForUserRecordsUpdate>> source1 = deletedDacEntries.GroupBy<ModifiedDacEntryForUserRecordsUpdate, System.Type>((System.Func<ModifiedDacEntryForUserRecordsUpdate, System.Type>) (entry => entry.EntityType));
    if (!source1.Any<IGrouping<System.Type, ModifiedDacEntryForUserRecordsUpdate>>())
      return;
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint((IDbTransaction) PXTransactionScope.GetTransaction());
    ITableAdapter table1 = dbServicesPoint.GetTable("VisitedRecord", FileMode.Open);
    ITableAdapter table2 = dbServicesPoint.GetTable("FavoriteRecord", FileMode.Open);
    companySetting settings;
    int companyId1 = PXDatabase.Provider.getCompanyID(table1.TableName, out settings);
    int companyId2 = PXDatabase.Provider.getCompanyID(table2.TableName, out settings);
    ExecutionContext executionContext = new ExecutionContext((IExecutionObserver) null)
    {
      TimeoutMultiplier = 60
    };
    List<CmdDelete> cmdDeleteList = new List<CmdDelete>();
    foreach (IGrouping<System.Type, ModifiedDacEntryForUserRecordsUpdate> source2 in source1)
    {
      Guid[] array = source2.Select<ModifiedDacEntryForUserRecordsUpdate, Guid>((System.Func<ModifiedDacEntryForUserRecordsUpdate, Guid>) (entry => entry.NoteID)).ToArray<Guid>();
      CmdDelete recordsForAllUsers1 = this.CreateCommandToDeleteVisitedRecordsForAllUsers(source2.Key.FullName, companyId1, array);
      CmdDelete recordsForAllUsers2 = this.CreateCommandToDeleteFavoriteRecordsForAllUsers(source2.Key.FullName, companyId2, array);
      cmdDeleteList.Add(recordsForAllUsers1);
      cmdDeleteList.Add(recordsForAllUsers2);
    }
    using (LoggerOperationExtensions.OperationAt(this._logger, (LogEventLevel) 0, new LogEventLevel?()).Time("Delete of the user records for all users in DB Transaction {TransactionID}", new object[1]
    {
      (object) transactionID
    }))
    {
      try
      {
        dbServicesPoint.executeCommands((IEnumerable<CommandBase>) cmdDeleteList, executionContext, false);
      }
      catch (PXDatabaseException ex)
      {
        this._logger.Error<string>((Exception) ex, "Failed to delete user records. The error message: {ErrorMessage}", ex.Message);
        throw;
      }
    }
  }

  private CmdDelete CreateCommandToDeleteVisitedRecordsForAllUsers(
    string entityType,
    int companyID,
    Guid[] dacNoteIDs)
  {
    return new CmdDelete(YaqlSchemaTable.op_Implicit("VisitedRecord"), (List<YaqlJoin>) null)
    {
      Condition = Yaql.and(Yaql.and(Yaql.eq<int>((YaqlScalar) Yaql.column("CompanyID", (string) null), companyID), Yaql.isIn<Guid>((YaqlScalar) Yaql.column<VisitedRecord.refNoteID>((string) null), (IEnumerable<Guid>) dacNoteIDs)), Yaql.eq<string>((YaqlScalar) Yaql.column<VisitedRecord.entityType>((string) null), entityType))
    };
  }

  private CmdDelete CreateCommandToDeleteFavoriteRecordsForAllUsers(
    string entityType,
    int companyID,
    Guid[] dacNoteIDs)
  {
    return new CmdDelete(YaqlSchemaTable.op_Implicit("FavoriteRecord"), (List<YaqlJoin>) null)
    {
      Condition = Yaql.and(Yaql.and(Yaql.eq<int>((YaqlScalar) Yaql.column("CompanyID", (string) null), companyID), Yaql.isIn<Guid>((YaqlScalar) Yaql.column<FavoriteRecord.refNoteID>((string) null), (IEnumerable<Guid>) dacNoteIDs)), Yaql.eq<string>((YaqlScalar) Yaql.column<FavoriteRecord.entityType>((string) null), entityType))
    };
  }

  private void UpdateUserRecordsForChangedDACs(
    Guid? transactionID,
    IEnumerable<ModifiedDacEntryForUserRecordsUpdate> updatedDacEntries)
  {
    foreach (ModifiedDacEntryForUserRecordsUpdate updatedDacEntry in updatedDacEntries)
    {
      string fullName = updatedDacEntry.EntityType.FullName;
      try
      {
        this.UpdateVisitedRecordsCachedContentForAllUsers(transactionID, updatedDacEntry, fullName);
        this.UpdateFavoriteRecordsCachedContentForAllUsers(transactionID, updatedDacEntry, fullName);
      }
      catch (PXDatabaseException ex)
      {
        this._logger.Error<string, Guid, string>((Exception) ex, "Failed to update user records for entity of type {EntityType} with NoteID {NoteID}.The error message: {ErrorMessage}", fullName, updatedDacEntry.NoteID, ex.Message);
        throw;
      }
    }
  }

  private void UpdateVisitedRecordsCachedContentForAllUsers(
    Guid? transactionID,
    ModifiedDacEntryForUserRecordsUpdate dacEntry,
    string entityType)
  {
    using (LoggerOperationExtensions.OperationAt(this._logger, (LogEventLevel) 0, new LogEventLevel?()).Time("Update of the visited records indexed content for all users in DB Transaction {TransactionID} for entity {EntityType} {NoteID}", new object[3]
    {
      (object) transactionID,
      (object) entityType,
      (object) dacEntry.NoteID
    }))
    {
      this._logger.Verbose<Guid?, string, Guid>("Starting update of the visited records indexed content for all users in DB Transaction {TransactionID} for entity {EntityType} {NoteID}", transactionID, entityType, dacEntry.NoteID);
      PXDatabase.Update<VisitedRecord>((PXDataFieldParam) new PXDataFieldRestrict("refNoteID", PXDbType.UniqueIdentifier, (object) dacEntry.NoteID), (PXDataFieldParam) new PXDataFieldRestrict(nameof (entityType), PXDbType.NVarChar, (object) entityType), (PXDataFieldParam) new PXDataFieldAssign("recordContent", PXDbType.NText, (object) dacEntry.CachedContent));
    }
  }

  private void UpdateFavoriteRecordsCachedContentForAllUsers(
    Guid? transactionID,
    ModifiedDacEntryForUserRecordsUpdate dacEntry,
    string entityType)
  {
    using (LoggerOperationExtensions.OperationAt(this._logger, (LogEventLevel) 0, new LogEventLevel?()).Time("Update of the favorite records indexed content for all users in DB Transaction {TransactionID} for entity {EntityType} {NoteID}", new object[3]
    {
      (object) transactionID,
      (object) entityType,
      (object) dacEntry.NoteID
    }))
    {
      this._logger.Verbose<Guid?, string, Guid>("Starting update of the favorite records indexed content for all users in DB Transaction {TransactionID} for entity {EntityType} {NoteID}", transactionID, entityType, dacEntry.NoteID);
      PXDatabase.Update<FavoriteRecord>((PXDataFieldParam) new PXDataFieldRestrict("refNoteID", PXDbType.UniqueIdentifier, (object) dacEntry.NoteID), (PXDataFieldParam) new PXDataFieldRestrict(nameof (entityType), PXDbType.NVarChar, (object) entityType), (PXDataFieldParam) new PXDataFieldAssign("recordContent", PXDbType.NText, (object) dacEntry.CachedContent));
    }
  }
}

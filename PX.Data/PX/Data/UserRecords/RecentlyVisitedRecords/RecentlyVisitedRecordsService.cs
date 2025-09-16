// Decompiled with JetBrains decompiler
// Type: PX.Data.UserRecords.RecentlyVisitedRecords.RecentlyVisitedRecordsService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.BulkInsert.Installer.SysUpdates;
using PX.Common;
using PX.Data.Services.Interfaces;
using PX.DbServices.Commands;
using PX.DbServices.Commands.Data;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using Serilog;
using Serilog.Events;
using SerilogTimings;
using SerilogTimings.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Data.UserRecords.RecentlyVisitedRecords;

/// <summary>
/// A recently visited records provider service implementation. Will store <c>200</c> visited records per user.
/// </summary>
internal class RecentlyVisitedRecordsService : IRecentlyVisitedRecordsService
{
  private readonly IRecordCachedContentBuilder _cachedContentBuilder;
  private readonly ICurrentUserInformationProvider _currentUserInformationProvider;
  private readonly IAppInstanceInfo _appInstanceInfo;
  private readonly ILoginAsUser _loginAsUser;
  private readonly ILogger _logger;
  private const int MaxRecordsPerUser = 500;

  public RecentlyVisitedRecordsService(
    IRecordCachedContentBuilder cachedContentBuilder,
    ICurrentUserInformationProvider currentUserInformationProvider,
    IAppInstanceInfo appInstanceInfo,
    ILoginAsUser loginAsUser,
    ILogger logger)
  {
    this._cachedContentBuilder = cachedContentBuilder;
    this._currentUserInformationProvider = currentUserInformationProvider;
    this._appInstanceInfo = appInstanceInfo;
    this._loginAsUser = loginAsUser;
    this._logger = logger;
  }

  /// <summary>Gets the current user visited records.</summary>
  /// <returns>The current user visited records.</returns>
  public IQueryable<VisitedRecord> GetCurrentUserVisitedRecords()
  {
    return this.GetRecordsVisitedByUser(this._currentUserInformationProvider.GetUserIdOrDefault());
  }

  /// <summary>Record visited document for the current user.</summary>
  /// <param name="screenGraph">The graph of the screen.</param>
  /// <param name="document">The document.</param>
  /// <returns>True if it succeeds, false if it fails.</returns>
  public bool RecordVisitedDocumentForCurrentUser(PXGraph screenGraph, IBqlTable document)
  {
    ExceptionExtensions.ThrowOnNull<IBqlTable>(document, nameof (document), (string) null);
    ExceptionExtensions.ThrowOnNull<PXGraph>(screenGraph, nameof (screenGraph), (string) null);
    System.Type type = document.GetType();
    PXCache cach = screenGraph.Caches[type];
    if (!this.ShouldRecord(cach, document))
      return false;
    Guid? entityNoteId = EntityHelper.GetEntityNoteID(cach, document);
    if (!entityNoteId.HasValue)
      return false;
    Guid userIdOrDefault = this._currentUserInformationProvider.GetUserIdOrDefault();
    string fullName = type.FullName;
    bool flag = this.HasExistingVisitedRecord(userIdOrDefault, entityNoteId.Value, fullName);
    string cachedContent = !flag ? this._cachedContentBuilder.BuildCachedContent(screenGraph, document) : (string) null;
    string str = !flag ? "Insert of a new VisitedRecord with IsPortal={IsPortal} in a DB transaction for user {UserID} and entity {EntityType} {NoteID}" : "Update of the existing VisitedRecord with IsPortal={IsPortal} in a DB transaction for user {UserID} and entity {EntityType} {NoteID}";
    using (Operation operation = LoggerOperationExtensions.OperationAt(this._logger, (LogEventLevel) 0, new LogEventLevel?()).Begin(str, new object[4]
    {
      (object) this._appInstanceInfo.IsPortal,
      (object) userIdOrDefault,
      (object) fullName,
      (object) entityNoteId
    }))
    {
      Guid rootUid;
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        rootUid = transactionScope._RootUID;
        this._logger.Verbose("DB Transaction {TransactionID} started for IsPortal={IsPortal}, user {UserID} and entity {EntityType} {NoteID}", new object[5]
        {
          (object) rootUid,
          (object) this._appInstanceInfo.IsPortal,
          (object) userIdOrDefault,
          (object) fullName,
          (object) entityNoteId
        });
        if (!flag)
          this.TryInsertNewRecordForUser(userIdOrDefault, entityNoteId.Value, fullName, cachedContent);
        else
          this.UpdateExistingRecord(userIdOrDefault, entityNoteId.Value, fullName);
        transactionScope.Complete();
      }
      operation.Complete("TransactionID", (object) rootUid, false);
    }
    return true;
  }

  /// <summary>
  /// Determine if the service should record <paramref name="document" />.
  /// </summary>
  /// <param name="documentCache">The document cache.</param>
  /// <param name="document">The document.</param>
  /// <returns />
  protected virtual bool ShouldRecord(PXCache documentCache, IBqlTable document)
  {
    if (this.IsUserImpersonationActive() || documentCache.Keys.Count == 0 || PXContext.IsInMobileCrawlingMode() || documentCache.GetStatus((object) document) != PXEntryStatus.Notchanged)
      return false;
    System.Type type = document.GetType();
    return !Notes.TablesWithNonUniqueNoteId.Contains(type.Name) && !Attribute.IsDefined((MemberInfo) type, typeof (PXHiddenAttribute), false) && Attribute.IsDefined((MemberInfo) type, typeof (PXPrimaryGraphBaseAttribute), true);
  }

  private bool IsUserImpersonationActive()
  {
    return this._loginAsUser.TryGetLoggedAsUserNameFromCurrentContext() != null;
  }

  private bool HasExistingVisitedRecord(Guid userID, Guid refNoteID, string entityType)
  {
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<VisitedRecord>((PXDataField) new PXDataField<VisitedRecord.refNoteID>(), (PXDataField) new PXDataFieldValue<VisitedRecord.isPortal>(PXDbType.Bit, (object) this._appInstanceInfo.IsPortal), (PXDataField) new PXDataFieldValue<VisitedRecord.userID>(PXDbType.UniqueIdentifier, (object) userID), (PXDataField) new PXDataFieldValue<VisitedRecord.refNoteID>(PXDbType.UniqueIdentifier, (object) refNoteID), (PXDataField) new PXDataFieldValue<VisitedRecord.entityType>((object) entityType)))
      return pxDataRecord != null;
  }

  private void TryInsertNewRecordForUser(
    Guid userID,
    Guid refNoteID,
    string entityType,
    string cachedContent)
  {
    try
    {
      this.InsertNewRecordForUser(userID, refNoteID, entityType, cachedContent);
    }
    catch (PXLockViolationException ex) when (ex.Operation == PXDBOperation.Insert)
    {
      this.UpdateExistingRecord(userID, refNoteID, entityType);
    }
    catch (PXDatabaseException ex) when (ex.ErrorCode == PXDbExceptions.PrimaryKeyConstraintViolation)
    {
      this.UpdateExistingRecord(userID, refNoteID, entityType);
    }
  }

  private void UpdateExistingRecord(Guid userID, Guid refNoteID, string entityType)
  {
    PXDataFieldParam[] pxDataFieldParamArray = new PXDataFieldParam[6]
    {
      (PXDataFieldParam) new PXDataFieldRestrict<VisitedRecord.isPortal>(PXDbType.Bit, (object) this._appInstanceInfo.IsPortal),
      (PXDataFieldParam) new PXDataFieldRestrict<VisitedRecord.userID>(PXDbType.UniqueIdentifier, (object) userID),
      (PXDataFieldParam) new PXDataFieldRestrict<VisitedRecord.refNoteID>(PXDbType.UniqueIdentifier, (object) refNoteID),
      (PXDataFieldParam) new PXDataFieldRestrict<VisitedRecord.entityType>((object) entityType),
      null,
      null
    };
    PXDataFieldAssign<VisitedRecord.visitCount> pxDataFieldAssign = new PXDataFieldAssign<VisitedRecord.visitCount>(PXDbType.Int, (object) 1);
    pxDataFieldAssign.Behavior = PXDataFieldAssign.AssignBehavior.Summarize;
    pxDataFieldParamArray[4] = (PXDataFieldParam) pxDataFieldAssign;
    pxDataFieldParamArray[5] = (PXDataFieldParam) new PXDataFieldAssign<VisitedRecord.lastModifiedDateTime>(PXDbType.DateTime, (object) PXTimeZoneInfo.UtcNow);
    PXDatabase.Update<VisitedRecord>(pxDataFieldParamArray);
  }

  private void InsertNewRecordForUser(
    Guid userID,
    Guid refNoteID,
    string entityType,
    string cachedContent)
  {
    this.DeleteOldestNotVisitedRecords(userID);
    PXDatabase.Insert<VisitedRecord>(GetFieldsForNewVisitedRecord().ToArray<PXDataFieldAssign>());

    IEnumerable<PXDataFieldAssign> GetFieldsForNewVisitedRecord()
    {
      yield return (PXDataFieldAssign) new PXDataFieldAssign<VisitedRecord.isPortal>(PXDbType.Bit, (object) this._appInstanceInfo.IsPortal);
      yield return (PXDataFieldAssign) new PXDataFieldAssign<VisitedRecord.userID>(PXDbType.UniqueIdentifier, (object) userID);
      yield return (PXDataFieldAssign) new PXDataFieldAssign<VisitedRecord.refNoteID>(PXDbType.UniqueIdentifier, (object) refNoteID);
      yield return (PXDataFieldAssign) new PXDataFieldAssign<VisitedRecord.entityType>((object) entityType);
      yield return (PXDataFieldAssign) new PXDataFieldAssign<VisitedRecord.visitCount>(PXDbType.Int, (object) 1);
      yield return (PXDataFieldAssign) new PXDataFieldAssign<VisitedRecord.recordContent>((object) cachedContent);
      yield return (PXDataFieldAssign) new PXDataFieldAssign<VisitedRecord.createdByScreenID>((object) (PXContext.GetScreenID()?.Replace(".", "") ?? "00000000"));
      System.DateTime createdDateTime = PXTimeZoneInfo.UtcNow;
      yield return (PXDataFieldAssign) new PXDataFieldAssign<VisitedRecord.createdDateTime>(PXDbType.DateTime, (object) createdDateTime);
      yield return (PXDataFieldAssign) new PXDataFieldAssign<VisitedRecord.lastModifiedDateTime>(PXDbType.DateTime, (object) createdDateTime);
    }
  }

  private void DeleteOldestNotVisitedRecords(Guid userID)
  {
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint((IDbTransaction) PXTransactionScope.GetTransaction());
    int companyId = PXDatabase.Provider.getCompanyID(dbServicesPoint.GetTable("VisitedRecord", FileMode.Open).TableName, out companySetting _);
    YaqlTableQuery yaqlTableQuery = new YaqlTableQuery((YaqlTable) Yaql.schemaTable("VisitedRecord", "innerRecords"), (List<YaqlJoin>) null, "latestRecordsSubQuery");
    ((YaqlQueryBase) yaqlTableQuery).Condition = Yaql.and(Yaql.and(Yaql.eq<int>((YaqlScalar) Yaql.column("CompanyID", "innerRecords"), companyId), Yaql.eq<bool>((YaqlScalar) Yaql.column<VisitedRecord.isPortal>("innerRecords"), this._appInstanceInfo.IsPortal)), Yaql.eq<Guid>((YaqlScalar) Yaql.column<VisitedRecord.userID>("innerRecords"), userID));
    ((YaqlQueryBase) yaqlTableQuery).OrderBy.Add(new YaqlOrderByScalar()
    {
      IsAsc = false,
      Scalar = (YaqlScalar) Yaql.column<VisitedRecord.lastModifiedDateTime>("innerRecords")
    });
    yaqlTableQuery.Columns.Add(YaqlScalarAlilased.op_Implicit(Yaql.column<VisitedRecord.lastModifiedDateTime>("innerRecords")));
    yaqlTableQuery.LimitRows = 500;
    YaqlScalarQuery yaqlScalarQuery1 = new YaqlScalarQuery((YaqlTable) yaqlTableQuery, (List<YaqlJoin>) null);
    ((YaqlVectorQuery) yaqlScalarQuery1).Column = new YaqlScalarAlilased(Yaql.min((YaqlScalar) Yaql.column<VisitedRecord.lastModifiedDateTime>("latestRecordsSubQuery")), (string) null);
    YaqlScalarQuery yaqlScalarQuery2 = yaqlScalarQuery1;
    dbServicesPoint.executeSingleCommand((CommandBase) new CmdDelete(YaqlSchemaTable.op_Implicit("VisitedRecord"), (List<YaqlJoin>) null)
    {
      Condition = Yaql.and(Yaql.and(Yaql.and(Yaql.eq<int>((YaqlScalar) Yaql.column("CompanyID", (string) null), companyId), Yaql.eq<bool>((YaqlScalar) Yaql.column<VisitedRecord.isPortal>((string) null), this._appInstanceInfo.IsPortal)), Yaql.eq<Guid>((YaqlScalar) Yaql.column<VisitedRecord.userID>((string) null), userID)), Yaql.lt<YaqlScalarQuery>((YaqlScalar) Yaql.column<VisitedRecord.lastModifiedDateTime>((string) null), yaqlScalarQuery2))
    }, new ExecutionContext((IExecutionObserver) null)
    {
      TimeoutMultiplier = 60
    }, false);
  }

  private IQueryable<VisitedRecord> GetRecordsVisitedByUser(Guid userID)
  {
    bool isPortal = this._appInstanceInfo.IsPortal;
    return PXDatabase.Select<VisitedRecord>().Where<VisitedRecord>((Expression<System.Func<VisitedRecord, bool>>) (record => record.IsPortal == (bool?) isPortal && record.UserID == (Guid?) userID));
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.UserRecords.FavoriteRecords.FavoriteRecordsService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Services.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.UserRecords.FavoriteRecords;

/// <summary>Implementation of favorite records service.</summary>
internal class FavoriteRecordsService : IFavoriteRecordsService
{
  private readonly IRecordCachedContentBuilder _contentBuilder;
  private readonly ICurrentUserInformationProvider _currentUserInformationProvider;
  private readonly IAppInstanceInfo _appInstanceInfo;

  public FavoriteRecordsService(
    IRecordCachedContentBuilder contentBuilder,
    ICurrentUserInformationProvider currentUserInformationProvider,
    IAppInstanceInfo appInstanceInfo)
  {
    this._currentUserInformationProvider = currentUserInformationProvider;
    this._appInstanceInfo = appInstanceInfo;
    this._contentBuilder = contentBuilder;
  }

  /// <summary>Gets the current user favorite records.</summary>
  /// <returns>The current user favorite records.</returns>
  public IQueryable<FavoriteRecord> GetCurrentUserFavoriteRecords()
  {
    return this.GetFavoriteRecordsForUser(this._currentUserInformationProvider.GetUserIdOrDefault());
  }

  /// <summary>
  /// Adds record with NoteID <paramref name="refNoteID" /> to the current user favorites. Returns true if it succeeds, false if it fails.
  /// </summary>
  /// <param name="graph">Graph to work on.</param>
  /// <param name="refNoteID">Entity's NoteID.</param>
  /// <param name="entityTypeName">Name of the entity's type.</param>
  /// <returns>True if it succeeds, false if it fails.</returns>
  public bool AddToCurrentUserFavorites(PXGraph graph, Guid refNoteID, string entityTypeName)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(entityTypeName, nameof (entityTypeName), (string) null);
    System.Type entityType = this.GetEntityType(entityTypeName);
    if (entityType == (System.Type) null)
      return false;
    Guid userIdOrDefault = this._currentUserInformationProvider.GetUserIdOrDefault();
    if (this.HasExistingFavoriteRecord(userIdOrDefault, refNoteID, entityTypeName))
      return true;
    if (!(new EntityHelper(graph).GetEntityRow(entityType, new Guid?(refNoteID)) is IBqlTable entityRow))
      return false;
    string cachedContent = this._contentBuilder.BuildCachedContent(graph, entityRow);
    PXDataFieldAssign[] newFavoriteRecord = this.GetFieldsForNewFavoriteRecord(userIdOrDefault, refNoteID, entityTypeName, cachedContent);
    try
    {
      return PXDatabase.Insert<FavoriteRecord>(newFavoriteRecord);
    }
    catch (PXLockViolationException ex) when (ex.Operation == PXDBOperation.Insert)
    {
      return true;
    }
    catch (PXDatabaseException ex) when (ex.ErrorCode == PXDbExceptions.PrimaryKeyConstraintViolation)
    {
      return true;
    }
  }

  private PXDataFieldAssign[] GetFieldsForNewFavoriteRecord(
    Guid userID,
    Guid refNoteID,
    string entityType,
    string cachedContent)
  {
    return new PXDataFieldAssign[7]
    {
      (PXDataFieldAssign) new PXDataFieldAssign<FavoriteRecord.isPortal>(PXDbType.Bit, (object) this._appInstanceInfo.IsPortal),
      (PXDataFieldAssign) new PXDataFieldAssign<FavoriteRecord.userID>(PXDbType.UniqueIdentifier, (object) userID),
      (PXDataFieldAssign) new PXDataFieldAssign<FavoriteRecord.refNoteID>(PXDbType.UniqueIdentifier, (object) refNoteID),
      (PXDataFieldAssign) new PXDataFieldAssign<FavoriteRecord.entityType>((object) entityType),
      (PXDataFieldAssign) new PXDataFieldAssign<FavoriteRecord.recordContent>((object) cachedContent),
      (PXDataFieldAssign) new PXDataFieldAssign<FavoriteRecord.createdByScreenID>((object) (PXContext.GetScreenID()?.Replace(".", "") ?? "00000000")),
      (PXDataFieldAssign) new PXDataFieldAssign<FavoriteRecord.createdDateTime>(PXDbType.DateTime, (object) PXTimeZoneInfo.UtcNow)
    };
  }

  private System.Type GetEntityType(string entityTypeName)
  {
    System.Type type = PXBuildManager.GetType(entityTypeName, false);
    if (type == (System.Type) null)
      return (System.Type) null;
    return !Attribute.IsDefined((MemberInfo) type, typeof (PXHiddenAttribute), false) ? type : (System.Type) null;
  }

  /// <summary>
  /// Removes record from current user favorites described by <paramref name="refNoteID" />.
  /// </summary>
  /// <param name="refNoteID">Identifier for the reference note.</param>
  /// <param name="entityType">Type of the entity.</param>
  public void RemoveFromCurrentUserFavorites(Guid refNoteID, string entityType)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(entityType, nameof (entityType), (string) null);
    PXDatabase.Delete<FavoriteRecord>((PXDataFieldRestrict) new PXDataFieldRestrict<FavoriteRecord.isPortal>(PXDbType.Bit, (object) this._appInstanceInfo.IsPortal), (PXDataFieldRestrict) new PXDataFieldRestrict<FavoriteRecord.userID>(PXDbType.UniqueIdentifier, (object) this._currentUserInformationProvider.GetUserIdOrDefault()), (PXDataFieldRestrict) new PXDataFieldRestrict<FavoriteRecord.refNoteID>(PXDbType.UniqueIdentifier, (object) refNoteID), (PXDataFieldRestrict) new PXDataFieldRestrict<FavoriteRecord.entityType>((object) entityType));
  }

  private IQueryable<FavoriteRecord> GetFavoriteRecordsForUser(Guid userID)
  {
    bool isPortal = this._appInstanceInfo.IsPortal;
    return PXDatabase.Select<FavoriteRecord>().Where<FavoriteRecord>((Expression<Func<FavoriteRecord, bool>>) (record => record.IsPortal == (bool?) isPortal && record.UserID == (Guid?) userID));
  }

  private bool HasExistingFavoriteRecord(Guid userID, Guid refNoteID, string entityType)
  {
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<FavoriteRecord>((PXDataField) new PXDataField<FavoriteRecord.refNoteID>(), (PXDataField) new PXDataFieldValue<FavoriteRecord.isPortal>(PXDbType.Bit, (object) this._appInstanceInfo.IsPortal), (PXDataField) new PXDataFieldValue<FavoriteRecord.userID>(PXDbType.UniqueIdentifier, (object) userID), (PXDataField) new PXDataFieldValue<FavoriteRecord.refNoteID>(PXDbType.UniqueIdentifier, (object) refNoteID), (PXDataField) new PXDataFieldValue<FavoriteRecord.entityType>((object) entityType)))
      return pxDataRecord != null;
  }
}

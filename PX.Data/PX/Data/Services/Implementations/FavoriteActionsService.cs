// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.Implementations.FavoriteActionsService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Data.Services.Implementations;

internal class FavoriteActionsService : IFavoriteActionsService
{
  public bool AddToCurrentUserFavorites(string screenId, string actionName)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(screenId, nameof (screenId), (string) null);
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(actionName, nameof (actionName), (string) null);
    Guid userId = PXAccess.GetUserID();
    FavoriteActionsGraph instance = PXGraph.CreateInstance<FavoriteActionsGraph>();
    PXCache<FavoriteActionRecord> pxCache = instance.Caches<FavoriteActionRecord>();
    if (this.GetExistingFavoriteRecord((PXGraph) instance, userId, screenId, actionName) != null)
      return true;
    FavoriteActionRecord data = new FavoriteActionRecord()
    {
      ActionName = actionName,
      ScreenID = screenId,
      UserID = new Guid?(userId)
    };
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      try
      {
        pxCache.Insert(data);
        pxCache.Persist(PXDBOperation.Insert);
        pxCache.Persisted(false);
      }
      catch (PXLockViolationException ex) when (ex.Operation == PXDBOperation.Insert)
      {
        pxCache.Persisted(true);
        return true;
      }
      catch (PXDatabaseException ex) when (ex.ErrorCode == PXDbExceptions.PrimaryKeyConstraintViolation)
      {
        pxCache.Persisted(true);
        return true;
      }
      finally
      {
        if (PXContext.Session.IsSessionEnabled)
          this.GetCache()?.Remove(screenId);
      }
      transactionScope.Complete((PXGraph) instance);
    }
    return true;
  }

  public void RemoveFromCurrentUserFavorites(string screenId, string actionName)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(screenId, nameof (screenId), (string) null);
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(actionName, nameof (actionName), (string) null);
    Guid userId = PXAccess.GetUserID();
    FavoriteActionsGraph instance = PXGraph.CreateInstance<FavoriteActionsGraph>();
    PXCache<FavoriteActionRecord> pxCache = instance.Caches<FavoriteActionRecord>();
    FavoriteActionRecord existingFavoriteRecord = this.GetExistingFavoriteRecord((PXGraph) instance, userId, screenId, actionName);
    if (existingFavoriteRecord == null)
      return;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      try
      {
        pxCache.Delete(existingFavoriteRecord);
        pxCache.Persist(PXDBOperation.Delete);
        pxCache.Persisted(false);
      }
      finally
      {
        if (PXContext.Session.IsSessionEnabled)
          this.GetCache()?.Remove(screenId);
      }
      transactionScope.Complete((PXGraph) instance);
    }
  }

  public IEnumerable<FavoriteActionRecord> GetCurrentUserFavoriteActions(
    PXGraph graph,
    string screenId)
  {
    if (screenId == null)
      return (IEnumerable<FavoriteActionRecord>) null;
    if (!PXContext.Session.IsSessionEnabled)
      return (IEnumerable<FavoriteActionRecord>) this.GetFavoriteRecordsForUser(graph, PXAccess.GetUserID(), screenId).ToList<FavoriteActionRecord>();
    Dictionary<string, List<FavoriteActionRecord>> cache = this.GetCache();
    List<FavoriteActionRecord> userFavoriteActions;
    if (cache.TryGetValue(screenId, out userFavoriteActions))
      return (IEnumerable<FavoriteActionRecord>) userFavoriteActions;
    List<FavoriteActionRecord> list = this.GetFavoriteRecordsForUser(graph, PXAccess.GetUserID(), screenId).ToList<FavoriteActionRecord>();
    cache.Add(screenId, list);
    return (IEnumerable<FavoriteActionRecord>) list;
  }

  private Dictionary<string, List<FavoriteActionRecord>> GetCache()
  {
    if (!(PXContext.Session["UserFavoriteActions"] is Dictionary<string, List<FavoriteActionRecord>> cache))
    {
      cache = new Dictionary<string, List<FavoriteActionRecord>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      PXContext.Session["UserFavoriteActions"] = (object) cache;
    }
    return cache;
  }

  private IQueryable<FavoriteActionRecord> GetFavoriteRecordsForUser(
    PXGraph graph,
    Guid userID,
    string screenId)
  {
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    return PXSelectBase<FavoriteActionRecord, PXSelectReadonly<FavoriteActionRecord, Where<FavoriteActionRecord.isPortal, Equal<IsPortal>, And<FavoriteActionRecord.userID, Equal<Required<FavoriteActionRecord.userID>>, And<FavoriteActionRecord.screenID, Equal<Required<FavoriteActionRecord.screenID>>>>>>.Config>.Select(graph, (object) userID, (object) screenId).Select<PXResult<FavoriteActionRecord>, FavoriteActionRecord>(Expression.Lambda<Func<PXResult<FavoriteActionRecord>, FavoriteActionRecord>>((Expression) Expression.Call(res, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression));
  }

  private FavoriteActionRecord GetExistingFavoriteRecord(
    PXGraph graph,
    Guid userID,
    string screenId,
    string actionName)
  {
    return (FavoriteActionRecord) PXSelectBase<FavoriteActionRecord, PXSelectReadonly<FavoriteActionRecord, Where<FavoriteActionRecord.isPortal, Equal<IsPortal>, And<FavoriteActionRecord.userID, Equal<Required<FavoriteActionRecord.userID>>, And<FavoriteActionRecord.screenID, Equal<Required<FavoriteActionRecord.screenID>>, And<FavoriteActionRecord.actionName, Equal<Required<FavoriteActionRecord.actionName>>>>>>>.Config>.SelectSingleBound(graph, (object[]) null, (object) userID, (object) screenId, (object) actionName);
  }
}

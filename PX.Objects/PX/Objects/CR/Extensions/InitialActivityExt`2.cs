// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.InitialActivityExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using Serilog;
using System;
using System.Linq;

#nullable enable
namespace PX.Objects.CR.Extensions;

/// <summary>
/// The graph extension that inserts the initial activity on entity insertion.
/// </summary>
/// <typeparam name="TGraph">The base graph</typeparam>
/// <typeparam name="TPrimaryEntity">The type of the primary entity</typeparam>
public abstract class InitialActivityExt<TGraph, TPrimaryEntity> : PXGraphExtension<
#nullable disable
TGraph>
  where TGraph : PXGraph, new()
  where TPrimaryEntity : class, IBqlTable, INotable, new()
{
  public FbqlSelect<SelectFromBase<CRActivity, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  CRActivity.refNoteID, IBqlGuid>.IsEqual<
  #nullable disable
  P.AsGuid>>, CRActivity>.View RelatedActivities;

  /// <exclude />
  public bool SkipInitialActivityCreation { get; set; }

  /// <exclude />
  [InjectDependency]
  public ILogger Logger { get; protected set; }

  /// <exclude />
  public virtual bool ShouldAddInitialActivity()
  {
    PXCache cach = this.Base.Caches[typeof (TPrimaryEntity)];
    if (!this.SkipInitialActivityCreation && cach.GetStatus(cach.Current) == 2)
    {
      Guid? noteId = (Guid?) GraphHelper.Caches<TPrimaryEntity>((PXGraph) this.Base).Rows.Current?.NoteID;
      if (noteId.HasValue)
      {
        if (((PXSelectBase<CRActivity>) this.RelatedActivities).Select(new object[1]
        {
          (object) noteId.GetValueOrDefault()
        }).Count == 0)
          return true;
      }
    }
    return false;
  }

  /// <exclude />
  public virtual bool TryToPersistInitialActivity()
  {
    CRActivity crActivity = (CRActivity) null;
    try
    {
      PXCache<CRActivity> pxCache = GraphHelper.Caches<CRActivity>((PXGraph) this.Base);
      crActivity = this.NewInitialActivity();
      PXCacheEx.AdjustReadonly<PXRestrictorAttribute>((PXCache) pxCache, (object) crActivity).For<CRActivity.type>((Action<PXRestrictorAttribute>) (r => r.SuppressVerify = true));
      PXCacheEx.AdjustReadonly<ActivityEntityIDSelectorAttribute>((PXCache) pxCache, (object) crActivity).For<CRActivity.refNoteID>((Action<ActivityEntityIDSelectorAttribute>) (a => a.SuppressFillingContactAndBAccount = true));
      crActivity = pxCache.Insert(crActivity);
      if (((PXCache) pxCache).PersistInserted((object) crActivity))
      {
        pxCache.Remove(crActivity);
        this.Logger.Verbose<CRActivity>("Initial activity persisted successfully {@Activity}", crActivity);
        return true;
      }
      this.Logger.Error<CRActivity>("Failed to persist initial activity {@Activity}", crActivity);
      return false;
    }
    catch (Exception ex)
    {
      this.Logger.Error<CRActivity>(ex, "Failed to persist initial activity {@Activity}", crActivity);
      return false;
    }
  }

  /// <exclude />
  public virtual bool TryToPersistActivityStatistics()
  {
    CRActivityStatistics activityStatistics = (CRActivityStatistics) null;
    try
    {
      PXCache<CRActivityStatistics> pxCache = GraphHelper.Caches<CRActivityStatistics>((PXGraph) this.Base);
      activityStatistics = pxCache.Rows.Inserted.FirstOrDefault<CRActivityStatistics>();
      if (activityStatistics == null)
      {
        this.Logger.Verbose("No activity statistics to persist");
        return false;
      }
      if (((PXCache) pxCache).PersistInserted((object) activityStatistics))
      {
        pxCache.Remove(activityStatistics);
        this.Logger.Verbose<CRActivityStatistics>("Activity statistics persisted successfully {@ActivityStatistics}", activityStatistics);
        return true;
      }
      this.Logger.Error<CRActivityStatistics>("Failed to persist activity statistics {@ActivityStatistics}", activityStatistics);
      return false;
    }
    catch (Exception ex)
    {
      this.Logger.Error<CRActivityStatistics>(ex, "Failed to persist activity statistics {@ActivityStatistics}", activityStatistics);
      return false;
    }
  }

  /// <exclude />
  public virtual CRActivity NewInitialActivity()
  {
    CRActivity crActivity = new CRActivity();
    crActivity.Subject = this.GetInitialActivitySubject();
    crActivity.Type = "ASI";
    crActivity.StartDate = new DateTime?(PXTimeZoneInfo.Now);
    crActivity.UIStatus = "CD";
    crActivity.RefNoteIDType = typeof (CRCase).FullName;
    crActivity.RefNoteID = (Guid?) GraphHelper.Caches<TPrimaryEntity>((PXGraph) this.Base).Rows.Current?.NoteID ?? throw new PXInvalidOperationException("Cannot create initial activity. Current primary entity is null or RefNoteID is null.");
    return crActivity;
  }

  /// <exclude />
  public virtual string GetInitialActivityType() => "ASI";

  /// <exclude />
  public virtual string GetInitialActivitySubject()
  {
    return PXMessages.LocalizeNoPrefix("Case created in ERP");
  }

  /// <exclude />
  public virtual void FillInitialActivity(CRActivity activity)
  {
    activity.ContactID = new int?();
    activity.BAccountID = new int?();
    activity.DocumentNoteID = new Guid?();
  }

  /// <exclude />
  [PXOverride]
  public virtual void PreCommit()
  {
    if (!this.ShouldAddInitialActivity() || !this.TryToPersistInitialActivity())
      return;
    this.TryToPersistActivityStatistics();
  }

  protected virtual void _(Events.RowInserting<CRActivity> e, PXRowInserting del)
  {
    del?.Invoke(((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<CRActivity>>) e).Cache, ((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<CRActivity>>) e).Args);
    if (e.Row?.Type != this.GetInitialActivityType())
      return;
    this.FillInitialActivity(e.Row);
  }
}

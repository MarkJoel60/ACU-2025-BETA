// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCaseCommitments.CRCaseCommitmentsExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.CR.Extensions.CRCaseCommitments;

/// <summary>
/// The graph extension that calculates <see cref="T:PX.Objects.CR.CRCaseCommitments">case commitments</see> for related activities.
/// </summary>
/// <typeparam name="TGraph">The base graph</typeparam>
/// <typeparam name="TActivity">The type of the activity</typeparam>
public abstract class CRCaseCommitmentsExt<TGraph, TActivity> : CRCaseCommitmentsExt<TGraph>
  where TGraph : PXGraph
  where TActivity : CRActivity, new()
{
  /// <summary>
  /// Calculates <see cref="T:PX.Objects.CR.CRCaseCommitments">commitments</see> for the <see cref="T:PX.Objects.CR.CRCase">case</see> that is related to the provided <paramref name="activity" />.
  /// If the provided activity is not related to any case, no commitments are calculated.
  /// </summary>
  /// <param name="activity">An activity</param>
  public virtual void TryCalculateCommitmentsForRelatedCase(TActivity? activity)
  {
    if ((object) activity == null)
      return;
    activity = PXCache<TActivity>.CreateCopy(activity);
    if (!(activity.RefNoteIDType == typeof (CRCase).FullName))
      return;
    Guid? refNoteId = activity.RefNoteID;
    if (!refNoteId.HasValue)
      return;
    if (!(new EntityHelper((PXGraph) this.Base).GetEntityRow(typeof (CRCase), new Guid?(refNoteId.GetValueOrDefault())) is CRCase entityRow))
      return;
    if (activity.Application.Equals((object) 2))
    {
      bool? responseTimeCalculation = CRCaseClass.PK.Find((PXGraph) this.Base, entityRow.CaseClassID).IncludeSystemActivitiesResponseTimeCalculation;
      bool flag = false;
      if (responseTimeCalculation.GetValueOrDefault() == flag & responseTimeCalculation.HasValue)
        return;
    }
    int num = this.SetSolutionProvidedForRelatedCaseIfNeeded(activity, entityRow) ? 1 : 0;
    CaseCommitmentsDto dto = this.ReadCaseCommitmentsDto(entityRow);
    if (dto.Commitments == null)
    {
      this.EnsureCaseCommitmentsExists(dto.Case);
      PX.Objects.CR.CRCaseCommitments current = ((PXSelectBase<PX.Objects.CR.CRCaseCommitments>) this.CaseCommitments)?.Current;
      if (current != null)
      {
        current.CaseCD = dto.Case.CaseCD;
        GraphHelper.Caches<PX.Objects.CR.CRCaseCommitments>((PXGraph) this.Base).Insert(current);
        ((PXCache) GraphHelper.Caches<PX.Objects.CR.CRCaseCommitments>((PXGraph) this.Base)).PersistInserted((object) current);
        dto = this.ReadCaseCommitmentsDto(dto.Case);
      }
    }
    if (this.CalculateCaseCommitments(dto))
    {
      this.PersistCaseCommitments(dto.Commitments);
      this.PersistCommitmentsForRelatedActivities(dto);
    }
    if (num != 0)
      this.PersistCase(entityRow);
    if (this.UpdateActivityResponsesIfNeeded(dto, activity))
      this.PersistUpdatedActivityResponses(activity);
    this.PersistUpdatedResponseNoteIDForRelatedActivities(activity, dto);
  }

  /// <exclude />
  public virtual void PersistCase(CRCase @case)
  {
    PXCache<CRCase> pxCache = GraphHelper.Caches<CRCase>((PXGraph) this.Base);
    pxCache.Update(@case);
    ((PXCache) pxCache).PersistUpdated((object) @case);
    ((PXCache) pxCache).Persisted(false);
    ((PXCache) pxCache).Clear();
  }

  /// <exclude />
  public virtual bool PersistUpdatedActivityResponses(TActivity activity)
  {
    return PXDatabase.Update<CRActivity>(new PXDataFieldParam[3]
    {
      (PXDataFieldParam) new PXDataFieldRestrict<CRActivity.noteID>((PXDbType) 14, new int?(), (object) activity.NoteID, (PXComp) 0),
      (PXDataFieldParam) new PXDataFieldAssign<CRActivity.responseDueDateTime>((object) activity.ResponseDueDateTime),
      (PXDataFieldParam) new PXDataFieldAssign<CRActivity.responseActivityNoteID>((object) activity.ResponseActivityNoteID)
    });
  }

  /// <exclude />
  public virtual void ActualizeActivity(TActivity activity)
  {
    bool? incoming = activity.Incoming;
    if (!incoming.HasValue || !incoming.GetValueOrDefault())
      return;
    var data = this.Base.Select<CRActivity>().Where<CRActivity>((Expression<Func<CRActivity, bool>>) (a => a.NoteID == activity.NoteID)).Select(a => new
    {
      ResponseActivityNoteID = a.ResponseActivityNoteID,
      ResponseDueDateTime = a.ResponseDueDateTime
    }).FirstOrDefault();
    if (data == null)
      return;
    activity.ResponseActivityNoteID = data.ResponseActivityNoteID;
    activity.ResponseDueDateTime = data.ResponseDueDateTime;
  }

  /// <exclude />
  public virtual TActivity? TryGetActivity()
  {
    return (TActivity) this.Base.Caches[typeof (TActivity)].Current;
  }

  /// <exclude />
  public virtual bool SetSolutionProvidedForRelatedCaseIfNeeded(
    TActivity? activity,
    CRCase caseEntity)
  {
    if ((object) activity != null)
    {
      bool? providesCaseSolution = activity.ProvidesCaseSolution;
      if (providesCaseSolution.HasValue && providesCaseSolution.GetValueOrDefault() && activity.UIStatus == "CD" && caseEntity != null && !caseEntity.SolutionActivityNoteID.HasValue)
      {
        caseEntity.SolutionActivityNoteID = activity.NoteID;
        return true;
      }
    }
    return false;
  }

  /// <exclude />
  public virtual bool PersistUpdatedResponseNoteIDForRelatedActivities(
    TActivity activity,
    CaseCommitmentsDto dto)
  {
    bool? trackResponseTime = (bool?) dto.SeverityTime?.TrackResponseTime;
    if (!trackResponseTime.HasValue || !trackResponseTime.GetValueOrDefault() || activity.UIStatus != "CD")
      return false;
    return PXDatabase.Update<CRActivity>(new PXDataFieldParam[5]
    {
      (PXDataFieldParam) new PXDataFieldRestrict<CRActivity.refNoteID>((object) dto.Case.NoteID),
      (PXDataFieldParam) new PXDataFieldRestrict<CRActivity.incoming>((object) activity.Outgoing),
      (PXDataFieldParam) new PXDataFieldRestrict<CRActivity.responseActivityNoteID>((PXDbType) 14, new int?(), (object) null, (PXComp) 6),
      (PXDataFieldParam) new PXDataFieldRestrict<CRActivity.noteID>((PXDbType) 14, new int?(), (object) activity.NoteID, (PXComp) 1),
      (PXDataFieldParam) new PXDataFieldAssign<CRActivity.responseActivityNoteID>((object) activity.NoteID)
    });
  }

  /// <exclude />
  public virtual bool UpdateActivityResponsesIfNeeded(CaseCommitmentsDto dto, TActivity activity)
  {
    bool? nullable1 = (bool?) dto.SeverityTime?.TrackResponseTime;
    if (nullable1.HasValue && nullable1.GetValueOrDefault())
    {
      nullable1 = activity.Incoming;
      if (nullable1.HasValue && nullable1.GetValueOrDefault())
      {
        DateTime? nullable2 = (DateTime?) dto.ActivityStatistics?.LastOutgoingActivityDate;
        DateTime dateTime1 = nullable2 ?? DateTime.MinValue;
        (DateTime?, Guid?) valueTuple1 = (activity.ResponseDueDateTime, activity.ResponseActivityNoteID);
        DateTime dateTime2 = dateTime1;
        nullable2 = activity.CompletedDate;
        if ((nullable2.HasValue ? (dateTime2 >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          // ISSUE: variable of a boxed type
          __Boxed<TActivity> local = (object) activity;
          nullable2 = new DateTime?();
          DateTime? nullable3 = nullable2;
          local.ResponseDueDateTime = nullable3;
          activity.ResponseActivityNoteID = (Guid?) dto.ActivityStatistics?.LastOutgoingActivityNoteID;
        }
        else
        {
          activity.ResponseDueDateTime = dto.Commitments.ResponseDueDateTime;
          activity.ResponseActivityNoteID = new Guid?();
        }
        (DateTime?, Guid?) valueTuple2 = valueTuple1;
        nullable2 = activity.ResponseDueDateTime;
        Guid? responseActivityNoteId = activity.ResponseActivityNoteID;
        DateTime? nullable4 = valueTuple2.Item1;
        DateTime? nullable5 = nullable2;
        if ((nullable4.HasValue == nullable5.HasValue ? (nullable4.HasValue ? (nullable4.GetValueOrDefault() != nullable5.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
          return true;
        Guid? nullable6 = valueTuple2.Item2;
        Guid? nullable7 = responseActivityNoteId;
        if (nullable6.HasValue != nullable7.HasValue)
          return true;
        return nullable6.HasValue && nullable6.GetValueOrDefault() != nullable7.GetValueOrDefault();
      }
    }
    return false;
  }

  [PXDBDate]
  [PXMergeAttributes]
  protected virtual void _(Events.CacheAttached<CRCase.statusDate> e)
  {
  }

  [PXDBInt]
  [PXMergeAttributes]
  protected virtual void _(Events.CacheAttached<CRCase.statusRevision> e)
  {
  }

  public virtual void _(Events.RowPersisted<TActivity> e)
  {
    if ((object) e.Row == null)
      return;
    if (e.TranStatus == null)
    {
      this.TryCalculateCommitmentsForRelatedCase(e.Row);
    }
    else
    {
      if (e.TranStatus != 1)
        return;
      this.ActualizeActivity(e.Row);
    }
  }
}

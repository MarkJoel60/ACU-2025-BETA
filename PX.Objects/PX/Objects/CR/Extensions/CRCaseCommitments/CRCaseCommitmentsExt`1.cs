// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCaseCommitments.CRCaseCommitmentsExt`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.CS.Services.WorkTimeCalculation;
using System;
using System.Data.SqlTypes;

#nullable enable
namespace PX.Objects.CR.Extensions.CRCaseCommitments;

/// <summary>
/// The base extension used to calculate <see cref="T:PX.Objects.CR.CRCaseCommitments">case commitments</see> for related entities.
/// </summary>
/// <typeparam name="TGraph">Graph.</typeparam>
public abstract class CRCaseCommitmentsExt<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public FbqlSelect<
  #nullable disable
  SelectFromBase<
  #nullable enable
  CRPMTimeActivity, 
  #nullable disable
  TypeArrayOf<IFbqlJoin>.Empty>.Where<
  #nullable enable
  BqlChainableConditionBase<
  #nullable disable
  TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CRPMTimeActivity.refNoteID, 
  #nullable disable
  Equal<
  #nullable enable
  P.AsGuid>>>>, 
  #nullable disable
  And<
  #nullable enable
  BqlOperand<CRPMTimeActivity.completedDate, IBqlDateTime>.IsGreaterEqual<P.AsDateTime>>>, 
  #nullable disable
  And<
  #nullable enable
  BqlOperand<CRPMTimeActivity.incoming, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<CRPMTimeActivity.isPrivate, IBqlBool>.IsNotEqual<True>>>.Order<
  #nullable disable
  By<
  #nullable enable
  BqlField<CRPMTimeActivity.completedDate, IBqlDateTime>.Asc>>, CRPMTimeActivity>.View FirstUnansweredActivity;
  [PXViewName("Case Commitments")]
  public FbqlSelect<
  #nullable disable
  SelectFromBase<
  #nullable enable
  PX.Objects.CR.CRCaseCommitments, 
  #nullable disable
  TypeArrayOf<IFbqlJoin>.Empty>.Where<
  #nullable enable
  KeysRelation<
  #nullable disable
  Field<PX.Objects.CR.CRCaseCommitments.caseCD>.IsRelatedTo<CRCase.caseCD>.AsSimpleKey.WithTablesOf<CRCase, PX.Objects.CR.CRCaseCommitments>, CRCase, PX.Objects.CR.CRCaseCommitments>.SameAsCurrent>, 
  #nullable enable
  PX.Objects.CR.CRCaseCommitments>.View? CaseCommitments;

  /// <exclude />
  protected static bool IsExtensionActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.caseCommitmentsTracking>();
  }

  /// <summary>
  /// Calculates <see cref="T:PX.Objects.CR.CRCaseCommitments">commitments</see> for the related <see cref="T:PX.Objects.CR.CRCase">case</see>.
  /// </summary>
  /// <param name="dto">The set of items required for calculation.
  /// The <see cref="P:PX.Objects.CR.Extensions.CRCaseCommitments.CaseCommitmentsDto.Commitments" /> value will be updated after the method has been executed.</param>
  /// <returns><see langword="true" /> if commitments have been calculated; otherwise returns <see langword="false" />.</returns>
  public virtual bool CalculateCaseCommitments(CaseCommitmentsDto dto)
  {
    PX.Objects.CR.CRCaseCommitments copy = PXCache<PX.Objects.CR.CRCaseCommitments>.CreateCopy(dto.Commitments);
    this.CalculateInitialResponseTime(dto);
    this.CalculateResponseTime(dto);
    this.CalculateResolutionTime(dto);
    PXCache<PX.Objects.CR.CRCaseCommitments> pxCache = GraphHelper.Caches<PX.Objects.CR.CRCaseCommitments>((PXGraph) this.Base);
    int num = !((PXCache) pxCache).ObjectsEqual<PX.Objects.CR.CRCaseCommitments.resolutionDueDateTime, PX.Objects.CR.CRCaseCommitments.responseDueDateTime, PX.Objects.CR.CRCaseCommitments.initialResponseDueDateTime>((object) copy, (object) dto.Commitments) ? 1 : 0;
    if (num == 0)
      return num != 0;
    dto.Commitments = pxCache.Update(dto.Commitments);
    return num != 0;
  }

  /// <exclude />
  public virtual bool PersistCommitmentsForRelatedActivities(CaseCommitmentsDto dto)
  {
    (CRCase _, PX.Objects.CR.CRCaseCommitments commitments, CRCaseClass _, CRActivityStatistics activityStatistics, CRClassSeverityTime targetConfigRecord, CRPMTimeActivity _) = dto;
    bool? trackResponseTime = (bool?) targetConfigRecord?.TrackResponseTime;
    if (!trackResponseTime.HasValue || !trackResponseTime.GetValueOrDefault())
      return false;
    DateTime? nullable1 = TryConvertTime(commitments.ResponseDueDateTime);
    DateTime? nullable2 = new DateTime?(TryConvertTime((DateTime?) activityStatistics?.LastOutgoingActivityDate) ?? SqlDateTime.MinValue.Value);
    return PXDatabase.Update<CRActivity>(new PXDataFieldParam[4]
    {
      (PXDataFieldParam) new PXDataFieldRestrict<CRActivity.refNoteID>((object) dto.Case.NoteID),
      (PXDataFieldParam) new PXDataFieldRestrict<CRActivity.incoming>((object) true),
      (PXDataFieldParam) new PXDataFieldRestrict<CRActivity.completedDate>((PXDbType) 4, new int?(), (object) nullable2, (PXComp) 3),
      (PXDataFieldParam) new PXDataFieldAssign<CRActivity.responseDueDateTime>((object) nullable1)
    });

    static DateTime? TryConvertTime(DateTime? dt)
    {
      return !dt.HasValue ? new DateTime?() : new DateTime?(PXTimeZoneInfo.ConvertTimeToUtc(dt.Value, LocaleInfo.GetTimeZone()));
    }
  }

  /// <exclude />
  public virtual void PersistCaseCommitments(PX.Objects.CR.CRCaseCommitments commitments)
  {
    PXCache<PX.Objects.CR.CRCaseCommitments> pxCache = GraphHelper.Caches<PX.Objects.CR.CRCaseCommitments>((PXGraph) this.Base);
    ((PXCache) pxCache).ResetPersisted((object) commitments);
    pxCache.Update(commitments);
    ((PXCache) pxCache).PersistUpdated((object) commitments);
    ((PXCache) pxCache).Persisted(false);
    ((PXCache) pxCache).Clear();
  }

  /// <exclude />
  public virtual CaseCommitmentsDto ReadCaseCommitmentsDto(CRCase @case)
  {
    CRActivityStatistics activityStatistics = CRActivityStatistics.PK.Find((PXGraph) this.Base, @case.NoteID, (PKFindOptions) 1);
    CaseCommitmentsDto dto = new CaseCommitmentsDto()
    {
      Case = @case,
      Commitments = PX.Objects.CR.CRCaseCommitments.PK.Find((PXGraph) this.Base, @case.CaseCD, (PKFindOptions) 1),
      Class = CRCaseClass.PK.Find((PXGraph) this.Base, @case.CaseClassID, (PKFindOptions) 1),
      ActivityStatistics = activityStatistics,
      SeverityTime = CRClassSeverityTime.PK.Find((PXGraph) this.Base, @case.CaseClassID, @case.Severity, (PKFindOptions) 1),
      FirstUnansweredActivity = ((PXSelectBase<CRPMTimeActivity>) this.FirstUnansweredActivity).SelectSingle(new object[2]
      {
        (object) @case.NoteID,
        (object) (DateTime?) activityStatistics?.LastOutgoingActivityDate
      })
    };
    this.FillOriginals(dto);
    return dto;
  }

  /// <exclude />
  public virtual void FillOriginals(CaseCommitmentsDto dto)
  {
  }

  /// <exclude />
  public virtual void CalculateInitialResponseTime(CaseCommitmentsDto dto)
  {
    (CRCase @case, PX.Objects.CR.CRCaseCommitments commitments, CRCaseClass crCaseClass, CRActivityStatistics activityStatistics, CRClassSeverityTime targetConfigRecord, CRPMTimeActivity _) = dto;
    bool? nullable1 = @case.IsActive;
    DateTime? nullable2;
    if (nullable1.HasValue && nullable1.GetValueOrDefault() && targetConfigRecord != null)
    {
      nullable1 = targetConfigRecord.TrackInitialResponseTime;
      if (nullable1.HasValue && nullable1.GetValueOrDefault())
      {
        nullable2 = (DateTime?) activityStatistics?.InitialOutgoingActivityCompletedAtDate;
        if (!nullable2.HasValue)
        {
          commitments.InitialResponseDueDateTime = this.AddTargetTime(crCaseClass, @case.ReportedOnDateTime, targetConfigRecord.InitialResponseTimeTarget);
          this.CalculateGracePeriodIfNeeded<PX.Objects.CR.CRCaseCommitments.initialResponseDueDateTime, CRClassSeverityTime.initialResponseGracePeriod>(dto);
          return;
        }
      }
    }
    nullable1 = @case.IsActive;
    bool flag1 = (!nullable1.HasValue ? 0 : (nullable1.GetValueOrDefault() ? 1 : 0)) == 0;
    if (!flag1)
    {
      bool flag2;
      if (targetConfigRecord != null)
      {
        nullable1 = targetConfigRecord.TrackInitialResponseTime;
        if (nullable1.HasValue && nullable1.GetValueOrDefault())
        {
          flag2 = false;
          goto label_9;
        }
      }
      flag2 = true;
label_9:
      flag1 = flag2;
    }
    if (!flag1)
    {
      if (activityStatistics == null)
        return;
      nullable2 = activityStatistics.InitialOutgoingActivityCompletedAtDate;
      if (!nullable2.HasValue)
        return;
    }
    PX.Objects.CR.CRCaseCommitments crCaseCommitments = commitments;
    nullable2 = new DateTime?();
    DateTime? nullable3 = nullable2;
    crCaseCommitments.InitialResponseDueDateTime = nullable3;
  }

  /// <exclude />
  public virtual void CalculateResponseTime(CaseCommitmentsDto dto)
  {
    (CRCase @case, PX.Objects.CR.CRCaseCommitments commitments, CRCaseClass crCaseClass, CRActivityStatistics activityStatistics, CRClassSeverityTime targetConfigRecord, CRPMTimeActivity firstUnansweredActivity) = dto;
    bool? nullable1 = @case.IsActive;
    if (nullable1.HasValue && nullable1.GetValueOrDefault() && targetConfigRecord != null)
    {
      nullable1 = targetConfigRecord.TrackResponseTime;
      if (nullable1.HasValue && nullable1.GetValueOrDefault())
      {
        DateTime? nullable2 = (DateTime?) activityStatistics?.InitialOutgoingActivityCompletedAtDate;
        if (!nullable2.HasValue)
        {
          commitments.ResponseDueDateTime = this.AddTargetTime(crCaseClass, @case.ReportedOnDateTime, targetConfigRecord.ResponseTimeTarget);
          this.CalculateGracePeriodIfNeeded<PX.Objects.CR.CRCaseCommitments.responseDueDateTime, CRClassSeverityTime.responseGracePeriod>(dto);
          return;
        }
        nullable2 = (DateTime?) activityStatistics?.LastOutgoingActivityDate;
        if (!nullable2.HasValue)
        {
          commitments.ResponseDueDateTime = this.AddTargetTime(crCaseClass, @case.ReportedOnDateTime, targetConfigRecord.ResponseTimeTarget);
          return;
        }
        nullable2 = (DateTime?) activityStatistics?.LastIncomingActivityDate;
        DateTime? nullable3 = (DateTime?) activityStatistics?.LastOutgoingActivityDate;
        if ((nullable2.HasValue & nullable3.HasValue ? (nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        {
          DateTime? nullable4;
          if (activityStatistics == null)
          {
            nullable2 = new DateTime?();
            nullable4 = nullable2;
          }
          else
            nullable4 = activityStatistics.LastOutgoingActivityDate;
          nullable3 = nullable4;
          if (nullable3.HasValue)
          {
            DateTime? nullable5;
            if (activityStatistics == null)
            {
              nullable2 = new DateTime?();
              nullable5 = nullable2;
            }
            else
              nullable5 = activityStatistics.LastIncomingActivityDate;
            nullable3 = nullable5;
            if (nullable3.HasValue)
            {
              nullable3 = (DateTime?) activityStatistics?.LastOutgoingActivityDate;
              nullable2 = (DateTime?) activityStatistics?.LastIncomingActivityDate;
              if ((nullable3.HasValue & nullable2.HasValue ? (nullable3.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
                return;
            }
            PX.Objects.CR.CRCaseCommitments crCaseCommitments = commitments;
            nullable2 = new DateTime?();
            DateTime? nullable6 = nullable2;
            crCaseCommitments.ResponseDueDateTime = nullable6;
            return;
          }
        }
        PX.Objects.CR.CRCaseCommitments crCaseCommitments1 = commitments;
        CRCaseClass caseClass = crCaseClass;
        DateTime? nullable7;
        if (firstUnansweredActivity == null)
        {
          nullable2 = new DateTime?();
          nullable7 = nullable2;
        }
        else
          nullable7 = firstUnansweredActivity.CompletedDate;
        nullable3 = nullable7;
        DateTime? dateTime;
        if (!nullable3.HasValue)
        {
          if (activityStatistics == null)
          {
            nullable2 = new DateTime?();
            dateTime = nullable2;
          }
          else
            dateTime = activityStatistics.LastIncomingActivityDate;
        }
        else
          dateTime = nullable3;
        int? responseTimeTarget = targetConfigRecord.ResponseTimeTarget;
        DateTime? nullable8 = this.AddTargetTime(caseClass, dateTime, responseTimeTarget);
        crCaseCommitments1.ResponseDueDateTime = nullable8;
        this.CalculateGracePeriodIfNeeded<PX.Objects.CR.CRCaseCommitments.responseDueDateTime, CRClassSeverityTime.responseGracePeriod>(dto);
        return;
      }
    }
    nullable1 = @case.IsActive;
    bool flag1 = (!nullable1.HasValue ? 0 : (nullable1.GetValueOrDefault() ? 1 : 0)) == 0;
    if (!flag1)
    {
      bool flag2;
      if (targetConfigRecord != null)
      {
        nullable1 = targetConfigRecord.TrackInitialResponseTime;
        if (nullable1.HasValue && !nullable1.GetValueOrDefault())
        {
          bool? trackResponseTime = targetConfigRecord.TrackResponseTime;
          if (trackResponseTime.HasValue && !trackResponseTime.GetValueOrDefault())
            goto label_30;
        }
        flag2 = false;
        goto label_32;
      }
label_30:
      flag2 = true;
label_32:
      flag1 = flag2;
    }
    DateTime? nullable9;
    if (!flag1)
    {
      DateTime? nullable10 = (DateTime?) activityStatistics?.LastIncomingActivityDate;
      if (nullable10.HasValue)
      {
        nullable10 = (DateTime?) activityStatistics?.LastOutgoingActivityDate;
        nullable9 = (DateTime?) activityStatistics?.LastIncomingActivityDate;
        if ((nullable10.HasValue & nullable9.HasValue ? (nullable10.GetValueOrDefault() > nullable9.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          return;
      }
    }
    PX.Objects.CR.CRCaseCommitments crCaseCommitments2 = commitments;
    nullable9 = new DateTime?();
    DateTime? nullable11 = nullable9;
    crCaseCommitments2.ResponseDueDateTime = nullable11;
  }

  /// <exclude />
  public virtual void CalculateResolutionTime(CaseCommitmentsDto dto)
  {
    (CRCase @case, PX.Objects.CR.CRCaseCommitments commitments, CRCaseClass crCaseClass, CRActivityStatistics _, CRClassSeverityTime targetConfigRecord, CRPMTimeActivity _) = dto;
    bool? nullable1;
    int? stopTimeCounterType;
    if (targetConfigRecord != null)
    {
      nullable1 = targetConfigRecord.TrackResolutionTime;
      if (nullable1.HasValue && nullable1.GetValueOrDefault())
      {
        stopTimeCounterType = crCaseClass.StopTimeCounterType;
        int num1 = stopTimeCounterType.GetValueOrDefault() != 1 ? 0 : (!@case.SolutionActivityNoteID.HasValue ? 1 : 0);
        stopTimeCounterType = crCaseClass.StopTimeCounterType;
        int num2 = 0;
        int num3;
        if (stopTimeCounterType.GetValueOrDefault() == num2 & stopTimeCounterType.HasValue)
        {
          nullable1 = @case.IsActive;
          num3 = !nullable1.HasValue ? 0 : (nullable1.GetValueOrDefault() ? 1 : 0);
        }
        else
          num3 = 0;
        int num4 = num3 != 0 ? 1 : 0;
        if ((num1 | num4) != 0)
        {
          DateTime? nullable2 = this.AddTargetTime(crCaseClass, @case.ReportedOnDateTime, targetConfigRecord.ResolutionTimeTarget);
          if (nullable2.HasValue)
            commitments.ResolutionDueDateTime = new DateTime?(nullable2.Value.AddMilliseconds((double) -nullable2.Value.Millisecond));
          this.CalculateGracePeriodIfNeeded<PX.Objects.CR.CRCaseCommitments.resolutionDueDateTime, CRClassSeverityTime.resolutionGracePeriod>(dto);
          return;
        }
      }
    }
    stopTimeCounterType = crCaseClass.StopTimeCounterType;
    int num5 = stopTimeCounterType.GetValueOrDefault() != 1 ? 0 : (@case.SolutionActivityNoteID.HasValue ? 1 : 0);
    stopTimeCounterType = crCaseClass.StopTimeCounterType;
    int num6 = 0;
    int num7;
    if (stopTimeCounterType.GetValueOrDefault() == num6 & stopTimeCounterType.HasValue)
    {
      nullable1 = @case.IsActive;
      num7 = !nullable1.HasValue ? 0 : (!nullable1.GetValueOrDefault() ? 1 : 0);
    }
    else
      num7 = 0;
    int num8 = num7 != 0 ? 1 : 0;
    if ((num5 | num8) == 0)
      return;
    commitments.ResolutionDueDateTime = new DateTime?();
  }

  /// <exclude />
  public virtual DateTime? AddTargetTime(
    CRCaseClass caseClass,
    DateTime? dateTime,
    int? targetTimeInMinutes)
  {
    if (!dateTime.HasValue || !targetTimeInMinutes.HasValue)
      return new DateTime?();
    IWorkTimeCalculator workTimeCalculator = this.GetWorkTimeCalculator(caseClass);
    WorkTimeSpan workTimeSpan = workTimeCalculator.ToWorkTimeSpan(TimeSpan.FromMinutes((double) targetTimeInMinutes.Value));
    return new DateTime?(workTimeCalculator.AddWorkTime(DateTimeInfo.FromLocalTimeZone(dateTime.Value), workTimeSpan).DateTime);
  }

  /// <exclude />
  public virtual IWorkTimeCalculator GetWorkTimeCalculator(CRCaseClass caseClass)
  {
    string key = "IWorkTimeCalculator_" + caseClass.CalendarID;
    return PXContext.GetSlot<IWorkTimeCalculator>(key) ?? PXContext.SetSlot<IWorkTimeCalculator>(key, WorkTimeCalculatorProvider.GetWorkTimeCalculator(caseClass.CalendarID));
  }

  /// <exclude />
  public virtual void CalculateGracePeriodIfNeeded<TDueDate, TGracePeriod>(CaseCommitmentsDto dto)
    where TDueDate : IBqlField
    where TGracePeriod : IBqlField
  {
    (CRCase @case, PX.Objects.CR.CRCaseCommitments commitments, CRCaseClass crCaseClass, CRActivityStatistics _, CRClassSeverityTime targetConfigRecord, CRPMTimeActivity _) = dto;
    if (dto.OriginalCase == null || dto.OriginalCommitments == null || @case.Severity == dto.OriginalCase.Severity && @case.CaseClassID == dto.OriginalCase.CaseClassID)
      return;
    PXCache<PX.Objects.CR.CRCaseCommitments> pxCache = GraphHelper.Caches<PX.Objects.CR.CRCaseCommitments>((PXGraph) this.Base);
    DateTime? nullable1 = ((PXCache) pxCache).GetValue<TDueDate>((object) dto.OriginalCommitments) as DateTime?;
    DateTime now = PXTimeZoneInfo.Now;
    DateTime? nullable2 = nullable1;
    DateTime dateTime = now;
    if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() <= dateTime ? 1 : 0) : 0) != 0)
      return;
    DateTime? nullable3 = ((PXCache) pxCache).GetValue<TDueDate>((object) commitments) as DateTime?;
    int? targetTimeInMinutes = this.Base.Caches[typeof (CRClassSeverityTime)].GetValue<TGracePeriod>((object) targetConfigRecord) as int?;
    DateTime? nullable4 = this.AddTargetTime(crCaseClass, new DateTime?(now), targetTimeInMinutes);
    nullable2 = nullable3;
    DateTime? nullable5 = nullable4;
    if ((nullable2.HasValue & nullable5.HasValue ? (nullable2.GetValueOrDefault() >= nullable5.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      return;
    ((PXCache) pxCache).SetValue<TDueDate>((object) commitments, (object) nullable4);
  }

  public virtual void EnsureCaseCommitmentsExists(CRCase @case)
  {
    if (@case == null || !EnumerableExtensions.IsNotIn<PXEntryStatus>(GraphHelper.Caches<CRCase>((PXGraph) this.Base).GetStatus(@case), (PXEntryStatus) 3, (PXEntryStatus) 4) || ((PXSelectBase<PX.Objects.CR.CRCaseCommitments>) this.CaseCommitments)?.SelectSingle(Array.Empty<object>()) != null)
      return;
    ((PXSelectBase<PX.Objects.CR.CRCaseCommitments>) this.CaseCommitments)?.Insert(new PX.Objects.CR.CRCaseCommitments());
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCaseMaint_Extensions.CRCaseClassMaint_CommitmentsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.CS.Services.WorkTimeCalculation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CR.CRCaseMaint_Extensions;

public class CRCaseClassMaint_CommitmentsExt : PXGraphExtension<
#nullable disable
CRCaseClassMaint>
{
  [PXViewName("Reaction")]
  public FbqlSelect<SelectFromBase<CRClassSeverityTime, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  CRClassSeverityTime.caseClassID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CRCaseClass.caseClassID, IBqlString>.FromCurrent>>, 
  #nullable disable
  CRClassSeverityTime>.View CaseClassesReaction;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.caseCommitmentsTracking>();

  protected virtual bool NeedTrackingSolutionsInActivities
  {
    get
    {
      return ((IEnumerable<CRClassSeverityTime>) ((PXSelectBase<CRClassSeverityTime>) this.CaseClassesReaction).SelectMain(Array.Empty<object>())).Any<CRClassSeverityTime>((Func<CRClassSeverityTime, bool>) (c => c.TrackResolutionTime ?? false));
    }
  }

  protected virtual void _(Events.RowPersisting<CRCaseClass> e)
  {
    CRCaseClass row = e.Row;
    if (row == null || e.Operation == 3 || !((PXGraph) this.Base).IsImport)
      return;
    bool? solutionsInActivities = row.TrackSolutionsInActivities;
    if (!solutionsInActivities.HasValue || solutionsInActivities.GetValueOrDefault() || !this.NeedTrackingSolutionsInActivities)
      return;
    row.TrackSolutionsInActivities = new bool?(true);
  }

  protected virtual void _(
    Events.FieldUpdated<CRClassSeverityTime, CRClassSeverityTime.trackResolutionTime> e)
  {
    if (e.Row == null || !this.NeedTrackingSolutionsInActivities)
      return;
    CRCaseClass current = ((PXSelectBase<CRCaseClass>) this.Base.CaseClasses).Current;
    if (current == null)
      return;
    current.TrackSolutionsInActivities = new bool?(true);
    ((PXSelectBase<CRCaseClass>) this.Base.CaseClasses).Update(current);
  }

  protected virtual void _(
    Events.FieldUpdated<CRCaseClass, CRCaseClass.calendarID> e)
  {
    if (e.Row == null || !(e.NewValue is string newValue) || !(((Events.FieldUpdatedBase<Events.FieldUpdated<CRCaseClass, CRCaseClass.calendarID>, CRCaseClass, object>) e).OldValue is string oldValue) || newValue == oldValue)
      return;
    IWorkTimeCalculator oldCalculator = WorkTimeCalculatorProvider.GetWorkTimeCalculator(oldValue);
    IWorkTimeCalculator newCalculator = WorkTimeCalculatorProvider.GetWorkTimeCalculator(newValue);
    foreach (CRClassSeverityTime classSeverityTime in ((PXSelectBase<CRClassSeverityTime>) this.CaseClassesReaction).SelectMain(Array.Empty<object>()))
    {
      CRClassSeverityTime commitments = classSeverityTime;
      Recalculate<CRClassSeverityTime.resolutionGracePeriod>();
      Recalculate<CRClassSeverityTime.resolutionTimeTarget>();
      Recalculate<CRClassSeverityTime.responseGracePeriod>();
      Recalculate<CRClassSeverityTime.responseTimeTarget>();
      Recalculate<CRClassSeverityTime.initialResponseGracePeriod>();
      Recalculate<CRClassSeverityTime.initialResponseTimeTarget>();
      ((PXSelectBase<CRClassSeverityTime>) this.CaseClassesReaction).Update(commitments);

      void Recalculate<TField>() where TField : IBqlField
      {
        if (!(((PXSelectBase) this.CaseClassesReaction).Cache.GetValue<TField>((object) commitments) is int num))
          return;
        TimeSpan timeSpan = TimeSpan.FromMinutes((double) num);
        WorkTimeInfo workTimeInfo = WorkTimeInfo.FromWorkTimeSpan(oldCalculator.ToWorkTimeSpan(timeSpan));
        WorkTimeSpan workTimeSpan = newCalculator.ToWorkTimeSpan(workTimeInfo);
        ((PXSelectBase) this.CaseClassesReaction).Cache.SetValue<TField>((object) commitments, (object) (int) workTimeSpan.TotalMinutes);
      }
    }
  }

  protected virtual void _(Events.RowSelected<CRCaseClass> e)
  {
    if (e.Row == null)
      return;
    CRCaseClass row = e.Row;
    int? stopTimeCounterType = row.StopTimeCounterType;
    if (stopTimeCounterType.HasValue && stopTimeCounterType.GetValueOrDefault() == 1)
    {
      bool? solutionsInActivities = row.TrackSolutionsInActivities;
      if (solutionsInActivities.HasValue && !solutionsInActivities.GetValueOrDefault())
      {
        if (!this.NeedTrackingSolutionsInActivities)
          return;
        ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRCaseClass>>) e).Cache.RaiseExceptionHandling<CRCaseClass.trackSolutionsInActivities>((object) row, (object) row.StopTimeCounterType, (Exception) new PXSetPropertyException<CRCaseClass.trackSolutionsInActivities>("At least one check box is selected for time tracking on the Commitments tab. Consider selecting the Track Solutions in Activities check box to track the resolution time in activities marked as a solution.", (PXErrorLevel) 2));
        return;
      }
    }
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRCaseClass>>) e).Cache.RaiseExceptionHandling<CRCaseClass.trackSolutionsInActivities>((object) row, (object) row.StopTimeCounterType, (Exception) null);
  }
}

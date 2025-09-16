// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCaseCommitments.CRCaseCommitmentsExt_OfCase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.EP;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions.CRCaseCommitments;

/// <exclude />
public abstract class CRCaseCommitmentsExt_OfCase<TGraph> : CRCaseCommitmentsExt<
#nullable disable
TGraph> where TGraph : PXGraph
{
  [PXHidden]
  public FbqlSelect<SelectFromBase<CRClassSeverityTime, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CRClassSeverityTime.caseClassID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CRCase.caseClassID, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  CRClassSeverityTime.severity, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CRCase.severity, IBqlString>.FromCurrent>>>, 
  #nullable disable
  CRClassSeverityTime>.View TargetConfigRecord;
  private const string OriginalCaseFieldStatesSlotName = "CRCaseOriginalCaseFieldStatesSlotName";

  /// <exclude />
  protected bool CommitmentsChanged { get; set; }

  public bool SuspendCalculation { get; set; }

  /// <exclude />
  protected virtual string GetCaseFieldSlotName(CRCase @case)
  {
    return "CRCaseOriginalCaseFieldStatesSlotName_" + @case?.CaseCD;
  }

  /// <exclude />
  [PXOverride]
  public virtual bool PrePersist(Func<bool> prePersist)
  {
    if (!prePersist())
      return false;
    this.EnsureCaseCommitmentsExists(GraphHelper.Caches<CRCase>((PXGraph) this.Base).Rows.Current);
    return true;
  }

  /// <exclude />
  [PXOverride]
  public virtual void PreCommit()
  {
    if (!this.CommitmentsChanged)
      return;
    this.PersistCommitmentsForRelatedActivities(this.ReadCaseCommitmentsDto(GraphHelper.Caches<CRCase>((PXGraph) this.Base).Rows.Current));
  }

  /// <exclude />
  public override void FillOriginals(CaseCommitmentsDto dto)
  {
    PXCache<CRCase> pxCache = GraphHelper.Caches<CRCase>((PXGraph) this.Base);
    if (!EnumerableExtensions.IsIn<PXEntryStatus>(pxCache.GetStatus(dto.Case), (PXEntryStatus) 6, (PXEntryStatus) 1))
      return;
    dto.OriginalCase = pxCache.GetOriginal(dto.Case);
    dto.OriginalCommitments = GraphHelper.Caches<PX.Objects.CR.CRCaseCommitments>((PXGraph) this.Base).GetOriginal(dto.Commitments);
  }

  /// <exclude />
  public virtual void SetStateForOriginalFields(CRCase @case, PX.Objects.CR.CRCaseCommitments commitments)
  {
    var slot = PXContext.EnsureSlot(this.GetCaseFieldSlotName(@case), () =>
    {
      CRCase original = GraphHelper.Caches<CRCase>((PXGraph) this.Base).GetStatus(@case) == 2 ? (CRCase) null : GraphHelper.Caches<CRCase>((PXGraph) this.Base).GetOriginal(@case);
      CRClassSeverityTime classSeverityTime1;
      if (@case != null)
        classSeverityTime1 = ((PXSelectBase) this.TargetConfigRecord).View.SelectSingleBound((object[]) new CRCase[1]
        {
          original
        }, Array.Empty<object>()) as CRClassSeverityTime;
      else
        classSeverityTime1 = (CRClassSeverityTime) null;
      CRClassSeverityTime classSeverityTime2 = classSeverityTime1;
      CRCaseClass parent = @case == null ? (CRCaseClass) null : (CRCaseClass) PrimaryKeyOf<CRCaseClass>.By<CRCaseClass.caseClassID>.ForeignKeyOf<CRCase>.By<CRCase.caseClassID>.FindParent((PXGraph) this.Base, (CRCase.caseClassID) original, (PKFindOptions) 0);
      return new
      {
        TrackInitialResponseTime = (bool?) classSeverityTime2?.TrackInitialResponseTime,
        TrackResponseTime = (bool?) classSeverityTime2?.TrackResponseTime,
        TrackResolutionTime = (bool?) classSeverityTime2?.TrackResolutionTime,
        TrackSolutionsInActivities = (bool?) parent?.TrackSolutionsInActivities
      };
    });
    PXCacheEx.AdjustUI((PXCache) GraphHelper.Caches<CRCase>((PXGraph) this.Base), (object) @case).For<CRCase.solutionActivityNoteID>((Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute1 = ui;
      PXUIFieldAttribute pxuiFieldAttribute2 = ui;
      bool? solutionsInActivities = slot.TrackSolutionsInActivities;
      int num1;
      bool flag = (num1 = !solutionsInActivities.HasValue ? 0 : (solutionsInActivities.GetValueOrDefault() ? 1 : 0)) != 0;
      pxuiFieldAttribute2.Enabled = num1 != 0;
      int num2 = flag ? 1 : 0;
      pxuiFieldAttribute1.Visible = num2 != 0;
    })).For<CRCase.solutionProvidedDateTime>((Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      bool? trackResolutionTime = slot.TrackResolutionTime;
      int num = !trackResolutionTime.HasValue ? 0 : (trackResolutionTime.GetValueOrDefault() ? 1 : 0);
      pxuiFieldAttribute.Visible = num != 0;
    }));
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI(((PXSelectBase) this.CaseCommitments).Cache, (object) commitments).For<PX.Objects.CR.CRCaseCommitments.initialResponseDueDateTime>((Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      bool? initialResponseTime = slot.TrackInitialResponseTime;
      int num = !initialResponseTime.HasValue ? 0 : (initialResponseTime.GetValueOrDefault() ? 1 : 0);
      pxuiFieldAttribute.Visible = num != 0;
    }));
    chained = chained.For<PX.Objects.CR.CRCaseCommitments.headerInitialResponseDueDateTime>((Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      bool? initialResponseTime = slot.TrackInitialResponseTime;
      int num;
      if (initialResponseTime.HasValue && initialResponseTime.GetValueOrDefault() && commitments != null)
      {
        DateTime? nullable = commitments.InitialResponseDueDateTime;
        if (nullable.HasValue)
        {
          DateTime valueOrDefault = nullable.GetValueOrDefault();
          DateTime? responseDueDateTime = commitments.ResponseDueDateTime;
          if (responseDueDateTime.HasValue)
          {
            DateTime dateTime = valueOrDefault;
            nullable = responseDueDateTime;
            num = nullable.HasValue ? (dateTime <= nullable.GetValueOrDefault() ? 1 : 0) : 0;
            goto label_6;
          }
          num = 1;
          goto label_6;
        }
      }
      num = 0;
label_6:
      pxuiFieldAttribute.Visible = num != 0;
    }));
    chained = chained.For<PX.Objects.CR.CRCaseCommitments.responseDueDateTime>((Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      bool? trackResponseTime = slot.TrackResponseTime;
      int num = !trackResponseTime.HasValue ? 0 : (trackResponseTime.GetValueOrDefault() ? 1 : 0);
      pxuiFieldAttribute.Visible = num != 0;
    }));
    chained = chained.For<PX.Objects.CR.CRCaseCommitments.headerResponseDueDateTime>((Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      bool? trackResponseTime = slot.TrackResponseTime;
      int num;
      if (trackResponseTime.HasValue && trackResponseTime.GetValueOrDefault() && commitments != null)
      {
        DateTime? responseDueDateTime = commitments.InitialResponseDueDateTime;
        DateTime? nullable = commitments.ResponseDueDateTime;
        if (nullable.HasValue)
        {
          DateTime valueOrDefault = nullable.GetValueOrDefault();
          if (responseDueDateTime.HasValue)
          {
            DateTime dateTime = valueOrDefault;
            nullable = responseDueDateTime;
            num = nullable.HasValue ? (dateTime < nullable.GetValueOrDefault() ? 1 : 0) : 0;
            goto label_6;
          }
          num = 1;
          goto label_6;
        }
      }
      num = 0;
label_6:
      pxuiFieldAttribute.Visible = num != 0;
    }));
    chained = chained.For<PX.Objects.CR.CRCaseCommitments.resolutionDueDateTime>((Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      bool? trackResolutionTime = slot.TrackResolutionTime;
      int num = !trackResolutionTime.HasValue ? 0 : (trackResolutionTime.GetValueOrDefault() ? 1 : 0);
      pxuiFieldAttribute.Visible = num != 0;
    }));
    chained.For<PX.Objects.CR.CRCaseCommitments.headerResolutionDueDateTime>((Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      bool? nullable = slot.TrackResolutionTime;
      int num;
      if (nullable.HasValue && nullable.GetValueOrDefault())
      {
        if (@case != null)
        {
          nullable = @case.IsActive;
          if (nullable.HasValue)
          {
            num = nullable.GetValueOrDefault() ? 1 : 0;
            goto label_6;
          }
        }
        num = 0;
      }
      else
        num = 0;
label_6:
      pxuiFieldAttribute.Visible = num != 0;
    }));
  }

  /// <exclude />
  [PXOverride]
  public virtual void PostPersist()
  {
    PXContext.ClearSlot(this.GetCaseFieldSlotName(GraphHelper.Caches<CRCase>((PXGraph) this.Base).Rows.Current));
    this.SetStateForOriginalFields(GraphHelper.Caches<CRCase>((PXGraph) this.Base).Rows.Current, ((PXSelectBase<PX.Objects.CR.CRCaseCommitments>) this.CaseCommitments).SelectSingle(Array.Empty<object>()));
  }

  [PXDefault]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRCase.reportedOnDateTime> e)
  {
  }

  [PXFormula(typeof (Concat<TypeArrayOf<IBqlOperand>.FilledWith<Selector<CRActivity.type, EPActivityType.description>, Space, RTrim<CRActivity.startDate>, Space, CRActivity.subject>>))]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CRActivity.selectorDescription> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<CRCase> e)
  {
    this.SetStateForOriginalFields(e.Row, ((PXSelectBase) this.CaseCommitments).View.SelectSingleBound(new object[1]
    {
      (object) e.Row
    }, Array.Empty<object>()) as PX.Objects.CR.CRCaseCommitments);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.CR.CRCaseCommitments> e)
  {
    this.SetStateForOriginalFields(GraphHelper.Caches<CRCase>((PXGraph) this.Base).Rows.Current, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CRCase> e, PXRowPersisting del)
  {
    del?.Invoke(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CRCase>>) e).Cache, ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CRCase>>) e).Args);
    if (e.Row == null || e.Operation == 3 || this.SuspendCalculation)
      return;
    this.CommitmentsChanged = this.CalculateCaseCommitments(this.ReadCaseCommitmentsDto(e.Row));
  }
}

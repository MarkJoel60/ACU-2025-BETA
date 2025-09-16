// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.SingleProjectExtension`7
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.PM;

public abstract class SingleProjectExtension<TGraph, TDocument, TDocumentProjectID, TDocumentHasMultipleProjects, TDocumentExtension, TDetail, TDetailProjectID> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph<TGraph, TDocument>
  where TDocument : class, IProjectHeader, IProjectTaxes, IBqlTable, new()
  where TDocumentProjectID : BqlType<IBqlInt, int>.Field<TDocumentProjectID>
  where TDocumentHasMultipleProjects : BqlType<IBqlBool, bool>.Field<TDocumentHasMultipleProjects>
  where TDocumentExtension : PXCacheExtension, IExtends<TDocument>, ISingleProjectExtension
  where TDetail : class, IProjectLine, IBqlTable, new()
  where TDetailProjectID : BqlType<IBqlInt, int>.Field<TDetailProjectID>
{
  [PXHidden]
  public PXSetup<PMSetup> ProjectSettings;

  public static bool IsExtensionEnabled()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();
  }

  protected abstract PXSelectBase<TDocument> Document { get; }

  protected abstract PXSelectBase<TDetail> Details { get; }

  protected virtual bool IsSingleProjectOnlyMode => false;

  protected virtual bool IsDetailLineIgnored(TDetail? detail) => false;

  protected virtual void _(
    Events.FieldSelecting<TDocument, TDocumentProjectID> e)
  {
    if ((object) e.Row == null || !e.Row.HasMultipleProjects.GetValueOrDefault())
      return;
    ((Events.FieldSelectingBase<Events.FieldSelecting<TDocument, TDocumentProjectID>>) e).ReturnValue = (object) null;
  }

  protected virtual void _(Events.RowInserted<TDocument> e)
  {
    TDocument row = e.Row;
    if ((object) row == null)
      return;
    this.InitializeEmptyDocument(row);
  }

  protected virtual void _(Events.RowInserting<TDetail> e)
  {
    TDetail row = e.Row;
    if ((object) row == null || this.IsDetailLineIgnored(row) || !this.IsFromUI(e.ExternalCall))
      return;
    int? projectId = this.Document.Current.ProjectID;
    if (!projectId.HasValue)
      return;
    int? nullable1 = projectId;
    int? nullable2 = ProjectDefaultAttribute.NonProject();
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    ((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<TDetail>>) e).Cache.SetValue<TDetailProjectID>((object) row, (object) projectId);
    ((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<TDetail>>) e).Cache.SetDefaultExt((object) row, "TaskID", (object) null);
  }

  protected virtual void _(Events.RowInserted<TDetail> e)
  {
    TDetail row = e.Row;
    if ((object) row == null || this.IsDetailLineIgnored(row) || !this.RecalculateHasMultipleProjects(new int?(), row.ProjectID, 1) || !this.IsFromUI(e.ExternalCall))
      return;
    this.CorrectTaxZone();
  }

  protected virtual void CorrectTaxZone()
  {
    if (!new ProjectSettingsManager().CalculateProjectSpecificTaxes)
      return;
    ((PXSelectBase) this.Document).Cache.SetDefaultExt((object) this.Document.Current, "TaxZoneID", (object) null);
  }

  protected virtual void _(Events.RowDeleted<TDetail> e)
  {
    TDetail row = e.Row;
    if ((object) row == null || this.IsDetailLineIgnored(row))
      return;
    PXCache cache = ((PXSelectBase) this.Document).Cache;
    TDocument current = this.Document.Current;
    if ((object) PXResultset<TDetail>.op_Implicit(this.Details.Select(Array.Empty<object>())) != null)
    {
      if (!this.RecalculateHasMultipleProjects(row.ProjectID, new int?(), -1) || !this.IsFromUI(e.ExternalCall))
        return;
      this.CorrectTaxZone();
    }
    else
    {
      this.InitializeEmptyDocument(current);
      cache.SetValue<TDocumentHasMultipleProjects>((object) current, (object) false);
    }
  }

  protected virtual void _(
    Events.FieldVerifying<TDocument, TDocumentProjectID> e)
  {
    if (((Events.FieldVerifyingBase<Events.FieldVerifying<TDocument, TDocumentProjectID>, TDocument, object>) e).NewValue != null || e.OldValue == null || this.IsSingleProjectOnlyMode)
      return;
    ((Events.FieldVerifyingBase<Events.FieldVerifying<TDocument, TDocumentProjectID>, TDocument, object>) e).NewValue = (object) ProjectDefaultAttribute.NonProject();
  }

  protected virtual void _(
    Events.FieldUpdating<TDocument, TDocumentProjectID> e)
  {
    TDocument row = e.Row;
    if ((object) row == null || !this.IsFromUI(true) || !row.HasMultipleProjects.GetValueOrDefault())
      return;
    int? oldValue = e.OldValue as int?;
    int? nullable = ProjectDefaultAttribute.NonProject();
    if (!(oldValue.GetValueOrDefault() == nullable.GetValueOrDefault() & oldValue.HasValue == nullable.HasValue) || !(((Events.FieldUpdatingBase<Events.FieldUpdating<TDocument, TDocumentProjectID>>) e).NewValue as string == ((PXSelectBase<PMSetup>) this.ProjectSettings).Current.NonProjectCode))
      return;
    this.SetSingleProject(row, ProjectDefaultAttribute.NonProject());
  }

  protected virtual void _(
    Events.FieldUpdated<TDocument, TDocumentProjectID> e)
  {
    TDocument row = e.Row;
    if ((object) row == null || !this.IsFromUI(((Events.FieldUpdatedBase<Events.FieldUpdated<TDocument, TDocumentProjectID>>) e).ExternalCall))
      return;
    this.SetSingleProject(row, e.NewValue as int?);
  }

  protected virtual void SetSingleProject(TDocument document, int? newProjectID)
  {
    if (!newProjectID.HasValue)
      return;
    int num = 0;
    foreach (PXResult<TDetail> pxResult in this.Details.Select(Array.Empty<object>()))
    {
      TDetail detail = PXResult<TDetail>.op_Implicit(pxResult);
      ++num;
      int? projectId = detail.ProjectID;
      int? nullable1 = newProjectID;
      if (!(projectId.GetValueOrDefault() == nullable1.GetValueOrDefault() & projectId.HasValue == nullable1.HasValue))
      {
        ((PXSelectBase) this.Details).Cache.SetValue<TDetailProjectID>((object) detail, (object) newProjectID);
        if (PXAccess.FeatureInstalled<FeaturesSet.costCodes>())
        {
          nullable1 = newProjectID;
          int? nullable2 = ProjectDefaultAttribute.NonProject();
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
            ((PXSelectBase) this.Details).Cache.SetValue((object) detail, "CostCodeID", (object) null);
        }
        try
        {
          this.Details.Update(detail);
        }
        catch (PXException ex)
        {
          PXFieldState stateExt = (PXFieldState) ((PXSelectBase) this.Document).Cache.GetStateExt<TDocumentProjectID>((object) document);
          ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<TDetailProjectID>((object) detail, stateExt.Value, (Exception) ex);
        }
      }
    }
    long sumId = this.GetSumID(newProjectID);
    this.SetProjectIDSums(document, new int?(num), new long?((long) num * sumId), new long?((long) num * sumId * sumId));
    ((PXSelectBase) this.Document).Cache.SetValueExt<TDocumentHasMultipleProjects>((object) document, (object) false);
  }

  protected virtual void _(Events.RowUpdated<TDetail> e)
  {
    TDetail oldRow = e.OldRow;
    TDetail row = e.Row;
    if ((object) oldRow == null || (object) row == null)
      return;
    int? projectId1 = oldRow.ProjectID;
    int? projectId2 = row.ProjectID;
    if (projectId1.GetValueOrDefault() == projectId2.GetValueOrDefault() & projectId1.HasValue == projectId2.HasValue || this.IsDetailLineIgnored(row) || ((PXSelectBase) this.Details).Cache.GetStatus((object) row) == null || !this.RecalculateHasMultipleProjects(oldRow.ProjectID, row.ProjectID, 0) || !this.IsFromUI(e.ExternalCall))
      return;
    this.CorrectTaxZone();
  }

  protected virtual void SetProjectIDSums(
    TDocument document,
    int? count,
    long? sumID,
    long? squareSumID)
  {
    ((PXSelectBase) this.Document).Cache.SetValue((object) document, "DetailCount", (object) count);
    ((PXSelectBase) this.Document).Cache.SetValue((object) document, "SumProjectID", (object) sumID);
    ((PXSelectBase) this.Document).Cache.SetValue((object) document, "SquareSumProjectID", (object) squareSumID);
  }

  protected virtual void InitializeEmptyDocument(TDocument document)
  {
    this.SetProjectIDSums(document, new int?(0), new long?(0L), new long?(0L));
  }

  protected virtual bool RecalculateHasMultipleProjects(
    int? oldProjectID,
    int? newProjectID,
    int countChange)
  {
    TDocument current = this.Document.Current;
    if ((object) current == null)
      return false;
    int? projectId = current.ProjectID;
    (int? detailCount, long? sumProjectID, long? squareSumProjectID) = this.GetSingleProjectExtension(current);
    int? count = detailCount;
    long? sumID = sumProjectID;
    long? squareSumID = squareSumProjectID;
    long sumId1 = this.GetSumID(newProjectID);
    if (!count.HasValue || !sumID.HasValue || !squareSumID.HasValue)
    {
      (int Count, long SumID, long SquareSumID) projectIdSums = this.CalculateProjectIDSums();
      detailCount = new int?(projectIdSums.Count);
      squareSumProjectID = new long?(projectIdSums.SumID);
      long? nullable = new long?(projectIdSums.SquareSumID);
      count = detailCount;
      sumID = squareSumProjectID;
      squareSumID = nullable;
    }
    else
    {
      long sumId2 = this.GetSumID(oldProjectID);
      long? nullable1 = sumID;
      long num1 = sumId1 - sumId2;
      long? nullable2;
      if (!nullable1.HasValue)
      {
        sumProjectID = new long?();
        nullable2 = sumProjectID;
      }
      else
        nullable2 = new long?(nullable1.GetValueOrDefault() + num1);
      sumID = nullable2;
      long? nullable3 = squareSumID;
      long num2 = sumId1 * sumId1 - sumId2 * sumId2;
      long? nullable4;
      if (!nullable3.HasValue)
      {
        sumProjectID = new long?();
        nullable4 = sumProjectID;
      }
      else
        nullable4 = new long?(nullable3.GetValueOrDefault() + num2);
      squareSumID = nullable4;
      int? nullable5 = count;
      int num3 = countChange;
      count = nullable5.HasValue ? new int?(nullable5.GetValueOrDefault() + num3) : new int?();
    }
    this.SetProjectIDSums(current, count, sumID, squareSumID);
    if (countChange < 0)
    {
      TDetail detail = PXResultset<TDetail>.op_Implicit(this.Details.Select(Array.Empty<object>()));
      int? nullable;
      if ((object) detail == null)
      {
        detailCount = new int?();
        nullable = detailCount;
      }
      else
        nullable = detail.ProjectID;
      newProjectID = nullable;
      sumId1 = this.GetSumID(newProjectID);
    }
    long? nullable6 = sumID;
    detailCount = count;
    long? nullable7 = detailCount.HasValue ? new long?((long) detailCount.GetValueOrDefault()) : new long?();
    long num4 = sumId1;
    sumProjectID = nullable7.HasValue ? new long?(nullable7.GetValueOrDefault() * num4) : new long?();
    int num5;
    if (nullable6.GetValueOrDefault() == sumProjectID.GetValueOrDefault() & nullable6.HasValue == sumProjectID.HasValue)
    {
      sumProjectID = squareSumID;
      detailCount = count;
      long? nullable8 = detailCount.HasValue ? new long?((long) detailCount.GetValueOrDefault()) : new long?();
      long num6 = sumId1;
      nullable7 = nullable8.HasValue ? new long?(nullable8.GetValueOrDefault() * num6) : new long?();
      long num7 = sumId1;
      long? nullable9;
      if (!nullable7.HasValue)
      {
        nullable8 = new long?();
        nullable9 = nullable8;
      }
      else
        nullable9 = new long?(nullable7.GetValueOrDefault() * num7);
      nullable6 = nullable9;
      num5 = !(sumProjectID.GetValueOrDefault() == nullable6.GetValueOrDefault() & sumProjectID.HasValue == nullable6.HasValue) ? 1 : 0;
    }
    else
      num5 = 1;
    bool flag = num5 != 0;
    ((PXSelectBase) this.Document).Cache.SetValue<TDocumentHasMultipleProjects>((object) current, (object) flag);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Document).Cache, (object) current);
    int? nullable10;
    if (!flag && newProjectID.HasValue)
    {
      detailCount = newProjectID;
      nullable10 = projectId;
      if (!(detailCount.GetValueOrDefault() == nullable10.GetValueOrDefault() & detailCount.HasValue == nullable10.HasValue))
      {
        ((PXSelectBase) this.Document).Cache.SetValue<TDocumentProjectID>((object) current, (object) newProjectID);
        GraphHelper.MarkUpdated(((PXSelectBase) this.Document).Cache, (object) current);
        return true;
      }
    }
    if (flag)
    {
      nullable10 = projectId;
      detailCount = ProjectDefaultAttribute.NonProject();
      if (!(nullable10.GetValueOrDefault() == detailCount.GetValueOrDefault() & nullable10.HasValue == detailCount.HasValue))
      {
        ((PXSelectBase) this.Document).Cache.SetValue<TDocumentProjectID>((object) current, (object) ProjectDefaultAttribute.NonProject());
        GraphHelper.MarkUpdated(((PXSelectBase) this.Document).Cache, (object) current);
        return true;
      }
    }
    return false;
  }

  protected (int Count, long SumID, long SquareSumID) CalculateProjectIDSums()
  {
    int num1 = 0;
    long num2 = 0;
    long num3 = 0;
    foreach (PXResult<TDetail> pxResult in this.Details.Select(Array.Empty<object>()))
    {
      TDetail detail = PXResult<TDetail>.op_Implicit(pxResult);
      ++num1;
      long sumId = this.GetSumID(detail.ProjectID);
      num2 += sumId;
      num3 += sumId * sumId;
    }
    return (num1, num2, num3);
  }

  protected long GetSumID(int? id)
  {
    long sumId;
    if (!id.HasValue)
    {
      sumId = 0L;
    }
    else
    {
      int? nullable1 = id;
      int? nullable2 = ProjectDefaultAttribute.NonProject();
      sumId = !(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) ? (long) id.Value : -1L;
    }
    return sumId;
  }

  protected ISingleProjectExtension GetSingleProjectExtension(TDocument document)
  {
    return (ISingleProjectExtension) ((PXSelectBase) this.Document).Cache.GetExtension<TDocumentExtension>((object) document);
  }

  protected bool IsFromUI(bool externalCall)
  {
    return externalCall && !((PXGraph) (object) this.Base).IsImport && !((PXGraph) (object) this.Base).IsImportFromExcel && !((PXGraph) (object) this.Base).IsCopyPasteContext;
  }
}

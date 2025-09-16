// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.TranslationEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CM;

[TableAndChartDashboardType]
public class TranslationEnq : PXGraph<TranslationEnq>
{
  public PXAction<TranslationEnqFilter> cancel;
  public PXAction<TranslationEnqFilter> viewTranslatedBatch;
  [Obsolete("Will be removed in Acumatica 2019R1")]
  public PXAction<TranslationEnqFilter> viewDetails;
  public PXFilter<TranslationEnqFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelect<TranslationHistory> TranslationHistoryRecords;
  public PXSetup<PX.Objects.CM.CMSetup> CMSetup;

  [PXUIField]
  [PXCancelButton]
  protected virtual IEnumerable Cancel(PXAdapter adapter)
  {
    ((PXSelectBase) this.TranslationHistoryRecords).Cache.Clear();
    ((PXSelectBase) this.Filter).Cache.Clear();
    ((PXGraph) this).TimeStamp = (byte[]) null;
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewTranslatedBatch(PXAdapter adapter)
  {
    if (((PXSelectBase<TranslationHistory>) this.TranslationHistoryRecords).Current != null && ((PXSelectBase<TranslationHistory>) this.TranslationHistoryRecords).Current.BatchNbr != null)
    {
      JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
      ((PXGraph) instance).Clear();
      Batch batch = new Batch();
      ((PXSelectBase<Batch>) instance.BatchModule).Current = PXResultset<Batch>.op_Implicit(PXSelectBase<Batch, PXSelect<Batch, Where<Batch.module, Equal<Required<Batch.module>>, And<Batch.batchNbr, Equal<Required<Batch.batchNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) "CM",
        (object) ((PXSelectBase<TranslationHistory>) this.TranslationHistoryRecords).Current.BatchNbr
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Translation Batch");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return (IEnumerable) ((PXSelectBase<TranslationEnqFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDetails(PXAdapter adapter)
  {
    if (((PXSelectBase<TranslationHistory>) this.TranslationHistoryRecords).Current != null && ((PXSelectBase<TranslationHistory>) this.TranslationHistoryRecords).Current.ReferenceNbr != null)
    {
      TranslationHistoryMaint instance = PXGraph.CreateInstance<TranslationHistoryMaint>();
      ((PXGraph) instance).Clear();
      TranslationHistory translationHistory = new TranslationHistory();
      ((PXSelectBase<TranslationHistory>) instance.TranslHistRecords).Current = PXResultset<TranslationHistory>.op_Implicit(PXSelectBase<TranslationHistory, PXSelect<TranslationHistory, Where<TranslationHistory.referenceNbr, Equal<Required<TranslationHistory.referenceNbr>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<TranslationHistory>) this.TranslationHistoryRecords).Current.ReferenceNbr
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Translation Details");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return (IEnumerable) ((PXSelectBase<TranslationEnqFilter>) this.Filter).Select(Array.Empty<object>());
  }

  public TranslationEnq()
  {
    PX.Objects.CM.CMSetup current = ((PXSelectBase<PX.Objects.CM.CMSetup>) this.CMSetup).Current;
  }

  protected virtual IEnumerable translationHistoryRecords()
  {
    TranslationEnq translationEnq = this;
    TranslationEnqFilter current = ((PXSelectBase<TranslationEnqFilter>) translationEnq.Filter).Current;
    ((PXSelectBase) translationEnq.TranslationHistoryRecords).Cache.AllowInsert = false;
    ((PXSelectBase) translationEnq.TranslationHistoryRecords).Cache.AllowDelete = false;
    ((PXSelectBase) translationEnq.TranslationHistoryRecords).Cache.AllowUpdate = false;
    ((PXSelectBase) translationEnq.TranslationHistoryRecords).Cache.Clear();
    PXSelectBase<TranslationHistory> pxSelectBase = (PXSelectBase<TranslationHistory>) new PXSelectOrderBy<TranslationHistory, OrderBy<Desc<TranslationHistory.finPeriodID, Desc<TranslationHistory.translDefId, Asc<TranslationHistory.referenceNbr>>>>>((PXGraph) translationEnq);
    if (current.TranslDefId != null)
      pxSelectBase.WhereAnd<Where<TranslationHistory.translDefId, Equal<Current<TranslationEnqFilter.translDefId>>>>();
    if (current.FinPeriodID != null)
      pxSelectBase.WhereAnd<Where<TranslationHistory.finPeriodID, Equal<Current<TranslationEnqFilter.finPeriodID>>>>();
    bool? nullable = current.Released;
    bool flag1 = false;
    if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      pxSelectBase.WhereAnd<Where<TranslationHistory.status, NotEqual<TranslationReleased>>>();
    nullable = current.Unreleased;
    bool flag2 = false;
    if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
      pxSelectBase.WhereAnd<Where<TranslationHistory.status, NotEqual<TranslationUnReleased>>>();
    nullable = current.Voided;
    bool flag3 = false;
    if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
      pxSelectBase.WhereAnd<Where<TranslationHistory.status, NotEqual<TranslationVoided>>>();
    foreach (object obj in pxSelectBase.Select(Array.Empty<object>()))
      yield return obj;
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
  }

  protected virtual void TranslationHistory_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    TranslationHistory row = (TranslationHistory) e.Row;
    PXUIFieldAttribute.SetEnabled<TranslationHistory.dateEntered>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<TranslationHistory.description>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<TranslationHistory.ledgerID>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<TranslationHistory.finPeriodID>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<TranslationHistory.referenceNbr>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<TranslationHistory.batchNbr>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<TranslationHistory.status>(cache, (object) row, false);
  }
}

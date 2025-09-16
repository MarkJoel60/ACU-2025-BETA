// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ScheduleMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.ProjectDefinition.Workflow;
using PX.Data.WorkflowAPI;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.EP;
using PX.Objects.GL.Overrides.ScheduleMaint;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.GL;

public class ScheduleMaint : 
  ScheduleMaintBase<PX.Objects.GL.ScheduleMaint, ScheduleProcess>,
  IWorkflowExpressionParserParametersProvider
{
  public PXSelect<Batch, Where<Batch.scheduleID, Equal<Current<Schedule.scheduleID>>, And<Batch.scheduled, Equal<False>>>> Batch_History;
  public PXSelect<BatchSelection, Where<BatchSelection.scheduleID, Equal<Current<Schedule.scheduleID>>, And<BatchSelection.scheduled, Equal<True>>>> Batch_Detail;
  public PXSelect<Batch, Where<Batch.module, Equal<Required<Batch.module>>, And<Batch.batchNbr, Equal<Required<Batch.batchNbr>>>>> _dummyBatch;
  public PXSelect<GLTran> GLTransactions;
  [Obsolete("Will be removed in Acumatica 2019R1")]
  public PXSelect<CATran> CATransactions;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  public PXAction<Schedule> viewBatchD;
  public PXAction<Schedule> viewBatch;

  public Dictionary<string, ExpressionParameterInfo> GetAvailableExpressionParameters()
  {
    Dictionary<string, ExpressionParameterInfo> expressionParameters = new Dictionary<string, ExpressionParameterInfo>();
    ExpressionParameterInfo expressionParameterInfo = new ExpressionParameterInfo();
    ((ExpressionParameterInfo) ref expressionParameterInfo).Value = (object) ((PXSelectBase<Schedule>) this.Schedule_Header).Current?.ScheduleID;
    expressionParameters.Add("ScheduleID", expressionParameterInfo);
    return expressionParameters;
  }

  public ScheduleMaint()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
    ((PXSelectBase) this.Batch_History).Cache.AllowDelete = false;
    ((PXSelectBase) this.Batch_History).Cache.AllowInsert = false;
    ((PXSelectBase) this.Batch_History).Cache.AllowUpdate = false;
    ((PXSelectBase<Schedule>) this.Schedule_Header).WhereAnd<Where<Schedule.module, Equal<BatchModule.moduleGL>>>();
    PXNoteAttribute.ForceRetain<Batch.noteID>(((PXSelectBase) this.Batch_Detail).Cache);
    GraphHelper.EnsureCachePersistence<Batch>((PXGraph) this);
  }

  internal override bool AnyScheduleDetails()
  {
    return ((PXSelectBase<BatchSelection>) this.Batch_Detail).Any<BatchSelection>();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewBatchD(PXAdapter adapter)
  {
    Batch current = (Batch) ((PXSelectBase<BatchSelection>) this.Batch_Detail).Current;
    if (current != null)
    {
      JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
      ((PXSelectBase<Batch>) instance.BatchModule).Current = (Batch) ((PXSelectBase) this.Batch_Detail).Cache.CreateCopy((object) (BatchSelection) current);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Batch");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    Batch current = ((PXSelectBase<Batch>) this.Batch_History).Current;
    if (current != null)
    {
      JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
      ((PXSelectBase<Batch>) instance.BatchModule).Current = current;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Batch");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Module", Enabled = false, IsReadOnly = true, Visible = false)]
  protected virtual void Batch_Module_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  [GLCashTranID]
  protected virtual void GLTran_CATranID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void BatchSelection_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null || string.IsNullOrWhiteSpace(((Batch) e.Row).BatchNbr))
      return;
    PXCache pxCache1 = sender;
    object row1 = e.Row;
    bool? scheduled = ((Batch) e.Row).Scheduled;
    int num1 = !scheduled.Value ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<BatchSelection.module>(pxCache1, row1, num1 != 0);
    PXCache pxCache2 = sender;
    object row2 = e.Row;
    scheduled = ((Batch) e.Row).Scheduled;
    int num2 = !scheduled.Value ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<BatchSelection.batchNbr>(pxCache2, row2, num2 != 0);
  }

  protected override void SetControlsState(PXCache cache, Schedule s)
  {
    base.SetControlsState(cache, s);
    PXUIFieldAttribute.SetEnabled<BatchSelection.module>(((PXSelectBase) this.Batch_Detail).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<BatchSelection.batchNbr>(((PXSelectBase) this.Batch_Detail).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<Batch.ledgerID>(((PXSelectBase) this.Batch_Detail).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<Batch.dateEntered>(((PXSelectBase) this.Batch_Detail).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<BatchSelection.finPeriodID>(((PXSelectBase) this.Batch_Detail).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<Batch.curyControlTotal>(((PXSelectBase) this.Batch_Detail).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<Batch.curyID>(((PXSelectBase) this.Batch_Detail).Cache, (object) null, false);
  }

  protected virtual void BatchSelection_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (e.Row is Batch row1)
    {
      bool? voided = row1.Voided;
      bool flag = false;
      if (voided.GetValueOrDefault() == flag & voided.HasValue)
      {
        row1.Scheduled = new bool?(true);
        row1.ScheduleID = ((PXSelectBase<Schedule>) this.Schedule_Header).Current.ScheduleID;
      }
    }
    if (!(e.Row is BatchSelection row2) || string.IsNullOrWhiteSpace(row2.Module) || string.IsNullOrWhiteSpace(row2.BatchNbr) || PXSelectorAttribute.Select<BatchSelection.batchNbr>(cache, (object) row2) != null)
      return;
    cache.RaiseExceptionHandling<BatchSelection.batchNbr>((object) row2, (object) row2.BatchNbr, (Exception) new PXSetPropertyException("Batch Number is not valid!"));
    ((PXSelectBase) this.Batch_Detail).Cache.Remove((object) row2);
  }

  protected virtual void BatchSelection_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is BatchSelection row1) || string.IsNullOrWhiteSpace(row1.Module) || string.IsNullOrWhiteSpace(row1.BatchNbr))
      return;
    BatchSelection batchSelection = PXResultset<BatchSelection>.op_Implicit(PXSelectBase<BatchSelection, PXSelectReadonly<BatchSelection, Where<BatchSelection.module, Equal<Required<BatchSelection.module>>, And<BatchSelection.batchNbr, Equal<Required<BatchSelection.batchNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row1.Module,
      (object) row1.BatchNbr
    }));
    BqlCommand select = ((PXSelectorAttribute) sender.GetAttributesReadonly<BatchSelection.batchNbr>((object) batchSelection).FirstOrDefault<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (attr => attr is PXSelectorAttribute))).GetSelect();
    if (batchSelection != null && select.Meet(sender, (object) batchSelection, Array.Empty<object>()))
    {
      ((PXSelectBase<BatchSelection>) this.Batch_Detail).Delete(batchSelection);
      ((PXSelectBase<BatchSelection>) this.Batch_Detail).Update(batchSelection);
    }
    else
    {
      BatchSelection row2 = (BatchSelection) e.Row;
      sender.RaiseExceptionHandling<BatchSelection.batchNbr>((object) row2, (object) row2.BatchNbr, (Exception) new PXSetPropertyException("Batch Number is not valid!"));
      ((PXSelectBase<BatchSelection>) this.Batch_Detail).Delete(row2);
    }
  }

  protected virtual void BatchSelection_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
    if (e.Operation == 3)
      return;
    BatchSelection document = e.Row as BatchSelection;
    if (document == null || GraphHelper.RowCast<Batch>(((PXSelectBase) this._dummyBatch).Cache.Updated).FirstOrDefault<Batch>((Func<Batch, bool>) (p => p.Module == document.Module && p.BatchNbr == document.BatchNbr)) != null)
      return;
    Batch batch = PXResultset<Batch>.op_Implicit(((PXSelectBase<Batch>) this._dummyBatch).Select(new object[2]
    {
      (object) document.Module,
      (object) document.BatchNbr
    }));
    ((PXSelectBase) this._dummyBatch).Cache.SetStatus((object) batch, (PXEntryStatus) 1);
    PXDBTimestampAttribute timestampAttribute = ((PXSelectBase) this._dummyBatch).Cache.GetAttributesOfType<PXDBTimestampAttribute>((object) null, "Tstamp").First<PXDBTimestampAttribute>();
    VerifyTimestampOptions verifyTimestamp = timestampAttribute.VerifyTimestamp;
    timestampAttribute.VerifyTimestamp = (VerifyTimestampOptions) 1;
    try
    {
      ((PXSelectBase) this._dummyBatch).Cache.PersistUpdated((object) batch);
    }
    finally
    {
      timestampAttribute.VerifyTimestamp = verifyTimestamp;
    }
  }

  protected virtual void Schedule_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    foreach (PXResult<BatchSelection> pxResult in PXSelectBase<BatchSelection, PXSelect<BatchSelection, Where<BatchSelection.scheduleID, Equal<Required<Schedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((Schedule) e.Row).ScheduleID
    }))
      ((PXSelectBase<BatchSelection>) this.Batch_Detail).Delete(PXResult<BatchSelection>.op_Implicit(pxResult));
  }

  protected virtual void Batch_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != null || e.Operation != 1)
      return;
    this.CleanScheduleBatch(e.Row as Batch);
  }

  private void CleanScheduleBatch(Batch batch)
  {
    JournalEntry je = PXContext.GetSlot<JournalEntry>();
    if (je == null)
    {
      je = PXGraph.CreateInstance<JournalEntry>();
      PXContext.SetSlot<JournalEntry>(je);
    }
    ((PXGraph) je).Clear();
    ((PXGraph) je).SelectTimeStamp();
    Batch batch1 = PXResultset<Batch>.op_Implicit(PXSelectBase<Batch, PXSelect<Batch, Where<Batch.batchType, Equal<Required<Batch.batchType>>, And<Batch.batchNbr, Equal<Required<Batch.batchNbr>>>>>.Config>.Select((PXGraph) je, new object[2]
    {
      (object) batch.BatchType,
      (object) batch.BatchNbr
    }));
    ((PXSelectBase<Batch>) je.BatchModule).Current = batch1;
    EnumerableExtensions.ForEach<EPApproval>(GraphHelper.RowCast<EPApproval>((IEnumerable) ((PXSelectBase<EPApproval>) je.Approval).Select(Array.Empty<object>())), (Action<EPApproval>) (a => ((PXSelectBase<EPApproval>) je.Approval).Delete(a)));
    ((PXAction) je.Save).Press();
  }

  protected virtual void Schedule_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus == null)
    {
      foreach (BatchSelection updatedBatch in ((PXSelectBase) this.Batch_Detail).Cache.Cached)
      {
        PXEntryStatus status = ((PXSelectBase) this.Batch_Detail).Cache.GetStatus((object) updatedBatch);
        if ((status == 2 || status == 1) && !updatedBatch.Voided.GetValueOrDefault())
        {
          ((SelectedEntityEvent<Batch, Schedule>) PXEntityEventBase<Batch>.Container<Batch.Events>.Select<Schedule>((Expression<Func<Batch.Events, PXEntityEvent<Batch.Events, Schedule>>>) (ev => ev.ConfirmSchedule))).FireOn((PXGraph) this, (Batch) updatedBatch, ((PXSelectBase<Schedule>) this.Schedule_Header).Current);
          this.UpdateTransactionsLedgerBalanceType((Batch) updatedBatch);
        }
        if (status == 3)
        {
          ((SelectedEntityEvent<Batch, Schedule>) PXEntityEventBase<Batch>.Container<Batch.Events>.Select<Schedule>((Expression<Func<Batch.Events, PXEntityEvent<Batch.Events, Schedule>>>) (ev => ev.VoidSchedule))).FireOn((PXGraph) this, (Batch) updatedBatch, ((PXSelectBase<Schedule>) this.Schedule_Header).Current);
          Batch batch = ((PXSelectBase<Batch>) this.Batch_History).Locate((Batch) updatedBatch);
          if (batch != null)
            ((PXSelectBase) this.Batch_History).Cache.RestoreCopy((object) batch, (object) updatedBatch);
        }
      }
    }
    if (e.TranStatus != 1)
      return;
    ((PXSelectBase) this.Batch_Detail).Cache.Clear();
    ((PXSelectBase) this.Batch_Detail).View.Clear();
  }

  private void UpdateTransactionsLedgerBalanceType(Batch updatedBatch)
  {
    foreach (PXResult<GLTran> pxResult in PXSelectBase<GLTran, PXSelect<GLTran, Where<GLTran.module, Equal<Current<Batch.module>>, And<GLTran.batchNbr, Equal<Current<Batch.batchNbr>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) updatedBatch
    }, Array.Empty<object>()))
    {
      GLTran glTran = PXResult<GLTran>.op_Implicit(pxResult);
      glTran.LedgerBalanceType = "N";
      ((PXSelectBase<GLTran>) this.GLTransactions).Update(glTran);
    }
  }
}

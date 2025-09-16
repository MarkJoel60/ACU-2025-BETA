// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARScheduleMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.ProjectDefinition.Workflow;
using PX.Data.WorkflowAPI;
using PX.Objects.AR.Overrides.ScheduleMaint;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AR;

public class ARScheduleMaint : 
  ScheduleMaintBase<ARScheduleMaint, ARScheduleProcess>,
  IWorkflowExpressionParserParametersProvider
{
  public PXSelect<Customer> Customers;
  public PXSelect<ARRegister, Where<ARRegister.scheduleID, Equal<Current<PX.Objects.GL.Schedule.scheduleID>>, And<ARRegister.scheduled, Equal<False>>>> Document_History;
  public PXSelect<DocumentSelection, Where<DocumentSelection.scheduleID, Equal<Current<PX.Objects.GL.Schedule.scheduleID>>, And<DocumentSelection.scheduled, Equal<True>>>> Document_Detail;
  public PXSelect<ARInvoice, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>> _dummyInvoice;
  public PXSelect<ARBalances> arbalances;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  public PXAction<PX.Objects.GL.Schedule> viewDocument;
  public PXAction<PX.Objects.GL.Schedule> viewGenDocument;

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [AutoNumber(typeof (PX.Objects.GL.GLSetup.scheduleNumberingID), typeof (AccessInfo.businessDate))]
  [PXSelector(typeof (Search2<PX.Objects.GL.Schedule.scheduleID, LeftJoin<ARRegisterAccess, On<ARRegisterAccess.scheduleID, Equal<PX.Objects.GL.Schedule.scheduleID>, And<ARRegisterAccess.scheduled, Equal<True>, And<Not<Match<ARRegisterAccess, Current<AccessInfo.userName>>>>>>>, Where<PX.Objects.GL.Schedule.module, Equal<BatchModule.moduleAR>, And<ARRegisterAccess.docType, IsNull>>>))]
  [PXDefault]
  protected virtual void Schedule_ScheduleID_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", false)]
  protected virtual void _(PX.Data.Events.CacheAttached<ARInvoice.invoiceNbr> e)
  {
  }

  public ARScheduleMaint()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    ((PXSelectBase) this.Document_History).Cache.AllowDelete = false;
    ((PXSelectBase) this.Document_History).Cache.AllowInsert = false;
    ((PXSelectBase) this.Document_History).Cache.AllowUpdate = false;
    ((PXSelectBase<PX.Objects.GL.Schedule>) this.Schedule_Header).Join<LeftJoin<ARRegisterAccess, On<ARRegisterAccess.scheduleID, Equal<PX.Objects.GL.Schedule.scheduleID>, And<ARRegisterAccess.scheduled, Equal<True>, And<Not<Match<ARRegisterAccess, Current<AccessInfo.userName>>>>>>>>();
    ((PXSelectBase<PX.Objects.GL.Schedule>) this.Schedule_Header).WhereAnd<Where<PX.Objects.GL.Schedule.module, Equal<BatchModule.moduleAR>, And<ARRegisterAccess.docType, IsNull>>>();
    PXNoteAttribute.ForceRetain<ARRegister.noteID>(((PXSelectBase) this.Document_Detail).Cache);
    GraphHelper.EnsureCachePersistence<ARInvoice>((PXGraph) this);
    GraphHelper.EnsureCachePersistence<ARPayment>((PXGraph) this);
  }

  protected override string Module => "AR";

  internal override bool AnyScheduleDetails()
  {
    return ((PXSelectBase<DocumentSelection>) this.Document_Detail).Any<DocumentSelection>();
  }

  protected virtual void DocumentSelection_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<DocumentSelection.docType>(sender, e.Row, !((ARRegister) e.Row).Scheduled.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<DocumentSelection.refNbr>(sender, e.Row, !((ARRegister) e.Row).Scheduled.GetValueOrDefault());
  }

  protected override void SetControlsState(PXCache cache, PX.Objects.GL.Schedule schedule)
  {
    base.SetControlsState(cache, schedule);
    bool flag = !schedule.LastRunDate.HasValue;
    PXUIFieldAttribute.SetEnabled<PX.Objects.GL.Schedule.nextRunDate>(cache, (object) schedule, !flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.GL.Schedule.formScheduleType>(cache, (object) schedule, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.GL.Schedule.startDate>(cache, (object) schedule, flag);
    PXUIFieldAttribute.SetEnabled<ARRegister.customerID>(((PXSelectBase) this.Document_Detail).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARRegister.status>(((PXSelectBase) this.Document_Detail).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARRegister.docDate>(((PXSelectBase) this.Document_Detail).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<DocumentSelection.finPeriodID>(((PXSelectBase) this.Document_Detail).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARRegister.curyOrigDocAmt>(((PXSelectBase) this.Document_Detail).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARRegister.curyID>(((PXSelectBase) this.Document_Detail).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARRegister.docDesc>(((PXSelectBase) this.Document_Detail).Cache, (object) null, false);
  }

  protected virtual void DocumentSelection_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (e.Row is ARRegister row1)
    {
      bool? nullable = row1.Voided;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = row1.Scheduled;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
          this.AddDocumentToSchedule(row1);
      }
    }
    if (!(e.Row is DocumentSelection row2) || string.IsNullOrWhiteSpace(row2.DocType) || string.IsNullOrWhiteSpace(row2.RefNbr) || PXSelectorAttribute.Select<DocumentSelection.refNbr>(cache, (object) row2) != null)
      return;
    cache.RaiseExceptionHandling<DocumentSelection.refNbr>((object) row2, (object) row2.RefNbr, (Exception) new PXSetPropertyException("Reference Number is not valid"));
    ((PXSelectBase) this.Document_Detail).Cache.Remove((object) row2);
  }

  protected virtual void DocumentSelection_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    DocumentSelection row1 = (DocumentSelection) e.Row;
    if (row1 == null || string.IsNullOrWhiteSpace(row1.DocType) || string.IsNullOrWhiteSpace(row1.RefNbr))
      return;
    DocumentSelection documentSelection = PXResultset<DocumentSelection>.op_Implicit(PXSelectBase<DocumentSelection, PXSelectReadonly<DocumentSelection, Where<DocumentSelection.docType, Equal<Required<DocumentSelection.docType>>, And<DocumentSelection.refNbr, Equal<Required<DocumentSelection.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row1.DocType,
      (object) row1.RefNbr
    }));
    if (documentSelection != null)
    {
      ((PXSelectBase<DocumentSelection>) this.Document_Detail).Delete(documentSelection);
      ((PXSelectBase<DocumentSelection>) this.Document_Detail).Update(documentSelection);
    }
    else
    {
      DocumentSelection row2 = (DocumentSelection) e.Row;
      ((PXSelectBase<DocumentSelection>) this.Document_Detail).Delete(row2);
      cache.RaiseExceptionHandling<DocumentSelection.refNbr>((object) row2, (object) row2.RefNbr, (Exception) new PXSetPropertyException("Reference Number is not valid"));
    }
  }

  protected virtual void DocumentSelection_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
    if (e.Operation == 3)
      return;
    DocumentSelection document = e.Row as DocumentSelection;
    if (document == null || GraphHelper.RowCast<ARInvoice>(((PXSelectBase) this._dummyInvoice).Cache.Updated).FirstOrDefault<ARInvoice>((Func<ARInvoice, bool>) (p => p.DocType == document.DocType && p.RefNbr == document.RefNbr)) != null)
      return;
    ARInvoice arInvoice = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) this._dummyInvoice).Select(new object[2]
    {
      (object) document.DocType,
      (object) document.RefNbr
    }));
    ((PXSelectBase) this._dummyInvoice).Cache.SetStatus((object) arInvoice, (PXEntryStatus) 1);
    ((PXSelectBase) this._dummyInvoice).Cache.PersistUpdated((object) arInvoice);
  }

  protected virtual void ARInvoice_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != null || e.Operation != 1)
      return;
    this.CleanScheduleDocument(e.Row as ARInvoice);
  }

  protected virtual void Schedule_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    foreach (PXResult<DocumentSelection> pxResult in PXSelectBase<DocumentSelection, PXSelect<DocumentSelection, Where<DocumentSelection.scheduleID, Equal<Required<PX.Objects.GL.Schedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PX.Objects.GL.Schedule) e.Row).ScheduleID
    }))
    {
      DocumentSelection documentSelection = PXResult<DocumentSelection>.op_Implicit(pxResult);
      documentSelection.ScheduleID = (string) null;
      ((PXSelectBase) this.Document_Detail).Cache.Delete((object) documentSelection);
    }
  }

  protected virtual void Schedule_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus == null)
    {
      foreach (DocumentSelection documentSelection in ((PXSelectBase) this.Document_Detail).Cache.Cached)
      {
        PXEntryStatus status = ((PXSelectBase) this.Document_Detail).Cache.GetStatus((object) documentSelection);
        if ((status == 2 || status == 1) && !documentSelection.Voided.GetValueOrDefault())
          ((SelectedEntityEvent<ARRegister, PX.Objects.GL.Schedule>) PXEntityEventBase<ARRegister>.Container<ARRegister.Events>.Select<PX.Objects.GL.Schedule>((Expression<Func<ARRegister.Events, PXEntityEvent<ARRegister.Events, PX.Objects.GL.Schedule>>>) (ev => ev.ConfirmSchedule))).FireOn((PXGraph) this, (ARRegister) documentSelection, ((PXSelectBase<PX.Objects.GL.Schedule>) this.Schedule_Header).Current);
        if (status == 3)
          ((SelectedEntityEvent<ARRegister, PX.Objects.GL.Schedule>) PXEntityEventBase<ARRegister>.Container<ARRegister.Events>.Select<PX.Objects.GL.Schedule>((Expression<Func<ARRegister.Events, PXEntityEvent<ARRegister.Events, PX.Objects.GL.Schedule>>>) (ev => ev.VoidSchedule))).FireOn((PXGraph) this, (ARRegister) documentSelection, ((PXSelectBase<PX.Objects.GL.Schedule>) this.Schedule_Header).Current);
      }
    }
    if (e.TranStatus != 1)
      return;
    ((PXSelectBase) this.Document_Detail).Cache.Clear();
    ((PXSelectBase) this.Document_Detail).View.Clear();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<DocumentSelection>) this.Document_Detail).Current == null)
      return adapter.Get();
    ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
    ((PXSelectBase<ARInvoice>) instance.Document).Current = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) instance.Document).Search<ARInvoice.refNbr>((object) ((PXSelectBase<DocumentSelection>) this.Document_Detail).Current.RefNbr, new object[1]
    {
      (object) ((PXSelectBase<DocumentSelection>) this.Document_Detail).Current.DocType
    }));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Document");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewGenDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<ARRegister>) this.Document_History).Current == null)
      return adapter.Get();
    ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
    ((PXSelectBase<ARInvoice>) instance.Document).Current = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) instance.Document).Search<ARInvoice.refNbr>((object) ((PXSelectBase<ARRegister>) this.Document_History).Current.RefNbr, new object[1]
    {
      (object) ((PXSelectBase<ARRegister>) this.Document_History).Current.DocType
    }));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Generated Document");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  /// <summary>
  /// Removes all application records and approval records associated with the specified
  /// document. This is required in order to prevent stuck application
  /// records after a document becomes scheduled.
  /// </summary>
  private void CleanScheduleDocument(ARInvoice document)
  {
    ARInvoiceEntry invoiceEntry = PXContext.GetSlot<ARInvoiceEntry>();
    if (invoiceEntry == null)
    {
      invoiceEntry = PXGraph.CreateInstance<ARInvoiceEntry>();
      PXContext.SetSlot<ARInvoiceEntry>(invoiceEntry);
    }
    ((PXGraph) invoiceEntry).Clear();
    ((PXGraph) invoiceEntry).SelectTimeStamp();
    ARInvoice arInvoice = PXResultset<ARInvoice>.op_Implicit(PXSelectBase<ARInvoice, PXSelect<ARInvoice, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) invoiceEntry, new object[2]
    {
      (object) document.DocType,
      (object) document.RefNbr
    }));
    ((PXSelectBase<ARInvoice>) invoiceEntry.Document).Current = arInvoice;
    PXCacheEx.Adjust<PXFormulaAttribute>(((PXSelectBase) invoiceEntry.Adjustments).Cache, (object) null).For<ARAdjust2.curyAdjdWOAmt>((Action<PXFormulaAttribute>) (a =>
    {
      if (!(a.Aggregate == typeof (SumCalc<ARInvoice.curyBalanceWOTotal>)))
        return;
      a.Aggregate = (Type) null;
    }));
    PXCacheEx.Adjust<PXFormulaAttribute>(((PXSelectBase) invoiceEntry.Adjustments).Cache, (object) null).For<ARAdjust2.curyAdjdAmt>((Action<PXFormulaAttribute>) (a =>
    {
      if (!EnumerableExtensions.IsIn<Type>(a.Aggregate, typeof (SumCalc<ARInvoice.curyPaymentTotal>), typeof (SumCalc<ARInvoice.curyUnreleasedPaymentAmt>), typeof (SumCalc<ARInvoice.curyCCAuthorizedAmt>), typeof (SumCalc<ARInvoice.curyPaidAmt>)))
        return;
      a.Aggregate = (Type) null;
    }));
    PXCacheEx.Adjust<PXFormulaAttribute>(((PXSelectBase) invoiceEntry.Adjustments).Cache, (object) null).For<ARAdjust2.adjdRefNbr>((Action<PXFormulaAttribute>) (a =>
    {
      if (!EnumerableExtensions.IsIn<Type>(a.Aggregate, typeof (SumCalc<ARInvoice.pendingProcessingCntr>), typeof (SumCalc<ARInvoice.captureFailedCntr>), typeof (SumCalc<ARInvoice.authorizedPaymentCntr>)))
        return;
      a.Aggregate = (Type) null;
    }));
    EnumerableExtensions.ForEach<EPApproval>(GraphHelper.RowCast<EPApproval>((IEnumerable) ((PXSelectBase<EPApproval>) invoiceEntry.Approval).Select(Array.Empty<object>())), (Action<EPApproval>) (a => ((PXSelectBase<EPApproval>) invoiceEntry.Approval).Delete(a)));
    EnumerableExtensions.ForEach<ARAdjust2>(GraphHelper.RowCast<ARAdjust2>((IEnumerable) ((PXSelectBase<ARAdjust2>) invoiceEntry.Adjustments).Select(Array.Empty<object>())).Where<ARAdjust2>((Func<ARAdjust2, bool>) (application => !application.Released.GetValueOrDefault())), (Action<ARAdjust2>) (application => ((PXSelectBase<ARAdjust2>) invoiceEntry.Adjustments).Delete(application)));
    EnumerableExtensions.ForEach<ARAdjust>(GraphHelper.RowCast<ARAdjust>((IEnumerable) ((PXSelectBase<ARAdjust>) invoiceEntry.Adjustments_1).Select(Array.Empty<object>())).Where<ARAdjust>((Func<ARAdjust, bool>) (application => !application.Released.GetValueOrDefault())), (Action<ARAdjust>) (application => ((PXSelectBase<ARAdjust>) invoiceEntry.Adjustments_1).Delete(application)));
    ARInvoice current = ((PXSelectBase<ARInvoice>) invoiceEntry.Document).Current;
    if ((current != null ? (current.IsTaxSaved.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      try
      {
        ((PXGraph) invoiceEntry).GetExtension<ARInvoiceEntryExternalTax>()?.VoidScheduledDocument(((PXSelectBase<ARInvoice>) invoiceEntry.Document).Current);
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex.Message);
      }
    }
    ((PXAction) invoiceEntry.Save).Press();
  }

  protected virtual void AddDocumentToSchedule(ARRegister documentAsRegister)
  {
    ARRegister ardoc = documentAsRegister;
    Decimal? origDocAmt = documentAsRegister.OrigDocAmt;
    Decimal? BalanceAmt = origDocAmt.HasValue ? new Decimal?(-origDocAmt.GetValueOrDefault()) : new Decimal?();
    ARReleaseProcess.UpdateARBalances((PXGraph) this, ardoc, BalanceAmt);
    documentAsRegister.Scheduled = new bool?(true);
    documentAsRegister.ScheduleID = ((PXSelectBase<PX.Objects.GL.Schedule>) this.Schedule_Header).Current.ScheduleID;
    ARReleaseProcess.UpdateARBalances((PXGraph) this, documentAsRegister, documentAsRegister.OrigDocAmt);
  }

  public Dictionary<string, ExpressionParameterInfo> GetAvailableExpressionParameters()
  {
    Dictionary<string, ExpressionParameterInfo> expressionParameters = new Dictionary<string, ExpressionParameterInfo>();
    ExpressionParameterInfo expressionParameterInfo = new ExpressionParameterInfo();
    ((ExpressionParameterInfo) ref expressionParameterInfo).Value = (object) ((PXSelectBase<PX.Objects.GL.Schedule>) this.Schedule_Header).Current?.ScheduleID;
    expressionParameters.Add("ScheduleID", expressionParameterInfo);
    return expressionParameters;
  }
}

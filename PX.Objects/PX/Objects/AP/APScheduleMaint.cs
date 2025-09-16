// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APScheduleMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.ProjectDefinition.Workflow;
using PX.Data.WorkflowAPI;
using PX.Objects.AP.Overrides.ScheduleMaint;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AP;

public class APScheduleMaint : 
  ScheduleMaintBase<APScheduleMaint, APScheduleProcess>,
  IWorkflowExpressionParserParametersProvider
{
  public PXSelect<Vendor> Vendors;
  public PXSelect<APRegister, Where<APRegister.scheduleID, Equal<Current<PX.Objects.GL.Schedule.scheduleID>>, And<APRegister.scheduled, Equal<False>>>> Document_History;
  public PXSelect<DocumentSelection, Where<DocumentSelection.scheduleID, Equal<Current<PX.Objects.GL.Schedule.scheduleID>>, And<DocumentSelection.scheduled, Equal<True>>>> Document_Detail;
  public PXSelect<APInvoice, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>> _dummyInvoice;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  public PXAction<PX.Objects.GL.Schedule> viewDocument;
  public PXAction<PX.Objects.GL.Schedule> viewGenDocument;

  public Dictionary<string, ExpressionParameterInfo> GetAvailableExpressionParameters()
  {
    return new Dictionary<string, ExpressionParameterInfo>()
    {
      {
        "ScheduleID",
        new ExpressionParameterInfo()
        {
          Value = (object) this.Schedule_Header.Current?.ScheduleID
        }
      }
    };
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Schedule ID", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 0)]
  [AutoNumber(typeof (PX.Objects.GL.GLSetup.scheduleNumberingID), typeof (AccessInfo.businessDate))]
  [PXSelector(typeof (Search2<PX.Objects.GL.Schedule.scheduleID, LeftJoin<APRegisterAccess, On<APRegisterAccess.scheduleID, Equal<PX.Objects.GL.Schedule.scheduleID>, And<APRegisterAccess.scheduled, Equal<True>, And<Not<Match<APRegisterAccess, Current<AccessInfo.userName>>>>>>>, Where<PX.Objects.GL.Schedule.module, Equal<BatchModule.moduleAP>, And<APRegisterAccess.docType, IsNull>>>))]
  [PXDefault]
  protected virtual void Schedule_ScheduleID_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", false)]
  protected virtual void _(PX.Data.Events.CacheAttached<APInvoice.invoiceNbr> e)
  {
  }

  public APScheduleMaint()
  {
    PX.Objects.AP.APSetup current1 = this.APSetup.Current;
    PX.Objects.GL.GLSetup current2 = this.GLSetup.Current;
    this.Document_History.Cache.AllowDelete = false;
    this.Document_History.Cache.AllowInsert = false;
    this.Document_History.Cache.AllowUpdate = false;
    this.Schedule_Header.Join<LeftJoin<APRegisterAccess, On<APRegisterAccess.scheduleID, Equal<PX.Objects.GL.Schedule.scheduleID>, And<APRegisterAccess.scheduled, Equal<True>, And<Not<Match<APRegisterAccess, Current<AccessInfo.userName>>>>>>>>();
    this.Schedule_Header.WhereAnd<Where<PX.Objects.GL.Schedule.module, Equal<BatchModule.moduleAP>, And<APRegisterAccess.docType, IsNull>>>();
    PXNoteAttribute.ForceRetain<APRegister.noteID>(this.Document_Detail.Cache);
    this.EnsureCachePersistence<APInvoice>();
    this.EnsureCachePersistence<APPayment>();
  }

  protected override string Module => "AP";

  internal override bool AnyScheduleDetails() => this.Document_Detail.Any<DocumentSelection>();

  protected virtual void DocumentSelection_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is DocumentSelection row))
      return;
    PXCache cache1 = sender;
    DocumentSelection data1 = row;
    bool? scheduled = row.Scheduled;
    int num1 = !scheduled.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<DocumentSelection.docType>(cache1, (object) data1, num1 != 0);
    PXCache cache2 = sender;
    DocumentSelection data2 = row;
    scheduled = row.Scheduled;
    int num2 = !scheduled.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<DocumentSelection.refNbr>(cache2, (object) data2, num2 != 0);
  }

  protected override void SetControlsState(PXCache cache, PX.Objects.GL.Schedule schedule)
  {
    base.SetControlsState(cache, schedule);
    bool isEnabled = !schedule.LastRunDate.HasValue;
    PXUIFieldAttribute.SetEnabled<PX.Objects.GL.Schedule.nextRunDate>(cache, (object) schedule, !isEnabled);
    PXUIFieldAttribute.SetEnabled<PX.Objects.GL.Schedule.formScheduleType>(cache, (object) schedule, isEnabled);
    PXUIFieldAttribute.SetEnabled<PX.Objects.GL.Schedule.startDate>(cache, (object) schedule, isEnabled);
    PXUIFieldAttribute.SetEnabled<APRegister.vendorID>(this.Document_Detail.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<APRegister.status>(this.Document_Detail.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<APRegister.docDate>(this.Document_Detail.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<DocumentSelection.finPeriodID>(this.Document_Detail.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<APRegister.curyOrigDocAmt>(this.Document_Detail.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<APRegister.curyID>(this.Document_Detail.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<APRegister.docDesc>(this.Document_Detail.Cache, (object) null, false);
  }

  protected virtual void DocumentSelection_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (e.Row is APRegister row1)
    {
      bool? voided = row1.Voided;
      bool flag = false;
      if (voided.GetValueOrDefault() == flag & voided.HasValue)
      {
        row1.ScheduleID = this.Schedule_Header.Current.ScheduleID;
        row1.Scheduled = new bool?(true);
      }
    }
    if (!(e.Row is DocumentSelection row2) || string.IsNullOrWhiteSpace(row2.DocType) || string.IsNullOrWhiteSpace(row2.RefNbr) || PXSelectorAttribute.Select<DocumentSelection.refNbr>(cache, (object) row2) != null)
      return;
    cache.RaiseExceptionHandling<DocumentSelection.refNbr>((object) row2, (object) row2.RefNbr, (Exception) new PXSetPropertyException("Reference Number is not valid"));
    this.Document_Detail.Cache.Remove((object) row2);
  }

  protected virtual void DocumentSelection_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is DocumentSelection row1) || string.IsNullOrWhiteSpace(row1.DocType) || string.IsNullOrWhiteSpace(row1.RefNbr))
      return;
    DocumentSelection documentSelection = (DocumentSelection) PXSelectBase<DocumentSelection, PXSelectReadonly<DocumentSelection, Where<DocumentSelection.docType, Equal<Required<DocumentSelection.docType>>, And<DocumentSelection.refNbr, Equal<Required<DocumentSelection.refNbr>>>>>.Config>.Select((PXGraph) this, (object) row1.DocType, (object) row1.RefNbr);
    if (documentSelection != null)
    {
      this.Document_Detail.Delete(documentSelection);
      this.Document_Detail.Update(documentSelection);
    }
    else
    {
      DocumentSelection row2 = (DocumentSelection) e.Row;
      this.Document_Detail.Delete(row2);
      cache.RaiseExceptionHandling<DocumentSelection.refNbr>((object) row2, (object) row2.RefNbr, (Exception) new PXSetPropertyException("Reference Number is not valid"));
    }
  }

  protected virtual void DocumentSelection_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    e.Cancel = true;
    if (e.Operation == PXDBOperation.Delete)
      return;
    DocumentSelection document = e.Row as DocumentSelection;
    if (document == null || this._dummyInvoice.Cache.Updated.RowCast<APInvoice>().FirstOrDefault<APInvoice>((Func<APInvoice, bool>) (p => p.DocType == document.DocType && p.RefNbr == document.RefNbr)) != null)
      return;
    APInvoice row = (APInvoice) this._dummyInvoice.Select((object) document.DocType, (object) document.RefNbr);
    this._dummyInvoice.Cache.SetStatus((object) row, PXEntryStatus.Updated);
    this._dummyInvoice.Cache.PersistUpdated((object) row);
  }

  protected virtual void Schedule_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    foreach (PXResult<DocumentSelection> pxResult in PXSelectBase<DocumentSelection, PXSelect<DocumentSelection, Where<DocumentSelection.scheduleID, Equal<Required<PX.Objects.GL.Schedule.scheduleID>>>>.Config>.Select((PXGraph) this, (object) ((PX.Objects.GL.Schedule) e.Row).ScheduleID))
      this.Document_Detail.Cache.Delete((object) (DocumentSelection) pxResult);
  }

  protected virtual void Schedule_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus == PXTranStatus.Open)
    {
      foreach (DocumentSelection eventTarget in this.Document_Detail.Cache.Cached)
      {
        PXEntryStatus status = this.Document_Detail.Cache.GetStatus((object) eventTarget);
        if ((status == PXEntryStatus.Inserted || status == PXEntryStatus.Updated) && !eventTarget.Voided.GetValueOrDefault())
          PXEntityEventBase<APRegister>.Container<APRegister.Events>.Select<PX.Objects.GL.Schedule>((Expression<Func<APRegister.Events, PXEntityEvent<APRegister, PX.Objects.GL.Schedule>>>) (ev => ev.ConfirmSchedule)).FireOn((PXGraph) this, (APRegister) eventTarget, this.Schedule_Header.Current);
        if (status == PXEntryStatus.Deleted)
          PXEntityEventBase<APRegister>.Container<APRegister.Events>.Select<PX.Objects.GL.Schedule>((Expression<Func<APRegister.Events, PXEntityEvent<APRegister, PX.Objects.GL.Schedule>>>) (ev => ev.VoidSchedule)).FireOn((PXGraph) this, (APRegister) eventTarget, this.Schedule_Header.Current);
      }
    }
    if (e.TranStatus != PXTranStatus.Completed)
      return;
    this.Document_Detail.Cache.Clear();
    this.Document_Detail.View.Clear();
  }

  protected virtual void APInvoice_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != PXTranStatus.Open || e.Operation != PXDBOperation.Update)
      return;
    this.CleanScheduleDocument(e.Row as APInvoice);
  }

  [PXUIField(DisplayName = "View Document", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (this.Document_Detail.Current == null)
      return adapter.Get();
    APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
    instance.Document.Current = (APInvoice) instance.Document.Search<APInvoice.refNbr>((object) this.Document_Detail.Current.RefNbr, (object) this.Document_Detail.Current.DocType);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Document");
    requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
    throw requiredException;
  }

  [PXUIField(DisplayName = "View Document", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public virtual IEnumerable ViewGenDocument(PXAdapter adapter)
  {
    if (this.Document_History.Current == null)
      return adapter.Get();
    APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
    instance.Document.Current = (APInvoice) instance.Document.Search<APInvoice.refNbr>((object) this.Document_History.Current.RefNbr, (object) this.Document_History.Current.DocType);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Generated Document");
    requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
    throw requiredException;
  }

  /// <summary>
  /// Removes all application and approval records associated with the specified
  /// document. This is required in order to prevent stuck application
  /// records after a document becomes scheduled.
  /// </summary>
  private void CleanScheduleDocument(APInvoice document)
  {
    APInvoiceEntry invoiceEntry = PXContext.GetSlot<APInvoiceEntry>();
    if (invoiceEntry == null)
    {
      invoiceEntry = PXGraph.CreateInstance<APInvoiceEntry>();
      PXContext.SetSlot<APInvoiceEntry>(invoiceEntry);
    }
    invoiceEntry.Clear();
    invoiceEntry.SelectTimeStamp();
    APInvoice apInvoice = (APInvoice) PXSelectBase<APInvoice, PXSelect<APInvoice, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>.Config>.Select((PXGraph) invoiceEntry, (object) document.DocType, (object) document.RefNbr);
    invoiceEntry.Document.Current = apInvoice;
    EnumerableExtensions.ForEach<EPApproval>(invoiceEntry.Approval.Select().RowCast<EPApproval>(), (System.Action<EPApproval>) (a => invoiceEntry.Approval.Delete(a)));
    EnumerableExtensions.ForEach<APInvoiceEntry.APAdjust>(invoiceEntry.Adjustments.Select().RowCast<APInvoiceEntry.APAdjust>().Where<APInvoiceEntry.APAdjust>((Func<APInvoiceEntry.APAdjust, bool>) (application => !application.Released.GetValueOrDefault())), (System.Action<APInvoiceEntry.APAdjust>) (application => invoiceEntry.Adjustments.Delete(application)));
    APInvoice current = invoiceEntry.Document.Current;
    if ((current != null ? (current.IsTaxSaved.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      try
      {
        invoiceEntry.GetExtension<APInvoiceEntryExternalTax>()?.VoidScheduledDocument(invoiceEntry.Document.Current);
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex.Message);
      }
    }
    invoiceEntry.Save.Press();
  }
}

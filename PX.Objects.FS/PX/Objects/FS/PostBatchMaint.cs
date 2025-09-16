// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.PostBatchMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.EP;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR.MassProcess;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO;

#nullable disable
namespace PX.Objects.FS;

public class PostBatchMaint : PXGraph<PostBatchMaint>
{
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> BAccount;
  [PXHidden]
  public PXSelect<PX.Objects.AR.Customer> Customer;
  [PXHidden]
  public PXSelect<FSAppointment> Appointment;
  [PXHidden]
  public PXSelect<FSServiceOrder> FSServiceOrderDummy;
  public PXSelect<FSPostBatch, Where<FSPostBatch.postTo, NotEqual<ListField_PostTo.IN>, And<Where<FSPostBatch.status, IsNull, Or<FSPostBatch.status, NotEqual<FSPostBatch.status.temporary>>>>>> BatchRecords;
  [PXHidden]
  public PXSelect<PostingBatchDetail> PostingBatchDetails;
  public PXSelectJoin<FSCreatedDoc, InnerJoin<PostingBatchDetail, On<PostingBatchDetail.postedTO, Equal<FSCreatedDoc.postTo>, And<PostingBatchDetail.postDocType, Equal<FSCreatedDoc.createdDocType>, And<PostingBatchDetail.postRefNbr, Equal<FSCreatedDoc.createdRefNbr>>>>>, Where<FSCreatedDoc.batchID, Equal<Current<FSPostBatch.batchID>>>, OrderBy<Asc<FSCreatedDoc.recordID, Asc<PostingBatchDetail.sOID, Asc<PostingBatchDetail.appointmentID>>>>> BatchDetailsInfo;
  public PXSave<FSPostBatch> Save;
  public PXCancel<FSPostBatch> Cancel;
  public PXFirst<FSPostBatch> First;
  public PXPrevious<FSPostBatch> Previous;
  public PXNext<FSPostBatch> Next;
  public PXLast<FSPostBatch> Last;
  public PXAction<FSPostBatch> OpenDocument;

  public PostBatchMaint()
  {
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.BatchRecords).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<FSPostBatch.batchNbr>(((PXSelectBase) this.BatchRecords).Cache, (object) null, true);
    ((PXSelectBase) this.BatchRecords).Cache.AllowInsert = false;
    ((PXAction) this.Save).SetVisible(false);
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<FSPostBatch.batchNbr, Where<FSPostBatch.postTo, NotEqual<ListField_PostTo.IN>, And<FSPostBatch.status, NotEqual<FSPostBatch.status.temporary>>>>))]
  [AutoNumber(typeof (Search<FSSetup.postBatchNumberingID>), typeof (AccessInfo.businessDate))]
  protected virtual void FSPostBatch_BatchNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(BqlField = typeof (FSAppointment.appointmentID))]
  [PXUIField(DisplayName = "Appointment Nbr.")]
  [PXSelector(typeof (Search<FSAppointment.appointmentID>), SubstituteKey = typeof (FSAppointment.refNbr))]
  protected virtual void PostingBatchDetail_AppointmentID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(BqlField = typeof (FSServiceOrder.sOID))]
  [PXUIField(DisplayName = "Service Order Nbr.")]
  [PXSelector(typeof (Search<FSServiceOrder.sOID>), SubstituteKey = typeof (FSServiceOrder.refNbr))]
  protected virtual void PostingBatchDetail_SOID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(60, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Customer.acctName))]
  [PXDefault]
  [PXFieldDescription]
  [PXMassMergableField]
  [PXUIField]
  protected virtual void PostingBatchDetail_AcctName_CacheAttached(PXCache sender)
  {
  }

  [PXButton]
  [PXUIField]
  public virtual void openDocument()
  {
    FSCreatedDoc current = ((PXSelectBase<FSCreatedDoc>) this.BatchDetailsInfo).Current;
    if (current.PostTo == "SO")
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
      {
        SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
        ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) current.CreatedRefNbr, new object[1]
        {
          (object) current.CreatedDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    else
    {
      if (current.PostTo == "AR")
      {
        ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) current.CreatedRefNbr, new object[1]
        {
          (object) current.CreatedDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (current.PostTo == "SI")
      {
        SOInvoiceEntry instance = PXGraph.CreateInstance<SOInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) current.CreatedRefNbr, new object[1]
        {
          (object) current.CreatedDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (current.PostTo == "AP")
      {
        APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Current = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Search<PX.Objects.AP.APInvoice.refNbr>((object) current.CreatedRefNbr, new object[1]
        {
          (object) current.CreatedDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (current.PostTo == "IN")
      {
        INIssueEntry instance = PXGraph.CreateInstance<INIssueEntry>();
        ((PXSelectBase<PX.Objects.IN.INRegister>) instance.issue).Current = PXResultset<PX.Objects.IN.INRegister>.op_Implicit(((PXSelectBase<PX.Objects.IN.INRegister>) instance.issue).Search<PX.Objects.IN.INRegister.refNbr>((object) current.CreatedRefNbr, new object[1]
        {
          (object) current.CreatedDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (current.PostTo == "PM")
      {
        RegisterEntry instance = PXGraph.CreateInstance<RegisterEntry>();
        ((PXSelectBase<PMRegister>) instance.Document).Current = PXResultset<PMRegister>.op_Implicit(((PXSelectBase<PMRegister>) instance.Document).Search<PMRegister.refNbr>((object) current.CreatedRefNbr, new object[1]
        {
          (object) current.CreatedDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSPostBatch> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSPostBatch> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSPostBatch> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSPostBatch> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSPostBatch> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSPostBatch> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSPostBatch> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSPostBatch> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSPostBatch> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSPostBatch> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PostingBatchDetail> e)
  {
    if (e.Row == null)
      return;
    PostingBatchDetail row = e.Row;
    bool? nullable = row.SOPosted;
    if (nullable.GetValueOrDefault())
    {
      using (new PXConnectionScope())
      {
        PX.Objects.SO.SOOrderShipment soOrderShipment = PXResultset<PX.Objects.SO.SOOrderShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrderShipment, PXSelectReadonly<PX.Objects.SO.SOOrderShipment, Where<PX.Objects.SO.SOOrderShipment.orderNbr, Equal<Required<PX.Objects.SO.SOOrderShipment.orderNbr>>, And<PX.Objects.SO.SOOrderShipment.orderType, Equal<Required<PX.Objects.SO.SOOrderShipment.orderType>>>>>.Config>.Select(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<PostingBatchDetail>>) e).Cache.Graph, new object[2]
        {
          (object) row.SOOrderNbr,
          (object) row.SOOrderType
        }));
        row.InvoiceRefNbr = soOrderShipment?.InvoiceNbr;
      }
    }
    else
    {
      nullable = row.ARPosted;
      if (!nullable.GetValueOrDefault())
      {
        nullable = row.SOInvPosted;
        if (!nullable.GetValueOrDefault())
          return;
      }
      row.InvoiceRefNbr = row.Mem_DocNbr;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<PostingBatchDetail> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<PostingBatchDetail> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<PostingBatchDetail> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<PostingBatchDetail> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PostingBatchDetail> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PostingBatchDetail> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PostingBatchDetail> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PostingBatchDetail> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PostingBatchDetail> e)
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.InventoryPostBatchMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.EP;
using PX.Objects.CR.MassProcess;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.FS;

public class InventoryPostBatchMaint : PXGraph<InventoryPostBatchMaint>
{
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> BAccount;
  [PXHidden]
  public PXSelect<PX.Objects.AR.Customer> Customer;
  [PXHidden]
  public PXSelect<FSServiceOrder> FSServiceOrderDummy;
  public PXSelect<FSPostBatch, Where<FSPostBatch.postTo, Equal<ListField_PostTo.IN>>> BatchRecords;
  [PXHidden]
  public PXSelect<FSPostDet> BatchDetails;
  public PXSelectReadonly<InventoryPostingBatchDetail, Where<PostingBatchDetail.batchID, Equal<Current<FSPostBatch.batchID>>>> BatchDetailsInfo;
  public PXSave<FSPostBatch> Save;
  public PXCancel<FSPostBatch> Cancel;
  public PXFirst<FSPostBatch> First;
  public PXPrevious<FSPostBatch> Previous;
  public PXNext<FSPostBatch> Next;
  public PXLast<FSPostBatch> Last;
  public PXAction<FSPostBatch> OpenDocument;

  public InventoryPostBatchMaint()
  {
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.BatchRecords).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<FSPostBatch.batchNbr>(((PXSelectBase) this.BatchRecords).Cache, (object) null, true);
    ((PXSelectBase) this.BatchRecords).Cache.AllowInsert = false;
    ((PXAction) this.Save).SetVisible(false);
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<FSPostBatch.batchNbr, Where<FSPostBatch.postTo, Equal<ListField_PostTo.IN>, And<FSPostBatch.status, NotEqual<FSPostBatch.status.temporary>>>>), new System.Type[] {typeof (FSPostBatch.batchNbr), typeof (FSPostBatch.finPeriodID), typeof (FSPostBatch.cutOffDate)})]
  [AutoNumber(typeof (Search<FSSetup.postBatchNumberingID>), typeof (AccessInfo.businessDate))]
  protected virtual void FSPostBatch_BatchNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Lines Processed")]
  protected virtual void FSPostBatch_QtyDoc_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Up to Date")]
  protected virtual void FSPostBatch_CutOffDate_CacheAttached(PXCache sender)
  {
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (FSPostBatch.finPeriodID))]
  [PXUIField]
  protected virtual void FSPostBatch_FinPeriodID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  protected virtual void FSPostBatch_InvoiceDate_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(BqlField = typeof (FSAppointment.appointmentID))]
  [PXUIField(DisplayName = "Appointment Nbr.")]
  [PXSelector(typeof (Search<FSAppointment.appointmentID>), SubstituteKey = typeof (FSAppointment.refNbr))]
  protected virtual void InventoryPostingBatchDetail_AppointmentID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(BqlField = typeof (FSAppointment.sOID))]
  [PXUIField(DisplayName = "Service Order Nbr.")]
  [PXSelector(typeof (Search<FSServiceOrder.sOID>), SubstituteKey = typeof (FSServiceOrder.refNbr))]
  protected virtual void InventoryPostingBatchDetail_SOID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(60, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Customer.acctName))]
  [PXDefault]
  [PXFieldDescription]
  [PXMassMergableField]
  [PXUIField]
  protected virtual void InventoryPostingBatchDetail_AcctName_CacheAttached(PXCache sender)
  {
  }

  [PXButton]
  [PXUIField]
  public virtual void openDocument()
  {
    FSPostDet fsPostDet = PXResultset<FSPostDet>.op_Implicit(PXSelectBase<FSPostDet, PXSelectJoin<FSPostDet, InnerJoin<FSPostInfo, On<FSPostInfo.postID, Equal<FSPostDet.postID>>, InnerJoin<FSAppointmentDet, On<FSAppointmentDet.postID, Equal<FSPostInfo.postID>>>>, Where<FSPostDet.batchID, Equal<Current<FSPostBatch.batchID>>, And<FSAppointmentDet.appDetID, Equal<Required<FSAppointmentDet.appDetID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<InventoryPostingBatchDetail>) this.BatchDetailsInfo).Current.AppointmentInventoryItemID
    }));
    if (fsPostDet == null || !fsPostDet.INPosted.GetValueOrDefault())
      return;
    if (fsPostDet.INDocType.Trim() == "R")
    {
      INReceiptEntry instance = PXGraph.CreateInstance<INReceiptEntry>();
      ((PXSelectBase<INRegister>) instance.receipt).Current = PXResultset<INRegister>.op_Implicit(((PXSelectBase<INRegister>) instance.receipt).Search<INRegister.refNbr>((object) fsPostDet.INRefNbr, Array.Empty<object>()));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    INIssueEntry instance1 = PXGraph.CreateInstance<INIssueEntry>();
    ((PXSelectBase<INRegister>) instance1.issue).Current = PXResultset<INRegister>.op_Implicit(((PXSelectBase<INRegister>) instance1.issue).Search<INRegister.refNbr>((object) fsPostDet.INRefNbr, Array.Empty<object>()));
    PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, (string) null);
    ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException1;
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSPostBatch> e)
  {
  }
}

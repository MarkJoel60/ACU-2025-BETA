// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.PostDocBatchMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.SO;

#nullable disable
namespace PX.Objects.FS;

public class PostDocBatchMaint : PXGraph<PostDocBatchMaint, FSPostBatch>
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
  public PXSelectJoin<FSPostDoc, LeftJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSPostDoc.sOID>>, LeftJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSPostDoc.appointmentID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.billCustomerID>>, InnerJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>, LeftJoin<FSGeoZonePostalCode, On<FSGeoZonePostalCode.postalCode, Equal<FSAddress.postalCode>>, LeftJoin<FSGeoZone, On<FSGeoZone.geoZoneID, Equal<FSGeoZonePostalCode.geoZoneID>>>>>>>>, Where<FSPostDoc.batchID, Equal<Current<FSPostBatch.batchID>>>> BatchDetailsInfo;
  public PXAction<FSPostBatch> OpenDocument;

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<FSPostBatch.batchNbr, Where<FSPostBatch.postTo, NotEqual<ListField_PostTo.IN>, And<FSPostBatch.status, NotEqual<FSPostBatch.status.temporary>>>>))]
  [AutoNumber(typeof (Search<FSSetup.postBatchNumberingID>), typeof (AccessInfo.businessDate))]
  protected virtual void FSPostBatch_BatchNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Start Time", BqlField = typeof (FSAppointment.actualDateTimeBegin))]
  [PXUIField]
  protected virtual void FSAppointment_ActualDateTimeBegin_CacheAttached(PXCache sender)
  {
  }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "End Time", BqlField = typeof (FSAppointment.actualDateTimeEnd))]
  [PXUIField]
  protected virtual void FSAppointment_ActualDateTimeEnd_CacheAttached(PXCache sender)
  {
  }

  [PXButton]
  [PXUIField]
  public virtual void openDocument()
  {
    FSPostDoc current = ((PXSelectBase<FSPostDoc>) this.BatchDetailsInfo).Current;
    if (current.PostedTO == "SO")
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
      {
        SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
        ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) current.PostRefNbr, new object[1]
        {
          (object) current.PostDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    else
    {
      if (current.PostedTO == "AR")
      {
        ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) current.PostRefNbr, new object[1]
        {
          (object) current.PostDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (current.PostedTO == "AP")
      {
        APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Current = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Search<PX.Objects.AP.APInvoice.refNbr>((object) current.PostRefNbr, new object[1]
        {
          (object) current.PostDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (current.PostedTO == "SI")
      {
        SOInvoiceEntry instance = PXGraph.CreateInstance<SOInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) current.PostRefNbr, new object[1]
        {
          (object) current.PostDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSPostBatch> e)
  {
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.BatchRecords).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<FSPostBatch.batchNbr>(((PXSelectBase) this.BatchRecords).Cache, (object) e.Row, true);
    ((PXSelectBase) this.BatchRecords).Cache.AllowInsert = false;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ContractPostBatchMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.SO;

#nullable disable
namespace PX.Objects.FS;

public class ContractPostBatchMaint : PXGraph<ContractPostBatchMaint>
{
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> BAccount;
  [PXHidden]
  public PXSelect<PX.Objects.AR.Customer> Customer;
  public PXSelect<FSContractPostBatch> ContractBatchRecords;
  public PXSelectReadonly<ContractPostBatchDetail, Where<ContractPostBatchDetail.contractPostBatchID, Equal<Current<FSContractPostBatch.contractPostBatchID>>>> ContractPostDocRecords;
  public PXSave<FSContractPostBatch> Save;
  public PXCancel<FSContractPostBatch> Cancel;
  public PXFirst<FSContractPostBatch> First;
  public PXPrevious<FSContractPostBatch> Previous;
  public PXNext<FSContractPostBatch> Next;
  public PXLast<FSContractPostBatch> Last;
  public PXAction<FSContractPostBatch> openDocument;
  public PXAction<FSContractPostBatch> openContract;

  public ContractPostBatchMaint()
  {
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.ContractBatchRecords).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<FSContractPostBatch.contractPostBatchNbr>(((PXSelectBase) this.ContractBatchRecords).Cache, (object) null, true);
    ((PXSelectBase) this.ContractBatchRecords).Cache.AllowInsert = false;
    ((PXAction) this.Save).SetVisible(false);
  }

  [PXButton]
  [PXUIField]
  public virtual void OpenDocument()
  {
    ContractPostBatchDetail current = ((PXSelectBase<ContractPostBatchDetail>) this.ContractPostDocRecords).Current;
    if (current == null)
      return;
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
    else if (current.PostedTO == "AR")
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
  }

  [PXButton]
  [PXUIField]
  public virtual void OpenContract()
  {
    ContractPostBatchDetail current = ((PXSelectBase<ContractPostBatchDetail>) this.ContractPostDocRecords).Current;
    if (current == null)
      return;
    FSServiceContract fsServiceContract = FSServiceContract.PK.Find((PXGraph) this, current.ServiceContractID);
    if (fsServiceContract == null)
      return;
    if (fsServiceContract.RecordType == "NRSC")
    {
      ServiceContractEntry instance = PXGraph.CreateInstance<ServiceContractEntry>();
      ((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) fsServiceContract.ServiceContractID, new object[1]
      {
        (object) fsServiceContract.CustomerID
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    RouteServiceContractEntry instance1 = PXGraph.CreateInstance<RouteServiceContractEntry>();
    ((PXSelectBase<FSServiceContract>) instance1.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) instance1.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) fsServiceContract.ServiceContractID, new object[1]
    {
      (object) fsServiceContract.CustomerID
    }));
    PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, (string) null);
    ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException1;
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSContractPostBatch> e)
  {
  }
}

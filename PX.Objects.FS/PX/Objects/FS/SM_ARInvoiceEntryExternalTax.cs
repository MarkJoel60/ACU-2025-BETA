// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_ARInvoiceEntryExternalTax
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.FS.DAC;
using PX.Objects.IN;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class SM_ARInvoiceEntryExternalTax : 
  PXGraphExtension<ARInvoiceEntryExternalTax, ARInvoiceEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXOverride]
  public virtual IAddressLocation GetFromAddress(
    PX.Objects.AR.ARInvoice invoice,
    PX.Objects.AR.ARTran tran,
    SM_ARInvoiceEntryExternalTax.GetFromAddressLineDelegate del)
  {
    string srvOrderType;
    string refNbr;
    this.GetServiceOrderKeys(tran, out srvOrderType, out refNbr);
    IAddressLocation iaddressLocation = (IAddressLocation) null;
    if (!string.IsNullOrEmpty(refNbr) && !tran.SiteID.HasValue)
      iaddressLocation = (IAddressLocation) GraphHelper.RowCast<FSAddress>((IEnumerable) PXSelectBase<FSAddress, PXSelectJoin<FSAddress, InnerJoin<FSBranchLocation, On<FSBranchLocation.branchLocationAddressID, Equal<FSAddress.addressID>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.branchLocationID, Equal<FSBranchLocation.branchLocationID>>>>, Where<FSServiceOrder.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Required<FSServiceOrder.refNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, new object[2]
      {
        (object) srvOrderType,
        (object) refNbr
      })).FirstOrDefault<FSAddress>();
    else if (!string.IsNullOrEmpty(refNbr) && tran.SiteID.HasValue)
      iaddressLocation = (IAddressLocation) GraphHelper.RowCast<PX.Objects.CR.Address>((IEnumerable) PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, InnerJoin<INSite, On<PX.Objects.CR.Address.addressID, Equal<INSite.addressID>>>, Where<INSite.siteID, Equal<Required<INSite.siteID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, new object[1]
      {
        (object) tran.SiteID
      })).FirstOrDefault<PX.Objects.CR.Address>();
    return iaddressLocation ?? del(invoice, tran);
  }

  [PXOverride]
  public virtual IAddressLocation GetToAddress(
    PX.Objects.AR.ARInvoice invoice,
    PX.Objects.AR.ARTran tran,
    SM_ARInvoiceEntryExternalTax.GetToAddressLineDelegate del)
  {
    string srvOrderType;
    string refNbr;
    this.GetServiceOrderKeys(tran, out srvOrderType, out refNbr);
    if (string.IsNullOrEmpty(refNbr))
      return del(invoice, tran);
    return (IAddressLocation) GraphHelper.RowCast<FSAddress>((IEnumerable) PXSelectBase<FSAddress, PXSelectJoin<FSAddress, InnerJoin<FSServiceOrder, On<FSServiceOrder.serviceOrderAddressID, Equal<FSAddress.addressID>>>, Where<FSServiceOrder.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Required<FSServiceOrder.refNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, new object[2]
    {
      (object) srvOrderType,
      (object) refNbr
    })).FirstOrDefault<FSAddress>();
  }

  protected void GetServiceOrderKeys(PX.Objects.AR.ARTran tran, out string srvOrderType, out string refNbr)
  {
    FSxARTran extension = ((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base).Caches[typeof (PX.Objects.AR.ARTran)].GetExtension<FSxARTran>((object) tran);
    srvOrderType = extension?.SrvOrdType;
    refNbr = extension?.ServiceOrderRefNbr;
  }

  public delegate IAddressLocation GetFromAddressLineDelegate(PX.Objects.AR.ARInvoice invoice, PX.Objects.AR.ARTran tran);

  public delegate IAddressLocation GetToAddressLineDelegate(PX.Objects.AR.ARInvoice invoice, PX.Objects.AR.ARTran tran);
}

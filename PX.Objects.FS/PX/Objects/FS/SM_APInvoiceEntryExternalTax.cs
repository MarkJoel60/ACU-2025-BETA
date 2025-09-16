// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_APInvoiceEntryExternalTax
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class SM_APInvoiceEntryExternalTax : 
  PXGraphExtension<APInvoiceEntryExternalTax, APInvoiceEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXOverride]
  public virtual IAddressLocation GetFromAddress(
    PX.Objects.AP.APInvoice invoice,
    APTran tran,
    SM_APInvoiceEntryExternalTax.GetFromAddressLineDelegate del)
  {
    string srvOrderType;
    string refNbr;
    this.GetServiceOrderKeys(tran, out srvOrderType, out refNbr);
    if (string.IsNullOrEmpty(refNbr))
      return del(invoice, tran);
    return (IAddressLocation) GraphHelper.RowCast<FSAddress>((IEnumerable) PXSelectBase<FSAddress, PXSelectJoin<FSAddress, InnerJoin<FSServiceOrder, On<FSServiceOrder.serviceOrderAddressID, Equal<FSAddress.addressID>>>, Where<FSServiceOrder.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Required<FSServiceOrder.refNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, new object[2]
    {
      (object) srvOrderType,
      (object) refNbr
    })).FirstOrDefault<FSAddress>();
  }

  [PXOverride]
  public virtual IAddressLocation GetToAddress(
    PX.Objects.AP.APInvoice invoice,
    APTran tran,
    SM_APInvoiceEntryExternalTax.GetToAddressLineDelegate del)
  {
    string srvOrderType;
    string refNbr;
    this.GetServiceOrderKeys(tran, out srvOrderType, out refNbr);
    if (string.IsNullOrEmpty(refNbr))
      return del(invoice, tran);
    return (IAddressLocation) GraphHelper.RowCast<FSAddress>((IEnumerable) PXSelectBase<FSAddress, PXSelectJoin<FSAddress, InnerJoin<FSBranchLocation, On<FSBranchLocation.branchLocationAddressID, Equal<FSAddress.addressID>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.branchLocationID, Equal<FSBranchLocation.branchLocationID>>>>, Where<FSServiceOrder.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Required<FSServiceOrder.refNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, new object[2]
    {
      (object) srvOrderType,
      (object) refNbr
    })).FirstOrDefault<FSAddress>();
  }

  protected void GetServiceOrderKeys(APTran line, out string srvOrderType, out string refNbr)
  {
    FSxAPTran extension = PXCache<APTran>.GetExtension<FSxAPTran>(line);
    srvOrderType = extension?.SrvOrdType;
    refNbr = extension?.ServiceOrderRefNbr;
  }

  public delegate IAddressLocation GetFromAddressLineDelegate(PX.Objects.AP.APInvoice invoice, APTran tran);

  public delegate IAddressLocation GetToAddressLineDelegate(PX.Objects.AP.APInvoice invoice, APTran tran);
}

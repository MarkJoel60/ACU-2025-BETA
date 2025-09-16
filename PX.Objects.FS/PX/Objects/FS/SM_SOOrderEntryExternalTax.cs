// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_SOOrderEntryExternalTax
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.SO;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class SM_SOOrderEntryExternalTax : PXGraphExtension<SOOrderEntryExternalTax, SOOrderEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXOverride]
  public virtual IAddressLocation GetFromAddress(
    PX.Objects.SO.SOOrder order,
    SOLine line,
    SM_SOOrderEntryExternalTax.GetFromAddressLineDelegate del)
  {
    string srvOrderType;
    string refNbr;
    this.GetServiceOrderKeys(line, out srvOrderType, out refNbr);
    if (string.IsNullOrEmpty(refNbr) || line.SiteID.HasValue)
      return del(order, line);
    return (IAddressLocation) GraphHelper.RowCast<FSAddress>((IEnumerable) PXSelectBase<FSAddress, PXSelectJoin<FSAddress, InnerJoin<FSBranchLocation, On<FSBranchLocation.branchLocationAddressID, Equal<FSAddress.addressID>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.branchLocationID, Equal<FSBranchLocation.branchLocationID>>>>, Where<FSServiceOrder.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Required<FSServiceOrder.refNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[2]
    {
      (object) srvOrderType,
      (object) refNbr
    })).FirstOrDefault<FSAddress>();
  }

  [PXOverride]
  public virtual IAddressLocation GetToAddress(
    PX.Objects.SO.SOOrder order,
    SOLine line,
    SM_SOOrderEntryExternalTax.GetToAddressLineDelegate del)
  {
    string srvOrderType;
    string refNbr;
    this.GetServiceOrderKeys(line, out srvOrderType, out refNbr);
    if (string.IsNullOrEmpty(refNbr))
      return del(order, line);
    return (IAddressLocation) GraphHelper.RowCast<FSAddress>((IEnumerable) PXSelectBase<FSAddress, PXSelectJoin<FSAddress, InnerJoin<FSServiceOrder, On<FSServiceOrder.serviceOrderAddressID, Equal<FSAddress.addressID>>>, Where<FSServiceOrder.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Required<FSServiceOrder.refNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[2]
    {
      (object) srvOrderType,
      (object) refNbr
    })).FirstOrDefault<FSAddress>();
  }

  protected void GetServiceOrderKeys(SOLine line, out string srvOrderType, out string refNbr)
  {
    FSxSOLine extension = PXCache<SOLine>.GetExtension<FSxSOLine>(line);
    srvOrderType = extension?.SrvOrdType;
    refNbr = extension?.ServiceOrderRefNbr;
  }

  public delegate IAddressLocation GetFromAddressLineDelegate(PX.Objects.SO.SOOrder order, SOLine line);

  public delegate IAddressLocation GetToAddressLineDelegate(PX.Objects.SO.SOOrder order, SOLine line);
}

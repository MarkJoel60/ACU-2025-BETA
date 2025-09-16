// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CRExtensionHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.EP;

#nullable disable
namespace PX.Objects.FS;

public static class CRExtensionHelper
{
  public static void LaunchEmployeeBoard(PXGraph graph, string refNbr, string srvOrdType)
  {
    if (refNbr == null)
      return;
    ServiceOrderEntry instance = PXGraph.CreateInstance<ServiceOrderEntry>();
    ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) refNbr, new object[1]
    {
      (object) srvOrdType
    }));
    instance.OpenEmployeeBoard();
  }

  public static void LaunchServiceOrderScreen(PXGraph graph, string refNbr, string srvOrdType)
  {
    if (refNbr != null)
    {
      ServiceOrderEntry instance = PXGraph.CreateInstance<ServiceOrderEntry>();
      ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) refNbr, new object[1]
      {
        (object) srvOrdType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  public static FSSrvOrdType GetServiceOrderType(PXGraph graph, string srvOrdType)
  {
    if (string.IsNullOrEmpty(srvOrdType))
      return (FSSrvOrdType) null;
    return PXResultset<FSSrvOrdType>.op_Implicit(PXSelectBase<FSSrvOrdType, PXSelect<FSSrvOrdType, Where<FSSrvOrdType.srvOrdType, Equal<Required<FSSrvOrdType.srvOrdType>>>>.Config>.Select(graph, new object[1]
    {
      (object) srvOrdType
    }));
  }

  public static int? GetSalesPersonID(PXGraph graph, int? ownerID)
  {
    return PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.defContactID, Equal<Required<EPEmployee.defContactID>>>>.Config>.Select(graph, new object[1]
    {
      (object) ownerID
    }))?.SalesPersonID;
  }

  public static FSServiceOrder InitNewServiceOrder(string srvOrdType, string sourceType)
  {
    return new FSServiceOrder()
    {
      SrvOrdType = srvOrdType,
      SourceType = sourceType
    };
  }

  public static FSServiceOrder GetRelatedServiceOrder(
    PXGraph graph,
    PXCache chache,
    IBqlTable crTable,
    string refNbr,
    string srvOrdType)
  {
    FSServiceOrder relatedServiceOrder = (FSServiceOrder) null;
    if (refNbr != null && chache.GetStatus((object) crTable) != 2)
      relatedServiceOrder = PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXSelect<FSServiceOrder, Where<FSServiceOrder.refNbr, Equal<Required<FSServiceOrder.refNbr>>, And<FSServiceOrder.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>>>>.Config>.Select(graph, new object[2]
      {
        (object) refNbr,
        (object) srvOrdType
      }));
    return relatedServiceOrder;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.LocationHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AR;

#nullable disable
namespace PX.Objects.FS;

public class LocationHelper
{
  public static void OpenCustomerLocation(PXGraph graph, int? soID)
  {
    FSServiceOrder fsServiceOrder = PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXSelect<FSServiceOrder, Where<FSServiceOrder.sOID, Equal<Required<FSServiceOrder.sOID>>>>.Config>.Select(graph, new object[1]
    {
      (object) soID
    }));
    CustomerLocationMaint instance = PXGraph.CreateInstance<CustomerLocationMaint>();
    PX.Objects.CR.BAccount baccount = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) fsServiceOrder.CustomerID
    }));
    ((PXSelectBase<PX.Objects.CR.Location>) instance.Location).Current = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) instance.Location).Search<PX.Objects.CR.Location.locationID>((object) fsServiceOrder.LocationID, new object[1]
    {
      (object) baccount.AcctCD
    }));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }
}

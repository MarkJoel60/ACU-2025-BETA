// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.EPEmployeeMaintBridge
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.EP;

#nullable disable
namespace PX.Objects.FS;

public class EPEmployeeMaintBridge : PXGraph<EPEmployeeMaintBridge, EPEmployeeFSRouteEmployee>
{
  public PXSelect<EPEmployeeFSRouteEmployee> EPEmployeeFSRouteEmployeeRecords;

  protected virtual void _(PX.Data.Events.RowSelected<EPEmployeeFSRouteEmployee> e)
  {
    if (e.Row != null)
    {
      EPEmployeeFSRouteEmployee row = e.Row;
      EmployeeMaint instance = PXGraph.CreateInstance<EmployeeMaint>();
      if (row.BAccountID.HasValue)
        ((PXSelectBase<EPEmployee>) instance.CurrentEmployee).Current = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>.Config>.Select(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPEmployeeFSRouteEmployee>>) e).Cache.Graph, new object[1]
        {
          (object) row.BAccountID
        }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }
}

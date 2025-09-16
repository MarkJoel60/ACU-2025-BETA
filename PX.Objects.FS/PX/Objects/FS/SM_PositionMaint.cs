// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_PositionMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.EP;

#nullable disable
namespace PX.Objects.FS;

public class SM_PositionMaint : PXGraphExtension<PositionMaint>
{
  [PXHidden]
  public PXSelect<EPEmployee> Employee;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPPosition, FSxEPPosition.sDEnabled> e)
  {
    if (e.Row == null)
      return;
    EPPosition row = e.Row;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPPosition, FSxEPPosition.sDEnabled>>) e).Cache.GetExtension<FSxEPPosition>((object) row).SDEnabledModified = true;
  }

  protected virtual void _(PX.Data.Events.RowSelecting<EPPosition> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<EPPosition> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<EPPosition> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<EPPosition> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<EPPosition> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<EPPosition> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<EPPosition> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<EPPosition> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<EPPosition> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<EPPosition> e)
  {
    if (e.Row == null || e.TranStatus != null)
      return;
    EPPosition row = e.Row;
    FSxEPPosition extension1 = ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<EPPosition>>) e).Cache.GetExtension<FSxEPPosition>((object) row);
    if (!extension1.SDEnabledModified || ((PXSelectBase<EPPosition>) this.Base.EPPosition).Ask("Propagation Confirmation", "Changes will be saved. Do you want to propagate the changes to the associated Employees?", (MessageButtons) 4) != 6)
      return;
    ((PXSelectBase) this.Employee).Cache.Clear();
    foreach (PXResult<EPEmployee> pxResult in PXSelectBase<EPEmployee, PXSelectJoin<EPEmployee, InnerJoin<EPEmployeePosition, On<EPEmployeePosition.employeeID, Equal<EPEmployee.bAccountID>>>, Where<EPEmployeePosition.positionID, Equal<Required<EPEmployeePosition.positionID>>, And<EPEmployeePosition.isActive, Equal<True>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.PositionID
    }))
    {
      EPEmployee epEmployee = PXResult<EPEmployee>.op_Implicit(pxResult);
      FSxEPEmployee extension2 = ((PXSelectBase) this.Employee).Cache.GetExtension<FSxEPEmployee>((object) epEmployee);
      if (extension2 != null)
      {
        extension2.SDEnabled = extension1.SDEnabled;
        ((PXSelectBase) this.Employee).Cache.Update((object) epEmployee);
        ((PXSelectBase) this.Employee).Cache.SetStatus((object) epEmployee, (PXEntryStatus) 1);
      }
    }
    ((PXSelectBase) this.Employee).Cache.Persist((PXDBOperation) 3);
  }
}

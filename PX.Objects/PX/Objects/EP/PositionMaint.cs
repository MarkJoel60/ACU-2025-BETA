// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.PositionMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.EP;

public class PositionMaint : PXGraph<PositionMaint>
{
  public PXSelect<PX.Objects.EP.EPPosition> EPPosition;
  public PXSavePerRow<PX.Objects.EP.EPPosition> Save;
  public PXCancel<PX.Objects.EP.EPPosition> Cancel;

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Position")]
  protected virtual void _(Events.CacheAttached<PX.Objects.EP.EPPosition.positionID> e)
  {
  }

  protected virtual void EPPosition_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    string positionId = ((PX.Objects.EP.EPPosition) e.Row).PositionID;
    if ((PX.Objects.EP.EPPosition) PXSelectBase<PX.Objects.EP.EPPosition, PXSelect<PX.Objects.EP.EPPosition, Where<PX.Objects.EP.EPPosition.positionID, Equal<Required<PX.Objects.EP.EPPosition.positionID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) positionId) == null)
      return;
    cache.RaiseExceptionHandling<PX.Objects.EP.EPPosition.positionID>(e.Row, (object) positionId, (Exception) new PXException("Record already exists."));
    e.Cancel = true;
  }

  protected virtual void EPPosition_PositionID_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is PX.Objects.EP.EPPosition row) || e.NewValue == null || row.PositionID == null)
      return;
    EPEmployeePosition employeePosition = (EPEmployeePosition) PXSelectBase<EPEmployeePosition, PXSelect<EPEmployeePosition, Where<EPEmployeePosition.positionID, Equal<Required<PX.Objects.EP.EPPosition.positionID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) row.PositionID);
    if (employeePosition != null && employeePosition.PositionID != e.NewValue.ToString())
      throw new PXSetPropertyException("This Position is assigned to the Employee and cannot be changed.");
  }

  protected virtual void EPPosition_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is PX.Objects.EP.EPPosition row))
      return;
    EPEmployee epEmployee = (EPEmployee) PXSelectBase<EPEmployee, PXSelectJoin<EPEmployee, LeftJoin<EPEmployeePosition, On<EPEmployee.bAccountID, Equal<EPEmployeePosition.employeeID>>>, Where<EPEmployeePosition.positionID, Equal<Required<PX.Objects.EP.EPPosition.positionID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) row.PositionID);
    if (epEmployee != null)
      throw new PXException("This Position is assigned to the Employee '{0}' and cannot be deleted.", new object[1]
      {
        (object) epEmployee.AcctCD
      });
  }
}

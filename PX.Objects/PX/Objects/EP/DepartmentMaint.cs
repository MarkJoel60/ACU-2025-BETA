// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.DepartmentMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.EP;

public class DepartmentMaint : PXGraph<DepartmentMaint>
{
  public PXSelect<PX.Objects.EP.EPDepartment> EPDepartment;
  public PXSavePerRow<PX.Objects.EP.EPDepartment> Save;
  public PXCancel<PX.Objects.EP.EPDepartment> Cancel;
  public PXInsert<PX.Objects.EP.EPDepartment> Insert;
  public PXDelete<PX.Objects.EP.EPDepartment> Delete;

  public DepartmentMaint()
  {
    ((PXAction) this.Insert).SetVisible(false);
    ((PXAction) this.Delete).SetVisible(false);
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Department")]
  protected virtual void _(Events.CacheAttached<PX.Objects.EP.EPDepartment.departmentID> e)
  {
  }

  protected virtual void EPDepartment_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    string departmentId = ((PX.Objects.EP.EPDepartment) e.Row).DepartmentID;
    if (PXResultset<PX.Objects.EP.EPDepartment>.op_Implicit(PXSelectBase<PX.Objects.EP.EPDepartment, PXSelect<PX.Objects.EP.EPDepartment, Where<PX.Objects.EP.EPDepartment.departmentID, Equal<Required<PX.Objects.EP.EPDepartment.departmentID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) departmentId
    })) == null)
      return;
    cache.RaiseExceptionHandling<PX.Objects.EP.EPDepartment.departmentID>(e.Row, (object) departmentId, (Exception) new PXException("Record already exists."));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void EPDepartment_DepartmentID_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is PX.Objects.EP.EPDepartment row) || e.NewValue == null || row.DepartmentID == null)
      return;
    EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.departmentID, Equal<Required<PX.Objects.EP.EPDepartment.departmentID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.DepartmentID
    }));
    if (epEmployee != null && epEmployee.DepartmentID != e.NewValue.ToString())
      throw new PXSetPropertyException("This Department is assigned to the Employee and cannot be changed.");
  }
}

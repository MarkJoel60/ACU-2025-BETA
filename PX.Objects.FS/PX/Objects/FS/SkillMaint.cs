// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SkillMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class SkillMaint : PXGraph<SkillMaint>
{
  [PXImport(typeof (FSSkill))]
  public PXSelect<FSSkill> SkillRecords;
  public PXSave<FSSkill> Save;
  public PXCancel<FSSkill> Cancel;

  protected virtual void _(Events.RowDeleting<FSSkill> e)
  {
    if (e.Row == null)
      return;
    FSEmployeeSkill fsEmployeeSkill = PXResultset<FSEmployeeSkill>.op_Implicit(PXSelectBase<FSEmployeeSkill, PXSelect<FSEmployeeSkill, Where<FSEmployeeSkill.skillID, Equal<Required<FSSkill.skillID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) e.Row.SkillID
    }));
    FSServiceSkill fsServiceSkill = PXResultset<FSServiceSkill>.op_Implicit(PXSelectBase<FSServiceSkill, PXSelect<FSServiceSkill, Where<FSServiceSkill.skillID, Equal<Required<FSSkill.skillID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) e.Row.SkillID
    }));
    if (fsEmployeeSkill != null || fsServiceSkill != null)
      throw new PXException("This skill is specified for at least one non-stock item on the Non-Stock Items (IN202000) form, or an employee on the Employees (EP203000) form.");
  }
}

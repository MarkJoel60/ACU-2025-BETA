// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.CostCodeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CT;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

[Serializable]
public class CostCodeMaint : PXGraph<CostCodeMaint>, PXImportAttribute.IPXPrepareItems
{
  [PXImport(typeof (PMCostCode))]
  [PXViewName("Cost Code")]
  public PXSelect<PMCostCode> Items;
  public PXSavePerRow<PMCostCode> Save;
  public PXCancel<PMCostCode> Cancel;
  public ChangeCostCode changeID;
  public PXSetup<PMSetup> Setup;

  [PXBool]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMCostCode.isProjectOverride> e)
  {
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable ChangeID(PXAdapter adapter)
  {
    return ((PXAction) new ChangeCostCode((PXGraph) this, "changeID")).Press(adapter);
  }

  public CostCodeMaint()
  {
    PMSetup current = ((PXSelectBase<PMSetup>) this.Setup).Current;
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PMCostCode> e)
  {
    if (e.Row.IsDefault.GetValueOrDefault())
      throw new PXException("This is a system record and cannot be deleted.");
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMCostCode> e)
  {
    PMCostCode row = e.Row;
    if (row == null)
      return;
    bool flag = PXContext.PXIdentity.User.IsInRole(PredefinedRoles.ProjectAccountant);
    PXUIFieldAttribute.SetEnabled<PMCostCode.isActive>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostCode>>) e).Cache, (object) row, flag);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PMCostCode.isActive> e)
  {
    if (!(e.Row is PMCostCode row))
      return;
    bool? oldValue = e.OldValue as bool?;
    bool? newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMCostCode.isActive>, object, object>) e).NewValue as bool?;
    if (!oldValue.GetValueOrDefault())
      return;
    bool? nullable = newValue;
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    PXResult<PMBudget> pxResult = !row.IsDefault.GetValueOrDefault() ? ((IQueryable<PXResult<PMBudget>>) PXSelectBase<PMBudget, PXViewOf<PMBudget>.BasedOn<SelectFromBase<PMBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMTask>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMBudget.projectTaskID, Equal<PMTask.taskID>>>>>.And<BqlOperand<PMBudget.projectID, IBqlInt>.IsEqual<PMTask.projectID>>>>, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMBudget.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTask.status, Equal<ProjectTaskStatus.active>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>, And<BqlOperand<PMProject.nonProject, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<PMBudget.costCodeID, IBqlInt>.IsEqual<P.AsInt>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) row.CostCodeID
    })).FirstOrDefault<PXResult<PMBudget>>() : throw new PXSetPropertyException<PMCostCode.isActive>("The default cost code cannot be deactivated.", (PXErrorLevel) 5);
    if (pxResult != null)
      throw new PXSetPropertyException<PMCostCode.isActive>("The {0} cost code cannot be deactivated because it is currently in use for the {1} project.", (PXErrorLevel) 5, new object[2]
      {
        (object) CostCodeSelectorAttribute.FormatValueByMask<PMCostCode.costCodeCD>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMCostCode.isActive>>) e).Cache, (object) row, row.CostCodeCD),
        (object) ((PXResult) pxResult).GetItem<PMProject>().ContractCD
      });
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    if (this.IsKeyChanged(keys, values))
      throw new PXException("Cost code number cannot be updated directly. Use the Change ID action.");
    return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
  }

  public virtual bool IsKeyChanged(IDictionary keys, IDictionary values)
  {
    if (keys.Contains((object) "CostCodeCD"))
    {
      string key = (string) keys[(object) "CostCodeCD"];
      if (!string.IsNullOrEmpty(key) && values.Contains((object) "CostCodeCD"))
      {
        string str = (string) values[(object) "CostCodeCD"];
        if (key != str)
          return true;
      }
    }
    return false;
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values) => true;

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public void PrepareItems(string viewName, IEnumerable items)
  {
  }
}

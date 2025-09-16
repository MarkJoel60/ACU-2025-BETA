// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.INIssueEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.PM;

public class INIssueEntryExt : ProjectCostCenterSupport<INIssueEntry>
{
  public PXSetupOptional<PMSetup> Setup;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  protected virtual bool IsPMVisible
  {
    get
    {
      PMSetup current = ((PXSelectBase<PMSetup>) this.Setup).Current;
      return current != null && current.VisibleInIN.GetValueOrDefault();
    }
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<INTran.tranType>, NotIn3<INTranType.issue, INTranType.invoice, INTranType.debitMemo>>>>>.Or<BqlOperand<Current<INTran.inventorySource>, IBqlString>.IsNotEqual<InventorySourceType.freeStock>>>>, Or<BqlOperand<PMProject.accountingMode, IBqlString>.IsNotEqual<ProjectAccountingModes.linked>>>>.Or<BqlOperand<PMProject.nonProject, IBqlBool>.IsEqual<True>>>), "The Free Stock inventory source cannot be used in the line because Track by Location is selected for the {0} project in the Inventory Tracking box on the Projects (PM301000) form.", new Type[] {typeof (PMProject.contractCD)}, SuppressVerify = true)]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<INTran.tranType>, NotIn3<INTranType.issue, INTranType.invoice, INTranType.debitMemo>>>>>.Or<BqlOperand<Current<INTran.inventorySource>, IBqlString>.IsNotEqual<InventorySourceType.freeStock>>>>, Or<BqlOperand<PMProject.accountingMode, IBqlString>.IsEqual<ProjectAccountingModes.linked>>>, Or<BqlOperand<PMProject.allowIssueFromFreeStock, IBqlBool>.IsEqual<True>>>>.Or<BqlOperand<PMProject.nonProject, IBqlBool>.IsEqual<True>>>), "The Free Stock inventory source cannot be used if the project selected in the line has the Allow Issue from Free Stock check box cleared on the Summary tab of the Projects (PM301000) form. Select a different inventory source or allow issuing from free stock for the {0} project.", new Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<INTran.tranType>, NotIn3<INTranType.return_, INTranType.creditMemo>>>>>.Or<BqlOperand<Current<INTran.inventorySource>, IBqlString>.IsNotEqual<InventorySourceType.freeStock>>>>>.Or<BqlOperand<PMProject.nonProject, IBqlBool>.IsEqual<True>>>), "If the Return or Credit Memo transaction type is specified in the line, the inventory source of the Free Stock type can be used only with the non-project code.", new Type[] {}, SuppressVerify = true)]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<INTran.inventorySource>, NotEqual<InventorySourceType.projectStock>>>>>.Or<BqlOperand<PMProject.nonProject, IBqlBool>.IsNotEqual<True>>>), "The Project Stock inventory source cannot be used if the non-project code is selected in the line. Select a different inventory source or specify a project for this line.", new Type[] {})]
  protected override void _(PX.Data.Events.CacheAttached<INTran.projectID> e)
  {
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<INTran, INTran.tranType> e)
  {
    if (e.Row == null || e.Row.InventorySource != "F" || e.NewValue == null || EnumerableExtensions.IsNotIn<object>(e.NewValue, (object) "RET", (object) "CRM"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.tranType>>) e).Cache.SetDefaultExt<INTran.projectID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.tranType>>) e).Cache.SetDefaultExt<INTran.taskID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.tranType>>) e).Cache.SetDefaultExt<INTran.costCodeID>((object) e.Row);
    foreach (PXResult<INTranSplit> pxResult in ((PXSelectBase<INTranSplit>) this.Base.splits).Select(Array.Empty<object>()))
    {
      INTranSplit inTranSplit = PXResult<INTranSplit>.op_Implicit(pxResult);
      ((PXSelectBase) this.Base.splits).Cache.SetDefaultExt<INTranSplit.projectID>((object) inTranSplit);
      ((PXSelectBase) this.Base.splits).Cache.SetDefaultExt<INTranSplit.taskID>((object) inTranSplit);
    }
  }

  protected override void _(PX.Data.Events.FieldDefaulting<INTran, INTran.projectID> e)
  {
    base._(e);
    INTran row = e.Row;
    if (row == null || !ProjectAttribute.IsPMVisible("IN") || !row.LocationID.HasValue)
      return;
    foreach (PXResult<INLocation, PMProject> pxResult in PXSelectBase<INLocation, PXSelectReadonly2<INLocation, LeftJoin<PMProject, On<PMProject.contractID, Equal<INLocation.projectID>>>, Where<INLocation.siteID, Equal<Required<INLocation.siteID>>, And<INLocation.locationID, Equal<Required<INLocation.locationID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) row.SiteID,
      (object) row.LocationID
    }))
    {
      PMProject pmProject = PXResult<INLocation, PMProject>.op_Implicit(pxResult);
      if (pmProject != null && pmProject.ContractCD != null && pmProject.VisibleInIN.GetValueOrDefault())
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.projectID>, INTran, object>) e).NewValue = (object) pmProject.ContractCD;
        break;
      }
    }
  }

  protected override void _(PX.Data.Events.FieldVerifying<INTran, INTran.projectID> e)
  {
    if (!((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.projectID>>) e).ExternalCall || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.projectID>, INTran, object>) e).NewValue == null || !(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.projectID>, INTran, object>) e).NewValue is int?))
      return;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.projectID>, INTran, object>) e).NewValue);
    if (pmProject == null || pmProject.NonProject.GetValueOrDefault() || !(pmProject.AccountingMode == "L") || !e.Row.LocationID.HasValue)
      return;
    INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, e.Row.LocationID);
    if (inLocation == null)
      return;
    int? projectId = inLocation.ProjectID;
    int? contractId = pmProject.ContractID;
    if (!(projectId.GetValueOrDefault() == contractId.GetValueOrDefault() & projectId.HasValue == contractId.HasValue) && (inLocation.ProjectID.HasValue || PXAccess.FeatureInstalled<FeaturesSet.materialManagement>()))
    {
      PXSetPropertyException<INTran.projectID> propertyException = new PXSetPropertyException<INTran.projectID>("The {0} project cannot be selected because the {1} warehouse location specified in the line is not linked to this project. Select another project, or change the warehouse location.", (PXErrorLevel) 4, new object[2]
      {
        (object) pmProject.ContractCD,
        (object) inLocation.LocationCD
      });
      ((PXSetPropertyException) propertyException).ErrorValue = (object) pmProject.ContractCD;
      throw propertyException;
    }
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<INTran, INTran.taskID> e)
  {
    INTran row = e.Row;
    if (row == null || !ProjectAttribute.IsPMVisible("IN") || !row.LocationID.HasValue)
      return;
    foreach (PXResult<INLocation, PMTask> pxResult in PXSelectBase<INLocation, PXSelectReadonly2<INLocation, LeftJoin<PMTask, On<PMTask.projectID, Equal<INLocation.projectID>, And<PMTask.taskID, Equal<INLocation.taskID>>>>, Where<INLocation.siteID, Equal<Required<INLocation.siteID>>, And<INLocation.locationID, Equal<Required<INLocation.locationID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) row.SiteID,
      (object) row.LocationID
    }))
    {
      PMTask pmTask = PXResult<INLocation, PMTask>.op_Implicit(pxResult);
      INLocation inLocation = PXResult<INLocation, PMTask>.op_Implicit(pxResult);
      if (pmTask != null && pmTask.TaskCD != null && pmTask.VisibleInIN.GetValueOrDefault())
      {
        int? projectId1 = pmTask.ProjectID;
        int? projectId2 = row.ProjectID;
        if (projectId1.GetValueOrDefault() == projectId2.GetValueOrDefault() & projectId1.HasValue == projectId2.HasValue)
        {
          projectId2 = inLocation.ProjectID;
          if (projectId2.HasValue)
          {
            projectId2 = row.ProjectID;
            projectId1 = inLocation.ProjectID;
            if (projectId2.GetValueOrDefault() == projectId1.GetValueOrDefault() & projectId2.HasValue == projectId1.HasValue)
            {
              ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.taskID>, INTran, object>) e).NewValue = (object) pmTask.TaskCD;
              break;
            }
          }
        }
      }
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<INTran, INTran.locationID> e)
  {
    if (e.Row == null || !((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).ExternalCall)
      return;
    if (this.IsProjectLocation((int?) e.NewValue))
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.SetValue<INTran.inventorySource>((object) e.Row, (object) "P");
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.SetDefaultExt<INTran.projectID>((object) e.Row);
    if (((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.GetValuePending<INTran.taskID>((object) e.Row) == null)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.SetValuePending<INTran.taskID>((object) e.Row, PXCache.NotSetValue);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.SetDefaultExt<INTran.taskID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<INTran, INTran.reasonCode> e)
  {
    INTran row = e.Row;
    if (row == null)
      return;
    PX.Objects.CS.ReasonCode reasonCode = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this.Base, (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.reasonCode>, INTran, object>) e).NewValue);
    if (reasonCode == null || !row.ProjectID.HasValue || ProjectDefaultAttribute.IsNonProject(row.ProjectID))
      return;
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) reasonCode.AccountID
    }));
    if (account == null || account.AccountGroupID.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<INTran, INTran.reasonCode>>) e).Cache.RaiseExceptionHandling<INTran.reasonCode>((object) e.Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Record is associated with Project whereas Account '{0}' is not associated with any Account Group", (PXErrorLevel) 2, new object[1]
    {
      (object) account.AccountCD
    }));
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INTranSplit, INTranSplit.projectID> e)
  {
    if (!(PXParentAttribute.SelectParent(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<INTranSplit, INTranSplit.projectID>>) e).Cache, (object) e.Row, typeof (INTran)) is INTran inTran))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTranSplit, INTranSplit.projectID>, INTranSplit, object>) e).NewValue = (object) inTran.ProjectID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INTranSplit, INTranSplit.taskID> e)
  {
    if (!(PXParentAttribute.SelectParent(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<INTranSplit, INTranSplit.taskID>>) e).Cache, (object) e.Row, typeof (INTran)) is INTran inTran))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTranSplit, INTranSplit.taskID>, INTranSplit, object>) e).NewValue = (object) inTran.TaskID;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.IN.INRegister> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<INTran.projectID>(((PXSelectBase) this.Base.transactions).Cache, (object) null, this.IsPMVisible);
    PXUIFieldAttribute.SetVisible<INTran.taskID>(((PXSelectBase) this.Base.transactions).Cache, (object) null, this.IsPMVisible);
  }

  [PXOverride]
  public virtual (bool? Project, bool? Task) IsProjectTaskEnabled(
    INTran row,
    Func<INTran, (bool? Project, bool? Task)> baseMethod)
  {
    (bool?, bool?) valueTuple = baseMethod(row);
    INIssueEntryExt.LinkedInfo linkedInfo = this.GetLinkedInfo(row.LocationID);
    return (new bool?((valueTuple.Item1 ?? true) && !linkedInfo.IsLinked), new bool?((valueTuple.Item2 ?? true) && !linkedInfo.IsTaskRestricted));
  }

  protected virtual void _(PX.Data.Events.RowSelected<INTran> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<INTran.inventorySource>(((PXSelectBase) this.Base.transactions).Cache, (object) e.Row, !this.IsProjectLocation(e.Row.LocationID));
  }

  public override void ValidateForPersisting(INTran tran)
  {
    this.ValidateProjectLayerFields(tran);
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, tran.ProjectID);
    if (pmProject.AccountingMode == "L")
    {
      if (pmProject.NonProject.GetValueOrDefault() && tran.InventorySource == "P")
      {
        ((PXCache) GraphHelper.Caches<INTran>((PXGraph) this.Base)).RaiseExceptionHandling<INTran.projectID>((object) tran, (object) pmProject.ContractCD, (Exception) new PXSetPropertyException((IBqlTable) tran, "The Project Stock inventory source cannot be used if the non-project code is selected in the line. Select a different inventory source or specify a project for this line.", (PXErrorLevel) 4));
        throw new PXRowPersistingException("projectID", (object) tran.ProjectID, "The Project Stock inventory source cannot be used if the non-project code is selected in the line. Select a different inventory source or specify a project for this line.", new object[1]
        {
          (object) pmProject.ContractCD
        });
      }
      this.CheckForSingleLocation((PXCache) GraphHelper.Caches<INTran>((PXGraph) this.Base), tran);
      this.CheckLocationTaskRule((PXCache) GraphHelper.Caches<INTran>((PXGraph) this.Base), tran);
    }
    else
    {
      if (!(tran.InventorySource == "F"))
        return;
      bool? nullable = pmProject.AllowIssueFromFreeStock;
      if (!nullable.GetValueOrDefault())
      {
        ((PXCache) GraphHelper.Caches<INTran>((PXGraph) this.Base)).RaiseExceptionHandling<INTran.projectID>((object) tran, (object) pmProject.ContractCD, (Exception) new PXSetPropertyException((IBqlTable) tran, "The Free Stock inventory source cannot be used if the project selected in the line has the Allow Issue from Free Stock check box cleared on the Summary tab of the Projects (PM301000) form. Select a different inventory source or allow issuing from free stock for the {0} project.", (PXErrorLevel) 4));
        throw new PXRowPersistingException("projectID", (object) tran.ProjectID, "The Free Stock inventory source cannot be used if the project selected in the line has the Allow Issue from Free Stock check box cleared on the Summary tab of the Projects (PM301000) form. Select a different inventory source or allow issuing from free stock for the {0} project.", new object[1]
        {
          (object) pmProject.ContractCD
        });
      }
      if (!EnumerableExtensions.IsIn<string>(tran.TranType, "RET", "CRM"))
        return;
      nullable = pmProject.NonProject;
      if (!nullable.GetValueOrDefault())
      {
        ((PXCache) GraphHelper.Caches<INTran>((PXGraph) this.Base)).RaiseExceptionHandling<INTran.projectID>((object) tran, (object) pmProject.ContractCD, (Exception) new PXSetPropertyException((IBqlTable) tran, "If the Return or Credit Memo transaction type is specified in the line, the inventory source of the Free Stock type can be used only with the non-project code.", (PXErrorLevel) 4));
        throw new PXRowPersistingException("projectID", (object) tran.ProjectID, "If the Return or Credit Memo transaction type is specified in the line, the inventory source of the Free Stock type can be used only with the non-project code.", new object[1]
        {
          (object) pmProject.ContractCD
        });
      }
    }
  }

  protected virtual void CheckLocationTaskRule(PXCache sender, INTran row)
  {
    if (!row.TaskID.HasValue)
      return;
    INLocation inLocation = INLocation.PK.Find(sender.Graph, row.LocationID);
    if (inLocation == null)
      return;
    int? taskId1 = inLocation.TaskID;
    int? taskId2 = row.TaskID;
    if (taskId1.GetValueOrDefault() == taskId2.GetValueOrDefault() & taskId1.HasValue == taskId2.HasValue || !inLocation.TaskID.HasValue && !inLocation.ProjectID.HasValue)
      return;
    sender.RaiseExceptionHandling<INTran.locationID>((object) row, (object) inLocation.LocationCD, (Exception) new PXSetPropertyException("The Project Task specified for the given Location do not match the selected Project Task.", (PXErrorLevel) 2));
  }

  protected virtual void CheckForSingleLocation(PXCache sender, INTran row)
  {
    if (!row.TaskID.HasValue || row.LocationID.HasValue)
      return;
    Decimal? qty = row.Qty;
    Decimal num = 0M;
    if (qty.GetValueOrDefault() == num & qty.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, row.InventoryID);
    if (inventoryItem == null || !inventoryItem.StkItem.GetValueOrDefault() || POLineType.IsProjectDropShip(row.POLineType) || row.LocationID.HasValue)
      return;
    sender.RaiseExceptionHandling<INTran.locationID>((object) row, (object) null, (Exception) new PXSetPropertyException("When posting to Project Location must be the same for all splits."));
  }

  private INIssueEntryExt.LinkedInfo GetLinkedInfo(int? locationID)
  {
    INIssueEntryExt.LinkedInfo linkedInfo = new INIssueEntryExt.LinkedInfo();
    if (locationID.HasValue)
    {
      INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, locationID);
      if (inLocation != null && inLocation.ProjectID.HasValue)
      {
        PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, inLocation.ProjectID);
        if (pmProject != null)
        {
          linkedInfo.IsLinked = pmProject.AccountingMode == "L";
          linkedInfo.IsTaskRestricted = inLocation.TaskID.HasValue;
        }
      }
    }
    return linkedInfo;
  }

  private struct LinkedInfo
  {
    public bool IsLinked;
    public bool IsTaskRestricted;
  }
}

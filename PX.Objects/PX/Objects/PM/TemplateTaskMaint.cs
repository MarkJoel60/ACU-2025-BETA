// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TemplateTaskMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.PM;

public class TemplateTaskMaint : PXGraph<
#nullable disable
TemplateTaskMaint>
{
  public PXSelectJoin<PMTask, LeftJoin<PMProject, On<PMTask.projectID, Equal<PMProject.contractID>>>, Where<PMProject.nonProject, Equal<False>, And<PMProject.baseType, Equal<CTPRType.projectTemplate>>>> Task;
  public PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMTask.projectID>>, And<PMTask.taskID, Equal<Current<PMTask.taskID>>>>> TaskProperties;
  public PXSelect<PMRecurringItem, Where<PMRecurringItem.projectID, Equal<Current<PMTask.projectID>>, And<PMRecurringItem.taskID, Equal<Current<PMTask.taskID>>>>> BillingItems;
  public FbqlSelect<SelectFromBase<PMBudget, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMBudget.projectID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PMTask.projectID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PMBudget.projectTaskID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMTask.taskID, IBqlInt>.FromCurrent>>>, 
  #nullable disable
  PMBudget>.View TaskBudgets;
  [PXViewName("Task Answers")]
  public CRAttributeList<PMTask> Answers;
  public PXSetup<PMSetup> Setup;
  public PXSetup<Company> CompanySetup;
  public PXSave<PMTask> Save;
  public PXCancel<PMTask> Cancel;
  public PXInsert<PMTask> Insert;
  public PXDelete<PMTask> Delete;
  public PXFirst<PMTask> First;
  public PXPrevious<PMTask> previous;
  public PXNext<PMTask> next;
  public PXLast<PMTask> Last;

  [Project(typeof (Where<PMProject.baseType, Equal<CTPRType.projectTemplate>, And<PMProject.nonProject, Equal<False>>>), DisplayName = "Project Template ID", IsKey = true)]
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [PXDefault(typeof (PMProject.contractID))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.projectID> e)
  {
  }

  [PXDimensionSelector("PROTASK", typeof (Search<PMTask.taskCD, Where<PMTask.projectID, Equal<Current<PMTask.projectID>>>>), typeof (PMTask.taskCD), new System.Type[] {typeof (PMTask.taskCD), typeof (PMTask.locationID), typeof (PMTask.description), typeof (PMTask.status)}, DescriptionField = typeof (PMTask.description))]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.taskCD> e)
  {
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("A")]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.status> e)
  {
  }

  [PXMergeAttributes]
  [PXBool]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMCostCode.isProjectOverride> e)
  {
  }

  public TemplateTaskMaint()
  {
    if (((PXSelectBase<PMSetup>) this.Setup).Current == null)
      throw new PXException("Project Management Setup is not configured.");
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMTask> e)
  {
    if (e.Row == null)
      return;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) e.Row
    }, Array.Empty<object>()));
    PXUIFieldAttribute.SetEnabled<PMTask.autoIncludeInPrj>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTask>>) e).Cache, (object) e.Row, pmProject != null && !pmProject.NonProject.GetValueOrDefault());
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMTask, PMTask.isDefault> e)
  {
    if (!e.Row.IsDefault.GetValueOrDefault())
      return;
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) new PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) e.Row.ProjectID
    }))
    {
      PMTask pmTask = PXResult<PMTask>.op_Implicit(pxResult);
      if (pmTask.IsDefault.GetValueOrDefault())
      {
        int? taskId1 = pmTask.TaskID;
        int? taskId2 = e.Row.TaskID;
        if (!(taskId1.GetValueOrDefault() == taskId2.GetValueOrDefault() & taskId1.HasValue == taskId2.HasValue))
        {
          ((PXSelectBase) this.Task).Cache.SetValue<PMTask.isDefault>((object) pmTask, (object) false);
          GraphHelper.SmartSetStatus(((PXSelectBase) this.Task).Cache, (object) pmTask, (PXEntryStatus) 1, (PXEntryStatus) 0);
        }
      }
    }
  }

  protected virtual void PMTask_CustomerID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is PMTask row))
      return;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (pmProject == null || !pmProject.NonProject.GetValueOrDefault())
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PMTask_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    PMTask row = e.Row as PMTask;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (row == null || pmProject == null)
      return;
    row.BillingID = pmProject.BillingID;
    row.DefaultSalesAccountID = pmProject.DefaultSalesAccountID;
    row.DefaultSalesSubID = pmProject.DefaultSalesSubID;
    row.DefaultExpenseAccountID = pmProject.DefaultExpenseAccountID;
    row.DefaultExpenseSubID = pmProject.DefaultExpenseSubID;
  }

  protected virtual void PMTask_ProjectID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMTask))
      return;
    sender.SetDefaultExt<PMTask.customerID>(e.Row);
    sender.SetDefaultExt<PMTask.defaultSalesAccountID>(e.Row);
    sender.SetDefaultExt<PMTask.defaultSalesSubID>(e.Row);
    sender.SetDefaultExt<PMTask.defaultExpenseAccountID>(e.Row);
    sender.SetDefaultExt<PMTask.defaultExpenseSubID>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRecurringItem, PMRecurringItem.inventoryID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRecurringItem, PMRecurringItem.inventoryID>>) e).Cache.SetDefaultExt<PMRecurringItem.description>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRecurringItem, PMRecurringItem.inventoryID>>) e).Cache.SetDefaultExt<PMRecurringItem.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRecurringItem, PMRecurringItem.inventoryID>>) e).Cache.SetDefaultExt<PMRecurringItem.amount>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMRecurringItem, PMRecurringItem.amount> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.InventoryID
    }));
    if (inventoryItem == null)
      return;
    InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this, inventoryItem.InventoryID, ((PXGraph) this).Accessinfo.BaseCuryID);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRecurringItem, PMRecurringItem.amount>, PMRecurringItem, object>) e).NewValue = (object) (Decimal?) itemCurySettings?.BasePrice;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMRecurringItem> e)
  {
    if (e.Row == null || ((PXSelectBase<PMTask>) this.Task).Current == null)
      return;
    PXUIFieldAttribute.SetEnabled<PMRecurringItem.included>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRecurringItem>>) e).Cache, (object) e.Row, !((PXSelectBase<PMTask>) this.Task).Current.IsActive.GetValueOrDefault());
  }
}

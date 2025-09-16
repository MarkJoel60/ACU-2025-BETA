// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TemplateGlobalTaskMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.PM;

public class TemplateGlobalTaskMaint : PXGraph<TemplateGlobalTaskMaint>
{
  public PXSelectJoin<PMTask, LeftJoin<PMProject, On<PMTask.projectID, Equal<PMProject.contractID>>>, Where<PMProject.nonProject, Equal<True>>> Task;
  public PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMTask.projectID>>, And<PMTask.taskID, Equal<Current<PMTask.taskID>>>>> TaskProperties;
  public PXSelect<PMRecurringItem, Where<PMRecurringItem.projectID, Equal<Current<PMTask.projectID>>, And<PMRecurringItem.taskID, Equal<Current<PMTask.taskID>>>>> BillingItems;
  [PXImport(typeof (PMTask))]
  public PXSelectJoin<PMBudget, LeftJoin<PMAccountGroup, On<PMBudget.accountGroupID, Equal<PMAccountGroup.groupID>>>, Where<PMBudget.projectID, Equal<Current<PMTask.projectID>>, And<PMBudget.projectTaskID, Equal<Current<PMTask.taskID>>>>> Budget;
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

  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [PXDefault(typeof (Search<PMProject.contractID, Where<PMProject.nonProject, Equal<True>>>))]
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

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInGL>))]
  [PXUIField(DisplayName = "GL")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInGL> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInAP>))]
  [PXUIField(DisplayName = "AP")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInAP> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInAR>))]
  [PXUIField(DisplayName = "AR")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInAR> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInCA>))]
  [PXUIField(DisplayName = "CA")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInCA> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInCR>))]
  [PXUIField(DisplayName = "CRM")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInCR> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInTA>))]
  [PXUIField(DisplayName = "Time Entries")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInTA> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInEA>))]
  [PXUIField(DisplayName = "Expenses")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInEA> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInIN>))]
  [PXUIField(DisplayName = "IN")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInIN> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInSO>))]
  [PXUIField(DisplayName = "SO")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInSO> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInPO>))]
  [PXUIField(DisplayName = "PO")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInPO> e)
  {
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Rate Table Code")]
  [PXSelector(typeof (PMRateTable.rateTableID), DescriptionField = typeof (PMRateTable.description))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.rateTableID> e)
  {
  }

  [PXDefault]
  [AccountGroup(IsKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMBudget.accountGroupID> e)
  {
  }

  [PXDefault(typeof (PMTask.projectID))]
  [PXDBInt(IsKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMBudget.projectID> e)
  {
  }

  [PXDBDefault(typeof (PMTask.taskID))]
  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<PMTask, Where<PMTask.projectID, Equal<Current<PMBudget.projectID>>, And<PMTask.taskID, Equal<Current<PMBudget.projectTaskID>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMBudget.projectTaskID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<PMBudget.curyInfoID> e)
  {
  }

  [PXDefault("N")]
  [PXUIField(DisplayName = "Reset Usage", Visible = false)]
  [PXDBString(1, IsFixed = true)]
  [ResetUsageOption.ListForProject]
  protected virtual void ResetUsageCacheAttached(PX.Data.Events.CacheAttached<PMRecurringItem.resetUsage> e)
  {
  }

  [PXMergeAttributes]
  [PXBool]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMCostCode.isProjectOverride> e)
  {
  }

  [InjectDependency]
  public IUnitRateService RateService { get; set; }

  public TemplateGlobalTaskMaint()
  {
    if (((PXSelectBase<PMSetup>) this.Setup).Current == null)
      throw new PXException("Project Management Setup is not configured.");
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.costCodeID> e)
  {
    if (CostCodeAttribute.UseCostCode())
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.costCodeID>, PMBudget, object>) e).NewValue = (object) CostCodeAttribute.GetDefaultCostCode();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.inventoryID> e)
  {
    if (!CostCodeAttribute.UseCostCode())
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.inventoryID>, PMBudget, object>) e).NewValue = (object) PMInventorySelectorAttribute.EmptyInventoryID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.progressBillingBase> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.progressBillingBase>, PMBudget, object>) e).NewValue = (object) ((PXSelectBase<PMTask>) this.Task).Current?.ProgressBillingBase;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMBudget, PMBudget.inventoryID> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMBudget, PMBudget.inventoryID>>) e).Cache.SetDefaultExt<PMBudget.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMBudget, PMBudget.inventoryID>>) e).Cache.SetDefaultExt<PMBudget.curyUnitRate>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMBudget, PMBudget.accountGroupID> e)
  {
    PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, e.Row.AccountGroupID);
    if (pmAccountGroup != null)
    {
      bool? isExpense = pmAccountGroup.IsExpense;
      e.Row.Type = !isExpense.GetValueOrDefault() ? pmAccountGroup.Type : "E";
    }
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMBudget, PMBudget.accountGroupID>>) e).Cache.SetDefaultExt<PMBudget.curyUnitRate>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.curyUnitRate> e)
  {
    PMBudget row = e.Row;
    if ((row != null ? (!row.AccountGroupID.HasValue ? 1 : 0) : 1) != 0)
      return;
    int? nullable1 = e.Row.InventoryID;
    if (!nullable1.HasValue)
      return;
    nullable1 = e.Row.InventoryID;
    int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
    if (nullable1.GetValueOrDefault() == emptyInventoryId & nullable1.HasValue)
      return;
    Decimal? nullable2;
    if (e.Row.Type == "E")
    {
      IUnitRateService rateService = this.RateService;
      PXCache cache = ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.curyUnitRate>>) e).Cache;
      nullable1 = new int?();
      int? projectID = nullable1;
      nullable1 = new int?();
      int? projectTaskID = nullable1;
      int? inventoryId = e.Row.InventoryID;
      string uom = e.Row.UOM;
      nullable1 = new int?();
      int? employeeID = nullable1;
      DateTime? date = new DateTime?();
      long? curyInfoID = new long?();
      nullable2 = rateService.CalculateUnitCost(cache, projectID, projectTaskID, inventoryId, uom, employeeID, date, curyInfoID);
    }
    else
    {
      IUnitRateService rateService = this.RateService;
      PXCache cache = ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.curyUnitRate>>) e).Cache;
      nullable1 = new int?();
      int? projectID = nullable1;
      nullable1 = new int?();
      int? projectTaskID = nullable1;
      int? inventoryId = e.Row.InventoryID;
      string uom = e.Row.UOM;
      Decimal? qty = new Decimal?();
      DateTime? date = new DateTime?();
      long? curyInfoID = new long?();
      nullable2 = rateService.CalculateUnitPrice(cache, projectID, projectTaskID, inventoryId, uom, qty, date, curyInfoID);
    }
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.curyUnitRate>, PMBudget, object>) e).NewValue = (object) nullable2.GetValueOrDefault();
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
    PXUIFieldAttribute.SetRequired<PMBudget.costCodeID>(((PXSelectBase) this.Budget).Cache, CostCodeAttribute.UseCostCode());
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMBudget> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<PMBudget.isProduction>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMBudget>>) e).Cache, (object) e.Row, e.Row.Type == "E");
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

  protected virtual void PMTask_ProjectID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMTask))
      return;
    sender.SetDefaultExt<PMTask.customerID>(e.Row);
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

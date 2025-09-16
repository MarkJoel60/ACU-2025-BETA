// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectTaskEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.PM;

public class ProjectTaskEntry : PXGraph<
#nullable disable
ProjectTaskEntry, PMTask>
{
  public FbqlSelect<SelectFromBase<PMTask, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PMProject>.On<BqlOperand<
  #nullable enable
  PMProject.contractID, IBqlInt>.IsEqual<
  #nullable disable
  PMTask.projectID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMProject.nonProject, 
  #nullable disable
  Equal<False>>>>, And<BqlOperand<
  #nullable enable
  PMProject.baseType, IBqlString>.IsEqual<
  #nullable disable
  CTPRType.project>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMTask.projectID, 
  #nullable disable
  IsNull>>>>.Or<MatchUserFor<PMProject>>>>, PMTask>.View Task;
  [PXViewName("Project Task")]
  public PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMTask.projectID>>, And<PMTask.taskID, Equal<Current<PMTask.taskID>>>>> TaskProperties;
  public PXSelect<PMRecurringItem, Where<PMRecurringItem.projectID, Equal<Current<PMTask.projectID>>, And<PMRecurringItem.taskID, Equal<Current<PMTask.taskID>>>>> BillingItems;
  public PXSelect<PMBudget, Where<PMBudget.projectID, Equal<Current<PMTask.projectID>>, And<PMBudget.projectTaskID, Equal<Current<PMTask.taskID>>>>> TaskBudgets;
  [PXViewName("Task Answers")]
  public CRAttributeList<PMTask> Answers;
  public PXSetup<PMSetup> Setup;
  public PXSetup<Company> CompanySetup;
  [PXViewName("Project")]
  public PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>> Project;
  [PXReadOnlyView]
  public PXSelect<CRCampaign, Where<CRCampaign.projectID, Equal<Current<PMTask.projectID>>, And<CRCampaign.projectTaskID, Equal<Current<PMTask.taskID>>>>> TaskCampaign;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<PMForecastDetail> ForecastDetails;
  public PXAction<PMTask> activate;
  public PXAction<PMTask> complete;
  public PXAction<PMTask> hold;
  public PXAction<PMTask> cancelTask;

  [PX.Objects.PM.Project(typeof (Where<PMProject.nonProject, NotEqual<True>, And<PMProject.baseType, Equal<CTPRType.project>>>), false, DisplayName = "Project ID", IsKey = true)]
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [PXDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.projectID> e)
  {
  }

  [PXDimensionSelector("PROTASK", typeof (Search<PMTask.taskCD, Where<PMTask.projectID, Equal<Current<PMTask.projectID>>>>), typeof (PMTask.taskCD), new System.Type[] {typeof (PMTask.taskCD), typeof (PMTask.locationID), typeof (PMTask.description), typeof (PMTask.status)}, DescriptionField = typeof (PMTask.description))]
  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.taskCD> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<CRCampaign.campaignID> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<PMTask, Where<PMTask.projectID, Equal<Current<PMBudget.projectID>>, And<PMTask.taskID, Equal<Current<PMBudget.projectTaskID>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMBudget.projectTaskID> e)
  {
  }

  [PXMergeAttributes]
  [PXParent(typeof (Select<PMTask, Where<PMTask.taskID, Equal<Current<PMForecastDetail.projectTaskID>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMForecastDetail.projectTaskID> e)
  {
  }

  [PXMergeAttributes]
  [PXBool]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMCostCode.isProjectOverride> e)
  {
  }

  public ProjectTaskEntry()
  {
    if (((PXSelectBase<PMSetup>) this.Setup).Current == null)
      throw new PXException("Project Management Setup is not configured.");
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Activate")]
  protected virtual IEnumerable Activate(PXAdapter adapter)
  {
    if (((PXSelectBase<PMTask>) this.Task).Current != null && !((PXSelectBase<PMTask>) this.Task).Current.StartDate.HasValue)
    {
      ((PXSelectBase<PMTask>) this.Task).Current.StartDate = ((PXGraph) this).Accessinfo.BusinessDate;
      ((PXSelectBase<PMTask>) this.Task).Update(((PXSelectBase<PMTask>) this.Task).Current);
    }
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Complete")]
  protected virtual IEnumerable Complete(PXAdapter adapter)
  {
    if (((PXSelectBase<PMTask>) this.Task).Current != null)
    {
      if (!((PXSelectBase<PMTask>) this.Task).Current.EndDate.HasValue)
        ((PXSelectBase<PMTask>) this.Task).Current.EndDate = ((PXGraph) this).Accessinfo.BusinessDate;
      ((PXSelectBase<PMTask>) this.Task).Current.CompletedPercent = new Decimal?(PMTaskCompletedAttribute.GetCompletionPercentageOfCompletedTask(((PXSelectBase<PMTask>) this.Task).Current));
      ((PXSelectBase<PMTask>) this.Task).Update(((PXSelectBase<PMTask>) this.Task).Current);
    }
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable Hold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Cancel")]
  protected virtual IEnumerable CancelTask(PXAdapter adapter) => adapter.Get();

  protected virtual void PMTask_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PMTask row))
      return;
    PXCache pxCache1 = sender;
    PMTask pmTask1 = row;
    bool? nullable = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInGL;
    int num1 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMTask.visibleInGL>(pxCache1, (object) pmTask1, num1 != 0);
    PXCache pxCache2 = sender;
    PMTask pmTask2 = row;
    nullable = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInAP;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMTask.visibleInAP>(pxCache2, (object) pmTask2, num2 != 0);
    PXCache pxCache3 = sender;
    PMTask pmTask3 = row;
    nullable = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInAR;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMTask.visibleInAR>(pxCache3, (object) pmTask3, num3 != 0);
    PXCache pxCache4 = sender;
    PMTask pmTask4 = row;
    nullable = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInSO;
    int num4 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMTask.visibleInSO>(pxCache4, (object) pmTask4, num4 != 0);
    PXCache pxCache5 = sender;
    PMTask pmTask5 = row;
    nullable = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInPO;
    int num5 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMTask.visibleInPO>(pxCache5, (object) pmTask5, num5 != 0);
    PXCache pxCache6 = sender;
    PMTask pmTask6 = row;
    nullable = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInTA;
    int num6 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMTask.visibleInTA>(pxCache6, (object) pmTask6, num6 != 0);
    PXCache pxCache7 = sender;
    PMTask pmTask7 = row;
    nullable = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInEA;
    int num7 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMTask.visibleInEA>(pxCache7, (object) pmTask7, num7 != 0);
    PXCache pxCache8 = sender;
    PMTask pmTask8 = row;
    nullable = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInIN;
    int num8 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMTask.visibleInIN>(pxCache8, (object) pmTask8, num8 != 0);
    PXCache pxCache9 = sender;
    PMTask pmTask9 = row;
    nullable = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInCA;
    int num9 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMTask.visibleInCA>(pxCache9, (object) pmTask9, num9 != 0);
    PMProject project = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Project).Select(Array.Empty<object>()));
    if (project == null)
      return;
    string statusFromFlags = this.GetStatusFromFlags(row);
    bool flag1 = ProjectEntry.IsProjectEditable(project);
    PXUIFieldAttribute.SetEnabled<PMTask.description>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<PMTask.rateTableID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<PMTask.allocationID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<PMTask.billingID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<PMTask.billingOption>(sender, (object) row, statusFromFlags == "D");
    PXUIFieldAttribute.SetEnabled<PMTask.completedPercent>(sender, (object) row, ((!(row.CompletedPctMethod == "M") ? 0 : (statusFromFlags != "D" ? 1 : 0)) & (flag1 ? 1 : 0)) != 0);
    PXUIFieldAttribute.SetEnabled<PMTask.taxCategoryID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<PMTask.approverID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<PMTask.startDate>(sender, (object) row, ((statusFromFlags == "D" ? 1 : (statusFromFlags == "A" ? 1 : 0)) & (flag1 ? 1 : 0)) != 0);
    PXUIFieldAttribute.SetEnabled<PMTask.endDate>(sender, (object) row, statusFromFlags != "F" & flag1);
    PXUIFieldAttribute.SetEnabled<PMTask.plannedStartDate>(sender, (object) row, statusFromFlags == "D");
    PXUIFieldAttribute.SetEnabled<PMTask.plannedEndDate>(sender, (object) row, statusFromFlags == "D");
    PXUIFieldAttribute.SetEnabled<PMTask.isDefault>(sender, (object) row, flag1);
    ((PXAction) this.activate).SetEnabled(flag1);
    ((PXAction) this.hold).SetEnabled(flag1);
    ((PXAction) this.complete).SetEnabled(flag1);
    ((PXAction) this.cancelTask).SetEnabled(flag1);
    bool flag2 = project.Status == "L";
    ((PXSelectBase) this.Task).Cache.AllowUpdate = !flag2;
    ((PXSelectBase) this.BillingItems).Cache.SetAllEditPermissions(!flag2);
    ((PXSelectBase) this.Answers).Cache.SetAllEditPermissions(!flag2);
  }

  protected virtual void PMTask_IsActive_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is PMTask) || e.NewValue == null || !(bool) e.NewValue)
      return;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Project).Select(Array.Empty<object>()));
    if (pmProject == null)
      return;
    bool? isActive = pmProject.IsActive;
    bool flag = false;
    if (!(isActive.GetValueOrDefault() == flag & isActive.HasValue))
      return;
    sender.RaiseExceptionHandling<PMTask.status>(e.Row, e.NewValue, (Exception) new PXSetPropertyException("Project is Not Active. Please Activate Project.", (PXErrorLevel) 2));
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

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMTask, PMTask.completedPctMethod> e)
  {
    ProjectTaskEntry.OnTaskCompletedPctMethodUpdated((PXGraph) this, e, ((PXSelectBase) this.Task).Cache);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Task).Cache, (object) e.Row, true);
  }

  public static void OnTaskCompletedPctMethodUpdated(
    PXGraph graph,
    PX.Data.Events.FieldUpdated<PMTask, PMTask.completedPctMethod> e,
    PXCache cache = null)
  {
    if (!(e.Row.CompletedPctMethod != "M"))
      return;
    Decimal? nullable = new Decimal?(PMTaskCompletedAttribute.CalculateTaskCompletionPercentage(graph, e.Row));
    (cache ?? ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTask, PMTask.completedPctMethod>>) e).Cache).SetValue<PMTask.completedPercent>((object) e.Row, (object) nullable);
  }

  protected virtual void PMTask_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is PMTask row))
      return;
    bool? nullable = row.IsActive;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.IsCancelled;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        throw new PXException("The task cannot be deleted. You can delete a task with only the Planning or Canceled status.");
    }
    if (PXResultset<PMTran>.op_Implicit(PXSelectBase<PMTran, PXSelect<PMTran, Where<PMTran.projectID, Equal<Required<PMTask.projectID>>, And<PMTran.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
    {
      (object) row.ProjectID,
      (object) row.TaskID
    })) != null)
      throw new PXException("Cannot delete Task since it already has at least one Transaction associated with it.");
    if (PXResultset<PMTimeActivity>.op_Implicit(PXSelectBase<PMTimeActivity, PXSelect<PMTimeActivity, Where<PMTimeActivity.projectID, Equal<Required<PMTask.projectID>>, And<PMTimeActivity.projectTaskID, Equal<Required<PMTask.taskID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
    {
      (object) row.ProjectID,
      (object) row.TaskID
    })) != null)
      throw new PXException("Cannot delete Task since it already has at least on Activity associated with it.");
    if (PXResultset<EPTimeCardItem>.op_Implicit(PXSelectBase<EPTimeCardItem, PXSelect<EPTimeCardItem, Where<EPTimeCardItem.projectID, Equal<Required<PMTask.projectID>>, And<EPTimeCardItem.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
    {
      (object) row.ProjectID,
      (object) row.TaskID
    })) != null)
      throw new PXException("Cannot delete Task since it already has at least one Time Card Item Record associated with it.");
  }

  protected virtual void PMTask_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    PMTask row = e.Row as PMTask;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (row == null || pmProject == null)
      return;
    row.CustomerID = pmProject.CustomerID;
    row.BillingID = pmProject.BillingID;
    row.AllocationID = pmProject.AllocationID;
    row.DefaultSalesAccountID = pmProject.DefaultSalesAccountID;
    row.DefaultSalesSubID = pmProject.DefaultSalesSubID;
    row.DefaultExpenseAccountID = pmProject.DefaultExpenseAccountID;
    row.DefaultExpenseSubID = pmProject.DefaultExpenseSubID;
  }

  protected virtual void PMTask_ProjectID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMTask row))
      return;
    sender.SetDefaultExt<PMTask.visibleInAP>((object) row);
    sender.SetDefaultExt<PMTask.visibleInAR>((object) row);
    sender.SetDefaultExt<PMTask.visibleInCA>((object) row);
    sender.SetDefaultExt<PMTask.visibleInCR>((object) row);
    sender.SetDefaultExt<PMTask.visibleInTA>((object) row);
    sender.SetDefaultExt<PMTask.visibleInEA>((object) row);
    sender.SetDefaultExt<PMTask.visibleInGL>((object) row);
    sender.SetDefaultExt<PMTask.visibleInIN>((object) row);
    sender.SetDefaultExt<PMTask.visibleInPO>((object) row);
    sender.SetDefaultExt<PMTask.visibleInSO>((object) row);
    sender.SetDefaultExt<PMTask.customerID>((object) row);
    sender.SetDefaultExt<PMTask.locationID>((object) row);
    sender.SetDefaultExt<PMTask.rateTableID>((object) row);
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
    PXUIFieldAttribute.SetEnabled<PMRecurringItem.accountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRecurringItem>>) e).Cache, (object) e.Row, e.Row.AccountSource != "N");
    PXUIFieldAttribute.SetEnabled<PMRecurringItem.subID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRecurringItem>>) e).Cache, (object) e.Row, e.Row.AccountSource != "N");
    PXUIFieldAttribute.SetEnabled<PMRecurringItem.subMask>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRecurringItem>>) e).Cache, (object) e.Row, e.Row.AccountSource != "N");
  }

  [PXMergeAttributes]
  [PXDateAndTime]
  [PXUIField(DisplayName = "Start Date")]
  [PXFormula(typeof (IsNull<Current<CRActivity.startDate>, Current<PMCRActivity.date>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMCRActivity.startDate> e)
  {
    EPSetup epSetup1 = (EPSetup) null;
    try
    {
      if (!(((PXGraph) this).Caches[typeof (EPSetup)]?.Current is EPSetup epSetup2))
        epSetup2 = ((PXSelectBase<EPSetup>) new PXSetupSelect<EPSetup>((PXGraph) this)).SelectSingle(Array.Empty<object>());
      epSetup1 = epSetup2;
    }
    catch
    {
    }
    PXDateAndTimeAttribute attribute = PXDateAndTimeAttribute.GetAttribute(e.Cache, (object) null, "startDate");
    string str1;
    string str2 = str1 = epSetup1 == null || !epSetup1.RequireTimes.GetValueOrDefault() ? "d" : "g";
    ((PXDateAttribute) attribute).DisplayMask = str1;
    ((PXDateAttribute) attribute).InputMask = str2;
  }

  public virtual string GetStatusFromFlags(PMTask task)
  {
    if (task == null)
      return "D";
    bool? nullable = task.IsCancelled;
    if (nullable.GetValueOrDefault())
      return "C";
    nullable = task.IsCompleted;
    if (nullable.GetValueOrDefault())
      return "F";
    nullable = task.IsActive;
    return nullable.GetValueOrDefault() ? "A" : "D";
  }

  public virtual void SetFieldStateByStatus(PMTask task, string status)
  {
  }

  public class ProjectTaskEntry_ActivityDetailsExt_Actions : 
    ActivityDetailsExt_Inversed_Actions<ProjectTaskEntry.ProjectTaskEntry_ActivityDetailsExt, ProjectTaskEntry, PMTask>
  {
  }

  public class ProjectTaskEntry_ActivityDetailsExt : 
    ActivityDetailsExt_Inversed<ProjectTaskEntry, PMTask>
  {
    public override System.Type GetLinkConditionClause()
    {
      return typeof (Where<CRPMTimeActivity.projectTaskID, Equal<Current<PMTask.taskID>>>);
    }

    public override System.Type GetBAccountIDCommand()
    {
      return typeof (Select<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PMTask.customerID>>>>);
    }

    public override string GetCustomMailTo()
    {
      PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Base.Project).Select(Array.Empty<object>()));
      if (pmProject == null)
        return (string) null;
      PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) pmProject.CustomerID
      }));
      return !string.IsNullOrWhiteSpace(contact?.EMail) ? PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(contact.EMail, contact.DisplayName) : (string) null;
    }

    public override void CreateTimeActivity(PXGraph targetGraph, int classID, string activityType)
    {
      base.CreateTimeActivity(targetGraph, classID, activityType);
      PXCache cach = targetGraph.Caches[typeof (PMTimeActivity)];
      if (cach == null)
        return;
      PMTimeActivity current = (PMTimeActivity) cach.Current;
      if (current == null)
        return;
      bool flag = classID != 0 && classID != 1;
      current.TrackTime = new bool?(flag);
      current.ProjectID = (int?) ((PMTask) ((PXGraph) this.Base).Caches[typeof (PMTask)].Current)?.ProjectID;
      PMTimeActivity pmTimeActivity = (PMTimeActivity) cach.Update((object) current);
      pmTimeActivity.ProjectTaskID = (int?) ((PMTask) ((PXGraph) this.Base).Caches[typeof (PMTask)].Current)?.TaskID;
      cach.Update((object) pmTimeActivity);
    }

    protected virtual void _(PX.Data.Events.RowSelected<PMTask> e)
    {
      if (e.Row == null)
        return;
      PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
      bool flag = true;
      if (pmProject != null && pmProject.RestrictToEmployeeList.GetValueOrDefault())
        flag = ((PXSelectBase<EPEmployeeContract>) new PXSelectJoin<EPEmployeeContract, InnerJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<EPEmployeeContract.employeeID>>>, Where<EPEmployeeContract.contractID, Equal<Current<PMTask.projectID>>, And<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>>((PXGraph) this.Base)).SelectSingle(Array.Empty<object>()) != null;
      ((PXSelectBase) this.Activities).Cache.AllowInsert = flag;
    }
  }
}

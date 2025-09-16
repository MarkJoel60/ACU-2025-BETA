// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.AllocationMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.EP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public class AllocationMaint : PXGraph<AllocationMaint, PMAllocation>
{
  public PXSelect<PMTran> PMTranMetaData;
  public PXSelect<PMProject> PMProjectMetaData;
  public PXSelect<PMTask> PMTaskMetaData;
  public PXSelect<PMAccountGroup> PMAccountGroupMetaData;
  public PXSelect<PX.Objects.PM.PMBudget> PMBudget;
  public PXSelect<EPEmployee> EmployeesMetaData;
  public PXSelect<PX.Objects.AP.Vendor> VendorMetaData;
  public PXSelect<PX.Objects.AR.Customer> CustomerMetaData;
  public PXSelect<PX.Objects.IN.InventoryItem> InventoryItemMetaData;
  public PXSelect<PMAllocation> Allocations;
  public PXSelect<PMAllocationDetail, Where<PMAllocationDetail.allocationID, Equal<Current<PMAllocation.allocationID>>>> Steps;
  public PXSelect<PMAllocationDetail, Where<PMAllocationDetail.allocationID, Equal<Current<PMAllocationDetail.allocationID>>, And<PMAllocationDetail.stepID, Equal<Current<PMAllocationDetail.stepID>>>>> Step;
  public PXSelect<PMAllocationDetail, Where<PMAllocationDetail.allocationID, Equal<Current<PMAllocationDetail.allocationID>>, And<PMAllocationDetail.stepID, Equal<Current<PMAllocationDetail.stepID>>>>> StepRules;
  public PXSelect<PMAllocationDetail, Where<PMAllocationDetail.allocationID, Equal<Current<PMAllocationDetail.allocationID>>, And<PMAllocationDetail.stepID, Equal<Current<PMAllocationDetail.stepID>>>>> StepSettings;

  [InjectDependency]
  private ICurrentUserInformationProvider _currentUserInformationProvider { get; set; }

  protected virtual void PMAllocationDetail_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row1))
      return;
    bool? nullable;
    int num1;
    if (row1.Method == "B")
    {
      nullable = row1.UpdateGL;
      num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag = num1 != 0;
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.selectOption>(sender, e.Row, row1.Method == "T");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.post>(sender, e.Row, row1.Method == "T");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.dateSource>(sender, e.Row, row1.Method == "T");
    PXCache pxCache1 = sender;
    object row2 = e.Row;
    nullable = row1.UpdateGL;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.accountOrigin>(pxCache1, row2, num2 != 0);
    PXCache pxCache2 = sender;
    object row3 = e.Row;
    nullable = row1.UpdateGL;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.accountID>(pxCache2, row3, num3 != 0);
    PXCache pxCache3 = sender;
    object row4 = e.Row;
    nullable = row1.UpdateGL;
    int num4 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.subMask>(pxCache3, row4, num4 != 0);
    PXCache pxCache4 = sender;
    object row5 = e.Row;
    nullable = row1.UpdateGL;
    int num5 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.subID>(pxCache4, row5, num5 != 0);
    PXCache pxCache5 = sender;
    object row6 = e.Row;
    nullable = row1.UpdateGL;
    int num6 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.accountOrigin>(pxCache5, row6, num6 != 0);
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.offsetProjectOrigin>(sender, e.Row, flag);
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.offsetProjectID>(sender, e.Row, flag);
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.offsetTaskOrigin>(sender, e.Row, flag);
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.offsetTaskID>(sender, e.Row, flag);
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.offsetTaskCD>(sender, e.Row, flag);
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.offsetCostCodeOrigin>(sender, e.Row, flag);
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.offsetCostCodeID>(sender, e.Row, flag);
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.projectOrigin>(sender, e.Row, row1.Method == "T");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.taskOrigin>(sender, e.Row, row1.Method == "T");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.costCodeOrigin>(sender, e.Row, row1.Method == "T");
    PXCache pxCache6 = sender;
    object row7 = e.Row;
    nullable = row1.UpdateGL;
    int num7 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.offsetAccountOrigin>(pxCache6, row7, num7 != 0);
    PXCache pxCache7 = sender;
    object row8 = e.Row;
    nullable = row1.UpdateGL;
    int num8 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.offsetAccountID>(pxCache7, row8, num8 != 0);
    PXCache pxCache8 = sender;
    object row9 = e.Row;
    nullable = row1.UpdateGL;
    int num9 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.offsetSubMask>(pxCache8, row9, num9 != 0);
    PXCache pxCache9 = sender;
    object row10 = e.Row;
    nullable = row1.UpdateGL;
    int num10 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.offsetSubID>(pxCache9, row10, num10 != 0);
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.offsetProjectOrigin>(sender, e.Row, row1.Method == "T");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.offsetTaskOrigin>(sender, e.Row, row1.Method == "T");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.offsetCostCodeOrigin>(sender, e.Row, row1.Method == "T");
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.taskID>(sender, e.Row, row1.ProjectID.HasValue);
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.taskCD>(sender, e.Row, !row1.ProjectID.HasValue);
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.sourceBranchID>(sender, e.Row, row1.SelectOption == "T");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.projectID>(sender, e.Row, row1.ProjectOrigin == "C" && row1.AccountGroupOrigin != "N");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.taskID>(sender, e.Row, row1.TaskOrigin == "C" && row1.AccountGroupOrigin != "N");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.taskCD>(sender, e.Row, row1.TaskOrigin == "C" && row1.AccountGroupOrigin != "N");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.costCodeID>(sender, e.Row, row1.CostCodeOrigin == "C" && row1.AccountGroupOrigin != "N");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.accountGroupID>(sender, e.Row, row1.AccountGroupOrigin == "C");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.accountID>(sender, e.Row, row1.AccountOrigin == "C");
    PXUIFieldAttribute.SetVisible<PMAllocationDetail.offsetBranchOrigin>(sender, e.Row, this.ShowBranchOptions());
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.targetBranchID>(sender, e.Row, row1.OffsetBranchOrigin == "C");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.offsetProjectID>(sender, e.Row, row1.OffsetProjectOrigin == "C");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.offsetTaskID>(sender, e.Row, row1.OffsetTaskOrigin == "C");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.offsetTaskCD>(sender, e.Row, row1.OffsetTaskOrigin == "C");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.offsetCostCodeID>(sender, e.Row, row1.OffsetCostCodeOrigin == "C");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.offsetAccountGroupID>(sender, e.Row, row1.OffsetAccountGroupOrigin == "C");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.offsetAccountID>(sender, e.Row, row1.OffsetAccountOrigin == "C");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.rangeStart>(sender, e.Row, row1.SelectOption == "S");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.rangeEnd>(sender, e.Row, row1.SelectOption == "S");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.accountGroupFrom>(sender, e.Row, row1.SelectOption != "S");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.accountGroupTo>(sender, e.Row, row1.SelectOption != "S");
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.rateTypeID>(sender, e.Row, row1.Method == "T");
    PXCache pxCache10 = sender;
    object row11 = e.Row;
    nullable = row1.Post;
    int num11 = !nullable.GetValueOrDefault() ? 0 : (row1.Method == "T" ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.billableQtyFormula>(pxCache10, row11, num11 != 0);
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.allocateZeroAmount>(sender, e.Row, row1.Method != "B");
    PXCache pxCache11 = sender;
    object row12 = e.Row;
    nullable = row1.Post;
    int num12 = !nullable.GetValueOrDefault() ? 0 : (row1.Method == "T" ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.qtyFormula>(pxCache11, row12, num12 != 0);
    PXCache pxCache12 = sender;
    object row13 = e.Row;
    nullable = row1.Post;
    int num13 = !nullable.GetValueOrDefault() ? 0 : (row1.Method == "T" ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.amountFormula>(pxCache12, row13, num13 != 0);
    PXCache pxCache13 = sender;
    object row14 = e.Row;
    nullable = row1.Post;
    int num14 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.descriptionFormula>(pxCache13, row14, num14 != 0);
    PXCache pxCache14 = sender;
    object row15 = e.Row;
    nullable = row1.Post;
    int num15 = !nullable.GetValueOrDefault() ? 0 : (row1.Method == "T" ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.groupByDate>(pxCache14, row15, num15 != 0);
    PXCache pxCache15 = sender;
    object row16 = e.Row;
    nullable = row1.Post;
    int num16 = !nullable.GetValueOrDefault() ? 0 : (row1.Method == "T" ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.groupByEmployee>(pxCache15, row16, num16 != 0);
    PXCache pxCache16 = sender;
    object row17 = e.Row;
    nullable = row1.Post;
    int num17 = !nullable.GetValueOrDefault() ? 0 : (row1.Method == "T" ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.groupByItem>(pxCache16, row17, num17 != 0);
    PXCache pxCache17 = sender;
    object row18 = e.Row;
    nullable = row1.Post;
    int num18 = !nullable.GetValueOrDefault() ? 0 : (row1.Method == "T" ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<PMAllocationDetail.groupByVendor>(pxCache17, row18, num18 != 0);
    this.ValidateWarnings(row1);
  }

  public virtual bool ShowBranchOptions()
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.branch>() && !PXAccess.FeatureInstalled<FeaturesSet.multiCompany>())
      return false;
    IEnumerable<BranchInfo> activeBranches = this._currentUserInformationProvider.GetActiveBranches();
    return activeBranches != null && activeBranches.Count<BranchInfo>() > 1;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMAllocationDetail, PMAllocationDetail.offsetBranchOrigin> e)
  {
    if (e.Row != null && e.Row.OffsetBranchOrigin != null && e.Row.OffsetBranchOrigin == "S")
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMAllocationDetail, PMAllocationDetail.offsetBranchOrigin>>) e).Cache.SetDefaultExt<PMAllocationDetail.targetBranchID>((object) e.Row);
    if (!(e.Row.OffsetBranchOrigin != "C"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMAllocationDetail, PMAllocationDetail.offsetBranchOrigin>>) e).Cache.SetValuePending<PMAllocationDetail.targetBranchID>((object) e.Row, PXCache.NotSetValue);
  }

  protected virtual void PMAllocationDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row) || !(e.OldRow is PMAllocationDetail oldRow))
      return;
    if (row.SelectOption == "S" && oldRow.SelectOption != "S")
    {
      row.AccountGroupFrom = new int?();
      row.AccountGroupTo = new int?();
    }
    else if (row.SelectOption != "S" && oldRow.SelectOption == "S")
    {
      row.RangeStart = new int?();
      row.RangeEnd = new int?();
    }
    this.ValidateWarnings(row);
  }

  protected virtual void PMAllocationDetail_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row) || e.Operation == 3)
      return;
    this.Validate(row);
  }

  protected virtual void PMAllocationDetail_UpdateGL_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row))
      return;
    if (row.UpdateGL.GetValueOrDefault())
    {
      sender.SetValueExt<PMAllocationDetail.accountOrigin>((object) row, (object) "C");
      sender.SetValueExt<PMAllocationDetail.offsetAccountOrigin>((object) row, (object) "C");
      row.Reverse = "I";
    }
    else
    {
      sender.SetValueExt<PMAllocationDetail.accountGroupOrigin>((object) row, (object) "S");
      sender.SetValueExt<PMAllocationDetail.offsetAccountGroupOrigin>((object) row, (object) "S");
      row.Reverse = "B";
    }
  }

  protected virtual void PMAllocationDetail_AccountOrigin_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row))
      return;
    if (row.AccountOrigin == "C")
    {
      row.AccountGroupOrigin = "F";
    }
    else
    {
      row.AccountID = new int?();
      sender.SetValuePending<PMAllocationDetail.accountID>(e.Row, PXCache.NotSetValue);
    }
  }

  protected virtual void PMAllocationDetail_OffsetAccountOrigin_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row))
      return;
    if (row.OffsetAccountOrigin == "C")
    {
      row.OffsetAccountGroupOrigin = "F";
    }
    else
    {
      row.OffsetAccountID = new int?();
      sender.SetValuePending<PMAllocationDetail.offsetAccountID>(e.Row, PXCache.NotSetValue);
    }
  }

  protected virtual void PMAllocationDetail_AccountGroupOrigin_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row))
      return;
    if (row.AccountGroupOrigin != "C")
    {
      row.AccountGroupID = new int?();
      sender.SetValuePending<PMAllocationDetail.accountGroupOrigin>(e.Row, PXCache.NotSetValue);
    }
    if (!(row.AccountGroupOrigin == "N"))
      return;
    row.ProjectID = new int?();
    row.TaskID = new int?();
    row.CostCodeID = new int?();
  }

  protected virtual void PMAllocationDetail_TaskOrigin_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row))
      return;
    if (row.TaskCD != null)
      row.TaskCD = (string) null;
    int? nullable1 = row.TaskID;
    if (nullable1.HasValue)
    {
      PMAllocationDetail allocationDetail = row;
      nullable1 = new int?();
      int? nullable2 = nullable1;
      allocationDetail.TaskID = nullable2;
    }
    if (!(row.TaskOrigin == "S"))
      return;
    sender.SetValuePending<PMAllocationDetail.taskID>(e.Row, PXCache.NotSetValue);
  }

  protected virtual void PMAllocationDetail_OffsetTaskOrigin_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row))
      return;
    if (row.OffsetTaskCD != null)
      row.OffsetTaskCD = (string) null;
    int? nullable1 = row.OffsetTaskID;
    if (nullable1.HasValue)
    {
      PMAllocationDetail allocationDetail = row;
      nullable1 = new int?();
      int? nullable2 = nullable1;
      allocationDetail.OffsetTaskID = nullable2;
    }
    if (!(row.OffsetTaskOrigin == "S"))
      return;
    sender.SetValuePending<PMAllocationDetail.offsetTaskID>(e.Row, PXCache.NotSetValue);
  }

  protected virtual void PMAllocationDetail_CostCodeOrigin_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row))
      return;
    int? nullable1 = row.CostCodeID;
    if (nullable1.HasValue)
    {
      PMAllocationDetail allocationDetail = row;
      nullable1 = new int?();
      int? nullable2 = nullable1;
      allocationDetail.CostCodeID = nullable2;
    }
    if (!(row.CostCodeOrigin == "S"))
      return;
    sender.SetValuePending<PMAllocationDetail.costCodeID>(e.Row, PXCache.NotSetValue);
  }

  protected virtual void PMAllocationDetail_OffsetCostCodeOrigin_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row))
      return;
    int? nullable1 = row.OffsetCostCodeID;
    if (nullable1.HasValue)
    {
      PMAllocationDetail allocationDetail = row;
      nullable1 = new int?();
      int? nullable2 = nullable1;
      allocationDetail.OffsetCostCodeID = nullable2;
    }
    if (!(row.OffsetCostCodeOrigin == "S"))
      return;
    sender.SetValuePending<PMAllocationDetail.offsetCostCodeID>(e.Row, PXCache.NotSetValue);
  }

  protected virtual void PMAllocationDetail_ProjectID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row) || row.TaskCD == null)
      return;
    row.TaskCD = (string) null;
  }

  protected virtual void PMAllocationDetail_OffsetProjectID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row) || row.OffsetTaskCD == null)
      return;
    row.OffsetTaskCD = (string) null;
  }

  protected virtual void PMAllocationDetail_ProjectOrigin_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row) || !(row.ProjectOrigin == "S"))
      return;
    row.ProjectID = new int?();
    sender.SetValuePending<PMAllocationDetail.projectID>(e.Row, PXCache.NotSetValue);
  }

  protected virtual void PMAllocationDetail_OffsetProjectOrigin_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row) || !(row.OffsetProjectOrigin == "S"))
      return;
    row.OffsetProjectID = new int?();
  }

  protected virtual void PMAllocationDetail_OffsetAccountGroupOrigin_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row) || !(row.OffsetAccountGroupOrigin != "C"))
      return;
    row.OffsetAccountGroupID = new int?();
  }

  protected virtual void PMAllocationDetail_Method_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row))
      return;
    if (row.Method == "B")
    {
      row.RateTypeID = (string) null;
      row.QtyFormula = (string) null;
      row.BillableQtyFormula = (string) null;
      row.AmountFormula = (string) null;
      row.SelectOption = "T";
      row.AllocateZeroAmount = new bool?(false);
      row.Post = new bool?(true);
      row.DateSource = "A";
    }
    else
    {
      bool? updateGl = row.UpdateGL;
      bool flag = false;
      if (!(updateGl.GetValueOrDefault() == flag & updateGl.HasValue) || !(row.AccountGroupOrigin == "N"))
        return;
      sender.SetDefaultExt<PMAllocationDetail.accountGroupOrigin>(e.Row);
    }
  }

  protected virtual void PMAllocationDetail_SourceBranchID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail))
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PMAllocationDetail_TargetBranchID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail))
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PMAllocationDetail_AccountGroupOrigin_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row))
      return;
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    stringList1.Add("S");
    stringList1.Add("C");
    stringList2.Add(Messages.GetLocal("Use Source"));
    stringList2.Add(Messages.GetLocal("Replace"));
    if (row.UpdateGL.GetValueOrDefault())
    {
      stringList1.Add("F");
      stringList2.Add(Messages.GetLocal("From Account"));
    }
    else if (row.Method == "B")
    {
      stringList1.Add("N");
      stringList2.Add(Messages.GetLocal("None"));
    }
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(1), new bool?(false), typeof (PMAllocationDetail.accountGroupOrigin).Name, new bool?(false), new int?(1), (string) null, stringList1.ToArray(), stringList2.ToArray(), new bool?(true), "S", (string[]) null);
    ((PXFieldState) e.ReturnState).Enabled = !row.UpdateGL.GetValueOrDefault();
  }

  protected virtual void PMAllocationDetail_OffsetAccountGroupOrigin_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row))
      return;
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    stringList1.Add("S");
    stringList1.Add("C");
    stringList2.Add(Messages.GetLocal("Use Source"));
    stringList2.Add(Messages.GetLocal("Replace"));
    if (row.UpdateGL.GetValueOrDefault())
    {
      stringList1.Add("F");
      stringList2.Add(Messages.GetLocal("From Account"));
    }
    else
    {
      stringList1.Add("N");
      stringList2.Add(Messages.GetLocal("None"));
    }
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(1), new bool?(false), typeof (PMAllocationDetail.offsetAccountGroupOrigin).Name, new bool?(false), new int?(1), (string) null, stringList1.ToArray(), stringList2.ToArray(), new bool?(true), "S", (string[]) null);
    ((PXFieldState) e.ReturnState).Enabled = !row.UpdateGL.GetValueOrDefault();
  }

  protected virtual void PMAllocationDetail_Post_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row))
      return;
    sender.SetValueExt<PMAllocationDetail.updateGL>(e.Row, (object) row.Post);
  }

  protected virtual void PMAllocationDetail_RangeStart_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row))
      return;
    int? nullable1 = row.RangeStart;
    int? nullable2 = row.StepID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      sender.RaiseExceptionHandling<PMAllocationDetail.rangeStart>(e.Row, e.NewValue, (Exception) new PXSetPropertyException("Range for the summary step should not refer to itself."));
    nullable2 = row.RangeStart;
    nullable1 = row.StepID;
    if (!(nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue))
      return;
    sender.RaiseExceptionHandling<PMAllocationDetail.rangeStart>(e.Row, e.NewValue, (Exception) new PXSetPropertyException("Range for the summary step should not refer future steps."));
  }

  protected virtual void PMAllocationDetail_RangeEnd_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is PMAllocationDetail row))
      return;
    int? nullable1 = row.RangeEnd;
    int? nullable2 = row.StepID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      sender.RaiseExceptionHandling<PMAllocationDetail.rangeEnd>(e.Row, e.NewValue, (Exception) new PXSetPropertyException("Range for the summary step should not refer to itself."));
    nullable2 = row.RangeEnd;
    nullable1 = row.StepID;
    if (!(nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue))
      return;
    sender.RaiseExceptionHandling<PMAllocationDetail.rangeEnd>(e.Row, e.NewValue, (Exception) new PXSetPropertyException("Range for the summary step should not refer future steps."));
  }

  protected virtual void Validate(PMAllocationDetail step)
  {
    if (!this.ValidateErrors(step))
      return;
    this.ValidateWarnings(step);
  }

  /// <summary>
  /// Validate conditions for the given step that raise warnings.
  /// </summary>
  /// <param name="step">Allocation rule</param>
  protected virtual void ValidateWarnings(PMAllocationDetail step)
  {
    if (step.UpdateGL.GetValueOrDefault())
    {
      if (!step.AccountID.HasValue || !step.OffsetAccountID.HasValue)
        return;
      int? accountId = step.AccountID;
      int? offsetAccountId = step.OffsetAccountID;
      if (!(accountId.GetValueOrDefault() == offsetAccountId.GetValueOrDefault() & accountId.HasValue == offsetAccountId.HasValue))
        return;
      ((PXSelectBase) this.Step).Cache.RaiseExceptionHandling<PMAllocationDetail.accountID>((object) step, (object) null, (Exception) new PXSetPropertyException("Debit Account matches Credit Account.", (PXErrorLevel) 3));
    }
    else
    {
      if (!step.AccountGroupID.HasValue || !step.OffsetAccountGroupID.HasValue)
        return;
      int? accountGroupId = step.AccountGroupID;
      int? offsetAccountGroupId = step.OffsetAccountGroupID;
      if (!(accountGroupId.GetValueOrDefault() == offsetAccountGroupId.GetValueOrDefault() & accountGroupId.HasValue == offsetAccountGroupId.HasValue))
        return;
      ((PXSelectBase) this.Step).Cache.RaiseExceptionHandling<PMAllocationDetail.accountID>((object) step, (object) null, (Exception) new PXSetPropertyException("Debit Account Group matches Credit Account Group.", (PXErrorLevel) 3));
    }
  }

  /// <summary>
  /// Validate conditions for the given step that raise errors.
  /// </summary>
  /// <param name="step">Allocation rule</param>
  /// <returns>True if valid</returns>
  protected virtual bool ValidateErrors(PMAllocationDetail step)
  {
    bool flag = true;
    int? nullable1;
    if (step.SelectOption == "S")
    {
      nullable1 = step.RangeStart;
      if (!nullable1.HasValue)
      {
        flag = false;
        ((PXSelectBase) this.Step).Cache.RaiseExceptionHandling<PMAllocationDetail.rangeStart>((object) step, (object) null, (Exception) new PXException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[rangeStart]"
        }));
      }
      nullable1 = step.RangeEnd;
      if (!nullable1.HasValue)
      {
        flag = false;
        ((PXSelectBase) this.Step).Cache.RaiseExceptionHandling<PMAllocationDetail.rangeEnd>((object) step, (object) null, (Exception) new PXException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[rangeEnd]"
        }));
      }
      nullable1 = step.RangeStart;
      int? stepId = step.StepID;
      if (nullable1.GetValueOrDefault() == stepId.GetValueOrDefault() & nullable1.HasValue == stepId.HasValue)
      {
        flag = false;
        ((PXSelectBase) this.Step).Cache.RaiseExceptionHandling<PMAllocationDetail.rangeStart>((object) step, (object) step.RangeStart, (Exception) new PXSetPropertyException("Range for the summary step should not refer to itself."));
      }
      int? nullable2 = step.RangeStart;
      nullable1 = step.StepID;
      if (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
      {
        flag = false;
        ((PXSelectBase) this.Step).Cache.RaiseExceptionHandling<PMAllocationDetail.rangeStart>((object) step, (object) step.RangeStart, (Exception) new PXSetPropertyException("Range for the summary step should not refer future steps."));
      }
      nullable1 = step.RangeEnd;
      nullable2 = step.StepID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        flag = false;
        ((PXSelectBase) this.Step).Cache.RaiseExceptionHandling<PMAllocationDetail.rangeEnd>((object) step, (object) step.RangeEnd, (Exception) new PXSetPropertyException("Range for the summary step should not refer to itself."));
      }
      nullable2 = step.RangeEnd;
      nullable1 = step.StepID;
      if (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
      {
        flag = false;
        ((PXSelectBase) this.Step).Cache.RaiseExceptionHandling<PMAllocationDetail.rangeEnd>((object) step, (object) step.RangeEnd, (Exception) new PXSetPropertyException("Range for the summary step should not refer future steps."));
      }
    }
    else
    {
      nullable1 = step.AccountGroupFrom;
      if (!nullable1.HasValue)
      {
        flag = false;
        ((PXSelectBase) this.Step).Cache.RaiseExceptionHandling<PMAllocationDetail.accountGroupFrom>((object) step, (object) null, (Exception) new PXException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[accountGroupFrom]"
        }));
      }
    }
    if (step.AccountGroupOrigin == "C")
    {
      nullable1 = step.AccountGroupID;
      if (!nullable1.HasValue)
      {
        flag = false;
        ((PXSelectBase) this.Step).Cache.RaiseExceptionHandling<PMAllocationDetail.accountGroupID>((object) step, (object) step.AccountGroupID, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[accountGroupID]"
        }));
      }
    }
    if (step.OffsetBranchOrigin == "C")
    {
      nullable1 = step.TargetBranchID;
      if (!nullable1.HasValue)
      {
        flag = false;
        ((PXSelectBase) this.Step).Cache.RaiseExceptionHandling<PMAllocationDetail.targetBranchID>((object) step, (object) step.TargetBranchID, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[targetBranchID]"
        }));
      }
    }
    if (step.OffsetAccountGroupOrigin == "C")
    {
      nullable1 = step.OffsetAccountGroupID;
      if (!nullable1.HasValue)
      {
        flag = false;
        ((PXSelectBase) this.Step).Cache.RaiseExceptionHandling<PMAllocationDetail.offsetAccountGroupID>((object) step, (object) step.OffsetAccountGroupID, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[offsetAccountGroupID]"
        }));
      }
    }
    if (step.CostCodeOrigin == "C")
    {
      nullable1 = step.CostCodeID;
      if (!nullable1.HasValue)
      {
        flag = false;
        ((PXSelectBase) this.Step).Cache.RaiseExceptionHandling<PMAllocationDetail.costCodeID>((object) step, (object) step.CostCodeID, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[costCodeID]"
        }));
      }
    }
    if (step.OffsetCostCodeOrigin == "C")
    {
      nullable1 = step.OffsetCostCodeID;
      if (!nullable1.HasValue)
      {
        flag = false;
        ((PXSelectBase) this.Step).Cache.RaiseExceptionHandling<PMAllocationDetail.offsetCostCodeID>((object) step, (object) step.OffsetCostCodeID, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[offsetCostCodeID]"
        }));
      }
    }
    if (!step.UpdateGL.GetValueOrDefault())
    {
      if (step.Method == "T")
      {
        if (step.AccountGroupOrigin == "C")
        {
          nullable1 = step.AccountGroupID;
          if (!nullable1.HasValue)
          {
            flag = false;
            ((PXSelectBase) this.Step).Cache.RaiseExceptionHandling<PMAllocationDetail.accountGroupID>((object) step, (object) step.AccountGroupID, (Exception) new PXException("Allocation Rule Step {0} is not defined correctly. Debit Account Group is required.", new object[1]
            {
              (object) step.StepID
            }));
          }
        }
      }
      else if (step.AccountGroupOrigin == "N" && step.OffsetAccountGroupOrigin == "N")
      {
        flag = false;
        ((PXSelectBase) this.Step).Cache.RaiseExceptionHandling<PMAllocationDetail.accountGroupID>((object) step, (object) step.AccountGroupID, (Exception) new PXException("Allocation Rule Step {0} is not defined correctly. At least either Debit or Credit Account Group is required.", new object[1]
        {
          (object) step.StepID
        }));
      }
    }
    return flag;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PO.GraphExtensions.PoOrderEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CN.CacheExtensions;
using PX.Objects.CN.ProjectAccounting.AP.CacheExtensions;
using PX.Objects.Common.Extensions;
using PX.Objects.Common.Scopes;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.PM;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CN.ProjectAccounting.PO.GraphExtensions;

public class PoOrderEntryExt : PXGraphExtension<
#nullable disable
POOrderEntry>
{
  [PXCopyPasteHiddenView]
  public PXFilter<PoOrderEntryExt.CostBudgetFilter> ProjectItemFilter;
  [PXCopyPasteHiddenView]
  public PXSelect<PMCostBudget> AvailableProjectItems;
  public PXAction<PX.Objects.PO.POOrder> addProjectItem;
  public PXAction<PX.Objects.PO.POOrder> appendSelectedProjectItems;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PMTask.type, NotEqual<ProjectTaskType.revenue>>), "Task Type is not valid", new Type[] {typeof (PMTask.type)})]
  [PXFormula(typeof (Validate<PX.Objects.PO.POLine.projectID, PX.Objects.PO.POLine.costCodeID, PX.Objects.PO.POLine.inventoryID, PX.Objects.PO.POLine.siteID>))]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PX.Objects.PO.POLine.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.type, NotEqual<ProjectTaskType.revenue>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.taskID> e)
  {
  }

  public virtual void Initialize()
  {
    ((PXGraph) this.Base).OnBeforePersist += new Action<PXGraph>(this.OnBeforeGraphPersist);
  }

  public virtual IEnumerable availableProjectItems()
  {
    PoOrderEntryExt poOrderEntryExt = this;
    HashSet<BudgetKeyTuple> existing = new HashSet<BudgetKeyTuple>();
    foreach (PXResult<PX.Objects.PO.POLine> pxResult in ((PXSelectBase<PX.Objects.PO.POLine>) poOrderEntryExt.Base.Transactions).Select(Array.Empty<object>()))
    {
      PX.Objects.PO.POLine line = PXResult<PX.Objects.PO.POLine>.op_Implicit(pxResult);
      existing.Add(poOrderEntryExt.GetBudgetKeyFromLine(line));
    }
    PXSelectJoin<PMCostBudget, InnerJoin<PMTask, On<PMCostBudget.projectTaskID, Equal<PMTask.taskID>, And<PMTask.isCompleted, NotEqual<True>, And<PMTask.isCancelled, NotEqual<True>, And<PMTask.visibleInPO, Equal<True>>>>>, InnerJoin<PMCostCode, On<PMCostCode.costCodeID, Equal<PMCostBudget.costCodeID>>>>, Where<PMCostBudget.projectID, Equal<Current<PoOrderEntryExt.CostBudgetFilter.projectID>>, And<PMCostBudget.type, Equal<AccountType.expense>, And<PMCostCode.isActive, Equal<True>, And<Current<PoOrderEntryExt.CostBudgetFilter.projectID>, IsNotNull, And<Current<PoOrderEntryExt.CostBudgetFilter.projectID>, NotEqual<Required<PMCostBudget.projectID>>>>>>>> pxSelectJoin = new PXSelectJoin<PMCostBudget, InnerJoin<PMTask, On<PMCostBudget.projectTaskID, Equal<PMTask.taskID>, And<PMTask.isCompleted, NotEqual<True>, And<PMTask.isCancelled, NotEqual<True>, And<PMTask.visibleInPO, Equal<True>>>>>, InnerJoin<PMCostCode, On<PMCostCode.costCodeID, Equal<PMCostBudget.costCodeID>>>>, Where<PMCostBudget.projectID, Equal<Current<PoOrderEntryExt.CostBudgetFilter.projectID>>, And<PMCostBudget.type, Equal<AccountType.expense>, And<PMCostCode.isActive, Equal<True>, And<Current<PoOrderEntryExt.CostBudgetFilter.projectID>, IsNotNull, And<Current<PoOrderEntryExt.CostBudgetFilter.projectID>, NotEqual<Required<PMCostBudget.projectID>>>>>>>>((PXGraph) poOrderEntryExt.Base);
    object[] objArray = new object[1]
    {
      (object) ProjectDefaultAttribute.NonProject()
    };
    foreach (PXResult<PMCostBudget, PMTask, PMCostCode> pxResult in ((PXSelectBase<PMCostBudget>) pxSelectJoin).Select(objArray))
    {
      PMCostBudget pmCostBudget = PXResult<PMCostBudget, PMTask, PMCostCode>.op_Implicit(pxResult);
      if (existing.Contains(poOrderEntryExt.GetBudgetKeyFromCostBudget(pmCostBudget)))
        pmCostBudget.Selected = new bool?(true);
      yield return (object) pmCostBudget;
    }
  }

  [PXUIField(DisplayName = "Add Project Items")]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false)]
  public IEnumerable AddProjectItem(PXAdapter adapter)
  {
    if (((PXSelectBase) this.AvailableProjectItems).View.Answer != 1)
      this.ClearSelectionForProjectItems();
    if (((PXSelectBase) this.AvailableProjectItems).View.AskExt() == 1)
      this.AddSelectedProjectItems();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Add Lines")]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false)]
  public IEnumerable AppendSelectedProjectItems(PXAdapter adapter)
  {
    this.AddSelectedProjectItems();
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POOrder> e)
  {
    if (e.Row == null)
      return;
    ((PXAction) this.addProjectItem).SetVisible(ProjectAttribute.IsPMVisible("PO"));
    ((PXAction) this.addProjectItem).SetEnabled(e.Row.VendorLocationID.HasValue && e.Row.Hold.GetValueOrDefault());
    PXUIFieldAttribute.SetVisible<PoOrderEntryExt.CostBudgetFilter.projectID>(((PXSelectBase) this.ProjectItemFilter).Cache, (object) null, e.Row?.OrderType != "PD");
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.PO.POOrder, PX.Objects.PO.POOrder.projectID> e)
  {
    if (!(e.Row?.OrderType == "PD"))
      return;
    ((PXSelectBase) this.ProjectItemFilter).Cache.SetDefaultExt<PoOrderEntryExt.CostBudgetFilter.projectID>((object) ((PXSelectBase<PoOrderEntryExt.CostBudgetFilter>) this.ProjectItemFilter).Current);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POLine> e)
  {
    if (e.Row == null || PXUIFieldAttribute.GetErrorOnly<PX.Objects.PO.POLine.inventoryID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POLine>>) e).Cache, (object) e.Row) != null)
      return;
    this.ValidateInventoryItemAndSetWarning(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.PO.POLine, PX.Objects.PO.POLine.inventoryID> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current;
    if ((current != null ? (current.VendorID.HasValue ? 1 : 0) : 0) == 0 || FlaggedModeScopeBase<ChangeOrderReleaseScope>.IsActive)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.PO.POLine, PX.Objects.PO.POLine.inventoryID>, PX.Objects.PO.POLine, object>) e).NewValue = (object) this.GetVendorDefaultInventoryId();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PoOrderEntryExt.CostBudgetFilter, PoOrderEntryExt.CostBudgetFilter.projectID> e)
  {
    if (!(((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current?.OrderType == "PD") || !ProjectDefaultAttribute.IsProject((PXGraph) this.Base, (int?) ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current?.ProjectID))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PoOrderEntryExt.CostBudgetFilter, PoOrderEntryExt.CostBudgetFilter.projectID>, PoOrderEntryExt.CostBudgetFilter, object>) e).NewValue = (object) ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.ProjectID;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PoOrderEntryExt.CostBudgetFilter, PoOrderEntryExt.CostBudgetFilter.projectID> e)
  {
    this.ClearSelectionForProjectItems();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMCostBudget, PMBudget.selected> e)
  {
    if (((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current == null || !(((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.OrderType == "RS"))
      return;
    this.RaiseErrorIfReceiptIsRequired(e.Row.InventoryID);
  }

  protected virtual void AddSelectedProjectItems()
  {
    this.AddNewLinesSkippingExisting(this.GetExistingLines());
    foreach (PMCostBudget pmCostBudget in ((PXSelectBase) this.AvailableProjectItems).Cache.Updated)
      ((PXSelectBase) this.AvailableProjectItems).Cache.SetStatus((object) pmCostBudget, (PXEntryStatus) 0);
  }

  protected virtual void AddLine(PMCostBudget item)
  {
    ((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Insert(new PX.Objects.PO.POLine()
    {
      InventoryID = this.NormalizeInventoryID(item.InventoryID),
      ProjectID = item.ProjectID,
      TaskID = item.ProjectTaskID,
      CostCodeID = item.CostCodeID
    });
    if (!new ProjectSettingsManager().CalculateProjectSpecificTaxes || ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current == null)
      return;
    ((PXSelectBase) this.Base.Document).Cache.SetDefaultExt<PX.Objects.PO.POOrder.taxZoneID>((object) ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current);
  }

  private int? NormalizeInventoryID(int? inventoryID)
  {
    if (!inventoryID.HasValue)
      return new int?();
    int? nullable = inventoryID;
    int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
    if (!(nullable.GetValueOrDefault() == emptyInventoryId & nullable.HasValue))
      return inventoryID;
    nullable = new int?();
    return nullable;
  }

  private void ClearSelectionForProjectItems()
  {
    foreach (PMBudget pmBudget in ((PXSelectBase) this.AvailableProjectItems).Cache.Updated)
      pmBudget.Selected = new bool?(false);
  }

  private HashSet<BudgetKeyTuple> GetExistingLines()
  {
    HashSet<BudgetKeyTuple> existingLines = new HashSet<BudgetKeyTuple>();
    foreach (PXResult<PX.Objects.PO.POLine> pxResult in ((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Select(Array.Empty<object>()))
    {
      PX.Objects.PO.POLine line = PXResult<PX.Objects.PO.POLine>.op_Implicit(pxResult);
      if (line.TaskID.HasValue)
        existingLines.Add(this.GetBudgetKeyFromLine(line));
    }
    return existingLines;
  }

  private BudgetKeyTuple GetBudgetKeyFromLine(PX.Objects.PO.POLine line)
  {
    int? nullable = line.ProjectID;
    int valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = line.TaskID;
    int valueOrDefault2 = nullable.GetValueOrDefault();
    nullable = line.InventoryID;
    int inventoryID = nullable ?? PMInventorySelectorAttribute.EmptyInventoryID;
    nullable = line.CostCodeID;
    int valueOrDefault3 = nullable.GetValueOrDefault();
    return new BudgetKeyTuple(valueOrDefault1, valueOrDefault2, 0, inventoryID, valueOrDefault3);
  }

  private BudgetKeyTuple GetBudgetKeyFromCostBudget(PMCostBudget item)
  {
    int? nullable = item.ProjectID;
    int valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = item.TaskID;
    int valueOrDefault2 = nullable.GetValueOrDefault();
    nullable = item.InventoryID;
    int valueOrDefault3 = nullable.GetValueOrDefault();
    nullable = item.CostCodeID;
    int valueOrDefault4 = nullable.GetValueOrDefault();
    return new BudgetKeyTuple(valueOrDefault1, valueOrDefault2, 0, valueOrDefault3, valueOrDefault4);
  }

  private void AddNewLinesSkippingExisting(HashSet<BudgetKeyTuple> existing)
  {
    foreach (PMCostBudget pmCostBudget in ((PXSelectBase) this.AvailableProjectItems).Cache.Updated)
    {
      if (pmCostBudget.Selected.GetValueOrDefault() && !existing.Contains(this.GetBudgetKeyFromCostBudget(pmCostBudget)))
        this.AddLine(pmCostBudget);
    }
  }

  private int? GetVendorDefaultInventoryId()
  {
    return PXCacheEx.GetExtension<VendorExt>((IBqlTable) this.GetVendor()).VendorDefaultInventoryId;
  }

  private PX.Objects.AP.Vendor GetVendor()
  {
    return PXResultset<PX.Objects.AP.Vendor>.op_Implicit(((PXSelectBase<PX.Objects.AP.Vendor>) new PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>((PXGraph) this.Base)).Select(new object[1]
    {
      (object) ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.VendorID
    }));
  }

  private void ValidateInventoryItemAndSetWarning(PX.Objects.PO.POLine line)
  {
    if (!this.IsInventoryItemValid(line))
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, line.InventoryID);
      ((PXSelectBase) this.Base.Transactions).Cache.RaiseException<PX.Objects.PO.POLine.inventoryID>((object) line, "The cost budget of the specified project does not have the corresponding budget line.", (object) inventoryItem.InventoryCD, (PXErrorLevel) 3);
    }
    else
      ((PXSelectBase) this.Base.Transactions).Cache.ClearFieldErrors<PX.Objects.PO.POLine.inventoryID>((object) line);
  }

  private bool IsInventoryItemValid(PX.Objects.PO.POLine line)
  {
    if (!line.InventoryID.HasValue || !this.ShouldValidateInventoryItem() || !line.ProjectID.HasValue || ProjectDefaultAttribute.IsNonProject(line.ProjectID))
      return true;
    PMProject project = PMProject.PK.Find((PXGraph) this.Base, line.ProjectID);
    return this.IsNonProjectAccountGroupsAllowed(project) || !this.IsItemLevel(project) || this.IsInventoryItemUsedInProject(line, line.InventoryID);
  }

  private bool IsItemLevel(PMProject project)
  {
    return project.CostBudgetLevel == "I" || project.CostBudgetLevel == "D";
  }

  private bool IsInventoryItemUsedInProject(PX.Objects.PO.POLine line, int? inventoryID)
  {
    if (!line.ProjectID.HasValue || ProjectDefaultAttribute.IsNonProject(line.ProjectID) || !this.IsItemLevel(PMProject.PK.Find((PXGraph) this.Base, line.ProjectID)))
      return false;
    PXSelect<PMCostBudget, Where<PMCostBudget.projectID, Equal<Required<PMCostBudget.projectID>>>> pxSelect = new PXSelect<PMCostBudget, Where<PMCostBudget.projectID, Equal<Required<PMCostBudget.projectID>>>>((PXGraph) this.Base);
    List<object> objectList = new List<object>();
    objectList.Add((object) line.ProjectID);
    int? nullable1 = line.TaskID;
    if (nullable1.HasValue)
    {
      ((PXSelectBase<PMCostBudget>) pxSelect).WhereAnd<Where<PMCostBudget.projectTaskID, Equal<Required<PMCostBudget.projectTaskID>>>>();
      objectList.Add((object) line.TaskID);
    }
    nullable1 = line.CostCodeID;
    if (nullable1.HasValue)
    {
      ((PXSelectBase<PMCostBudget>) pxSelect).WhereAnd<Where<PMCostBudget.costCodeID, Equal<Required<PMCostBudget.costCodeID>>>>();
      objectList.Add((object) line.CostCodeID);
    }
    return ((IEnumerable<PXResult<PMCostBudget>>) ((PXSelectBase<PMCostBudget>) pxSelect).Select(objectList.ToArray())).AsEnumerable<PXResult<PMCostBudget>>().Any<PXResult<PMCostBudget>>((Func<PXResult<PMCostBudget>, bool>) (b =>
    {
      int? inventoryId = PXResult<PMCostBudget>.op_Implicit(b).InventoryID;
      int? nullable2 = inventoryID;
      return inventoryId.GetValueOrDefault() == nullable2.GetValueOrDefault() & inventoryId.HasValue == nullable2.HasValue;
    }));
  }

  private bool ShouldValidateInventoryItem()
  {
    PX.Objects.PO.POOrder original = (PX.Objects.PO.POOrder) ((PXSelectBase) this.Base.Document).Cache.GetOriginal((object) ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current);
    if (original == null)
      return true;
    if (!(original.Status != "B") && !(((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.Status != "H"))
      return false;
    return original.Status == "H" || original.Status == "B";
  }

  private bool IsNonProjectAccountGroupsAllowed(PMProject project)
  {
    return PXCache<Contract>.GetExtension<ContractExt>((Contract) project).AllowNonProjectAccountGroups.GetValueOrDefault();
  }

  private void RaiseErrorIfReceiptIsRequired(int? inventoryID)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, inventoryID);
    if (inventoryItem != null && (inventoryItem.StkItem.GetValueOrDefault() || inventoryItem.NonStockReceipt.GetValueOrDefault()))
      throw new PXSetPropertyException<PMCostBudget.inventoryID>("You cannot add inventory items for which the system requires receipt to subcontracts. Select a non-stock item that is configured so that the system does not require receipt for it.", (PXErrorLevel) 5);
  }

  public virtual void OnBeforeGraphPersist(PXGraph obj)
  {
    if (((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current == null)
      return;
    this.ValidateBudgetExistance(((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current);
  }

  private void ValidateBudgetExistance(PX.Objects.PO.POOrder order)
  {
    if (order == null || order.Hold.GetValueOrDefault() || !((bool?) ((PXSelectBase) this.Base.Document).Cache.GetValueOriginal<PX.Objects.PO.POOrder.hold>((object) order)).GetValueOrDefault())
      return;
    bool flag = false;
    foreach (PXResult<PX.Objects.PO.POLine> pxResult in ((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Select(Array.Empty<object>()))
    {
      PX.Objects.PO.POLine poLine = PXResult<PX.Objects.PO.POLine>.op_Implicit(pxResult);
      if (!this.IsInventoryItemValid(poLine))
      {
        flag = true;
        ((PXSelectBase) this.Base.Transactions).Cache.RaiseException<PX.Objects.PO.POLine.inventoryID>((object) poLine, "The cost budget of the specified project does not have the corresponding budget line.", errorLevel: (PXErrorLevel) 5);
      }
    }
    if (flag)
      throw new PXException("At least one line is not present in the cost budget of the project. Either add the corresponding budget lines to the project or select the Allow Adding New Items on the Fly check box on the Summary tab of the Projects (PM301000) form.");
  }

  [PXHidden]
  [Serializable]
  public class CostBudgetFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXRestrictor(typeof (Where<Contract.isCancelled, Equal<False>>), "The {0} project or contract is canceled.", new Type[] {typeof (PMProject.contractCD)})]
    [PXRestrictor(typeof (Where<PMProject.visibleInPO, Equal<True>>), "The '{0}' project is invisible in the module.", new Type[] {typeof (PMProject.contractCD)})]
    [ProjectBase]
    public virtual int? ProjectID { get; set; }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PoOrderEntryExt.CostBudgetFilter.projectID>
    {
    }
  }
}

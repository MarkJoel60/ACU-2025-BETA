// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPApprovalAndAssignmentMapBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Common.Extensions;
using PX.Data;
using PX.Data.Automation;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.SM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.EP;

public class EPApprovalAndAssignmentMapBase<TGraph> : PXGraph<TGraph> where TGraph : PXGraph
{
  private (System.Type EntityType, System.Type GraphType) _RelatedInfo;
  public PXSave<EPAssignmentMap> Save;
  public PXCancel<EPAssignmentMap> Cancel;
  public PXInsert<EPAssignmentMap> Insert;
  public PXCopyPasteAction<EPAssignmentMap> CopyPaste;
  public PXDelete<EPAssignmentMap> Delete;
  public PXAction<EPAssignmentMap> up;
  public PXAction<EPAssignmentMap> down;
  public PXAction<EPAssignmentMap> conditionUp;
  public PXAction<EPAssignmentMap> conditionDown;
  public PXAction<EPAssignmentMap> conditionInsert;
  [PXHidden]
  public PXSelect<PX.Objects.AP.Vendor> VendorsDummy;
  public PXSelect<EPAssignmentMap> AssigmentMap;
  public PXSelect<EPRuleTree, Where<EPRule.assignmentMapID, Equal<Current<EPAssignmentMap.assignmentMapID>>>, OrderBy<Asc<EPRule.sequence>>> NodesTree;
  public PXSelect<EPRule, Where<EPRule.assignmentMapID, Equal<Current<EPAssignmentMap.assignmentMapID>>>, OrderBy<Asc<EPRule.sequence>>> Nodes;
  public PXSelect<EPRule, Where<EPRule.ruleID, Equal<Current<EPRule.ruleID>>>> CurrentNode;
  public PXSelect<EPRuleCondition, Where<EPRuleCondition.ruleID, Equal<Current<EPRule.ruleID>>>, OrderBy<Asc<EPRuleCondition.rowNbr>>> Rules;
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> accountDummy;
  public PXSelect<PX.Objects.CR.Standalone.EPEmployee, Where<PX.Objects.CR.Standalone.EPEmployee.defContactID, Equal<Required<PX.Objects.CR.Contact.contactID>>, And<PX.Objects.CR.Standalone.EPEmployee.vStatus, Equal<VendorStatus.inactive>>>> EmployeeInactive;
  public PXSelect<CacheEntityItem, Where<CacheEntityItem.path, Equal<CacheEntityItem.path>>, OrderBy<Asc<CacheEntityItem.number>>> EntityItems;
  public PXAction<EPAssignmentMap> addRule;
  public PXAction<EPAssignmentMap> deleteRoute;
  private const string _FIELDNAME_STR = "FieldName";

  public (System.Type EntityType, System.Type GraphType) RelatedInfo
  {
    get
    {
      if (this._RelatedInfo.EntityType != (System.Type) null && this._RelatedInfo.GraphType != (System.Type) null)
        return (this._RelatedInfo.EntityType, this._RelatedInfo.GraphType);
      EPAssignmentMap current = ((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current;
      string entityType = current?.EntityType;
      string graphType = current?.GraphType;
      if (entityType == null || graphType == null)
      {
        this._RelatedInfo.GraphType = (System.Type) null;
      }
      else
      {
        this._RelatedInfo.EntityType = GraphHelper.GetType(entityType);
        if (this._RelatedInfo.EntityType == (System.Type) null)
          return ((System.Type) null, (System.Type) null);
        if (!string.IsNullOrEmpty(graphType))
        {
          System.Type type = GraphHelper.GetType(graphType);
          if ((object) type != null)
          {
            this._RelatedInfo.GraphType = type;
            goto label_10;
          }
        }
        this._RelatedInfo.GraphType = EntityHelper.GetPrimaryGraphType((PXGraph) this, this._RelatedInfo.EntityType);
      }
label_10:
      return (this._RelatedInfo.EntityType, this._RelatedInfo.GraphType);
    }
  }

  [InjectDependency]
  protected IWorkflowService WorkflowService { get; set; }

  protected virtual IEnumerable nodesTree([PXDBGuid(false)] Guid? ruleID)
  {
    PXResultset<EPRule> pxResultset = ((PXSelectBase<EPRule>) this.Nodes).Select(new object[1]
    {
      (object) ruleID
    });
    List<EPRuleTree> epRuleTreeList = new List<EPRuleTree>();
    foreach (PXResult<EPRule> pxResult in pxResultset)
    {
      EPRule epRule = PXResult<EPRule>.op_Implicit(pxResult);
      EPRuleTree epRuleTree1 = new EPRuleTree();
      epRuleTree1.StepName = epRule.StepName;
      epRuleTree1.IsActive = epRule.IsActive;
      epRuleTree1.Name = string.IsNullOrEmpty(epRule.Name) ? "<No Name>" : epRule.Name;
      epRuleTree1.RuleID = epRule.RuleID;
      epRuleTree1.StepID = epRule.StepID;
      epRuleTree1.ApproveType = epRule.ApproveType;
      epRuleTree1.ReasonForApprove = epRule.ReasonForApprove;
      epRuleTree1.ReasonForReject = epRule.ReasonForReject;
      epRuleTree1.AssignmentMapID = epRule.AssignmentMapID;
      epRuleTree1.Icon = epRule.Icon;
      epRuleTree1.Sequence = epRule.Sequence;
      epRuleTree1.OwnerID = epRule.OwnerID;
      epRuleTree1.OwnerSource = epRule.OwnerSource;
      epRuleTree1.AllowReassignment = epRule.AllowReassignment;
      epRuleTree1.RuleType = epRule.RuleType;
      epRuleTree1.WaitTime = epRule.WaitTime;
      epRuleTree1.WorkgroupID = epRule.WorkgroupID;
      epRuleTree1.EmptyStepType = epRule.EmptyStepType;
      epRuleTree1.ExecuteStep = epRule.ExecuteStep;
      epRuleTree1.CreatedByID = epRule.CreatedByID;
      epRuleTree1.CreatedByScreenID = epRule.CreatedByScreenID;
      epRuleTree1.CreatedDateTime = epRule.CreatedDateTime;
      epRuleTree1.LastModifiedByID = epRule.LastModifiedByID;
      epRuleTree1.LastModifiedByScreenID = epRule.LastModifiedByScreenID;
      epRuleTree1.LastModifiedDateTime = epRule.LastModifiedDateTime;
      epRuleTree1.tstamp = epRule.tstamp;
      EPRuleTree epRuleTree2 = epRuleTree1;
      epRuleTreeList.Add(epRuleTree2);
    }
    return (IEnumerable) epRuleTreeList;
  }

  [PXMergeAttributes]
  [PXStringList]
  [PXUIField(DisplayName = "Entity Type", Required = true)]
  protected virtual void EPAssignmentMap_GraphType_CacheAttached(PXCache sender)
  {
  }

  [PXSelector(typeof (Search5<EPCompanyTree.workGroupID, InnerJoin<EPCompanyTreeMember, On<EPCompanyTreeMember.workGroupID, Equal<EPCompanyTree.workGroupID>>, InnerJoin<EPEmployee, On<EPCompanyTreeMember.contactID, Equal<EPEmployee.defContactID>>>>, Aggregate<GroupBy<EPCompanyTree.workGroupID, GroupBy<EPCompanyTree.description>>>>), SubstituteKey = typeof (EPCompanyTree.description))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POOrder.ownerWorkgroupID> e)
  {
  }

  protected IEnumerable entityItems(string parent)
  {
    EPApprovalAndAssignmentMapBase<TGraph> assignmentMapBase = this;
    if (((PXSelectBase<EPAssignmentMap>) assignmentMapBase.AssigmentMap).Current != null)
    {
      System.Type type1 = GraphHelper.GetType(((PXSelectBase<EPAssignmentMap>) assignmentMapBase.AssigmentMap).Current.EntityType);
      System.Type type2;
      if (((PXSelectBase<EPAssignmentMap>) assignmentMapBase.AssigmentMap).Current.GraphType == null)
      {
        if (type1 == (System.Type) null && parent != null)
          yield break;
        type2 = EntityHelper.GetPrimaryGraphType((PXGraph) assignmentMapBase, type1);
      }
      else
        type2 = GraphHelper.GetType(((PXSelectBase<EPAssignmentMap>) assignmentMapBase.AssigmentMap).Current.GraphType);
      foreach (CacheEntityItem cacheEntityItem in EMailSourceHelper.TemplateEntity((PXGraph) assignmentMapBase, parent, type1.FullName, type2 != (System.Type) null ? type2.FullName : (string) null))
        yield return (object) cacheEntityItem;
    }
  }

  [PXUIField]
  [PXButton(ImageKey = "AddNew", ImageSet = "main", Tooltip = "Add Rule")]
  public virtual void AddRule()
  {
    EPRule epRule = ((PXSelectBase<EPRule>) this.Nodes).Insert();
    epRule.Name = "Rule";
    ((PXSelectBase<EPRule>) this.Nodes).Update(epRule);
    ((PXSelectBase) this.Nodes).Cache.ActiveRow = (IBqlTable) epRule;
    ((PXSelectBase) this.NodesTree).View.RequestRefresh();
  }

  [PXUIField]
  [PXButton(ImageKey = "RecordDel")]
  public virtual void DeleteRoute()
  {
    if (((PXSelectBase<EPRule>) this.Nodes).Current == null)
      return;
    ((PXSelectBase<EPRule>) this.Nodes).Delete(((PXSelectBase<EPRule>) this.Nodes).Current);
  }

  [PXButton(ImageKey = "ArrowUp", ImageSet = "main", Tooltip = "Move Row Up")]
  [PXUIField]
  public virtual IEnumerable ConditionUp(PXAdapter adapter)
  {
    if (((PXSelectBase<EPRuleCondition>) this.Rules).Current == null)
      return adapter.Get();
    EPRuleCondition epRuleCondition = PXResultset<EPRuleCondition>.op_Implicit(PXSelectBase<EPRuleCondition, PXSelect<EPRuleCondition, Where<EPRuleCondition.ruleID, Equal<Current<EPRule.ruleID>>, And<EPRuleCondition.rowNbr, Less<Current<EPRuleCondition.rowNbr>>>>, OrderBy<Desc<EPRuleCondition.rowNbr>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (epRuleCondition != null)
    {
      EPRuleCondition current = ((PXSelectBase<EPRuleCondition>) this.Rules).Current;
      EPRuleCondition copy1 = (EPRuleCondition) ((PXSelectBase) this.Rules).Cache.CreateCopy((object) epRuleCondition);
      EPRuleCondition copy2 = (EPRuleCondition) ((PXSelectBase) this.Rules).Cache.CreateCopy((object) ((PXSelectBase<EPRuleCondition>) this.Rules).Current);
      this.SwapItems(((PXSelectBase) this.Rules).Cache, (object) epRuleCondition, (object) current);
      this.RestoreConditionValues(epRuleCondition, copy2);
      this.RestoreConditionValues(current, copy1);
      ((PXSelectBase) this.Rules).Cache.ActiveRow = (IBqlTable) epRuleCondition;
      ((PXSelectBase) this.Rules).View.RequestRefresh();
    }
    return adapter.Get();
  }

  [PXButton(ImageKey = "ArrowDown", ImageSet = "main", Tooltip = "Move Row Down")]
  [PXUIField]
  public virtual IEnumerable ConditionDown(PXAdapter adapter)
  {
    if (((PXSelectBase<EPRuleCondition>) this.Rules).Current == null)
      return adapter.Get();
    EPRuleCondition epRuleCondition = PXResultset<EPRuleCondition>.op_Implicit(PXSelectBase<EPRuleCondition, PXSelect<EPRuleCondition, Where<EPRuleCondition.ruleID, Equal<Current<EPRule.ruleID>>, And<EPRuleCondition.rowNbr, Greater<Current<EPRuleCondition.rowNbr>>>>, OrderBy<Asc<EPRuleCondition.rowNbr>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (epRuleCondition != null)
    {
      EPRuleCondition current = ((PXSelectBase<EPRuleCondition>) this.Rules).Current;
      EPRuleCondition copy1 = (EPRuleCondition) ((PXSelectBase) this.Rules).Cache.CreateCopy((object) epRuleCondition);
      EPRuleCondition copy2 = (EPRuleCondition) ((PXSelectBase) this.Rules).Cache.CreateCopy((object) ((PXSelectBase<EPRuleCondition>) this.Rules).Current);
      this.SwapItems(((PXSelectBase) this.Rules).Cache, (object) epRuleCondition, (object) current);
      this.RestoreConditionValues(epRuleCondition, copy2);
      this.RestoreConditionValues(current, copy1);
      ((PXSelectBase) this.Rules).Cache.ActiveRow = (IBqlTable) epRuleCondition;
      ((PXSelectBase) this.Rules).View.RequestRefresh();
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable ConditionInsert(PXAdapter adapter)
  {
    if (((PXSelectBase<EPRuleCondition>) this.Rules).Current == null)
      return adapter.Get();
    PXResultset<EPRuleCondition> pxResultset = PXSelectBase<EPRuleCondition, PXSelect<EPRuleCondition, Where<EPRuleCondition.ruleID, Equal<Current<EPRule.ruleID>>, And<EPRuleCondition.rowNbr, GreaterEqual<Current<EPRuleCondition.rowNbr>>>>, OrderBy<Desc<EPRuleCondition.rowNbr>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    short? rowNbr1 = ((PXSelectBase<EPRuleCondition>) this.Rules).Current.RowNbr;
    EPRuleCondition copy1 = (EPRuleCondition) ((PXSelectBase) this.Rules).Cache.CreateCopy((object) ((PXSelectBase<EPRuleCondition>) this.Rules).Current);
    ((PXSelectBase) this.Rules).Cache.ClearQueryCacheObsolete();
    foreach (PXResult<EPRuleCondition> pxResult in pxResultset)
    {
      EPRuleCondition restoreFrom = PXResult<EPRuleCondition>.op_Implicit(pxResult);
      EPRuleCondition copy2 = (EPRuleCondition) ((PXSelectBase) this.Rules).Cache.CreateCopy((object) restoreFrom);
      EPRuleCondition epRuleCondition = copy2;
      short? rowNbr2 = epRuleCondition.RowNbr;
      epRuleCondition.RowNbr = rowNbr2.HasValue ? new short?((short) ((int) rowNbr2.GetValueOrDefault() + 1)) : new short?();
      ((PXSelectBase<EPRuleCondition>) this.Rules).Update(copy2);
      this.RestoreConditionValues(copy2, restoreFrom);
      ((PXSelectBase<EPRuleCondition>) this.Rules).Delete(restoreFrom);
    }
    copy1.Entity = (string) null;
    copy1.Condition = new int?(0);
    copy1.CloseBrackets = new int?(0);
    copy1.OpenBrackets = new int?(0);
    copy1.Operator = new int?(0);
    ((PXSelectBase<EPRuleCondition>) this.Rules).Update(copy1);
    return adapter.Get();
  }

  public virtual bool CanClipboardCopyPaste() => false;

  protected virtual void EPAssignmentMap_GraphType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EPAssignmentMap row) || row.GraphType == null)
      return;
    PXGraph instance = PXGraph.CreateInstance(GraphHelper.GetType(row.GraphType));
    System.Type itemType = instance.Views[instance.PrimaryView].Cache.GetItemType();
    row.EntityType = itemType.FullName;
  }

  protected virtual void EPRuleCondition_Entity_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current == null)
      return;
    e.ReturnState = (object) this.CreateFieldStateForEntity(e.ReturnValue, this.RelatedInfo.EntityType, this.RelatedInfo.GraphType);
  }

  protected virtual void EPRuleCondition_Entity_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.Rules).Cache.SetValue<EPRuleCondition.fieldName>((object) (e.Row as EPRuleCondition), (object) null);
  }

  protected virtual void EPRuleCondition_FieldName_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is EPRuleCondition row) || row.Entity == null)
      return;
    e.ReturnState = (object) this.CreateFieldStateForFieldName(e.ReturnState, this.RelatedInfo.EntityType, this.RelatedInfo.GraphType, row.Entity, (EPRuleBaseCondition) row);
  }

  protected virtual void EPAssignmentMap_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.CurrentNode).Cache, (object) ((PXSelectBase<EPRule>) this.CurrentNode).Current, (string) null, ((PXSelectBase<EPRule>) this.CurrentNode).Current != null);
    EPAssignmentMap row = e.Row as EPAssignmentMap;
    ((PXAction) this.addRule).SetEnabled(row != null && row.EntityType != null);
    if (row != null)
    {
      if (row.EntityType != null && row.GraphType == null)
      {
        System.Type type = PXBuildManager.GetType(row.EntityType, false);
        System.Type primaryGraphType = type != (System.Type) null ? EntityHelper.GetPrimaryGraphType((PXGraph) this, type) : (System.Type) null;
        if (primaryGraphType != (System.Type) null)
        {
          ((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current.GraphType = primaryGraphType.FullName;
          ((PXSelectBase) this.AssigmentMap).Cache.SetStatus((object) ((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current, (PXEntryStatus) 1);
          ((PXSelectBase) this.AssigmentMap).Cache.IsDirty = true;
        }
      }
      ((PXSelectBase) this.Nodes).Cache.AllowInsert = row.EntityType != null;
      bool flag = sender.GetStatus((object) row) == 2;
      PXUIFieldAttribute.SetEnabled<EPAssignmentMap.graphType>(sender, (object) row, flag);
    }
    ((PXAction) this.up).SetEnabled(((PXSelectBase<EPRule>) this.CurrentNode).Current != null);
    ((PXAction) this.down).SetEnabled(((PXSelectBase<EPRule>) this.CurrentNode).Current != null);
    ((PXAction) this.deleteRoute).SetEnabled(((PXSelectBase<EPRule>) this.CurrentNode).Current != null);
    if (((PXSelectBase<EPRuleTree>) this.NodesTree).Current != null)
      return;
    EPRuleTree epRuleTree = ((PXSelectBase<EPRuleTree>) this.NodesTree).SelectSingle(Array.Empty<object>());
    if (epRuleTree == null)
      return;
    ((PXSelectBase) this.NodesTree).Cache.ActiveRow = (IBqlTable) (PXResultset<EPRuleTree>.op_Implicit(((PXSelectBase<EPRuleTree>) this.NodesTree).Select(new object[1]
    {
      (object) epRuleTree.RuleID
    })) ?? epRuleTree);
    ((PXSelectBase<EPRuleTree>) this.NodesTree).Current = (EPRuleTree) ((PXSelectBase) this.NodesTree).Cache.ActiveRow;
    ((PXSelectBase) this.NodesTree).View.RequestRefresh();
  }

  protected virtual void EPRuleCondition_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    EPRuleCondition oldRow = e.OldRow as EPRuleCondition;
    EPRuleCondition row1 = e.Row as EPRuleCondition;
    if (oldRow == null || row1 == null)
      return;
    if (!string.Equals(row1.Entity, oldRow.Entity, StringComparison.OrdinalIgnoreCase))
      row1.FieldName = row1.Value = row1.Value2 = (string) null;
    if (!string.Equals(row1.FieldName, oldRow.FieldName, StringComparison.OrdinalIgnoreCase))
      row1.Value = row1.Value2 = (string) null;
    EPRuleCondition row2 = e.Row as EPRuleCondition;
    int? condition = row2.Condition;
    if (condition.HasValue)
    {
      condition = row2.Condition;
      if (condition.Value != 11)
      {
        condition = row2.Condition;
        if (condition.Value != 12)
          goto label_9;
      }
    }
    row1.Value = row1.Value2 = (string) null;
label_9:
    if (row1.Value != null)
      return;
    PXFieldState stateExt = sender.GetStateExt<EPRuleCondition.value>((object) row1) as PXFieldState;
    row1.Value = stateExt == null || stateExt.Value == null ? (string) null : stateExt.Value.ToString();
  }

  protected virtual void EPRuleCondition_Value_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is EPRuleCondition row) || string.IsNullOrEmpty(row.FieldName))
      return;
    int? condition = row.Condition;
    if (!condition.HasValue)
      return;
    condition = row.Condition;
    if (condition.Value == 11)
      return;
    condition = row.Condition;
    if (condition.Value == 12 || (sender.GetStateExt<EPRuleCondition.fieldName>((object) row) is PXFieldState stateExt ? (stateExt.ErrorLevel != 4 ? 1 : 0) : 1) == 0)
      return;
    PXFieldState stateForFieldValue = this.CreateFieldStateForFieldValue(e.ReturnState, ((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current.EntityType, row.Entity, row.FieldName);
    if (stateForFieldValue == null)
      return;
    e.ReturnState = (object) stateForFieldValue;
  }

  protected virtual void EPRuleCondition_Value2_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    object returnValue = e.ReturnValue;
    this.EPRuleCondition_Value_FieldSelecting(sender, e);
    e.ReturnValue = returnValue;
  }

  protected virtual void EPRuleCondition_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    EPRuleCondition row = (EPRuleCondition) e.Row;
    if (row == null || ((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current == null || !(GraphHelper.GetType(((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current.EntityType) != (System.Type) null))
      return;
    System.Type type = GraphHelper.GetType(row.Entity);
    if (!(type == (System.Type) null) && (this.GetPXFieldState(type, row.FieldName) == null || this.FieldCannotBeFoundAdditionalCondition(row.FieldName)))
      throw new PXSetPropertyException<EPRuleCondition.fieldName>("The specified field cannot be found.", (PXErrorLevel) 4);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<EPRuleCondition> e)
  {
    EPRuleCondition row = e?.Row;
    if (row == null || row.UiNoteID.HasValue)
      return;
    row.UiNoteID = new Guid?(Guid.NewGuid());
  }

  public virtual void _(PX.Data.Events.FieldDefaulting<EPRuleCondition.uiNoteID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<EPRuleCondition.uiNoteID>, object, object>) e).NewValue = (object) Guid.NewGuid();
  }

  protected virtual void EPRule_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is EPRule row))
      return;
    if (row.OwnerID.HasValue)
    {
      if (((PXSelectBase<PX.Objects.CR.Standalone.EPEmployee>) this.EmployeeInactive).SelectSingle(new object[1]
      {
        (object) row.OwnerID
      }) != null)
        sender.RaiseExceptionHandling<EPRule.ownerID>((object) row, (object) null, (Exception) new PXSetPropertyException(e.Row as IBqlTable, "The employee is not active.", (PXErrorLevel) 2));
      else
        sender.RaiseExceptionHandling<EPRule.ownerID>((object) row, (object) null, (Exception) null);
    }
    PXUIFieldAttribute.SetVisible<EPRule.ownerID>(((PXSelectBase) this.Nodes).Cache, (object) ((PXSelectBase<EPRule>) this.Nodes).Current, row.RuleType == "D");
    PXUIFieldAttribute.SetVisible<EPRule.ownerSource>(((PXSelectBase) this.Nodes).Cache, (object) ((PXSelectBase<EPRule>) this.Nodes).Current, row.RuleType == "E");
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.CurrentNode).Cache, (object) null, (string) null, true);
    ((PXAction) this.up).SetEnabled(true);
    ((PXAction) this.down).SetEnabled(true);
    ((PXAction) this.deleteRoute).SetEnabled(true);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<EPRule, EPRule.ownerSource> e)
  {
    try
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      EPApprovalAndAssignmentMapBase<TGraph>.\u003C\u003Ec__DisplayClass50_0 cDisplayClass500 = new EPApprovalAndAssignmentMapBase<TGraph>.\u003C\u003Ec__DisplayClass50_0();
      string newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<EPRule, EPRule.ownerSource>, EPRule, object>) e).NewValue as string;
      if (e.Row.RuleType != "E" || string.IsNullOrEmpty(newValue))
        return;
      string str1 = new Regex("(?<=^\\(\\()[\\S]{1,}(?=\\)\\)$)").Match(newValue).Value;
      if (string.IsNullOrEmpty(str1))
        throw new Exception("The Employee box is empty.");
      string str2 = str1.Contains(".") ? str1.Substring(0, str1.LastIndexOf(".")) : str1;
      if (string.IsNullOrEmpty(str2))
        throw new Exception("At least one element in the formula is incorrect or missing.");
      // ISSUE: reference to a compiler-generated field
      cDisplayClass500.childKey = str1.Contains(".") ? str1.Substring(str1.LastIndexOf(".") + 1) : string.Empty;
      int length = str2.IndexOf('.');
      if (length > 0)
        str2 = $"{str2.Substring(0, length)}.{str2.Substring(length + 1).Replace('.', '>')}";
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: type reference
      if (!((IQueryable<PXResult<CacheEntityItem>>) ((PXSelectBase<CacheEntityItem>) this.EntityItems).Select((object[]) new string[1]
      {
        str2
      })).Select<PXResult<CacheEntityItem>, CacheEntityItem>((Expression<Func<PXResult<CacheEntityItem>, CacheEntityItem>>) (c => c.get_Item(0) as CacheEntityItem)).Any<CacheEntityItem>(Expression.Lambda<Func<CacheEntityItem, bool>>((Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CacheEntityItem.get_SubKey))), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass500, typeof (EPApprovalAndAssignmentMapBase<TGraph>.\u003C\u003Ec__DisplayClass50_0)), FieldInfo.GetFieldFromHandle(__fieldref (EPApprovalAndAssignmentMapBase<TGraph>.\u003C\u003Ec__DisplayClass50_0.childKey), __typeref (EPApprovalAndAssignmentMapBase<TGraph>.\u003C\u003Ec__DisplayClass50_0)))), parameterExpression)))
        throw new Exception("At least one element in the formula is incorrect or missing.");
    }
    catch (Exception ex)
    {
      string[] strArray = new string[1]
      {
        StringExtensions.Truncate(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<EPRule, EPRule.ownerSource>, EPRule, object>) e).NewValue.ToString(), 20)
      };
      PXTrace.WriteWarning("The following formula in the Employee box is invalid: {0}.", (object[]) strArray);
      throw new PXSetPropertyException(ex, (IBqlTable) e.Row, (PXErrorLevel) 2, "The following formula in the Employee box is invalid: {0}.", (object[]) strArray);
    }
  }

  protected virtual void EPAssignmentMap_EntityType_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is EPAssignmentMap row))
      return;
    e.ReturnState = (object) this.CreateFieldStateForEntityType(e.ReturnValue, row.EntityType, row.GraphType);
  }

  private PXFieldState CreateFieldStateForEntityType(
    object returnState,
    string entityType,
    string graphType)
  {
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    System.Type type1 = (System.Type) null;
    if (graphType != null)
      type1 = GraphHelper.GetType(graphType);
    else if (entityType != null)
    {
      System.Type type2 = PXBuildManager.GetType(entityType, false);
      type1 = type2 == (System.Type) null ? (System.Type) null : EntityHelper.GetPrimaryGraphType((PXGraph) this, type2);
    }
    if (type1 != (System.Type) null)
    {
      PXSiteMapNode siteMapNodeUnsecure = PXSiteMapProviderExtensions.FindSiteMapNodeUnsecure(PXSiteMap.Provider, type1);
      if (siteMapNodeUnsecure != null)
      {
        stringList1.Add(entityType);
        stringList2.Add(siteMapNodeUnsecure.Title);
      }
    }
    return PXStringState.CreateInstance(returnState, new int?(60), new bool?(), "Entity", new bool?(false), new int?(1), (string) null, stringList1.ToArray(), stringList2.ToArray(), new bool?(true), (string) null, (string[]) null);
  }

  private PXFieldState GetPXFieldState(System.Type cachetype, string fieldName)
  {
    PXCache cach = ((PXGraph) this).Caches[cachetype];
    PXDBAttributeAttribute.Activate(cach);
    return cach.GetStateExt((object) null, fieldName) as PXFieldState;
  }

  private PXFieldState CreateFieldStateForEntity(
    object returnState,
    System.Type entityType,
    System.Type graphType)
  {
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    if (entityType != (System.Type) null)
    {
      if (graphType == (System.Type) null)
      {
        PXCacheNameAttribute[] customAttributes = (PXCacheNameAttribute[]) entityType.GetCustomAttributes(typeof (PXCacheNameAttribute), true);
        if (entityType.IsSubclassOf(typeof (CSAnswers)))
        {
          stringList1.Add(entityType.FullName);
          stringList2.Add(customAttributes.Length != 0 ? ((PXNameAttribute) customAttributes[0]).Name : entityType.Name);
        }
      }
      else
      {
        foreach (CacheEntityItem cacheEntityItem in (IEnumerable<CacheEntityItem>) EMailSourceHelper.TemplateEntity((PXGraph) this, (string) null, entityType.FullName, graphType.FullName).Cast<CacheEntityItem>().OrderBy<CacheEntityItem, string>((Func<CacheEntityItem, string>) (e => e.Name)))
        {
          if (cacheEntityItem.SubKey != typeof (CSAnswers).FullName)
          {
            stringList1.Add(cacheEntityItem.SubKey);
            stringList2.Add(cacheEntityItem.Name);
          }
        }
      }
    }
    return PXStringState.CreateInstance(returnState, new int?(60), new bool?(), "Entity", new bool?(false), new int?(1), (string) null, stringList1.ToArray(), stringList2.ToArray(), new bool?(true), (string) null, (string[]) null);
  }

  protected PXFieldState CreateFieldStateForFieldName(
    object returnState,
    System.Type entityType,
    System.Type graphType,
    string cacheName,
    EPRuleBaseCondition condition = null)
  {
    if (!(entityType != (System.Type) null))
      return PXStringState.CreateInstance(returnState, new int?(128 /*0x80*/), new bool?(), "FieldName", new bool?(false), new int?(1), (string) null, new List<string>().ToArray(), new List<string>().ToArray(), new bool?(false), (string) null, (string[]) null);
    string viewName = (string) null;
    if (graphType != (System.Type) null)
    {
      foreach (CacheEntityItem cacheEntityItem in EMailSourceHelper.TemplateEntity((PXGraph) this, (string) null, entityType.FullName, graphType.FullName))
      {
        if (cacheEntityItem.SubKey == cacheName)
        {
          viewName = cacheEntityItem.Key;
          break;
        }
      }
    }
    return this.CreateFieldStateForFieldName(returnState, entityType, graphType, viewName, cacheName, new int?(128 /*0x80*/), condition);
  }

  protected PXFieldState CreateFieldStateForFieldName(
    object returnState,
    System.Type entityType,
    System.Type gType,
    string viewName,
    string cacheName,
    int? fieldLength,
    EPRuleBaseCondition condition)
  {
    List<string> allowedValues = new List<string>();
    List<string> allowedLabels = new List<string>();
    List<string> uniqueLabels = new List<string>();
    if (entityType != (System.Type) null)
    {
      System.Type type = GraphHelper.GetType(cacheName);
      if (type == (System.Type) null)
        return (PXFieldState) null;
      if (condition != null)
      {
        string fieldName = condition.FieldName;
        if (!string.IsNullOrEmpty(fieldName) && (this.GetPXFieldState(type, fieldName) == null || this.FieldCannotBeFoundAdditionalCondition(fieldName)))
        {
          ((PXSelectBase) this.Rules).Cache.SetStatus((object) condition, (PXEntryStatus) 1);
          PXUIFieldAttribute.SetError<EPRuleCondition.fieldName>(((PXSelectBase) this.Rules).Cache, (object) condition, "The specified field cannot be found.", condition.FieldName);
        }
      }
      Dictionary<string, string> source = new Dictionary<string, string>();
      foreach (CacheEntityItem cacheEntityItem in EMailSourceHelper.TemplateEntity((PXGraph) this, viewName, entityType.FullName, gType != (System.Type) null ? gType.FullName : (string) null, false))
        source[cacheEntityItem.SubKey] = cacheEntityItem.Name;
      Action<string, string> action = (Action<string, string>) ((value, label) =>
      {
        allowedValues.Add(value);
        allowedLabels.Add(label);
        uniqueLabels.Add(label);
      });
      foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) source.OrderBy<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (i => i.Value)))
      {
        if (this.CheckFieldValid(keyValuePair))
        {
          IEnumerable<PXEventSubscriberAttribute> attributes = ((PXGraph) this).Caches[type].GetAttributes((object) null, keyValuePair.Key);
          string str1 = keyValuePair.Value;
          if (attributes != null)
          {
            PXTimeListAttribute timeListAttribute = attributes.FirstOrDefault<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is PXTimeListAttribute)) as PXTimeListAttribute;
            PXIntAttribute pxIntAttribute = attributes.FirstOrDefault<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is PXIntAttribute)) as PXIntAttribute;
            if (timeListAttribute != null && pxIntAttribute != null)
              str1 = $"{str1} ({PXLocalizer.Localize("Minutes")})";
          }
          if (!uniqueLabels.Contains(str1))
          {
            action(keyValuePair.Key, str1);
          }
          else
          {
            string str2 = $"{keyValuePair.Key} - {str1}";
            action(keyValuePair.Key, str2);
          }
        }
      }
    }
    return PXStringState.CreateInstance(returnState, fieldLength, new bool?(), "FieldName", new bool?(false), new int?(1), (string) null, allowedValues.ToArray(), allowedLabels.ToArray(), new bool?(false), (string) null, (string[]) null);
  }

  protected PXFieldState CreateFieldStateForFieldValue(
    object returnState,
    string entityType,
    string cacheName,
    string fieldName)
  {
    if (!(GraphHelper.GetType(entityType) != (System.Type) null))
      return (PXFieldState) null;
    System.Type type = GraphHelper.GetType(cacheName);
    if (type == (System.Type) null)
      return (PXFieldState) null;
    PXCache cach = ((PXGraph) this).Caches[type];
    PXFieldState pxFieldState = this.GetPXFieldState(type, fieldName) ?? PXFieldState.CreateInstance(returnState, (System.Type) null, new bool?(), new bool?(), new int?(), new int?(), new int?(10), returnState, fieldName, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(true), new bool?(true), new bool?(), (PXUIVisibility) 3, (string) null, (string[]) null, (string[]) null);
    IEnumerable<PXEventSubscriberAttribute> attributes = cach.GetAttributes((object) null, fieldName);
    if (attributes != null)
    {
      PXTimeListAttribute timeListAttribute = attributes.FirstOrDefault<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is PXTimeListAttribute)) as PXTimeListAttribute;
      PXIntAttribute pxIntAttribute = attributes.FirstOrDefault<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is PXIntAttribute)) as PXIntAttribute;
      if (timeListAttribute != null && pxIntAttribute != null)
      {
        pxFieldState = PXTimeState.CreateInstance((PXIntState) pxFieldState, (int[]) null, (string[]) null);
        pxFieldState.SelectorMode = (PXSelectorMode) 0;
      }
      if (attributes.Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is StateAttribute)))
      {
        string key = "StatesNoFilterByCountry";
        if (!((Dictionary<string, PXView>) cach.Graph.Views).ContainsKey(key))
        {
          FbqlSelect<SelectFromBase<PX.Objects.CS.State, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CS.Country>.On<BqlOperand<PX.Objects.CS.State.countryID, IBqlString>.IsEqual<PX.Objects.CS.Country.countryID>>>>, PX.Objects.CS.State>.View view = new FbqlSelect<SelectFromBase<PX.Objects.CS.State, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CS.Country>.On<BqlOperand<PX.Objects.CS.State.countryID, IBqlString>.IsEqual<PX.Objects.CS.Country.countryID>>>>, PX.Objects.CS.State>.View(cach.Graph);
          cach.Graph.Views.Add(key, ((PXSelectBase) view).View);
        }
        pxFieldState.ViewName = key;
        pxFieldState.FieldList = new string[4]
        {
          "stateID",
          "name",
          $"Country__countryID",
          $"Country__description"
        };
        pxFieldState.HeaderList = new string[4]
        {
          PXUIFieldAttribute.GetDisplayName((PXCache) GraphHelper.Caches<PX.Objects.CS.State>(cach.Graph), "stateID"),
          PXUIFieldAttribute.GetDisplayName((PXCache) GraphHelper.Caches<PX.Objects.CS.State>(cach.Graph), "name"),
          PXUIFieldAttribute.GetDisplayName((PXCache) GraphHelper.Caches<PX.Objects.CS.Country>(cach.Graph), "countryID"),
          PXUIFieldAttribute.GetDisplayName((PXCache) GraphHelper.Caches<PX.Objects.CS.Country>(cach.Graph), "description")
        };
      }
      if (attributes.Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is BranchBaseAttribute)))
      {
        string key = "BranchNoRestrictor";
        if (!((Dictionary<string, PXView>) cach.Graph.Views).ContainsKey(key))
        {
          FbqlSelect<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.DAC.Organization>.On<BqlOperand<PX.Objects.GL.Branch.organizationID, IBqlInt>.IsEqual<PX.Objects.GL.DAC.Organization.organizationID>>>>.Where<MatchWithBranch<PX.Objects.GL.Branch.branchID>>, PX.Objects.GL.Branch>.View view = new FbqlSelect<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.DAC.Organization>.On<BqlOperand<PX.Objects.GL.Branch.organizationID, IBqlInt>.IsEqual<PX.Objects.GL.DAC.Organization.organizationID>>>>.Where<MatchWithBranch<PX.Objects.GL.Branch.branchID>>, PX.Objects.GL.Branch>.View(cach.Graph);
          cach.Graph.Views.Add(key, ((PXSelectBase) view).View);
        }
        pxFieldState.ViewName = key;
      }
    }
    if (pxFieldState != null)
    {
      if (returnState == null)
      {
        object instance = cach.CreateInstance();
        object obj;
        cach.RaiseFieldDefaulting(fieldName, instance, ref obj);
        if (obj != null)
          cach.RaiseFieldSelecting(fieldName, instance, ref obj, false);
        else if (pxFieldState.DataType.Name == "Boolean")
          obj = (object) false;
        pxFieldState.Value = obj;
      }
      else
        pxFieldState.Value = returnState;
      pxFieldState.Enabled = true;
    }
    return attributes != null && attributes.FirstOrDefault<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a.GetType().IsSubclassOf(typeof (PXIntListAttribute)))) is PXIntListAttribute ? pxFieldState : ((pxFieldState is PXStringState pxStringState1 ? pxStringState1.AllowedValues : (string[]) null) != null ? PXStringState.CreateInstance(((PXFieldState) (pxFieldState as PXStringState)).Value, new int?(pxFieldState.Length), new bool?(false), pxFieldState.Name, new bool?(pxFieldState.PrimaryKey), new int?(pxFieldState.Required.GetValueOrDefault() ? 1 : (!pxFieldState.Required.HasValue ? 0 : -1)), pxFieldState is PXStringState pxStringState2 ? pxStringState2.InputMask : (string) null, pxFieldState is PXStringState pxStringState3 ? pxStringState3.AllowedValues : (string[]) null, pxFieldState is PXStringState pxStringState4 ? pxStringState4.AllowedLabels : (string[]) null, new bool?(true), pxFieldState is PXStringState pxStringState5 ? (string) ((PXFieldState) pxStringState5).DefaultValue : (string) (object) null, (string[]) null) : pxFieldState.CreateInstance(pxFieldState.DataType, new bool?(pxFieldState.PrimaryKey), new bool?(pxFieldState.Nullable), new int?(pxFieldState.Required.GetValueOrDefault() ? 1 : (!pxFieldState.Required.HasValue ? 0 : -1)), new int?(pxFieldState.Precision), new int?(pxFieldState.Length), pxFieldState.DefaultValue, fieldName, pxFieldState.DescriptionName, pxFieldState.DisplayName, pxFieldState.Error, pxFieldState.ErrorLevel, new bool?(true), new bool?(true), new bool?(false), (PXUIVisibility) 3, pxFieldState.ViewName, pxFieldState.FieldList, pxFieldState.HeaderList));
  }

  private static int Comparison(EPRule x, EPRule y) => x.Sequence.Value.CompareTo(y.Sequence.Value);

  private IList<EPRule> GetSortedItems()
  {
    List<EPRule> sortedItems = new List<EPRule>();
    foreach (EPRule epRule in ((PXSelectBase) this.Nodes).Cache.Cached)
    {
      if (((PXSelectBase) this.Nodes).Cache.GetStatus((object) epRule) != 3 && ((PXSelectBase) this.Nodes).Cache.GetStatus((object) epRule) != 4)
      {
        if (((PXSelectBase<EPRule>) this.Nodes).Current != null)
        {
          Guid? stepId1 = ((PXSelectBase<EPRule>) this.Nodes).Current.StepID;
          Guid? stepId2 = epRule.StepID;
          if ((stepId1.HasValue == stepId2.HasValue ? (stepId1.HasValue ? (stepId1.GetValueOrDefault() == stepId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
            continue;
        }
        sortedItems.Add(epRule);
      }
    }
    sortedItems.Sort(new System.Comparison<EPRule>(EPApprovalAndAssignmentMapBase<TGraph>.Comparison));
    return (IList<EPRule>) sortedItems;
  }

  protected virtual PXGraph CreateGraph(string graphName, string screenID)
  {
    System.Type type1 = PXBuildManager.GetType(graphName, false);
    if (type1 == (System.Type) null)
      type1 = System.Type.GetType(graphName);
    if (!(type1 != (System.Type) null))
      return (PXGraph) null;
    System.Type type2 = PXBuildManager.GetType(CustomizedTypeManager.GetCustomizedTypeFullName(type1), false);
    if ((object) type2 == null)
      type2 = type1;
    System.Type type3 = type2;
    using (new PXPreserveScope())
    {
      try
      {
        return type3 == typeof (PXGenericInqGrph) ? (PXGraph) (object) PXGenericInqGrph.CreateInstance(screenID) : PXGraph.CreateInstance(type3);
      }
      catch (TargetInvocationException ex)
      {
        throw PXException.ExtractInner((Exception) ex);
      }
    }
  }

  protected List<PXSiteMapNode> GetGraphTypes(EMailSourceHelper.MakeSiteMapCondition condition)
  {
    List<PXSiteMapNode> graphTypes = new List<PXSiteMapNode>();
    foreach (EntityItemSource entityItemSource in EMailSourceHelper.TemplateScreensByCondition((PXGraph) this, (string) null, (string) null, condition))
    {
      if (!string.IsNullOrEmpty(entityItemSource.ScreenID))
      {
        PXSiteMapNode siteMapNodeFromKey = PXSiteMapProviderExtensions.FindSiteMapNodeFromKey(PXSiteMap.Provider, entityItemSource.Key);
        if (siteMapNodeFromKey != null && !string.IsNullOrEmpty(siteMapNodeFromKey.GraphType))
        {
          entityItemSource.SubKey = siteMapNodeFromKey.GraphType;
          graphTypes.Add(siteMapNodeFromKey);
        }
      }
    }
    return graphTypes;
  }

  private void SwapItems(PXCache cache, object first, object second)
  {
    object copy = cache.CreateCopy(first);
    foreach (System.Type bqlField in cache.BqlFields)
    {
      if (!cache.BqlKeys.Contains(bqlField))
        cache.SetValue(first, bqlField.Name, cache.GetValue(second, bqlField.Name));
    }
    foreach (System.Type bqlField in cache.BqlFields)
    {
      if (!cache.BqlKeys.Contains(bqlField))
        cache.SetValue(second, bqlField.Name, cache.GetValue(copy, bqlField.Name));
    }
    cache.Update(first);
    cache.Update(second);
  }

  private void RestoreConditionValues(EPRuleCondition row, EPRuleCondition restoreFrom)
  {
    ((PXSelectBase) this.Rules).Cache.SetValue<EPRuleCondition.fieldName>((object) row, (object) restoreFrom.FieldName);
    ((PXSelectBase) this.Rules).Cache.Update((object) row);
    ((PXSelectBase) this.Rules).Cache.SetValueExt<EPRuleCondition.value>((object) row, (object) restoreFrom.Value);
    ((PXSelectBase) this.Rules).Cache.SetValue<EPRuleCondition.value2>((object) row, (object) restoreFrom.Value2);
    ((PXSelectBase) this.Rules).Cache.Update((object) row);
  }

  protected IList<EPRule> UpdateSequence()
  {
    IList<EPRule> sortedItems = this.GetSortedItems();
    EPRule epRule = ((PXSelectBase<EPRule>) this.Nodes).Current;
    for (int index = 0; index < sortedItems.Count; ++index)
    {
      int? sequence = sortedItems[index].Sequence;
      int num = index + 1;
      if (!(sequence.GetValueOrDefault() == num & sequence.HasValue))
      {
        sortedItems[index].Sequence = new int?(index + 1);
        Guid? ruleId1 = sortedItems[index].RuleID;
        Guid? ruleId2 = epRule.RuleID;
        if ((ruleId1.HasValue == ruleId2.HasValue ? (ruleId1.HasValue ? (ruleId1.GetValueOrDefault() == ruleId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          epRule = ((PXSelectBase<EPRule>) this.Nodes).Update(sortedItems[index]);
        else
          ((PXSelectBase<EPRule>) this.Nodes).Update(sortedItems[index]);
      }
    }
    ((PXSelectBase<EPRule>) this.Nodes).Current = epRule;
    return sortedItems;
  }

  public virtual bool CheckFieldValid(KeyValuePair<string, string> item)
  {
    return item.Key.EndsWith("_Attributes") || !item.Key.Contains<char>('_');
  }

  protected virtual bool FieldCannotBeFoundAdditionalCondition(string fieldName) => false;
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPApprovalMapMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.EP.DAC;
using PX.SM;
using PX.TM;
using PX.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.EP;

public class EPApprovalMapMaint : EPApprovalAndAssignmentMapBase<
#nullable disable
EPApprovalMapMaint>, ICaptionable
{
  public PXSelect<EPRuleEmployeeCondition, Where<EPRuleEmployeeCondition.ruleID, Equal<Current<EPRule.ruleID>>>> EmployeeCondition;
  public FbqlSelect<SelectFromBase<EPRuleApprover, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CR.Contact>.On<BqlOperand<
  #nullable enable
  PX.Objects.CR.Contact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  EPRuleApprover.ownerID>>>, FbqlJoins.Inner<CREmployee>.On<BqlOperand<
  #nullable enable
  CREmployee.defContactID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.CR.Contact.contactID>>>>.Where<BqlOperand<
  #nullable enable
  EPRuleApprover.ruleID, IBqlGuid>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  EPRule.ruleID, IBqlGuid>.FromCurrent>>, 
  #nullable disable
  EPRuleApprover>.View RuleApprovers;
  public PXAction<EPAssignmentMap> addStep;

  public EPApprovalMapMaint()
  {
    ((PXSelectBase) this.AssigmentMap).View = new PXView((PXGraph) this, false, BqlCommand.CreateInstance(new System.Type[1]
    {
      BqlCommand.Compose(new System.Type[6]
      {
        typeof (Select<,>),
        typeof (EPAssignmentMap),
        typeof (Where<,>),
        typeof (EPAssignmentMap.mapType),
        typeof (Equal<>),
        typeof (EPMapType.approval)
      })
    }));
  }

  public string Caption()
  {
    EPAssignmentMap currentItem = ((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current;
    if (currentItem == null || currentItem.Name == null || currentItem.GraphType == null)
      return "";
    PXSiteMapNode pxSiteMapNode = this.Definitions.SiteMapNodes.FirstOrDefault<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (x => x.GraphType == currentItem.GraphType));
    return pxSiteMapNode != null ? $"{currentItem.Name} - {pxSiteMapNode.Title}" : "";
  }

  protected virtual IEnumerable nodes([PXDBGuid(false)] Guid? ruleID)
  {
    List<EPRule> epRuleList = new List<EPRule>();
    PXResultset<EPRule> pxResultset;
    if (!ruleID.HasValue)
    {
      pxResultset = PXSelectBase<EPRule, PXSelect<EPRule, Where<EPRule.assignmentMapID, Equal<Current<EPAssignmentMap.assignmentMapID>>, And<EPRule.stepID, IsNull>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    }
    else
    {
      EPRule epRule = PXResultset<EPRule>.op_Implicit(PXSelectBase<EPRule, PXSelect<EPRule, Where<EPRule.assignmentMapID, Equal<Current<EPAssignmentMap.assignmentMapID>>, And<EPRule.ruleID, Equal<Required<EPRule.ruleID>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ruleID
      }));
      if (epRule == null || epRule.StepID.HasValue)
        return (IEnumerable) epRuleList;
      pxResultset = PXSelectBase<EPRule, PXSelect<EPRule, Where<EPRule.assignmentMapID, Equal<Current<EPAssignmentMap.assignmentMapID>>, And<EPRule.stepID, Equal<Required<EPRule.stepID>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) epRule.RuleID
      });
    }
    foreach (PXResult<EPRule> pxResult in pxResultset)
    {
      EPRule epRule = PXResult<EPRule>.op_Implicit(pxResult);
      Guid? stepId = epRule.StepID;
      epRule.Icon = !stepId.HasValue ? Sprite.Main.GetFullUrl("Folder") : Sprite.Tree.GetFullUrl("Leaf");
      epRuleList.Add(epRule);
    }
    return (IEnumerable) epRuleList;
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowUp")]
  public virtual IEnumerable Up(PXAdapter adapter)
  {
    IList<EPRule> epRuleList = this.UpdateSequence();
    int index = ((PXSelectBase<EPRule>) this.Nodes).Current.Sequence.Value - 1;
    if (index > 0)
    {
      EPRule epRule1 = epRuleList[index];
      int? sequence = epRule1.Sequence;
      epRule1.Sequence = sequence.HasValue ? new int?(sequence.GetValueOrDefault() - 1) : new int?();
      EPRule epRule2 = epRuleList[index - 1];
      sequence = epRule2.Sequence;
      epRule2.Sequence = sequence.HasValue ? new int?(sequence.GetValueOrDefault() + 1) : new int?();
      ((PXSelectBase<EPRule>) this.Nodes).Update(epRuleList[index]);
      ((PXSelectBase<EPRule>) this.Nodes).Update(epRuleList[index - 1]);
      ((PXSelectBase) this.Nodes).Cache.ActiveRow = (IBqlTable) epRuleList[index];
      ((PXSelectBase) this.NodesTree).View.RequestRefresh();
    }
    else if (((PXSelectBase<EPRule>) this.Nodes).Current.StepID.HasValue)
    {
      EPRule previousStep = this.GetPreviousStep(((PXSelectBase<EPRule>) this.Nodes).Current.StepID);
      if (previousStep != null)
      {
        ((PXSelectBase<EPRule>) this.Nodes).Current.StepID = previousStep.RuleID;
        ((PXSelectBase<EPRule>) this.Nodes).Current.Sequence = new int?(int.MaxValue);
        ((PXSelectBase<EPRule>) this.Nodes).Update(((PXSelectBase<EPRule>) this.Nodes).Current);
        this.UpdateSequence();
        ((PXSelectBase) this.Nodes).Cache.ClearQueryCacheObsolete();
        ((PXSelectBase) this.NodesTree).View.RequestRefresh();
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowDown")]
  public virtual IEnumerable Down(PXAdapter adapter)
  {
    IList<EPRule> epRuleList = this.UpdateSequence();
    int index = ((PXSelectBase<EPRule>) this.Nodes).Current.Sequence.Value - 1;
    if (index < epRuleList.Count - 1)
    {
      EPRule epRule1 = epRuleList[index];
      int? sequence = epRule1.Sequence;
      epRule1.Sequence = sequence.HasValue ? new int?(sequence.GetValueOrDefault() + 1) : new int?();
      EPRule epRule2 = epRuleList[index + 1];
      sequence = epRule2.Sequence;
      epRule2.Sequence = sequence.HasValue ? new int?(sequence.GetValueOrDefault() - 1) : new int?();
      ((PXSelectBase<EPRule>) this.Nodes).Update(epRuleList[index]);
      ((PXSelectBase<EPRule>) this.Nodes).Update(epRuleList[index + 1]);
      ((PXSelectBase) this.Nodes).Cache.ActiveRow = (IBqlTable) epRuleList[index];
      ((PXSelectBase) this.NodesTree).View.RequestRefresh();
    }
    else if (((PXSelectBase<EPRule>) this.Nodes).Current.StepID.HasValue)
    {
      EPRule nextStep = this.GetNextStep(((PXSelectBase<EPRule>) this.Nodes).Current.StepID);
      if (nextStep != null)
      {
        ((PXSelectBase<EPRule>) this.Nodes).Current.StepID = nextStep.RuleID;
        ((PXSelectBase<EPRule>) this.Nodes).Current.Sequence = new int?(0);
        ((PXSelectBase<EPRule>) this.Nodes).Update(((PXSelectBase<EPRule>) this.Nodes).Current);
        this.UpdateSequence();
        ((PXSelectBase) this.Nodes).Cache.ClearQueryCacheObsolete();
        ((PXSelectBase) this.NodesTree).View.RequestRefresh();
      }
    }
    return adapter.Get();
  }

  private EPRule GetPreviousStep(Guid? currentStepId)
  {
    return this.GetNextStep(currentStepId, (IEnumerable<PXResult<EPRule>>) ((IQueryable<PXResult<EPRule>>) ((PXSelectBase<EPRule>) this.Nodes).Select(Array.Empty<object>())).OrderByDescending<PXResult<EPRule>, int?>((Expression<Func<PXResult<EPRule>, int?>>) (x => ((EPRule) x).Sequence)).ToList<PXResult<EPRule>>());
  }

  private EPRule GetNextStep(Guid? currentStepId)
  {
    return this.GetNextStep(currentStepId, (IEnumerable<PXResult<EPRule>>) ((IEnumerable<PXResult<EPRule>>) ((PXSelectBase<EPRule>) this.Nodes).Select(Array.Empty<object>())).ToList<PXResult<EPRule>>());
  }

  private EPRule GetNextStep(Guid? currentStepId, IEnumerable<PXResult<EPRule>> nodes)
  {
    bool flag = false;
    foreach (PXResult<EPRule> node in nodes)
    {
      if (flag)
        return PXResult<EPRule>.op_Implicit(node);
      Guid? ruleId = PXResult<EPRule>.op_Implicit(node).RuleID;
      Guid? nullable = currentStepId;
      if ((ruleId.HasValue == nullable.HasValue ? (ruleId.HasValue ? (ruleId.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        flag = true;
    }
    return (EPRule) null;
  }

  [PXUIField]
  [PXButton(Tooltip = "Add Step")]
  public virtual void AddStep()
  {
    int num = ((PXSelectBase<EPRule>) this.Nodes).Current != null ? 1 : 0;
    EPRule epRule1 = ((PXSelectBase<EPRule>) this.Nodes).Insert();
    epRule1.Name = "Step";
    if (num == 0 && !((PXGraph) this).IsImport)
    {
      EPRule epRule2 = ((PXSelectBase<EPRule>) this.Nodes).Insert();
      epRule2.StepID = epRule1.RuleID;
      epRule2.Name = "Rule";
    }
    ((PXSelectBase) this.Nodes).Cache.ActiveRow = (IBqlTable) epRule1;
    ((PXSelectBase) this.NodesTree).View.RequestRefresh();
  }

  [PXUIField]
  [PXButton(ImageKey = "AddNew", ImageSet = "main", Tooltip = "Add Rule")]
  public override void AddRule()
  {
    EPRule epRule1;
    if (((PXSelectBase<EPRule>) this.Nodes).Current == null)
    {
      EPRule epRule2 = ((PXSelectBase<EPRule>) this.Nodes).Insert();
      epRule2.Name = "Step";
      epRule1 = ((PXSelectBase<EPRule>) this.Nodes).Insert();
      epRule1.StepID = epRule2.RuleID;
    }
    else if (!((PXSelectBase<EPRule>) this.Nodes).Current.StepID.HasValue)
    {
      Guid? ruleId = ((PXSelectBase<EPRule>) this.Nodes).Current.RuleID;
      epRule1 = ((PXSelectBase<EPRule>) this.Nodes).Insert();
      epRule1.StepID = ruleId;
    }
    else
    {
      Guid? stepId = ((PXSelectBase<EPRule>) this.Nodes).Current.StepID;
      epRule1 = ((PXSelectBase<EPRule>) this.Nodes).Insert();
      epRule1.StepID = stepId;
    }
    epRule1.Name = "Rule";
    ((PXSelectBase) this.Nodes).Cache.ActiveRow = (IBqlTable) epRule1;
    ((PXSelectBase) this.NodesTree).View.RequestRefresh();
  }

  [PXMergeAttributes]
  [EPAssignmentMapSelector(MapType = 2)]
  protected virtual void EPAssignmentMap_AssignmentMapID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDefault(2)]
  protected virtual void EPAssignmentMap_MapType_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Workgroup")]
  [PXCompanyTreeSelector]
  [PXDefault]
  [PXFormula(typeof (Default<EPRule.ruleType>))]
  protected virtual void EPRuleTree_WorkgroupID_CacheAttached(PXCache sender)
  {
  }

  [Owner]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<EPRuleApprover.ownerID> e)
  {
  }

  protected override void EPAssignmentMap_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    EPAssignmentMap row = e.Row as EPAssignmentMap;
    ((PXAction) this.addStep).SetEnabled(row != null && row.EntityType != null);
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Rules).Cache, (string) null, ((PXSelectBase<EPRule>) this.CurrentNode).Current != null);
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.EmployeeCondition).Cache, (string) null, false);
    ((PXSelectBase) this.RuleApprovers).AllowInsert = ((PXSelectBase<EPRule>) this.CurrentNode).Current != null;
    base.EPAssignmentMap_RowSelected(sender, e);
  }

  protected virtual IEnumerable<string> GetEntityTypeScreens()
  {
    return (IEnumerable<string>) new string[27]
    {
      "AR301000",
      "AP301000",
      "AP302000",
      "AP304000",
      "AR302000",
      "AR304000",
      "CA302000",
      "CA304000",
      "EP305000",
      "EP308000",
      "EP301000",
      "EP301020",
      "PM301000",
      "PM307000",
      "PM308000",
      "PM308500",
      "PO301000",
      "RQ301000",
      "RQ302000",
      "SO301000",
      "SO303000",
      "CR304500",
      "PM304500",
      "PM305000",
      "PM305500",
      "PM305600",
      "GL301000"
    };
  }

  protected virtual void EPAssignmentMap_GraphType_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is EPAssignmentMap row))
      return;
    IOrderedEnumerable<PXSiteMapNode> source = this.Definitions.SiteMapNodes.OrderBy<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (x => x.Title));
    PXStringListAttribute.SetLocalizable<EPAssignmentMap.graphType>(sender, (object) null, false);
    PXCache pxCache = sender;
    string[] array1 = source.Select<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (x => x.GraphType)).ToArray<string>();
    string[] array2 = source.Select<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (x => x.Title)).ToArray<string>();
    PXStringListAttribute.SetList<EPAssignmentMap.graphType>(pxCache, (object) row, array1, array2);
  }

  private EPApprovalMapMaint.Definition Definitions
  {
    get
    {
      EPApprovalMapMaint.Definition definitions = PXContext.GetSlot<EPApprovalMapMaint.Definition>();
      if (definitions == null)
        definitions = PXContext.SetSlot<EPApprovalMapMaint.Definition>(PXDatabase.GetSlot<EPApprovalMapMaint.Definition, EPApprovalMapMaint>(typeof (EPApprovalMapMaint.Definition).FullName, this, new System.Type[1]
        {
          typeof (SiteMap)
        }));
      return definitions;
    }
  }

  private bool IsEpApproval(PXSiteMapNode node)
  {
    return GraphHelper.GetGraphViews(node.GraphType, false).Any<PXViewInfo>((Func<PXViewInfo, bool>) (x => x.Cache != null && x.Cache.CacheType == typeof (EPApproval)));
  }

  protected virtual void EPRule_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is EPRule row))
      return;
    IList<EPRule> epRuleList = this.UpdateSequence();
    if (row.StepID.HasValue)
    {
      bool flag = false;
      foreach (EPRule epRule1 in (IEnumerable<EPRule>) epRuleList)
      {
        if (flag)
        {
          EPRule epRule2 = epRule1;
          int? sequence = epRule2.Sequence;
          epRule2.Sequence = sequence.HasValue ? new int?(sequence.GetValueOrDefault() + 1) : new int?();
          ((PXSelectBase<EPRule>) this.Nodes).Update(epRule1);
        }
        else
        {
          Guid? ruleId1 = epRule1.RuleID;
          Guid? ruleId2 = ((PXSelectBase<EPRule>) this.Nodes).Current.RuleID;
          if ((ruleId1.HasValue == ruleId2.HasValue ? (ruleId1.HasValue ? (ruleId1.GetValueOrDefault() == ruleId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          {
            flag = true;
            EPRule epRule3 = row;
            int? sequence = epRule1.Sequence;
            int? nullable = sequence.HasValue ? new int?(sequence.GetValueOrDefault() + 1) : new int?();
            epRule3.Sequence = nullable;
          }
        }
      }
    }
    else
      row.Sequence = new int?((int) (((PXSelectBase) this.Nodes).Cache.Cached.Count() + 1L));
  }

  protected virtual void EPRule_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is EPRule row))
      return;
    row.OwnerID = new int?();
    if (row.OwnerSource == null && row.StepID.HasValue && row.RuleType == "E")
      throw new PXSetPropertyException<EPRule.ownerSource>("'Employee' cannot be empty", (PXErrorLevel) 4);
  }

  protected virtual void EPRule_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is EPRule row) || row.StepID.HasValue)
      return;
    foreach (PXResult<EPRule> pxResult in PXSelectBase<EPRule, PXSelect<EPRule, Where<EPRule.stepID, Equal<Required<EPRule.ruleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.RuleID
    }))
    {
      if (((PXSelectBase) this.Nodes).Cache.GetStatus((object) PXResult<EPRule>.op_Implicit(pxResult)) == 2)
        ((PXSelectBase) this.Nodes).Cache.SetStatus((object) PXResult<EPRule>.op_Implicit(pxResult), (PXEntryStatus) 4);
      else
        ((PXSelectBase<EPRule>) this.Nodes).Delete(PXResult<EPRule>.op_Implicit(pxResult));
    }
  }

  protected override void EPRule_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.EPRule_RowSelected(sender, e);
    if (!(e.Row is EPRule row))
      return;
    PXCache cache1 = ((PXSelectBase) this.RuleApprovers).Cache;
    Guid? stepId = row.StepID;
    int num1 = !stepId.HasValue ? 0 : (row.RuleType == "D" ? 1 : 0);
    PXUIFieldAttribute.SetVisible(cache1, (string) null, num1 != 0);
    PXCache cache2 = ((PXSelectBase) this.Nodes).Cache;
    EPRule current1 = ((PXSelectBase<EPRule>) this.Nodes).Current;
    stepId = row.StepID;
    int num2 = !stepId.HasValue ? 0 : (row.RuleType == "E" ? 1 : 0);
    PXUIFieldAttribute.SetVisible<EPRule.ownerSource>(cache2, (object) current1, num2 != 0);
    PXCache cache3 = ((PXSelectBase) this.EmployeeCondition).Cache;
    stepId = row.StepID;
    int num3 = !stepId.HasValue ? 0 : (row.RuleType == "F" ? 1 : 0);
    PXUIFieldAttribute.SetVisible(cache3, (string) null, num3 != 0);
    PXCache cache4 = ((PXSelectBase) this.Nodes).Cache;
    EPRule current2 = ((PXSelectBase<EPRule>) this.Nodes).Current;
    stepId = row.StepID;
    int num4 = !stepId.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<EPRule.emptyStepType>(cache4, (object) current2, num4 != 0);
    PXCache cache5 = ((PXSelectBase) this.Nodes).Cache;
    EPRule current3 = ((PXSelectBase<EPRule>) this.Nodes).Current;
    stepId = row.StepID;
    int num5 = !stepId.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<EPRule.executeStep>(cache5, (object) current3, num5 != 0);
    PXCache cache6 = ((PXSelectBase) this.Rules).Cache;
    stepId = row.StepID;
    int num6 = stepId.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible(cache6, (object) null, (string) null, num6 != 0);
    PXCache cache7 = ((PXSelectBase) this.Nodes).Cache;
    EPRule current4 = ((PXSelectBase<EPRule>) this.Nodes).Current;
    stepId = row.StepID;
    int num7 = stepId.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<EPRule.ruleType>(cache7, (object) current4, num7 != 0);
    PXCache cache8 = ((PXSelectBase) this.Nodes).Cache;
    EPRule current5 = ((PXSelectBase<EPRule>) this.Nodes).Current;
    stepId = row.StepID;
    int num8 = stepId.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<EPRule.workgroupID>(cache8, (object) current5, num8 != 0);
    PXCache cache9 = ((PXSelectBase) this.Nodes).Cache;
    EPRule current6 = ((PXSelectBase<EPRule>) this.Nodes).Current;
    stepId = row.StepID;
    int num9 = stepId.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<EPRule.approveType>(cache9, (object) current6, num9 != 0);
    PXCache cache10 = ((PXSelectBase) this.CurrentNode).Cache;
    EPRule current7 = ((PXSelectBase<EPRule>) this.CurrentNode).Current;
    stepId = row.StepID;
    int num10 = stepId.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<EPRule.waitTime>(cache10, (object) current7, num10 != 0);
    PXCache cache11 = ((PXSelectBase) this.CurrentNode).Cache;
    EPRule current8 = ((PXSelectBase<EPRule>) this.CurrentNode).Current;
    stepId = row.StepID;
    int num11 = stepId.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<EPRule.allowReassignment>(cache11, (object) current8, num11 != 0);
    PXCache cache12 = ((PXSelectBase) this.Nodes).Cache;
    EPRule current9 = ((PXSelectBase<EPRule>) this.Nodes).Current;
    stepId = row.StepID;
    int num12 = stepId.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<EPRule.reasonForApprove>(cache12, (object) current9, num12 != 0);
    PXCache cache13 = ((PXSelectBase) this.Nodes).Cache;
    EPRule current10 = ((PXSelectBase<EPRule>) this.Nodes).Current;
    stepId = row.StepID;
    int num13 = stepId.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<EPRule.reasonForReject>(cache13, (object) current10, num13 != 0);
    ((PXSelectBase) this.RuleApprovers).AllowInsert = true;
    Exception exception = (Exception) null;
    if (row.RuleType == "F")
      exception = (Exception) new PXSetPropertyException("Approval will be required from all employees specified in the following list.", (PXErrorLevel) 2);
    else if (row.RuleType == "D" && ((PXSelectBase<EPRuleApprover>) this.RuleApprovers).Select(Array.Empty<object>()).Count > 1)
      exception = (Exception) new PXSetPropertyException("Approval will be required from all employees specified in the following table.", (PXErrorLevel) 2);
    sender.RaiseExceptionHandling<EPRule.ruleType>((object) row, (object) row.RuleType, exception);
  }

  protected virtual void EPRuleEmployeeCondition_RuleID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) (Guid?) ((PXSelectBase<EPRule>) this.CurrentNode).Current?.RuleID;
  }

  protected virtual void EPRuleEmployeeCondition_FieldName_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is EPRuleEmployeeCondition row))
      return;
    e.ReturnState = (object) this.CreateFieldStateForFieldName(e.ReturnState, GraphHelper.GetType(row.Entity), (System.Type) null, row.Entity);
  }

  protected virtual void EPRuleEmployeeCondition_FieldName_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is EPRuleEmployeeCondition row))
      return;
    System.Type type = GraphHelper.GetType(row.Entity);
    if (type == (System.Type) null)
      return;
    PXCache cach = ((PXGraph) this).Caches[type];
    PXDBAttributeAttribute.Activate(cach);
    if (!(cach.GetStateExt((object) null, e.NewValue.ToString()) is PXFieldState))
      throw new PXException("The specified field cannot be found.");
  }

  protected virtual void _(PX.Data.Events.RowUpdated<EPRuleEmployeeCondition> e)
  {
    EPRuleEmployeeCondition oldRow = e.OldRow;
    EPRuleEmployeeCondition row1 = e.Row;
    if (oldRow == null || row1 == null)
      return;
    if (!string.Equals(row1.Entity, oldRow.Entity, StringComparison.OrdinalIgnoreCase))
      row1.FieldName = row1.Value = row1.Value2 = (string) null;
    if (!string.Equals(row1.FieldName, oldRow.FieldName, StringComparison.OrdinalIgnoreCase))
      row1.Value = row1.Value2 = (string) null;
    EPRuleEmployeeCondition row2 = e.Row;
    if (!row2.Condition.HasValue || row2.Condition.Value == 11 || row2.Condition.Value == 12)
      row1.Value = row1.Value2 = (string) null;
    if (row1.Value != null)
      return;
    PXFieldState stateExt = ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<EPRuleEmployeeCondition>>) e).Cache.GetStateExt<EPRuleEmployeeCondition.value>((object) row1) as PXFieldState;
    row1.Value = stateExt == null || stateExt.Value == null ? (string) null : stateExt.Value.ToString();
  }

  protected virtual void EPRuleEmployeeCondition_Value_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is EPRuleEmployeeCondition row))
      return;
    if (row.IsField.GetValueOrDefault())
    {
      e.ReturnState = (object) this.CreateFieldStateForFieldName(e.ReturnState, this.RelatedInfo.EntityType, (System.Type) null, this.RelatedInfo.EntityType.FullName);
    }
    else
    {
      if (string.IsNullOrEmpty(row.FieldName))
        return;
      int? condition = row.Condition;
      if (!condition.HasValue)
        return;
      condition = row.Condition;
      if (condition.Value == 11)
        return;
      condition = row.Condition;
      if (condition.Value == 12)
        return;
      condition = row.Condition;
      if (condition.Value == 9)
        return;
      condition = row.Condition;
      if (condition.Value == 6)
        return;
      condition = row.Condition;
      if (condition.Value == 8)
        return;
      condition = row.Condition;
      if (condition.Value == 7)
        return;
      PXFieldState stateForFieldValue = this.CreateFieldStateForFieldValue(e.ReturnState, row.Entity, row.Entity, row.FieldName);
      if (stateForFieldValue == null)
        return;
      e.ReturnState = (object) stateForFieldValue;
    }
  }

  protected virtual void EPRuleEmployeeCondition_Value2_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is EPRuleEmployeeCondition row))
      return;
    if (row.IsField.GetValueOrDefault())
    {
      e.ReturnState = (object) this.CreateFieldStateForFieldName(e.ReturnState, this.RelatedInfo.EntityType, (System.Type) null, this.RelatedInfo.EntityType.FullName);
    }
    else
    {
      if (row == null || string.IsNullOrEmpty(row.FieldName))
        return;
      int? condition = row.Condition;
      if (!condition.HasValue)
        return;
      condition = row.Condition;
      if (condition.Value == 11)
        return;
      condition = row.Condition;
      if (condition.Value == 12)
        return;
      condition = row.Condition;
      if (condition.Value == 9)
        return;
      condition = row.Condition;
      if (condition.Value == 6)
        return;
      condition = row.Condition;
      if (condition.Value == 8)
        return;
      condition = row.Condition;
      if (condition.Value == 7)
        return;
      PXFieldState stateForFieldValue = this.CreateFieldStateForFieldValue(e.ReturnState, row.Entity, row.Entity, row.FieldName);
      if (stateForFieldValue == null)
        return;
      stateForFieldValue.Value = (object) null;
      e.ReturnState = (object) stateForFieldValue;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelecting<EPRuleEmployeeCondition> e)
  {
    EPRuleEmployeeCondition row = e?.Row;
    if (row == null || row.UiNoteID.HasValue)
      return;
    row.UiNoteID = new Guid?(Guid.NewGuid());
  }

  public virtual void _(
    PX.Data.Events.FieldDefaulting<EPRuleEmployeeCondition.uiNoteID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<EPRuleEmployeeCondition.uiNoteID>, object, object>) e).NewValue = (object) Guid.NewGuid();
  }

  protected virtual void _(PX.Data.Events.RowSelected<EPRuleApprover> e)
  {
    EPRuleApprover row = e.Row;
    if (row == null)
      return;
    if (((PXSelectBase<EPRule>) this.CurrentNode).Current?.RuleType != "D")
    {
      PXUIFieldAttribute.SetError<EPRuleApprover.ownerID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPRuleApprover>>) e).Cache, (object) row, (string) null);
    }
    else
    {
      if (((PXSelectBase<PX.Objects.CR.Standalone.EPEmployee>) this.EmployeeInactive).SelectSingle(new object[1]
      {
        (object) row.OwnerID
      }) == null)
        return;
      PXUIFieldAttribute.SetWarning<EPRuleApprover.ownerID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPRuleApprover>>) e).Cache, (object) row, "The employee is not active.");
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<EPRule, EPRule.workgroupID> e)
  {
    if (e.Row == null)
      return;
    EnumerableExtensions.ForEach<EPRuleApprover>((IEnumerable<EPRuleApprover>) ((PXSelectBase<EPRuleApprover>) this.RuleApprovers).SelectMain(Array.Empty<object>()), (Action<EPRuleApprover>) (approver =>
    {
      object ownerId = (object) approver.OwnerID;
      try
      {
        ((PXSelectBase) this.RuleApprovers).Cache.RaiseFieldVerifying<EPRuleApprover.ownerID>((object) approver, ref ownerId);
      }
      catch (Exception ex)
      {
        ((PXSelectBase<EPRuleApprover>) this.RuleApprovers).Delete(approver);
      }
    }));
  }

  public virtual string GetWorkflowStatePropertyName()
  {
    string screenId = PXSiteMapProviderExtensions.FindSiteMapNodeByGraphType(PXSiteMap.Provider, ((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current.GraphType)?.ScreenID;
    return string.IsNullOrEmpty(screenId) ? string.Empty : this.WorkflowService.GetWorkflowStatePropertyName(screenId);
  }

  public override bool CheckFieldValid(KeyValuePair<string, string> item)
  {
    return !item.Key.Equals(this.GetWorkflowStatePropertyName(), StringComparison.OrdinalIgnoreCase) && base.CheckFieldValid(item);
  }

  protected override bool FieldCannotBeFoundAdditionalCondition(string fieldName)
  {
    return fieldName.Equals(this.GetWorkflowStatePropertyName(), StringComparison.OrdinalIgnoreCase);
  }

  private class Definition : IPrefetchable<EPApprovalMapMaint>, IPXCompanyDependent
  {
    public List<PXSiteMapNode> SiteMapNodes = new List<PXSiteMapNode>();

    public void Prefetch(EPApprovalMapMaint graph)
    {
      this.SiteMapNodes = PXSiteMapProviderExtensions.GetNodes(PXSiteMap.Provider).Where<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (x => graph.GetEntityTypeScreens().Contains<string>(x.ScreenID) && graph.IsEpApproval(x))).GroupBy<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (x => x.ScreenID)).Select<IGrouping<string, PXSiteMapNode>, PXSiteMapNode>((Func<IGrouping<string, PXSiteMapNode>, PXSiteMapNode>) (x => x.First<PXSiteMapNode>())).ToList<PXSiteMapNode>();
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPAssignmentMapMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.SM;
using PX.TM;
using PX.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public class EPAssignmentMapMaint : EPApprovalAndAssignmentMapBase<EPAssignmentMapMaint>
{
  public EPAssignmentMapMaint()
  {
    ((PXSelectBase) this.AssigmentMap).View = new PXView((PXGraph) this, false, BqlCommand.CreateInstance(new Type[1]
    {
      BqlCommand.Compose(new Type[6]
      {
        typeof (Select<,>),
        typeof (EPAssignmentMap),
        typeof (Where<,>),
        typeof (EPAssignmentMap.mapType),
        typeof (Equal<>),
        typeof (EPMapType.assignment)
      })
    }));
  }

  protected virtual IEnumerable nodes([PXDBGuid(false)] Guid? ruleID)
  {
    List<EPRule> epRuleList = new List<EPRule>();
    if (ruleID.HasValue)
      return (IEnumerable) epRuleList;
    foreach (PXResult<EPRule, EPCompanyTree> pxResult in (IEnumerable) PXSelectBase<EPRule, PXSelectJoin<EPRule, LeftJoin<EPCompanyTree, On<EPRule.workgroupID, Equal<EPCompanyTree.workGroupID>>>, Where<EPRule.assignmentMapID, Equal<Current<EPAssignmentMap.assignmentMapID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      EPRule epRule = PXResult<EPRule, EPCompanyTree>.op_Implicit(pxResult);
      epRule.Icon = Sprite.Tree.GetFullUrl("Leaf");
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
    }
    return adapter.Get();
  }

  [PXMergeAttributes]
  [EPAssignmentMapSelector(MapType = 1)]
  protected virtual void EPAssignmentMap_AssignmentMapID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDefault(1)]
  protected virtual void EPAssignmentMap_MapType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Assign Ownership To")]
  [EPAssignRuleType.List]
  protected virtual void EPRuleTree_RuleType_CacheAttached(PXCache sender)
  {
  }

  protected override void EPAssignmentMap_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.EPAssignmentMap_RowSelected(sender, e);
    ((PXSelectBase) this.Rules).Cache.AllowInsert = ((PXSelectBase<EPRule>) this.CurrentNode).Current != null;
  }

  protected virtual IEnumerable<string> GetEntityTypeScreens()
  {
    return (IEnumerable<string>) new string[10]
    {
      "CR303000",
      "CR306000",
      "CR302000",
      "CR306015",
      "CR301000",
      "CR304000",
      "PO302000",
      "RQ301000",
      "RQ302000",
      "PM305000"
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

  private EPAssignmentMapMaint.Definition Definitions
  {
    get
    {
      EPAssignmentMapMaint.Definition definitions = PXContext.GetSlot<EPAssignmentMapMaint.Definition>();
      if (definitions == null)
        definitions = PXContext.SetSlot<EPAssignmentMapMaint.Definition>(PXDatabase.GetSlot<EPAssignmentMapMaint.Definition, EPAssignmentMapMaint>(typeof (EPAssignmentMapMaint.Definition).FullName, this, new Type[1]
        {
          typeof (SiteMap)
        }));
      return definitions;
    }
  }

  protected virtual void EPRule_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Row is EPRule row && row.OwnerSource == null && row.RuleType == "E")
      throw new PXSetPropertyException<EPRule.ownerSource>("'Employee' cannot be empty", (PXErrorLevel) 4);
  }

  protected virtual void EPRule_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is EPRule row))
      return;
    IList<EPRule> epRuleList = this.UpdateSequence();
    bool flag = false;
    int? sequence;
    int? nullable1;
    foreach (EPRule epRule1 in (IEnumerable<EPRule>) epRuleList)
    {
      if (flag)
      {
        EPRule epRule2 = epRule1;
        sequence = epRule2.Sequence;
        nullable1 = sequence;
        epRule2.Sequence = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
        ((PXSelectBase<EPRule>) this.Nodes).Update(epRule1);
      }
      else if (((PXSelectBase<EPRule>) this.Nodes).Current != null)
      {
        Guid? ruleId1 = epRule1.RuleID;
        Guid? ruleId2 = ((PXSelectBase<EPRule>) this.Nodes).Current.RuleID;
        if ((ruleId1.HasValue == ruleId2.HasValue ? (ruleId1.HasValue ? (ruleId1.GetValueOrDefault() == ruleId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          flag = true;
          sequence = epRule1.Sequence;
          int? nullable2;
          if (!sequence.HasValue)
          {
            nullable1 = new int?();
            nullable2 = nullable1;
          }
          else
            nullable2 = new int?(sequence.GetValueOrDefault() + 1);
          row.Sequence = nullable2;
        }
      }
    }
  }

  private class Definition : IPrefetchable<EPAssignmentMapMaint>, IPXCompanyDependent
  {
    public List<PXSiteMapNode> SiteMapNodes = new List<PXSiteMapNode>();

    public void Prefetch(EPAssignmentMapMaint graph)
    {
      this.SiteMapNodes = PXSiteMapProviderExtensions.GetNodes(PXSiteMap.Provider).Where<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (x => graph.GetEntityTypeScreens().Contains<string>(x.ScreenID))).GroupBy<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (x => x.ScreenID)).Select<IGrouping<string, PXSiteMapNode>, PXSiteMapNode>((Func<IGrouping<string, PXSiteMapNode>, PXSiteMapNode>) (x => x.First<PXSiteMapNode>())).ToList<PXSiteMapNode>();
    }
  }
}

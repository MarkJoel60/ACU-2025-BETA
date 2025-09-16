// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPAssignmentMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PM;
using PX.SM;
using PX.TM;
using PX.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.EP;

public class EPAssignmentMaint : PXGraph<EPAssignmentMaint>
{
  public PXSave<EPAssignmentMap> Save;
  public PXCancel<EPAssignmentMap> Cancel;
  public PXInsert<EPAssignmentMap> Insert;
  public PXDelete<EPAssignmentMap> Delete;
  public PXSelect<PX.Objects.CR.BAccount> bAccount;
  public PXSelect<PX.Objects.AP.Vendor> vendor;
  public PXSelect<PX.Objects.AR.Customer> customer;
  public PXSelect<EPEmployee> employee;
  [PXViewName("Assignment Map")]
  public PXSelect<EPAssignmentMap, Where<EPAssignmentMap.mapType, IsNull, Or<EPAssignmentMap.mapType, Equal<Zero>>>> AssigmentMap;
  public PXSetup<Company> company;
  public PXSetup<EPSetup> setup;
  public PXAction<EPAssignmentMap> down;
  public PXSelectJoin<EPAssignmentRoute, LeftJoin<EPCompanyTree, On<EPAssignmentRoute.workgroupID, Equal<EPCompanyTree.workGroupID>>>, Where<EPAssignmentRoute.assignmentRouteID, IsNotNull, And<EPAssignmentRoute.parent, Equal<Argument<int?>>>>, OrderBy<Asc<EPAssignmentRoute.sequence>>> Nodes;
  public PXSelectJoin<EPAssignmentRoute, LeftJoin<EPEmployee, On<EPEmployee.defContactID, Equal<EPAssignmentRoute.ownerID>>>, Where<EPAssignmentRoute.sequence, IsNotNull>, OrderBy<Asc<EPAssignmentRoute.sequence>>> Items;
  public PXSelect<EPAssignmentRoute, Where<EPAssignmentRoute.assignmentRouteID, Equal<Current<EPAssignmentRoute.assignmentRouteID>>>> CurrentItem;
  public PXFilter<Position> PositionFilter;
  public PXSelect<EPAssignmentRule> Rules;
  public PXAction<EPAssignmentMap> up;
  public PXSelect<EPEmployee> Employee;
  [PXHidden]
  public PXSelect<PX.Objects.CT.Contract> Dummy_Contract;
  [PXHidden]
  public PXSelect<PMProject> Dummy_Project;
  private const int RootNodeID = -99;
  public PXSelect<CacheEntityItem, Where<CacheEntityItem.path, Equal<CacheEntityItem.path>>, OrderBy<Asc<CacheEntityItem.number>>> EntityItems;
  private EPAssignmentRoute current;
  private const string _FIELDNAME_STR = "FieldName";

  public EPAssignmentMaint()
  {
    ((PXGraph) this).Views.Caches.Remove(((PXSelectBase) this.bAccount).Cache.GetItemType());
    ((PXGraph) this).Views.Caches.Remove(((PXSelectBase) this.vendor).Cache.GetItemType());
    ((PXGraph) this).Views.Caches.Remove(((PXSelectBase) this.customer).Cache.GetItemType());
    ((PXGraph) this).Views.Caches.Remove(((PXSelectBase) this.employee).Cache.GetItemType());
  }

  protected virtual IEnumerable nodes([PXDBInt] int? assignmentRouteID)
  {
    List<EPAssignmentRoute> epAssignmentRouteList = new List<EPAssignmentRoute>();
    if (!assignmentRouteID.HasValue)
    {
      epAssignmentRouteList.Add(new EPAssignmentRoute()
      {
        AssignmentRouteID = new int?(-99),
        AssignmentMapID = ((PXSelectBase<Position>) this.PositionFilter).Current.MapID,
        Name = ((PXGraph) this).Accessinfo.CompanyName ?? "Company",
        Icon = Sprite.Main.GetFullUrl("Folder")
      });
    }
    else
    {
      IEnumerable enumerable;
      if (assignmentRouteID.GetValueOrDefault() == -99)
        enumerable = (IEnumerable) PXSelectBase<EPAssignmentRoute, PXSelectJoin<EPAssignmentRoute, LeftJoin<EPCompanyTree, On<EPAssignmentRoute.workgroupID, Equal<EPCompanyTree.workGroupID>>>, Where<EPAssignmentRoute.assignmentMapID, Equal<Current<Position.mapID>>, And<EPAssignmentRoute.parent, IsNull>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
      else
        enumerable = (IEnumerable) PXSelectBase<EPAssignmentRoute, PXSelectJoin<EPAssignmentRoute, LeftJoin<EPCompanyTree, On<EPAssignmentRoute.workgroupID, Equal<EPCompanyTree.workGroupID>>>, Where<EPAssignmentRoute.assignmentMapID, Equal<Current<Position.mapID>>, And<EPAssignmentRoute.parent, Equal<Required<EPAssignmentRoute.parent>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) assignmentRouteID
        });
      foreach (PXResult<EPAssignmentRoute, EPCompanyTree> pxResult in enumerable)
      {
        EPAssignmentRoute epAssignmentRoute = PXResult<EPAssignmentRoute, EPCompanyTree>.op_Implicit(pxResult);
        if (epAssignmentRoute.RouterType == "W")
          epAssignmentRoute.Icon = Sprite.Main.GetFullUrl("Roles");
        else if (epAssignmentRoute.RouteID.HasValue)
        {
          epAssignmentRoute.Icon = Sprite.Main.GetFullUrl("Redo");
        }
        else
        {
          int? ownerId = epAssignmentRoute.OwnerID;
          epAssignmentRoute.Icon = !ownerId.HasValue ? Sprite.Main.GetFullUrl("Folder") : Sprite.Main.GetFullUrl("Users");
        }
        epAssignmentRouteList.Add(epAssignmentRoute);
      }
    }
    ((PXSelectBase<Position>) this.PositionFilter).Current.UseCurrentTreeItem = new bool?(PXView.Searches != null && PXView.Searches.Length != 0 && PXView.Searches[0] != null);
    return (IEnumerable) epAssignmentRouteList;
  }

  protected virtual IEnumerable items([PXDBInt] int? parent)
  {
    EPAssignmentMaint epAssignmentMaint = this;
    if ((((PXGraph) epAssignmentMaint).IsExport || ((PXGraph) epAssignmentMaint).IsImport) && !parent.HasValue && ((PXSelectBase<EPAssignmentRoute>) epAssignmentMaint.Nodes).Current != null && !PXGraph.ProxyIsActive)
      parent = ((PXSelectBase<EPAssignmentRoute>) epAssignmentMaint.Nodes).Current.AssignmentRouteID;
    ((PXSelectBase<Position>) epAssignmentMaint.PositionFilter).Current.NodeID = parent;
    if (parent.GetValueOrDefault() == -99)
      ((PXSelectBase<Position>) epAssignmentMaint.PositionFilter).Current.NodeID = new int?();
    PXResultset<EPAssignmentRoute> pxResultset;
    if (!parent.HasValue || parent.GetValueOrDefault() == -99)
      pxResultset = PXSelectBase<EPAssignmentRoute, PXSelectJoin<EPAssignmentRoute, LeftJoin<EPCompanyTree, On<EPAssignmentRoute.workgroupID, Equal<EPCompanyTree.workGroupID>>, LeftJoin<EPEmployee, On<EPEmployee.defContactID, Equal<EPAssignmentRoute.ownerID>>>>, Where<EPAssignmentRoute.assignmentMapID, Equal<Current<Position.mapID>>, And<EPAssignmentRoute.parent, IsNull>>>.Config>.Select((PXGraph) epAssignmentMaint, Array.Empty<object>());
    else
      pxResultset = PXSelectBase<EPAssignmentRoute, PXSelectJoin<EPAssignmentRoute, LeftJoin<EPCompanyTree, On<EPAssignmentRoute.workgroupID, Equal<EPCompanyTree.workGroupID>>, LeftJoin<EPEmployee, On<EPEmployee.defContactID, Equal<EPAssignmentRoute.ownerID>>>>, Where<EPAssignmentRoute.assignmentMapID, Equal<Current<Position.mapID>>, And<EPAssignmentRoute.parent, Equal<Required<EPAssignmentRoute.parent>>>>>.Config>.Select((PXGraph) epAssignmentMaint, new object[1]
      {
        (object) parent
      });
    ((PXSelectBase) epAssignmentMaint.Rules).Cache.AllowInsert = pxResultset.Count > 0;
    ((PXSelectBase) epAssignmentMaint.Rules).Cache.AllowUpdate = pxResultset.Count > 0;
    ((PXSelectBase) epAssignmentMaint.Rules).Cache.AllowDelete = pxResultset.Count > 0;
    foreach (PXResult<EPAssignmentRoute, EPCompanyTree> pxResult in pxResultset)
      yield return (object) pxResult;
  }

  protected virtual IEnumerable rules([PXDBInt] int? routeID)
  {
    if ((((PXGraph) this).IsExport || ((PXGraph) this).IsImport) && !routeID.HasValue && ((PXSelectBase<EPAssignmentRoute>) this.Items).Current != null && !PXGraph.ProxyIsActive)
    {
      int? nodeId = ((PXSelectBase<Position>) this.PositionFilter).Current.NodeID;
      int? assignmentRouteId = ((PXSelectBase<EPAssignmentRoute>) this.Items).Current.AssignmentRouteID;
      if (!(nodeId.GetValueOrDefault() == assignmentRouteId.GetValueOrDefault() & nodeId.HasValue == assignmentRouteId.HasValue))
        routeID = ((PXSelectBase<EPAssignmentRoute>) this.Items).Current.AssignmentRouteID;
    }
    ((PXSelectBase<Position>) this.PositionFilter).Current.ItemID = routeID;
    List<EPAssignmentRule> epAssignmentRuleList = new List<EPAssignmentRule>();
    PXResultset<EPAssignmentRule> pxResultset;
    if (!routeID.HasValue)
      pxResultset = PXSelectBase<EPAssignmentRule, PXSelect<EPAssignmentRule, Where<EPAssignmentRule.assignmentRouteID, IsNull>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    else
      pxResultset = PXSelectBase<EPAssignmentRule, PXSelect<EPAssignmentRule, Where<EPAssignmentRule.assignmentRouteID, Equal<Required<EPAssignmentRoute.parent>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) routeID
      });
    foreach (PXResult<EPAssignmentRule> pxResult in pxResultset)
    {
      EPAssignmentRule epAssignmentRule = PXResult<EPAssignmentRule>.op_Implicit(pxResult);
      epAssignmentRuleList.Add(epAssignmentRule);
    }
    return (IEnumerable) epAssignmentRuleList;
  }

  protected IEnumerable entityItems(string parent)
  {
    EPAssignmentMaint epAssignmentMaint = this;
    if (((PXSelectBase<EPAssignmentMap>) epAssignmentMaint.AssigmentMap).Current != null)
    {
      System.Type type1 = GraphHelper.GetType(((PXSelectBase<EPAssignmentMap>) epAssignmentMaint.AssigmentMap).Current.EntityType);
      System.Type type2;
      if (((PXSelectBase<EPAssignmentMap>) epAssignmentMaint.AssigmentMap).Current.GraphType == null)
      {
        if (type1 == (System.Type) null && parent != null)
          yield break;
        type2 = EntityHelper.GetPrimaryGraphType((PXGraph) epAssignmentMaint, type1);
      }
      else
        type2 = GraphHelper.GetType(((PXSelectBase<EPAssignmentMap>) epAssignmentMaint.AssigmentMap).Current.GraphType);
      foreach (CacheEntityItem cacheEntityItem in EMailSourceHelper.TemplateEntity((PXGraph) epAssignmentMaint, parent, type1.FullName, type2 != (System.Type) null ? type2.FullName : (string) null))
        yield return (object) cacheEntityItem;
    }
  }

  protected virtual IEnumerable<string> GetEntityTypeScreens()
  {
    return (IEnumerable<string>) new string[29]
    {
      "AR301000",
      "AP301000",
      "AP302000",
      "AP304000",
      "AR302000",
      "AR304000",
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
      "CR304500",
      "PM304500",
      "PM305000",
      "CR303000",
      "CR306000",
      "CR302000",
      "CR306015",
      "CR301000",
      "CR304000",
      "PO302000"
    };
  }

  private IEnumerable<PXSiteMapNode> GetEntityTypeNodes()
  {
    return (IEnumerable<PXSiteMapNode>) PXSiteMapProviderExtensions.GetNodes(PXSiteMap.Provider).Where<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (x => this.GetEntityTypeScreens().Contains<string>(x.ScreenID))).GroupBy<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (x => x.ScreenID)).Select<IGrouping<string, PXSiteMapNode>, PXSiteMapNode>((Func<IGrouping<string, PXSiteMapNode>, PXSiteMapNode>) (x => x.First<PXSiteMapNode>())).ToList<PXSiteMapNode>();
  }

  private IEnumerable<PXSiteMapNode> GraphTypeList()
  {
    EPAssignmentMaint epAssignmentMaint = this;
    if (((PXSelectBase<EPAssignmentMap>) epAssignmentMaint.AssigmentMap).Current != null)
    {
      EPAssignmentMap current = ((PXSelectBase<EPAssignmentMap>) epAssignmentMaint.AssigmentMap).Current;
      if (PXResultset<EPAssignmentRoute>.op_Implicit(PXSelectBase<EPAssignmentRoute, PXSelect<EPAssignmentRoute, Where<EPAssignmentRoute.assignmentMapID, Equal<Required<EPAssignmentRoute.assignmentMapID>>>>.Config>.Select((PXGraph) epAssignmentMaint, new object[1]
      {
        (object) current.AssignmentMapID
      })) != null && current.EntityType != null)
      {
        System.Type type1;
        if (current.GraphType != null)
        {
          type1 = GraphHelper.GetType(current.GraphType);
        }
        else
        {
          System.Type type2 = PXBuildManager.GetType(current.EntityType, false);
          type1 = EntityHelper.GetPrimaryGraphType((PXGraph) epAssignmentMaint, type2);
          current.GraphType = type1.FullName;
        }
        if (type1 != (System.Type) null)
        {
          PXSiteMapNode siteMapNodeUnsecure = PXSiteMapProviderExtensions.FindSiteMapNodeUnsecure(PXSiteMap.Provider, type1);
          if (siteMapNodeUnsecure != null)
            yield return siteMapNodeUnsecure;
        }
        else
        {
          foreach (PXSiteMapNode entityTypeNode in epAssignmentMaint.GetEntityTypeNodes())
            yield return entityTypeNode;
        }
      }
      else
      {
        foreach (PXSiteMapNode entityTypeNode in epAssignmentMaint.GetEntityTypeNodes())
          yield return entityTypeNode;
      }
    }
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowUp", Tooltip = "Move Node Up")]
  public virtual IEnumerable Up(PXAdapter adapter)
  {
    IList<EPAssignmentRoute> sortedItems = this.GetSortedItems();
    int? assignmentRouteId = ((PXSelectBase<EPAssignmentRoute>) this.Items).Current.AssignmentRouteID;
    int index1 = 0;
    int? nullable1;
    int? nullable2;
    for (int index2 = 0; index2 < sortedItems.Count; ++index2)
    {
      nullable1 = sortedItems[index2].AssignmentRouteID;
      nullable2 = assignmentRouteId;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        index1 = index2;
      sortedItems[index2].Sequence = new int?(index2 + 1);
      ((PXSelectBase<EPAssignmentRoute>) this.Items).Update(sortedItems[index2]);
    }
    if (index1 > 0)
    {
      EPAssignmentRoute epAssignmentRoute1 = sortedItems[index1];
      nullable2 = epAssignmentRoute1.Sequence;
      nullable1 = nullable2;
      epAssignmentRoute1.Sequence = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() - 1) : new int?();
      EPAssignmentRoute epAssignmentRoute2 = sortedItems[index1 - 1];
      nullable2 = epAssignmentRoute2.Sequence;
      nullable1 = nullable2;
      epAssignmentRoute2.Sequence = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
      ((PXSelectBase<EPAssignmentRoute>) this.Items).Update(sortedItems[index1]);
      ((PXSelectBase<EPAssignmentRoute>) this.Items).Update(sortedItems[index1 - 1]);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowDown", Tooltip = "Move Node Down")]
  public virtual IEnumerable Down(PXAdapter adapter)
  {
    IList<EPAssignmentRoute> sortedItems = this.GetSortedItems();
    int? assignmentRouteId = ((PXSelectBase<EPAssignmentRoute>) this.Items).Current.AssignmentRouteID;
    int index1 = 0;
    int? nullable1;
    int? nullable2;
    for (int index2 = 0; index2 < sortedItems.Count; ++index2)
    {
      nullable1 = sortedItems[index2].AssignmentRouteID;
      nullable2 = assignmentRouteId;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        index1 = index2;
      sortedItems[index2].Sequence = new int?(index2 + 1);
      ((PXSelectBase<EPAssignmentRoute>) this.Items).Update(sortedItems[index2]);
    }
    if (index1 < sortedItems.Count - 1)
    {
      EPAssignmentRoute epAssignmentRoute1 = sortedItems[index1];
      nullable2 = epAssignmentRoute1.Sequence;
      nullable1 = nullable2;
      epAssignmentRoute1.Sequence = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
      EPAssignmentRoute epAssignmentRoute2 = sortedItems[index1 + 1];
      nullable2 = epAssignmentRoute2.Sequence;
      nullable1 = nullable2;
      epAssignmentRoute2.Sequence = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() - 1) : new int?();
      ((PXSelectBase<EPAssignmentRoute>) this.Items).Update(sortedItems[index1]);
      ((PXSelectBase<EPAssignmentRoute>) this.Items).Update(sortedItems[index1 + 1]);
    }
    return adapter.Get();
  }

  [PXDBString(255 /*0xFF*/)]
  protected virtual void EPAssignmentMap_EntityType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXStringList]
  protected virtual void EPAssignmentMap_GraphType_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Workgroup")]
  [PXCompanyTreeSelector]
  [PXDefault]
  [PXFormula(typeof (Default<EPAssignmentRoute.routerType>))]
  protected virtual void EPAssignmentRoute_WorkgroupID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void EPAssignmentRoute_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    EPAssignmentRoute newRow = (EPAssignmentRoute) e.NewRow;
    EPAssignmentRoute row = (EPAssignmentRoute) e.Row;
    int? sequence1 = newRow.Sequence;
    int? sequence2 = row.Sequence;
    if (sequence1.GetValueOrDefault() == sequence2.GetValueOrDefault() & sequence1.HasValue == sequence2.HasValue || !e.ExternalCall)
      return;
    int? sequence3 = row.Sequence;
    int? sequence4 = newRow.Sequence;
    if (sequence3.GetValueOrDefault() < sequence4.GetValueOrDefault() & sequence3.HasValue & sequence4.HasValue)
    {
      PXCache sender1 = sender;
      EPAssignmentRoute route = newRow;
      sequence4 = row.Sequence;
      int? from = sequence4.HasValue ? new int?(sequence4.GetValueOrDefault() + 1) : new int?();
      int? sequence5 = newRow.Sequence;
      this.UpdateSequence(sender1, route, from, sequence5, -1);
    }
    else
      this.UpdateSequence(sender, newRow, newRow.Sequence, row.Sequence, 1);
    ((PXSelectBase) this.AssigmentMap).View.RequestRefresh();
  }

  private void UpdateSequence(
    PXCache sender,
    EPAssignmentRoute route,
    int? from,
    int? to,
    int step)
  {
    foreach (PXResult<EPAssignmentRoute> pxResult in PXSelectBase<EPAssignmentRoute, PXSelect<EPAssignmentRoute, Where<EPAssignmentRoute.assignmentMapID, Equal<Required<EPAssignmentRoute.assignmentMapID>>, And<EPAssignmentRoute.sequence, Between<Required<EPAssignmentRoute.sequence>, Required<EPAssignmentRoute.sequence>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) route.AssignmentMapID,
      (object) from,
      (object) to
    }))
    {
      EPAssignmentRoute epAssignmentRoute1 = PXResult<EPAssignmentRoute>.op_Implicit(pxResult);
      int? assignmentRouteId = epAssignmentRoute1.AssignmentRouteID;
      int? nullable = route.AssignmentRouteID;
      if (!(assignmentRouteId.GetValueOrDefault() == nullable.GetValueOrDefault() & assignmentRouteId.HasValue == nullable.HasValue))
      {
        EPAssignmentRoute epAssignmentRoute2 = epAssignmentRoute1;
        nullable = epAssignmentRoute2.Sequence;
        int num = step;
        epAssignmentRoute2.Sequence = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + num) : new int?();
        sender.SetStatus((object) epAssignmentRoute1, (PXEntryStatus) 1);
      }
    }
  }

  protected virtual void EPAssignmentRoute_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is EPAssignmentRoute row) || ((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current == null)
      return;
    IEnumerable enumerable = this.items(((PXSelectBase<Position>) this.PositionFilter).Current.NodeID);
    int num = 0;
    foreach (PXResult<EPAssignmentRoute, EPCompanyTree> pxResult in enumerable)
    {
      int? sequence = ((EPAssignmentRoute) ((PXResult) pxResult)[0]).Sequence;
      if (sequence.Value > num)
      {
        sequence = ((EPAssignmentRoute) ((PXResult) pxResult)[0]).Sequence;
        num = sequence.Value;
      }
    }
    if (((PXGraph) this).IsImport && !PXGraph.ProxyIsActive)
    {
      if (((PXSelectBase<Position>) this.PositionFilter).Current.UseCurrentTreeItem.GetValueOrDefault() && ((PXSelectBase<EPAssignmentRoute>) this.Nodes).Current != null)
        ((PXSelectBase<Position>) this.PositionFilter).Current.RouteParentID = ((PXSelectBase<EPAssignmentRoute>) this.Nodes).Current.AssignmentRouteID.GetValueOrDefault() != -99 ? ((PXSelectBase<EPAssignmentRoute>) this.Nodes).Current.AssignmentRouteID : new int?();
      ((PXSelectBase<Position>) this.PositionFilter).Current.UseCurrentTreeItem = new bool?(false);
      row.Parent = ((PXSelectBase<Position>) this.PositionFilter).Current.RouteParentID;
    }
    else
      row.Parent = ((PXSelectBase<Position>) this.PositionFilter).Current.NodeID;
    row.Sequence = new int?(num + 1);
    row.AssignmentMapID = ((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current.AssignmentMapID;
  }

  protected virtual void EPAssignmentRoute_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is EPAssignmentRoute row) || ((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current == null)
      return;
    ((PXSelectBase<Position>) this.PositionFilter).Current.NodeID = row.RouteID;
    ((PXSelectBase<Position>) this.PositionFilter).Current.RouteItemID = row.AssignmentRouteID;
  }

  protected virtual void EPAssignmentRoute_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (sender.Current == null)
      return;
    IList<EPAssignmentRoute> sortedItems = this.GetSortedItems();
    int num = 1;
    foreach (EPAssignmentRoute epAssignmentRoute in (IEnumerable<EPAssignmentRoute>) sortedItems)
    {
      epAssignmentRoute.Sequence = new int?(num++);
      sender.SetStatus((object) epAssignmentRoute, (PXEntryStatus) 1);
    }
    ((PXSelectBase) this.Items).View.RequestRefresh();
  }

  protected virtual void EPAssignmentRoute_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is EPAssignmentRoute row))
      return;
    PXCache cache1 = ((PXSelectBase) this.Rules).Cache;
    int? sequence;
    int num1;
    if (e.Row != null)
    {
      sequence = row.Sequence;
      num1 = sequence.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    cache1.AllowInsert = num1 != 0;
    PXCache cache2 = ((PXSelectBase) this.Rules).Cache;
    int num2;
    if (e.Row != null)
    {
      sequence = row.Sequence;
      num2 = sequence.HasValue ? 1 : 0;
    }
    else
      num2 = 0;
    cache2.AllowUpdate = num2 != 0;
    PXCache cache3 = ((PXSelectBase) this.Rules).Cache;
    int num3;
    if (e.Row != null)
    {
      sequence = row.Sequence;
      num3 = sequence.HasValue ? 1 : 0;
    }
    else
      num3 = 0;
    cache3.AllowDelete = num3 != 0;
    PXUIFieldAttribute.SetEnabled<EPAssignmentRoute.workgroupID>(sender, (object) row, row == null || row.RouterType == "W");
    PXUIFieldAttribute.SetEnabled<EPAssignmentRoute.ownerID>(sender, (object) row, row == null || row.RouterType == "W");
    PXUIFieldAttribute.SetEnabled<EPAssignmentRoute.routeID>(sender, (object) row, row != null && row.RouterType == "R");
  }

  protected virtual void EPAssignmentRoute_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is EPAssignmentRoute row) || e.Operation == 3)
      return;
    PXDefaultAttribute.SetPersistingCheck<EPAssignmentRoute.routeID>(sender, e.Row, row.RouterType == "R" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    if (e.Operation != 2)
      return;
    int? nullable1 = ((PXSelectBase<Position>) this.PositionFilter).Current.MapID;
    int? nullable2 = row.AssignmentMapID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      return;
    nullable2 = ((PXSelectBase<Position>) this.PositionFilter).Current.NodeID;
    nullable1 = row.AssignmentRouteID;
    if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
    {
      nullable1 = ((PXSelectBase<Position>) this.PositionFilter).Current.NodeID;
      if (nullable1.HasValue)
        return;
    }
    this.current = row;
  }

  protected virtual void EPAssignmentRoute_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    EPAssignmentRoute row = (EPAssignmentRoute) e.Row;
    if (row != this.current || e.TranStatus != 1)
      return;
    ((PXSelectBase<Position>) this.PositionFilter).Current.MapID = row.AssignmentMapID;
    ((PXSelectBase<Position>) this.PositionFilter).Current.NodeID = row.AssignmentRouteID;
  }

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

  protected virtual void EPAssignmentMap_EntityType_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue != null && GraphHelper.GetType((string) e.NewValue) == (System.Type) null)
      throw new PXSetPropertyException(e.Row as IBqlTable, "Only types can be used as entity type for assignment map.", (PXErrorLevel) 4);
  }

  protected virtual void EPAssignmentMap_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is EPAssignmentMap row))
      return;
    if (row.EntityType != null && row.GraphType == null)
    {
      System.Type type = PXBuildManager.GetType(row.EntityType, false);
      System.Type primaryGraphType = type == (System.Type) null ? (System.Type) null : EntityHelper.GetPrimaryGraphType((PXGraph) this, type);
      if (primaryGraphType != (System.Type) null)
      {
        ((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current.GraphType = primaryGraphType.FullName;
        ((PXSelectBase) this.AssigmentMap).Cache.SetStatus((object) ((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current, (PXEntryStatus) 1);
        ((PXSelectBase) this.AssigmentMap).Cache.IsDirty = true;
      }
    }
    ((PXSelectBase<Position>) this.PositionFilter).Current.MapID = row.AssignmentMapID;
    ((PXSelectBase) this.Nodes).Cache.AllowInsert = row.EntityType != null;
    bool flag = sender.GetStatus((object) row) == 2;
    PXUIFieldAttribute.SetEnabled<EPAssignmentMap.graphType>(sender, (object) row, flag);
    IOrderedEnumerable<PXSiteMapNode> source = this.GraphTypeList().ToList<PXSiteMapNode>().OrderBy<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (o => o.Title));
    PXStringListAttribute.SetLocalizable<EPAssignmentMap.graphType>(sender, (object) null, false);
    PXStringListAttribute.SetList<EPAssignmentMap.graphType>(sender, (object) row, source.Select<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (x => x.GraphType)).ToArray<string>(), source.Select<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (x => x.Title)).ToArray<string>());
  }

  protected virtual void EPAssignmentRule_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is EPAssignmentRule row))
      return;
    PXCache pxCache = sender;
    EPAssignmentRule epAssignmentRule = row;
    int? condition = row.Condition;
    int num;
    if (condition.GetValueOrDefault() != 11)
    {
      condition = row.Condition;
      num = condition.GetValueOrDefault() != 12 ? 1 : 0;
    }
    else
      num = 0;
    PXUIFieldAttribute.SetEnabled<EPAssignmentRule.fieldValue>(pxCache, (object) epAssignmentRule, num != 0);
  }

  protected virtual void EPAssignmentRule_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is EPAssignmentRule row))
      return;
    if (!((PXGraph) this).IsImport)
    {
      row.AssignmentRouteID = ((PXSelectBase<Position>) this.PositionFilter).Current.ItemID;
    }
    else
    {
      int? routeItemId = ((PXSelectBase<Position>) this.PositionFilter).Current.RouteItemID;
      if (!routeItemId.HasValue)
        return;
      routeItemId = ((PXSelectBase<Position>) this.PositionFilter).Current.RouteItemID;
      int? assignmentRouteId = row.AssignmentRouteID;
      if (routeItemId.GetValueOrDefault() == assignmentRouteId.GetValueOrDefault() & routeItemId.HasValue == assignmentRouteId.HasValue)
        return;
      row.AssignmentRouteID = ((PXSelectBase<Position>) this.PositionFilter).Current.RouteItemID;
      ((PXSelectBase<EPAssignmentRoute>) this.Items).Current = ((PXSelectBase<EPAssignmentRoute>) this.Items).Locate(new EPAssignmentRoute()
      {
        AssignmentMapID = ((PXSelectBase<Position>) this.PositionFilter).Current.MapID,
        AssignmentRouteID = row.AssignmentRouteID
      });
    }
  }

  protected virtual void EPAssignmentRule_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    EPAssignmentRule oldRow = e.OldRow as EPAssignmentRule;
    EPAssignmentRule row1 = e.Row as EPAssignmentRule;
    if (oldRow == null || row1 == null)
      return;
    if (!string.Equals(row1.Entity, oldRow.Entity, StringComparison.OrdinalIgnoreCase))
      row1.FieldName = row1.FieldValue = (string) null;
    if (!string.Equals(row1.FieldName, oldRow.FieldName, StringComparison.OrdinalIgnoreCase))
      row1.FieldValue = (string) null;
    EPAssignmentRule row2 = e.Row as EPAssignmentRule;
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
    row1.FieldValue = (string) null;
label_9:
    if (row1.FieldValue != null)
      return;
    PXFieldState stateExt = sender.GetStateExt<EPAssignmentRule.fieldValue>((object) row1) as PXFieldState;
    row1.FieldValue = stateExt == null || stateExt.Value == null ? (string) null : stateExt.Value.ToString();
  }

  protected virtual void EPAssignmentRule_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    ((PXSelectBase) this.Items).Cache.IsDirty = true;
  }

  protected virtual void EPAssignmentRule_Entity_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current == null)
      return;
    e.ReturnState = (object) this.CreateFieldStateForEntity(e.ReturnValue, ((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current.EntityType, ((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current.GraphType);
  }

  protected virtual void EPAssignmentRule_FieldName_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is EPAssignmentRule row) || row.Entity == null)
      return;
    e.ReturnState = (object) this.CreateFieldStateForFieldName(e.ReturnState, ((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current.EntityType, ((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current.GraphType, row.Entity);
  }

  protected virtual void EPAssignmentRule_FieldValue_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is EPAssignmentRule row) || string.IsNullOrEmpty(row.FieldName))
      return;
    int? condition = row.Condition;
    if (!condition.HasValue)
      return;
    condition = row.Condition;
    if (condition.Value == 11)
      return;
    condition = row.Condition;
    if (condition.Value == 12 || (sender.GetStateExt<EPAssignmentRule.fieldName>((object) row) is PXFieldState stateExt ? (stateExt.ErrorLevel != 4 ? 1 : 0) : 1) == 0)
      return;
    PXFieldState stateForFieldValue = this.CreateFieldStateForFieldValue(e.ReturnState, ((PXSelectBase<EPAssignmentMap>) this.AssigmentMap).Current.EntityType, row.Entity, row.FieldName);
    if (stateForFieldValue == null)
      return;
    e.ReturnState = (object) stateForFieldValue;
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

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
  }

  public virtual int ExecuteInsert(string viewName, IDictionary values, params object[] parameters)
  {
    return ((PXGraph) this).ExecuteInsert(viewName, values, parameters);
  }

  private PXFieldState CreateFieldStateForEntity(
    object returnState,
    string entityType,
    string graphType)
  {
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    System.Type type1 = GraphHelper.GetType(entityType);
    if (type1 != (System.Type) null)
    {
      System.Type type2 = EntityHelper.GetPrimaryGraphType((PXGraph) this, type1);
      if (!string.IsNullOrEmpty(graphType))
        type2 = GraphHelper.GetType(graphType);
      if (type2 == (System.Type) null)
      {
        PXCacheNameAttribute[] customAttributes = (PXCacheNameAttribute[]) type1.GetCustomAttributes(typeof (PXCacheNameAttribute), true);
        if (type1.IsSubclassOf(typeof (CSAnswers)))
        {
          stringList1.Add(type1.FullName);
          stringList2.Add(customAttributes.Length != 0 ? ((PXNameAttribute) customAttributes[0]).Name : type1.Name);
        }
      }
      else
      {
        foreach (CacheEntityItem cacheEntityItem in EMailSourceHelper.TemplateEntity((PXGraph) this, (string) null, type1.FullName, type2.FullName))
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

  private PXFieldState CreateFieldStateForFieldName(
    object returnState,
    string entityType,
    string graphType,
    string cacheName)
  {
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    System.Type type1 = GraphHelper.GetType(entityType);
    if (type1 != (System.Type) null)
    {
      System.Type type2 = EntityHelper.GetPrimaryGraphType((PXGraph) this, type1);
      if (!string.IsNullOrEmpty(graphType))
        type2 = GraphHelper.GetType(graphType);
      string str = (string) null;
      if (type2 != (System.Type) null)
      {
        foreach (CacheEntityItem cacheEntityItem in EMailSourceHelper.TemplateEntity((PXGraph) this, (string) null, type1.FullName, type2.FullName))
        {
          if (cacheEntityItem.SubKey == cacheName)
          {
            str = cacheEntityItem.Key;
            break;
          }
        }
      }
      Dictionary<string, string> source = new Dictionary<string, string>();
      foreach (CacheEntityItem cacheEntityItem in EMailSourceHelper.TemplateEntity((PXGraph) this, str, type1.FullName, type2 != (System.Type) null ? type2.FullName : (string) null, false))
        source[cacheEntityItem.SubKey] = cacheEntityItem.Name;
      foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) source.OrderBy<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (i => i.Value)))
      {
        if (keyValuePair.Key.EndsWith("_Attributes") || !keyValuePair.Key.Contains<char>('_'))
        {
          stringList1.Add(keyValuePair.Key);
          stringList2.Add(keyValuePair.Value);
        }
      }
    }
    return PXStringState.CreateInstance(returnState, new int?(60), new bool?(), "FieldName", new bool?(false), new int?(1), (string) null, stringList1.ToArray(), stringList2.ToArray(), new bool?(false), (string) null, (string[]) null);
  }

  private PXFieldState CreateFieldStateForFieldValue(
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
    pxFieldState.DescriptionName = (string) null;
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
        pxFieldState.Value = obj;
      }
      else
        pxFieldState.Value = returnState;
      pxFieldState.Enabled = true;
    }
    return attributes != null && attributes.FirstOrDefault<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a.GetType().IsSubclassOf(typeof (PXIntListAttribute)))) is PXIntListAttribute ? pxFieldState : ((pxFieldState is PXStringState pxStringState1 ? pxStringState1.AllowedValues : (string[]) null) != null ? PXStringState.CreateInstance(((PXFieldState) (pxFieldState as PXStringState)).Value, new int?(pxFieldState.Length), new bool?(false), pxFieldState.Name, new bool?(pxFieldState.PrimaryKey), new int?(pxFieldState.Required.GetValueOrDefault() ? 1 : (!pxFieldState.Required.HasValue ? 0 : -1)), pxFieldState is PXStringState pxStringState2 ? pxStringState2.InputMask : (string) null, pxFieldState is PXStringState pxStringState3 ? pxStringState3.AllowedValues : (string[]) null, pxFieldState is PXStringState pxStringState4 ? pxStringState4.AllowedLabels : (string[]) null, new bool?(true), pxFieldState is PXStringState pxStringState5 ? (string) ((PXFieldState) pxStringState5).DefaultValue : (string) (object) null, (string[]) null) : pxFieldState.CreateInstance(pxFieldState.DataType, new bool?(pxFieldState.PrimaryKey), new bool?(pxFieldState.Nullable), new int?(pxFieldState.Required.GetValueOrDefault() ? 1 : (!pxFieldState.Required.HasValue ? 0 : -1)), new int?(pxFieldState.Precision), new int?(pxFieldState.Length), pxFieldState.DefaultValue, fieldName, pxFieldState.DescriptionName, pxFieldState.DisplayName, pxFieldState.Error, pxFieldState.ErrorLevel, new bool?(true), new bool?(true), new bool?(false), (PXUIVisibility) 3, pxFieldState.ViewName, pxFieldState.FieldList, pxFieldState.HeaderList));
  }

  private PXFieldState GetPXFieldState(System.Type cachetype, string fieldName)
  {
    PXCache cach = ((PXGraph) this).Caches[cachetype];
    PXDBAttributeAttribute.Activate(cach);
    return cach.GetStateExt((object) null, fieldName) as PXFieldState;
  }

  private static int Comparison(EPAssignmentRoute x, EPAssignmentRoute y)
  {
    return x.Sequence.Value.CompareTo(y.Sequence.Value);
  }

  private IList<EPAssignmentRoute> GetSortedItems()
  {
    List<EPAssignmentRoute> sortedItems = new List<EPAssignmentRoute>();
    if (((PXSelectBase<EPAssignmentRoute>) this.CurrentItem).Current == null)
      return (IList<EPAssignmentRoute>) sortedItems;
    foreach (EPAssignmentRoute epAssignmentRoute in ((PXSelectBase) this.Items).Cache.Cached)
    {
      int? parent1 = epAssignmentRoute.Parent;
      int? parent2 = ((PXSelectBase<EPAssignmentRoute>) this.CurrentItem).Current.Parent;
      if (parent1.GetValueOrDefault() == parent2.GetValueOrDefault() & parent1.HasValue == parent2.HasValue && ((PXSelectBase) this.Items).Cache.GetStatus((object) epAssignmentRoute) != 3 && ((PXSelectBase) this.Items).Cache.GetStatus((object) epAssignmentRoute) != 4)
        sortedItems.Add(epAssignmentRoute);
    }
    sortedItems.Sort(new System.Comparison<EPAssignmentRoute>(EPAssignmentMaint.Comparison));
    return (IList<EPAssignmentRoute>) sortedItems;
  }
}

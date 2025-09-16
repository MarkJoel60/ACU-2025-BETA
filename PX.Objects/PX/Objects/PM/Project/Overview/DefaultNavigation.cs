// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Project.Overview.DefaultNavigation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM.Project.Overview;

public static class DefaultNavigation
{
  private static readonly DefaultNavigation.NavigationNodeWrapper[] Tree = new DefaultNavigation.NavigationNodeWrapper[21]
  {
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.ProjectAccounting, "Overview", (string) null, "Summary"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.ProjectAccounting, "Financials", (string) null, "Financials", "ProjectTasks"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.ProjectAccounting, "ProjectTasks", "Financials", "Project Tasks"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.ProjectAccounting, "Budget", "Financials", "Project Budget"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.ChangeOrders, "ChangeOrders", "Financials", "Change Orders"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.Construction, "CostProjections", "Financials", "Cost Projections"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.ProjectAccounting, "ProFormaInvoices", "Financials", "Pro Forma Invoices"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.Construction, "SubcontractsAndPurchasing", (string) null, "Subcontracts and Purchases", "Subcontracts"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.Construction, "Subcontracts", "SubcontractsAndPurchasing", "Subcontracts"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.Inventory, "PurchaseOrders", "SubcontractsAndPurchasing", "Purchase Orders"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.ProjectSpecificInventory, "ProjectInventory", "SubcontractsAndPurchasing", "Project Inventory"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.ProjectManagement, "ProjectManagement", (string) null, "Project Management", "DailyFieldReports"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.ProjectManagement, "DailyFieldReports", "ProjectManagement", "Daily Field Reports"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.ProjectManagement, "RequestsForInformation", "ProjectManagement", "Requests For Information"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.ProjectManagement, "ProjectIssues", "ProjectManagement", "Project Issues"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.ProjectManagement, "DrawingLogs", "ProjectManagement", "Drawing Logs"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.ProjectManagement, "PhotoLogs", "ProjectManagement", "Photo Logs"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.ProjectManagement, "Submittals", "ProjectManagement", "Submittals"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.ProjectManagement, "Compliance", "ProjectManagement", "Compliance"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.Manufacturing, "ProductionAndServices", (string) null, "Production", "ProductionOrders"),
    new DefaultNavigation.NavigationNodeWrapper(ProjectFeatureSet.Manufacturing, "ProductionOrders", "ProductionAndServices", "Production Orders")
  };

  public static NavigationTreeViewNode[] GetNavigationTree()
  {
    ProjectFeatureSet currentFeatureSet = FeatureSetHelper.GetCurrentProjectFeatureSet();
    return ((IEnumerable<DefaultNavigation.NavigationNodeWrapper>) DefaultNavigation.Tree).Where<DefaultNavigation.NavigationNodeWrapper>((Func<DefaultNavigation.NavigationNodeWrapper, bool>) (x => FeatureSetHelper.IsProjectFeatureIn(currentFeatureSet, x.Features))).Select<DefaultNavigation.NavigationNodeWrapper, NavigationTreeViewNode>(new Func<DefaultNavigation.NavigationNodeWrapper, NavigationTreeViewNode>(DefaultNavigation.ToNavigation)).ToArray<NavigationTreeViewNode>();
  }

  private static NavigationTreeViewNode ToNavigation(DefaultNavigation.NavigationNodeWrapper node)
  {
    return new NavigationTreeViewNode()
    {
      Key = node.Key,
      ParentKey = node.ParentKey,
      Name = node.Name,
      Navigation = node.Navigation
    };
  }

  private class NavigationNodeWrapper
  {
    /// <summary>The required set of available features</summary>
    public ProjectFeatureSet Features { get; private set; }

    public string Key { get; private set; }

    public string ParentKey { get; private set; }

    public string Name { get; private set; }

    public string Navigation { get; private set; }

    public NavigationNodeWrapper(
      ProjectFeatureSet features,
      string key,
      string parentKey,
      string name,
      string navigation)
    {
      this.Features = features;
      this.Key = key;
      this.ParentKey = parentKey;
      this.Name = name;
      this.Navigation = navigation;
    }

    public NavigationNodeWrapper(
      ProjectFeatureSet features,
      string key,
      string parentKey,
      string name)
      : this(features, key, parentKey, name, key)
    {
    }
  }
}

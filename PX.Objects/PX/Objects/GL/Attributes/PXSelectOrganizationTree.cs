// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Attributes.PXSelectOrganizationTree
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL.DAC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.Attributes;

public class PXSelectOrganizationTree : PXSelectBase<BranchItem>
{
  public bool OnlyActive = true;

  [InjectDependency]
  private ICurrentUserInformationProvider _currentUserInformationProvider { get; set; }

  public PXSelectOrganizationTree(PXGraph graph)
  {
    ((PXSelectBase) this)._Graph = graph;
    PXGraph graph1 = ((PXSelectBase) this)._Graph;
    PXSelectOrganizationTree organizationTree = this;
    // ISSUE: virtual method pointer
    PXSelectDelegate<string> handler = new PXSelectDelegate<string>((object) organizationTree, __vmethodptr(organizationTree, tree));
    ((PXSelectBase) this).View = PXSelectOrganizationTree.CreateView(graph1, (Delegate) handler);
  }

  public PXSelectOrganizationTree(PXGraph graph, Delegate handler)
  {
    ((PXSelectBase) this)._Graph = graph;
    ((PXSelectBase) this).View = PXSelectOrganizationTree.CreateView(((PXSelectBase) this)._Graph, handler);
  }

  private static PXView CreateView(PXGraph graph, Delegate handler)
  {
    return new PXView(graph, false, (BqlCommand) new Search<BranchItem.bAccountID, Where<BranchItem.acctCD, Equal<Argument<string>>>, OrderBy<Asc<BranchItem.acctCD>>>(), handler);
  }

  public virtual IEnumerable tree([PXString] string AcctCD)
  {
    List<BranchItem> branchItemList = new List<BranchItem>();
    this.AddOrganizations(branchItemList, (PXAccess.MasterCollection.Organization) null, this._currentUserInformationProvider.GetOrganizations(this.OnlyActive, false).Where<PXAccess.MasterCollection.Organization>((Func<PXAccess.MasterCollection.Organization, bool>) (_ =>
    {
      bool? deletedDatabaseRecord = ((PXAccess.Organization) _).DeletedDatabaseRecord;
      bool flag = false;
      return deletedDatabaseRecord.GetValueOrDefault() == flag & deletedDatabaseRecord.HasValue;
    })));
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) branchItemList);
    pxDelegateResult.IsResultFiltered = true;
    pxDelegateResult.IsResultSorted = PXView.Searches.Length == 0 || PXView.Searches[0] == null;
    pxDelegateResult.IsResultTruncated = false;
    return (IEnumerable) pxDelegateResult;
  }

  private void AddOrganizations(
    List<BranchItem> result,
    PXAccess.MasterCollection.Organization masterOrganization,
    IEnumerable<PXAccess.MasterCollection.Organization> organizations)
  {
    foreach (PXAccess.MasterCollection.Organization organization1 in organizations.Where<PXAccess.MasterCollection.Organization>((Func<PXAccess.MasterCollection.Organization, bool>) (_ =>
    {
      bool? deletedDatabaseRecord = ((PXAccess.Organization) _).DeletedDatabaseRecord;
      bool flag = false;
      return deletedDatabaseRecord.GetValueOrDefault() == flag & deletedDatabaseRecord.HasValue;
    })))
    {
      PXAccess.MasterCollection.Organization organization = organization1;
      BranchItem branchItem = this.CreateItem(((PXAccess.Organization) organization).BAccountID, ((PXAccess.Organization) organization).OrganizationCD, ((PXAccess.Organization) organization).OrganizationName, organization.IsGroup ? "group" : "organization", (int?) ((PXAccess.Organization) masterOrganization)?.BAccountID, primaryGroupID: (int?) ((PXAccess.Organization) organization.PrimaryParent)?.BAccountID);
      result.Add(branchItem);
      if (organization.IsGroup)
        this.AddOrganizations(result, organization, (IEnumerable<PXAccess.MasterCollection.Organization>) organization.ChildOrganizations);
      else if (!organization.IsSingle)
        result.AddRange(organization.ChildBranches.Where<PXAccess.MasterCollection.Branch>((Func<PXAccess.MasterCollection.Branch, bool>) (_ => !_.DeletedDatabaseRecord)).Select<PXAccess.MasterCollection.Branch, BranchItem>((Func<PXAccess.MasterCollection.Branch, BranchItem>) (branch => this.CreateItem(new int?(branch.BAccountID), branch.BranchCD, branch.BranchName, nameof (branch), ((PXAccess.Organization) organization).BAccountID))));
    }
  }

  private BranchItem CreateItem(
    int? id,
    string cd,
    string name,
    string type,
    int? parentId = null,
    bool? canSelect = true,
    int? primaryGroupID = null)
  {
    BranchItem branchItem1 = new BranchItem();
    branchItem1.BAccountID = id;
    branchItem1.AcctName = name;
    branchItem1.ParentBAccountID = parentId;
    branchItem1.PrimaryGroupID = primaryGroupID;
    branchItem1.Type = type;
    branchItem1.CanSelect = canSelect;
    BranchItem branchItem2 = branchItem1;
    object obj = (object) cd;
    ((PXSelectBase) this)._Graph.Caches[typeof (BranchItem)].RaiseFieldUpdating<BranchItem.acctCD>((object) branchItem2, ref obj);
    branchItem2.AcctCD = obj.ToString();
    return branchItem2;
  }
}

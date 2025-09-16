// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.AP1099ReportingPayerTreeSelect
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.DAC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

public class AP1099ReportingPayerTreeSelect : PXSelectOrganizationTree
{
  public AP1099ReportingPayerTreeSelect(PXGraph graph)
    : base(graph)
  {
  }

  public AP1099ReportingPayerTreeSelect(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public override IEnumerable tree([PXString] string AcctCD)
  {
    List<BranchItem> branchItemList1 = new List<BranchItem>();
    List<(PX.Objects.GL.Branch, BAccountR)> list = PXSelectBase<PX.Objects.GL.Branch, PXViewOf<PX.Objects.GL.Branch>.BasedOn<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<BAccountR>.On<BqlOperand<PX.Objects.GL.Branch.bAccountID, IBqlInt>.IsEqual<BAccountR.bAccountID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.active, Equal<True>>>>>.And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>.Config>.Select(this._Graph).AsEnumerable<PXResult<PX.Objects.GL.Branch>>().Cast<PXResult<PX.Objects.GL.Branch, BAccountR>>().Select<PXResult<PX.Objects.GL.Branch, BAccountR>, (PX.Objects.GL.Branch, BAccountR)>((Func<PXResult<PX.Objects.GL.Branch, BAccountR>, (PX.Objects.GL.Branch, BAccountR)>) (row => ((PX.Objects.GL.Branch) row, (BAccountR) row))).ToList<(PX.Objects.GL.Branch, BAccountR)>();
    foreach (PXResult<PX.Objects.GL.DAC.Organization, BAccountR> pxResult in PXSelectBase<PX.Objects.GL.DAC.Organization, PXViewOf<PX.Objects.GL.DAC.Organization>.BasedOn<SelectFromBase<PX.Objects.GL.DAC.Organization, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<BAccountR>.On<BqlOperand<PX.Objects.GL.DAC.Organization.bAccountID, IBqlInt>.IsEqual<BAccountR.bAccountID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.DAC.Organization.active, Equal<True>>>>, PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.DAC.Organization.reporting1099, Equal<True>>>>>.Or<BqlOperand<PX.Objects.GL.DAC.Organization.reporting1099ByBranches, IBqlBool>.IsEqual<True>>>>>.And<MatchWithOrganization<PX.Objects.GL.DAC.Organization.organizationID>>>>.Config>.Select(this._Graph))
    {
      PX.Objects.GL.DAC.Organization organization = (PX.Objects.GL.DAC.Organization) pxResult;
      BAccountR baccountR = (BAccountR) pxResult;
      bool flag = true;
      bool? reporting1099ByBranches = organization.Reporting1099ByBranches;
      if (reporting1099ByBranches.GetValueOrDefault())
      {
        flag = false;
        foreach ((PX.Objects.GL.Branch, BAccountR) tuple in list.Where<(PX.Objects.GL.Branch, BAccountR)>((Func<(PX.Objects.GL.Branch, BAccountR), bool>) (pair =>
        {
          int? organizationId1 = pair.Branch.OrganizationID;
          int? organizationId2 = organization.OrganizationID;
          return organizationId1.GetValueOrDefault() == organizationId2.GetValueOrDefault() & organizationId1.HasValue == organizationId2.HasValue && pair.Branch.Reporting1099.GetValueOrDefault();
        })))
        {
          flag = true;
          List<BranchItem> branchItemList2 = branchItemList1;
          BranchItem branchItem = new BranchItem();
          branchItem.BAccountID = tuple.Item2.BAccountID;
          branchItem.AcctCD = tuple.Item2.AcctCD;
          branchItem.AcctName = tuple.Item2.AcctName;
          branchItem.ParentBAccountID = organization.BAccountID;
          branchItemList2.Add(branchItem);
        }
      }
      if (flag)
      {
        List<BranchItem> branchItemList3 = branchItemList1;
        BranchItem branchItem = new BranchItem();
        branchItem.BAccountID = baccountR.BAccountID;
        branchItem.AcctCD = baccountR.AcctCD;
        branchItem.AcctName = baccountR.AcctName;
        reporting1099ByBranches = organization.Reporting1099ByBranches;
        int num;
        if (reporting1099ByBranches.GetValueOrDefault())
        {
          int? baccountId1 = baccountR.BAccountID;
          int? baccountId2 = organization.BAccountID;
          num = !(baccountId1.GetValueOrDefault() == baccountId2.GetValueOrDefault() & baccountId1.HasValue == baccountId2.HasValue) ? 1 : 0;
        }
        else
          num = 1;
        branchItem.CanSelect = new bool?(num != 0);
        branchItemList3.Add(branchItem);
      }
    }
    return (IEnumerable) branchItemList1;
  }
}

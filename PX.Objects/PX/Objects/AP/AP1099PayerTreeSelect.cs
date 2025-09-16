// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.AP1099PayerTreeSelect
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

#nullable disable
namespace PX.Objects.AP;

public class AP1099PayerTreeSelect : PXSelectOrganizationTree
{
  public AP1099PayerTreeSelect(PXGraph graph)
    : base(graph)
  {
  }

  public AP1099PayerTreeSelect(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public override IEnumerable tree([PXString] string AcctCD)
  {
    List<BranchItem> branchItemList = new List<BranchItem>();
    foreach (PXResult<BAccountR, PX.Objects.GL.Branch, PX.Objects.GL.DAC.Organization> pxResult in PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.GL.Branch>.On<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.bAccountID>>>, FbqlJoins.Inner<PX.Objects.GL.DAC.Organization>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>>>.Or<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<PX.Objects.GL.DAC.Organization.bAccountID>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.branchID, PX.Data.IsNull>>>, PX.Data.Or<BqlOperand<PX.Objects.GL.Branch.bAccountID, IBqlInt>.IsEqual<PX.Objects.GL.DAC.Organization.bAccountID>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.branchID, PX.Data.IsNotNull>>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.reporting1099ByBranches, IBqlBool>.IsEqual<True>>>>>, PX.Data.And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>, PX.Data.And<MatchWithOrganization<PX.Objects.GL.DAC.Organization.organizationID>>>, PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.branchID, PX.Data.IsNull>>>>.Or<BqlOperand<PX.Objects.GL.Branch.active, IBqlBool>.IsEqual<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.DAC.Organization.organizationID, PX.Data.IsNull>>>>.Or<BqlOperand<PX.Objects.GL.DAC.Organization.active, IBqlBool>.IsEqual<True>>>>>.Config>.Select(this._Graph))
    {
      BAccountR baccountR = (BAccountR) pxResult;
      PX.Objects.GL.Branch branch = (PX.Objects.GL.Branch) pxResult;
      PX.Objects.GL.DAC.Organization organization = (PX.Objects.GL.DAC.Organization) pxResult;
      BranchItem branchItem1 = new BranchItem();
      branchItem1.BAccountID = baccountR.BAccountID;
      branchItem1.AcctCD = baccountR.AcctCD;
      branchItem1.AcctName = baccountR.AcctName;
      branchItem1.CanSelect = new bool?(true);
      BranchItem branchItem2 = branchItem1;
      int? baccountId1;
      int? baccountId2;
      if (branch != null)
      {
        baccountId1 = branch.BAccountID;
        if (baccountId1.HasValue)
        {
          baccountId1 = organization.BAccountID;
          baccountId2 = branch.BAccountID;
          if (!(baccountId1.GetValueOrDefault() == baccountId2.GetValueOrDefault() & baccountId1.HasValue == baccountId2.HasValue))
            branchItem2.ParentBAccountID = PXAccess.GetParentOrganization(branch.BranchID).BAccountID;
        }
      }
      BranchItem branchItem3 = branchItem2;
      int num;
      if (organization.Reporting1099ByBranches.GetValueOrDefault())
      {
        baccountId2 = branchItem2.BAccountID;
        baccountId1 = organization.BAccountID;
        num = !(baccountId2.GetValueOrDefault() == baccountId1.GetValueOrDefault() & baccountId2.HasValue == baccountId1.HasValue) ? 1 : 0;
      }
      else
        num = 1;
      bool? nullable = new bool?(num != 0);
      branchItem3.CanSelect = nullable;
      branchItemList.Add(branchItem2);
    }
    return (IEnumerable) branchItemList;
  }
}

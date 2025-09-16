// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.TrialBalanceImportOrganizationTreeSelect
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.DAC;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

public class TrialBalanceImportOrganizationTreeSelect : PXSelectOrganizationTree
{
  public TrialBalanceImportOrganizationTreeSelect(PXGraph graph)
    : base(graph)
  {
  }

  public TrialBalanceImportOrganizationTreeSelect(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public override IEnumerable tree([PXString] string AcctCD)
  {
    List<BranchItem> branchItemList = new List<BranchItem>();
    foreach (PXResult<PX.Objects.CR.BAccount, Branch, PX.Objects.GL.DAC.Organization> pxResult in PXSelectBase<PX.Objects.CR.BAccount, PXViewOf<PX.Objects.CR.BAccount>.BasedOn<SelectFromBase<PX.Objects.CR.BAccount, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<Branch>.On<BqlOperand<PX.Objects.CR.BAccount.bAccountID, IBqlInt>.IsEqual<Branch.bAccountID>>>, FbqlJoins.Inner<PX.Objects.GL.DAC.Organization>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>>>.Or<BqlOperand<PX.Objects.CR.BAccount.bAccountID, IBqlInt>.IsEqual<PX.Objects.GL.DAC.Organization.bAccountID>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Branch.branchID, IsNull>>>, Or<BqlOperand<Branch.bAccountID, IBqlInt>.IsEqual<PX.Objects.GL.DAC.Organization.bAccountID>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Branch.branchID, IsNotNull>>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.organizationType, IBqlString>.IsEqual<P.AsString>>>>>, And<MatchWithBranch<Branch.branchID>>>, And<MatchWithOrganization<PX.Objects.GL.DAC.Organization.organizationID>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Branch.branchID, IsNull>>>>.Or<BqlOperand<Branch.active, IBqlBool>.IsEqual<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.DAC.Organization.organizationID, IsNull>>>>.Or<BqlOperand<PX.Objects.GL.DAC.Organization.active, IBqlBool>.IsEqual<True>>>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[1]
    {
      (object) "Balancing"
    }))
    {
      PX.Objects.CR.BAccount baccount = PXResult<PX.Objects.CR.BAccount, Branch, PX.Objects.GL.DAC.Organization>.op_Implicit(pxResult);
      Branch branch = PXResult<PX.Objects.CR.BAccount, Branch, PX.Objects.GL.DAC.Organization>.op_Implicit(pxResult);
      PX.Objects.GL.DAC.Organization organization = PXResult<PX.Objects.CR.BAccount, Branch, PX.Objects.GL.DAC.Organization>.op_Implicit(pxResult);
      BranchItem branchItem1 = new BranchItem();
      branchItem1.BAccountID = baccount.BAccountID;
      branchItem1.AcctCD = baccount.AcctCD;
      branchItem1.AcctName = baccount.AcctName;
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
      if (organization.OrganizationType == "Balancing")
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

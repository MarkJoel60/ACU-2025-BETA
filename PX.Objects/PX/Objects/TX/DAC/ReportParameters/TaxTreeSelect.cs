// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.DAC.ReportParameters.TaxTreeSelect
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
namespace PX.Objects.TX.DAC.ReportParameters;

public class TaxTreeSelect : PXSelectOrganizationTree
{
  public TaxTreeSelect(PXGraph graph)
    : base(graph)
  {
  }

  public TaxTreeSelect(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public override IEnumerable tree([PXString] string AcctCD)
  {
    List<BranchItem> branchItemList = new List<BranchItem>();
    foreach (PXResult<BAccountR, PX.Objects.GL.Branch, PX.Objects.GL.DAC.Organization> pxResult in PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.GL.Branch>.On<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.bAccountID>>>, FbqlJoins.Inner<PX.Objects.GL.DAC.Organization>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>>>.Or<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<PX.Objects.GL.DAC.Organization.bAccountID>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.Branch.branchID, IsNull>>>, Or<BqlOperand<PX.Objects.GL.Branch.bAccountID, IBqlInt>.IsEqual<PX.Objects.GL.DAC.Organization.bAccountID>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.Branch.branchID, IsNotNull>>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.fileTaxesByBranches, IBqlBool>.IsEqual<True>>>>>, And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>, And<MatchWithOrganization<PX.Objects.GL.DAC.Organization.organizationID>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.Branch.branchID, IsNull>>>>.Or<BqlOperand<PX.Objects.GL.Branch.active, IBqlBool>.IsEqual<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.DAC.Organization.organizationID, IsNull>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.DAC.Organization.organizationType, NotEqual<OrganizationTypes.group>>>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.active, IBqlBool>.IsEqual<True>>>>>>.Config>.Select(((PXSelectBase) this)._Graph, Array.Empty<object>()))
    {
      BAccountR baccountR = PXResult<BAccountR, PX.Objects.GL.Branch, PX.Objects.GL.DAC.Organization>.op_Implicit(pxResult);
      PX.Objects.GL.Branch branch = PXResult<BAccountR, PX.Objects.GL.Branch, PX.Objects.GL.DAC.Organization>.op_Implicit(pxResult);
      PX.Objects.GL.DAC.Organization organization = PXResult<BAccountR, PX.Objects.GL.Branch, PX.Objects.GL.DAC.Organization>.op_Implicit(pxResult);
      BranchItem branchItem1 = new BranchItem();
      branchItem1.BAccountID = baccountR.BAccountID;
      branchItem1.AcctCD = baccountR.AcctCD;
      branchItem1.AcctName = baccountR.AcctName;
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
      if (organization.FileTaxesByBranches.GetValueOrDefault())
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

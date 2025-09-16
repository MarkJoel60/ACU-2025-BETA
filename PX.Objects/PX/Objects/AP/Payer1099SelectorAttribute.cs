// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Payer1099SelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.AP;

[PXDBInt]
[PXUIField(DisplayName = "Company/Branch")]
[PXDimensionSelector("BIZACCT", typeof (SearchFor<BAccountR.bAccountID>.In<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.GL.Branch>.On<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.bAccountID>>>, FbqlJoins.Inner<PX.Objects.GL.DAC.Organization>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>>>.Or<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<PX.Objects.GL.DAC.Organization.bAccountID>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.branchID, PX.Data.IsNotNull>>>, PX.Data.And<BqlOperand<PX.Objects.GL.DAC.Organization.reporting1099ByBranches, IBqlBool>.IsEqual<True>>>, PX.Data.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.branchID, PX.Data.IsNull>>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.reporting1099ByBranches, IBqlBool>.IsNotEqual<True>>>>>.Or<BqlOperand<PX.Objects.GL.Branch.bAccountID, IBqlInt>.IsEqual<PX.Objects.GL.DAC.Organization.bAccountID>>>>, PX.Data.And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>.And<MatchWithOrganization<PX.Objects.GL.DAC.Organization.organizationID>>>>), typeof (BAccountR.acctCD), new System.Type[] {typeof (BAccountR.acctCD), typeof (BAccountR.acctName), typeof (BAccountR.type)})]
public class Payer1099SelectorAttribute : PXEntityAttribute
{
  public Payer1099SelectorAttribute()
  {
    this.DescriptionField = typeof (BAccountR.acctName);
    this.Initialize();
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.DAC.ReportParameters.AP1099ReportParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;

#nullable enable
namespace PX.Objects.AP.DAC.ReportParameters;

public class AP1099ReportParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Organization(false)]
  public virtual int? OrganizationID { get; set; }

  [Branch(null, typeof (SearchFor<PX.Objects.GL.Branch.branchID>.In<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.DAC.Organization>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.reporting1099ByBranches, IBqlBool>.IsEqual<True>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.organizationID, Equal<BqlField<AP1099ReportParameters.organizationID, IBqlInt>.AsOptional.NoDefault>>>>>.And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>), false, false, false)]
  public int? BranchID { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "1099 Year")]
  [PXSelector(typeof (Search4<AP1099Year.finYear, Where<AP1099Year.organizationID, Equal<Optional2<AP1099ReportParameters.organizationID>>, Or<Optional2<AP1099ReportParameters.organizationID>, PX.Data.IsNull>>, PX.Data.Aggregate<GroupBy<AP1099Year.finYear>>>))]
  public virtual 
  #nullable disable
  string FinYear { get; set; }

  [Payer1099Selector]
  public virtual int? PayerBAccountID { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "1099 Year")]
  [PXSelector(typeof (Search5<AP1099Year.finYear, InnerJoin<PX.Objects.GL.DAC.Organization, PX.Data.On<BqlOperand<AP1099Year.organizationID, IBqlInt>.IsEqual<PX.Objects.GL.DAC.Organization.organizationID>>, InnerJoin<PX.Objects.GL.Branch, PX.Data.On<BqlOperand<PX.Objects.GL.DAC.Organization.organizationID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.organizationID>>>>, PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.DAC.Organization.bAccountID, Equal<BqlField<AP1099ReportParameters.payerBAccountID, IBqlInt>.AsOptional.NoDefault>>>>>.Or<BqlOperand<PX.Objects.GL.Branch.bAccountID, IBqlInt>.IsEqual<BqlField<AP1099ReportParameters.payerBAccountID, IBqlInt>.AsOptional.NoDefault>>>, PX.Data.Aggregate<GroupBy<AP1099Year.finYear>>>))]
  public virtual string FinYearByPayer { get; set; }

  [OrganizationTree(null, null, typeof (AP1099PayerTreeSelect), true, SelectionMode = BaseOrganizationTreeAttribute.SelectionModes.Branches)]
  public int? OrgBAccountID { get; set; }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AP1099ReportParameters.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AP1099ReportParameters.branchID>
  {
  }

  public abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AP1099ReportParameters.finYear>
  {
  }

  public abstract class payerBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AP1099ReportParameters.payerBAccountID>
  {
  }

  public abstract class finYearByPayer : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AP1099ReportParameters.finYearByPayer>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }
}

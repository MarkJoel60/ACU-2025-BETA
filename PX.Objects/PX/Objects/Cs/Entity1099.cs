// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Entity1099
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
using System;

#nullable enable
namespace PX.Objects.CS;

[PXProjection(typeof (SelectFromBase<PX.Objects.GL.DAC.Organization, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.GL.Branch>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.DAC.Organization.organizationID, Equal<PX.Objects.GL.Branch.organizationID>>>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.reporting1099ByBranches, IBqlBool>.IsEqual<True>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.DAC.Organization.active, Equal<True>>>>>.And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>))]
[PXHidden]
[Serializable]
public class Entity1099 : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Organization(true, BqlTable = typeof (PX.Objects.GL.DAC.Organization))]
  public virtual int? OrganizationID { get; set; }

  [Branch(null, null, true, true, true, BqlTable = typeof (PX.Objects.GL.Branch))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.GL.DAC.Organization.bAccountID))]
  public virtual int? OrganizationBAccountID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.GL.Branch.bAccountID))]
  public virtual int? BranchBAccountID { get; set; }

  [PXInt]
  [PXDBCalced(typeof (IIf<Where<BqlOperand<PX.Objects.GL.DAC.Organization.reporting1099ByBranches, IBqlBool>.IsEqual<True>>, PX.Objects.GL.Branch.bAccountID, PX.Objects.GL.DAC.Organization.bAccountID>), typeof (int?))]
  public virtual int? BAccountID { get; set; }

  public abstract class organizationID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  Entity1099.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Entity1099.branchID>
  {
  }

  public abstract class organizationBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Entity1099.organizationBAccountID>
  {
  }

  public abstract class branchBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Entity1099.branchBAccountID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Entity1099.bAccountID>
  {
  }
}

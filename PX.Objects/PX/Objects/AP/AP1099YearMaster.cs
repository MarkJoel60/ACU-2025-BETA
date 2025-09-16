// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.AP1099YearMaster
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
namespace PX.Objects.AP;

[Serializable]
public class AP1099YearMaster : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _VendorID;

  [PXDBString(4, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "1099 Year", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Search<AP1099Year.finYear, Where<AP1099Year.organizationID, Equal<Current<AP1099YearMaster.organizationID>>>>))]
  public virtual 
  #nullable disable
  string FinYear { get; set; }

  [Vendor(typeof (Search<Vendor.bAccountID, Where<Vendor.vendor1099, Equal<True>>>), DisplayName = "Vendor", DescriptionField = typeof (Vendor.acctName))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [Organization(false)]
  public virtual int? OrganizationID { get; set; }

  [Branch(null, typeof (SearchFor<PX.Objects.GL.Branch.branchID>.In<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.DAC.Organization>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.reporting1099ByBranches, IBqlBool>.IsEqual<True>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.organizationID, Equal<BqlField<AP1099YearMaster.organizationID, IBqlInt>.FromCurrent>>>>>.And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>), true, false, false)]
  [PXUIEnabled(typeof (Where<Selector<AP1099YearMaster.organizationID, PX.Objects.GL.DAC.Organization.reporting1099ByBranches>, Equal<True>>))]
  [PXUIRequired(typeof (Where<Selector<AP1099YearMaster.organizationID, PX.Objects.GL.DAC.Organization.reporting1099ByBranches>, Equal<True>>))]
  public virtual int? BranchID { get; set; }

  [OrganizationTree(typeof (AP1099YearMaster.organizationID), typeof (AP1099YearMaster.branchID), typeof (AP1099PayerTreeSelect), true, DisplayName = "Company/Branch", SelectionMode = BaseOrganizationTreeAttribute.SelectionModes.Branches)]
  public int? OrgBAccountID { get; set; }

  public abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AP1099YearMaster.finYear>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AP1099YearMaster.vendorID>
  {
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AP1099YearMaster.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AP1099YearMaster.branchID>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.RevalueFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.DAC;
using PX.Objects.GL.FinPeriods;
using System;

#nullable enable
namespace PX.Objects.CM;

[Serializable]
public class RevalueFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _BusinessDate;
  protected 
  #nullable disable
  string _FinPeriodID;
  protected DateTime? _CuryEffDate;
  protected string _CuryID;
  protected string _Description;
  protected Decimal? _TotalRevalued;

  [Organization(false, typeof (Search2<PX.Objects.GL.DAC.Organization.organizationID, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<PX.Objects.GL.Branch.organizationID>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>, And2<FeatureInstalled<FeaturesSet.branch>, And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>>))]
  public int? OrganizationID { get; set; }

  [BranchOfOrganization(typeof (RevalueFilter.organizationID), false, typeof (Search2<PX.Objects.GL.Branch.branchID, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>, CrossJoin<FeaturesSet>>, Where<FeaturesSet.branch, Equal<True>, And<PX.Objects.GL.DAC.Organization.organizationType, NotEqual<OrganizationTypes.withoutBranches>, And<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>>), null)]
  public int? BranchID { get; set; }

  [OrganizationTree(typeof (RevalueFilter.organizationID), typeof (RevalueFilter.branchID), null, false)]
  [PXUIRequired(typeof (FeatureInstalled<FeaturesSet.multipleBaseCurrencies>))]
  [PXUIField(DisplayName = "Company/Branch")]
  public int? OrgBAccountID { get; set; }

  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXFormula(typeof (GetOrganizationBaseCuryID<RevalueFilter.orgBAccountID>))]
  public string OrganizationBaseCuryID { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? BusinessDate
  {
    get => this._BusinessDate;
    set => this._BusinessDate = value;
  }

  [ClosedPeriod(null, typeof (RevalueFilter.businessDate), typeof (RevalueFilter.branchID), null, typeof (RevalueFilter.organizationID), null, null, true, true)]
  [PXUIField(DisplayName = "Fin. Period", Required = true)]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDBDate]
  [PXDefault(typeof (Search<MasterFinPeriod.endDate, Where<MasterFinPeriod.finPeriodID, Equal<Current<RevalueFilter.finPeriodID>>>>))]
  [PXUIField]
  public virtual DateTime? CuryEffDate
  {
    get => this._CuryEffDate;
    set => this._CuryEffDate = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXDefault]
  [PXUIField(DisplayName = "Currency")]
  [PXSelector(typeof (Search<Currency.curyID, Where<Currency.curyID, NotEqual<Current<RevalueFilter.organizationBaseCuryID>>, Or<Where<Current<RevalueFilter.organizationBaseCuryID>, IsNull, And<Not<Currency.curyID, EqualBaseCuryID<Current<AccessInfo.branchID>>>>>>>>))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Transaction Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBBaseCury(null, typeof (RevalueFilter.organizationBaseCuryID))]
  [PXUIField(DisplayName = "Revaluation Total", Enabled = false)]
  [CurySymbol(null, null, typeof (RevalueFilter.organizationBaseCuryID), null, null, null, null, true, true)]
  public virtual Decimal? TotalRevalued
  {
    get => this._TotalRevalued;
    set => this._TotalRevalued = value;
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RevalueFilter.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RevalueFilter.branchID>
  {
  }

  public abstract class orgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RevalueFilter.orgBAccountID>
  {
  }

  public abstract class organizationBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RevalueFilter.orgBAccountID>
  {
  }

  public abstract class businessDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RevalueFilter.businessDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RevalueFilter.finPeriodID>
  {
  }

  public abstract class curyEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RevalueFilter.curyEffDate>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RevalueFilter.curyID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RevalueFilter.description>
  {
  }

  public abstract class totalRevalued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RevalueFilter.totalRevalued>
  {
  }
}

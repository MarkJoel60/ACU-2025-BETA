// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.MISC1099EFileFilter
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
public class MISC1099EFileFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _FinYear;

  [Organization(true, typeof (Coalesce<SearchFor<PX.Objects.GL.DAC.Organization.organizationID>.In<SelectFromBase<PX.Objects.GL.DAC.Organization, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.DAC.Organization.organizationID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.organizationID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.branchID, Equal<BqlField<AccessInfo.branchID, IBqlInt>.FromCurrent>>>>, PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.DAC.Organization.reporting1099, Equal<True>>>>>.Or<BqlOperand<PX.Objects.GL.DAC.Organization.reporting1099ByBranches, IBqlBool>.IsEqual<True>>>>>.And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>, SearchFor<PX.Objects.GL.DAC.Organization.organizationID>.In<SelectFromBase<PX.Objects.GL.DAC.Organization, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.DAC.Organization.organizationID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.organizationID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.DAC.Organization.reporting1099, Equal<True>>>>>.Or<BqlOperand<PX.Objects.GL.DAC.Organization.reporting1099ByBranches, IBqlBool>.IsEqual<True>>>>>.And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>>), DisplayName = "Transmitter Company")]
  [PXRestrictor(typeof (PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.DAC.Organization.reporting1099, Equal<True>>>>>.Or<BqlOperand<PX.Objects.GL.DAC.Organization.reporting1099ByBranches, IBqlBool>.IsEqual<True>>>), "E-filing is available for companies that have the 1099-MISC Reporting Entity or Report 1099-MISC by Branches check boxes selected on the Company Details tab of the Companies (CS101500) form.", new System.Type[] {})]
  public virtual int? OrganizationID { get; set; }

  [Branch(null, typeof (SearchFor<PX.Objects.GL.Branch.branchID>.In<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.DAC.Organization>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.reporting1099ByBranches, IBqlBool>.IsEqual<True>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.organizationID, Equal<BqlField<MISC1099EFileFilter.organizationID, IBqlInt>.FromCurrent>>>>>.And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>), true, true, false, DisplayName = "Transmitter Branch")]
  [PXRestrictor(typeof (PX.Data.Where<BqlOperand<PX.Objects.GL.Branch.reporting1099, IBqlBool>.IsEqual<True>>), "E-filing is available for branches that have the 1099-MISC Reporting Entity check box selected on the Branch Details tab of the Branches (CS102000) form.", new System.Type[] {})]
  [PXUIEnabled(typeof (Where<Selector<MISC1099EFileFilter.organizationID, PX.Objects.GL.DAC.Organization.reporting1099ByBranches>, Equal<True>>))]
  [PXUIRequired(typeof (Where<Selector<MISC1099EFileFilter.organizationID, PX.Objects.GL.DAC.Organization.reporting1099ByBranches>, Equal<True>>))]
  public virtual int? BranchID { get; set; }

  [OrganizationTree(typeof (MISC1099EFileFilter.organizationID), typeof (MISC1099EFileFilter.branchID), typeof (AP1099ReportingPayerTreeSelect), true, DisplayName = "Transmitter", SelectionMode = BaseOrganizationTreeAttribute.SelectionModes.Branches)]
  public int? OrgBAccountID { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "1099 Year", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Search<AP1099Year.finYear, Where<AP1099Year.organizationID, Equal<Optional<MISC1099EFileFilter.organizationID>>>>))]
  public virtual string FinYear
  {
    get => this._FinYear;
    set => this._FinYear = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "File Format")]
  [MISC1099EFileFilter.fileFormat.List]
  [PXDefault("M")]
  public virtual string FileFormat { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Prepare For")]
  [MISC1099EFileFilter.include.List]
  [PXDefault("T")]
  public virtual string Include { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Include")]
  [MISC1099EFileFilter.box7.List]
  [PXDefault("AL")]
  [PXUIEnabled(typeof (PX.Data.Where<BqlOperand<MISC1099EFileFilter.finYear, IBqlString>.IsLess<MISC1099EFileFilter.finYear.nECAvailable1099Year>>))]
  public virtual string Box7 { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Prior Year")]
  public virtual bool? IsPriorYear { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Correction File")]
  public virtual bool? IsCorrectionReturn { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Last Filing")]
  public virtual bool? IsLastFiling { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<MISC1099EFileFilter.fileFormat, Equal<MISC1099EFileFilter.fileFormat.mISC>>>>>.Or<BqlOperand<MISC1099EFileFilter.finYear, IBqlString>.IsGreater<MISC1099EFileFilter.finYear.nECAvailable1099Year>>>))]
  [PXFormula(typeof (Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<MISC1099EFileFilter.fileFormat, Equal<MISC1099EFileFilter.fileFormat.nEC>>>>>.And<BqlOperand<MISC1099EFileFilter.finYear, IBqlString>.IsLessEqual<MISC1099EFileFilter.finYear.nECAvailable1099Year>>>, False>, MISC1099EFileFilter.reportingDirectSalesOnly>))]
  [PXUIField(DisplayName = "Direct Sales Only")]
  public virtual bool? ReportingDirectSalesOnly { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Test File")]
  public virtual bool? IsTestMode { get; set; }

  [PXDBString(100)]
  public virtual string CountryID { get; set; }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MISC1099EFileFilter.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  MISC1099EFileFilter.branchID>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MISC1099EFileFilter.finYear>
  {
    public const string NECAvailable1099Year = "2020";

    public class nECAvailable1099Year : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      MISC1099EFileFilter.finYear.nECAvailable1099Year>
    {
      public nECAvailable1099Year()
        : base("2020")
      {
      }
    }
  }

  public abstract class fileFormat : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MISC1099EFileFilter.fileFormat>
  {
    public const string MISC = "M";
    public const string NEC = "N";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "M", "N" }, new string[2]
        {
          "MISC",
          "NEC"
        })
      {
      }
    }

    public class mISC : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    MISC1099EFileFilter.fileFormat.mISC>
    {
      public mISC()
        : base("M")
      {
      }
    }

    public class nEC : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    MISC1099EFileFilter.fileFormat.nEC>
    {
      public nEC()
        : base("N")
      {
      }
    }
  }

  public abstract class include : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MISC1099EFileFilter.include>
  {
    public const string TransmitterOnly = "T";
    public const string AllMarkedOrganizations = "A";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "T", "A" }, new string[2]
        {
          "Transmitter Only",
          "All Marked Companies"
        })
      {
      }
    }

    public class transmitterOnly : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      MISC1099EFileFilter.include.transmitterOnly>
    {
      public transmitterOnly()
        : base("T")
      {
      }
    }

    public class allMarkedOrganizations : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      MISC1099EFileFilter.include.allMarkedOrganizations>
    {
      public allMarkedOrganizations()
        : base("A")
      {
      }
    }
  }

  public abstract class box7 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MISC1099EFileFilter.box7>
  {
    public const string Box7All = "AL";
    public const string Box7Equal = "EQ";
    public const string Box7NotEqual = "NE";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[3]{ "AL", "EQ", "NE" }, new string[3]
        {
          "All Boxes",
          "Only Nonemployee Compensation",
          "Except Nonemployee Compensation"
        })
      {
      }
    }

    public class box7All : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    MISC1099EFileFilter.box7.box7All>
    {
      public box7All()
        : base("AL")
      {
      }
    }

    public class box7Equal : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    MISC1099EFileFilter.box7.box7Equal>
    {
      public box7Equal()
        : base("EQ")
      {
      }
    }

    public class box7NotEqual : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      MISC1099EFileFilter.box7.box7NotEqual>
    {
      public box7NotEqual()
        : base("NE")
      {
      }
    }

    public class box7Nbr : BqlType<
    #nullable enable
    IBqlShort, short>.Constant<
    #nullable disable
    MISC1099EFileFilter.box7.box7Nbr>
    {
      public box7Nbr()
        : base((short) 7)
      {
      }
    }
  }

  public abstract class isPriorYear : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  MISC1099EFileFilter.isPriorYear>
  {
  }

  public abstract class isCorrectionReturn : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    MISC1099EFileFilter.isCorrectionReturn>
  {
  }

  public abstract class isLastFiling : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    MISC1099EFileFilter.isLastFiling>
  {
  }

  public abstract class reportingDirectSalesOnly : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    MISC1099EFileFilter.reportingDirectSalesOnly>
  {
  }

  public abstract class isTestMode : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  MISC1099EFileFilter.isTestMode>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MISC1099EFileFilter.countryID>
  {
  }
}

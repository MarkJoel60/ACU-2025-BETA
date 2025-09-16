// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Turnover.INTurnoverCalcFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods.TableDefinition;

#nullable enable
namespace PX.Objects.IN.Turnover;

[PXCacheName("Manage Turnover History Filter")]
public class INTurnoverCalcFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [INTurnoverCalcFilter.action.List]
  [PXDBString(4)]
  [PXUIField(DisplayName = "Action", Required = true)]
  [PXDefault("NONE")]
  public virtual 
  #nullable disable
  string Action { get; set; }

  [Organization(false, Required = false)]
  public virtual int? OrganizationID { get; set; }

  [BranchOfOrganization(typeof (INTurnoverCalcFilter.organizationID), false, null, null)]
  public virtual int? BranchID { get; set; }

  [OrganizationTree(typeof (INTurnoverCalcFilter.organizationID), typeof (INTurnoverCalcFilter.branchID), null, false, Required = true)]
  public virtual int? OrgBAccountID { get; set; }

  [PXDBBool]
  public bool? UseMasterCalendar { get; set; }

  [AnyPeriodFilterable(null, null, typeof (INTurnoverCalcFilter.branchID), null, typeof (INTurnoverCalcFilter.organizationID), typeof (INTurnoverCalcFilter.useMasterCalendar), null, false, null, null)]
  [PXUIField(DisplayName = "From Period", Required = false)]
  public virtual string FromPeriodID { get; set; }

  [AnyPeriodFilterable(null, null, typeof (INTurnoverCalcFilter.branchID), null, typeof (INTurnoverCalcFilter.organizationID), typeof (INTurnoverCalcFilter.useMasterCalendar), null, false, null, null)]
  [PXUIField(DisplayName = "To Period", Required = false)]
  [PXDefault(typeof (Coalesce<Search<FinPeriod.finPeriodID, Where<FinPeriod.organizationID, Equal<Current<INTurnoverCalcFilter.organizationID>>, And<FinPeriod.startDate, LessEqual<Current<AccessInfo.businessDate>>>>, OrderBy<Desc<FinPeriod.startDate, Desc<FinPeriod.finPeriodID>>>>, Search<FinPeriod.finPeriodID, Where<FinPeriod.organizationID, Equal<Zero>, And<FinPeriod.startDate, LessEqual<Current<AccessInfo.businessDate>>>>, OrderBy<Desc<FinPeriod.startDate, Desc<FinPeriod.finPeriodID>>>>>))]
  public virtual string ToPeriodID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Calculate for Last N Period(s)", Visible = false)]
  public virtual int? NumberOfPeriods { get; set; }

  [INTurnoverCalcFilter.calculateBy.List]
  [PXDBString(6)]
  [PXUIField(DisplayName = "Calculate By")]
  [PXDefault(typeof (Switch<Case<Where<BqlOperand<INTurnoverCalcFilter.action, IBqlString>.IsEqual<INTurnoverCalcFilter.action.calculate>>, INTurnoverCalcFilter.calculateBy.range>, Null>))]
  public virtual string CalculateBy { get; set; }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTurnoverCalcFilter.action>
  {
    public const string None = "NONE";
    public const string Calculate = "CALC";
    public const string Delete = "DEL";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new (string, string)[3]
        {
          ("NONE", "<SELECT>"),
          ("CALC", "Calculate Turnover"),
          ("DEL", "Delete Records")
        })
      {
      }
    }

    public class calculate : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      INTurnoverCalcFilter.action.calculate>
    {
      public calculate()
        : base("CALC")
      {
      }
    }
  }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTurnoverCalcFilter.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverCalcFilter.branchID>
  {
  }

  public abstract class orgBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTurnoverCalcFilter.orgBAccountID>
  {
  }

  public abstract class useMasterCalendar : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INTurnoverCalcFilter.useMasterCalendar>
  {
  }

  public abstract class fromPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTurnoverCalcFilter.fromPeriodID>
  {
  }

  public abstract class toPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTurnoverCalcFilter.toPeriodID>
  {
  }

  public abstract class numberOfPeriods : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTurnoverCalcFilter.numberOfPeriods>
  {
  }

  public abstract class calculateBy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTurnoverCalcFilter.action>
  {
    public const string None = "NONE";
    public const string Period = "PERIOD";
    public const string Year = "YEAR";
    public const string Range = "RANGE";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new (string, string)[4]
        {
          ("NONE", "<SELECT>"),
          ("PERIOD", "Period"),
          ("YEAR", "Year"),
          ("RANGE", "Selected Range")
        })
      {
      }
    }

    public class range : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INTurnoverCalcFilter.calculateBy.range>
    {
      public range()
        : base("RANGE")
      {
      }
    }
  }
}

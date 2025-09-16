// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.FinPeriodClosingProcessBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.SQLTree;
using PX.Objects.CS;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL.Formula;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.Common;

public abstract class FinPeriodClosingProcessBase : PXGraph
{
  public abstract 
  #nullable disable
  string ClosedFieldName { get; }

  protected virtual string FeatureName => (string) null;

  public virtual bool NeedValidate
  {
    get => string.IsNullOrEmpty(this.FeatureName) || PXAccess.FeatureInstalled(this.FeatureName);
  }

  protected abstract FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule[] CheckingRules { get; }

  public abstract void ClosePeriod(FinPeriod finPeriod);

  public abstract void ClosePeriods(List<FinPeriod> finPeriods);

  public abstract void ReopenPeriod(FinPeriod finPeriod);

  public abstract void ProcessPeriods(List<FinPeriod> finPeriods);

  public abstract List<(string ReportID, IPXResultset ReportData)> GetReportsData(
    int? organizationiD,
    string fromPeriodID,
    string toPeriodID);

  public abstract bool IsUnclosablePeriod(FinPeriod finPeriod);

  [Serializable]
  public class FinPeriodClosingProcessParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [Organization(true, typeof (Switch<Case<Where<Not<FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>>>, OrganizationOfBranch<Current<AccessInfo.branchID>>>>))]
    public virtual int? OrganizationID { get; set; }

    [PXInt]
    [PXFormula(typeof (IIf<Where<FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>>, FinPeriod.organizationID.masterValue, FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.organizationID>))]
    public virtual int? FilterOrganizationID { get; set; }

    [PXString(10)]
    [PXUIField(DisplayName = "Action")]
    [FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action.FullList]
    [PXDefault("Close")]
    public virtual string Action { get; set; }

    [PXString(4, IsFixed = true)]
    [PXFormula(typeof (Default<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.filterOrganizationID, FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action>))]
    public virtual string FirstYear { get; set; }

    [PXString(4, IsFixed = true)]
    [PXFormula(typeof (Default<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.filterOrganizationID, FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action>))]
    public virtual string LastYear { get; set; }

    [PXString(4, IsFixed = true)]
    [PXFormula(typeof (IIf2<Where<IsDirectFinPeriodAction<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action>, Equal<True>>, FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.firstYear, FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.lastYear>))]
    [PXUIEnabled(typeof (Where<IsDirectFinPeriodAction<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action>, NotEqual<True>>))]
    [PXUIField(DisplayName = "From Year", Required = true)]
    [PXDefault]
    [PXSelector(typeof (Search<FinYear.year, Where<FinYear.organizationID, Equal<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.filterOrganizationID>>, And2<Where<FinYear.year, GreaterEqual<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.firstYear>>, Or<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.firstYear>, IsNull>>, And<Where<FinYear.year, LessEqual<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.lastYear>>, Or<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.lastYear>, IsNull>>>>>>))]
    public virtual string FromYear { get; set; }

    [PXString(4, IsFixed = true)]
    [PXFormula(typeof (IIf2<Where<IsDirectFinPeriodAction<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action>, Equal<True>>, FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.firstYear, FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.lastYear>))]
    [PXUIEnabled(typeof (Where<IsDirectFinPeriodAction<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action>, Equal<True>>))]
    [PXUIField(DisplayName = "To Year", Required = true)]
    [PXDefault]
    [PXSelector(typeof (Search<FinYear.year, Where<FinYear.organizationID, Equal<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.filterOrganizationID>>, And2<Where<FinYear.year, GreaterEqual<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.firstYear>>, Or<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.firstYear>, IsNull>>, And<Where<FinYear.year, LessEqual<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.lastYear>>, Or<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.lastYear>, IsNull>>>>>>))]
    public virtual string ToYear { get; set; }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.organizationID>
    {
    }

    public abstract class filterOrganizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.filterOrganizationID>
    {
    }

    public abstract class action : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action>
    {
      public class RestrictedListAttribute : PXStringListAttribute
      {
        public RestrictedListAttribute()
          : base(new string[1]{ "Close" }, new string[1]
          {
            "Close"
          })
        {
        }
      }

      public class FullListAttribute : PXStringListAttribute
      {
        public FullListAttribute()
          : base(new string[2]{ "Close", "Reopen" }, new string[2]
          {
            "Close",
            "Reopen"
          })
        {
        }
      }

      public class close : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action.close>
      {
        public close()
          : base("Close")
        {
        }
      }

      public class reopen : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action.reopen>
      {
        public reopen()
          : base("Reopen")
        {
        }
      }
    }

    public abstract class firstYear : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.firstYear>
    {
    }

    public abstract class lastYear : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.lastYear>
    {
    }

    public abstract class fromYear : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.fromYear>
    {
    }

    public abstract class toYear : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.toYear>
    {
    }
  }

  [Serializable]
  public class UnprocessedObjectsQueryParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt]
    public virtual int? OrganizationID { get; set; }

    [PXString(6, IsFixed = true)]
    public virtual string FromFinPeriodID { get; set; }

    [PXString(6, IsFixed = true)]
    public virtual string ToFinPeriodID { get; set; }

    [PXDate]
    public virtual DateTime? FromFinPeriodStartDate { get; set; }

    [PXDate]
    public virtual DateTime? ToFinPeriodEndDate { get; set; }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.organizationID>
    {
    }

    public abstract class fromFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.fromFinPeriodID>
    {
    }

    public abstract class toFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.toFinPeriodID>
    {
    }

    public abstract class fromFinPeriodStartDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.fromFinPeriodStartDate>
    {
    }

    public abstract class toFinPeriodEndDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.toFinPeriodEndDate>
    {
    }
  }

  public class UnprocessedObjectsCheckingRule
  {
    public string ReportID { get; set; }

    public BqlCommand CheckCommand { get; set; }

    public string ErrorMessage { get; set; }

    public Type[] MessageParameters { get; set; } = new Type[0];
  }

  public class WhereFinPeriodInRange<TFinPeriodID, TOrganizationID> : 
    IBqlWhere,
    IBqlUnary,
    IBqlCreator,
    IBqlVerifier
    where TFinPeriodID : IBqlOperand
    where TOrganizationID : IBqlOperand
  {
    private static IBqlCreator Where
    {
      get
      {
        return PXAccess.FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>() ? (IBqlCreator) new Where<TFinPeriodID, GreaterEqual<Current<FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.fromFinPeriodID>>, And<TFinPeriodID, LessEqual<Current<FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.toFinPeriodID>>>>() : (IBqlCreator) new Where<TFinPeriodID, GreaterEqual<Current<FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.fromFinPeriodID>>, And<TFinPeriodID, LessEqual<Current<FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.toFinPeriodID>>, And<Where<TOrganizationID, Equal<Current<FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.organizationID>>, Or<Current<FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.organizationID>, IsNull>>>>>();
      }
    }

    public bool AppendExpression(
      ref SQLExpression exp,
      PXGraph graph,
      BqlCommandInfo info,
      BqlCommand.Selection selection)
    {
      return FinPeriodClosingProcessBase.WhereFinPeriodInRange<TFinPeriodID, TOrganizationID>.Where.AppendExpression(ref exp, graph, info, selection);
    }

    public void Verify(
      PXCache cache,
      object item,
      List<object> pars,
      ref bool? result,
      ref object value)
    {
      ((IBqlVerifier) FinPeriodClosingProcessBase.WhereFinPeriodInRange<TFinPeriodID, TOrganizationID>.Where).Verify(cache, item, pars, ref result, ref value);
    }
  }

  public class WhereDateInRange<TDate, TOrganizationID> : 
    IBqlWhere,
    IBqlUnary,
    IBqlCreator,
    IBqlVerifier
    where TDate : IBqlOperand
    where TOrganizationID : IBqlOperand
  {
    private static IBqlCreator Where
    {
      get
      {
        return (IBqlCreator) new Where<TDate, GreaterEqual<Current<FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.fromFinPeriodStartDate>>, And<TDate, LessEqual<Current<FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.toFinPeriodEndDate>>, And<Where<TOrganizationID, Equal<Current<FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.organizationID>>, Or<Current<FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters.organizationID>, IsNull>>>>>();
      }
    }

    public bool AppendExpression(
      ref SQLExpression exp,
      PXGraph graph,
      BqlCommandInfo info,
      BqlCommand.Selection selection)
    {
      return FinPeriodClosingProcessBase.WhereDateInRange<TDate, TOrganizationID>.Where.AppendExpression(ref exp, graph, info, selection);
    }

    public void Verify(
      PXCache cache,
      object item,
      List<object> pars,
      ref bool? result,
      ref object value)
    {
      ((IBqlVerifier) FinPeriodClosingProcessBase.WhereDateInRange<TDate, TOrganizationID>.Where).Verify(cache, item, pars, ref result, ref value);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARStatementDetails
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARStatementDetails : PXGraph<
#nullable disable
ARStatementDetails>
{
  public PXCancel<ARStatementDetails.ARStatementDetailsParameters> Cancel;
  public PXAction<ARStatementDetails.ARStatementDetailsParameters> prevStatementDate;
  public PXAction<ARStatementDetails.ARStatementDetailsParameters> nextStatementDate;
  public PXFilter<ARStatementDetails.ARStatementDetailsParameters> Filter;
  [PXFilterable(new Type[] {})]
  public FbqlSelect<SelectFromBase<ARStatementDetails.DetailsResult, TypeArrayOf<IFbqlJoin>.Empty>.Order<PX.Data.BQL.Fluent.By<Asc<ARStatementDetails.DetailsResult.customerId>>>, ARStatementDetails.DetailsResult>.View Details;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXAction<ARStatementDetails.ARStatementDetailsParameters> viewDetails;
  public PXAction<ARStatementDetails.ARStatementDetailsParameters> printReport;

  public ARStatementDetails()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    ((PXSelectBase) this.Details).Cache.AllowDelete = false;
    ((PXSelectBase) this.Details).Cache.AllowInsert = false;
    ((PXSelectBase) this.Details).Cache.AllowUpdate = false;
  }

  [PXUIField]
  [PXPreviousButton]
  public virtual IEnumerable PrevStatementDate(PXAdapter adapter)
  {
    ARStatementDetails.ARStatementDetailsParameters current = ((PXSelectBase<ARStatementDetails.ARStatementDetailsParameters>) this.Filter).Current;
    if (current != null && !string.IsNullOrEmpty(current.StatementCycleId))
    {
      ARStatement arStatement = PXResultset<ARStatement>.op_Implicit(PXSelectBase<ARStatement, PXSelect<ARStatement, Where<ARStatement.statementCycleId, Equal<Required<ARStatement.statementCycleId>>, And<Where<ARStatement.statementDate, Less<Required<ARStatement.statementDate>>, Or<Required<ARStatement.statementDate>, IsNull>>>>, OrderBy<Desc<ARStatement.statementDate>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) current.StatementCycleId,
        (object) current.StatementDate,
        (object) current.StatementDate
      }));
      if (arStatement != null)
        current.StatementDate = arStatement.StatementDate;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXNextButton]
  public virtual IEnumerable NextStatementDate(PXAdapter adapter)
  {
    ARStatementDetails.ARStatementDetailsParameters current = ((PXSelectBase<ARStatementDetails.ARStatementDetailsParameters>) this.Filter).Current;
    if (current != null && !string.IsNullOrEmpty(current.StatementCycleId))
    {
      ARStatement arStatement = PXResultset<ARStatement>.op_Implicit(PXSelectBase<ARStatement, PXSelect<ARStatement, Where<ARStatement.statementCycleId, Equal<Required<ARStatement.statementCycleId>>, And<Where<ARStatement.statementDate, Greater<Required<ARStatement.statementDate>>, Or<Required<ARStatement.statementDate>, IsNull>>>>, OrderBy<Asc<ARStatement.statementDate>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) current.StatementCycleId,
        (object) current.StatementDate,
        (object) current.StatementDate
      }));
      if (arStatement != null)
        current.StatementDate = arStatement.StatementDate;
    }
    return adapter.Get();
  }

  protected virtual IEnumerable details()
  {
    ARStatementDetails.ARStatementDetailsParameters current = ((PXSelectBase<ARStatementDetails.ARStatementDetailsParameters>) this.Filter).Current;
    List<ARStatementDetails.DetailsResult> detailsResultList = new List<ARStatementDetails.DetailsResult>();
    if (current == null)
      return (IEnumerable) detailsResultList;
    PXResultset<Company>.op_Implicit(PXSelectBase<Company, PXSelect<Company>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    foreach (PXResult<ARStatement, Customer> pxResult in PXSelectBase<ARStatement, PXSelectJoin<ARStatement, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARStatement.statementCustomerID>>>, Where<ARStatement.statementDate, Equal<Required<ARStatement.statementDate>>, And<ARStatement.statementCycleId, Equal<Required<ARStatement.statementCycleId>>>>, OrderBy<Asc<ARStatement.statementCustomerID, Asc<ARStatement.curyID>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) current.StatementDate,
      (object) current.StatementCycleId
    }))
    {
      ARStatementDetails.DetailsResult aSrc1 = new ARStatementDetails.DetailsResult();
      ARStatement aSrc2 = PXResult<ARStatement, Customer>.op_Implicit(pxResult);
      Customer cust = PXResult<ARStatement, Customer>.op_Implicit(pxResult);
      aSrc1.Copy(aSrc2, cust);
      if (cust.PrintCuryStatements.GetValueOrDefault())
      {
        ARStatementDetails.DetailsResult detailsResult = detailsResultList.Count > 0 ? detailsResultList[detailsResultList.Count - 1] : (ARStatementDetails.DetailsResult) null;
        if (detailsResult != null)
        {
          int? customerId1 = detailsResult.CustomerId;
          int? customerId2 = aSrc1.CustomerId;
          if (customerId1.GetValueOrDefault() == customerId2.GetValueOrDefault() & customerId1.HasValue == customerId2.HasValue && detailsResult.CuryID == aSrc1.CuryID)
          {
            detailsResult.Append(aSrc1);
            continue;
          }
        }
        detailsResultList.Add(aSrc1);
      }
      else
      {
        string baseCuryId = PXOrgAccess.GetBaseCuryID(PXAccess.GetBranchCD(aSrc2.BranchID));
        aSrc1.ResetToBaseCury(baseCuryId);
        ARStatementDetails.DetailsResult detailsResult = detailsResultList.Count > 0 ? detailsResultList[detailsResultList.Count - 1] : (ARStatementDetails.DetailsResult) null;
        if (detailsResult != null)
        {
          int? customerId3 = detailsResult.CustomerId;
          int? customerId4 = aSrc1.CustomerId;
          if (customerId3.GetValueOrDefault() == customerId4.GetValueOrDefault() & customerId3.HasValue == customerId4.HasValue && detailsResult.CuryID == baseCuryId)
          {
            detailsResult.Append(aSrc1);
            continue;
          }
        }
        detailsResultList.Add(aSrc1);
      }
    }
    return (IEnumerable) detailsResultList;
  }

  protected virtual void ARStatementDetailsParameters_StatementCycleId_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ARStatementDetails.ARStatementDetailsParameters row = (ARStatementDetails.ARStatementDetailsParameters) e.Row;
    if (string.IsNullOrEmpty(row.StatementCycleId))
      return;
    ARStatementCycle arStatementCycle = PXResultset<ARStatementCycle>.op_Implicit(PXSelectBase<ARStatementCycle, PXSelect<ARStatementCycle, Where<ARStatementCycle.statementCycleId, Equal<Required<ARStatementCycle.statementCycleId>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.StatementCycleId
    }));
    row.StatementDate = arStatementCycle.LastStmtDate;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = true)]
  public virtual IEnumerable ViewDetails(PXAdapter adapter)
  {
    if (((PXSelectBase<ARStatementDetails.DetailsResult>) this.Details).Current != null && ((PXSelectBase<ARStatementDetails.ARStatementDetailsParameters>) this.Filter).Current != null)
    {
      ARStatementDetails.DetailsResult current1 = ((PXSelectBase<ARStatementDetails.DetailsResult>) this.Details).Current;
      ARStatementForCustomer instance = PXGraph.CreateInstance<ARStatementForCustomer>();
      ARStatementForCustomer.ARStatementForCustomerParameters current2 = ((PXSelectBase<ARStatementForCustomer.ARStatementForCustomerParameters>) instance.Filter).Current;
      current2.CustomerID = current1.CustomerId;
      ((PXSelectBase<ARStatementForCustomer.ARStatementForCustomerParameters>) instance.Filter).Update(current2);
      PXResultset<ARStatementForCustomer.ARStatementForCustomerParameters>.op_Implicit(((PXSelectBase<ARStatementForCustomer.ARStatementForCustomerParameters>) instance.Filter).Select(Array.Empty<object>()));
      throw new PXRedirectRequiredException((PXGraph) instance, "Statement History");
    }
    return (IEnumerable) ((PXSelectBase<ARStatementDetails.ARStatementDetailsParameters>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable PrintReport(PXAdapter adapter)
  {
    if (((PXSelectBase<ARStatementDetails.DetailsResult>) this.Details).Current != null && ((PXSelectBase<ARStatementDetails.ARStatementDetailsParameters>) this.Filter).Current != null)
    {
      Dictionary<string, string> aRes = new Dictionary<string, string>();
      ARStatementDetails.DetailsResult current = ((PXSelectBase<ARStatementDetails.DetailsResult>) this.Details).Current;
      Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) current.CustomerId
      }));
      ARStatementDetails.Export(aRes, ((PXSelectBase<ARStatementDetails.ARStatementDetailsParameters>) this.Filter).Current);
      aRes["StatementCustomerId"] = customer.AcctCD;
      string str = current.UseCurrency.GetValueOrDefault() ? "AR642000" : "AR641500";
      throw new PXReportRequiredException(aRes, str, "AR Statement Report", (CurrentLocalization) null);
    }
    return (IEnumerable) ((PXSelectBase<ARStatementDetails.ARStatementDetailsParameters>) this.Filter).Select(Array.Empty<object>());
  }

  protected static void Export(
    Dictionary<string, string> aRes,
    ARStatementDetails.ARStatementDetailsParameters aSrc)
  {
    aRes["StatementCycleId"] = aSrc.StatementCycleId;
    aRes["StatementDate"] = aSrc.StatementDate.Value.ToString("d", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  [Serializable]
  public class ARStatementDetailsParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _StatementCycleId;
    protected DateTime? _StatementDate;

    [PXDBString(10, IsUnicode = true)]
    [PXDefault(typeof (ARStatementCycle))]
    [PXUIField]
    [PXSelector(typeof (ARStatementCycle.statementCycleId), DescriptionField = typeof (ARStatementCycle.descr))]
    public virtual string StatementCycleId
    {
      get => this._StatementCycleId;
      set => this._StatementCycleId = value;
    }

    [PXDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? StatementDate
    {
      get => this._StatementDate;
      set => this._StatementDate = value;
    }

    public abstract class statementCycleId : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARStatementDetails.ARStatementDetailsParameters.statementCycleId>
    {
    }

    public abstract class statementDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARStatementDetails.ARStatementDetailsParameters.statementDate>
    {
    }
  }

  [Serializable]
  public class DetailsResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _CustomerId;
    protected string _CuryID;
    protected Decimal? _StatementBalance;
    protected Decimal? _CuryStatementBalance;
    protected bool? _UseCurrency;
    protected bool? _DontPrint;
    protected bool? _Printed;
    protected bool? _DontEmail;
    protected bool? _Emailed;
    protected Decimal? _AgeBalance00;
    protected Decimal? _CuryAgeBalance00;

    [PXInt(IsKey = true)]
    [CustomerActive(DescriptionField = typeof (Customer.acctName), DisplayName = "Customer")]
    public virtual int? CustomerId
    {
      get => this._CustomerId;
      set => this._CustomerId = value;
    }

    [PXDBString(5, IsUnicode = true, IsKey = true)]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
    [PXUIField(DisplayName = "Currency")]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    [PXBaseCury]
    [PXUIField(DisplayName = "Statement Balance")]
    public virtual Decimal? StatementBalance
    {
      get => this._StatementBalance;
      set => this._StatementBalance = value;
    }

    [PXCury(typeof (ARStatementDetails.DetailsResult.curyID))]
    [PXUIField(DisplayName = "FC Statement Balance")]
    public virtual Decimal? CuryStatementBalance
    {
      get => this._CuryStatementBalance;
      set => this._CuryStatementBalance = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "FC Statement")]
    public virtual bool? UseCurrency
    {
      get => this._UseCurrency;
      set => this._UseCurrency = value;
    }

    [PXBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Don't Print")]
    public virtual bool? DontPrint
    {
      get => this._DontPrint;
      set => this._DontPrint = value;
    }

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Printed")]
    public virtual bool? Printed
    {
      get => this._Printed;
      set => this._Printed = value;
    }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Don't Email")]
    public virtual bool? DontEmail
    {
      get => this._DontEmail;
      set => this._DontEmail = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Emailed")]
    public virtual bool? Emailed
    {
      get => this._Emailed;
      set => this._Emailed = value;
    }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Age00 Balance")]
    public virtual Decimal? AgeBalance00
    {
      get => this._AgeBalance00;
      set => this._AgeBalance00 = value;
    }

    [PXCury(typeof (ARStatementDetails.DetailsResult.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "FC Age00 Balance")]
    public virtual Decimal? CuryAgeBalance00
    {
      get => this._CuryAgeBalance00;
      set => this._CuryAgeBalance00 = value;
    }

    [PXBaseCury]
    [PXUIField(DisplayName = "Overdue Balance")]
    public virtual Decimal? OverdueBalance
    {
      [PXDependsOnFields(new Type[] {typeof (ARStatementDetails.DetailsResult.statementBalance), typeof (ARStatementDetails.DetailsResult.ageBalance00)})] get
      {
        Decimal? statementBalance = this.StatementBalance;
        Decimal? ageBalance00 = this.AgeBalance00;
        return !(statementBalance.HasValue & ageBalance00.HasValue) ? new Decimal?() : new Decimal?(statementBalance.GetValueOrDefault() - ageBalance00.GetValueOrDefault());
      }
    }

    [PXCury(typeof (ARStatementDetails.DetailsResult.curyID))]
    [PXUIField(DisplayName = "FC Overdue Balance")]
    public virtual Decimal? CuryOverdueBalance
    {
      [PXDependsOnFields(new Type[] {typeof (ARStatementDetails.DetailsResult.curyStatementBalance), typeof (ARStatementDetails.DetailsResult.curyAgeBalance00)})] get
      {
        Decimal? statementBalance = this._CuryStatementBalance;
        Decimal valueOrDefault = this.CuryAgeBalance00.GetValueOrDefault();
        return !statementBalance.HasValue ? new Decimal?() : new Decimal?(statementBalance.GetValueOrDefault() - valueOrDefault);
      }
    }

    [PXBool]
    [PXUIField(DisplayName = "On-Demand Statement")]
    public virtual bool? OnDemand { get; set; }

    [PXDate]
    [PXUIField(DisplayName = "Prepared On")]
    public virtual DateTime? PreparedOn { get; set; }

    public virtual void Copy(ARStatement aSrc, Customer cust)
    {
      this.CustomerId = cust.BAccountID;
      this.UseCurrency = cust.PrintCuryStatements;
      this.StatementBalance = new Decimal?(aSrc.EndBalance.GetValueOrDefault());
      this.AgeBalance00 = new Decimal?(aSrc.AgeBalance00.GetValueOrDefault());
      this.CuryID = aSrc.CuryID;
      this.CuryStatementBalance = new Decimal?(aSrc.CuryEndBalance.GetValueOrDefault());
      this.CuryAgeBalance00 = new Decimal?(aSrc.CuryAgeBalance00.GetValueOrDefault());
      this.DontPrint = aSrc.DontPrint;
      this.Printed = aSrc.Printed;
      this.DontEmail = aSrc.DontEmail;
      this.Emailed = aSrc.Emailed;
      this.OnDemand = aSrc.OnDemand;
      this.PreparedOn = aSrc.LastModifiedDateTime;
    }

    public virtual void Append(ARStatementDetails.DetailsResult aSrc)
    {
      Decimal? nullable1 = this.StatementBalance;
      Decimal? statementBalance = aSrc.StatementBalance;
      this.StatementBalance = nullable1.HasValue & statementBalance.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + statementBalance.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable2 = this.AgeBalance00;
      nullable1 = aSrc.AgeBalance00;
      this.AgeBalance00 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
      if (this.CuryID == aSrc.CuryID)
      {
        nullable1 = this.CuryStatementBalance;
        nullable2 = aSrc.CuryStatementBalance;
        this.CuryStatementBalance = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        nullable2 = this.CuryAgeBalance00;
        nullable1 = aSrc.CuryAgeBalance00;
        this.CuryAgeBalance00 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
      }
      else
      {
        this.CuryStatementBalance = new Decimal?(0M);
        this.CuryAgeBalance00 = new Decimal?(0M);
      }
      bool? dontEmail = aSrc.DontEmail;
      bool flag1 = false;
      if (dontEmail.GetValueOrDefault() == flag1 & dontEmail.HasValue)
        this.DontEmail = new bool?(false);
      bool? dontPrint = aSrc.DontPrint;
      bool flag2 = false;
      if (dontPrint.GetValueOrDefault() == flag2 & dontPrint.HasValue)
        this.DontPrint = new bool?(false);
      bool? emailed = aSrc.Emailed;
      bool flag3 = false;
      if (emailed.GetValueOrDefault() == flag3 & emailed.HasValue)
        this.Emailed = new bool?(false);
      bool? printed = aSrc.Printed;
      bool flag4 = false;
      if (!(printed.GetValueOrDefault() == flag4 & printed.HasValue))
        return;
      this.Printed = new bool?(false);
    }

    public virtual void ResetToBaseCury(string aBaseCuryID)
    {
      this._CuryID = aBaseCuryID;
      this._CuryStatementBalance = this._StatementBalance;
      this._CuryAgeBalance00 = this._AgeBalance00;
    }

    public abstract class customerId : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementDetails.DetailsResult.customerId>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARStatementDetails.DetailsResult.curyID>
    {
    }

    public abstract class statementBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementDetails.DetailsResult.statementBalance>
    {
    }

    public abstract class curyStatementBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementDetails.DetailsResult.curyStatementBalance>
    {
    }

    public abstract class useCurrency : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementDetails.DetailsResult.useCurrency>
    {
    }

    public abstract class dontPrint : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementDetails.DetailsResult.dontPrint>
    {
    }

    public abstract class printed : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementDetails.DetailsResult.printed>
    {
    }

    public abstract class dontEmail : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementDetails.DetailsResult.dontEmail>
    {
    }

    public abstract class emailed : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementDetails.DetailsResult.emailed>
    {
    }

    public abstract class ageBalance00 : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementDetails.DetailsResult.ageBalance00>
    {
    }

    public abstract class curyAgeBalance00 : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementDetails.DetailsResult.curyAgeBalance00>
    {
    }

    public abstract class overdueBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementDetails.DetailsResult.overdueBalance>
    {
    }

    public abstract class curyOverdueBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementDetails.DetailsResult.curyOverdueBalance>
    {
    }

    public abstract class onDemand : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementDetails.DetailsResult.onDemand>
    {
    }

    public abstract class preparedOn : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARStatementDetails.DetailsResult.preparedOn>
    {
    }
  }
}

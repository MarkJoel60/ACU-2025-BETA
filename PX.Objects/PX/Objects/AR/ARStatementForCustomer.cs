// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARStatementForCustomer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR.Repositories;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARStatementForCustomer : PXGraph<
#nullable disable
ARStatementForCustomer>
{
  public PXFilter<ARStatementForCustomer.ARStatementForCustomerParameters> Filter;
  public PXCancel<ARStatementForCustomer.ARStatementForCustomerParameters> Cancel;
  [PXFilterable(new Type[] {})]
  public PXSelectOrderBy<ARStatementForCustomer.DetailsResult, OrderBy<Asc<ARStatementForCustomer.DetailsResult.statementDate>>> Details;
  protected readonly CustomerRepository CustomerRepository;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXAction<ARStatementForCustomer.ARStatementForCustomerParameters> printReport;

  public ARStatementForCustomer()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    ((PXSelectBase) this.Details).Cache.AllowDelete = false;
    ((PXSelectBase) this.Details).Cache.AllowInsert = false;
    ((PXSelectBase) this.Details).Cache.AllowUpdate = false;
    this.CustomerRepository = new CustomerRepository((PXGraph) this);
  }

  protected virtual IEnumerable details()
  {
    ARStatementForCustomer.ARStatementForCustomerParameters current = ((PXSelectBase<ARStatementForCustomer.ARStatementForCustomerParameters>) this.Filter).Current;
    Dictionary<ARStatementForCustomer.DetailKey, ARStatementForCustomer.DetailsResult> dictionary = new Dictionary<ARStatementForCustomer.DetailKey, ARStatementForCustomer.DetailsResult>((IEqualityComparer<ARStatementForCustomer.DetailKey>) EqualityComparer<ARStatementForCustomer.DetailKey>.Default);
    List<ARStatementForCustomer.DetailsResult> detailsResultList = new List<ARStatementForCustomer.DetailsResult>();
    if (current == null)
      return (IEnumerable) detailsResultList;
    Customer byId = this.CustomerRepository.FindByID(current.CustomerID);
    if (byId == null)
      return (IEnumerable) detailsResultList;
    bool valueOrDefault = byId.PrintCuryStatements.GetValueOrDefault();
    Company company = PXResultset<Company>.op_Implicit(PXSelectBase<Company, PXSelect<Company>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    PXSelectBase<ARStatement> pxSelectBase = (PXSelectBase<ARStatement>) new PXSelectJoin<ARStatement, InnerJoin<PX.Objects.GL.Branch, On<ARStatement.branchID, Equal<PX.Objects.GL.Branch.branchID>>, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>>, Where<ARStatement.statementCustomerID, Equal<Required<ARStatement.customerID>>>, OrderBy<Asc<ARStatement.statementCycleId, Asc<ARStatement.statementDate, Asc<ARStatement.curyID, Asc<ARStatement.branchID, Asc<ARStatement.customerID>>>>>>>((PXGraph) this);
    List<object> objectList = new List<object>()
    {
      (object) current.CustomerID
    };
    int? nullable;
    if (current.BranchID.HasValue)
    {
      pxSelectBase.WhereAnd<Where<ARStatement.branchID, Equal<Required<ARStatement.branchID>>>>();
      objectList.Add((object) current.BranchID);
    }
    else
    {
      nullable = current.OrgBAccountID;
      if (nullable.HasValue)
      {
        pxSelectBase.WhereAnd<Where<PX.Objects.GL.DAC.Organization.bAccountID, Equal<Required<PX.Objects.GL.DAC.Organization.bAccountID>>>>();
        objectList.Add((object) current.OrgBAccountID);
      }
    }
    foreach (PXResult<ARStatement> pxResult in pxSelectBase.Select(objectList.ToArray()))
    {
      ARStatement aSrc1 = PXResult<ARStatement>.op_Implicit(pxResult);
      ARStatementForCustomer.DetailsResult aSrc2 = new ARStatementForCustomer.DetailsResult();
      aSrc2.Copy(aSrc1, byId);
      DateTime? statementDate1;
      int? branchId;
      if (valueOrDefault)
      {
        ARStatementForCustomer.DetailsResult detailsResult = detailsResultList.Count > 0 ? detailsResultList[detailsResultList.Count - 1] : (ARStatementForCustomer.DetailsResult) null;
        if (detailsResult != null && detailsResult.StatementCycleId == aSrc2.StatementCycleId)
        {
          DateTime? statementDate2 = detailsResult.StatementDate;
          statementDate1 = aSrc2.StatementDate;
          if ((statementDate2.HasValue == statementDate1.HasValue ? (statementDate2.HasValue ? (statementDate2.GetValueOrDefault() == statementDate1.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && detailsResult.CuryID == aSrc2.CuryID)
          {
            nullable = detailsResult.BranchID;
            branchId = aSrc2.BranchID;
            if (nullable.GetValueOrDefault() == branchId.GetValueOrDefault() & nullable.HasValue == branchId.HasValue)
            {
              detailsResult.Append(aSrc2);
              continue;
            }
          }
        }
        detailsResultList.Add(aSrc2);
      }
      else
      {
        statementDate1 = aSrc2.StatementDate;
        DateTime aFirst = statementDate1.Value;
        string statementCycleId = aSrc2.StatementCycleId;
        branchId = aSrc2.BranchID;
        int aThird = branchId.Value;
        ARStatementForCustomer.DetailKey key = new ARStatementForCustomer.DetailKey(aFirst, statementCycleId, aThird);
        string aBaseCuryID = PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() ? PXOrgAccess.GetBaseCuryID(PXAccess.GetBranchCD(aSrc1.BranchID)) : company.BaseCuryID;
        aSrc2.ResetToBaseCury(aBaseCuryID);
        if (!dictionary.ContainsKey(key))
          dictionary[key] = aSrc2;
        else
          dictionary[key].Append(aSrc2);
      }
    }
    return !valueOrDefault ? (IEnumerable) dictionary.Values : (IEnumerable) detailsResultList;
  }

  [PXUIField]
  [PXButton]
  public IEnumerable PrintReport(PXAdapter adapter)
  {
    if (((PXSelectBase<ARStatementForCustomer.DetailsResult>) this.Details).Current != null && ((PXSelectBase<ARStatementForCustomer.ARStatementForCustomerParameters>) this.Filter).Current != null && ((PXSelectBase<ARStatementForCustomer.ARStatementForCustomerParameters>) this.Filter).Current.CustomerID.HasValue)
    {
      Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<ARStatementForCustomer.ARStatementForCustomerParameters>) this.Filter).Current.CustomerID
      }));
      if (customer != null)
      {
        Dictionary<string, string> aRes = ARStatementReportParams.FromCustomer(customer);
        int? nullable = ((PXSelectBase<ARStatementForCustomer.ARStatementForCustomerParameters>) this.Filter).Current.BranchID;
        if (nullable.HasValue)
        {
          aRes["BranchID"] = PXAccess.GetBranchCD(((PXSelectBase<ARStatementForCustomer.ARStatementForCustomerParameters>) this.Filter).Current.BranchID);
        }
        else
        {
          nullable = ((PXSelectBase<ARStatementForCustomer.ARStatementForCustomerParameters>) this.Filter).Current.OrganizationID;
          if (nullable.HasValue)
            aRes["OrganizationID"] = PXAccess.GetOrganizationCD(((PXSelectBase<ARStatementForCustomer.ARStatementForCustomerParameters>) this.Filter).Current.OrganizationID);
        }
        ARStatementForCustomer.Export(aRes, ((PXSelectBase<ARStatementForCustomer.DetailsResult>) this.Details).Current);
        PXReportRequiredException requiredException = PXReportRequiredException.CombineReport((PXReportRequiredException) null, ARStatementReportParams.ReportIDForCustomer((PXGraph) this, customer, ((PXSelectBase<ARStatementForCustomer.DetailsResult>) this.Details).Current.BranchID), aRes, (CurrentLocalization) null);
        if (requiredException != null)
          throw requiredException;
      }
    }
    return (IEnumerable) ((PXSelectBase<ARStatementForCustomer.ARStatementForCustomerParameters>) this.Filter).Select(Array.Empty<object>());
  }

  protected virtual void ARStatementForCustomerParameters_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARStatementForCustomer.ARStatementForCustomerParameters row))
      return;
    Customer byId = this.CustomerRepository.FindByID(row.CustomerID);
    if (byId == null)
      return;
    bool valueOrDefault = byId.PrintCuryStatements.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<ARStatementForCustomer.DetailsResult.curyID>(((PXSelectBase) this.Details).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<ARStatementForCustomer.DetailsResult.curyStatementBalance>(((PXSelectBase) this.Details).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<ARStatementForCustomer.DetailsResult.curyOverdueBalance>(((PXSelectBase) this.Details).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<ARStatementForCustomer.DetailsResult.statementBalance>(((PXSelectBase) this.Details).Cache, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetVisible<ARStatementForCustomer.DetailsResult.overdueBalance>(((PXSelectBase) this.Details).Cache, (object) null, !valueOrDefault);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<ARStatementForCustomer.ARStatementForCustomerParameters, ARStatementForCustomer.ARStatementForCustomerParameters.orgBAccountID> e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.branch>())
      return;
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(((PXGraph) this).Accessinfo.BranchID);
    if (branch == null || ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARStatementForCustomer.ARStatementForCustomerParameters, ARStatementForCustomer.ARStatementForCustomerParameters.orgBAccountID>, ARStatementForCustomer.ARStatementForCustomerParameters, object>) e).NewValue != null)
      return;
    switch (((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.PrepareStatements)
    {
      case "B":
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARStatementForCustomer.ARStatementForCustomerParameters, ARStatementForCustomer.ARStatementForCustomerParameters.orgBAccountID>, ARStatementForCustomer.ARStatementForCustomerParameters, object>) e).NewValue = (object) branch.BranchCD.Trim();
        break;
      case "C":
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARStatementForCustomer.ARStatementForCustomerParameters, ARStatementForCustomer.ARStatementForCustomerParameters.orgBAccountID>, ARStatementForCustomer.ARStatementForCustomerParameters, object>) e).NewValue = (object) ((PXAccess.Organization) branch.Organization).OrganizationCD.Trim();
        break;
      default:
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARStatementForCustomer.ARStatementForCustomerParameters, ARStatementForCustomer.ARStatementForCustomerParameters.orgBAccountID>, ARStatementForCustomer.ARStatementForCustomerParameters, object>) e).NewValue = (object) null;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARStatementForCustomer.ARStatementForCustomerParameters, ARStatementForCustomer.ARStatementForCustomerParameters.orgBAccountID>>) e).Cancel = true;
        break;
    }
  }

  protected static void Export(
    Dictionary<string, string> aRes,
    ARStatementForCustomer.DetailsResult aDetail)
  {
    aRes["StatementCycleId"] = aDetail.StatementCycleId;
    aRes["StatementDate"] = aDetail.StatementDate.Value.ToString("d", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  [Serializable]
  public class ARStatementForCustomerParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _CustomerID;

    [PXInt]
    [PXDefault]
    [PXUIField(DisplayName = "Customer")]
    [Customer(DescriptionField = typeof (Customer.acctName))]
    public virtual int? CustomerID
    {
      get => this._CustomerID;
      set => this._CustomerID = value;
    }

    [Organization(false, null)]
    public int? OrganizationID { get; set; }

    [Branch(null, null, false, true, false, Required = false)]
    public virtual int? BranchID { get; set; }

    [OrganizationTree(typeof (ARStatementForCustomer.ARStatementForCustomerParameters.organizationID), typeof (ARStatementForCustomer.ARStatementForCustomerParameters.branchID), null, false)]
    public int? OrgBAccountID { get; set; }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementForCustomer.ARStatementForCustomerParameters.customerID>
    {
    }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementForCustomer.ARStatementForCustomerParameters.organizationID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementForCustomer.ARStatementForCustomerParameters.branchID>
    {
    }

    public abstract class orgBAccountID : IBqlField, IBqlOperand
    {
    }
  }

  [Serializable]
  public class DetailsResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _StatementCycleId;
    protected DateTime? _StatementDate;
    protected Decimal? _StatementBalance;
    protected string _CuryID;
    protected Decimal? _CuryStatementBalance;
    protected bool? _DontPrint;
    protected bool? _Printed;
    protected bool? _DontEmail;
    protected bool? _Emailed;
    protected Decimal? _AgeBalance00;
    protected Decimal? _CuryAgeBalance00;
    protected int? _BranchID;

    [PXString(10, IsUnicode = true)]
    [PXUIField(DisplayName = "Statement Cycle")]
    [PXSelector(typeof (ARStatementCycle.statementCycleId))]
    public virtual string StatementCycleId
    {
      get => this._StatementCycleId;
      set => this._StatementCycleId = value;
    }

    [PXDate(IsKey = true)]
    [PXDefault]
    [PXUIField(DisplayName = "Statement Date")]
    public virtual DateTime? StatementDate
    {
      get => this._StatementDate;
      set => this._StatementDate = value;
    }

    [PXDBBaseCury(null, null)]
    [PXUIField(DisplayName = "Statement Balance")]
    public virtual Decimal? StatementBalance
    {
      get => this._StatementBalance;
      set => this._StatementBalance = value;
    }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXUIField]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    [PXCury(typeof (ARStatementForCustomer.DetailsResult.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "FC Statement Balance")]
    public virtual Decimal? CuryStatementBalance
    {
      get => this._CuryStatementBalance;
      set => this._CuryStatementBalance = value;
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

    [PXCury(typeof (ARStatementForCustomer.DetailsResult.curyID))]
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
      [PXDependsOnFields(new Type[] {typeof (ARStatementForCustomer.DetailsResult.statementBalance), typeof (ARStatementForCustomer.DetailsResult.ageBalance00)})] get
      {
        Decimal? statementBalance = this.StatementBalance;
        Decimal? ageBalance00 = this.AgeBalance00;
        return !(statementBalance.HasValue & ageBalance00.HasValue) ? new Decimal?() : new Decimal?(statementBalance.GetValueOrDefault() - ageBalance00.GetValueOrDefault());
      }
    }

    [PXCury(typeof (ARStatementForCustomer.DetailsResult.curyID))]
    [PXUIField(DisplayName = "FC Overdue Balance")]
    public virtual Decimal? CuryOverdueBalance
    {
      [PXDependsOnFields(new Type[] {typeof (ARStatementForCustomer.DetailsResult.curyStatementBalance), typeof (ARStatementForCustomer.DetailsResult.curyAgeBalance00)})] get
      {
        return new Decimal?(this._CuryStatementBalance.GetValueOrDefault() - this.CuryAgeBalance00.GetValueOrDefault());
      }
    }

    [Branch(null, null, true, true, true)]
    public virtual int? BranchID
    {
      get => this._BranchID;
      set => this._BranchID = value;
    }

    [PXBool]
    [PXUIField(DisplayName = "On-Demand Statement")]
    public virtual bool? OnDemand { get; set; }

    [PXDate]
    [PXUIField(DisplayName = "Prepared On")]
    public virtual DateTime? PreparedOn { get; set; }

    public virtual void Copy(ARStatement aSrc, Customer cust)
    {
      this.StatementCycleId = aSrc.StatementCycleId;
      this.StatementDate = aSrc.StatementDate;
      this.StatementBalance = new Decimal?(aSrc.EndBalance.GetValueOrDefault());
      this.AgeBalance00 = new Decimal?(aSrc.AgeBalance00.GetValueOrDefault());
      this.CuryID = aSrc.CuryID;
      this.CuryStatementBalance = new Decimal?(aSrc.CuryEndBalance.GetValueOrDefault());
      this.CuryAgeBalance00 = new Decimal?(aSrc.CuryAgeBalance00.GetValueOrDefault());
      this.DontPrint = aSrc.DontPrint;
      this.Printed = aSrc.Printed;
      this.DontEmail = aSrc.DontEmail;
      this.Emailed = aSrc.Emailed;
      this.BranchID = aSrc.BranchID;
      this.OnDemand = aSrc.OnDemand;
      this.PreparedOn = aSrc.CreatedDateTime;
    }

    public virtual void Append(ARStatementForCustomer.DetailsResult aSrc)
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
      bool? dontPrint = aSrc.DontPrint;
      bool flag1 = false;
      if (dontPrint.GetValueOrDefault() == flag1 & dontPrint.HasValue)
        this.DontPrint = new bool?(false);
      bool? dontEmail = aSrc.DontEmail;
      bool flag2 = false;
      if (dontEmail.GetValueOrDefault() == flag2 & dontEmail.HasValue)
        this.DontEmail = new bool?(false);
      bool? printed = aSrc.Printed;
      bool flag3 = false;
      if (printed.GetValueOrDefault() == flag3 & printed.HasValue)
        this.Printed = new bool?(false);
      bool? emailed = aSrc.Emailed;
      bool flag4 = false;
      if (!(emailed.GetValueOrDefault() == flag4 & emailed.HasValue))
        return;
      this.Emailed = new bool?(false);
    }

    public virtual void ResetToBaseCury(string aBaseCuryID)
    {
      this._CuryID = aBaseCuryID;
      this._CuryStatementBalance = this._StatementBalance;
      this._CuryAgeBalance00 = this._AgeBalance00;
    }

    public abstract class statementCycleId : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARStatementForCustomer.DetailsResult.statementCycleId>
    {
    }

    public abstract class statementDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARStatementForCustomer.DetailsResult.statementDate>
    {
    }

    public abstract class statementBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementForCustomer.DetailsResult.statementBalance>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARStatementForCustomer.DetailsResult.curyID>
    {
    }

    public abstract class curyStatementBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementForCustomer.DetailsResult.curyStatementBalance>
    {
    }

    public abstract class dontPrint : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementForCustomer.DetailsResult.dontPrint>
    {
    }

    public abstract class printed : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementForCustomer.DetailsResult.printed>
    {
    }

    public abstract class dontEmail : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementForCustomer.DetailsResult.dontEmail>
    {
    }

    public abstract class emailed : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementForCustomer.DetailsResult.emailed>
    {
    }

    public abstract class ageBalance00 : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementForCustomer.DetailsResult.ageBalance00>
    {
    }

    public abstract class curyAgeBalance00 : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementForCustomer.DetailsResult.curyAgeBalance00>
    {
    }

    public abstract class overdueBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementForCustomer.DetailsResult.overdueBalance>
    {
    }

    public abstract class curyOverdueBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementForCustomer.DetailsResult.curyOverdueBalance>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementForCustomer.DetailsResult.branchID>
    {
    }

    public abstract class onDemand : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementForCustomer.DetailsResult.onDemand>
    {
    }

    public abstract class preparedOn : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARStatementForCustomer.DetailsResult.preparedOn>
    {
    }
  }

  public class DetailKey(DateTime aFirst, string aSecond, int aThird) : 
    Triplet<DateTime, string, int>(aFirst, aSecond, aThird),
    IEquatable<ARStatementForCustomer.DetailKey>
  {
    public override int GetHashCode()
    {
      return this.first.GetHashCode() ^ this.second.GetHashCode() ^ this.third.GetHashCode();
    }

    public virtual bool Equals(ARStatementForCustomer.DetailKey other)
    {
      return this.Equals((Triplet<DateTime, string, int>) other);
    }
  }
}

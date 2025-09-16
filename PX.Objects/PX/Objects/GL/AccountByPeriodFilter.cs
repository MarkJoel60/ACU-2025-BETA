// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountByPeriodFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods;
using System;

#nullable enable
namespace PX.Objects.GL;

[Serializable]
public class AccountByPeriodFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _StartPeriodID;
  protected string _EndPeriodID;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected int? _AccountID;
  protected string _SubID;
  protected bool? _ShowSummary;
  protected bool? _IncludeUnreleased;
  protected bool? _IncludeUnposted;
  protected bool? _IncludeReclassified;
  protected Decimal? _BegBal;
  protected Decimal? _CreditTotal;
  protected Decimal? _DebitTotal;
  protected Decimal? _EndBal;
  protected Decimal? _TranCreditTotal;
  protected Decimal? _TranDebitTotal;
  protected Decimal? _turnOver;
  protected bool? _ShowCuryDetail;
  protected DateTime? _PeriodStartDate;
  protected DateTime? _PeriodEndDate;
  protected string _FinPeriodID;

  [Organization(false, Required = false)]
  public int? OrganizationID { get; set; }

  [BranchOfOrganization(typeof (AccountByPeriodFilter.organizationID), false, null, null, Required = false)]
  public int? BranchID { get; set; }

  [OrganizationTree(typeof (AccountByPeriodFilter.organizationID), typeof (AccountByPeriodFilter.branchID), null, false)]
  public int? OrgBAccountID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Coalesce<Coalesce<Search<PX.Objects.GL.DAC.Organization.actualLedgerID, Where<PX.Objects.GL.DAC.Organization.bAccountID, Equal<Current2<AccountByPeriodFilter.orgBAccountID>>>>, Search<Branch.ledgerID, Where<Branch.bAccountID, Equal<Current2<AccountByPeriodFilter.orgBAccountID>>>>>, Search<Branch.ledgerID, Where<Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>))]
  [PXUIField]
  [PXSelector(typeof (Search<Ledger.ledgerID, Where<Ledger.balanceType, NotEqual<LedgerBalanceType.budget>>>), SubstituteKey = typeof (Ledger.ledgerCD), DescriptionField = typeof (Ledger.descr), CacheGlobal = true)]
  public virtual int? LedgerID { get; set; }

  [PXDefault]
  [AnyPeriodFilterable(null, typeof (AccessInfo.businessDate), typeof (AccountByPeriodFilter.branchID), null, typeof (AccountByPeriodFilter.organizationID), typeof (AccountByPeriodFilter.useMasterCalendar), null, false, null, null)]
  [PXUIField]
  public virtual string StartPeriodID
  {
    get => this._StartPeriodID;
    set => this._StartPeriodID = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Use Master Calendar")]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.multipleCalendarsSupport>))]
  public bool? UseMasterCalendar { get; set; }

  [PXDefault]
  [AnyPeriodFilterable(null, typeof (AccessInfo.businessDate), typeof (AccountByPeriodFilter.branchID), null, typeof (AccountByPeriodFilter.organizationID), typeof (AccountByPeriodFilter.useMasterCalendar), null, false, null, null)]
  [PXUIField]
  public virtual string EndPeriodID
  {
    get => this._EndPeriodID;
    set => this._EndPeriodID = value;
  }

  [PXDate]
  [PXUIField]
  public virtual DateTime? StartDateUI
  {
    [PXDependsOnFields(new Type[] {typeof (AccountByPeriodFilter.startDate), typeof (AccountByPeriodFilter.endDate)})] get
    {
      if (this._StartDate.HasValue && this._EndDate.HasValue)
      {
        DateTime? startDate = this._StartDate;
        DateTime? endDate = this._EndDate;
        if ((startDate.HasValue == endDate.HasValue ? (startDate.HasValue ? (startDate.GetValueOrDefault() == endDate.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          return new DateTime?(this._StartDate.Value.AddDays(-1.0));
      }
      return this._StartDate;
    }
    set
    {
      DateTime? nullable1;
      if (value.HasValue && this._EndDate.HasValue)
      {
        DateTime? nullable2 = value;
        DateTime? endDateUi = this.EndDateUI;
        if ((nullable2.HasValue == endDateUi.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == endDateUi.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          nullable1 = new DateTime?(value.Value.AddDays(1.0));
          goto label_4;
        }
      }
      nullable1 = value;
label_4:
      this._StartDate = nullable1;
    }
  }

  [PXDate]
  [PXUIField]
  public virtual DateTime? EndDateUI
  {
    get
    {
      ref DateTime? local = ref this._EndDate;
      return !local.HasValue ? new DateTime?() : new DateTime?(local.GetValueOrDefault().AddDays(-1.0));
    }
    set => this._EndDate = value?.AddDays(1.0);
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBDate]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [AccountAny]
  [PXDefault]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccountRestrictedRaw(DisplayName = "Subaccount", SuppressValidation = true)]
  public virtual string SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show Summary")]
  public virtual bool? ShowSummary
  {
    get => this._ShowSummary;
    set => this._ShowSummary = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Unreleased")]
  public virtual bool? IncludeUnreleased
  {
    get => this._IncludeUnreleased;
    set => this._IncludeUnreleased = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Unposted")]
  public virtual bool? IncludeUnposted
  {
    get => this._IncludeUnposted;
    set => this._IncludeUnposted = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Include Reclassified")]
  public virtual bool? IncludeReclassified
  {
    get => this._IncludeReclassified;
    set => this._IncludeReclassified = value;
  }

  [PXDBBaseCury(typeof (AccountByPeriodFilter.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Beginning Balance", Enabled = false, Visible = true)]
  public virtual Decimal? BegBal
  {
    get => this._BegBal;
    set => this._BegBal = value;
  }

  [PXDBBaseCury(typeof (AccountByPeriodFilter.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Acct. Credit Total", Enabled = false, Visible = false)]
  public virtual Decimal? CreditTotal
  {
    get => this._CreditTotal;
    set => this._CreditTotal = value;
  }

  [PXDBBaseCury(typeof (AccountByPeriodFilter.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Acct. Debit Total", Enabled = false, Visible = false)]
  public virtual Decimal? DebitTotal
  {
    get => this._DebitTotal;
    set => this._DebitTotal = value;
  }

  [PXDBBaseCury(typeof (AccountByPeriodFilter.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ending Balance", Enabled = false, Visible = true)]
  public virtual Decimal? EndBal
  {
    get => this._EndBal;
    set => this._EndBal = value;
  }

  [PXDBBaseCury(typeof (AccountByPeriodFilter.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Credit Total", Enabled = false, Visible = false)]
  public virtual Decimal? TranCreditTotal
  {
    get => this._TranCreditTotal;
    set => this._TranCreditTotal = value;
  }

  [PXDBBaseCury(typeof (AccountByPeriodFilter.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Debit Total", Enabled = false, Visible = false)]
  public virtual Decimal? TranDebitTotal
  {
    get => this._TranDebitTotal;
    set => this._TranDebitTotal = value;
  }

  [PXDBBaseCury(typeof (AccountByPeriodFilter.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Turnover", Enabled = false, Visible = true)]
  public virtual Decimal? TurnOver
  {
    get => this._turnOver;
    set => this._turnOver = value;
  }

  [PXDBBaseCury(typeof (AccountByPeriodFilter.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnsignedBegBal { get; set; }

  [PXDBCury(typeof (GLTranR.curyID))]
  public virtual Decimal? UnsignedCuryBegBal { get; set; }

  [PXDBBool]
  [PXDefault]
  [PXUIField]
  public virtual bool? ShowCuryDetail
  {
    get => this._ShowCuryDetail;
    set => this._ShowCuryDetail = value;
  }

  [PXDBString(30, IsUnicode = true)]
  public virtual string SubCDWildcard
  {
    [PXDependsOnFields(new Type[] {typeof (AccountByPeriodFilter.subID)})] get
    {
      return SubCDUtils.CreateSubCDWildcard(this._SubID, "SUBACCOUNT");
    }
  }

  [PXDBDate]
  [PXDefault(typeof (Search<MasterFinPeriod.startDate, Where<MasterFinPeriod.finPeriodID, Equal<Current<AccountByPeriodFilter.startPeriodID>>>>))]
  [PXUIField]
  public virtual DateTime? PeriodStartDate
  {
    get => this._PeriodStartDate;
    set => this._PeriodStartDate = value;
  }

  [PXDBDate]
  [PXDefault(typeof (Search<MasterFinPeriod.endDate, Where<MasterFinPeriod.finPeriodID, Equal<Current<AccountByPeriodFilter.endPeriodID>>>>))]
  public virtual DateTime? PeriodEndDate
  {
    get => this._PeriodEndDate;
    set => this._PeriodEndDate = value;
  }

  [PXDate]
  [PXUIField]
  public virtual DateTime? PeriodStartDateUI
  {
    [PXDependsOnFields(new Type[] {typeof (AccountByPeriodFilter.periodStartDate), typeof (AccountByPeriodFilter.periodEndDate)})] get
    {
      if (this._PeriodStartDate.HasValue && this._PeriodEndDate.HasValue)
      {
        DateTime? periodStartDate = this._PeriodStartDate;
        DateTime? periodEndDate = this._PeriodEndDate;
        if ((periodStartDate.HasValue == periodEndDate.HasValue ? (periodStartDate.HasValue ? (periodStartDate.GetValueOrDefault() == periodEndDate.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          return new DateTime?(this._PeriodStartDate.Value.AddDays(-1.0));
      }
      return this._PeriodStartDate;
    }
    set
    {
    }
  }

  [PXDate]
  [PXUIField]
  public virtual DateTime? PeriodEndDateUI
  {
    get
    {
      ref DateTime? local = ref this._PeriodEndDate;
      return !local.HasValue ? new DateTime?() : new DateTime?(local.GetValueOrDefault().AddDays(-1.0));
    }
  }

  [FinPeriodIDFormatting]
  public string FinPeriodID
  {
    get => this._FinPeriodID;
    set
    {
      this._FinPeriodID = value;
      if (value == null)
        return;
      this.StartPeriodID = value;
      this.EndPeriodID = value;
    }
  }

  /// <summary>
  /// This field is used to pass parameters from the GL632000 report.
  /// When set to <c>true</c> indicates that an empty OrgBAccountID parameter was received.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  public virtual bool? OrgBAccountIDIsEmpty { get; set; }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AccountByPeriodFilter.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccountByPeriodFilter.branchID>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccountByPeriodFilter.ledgerID>
  {
  }

  public abstract class startPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AccountByPeriodFilter.startPeriodID>
  {
  }

  public abstract class useMasterCalendar : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AccountByPeriodFilter.useMasterCalendar>
  {
  }

  public abstract class endPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AccountByPeriodFilter.endPeriodID>
  {
  }

  public abstract class startDateUI : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    AccountByPeriodFilter.startDateUI>
  {
  }

  public abstract class endDateUI : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    AccountByPeriodFilter.endDateUI>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    AccountByPeriodFilter.startDate>
  {
  }

  public abstract class endDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    AccountByPeriodFilter.endDate>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccountByPeriodFilter.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AccountByPeriodFilter.subID>
  {
  }

  public abstract class showSummary : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AccountByPeriodFilter.showSummary>
  {
  }

  public abstract class includeUnreleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AccountByPeriodFilter.includeUnreleased>
  {
  }

  public abstract class includeUnposted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AccountByPeriodFilter.includeUnposted>
  {
  }

  public abstract class includeReclassified : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AccountByPeriodFilter.includeReclassified>
  {
  }

  public abstract class begBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  AccountByPeriodFilter.begBal>
  {
  }

  public abstract class creditTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AccountByPeriodFilter.creditTotal>
  {
  }

  public abstract class debitTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AccountByPeriodFilter.debitTotal>
  {
  }

  public abstract class endBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  AccountByPeriodFilter.endBal>
  {
  }

  public abstract class tranCreditTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AccountByPeriodFilter.tranCreditTotal>
  {
  }

  public abstract class tranDebitTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AccountByPeriodFilter.tranDebitTotal>
  {
  }

  public abstract class turnOver : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AccountByPeriodFilter.turnOver>
  {
  }

  public abstract class unsignedBegBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AccountByPeriodFilter.unsignedBegBal>
  {
  }

  public abstract class unsignedCuryBegBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AccountByPeriodFilter.unsignedCuryBegBal>
  {
  }

  public abstract class showCuryDetail : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AccountByPeriodFilter.showCuryDetail>
  {
  }

  public abstract class subCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AccountByPeriodFilter.subCDWildcard>
  {
  }

  public abstract class periodStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    AccountByPeriodFilter.periodStartDate>
  {
  }

  public abstract class periodEndDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    AccountByPeriodFilter.periodEndDate>
  {
  }

  public abstract class periodstartDateUI : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    AccountByPeriodFilter.periodstartDateUI>
  {
  }

  public abstract class periodEndDateUI : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    AccountByPeriodFilter.periodEndDateUI>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AccountByPeriodFilter.finPeriodID>
  {
  }

  public abstract class orgBAccountIDIsEmpty : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AccountByPeriodFilter.orgBAccountIDIsEmpty>
  {
  }
}

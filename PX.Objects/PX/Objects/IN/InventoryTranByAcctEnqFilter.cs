// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryTranByAcctEnqFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class InventoryTranByAcctEnqFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _FinPeriodID;
  protected bool? _ByFinancialPeriod;
  protected DateTime? _PeriodStartDate;
  protected DateTime? _PeriodEndDate;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected int? _AccountID;
  protected string _SubCD;
  protected bool? _SummaryByDay;
  protected int? _InventoryID;
  protected int? _SiteID;

  [FinPeriodSelector(typeof (AccessInfo.businessDate))]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "By Financial Period")]
  public virtual bool? ByFinancialPeriod
  {
    get => this._ByFinancialPeriod;
    set => this._ByFinancialPeriod = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? PeriodStartDate
  {
    get => this._PeriodStartDate;
    set => this._PeriodStartDate = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? PeriodEndDate
  {
    get => this._PeriodEndDate;
    set => this._PeriodEndDate = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? PeriodEndDateInclusive
  {
    get
    {
      return !this._PeriodEndDate.HasValue ? new DateTime?() : new DateTime?(this._PeriodEndDate.Value.AddDays(-1.0));
    }
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "End Date")]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDefault]
  [Account(null, typeof (Search5<PX.Objects.GL.Account.accountID, InnerJoin<INItemCostHist, On<PX.Objects.GL.Account.accountID, Equal<INItemCostHist.accountID>>>, Where<Match<Current<AccessInfo.userName>>>, Aggregate<GroupBy<PX.Objects.GL.Account.accountID>>>), DisplayName = "Inventory Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccountRaw(DisplayName = "Subaccount")]
  public virtual string SubCD
  {
    get => this._SubCD;
    set => this._SubCD = value;
  }

  [PXDBString(30, IsUnicode = true)]
  public virtual string SubCDWildcard => SubCDUtils.CreateSubCDWildcard(this._SubCD, "SUBACCOUNT");

  [PXDBBool]
  [PXDefault]
  [PXUIField(DisplayName = "Summary by Day")]
  public virtual bool? SummaryByDay
  {
    get => this._SummaryByDay;
    set => this._SummaryByDay = value;
  }

  [AnyInventory(typeof (Search<InventoryItem.inventoryID, Where2<Where<InventoryItem.stkItem, NotEqual<boolFalse>, Or<InventoryItem.isConverted, Equal<True>>>, And<Where<Match<Current<AccessInfo.userName>>>>>>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [Site(DescriptionField = typeof (INSite.descr), DisplayName = "Warehouse")]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [OrganizationTree(null, true, FieldClass = "MultipleBaseCurrencies", Required = true)]
  public int? OrgBAccountID { get; set; }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranByAcctEnqFilter.finPeriodID>
  {
  }

  public abstract class byFinancialPeriod : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryTranByAcctEnqFilter.byFinancialPeriod>
  {
  }

  public abstract class periodStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryTranByAcctEnqFilter.periodStartDate>
  {
  }

  public abstract class periodEndDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryTranByAcctEnqFilter.periodEndDate>
  {
  }

  public abstract class periodEndDateInclusive : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryTranByAcctEnqFilter.periodEndDateInclusive>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryTranByAcctEnqFilter.startDate>
  {
  }

  public abstract class endDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryTranByAcctEnqFilter.endDate>
  {
  }

  public abstract class accountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryTranByAcctEnqFilter.accountID>
  {
  }

  public abstract class subCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryTranByAcctEnqFilter.subCD>
  {
  }

  public abstract class subCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranByAcctEnqFilter.subCDWildcard>
  {
  }

  public abstract class summaryByDay : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryTranByAcctEnqFilter.summaryByDay>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryTranByAcctEnqFilter.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryTranByAcctEnqFilter.siteID>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }
}

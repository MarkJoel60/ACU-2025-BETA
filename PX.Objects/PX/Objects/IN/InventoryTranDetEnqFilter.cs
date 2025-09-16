// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryTranDetEnqFilter
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
public class InventoryTranDetEnqFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TransferNbr;
  protected string _FinPeriodID;
  protected DateTime? _PeriodStartDate;
  protected DateTime? _PeriodEndDate;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected int? _InventoryID;
  protected string _SubItemCD;
  protected int? _SiteID;
  protected int? _LocationID;
  protected string _LotSerialNbr;
  protected bool? _ByFinancialPeriod;
  protected bool? _SummaryByDay;
  protected bool? _IncludeUnreleased;

  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<INRegister.refNbr, Where<INRegister.docType, Equal<INDocType.transfer>>>))]
  public virtual string TransferNbr
  {
    get => this._TransferNbr;
    set => this._TransferNbr = value;
  }

  [FinPeriodSelector(typeof (AccessInfo.businessDate))]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
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
  [AnyInventory(typeof (Search<InventoryItem.inventoryID, Where2<Where<InventoryItem.stkItem, NotEqual<boolFalse>, Or<InventoryItem.isConverted, Equal<True>>>, And<Where<Match<Current<AccessInfo.userName>>>>>>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItemRawExt(typeof (InventoryTranDetEnqFilter.inventoryID), DisplayName = "Subitem")]
  public virtual string SubItemCD
  {
    get => this._SubItemCD;
    set => this._SubItemCD = value;
  }

  [PXDBString(30, IsUnicode = true)]
  public virtual string SubItemCDWildcard
  {
    get => SubCDUtils.CreateSubCDWildcard(this._SubItemCD, "INSUBITEM");
  }

  [Site(DescriptionField = typeof (INSite.descr), Required = false, DisplayName = "Warehouse")]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [Location(typeof (InventoryTranDetEnqFilter.siteID))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PX.Objects.IN.LotSerialNbr]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXDBString(100, IsUnicode = true)]
  public virtual string LotSerialNbrWildcard
  {
    get
    {
      return PXDatabase.Provider.SqlDialect.WildcardAnything + this._LotSerialNbr + PXDatabase.Provider.SqlDialect.WildcardAnything;
    }
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "By Financial Period")]
  public virtual bool? ByFinancialPeriod
  {
    get => this._ByFinancialPeriod;
    set => this._ByFinancialPeriod = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Summary by Day")]
  public virtual bool? SummaryByDay
  {
    get => this._SummaryByDay;
    set => this._SummaryByDay = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IncludeUnreleased
  {
    get => this._IncludeUnreleased;
    set => this._IncludeUnreleased = value;
  }

  [OrganizationTree(null, true, FieldClass = "MultipleBaseCurrencies", Required = true)]
  public int? OrgBAccountID { get; set; }

  public abstract class transferNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranDetEnqFilter.transferNbr>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranDetEnqFilter.finPeriodID>
  {
  }

  public abstract class periodStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryTranDetEnqFilter.periodStartDate>
  {
  }

  public abstract class periodEndDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryTranDetEnqFilter.periodEndDate>
  {
  }

  public abstract class periodEndDateInclusive : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryTranDetEnqFilter.periodEndDateInclusive>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryTranDetEnqFilter.startDate>
  {
  }

  public abstract class endDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryTranDetEnqFilter.endDate>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryTranDetEnqFilter.inventoryID>
  {
  }

  public abstract class subItemCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranDetEnqFilter.subItemCD>
  {
  }

  public abstract class subItemCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranDetEnqFilter.subItemCDWildcard>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryTranDetEnqFilter.siteID>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryTranDetEnqFilter.locationID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranDetEnqFilter.lotSerialNbr>
  {
  }

  public abstract class lotSerialNbrWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranDetEnqFilter.lotSerialNbrWildcard>
  {
  }

  public abstract class byFinancialPeriod : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryTranDetEnqFilter.byFinancialPeriod>
  {
  }

  public abstract class summaryByDay : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryTranDetEnqFilter.summaryByDay>
  {
  }

  public abstract class includeUnreleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryTranDetEnqFilter.includeUnreleased>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }
}

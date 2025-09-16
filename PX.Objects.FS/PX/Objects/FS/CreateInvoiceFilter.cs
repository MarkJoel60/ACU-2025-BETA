// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CreateInvoiceFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class CreateInvoiceFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private DateTime? _UpToDate;

  [PXString(2, IsFixed = true)]
  [FSPostTo.List]
  [PXDefault("AA")]
  [PXUIField(DisplayName = "Generated Billing Documents")]
  public virtual 
  #nullable disable
  string PostTo { get; set; }

  [PXInt]
  [PXDefault(typeof (AccessInfo.branchID))]
  [PXUIField(DisplayName = "Branch")]
  public virtual int? BranchID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Billing Customer")]
  [FSSelectorCustomer]
  public virtual int? CustomerID { get; set; }

  /// <exclude />
  [PXDate]
  [PXUIField(DisplayName = "From Date")]
  public virtual DateTime? FromDate { get; set; }

  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Up to Date")]
  public virtual DateTime? UpToDate
  {
    get => this._UpToDate;
    set
    {
      this._UpToDate = value;
      DateTime dateTime = this._UpToDate.Value;
      int year = dateTime.Year;
      dateTime = this._UpToDate.Value;
      int month = dateTime.Month;
      dateTime = this._UpToDate.Value;
      int day = dateTime.Day;
      this.UpToDateWithTimeZone = new DateTime?(new DateTime(year, month, day, 23, 59, 59));
    }
  }

  [PXDate]
  public virtual DateTime? UpToDateWithTimeZone { get; set; }

  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Billing Date")]
  public virtual DateTime? InvoiceDate { get; set; }

  [SMOpenPeriod(typeof (CreateInvoiceFilter.invoiceDate), typeof (CreateInvoiceFilter.postTo), typeof (CreateInvoiceFilter.branchID), null, null, null, null, true, false)]
  [PXUIField]
  public virtual string InvoiceFinPeriodID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Billing Cycle")]
  [PXSelector(typeof (FSBillingCycle.billingCycleID), SubstituteKey = typeof (FSBillingCycle.billingCycleCD), DescriptionField = typeof (FSBillingCycle.descr))]
  public virtual int? BillingCycleID { get; set; }

  [PXDBBoolInvert]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Time Frame Grouping")]
  public virtual bool? IgnoreBillingCycles { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Filter Manually", Visible = false)]
  public virtual bool? LoadData { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (Where<CreateInvoiceFilter.prepareInvoice, Equal<True>>))]
  [PXFormula(typeof (Default<CreateInvoiceFilter.prepareInvoice>))]
  [PXUIField(DisplayName = "Release Invoice")]
  public virtual bool? ReleaseInvoice { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Email Invoice")]
  public virtual bool? EmailInvoice { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Release Bill")]
  public virtual bool? ReleaseBill { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Pay Bill")]
  public virtual bool? PayBill { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Prepare Invoice")]
  [PXUIEnabled(typeof (Where<CreateInvoiceFilter.sOQuickProcess, Equal<False>>))]
  public virtual bool? PrepareInvoice { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Email Sales Order/Quote")]
  public virtual bool? EmailSalesOrder { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Sales Order Quick Processing")]
  [PXUIEnabled(typeof (Where<CreateInvoiceFilter.prepareInvoice, Equal<False>>))]
  public virtual bool? SOQuickProcess { get; set; }

  public abstract class postTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CreateInvoiceFilter.postTo>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CreateInvoiceFilter.branchID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CreateInvoiceFilter.customerID>
  {
  }

  /// <exclude />
  public abstract class fromDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CreateInvoiceFilter.fromDate>
  {
  }

  public abstract class upToDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CreateInvoiceFilter.upToDate>
  {
  }

  public abstract class upToDateWithTimeZone : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CreateInvoiceFilter.upToDateWithTimeZone>
  {
  }

  public abstract class invoiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CreateInvoiceFilter.invoiceDate>
  {
  }

  public abstract class invoiceFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CreateInvoiceFilter.invoiceFinPeriodID>
  {
  }

  public abstract class billingCycleID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CreateInvoiceFilter.billingCycleID>
  {
  }

  public abstract class ignoreBillingCycles : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateInvoiceFilter.ignoreBillingCycles>
  {
  }

  public abstract class loadData : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CreateInvoiceFilter.loadData>
  {
  }

  public abstract class releaseInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateInvoiceFilter.releaseInvoice>
  {
  }

  public abstract class emailInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateInvoiceFilter.emailInvoice>
  {
  }

  public abstract class releaseBill : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CreateInvoiceFilter.releaseBill>
  {
  }

  public abstract class payBill : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CreateInvoiceFilter.payBill>
  {
  }

  public abstract class prepareInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateInvoiceFilter.prepareInvoice>
  {
  }

  public abstract class emailSalesOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateInvoiceFilter.emailSalesOrder>
  {
  }

  public abstract class sOQuickProcess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateInvoiceFilter.sOQuickProcess>
  {
  }
}

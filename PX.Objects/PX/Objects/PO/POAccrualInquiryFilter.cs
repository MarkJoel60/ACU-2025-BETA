// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POAccrualInquiryFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("Purchase Accrual Balance Filter")]
[Serializable]
public class POAccrualInquiryFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Organization(false, Required = false)]
  public virtual int? OrganizationID { get; set; }

  [BranchOfOrganization(typeof (POAccrualInquiryFilter.organizationID), false, null, null)]
  public virtual int? BranchID { get; set; }

  [OrganizationTree(typeof (POAccrualInquiryFilter.organizationID), typeof (POAccrualInquiryFilter.branchID), null, false)]
  public virtual int? OrgBAccountID { get; set; }

  [Vendor(DescriptionField = typeof (PX.Objects.AP.Vendor.acctName))]
  public virtual int? VendorID { get; set; }

  [Account(null, DescriptionField = typeof (PX.Objects.GL.Account.description))]
  [PXDefault]
  public virtual int? AcctID { get; set; }

  [SubAccount(DisplayName = "Sub.", DescriptionField = typeof (Sub.description))]
  public virtual int? SubID { get; set; }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Subaccount", FieldClass = "SUBACCOUNT")]
  [PXDimension("SUBACCOUNT", ValidComboRequired = false)]
  public virtual 
  #nullable disable
  string SubCD { get; set; }

  [PXDBString(30, IsUnicode = true)]
  public virtual string SubCDWildcard
  {
    [PXDependsOnFields(new Type[] {typeof (POAccrualInquiryFilter.subCD)})] get
    {
      return SubCDUtils.CreateSubCDWildcard(this.SubCD, "SUBACCOUNT");
    }
    set
    {
    }
  }

  [PXBool]
  public bool? UseMasterCalendar { get; set; }

  [AnyPeriodFilterable(null, null, typeof (POAccrualInquiryFilter.branchID), null, typeof (POAccrualInquiryFilter.organizationID), typeof (POAccrualInquiryFilter.useMasterCalendar), null, false, null, null)]
  [PXUIField(DisplayName = "Period")]
  [PXDefault(typeof (Coalesce<Search<FinPeriod.finPeriodID, Where<FinPeriod.organizationID, Equal<Current<POAccrualInquiryFilter.organizationID>>, And<FinPeriod.startDate, LessEqual<Current<AccessInfo.businessDate>>>>, OrderBy<Desc<FinPeriod.startDate, Desc<FinPeriod.finPeriodID>>>>, Search<FinPeriod.finPeriodID, Where<FinPeriod.organizationID, Equal<Zero>, And<FinPeriod.startDate, LessEqual<Current<AccessInfo.businessDate>>>>, OrderBy<Desc<FinPeriod.startDate, Desc<FinPeriod.finPeriodID>>>>>))]
  public virtual string FinPeriodID { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Show Details by Line")]
  public virtual bool? ShowByLines { get; set; }

  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unbilled Total", Enabled = false)]
  public virtual Decimal? UnbilledAmt { get; set; }

  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Not Received Total", Enabled = false)]
  public virtual Decimal? NotReceivedAmt { get; set; }

  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Drop-Ship Total Not Invoiced", Enabled = false)]
  public virtual Decimal? NotInvoicedAmt { get; set; }

  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "IN Adjustment Total Not Released", Enabled = false)]
  public virtual Decimal? NotAdjustedAmt { get; set; }

  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "PO Accrued Total", Enabled = false)]
  public virtual Decimal? Balance { get; set; }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POAccrualInquiryFilter.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualInquiryFilter.branchID>
  {
  }

  public abstract class orgBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POAccrualInquiryFilter.orgBAccountID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualInquiryFilter.vendorID>
  {
  }

  public abstract class acctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualInquiryFilter.acctID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualInquiryFilter.subID>
  {
  }

  public abstract class subCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualInquiryFilter.subCD>
  {
  }

  public abstract class subCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualInquiryFilter.subCDWildcard>
  {
  }

  public abstract class useMasterCalendar : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POAccrualInquiryFilter.useMasterCalendar>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualInquiryFilter.finPeriodID>
  {
  }

  public abstract class showByLines : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POAccrualInquiryFilter.showByLines>
  {
  }

  public abstract class unbilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryFilter.unbilledAmt>
  {
  }

  public abstract class notReceivedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryFilter.notReceivedAmt>
  {
  }

  public abstract class notInvoicedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryFilter.notInvoicedAmt>
  {
  }

  public abstract class notAdjustedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryFilter.notAdjustedAmt>
  {
  }

  public abstract class balance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualInquiryFilter.balance>
  {
  }
}

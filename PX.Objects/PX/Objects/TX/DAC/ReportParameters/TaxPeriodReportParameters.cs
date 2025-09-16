// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.DAC.ReportParameters.TaxPeriodReportParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.TX.Descriptor;
using System;

#nullable enable
namespace PX.Objects.TX.DAC.ReportParameters;

public class TaxPeriodReportParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _VendorID;
  protected 
  #nullable disable
  string _TaxPeriodID;

  [Organization(false)]
  public int? OrganizationID { get; set; }

  [TaxPeriodFilterBranch(typeof (TaxPeriodReportParameters.organizationID), false)]
  public int? BranchID { get; set; }

  [Vendor(typeof (Search<PX.Objects.AP.Vendor.bAccountID, Where<PX.Objects.AP.Vendor.taxAgency, Equal<True>>>), DisplayName = "Tax Agency")]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXSelector(typeof (Search<TaxPeriod.taxPeriodID, Where<TaxPeriod.organizationID, Equal<Optional2<TaxPeriodForReportShowing.organizationID>>, And<TaxPeriod.vendorID, Equal<Optional2<TaxPeriodForReportShowing.vendorID>>, And<TaxPeriod.status, NotEqual<TaxPeriodStatus.open>>>>>), new Type[] {typeof (TaxPeriod.taxPeriodID), typeof (TaxPeriod.startDateUI), typeof (TaxPeriod.endDateUI), typeof (TaxPeriod.status)})]
  [PXUIField]
  public virtual string TaxPeriodID
  {
    get => this._TaxPeriodID;
    set => this._TaxPeriodID = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXSelector(typeof (Search<TaxPeriod.taxPeriodID, Where<TaxPeriod.organizationID, Suit<Optional2<TaxPeriodReportParameters.orgBAccountID>>, And<TaxPeriod.vendorID, Equal<Optional2<TaxPeriodReportParameters.vendorID>>, And<TaxPeriod.status, NotEqual<TaxPeriodStatus.open>>>>>), new Type[] {typeof (TaxPeriod.taxPeriodID), typeof (TaxPeriod.startDateUI), typeof (TaxPeriod.endDateUI), typeof (TaxPeriod.status)})]
  [PXUIField]
  public virtual string TaxPeriodIDByBAccount { get; set; }

  [OrganizationTree(typeof (TaxPeriodReportParameters.organizationID), typeof (TaxPeriodReportParameters.branchID), typeof (TaxTreeSelect), true, SelectionMode = BaseOrganizationTreeAttribute.SelectionModes.Branches)]
  [PXUIRequired(typeof (FeatureInstalled<FeaturesSet.multipleBaseCurrencies>))]
  public int? OrgBAccountID { get; set; }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxPeriodReportParameters.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxPeriodReportParameters.branchID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxPeriodReportParameters.vendorID>
  {
  }

  public abstract class taxPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxPeriodReportParameters.taxPeriodID>
  {
  }

  public abstract class taxPeriodIDByBAccount : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxPeriodReportParameters.taxPeriodID>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }
}

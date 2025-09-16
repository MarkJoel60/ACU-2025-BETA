// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxPeriodForReportShowing
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.TX;

[PXProjection(typeof (Select<TaxPeriod, Where<TaxPeriod.status, NotEqual<TaxPeriodStatus.open>>>))]
public class TaxPeriodForReportShowing : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _VendorID;
  protected 
  #nullable disable
  string _TaxPeriodID;
  protected string _Status;
  protected DateTime? _EndDate;
  protected int? _RevisionID;

  [Organization(true, IsKey = true, BqlField = typeof (TaxPeriod.organizationID))]
  public virtual int? OrganizationID { get; set; }

  [Vendor(typeof (Search<PX.Objects.AP.Vendor.bAccountID, Where<PX.Objects.AP.Vendor.taxAgency, Equal<True>>>), IsKey = true, BqlField = typeof (TaxPeriod.vendorID), DisplayName = "Tax Agency")]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (TaxPeriod.taxPeriodID))]
  [PXSelector(typeof (Search<TaxPeriod.taxPeriodID, Where<TaxPeriod.organizationID, Equal<Optional2<TaxPeriodForReportShowing.organizationID>>, And<TaxPeriod.vendorID, Equal<Optional2<TaxPeriodForReportShowing.vendorID>>, And<TaxPeriod.status, NotEqual<TaxPeriodStatus.open>>>>>), new Type[] {typeof (TaxPeriod.taxPeriodID), typeof (TaxPeriod.startDateUI), typeof (TaxPeriod.endDateUI), typeof (TaxPeriod.status)})]
  [PXUIField]
  public virtual string TaxPeriodID
  {
    get => this._TaxPeriodID;
    set => this._TaxPeriodID = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (TaxPeriod.status))]
  [PXDefault("N")]
  [TaxPeriodStatus.List]
  [PXUIField]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBDate(BqlField = typeof (TaxPeriod.endDate))]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXSelector(typeof (Search5<TaxHistory.revisionID, InnerJoin<PX.Objects.GL.Branch, On<TaxHistory.branchID, Equal<PX.Objects.GL.Branch.branchID>>>, Where<PX.Objects.GL.Branch.organizationID, Equal<Optional<TaxPeriodForReportShowing.organizationID>>, And<TaxHistory.vendorID, Equal<Optional<TaxPeriodForReportShowing.vendorID>>, And<Where<TaxHistory.taxPeriodID, Equal<Optional<TaxPeriodForReportShowing.taxPeriodID>>>>>>, Aggregate<GroupBy<TaxHistory.revisionID>>>))]
  [PXUIField]
  [PXInt]
  public virtual int? RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxPeriodForReportShowing.organizationID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxPeriodForReportShowing.vendorID>
  {
  }

  public abstract class taxPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxPeriodForReportShowing.taxPeriodID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxPeriodForReportShowing.status>
  {
  }

  public abstract class endDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxPeriodForReportShowing.endDate>
  {
  }

  public abstract class revisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxPeriodForReportShowing.revisionID>
  {
  }
}

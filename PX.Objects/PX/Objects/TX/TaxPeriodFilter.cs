// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxPeriodFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.TX.Descriptor;
using System;

#nullable enable
namespace PX.Objects.TX;

[Serializable]
public class TaxPeriodFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _VendorID;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected int? _RevisionId;
  protected bool? _ShowDifference;
  protected 
  #nullable disable
  string _PreparedWarningMsg;

  [Organization(true, IsDBField = false, FieldClass = "MULTICOMPANY")]
  public virtual int? OrganizationID { get; set; }

  [TaxPeriodFilterBranch(typeof (TaxPeriodFilter.organizationID), true, IsDBField = false)]
  public virtual int? BranchID { get; set; }

  [OrganizationBAccountID(typeof (TaxPeriodFilter.organizationID), typeof (TaxPeriodFilter.branchID))]
  public int? OrgBAccountID { get; set; }

  [TaxAgencyActive]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDefault]
  [PXUIField]
  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXSelector(typeof (Search<TaxPeriod.taxPeriodID, Where<TaxPeriod.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>, And<TaxPeriod.organizationID, Equal<Current<TaxPeriodFilter.organizationID>>>>>), new Type[] {typeof (TaxPeriod.taxPeriodID), typeof (TaxPeriod.startDateUI), typeof (TaxPeriod.endDateUI), typeof (TaxPeriod.status)})]
  public virtual string TaxPeriodID { get; set; }

  [PXDate]
  [PXUIField]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDate]
  [PXUIField]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXInt]
  [PXUnboundDefault]
  [PXSelector(typeof (Search4<TaxReport.revisionID, Where<TaxReport.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>, And<TaxReport.validFrom, LessEqual<Current<TaxPeriodFilter.endDate>>>>, Aggregate<Max<TaxReport.validFrom>>, OrderBy<Desc<TaxReport.validFrom>>>))]
  public virtual int? TaxReportRevisionID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Revision")]
  [PXSelector(typeof (Search5<TaxHistory.revisionID, InnerJoin<PX.Objects.GL.Branch, On<TaxHistory.branchID, Equal<PX.Objects.GL.Branch.branchID>>>, Where<PX.Objects.GL.Branch.organizationID, Equal<Current2<TaxPeriodFilter.organizationID>>, And2<Where<TaxHistory.branchID, Equal<Current2<TaxPeriodFilter.branchID>>, Or<Current2<TaxPeriodFilter.branchID>, IsNull>>, And<TaxHistory.vendorID, Equal<Current2<TaxPeriodFilter.vendorID>>, And<TaxHistory.taxReportRevisionID, Equal<Current2<TaxPeriodFilter.taxReportRevisionID>>, And<TaxHistory.taxPeriodID, Equal<Current2<TaxPeriodFilter.taxPeriodID>>>>>>>, Aggregate<GroupBy<TaxHistory.revisionID>>, OrderBy<Asc<TaxHistory.revisionID>>>))]
  public virtual int? RevisionId
  {
    get => this._RevisionId;
    set => this._RevisionId = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show Difference", Visible = false)]
  public virtual bool? ShowDifference
  {
    get => this._ShowDifference;
    set => this._ShowDifference = value;
  }

  [PXString]
  public virtual string PreparedWarningMsg
  {
    get => this._PreparedWarningMsg;
    set => this._PreparedWarningMsg = value;
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxPeriodFilter.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxPeriodFilter.branchID>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxPeriodFilter.vendorID>
  {
  }

  public abstract class taxPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxPeriodFilter.taxPeriodID>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxPeriodFilter.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxPeriodFilter.endDate>
  {
  }

  public abstract class taxReportRevisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxPeriodFilter.taxReportRevisionID>
  {
  }

  public abstract class revisionId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxPeriodFilter.revisionId>
  {
  }

  public abstract class showDifference : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    TaxPeriodFilter.showDifference>
  {
  }

  public abstract class preparedWarningMsg : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxPeriodFilter.preparedWarningMsg>
  {
  }
}

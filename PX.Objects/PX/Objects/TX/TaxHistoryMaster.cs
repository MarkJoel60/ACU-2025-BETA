// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxHistoryMaster
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.TX.Descriptor;
using System;

#nullable enable
namespace PX.Objects.TX;

[Serializable]
public class TaxHistoryMaster : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected int? _VendorID;
  protected 
  #nullable disable
  string _TaxPeriodID;
  protected int? _LineNbr;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;

  [Organization(true, IsDBField = false, FieldClass = "MULTICOMPANY")]
  public virtual int? OrganizationID { get; set; }

  [TaxPeriodFilterBranch(typeof (TaxHistoryMaster.organizationID), true, IsDBField = false)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [OrganizationBAccountID(typeof (TaxHistoryMaster.organizationID), typeof (TaxHistoryMaster.branchID))]
  public int? OrgBAccountID { get; set; }

  [Vendor(typeof (Search<PX.Objects.AP.Vendor.bAccountID, Where<PX.Objects.AP.Vendor.taxAgency, Equal<True>>>), DisplayName = "Tax Agency", Required = true)]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<TaxPeriod.taxPeriodID, Where<TaxPeriod.vendorID, Equal<Current<TaxHistoryMaster.vendorID>>, And<TaxPeriod.organizationID, Equal<Current<TaxHistoryMaster.organizationID>>, And<Where<TaxPeriod.status, Equal<TaxPeriodStatus.prepared>, Or<TaxPeriod.status, Equal<TaxPeriodStatus.closed>>>>>>>), new Type[] {typeof (TaxPeriod.taxPeriodID), typeof (TaxPeriod.startDateUI), typeof (TaxPeriod.endDateUI), typeof (TaxPeriod.status)})]
  public virtual string TaxPeriodID
  {
    get => this._TaxPeriodID;
    set => this._TaxPeriodID = value;
  }

  [PXDBInt]
  [PXUIField]
  [PXIntList(new int[] {0}, new string[] {"undefined"})]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  /// <summary>
  /// The field used to display and edit the <see cref="P:PX.Objects.TX.TaxHistoryMaster.StartDate" /> of the period (inclusive) in the UI.
  /// </summary>
  /// <value>
  /// Depends on and changes the value of the <see cref="P:PX.Objects.TX.TaxHistoryMaster.StartDate" /> field, performing additional transformations.
  /// </value>
  [PXDate]
  [PXUIField]
  public virtual DateTime? StartDateUI
  {
    [PXDependsOnFields(new Type[] {typeof (TaxHistoryMaster.startDate), typeof (TaxHistoryMaster.endDate)})] get
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
        DateTime? endDateInclusive = this.EndDateInclusive;
        if ((nullable2.HasValue == endDateInclusive.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == endDateInclusive.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
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

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDate]
  [PXUIField]
  public virtual DateTime? EndDateInclusive
  {
    get
    {
      return this._EndDate.HasValue ? new DateTime?(this._EndDate.Value.AddDays(-1.0)) : new DateTime?();
    }
    set
    {
      this._EndDate = !value.HasValue ? new DateTime?() : new DateTime?(value.Value.AddDays(1.0));
    }
  }

  [PXInt]
  public virtual int? TaxReportRevisionID { get; set; }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxHistoryMaster.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxHistoryMaster.branchID>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxHistoryMaster.vendorID>
  {
  }

  public abstract class taxPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxHistoryMaster.taxPeriodID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxHistoryMaster.lineNbr>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxHistoryMaster.startDate>
  {
  }

  public abstract class startDateUI : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxHistoryMaster.startDateUI>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxHistoryMaster.endDate>
  {
  }

  public abstract class endDateInclusive : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxHistoryMaster.endDateInclusive>
  {
  }

  public abstract class taxReportRevisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxHistoryMaster.taxReportRevisionID>
  {
  }
}

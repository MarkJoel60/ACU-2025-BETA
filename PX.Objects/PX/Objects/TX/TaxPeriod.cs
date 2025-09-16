// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.TX;

/// <summary>
/// Represent an agency tax period at the company level.
/// The instance of DAC is created on the Prepare Tax Report(TX501000) page when the user selects a tax agency.
/// </summary>
[PXCacheName("Tax Period")]
[Serializable]
public class TaxPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _VendorID;
  protected 
  #nullable disable
  string _TaxPeriodID;
  protected string _TaxYear;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected string _Status;
  protected bool? _Filed;
  protected byte[] _tstamp;

  /// <summary>
  /// The reference to the <see cref="!:Organization" /> record to which the record belongs.
  /// </summary>
  [Organization(true, IsKey = true)]
  [PXParent(typeof (SelectFromBase<PX.Objects.GL.DAC.Organization, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.GL.DAC.Organization.organizationID, IBqlInt>.IsEqual<BqlField<TaxPeriod.organizationID, IBqlInt>.FromCurrent>>))]
  public virtual int? OrganizationID { get; set; }

  /// <summary>
  /// <see cref="!:PX.Objects.AP.Vendor.BAccountID" /> of a tax agency to which the tax period belongs.
  /// </summary>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  /// <summary>Identifier of the tax period.</summary>
  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  public virtual string TaxPeriodID
  {
    get => this._TaxPeriodID;
    set => this._TaxPeriodID = value;
  }

  /// <summary>
  /// Identifier of a <see cref="T:PX.Objects.TX.TaxYear" /> to which the tax period belongs.
  /// </summary>
  [PXDBString(4, IsFixed = true)]
  [PXDefault]
  public virtual string TaxYear
  {
    get => this._TaxYear;
    set => this._TaxYear = value;
  }

  /// <summary>The start date of the period.</summary>
  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  /// <summary>The end date of the period (exclusive).</summary>
  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  /// <summary>
  /// The field used to display and edit the <see cref="P:PX.Objects.TX.TaxPeriod.StartDate" /> of the period (inclusive) in the UI.
  /// </summary>
  /// <value>
  /// Depends on and changes the value of the <see cref="P:PX.Objects.TX.TaxPeriod.StartDate" /> field, performing additional transformations.
  /// </value>
  [PXDate]
  [PXUIField]
  public virtual DateTime? StartDateUI
  {
    [PXDependsOnFields(new Type[] {typeof (TaxPeriod.startDate), typeof (TaxPeriod.endDate)})] get
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

  /// <summary>
  /// The field that is used to display <see cref="P:PX.Objects.TX.TaxPeriod.EndDate" /> of the period (inclusive) in the UI.
  /// </summary>
  /// <value>
  /// Depends on and changes the value of the <see cref="P:PX.Objects.TX.TaxPeriod.EndDate" /> field by performing additional transformations.
  /// </value>
  [PXDate]
  [PXUIField]
  public virtual DateTime? EndDateUI
  {
    [PXDependsOnFields(new Type[] {typeof (TaxPeriod.endDate)})] get
    {
      ref DateTime? local = ref this._EndDate;
      return !local.HasValue ? new DateTime?() : new DateTime?(local.GetValueOrDefault().AddDays(-1.0));
    }
  }

  /// <summary>The status of the tax period.</summary>
  /// <value>
  /// The field can have the following values:
  /// <c>"N"</c> - Open.
  /// <c>"P"</c> - Prepared.
  /// <c>"C"</c> - Closed.
  /// <c>"D"</c> - Dummy. This status is used to show periods without <see cref="T:PX.Objects.TX.TaxTran" /> on the Prepare Tax Report (TX501000) page.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [TaxPeriodStatus.List]
  [PXUIField]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  /// <summary>The field is not used.</summary>
  [Obsolete("This property is obsolete and will be removed in Acumatica 8.0")]
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Filed
  {
    get => this._Filed;
    set => this._Filed = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<TaxPeriod>.By<TaxPeriod.organizationID, TaxPeriod.vendorID, TaxPeriod.taxPeriodID>
  {
    public static TaxPeriod Find(
      PXGraph graph,
      int? organizationID,
      int? vendorID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (TaxPeriod) PrimaryKeyOf<TaxPeriod>.By<TaxPeriod.organizationID, TaxPeriod.vendorID, TaxPeriod.taxPeriodID>.FindBy(graph, (object) organizationID, (object) vendorID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class Organization : 
      PrimaryKeyOf<PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<TaxPeriod>.By<TaxPeriod.organizationID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<TaxPeriod>.By<TaxPeriod.vendorID>
    {
    }

    public class FinYear : 
      PrimaryKeyOf<PX.Objects.TX.TaxYear>.By<PX.Objects.TX.TaxYear.organizationID, PX.Objects.TX.TaxYear.vendorID, PX.Objects.TX.TaxYear.year>.ForeignKeyOf<TaxPeriod>.By<TaxPeriod.organizationID, TaxPeriod.vendorID, TaxPeriod.taxYear>
    {
    }
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxPeriod.organizationID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxPeriod.vendorID>
  {
  }

  public abstract class taxPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxPeriod.taxPeriodID>
  {
  }

  public abstract class taxYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxPeriod.taxYear>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxPeriod.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxPeriod.endDate>
  {
  }

  public abstract class startDateUI : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxPeriod.startDateUI>
  {
  }

  public abstract class endDateUI : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxPeriod.endDateUI>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxPeriod.status>
  {
    public const string DefaultStatus = "N";
  }

  public abstract class filed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxPeriod.filed>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TaxPeriod.Tstamp>
  {
  }
}

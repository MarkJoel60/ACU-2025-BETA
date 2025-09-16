// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxYear
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL.Attributes;
using PX.Objects.TX.Descriptor;
using System;

#nullable enable
namespace PX.Objects.TX;

[PXCacheName("Tax Year")]
[Serializable]
public class TaxYear : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Filed;
  protected 
  #nullable disable
  string _TaxPeriodType;
  protected byte[] _tstamp;

  /// <summary>
  /// The reference to the <see cref="!:Organization" /> record to which the record belongs.
  /// </summary>
  [Organization(true, IsKey = true)]
  [PXDefault]
  public virtual int? OrganizationID { get; set; }

  /// <summary>
  /// The reference to the tax agency (<see cref="T:PX.Objects.AP.Vendor" />) record to which the record belongs.
  /// </summary>
  [TaxAgencyActive(IsKey = true)]
  [PXDefault]
  [PXForeignReference(typeof (TaxYear.FK.Vendor))]
  public virtual int? VendorID { get; set; }

  [PXUIField(DisplayName = "Tax Year")]
  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXSelector(typeof (Search<TaxYear.year, Where<TaxYear.organizationID, Equal<Optional<TaxYear.organizationID>>, And<TaxYear.vendorID, Equal<Optional<TaxYear.vendorID>>>>>), new Type[] {typeof (TaxYear.year)})]
  public virtual string Year { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? StartDate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Filed
  {
    get => this._Filed;
    set => this._Filed = value;
  }

  /// <summary>The actual count of periods of the year.</summary>
  [PXDBInt]
  public virtual int? PeriodsCount { get; set; }

  [PXDBInt]
  public virtual int? PlanPeriodsCount { get; set; }

  /// <summary>The calendar type of the tax year.</summary>
  [PXDBString(1)]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Tax Period Type")]
  [VendorTaxPeriodType.List]
  public virtual string TaxPeriodType
  {
    get => this._TaxPeriodType;
    set => this._TaxPeriodType = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<TaxYear>.By<TaxYear.organizationID, TaxYear.vendorID, TaxYear.year>
  {
    public static TaxYear Find(
      PXGraph graph,
      int? organizationID,
      int? vendorID,
      string year,
      PKFindOptions options = 0)
    {
      return (TaxYear) PrimaryKeyOf<TaxYear>.By<TaxYear.organizationID, TaxYear.vendorID, TaxYear.year>.FindBy(graph, (object) organizationID, (object) vendorID, (object) year, options);
    }
  }

  public static class FK
  {
    public class Organization : 
      PrimaryKeyOf<PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<TaxYear>.By<TaxYear.organizationID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<TaxYear>.By<TaxYear.vendorID>
    {
    }
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxYear.organizationID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxYear.vendorID>
  {
  }

  public abstract class year : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxYear.year>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxYear.startDate>
  {
  }

  public abstract class filed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxYear.filed>
  {
  }

  public abstract class periodsCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxYear.periodsCount>
  {
  }

  public abstract class planPeriodsCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxYear.planPeriodsCount>
  {
  }

  public abstract class taxPeriodType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxYear.taxPeriodType>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TaxYear.Tstamp>
  {
  }
}

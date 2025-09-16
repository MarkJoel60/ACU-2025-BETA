// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxRecord
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.TX;

[DebuggerDisplay("{TaxID}={Rate}")]
[Serializable]
public class TaxRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TaxID;
  protected string _Description;
  protected Decimal? _Rate;
  protected DateTime? _EffectiveDate;
  protected Decimal? _PreviousRate;
  protected Decimal? _TaxableMax;
  protected Decimal? _RateOverMax;
  protected bool? _IsTaxable;
  protected bool? _IsFreight;
  protected bool? _IsService;
  protected bool? _IsLabor;

  [PXString(60, IsKey = true, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax ID")]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDecimal(5)]
  [PXUIField(DisplayName = "Rate")]
  public virtual Decimal? Rate
  {
    get => this._Rate;
    set => this._Rate = value;
  }

  [PXDate]
  [PXUIField]
  public virtual DateTime? EffectiveDate
  {
    get => this._EffectiveDate;
    set => this._EffectiveDate = value;
  }

  [PXDecimal(5)]
  [PXUIField(DisplayName = "Previous Rate")]
  public virtual Decimal? PreviousRate
  {
    get => this._PreviousRate;
    set => this._PreviousRate = value;
  }

  [PXDecimal(4)]
  [PXUIField(DisplayName = "Taxable Max.")]
  public virtual Decimal? TaxableMax
  {
    get => this._TaxableMax;
    set => this._TaxableMax = value;
  }

  [PXDecimal(5)]
  [PXUIField(DisplayName = "Rate Over Max.")]
  public virtual Decimal? RateOverMax
  {
    get => this._RateOverMax;
    set => this._RateOverMax = value;
  }

  public string CountyCode { get; set; }

  public string CityCode { get; set; }

  public string CountyName { get; set; }

  public string CityName { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Taxable")]
  public virtual bool? IsTaxable
  {
    get => this._IsTaxable;
    set => this._IsTaxable = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Freight")]
  public virtual bool? IsFreight
  {
    get => this._IsFreight;
    set => this._IsFreight = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Service")]
  public virtual bool? IsService
  {
    get => this._IsService;
    set => this._IsService = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Labor")]
  public virtual bool? IsLabor
  {
    get => this._IsLabor;
    set => this._IsLabor = value;
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxRecord.taxID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxRecord.description>
  {
  }

  public abstract class rate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxRecord.rate>
  {
  }

  public abstract class effectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxRecord.effectiveDate>
  {
  }

  public abstract class previousRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxRecord.previousRate>
  {
  }

  public abstract class taxableMax : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxRecord.taxableMax>
  {
  }

  public abstract class rateOverMax : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxRecord.rateOverMax>
  {
  }

  public abstract class isTaxable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxRecord.isTaxable>
  {
  }

  public abstract class isFreight : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxRecord.isFreight>
  {
  }

  public abstract class isService : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxRecord.isService>
  {
  }

  public abstract class isLabor : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxRecord.isLabor>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TXImportFileData
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.TX;

[Serializable]
public class TXImportFileData : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RecordID;
  protected 
  #nullable disable
  string _StateCode;
  protected string _StateName;
  protected string _CityName;
  protected string _CountyName;
  protected string _ZipCode;
  protected string _Origin;
  protected string _TaxFreight;
  protected string _TaxServices;
  protected string _SignatureCode;
  protected Decimal? _StateSalesTaxRate;
  protected DateTime? _StateSalesTaxRateEffectiveDate;
  protected Decimal? _StateSalesTaxPreviousRate;
  protected Decimal? _StateUseTaxRate;
  protected DateTime? _StateUseTaxRateEffectiveDate;
  protected Decimal? _StateUseTaxPreviousRate;
  protected Decimal? _StateTaxableMaximum;
  protected Decimal? _StateTaxOverMaximumRate;
  protected string _SignatureCodeCity;
  protected string _CityTaxCodeAssignedByState;
  protected string _CityLocalRegister;
  protected Decimal? _CitySalesTaxRate;
  protected DateTime? _CitySalesTaxRateEffectiveDate;
  protected Decimal? _CitySalesTaxPreviousRate;
  protected Decimal? _CityUseTaxRate;
  protected DateTime? _CityUseTaxRateEffectiveDate;
  protected Decimal? _CityUseTaxPreviousRate;
  protected Decimal? _CityTaxableMaximum;
  protected Decimal? _CityTaxOverMaximumRate;
  protected string _SignatureCodeCounty;
  protected string _CountyTaxCodeAssignedByState;
  protected string _CountyLocalRegister;
  protected Decimal? _CountySalesTaxRate;
  protected DateTime? _CountySalesTaxRateEffectiveDate;
  protected Decimal? _CountySalesTaxPreviousRate;
  protected Decimal? _CountyUseTaxRate;
  protected DateTime? _CountyUseTaxRateEffectiveDate;
  protected Decimal? _CountyUseTaxPreviousRate;
  protected Decimal? _CountyTaxableMaximum;
  protected Decimal? _CountyTaxOverMaximumRate;
  protected string _SignatureCodeTransit;
  protected string _TransitTaxCodeAssignedByState;
  protected Decimal? _TransitSalesTaxRate;
  protected DateTime? _TransitSalesTaxRateEffectiveDate;
  protected Decimal? _TransitSalesTaxPreviousRate;
  protected Decimal? _TransitUseTaxRate;
  protected DateTime? _TransitUseTaxRateEffectiveDate;
  protected Decimal? _TransitUseTaxPreviousRate;
  protected string _TransitTaxIsCity;
  protected string _SignatureCodeOther1;
  protected string _OtherTaxCode1AssignedByState;
  protected Decimal? _Other1SalesTaxRate;
  protected DateTime? _Other1SalesTaxRateEffectiveDate;
  protected Decimal? _Other1SalesTaxPreviousRate;
  protected Decimal? _Other1UseTaxRate;
  protected DateTime? _Other1UseTaxRateEffectiveDate;
  protected Decimal? _Other1UseTaxPreviousRate;
  protected string _Other1TaxIsCity;
  protected string _SignatureCodeOther2;
  protected string _OtherTaxCode2AssignedByState;
  protected Decimal? _Other2SalesTaxRate;
  protected DateTime? _Other2SalesTaxRateEffectiveDate;
  protected Decimal? _Other2SalesTaxPreviousRate;
  protected Decimal? _Other2UseTaxRate;
  protected DateTime? _Other2UseTaxRateEffectiveDate;
  protected Decimal? _Other2UseTaxPreviousRate;
  protected string _Other2TaxIsCity;
  protected string _SignatureCodeOther3;
  protected string _OtherTaxCode3AssignedByState;
  protected Decimal? _Other3SalesTaxRate;
  protected DateTime? _Other3SalesTaxRateEffectiveDate;
  protected Decimal? _Other3SalesTaxPreviousRate;
  protected Decimal? _Other3UseTaxRate;
  protected DateTime? _Other3UseTaxRateEffectiveDate;
  protected Decimal? _Other3UseTaxPreviousRate;
  protected string _Other3TaxIsCity;
  protected string _SignatureCodeOther4;
  protected string _OtherTaxCode4AssignedByState;
  protected Decimal? _Other4SalesTaxRate;
  protected DateTime? _Other4SalesTaxRateEffectiveDate;
  protected Decimal? _Other4SalesTaxPreviousRate;
  protected Decimal? _Other4UseTaxRate;
  protected DateTime? _Other4UseTaxRateEffectiveDate;
  protected Decimal? _Other4UseTaxPreviousRate;
  protected string _Other4TaxIsCity;
  protected Decimal? _CombinedSalesTaxRate;
  protected DateTime? _CombinedSalesTaxRateEffectiveDate;
  protected Decimal? _CombinedSalesTaxPreviousRate;
  protected Decimal? _CombinedUseTaxRate;
  protected DateTime? _CombinedUseTaxRateEffectiveDate;
  protected Decimal? _CombinedUseTaxPreviousRate;
  protected DateTime? _DateLastUpdated;
  protected string _DeleteCode;

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "RecordID")]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PXDBString(25)]
  [PXUIField]
  public virtual string StateCode
  {
    get => this._StateCode;
    set => this._StateCode = value;
  }

  [PXDBString(25)]
  [PXUIField]
  public virtual string StateName
  {
    get => this._StateName;
    set => this._StateName = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "CityName")]
  public virtual string CityName
  {
    get => this._CityName;
    set => this._CityName = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "CountyName")]
  public virtual string CountyName
  {
    get => this._CountyName;
    set => this._CountyName = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "ZipCode")]
  public virtual string ZipCode
  {
    get => this._ZipCode;
    set => this._ZipCode = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "Origin")]
  public virtual string Origin
  {
    get => this._Origin;
    set => this._Origin = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "TaxFreight")]
  public virtual string TaxFreight
  {
    get => this._TaxFreight;
    set => this._TaxFreight = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "TaxServices")]
  public virtual string TaxServices
  {
    get => this._TaxServices;
    set => this._TaxServices = value;
  }

  [PXDBString(25)]
  [PXDefault("")]
  [PXUIField(DisplayName = "SignatureCode")]
  public virtual string SignatureCode
  {
    get => this._SignatureCode;
    set => this._SignatureCode = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "StateSalesTaxRate")]
  public virtual Decimal? StateSalesTaxRate
  {
    get => this._StateSalesTaxRate;
    set => this._StateSalesTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "StateSalesTaxRateEffectiveDate")]
  public virtual DateTime? StateSalesTaxRateEffectiveDate
  {
    get => this._StateSalesTaxRateEffectiveDate;
    set => this._StateSalesTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "StateSalesTaxPreviousRate")]
  public virtual Decimal? StateSalesTaxPreviousRate
  {
    get => this._StateSalesTaxPreviousRate;
    set => this._StateSalesTaxPreviousRate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "StateUseTaxRate")]
  public virtual Decimal? StateUseTaxRate
  {
    get => this._StateUseTaxRate;
    set => this._StateUseTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "StateUseTaxRateEffectiveDate")]
  public virtual DateTime? StateUseTaxRateEffectiveDate
  {
    get => this._StateUseTaxRateEffectiveDate;
    set => this._StateUseTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "StateUseTaxPreviousRate")]
  public virtual Decimal? StateUseTaxPreviousRate
  {
    get => this._StateUseTaxPreviousRate;
    set => this._StateUseTaxPreviousRate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "StateTaxableMaximum")]
  public virtual Decimal? StateTaxableMaximum
  {
    get => this._StateTaxableMaximum;
    set => this._StateTaxableMaximum = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "StateTaxOverMaximumRate")]
  public virtual Decimal? StateTaxOverMaximumRate
  {
    get => this._StateTaxOverMaximumRate;
    set => this._StateTaxOverMaximumRate = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "SignatureCodeCity")]
  public virtual string SignatureCodeCity
  {
    get => this._SignatureCodeCity;
    set => this._SignatureCodeCity = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "CityTaxCodeAssignedByState")]
  public virtual string CityTaxCodeAssignedByState
  {
    get => this._CityTaxCodeAssignedByState;
    set => this._CityTaxCodeAssignedByState = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "CityLocalRegister")]
  public virtual string CityLocalRegister
  {
    get => this._CityLocalRegister;
    set => this._CityLocalRegister = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CitySalesTaxRate")]
  public virtual Decimal? CitySalesTaxRate
  {
    get => this._CitySalesTaxRate;
    set => this._CitySalesTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "CitySalesTaxRateEffectiveDate")]
  public virtual DateTime? CitySalesTaxRateEffectiveDate
  {
    get => this._CitySalesTaxRateEffectiveDate;
    set => this._CitySalesTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CitySalesTaxPreviousRate")]
  public virtual Decimal? CitySalesTaxPreviousRate
  {
    get => this._CitySalesTaxPreviousRate;
    set => this._CitySalesTaxPreviousRate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CityUseTaxRate")]
  public virtual Decimal? CityUseTaxRate
  {
    get => this._CityUseTaxRate;
    set => this._CityUseTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "CityUseTaxRateEffectiveDate")]
  public virtual DateTime? CityUseTaxRateEffectiveDate
  {
    get => this._CityUseTaxRateEffectiveDate;
    set => this._CityUseTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CityUseTaxPreviousRate")]
  public virtual Decimal? CityUseTaxPreviousRate
  {
    get => this._CityUseTaxPreviousRate;
    set => this._CityUseTaxPreviousRate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CityTaxableMaximum")]
  public virtual Decimal? CityTaxableMaximum
  {
    get => this._CityTaxableMaximum;
    set => this._CityTaxableMaximum = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CityTaxOverMaximumRate")]
  public virtual Decimal? CityTaxOverMaximumRate
  {
    get => this._CityTaxOverMaximumRate;
    set => this._CityTaxOverMaximumRate = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "SignatureCodeCounty")]
  public virtual string SignatureCodeCounty
  {
    get => this._SignatureCodeCounty;
    set => this._SignatureCodeCounty = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "CountyTaxCodeAssignedByState")]
  public virtual string CountyTaxCodeAssignedByState
  {
    get => this._CountyTaxCodeAssignedByState;
    set => this._CountyTaxCodeAssignedByState = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "CountyLocalRegister")]
  public virtual string CountyLocalRegister
  {
    get => this._CountyLocalRegister;
    set => this._CountyLocalRegister = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CountySalesTaxRate")]
  public virtual Decimal? CountySalesTaxRate
  {
    get => this._CountySalesTaxRate;
    set => this._CountySalesTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "CountySalesTaxRateEffectiveDate")]
  public virtual DateTime? CountySalesTaxRateEffectiveDate
  {
    get => this._CountySalesTaxRateEffectiveDate;
    set => this._CountySalesTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CountySalesTaxPreviousRate")]
  public virtual Decimal? CountySalesTaxPreviousRate
  {
    get => this._CountySalesTaxPreviousRate;
    set => this._CountySalesTaxPreviousRate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CountyUseTaxRate")]
  public virtual Decimal? CountyUseTaxRate
  {
    get => this._CountyUseTaxRate;
    set => this._CountyUseTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "CountyUseTaxRateEffectiveDate")]
  public virtual DateTime? CountyUseTaxRateEffectiveDate
  {
    get => this._CountyUseTaxRateEffectiveDate;
    set => this._CountyUseTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CountyUseTaxPreviousRate")]
  public virtual Decimal? CountyUseTaxPreviousRate
  {
    get => this._CountyUseTaxPreviousRate;
    set => this._CountyUseTaxPreviousRate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CountyTaxableMaximum")]
  public virtual Decimal? CountyTaxableMaximum
  {
    get => this._CountyTaxableMaximum;
    set => this._CountyTaxableMaximum = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CountyTaxOverMaximumRate")]
  public virtual Decimal? CountyTaxOverMaximumRate
  {
    get => this._CountyTaxOverMaximumRate;
    set => this._CountyTaxOverMaximumRate = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "SignatureCodeTransit")]
  public virtual string SignatureCodeTransit
  {
    get => this._SignatureCodeTransit;
    set => this._SignatureCodeTransit = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "TransitTaxCodeAssignedByState")]
  public virtual string TransitTaxCodeAssignedByState
  {
    get => this._TransitTaxCodeAssignedByState;
    set => this._TransitTaxCodeAssignedByState = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "TransitSalesTaxRate")]
  public virtual Decimal? TransitSalesTaxRate
  {
    get => this._TransitSalesTaxRate;
    set => this._TransitSalesTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "TransitSalesTaxRateEffectiveDate")]
  public virtual DateTime? TransitSalesTaxRateEffectiveDate
  {
    get => this._TransitSalesTaxRateEffectiveDate;
    set => this._TransitSalesTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "TransitSalesTaxPreviousRate")]
  public virtual Decimal? TransitSalesTaxPreviousRate
  {
    get => this._TransitSalesTaxPreviousRate;
    set => this._TransitSalesTaxPreviousRate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "TransitUseTaxRate")]
  public virtual Decimal? TransitUseTaxRate
  {
    get => this._TransitUseTaxRate;
    set => this._TransitUseTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "TransitUseTaxRateEffectiveDate")]
  public virtual DateTime? TransitUseTaxRateEffectiveDate
  {
    get => this._TransitUseTaxRateEffectiveDate;
    set => this._TransitUseTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "TransitUseTaxPreviousRate")]
  public virtual Decimal? TransitUseTaxPreviousRate
  {
    get => this._TransitUseTaxPreviousRate;
    set => this._TransitUseTaxPreviousRate = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "TransitTaxIsCity")]
  public virtual string TransitTaxIsCity
  {
    get => this._TransitTaxIsCity;
    set => this._TransitTaxIsCity = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "SignatureCodeOther1")]
  public virtual string SignatureCodeOther1
  {
    get => this._SignatureCodeOther1;
    set => this._SignatureCodeOther1 = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "OtherTaxCode1AssignedByState")]
  public virtual string OtherTaxCode1AssignedByState
  {
    get => this._OtherTaxCode1AssignedByState;
    set => this._OtherTaxCode1AssignedByState = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Other1SalesTaxRate")]
  public virtual Decimal? Other1SalesTaxRate
  {
    get => this._Other1SalesTaxRate;
    set => this._Other1SalesTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Other1SalesTaxRateEffectiveDate")]
  public virtual DateTime? Other1SalesTaxRateEffectiveDate
  {
    get => this._Other1SalesTaxRateEffectiveDate;
    set => this._Other1SalesTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Other1SalesTaxPreviousRate")]
  public virtual Decimal? Other1SalesTaxPreviousRate
  {
    get => this._Other1SalesTaxPreviousRate;
    set => this._Other1SalesTaxPreviousRate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Other1UseTaxRate")]
  public virtual Decimal? Other1UseTaxRate
  {
    get => this._Other1UseTaxRate;
    set => this._Other1UseTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Other1UseTaxRateEffectiveDate")]
  public virtual DateTime? Other1UseTaxRateEffectiveDate
  {
    get => this._Other1UseTaxRateEffectiveDate;
    set => this._Other1UseTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Other1UseTaxPreviousRate")]
  public virtual Decimal? Other1UseTaxPreviousRate
  {
    get => this._Other1UseTaxPreviousRate;
    set => this._Other1UseTaxPreviousRate = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "Other1TaxIsCity")]
  public virtual string Other1TaxIsCity
  {
    get => this._Other1TaxIsCity;
    set => this._Other1TaxIsCity = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "SignatureCodeOther2")]
  public virtual string SignatureCodeOther2
  {
    get => this._SignatureCodeOther2;
    set => this._SignatureCodeOther2 = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "OtherTaxCode2AssignedByState")]
  public virtual string OtherTaxCode2AssignedByState
  {
    get => this._OtherTaxCode2AssignedByState;
    set => this._OtherTaxCode2AssignedByState = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Other2SalesTaxRate")]
  public virtual Decimal? Other2SalesTaxRate
  {
    get => this._Other2SalesTaxRate;
    set => this._Other2SalesTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Other2SalesTaxRateEffectiveDate")]
  public virtual DateTime? Other2SalesTaxRateEffectiveDate
  {
    get => this._Other2SalesTaxRateEffectiveDate;
    set => this._Other2SalesTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Other2SalesTaxPreviousRate")]
  public virtual Decimal? Other2SalesTaxPreviousRate
  {
    get => this._Other2SalesTaxPreviousRate;
    set => this._Other2SalesTaxPreviousRate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Other2UseTaxRate")]
  public virtual Decimal? Other2UseTaxRate
  {
    get => this._Other2UseTaxRate;
    set => this._Other2UseTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Other2UseTaxRateEffectiveDate")]
  public virtual DateTime? Other2UseTaxRateEffectiveDate
  {
    get => this._Other2UseTaxRateEffectiveDate;
    set => this._Other2UseTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Other2UseTaxPreviousRate")]
  public virtual Decimal? Other2UseTaxPreviousRate
  {
    get => this._Other2UseTaxPreviousRate;
    set => this._Other2UseTaxPreviousRate = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "Other2TaxIsCity")]
  public virtual string Other2TaxIsCity
  {
    get => this._Other2TaxIsCity;
    set => this._Other2TaxIsCity = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "SignatureCodeOther3")]
  public virtual string SignatureCodeOther3
  {
    get => this._SignatureCodeOther3;
    set => this._SignatureCodeOther3 = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "OtherTaxCode3AssignedByState")]
  public virtual string OtherTaxCode3AssignedByState
  {
    get => this._OtherTaxCode3AssignedByState;
    set => this._OtherTaxCode3AssignedByState = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Other3SalesTaxRate")]
  public virtual Decimal? Other3SalesTaxRate
  {
    get => this._Other3SalesTaxRate;
    set => this._Other3SalesTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Other3SalesTaxRateEffectiveDate")]
  public virtual DateTime? Other3SalesTaxRateEffectiveDate
  {
    get => this._Other3SalesTaxRateEffectiveDate;
    set => this._Other3SalesTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Other3SalesTaxPreviousRate")]
  public virtual Decimal? Other3SalesTaxPreviousRate
  {
    get => this._Other3SalesTaxPreviousRate;
    set => this._Other3SalesTaxPreviousRate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Other3UseTaxRate")]
  public virtual Decimal? Other3UseTaxRate
  {
    get => this._Other3UseTaxRate;
    set => this._Other3UseTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Other3UseTaxRateEffectiveDate")]
  public virtual DateTime? Other3UseTaxRateEffectiveDate
  {
    get => this._Other3UseTaxRateEffectiveDate;
    set => this._Other3UseTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Other3UseTaxPreviousRate")]
  public virtual Decimal? Other3UseTaxPreviousRate
  {
    get => this._Other3UseTaxPreviousRate;
    set => this._Other3UseTaxPreviousRate = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "Other3TaxIsCity")]
  public virtual string Other3TaxIsCity
  {
    get => this._Other3TaxIsCity;
    set => this._Other3TaxIsCity = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "SignatureCodeOther4")]
  public virtual string SignatureCodeOther4
  {
    get => this._SignatureCodeOther4;
    set => this._SignatureCodeOther4 = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "OtherTaxCode4AssignedByState")]
  public virtual string OtherTaxCode4AssignedByState
  {
    get => this._OtherTaxCode4AssignedByState;
    set => this._OtherTaxCode4AssignedByState = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Other4SalesTaxRate")]
  public virtual Decimal? Other4SalesTaxRate
  {
    get => this._Other4SalesTaxRate;
    set => this._Other4SalesTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Other4SalesTaxRateEffectiveDate")]
  public virtual DateTime? Other4SalesTaxRateEffectiveDate
  {
    get => this._Other4SalesTaxRateEffectiveDate;
    set => this._Other4SalesTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Other4SalesTaxPreviousRate")]
  public virtual Decimal? Other4SalesTaxPreviousRate
  {
    get => this._Other4SalesTaxPreviousRate;
    set => this._Other4SalesTaxPreviousRate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Other4UseTaxRate")]
  public virtual Decimal? Other4UseTaxRate
  {
    get => this._Other4UseTaxRate;
    set => this._Other4UseTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Other4UseTaxRateEffectiveDate")]
  public virtual DateTime? Other4UseTaxRateEffectiveDate
  {
    get => this._Other4UseTaxRateEffectiveDate;
    set => this._Other4UseTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Other4UseTaxPreviousRate")]
  public virtual Decimal? Other4UseTaxPreviousRate
  {
    get => this._Other4UseTaxPreviousRate;
    set => this._Other4UseTaxPreviousRate = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "Other4TaxIsCity")]
  public virtual string Other4TaxIsCity
  {
    get => this._Other4TaxIsCity;
    set => this._Other4TaxIsCity = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CombinedSalesTaxRate")]
  public virtual Decimal? CombinedSalesTaxRate
  {
    get => this._CombinedSalesTaxRate;
    set => this._CombinedSalesTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "CombinedSalesTaxRateEffectiveDate")]
  public virtual DateTime? CombinedSalesTaxRateEffectiveDate
  {
    get => this._CombinedSalesTaxRateEffectiveDate;
    set => this._CombinedSalesTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CombinedSalesTaxPreviousRate")]
  public virtual Decimal? CombinedSalesTaxPreviousRate
  {
    get => this._CombinedSalesTaxPreviousRate;
    set => this._CombinedSalesTaxPreviousRate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CombinedUseTaxRate")]
  public virtual Decimal? CombinedUseTaxRate
  {
    get => this._CombinedUseTaxRate;
    set => this._CombinedUseTaxRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "CombinedUseTaxRateEffectiveDate")]
  public virtual DateTime? CombinedUseTaxRateEffectiveDate
  {
    get => this._CombinedUseTaxRateEffectiveDate;
    set => this._CombinedUseTaxRateEffectiveDate = value;
  }

  [PXDBDecimal(5)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CombinedUseTaxPreviousRate")]
  public virtual Decimal? CombinedUseTaxPreviousRate
  {
    get => this._CombinedUseTaxPreviousRate;
    set => this._CombinedUseTaxPreviousRate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "DateLastUpdated")]
  public virtual DateTime? DateLastUpdated
  {
    get => this._DateLastUpdated;
    set => this._DateLastUpdated = value;
  }

  [PXDBString(25)]
  [PXUIField(DisplayName = "DeleteCode")]
  public virtual string DeleteCode
  {
    get => this._DeleteCode;
    set => this._DeleteCode = value;
  }

  public class PK : PrimaryKeyOf<TXImportFileData>.By<TXImportFileData.recordID>
  {
    public static TXImportFileData Find(PXGraph graph, int? recordID, PKFindOptions options = 0)
    {
      return (TXImportFileData) PrimaryKeyOf<TXImportFileData>.By<TXImportFileData.recordID>.FindBy(graph, (object) recordID, options);
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TXImportFileData.recordID>
  {
  }

  public abstract class stateCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TXImportFileData.stateCode>
  {
  }

  public abstract class stateName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TXImportFileData.stateName>
  {
  }

  public abstract class cityName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TXImportFileData.cityName>
  {
  }

  public abstract class countyName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TXImportFileData.countyName>
  {
  }

  public abstract class zipCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TXImportFileData.zipCode>
  {
  }

  public abstract class origin : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TXImportFileData.origin>
  {
  }

  public abstract class taxFreight : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TXImportFileData.taxFreight>
  {
  }

  public abstract class taxServices : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TXImportFileData.taxServices>
  {
  }

  public abstract class signatureCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.signatureCode>
  {
  }

  public abstract class stateSalesTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.stateSalesTaxRate>
  {
  }

  public abstract class stateSalesTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.stateSalesTaxRateEffectiveDate>
  {
  }

  public abstract class stateSalesTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.stateSalesTaxPreviousRate>
  {
  }

  public abstract class stateUseTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.stateUseTaxRate>
  {
  }

  public abstract class stateUseTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.stateUseTaxRateEffectiveDate>
  {
  }

  public abstract class stateUseTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.stateUseTaxPreviousRate>
  {
  }

  public abstract class stateTaxableMaximum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.stateTaxableMaximum>
  {
  }

  public abstract class stateTaxOverMaximumRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.stateTaxOverMaximumRate>
  {
  }

  public abstract class signatureCodeCity : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.signatureCodeCity>
  {
  }

  public abstract class cityTaxCodeAssignedByState : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.cityTaxCodeAssignedByState>
  {
  }

  public abstract class cityLocalRegister : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.cityLocalRegister>
  {
  }

  public abstract class citySalesTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.citySalesTaxRate>
  {
  }

  public abstract class citySalesTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.citySalesTaxRateEffectiveDate>
  {
  }

  public abstract class citySalesTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.citySalesTaxPreviousRate>
  {
  }

  public abstract class cityUseTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.cityUseTaxRate>
  {
  }

  public abstract class cityUseTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.cityUseTaxRateEffectiveDate>
  {
  }

  public abstract class cityUseTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.cityUseTaxPreviousRate>
  {
  }

  public abstract class cityTaxableMaximum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.cityTaxableMaximum>
  {
  }

  public abstract class cityTaxOverMaximumRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.cityTaxOverMaximumRate>
  {
  }

  public abstract class signatureCodeCounty : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.signatureCodeCounty>
  {
  }

  public abstract class countyTaxCodeAssignedByState : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.countyTaxCodeAssignedByState>
  {
  }

  public abstract class countyLocalRegister : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.countyLocalRegister>
  {
  }

  public abstract class countySalesTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.countySalesTaxRate>
  {
  }

  public abstract class countySalesTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.countySalesTaxRateEffectiveDate>
  {
  }

  public abstract class countySalesTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.countySalesTaxPreviousRate>
  {
  }

  public abstract class countyUseTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.countyUseTaxRate>
  {
  }

  public abstract class countyUseTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.countyUseTaxRateEffectiveDate>
  {
  }

  public abstract class countyUseTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.countyUseTaxPreviousRate>
  {
  }

  public abstract class countyTaxableMaximum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.countyTaxableMaximum>
  {
  }

  public abstract class countyTaxOverMaximumRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.countyTaxOverMaximumRate>
  {
  }

  public abstract class signatureCodeTransit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.signatureCodeTransit>
  {
  }

  public abstract class transitTaxCodeAssignedByState : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.transitTaxCodeAssignedByState>
  {
  }

  public abstract class transitSalesTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.transitSalesTaxRate>
  {
  }

  public abstract class transitSalesTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.transitSalesTaxRateEffectiveDate>
  {
  }

  public abstract class transitSalesTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.transitSalesTaxPreviousRate>
  {
  }

  public abstract class transitUseTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.transitUseTaxRate>
  {
  }

  public abstract class transitUseTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.transitUseTaxRateEffectiveDate>
  {
  }

  public abstract class transitUseTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.transitUseTaxPreviousRate>
  {
  }

  public abstract class transitTaxIsCity : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.transitTaxIsCity>
  {
  }

  public abstract class signatureCodeOther1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.signatureCodeOther1>
  {
  }

  public abstract class otherTaxCode1AssignedByState : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.otherTaxCode1AssignedByState>
  {
  }

  public abstract class other1SalesTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.other1SalesTaxRate>
  {
  }

  public abstract class other1SalesTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.other1SalesTaxRateEffectiveDate>
  {
  }

  public abstract class other1SalesTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.other1SalesTaxPreviousRate>
  {
  }

  public abstract class other1UseTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.other1UseTaxRate>
  {
  }

  public abstract class other1UseTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.other1UseTaxRateEffectiveDate>
  {
  }

  public abstract class other1UseTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.other1UseTaxPreviousRate>
  {
  }

  public abstract class other1TaxIsCity : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.other1TaxIsCity>
  {
  }

  public abstract class signatureCodeOther2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.signatureCodeOther2>
  {
  }

  public abstract class otherTaxCode2AssignedByState : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.otherTaxCode2AssignedByState>
  {
  }

  public abstract class other2SalesTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.other2SalesTaxRate>
  {
  }

  public abstract class other2SalesTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.other2SalesTaxRateEffectiveDate>
  {
  }

  public abstract class other2SalesTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.other2SalesTaxPreviousRate>
  {
  }

  public abstract class other2UseTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.other2UseTaxRate>
  {
  }

  public abstract class other2UseTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.other2UseTaxRateEffectiveDate>
  {
  }

  public abstract class other2UseTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.other2UseTaxPreviousRate>
  {
  }

  public abstract class other2TaxIsCity : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.other2TaxIsCity>
  {
  }

  public abstract class signatureCodeOther3 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.signatureCodeOther3>
  {
  }

  public abstract class otherTaxCode3AssignedByState : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.otherTaxCode3AssignedByState>
  {
  }

  public abstract class other3SalesTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.other3SalesTaxRate>
  {
  }

  public abstract class other3SalesTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.other3SalesTaxRateEffectiveDate>
  {
  }

  public abstract class other3SalesTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.other3SalesTaxPreviousRate>
  {
  }

  public abstract class other3UseTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.other3UseTaxRate>
  {
  }

  public abstract class other3UseTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.other3UseTaxRateEffectiveDate>
  {
  }

  public abstract class other3UseTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.other3UseTaxPreviousRate>
  {
  }

  public abstract class other3TaxIsCity : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.other3TaxIsCity>
  {
  }

  public abstract class signatureCodeOther4 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.signatureCodeOther4>
  {
  }

  public abstract class otherTaxCode4AssignedByState : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.otherTaxCode4AssignedByState>
  {
  }

  public abstract class other4SalesTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.other4SalesTaxRate>
  {
  }

  public abstract class other4SalesTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.other4SalesTaxRateEffectiveDate>
  {
  }

  public abstract class other4SalesTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.other4SalesTaxPreviousRate>
  {
  }

  public abstract class other4UseTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.other4UseTaxRate>
  {
  }

  public abstract class other4UseTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.other4UseTaxRateEffectiveDate>
  {
  }

  public abstract class other4UseTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.other4UseTaxPreviousRate>
  {
  }

  public abstract class other4TaxIsCity : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportFileData.other4TaxIsCity>
  {
  }

  public abstract class combinedSalesTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.combinedSalesTaxRate>
  {
  }

  public abstract class combinedSalesTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.combinedSalesTaxRateEffectiveDate>
  {
  }

  public abstract class combinedSalesTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.combinedSalesTaxPreviousRate>
  {
  }

  public abstract class combinedUseTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.combinedUseTaxRate>
  {
  }

  public abstract class combinedUseTaxRateEffectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.combinedUseTaxRateEffectiveDate>
  {
  }

  public abstract class combinedUseTaxPreviousRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TXImportFileData.combinedUseTaxPreviousRate>
  {
  }

  public abstract class dateLastUpdated : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXImportFileData.dateLastUpdated>
  {
  }

  public abstract class deleteCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TXImportFileData.deleteCode>
  {
  }
}

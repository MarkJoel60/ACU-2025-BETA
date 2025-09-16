// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.GenericTaxBuilder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.TX;

public class GenericTaxBuilder : TaxBuilder
{
  protected string State { get; set; }

  public GenericTaxBuilder(
    string state,
    List<TXImportFileData> data,
    List<TXImportZipFileData> zipData)
    : base(data, zipData)
  {
    this.State = state;
    this.taxes = new List<TaxRecord>(200);
    this.zones = new List<ZoneRecord>(200);
    this.zoneDetails = new List<ZoneDetailRecord>(500);
    this.zoneZips = new List<ZoneZipRecord>(5000);
    this.zoneZipPlus = new Dictionary<ZoneRecord, IList<ZoneZipPlusRecord>>(500);
  }

  protected override void ExecuteBuilder()
  {
    TaxRecord tax1 = (TaxRecord) null;
    TaxRecord tax2 = (TaxRecord) null;
    ZoneRecord zone1 = (ZoneRecord) null;
    Dictionary<string, ZoneRecord> dictionary1 = new Dictionary<string, ZoneRecord>();
    Dictionary<string, TaxRecord> dictionary2 = new Dictionary<string, TaxRecord>();
    Dictionary<string, TaxRecord> dictionary3 = new Dictionary<string, TaxRecord>();
    Dictionary<string, TaxRecord> dictionary4 = new Dictionary<string, TaxRecord>();
    Dictionary<string, TaxRecord> dictionary5 = new Dictionary<string, TaxRecord>();
    Dictionary<string, TaxRecord> dictionary6 = new Dictionary<string, TaxRecord>();
    Dictionary<string, TaxRecord> dictionary7 = new Dictionary<string, TaxRecord>();
    Dictionary<string, TaxRecord> dictionary8 = new Dictionary<string, TaxRecord>();
    Dictionary<string, TaxRecord> dictionary9 = new Dictionary<string, TaxRecord>();
    Dictionary<string, TaxRecord> dictionary10 = new Dictionary<string, TaxRecord>();
    Dictionary<string, TaxRecord> dictionary11 = new Dictionary<string, TaxRecord>();
    Dictionary<string, TaxRecord> dictionary12 = new Dictionary<string, TaxRecord>();
    Dictionary<string, TaxRecord> dictionary13 = new Dictionary<string, TaxRecord>();
    foreach (TXImportFileData txImportFileData in this.data)
    {
      string key = this.State;
      List<TaxRecord> taxRecordList = new List<TaxRecord>();
      if (!TaxBuilder.HasTax(txImportFileData))
      {
        if (tax2 == null)
        {
          tax2 = this.AppendTax(this.State + "ZERO", "NO Tax", txImportFileData.CombinedSalesTaxRate, txImportFileData.CombinedSalesTaxRateEffectiveDate, txImportFileData.CombinedSalesTaxPreviousRate);
          this.SetFlags(tax2, txImportFileData);
          zone1 = new ZoneRecord();
          zone1.ZoneID = "NO Tax";
          zone1.Description = "No Tax";
          zone1.CombinedRate = txImportFileData.CombinedSalesTaxRate;
          this.zones.Add(zone1);
          this.AppendZoneDetail(zone1, tax2);
        }
        this.AppendZip(zone1, txImportFileData);
      }
      else
      {
        if (tax1 == null)
        {
          tax1 = this.AppendTax(this.State + "STATE", this.State + " State Tax");
          tax1.Rate = txImportFileData.StateSalesTaxRate;
          tax1.PreviousRate = txImportFileData.StateSalesTaxPreviousRate;
          tax1.EffectiveDate = txImportFileData.StateSalesTaxRateEffectiveDate;
          this.SetFlags(tax1, txImportFileData);
        }
        Decimal? nullable = txImportFileData.StateSalesTaxRate;
        Decimal num1 = 0M;
        if (nullable.GetValueOrDefault() > num1 & nullable.HasValue)
          taxRecordList.Add(tax1);
        nullable = txImportFileData.CountySalesTaxRate;
        Decimal num2 = 0M;
        if (!(nullable.GetValueOrDefault() > num2 & nullable.HasValue))
        {
          nullable = txImportFileData.CountySalesTaxPreviousRate;
          Decimal num3 = 0M;
          if (!(nullable.GetValueOrDefault() > num3 & nullable.HasValue))
            goto label_17;
        }
        if (!string.IsNullOrEmpty(txImportFileData.SignatureCodeCounty))
          key = txImportFileData.SignatureCodeCounty;
        if (!dictionary2.ContainsKey(txImportFileData.SignatureCodeCounty))
        {
          TaxRecord tax3 = this.AppendTax(txImportFileData.StateCode + txImportFileData.SignatureCodeCounty, "County Tax " + txImportFileData.CountyName);
          tax3.Rate = txImportFileData.CountySalesTaxRate;
          tax3.PreviousRate = txImportFileData.CountySalesTaxPreviousRate;
          tax3.EffectiveDate = txImportFileData.CountySalesTaxRateEffectiveDate;
          this.SetFlags(tax3, txImportFileData);
          dictionary2.Add(txImportFileData.SignatureCodeCounty, tax3);
          taxRecordList.Add(tax3);
          tax3.CountyCode = txImportFileData.SignatureCodeCounty;
          tax3.CountyName = txImportFileData.CountyName;
        }
        else
          taxRecordList.Add(dictionary2[txImportFileData.SignatureCodeCounty]);
label_17:
        if (txImportFileData.TransitTaxIsCity == "C")
        {
          nullable = txImportFileData.TransitSalesTaxRate;
          Decimal num4 = 0M;
          if (!(nullable.GetValueOrDefault() > num4 & nullable.HasValue))
          {
            nullable = txImportFileData.TransitSalesTaxPreviousRate;
            Decimal num5 = 0M;
            if (!(nullable.GetValueOrDefault() > num5 & nullable.HasValue))
              goto label_25;
          }
          if (!string.IsNullOrEmpty(txImportFileData.SignatureCodeTransit))
            key = txImportFileData.SignatureCodeTransit;
          if (!dictionary3.ContainsKey(txImportFileData.SignatureCodeTransit))
          {
            TaxRecord tax4 = this.AppendTax($"{txImportFileData.StateCode}CT{txImportFileData.SignatureCodeTransit}", "Transit Tax " + txImportFileData.CityName);
            tax4.Rate = txImportFileData.TransitSalesTaxRate;
            tax4.PreviousRate = txImportFileData.TransitSalesTaxPreviousRate;
            tax4.EffectiveDate = txImportFileData.TransitSalesTaxRateEffectiveDate;
            this.SetFlags(tax4, txImportFileData);
            dictionary3.Add(txImportFileData.SignatureCodeTransit, tax4);
            taxRecordList.Add(tax4);
            tax4.CountyCode = txImportFileData.SignatureCodeTransit;
            tax4.CountyName = txImportFileData.CountyName;
          }
          else
            taxRecordList.Add(dictionary3[txImportFileData.SignatureCodeTransit]);
        }
label_25:
        if (txImportFileData.Other1TaxIsCity == "C")
        {
          nullable = txImportFileData.Other1SalesTaxRate;
          Decimal num6 = 0M;
          if (!(nullable.GetValueOrDefault() > num6 & nullable.HasValue))
          {
            nullable = txImportFileData.Other1SalesTaxPreviousRate;
            Decimal num7 = 0M;
            if (!(nullable.GetValueOrDefault() > num7 & nullable.HasValue))
              goto label_33;
          }
          if (!string.IsNullOrEmpty(txImportFileData.SignatureCodeOther1))
            key = txImportFileData.SignatureCodeOther1;
          if (!dictionary4.ContainsKey(txImportFileData.SignatureCodeOther1))
          {
            TaxRecord tax5 = this.AppendTax($"{txImportFileData.StateCode}CO1{txImportFileData.SignatureCodeOther1}", "County Other 1 Tax " + txImportFileData.CountyName);
            tax5.Rate = txImportFileData.Other1SalesTaxRate;
            tax5.PreviousRate = txImportFileData.Other1SalesTaxPreviousRate;
            tax5.EffectiveDate = txImportFileData.Other1SalesTaxRateEffectiveDate;
            this.SetFlags(tax5, txImportFileData);
            dictionary4.Add(txImportFileData.SignatureCodeOther1, tax5);
            taxRecordList.Add(tax5);
            tax5.CountyCode = txImportFileData.SignatureCodeOther1;
            tax5.CountyName = txImportFileData.CountyName;
          }
          else
            taxRecordList.Add(dictionary4[txImportFileData.SignatureCodeOther1]);
        }
label_33:
        if (txImportFileData.Other2TaxIsCity == "C")
        {
          nullable = txImportFileData.Other2SalesTaxRate;
          Decimal num8 = 0M;
          if (!(nullable.GetValueOrDefault() > num8 & nullable.HasValue))
          {
            nullable = txImportFileData.Other2SalesTaxPreviousRate;
            Decimal num9 = 0M;
            if (!(nullable.GetValueOrDefault() > num9 & nullable.HasValue))
              goto label_41;
          }
          if (!string.IsNullOrEmpty(txImportFileData.SignatureCodeOther2))
            key = txImportFileData.SignatureCodeOther2;
          if (!dictionary5.ContainsKey(txImportFileData.SignatureCodeOther2))
          {
            TaxRecord tax6 = this.AppendTax($"{txImportFileData.StateCode}CO2{txImportFileData.SignatureCodeOther2}", "County Other 2 Tax " + txImportFileData.CountyName);
            tax6.Rate = txImportFileData.Other2SalesTaxRate;
            tax6.PreviousRate = txImportFileData.Other2SalesTaxPreviousRate;
            tax6.EffectiveDate = txImportFileData.Other2SalesTaxRateEffectiveDate;
            this.SetFlags(tax6, txImportFileData);
            dictionary5.Add(txImportFileData.SignatureCodeOther2, tax6);
            taxRecordList.Add(tax6);
            tax6.CountyCode = txImportFileData.SignatureCodeOther2;
            tax6.CountyName = txImportFileData.CountyName;
          }
          else
            taxRecordList.Add(dictionary5[txImportFileData.SignatureCodeOther2]);
        }
label_41:
        if (txImportFileData.Other3TaxIsCity == "C")
        {
          nullable = txImportFileData.Other3SalesTaxRate;
          Decimal num10 = 0M;
          if (!(nullable.GetValueOrDefault() > num10 & nullable.HasValue))
          {
            nullable = txImportFileData.Other3SalesTaxPreviousRate;
            Decimal num11 = 0M;
            if (!(nullable.GetValueOrDefault() > num11 & nullable.HasValue))
              goto label_49;
          }
          if (!string.IsNullOrEmpty(txImportFileData.SignatureCodeOther3))
            key = txImportFileData.SignatureCodeOther3;
          if (!dictionary6.ContainsKey(txImportFileData.SignatureCodeOther3))
          {
            TaxRecord tax7 = this.AppendTax($"{txImportFileData.StateCode}CO3{txImportFileData.SignatureCodeOther3}", "County Other 3 Tax " + txImportFileData.CountyName);
            tax7.Rate = txImportFileData.Other3SalesTaxRate;
            tax7.PreviousRate = txImportFileData.Other3SalesTaxPreviousRate;
            tax7.EffectiveDate = txImportFileData.Other3SalesTaxRateEffectiveDate;
            this.SetFlags(tax7, txImportFileData);
            dictionary6.Add(txImportFileData.SignatureCodeOther3, tax7);
            taxRecordList.Add(tax7);
            tax7.CountyCode = txImportFileData.SignatureCodeOther3;
            tax7.CountyName = txImportFileData.CountyName;
          }
          else
            taxRecordList.Add(dictionary6[txImportFileData.SignatureCodeOther3]);
        }
label_49:
        if (txImportFileData.Other4TaxIsCity == "C")
        {
          nullable = txImportFileData.Other4SalesTaxRate;
          Decimal num12 = 0M;
          if (!(nullable.GetValueOrDefault() > num12 & nullable.HasValue))
          {
            nullable = txImportFileData.Other4SalesTaxPreviousRate;
            Decimal num13 = 0M;
            if (!(nullable.GetValueOrDefault() > num13 & nullable.HasValue))
              goto label_57;
          }
          if (!string.IsNullOrEmpty(txImportFileData.SignatureCodeOther4))
            key = txImportFileData.SignatureCodeOther4;
          if (!dictionary7.ContainsKey(txImportFileData.SignatureCodeOther4))
          {
            TaxRecord tax8 = this.AppendTax($"{txImportFileData.StateCode}CO4{txImportFileData.SignatureCodeOther4}", "County Other 4 Tax " + txImportFileData.CountyName);
            tax8.Rate = txImportFileData.Other4SalesTaxRate;
            tax8.PreviousRate = txImportFileData.Other4SalesTaxPreviousRate;
            tax8.EffectiveDate = txImportFileData.Other4SalesTaxRateEffectiveDate;
            this.SetFlags(tax8, txImportFileData);
            dictionary7.Add(txImportFileData.SignatureCodeOther4, tax8);
            taxRecordList.Add(tax8);
            tax8.CountyCode = txImportFileData.SignatureCodeOther4;
            tax8.CountyName = txImportFileData.CountyName;
          }
          else
            taxRecordList.Add(dictionary7[txImportFileData.SignatureCodeOther4]);
        }
label_57:
        nullable = txImportFileData.CitySalesTaxRate;
        Decimal num14 = 0M;
        if (!(nullable.GetValueOrDefault() > num14 & nullable.HasValue))
        {
          nullable = txImportFileData.CitySalesTaxPreviousRate;
          Decimal num15 = 0M;
          if (!(nullable.GetValueOrDefault() > num15 & nullable.HasValue))
            goto label_64;
        }
        if (!string.IsNullOrEmpty(txImportFileData.SignatureCodeCity))
          key = txImportFileData.SignatureCodeCity;
        if (!dictionary8.ContainsKey(txImportFileData.SignatureCodeCity))
        {
          TaxRecord tax9 = this.AppendTax(txImportFileData.StateCode + txImportFileData.SignatureCodeCity, "City Tax " + txImportFileData.CityName);
          tax9.Rate = txImportFileData.CitySalesTaxRate;
          tax9.PreviousRate = txImportFileData.CitySalesTaxPreviousRate;
          tax9.EffectiveDate = txImportFileData.CitySalesTaxRateEffectiveDate;
          this.SetFlags(tax9, txImportFileData);
          dictionary8.Add(txImportFileData.SignatureCodeCity, tax9);
          taxRecordList.Add(tax9);
          tax9.CityCode = txImportFileData.SignatureCodeCity;
          tax9.CityName = txImportFileData.CityName;
        }
        else
          taxRecordList.Add(dictionary8[txImportFileData.SignatureCodeCity]);
label_64:
        if (txImportFileData.TransitTaxIsCity == "T")
        {
          nullable = txImportFileData.TransitSalesTaxRate;
          Decimal num16 = 0M;
          if (!(nullable.GetValueOrDefault() > num16 & nullable.HasValue))
          {
            nullable = txImportFileData.TransitSalesTaxPreviousRate;
            Decimal num17 = 0M;
            if (!(nullable.GetValueOrDefault() > num17 & nullable.HasValue))
              goto label_72;
          }
          if (!string.IsNullOrEmpty(txImportFileData.SignatureCodeTransit))
            key = txImportFileData.SignatureCodeTransit;
          if (!dictionary9.ContainsKey(txImportFileData.SignatureCodeTransit))
          {
            TaxRecord tax10 = this.AppendTax($"{txImportFileData.StateCode}TT{txImportFileData.SignatureCodeCounty}", "City Transit Tax " + txImportFileData.CityName);
            tax10.Rate = txImportFileData.TransitSalesTaxRate;
            tax10.PreviousRate = txImportFileData.TransitSalesTaxPreviousRate;
            tax10.EffectiveDate = txImportFileData.TransitSalesTaxRateEffectiveDate;
            this.SetFlags(tax10, txImportFileData);
            dictionary9.Add(txImportFileData.SignatureCodeTransit, tax10);
            taxRecordList.Add(tax10);
            tax10.CityCode = txImportFileData.SignatureCodeTransit;
            tax10.CityName = txImportFileData.CityName;
          }
          else
            taxRecordList.Add(dictionary9[txImportFileData.SignatureCodeTransit]);
        }
label_72:
        if (txImportFileData.Other1TaxIsCity == "T")
        {
          nullable = txImportFileData.Other1SalesTaxRate;
          Decimal num18 = 0M;
          if (!(nullable.GetValueOrDefault() > num18 & nullable.HasValue))
          {
            nullable = txImportFileData.Other1SalesTaxPreviousRate;
            Decimal num19 = 0M;
            if (!(nullable.GetValueOrDefault() > num19 & nullable.HasValue))
              goto label_80;
          }
          if (!string.IsNullOrEmpty(txImportFileData.SignatureCodeOther1))
            key = txImportFileData.SignatureCodeOther1;
          if (!dictionary10.ContainsKey(txImportFileData.SignatureCodeOther1))
          {
            TaxRecord tax11 = this.AppendTax($"{txImportFileData.StateCode}TO1{txImportFileData.SignatureCodeOther1}", "City Other 1 Tax " + txImportFileData.CityName);
            tax11.Rate = txImportFileData.Other1SalesTaxRate;
            tax11.PreviousRate = txImportFileData.Other1SalesTaxPreviousRate;
            tax11.EffectiveDate = txImportFileData.Other1SalesTaxRateEffectiveDate;
            this.SetFlags(tax11, txImportFileData);
            dictionary10.Add(txImportFileData.SignatureCodeOther1, tax11);
            taxRecordList.Add(tax11);
            tax11.CityCode = txImportFileData.SignatureCodeOther1;
            tax11.CityName = txImportFileData.CityName;
          }
          else
            taxRecordList.Add(dictionary10[txImportFileData.SignatureCodeOther1]);
        }
label_80:
        if (txImportFileData.Other2TaxIsCity == "T")
        {
          nullable = txImportFileData.Other2SalesTaxRate;
          Decimal num20 = 0M;
          if (!(nullable.GetValueOrDefault() > num20 & nullable.HasValue))
          {
            nullable = txImportFileData.Other2SalesTaxPreviousRate;
            Decimal num21 = 0M;
            if (!(nullable.GetValueOrDefault() > num21 & nullable.HasValue))
              goto label_88;
          }
          if (!string.IsNullOrEmpty(txImportFileData.SignatureCodeOther2))
            key = txImportFileData.SignatureCodeOther2;
          if (!dictionary11.ContainsKey(txImportFileData.SignatureCodeOther2))
          {
            TaxRecord tax12 = this.AppendTax($"{txImportFileData.StateCode}TO2{txImportFileData.SignatureCodeOther2}", "City Other 2 Tax " + txImportFileData.CityName);
            tax12.Rate = txImportFileData.Other2SalesTaxRate;
            tax12.PreviousRate = txImportFileData.Other2SalesTaxPreviousRate;
            tax12.EffectiveDate = txImportFileData.Other2SalesTaxRateEffectiveDate;
            this.SetFlags(tax12, txImportFileData);
            dictionary11.Add(txImportFileData.SignatureCodeOther2, tax12);
            taxRecordList.Add(tax12);
            tax12.CityCode = txImportFileData.SignatureCodeOther2;
            tax12.CityName = txImportFileData.CityName;
          }
          else
            taxRecordList.Add(dictionary11[txImportFileData.SignatureCodeOther2]);
        }
label_88:
        if (txImportFileData.Other3TaxIsCity == "T")
        {
          nullable = txImportFileData.Other3SalesTaxRate;
          Decimal num22 = 0M;
          if (!(nullable.GetValueOrDefault() > num22 & nullable.HasValue))
          {
            nullable = txImportFileData.Other3SalesTaxPreviousRate;
            Decimal num23 = 0M;
            if (!(nullable.GetValueOrDefault() > num23 & nullable.HasValue))
              goto label_96;
          }
          if (!string.IsNullOrEmpty(txImportFileData.SignatureCodeOther3))
            key = txImportFileData.SignatureCodeOther3;
          if (!dictionary12.ContainsKey(txImportFileData.SignatureCodeOther3))
          {
            TaxRecord tax13 = this.AppendTax($"{txImportFileData.StateCode}TO3{txImportFileData.SignatureCodeOther3}", "City Other 3 Tax " + txImportFileData.CityName);
            tax13.Rate = txImportFileData.Other3SalesTaxRate;
            tax13.PreviousRate = txImportFileData.Other3SalesTaxPreviousRate;
            tax13.EffectiveDate = txImportFileData.Other3SalesTaxRateEffectiveDate;
            this.SetFlags(tax13, txImportFileData);
            dictionary12.Add(txImportFileData.SignatureCodeOther3, tax13);
            taxRecordList.Add(tax13);
            tax13.CityCode = txImportFileData.CityTaxCodeAssignedByState ?? txImportFileData.OtherTaxCode1AssignedByState;
            tax13.CityName = txImportFileData.CityName;
          }
          else
            taxRecordList.Add(dictionary12[txImportFileData.SignatureCodeOther3]);
        }
label_96:
        if (txImportFileData.Other4TaxIsCity == "T")
        {
          nullable = txImportFileData.Other4SalesTaxRate;
          Decimal num24 = 0M;
          if (!(nullable.GetValueOrDefault() > num24 & nullable.HasValue))
          {
            nullable = txImportFileData.Other4SalesTaxPreviousRate;
            Decimal num25 = 0M;
            if (!(nullable.GetValueOrDefault() > num25 & nullable.HasValue))
              goto label_104;
          }
          if (!string.IsNullOrEmpty(txImportFileData.SignatureCodeOther4))
            key = txImportFileData.SignatureCodeOther4;
          if (!dictionary13.ContainsKey(txImportFileData.SignatureCodeOther4))
          {
            TaxRecord tax14 = this.AppendTax($"{txImportFileData.StateCode}TO4{txImportFileData.SignatureCodeOther4}", "City Other 4 Tax " + txImportFileData.CityName);
            tax14.Rate = txImportFileData.Other4SalesTaxRate;
            tax14.PreviousRate = txImportFileData.Other4SalesTaxPreviousRate;
            tax14.EffectiveDate = txImportFileData.Other4SalesTaxRateEffectiveDate;
            this.SetFlags(tax14, txImportFileData);
            dictionary13.Add(txImportFileData.SignatureCodeOther4, tax14);
            taxRecordList.Add(tax14);
            tax14.CityCode = txImportFileData.SignatureCodeOther4;
            tax14.CityName = txImportFileData.CityName;
          }
          else
            taxRecordList.Add(dictionary13[txImportFileData.SignatureCodeOther4]);
        }
label_104:
        ZoneRecord zone2;
        if (!dictionary1.ContainsKey(key))
        {
          string str1 = (string) null;
          string str2 = (string) null;
          string str3 = (string) null;
          string str4 = (string) null;
          foreach (TaxRecord taxRecord in taxRecordList)
          {
            if (str1 == null && !string.IsNullOrEmpty(taxRecord.CountyCode))
            {
              str1 = this.State + taxRecord.CountyCode;
              str4 = taxRecord.CountyName;
            }
            else if (str1 != null && !string.IsNullOrEmpty(taxRecord.CountyCode))
            {
              int num26 = str1 != this.State + taxRecord.CountyCode ? 1 : 0;
            }
            if (str2 == null && !string.IsNullOrEmpty(taxRecord.CityCode))
            {
              str2 = this.State + taxRecord.CityCode;
              str3 = taxRecord.CityName;
            }
            else if (str2 != null && !string.IsNullOrEmpty(taxRecord.CityCode))
            {
              int num27 = str2 != this.State + taxRecord.CityCode ? 1 : 0;
            }
          }
          string str5 = str2;
          string str6 = str4;
          if (str5 == null)
            str5 = str1;
          if (!string.IsNullOrEmpty(str3))
            str6 = $"{str3} - {str4}";
          if (str5 == null)
            str5 = key;
          if (str6 == null)
            str6 = this.State;
          zone2 = new ZoneRecord();
          zone2.ZoneID = str5;
          zone2.Description = str6;
          zone2.CombinedRate = txImportFileData.CombinedSalesTaxRate;
          this.zones.Add(zone2);
          dictionary1.Add(key, zone2);
          foreach (TaxRecord tax15 in taxRecordList)
            this.AppendZoneDetail(zone2, tax15);
        }
        else
          zone2 = dictionary1[key];
        this.AppendZip(zone2, txImportFileData);
      }
    }
  }
}

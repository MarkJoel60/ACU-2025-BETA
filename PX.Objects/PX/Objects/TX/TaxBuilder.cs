// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxBuilder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PX.Objects.TX;

public abstract class TaxBuilder
{
  protected List<TXImportFileData> data;
  protected List<TXImportZipFileData> zipData;
  protected List<TaxRecord> taxes;
  protected List<ZoneRecord> zones;
  protected List<ZoneDetailRecord> zoneDetails;
  protected List<ZoneZipRecord> zoneZips;
  protected Dictionary<ZoneRecord, IList<ZoneZipPlusRecord>> zoneZipPlus;

  public TaxBuilder(List<TXImportFileData> data, List<TXImportZipFileData> zipData)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (zipData == null)
      throw new ArgumentNullException(nameof (zipData));
    this.data = data;
    this.zipData = zipData;
  }

  public virtual TaxBuilder.Result Execute()
  {
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    this.ExecuteBuilder();
    stopwatch.Stop();
    return new TaxBuilder.Result((IList<TaxRecord>) this.taxes, (IList<ZoneRecord>) this.zones, (IList<ZoneDetailRecord>) this.zoneDetails, (IList<ZoneZipRecord>) this.zoneZips, (IDictionary<ZoneRecord, IList<ZoneZipPlusRecord>>) this.zoneZipPlus);
  }

  protected abstract void ExecuteBuilder();

  public static bool HasTax(TXImportFileData record)
  {
    Decimal? combinedSalesTaxRate = record.CombinedSalesTaxRate;
    Decimal num = 0M;
    return combinedSalesTaxRate.GetValueOrDefault() > num & combinedSalesTaxRate.HasValue;
  }

  public static bool IsOnlyState(TXImportFileData record)
  {
    Decimal? stateSalesTaxRate = record.StateSalesTaxRate;
    Decimal? combinedSalesTaxRate = record.CombinedSalesTaxRate;
    return stateSalesTaxRate.GetValueOrDefault() == combinedSalesTaxRate.GetValueOrDefault() & stateSalesTaxRate.HasValue == combinedSalesTaxRate.HasValue;
  }

  public static bool IsOnlyStateAndCounty(TXImportFileData record)
  {
    Decimal? stateSalesTaxRate = record.StateSalesTaxRate;
    Decimal? countySalesTaxRate = record.CountySalesTaxRate;
    Decimal? nullable = stateSalesTaxRate.HasValue & countySalesTaxRate.HasValue ? new Decimal?(stateSalesTaxRate.GetValueOrDefault() + countySalesTaxRate.GetValueOrDefault()) : new Decimal?();
    Decimal? combinedSalesTaxRate = record.CombinedSalesTaxRate;
    return nullable.GetValueOrDefault() == combinedSalesTaxRate.GetValueOrDefault() & nullable.HasValue == combinedSalesTaxRate.HasValue;
  }

  public static bool ContainsCityTax(TXImportFileData record)
  {
    Decimal num = TaxBuilder.TotalCounty(record);
    Decimal? stateSalesTaxRate = record.StateSalesTaxRate;
    Decimal? nullable = stateSalesTaxRate.HasValue ? new Decimal?(num + stateSalesTaxRate.GetValueOrDefault()) : new Decimal?();
    Decimal? combinedSalesTaxRate = record.CombinedSalesTaxRate;
    return !(nullable.GetValueOrDefault() == combinedSalesTaxRate.GetValueOrDefault() & nullable.HasValue == combinedSalesTaxRate.HasValue);
  }

  public static bool ContainsDistrictTax(TXImportFileData record)
  {
    return TaxBuilder.TotalDistrict(record) > 0M;
  }

  public static Decimal TotalCounty(TXImportFileData record)
  {
    return record.CountySalesTaxRate.Value + TaxBuilder.TotalDistrict(record);
  }

  public static Decimal TotalDistrict(TXImportFileData record)
  {
    Decimal num = 0M;
    if (record.TransitTaxIsCity == "C")
      num += record.TransitSalesTaxRate.Value;
    if (record.Other1TaxIsCity == "C")
      num += record.Other1SalesTaxRate.Value;
    if (record.Other2TaxIsCity == "C")
      num += record.Other2SalesTaxRate.Value;
    if (record.Other3TaxIsCity == "C")
      num += record.Other3SalesTaxRate.Value;
    if (record.Other4TaxIsCity == "C")
      num += record.Other4SalesTaxRate.Value;
    return num;
  }

  public static Decimal TotalCity(TXImportFileData record)
  {
    Decimal num = record.CitySalesTaxRate.Value;
    if (record.TransitTaxIsCity == "T")
      num += record.TransitSalesTaxRate.Value;
    if (record.Other1TaxIsCity == "T")
      num += record.Other1SalesTaxRate.Value;
    if (record.Other2TaxIsCity == "T")
      num += record.Other2SalesTaxRate.Value;
    if (record.Other3TaxIsCity == "T")
      num += record.Other3SalesTaxRate.Value;
    if (record.Other4TaxIsCity == "T")
      num += record.Other4SalesTaxRate.Value;
    return num;
  }

  protected virtual ZoneDetailRecord AppendZoneDetail(ZoneRecord zone, TaxRecord tax)
  {
    ZoneDetailRecord zoneDetailRecord = new ZoneDetailRecord();
    zoneDetailRecord.ZoneID = zone.ZoneID;
    zoneDetailRecord.TaxID = tax.TaxID;
    this.zoneDetails.Add(zoneDetailRecord);
    return zoneDetailRecord;
  }

  protected virtual ZoneRecord AppendZone(string zoneID, string description)
  {
    ZoneRecord zoneRecord = new ZoneRecord();
    zoneRecord.ZoneID = zoneID;
    zoneRecord.Description = description;
    this.zones.Add(zoneRecord);
    return zoneRecord;
  }

  protected virtual TaxRecord AppendTax(string taxID, string description)
  {
    TaxRecord taxRecord = new TaxRecord();
    taxRecord.TaxID = taxID;
    taxRecord.Description = description;
    this.taxes.Add(taxRecord);
    return taxRecord;
  }

  protected virtual TaxRecord AppendTax(
    string taxID,
    string description,
    Decimal? rate,
    DateTime? effectiveDate,
    Decimal? previousRate)
  {
    TaxRecord taxRecord = new TaxRecord();
    taxRecord.TaxID = taxID;
    taxRecord.Description = description;
    taxRecord.Rate = rate;
    taxRecord.EffectiveDate = effectiveDate;
    taxRecord.PreviousRate = previousRate;
    this.taxes.Add(taxRecord);
    return taxRecord;
  }

  protected virtual void SetFlags(TaxRecord tax, TXImportFileData record)
  {
    TaxRecord taxRecord = tax;
    Decimal? combinedSalesTaxRate = record.CombinedSalesTaxRate;
    Decimal num = 0M;
    bool? nullable = new bool?(combinedSalesTaxRate.GetValueOrDefault() > num & combinedSalesTaxRate.HasValue);
    taxRecord.IsTaxable = nullable;
    tax.IsFreight = new bool?(record.TaxFreight == "Y");
    tax.IsService = new bool?(record.TaxServices == "Y");
    tax.IsLabor = new bool?(record.TaxServices == "Y" || record.TaxServices == "S");
  }

  protected virtual void AppendZip(ZoneRecord zone, TXImportFileData t)
  {
    IEnumerable<TXImportZipFileData> importZipFileDatas = this.zipData.Where<TXImportZipFileData>((Func<TXImportZipFileData, bool>) (z => z.ZipCode == t.ZipCode && z.CountyName == t.CountyName));
    bool flag = false;
    foreach (TXImportZipFileData importZipFileData in importZipFileDatas)
    {
      ZoneZipPlusRecord zoneZipPlusRecord = new ZoneZipPlusRecord();
      zoneZipPlusRecord.ZoneID = zone.ZoneID;
      zoneZipPlusRecord.ZipCode = t.ZipCode;
      zoneZipPlusRecord.ZipMin = importZipFileData.Plus4PortionOfZipCode;
      zoneZipPlusRecord.ZipMax = importZipFileData.Plus4PortionOfZipCode2;
      if (this.zoneZipPlus.ContainsKey(zone))
        this.zoneZipPlus[zone].Add(zoneZipPlusRecord);
      else
        this.zoneZipPlus.Add(zone, (IList<ZoneZipPlusRecord>) new List<ZoneZipPlusRecord>()
        {
          zoneZipPlusRecord
        });
      flag = true;
    }
    if (flag)
      return;
    ZoneZipPlusRecord zoneZipPlusRecord1 = new ZoneZipPlusRecord();
    zoneZipPlusRecord1.ZoneID = zone.ZoneID;
    zoneZipPlusRecord1.ZipCode = t.ZipCode;
    zoneZipPlusRecord1.ZipMin = new int?(1);
    zoneZipPlusRecord1.ZipMax = new int?(9999);
    if (this.zoneZipPlus.ContainsKey(zone))
      this.zoneZipPlus[zone].Add(zoneZipPlusRecord1);
    else
      this.zoneZipPlus.Add(zone, (IList<ZoneZipPlusRecord>) new List<ZoneZipPlusRecord>()
      {
        zoneZipPlusRecord1
      });
  }

  public class Result
  {
    public IList<TaxRecord> Taxes { get; private set; }

    public IList<ZoneRecord> Zones { get; private set; }

    public IList<ZoneDetailRecord> ZoneDetails { get; private set; }

    public IList<ZoneZipRecord> ZoneZips { get; private set; }

    public IDictionary<ZoneRecord, IList<ZoneZipPlusRecord>> ZoneZipPlus { get; private set; }

    public Result(
      IList<TaxRecord> taxes,
      IList<ZoneRecord> zones,
      IList<ZoneDetailRecord> zoneDetails,
      IList<ZoneZipRecord> zoneZips,
      IDictionary<ZoneRecord, IList<ZoneZipPlusRecord>> zoneZipPlus)
    {
      this.Taxes = taxes;
      this.Zones = zones;
      this.ZoneDetails = zoneDetails;
      this.ZoneZips = zoneZips;
      this.ZoneZipPlus = zoneZipPlus;
    }
  }
}

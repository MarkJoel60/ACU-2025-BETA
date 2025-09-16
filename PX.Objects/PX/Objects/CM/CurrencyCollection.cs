// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurrencyCollection
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.DbServices.QueryObjectModel;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CM;

public static class CurrencyCollection
{
  /// <summary>
  /// Legacy method. Returns base currency from tenant, not organization. To get base currency for current organization/batch, use GetCurrency(AccessInfo.BaseCuryID)
  /// </summary>
  /// <returns>Base Currency of current Tenant (not of selected organization/Branch)</returns>
  public static Currency GetBaseCurrency()
  {
    CurrencyCollection.Definition slot = CurrencyCollection.slot;
    Currency currency;
    return slot.BaseCuryID != null && slot.Currency.TryGetValue(slot.BaseCuryID, out currency) ? currency : (Currency) null;
  }

  public static HashSet<string> GetBaseCurrencies() => CurrencyCollection.slot.BaseCurrencies;

  public static bool IsBaseCurrency(string currency)
  {
    return CurrencyCollection.slot.BaseCurrencies.Contains(currency);
  }

  public static Currency GetCurrency(string curyID)
  {
    Currency currency;
    return curyID != null && CurrencyCollection.slot.Currency.TryGetValue(curyID, out currency) ? currency : (Currency) null;
  }

  public static Currency GetCurrency(PXCache sender, Type sourceCuryID, object row)
  {
    if (sourceCuryID.DeclaringType == (Type) null)
      return (Currency) null;
    int num = sourceCuryID.DeclaringType.IsAssignableFrom(sender.GetItemType()) ? 1 : 0;
    PXCache pxCache = num != 0 ? sender : sender.Graph.Caches[sourceCuryID.DeclaringType];
    object obj = num != 0 ? row : pxCache.Current;
    return CurrencyCollection.GetCurrency(pxCache.GetValue(obj, sourceCuryID.Name) as string);
  }

  public static long? MatchBaseCuryInfoId(CurrencyInfo info)
  {
    Currency currency = CurrencyCollection.GetCurrency(info.BaseCuryID);
    long? nullable;
    if (currency != null)
    {
      nullable = currency.CuryInfoID;
      if (nullable.HasValue && info.CuryID == info.BaseCuryID && info.CuryID == currency.CuryID)
        return !info.BaseCalc.GetValueOrDefault() ? currency.CuryInfoBaseID : currency.CuryInfoID;
    }
    nullable = new long?();
    return nullable;
  }

  public static bool IsBaseCuryInfo(CurrencyInfo info, string curyID = null)
  {
    if (curyID == null)
      curyID = info.CuryID;
    Currency currency = CurrencyCollection.GetCurrency(info?.BaseCuryID) ?? CurrencyCollection.GetBaseCurrency();
    return currency != null && currency.CuryInfoID.HasValue && curyID == info.BaseCuryID && curyID == currency.CuryID;
  }

  public static long? MatchBaseCuryInfoId(PX.Objects.CM.Extensions.CurrencyInfo info)
  {
    Currency currency = CurrencyCollection.GetCurrency(info.BaseCuryID);
    long? nullable;
    if (currency != null)
    {
      nullable = currency.CuryInfoID;
      if (nullable.HasValue && info.CuryID == info.BaseCuryID && info.CuryID == currency.CuryID)
        return !info.BaseCalc.GetValueOrDefault() ? currency.CuryInfoBaseID : currency.CuryInfoID;
    }
    nullable = new long?();
    return nullable;
  }

  public static Type CombiteSearchPrecision(Type curyID)
  {
    return BqlCommand.Compose(new Type[7]
    {
      typeof (Search<,>),
      typeof (Currency.decimalPlaces),
      typeof (Where<,>),
      typeof (Currency.curyID),
      typeof (Equal<>),
      typeof (Current<>),
      curyID
    });
  }

  public static bool IsBaseCuryInfo(PX.Objects.CM.Extensions.CurrencyInfo info, string curyID = null)
  {
    if (curyID == null)
      curyID = info.CuryID;
    Currency currency = CurrencyCollection.GetCurrency(info?.BaseCuryID) ?? CurrencyCollection.GetBaseCurrency();
    return currency != null && currency.CuryInfoID.HasValue && curyID == info.BaseCuryID && curyID == currency.CuryID;
  }

  private static CurrencyCollection.Definition slot
  {
    get
    {
      CurrencyCollection.Definition slot1 = PXContext.GetSlot<CurrencyCollection.Definition>();
      if (slot1 != null)
        return slot1;
      CurrencyCollection.Definition slot2 = PXDatabase.GetSlot<CurrencyCollection.Definition>(nameof (CurrencyCollection), new Type[4]
      {
        typeof (Currency),
        typeof (CurrencyList),
        typeof (Company),
        typeof (PX.Objects.GL.Branch)
      });
      PXContext.SetSlot<CurrencyCollection.Definition>(slot2);
      return slot2;
    }
  }

  private class Definition : IPrefetchable, IPXCompanyDependent
  {
    public Dictionary<string, Currency> Currency = new Dictionary<string, Currency>();
    public HashSet<string> BaseCurrencies = new HashSet<string>();
    public string BaseCuryID;

    public void Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Company>(new PXDataField[1]
      {
        (PXDataField) new PXAliasedDataField<Company.baseCuryID>()
      }))
        this.BaseCuryID = pxDataRecord?.GetString(0)?.Trim();
      this.Currency = PXDatabase.SelectMulti<Currency>(Yaql.join<CurrencyList>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<CurrencyList.curyID>((string) null), Yaql.column<Currency.curyID>((string) null)), (YaqlJoinType) 0), new PXDataField[16 /*0x10*/]
      {
        (PXDataField) new PXAliasedDataField<Currency.curyID>(),
        (PXDataField) new PXAliasedDataField<Currency.curyInfoID>(),
        (PXDataField) new PXAliasedDataField<Currency.curyInfoBaseID>(),
        (PXDataField) new PXAliasedDataField<CurrencyList.curySymbol>(),
        (PXDataField) new PXAliasedDataField<CurrencyList.decimalPlaces>(),
        (PXDataField) new PXAliasedDataField<Currency.roundingLimit>(),
        (PXDataField) new PXAliasedDataField<Currency.aPInvoicePrecision>(),
        (PXDataField) new PXAliasedDataField<Currency.aPInvoiceRounding>(),
        (PXDataField) new PXAliasedDataField<Currency.useAPPreferencesSettings>(),
        (PXDataField) new PXAliasedDataField<Currency.aRInvoicePrecision>(),
        (PXDataField) new PXAliasedDataField<Currency.aRInvoiceRounding>(),
        (PXDataField) new PXAliasedDataField<Currency.useARPreferencesSettings>(),
        (PXDataField) new PXAliasedDataField<Currency.translationGainAcctID>(),
        (PXDataField) new PXAliasedDataField<Currency.translationGainSubID>(),
        (PXDataField) new PXAliasedDataField<Currency.translationLossAcctID>(),
        (PXDataField) new PXAliasedDataField<Currency.translationLossSubID>()
      }).Select<PXDataRecord, Currency>((Func<PXDataRecord, Currency>) (row => new Currency()
      {
        CuryID = row.GetString(0).Trim(),
        CuryInfoID = row.GetInt64(1),
        CuryInfoBaseID = row.GetInt64(2),
        CurySymbol = row.GetString(3),
        DecimalPlaces = row.GetInt16(4),
        RoundingLimit = row.GetDecimal(5),
        APInvoicePrecision = row.GetDecimal(6),
        APInvoiceRounding = row.GetString(7),
        UseAPPreferencesSettings = row.GetBoolean(8),
        ARInvoicePrecision = row.GetDecimal(9),
        ARInvoiceRounding = row.GetString(10),
        UseARPreferencesSettings = row.GetBoolean(11),
        TranslationGainAcctID = row.GetInt32(12),
        TranslationGainSubID = row.GetInt32(13),
        TranslationLossAcctID = row.GetInt32(14),
        TranslationLossSubID = row.GetInt32(15)
      })).ToDictionary<Currency, string>((Func<Currency, string>) (c => c.CuryID));
      this.BaseCurrencies = PXDatabase.SelectMulti<PX.Objects.GL.DAC.Organization>(new PXDataField[1]
      {
        (PXDataField) new PXAliasedDataField<PX.Objects.GL.DAC.Organization.baseCuryID>()
      }).Select<PXDataRecord, string>((Func<PXDataRecord, string>) (row => row.GetString(0).Trim())).Distinct<string>().ToHashSet<string>();
    }
  }
}

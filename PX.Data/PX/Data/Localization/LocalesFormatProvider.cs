// Decompiled with JetBrains decompiler
// Type: PX.Data.Localization.LocalesFormatProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Update;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web;

#nullable disable
namespace PX.Data.Localization;

public static class LocalesFormatProvider
{
  private static readonly string SLOT_KEY = "locale_format_key";
  private static readonly System.Type[] DEPENDED_TYPES = new System.Type[3]
  {
    typeof (Locale),
    typeof (LocaleFormat),
    typeof (UserPreferences)
  };

  public static LocaleFormatInfo GetLocaleFormatInfo()
  {
    return LocalesFormatProvider.GetLocaleFormatInfo(new LocaleFormatRequest(PXLocalesProvider.GetCurrentLocale(), LocalesFormatProvider.GetCurrentUser()));
  }

  public static LocaleFormatInfo GetLocaleFormatInfo(string locale, Guid user)
  {
    return LocalesFormatProvider.GetLocaleFormatInfo(new LocaleFormatRequest(PXLocalesProvider.GetCurrentLocale(), LocalesFormatProvider.GetCurrentUser()));
  }

  public static LocaleFormatInfo GetLocaleFormatInfo(LocaleFormatRequest request)
  {
    try
    {
      using (PXContext.PXIdentity.Authenticated ? (PXLoginScope) null : new PXLoginScope(PXInstanceHelper.ScopeUser, Array.Empty<string>()))
      {
        Dictionary<string, LocaleFormatInfo> slot = PXDatabase.GetSlot<Dictionary<string, LocaleFormatInfo>>(LocalesFormatProvider.SLOT_KEY, LocalesFormatProvider.DEPENDED_TYPES);
        if (slot != null)
        {
          lock (((ICollection) slot).SyncRoot)
          {
            LocaleFormatInfo localeFormatInfo = (LocaleFormatInfo) null;
            if (slot.TryGetValue(request.Key, out localeFormatInfo))
              return localeFormatInfo;
            try
            {
              localeFormatInfo = LocalesFormatProvider.FillLocaleFormat(request);
            }
            catch
            {
            }
            return slot[request.Key] = localeFormatInfo;
          }
        }
      }
    }
    catch
    {
    }
    return (LocaleFormatInfo) null;
  }

  public static void ApplyLocaleFormat(CultureInfo info)
  {
    LocalesFormatProvider.ApplyLocaleFormat(info, new Guid?());
  }

  internal static void ApplyLocaleFormat(CultureInfo info, Guid? userId)
  {
    LocaleFormatInfo localeFormatInfo = LocalesFormatProvider.GetLocaleFormatInfo(new LocaleFormatRequest(info.Name, userId ?? LocalesFormatProvider.GetCurrentUser()));
    if (localeFormatInfo == null || info.IsReadOnly)
      return;
    Func<string, bool> func = (Func<string, bool>) (format =>
    {
      if (string.IsNullOrEmpty(format))
        return false;
      try
      {
        System.DateTime.Now.ToString(format);
        return true;
      }
      catch
      {
      }
      return false;
    });
    if (func(localeFormatInfo.LongDatePattern))
      info.DateTimeFormat.LongDatePattern = localeFormatInfo.LongDatePattern;
    if (func(localeFormatInfo.LongTimePattern))
      info.DateTimeFormat.LongTimePattern = localeFormatInfo.LongTimePattern;
    if (func(localeFormatInfo.ShortDatePattern))
      info.DateTimeFormat.ShortDatePattern = localeFormatInfo.ShortDatePattern;
    if (func(localeFormatInfo.ShortTimePattern))
      info.DateTimeFormat.ShortTimePattern = localeFormatInfo.ShortTimePattern;
    if (func(localeFormatInfo.FullDateTimePattern))
      info.DateTimeFormat.FullDateTimePattern = localeFormatInfo.FullDateTimePattern;
    if (!string.IsNullOrEmpty(localeFormatInfo.AMDesignator))
      info.DateTimeFormat.AMDesignator = localeFormatInfo.AMDesignator;
    if (!string.IsNullOrEmpty(localeFormatInfo.PMDesignator))
      info.DateTimeFormat.PMDesignator = localeFormatInfo.PMDesignator;
    if (!string.IsNullOrEmpty(localeFormatInfo.NumberDecimalSeparator))
    {
      info.NumberFormat.CurrencyDecimalSeparator = localeFormatInfo.NumberDecimalSeparator;
      info.NumberFormat.NumberDecimalSeparator = localeFormatInfo.NumberDecimalSeparator;
    }
    if (string.IsNullOrEmpty(localeFormatInfo.NumberGroupSeparator))
      return;
    info.NumberFormat.CurrencyGroupSeparator = localeFormatInfo.NumberGroupSeparator;
    info.NumberFormat.NumberGroupSeparator = localeFormatInfo.NumberGroupSeparator;
  }

  internal static string GetSlotCacheKey()
  {
    return PXDatabase.GetSlot<Dictionary<string, LocaleFormatInfo>>(LocalesFormatProvider.SLOT_KEY, LocalesFormatProvider.DEPENDED_TYPES)?.GetHashCode().ToString();
  }

  private static LocaleFormatInfo FillLocaleFormat(LocaleFormatRequest request)
  {
    LocaleFormatInfo localeFormatInfo1 = (LocaleFormatInfo) null;
    LocaleFormatInfo localeFormatInfo2 = (LocaleFormatInfo) null;
    if (!PXDatabase.Provider.SchemaCache.TableExists(typeof (UserLocaleFormat).Name))
      return (LocaleFormatInfo) null;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle(typeof (UserLocaleFormat), new PXDataField(typeof (UserLocaleFormat.formatID).Name), (PXDataField) new PXDataFieldValue(typeof (UserLocaleFormat.localeName).Name, (object) request.LocaleID), (PXDataField) new PXDataFieldValue(typeof (UserLocaleFormat.userID).Name, (object) request.UserID)))
    {
      if (pxDataRecord != null)
      {
        if (!pxDataRecord.IsDBNull(0))
          localeFormatInfo2 = LocalesFormatProvider.FillLocaleFormat(pxDataRecord.GetInt32(0).Value);
      }
    }
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle(typeof (Locale), new PXDataField(typeof (Locale.formatID).Name), (PXDataField) new PXDataFieldValue(typeof (Locale.localeName).Name, (object) request.LocaleID)))
    {
      if (pxDataRecord != null)
      {
        if (!pxDataRecord.IsDBNull(0))
          localeFormatInfo1 = LocalesFormatProvider.FillLocaleFormat(pxDataRecord.GetInt32(0).Value);
      }
    }
    if (localeFormatInfo2 == null && localeFormatInfo1 == null)
      return (LocaleFormatInfo) null;
    if (localeFormatInfo2 != null && localeFormatInfo1 == null)
      return localeFormatInfo2;
    if (localeFormatInfo2 == null && localeFormatInfo1 != null)
      return localeFormatInfo1;
    return new LocaleFormatInfo()
    {
      NumberDecimalSeparator = localeFormatInfo2.NumberDecimalSeparator ?? localeFormatInfo1.NumberDecimalSeparator,
      NumberGroupSeparator = localeFormatInfo2.NumberGroupSeparator ?? localeFormatInfo1.NumberGroupSeparator,
      FullDateTimePattern = localeFormatInfo2.FullDateTimePattern ?? localeFormatInfo1.FullDateTimePattern,
      ShortTimePattern = localeFormatInfo2.ShortTimePattern ?? localeFormatInfo1.ShortTimePattern,
      LongTimePattern = localeFormatInfo2.LongTimePattern ?? localeFormatInfo1.LongTimePattern,
      ShortDatePattern = localeFormatInfo2.ShortDatePattern ?? localeFormatInfo1.ShortDatePattern,
      LongDatePattern = localeFormatInfo2.LongDatePattern ?? localeFormatInfo1.LongDatePattern,
      AMDesignator = localeFormatInfo2.AMDesignator ?? localeFormatInfo1.AMDesignator,
      PMDesignator = localeFormatInfo2.PMDesignator ?? localeFormatInfo1.PMDesignator
    };
  }

  private static LocaleFormatInfo FillLocaleFormat(int formatID)
  {
    LocaleFormatInfo localeFormatInfo = (LocaleFormatInfo) null;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle(typeof (LocaleFormat), new PXDataField(typeof (LocaleFormat.numberDecimalSeporator).Name), new PXDataField(typeof (LocaleFormat.numberGroupSeparator).Name), new PXDataField(typeof (LocaleFormat.dateTimePattern).Name), new PXDataField(typeof (LocaleFormat.timeShortPattern).Name), new PXDataField(typeof (LocaleFormat.timeLongPattern).Name), new PXDataField(typeof (LocaleFormat.dateShortPattern).Name), new PXDataField(typeof (LocaleFormat.dateLongPattern).Name), new PXDataField(typeof (LocaleFormat.aMDesignator).Name), new PXDataField(typeof (LocaleFormat.pMDesignator).Name), (PXDataField) new PXDataFieldValue(typeof (LocaleFormat.formatID).Name, (object) formatID)))
    {
      if (pxDataRecord != null)
      {
        localeFormatInfo = new LocaleFormatInfo();
        localeFormatInfo.NumberDecimalSeparator = PXNumberSeparatorListAttribute.Decode(pxDataRecord.GetString(0));
        localeFormatInfo.NumberGroupSeparator = PXNumberSeparatorListAttribute.Decode(pxDataRecord.GetString(1));
        localeFormatInfo.FullDateTimePattern = PXNumberSeparatorListAttribute.Decode(pxDataRecord.GetString(2));
        localeFormatInfo.ShortTimePattern = PXNumberSeparatorListAttribute.Decode(pxDataRecord.GetString(3));
        localeFormatInfo.LongTimePattern = PXNumberSeparatorListAttribute.Decode(pxDataRecord.GetString(4));
        localeFormatInfo.ShortDatePattern = PXNumberSeparatorListAttribute.Decode(pxDataRecord.GetString(5));
        localeFormatInfo.LongDatePattern = PXNumberSeparatorListAttribute.Decode(pxDataRecord.GetString(6));
        localeFormatInfo.AMDesignator = PXNumberSeparatorListAttribute.Decode(pxDataRecord.GetString(7));
        localeFormatInfo.PMDesignator = PXNumberSeparatorListAttribute.Decode(pxDataRecord.GetString(8));
      }
    }
    return localeFormatInfo;
  }

  private static Guid GetCurrentUser()
  {
    Guid currentUser = Guid.Empty;
    try
    {
      if (HttpContext.Current == null || HttpContext.Current.Request == null || !HttpContext.Current.Request.IsAuthenticated || !PXContext.PXIdentity.IsAuthenticatedAndNotEmpty())
        return currentUser;
      currentUser = PXAccess.GetTrueUserID();
    }
    catch
    {
    }
    return currentUser;
  }
}

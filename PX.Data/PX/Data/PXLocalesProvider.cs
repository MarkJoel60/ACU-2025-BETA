// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLocalesProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Localization;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;

#nullable disable
namespace PX.Data;

/// <summary>
/// Provides access to all locales available in the system.
/// </summary>
public static class PXLocalesProvider
{
  private static readonly Dictionary<string, PXLocale[]> localesCache = new Dictionary<string, PXLocale[]>();
  private static readonly object cultlock = new object();

  public static PXCollationComparer CollationComparer
  {
    get
    {
      PXCollationComparer slot = PXContext.GetSlot<PXCollationComparer>();
      if (slot != null)
        return slot;
      return PXContext.SetSlot<PXCollationComparer>(PXDatabase.GetSlot<PXCollationComparer>(nameof (CollationComparer), typeof (SystemCollation)));
    }
  }

  internal static PXCollationProvider CollationProvider
  {
    get
    {
      PXCollationProvider slot = PXContext.GetSlot<PXCollationProvider>();
      if (slot != null)
        return slot;
      return PXContext.SetSlot<PXCollationProvider>(PXDatabase.GetSlot<PXCollationProvider>(nameof (CollationProvider), typeof (SystemCollation)));
    }
  }

  static PXLocalesProvider()
  {
    PXDatabase.Subscribe(typeof (Locale), (PXDatabaseTableChanged) (() => PXLocalesProvider.Clear()));
  }

  private static PXLocale[] Prefetch()
  {
    try
    {
      List<PXLocale> pxLocaleList = new List<PXLocale>();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti(typeof (Locale), new PXDataField("LocaleName"), new PXDataField("TranslatedName"), new PXDataField("Number"), new PXDataField(typeof (Locale.showValidationWarnings).Name), (PXDataField) new PXDataFieldValue("IsActive", (object) true)))
      {
        if (pxDataRecord != null && !pxDataRecord.IsDBNull(0) && !pxDataRecord.IsDBNull(1))
          pxLocaleList.Add(new PXLocale(pxDataRecord.GetString(0), pxDataRecord.GetString(1), pxDataRecord.GetInt16(2).GetValueOrDefault(), pxDataRecord.GetBoolean(3)));
      }
      pxLocaleList.Sort((Comparison<PXLocale>) ((l1, l2) =>
      {
        if ((int) l1.Number > (int) l2.Number)
          return 1;
        return (int) l1.Number < (int) l2.Number ? -1 : 0;
      }));
      return pxLocaleList.ToArray();
    }
    catch
    {
    }
    return (PXLocale[]) null;
  }

  /// <exclude />
  public static void Clear()
  {
    lock (PXLocalesProvider.cultlock)
      PXLocalesProvider.localesCache.Clear();
  }

  /// <exclude />
  public static PXLocale[] GetLocales() => PXLocalesProvider.Prefetch();

  /// <exclude />
  public static PXLocale[] GetLocales(string userName)
  {
    lock (PXLocalesProvider.cultlock)
    {
      if (PXLocalesProvider.localesCache.ContainsKey(userName))
        return PXLocalesProvider.localesCache[userName];
      using (new PXLoginScope(userName, Array.Empty<string>()))
      {
        PXLocale[] locales = PXLocalesProvider.Prefetch();
        if (locales != null)
          PXLocalesProvider.localesCache[userName] = locales;
        return locales;
      }
    }
  }

  /// <exclude />
  public static string GetCurrentLocale()
  {
    string currentLocale = string.Empty;
    if (HttpContext.Current != null)
      currentLocale = PXContext.Session["LocaleName"] as string;
    if (string.IsNullOrEmpty(currentLocale))
      currentLocale = Thread.CurrentThread.CurrentCulture.Name;
    return currentLocale;
  }

  /// <exclude />
  public static bool ContainsCollation(this string str, string value)
  {
    return PXLocalesProvider.CollationComparer.Contains(str, value);
  }

  /// <exclude />
  public static bool StartsWithCollation(this string str, string value)
  {
    return PXLocalesProvider.CollationComparer.StartsWith(str, value);
  }

  /// <exclude />
  public static bool EndsWithCollation(this string str, string value)
  {
    return PXLocalesProvider.CollationComparer.EndsWith(str, value);
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.Localization.PXLocalesHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Localization;

internal static class PXLocalesHelper
{
  private const string DefaultLocale = "en-US";

  public static string GetLanguage(this PXLocale locale)
  {
    return PXLocalesHelper.GetLanguageFromLocaleName(locale.Name);
  }

  public static string GetDefaultLanguage()
  {
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Locale>(new PXDataField("LocaleName"), (PXDataField) new PXDataFieldValue("IsActive", (object) true), (PXDataField) new PXDataFieldValue("IsDefault", (object) true)))
      return PXLocalesHelper.GetLanguageFromLocaleName(pxDataRecord?.GetString(0) ?? "en-US");
  }

  public static string GetCurrentLanguage()
  {
    return PXLocalesHelper.GetLanguageFromLocaleName(PXLocalesProvider.GetCurrentLocale());
  }

  private static string GetLanguageFromLocaleName(string locale)
  {
    if (string.IsNullOrEmpty(locale) || !locale.Contains("-"))
      return locale;
    return ((IEnumerable<string>) locale.Split('-')).First<string>();
  }

  public static (string LocaleName, string Language)[] GetLocalesWithUniqueLanguage(
    string currentUserName)
  {
    PXLocale[] locales = PXLocalesProvider.GetLocales(currentUserName);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return locales == null ? ((string, string)[]) null : ((IEnumerable<PXLocale>) locales).GroupBy<PXLocale, string>(PXLocalesHelper.\u003C\u003EO.\u003C0\u003E__GetLanguage ?? (PXLocalesHelper.\u003C\u003EO.\u003C0\u003E__GetLanguage = new Func<PXLocale, string>(PXLocalesHelper.GetLanguage))).Select<IGrouping<string, PXLocale>, (string, string)>((Func<IGrouping<string, PXLocale>, (string, string)>) (g => (g.First<PXLocale>().Name, g.Key))).ToArray<(string, string)>();
  }
}

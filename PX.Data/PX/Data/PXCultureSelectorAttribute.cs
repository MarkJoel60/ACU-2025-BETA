// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCultureSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXCultureSelectorAttribute : PXCustomSelectorAttribute
{
  public PXCultureSelectorAttribute()
    : base(typeof (Locale.localeName), typeof (Locale.localeName), typeof (Locale.cultureReadableName), typeof (Locale.translatedName))
  {
    this.DescriptionField = typeof (Locale.cultureReadableName);
  }

  internal IEnumerable GetRecords() => (IEnumerable) PXCultureSelectorAttribute.GetLocales();

  public static IEnumerable<Locale> GetLocales()
  {
    CultureInfo[] cultureInfoArray = CultureInfo.GetCultures(CultureTypes.AllCultures);
    for (int index = 0; index < cultureInfoArray.Length; ++index)
    {
      CultureInfo culture = cultureInfoArray[index];
      if (!PXCultureSelectorAttribute.SkipCulture(culture))
        yield return new Locale()
        {
          LocaleName = culture.Name,
          TranslatedName = culture.NativeName.Replace(Convert.ToChar(173).ToString(), string.Empty),
          CultureReadableName = culture.EnglishName
        };
    }
    cultureInfoArray = (CultureInfo[]) null;
  }

  public static bool SkipCulture(CultureInfo culture)
  {
    return culture == null || culture.Name.IndexOf('-') == -1 || culture.IsNeutralCulture;
  }
}

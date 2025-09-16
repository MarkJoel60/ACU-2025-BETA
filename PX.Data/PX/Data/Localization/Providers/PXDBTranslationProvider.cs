// Decompiled with JetBrains decompiler
// Type: PX.Data.Localization.Providers.PXDBTranslationProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Translation;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Localization.Providers;

internal class PXDBTranslationProvider : IPXTranslationProvider
{
  public virtual PXCultureDictionary LoadCultureDictionary(
    string locale,
    bool includeObsolete,
    bool escapeStrings)
  {
    object[] parameters;
    PXSelectBase<LocalizationTranslation> translationSelect = this.GetTranslationSelect(locale, includeObsolete, out parameters);
    PXCultureDictionary cultureDictionary1 = new PXCultureDictionary();
    List<Tuple<string, string>> tupleList = new List<Tuple<string, string>>();
    object[] objArray = parameters;
    foreach (PXResult<LocalizationTranslation, LocalizationValue, LocalizationResource> pxResult in translationSelect.Select(objArray))
    {
      LocalizationTranslation localizationTranslation = (LocalizationTranslation) pxResult;
      LocalizationValue localizationValue = (LocalizationValue) pxResult;
      LocalizationResource localizationResource = (LocalizationResource) pxResult;
      string str1 = escapeStrings ? PXLocalizer.EscapeString(localizationTranslation.Value) : localizationTranslation.Value;
      bool? isNotLocalized;
      if (localizationTranslation.IdRes == null || string.Compare(localizationTranslation.IdRes, "D41D8CD98F00B204E9800998ECF8427E", StringComparison.OrdinalIgnoreCase) == 0)
      {
        PXCultureDictionary cultureDictionary2 = cultureDictionary1;
        string neutralValue = localizationValue.NeutralValue;
        string locale1 = localizationTranslation.Locale;
        string str2 = str1;
        isNotLocalized = localizationValue.IsNotLocalized;
        bool flag = true;
        int num = isNotLocalized.GetValueOrDefault() == flag & isNotLocalized.HasValue ? 1 : 0;
        PXCultureValue pxCultureValue = new PXCultureValue(locale1, str2, num != 0);
        cultureDictionary2.Append(neutralValue, pxCultureValue);
      }
      else if (localizationResource != null)
      {
        PXCultureDictionary cultureDictionary3 = cultureDictionary1;
        string neutralValue = localizationValue.NeutralValue;
        string resKey = localizationResource.ResKey;
        string locale2 = localizationTranslation.Locale;
        string str3 = str1;
        isNotLocalized = localizationResource.IsNotLocalized;
        bool flag = true;
        int num = isNotLocalized.GetValueOrDefault() == flag & isNotLocalized.HasValue ? 1 : 0;
        PXCultureEx ex = new PXCultureEx(resKey, locale2, str3, num != 0);
        cultureDictionary3.AppendException(neutralValue, ex);
      }
      if (localizationResource != null && localizationResource.ResType.HasValue && localizationResource.ResType.Value == 30)
        tupleList.Add(new Tuple<string, string>(localizationValue.NeutralValue, localizationResource.ResKey));
    }
    foreach (Tuple<string, string> tuple in tupleList)
    {
      string str4;
      string str5;
      tuple.Deconstruct<string, string>(out str4, out str5);
      string key = str4;
      string resKey = str5;
      cultureDictionary1.AppendClientDictionary(key, resKey, locale);
    }
    return cultureDictionary1;
  }

  private PXSelectBase<LocalizationTranslation> GetTranslationSelect(
    string locale,
    bool includeObsolete,
    out object[] parameters)
  {
    PXSelectBase<LocalizationTranslation> translationSelect = (PXSelectBase<LocalizationTranslation>) new PXSelectJoin<LocalizationTranslation, InnerJoin<LocalizationValue, On<LocalizationValue.id, Equal<LocalizationTranslation.idValue>>, LeftJoin<LocalizationResource, On<LocalizationResource.idValue, Equal<LocalizationTranslation.idValue>, And<LocalizationResource.id, Equal<LocalizationTranslation.idRes>>>>>>(new PXGraph()
    {
      FullTrust = true
    });
    List<object> objectList = new List<object>();
    if (!includeObsolete)
    {
      if (PXSiteMap.IsPortal)
        translationSelect.WhereAnd<Where<LocalizationValue.isObsoletePortal, Equal<False>>>();
      else
        translationSelect.WhereAnd<Where<LocalizationValue.isObsolete, Equal<False>>>();
    }
    if (!string.IsNullOrEmpty(locale))
    {
      translationSelect.WhereAnd<Where<LocalizationTranslation.locale, Equal<Required<LocalizationTranslation.locale>>>>();
      objectList.Add((object) locale);
    }
    parameters = objectList.ToArray();
    return translationSelect;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Translation.PXCultureDictionary
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

#nullable enable
namespace PX.Translation;

/// <exclude />
public class PXCultureDictionary : Dictionary<
#nullable disable
string, PXTranslatedRecord>
{
  private readonly Dictionary<string, Dictionary<string, string>> _clientDictionary = new Dictionary<string, Dictionary<string, string>>();
  private readonly Lazy<Dictionary<string, List<string>>> _clientValueMap = new Lazy<Dictionary<string, List<string>>>(PXCultureDictionary.\u003C\u003EO.\u003C0\u003E__BuildValueMap ?? (PXCultureDictionary.\u003C\u003EO.\u003C0\u003E__BuildValueMap = new Func<Dictionary<string, List<string>>>(PXCultureDictionary.BuildValueMap)));

  public PXCultureDictionary()
    : base((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
  {
  }

  public PXCultureDictionary(int capacity)
    : base(capacity, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
  {
  }

  internal IReadOnlyDictionary<string, Dictionary<string, string>> ClientDictionary
  {
    get => (IReadOnlyDictionary<string, Dictionary<string, string>>) this._clientDictionary;
  }

  internal IReadOnlyDictionary<string, string> GetClientResourcesForScreen(string screenId)
  {
    return (IReadOnlyDictionary<string, string>) ImmutableDictionary.ToImmutableDictionary<KeyValuePair<string, List<string>>, string, string>(this._clientValueMap.Value.Where<KeyValuePair<string, List<string>>>((Func<KeyValuePair<string, List<string>>, bool>) (res => res.Value.Any<string>((Func<string, bool>) (resKey => resKey.StartsWith(screenId, StringComparison.OrdinalIgnoreCase))))), (Func<KeyValuePair<string, List<string>>, string>) (it => it.Key), (Func<KeyValuePair<string, List<string>>, string>) (it => it.Value.First<string>((Func<string, bool>) (resKey => resKey.StartsWith(screenId, StringComparison.OrdinalIgnoreCase)))));
  }

  private static Dictionary<string, List<string>> BuildValueMap()
  {
    Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
    try
    {
      foreach (PXResult<LocalizationValue, LocalizationResource> pxResult in new PXSelectJoin<LocalizationValue, LeftJoin<LocalizationResource, On<LocalizationValue.id, Equal<LocalizationResource.idValue>>>, Where<LocalizationResource.resType, Equal<Required<LocalizationResource.resType>>>>(new PXGraph()
      {
        FullTrust = true
      }).Select((object) LocalizationResourceType.TSLiteral))
      {
        LocalizationResource localizationResource = (LocalizationResource) pxResult;
        LocalizationValue localizationValue = (LocalizationValue) pxResult;
        List<string> stringList;
        if (!dictionary.TryGetValue(localizationValue.NeutralValue, out stringList))
        {
          stringList = new List<string>();
          dictionary.Add(localizationValue.NeutralValue, stringList);
        }
        if (!stringList.Contains(localizationResource.ResKey))
          stringList.Add(localizationResource.ResKey);
      }
    }
    catch (Exception ex)
    {
      PXTrace.WithStack().Error<string>("Failed to load TS localization strings: {Reason}", ex.Message);
    }
    return dictionary;
  }

  /// <summary>
  /// Determines whether PXCultureDictionary contains translation for the given word and locale.
  /// </summary>
  /// <param name="key">Word to look a translation for.</param>
  /// <param name="lang">Locale to look a translation for.</param>
  /// <returns>True if translation for the given word and locale is found, otherwise false.</returns>
  public bool Contains(string key, string lang)
  {
    if (key == null || !this.ContainsKey(key))
      return false;
    foreach (PXCultureValue pxCultureValue in this[key].Translated)
    {
      if (string.Compare(pxCultureValue.CultureName, lang, true) == 0)
        return true;
    }
    return false;
  }

  /// <summary>
  /// Determines whether PXCultureDictionary contains exceptional translation for the given word, resource key and locale.
  /// </summary>
  /// <param name="key">Word to look a translation for.</param>
  /// <param name="resKey">Resource key.</param>
  /// <param name="lang">Locale to look a translation for.</param>
  /// <returns>True if exceptional translation for the given word, resource key and locale is found, otherwise false.</returns>
  public bool ContainsException(string key, string resKey, string lang)
  {
    if (key == null || !this.ContainsKey(key))
      return false;
    foreach (PXCultureEx exception in this[key].Exceptions)
    {
      if (string.Compare(exception.ResourceKey, resKey, true) == 0 && string.Compare(exception.Language, lang, true) == 0)
        return true;
    }
    return false;
  }

  /// <summary>
  /// Adds the PXCultureValue object to a list of translated values at specified key.
  /// </summary>
  /// <param name="key">The key of the element to add value to.</param>
  /// <param name="value">The value of the element to add.</param>
  public void Append(string key, PXCultureValue value)
  {
    if (key == null)
      return;
    if (!this.ContainsKey(key))
      this.Add(key, new PXTranslatedRecord());
    if (this.Contains(key, value.CultureName))
      return;
    this[key].Translated.Add(value);
    List<string> stringList;
    if (!this._clientValueMap.Value.TryGetValue(key, out stringList))
      return;
    stringList.ForEach((System.Action<string>) (clientKey => this.AppendClientDictionary(key, clientKey, value.CultureName)));
  }

  /// <summary>
  /// Adds the PXCultureEx object to a list of exceptions at specified key (only if any translated values exist for this key).
  /// </summary>
  /// <param name="key">The key of the element to add exception to.</param>
  /// <param name="ex">The value of the xception to add.</param>
  public void AppendException(string key, PXCultureEx ex)
  {
    if (key == null)
      return;
    if (!this.ContainsKey(key))
      this.Add(key, new PXTranslatedRecord());
    if (this.ContainsException(key, ex.ResourceKey, ex.Language))
      return;
    this[key].Exceptions.Add(ex);
  }

  internal void AppendClientDictionary(string key, string resKey, string lang)
  {
    bool flag = false;
    bool isNotLocalizable;
    string str = this.TryGetException(key, resKey, lang, out isNotLocalizable);
    if (string.IsNullOrEmpty(str))
    {
      str = this.TryGetLocal(key, lang, out isNotLocalizable);
      flag = true;
    }
    Dictionary<string, string> dictionary;
    if (!this._clientDictionary.TryGetValue(key, out dictionary))
    {
      dictionary = new Dictionary<string, string>();
      this._clientDictionary.Add(key, dictionary);
      if (!flag)
      {
        string local = this.TryGetLocal(key, lang, out isNotLocalizable);
        if (!string.IsNullOrEmpty(local))
          dictionary.Add("default", local);
      }
    }
    if (flag)
    {
      if (dictionary.ContainsKey("default"))
        return;
      dictionary.Add("default", str);
    }
    else
      dictionary[resKey] = str;
  }

  /// <summary>
  /// Looks for translation of the given phrase to specific language.
  /// </summary>
  /// <param name="key">A word or phrase to look for.</param>
  /// <param name="lang">Destination language.</param>
  /// <returns>Translated value if it exists, otherwise null.</returns>
  public string TryGetLocal(string key, string lang, out bool isNotLocalizable)
  {
    isNotLocalizable = false;
    if (key == null || !this.ContainsKey(key))
      return (string) null;
    foreach (PXCultureValue pxCultureValue in this[key].Translated)
    {
      if (string.Compare(pxCultureValue.CultureName, lang, true) == 0)
      {
        isNotLocalizable = pxCultureValue.IsNotLocalizable;
        return pxCultureValue.Value;
      }
    }
    return (string) null;
  }

  /// <summary>
  /// Looks for exceptional translation value of the given word or phrase to specific language depending on specific resource key.
  /// </summary>
  /// <param name="key">A word or phrase to look for.</param>
  /// <param name="resKey">Destination resource key.</param>
  /// <param name="lang">Destination language.</param>
  /// <returns>Translated exceptional value if it exists, otherwise null.</returns>
  public string TryGetException(string key, string resKey, string lang, out bool isNotLocalizable)
  {
    isNotLocalizable = false;
    if (key == null || !this.ContainsKey(key))
      return (string) null;
    foreach (PXCultureEx exception in this[key].Exceptions)
    {
      if (string.Compare(resKey, exception.ResourceKey, true) == 0 && string.Compare(lang, exception.Language, true) == 0)
      {
        isNotLocalizable = exception.IsNotLocalizable;
        return exception.Value;
      }
    }
    return (string) null;
  }
}

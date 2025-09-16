// Decompiled with JetBrains decompiler
// Type: PX.Translation.PXDictionaryManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Localization.Providers;
using System.Collections.Generic;

#nullable disable
namespace PX.Translation;

/// <exclude />
[PXInternalUseOnly]
public sealed class PXDictionaryManager
{
  private readonly IPXTranslationProvider _translationProvider;
  private PXCultureDictionary cultDict;

  internal string Locale { get; }

  private PXDictionaryManager(
    PXCultureDictionary cultDict,
    string locale,
    IPXTranslationProvider translationProvider)
  {
    this.Locale = locale;
    this.cultDict = cultDict;
    this._translationProvider = translationProvider;
  }

  internal static PXDictionaryManager Load(
    string locale,
    bool includeObsolete,
    bool escapeStrings,
    IPXTranslationProvider translationProvider)
  {
    PXDictionaryManager dictionaryManager = new PXDictionaryManager((PXCultureDictionary) null, locale, translationProvider);
    dictionaryManager.DoLoad(locale, includeObsolete, escapeStrings);
    return dictionaryManager;
  }

  internal IReadOnlyDictionary<string, Dictionary<string, string>> GetClientDictionary()
  {
    return this.cultDict?.ClientDictionary;
  }

  internal IReadOnlyDictionary<string, string> GetClientResourcesForScreen(string screenId)
  {
    return this.cultDict?.GetClientResourcesForScreen(screenId);
  }

  /// <summary>
  /// Translates given value to specified language using currently loaded dictionary.
  /// </summary>
  /// <param name="value">A value to translate.</param>
  /// <param name="cultureName">Name of target culture.</param>
  /// <returns>Translated value.</returns>
  public string Translate(string value, string cultureName, out bool isNotLocalizable)
  {
    isNotLocalizable = false;
    return this.cultDict == null ? (string) null : this.cultDict.TryGetLocal(value, cultureName, out isNotLocalizable);
  }

  /// <summary>
  /// Translates given value to specified language using currently loaded dictionary.
  /// </summary>
  /// <param name="value">A value to translate.</param>
  /// <param name="resourceKey">Resource key of value being translated (to avoid conflicts).</param>
  /// <param name="cultureName">Name of target culture.</param>
  /// <returns>Translated value.</returns>
  public string Translate(
    string value,
    string resourceKey,
    string cultureName,
    out bool isNotLocalizable)
  {
    return this.Translate(value, resourceKey, cultureName, out bool _, out isNotLocalizable);
  }

  private string Translate(
    string value,
    string resourceKey,
    string cultureName,
    out bool isException,
    out bool isNotLocalizable)
  {
    isException = false;
    isNotLocalizable = false;
    if (this.cultDict == null)
      return (string) null;
    string str = this.cultDict.TryGetException(value, resourceKey, cultureName, out isNotLocalizable);
    isException = true;
    if (string.IsNullOrEmpty(str))
    {
      isException = false;
      str = this.Translate(value, cultureName, out isNotLocalizable);
    }
    return str;
  }

  private void DoLoad(string locale, bool includeObsolete, bool escapeStrings)
  {
    this.cultDict = this._translationProvider.LoadCultureDictionary(locale, includeObsolete, escapeStrings);
  }
}

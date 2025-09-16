// Decompiled with JetBrains decompiler
// Type: PX.Data.Localization.Providers.IPXTranslationProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Translation;

#nullable disable
namespace PX.Data.Localization.Providers;

/// <summary>Provider for strings localization</summary>
[PXInternalUseOnly]
public interface IPXTranslationProvider
{
  /// <summary>Loading culture dictionary for strings translation</summary>
  /// <param name="locale">Required locale</param>
  /// <param name="includeObsolete">Including obsolete strings</param>
  /// <param name="escapeStrings">Escape strings</param>
  /// <returns></returns>
  PXCultureDictionary LoadCultureDictionary(
    string locale,
    bool includeObsolete,
    bool escapeStrings);
}

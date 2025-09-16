// Decompiled with JetBrains decompiler
// Type: PX.Translation.PXTranslatedRecord
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Translation;

/// <summary>Represents single record in culture dictionary.</summary>
public class PXTranslatedRecord
{
  /// <summary>
  /// List of translation of current word or phrase to different languages.
  /// </summary>
  public readonly List<PXCultureValue> Translated = new List<PXCultureValue>();
  /// <summary>
  /// List of exceptions (alternative translations) of current
  /// word or phrase depending on resource key.
  /// </summary>
  public readonly List<PXCultureEx> Exceptions = new List<PXCultureEx>();
}

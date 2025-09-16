// Decompiled with JetBrains decompiler
// Type: PX.Translation.StringCollectingOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Translation;

/// <summary>Options for collection of localization strings.</summary>
internal class StringCollectingOptions
{
  public const string TranslationSettingsPrefix = "translation";

  /// <summary>
  /// Gets or sets a value indicating whether to collect XML comments.
  /// </summary>
  /// <value>True if collect XML comments, false if not.</value>
  public bool CollectXmlComments { get; set; } = true;
}

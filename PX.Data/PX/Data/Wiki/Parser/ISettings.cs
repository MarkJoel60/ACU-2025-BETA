// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.ISettings
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

public interface ISettings
{
  bool NamedLinks { get; }

  /// <summary>
  /// Gets or sets text whic is used as caption for EditLink in 'edit' mode.
  /// </summary>
  string EditLinkText { get; }

  /// <summary>
  /// Gets or sets text which is used as caption for EditLink in 'close' mode.
  /// </summary>
  string CloseLinkText { get; }

  /// <summary>
  /// Gets or sets URL of image used as Info icon inside of HintBox.
  /// </summary>
  string HintImageUrl { get; }

  /// <summary>
  /// Gets or sets URL of image used as Warning icon inside of WarnBox.
  /// </summary>
  string WarnImageUrl { get; }

  /// <summary>Gets or sets path to CSS styles for wiki.</summary>
  bool IsHtml { get; set; }

  string DefaultStylesPath { get; }

  string GetCSSUrl { get; set; }

  string ArticleShowUrl { get; }

  string GetFileUrl { get; }

  string FileEditUrl { get; }

  string MagnifyImageUrl { get; }

  bool FilesDirectAccess { get; }

  string PlayFlashIconUrl { get; set; }

  string GetRSSUrl { get; }

  string RSSImageUrl { get; }

  string RootUrl { get; }

  string ExternalRootUrl { get; }

  string SearchLink { get; set; }

  string SearchUnknownLink { get; set; }

  /// <summary>
  /// Gets or sets path to a default image displayed as icon for files with unknown extensions.
  /// </summary>
  string DefaultExtensionImage { get; }

  /// <summary>
  /// Gets or sets paths to images displayed as icons for files with specified extensions.
  /// </summary>
  IDictionary<string, string> ExtensionsImages { get; }
}

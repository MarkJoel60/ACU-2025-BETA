// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.Services.NavigationTemplateHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Data.Automation.Services;

internal static class NavigationTemplateHelper
{
  private const string RawPlaceholderStart = "(((";
  private static readonly Regex UrlTemplates = new Regex("\\(?\\(\\(\\w+\\)\\)\\)?", RegexOptions.IgnoreCase | RegexOptions.Compiled);

  /// <summary>Gets list of template placeholders</summary>
  /// <param name="template">Templated string with placeholders surrounded with double or triple brackets</param>
  /// <returns>List of placeholders found in the provided <paramref name="template" /></returns>
  internal static IEnumerable<string> GetTemplatePlaceholders(string template)
  {
    return NavigationTemplateHelper.UrlTemplates.Matches(template).OfType<Match>().Where<Match>((Func<Match, bool>) (x => x.Success)).Select<Match, string>((Func<Match, string>) (x => x.Value)).Distinct<string>();
  }

  /// <summary>
  /// Checks if provided <paramref name="value" /> is external URL or contains template placeholders
  /// </summary>
  /// <param name="value">Value to be checked</param>
  internal static bool IsExternalUrlOrTemplate(string value)
  {
    return PXUrl.IsExternalUrl(value) || NavigationTemplateHelper.UrlTemplates.IsMatch(value);
  }

  internal static string ProcessTemplate(
    string templatedString,
    IReadOnlyDictionary<string, object> templateParameters)
  {
    StringBuilder stringBuilder = new StringBuilder(templatedString);
    foreach (KeyValuePair<string, object> templateParameter in (IEnumerable<KeyValuePair<string, object>>) templateParameters)
    {
      string stringToEscape = templateParameter.Value?.ToString() ?? string.Empty;
      stringBuilder.Replace(templateParameter.Key, templateParameter.Key.StartsWith("(((") ? stringToEscape : Uri.EscapeDataString(stringToEscape));
    }
    return stringBuilder.ToString();
  }
}

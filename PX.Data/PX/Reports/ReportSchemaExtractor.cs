// Decompiled with JetBrains decompiler
// Type: PX.Reports.ReportSchemaExtractor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text.RegularExpressions;
using System.Web;

#nullable disable
namespace PX.Reports;

/// <summary>Report schema extractor helper.</summary>
internal static class ReportSchemaExtractor
{
  private static readonly Regex regex = new Regex("(?<Path>.*)[?]ID=(?<File>[^&|\\s]*)?", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

  public static string ExtractSchemaFromUrl(string url)
  {
    Match match = ReportSchemaExtractor.regex.Match(url);
    if (string.IsNullOrEmpty(match.Value))
      return (string) null;
    string str = match.Groups["File"].Value;
    int length = str.IndexOf("#");
    if (length > 0)
      str = str.Substring(0, length);
    return HttpUtility.UrlDecode(str);
  }
}

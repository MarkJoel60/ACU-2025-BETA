// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.ExternalFiles.RegExpHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text.RegularExpressions;

#nullable disable
namespace PX.Data.Wiki.ExternalFiles;

public class RegExpHelper
{
  private static readonly string[] parseFormats = new string[6]
  {
    "(?<dir>[\\-dl])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\w+\\s+\\w+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{4})\\s+(?<name>.+)",
    "(?<dir>[\\-dl])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\d+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{4})\\s+(?<name>.+)",
    "(?<dir>[\\-dl])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\d+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{1,2}:\\d{2})\\s+(?<name>.+)",
    "(?<dir>[\\-dl])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\w+\\s+\\w+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{1,2}:\\d{2})\\s+(?<name>.+)",
    "(?<dir>[\\-dl])(?<permission>([\\-r][\\-w][\\-xs]){3})(\\s+)(?<size>(\\d+))(\\s+)(?<ctbit>(\\w+\\s\\w+))(\\s+)(?<size2>(\\d+))\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{2}:\\d{2})\\s+(?<name>.+)",
    "(?<timestamp>\\d{2}\\-\\d{2}\\-\\d{2}\\s+\\d{2}:\\d{2}[Aa|Pp][mM])\\s+(?<dir>\\<\\w+\\>){0,1}(?<size>\\d+){0,1}\\s+(?<name>.+)"
  };

  private static Match GetMatchingRegEx(string line)
  {
    for (short index = 0; (int) index < RegExpHelper.parseFormats.Length; ++index)
    {
      Match matchingRegEx = new Regex(RegExpHelper.parseFormats[(int) index], RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace).Match(line);
      if (matchingRegEx.Success)
        return matchingRegEx;
    }
    return (Match) null;
  }

  public static ExternalFileInfo ParseFtpLine(string line)
  {
    Match matchingRegEx = RegExpHelper.GetMatchingRegEx(line);
    if (matchingRegEx == null)
      return (ExternalFileInfo) null;
    if (matchingRegEx.Groups["dir"].Value != string.Empty && matchingRegEx.Groups["dir"].Value != "-")
      return (ExternalFileInfo) null;
    return new ExternalFileInfo()
    {
      Size = long.Parse(matchingRegEx.Groups["size"].Value),
      Name = matchingRegEx.Groups["name"].Value,
      Date = System.DateTime.MinValue
    };
  }

  public static bool ValidateMask(string str, string mask)
  {
    if (string.IsNullOrEmpty(mask))
      return true;
    Regex regex = new Regex(mask, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
    return regex != null && regex.Match(str).Success;
  }

  public static bool ValidateUrl(string uri)
  {
    return RegExpHelper.ValidateMask(uri, "((https?|ftp):((//)|(\\\\\\\\))+[\\w\\d:#@%/;$()~_?\\+-=\\\\\\.&]*)");
  }

  public static bool ValidatePath(string path)
  {
    return RegExpHelper.ValidateMask(path, "^(([a-zA-Z]\\:)|(\\\\))(\\\\{1}|((\\\\{1})[^\\\\]([^/:*?<>\"|]*))+)$");
  }
}

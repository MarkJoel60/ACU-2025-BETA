// Decompiled with JetBrains decompiler
// Type: PX.Data.Database.SqlErrorMessageParserBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Data.Database;

public abstract class SqlErrorMessageParserBase : ISqlErrorMessageParser
{
  private Dictionary<int, Dictionary<int, string>> _messageTemplates;

  public SqlErrorMessageParserBase(
    Dictionary<int, Dictionary<int, string>> messageTemplates)
  {
    this._messageTemplates = messageTemplates ?? new Dictionary<int, Dictionary<int, string>>();
  }

  /// <inheritdoc />
  public bool TryParse(string errorMessage, string template, out List<string> messagePatterns)
  {
    messagePatterns = new List<string>();
    try
    {
      messagePatterns = this.ReverseStringFormat(template, errorMessage);
    }
    catch
    {
      return false;
    }
    return true;
  }

  /// <inheritdoc />
  public bool TryGetMessageTemplate(int errorCode, int languageId, out string template)
  {
    template = "";
    Dictionary<int, string> dictionary;
    return this._messageTemplates.TryGetValue(errorCode, out dictionary) && dictionary.TryGetValue(languageId, out template);
  }

  /// <inheritdoc />
  public int TryGetPatternIndex(string template, int patternNumber)
  {
    MatchCollection matchCollection = new Regex("(%\\d!)").Matches(template);
    for (int i = 0; i < matchCollection.Count; ++i)
    {
      if (matchCollection[i].Value == $"%{patternNumber}!")
        return i + 1;
    }
    return 0;
  }

  private List<string> ReverseStringFormat(string template, string str)
  {
    template = Regex.Replace(template, "[\\\\\\^\\$\\.\\|\\?\\*\\+\\(\\)]", (MatchEvaluator) (m => "\\" + m.Value));
    Match match = new Regex(this.ReplaceStringPattern(template)).Match(str);
    List<string> stringList = new List<string>();
    for (int groupnum = 0; groupnum < match.Groups.Count; ++groupnum)
      stringList.Add(match.Groups[groupnum].Value);
    return stringList;
  }

  protected string ReplaceStringPattern(string template)
  {
    return $"^{Regex.Replace(template, "%\\d!", "(.*?)")}$";
  }
}

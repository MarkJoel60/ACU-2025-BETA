// Decompiled with JetBrains decompiler
// Type: PX.Data.RichTextEdit.ParsedTag
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.RichTextEdit;

public abstract class ParsedTag
{
  protected string buffer = string.Empty;
  protected string _name;

  public string Value => this.buffer;

  public string Name => this._name;

  protected static string getID(string str, ref int index)
  {
    int startIndex = index;
    if (str.Length <= index || !char.IsLetter(str[index]))
      return string.Empty;
    ++index;
    while (index < str.Length)
    {
      char c = str[index];
      switch (c)
      {
        case '-':
        case ':':
        case '_':
          ++index;
          continue;
        default:
          if (!char.IsLetterOrDigit(c))
            return str.Substring(startIndex, index - startIndex);
          goto case '-';
      }
    }
    return str.Substring(startIndex, 0);
  }

  public static string SkipHtmlComment(string str, ref int index)
  {
    if (index + 5 >= str.Length || str.Substring(index, 4) != "<!--")
      return string.Empty;
    int num = str.IndexOf("-->", index);
    if (num < 0)
      num = index + 4;
    string str1 = str.Substring(index, num - index);
    index = num;
    return str1;
  }
}

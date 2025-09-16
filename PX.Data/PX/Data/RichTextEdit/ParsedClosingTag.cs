// Decompiled with JetBrains decompiler
// Type: PX.Data.RichTextEdit.ParsedClosingTag
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.RichTextEdit;

/// <exclude />
public class ParsedClosingTag : ParsedTag
{
  private string Opening = "<";
  private string Closing = ">";

  public ParsedClosingTag(string body, string name, string opening, string closing)
  {
    this.buffer = body;
    this._name = name;
    this.Opening = opening;
    this.Closing = closing;
  }

  public static ParsedClosingTag Parse(string str, ref int index, string opening = "<", string closing = ">")
  {
    int startIndex = index;
    if (str.Length - index < closing.Length)
      return (ParsedClosingTag) null;
    if (str.Substring(index, opening.Length) != opening)
      return (ParsedClosingTag) null;
    index += opening.Length;
    if (index >= str.Length || str[index] != '/')
    {
      index = startIndex;
      return (ParsedClosingTag) null;
    }
    ++index;
    string lower = ParsedTag.getID(str, ref index).ToLower();
    if (string.IsNullOrEmpty(lower) || str.Substring(index, closing.Length) != closing)
    {
      index = startIndex;
      return (ParsedClosingTag) null;
    }
    ++index;
    return new ParsedClosingTag(str.Substring(startIndex, opening.Length + 1 + lower.Length + closing.Length), lower, opening, closing);
  }

  public void Rename(string newName)
  {
    this.buffer = $"</{newName}>";
    this._name = newName;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.RichTextEdit.ParsedOpeningTag
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.RichTextEdit;

/// <exclude />
public class ParsedOpeningTag : ParsedTag
{
  private bool changed;
  private Dictionary<string, ParsedOpeningTag.ParseRange> attributes = new Dictionary<string, ParsedOpeningTag.ParseRange>();
  private string Opening = "<";
  private string Closing = ">";

  public bool Changed => this.changed;

  public bool Closed { get; private set; }

  public List<string> AttributeNames
  {
    get => new List<string>((IEnumerable<string>) this.attributes.Keys);
  }

  private static void SkipWhiteSpace(string str, ref int index)
  {
    while (index < str.Length && char.IsWhiteSpace(str[index]))
      ++index;
  }

  private static bool ParseAttribute(
    int tagStart,
    string str,
    ref int index,
    ParsedOpeningTag tag,
    string closing)
  {
    int num1 = index;
    string lower = ParsedTag.getID(str, ref index).ToLower();
    if (string.IsNullOrEmpty(lower))
      return false;
    int num2 = index;
    ParsedOpeningTag.SkipWhiteSpace(str, ref index);
    if (index < str.Length && str[index] == '=')
    {
      ++index;
      ParsedOpeningTag.SkipWhiteSpace(str, ref index);
      char ch = str[index];
      switch (ch)
      {
        case '"':
        case '\'':
          int num3 = str.IndexOf(ch, index + 1);
          if (num3 > -1)
          {
            tag.attributes[lower] = new ParsedOpeningTag.ParseRange(index + 1 - tagStart, num3 - tagStart, num1 - tagStart, num3 - tagStart + 1);
            index = num3 + 1;
            break;
          }
          tag.attributes[lower] = new ParsedOpeningTag.ParseRange(index + 1 - tagStart, str.Length - tagStart, num1 - tagStart, str.Length - tagStart);
          index = str.Length;
          break;
        default:
          int num4 = index;
          while (index < str.Length && !char.IsWhiteSpace(str[index]) && !(str.Substring(index, closing.Length) == closing))
            ++index;
          tag.attributes[lower] = new ParsedOpeningTag.ParseRange(num4 - tagStart, index - tagStart, num1 - tagStart, index - tagStart);
          break;
      }
    }
    else
      tag.attributes[lower] = new ParsedOpeningTag.ParseRange(num2 - tagStart, num1 - tagStart, num2 - tagStart);
    return true;
  }

  public static ParsedOpeningTag Parse(string str, ref int index, string opening = "<", string closing = ">")
  {
    int num = index;
    if (str.Length - index < opening.Length)
      return (ParsedOpeningTag) null;
    if (str.Substring(index, opening.Length) != opening)
      return (ParsedOpeningTag) null;
    index += opening.Length;
    if (index >= str.Length || str[index] == '/')
    {
      index = num;
      return (ParsedOpeningTag) null;
    }
    string lower = ParsedTag.getID(str, ref index).ToLower();
    if (string.IsNullOrWhiteSpace(lower))
    {
      index = num;
      return (ParsedOpeningTag) null;
    }
    ParsedOpeningTag tag = new ParsedOpeningTag(lower, opening, closing);
    tag.ClosingTag = $"{opening}/{lower}{closing}";
    while (index < str.Length)
    {
      char c = str[index];
      if (str.Substring(index, closing.Length) == closing)
      {
        index += closing.Length;
        tag.buffer = str.Substring(num, index - num);
        return tag;
      }
      if (c == '/' && (index >= str.Length - 1 || str.Substring(index + 1, closing.Length) == closing))
      {
        index += 1 + closing.Length;
        tag.buffer = str.Substring(num, index - num);
        tag.Closed = true;
        return tag;
      }
      if (char.IsWhiteSpace(c))
        ++index;
      else if (!ParsedOpeningTag.ParseAttribute(num, str, ref index, tag, closing))
      {
        index = num;
        return (ParsedOpeningTag) null;
      }
    }
    tag.buffer = str.Substring(num, index - num);
    return tag;
  }

  public string SkipTag(string str, ref int index)
  {
    string contents = this.ExtractContents(str, ref index);
    index += this.ClosingTag.Length;
    return this.Value + contents + this.ClosingTag;
  }

  public string ExtractContents(string str, ref int index)
  {
    int num = str.IndexOf(this.ClosingTag, index, StringComparison.InvariantCultureIgnoreCase);
    if (num > -1)
    {
      string contents = str.Substring(index, num - index);
      index = num;
      return contents;
    }
    index = str.Length;
    return str.Substring(index);
  }

  public string GetContents(string str, int index)
  {
    int num = str.IndexOf(this.ClosingTag, index, StringComparison.InvariantCultureIgnoreCase);
    return num > -1 ? str.Substring(index, num - index) : str.Substring(str.Length);
  }

  public ParsedOpeningTag(string name, string opening, string closing)
  {
    this._name = name;
    this.Closing = closing;
    this.Opening = opening;
  }

  public string NakedTag
  {
    get
    {
      return this.Value.Substring(this.Opening.Length, this.Value.Length - this.Opening.Length - this.Closing.Length);
    }
  }

  public bool HasAttribute(string name) => this.attributes.Keys.Contains<string>(name);

  public void RemoveAttribute(string name)
  {
    ParsedOpeningTag.ParseRange attribute = this.attributes[name];
    string str1 = this.buffer.Substring(0, attribute.attribStart);
    string str2 = str1 + this.buffer.Substring(attribute.attribEnd, this.buffer.Length - attribute.attribEnd);
    int diff = str2.Length - this.buffer.Length;
    int length = str1.Length;
    foreach (KeyValuePair<string, ParsedOpeningTag.ParseRange> keyValuePair in this.attributes.Where<KeyValuePair<string, ParsedOpeningTag.ParseRange>>((Func<KeyValuePair<string, ParsedOpeningTag.ParseRange>, bool>) (tuple => tuple.Key != name)))
      keyValuePair.Value.ShiftIf(length, diff);
    this.buffer = str2;
    this.attributes.Remove(name);
  }

  public string GetAttribute(string name)
  {
    if (!this.HasAttribute(name))
      return string.Empty;
    ParsedOpeningTag.ParseRange attribute = this.attributes[name];
    return this.buffer.Substring(attribute.valueStart, attribute.valueEnd - attribute.valueStart);
  }

  private static int ShiftIf(int boundary, int oldVal, int diff)
  {
    return oldVal > boundary ? oldVal + diff : oldVal;
  }

  public void SetAttribute(string name, string value)
  {
    this.changed = true;
    if (this.HasAttribute(name))
    {
      ParsedOpeningTag.ParseRange attribute = this.attributes[name];
      string str1 = this.buffer.Substring(0, attribute.valueStart);
      string str2 = this.buffer.Substring(attribute.valueEnd, this.buffer.Length - attribute.valueEnd);
      string str3 = str1 + value + str2;
      this.Shift(str1.Length, str3.Length - this.buffer.Length);
      this.buffer = str3;
    }
    else
    {
      this.buffer = this.buffer.Substring(0, this.buffer.Length - this.Closing.Length);
      if (this.buffer[this.buffer.Length - 1] != ' ')
        this.buffer += " ";
      int length = this.buffer.Length;
      int start = length + name.Length + 2;
      int end = start + value.Length;
      ParsedOpeningTag.ParseRange parseRange = new ParsedOpeningTag.ParseRange(start, end, length, end + 1);
      if (string.IsNullOrEmpty(value))
        this.buffer = $"{this.buffer}{name} ";
      else
        this.buffer = $"{this.buffer}{name}=\"{value}\"{this.Closing}";
    }
  }

  private void Shift(int boundary, int diff)
  {
    foreach (KeyValuePair<string, ParsedOpeningTag.ParseRange> attribute in this.attributes)
      attribute.Value.ShiftIf(boundary, diff);
  }

  public void ReplaceIfExists(string attrName, ParsedOpeningTag.Replacer replacer)
  {
    if (!this.HasAttribute(attrName))
      return;
    this.SetAttribute(attrName, replacer(this.GetAttribute(attrName)));
  }

  public string ClosingTag { get; set; }

  public void SkipClosingTag(string str, ref int index)
  {
    if (str.IndexOf(this.ClosingTag, index) != 0)
      return;
    index += this.ClosingTag.Length;
  }

  public void Rename(string newName)
  {
    this.buffer = this.Opening + newName + this.buffer.Substring(this._name.Length + 1);
    this.Shift(0, newName.Length - this._name.Length);
    this._name = newName;
  }

  /// <exclude />
  public class ParseRange
  {
    public int valueStart;
    public int valueEnd;
    public bool Empty;
    public int attribStart;
    public int attribEnd;

    public ParseRange(int pos, int outerStart, int outerEnd)
    {
      this.Empty = true;
      this.valueEnd = this.valueStart = 0;
      this.attribStart = outerStart;
      this.attribEnd = outerEnd;
    }

    public ParseRange(int start, int end, int outerStart, int outerEnd)
    {
      this.Empty = false;
      this.valueStart = start;
      this.valueEnd = end;
      this.attribStart = outerStart;
      this.attribEnd = outerEnd;
    }

    public void ShiftIf(int boundary, int diff)
    {
      if (this.valueStart > boundary)
        this.valueStart += diff;
      if (this.valueEnd > boundary)
        this.valueEnd += diff;
      if (this.attribStart > boundary)
        this.attribStart += diff;
      if (this.attribEnd <= boundary)
        return;
      this.attribEnd += diff;
    }
  }

  /// <exclude />
  public delegate string Replacer(string oldValue);
}

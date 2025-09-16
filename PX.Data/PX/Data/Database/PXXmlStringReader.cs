// Decompiled with JetBrains decompiler
// Type: PX.Data.Database.PXXmlStringReader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

#nullable disable
namespace PX.Data.Database;

/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringReader" /> class that reads from the specified string.</summary>
/// <param name="s">The string to which the <see cref="T:System.IO.StringReader" /> should be initialized.</param>
/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is <see langword="null" />.</exception>
internal class PXXmlStringReader(string s) : StringReader(s)
{
  public override int Read([In, Out] char[] buffer, int index, int count)
  {
    int num1 = base.Read(buffer, index, count);
    int index1 = index;
    int index2 = index;
    bool isHex = false;
    for (; index1 < index + num1; ++index1)
    {
      isHex = this.IsHexValue(buffer, index1, isHex);
      if (!isHex && this.IsXmlChar(buffer[index1]))
      {
        if (index1 != index2)
          buffer[index2] = buffer[index1];
        ++index2;
      }
    }
    int num2 = num1 - (index1 - index2);
    for (; index2 < index1; ++index2)
      buffer[index2] = char.MinValue;
    return num2;
  }

  public override int Read()
  {
    int c = base.Read();
    while (c != -1 && !this.IsXmlChar((char) c))
      c = base.Read();
    return c;
  }

  public override int Peek()
  {
    int c;
    for (c = base.Peek(); c != -1 && !this.IsXmlChar((char) c); c = base.Peek())
      base.Read();
    return c;
  }

  public override string ReadToEnd() => this.ToXmlCharString(base.ReadToEnd());

  public override string ReadLine() => this.ToXmlCharString(base.ReadLine());

  private string ToXmlCharString(string value)
  {
    if (string.IsNullOrEmpty(value))
      return value;
    StringBuilder stringBuilder = new StringBuilder(value.Length);
    foreach (char c in value)
    {
      if (this.IsXmlChar(c))
        stringBuilder.Append(c);
    }
    return stringBuilder.ToString();
  }

  private bool IsHexValue(char[] buffer, int index, bool isHex)
  {
    if (!isHex && buffer[index] == '&' && buffer[index + 1] == '#' && buffer[index + 2] == 'x')
      return true;
    return (!isHex || buffer[index - 1] != ';') && isHex;
  }

  private bool IsXmlChar(char c)
  {
    return (c == '\t' || c == '\n' || c == '\r' || c > '\u001F') && XmlConvert.IsXmlChar(c);
  }
}

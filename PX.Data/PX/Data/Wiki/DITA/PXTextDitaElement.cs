// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.PXTextDitaElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

#nullable disable
namespace PX.Data.Wiki.DITA;

public class PXTextDitaElement : PXDitaSimple
{
  public override void Write(XmlTextWriter stream, IFileManager filemanager)
  {
    stream.WriteRaw(this.Content);
  }

  public override StringBuilder Read(StringBuilder globalContent1, ExportContext _context)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append((object) globalContent1);
    if (this.Content == null)
      return stringBuilder;
    if (_context.IsShared)
    {
      _context._ConRef[_context.currentShared] = this.Content;
      stringBuilder.Append(this.Content);
      return stringBuilder;
    }
    if (_context.IsPh)
    {
      Regex regex1 = new Regex("\\(([A-Z]{2})\\.6([0-9]{1})\\.([0-9]{2})\\.([0-9]{2})\\)");
      Regex regex2 = new Regex("\\(([A-Z]{2})\\.([0-9]{2})\\.([0-9]{2})\\.([0-9]{2})\\)");
      string content = this.Content;
      Match match1 = regex1.Match(content);
      if (match1.Success)
      {
        stringBuilder.Append(" ([~/reports/reportlauncher.aspx?ID=");
        stringBuilder.Append("6");
        stringBuilder.Append(match1.Groups[1].Value);
        stringBuilder.Append(match1.Groups[2].Value);
        stringBuilder.Append(match1.Groups[3].Value);
        stringBuilder.Append(match1.Groups[4].Value);
        stringBuilder.Append(".rpx|");
        stringBuilder.Append(match1.Groups[1].Value);
        stringBuilder.Append(".");
        stringBuilder.Append("6");
        stringBuilder.Append(match1.Groups[2].Value);
        stringBuilder.Append(".");
        stringBuilder.Append(match1.Groups[3].Value);
        stringBuilder.Append(".");
        stringBuilder.Append(match1.Groups[4].Value);
        stringBuilder.Append("])");
      }
      else
      {
        Match match2 = regex2.Match(this.Content);
        if (match2.Success)
        {
          stringBuilder.Append(" ([~/pages/");
          stringBuilder.Append(match2.Groups[1].Value);
          stringBuilder.Append("/");
          stringBuilder.Append(match2.Groups[1].Value);
          stringBuilder.Append(match2.Groups[2].Value);
          stringBuilder.Append(match2.Groups[3].Value);
          stringBuilder.Append(match2.Groups[4].Value);
          stringBuilder.Append(".aspx|");
          stringBuilder.Append(match2.Groups[1].Value);
          stringBuilder.Append(".");
          stringBuilder.Append(match2.Groups[2].Value);
          stringBuilder.Append(".");
          stringBuilder.Append(match2.Groups[3].Value);
          stringBuilder.Append(".");
          stringBuilder.Append(match2.Groups[4].Value);
          stringBuilder.Append("])");
        }
        else
          stringBuilder.Append(this.Content);
      }
      return stringBuilder;
    }
    if (!_context.IsPre && !_context.IsCodeBlock)
    {
      this.Content = this.Content.Replace("\n", "");
      this.Content = this.Content.Replace("\r", "");
    }
    this.Content = this.Content.Replace("<", "&lt;");
    this.Content = this.Content.Replace(">", "&gt;");
    this.Content = this.Content.Replace("&#38;", "&amp;");
    this.Content = this.Content.Replace("&#8482;", "&trade;");
    this.Content = this.Content.Replace("&#191;", "&iquest;");
    this.Content = this.Content.Replace("&#169;", "&copy;");
    this.Content = this.Content.Replace("&#161;", "&iexcl;");
    this.Content = this.Content.Replace("&#60;", "&lsaquo;");
    this.Content = this.Content.Replace("&#62;", "&rsaquo;");
    this.Content = this.Content.Replace("&#174;", "&reg;");
    this.Content = this.Content.Replace("&#167;", "&sect;");
    this.Content = this.Content.Replace("&#187;", "&cent;");
    this.Content = this.Content.Replace("&#182;", "&para;");
    this.Content = this.Content.Replace("&#171;", "&laquo;");
    this.Content = this.Content.Replace("&#187;", "&raquo;");
    this.Content = this.Content.Replace("&#8364;", "&euro;");
    this.Content = this.Content.Replace("&#8224;", "&dagger;");
    this.Content = this.Content.Replace("&#8216;", "&lsquo;");
    this.Content = this.Content.Replace("&#8217;", "&rsquo;");
    this.Content = this.Content.Replace("&#165;", "&yen;");
    this.Content = this.Content.Replace("&#8225;", "&Dagger;");
    this.Content = this.Content.Replace("&#163;", "&pound;");
    this.Content = this.Content.Replace("&#8226;", "&bull;");
    this.Content = this.Content.Replace("&#164;", "&curren;");
    this.Content = this.Content.Replace("&#8211;", "&ndash;");
    this.Content = this.Content.Replace("&#8220;", "&ldquo;");
    this.Content = this.Content.Replace("&#8221;", "&rdquo;");
    this.Content = this.Content.Replace("&#151;", "&mdash;");
    this.Content = this.Content.Replace("&#150;", "-");
    while (this.Content.IndexOf("  ") > -1)
      this.Content = this.Content.Replace("  ", " ");
    stringBuilder.Append(this.Content);
    return stringBuilder;
  }
}

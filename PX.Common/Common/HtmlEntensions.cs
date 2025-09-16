// Decompiled with JetBrains decompiler
// Type: PX.Common.HtmlEntensions
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

#nullable enable
namespace PX.Common;

public static class HtmlEntensions
{
  private static string \u0002 = "48fa303b-ab44-43a1-91ed-99006919eae3";
  private static string \u000E = "13fc34b9-1efc-434b-bf77-4f857731c2e5";
  private static string \u0006 = "ccb5d4fc-b52b-4e8b-a256-20d0956a10c0";
  private static string \u0008 = "192d7c0a-80b1-4918-86d3-67755f4ae96f";
  private static string \u0003 = "\r\n";
  private static string[] \u000F = new string[16 /*0x10*/]
  {
    "#text",
    "br",
    "span",
    "i",
    "b",
    "a",
    "input",
    "select",
    "button",
    "sub",
    "sup",
    "strong",
    "small",
    "label",
    "img",
    "svg"
  };
  private static string[] \u0005 = new string[6]
  {
    "br",
    "input",
    "select",
    "button",
    "img",
    "svg"
  };

  public static string? MergeHtmls(string? firstHtmlText, string? secondHtmlText)
  {
    if (Str.IsNullOrEmpty(secondHtmlText))
      return firstHtmlText;
    if (Str.IsNullOrEmpty(firstHtmlText))
      return secondHtmlText;
    firstHtmlText = HtmlEntensions.ConvertSimpleTextToHtml(firstHtmlText);
    secondHtmlText = HtmlEntensions.ConvertSimpleTextToHtml(secondHtmlText);
    HtmlDocument htmlDocument1 = new HtmlDocument();
    htmlDocument1.LoadHtml(firstHtmlText);
    HtmlDocument htmlDocument2 = new HtmlDocument();
    htmlDocument2.LoadHtml(secondHtmlText);
    HtmlEntensions.\u0002(htmlDocument1.DocumentNode.\u0002("html", false), htmlDocument2.DocumentNode.\u0002("html", false));
    return htmlDocument1.DocumentNode.OuterHtml;
  }

  public static string GetHtmlBodyContent(string? html)
  {
    if (string.IsNullOrEmpty(html))
      return string.Empty;
    html = HtmlEntensions.ConvertSimpleTextToHtml(html);
    HtmlDocument htmlDocument = new HtmlDocument();
    htmlDocument.LoadHtml(html);
    return htmlDocument.DocumentNode.\u0002(nameof (html), false).\u0002("body", false).InnerHtml ?? string.Empty;
  }

  private static void \u0002(HtmlNode _param0, HtmlNode _param1)
  {
    HtmlEntensions.\u000E(_param0.\u0002("head", false), _param1.\u0002("head", false));
    HtmlEntensions.\u0002(_param0.\u0002("body", false), _param1.\u0002("body", false), true, true);
  }

  private static void \u000E(HtmlNode _param0, HtmlNode _param1)
  {
    HtmlEntensions.\u0002(_param0.\u0002("title", false), _param1.\u0002("title", false), true, true);
    HtmlEntensions.\u0002(_param0.\u0002("style", false), _param1.\u0002("style", false), true, true);
    HtmlEntensions.\u0006(_param0, _param1);
  }

  private static void \u0002(HtmlNode _param0, HtmlNode _param1, bool _param2, bool _param3)
  {
    _param0.InnerHtml = _param2 ? _param0.InnerHtml + _param1.InnerHtml : (_param3 ? _param1.InnerHtml : _param0.InnerHtml);
    foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) _param1.Attributes)
    {
      if (!_param0.Attributes.Contains(attribute.Name))
        _param0.Attributes.Add(attribute);
      else
        _param0.Attributes[attribute.Name].Value = _param2 ? _param0.Attributes[attribute.Name].Value + attribute.Value : (_param3 ? attribute.Value : _param0.Attributes[attribute.Name].Value);
    }
  }

  private static void \u0006(HtmlNode _param0, HtmlNode _param1)
  {
    HtmlNodeCollection htmlNodeCollection = _param1.SelectNodes("meta");
    if (htmlNodeCollection == null)
      return;
    foreach (HtmlNode htmlNode in (IEnumerable<HtmlNode>) htmlNodeCollection)
    {
      string str = htmlNode.Attributes["http-equiv"]?.Value;
      if (str != null)
        HtmlEntensions.\u0002(_param0.\u0002("meta", ("http-equiv", str), true), htmlNode, false, false);
    }
  }

  private static HtmlNode \u0002(
    this HtmlNode? _param0,
    string _param1,
    (string, string) _param2,
    bool _param3)
  {
    HtmlEntensions.\u0008 obj = new HtmlEntensions.\u0008();
    obj.\u0002 = _param2;
    HtmlNodeCollection source = _param0?.SelectNodes(_param1);
    if (source != null)
    {
      HtmlNode htmlNode = ((IEnumerable<HtmlNode>) source).FirstOrDefault<HtmlNode>(new Func<HtmlNode, bool>(obj.\u0002));
      if (htmlNode != null)
        return htmlNode;
    }
    return _param0.\u000E(_param1, _param3);
  }

  private static HtmlNode \u0002(this HtmlNode? _param0, string _param1, bool _param2)
  {
    return _param0?.ChildNodes[_param1] ?? _param0.\u000E(_param1, _param2);
  }

  private static HtmlNode \u000E(this HtmlNode? _param0, string _param1, bool _param2)
  {
    HtmlNode node;
    if (!_param2)
      node = HtmlNode.CreateNode($"<{_param1}></{_param1}>");
    else
      node = HtmlNode.CreateNode($"<{_param1}/>");
    HtmlNode htmlNode = node;
    htmlNode.Name = _param1;
    _param0?.AppendChild(htmlNode);
    return htmlNode;
  }

  public static string RemoveHeader(string? html)
  {
    if (Str.IsNullOrEmpty(html) || html.IndexOf("<html", StringComparison.OrdinalIgnoreCase) < 0)
      return html ?? string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    using (TextReader textReader = (TextReader) new StringReader(html))
    {
      try
      {
        FlexibleXmlReader flexibleXmlReader = FlexibleXmlReader.Create(textReader, false);
        bool flag1 = false;
        bool flag2 = false;
        while (flexibleXmlReader.Read())
        {
          switch (flexibleXmlReader.NodeType)
          {
            case XmlNodeType.Element:
              switch (flexibleXmlReader.Name.ToUpper())
              {
                case "BODY":
                  flag1 = true;
                  continue;
                case "BR":
                  if (flag2)
                  {
                    stringBuilder.Append(">");
                    flag2 = false;
                  }
                  stringBuilder.Append("<br>");
                  continue;
                default:
                  if (flag2)
                    stringBuilder.Append(">");
                  if (flag1)
                  {
                    flag2 = true;
                    stringBuilder.Append("<");
                    stringBuilder.Append(flexibleXmlReader.Name);
                    continue;
                  }
                  continue;
              }
            case XmlNodeType.Attribute:
              if (flag2)
              {
                stringBuilder.Append(" ");
                stringBuilder.Append(flexibleXmlReader.Name);
                stringBuilder.Append("=\"");
                stringBuilder.Append(flexibleXmlReader.Value);
                stringBuilder.Append("\"");
                continue;
              }
              continue;
            case XmlNodeType.Text:
              if (flag1)
              {
                if (flag2)
                {
                  stringBuilder.Append(">");
                  flag2 = false;
                }
                stringBuilder.Append(flexibleXmlReader.Value);
                continue;
              }
              continue;
            case XmlNodeType.EndElement:
              if (flag1)
              {
                switch (flexibleXmlReader.Name.ToUpper())
                {
                  case "BODY":
                    return stringBuilder.ToString();
                  case "HTML":
                    return stringBuilder.ToString();
                  default:
                    if (flag2)
                    {
                      stringBuilder.Append(">");
                      flag2 = false;
                    }
                    stringBuilder.Append("</");
                    stringBuilder.Append(flexibleXmlReader.Name);
                    stringBuilder.Append(">");
                    continue;
                }
              }
              else
                continue;
            default:
              continue;
          }
        }
      }
      catch (StackOverflowException ex)
      {
        throw;
      }
      catch (OutOfMemoryException ex)
      {
        throw;
      }
      catch
      {
      }
    }
    return stringBuilder.ToString();
  }

  public static string? ConvertHtmlFragmentToSimpleText(string? html)
  {
    if (!Str.IsNullOrEmpty(html))
    {
      using (TextReader textReader = (TextReader) new StringReader(html))
      {
        try
        {
          FlexibleXmlReader flexibleXmlReader = FlexibleXmlReader.Create(textReader, false);
          StringBuilder stringBuilder = new StringBuilder();
          bool flag = false;
          while (flexibleXmlReader.Read())
          {
            switch (flexibleXmlReader.NodeType)
            {
              case XmlNodeType.Element:
                switch (flexibleXmlReader.Name.ToUpper())
                {
                  case "BR":
                  case "HR":
                  case "LI":
                    stringBuilder.AppendLine();
                    continue;
                  case "DIV":
                  case "TR":
                    flag = true;
                    continue;
                  default:
                    continue;
                }
              case XmlNodeType.Text:
                if (flag)
                {
                  stringBuilder.AppendLine();
                  flag = false;
                }
                stringBuilder.Append(HttpUtility.HtmlDecode(flexibleXmlReader.Value));
                continue;
              case XmlNodeType.EndElement:
                switch (flexibleXmlReader.Name.ToUpper())
                {
                  case "OL":
                  case "UL":
                    stringBuilder.AppendLine();
                    continue;
                  case "BODY":
                    return stringBuilder.ToString();
                  default:
                    continue;
                }
              default:
                continue;
            }
          }
          html = stringBuilder.ToString();
        }
        catch (StackOverflowException ex)
        {
          throw;
        }
        catch (OutOfMemoryException ex)
        {
          throw;
        }
        catch
        {
        }
      }
    }
    return html;
  }

  private static string \u0002(string _param0, string _param1, int _param2)
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < Tools.Max<int>(1, _param2); ++index)
      stringBuilder.Append(_param1);
    string newValue = stringBuilder.ToString();
    string oldValue = newValue + _param1;
    while (true)
    {
      string str = _param0.Replace(oldValue, newValue);
      if (str.Length != _param0.Length)
        _param0 = str;
      else
        break;
    }
    return _param0;
  }

  public static string ConvertHtmlToSimpleText(string? html)
  {
    if (Str.IsNullOrEmpty(html) || !HtmlEntensions.\u0002(html))
      return html ?? string.Empty;
    HtmlDocument htmlDocument = new HtmlDocument();
    htmlDocument.LoadHtml(html);
    StringWriter stringWriter = new StringWriter();
    HtmlEntensions.\u000E(htmlDocument.DocumentNode, (TextWriter) stringWriter);
    stringWriter.Flush();
    return HtmlEntensions.\u0002(HtmlEntensions.\u0002(HtmlEntensions.\u0002(stringWriter.ToString(), HtmlEntensions.\u000E, 1), HtmlEntensions.\u0006, 1).Replace(HtmlEntensions.\u000E + HtmlEntensions.\u0006, "").Replace(HtmlEntensions.\u0006 + HtmlEntensions.\u000E, HtmlEntensions.\u0003).Replace(HtmlEntensions.\u000E, HtmlEntensions.\u0003).Replace(HtmlEntensions.\u0006, HtmlEntensions.\u0003).Replace(HtmlEntensions.\u0008, ""), HtmlEntensions.\u0002, 1).Replace(HtmlEntensions.\u0002, " ").Trim();
  }

  private static bool \u0002(string _param0)
  {
    return ((IEnumerable<string>) new string[7]
    {
      "<html",
      "<head",
      "<body",
      "<script",
      "<p",
      "<br",
      "<div"
    }).Any<string>(new Func<string, bool>(new HtmlEntensions.\u0002()
    {
      \u0002 = _param0
    }.\u0002));
  }

  private static void \u0002(HtmlNode _param0, TextWriter _param1)
  {
    foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) _param0.ChildNodes)
      HtmlEntensions.\u000E(childNode, _param1);
  }

  private static bool \u0002([NotNullWhen(true)] HtmlNode? _param0)
  {
    HtmlEntensions.\u000E obj = new HtmlEntensions.\u000E();
    obj.\u0002 = _param0;
    return obj.\u0002 != null && ((IEnumerable<string>) HtmlEntensions.\u000F).Any<string>(new Func<string, bool>(obj.\u0002));
  }

  private static bool \u000E([NotNullWhen(true)] HtmlNode? _param0)
  {
    HtmlEntensions.\u0006 obj = new HtmlEntensions.\u0006();
    obj.\u0002 = _param0;
    return obj.\u0002 != null && ((IEnumerable<string>) HtmlEntensions.\u0005).Any<string>(new Func<string, bool>(obj.\u0002));
  }

  private static bool \u0002(HtmlNode? _param0, HtmlNode? _param1)
  {
    if (_param0 == null)
    {
      if (_param1 == null)
        return false;
      HtmlNode parentNode = _param1.ParentNode;
      return parentNode != null && HtmlEntensions.\u0002(parentNode) && HtmlEntensions.\u0002(parentNode.NextSibling, parentNode);
    }
    if (HtmlEntensions.\u000E(_param0))
      return true;
    if (!HtmlEntensions.\u0002(_param0))
      return false;
    return _param0.NodeType == 3 && _param0.InnerText.Trim().Length != 0 || HtmlEntensions.\u0002(_param0.FirstChild, (HtmlNode) null) || HtmlEntensions.\u0002(_param0.NextSibling, _param1);
  }

  private static void \u000E(HtmlNode _param0, TextWriter _param1)
  {
    switch ((int) _param0.NodeType)
    {
      case 0:
        HtmlEntensions.\u0002(_param0, _param1);
        break;
      case 1:
        string name = _param0.Name;
        if (name != null)
        {
          switch (name.Length)
          {
            case 1:
              if (name == "p")
                break;
              goto label_33;
            case 2:
              switch (name[0])
              {
                case 'b':
                  if (name == "br" && HtmlEntensions.\u0002(_param0.NextSibling, _param0))
                  {
                    _param1.Write(HtmlEntensions.\u0003);
                    goto label_33;
                  }
                  goto label_33;
                case 'h':
                  if (name == "hr")
                  {
                    _param1.Write(HtmlEntensions.\u0003);
                    goto label_33;
                  }
                  goto label_33;
                case 'l':
                  if (name == "li")
                    break;
                  goto label_33;
                case 't':
                  if (name == "tr")
                    break;
                  goto label_33;
                default:
                  goto label_33;
              }
              break;
            case 3:
              switch (name[0])
              {
                case 'd':
                  if (name == "div")
                    break;
                  goto label_33;
                case 'i':
                  if (name == "img")
                  {
                    _param1.Write(_param0.OuterHtml);
                    goto label_33;
                  }
                  goto label_33;
                case 'p':
                  if (name == "pre")
                    break;
                  goto label_33;
                default:
                  goto label_33;
              }
              break;
            default:
              goto label_33;
          }
          _param1.Write(HtmlEntensions.\u000E);
          if (HtmlEntensions.\u0002(_param0.FirstChild, (HtmlNode) null))
            _param1.Write(HtmlEntensions.\u0008);
        }
label_33:
        if (_param0.HasChildNodes)
        {
          if (_param0.Name != "td")
          {
            HtmlEntensions.\u0002(_param0, _param1);
          }
          else
          {
            StringWriter stringWriter = new StringWriter();
            HtmlEntensions.\u0002(_param0, (TextWriter) stringWriter);
            string str = HtmlEntensions.\u0002(stringWriter.ToString().Replace(HtmlEntensions.\u0003, HtmlEntensions.\u0002).Replace(HtmlEntensions.\u000E, HtmlEntensions.\u0002).Replace(HtmlEntensions.\u0006, HtmlEntensions.\u0002).Replace(HtmlEntensions.\u0008, ""), HtmlEntensions.\u0002, 1).Replace(HtmlEntensions.\u0002, " ");
            _param1.Write(str.Trim());
          }
        }
        switch (_param0.Name)
        {
          case "tr":
          case "li":
          case "div":
          case "p":
          case "pre":
            _param1.Write(HtmlEntensions.\u0006);
            return;
          case "td":
            _param1.Write(HtmlEntensions.\u0002);
            return;
          default:
            return;
        }
      case 3:
        switch (_param0.ParentNode.Name)
        {
          case "script":
            return;
          case "style":
            return;
          case "head":
            return;
          default:
            string str1 = ((HtmlTextNode) _param0).Text;
            string str2;
            if (_param0.ParentNode != null && _param0.ParentNode.Name != "pre")
            {
              if (!HtmlEntensions.\u0002(_param0.NextSibling, _param0))
                str1 = str1.Trim();
              str2 = str1.Replace("\n", HtmlEntensions.\u0002).Replace("\r", "");
            }
            else
              str2 = str1.Trim();
            if (HtmlNode.IsOverlappedClosingElement(str2) || str2.Trim().Length <= 0)
              return;
            _param1.Write(HtmlEntity.DeEntitize(str2));
            return;
        }
    }
  }

  public static string ConvertSimpleTextToHtml(string? simpleText)
  {
    if (simpleText == null)
      simpleText = string.Empty;
    return !HtmlEntensions.\u0002(simpleText) ? $"<html><body>{HtmlEntensions.\u0002(simpleText)}</body></html>" : simpleText;
  }

  public static string ConvertSimpleTextToHtmlWithStyles(string? html)
  {
    if (html == null)
      html = "<html><head><title></title><style></style></head><body></body></html>";
    if (!HtmlEntensions.\u0002(html))
      html = $"<html><head><title></title><style></style></head><body>{HtmlEntensions.\u0002(html)}</body></html>";
    else if (!html.Contains("<style"))
      html = html.Contains("<head>") ? html.Insert(html.IndexOf("<head>", StringComparison.OrdinalIgnoreCase) + "<head>".Length, "<style></style>") : html.Insert(html.IndexOf("<html>", StringComparison.OrdinalIgnoreCase) + "<html>".Length, "<head><title></title><style></style></head>");
    return html;
  }

  private static string \u0002(string? _param0)
  {
    if (_param0 == null)
      _param0 = string.Empty;
    return _param0.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\n", "<br/>").Replace("\r\n", "<br/>").Replace("\n\r", "<br/>").Replace("\n\n", "<br/><br/>").Replace("\r\r", "<br/><br/>");
  }

  private sealed class \u0002
  {
    public 
    #nullable disable
    string \u0002;

    internal bool \u0002(string _param1)
    {
      return this.\u0002.IndexOf(_param1, StringComparison.OrdinalIgnoreCase) >= 0;
    }
  }

  private sealed class \u0006
  {
    public HtmlNode \u0002;

    internal bool \u0002(string _param1)
    {
      return _param1.Equals(this.\u0002.Name, StringComparison.OrdinalIgnoreCase);
    }
  }

  private sealed class \u0008
  {
    public (
    #nullable enable
    string, string) \u0002;

    internal bool \u0002(
    #nullable disable
    HtmlNode _param1)
    {
      return string.Equals(_param1.GetAttributeValue(this.\u0002.Item1, (string) null), this.\u0002.Item2);
    }
  }

  private sealed class \u000E
  {
    public HtmlNode \u0002;

    internal bool \u0002(string _param1)
    {
      return _param1.Equals(this.\u0002.Name, StringComparison.OrdinalIgnoreCase);
    }
  }
}

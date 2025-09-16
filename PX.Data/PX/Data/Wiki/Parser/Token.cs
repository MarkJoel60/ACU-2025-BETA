// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Token
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

public class Token
{
  internal const string HtmlStart = "htmlstart";
  public static Token h1 = new Token(nameof (h1));
  public static Token h2 = new Token(nameof (h2));
  public static Token h3 = new Token(nameof (h3));
  public static Token h4 = new Token(nameof (h4));
  public static Token bold = new Token(nameof (bold));
  public static Token italic = new Token(nameof (italic));
  public static Token bolditalic = new Token(nameof (bolditalic));
  public static Token underlined = new Token(nameof (underlined));
  public static Token striked = new Token(nameof (striked));
  public static Token list = new Token(nameof (list));
  public static Token linkstart = new Token(nameof (linkstart));
  public static Token linkend = new Token(nameof (linkend));
  public static Token link2start = new Token(nameof (link2start));
  public static Token link2end = new Token(nameof (link2end));
  public static Token linkseparator = new Token(nameof (linkseparator));
  public static Token image = new Token(nameof (image));
  public static Token rss = new Token(nameof (rss));
  public static Token rssart = new Token(nameof (rssart));
  public static Token horline = new Token(nameof (horline));
  public static Token codestart = new Token(nameof (codestart));
  public static Token codeend = new Token(nameof (codeend));
  public static Token codeboxstart = new Token(nameof (codeboxstart));
  public static Token codeboxend = new Token(nameof (codeboxend));
  public static Token boxstart = new Token(nameof (boxstart));
  public static Token boxend = new Token(nameof (boxend));
  public static Token indent = new Token(nameof (indent));
  public static Token specialtagstart = new Token(nameof (specialtagstart));
  public static Token specialtagend = new Token(nameof (specialtagend));
  public static Token tablestart = new Token(nameof (tablestart));
  public static Token tableend = new Token(nameof (tableend));
  public static Token htmlstart = new Token(nameof (htmlstart));
  public static Token htmlend = new Token(nameof (htmlend));
  public static Token infoboxstart = new Token(nameof (infoboxstart));
  public static Token infoboxend = new Token(nameof (infoboxend));
  public static Token templatestart = new Token(nameof (templatestart));
  public static Token templateend = new Token(nameof (templateend));
  public static Token hiddenstart = new Token(nameof (hiddenstart));
  public static Token hiddenend = new Token(nameof (hiddenend));
  public static Token emptytag = new Token(nameof (emptytag));
  public static Token chars = new Token(nameof (chars));
  public static Token codeitalic = new Token(nameof (codeitalic));
  public static Token codebold = new Token(nameof (codebold));
  public static Token endoftext = new Token(nameof (endoftext));
  public static Token htmlstartpartial = new Token(nameof (htmlstartpartial));
  public static Token embedvideostart = new Token(nameof (embedvideostart));
  public static Token embedvideoend = new Token(nameof (embedvideoend));

  public Token(string value) => this.Value = value;

  public override string ToString() => this.Value;

  public string Value { get; private set; }
}

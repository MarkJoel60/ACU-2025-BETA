// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXImageParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class PXImageParser : PXBlockParser
{
  protected override bool IsAllowedForParsing(Token tk) => false;

  protected override void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    StringBuilder wikiText = new StringBuilder("Image:");
    string imageName = this.GetImageName(context, wikiText);
    string imageOptions = this.GetImageOptions(context);
    int result1 = 0;
    if (string.IsNullOrEmpty(imageName))
      return;
    string[] strArray = imageName.Split(':');
    if (strArray.Length == 2)
    {
      imageName = strArray[0];
      int.TryParse(strArray[1], out result1);
    }
    wikiText.Append(imageOptions);
    PXImageElement pxImageElement = new PXImageElement(imageName);
    this.TryAddElementToParagraph((PXElement) pxImageElement, context, result);
    this.ParseImageOptions(pxImageElement, imageOptions, context);
    if (result1 != 0)
      pxImageElement.FileRevision = new int?(result1);
    if (!context.Settings.IsDesignMode)
      return;
    pxImageElement.WikiTag = "image";
    pxImageElement.WikiText = wikiText.ToString();
  }

  private string GetImageName(PXBlockParser.ParseContext context, StringBuilder wikiText)
  {
    string imageName = "";
    string TokenValue = "";
    while (context.StartIndex < context.WikiText.Length)
    {
      Token nextToken = this.GetNextToken(context, out TokenValue);
      if (nextToken == Token.linkend || nextToken == Token.link2end)
      {
        context.StartIndex -= TokenValue.Length;
        break;
      }
      wikiText.Append(TokenValue);
      if (nextToken != Token.linkseparator)
        imageName += TokenValue;
      else
        break;
    }
    return imageName;
  }

  private string GetImageOptions(PXBlockParser.ParseContext context)
  {
    string imageOptions = "";
    string TokenValue = "";
    while (context.StartIndex < context.WikiText.Length)
    {
      Token nextToken = this.GetNextToken(context, out TokenValue);
      if (nextToken == Token.linkend || nextToken == Token.link2end)
      {
        context.StartIndex -= TokenValue.Length;
        break;
      }
      imageOptions += TokenValue;
    }
    return imageOptions;
  }

  private void ParseImageOptions(
    PXImageElement e,
    string options,
    PXBlockParser.ParseContext context)
  {
    string str1 = options;
    char[] separator = new char[1]{ '|' };
    foreach (string str2 in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
    {
      string lower = str2.Trim().ToLower();
      if (lower != null)
      {
        switch (lower.Length)
        {
          case 4:
            if (lower == "left")
            {
              e.Location = ImageLocation.Left;
              continue;
            }
            goto label_27;
          case 5:
            switch (lower[0])
            {
              case 'e':
                if (lower == "embed")
                {
                  e.IsEmbedded = true;
                  continue;
                }
                goto label_27;
              case 'f':
                if (lower == "frame")
                {
                  e.Type = ImageType.Frame;
                  continue;
                }
                goto label_27;
              case 'p':
                if (lower == "popup")
                {
                  e.Type = ImageType.Popup;
                  continue;
                }
                goto label_27;
              case 'r':
                if (lower == "right")
                {
                  e.Location = ImageLocation.Right;
                  continue;
                }
                goto label_27;
              case 't':
                if (lower == "thumb")
                  break;
                goto label_27;
              default:
                goto label_27;
            }
            break;
          case 6:
            switch (lower[0])
            {
              case 'b':
                if (lower == "border")
                {
                  e.Type = ImageType.Border;
                  continue;
                }
                goto label_27;
              case 'c':
                if (lower == "center")
                {
                  e.Location = ImageLocation.Center;
                  continue;
                }
                goto label_27;
              case 'n':
                if (lower == "noedit")
                {
                  e.HasEditLink = false;
                  continue;
                }
                goto label_27;
              default:
                goto label_27;
            }
          case 7:
            if (lower == "noclick")
            {
              e.IsClickable = false;
              continue;
            }
            goto label_27;
          case 9:
            if (lower == "thumbnail")
              break;
            goto label_27;
          default:
            goto label_27;
        }
        e.Type = ImageType.Thumb;
        continue;
      }
label_27:
      string props = str2.Trim();
      if (!PXImageParser.TryParseSize((IWidthHeightSettable) e, props.ToLower()))
      {
        if (props.ToLower().StartsWith("http://") || props.ToLower().StartsWith("https://"))
          e.NavigateUrl = props;
        else if (props.StartsWith("~/"))
          e.NavigateUrl = props.Substring(1);
        else if (props.StartsWith(">") && props.Length > 1)
          e.InternalLink = props.Substring(1).Trim();
        else if (string.IsNullOrEmpty(e.Caption))
          e.Caption = props;
        else
          e.Props = this.SanitizeAttributes(props, context.Settings);
      }
    }
  }

  public static bool TryParseSize(IWidthHeightSettable e, string str)
  {
    if (string.IsNullOrEmpty(str))
      return false;
    if (str.Length >= 2)
      str = str.Replace("px", "");
    string[] strArray = str.Split('x');
    if (strArray.Length > 2)
      return false;
    int num = -1;
    int sizeValue = PXImageParser.TryParseSizeValue(strArray[0]);
    if (sizeValue == -1)
      return false;
    e.Width = sizeValue;
    if (strArray.Length == 2)
      num = PXImageParser.TryParseSizeValue(strArray[1]);
    if (num != -1)
      e.Height = num;
    return true;
  }

  private static int TryParseSizeValue(string value)
  {
    if (value.Length >= 2 && value.Substring(value.Length - 2).ToLower() == "px")
      value = value.Remove(value.Length - 2);
    int result;
    return int.TryParse(value, out result) ? result : -1;
  }
}

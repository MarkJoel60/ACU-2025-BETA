// Decompiled with JetBrains decompiler
// Type: PX.Data.ImageExtractor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using HtmlAgilityPack;
using PX.Data.RichTextEdit;
using PX.Data.Wiki.Parser;
using PX.SM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

#nullable disable
namespace PX.Data;

public sealed class ImageExtractor
{
  private const string EMBEDDED_PREFIX = "data:";
  public const string PREFIX_IMG_BY_FILEID = "~/Frames/GetFile.ashx?fileID=";

  public bool Extract(
    string content,
    out string newContent,
    out ICollection<ImageExtractor.ImageInfo> images,
    List<Attachment> inFiles)
  {
    if (inFiles == null)
      throw new ArgumentNullException(nameof (inFiles));
    return this.Extract(content, out newContent, out images, knownFileDataExtractor: (Func<(string, string), ImageExtractor.ImageInfo>) (item =>
    {
      Attachment attachment = inFiles.FirstOrDefault<Attachment>((Func<Attachment, bool>) (i => string.Equals(i.ContentId, item.cid, StringComparison.InvariantCultureIgnoreCase)));
      if (attachment == null)
        return (ImageExtractor.ImageInfo) null;
      using (MemoryStream destination = new MemoryStream((int) attachment.ContentStream.Length))
      {
        long position = attachment.ContentStream.Position;
        attachment.ContentStream.Seek(0L, SeekOrigin.Begin);
        attachment.ContentStream.CopyTo((Stream) destination);
        attachment.ContentStream.Seek(position, SeekOrigin.Begin);
        return new ImageExtractor.ImageInfo(Guid.NewGuid(), destination.ToArray(), item.alt, item.cid);
      }
    }));
  }

  public bool Extract(
    string content,
    out string newContent,
    out ICollection<ImageExtractor.ImageInfo> images,
    Func<ImageExtractor.ImageInfo, (string src, string title)> getSrcAndTitle = null,
    Func<(string cid, string alt), ImageExtractor.ImageInfo> knownFileDataExtractor = null)
  {
    if (content == null)
      throw new ArgumentNullException(nameof (content));
    if (getSrcAndTitle == null)
      getSrcAndTitle = (Func<ImageExtractor.ImageInfo, (string, string)>) (img => ("cid:" + img.CID, (string) null));
    images = (ICollection<ImageExtractor.ImageInfo>) new List<ImageExtractor.ImageInfo>();
    StringBuilder stringBuilder = new StringBuilder();
    int index = 0;
    PXGraph instance = PXGraph.CreateInstance<PXGraph>();
    while (index < content.Length)
    {
      stringBuilder.Append(ParsedTag.SkipHtmlComment(content, ref index));
      ParsedOpeningTag parsedOpeningTag = ParsedOpeningTag.Parse(content, ref index);
      if (parsedOpeningTag != null)
      {
        if (!(parsedOpeningTag.Name == "base"))
        {
          if (parsedOpeningTag.Name == "img" && parsedOpeningTag.HasAttribute("src"))
          {
            string attribute1 = parsedOpeningTag.GetAttribute("src");
            if (this.IsEmbeddedSource(attribute1))
            {
              string attribute2 = parsedOpeningTag.GetAttribute("alt");
              ImageExtractor.ImageInfo imageInfo;
              if (this.ParseEmbedded(attribute1, attribute2, out imageInfo))
              {
                images.Add(imageInfo);
                (string str1, string str2) = getSrcAndTitle(imageInfo);
                parsedOpeningTag.SetAttribute("src", str1);
                if (str2 != null)
                  parsedOpeningTag.SetAttribute("title", str2);
              }
            }
            else
            {
              string contentId = attribute1.StartsWith("cid:") ? attribute1.Substring(4) : HttpUtility.UrlDecode(attribute1);
              string str3 = parsedOpeningTag.GetAttribute("alt") ?? "image";
              ImageExtractor.ImageInfo imageInfo = knownFileDataExtractor != null ? knownFileDataExtractor((contentId, str3)) : (ImageExtractor.ImageInfo) null;
              if (imageInfo != null)
              {
                images.Add(imageInfo);
              }
              else
              {
                ICollection<ImageExtractor.ImageInfo> source = images;
                imageInfo = source != null ? source.FirstOrDefault<ImageExtractor.ImageInfo>((Func<ImageExtractor.ImageInfo, bool>) (x => x.Name == contentId)) : (ImageExtractor.ImageInfo) null;
                if (imageInfo == null)
                {
                  UploadFileRevision uploadFileRevision = (UploadFileRevision) PXSelectBase<UploadFileRevision, PXSelectJoin<UploadFileRevision, InnerJoin<UploadFile, On<UploadFile.fileID, Equal<UploadFileRevision.fileID>, And<UploadFile.lastRevisionID, Equal<UploadFileRevision.fileRevisionID>>>>, Where<UploadFile.name, Equal<Required<UploadFile.name>>>>.Config>.SelectSingleBound(instance, (object[]) null, (object) contentId);
                  if (uploadFileRevision != null && uploadFileRevision.Data != null)
                  {
                    byte[] data = uploadFileRevision.Data;
                    imageInfo = new ImageExtractor.ImageInfo(uploadFileRevision.FileID.Value, data, contentId);
                    images.Add(imageInfo);
                  }
                }
              }
              if (imageInfo != null)
              {
                (string str4, string str5) = getSrcAndTitle(imageInfo);
                parsedOpeningTag.SetAttribute("src", str4);
                if (str5 != null)
                  parsedOpeningTag.SetAttribute("title", str5);
              }
            }
          }
          stringBuilder.Append(parsedOpeningTag.Value);
        }
      }
      else
        stringBuilder.Append(content[index++]);
    }
    newContent = stringBuilder.ToString();
    return images.Count > 0;
  }

  public bool FindImages(string content, List<UploadFile> files, out ICollection<Guid> found)
  {
    if (content == null)
      throw new ArgumentNullException(nameof (content));
    if (files == null)
      throw new ArgumentNullException(nameof (files));
    found = (ICollection<Guid>) new HashSet<Guid>();
    HtmlDocument htmlDocument = new HtmlDocument();
    htmlDocument.LoadHtml(content);
    HtmlNodeCollection source = htmlDocument.DocumentNode.SelectNodes("//img");
    if (source == null)
      return false;
    foreach (string str in ((IEnumerable<HtmlNode>) source).Select<HtmlNode, string>((Func<HtmlNode, string>) (n => n.GetAttributeValue("src", (string) null))).Where<string>((Func<string, bool>) (n => n != null)))
    {
      string src = str;
      UploadFile uploadFile;
      if (src.StartsWith("~/Frames/GetFile.ashx?fileID="))
      {
        string fileIdStr = HttpUtility.UrlDecode(src.Substring("~/Frames/GetFile.ashx?fileID=".Length));
        uploadFile = files.FirstOrDefault<UploadFile>((Func<UploadFile, bool>) (f =>
        {
          Guid? fileId = f.FileID;
          ref Guid? local = ref fileId;
          return string.Equals(local.HasValue ? local.GetValueOrDefault().ToString() : (string) null, fileIdStr, StringComparison.InvariantCultureIgnoreCase);
        }));
      }
      else
        uploadFile = files.FirstOrDefault<UploadFile>((Func<UploadFile, bool>) (f => string.Equals(f.Name, src, StringComparison.InvariantCultureIgnoreCase)));
      if (uploadFile != null && uploadFile.FileID.HasValue)
        found.Add(uploadFile.FileID.Value);
    }
    return found.Count > 0;
  }

  public bool ExtractEmbedded(
    string content,
    Func<ImageExtractor.ImageInfo, (string src, string title)> getSrcAndTitle,
    out string newContent,
    out ICollection<ImageExtractor.ImageInfo> images)
  {
    if (content == null)
      throw new ArgumentNullException(nameof (content));
    if (getSrcAndTitle == null)
      throw new ArgumentNullException(nameof (getSrcAndTitle));
    HtmlDocument htmlDocument = new HtmlDocument();
    htmlDocument.LoadHtml(content);
    images = (ICollection<ImageExtractor.ImageInfo>) new List<ImageExtractor.ImageInfo>();
    string str1 = $"//img[starts-with(@src, '{"data:"}')]";
    HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes(str1);
    if (htmlNodeCollection == null)
    {
      newContent = (string) null;
      return false;
    }
    foreach (HtmlNode htmlNode in (IEnumerable<HtmlNode>) htmlNodeCollection)
    {
      ImageExtractor.ImageInfo imageInfo;
      if (this.ParseEmbedded(htmlNode.GetAttributeValue("src", (string) null), htmlNode.GetAttributeValue("alt", (string) null), out imageInfo))
      {
        (string str2, string str3) = getSrcAndTitle(imageInfo);
        images.Add(imageInfo);
        htmlNode.SetAttributeValue("src", str2);
        if (!string.IsNullOrEmpty(str3))
          htmlNode.SetAttributeValue("title", str3);
      }
    }
    newContent = htmlDocument.DocumentNode.OuterHtml;
    return images.Count > 0;
  }

  public bool IsEmbeddedSource(string imgSource) => imgSource.StartsWith("data:");

  /// <summary>Parse embedded image info</summary>
  /// <param name="imgSource">text of src tag</param>
  /// <param name="imgAlt">text of alt tag</param>
  /// <param name="imageInfo">info about embedded image</param>
  /// <returns>
  /// true if embedded image extracted from imageSource
  /// false if imageSource is not embedded or without data, or unknown mime type</returns>
  public bool ParseEmbedded(
    string imgSource,
    string imgAlt,
    out ImageExtractor.ImageInfo imageInfo)
  {
    imageInfo = (ImageExtractor.ImageInfo) null;
    if (!this.IsEmbeddedSource(imgSource))
      return false;
    int length = imgSource.IndexOf(",");
    if (length <= 0)
      return false;
    int num1 = imgSource.IndexOf(";");
    if (num1 <= 0)
      num1 = length;
    string mimetype = imgSource.Substring("data:".Length, num1 - "data:".Length);
    if (string.IsNullOrEmpty(mimetype))
      mimetype = "text/plain";
    string name = imgAlt;
    if (string.IsNullOrEmpty(name))
      name = "image";
    int num2 = name.LastIndexOf('.');
    if (num2 < 0 || num2 < name.Length - 5)
    {
      string extension = MimeTypes.GetExtension(mimetype);
      name += extension;
    }
    string s = imgSource.Substring(length + 1);
    byte[] bytes;
    if (imgSource.Substring(0, length).EndsWith(";base64"))
    {
      try
      {
        try
        {
          bytes = Convert.FromBase64String(s);
        }
        catch (FormatException ex)
        {
          bytes = Convert.FromBase64String(WebUtility.HtmlDecode(s));
        }
      }
      catch (FormatException ex)
      {
        throw new ImageExtractor.Base64FormatException((Exception) ex);
      }
    }
    else
      bytes = Encoding.ASCII.GetBytes(s);
    imageInfo = new ImageExtractor.ImageInfo(Guid.NewGuid(), bytes, name);
    return true;
  }

  public sealed class ImageInfo
  {
    public ImageInfo(Guid id, byte[] bytes, string name = null, string cid = null)
    {
      this.ID = id;
      this.CID = cid ?? id.ToString();
      this.Bytes = bytes;
      this.Name = name;
    }

    public Guid ID { get; }

    public string CID { get; }

    public byte[] Bytes { get; }

    public string Name { get; }
  }

  public class Base64FormatException : FormatException
  {
    internal Base64FormatException(Exception e)
      : base(PXMessages.LocalizeNoPrefix("An HTML element with the \"img\" tag cannot be parsed because it has the \"src\" attribute with invalid base64 content."), e)
    {
    }

    internal Base64FormatException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}

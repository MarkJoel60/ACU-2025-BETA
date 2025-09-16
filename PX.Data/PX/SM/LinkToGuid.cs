// Decompiled with JetBrains decompiler
// Type: PX.SM.LinkToGuid
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.Wiki.Parser;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.SM;

/// <exclude />
internal class LinkToGuid
{
  public readonly string Result;
  public readonly List<UploadFile> Files;
  public readonly List<WikiFileInPage> FileLinks;
  public readonly List<WikiPageLink> PageLinks;
  private readonly WikiRevision revision;
  private readonly PXSelectBase<UploadFile> qFiles;
  private readonly PXSelectBase<WikiPageSimple> qPages;
  private readonly Guid? wikiID;
  private static readonly Regex regex = new Regex("\\[(?<FileLink>image:\\s*|{up})?((?<FileID>\\d+):)?(?<Name>.+?)?(?<End>(:\\d+)?(\\]|\\|))", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

  private LinkToGuid(PXGraph graph, Guid? wikiID)
  {
    this.wikiID = wikiID;
    this.Files = new List<UploadFile>();
    this.FileLinks = new List<WikiFileInPage>();
    this.PageLinks = new List<WikiPageLink>();
    this.qFiles = (PXSelectBase<UploadFile>) new PXSelect<UploadFile, Where<UploadFile.fileID, Equal<Required<UploadFile.fileID>>, Or<UploadFile.name, Equal<Required<UploadFile.name>>>>>(graph);
    this.qPages = (PXSelectBase<WikiPageSimple>) new PXSelectJoin<WikiPageSimple, LeftJoin<WikiDescriptor, On<WikiDescriptor.pageID, Equal<WikiPageSimple.wikiID>>>, Where<WikiPageSimple.pageID, Equal<Required<WikiPageSimple.pageID>>, Or<Where<WikiPageSimple.name, Equal<Required<WikiPageSimple.name>>, And<Where<WikiPageSimple.wikiID, Equal<Required<WikiPageSimple.wikiID>>, Or<WikiDescriptor.name, Equal<Required<WikiDescriptor.name>>>>>>>>>(graph);
  }

  public LinkToGuid(PXGraph graph, string content, Guid? wikiID)
    : this(graph, wikiID)
  {
    this.Result = this.ApplyReplacement(content);
  }

  public LinkToGuid(PXGraph graph, WikiRevision revision, Guid? wikiID)
    : this(graph, wikiID)
  {
    this.revision = revision;
    this.Result = this.revision != null ? this.ApplyReplacement(this.revision.Content) : (string) null;
  }

  private string ApplyReplacement(string content)
  {
    if (string.IsNullOrEmpty(content))
      return content;
    List<string> pre1 = new List<string>();
    List<string> pre2 = new List<string>();
    List<string> pre3 = new List<string>();
    List<string> pre4 = new List<string>();
    List<string> pre5 = new List<string>();
    content = this.CutPre(content, "{{{{", "}}}}", 0, pre1);
    content = this.CutPre(content, "<pre>", "</pre>", 1, pre2);
    content = this.CutPre(content, "<source lang=\"csharp\">", "</source>", 2, pre3);
    content = this.CutPre(content, "<nowiki>", "</nowiki>", 3, pre4);
    content = this.CutPre(content, "{{", "}}", 4, pre5);
    content = LinkToGuid.regex.Replace(content, new MatchEvaluator(this.DoReplace));
    content = this.PastePre(content, 0, pre1);
    content = this.PastePre(content, 1, pre2);
    content = this.PastePre(content, 2, pre3);
    content = this.PastePre(content, 3, pre4);
    return this.PastePre(content, 4, pre5);
  }

  private string CutPre(
    string content,
    string startTag,
    string endTag,
    int num,
    List<string> pre)
  {
    string str = '\u001E'.ToString() + num.ToString();
    Regex regex = new Regex($"{startTag}[^{str}]+?{endTag}", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant);
    for (Match match = regex.Match(content); match.Success; match = regex.Match(content))
    {
      content = match.Index + match.Length >= content.Length ? content.Substring(0, match.Index) + str : content.Substring(0, match.Index) + str + content.Substring(match.Index + match.Length);
      pre.Add(match.Value);
    }
    return content;
  }

  private string PastePre(string content, int num, List<string> pre)
  {
    string str1 = '\u001E'.ToString() + num.ToString();
    foreach (string str2 in pre)
    {
      int length = content.IndexOf(str1);
      content = length + str1.Length >= content.Length ? content.Substring(0, length) + str2 : content.Substring(0, length) + str2 + content.Substring(length + str1.Length);
    }
    return content;
  }

  private string DoReplace(Match match)
  {
    string str1 = PXBlockParser.DecodeSpecialCharsSimple(match.Groups["Name"].Value);
    object guid = (object) GUID.CreateGuid(str1);
    UploadFile uploadFile1 = (UploadFile) null;
    if (guid == null)
    {
      uploadFile1 = (UploadFile) this.qFiles.Select(null, (object) str1);
      if (uploadFile1 != null && uploadFile1.FileID.HasValue)
        guid = (object) uploadFile1.FileID.Value;
    }
    if (str1 != null)
    {
      if (string.IsNullOrEmpty(match.Groups["FileLink"].Value))
      {
        int length = str1.IndexOf("\\");
        string str2 = length > 0 ? str1.Substring(length + 1) : str1;
        string str3 = length > 0 ? str1.Substring(0, length) : string.Empty;
        WikiPage wikiPage = (WikiPage) (WikiPageSimple) this.qPages.Select(guid, (object) str2, (object) this.wikiID, (object) str3);
        if (wikiPage != null)
          return $"[{wikiPage.PageID}{match.Groups["End"].Value}";
      }
      else
      {
        UploadFile uploadFile2 = uploadFile1;
        if (uploadFile2 == null)
          uploadFile2 = (UploadFile) this.qFiles.Select(guid, (object) str1);
        UploadFile uploadFile3 = uploadFile2;
        if (uploadFile3 == null)
        {
          uploadFile3 = new UploadFile();
          uploadFile3.Name = str1;
        }
        this.Files.Add(uploadFile3);
        if (uploadFile3.FileID.HasValue && this.revision != null)
        {
          WikiFileInPage wikiFileInPage = new WikiFileInPage();
          wikiFileInPage.PageID = this.revision.PageID;
          wikiFileInPage.Language = this.revision.Language;
          wikiFileInPage.PageRevisionID = this.revision.PageRevisionID;
          wikiFileInPage.FileID = uploadFile3.FileID;
          this.FileLinks.Add(wikiFileInPage);
          return $"[{match.Groups["FileLink"].Value}{wikiFileInPage.FileID}{match.Groups["End"].Value}";
        }
      }
    }
    return match.Value;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.SM.GuidToLink
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.Wiki.Parser;
using System;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.SM;

/// <exclude />
internal class GuidToLink
{
  public readonly string Result;
  private readonly PXSelectBase<UploadFile> qFiles;
  private readonly PXSelectBase<WikiPageSimple> qPages;
  private readonly Guid? wikiID;
  public const string RegexText = "\\[(?<FileLink>image:\\s*|{up})?(?<Name>[0-9a-fA-F-]+?)(?<End>(:\\d+)?(\\]|\\|))";
  private static readonly Regex regex = new Regex("\\[(?<FileLink>image:\\s*|{up})?(?<Name>[0-9a-fA-F-]+?)(?<End>(:\\d+)?(\\]|\\|))", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

  public GuidToLink(PXGraph graph, string content, Guid? wikiID)
  {
    this.wikiID = wikiID;
    this.qFiles = (PXSelectBase<UploadFile>) new PXSelect<UploadFile, Where<UploadFile.fileID, Equal<Required<UploadFile.fileID>>>>(graph);
    this.qPages = (PXSelectBase<WikiPageSimple>) new PXSelectJoin<WikiPageSimple, InnerJoin<WikiDescriptor, On<WikiDescriptor.pageID, Equal<WikiPageSimple.wikiID>>>, Where<WikiPageSimple.pageID, Equal<Required<WikiPageSimple.pageID>>>>(graph);
    this.Result = content != null ? GuidToLink.regex.Replace(content, new MatchEvaluator(this.DoReplace)) : (string) null;
  }

  private string DoReplace(Match match)
  {
    try
    {
      Guid? guid = GUID.CreateGuid(match.Groups["Name"].Value);
      string str = (string) null;
      if (string.IsNullOrEmpty(match.Groups["FileLink"].Value))
      {
        PXResult<WikiPageSimple, WikiDescriptor> pxResult = (PXResult<WikiPageSimple, WikiDescriptor>) (PXResult<WikiPageSimple>) this.qPages.Select((object) guid);
        if (pxResult != null)
        {
          WikiPageSimple wikiPageSimple = (WikiPageSimple) pxResult;
          WikiDescriptor wikiDescriptor = (WikiDescriptor) pxResult;
          Guid? wikiId1 = wikiPageSimple.WikiID;
          Guid? wikiId2 = this.wikiID;
          str = (wikiId1.HasValue == wikiId2.HasValue ? (wikiId1.HasValue ? (wikiId1.GetValueOrDefault() == wikiId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 ? wikiPageSimple.Name : $"{wikiDescriptor.Name}\\{wikiPageSimple.Name}";
        }
      }
      else
      {
        UploadFile uploadFile = (UploadFile) this.qFiles.Select((object) guid);
        if (uploadFile != null)
          str = PXBlockParser.EncodeSpecialChars(uploadFile.Name);
      }
      return str != null ? $"[{match.Groups["FileLink"].Value}{str}{match.Groups["End"].Value}" : match.Value;
    }
    catch (Exception ex)
    {
      return match.Value;
    }
  }
}

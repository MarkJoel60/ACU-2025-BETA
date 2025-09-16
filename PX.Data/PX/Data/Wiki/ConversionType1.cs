// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.ConversionType1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;
using PX.SM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki;

/// <exclude />
public class ConversionType1 : PXGraph<ConversionType1, WikiPage>, IWikiExport
{
  public PXSelectJoin<WikiPage, LeftJoin<WikiPageLanguage, On<WikiPageLanguage.language, Equal<Required<WikiPageLanguage.language>>, And<WikiPageLanguage.pageID, Equal<WikiPage.pageID>>>, LeftJoin<WikiRevision, On<WikiRevision.language, Equal<WikiPageLanguage.language>, And<WikiRevision.pageID, Equal<WikiPage.pageID>, And<WikiRevision.pageRevisionID, Equal<WikiPageLanguage.lastRevisionID>>>>, LeftJoin<WikiFileInPage, On<WikiFileInPage.pageID, Equal<WikiPage.pageID>, And<WikiFileInPage.pageRevisionID, Equal<WikiRevision.pageRevisionID>>>, LeftJoin<UploadFile, On<UploadFile.fileID, Equal<WikiFileInPage.fileID>>, LeftJoin<UploadFileRevision, On<UploadFileRevision.fileID, Equal<WikiFileInPage.fileID>, And<UploadFileRevision.fileRevisionID, Equal<UploadFile.lastRevisionID>>>>>>>>, Where<WikiPage.pageID, Equal<Required<WikiPage.pageID>>>, OrderBy<Asc<WikiPage.number>>> UploadFileWithRevisionAndFiles;
  public PXSelectJoin<WikiPage, InnerJoin<WikiPageLanguage, On<WikiPageLanguage.language, Equal<Required<WikiPageLanguage.language>>, And<WikiPageLanguage.pageID, Equal<WikiPage.pageID>>>, InnerJoin<WikiRevision, On<WikiRevision.language, Equal<WikiPageLanguage.language>, And<WikiRevision.pageID, Equal<WikiPage.pageID>, And<WikiRevision.pageRevisionID, Equal<WikiPageLanguage.lastRevisionID>>>>, LeftJoin<WikiFileInPage, On<WikiFileInPage.pageID, Equal<WikiPage.pageID>, And<WikiFileInPage.pageRevisionID, Equal<WikiRevision.pageRevisionID>>>, LeftJoin<UploadFile, On<UploadFile.fileID, Equal<WikiFileInPage.fileID>>, LeftJoin<UploadFileRevision, On<UploadFileRevision.fileID, Equal<UploadFile.fileID>, And<UploadFileRevision.fileRevisionID, Equal<UploadFile.lastRevisionID>>>>>>>>, Where<WikiPage.parentUID, Equal<Required<WikiPage.pageID>>>, OrderBy<Asc<WikiPage.number>>> UploadFilesWithRevisionAndFiles;
  public PXSelectJoin<WikiPage, LeftJoin<WikiPageLanguage, On<WikiPageLanguage.language, Equal<Required<WikiPageLanguage.language>>, And<WikiPageLanguage.pageID, Equal<WikiPage.pageID>>>, LeftJoin<WikiRevision, On<WikiRevision.language, Equal<WikiPageLanguage.language>, And<WikiRevision.pageID, Equal<WikiPage.pageID>, And<WikiRevision.pageRevisionID, Equal<WikiPageLanguage.lastPublishedID>>>>, LeftJoin<WikiFileInPage, On<WikiFileInPage.pageID, Equal<WikiPage.pageID>, And<WikiFileInPage.pageRevisionID, Equal<WikiRevision.pageRevisionID>>>, LeftJoin<UploadFile, On<UploadFile.fileID, Equal<WikiFileInPage.fileID>>, LeftJoin<UploadFileRevision, On<UploadFileRevision.fileID, Equal<WikiFileInPage.fileID>, And<UploadFileRevision.fileRevisionID, Equal<UploadFile.lastRevisionID>>>>>>>>, Where<WikiPage.pageID, Equal<Required<WikiPage.pageID>>>, OrderBy<Asc<WikiPage.number>>> UploadFileWithRevisionAndFilesLastPublished;
  public PXSelectJoin<WikiPage, InnerJoin<WikiPageLanguage, On<WikiPageLanguage.language, Equal<Required<WikiPageLanguage.language>>, And<WikiPageLanguage.pageID, Equal<WikiPage.pageID>>>, InnerJoin<WikiRevision, On<WikiRevision.language, Equal<WikiPageLanguage.language>, And<WikiRevision.pageID, Equal<WikiPage.pageID>, And<WikiRevision.pageRevisionID, Equal<WikiPageLanguage.lastPublishedID>>>>, LeftJoin<WikiFileInPage, On<WikiFileInPage.pageID, Equal<WikiPage.pageID>, And<WikiFileInPage.pageRevisionID, Equal<WikiRevision.pageRevisionID>>>, LeftJoin<UploadFile, On<UploadFile.fileID, Equal<WikiFileInPage.fileID>>, LeftJoin<UploadFileRevision, On<UploadFileRevision.fileID, Equal<UploadFile.fileID>, And<UploadFileRevision.fileRevisionID, Equal<UploadFile.lastRevisionID>>>>>>>>, Where<WikiPage.parentUID, Equal<Required<WikiPage.pageID>>>, OrderBy<Asc<WikiPage.number>>> UploadFilesWithRevisionAndFilesLastPublished;

  IEnumerable<KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>> IWikiExport.GetFileWithFiles(
    Guid guid,
    string language,
    string type)
  {
    List<Package.MyFileInfo> myFileInfoList = new List<Package.MyFileInfo>();
    CutWikiPage key = new CutWikiPage();
    List<KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>> fileWithFiles = new List<KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>>();
    if (type == "last")
    {
      foreach (PXResult<WikiPage, WikiPageLanguage, WikiRevision, WikiFileInPage, UploadFile, UploadFileRevision> pxResult in this.UploadFileWithRevisionAndFiles.Select((object) language, (object) guid))
      {
        WikiPage wikiPage = (WikiPage) pxResult[typeof (WikiPage)];
        WikiPageLanguage wikiPageLanguage = (WikiPageLanguage) pxResult[typeof (WikiPageLanguage)];
        WikiRevision wikiRevision = (WikiRevision) pxResult[typeof (WikiRevision)];
        UploadFile uploadFile = (UploadFile) pxResult[typeof (UploadFile)];
        WikiFileInPage wikiFileInPage = (WikiFileInPage) pxResult[typeof (WikiFileInPage)];
        UploadFileRevision uploadFileRevision = (UploadFileRevision) pxResult[typeof (UploadFileRevision)];
        CutWikiPage cutWikiPage1 = key;
        Guid? nullable = wikiRevision.PageID;
        int num = !nullable.HasValue ? 1 : 0;
        cutWikiPage1.IsWiki = num != 0;
        key.Name = wikiPage.Name;
        CutWikiPage cutWikiPage2 = key;
        nullable = wikiPage.PageID;
        Guid guid1 = nullable.Value;
        cutWikiPage2.PageID = guid1;
        if (!key.IsWiki)
        {
          key.Title = wikiPageLanguage.Title;
          key.DataTime = wikiRevision.CreatedDateTime.Value;
          key.Content = wikiRevision.Content;
        }
        else
          key.Title = wikiPage.Name;
        if (uploadFile.Name != null)
        {
          nullable = uploadFileRevision.FileID;
          if (nullable.HasValue && uploadFileRevision.Data != null)
          {
            uploadFile.Name = uploadFile.Name.Replace("/", "");
            byte[] data = uploadFileRevision.Data;
            string name = uploadFile.Name;
            nullable = uploadFileRevision.FileID;
            Guid guid2 = nullable.Value;
            Package.MyFileInfo myFileInfo = new Package.MyFileInfo(data, name, guid2);
            myFileInfoList.Add(myFileInfo);
          }
        }
      }
      KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>> keyValuePair = new KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>(key, (IEnumerable<Package.MyFileInfo>) myFileInfoList);
      fileWithFiles.Add(keyValuePair);
    }
    else
    {
      foreach (PXResult<WikiPage, WikiPageLanguage, WikiRevision, WikiFileInPage, UploadFile, UploadFileRevision> pxResult in this.UploadFileWithRevisionAndFilesLastPublished.Select((object) language, (object) guid))
      {
        WikiPage wikiPage = (WikiPage) pxResult[typeof (WikiPage)];
        WikiPageLanguage wikiPageLanguage = (WikiPageLanguage) pxResult[typeof (WikiPageLanguage)];
        WikiRevision wikiRevision = (WikiRevision) pxResult[typeof (WikiRevision)];
        UploadFile uploadFile = (UploadFile) pxResult[typeof (UploadFile)];
        WikiFileInPage wikiFileInPage = (WikiFileInPage) pxResult[typeof (WikiFileInPage)];
        UploadFileRevision uploadFileRevision = (UploadFileRevision) pxResult[typeof (UploadFileRevision)];
        CutWikiPage cutWikiPage3 = key;
        Guid? nullable = wikiRevision.PageID;
        int num = !nullable.HasValue ? 1 : 0;
        cutWikiPage3.IsWiki = num != 0;
        key.Name = wikiPage.Name;
        CutWikiPage cutWikiPage4 = key;
        nullable = wikiPage.PageID;
        Guid guid3 = nullable.Value;
        cutWikiPage4.PageID = guid3;
        if (!key.IsWiki)
        {
          key.Title = wikiPageLanguage.Title;
          key.DataTime = wikiRevision.CreatedDateTime.Value;
          key.Content = wikiRevision.Content;
        }
        else
          key.Title = wikiPage.Name;
        if (uploadFile.Name != null)
        {
          nullable = uploadFileRevision.FileID;
          if (nullable.HasValue && uploadFileRevision.Data != null)
          {
            uploadFile.Name = uploadFile.Name.Replace("/", "");
            byte[] data = uploadFileRevision.Data;
            string name = uploadFile.Name;
            nullable = uploadFileRevision.FileID;
            Guid guid4 = nullable.Value;
            Package.MyFileInfo myFileInfo = new Package.MyFileInfo(data, name, guid4);
            myFileInfoList.Add(myFileInfo);
          }
        }
      }
      KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>> keyValuePair = new KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>(key, (IEnumerable<Package.MyFileInfo>) myFileInfoList);
      fileWithFiles.Add(keyValuePair);
    }
    return (IEnumerable<KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>>) fileWithFiles;
  }

  IEnumerable<KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>> IWikiExport.GetChildFilesWithFiles(
    Guid guid,
    string language,
    string type)
  {
    Dictionary<Guid, List<Package.MyFileInfo>> dictionary1 = new Dictionary<Guid, List<Package.MyFileInfo>>();
    Dictionary<Guid, CutWikiPage> dictionary2 = new Dictionary<Guid, CutWikiPage>();
    CutWikiPage cutWikiPage1 = new CutWikiPage();
    List<KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>> childFilesWithFiles = new List<KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>>();
    Package.MyFileInfo myFileInfo1 = new Package.MyFileInfo();
    if (type == "last")
    {
      foreach (PXResult<WikiPage, WikiPageLanguage, WikiRevision, WikiFileInPage, UploadFile, UploadFileRevision> pxResult in this.UploadFilesWithRevisionAndFiles.Select((object) language, (object) guid))
      {
        WikiPage wikiPage = (WikiPage) pxResult[typeof (WikiPage)];
        WikiPageLanguage wikiPageLanguage = (WikiPageLanguage) pxResult[typeof (WikiPageLanguage)];
        WikiRevision wikiRevision = (WikiRevision) pxResult[typeof (WikiRevision)];
        WikiFileInPage wikiFileInPage = (WikiFileInPage) pxResult[typeof (WikiFileInPage)];
        UploadFile uploadFile = (UploadFile) pxResult[typeof (UploadFile)];
        UploadFileRevision uploadFileRevision = (UploadFileRevision) pxResult[typeof (UploadFileRevision)];
        CutWikiPage cutWikiPage2 = new CutWikiPage();
        cutWikiPage2.IsWiki = false;
        cutWikiPage2.Title = wikiPageLanguage.Title;
        cutWikiPage2.Name = wikiPage.Name;
        cutWikiPage2.DataTime = wikiRevision.CreatedDateTime.Value;
        CutWikiPage cutWikiPage3 = cutWikiPage2;
        Guid? nullable = wikiPage.PageID;
        Guid guid1 = nullable.Value;
        cutWikiPage3.PageID = guid1;
        cutWikiPage2.Content = wikiRevision.Content;
        if (uploadFile.Name != null)
        {
          nullable = uploadFileRevision.FileID;
          if (nullable.HasValue && uploadFileRevision.Data != null)
          {
            uploadFile.Name = uploadFile.Name.Replace("/", "");
            myFileInfo1 = new Package.MyFileInfo();
            myFileInfo1.Owndata = uploadFileRevision.Data;
            Package.MyFileInfo myFileInfo2 = myFileInfo1;
            nullable = uploadFileRevision.FileID;
            Guid guid2 = nullable.Value;
            myFileInfo2.Guid = guid2;
            myFileInfo1.FullName = uploadFile.Name;
          }
        }
        Dictionary<Guid, List<Package.MyFileInfo>> dictionary3 = dictionary1;
        nullable = wikiPage.PageID;
        Guid key1 = nullable.Value;
        if (!dictionary3.ContainsKey(key1))
        {
          List<Package.MyFileInfo> myFileInfoList1 = new List<Package.MyFileInfo>();
          Dictionary<Guid, List<Package.MyFileInfo>> dictionary4 = dictionary1;
          nullable = wikiPage.PageID;
          Guid key2 = nullable.Value;
          List<Package.MyFileInfo> myFileInfoList2 = myFileInfoList1;
          dictionary4.Add(key2, myFileInfoList2);
          Dictionary<Guid, CutWikiPage> dictionary5 = dictionary2;
          nullable = wikiPage.PageID;
          Guid key3 = nullable.Value;
          CutWikiPage cutWikiPage4 = cutWikiPage2;
          dictionary5.Add(key3, cutWikiPage4);
        }
        Dictionary<Guid, List<Package.MyFileInfo>> dictionary6 = dictionary1;
        nullable = wikiPage.PageID;
        Guid key4 = nullable.Value;
        dictionary6[key4].Add(myFileInfo1);
      }
      foreach (KeyValuePair<Guid, List<Package.MyFileInfo>> keyValuePair1 in dictionary1)
      {
        KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>> keyValuePair2 = new KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>(dictionary2[keyValuePair1.Key], (IEnumerable<Package.MyFileInfo>) keyValuePair1.Value);
        childFilesWithFiles.Add(keyValuePair2);
      }
      if (childFilesWithFiles.Count == 0)
        return (IEnumerable<KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>>) null;
    }
    else
    {
      foreach (PXResult<WikiPage, WikiPageLanguage, WikiRevision, WikiFileInPage, UploadFile, UploadFileRevision> pxResult in this.UploadFilesWithRevisionAndFilesLastPublished.Select((object) language, (object) guid))
      {
        WikiPage wikiPage = (WikiPage) pxResult[typeof (WikiPage)];
        WikiPageLanguage wikiPageLanguage = (WikiPageLanguage) pxResult[typeof (WikiPageLanguage)];
        WikiRevision wikiRevision = (WikiRevision) pxResult[typeof (WikiRevision)];
        WikiFileInPage wikiFileInPage = (WikiFileInPage) pxResult[typeof (WikiFileInPage)];
        UploadFile uploadFile = (UploadFile) pxResult[typeof (UploadFile)];
        UploadFileRevision uploadFileRevision = (UploadFileRevision) pxResult[typeof (UploadFileRevision)];
        CutWikiPage cutWikiPage5 = new CutWikiPage();
        cutWikiPage5.IsWiki = false;
        cutWikiPage5.Title = wikiPageLanguage.Title;
        cutWikiPage5.Name = wikiPage.Name;
        cutWikiPage5.DataTime = wikiRevision.CreatedDateTime.Value;
        CutWikiPage cutWikiPage6 = cutWikiPage5;
        Guid? nullable = wikiPage.PageID;
        Guid guid3 = nullable.Value;
        cutWikiPage6.PageID = guid3;
        cutWikiPage5.Content = wikiRevision.Content;
        if (uploadFile.Name != null)
        {
          nullable = uploadFileRevision.FileID;
          if (nullable.HasValue && uploadFileRevision.Data != null)
          {
            uploadFile.Name = uploadFile.Name.Replace("/", "");
            myFileInfo1 = new Package.MyFileInfo();
            myFileInfo1.Owndata = uploadFileRevision.Data;
            Package.MyFileInfo myFileInfo3 = myFileInfo1;
            nullable = uploadFileRevision.FileID;
            Guid guid4 = nullable.Value;
            myFileInfo3.Guid = guid4;
            myFileInfo1.FullName = uploadFile.Name;
          }
        }
        Dictionary<Guid, List<Package.MyFileInfo>> dictionary7 = dictionary1;
        nullable = wikiPage.PageID;
        Guid key5 = nullable.Value;
        if (!dictionary7.ContainsKey(key5))
        {
          List<Package.MyFileInfo> myFileInfoList3 = new List<Package.MyFileInfo>();
          Dictionary<Guid, List<Package.MyFileInfo>> dictionary8 = dictionary1;
          nullable = wikiPage.PageID;
          Guid key6 = nullable.Value;
          List<Package.MyFileInfo> myFileInfoList4 = myFileInfoList3;
          dictionary8.Add(key6, myFileInfoList4);
          Dictionary<Guid, CutWikiPage> dictionary9 = dictionary2;
          nullable = wikiPage.PageID;
          Guid key7 = nullable.Value;
          CutWikiPage cutWikiPage7 = cutWikiPage5;
          dictionary9.Add(key7, cutWikiPage7);
        }
        Dictionary<Guid, List<Package.MyFileInfo>> dictionary10 = dictionary1;
        nullable = wikiPage.PageID;
        Guid key8 = nullable.Value;
        dictionary10[key8].Add(myFileInfo1);
      }
      foreach (KeyValuePair<Guid, List<Package.MyFileInfo>> keyValuePair3 in dictionary1)
      {
        KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>> keyValuePair4 = new KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>(dictionary2[keyValuePair3.Key], (IEnumerable<Package.MyFileInfo>) keyValuePair3.Value);
        childFilesWithFiles.Add(keyValuePair4);
      }
      if (childFilesWithFiles.Count == 0)
        return (IEnumerable<KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>>) null;
    }
    return (IEnumerable<KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>>) childFilesWithFiles;
  }
}

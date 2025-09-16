// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ImageExtractorEmailProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.SM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP;

public class ImageExtractorEmailProcessor : BasicEmailProcessor
{
  protected override bool Process(BasicEmailProcessor.Package package)
  {
    EMailAccount account = package.Account;
    CRSMEmail message = package.Message;
    PXGraph graph = package.Graph;
    if (message.Body == null)
      return true;
    if (!message.NoteID.HasValue)
      return false;
    Guid noteID = message.NoteID.Value;
    string str;
    ICollection<ImageExtractor.ImageInfo> imageInfos;
    if (new ImageExtractor().ExtractEmbedded(message.Body, (Func<ImageExtractor.ImageInfo, (string, string)>) (img => this.AddExtractedImage(graph, noteID, img)), ref str, ref imageInfos))
      message.Body = str;
    return true;
  }

  private (string src, string title) AddExtractedImage(
    PXGraph graph,
    Guid noteId,
    ImageExtractor.ImageInfo img)
  {
    this.CreateFile(graph, noteId, img.ID, img.Name, img.Bytes);
    return ($"~/Frames/GetFile.ashx?fileID={img.CID}", img.Name);
  }

  private void CreateFile(
    PXGraph graph,
    Guid noteId,
    Guid newFileId,
    string name,
    byte[] content)
  {
    PXCache cach1 = graph.Caches[typeof (NoteDoc)];
    NoteDoc instance1 = (NoteDoc) cach1.CreateInstance();
    instance1.NoteID = new Guid?(noteId);
    instance1.FileID = new Guid?(newFileId);
    cach1.Insert((object) instance1);
    GraphHelper.EnsureCachePersistence(graph, typeof (NoteDoc));
    PXCache cach2 = graph.Caches[typeof (CommonMailReceiveProvider.UploadFile)];
    CommonMailReceiveProvider.UploadFile instance2 = (CommonMailReceiveProvider.UploadFile) cach2.CreateInstance();
    instance2.FileID = new Guid?(newFileId);
    instance2.LastRevisionID = new int?(1);
    instance2.Versioned = new bool?(true);
    instance2.IsPublic = new bool?(false);
    if (name.Length > 200)
      name = name.Substring(0, 200);
    instance2.Name = $"{newFileId.ToString()}\\{name}";
    instance2.PrimaryScreenID = "CR306015";
    cach2.Insert((object) instance2);
    GraphHelper.EnsureCachePersistence(graph, typeof (CommonMailReceiveProvider.UploadFile));
    PXCache cach3 = graph.Caches[typeof (UploadFileRevision)];
    UploadFileRevision instance3 = (UploadFileRevision) cach3.CreateInstance();
    instance3.FileID = new Guid?(newFileId);
    instance3.FileRevisionID = new int?(1);
    instance3.Data = content;
    instance3.Size = new int?(UploadFileHelper.BytesToKilobytes(content.Length));
    cach3.Insert((object) instance3);
    GraphHelper.EnsureCachePersistence(graph, typeof (UploadFileRevision));
  }
}

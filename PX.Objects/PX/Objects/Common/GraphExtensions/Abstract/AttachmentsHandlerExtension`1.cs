// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.AttachmentsHandlerExtension`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract;

[PXInternalUseOnly]
public class AttachmentsHandlerExtension<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph, new()
{
  protected string PrimaryScreenID { get; private set; }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.PrimaryScreenID = PXSiteMapProviderExtensions.FindSiteMapNodeByGraphType(PXSiteMap.Provider, CustomizedTypeManager.GetTypeNotCustomized(typeof (TGraph).FullName))?.ScreenID ?? "CR306015";
    PXCache cach = this.Base.Caches[typeof (UploadFileRevision)];
  }

  [PXDBString(InputMask = "", IsUnicode = true)]
  public virtual void _(Events.CacheAttached<UploadFile.name> e)
  {
  }

  internal void InsertFile(FileDto file) => this.InsertFile(file, out Action _);

  private protected void InsertFile(FileDto file, out Action revertCallback)
  {
    if (file == null)
      throw new ArgumentNullException(nameof (file));
    GraphHelper.EnsureCachePersistence((PXGraph) this.Base, typeof (UploadFile));
    PXCache uploadFileCache = this.Base.Caches[typeof (UploadFile)];
    UploadFile uploadFile = (UploadFile) uploadFileCache.CreateInstance();
    uploadFile.FileID = new Guid?(file.FileId);
    uploadFile.LastRevisionID = new int?(1);
    uploadFile.Versioned = new bool?(true);
    uploadFile.IsPublic = new bool?(false);
    uploadFile.Name = file.FullName;
    uploadFile.PrimaryScreenID = this.PrimaryScreenID;
    uploadFileCache.Insert((object) uploadFile);
    GraphHelper.EnsureCachePersistence((PXGraph) this.Base, typeof (UploadFileRevision));
    PXCache fileRevisionCache = this.Base.Caches[typeof (UploadFileRevision)];
    UploadFileRevision fileRevision = (UploadFileRevision) fileRevisionCache.CreateInstance();
    fileRevision.FileID = new Guid?(file.FileId);
    fileRevision.FileRevisionID = new int?(1);
    fileRevision.Data = file.Content;
    fileRevision.Size = new int?(UploadFileHelper.BytesToKilobytes(file.Content.Length));
    fileRevisionCache.Insert((object) fileRevision);
    GraphHelper.EnsureCachePersistence((PXGraph) this.Base, typeof (NoteDoc));
    PXCache noteDocCache = this.Base.Caches[typeof (NoteDoc)];
    NoteDoc noteDoc = (NoteDoc) noteDocCache.CreateInstance();
    noteDoc.NoteID = new Guid?(file.EntityNoteId);
    noteDoc.FileID = new Guid?(file.FileId);
    noteDocCache.Insert((object) noteDoc);
    revertCallback = (Action) (() =>
    {
      noteDocCache.SetStatus((object) noteDoc, (PXEntryStatus) 4);
      uploadFileCache.SetStatus((object) uploadFile, (PXEntryStatus) 4);
      fileRevisionCache.SetStatus((object) fileRevision, (PXEntryStatus) 4);
    });
  }
}

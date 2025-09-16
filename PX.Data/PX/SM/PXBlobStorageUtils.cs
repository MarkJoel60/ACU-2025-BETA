// Decompiled with JetBrains decompiler
// Type: PX.SM.PXBlobStorageUtils
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Web;

#nullable disable
namespace PX.SM;

public class PXBlobStorageUtils
{
  public static void OnBeforeEditFile(string fileId)
  {
    if (string.IsNullOrEmpty(fileId) || PXBlobStorage.FileAttachmentProvider == null)
      return;
    if (!((UploadFileRevision) PXSelectBase<UploadFileRevision, PXSelectJoin<UploadFileRevision, InnerJoin<UploadFile, On<UploadFile.fileID, Equal<UploadFileRevision.fileID>, And<UploadFile.lastRevisionID, Equal<UploadFileRevision.fileRevisionID>>>>, Where<UploadFile.fileID, Equal<Optional<UploadFile.fileID>>>>.Config>.Select(new PXGraph(), (object) fileId)).BlobHandler.HasValue)
      return;
    string editUrl = PXBlobStorage.FileAttachmentProvider.GetEditUrl(Guid.Parse(fileId));
    if (string.IsNullOrEmpty(editUrl))
      return;
    HttpContext.Current.Response.Redirect(editUrl, true);
  }

  public static IPXFileAttachmentProvider FileAttachementProvider
  {
    get => PXBlobStorage.FileAttachmentProvider;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.SM.BlobStorageAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

internal class BlobStorageAttribute : PXEventSubscriberAttribute
{
  public override void CacheAttached(PXCache sender)
  {
    sender.Fields.Remove(this.FieldName);
    sender.Graph.OnBeforePersist += (System.Action<PXGraph>) (_param1 =>
    {
      foreach (UploadFileRevision uploadFileRevision in sender.Cached)
      {
        switch (sender.GetStatus((object) uploadFileRevision))
        {
          case PXEntryStatus.Updated:
          case PXEntryStatus.Inserted:
            if (!uploadFileRevision.BlobHandler.HasValue && PXBlobStorage.IsRemoteStorageEnabled(uploadFileRevision.Data))
            {
              if (PXBlobStorage.SaveContext == null)
                PXBlobStorage.SaveContext = new PXBlobStorageContext();
              PXCache cach = sender.Graph.Caches[typeof (UploadFile)];
              UploadFile instance = (UploadFile) cach.CreateInstance();
              instance.FileID = uploadFileRevision.FileID;
              UploadFile uploadFile = (UploadFile) cach.Locate((object) instance);
              PXBlobStorage.SaveContext.FileInfo = uploadFile;
              PXBlobStorage.SaveContext.Revision = uploadFileRevision;
              uploadFileRevision.MoveToStorage();
              continue;
            }
            continue;
          default:
            continue;
        }
      }
      PXBlobStorage.SaveContext = (PXBlobStorageContext) null;
    });
  }
}

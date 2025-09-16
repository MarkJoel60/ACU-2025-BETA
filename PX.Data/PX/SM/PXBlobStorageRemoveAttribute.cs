// Decompiled with JetBrains decompiler
// Type: PX.SM.PXBlobStorageRemoveAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.SM;

internal class PXBlobStorageRemoveAttribute : PXEventSubscriberAttribute, IPXRowPersistedSubscriber
{
  public void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (!PXBlobStorage.AllowSave || e.TranStatus != PXTranStatus.Completed || (e.Operation & PXDBOperation.Delete) != PXDBOperation.Delete || !(e.Row is UploadFileRevision row))
      return;
    Guid? blobHandler = row.BlobHandler;
    if (!blobHandler.HasValue)
      return;
    blobHandler = row.BlobHandler;
    PXBlobStorage.Remove(blobHandler.Value);
  }
}

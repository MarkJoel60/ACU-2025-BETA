// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.PXSyncItemBucket`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.CS.Email;

public class PXSyncItemBucket<T1> where T1 : IBqlTable
{
  public readonly string ID;
  public readonly PXSyncMailbox Mailbox;
  public readonly PXSyncItemStatus Status;
  public readonly EMailSyncReference Reference;
  public UploadFileWithData[] Attachments;
  public readonly T1 Item1;

  public PXSyncItemBucket(
    PXSyncMailbox mailbox,
    PXSyncItemStatus status,
    EMailSyncReference reference,
    T1 item1)
  {
    this.ID = Guid.NewGuid().ToString();
    this.Item1 = item1;
    this.Mailbox = mailbox;
    this.Status = status;
    this.Reference = reference;
  }
}

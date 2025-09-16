// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.PXSyncItemBucket`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;

#nullable disable
namespace PX.Objects.CS.Email;

public class PXSyncItemBucket<T1, T2> : PXSyncItemBucket<T1>
  where T1 : IBqlTable
  where T2 : IBqlTable
{
  public readonly T2 Item2;

  public PXSyncItemBucket(
    PXSyncMailbox mailbox,
    PXSyncItemStatus status,
    EMailSyncReference reference,
    T1 item1,
    T2 item2)
    : base(mailbox, status, reference, item1)
  {
    this.Item2 = item2;
  }
}

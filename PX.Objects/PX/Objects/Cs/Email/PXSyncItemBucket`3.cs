// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.PXSyncItemBucket`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;

#nullable disable
namespace PX.Objects.CS.Email;

public class PXSyncItemBucket<T1, T2, T3> : PXSyncItemBucket<T1, T2>
  where T1 : IBqlTable
  where T2 : IBqlTable
  where T3 : IBqlTable
{
  public readonly T3 Item3;

  public PXSyncItemBucket(
    PXSyncMailbox mailbox,
    PXSyncItemStatus status,
    EMailSyncReference reference,
    T1 item1,
    T2 item2,
    T3 item3)
    : base(mailbox, status, reference, item1, item2)
  {
    this.Item3 = item3;
  }
}

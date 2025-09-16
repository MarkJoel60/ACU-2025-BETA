// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.WebServices.PXExchangeReBase`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update.ExchangeService;

#nullable disable
namespace PX.Data.Update.WebServices;

public class PXExchangeReBase<T> : PXExchangeItem<T> where T : ItemType, new()
{
  public readonly object Tag;
  public readonly string UID;

  public PXExchangeReBase(
    string mailbox,
    T item,
    string uid,
    object tag,
    AttachmentType[] attachments = null)
    : base(mailbox, item, attachments)
  {
    this.Tag = tag;
    this.UID = uid;
  }
}

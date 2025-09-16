// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.WebServices.PXExchangeItem`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update.ExchangeService;

#nullable disable
namespace PX.Data.Update.WebServices;

public class PXExchangeItem<T> : PXExchangeItemBase where T : ItemType, new()
{
  public readonly T Item;
  public readonly AttachmentType[] Attachments;
  private string hash;

  public string Hash
  {
    get
    {
      if (System.DateTime.Now.Ticks > 0L)
        return (string) null;
      if (this.hash == null)
        this.hash = this.GetItemHash<T>();
      return this.hash;
    }
  }

  public PXExchangeItem(string mailbox, T item, AttachmentType[] attachments = null)
    : base(mailbox)
  {
    this.Item = item;
    this.Attachments = attachments;
  }
}

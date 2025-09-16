// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.WebServices.PXExchangeItemID
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update.ExchangeService;

#nullable disable
namespace PX.Data.Update.WebServices;

public class PXExchangeItemID : PXExchangeItemBase
{
  public readonly ItemIdType ItemID;
  public readonly System.DateTime? Date;

  public PXExchangeItemID(string mailbox, ItemIdType itemID, System.DateTime? date = null)
    : base(mailbox)
  {
    this.ItemID = itemID;
    this.Date = date;
  }
}

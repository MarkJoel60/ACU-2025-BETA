// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.WebServices.PXExchangeFolderID
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update.ExchangeService;

#nullable disable
namespace PX.Data.Update.WebServices;

public class PXExchangeFolderID : PXExchangeItemBase
{
  public readonly BaseFolderIdType FolderID;
  public bool NeedUpdate;

  public PXExchangeFolderID(string mailbox, BaseFolderIdType folderid, bool needUpdate = false)
    : base(mailbox)
  {
    this.FolderID = folderid;
    this.NeedUpdate = needUpdate;
  }
}

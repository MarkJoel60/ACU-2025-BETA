// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.WebServices.PXExchangeRequest`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update.ExchangeService;

#nullable disable
namespace PX.Data.Update.WebServices;

public class PXExchangeRequest<T, ReqT> : PXExchangeReBase<T> where T : ItemType, new()
{
  public readonly ReqT Request;
  public readonly PXExchangeFolderID Folder;
  public bool SendRequired;
  public bool SendSeparateRequired;
  public bool DetailsRequired;

  public PXExchangeRequest(
    PXExchangeFolderID folder,
    ReqT item,
    string uid,
    object tag,
    AttachmentType[] attachments = null)
    : base(folder.Address, (object) item as T, uid, tag, attachments)
  {
    this.Request = item;
    this.Folder = folder;
  }
}

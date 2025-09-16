// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.WebServices.PXExchangeResponce`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update.ExchangeService;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Update.WebServices;

public class PXExchangeResponce<T> : PXExchangeReBase<T> where T : ItemType, new()
{
  public readonly string Message;
  public readonly string[] Details;
  public readonly ResponseCodeType Code;

  public bool Success => this.Message == null && (object) this.Item != null;

  public PXExchangeResponce(
    PXExchangeReBase<T> request,
    T item = null,
    ResponseCodeType code = ResponseCodeType.NoError,
    string error = null,
    IEnumerable<string> messages = null)
    : base(request.Address, item ?? request.Item, request.UID, request.Tag)
  {
    this.Code = code;
    this.Message = error;
    this.Details = messages != null ? messages.ToArray<string>() : (string[]) null;
  }
}

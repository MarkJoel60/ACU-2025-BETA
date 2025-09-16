// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.WebServices.IPXExchangeServerProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;

#nullable disable
namespace PX.Data.Update.WebServices;

/// <summary>
/// Provider of the <see cref="T:PX.Data.Update.WebServices.PXExchangeServer" />.
/// </summary>
[PXInternalUseOnly]
public interface IPXExchangeServerProvider
{
  PXExchangeServer GetPXExchangeServer(EMailSyncServer config);
}

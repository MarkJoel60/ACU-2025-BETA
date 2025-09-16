// Decompiled with JetBrains decompiler
// Type: PX.Api.Mobile.PushNotifications.IPushNotificationSender
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Api.Mobile.PushNotifications;

[PXInternalUseOnly]
public interface IPushNotificationSender
{
  Task SendNotificationAsync(
    IEnumerable<Guid> userIds,
    string title,
    string text,
    (string screenId, Guid noteId)? link,
    CancellationToken cancellation,
    string category = null,
    IDictionary<string, string> customData = null,
    int? groupingId = null);

  int CountActiveTokens(IEnumerable<Guid> userIds);
}

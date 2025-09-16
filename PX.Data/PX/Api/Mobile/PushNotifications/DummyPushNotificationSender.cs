// Decompiled with JetBrains decompiler
// Type: PX.Api.Mobile.PushNotifications.DummyPushNotificationSender
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Api.Mobile.PushNotifications;

internal class DummyPushNotificationSender : IPushNotificationSender
{
  public async Task SendNotificationAsync(
    IEnumerable<Guid> userIds,
    string title,
    string text,
    (string screenId, Guid noteId)? link,
    CancellationToken cancellation,
    string category,
    IDictionary<string, string> customData,
    int? groupingId)
  {
    await Task.Run((Func<Task>) (() =>
    {
      throw new NotImplementedException();
    }));
  }

  public int CountActiveTokens(IEnumerable<Guid> userIds) => 0;
}

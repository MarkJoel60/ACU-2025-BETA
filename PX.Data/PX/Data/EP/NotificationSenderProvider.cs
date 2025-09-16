// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.NotificationSenderProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;

#nullable enable
namespace PX.Data.EP;

public static class NotificationSenderProvider
{
  private static readonly INotificationSender _handler = ServiceLocator.IsLocationProviderSet ? ServiceLocator.Current.GetInstance<INotificationSender>() : (INotificationSender) new EmptyNotificationSender();

  public static void Notify(EmailNotificationParameters parameters)
  {
    NotificationSenderProvider._handler.Notify(parameters);
  }
}

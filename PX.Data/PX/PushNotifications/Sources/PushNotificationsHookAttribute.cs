// Decompiled with JetBrains decompiler
// Type: PX.PushNotifications.Sources.PushNotificationsHookAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.PushNotifications.Sources;

[AttributeUsage(AttributeTargets.Class)]
public class PushNotificationsHookAttribute : Attribute
{
  public PushNotificationsHookAttribute(string name, string hookType, string hookId)
  {
    this.Name = name;
    this.HookType = hookType;
    this.HookId = Guid.Parse(hookId);
  }

  public virtual bool IsActive => true;

  public string Name { get; set; }

  public string Address { get; set; }

  public string HookType { get; set; }

  public Guid HookId { get; set; }

  public string HeaderName { get; set; }

  public string HeaderValue { get; set; }
}

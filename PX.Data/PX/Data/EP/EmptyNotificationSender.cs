// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.EmptyNotificationSender
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Data.EP;

/// <exclude />
public sealed class EmptyNotificationSender : INotificationSender
{
  public IEnumerable<IBqlTable> Notify(EmailNotificationParameters parameters)
  {
    throw new InvalidOperationException();
  }

  public IEnumerable<IBqlTable> NotifyAndDeliver(EmailNotificationParameters parameters)
  {
    throw new InvalidOperationException();
  }
}

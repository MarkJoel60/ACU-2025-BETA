// Decompiled with JetBrains decompiler
// Type: PX.Objects.SM.NotificationService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.SM;

public class NotificationService : INotificationService
{
  private PXGraph _graph;

  public INotification Read(string notificationCD)
  {
    PXSelectBase<Notification, PXSelect<Notification, Where<Notification.notificationID, Equal<Required<Notification.notificationID>>>>.Config>.Clear(this.Graph);
    this._graph.Caches[typeof (Notification)].Clear();
    return (INotification) notificationCD.With<string, Notification>((Func<string, Notification>) (_ => PXResultset<Notification>.op_Implicit(PXSelectBase<Notification, PXSelect<Notification, Where<Notification.notificationID, Equal<Required<Notification.notificationID>>>>.Config>.Select(this.Graph, new object[1]
    {
      (object) _
    }))));
  }

  private PXGraph Graph => this._graph ?? (this._graph = new PXGraph());
}

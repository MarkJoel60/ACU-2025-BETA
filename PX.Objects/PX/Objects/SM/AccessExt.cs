// Decompiled with JetBrains decompiler
// Type: PX.Objects.SM.AccessExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Async;
using PX.Common;
using PX.Data;
using PX.Data.EP;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.EP;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace PX.Objects.SM;

public class AccessExt : PXGraphExtension<Access>
{
  [InjectDependency]
  private IMailSendProvider MailSendProvider { get; set; }

  [PXOverride]
  public void SendUserNotification(
    int? accountId,
    Notification notification,
    Action<int?, Notification> del)
  {
    if (!accountId.HasValue || notification == null)
      return;
    Access graph = this.Base.CloneGraphState<Access>();
    ((PXSelectBase<Users>) graph.UserList).Current = ((PXSelectBase<Users>) this.Base.UserList).Current;
    ((ILongOperationManager) ((PXGraph) this.Base).LongOperationManager).StartOperation((PXGraph) this.Base, (Action<CancellationToken>) (ct =>
    {
      TemplateNotificationGenerator notificationGenerator = TemplateNotificationGenerator.Create((PXGraph) graph, (object) ((PXSelectBase<Users>) graph.UserList).Current, notification);
      notificationGenerator.MailAccountId = accountId;
      notificationGenerator.To = ((PXSelectBase<Users>) graph.UserList).Current.Email;
      notificationGenerator.LinkToEntity = true;
      notificationGenerator.Body = notificationGenerator.Body.Replace("((UserList.Password))", ((PXSelectBase<Users>) graph.UserList).Current.Password);
      notificationGenerator.Body = notificationGenerator.Body.Replace("((UserList.RecoveryLink))", ((PXSelectBase<Users>) graph.UserList).Current.RecoveryLink);
      PXContext.SetScreenID("CR306015");
      ct.ThrowIfCancellationRequested();
      IEnumerable<CRSMEmail> crsmemails = notificationGenerator.Send();
      IMailSendProvider mailSendProvider = ((PXGraph) graph).GetExtension<AccessExt>().MailSendProvider;
      foreach (SMEmail smEmail in notificationGenerator.CastToSMEmail(crsmemails))
      {
        ct.ThrowIfCancellationRequested();
        mailSendProvider.SendMessage((object) smEmail);
      }
    }));
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.SM.Email.CheckEmailAccountGraphExt`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Mail;
using PX.Data;
using PX.Data.EP;
using System;
using System.Collections;
using System.Net.Mail;
using System.Threading;

#nullable disable
namespace PX.SM.Email;

public abstract class CheckEmailAccountGraphExt<TGraph, TMain> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TMain : class, IBqlTable, new()
{
  public PXFilter<TestEmailAccountFilter> SendTestEmailFilter;
  public PXAction<TMain> CheckEmailAccount;

  public override void Initialize()
  {
    base.Initialize();
    this.CheckEmailAccount.SetEnabled(false);
  }

  [PXUIField(DisplayName = "Test", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  protected virtual IEnumerable checkEmailAccount(PXAdapter adapter)
  {
    this.DoCheckEmailAccount(this.Base.Caches[typeof (EMailAccount)].Current as EMailAccount, (TestEmailAccountFilter) null);
    return adapter.Get();
  }

  public virtual void DoCheckEmailAccount(EMailAccount account, TestEmailAccountFilter parameters)
  {
    if (parameters == null)
      this.Base.LongOperationManager.StartOperation((System.Action<CancellationToken>) (cancellationToken => this.DoTestEmailAccount(account, cancellationToken)));
    else
      this.Base.LongOperationManager.StartOperation((System.Action<CancellationToken>) (cancellationToken => this.DoSendTestEmail(account, parameters, cancellationToken)));
  }

  public virtual void DoSendTestEmail(
    EMailAccount account,
    TestEmailAccountFilter parameters,
    CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    try
    {
      if (string.IsNullOrWhiteSpace(account.OutcomingHostName))
        return;
      using (MailSender sender = MailAccountManager.GetSender(account, true))
        EMailAccountMaint.WrapException((System.Action) (() => sender.Send(new MailSender.MailMessageT(EMailAccount.PK.Find((PXGraph) this.Base, parameters.From)?.Address, Guid.NewGuid().ToString(), new MailSender.MessageAddressee(parameters.EmailAddress, (string) null, (string) null, (string) null), new MailSender.MessageContent("Test", false, "Test mail account")), (Attachment[]) null)), "The mail send has failed.\r\n{0}");
    }
    finally
    {
      PXLongOperation.SetCustomInfo(new object());
    }
  }

  public virtual void DoTestEmailAccount(EMailAccount account, CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    try
    {
      if (!string.IsNullOrWhiteSpace(account.IncomingHostName))
      {
        using (MailReceiver receiver = MailAccountManager.GetReceiver(account, true))
        {
          int? incomingHostProtocol = account.IncomingHostProtocol;
          int num = 1;
          if (incomingHostProtocol.GetValueOrDefault() == num & incomingHostProtocol.HasValue)
            receiver.RootFolder = account.ImapRootFolder;
          EMailAccountMaint.WrapException((System.Action) (() => receiver.Test()), "The mail receive has failed.\r\n{0}");
        }
      }
      if (string.IsNullOrWhiteSpace(account.OutcomingHostName))
        return;
      using (MailSender sender = MailAccountManager.GetSender(account, true))
        EMailAccountMaint.WrapException((System.Action) (() => sender.Test()), "The mail send has failed.\r\n{0}");
    }
    finally
    {
      PXLongOperation.SetCustomInfo(new object());
    }
  }

  protected virtual void _(Events.RowSelected<EMailAccount> e)
  {
    if (e.Row == null)
      return;
    bool flag1 = e.Row.EmailAccountType == "E";
    PXAction<TMain> checkEmailAccount = this.CheckEmailAccount;
    bool? isActive = e.Row.IsActive;
    bool flag2 = true;
    int num = !(isActive.GetValueOrDefault() == flag2 & isActive.HasValue) ? 0 : (!flag1 ? 1 : 0);
    checkEmailAccount.SetEnabled(num != 0);
  }
}

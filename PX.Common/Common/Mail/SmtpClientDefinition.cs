// Decompiled with JetBrains decompiler
// Type: PX.Common.Mail.SmtpClientDefinition
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using PX.Common.SMTP.Client;
using PX.Common.TCP;
using System;

#nullable disable
namespace PX.Common.Mail;

public sealed class SmtpClientDefinition : IDisposable
{
  public static string _SMTPCLIENT_SLOT_KEY_PREFIX = "SmtpClient@" + PXContext.PXIdentity.IdentityName;
  private SmtpClient \u0002;

  public SmtpClientDefinition(MailSender parameter, int timeout)
  {
    this.\u0002(new SmtpClient(parameter.Host, parameter.Credential, MainTools.GetLongName(parameter.GetType()), timeout));
  }

  public SmtpClient Client => this.\u0002;

  private void \u0002(SmtpClient _param1) => this.\u0002 = _param1;

  public void Dispose() => ((TcpSession) this.Client)?.Dispose();
}

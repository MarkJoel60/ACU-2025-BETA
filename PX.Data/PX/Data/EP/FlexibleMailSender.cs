// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.FlexibleMailSender
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Mail;
using PX.SM;
using System;
using System.Net.Mail;

#nullable disable
namespace PX.Data.EP;

/// <exclude />
public class FlexibleMailSender : MailSender
{
  private readonly MailSender _baseSender;
  internal bool _validateFrom;
  internal string _address;

  public FlexibleMailSender(MailSender baseSender)
    : base(baseSender.Host, baseSender.Credential, baseSender.EmailAddress, baseSender.Timeout)
  {
    this._baseSender = baseSender != null ? baseSender : throw new ArgumentNullException(nameof (baseSender));
  }

  [Obsolete]
  public static FlexibleMailSender Create(EMailAccount account, bool decrypted)
  {
    if (account == null)
      throw new ArgumentNullException(nameof (account));
    if (string.IsNullOrEmpty(account.Address))
      throw new ArgumentException("An address cannot be empty.", nameof (account));
    string str1 = (string) null;
    string str2 = (string) null;
    bool? authenticationRequest = account.OutcomingAuthenticationRequest;
    bool flag1 = true;
    int? nullable1 = authenticationRequest.GetValueOrDefault() == flag1 & authenticationRequest.HasValue ? account.OutgoingConnectionEncryption : new int?();
    int? nullable2 = account.AuthenticationType;
    switch (nullable2 ?? 1)
    {
      case 1:
        str1 = account.LoginName;
        str2 = decrypted ? account.Password : PXRSACryptStringAttribute.Decrypt(account.Password);
        break;
      case 2:
        str1 = account.OutcomingLoginName;
        str2 = decrypted ? account.OutcomingPassword : PXRSACryptStringAttribute.Decrypt(account.OutcomingPassword);
        break;
    }
    ConnectionSettings connectionSettings;
    ref ConnectionSettings local = ref connectionSettings;
    string outcomingHostName = account.OutcomingHostName;
    nullable2 = account.OutcomingPort;
    int valueOrDefault = nullable2.GetValueOrDefault();
    int num = !nullable1.HasValue ? 0 : nullable1.Value;
    // ISSUE: explicit constructor call
    ((ConnectionSettings) ref local).\u002Ector(outcomingHostName, valueOrDefault, (SecurityProtocolType) num);
    CredentialSettings credentialSettings;
    // ISSUE: explicit constructor call
    ((CredentialSettings) ref credentialSettings).\u002Ector(str1, str2);
    nullable2 = account.OutcomingMailSender;
    MailSender.Types types;
    if (nullable2.HasValue)
    {
      switch (nullable2.GetValueOrDefault())
      {
        case 1:
          types = MailSender.Types.NativeSMTP;
          goto label_13;
        case 2:
          types = MailSender.Types.Dummy;
          goto label_13;
        case 4:
          types = MailSender.Types.File;
          goto label_13;
      }
    }
    types = MailSender.Types.SMTP;
label_13:
    int type = (int) types;
    ConnectionSettings host = connectionSettings;
    CredentialSettings login = credentialSettings;
    string address = account.Address;
    nullable2 = account.Timeout;
    int timeout = nullable2 ?? 60000;
    FlexibleMailSender flexibleMailSender = new FlexibleMailSender(MailSender.Create((MailSender.Types) type, host, login, address, timeout));
    bool? validateFrom = account.ValidateFrom;
    bool flag2 = true;
    flexibleMailSender._validateFrom = validateFrom.GetValueOrDefault() == flag2 & validateFrom.HasValue;
    flexibleMailSender._address = account.Address;
    return flexibleMailSender;
  }

  public override void Send(MailSender.MailMessageT message, Attachment[] files)
  {
    if (this._validateFrom)
    {
      string from = message.From;
      string str;
      if (from == null)
        str = (string) null;
      else
        str = from.TrimEnd(';');
      Mailbox mailbox;
      ref Mailbox local = ref mailbox;
      message = new MailSender.MailMessageT(Mailbox.TryParse(str, ref local) ? Mailbox.Create(mailbox.DisplayName, this._address) : this._address, message.UID, message.Addressee, message.Content, message.Payload);
    }
    this._baseSender.Send(message, files);
  }

  public override void Test() => this._baseSender.Test();

  public override void Dispose() => this._baseSender?.Dispose();
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.MultiFactorAuth.EmailFactorSender
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.EP;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Hosting;

#nullable disable
namespace PX.Data.MultiFactorAuth;

internal class EmailFactorSender(INotificationSender sender) : 
  EmailFactorSenderBase(sender),
  ITwoFactorSender
{
  public TwoFactorSenderType TypeId => TwoFactorSenderType.Email;

  public bool IsPersistentCode => true;

  public string Type => "Email";

  public bool ShowTextBox => true;

  public string ButtonName => "Email";

  public string ButtonToolTip => "Receive code by email";

  public (string message, bool isError) SendAcceptRequest(
    IEnumerable<(int companyId, Guid userId, int twoFactorLevel)> users,
    string innerCorrelation,
    Dictionary<string, string> customData)
  {
    (int, Guid, string) user = this.SelectUserEmails(users).FirstOrDefault<(int, Guid, string)>((Func<(int, Guid, string), bool>) (c => !string.IsNullOrWhiteSpace(c.email)));
    string email = user.Item3;
    if (string.IsNullOrWhiteSpace(email))
      return (PXLocalizer.Localize("There is no email address for this user."), true);
    string lang = Thread.CurrentThread.CurrentCulture.Name;
    HostingEnvironment.QueueBackgroundWorkItem((System.Action<CancellationToken>) (ct =>
    {
      Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
      using (PXAccess.GetAdminScopeForCompany(user.Item1))
      {
        if (this.SendWithTemplate<PreferencesEmail.twoFactorCodeByNotificationId>(user.Item2, email, customData["twofactor_persistentcode"]))
          return;
        this._sender.NotifyAndDeliver(new EmailNotificationParameters()
        {
          To = email,
          Body = customData["twofactor_persistentcode"]
        });
      }
    }));
    return (PXLocalizer.LocalizeFormat("Enter code which was sent to your email {0}:", (object) email), false);
  }

  public int ResendAfter => 300;
}

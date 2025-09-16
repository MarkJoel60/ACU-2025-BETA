// Decompiled with JetBrains decompiler
// Type: PX.Data.MultiFactorAuth.EmailMultifactorRegistrationSender
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.EP;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.MultiFactorAuth;

internal class EmailMultifactorRegistrationSender(INotificationSender sender) : 
  EmailFactorSenderBase(sender),
  IMultifactorRegistrationSender
{
  public (string message, bool isError) SendAcceptRequest(
    IEnumerable<(int companyId, Guid userId, int twoFactorLevel)> users,
    string innerCorrelation,
    Dictionary<string, string> customData)
  {
    (int, Guid, string) tuple = this.SelectUserEmails(users).FirstOrDefault<(int, Guid, string)>((Func<(int, Guid, string), bool>) (c => !string.IsNullOrWhiteSpace(c.email)));
    string email = tuple.Item3;
    if (string.IsNullOrWhiteSpace(email))
      return (PXLocalizer.Localize("There is no email address for this user."), true);
    using (PXAccess.GetAdminScopeForCompany(tuple.Item1))
    {
      try
      {
        this.SendRegistrationCode(customData, email, tuple.Item2);
        return (PXLocalizer.LocalizeFormat("To verify your identity by approving push requests on your mobile device: ~ ~1. Install Acumatica mobile app on your device. ~ ~2. Sign in to the account of this Acumatica instance using access code sent to your email: {0}. ~ ", (object) email), false);
      }
      catch (Exception ex)
      {
        return (PXLocalizer.LocalizeFormat("The following error occurred when sending the multifactor notification: {0}.", (object) ex.Message), true);
      }
    }
  }

  public void SendRegistrationCode(
    Dictionary<string, string> customData,
    string email,
    Guid userId)
  {
    if (this.SendWithTemplate<PreferencesEmail.twoFactorNewDeviceNotificationId>(userId, email, customData["twofactor_persistentcode"]))
      return;
    this._sender.NotifyAndDeliver(new EmailNotificationParameters()
    {
      To = email,
      Body = customData["twofactor_persistentcode"]
    });
  }

  public int ResendAfter => 300;
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.MailAccountManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using CommonServiceLocator;
using HtmlAgilityPack;
using PX.Common;
using PX.Common.Mail;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Data.EP;

/// <exclude />
public static class MailAccountManager
{
  private const 
  #nullable disable
  string _SLOT_PREFIX = "_MailAccountManager_";

  internal static void RegisterFactories(ContainerBuilder builder)
  {
    MailAccountManager.RegisterSenders(builder);
    MailAccountManager.RegisterSenderProxy(builder);
    MailAccountManager.RegisterReceivers(builder);
    MailAccountManager.RegisterRecieverProxy(builder);
  }

  internal static void RegisterSenders(ContainerBuilder builder)
  {
    RegistrationExtensions.Register<MailAccountManager.MailSenderFactory>(builder, (Func<IComponentContext, MailAccountManager.MailSenderFactory>) (c => (MailAccountManager.MailSenderFactory) (account =>
    {
      CredentialSettings credential;
      ConnectionSettings host;
      MailAccountManager.FillSenderConnectionInfo(account, out credential, out host);
      return (MailSender) new MailSender.SmtpSender(host, credential, account.Address, account.Timeout ?? 60000);
    }))).Keyed<MailAccountManager.MailSenderFactory>((object) 0).SingleInstance();
    RegistrationExtensions.Register<MailAccountManager.MailSenderFactory>(builder, (Func<IComponentContext, MailAccountManager.MailSenderFactory>) (c => (MailAccountManager.MailSenderFactory) (account =>
    {
      CredentialSettings credential;
      ConnectionSettings host;
      MailAccountManager.FillSenderConnectionInfo(account, out credential, out host);
      return (MailSender) new MailSender.NativeSmtpSender(host, credential, account.Address, account.Timeout ?? 60000);
    }))).Keyed<MailAccountManager.MailSenderFactory>((object) 3).SingleInstance();
    RegistrationExtensions.Register<MailAccountManager.MailSenderFactory>(builder, (Func<IComponentContext, MailAccountManager.MailSenderFactory>) (c => (MailAccountManager.MailSenderFactory) (account =>
    {
      CredentialSettings credential;
      ConnectionSettings host;
      MailAccountManager.FillSenderConnectionInfo(account, out credential, out host);
      return (MailSender) new MailSender.DummySmtpSender(host, credential, account.Address, account.Timeout ?? 60000);
    }))).Keyed<MailAccountManager.MailSenderFactory>((object) 2).SingleInstance();
    RegistrationExtensions.Register<MailAccountManager.MailSenderFactory>(builder, (Func<IComponentContext, MailAccountManager.MailSenderFactory>) (c => (MailAccountManager.MailSenderFactory) (account => (MailSender) new MailSender.DebugMailSender()))).Keyed<MailAccountManager.MailSenderFactory>((object) 4).SingleInstance();
  }

  internal static void RegisterSenderProxy(ContainerBuilder builder)
  {
    RegistrationExtensions.Register<MailAccountManager.MailSenderFactory>(builder, (Func<IComponentContext, MailAccountManager.MailSenderFactory>) (context =>
    {
      IComponentContext ctx = ResolutionExtensions.Resolve<IComponentContext>(context);
      return (MailAccountManager.MailSenderFactory) (account =>
      {
        MailAccountManager.MailSenderFactory mailSenderFactory6;
        if (!string.IsNullOrWhiteSpace(account.PluginTypeName))
        {
          object obj;
          ResolutionExtensions.TryResolveNamed(ctx, account.PluginTypeName, typeof (MailAccountManager.MailSenderFactory), ref obj);
          mailSenderFactory6 = obj is MailAccountManager.MailSenderFactory mailSenderFactory4 ? mailSenderFactory4 : throw new PXInvalidOperationException("The system cannot resolve the {0} plug-in type.", new object[1]
          {
            (object) account.PluginTypeName
          });
        }
        else
        {
          if (!account.OutcomingMailSender.HasValue)
            throw new PXInvalidOperationException("No mail sender is specified.");
          object obj;
          ResolutionExtensions.TryResolveKeyed(ctx, (object) account.OutcomingMailSender, typeof (MailAccountManager.MailSenderFactory), ref obj);
          mailSenderFactory6 = obj is MailAccountManager.MailSenderFactory mailSenderFactory5 ? mailSenderFactory5 : throw new PXInvalidOperationException("The system cannot resolve the {0} mail sender.", new object[1]
          {
            (object) account.OutcomingMailSender
          });
        }
        FlexibleMailSender flexibleMailSender = new FlexibleMailSender(mailSenderFactory6(account));
        bool? validateFrom = account.ValidateFrom;
        bool flag = true;
        flexibleMailSender._validateFrom = validateFrom.GetValueOrDefault() == flag & validateFrom.HasValue;
        flexibleMailSender._address = account.Address;
        return (MailSender) flexibleMailSender;
      });
    })).SingleInstance();
  }

  internal static void RegisterReceivers(ContainerBuilder builder)
  {
    RegistrationExtensions.Register<MailAccountManager.MailReceiverFactory>(builder, (Func<IComponentContext, MailAccountManager.MailReceiverFactory>) (c => (MailAccountManager.MailReceiverFactory) (account =>
    {
      CredentialSettings credential;
      ConnectionSettings host;
      MailAccountManager.FillReceiverConnectionInfo(account, out credential, out host);
      return (MailReceiver) new MailReceiver.Pop3Receiver(host, credential, account.Timeout ?? 60000);
    }))).Keyed<MailAccountManager.MailReceiverFactory>((object) 0).SingleInstance();
    RegistrationExtensions.Register<MailAccountManager.MailReceiverFactory>(builder, (Func<IComponentContext, MailAccountManager.MailReceiverFactory>) (c => (MailAccountManager.MailReceiverFactory) (account =>
    {
      CredentialSettings credential;
      ConnectionSettings host;
      MailAccountManager.FillReceiverConnectionInfo(account, out credential, out host);
      return (MailReceiver) new MailReceiver.ImapReceiver(host, credential, account.Timeout ?? 60000)
      {
        RootFolder = account.ImapRootFolder,
        DeleteFromServer = account.IncomingDelSuccess.GetValueOrDefault(),
        EnableImapEnvelope = account.EnableImapEnvelope.GetValueOrDefault()
      };
    }))).Keyed<MailAccountManager.MailReceiverFactory>((object) 1).SingleInstance();
  }

  internal static void RegisterRecieverProxy(ContainerBuilder builder)
  {
    RegistrationExtensions.Register<MailAccountManager.MailReceiverFactory>(builder, (Func<IComponentContext, MailAccountManager.MailReceiverFactory>) (context =>
    {
      IComponentContext ctx = ResolutionExtensions.Resolve<IComponentContext>(context);
      return (MailAccountManager.MailReceiverFactory) (account =>
      {
        if (!account.IncomingHostProtocol.HasValue)
          throw new PXInvalidOperationException("No mail receiver is specified.");
        object obj;
        ResolutionExtensions.TryResolveKeyed(ctx, (object) account.IncomingHostProtocol, typeof (MailAccountManager.MailReceiverFactory), ref obj);
        if (!(obj is MailAccountManager.MailReceiverFactory mailReceiverFactory2))
          throw new PXInvalidOperationException("The system cannot resolve the {0} mail receiver.", new object[1]
          {
            (object) account.IncomingHostProtocol
          });
        return mailReceiverFactory2 == null ? (MailReceiver) null : mailReceiverFactory2(account);
      });
    })).SingleInstance();
  }

  private static void FillSenderConnectionInfo(
    EMailAccount account,
    out CredentialSettings credential,
    out ConnectionSettings host)
  {
    if (account == null)
      throw new ArgumentNullException(nameof (account));
    if (string.IsNullOrEmpty(account.Address))
      throw new ArgumentException("An address cannot be empty.", nameof (account));
    credential = new CredentialSettings();
    bool? authenticationRequest = account.OutcomingAuthenticationRequest;
    bool flag1 = true;
    int? nullable1 = authenticationRequest.GetValueOrDefault() == flag1 & authenticationRequest.HasValue ? account.OutgoingConnectionEncryption : new int?();
    int? nullable2 = account.AuthenticationType;
    switch (nullable2 ?? 1)
    {
      case 1:
        string loginName = account.LoginName;
        bool? passwordIsDecrypted1 = account.PasswordIsDecrypted;
        bool flag2 = true;
        string str1 = passwordIsDecrypted1.GetValueOrDefault() == flag2 & passwordIsDecrypted1.HasValue ? account.Password : PXRSACryptStringAttribute.Decrypt(account.Password);
        credential = new CredentialSettings(loginName, str1);
        break;
      case 2:
        string outcomingLoginName = account.OutcomingLoginName;
        bool? passwordIsDecrypted2 = account.PasswordIsDecrypted;
        bool flag3 = true;
        string str2 = passwordIsDecrypted2.GetValueOrDefault() == flag3 & passwordIsDecrypted2.HasValue ? account.OutcomingPassword : PXRSACryptStringAttribute.Decrypt(account.OutcomingPassword);
        credential = new CredentialSettings(outcomingLoginName, str2);
        break;
      case 3:
      case 5:
        (Func<string> getAccessToken, Func<string> refreshAccessToken) tokenDelegates = MailAccountManager.GetTokenDelegates(account);
        credential = CredentialSettings.GetOAuthCredentials(account.Address, tokenDelegates.getAccessToken, tokenDelegates.refreshAccessToken);
        break;
    }
    ref ConnectionSettings local = ref host;
    string outcomingHostName = account.OutcomingHostName;
    nullable2 = account.OutcomingPort;
    int valueOrDefault = nullable2.GetValueOrDefault();
    int num = !nullable1.HasValue ? 0 : nullable1.Value;
    ConnectionSettings connectionSettings = new ConnectionSettings(outcomingHostName, valueOrDefault, (SecurityProtocolType) num);
    local = connectionSettings;
  }

  private static void FillReceiverConnectionInfo(
    EMailAccount account,
    out CredentialSettings credential,
    out ConnectionSettings host)
  {
    if (account == null)
      throw new ArgumentNullException(nameof (account));
    switch (account.AuthenticationType ?? 1)
    {
      case 3:
      case 5:
        (Func<string> getAccessToken, Func<string> refreshAccessToken) tokenDelegates = MailAccountManager.GetTokenDelegates(account);
        credential = CredentialSettings.GetOAuthCredentials(account.Address, tokenDelegates.getAccessToken, tokenDelegates.refreshAccessToken);
        break;
      default:
        string loginName = account.LoginName;
        bool? passwordIsDecrypted = account.PasswordIsDecrypted;
        bool flag = true;
        string str = passwordIsDecrypted.GetValueOrDefault() == flag & passwordIsDecrypted.HasValue ? account.Password : PXRSACryptStringAttribute.Decrypt(account.Password);
        credential = new CredentialSettings(loginName, str);
        break;
    }
    ref ConnectionSettings local = ref host;
    string incomingHostName = account.IncomingHostName;
    int valueOrDefault = account.IncomingPort.GetValueOrDefault();
    int? connectionEncryption = account.IncomingConnectionEncryption;
    int num = !connectionEncryption.HasValue || connectionEncryption.GetValueOrDefault() != 1 ? 0 : 1;
    ConnectionSettings connectionSettings = new ConnectionSettings(incomingHostName, valueOrDefault, (SecurityProtocolType) num);
    local = connectionSettings;
  }

  private static (Func<string> getAccessToken, Func<string> refreshAccessToken) GetTokenDelegates(
    EMailAccount account)
  {
    if (!account.OAuthApplicationID.HasValue)
      throw new PXInvalidOperationException("The {0} email account cannot be used for OAuth2 authentication. ", new object[1]
      {
        (object) account.EmailAccountID
      });
    IEmailAccessTokenProvider service = ServiceLocator.Current.GetInstance<IEmailAccessTokenProvider>();
    return ((Func<string>) (() => service.GetAccessToken(account)), (Func<string>) (() => service.RefreshAccessToken(account)));
  }

  public static MailSender Sender
  {
    get => MailAccountManager.GetSender(MailAccountManager.GetDefaultEmailAccount());
  }

  public static MailSender GetSender(EMailAccount account)
  {
    return MailAccountManager.GetSender(account, false);
  }

  public static MailSender GetSender(EMailAccount account, bool decrypted)
  {
    if ((account != null ? (!account.EmailAccountID.HasValue ? 1 : 0) : 1) != 0)
      return (MailSender) null;
    if (decrypted)
      account.PasswordIsDecrypted = new bool?(true);
    return ServiceLocator.Current.GetInstance<MailAccountManager.MailSenderFactory>()(account);
  }

  public static MailSender GetSender(string accountAddress)
  {
    EMailAccount account = (EMailAccount) PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where<EMailAccount.address, Equal<Required<EMailAccount.address>>>>.Config>.SelectWindowed(new PXGraph(), 0, 1, (object) accountAddress);
    return account == null ? (MailSender) null : MailAccountManager.GetSender(account);
  }

  public static MailReceiver Receiver
  {
    get => MailAccountManager.GetReceiver(MailAccountManager.GetDefaultEmailAccount());
  }

  public static MailReceiver GetReceiver(EMailAccount account)
  {
    return MailAccountManager.GetReceiver(account, false);
  }

  internal static MailReceiver GetReceiver(EMailAccount account, bool decrypted)
  {
    if ((account != null ? (!account.EmailAccountID.HasValue ? 1 : 0) : 1) != 0)
      return (MailReceiver) null;
    if (decrypted)
      account.PasswordIsDecrypted = new bool?(true);
    return ServiceLocator.Current.GetInstance<MailAccountManager.MailReceiverFactory>()(account);
  }

  public static int? DefaultMailAccountID
  {
    get => MailAccountManager.GetDefaultMailAccountID(new Guid?(PXAccess.GetUserID()));
  }

  public static int? DefaultAnyMailAccountID
  {
    get => MailAccountManager.GetDefaultMailAccountID(new Guid?(PXAccess.GetUserID()), true);
  }

  public static int? GetDefaultMailAccountID(int? contactID, bool includeExchangeAccount = false)
  {
    return MailAccountManager.GetDefaultMailAccountID(PXAccess.GetUserID(contactID), includeExchangeAccount);
  }

  public static int? GetDefaultMailAccountID(Guid? userId, bool includeExchangeAccount = false)
  {
    return MailAccountManager.GetDefaultEmailAccount(userId, includeExchangeAccount).EmailAccountID;
  }

  public static EMailAccount GetDefaultEmailAccount(bool includeExchangeAccount = false)
  {
    return MailAccountManager.GetDefaultEmailAccount(new Guid?(PXAccess.GetUserID()));
  }

  public static EMailAccount GetDefaultEmailAccount(Guid? userId, bool includeExchangeAccount = false)
  {
    return !userId.HasValue ? new EMailAccount() : MailAccountManager.GetUserSettingsEmailAccount(userId, includeExchangeAccount) ?? MailAccountManager.GetSystemSettingsEmailAccount(userId, includeExchangeAccount) ?? new EMailAccount();
  }

  public static EMailAccount GetUserSettingsEmailAccount(Guid? userId, bool includeExchangeAccount = false)
  {
    if (!userId.HasValue)
      return (EMailAccount) null;
    MailAccountManager.UserSettingsDefinition userSettings = MailAccountManager.GetUserSettings(userId.Value);
    return userSettings != null && userSettings.DefaultAccount?.EmailAccountType != null && (includeExchangeAccount || userSettings.DefaultAccount.EmailAccountType != "E") ? userSettings.DefaultAccount : (EMailAccount) null;
  }

  public static EMailAccount GetSystemSettingsEmailAccount(
    Guid? userId,
    bool includeExchangeAccount = false)
  {
    if (!userId.HasValue)
      return (EMailAccount) null;
    MailAccountManager.SystemSettingsDefinition systemSettings = MailAccountManager.GetSystemSettings();
    return systemSettings != null && systemSettings.DefaultAccount?.EmailAccountType != null && (includeExchangeAccount || systemSettings.DefaultAccount.EmailAccountType != "E") ? systemSettings.DefaultAccount : (EMailAccount) null;
  }

  public static EMailAccount GetEmailAccountIfAllowed(PXGraph graph, int? accountID)
  {
    EMailAccount accountIfAllowed = (EMailAccount) PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Required<EMailAccount.emailAccountID>>, And<Match<Current<AccessInfo.userName>>>>>.Config>.SelectSingleBound(graph, (object[]) null, (object) accountID);
    if (accountIfAllowed == null)
      return (EMailAccount) null;
    if (!(accountIfAllowed.EmailAccountType != "E"))
    {
      int? defaultOwnerId = accountIfAllowed.DefaultOwnerID;
      int? contactId = graph.Accessinfo.ContactID;
      if (!(defaultOwnerId.GetValueOrDefault() == contactId.GetValueOrDefault() & defaultOwnerId.HasValue == contactId.HasValue))
        return (EMailAccount) null;
    }
    return accountIfAllowed;
  }

  public static PreferencesEmail GetEmailPreferences()
  {
    return MailAccountManager.GetSystemSettings()?.EmailPreferences ?? new PreferencesEmail();
  }

  private static MailAccountManager.SystemSettingsDefinition GetSystemSettings()
  {
    return PXDatabase.GetSlot<MailAccountManager.SystemSettingsDefinition>("_MailAccountManager_SystemSettings", typeof (PreferencesEmail));
  }

  private static MailAccountManager.UserSettingsDefinition GetUserSettings(Guid userID)
  {
    return PXDatabase.GetSlot<MailAccountManager.UserSettingsDefinition, Guid>("_MailAccountManager_" + userID.ToString(), userID, typeof (EMailAccount), typeof (UserPreferences));
  }

  private static string SignatureAttribute => "data-email-signature-block";

  private static string GetSignatureWithLayoutTags(string signature)
  {
    return $"<div {MailAccountManager.SignatureAttribute}>{signature}</div>";
  }

  public static string GetSignature(PXGraph graph, MailAccountManager.SignatureOptions options)
  {
    UserPreferences userPreferences = (UserPreferences) PXSelectBase<UserPreferences, PXSelect<UserPreferences>.Config>.Search<UserPreferences.userID>(graph, (object) PXAccess.GetUserID());
    string mailSignature = userPreferences?.MailSignature;
    string html;
    if (mailSignature == null || (html = mailSignature.Trim()) == string.Empty)
      return string.Empty;
    string signatureWithLayoutTags = MailAccountManager.GetSignatureWithLayoutTags(HtmlEntensions.GetHtmlBodyContent(html));
    switch (options)
    {
      case MailAccountManager.SignatureOptions.NewEmail:
        bool? signatureToNewEmail = userPreferences.SignatureToNewEmail;
        bool flag1 = true;
        return !(signatureToNewEmail.GetValueOrDefault() == flag1 & signatureToNewEmail.HasValue) ? string.Empty : signatureWithLayoutTags;
      case MailAccountManager.SignatureOptions.ReplyAndForward:
        bool? toReplyAndForward = userPreferences.SignatureToReplyAndForward;
        bool flag2 = true;
        return !(toReplyAndForward.GetValueOrDefault() == flag2 & toReplyAndForward.HasValue) ? string.Empty : signatureWithLayoutTags;
      default:
        return signatureWithLayoutTags;
    }
  }

  public static string AppendSignature(
    string input,
    PXGraph graph,
    MailAccountManager.SignatureOptions options)
  {
    string str = input;
    string signature = MailAccountManager.GetSignature(graph, options);
    if (!string.IsNullOrEmpty(signature))
    {
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(str ?? string.Empty);
      HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//div");
      if (htmlNodeCollection != null)
      {
        foreach (HtmlNode htmlNode in (IEnumerable<HtmlNode>) htmlNodeCollection)
        {
          if (htmlNode.GetAttributeValue(MailAccountManager.SignatureAttribute, (string) null) != null)
            htmlNode.Remove();
        }
      }
      str = HtmlEntensions.MergeHtmls(htmlDocument.DocumentNode.OuterHtml, signature);
    }
    return str;
  }

  public static bool IsMailProcessingOff
  {
    get
    {
      PreferencesEmail emailPreferences = MailAccountManager.GetEmailPreferences();
      return emailPreferences != null && emailPreferences.SuspendEmailProcessing.GetValueOrDefault();
    }
  }

  public static int? DefaultSystemMailAccountID
  {
    get => MailAccountManager.GetEmailPreferences()?.DefaultEMailAccountID;
  }

  /// <summary>
  /// Factory method used to initialize <see cref="T:PX.Common.Mail.MailSender" /> from the <see cref="T:PX.SM.EMailAccount">Email Account</see>.
  /// Inject it from the DI.
  /// </summary>
  /// <param name="emailAccount">Email account for which <see cref="T:PX.Common.Mail.MailSender" /> will be created.</param>
  /// <returns></returns>
  public delegate MailSender MailSenderFactory(EMailAccount emailAccount);

  /// <summary>
  /// Factory method used to initialize <see cref="T:PX.Common.Mail.MailReceiver" /> from the <see cref="T:PX.SM.EMailAccount">Email Account</see>.
  /// Inject it from the DI.
  /// </summary>
  /// <param name="emailAccount">Email account for which <see cref="T:PX.Common.Mail.MailReceiver" /> will be created.</param>
  /// <returns></returns>
  public delegate MailReceiver MailReceiverFactory(EMailAccount emailAccount);

  private abstract class BaseSettingsDefinition
  {
    protected Lazy<PXGraph> Graph { get; } = new Lazy<PXGraph>((Func<PXGraph>) (() => PXGraph.CreateInstance<PXGraph>()));

    /// <summary>
    /// Gets EMailAccount by EMailAccountID, returns null if not found
    /// </summary>
    protected EMailAccount GetEMailAccount(int? accountId)
    {
      if (!accountId.HasValue || accountId.Value == 0)
        return (EMailAccount) null;
      return this.Graph.Value.SelectReadOnly<EMailAccount>().Where<EMailAccount>((Expression<Func<EMailAccount, bool>>) (a => a.EmailAccountID == accountId)).FirstOrDefault<EMailAccount>();
    }

    /// <summary>Gets PreferencesEmail, returns null if not found</summary>
    protected PreferencesEmail GetPreferencesEmail()
    {
      return this.Graph.Value.SelectReadOnly<PreferencesEmail>().FirstOrDefault<PreferencesEmail>();
    }
  }

  private class UserSettingsDefinition : 
    MailAccountManager.BaseSettingsDefinition,
    IPrefetchable<Guid>,
    IPXCompanyDependent
  {
    public EMailAccount DefaultAccount { get; private set; }

    public void Prefetch(Guid UserID)
    {
      this.DefaultAccount = this.GetEMailAccount(this.GetDefaultEMailAccountID(UserID));
    }

    private int? GetDefaultEMailAccountID(Guid UserID)
    {
      return this.Graph.Value.SelectReadOnly<UserPreferences>().Where<UserPreferences>((Expression<Func<UserPreferences, bool>>) (u => u.UserID == (Guid?) UserID)).Select<UserPreferences, int?>((Expression<Func<UserPreferences, int?>>) (u => u.DefaultEMailAccountID)).FirstOrDefault<int?>();
    }
  }

  private class SystemSettingsDefinition : 
    MailAccountManager.BaseSettingsDefinition,
    IPrefetchable,
    IPXCompanyDependent
  {
    public PreferencesEmail EmailPreferences { get; private set; }

    public EMailAccount DefaultAccount { get; private set; }

    public void Prefetch()
    {
      this.EmailPreferences = this.GetPreferencesEmail();
      this.DefaultAccount = this.GetEMailAccount((int?) this.EmailPreferences?.DefaultEMailAccountID);
    }
  }

  public enum SignatureOptions
  {
    Default,
    NewEmail,
    ReplyAndForward,
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.SM.EMailAccount
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM.Email;
using PX.TM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.SM;

[PXPrimaryGraph(typeof (EMailAccountMaint))]
[PXCacheName("Email Account")]
public class EMailAccount : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IIncludable,
  IRestricted,
  INotable
{
  protected 
  #nullable disable
  string _Description;
  protected string _ReplyAddress;
  protected string _LoginName;
  protected string _Password;
  protected string _OutcomingHostName;
  protected bool? _OutcomingAuthenticationRequest;
  protected bool? _OutcomingAuthenticationDifferent;
  protected string _OutcomingLoginName;
  protected string _OutcomingPassword;
  protected int? _OutcomingMailSender;
  protected string _IncomingHostName;
  protected int? _SendGroupMails;
  protected int? _AutoReceiveDelay;
  protected bool? _IncomingDelSuccess;
  protected string _IncomingAttachmentType;
  protected bool? _DeleteUnProcessed;
  protected bool? _Included;
  protected int? _TypeDelete;
  protected byte[] _tstamp;
  protected byte[] _GroupMask;

  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Selected", Visibility = PXUIVisibility.Service)]
  public virtual bool? Selected { get; set; }

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Email Account ID")]
  [EMailAccountPrimarySelector]
  [PXReferentialIntegrityCheck]
  public virtual int? EmailAccountID { get; set; }

  [PXDBString(100, InputMask = "")]
  [PXUIField(DisplayName = "Email Address", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Address { get; set; }

  [PXDBString(100, InputMask = "")]
  [PXUIField(DisplayName = "Account Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(typeof (Coalesce<Search<EMailAccount.description, Where<Current<EMailAccount.userID>, PX.Data.IsNull, And<EMailAccount.emailAccountID, Equal<Current<EMailAccount.emailAccountID>>>>>, Search<Users.displayName, Where<Current<EMailAccount.userID>, PX.Data.IsNotNull, And<Users.pKID, Equal<Current<EMailAccount.userID>>>>>>))]
  [PXFormula(typeof (Default<EMailAccount.userID>))]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>
  /// Calculated field for display account in form: Name&lt;email@domai.com&gt;
  /// </summary>
  [PXString]
  [PXDBCalced(typeof (BqlOperand<PX.Data.Concat<TypeArrayOf<IBqlOperand>.FilledWith<PX.Data.Concat<TypeArrayOf<IBqlOperand>.FilledWith<PX.Data.Concat<TypeArrayOf<IBqlOperand>.FilledWith<EMailAccount.description, Space>>, LeftAngleBracket>>, EMailAccount.address>>, IBqlString>.Concat<RightAngleBracket>), typeof (string))]
  [PXUIField(DisplayName = "Account Name", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string DisplayEmailAddress { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBString]
  [PXFormula(typeof (Default<EMailAccount.pluginTypeName>))]
  [PXDefault(typeof (IIf<Where<EMailAccount.pluginTypeName, PX.Data.IsNull>, EmailAccountTypesAttribute.standard, EmailAccountTypesAttribute.plugin>))]
  [EmailAccountTypes]
  [PXUIField(DisplayName = "Email Account Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual string EmailAccountType { get; set; }

  /// <summary>
  /// The identifier of the user associated with the contact.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.SM.Users.PKID">Users.PKID</see> field.
  /// </value>
  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Personal Account For", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Users.pKID), SubstituteKey = typeof (Users.username), DescriptionField = typeof (Users.fullName), CacheGlobal = true)]
  [PXForeignReference(typeof (EMailAccount.FK.User), ReferenceBehavior.SetNull)]
  public virtual Guid? UserID { get; set; }

  [PXDBString(100, InputMask = "")]
  [PXUIField(DisplayName = "Reply Address", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string ReplyAddress
  {
    get => this._ReplyAddress;
    set => this._ReplyAddress = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXProviderTypeSelector(new System.Type[] {typeof (IEmailConnectedService)}, SubstituteKey = typeof (PXProviderTypeSelectorAttribute.ProviderRec.displayTypeName))]
  [PXUIField(DisplayName = "Email Service Plug-In")]
  public virtual string PluginTypeName { get; set; }

  [PXBool]
  [PXUIField(Visible = false)]
  [PXFormula(typeof (BqlOperand<EMailAccount.pluginTypeName, IBqlString>.IsNotNull))]
  public virtual bool? IsOfPluginType { get; set; }

  [PXDBString(1, IsFixed = true, IsUnicode = false)]
  [PXDefault("A")]
  [PX.SM.SenderDisplayNameSource]
  [PXUIField(DisplayName = "Name Source")]
  public virtual string SenderDisplayNameSource { get; set; }

  [PXDBString(100, IsUnicode = false)]
  [PXUIField(DisplayName = "Sender Name")]
  [PXDefault(typeof (Coalesce<Search<EMailAccount.description, Where<Current<EMailAccount.userID>, PX.Data.IsNull, And<EMailAccount.emailAccountID, Equal<Current<EMailAccount.emailAccountID>>>>>, Search<Users.displayName, Where<Current<EMailAccount.userID>, PX.Data.IsNotNull, And<Users.pKID, Equal<Current<EMailAccount.userID>>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXFormula(typeof (Default<EMailAccount.userID>))]
  public virtual string AccountDisplayName { get; set; }

  [PXDBInt]
  [PX.SM.AuthenticationType.List]
  [PXFormula(typeof (Default<EMailAccount.authenticationMethod>))]
  [PXDefault(typeof (Switch<Case<Where<BqlOperand<EMailAccount.authenticationMethod, IBqlString>.IsEqual<EMailAccount.authenticationMethod.basic>>, PX.SM.AuthenticationType.main, Case<Where<BqlOperand<EMailAccount.authenticationMethod, IBqlString>.IsIn<EMailAccount.authenticationMethod.gmailLegacy, EMailAccount.authenticationMethod.azureLegacy>>, PX.SM.AuthenticationType.oAuth2Old, Case<Where<BqlOperand<EMailAccount.authenticationMethod, IBqlString>.IsIn<EMailAccount.authenticationMethod.gmail, EMailAccount.authenticationMethod.azure>>, PX.SM.AuthenticationType.oAuth2New, Case<Where<BqlOperand<EMailAccount.authenticationMethod, IBqlString>.IsEqual<EMailAccount.authenticationMethod.plugIn>>, PX.SM.AuthenticationType.plugIn>>>>, PX.SM.AuthenticationType.none>))]
  public virtual int? AuthenticationType { get; set; }

  [PXDBString(5, IsFixed = true)]
  [PXDefault("BASIC")]
  [EMailAccount.authenticationMethod.List(EMailAccount.authenticationMethod.ListType.Default)]
  [PXUIField(DisplayName = "Authentication Method", Required = true)]
  public virtual string AuthenticationMethod { get; set; }

  [PXDBInt]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "External Application", Required = true)]
  public virtual int? OAuthApplicationID { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "OAuth2 Scopes")]
  public virtual string OAuthScopes { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "OAuth2 Parameters")]
  public virtual string OAuthParameters { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Azure Tenant ID", Visible = false)]
  public string AzureTenantID { get; set; }

  [PXDBString(100, InputMask = "")]
  [PXUIField(DisplayName = "Incoming Mail Username")]
  public virtual string LoginName
  {
    get => this._LoginName;
    set => this._LoginName = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Incoming Mail Password")]
  public virtual string Password
  {
    get => this._Password;
    set => this._Password = value;
  }

  [PXBool]
  [PXFormula(typeof (False))]
  public bool? PasswordIsDecrypted { get; set; }

  [PXDBString(100, InputMask = "")]
  [PXUIField(DisplayName = "Outgoing Mail Server")]
  public virtual string OutcomingHostName
  {
    get => !string.IsNullOrEmpty(this._OutcomingHostName) ? this._OutcomingHostName : (string) null;
    set => this._OutcomingHostName = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "My Outgoing Server Requires Authentication")]
  [PXDefault(false)]
  public virtual bool? OutcomingAuthenticationRequest
  {
    get
    {
      if (!this.AuthenticationType.HasValue)
        return new bool?();
      int? authenticationType = this.AuthenticationType;
      int num = 0;
      return new bool?(!(authenticationType.GetValueOrDefault() == num & authenticationType.HasValue));
    }
    set
    {
      if (!value.HasValue)
        return;
      bool? nullable = value;
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        int? authenticationType = this.AuthenticationType;
        int num = 0;
        if (!(authenticationType.GetValueOrDefault() == num & authenticationType.HasValue))
          return;
        this.AuthenticationType = new int?(1);
      }
      else
        this.AuthenticationType = new int?(0);
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "Log on Using")]
  [PXDefault(false)]
  public virtual bool? OutcomingAuthenticationDifferent
  {
    get
    {
      if (!this.AuthenticationType.HasValue)
        return new bool?();
      int? authenticationType = this.AuthenticationType;
      int num = 2;
      return new bool?(authenticationType.GetValueOrDefault() == num & authenticationType.HasValue);
    }
    set
    {
      if (!value.HasValue)
        return;
      bool? nullable = value;
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        this.AuthenticationType = new int?(2);
      }
      else
      {
        int? authenticationType = this.AuthenticationType;
        int num = 2;
        if (!(authenticationType.GetValueOrDefault() == num & authenticationType.HasValue))
          return;
        this.AuthenticationType = new int?(1);
      }
    }
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Outgoing Connection Encryption")]
  [MailConnectionEncryption.Outgoing]
  [PXDefault(typeof (IIf<BqlOperand<EMailAccount.emailAccountType, IBqlString>.IsEqual<EmailAccountTypesAttribute.standard>, MailConnectionEncryption.explicitTls, MailConnectionEncryption.none>))]
  [PXFormula(typeof (Default<EMailAccount.emailAccountType>))]
  public virtual int? OutgoingConnectionEncryption { get; set; }

  [PXDBString(100, InputMask = "")]
  [PXUIField(DisplayName = "Outgoing Mail Username")]
  public virtual string OutcomingLoginName
  {
    get => this._OutcomingLoginName;
    set => this._OutcomingLoginName = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Outgoing Mail Password")]
  public virtual string OutcomingPassword
  {
    get => this._OutcomingPassword;
    set => this._OutcomingPassword = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Outgoing Mail Port")]
  [PXDefault(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<EMailAccount.emailAccountType, Equal<EmailAccountTypesAttribute.standard>>>>>.And<BqlOperand<EMailAccount.outgoingConnectionEncryption, IBqlInt>.IsEqual<MailConnectionEncryption.none>>>, MailConnectionPorts.Outgoing.smtpNoneSecureOutgoingPort, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<EMailAccount.emailAccountType, Equal<EmailAccountTypesAttribute.standard>>>>>.And<BqlOperand<EMailAccount.outgoingConnectionEncryption, IBqlInt>.IsEqual<MailConnectionEncryption.implicitTls>>>, MailConnectionPorts.Outgoing.smtpImplicitTlsOutgoingPort, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<EMailAccount.emailAccountType, Equal<EmailAccountTypesAttribute.standard>>>>>.And<BqlOperand<EMailAccount.outgoingConnectionEncryption, IBqlInt>.IsEqual<MailConnectionEncryption.explicitTls>>>, MailConnectionPorts.Outgoing.smtpExplicitTlsOutgoingPort>>>, Null>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXFormula(typeof (Default<EMailAccount.outgoingConnectionEncryption, EMailAccount.emailAccountType>))]
  public virtual int? OutcomingPort { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Outcoming Mail Sender")]
  [PXFormula(typeof (Default<EMailAccount.emailAccountType>))]
  [PXDefault(typeof (IIf<Where<EMailAccount.emailAccountType, Equal<EmailAccountTypesAttribute.standard>>, EMailAccount.Sender.custom, Null>), PersistingCheck = PXPersistingCheck.Nothing)]
  [EMailAccount.Sender.List]
  public virtual int? OutcomingMailSender
  {
    get => this._OutcomingMailSender;
    set => this._OutcomingMailSender = value;
  }

  [PXDBInt]
  [PXDefault(1)]
  [IncomingMailProtocols]
  [PXUIField(DisplayName = "Protocol")]
  public virtual int? IncomingHostProtocol { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Incoming Mail Processing")]
  [PXDefault(false)]
  public virtual bool? IncomingProcessing { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Add Tags for the Incoming Processing")]
  [PXDefault(false, typeof (EMailAccount.incomingProcessing))]
  [PXFormula(typeof (Default<EMailAccount.incomingProcessing>))]
  [PXFormula(typeof (Default<EMailAccount.routeEmployeeEmails>))]
  public virtual bool? AddIncomingProcessingTags { get; set; }

  [PXDBString(100, InputMask = "")]
  [PXUIField(DisplayName = "Incoming Mail Server")]
  public virtual string IncomingHostName
  {
    get => !string.IsNullOrEmpty(this._IncomingHostName) ? this._IncomingHostName : (string) null;
    set => this._IncomingHostName = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Incoming Connection Encryption")]
  [MailConnectionEncryption.Incoming]
  [PXDefault(typeof (IIf<BqlOperand<EMailAccount.emailAccountType, IBqlString>.IsEqual<EmailAccountTypesAttribute.standard>, MailConnectionEncryption.implicitTls, MailConnectionEncryption.none>))]
  [PXFormula(typeof (Default<EMailAccount.emailAccountType>))]
  public virtual int? IncomingConnectionEncryption { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Incoming Mail Port")]
  [PXDefault(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<EMailAccount.incomingConnectionEncryption, Equal<MailConnectionEncryption.none>>>>>.And<BqlOperand<EMailAccount.emailAccountType, IBqlString>.IsEqual<EmailAccountTypesAttribute.standard>>>, IIf<BqlOperand<EMailAccount.incomingHostProtocol, IBqlInt>.IsEqual<IncomingMailProtocolsAttribute.pop3>, MailConnectionPorts.Incoming.pop3NonSecureIncomingPort, MailConnectionPorts.Incoming.imapNonSecureIncomingPort>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<EMailAccount.incomingConnectionEncryption, Equal<MailConnectionEncryption.implicitTls>>>>>.And<BqlOperand<EMailAccount.emailAccountType, IBqlString>.IsEqual<EmailAccountTypesAttribute.standard>>>, IIf<BqlOperand<EMailAccount.incomingHostProtocol, IBqlInt>.IsEqual<IncomingMailProtocolsAttribute.pop3>, MailConnectionPorts.Incoming.pop3TlsIncomingPort, MailConnectionPorts.Incoming.imapTlsIncomingPort>>>, Null>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXFormula(typeof (Default<EMailAccount.incomingConnectionEncryption, EMailAccount.incomingHostProtocol, EMailAccount.isOfPluginType>))]
  public virtual int? IncomingPort { get; set; }

  /// <summary>
  /// This field indicates that this <see cref="T:PX.SM.EMailAccount">email account</see> supports email receiving.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (IIf<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<EMailAccount.incomingHostName, PX.Data.IsNull>>>, PX.Data.Or<BqlOperand<EMailAccount.incomingHostName, IBqlString>.IsEqual<PX.Data.Empty>>>>.Or<BqlOperand<EMailAccount.isOfPluginType, IBqlBool>.IsEqual<PX.Data.True>>, False, PX.Data.True>))]
  public virtual bool? SupportReceiving { get; set; }

  /// <summary>
  /// This field indicates that this <see cref="T:PX.SM.EMailAccount">email account</see> supports email sending.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (IIf<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<EMailAccount.outcomingHostName, PX.Data.IsNull>>>>.Or<BqlOperand<EMailAccount.outcomingHostName, IBqlString>.IsEqual<PX.Data.Empty>>>>>.And<BqlOperand<EMailAccount.isOfPluginType, IBqlBool>.IsEqual<False>>, False, PX.Data.True>))]
  public virtual bool? SupportSending { get; set; }

  [PXDefault(0, PersistingCheck = PXPersistingCheck.Null)]
  [PXDBInt]
  [PXUIField(DisplayName = "Group Mails")]
  public virtual int? SendGroupMails
  {
    get => new int?(this._SendGroupMails.GetValueOrDefault());
    set => this._SendGroupMails = value;
  }

  [PXDefault(0, PersistingCheck = PXPersistingCheck.Null)]
  [PXDBTimeSpanLong(Format = TimeSpanFormatType.DaysHoursMinites)]
  [PXUIField(DisplayName = "Receive Mail Every")]
  public virtual int? AutoReceiveDelay
  {
    get => new int?(this._AutoReceiveDelay.GetValueOrDefault());
    set => this._AutoReceiveDelay = value;
  }

  [PXDBString(250, IsUnicode = false)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Root Folder")]
  public virtual string ImapRootFolder { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Server Validates From Address")]
  public virtual bool? ValidateFrom { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Connection Timeout")]
  [PX.SM.Timeout]
  [PXDefault(60000)]
  public virtual int? Timeout { get; set; }

  [PXDefault(typeof (PX.SM.FetchingBehavior.markEmailOnServerAsRead), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXFormula(typeof (IIf<Where<EMailAccount.emailAccountType, Equal<EmailAccountTypesAttribute.standard>, And<EMailAccount.incomingHostProtocol, Equal<IncomingMailProtocolsAttribute.imap>>>, Current<EMailAccount.fetchingBehavior>, Null>))]
  [PXDBInt]
  [PXUIField(DisplayName = "After Receiving")]
  [PX.SM.FetchingBehavior.List]
  public virtual int? FetchingBehavior { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Remove Read Messages from Server")]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? IncomingDelSuccess
  {
    get => this._IncomingDelSuccess;
    set => this._IncomingDelSuccess = value;
  }

  [PXDBString(InputMask = "")]
  [PXUIField(DisplayName = "Allowed Attachment Type")]
  public virtual string IncomingAttachmentType
  {
    get => this._IncomingAttachmentType;
    set => this._IncomingAttachmentType = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Delete Emails")]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? DeleteUnProcessed
  {
    get => this._DeleteUnProcessed;
    set => this._DeleteUnProcessed = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Reply to unassigned emails")]
  public virtual bool? ProcessUnassigned { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Reply template")]
  [PXSelector(typeof (Search<Notification.notificationID>), SubstituteKey = typeof (Notification.name), DescriptionField = typeof (Notification.name))]
  public virtual int? ResponseNotificationID { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Confirm Receipt")]
  public virtual bool? ConfirmReceipt { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Confirmation template")]
  [PXSelector(typeof (Search<Notification.notificationID>), SubstituteKey = typeof (Notification.name), DescriptionField = typeof (Notification.name))]
  public virtual int? ConfirmReceiptNotificationID { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Scenario ID", Visible = false)]
  public virtual Guid? ProcessScenarioID { get; set; }

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Forbid Routing")]
  public virtual bool? ForbidRouting { get; set; }

  [PXUnboundDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXBool]
  [PXUIField(DisplayName = "Included")]
  public virtual bool? Included
  {
    get => this._Included;
    set => this._Included = value;
  }

  [PXInt]
  [PXUIField(DisplayName = "Inbox", Visible = false)]
  public virtual int? InboxCount { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Outbox", Visible = false)]
  public virtual int? OutboxCount { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Create New Case")]
  public virtual bool? CreateCase { get; set; }

  [PXDBString(10)]
  [PXUIField(DisplayName = "New Case Class")]
  public virtual string CreateCaseClassID { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Route Employee Emails")]
  [PXDefault(false)]
  public virtual bool? RouteEmployeeEmails { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Associate with Contact")]
  public virtual bool? CreateActivity { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Create New Lead")]
  public virtual bool? CreateLead { get; set; }

  [PXDBString(10)]
  [PXUIField(DisplayName = "New Lead Class")]
  public virtual string CreateLeadClassID { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Add brief information about references")]
  public virtual bool? AddUpInformation { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "")]
  [PX.Data.TypeDelete]
  [PXDefault(1)]
  public virtual int? TypeDelete
  {
    get => new int?(this._TypeDelete ?? 1);
    set => this._TypeDelete = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DeletedDatabaseRecord { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Default Email Workgroup")]
  [PXCompanyTreeSelector]
  public virtual int? DefaultWorkgroupID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Default Email Owner")]
  public virtual int? DefaultOwnerID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Email Assignment Map")]
  public virtual int? DefaultEmailAssignmentMapID { get; set; }

  /// <summary>
  /// Indicates whether the IMAP envelope will be fetched when reading emails.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Enable IMAP Envelope")]
  [PXDefault(false)]
  public virtual bool? EnableImapEnvelope { get; set; }

  [PXNote]
  [PXSearchable(65535 /*0xFFFF*/, "Email Account: {0}", new System.Type[] {typeof (EMailAccount.description)}, new System.Type[] {typeof (EMailAccount.description), typeof (EMailAccount.address)}, Line1Format = "{0}", Line1Fields = new System.Type[] {typeof (EMailAccount.address)}, Line2Format = "", Line2Fields = new System.Type[] {})]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBGroupMask]
  public virtual byte[] GroupMask
  {
    get => this._GroupMask;
    set => this._GroupMask = value;
  }

  public class PK : PrimaryKeyOf<EMailAccount>.By<EMailAccount.emailAccountID>
  {
    public static EMailAccount Find(PXGraph graph, int? emailAccountID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<EMailAccount>.By<EMailAccount.emailAccountID>.FindBy(graph, (object) emailAccountID, options);
    }
  }

  public static class FK
  {
    public class ResponseNotification : 
      PrimaryKeyOf<Notification>.By<Notification.notificationID>.ForeignKeyOf<EMailAccount>.By<EMailAccount.responseNotificationID>
    {
    }

    public class ConfirmReceiptNotification : 
      PrimaryKeyOf<Notification>.By<Notification.notificationID>.ForeignKeyOf<EMailAccount>.By<EMailAccount.confirmReceiptNotificationID>
    {
    }

    public class DefaultWorkgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<EMailAccount>.By<EMailAccount.defaultWorkgroupID>
    {
    }

    public class DefaultOwner : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<EMailAccount>.By<EMailAccount.defaultOwnerID>
    {
    }

    public class User : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<EMailAccount>.By<EMailAccount.userID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailAccount.selected>
  {
  }

  public abstract class emailAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailAccount.emailAccountID>
  {
  }

  public abstract class address : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailAccount.address>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailAccount.description>
  {
  }

  public abstract class displayEmailAddress : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccount.displayEmailAddress>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailAccount.isActive>
  {
  }

  public abstract class emailAccountType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccount.emailAccountType>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EMailAccount.userID>
  {
    public class PreventMakingPersonalIfUsedAsSystem<TSelect> : 
      EditPreventor<TypeArrayOf<IBqlField>.FilledWith<EMailAccount.userID>>.On<EMailAccountMaint>.IfExists<TSelect>
      where TSelect : BqlCommand, new()
    {
      protected System.Type _SubstituteKey;

      [InjectDependency]
      protected PXSiteMapProvider SiteMapProvider { get; private set; }

      public override void Initialize()
      {
        base.Initialize();
        using (IEnumerator<PXSelectorAttribute> enumerator = this.Base.Caches[typeof (EMailAccount)].GetAttributesReadonly(nameof (userID)).OfType<PXSelectorAttribute>().GetEnumerator())
        {
          if (!enumerator.MoveNext())
            return;
          this._SubstituteKey = enumerator.Current.SubstituteKey;
        }
      }

      protected override void SetError(
        PXCache cache,
        PXFieldVerifyingEventArgs e,
        System.Type field,
        string editPreventingReason)
      {
        try
        {
          base.SetError(cache, e, field, editPreventingReason);
        }
        catch (Exception ex)
        {
          if (this._SubstituteKey != (System.Type) null)
          {
            object newValue = e.NewValue;
            cache.RaiseFieldSelecting(nameof (userID), e.Row, ref newValue, false);
            PXFieldState pxFieldState = newValue as PXFieldState;
            e.NewValue = pxFieldState != null ? pxFieldState.Value : newValue;
          }
          throw;
        }
      }

      protected override void OnPreventEdit(GetEditPreventingReasonArgs args)
      {
        if (args.NewValue != null && args.Cache.GetValue(args.Row, args.Field.Name) == null)
          return;
        args.Cancel = true;
      }

      protected override string CreateEditPreventingReason(
        GetEditPreventingReasonArgs arg,
        object firstPreventingEntity,
        string fieldName,
        string currentTableName,
        string foreignTableName)
      {
        object row = PXResult.UnwrapFirst(firstPreventingEntity);
        System.Type primaryGraphType = new EntityHelper((PXGraph) this.Base).GetPrimaryGraphType(row, false);
        string name = new TSelect().GetFieldPairs().FirstOrDefault<KeyValuePair<System.Type, System.Type>>().Key.Name;
        string displayName = PXUIFieldAttribute.GetDisplayName(this.Base.Caches[row.GetType()], name);
        if (primaryGraphType != (System.Type) null)
        {
          PXSiteMapNode siteMapNodeUnsecure = this.SiteMapProvider.FindSiteMapNodeUnsecure(primaryGraphType);
          if (siteMapNodeUnsecure != null)
            return string.Format(PXMessages.Localize("The current system email account cannot be used as a personal email account because it is already specified as {0} on the {1} ({2}) form. Personal email accounts cannot be used for automated email."), (object) displayName, (object) siteMapNodeUnsecure.Title, (object) siteMapNodeUnsecure.ScreenID);
        }
        return PXMessages.Localize("The current system email account cannot be used as a personal email account.");
      }

      public override PXErrorLevel ErrorLevel => PXErrorLevel.Error;
    }
  }

  public abstract class replyAddress : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailAccount.replyAddress>
  {
  }

  public abstract class pluginTypeName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccount.pluginTypeName>
  {
  }

  public abstract class isOfPluginType : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailAccount.isOfPluginType>
  {
  }

  public abstract class senderDisplayNameSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccount.senderDisplayNameSource>
  {
  }

  public abstract class accountDisplayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccount.accountDisplayName>
  {
  }

  public abstract class authenticationType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailAccount.authenticationType>
  {
  }

  public abstract class authenticationMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccount.authenticationMethod>
  {
    public const int Length = 5;
    public const string Empty = "EMPTY";
    public const string Basic = "BASIC";
    public const string Gmail = "GMAIL";
    public const string Azure = "AZURE";
    public const string GmailLegacy = "GMLEG";
    public const string AzureLegacy = "AZLEG";
    public const string PlugIn = "PLUGN";

    public class empty : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      EMailAccount.authenticationMethod.empty>
    {
      public empty()
        : base("EMPTY")
      {
      }
    }

    public class basic : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      EMailAccount.authenticationMethod.basic>
    {
      public basic()
        : base("BASIC")
      {
      }
    }

    public class gmail : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      EMailAccount.authenticationMethod.gmail>
    {
      public gmail()
        : base("GMAIL")
      {
      }
    }

    public class azure : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      EMailAccount.authenticationMethod.azure>
    {
      public azure()
        : base("AZURE")
      {
      }
    }

    public class gmailLegacy : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      EMailAccount.authenticationMethod.gmailLegacy>
    {
      public gmailLegacy()
        : base("GMLEG")
      {
      }
    }

    public class azureLegacy : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      EMailAccount.authenticationMethod.azureLegacy>
    {
      public azureLegacy()
        : base("AZLEG")
      {
      }
    }

    public class plugIn : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      EMailAccount.authenticationMethod.plugIn>
    {
      public plugIn()
        : base("PLUGN")
      {
      }
    }

    public class ListAttribute(EMailAccount.authenticationMethod.ListType type = EMailAccount.authenticationMethod.ListType.Default) : 
      PXStringListAttribute(Enumerable.ToArray<(string, string)>(EMailAccount.authenticationMethod.ListAttribute.GetValueAndLabels(type)))
    {
      private static IEnumerable<(string, string)> GetValueAndLabels(
        EMailAccount.authenticationMethod.ListType type)
      {
        yield return ("BASIC", "Basic Authentication");
        yield return ("GMAIL", "OAuth 2.0 for Gmail");
        yield return ("AZURE", "OAuth 2.0 for Microsoft 365");
        yield return ("AZLEG", "Exchange Online SMTP/IMAP/POP3");
        yield return ("GMLEG", "Google SMTP/IMAP/POP3");
        if (type == EMailAccount.authenticationMethod.ListType.WithPlugIn)
          yield return ("PLUGN", "Plug-In");
      }
    }

    public enum ListType
    {
      Default,
      WithPlugIn,
    }
  }

  public abstract class oAuthApplicationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailAccount.oAuthApplicationID>
  {
  }

  public abstract class oAuthScopes : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailAccount.oAuthScopes>
  {
  }

  public abstract class oAuthParameters : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccount.oAuthParameters>
  {
  }

  public abstract class azureTenantID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailAccount.azureTenantID>
  {
  }

  public abstract class loginName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailAccount.loginName>
  {
  }

  public abstract class password : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailAccount.password>
  {
  }

  public abstract class passwordIsDecrypted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailAccount.passwordIsDecrypted>
  {
  }

  public abstract class outcomingHostName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccount.outcomingHostName>
  {
  }

  public abstract class outcomingAuthenticationRequest : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailAccount.outcomingAuthenticationRequest>
  {
  }

  public abstract class outcomingAuthenticationDifferent : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailAccount.outcomingAuthenticationDifferent>
  {
  }

  public abstract class outgoingConnectionEncryption : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailAccount.outgoingConnectionEncryption>
  {
  }

  public abstract class outcomingLoginName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccount.outcomingLoginName>
  {
  }

  public abstract class outcomingPassword : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccount.outcomingPassword>
  {
  }

  public abstract class outcomingPort : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailAccount.outcomingPort>
  {
  }

  public abstract class outcomingMailSender : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailAccount.outcomingMailSender>
  {
  }

  public abstract class incomingHostProtocol : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailAccount.incomingHostProtocol>
  {
  }

  public abstract class incomingProcessing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailAccount.incomingProcessing>
  {
  }

  public abstract class addIncomingProcessingTags : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailAccount.addIncomingProcessingTags>
  {
  }

  public abstract class incomingHostName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccount.incomingHostName>
  {
  }

  public abstract class incomingConnectionEncryption : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailAccount.incomingConnectionEncryption>
  {
  }

  public abstract class incomingPort : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailAccount.incomingPort>
  {
  }

  public abstract class supportReceiving : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailAccount.supportReceiving>
  {
  }

  public abstract class supportSending : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailAccount.supportSending>
  {
  }

  public abstract class sendGroupMails : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailAccount.sendGroupMails>
  {
  }

  public abstract class autoReceiveDelay : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailAccount.autoReceiveDelay>
  {
  }

  public abstract class imapRootFolder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccount.imapRootFolder>
  {
  }

  public abstract class validateFrom : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailAccount.validateFrom>
  {
  }

  public abstract class timeout : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailAccount.timeout>
  {
  }

  public abstract class fetchingBehavior : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailAccount.fetchingBehavior>
  {
  }

  public abstract class incomingDelSuccess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailAccount.incomingDelSuccess>
  {
  }

  public abstract class incomingAttachmentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccount.incomingAttachmentType>
  {
  }

  public abstract class deleteUnProcessed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailAccount.deleteUnProcessed>
  {
  }

  public abstract class processUnassigned : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailAccount.processUnassigned>
  {
  }

  public abstract class responseNotificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailAccount.responseNotificationID>
  {
  }

  public abstract class confirmReceipt : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailAccount.confirmReceipt>
  {
  }

  public abstract class confirmReceiptNotificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailAccount.confirmReceiptNotificationID>
  {
  }

  public abstract class processScenarioID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EMailAccount.processScenarioID>
  {
  }

  public abstract class forbidRouting : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailAccount.forbidRouting>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailAccount.included>
  {
  }

  public abstract class inboxCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailAccount.inboxCount>
  {
  }

  public abstract class outboxCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailAccount.outboxCount>
  {
  }

  public abstract class createCase : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailAccount.createCase>
  {
  }

  public abstract class createCaseClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccount.createCaseClassID>
  {
  }

  public abstract class routeEmployeeEmails : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailAccount.routeEmployeeEmails>
  {
  }

  public abstract class createActivity : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailAccount.createActivity>
  {
  }

  public abstract class createLead : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailAccount.createLead>
  {
  }

  public abstract class createLeadClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccount.createLeadClassID>
  {
  }

  public abstract class addUpInformation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailAccount.addUpInformation>
  {
  }

  public abstract class typeDelete : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailAccount.typeDelete>
  {
  }

  public abstract class deletedDatabaseRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailAccount.deletedDatabaseRecord>
  {
  }

  public abstract class defaultWorkgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailAccount.defaultWorkgroupID>
  {
  }

  public abstract class defaultOwnerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailAccount.defaultOwnerID>
  {
  }

  public abstract class defaultEmailAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailAccount.defaultEmailAssignmentMapID>
  {
  }

  public abstract class enableImapEnvelope : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailAccount.enableImapEnvelope>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EMailAccount.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EMailAccount.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccount.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EMailAccount.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EMailAccount.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccount.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EMailAccount.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EMailAccount.Tstamp>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EMailAccount.groupMask>
  {
  }

  public class Sender
  {
    public const int Custom = 0;
    public const int Dummy = 2;
    public const int Native = 3;
    public const int File = 4;

    public class List : PXIntListAttribute
    {
      public List()
        : base((0, "Custom"), (2, "Dummy"), (3, "Native"), (4, "File"))
      {
      }
    }

    public class UI
    {
      public const string Custom = "Custom";
      public const string Dummy = "Dummy";
      public const string Native = "Native";
      public const string File = "File";
    }

    public sealed class custom : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    EMailAccount.Sender.custom>
    {
      public custom()
        : base(0)
      {
      }
    }
  }
}

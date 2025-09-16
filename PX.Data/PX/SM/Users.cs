// Decompiled with JetBrains decompiler
// Type: PX.SM.Users
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Access.ActiveDirectory;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.Maintenance;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable enable
namespace PX.SM;

/// <summary>Represents a user account to access Acumatica ERP.</summary>
/// <remarks>
/// To access Acumatica ERP, an individual must have a user account in the system and a user role assigned to the account.
/// Then based on their roles, users may access only the resources and perform only the actions they are authorized to.
/// The records of this type are created and edited on the <i>Users (SM201010)</i> form,
/// which corresponds to the <see cref="T:PX.SM.Access" /> graph.
/// </remarks>
[PXCacheName("User")]
[PXPrimaryGraph(new System.Type[] {typeof (AccessWebUsers), typeof (PX.SM.Access)}, new System.Type[] {typeof (Select<Users, Where<Users.guest, Equal<PX.Data.True>, And<Users.pKID, Equal<Current<Users.pKID>>>>>), typeof (Select<Users, Where<Users.pKID, Equal<Current<Users.pKID>>>>)})]
[DebuggerDisplay("Username = {Username}, PKID = {PKID}")]
public class Users : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IIncludable,
  IRestricted,
  INotable
{
  protected Guid? _PKID;
  protected 
  #nullable disable
  string _username;
  private string _domain;
  private ReadOnlyCollection<Login> _logins;
  protected string _FirstName;
  protected string _LastName;
  protected string _ApplicationName;
  protected string _Email;
  protected string _Phone;
  protected string _Comment;
  protected int? _ContactID;
  protected bool? _GeneratePassword;
  protected string _Password;
  protected string _OldPassword;
  protected string _NewPassword;
  protected string _ConfirmPassword;
  protected bool? _AllowPasswordRecovery;
  protected string _PasswordQuestion;
  protected string _PasswordAnswer;
  protected string _RecoveryLink;
  protected bool? _PasswordChangeOnNextLogin;
  protected bool? _PasswordChangeable;
  protected bool? _PasswordNeverExpires;
  protected System.DateTime? _LastActivityDate;
  protected System.DateTime? _LastLoginDate;
  protected System.DateTime? _LastPasswordChangedDate;
  protected System.DateTime? _CreationDate;
  protected bool? _IsOnLine;
  protected System.DateTime? _LockedOutDate;
  protected bool? _IsLockedOut;
  protected System.DateTime? _LastLockedOutDate;
  protected int? _FailedPasswordAttemptCount;
  protected System.DateTime? _FailedPasswordAttemptWindowStart;
  protected int? _FailedPasswordAnswerAttemptCount;
  protected System.DateTime? _FailedPasswordAnswerAttemptWindowStart;
  protected byte[] _GroupMask;
  protected bool? _Included;
  protected string _ActivationID;
  public const int AllowedSessionsDefautValue = 3;

  /// <summary>The unique identifier assigned to the user login.</summary>
  [PXDBGuidMaintainDeleted]
  [PXDefault]
  [PXUser]
  [PXUIField(Visibility = PXUIVisibility.Invisible)]
  [PXReferentialIntegrityCheck]
  public virtual Guid? PKID
  {
    get => this._PKID;
    set => this._PKID = value;
  }

  /// <summary>
  /// The login name for the user.
  /// This field is a key field.
  /// </summary>
  [PXDBString(256 /*0x0100*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Login", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Search<Users.username, Where<Users.isHidden, Equal<False>>>))]
  [PXFieldDescription]
  [PXReferentialIntegrityCheck]
  public virtual string Username
  {
    get => this._username;
    set => this._username = value?.Trim();
  }

  /// <summary>The domain name of the user login</summary>
  [PXString]
  [PXUIField(DisplayName = "Domain", Enabled = false)]
  public virtual string Domain
  {
    get
    {
      return this._domain ?? (this._domain = this.Logins == null ? string.Empty : Users.GetDomains((IEnumerable<Login>) this.Logins));
    }
  }

  public static string GetDomains(IEnumerable<Login> logins)
  {
    List<string> list = new List<string>();
    if (logins != null)
    {
      foreach (Login login in logins.Where<Login>((Func<Login, bool>) (login => !list.Contains(login.Domain))))
        list.Add(login.Domain);
    }
    return string.Join("; ", list.ToArray());
  }

  public ReadOnlyCollection<Login> Logins
  {
    get => this._logins;
    set
    {
      if (this._logins == value)
        return;
      this._logins = value;
      this._domain = (string) null;
    }
  }

  /// <inheritdoc cref="P:PX.SM.Users.FullName" />
  [PXString]
  [PXUIField(DisplayName = "Display Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXFormula(typeof (IsNull<Users.fullName, PX.Data.Empty>))]
  public virtual string DisplayName { get; set; }

  /// <summary>
  /// The user <see cref="P:PX.Data.Access.ActiveDirectory.User.SID">SID (Security Identifier)</see> from AD.
  /// </summary>
  [PXDBString(512 /*0x0200*/, IsFixed = false, IsUnicode = true)]
  public virtual string ExtRef { get; set; }

  /// <summary>
  /// This field indicates the location from where the user logged in.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.SM.PXUsersSourceListAttribute" /> class.
  /// The default value is <see cref="F:PX.SM.PXUsersSourceListAttribute.Application" />.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Source", Visibility = PXUIVisibility.SelectorVisible)]
  [PXUsersSourceList]
  [PXDefault(0)]
  public virtual int? Source { get; set; }

  /// <summary>The first name of the user.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "First Name")]
  [PXPersonalDataField]
  public virtual string FirstName
  {
    get => this._FirstName;
    set => this._FirstName = value;
  }

  /// <summary>
  /// This field indicates whether the user login was created from AD.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<Users.source, Equal<PXUsersSourceListAttribute.activeDirectory>>, PX.Data.True>, False>))]
  public virtual bool? IsADUser { get; set; }

  /// <summary>The last name of the user.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Last Name")]
  [PXPersonalDataField]
  public virtual string LastName
  {
    get => this._LastName;
    set => this._LastName = value;
  }

  /// <summary>
  /// The full name of the user as a concatenation <see cref="P:PX.SM.Users.FirstName" /> and <see cref="P:PX.SM.Users.LastName" />.
  /// </summary>
  [PXUIField(DisplayName = "Full Name", Enabled = false)]
  [PXDependsOnFields(new System.Type[] {typeof (Users.lastName), typeof (Users.firstName)})]
  [PersonDisplayName(typeof (Users.lastName), typeof (Users.firstName))]
  [PXPersonalDataField]
  public virtual string FullName { get; set; }

  /// <summary>
  /// The name of the application for which the user was created.
  /// </summary>
  [PXDBString(32 /*0x20*/)]
  [PXDefault("/")]
  [PXUIField]
  public virtual string ApplicationName
  {
    get => this._ApplicationName;
    set => this._ApplicationName = value;
  }

  /// <summary>The email of the user.</summary>
  [PXDBEmail]
  [PXDefault]
  [PXUIField(DisplayName = "Email", Visibility = PXUIVisibility.SelectorVisible)]
  [PXUIRequired(typeof (Where<Users.source, NotEqual<PXUsersSourceListAttribute.activeDirectory>, And<Users.username, PX.Data.IsNotNull>>))]
  [PXPersonalDataField]
  public virtual string Email
  {
    get => this._Email != null ? this._Email.Trim() : (string) null;
    set => this._Email = value?.Trim();
  }

  /// <summary>The phone number of the user.</summary>
  [PXDBString(50)]
  [PXUIField(DisplayName = "Phone")]
  [PXPhoneValidation("+# (###) ###-#### Ext:####")]
  [PXPersonalDataField]
  [PXPhone]
  public virtual string Phone
  {
    get => this._Phone;
    set => this._Phone = value;
  }

  /// <summary>Additional information about this user.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Comment")]
  public virtual string Comment
  {
    get => this._Comment;
    set => this._Comment = value;
  }

  /// <summary>The type of the user login.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.EP.EPLoginType.LoginTypeID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "User Type", Visibility = PXUIVisibility.Visible)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual int? LoginTypeID { get; set; }

  /// <summary>The linked contact for the user.</summary>
  /// <value>
  /// Corresponds to the <see cref="!:Contact.ContactID" /> field.
  /// </value>
  [PXInt]
  [PXUIField(DisplayName = "Linked Entity")]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual int? ContactID
  {
    get => this._ContactID;
    set => this._ContactID = value;
  }

  /// <summary>
  /// This field indicates whether the system needs to automatically generate the password automatically for this user.
  /// </summary>
  /// <value>
  /// The default value is <see langword="false" /> (the user has to input the password manually).
  /// </value>
  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Generate Password")]
  public virtual bool? GeneratePassword
  {
    get => this._GeneratePassword;
    set => this._GeneratePassword = value;
  }

  /// <summary>The password of the user.</summary>
  [PXDBUserPassword]
  [PXUIField(DisplayName = "Password")]
  [PXDefault]
  [PXUIRequired(typeof (Where<Users.source, Equal<PXUsersSourceListAttribute.application>>))]
  public virtual string Password
  {
    get => this._Password;
    set => this._Password = value;
  }

  /// <exclude />
  [PXString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Old Password")]
  public virtual string OldPassword
  {
    get => this._OldPassword;
    set => this._OldPassword = value;
  }

  /// <exclude />
  [PXString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "New Password", Required = true)]
  public virtual string NewPassword
  {
    get => this._NewPassword;
    set => this._NewPassword = value;
  }

  /// <exclude />
  [PXString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault("", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Confirm Password", Required = true)]
  public virtual string ConfirmPassword
  {
    get => this._ConfirmPassword;
    set => this._ConfirmPassword = value;
  }

  /// <summary>
  /// This field indicates whether the password can be recovered.
  /// </summary>
  /// <value>
  /// The default value is <see langword="true" />.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Allow Password Recovery")]
  public virtual bool? AllowPasswordRecovery
  {
    get => this._AllowPasswordRecovery;
    set => this._AllowPasswordRecovery = value;
  }

  /// <summary>The question for recovering the password.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Password Recovery Question")]
  public virtual string PasswordQuestion
  {
    get => this._PasswordQuestion;
    set => this._PasswordQuestion = value;
  }

  /// <summary>
  /// The answer to the question for recovering the password.
  /// </summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Password Recovery Answer")]
  public virtual string PasswordAnswer
  {
    get => this._PasswordAnswer;
    set => this._PasswordAnswer = value;
  }

  /// <summary>The link for recovering the password.</summary>
  [PXString]
  [PXUIField(DisplayName = "Password Recovery Link")]
  public virtual string RecoveryLink
  {
    get => this._RecoveryLink;
    set => this._RecoveryLink = value;
  }

  /// <summary>The code name for two-factor authentication.</summary>
  [PXString]
  [PXUIField(Visible = false, Enabled = false, Visibility = PXUIVisibility.Invisible)]
  public virtual string TwoFactorCode { get; set; }

  /// <summary>
  /// This field indicates that the user will have to change the password during next authentication.
  /// </summary>
  /// <value>
  /// The default value is <see langword="false" />.
  /// </value>
  [PXDBBool]
  [PXUIField(DisplayName = "Force User to Change Password on Next Login")]
  [PXDefault(false)]
  public virtual bool? PasswordChangeOnNextLogin
  {
    get => this._PasswordChangeOnNextLogin;
    set => this._PasswordChangeOnNextLogin = value;
  }

  /// <summary>
  /// This field indicates whether the user can change the password.
  /// </summary>
  /// <value>
  /// The default value is <see langword="true" />.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Allow Password Changes")]
  public virtual bool? PasswordChangeable
  {
    get => this._PasswordChangeable;
    set => this._PasswordChangeable = value;
  }

  /// <summary>This field indicates whether the password can expire.</summary>
  /// <value>
  /// The default value is <see langword="true" />.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Password Never Expires")]
  public virtual bool? PasswordNeverExpires
  {
    get => this._PasswordNeverExpires;
    set => this._PasswordNeverExpires = value;
  }

  /// <exclude />
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsHidden { get; set; }

  /// <summary>The field indicates that login was activated.</summary>
  /// <value>
  /// The default value is <see langword="true" />.
  /// </value>
  [PXDBBool]
  [PXUIField(DisplayName = "Activate Account")]
  [PXDefault(true)]
  [PXFormula(typeof (Switch<Case<Where<Users.source, Equal<PXUsersSourceListAttribute.activeDirectory>>, PX.Data.True>, Users.isApproved>))]
  [PXPersonalDataField]
  public virtual bool? IsApproved { get; set; }

  /// <summary>The field indicates that login is pending activation.</summary>
  [PXDBBool]
  public virtual bool? IsPendingActivation { get; set; }

  /// <summary>The field indicates that login is guest account.</summary>
  /// <value>
  /// The default value is <see langword="false" />.
  /// </value>
  [PXDBBool]
  [PXUIField(DisplayName = "Guest Account", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(false)]
  public virtual bool? Guest { get; set; }

  /// <exclude />
  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? IsAssigned { get; set; }

  /// <summary>The date and time of the user's last activity.</summary>
  [PXDBDate(UseTimeZone = true)]
  [PXUIField(DisplayName = "Last Activity Date")]
  public virtual System.DateTime? LastActivityDate
  {
    get => this._LastActivityDate;
    set => this._LastActivityDate = value;
  }

  /// <summary>The date and time of user's last login.</summary>
  [PXDBDate(PreserveTime = true, UseTimeZone = true)]
  [PXUIField(DisplayName = "Last Login Date")]
  [PXPersonalDataField]
  public virtual System.DateTime? LastLoginDate
  {
    get => this._LastLoginDate;
    set => this._LastLoginDate = value;
  }

  /// <summary>
  /// The date and time of the last time the user changed the password.
  /// </summary>
  [PXDBDate(PreserveTime = true, UseTimeZone = true)]
  [PXUIField(DisplayName = "Last Password Change Date")]
  public virtual System.DateTime? LastPasswordChangedDate
  {
    get => this._LastPasswordChangedDate;
    set => this._LastPasswordChangedDate = value;
  }

  /// <summary>The date and time of when the login was created.</summary>
  [PXDBDate(PreserveTime = true, UseTimeZone = true)]
  [PXUIField(DisplayName = "Account Creation Date")]
  public virtual System.DateTime? CreationDate
  {
    get => this._CreationDate;
    set => this._CreationDate = value;
  }

  /// <summary>This field indicates whether the user is online.</summary>
  /// <value>
  /// The default value is <see langword="false" />.
  /// </value>
  [PXDBBool]
  [PXUIField(DisplayName = "Is Online", Enabled = false)]
  [PXDefault(false)]
  public virtual bool? IsOnLine
  {
    get => this._IsOnLine;
    set => this._IsOnLine = value;
  }

  /// <summary>The date and time of the login lockout.</summary>
  [PXDBDate(PreserveTime = true, UseTimeZone = true)]
  public virtual System.DateTime? LockedOutDate
  {
    get => this._LockedOutDate;
    set => this._LockedOutDate = value;
  }

  /// <summary>
  /// This field indicates whether this login is temporarily locked out.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Temporarily Lock Out Account")]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXFormula(typeof (IsUserLocked<Users.lockedOutDate>))]
  public virtual bool? IsLockedOut
  {
    get => this._IsLockedOut;
    set => this._IsLockedOut = value;
  }

  /// <summary>
  /// This field indicates whether the Active Directory roles will be overridden with local roles.
  /// </summary>
  /// <value>
  /// The default value is <see langword="false" />.
  /// </value>
  [PXDBBool]
  [PXUIField(DisplayName = "Override Active Directory Roles with Local Roles")]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? OverrideADRoles { get; set; }

  /// <summary>The date and time of when the login was locked out.</summary>
  [PXDBDate(PreserveTime = true, UseTimeZone = true)]
  [PXUIField(DisplayName = "Last Lockout Date")]
  public virtual System.DateTime? LastLockedOutDate
  {
    get => this._LastLockedOutDate;
    set => this._LastLockedOutDate = value;
  }

  /// <summary>
  /// The number of unsuccessful attempts to enter the password after which the account should be locked.
  /// </summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Number of Unsuccessful Attempts To Enter Password")]
  public virtual int? FailedPasswordAttemptCount
  {
    get => this._FailedPasswordAttemptCount;
    set => this._FailedPasswordAttemptCount = value;
  }

  /// <summary>
  /// The date and time of when the user's attempts to login were exhausted.
  /// </summary>
  [PXDBDate]
  [PXUIField]
  public virtual System.DateTime? FailedPasswordAttemptWindowStart
  {
    get => this._FailedPasswordAttemptWindowStart;
    set => this._FailedPasswordAttemptWindowStart = value;
  }

  /// <summary>
  /// The number of unsuccessful attempts to enter the answer to the <see cref="P:PX.SM.Users.PasswordQuestion"></see>, after which the account should be locked.
  /// </summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Number of Unsuccessful Attempts To Enter Recovery Answer")]
  public virtual int? FailedPasswordAnswerAttemptCount
  {
    get => this._FailedPasswordAnswerAttemptCount;
    set => this._FailedPasswordAnswerAttemptCount = value;
  }

  /// <summary>
  /// The date and time of when the user attempts to answer the <see cref="P:PX.SM.Users.PasswordQuestion"></see> were exhausted.
  /// </summary>
  [PXDBDate]
  [PXUIField]
  public virtual System.DateTime? FailedPasswordAnswerAttemptWindowStart
  {
    get => this._FailedPasswordAnswerAttemptWindowStart;
    set => this._FailedPasswordAnswerAttemptWindowStart = value;
  }

  [PXDBBinary]
  public virtual byte[] GroupMask
  {
    get => this._GroupMask;
    set => this._GroupMask = value;
  }

  /// <summary>
  /// This fields indicates what is the current status of the user login.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.SM.Users.state.List" /> class.
  /// </value>
  [PXString(1, IsFixed = true, IsUnicode = false)]
  [PXUIField(DisplayName = "Status", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
  [Users.state.List]
  [PXDependsOnFields(new System.Type[] {typeof (Users.isApproved), typeof (Users.isPendingActivation), typeof (Users.isLockedOut), typeof (Users.isOnLine)})]
  [PXFormula(typeof (Switch<Case<Where<Current2<Users.isApproved>, NotEqual<PX.Data.True>>, Users.state.disabled, Case<Where<Current2<Users.isApproved>, Equal<PX.Data.True>, And<Current2<Users.isPendingActivation>, Equal<PX.Data.True>>>, Users.state.pendingActivation, Case<Where<Current2<Users.isApproved>, Equal<PX.Data.True>, And<Current<Users.isLockedOut>, Equal<PX.Data.True>>>, Users.state.tempLocked, Case<Where<Current2<Users.isOnLine>, Equal<PX.Data.True>>, Users.state.online, Case<Where<Current2<Users.isApproved>, Equal<PX.Data.True>, And<Current2<Users.isOnLine>, NotEqual<PX.Data.True>>>, Users.state.active>>>>>, Users.state.disabled>), Persistent = true)]
  public virtual string State { get; set; }

  /// <exclude />
  [PXUnboundDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXBool]
  [PXUIField(DisplayName = "Included")]
  public virtual bool? Included
  {
    get => this._Included;
    set => this._Included = value;
  }

  [PXString]
  public virtual string ActivationID
  {
    get => this._ActivationID;
    set => this._ActivationID = value;
  }

  [PXNote(ShowInReferenceSelector = true)]
  [PXSearchable(65535 /*0xFFFF*/, "User: {0}", new System.Type[] {typeof (Users.username)}, new System.Type[] {typeof (Users.username)}, Line1Format = "{0}, {1}", Line1Fields = new System.Type[] {typeof (Users.firstName), typeof (Users.lastName)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (Users.email)})]
  public virtual Guid? NoteID { get; set; }

  /// <summary>The option of two-factor user authentication.</summary>
  /// <value>
  /// The field can have one of the values listed in the its PXIntListAttribute.
  /// The default value is 0 ("None").
  /// </value>
  [PXDBInt]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"None", "Required for Unknown Devices", "Required"})]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Two-Factor Authentication")]
  [PXMultiFactorType]
  public virtual int? MultiFactorType { get; set; }

  /// <summary>
  /// This field indicates whether the security preferences will be overridden.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Override Security Preferences")]
  [PXDefault(false)]
  public virtual bool? MultiFactorOverride { get; set; }

  /// <summary>
  /// The number of the allowed number of sessions for this user.
  /// </summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Max. Number of Concurrent Logins")]
  [PXDefault(3, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual int? AllowedSessions { get; set; }

  /// <summary>
  /// This field indicates whether the login with the password has to be forbidden.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Forbid Login with Password", Visible = false)]
  public virtual bool? ForbidLoginWithPassword { get; set; }

  /// <summary>
  /// This field indicates whether the local roles are overridden with OIDC (OpenID Connect) provider roles.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Roles from Provider Settings", Visible = false)]
  public virtual bool? OverrideLocalRolesWithOidcProviderRoles { get; set; }

  public void Fill(PX.Data.Access.ActiveDirectory.User user)
  {
    this.Username = user.Name.DomainLogin;
    this.PKID = user.ObjectGUID;
    this.AllowPasswordRecovery = new bool?(false);
    this.Comment = user.Comment;
    this.CreationDate = new System.DateTime?(user.CreationDate);
    this.Email = user.Email;
    this.FirstName = user.FirstName;
    this.LastName = user.LastName;
    this.FullName = user.DisplayName;
    this.Source = new int?(1);
    this.PasswordChangeOnNextLogin = new bool?(false);
    this.PasswordChangeable = new bool?(false);
    this.Guest = new bool?(false);
    this.ExtRef = user.SID;
    this.Logins = user.Name.Logins;
  }

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<Users>.By<Users.pKID>
  {
    public static Users Find(PXGraph graph, Guid? pkID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<Users>.By<Users.pKID>.FindBy(graph, (object) pkID, options);
    }
  }

  public class UK : PrimaryKeyOf<Users>.By<Users.username>
  {
    public static Users Find(PXGraph graph, string username, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<Users>.By<Users.username>.FindBy(graph, (object) username, options);
    }
  }

  public abstract class pKID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Users.pKID>
  {
  }

  public abstract class username : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.username>
  {
  }

  public abstract class domain : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.domain>
  {
  }

  public abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.displayName>
  {
  }

  public abstract class extRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.extRef>
  {
  }

  public abstract class source : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Users.source>
  {
  }

  public abstract class firstName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.firstName>
  {
  }

  public abstract class isADUser : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Users.isADUser>
  {
  }

  public abstract class lastName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.lastName>
  {
  }

  public abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.fullName>
  {
  }

  public abstract class applicationName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.applicationName>
  {
  }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.email>
  {
  }

  public abstract class phone : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.phone>
  {
  }

  public abstract class comment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.comment>
  {
  }

  public abstract class loginTypeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Users.loginTypeID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Users.contactID>
  {
  }

  public abstract class generatePassword : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Users.generatePassword>
  {
  }

  public abstract class password : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.password>
  {
  }

  public abstract class oldPassword : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.oldPassword>
  {
  }

  public abstract class newPassword : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.newPassword>
  {
  }

  public abstract class confirmPassword : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.confirmPassword>
  {
  }

  public abstract class allowPasswordRecovery : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Users.allowPasswordRecovery>
  {
  }

  public abstract class passwordQuestion : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.passwordQuestion>
  {
  }

  public abstract class passwordAnswer : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.passwordAnswer>
  {
  }

  public abstract class recoveryLink : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.recoveryLink>
  {
  }

  public abstract class twoFactorCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.twoFactorCode>
  {
  }

  public abstract class passwordChangeOnNextLogin : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Users.passwordChangeOnNextLogin>
  {
  }

  public abstract class passwordChangeable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Users.passwordChangeable>
  {
  }

  public abstract class passwordNeverExpires : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Users.passwordNeverExpires>
  {
  }

  public abstract class isHidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Users.isHidden>
  {
  }

  public abstract class isApproved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Users.isApproved>
  {
  }

  public abstract class isPendingActivation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Users.isPendingActivation>
  {
  }

  public abstract class guest : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Users.guest>
  {
  }

  public abstract class isAssigned : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Users.isAssigned>
  {
  }

  public abstract class lastActivityDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    Users.lastActivityDate>
  {
  }

  public abstract class lastLoginDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  Users.lastLoginDate>
  {
  }

  public abstract class lastPasswordChangedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    Users.lastPasswordChangedDate>
  {
  }

  public abstract class creationDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  Users.creationDate>
  {
  }

  public abstract class isOnLine : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Users.isOnLine>
  {
  }

  public abstract class lockedOutDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  Users.lockedOutDate>
  {
  }

  public abstract class isLockedOut : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Users.isLockedOut>
  {
  }

  public abstract class overrideADRoles : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Users.overrideADRoles>
  {
  }

  public abstract class lastLockedOutDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    Users.lastLockedOutDate>
  {
  }

  public abstract class failedPasswordAttemptCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Users.failedPasswordAttemptCount>
  {
  }

  public abstract class failedPasswordAttemptWindowStart : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    Users.failedPasswordAttemptWindowStart>
  {
  }

  public abstract class failedPasswordAnswerAttemptCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Users.failedPasswordAnswerAttemptCount>
  {
  }

  public abstract class failedPasswordAnswerAttemptWindowStart : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    Users.failedPasswordAnswerAttemptWindowStart>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Users.groupMask>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.state>
  {
    public const string NotCreated = "N";
    public const string Online = "O";
    public const string PendingActivation = "P";
    public const string Disabled = "D";
    public const string Active = "A";
    public const string TempLocked = "L";

    public class List : PXStringListAttribute
    {
      public List()
        : base(new string[6]{ "N", "O", "P", "D", "A", "L" }, new string[6]
        {
          "Not Created",
          "Online",
          "Pending Activation",
          "Disabled",
          "Active",
          "Temporarily Locked"
        })
      {
      }
    }

    public class online : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Users.state.online>
    {
      public online()
        : base("O")
      {
      }
    }

    public class pendingActivation : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      Users.state.pendingActivation>
    {
      public pendingActivation()
        : base("P")
      {
      }
    }

    public class disabled : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Users.state.disabled>
    {
      public disabled()
        : base("D")
      {
      }
    }

    public class active : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Users.state.active>
    {
      public active()
        : base("A")
      {
      }
    }

    public class tempLocked : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Users.state.tempLocked>
    {
      public tempLocked()
        : base("L")
      {
      }
    }
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Users.included>
  {
  }

  public abstract class activationID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Users.activationID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Users.noteID>
  {
  }

  public abstract class multiFactorType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Users.multiFactorType>
  {
  }

  public abstract class multiFactorOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Users.multiFactorOverride>
  {
  }

  public abstract class allowedSessions : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Users.allowedSessions>
  {
  }

  public abstract class forbidLoginWithPassword : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Users.forbidLoginWithPassword>
  {
  }

  public abstract class overrideLocalRolesWithOidcProviderRoles : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Users.overrideLocalRolesWithOidcProviderRoles>
  {
  }
}

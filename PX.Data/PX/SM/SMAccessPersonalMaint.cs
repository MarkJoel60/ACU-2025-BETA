// Decompiled with JetBrains decompiler
// Type: PX.SM.SMAccessPersonalMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using PX.Common;
using PX.Data;
using PX.Data.Auth;
using PX.Data.MultiFactorAuth;
using PX.Export.Authentication;
using PX.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;

#nullable disable
namespace PX.SM;

public class SMAccessPersonalMaint : PXGraph<SMAccessPersonalMaint>
{
  public PXSelect<Users, Where<Users.pKID, Equal<Current<AccessInfo.userID>>>> UserProfile;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (UserPreferences)})]
  public PXSelect<UserPreferences, Where<UserPreferences.userID, Equal<Current<AccessInfo.userID>>>> UserPrefs;
  public PXSelectJoin<UploadFile, InnerJoin<NoteDoc, On<UploadFile.fileID, Equal<NoteDoc.fileID>>>, Where<NoteDoc.noteID, Equal<Current<Users.noteID>>>> AttachFiles;
  [PXHidden]
  public PXSelect<Branch> BranchBase;
  public PXSelect<UserPreferences, Where<UserPreferences.userID, Equal<Current<AccessInfo.userID>>>> UserPrefsSafe;
  public PXSelect<SMCalendarSettings, Where<SMCalendarSettings.userID, Equal<Current<AccessInfo.userID>>>> CalendarSettings;
  public PXSelect<PX.SM.SiteMap, Where<PX.SM.SiteMap.parentID, Equal<Argument<Guid?>>>, OrderBy<Asc<PX.SM.SiteMap.position>>> SiteMap;
  public PXSelect<EMailAccount, Where<EMailAccount.userID, Equal<Current<AccessInfo.userID>>>> EMailAccounts;
  public PXSelect<LocaleFormat> LocaleFormats;
  public PXSelect<UserLocaleFormat> UserLocaleFormats;
  public PXFilter<PasswordsInfo> Passwords;
  public PXFilter<NewEmailInfo> NewEmail;
  public PXFilter<NewAnswerInfo> NewAnswer;
  protected bool doCopyLocaleSettings;
  public PXSave<Users> SaveUsers;
  public PXCancel<Users> CancelUsers;
  public PXAction<Users> ChangePassword;
  public PXAction<Users> ChangeEmail;
  public PXAction<Users> ChangeSecretAnswer;
  public PXAction<Users> GetCalendarSyncUrl;
  public PXAction<Users> GenerateOneTimeCodes;
  public PXAction<Users> ResetTimeZone;
  public PXAction<Users> ManifestHelp;
  public PXAction<Users> ViewEMailAccount;

  [InjectDependency]
  protected IMultiFactorService _multiFactorService { get; set; }

  [InjectDependency]
  private IExternalAuthenticationService _externalAuthenticationService { get; set; }

  [InjectDependency]
  private IOptions<ExternalAuthenticationOptions> _externalAuthenticationOptions { get; set; }

  [InjectDependency]
  private IUserManagementService _userManagementService { get; set; }

  [InjectDependency]
  private IUserOrganizationService UserOrganizationService { get; set; }

  [InjectDependency]
  private IHttpContextAccessor _httpContextAccessor { get; set; }

  [InjectDependency]
  private LinkGenerator LinkGenerator { get; set; }

  public SMAccessPersonalMaint()
  {
    this.UserProfile.Cache.AllowInsert = false;
    this.EnsureCalendarSettings();
  }

  internal virtual IEnumerable sitemap([PXGuid] Guid? NodeID)
  {
    bool needRoot = false;
    if (!NodeID.HasValue)
    {
      NodeID = new Guid?(Guid.Empty);
      needRoot = true;
    }
    return SMAccessPersonalMaint.EnumNodes(NodeID.Value, needRoot);
  }

  public static IEnumerable EnumNodes(Guid nodeId, bool needRoot)
  {
    PXSiteMapNode siteMapNodeFromKey = PXSiteMap.Provider.FindSiteMapNodeFromKey(nodeId);
    if (needRoot)
    {
      yield return (object) SMAccessPersonalMaint.CreateSiteMap(siteMapNodeFromKey);
    }
    else
    {
      foreach (PXSiteMapNode childNode in (IEnumerable<PXSiteMapNode>) PXSiteMap.Provider.GetChildNodes(siteMapNodeFromKey))
        yield return (object) SMAccessPersonalMaint.CreateSiteMap(childNode);
    }
  }

  public static PX.SM.SiteMap CreateSiteMap(PXSiteMapNode node)
  {
    return new PX.SM.SiteMap()
    {
      NodeID = new Guid?(node.NodeID),
      ParentID = new Guid?(node.ParentID),
      ScreenID = node.ScreenID,
      Title = node.Title,
      Url = node.Url
    };
  }

  internal void sitecache([PXDBGuid(false)] ref Guid? ID)
  {
    if (ID.HasValue)
      return;
    ID = new Guid?(Guid.Empty);
  }

  public PX.SM.SiteMap siteMap
  {
    get
    {
      return (PX.SM.SiteMap) this.SiteMap[new object[1]
      {
        (object) Guid.Empty
      }];
    }
  }

  internal IEnumerable localeFormats()
  {
    SMAccessPersonalMaint graph = this;
    Users current = graph.UserProfile.Current;
    if (current != null && current.PKID.HasValue)
    {
      LocaleFormat localeFormat1 = graph.LocaleFormats.Current;
      if (localeFormat1 == null)
      {
        localeFormat1 = (LocaleFormat) PXSelectBase<LocaleFormat, PXSelectJoin<LocaleFormat, InnerJoin<UserLocaleFormat, On<UserLocaleFormat.formatID, Equal<LocaleFormat.formatID>>>, Where<UserLocaleFormat.userID, Equal<Current<Users.pKID>>, And<UserLocaleFormat.localeName, Equal<currentCulture>>>>.Config>.SelectSingleBound((PXGraph) graph, (object[]) null, (object[]) null);
        if (localeFormat1 == null)
        {
          LocaleFormat localeFormat2 = new LocaleFormat();
          localeFormat1 = graph.LocaleFormats.Insert(localeFormat2);
          graph.LocaleFormats.Cache.IsDirty = false;
          graph.UserLocaleFormats.Insert(new UserLocaleFormat()
          {
            UserID = current.PKID,
            LocaleName = PXLocalesProvider.GetCurrentLocale(),
            FormatID = localeFormat1.FormatID
          });
          graph.UserLocaleFormats.Cache.IsDirty = false;
        }
      }
      yield return (object) localeFormat1;
    }
  }

  internal void Users_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    Users row = e.Row as Users;
    if (string.IsNullOrEmpty(row.Username))
      sender.RaiseExceptionHandling<Users.username>(e.Row, (object) row.Username, (Exception) new PXSetPropertyException("The name cannot be empty."));
    if (!(row.Username != this.Accessinfo.UserName))
      return;
    this.UpdateUserNameInRole(row);
  }

  internal void Users_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Update || e.TranStatus != PXTranStatus.Completed)
      return;
    this.Accessinfo.UserName = ((Users) e.Row).Username;
    this.Accessinfo.DisplayName = ((Users) e.Row).DisplayName;
  }

  protected internal virtual void Users_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    Users row1 = (Users) e.Row;
    int? source = row1.Source;
    int num1 = 0;
    bool isEnabled = source.GetValueOrDefault() == num1 & source.HasValue;
    PXUIFieldAttribute.SetEnabled(sender, e.Row, isEnabled);
    PXUIFieldAttribute.SetEnabled<Users.displayName>(sender, e.Row, false);
    PXCache cache1 = sender;
    object row2 = e.Row;
    bool? passwordChangeable;
    int num2;
    if (isEnabled)
    {
      passwordChangeable = row1.PasswordChangeable;
      num2 = passwordChangeable.Value ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetEnabled<Users.newPassword>(cache1, row2, num2 != 0);
    PXCache cache2 = sender;
    object row3 = e.Row;
    int num3;
    if (isEnabled)
    {
      passwordChangeable = row1.PasswordChangeable;
      num3 = passwordChangeable.Value ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetEnabled<Users.confirmPassword>(cache2, row3, num3 != 0);
    PXAction<Users> changePassword = this.ChangePassword;
    int num4;
    if (isEnabled)
    {
      passwordChangeable = row1.PasswordChangeable;
      num4 = passwordChangeable.Value ? 1 : 0;
    }
    else
      num4 = 0;
    changePassword.SetEnabled(num4 != 0);
  }

  internal void Users_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    bool flag = false;
    foreach (string field in (List<string>) sender.Fields)
    {
      if (field != "NewPassword" && field != "OldPassword" && field != "ConfirmPassword" && !object.Equals(sender.GetValue(e.Row, field), sender.GetValue(e.OldRow, field)))
      {
        flag = true;
        break;
      }
    }
    sender.IsDirty = flag;
  }

  protected virtual void _(Events.RowSelected<EMailAccount> e)
  {
    if (e.Row == null)
      return;
    e.Cache.AdjustUI((object) e.Row).ForAllFields((System.Action<PXUIFieldAttribute>) (_ => _.Enabled = false)).For<EMailAccount.isActive>((System.Action<PXUIFieldAttribute>) (_ => _.Enabled = true));
  }

  protected internal virtual void UserPreferences_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if ((UserPreferences) e.Row == null)
      return;
    bool isVisible = PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+IntelligentTextCompletion");
    PXUIFieldAttribute.SetVisible<UserPreferences.enableSmartSuggest>(sender, e.Row, isVisible);
  }

  internal void UserPreferences_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    LocaleInfo.SetTimeZone(PXTimeZoneInfo.FindSystemTimeZoneById(((UserPreferences) e.Row).TimeZone) ?? PXTimeZoneInfo.FindSystemTimeZoneById(this.GetUserTimeZoneId()));
  }

  protected void UserPreferences_UserID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this.Accessinfo.UserID;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<Branch.branchID, Where<MatchWithBranch<Branch.branchID>>>), new System.Type[] {typeof (Branch.branchCD), typeof (Branch.roleName)}, DescriptionField = typeof (Branch.branchCD))]
  [PXUIField(DisplayName = "Default Branch")]
  protected virtual void UserPreferences_DefBranchID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXUIField(Visible = false, Visibility = PXUIVisibility.Invisible)]
  protected virtual void Branch_BranchID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Branch", Visibility = PXUIVisibility.SelectorVisible)]
  protected virtual void Branch_BranchCD_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(64 /*0x40*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Role", Visibility = PXUIVisibility.SelectorVisible)]
  protected virtual void Branch_RoleName_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [EMailAccount.authenticationMethod.List(EMailAccount.authenticationMethod.ListType.WithPlugIn)]
  protected virtual void _(
    Events.CacheAttached<EMailAccount.authenticationMethod> e)
  {
  }

  internal void LocaleFormat_templateLocale_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs args)
  {
    LocaleFormat row = args.Row as LocaleFormat;
    string str = args.NewValue == null ? (string) null : args.NewValue.ToString();
    this.doCopyLocaleSettings = string.IsNullOrEmpty(row.TemplateLocale);
    if (this.doCopyLocaleSettings || string.IsNullOrEmpty(str))
      return;
    this.doCopyLocaleSettings = true;
    if (this.LocaleFormats.Ask("Warning", "Please confirm that you want to update the format settings of the current locale with the system locale defaults. Otherwise, original settings will be preserved.", MessageButtons.YesNo) != WebDialogResult.Yes)
      return;
    this.doCopyLocaleSettings = true;
  }

  public override void Persist()
  {
    bool flag = false;
    if (this.UserPrefs.Current != null)
    {
      if (this.UserPrefs.Cache.GetOriginal((object) this.UserPrefs.Current) is UserPreferences original)
      {
        bool? disableSuggest1 = original.DisableSuggest;
        bool? disableSuggest2 = this.UserPrefs.Current.DisableSuggest;
        if (disableSuggest1.GetValueOrDefault() == disableSuggest2.GetValueOrDefault() & disableSuggest1.HasValue == disableSuggest2.HasValue)
          goto label_4;
      }
      flag = true;
    }
label_4:
    base.Persist();
    if (!flag)
      return;
    PXPageCacheUtils.InvalidateCachedPages();
    PXDatabase.SelectTimeStamp();
  }

  internal void LocaleFormat_templateLocale_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs args)
  {
    LocaleFormat row = args.Row as LocaleFormat;
    if (!this.doCopyLocaleSettings || string.IsNullOrEmpty(row.TemplateLocale))
      return;
    CultureInfo cultureInfo = CultureInfo.GetCultureInfo(row.TemplateLocale);
    row.NumberDecimalSeporator = PXNumberSeparatorListAttribute.Encode(cultureInfo.NumberFormat.NumberDecimalSeparator);
    row.NumberGroupSeparator = PXNumberSeparatorListAttribute.Encode(cultureInfo.NumberFormat.NumberGroupSeparator);
    row.DateLongPattern = PXNumberSeparatorListAttribute.Encode(cultureInfo.DateTimeFormat.LongDatePattern);
    row.DateShortPattern = PXNumberSeparatorListAttribute.Encode(cultureInfo.DateTimeFormat.ShortDatePattern);
    row.DateTimePattern = PXNumberSeparatorListAttribute.Encode(cultureInfo.DateTimeFormat.FullDateTimePattern);
    row.TimeLongPattern = PXNumberSeparatorListAttribute.Encode(cultureInfo.DateTimeFormat.LongTimePattern);
    row.TimeShortPattern = PXNumberSeparatorListAttribute.Encode(cultureInfo.DateTimeFormat.ShortTimePattern);
    row.AMDesignator = PXNumberSeparatorListAttribute.Encode(cultureInfo.DateTimeFormat.AMDesignator);
    row.PMDesignator = PXNumberSeparatorListAttribute.Encode(cultureInfo.DateTimeFormat.PMDesignator);
  }

  internal void LocaleFormat_NumberDecimalSeporator_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row is LocaleFormat row && e.NewValue != null && PXLocalesProvider.CollationComparer.Equals(row.NumberGroupSeparator, e.NewValue.ToString()))
      throw new PXSetPropertyException("The Number Group and Number Decimal separators have to be different.");
  }

  internal void LocaleFormat_NumberGroupSeparator_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row is LocaleFormat row && e.NewValue != null && PXLocalesProvider.CollationComparer.Equals(row.NumberDecimalSeporator, e.NewValue.ToString()))
      throw new PXSetPropertyException("The Number Group and Number Decimal separators have to be different.");
  }

  [PXUIField(DisplayName = "Change Password", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public void changePassword()
  {
    if (this.Passwords.AskExt((PXView.InitializePanel) ((graph, viewname) =>
    {
      this.Passwords.Current.OldPassword = (string) null;
      this.Passwords.Current.NewPassword = (string) null;
      this.Passwords.Current.ConfirmPassword = (string) null;
    })) != WebDialogResult.OK || !this.Passwords.VerifyRequired() || !SMAccessPersonalMaint.IsValidPasswords((IUserValidationService) this._userManagementService, (PXCache<PasswordsInfo>) this.Passwords.Cache))
      return;
    this.changePasswordOK();
  }

  public void changePasswordOK(bool allowRefresh = true)
  {
    Users copy = (Users) this.UserProfile.Cache.CreateCopy((object) this.UserProfile.Current);
    copy.OldPassword = this.Passwords.Current.OldPassword;
    copy.Password = copy.NewPassword = this.Passwords.Current.NewPassword;
    copy.ConfirmPassword = this.Passwords.Current.ConfirmPassword;
    Access.SetPassword(this._userManagementService, false, false, copy);
    try
    {
      PXSelectUsers<Users>.SendPasswordInfo(copy);
    }
    finally
    {
      if (allowRefresh && this.UserProfile.Current.Username == this.Accessinfo.UserName)
        throw new PXRefreshException();
    }
  }

  [PXUIField(DisplayName = "Change Email", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public virtual void changeEmail()
  {
    if (this.NewEmail.AskExt((PXView.InitializePanel) ((graph, viewname) =>
    {
      this.NewEmail.Current.Email = (string) null;
      this.NewEmail.Current.Password = (string) null;
    })) != WebDialogResult.OK || !this.NewEmail.VerifyRequired() || !this.IsValidCurrentPassword<NewEmailInfo.password>())
      return;
    Users copy = (Users) this.UserProfile.Cache.CreateCopy((object) this.UserProfile.Current);
    copy.Email = this.NewEmail.Current.Email;
    this.UserProfile.Update(copy);
  }

  [PXUIField(DisplayName = "Change Answer", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public virtual void changeSecretAnswer()
  {
    if (this.NewAnswer.AskExt((PXView.InitializePanel) ((graph, viewname) =>
    {
      this.NewAnswer.Current.PasswordAnswer = (string) null;
      this.NewAnswer.Current.Password = (string) null;
    })) != WebDialogResult.OK || !this.NewAnswer.VerifyRequired() || !this.IsValidCurrentPassword<NewAnswerInfo.password>())
      return;
    this.ChangeSecretAnswerOK(false);
  }

  public virtual void ChangeSecretAnswerOK(bool updateQuestion)
  {
    Users copy = (Users) this.UserProfile.Cache.CreateCopy((object) this.UserProfile.Current);
    copy.PasswordAnswer = this.NewAnswer.Current.PasswordAnswer;
    if (updateQuestion)
      copy.PasswordQuestion = this.NewAnswer.Current.PasswordQuestion;
    (this._userManagementService.GetUser(copy.PKID.Value) ?? throw new PXSetPropertyException("The supplied username has not been found.")).ChangePasswordQuestionAndAnswer(this.NewAnswer.Current.Password, copy.PasswordQuestion, copy.PasswordAnswer);
  }

  [PXUIField(DisplayName = "Synchronization URL")]
  [PXButton(ImageKey = "DataEntry")]
  public IEnumerable getCalendarSyncUrl(PXAdapter adapter)
  {
    this.EnsureCalendarSettings();
    if (this.CalendarSettings != null && this.CalendarSettings.Current != null)
    {
      int num = (int) this.CalendarSettings.View.Ask((object) null, "Synchronization URL", this.GetSyncUrl(), MessageButtons.OK, MessageIcon.Information);
    }
    return adapter.Get();
  }

  private string GetSyncUrl()
  {
    HttpContext httpContext1 = this._httpContextAccessor.HttpContext;
    string str = this.CalendarSettings.Current.UrlGuid.ToString();
    string companyName = PXAccess.GetCompanyName();
    LinkGenerator linkGenerator = this.LinkGenerator;
    HttpContext httpContext2 = httpContext1;
    QueryString queryString1 = QueryString.Create((IEnumerable<KeyValuePair<string, string>>) new Dictionary<string, string>()
    {
      {
        "id",
        str
      },
      {
        "cid",
        companyName
      }
    });
    HostString? nullable1 = new HostString?();
    PathString? nullable2 = new PathString?();
    QueryString queryString2 = queryString1;
    FragmentString fragmentString = new FragmentString();
    return LinkGeneratorContentUrlExtensions.GetUriForContent(linkGenerator, httpContext2, "~/calendarSync.ics", (string) null, nullable1, nullable2, queryString2, fragmentString);
  }

  public bool ValidatePasswords()
  {
    return SMAccessPersonalMaint.IsValidPasswords((IUserValidationService) this._userManagementService, (PXCache<PasswordsInfo>) this.Passwords.Cache);
  }

  private static bool IsValidPasswords(
    IUserValidationService userValidationService,
    PXCache<PasswordsInfo> cache)
  {
    bool flag = false;
    PasswordsInfo current = (PasswordsInfo) cache.Current;
    if (!UserValidationServiceExtensions.CheckUserPassword(userValidationService, PXAccess.GetUserName(), current.OldPassword))
    {
      cache.RaiseExceptionHandling<PasswordsInfo.oldPassword>((object) current, (object) current.OldPassword, (Exception) new PXSetPropertyException("Incorrect password"));
      flag = true;
    }
    if (current.NewPassword != current.ConfirmPassword)
    {
      cache.RaiseExceptionHandling<PasswordsInfo.confirmPassword>((object) current, (object) current.ConfirmPassword, (Exception) new PXSetPropertyException("The entered password doesn't match the confirmation."));
      flag = true;
    }
    else
    {
      Exception exception = userValidationService.ValidatePasswordPolicy(current.NewPassword);
      if (exception != null)
      {
        cache.RaiseExceptionHandling<PasswordsInfo.newPassword>((object) current, (object) current.NewPassword, exception);
        flag = true;
      }
    }
    return !flag;
  }

  public bool IsValidCurrentPassword<TField>() where TField : IBqlField
  {
    PXCache cach = this.Caches[BqlCommand.GetItemType(typeof (TField))];
    object current = cach.Current;
    string newValue = (string) cach.GetValue<TField>(current);
    if (UserValidationServiceExtensions.CheckUserPassword((IUserValidationService) this._userManagementService, this.Accessinfo.UserName, newValue))
      return true;
    cach.RaiseExceptionHandling<TField>(current, (object) newValue, (Exception) new PXSetPropertyException("Incorrect password"));
    return false;
  }

  [PXUIField(DisplayName = "Reset To Default Time Zone", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton(Tooltip = "Reset To Calendar Time Zone")]
  public virtual IEnumerable resetTimeZone(PXAdapter adapter)
  {
    if (this.UserPrefs.Current == null)
      return adapter.Get();
    string defaultUserTimeZoneId = this.GetDefaultUserTimeZoneId();
    if (defaultUserTimeZoneId == null)
      return adapter.Get();
    UserPreferences copy = PXCache<UserPreferences>.CreateCopy(this.UserPrefs.Current);
    copy.TimeZone = defaultUserTimeZoneId;
    this.UserPrefs.Update(copy);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Help", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public virtual IEnumerable manifestHelp(PXAdapter adapter)
  {
    throw new PXRedirectToUrlException($"~/Main?ScreenId=ShowWiki&pageid={PXSiteMap.WikiProvider.GetWikiPageIDByPageName("OU_00_00_00").ToString()}", PXBaseRedirectException.WindowMode.NewWindow, "");
  }

  [PXUIField(DisplayName = "Generate Access Codes", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update, Visible = true)]
  [PXButton]
  public IEnumerable generateOneTimeCodes(PXAdapter adapter)
  {
    Users current = this.UserProfile.Current;
    if (current == null)
      return adapter.Get();
    this._multiFactorService.GenerateCodesAndShowReport(current.PKID.Value);
    return adapter.Get();
  }

  [PXUIField(Visible = false)]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable viewEMailAccount(PXAdapter adapter)
  {
    EMailAccount current = this.EMailAccounts.Current;
    if (current == null)
      return adapter.Get();
    PXRedirectHelper.TryRedirect(this.Caches[typeof (EMailAccount)], (object) current, "", PXRedirectHelper.WindowMode.New);
    return adapter.Get();
  }

  public string GetUserTimeZoneId(string login, string companyId)
  {
    using (new PXLoginScope(login + (string.IsNullOrEmpty(companyId) ? string.Empty : "@") + companyId, PXAccess.GetAdministratorRoles()))
    {
      SMAccessPersonalMaint instance = (SMAccessPersonalMaint) PXGraph.CreateInstance(this.GetType());
      instance.Load();
      return instance.GetUserTimeZoneId(login);
    }
  }

  public virtual int? GetDefaultBranchId(string username, string companyId)
  {
    if (string.IsNullOrEmpty(username))
      return new int?();
    PXSelectBase<UserPreferences, PXSelectJoin<UserPreferences, InnerJoin<Users, On<Users.pKID, Equal<UserPreferences.userID>>>, Where<Users.username, Equal<Required<Users.username>>>>.Config>.Clear((PXGraph) this);
    PXDatabase.ResetCredentials();
    using (new PXLoginScope(username + (string.IsNullOrEmpty(companyId) ? string.Empty : "@") + companyId, PXAccess.GetAdministratorRoles()))
    {
      int? result = new int?();
      BranchInfo[] array = this.UserOrganizationService.GetBranches(username).ToArray<BranchInfo>();
      if (array.Length != 0)
      {
        PXResultset<UserPreferences> pxResultset = PXSelectBase<UserPreferences, PXSelectJoin<UserPreferences, InnerJoin<Users, On<Users.pKID, Equal<UserPreferences.userID>>>, Where<Users.username, Equal<Required<Users.username>>>>.Config>.Select((PXGraph) this, (object) username);
        if (pxResultset != null && pxResultset.Count > 0)
          result = ((UserPreferences) pxResultset[0][typeof (UserPreferences)]).DefBranchID;
        if (!result.HasValue || Array.FindIndex<BranchInfo>(array, 0, (Predicate<BranchInfo>) (s => result.Equals((object) s.Id))) < 0)
          result = new int?(array[0].Id);
      }
      return result;
    }
  }

  public virtual int? GetDefaultBranchId()
  {
    PXSelectBase<UserPreferences, PXSelectJoin<UserPreferences, InnerJoin<Users, On<Users.pKID, Equal<UserPreferences.userID>>>, Where<Users.username, Equal<Required<Users.username>>>>.Config>.Clear((PXGraph) this);
    int? result = new int?();
    BranchInfo[] array = this.UserOrganizationService.GetBranches(this.Accessinfo.UserName).ToArray<BranchInfo>();
    if (array != null && array.Length != 0)
    {
      PXResultset<UserPreferences> pxResultset = PXSelectBase<UserPreferences, PXSelectJoin<UserPreferences, InnerJoin<Users, On<Users.pKID, Equal<UserPreferences.userID>>>, Where<Users.username, Equal<Required<Users.username>>>>.Config>.Select((PXGraph) this, (object) this.Accessinfo.UserName);
      if (pxResultset != null && pxResultset.Count > 0)
        result = ((UserPreferences) pxResultset[0][typeof (UserPreferences)]).DefBranchID;
      if (!result.HasValue || Array.FindIndex<BranchInfo>(array, 0, (Predicate<BranchInfo>) (s => result.Equals((object) s.Id))) < 0)
        result = new int?(array[0].Id);
    }
    return result;
  }

  public IEnumerable<KeyValuePair<int, string>> GetAvalableBranches(
    string username,
    string companyId)
  {
    if (!string.IsNullOrEmpty(username))
    {
      BranchInfo[] array;
      using (new PXLoginScope(username + (string.IsNullOrEmpty(companyId) ? string.Empty : "@") + companyId, PXAccess.GetAdministratorRoles()))
        array = this.UserOrganizationService.GetBranches(username).ToArray<BranchInfo>();
      BranchInfo[] branchInfoArray = array;
      for (int index = 0; index < branchInfoArray.Length; ++index)
      {
        BranchInfo branchInfo = branchInfoArray[index];
        yield return new KeyValuePair<int, string>(branchInfo.Id, branchInfo.Cd);
      }
      branchInfoArray = (BranchInfo[]) null;
    }
  }

  protected string GetDefaultUserTimeZoneId()
  {
    if (this.UserPrefs.Current == null)
      return (string) null;
    Users users = (Users) PXSelectBase<Users, PXSelect<Users>.Config>.Search<Users.pKID>((PXGraph) this, (object) this.UserPrefs.Current.UserID);
    return users == null ? (string) null : this.GetDefaultUserTimeZoneId(users.Username);
  }

  protected virtual string GetDefaultUserTimeZoneId(string username)
  {
    return ((PreferencesGeneral) PXSelectBase<PreferencesGeneral, PXSelect<PreferencesGeneral>.Config>.Select((PXGraph) this)).With<PreferencesGeneral, string>((Func<PreferencesGeneral, string>) (_ => _.TimeZone));
  }

  public virtual string GetUserTimeZoneId(string username)
  {
    PXResultset<UserPreferences> pxResultset = PXSelectBase<UserPreferences, PXSelectJoin<UserPreferences, InnerJoin<Users, On<Users.pKID, Equal<UserPreferences.userID>>>, Where<Users.username, Equal<Required<Users.username>>>>.Config>.Select((PXGraph) this, (object) username);
    return pxResultset == null || pxResultset.Count <= 0 ? (string) null : ((UserPreferences) pxResultset[0][typeof (UserPreferences)]).TimeZone;
  }

  private void EnsureCalendarSettings()
  {
    if (this.CalendarSettings == null)
      return;
    PXResultset<SMCalendarSettings> pxResultset = this.CalendarSettings.Select();
    if (pxResultset.Count > 0)
    {
      this.CalendarSettings.Current = (SMCalendarSettings) pxResultset[0];
    }
    else
    {
      this.CalendarSettings.Insert(new SMCalendarSettings());
      this.CalendarSettings.Cache.PersistInserted((object) this.CalendarSettings.Current);
      this.EnsureCalendarSettings();
    }
  }

  private void UpdateUserNameInRole(Users UpdatedUser)
  {
    UsersInRoles row = (UsersInRoles) this.Caches[typeof (UsersInRoles)].Delete((object) (UsersInRoles) PXSelectBase<UsersInRoles, PXSelect<UsersInRoles, Where<UsersInRoles.username, Equal<Required<UsersInRoles.username>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) this.Accessinfo.UserName));
    this.Caches[typeof (UsersInRoles)].PersistDeleted((object) row);
    this.Caches[typeof (UsersInRoles)].PersistInserted((object) (UsersInRoles) this.Caches[typeof (UsersInRoles)].Insert((object) new UsersInRoles()
    {
      Username = UpdatedUser.Username,
      ApplicationName = row.ApplicationName,
      Rolename = row.Rolename
    }));
  }

  protected string GetUserTimeZoneId()
  {
    if (this.UserPrefs.Current != null)
    {
      PXResultset<Users> pxResultset = PXSelectBase<Users, PXSelect<Users>.Config>.Search<Users.pKID>((PXGraph) this, (object) this.UserPrefs.Current.UserID);
      if (pxResultset != null && pxResultset.Count > 0)
        return this.GetUserTimeZoneId(((Users) pxResultset[0][typeof (Users)]).Username);
    }
    return (string) null;
  }

  private sealed class PXAuthLoginScope : IDisposable
  {
    private IPrincipal prev;

    public PXAuthLoginScope()
    {
      this.prev = HttpContext.Current.User;
      HttpContext.Current.User = (IPrincipal) new GenericPrincipal((IIdentity) new GenericIdentity(string.Empty), new string[0]);
    }

    void IDisposable.Dispose()
    {
      if (this.prev != null)
        HttpContext.Current.User = this.prev;
      PXDatabase.ResetCredentials();
    }
  }
}

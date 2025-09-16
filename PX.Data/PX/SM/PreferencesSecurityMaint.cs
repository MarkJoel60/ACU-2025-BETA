// Decompiled with JetBrains decompiler
// Type: PX.SM.PreferencesSecurityMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using PX.Api.SMS;
using PX.Data;
using PX.Data.BQL;
using PX.Data.MultiFactorAuth;
using PX.Export.Authentication;
using PX.Security;
using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

#nullable enable
namespace PX.SM;

public class PreferencesSecurityMaint : PXGraph<
#nullable disable
PreferencesSecurityMaint>
{
  internal bool SkipMultiFactorSwitchOnValidation;
  public PXSelect<PreferencesSecurity> Prefs;
  public PXSelect<PreferencesGlobal> PrefsGlobal;
  public PXSelect<PreferencesEmail> PrefsEmail;
  public PXSelect<PreferencesIdentityProvider> Identities;
  public PXFilter<PreferencesSecurityMaint.ConfirmAccessCode> ConfirmView;
  public PreferencesSecurityMaint.PreferencesSecuritySave Save;
  public PXCancel<PreferencesSecurity> Cancel;
  public PXAction<PreferencesSecurity> GenerateCodes;

  [InjectDependency]
  internal ISmsSender _smsSender { get; set; }

  [InjectDependency]
  internal IMultiFactorService _multiFactorService { get; set; }

  [InjectDependency]
  internal IOptions<ExternalAuthenticationOptions> _externalAuthenticationOptions { get; set; }

  [InjectDependency]
  internal ICurrentUserInformationProvider _currentUserInformationProvider { get; set; }

  [InjectDependency]
  private IRoleManagementService _roleManagementService { get; set; }

  [InjectDependency]
  private IOptionsSnapshot<FormsAuthenticationOptions> _formsAuthenticationOptions { get; set; }

  public PreferencesSecurityMaint()
  {
    this.Identities.AllowDelete = false;
    this.Identities.AllowInsert = false;
    this.PrefsGlobal.AllowDelete = false;
    this.PrefsGlobal.AllowInsert = false;
  }

  public IEnumerable identities()
  {
    PreferencesSecurityMaint graph = this;
    PXSelectBase<PreferencesIdentityProvider> select = (PXSelectBase<PreferencesIdentityProvider>) new PXSelect<PreferencesIdentityProvider, Where<PreferencesIdentityProvider.instanceKey, Equal<Required<PreferencesIdentityProvider.instanceKey>>>>((PXGraph) graph);
    bool flag = false;
    PXSelectBase<PreferencesIdentityProvider> pxSelectBase1 = select;
    object[] objArray1 = new object[1]
    {
      (object) graph._externalAuthenticationOptions.Value.GetInstanceKey()
    };
    foreach (PXResult<PreferencesIdentityProvider> pxResult in pxSelectBase1.Select(objArray1))
    {
      yield return (object) (PreferencesIdentityProvider) pxResult;
      flag = true;
    }
    if (!flag)
    {
      bool dirty = select.Cache.IsDirty;
      PXSelectBase<PreferencesIdentityProvider> pxSelectBase2 = select;
      object[] objArray2 = new object[1]
      {
        (object) graph._externalAuthenticationOptions.Value.GetDefaultInstanceKey()
      };
      foreach (PXResult<PreferencesIdentityProvider> pxResult in pxSelectBase2.Select(objArray2))
      {
        PreferencesIdentityProvider identityProvider = (PreferencesIdentityProvider) pxResult;
        identityProvider.InstanceKey = graph._externalAuthenticationOptions.Value.GetInstanceKey();
        identityProvider.Active = new bool?(false);
        identityProvider.ApplicationID = (string) null;
        identityProvider.ApplicationSecret = (string) null;
        identityProvider.Realm = (string) null;
        yield return select.Cache.Insert((object) identityProvider);
      }
      select.Cache.IsDirty = dirty;
    }
  }

  protected void PreferencesSecurity_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PreferencesSecurity row))
      return;
    PXCache cache1 = cache;
    PreferencesSecurity data1 = row;
    bool? nullable = row.IsPasswordDayAge;
    bool flag1 = true;
    int num1 = nullable.GetValueOrDefault() == flag1 & nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PreferencesSecurity.passwordDayAge>(cache1, (object) data1, num1 != 0);
    PXCache cache2 = cache;
    PreferencesSecurity data2 = row;
    nullable = row.IsPasswordMinLength;
    bool flag2 = true;
    int num2 = nullable.GetValueOrDefault() == flag2 & nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PreferencesSecurity.passwordMinLength>(cache2, (object) data2, num2 != 0);
    PXUIFieldAttribute.SetVisible<PreferencesSecurity.smsEnabled>(cache, (object) row, this._smsSender != null);
    string administratorRole = cache.GetValue<PreferencesSecurity.defaultMenuEditorRole>(e.Row)?.ToString();
    if (string.IsNullOrEmpty(administratorRole))
      administratorRole = PXAccess.GetAdministratorRole();
    if (this._roleManagementService.RoleExists(administratorRole))
      return;
    cache.RaiseExceptionHandling<PreferencesSecurity.defaultMenuEditorRole>(e.Row, (object) null, (Exception) new PXSetPropertyException("The selected role has been deleted from the system. Select another role.", PXErrorLevel.Warning));
  }

  protected void PreferencesSecurity_RowPersisting(PXCache cahce, PXRowPersistingEventArgs e)
  {
    PreferencesSecurity row = e.Row as PreferencesSecurity;
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert && (e.Operation & PXDBOperation.Delete) != PXDBOperation.Update || string.IsNullOrEmpty(row.PasswordRegexCheck) || !string.IsNullOrEmpty(row.PasswordRegexCheckMessage))
      return;
    cahce.RaiseExceptionHandling<PreferencesSecurity.passwordRegexCheckMessage>(e.Row, (object) null, (Exception) new PXException("An alert message can't be empty when a mask is filled."));
  }

  protected void PreferencesSecurity_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    int tranStatus = (int) e.TranStatus;
  }

  protected void PreferencesSecurity_PasswordRegexCheck_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    try
    {
      Regex regex = new Regex(e.NewValue.ToString());
    }
    catch (Exception ex)
    {
      PXSetPropertyException propertyException = new PXSetPropertyException(ex.Message);
    }
  }

  protected void PreferencesSecurity_PdfCertificateName_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row is PreferencesSecurity && !string.IsNullOrEmpty(e.NewValue as string) && !SitePolicy.ValidateCertificate((string) e.NewValue))
      throw new PXSetPropertyException("An encryption certificate cannot be created. Make sure that the certificate has been configured properly and the valid certificate file has been uploaded to the site.");
  }

  protected void PreferencesIdentityProvider_InstanceKey_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this._externalAuthenticationOptions.Value.GetInstanceKey();
  }

  protected virtual void _(
    Events.FieldSelecting<PreferencesGlobal, PreferencesGlobal.logoutTimeout> e)
  {
    if (e.Row == null)
      return;
    bool? preconfiguredTimeouts = e.Row.UsePreconfiguredTimeouts;
    bool flag = false;
    bool isVisible = preconfiguredTimeouts.GetValueOrDefault() == flag & preconfiguredTimeouts.HasValue;
    PXUIFieldAttribute.SetVisible<PreferencesGlobal.logoutTimeout>(e.Cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<PreferencesGlobal.preconfiguredLogoutTimeout>(e.Cache, (object) null, !isVisible);
  }

  protected virtual void _(Events.RowSelecting<PreferencesGlobal> e)
  {
    if (e.Row == null)
      return;
    int configuredTimeout = ((IOptions<FormsAuthenticationOptions>) this._formsAuthenticationOptions).Value.ConfiguredTimeout;
    e.Row.PreconfiguredLogoutTimeout = PreferencesSecurityMaint.FormatMinutes(configuredTimeout);
  }

  protected virtual void _(
    Events.FieldUpdating<PreferencesGlobal, PreferencesGlobal.logoutTimeout> e)
  {
    PreferencesSecurityMaint.CheckIfShouldSetAllTenantsWarning<PreferencesGlobal, PreferencesGlobal.logoutTimeout>(e);
  }

  protected virtual void _(
    Events.FieldUpdating<PreferencesGlobal, PreferencesGlobal.usePreconfiguredTimeouts> e)
  {
    PreferencesSecurityMaint.CheckIfShouldSetAllTenantsWarning<PreferencesGlobal, PreferencesGlobal.usePreconfiguredTimeouts>(e);
  }

  internal static string FormatMinutes(int totalNumberOfMinutes)
  {
    if (totalNumberOfMinutes <= 0)
      return "0 min";
    int num1 = totalNumberOfMinutes / 60;
    int num2 = totalNumberOfMinutes % 60;
    if (num1 > 0 && num2 > 0)
      return $"{num1} h {num2} min";
    return num1 > 0 ? $"{num1} h" : $"{num2} min";
  }

  private static void CheckIfShouldSetAllTenantsWarning<T, Field>(
    Events.FieldUpdating<T, Field> e,
    bool _ = true)
    where T : class, IBqlTable, new()
    where Field : class, IBqlField
  {
    if ((object) e.Row == null)
      return;
    bool flag = e.NewValue != e.OldValue;
    PXUIFieldAttribute.SetWarning<Field>(e.Cache, (object) null, flag ? "Note that your changes will be applied to all tenants of the instance." : (string) null);
  }

  [PXButton]
  [PXUIField(DisplayName = "Generate List of Access Codes", Visibility = PXUIVisibility.Visible)]
  public IEnumerable generateCodes(PXAdapter adapter)
  {
    this._multiFactorService.GenerateCodesAndShowReport(this.Accessinfo.UserID);
    return adapter.Get();
  }

  public override void Persist()
  {
    base.Persist();
    PXDatabase.Update(typeof (Users), (PXDataFieldParam) new PXDataFieldAssign<Users.multiFactorType>((object) ((PreferencesSecurity) this.Caches<PreferencesSecurity>().Current).MultiFactorAuthLevel), (PXDataFieldParam) new PXDataFieldRestrict<Users.multiFactorOverride>((object) false));
  }

  [Serializable]
  public class ConfirmAccessCode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString]
    [PXDefault]
    [PXUIField(DisplayName = "Enter access code")]
    public string AccessCode { get; set; }

    [PXString(IsUnicode = true)]
    [PXUIField(Enabled = false, DisplayName = "")]
    public string ConfirmText { get; set; }

    [PXString(IsUnicode = true)]
    [PXUIField(Enabled = false, DisplayName = "")]
    public string BackupText { get; set; }

    [PXString(IsUnicode = true)]
    [PXUIField(Enabled = false, DisplayName = "")]
    public string ConfigureApiText { get; set; }

    public abstract class accessCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PreferencesSecurityMaint.ConfirmAccessCode.accessCode>
    {
    }

    public abstract class confirmText : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PreferencesSecurityMaint.ConfirmAccessCode.confirmText>
    {
    }

    public abstract class backupText : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PreferencesSecurityMaint.ConfirmAccessCode.backupText>
    {
    }

    public abstract class configureApiText : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PreferencesSecurityMaint.ConfirmAccessCode.configureApiText>
    {
    }
  }

  public class PreferencesSecuritySave : PXSave<PreferencesSecurity>
  {
    public PreferencesSecuritySave(PXGraph graph, string name)
      : base(graph, name)
    {
    }

    public PreferencesSecuritySave(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }

    [PXUIField(DisplayName = "Save", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
    [PXSaveButton]
    protected override IEnumerable Handler(PXAdapter adapter)
    {
      PreferencesSecurityMaint graph = this._Graph as PreferencesSecurityMaint;
      PXCache cache = graph?.Prefs.Cache;
      if (cache != null && cache.Updated.OfType<PreferencesSecurity>().Any<PreferencesSecurity>((Func<PreferencesSecurity, bool>) (c => MultiFactorSwitchOn(cache, c))))
      {
        WebDialogResult? nullable = graph?.ConfirmView.AskExt((PXView.InitializePanel) ((g, v) =>
        {
          PXCache cache1 = g.Views[v].Cache;
          cache1.Clear();
          cache1.Insert();
          object current = cache1.Current;
          cache1.SetValue<PreferencesSecurityMaint.ConfirmAccessCode.confirmText>(current, (object) PXLocalizer.LocalizeFormat("During the first sign-in with two-factor authentication, you will receive an email with the access code for the mobile app. To be sure that you have properly specified your email address, please enter the test access code that has been sent to your email address, {0}.", (object) graph._currentUserInformationProvider.GetEmail()));
          cache1.SetValue<PreferencesSecurityMaint.ConfirmAccessCode.backupText>(current, (object) PXLocalizer.Localize("You can generate a list of access codes, so that if other methods of the second step are unavailable during two-factor authentication, you can use an access code from the list. Print or save the list of access codes to avoid losing access to your account."));
          cache1.SetValue<PreferencesSecurityMaint.ConfirmAccessCode.configureApiText>(current, (object) PXLocalizer.Localize("If integrated applications sign in with a user account's credentials, you need to turn off two-factor authentication for this user on the Users form."));
          cache1.IsDirty = false;
          if (!(g is PreferencesSecurityMaint preferencesSecurityMaint2))
            return;
          preferencesSecurityMaint2._multiFactorService.SendRegistrationPersistentCode();
        }), true);
        WebDialogResult webDialogResult = WebDialogResult.OK;
        if (!(nullable.GetValueOrDefault() == webDialogResult & nullable.HasValue))
          return adapter.Get();
        if (!graph._multiFactorService.CheckPersistentCode(graph.ConfirmView.Current.AccessCode))
        {
          PreferencesSecurityMaint.ConfirmAccessCode current = graph.ConfirmView.Current;
          graph.ConfirmView.Cache.RaiseExceptionHandling("AccessCode", (object) current, (object) current.AccessCode, (Exception) new PXSetPropertyException("Incorrect code. Please try again."));
          return adapter.Get();
        }
      }
      return base.Handler(adapter);

      static bool MultiFactorSwitchOn(PXCache pxCache, PreferencesSecurity updated)
      {
        if ((pxCache.Graph is PreferencesSecurityMaint graph1 ? (graph1.SkipMultiFactorSwitchOnValidation ? 1 : 0) : 0) != 0)
          return false;
        PreferencesSecurity original = pxCache.GetOriginal((object) updated) as PreferencesSecurity;
        int? multiFactorAuthLevel1 = updated.MultiFactorAuthLevel;
        int num1 = 0;
        if (!(multiFactorAuthLevel1.GetValueOrDefault() > num1 & multiFactorAuthLevel1.HasValue) || original == null)
          return false;
        int? multiFactorAuthLevel2 = original.MultiFactorAuthLevel;
        int num2 = 0;
        return multiFactorAuthLevel2.GetValueOrDefault() == num2 & multiFactorAuthLevel2.HasValue;
      }
    }
  }
}

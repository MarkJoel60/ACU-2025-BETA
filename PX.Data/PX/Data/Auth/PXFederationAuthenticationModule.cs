// Decompiled with JetBrains decompiler
// Type: PX.Data.Auth.PXFederationAuthenticationModule
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using PX.Data.Access.ActiveDirectory;
using PX.Export.Authentication;
using PX.SM;
using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.IdentityModel.Services.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

#nullable disable
namespace PX.Data.Auth;

internal class PXFederationAuthenticationModule : 
  WSFederationAuthenticationModule,
  IFederationAuthentication
{
  private readonly IActiveDirectoryProvider _activeDirectoryProvider;
  private readonly ExternalAuthenticationOptions _externalAuthenticationOptions;
  private Exception _initFederatedError;

  public PXFederationAuthenticationModule(
    IOptions<ExternalAuthenticationOptions> externalAuthenticationOptions,
    IActiveDirectoryProvider activeDirectoryProvider)
  {
    this._externalAuthenticationOptions = externalAuthenticationOptions.Value;
    this._activeDirectoryProvider = activeDirectoryProvider;
  }

  protected override void InitializeModule(HttpApplication context)
  {
    this.InitializePropertiesFromConfiguration();
  }

  internal void InitializeConfiguration()
  {
    try
    {
      this.FederationConfiguration = FederatedAuthentication.FederationConfiguration;
      this.InitializePropertiesFromConfiguration();
    }
    catch (Exception ex)
    {
      this._initFederatedError = ex;
    }
  }

  protected override void InitializePropertiesFromConfiguration()
  {
    PXFederationAuthenticationModule.AddCaseInsensitiveSettingsToFederationConfiguration(this.FederationConfiguration?.WsFederationConfiguration);
    base.InitializePropertiesFromConfiguration();
  }

  private static void AddCaseInsensitiveSettingsToFederationConfiguration(
    WsFederationConfiguration wsFederationConfiguration)
  {
    int? count = wsFederationConfiguration?.CustomAttributes?.Count;
    if (!count.HasValue || count.GetValueOrDefault() == 0)
      return;
    Dictionary<string, string> dictionary;
    try
    {
      dictionary = new Dictionary<string, string>((IDictionary<string, string>) wsFederationConfiguration.CustomAttributes, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }
    catch (Exception ex)
    {
      throw new InvalidOperationException("Duplicate settings in the Active Directory federation auth configuration", ex);
    }
    string str1;
    if (dictionary.TryGetValue("Issuer", out str1))
      wsFederationConfiguration.Issuer = str1;
    string str2;
    if (dictionary.TryGetValue("Reply", out str2))
      wsFederationConfiguration.Reply = str2;
    string str3;
    if (dictionary.TryGetValue("Freshness", out str3))
      wsFederationConfiguration.Freshness = str3;
    string str4;
    if (dictionary.TryGetValue("AuthenticationType", out str4))
      wsFederationConfiguration.AuthenticationType = str4;
    string str5;
    if (dictionary.TryGetValue("HomeRealm", out str5))
      wsFederationConfiguration.HomeRealm = str5;
    string str6;
    if (dictionary.TryGetValue("Policy", out str6))
      wsFederationConfiguration.Policy = str6;
    string str7;
    if (dictionary.TryGetValue("Realm", out str7))
      wsFederationConfiguration.Realm = str7;
    string str8;
    if (dictionary.TryGetValue("SignOutReply", out str8))
      wsFederationConfiguration.SignOutReply = str8;
    string str9;
    if (dictionary.TryGetValue("Request", out str9))
      wsFederationConfiguration.Request = str9;
    string str10;
    if (dictionary.TryGetValue("RequestPtr", out str10))
      wsFederationConfiguration.RequestPtr = str10;
    string str11;
    if (dictionary.TryGetValue("Resource", out str11))
      wsFederationConfiguration.Resource = str11;
    string str12;
    if (dictionary.TryGetValue("SignInQueryString", out str12))
      wsFederationConfiguration.SignInQueryString = str12;
    string str13;
    if (dictionary.TryGetValue("SignOutQueryString", out str13))
      wsFederationConfiguration.SignOutQueryString = str13;
    string str14;
    bool result1;
    if (dictionary.TryGetValue("RequireHttps", out str14) && bool.TryParse(str14, out result1))
      wsFederationConfiguration.RequireHttps = result1;
    string str15;
    bool result2;
    if (dictionary.TryGetValue("PassiveRedirectEnabled", out str15) && bool.TryParse(str15, out result2))
      wsFederationConfiguration.PassiveRedirectEnabled = result2;
    string str16;
    bool result3;
    if (!dictionary.TryGetValue("PersistentCookiesOnPassiveRedirects", out str16) || !bool.TryParse(str16, out result3))
      return;
    wsFederationConfiguration.PersistentCookiesOnPassiveRedirects = result3;
  }

  ClaimsPrincipal IFederationAuthentication.TryAuthenticate(
    HttpRequestWrapper wrapper,
    out string company)
  {
    company = wrapper.QueryString["_company_"];
    if (this._initFederatedError != null)
      return (ClaimsPrincipal) null;
    if (!this.IsSignInResponse((HttpRequestBase) wrapper) || !this.CanReadSignInResponse((HttpRequestBase) wrapper))
      return (ClaimsPrincipal) null;
    IEnumerable<ClaimsIdentity> claimsIdentities = (IEnumerable<ClaimsIdentity>) FederatedAuthentication.FederationConfiguration.IdentityConfiguration.SecurityTokenHandlers.ValidateToken(this.GetSecurityToken(this.GetSignInResponseMessage((HttpRequestBase) wrapper)));
    PXFederationAuthenticationModule.AddClaimToCollectionIfNotEmpty(claimsIdentities, "_company_", company);
    PXFederationAuthenticationModule.AddClaimToCollectionIfNotEmpty(claimsIdentities, "_locale_", wrapper.QueryString["_locale_"]);
    ClaimsPrincipal principal = FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthenticationManager.Authenticate(wrapper.RawUrl, new ClaimsPrincipal(claimsIdentities));
    if (this._activeDirectoryProvider.IsAzureActiveDirectoryProvider())
      return principal;
    if (!this._externalAuthenticationOptions.ClaimsAuthEnabled() && !this._activeDirectoryProvider.IsClaimActiveDirectoryProvider())
      return (ClaimsPrincipal) null;
    string name = (string) null;
    if (company == null && PXDatabase.Companies != null && ((IEnumerable<string>) PXDatabase.Companies).Any<string>())
    {
      foreach (string company1 in PXDatabase.Companies)
      {
        bool flag = false;
        try
        {
          using (ExternalAuthenticationModule.GetAdminLoginScopeWithCompany(company1))
          {
            bool isNewUser;
            name = PXFederationAuthenticationModule.TrySyncClaimUser(company1, principal, out isNewUser);
            if (isNewUser)
              flag = true;
          }
        }
        catch
        {
          continue;
        }
        if (company == null)
          company = company1;
        if (!flag)
          break;
      }
    }
    else
    {
      bool flag = false;
      using (ExternalAuthenticationModule.GetAdminLoginScope(company))
      {
        bool isNewUser;
        name = PXFederationAuthenticationModule.TrySyncClaimUser(company, principal, out isNewUser);
        if (isNewUser)
          flag = true;
      }
      if (flag && PXDatabase.Companies != null && PXDatabase.Companies.Length > 1)
      {
        foreach (string company2 in PXDatabase.Companies)
        {
          if (!string.Equals(company2, company, StringComparison.InvariantCultureIgnoreCase))
          {
            try
            {
              using (ExternalAuthenticationModule.GetAdminLoginScopeWithCompany(company2))
                PXFederationAuthenticationModule.TrySyncClaimUser(company2, principal, out bool _);
            }
            catch
            {
            }
          }
        }
      }
    }
    return name == null || principal.Identity.Name == name ? principal : new ClaimsPrincipal((IIdentity) new ClaimsIdentity((IIdentity) new GenericIdentity(name), principal.Claims));
  }

  private static string TrySyncClaimUser(
    string company,
    ClaimsPrincipal principal,
    out bool isNewUser)
  {
    string str1 = (string) null;
    string sid = (string) null;
    string firstName = (string) null;
    string lastName = (string) null;
    string str2 = (string) null;
    string displayName = (string) null;
    string email = (string) null;
    List<string> source1 = new List<string>();
    foreach (Claim claim in principal.Claims)
    {
      if (claim.Type.EndsWith("/surname", StringComparison.InvariantCultureIgnoreCase))
        lastName = claim.Value;
      else if (claim.Type.EndsWith("/givenname", StringComparison.InvariantCultureIgnoreCase))
        firstName = claim.Value;
      else if (claim.Type.EndsWith("/upn", StringComparison.InvariantCultureIgnoreCase))
        str1 = claim.Value;
      else if (claim.Type.EndsWith("/name", StringComparison.InvariantCultureIgnoreCase))
        str2 = claim.Value;
      else if (claim.Type.EndsWith("/emailaddress", StringComparison.InvariantCultureIgnoreCase))
        email = claim.Value;
      else if (claim.Type.EndsWith("/commonname", StringComparison.InvariantCultureIgnoreCase))
        displayName = claim.Value;
      else if (claim.Type.EndsWith("/primarysid", StringComparison.InvariantCultureIgnoreCase))
        sid = claim.Value;
      else if (claim.Type.EndsWith("/windowsaccountname", StringComparison.InvariantCultureIgnoreCase))
      {
        string str3 = claim.Value;
      }
      else if (claim.Type.EndsWith("/role", StringComparison.InvariantCultureIgnoreCase))
        source1.Add(claim.Value);
    }
    if (string.IsNullOrEmpty(str1))
      throw new PXException("The required claim '{0}' is missing. Please configure your ADFS server to pass through this claim.", new object[1]
      {
        (object) "upn"
      });
    if (string.IsNullOrEmpty(sid))
      throw new PXException("The required claim '{0}' is missing. Please configure your ADFS server to pass through this claim.", new object[1]
      {
        (object) "primarysid"
      });
    if (string.IsNullOrEmpty(email))
      throw new PXException("The required claim '{0}' is missing. Please configure your ADFS server to pass through this claim.", new object[1]
      {
        (object) "emailaddress"
      });
    if (source1 == null || source1.Count <= 0)
      throw new PXException("The required claim '{0}' is missing. Please configure your ADFS server to pass through this claim.", new object[1]
      {
        (object) "role"
      });
    string domain;
    if (!string.IsNullOrEmpty(str2))
      domain = str1.Replace(str2, string.Empty).Trim('@');
    else
      domain = !str1.Contains("@") ? string.Empty : str1.Substring(str1.LastIndexOf("@") + 1);
    PX.Data.Access.ActiveDirectory.User user = new PX.Data.Access.ActiveDirectory.User(sid, new Guid?(Guid.NewGuid()), new NameInfo(str1, str1, (IEnumerable<Login>) new Login[1]
    {
      new Login(str2, domain)
    }), displayName, firstName, lastName, email);
    bool flag1 = false;
    bool flag2 = false;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>((PXDataField) new PXDataField<Users.isApproved>(), (PXDataField) new PXDataField<Users.overrideADRoles>(), (PXDataField) new PXDataFieldValue<Users.pKID>((object) PXActiveDirectorySyncMembershipProvider.TrySynchronizeInternalUser(str1, user, out isNewUser))))
    {
      flag1 = ((int) pxDataRecord.GetBoolean(0) ?? 1) != 0;
      flag2 = pxDataRecord.GetBoolean(1).GetValueOrDefault();
    }
    if (!flag1)
      throw new PXException("Your account is disabled. Please contact your system administrator.");
    List<string> source2 = new List<string>();
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<RoleClaims>((PXDataField) new PXDataField<RoleClaims.role>(), (PXDataField) new PXDataField<RoleClaims.groupID>()))
    {
      string str4 = pxDataRecord.GetString(0);
      string str5 = pxDataRecord.GetString(1);
      if (!source2.Contains<string>(str4, (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase) && source1.Contains<string>(str5, (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase))
        source2.Add(str4);
    }
    if (!flag2 && source2.Count <= 0)
      throw new PXException("User '{0}' doesn't have associated roles in the system.", new object[1]
      {
        (object) str1
      });
    bool flag3 = false;
    List<string> stringList = new List<string>();
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<UsersInRoles>((PXDataField) new PXDataField<UsersInRoles.rolename>(), (PXDataField) new PXDataFieldValue<UsersInRoles.applicationName>((object) "/"), (PXDataField) new PXDataFieldValue<UsersInRoles.username>((object) user.Name.DomainLogin)))
        {
          string str6 = pxDataRecord.GetString(0);
          if (!source2.Contains(str6))
            stringList.Add(str6);
          else
            source2.Remove(str6);
          flag3 = true;
        }
        if (!flag2)
        {
          foreach (string str7 in source2)
            PXDatabase.Insert<UsersInRoles>((PXDataFieldAssign) new PXDataFieldAssign<UsersInRoles.applicationName>((object) "/"), (PXDataFieldAssign) new PXDataFieldAssign<UsersInRoles.rolename>((object) str7), (PXDataFieldAssign) new PXDataFieldAssign<UsersInRoles.username>((object) user.Name.DomainLogin), (PXDataFieldAssign) new PXDataFieldAssign<UsersInRoles.createdByID>((object) user.ObjectGUID), (PXDataFieldAssign) new PXDataFieldAssign<UsersInRoles.createdByScreenID>((object) "00000000"), (PXDataFieldAssign) new PXDataFieldAssign<UsersInRoles.createdDateTime>((object) System.DateTime.Now), (PXDataFieldAssign) new PXDataFieldAssign<UsersInRoles.lastModifiedByID>((object) user.ObjectGUID), (PXDataFieldAssign) new PXDataFieldAssign<UsersInRoles.lastModifiedByScreenID>((object) "00000000"), (PXDataFieldAssign) new PXDataFieldAssign<UsersInRoles.lastModifiedDateTime>((object) System.DateTime.Now));
          foreach (string str8 in stringList)
            PXDatabase.Delete<UsersInRoles>((PXDataFieldRestrict) new PXDataFieldRestrict<UsersInRoles.applicationName>((object) "/"), (PXDataFieldRestrict) new PXDataFieldRestrict<UsersInRoles.rolename>((object) str8), (PXDataFieldRestrict) new PXDataFieldRestrict<UsersInRoles.username>((object) user.Name.DomainLogin));
        }
        else
        {
          if (!flag3 && !string.IsNullOrEmpty(company))
            throw new PXException("You are not allowed to log in to the tenant {0}. Please contact your system administrator.", new object[1]
            {
              (object) company
            });
          if (!flag3)
            throw new PXException("User '{0}' doesn't have associated roles in the system.", new object[1]
            {
              (object) str1
            });
        }
        transactionScope.Complete();
      }
    }
    PXAccess.Provider.Clear();
    PXDatabase.SelectTimeStamp();
    return str1;
  }

  private static void AddClaimToCollectionIfNotEmpty(
    IEnumerable<ClaimsIdentity> collection,
    string claimType,
    string value)
  {
    if (value == null)
      return;
    foreach (ClaimsIdentity claimsIdentity in collection)
      claimsIdentity.AddClaim(new Claim(claimType, value, (string) null, "Acumatica"));
  }

  string IFederationAuthentication.GetRedirectUrl(
    HttpRequest request,
    Dictionary<string, string> parameters)
  {
    if (this._initFederatedError != null)
      throw this._initFederatedError;
    SignInRequestMessage signInRequest = this.CreateSignInRequest("passive", request.RawUrl, true);
    signInRequest.Reply = this._externalAuthenticationOptions.GetAuthUrl(signInRequest.Reply ?? signInRequest.Realm, parameters);
    return signInRequest.WriteQueryString();
  }

  string IFederationAuthentication.GetSignOutQueryString(
    string redirectUrl,
    Dictionary<string, string> parameters)
  {
    if (this._initFederatedError != null)
      throw this._initFederatedError;
    SignOutRequestMessage outRequestMessage = new SignOutRequestMessage(new Uri(this.Issuer), this._externalAuthenticationOptions.GetReturnUrl(redirectUrl ?? this.Reply ?? this.Realm, parameters));
    outRequestMessage.SetParameter("wtrealm", this.Realm);
    return outRequestMessage.WriteQueryString();
  }
}

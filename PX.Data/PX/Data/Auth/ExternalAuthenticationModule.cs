// Decompiled with JetBrains decompiler
// Type: PX.Data.Auth.ExternalAuthenticationModule
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using PX.Data.Update;
using PX.Export.Authentication;
using PX.Security;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Monads;
using System.Security.Claims;
using System.Web;

#nullable disable
namespace PX.Data.Auth;

internal sealed class ExternalAuthenticationModule
{
  private readonly ExternalAuthenticationOptions _options;
  private readonly Exception _initExternalError;
  private readonly IFederationAuthentication _federationAuthentication;
  private readonly ProviderCollection _providers = new ProviderCollection();
  private readonly IExternalAuthenticationProvider[] _externalAuthenticationProviders;
  private readonly IUserValidationService _userValidationService;
  private readonly ILegacyCompanyService _legacyCompanyService;
  private readonly IDataProtectionProvider _dataProtectionProvider;
  private const string AdminLogin = "admin";

  public ExternalAuthenticationModule(
    IOptions<ExternalAuthenticationOptions> options,
    IEnumerable<IExternalAuthenticationProvider> externalAuthenticationProviders,
    IFederationAuthentication federationAuthentication,
    IUserValidationService userValidationService,
    ILegacyCompanyService legacyCompanyService,
    IDataProtectionProvider dataProtectionProvider)
  {
    this._federationAuthentication = federationAuthentication;
    this._userValidationService = userValidationService;
    this._legacyCompanyService = legacyCompanyService;
    this._dataProtectionProvider = dataProtectionProvider;
    this._options = options.Value;
    this._externalAuthenticationProviders = externalAuthenticationProviders.ToArray<IExternalAuthenticationProvider>();
  }

  internal ClaimsPrincipal Authenticate(HttpContext context, out string company)
  {
    if (this._initExternalError != null)
      throw this._initExternalError;
    HttpRequestWrapper request = new HttpRequestWrapper(context.Request);
    Tuple<ClaimsPrincipal, string> tuple = this.TryAuthenticateExternal(context, request).FirstOrDefault<Tuple<ClaimsPrincipal, string>>((Func<Tuple<ClaimsPrincipal, string>, bool>) (p => p.Item1 != null));
    if (tuple == null)
    {
      company = string.Empty;
      return (ClaimsPrincipal) null;
    }
    ClaimsPrincipal claimsPrincipal = tuple.Item1;
    company = tuple.Item2;
    return claimsPrincipal;
  }

  private IEnumerable<Tuple<ClaimsPrincipal, string>> TryAuthenticateExternal(
    HttpContext context,
    HttpRequestWrapper request)
  {
    string company1;
    yield return Tuple.Create<ClaimsPrincipal, string>(this._federationAuthentication.TryAuthenticate(request, out company1), company1);
    IExternalAuthenticationProvider[] authenticationProviderArray = this._externalAuthenticationProviders;
    for (int index = 0; index < authenticationProviderArray.Length; ++index)
    {
      ClaimsPrincipal claimsPrincipal = authenticationProviderArray[index].TryAuthenticate((HttpRequestBase) request);
      if (claimsPrincipal != null)
      {
        string company2 = this._legacyCompanyService.ExtractCompany(claimsPrincipal.Identity?.Name);
        yield return Tuple.Create<ClaimsPrincipal, string>(claimsPrincipal, company2);
      }
    }
    authenticationProviderArray = (IExternalAuthenticationProvider[]) null;
  }

  internal Users GetUser(HttpRequestBase request, string provider, string id, string key)
  {
    Users users = (Users) null;
    bool flag = false;
    PXResult<Users, UserIdentity, PreferencesIdentityProvider> pxResult = (PXResult<Users, UserIdentity, PreferencesIdentityProvider>) null;
    PXGraph graph = new PXGraph();
    Guid result;
    if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out result))
    {
      flag = true;
      pxResult = (PXResult<Users, UserIdentity, PreferencesIdentityProvider>) PXSelectBase<Users, PXSelectJoin<Users, InnerJoin<UserIdentity, On<Users.pKID, Equal<UserIdentity.userID>>, InnerJoin<PreferencesIdentityProvider, On<UserIdentity.providerName, Equal<PreferencesIdentityProvider.providerName>>>>, Where<Users.pKID, Equal<Required<Users.pKID>>, And<PreferencesIdentityProvider.instanceKey, Equal<Required<PreferencesIdentityProvider.instanceKey>>, And<UserIdentity.providerName, Equal<Required<UserIdentity.providerName>>, And<PreferencesIdentityProvider.active, Equal<PX.Data.True>, And<UserIdentity.active, Equal<PX.Data.True>>>>>>>.Config>.SelectSingleBound(graph, (object[]) null, (object) result, (object) this._options.GetInstanceKey(), (object) provider).FirstOrDefault<PXResult<Users>>();
      if (pxResult != null)
        users = (Users) pxResult;
    }
    if (users == null && key != null)
    {
      pxResult = (PXResult<Users, UserIdentity, PreferencesIdentityProvider>) PXSelectBase<Users, PXSelectJoin<Users, InnerJoin<UserIdentity, On<Users.pKID, Equal<UserIdentity.userID>>, InnerJoin<PreferencesIdentityProvider, On<UserIdentity.providerName, Equal<PreferencesIdentityProvider.providerName>>>>, Where<UserIdentity.userKey, Equal<Required<UserIdentity.userKey>>, And<PreferencesIdentityProvider.instanceKey, Equal<Required<PreferencesIdentityProvider.instanceKey>>, And<UserIdentity.providerName, Equal<Required<UserIdentity.providerName>>, And<PreferencesIdentityProvider.active, Equal<PX.Data.True>, And<UserIdentity.active, Equal<PX.Data.True>>>>>>>.Config>.SelectSingleBound(graph, (object[]) null, (object) key, (object) this._options.GetInstanceKey(), (object) provider).FirstOrDefault<PXResult<Users>>();
      if (pxResult != null)
        users = (Users) pxResult;
    }
    if (flag && pxResult != null && users != null)
    {
      UserIdentity userIdentity = (UserIdentity) pxResult;
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        try
        {
          PXDatabase.Update<UserIdentity>((PXDataFieldParam) new PXDataFieldAssign<UserIdentity.userKey>(PXDbType.NVarChar, (object) key), (PXDataFieldParam) new PXDataFieldRestrict<UserIdentity.providerName>(PXDbType.NVarChar, (object) provider), (PXDataFieldParam) new PXDataFieldRestrict<UserIdentity.userID>(PXDbType.UniqueIdentifier, (object) userIdentity.UserID), (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed);
        }
        catch (PXDbOperationSwitchRequiredException ex)
        {
          PXDatabase.Insert<UserIdentity>((PXDataFieldAssign) new PXDataFieldAssign<UserIdentity.active>(PXDbType.Bit, (object) userIdentity.Active), (PXDataFieldAssign) new PXDataFieldAssign<UserIdentity.providerName>(PXDbType.NVarChar, (object) userIdentity.ProviderName), (PXDataFieldAssign) new PXDataFieldAssign<UserIdentity.userID>(PXDbType.UniqueIdentifier, (object) userIdentity.UserID), (PXDataFieldAssign) new PXDataFieldAssign<UserIdentity.userKey>(PXDbType.NVarChar, (object) key));
        }
        PXDatabase.Update<UserIdentity>((PXDataFieldParam) new PXDataFieldAssign<UserIdentity.userKey>(PXDbType.NVarChar, (object) null), (PXDataFieldParam) new PXDataFieldRestrict<UserIdentity.providerName>(PXDbType.NVarChar, (object) provider), (PXDataFieldParam) new PXDataFieldRestrict<UserIdentity.userKey>(PXDbType.NVarChar, (object) key), (PXDataFieldParam) new PXDataFieldRestrict<UserIdentity.userID>(PXDbType.UniqueIdentifier, new int?(), (object) userIdentity.UserID, PXComp.NE));
        List<Tuple<Guid, bool>> tupleList = new List<Tuple<Guid, bool>>();
        foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<UserIdentity>((PXDataField) new PXDataField<UserIdentity.userID>(), (PXDataField) new PXDataField<UserIdentity.active>(), (PXDataField) new PXDataFieldValue<UserIdentity.providerName>(PXDbType.NVarChar, (object) provider), (PXDataField) new PXDataFieldValue<UserIdentity.userKey>(PXDbType.NVarChar, (object) key), (PXDataField) new PXDataFieldValue<UserIdentity.userID>(PXDbType.UniqueIdentifier, new int?(), (object) userIdentity.UserID, PXComp.NE)))
        {
          Guid? guid = pxDataRecord.GetGuid(0);
          bool? boolean = pxDataRecord.GetBoolean(1);
          if (guid.HasValue)
            tupleList.Add(Tuple.Create<Guid, bool>(guid.Value, boolean.GetValueOrDefault()));
        }
        foreach (Tuple<Guid, bool> tuple in tupleList)
        {
          try
          {
            PXDatabase.Update<UserIdentity>((PXDataFieldParam) new PXDataFieldAssign<UserIdentity.userKey>(PXDbType.NVarChar, (object) null), (PXDataFieldParam) new PXDataFieldRestrict<UserIdentity.providerName>(PXDbType.NVarChar, (object) provider), (PXDataFieldParam) new PXDataFieldRestrict<UserIdentity.userID>(PXDbType.UniqueIdentifier, (object) tuple.Item1), (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed);
          }
          catch (PXDbOperationSwitchRequiredException ex)
          {
            PXDatabase.Insert<UserIdentity>((PXDataFieldAssign) new PXDataFieldAssign<UserIdentity.active>(PXDbType.Bit, (object) tuple.Item2), (PXDataFieldAssign) new PXDataFieldAssign<UserIdentity.providerName>(PXDbType.NVarChar, (object) provider), (PXDataFieldAssign) new PXDataFieldAssign<UserIdentity.userID>(PXDbType.UniqueIdentifier, (object) tuple.Item1), (PXDataFieldAssign) new PXDataFieldAssign<UserIdentity.userKey>(PXDbType.NVarChar, (object) key));
          }
        }
        transactionScope.Complete();
      }
      users = (Users) pxResult;
    }
    return MaybeObjects.If<Users>(users, (Func<Users, bool>) (u => this._userValidationService.IsValidForLogin(request, u)));
  }

  internal static PXLoginScope GetAdminLoginScope(string company)
  {
    return company != null ? ExternalAuthenticationModule.GetAdminLoginScopeWithCompany(company) : ExternalAuthenticationModule.GetAdminLoginScopeWithoutCompany();
  }

  internal static PXLoginScope GetAdminLoginScopeWithoutCompany()
  {
    return new PXLoginScope("admin", Array.Empty<string>());
  }

  internal static PXLoginScope GetAdminLoginScopeWithCompany(string company)
  {
    return new PXLoginScope("admin@" + company, Array.Empty<string>());
  }

  internal static bool IsFeatureEnabledForProvider(
    string providerName,
    out string disabledFeature,
    string company)
  {
    string neededForProvider = ExternalAuthenticationModule.GetFeatureNeededForProvider(providerName);
    string[] strArray;
    if (string.IsNullOrEmpty(company))
      strArray = ((IEnumerable<CompanyInfo>) PXDatabase.SelectCompanies()).Where<CompanyInfo>((Func<CompanyInfo, bool>) (ci => !ci.System && !string.IsNullOrEmpty(ci.LoginName))).Select<CompanyInfo, string>((Func<CompanyInfo, string>) (ci => ci.LoginName)).ToArray<string>();
    else
      strArray = new string[1]{ company };
    foreach (string company1 in strArray)
    {
      using (ExternalAuthenticationModule.GetAdminLoginScopeWithCompany(company1))
      {
        if (ExternalAuthenticationModule.IsFeatureInstalled(neededForProvider))
        {
          disabledFeature = (string) null;
          return true;
        }
      }
    }
    disabledFeature = neededForProvider;
    return false;
  }

  private static string GetFeatureNeededForProvider(string providerName)
  {
    return "ActiveDirectoryAndOtherExternalSSO";
  }

  private static bool IsFeatureInstalled(string featureName)
  {
    return PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+" + featureName);
  }

  internal static bool IsProviderFeatureInstalled(string providerName)
  {
    return ExternalAuthenticationModule.IsFeatureInstalled(ExternalAuthenticationModule.GetFeatureNeededForProvider(providerName));
  }
}

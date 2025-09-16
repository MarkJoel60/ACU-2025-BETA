// Decompiled with JetBrains decompiler
// Type: PX.Data.Auth.IdentityProviderDefinition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Auth;

[Obsolete("Required only for outlook plugin authentication")]
internal class IdentityProviderDefinition : IPrefetchable<string>, IPXCompanyDependent
{
  private static readonly IReadOnlyDictionary<string, IdentityProviderDefinition.IdentityProvider> Empty = (IReadOnlyDictionary<string, IdentityProviderDefinition.IdentityProvider>) new Dictionary<string, IdentityProviderDefinition.IdentityProvider>();

  private static IReadOnlyDictionary<string, IdentityProviderDefinition.IdentityProvider> Get(
    string instanceKey)
  {
    return PXDatabase.GetSlot<IdentityProviderDefinition, string>("ProviderSlot", instanceKey, typeof (PreferencesIdentityProvider))?.Providers ?? IdentityProviderDefinition.Empty;
  }

  void IPrefetchable<string>.Prefetch(string instanceKey)
  {
    this.Providers = (IReadOnlyDictionary<string, IdentityProviderDefinition.IdentityProvider>) PXDatabase.SelectMulti<PreferencesIdentityProvider>((PXDataField) new PXDataField<PreferencesIdentityProvider.instanceKey>(), (PXDataField) new PXDataField<PreferencesIdentityProvider.providerName>(), (PXDataField) new PXDataField<PreferencesIdentityProvider.active>(), (PXDataField) new PXDataField<PreferencesIdentityProvider.realm>(), (PXDataField) new PXDataField<PreferencesIdentityProvider.applicationID>(), (PXDataField) new PXDataField<PreferencesIdentityProvider.applicationSecret>(), (PXDataField) new PXDataFieldValue<PreferencesIdentityProvider.instanceKey>((object) instanceKey)).Select<PXDataRecord, IdentityProviderDefinition.IdentityProvider>((Func<PXDataRecord, IdentityProviderDefinition.IdentityProvider>) (row => new IdentityProviderDefinition.IdentityProvider()
    {
      Key = row.GetString(0),
      Name = row.GetString(1),
      Active = row.GetBoolean(2).GetValueOrDefault(),
      Realm = row.GetString(3),
      ApplicationID = row.GetString(4),
      ApplicationSecret = PXRSACryptStringAttribute.Decrypt(row.GetString(5))
    })).ToDictionary<IdentityProviderDefinition.IdentityProvider, string>((Func<IdentityProviderDefinition.IdentityProvider, string>) (prov => prov.Name));
  }

  private IReadOnlyDictionary<string, IdentityProviderDefinition.IdentityProvider> Providers { get; set; }

  internal static IdentityProviderDefinition.IdentityProvider GetProviderConfiguration(
    string instanceKey,
    string type,
    string company,
    bool silent = false)
  {
    if (company != null)
    {
      using (ExternalAuthenticationModule.GetAdminLoginScopeWithCompany(company))
        return Get(IdentityProviderDefinition.Get(instanceKey));
    }
    if (PXDatabase.Companies == null || PXDatabase.Companies.Length == 0)
    {
      using (ExternalAuthenticationModule.GetAdminLoginScopeWithoutCompany())
        return Get(IdentityProviderDefinition.Get(instanceKey));
    }
    foreach (string company1 in PXDatabase.Companies)
    {
      using (ExternalAuthenticationModule.GetAdminLoginScopeWithCompany(company1))
      {
        IReadOnlyDictionary<string, IdentityProviderDefinition.IdentityProvider> def = IdentityProviderDefinition.Get(instanceKey);
        if (def.Values.Any<IdentityProviderDefinition.IdentityProvider>((Func<IdentityProviderDefinition.IdentityProvider, bool>) (p => p.Name.OrdinalEquals(type) && p.Active)))
          return Get(def);
      }
    }
    if (silent)
      return (IdentityProviderDefinition.IdentityProvider) null;
    throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("{0} identity provider configuration is not found.", (object) type));

    IdentityProviderDefinition.IdentityProvider Get(
      IReadOnlyDictionary<string, IdentityProviderDefinition.IdentityProvider> def)
    {
      IdentityProviderDefinition.IdentityProvider identityProvider = (IdentityProviderDefinition.IdentityProvider) null;
      if (type != null && def.TryGetValue(type, out identityProvider) && identityProvider.Active && !string.IsNullOrEmpty(identityProvider.Realm))
        return identityProvider;
      if (silent)
        return (IdentityProviderDefinition.IdentityProvider) null;
      if (string.IsNullOrEmpty(type))
        throw new PXException("Identity provider is not specified.");
      if (identityProvider != null)
        throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("{0} identity provider is not enabled.", (object) type));
      throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("{0} identity provider configuration is not found.", (object) type));
    }
  }

  internal static bool HasActiveProvider(string instanceKey, string company)
  {
    if (company == null && PXDatabase.Companies != null && PXDatabase.Companies.Length > 1)
      throw new PXException("Tenant is not specified");
    using (ExternalAuthenticationModule.GetAdminLoginScope(company))
      return IdentityProviderDefinition.Get(instanceKey).Values.Any<IdentityProviderDefinition.IdentityProvider>((Func<IdentityProviderDefinition.IdentityProvider, bool>) (p => p.Active));
  }

  internal static string GetCurrentCompany(string instanceKey, string provider)
  {
    foreach (string company in PXDatabase.Companies)
    {
      using (ExternalAuthenticationModule.GetAdminLoginScopeWithCompany(company))
      {
        if (IdentityProviderDefinition.Get(instanceKey).Values.Any<IdentityProviderDefinition.IdentityProvider>((Func<IdentityProviderDefinition.IdentityProvider, bool>) (p => p.Name.OrdinalEquals(provider) && p.Active)))
          return company;
      }
    }
    return (string) null;
  }

  internal static IEnumerable<IdentityProviderDefinition.IdentityProvider> GetProviders(
    string instanceKey)
  {
    if (PXDatabase.Companies == null || PXDatabase.Companies.Length == 0)
    {
      using (ExternalAuthenticationModule.GetAdminLoginScopeWithoutCompany())
        return EnumerableExtensions.Distinct<IdentityProviderDefinition.IdentityProvider, string>(IdentityProviderDefinition.Get(instanceKey).Values.Where<IdentityProviderDefinition.IdentityProvider>((Func<IdentityProviderDefinition.IdentityProvider, bool>) (p => p.Active && !string.IsNullOrEmpty(p.Realm) && !p.Name.StartsWith("ExchangeIdentity", StringComparison.OrdinalIgnoreCase) && ExternalAuthenticationModule.IsProviderFeatureInstalled(p.Name))), (Func<IdentityProviderDefinition.IdentityProvider, string>) (p => p.Name));
    }
    List<IdentityProviderDefinition.IdentityProvider> source = new List<IdentityProviderDefinition.IdentityProvider>();
    foreach (string company in PXDatabase.Companies)
    {
      using (ExternalAuthenticationModule.GetAdminLoginScopeWithCompany(company))
      {
        IReadOnlyDictionary<string, IdentityProviderDefinition.IdentityProvider> readOnlyDictionary = IdentityProviderDefinition.Get(instanceKey);
        source.AddRange(readOnlyDictionary.Values.Where<IdentityProviderDefinition.IdentityProvider>((Func<IdentityProviderDefinition.IdentityProvider, bool>) (p => ExternalAuthenticationModule.IsProviderFeatureInstalled(p.Name))));
      }
    }
    return EnumerableExtensions.Distinct<IdentityProviderDefinition.IdentityProvider, string>(source.Where<IdentityProviderDefinition.IdentityProvider>((Func<IdentityProviderDefinition.IdentityProvider, bool>) (p => p.Active && !string.IsNullOrEmpty(p.Realm) && !p.Name.StartsWith("ExchangeIdentity", StringComparison.OrdinalIgnoreCase))), (Func<IdentityProviderDefinition.IdentityProvider, string>) (p => p.Name));
  }

  internal class IdentityProvider
  {
    public string Name { get; set; }

    public string Key { get; set; }

    public bool Active { get; set; }

    public string Realm { get; set; }

    public string ApplicationID { get; set; }

    public string ApplicationSecret { get; set; }
  }
}

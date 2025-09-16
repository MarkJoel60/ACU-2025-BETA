// Decompiled with JetBrains decompiler
// Type: PX.Objects.Cs.AdvancedAuthenticationRestriction.PreferencesSecurityMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.Cs.AdvancedAuthenticationRestriction;

public class PreferencesSecurityMaintExt : PXGraphExtension<PreferencesSecurityMaint>
{
  [InjectDependency]
  private IAdvancedAuthenticationRestrictor AdvancedAuthenticationRestrictor { get; set; }

  public IEnumerable identities()
  {
    return (IEnumerable) this.Base.identities().OfType<PreferencesIdentityProvider>().Where<PreferencesIdentityProvider>((Func<PreferencesIdentityProvider, bool>) (p => this.AdvancedAuthenticationRestrictor.IsAllowedProviderName(p.ProviderName)));
  }

  public virtual void _(Events.RowSelected<PreferencesIdentityProvider> e)
  {
    if (e?.Row == null || !this.AdvancedAuthenticationRestrictor.IsDeprecatedProvider(e.Row.ProviderName))
      return;
    ((PXSelectBase) this.Base.Identities).Cache.RaiseExceptionHandling<PreferencesIdentityProvider.providerName>((object) e.Row, (object) e.Row.ProviderName, (Exception) new PXSetPropertyException("The {0} identity provider can no longer be configured on this form. Use the OpenID Providers (SM303020) form to configure external identity providers.", (PXErrorLevel) 3, new object[1]
    {
      (object) e.Row.ProviderName
    }));
  }
}

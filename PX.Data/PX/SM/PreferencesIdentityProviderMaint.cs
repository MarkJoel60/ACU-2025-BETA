// Decompiled with JetBrains decompiler
// Type: PX.SM.PreferencesIdentityProviderMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using PX.Data;
using PX.Export.Authentication;
using System.Collections;

#nullable disable
namespace PX.SM;

public class PreferencesIdentityProviderMaint : PXGraph<PreferencesIdentityProviderMaint>
{
  public PXSelect<PreferencesIdentityProvider> Identities;
  public PXSave<PreferencesIdentityProvider> Save;
  public PXCancel<PreferencesIdentityProvider> Cancel;

  [InjectDependency]
  internal IOptions<ExternalAuthenticationOptions> _externalAuthenticationOptions { get; set; }

  public PreferencesIdentityProviderMaint()
  {
    this.Identities.AllowDelete = false;
    this.Identities.AllowInsert = false;
  }

  public IEnumerable identities()
  {
    PreferencesIdentityProviderMaint graph = this;
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

  protected void PreferencesIdentityProvider_InstanceKey_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this._externalAuthenticationOptions.Value.GetInstanceKey();
  }
}

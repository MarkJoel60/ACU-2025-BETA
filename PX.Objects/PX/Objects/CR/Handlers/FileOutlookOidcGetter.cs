// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Handlers.FileOutlookOidcGetter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Api.Outlook.Services;
using PX.Common;
using PX.Data;
using System.Web;

#nullable disable
namespace PX.Objects.CR.Handlers;

[PXInternalUseOnly]
public class FileOutlookOidcGetter : FileOutlookGetterBase
{
  protected override string ResourceAddinManifest => "PX.Objects.CR.Handlers.AddinManifestOidc.xml";

  protected override string OutputFileName => "OutlookAddinManifestMicrosoft365.xml";

  protected override string AdjustManifest(string manifest, HttpRequest request)
  {
    string clientId = this.GetClientId();
    return base.AdjustManifest(manifest, request).Replace("{apiHost}", this.GetApiHost(request)).Replace("{clientId}", clientId);
  }

  protected virtual string GetApiHost(HttpRequest request) => "api://" + this.GetHost(request);

  protected virtual string GetClientId()
  {
    return ServiceLocator.Current.GetInstance<IOutlookOidcProviderProvider>().TryGetCurrentProvider()?.ClientId ?? throw new PXInvalidOperationException("The OpenID provider is not specified for the Acumatica add-in for Outlook.");
  }
}

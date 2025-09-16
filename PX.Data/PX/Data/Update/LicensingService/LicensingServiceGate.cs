// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.LicensingService.LicensingServiceGate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

#nullable disable
namespace PX.Data.Update.LicensingService;

/// <remarks />
[GeneratedCode("System.Web.Services", "4.0.30319.17929")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[WebServiceBinding(Name = "ServiceSoap", Namespace = "http://acumatica.com/")]
public class LicensingServiceGate : SoapHttpClientProtocol
{
  private bool useDefaultCredentialsSetExplicitly;

  public LicensingServiceGate(string url)
  {
    this.Url = url;
    if (this.IsLocalFileSystemWebService(this.Url))
    {
      this.UseDefaultCredentials = true;
      this.useDefaultCredentialsSetExplicitly = false;
    }
    else
      this.useDefaultCredentialsSetExplicitly = true;
  }

  public new string Url
  {
    get => base.Url;
    set
    {
      if (this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value))
        base.UseDefaultCredentials = false;
      base.Url = value;
    }
  }

  public new bool UseDefaultCredentials
  {
    get => base.UseDefaultCredentials;
    set
    {
      base.UseDefaultCredentials = value;
      this.useDefaultCredentialsSetExplicitly = true;
    }
  }

  [SoapDocumentMethod("http://acumatica.com/GetLicense", RequestNamespace = "http://acumatica.com/", ResponseNamespace = "http://acumatica.com/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public LicenseInfo GetLicense(InstanceInfo info)
  {
    return (LicenseInfo) this.Invoke(nameof (GetLicense), new object[1]
    {
      (object) info
    })[0];
  }

  public new void CancelAsync(object userState) => base.CancelAsync(userState);

  private bool IsLocalFileSystemWebService(string url)
  {
    if (url == null || url == string.Empty)
      return false;
    Uri uri = new Uri(url);
    return uri.Port >= 1024 /*0x0400*/ && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0;
  }
}

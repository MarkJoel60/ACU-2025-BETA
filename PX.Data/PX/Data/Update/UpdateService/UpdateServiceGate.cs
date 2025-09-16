// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UpdateService.UpdateServiceGate
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
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.UpdateService;

/// <remarks />
[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[WebServiceBinding(Name = "ServiceSoap", Namespace = "http://acumatica.com/")]
internal class UpdateServiceGate : SoapHttpClientProtocol
{
  private bool useDefaultCredentialsSetExplicitly;

  /// <remarks />
  public UpdateServiceGate(string url)
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

  /// <remarks />
  [SoapDocumentMethod("http://acumatica.com/CheckUpdates", RequestNamespace = "http://acumatica.com/", ResponseNamespace = "http://acumatica.com/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public bool CheckUpdates(VersionInfo currentVersion)
  {
    return (bool) this.Invoke(nameof (CheckUpdates), new object[1]
    {
      (object) currentVersion
    })[0];
  }

  /// <remarks />
  [SoapDocumentMethod("http://acumatica.com/GetBranchesFiltered", RequestNamespace = "http://acumatica.com/", ResponseNamespace = "http://acumatica.com/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public BranchInfo[] GetBranchesFiltered(VersionInfo currentVersion, string key)
  {
    return (BranchInfo[]) this.Invoke(nameof (GetBranchesFiltered), new object[2]
    {
      (object) currentVersion,
      (object) key
    })[0];
  }

  /// <remarks />
  [SoapDocumentMethod("http://acumatica.com/GetBuilds", RequestNamespace = "http://acumatica.com/", ResponseNamespace = "http://acumatica.com/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public BuildInfo[] GetBuilds(VersionInfo currentVersion, string key)
  {
    return (BuildInfo[]) this.Invoke(nameof (GetBuilds), new object[2]
    {
      (object) currentVersion,
      (object) key
    })[0];
  }

  /// <remarks />
  [SoapDocumentMethod("http://acumatica.com/DownloadBuildPartially", RequestNamespace = "http://acumatica.com/", ResponseNamespace = "http://acumatica.com/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  [return: XmlElement(DataType = "base64Binary")]
  public byte[] DownloadBuildPartially(VersionInfo version, string key, int position)
  {
    return (byte[]) this.Invoke(nameof (DownloadBuildPartially), new object[3]
    {
      (object) version,
      (object) key,
      (object) position
    })[0];
  }

  private bool IsLocalFileSystemWebService(string url)
  {
    if (url == null || url == string.Empty)
      return false;
    Uri uri = new Uri(url);
    return uri.Port >= 1024 /*0x0400*/ && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0;
  }
}

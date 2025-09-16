// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UntypedService.Screen
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
namespace PX.Data.Update.UntypedService;

[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[WebServiceBinding(Name = "ScreenSoap", Namespace = "http://www.acumatica.com/untyped/")]
[XmlInclude(typeof (Filter[]))]
internal class Screen : SoapHttpClientProtocol
{
  private bool useDefaultCredentialsSetExplicitly;

  public Screen(string url)
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

  [SoapDocumentMethod("http://www.acumatica.com/untyped/Clear", RequestNamespace = "http://www.acumatica.com/untyped/", ResponseNamespace = "http://www.acumatica.com/untyped/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public void Clear(string screenID)
  {
    this.Invoke(nameof (Clear), new object[1]
    {
      (object) screenID
    });
  }

  [SoapDocumentMethod("http://www.acumatica.com/untyped/GetProcessStatus", RequestNamespace = "http://www.acumatica.com/untyped/", ResponseNamespace = "http://www.acumatica.com/untyped/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public ProcessResult GetProcessStatus(string screenID)
  {
    return (ProcessResult) this.Invoke(nameof (GetProcessStatus), new object[1]
    {
      (object) screenID
    })[0];
  }

  [SoapDocumentMethod("http://www.acumatica.com/untyped/GetScenario", RequestNamespace = "http://www.acumatica.com/untyped/", ResponseNamespace = "http://www.acumatica.com/untyped/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public Command[] GetScenario(string scenario)
  {
    return (Command[]) this.Invoke(nameof (GetScenario), new object[1]
    {
      (object) scenario
    })[0];
  }

  [SoapDocumentMethod("http://www.acumatica.com/untyped/GetSchema", RequestNamespace = "http://www.acumatica.com/untyped/", ResponseNamespace = "http://www.acumatica.com/untyped/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public Content GetSchema(string screenID)
  {
    return (Content) this.Invoke(nameof (GetSchema), new object[1]
    {
      (object) screenID
    })[0];
  }

  [SoapDocumentMethod("http://www.acumatica.com/untyped/SetSchema", RequestNamespace = "http://www.acumatica.com/untyped/", ResponseNamespace = "http://www.acumatica.com/untyped/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public void SetSchema(string screenID, Content schema)
  {
    this.Invoke(nameof (SetSchema), new object[2]
    {
      (object) screenID,
      (object) schema
    });
  }

  [SoapDocumentMethod("http://www.acumatica.com/untyped/Export", RequestNamespace = "http://www.acumatica.com/untyped/", ResponseNamespace = "http://www.acumatica.com/untyped/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  [return: XmlArrayItem("ArrayOfString")]
  [return: XmlArrayItem(NestingLevel = 1)]
  public string[][] Export(
    string screenID,
    Command[] commands,
    Filter[] filters,
    int topCount,
    bool includeHeaders,
    bool breakOnError)
  {
    return (string[][]) this.Invoke(nameof (Export), new object[6]
    {
      (object) screenID,
      (object) commands,
      (object) filters,
      (object) topCount,
      (object) includeHeaders,
      (object) breakOnError
    })[0];
  }

  [SoapDocumentMethod("http://www.acumatica.com/untyped/Import", RequestNamespace = "http://www.acumatica.com/untyped/", ResponseNamespace = "http://www.acumatica.com/untyped/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public ImportResult[] Import(
    string screenID,
    Command[] commands,
    Filter[] filters,
    [XmlArrayItem("ArrayOfString"), XmlArrayItem(NestingLevel = 1)] string[][] data,
    bool includedHeaders,
    bool breakOnError,
    bool breakOnIncorrectTarget)
  {
    return (ImportResult[]) this.Invoke(nameof (Import), new object[7]
    {
      (object) screenID,
      (object) commands,
      (object) filters,
      (object) data,
      (object) includedHeaders,
      (object) breakOnError,
      (object) breakOnIncorrectTarget
    })[0];
  }

  [SoapDocumentMethod("http://www.acumatica.com/untyped/Submit", RequestNamespace = "http://www.acumatica.com/untyped/", ResponseNamespace = "http://www.acumatica.com/untyped/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public Content[] Submit(string screenID, Command[] commands)
  {
    return (Content[]) this.Invoke(nameof (Submit), new object[2]
    {
      (object) screenID,
      (object) commands
    })[0];
  }

  [SoapDocumentMethod("http://www.acumatica.com/untyped/Login", RequestNamespace = "http://www.acumatica.com/untyped/", ResponseNamespace = "http://www.acumatica.com/untyped/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public LoginResult Login(string name, string password)
  {
    return (LoginResult) this.Invoke(nameof (Login), new object[2]
    {
      (object) name,
      (object) password
    })[0];
  }

  [SoapDocumentMethod("http://www.acumatica.com/untyped/SetBusinessDate", RequestNamespace = "http://www.acumatica.com/untyped/", ResponseNamespace = "http://www.acumatica.com/untyped/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public void SetBusinessDate(System.DateTime date)
  {
    this.Invoke(nameof (SetBusinessDate), new object[1]
    {
      (object) date
    });
  }

  [SoapDocumentMethod("http://www.acumatica.com/untyped/SetLocaleName", RequestNamespace = "http://www.acumatica.com/untyped/", ResponseNamespace = "http://www.acumatica.com/untyped/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public void SetLocaleName(string localeName)
  {
    this.Invoke(nameof (SetLocaleName), new object[1]
    {
      (object) localeName
    });
  }

  [SoapDocumentMethod("http://www.acumatica.com/untyped/SetSchemaMode", RequestNamespace = "http://www.acumatica.com/untyped/", ResponseNamespace = "http://www.acumatica.com/untyped/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public void SetSchemaMode(SchemaMode mode)
  {
    this.Invoke(nameof (SetSchemaMode), new object[1]
    {
      (object) mode
    });
  }

  private bool IsLocalFileSystemWebService(string url)
  {
    if (url == null || url == string.Empty)
      return false;
    Uri uri = new Uri(url);
    return uri.Port >= 1024 /*0x0400*/ && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0;
  }
}

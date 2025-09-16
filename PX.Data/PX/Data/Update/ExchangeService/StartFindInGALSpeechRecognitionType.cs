// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.StartFindInGALSpeechRecognitionType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
[Serializable]
public class StartFindInGALSpeechRecognitionType : BaseRequestType
{
  private string cultureField;
  private string timeZoneField;
  private string userObjectGuidField;
  private string tenantGuidField;

  /// <remarks />
  public string Culture
  {
    get => this.cultureField;
    set => this.cultureField = value;
  }

  /// <remarks />
  public string TimeZone
  {
    get => this.timeZoneField;
    set => this.timeZoneField = value;
  }

  /// <remarks />
  public string UserObjectGuid
  {
    get => this.userObjectGuidField;
    set => this.userObjectGuidField = value;
  }

  /// <remarks />
  public string TenantGuid
  {
    get => this.tenantGuidField;
    set => this.tenantGuidField = value;
  }
}

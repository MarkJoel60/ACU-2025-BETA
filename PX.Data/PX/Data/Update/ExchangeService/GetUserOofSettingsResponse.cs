// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.GetUserOofSettingsResponse
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
public class GetUserOofSettingsResponse
{
  private ResponseMessageType responseMessageField;
  private UserOofSettings oofSettingsField;
  private ExternalAudience allowExternalOofField;
  private bool allowExternalOofFieldSpecified;

  /// <remarks />
  public ResponseMessageType ResponseMessage
  {
    get => this.responseMessageField;
    set => this.responseMessageField = value;
  }

  /// <remarks />
  [XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
  public UserOofSettings OofSettings
  {
    get => this.oofSettingsField;
    set => this.oofSettingsField = value;
  }

  /// <remarks />
  public ExternalAudience AllowExternalOof
  {
    get => this.allowExternalOofField;
    set => this.allowExternalOofField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool AllowExternalOofSpecified
  {
    get => this.allowExternalOofFieldSpecified;
    set => this.allowExternalOofFieldSpecified = value;
  }
}

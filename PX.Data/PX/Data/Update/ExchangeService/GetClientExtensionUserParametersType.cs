// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.GetClientExtensionUserParametersType
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
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public class GetClientExtensionUserParametersType
{
  private string[] userEnabledExtensionsField;
  private string[] userDisabledExtensionsField;
  private string userIdField;
  private bool enabledOnlyField;
  private bool enabledOnlyFieldSpecified;

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] UserEnabledExtensions
  {
    get => this.userEnabledExtensionsField;
    set => this.userEnabledExtensionsField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] UserDisabledExtensions
  {
    get => this.userDisabledExtensionsField;
    set => this.userDisabledExtensionsField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public string UserId
  {
    get => this.userIdField;
    set => this.userIdField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public bool EnabledOnly
  {
    get => this.enabledOnlyField;
    set => this.enabledOnlyField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool EnabledOnlySpecified
  {
    get => this.enabledOnlyFieldSpecified;
    set => this.enabledOnlyFieldSpecified = value;
  }
}

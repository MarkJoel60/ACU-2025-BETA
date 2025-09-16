// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.RuleType
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
public class RuleType
{
  private string ruleIdField;
  private string displayNameField;
  private int priorityField;
  private bool isEnabledField;
  private bool isNotSupportedField;
  private bool isNotSupportedFieldSpecified;
  private bool isInErrorField;
  private bool isInErrorFieldSpecified;
  private RulePredicatesType conditionsField;
  private RulePredicatesType exceptionsField;
  private RuleActionsType actionsField;

  /// <remarks />
  public string RuleId
  {
    get => this.ruleIdField;
    set => this.ruleIdField = value;
  }

  /// <remarks />
  public string DisplayName
  {
    get => this.displayNameField;
    set => this.displayNameField = value;
  }

  /// <remarks />
  public int Priority
  {
    get => this.priorityField;
    set => this.priorityField = value;
  }

  /// <remarks />
  public bool IsEnabled
  {
    get => this.isEnabledField;
    set => this.isEnabledField = value;
  }

  /// <remarks />
  public bool IsNotSupported
  {
    get => this.isNotSupportedField;
    set => this.isNotSupportedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsNotSupportedSpecified
  {
    get => this.isNotSupportedFieldSpecified;
    set => this.isNotSupportedFieldSpecified = value;
  }

  /// <remarks />
  public bool IsInError
  {
    get => this.isInErrorField;
    set => this.isInErrorField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsInErrorSpecified
  {
    get => this.isInErrorFieldSpecified;
    set => this.isInErrorFieldSpecified = value;
  }

  /// <remarks />
  public RulePredicatesType Conditions
  {
    get => this.conditionsField;
    set => this.conditionsField = value;
  }

  /// <remarks />
  public RulePredicatesType Exceptions
  {
    get => this.exceptionsField;
    set => this.exceptionsField = value;
  }

  /// <remarks />
  public RuleActionsType Actions
  {
    get => this.actionsField;
    set => this.actionsField = value;
  }
}

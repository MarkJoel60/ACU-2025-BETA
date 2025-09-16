// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ProtectionRuleType
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
public class ProtectionRuleType
{
  private ProtectionRuleConditionType conditionField;
  private ProtectionRuleActionType actionField;
  private string nameField;
  private bool userOverridableField;
  private int priorityField;

  /// <remarks />
  public ProtectionRuleConditionType Condition
  {
    get => this.conditionField;
    set => this.conditionField = value;
  }

  /// <remarks />
  public ProtectionRuleActionType Action
  {
    get => this.actionField;
    set => this.actionField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public string Name
  {
    get => this.nameField;
    set => this.nameField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public bool UserOverridable
  {
    get => this.userOverridableField;
    set => this.userOverridableField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public int Priority
  {
    get => this.priorityField;
    set => this.priorityField = value;
  }
}

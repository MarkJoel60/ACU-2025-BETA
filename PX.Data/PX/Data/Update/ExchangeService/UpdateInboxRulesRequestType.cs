// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.UpdateInboxRulesRequestType
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
public class UpdateInboxRulesRequestType : BaseRequestType
{
  private string mailboxSmtpAddressField;
  private bool removeOutlookRuleBlobField;
  private bool removeOutlookRuleBlobFieldSpecified;
  private RuleOperationType[] operationsField;

  /// <remarks />
  public string MailboxSmtpAddress
  {
    get => this.mailboxSmtpAddressField;
    set => this.mailboxSmtpAddressField = value;
  }

  /// <remarks />
  public bool RemoveOutlookRuleBlob
  {
    get => this.removeOutlookRuleBlobField;
    set => this.removeOutlookRuleBlobField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool RemoveOutlookRuleBlobSpecified
  {
    get => this.removeOutlookRuleBlobFieldSpecified;
    set => this.removeOutlookRuleBlobFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("CreateRuleOperation", typeof (CreateRuleOperationType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  [XmlArrayItem("DeleteRuleOperation", typeof (DeleteRuleOperationType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  [XmlArrayItem("SetRuleOperation", typeof (SetRuleOperationType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public RuleOperationType[] Operations
  {
    get => this.operationsField;
    set => this.operationsField = value;
  }
}

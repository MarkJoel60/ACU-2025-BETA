// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ApprovalRequestDataType
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
public class ApprovalRequestDataType
{
  private bool isUndecidedApprovalRequestField;
  private bool isUndecidedApprovalRequestFieldSpecified;
  private int approvalDecisionField;
  private bool approvalDecisionFieldSpecified;
  private string approvalDecisionMakerField;
  private System.DateTime approvalDecisionTimeField;
  private bool approvalDecisionTimeFieldSpecified;

  /// <remarks />
  public bool IsUndecidedApprovalRequest
  {
    get => this.isUndecidedApprovalRequestField;
    set => this.isUndecidedApprovalRequestField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsUndecidedApprovalRequestSpecified
  {
    get => this.isUndecidedApprovalRequestFieldSpecified;
    set => this.isUndecidedApprovalRequestFieldSpecified = value;
  }

  /// <remarks />
  public int ApprovalDecision
  {
    get => this.approvalDecisionField;
    set => this.approvalDecisionField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ApprovalDecisionSpecified
  {
    get => this.approvalDecisionFieldSpecified;
    set => this.approvalDecisionFieldSpecified = value;
  }

  /// <remarks />
  public string ApprovalDecisionMaker
  {
    get => this.approvalDecisionMakerField;
    set => this.approvalDecisionMakerField = value;
  }

  /// <remarks />
  public System.DateTime ApprovalDecisionTime
  {
    get => this.approvalDecisionTimeField;
    set => this.approvalDecisionTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ApprovalDecisionTimeSpecified
  {
    get => this.approvalDecisionTimeFieldSpecified;
    set => this.approvalDecisionTimeFieldSpecified = value;
  }
}

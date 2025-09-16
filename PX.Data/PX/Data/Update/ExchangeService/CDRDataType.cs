// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.CDRDataType
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
public class CDRDataType
{
  private System.DateTime callStartTimeField;
  private string callTypeField;
  private string callIdentityField;
  private string parentCallIdentityField;
  private string uMServerNameField;
  private string dialPlanGuidField;
  private string dialPlanNameField;
  private int callDurationField;
  private string iPGatewayAddressField;
  private string iPGatewayNameField;
  private string gatewayGuidField;
  private string calledPhoneNumberField;
  private string callerPhoneNumberField;
  private string offerResultField;
  private string dropCallReasonField;
  private string reasonForCallField;
  private string transferredNumberField;
  private string dialedStringField;
  private string callerMailboxAliasField;
  private string calleeMailboxAliasField;
  private string callerLegacyExchangeDNField;
  private string calleeLegacyExchangeDNField;
  private string autoAttendantNameField;
  private AudioQualityType audioQualityMetricsField;
  private System.DateTime creationTimeField;

  /// <remarks />
  public System.DateTime CallStartTime
  {
    get => this.callStartTimeField;
    set => this.callStartTimeField = value;
  }

  /// <remarks />
  public string CallType
  {
    get => this.callTypeField;
    set => this.callTypeField = value;
  }

  /// <remarks />
  public string CallIdentity
  {
    get => this.callIdentityField;
    set => this.callIdentityField = value;
  }

  /// <remarks />
  public string ParentCallIdentity
  {
    get => this.parentCallIdentityField;
    set => this.parentCallIdentityField = value;
  }

  /// <remarks />
  public string UMServerName
  {
    get => this.uMServerNameField;
    set => this.uMServerNameField = value;
  }

  /// <remarks />
  public string DialPlanGuid
  {
    get => this.dialPlanGuidField;
    set => this.dialPlanGuidField = value;
  }

  /// <remarks />
  public string DialPlanName
  {
    get => this.dialPlanNameField;
    set => this.dialPlanNameField = value;
  }

  /// <remarks />
  public int CallDuration
  {
    get => this.callDurationField;
    set => this.callDurationField = value;
  }

  /// <remarks />
  public string IPGatewayAddress
  {
    get => this.iPGatewayAddressField;
    set => this.iPGatewayAddressField = value;
  }

  /// <remarks />
  public string IPGatewayName
  {
    get => this.iPGatewayNameField;
    set => this.iPGatewayNameField = value;
  }

  /// <remarks />
  public string GatewayGuid
  {
    get => this.gatewayGuidField;
    set => this.gatewayGuidField = value;
  }

  /// <remarks />
  public string CalledPhoneNumber
  {
    get => this.calledPhoneNumberField;
    set => this.calledPhoneNumberField = value;
  }

  /// <remarks />
  public string CallerPhoneNumber
  {
    get => this.callerPhoneNumberField;
    set => this.callerPhoneNumberField = value;
  }

  /// <remarks />
  public string OfferResult
  {
    get => this.offerResultField;
    set => this.offerResultField = value;
  }

  /// <remarks />
  public string DropCallReason
  {
    get => this.dropCallReasonField;
    set => this.dropCallReasonField = value;
  }

  /// <remarks />
  public string ReasonForCall
  {
    get => this.reasonForCallField;
    set => this.reasonForCallField = value;
  }

  /// <remarks />
  public string TransferredNumber
  {
    get => this.transferredNumberField;
    set => this.transferredNumberField = value;
  }

  /// <remarks />
  public string DialedString
  {
    get => this.dialedStringField;
    set => this.dialedStringField = value;
  }

  /// <remarks />
  public string CallerMailboxAlias
  {
    get => this.callerMailboxAliasField;
    set => this.callerMailboxAliasField = value;
  }

  /// <remarks />
  public string CalleeMailboxAlias
  {
    get => this.calleeMailboxAliasField;
    set => this.calleeMailboxAliasField = value;
  }

  /// <remarks />
  public string CallerLegacyExchangeDN
  {
    get => this.callerLegacyExchangeDNField;
    set => this.callerLegacyExchangeDNField = value;
  }

  /// <remarks />
  public string CalleeLegacyExchangeDN
  {
    get => this.calleeLegacyExchangeDNField;
    set => this.calleeLegacyExchangeDNField = value;
  }

  /// <remarks />
  public string AutoAttendantName
  {
    get => this.autoAttendantNameField;
    set => this.autoAttendantNameField = value;
  }

  /// <remarks />
  public AudioQualityType AudioQualityMetrics
  {
    get => this.audioQualityMetricsField;
    set => this.audioQualityMetricsField = value;
  }

  /// <remarks />
  public System.DateTime CreationTime
  {
    get => this.creationTimeField;
    set => this.creationTimeField = value;
  }
}

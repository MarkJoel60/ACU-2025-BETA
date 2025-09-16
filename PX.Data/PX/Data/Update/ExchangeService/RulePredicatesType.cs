// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.RulePredicatesType
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
public class RulePredicatesType
{
  private string[] categoriesField;
  private string[] containsBodyStringsField;
  private string[] containsHeaderStringsField;
  private string[] containsRecipientStringsField;
  private string[] containsSenderStringsField;
  private string[] containsSubjectOrBodyStringsField;
  private string[] containsSubjectStringsField;
  private FlaggedForActionType flaggedForActionField;
  private bool flaggedForActionFieldSpecified;
  private EmailAddressType[] fromAddressesField;
  private string[] fromConnectedAccountsField;
  private bool hasAttachmentsField;
  private bool hasAttachmentsFieldSpecified;
  private ImportanceChoicesType importanceField;
  private bool importanceFieldSpecified;
  private bool isApprovalRequestField;
  private bool isApprovalRequestFieldSpecified;
  private bool isAutomaticForwardField;
  private bool isAutomaticForwardFieldSpecified;
  private bool isAutomaticReplyField;
  private bool isAutomaticReplyFieldSpecified;
  private bool isEncryptedField;
  private bool isEncryptedFieldSpecified;
  private bool isMeetingRequestField;
  private bool isMeetingRequestFieldSpecified;
  private bool isMeetingResponseField;
  private bool isMeetingResponseFieldSpecified;
  private bool isNDRField;
  private bool isNDRFieldSpecified;
  private bool isPermissionControlledField;
  private bool isPermissionControlledFieldSpecified;
  private bool isReadReceiptField;
  private bool isReadReceiptFieldSpecified;
  private bool isSignedField;
  private bool isSignedFieldSpecified;
  private bool isVoicemailField;
  private bool isVoicemailFieldSpecified;
  private string[] itemClassesField;
  private string[] messageClassificationsField;
  private bool notSentToMeField;
  private bool notSentToMeFieldSpecified;
  private bool sentCcMeField;
  private bool sentCcMeFieldSpecified;
  private bool sentOnlyToMeField;
  private bool sentOnlyToMeFieldSpecified;
  private EmailAddressType[] sentToAddressesField;
  private bool sentToMeField;
  private bool sentToMeFieldSpecified;
  private bool sentToOrCcMeField;
  private bool sentToOrCcMeFieldSpecified;
  private SensitivityChoicesType sensitivityField;
  private bool sensitivityFieldSpecified;
  private RulePredicateDateRangeType withinDateRangeField;
  private RulePredicateSizeRangeType withinSizeRangeField;

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] Categories
  {
    get => this.categoriesField;
    set => this.categoriesField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] ContainsBodyStrings
  {
    get => this.containsBodyStringsField;
    set => this.containsBodyStringsField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] ContainsHeaderStrings
  {
    get => this.containsHeaderStringsField;
    set => this.containsHeaderStringsField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] ContainsRecipientStrings
  {
    get => this.containsRecipientStringsField;
    set => this.containsRecipientStringsField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] ContainsSenderStrings
  {
    get => this.containsSenderStringsField;
    set => this.containsSenderStringsField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] ContainsSubjectOrBodyStrings
  {
    get => this.containsSubjectOrBodyStringsField;
    set => this.containsSubjectOrBodyStringsField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] ContainsSubjectStrings
  {
    get => this.containsSubjectStringsField;
    set => this.containsSubjectStringsField = value;
  }

  /// <remarks />
  public FlaggedForActionType FlaggedForAction
  {
    get => this.flaggedForActionField;
    set => this.flaggedForActionField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool FlaggedForActionSpecified
  {
    get => this.flaggedForActionFieldSpecified;
    set => this.flaggedForActionFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("Address", IsNullable = false)]
  public EmailAddressType[] FromAddresses
  {
    get => this.fromAddressesField;
    set => this.fromAddressesField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] FromConnectedAccounts
  {
    get => this.fromConnectedAccountsField;
    set => this.fromConnectedAccountsField = value;
  }

  /// <remarks />
  public bool HasAttachments
  {
    get => this.hasAttachmentsField;
    set => this.hasAttachmentsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool HasAttachmentsSpecified
  {
    get => this.hasAttachmentsFieldSpecified;
    set => this.hasAttachmentsFieldSpecified = value;
  }

  /// <remarks />
  public ImportanceChoicesType Importance
  {
    get => this.importanceField;
    set => this.importanceField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ImportanceSpecified
  {
    get => this.importanceFieldSpecified;
    set => this.importanceFieldSpecified = value;
  }

  /// <remarks />
  public bool IsApprovalRequest
  {
    get => this.isApprovalRequestField;
    set => this.isApprovalRequestField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsApprovalRequestSpecified
  {
    get => this.isApprovalRequestFieldSpecified;
    set => this.isApprovalRequestFieldSpecified = value;
  }

  /// <remarks />
  public bool IsAutomaticForward
  {
    get => this.isAutomaticForwardField;
    set => this.isAutomaticForwardField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsAutomaticForwardSpecified
  {
    get => this.isAutomaticForwardFieldSpecified;
    set => this.isAutomaticForwardFieldSpecified = value;
  }

  /// <remarks />
  public bool IsAutomaticReply
  {
    get => this.isAutomaticReplyField;
    set => this.isAutomaticReplyField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsAutomaticReplySpecified
  {
    get => this.isAutomaticReplyFieldSpecified;
    set => this.isAutomaticReplyFieldSpecified = value;
  }

  /// <remarks />
  public bool IsEncrypted
  {
    get => this.isEncryptedField;
    set => this.isEncryptedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsEncryptedSpecified
  {
    get => this.isEncryptedFieldSpecified;
    set => this.isEncryptedFieldSpecified = value;
  }

  /// <remarks />
  public bool IsMeetingRequest
  {
    get => this.isMeetingRequestField;
    set => this.isMeetingRequestField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsMeetingRequestSpecified
  {
    get => this.isMeetingRequestFieldSpecified;
    set => this.isMeetingRequestFieldSpecified = value;
  }

  /// <remarks />
  public bool IsMeetingResponse
  {
    get => this.isMeetingResponseField;
    set => this.isMeetingResponseField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsMeetingResponseSpecified
  {
    get => this.isMeetingResponseFieldSpecified;
    set => this.isMeetingResponseFieldSpecified = value;
  }

  /// <remarks />
  public bool IsNDR
  {
    get => this.isNDRField;
    set => this.isNDRField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsNDRSpecified
  {
    get => this.isNDRFieldSpecified;
    set => this.isNDRFieldSpecified = value;
  }

  /// <remarks />
  public bool IsPermissionControlled
  {
    get => this.isPermissionControlledField;
    set => this.isPermissionControlledField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsPermissionControlledSpecified
  {
    get => this.isPermissionControlledFieldSpecified;
    set => this.isPermissionControlledFieldSpecified = value;
  }

  /// <remarks />
  public bool IsReadReceipt
  {
    get => this.isReadReceiptField;
    set => this.isReadReceiptField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsReadReceiptSpecified
  {
    get => this.isReadReceiptFieldSpecified;
    set => this.isReadReceiptFieldSpecified = value;
  }

  /// <remarks />
  public bool IsSigned
  {
    get => this.isSignedField;
    set => this.isSignedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsSignedSpecified
  {
    get => this.isSignedFieldSpecified;
    set => this.isSignedFieldSpecified = value;
  }

  /// <remarks />
  public bool IsVoicemail
  {
    get => this.isVoicemailField;
    set => this.isVoicemailField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsVoicemailSpecified
  {
    get => this.isVoicemailFieldSpecified;
    set => this.isVoicemailFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] ItemClasses
  {
    get => this.itemClassesField;
    set => this.itemClassesField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] MessageClassifications
  {
    get => this.messageClassificationsField;
    set => this.messageClassificationsField = value;
  }

  /// <remarks />
  public bool NotSentToMe
  {
    get => this.notSentToMeField;
    set => this.notSentToMeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool NotSentToMeSpecified
  {
    get => this.notSentToMeFieldSpecified;
    set => this.notSentToMeFieldSpecified = value;
  }

  /// <remarks />
  public bool SentCcMe
  {
    get => this.sentCcMeField;
    set => this.sentCcMeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool SentCcMeSpecified
  {
    get => this.sentCcMeFieldSpecified;
    set => this.sentCcMeFieldSpecified = value;
  }

  /// <remarks />
  public bool SentOnlyToMe
  {
    get => this.sentOnlyToMeField;
    set => this.sentOnlyToMeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool SentOnlyToMeSpecified
  {
    get => this.sentOnlyToMeFieldSpecified;
    set => this.sentOnlyToMeFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("Address", IsNullable = false)]
  public EmailAddressType[] SentToAddresses
  {
    get => this.sentToAddressesField;
    set => this.sentToAddressesField = value;
  }

  /// <remarks />
  public bool SentToMe
  {
    get => this.sentToMeField;
    set => this.sentToMeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool SentToMeSpecified
  {
    get => this.sentToMeFieldSpecified;
    set => this.sentToMeFieldSpecified = value;
  }

  /// <remarks />
  public bool SentToOrCcMe
  {
    get => this.sentToOrCcMeField;
    set => this.sentToOrCcMeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool SentToOrCcMeSpecified
  {
    get => this.sentToOrCcMeFieldSpecified;
    set => this.sentToOrCcMeFieldSpecified = value;
  }

  /// <remarks />
  public SensitivityChoicesType Sensitivity
  {
    get => this.sensitivityField;
    set => this.sensitivityField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool SensitivitySpecified
  {
    get => this.sensitivityFieldSpecified;
    set => this.sensitivityFieldSpecified = value;
  }

  /// <remarks />
  public RulePredicateDateRangeType WithinDateRange
  {
    get => this.withinDateRangeField;
    set => this.withinDateRangeField = value;
  }

  /// <remarks />
  public RulePredicateSizeRangeType WithinSizeRange
  {
    get => this.withinSizeRangeField;
    set => this.withinSizeRangeField = value;
  }
}

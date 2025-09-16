// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.RuleFieldURIType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public enum RuleFieldURIType
{
  /// <remarks />
  RuleId,
  /// <remarks />
  DisplayName,
  /// <remarks />
  Priority,
  /// <remarks />
  IsNotSupported,
  /// <remarks />
  Actions,
  /// <remarks />
  [XmlEnum("Condition:Categories")] ConditionCategories,
  /// <remarks />
  [XmlEnum("Condition:ContainsBodyStrings")] ConditionContainsBodyStrings,
  /// <remarks />
  [XmlEnum("Condition:ContainsHeaderStrings")] ConditionContainsHeaderStrings,
  /// <remarks />
  [XmlEnum("Condition:ContainsRecipientStrings")] ConditionContainsRecipientStrings,
  /// <remarks />
  [XmlEnum("Condition:ContainsSenderStrings")] ConditionContainsSenderStrings,
  /// <remarks />
  [XmlEnum("Condition:ContainsSubjectOrBodyStrings")] ConditionContainsSubjectOrBodyStrings,
  /// <remarks />
  [XmlEnum("Condition:ContainsSubjectStrings")] ConditionContainsSubjectStrings,
  /// <remarks />
  [XmlEnum("Condition:FlaggedForAction")] ConditionFlaggedForAction,
  /// <remarks />
  [XmlEnum("Condition:FromAddresses")] ConditionFromAddresses,
  /// <remarks />
  [XmlEnum("Condition:FromConnectedAccounts")] ConditionFromConnectedAccounts,
  /// <remarks />
  [XmlEnum("Condition:HasAttachments")] ConditionHasAttachments,
  /// <remarks />
  [XmlEnum("Condition:Importance")] ConditionImportance,
  /// <remarks />
  [XmlEnum("Condition:IsApprovalRequest")] ConditionIsApprovalRequest,
  /// <remarks />
  [XmlEnum("Condition:IsAutomaticForward")] ConditionIsAutomaticForward,
  /// <remarks />
  [XmlEnum("Condition:IsAutomaticReply")] ConditionIsAutomaticReply,
  /// <remarks />
  [XmlEnum("Condition:IsEncrypted")] ConditionIsEncrypted,
  /// <remarks />
  [XmlEnum("Condition:IsMeetingRequest")] ConditionIsMeetingRequest,
  /// <remarks />
  [XmlEnum("Condition:IsMeetingResponse")] ConditionIsMeetingResponse,
  /// <remarks />
  [XmlEnum("Condition:IsNDR")] ConditionIsNDR,
  /// <remarks />
  [XmlEnum("Condition:IsPermissionControlled")] ConditionIsPermissionControlled,
  /// <remarks />
  [XmlEnum("Condition:IsReadReceipt")] ConditionIsReadReceipt,
  /// <remarks />
  [XmlEnum("Condition:IsSigned")] ConditionIsSigned,
  /// <remarks />
  [XmlEnum("Condition:IsVoicemail")] ConditionIsVoicemail,
  /// <remarks />
  [XmlEnum("Condition:ItemClasses")] ConditionItemClasses,
  /// <remarks />
  [XmlEnum("Condition:MessageClassifications")] ConditionMessageClassifications,
  /// <remarks />
  [XmlEnum("Condition:NotSentToMe")] ConditionNotSentToMe,
  /// <remarks />
  [XmlEnum("Condition:SentCcMe")] ConditionSentCcMe,
  /// <remarks />
  [XmlEnum("Condition:SentOnlyToMe")] ConditionSentOnlyToMe,
  /// <remarks />
  [XmlEnum("Condition:SentToAddresses")] ConditionSentToAddresses,
  /// <remarks />
  [XmlEnum("Condition:SentToMe")] ConditionSentToMe,
  /// <remarks />
  [XmlEnum("Condition:SentToOrCcMe")] ConditionSentToOrCcMe,
  /// <remarks />
  [XmlEnum("Condition:Sensitivity")] ConditionSensitivity,
  /// <remarks />
  [XmlEnum("Condition:WithinDateRange")] ConditionWithinDateRange,
  /// <remarks />
  [XmlEnum("Condition:WithinSizeRange")] ConditionWithinSizeRange,
  /// <remarks />
  [XmlEnum("Exception:Categories")] ExceptionCategories,
  /// <remarks />
  [XmlEnum("Exception:ContainsBodyStrings")] ExceptionContainsBodyStrings,
  /// <remarks />
  [XmlEnum("Exception:ContainsHeaderStrings")] ExceptionContainsHeaderStrings,
  /// <remarks />
  [XmlEnum("Exception:ContainsRecipientStrings")] ExceptionContainsRecipientStrings,
  /// <remarks />
  [XmlEnum("Exception:ContainsSenderStrings")] ExceptionContainsSenderStrings,
  /// <remarks />
  [XmlEnum("Exception:ContainsSubjectOrBodyStrings")] ExceptionContainsSubjectOrBodyStrings,
  /// <remarks />
  [XmlEnum("Exception:ContainsSubjectStrings")] ExceptionContainsSubjectStrings,
  /// <remarks />
  [XmlEnum("Exception:FlaggedForAction")] ExceptionFlaggedForAction,
  /// <remarks />
  [XmlEnum("Exception:FromAddresses")] ExceptionFromAddresses,
  /// <remarks />
  [XmlEnum("Exception:FromConnectedAccounts")] ExceptionFromConnectedAccounts,
  /// <remarks />
  [XmlEnum("Exception:HasAttachments")] ExceptionHasAttachments,
  /// <remarks />
  [XmlEnum("Exception:Importance")] ExceptionImportance,
  /// <remarks />
  [XmlEnum("Exception:IsApprovalRequest")] ExceptionIsApprovalRequest,
  /// <remarks />
  [XmlEnum("Exception:IsAutomaticForward")] ExceptionIsAutomaticForward,
  /// <remarks />
  [XmlEnum("Exception:IsAutomaticReply")] ExceptionIsAutomaticReply,
  /// <remarks />
  [XmlEnum("Exception:IsEncrypted")] ExceptionIsEncrypted,
  /// <remarks />
  [XmlEnum("Exception:IsMeetingRequest")] ExceptionIsMeetingRequest,
  /// <remarks />
  [XmlEnum("Exception:IsMeetingResponse")] ExceptionIsMeetingResponse,
  /// <remarks />
  [XmlEnum("Exception:IsNDR")] ExceptionIsNDR,
  /// <remarks />
  [XmlEnum("Exception:IsPermissionControlled")] ExceptionIsPermissionControlled,
  /// <remarks />
  [XmlEnum("Exception:IsReadReceipt")] ExceptionIsReadReceipt,
  /// <remarks />
  [XmlEnum("Exception:IsSigned")] ExceptionIsSigned,
  /// <remarks />
  [XmlEnum("Exception:IsVoicemail")] ExceptionIsVoicemail,
  /// <remarks />
  [XmlEnum("Exception:ItemClasses")] ExceptionItemClasses,
  /// <remarks />
  [XmlEnum("Exception:MessageClassifications")] ExceptionMessageClassifications,
  /// <remarks />
  [XmlEnum("Exception:NotSentToMe")] ExceptionNotSentToMe,
  /// <remarks />
  [XmlEnum("Exception:SentCcMe")] ExceptionSentCcMe,
  /// <remarks />
  [XmlEnum("Exception:SentOnlyToMe")] ExceptionSentOnlyToMe,
  /// <remarks />
  [XmlEnum("Exception:SentToAddresses")] ExceptionSentToAddresses,
  /// <remarks />
  [XmlEnum("Exception:SentToMe")] ExceptionSentToMe,
  /// <remarks />
  [XmlEnum("Exception:SentToOrCcMe")] ExceptionSentToOrCcMe,
  /// <remarks />
  [XmlEnum("Exception:Sensitivity")] ExceptionSensitivity,
  /// <remarks />
  [XmlEnum("Exception:WithinDateRange")] ExceptionWithinDateRange,
  /// <remarks />
  [XmlEnum("Exception:WithinSizeRange")] ExceptionWithinSizeRange,
  /// <remarks />
  [XmlEnum("Action:AssignCategories")] ActionAssignCategories,
  /// <remarks />
  [XmlEnum("Action:CopyToFolder")] ActionCopyToFolder,
  /// <remarks />
  [XmlEnum("Action:Delete")] ActionDelete,
  /// <remarks />
  [XmlEnum("Action:ForwardAsAttachmentToRecipients")] ActionForwardAsAttachmentToRecipients,
  /// <remarks />
  [XmlEnum("Action:ForwardToRecipients")] ActionForwardToRecipients,
  /// <remarks />
  [XmlEnum("Action:MarkImportance")] ActionMarkImportance,
  /// <remarks />
  [XmlEnum("Action:MarkAsRead")] ActionMarkAsRead,
  /// <remarks />
  [XmlEnum("Action:MoveToFolder")] ActionMoveToFolder,
  /// <remarks />
  [XmlEnum("Action:PermanentDelete")] ActionPermanentDelete,
  /// <remarks />
  [XmlEnum("Action:RedirectToRecipients")] ActionRedirectToRecipients,
  /// <remarks />
  [XmlEnum("Action:SendSMSAlertToRecipients")] ActionSendSMSAlertToRecipients,
  /// <remarks />
  [XmlEnum("Action:ServerReplyWithMessage")] ActionServerReplyWithMessage,
  /// <remarks />
  [XmlEnum("Action:StopProcessingRules")] ActionStopProcessingRules,
  /// <remarks />
  IsEnabled,
  /// <remarks />
  IsInError,
  /// <remarks />
  Conditions,
  /// <remarks />
  Exceptions,
}

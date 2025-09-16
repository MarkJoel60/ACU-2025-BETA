// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ResponseMessageType
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
[XmlInclude(typeof (GetUMSubscriberCallAnsweringDataResponseMessageType))]
[XmlInclude(typeof (GetClientIntentResponseMessageType))]
[XmlInclude(typeof (GetUMPinResponseMessageType))]
[XmlInclude(typeof (SaveUMPinResponseMessageType))]
[XmlInclude(typeof (ValidateUMPinResponseMessageType))]
[XmlInclude(typeof (ResetUMMailboxResponseMessageType))]
[XmlInclude(typeof (InitUMMailboxResponseMessageType))]
[XmlInclude(typeof (GetUMCallSummaryResponseMessageType))]
[XmlInclude(typeof (GetUMCallDataRecordsResponseMessageType))]
[XmlInclude(typeof (CreateUMCallDataRecordResponseMessageType))]
[XmlInclude(typeof (CompleteFindInGALSpeechRecognitionResponseMessageType))]
[XmlInclude(typeof (StartFindInGALSpeechRecognitionResponseMessageType))]
[XmlInclude(typeof (GetUserPhotoResponseMessageType))]
[XmlInclude(typeof (GetUserRetentionPolicyTagsResponseMessageType))]
[XmlInclude(typeof (SetImListMigrationCompletedResponseMessageType))]
[XmlInclude(typeof (SetImGroupResponseMessageType))]
[XmlInclude(typeof (RemoveImGroupResponseMessageType))]
[XmlInclude(typeof (RemoveDistributionGroupFromImListResponseMessageType))]
[XmlInclude(typeof (RemoveContactFromImListResponseMessageType))]
[XmlInclude(typeof (GetImItemsResponseMessageType))]
[XmlInclude(typeof (GetImItemListResponseMessageType))]
[XmlInclude(typeof (AddDistributionGroupToImListResponseMessageType))]
[XmlInclude(typeof (AddImGroupResponseMessageType))]
[XmlInclude(typeof (RemoveImContactFromGroupResponseMessageType))]
[XmlInclude(typeof (AddImContactToGroupResponseMessageType))]
[XmlInclude(typeof (AddNewTelUriContactToGroupResponseMessageType))]
[XmlInclude(typeof (AddNewImContactToGroupResponseMessageType))]
[XmlInclude(typeof (DisableAppResponseType))]
[XmlInclude(typeof (UninstallAppResponseType))]
[XmlInclude(typeof (InstallAppResponseType))]
[XmlInclude(typeof (MarkAsJunkResponseMessageType))]
[XmlInclude(typeof (GetAppMarketplaceUrlResponseMessageType))]
[XmlInclude(typeof (GetAppManifestsResponseType))]
[XmlInclude(typeof (SetEncryptionConfigurationResponseType))]
[XmlInclude(typeof (EncryptionConfigurationResponseType))]
[XmlInclude(typeof (ClientExtensionResponseType))]
[XmlInclude(typeof (GetConversationItemsResponseMessageType))]
[XmlInclude(typeof (GetNonIndexableItemDetailsResponseMessageType))]
[XmlInclude(typeof (GetNonIndexableItemStatisticsResponseMessageType))]
[XmlInclude(typeof (SetHoldOnMailboxesResponseMessageType))]
[XmlInclude(typeof (GetHoldOnMailboxesResponseMessageType))]
[XmlInclude(typeof (GetDiscoverySearchConfigurationResponseMessageType))]
[XmlInclude(typeof (SearchMailboxesResponseMessageType))]
[XmlInclude(typeof (GetSearchableMailboxesResponseMessageType))]
[XmlInclude(typeof (FindMailboxStatisticsByKeywordsResponseMessageType))]
[XmlInclude(typeof (UpdateInboxRulesResponseType))]
[XmlInclude(typeof (GetInboxRulesResponseType))]
[XmlInclude(typeof (GetMessageTrackingReportResponseMessageType))]
[XmlInclude(typeof (FindMessageTrackingReportResponseMessageType))]
[XmlInclude(typeof (ServiceConfigurationResponseMessageType))]
[XmlInclude(typeof (GetServiceConfigurationResponseMessageType))]
[XmlInclude(typeof (PerformReminderActionResponseMessageType))]
[XmlInclude(typeof (GetRemindersResponseMessageType))]
[XmlInclude(typeof (GetRoomsResponseMessageType))]
[XmlInclude(typeof (GetRoomListsResponseMessageType))]
[XmlInclude(typeof (UnpinTeamMailboxResponseMessageType))]
[XmlInclude(typeof (SetTeamMailboxResponseMessageType))]
[XmlInclude(typeof (GetUserConfigurationResponseMessageType))]
[XmlInclude(typeof (GetSharingFolderResponseMessageType))]
[XmlInclude(typeof (RefreshSharingFolderResponseMessageType))]
[XmlInclude(typeof (GetSharingMetadataResponseMessageType))]
[XmlInclude(typeof (BaseDelegateResponseMessageType))]
[XmlInclude(typeof (UpdateDelegateResponseMessageType))]
[XmlInclude(typeof (RemoveDelegateResponseMessageType))]
[XmlInclude(typeof (AddDelegateResponseMessageType))]
[XmlInclude(typeof (GetDelegateResponseMessageType))]
[XmlInclude(typeof (DelegateUserResponseMessageType))]
[XmlInclude(typeof (ConvertIdResponseMessageType))]
[XmlInclude(typeof (SyncFolderItemsResponseMessageType))]
[XmlInclude(typeof (SyncFolderHierarchyResponseMessageType))]
[XmlInclude(typeof (SendNotificationResponseMessageType))]
[XmlInclude(typeof (GetStreamingEventsResponseMessageType))]
[XmlInclude(typeof (GetEventsResponseMessageType))]
[XmlInclude(typeof (SubscribeResponseMessageType))]
[XmlInclude(typeof (GetServerTimeZonesResponseMessageType))]
[XmlInclude(typeof (ExpandDLResponseMessageType))]
[XmlInclude(typeof (GetUMPromptResponseMessageType))]
[XmlInclude(typeof (GetUMPromptNamesResponseMessageType))]
[XmlInclude(typeof (CreateUMPromptResponseMessageType))]
[XmlInclude(typeof (DeleteUMPromptsResponseMessageType))]
[XmlInclude(typeof (DisconnectPhoneCallResponseMessageType))]
[XmlInclude(typeof (GetPhoneCallInformationResponseMessageType))]
[XmlInclude(typeof (PlayOnPhoneResponseMessageType))]
[XmlInclude(typeof (MailTipsResponseMessageType))]
[XmlInclude(typeof (GetMailTipsResponseMessageType))]
[XmlInclude(typeof (GetPasswordExpirationDateResponseMessageType))]
[XmlInclude(typeof (ResolveNamesResponseMessageType))]
[XmlInclude(typeof (GetClientAccessTokenResponseMessageType))]
[XmlInclude(typeof (FindItemResponseMessageType))]
[XmlInclude(typeof (GetFederatedDirectoryUserResponseMessageType))]
[XmlInclude(typeof (GetPersonaResponseMessageType))]
[XmlInclude(typeof (FindPeopleResponseMessageType))]
[XmlInclude(typeof (FindConversationResponseMessageType))]
[XmlInclude(typeof (DeleteAttachmentResponseMessageType))]
[XmlInclude(typeof (AttachmentInfoResponseMessageType))]
[XmlInclude(typeof (ItemInfoResponseMessageType))]
[XmlInclude(typeof (UpdateItemInRecoverableItemsResponseMessageType))]
[XmlInclude(typeof (UpdateItemResponseMessageType))]
[XmlInclude(typeof (FindFolderResponseMessageType))]
[XmlInclude(typeof (FolderInfoResponseMessageType))]
[XmlInclude(typeof (ExportItemsResponseMessageType))]
[XmlInclude(typeof (UploadItemsResponseMessageType))]
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
[Serializable]
public class ResponseMessageType
{
  private string messageTextField;
  private ResponseCodeType responseCodeField;
  private bool responseCodeFieldSpecified;
  private int descriptiveLinkKeyField;
  private bool descriptiveLinkKeyFieldSpecified;
  private ResponseMessageTypeMessageXml messageXmlField;
  private ResponseClassType responseClassField;

  /// <remarks />
  public string MessageText
  {
    get => this.messageTextField;
    set => this.messageTextField = value;
  }

  /// <remarks />
  public ResponseCodeType ResponseCode
  {
    get => this.responseCodeField;
    set => this.responseCodeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ResponseCodeSpecified
  {
    get => this.responseCodeFieldSpecified;
    set => this.responseCodeFieldSpecified = value;
  }

  /// <remarks />
  public int DescriptiveLinkKey
  {
    get => this.descriptiveLinkKeyField;
    set => this.descriptiveLinkKeyField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool DescriptiveLinkKeySpecified
  {
    get => this.descriptiveLinkKeyFieldSpecified;
    set => this.descriptiveLinkKeyFieldSpecified = value;
  }

  /// <remarks />
  public ResponseMessageTypeMessageXml MessageXml
  {
    get => this.messageXmlField;
    set => this.messageXmlField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public ResponseClassType ResponseClass
  {
    get => this.responseClassField;
    set => this.responseClassField = value;
  }
}

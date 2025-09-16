// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ArrayOfResponseMessagesType
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
public class ArrayOfResponseMessagesType
{
  private ResponseMessageType[] itemsField;
  private ItemsChoiceType4[] itemsElementNameField;

  /// <remarks />
  [XmlElement("ApplyConversationActionResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("ArchiveItemResponseMessage", typeof (ItemInfoResponseMessageType))]
  [XmlElement("ConvertIdResponseMessage", typeof (ConvertIdResponseMessageType))]
  [XmlElement("CopyFolderResponseMessage", typeof (FolderInfoResponseMessageType))]
  [XmlElement("CopyItemResponseMessage", typeof (ItemInfoResponseMessageType))]
  [XmlElement("CreateAttachmentResponseMessage", typeof (AttachmentInfoResponseMessageType))]
  [XmlElement("CreateFolderPathResponseMessage", typeof (FolderInfoResponseMessageType))]
  [XmlElement("CreateFolderResponseMessage", typeof (FolderInfoResponseMessageType))]
  [XmlElement("CreateItemResponseMessage", typeof (ItemInfoResponseMessageType))]
  [XmlElement("CreateManagedFolderResponseMessage", typeof (FolderInfoResponseMessageType))]
  [XmlElement("CreateUserConfigurationResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("DeleteAttachmentResponseMessage", typeof (DeleteAttachmentResponseMessageType))]
  [XmlElement("DeleteFolderResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("DeleteItemResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("DeleteUserConfigurationResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("EmptyFolderResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("ExpandDLResponseMessage", typeof (ExpandDLResponseMessageType))]
  [XmlElement("ExportItemsResponseMessage", typeof (ExportItemsResponseMessageType))]
  [XmlElement("FindFolderResponseMessage", typeof (FindFolderResponseMessageType))]
  [XmlElement("FindItemResponseMessage", typeof (FindItemResponseMessageType))]
  [XmlElement("FindMailboxStatisticsByKeywordsResponseMessage", typeof (FindMailboxStatisticsByKeywordsResponseMessageType))]
  [XmlElement("FindPeopleResponseMessage", typeof (FindPeopleResponseMessageType))]
  [XmlElement("GetAppManifestsResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("GetAttachmentResponseMessage", typeof (AttachmentInfoResponseMessageType))]
  [XmlElement("GetClientAccessTokenResponseMessage", typeof (GetClientAccessTokenResponseMessageType))]
  [XmlElement("GetClientExtensionResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("GetConversationItemsResponseMessage", typeof (GetConversationItemsResponseMessageType))]
  [XmlElement("GetDiscoverySearchConfigurationResponseMessage", typeof (GetDiscoverySearchConfigurationResponseMessageType))]
  [XmlElement("GetEncryptionConfigurationResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("GetEventsResponseMessage", typeof (GetEventsResponseMessageType))]
  [XmlElement("GetFolderResponseMessage", typeof (FolderInfoResponseMessageType))]
  [XmlElement("GetHoldOnMailboxesResponseMessage", typeof (GetHoldOnMailboxesResponseMessageType))]
  [XmlElement("GetItemResponseMessage", typeof (ItemInfoResponseMessageType))]
  [XmlElement("GetNonIndexableItemDetailsResponseMessage", typeof (GetNonIndexableItemDetailsResponseMessageType))]
  [XmlElement("GetNonIndexableItemStatisticsResponseMessage", typeof (GetNonIndexableItemStatisticsResponseMessageType))]
  [XmlElement("GetPasswordExpirationDateResponse", typeof (GetPasswordExpirationDateResponseMessageType))]
  [XmlElement("GetPersonaResponseMessage", typeof (GetPersonaResponseMessageType))]
  [XmlElement("GetRemindersResponse", typeof (GetRemindersResponseMessageType))]
  [XmlElement("GetRoomListsResponse", typeof (GetRoomListsResponseMessageType))]
  [XmlElement("GetRoomsResponse", typeof (GetRoomsResponseMessageType))]
  [XmlElement("GetSearchableMailboxesResponseMessage", typeof (GetSearchableMailboxesResponseMessageType))]
  [XmlElement("GetServerTimeZonesResponseMessage", typeof (GetServerTimeZonesResponseMessageType))]
  [XmlElement("GetSharingFolderResponseMessage", typeof (GetSharingFolderResponseMessageType))]
  [XmlElement("GetSharingMetadataResponseMessage", typeof (GetSharingMetadataResponseMessageType))]
  [XmlElement("GetStreamingEventsResponseMessage", typeof (GetStreamingEventsResponseMessageType))]
  [XmlElement("GetUserConfigurationResponseMessage", typeof (GetUserConfigurationResponseMessageType))]
  [XmlElement("GetUserPhotoResponseMessage", typeof (GetUserPhotoResponseMessageType))]
  [XmlElement("GetUserRetentionPolicyTagsResponseMessage", typeof (GetUserRetentionPolicyTagsResponseMessageType))]
  [XmlElement("MarkAllItemsAsReadResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("MarkAsJunkResponseMessage", typeof (MarkAsJunkResponseMessageType))]
  [XmlElement("MoveFolderResponseMessage", typeof (FolderInfoResponseMessageType))]
  [XmlElement("MoveItemResponseMessage", typeof (ItemInfoResponseMessageType))]
  [XmlElement("PerformReminderActionResponse", typeof (PerformReminderActionResponseMessageType))]
  [XmlElement("PostModernGroupItemResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("RefreshSharingFolderResponseMessage", typeof (RefreshSharingFolderResponseMessageType))]
  [XmlElement("ResolveNamesResponseMessage", typeof (ResolveNamesResponseMessageType))]
  [XmlElement("SearchMailboxesResponseMessage", typeof (SearchMailboxesResponseMessageType))]
  [XmlElement("SendItemResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("SendNotificationResponseMessage", typeof (SendNotificationResponseMessageType))]
  [XmlElement("SetClientExtensionResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("SetEncryptionConfigurationResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("SetHoldOnMailboxesResponseMessage", typeof (SetHoldOnMailboxesResponseMessageType))]
  [XmlElement("SubscribeResponseMessage", typeof (SubscribeResponseMessageType))]
  [XmlElement("SyncFolderHierarchyResponseMessage", typeof (SyncFolderHierarchyResponseMessageType))]
  [XmlElement("SyncFolderItemsResponseMessage", typeof (SyncFolderItemsResponseMessageType))]
  [XmlElement("UnsubscribeResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("UpdateFolderResponseMessage", typeof (FolderInfoResponseMessageType))]
  [XmlElement("UpdateGroupMailboxResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("UpdateItemInRecoverableItemsResponseMessage", typeof (UpdateItemInRecoverableItemsResponseMessageType))]
  [XmlElement("UpdateItemResponseMessage", typeof (UpdateItemResponseMessageType))]
  [XmlElement("UpdateMailboxAssociationResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("UpdateUserConfigurationResponseMessage", typeof (ResponseMessageType))]
  [XmlElement("UploadItemsResponseMessage", typeof (UploadItemsResponseMessageType))]
  [XmlChoiceIdentifier("ItemsElementName")]
  public ResponseMessageType[] Items
  {
    get => this.itemsField;
    set => this.itemsField = value;
  }

  /// <remarks />
  [XmlElement("ItemsElementName")]
  [XmlIgnore]
  public ItemsChoiceType4[] ItemsElementName
  {
    get => this.itemsElementNameField;
    set => this.itemsElementNameField = value;
  }
}

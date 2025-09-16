// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ItemType
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
[XmlInclude(typeof (PostItemType))]
[XmlInclude(typeof (TaskType))]
[XmlInclude(typeof (DistributionListType))]
[XmlInclude(typeof (ContactItemType))]
[XmlInclude(typeof (CalendarItemType))]
[XmlInclude(typeof (MessageType))]
[XmlInclude(typeof (MeetingMessageType))]
[XmlInclude(typeof (MeetingCancellationMessageType))]
[XmlInclude(typeof (MeetingResponseMessageType))]
[XmlInclude(typeof (MeetingRequestMessageType))]
[XmlInclude(typeof (ResponseObjectCoreType))]
[XmlInclude(typeof (ResponseObjectType))]
[XmlInclude(typeof (PostReplyItemBaseType))]
[XmlInclude(typeof (PostReplyItemType))]
[XmlInclude(typeof (AddItemToMyCalendarType))]
[XmlInclude(typeof (RemoveItemType))]
[XmlInclude(typeof (ProposeNewTimeType))]
[XmlInclude(typeof (ReferenceItemResponseType))]
[XmlInclude(typeof (AcceptSharingInvitationType))]
[XmlInclude(typeof (SuppressReadReceiptType))]
[XmlInclude(typeof (SmartResponseBaseType))]
[XmlInclude(typeof (SmartResponseType))]
[XmlInclude(typeof (CancelCalendarItemType))]
[XmlInclude(typeof (ForwardItemType))]
[XmlInclude(typeof (ReplyAllToItemType))]
[XmlInclude(typeof (ReplyToItemType))]
[XmlInclude(typeof (WellKnownResponseObjectType))]
[XmlInclude(typeof (MeetingRegistrationResponseObjectType))]
[XmlInclude(typeof (DeclineItemType))]
[XmlInclude(typeof (TentativelyAcceptItemType))]
[XmlInclude(typeof (AcceptItemType))]
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public class ItemType
{
  private MimeContentType mimeContentField;
  private ItemIdType itemIdField;
  private FolderIdType parentFolderIdField;
  private string itemClassField;
  private string subjectField;
  private SensitivityChoicesType sensitivityField;
  private bool sensitivityFieldSpecified;
  private BodyType bodyField;
  private AttachmentType[] attachmentsField;
  private System.DateTime dateTimeReceivedField;
  private bool dateTimeReceivedFieldSpecified;
  private int sizeField;
  private bool sizeFieldSpecified;
  private string[] categoriesField;
  private ImportanceChoicesType importanceField;
  private bool importanceFieldSpecified;
  private string inReplyToField;
  private bool isSubmittedField;
  private bool isSubmittedFieldSpecified;
  private bool isDraftField;
  private bool isDraftFieldSpecified;
  private bool isFromMeField;
  private bool isFromMeFieldSpecified;
  private bool isResendField;
  private bool isResendFieldSpecified;
  private bool isUnmodifiedField;
  private bool isUnmodifiedFieldSpecified;
  private InternetHeaderType[] internetMessageHeadersField;
  private System.DateTime dateTimeSentField;
  private bool dateTimeSentFieldSpecified;
  private System.DateTime dateTimeCreatedField;
  private bool dateTimeCreatedFieldSpecified;
  private ResponseObjectType[] responseObjectsField;
  private System.DateTime reminderDueByField;
  private bool reminderDueByFieldSpecified;
  private bool reminderIsSetField;
  private bool reminderIsSetFieldSpecified;
  private System.DateTime reminderNextTimeField;
  private bool reminderNextTimeFieldSpecified;
  private string reminderMinutesBeforeStartField;
  private string displayCcField;
  private string displayToField;
  private bool hasAttachmentsField;
  private bool hasAttachmentsFieldSpecified;
  private ExtendedPropertyType[] extendedPropertyField;
  private string cultureField;
  private EffectiveRightsType effectiveRightsField;
  private string lastModifiedNameField;
  private System.DateTime lastModifiedTimeField;
  private bool lastModifiedTimeFieldSpecified;
  private bool isAssociatedField;
  private bool isAssociatedFieldSpecified;
  private string webClientReadFormQueryStringField;
  private string webClientEditFormQueryStringField;
  private ItemIdType conversationIdField;
  private BodyType uniqueBodyField;
  private FlagType flagField;
  private byte[] storeEntryIdField;
  private byte[] instanceKeyField;
  private BodyType normalizedBodyField;
  private EntityExtractionResultType entityExtractionResultField;
  private RetentionTagType policyTagField;
  private RetentionTagType archiveTagField;
  private System.DateTime retentionDateField;
  private bool retentionDateFieldSpecified;
  private string previewField;
  private RightsManagementLicenseDataType rightsManagementLicenseDataField;
  private PredictedMessageActionType nextPredictedActionField;
  private bool nextPredictedActionFieldSpecified;
  private PredictedMessageActionType groupingActionField;
  private bool groupingActionFieldSpecified;
  private PredictedActionReasonType[] predictedActionReasonsField;
  private bool isClutterField;
  private bool isClutterFieldSpecified;
  private bool blockStatusField;
  private bool blockStatusFieldSpecified;
  private bool hasBlockedImagesField;
  private bool hasBlockedImagesFieldSpecified;
  private BodyType textBodyField;
  private IconIndexType iconIndexField;
  private bool iconIndexFieldSpecified;

  /// <remarks />
  public MimeContentType MimeContent
  {
    get => this.mimeContentField;
    set => this.mimeContentField = value;
  }

  /// <remarks />
  public ItemIdType ItemId
  {
    get => this.itemIdField;
    set => this.itemIdField = value;
  }

  /// <remarks />
  public FolderIdType ParentFolderId
  {
    get => this.parentFolderIdField;
    set => this.parentFolderIdField = value;
  }

  /// <remarks />
  public string ItemClass
  {
    get => this.itemClassField;
    set => this.itemClassField = value;
  }

  /// <remarks />
  public string Subject
  {
    get => this.subjectField;
    set => this.subjectField = value;
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
  public BodyType Body
  {
    get => this.bodyField;
    set => this.bodyField = value;
  }

  /// <remarks />
  [XmlArrayItem("FileAttachment", typeof (FileAttachmentType), IsNullable = false)]
  [XmlArrayItem("ItemAttachment", typeof (ItemAttachmentType), IsNullable = false)]
  public AttachmentType[] Attachments
  {
    get => this.attachmentsField;
    set => this.attachmentsField = value;
  }

  /// <remarks />
  public System.DateTime DateTimeReceived
  {
    get => this.dateTimeReceivedField;
    set => this.dateTimeReceivedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool DateTimeReceivedSpecified
  {
    get => this.dateTimeReceivedFieldSpecified;
    set => this.dateTimeReceivedFieldSpecified = value;
  }

  /// <remarks />
  public int Size
  {
    get => this.sizeField;
    set => this.sizeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool SizeSpecified
  {
    get => this.sizeFieldSpecified;
    set => this.sizeFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] Categories
  {
    get => this.categoriesField;
    set => this.categoriesField = value;
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
  public string InReplyTo
  {
    get => this.inReplyToField;
    set => this.inReplyToField = value;
  }

  /// <remarks />
  public bool IsSubmitted
  {
    get => this.isSubmittedField;
    set => this.isSubmittedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsSubmittedSpecified
  {
    get => this.isSubmittedFieldSpecified;
    set => this.isSubmittedFieldSpecified = value;
  }

  /// <remarks />
  public bool IsDraft
  {
    get => this.isDraftField;
    set => this.isDraftField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsDraftSpecified
  {
    get => this.isDraftFieldSpecified;
    set => this.isDraftFieldSpecified = value;
  }

  /// <remarks />
  public bool IsFromMe
  {
    get => this.isFromMeField;
    set => this.isFromMeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsFromMeSpecified
  {
    get => this.isFromMeFieldSpecified;
    set => this.isFromMeFieldSpecified = value;
  }

  /// <remarks />
  public bool IsResend
  {
    get => this.isResendField;
    set => this.isResendField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsResendSpecified
  {
    get => this.isResendFieldSpecified;
    set => this.isResendFieldSpecified = value;
  }

  /// <remarks />
  public bool IsUnmodified
  {
    get => this.isUnmodifiedField;
    set => this.isUnmodifiedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsUnmodifiedSpecified
  {
    get => this.isUnmodifiedFieldSpecified;
    set => this.isUnmodifiedFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("InternetMessageHeader", IsNullable = false)]
  public InternetHeaderType[] InternetMessageHeaders
  {
    get => this.internetMessageHeadersField;
    set => this.internetMessageHeadersField = value;
  }

  /// <remarks />
  public System.DateTime DateTimeSent
  {
    get => this.dateTimeSentField;
    set => this.dateTimeSentField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool DateTimeSentSpecified
  {
    get => this.dateTimeSentFieldSpecified;
    set => this.dateTimeSentFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime DateTimeCreated
  {
    get => this.dateTimeCreatedField;
    set => this.dateTimeCreatedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool DateTimeCreatedSpecified
  {
    get => this.dateTimeCreatedFieldSpecified;
    set => this.dateTimeCreatedFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("AcceptItem", typeof (AcceptItemType), IsNullable = false)]
  [XmlArrayItem("AcceptSharingInvitation", typeof (AcceptSharingInvitationType), IsNullable = false)]
  [XmlArrayItem("AddItemToMyCalendar", typeof (AddItemToMyCalendarType), IsNullable = false)]
  [XmlArrayItem("CancelCalendarItem", typeof (CancelCalendarItemType), IsNullable = false)]
  [XmlArrayItem("DeclineItem", typeof (DeclineItemType), IsNullable = false)]
  [XmlArrayItem("ForwardItem", typeof (ForwardItemType), IsNullable = false)]
  [XmlArrayItem("PostReplyItem", typeof (PostReplyItemType), IsNullable = false)]
  [XmlArrayItem("ProposeNewTime", typeof (ProposeNewTimeType), IsNullable = false)]
  [XmlArrayItem("RemoveItem", typeof (RemoveItemType), IsNullable = false)]
  [XmlArrayItem("ReplyAllToItem", typeof (ReplyAllToItemType), IsNullable = false)]
  [XmlArrayItem("ReplyToItem", typeof (ReplyToItemType), IsNullable = false)]
  [XmlArrayItem("SuppressReadReceipt", typeof (SuppressReadReceiptType), IsNullable = false)]
  [XmlArrayItem("TentativelyAcceptItem", typeof (TentativelyAcceptItemType), IsNullable = false)]
  public ResponseObjectType[] ResponseObjects
  {
    get => this.responseObjectsField;
    set => this.responseObjectsField = value;
  }

  /// <remarks />
  public System.DateTime ReminderDueBy
  {
    get => this.reminderDueByField;
    set => this.reminderDueByField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ReminderDueBySpecified
  {
    get => this.reminderDueByFieldSpecified;
    set => this.reminderDueByFieldSpecified = value;
  }

  /// <remarks />
  public bool ReminderIsSet
  {
    get => this.reminderIsSetField;
    set => this.reminderIsSetField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ReminderIsSetSpecified
  {
    get => this.reminderIsSetFieldSpecified;
    set => this.reminderIsSetFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime ReminderNextTime
  {
    get => this.reminderNextTimeField;
    set => this.reminderNextTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ReminderNextTimeSpecified
  {
    get => this.reminderNextTimeFieldSpecified;
    set => this.reminderNextTimeFieldSpecified = value;
  }

  /// <remarks />
  public string ReminderMinutesBeforeStart
  {
    get => this.reminderMinutesBeforeStartField;
    set => this.reminderMinutesBeforeStartField = value;
  }

  /// <remarks />
  public string DisplayCc
  {
    get => this.displayCcField;
    set => this.displayCcField = value;
  }

  /// <remarks />
  public string DisplayTo
  {
    get => this.displayToField;
    set => this.displayToField = value;
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
  [XmlElement("ExtendedProperty")]
  public ExtendedPropertyType[] ExtendedProperty
  {
    get => this.extendedPropertyField;
    set => this.extendedPropertyField = value;
  }

  /// <remarks />
  [XmlElement(DataType = "language")]
  public string Culture
  {
    get => this.cultureField;
    set => this.cultureField = value;
  }

  /// <remarks />
  public EffectiveRightsType EffectiveRights
  {
    get => this.effectiveRightsField;
    set => this.effectiveRightsField = value;
  }

  /// <remarks />
  public string LastModifiedName
  {
    get => this.lastModifiedNameField;
    set => this.lastModifiedNameField = value;
  }

  /// <remarks />
  public System.DateTime LastModifiedTime
  {
    get => this.lastModifiedTimeField;
    set => this.lastModifiedTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool LastModifiedTimeSpecified
  {
    get => this.lastModifiedTimeFieldSpecified;
    set => this.lastModifiedTimeFieldSpecified = value;
  }

  /// <remarks />
  public bool IsAssociated
  {
    get => this.isAssociatedField;
    set => this.isAssociatedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsAssociatedSpecified
  {
    get => this.isAssociatedFieldSpecified;
    set => this.isAssociatedFieldSpecified = value;
  }

  /// <remarks />
  public string WebClientReadFormQueryString
  {
    get => this.webClientReadFormQueryStringField;
    set => this.webClientReadFormQueryStringField = value;
  }

  /// <remarks />
  public string WebClientEditFormQueryString
  {
    get => this.webClientEditFormQueryStringField;
    set => this.webClientEditFormQueryStringField = value;
  }

  /// <remarks />
  public ItemIdType ConversationId
  {
    get => this.conversationIdField;
    set => this.conversationIdField = value;
  }

  /// <remarks />
  public BodyType UniqueBody
  {
    get => this.uniqueBodyField;
    set => this.uniqueBodyField = value;
  }

  /// <remarks />
  public FlagType Flag
  {
    get => this.flagField;
    set => this.flagField = value;
  }

  /// <remarks />
  [XmlElement(DataType = "base64Binary")]
  public byte[] StoreEntryId
  {
    get => this.storeEntryIdField;
    set => this.storeEntryIdField = value;
  }

  /// <remarks />
  [XmlElement(DataType = "base64Binary")]
  public byte[] InstanceKey
  {
    get => this.instanceKeyField;
    set => this.instanceKeyField = value;
  }

  /// <remarks />
  public BodyType NormalizedBody
  {
    get => this.normalizedBodyField;
    set => this.normalizedBodyField = value;
  }

  /// <remarks />
  public EntityExtractionResultType EntityExtractionResult
  {
    get => this.entityExtractionResultField;
    set => this.entityExtractionResultField = value;
  }

  /// <remarks />
  public RetentionTagType PolicyTag
  {
    get => this.policyTagField;
    set => this.policyTagField = value;
  }

  /// <remarks />
  public RetentionTagType ArchiveTag
  {
    get => this.archiveTagField;
    set => this.archiveTagField = value;
  }

  /// <remarks />
  public System.DateTime RetentionDate
  {
    get => this.retentionDateField;
    set => this.retentionDateField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool RetentionDateSpecified
  {
    get => this.retentionDateFieldSpecified;
    set => this.retentionDateFieldSpecified = value;
  }

  /// <remarks />
  public string Preview
  {
    get => this.previewField;
    set => this.previewField = value;
  }

  /// <remarks />
  public RightsManagementLicenseDataType RightsManagementLicenseData
  {
    get => this.rightsManagementLicenseDataField;
    set => this.rightsManagementLicenseDataField = value;
  }

  /// <remarks />
  public PredictedMessageActionType NextPredictedAction
  {
    get => this.nextPredictedActionField;
    set => this.nextPredictedActionField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool NextPredictedActionSpecified
  {
    get => this.nextPredictedActionFieldSpecified;
    set => this.nextPredictedActionFieldSpecified = value;
  }

  /// <remarks />
  public PredictedMessageActionType GroupingAction
  {
    get => this.groupingActionField;
    set => this.groupingActionField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool GroupingActionSpecified
  {
    get => this.groupingActionFieldSpecified;
    set => this.groupingActionFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("PredictedActionReason", IsNullable = false)]
  public PredictedActionReasonType[] PredictedActionReasons
  {
    get => this.predictedActionReasonsField;
    set => this.predictedActionReasonsField = value;
  }

  /// <remarks />
  public bool IsClutter
  {
    get => this.isClutterField;
    set => this.isClutterField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsClutterSpecified
  {
    get => this.isClutterFieldSpecified;
    set => this.isClutterFieldSpecified = value;
  }

  /// <remarks />
  public bool BlockStatus
  {
    get => this.blockStatusField;
    set => this.blockStatusField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool BlockStatusSpecified
  {
    get => this.blockStatusFieldSpecified;
    set => this.blockStatusFieldSpecified = value;
  }

  /// <remarks />
  public bool HasBlockedImages
  {
    get => this.hasBlockedImagesField;
    set => this.hasBlockedImagesField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool HasBlockedImagesSpecified
  {
    get => this.hasBlockedImagesFieldSpecified;
    set => this.hasBlockedImagesFieldSpecified = value;
  }

  /// <remarks />
  public BodyType TextBody
  {
    get => this.textBodyField;
    set => this.textBodyField = value;
  }

  /// <remarks />
  public IconIndexType IconIndex
  {
    get => this.iconIndexField;
    set => this.iconIndexField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IconIndexSpecified
  {
    get => this.iconIndexFieldSpecified;
    set => this.iconIndexFieldSpecified = value;
  }
}

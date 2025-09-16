// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ConversationType
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
public class ConversationType
{
  private ItemIdType conversationIdField;
  private string conversationTopicField;
  private string[] uniqueRecipientsField;
  private string[] globalUniqueRecipientsField;
  private string[] uniqueUnreadSendersField;
  private string[] globalUniqueUnreadSendersField;
  private string[] uniqueSendersField;
  private string[] globalUniqueSendersField;
  private System.DateTime lastDeliveryTimeField;
  private bool lastDeliveryTimeFieldSpecified;
  private System.DateTime globalLastDeliveryTimeField;
  private bool globalLastDeliveryTimeFieldSpecified;
  private string[] categoriesField;
  private string[] globalCategoriesField;
  private FlagStatusType flagStatusField;
  private bool flagStatusFieldSpecified;
  private FlagStatusType globalFlagStatusField;
  private bool globalFlagStatusFieldSpecified;
  private bool hasAttachmentsField;
  private bool hasAttachmentsFieldSpecified;
  private bool globalHasAttachmentsField;
  private bool globalHasAttachmentsFieldSpecified;
  private int messageCountField;
  private bool messageCountFieldSpecified;
  private int globalMessageCountField;
  private bool globalMessageCountFieldSpecified;
  private int unreadCountField;
  private bool unreadCountFieldSpecified;
  private int globalUnreadCountField;
  private bool globalUnreadCountFieldSpecified;
  private int sizeField;
  private bool sizeFieldSpecified;
  private int globalSizeField;
  private bool globalSizeFieldSpecified;
  private string[] itemClassesField;
  private string[] globalItemClassesField;
  private ImportanceChoicesType importanceField;
  private bool importanceFieldSpecified;
  private ImportanceChoicesType globalImportanceField;
  private bool globalImportanceFieldSpecified;
  private BaseItemIdType[] itemIdsField;
  private BaseItemIdType[] globalItemIdsField;
  private System.DateTime lastModifiedTimeField;
  private bool lastModifiedTimeFieldSpecified;
  private byte[] instanceKeyField;
  private string previewField;
  private PredictedMessageActionType nextPredictedActionField;
  private bool nextPredictedActionFieldSpecified;
  private PredictedMessageActionType groupingActionField;
  private bool groupingActionFieldSpecified;
  private MailboxSearchLocationType mailboxScopeField;
  private bool mailboxScopeFieldSpecified;
  private IconIndexType iconIndexField;
  private bool iconIndexFieldSpecified;
  private IconIndexType globalIconIndexField;
  private bool globalIconIndexFieldSpecified;
  private BaseItemIdType[] draftItemIdsField;
  private bool hasIrmField;
  private bool hasIrmFieldSpecified;
  private bool globalHasIrmField;
  private bool globalHasIrmFieldSpecified;

  /// <remarks />
  public ItemIdType ConversationId
  {
    get => this.conversationIdField;
    set => this.conversationIdField = value;
  }

  /// <remarks />
  public string ConversationTopic
  {
    get => this.conversationTopicField;
    set => this.conversationTopicField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] UniqueRecipients
  {
    get => this.uniqueRecipientsField;
    set => this.uniqueRecipientsField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] GlobalUniqueRecipients
  {
    get => this.globalUniqueRecipientsField;
    set => this.globalUniqueRecipientsField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] UniqueUnreadSenders
  {
    get => this.uniqueUnreadSendersField;
    set => this.uniqueUnreadSendersField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] GlobalUniqueUnreadSenders
  {
    get => this.globalUniqueUnreadSendersField;
    set => this.globalUniqueUnreadSendersField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] UniqueSenders
  {
    get => this.uniqueSendersField;
    set => this.uniqueSendersField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] GlobalUniqueSenders
  {
    get => this.globalUniqueSendersField;
    set => this.globalUniqueSendersField = value;
  }

  /// <remarks />
  public System.DateTime LastDeliveryTime
  {
    get => this.lastDeliveryTimeField;
    set => this.lastDeliveryTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool LastDeliveryTimeSpecified
  {
    get => this.lastDeliveryTimeFieldSpecified;
    set => this.lastDeliveryTimeFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime GlobalLastDeliveryTime
  {
    get => this.globalLastDeliveryTimeField;
    set => this.globalLastDeliveryTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool GlobalLastDeliveryTimeSpecified
  {
    get => this.globalLastDeliveryTimeFieldSpecified;
    set => this.globalLastDeliveryTimeFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] Categories
  {
    get => this.categoriesField;
    set => this.categoriesField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] GlobalCategories
  {
    get => this.globalCategoriesField;
    set => this.globalCategoriesField = value;
  }

  /// <remarks />
  public FlagStatusType FlagStatus
  {
    get => this.flagStatusField;
    set => this.flagStatusField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool FlagStatusSpecified
  {
    get => this.flagStatusFieldSpecified;
    set => this.flagStatusFieldSpecified = value;
  }

  /// <remarks />
  public FlagStatusType GlobalFlagStatus
  {
    get => this.globalFlagStatusField;
    set => this.globalFlagStatusField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool GlobalFlagStatusSpecified
  {
    get => this.globalFlagStatusFieldSpecified;
    set => this.globalFlagStatusFieldSpecified = value;
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
  public bool GlobalHasAttachments
  {
    get => this.globalHasAttachmentsField;
    set => this.globalHasAttachmentsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool GlobalHasAttachmentsSpecified
  {
    get => this.globalHasAttachmentsFieldSpecified;
    set => this.globalHasAttachmentsFieldSpecified = value;
  }

  /// <remarks />
  public int MessageCount
  {
    get => this.messageCountField;
    set => this.messageCountField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MessageCountSpecified
  {
    get => this.messageCountFieldSpecified;
    set => this.messageCountFieldSpecified = value;
  }

  /// <remarks />
  public int GlobalMessageCount
  {
    get => this.globalMessageCountField;
    set => this.globalMessageCountField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool GlobalMessageCountSpecified
  {
    get => this.globalMessageCountFieldSpecified;
    set => this.globalMessageCountFieldSpecified = value;
  }

  /// <remarks />
  public int UnreadCount
  {
    get => this.unreadCountField;
    set => this.unreadCountField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool UnreadCountSpecified
  {
    get => this.unreadCountFieldSpecified;
    set => this.unreadCountFieldSpecified = value;
  }

  /// <remarks />
  public int GlobalUnreadCount
  {
    get => this.globalUnreadCountField;
    set => this.globalUnreadCountField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool GlobalUnreadCountSpecified
  {
    get => this.globalUnreadCountFieldSpecified;
    set => this.globalUnreadCountFieldSpecified = value;
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
  public int GlobalSize
  {
    get => this.globalSizeField;
    set => this.globalSizeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool GlobalSizeSpecified
  {
    get => this.globalSizeFieldSpecified;
    set => this.globalSizeFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("ItemClass", IsNullable = false)]
  public string[] ItemClasses
  {
    get => this.itemClassesField;
    set => this.itemClassesField = value;
  }

  /// <remarks />
  [XmlArrayItem("ItemClass", IsNullable = false)]
  public string[] GlobalItemClasses
  {
    get => this.globalItemClassesField;
    set => this.globalItemClassesField = value;
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
  public ImportanceChoicesType GlobalImportance
  {
    get => this.globalImportanceField;
    set => this.globalImportanceField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool GlobalImportanceSpecified
  {
    get => this.globalImportanceFieldSpecified;
    set => this.globalImportanceFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("ItemId", typeof (ItemIdType), IsNullable = false)]
  [XmlArrayItem("OccurrenceItemId", typeof (OccurrenceItemIdType), IsNullable = false)]
  [XmlArrayItem("RecurringMasterItemId", typeof (RecurringMasterItemIdType), IsNullable = false)]
  [XmlArrayItem("RecurringMasterItemIdRanges", typeof (RecurringMasterItemIdRangesType), IsNullable = false)]
  public BaseItemIdType[] ItemIds
  {
    get => this.itemIdsField;
    set => this.itemIdsField = value;
  }

  /// <remarks />
  [XmlArrayItem("ItemId", typeof (ItemIdType), IsNullable = false)]
  [XmlArrayItem("OccurrenceItemId", typeof (OccurrenceItemIdType), IsNullable = false)]
  [XmlArrayItem("RecurringMasterItemId", typeof (RecurringMasterItemIdType), IsNullable = false)]
  [XmlArrayItem("RecurringMasterItemIdRanges", typeof (RecurringMasterItemIdRangesType), IsNullable = false)]
  public BaseItemIdType[] GlobalItemIds
  {
    get => this.globalItemIdsField;
    set => this.globalItemIdsField = value;
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
  [XmlElement(DataType = "base64Binary")]
  public byte[] InstanceKey
  {
    get => this.instanceKeyField;
    set => this.instanceKeyField = value;
  }

  /// <remarks />
  public string Preview
  {
    get => this.previewField;
    set => this.previewField = value;
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
  public MailboxSearchLocationType MailboxScope
  {
    get => this.mailboxScopeField;
    set => this.mailboxScopeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MailboxScopeSpecified
  {
    get => this.mailboxScopeFieldSpecified;
    set => this.mailboxScopeFieldSpecified = value;
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

  /// <remarks />
  public IconIndexType GlobalIconIndex
  {
    get => this.globalIconIndexField;
    set => this.globalIconIndexField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool GlobalIconIndexSpecified
  {
    get => this.globalIconIndexFieldSpecified;
    set => this.globalIconIndexFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("ItemId", typeof (ItemIdType), IsNullable = false)]
  [XmlArrayItem("OccurrenceItemId", typeof (OccurrenceItemIdType), IsNullable = false)]
  [XmlArrayItem("RecurringMasterItemId", typeof (RecurringMasterItemIdType), IsNullable = false)]
  [XmlArrayItem("RecurringMasterItemIdRanges", typeof (RecurringMasterItemIdRangesType), IsNullable = false)]
  public BaseItemIdType[] DraftItemIds
  {
    get => this.draftItemIdsField;
    set => this.draftItemIdsField = value;
  }

  /// <remarks />
  public bool HasIrm
  {
    get => this.hasIrmField;
    set => this.hasIrmField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool HasIrmSpecified
  {
    get => this.hasIrmFieldSpecified;
    set => this.hasIrmFieldSpecified = value;
  }

  /// <remarks />
  public bool GlobalHasIrm
  {
    get => this.globalHasIrmField;
    set => this.globalHasIrmField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool GlobalHasIrmSpecified
  {
    get => this.globalHasIrmFieldSpecified;
    set => this.globalHasIrmFieldSpecified = value;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.SearchPreviewItemType
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
public class SearchPreviewItemType
{
  private ItemIdType idField;
  private PreviewItemMailboxType mailboxField;
  private ItemIdType parentIdField;
  private string itemClassField;
  private string uniqueHashField;
  private string sortValueField;
  private string owaLinkField;
  private string senderField;
  private string[] toRecipientsField;
  private string[] ccRecipientsField;
  private string[] bccRecipientsField;
  private System.DateTime createdTimeField;
  private bool createdTimeFieldSpecified;
  private System.DateTime receivedTimeField;
  private bool receivedTimeFieldSpecified;
  private System.DateTime sentTimeField;
  private bool sentTimeFieldSpecified;
  private string subjectField;
  private long sizeField;
  private bool sizeFieldSpecified;
  private string previewField;
  private ImportanceChoicesType importanceField;
  private bool importanceFieldSpecified;
  private bool readField;
  private bool readFieldSpecified;
  private bool hasAttachmentField;
  private bool hasAttachmentFieldSpecified;
  private NonEmptyArrayOfExtendedPropertyType extendedPropertiesField;

  /// <remarks />
  public ItemIdType Id
  {
    get => this.idField;
    set => this.idField = value;
  }

  /// <remarks />
  public PreviewItemMailboxType Mailbox
  {
    get => this.mailboxField;
    set => this.mailboxField = value;
  }

  /// <remarks />
  public ItemIdType ParentId
  {
    get => this.parentIdField;
    set => this.parentIdField = value;
  }

  /// <remarks />
  public string ItemClass
  {
    get => this.itemClassField;
    set => this.itemClassField = value;
  }

  /// <remarks />
  public string UniqueHash
  {
    get => this.uniqueHashField;
    set => this.uniqueHashField = value;
  }

  /// <remarks />
  public string SortValue
  {
    get => this.sortValueField;
    set => this.sortValueField = value;
  }

  /// <remarks />
  public string OwaLink
  {
    get => this.owaLinkField;
    set => this.owaLinkField = value;
  }

  /// <remarks />
  public string Sender
  {
    get => this.senderField;
    set => this.senderField = value;
  }

  /// <remarks />
  [XmlArrayItem("SmtpAddress", IsNullable = false)]
  public string[] ToRecipients
  {
    get => this.toRecipientsField;
    set => this.toRecipientsField = value;
  }

  /// <remarks />
  [XmlArrayItem("SmtpAddress", IsNullable = false)]
  public string[] CcRecipients
  {
    get => this.ccRecipientsField;
    set => this.ccRecipientsField = value;
  }

  /// <remarks />
  [XmlArrayItem("SmtpAddress", IsNullable = false)]
  public string[] BccRecipients
  {
    get => this.bccRecipientsField;
    set => this.bccRecipientsField = value;
  }

  /// <remarks />
  public System.DateTime CreatedTime
  {
    get => this.createdTimeField;
    set => this.createdTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool CreatedTimeSpecified
  {
    get => this.createdTimeFieldSpecified;
    set => this.createdTimeFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime ReceivedTime
  {
    get => this.receivedTimeField;
    set => this.receivedTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ReceivedTimeSpecified
  {
    get => this.receivedTimeFieldSpecified;
    set => this.receivedTimeFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime SentTime
  {
    get => this.sentTimeField;
    set => this.sentTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool SentTimeSpecified
  {
    get => this.sentTimeFieldSpecified;
    set => this.sentTimeFieldSpecified = value;
  }

  /// <remarks />
  public string Subject
  {
    get => this.subjectField;
    set => this.subjectField = value;
  }

  /// <remarks />
  public long Size
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
  public string Preview
  {
    get => this.previewField;
    set => this.previewField = value;
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
  public bool Read
  {
    get => this.readField;
    set => this.readField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ReadSpecified
  {
    get => this.readFieldSpecified;
    set => this.readFieldSpecified = value;
  }

  /// <remarks />
  public bool HasAttachment
  {
    get => this.hasAttachmentField;
    set => this.hasAttachmentField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool HasAttachmentSpecified
  {
    get => this.hasAttachmentFieldSpecified;
    set => this.hasAttachmentFieldSpecified = value;
  }

  /// <remarks />
  public NonEmptyArrayOfExtendedPropertyType ExtendedProperties
  {
    get => this.extendedPropertiesField;
    set => this.extendedPropertiesField = value;
  }
}

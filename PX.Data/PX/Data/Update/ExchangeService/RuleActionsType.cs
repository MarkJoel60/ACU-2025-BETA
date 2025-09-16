// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.RuleActionsType
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
public class RuleActionsType
{
  private string[] assignCategoriesField;
  private TargetFolderIdType copyToFolderField;
  private bool deleteField;
  private bool deleteFieldSpecified;
  private EmailAddressType[] forwardAsAttachmentToRecipientsField;
  private EmailAddressType[] forwardToRecipientsField;
  private ImportanceChoicesType markImportanceField;
  private bool markImportanceFieldSpecified;
  private bool markAsReadField;
  private bool markAsReadFieldSpecified;
  private TargetFolderIdType moveToFolderField;
  private bool permanentDeleteField;
  private bool permanentDeleteFieldSpecified;
  private EmailAddressType[] redirectToRecipientsField;
  private EmailAddressType[] sendSMSAlertToRecipientsField;
  private ItemIdType serverReplyWithMessageField;
  private bool stopProcessingRulesField;
  private bool stopProcessingRulesFieldSpecified;

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] AssignCategories
  {
    get => this.assignCategoriesField;
    set => this.assignCategoriesField = value;
  }

  /// <remarks />
  public TargetFolderIdType CopyToFolder
  {
    get => this.copyToFolderField;
    set => this.copyToFolderField = value;
  }

  /// <remarks />
  public bool Delete
  {
    get => this.deleteField;
    set => this.deleteField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool DeleteSpecified
  {
    get => this.deleteFieldSpecified;
    set => this.deleteFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("Address", IsNullable = false)]
  public EmailAddressType[] ForwardAsAttachmentToRecipients
  {
    get => this.forwardAsAttachmentToRecipientsField;
    set => this.forwardAsAttachmentToRecipientsField = value;
  }

  /// <remarks />
  [XmlArrayItem("Address", IsNullable = false)]
  public EmailAddressType[] ForwardToRecipients
  {
    get => this.forwardToRecipientsField;
    set => this.forwardToRecipientsField = value;
  }

  /// <remarks />
  public ImportanceChoicesType MarkImportance
  {
    get => this.markImportanceField;
    set => this.markImportanceField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MarkImportanceSpecified
  {
    get => this.markImportanceFieldSpecified;
    set => this.markImportanceFieldSpecified = value;
  }

  /// <remarks />
  public bool MarkAsRead
  {
    get => this.markAsReadField;
    set => this.markAsReadField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MarkAsReadSpecified
  {
    get => this.markAsReadFieldSpecified;
    set => this.markAsReadFieldSpecified = value;
  }

  /// <remarks />
  public TargetFolderIdType MoveToFolder
  {
    get => this.moveToFolderField;
    set => this.moveToFolderField = value;
  }

  /// <remarks />
  public bool PermanentDelete
  {
    get => this.permanentDeleteField;
    set => this.permanentDeleteField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool PermanentDeleteSpecified
  {
    get => this.permanentDeleteFieldSpecified;
    set => this.permanentDeleteFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("Address", IsNullable = false)]
  public EmailAddressType[] RedirectToRecipients
  {
    get => this.redirectToRecipientsField;
    set => this.redirectToRecipientsField = value;
  }

  /// <remarks />
  [XmlArrayItem("Address", IsNullable = false)]
  public EmailAddressType[] SendSMSAlertToRecipients
  {
    get => this.sendSMSAlertToRecipientsField;
    set => this.sendSMSAlertToRecipientsField = value;
  }

  /// <remarks />
  public ItemIdType ServerReplyWithMessage
  {
    get => this.serverReplyWithMessageField;
    set => this.serverReplyWithMessageField = value;
  }

  /// <remarks />
  public bool StopProcessingRules
  {
    get => this.stopProcessingRulesField;
    set => this.stopProcessingRulesField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool StopProcessingRulesSpecified
  {
    get => this.stopProcessingRulesFieldSpecified;
    set => this.stopProcessingRulesFieldSpecified = value;
  }
}

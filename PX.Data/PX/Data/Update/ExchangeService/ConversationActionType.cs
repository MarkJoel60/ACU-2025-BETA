// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ConversationActionType
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
public class ConversationActionType
{
  private ConversationActionTypeType actionField;
  private ItemIdType conversationIdField;
  private TargetFolderIdType contextFolderIdField;
  private System.DateTime conversationLastSyncTimeField;
  private bool conversationLastSyncTimeFieldSpecified;
  private bool processRightAwayField;
  private bool processRightAwayFieldSpecified;
  private TargetFolderIdType destinationFolderIdField;
  private string[] categoriesField;
  private bool enableAlwaysDeleteField;
  private bool enableAlwaysDeleteFieldSpecified;
  private bool isReadField;
  private bool isReadFieldSpecified;
  private DisposalType deleteTypeField;
  private bool deleteTypeFieldSpecified;
  private RetentionType retentionPolicyTypeField;
  private bool retentionPolicyTypeFieldSpecified;
  private string retentionPolicyTagIdField;
  private FlagType flagField;
  private bool suppressReadReceiptsField;
  private bool suppressReadReceiptsFieldSpecified;

  /// <remarks />
  public ConversationActionTypeType Action
  {
    get => this.actionField;
    set => this.actionField = value;
  }

  /// <remarks />
  public ItemIdType ConversationId
  {
    get => this.conversationIdField;
    set => this.conversationIdField = value;
  }

  /// <remarks />
  public TargetFolderIdType ContextFolderId
  {
    get => this.contextFolderIdField;
    set => this.contextFolderIdField = value;
  }

  /// <remarks />
  public System.DateTime ConversationLastSyncTime
  {
    get => this.conversationLastSyncTimeField;
    set => this.conversationLastSyncTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ConversationLastSyncTimeSpecified
  {
    get => this.conversationLastSyncTimeFieldSpecified;
    set => this.conversationLastSyncTimeFieldSpecified = value;
  }

  /// <remarks />
  public bool ProcessRightAway
  {
    get => this.processRightAwayField;
    set => this.processRightAwayField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ProcessRightAwaySpecified
  {
    get => this.processRightAwayFieldSpecified;
    set => this.processRightAwayFieldSpecified = value;
  }

  /// <remarks />
  public TargetFolderIdType DestinationFolderId
  {
    get => this.destinationFolderIdField;
    set => this.destinationFolderIdField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] Categories
  {
    get => this.categoriesField;
    set => this.categoriesField = value;
  }

  /// <remarks />
  public bool EnableAlwaysDelete
  {
    get => this.enableAlwaysDeleteField;
    set => this.enableAlwaysDeleteField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool EnableAlwaysDeleteSpecified
  {
    get => this.enableAlwaysDeleteFieldSpecified;
    set => this.enableAlwaysDeleteFieldSpecified = value;
  }

  /// <remarks />
  public bool IsRead
  {
    get => this.isReadField;
    set => this.isReadField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsReadSpecified
  {
    get => this.isReadFieldSpecified;
    set => this.isReadFieldSpecified = value;
  }

  /// <remarks />
  public DisposalType DeleteType
  {
    get => this.deleteTypeField;
    set => this.deleteTypeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool DeleteTypeSpecified
  {
    get => this.deleteTypeFieldSpecified;
    set => this.deleteTypeFieldSpecified = value;
  }

  /// <remarks />
  public RetentionType RetentionPolicyType
  {
    get => this.retentionPolicyTypeField;
    set => this.retentionPolicyTypeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool RetentionPolicyTypeSpecified
  {
    get => this.retentionPolicyTypeFieldSpecified;
    set => this.retentionPolicyTypeFieldSpecified = value;
  }

  /// <remarks />
  public string RetentionPolicyTagId
  {
    get => this.retentionPolicyTagIdField;
    set => this.retentionPolicyTagIdField = value;
  }

  /// <remarks />
  public FlagType Flag
  {
    get => this.flagField;
    set => this.flagField = value;
  }

  /// <remarks />
  public bool SuppressReadReceipts
  {
    get => this.suppressReadReceiptsField;
    set => this.suppressReadReceiptsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool SuppressReadReceiptsSpecified
  {
    get => this.suppressReadReceiptsFieldSpecified;
    set => this.suppressReadReceiptsFieldSpecified = value;
  }
}

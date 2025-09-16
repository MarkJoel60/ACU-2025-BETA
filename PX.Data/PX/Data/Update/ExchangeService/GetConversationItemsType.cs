// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.GetConversationItemsType
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
public class GetConversationItemsType : BaseRequestType
{
  private ItemResponseShapeType itemShapeField;
  private BaseFolderIdType[] foldersToIgnoreField;
  private int maxItemsToReturnField;
  private bool maxItemsToReturnFieldSpecified;
  private ConversationNodeSortOrder sortOrderField;
  private bool sortOrderFieldSpecified;
  private MailboxSearchLocationType mailboxScopeField;
  private bool mailboxScopeFieldSpecified;
  private ConversationRequestType[] conversationsField;

  /// <remarks />
  public ItemResponseShapeType ItemShape
  {
    get => this.itemShapeField;
    set => this.itemShapeField = value;
  }

  /// <remarks />
  [XmlArrayItem("DistinguishedFolderId", typeof (DistinguishedFolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  [XmlArrayItem("FolderId", typeof (FolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public BaseFolderIdType[] FoldersToIgnore
  {
    get => this.foldersToIgnoreField;
    set => this.foldersToIgnoreField = value;
  }

  /// <remarks />
  public int MaxItemsToReturn
  {
    get => this.maxItemsToReturnField;
    set => this.maxItemsToReturnField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MaxItemsToReturnSpecified
  {
    get => this.maxItemsToReturnFieldSpecified;
    set => this.maxItemsToReturnFieldSpecified = value;
  }

  /// <remarks />
  public ConversationNodeSortOrder SortOrder
  {
    get => this.sortOrderField;
    set => this.sortOrderField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool SortOrderSpecified
  {
    get => this.sortOrderFieldSpecified;
    set => this.sortOrderFieldSpecified = value;
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
  [XmlArrayItem("Conversation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public ConversationRequestType[] Conversations
  {
    get => this.conversationsField;
    set => this.conversationsField = value;
  }
}

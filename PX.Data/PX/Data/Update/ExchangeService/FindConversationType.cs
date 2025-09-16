// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.FindConversationType
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
public class FindConversationType : BaseRequestType
{
  private BasePagingType itemField;
  private FieldOrderType[] sortOrderField;
  private TargetFolderIdType parentFolderIdField;
  private MailboxSearchLocationType mailboxScopeField;
  private bool mailboxScopeFieldSpecified;
  private QueryStringType queryStringField;
  private ConversationResponseShapeType conversationShapeField;
  private ConversationQueryTraversalType traversalField;
  private bool traversalFieldSpecified;
  private ViewFilterType viewFilterField;
  private bool viewFilterFieldSpecified;

  /// <remarks />
  [XmlElement("IndexedPageItemView", typeof (IndexedPageViewType))]
  [XmlElement("SeekToConditionPageItemView", typeof (SeekToConditionPageViewType))]
  public BasePagingType Item
  {
    get => this.itemField;
    set => this.itemField = value;
  }

  /// <remarks />
  [XmlArrayItem("FieldOrder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public FieldOrderType[] SortOrder
  {
    get => this.sortOrderField;
    set => this.sortOrderField = value;
  }

  /// <remarks />
  public TargetFolderIdType ParentFolderId
  {
    get => this.parentFolderIdField;
    set => this.parentFolderIdField = value;
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
  public QueryStringType QueryString
  {
    get => this.queryStringField;
    set => this.queryStringField = value;
  }

  /// <remarks />
  public ConversationResponseShapeType ConversationShape
  {
    get => this.conversationShapeField;
    set => this.conversationShapeField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public ConversationQueryTraversalType Traversal
  {
    get => this.traversalField;
    set => this.traversalField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool TraversalSpecified
  {
    get => this.traversalFieldSpecified;
    set => this.traversalFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public ViewFilterType ViewFilter
  {
    get => this.viewFilterField;
    set => this.viewFilterField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ViewFilterSpecified
  {
    get => this.viewFilterFieldSpecified;
    set => this.viewFilterFieldSpecified = value;
  }
}

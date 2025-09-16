// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.FindConversationResponseMessageType
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
public class FindConversationResponseMessageType : ResponseMessageType
{
  private ConversationType[] conversationsField;
  private HighlightTermType[] highlightTermsField;
  private int totalConversationsInViewField;
  private bool totalConversationsInViewFieldSpecified;
  private int indexedOffsetField;
  private bool indexedOffsetFieldSpecified;

  /// <remarks />
  [XmlArrayItem("Conversation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public ConversationType[] Conversations
  {
    get => this.conversationsField;
    set => this.conversationsField = value;
  }

  /// <remarks />
  [XmlArrayItem("Term", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public HighlightTermType[] HighlightTerms
  {
    get => this.highlightTermsField;
    set => this.highlightTermsField = value;
  }

  /// <remarks />
  public int TotalConversationsInView
  {
    get => this.totalConversationsInViewField;
    set => this.totalConversationsInViewField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool TotalConversationsInViewSpecified
  {
    get => this.totalConversationsInViewFieldSpecified;
    set => this.totalConversationsInViewFieldSpecified = value;
  }

  /// <remarks />
  public int IndexedOffset
  {
    get => this.indexedOffsetField;
    set => this.indexedOffsetField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IndexedOffsetSpecified
  {
    get => this.indexedOffsetFieldSpecified;
    set => this.indexedOffsetFieldSpecified = value;
  }
}

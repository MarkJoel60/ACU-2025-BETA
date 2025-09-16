// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.PostItemType
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
public class PostItemType : ItemType
{
  private byte[] conversationIndexField;
  private string conversationTopicField;
  private SingleRecipientType fromField;
  private string internetMessageIdField;
  private bool isReadField;
  private bool isReadFieldSpecified;
  private System.DateTime postedTimeField;
  private bool postedTimeFieldSpecified;
  private string referencesField;
  private SingleRecipientType senderField;

  /// <remarks />
  [XmlElement(DataType = "base64Binary")]
  public byte[] ConversationIndex
  {
    get => this.conversationIndexField;
    set => this.conversationIndexField = value;
  }

  /// <remarks />
  public string ConversationTopic
  {
    get => this.conversationTopicField;
    set => this.conversationTopicField = value;
  }

  /// <remarks />
  public SingleRecipientType From
  {
    get => this.fromField;
    set => this.fromField = value;
  }

  /// <remarks />
  public string InternetMessageId
  {
    get => this.internetMessageIdField;
    set => this.internetMessageIdField = value;
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
  public System.DateTime PostedTime
  {
    get => this.postedTimeField;
    set => this.postedTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool PostedTimeSpecified
  {
    get => this.postedTimeFieldSpecified;
    set => this.postedTimeFieldSpecified = value;
  }

  /// <remarks />
  public string References
  {
    get => this.referencesField;
    set => this.referencesField = value;
  }

  /// <remarks />
  public SingleRecipientType Sender
  {
    get => this.senderField;
    set => this.senderField = value;
  }
}

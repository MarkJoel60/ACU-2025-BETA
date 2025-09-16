// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.AttachmentType
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
[XmlInclude(typeof (ReferenceAttachmentType))]
[XmlInclude(typeof (FileAttachmentType))]
[XmlInclude(typeof (ItemAttachmentType))]
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public class AttachmentType
{
  private AttachmentIdType attachmentIdField;
  private string nameField;
  private string contentTypeField;
  private string contentIdField;
  private string contentLocationField;
  private int sizeField;
  private bool sizeFieldSpecified;
  private System.DateTime lastModifiedTimeField;
  private bool lastModifiedTimeFieldSpecified;
  private bool isInlineField;
  private bool isInlineFieldSpecified;

  /// <remarks />
  public AttachmentIdType AttachmentId
  {
    get => this.attachmentIdField;
    set => this.attachmentIdField = value;
  }

  /// <remarks />
  public string Name
  {
    get => this.nameField;
    set => this.nameField = value;
  }

  /// <remarks />
  public string ContentType
  {
    get => this.contentTypeField;
    set => this.contentTypeField = value;
  }

  /// <remarks />
  public string ContentId
  {
    get => this.contentIdField;
    set => this.contentIdField = value;
  }

  /// <remarks />
  public string ContentLocation
  {
    get => this.contentLocationField;
    set => this.contentLocationField = value;
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
  public bool IsInline
  {
    get => this.isInlineField;
    set => this.isInlineField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsInlineSpecified
  {
    get => this.isInlineFieldSpecified;
    set => this.isInlineFieldSpecified = value;
  }
}

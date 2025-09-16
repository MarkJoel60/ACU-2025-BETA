// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.CreateAttachmentType
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
public class CreateAttachmentType : BaseRequestType
{
  private ItemIdType parentItemIdField;
  private AttachmentType[] attachmentsField;

  /// <remarks />
  public ItemIdType ParentItemId
  {
    get => this.parentItemIdField;
    set => this.parentItemIdField = value;
  }

  /// <remarks />
  [XmlArrayItem("FileAttachment", typeof (FileAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  [XmlArrayItem("ItemAttachment", typeof (ItemAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public AttachmentType[] Attachments
  {
    get => this.attachmentsField;
    set => this.attachmentsField = value;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.UpdateItemInRecoverableItemsType
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
public class UpdateItemInRecoverableItemsType : BaseRequestType
{
  private ItemIdType itemIdField;
  private ItemChangeDescriptionType[] updatesField;
  private AttachmentType[] attachmentsField;
  private bool makeItemImmutableField;
  private bool makeItemImmutableFieldSpecified;

  /// <remarks />
  public ItemIdType ItemId
  {
    get => this.itemIdField;
    set => this.itemIdField = value;
  }

  /// <remarks />
  [XmlArrayItem("AppendToItemField", typeof (AppendToItemFieldType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  [XmlArrayItem("DeleteItemField", typeof (DeleteItemFieldType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  [XmlArrayItem("SetItemField", typeof (SetItemFieldType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public ItemChangeDescriptionType[] Updates
  {
    get => this.updatesField;
    set => this.updatesField = value;
  }

  /// <remarks />
  [XmlArrayItem("FileAttachment", typeof (FileAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  [XmlArrayItem("ItemAttachment", typeof (ItemAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public AttachmentType[] Attachments
  {
    get => this.attachmentsField;
    set => this.attachmentsField = value;
  }

  /// <remarks />
  public bool MakeItemImmutable
  {
    get => this.makeItemImmutableField;
    set => this.makeItemImmutableField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MakeItemImmutableSpecified
  {
    get => this.makeItemImmutableFieldSpecified;
    set => this.makeItemImmutableFieldSpecified = value;
  }
}

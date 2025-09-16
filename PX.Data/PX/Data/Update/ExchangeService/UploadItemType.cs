// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.UploadItemType
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
public class UploadItemType
{
  private FolderIdType parentFolderIdField;
  private ItemIdType itemIdField;
  private byte[] dataField;
  private CreateActionType createActionField;
  private bool isAssociatedField;
  private bool isAssociatedFieldSpecified;

  /// <remarks />
  public FolderIdType ParentFolderId
  {
    get => this.parentFolderIdField;
    set => this.parentFolderIdField = value;
  }

  /// <remarks />
  public ItemIdType ItemId
  {
    get => this.itemIdField;
    set => this.itemIdField = value;
  }

  /// <remarks />
  [XmlElement(DataType = "base64Binary")]
  public byte[] Data
  {
    get => this.dataField;
    set => this.dataField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public CreateActionType CreateAction
  {
    get => this.createActionField;
    set => this.createActionField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public bool IsAssociated
  {
    get => this.isAssociatedField;
    set => this.isAssociatedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsAssociatedSpecified
  {
    get => this.isAssociatedFieldSpecified;
    set => this.isAssociatedFieldSpecified = value;
  }
}

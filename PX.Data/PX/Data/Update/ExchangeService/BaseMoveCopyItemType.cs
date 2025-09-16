// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.BaseMoveCopyItemType
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
[XmlInclude(typeof (CopyItemType))]
[XmlInclude(typeof (MoveItemType))]
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
[Serializable]
public class BaseMoveCopyItemType : BaseRequestType
{
  private TargetFolderIdType toFolderIdField;
  private BaseItemIdType[] itemIdsField;
  private bool returnNewItemIdsField;
  private bool returnNewItemIdsFieldSpecified;

  /// <remarks />
  public TargetFolderIdType ToFolderId
  {
    get => this.toFolderIdField;
    set => this.toFolderIdField = value;
  }

  /// <remarks />
  [XmlArrayItem("ItemId", typeof (ItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  [XmlArrayItem("OccurrenceItemId", typeof (OccurrenceItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  [XmlArrayItem("RecurringMasterItemId", typeof (RecurringMasterItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  [XmlArrayItem("RecurringMasterItemIdRanges", typeof (RecurringMasterItemIdRangesType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public BaseItemIdType[] ItemIds
  {
    get => this.itemIdsField;
    set => this.itemIdsField = value;
  }

  /// <remarks />
  public bool ReturnNewItemIds
  {
    get => this.returnNewItemIdsField;
    set => this.returnNewItemIdsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ReturnNewItemIdsSpecified
  {
    get => this.returnNewItemIdsFieldSpecified;
    set => this.returnNewItemIdsFieldSpecified = value;
  }
}

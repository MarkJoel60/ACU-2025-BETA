// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.SyncFolderItemsType
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
public class SyncFolderItemsType : BaseRequestType
{
  private ItemResponseShapeType itemShapeField;
  private TargetFolderIdType syncFolderIdField;
  private string syncStateField;
  private ItemIdType[] ignoreField;
  private int maxChangesReturnedField;
  private SyncFolderItemsScopeType syncScopeField;
  private bool syncScopeFieldSpecified;

  /// <remarks />
  public ItemResponseShapeType ItemShape
  {
    get => this.itemShapeField;
    set => this.itemShapeField = value;
  }

  /// <remarks />
  public TargetFolderIdType SyncFolderId
  {
    get => this.syncFolderIdField;
    set => this.syncFolderIdField = value;
  }

  /// <remarks />
  public string SyncState
  {
    get => this.syncStateField;
    set => this.syncStateField = value;
  }

  /// <remarks />
  [XmlArrayItem("ItemId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public ItemIdType[] Ignore
  {
    get => this.ignoreField;
    set => this.ignoreField = value;
  }

  /// <remarks />
  public int MaxChangesReturned
  {
    get => this.maxChangesReturnedField;
    set => this.maxChangesReturnedField = value;
  }

  /// <remarks />
  public SyncFolderItemsScopeType SyncScope
  {
    get => this.syncScopeField;
    set => this.syncScopeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool SyncScopeSpecified
  {
    get => this.syncScopeFieldSpecified;
    set => this.syncScopeFieldSpecified = value;
  }
}

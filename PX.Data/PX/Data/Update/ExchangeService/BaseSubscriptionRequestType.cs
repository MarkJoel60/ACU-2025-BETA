// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.BaseSubscriptionRequestType
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
[XmlInclude(typeof (PullSubscriptionRequestType))]
[XmlInclude(typeof (PushSubscriptionRequestType))]
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public abstract class BaseSubscriptionRequestType
{
  private BaseFolderIdType[] folderIdsField;
  private NotificationEventTypeType[] eventTypesField;
  private string watermarkField;
  private bool subscribeToAllFoldersField;
  private bool subscribeToAllFoldersFieldSpecified;

  /// <remarks />
  [XmlArrayItem("DistinguishedFolderId", typeof (DistinguishedFolderIdType), IsNullable = false)]
  [XmlArrayItem("FolderId", typeof (FolderIdType), IsNullable = false)]
  public BaseFolderIdType[] FolderIds
  {
    get => this.folderIdsField;
    set => this.folderIdsField = value;
  }

  /// <remarks />
  [XmlArrayItem("EventType", IsNullable = false)]
  public NotificationEventTypeType[] EventTypes
  {
    get => this.eventTypesField;
    set => this.eventTypesField = value;
  }

  /// <remarks />
  public string Watermark
  {
    get => this.watermarkField;
    set => this.watermarkField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public bool SubscribeToAllFolders
  {
    get => this.subscribeToAllFoldersField;
    set => this.subscribeToAllFoldersField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool SubscribeToAllFoldersSpecified
  {
    get => this.subscribeToAllFoldersFieldSpecified;
    set => this.subscribeToAllFoldersFieldSpecified = value;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.UpdateItemType
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
public class UpdateItemType : BaseRequestType
{
  private TargetFolderIdType savedItemFolderIdField;
  private ItemChangeType[] itemChangesField;
  private ConflictResolutionType conflictResolutionField;
  private MessageDispositionType messageDispositionField;
  private bool messageDispositionFieldSpecified;
  private CalendarItemUpdateOperationType sendMeetingInvitationsOrCancellationsField;
  private bool sendMeetingInvitationsOrCancellationsFieldSpecified;
  private bool suppressReadReceiptsField;
  private bool suppressReadReceiptsFieldSpecified;

  /// <remarks />
  public TargetFolderIdType SavedItemFolderId
  {
    get => this.savedItemFolderIdField;
    set => this.savedItemFolderIdField = value;
  }

  /// <remarks />
  [XmlArrayItem("ItemChange", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public ItemChangeType[] ItemChanges
  {
    get => this.itemChangesField;
    set => this.itemChangesField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public ConflictResolutionType ConflictResolution
  {
    get => this.conflictResolutionField;
    set => this.conflictResolutionField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public MessageDispositionType MessageDisposition
  {
    get => this.messageDispositionField;
    set => this.messageDispositionField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MessageDispositionSpecified
  {
    get => this.messageDispositionFieldSpecified;
    set => this.messageDispositionFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public CalendarItemUpdateOperationType SendMeetingInvitationsOrCancellations
  {
    get => this.sendMeetingInvitationsOrCancellationsField;
    set => this.sendMeetingInvitationsOrCancellationsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool SendMeetingInvitationsOrCancellationsSpecified
  {
    get => this.sendMeetingInvitationsOrCancellationsFieldSpecified;
    set => this.sendMeetingInvitationsOrCancellationsFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public bool SuppressReadReceipts
  {
    get => this.suppressReadReceiptsField;
    set => this.suppressReadReceiptsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool SuppressReadReceiptsSpecified
  {
    get => this.suppressReadReceiptsFieldSpecified;
    set => this.suppressReadReceiptsFieldSpecified = value;
  }
}

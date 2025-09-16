// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.DeleteItemType
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
public class DeleteItemType : BaseRequestType
{
  private BaseItemIdType[] itemIdsField;
  private DisposalType deleteTypeField;
  private CalendarItemCreateOrDeleteOperationType sendMeetingCancellationsField;
  private bool sendMeetingCancellationsFieldSpecified;
  private AffectedTaskOccurrencesType affectedTaskOccurrencesField;
  private bool affectedTaskOccurrencesFieldSpecified;
  private bool suppressReadReceiptsField;
  private bool suppressReadReceiptsFieldSpecified;

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
  [XmlAttribute]
  public DisposalType DeleteType
  {
    get => this.deleteTypeField;
    set => this.deleteTypeField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public CalendarItemCreateOrDeleteOperationType SendMeetingCancellations
  {
    get => this.sendMeetingCancellationsField;
    set => this.sendMeetingCancellationsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool SendMeetingCancellationsSpecified
  {
    get => this.sendMeetingCancellationsFieldSpecified;
    set => this.sendMeetingCancellationsFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public AffectedTaskOccurrencesType AffectedTaskOccurrences
  {
    get => this.affectedTaskOccurrencesField;
    set => this.affectedTaskOccurrencesField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool AffectedTaskOccurrencesSpecified
  {
    get => this.affectedTaskOccurrencesFieldSpecified;
    set => this.affectedTaskOccurrencesFieldSpecified = value;
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

// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.RecipientTrackingEventType
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
public class RecipientTrackingEventType
{
  private System.DateTime dateField;
  private EmailAddressType recipientField;
  private string deliveryStatusField;
  private string eventDescriptionField;
  private string[] eventDataField;
  private string serverField;
  private string internalIdField;
  private bool bccRecipientField;
  private bool bccRecipientFieldSpecified;
  private bool hiddenRecipientField;
  private bool hiddenRecipientFieldSpecified;
  private string uniquePathIdField;
  private string rootAddressField;
  private TrackingPropertyType[] propertiesField;

  /// <remarks />
  public System.DateTime Date
  {
    get => this.dateField;
    set => this.dateField = value;
  }

  /// <remarks />
  public EmailAddressType Recipient
  {
    get => this.recipientField;
    set => this.recipientField = value;
  }

  /// <remarks />
  public string DeliveryStatus
  {
    get => this.deliveryStatusField;
    set => this.deliveryStatusField = value;
  }

  /// <remarks />
  public string EventDescription
  {
    get => this.eventDescriptionField;
    set => this.eventDescriptionField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] EventData
  {
    get => this.eventDataField;
    set => this.eventDataField = value;
  }

  /// <remarks />
  public string Server
  {
    get => this.serverField;
    set => this.serverField = value;
  }

  /// <remarks />
  [XmlElement(DataType = "nonNegativeInteger")]
  public string InternalId
  {
    get => this.internalIdField;
    set => this.internalIdField = value;
  }

  /// <remarks />
  public bool BccRecipient
  {
    get => this.bccRecipientField;
    set => this.bccRecipientField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool BccRecipientSpecified
  {
    get => this.bccRecipientFieldSpecified;
    set => this.bccRecipientFieldSpecified = value;
  }

  /// <remarks />
  public bool HiddenRecipient
  {
    get => this.hiddenRecipientField;
    set => this.hiddenRecipientField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool HiddenRecipientSpecified
  {
    get => this.hiddenRecipientFieldSpecified;
    set => this.hiddenRecipientFieldSpecified = value;
  }

  /// <remarks />
  public string UniquePathId
  {
    get => this.uniquePathIdField;
    set => this.uniquePathIdField = value;
  }

  /// <remarks />
  public string RootAddress
  {
    get => this.rootAddressField;
    set => this.rootAddressField = value;
  }

  /// <remarks />
  [XmlArrayItem(IsNullable = false)]
  public TrackingPropertyType[] Properties
  {
    get => this.propertiesField;
    set => this.propertiesField = value;
  }
}

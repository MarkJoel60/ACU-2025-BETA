// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.GetStreamingEventsResponseMessageType
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
public class GetStreamingEventsResponseMessageType : ResponseMessageType
{
  private NotificationType[] notificationsField;
  private string[] errorSubscriptionIdsField;
  private ConnectionStatusType connectionStatusField;
  private bool connectionStatusFieldSpecified;

  /// <remarks />
  [XmlArrayItem("Notification", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public NotificationType[] Notifications
  {
    get => this.notificationsField;
    set => this.notificationsField = value;
  }

  /// <remarks />
  [XmlArrayItem("SubscriptionId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public string[] ErrorSubscriptionIds
  {
    get => this.errorSubscriptionIdsField;
    set => this.errorSubscriptionIdsField = value;
  }

  /// <remarks />
  public ConnectionStatusType ConnectionStatus
  {
    get => this.connectionStatusField;
    set => this.connectionStatusField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ConnectionStatusSpecified
  {
    get => this.connectionStatusFieldSpecified;
    set => this.connectionStatusFieldSpecified = value;
  }
}

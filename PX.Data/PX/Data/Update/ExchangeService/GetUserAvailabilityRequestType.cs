// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.GetUserAvailabilityRequestType
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
public class GetUserAvailabilityRequestType : BaseRequestType
{
  private SerializableTimeZone timeZoneField;
  private MailboxData[] mailboxDataArrayField;
  private FreeBusyViewOptionsType freeBusyViewOptionsField;
  private SuggestionsViewOptionsType suggestionsViewOptionsField;

  /// <remarks />
  [XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
  public SerializableTimeZone TimeZone
  {
    get => this.timeZoneField;
    set => this.timeZoneField = value;
  }

  /// <remarks />
  [XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
  public MailboxData[] MailboxDataArray
  {
    get => this.mailboxDataArrayField;
    set => this.mailboxDataArrayField = value;
  }

  /// <remarks />
  [XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
  public FreeBusyViewOptionsType FreeBusyViewOptions
  {
    get => this.freeBusyViewOptionsField;
    set => this.freeBusyViewOptionsField = value;
  }

  /// <remarks />
  [XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
  public SuggestionsViewOptionsType SuggestionsViewOptions
  {
    get => this.suggestionsViewOptionsField;
    set => this.suggestionsViewOptionsField = value;
  }
}

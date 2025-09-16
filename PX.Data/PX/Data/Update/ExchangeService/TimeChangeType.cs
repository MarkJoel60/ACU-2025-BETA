// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.TimeChangeType
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
public class TimeChangeType
{
  private string offsetField;
  private object itemField;
  private System.DateTime timeField;
  private string timeZoneNameField;

  /// <remarks />
  [XmlElement(DataType = "duration")]
  public string Offset
  {
    get => this.offsetField;
    set => this.offsetField = value;
  }

  /// <remarks />
  [XmlElement("AbsoluteDate", typeof (System.DateTime), DataType = "date")]
  [XmlElement("RelativeYearlyRecurrence", typeof (RelativeYearlyRecurrencePatternType))]
  public object Item
  {
    get => this.itemField;
    set => this.itemField = value;
  }

  /// <remarks />
  [XmlElement(DataType = "time")]
  public System.DateTime Time
  {
    get => this.timeField;
    set => this.timeField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public string TimeZoneName
  {
    get => this.timeZoneNameField;
    set => this.timeZoneNameField = value;
  }
}

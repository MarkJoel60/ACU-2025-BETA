// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.RulePredicateDateRangeType
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
public class RulePredicateDateRangeType
{
  private System.DateTime startDateTimeField;
  private bool startDateTimeFieldSpecified;
  private System.DateTime endDateTimeField;
  private bool endDateTimeFieldSpecified;

  /// <remarks />
  public System.DateTime StartDateTime
  {
    get => this.startDateTimeField;
    set => this.startDateTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool StartDateTimeSpecified
  {
    get => this.startDateTimeFieldSpecified;
    set => this.startDateTimeFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime EndDateTime
  {
    get => this.endDateTimeField;
    set => this.endDateTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool EndDateTimeSpecified
  {
    get => this.endDateTimeFieldSpecified;
    set => this.endDateTimeFieldSpecified = value;
  }
}

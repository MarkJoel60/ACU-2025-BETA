// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.OccurrencesRangeType
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
public class OccurrencesRangeType
{
  private System.DateTime startField;
  private bool startFieldSpecified;
  private System.DateTime endField;
  private bool endFieldSpecified;
  private int countField;
  private bool countFieldSpecified;
  private bool compareOriginalStartTimeField;
  private bool compareOriginalStartTimeFieldSpecified;

  /// <remarks />
  [XmlAttribute]
  public System.DateTime Start
  {
    get => this.startField;
    set => this.startField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool StartSpecified
  {
    get => this.startFieldSpecified;
    set => this.startFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public System.DateTime End
  {
    get => this.endField;
    set => this.endField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool EndSpecified
  {
    get => this.endFieldSpecified;
    set => this.endFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public int Count
  {
    get => this.countField;
    set => this.countField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool CountSpecified
  {
    get => this.countFieldSpecified;
    set => this.countFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public bool CompareOriginalStartTime
  {
    get => this.compareOriginalStartTimeField;
    set => this.compareOriginalStartTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool CompareOriginalStartTimeSpecified
  {
    get => this.compareOriginalStartTimeFieldSpecified;
    set => this.compareOriginalStartTimeFieldSpecified = value;
  }
}

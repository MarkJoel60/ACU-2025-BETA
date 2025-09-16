// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ChangeHighlightsType
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
public class ChangeHighlightsType
{
  private bool hasLocationChangedField;
  private bool hasLocationChangedFieldSpecified;
  private string locationField;
  private bool hasStartTimeChangedField;
  private bool hasStartTimeChangedFieldSpecified;
  private System.DateTime startField;
  private bool startFieldSpecified;
  private bool hasEndTimeChangedField;
  private bool hasEndTimeChangedFieldSpecified;
  private System.DateTime endField;
  private bool endFieldSpecified;

  /// <remarks />
  public bool HasLocationChanged
  {
    get => this.hasLocationChangedField;
    set => this.hasLocationChangedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool HasLocationChangedSpecified
  {
    get => this.hasLocationChangedFieldSpecified;
    set => this.hasLocationChangedFieldSpecified = value;
  }

  /// <remarks />
  public string Location
  {
    get => this.locationField;
    set => this.locationField = value;
  }

  /// <remarks />
  public bool HasStartTimeChanged
  {
    get => this.hasStartTimeChangedField;
    set => this.hasStartTimeChangedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool HasStartTimeChangedSpecified
  {
    get => this.hasStartTimeChangedFieldSpecified;
    set => this.hasStartTimeChangedFieldSpecified = value;
  }

  /// <remarks />
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
  public bool HasEndTimeChanged
  {
    get => this.hasEndTimeChangedField;
    set => this.hasEndTimeChangedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool HasEndTimeChangedSpecified
  {
    get => this.hasEndTimeChangedFieldSpecified;
    set => this.hasEndTimeChangedFieldSpecified = value;
  }

  /// <remarks />
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
}

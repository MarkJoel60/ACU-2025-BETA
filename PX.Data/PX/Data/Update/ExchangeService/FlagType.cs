// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.FlagType
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
public class FlagType
{
  private FlagStatusType flagStatusField;
  private System.DateTime startDateField;
  private bool startDateFieldSpecified;
  private System.DateTime dueDateField;
  private bool dueDateFieldSpecified;
  private System.DateTime completeDateField;
  private bool completeDateFieldSpecified;

  /// <remarks />
  public FlagStatusType FlagStatus
  {
    get => this.flagStatusField;
    set => this.flagStatusField = value;
  }

  /// <remarks />
  public System.DateTime StartDate
  {
    get => this.startDateField;
    set => this.startDateField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool StartDateSpecified
  {
    get => this.startDateFieldSpecified;
    set => this.startDateFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime DueDate
  {
    get => this.dueDateField;
    set => this.dueDateField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool DueDateSpecified
  {
    get => this.dueDateFieldSpecified;
    set => this.dueDateFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime CompleteDate
  {
    get => this.completeDateField;
    set => this.completeDateField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool CompleteDateSpecified
  {
    get => this.completeDateFieldSpecified;
    set => this.completeDateFieldSpecified = value;
  }
}

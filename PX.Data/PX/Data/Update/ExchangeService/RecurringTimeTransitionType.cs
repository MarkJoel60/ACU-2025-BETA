// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.RecurringTimeTransitionType
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
[XmlInclude(typeof (RecurringDayTransitionType))]
[XmlInclude(typeof (RecurringDateTransitionType))]
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public abstract class RecurringTimeTransitionType : TransitionType
{
  private string timeOffsetField;
  private int monthField;

  /// <remarks />
  [XmlElement(DataType = "duration")]
  public string TimeOffset
  {
    get => this.timeOffsetField;
    set => this.timeOffsetField = value;
  }

  /// <remarks />
  public int Month
  {
    get => this.monthField;
    set => this.monthField = value;
  }
}

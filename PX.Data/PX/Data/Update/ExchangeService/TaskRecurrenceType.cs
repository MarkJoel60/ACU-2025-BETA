// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.TaskRecurrenceType
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
public class TaskRecurrenceType
{
  private RecurrencePatternBaseType itemField;
  private RecurrenceRangeBaseType item1Field;

  /// <remarks />
  [XmlElement("AbsoluteMonthlyRecurrence", typeof (AbsoluteMonthlyRecurrencePatternType))]
  [XmlElement("AbsoluteYearlyRecurrence", typeof (AbsoluteYearlyRecurrencePatternType))]
  [XmlElement("DailyRecurrence", typeof (DailyRecurrencePatternType))]
  [XmlElement("DailyRegeneration", typeof (DailyRegeneratingPatternType))]
  [XmlElement("MonthlyRegeneration", typeof (MonthlyRegeneratingPatternType))]
  [XmlElement("RelativeMonthlyRecurrence", typeof (RelativeMonthlyRecurrencePatternType))]
  [XmlElement("RelativeYearlyRecurrence", typeof (RelativeYearlyRecurrencePatternType))]
  [XmlElement("WeeklyRecurrence", typeof (WeeklyRecurrencePatternType))]
  [XmlElement("WeeklyRegeneration", typeof (WeeklyRegeneratingPatternType))]
  [XmlElement("YearlyRegeneration", typeof (YearlyRegeneratingPatternType))]
  public RecurrencePatternBaseType Item
  {
    get => this.itemField;
    set => this.itemField = value;
  }

  /// <remarks />
  [XmlElement("EndDateRecurrence", typeof (EndDateRecurrenceRangeType))]
  [XmlElement("NoEndRecurrence", typeof (NoEndRecurrenceRangeType))]
  [XmlElement("NumberedRecurrence", typeof (NumberedRecurrenceRangeType))]
  public RecurrenceRangeBaseType Item1
  {
    get => this.item1Field;
    set => this.item1Field = value;
  }
}

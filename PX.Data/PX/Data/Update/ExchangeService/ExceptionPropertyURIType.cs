// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ExceptionPropertyURIType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public enum ExceptionPropertyURIType
{
  /// <remarks />
  [XmlEnum("attachment:Name")] attachmentName,
  /// <remarks />
  [XmlEnum("attachment:ContentType")] attachmentContentType,
  /// <remarks />
  [XmlEnum("attachment:Content")] attachmentContent,
  /// <remarks />
  [XmlEnum("recurrence:Month")] recurrenceMonth,
  /// <remarks />
  [XmlEnum("recurrence:DayOfWeekIndex")] recurrenceDayOfWeekIndex,
  /// <remarks />
  [XmlEnum("recurrence:DaysOfWeek")] recurrenceDaysOfWeek,
  /// <remarks />
  [XmlEnum("recurrence:DayOfMonth")] recurrenceDayOfMonth,
  /// <remarks />
  [XmlEnum("recurrence:Interval")] recurrenceInterval,
  /// <remarks />
  [XmlEnum("recurrence:NumberOfOccurrences")] recurrenceNumberOfOccurrences,
  /// <remarks />
  [XmlEnum("timezone:Offset")] timezoneOffset,
}

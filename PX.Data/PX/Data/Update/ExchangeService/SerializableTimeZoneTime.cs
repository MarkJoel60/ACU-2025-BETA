// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.SerializableTimeZoneTime
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
public class SerializableTimeZoneTime
{
  private int biasField;
  private string timeField;
  private short dayOrderField;
  private short monthField;
  private string dayOfWeekField;
  private string yearField;

  /// <remarks />
  public int Bias
  {
    get => this.biasField;
    set => this.biasField = value;
  }

  /// <remarks />
  public string Time
  {
    get => this.timeField;
    set => this.timeField = value;
  }

  /// <remarks />
  public short DayOrder
  {
    get => this.dayOrderField;
    set => this.dayOrderField = value;
  }

  /// <remarks />
  public short Month
  {
    get => this.monthField;
    set => this.monthField = value;
  }

  /// <remarks />
  public string DayOfWeek
  {
    get => this.dayOfWeekField;
    set => this.dayOfWeekField = value;
  }

  /// <remarks />
  public string Year
  {
    get => this.yearField;
    set => this.yearField = value;
  }
}

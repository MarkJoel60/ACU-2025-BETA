// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.SuggestionDayResult
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
public class SuggestionDayResult
{
  private System.DateTime dateField;
  private SuggestionQuality dayQualityField;
  private Suggestion[] suggestionArrayField;

  /// <remarks />
  public System.DateTime Date
  {
    get => this.dateField;
    set => this.dateField = value;
  }

  /// <remarks />
  public SuggestionQuality DayQuality
  {
    get => this.dayQualityField;
    set => this.dayQualityField = value;
  }

  /// <remarks />
  [XmlArrayItem(IsNullable = false)]
  public Suggestion[] SuggestionArray
  {
    get => this.suggestionArrayField;
    set => this.suggestionArrayField = value;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.FindMessageTrackingReportResponseMessageType
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
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
[Serializable]
public class FindMessageTrackingReportResponseMessageType : ResponseMessageType
{
  private string[] diagnosticsField;
  private FindMessageTrackingSearchResultType[] messageTrackingSearchResultsField;
  private string executedSearchScopeField;
  private ArrayOfTrackingPropertiesType[] errorsField;
  private TrackingPropertyType[] propertiesField;

  /// <remarks />
  [XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public string[] Diagnostics
  {
    get => this.diagnosticsField;
    set => this.diagnosticsField = value;
  }

  /// <remarks />
  [XmlArrayItem("MessageTrackingSearchResult", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public FindMessageTrackingSearchResultType[] MessageTrackingSearchResults
  {
    get => this.messageTrackingSearchResultsField;
    set => this.messageTrackingSearchResultsField = value;
  }

  /// <remarks />
  public string ExecutedSearchScope
  {
    get => this.executedSearchScopeField;
    set => this.executedSearchScopeField = value;
  }

  /// <remarks />
  [XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public ArrayOfTrackingPropertiesType[] Errors
  {
    get => this.errorsField;
    set => this.errorsField = value;
  }

  /// <remarks />
  [XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public TrackingPropertyType[] Properties
  {
    get => this.propertiesField;
    set => this.propertiesField = value;
  }
}

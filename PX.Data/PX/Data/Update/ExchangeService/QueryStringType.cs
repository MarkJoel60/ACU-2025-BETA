// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.QueryStringType
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
public class QueryStringType
{
  private bool resetCacheField;
  private bool resetCacheFieldSpecified;
  private bool returnHighlightTermsField;
  private bool returnHighlightTermsFieldSpecified;
  private bool returnDeletedItemsField;
  private bool returnDeletedItemsFieldSpecified;
  private string valueField;

  /// <remarks />
  [XmlAttribute]
  public bool ResetCache
  {
    get => this.resetCacheField;
    set => this.resetCacheField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ResetCacheSpecified
  {
    get => this.resetCacheFieldSpecified;
    set => this.resetCacheFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public bool ReturnHighlightTerms
  {
    get => this.returnHighlightTermsField;
    set => this.returnHighlightTermsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ReturnHighlightTermsSpecified
  {
    get => this.returnHighlightTermsFieldSpecified;
    set => this.returnHighlightTermsFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public bool ReturnDeletedItems
  {
    get => this.returnDeletedItemsField;
    set => this.returnDeletedItemsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ReturnDeletedItemsSpecified
  {
    get => this.returnDeletedItemsFieldSpecified;
    set => this.returnDeletedItemsFieldSpecified = value;
  }

  /// <remarks />
  [XmlText]
  public string Value
  {
    get => this.valueField;
    set => this.valueField = value;
  }
}

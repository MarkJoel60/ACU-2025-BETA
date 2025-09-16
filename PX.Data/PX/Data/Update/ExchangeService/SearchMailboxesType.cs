// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.SearchMailboxesType
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
public class SearchMailboxesType : BaseRequestType
{
  private MailboxQueryType[] searchQueriesField;
  private SearchResultType resultTypeField;
  private PreviewItemResponseShapeType previewItemResponseShapeField;
  private FieldOrderType sortByField;
  private string languageField;
  private bool deduplicationField;
  private bool deduplicationFieldSpecified;
  private int pageSizeField;
  private bool pageSizeFieldSpecified;
  private string pageItemReferenceField;
  private SearchPageDirectionType pageDirectionField;
  private bool pageDirectionFieldSpecified;

  /// <remarks />
  [XmlArrayItem("MailboxQuery", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public MailboxQueryType[] SearchQueries
  {
    get => this.searchQueriesField;
    set => this.searchQueriesField = value;
  }

  /// <remarks />
  public SearchResultType ResultType
  {
    get => this.resultTypeField;
    set => this.resultTypeField = value;
  }

  /// <remarks />
  public PreviewItemResponseShapeType PreviewItemResponseShape
  {
    get => this.previewItemResponseShapeField;
    set => this.previewItemResponseShapeField = value;
  }

  /// <remarks />
  public FieldOrderType SortBy
  {
    get => this.sortByField;
    set => this.sortByField = value;
  }

  /// <remarks />
  public string Language
  {
    get => this.languageField;
    set => this.languageField = value;
  }

  /// <remarks />
  public bool Deduplication
  {
    get => this.deduplicationField;
    set => this.deduplicationField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool DeduplicationSpecified
  {
    get => this.deduplicationFieldSpecified;
    set => this.deduplicationFieldSpecified = value;
  }

  /// <remarks />
  public int PageSize
  {
    get => this.pageSizeField;
    set => this.pageSizeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool PageSizeSpecified
  {
    get => this.pageSizeFieldSpecified;
    set => this.pageSizeFieldSpecified = value;
  }

  /// <remarks />
  public string PageItemReference
  {
    get => this.pageItemReferenceField;
    set => this.pageItemReferenceField = value;
  }

  /// <remarks />
  public SearchPageDirectionType PageDirection
  {
    get => this.pageDirectionField;
    set => this.pageDirectionField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool PageDirectionSpecified
  {
    get => this.pageDirectionFieldSpecified;
    set => this.pageDirectionFieldSpecified = value;
  }
}

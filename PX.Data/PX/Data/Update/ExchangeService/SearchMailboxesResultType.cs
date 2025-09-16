// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.SearchMailboxesResultType
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
public class SearchMailboxesResultType
{
  private MailboxQueryType[] searchQueriesField;
  private SearchResultType resultTypeField;
  private long itemCountField;
  private long sizeField;
  private int pageItemCountField;
  private long pageItemSizeField;
  private KeywordStatisticsSearchResultType[] keywordStatsField;
  private SearchPreviewItemType[] itemsField;
  private FailedSearchMailboxType[] failedMailboxesField;
  private SearchRefinerItemType[] refinersField;
  private MailboxStatisticsItemType[] mailboxStatsField;

  /// <remarks />
  [XmlArrayItem("MailboxQuery", IsNullable = false)]
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
  public long ItemCount
  {
    get => this.itemCountField;
    set => this.itemCountField = value;
  }

  /// <remarks />
  public long Size
  {
    get => this.sizeField;
    set => this.sizeField = value;
  }

  /// <remarks />
  public int PageItemCount
  {
    get => this.pageItemCountField;
    set => this.pageItemCountField = value;
  }

  /// <remarks />
  public long PageItemSize
  {
    get => this.pageItemSizeField;
    set => this.pageItemSizeField = value;
  }

  /// <remarks />
  [XmlArrayItem("KeywordStat", IsNullable = false)]
  public KeywordStatisticsSearchResultType[] KeywordStats
  {
    get => this.keywordStatsField;
    set => this.keywordStatsField = value;
  }

  /// <remarks />
  [XmlArrayItem("SearchPreviewItem", IsNullable = false)]
  public SearchPreviewItemType[] Items
  {
    get => this.itemsField;
    set => this.itemsField = value;
  }

  /// <remarks />
  [XmlArrayItem("FailedMailbox", IsNullable = false)]
  public FailedSearchMailboxType[] FailedMailboxes
  {
    get => this.failedMailboxesField;
    set => this.failedMailboxesField = value;
  }

  /// <remarks />
  [XmlArrayItem("Refiner", IsNullable = false)]
  public SearchRefinerItemType[] Refiners
  {
    get => this.refinersField;
    set => this.refinersField = value;
  }

  /// <remarks />
  [XmlArrayItem("MailboxStat", IsNullable = false)]
  public MailboxStatisticsItemType[] MailboxStats
  {
    get => this.mailboxStatsField;
    set => this.mailboxStatsField = value;
  }
}

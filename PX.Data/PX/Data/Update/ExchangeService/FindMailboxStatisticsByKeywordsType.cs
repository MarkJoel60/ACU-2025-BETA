// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.FindMailboxStatisticsByKeywordsType
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
public class FindMailboxStatisticsByKeywordsType : BaseRequestType
{
  private UserMailboxType[] mailboxesField;
  private string[] keywordsField;
  private string languageField;
  private string[] sendersField;
  private string[] recipientsField;
  private System.DateTime fromDateField;
  private bool fromDateFieldSpecified;
  private System.DateTime toDateField;
  private bool toDateFieldSpecified;
  private SearchItemKindType[] messageTypesField;
  private bool searchDumpsterField;
  private bool searchDumpsterFieldSpecified;
  private bool includePersonalArchiveField;
  private bool includePersonalArchiveFieldSpecified;
  private bool includeUnsearchableItemsField;
  private bool includeUnsearchableItemsFieldSpecified;

  /// <remarks />
  [XmlArrayItem("UserMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public UserMailboxType[] Mailboxes
  {
    get => this.mailboxesField;
    set => this.mailboxesField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public string[] Keywords
  {
    get => this.keywordsField;
    set => this.keywordsField = value;
  }

  /// <remarks />
  public string Language
  {
    get => this.languageField;
    set => this.languageField = value;
  }

  /// <remarks />
  [XmlArrayItem("SmtpAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public string[] Senders
  {
    get => this.sendersField;
    set => this.sendersField = value;
  }

  /// <remarks />
  [XmlArrayItem("SmtpAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public string[] Recipients
  {
    get => this.recipientsField;
    set => this.recipientsField = value;
  }

  /// <remarks />
  public System.DateTime FromDate
  {
    get => this.fromDateField;
    set => this.fromDateField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool FromDateSpecified
  {
    get => this.fromDateFieldSpecified;
    set => this.fromDateFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime ToDate
  {
    get => this.toDateField;
    set => this.toDateField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ToDateSpecified
  {
    get => this.toDateFieldSpecified;
    set => this.toDateFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("SearchItemKind", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public SearchItemKindType[] MessageTypes
  {
    get => this.messageTypesField;
    set => this.messageTypesField = value;
  }

  /// <remarks />
  public bool SearchDumpster
  {
    get => this.searchDumpsterField;
    set => this.searchDumpsterField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool SearchDumpsterSpecified
  {
    get => this.searchDumpsterFieldSpecified;
    set => this.searchDumpsterFieldSpecified = value;
  }

  /// <remarks />
  public bool IncludePersonalArchive
  {
    get => this.includePersonalArchiveField;
    set => this.includePersonalArchiveField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IncludePersonalArchiveSpecified
  {
    get => this.includePersonalArchiveFieldSpecified;
    set => this.includePersonalArchiveFieldSpecified = value;
  }

  /// <remarks />
  public bool IncludeUnsearchableItems
  {
    get => this.includeUnsearchableItemsField;
    set => this.includeUnsearchableItemsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IncludeUnsearchableItemsSpecified
  {
    get => this.includeUnsearchableItemsFieldSpecified;
    set => this.includeUnsearchableItemsFieldSpecified = value;
  }
}

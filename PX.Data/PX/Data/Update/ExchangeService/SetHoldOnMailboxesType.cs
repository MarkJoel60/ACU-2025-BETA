// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.SetHoldOnMailboxesType
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
public class SetHoldOnMailboxesType : BaseRequestType
{
  private HoldActionType actionTypeField;
  private string holdIdField;
  private string queryField;
  private string[] mailboxesField;
  private string languageField;
  private bool includeNonIndexableItemsField;
  private bool includeNonIndexableItemsFieldSpecified;
  private bool deduplicationField;
  private bool deduplicationFieldSpecified;
  private string inPlaceHoldIdentityField;
  private string itemHoldPeriodField;

  /// <remarks />
  public HoldActionType ActionType
  {
    get => this.actionTypeField;
    set => this.actionTypeField = value;
  }

  /// <remarks />
  public string HoldId
  {
    get => this.holdIdField;
    set => this.holdIdField = value;
  }

  /// <remarks />
  public string Query
  {
    get => this.queryField;
    set => this.queryField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public string[] Mailboxes
  {
    get => this.mailboxesField;
    set => this.mailboxesField = value;
  }

  /// <remarks />
  public string Language
  {
    get => this.languageField;
    set => this.languageField = value;
  }

  /// <remarks />
  public bool IncludeNonIndexableItems
  {
    get => this.includeNonIndexableItemsField;
    set => this.includeNonIndexableItemsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IncludeNonIndexableItemsSpecified
  {
    get => this.includeNonIndexableItemsFieldSpecified;
    set => this.includeNonIndexableItemsFieldSpecified = value;
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
  public string InPlaceHoldIdentity
  {
    get => this.inPlaceHoldIdentityField;
    set => this.inPlaceHoldIdentityField = value;
  }

  /// <remarks />
  public string ItemHoldPeriod
  {
    get => this.itemHoldPeriodField;
    set => this.itemHoldPeriodField = value;
  }
}

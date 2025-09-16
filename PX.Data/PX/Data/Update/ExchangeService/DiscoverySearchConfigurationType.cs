// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.DiscoverySearchConfigurationType
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
public class DiscoverySearchConfigurationType
{
  private string searchIdField;
  private string searchQueryField;
  private SearchableMailboxType[] searchableMailboxesField;
  private string inPlaceHoldIdentityField;
  private string managedByOrganizationField;
  private string languageField;

  /// <remarks />
  public string SearchId
  {
    get => this.searchIdField;
    set => this.searchIdField = value;
  }

  /// <remarks />
  public string SearchQuery
  {
    get => this.searchQueryField;
    set => this.searchQueryField = value;
  }

  /// <remarks />
  [XmlArrayItem("SearchableMailbox", IsNullable = false)]
  public SearchableMailboxType[] SearchableMailboxes
  {
    get => this.searchableMailboxesField;
    set => this.searchableMailboxesField = value;
  }

  /// <remarks />
  public string InPlaceHoldIdentity
  {
    get => this.inPlaceHoldIdentityField;
    set => this.inPlaceHoldIdentityField = value;
  }

  /// <remarks />
  public string ManagedByOrganization
  {
    get => this.managedByOrganizationField;
    set => this.managedByOrganizationField = value;
  }

  /// <remarks />
  public string Language
  {
    get => this.languageField;
    set => this.languageField = value;
  }
}

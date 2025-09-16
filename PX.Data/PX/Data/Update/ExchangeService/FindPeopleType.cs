// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.FindPeopleType
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
public class FindPeopleType : BaseRequestType
{
  private PersonaResponseShapeType personaShapeField;
  private IndexedPageViewType indexedPageItemViewField;
  private RestrictionType restrictionField;
  private RestrictionType aggregationRestrictionField;
  private FieldOrderType[] sortOrderField;
  private TargetFolderIdType parentFolderIdField;
  private string queryStringField;

  /// <remarks />
  public PersonaResponseShapeType PersonaShape
  {
    get => this.personaShapeField;
    set => this.personaShapeField = value;
  }

  /// <remarks />
  public IndexedPageViewType IndexedPageItemView
  {
    get => this.indexedPageItemViewField;
    set => this.indexedPageItemViewField = value;
  }

  /// <remarks />
  public RestrictionType Restriction
  {
    get => this.restrictionField;
    set => this.restrictionField = value;
  }

  /// <remarks />
  public RestrictionType AggregationRestriction
  {
    get => this.aggregationRestrictionField;
    set => this.aggregationRestrictionField = value;
  }

  /// <remarks />
  [XmlArrayItem("FieldOrder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public FieldOrderType[] SortOrder
  {
    get => this.sortOrderField;
    set => this.sortOrderField = value;
  }

  /// <remarks />
  public TargetFolderIdType ParentFolderId
  {
    get => this.parentFolderIdField;
    set => this.parentFolderIdField = value;
  }

  /// <remarks />
  public string QueryString
  {
    get => this.queryStringField;
    set => this.queryStringField = value;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.FindItemType
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
public class FindItemType : BaseRequestType
{
  private ItemResponseShapeType itemShapeField;
  private BasePagingType itemField;
  private BaseGroupByType item1Field;
  private RestrictionType restrictionField;
  private FieldOrderType[] sortOrderField;
  private BaseFolderIdType[] parentFolderIdsField;
  private QueryStringType queryStringField;
  private ItemQueryTraversalType traversalField;

  /// <remarks />
  public ItemResponseShapeType ItemShape
  {
    get => this.itemShapeField;
    set => this.itemShapeField = value;
  }

  /// <remarks />
  [XmlElement("CalendarView", typeof (CalendarViewType))]
  [XmlElement("ContactsView", typeof (ContactsViewType))]
  [XmlElement("FractionalPageItemView", typeof (FractionalPageViewType))]
  [XmlElement("IndexedPageItemView", typeof (IndexedPageViewType))]
  [XmlElement("SeekToConditionPageItemView", typeof (SeekToConditionPageViewType))]
  public BasePagingType Item
  {
    get => this.itemField;
    set => this.itemField = value;
  }

  /// <remarks />
  [XmlElement("DistinguishedGroupBy", typeof (DistinguishedGroupByType))]
  [XmlElement("GroupBy", typeof (GroupByType))]
  public BaseGroupByType Item1
  {
    get => this.item1Field;
    set => this.item1Field = value;
  }

  /// <remarks />
  public RestrictionType Restriction
  {
    get => this.restrictionField;
    set => this.restrictionField = value;
  }

  /// <remarks />
  [XmlArrayItem("FieldOrder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public FieldOrderType[] SortOrder
  {
    get => this.sortOrderField;
    set => this.sortOrderField = value;
  }

  /// <remarks />
  [XmlArrayItem("DistinguishedFolderId", typeof (DistinguishedFolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  [XmlArrayItem("FolderId", typeof (FolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public BaseFolderIdType[] ParentFolderIds
  {
    get => this.parentFolderIdsField;
    set => this.parentFolderIdsField = value;
  }

  /// <remarks />
  public QueryStringType QueryString
  {
    get => this.queryStringField;
    set => this.queryStringField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public ItemQueryTraversalType Traversal
  {
    get => this.traversalField;
    set => this.traversalField = value;
  }
}

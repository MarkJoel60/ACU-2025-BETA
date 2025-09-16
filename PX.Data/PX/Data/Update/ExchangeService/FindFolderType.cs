// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.FindFolderType
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
public class FindFolderType : BaseRequestType
{
  private FolderResponseShapeType folderShapeField;
  private BasePagingType itemField;
  private RestrictionType restrictionField;
  private BaseFolderIdType[] parentFolderIdsField;
  private FolderQueryTraversalType traversalField;

  /// <remarks />
  public FolderResponseShapeType FolderShape
  {
    get => this.folderShapeField;
    set => this.folderShapeField = value;
  }

  /// <remarks />
  [XmlElement("FractionalPageFolderView", typeof (FractionalPageViewType))]
  [XmlElement("IndexedPageFolderView", typeof (IndexedPageViewType))]
  public BasePagingType Item
  {
    get => this.itemField;
    set => this.itemField = value;
  }

  /// <remarks />
  public RestrictionType Restriction
  {
    get => this.restrictionField;
    set => this.restrictionField = value;
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
  [XmlAttribute]
  public FolderQueryTraversalType Traversal
  {
    get => this.traversalField;
    set => this.traversalField = value;
  }
}

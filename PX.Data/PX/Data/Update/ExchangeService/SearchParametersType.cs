// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.SearchParametersType
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
public class SearchParametersType
{
  private RestrictionType restrictionField;
  private BaseFolderIdType[] baseFolderIdsField;
  private SearchFolderTraversalType traversalField;
  private bool traversalFieldSpecified;

  /// <remarks />
  public RestrictionType Restriction
  {
    get => this.restrictionField;
    set => this.restrictionField = value;
  }

  /// <remarks />
  [XmlArrayItem("DistinguishedFolderId", typeof (DistinguishedFolderIdType), IsNullable = false)]
  [XmlArrayItem("FolderId", typeof (FolderIdType), IsNullable = false)]
  public BaseFolderIdType[] BaseFolderIds
  {
    get => this.baseFolderIdsField;
    set => this.baseFolderIdsField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public SearchFolderTraversalType Traversal
  {
    get => this.traversalField;
    set => this.traversalField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool TraversalSpecified
  {
    get => this.traversalFieldSpecified;
    set => this.traversalFieldSpecified = value;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.EmptyFolderType
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
public class EmptyFolderType : BaseRequestType
{
  private BaseFolderIdType[] folderIdsField;
  private DisposalType deleteTypeField;
  private bool deleteSubFoldersField;

  /// <remarks />
  [XmlArrayItem("DistinguishedFolderId", typeof (DistinguishedFolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  [XmlArrayItem("FolderId", typeof (FolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public BaseFolderIdType[] FolderIds
  {
    get => this.folderIdsField;
    set => this.folderIdsField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public DisposalType DeleteType
  {
    get => this.deleteTypeField;
    set => this.deleteTypeField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public bool DeleteSubFolders
  {
    get => this.deleteSubFoldersField;
    set => this.deleteSubFoldersField = value;
  }
}

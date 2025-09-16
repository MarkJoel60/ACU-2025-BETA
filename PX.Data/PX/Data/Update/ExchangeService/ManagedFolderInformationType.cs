// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ManagedFolderInformationType
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
public class ManagedFolderInformationType
{
  private bool canDeleteField;
  private bool canDeleteFieldSpecified;
  private bool canRenameOrMoveField;
  private bool canRenameOrMoveFieldSpecified;
  private bool mustDisplayCommentField;
  private bool mustDisplayCommentFieldSpecified;
  private bool hasQuotaField;
  private bool hasQuotaFieldSpecified;
  private bool isManagedFoldersRootField;
  private bool isManagedFoldersRootFieldSpecified;
  private string managedFolderIdField;
  private string commentField;
  private int storageQuotaField;
  private bool storageQuotaFieldSpecified;
  private int folderSizeField;
  private bool folderSizeFieldSpecified;
  private string homePageField;

  /// <remarks />
  public bool CanDelete
  {
    get => this.canDeleteField;
    set => this.canDeleteField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool CanDeleteSpecified
  {
    get => this.canDeleteFieldSpecified;
    set => this.canDeleteFieldSpecified = value;
  }

  /// <remarks />
  public bool CanRenameOrMove
  {
    get => this.canRenameOrMoveField;
    set => this.canRenameOrMoveField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool CanRenameOrMoveSpecified
  {
    get => this.canRenameOrMoveFieldSpecified;
    set => this.canRenameOrMoveFieldSpecified = value;
  }

  /// <remarks />
  public bool MustDisplayComment
  {
    get => this.mustDisplayCommentField;
    set => this.mustDisplayCommentField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MustDisplayCommentSpecified
  {
    get => this.mustDisplayCommentFieldSpecified;
    set => this.mustDisplayCommentFieldSpecified = value;
  }

  /// <remarks />
  public bool HasQuota
  {
    get => this.hasQuotaField;
    set => this.hasQuotaField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool HasQuotaSpecified
  {
    get => this.hasQuotaFieldSpecified;
    set => this.hasQuotaFieldSpecified = value;
  }

  /// <remarks />
  public bool IsManagedFoldersRoot
  {
    get => this.isManagedFoldersRootField;
    set => this.isManagedFoldersRootField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsManagedFoldersRootSpecified
  {
    get => this.isManagedFoldersRootFieldSpecified;
    set => this.isManagedFoldersRootFieldSpecified = value;
  }

  /// <remarks />
  public string ManagedFolderId
  {
    get => this.managedFolderIdField;
    set => this.managedFolderIdField = value;
  }

  /// <remarks />
  public string Comment
  {
    get => this.commentField;
    set => this.commentField = value;
  }

  /// <remarks />
  public int StorageQuota
  {
    get => this.storageQuotaField;
    set => this.storageQuotaField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool StorageQuotaSpecified
  {
    get => this.storageQuotaFieldSpecified;
    set => this.storageQuotaFieldSpecified = value;
  }

  /// <remarks />
  public int FolderSize
  {
    get => this.folderSizeField;
    set => this.folderSizeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool FolderSizeSpecified
  {
    get => this.folderSizeFieldSpecified;
    set => this.folderSizeFieldSpecified = value;
  }

  /// <remarks />
  public string HomePage
  {
    get => this.homePageField;
    set => this.homePageField = value;
  }
}

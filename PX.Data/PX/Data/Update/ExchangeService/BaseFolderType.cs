// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.BaseFolderType
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
[XmlInclude(typeof (ContactsFolderType))]
[XmlInclude(typeof (CalendarFolderType))]
[XmlInclude(typeof (FolderType))]
[XmlInclude(typeof (TasksFolderType))]
[XmlInclude(typeof (SearchFolderType))]
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public abstract class BaseFolderType
{
  private FolderIdType folderIdField;
  private FolderIdType parentFolderIdField;
  private string folderClassField;
  private string displayNameField;
  private int totalCountField;
  private bool totalCountFieldSpecified;
  private int childFolderCountField;
  private bool childFolderCountFieldSpecified;
  private ExtendedPropertyType[] extendedPropertyField;
  private ManagedFolderInformationType managedFolderInformationField;
  private EffectiveRightsType effectiveRightsField;
  private DistinguishedFolderIdNameType distinguishedFolderIdField;
  private bool distinguishedFolderIdFieldSpecified;
  private RetentionTagType policyTagField;
  private RetentionTagType archiveTagField;

  /// <remarks />
  public FolderIdType FolderId
  {
    get => this.folderIdField;
    set => this.folderIdField = value;
  }

  /// <remarks />
  public FolderIdType ParentFolderId
  {
    get => this.parentFolderIdField;
    set => this.parentFolderIdField = value;
  }

  /// <remarks />
  public string FolderClass
  {
    get => this.folderClassField;
    set => this.folderClassField = value;
  }

  /// <remarks />
  public string DisplayName
  {
    get => this.displayNameField;
    set => this.displayNameField = value;
  }

  /// <remarks />
  public int TotalCount
  {
    get => this.totalCountField;
    set => this.totalCountField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool TotalCountSpecified
  {
    get => this.totalCountFieldSpecified;
    set => this.totalCountFieldSpecified = value;
  }

  /// <remarks />
  public int ChildFolderCount
  {
    get => this.childFolderCountField;
    set => this.childFolderCountField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ChildFolderCountSpecified
  {
    get => this.childFolderCountFieldSpecified;
    set => this.childFolderCountFieldSpecified = value;
  }

  /// <remarks />
  [XmlElement("ExtendedProperty")]
  public ExtendedPropertyType[] ExtendedProperty
  {
    get => this.extendedPropertyField;
    set => this.extendedPropertyField = value;
  }

  /// <remarks />
  public ManagedFolderInformationType ManagedFolderInformation
  {
    get => this.managedFolderInformationField;
    set => this.managedFolderInformationField = value;
  }

  /// <remarks />
  public EffectiveRightsType EffectiveRights
  {
    get => this.effectiveRightsField;
    set => this.effectiveRightsField = value;
  }

  /// <remarks />
  public DistinguishedFolderIdNameType DistinguishedFolderId
  {
    get => this.distinguishedFolderIdField;
    set => this.distinguishedFolderIdField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool DistinguishedFolderIdSpecified
  {
    get => this.distinguishedFolderIdFieldSpecified;
    set => this.distinguishedFolderIdFieldSpecified = value;
  }

  /// <remarks />
  public RetentionTagType PolicyTag
  {
    get => this.policyTagField;
    set => this.policyTagField = value;
  }

  /// <remarks />
  public RetentionTagType ArchiveTag
  {
    get => this.archiveTagField;
    set => this.archiveTagField = value;
  }
}

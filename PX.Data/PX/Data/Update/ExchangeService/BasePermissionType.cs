// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.BasePermissionType
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
[XmlInclude(typeof (CalendarPermissionType))]
[XmlInclude(typeof (PermissionType))]
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public abstract class BasePermissionType
{
  private UserIdType userIdField;
  private bool canCreateItemsField;
  private bool canCreateItemsFieldSpecified;
  private bool canCreateSubFoldersField;
  private bool canCreateSubFoldersFieldSpecified;
  private bool isFolderOwnerField;
  private bool isFolderOwnerFieldSpecified;
  private bool isFolderVisibleField;
  private bool isFolderVisibleFieldSpecified;
  private bool isFolderContactField;
  private bool isFolderContactFieldSpecified;
  private PermissionActionType editItemsField;
  private bool editItemsFieldSpecified;
  private PermissionActionType deleteItemsField;
  private bool deleteItemsFieldSpecified;

  /// <remarks />
  public UserIdType UserId
  {
    get => this.userIdField;
    set => this.userIdField = value;
  }

  /// <remarks />
  public bool CanCreateItems
  {
    get => this.canCreateItemsField;
    set => this.canCreateItemsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool CanCreateItemsSpecified
  {
    get => this.canCreateItemsFieldSpecified;
    set => this.canCreateItemsFieldSpecified = value;
  }

  /// <remarks />
  public bool CanCreateSubFolders
  {
    get => this.canCreateSubFoldersField;
    set => this.canCreateSubFoldersField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool CanCreateSubFoldersSpecified
  {
    get => this.canCreateSubFoldersFieldSpecified;
    set => this.canCreateSubFoldersFieldSpecified = value;
  }

  /// <remarks />
  public bool IsFolderOwner
  {
    get => this.isFolderOwnerField;
    set => this.isFolderOwnerField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsFolderOwnerSpecified
  {
    get => this.isFolderOwnerFieldSpecified;
    set => this.isFolderOwnerFieldSpecified = value;
  }

  /// <remarks />
  public bool IsFolderVisible
  {
    get => this.isFolderVisibleField;
    set => this.isFolderVisibleField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsFolderVisibleSpecified
  {
    get => this.isFolderVisibleFieldSpecified;
    set => this.isFolderVisibleFieldSpecified = value;
  }

  /// <remarks />
  public bool IsFolderContact
  {
    get => this.isFolderContactField;
    set => this.isFolderContactField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsFolderContactSpecified
  {
    get => this.isFolderContactFieldSpecified;
    set => this.isFolderContactFieldSpecified = value;
  }

  /// <remarks />
  public PermissionActionType EditItems
  {
    get => this.editItemsField;
    set => this.editItemsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool EditItemsSpecified
  {
    get => this.editItemsFieldSpecified;
    set => this.editItemsFieldSpecified = value;
  }

  /// <remarks />
  public PermissionActionType DeleteItems
  {
    get => this.deleteItemsField;
    set => this.deleteItemsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool DeleteItemsSpecified
  {
    get => this.deleteItemsFieldSpecified;
    set => this.deleteItemsFieldSpecified = value;
  }
}

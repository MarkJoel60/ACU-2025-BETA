// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.PersonaAttributionType
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
public class PersonaAttributionType
{
  private string idField;
  private ItemIdType sourceIdField;
  private string displayNameField;
  private bool isWritableField;
  private bool isWritableFieldSpecified;
  private bool isQuickContactField;
  private bool isQuickContactFieldSpecified;
  private bool isHiddenField;
  private bool isHiddenFieldSpecified;
  private FolderIdType folderIdField;

  /// <remarks />
  public string Id
  {
    get => this.idField;
    set => this.idField = value;
  }

  /// <remarks />
  public ItemIdType SourceId
  {
    get => this.sourceIdField;
    set => this.sourceIdField = value;
  }

  /// <remarks />
  public string DisplayName
  {
    get => this.displayNameField;
    set => this.displayNameField = value;
  }

  /// <remarks />
  public bool IsWritable
  {
    get => this.isWritableField;
    set => this.isWritableField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsWritableSpecified
  {
    get => this.isWritableFieldSpecified;
    set => this.isWritableFieldSpecified = value;
  }

  /// <remarks />
  public bool IsQuickContact
  {
    get => this.isQuickContactField;
    set => this.isQuickContactField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsQuickContactSpecified
  {
    get => this.isQuickContactFieldSpecified;
    set => this.isQuickContactFieldSpecified = value;
  }

  /// <remarks />
  public bool IsHidden
  {
    get => this.isHiddenField;
    set => this.isHiddenField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsHiddenSpecified
  {
    get => this.isHiddenFieldSpecified;
    set => this.isHiddenFieldSpecified = value;
  }

  /// <remarks />
  public FolderIdType FolderId
  {
    get => this.folderIdField;
    set => this.folderIdField = value;
  }
}

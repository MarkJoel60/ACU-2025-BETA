// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ResolveNamesType
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
public class ResolveNamesType : BaseRequestType
{
  private BaseFolderIdType[] parentFolderIdsField;
  private string unresolvedEntryField;
  private bool returnFullContactDataField;
  private ResolveNamesSearchScopeType searchScopeField;
  private DefaultShapeNamesType contactDataShapeField;
  private bool contactDataShapeFieldSpecified;

  public ResolveNamesType()
  {
    this.searchScopeField = ResolveNamesSearchScopeType.ActiveDirectoryContacts;
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
  public string UnresolvedEntry
  {
    get => this.unresolvedEntryField;
    set => this.unresolvedEntryField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public bool ReturnFullContactData
  {
    get => this.returnFullContactDataField;
    set => this.returnFullContactDataField = value;
  }

  /// <remarks />
  [XmlAttribute]
  [DefaultValue(ResolveNamesSearchScopeType.ActiveDirectoryContacts)]
  public ResolveNamesSearchScopeType SearchScope
  {
    get => this.searchScopeField;
    set => this.searchScopeField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public DefaultShapeNamesType ContactDataShape
  {
    get => this.contactDataShapeField;
    set => this.contactDataShapeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ContactDataShapeSpecified
  {
    get => this.contactDataShapeFieldSpecified;
    set => this.contactDataShapeFieldSpecified = value;
  }
}

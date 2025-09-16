// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.FolderType
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
[XmlInclude(typeof (TasksFolderType))]
[XmlInclude(typeof (SearchFolderType))]
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public class FolderType : BaseFolderType
{
  private PermissionSetType permissionSetField;
  private int unreadCountField;
  private bool unreadCountFieldSpecified;

  /// <remarks />
  public PermissionSetType PermissionSet
  {
    get => this.permissionSetField;
    set => this.permissionSetField = value;
  }

  /// <remarks />
  public int UnreadCount
  {
    get => this.unreadCountField;
    set => this.unreadCountField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool UnreadCountSpecified
  {
    get => this.unreadCountFieldSpecified;
    set => this.unreadCountFieldSpecified = value;
  }
}

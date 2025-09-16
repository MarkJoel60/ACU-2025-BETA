// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.MailboxAssociationType
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
public class MailboxAssociationType
{
  private GroupLocatorType groupField;
  private UserLocatorType userField;
  private bool isMemberField;
  private bool isMemberFieldSpecified;
  private System.DateTime joinDateField;
  private bool joinDateFieldSpecified;
  private bool isPinField;
  private bool isPinFieldSpecified;
  private string joinedByField;

  /// <remarks />
  public GroupLocatorType Group
  {
    get => this.groupField;
    set => this.groupField = value;
  }

  /// <remarks />
  public UserLocatorType User
  {
    get => this.userField;
    set => this.userField = value;
  }

  /// <remarks />
  public bool IsMember
  {
    get => this.isMemberField;
    set => this.isMemberField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsMemberSpecified
  {
    get => this.isMemberFieldSpecified;
    set => this.isMemberFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime JoinDate
  {
    get => this.joinDateField;
    set => this.joinDateField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool JoinDateSpecified
  {
    get => this.joinDateFieldSpecified;
    set => this.joinDateFieldSpecified = value;
  }

  /// <remarks />
  public bool IsPin
  {
    get => this.isPinField;
    set => this.isPinField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsPinSpecified
  {
    get => this.isPinFieldSpecified;
    set => this.isPinFieldSpecified = value;
  }

  /// <remarks />
  public string JoinedBy
  {
    get => this.joinedByField;
    set => this.joinedByField = value;
  }
}

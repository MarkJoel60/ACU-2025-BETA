// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.FederatedDirectoryGroupType
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
public class FederatedDirectoryGroupType
{
  private string aliasField;
  private string calendarUrlField;
  private string displayNameField;
  private string externalDirectoryObjectIdField;
  private FederatedDirectoryGroupTypeType groupTypeField;
  private bool groupTypeFieldSpecified;
  private string inboxUrlField;
  private bool isMemberField;
  private bool isMemberFieldSpecified;
  private bool isPinnedField;
  private bool isPinnedFieldSpecified;
  private System.DateTime joinDateField;
  private bool joinDateFieldSpecified;
  private string legacyDnField;
  private string peopleUrlField;
  private string photoUrlField;
  private string smtpAddressField;

  /// <remarks />
  public string Alias
  {
    get => this.aliasField;
    set => this.aliasField = value;
  }

  /// <remarks />
  public string CalendarUrl
  {
    get => this.calendarUrlField;
    set => this.calendarUrlField = value;
  }

  /// <remarks />
  public string DisplayName
  {
    get => this.displayNameField;
    set => this.displayNameField = value;
  }

  /// <remarks />
  public string ExternalDirectoryObjectId
  {
    get => this.externalDirectoryObjectIdField;
    set => this.externalDirectoryObjectIdField = value;
  }

  /// <remarks />
  public FederatedDirectoryGroupTypeType GroupType
  {
    get => this.groupTypeField;
    set => this.groupTypeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool GroupTypeSpecified
  {
    get => this.groupTypeFieldSpecified;
    set => this.groupTypeFieldSpecified = value;
  }

  /// <remarks />
  public string InboxUrl
  {
    get => this.inboxUrlField;
    set => this.inboxUrlField = value;
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
  public bool IsPinned
  {
    get => this.isPinnedField;
    set => this.isPinnedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsPinnedSpecified
  {
    get => this.isPinnedFieldSpecified;
    set => this.isPinnedFieldSpecified = value;
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
  public string LegacyDn
  {
    get => this.legacyDnField;
    set => this.legacyDnField = value;
  }

  /// <remarks />
  public string PeopleUrl
  {
    get => this.peopleUrlField;
    set => this.peopleUrlField = value;
  }

  /// <remarks />
  public string PhotoUrl
  {
    get => this.photoUrlField;
    set => this.photoUrlField = value;
  }

  /// <remarks />
  public string SmtpAddress
  {
    get => this.smtpAddressField;
    set => this.smtpAddressField = value;
  }
}

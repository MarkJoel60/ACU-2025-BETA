// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.MasterMailboxType
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
public class MasterMailboxType
{
  private string mailboxTypeField;
  private string aliasField;
  private string displayNameField;
  private string smtpAddressField;
  private ModernGroupTypeType groupTypeField;
  private bool groupTypeFieldSpecified;
  private string descriptionField;
  private string photoField;
  private string sharePointUrlField;
  private string inboxUrlField;
  private string calendarUrlField;
  private string domainControllerField;

  /// <remarks />
  public string MailboxType
  {
    get => this.mailboxTypeField;
    set => this.mailboxTypeField = value;
  }

  /// <remarks />
  public string Alias
  {
    get => this.aliasField;
    set => this.aliasField = value;
  }

  /// <remarks />
  public string DisplayName
  {
    get => this.displayNameField;
    set => this.displayNameField = value;
  }

  /// <remarks />
  public string SmtpAddress
  {
    get => this.smtpAddressField;
    set => this.smtpAddressField = value;
  }

  /// <remarks />
  public ModernGroupTypeType GroupType
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
  public string Description
  {
    get => this.descriptionField;
    set => this.descriptionField = value;
  }

  /// <remarks />
  public string Photo
  {
    get => this.photoField;
    set => this.photoField = value;
  }

  /// <remarks />
  public string SharePointUrl
  {
    get => this.sharePointUrlField;
    set => this.sharePointUrlField = value;
  }

  /// <remarks />
  public string InboxUrl
  {
    get => this.inboxUrlField;
    set => this.inboxUrlField = value;
  }

  /// <remarks />
  public string CalendarUrl
  {
    get => this.calendarUrlField;
    set => this.calendarUrlField = value;
  }

  /// <remarks />
  public string DomainController
  {
    get => this.domainControllerField;
    set => this.domainControllerField = value;
  }
}

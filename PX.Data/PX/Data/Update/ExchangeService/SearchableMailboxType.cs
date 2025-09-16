// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.SearchableMailboxType
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
public class SearchableMailboxType
{
  private string guidField;
  private string primarySmtpAddressField;
  private bool isExternalMailboxField;
  private string externalEmailAddressField;
  private string displayNameField;
  private bool isMembershipGroupField;
  private string referenceIdField;

  /// <remarks />
  public string Guid
  {
    get => this.guidField;
    set => this.guidField = value;
  }

  /// <remarks />
  public string PrimarySmtpAddress
  {
    get => this.primarySmtpAddressField;
    set => this.primarySmtpAddressField = value;
  }

  /// <remarks />
  public bool IsExternalMailbox
  {
    get => this.isExternalMailboxField;
    set => this.isExternalMailboxField = value;
  }

  /// <remarks />
  public string ExternalEmailAddress
  {
    get => this.externalEmailAddressField;
    set => this.externalEmailAddressField = value;
  }

  /// <remarks />
  public string DisplayName
  {
    get => this.displayNameField;
    set => this.displayNameField = value;
  }

  /// <remarks />
  public bool IsMembershipGroup
  {
    get => this.isMembershipGroupField;
    set => this.isMembershipGroupField = value;
  }

  /// <remarks />
  public string ReferenceId
  {
    get => this.referenceIdField;
    set => this.referenceIdField = value;
  }
}

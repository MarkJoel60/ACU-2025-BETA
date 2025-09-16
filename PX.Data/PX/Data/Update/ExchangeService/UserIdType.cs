// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.UserIdType
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
public class UserIdType
{
  private string sIDField;
  private string primarySmtpAddressField;
  private string displayNameField;
  private DistinguishedUserType distinguishedUserField;
  private bool distinguishedUserFieldSpecified;
  private string externalUserIdentityField;

  /// <remarks />
  public string SID
  {
    get => this.sIDField;
    set => this.sIDField = value;
  }

  /// <remarks />
  public string PrimarySmtpAddress
  {
    get => this.primarySmtpAddressField;
    set => this.primarySmtpAddressField = value;
  }

  /// <remarks />
  public string DisplayName
  {
    get => this.displayNameField;
    set => this.displayNameField = value;
  }

  /// <remarks />
  public DistinguishedUserType DistinguishedUser
  {
    get => this.distinguishedUserField;
    set => this.distinguishedUserField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool DistinguishedUserSpecified
  {
    get => this.distinguishedUserFieldSpecified;
    set => this.distinguishedUserFieldSpecified = value;
  }

  /// <remarks />
  public string ExternalUserIdentity
  {
    get => this.externalUserIdentityField;
    set => this.externalUserIdentityField = value;
  }
}

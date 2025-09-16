// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.UpdateGroupMailboxType
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
public class UpdateGroupMailboxType : BaseRequestType
{
  private string groupSmtpAddressField;
  private string executingUserSmtpAddressField;
  private string domainControllerField;
  private GroupMailboxConfigurationActionType forceConfigurationActionField;
  private GroupMemberIdentifierType memberIdentifierTypeField;
  private bool memberIdentifierTypeFieldSpecified;
  private string[] addedMembersField;
  private string[] removedMembersField;
  private string[] addedPendingMembersField;
  private string[] removedPendingMembersField;

  /// <remarks />
  public string GroupSmtpAddress
  {
    get => this.groupSmtpAddressField;
    set => this.groupSmtpAddressField = value;
  }

  /// <remarks />
  public string ExecutingUserSmtpAddress
  {
    get => this.executingUserSmtpAddressField;
    set => this.executingUserSmtpAddressField = value;
  }

  /// <remarks />
  public string DomainController
  {
    get => this.domainControllerField;
    set => this.domainControllerField = value;
  }

  /// <remarks />
  public GroupMailboxConfigurationActionType ForceConfigurationAction
  {
    get => this.forceConfigurationActionField;
    set => this.forceConfigurationActionField = value;
  }

  /// <remarks />
  public GroupMemberIdentifierType MemberIdentifierType
  {
    get => this.memberIdentifierTypeField;
    set => this.memberIdentifierTypeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MemberIdentifierTypeSpecified
  {
    get => this.memberIdentifierTypeFieldSpecified;
    set => this.memberIdentifierTypeFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public string[] AddedMembers
  {
    get => this.addedMembersField;
    set => this.addedMembersField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public string[] RemovedMembers
  {
    get => this.removedMembersField;
    set => this.removedMembersField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public string[] AddedPendingMembers
  {
    get => this.addedPendingMembersField;
    set => this.addedPendingMembersField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public string[] RemovedPendingMembers
  {
    get => this.removedPendingMembersField;
    set => this.removedPendingMembersField = value;
  }
}

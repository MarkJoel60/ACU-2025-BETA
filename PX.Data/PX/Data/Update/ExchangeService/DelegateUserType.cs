// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.DelegateUserType
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
public class DelegateUserType
{
  private UserIdType userIdField;
  private DelegatePermissionsType delegatePermissionsField;
  private bool receiveCopiesOfMeetingMessagesField;
  private bool receiveCopiesOfMeetingMessagesFieldSpecified;
  private bool viewPrivateItemsField;
  private bool viewPrivateItemsFieldSpecified;

  /// <remarks />
  public UserIdType UserId
  {
    get => this.userIdField;
    set => this.userIdField = value;
  }

  /// <remarks />
  public DelegatePermissionsType DelegatePermissions
  {
    get => this.delegatePermissionsField;
    set => this.delegatePermissionsField = value;
  }

  /// <remarks />
  public bool ReceiveCopiesOfMeetingMessages
  {
    get => this.receiveCopiesOfMeetingMessagesField;
    set => this.receiveCopiesOfMeetingMessagesField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ReceiveCopiesOfMeetingMessagesSpecified
  {
    get => this.receiveCopiesOfMeetingMessagesFieldSpecified;
    set => this.receiveCopiesOfMeetingMessagesFieldSpecified = value;
  }

  /// <remarks />
  public bool ViewPrivateItems
  {
    get => this.viewPrivateItemsField;
    set => this.viewPrivateItemsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ViewPrivateItemsSpecified
  {
    get => this.viewPrivateItemsFieldSpecified;
    set => this.viewPrivateItemsFieldSpecified = value;
  }
}

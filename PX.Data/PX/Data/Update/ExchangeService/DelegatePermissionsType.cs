// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.DelegatePermissionsType
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
public class DelegatePermissionsType
{
  private DelegateFolderPermissionLevelType calendarFolderPermissionLevelField;
  private bool calendarFolderPermissionLevelFieldSpecified;
  private DelegateFolderPermissionLevelType tasksFolderPermissionLevelField;
  private bool tasksFolderPermissionLevelFieldSpecified;
  private DelegateFolderPermissionLevelType inboxFolderPermissionLevelField;
  private bool inboxFolderPermissionLevelFieldSpecified;
  private DelegateFolderPermissionLevelType contactsFolderPermissionLevelField;
  private bool contactsFolderPermissionLevelFieldSpecified;
  private DelegateFolderPermissionLevelType notesFolderPermissionLevelField;
  private bool notesFolderPermissionLevelFieldSpecified;
  private DelegateFolderPermissionLevelType journalFolderPermissionLevelField;
  private bool journalFolderPermissionLevelFieldSpecified;

  /// <remarks />
  public DelegateFolderPermissionLevelType CalendarFolderPermissionLevel
  {
    get => this.calendarFolderPermissionLevelField;
    set => this.calendarFolderPermissionLevelField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool CalendarFolderPermissionLevelSpecified
  {
    get => this.calendarFolderPermissionLevelFieldSpecified;
    set => this.calendarFolderPermissionLevelFieldSpecified = value;
  }

  /// <remarks />
  public DelegateFolderPermissionLevelType TasksFolderPermissionLevel
  {
    get => this.tasksFolderPermissionLevelField;
    set => this.tasksFolderPermissionLevelField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool TasksFolderPermissionLevelSpecified
  {
    get => this.tasksFolderPermissionLevelFieldSpecified;
    set => this.tasksFolderPermissionLevelFieldSpecified = value;
  }

  /// <remarks />
  public DelegateFolderPermissionLevelType InboxFolderPermissionLevel
  {
    get => this.inboxFolderPermissionLevelField;
    set => this.inboxFolderPermissionLevelField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool InboxFolderPermissionLevelSpecified
  {
    get => this.inboxFolderPermissionLevelFieldSpecified;
    set => this.inboxFolderPermissionLevelFieldSpecified = value;
  }

  /// <remarks />
  public DelegateFolderPermissionLevelType ContactsFolderPermissionLevel
  {
    get => this.contactsFolderPermissionLevelField;
    set => this.contactsFolderPermissionLevelField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ContactsFolderPermissionLevelSpecified
  {
    get => this.contactsFolderPermissionLevelFieldSpecified;
    set => this.contactsFolderPermissionLevelFieldSpecified = value;
  }

  /// <remarks />
  public DelegateFolderPermissionLevelType NotesFolderPermissionLevel
  {
    get => this.notesFolderPermissionLevelField;
    set => this.notesFolderPermissionLevelField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool NotesFolderPermissionLevelSpecified
  {
    get => this.notesFolderPermissionLevelFieldSpecified;
    set => this.notesFolderPermissionLevelFieldSpecified = value;
  }

  /// <remarks />
  public DelegateFolderPermissionLevelType JournalFolderPermissionLevel
  {
    get => this.journalFolderPermissionLevelField;
    set => this.journalFolderPermissionLevelField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool JournalFolderPermissionLevelSpecified
  {
    get => this.journalFolderPermissionLevelFieldSpecified;
    set => this.journalFolderPermissionLevelFieldSpecified = value;
  }
}

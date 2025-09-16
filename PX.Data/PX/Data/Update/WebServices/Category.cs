// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.WebServices.Category
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.WebServices;

/// <remarks />
[XmlType(Namespace = "CategoryList.xsd")]
[Serializable]
public class Category
{
  /// <remarks />
  [XmlAttribute("name")]
  public string Name { get; set; }

  /// <remarks />
  [XmlAttribute("color")]
  public CategoryColor Color { get; set; }

  /// <remarks />
  [XmlAttribute("keyboardShortcut")]
  public CategoryKeyboardShortcut KeyboardShortcut { get; set; }

  /// <remarks />
  [XmlAttribute("usageCount")]
  public int UsageCount { get; set; }

  /// <remarks />
  [XmlAttribute("lastTimeUsedNotes")]
  public System.DateTime LastTimeUsedNotes { get; set; }

  /// <remarks />
  [XmlAttribute("lastTimeUsedJournal")]
  public System.DateTime LastTimeUsedJournal { get; set; }

  /// <remarks />
  [XmlAttribute("lastTimeUsedContacts")]
  public System.DateTime LastTimeUsedContacts { get; set; }

  /// <remarks />
  [XmlAttribute("lastTimeUsedTasks")]
  public System.DateTime LastTimeUsedTasks { get; set; }

  /// <remarks />
  [XmlAttribute("lastTimeUsedCalendar")]
  public System.DateTime LastTimeUsedCalendar { get; set; }

  /// <remarks />
  [XmlAttribute("lastTimeUsedMail")]
  public System.DateTime LastTimeUsedMail { get; set; }

  /// <remarks />
  [XmlAttribute("lastTimeUsed")]
  public System.DateTime LastTimeUsed { get; set; }

  /// <remarks />
  [XmlAttribute("lastSessionUsed")]
  public int LastSessionUsed { get; set; }

  /// <remarks />
  [XmlAttribute("guid")]
  public Guid Id { get; set; }

  /// <remarks />
  [XmlAttribute("renameOnFirstUse")]
  public CategoryRenameOnFirstUse RenameOnFirstUse { get; set; }

  public Category()
  {
  }

  public Category(string name, CategoryColor color, CategoryKeyboardShortcut keyboardShortcut)
    : this()
  {
    this.Name = name;
    this.Color = color;
    this.KeyboardShortcut = keyboardShortcut;
    this.Id = Guid.NewGuid();
  }
}

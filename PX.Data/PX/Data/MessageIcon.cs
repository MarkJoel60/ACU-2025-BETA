// Decompiled with JetBrains decompiler
// Type: PX.Data.MessageIcon
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Defines possible icons that can be displayed beside the
/// message in the dialog window opened by the <tt>Ask()</tt>
/// method.</summary>
public enum MessageIcon
{
  /// <summary>No icon is displayed.</summary>
  None,
  /// <summary>The error sign is displayed.</summary>
  Error,
  /// <summary>The question mark sign is displayed.</summary>
  Question,
  /// <summary>The warning sign is displayed.</summary>
  Warning,
  /// <summary>The information sign is displayed.</summary>
  Information,
}

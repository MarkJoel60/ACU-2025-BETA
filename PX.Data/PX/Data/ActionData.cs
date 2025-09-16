// Decompiled with JetBrains decompiler
// Type: PX.Data.ActionData
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Information about a <see cref="T:PX.Data.PXAction" /> being executed.
/// </summary>
[PXContextCopyingRequired]
internal class ActionData
{
  public const string ActionDataSlotKey = "ActionInProgress";

  /// <summary>Action name.</summary>
  public string Name { get; set; }

  /// <summary>Action's origin.</summary>
  public ActionOrigin Origin { get; set; }
}

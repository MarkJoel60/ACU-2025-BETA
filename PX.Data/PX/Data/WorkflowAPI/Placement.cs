// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.Placement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.WorkflowAPI;

/// <summary>
/// Provides a location of an action inside a category or location of a category in the More menu.
/// </summary>
public enum Placement : byte
{
  /// <summary>
  /// The current object should be placed after the specified object.
  /// </summary>
  After = 0,
  /// <summary>
  /// By default, the current object is placed after the specified object.
  /// </summary>
  Default = 0,
  /// <summary>
  /// The current object should be placed before the specified object.
  /// </summary>
  Before = 1,
  /// <summary>
  /// The current object should be placed first in the list of objects.
  /// </summary>
  First = 2,
  /// <summary>
  /// The current object should be places last in the list of objects.
  /// </summary>
  Last = 3,
}

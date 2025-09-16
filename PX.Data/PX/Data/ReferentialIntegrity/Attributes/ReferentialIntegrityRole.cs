// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.ReferentialIntegrityRole
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes;

/// <summary>Defines how referential integrity check should be performed.</summary>
[Flags]
internal enum ReferentialIntegrityRole
{
  /// <summary>Check children presence on row deleting</summary>
  AsParent = 1,
  /// <summary>
  /// Check parent presence on row inserting and row updating
  /// </summary>
  AsChild = 2,
  /// <summary>
  /// Check children presence on row deleting and parent presence on row inserting/updating
  /// </summary>
  Full = AsChild | AsParent, // 0x00000003
}

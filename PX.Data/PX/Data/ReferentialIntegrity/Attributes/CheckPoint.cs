// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.CheckPoint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes;

/// <summary>Defines when referential integrity check should be performed.</summary>
[Flags]
public enum CheckPoint
{
  /// <summary>
  /// Check referential integrity on parents <see cref="E:PX.Data.PXCache.RowDeleting" />
  /// and children <see cref="E:PX.Data.PXCache.RowInserting" /> and <see cref="E:PX.Data.PXCache.RowUpdating" /> events.
  /// </summary>
  BeforePersisting = 1,
  /// <summary>
  /// Check referential integrity on <see cref="E:PX.Data.PXCache.RowPersisting" /> event.
  /// </summary>
  OnPersisting = 2,
  /// <summary>
  /// Check referential integrity both on parents <see cref="E:PX.Data.PXCache.RowDeleting" />
  /// and children <see cref="E:PX.Data.PXCache.RowInserting" /> and <see cref="E:PX.Data.PXCache.RowUpdating" />,
  /// and on <see cref="E:PX.Data.PXCache.RowPersisting" /> event.
  /// </summary>
  Both = OnPersisting | BeforePersisting, // 0x00000003
}

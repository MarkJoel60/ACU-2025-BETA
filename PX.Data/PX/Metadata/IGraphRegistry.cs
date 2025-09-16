// Decompiled with JetBrains decompiler
// Type: PX.Metadata.IGraphRegistry
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Metadata;

/// <summary>
/// A registry of <see cref="T:PX.Data.PXGraph" />-derived types (Business Logic Controllers, or BLCs).
/// To be used for retrieving lists of graphs available in the system.
/// </summary>
/// <remarks>
/// <para>Always returns non-customized types.</para>
/// <para>Contains only types with <see langword="public" /> visibility.
/// All other types are considered auxiliary classes that belong to their area of visibility.</para>
/// <para>Does not contain abstract or open generic types.</para></remarks>
internal interface IGraphRegistry
{
  /// <summary>
  /// Returns the list of all <see cref="T:PX.Data.PXGraph" />-derived types (Business Logic Controllers, or BLCs) available in the system.
  /// </summary>
  /// <remarks>Only types with <see langword="public" /> visibility are returned.</remarks>
  IEnumerable<Type> All { get; }

  /// <summary>
  /// Returns the list of all <see cref="T:PX.Data.PXGraph" />-derived types (Business Logic Controllers, or BLCs)
  /// available in the system that are not marked with <see cref="!:PXHiddenAttribute" />.
  /// </summary>
  /// <remarks>Only types with <see langword="public" /> visibility are returned.</remarks>
  IEnumerable<Type> Visible { get; }

  /// <summary>
  /// Returns the list of all <see cref="T:PX.Data.PXGraph" />-derived types (Business Logic Controllers, or BLCs)
  /// available in the system marked with <see cref="!:PXHiddenAttribute" />.
  /// </summary>
  /// <remarks>Only types with <see langword="public" /> visibility are returned.</remarks>
  IEnumerable<Type> Hidden { get; }
}

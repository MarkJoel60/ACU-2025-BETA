// Decompiled with JetBrains decompiler
// Type: PX.Metadata.IDacRegistry
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Metadata;

/// <summary>
/// A registry of Data Access Classes (DACs). To be used for retrieving lists of DACs available in the system.
/// </summary>
/// <remarks>Contains only DACs with <see langword="public" /> visibility.
/// All other DACs are considered auxiliary classes that belong to their area of visibility.</remarks>
[PXInternalUseOnly]
public interface IDacRegistry
{
  /// <summary>
  /// Returns the list of all Data Access Classes (DACs) available in the system.
  /// </summary>
  /// <remarks>Only DACs with <see langword="public" /> visibility are returned.</remarks>
  IEnumerable<Type> All { get; }

  /// <summary>
  /// Returns the list of all Data Access Classes (DACs) available in the system that are not marked with <see cref="T:PX.Data.PXHiddenAttribute" />.
  /// </summary>
  /// <remarks>Only DACs with <see langword="public" /> visibility are returned.</remarks>
  IEnumerable<Type> Visible { get; }

  /// <summary>
  /// Returns the list of all Data Access Classes (DACs) available in the system marked with <see cref="T:PX.Data.PXHiddenAttribute" />.
  /// </summary>
  /// <remarks>Only DACs with <see langword="public" /> visibility are returned.</remarks>
  IEnumerable<Type> Hidden { get; }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDacType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Type(category) of data access class (DAC).</summary>
[Serializable]
public enum PXDacType
{
  /// <summary>Default value, not set</summary>
  Unknown,
  /// <summary>
  /// The type to assign to the DAC which have a config fields.
  /// </summary>
  Config,
  /// <summary>The type to assign to the DAC which is a catalogue.</summary>
  Catalogue,
  /// <summary>The type to assign to the DAC which is a document.</summary>
  Document,
  /// <summary>
  /// The type to assign to the DAC which is a document detail.
  /// </summary>
  Details,
  /// <summary>
  /// The type to assign to the DAC which have a history of operations.
  /// </summary>
  History,
  /// <summary>
  /// The type to assign to the DAC which have a balance fields.
  /// </summary>
  Balance,
}

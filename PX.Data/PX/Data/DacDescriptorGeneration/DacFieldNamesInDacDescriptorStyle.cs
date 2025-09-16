// Decompiled with JetBrains decompiler
// Type: PX.Data.DacDescriptorGeneration.DacFieldNamesInDacDescriptorStyle
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.DacDescriptorGeneration;

/// <summary>
/// Styles of adding DAC field names to the DAC descriptor together with their values.
/// </summary>
public enum DacFieldNamesInDacDescriptorStyle
{
  /// <summary>No DAC field names are added to the descriptor.</summary>
  None,
  /// <summary>
  /// DAC field names are added to the descriptor for all DAC fields except DAC keys.
  /// </summary>
  AllExceptKeys,
  /// <summary>
  /// DAC field names are added to the descriptor for all DAC fields.
  /// </summary>
  All,
}

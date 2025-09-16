// Decompiled with JetBrains decompiler
// Type: PX.Data.DacDescriptorGeneration.DacKeysInDacDescriptorStyle
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.DacDescriptorGeneration;

/// <summary>Styles of adding DAC keys to a DAC descriptor.</summary>
public enum DacKeysInDacDescriptorStyle
{
  /// <summary>
  /// Always include DAC keys into the DAC descriptor.
  /// The presence of the <see cref="T:PX.Data.EP.PXFieldDescriptionAttribute" /> attribute is not considered.
  /// </summary>
  AlwaysInclude,
  /// <summary>
  /// Include DAC keys for fields marked with the  <see cref="T:PX.Data.EP.PXFieldDescriptionAttribute" /> attribute.
  /// </summary>
  KeysWithFieldDescriptionAttribute,
  /// <summary>
  /// Never include DAC keys into the DAC descriptor.
  /// The presence of the <see cref="T:PX.Data.EP.PXFieldDescriptionAttribute" /> attribute is not considered.
  /// </summary>
  NeverInclude,
}

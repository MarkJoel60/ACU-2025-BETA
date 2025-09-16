// Decompiled with JetBrains decompiler
// Type: PX.Data.DacDescriptorGeneration.NullOrEmptyDacFieldValuesStyle
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.DacDescriptorGeneration;

/// <summary>
/// Styles of adding null, empty or whitespace DAC field values to the DAC descriptor.
/// </summary>
public enum NullOrEmptyDacFieldValuesStyle
{
  /// <summary>
  /// Decide whether to include null, empty or whitespace values based on the <see cref="P:PX.Data.EP.PXFieldDescriptionAttribute.IncludeNullAndEmptyValuesInDacDescriptor" /> flag.
  /// </summary>
  UseFieldAttributes,
  /// <summary>
  /// Never include null, empty or whitespace values on the DAC descriptor.
  /// The <see cref="P:PX.Data.EP.PXFieldDescriptionAttribute.IncludeNullAndEmptyValuesInDacDescriptor" /> flag is not considered.
  /// </summary>
  NeverInclude,
  /// <summary>
  /// Always include null, empty or whitespace values on the DAC descriptor.
  /// The <see cref="P:PX.Data.EP.PXFieldDescriptionAttribute.IncludeNullAndEmptyValuesInDacDescriptor" /> flag is not considered.
  /// </summary>
  AlwaysInclude,
}

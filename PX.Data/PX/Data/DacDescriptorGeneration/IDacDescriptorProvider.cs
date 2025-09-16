// Decompiled with JetBrains decompiler
// Type: PX.Data.DacDescriptorGeneration.IDacDescriptorProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable enable
namespace PX.Data.DacDescriptorGeneration;

/// <summary>Interface for the DAC descriptor provider.</summary>
internal interface IDacDescriptorProvider
{
  /// <summary>Creates DAC descriptor.</summary>
  /// <param name="graph">The graph.</param>
  /// <param name="dac">The DAC.</param>
  /// <param name="descriptorCreationOptions">(Optional) Options for controlling the DAC descriptor creation. If <see langword="null" /> then default options will be used.</param>
  /// <returns>The new DAC descriptor.</returns>
  DacDescriptor CreateDacDescriptor(
    PXGraph graph,
    IBqlTable dac,
    DacDescriptorCreationOptions? descriptorCreationOptions = null);
}

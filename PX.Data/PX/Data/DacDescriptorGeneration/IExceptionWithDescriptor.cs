// Decompiled with JetBrains decompiler
// Type: PX.Data.DacDescriptorGeneration.IExceptionWithDescriptor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.DacDescriptorGeneration;

/// <summary>Interface for exception with a DAC descriptor.</summary>
public interface IExceptionWithDescriptor
{
  /// <summary>
  /// The DAC descriptor of the DAC record related to the exception.
  /// </summary>
  PX.Data.DacDescriptorGeneration.DacDescriptor? DacDescriptor { get; }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.DacDescriptorGeneration.DacDescriptorTraceWindowOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.DacDescriptorGeneration;

/// <summary>
/// Options related to the display of DAC descriptor in the Trace Window.
/// </summary>
internal class DacDescriptorTraceWindowOptions
{
  public const int DefaultNumberOfDacDescriptorsPerExceptionGroup = 10;

  /// <summary>
  /// The number of DAC descriptors to be displayed in the trace window for each exception type group.
  /// </summary>
  public int NumberOfDacDescriptorsPerExceptionGroup { get; set; } = 10;
}

// Decompiled with JetBrains decompiler
// Type: PX.Async.ParallelProcessing.ParallelProcessingOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Async.ParallelProcessing;

[PXInternalUseOnly]
public class ParallelProcessingOptions
{
  internal void Configure(PXLicense license)
  {
    this.ProcessorCount = ParallelProcessingOptions.GetProcessorCount(license);
  }

  [PXInternalUseOnly]
  public int ProcessorCount { get; private set; } = 1;

  internal static int GetProcessorCount(PXLicense lic)
  {
    int processorCount = Environment.ProcessorCount / 2 + 1;
    if (lic.Licensed)
      processorCount = lic.Processors / 2;
    if (processorCount > Environment.ProcessorCount)
      processorCount = Environment.ProcessorCount;
    int processingMaxThreads = WebConfig.ParallelProcessingMaxThreads;
    if (processingMaxThreads > 0 && processingMaxThreads < processorCount)
      processorCount = processingMaxThreads;
    if (processorCount < 1)
      processorCount = 1;
    return processorCount;
  }
}

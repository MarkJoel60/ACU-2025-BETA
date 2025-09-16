// Decompiled with JetBrains decompiler
// Type: PX.Logging.TraceProviders.IExposesMinimumLevel
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Diagnostics;

#nullable disable
namespace PX.Logging.TraceProviders;

[PXInternalUseOnly]
public interface IExposesMinimumLevel
{
  SourceLevels MinimumLevel { get; }
}

// Decompiled with JetBrains decompiler
// Type: PX.SM.TraceTypeListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

internal class TraceTypeListAttribute : PXStringListAttribute
{
  public TraceTypeListAttribute()
    : base(PXPerformanceMonitor._logLevels, PXPerformanceMonitor._logLevels)
  {
  }
}

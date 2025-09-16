// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectResultBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System.Data;

#nullable disable
namespace PX.Data;

internal abstract class PXSelectResultBase
{
  internal abstract class PXSelectResultEnumeratorBase
  {
    protected int ReadRowCount;
    protected PXProfilerSqlSample _sample;

    protected void PerformanceMonitorSampleDispose(IDataReader reader)
    {
      try
      {
        if (!PXPerformanceMonitor.SqlProfilerEnabled || !PXPerformanceMonitor.IsEnabled)
          return;
        PXPerformanceInfo currentSample = PXPerformanceMonitor.CurrentSample;
        if (currentSample != null)
          currentSample.SqlRows += this.ReadRowCount;
        if (this._sample == null && currentSample != null && currentSample.SqlSamples != null && reader != null)
          this._sample = PXPerformanceInfo.FindLastSample(currentSample.SqlSamples, reader);
        if (this._sample == null)
          return;
        this._sample.RowCount = new int?(this.ReadRowCount);
        this._sample.Reader = (IDataReader) null;
        this._sample = (PXProfilerSqlSample) null;
      }
      catch
      {
      }
    }
  }
}

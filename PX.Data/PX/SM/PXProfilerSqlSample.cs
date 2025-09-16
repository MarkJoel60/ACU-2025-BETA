// Decompiled with JetBrains decompiler
// Type: PX.SM.PXProfilerSqlSample
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace PX.SM;

public class PXProfilerSqlSample
{
  public Stopwatch SqlTimer = new Stopwatch();
  public int? RowCount;
  public string Text;
  public string Params;
  public string Tables;
  public int SqlTextId;
  public int TraceTextId;
  public double? StartTime;
  public DateTime? SqlSampleDateTime;
  public bool QueryCache;
  public string StackTrace;
  [NonSerialized]
  public IDataReader Reader;
  public string BqlHash;
  public string BqlHashViewName;
}

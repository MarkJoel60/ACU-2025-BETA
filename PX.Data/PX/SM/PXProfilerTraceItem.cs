// Decompiled with JetBrains decompiler
// Type: PX.SM.PXProfilerTraceItem
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.SM;

public class PXProfilerTraceItem
{
  public string Source;
  public string EventType;
  public string Message;
  public string StackTrace;
  public int MessageId;
  public int? TraceId;
  public double? StartTime;
  public DateTime EventDateTime;
  public string ExceptionType;
  public string DacDescriptorInfo;
  public string EventDetails;
  public string Category;
}

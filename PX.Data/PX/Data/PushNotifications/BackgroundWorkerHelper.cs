// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.BackgroundWorkerHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using Serilog;
using Serilog.Events;
using SerilogTimings;
using SerilogTimings.Extensions;
using System;

#nullable disable
namespace PX.Data.PushNotifications;

internal class BackgroundWorkerHelper
{
  public static void SetupExecutionContext(PXTimeZoneInfo timeZoneInfo, ILogger logger = null)
  {
    if (logger == null)
      logger = PXTrace.Logger;
    PXExecutionContext current = PXExecutionContext.Current;
    current.Time = new PXTimeInfo(System.DateTime.UtcNow, timeZoneInfo);
    PXExecutionContext.Current = current;
    PXExecutionContext.Current.Restore(current);
    using (Operation operation = LoggerOperationExtensions.OperationAt(logger, (LogEventLevel) 0, new LogEventLevel?()).Begin("Select timestamp from database", Array.Empty<object>()))
    {
      PXDatabase.SelectTimeStamp();
      operation.Complete();
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Logging.Sinks.SystemEventsDbSink.SystemEventCleansing
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Threading;

#nullable disable
namespace PX.Logging.Sinks.SystemEventsDbSink;

/// <summary>
/// Manager class for SystemEvent table cleansing.
/// Records older than 30 days are removed once per configured period.
/// </summary>
public static class SystemEventCleansing
{
  private static Timer SystemEventsCleansingTimer;

  public static void Init()
  {
    TimeSpan result;
    if (!TimeSpan.TryParse(WebConfig.GetString("SystemEventsDbSink:CleansingPeriod", "1"), out result))
      result = TimeSpan.Parse("1");
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    SystemEventCleansing.SystemEventsCleansingTimer = new Timer(SystemEventCleansing.\u003C\u003EO.\u003C0\u003E__RemoveOldSystemEvents ?? (SystemEventCleansing.\u003C\u003EO.\u003C0\u003E__RemoveOldSystemEvents = new TimerCallback(SystemEventCleansing.RemoveOldSystemEvents)), (object) null, TimeSpan.FromMinutes(1.0), result);
  }

  private static void RemoveOldSystemEvents(object state)
  {
    try
    {
      System.Type table = typeof (SystemEvent);
      PXDataFieldRestrict[] dataFieldRestrictArray = new PXDataFieldRestrict[1];
      int? valueLength = new int?();
      System.DateTime dateTime = System.DateTime.UtcNow;
      dateTime = dateTime.Date;
      // ISSUE: variable of a boxed type
      __Boxed<System.DateTime> local = (ValueType) dateTime.AddDays(-30.0);
      dataFieldRestrictArray[0] = (PXDataFieldRestrict) new PXDataFieldRestrict<SystemEvent.date>(PXDbType.DateTime, valueLength, (object) local, PXComp.LE);
      PXDatabase.Delete(table, dataFieldRestrictArray);
    }
    catch
    {
    }
  }
}

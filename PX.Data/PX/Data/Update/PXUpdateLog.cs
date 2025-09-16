// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXUpdateLog
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Diagnostics;

#nullable disable
namespace PX.Data.Update;

[Obsolete("This object is obsolete and will be removed from public API. Rewrite your code without this object or contact your partner for assistance.")]
internal static class PXUpdateLog
{
  public static void WriteMessage(string message)
  {
    PXUpdateLog.WriteMessage(new PXUpdateEvent(EventLogEntryType.Error, message));
  }

  public static void WriteMessage(Exception ex)
  {
    PXUpdateLog.WriteMessage(new PXUpdateEvent(EventLogEntryType.Error, ex));
  }

  public static void WriteMessage(string message, Exception ex)
  {
    PXUpdateLog.WriteMessage(new PXUpdateEvent(EventLogEntryType.Error, message, ex));
  }

  public static void WriteMessage(PXUpdateEvent message) => PXUpdateLog.InternalWrite(message);

  private static void InternalWrite(PXUpdateEvent message)
  {
    if (message == null)
      throw new ArgumentNullException(nameof (message));
    try
    {
      new PXFileLogProvider().Write(message);
      PXFirstChanceExceptionLogger.LogMessage(message.ToString());
    }
    catch
    {
    }
    PXFirstChanceExceptionLogger.LogMessage(message.ToString());
  }

  internal static void ClearDefaultLog()
  {
    PXFileStatusWriter.ClearUpdateStatus();
    new PXFileLogProvider().ClearLog();
  }

  internal static string GetDefaultLog() => new PXFileLogProvider().GetLog();

  internal static bool HasLog() => new PXFileLogProvider().HasLog();
}

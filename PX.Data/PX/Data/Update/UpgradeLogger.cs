// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UpgradeLogger
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Diagnostics;

#nullable disable
namespace PX.Data.Update;

[Serializable]
public class UpgradeLogger
{
  private PXFileLogProvider file;
  private PXFileStatusWriter status;

  public UpgradeLogger(string version)
  {
    this.status = new PXFileStatusWriter();
    this.file = new PXFileLogProvider();
  }

  public void Write(EventLogEntryType type, string msg, Exception ex)
  {
    PXUpdateEvent message = new PXUpdateEvent(type, msg, ex);
    bool flag1 = type == EventLogEntryType.FailureAudit;
    bool flag2 = type == EventLogEntryType.SuccessAudit;
    if (type == EventLogEntryType.FailureAudit)
      type = EventLogEntryType.Error;
    if (type == EventLogEntryType.SuccessAudit)
      type = EventLogEntryType.Information;
    try
    {
      if (this.file != null)
        this.file.Write(message);
    }
    catch
    {
    }
    try
    {
      if (flag1)
        this.status.Write("Failed", msg ?? ex.Message);
      if (!flag2)
        return;
      this.status.Write("Succeeded", (string) null);
    }
    catch
    {
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Owin.DeviceHub.PrintJob
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Owin.DeviceHub;

public class PrintJob : BaseJob
{
  public string GroupID { get; set; }

  public string ReportID { get; set; }

  public string PrinterName { get; set; }

  public int NumberOfCopies { get; set; }

  public string Description { get; set; }

  public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
}

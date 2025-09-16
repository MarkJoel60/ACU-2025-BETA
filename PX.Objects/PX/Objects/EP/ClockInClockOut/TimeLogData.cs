// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.TimeLogData
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.EP.ClockInClockOut;

[PXInternalUseOnly]
public class TimeLogData
{
  public int? ProjectID { get; set; }

  public int? TaskID { get; set; }

  public string DocumentNbr { get; set; }

  public string Summary { get; set; }
}

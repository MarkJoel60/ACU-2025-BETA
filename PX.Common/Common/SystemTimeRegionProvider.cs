// Decompiled with JetBrains decompiler
// Type: PX.Common.SystemTimeRegionProvider
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;

#nullable disable
namespace PX.Common;

public class SystemTimeRegionProvider : ITimeRegionProvider
{
  public static string FindTimeRegionId(string timeZone)
  {
    return PXTimeZoneInfo.FindSystemTimeZoneById(timeZone).With<PXTimeZoneInfo, string>(SystemTimeRegionProvider.\u0002.\u000E ?? (SystemTimeRegionProvider.\u0002.\u000E = new Func<PXTimeZoneInfo, string>(SystemTimeRegionProvider.\u0002.\u0002.\u0002)));
  }

  public virtual ITimeRegion FindTimeRegionByTimeZone(string id)
  {
    ITimeRegion regionByTimeZone = (ITimeRegion) null;
    try
    {
      string timeRegionId = SystemTimeRegionProvider.FindTimeRegionId(id);
      if (!string.IsNullOrEmpty(timeRegionId))
        regionByTimeZone = (ITimeRegion) new SystemTimeRegion(TimeZoneInfo.FindSystemTimeZoneById(timeRegionId));
    }
    catch (StackOverflowException ex)
    {
      throw;
    }
    catch (OutOfMemoryException ex)
    {
      throw;
    }
    catch (Exception ex)
    {
    }
    return regionByTimeZone;
  }

  [Serializable]
  private sealed class \u0002
  {
    public static readonly SystemTimeRegionProvider.\u0002 \u0002 = new SystemTimeRegionProvider.\u0002();
    public static Func<PXTimeZoneInfo, string> \u000E;

    internal string \u0002(PXTimeZoneInfo _param1) => _param1._systemRegionId;
  }
}

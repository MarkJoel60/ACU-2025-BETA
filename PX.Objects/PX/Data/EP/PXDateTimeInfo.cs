// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.PXDateTimeInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Objects.EP;
using System;

#nullable disable
namespace PX.Data.EP;

public class PXDateTimeInfo
{
  public static int GetWeekNumber(DateTime date)
  {
    PXDateTimeInfo.Definition definition = PXDateTimeInfo.Definition.Get();
    return definition == null ? Tools.GetWeekNumber(date) : Tools.GetWeekNumber(date, definition.FirstDayOfWeek);
  }

  public static DateTime GetWeekStart(int year, int weekNumber)
  {
    PXDateTimeInfo.Definition definition = PXDateTimeInfo.Definition.Get();
    return definition == null ? Tools.GetWeekStart(year, weekNumber) : Tools.GetWeekStart(year, weekNumber, definition.FirstDayOfWeek);
  }

  private class Definition : IPrefetchable, IPXCompanyDependent
  {
    private DayOfWeek _firstDayOfWeek;

    public void Prefetch()
    {
      using (new PXConnectionScope())
      {
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<EPSetup>(new PXDataField[1]
        {
          new PXDataField(typeof (EPSetup.firstDayOfWeek).Name)
        }))
        {
          if (pxDataRecord == null)
            return;
          int? int32 = pxDataRecord.GetInt32(0);
          this._firstDayOfWeek = !int32.HasValue ? DayOfWeek.Sunday : (DayOfWeek) int32.Value;
        }
      }
    }

    public static PXDateTimeInfo.Definition Get()
    {
      return PXDatabase.GetSlot<PXDateTimeInfo.Definition>(typeof (PXDateTimeInfo).Name, new Type[1]
      {
        typeof (EPSetup)
      });
    }

    public DayOfWeek FirstDayOfWeek => this._firstDayOfWeek;
  }
}

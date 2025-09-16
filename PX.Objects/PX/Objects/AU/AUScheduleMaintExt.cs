// Decompiled with JetBrains decompiler
// Type: PX.Objects.AU.AUScheduleMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.AU;

public class AUScheduleMaintExt : PXGraphExtension<AUScheduleMaint>
{
  protected void AUSchedule_RowUpdated(
    PXCache sender,
    PXRowUpdatedEventArgs e,
    PXRowUpdated baseHandler)
  {
    try
    {
      baseHandler.Invoke(sender, e);
    }
    catch (PXFinPeriodDoesNotExist ex)
    {
      AUSchedule row = (AUSchedule) e.Row;
      sender.RaiseExceptionHandling<AUSchedule.nextRunDate>((object) row, (object) row.NextRunDate, (Exception) new PXSetPropertyException(PXMessages.Localize("The financial period for the next execution date is not opened.")));
      AUSchedule auSchedule = row;
      DateTime? nullable1 = row.NextRunDate;
      DateTime date = nullable1.Value.Date;
      ref DateTime local = ref date;
      nullable1 = row.NextRunTime;
      int hour = nullable1.Value.Hour;
      nullable1 = row.NextRunTime;
      int minute = nullable1.Value.Minute;
      TimeSpan timeSpan = new TimeSpan(hour, minute, 0);
      DateTime? nullable2 = new DateTime?(local.Add(timeSpan));
      auSchedule.NextRunTime = nullable2;
      ((PXSelectBase<AUScheduleCurrentScreen>) this.Base.ScheduleCurrentScreen).Current.ScreenID = row.ScreenID;
    }
  }
}

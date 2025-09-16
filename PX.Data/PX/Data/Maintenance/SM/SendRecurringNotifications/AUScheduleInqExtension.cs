// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.SM.SendRecurringNotifications.AUScheduleInqExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;

#nullable disable
namespace PX.Data.Maintenance.SM.SendRecurringNotifications;

public class AUScheduleInqExtension : PXGraphExtension<AUScheduleInq>
{
  public virtual void _(
    Events.RowSelected<AUScheduleInq.AUScheduleExt> e,
    PXRowSelected baseHandler)
  {
    baseHandler(e.Cache, e.Args);
    string error = "SM205060".Equals(e.Row?.ScreenID, StringComparison.OrdinalIgnoreCase) ? "The Send Reports (SM205060) form will be deprecated in the next release. To avoid interruptions in report sending, use the Migrate button on the Automation Schedule (SM205020) form to migrate your automation schedules. This process will create an email template for each report template in the schedule and link it to a new schedule." : (string) null;
    PXUIFieldAttribute.SetWarning<AUSchedule.screenID>(e.Cache, (object) e.Row, error);
  }
}

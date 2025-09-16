// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.SM.SendRecurringNotifications.Messages
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.Maintenance.SM.SendRecurringNotifications;

[PXLocalizable]
public static class Messages
{
  public const string ScheduleHaveToBeMigrated = "The Send Reports (SM205060) form will be deprecated in the next release. To avoid interruptions in report sending, use the Migrate button in the toolbar to migrate your schedule. This process will create an email template for each report template in the schedule and link it to a new schedule.";
  public const string ThisScreenWillBeDeprecated = "This form will be deprecated in the next release. To avoid interruptions in report sending, use the Migrate button on the Automation Schedule (SM205020) form to migrate your automation schedules. This process will create an email template for each report template in the schedule and link it to a new schedule.";
  public const string SendReportsScreenWiilBeDeprecated = "The Send Reports (SM205060) form will be deprecated in the next release. To avoid interruptions in report sending, use the Migrate button on the Automation Schedule (SM205020) form to migrate your automation schedules. This process will create an email template for each report template in the schedule and link it to a new schedule.";
  public const string MigratedSchedulePrefix = "Migrated";
  public const string NotificationWithScreenIdContainsSchedule = "If this email template is sent by schedules, the system will not be able to retrieve values for the screen fields and actions. As a result, the generated emails and attached reports will contain empty placeholders, if any. Consider reviewing the email content to avoid confusion due to missing placeholder values.";
  public const string NoScheduleIsLinked = "No automation schedule is linked to trigger this email notification. Create an automation schedule on the Send by Schedule tab.";
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.SM.SendRecurringNotifications.NotificationSchedule
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Data.Maintenance.SM.SendRecurringNotifications;

[PXCacheName("Notification Schedule")]
public class NotificationSchedule : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (Notification.notificationID))]
  [PXParent(typeof (Select<Notification, Where<Notification.notificationID, Equal<Current<NotificationSchedule.notificationID>>>>))]
  [PXDBChildIdentity(typeof (Notification.notificationID))]
  [PXForeignReference(typeof (NotificationSchedule.FK.NotificationNotificationID))]
  [PXUIField(DisplayName = "Notification ID")]
  public virtual int? NotificationID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXParent(typeof (Select<AUSchedule, Where<AUSchedule.scheduleID, Equal<Current<NotificationSchedule.scheduleID>>>>))]
  [PXDBChildIdentity(typeof (AUSchedule.scheduleID))]
  [PXForeignReference(typeof (NotificationSchedule.FK.AUScheduleScheduleID))]
  [PXUIField(DisplayName = "Schedule ID")]
  public int? ScheduleID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Active", Enabled = false)]
  [PXDBScalar(typeof (Search<AUSchedule.isActive, Where<AUSchedule.scheduleID, Equal<NotificationSchedule.scheduleID>>>))]
  public bool? IsActive { get; set; }

  [PXGuid]
  public Guid? ScheduleNoteID { get; set; }

  public class PK : 
    PrimaryKeyOf<
    #nullable disable
    NotificationSchedule>.By<NotificationSchedule.notificationID, NotificationSchedule.scheduleID>
  {
    public static NotificationSchedule Find(PXGraph graph, int? notificationID, int? scheduleID)
    {
      return PrimaryKeyOf<NotificationSchedule>.By<NotificationSchedule.notificationID, NotificationSchedule.scheduleID>.FindBy(graph, (object) notificationID, (object) scheduleID);
    }
  }

  public static class FK
  {
    public class AUScheduleScheduleID : 
      PrimaryKeyOf<AUSchedule>.By<AUSchedule.scheduleID>.ForeignKeyOf<NotificationSchedule>.By<NotificationSchedule.scheduleID>
    {
    }

    public class NotificationNotificationID : 
      PrimaryKeyOf<Notification>.By<Notification.notificationID>.ForeignKeyOf<NotificationSchedule>.By<NotificationSchedule.notificationID>
    {
    }
  }

  public abstract class notificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    NotificationSchedule.notificationID>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NotificationSchedule.scheduleID>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  NotificationSchedule.isActive>
  {
  }

  public abstract class scheduleNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    NotificationSchedule.scheduleNoteID>
  {
  }
}

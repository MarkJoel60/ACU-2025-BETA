// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSGPSTrackingLocation
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.FS;
using PX.Objects.EP;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.FS;

[Serializable]
public class FSGPSTrackingLocation : FSGPSTrackingRequest
{
  [PXInt]
  [DayOfWeek]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Day of Week")]
  public virtual int? WeekDay { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "User Name")]
  [PXDBDefault(typeof (Users.username))]
  [PXParent(typeof (Select<Users, Where<Users.username, Equal<Current<FSGPSTrackingLocation.userName>>>>))]
  public virtual string UserName { get; set; }

  [PXDBString(32 /*0x20*/)]
  [PXDefault(typeof (Search<UserPreferences.timeZone, Where<UserPreferences.userID, Equal<Current<Users.pKID>>>>))]
  [PXUIField(DisplayName = "Time Zone")]
  public virtual string TimeZoneID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Sunday")]
  public virtual bool? WeeklyOnDay1 { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Monday")]
  public virtual bool? WeeklyOnDay2 { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tuesday")]
  public virtual bool? WeeklyOnDay3 { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Wednesday")]
  public virtual bool? WeeklyOnDay4 { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Thursday")]
  public virtual bool? WeeklyOnDay5 { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Friday")]
  public virtual bool? WeeklyOnDay6 { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Saturday")]
  public virtual bool? WeeklyOnDay7 { get; set; }

  [AUSchedule.AUSeparateTime(typeof (FSGPSTrackingRequest.startDate), UseTimeZone = false, DisplayMask = "t")]
  [PXUIField(DisplayName = "Start Time")]
  [PXUIVerify]
  public virtual DateTime? StartTime { get; set; }

  [AUSchedule.AUSeparateTime(typeof (FSGPSTrackingRequest.endDate), UseTimeZone = false, DisplayMask = "t")]
  [PXUIField(DisplayName = "End Time")]
  [PXUIVerify]
  public virtual DateTime? EndTime { get; set; }

  public abstract class weekDay : IBqlField, IBqlOperand
  {
  }

  public abstract class userName : IBqlField, IBqlOperand
  {
  }

  public abstract class timeZoneID : IBqlField, IBqlOperand
  {
  }

  public abstract class weeklyOnDay1 : IBqlField, IBqlOperand
  {
  }

  public abstract class weeklyOnDay2 : IBqlField, IBqlOperand
  {
  }

  public abstract class weeklyOnDay3 : IBqlField, IBqlOperand
  {
  }

  public abstract class weeklyOnDay4 : IBqlField, IBqlOperand
  {
  }

  public abstract class weeklyOnDay5 : IBqlField, IBqlOperand
  {
  }

  public abstract class weeklyOnDay6 : IBqlField, IBqlOperand
  {
  }

  public abstract class weeklyOnDay7 : IBqlField, IBqlOperand
  {
  }

  public abstract class startTime : IBqlField, IBqlOperand
  {
  }

  public abstract class endTime : IBqlField, IBqlOperand
  {
  }
}

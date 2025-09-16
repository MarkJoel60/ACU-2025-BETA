// Decompiled with JetBrains decompiler
// Type: PX.Common.PXTimeZoneInfo
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#nullable disable
namespace PX.Common;

public sealed class PXTimeZoneInfo
{
  public static readonly PXTimeZoneInfo Invariant;
  private TimeSpan \u0002;
  private readonly string \u000E;
  private readonly string \u0006;
  private readonly string \u0008;
  internal readonly string _systemRegionId;
  private static readonly IDictionary<string, PXTimeZoneInfo> \u0003;
  private static ITimeRegionProvider \u000F = (ITimeRegionProvider) new SystemTimeRegionProvider();
  private ITimeRegion \u0005;
  private bool \u0002\u2009;

  static PXTimeZoneInfo()
  {
    PXTimeZoneInfo.\u0003 = (IDictionary<string, PXTimeZoneInfo>) new Dictionary<string, PXTimeZoneInfo>(87);
    PXTimeZoneInfo.\u0003.Add("GMTM1200A", new PXTimeZoneInfo("GMTM1200A", new TimeSpan(-12, 0, 0), "(GMT-12:00) International Date Line West", "Dateline Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM1000A", new PXTimeZoneInfo("GMTM1000A", new TimeSpan(-10, 0, 0), "(GMT-10:00) Hawaii", "Hawaiian Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0900A", new PXTimeZoneInfo("GMTM0900A", new TimeSpan(-9, 0, 0), "(GMT-09:00) Alaska", "Alaskan Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0800A", new PXTimeZoneInfo("GMTM0800A", new TimeSpan(-8, 0, 0), "(GMT-08:00) Pacific Time (US & Canada)", "Pacific Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0800G", new PXTimeZoneInfo("GMTM0800G", new TimeSpan(-8, 0, 0), "(GMT-08:00) Tijuana, Baja California", "Pacific Standard Time (Mexico)"));
    PXTimeZoneInfo.\u0003.Add("GMTM0700A", new PXTimeZoneInfo("GMTM0700A", new TimeSpan(-7, 0, 0), "(GMT-07:00) Arizona", "US Mountain Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0700G", new PXTimeZoneInfo("GMTM0700G", new TimeSpan(-7, 0, 0), "(GMT-07:00) Chihuahua, La Paz, Mazatlan", "Mountain Standard Time (Mexico)"));
    PXTimeZoneInfo.\u0003.Add("GMTM0700S", new PXTimeZoneInfo("GMTM0700S", new TimeSpan(-7, 0, 0), "(GMT-07:00) Mountain Time (US & Canada)", "Mountain Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0600A", new PXTimeZoneInfo("GMTM0600A", new TimeSpan(-6, 0, 0), "(GMT-06:00) Central America", "Central America Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0600G", new PXTimeZoneInfo("GMTM0600G", new TimeSpan(-6, 0, 0), "(GMT-06:00) Central Time (US & Canada)", "Central Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0600M", new PXTimeZoneInfo("GMTM0600M", new TimeSpan(-6, 0, 0), "(GMT-06:00) Guadalajara, Mexico City, Monterrey", "Central Standard Time (Mexico)"));
    PXTimeZoneInfo.\u0003.Add("GMTM0600Y", new PXTimeZoneInfo("GMTM0600Y", new TimeSpan(-6, 0, 0), "(GMT-06:00) Saskatchewan", "Canada Central Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0500A", new PXTimeZoneInfo("GMTM0500A", new TimeSpan(-5, 0, 0), "(GMT-05:00) Bogota, Lima, Quito, Rio Branco", "SA Pacific Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0500G", new PXTimeZoneInfo("GMTM0500G", new TimeSpan(-5, 0, 0), "(GMT-05:00) Eastern Time (US & Canada)", "Eastern Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0500M", new PXTimeZoneInfo("GMTM0500M", new TimeSpan(-5, 0, 0), "(GMT-05:00) Indiana (East)", "US Eastern Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0400Y", new PXTimeZoneInfo("GMTM0400Y", new TimeSpan(-4, 0, 0), "(GMT-04:00) Caracas", "Venezuela Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0400A", new PXTimeZoneInfo("GMTM0400A", new TimeSpan(-4, 0, 0), "(GMT-04:00) Atlantic Time (Canada)", "Atlantic Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0400G", new PXTimeZoneInfo("GMTM0400G", new TimeSpan(-4, 0, 0), "(GMT-04:00) La Paz", "SA Western Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0400M", new PXTimeZoneInfo("GMTM0400M", new TimeSpan(-4, 0, 0), "(GMT-04:00) Manaus", "Atlantic Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0400S", new PXTimeZoneInfo("GMTM0400S", new TimeSpan(-4, 0, 0), "(GMT-04:00) Santiago", "Pacific SA Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0330A", new PXTimeZoneInfo("GMTM0330A", new TimeSpan(-3, -30, 0), "(GMT-03:30) Newfoundland", "Newfoundland Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0300A", new PXTimeZoneInfo("GMTM0300A", new TimeSpan(-3, 0, 0), "(GMT-03:00) Brasilia", "E. South America Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0300G", new PXTimeZoneInfo("GMTM0300G", new TimeSpan(-3, 0, 0), "(GMT-03:00) Buenos Aires, Georgetown", "Argentina Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0300M", new PXTimeZoneInfo("GMTM0300M", new TimeSpan(-3, 0, 0), "(GMT-03:00) Greenland", "Greenland Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0300S", new PXTimeZoneInfo("GMTM0300S", new TimeSpan(-3, 0, 0), "(GMT-03:00) Montevideo", "Montevideo Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0200A", new PXTimeZoneInfo("GMTM0200A", new TimeSpan(-2, 0, 0), "(GMT-02:00) Mid-Atlantic", "Mid-Atlantic Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0100A", new PXTimeZoneInfo("GMTM0100A", new TimeSpan(-1, 0, 0), "(GMT-01:00) Azores", "Azores Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTM0100M", new PXTimeZoneInfo("GMTM0100M", new TimeSpan(-1, 0, 0), "(GMT-01:00) Cape Verde Is.", "Cape Verde Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTE0000A", new PXTimeZoneInfo("GMTE0000A", new TimeSpan(0, 0, 0), "(GMT) Casablanca", "Morocco Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTE0000B", new PXTimeZoneInfo("GMTE0000B", new TimeSpan(0, 0, 0), "(GMT) Monrovia, Reykjavik", "Greenwich Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTE0000K", new PXTimeZoneInfo("GMTE0000K", new TimeSpan(0, 0, 0), "(GMT) Greenwich Mean Time : Dublin, Edinburgh, Lisbon, London", "GMT Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTE0000U", new PXTimeZoneInfo("GMTE0000U", new TimeSpan(0, 0, 0), "(GMT) Universal Standard Time", "UTC"));
    PXTimeZoneInfo.\u0003.Add("GMTP0100A", new PXTimeZoneInfo("GMTP0100A", new TimeSpan(1, 0, 0), "(GMT+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna", "W. Europe Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0100G", new PXTimeZoneInfo("GMTP0100G", new TimeSpan(1, 0, 0), "(GMT+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague", "Central Europe Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0100M", new PXTimeZoneInfo("GMTP0100M", new TimeSpan(1, 0, 0), "(GMT+01:00) Brussels, Copenhagen, Madrid, Paris", "Romance Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0100S", new PXTimeZoneInfo("GMTP0100S", new TimeSpan(1, 0, 0), "(GMT+01:00) Sarajevo, Skopje, Warsaw, Zagreb", "Central European Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0100Y", new PXTimeZoneInfo("GMTP0100Y", new TimeSpan(1, 0, 0), "(GMT+01:00) West Central Africa", "W. Central Africa Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0200A", new PXTimeZoneInfo("GMTP0200A", new TimeSpan(2, 0, 0), "(GMT+02:00) Amman", "Jordan Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0200D", new PXTimeZoneInfo("GMTP0200D", new TimeSpan(2, 0, 0), "(GMT+02:00) Athens, Bucharest, Istanbul", "GTB Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0200G", new PXTimeZoneInfo("GMTP0200G", new TimeSpan(2, 0, 0), "(GMT+02:00) Beirut", "Middle East Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0200J", new PXTimeZoneInfo("GMTP0200J", new TimeSpan(2, 0, 0), "(GMT+02:00) Cairo", "Egypt Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0200M", new PXTimeZoneInfo("GMTP0200M", new TimeSpan(2, 0, 0), "(GMT+02:00) Harare, Pretoria", "South Africa Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0200P", new PXTimeZoneInfo("GMTP0200P", new TimeSpan(2, 0, 0), "(GMT+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius", "FLE Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0200S", new PXTimeZoneInfo("GMTP0200S", new TimeSpan(2, 0, 0), "(GMT+02:00) Jerusalem", "Israel Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0200V", new PXTimeZoneInfo("GMTP0200V", new TimeSpan(2, 0, 0), "(GMT+02:00) Minsk", "E. Europe Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0200Y", new PXTimeZoneInfo("GMTP0200Y", new TimeSpan(2, 0, 0), "(GMT+02:00) Windhoek", "South Africa Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0300A", new PXTimeZoneInfo("GMTP0300A", new TimeSpan(3, 0, 0), "(GMT+03:00) Baghdad", "Arabic Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0300G", new PXTimeZoneInfo("GMTP0300G", new TimeSpan(3, 0, 0), "(GMT+03:00) Kuwait, Riyadh", "Arab Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0300M", new PXTimeZoneInfo("GMTP0300M", new TimeSpan(3, 0, 0), "(GMT+03:00) Moscow, St. Petersburg, Volgograd", "Russian Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0300S", new PXTimeZoneInfo("GMTP0300S", new TimeSpan(3, 0, 0), "(GMT+03:00) Nairobi", "E. Africa Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0400Y", new PXTimeZoneInfo("GMTP0400Y", new TimeSpan(4, 0, 0), "(GMT+04:00) Tbilisi", "Georgian Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0330A", new PXTimeZoneInfo("GMTP0330A", new TimeSpan(3, 30, 0), "(GMT+03:30) Tehran", "Iran Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0400A", new PXTimeZoneInfo("GMTP0400A", new TimeSpan(4, 0, 0), "(GMT+04:00) Abu Dhabi, Muscat", "Arabian Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0400G", new PXTimeZoneInfo("GMTP0400G", new TimeSpan(4, 0, 0), "(GMT+04:00) Baku", "Azerbaijan Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0400M", new PXTimeZoneInfo("GMTP0400M", new TimeSpan(4, 0, 0), "(GMT+04:00) Caucasus Standard Time", "Caucasus Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0400S", new PXTimeZoneInfo("GMTP0400S", new TimeSpan(4, 0, 0), "(GMT+04:00) Yerevan", "Caucasus Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0430A", new PXTimeZoneInfo("GMTP0430A", new TimeSpan(4, 30, 0), "(GMT+04:30) Kabul", "Afghanistan Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0500A", new PXTimeZoneInfo("GMTP0500A", new TimeSpan(5, 0, 0), "(GMT+05:00) Ekaterinburg", "Ekaterinburg Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0500M", new PXTimeZoneInfo("GMTP0500M", new TimeSpan(5, 0, 0), "(GMT+05:00) Islamabad, Karachi", "Pakistan Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0500N", new PXTimeZoneInfo("GMTP0500N", new TimeSpan(5, 0, 0), "(GMT+05:00) Tashkent", "West Asia Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0530A", new PXTimeZoneInfo("GMTP0530A", new TimeSpan(5, 30, 0), "(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi", "India Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0530M", new PXTimeZoneInfo("GMTP0530M", new TimeSpan(5, 30, 0), "(GMT+05:30) Sri Jayawardenepura", "Sri Lanka Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0545A", new PXTimeZoneInfo("GMTP0545A", new TimeSpan(5, 45, 0), "(GMT+05:45) Kathmandu", "Nepal Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0700N", new PXTimeZoneInfo("GMTP0700N", new TimeSpan(7, 0, 0), "(GMT+07:00) Almaty, Novosibirsk", "N. Central Asia Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0600M", new PXTimeZoneInfo("GMTP0600M", new TimeSpan(6, 0, 0), "(GMT+06:00) Astana", "Central Asia Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0600N", new PXTimeZoneInfo("GMTP0600N", new TimeSpan(6, 0, 0), "(GMT+06:00) Dhaka", "Bangladesh Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0630A", new PXTimeZoneInfo("GMTP0630A", new TimeSpan(6, 30, 0), "(GMT+06:30) Yangon (Rangoon)", "Myanmar Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0700A", new PXTimeZoneInfo("GMTP0700A", new TimeSpan(7, 0, 0), "(GMT+07:00) Bangkok, Hanoi, Jakarta", "SE Asia Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0700M", new PXTimeZoneInfo("GMTP0700M", new TimeSpan(7, 0, 0), "(GMT+07:00) Krasnoyarsk", "North Asia Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0800A", new PXTimeZoneInfo("GMTP0800A", new TimeSpan(8, 0, 0), "(GMT+08:00) Beijing, Chongqing, Hong Kong, Urumqi", "China Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0800G", new PXTimeZoneInfo("GMTP0800G", new TimeSpan(8, 0, 0), "(GMT+08:00) Irkutsk, Ulaan Bataar", "North Asia East Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0800M", new PXTimeZoneInfo("GMTP0800M", new TimeSpan(8, 0, 0), "(GMT+08:00) Kuala Lumpur, Singapore", "Singapore Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0800S", new PXTimeZoneInfo("GMTP0800S", new TimeSpan(8, 0, 0), "(GMT+08:00) Perth", "W. Australia Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0800Y", new PXTimeZoneInfo("GMTP0800Y", new TimeSpan(8, 0, 0), "(GMT+08:00) Taipei", "Taipei Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0900A", new PXTimeZoneInfo("GMTP0900A", new TimeSpan(9, 0, 0), "(GMT+09:00) Osaka, Sapporo, Tokyo", "Tokyo Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0900G", new PXTimeZoneInfo("GMTP0900G", new TimeSpan(9, 0, 0), "(GMT+09:00) Seoul", "Korea Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0900M", new PXTimeZoneInfo("GMTP0900M", new TimeSpan(9, 0, 0), "(GMT+09:00) Yakutsk", "Yakutsk Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0930A", new PXTimeZoneInfo("GMTP0930A", new TimeSpan(9, 30, 0), "(GMT+09:30) Adelaide", "Cen. Australia Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP0930M", new PXTimeZoneInfo("GMTP0930M", new TimeSpan(9, 30, 0), "(GMT+09:30) Darwin", "AUS Central Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP1000A", new PXTimeZoneInfo("GMTP1000A", new TimeSpan(10, 0, 0), "(GMT+10:00) Brisbane", "E. Australia Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP1000G", new PXTimeZoneInfo("GMTP1000G", new TimeSpan(10, 0, 0), "(GMT+10:00) Canberra, Melbourne, Sydney", "AUS Eastern Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP1000M", new PXTimeZoneInfo("GMTP1000M", new TimeSpan(10, 0, 0), "(GMT+10:00) Guam, Port Moresby", "West Pacific Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP1000S", new PXTimeZoneInfo("GMTP1000S", new TimeSpan(10, 0, 0), "(GMT+10:00) Hobart", "Tasmania Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP1000Y", new PXTimeZoneInfo("GMTP1000Y", new TimeSpan(10, 0, 0), "(GMT+10:00) Vladivostok", "Vladivostok Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP1100A", new PXTimeZoneInfo("GMTP1100A", new TimeSpan(11, 0, 0), "(GMT+11:00) Magadan", "Magadan Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP1100B", new PXTimeZoneInfo("GMTP1100B", new TimeSpan(11, 0, 0), "(GMT+11:00) Solomon Is., New Caledonia", "Central Pacific Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP1200A", new PXTimeZoneInfo("GMTP1200A", new TimeSpan(12, 0, 0), "(GMT+12:00) Auckland, Wellington", "New Zealand Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP1200K", new PXTimeZoneInfo("GMTP1200K", new TimeSpan(12, 0, 0), "(GMT+12:00) Fiji", "Fiji Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP1200G", new PXTimeZoneInfo("GMTP1200G", new TimeSpan(12, 0, 0), "(GMT+12:00) Kamchatka, Marshall Is.", "Kamchatka Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP1300A", new PXTimeZoneInfo("GMTP1300A", new TimeSpan(13, 0, 0), "(GMT+13:00) Nuku'alofa", "Tonga Standard Time"));
    PXTimeZoneInfo.\u0003.Add("GMTP1300M", new PXTimeZoneInfo("GMTP1300M", new TimeSpan(13, 0, 0), "(GMT+13:00) Midway Island, Samoa", "Samoa Standard Time"));
    PXTimeZoneInfo.Invariant = PXTimeZoneInfo.\u0003["GMTE0000U"];
  }

  private PXTimeZoneInfo(string _param1, TimeSpan _param2, string _param3, string _param4)
  {
    this.\u0002 = _param2;
    this.\u0006 = _param3;
    this.\u000E = _param1;
    this.\u0008 = $"GMT{(_param2.Hours >= 0 ? (object) "+" : (object) "")}{_param2.Hours:00}:{Math.Abs(_param2.Minutes):00}";
    this._systemRegionId = _param4;
  }

  internal static void RegisterTimeRegionProvider(ITimeRegionProvider _param0)
  {
    PXTimeZoneInfo.\u000F = _param0 ?? throw new ArgumentNullException("timeRegionProvider");
  }

  private static long \u0002(DateTime _param0, PXTimeZoneInfo _param1, bool _param2)
  {
    if (_param1 == null)
      throw new ArgumentNullException("destinationTimeZone");
    if ((_param2 ? 0 : (_param1.IsAmbiguousTime(_param0) ? 1 : 0)) != 0)
      return 0;
    int year = _param2 ? _param0.Year : PXTimeZoneInfo.\u0002(_param0.Ticks - _param1.BaseUtcOffset.Ticks).Year;
    DaylightSavingTime daylightSavingTime = _param1.GetDaylightSavingTime(year);
    if (daylightSavingTime == null || !daylightSavingTime.IsActive)
      return 0;
    return (PXTimeZoneInfo.\u0002.\u000E ?? (PXTimeZoneInfo.\u0002.\u000E = new Func<DateTime, DateTime, DateTime, bool>(PXTimeZoneInfo.\u0002.\u0002.\u0002)))(_param2 ? PXTimeZoneInfo.\u0002(_param0.Ticks + _param1.BaseUtcOffset.Ticks + daylightSavingTime.BaseOffset.Ticks) : _param0, _param2 ? daylightSavingTime.Start : daylightSavingTime.Start + daylightSavingTime.BaseOffset + daylightSavingTime.DaylightOffset, _param2 ? daylightSavingTime.End - daylightSavingTime.DaylightOffset : daylightSavingTime.End) ? daylightSavingTime.DaylightOffset.Ticks + daylightSavingTime.BaseOffset.Ticks : daylightSavingTime.BaseOffset.Ticks;
  }

  private static DateTime \u0002(long _param0)
  {
    if (_param0 < DateTime.MinValue.Ticks)
      return DateTime.MinValue;
    return _param0 > DateTime.MaxValue.Ticks ? DateTime.MaxValue : new DateTime(_param0);
  }

  public static DateTime ConvertTimeToUtc(
    DateTime dateTime,
    PXTimeZoneInfo sourceTimeZone,
    bool useDST = true)
  {
    DateTime utc = PXTimeZoneInfo.\u0002(dateTime.Ticks - sourceTimeZone.\u0002.Ticks);
    if (useDST)
    {
      long num = PXTimeZoneInfo.\u0002(dateTime, sourceTimeZone, false);
      utc = PXTimeZoneInfo.\u0002(utc.Ticks - num);
    }
    return utc;
  }

  public static DateTime ConvertTimeFromUtc(
    DateTime dateTime,
    PXTimeZoneInfo destinationTimeZone,
    bool useDST = true)
  {
    DateTime dateTime1 = PXTimeZoneInfo.\u0002(dateTime.Ticks + destinationTimeZone.\u0002.Ticks);
    if (useDST)
    {
      long num = PXTimeZoneInfo.\u0002(dateTime, destinationTimeZone, true);
      dateTime1 = PXTimeZoneInfo.\u0002(dateTime1.Ticks + num);
    }
    return dateTime1;
  }

  public static PXTimeZoneInfo CreateCustomTimeZone(
    string id,
    TimeSpan baseUtcOffset,
    string displayName)
  {
    if (PXTimeZoneInfo.\u0003.ContainsKey(id))
      throw new ArgumentException("The specified ID is used by the system PXTimeZoneInfo.", nameof (id));
    return new PXTimeZoneInfo(id, baseUtcOffset, displayName, (string) null);
  }

  public static PXTimeZoneInfo FindSystemTimeZoneById(string id)
  {
    return string.IsNullOrEmpty(id) || !PXTimeZoneInfo.\u0003.ContainsKey(id) ? (PXTimeZoneInfo) null : PXTimeZoneInfo.\u0003[id];
  }

  public static PXTimeZoneInfo Local => PXTimeZoneInfo.\u0002(TimeZoneInfo.Local);

  private static PXTimeZoneInfo \u0002(TimeZoneInfo _param0)
  {
    return PXTimeZoneInfo.FindSystemTimeZoneBySystemRegionId(_param0.Id) ?? new PXTimeZoneInfo(_param0.StandardName, _param0.BaseUtcOffset, _param0.DisplayName, _param0.Id);
  }

  public static PXTimeZoneInfo FindSystemTimeZoneByOffset(double offset)
  {
    foreach (PXTimeZoneInfo timeZoneByOffset in (IEnumerable<PXTimeZoneInfo>) PXTimeZoneInfo.\u0003.Values)
    {
      if (Math.Abs(offset - timeZoneByOffset.BaseUtcOffset.TotalSeconds) <= 1.0)
        return timeZoneByOffset;
    }
    return (PXTimeZoneInfo) null;
  }

  public static PXTimeZoneInfo FindSystemTimeZoneBySystemRegionId(string systemRegionId)
  {
    return PXTimeZoneInfo.\u0003.Values.FirstOrDefault<PXTimeZoneInfo>(new Func<PXTimeZoneInfo, bool>(new PXTimeZoneInfo.\u000E()
    {
      \u0002 = systemRegionId
    }.\u0002));
  }

  public static ReadOnlyCollection<PXTimeZoneInfo> GetSystemTimeZones()
  {
    return new ReadOnlyCollection<PXTimeZoneInfo>((IList<PXTimeZoneInfo>) new List<PXTimeZoneInfo>((IEnumerable<PXTimeZoneInfo>) PXTimeZoneInfo.\u0003.Values));
  }

  public TimeSpan UtcOffset => this.GetUtcOffset(PXExecutionContext.Current.Time.UtcNow);

  internal TimeSpan GetUtcOffset(DateTime _param1)
  {
    return this.\u0002 + new TimeSpan(PXTimeZoneInfo.\u0002(_param1, this, true));
  }

  public TimeSpan BaseUtcOffset => this.\u0002;

  public string DisplayName => this.\u0006;

  public string ShortName => this.\u0008;

  public string RegionId => this._systemRegionId;

  public string Id => this.\u000E;

  public static DateTime Now
  {
    get
    {
      return PXTimeZoneInfo.ConvertTimeFromUtc(PXExecutionContext.Current.Time.UtcNow, LocaleInfo.GetTimeZone());
    }
  }

  public static DateTime NowWithoutDST
  {
    get
    {
      return PXTimeZoneInfo.ConvertTimeFromUtc(PXExecutionContext.Current.Time.UtcNow, LocaleInfo.GetTimeZone(), false);
    }
  }

  public static DateTime UtcNow => PXExecutionContext.Current.Time.UtcNow;

  public static DateTime Today => PXTimeZoneInfo.Now.Date;

  public static DateTime UtcToday => PXTimeZoneInfo.UtcNow.Date;

  public static string GetLongTime()
  {
    return string.Format("{0:dd MMM yyyy HH:mm tt} {1}", (object) PXTimeZoneInfo.Now, (object) LocaleInfo.GetTimeZone().ShortName);
  }

  public DaylightSavingTime GetDaylightSavingTime(int year)
  {
    this.\u0002();
    return new DaylightSavingTime(year, this.\u0005);
  }

  public bool IsAmbiguousTime(DateTime dateTime)
  {
    this.\u0002();
    return this.\u0005.IsAmbiguousTime(dateTime);
  }

  public TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules()
  {
    this.\u0002();
    return this.\u0005.GetAdjustmentRules();
  }

  private void \u0002()
  {
    if (this.\u0002\u2009)
      return;
    if (!string.IsNullOrEmpty(this.\u000E))
      this.\u0005 = PXTimeZoneInfo.\u000F.FindTimeRegionByTimeZone(this.\u000E);
    this.\u0002\u2009 = true;
  }

  [Serializable]
  private sealed class \u0002
  {
    public static readonly PXTimeZoneInfo.\u0002 \u0002 = new PXTimeZoneInfo.\u0002();
    public static Func<DateTime, DateTime, DateTime, bool> \u000E;

    internal bool \u0002(DateTime _param1, DateTime _param2, DateTime _param3)
    {
      return !(_param2 < _param3) ? _param1 >= _param2 || _param1 < _param3 : _param1 >= _param2 && _param1 < _param3;
    }
  }

  private sealed class \u000E
  {
    public string \u0002;

    internal bool \u0002(PXTimeZoneInfo _param1)
    {
      return string.Equals(_param1._systemRegionId, this.\u0002, StringComparison.OrdinalIgnoreCase);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CustomTimeRegionProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

public class CustomTimeRegionProvider : SystemTimeRegionProvider
{
  public override ITimeRegion FindTimeRegionByTimeZone(string id)
  {
    return string.IsNullOrEmpty(id) ? (ITimeRegion) null : (ITimeRegion) new CustomTimeRegionProvider.TimeRegion(this, id, base.FindTimeRegionByTimeZone(id));
  }

  private TimeZoneInfo.AdjustmentRule GetAdjustmentRule(int year, string id)
  {
    return CustomTimeRegionProvider.Definition.GetRule(year, id);
  }

  private static TimeZoneInfo.TransitionTime GetTransitionTime(DateTime source)
  {
    DateTime timeOfDay = new DateTime(new TimeSpan(source.Hour, source.Minute, source.Second).Ticks);
    int month1 = source.Month;
    int day1 = source.Day;
    int month2 = month1;
    int day2 = day1;
    return TimeZoneInfo.TransitionTime.CreateFixedDateRule(timeOfDay, month2, day2);
  }

  private sealed class TimeRegion : ITimeRegion
  {
    private readonly CustomTimeRegionProvider _provider;
    private readonly string _id;
    private readonly ITimeRegion _systemTimeRegion;

    public TimeRegion(CustomTimeRegionProvider provider, string id)
    {
      if (provider == null)
        throw new ArgumentNullException(nameof (provider));
      if (string.IsNullOrEmpty(id))
        throw new ArgumentNullException(nameof (id));
      this._provider = provider;
      this._id = id;
    }

    public TimeRegion(CustomTimeRegionProvider provider, string id, ITimeRegion systemTimeRegion)
      : this(provider, id)
    {
      this._systemTimeRegion = systemTimeRegion;
    }

    public TimeZoneInfo.AdjustmentRule GetAdjustmentRule(int year)
    {
      TimeZoneInfo.AdjustmentRule adjustmentRule = this._provider.GetAdjustmentRule(year, this._id);
      if (adjustmentRule != null)
        return adjustmentRule;
      return this._systemTimeRegion?.GetAdjustmentRule(year);
    }

    public TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules()
    {
      return this._systemTimeRegion?.GetAdjustmentRules();
    }

    public bool IsAmbiguousTime(DateTime dateTime)
    {
      DaylightSavingTime daylightSavingTime1 = new DaylightSavingTime(dateTime.Year, (ITimeRegion) this);
      Func<DaylightSavingTime, DateTime, bool> func = (Func<DaylightSavingTime, DateTime, bool>) ((dst, dt) => dst.IsActive && dateTime >= dst.End - dst.DaylightOffset && dateTime <= dst.End);
      if (func(daylightSavingTime1, dateTime))
        return true;
      if (daylightSavingTime1.Start.Year >= daylightSavingTime1.End.Year)
        return false;
      DaylightSavingTime daylightSavingTime2 = new DaylightSavingTime(dateTime.Year - 1, (ITimeRegion) this);
      return func(daylightSavingTime2, dateTime);
    }

    public bool SupportsDaylightSavingTime => true;
  }

  private sealed class DefinitionParameters
  {
    private readonly string _id;
    private readonly int _year;

    public DefinitionParameters(string id, int year)
    {
      this._id = id;
      this._year = year;
    }

    public string Id => this._id;

    public int Year => this._year;
  }

  private sealed class Definition : 
    IPrefetchable<CustomTimeRegionProvider.DefinitionParameters>,
    IPXCompanyDependent
  {
    private const string _SLOT_KEY = "_CUSTOM_TIME_REGION_PROVIDER_SLOT_KEY_";
    private static readonly ITimeRegionProvider _systemProvider = (ITimeRegionProvider) new SystemTimeRegionProvider();
    private static readonly Type[] _tables = new Type[1]
    {
      typeof (DaylightShift)
    };
    private TimeZoneInfo.AdjustmentRule _rule;

    public TimeZoneInfo.AdjustmentRule Rule => this._rule;

    public void Prefetch(
      CustomTimeRegionProvider.DefinitionParameters parameter)
    {
      this._rule = this.Initialize(parameter);
    }

    private TimeZoneInfo.AdjustmentRule Initialize(
      CustomTimeRegionProvider.DefinitionParameters parameter)
    {
      DaylightShift daylightShift = (DaylightShift) null;
      string id = parameter.Id;
      int year = parameter.Year;
      using (new PXConnectionScope())
      {
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<DaylightShift>(new PXDataField[6]
        {
          new PXDataField(typeof (DaylightShift.fromDate).Name),
          new PXDataField(typeof (DaylightShift.toDate).Name),
          new PXDataField(typeof (DaylightShift.shift).Name),
          (PXDataField) new PXDataFieldValue(typeof (DaylightShift.isActive).Name, (PXDbType) 2, (object) 1),
          (PXDataField) new PXDataFieldValue(typeof (DaylightShift.year).Name, (PXDbType) 8, (object) year),
          (PXDataField) new PXDataFieldValue(typeof (DaylightShift.timeZone).Name, (PXDbType) 22, (object) id)
        }))
        {
          if (pxDataRecord != null)
          {
            daylightShift = new DaylightShift();
            daylightShift.IsActive = new bool?(true);
            daylightShift.Year = new int?(year);
            daylightShift.TimeZone = id;
            daylightShift.FromDate = pxDataRecord.GetDateTime(0);
            daylightShift.ToDate = pxDataRecord.GetDateTime(1);
            daylightShift.Shift = pxDataRecord.GetInt32(2);
          }
        }
      }
      if (daylightShift == null || string.IsNullOrEmpty(daylightShift.TimeZone))
        return CustomTimeRegionProvider.Definition._systemProvider.FindTimeRegionByTimeZone(id).With<ITimeRegion, TimeZoneInfo.AdjustmentRule>((Func<ITimeRegion, TimeZoneInfo.AdjustmentRule>) (_ => _.GetAdjustmentRule(year)));
      DateTime dateStart = new DateTime(year, 1, 1);
      DateTime dateTime = new DateTime(year, 12, 31 /*0x1F*/);
      TimeSpan timeSpan = TimeSpan.FromMinutes((double) daylightShift.Shift.GetValueOrDefault());
      DateTime? nullable = daylightShift.FromDate;
      TimeZoneInfo.TransitionTime transitionTime1 = CustomTimeRegionProvider.GetTransitionTime(nullable.Value);
      nullable = daylightShift.ToDate;
      TimeZoneInfo.TransitionTime transitionTime2 = CustomTimeRegionProvider.GetTransitionTime(nullable.Value);
      DateTime dateEnd = dateTime;
      TimeSpan daylightDelta = timeSpan;
      TimeZoneInfo.TransitionTime daylightTransitionStart = transitionTime1;
      TimeZoneInfo.TransitionTime daylightTransitionEnd = transitionTime2;
      return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd);
    }

    public static TimeZoneInfo.AdjustmentRule GetRule(int year, string id)
    {
      CustomTimeRegionProvider.DefinitionParameters definitionParameters = new CustomTimeRegionProvider.DefinitionParameters(id, year);
      string key = $"_CUSTOM_TIME_REGION_PROVIDER_SLOT_KEY_{year.ToString()}{id}";
      CustomTimeRegionProvider.Definition slot = PXContext.GetSlot<CustomTimeRegionProvider.Definition>(key);
      if (slot == null)
      {
        using (new PXConnectionScope())
        {
          slot = PXDatabase.GetSlot<CustomTimeRegionProvider.Definition, CustomTimeRegionProvider.DefinitionParameters>(key, definitionParameters, CustomTimeRegionProvider.Definition._tables);
          PXContext.SetSlot<CustomTimeRegionProvider.Definition>(key, slot);
        }
      }
      return slot?.Rule;
    }
  }
}

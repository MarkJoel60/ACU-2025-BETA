// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Aging.AgingEngine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common.Aging;

public static class AgingEngine
{
  /// <param name="shortFormat">
  /// If set to <c>true</c>, then no "Past Due" or "Outstanding" postfix will
  /// be appended to the bucket description, making <paramref name="agingDirection" />
  /// parameter irrelevant.
  /// </param>
  public static string GetDayAgingBucketDescription(
    int? lowerExclusiveBucketBoundary,
    int? upperInclusiveBucketBoundary,
    AgingDirection agingDirection,
    bool shortFormat)
  {
    if (!lowerExclusiveBucketBoundary.HasValue)
      return PXMessages.LocalizeNoPrefix(agingDirection == AgingDirection.Backwards ? "Current" : "Past Due");
    if (lowerExclusiveBucketBoundary.HasValue && !upperInclusiveBucketBoundary.HasValue)
      return PXMessages.LocalizeFormatNoPrefix(shortFormat ? "Over {0} Days" : (agingDirection == AgingDirection.Backwards ? "Over {0} Days Past Due" : "Over {0} Days Outstanding"), new object[1]
      {
        (object) lowerExclusiveBucketBoundary.Value
      });
    if (!lowerExclusiveBucketBoundary.HasValue || !upperInclusiveBucketBoundary.HasValue)
      return (string) null;
    return PXMessages.LocalizeFormatNoPrefix(shortFormat ? "{0} - {1} Days" : (agingDirection == AgingDirection.Backwards ? "{0} - {1} Days Past Due" : "{0} - {1} Days Outstanding"), new object[2]
    {
      (object) (lowerExclusiveBucketBoundary.Value + 1),
      (object) upperInclusiveBucketBoundary.Value
    });
  }

  /// <param name="shortFormat">
  /// If set to <c>true</c>, then no "Past Due" or "Outstanding" postfix will
  /// be appended to bucket descriptions, making <paramref name="agingDirection" />
  /// parameter irrelevant.
  /// </param>
  public static IEnumerable<string> GetDayAgingBucketDescriptions(
    AgingDirection agingDirection,
    IEnumerable<int> bucketBoundaries,
    bool shortFormat)
  {
    if (bucketBoundaries == null)
      throw new ArgumentNullException(nameof (bucketBoundaries));
    if (!bucketBoundaries.Any<int>())
    {
      yield return AgingEngine.GetDayAgingBucketDescription(new int?(), new int?(), AgingDirection.Backwards, shortFormat);
    }
    else
    {
      int? nullable = new int?();
      int? currentUpperBoundary = new int?();
      foreach (int bucketBoundary in bucketBoundaries)
      {
        int? lowerExclusiveBucketBoundary = currentUpperBoundary;
        currentUpperBoundary = new int?(bucketBoundary);
        yield return AgingEngine.GetDayAgingBucketDescription(lowerExclusiveBucketBoundary, currentUpperBoundary, agingDirection, shortFormat);
      }
      int? lowerExclusiveBucketBoundary1 = currentUpperBoundary;
      currentUpperBoundary = new int?();
      yield return AgingEngine.GetDayAgingBucketDescription(lowerExclusiveBucketBoundary1, currentUpperBoundary, agingDirection, shortFormat);
    }
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2019 R2.")]
  public static IEnumerable<string> GetPeriodAgingBucketDescriptions(
    IFinPeriodRepository finPeriodRepository,
    DateTime currentDate,
    AgingDirection agingDirection,
    int numberOfBuckets)
  {
    return AgingEngine.GetPeriodAgingBucketDescriptions(finPeriodRepository, currentDate, agingDirection, numberOfBuckets, 0, true);
  }

  public static IEnumerable<string> GetPeriodAgingBucketDescriptions(
    IFinPeriodRepository finPeriodRepository,
    DateTime currentDate,
    AgingDirection agingDirection,
    int numberOfBuckets,
    int calendarOrganizationID,
    bool usePeriodDescription)
  {
    if (finPeriodRepository == null)
      throw new ArgumentNullException(nameof (finPeriodRepository));
    if (numberOfBuckets <= 0)
      throw new ArgumentOutOfRangeException(nameof (numberOfBuckets));
    short periodStep = agingDirection == AgingDirection.Backwards ? (short) -1 : (short) 1;
    FinPeriod currentPeriod = finPeriodRepository.GetByID(finPeriodRepository.GetPeriodIDFromDate(new DateTime?(currentDate), new int?(calendarOrganizationID)), new int?(calendarOrganizationID));
    yield return usePeriodDescription ? currentPeriod.Descr : FinPeriodUtils.FormatForError(currentPeriod.FinPeriodID);
    for (--numberOfBuckets; numberOfBuckets > 1; --numberOfBuckets)
    {
      currentPeriod = finPeriodRepository.GetByID(finPeriodRepository.GetOffsetPeriodId(currentPeriod.FinPeriodID, (int) periodStep, new int?(calendarOrganizationID)), new int?(calendarOrganizationID));
      yield return usePeriodDescription ? currentPeriod.Descr : FinPeriodUtils.FormatForError(currentPeriod.FinPeriodID);
    }
    if (numberOfBuckets > 0)
      yield return PXMessages.LocalizeFormatNoPrefix(agingDirection == AgingDirection.Backwards ? "Before {0}" : "After {0}", new object[1]
      {
        usePeriodDescription ? (object) currentPeriod.Descr : (object) FinPeriodUtils.FormatForError(currentPeriod.FinPeriodID)
      });
  }

  /// <summary>
  /// Given the current date and the aging bucket boundaries (in maximum
  /// inclusive days from the current date), calculates the days difference
  /// between the current date and the test date, returning a zero-based number
  /// of aging bucket that the test date falls into.
  /// </summary>
  /// <param name="bucketBoundaries">
  /// Upper inclusive boundaries, in days, of the aging buckets.
  /// The first element of this sequence defines the upper inclusive
  /// boundary of the current bucket (it is usually zero).
  /// The total number of buckets would be equal to the number of elements
  /// in the sequence, plus one. The values in the sequence should
  /// be strictly non-decreasing.
  /// </param>
  /// <returns>
  /// The number of the aging bucket that the <paramref name="dateToAge" />
  /// falls into, in the [0; N] interval, where N is the the number of elements
  /// in <paramref name="bucketBoundaries" />. The value of N corresponds
  /// to the last aging bucket, which encompasses all dates that exceed
  /// the maximum bucket boundary.
  /// </returns>
  public static int AgeByDays(
    DateTime currentDate,
    DateTime dateToAge,
    AgingDirection agingDirection,
    IEnumerable<int> bucketBoundaries)
  {
    if (bucketBoundaries == null)
      throw new ArgumentNullException(nameof (bucketBoundaries));
    if (!bucketBoundaries.Any<int>())
      return 0;
    if (agingDirection == AgingDirection.Forward)
    {
      agingDirection = AgingDirection.Backwards;
      Utilities.Swap<DateTime>(ref currentDate, ref dateToAge);
    }
    int days = currentDate.Subtract(dateToAge).Days;
    int num = bucketBoundaries.FindIndex<int>((Predicate<int>) (boundary => boundary >= days));
    if (num < 0)
      num = -num - 1;
    return num;
  }

  public static int AgeByDays(
    DateTime currentAge,
    DateTime dateToAge,
    AgingDirection agingDirection,
    params int[] bucketBoundaries)
  {
    return AgingEngine.AgeByDays(currentAge, dateToAge, agingDirection, (IEnumerable<int>) bucketBoundaries);
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2019 R2.")]
  public static int AgeByPeriods(
    DateTime currentDate,
    DateTime dateToAge,
    IFinPeriodRepository finPeriodRepository,
    AgingDirection agingDirection,
    int numberOfBuckets)
  {
    return AgingEngine.AgeByPeriods(currentDate, dateToAge, finPeriodRepository, agingDirection, numberOfBuckets, 0);
  }

  /// <summary>
  /// Given the current date and the number of period-based aging buckets,
  /// returns the zero-based number of bucket that the specified test date
  /// falls into.
  /// </summary>
  /// <param name="numberOfBuckets">
  /// The total number of period-based buckets, including the "Current"
  /// and "Over" bucket. For backwards aging, the "Current" bucket encompasses
  /// dates in the same (or later) financial period as the current date, and
  /// the "Over" bucket corresponds to dates that are at least (numberOfBuckets - 1)
  /// periods back in time from the current date.
  /// </param>
  public static int AgeByPeriods(
    DateTime currentDate,
    DateTime dateToAge,
    IFinPeriodRepository finPeriodRepository,
    AgingDirection agingDirection,
    int numberOfBuckets,
    int organizationID)
  {
    if (finPeriodRepository == null)
      throw new ArgumentNullException(nameof (finPeriodRepository));
    if (numberOfBuckets <= 0)
      throw new ArgumentOutOfRangeException(nameof (numberOfBuckets));
    if (agingDirection == AgingDirection.Forward)
    {
      agingDirection = AgingDirection.Backwards;
      Utilities.Swap<DateTime>(ref currentDate, ref dateToAge);
    }
    if (dateToAge > currentDate)
      return 0;
    int num = finPeriodRepository.PeriodsBetweenInclusive(dateToAge, currentDate, new int?(organizationID)).Count<FinPeriod>() - 1;
    if (num < 0)
      throw new PXException("Financial Period cannot be found in the system.");
    if (num > numberOfBuckets - 1)
      num = numberOfBuckets - 1;
    return num;
  }
}

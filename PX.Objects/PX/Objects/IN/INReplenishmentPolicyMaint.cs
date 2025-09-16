// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INReplenishmentPolicyMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

public class INReplenishmentPolicyMaint : PXGraph<INReplenishmentPolicyMaint, INReplenishmentPolicy>
{
  public PXSelect<INReplenishmentPolicy> Policies;
  [PXImport(typeof (INReplenishmentPolicy))]
  public PXSelect<INReplenishmentSeason, Where<INReplenishmentSeason.replenishmentPolicyID, Equal<Optional<INReplenishmentPolicy.replenishmentPolicyID>>>, OrderBy<Asc<INReplenishmentSeason.startDate>>> Seasons;

  protected virtual void INReplenishmentSeason_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    INReplenishmentSeason row = (INReplenishmentSeason) e.Row;
    this.CheckSeasonOverlaps(sender, row);
  }

  protected virtual void INReplenishmentSeason_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 3)
      return;
    INReplenishmentSeason row = (INReplenishmentSeason) e.Row;
    if (this.CheckSeasonOverlaps(sender, row))
      throw new PXSetPropertyException("Periods overlap.");
  }

  protected virtual bool CheckSeasonOverlaps(PXCache cache, INReplenishmentSeason season)
  {
    foreach (PXResult<INReplenishmentSeason> pxResult in ((PXSelectBase<INReplenishmentSeason>) this.Seasons).Select(Array.Empty<object>()))
    {
      INReplenishmentSeason season1 = PXResult<INReplenishmentSeason>.op_Implicit(pxResult);
      bool flag = false;
      int? seasonId1 = season1.SeasonID;
      int? seasonId2 = season.SeasonID;
      if (!(seasonId1.GetValueOrDefault() == seasonId2.GetValueOrDefault() & seasonId1.HasValue == seasonId2.HasValue))
      {
        if (INReplenishmentPolicyMaint.IsDateInSeason(season.StartDate, season1))
        {
          flag = true;
          cache.RaiseExceptionHandling<INReplenishmentSeason.startDate>((object) season, (object) season.StartDate, (Exception) new PXSetPropertyException("Periods overlap."));
        }
        if (INReplenishmentPolicyMaint.IsDateInSeason(season.EndDate, season1))
        {
          flag = true;
          cache.RaiseExceptionHandling<INReplenishmentSeason.endDate>((object) season, (object) season.EndDate, (Exception) new PXSetPropertyException("Periods overlap."));
        }
        if (INReplenishmentPolicyMaint.IsDateInSeason(season1.StartDate, season))
        {
          flag = true;
          cache.RaiseExceptionHandling<INReplenishmentSeason.startDate>((object) season, (object) season.StartDate, (Exception) new PXSetPropertyException("Periods overlap."));
          cache.RaiseExceptionHandling<INReplenishmentSeason.endDate>((object) season, (object) season.EndDate, (Exception) new PXSetPropertyException("Periods overlap."));
        }
        if (flag)
          return true;
      }
    }
    return false;
  }

  public static bool IsDateInSeason(DateTime? date, INReplenishmentSeason season)
  {
    DateTime? startDate = season.StartDate;
    DateTime? nullable = date;
    if ((startDate.HasValue & nullable.HasValue ? (startDate.GetValueOrDefault() <= nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return false;
    nullable = date;
    DateTime? endDate = season.EndDate;
    return nullable.HasValue & endDate.HasValue && nullable.GetValueOrDefault() <= endDate.GetValueOrDefault();
  }

  protected virtual void INReplenishmentSeason_Factor_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if ((Decimal) e.NewValue == 0M)
      throw new PXSetPropertyException("The value must be greater than zero");
  }
}

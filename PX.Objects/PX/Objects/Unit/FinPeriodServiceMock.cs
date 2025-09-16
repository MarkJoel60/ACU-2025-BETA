// Decompiled with JetBrains decompiler
// Type: PX.Objects.Unit.FinPeriodServiceMock
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Unit;

public class FinPeriodServiceMock : IFinPeriodRepository
{
  public int? GetCalendarOrganizationID(
    int? organizationID,
    int? branchID,
    bool? useMasterCalendar)
  {
    return new int?(1);
  }

  public int? GetCalendarOrganizationID(int? branchID, bool? useMasterCalendar) => new int?(1);

  public FinPeriod FindMaxFinPeriodWithEndDataBelongToInterval(
    DateTime? startDate,
    DateTime? endDate,
    int? organizationID)
  {
    return new FinPeriod() { FinPeriodID = "201901" };
  }

  /// <summary>Returns PeriodID from the given date.</summary>
  public string GetPeriodIDFromDate(DateTime? date, int? organizationID) => "201901";

  public FinPeriod GetFinPeriodByDate(DateTime? date, int? organizationID)
  {
    return new FinPeriod() { FinPeriodID = "201901" };
  }

  public IEnumerable<MasterFinPeriod> GetPeriodsByFinYear(string finYear, bool clearQueryCache = false)
  {
    throw new NotImplementedException();
  }

  public FinPeriod FindFinPeriodByDate(DateTime? date, int? organizationID)
  {
    if (!organizationID.HasValue)
      return (FinPeriod) null;
    return new FinPeriod() { FinPeriodID = "201901" };
  }

  public string GetOffsetPeriodId(string finPeriodID, int offset, int? organizationID) => "201901";

  public FinPeriod GetOffsetPeriod(string finPeriodID, int offset, int? organizationID)
  {
    return new FinPeriod() { FinPeriodID = "201901" };
  }

  public string FindOffsetPeriodId(string finPeriodID, int offset, int? organizationID) => "201901";

  public FinPeriod FindOffsetPeriod(string finPeriodID, int offset, int? organizationID)
  {
    return new FinPeriod() { FinPeriodID = "201901" };
  }

  /// <summary>Returns Next Period from the given.</summary>
  public string NextPeriod(string finPeriodID, int? organizationID) => "201901";

  /// <summary>Returns Start date for the given Period</summary>
  public DateTime PeriodStartDate(string finPeriodID, int? organizationID)
  {
    return DateTime.Now.AddDays(-30.0);
  }

  /// <summary>Returns End date for the given period</summary>
  public DateTime PeriodEndDate(string finPeriodID, int? organizationID) => DateTime.Now;

  public IEnumerable<FinPeriod> GetFinPeriodsInInterval(
    DateTime? fromDate,
    DateTime? tillDate,
    int? organizationID)
  {
    yield return new FinPeriod() { FinPeriodID = "201901" };
  }

  public IEnumerable<FinPeriod> GetAdjustmentFinPeriods(string finYear, int? organizationID)
  {
    yield return new FinPeriod() { FinPeriodID = "201901" };
  }

  public FinPeriod FindLastYearNotAdjustmentPeriod(string finYear, int? organizationID)
  {
    return new FinPeriod() { FinPeriodID = "201901" };
  }

  public FinPeriod FindLastFinancialPeriodOfYear(string finYear, int? organizationID)
  {
    return new FinPeriod() { FinPeriodID = "201901" };
  }

  public OrganizationFinPeriod FindLastNonAdjustmentOrganizationFinPeriodOfYear(
    int? organizationID,
    string finYear,
    bool clearQueryCache = false)
  {
    return new OrganizationFinPeriod()
    {
      FinPeriodID = "201901"
    };
  }

  /// <summary>
  /// Returns a minimal set of financial periods that contain a given date interval
  /// within them, excluding any adjustment periods.
  /// </summary>
  /// <param name="Graph">The Graph which will be used when performing a select DB query.</param>
  /// <param name="startDate">The starting date of the date interval.</param>
  /// <param name="endDate">The ending date of the date interval.</param>
  public IEnumerable<FinPeriod> PeriodsBetweenInclusive(
    DateTime startDate,
    DateTime endDate,
    int? organizationID)
  {
    yield return new FinPeriod() { FinPeriodID = "201901" };
  }

  public void CheckIsDateWithinPeriod(
    string finPeriodID,
    int? organizationID,
    DateTime date,
    string errorMessage,
    PXErrorLevel errorLevel)
  {
  }

  public void CheckIsDateWithinPeriod(
    string finPeriodID,
    int? organizationID,
    DateTime date,
    string errorMessage)
  {
  }

  public bool IsDateWithinPeriod(string finPeriodID, DateTime date, int? organizationID) => true;

  public bool PeriodExists(string finPeriodID, int? organizationID) => true;

  /// <summary>
  /// Gets the ID of the financial period with the same <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod.PeriodNbr" />
  /// as the one specified, but residing in the previous financial year. If no such financial
  /// period exists, an exception is thrown.
  /// </summary>
  public string GetSamePeriodInPreviousYear(string finPeriodID, int? organizationID) => "201801";

  public FinPeriod GetByID(string finPeriodID, int? organizationID)
  {
    return new FinPeriod() { FinPeriodID = "201901" };
  }

  public FinPeriod FindByID(int? organizationID, string finPeriodID)
  {
    return new FinPeriod() { FinPeriodID = "201901" };
  }

  public FinPeriod FindPrevPeriod(int? organizationID, string finPeriodID, bool looped = false)
  {
    return new FinPeriod() { FinPeriodID = "201901" };
  }

  public FinPeriod FindNextPeriod(int? organizationID, string finPeriodID, bool looped = false)
  {
    return new FinPeriod() { FinPeriodID = "201901" };
  }

  public FinPeriod FindFirstPeriod(int? organizationID, bool clearQueryCache = false)
  {
    return new FinPeriod() { FinPeriodID = "201901" };
  }

  public FinPeriod FindLastPeriod(int? organizationID, bool clearQueryCache = false)
  {
    return new FinPeriod() { FinPeriodID = "201901" };
  }

  public PX.Objects.GL.FinPeriods.TableDefinition.FinYear FindFirstYear(
    int? organizationID,
    bool clearQueryCache = false)
  {
    return new PX.Objects.GL.FinPeriods.TableDefinition.FinYear();
  }

  public PX.Objects.GL.FinPeriods.TableDefinition.FinYear FindLastYear(
    int? organizationID,
    bool clearQueryCache = false)
  {
    return new PX.Objects.GL.FinPeriods.TableDefinition.FinYear();
  }

  public OrganizationFinPeriod FindFirstOpenFinPeriod(
    string fromFinPeriodID,
    int? organizationID,
    Type fieldModuleClosed = null)
  {
    return new OrganizationFinPeriod()
    {
      FinPeriodID = "201901"
    };
  }

  public FinPeriod FindNextOpenPeriod(string fromFinPeriodID, int? organizationID)
  {
    return new FinPeriod() { FinPeriodID = "201901" };
  }

  public MasterFinYear FindMasterFinYearByID(string year, bool clearQueryCache = false)
  {
    return new MasterFinYear();
  }

  public MasterFinPeriod FindMasterFinPeriodByID(string finPeriodID, bool clearQueryCache = false)
  {
    return new MasterFinPeriod() { FinPeriodID = "201901" };
  }

  public OrganizationFinYear FindOrganizationFinYearByID(
    int? organizationID,
    string year,
    bool clearQueryCache = false)
  {
    return new OrganizationFinYear();
  }

  public OrganizationFinPeriod FindOrganizationFinPeriodByID(
    int? organizationID,
    string finPeriodID,
    bool clearQueryCache = false)
  {
    return new OrganizationFinPeriod();
  }

  public MasterFinPeriod FindNextNonAdjustmentMasterFinPeriod(
    string prevFinPeriodID,
    bool clearQueryCache = false)
  {
    return new MasterFinPeriod() { FinPeriodID = "201901" };
  }

  public FinPeriod GetMappedPeriod(int? organizationID1, string finPeriodID1, int? organizationID2)
  {
    return new FinPeriod() { FinPeriodID = "201901" };
  }

  public virtual ProcessingResult<FinPeriod> GetFinPeriodByMasterPeriodID(
    int? organizationID,
    string masterFinPeriodID)
  {
    return new ProcessingResult<FinPeriod>();
  }

  public ProcessingResult FinPeriodsForMasterExist(string masterFinPeriodID, int?[] organizationIDs)
  {
    return new ProcessingResult();
  }

  public OrganizationFinYear FindNearestOrganizationFinYear(
    int? organizationID,
    string yearNumber,
    bool clearQueryCache = false,
    bool mergeCache = false)
  {
    return new OrganizationFinYear();
  }

  public string FindFinPeriodIDByMasterPeriodID(
    int? organizationID,
    string masterFinPeriodID,
    bool readAllAndCacheToPXContext = false)
  {
    return "201901";
  }

  public string GetFinPeriodByBranchAndMasterPeriodID(int? branchId, string masterFinPeriod)
  {
    return "201901";
  }
}

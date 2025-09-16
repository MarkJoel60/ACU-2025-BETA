// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriods.FinPeriodRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.GL.Exceptions;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.FinPeriods;

public class FinPeriodRepository : IFinPeriodRepository
{
  protected readonly PXGraph Graph;

  public static FinPeriodRepository.FinPeriodKeysByMasterAndOrganizationIDCachedCollection FinPeriodKeysByMasterAndOrganizationID
  {
    get
    {
      FinPeriodRepository.FinPeriodKeysByMasterAndOrganizationIDCachedCollection andOrganizationId = PXContext.GetSlot<FinPeriodRepository.FinPeriodKeysByMasterAndOrganizationIDCachedCollection>();
      if (andOrganizationId == null)
      {
        andOrganizationId = new FinPeriodRepository.FinPeriodKeysByMasterAndOrganizationIDCachedCollection();
        andOrganizationId.Load();
        PXContext.SetSlot<FinPeriodRepository.FinPeriodKeysByMasterAndOrganizationIDCachedCollection>(andOrganizationId);
      }
      return andOrganizationId;
    }
  }

  public FinPeriodRepository(PXGraph graph) => this.Graph = graph;

  public int? GetCalendarOrganizationID(
    int? organizationID,
    int? branchID,
    bool? useMasterCalendar)
  {
    if (useMasterCalendar.GetValueOrDefault() || !organizationID.HasValue && !branchID.HasValue)
      return new int?(0);
    return branchID.HasValue ? PXAccess.GetParentOrganizationID(branchID) : organizationID;
  }

  public int? GetCalendarOrganizationID(int? branchID, bool? useMasterCalendar)
  {
    return useMasterCalendar.GetValueOrDefault() || !branchID.HasValue ? new int?(0) : PXAccess.GetParentOrganizationID(branchID);
  }

  public FinPeriod FindMaxFinPeriodWithEndDataBelongToInterval(
    DateTime? startDate,
    DateTime? endDate,
    int? organizationID)
  {
    return PXResultset<FinPeriod>.op_Implicit(PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.finDate, GreaterEqual<Required<FinPeriod.finDate>>, And<FinPeriod.finDate, Less<Required<FinPeriod.finDate>>, And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>, OrderBy<Desc<FinPeriod.finPeriodID>>>.Config>.Select(this.Graph, new object[3]
    {
      (object) startDate,
      (object) endDate,
      (object) organizationID
    }));
  }

  /// <summary>Returns PeriodID from the given date.</summary>
  public string GetPeriodIDFromDate(DateTime? date, int? organizationID)
  {
    return this.GetFinPeriodByDate(date, organizationID).FinPeriodID;
  }

  public FinPeriod GetFinPeriodByDate(DateTime? date, int? organizationID)
  {
    return this.FindFinPeriodByDate(date, organizationID) ?? throw new FinancialPeriodNotDefinedForDateException(date);
  }

  private CalendarByYear GetCalendarCache(int organizationID)
  {
    CalendarByYearPrefetchParameters prefetchParameters = new CalendarByYearPrefetchParameters()
    {
      OrgID = organizationID,
      Graph = this.Graph
    };
    CalendarByYear slot = PXDatabase.GetSlot<CalendarByYear, CalendarByYearPrefetchParameters>("CalendarByYear" + organizationID.ToString(), prefetchParameters, new Type[1]
    {
      typeof (FinPeriod)
    });
    prefetchParameters.Graph = (PXGraph) null;
    return slot;
  }

  public FinPeriod FindFinPeriodByDate(DateTime? date, int? organizationID)
  {
    return !organizationID.HasValue || !date.HasValue ? (FinPeriod) null : this.GetCalendarCache(organizationID.Value).FetchFinPeriod(date.Value);
  }

  public string GetOffsetPeriodId(string finPeriodID, int offset, int? organizationID)
  {
    return this.FindOffsetPeriodId(finPeriodID, offset, organizationID) ?? throw new FinancialPeriodOffsetNotFoundException(finPeriodID, offset);
  }

  public FinPeriod GetOffsetPeriod(string finPeriodID, int offset, int? organizationID)
  {
    return this.FindOffsetPeriod(finPeriodID, offset, organizationID) ?? throw new FinancialPeriodOffsetNotFoundException(finPeriodID, offset);
  }

  public string FindOffsetPeriodId(string finPeriodID, int offset, int? organizationID)
  {
    FinYearSetup finYearSetup = PXResultset<FinYearSetup>.op_Implicit(PXSelectBase<FinYearSetup, PXSelect<FinYearSetup>.Config>.Select(this.Graph, Array.Empty<object>()));
    FinPeriodSetup finPeriodSetup = PXResultset<FinPeriodSetup>.op_Implicit(PXSelectBase<FinPeriodSetup, PXSelectGroupBy<FinPeriodSetup, Where<FinPeriodSetup.endDate, Greater<FinPeriodSetup.startDate>>, Aggregate<Max<FinPeriodSetup.periodNbr>>>.Config>.Select(this.Graph, Array.Empty<object>()));
    if (finYearSetup != null && FiscalPeriodSetupCreator.IsFixedLengthPeriod(finYearSetup.FPType) && finPeriodSetup != null && finPeriodSetup.PeriodNbr != null)
      return FinPeriodUtils.OffsetPeriod(finPeriodID, offset, Convert.ToInt32(finPeriodSetup.PeriodNbr));
    if (offset > 0)
    {
      PXResultset<FinPeriod> pxResultset = PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.finPeriodID, Greater<Required<FinPeriod.finPeriodID>>, And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>, And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>, OrderBy<Asc<FinPeriod.finPeriodID>>>.Config>.SelectWindowed(this.Graph, 0, offset, new object[2]
      {
        (object) finPeriodID,
        (object) organizationID
      });
      return pxResultset.Count < offset ? (string) null : PXResult<FinPeriod>.op_Implicit(pxResultset[pxResultset.Count - 1]).FinPeriodID;
    }
    if (offset >= 0)
      return finPeriodID;
    PXResultset<FinPeriod> pxResultset1 = PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.finPeriodID, Less<Required<FinPeriod.finPeriodID>>, And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>, And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>, OrderBy<Desc<FinPeriod.finPeriodID>>>.Config>.SelectWindowed(this.Graph, 0, -offset, new object[2]
    {
      (object) finPeriodID,
      (object) organizationID
    });
    return pxResultset1.Count < -offset ? (string) null : PXResult<FinPeriod>.op_Implicit(pxResultset1[pxResultset1.Count - 1]).FinPeriodID;
  }

  public FinPeriod FindOffsetPeriod(string finPeriodID, int offset, int? organizationID)
  {
    FinYearSetup finYearSetup = PXResultset<FinYearSetup>.op_Implicit(PXSelectBase<FinYearSetup, PXSelect<FinYearSetup>.Config>.Select(this.Graph, Array.Empty<object>()));
    FinPeriodSetup finPeriodSetup = PXResultset<FinPeriodSetup>.op_Implicit(PXSelectBase<FinPeriodSetup, PXSelectGroupBy<FinPeriodSetup, Where<FinPeriodSetup.endDate, Greater<FinPeriodSetup.startDate>>, Aggregate<Max<FinPeriodSetup.periodNbr>>>.Config>.Select(this.Graph, Array.Empty<object>()));
    if (finYearSetup != null && FiscalPeriodSetupCreator.IsFixedLengthPeriod(finYearSetup.FPType) && finPeriodSetup != null && finPeriodSetup.PeriodNbr != null)
    {
      string finPeriodID1 = FinPeriodUtils.OffsetPeriod(finPeriodID, offset, Convert.ToInt32(finPeriodSetup.PeriodNbr));
      return this.FindByID(organizationID, finPeriodID1);
    }
    if (offset > 0)
    {
      PXResultset<FinPeriod> pxResultset = PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.finPeriodID, Greater<Required<FinPeriod.finPeriodID>>, And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>, And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>, OrderBy<Asc<FinPeriod.finPeriodID>>>.Config>.SelectWindowed(this.Graph, 0, offset, new object[2]
      {
        (object) finPeriodID,
        (object) organizationID
      });
      return pxResultset.Count < offset ? (FinPeriod) null : PXResult<FinPeriod>.op_Implicit(pxResultset[pxResultset.Count - 1]);
    }
    if (offset >= 0)
      return this.FindByID(organizationID, finPeriodID);
    PXResultset<FinPeriod> pxResultset1 = PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.finPeriodID, Less<Required<FinPeriod.finPeriodID>>, And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>, And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>, OrderBy<Desc<FinPeriod.finPeriodID>>>.Config>.SelectWindowed(this.Graph, 0, -offset, new object[2]
    {
      (object) finPeriodID,
      (object) organizationID
    });
    return pxResultset1.Count < -offset ? (FinPeriod) null : PXResult<FinPeriod>.op_Implicit(pxResultset1[pxResultset1.Count - 1]);
  }

  /// <summary>Returns Next Period from the given.</summary>
  public string NextPeriod(string finPeriodID, int? organizationID)
  {
    return this.GetOffsetPeriodId(finPeriodID, 1, organizationID);
  }

  /// <summary>Returns Start date for the given Period</summary>
  public DateTime PeriodStartDate(string finPeriodID, int? organizationID)
  {
    return (PXResultset<FinPeriod>.op_Implicit(PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.finPeriodID, Equal<Required<FinPeriod.finPeriodID>>, And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>.Config>.Select(this.Graph, new object[2]
    {
      (object) finPeriodID,
      (object) organizationID
    })) ?? throw new FinancialPeriodWithIdNotFoundException(finPeriodID)).StartDate.Value;
  }

  /// <summary>Returns End date for the given period</summary>
  public DateTime PeriodEndDate(string finPeriodID, int? organizationID)
  {
    return (PXResultset<FinPeriod>.op_Implicit(PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.finPeriodID, Equal<Required<FinPeriod.finPeriodID>>, And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>.Config>.Select(this.Graph, new object[2]
    {
      (object) finPeriodID,
      (object) organizationID
    })) ?? throw new FinancialPeriodWithIdNotFoundException(finPeriodID)).EndDate.Value.AddDays(-1.0);
  }

  public IEnumerable<FinPeriod> GetFinPeriodsInInterval(
    DateTime? fromDate,
    DateTime? tillDate,
    int? organizationID)
  {
    return GraphHelper.RowCast<FinPeriod>((IEnumerable) PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.startDate, GreaterEqual<Required<FinPeriod.startDate>>, And<FinPeriod.endDate, LessEqual<Required<FinPeriod.endDate>>, And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>, And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>>>.Config>.Select(this.Graph, new object[3]
    {
      (object) fromDate,
      (object) tillDate,
      (object) organizationID
    }));
  }

  public IEnumerable<FinPeriod> GetAdjustmentFinPeriods(string finYear, int? organizationID)
  {
    return GraphHelper.RowCast<FinPeriod>((IEnumerable) PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.finYear, Equal<Required<FinPeriod.finYear>>, And<FinPeriod.startDate, Equal<FinPeriod.endDate>, And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>>.Config>.Select(this.Graph, new object[2]
    {
      (object) finYear,
      (object) organizationID
    }));
  }

  public FinPeriod FindLastYearNotAdjustmentPeriod(string finYear, int? organizationID)
  {
    return PXResultset<FinPeriod>.op_Implicit(PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.finYear, Equal<Required<FinPeriod.finYear>>, And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>, And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>, OrderBy<Desc<FinPeriod.finPeriodID>>>.Config>.SelectWindowed(this.Graph, 0, 1, new object[2]
    {
      (object) finYear,
      (object) organizationID
    }));
  }

  public FinPeriod FindLastFinancialPeriodOfYear(string finYear, int? organizationID)
  {
    return PXResultset<FinPeriod>.op_Implicit(PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.finYear, Equal<Required<FinPeriod.finYear>>, And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>, OrderBy<Desc<FinPeriod.finPeriodID>>>.Config>.SelectWindowed(this.Graph, 0, 1, new object[2]
    {
      (object) finYear,
      (object) organizationID
    }));
  }

  public OrganizationFinPeriod FindLastNonAdjustmentOrganizationFinPeriodOfYear(
    int? organizationID,
    string finYear,
    bool clearQueryCache = false)
  {
    PXSelectBase<OrganizationFinPeriod> pxSelectBase = (PXSelectBase<OrganizationFinPeriod>) new PXSelectReadonly<OrganizationFinPeriod, Where<OrganizationFinPeriod.finYear, Equal<Required<OrganizationFinPeriod.finYear>>, And<OrganizationFinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>, And<OrganizationFinPeriod.startDate, NotEqual<OrganizationFinPeriod.endDate>>>>, OrderBy<Desc<OrganizationFinPeriod.finPeriodID>>>(this.Graph);
    if (clearQueryCache)
      ((PXSelectBase) pxSelectBase).View.Clear();
    return ((PXSelectBase) pxSelectBase).View.SelectSingle(new object[2]
    {
      (object) finYear,
      (object) organizationID
    }) as OrganizationFinPeriod;
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
    if (startDate > endDate)
      throw new PXArgumentException(nameof (startDate));
    return GraphHelper.RowCast<FinPeriod>((IEnumerable) PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.endDate, Greater<Required<FinPeriod.endDate>>, And<FinPeriod.startDate, LessEqual<Required<FinPeriod.startDate>>, And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>, And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>>>.Config>.Select(this.Graph, new object[3]
    {
      (object) startDate,
      (object) endDate,
      (object) organizationID
    }));
  }

  public void CheckIsDateWithinPeriod(
    string finPeriodID,
    int? organizationID,
    DateTime date,
    string errorMessage,
    PXErrorLevel errorLevel)
  {
    if (!this.IsDateWithinPeriod(finPeriodID, date, organizationID))
      throw new PXSetPropertyException(errorMessage, errorLevel);
  }

  public void CheckIsDateWithinPeriod(
    string finPeriodID,
    int? organizationID,
    DateTime date,
    string errorMessage)
  {
    this.CheckIsDateWithinPeriod(finPeriodID, organizationID, date, errorMessage, (PXErrorLevel) 4);
  }

  public bool IsDateWithinPeriod(string finPeriodID, DateTime date, int? organizationID)
  {
    FinPeriod byId = this.GetByID(finPeriodID, organizationID);
    DateTime dateTime1 = date;
    DateTime? startDate = byId.StartDate;
    if ((startDate.HasValue ? (dateTime1 >= startDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return false;
    DateTime dateTime2 = date;
    DateTime? endDate = byId.EndDate;
    return endDate.HasValue && dateTime2 < endDate.GetValueOrDefault();
  }

  public bool PeriodExists(string finPeriodID, int? organizationID)
  {
    return this.FindByID(organizationID, finPeriodID) != null;
  }

  /// <summary>
  /// Gets the ID of the financial period with the same <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod.PeriodNbr" />
  /// as the one specified, but residing in the previous financial year. If no such financial
  /// period exists, an exception is thrown.
  /// </summary>
  public string GetSamePeriodInPreviousYear(string finPeriodID, int? organizationID)
  {
    string s = finPeriodID.Substring(0, 4);
    string str1 = finPeriodID.Substring(4, 2);
    string str2 = (int.Parse(s) - 1).ToString() + str1;
    return this.PeriodExists(str2, organizationID) ? str2 : throw new FinancialPeriodWithIdNotFoundException(str2);
  }

  public FinPeriod GetByID(string finPeriodID, int? organizationID)
  {
    return this.FindByID(organizationID, finPeriodID) ?? throw new PXException("{0} with ID '{1}' does not exist", new object[2]
    {
      (object) EntityHelper.GetFriendlyEntityName(typeof (FinPeriod)),
      (object) finPeriodID
    });
  }

  public FinPeriod FindByID(int? organizationID, string finPeriodID)
  {
    return !organizationID.HasValue ? (FinPeriod) null : this.GetCalendarCache(organizationID.Value).FetchFinPeriod(finPeriodID);
  }

  public FinPeriod FindPrevPeriod(int? organizationID, string finPeriodID, bool looped = false)
  {
    FinPeriod prevPeriod = (FinPeriod) null;
    if (!string.IsNullOrEmpty(finPeriodID))
      prevPeriod = PXResultset<FinPeriod>.op_Implicit(PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.finPeriodID, Less<Required<FinPeriod.finPeriodID>>, And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>, OrderBy<Desc<FinPeriod.finPeriodID>>>.Config>.SelectWindowed(this.Graph, 0, 1, new object[2]
      {
        (object) finPeriodID,
        (object) organizationID
      }));
    if (looped && prevPeriod == null)
      prevPeriod = this.FindFirstPeriod(organizationID, false);
    return prevPeriod;
  }

  public FinPeriod FindNextPeriod(int? organizationID, string finPeriodID, bool looped = false)
  {
    FinPeriod nextPeriod = (FinPeriod) null;
    if (!string.IsNullOrEmpty(finPeriodID))
      nextPeriod = PXResultset<FinPeriod>.op_Implicit(PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.finPeriodID, Greater<Required<FinPeriod.finPeriodID>>, And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>, OrderBy<Asc<FinPeriod.finPeriodID>>>.Config>.SelectWindowed(this.Graph, 0, 1, new object[2]
      {
        (object) finPeriodID,
        (object) organizationID
      }));
    if (looped && nextPeriod == null)
      nextPeriod = this.FindLastPeriod(organizationID, false);
    return nextPeriod;
  }

  public FinPeriod FindFirstPeriod(int? organizationID, bool clearQueryCache = false)
  {
    PXSelectBase<FinPeriod> pxSelectBase = (PXSelectBase<FinPeriod>) new PXSelectReadonly<FinPeriod, Where<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>, OrderBy<Asc<FinPeriod.finPeriodID>>>(this.Graph);
    if (clearQueryCache)
      ((PXSelectBase) pxSelectBase).View.Clear();
    return ((PXSelectBase) pxSelectBase).View.SelectSingle(new object[1]
    {
      (object) organizationID
    }) as FinPeriod;
  }

  public FinPeriod FindLastPeriod(int? organizationID, bool clearQueryCache = false)
  {
    PXSelectBase<FinPeriod> pxSelectBase = (PXSelectBase<FinPeriod>) new PXSelectReadonly<FinPeriod, Where<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>, OrderBy<Desc<FinPeriod.finPeriodID>>>(this.Graph);
    if (clearQueryCache)
      ((PXSelectBase) pxSelectBase).View.Clear();
    return ((PXSelectBase) pxSelectBase).View.SelectSingle(new object[1]
    {
      (object) organizationID
    }) as FinPeriod;
  }

  public PX.Objects.GL.FinPeriods.TableDefinition.FinYear FindFirstYear(
    int? organizationID,
    bool clearQueryCache = false)
  {
    PXSelectBase<PX.Objects.GL.FinPeriods.TableDefinition.FinYear> pxSelectBase = (PXSelectBase<PX.Objects.GL.FinPeriods.TableDefinition.FinYear>) new PXSelectReadonly<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<Required<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID>>>, OrderBy<Asc<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>>>(this.Graph);
    if (clearQueryCache)
      ((PXSelectBase) pxSelectBase).View.Clear();
    return ((PXSelectBase) pxSelectBase).View.SelectSingle(new object[1]
    {
      (object) organizationID
    }) as PX.Objects.GL.FinPeriods.TableDefinition.FinYear;
  }

  public PX.Objects.GL.FinPeriods.TableDefinition.FinYear FindLastYear(
    int? organizationID,
    bool clearQueryCache = false)
  {
    PXSelectBase<PX.Objects.GL.FinPeriods.TableDefinition.FinYear> pxSelectBase = (PXSelectBase<PX.Objects.GL.FinPeriods.TableDefinition.FinYear>) new PXSelectReadonly<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<Required<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID>>>, OrderBy<Desc<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>>>(this.Graph);
    if (clearQueryCache)
      ((PXSelectBase) pxSelectBase).View.Clear();
    return ((PXSelectBase) pxSelectBase).View.SelectSingle(new object[1]
    {
      (object) organizationID
    }) as PX.Objects.GL.FinPeriods.TableDefinition.FinYear;
  }

  public OrganizationFinPeriod FindFirstOpenFinPeriod(
    string fromFinPeriodID,
    int? organizationID,
    Type fieldModuleClosed = null)
  {
    bool flag = PXContext.GetSlot<bool>("FinPeriodUtils.AllowPostToUnlockedPeriod") || !PXResultset<GLSetup>.op_Implicit(PXSelectBase<GLSetup, PXSelect<GLSetup>.Config>.Select(this.Graph, Array.Empty<object>())).RestrictAccessToClosedPeriods.GetValueOrDefault() || !string.IsNullOrEmpty(PredefinedRoles.FinancialSupervisor) && PXContext.PXIdentity.User.IsInRole(PredefinedRoles.FinancialSupervisor);
    BqlCommand bqlCommand = BqlCommand.CreateInstance(new Type[1]
    {
      typeof (Select<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, GreaterEqual<Required<OrganizationFinPeriod.finPeriodID>>, And<OrganizationFinPeriod.organizationID, Equal<Required<OrganizationFinPeriod.organizationID>>>>, OrderBy<Asc<OrganizationFinPeriod.finPeriodID>>>)
    });
    if (fieldModuleClosed != (Type) null)
      bqlCommand = bqlCommand.WhereAnd(BqlCommand.Compose(new Type[3]
      {
        typeof (Where<,>),
        fieldModuleClosed,
        typeof (NotEqual<True>)
      }));
    return (OrganizationFinPeriod) new PXView(this.Graph, false, !flag ? bqlCommand.WhereAnd(typeof (Where<OrganizationFinPeriod.status, Equal<FinPeriod.status.open>>)) : bqlCommand.WhereAnd(typeof (Where<OrganizationFinPeriod.status, Equal<FinPeriod.status.open>, Or<OrganizationFinPeriod.status, Equal<FinPeriod.status.closed>>>))).SelectSingle(new object[2]
    {
      (object) fromFinPeriodID,
      (object) organizationID
    });
  }

  public FinPeriod FindNextOpenPeriod(string finPeriodID, int? organizationID)
  {
    FinPeriod nextOpenPeriod = (FinPeriod) null;
    if (!string.IsNullOrEmpty(finPeriodID))
      nextOpenPeriod = PXResultset<FinPeriod>.op_Implicit(PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.startDate, NotEqual<FinPeriod.endDate>, And<FinPeriod.status, Equal<FinPeriod.status.open>, And<FinPeriod.finPeriodID, Greater<Required<FinPeriod.finPeriodID>>, And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>>, OrderBy<Asc<FinPeriod.finPeriodID>>>.Config>.SelectWindowed(this.Graph, 0, 1, new object[2]
      {
        (object) finPeriodID,
        (object) organizationID
      }));
    return nextOpenPeriod;
  }

  public MasterFinYear FindMasterFinYearByID(string year, bool clearQueryCache = false)
  {
    PXSelectBase<MasterFinYear> pxSelectBase = (PXSelectBase<MasterFinYear>) new PXSelectReadonly<MasterFinYear, Where<MasterFinYear.year, Equal<Required<MasterFinYear.year>>>>(this.Graph);
    if (clearQueryCache)
      ((PXSelectBase) pxSelectBase).View.Clear();
    return ((PXSelectBase) pxSelectBase).View.SelectSingle(new object[1]
    {
      (object) year
    }) as MasterFinYear;
  }

  public MasterFinPeriod FindMasterFinPeriodByID(string finPeriodID, bool clearQueryCache = false)
  {
    PXSelectBase<MasterFinPeriod> pxSelectBase = (PXSelectBase<MasterFinPeriod>) new PXSelectReadonly<MasterFinPeriod, Where<MasterFinPeriod.finPeriodID, Equal<Required<MasterFinPeriod.finPeriodID>>>>(this.Graph);
    if (clearQueryCache)
      ((PXSelectBase) pxSelectBase).View.Clear();
    return ((PXSelectBase) pxSelectBase).View.SelectSingle(new object[1]
    {
      (object) finPeriodID
    }) as MasterFinPeriod;
  }

  public OrganizationFinYear FindOrganizationFinYearByID(
    int? organizationID,
    string year,
    bool clearQueryCache = false)
  {
    PXSelectBase<OrganizationFinYear> pxSelectBase = (PXSelectBase<OrganizationFinYear>) new PXSelectReadonly<OrganizationFinYear, Where<OrganizationFinYear.year, Equal<Required<OrganizationFinYear.year>>, And<OrganizationFinYear.organizationID, Equal<Required<OrganizationFinYear.organizationID>>>>>(this.Graph);
    if (clearQueryCache)
      ((PXSelectBase) pxSelectBase).View.Clear();
    return ((PXSelectBase) pxSelectBase).View.SelectSingle(new object[2]
    {
      (object) year,
      (object) organizationID
    }) as OrganizationFinYear;
  }

  public OrganizationFinPeriod FindOrganizationFinPeriodByID(
    int? organizationID,
    string finPeriodID,
    bool clearQueryCache = false)
  {
    PXSelectBase<OrganizationFinPeriod> pxSelectBase = (PXSelectBase<OrganizationFinPeriod>) new PXSelectReadonly<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, Equal<Required<OrganizationFinPeriod.finPeriodID>>, And<OrganizationFinPeriod.organizationID, Equal<Required<OrganizationFinPeriod.organizationID>>>>>(this.Graph);
    if (clearQueryCache)
      ((PXSelectBase) pxSelectBase).View.Clear();
    return ((PXSelectBase) pxSelectBase).View.SelectSingle(new object[2]
    {
      (object) finPeriodID,
      (object) organizationID
    }) as OrganizationFinPeriod;
  }

  public MasterFinPeriod FindNextNonAdjustmentMasterFinPeriod(
    string prevFinPeriodID,
    bool clearQueryCache = false)
  {
    PXSelectBase<MasterFinPeriod> pxSelectBase = (PXSelectBase<MasterFinPeriod>) new PXSelectReadonly<MasterFinPeriod, Where<MasterFinPeriod.finPeriodID, Greater<Required<MasterFinPeriod.finPeriodID>>, And<MasterFinPeriod.startDate, NotEqual<MasterFinPeriod.endDate>>>, OrderBy<Asc<MasterFinPeriod.finPeriodID>>>(this.Graph);
    if (clearQueryCache)
      ((PXSelectBase) pxSelectBase).View.Clear();
    return ((PXSelectBase) pxSelectBase).View.SelectSingle(new object[1]
    {
      (object) prevFinPeriodID
    }) as MasterFinPeriod;
  }

  public FinPeriod GetMappedPeriod(int? organizationID1, string finPeriodID1, int? organizationID2)
  {
    FinPeriod byId = this.FindByID(organizationID1, finPeriodID1);
    return this.GetFinPeriodByMasterPeriodID(organizationID2, byId?.MasterFinPeriodID).Result;
  }

  public virtual ProcessingResult<FinPeriod> GetFinPeriodByMasterPeriodID(
    int? organizationID,
    string masterFinPeriodID)
  {
    FinPeriod finPeriod = !organizationID.HasValue ? (FinPeriod) null : this.GetCalendarCache(organizationID.Value).FetchFinPeriodByMasterID(masterFinPeriodID);
    ProcessingResult<FinPeriod> success = ProcessingResultBase<ProcessingResult<FinPeriod>, FinPeriod, ProcessingResultMessage>.CreateSuccess(finPeriod);
    if (finPeriod == null)
    {
      string message = PXMessages.LocalizeFormatNoPrefix("The related financial periods for the {0} master period do not exist in the {1} company.", new object[2]
      {
        (object) PeriodIDAttribute.FormatForError(masterFinPeriodID),
        (object) PXAccess.GetOrganizationCD(organizationID)
      });
      success.AddErrorMessage(message);
    }
    return success;
  }

  public virtual string FindFinPeriodIDByMasterPeriodID(
    int? organizationID,
    string masterFinPeriodID,
    bool readAllAndCacheToPXContext = false)
  {
    if (readAllAndCacheToPXContext)
      return FinPeriodRepository.FinPeriodKeysByMasterAndOrganizationID.GetPeriodByMaster(organizationID, masterFinPeriodID);
    ProcessingResult<FinPeriod> byMasterPeriodId = this.GetFinPeriodByMasterPeriodID(organizationID, masterFinPeriodID);
    return byMasterPeriodId.IsSuccess ? byMasterPeriodId.Result.FinPeriodID : (string) null;
  }

  public virtual string GetFinPeriodByBranchAndMasterPeriodID(int? branchId, string masterFinPeriod)
  {
    if (!branchId.HasValue)
      throw new ArgumentNullException("branchId cannot be null");
    ProcessingResult<FinPeriod> processingResult = masterFinPeriod != null ? this.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(branchId), masterFinPeriod) : throw new ArgumentNullException("masterFinPeriod cannot be null");
    if (processingResult.IsSuccess)
      return processingResult.Result.FinPeriodID;
    throw new PXException(processingResult.GeneralMessage);
  }

  public ProcessingResult FinPeriodsForMasterExist(string masterFinPeriodID, int?[] organizationIDs)
  {
    List<FinPeriod> list = GraphHelper.RowCast<FinPeriod>((IEnumerable) PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.masterFinPeriodID, Equal<Required<FinPeriod.masterFinPeriodID>>, And<FinPeriod.organizationID, In<Required<FinPeriod.organizationID>>>>>.Config>.Select(this.Graph, new object[2]
    {
      (object) masterFinPeriodID,
      (object) organizationIDs
    })).ToList<FinPeriod>();
    ProcessingResult processingResult = new ProcessingResult();
    if (list.Count != organizationIDs.Length)
    {
      IEnumerable<int?> source = ((IEnumerable<int?>) organizationIDs).Except<int?>(list.Select<FinPeriod, int?>((Func<FinPeriod, int?>) (period => period.OrganizationID)));
      processingResult.AddMessage((PXErrorLevel) 4, "The related financial periods for the {0} master period do not exist in the {1} companies.", (object) FinPeriodIDFormattingAttribute.FormatForError(masterFinPeriodID), (object) ((ICollection<string>) source.Select<int?, string>(new Func<int?, string>(PXAccess.GetOrganizationCD)).OrderBy<string, string>((Func<string, string>) (v => v)).ToArray<string>()).JoinIntoStringForMessageNoQuotes<string>(20));
    }
    return processingResult;
  }

  public OrganizationFinYear FindNearestOrganizationFinYear(
    int? organizationID,
    string yearNumber,
    bool clearQueryCache = false,
    bool mergeCache = false)
  {
    if (((BqlCommand) new Select<OrganizationFinYear, Where<OrganizationFinYear.organizationID, Equal<Required<OrganizationFinYear.organizationID>>, And<OrganizationFinYear.year, Equal<Required<OrganizationFinYear.year>>>>>()).CreateView(this.Graph, clearQueryCache, mergeCache).SelectSingle(new object[2]
    {
      (object) organizationID,
      (object) yearNumber
    }) is OrganizationFinYear organizationFinYear1)
      return organizationFinYear1;
    if (((BqlCommand) new Select<OrganizationFinYear, Where<OrganizationFinYear.organizationID, Equal<Required<OrganizationFinYear.organizationID>>, And<OrganizationFinYear.year, Less<Required<OrganizationFinYear.year>>>>, OrderBy<Desc<OrganizationFinYear.year>>>()).CreateView(this.Graph, clearQueryCache, mergeCache).SelectSingle(new object[2]
    {
      (object) organizationID,
      (object) yearNumber
    }) is OrganizationFinYear organizationFinYear2)
      return organizationFinYear2;
    return ((BqlCommand) new Select<OrganizationFinYear, Where<OrganizationFinYear.organizationID, Equal<Required<OrganizationFinYear.organizationID>>, And<OrganizationFinYear.year, Greater<Required<OrganizationFinYear.year>>>>, OrderBy<Asc<OrganizationFinYear.year>>>()).CreateView(this.Graph, clearQueryCache, mergeCache).SelectSingle(new object[2]
    {
      (object) organizationID,
      (object) yearNumber
    }) is OrganizationFinYear organizationFinYear3 ? organizationFinYear3 : (OrganizationFinYear) null;
  }

  public class FinPeriodKeysByMasterAndOrganizationIDCachedCollection
  {
    public Dictionary<int, string> OrganizationFinPeriodByOrgAndMaster { get; set; }

    public void Load()
    {
      PXSelectBase<FinPeriod> pxSelectBase = (PXSelectBase<FinPeriod>) new PXSelectReadonly<FinPeriod, Where<FinPeriod.organizationID, NotEqual<FinPeriod.organizationID.masterValue>>>(PXGraph.CreateInstance<PXGraph>());
      using (new PXFieldScope(((PXSelectBase) pxSelectBase).View, new Type[3]
      {
        typeof (FinPeriod.organizationID),
        typeof (FinPeriod.finPeriodID),
        typeof (FinPeriod.masterFinPeriodID)
      }))
        this.OrganizationFinPeriodByOrgAndMaster = ((IEnumerable<PXResult<FinPeriod>>) pxSelectBase.Select(Array.Empty<object>())).AsEnumerable<PXResult<FinPeriod>>().Select<PXResult<FinPeriod>, FinPeriod>((Func<PXResult<FinPeriod>, FinPeriod>) (period => PXResult<FinPeriod>.op_Implicit(period))).ToDictionary<FinPeriod, int, string>((Func<FinPeriod, int>) (period => this.CalcCollectionKey(period.OrganizationID, period.MasterFinPeriodID)), (Func<FinPeriod, string>) (period => period.FinPeriodID));
    }

    public virtual string GetPeriodByMaster(int? organizationID, string masterFinPeriodID)
    {
      if (!organizationID.HasValue || masterFinPeriodID == null)
        return (string) null;
      string str;
      return this.OrganizationFinPeriodByOrgAndMaster.TryGetValue(this.CalcCollectionKey(organizationID, masterFinPeriodID), out str) ? str : (string) null;
    }

    public virtual int CalcCollectionKey(int? organizationID, string masterFinPeriodID)
    {
      return organizationID.Value * 100000 + Convert.ToInt32(masterFinPeriodID);
    }
  }
}

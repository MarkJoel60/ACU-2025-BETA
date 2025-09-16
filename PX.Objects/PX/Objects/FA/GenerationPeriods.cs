// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.GenerationPeriods
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.DAC;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FA;

public class GenerationPeriods : PXGraph<GenerationPeriods>
{
  public PXCancel<BoundaryYears> Cancel;
  public PXFilter<BoundaryYears> Years;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingOrderBy<FAOrganizationBook, BoundaryYears, OrderBy<Desc<FAOrganizationBook.updateGL, Asc<FAOrganizationBook.bookID>>>> Books;
  public PXSelect<FABookYear> bookyears;
  public PXSelect<FABookPeriod> bookperiods;
  public PXSelect<FABookPeriod, Where<FABookPeriod.organizationID, Equal<FinPeriod.organizationID.masterValue>>> MasterBookPeriods;
  public PXSetup<PX.Objects.GL.FinYearSetup> FinYearSetup;
  private GenerationPeriods.FABookPeriodIndex _FAMasterBookPeriodIndex = new GenerationPeriods.FABookPeriodIndex();

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFABookPeriodRepository FABookPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  public GenerationPeriods()
  {
    PX.Objects.GL.FinYearSetup current = ((PXSelectBase<PX.Objects.GL.FinYearSetup>) this.FinYearSetup).Current;
  }

  protected virtual void BoundaryYears_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    GenerationPeriods.\u003C\u003Ec__DisplayClass21_0 cDisplayClass210 = new GenerationPeriods.\u003C\u003Ec__DisplayClass21_0();
    if (e.Row == null)
      return;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.filter = (BoundaryYears) e.Row;
    int num1 = 1901;
    // ISSUE: reference to a compiler-generated field
    int num2 = int.Parse(cDisplayClass210.filter.FromYear);
    // ISSUE: reference to a compiler-generated field
    int num3 = int.Parse(cDisplayClass210.filter.ToYear);
    PXCache pxCache = sender;
    // ISSUE: reference to a compiler-generated field
    BoundaryYears filter = cDisplayClass210.filter;
    // ISSUE: reference to a compiler-generated field
    string fromYear = cDisplayClass210.filter.FromYear;
    PXSetPropertyException propertyException1;
    if (num2 >= num1)
      propertyException1 = (PXSetPropertyException) null;
    else
      propertyException1 = new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", (PXErrorLevel) 4, new object[1]
      {
        (object) (num1 - 1)
      });
    pxCache.RaiseExceptionHandling<BoundaryYears.fromYear>((object) filter, (object) fromYear, (Exception) propertyException1);
    PXSetPropertyException propertyException2 = (PXSetPropertyException) null;
    if (num3 < num1)
      propertyException2 = new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", (PXErrorLevel) 4, new object[1]
      {
        (object) (num1 - 1)
      });
    else if (num3 < num2)
      propertyException2 = new PXSetPropertyException("To Year must not be earlier than From Year.", (PXErrorLevel) 4);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    sender.RaiseExceptionHandling<BoundaryYears.toYear>((object) cDisplayClass210.filter, (object) cDisplayClass210.filter.ToYear, (Exception) propertyException2);
    ((PXProcessing<FAOrganizationBook>) this.Books).SetProcessAllEnabled(num2 >= num1 && num3 >= num1 && num3 >= num2);
    ((PXProcessing<FAOrganizationBook>) this.Books).SetProcessEnabled(num2 >= num1 && num3 >= num1 && num3 >= num2);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.exceptions = new List<Exception>();
    // ISSUE: method pointer
    ((PXProcessingBase<FAOrganizationBook>) this.Books).SetProcessDelegate(new PXProcessingBase<FAOrganizationBook>.ProcessListDelegate((object) cDisplayClass210, __methodptr(\u003CBoundaryYears_RowSelected\u003Eb__0)));
  }

  protected virtual void BoundaryYears_ToYear_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    int? nullable1 = new int?();
    foreach (PXResult<FAOrganizationBook> pxResult in ((PXSelectBase<FAOrganizationBook>) this.Books).Select(Array.Empty<object>()))
    {
      FAOrganizationBook organizationBook = PXResult<FAOrganizationBook>.op_Implicit(pxResult);
      int? nullable2 = organizationBook.LastCalendarYear != null ? new int?(int.Parse(organizationBook.LastCalendarYear)) : new int?();
      if (nullable2.HasValue && organizationBook.LastCalendarYear != null)
      {
        if (nullable1.HasValue)
        {
          int? nullable3 = nullable1;
          int? nullable4 = nullable2;
          if (!(nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() & nullable3.HasValue & nullable4.HasValue))
            continue;
        }
        nullable1 = new int?(nullable2.Value);
      }
    }
    if (!nullable1.HasValue)
      nullable1 = new int?(int.Parse(GraphHelper.RowCast<PX.Objects.GL.FinYearSetup>((IEnumerable) PXSelectBase<PX.Objects.GL.FinYearSetup, PXSelect<PX.Objects.GL.FinYearSetup>.Config>.Select((PXGraph) this, Array.Empty<object>())).First<PX.Objects.GL.FinYearSetup>().FirstFinYear) - 1);
    e.NewValue = (object) (nullable1.Value + 1).ToString("0000");
  }

  protected virtual void BoundaryYears_FromYear_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    int? nullable1 = new int?();
    foreach (PXResult<FAOrganizationBook> pxResult in ((PXSelectBase<FAOrganizationBook>) this.Books).Select(Array.Empty<object>()))
    {
      FAOrganizationBook organizationBook = PXResult<FAOrganizationBook>.op_Implicit(pxResult);
      int? nullable2 = organizationBook.LastCalendarYear != null ? new int?(int.Parse(organizationBook.LastCalendarYear)) : new int?();
      if (nullable2.HasValue && organizationBook.LastCalendarYear != null)
      {
        if (nullable1.HasValue)
        {
          int? nullable3 = nullable1;
          int? nullable4 = nullable2;
          if (!(nullable3.GetValueOrDefault() > nullable4.GetValueOrDefault() & nullable3.HasValue & nullable4.HasValue))
            continue;
        }
        nullable1 = new int?(nullable2.Value);
      }
    }
    if (!nullable1.HasValue)
      nullable1 = new int?(int.Parse(GraphHelper.RowCast<PX.Objects.GL.FinYearSetup>((IEnumerable) PXSelectBase<PX.Objects.GL.FinYearSetup, PXSelect<PX.Objects.GL.FinYearSetup>.Config>.Select((PXGraph) this, Array.Empty<object>())).First<PX.Objects.GL.FinYearSetup>().FirstFinYear) - 1);
    e.NewValue = (object) (nullable1.Value + 1).ToString("0000");
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R2.")]
  public static void GeneratePeriods(BoundaryYears filter, List<FAOrganizationBook> books)
  {
    List<Exception> exceptions = new List<Exception>();
    GenerationPeriods.GeneratePeriods(filter, books, ref exceptions);
  }

  public static void GeneratePeriods(
    BoundaryYears filter,
    List<FAOrganizationBook> books,
    ref List<Exception> exceptions)
  {
    PXGraph.CreateInstance<GenerationPeriods>().GeneratePeriodsProc(filter, books, ref exceptions);
  }

  protected virtual void FAOrganizationBook_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    FAOrganizationBook row = (FAOrganizationBook) e.Row;
    if (row == null)
      return;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    bool? updateGl;
    if (PXResultset<FABookYearSetup>.op_Implicit(PXSelectBase<FABookYearSetup, PXSelect<FABookYearSetup, Where<FABookYearSetup.bookID, Equal<Current<FAOrganizationBook.bookID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>())) == null)
    {
      updateGl = row.UpdateGL;
      if (!updateGl.GetValueOrDefault())
        propertyException = new PXSetPropertyException("FA calendar cannot be generated for the book '{0}' because the calendar structure is not configured for this book on the Book Calendars (FA206000) form.", (PXErrorLevel) 3, new object[1]
        {
          (object) row.BookCode
        });
    }
    OrganizationFinYear organizationFinYear = this.FinPeriodRepository.FindNearestOrganizationFinYear(row.OrganizationID, "1900");
    if (propertyException == null)
    {
      int? organizationId = row.OrganizationID;
      int num = 0;
      if (!(organizationId.GetValueOrDefault() == num & organizationId.HasValue))
      {
        updateGl = row.UpdateGL;
        if (updateGl.GetValueOrDefault() && organizationFinYear == null)
          propertyException = new PXSetPropertyException("The calendar for the {0} company does not exist. Generate it on the Company Financial Calendar (GL201100) form.", (PXErrorLevel) 3, new object[1]
          {
            (object) PXAccess.GetOrganizationCD(row.OrganizationID)
          });
      }
    }
    if (propertyException == null)
      return;
    PXUIFieldAttribute.SetEnabled<FAOrganizationBook.selected>(sender, (object) row, false);
    sender.RaiseExceptionHandling<FAOrganizationBook.selected>((object) row, (object) null, (Exception) propertyException);
  }

  protected virtual void _(PX.Data.Events.RowSelected<FABookYear> e)
  {
    if (e.Row == null)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FABookYear>>) e).Cache;
    FABookYear row = e.Row;
    int? organizationId = e.Row.OrganizationID;
    int num1 = 0;
    int num2 = organizationId.GetValueOrDefault() == num1 & organizationId.HasValue ? 2 : 0;
    PXDefaultAttribute.SetPersistingCheck<FABookYear.startMasterFinPeriodID>(cache, (object) row, (PXPersistingCheck) num2);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FABookPeriod> e)
  {
    if (e.Row == null)
      return;
    FABookPeriod row = e.Row;
    int? organizationId = row.OrganizationID;
    int num = 0;
    if (!(organizationId.GetValueOrDefault() == num & organizationId.HasValue))
      return;
    this._FAMasterBookPeriodIndex.Add(row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<FABookPeriod> e)
  {
    if (e.Row == null)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FABookPeriod>>) e).Cache;
    FABookPeriod row = e.Row;
    int? organizationId = e.Row.OrganizationID;
    int num1 = 0;
    int num2 = organizationId.GetValueOrDefault() == num1 & organizationId.HasValue ? 2 : 0;
    PXDefaultAttribute.SetPersistingCheck<FABookPeriod.masterFinPeriodID>(cache, (object) row, (PXPersistingCheck) num2);
  }

  protected virtual void _(PX.Data.Events.RowInserted<FABookPeriod> e)
  {
    if (e.Row == null)
      return;
    FABookPeriod row = e.Row;
    int? organizationId = row.OrganizationID;
    int num = 0;
    if (!(organizationId.GetValueOrDefault() == num & organizationId.HasValue))
      return;
    this._FAMasterBookPeriodIndex.Add(row);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FABookPeriod> e)
  {
    if (e.Row == null)
      return;
    FABookPeriod row = e.Row;
    int? organizationId = row.OrganizationID;
    int num = 0;
    if (!(organizationId.GetValueOrDefault() == num & organizationId.HasValue))
      return;
    this._FAMasterBookPeriodIndex.Remove(row);
  }

  protected virtual void AddFieldDefaultingHandler<FieldID>(
    int? id,
    Dictionary<(Type, int), PXFieldDefaulting> delegates)
    where FieldID : IBqlField
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    GenerationPeriods.\u003C\u003Ec__DisplayClass32_0<FieldID> cDisplayClass320 = new GenerationPeriods.\u003C\u003Ec__DisplayClass32_0<FieldID>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass320.id = id;
    PXFieldDefaulting pxFieldDefaulting = (PXFieldDefaulting) null;
    // ISSUE: reference to a compiler-generated field
    if (!delegates.TryGetValue((typeof (FieldID), cDisplayClass320.id.GetValueOrDefault()), out pxFieldDefaulting))
    {
      // ISSUE: method pointer
      pxFieldDefaulting = new PXFieldDefaulting((object) cDisplayClass320, __methodptr(\u003CAddFieldDefaultingHandler\u003Eb__0));
      // ISSUE: reference to a compiler-generated field
      delegates.Add((typeof (FieldID), cDisplayClass320.id.GetValueOrDefault()), pxFieldDefaulting);
    }
    ((PXGraph) this).FieldDefaulting.AddHandler<FieldID>(pxFieldDefaulting);
  }

  protected virtual void RemoveFieldDefaultingHandler<FieldID>(
    int? id,
    Dictionary<(Type, int), PXFieldDefaulting> delegates)
    where FieldID : IBqlField
  {
    ((PXGraph) this).FieldDefaulting.RemoveHandler<FieldID>(delegates[(typeof (FieldID), id.GetValueOrDefault())]);
  }

  public virtual void AddFieldDefaultingHandlers(
    FABook book,
    Dictionary<(Type, int), PXFieldDefaulting> delegates)
  {
    this.AddFieldDefaultingHandler<FABookYear.bookID>(book.BookID, delegates);
    this.AddFieldDefaultingHandler<FABookPeriod.bookID>(book.BookID, delegates);
  }

  public virtual void RemoveFieldDefaultingHandlers(
    FABook book,
    Dictionary<(Type, int), PXFieldDefaulting> delegates)
  {
    this.RemoveFieldDefaultingHandler<FABookYear.bookID>(book.BookID, delegates);
    this.RemoveFieldDefaultingHandler<FABookPeriod.bookID>(book.BookID, delegates);
  }

  protected virtual void GenerateFAMasterCalendarInYearRange(
    FABook book,
    string fromYearNumber,
    string toYearNumber)
  {
    if (string.Compare(fromYearNumber, toYearNumber) > 0)
      Utilities.Swap<string>(ref fromYearNumber, ref toYearNumber);
    this.GenerateFAMasterCalendarUpToYear(book, fromYearNumber);
    this.GenerateFAMasterCalendarToYear(book, toYearNumber);
  }

  protected virtual void GenerateFAMasterCalendarUpToYear(FABook book, string fromYearNumber)
  {
    IYearSetupMaintenanceGraph maintenanceGraph = book.UpdateGL.GetValueOrDefault() ? (IYearSetupMaintenanceGraph) PXGraph.CreateInstance<FiscalYearSetupMaint>() : (IYearSetupMaintenanceGraph) PXGraph.CreateInstance<FABookYearSetupMaint>();
    maintenanceGraph.SetCurrentYearSetup(new object[1]
    {
      (object) book.BookID
    });
    maintenanceGraph.ShiftBackFirstYearTo($"{fromYearNumber:0000}");
    FABookYear firstMasterFABookYear = this.FABookPeriodRepository.FindFirstFABookYear(book.BookID, new int?(0), mergeCache: true);
    FABookYear faBookYear = new FABookYear()
    {
      Year = firstMasterFABookYear?.Year
    };
    while (faBookYear != null && (faBookYear.Year == null || string.Compare(faBookYear.Year, fromYearNumber) > 0))
    {
      faBookYear = this.CreatePreviousMasterFABookYear(book, firstMasterFABookYear);
      if (firstMasterFABookYear != null)
      {
        DateTime? endDate = faBookYear.EndDate;
        DateTime? startDate = firstMasterFABookYear.StartDate;
        if ((endDate.HasValue == startDate.HasValue ? (endDate.HasValue ? (endDate.GetValueOrDefault() != startDate.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
          throw new PXSetPropertyException("The year {0} in the {1} book cannot be created for the master calendar because the financial year's configuration has been changed.", new object[2]
          {
            (object) faBookYear.Year,
            (object) book.BookCode
          });
      }
      firstMasterFABookYear = faBookYear;
    }
  }

  protected virtual void GenerateFAMasterCalendarToYear(FABook book, string toYearNumber)
  {
    FABookYear lastMasterFABookYear = this.FABookPeriodRepository.FindLastFABookYear(book.BookID, new int?(0), mergeCache: true);
    FABookYear faBookYear = new FABookYear()
    {
      Year = lastMasterFABookYear?.Year
    };
    while (faBookYear != null && (faBookYear.Year == null || string.Compare(faBookYear.Year, toYearNumber) < 0))
    {
      faBookYear = this.CreateNextMasterFABookYear(book, lastMasterFABookYear);
      lastMasterFABookYear = faBookYear;
    }
  }

  protected virtual FABookYear CreateNextMasterFABookYear(
    FABook book,
    FABookYear lastMasterFABookYear = null)
  {
    IYearSetup faBookYearSetup = this.FABookPeriodRepository.FindFABookYearSetup(book, true);
    IEnumerable<IPeriodSetup> faBookPeriodSetup = this.FABookPeriodRepository.FindFABookPeriodSetup(book, true);
    if (lastMasterFABookYear == null)
      lastMasterFABookYear = this.FABookPeriodRepository.FindLastFABookYear(book.BookID, new int?(0), mergeCache: true);
    return FiscalYearCreator<FABookYear, FABookPeriod>.CreateNextYear((PXGraph) this, faBookYearSetup, faBookPeriodSetup, lastMasterFABookYear);
  }

  protected virtual FABookYear CreatePreviousMasterFABookYear(
    FABook book,
    FABookYear firstMasterFABookYear = null)
  {
    IYearSetup faBookYearSetup = this.FABookPeriodRepository.FindFABookYearSetup(book, true);
    IEnumerable<IPeriodSetup> faBookPeriodSetup = this.FABookPeriodRepository.FindFABookPeriodSetup(book, true);
    if (firstMasterFABookYear == null)
      firstMasterFABookYear = this.FABookPeriodRepository.FindFirstFABookYear(book.BookID, new int?(0), mergeCache: true);
    return FiscalYearCreator<FABookYear, FABookPeriod>.CreatePrevYear((PXGraph) this, faBookYearSetup, faBookPeriodSetup, firstMasterFABookYear);
  }

  protected virtual void GenerateFAOrganizationCalendarInYearRange(
    FAOrganizationBook book,
    string fromYearNumber,
    string toYearNumber)
  {
    this.InitializeFAOrganizationCalendar(book, fromYearNumber);
    this.GenerateFAOrganizationCalendarUpToYear(book, fromYearNumber);
    this.GenerateFAOrganizationCalendarToYear(book, toYearNumber);
  }

  protected virtual void InitializeFAOrganizationCalendar(
    FAOrganizationBook book,
    string yearNumber)
  {
    if (this.FABookPeriodRepository.FindFirstFABookYear(book.BookID, book.OrganizationID, true) != null)
      return;
    OrganizationFinYear organizationFinYear = this.FinPeriodRepository.FindNearestOrganizationFinYear(book.OrganizationID, yearNumber);
    string year = organizationFinYear.Year;
    string toYearNumber = PX.Objects.GL.FinPeriods.FinPeriodUtils.FiscalYear(organizationFinYear.StartMasterFinPeriodID);
    this.GenerateFAMasterCalendarInYearRange((FABook) book, year, toYearNumber);
    FABookYear masterFaBookYearById = this.FABookPeriodRepository.FindMasterFABookYearByID((FABook) book, year, mergeCache: true);
    FABookPeriod faBookPeriodById = this.FABookPeriodRepository.FindMasterFABookPeriodByID((FABook) book, organizationFinYear.StartMasterFinPeriodID, mergeCache: true);
    this.GenerateSingleOrganizationFABookYear(book, masterFaBookYearById, faBookPeriodById);
  }

  protected virtual FABookPeriod CopyOrganizationFABookPeriodFromMaster(
    FAOrganizationBook book,
    FABookPeriod masterFABookPeriod,
    string yearNumber = null,
    string periodNumber = null)
  {
    string str = this.FinPeriodUtils.ComposeFinPeriodID(yearNumber, periodNumber) ?? masterFABookPeriod.FinPeriodID;
    return new FABookPeriod()
    {
      BookID = masterFABookPeriod.BookID,
      OrganizationID = book.OrganizationID,
      FinPeriodID = str,
      MasterFinPeriodID = masterFABookPeriod.FinPeriodID,
      FinYear = yearNumber ?? masterFABookPeriod.FinYear,
      PeriodNbr = periodNumber ?? masterFABookPeriod.PeriodNbr,
      Custom = masterFABookPeriod.Custom,
      DateLocked = masterFABookPeriod.DateLocked,
      StartDate = masterFABookPeriod.StartDate,
      EndDate = masterFABookPeriod.EndDate,
      Descr = masterFABookPeriod.Descr
    };
  }

  protected virtual FABookPeriod GenerateAdjustmentOrganizationFABookPeriod(
    FAOrganizationBook book,
    FABookPeriod prevOrganizationFABookPeriod)
  {
    string yearNumber1 = this.FinPeriodUtils.ParseFinPeriodID(prevOrganizationFABookPeriod.FinPeriodID).yearNumber;
    FABookYear masterFaBookYearById = this.FABookPeriodRepository.FindMasterFABookYearByID((FABook) book, yearNumber1, mergeCache: true);
    string str = $"{yearNumber1:0000}{masterFaBookYearById.FinPeriods:00}";
    (string yearNumber, string periodNumber) finPeriodId = this.FinPeriodUtils.ParseFinPeriodID(prevOrganizationFABookPeriod.FinPeriodID);
    string yearNumber2 = finPeriodId.yearNumber;
    string periodNumber = $"{int.Parse(finPeriodId.periodNumber) + 1:00}";
    return new FABookPeriod()
    {
      BookID = book.BookID,
      OrganizationID = book.OrganizationID,
      FinPeriodID = this.FinPeriodUtils.ComposeFinPeriodID(yearNumber2, periodNumber),
      MasterFinPeriodID = str,
      FinYear = yearNumber2,
      PeriodNbr = periodNumber,
      Custom = prevOrganizationFABookPeriod.Custom,
      DateLocked = prevOrganizationFABookPeriod.DateLocked,
      StartDate = prevOrganizationFABookPeriod.EndDate,
      EndDate = prevOrganizationFABookPeriod.EndDate,
      Descr = "Adjustment Period"
    };
  }

  protected virtual FABookYear GenerateSingleOrganizationFABookYear(
    FAOrganizationBook book,
    FABookYear startMasterYear,
    FABookPeriod startMasterPeriod)
  {
    if (startMasterYear == null)
      throw new ArgumentNullException(nameof (startMasterYear));
    if (startMasterPeriod == null)
      throw new ArgumentNullException(nameof (startMasterPeriod));
    FABookYear organizationFaBookYear = GraphHelper.Caches<FABookYear>((PXGraph) this).Insert(new FABookYear()
    {
      BookID = book.BookID,
      OrganizationID = book.OrganizationID,
      Year = startMasterYear.Year,
      FinPeriods = startMasterYear.FinPeriods,
      StartMasterFinPeriodID = startMasterPeriod.FinPeriodID,
      StartDate = startMasterPeriod.StartDate
    });
    short num1 = 1;
    FABookPeriod masterFABookPeriod = startMasterPeriod;
    int num2 = (int) organizationFaBookYear.FinPeriods.Value;
    IYearSetup faBookYearSetup = this.FABookPeriodRepository.FindFABookYearSetup((FABook) book, true);
    bool flag = faBookYearSetup.HasAdjustmentPeriod.GetValueOrDefault() && faBookYearSetup.FPType != FiscalPeriodSetupCreator.FPType.Custom || faBookYearSetup.FPType == FiscalPeriodSetupCreator.FPType.Custom && this.FABookPeriodRepository.FindFABookPeriodSetup((FABook) book).Where<IPeriodSetup>((Func<IPeriodSetup, bool>) (periodSetup =>
    {
      DateTime? startDate = periodSetup.StartDate;
      DateTime? endDate = periodSetup.EndDate;
      if (startDate.HasValue != endDate.HasValue)
        return false;
      return !startDate.HasValue || startDate.GetValueOrDefault() == endDate.GetValueOrDefault();
    })).Any<IPeriodSetup>();
    if (flag)
      --num2;
    FABookPeriod prevOrganizationFABookPeriod = (FABookPeriod) null;
    for (; (int) num1 <= num2; ++num1)
    {
      prevOrganizationFABookPeriod = GraphHelper.Caches<FABookPeriod>((PXGraph) this).Insert(this.CopyOrganizationFABookPeriodFromMaster(book, masterFABookPeriod, organizationFaBookYear.Year, $"{num1:00}"));
      if ((int) num1 < num2)
      {
        string finPeriodId = masterFABookPeriod.FinPeriodID;
        while ((masterFABookPeriod = this._FAMasterBookPeriodIndex.FindNextNonAdjustmentMasterFABookPeriod(book.BookID, finPeriodId)) == null)
          this.CreateNextMasterFABookYear((FABook) book);
      }
    }
    organizationFaBookYear.EndDate = prevOrganizationFABookPeriod.EndDate;
    if (flag)
      GraphHelper.Caches<FABookPeriod>((PXGraph) this).Insert(this.GenerateAdjustmentOrganizationFABookPeriod(book, prevOrganizationFABookPeriod));
    return organizationFaBookYear;
  }

  protected virtual void GenerateFAOrganizationCalendarUpToYear(
    FAOrganizationBook book,
    string fromYearNumber)
  {
    FABookYear firsOrganizationFABookyear = this.FABookPeriodRepository.FindFirstFABookYear(book.BookID, book.OrganizationID, mergeCache: true);
    FABookYear faBookYear = new FABookYear()
    {
      Year = firsOrganizationFABookyear?.Year
    };
    while (faBookYear != null && string.Compare(faBookYear.Year, fromYearNumber) > 0)
    {
      faBookYear = this.CreatePreviousOrganizationFABookYear(book, firsOrganizationFABookyear);
      if (firsOrganizationFABookyear != null)
      {
        DateTime? endDate = faBookYear.EndDate;
        DateTime? startDate = firsOrganizationFABookyear.StartDate;
        if ((endDate.HasValue == startDate.HasValue ? (endDate.HasValue ? (endDate.GetValueOrDefault() != startDate.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
          throw new PXSetPropertyException("The year {0} in the {1} book cannot be created for the {2} company because the financial year's configuration has been changed.", new object[3]
          {
            (object) faBookYear.Year,
            (object) book.BookCode,
            (object) PXAccess.GetOrganizationCD(book.OrganizationID)
          });
      }
      firsOrganizationFABookyear = faBookYear;
    }
  }

  protected virtual void GenerateFAOrganizationCalendarToYear(
    FAOrganizationBook book,
    string toYearNumber)
  {
    FABookYear lastOrganizationFABookYear = this.FABookPeriodRepository.FindLastFABookYear(book.BookID, book.OrganizationID, mergeCache: true);
    FABookYear faBookYear = new FABookYear()
    {
      Year = lastOrganizationFABookYear?.Year
    };
    while (faBookYear != null && string.Compare(faBookYear.Year, toYearNumber) < 0)
    {
      faBookYear = this.CreateNextOrganizationFABookYear(book, lastOrganizationFABookYear);
      lastOrganizationFABookYear = faBookYear;
    }
  }

  protected virtual FABookYear CreateNextOrganizationFABookYear(
    FAOrganizationBook book,
    FABookYear lastOrganizationFABookYear)
  {
    string str = $"{int.Parse(lastOrganizationFABookYear.Year) + 1:0000}";
    FABookYear masterFaBookYearById;
    while ((masterFaBookYearById = this.FABookPeriodRepository.FindMasterFABookYearByID((FABook) book, str, mergeCache: true)) == null)
      this.GenerateFAMasterCalendarInYearRange((FABook) book, this.FABookPeriodRepository.FindLastFABookYear(book.BookID, new int?(0), mergeCache: true).Year, str);
    short num1 = masterFaBookYearById.FinPeriods.Value;
    if (this.FABookPeriodRepository.FindFABookYearSetup((FABook) book, true).HasAdjustmentPeriod.GetValueOrDefault())
      --num1;
    FABookPeriod bookPeriodOfYear = this.FABookPeriodRepository.FindLastNonAdjustmentOrganizationFABookPeriodOfYear(book, lastOrganizationFABookYear.Year, mergeCache: true);
    int num2 = int.Parse(bookPeriodOfYear.FinYear);
    PXSelectBase<FABookPeriod> pxSelectBase1 = (PXSelectBase<FABookPeriod>) new PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<FinPeriod.organizationID.masterValue>, And<FABookPeriod.finPeriodID, Greater<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>>>>>, OrderBy<Asc<FABookPeriod.finPeriodID>>>((PXGraph) this);
    List<FABookPeriod> list;
    while (true)
    {
      PXSelectBase<FABookPeriod> pxSelectBase2 = pxSelectBase1;
      int num3 = (int) num1;
      object[] objArray = new object[2]
      {
        (object) book.BookID,
        (object) bookPeriodOfYear.MasterFinPeriodID
      };
      if ((list = GraphHelper.RowCast<FABookPeriod>((IEnumerable) pxSelectBase2.SelectWindowed(0, num3, objArray)).ToList<FABookPeriod>()).Count < (int) num1)
      {
        ++num2;
        this.GenerateFAMasterCalendarInYearRange((FABook) book, this.FABookPeriodRepository.FindLastFABookYear(book.BookID, new int?(0), mergeCache: true).Year, num2.ToString("0000"));
      }
      else
        break;
    }
    FABookPeriod startMasterPeriod = list.First<FABookPeriod>();
    return this.GenerateSingleOrganizationFABookYear(book, masterFaBookYearById, startMasterPeriod);
  }

  protected virtual FABookYear CreatePreviousOrganizationFABookYear(
    FAOrganizationBook book,
    FABookYear firsOrganizationFABookyear)
  {
    string str = $"{int.Parse(firsOrganizationFABookyear.Year) - 1:0000}";
    FABookYear masterFaBookYearById;
    while ((masterFaBookYearById = this.FABookPeriodRepository.FindMasterFABookYearByID((FABook) book, str, mergeCache: true)) == null)
      this.GenerateFAMasterCalendarInYearRange((FABook) book, str, this.FABookPeriodRepository.FindFirstFABookYear(book.BookID, new int?(0), mergeCache: true).Year);
    short num1 = masterFaBookYearById.FinPeriods.Value;
    if (this.FABookPeriodRepository.FindFABookYearSetup((FABook) book, true).HasAdjustmentPeriod.GetValueOrDefault())
      --num1;
    int num2 = int.Parse(PX.Objects.GL.FinPeriods.FinPeriodUtils.FiscalYear(firsOrganizationFABookyear.StartMasterFinPeriodID));
    PXSelectBase<FABookPeriod> pxSelectBase1 = (PXSelectBase<FABookPeriod>) new PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<FinPeriod.organizationID.masterValue>, And<FABookPeriod.finPeriodID, Less<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>>>>>, OrderBy<Desc<FABookPeriod.finPeriodID>>>((PXGraph) this);
    List<FABookPeriod> list;
    while (true)
    {
      PXSelectBase<FABookPeriod> pxSelectBase2 = pxSelectBase1;
      int num3 = (int) num1;
      object[] objArray = new object[2]
      {
        (object) book.BookID,
        (object) firsOrganizationFABookyear.StartMasterFinPeriodID
      };
      if ((list = GraphHelper.RowCast<FABookPeriod>((IEnumerable) pxSelectBase2.SelectWindowed(0, num3, objArray)).ToList<FABookPeriod>()).Count < (int) num1)
      {
        --num2;
        this.GenerateFAMasterCalendarInYearRange((FABook) book, num2.ToString("0000"), this.FABookPeriodRepository.FindFirstFABookYear(book.BookID, new int?(0), mergeCache: true).Year);
      }
      else
        break;
    }
    FABookPeriod startMasterPeriod = list.Last<FABookPeriod>();
    return this.GenerateSingleOrganizationFABookYear(book, masterFaBookYearById, startMasterPeriod);
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R2.")]
  public virtual void GeneratePeriodsProc(BoundaryYears filter, List<FAOrganizationBook> books)
  {
    List<Exception> exceptions = new List<Exception>();
    this.GeneratePeriodsProc(filter, books, ref exceptions);
  }

  public virtual void GeneratePeriodsProc(
    BoundaryYears filter,
    List<FAOrganizationBook> books,
    ref List<Exception> exceptions)
  {
    Dictionary<(Type, int), PXFieldDefaulting> delegates = new Dictionary<(Type, int), PXFieldDefaulting>();
    List<FAOrganizationBook> list = books.Where<FAOrganizationBook>((Func<FAOrganizationBook, bool>) (book => !book.UpdateGL.GetValueOrDefault())).ToList<FAOrganizationBook>();
    ((PXSelectBase) this.MasterBookPeriods).View.SelectMulti(Array.Empty<object>());
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      Exception exception1 = (Exception) null;
      foreach (FAOrganizationBook book in list)
      {
        PXProcessing<FAOrganizationBook>.SetCurrentItem((object) book);
        this.AddFieldDefaultingHandlers((FABook) book, delegates);
        try
        {
          this.GenerateFAMasterCalendarInYearRange((FABook) book, filter.FromYear, filter.ToYear);
        }
        catch (Exception ex)
        {
          exception1 = ex;
        }
        finally
        {
          this.RemoveFieldDefaultingHandlers((FABook) book, delegates);
        }
        this.SetProcessingResult(exception1, ref exceptions);
        exception1 = (Exception) null;
      }
      bool flag = PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>();
      FAOrganizationBook organizationBook1 = books.Where<FAOrganizationBook>((Func<FAOrganizationBook, bool>) (book =>
      {
        if (!book.UpdateGL.GetValueOrDefault())
          return false;
        int? organizationId = book.OrganizationID;
        int num = 0;
        return organizationId.GetValueOrDefault() == num & organizationId.HasValue;
      })).FirstOrDefault<FAOrganizationBook>();
      List<FAOrganizationBook> organizationBookList;
      if (flag)
      {
        if (organizationBook1 != null)
          throw new PXException("The master calendar of a posting book cannot be selected directly if multiple calendars are supported");
        organizationBookList = books.Where<FAOrganizationBook>((Func<FAOrganizationBook, bool>) (book =>
        {
          if (!book.UpdateGL.GetValueOrDefault())
            return false;
          int? organizationId = book.OrganizationID;
          int num = 0;
          return !(organizationId.GetValueOrDefault() == num & organizationId.HasValue);
        })).ToList<FAOrganizationBook>();
      }
      else
      {
        organizationBookList = new List<FAOrganizationBook>();
        foreach (PXResult<FABook, PX.Objects.GL.DAC.Organization> pxResult in PXSelectBase<FABook, PXSelectReadonly2<FABook, CrossJoin<PX.Objects.GL.DAC.Organization>, Where<FABook.updateGL, Equal<True>, And<PX.Objects.GL.DAC.Organization.organizationType, NotEqual<OrganizationTypes.group>, And<PX.Objects.GL.DAC.Organization.status, Equal<OrganizationStatus.active>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
        {
          FABook faBook = PXResult<FABook, PX.Objects.GL.DAC.Organization>.op_Implicit(pxResult);
          PX.Objects.GL.DAC.Organization organization = PXResult<FABook, PX.Objects.GL.DAC.Organization>.op_Implicit(pxResult);
          FAOrganizationBook organizationBook2 = new FAOrganizationBook();
          PXCache<FABook>.RestoreCopy((FABook) organizationBook2, faBook);
          organizationBook2.RawOrganizationID = organization.OrganizationID;
          organizationBook2.OrganizationID = new int?(organizationBook2.RawOrganizationID.GetValueOrDefault());
          organizationBook2.OrganizationCD = organization.OrganizationCD;
          organizationBookList.Add(organizationBook2);
        }
      }
      Exception exception2 = (Exception) null;
      foreach (FAOrganizationBook book in organizationBookList)
      {
        PXProcessing<FAOrganizationBook>.SetCurrentItem(flag ? (object) book : (object) organizationBook1);
        this.AddFieldDefaultingHandlers((FABook) book, delegates);
        try
        {
          this.GenerateFAOrganizationCalendarInYearRange(book, filter.FromYear, filter.ToYear);
        }
        catch (Exception ex)
        {
          exception2 = ex;
        }
        finally
        {
          this.RemoveFieldDefaultingHandlers((FABook) book, delegates);
        }
        if (flag)
        {
          this.SetProcessingResult(exception2, ref exceptions);
          exception2 = (Exception) null;
        }
      }
      if (!flag)
        this.SetProcessingResult(exception2, ref exceptions);
      transactionScope.Complete();
    }
  }

  public virtual void SetProcessingResult(Exception exception, ref List<Exception> exceptions)
  {
    if (exception != null)
    {
      PXProcessing<FAOrganizationBook>.SetError(exception);
      exceptions.Add(exception);
      ((PXSelectBase) this.bookperiods).Cache.Clear();
      ((PXSelectBase) this.bookyears).Cache.Clear();
      this._FAMasterBookPeriodIndex = new GenerationPeriods.FABookPeriodIndex();
      ((PXSelectBase) this.MasterBookPeriods).Cache.ClearQueryCache();
      ((PXSelectBase) this.MasterBookPeriods).View.SelectMulti(Array.Empty<object>());
    }
    else
    {
      PXProcessing<FAOrganizationBook>.SetProcessed();
      ((PXGraph) this).Actions.PressSave();
    }
  }

  private class FABookPeriodIndex
  {
    private Dictionary<int?, SortedDictionary<string, FABookPeriod>> _periods = new Dictionary<int?, SortedDictionary<string, FABookPeriod>>();

    public void Add(FABookPeriod period)
    {
      SortedDictionary<string, FABookPeriod> sortedDictionary = (SortedDictionary<string, FABookPeriod>) null;
      if (!this._periods.TryGetValue(period.BookID, out sortedDictionary))
      {
        sortedDictionary = new SortedDictionary<string, FABookPeriod>();
        this._periods.Add(period.BookID, sortedDictionary);
      }
      sortedDictionary.Add(period.FinPeriodID, period);
    }

    public bool Remove(FABookPeriod period)
    {
      SortedDictionary<string, FABookPeriod> sortedDictionary = (SortedDictionary<string, FABookPeriod>) null;
      if (!this._periods.TryGetValue(period.BookID, out sortedDictionary))
      {
        sortedDictionary = new SortedDictionary<string, FABookPeriod>();
        this._periods.Add(period.BookID, sortedDictionary);
      }
      sortedDictionary.Remove(period.FinPeriodID);
      return sortedDictionary.Count == 0 && this._periods.Remove(period.BookID);
    }

    public FABookPeriod FindNextNonAdjustmentMasterFABookPeriod(
      int? BookID,
      string prevFABookPeriodID)
    {
      SortedDictionary<string, FABookPeriod> source = (SortedDictionary<string, FABookPeriod>) null;
      return !this._periods.TryGetValue(BookID, out source) ? (FABookPeriod) null : source.Where<KeyValuePair<string, FABookPeriod>>((Func<KeyValuePair<string, FABookPeriod>, bool>) (i =>
      {
        if (string.Compare(i.Key, prevFABookPeriodID) <= 0)
          return false;
        DateTime? startDate = i.Value.StartDate;
        DateTime? endDate = i.Value.EndDate;
        if (startDate.HasValue != endDate.HasValue)
          return true;
        return startDate.HasValue && startDate.GetValueOrDefault() != endDate.GetValueOrDefault();
      })).FirstOrDefault<KeyValuePair<string, FABookPeriod>>().Value;
    }
  }
}

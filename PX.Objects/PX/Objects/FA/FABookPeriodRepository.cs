// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookPeriodRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FA;

public class FABookPeriodRepository : IFABookPeriodRepository
{
  protected readonly PXGraph Graph;

  protected virtual Dictionary<int, FABook> FABooks
  {
    get
    {
      return PXDatabase.GetSlot<FABookCollection>("FABookCollection", new Type[1]
      {
        typeof (FABook)
      }).Books;
    }
  }

  public FABookPeriodRepository(PXGraph graph) => this.Graph = graph;

  public static void CheckNotNullIDContract(int? id, string name)
  {
    if (!id.HasValue)
      throw new ArgumentNullException(name);
  }

  public static void CheckNotNullObjectContract(object obj, string name)
  {
    if (obj == null)
      throw new ArgumentNullException(name);
  }

  public static void CheckNotNullStringContract(string str, string name)
  {
    if (str == null)
      throw new ArgumentNullException(name);
  }

  public static void CheckNotNullOrEmptyStringContract(string str, string name)
  {
    if (string.IsNullOrEmpty(str))
      throw new ArgumentNullException(name);
  }

  public FABook FindFABook(int? bookID)
  {
    FABookPeriodRepository.CheckNotNullIDContract(bookID, nameof (bookID));
    FABook faBook;
    this.FABooks.TryGetValue(bookID.Value, out faBook);
    return faBook;
  }

  public bool IsPostingFABook(int? bookID)
  {
    return (this.FindFABook(bookID) ?? throw new ArgumentOutOfRangeException(nameof (bookID))).UpdateGL.GetValueOrDefault();
  }

  public int GetFABookPeriodOrganizationID(int? bookID, int? assetID, bool check = true)
  {
    if (check)
    {
      FABookPeriodRepository.CheckNotNullIDContract(bookID, nameof (bookID));
      FABookPeriodRepository.CheckNotNullIDContract(assetID, nameof (assetID));
    }
    if (!bookID.HasValue || !this.IsPostingFABook(bookID))
      return 0;
    return PXAccess.GetParentOrganizationID((PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select(this.Graph, new object[1]
    {
      (object) assetID
    })) ?? throw new ArgumentOutOfRangeException(nameof (assetID))).BranchID).Value;
  }

  public int GetFABookPeriodOrganizationID(FABookBalance balance, bool check = true)
  {
    return this.GetFABookPeriodOrganizationID(balance.BookID, balance.AssetID, check);
  }

  public IYearSetup FindFABookYearSetup(FABook book, bool clearQueryCache = false)
  {
    PXView pxView = new PXView(this.Graph, true, !book.UpdateGL.GetValueOrDefault() ? (BqlCommand) new Select<FABookYearSetup, Where<FABookYearSetup.bookID, Equal<Required<FABook.bookID>>>>() : (BqlCommand) new Select<FinYearSetup>());
    if (clearQueryCache)
      pxView.Clear();
    return pxView.SelectSingle(new object[1]
    {
      (object) book.BookID
    }) as IYearSetup;
  }

  public IYearSetup FindFABookYearSetup(int? bookID, bool clearQueryCache = false)
  {
    return this.FindFABookYearSetup(this.FindFABook(bookID), clearQueryCache);
  }

  public IEnumerable<IPeriodSetup> FindFABookPeriodSetup(FABook book, bool clearQueryCache = false)
  {
    PXView pxView = new PXView(this.Graph, true, !book.UpdateGL.GetValueOrDefault() ? (BqlCommand) new Select<FABookPeriodSetup, Where<FABookPeriodSetup.bookID, Equal<Required<FABook.bookID>>>, OrderBy<Asc<FABookPeriodSetup.periodNbr>>>() : (BqlCommand) new Select3<FinPeriodSetup, OrderBy<Asc<FinPeriodSetup.periodNbr>>>());
    if (clearQueryCache)
      pxView.Clear();
    return pxView.SelectMulti(new object[1]
    {
      (object) book.BookID
    }).Cast<IPeriodSetup>();
  }

  public IEnumerable<IPeriodSetup> FindFABookPeriodSetup(int? bookID, bool clearQueryCache = false)
  {
    return this.FindFABookPeriodSetup(this.FindFABook(bookID), clearQueryCache);
  }

  public FABookYear FindFirstFABookYear(
    int? bookID,
    int? organizationID,
    bool clearQueryCache = false,
    bool mergeCache = false)
  {
    FABookPeriodRepository.CheckNotNullIDContract(bookID, nameof (bookID));
    FABookPeriodRepository.CheckNotNullIDContract(organizationID, nameof (organizationID));
    return ((BqlCommand) new Select<FABookYear, Where<FABookYear.bookID, Equal<Required<FABook.bookID>>, And<FABookYear.organizationID, Equal<Required<PX.Objects.GL.DAC.Organization.organizationID>>>>, OrderBy<Asc<FABookYear.year>>>()).CreateView(this.Graph, clearQueryCache, mergeCache).SelectSingle(new object[2]
    {
      (object) bookID,
      (object) organizationID
    }) as FABookYear;
  }

  public FABookYear FindLastFABookYear(
    int? bookID,
    int? organizationID,
    bool clearQueryCache = false,
    bool mergeCache = false)
  {
    FABookPeriodRepository.CheckNotNullIDContract(bookID, nameof (bookID));
    FABookPeriodRepository.CheckNotNullIDContract(organizationID, nameof (organizationID));
    return ((BqlCommand) new Select<FABookYear, Where<FABookYear.bookID, Equal<Required<FABook.bookID>>, And<FABookYear.organizationID, Equal<Required<PX.Objects.GL.DAC.Organization.organizationID>>>>, OrderBy<Desc<FABookYear.year>>>()).CreateView(this.Graph, clearQueryCache, mergeCache).SelectSingle(new object[2]
    {
      (object) bookID,
      (object) organizationID
    }) as FABookYear;
  }

  public FABookYear FindMasterFABookYearByID(
    FABook book,
    string yearNumber,
    bool clearQueryCache = false,
    bool mergeCache = false)
  {
    FABookPeriodRepository.CheckNotNullObjectContract((object) book, nameof (book));
    FABookPeriodRepository.CheckNotNullOrEmptyStringContract(yearNumber, nameof (yearNumber));
    return ((BqlCommand) new Select<FABookYear, Where<FABookYear.bookID, Equal<Required<FABook.bookID>>, And<FABookYear.organizationID, Equal<FinPeriod.organizationID.masterValue>, And<FABookYear.year, Equal<Required<FABookYear.year>>>>>>()).CreateView(this.Graph, clearQueryCache, mergeCache).SelectSingle(new object[2]
    {
      (object) book.BookID,
      (object) yearNumber
    }) as FABookYear;
  }

  public FABookPeriod FindMasterFABookPeriodByID(
    FABook book,
    string periodID,
    bool clearQueryCache = false,
    bool mergeCache = false)
  {
    FABookPeriodRepository.CheckNotNullObjectContract((object) book, nameof (book));
    return this.FindMasterFABookPeriodByID(book.BookID, periodID, clearQueryCache, mergeCache);
  }

  protected FABookPeriod FindMasterFABookPeriodByID(
    int? bookID,
    string periodID,
    bool clearQueryCache = false,
    bool mergeCache = false)
  {
    FABookPeriodRepository.CheckNotNullObjectContract((object) bookID, nameof (bookID));
    FABookPeriodRepository.CheckNotNullOrEmptyStringContract(periodID, nameof (periodID));
    return ((BqlCommand) new Select<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABook.bookID>>, And<FABookPeriod.organizationID, Equal<FinPeriod.organizationID.masterValue>, And<FABookPeriod.finPeriodID, Equal<Required<FABookPeriod.finPeriodID>>>>>>()).CreateView(this.Graph, clearQueryCache, mergeCache).SelectSingle(new object[2]
    {
      (object) bookID,
      (object) periodID
    }) as FABookPeriod;
  }

  public FABookPeriod FindNextNonAdjustmentMasterFABookPeriod(
    FABook book,
    string prevFABookPeriodID,
    bool clearQueryCache = false,
    bool mergeCache = false)
  {
    FABookPeriodRepository.CheckNotNullObjectContract((object) book, nameof (book));
    FABookPeriodRepository.CheckNotNullOrEmptyStringContract(prevFABookPeriodID, nameof (prevFABookPeriodID));
    return ((BqlCommand) new Select<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABook.bookID>>, And<FABookPeriod.organizationID, Equal<FinPeriod.organizationID.masterValue>, And<FABookPeriod.finPeriodID, Greater<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>>>>>, OrderBy<Asc<FABookPeriod.finPeriodID>>>()).CreateView(this.Graph, clearQueryCache, mergeCache).SelectSingle(new object[2]
    {
      (object) book.BookID,
      (object) prevFABookPeriodID
    }) as FABookPeriod;
  }

  public FABookPeriod FindLastNonAdjustmentOrganizationFABookPeriodOfYear(
    FAOrganizationBook book,
    string finYear,
    bool clearQueryCache = false,
    bool mergeCache = false)
  {
    FABookPeriodRepository.CheckNotNullObjectContract((object) book, nameof (book));
    FABookPeriodRepository.CheckNotNullOrEmptyStringContract(finYear, nameof (finYear));
    return ((BqlCommand) new Select<FABookPeriod, Where<FABookPeriod.finYear, Equal<Required<FABookPeriod.finYear>>, And<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>>>>>, OrderBy<Desc<FABookPeriod.finPeriodID>>>()).CreateView(this.Graph, clearQueryCache, mergeCache).SelectSingle(new object[3]
    {
      (object) finYear,
      (object) book.BookID,
      (object) book.OrganizationID
    }) as FABookPeriod;
  }

  public FABookPeriod FindFABookPeriodOfDate(
    DateTime? date,
    int? bookID,
    int? assetID,
    bool check = true)
  {
    if (check)
    {
      FABookPeriodRepository.CheckNotNullObjectContract((object) date, nameof (date));
      FABookPeriodRepository.CheckNotNullIDContract(bookID, nameof (bookID));
      FABookPeriodRepository.CheckNotNullIDContract(assetID, nameof (assetID));
    }
    FABookPeriod faBookPeriod = (FABookPeriod) ((BqlCommand) new Select<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.startDate, LessEqual<Required<FABookPeriod.startDate>>, And<FABookPeriod.endDate, Greater<Required<FABookPeriod.endDate>>>>>>>()).CreateView(this.Graph).SelectSingle(new object[4]
    {
      (object) bookID,
      (object) this.GetFABookPeriodOrganizationID(bookID, assetID, check),
      (object) date,
      (object) date
    });
    return !check || faBookPeriod != null ? faBookPeriod : throw new PXFABookPeriodException();
  }

  public FABookPeriod FindFABookPeriodOfDateByBranchID(
    DateTime? date,
    int? bookID,
    int? branchID,
    bool check = true)
  {
    FABookPeriodRepository.CheckNotNullObjectContract((object) date, nameof (date));
    FABookPeriodRepository.CheckNotNullIDContract(bookID, nameof (bookID));
    FABookPeriodRepository.CheckNotNullIDContract(branchID, nameof (branchID));
    FABookPeriod faBookPeriod = PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.startDate, LessEqual<Required<FABookPeriod.startDate>>, And<FABookPeriod.endDate, Greater<Required<FABookPeriod.endDate>>>>>>>.Config>.Select(this.Graph, new object[4]
    {
      (object) bookID,
      (object) (this.IsPostingFABook(bookID) ? PXAccess.GetParentOrganizationID(branchID) : new int?(0)),
      (object) date,
      (object) date
    }));
    return !check || faBookPeriod != null ? faBookPeriod : throw new PXFABookPeriodException();
  }

  public string GetFABookPeriodIDOfDate(DateTime? date, int? bookID, int? assetID, bool check = true)
  {
    if (check)
    {
      FABookPeriodRepository.CheckNotNullObjectContract((object) date, nameof (date));
      FABookPeriodRepository.CheckNotNullIDContract(bookID, nameof (bookID));
      FABookPeriodRepository.CheckNotNullIDContract(assetID, nameof (assetID));
    }
    return this.FindFABookPeriodOfDate(date, bookID, assetID, check)?.FinPeriodID;
  }

  public short GetQuarterNumberOfDate(DateTime? date, int? bookID, int? assetID)
  {
    FABookPeriodRepository.CheckNotNullObjectContract((object) date, nameof (date));
    FABookPeriodRepository.CheckNotNullIDContract(bookID, nameof (bookID));
    FABookPeriodRepository.CheckNotNullIDContract(assetID, nameof (assetID));
    Decimal quarterNumberOfDate = Convert.ToDecimal(this.FindFABookPeriodOfDate(date, bookID, assetID, true).PeriodNbr);
    switch (this.FindFABookYearSetup(bookID, false).FPType)
    {
      case FiscalPeriodSetupCreator.FPType.Month:
        return (short) Decimal.Ceiling(quarterNumberOfDate / 3M);
      case FiscalPeriodSetupCreator.FPType.Quarter:
        return (short) quarterNumberOfDate;
      default:
        throw new PXException("Quarter is defined only for the 'Month' and 'Quarter' types of period.");
    }
  }

  public short GetPeriodNumberOfDate(DateTime? date, int? bookID, int? assetID)
  {
    return Convert.ToInt16(this.FindFABookPeriodOfDate(date, bookID, assetID, true).PeriodNbr);
  }

  public int GetYearNumberOfDate(DateTime? date, int? bookID, int? assetID)
  {
    return Convert.ToInt32(this.FindFABookPeriodOfDate(date, bookID, assetID, true).FinYear);
  }

  public FABookPeriod FindOrganizationFABookPeriodByID(
    string periodID,
    int? bookID,
    int? assetID,
    bool clearQueryCache = false,
    bool mergeCache = false)
  {
    return ((BqlCommand) new Select<FABookPeriod, Where<FABookPeriod.finPeriodID, Equal<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>>>>>()).CreateView(this.Graph, clearQueryCache, mergeCache).SelectSingle(new object[3]
    {
      (object) periodID,
      (object) bookID,
      (object) this.GetFABookPeriodOrganizationID(bookID, assetID, true)
    }) as FABookPeriod;
  }

  public FABookPeriod FindMappedPeriod(FABookPeriod.Key fromPeriodKey, FABookPeriod.Key toPeriodKey)
  {
    int? bookId1 = fromPeriodKey.BookID;
    int? bookId2 = toPeriodKey.BookID;
    if (!(bookId1.GetValueOrDefault() == bookId2.GetValueOrDefault() & bookId1.HasValue == bookId2.HasValue))
      return (FABookPeriod) null;
    if (!this.IsPostingFABook(fromPeriodKey.BookID))
      return (FABookPeriod) null;
    FABookPeriod byKey = this.FindByKey(fromPeriodKey.BookID, fromPeriodKey.OrganizationID, fromPeriodKey.PeriodID);
    return this.FindPeriodByMasterPeriodID(toPeriodKey.BookID, toPeriodKey.OrganizationID, byKey?.MasterFinPeriodID);
  }

  public FABookPeriod FindByKey(int? bookID, int? organizaionID, string periodID)
  {
    return PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.finPeriodID, Equal<Required<FABookPeriod.finPeriodID>>>>>>.Config>.Select(this.Graph, new object[3]
    {
      (object) bookID,
      (object) organizaionID,
      (object) periodID
    }));
  }

  public FABookPeriod FindPeriodByMasterPeriodID(
    int? bookID,
    int? organizaionID,
    string masterPeriodID)
  {
    return PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.masterFinPeriodID, Equal<Required<FABookPeriod.masterFinPeriodID>>>>>>.Config>.Select(this.Graph, new object[3]
    {
      (object) bookID,
      (object) organizaionID,
      (object) masterPeriodID
    }));
  }

  public virtual FABookPeriod GetMappedFABookPeriod(
    int? bookID,
    int? sourceOrganizationID,
    string sourcefinPeriodID,
    int? targetOrganizationID)
  {
    return this.FindMappedFABookPeriod(bookID, sourceOrganizationID, sourcefinPeriodID, targetOrganizationID).ThisOrRaiseIfHasError().Result;
  }

  public virtual ProcessingResult<FABookPeriod> FindMappedFABookPeriod(
    int? bookID,
    int? sourceOrganizationID,
    string sourcefinPeriodID,
    int? targetOrganizationID)
  {
    FABookPeriod byKey = this.FindByKey(bookID, sourceOrganizationID, sourcefinPeriodID);
    ProcessingResult<FABookPeriod> mappedFaBookPeriod = ProcessingResultBase<ProcessingResult<FABookPeriod>, FABookPeriod, ProcessingResultMessage>.CreateSuccess(byKey);
    if (byKey == null)
    {
      string message = PXMessages.LocalizeFormat("The {0} period does not exist for the {1} book and the {2} company.", new object[3]
      {
        (object) PeriodIDAttribute.FormatForError(sourcefinPeriodID),
        (object) this.FindFABook(bookID).BookCode,
        (object) PXAccess.GetOrganizationCD(sourceOrganizationID)
      });
      mappedFaBookPeriod.AddErrorMessage(message);
    }
    else if (this.IsPostingFABook(bookID))
    {
      int? nullable1 = sourceOrganizationID;
      int? nullable2 = targetOrganizationID;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        mappedFaBookPeriod = this.GetFABookPeriodByMasterPeriodID(bookID, targetOrganizationID, byKey?.MasterFinPeriodID);
    }
    return mappedFaBookPeriod;
  }

  public virtual ProcessingResult<FABookPeriod> FindMappedFABookPeriodUsingFinPeriod(
    int? bookID,
    int? sourceOrganizationID,
    string sourcefinPeriodID,
    int? targetOrganizationID)
  {
    string finPeriodId = this.Graph.GetService<IFinPeriodRepository>().GetMappedPeriod(sourceOrganizationID, sourcefinPeriodID, targetOrganizationID)?.FinPeriodID;
    FABookPeriod byKey = this.FindByKey(bookID, targetOrganizationID, finPeriodId);
    ProcessingResult<FABookPeriod> success = ProcessingResultBase<ProcessingResult<FABookPeriod>, FABookPeriod, ProcessingResultMessage>.CreateSuccess(byKey);
    if (byKey == null)
    {
      string message = PXMessages.LocalizeFormat("The {0} period does not exist for the {1} book and the {2} company.", new object[3]
      {
        (object) PeriodIDAttribute.FormatForError(sourcefinPeriodID),
        (object) this.FindFABook(bookID).BookCode,
        (object) PXAccess.GetOrganizationCD(sourceOrganizationID)
      });
      success.AddErrorMessage(message);
    }
    return success;
  }

  public virtual FABookPeriod GetMappedFABookPeriodByBranches(
    int? bookID,
    int? sourceBranchID,
    string sourcefinPeriodID,
    int? targetBranchID)
  {
    return this.GetMappedFABookPeriod(bookID, PXAccess.GetParentOrganizationID(sourceBranchID), sourcefinPeriodID, PXAccess.GetParentOrganizationID(targetBranchID));
  }

  public virtual ProcessingResult<FABookPeriod> GetFABookPeriodByMasterPeriodID(
    int? bookID,
    int? organizationID,
    string masterFinPeriodID)
  {
    FABookPeriod faBookPeriod = PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXViewOf<FABookPeriod>.BasedOn<SelectFromBase<FABookPeriod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.bookID, Equal<P.AsInt>>>>, And<BqlOperand<FABookPeriod.organizationID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<FABookPeriod.masterFinPeriodID, IBqlString>.IsEqual<P.AsString>>>>.ReadOnly.Config>.Select(this.Graph, new object[3]
    {
      (object) bookID,
      (object) organizationID,
      (object) masterFinPeriodID
    }));
    ProcessingResult<FABookPeriod> success = ProcessingResultBase<ProcessingResult<FABookPeriod>, FABookPeriod, ProcessingResultMessage>.CreateSuccess(faBookPeriod);
    if (faBookPeriod == null)
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
}

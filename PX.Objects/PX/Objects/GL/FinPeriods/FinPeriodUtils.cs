// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriods.FinPeriodUtils
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.FA;
using PX.Objects.GL.DAC.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.FinPeriods;

public class FinPeriodUtils : IFinPeriodUtils
{
  public const int YEAR_LENGTH = 4;
  public const int PERIOD_LENGTH = 2;
  public const int FULL_LENGHT = 6;
  public const string FirstPeriodOfYear = "01";
  public const int EarliestYear = 1901;
  private const string FirstPeriodNumber = "01";

  protected PXGraph Graph { get; set; }

  public FinPeriodUtils(PXGraph graph) => this.Graph = graph;

  /// <summary>
  /// Format Period to string that can be used in an error message.
  /// </summary>
  public static string FormatForError(string period)
  {
    return FinPeriodIDFormattingAttribute.FormatForError(period);
  }

  /// <summary>
  /// Format Period to string that can be displayed in the control.
  /// </summary>
  /// <param name="period">Period in database format</param>
  public static string FormatForDisplay(string period)
  {
    return FinPeriodIDFormattingAttribute.FormatForDisplay(period);
  }

  /// <summary>Format period to database format</summary>
  /// <param name="period">Period in display format</param>
  /// <returns></returns>
  public static string UnFormatPeriod(string period)
  {
    return FinPeriodIDFormattingAttribute.FormatForStoring(period);
  }

  public static string GetFirstFinPeriodIDOfYear(IYear year) => year.Year + "01";

  public static string GetFirstFinPeriodIDOfYear(string finPeriodID)
  {
    return finPeriodID.Substring(0, 4) + "01";
  }

  public static string GetYearIDOfPeriod(string periodID) => periodID.Substring(0, 4);

  public static string GetNextYearID(string finPeriodId)
  {
    return $"{Convert.ToInt32(finPeriodId.Substring(0, 4)) + 1}";
  }

  public static string GetPreviousYearID(string finPeriodId)
  {
    return $"{Convert.ToInt32(finPeriodId.Substring(0, 4)) - 1}";
  }

  public static bool FinPeriodEqual(
    string period1,
    string period2,
    FinPeriodUtils.FinPeriodComparison comparison)
  {
    if (period1 != null && period2 != null && period1.Length >= 6 && period2.Length >= 6)
    {
      switch (comparison)
      {
        case FinPeriodUtils.FinPeriodComparison.Year:
          period1 = period1.Substring(0, 4);
          period2 = period2.Substring(0, 4);
          break;
        case FinPeriodUtils.FinPeriodComparison.Month:
          period1 = period1.Substring(4, 2);
          period2 = period2.Substring(4, 2);
          break;
      }
    }
    return string.Equals(period1, period2);
  }

  public static string FiscalYear(string aFiscalPeriod) => aFiscalPeriod.Substring(0, 4);

  public static string PeriodInYear(string aFiscalPeriod) => aFiscalPeriod.Substring(4, 2);

  /// <summary>
  /// Attempts to extract integer values of the financial year and the number of the financial period inside the year
  /// from a Financial Period ID (<see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod.FinPeriodID" />).
  /// </summary>
  /// <param name="fiscalPeriodID">The ID of the period to extract parts from</param>
  /// <param name="year">Output: the financial year, to which the period belongs</param>
  /// <param name="periodNbr">Output: the number of the period in its financial year</param>
  /// <returns><c>true</c> upon success or <c>false</c> if failed to parse due to incorrect format of the input period ID</returns>
  public static bool TryParse(string fiscalPeriodID, out int year, out int periodNbr)
  {
    try
    {
      year = int.Parse(FinPeriodUtils.FiscalYear(fiscalPeriodID));
      periodNbr = int.Parse(FinPeriodUtils.PeriodInYear(fiscalPeriodID));
    }
    catch (FormatException ex)
    {
      year = -1;
      periodNbr = -1;
      return false;
    }
    return true;
  }

  public static string Assemble(string aYear, string aPeriod)
  {
    if (aYear.Length != 4 || aPeriod.Length != 2)
      throw new PXArgumentException((string) null, "Invalid Year or Period format.");
    return aYear + aPeriod;
  }

  public static string OffsetPeriod(string FiscalPeriodID, int counter, int periodsInYear)
  {
    int int32_1 = Convert.ToInt32(FiscalPeriodID.Substring(0, 4));
    int int32_2 = Convert.ToInt32(FiscalPeriodID.Substring(4, 2));
    int num1;
    int num2;
    return $"{int32_1 - 1 + (int) Decimal.Ceiling((Decimal) (int32_2 + counter) / (Decimal) periodsInYear):0000}{(int32_2 + counter <= 0 ? ((num2 = (int32_2 + counter) % periodsInYear) == 0 ? periodsInYear : periodsInYear + num2) : ((num1 = (int32_2 + counter) % periodsInYear) == 0 ? periodsInYear : num1)):00}";
  }

  public static string Min(string period1, string period2)
  {
    return string.CompareOrdinal(period1, period2) <= 0 ? period1 : period2;
  }

  public static string Max(string period1, string period2)
  {
    return string.CompareOrdinal(period1, period2) <= 0 ? period2 : period1;
  }

  public static DateTime Max(DateTime date1, DateTime date2)
  {
    return DateTime.Compare(date1, date2) <= 0 ? date2 : date1;
  }

  public static DateTime? Max(DateTime? date1, DateTime? date2)
  {
    if (date1.HasValue && !date2.HasValue)
      return date1;
    if (!date1.HasValue && date2.HasValue)
      return date2;
    if (!date1.HasValue && !date2.HasValue)
      return new DateTime?();
    return DateTime.Compare(date1.Value, date2.Value) <= 0 ? date2 : date1;
  }

  public virtual void VerifyAndSetFirstOpenedFinPeriod<TFinPeriodField, TBranchField>(
    PXCache rowCache,
    object row,
    PXSelectBase<OrganizationFinPeriod> finPeriodView,
    Type fieldModuleClosed = null)
    where TFinPeriodField : class, IBqlField
    where TBranchField : class, IBqlField
  {
    OrganizationFinPeriod current = finPeriodView.Current;
    if (current == null)
      return;
    bool flag = current.Status == "Closed";
    if (fieldModuleClosed != (Type) null)
      flag |= ((bool?) ((PXSelectBase) finPeriodView).Cache.GetValue((object) current, fieldModuleClosed.Name)).GetValueOrDefault();
    bool closedPeriod = this.CanPostToClosedPeriod();
    if (!(current.Status == "Inactive") && !(current.Status == "Locked") && (!flag || closedPeriod))
      return;
    string fromFinPeriodID = (string) rowCache.GetValue<TFinPeriodField>(row);
    int? parentOrganizationId = PXAccess.GetParentOrganizationID((int?) rowCache.GetValue<TBranchField>(row));
    rowCache.SetValue<TFinPeriodField>(row, (object) (rowCache.Graph.GetService<IFinPeriodRepository>().FindFirstOpenFinPeriod(fromFinPeriodID, parentOrganizationId, fieldModuleClosed) ?? throw new PXSetPropertyException("Operation cannot be performed. No active or open periods are available in the system starting from {0}.", new object[1]
    {
      (object) Mask.Format("##-####", ((PXSelectBase) finPeriodView).Cache.GetValueExt<OrganizationFinPeriod.finPeriodID>((object) finPeriodView.Current).ToString())
    })).FinPeriodID);
  }

  public virtual void ValidateFinPeriod(IEnumerable<IAccountable> records, Type fieldModuleClosed = null)
  {
    IEnumerable<IGrouping<string, IAccountable>> groupings = records.AsEnumerable<IAccountable>().GroupBy<IAccountable, string>((Func<IAccountable, string>) (record => record.FinPeriodID));
    ProcessingResult generalResult = new ProcessingResult();
    foreach (IGrouping<string, IAccountable> source in groupings)
    {
      int?[] array = source.GroupBy<IAccountable, int?>((Func<IAccountable, int?>) (t => PXAccess.GetParentOrganizationID(t.BranchID))).Select<IGrouping<int?, IAccountable>, int?>((Func<IGrouping<int?, IAccountable>, int?>) (g => g.Key)).ToArray<int?>();
      this.ValidateFinPeriod(source.Key, array, fieldModuleClosed, generalResult);
    }
    generalResult.RaiseIfHasError();
  }

  public void ValidateFinPeriod<T>(
    IEnumerable<T> records,
    Func<T, string> getFinPeriodID,
    Func<T, int?[]> getBranchIDs,
    Type fieldModuleClosed = null)
  {
    IEnumerable<IGrouping<string, T>> groupings = records.AsEnumerable<T>().GroupBy<T, string>((Func<T, string>) (record => getFinPeriodID(record)));
    ProcessingResult generalResult = new ProcessingResult();
    foreach (IGrouping<string, T> source in groupings)
    {
      int?[] array = source.SelectMany<T, int?>((Func<T, IEnumerable<int?>>) (record => ((IEnumerable<int?>) getBranchIDs(record)).Select<int?, int?>(new Func<int?, int?>(PXAccess.GetParentOrganizationID)))).Distinct<int?>().ToArray<int?>();
      this.ValidateFinPeriod(source.Key, array, fieldModuleClosed, generalResult);
    }
    generalResult.RaiseIfHasError();
  }

  protected virtual void ValidateFinPeriod(
    string finPeriodID,
    int?[] orgnizationIDs,
    Type fieldModuleClosed = null,
    ProcessingResult generalResult = null)
  {
    if (generalResult == null)
      generalResult = new ProcessingResult();
    ICollection<OrganizationFinPeriod> array1 = (ICollection<OrganizationFinPeriod>) GraphHelper.RowCast<OrganizationFinPeriod>((IEnumerable) PXSelectBase<OrganizationFinPeriod, PXSelect<OrganizationFinPeriod, Where<OrganizationFinPeriod.organizationID, In<Required<OrganizationFinPeriod.organizationID>>, And<OrganizationFinPeriod.finPeriodID, Equal<Required<OrganizationFinPeriod.finPeriodID>>>>>.Config>.Select(this.Graph, new object[2]
    {
      (object) orgnizationIDs,
      (object) finPeriodID
    })).ToArray<OrganizationFinPeriod>();
    if (array1.Count != orgnizationIDs.Length)
    {
      string[] array2 = ((IEnumerable<int?>) orgnizationIDs).Except<int?>(array1.Select<OrganizationFinPeriod, int?>((Func<OrganizationFinPeriod, int?>) (period => period.OrganizationID))).Select<int?, string>(new Func<int?, string>(PXAccess.GetOrganizationCD)).ToArray<string>();
      generalResult.AddErrorMessage("The {0} financial period does not exist for the following companies: {1}.", (object) FinPeriodIDFormattingAttribute.FormatForError(finPeriodID), (object) ((ICollection<string>) array2).JoinIntoStringForMessageNoQuotes<string>(20));
    }
    foreach (IFinPeriod finPeriod in (IEnumerable<OrganizationFinPeriod>) array1)
    {
      ProcessingResult period = this.CanPostToPeriod(finPeriod, fieldModuleClosed);
      generalResult.Aggregate((ProcessingResultBase<ProcessingResult, object, ProcessingResultMessage>) period);
      if (generalResult.Messages.Count > 20)
        generalResult.RaiseIfHasError();
    }
  }

  public void ValidateMasterFinPeriod<T>(
    IEnumerable<T> records,
    Func<T, string> getMasterFinPeriodID,
    Func<T, int?[]> getBranchIDs,
    Type fieldModuleClosed = null)
  {
    IEnumerable<IGrouping<string, T>> groupings = records.GroupBy<T, string>((Func<T, string>) (record => getMasterFinPeriodID(record)));
    ProcessingResult generalResult = new ProcessingResult();
    foreach (IGrouping<string, T> source in groupings)
    {
      int?[] array = source.SelectMany<T, int?>((Func<T, IEnumerable<int?>>) (record => ((IEnumerable<int?>) getBranchIDs(record)).Select<int?, int?>(new Func<int?, int?>(PXAccess.GetParentOrganizationID)))).Distinct<int?>().ToArray<int?>();
      this.ValidateMasterFinPeriod(source.Key, array, fieldModuleClosed, generalResult);
    }
    generalResult.RaiseIfHasError();
  }

  protected virtual void ValidateMasterFinPeriod(
    string masterFinPeriodID,
    int?[] orgnizationIDs,
    Type fieldModuleClosed = null,
    ProcessingResult generalResult = null)
  {
    if (generalResult == null)
      generalResult = new ProcessingResult();
    ICollection<OrganizationFinPeriod> array1 = (ICollection<OrganizationFinPeriod>) GraphHelper.RowCast<OrganizationFinPeriod>((IEnumerable) PXSelectBase<OrganizationFinPeriod, PXSelect<OrganizationFinPeriod, Where<OrganizationFinPeriod.organizationID, In<Required<OrganizationFinPeriod.organizationID>>, And<OrganizationFinPeriod.masterFinPeriodID, Equal<Required<OrganizationFinPeriod.masterFinPeriodID>>>>>.Config>.Select(this.Graph, new object[2]
    {
      (object) orgnizationIDs,
      (object) masterFinPeriodID
    })).ToArray<OrganizationFinPeriod>();
    if (array1.Count != orgnizationIDs.Length)
    {
      string[] array2 = ((IEnumerable<int?>) orgnizationIDs).Except<int?>(array1.Select<OrganizationFinPeriod, int?>((Func<OrganizationFinPeriod, int?>) (period => period.OrganizationID))).Select<int?, string>(new Func<int?, string>(PXAccess.GetOrganizationCD)).ToArray<string>();
      generalResult.AddErrorMessage("The related financial periods for the {0} master period do not exist in the {1} companies.", (object) FinPeriodIDFormattingAttribute.FormatForError(masterFinPeriodID), (object) ((ICollection<string>) array2).JoinIntoStringForMessageNoQuotes<string>(20));
    }
    foreach (IFinPeriod finPeriod in (IEnumerable<OrganizationFinPeriod>) array1)
    {
      ProcessingResult period = this.CanPostToPeriod(finPeriod, fieldModuleClosed);
      generalResult.Aggregate((ProcessingResultBase<ProcessingResult, object, ProcessingResultMessage>) period);
      if (generalResult.Messages.Count > 20)
        generalResult.RaiseIfHasError();
    }
  }

  public virtual ProcessingResult CanPostToPeriod(IFinPeriod finPeriod, Type fieldModuleClosed = null)
  {
    ProcessingResult period = new ProcessingResult();
    if (finPeriod.Status == "Locked")
    {
      period.AddErrorMessage("The {0} financial period is locked in the {1} company.", (object) FinPeriodIDFormattingAttribute.FormatForError(finPeriod.FinPeriodID), (object) PXAccess.GetOrganizationCD(finPeriod.OrganizationID));
      return period;
    }
    if (this.AllowPostToUnlockedPeriodAnyway)
      return period;
    if (finPeriod.Status == "Inactive")
    {
      period.AddErrorMessage("The {0} financial period is inactive in the {1} company.", (object) FinPeriodIDFormattingAttribute.FormatForError(finPeriod.FinPeriodID), (object) PXAccess.GetOrganizationCD(finPeriod.OrganizationID));
      return period;
    }
    if (finPeriod.Status == "Closed")
    {
      period = this.HandleErrorThatPeriodIsClosed(finPeriod);
      if (period.HasWarningOrError)
        return period;
    }
    if (fieldModuleClosed != (Type) null && new bool?((bool) this.Graph.Caches[BqlCommand.GetItemType(fieldModuleClosed)].GetValue((object) finPeriod, fieldModuleClosed.Name)).GetValueOrDefault())
      period = this.HandleErrorThatPeriodIsClosed(finPeriod, this.GetMessageForModule(fieldModuleClosed.Name));
    return period;
  }

  private string GetMessageForModule(string module)
  {
    switch (module)
    {
      case "iNClosed":
        return "The {0} financial period of the {1} company is closed in Inventory.";
      case "aRClosed":
        return "The {0} financial period of the {1} company is closed in Accounts Receivable.";
      case "aPClosed":
        return "The {0} financial period of the {1} company is closed in  Accounts Payable.";
      case "cAClosed":
        return "The {0} financial period of the {1} company is closed in Cash Management.";
      default:
        return "The {0} financial period is closed in the {1} company.";
    }
  }

  public bool CanPostToClosedPeriod()
  {
    if (this.AllowPostToUnlockedPeriodAnyway || !((PXSelectBase<GLSetup>) new PXSetup<GLSetup>(this.Graph)).Current.RestrictAccessToClosedPeriods.GetValueOrDefault())
      return true;
    return !string.IsNullOrEmpty(PredefinedRoles.FinancialSupervisor) && PXContext.PXIdentity.User.IsInRole(PredefinedRoles.FinancialSupervisor);
  }

  public bool AllowPostToUnlockedPeriodAnyway
  {
    get => PXContext.GetSlot<bool>("FinPeriodUtils.AllowPostToUnlockedPeriod");
    set => PXContext.SetSlot<bool>("FinPeriodUtils.AllowPostToUnlockedPeriod", value);
  }

  protected virtual ProcessingResult HandleErrorThatPeriodIsClosed(IFinPeriod finPeriod)
  {
    return this.HandleErrorThatPeriodIsClosed(finPeriod, "The {0} financial period is closed in the {1} company.");
  }

  private ProcessingResult HandleErrorThatPeriodIsClosed(IFinPeriod finPeriod, string message)
  {
    ProcessingResult processingResult = new ProcessingResult();
    if (!this.CanPostToClosedPeriod())
      processingResult.AddErrorMessage(message, (object) FinPeriodIDFormattingAttribute.FormatForError(finPeriod.FinPeriodID), (object) PXAccess.GetOrganizationCD(finPeriod.OrganizationID));
    return processingResult;
  }

  public virtual OrganizationFinPeriod GetOpenOrganizationFinPeriodInSubledger<TClosedInSubledgerField>(
    string orgFinPeriodID,
    int? branchID)
    where TClosedInSubledgerField : IBqlField
  {
    int? parentOrganizationId = PXAccess.GetParentOrganizationID(branchID);
    return PXResultset<OrganizationFinPeriod>.op_Implicit(PXSelectBase<OrganizationFinPeriod, PXSelect<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, Equal<Required<OrganizationFinPeriod.finPeriodID>>, And<OrganizationFinPeriod.organizationID, Equal<Required<OrganizationFinPeriod.organizationID>>, And<TClosedInSubledgerField, NotEqual<True>>>>>.Config>.SelectWindowed(this.Graph, 0, 1, new object[2]
    {
      (object) orgFinPeriodID,
      (object) parentOrganizationId
    })) ?? throw new PXException("The {0} financial period is closed in the {1} company.", new object[2]
    {
      (object) FinPeriodIDFormattingAttribute.FormatForError(orgFinPeriodID),
      (object) PXAccess.GetOrganizationCD(parentOrganizationId)
    });
  }

  protected virtual OrganizationFinPeriod GetNearestOpenOrganizationFinPeriodInSubledgerByOrganization<TClosedInSubledgerField>(
    string orgFinPeriodID,
    int? organizationID,
    Func<bool> additionalCondition = null)
    where TClosedInSubledgerField : IBqlField
  {
    OrganizationFinPeriod organizationFinPeriod = PXResultset<OrganizationFinPeriod>.op_Implicit(PXSelectBase<OrganizationFinPeriod, PXSelect<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, GreaterEqual<Required<OrganizationFinPeriod.finPeriodID>>, And<OrganizationFinPeriod.organizationID, Equal<Required<OrganizationFinPeriod.organizationID>>, And<TClosedInSubledgerField, NotEqual<True>, And<OrganizationFinPeriod.startDate, NotEqual<OrganizationFinPeriod.endDate>>>>>, OrderBy<Asc<OrganizationFinPeriod.finPeriodID>>>.Config>.SelectWindowed(this.Graph, 0, 1, new object[2]
    {
      (object) orgFinPeriodID,
      (object) organizationID
    }));
    return organizationFinPeriod != null || additionalCondition != null && !additionalCondition() ? organizationFinPeriod : throw new PXException("No open financial periods exist in the {0} company.", new object[1]
    {
      (object) PXAccess.GetOrganizationCD(organizationID)
    });
  }

  public virtual OrganizationFinPeriod GetNearestOpenOrganizationFinPeriodInSubledger<TClosedInSubledgerField>(
    string orgFinPeriodID,
    int? branchID,
    Func<bool> additionalCondition = null)
    where TClosedInSubledgerField : IBqlField
  {
    int? parentOrganizationId = PXAccess.GetParentOrganizationID(branchID);
    return this.GetNearestOpenOrganizationFinPeriodInSubledgerByOrganization<TClosedInSubledgerField>(orgFinPeriodID, parentOrganizationId, additionalCondition);
  }

  public virtual OrganizationFinPeriod GetNearestOpenOrganizationFinPeriodInSubledger<TClosedInSubledgerField>(
    IPeriod orgFinPeriod)
    where TClosedInSubledgerField : IBqlField
  {
    return this.GetNearestOpenOrganizationFinPeriodInSubledgerByOrganization<TClosedInSubledgerField>(orgFinPeriod.FinPeriodID, orgFinPeriod.OrganizationID);
  }

  public virtual OrganizationFinPeriod GetOpenOrganizationFinPeriodInFA(
    string orgFinPeriodID,
    int? assetID)
  {
    return PXResultset<OrganizationFinPeriod>.op_Implicit(PXSelectBase<OrganizationFinPeriod, PXSelectJoin<OrganizationFinPeriod, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.organizationID, Equal<OrganizationFinPeriod.organizationID>>, InnerJoin<FixedAsset, On<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>, And<FixedAsset.branchID, Equal<PX.Objects.GL.Branch.branchID>>>>>, Where<OrganizationFinPeriod.finPeriodID, Equal<Required<OrganizationFinPeriod.finPeriodID>>, And<OrganizationFinPeriod.fAClosed, NotEqual<True>>>>.Config>.SelectWindowed(this.Graph, 0, 1, new object[2]
    {
      (object) assetID,
      (object) orgFinPeriodID
    })) ?? throw new PXException("The {0} financial period is closed.", new object[1]
    {
      (object) FinPeriodIDFormattingAttribute.FormatForError(orgFinPeriodID)
    });
  }

  public string ComposeFinPeriodID(string yearNumber, string periodNumber)
  {
    return $"{yearNumber:0000}{periodNumber:00}";
  }

  public (string yearNumber, string periodNumber) ParseFinPeriodID(string finPeriodID)
  {
    return (finPeriodID.Substring(0, 4), finPeriodID.Substring(4, 2));
  }

  public (int firstYear, int lastYear) GetFirstLastYearForGeneration(
    int? organizationID,
    int fromYear,
    int toYear,
    bool clearQueryCache = false)
  {
    IFinPeriodRepository service = this.Graph.GetService<IFinPeriodRepository>();
    int result1;
    int? nullable1 = int.TryParse(service.FindFirstYear(new int?(organizationID.GetValueOrDefault()), clearQueryCache)?.Year, out result1) ? new int?(result1) : new int?();
    int result2;
    int? nullable2 = int.TryParse(service.FindLastYear(new int?(organizationID.GetValueOrDefault()), clearQueryCache)?.Year, out result2) ? new int?(result2) : new int?();
    int num = nullable2 ?? fromYear - 1;
    nullable2 = nullable1;
    return (nullable2 ?? fromYear - 1, num);
  }

  public void CheckParametersOfCalendarGeneration(int? organizationID, int fromYear, int toYear)
  {
    (int firstYear, int lastYear) = this.GetFirstLastYearForGeneration(organizationID, fromYear, toYear, false);
    if (fromYear > lastYear + 1)
      throw new PXException("The periods cannot be generated because the value in the From Year box is greater than {0}.", new object[1]
      {
        (object) (lastYear + 1)
      });
    if (toYear < firstYear - 1)
      throw new PXException("The periods cannot be generated because the value in the To Year box is less than {0}.", new object[1]
      {
        (object) (firstYear - 1)
      });
    if (toYear - fromYear + 1 > 99)
      throw new PXException("You cannot generate periods for more than 99 years at a time.");
  }

  public virtual void CopyPeriods<TDAC, TFinPeriodID, TMasterFinPeriodID>(
    PXCache cache,
    TDAC src,
    TDAC dest)
    where TDAC : class, IBqlTable, new()
    where TFinPeriodID : class, IBqlField
    where TMasterFinPeriodID : class, IBqlField
  {
    if (cache.ObjectsEqual<TFinPeriodID, TMasterFinPeriodID>((object) src, (object) dest))
      return;
    object obj = cache.GetValue<TFinPeriodID>((object) src);
    string masterFinPeriodID = (string) cache.GetValue<TMasterFinPeriodID>((object) src);
    try
    {
      FinPeriodIDAttribute.SetPeriodsByMaster<TFinPeriodID>(cache, (object) dest, masterFinPeriodID);
      obj = cache.GetValue<TFinPeriodID>((object) dest);
      cache.RaiseFieldVerifying<TFinPeriodID>((object) dest, ref obj);
    }
    catch (PXException ex)
    {
      cache.RaiseExceptionHandling<TFinPeriodID>((object) dest, obj, (Exception) ex);
    }
  }

  public virtual void CopyPeriods<TDAC, TSourceFinPeriodID, TSourceMasterFinPeriodID, TDestFinPeriodID, TDestMasterFinPeriodID>(
    PXCache cache,
    TDAC src,
    TDAC dest)
    where TDAC : class, IBqlTable, new()
    where TSourceFinPeriodID : class, IBqlField
    where TSourceMasterFinPeriodID : class, IBqlField
    where TDestFinPeriodID : class, IBqlField
    where TDestMasterFinPeriodID : class, IBqlField
  {
    object obj1 = cache.GetValue<TSourceFinPeriodID>((object) src);
    object obj2 = cache.GetValue<TSourceMasterFinPeriodID>((object) src);
    object obj3 = cache.GetValue<TDestFinPeriodID>((object) src);
    object obj4 = cache.GetValue<TDestMasterFinPeriodID>((object) src);
    object obj5 = obj3;
    if (obj1 == obj5 || obj2 == obj4)
      return;
    object obj6 = cache.GetValue<TSourceFinPeriodID>((object) src);
    string masterFinPeriodID = (string) cache.GetValue<TSourceMasterFinPeriodID>((object) src);
    try
    {
      FinPeriodIDAttribute.SetPeriodsByMaster<TDestFinPeriodID>(cache, (object) dest, masterFinPeriodID);
      obj6 = cache.GetValue<TSourceFinPeriodID>((object) dest);
      cache.RaiseFieldVerifying<TDestFinPeriodID>((object) dest, ref obj6);
    }
    catch (PXException ex)
    {
      cache.RaiseExceptionHandling<TDestFinPeriodID>((object) dest, obj6, (Exception) ex);
    }
  }

  public enum FinPeriodComparison
  {
    Full,
    Year,
    Month,
  }
}

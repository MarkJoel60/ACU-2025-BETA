// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods;
using PX.Objects.PR.Standalone;
using PX.Objects.TX;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.EP;

public class EPSetupMaint : PXGraph<
#nullable disable
EPSetupMaint>
{
  public PXSelect<EPSetup> Setup;
  public PXSelectReadonly<CMSetup> cmsetup;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  public PXSelect<WikiPageSimple, Where<WikiPageSimple.pageID, Equal<WikiPageSimple.pageID>>, OrderBy<Desc<WikiPage.articleType, Asc<WikiPage.number>>>> Articles;
  public PXFilter<EPSetupMaint.EPWeekFilter> WeekFilter;
  public PXSelect<EPCustomWeek, Where<EPCustomWeek.year, Equal<Current<EPSetupMaint.EPWeekFilter.year>>>> CustomWeek;
  public PXFilter<EPSetupMaint.EPGenerateWeeksDialog> GenerateWeeksDialog;
  private bool isGenerate;
  private const int _customWeekLimit = 99;
  public PXAction<EPSetup> generateWeeks;
  public PXAction<EPSetup> generateWeeksOk;
  public PXSave<EPSetup> Save;
  public PXCancel<EPSetup> Cancel;

  public EPSetupMaint()
  {
    if (((PXSelectBase<PX.Objects.AP.APSetup>) this.APSetup).Current == null)
      throw new PXArgumentException(nameof (APSetup));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<PX.Objects.IN.InventoryItem.stkItem>(EPSetupMaint.\u003C\u003Ec.\u003C\u003E9__9_0 ?? (EPSetupMaint.\u003C\u003Ec.\u003C\u003E9__9_0 = new PXFieldDefaulting((object) EPSetupMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__9_0))));
  }

  protected virtual IEnumerable articles(string PageID)
  {
    EPSetupMaint epSetupMaint1 = this;
    Guid? nullable = GUID.CreateGuid(PageID);
    Guid guid1 = nullable ?? Guid.Empty;
    EPSetupMaint epSetupMaint2 = epSetupMaint1;
    object[] objArray = new object[1]{ (object) guid1 };
    foreach (PXResult<WikiPageSimple> pxResult in PXSelectBase<WikiPageSimple, PXSelect<WikiPageSimple, Where<WikiPageSimple.parentUID, Equal<Required<WikiPageSimple.parentUID>>>>.Config>.Select((PXGraph) epSetupMaint2, objArray))
    {
      WikiPageSimple wikiPageSimple = PXResult<WikiPageSimple>.op_Implicit(pxResult);
      PXWikiProvider wikiProvider1 = PXSiteMap.WikiProvider;
      nullable = ((WikiPage) wikiPageSimple).PageID;
      Guid guid2 = nullable.Value;
      if (wikiProvider1.GetAccessRights(guid2) >= 1)
      {
        PXWikiProvider wikiProvider2 = PXSiteMap.WikiProvider;
        nullable = ((WikiPage) wikiPageSimple).PageID;
        Guid valueOrDefault = nullable.GetValueOrDefault();
        PXWikiMapNode siteMapNodeFromKey = (PXWikiMapNode) ((PXSiteMapProvider) wikiProvider2).FindSiteMapNodeFromKey(valueOrDefault);
        ((WikiPage) wikiPageSimple).Title = siteMapNodeFromKey == null ? ((WikiPage) wikiPageSimple).Name : ((PXSiteMapNode) siteMapNodeFromKey).Title;
        yield return (object) wikiPageSimple;
      }
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Generate Weeks", Visible = true)]
  public IEnumerable GenerateWeeks(PXAdapter adapter)
  {
    if (((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current != null && ((PXSelectBase<EPSetupMaint.EPWeekFilter>) this.WeekFilter).Current.Year.HasValue)
    {
      ((PXGraph) this).Actions.PressSave();
      DateTime startDate;
      int year;
      this.GetNextUsingWeek(out startDate, out int _, out year);
      DateTime? lasttUsingWeek = this.GetLasttUsingWeek();
      if (lasttUsingWeek.HasValue)
        year = Math.Max(year, lasttUsingWeek.Value.Year);
      ((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current.FromDate = new DateTime?(startDate);
      ((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current.TillDate = new DateTime?(new DateTime(year, 12, 31 /*0x1F*/));
      ((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).AskExt();
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Generate Weeks")]
  public IEnumerable GenerateWeeksOK(PXAdapter adapter)
  {
    this.isGenerate = true;
    DateTime startDate;
    int number;
    int year1;
    this.GetNextUsingWeek(out startDate, out number, out year1);
    DateTime? lasttUsingWeek = this.GetLasttUsingWeek();
    object fromDate = (object) ((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current.FromDate;
    object tillDate = (object) ((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current.TillDate;
    bool flag = ((PXSelectBase) this.GenerateWeeksDialog).Cache.RaiseFieldVerifying<EPSetupMaint.EPGenerateWeeksDialog.fromDate>((object) ((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current, ref fromDate) && ((PXSelectBase) this.GenerateWeeksDialog).Cache.RaiseFieldVerifying<EPSetupMaint.EPGenerateWeeksDialog.tillDate>((object) ((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current, ref tillDate);
    if (this.GenerateWeeksDialog.VerifyRequired() & flag)
    {
      DateTime? nullable1 = new DateTime?();
      while (true)
      {
        DateTime dateTime1 = startDate;
        DateTime? nullable2 = ((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current.TillDate;
        DateTime dateTime2 = nullable2.Value;
        if (dateTime1 <= dateTime2)
        {
          int weekNumber = PXDateTimeInfo.GetWeekNumber(startDate);
          int year2 = startDate.Year;
          if (weekNumber == 1 && startDate.Month == 12)
            ++year2;
          if (weekNumber > 31 /*0x1F*/ && startDate.Month == 1)
            --year2;
          DateTime date = PXDateTimeInfo.GetWeekStart(year2, weekNumber).AddDays(6.0);
          if (lasttUsingWeek.HasValue)
          {
            nullable2 = lasttUsingWeek;
            DateTime dateTime3 = startDate;
            if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() < dateTime3 ? 1 : 0) : 0) == 0)
              goto label_24;
          }
          int? nullable3;
          if (((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current.CutOffDayOne == "FDM")
          {
            int day1 = startDate.Day;
            nullable3 = ((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current.DayOne;
            int valueOrDefault = nullable3.GetValueOrDefault();
            if (day1 <= valueOrDefault & nullable3.HasValue)
            {
              nullable3 = ((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current.DayOne;
              int day2 = date.Day;
              if (nullable3.GetValueOrDefault() < day2 & nullable3.HasValue)
              {
                ref DateTime local = ref date;
                int year3 = startDate.Year;
                int month = startDate.Month;
                nullable3 = ((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current.DayOne;
                int day3 = nullable3.Value;
                local = new DateTime(year3, month, day3);
              }
            }
          }
          else if (((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current.CutOffDayOne == "EOM")
          {
            if (startDate.Year == date.Year)
            {
              if (startDate.Month < date.Month)
                date = new DateTime(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
            }
            else if (startDate.Year < date.Year)
              date = new DateTime(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
          }
          if (((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current.CutOffDayTwo == "FDM")
          {
            int day4 = startDate.Day;
            nullable3 = ((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current.DayTwo;
            int valueOrDefault = nullable3.GetValueOrDefault();
            if (day4 <= valueOrDefault & nullable3.HasValue)
            {
              nullable3 = ((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current.DayTwo;
              int day5 = date.Day;
              if (nullable3.GetValueOrDefault() < day5 & nullable3.HasValue)
              {
                ref DateTime local = ref date;
                int year4 = startDate.Year;
                int month = startDate.Month;
                nullable3 = ((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current.DayTwo;
                int day6 = nullable3.Value;
                local = new DateTime(year4, month, day6);
              }
            }
          }
          else if (((PXSelectBase<EPSetupMaint.EPGenerateWeeksDialog>) this.GenerateWeeksDialog).Current.CutOffDayTwo == "EOM" && startDate.Month != date.Month)
            date = new DateTime(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
label_24:
          if (weekNumber == 1 && startDate.Year < date.Year)
          {
            number = 1;
            ++year1;
          }
          ((PXSelectBase) this.CustomWeek).Cache.Insert((object) new EPCustomWeek()
          {
            StartDate = new DateTime?(this.GetBeginOfDate(startDate)),
            EndDate = new DateTime?(this.GetEndOfDate(date)),
            IsActive = new bool?(true),
            Year = new int?(year1),
            Number = new int?(number)
          });
          if (number != 1 && (startDate.Year < date.AddDays(1.0).Year || year1 < startDate.Year))
          {
            number = 1;
            ++year1;
          }
          else
            ++number;
          startDate = this.GetBeginOfDate(date.AddDays(1.0));
          nullable2 = nullable1;
          DateTime dateTime4 = startDate;
          if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() == dateTime4 ? 1 : 0) : 0) == 0)
            nullable1 = new DateTime?(startDate);
          else
            break;
        }
        else
          goto label_33;
      }
      throw new PXException("An infinite loop occurred due to an incorrect week generation option.");
    }
label_33:
    return adapter.Get();
  }

  public CMSetup CMSETUP
  {
    get
    {
      return PXResultset<CMSetup>.op_Implicit(((PXSelectBase<CMSetup>) this.cmsetup).Select(Array.Empty<object>())) ?? new CMSetup();
    }
  }

  public static string GetPostingOption(PXGraph graph, EPSetup setup, int? employeeID)
  {
    PRSetup payrollPreferences;
    return EPSetupMaint.UsePRPostingOption(graph, setup, employeeID, out payrollPreferences) ? payrollPreferences.TimePostingOption : setup.PostingOption;
  }

  public static int? GetOffBalancePostingAccount(PXGraph graph, EPSetup setup, int? employeeID)
  {
    PRSetup payrollPreferences;
    return EPSetupMaint.UsePRPostingOption(graph, setup, employeeID, out payrollPreferences) ? payrollPreferences.OffBalanceAccountGroupID : setup.OffBalanceAccountGroupID;
  }

  public static bool GetPostPMTransaction(PXGraph graph, EPSetup setup, int? employeeID)
  {
    string postingOption = EPSetupMaint.GetPostingOption(graph, setup, employeeID);
    return postingOption != "N" && postingOption != "A";
  }

  public static bool GetPostToOffBalance(PXGraph graph, EPSetup setup, int? employeeID)
  {
    string postingOption = EPSetupMaint.GetPostingOption(graph, setup, employeeID);
    return postingOption == "O" || postingOption == "M";
  }

  private static bool UsePRPostingOption(
    PXGraph graph,
    EPSetup setup,
    int? employeeID,
    out PRSetup payrollPreferences)
  {
    payrollPreferences = (PRSetup) null;
    if (!PXAccess.FeatureInstalled<FeaturesSet.payrollModule>())
      return false;
    PREmployee prEmployee = PXResult<PREmployee>.op_Implicit(((IQueryable<PXResult<PREmployee>>) PXSelectBase<PREmployee, PXSelect<PREmployee>.Config>.Search<PREmployee.bAccountID>(graph, (object) employeeID, Array.Empty<object>())).FirstOrDefault<PXResult<PREmployee>>());
    if ((prEmployee != null ? (!prEmployee.ActiveInPayroll.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return false;
    payrollPreferences = ((PXSelectBase<PRSetup>) new FbqlSelect<SelectFromBase<PRSetup, TypeArrayOf<IFbqlJoin>.Empty>, PRSetup>.View(graph)).SelectSingle(Array.Empty<object>());
    return payrollPreferences != null;
  }

  protected virtual void EPSetup_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is EPSetup row))
      return;
    PXUIFieldAttribute.SetEnabled<EPSetup.useReceiptAccountForTips>(cache, (object) row, row.NonTaxableTipItem.HasValue);
    PXUIFieldAttribute.SetEnabled<EPSetup.offBalanceAccountGroupID>(cache, (object) row, row.PostingOption == "O");
    if (row.CustomWeek.GetValueOrDefault())
    {
      EPCustomWeek epCustomWeek = PXResultset<EPCustomWeek>.op_Implicit(PXSelectBase<EPCustomWeek, PXSelectOrderBy<EPCustomWeek, OrderBy<Desc<EPCustomWeek.weekID>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()));
      PXUIFieldAttribute.SetEnabled<EPSetup.customWeek>(cache, (object) row, epCustomWeek == null);
    }
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && PXAccess.FeatureInstalled<FeaturesSet.expenseManagement>();
    PXUIFieldAttribute.SetVisible<EPSetup.claimDetailsAssignmentMapID>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<EPSetup.claimAssignmentMapID>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<EPSetup.claimDetailsAssignmentNotificationID>(cache, (object) null, flag);
  }

  protected virtual void EPSetup_PostToOffBalance_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EPSetup row) || !(row.PostingOption != "O"))
      return;
    row.OffBalanceAccountGroupID = new int?();
  }

  protected virtual void EPCustomWeek_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is EPCustomWeek row) || e.Operation == 3)
      return;
    int? number = row.Number;
    int num = 99;
    if (number.GetValueOrDefault() > num & number.HasValue)
    {
      string str = PXMessages.LocalizeFormatNoPrefix("The changes cannot be saved because the limit of weeks per year ({0}) has been exceeded for at least one year. Correct the number of weeks in the following years: {1}.", new object[2]
      {
        (object) 99,
        (object) row.Year
      });
      ((PXSelectBase) this.WeekFilter).Cache.RaiseExceptionHandling<EPSetupMaint.EPWeekFilter.year>((object) ((PXSelectBase<EPSetupMaint.EPWeekFilter>) this.WeekFilter).Current, (object) ((PXSelectBase<EPSetupMaint.EPWeekFilter>) this.WeekFilter).Current.Year, (Exception) new PXException(str));
      throw new PXRowPersistingException(typeof (EPSetupMaint.EPWeekFilter.year).Name, (object) ((PXSelectBase<EPSetupMaint.EPWeekFilter>) this.WeekFilter).Current.Year, str);
    }
  }

  protected virtual void EPSetup_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is EPSetup row) || e.Operation == 3)
      return;
    int? nullable1;
    if (row.PostingOption == "O")
    {
      nullable1 = row.OffBalanceAccountGroupID;
      if (!nullable1.HasValue)
      {
        if (cache.RaiseExceptionHandling<EPSetup.offBalanceAccountGroupID>(e.Row, (object) row.OffBalanceAccountGroupID, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<EPSetup.offBalanceAccountGroupID>(cache)
        })))
          throw new PXRowPersistingException(typeof (EPSetup.offBalanceAccountGroupID).Name, (object) row.OffBalanceAccountGroupID, "'{0}' cannot be empty.", new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName<EPSetup.offBalanceAccountGroupID>(cache)
          });
      }
    }
    if (!row.CustomWeek.GetValueOrDefault())
      return;
    DateTime? firstActivityDate = this.GetFirstActivityDate();
    DateTime? lasttUsingWeek = this.GetLasttUsingWeek();
    EPCustomWeek epCustomWeek1 = PXResultset<EPCustomWeek>.op_Implicit(PXSelectBase<EPCustomWeek, PXSelectOrderBy<EPCustomWeek, OrderBy<Asc<EPCustomWeek.weekID>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    EPCustomWeek epCustomWeek2 = PXResultset<EPCustomWeek>.op_Implicit(PXSelectBase<EPCustomWeek, PXSelectOrderBy<EPCustomWeek, OrderBy<Desc<EPCustomWeek.weekID>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    HashSet<int> intSet1 = new HashSet<int>();
    foreach (EPCustomWeek epCustomWeek3 in ((PXSelectBase) this.CustomWeek).Cache.Inserted)
    {
      int? nullable2;
      if (firstActivityDate.HasValue && lasttUsingWeek.HasValue)
      {
        if (epCustomWeek2 != null)
        {
          nullable1 = epCustomWeek3.WeekID;
          nullable2 = epCustomWeek2.WeekID;
          if (!(nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue))
            goto label_13;
        }
        epCustomWeek2 = epCustomWeek3;
      }
label_13:
      nullable2 = epCustomWeek3.Number;
      int num1 = 99;
      if (nullable2.GetValueOrDefault() > num1 & nullable2.HasValue)
      {
        HashSet<int> intSet2 = intSet1;
        nullable2 = epCustomWeek3.Year;
        int num2 = nullable2.Value;
        intSet2.Add(num2);
      }
    }
    if (firstActivityDate.HasValue && lasttUsingWeek.HasValue)
    {
      if (epCustomWeek1 != null)
      {
        DateTime? nullable3 = firstActivityDate;
        DateTime? startDate = epCustomWeek1.StartDate;
        if ((nullable3.HasValue & startDate.HasValue ? (nullable3.GetValueOrDefault() < startDate.GetValueOrDefault() ? 1 : 0) : 0) == 0 && epCustomWeek2 != null)
        {
          DateTime? endDate = epCustomWeek2.EndDate;
          nullable3 = lasttUsingWeek;
          if ((endDate.HasValue & nullable3.HasValue ? (endDate.GetValueOrDefault() < nullable3.GetValueOrDefault() ? 1 : 0) : 0) == 0)
            goto label_25;
        }
      }
      throw new PXRowPersistingException(typeof (EPSetup.customWeek).Name, (object) row.CustomWeek, "You must configure the weeks in the Custom Week Settings tab for the period spanning between {0:d} and {1:d} because there are existing Activities in this period", new object[2]
      {
        (object) firstActivityDate,
        (object) lasttUsingWeek
      });
    }
label_25:
    if (intSet1.Any<int>())
    {
      string str = PXMessages.LocalizeFormatNoPrefix("The changes cannot be saved because the limit of weeks per year ({0}) has been exceeded for at least one year. Correct the number of weeks in the following years: {1}.", new object[2]
      {
        (object) 99,
        (object) string.Join<int>(",", (IEnumerable<int>) intSet1)
      });
      ((PXSelectBase) this.WeekFilter).Cache.RaiseExceptionHandling<EPSetupMaint.EPWeekFilter.year>((object) ((PXSelectBase<EPSetupMaint.EPWeekFilter>) this.WeekFilter).Current, (object) ((PXSelectBase<EPSetupMaint.EPWeekFilter>) this.WeekFilter).Current.Year, (Exception) new PXException(str));
      throw new PXRowPersistingException(typeof (EPSetupMaint.EPWeekFilter.year).Name, (object) ((PXSelectBase<EPSetupMaint.EPWeekFilter>) this.WeekFilter).Current.Year, str);
    }
  }

  protected virtual void EPSetup_OvertimeMultiplier_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null || (Decimal) e.NewValue <= 0M)
      throw new PXSetPropertyException("The value must be greater than zero");
  }

  protected virtual void EPSetup_DefaultActivityType_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    EPActivityType epActivityType = PXResultset<EPActivityType>.op_Implicit(PXSelectBase<EPActivityType, PXSelect<EPActivityType>.Config>.Search<EPActivityType.type>((PXGraph) this, e.NewValue, Array.Empty<object>()));
    if (epActivityType == null || !epActivityType.RequireTimeByDefault.GetValueOrDefault())
      throw new PXSetPropertyException("Default Activity Type must have track time is enabled.", (PXErrorLevel) 4);
  }

  protected virtual void EPSetup_NonTaxableTipItem_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    PX.Objects.TX.TaxCategory taxCategory = PXResultset<PX.Objects.TX.TaxCategory>.op_Implicit(PXSelectBase<PX.Objects.TX.TaxCategory, PXSelectJoin<PX.Objects.TX.TaxCategory, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.taxCategoryID, Equal<PX.Objects.TX.TaxCategory.taxCategoryID>>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<EPSetup.nonTaxableTipItem>>, And<PX.Objects.TX.TaxCategory.active, Equal<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    }));
    if (taxCategory != null && taxCategory.TaxCategoryID != null)
    {
      bool? taxCatFlag = taxCategory.TaxCatFlag;
      if (taxCatFlag.HasValue)
      {
        taxCatFlag = taxCategory.TaxCatFlag;
        if ((!taxCatFlag.GetValueOrDefault() ? (PXSelectBase<TaxRev>) new PXSelectJoin<TaxRev, InnerJoin<TaxCategoryDet, On<TaxRev.taxID, Equal<TaxCategoryDet.taxID>, And<TaxCategoryDet.taxCategoryID, Equal<Required<PX.Objects.TX.TaxCategory.taxCategoryID>>>>>, Where<TaxRev.taxRate, NotEqual<decimal0>, And<TaxRev.taxRate, Greater<decimal0>>>>((PXGraph) this) : (PXSelectBase<TaxRev>) new PXSelectJoin<TaxRev, LeftJoin<TaxCategoryDet, On<TaxRev.taxID, Equal<TaxCategoryDet.taxID>, And<TaxCategoryDet.taxCategoryID, Equal<Required<PX.Objects.TX.TaxCategory.taxCategoryID>>>>>, Where<TaxRev.taxRate, NotEqual<decimal0>, And<TaxRev.taxRate, Greater<decimal0>, And<TaxCategoryDet.taxCategoryID, IsNull>>>>((PXGraph) this)).Select(new object[1]
        {
          (object) taxCategory.TaxCategoryID
        }).Count > 0)
        {
          string field = (string) PXSelectorAttribute.GetField(sender, e.Row, "NonTaxableTipItem", e.NewValue, typeof (PX.Objects.IN.InventoryItem.inventoryCD).Name);
          e.NewValue = (object) field;
          throw new PXSetPropertyException<EPSetup.nonTaxableTipItem>("The selected item is taxable and cannot be used as a non-taxable tip item. Select a tax-exempt item instead.", (PXErrorLevel) 4);
        }
        sender.RaiseExceptionHandling<EPSetup.nonTaxableTipItem>(e.Row, e.NewValue, (Exception) null);
        return;
      }
    }
    sender.RaiseExceptionHandling<EPSetup.nonTaxableTipItem>(e.Row, e.NewValue, (Exception) null);
  }

  protected virtual void EPWeekFilter_Year_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if ((EPSetupMaint.EPWeekFilter) e.Row == null)
      return;
    int year;
    this.GetNextUsingWeek(out DateTime _, out int _, out year);
    e.NewValue = (object) year;
  }

  protected virtual void EPCustomWeek_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    EPCustomWeek row = (EPCustomWeek) e.Row;
    if (this.isGenerate)
      return;
    DateTime? nullable;
    if (row != null)
    {
      nullable = row.StartDate;
      if (nullable.HasValue)
      {
        nullable = row.EndDate;
        if (nullable.HasValue)
        {
          nullable = row.StartDate;
          DateTime dateTime = nullable.Value;
          int year1 = dateTime.Year;
          int? year2 = row.Year;
          int num1 = year2.Value;
          if (year1 != num1)
          {
            nullable = row.EndDate;
            dateTime = nullable.Value;
            int year3 = dateTime.Year;
            year2 = row.Year;
            int num2 = year2.Value;
            if (year3 != num2)
              throw new PXException("End Of Year");
          }
        }
      }
    }
    EPCustomWeek epCustomWeek = PXResultset<EPCustomWeek>.op_Implicit(PXSelectBase<EPCustomWeek, PXSelectOrderBy<EPCustomWeek, OrderBy<Desc<EPCustomWeek.year, Desc<EPCustomWeek.number>>>>.Config>.SelectSingleBound((PXGraph) this, new object[0], Array.Empty<object>()));
    if (epCustomWeek == null)
      return;
    nullable = epCustomWeek.EndDate;
    if (!nullable.HasValue)
      throw new PXException("Incorrect \"End Date\" of previous week");
  }

  protected virtual void EPCustomWeek_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    EPCustomWeek row = (EPCustomWeek) e.Row;
    if (row == null)
      return;
    EPCustomWeek epCustomWeek1 = PXResultset<EPCustomWeek>.op_Implicit(PXSelectBase<EPCustomWeek, PXSelectOrderBy<EPCustomWeek, OrderBy<Desc<EPCustomWeek.weekID>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (epCustomWeek1 == null)
      return;
    int? weekId1 = epCustomWeek1.WeekID;
    int? weekId2 = row.WeekID;
    bool flag = weekId1.GetValueOrDefault() > weekId2.GetValueOrDefault() & weekId1.HasValue & weekId2.HasValue;
    PXUIFieldAttribute.SetReadOnly(cache, (object) row, true);
    PXUIFieldAttribute.SetReadOnly<EPCustomWeek.endDate>(cache, (object) row, flag);
    PXUIFieldAttribute.SetReadOnly<EPCustomWeek.isFullWeek>(cache, (object) row, flag);
    PXCache pxCache = cache;
    EPCustomWeek epCustomWeek2 = row;
    int? year1 = row.Year;
    int num1;
    if (year1.HasValue)
    {
      DateTime? startDate = row.StartDate;
      if (startDate.HasValue)
      {
        startDate = row.StartDate;
        DateTime dateTime = startDate.Value;
        dateTime = dateTime.AddDays(-6.0);
        int year2 = dateTime.Year;
        year1 = row.Year;
        int num2 = year1.Value;
        if (year2 <= num2)
        {
          year1 = row.Year;
          int num3 = year1.Value;
          startDate = row.StartDate;
          dateTime = startDate.Value;
          dateTime = dateTime.AddDays(6.0);
          int year3 = dateTime.Year;
          num1 = num3 <= year3 ? 1 : 0;
          goto label_8;
        }
        num1 = 0;
        goto label_8;
      }
    }
    num1 = 0;
label_8:
    PXUIFieldAttribute.SetReadOnly<EPCustomWeek.startDate>(pxCache, (object) epCustomWeek2, num1 != 0);
  }

  protected virtual void EPCustomWeek_StartDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if ((EPCustomWeek) e.Row == null)
      return;
    EPCustomWeek epCustomWeek = PXResultset<EPCustomWeek>.op_Implicit(PXSelectBase<EPCustomWeek, PXSelectOrderBy<EPCustomWeek, OrderBy<Desc<EPCustomWeek.year, Desc<EPCustomWeek.number>>>>.Config>.SelectSingleBound((PXGraph) this, new object[0], Array.Empty<object>()));
    if (epCustomWeek == null)
      return;
    DateTime? endDate = epCustomWeek.EndDate;
    if (!endDate.HasValue)
      return;
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    endDate = epCustomWeek.EndDate;
    // ISSUE: variable of a boxed type
    __Boxed<DateTime> beginOfDate = (ValueType) this.GetBeginOfDate(endDate.Value.AddDays(1.0));
    defaultingEventArgs.NewValue = (object) beginOfDate;
  }

  protected virtual void EPCustomWeek_EndDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    EPCustomWeek row = (EPCustomWeek) e.Row;
    if (row == null)
      return;
    DateTime? startDate = row.StartDate;
    if (!startDate.HasValue)
      return;
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    startDate = row.StartDate;
    // ISSUE: variable of a boxed type
    __Boxed<DateTime> endOfDate = (ValueType) this.GetEndOfDate(startDate.Value.AddDays(6.0));
    defaultingEventArgs.NewValue = (object) endOfDate;
  }

  protected virtual void EPCustomWeek_EndDate_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    e.NewValue = (object) this.GetEndOfDate((DateTime) e.NewValue);
  }

  protected virtual void EPCustomWeek_StartDate_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    e.NewValue = (object) this.GetBeginOfDate((DateTime) e.NewValue);
  }

  protected virtual void EPCustomWeek_Number_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    EPCustomWeek row = (EPCustomWeek) e.Row;
    if (row == null)
      return;
    EPCustomWeek epCustomWeek = PXResultset<EPCustomWeek>.op_Implicit(PXSelectBase<EPCustomWeek, PXSelect<EPCustomWeek, Where<EPCustomWeek.year, Equal<Required<EPCustomWeek.year>>>, OrderBy<Desc<EPCustomWeek.number>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) row.Year
    }));
    if (epCustomWeek != null)
      e.NewValue = (object) (epCustomWeek.Number.Value + 1);
    else
      e.NewValue = (object) 1;
  }

  protected virtual void EPCustomWeek_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    EPCustomWeek row = (EPCustomWeek) e.Row;
    if (row == null)
      return;
    if (PXResultset<EPTimeCard>.op_Implicit(PXSelectBase<EPTimeCard, PXSelect<EPTimeCard>.Config>.Search<EPTimeCard.weekId>((PXGraph) this, (object) row.WeekID, Array.Empty<object>())) != null || PXResultset<PMTimeActivity>.op_Implicit(PXSelectBase<PMTimeActivity, PXSelect<PMTimeActivity>.Config>.Search<PMTimeActivity.weekID>((PXGraph) this, (object) row.WeekID, Array.Empty<object>())) != null)
      throw new PXException("Week in use, cannot delete record");
    EPCustomWeek epCustomWeek = PXResultset<EPCustomWeek>.op_Implicit(PXSelectBase<EPCustomWeek, PXSelectOrderBy<EPCustomWeek, OrderBy<Desc<EPCustomWeek.weekID>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (epCustomWeek == null)
      return;
    int? weekId1 = epCustomWeek.WeekID;
    int? weekId2 = row.WeekID;
    if (weekId1.GetValueOrDefault() > weekId2.GetValueOrDefault() & weekId1.HasValue & weekId2.HasValue)
      throw new PXSetPropertyException("Week is not last, cannot delete record", (PXErrorLevel) 3);
  }

  protected virtual void EPCustomWeek_IsFullWeek_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    EPCustomWeek row = (EPCustomWeek) e.Row;
    if (row == null)
      return;
    DateTime? nullable = row.EndDate;
    if (nullable.HasValue)
    {
      nullable = row.StartDate;
      if (nullable.HasValue)
      {
        PXFieldDefaultingEventArgs defaultingEventArgs = e;
        nullable = row.EndDate;
        DateTime dateTime1 = nullable.Value;
        ref DateTime local1 = ref dateTime1;
        nullable = row.StartDate;
        DateTime dateTime2 = nullable.Value;
        // ISSUE: variable of a boxed type
        __Boxed<bool> local2 = (ValueType) (local1.Subtract(dateTime2).TotalDays > 6.0);
        defaultingEventArgs.NewValue = (object) local2;
        return;
      }
    }
    e.NewValue = (object) false;
  }

  protected virtual void EPCustomWeek_EndDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    EPCustomWeek row1 = (EPCustomWeek) e.Row;
    if (row1 == null || e.NewValue == null)
      return;
    DateTime? startDate = row1.StartDate;
    if (!startDate.HasValue)
      return;
    DateTime newValue = (DateTime) e.NewValue;
    ref DateTime local = ref newValue;
    startDate = row1.StartDate;
    DateTime dateTime = startDate.Value;
    if (local.Subtract(dateTime).TotalDays <= 7.0)
      return;
    PXCache pxCache = sender;
    object row2 = e.Row;
    object[] objArray = new object[2]
    {
      (object) PXUIFieldAttribute.GetDisplayName<EPCustomWeek.endDate>(sender),
      null
    };
    startDate = row1.StartDate;
    objArray[1] = (object) startDate.Value.AddDays(6.0);
    PXException pxException = new PXException("{0} cannot be later than '{1:d}'", objArray);
    pxCache.RaiseExceptionHandling<EPCustomWeek.endDate>(row2, (object) null, (Exception) pxException);
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void EPCustomWeek_StartDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    EPCustomWeek row = (EPCustomWeek) e.Row;
    if (row == null)
      return;
    DateTime newValue = (DateTime) e.NewValue;
    if (newValue.AddDays(-6.0).Year <= row.Year.Value && row.Year.Value <= newValue.AddDays(6.0).Year)
      return;
    sender.RaiseExceptionHandling<EPCustomWeek.startDate>(e.Row, (object) null, (Exception) new PXException("Start Date must have a Year at {0}.", new object[1]
    {
      (object) row.Year
    }));
  }

  protected virtual void EPGenerateWeeksDialog_TillDate_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    EPSetupMaint.EPGenerateWeeksDialog row = (EPSetupMaint.EPGenerateWeeksDialog) e.Row;
    if (e.NewValue == null || row == null)
      return;
    DateTime newValue1 = (DateTime) e.NewValue;
    DateTime? fromDate = row.FromDate;
    if ((fromDate.HasValue ? (newValue1 < fromDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXSetPropertyException("{0} must be greater than or equal to {1} '{2:d}'.", new object[3]
      {
        (object) PXUIFieldAttribute.GetDisplayName<EPSetupMaint.EPGenerateWeeksDialog.tillDate>(cache),
        (object) PXUIFieldAttribute.GetDisplayName<EPSetupMaint.EPGenerateWeeksDialog.fromDate>(cache),
        (object) row.FromDate
      });
    }
    DateTime? lasttUsingWeek = this.GetLasttUsingWeek();
    if (!lasttUsingWeek.HasValue)
      return;
    DateTime newValue2 = (DateTime) e.NewValue;
    DateTime? nullable = lasttUsingWeek;
    if ((nullable.HasValue ? (newValue2 < nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXSetPropertyException("The {0} cannot be earlier than '{1:d}' because some activities or time cards were created after the specified period.", new object[2]
      {
        (object) PXUIFieldAttribute.GetDisplayName<EPSetupMaint.EPGenerateWeeksDialog.tillDate>(cache),
        (object) lasttUsingWeek
      });
    }
  }

  protected virtual void EPGenerateWeeksDialog_FromDate_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    EPSetupMaint.EPGenerateWeeksDialog row = (EPSetupMaint.EPGenerateWeeksDialog) e.Row;
    if (e.NewValue == null || row == null)
      return;
    DateTime newValue1 = (DateTime) e.NewValue;
    DateTime? tillDate = row.TillDate;
    if ((tillDate.HasValue ? (newValue1 > tillDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXSetPropertyException("{0} must be less than or equal to {1} '{2:d}'.", new object[3]
      {
        (object) PXUIFieldAttribute.GetDisplayName<EPSetupMaint.EPGenerateWeeksDialog.fromDate>(cache),
        (object) PXUIFieldAttribute.GetDisplayName<EPSetupMaint.EPGenerateWeeksDialog.tillDate>(cache),
        (object) row.FromDate
      });
    }
    DateTime? firstUsingWeek = this.GetFirstUsingWeek();
    EPCustomWeek epCustomWeek = PXResultset<EPCustomWeek>.op_Implicit(PXSelectBase<EPCustomWeek, PXSelectOrderBy<EPCustomWeek, OrderBy<Desc<EPCustomWeek.weekID>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()));
    if (!firstUsingWeek.HasValue)
      return;
    DateTime newValue2 = (DateTime) e.NewValue;
    DateTime? nullable = firstUsingWeek;
    if ((nullable.HasValue ? (newValue2 > nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0 && epCustomWeek == null)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXSetPropertyException("The {0} cannot be later than '{1:d}' because some activities or time cards were created before the specified period.", new object[2]
      {
        (object) PXUIFieldAttribute.GetDisplayName<EPSetupMaint.EPGenerateWeeksDialog.fromDate>(cache),
        (object) firstUsingWeek
      });
    }
  }

  protected virtual void EPGenerateWeeksDialog_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    EPSetupMaint.EPGenerateWeeksDialog row = (EPSetupMaint.EPGenerateWeeksDialog) e.Row;
    if (row == null)
      return;
    if (row.CutOffDayOne == "FDM")
    {
      PXUIFieldAttribute.SetEnabled<EPSetupMaint.EPGenerateWeeksDialog.dayOne>(cache, (object) row, true);
      PXUIFieldAttribute.SetRequired<EPSetupMaint.EPGenerateWeeksDialog.dayOne>(cache, true);
      PXUIFieldAttribute.SetEnabled<EPSetupMaint.EPGenerateWeeksDialog.cutOffDayTwo>(cache, (object) row, true);
      PXDefaultAttribute.SetPersistingCheck<EPSetupMaint.EPGenerateWeeksDialog.dayOne>(cache, (object) row, (PXPersistingCheck) 1);
    }
    else
    {
      PXUIFieldAttribute.SetEnabled<EPSetupMaint.EPGenerateWeeksDialog.cutOffDayTwo>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<EPSetupMaint.EPGenerateWeeksDialog.dayOne>(cache, (object) row, false);
      row.DayOne = new int?();
      row.DayTwo = new int?();
      row.CutOffDayTwo = "NOT";
    }
    if (row.CutOffDayTwo == "FDM")
    {
      PXUIFieldAttribute.SetEnabled<EPSetupMaint.EPGenerateWeeksDialog.dayTwo>(cache, (object) row, true);
      PXUIFieldAttribute.SetRequired<EPSetupMaint.EPGenerateWeeksDialog.dayTwo>(cache, true);
      PXDefaultAttribute.SetPersistingCheck<EPSetupMaint.EPGenerateWeeksDialog.dayTwo>(cache, (object) row, (PXPersistingCheck) 1);
    }
    else
    {
      PXUIFieldAttribute.SetEnabled<EPSetupMaint.EPGenerateWeeksDialog.dayTwo>(cache, (object) row, false);
      row.DayTwo = new int?();
    }
    if (PXResultset<EPCustomWeek>.op_Implicit(PXSelectBase<EPCustomWeek, PXSelectOrderBy<EPCustomWeek, OrderBy<Desc<EPCustomWeek.number>>>.Config>.Select((PXGraph) this, Array.Empty<object>())) == null)
      return;
    PXUIFieldAttribute.SetEnabled<EPSetupMaint.EPGenerateWeeksDialog.fromDate>(cache, (object) row, false);
  }

  protected virtual void EPWeekFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    EPSetupMaint.EPWeekFilter row = (EPSetupMaint.EPWeekFilter) e.Row;
    if (row == null)
      return;
    DateTime? lasttUsingWeek = this.GetLasttUsingWeek();
    DateTime dateTime;
    if (lasttUsingWeek.HasValue)
    {
      dateTime = lasttUsingWeek.Value;
      int year1 = dateTime.Year;
      int? year2 = row.Year;
      int valueOrDefault = year2.GetValueOrDefault();
      if (year1 > valueOrDefault & year2.HasValue)
      {
        ((PXSelectBase) this.CustomWeek).Cache.AllowInsert = false;
        return;
      }
    }
    EPCustomWeek epCustomWeek = PXResultset<EPCustomWeek>.op_Implicit(PXSelectBase<EPCustomWeek, PXSelectOrderBy<EPCustomWeek, OrderBy<Desc<EPCustomWeek.weekID>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()));
    if (epCustomWeek != null)
    {
      DateTime? endDate = epCustomWeek.EndDate;
      if (endDate.HasValue)
      {
        endDate = epCustomWeek.EndDate;
        dateTime = endDate.Value;
        dateTime = dateTime.AddDays(1.0);
        int year3 = dateTime.Year;
        int? year4 = row.Year;
        int valueOrDefault = year4.GetValueOrDefault();
        if (!(year3 == valueOrDefault & year4.HasValue))
        {
          ((PXSelectBase) this.CustomWeek).Cache.AllowInsert = false;
          return;
        }
      }
    }
    ((PXSelectBase) this.CustomWeek).Cache.AllowInsert = true;
  }

  private DateTime? GetFirstUsingWeek()
  {
    DateTime? firstUsingWeek = this.GetFirstActivityDate();
    EPTimeCard epTimeCard = PXResultset<EPTimeCard>.op_Implicit(PXSelectBase<EPTimeCard, PXSelectOrderBy<EPTimeCard, OrderBy<Asc<EPTimeCard.weekId>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()));
    if (epTimeCard != null)
    {
      DateTime weekStartDate = PXWeekSelectorAttribute.GetWeekStartDate(epTimeCard.WeekID.Value);
      if (!firstUsingWeek.HasValue)
        firstUsingWeek = new DateTime?(weekStartDate);
      else if (weekStartDate < firstUsingWeek.Value)
        firstUsingWeek = new DateTime?(weekStartDate);
    }
    return firstUsingWeek;
  }

  private DateTime? GetFirstActivityDate()
  {
    MasterFinYear masterFinYear = PXResultset<MasterFinYear>.op_Implicit(PXSelectBase<MasterFinYear, PXSelect<MasterFinYear, Where<True, Equal<True>>, OrderBy<Desc<MasterFinYear.year>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    int result;
    if (masterFinYear != null && int.TryParse(masterFinYear.Year + "01", out result))
    {
      CRPMTimeActivity crpmTimeActivity = PXResultset<CRPMTimeActivity>.op_Implicit(PXSelectBase<CRPMTimeActivity, PXSelect<CRPMTimeActivity, Where<CRPMTimeActivity.weekID, GreaterEqual<Required<CRPMTimeActivity.weekID>>, And<CRPMTimeActivity.trackTime, Equal<True>, And<CRPMTimeActivity.classID, NotEqual<CRActivityClass.emailRouting>, And<CRPMTimeActivity.classID, NotEqual<CRActivityClass.task>, And<CRPMTimeActivity.classID, NotEqual<CRActivityClass.events>>>>>>, OrderBy<Asc<CRPMTimeActivity.weekID>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) result
      }));
      if (crpmTimeActivity != null)
        return new DateTime?(crpmTimeActivity.StartDate.Value);
    }
    return new DateTime?();
  }

  private DateTime? GetLasttUsingWeek()
  {
    DateTime dateTime1 = new DateTime(1900, 1, 1);
    DateTime dateTime2 = new DateTime(1900, 1, 1);
    EPTimeCard epTimeCard = PXResultset<EPTimeCard>.op_Implicit(PXSelectBase<EPTimeCard, PXSelectOrderBy<EPTimeCard, OrderBy<Desc<EPTimeCard.weekId>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()));
    CRPMTimeActivity crpmTimeActivity = PXResultset<CRPMTimeActivity>.op_Implicit(PXSelectBase<CRPMTimeActivity, PXSelect<CRPMTimeActivity, Where<CRPMTimeActivity.weekID, IsNotNull, And<CRPMTimeActivity.trackTime, Equal<True>, And<CRPMTimeActivity.classID, NotEqual<CRActivityClass.emailRouting>, And<CRPMTimeActivity.classID, NotEqual<CRActivityClass.task>, And<CRPMTimeActivity.classID, NotEqual<CRActivityClass.events>>>>>>, OrderBy<Desc<CRPMTimeActivity.weekID>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()));
    if (epTimeCard != null)
      dateTime2 = PXWeekSelector2Attribute.GetWeekStartDate((PXGraph) this, epTimeCard.WeekID.Value);
    if (crpmTimeActivity != null)
      dateTime1 = crpmTimeActivity.StartDate.Value;
    DateTime dateTime3 = dateTime2 >= dateTime1 ? dateTime2 : dateTime1;
    return dateTime3 == new DateTime(1900, 1, 1) ? new DateTime?() : new DateTime?(dateTime3);
  }

  private void GetNextUsingWeek(out DateTime startDate, out int number, out int year)
  {
    EPCustomWeek epCustomWeek = PXResultset<EPCustomWeek>.op_Implicit(PXSelectBase<EPCustomWeek, PXSelectOrderBy<EPCustomWeek, OrderBy<Desc<EPCustomWeek.weekID>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()));
    if (epCustomWeek != null)
    {
      ref DateTime local = ref startDate;
      DateTime dateTime1 = epCustomWeek.EndDate.Value;
      DateTime dateTime2 = dateTime1.AddDays(1.0);
      local = dateTime2;
      int? number1 = epCustomWeek.Number;
      int num = 1;
      if (number1.GetValueOrDefault() > num & number1.HasValue)
      {
        dateTime1 = epCustomWeek.StartDate.Value;
        if (dateTime1.Year < startDate.Year)
        {
          number = 1;
          goto label_5;
        }
      }
      number = epCustomWeek.Number.Value + 1;
label_5:
      year = startDate.Year;
    }
    else
    {
      DateTime? firstUsingWeek = this.GetFirstUsingWeek();
      startDate = !firstUsingWeek.HasValue ? (((PXSelectBase<EPSetupMaint.EPWeekFilter>) this.WeekFilter).Current == null ? new DateTime(((PXGraph) this).Accessinfo.BusinessDate.Value.Year, 1, 1) : new DateTime(((PXSelectBase<EPSetupMaint.EPWeekFilter>) this.WeekFilter).Current.Year.Value, 1, 1)) : firstUsingWeek.Value;
      number = PXWeekSelectorAttribute.GetWeekID(startDate) % 100;
      year = PXWeekSelectorAttribute.GetWeekID(startDate) / 100;
    }
  }

  private DateTime GetEndOfDate(DateTime date)
  {
    return new DateTime(date.Year, date.Month, date.Day, 23, 59, 0);
  }

  private DateTime GetBeginOfDate(DateTime date)
  {
    return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
  }

  [PXDBInt]
  [PXUIField(Visible = false)]
  [PXFormula(typeof (Add<Mult<EPCustomWeek.year, decimal100>, EPCustomWeek.number>))]
  protected virtual void EPCustomWeek_WeekID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Year", Visible = false)]
  [PXDefault(typeof (EPSetupMaint.EPWeekFilter.year))]
  protected virtual void EPCustomWeek_Year_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Number")]
  [PXDefault]
  protected virtual void EPCustomWeek_Number_CacheAttached(PXCache sender)
  {
  }

  [PXHidden]
  [Serializable]
  public class EPWeekFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    private int? _year;

    [PXDBInt]
    [PXUIField]
    [PXSelector(typeof (Search3<MasterFinYear.year, OrderBy<Desc<MasterFinYear.year>>>))]
    public virtual int? Year
    {
      get => this._year;
      set => this._year = value;
    }

    public abstract class year : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPSetupMaint.EPWeekFilter.year>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class EPGenerateWeeksDialog : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBDate]
    [PXDefault]
    [PXUIField(DisplayName = "From Date")]
    public virtual DateTime? FromDate { get; set; }

    [PXDBDate]
    [PXDefault]
    [PXUIField(DisplayName = "Until Date")]
    public virtual DateTime? TillDate { get; set; }

    [PXDBString]
    [EPSetupMaint.EPGenerateWeeksDialog.CutOffDayList]
    [PXDefault("NOT")]
    [PXUIField(DisplayName = "Cut Off Day One")]
    public virtual string CutOffDayOne { get; set; }

    [PXDBString(3, IsFixed = true)]
    [EPSetupMaint.EPGenerateWeeksDialog.CutOffDayList]
    [PXDefault("NOT")]
    [PXUIField(DisplayName = "Cut Off Day Two")]
    public virtual string CutOffDayTwo { get; set; }

    [PXDBInt(MinValue = 1, MaxValue = 31 /*0x1F*/)]
    [PXDefault]
    [PXUIField(DisplayName = "Day One")]
    public virtual int? DayOne { get; set; }

    [PXDBInt(MinValue = 1, MaxValue = 31 /*0x1F*/)]
    [PXDefault]
    [PXUIField(DisplayName = "Day Two")]
    public virtual int? DayTwo { get; set; }

    public abstract class fromDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      EPSetupMaint.EPGenerateWeeksDialog.fromDate>
    {
    }

    public abstract class tillDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      EPSetupMaint.EPGenerateWeeksDialog.tillDate>
    {
    }

    public abstract class cutOffDayOne : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      EPSetupMaint.EPGenerateWeeksDialog.cutOffDayOne>
    {
    }

    public abstract class cutOffDayTwo : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      EPSetupMaint.EPGenerateWeeksDialog.cutOffDayTwo>
    {
    }

    public abstract class dayOne : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EPSetupMaint.EPGenerateWeeksDialog.dayOne>
    {
    }

    public abstract class dayTwo : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EPSetupMaint.EPGenerateWeeksDialog.dayTwo>
    {
    }

    public class CutOffDayListAttribute : PXStringListAttribute
    {
      public const string None = "NOT";
      public const string FixedDayOfMonth = "FDM";
      public const string EndOfMonth = "EOM";

      public CutOffDayListAttribute()
        : base(new string[3]{ "NOT", "FDM", "EOM" }, new string[3]
        {
          nameof (None),
          "Fixed Day of Month",
          "End of Month"
        })
      {
      }
    }
  }
}

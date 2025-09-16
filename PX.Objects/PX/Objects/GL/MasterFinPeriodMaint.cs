// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.MasterFinPeriodMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.Common.Scopes;
using PX.Objects.CS;
using PX.Objects.FA;
using PX.Objects.GL.DAC;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL.GraphBaseExtensions;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

public class MasterFinPeriodMaint : 
  PXGraph<MasterFinPeriodMaint, MasterFinYear>,
  IFinPeriodMaintenanceGraph
{
  public PXAction<MasterFinYear> AutoFill;
  public PXFilter<FinPeriodSaveDialog> SaveDialog;
  public PXSelect<MasterFinYear> FiscalYear;
  public PXSelect<MasterFinPeriod, Where<MasterFinPeriod.finYear, Equal<Optional<MasterFinYear.year>>>, OrderBy<Asc<MasterFinPeriod.periodNbr>>> Periods;
  public PXSelect<OrganizationFinYear> OrganizationYear;
  public PXSelect<OrganizationFinPeriod> OrganizationPeriods;
  public PXSelectReadonly3<FinPeriodSetup, OrderBy<Asc<FinPeriodSetup.periodNbr>>> PeriodsSetup;
  public PXSetup<FinYearSetup> YearSetup;

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public MasterFinPeriodMaint()
  {
    if (((PXSelectBase<FinYearSetup>) this.YearSetup).Current == null)
      throw new PXSetPropertyException("You must configure the Financial Year Settings first.");
    ((PXSelectBase) this.FiscalYear).Cache.AllowInsert = true;
    ((PXSelectBase) this.FiscalYear).Cache.AllowUpdate = true;
    ((PXSelectBase) this.FiscalYear).Cache.AllowDelete = true;
    ((PXAction) this.Delete).SetConfirmationMessage("The selected financial year will be deleted.");
    PXUIFieldAttribute.SetVisible<MasterFinPeriod.iNClosed>(((PXSelectBase) this.Periods).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.inventory>() && PXAccess.FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>());
    PXUIFieldAttribute.SetVisibility<MasterFinPeriod.iNClosed>(((PXSelectBase) this.Periods).Cache, (object) null, !PXAccess.FeatureInstalled<FeaturesSet.inventory>() || !PXAccess.FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>() ? (PXUIVisibility) 1 : (PXUIVisibility) 3);
    PXUIFieldAttribute.SetVisible<MasterFinPeriod.fAClosed>(((PXSelectBase) this.Periods).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.fixedAsset>() && PXAccess.FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>());
    PXUIFieldAttribute.SetVisibility<MasterFinPeriod.fAClosed>(((PXSelectBase) this.Periods).Cache, (object) null, !PXAccess.FeatureInstalled<FeaturesSet.fixedAsset>() || !PXAccess.FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>() ? (PXUIVisibility) 1 : (PXUIVisibility) 3);
  }

  public virtual bool CanClipboardCopyPaste() => false;

  public virtual MasterFinYear GeneratePreviousMasterFinYear()
  {
    MasterFinYear previousMasterFinYear;
    using (new MasterFinPeriodMaint.YearsGenerationScope())
    {
      previousMasterFinYear = this.InsertPreviousMasterFinYear();
      MasterFinYear masterFinYear = PXResultset<MasterFinYear>.op_Implicit(PXSelectBase<MasterFinYear, PXSelect<MasterFinYear, Where<MasterFinYear.year, Greater<Required<MasterFinYear.year>>>, OrderBy<Asc<MasterFinYear.year>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) previousMasterFinYear.Year
      }));
      DateTime? endDate = previousMasterFinYear.EndDate;
      DateTime? startDate = masterFinYear.StartDate;
      if ((endDate.HasValue == startDate.HasValue ? (endDate.HasValue ? (endDate.GetValueOrDefault() != startDate.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        throw new PXSetPropertyException("A calendar for the {0} year cannot be added because the calendar template has been changed.", new object[1]
        {
          (object) previousMasterFinYear.Year
        });
      this.FillCurrentYearWithPeriods("Locked", this.FinPeriodRepository.FindFirstPeriod(new int?(0), true));
      ((PXAction) this.Save).Press();
    }
    return previousMasterFinYear;
  }

  public virtual MasterFinYear GenerateNextMasterFinYear()
  {
    MasterFinYear nextMasterFinYear;
    using (new MasterFinPeriodMaint.YearsGenerationScope())
    {
      nextMasterFinYear = ((PXSelectBase<MasterFinYear>) this.FiscalYear).Insert();
      this.FillCurrentYearWithPeriods("Inactive");
      ((PXAction) this.Save).Press();
    }
    return nextMasterFinYear;
  }

  public virtual void GenerateCalendar(int? organizationID, int fromYear, int toYear)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      FiscalYearSetupMaint instance = PXGraph.CreateInstance<FiscalYearSetupMaint>();
      instance.SetCurrentYearSetup();
      instance.ShiftBackFirstYearTo($"{fromYear:0000}");
      this.GenerateMasterCalendar(fromYear, toYear);
      transactionScope.Complete();
    }
  }

  protected virtual void GenerateMasterCalendar(int fromYear, int toYear)
  {
    if (this.HasInsertedYear)
      ((PXSelectBase) this.FiscalYear).Cache.Clear();
    (int firstYear, int lastYear) = this.FinPeriodUtils.GetFirstLastYearForGeneration(new int?(0), fromYear, toYear, true);
    if (fromYear < firstYear)
    {
      MasterFinYear previousMasterFinYear;
      do
      {
        previousMasterFinYear = this.GeneratePreviousMasterFinYear();
      }
      while (previousMasterFinYear != null && string.CompareOrdinal(previousMasterFinYear.Year, fromYear.ToString()) > 0);
    }
    if (toYear <= lastYear)
      return;
    MasterFinYear nextMasterFinYear;
    do
    {
      nextMasterFinYear = this.GenerateNextMasterFinYear();
    }
    while (nextMasterFinYear != null && string.CompareOrdinal(nextMasterFinYear.Year, toYear.ToString()) < 0);
  }

  protected virtual void FillCurrentYearWithPeriods(
    string status = null,
    FinPeriod masterFinPeriodStatusSource = null)
  {
    using (new MasterFinPeriodMaint.MassInsertingOfPeriodsScope())
    {
      MasterFinPeriod masterFinPeriod;
      do
      {
        masterFinPeriod = new MasterFinPeriod();
        if (masterFinPeriodStatusSource != null)
        {
          masterFinPeriod.Status = masterFinPeriodStatusSource.Status;
          masterFinPeriod.ARClosed = masterFinPeriodStatusSource.ARClosed;
          masterFinPeriod.APClosed = masterFinPeriodStatusSource.APClosed;
          masterFinPeriod.FAClosed = masterFinPeriodStatusSource.FAClosed;
          masterFinPeriod.CAClosed = masterFinPeriodStatusSource.CAClosed;
          masterFinPeriod.INClosed = masterFinPeriodStatusSource.INClosed;
        }
        else if (!string.IsNullOrEmpty(status))
        {
          masterFinPeriod.Status = status;
          switch (status)
          {
            case "Inactive":
            case "Open":
              masterFinPeriod.APClosed = masterFinPeriod.ARClosed = masterFinPeriod.CAClosed = masterFinPeriod.FAClosed = masterFinPeriod.INClosed = new bool?(false);
              break;
            case "Closed":
            case "Locked":
              masterFinPeriod.APClosed = masterFinPeriod.ARClosed = masterFinPeriod.CAClosed = masterFinPeriod.FAClosed = masterFinPeriod.INClosed = new bool?(true);
              break;
          }
        }
      }
      while (((PXSelectBase<MasterFinPeriod>) this.Periods).Insert(masterFinPeriod) != null);
    }
  }

  [PXButton(Tooltip = "Auto fill periods")]
  [PXUIField]
  public virtual IEnumerable autoFill(PXAdapter adapter)
  {
    this.FillCurrentYearWithPeriods();
    return adapter.Get();
  }

  protected virtual MasterFinYear InsertPreviousMasterFinYear()
  {
    using (new MasterFinPeriodMaint.InsertingOfPreviousYearScope())
      return ((PXSelectBase<MasterFinYear>) this.FiscalYear).Insert();
  }

  protected virtual void ModifyEndYear()
  {
    FinPeriodSaveDialog current1 = ((PXSelectBase<FinPeriodSaveDialog>) this.SaveDialog).Current;
    MasterFinYear current2 = ((PXSelectBase<MasterFinYear>) this.FiscalYear).Current;
    FinYearSetup current3 = ((PXSelectBase<FinYearSetup>) this.YearSetup).Current;
    MasterFinPeriod masterFinPeriod1 = (MasterFinPeriod) null;
    if (current2 != null)
    {
      foreach (PXResult<MasterFinPeriod> pxResult in ((PXSelectBase<MasterFinPeriod>) this.Periods).Select(new object[1]
      {
        (object) current2.Year
      }))
      {
        MasterFinPeriod masterFinPeriod2 = PXResult<MasterFinPeriod>.op_Implicit(pxResult);
        if (masterFinPeriod1 != null)
        {
          DateTime? nullable = masterFinPeriod2.StartDate;
          DateTime dateTime1 = nullable.Value;
          nullable = masterFinPeriod1.StartDate;
          DateTime dateTime2 = nullable.Value;
          if (!(dateTime1 > dateTime2))
          {
            nullable = masterFinPeriod2.EndDate;
            DateTime dateTime3 = nullable.Value;
            nullable = masterFinPeriod1.EndDate;
            DateTime dateTime4 = nullable.Value;
            if (!(dateTime3 > dateTime4))
              continue;
          }
          DateTime dateTime5 = masterFinPeriod2.StartDate.Value;
          nullable = masterFinPeriod2.EndDate;
          if ((nullable.HasValue ? (dateTime5 != nullable.GetValueOrDefault() ? 1 : 0) : 1) == 0)
            continue;
        }
        masterFinPeriod1 = masterFinPeriod2;
      }
    }
    DateTime? nullable1 = masterFinPeriod1.EndDate;
    DateTime dateTime6 = nullable1.Value;
    DateTime dateTime7;
    if (this.IsWeekBasedPeriod(current3.PeriodType))
    {
      nullable1 = current3.PeriodsStartDate;
      int dayOfWeek1 = (int) nullable1.Value.DayOfWeek;
      nullable1 = masterFinPeriod1.EndDate;
      int dayOfWeek2 = (int) nullable1.Value.DayOfWeek;
      if (dayOfWeek1 != dayOfWeek2 && !current1.MoveDayOfWeek.Value)
      {
        nullable1 = current3.PeriodsStartDate;
        int dayOfWeek3 = (int) nullable1.Value.DayOfWeek;
        nullable1 = masterFinPeriod1.EndDate;
        int dayOfWeek4 = (int) nullable1.Value.DayOfWeek;
        int num1 = dayOfWeek3 - dayOfWeek4;
        nullable1 = masterFinPeriod1.EndDate;
        int num2 = 7 - (int) nullable1.Value.DayOfWeek;
        nullable1 = current3.PeriodsStartDate;
        dateTime7 = nullable1.Value;
        int dayOfWeek5 = (int) dateTime7.DayOfWeek;
        int num3 = num2 - dayOfWeek5;
        DateTime dateTime8;
        if (Math.Abs(num1) < num3)
        {
          nullable1 = masterFinPeriod1.EndDate;
          dateTime7 = nullable1.Value;
          DateTime dateTime9 = dateTime7.AddDays((double) num1);
          nullable1 = masterFinPeriod1.StartDate;
          DateTime dateTime10 = nullable1.Value;
          if (dateTime9 > dateTime10)
          {
            nullable1 = masterFinPeriod1.EndDate;
            dateTime7 = nullable1.Value;
            dateTime8 = dateTime7.AddDays((double) num1);
            goto label_19;
          }
        }
        nullable1 = masterFinPeriod1.EndDate;
        dateTime7 = nullable1.Value;
        dateTime8 = dateTime7.AddDays((double) num3);
label_19:
        masterFinPeriod1.EndDate = new DateTime?(dateTime8);
        masterFinPeriod1 = ((PXSelectBase<MasterFinPeriod>) this.Periods).Update(masterFinPeriod1);
      }
    }
    switch (current1.Method)
    {
      case "Y":
        nullable1 = current2.EndDate;
        DateTime? endDate = masterFinPeriod1.EndDate;
        TimeSpan timeSpan1 = (nullable1.HasValue & endDate.HasValue ? new TimeSpan?(nullable1.GetValueOrDefault() - endDate.GetValueOrDefault()) : new TimeSpan?()).Value;
        if (!FiscalPeriodSetupCreator.IsFixedLengthPeriod(current3.FPType))
        {
          nullable1 = current2.EndDate;
          DateTime date1_1 = nullable1.Value;
          nullable1 = masterFinPeriod1.EndDate;
          DateTime date2_1 = nullable1.Value;
          if (this.IsLeapDayPresent(date1_1, date2_1))
          {
            nullable1 = current3.BegFinYear;
            DateTime date1_2 = nullable1.Value;
            nullable1 = current3.BegFinYear;
            DateTime date2_2 = nullable1.Value - timeSpan1;
            if (!this.IsLeapDayPresent(date1_2, date2_2))
            {
              timeSpan1 = timeSpan1.Days > 0 ? timeSpan1.Subtract(new TimeSpan(1, 0, 0, 0)) : timeSpan1.Add(new TimeSpan(1, 0, 0, 0));
              goto label_28;
            }
          }
          nullable1 = current2.EndDate;
          DateTime date1_3 = nullable1.Value;
          nullable1 = masterFinPeriod1.EndDate;
          DateTime date2_3 = nullable1.Value;
          if (this.IsLeapDayPresent(date1_3, date2_3, 28))
          {
            nullable1 = current3.BegFinYear;
            DateTime date1_4 = nullable1.Value;
            nullable1 = current3.BegFinYear;
            DateTime date2_4 = nullable1.Value - timeSpan1;
            if (this.IsLeapDayPresent(date1_4, date2_4))
              timeSpan1 = timeSpan1.Days > 0 ? timeSpan1.Add(new TimeSpan(1, 0, 0, 0)) : timeSpan1.Subtract(new TimeSpan(1, 0, 0, 0));
          }
        }
label_28:
        nullable1 = current3.BegFinYear;
        TimeSpan timeSpan2 = timeSpan1;
        DateTime? nullable2 = nullable1.HasValue ? new DateTime?(nullable1.GetValueOrDefault() - timeSpan2) : new DateTime?();
        nullable1 = current3.PeriodsStartDate;
        TimeSpan timeSpan3 = timeSpan1;
        DateTime? nullable3 = nullable1.HasValue ? new DateTime?(nullable1.GetValueOrDefault() - timeSpan3) : new DateTime?();
        current3.BegFinYear = nullable2;
        current3.PeriodsStartDate = nullable3;
        FinYearSetup finYearSetup = current3;
        nullable1 = current3.PeriodsStartDate;
        dateTime7 = nullable1.Value;
        int? nullable4 = new int?((int) (dateTime7.DayOfWeek + 1));
        finYearSetup.EndYearDayOfWeek = nullable4;
        FiscalYearSetupMaint instance = PXGraph.CreateInstance<FiscalYearSetupMaint>();
        ((PXSelectBase<FinYearSetup>) instance.FiscalYearSetup).Update(current3);
        if (!FiscalPeriodSetupCreator.IsFixedLengthPeriod(current3.FPType))
          ((PXAction) instance.AutoFill).Press();
        ((PXAction) instance.Save).Press();
        current2.BegFinYearHist = nullable2;
        current2.PeriodsStartDateHist = nullable3;
        current2.EndDate = masterFinPeriod1.EndDate;
        ((PXSelectBase<MasterFinYear>) this.FiscalYear).Update(current2);
        break;
      case "N":
        current2.EndDate = masterFinPeriod1.EndDate;
        ((PXSelectBase<MasterFinYear>) this.FiscalYear).Update(current2);
        break;
      case "E":
        masterFinPeriod1.EndDate = current2.EndDate;
        ((PXSelectBase<MasterFinPeriod>) this.Periods).Update(masterFinPeriod1);
        break;
    }
  }

  [PXMergeAttributes]
  [PXDBInt(IsKey = true, BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  [PXParent(typeof (Select<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.organizationID, Equal<Current<OrganizationFinYear.organizationID>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<OrganizationFinYear.organizationID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<MasterFinYear> e)
  {
    MasterFinYear row = e.Row;
    this.VerifyMasterFinYearForDelete(row);
    DateTime dateTime = row.StartDate.Value;
    int month1 = dateTime.Month;
    dateTime = ((PXSelectBase<FinYearSetup>) this.YearSetup).Current.PeriodsStartDate.Value;
    int month2 = dateTime.Month;
    if (month1 == month2 && row.StartDate.Value.Day == ((PXSelectBase<FinYearSetup>) this.YearSetup).Current.PeriodsStartDate.Value.Day)
      return;
    FinYearSetup current = ((PXSelectBase<FinYearSetup>) this.YearSetup).Current;
    MasterFinYear masterFinYear = PXResultset<MasterFinYear>.op_Implicit(PXSelectBase<MasterFinYear, PXSelect<MasterFinYear, Where<MasterFinYear.year, Less<Current<MasterFinYear.year>>>, OrderBy<Desc<MasterFinYear.year>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    if (masterFinYear == null)
      return;
    current.BegFinYear = masterFinYear.BegFinYearHist;
    current.PeriodsStartDate = masterFinYear.PeriodsStartDateHist;
    FiscalYearSetupMaint instance = PXGraph.CreateInstance<FiscalYearSetupMaint>();
    ((PXSelectBase<FinYearSetup>) instance.FiscalYearSetup).Update(current);
    if (!FiscalPeriodSetupCreator.IsFixedLengthPeriod(PXResultset<FinYearSetup>.op_Implicit(((PXSelectBase<FinYearSetup>) this.YearSetup).Select(Array.Empty<object>())).FPType))
    {
      ((PXSelectBase<FinYearSetup>) instance.FiscalYearSetup).Current = current;
      ((PXAction) instance.AutoFill).Press();
      ((PXAction) instance.Save).Press();
    }
    ((PXAction) instance.Save).Press();
  }

  protected virtual void VerifyMasterFinYearForDelete(MasterFinYear masterFinYear)
  {
    if (!this.IsLastMasterFinYear(masterFinYear))
      throw new PXException("Only last financial year can be deleted.");
    if (this.IsMasterFinYearUsed(masterFinYear))
      throw new PXException("You cannot delete a Financial Period which has already been used.");
  }

  protected bool IsMasterFinYearUsed(MasterFinYear masterFinYear)
  {
    return GraphHelper.RowCast<MasterFinPeriod>((IEnumerable) ((PXSelectBase<MasterFinPeriod>) this.Periods).Select(new object[1]
    {
      (object) masterFinYear.Year
    })).Any<MasterFinPeriod>((Func<MasterFinPeriod, bool>) (period => period.Status == "Closed" || period.Status == "Locked" || period.DateLocked.GetValueOrDefault())) || this.IsMasterFinYearReferenced(masterFinYear) || this.IsMasterFinYearUsedInOrganization(masterFinYear.Year);
  }

  private bool IsMasterFinYearReferenced(MasterFinYear masterFinYear)
  {
    using (new PXConnectionScope())
    {
      if (((IQueryable<PXResult<Batch>>) PXSelectBase<Batch, PXSelectReadonly2<Batch, InnerJoin<Branch, On<Batch.branchID, Equal<Branch.branchID>>, InnerJoin<OrganizationFinPeriod, On<Branch.organizationID, Equal<OrganizationFinPeriod.organizationID>, And<Batch.finPeriodID, Equal<OrganizationFinPeriod.finPeriodID>>>, InnerJoin<MasterFinPeriod, On<OrganizationFinPeriod.masterFinPeriodID, Equal<MasterFinPeriod.finPeriodID>>>>>, Where<MasterFinPeriod.finYear, Equal<Required<MasterFinYear.year>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) masterFinYear.Year
      })).Any<PXResult<Batch>>())
        return true;
      return ((IQueryable<PXResult<GLTran>>) PXSelectBase<GLTran, PXSelectReadonly2<GLTran, InnerJoin<Branch, On<GLTran.branchID, Equal<Branch.branchID>>, InnerJoin<OrganizationFinPeriod, On<Branch.organizationID, Equal<OrganizationFinPeriod.organizationID>, And<GLTran.finPeriodID, Equal<OrganizationFinPeriod.finPeriodID>>>, InnerJoin<MasterFinPeriod, On<OrganizationFinPeriod.masterFinPeriodID, Equal<MasterFinPeriod.finPeriodID>>>>>, Where<MasterFinPeriod.finYear, Equal<Required<MasterFinYear.year>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) masterFinYear.Year
      })).Any<PXResult<GLTran>>();
    }
  }

  protected virtual void CheckMasterFinPeriodReferencedInGL(string finPeriodID)
  {
    if (this.IsMasterFinPeriodReferencedInGL(finPeriodID))
      throw new PXException("The financial period \"{0}\" cannot be deleted because at least one General Ledger batch exists for this period.", new object[1]
      {
        (object) PeriodIDAttribute.FormatForError(finPeriodID)
      });
  }

  protected virtual void CheckMasterFinPeriodReferencedInFA(string finPeriodID)
  {
    if (this.IsMasterFinPeriodReferencedInFA(finPeriodID))
      throw new PXException("The {0} financial period cannot be deleted because at least one fixed asset transaction exists for this period in the posting book.", new object[1]
      {
        (object) PeriodIDAttribute.FormatForError(finPeriodID)
      });
  }

  protected virtual void CheckMasterFinPeriodReferenced(string finPeriodID)
  {
    this.CheckMasterFinPeriodReferencedInGL(finPeriodID);
    this.CheckMasterFinPeriodReferencedInFA(finPeriodID);
  }

  protected virtual bool IsMasterFinPeriodReferencedInGL(string finPeriodID)
  {
    using (new PXConnectionScope())
    {
      if (((IQueryable<PXResult<Batch>>) PXSelectBase<Batch, PXSelectReadonly2<Batch, InnerJoin<Branch, On<Batch.branchID, Equal<Branch.branchID>>, InnerJoin<OrganizationFinPeriod, On<Branch.organizationID, Equal<OrganizationFinPeriod.organizationID>, And<Batch.finPeriodID, Equal<OrganizationFinPeriod.finPeriodID>>>, InnerJoin<MasterFinPeriod, On<OrganizationFinPeriod.masterFinPeriodID, Equal<MasterFinPeriod.finPeriodID>>>>>, Where<MasterFinPeriod.finPeriodID, Equal<Required<MasterFinPeriod.finPeriodID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) finPeriodID
      })).Any<PXResult<Batch>>())
        return true;
      return ((IQueryable<PXResult<GLTran>>) PXSelectBase<GLTran, PXSelectReadonly2<GLTran, InnerJoin<Branch, On<GLTran.branchID, Equal<Branch.branchID>>, InnerJoin<OrganizationFinPeriod, On<Branch.organizationID, Equal<OrganizationFinPeriod.organizationID>, And<GLTran.finPeriodID, Equal<OrganizationFinPeriod.finPeriodID>>>, InnerJoin<MasterFinPeriod, On<OrganizationFinPeriod.masterFinPeriodID, Equal<MasterFinPeriod.finPeriodID>>>>>, Where<MasterFinPeriod.finPeriodID, Equal<Required<MasterFinPeriod.finPeriodID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) finPeriodID
      })).Any<PXResult<GLTran>>();
    }
  }

  protected virtual bool IsMasterFinPeriodReferencedInFA(string finPeriodID)
  {
    using (new PXConnectionScope())
      return ((IQueryable<PXResult<FATran>>) PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FABook>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABook.bookID, Equal<FATran.bookID>>>>>.And<BqlOperand<FABook.updateGL, IBqlBool>.IsEqual<True>>>>, FbqlJoins.Inner<Branch>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.branchID, Equal<Branch.branchID>>>>>.Or<BqlOperand<FATran.srcBranchID, IBqlInt>.IsEqual<Branch.branchID>>>>, FbqlJoins.Inner<OrganizationFinPeriod>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Branch.organizationID, Equal<OrganizationFinPeriod.organizationID>>>>>.And<BqlOperand<FATran.finPeriodID, IBqlString>.IsEqual<OrganizationFinPeriod.finPeriodID>>>>, FbqlJoins.Inner<MasterFinPeriod>.On<BqlOperand<OrganizationFinPeriod.masterFinPeriodID, IBqlString>.IsEqual<MasterFinPeriod.finPeriodID>>>>.Where<BqlOperand<MasterFinPeriod.finPeriodID, IBqlString>.IsEqual<P.AsString>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
      {
        (object) finPeriodID
      })).Any<PXResult<FATran>>();
  }

  private bool IsMasterFinYearUsedInOrganization(string year)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>())
      return false;
    return GraphHelper.RowCast<OrganizationFinPeriod>((IEnumerable) PXSelectBase<OrganizationFinPeriod, PXSelectReadonly2<OrganizationFinPeriod, InnerJoin<MasterFinPeriod, On<OrganizationFinPeriod.masterFinPeriodID, Equal<MasterFinPeriod.finPeriodID>>>, Where<MasterFinPeriod.finYear, Equal<Required<MasterFinPeriod.finYear>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) year
    })).Any<OrganizationFinPeriod>();
  }

  private bool IsLastMasterFinYear(MasterFinYear masterFinYear)
  {
    return !GraphHelper.RowCast<MasterFinYear>((IEnumerable) PXSelectBase<MasterFinYear, PXSelectReadonly<MasterFinYear, Where<MasterFinYear.year, Greater<Current<MasterFinYear.year>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) masterFinYear
    }, Array.Empty<object>())).Any<MasterFinYear>();
  }

  protected virtual void MasterFinYear_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    MasterFinYear row = (MasterFinYear) e.Row;
    if (row == null)
      return;
    MasterFinYear nextMasterFinYear = this.GetNextMasterFinYear(row);
    PXCache pxCache = cache;
    MasterFinYear masterFinYear = row;
    bool? customPeriods1 = row.CustomPeriods;
    int num1 = !((customPeriods1.HasValue ? new bool?(!customPeriods1.GetValueOrDefault()) : new bool?()) ?? true) ? 0 : (nextMasterFinYear == null ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<MasterFinYear.customPeriods>(pxCache, (object) masterFinYear, num1 != 0);
    PXCache cache1 = ((PXSelectBase) this.Periods).Cache;
    bool? customPeriods2 = row.CustomPeriods;
    int num2 = !customPeriods2.GetValueOrDefault() ? 0 : (nextMasterFinYear == null ? 1 : 0);
    cache1.AllowDelete = num2 != 0;
    PXCache cache2 = ((PXSelectBase) this.Periods).Cache;
    customPeriods2 = row.CustomPeriods;
    int num3 = !customPeriods2.GetValueOrDefault() ? 0 : (nextMasterFinYear == null ? 1 : 0);
    cache2.AllowInsert = num3 != 0;
    int num4;
    if (this.HasInsertedYear)
    {
      customPeriods2 = row.CustomPeriods;
      num4 = customPeriods2.GetValueOrDefault() ? 1 : 0;
    }
    else
      num4 = 0;
    bool flag = num4 != 0;
    ((PXAction) this.AutoFill).SetVisible(flag);
    ((PXAction) ((PXGraph) this).GetExtension<MasterFinPeriodMaint.GenerateMasterCalendarExtension>()?.GenerateYears).SetEnabled(!flag && !((PXSelectBase) this.Periods).Cache.IsDirty);
    PXResult<MasterFinYear, FinYearSetup> pxResult = (PXResult<MasterFinYear, FinYearSetup>) PXResultset<MasterFinYear>.op_Implicit(PXSelectBase<MasterFinYear, PXSelectJoinOrderBy<MasterFinYear, InnerJoin<FinYearSetup, On<FinYearSetup.firstFinYear, LessEqual<MasterFinYear.year>>>, OrderBy<Asc<MasterFinYear.year>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    customPeriods2 = row.CustomPeriods;
    bool valueOrDefault = customPeriods2.GetValueOrDefault();
    if (valueOrDefault)
      row.FinPeriods = new short?((short) PXSelectBase<MasterFinPeriod, PXSelect<MasterFinPeriod, Where<MasterFinPeriod.finYear, Equal<Required<MasterFinPeriod.finYear>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.Year
      }).Count);
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Periods).Cache, typeof (MasterFinPeriod.length).Name, valueOrDefault);
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Periods).Cache, typeof (MasterFinPeriod.isAdjustment).Name, valueOrDefault);
  }

  protected virtual void MasterFinYear_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    if (this.HasInsertedYear)
    {
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      MasterFinYear row = (MasterFinYear) e.Row;
      if ((FlaggedModeScopeBase<MasterFinPeriodMaint.InsertingOfPreviousYearScope>.IsActive ? (this.CreatePrevYear(row) ? 1 : 0) : (this.CreateNextYear(row) ? 1 : 0)) != 0)
        return;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  [PXMergeAttributes]
  [PXDBInt(IsKey = true, BqlTable = typeof (FinPeriod))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<OrganizationFinPeriod.organizationID> e)
  {
  }

  protected virtual void MasterFinPeriod_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    MasterFinPeriod row = (MasterFinPeriod) e.Row;
    MasterFinYear current1 = ((PXSelectBase<MasterFinYear>) this.FiscalYear).Current;
    MasterFinPeriod current2 = PXResultset<MasterFinPeriod>.op_Implicit(PXSelectBase<MasterFinPeriod, PXSelect<MasterFinPeriod, Where<MasterFinPeriod.finYear, Equal<Current<MasterFinYear.year>>>, OrderBy<Desc<MasterFinPeriod.periodNbr>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    int year = current1.StartDate.Value.Year;
    row.Custom = new bool?(((PXSelectBase<MasterFinYear>) this.FiscalYear).Current.CustomPeriods.GetValueOrDefault() && e.ExternalCall);
    if (new FiscalPeriodCreator<MasterFinYear, MasterFinPeriod>((IYearSetup) ((PXSelectBase<FinYearSetup>) this.YearSetup).Current, current1.Year, new DateTime?(current1.StartDate.Value), (IEnumerable<IPeriodSetup>) GraphHelper.RowCast<FinPeriodSetup>((IEnumerable) ((PXSelectBase<FinPeriodSetup>) this.PeriodsSetup).Select(Array.Empty<object>())))
    {
      Graph = ((PXGraph) this)
    }.fillNextPeriod(row, current2) || row.Custom.GetValueOrDefault())
      return;
    if (!FlaggedModeScopeBase<MasterFinPeriodMaint.MassInsertingOfPeriodsScope>.IsActive)
      throw new PXException("Row cannot be inserted. Data is not inconsistent.");
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void MasterFinPeriod_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    MasterFinPeriod row = (MasterFinPeriod) e.Row;
    MasterFinYear current = ((PXSelectBase<MasterFinYear>) this.FiscalYear).Current;
    DateTime? endDate = row.EndDate;
    DateTime? startDate = row.StartDate;
    if ((endDate.HasValue & startDate.HasValue ? (endDate.GetValueOrDefault() < startDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    row.EndDateUI = row.StartDate;
    PXUIFieldAttribute.SetError<MasterFinPeriod.endDateUI>(cache, (object) row, "End Date may not be less than Start Date");
  }

  protected virtual void MasterFinPeriod_EndDateUI_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    MasterFinPeriod row = (MasterFinPeriod) e.Row;
    if (row == null)
      return;
    DateTime newValue = (DateTime) e.NewValue;
    DateTime? startDateUi = row.StartDateUI;
    if ((startDateUi.HasValue ? (newValue < startDateUi.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException("End Date may not be less than Start Date");
  }

  protected virtual void MasterFinPeriod_EndDateUI_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    MasterFinPeriod row = (MasterFinPeriod) e.Row;
    MasterFinYear current = ((PXSelectBase<MasterFinYear>) this.FiscalYear).Current;
    PXSelectBase<MasterFinPeriod> pxSelectBase = (PXSelectBase<MasterFinPeriod>) new PXSelect<MasterFinPeriod, Where<MasterFinPeriod.finYear, Equal<Current<MasterFinYear.year>>, And<MasterFinPeriod.finPeriodID, Equal<Required<MasterFinPeriod.finPeriodID>>>>>((PXGraph) this);
    MasterFinPeriod masterFinPeriod1 = PXResultset<MasterFinPeriod>.op_Implicit(pxSelectBase.Select(new object[1]
    {
      (object) (int.Parse(row.FinPeriodID) + 1).ToString()
    }));
    DateTime? nullable1;
    DateTime? nullable2;
    if (masterFinPeriod1 != null && e.OldValue != null)
    {
      nullable1 = row.EndDateUI;
      if (nullable1.HasValue)
      {
        nullable1 = row.EndDateUI;
        DateTime oldValue1 = (DateTime) e.OldValue;
        if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() < oldValue1 ? 1 : 0) : 0) == 0)
        {
          nullable1 = row.EndDateUI;
          DateTime oldValue2 = (DateTime) e.OldValue;
          if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() > oldValue2 ? 1 : 0) : 0) != 0)
          {
            nullable2 = row.EndDate;
            DateTime dateTime = nullable2.Value.AddDays(1.0);
            nullable1 = masterFinPeriod1.EndDate;
            if ((nullable1.HasValue ? (dateTime < nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
              goto label_8;
          }
          else
            goto label_8;
        }
        nullable1 = masterFinPeriod1.StartDate;
        nullable2 = masterFinPeriod1.EndDate;
        if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          masterFinPeriod1.EndDate = row.EndDate;
        masterFinPeriod1.StartDate = row.EndDate;
        masterFinPeriod1.Custom = new bool?(true);
        ((PXSelectBase<MasterFinPeriod>) this.Periods).Update(masterFinPeriod1);
        goto label_38;
      }
    }
label_8:
    do
    {
      if (masterFinPeriod1 != null)
      {
        nullable2 = masterFinPeriod1.EndDate;
        nullable1 = row.EndDate;
        if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          MasterFinPeriod masterFinPeriod2 = PXResultset<MasterFinPeriod>.op_Implicit(pxSelectBase.Select(new object[1]
          {
            (object) (int.Parse(masterFinPeriod1.FinPeriodID) + 1).ToString()
          }));
          nullable1 = masterFinPeriod1.StartDate;
          nullable2 = masterFinPeriod1.EndDate;
          if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
          {
            nullable2 = masterFinPeriod1.StartDate;
            nullable1 = masterFinPeriod1.EndDate;
            if ((nullable2.HasValue == nullable1.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0 || masterFinPeriod2 == null)
            {
              if (masterFinPeriod2 == null)
                break;
              goto label_14;
            }
          }
          ((PXSelectBase<MasterFinPeriod>) this.Periods).Delete(masterFinPeriod1);
label_14:
          masterFinPeriod1 = masterFinPeriod2;
        }
      }
      if (masterFinPeriod1 != null)
      {
        nullable1 = masterFinPeriod1.EndDate;
        nullable2 = row.EndDate;
      }
      else
        break;
    }
    while ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0);
    if (masterFinPeriod1 != null)
    {
      List<MasterFinPeriod> masterFinPeriodList = new List<MasterFinPeriod>();
      foreach (PXResult<MasterFinPeriod> pxResult in PXSelectBase<MasterFinPeriod, PXSelect<MasterFinPeriod, Where<MasterFinPeriod.finYear, Equal<Current<MasterFinYear.year>>, And<MasterFinPeriod.finPeriodID, Greater<Required<MasterFinPeriod.finPeriodID>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) int.Parse(row.FinPeriodID).ToString()
      }))
      {
        MasterFinPeriod masterFinPeriod3 = PXResult<MasterFinPeriod>.op_Implicit(pxResult);
        masterFinPeriodList.Add(masterFinPeriod3);
        ((PXSelectBase<MasterFinPeriod>) this.Periods).Delete(masterFinPeriod3);
      }
      if (FiscalPeriodSetupCreator.IsFixedLengthPeriod(((PXSelectBase<FinYearSetup>) this.YearSetup).Current.FPType))
      {
        foreach (MasterFinPeriod masterFinPeriod4 in masterFinPeriodList)
          ((PXSelectBase<MasterFinPeriod>) this.Periods).Insert(masterFinPeriod4);
      }
      else
      {
        foreach (MasterFinPeriod masterFinPeriod5 in masterFinPeriodList)
        {
          MasterFinPeriod masterFinPeriod6 = new MasterFinPeriod();
          MasterFinPeriod masterFinPeriod7 = ((PXSelectBase<MasterFinPeriod>) this.Periods).Insert(masterFinPeriod5);
          ((PXSelectBase) this.Periods).Cache.SetDefaultExt<MasterFinPeriod.noteID>((object) masterFinPeriod7);
          DateTime? nullable3 = masterFinPeriod5.StartDate;
          DateTime? nullable4 = row.EndDate;
          int num = nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() > nullable4.GetValueOrDefault() ? 1 : 0) : 0;
          masterFinPeriod7.StartDate = num == 0 ? row.EndDate : masterFinPeriod5.StartDate;
          masterFinPeriod7.EndDate = masterFinPeriod5.EndDate;
          nullable4 = masterFinPeriod5.StartDate;
          nullable3 = masterFinPeriod5.EndDate;
          if ((nullable4.HasValue == nullable3.HasValue ? (nullable4.HasValue ? (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
            masterFinPeriod7.EndDate = masterFinPeriod7.StartDate;
          masterFinPeriod7.Descr = masterFinPeriod5.Descr;
          ((PXSelectBase<MasterFinPeriod>) this.Periods).Update(masterFinPeriod7);
        }
      }
    }
label_38:
    ((PXSelectBase) this.Periods).View.RequestRefresh();
    row.Custom = new bool?(true);
  }

  protected virtual void MasterFinPeriod_IsAdjustment_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    MasterFinPeriod row = (MasterFinPeriod) e.Row;
    if (e.NewValue == null || !(bool) e.NewValue)
      return;
    DateTime? startDate = row.StartDate;
    row.StartDate = new DateTime?();
    row.EndDate = startDate;
    ((PXSelectBase<MasterFinPeriod>) this.Periods).Update(row);
    row.StartDate = startDate;
    ((PXSelectBase<MasterFinPeriod>) this.Periods).Update(row);
  }

  protected virtual void MasterFinPeriod_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    MasterFinPeriod row = (MasterFinPeriod) e.Row;
    if (row == null)
      return;
    MasterFinYear current = ((PXSelectBase<MasterFinYear>) this.FiscalYear).Current;
    if (current == null)
      return;
    MasterFinYear nextMasterFinYear = this.GetNextMasterFinYear(current);
    int num1;
    if (((PXSelectBase<MasterFinYear>) this.FiscalYear).Current.CustomPeriods.GetValueOrDefault() && nextMasterFinYear == null)
      num1 = PXSelectBase<MasterFinPeriod, PXSelect<MasterFinPeriod, Where<MasterFinPeriod.periodNbr, GreaterEqual<Required<MasterFinPeriod.periodNbr>>, And<MasterFinPeriod.finYear, Equal<Current<MasterFinYear.year>>, And<Where<MasterFinPeriod.dateLocked, Equal<True>, Or<MasterFinPeriod.status, Equal<FinPeriod.status.open>>>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.PeriodNbr
      }).Count == 0 ? 1 : 0;
    else
      num1 = 0;
    bool flag1 = num1 != 0;
    bool flag2 = ((IQueryable<PXResult<FATran>>) PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FABook>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABook.bookID, Equal<FATran.bookID>>>>>.And<BqlOperand<FABook.updateGL, IBqlBool>.IsEqual<True>>>>, FbqlJoins.Inner<Branch>.On<BqlOperand<FATran.branchID, IBqlInt>.IsEqual<Branch.branchID>>>, FbqlJoins.Inner<OrganizationFinPeriod>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Branch.organizationID, Equal<OrganizationFinPeriod.organizationID>>>>>.And<BqlOperand<FATran.finPeriodID, IBqlString>.IsEqual<OrganizationFinPeriod.finPeriodID>>>>, FbqlJoins.Inner<MasterFinPeriod>.On<BqlOperand<OrganizationFinPeriod.masterFinPeriodID, IBqlString>.IsEqual<MasterFinPeriod.finPeriodID>>>>.Where<BqlOperand<MasterFinPeriod.finPeriodID, IBqlString>.IsGreaterEqual<P.AsString>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.FinPeriodID
    })).Any<PXResult<FATran>>();
    PXUIFieldAttribute.SetEnabled<MasterFinPeriod.endDateUI>(cache, (object) row, flag1 && !flag2);
    bool? nullable1;
    if (current.FinPeriods.HasValue && !string.IsNullOrWhiteSpace(row.PeriodNbr))
    {
      nullable1 = row.IsAdjustment;
      if (!nullable1.GetValueOrDefault())
      {
        PXCache pxCache = cache;
        MasterFinPeriod masterFinPeriod = row;
        short? finPeriods = current.FinPeriods;
        int? nullable2 = finPeriods.HasValue ? new int?((int) finPeriods.GetValueOrDefault()) : new int?();
        int num2 = (int) short.Parse(row.PeriodNbr);
        int num3 = !(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue) ? 0 : (nextMasterFinYear == null ? 1 : 0);
        PXUIFieldAttribute.SetEnabled<MasterFinPeriod.isAdjustment>(pxCache, (object) masterFinPeriod, num3 != 0);
      }
    }
    int num4;
    if (this.HasInsertedYear)
    {
      nullable1 = current.CustomPeriods;
      num4 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num4 = 0;
    bool flag3 = num4 != 0;
    PXCache cache1 = ((PXSelectBase) this.Periods).Cache;
    nullable1 = current.CustomPeriods;
    int num5 = !nullable1.GetValueOrDefault() || nextMasterFinYear != null ? 0 : (!this.HasAdjustmentPeriod ? 1 : 0);
    cache1.AllowInsert = num5 != 0;
    ((PXAction) this.AutoFill).SetEnabled(flag3 && !this.HasInsertedPeriods);
  }

  protected virtual void MasterFinYear_CustomPeriods_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (!bool.Parse(e.NewValue.ToString()))
      return;
    if (((PXSelectBase) this.FiscalYear).Ask("Modify Financial Periods", "Are you sure you want to modify financial periods for this year? This action could affect statistics, budgets and data on reports.", (MessageButtons) 4, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
    {
      {
        (WebDialogResult) 6,
        "Modify"
      },
      {
        (WebDialogResult) 7,
        "Cancel"
      }
    }) != 7)
      return;
    e.NewValue = (object) false;
  }

  protected virtual void MasterFinPeriod_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    MasterFinPeriod row = (MasterFinPeriod) e.Row;
    PXEntryStatus status1 = ((PXSelectBase) this.Periods).Cache.GetStatus((object) row);
    PXEntryStatus status2 = ((PXSelectBase) this.FiscalYear).Cache.GetStatus((object) ((PXSelectBase<MasterFinYear>) this.FiscalYear).Current);
    bool flag1 = 4 == status2 || 3 == status2;
    if ((status1 == null || status1 == 1 || status1 == 3) && (row.Status == "Closed" || row.Status == "Locked" || row.DateLocked.GetValueOrDefault()))
      throw new PXException("You cannot delete a Financial Period which has already been used.");
    int result = 0;
    if (int.TryParse(row.PeriodNbr, out result))
    {
      bool flag2 = this.GetNextPeriod(row) == null;
      bool flag3 = false;
      if (flag2)
      {
        string finYear = row.FinYear;
        foreach (PXResult<MasterFinPeriod> pxResult in ((PXSelectBase<MasterFinPeriod>) this.Periods).Select(new object[1]
        {
          (object) finYear
        }))
        {
          DateTime? startDate = PXResult<MasterFinPeriod>.op_Implicit(pxResult).StartDate;
          DateTime dateTime1 = startDate.Value;
          startDate = row.StartDate;
          DateTime dateTime2 = startDate.Value;
          if (dateTime1 > dateTime2)
          {
            flag3 = false;
            break;
          }
        }
      }
      if (!(flag1 | flag2) | flag3 && e.ExternalCall)
        throw new PXException("Financial Period cannot be deleted. You must delete all Financial Periods after this Financial Period first.");
      this.CheckMasterFinPeriodReferenced(row.FinPeriodID);
    }
    GLBudgetLine glBudgetLine = ((PXSelectBase<GLBudgetLine>) new FbqlSelect<SelectFromBase<GLBudgetLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<GLBudgetLineDetail>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<GLBudgetLineDetail.ledgerID, Equal<GLBudgetLine.ledgerID>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<GLBudgetLineDetail.branchID, Equal<GLBudgetLine.branchID>>>>, And<BqlOperand<GLBudgetLineDetail.finYear, IBqlString>.IsEqual<GLBudgetLine.finYear>>>>.And<BqlOperand<GLBudgetLineDetail.groupID, IBqlGuid>.IsEqual<GLBudgetLine.groupID>>>>>>.Where<BqlOperand<GLBudgetLineDetail.finPeriodID, IBqlString>.IsEqual<P.AsString>>, GLBudgetLine>.View((PXGraph) this)).SelectSingle(new object[1]
    {
      (object) row.FinPeriodID
    });
    if (glBudgetLine != null)
    {
      Branch branch = Branch.PK.Find((PXGraph) this, glBudgetLine.BranchID);
      Ledger ledger = Ledger.PK.Find((PXGraph) this, glBudgetLine.LedgerID);
      throw new PXException("The {0} financial period cannot be deleted because a budget has been created for this period in the {1} ledger for the {2} branch.", new object[3]
      {
        (object) PeriodIDAttribute.FormatForError(row.FinPeriodID),
        (object) ledger.LedgerCD,
        (object) branch.BranchCD
      });
    }
  }

  protected virtual void MasterFinPeriod_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    MasterFinPeriod row = (MasterFinPeriod) e.Row;
    string str = row.FinYear + row.PeriodNbr;
    if (row.FinPeriodID != str)
      throw new PXException("The {0} financial period ID does not match the {1} financial year and {2} period number.", new object[3]
      {
        (object) FinPeriodIDFormattingAttribute.FormatForError(row.FinPeriodID),
        (object) row.FinYear,
        (object) row.PeriodNbr
      });
    if (PXDBOperationExt.Command(e.Operation) != 3)
      return;
    this.CheckMasterFinPeriodReferenced(row.FinPeriodID);
  }

  protected virtual void FinPeriodSaveDialog_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    FinPeriodSaveDialog row = (FinPeriodSaveDialog) e.Row;
    if (row == null)
      return;
    MasterFinYear current1 = ((PXSelectBase<MasterFinYear>) this.FiscalYear).Current;
    if (current1 == null)
      return;
    FinYearSetup current2 = ((PXSelectBase<FinYearSetup>) this.YearSetup).Current;
    if (current2 == null)
      return;
    MasterFinPeriod lastPeriod = (MasterFinPeriod) null;
    int num1 = 0;
    if (current1 != null)
    {
      foreach (PXResult<MasterFinPeriod> pxResult in ((PXSelectBase<MasterFinPeriod>) this.Periods).Select(new object[1]
      {
        (object) current1.Year
      }))
      {
        MasterFinPeriod masterFinPeriod = PXResult<MasterFinPeriod>.op_Implicit(pxResult);
        ++num1;
        ((PXSelectBase) this.Periods).Cache.GetStatus((object) masterFinPeriod);
        if (lastPeriod == null)
        {
          lastPeriod = masterFinPeriod;
        }
        else
        {
          DateTime? nullable = masterFinPeriod.StartDate;
          DateTime dateTime1 = nullable.Value;
          nullable = lastPeriod.StartDate;
          DateTime dateTime2 = nullable.Value;
          if (!(dateTime1 > dateTime2))
          {
            nullable = masterFinPeriod.EndDate;
            DateTime dateTime3 = nullable.Value;
            nullable = lastPeriod.EndDate;
            DateTime dateTime4 = nullable.Value;
            if (!(dateTime3 > dateTime4))
              continue;
          }
          nullable = masterFinPeriod.StartDate;
          DateTime dateTime5 = nullable.Value;
          nullable = masterFinPeriod.EndDate;
          DateTime dateTime6 = nullable.Value;
          if (dateTime5 != dateTime6)
            lastPeriod = masterFinPeriod;
        }
      }
    }
    if (lastPeriod == null)
      return;
    Dictionary<string, string> valueLabelDic = new FinPeriodSaveDialog.method.ListAttribute().ValueLabelDic;
    if (!FiscalPeriodSetupCreator.IsFixedLengthPeriod(PXResultset<FinYearSetup>.op_Implicit(((PXSelectBase<FinYearSetup>) this.YearSetup).Select(Array.Empty<object>())).FPType))
      valueLabelDic.Remove("N");
    DateTime? endDate1 = current1.EndDate;
    DateTime? endDate2 = lastPeriod.EndDate;
    if ((endDate1.HasValue & endDate2.HasValue ? (endDate1.GetValueOrDefault() < endDate2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      valueLabelDic.Remove("E");
    PXStringListAttribute.SetList<FinPeriodSaveDialog.method>(sender, e.Row, valueLabelDic.Keys.ToArray<string>(), valueLabelDic.Values.ToArray<string>());
    DateTime dateTime7 = new DateTime();
    DateTime? nullable1;
    DateTime dateTime8;
    if (this.IsWeekBasedPeriod(current2.PeriodType))
    {
      nullable1 = current2.PeriodsStartDate;
      dateTime8 = nullable1.Value;
      int dayOfWeek1 = (int) dateTime8.DayOfWeek;
      nullable1 = lastPeriod.EndDate;
      dateTime8 = nullable1.Value;
      int dayOfWeek2 = (int) dateTime8.DayOfWeek;
      if (dayOfWeek1 != dayOfWeek2)
      {
        PXUIFieldAttribute.SetVisible<FinPeriodSaveDialog.moveDayOfWeek>(sender, (object) row, true);
        nullable1 = current2.PeriodsStartDate;
        dateTime8 = nullable1.Value;
        int dayOfWeek3 = (int) dateTime8.DayOfWeek;
        nullable1 = lastPeriod.EndDate;
        dateTime8 = nullable1.Value;
        int dayOfWeek4 = (int) dateTime8.DayOfWeek;
        int num2 = dayOfWeek3 - dayOfWeek4;
        nullable1 = lastPeriod.EndDate;
        dateTime8 = nullable1.Value;
        int num3 = 7 - (int) dateTime8.DayOfWeek;
        nullable1 = current2.PeriodsStartDate;
        dateTime8 = nullable1.Value;
        int dayOfWeek5 = (int) dateTime8.DayOfWeek;
        int num4 = num3 - dayOfWeek5;
        if (Math.Abs(num2) < num4)
        {
          nullable1 = lastPeriod.EndDate;
          dateTime8 = nullable1.Value;
          DateTime dateTime9 = dateTime8.AddDays((double) num2);
          nullable1 = lastPeriod.StartDate;
          DateTime dateTime10 = nullable1.Value;
          if (dateTime9 > dateTime10)
          {
            nullable1 = lastPeriod.EndDate;
            dateTime8 = nullable1.Value;
            dateTime7 = dateTime8.AddDays((double) num2);
            goto label_30;
          }
        }
        nullable1 = lastPeriod.EndDate;
        dateTime8 = nullable1.Value;
        dateTime7 = dateTime8.AddDays((double) num4);
      }
    }
label_30:
    List<string> stringList1 = new List<string>();
    switch (row.Method)
    {
      case "N":
        List<string> stringList2 = stringList1;
        object[] objArray1 = new object[1];
        nullable1 = lastPeriod.EndDate;
        dateTime8 = nullable1.Value;
        objArray1[0] = (object) dateTime8.ToShortDateString();
        string str1 = PXMessages.LocalizeFormatNoPrefix("the start date of the next financial year will be moved to {0}", objArray1);
        stringList2.Add(str1);
        if (this.IsWeekBasedPeriod(current2.PeriodType))
        {
          nullable1 = current2.PeriodsStartDate;
          dateTime8 = nullable1.Value;
          int dayOfWeek6 = (int) dateTime8.DayOfWeek;
          nullable1 = lastPeriod.EndDate;
          dateTime8 = nullable1.Value;
          int dayOfWeek7 = (int) dateTime8.DayOfWeek;
          if (dayOfWeek6 != dayOfWeek7)
          {
            if (!row.MoveDayOfWeek.Value)
            {
              stringList1.Clear();
              stringList1.Add(PXMessages.LocalizeFormatNoPrefix("the start date of the next financial year will be moved to {0}", new object[1]
              {
                (object) dateTime7.ToShortDateString()
              }));
              List<string> stringList3 = stringList1;
              object[] objArray2 = new object[2];
              dateTime8 = dateTime7.AddDays(-1.0);
              objArray2[0] = (object) dateTime8.ToShortDateString();
              nullable1 = current2.PeriodsStartDate;
              dateTime8 = nullable1.Value;
              objArray2[1] = (object) dateTime8.DayOfWeek.ToString();
              string str2 = PXMessages.LocalizeFormatNoPrefix("the end date of the last financial period will be moved to {0} to preserve the start day of financial periods ({1})", objArray2);
              stringList3.Add(str2);
              break;
            }
            List<string> stringList4 = stringList1;
            object[] objArray3 = new object[2];
            nullable1 = current2.PeriodsStartDate;
            dateTime8 = nullable1.Value;
            objArray3[0] = (object) dateTime8.DayOfWeek.ToString();
            nullable1 = lastPeriod.EndDate;
            dateTime8 = nullable1.Value;
            objArray3[1] = (object) dateTime8.DayOfWeek.ToString();
            string str3 = PXMessages.LocalizeFormatNoPrefix("the start day of financial periods will be moved from {0} to {1}", objArray3);
            stringList4.Add(str3);
            break;
          }
          break;
        }
        break;
      case "Y":
        nullable1 = lastPeriod.EndDate;
        endDate1 = current1.EndDate;
        TimeSpan timeSpan = (nullable1.HasValue & endDate1.HasValue ? new TimeSpan?(nullable1.GetValueOrDefault() - endDate1.GetValueOrDefault()) : new TimeSpan?()).Value;
        int num5 = 0;
        if (!FiscalPeriodSetupCreator.IsFixedLengthPeriod(PXResultset<FinYearSetup>.op_Implicit(((PXSelectBase<FinYearSetup>) this.YearSetup).Select(Array.Empty<object>())).FPType))
        {
          nullable1 = current1.EndDate;
          DateTime date1_1 = nullable1.Value;
          nullable1 = lastPeriod.EndDate;
          DateTime date2_1 = nullable1.Value;
          if (this.IsLeapDayPresent(date1_1, date2_1))
          {
            nullable1 = current2.BegFinYear;
            DateTime date1_2 = nullable1.Value;
            nullable1 = current2.BegFinYear;
            DateTime date2_2 = nullable1.Value + timeSpan;
            if (!this.IsLeapDayPresent(date1_2, date2_2))
            {
              num5 = -1;
              goto label_44;
            }
          }
          nullable1 = current1.EndDate;
          DateTime date1_3 = nullable1.Value;
          nullable1 = lastPeriod.EndDate;
          DateTime date2_3 = nullable1.Value;
          if (this.IsLeapDayPresent(date1_3, date2_3, 28))
          {
            nullable1 = current2.BegFinYear;
            DateTime date1_4 = nullable1.Value;
            nullable1 = current2.BegFinYear;
            DateTime date2_4 = nullable1.Value + timeSpan;
            if (this.IsLeapDayPresent(date1_4, date2_4))
              num5 = 1;
          }
        }
label_44:
        nullable1 = lastPeriod.EndDate;
        endDate1 = current1.EndDate;
        if ((nullable1.HasValue & endDate1.HasValue ? new TimeSpan?(nullable1.GetValueOrDefault() - endDate1.GetValueOrDefault()) : new TimeSpan?()).Value.Days > 0)
        {
          if (this.IsCalendarBasedPeriod(current2.PeriodType))
          {
            List<string> stringList5 = stringList1;
            object[] objArray4 = new object[1];
            nullable1 = current2.BegFinYear;
            dateTime8 = nullable1.Value;
            ref DateTime local = ref dateTime8;
            nullable1 = lastPeriod.EndDate;
            endDate1 = current1.EndDate;
            double num6 = (double) (Math.Abs((nullable1.HasValue & endDate1.HasValue ? new TimeSpan?(nullable1.GetValueOrDefault() - endDate1.GetValueOrDefault()) : new TimeSpan?()).Value.Days) + num5);
            dateTime8 = local.AddDays(num6);
            objArray4[0] = (object) dateTime8.ToString("MMMM dd");
            string str4 = PXMessages.LocalizeFormatNoPrefix("the next financial year will start on {0}", objArray4);
            stringList5.Add(str4);
          }
          else
          {
            List<string> stringList6 = stringList1;
            object[] objArray5 = new object[1];
            nullable1 = lastPeriod.EndDate;
            endDate1 = current1.EndDate;
            objArray5[0] = (object) ((nullable1.HasValue & endDate1.HasValue ? new TimeSpan?(nullable1.GetValueOrDefault() - endDate1.GetValueOrDefault()) : new TimeSpan?()).Value.Days + num5);
            string str5 = PXMessages.LocalizeFormatNoPrefix("the start date of the next financial year will be moved {0} day(s) forward", objArray5);
            stringList6.Add(str5);
          }
        }
        else if (this.IsCalendarBasedPeriod(current2.PeriodType))
        {
          List<string> stringList7 = stringList1;
          object[] objArray6 = new object[1];
          nullable1 = current2.BegFinYear;
          dateTime8 = nullable1.Value;
          ref DateTime local = ref dateTime8;
          nullable1 = lastPeriod.EndDate;
          endDate1 = current1.EndDate;
          double num7 = (double) (-Math.Abs((nullable1.HasValue & endDate1.HasValue ? new TimeSpan?(nullable1.GetValueOrDefault() - endDate1.GetValueOrDefault()) : new TimeSpan?()).Value.Days) - num5);
          dateTime8 = local.AddDays(num7);
          objArray6[0] = (object) dateTime8.ToString("MMMM dd");
          string str6 = PXMessages.LocalizeFormatNoPrefix("the next financial year will start on {0}", objArray6);
          stringList7.Add(str6);
        }
        else
        {
          List<string> stringList8 = stringList1;
          object[] objArray7 = new object[1];
          nullable1 = lastPeriod.EndDate;
          endDate1 = current1.EndDate;
          objArray7[0] = (object) (Math.Abs((nullable1.HasValue & endDate1.HasValue ? new TimeSpan?(nullable1.GetValueOrDefault() - endDate1.GetValueOrDefault()) : new TimeSpan?()).Value.Days) + num5);
          string str7 = PXMessages.LocalizeFormatNoPrefix("the start date of the next financial year will be moved {0} day(s) back", objArray7);
          stringList8.Add(str7);
        }
        if (this.IsWeekBasedPeriod(current2.PeriodType))
        {
          nullable1 = current2.PeriodsStartDate;
          dateTime8 = nullable1.Value;
          int dayOfWeek8 = (int) dateTime8.DayOfWeek;
          nullable1 = lastPeriod.EndDate;
          dateTime8 = nullable1.Value;
          int dayOfWeek9 = (int) dateTime8.DayOfWeek;
          if (dayOfWeek8 != dayOfWeek9)
          {
            if (!row.MoveDayOfWeek.Value)
            {
              stringList1.Clear();
              dateTime8 = dateTime7;
              nullable1 = current1.EndDate;
              if ((nullable1.HasValue ? (dateTime8 != nullable1.GetValueOrDefault() ? 1 : 0) : 1) != 0)
              {
                nullable1 = lastPeriod.EndDate;
                endDate1 = current1.EndDate;
                if ((nullable1.HasValue & endDate1.HasValue ? new TimeSpan?(nullable1.GetValueOrDefault() - endDate1.GetValueOrDefault()) : new TimeSpan?()).Value.Days != 0)
                {
                  nullable1 = lastPeriod.EndDate;
                  endDate1 = current1.EndDate;
                  if ((nullable1.HasValue & endDate1.HasValue ? new TimeSpan?(nullable1.GetValueOrDefault() - endDate1.GetValueOrDefault()) : new TimeSpan?()).Value.Days > 0)
                  {
                    List<string> stringList9 = stringList1;
                    object[] objArray8 = new object[1];
                    dateTime8 = dateTime7;
                    nullable1 = current1.EndDate;
                    objArray8[0] = (object) (nullable1.HasValue ? new TimeSpan?(dateTime8 - nullable1.GetValueOrDefault()) : new TimeSpan?()).Value.Days;
                    string str8 = PXMessages.LocalizeFormatNoPrefix("the start date of the next financial year will be moved {0} day(s) forward", objArray8);
                    stringList9.Add(str8);
                    goto label_60;
                  }
                  List<string> stringList10 = stringList1;
                  object[] objArray9 = new object[1];
                  dateTime8 = dateTime7;
                  nullable1 = current1.EndDate;
                  objArray9[0] = (object) Math.Abs((nullable1.HasValue ? new TimeSpan?(dateTime8 - nullable1.GetValueOrDefault()) : new TimeSpan?()).Value.Days);
                  string str9 = PXMessages.LocalizeFormatNoPrefix("the start date of the next financial year will be moved {0} day(s) back", objArray9);
                  stringList10.Add(str9);
                  goto label_60;
                }
              }
              stringList1.Add("the financial year settings will not be modified");
label_60:
              List<string> stringList11 = stringList1;
              object[] objArray10 = new object[2];
              dateTime8 = dateTime7.AddDays(-1.0);
              objArray10[0] = (object) dateTime8.ToShortDateString();
              nullable1 = current2.PeriodsStartDate;
              dateTime8 = nullable1.Value;
              objArray10[1] = (object) dateTime8.DayOfWeek.ToString();
              string str10 = PXMessages.LocalizeFormatNoPrefix("the end date of the last financial period will be moved to {0} to preserve the start day of financial periods ({1})", objArray10);
              stringList11.Add(str10);
              break;
            }
            List<string> stringList12 = stringList1;
            object[] objArray11 = new object[2];
            nullable1 = current2.PeriodsStartDate;
            dateTime8 = nullable1.Value;
            objArray11[0] = (object) dateTime8.DayOfWeek.ToString();
            nullable1 = lastPeriod.EndDate;
            dateTime8 = nullable1.Value;
            objArray11[1] = (object) dateTime8.DayOfWeek.ToString();
            string str11 = PXMessages.LocalizeFormatNoPrefix("the start day of financial periods will be moved from {0} to {1}", objArray11);
            stringList12.Add(str11);
            break;
          }
          break;
        }
        break;
      case "E":
        List<string> stringList13 = stringList1;
        object[] objArray12 = new object[1];
        nullable1 = current1.EndDate;
        dateTime8 = nullable1.Value;
        dateTime8 = dateTime8.AddDays(-1.0);
        objArray12[0] = (object) dateTime8.ToShortDateString();
        string str12 = PXMessages.LocalizeFormatNoPrefix("the end date of the last financial period will be moved to {0}", objArray12);
        stringList13.Add(str12);
        break;
    }
    row.MethodDescription = string.Join(PXMessages.LocalizeNoPrefix("; "), stringList1.ToArray());
    row.Message = this.ComposeDialogMessage(row, lastPeriod, current1);
  }

  [Obsolete("The MasterFinPeriodMaint.SynchronizeBaseAndOrganizationPeriods must be deleted in 2019r2")]
  public void SynchronizeBaseAndOrganizationPeriods()
  {
    this.SynchronizeMasterAndOrganizationPeriods();
  }

  public void SynchronizeMasterAndOrganizationPeriods()
  {
    PXCache<MasterFinYear> pxCache1 = GraphHelper.Caches<MasterFinYear>((PXGraph) this);
    PXCache<MasterFinPeriod> pxCache2 = GraphHelper.Caches<MasterFinPeriod>((PXGraph) this);
    PXCache<OrganizationFinYear> pxCache3 = GraphHelper.Caches<OrganizationFinYear>((PXGraph) this);
    PXCache<OrganizationFinPeriod> pxCache4 = GraphHelper.Caches<OrganizationFinPeriod>((PXGraph) this);
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>())
    {
      foreach (MasterFinPeriod masterFinPeriod in ((PXCache) pxCache2).Deleted)
      {
        this.CheckMasterFinPeriodReferencedInGL(masterFinPeriod.FinPeriodID);
        Dictionary<(int, string), short> dictionary = new Dictionary<(int, string), short>();
        GraphHelper.EnsureCachePersistence<OrganizationFinPeriod>((PXGraph) this);
        foreach (PXResult<OrganizationFinPeriod> pxResult in PXSelectBase<OrganizationFinPeriod, PXViewOf<OrganizationFinPeriod>.BasedOn<SelectFromBase<OrganizationFinPeriod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<OrganizationFinPeriod.masterFinPeriodID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) masterFinPeriod.FinPeriodID
        }))
        {
          OrganizationFinPeriod organizationFinPeriod = PXResult<OrganizationFinPeriod>.op_Implicit(pxResult);
          (int, string) key = (organizationFinPeriod.OrganizationID.Value, organizationFinPeriod.FinYear);
          if (dictionary.ContainsKey(key))
            dictionary[key]++;
          else
            dictionary[key] = (short) 1;
          GraphHelper.Caches<OrganizationFinPeriod>((PXGraph) this).Delete(organizationFinPeriod);
        }
        GraphHelper.EnsureCachePersistence<OrganizationFinYear>((PXGraph) this);
        foreach (KeyValuePair<(int, string), short> keyValuePair in dictionary)
        {
          OrganizationFinYear organizationFinYear1 = OrganizationFinYear.PK.Find((PXGraph) this, new int?(keyValuePair.Key.Item1), keyValuePair.Key.Item2);
          OrganizationFinYear organizationFinYear2 = organizationFinYear1;
          short? nullable1 = organizationFinYear2.FinPeriods;
          int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
          int num = (int) keyValuePair.Value;
          short? nullable3;
          if (!nullable2.HasValue)
          {
            nullable1 = new short?();
            nullable3 = nullable1;
          }
          else
            nullable3 = new short?((short) (nullable2.GetValueOrDefault() - num));
          organizationFinYear2.FinPeriods = nullable3;
          GraphHelper.Caches<OrganizationFinYear>((PXGraph) this).Update(organizationFinYear1);
        }
      }
    }
    else
    {
      int[] numArray = (int[]) null;
      PXSelectBase<PX.Objects.GL.DAC.Organization> pxSelectBase = (PXSelectBase<PX.Objects.GL.DAC.Organization>) new PXSelect<PX.Objects.GL.DAC.Organization, Where<BqlOperand<PX.Objects.GL.DAC.Organization.organizationType, IBqlString>.IsNotEqual<OrganizationTypes.group>>>((PXGraph) this);
      using (new PXReadBranchRestrictedScope())
      {
        foreach (PXResult<PX.Objects.GL.DAC.Organization> pxResult in pxSelectBase.Select(new object[1]
        {
          (object) numArray
        }))
        {
          PX.Objects.GL.DAC.Organization organization = PXResult<PX.Objects.GL.DAC.Organization>.op_Implicit(pxResult);
          PXProcessing.SetCurrentItem((object) organization);
          try
          {
            foreach (MasterFinYear masterFinYear in ((PXCache) pxCache1).Updated)
            {
              OrganizationFinYear organizationFinYear = PXResultset<OrganizationFinYear>.op_Implicit(PXSelectBase<OrganizationFinYear, PXSelect<OrganizationFinYear, Where<OrganizationFinYear.organizationID, Equal<Required<PX.Objects.GL.DAC.Organization.organizationID>>, And<OrganizationFinYear.year, Equal<Required<MasterFinYear.year>>>>>.Config>.Select((PXGraph) this, new object[2]
              {
                (object) organization.OrganizationID,
                (object) masterFinYear.Year
              }));
              if (organizationFinYear == null)
                throw new PXException("The {0} financial year is inconsistent in the {1} company.", new object[2]
                {
                  (object) masterFinYear.Year,
                  (object) organization.OrganizationCD
                });
              organizationFinYear.StartMasterFinPeriodID = PX.Objects.GL.FinPeriods.FinPeriodUtils.GetFirstFinPeriodIDOfYear((IYear) masterFinYear);
              organizationFinYear.FinPeriods = masterFinYear.FinPeriods;
              organizationFinYear.StartDate = masterFinYear.StartDate;
              organizationFinYear.EndDate = masterFinYear.EndDate;
              pxCache3.Update(organizationFinYear);
            }
            foreach (MasterFinYear masterFinYear in ((PXCache) pxCache1).Inserted)
            {
              if (pxCache3.Insert(new OrganizationFinYear()
              {
                OrganizationID = organization.OrganizationID,
                Year = masterFinYear.Year,
                FinPeriods = masterFinYear.FinPeriods,
                StartMasterFinPeriodID = PX.Objects.GL.FinPeriods.FinPeriodUtils.GetFirstFinPeriodIDOfYear((IYear) masterFinYear),
                StartDate = masterFinYear.StartDate,
                EndDate = masterFinYear.EndDate
              }) == null)
                throw new PXException("The {0} financial year cannot be created for the {1} company.", new object[2]
                {
                  (object) masterFinYear.Year,
                  (object) organization.OrganizationCD
                });
            }
            foreach (MasterFinPeriod masterFinPeriod in ((PXCache) pxCache2).Deleted)
              pxCache4.Delete(PXResultset<OrganizationFinPeriod>.op_Implicit(PXSelectBase<OrganizationFinPeriod, PXSelect<OrganizationFinPeriod, Where<OrganizationFinPeriod.organizationID, Equal<Required<PX.Objects.GL.DAC.Organization.organizationID>>, And<OrganizationFinPeriod.masterFinPeriodID, Equal<Required<MasterFinPeriod.finPeriodID>>>>>.Config>.Select((PXGraph) this, new object[2]
              {
                (object) organization.OrganizationID,
                (object) masterFinPeriod.FinPeriodID
              })) ?? throw new PXException("The {0} financial period is inconsistent in the {1} company.", new object[2]
              {
                (object) FinPeriodIDFormattingAttribute.FormatForError(masterFinPeriod.FinPeriodID),
                (object) organization.OrganizationCD
              }));
            bool flag = PXAccess.FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>();
            foreach (MasterFinPeriod masterFinPeriod in ((PXCache) pxCache2).Updated)
            {
              OrganizationFinPeriod organizationFinPeriod = PXResultset<OrganizationFinPeriod>.op_Implicit(PXSelectBase<OrganizationFinPeriod, PXSelect<OrganizationFinPeriod, Where<OrganizationFinPeriod.organizationID, Equal<Required<PX.Objects.GL.DAC.Organization.organizationID>>, And<OrganizationFinPeriod.masterFinPeriodID, Equal<Required<MasterFinPeriod.finPeriodID>>>>>.Config>.Select((PXGraph) this, new object[2]
              {
                (object) organization.OrganizationID,
                (object) masterFinPeriod.FinPeriodID
              }));
              if (organizationFinPeriod == null)
                throw new PXException("The {0} financial period is inconsistent in the {1} company.", new object[2]
                {
                  (object) FinPeriodIDFormattingAttribute.FormatForError(masterFinPeriod.FinPeriodID),
                  (object) organization.OrganizationCD
                });
              organizationFinPeriod.MasterFinPeriodID = masterFinPeriod.FinPeriodID;
              organizationFinPeriod.FinYear = masterFinPeriod.FinYear;
              organizationFinPeriod.PeriodNbr = masterFinPeriod.PeriodNbr;
              organizationFinPeriod.Custom = masterFinPeriod.Custom;
              organizationFinPeriod.DateLocked = masterFinPeriod.DateLocked;
              organizationFinPeriod.StartDate = masterFinPeriod.StartDate;
              organizationFinPeriod.EndDate = masterFinPeriod.EndDate;
              organizationFinPeriod.Descr = masterFinPeriod.Descr;
              PXDBLocalizableStringAttribute.CopyTranslations<MasterFinPeriod.descr, OrganizationFinPeriod.descr>(((PXSelectBase) this.Periods).Cache, (object) masterFinPeriod, (PXCache) pxCache4, (object) organizationFinPeriod);
              if (flag)
              {
                organizationFinPeriod.Status = masterFinPeriod.Status;
                organizationFinPeriod.ARClosed = masterFinPeriod.ARClosed;
                organizationFinPeriod.APClosed = masterFinPeriod.APClosed;
                organizationFinPeriod.FAClosed = masterFinPeriod.FAClosed;
                organizationFinPeriod.CAClosed = masterFinPeriod.CAClosed;
                organizationFinPeriod.INClosed = masterFinPeriod.INClosed;
              }
              pxCache4.Update(organizationFinPeriod);
            }
            foreach (MasterFinPeriod masterFinPeriod in ((PXCache) pxCache2).Inserted)
            {
              OrganizationFinPeriod organizationFinPeriod = pxCache4.Insert(new OrganizationFinPeriod()
              {
                OrganizationID = organization.OrganizationID,
                FinPeriodID = masterFinPeriod.FinPeriodID,
                MasterFinPeriodID = masterFinPeriod.FinPeriodID,
                FinYear = masterFinPeriod.FinYear,
                PeriodNbr = masterFinPeriod.PeriodNbr,
                Custom = masterFinPeriod.Custom,
                DateLocked = masterFinPeriod.DateLocked,
                StartDate = masterFinPeriod.StartDate,
                EndDate = masterFinPeriod.EndDate,
                Descr = masterFinPeriod.Descr
              });
              PXDBLocalizableStringAttribute.CopyTranslations<MasterFinPeriod.descr, OrganizationFinPeriod.descr>(((PXSelectBase) this.Periods).Cache, (object) masterFinPeriod, (PXCache) pxCache4, (object) organizationFinPeriod);
              if (flag || FlaggedModeScopeBase<MasterFinPeriodMaint.YearsGenerationScope>.IsActive)
              {
                organizationFinPeriod.Status = masterFinPeriod.Status;
                organizationFinPeriod.ARClosed = masterFinPeriod.ARClosed;
                organizationFinPeriod.APClosed = masterFinPeriod.APClosed;
                organizationFinPeriod.FAClosed = masterFinPeriod.FAClosed;
                organizationFinPeriod.CAClosed = masterFinPeriod.CAClosed;
                organizationFinPeriod.INClosed = masterFinPeriod.INClosed;
              }
              if (organizationFinPeriod == null)
                throw new PXException("The {0} financial period cannot be created for the {1} company.", new object[2]
                {
                  (object) FinPeriodIDFormattingAttribute.FormatForError(masterFinPeriod.FinPeriodID),
                  (object) organization.OrganizationCD
                });
            }
          }
          catch (Exception ex)
          {
            PXProcessing.SetError(ex);
          }
          PXProcessing.SetProcessed();
        }
      }
    }
  }

  public virtual void Persist()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    MasterFinPeriodMaint.\u003C\u003Ec__DisplayClass58_0 cDisplayClass580 = new MasterFinPeriodMaint.\u003C\u003Ec__DisplayClass58_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass580.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass580.year = ((PXSelectBase<MasterFinYear>) this.FiscalYear).Current;
    IEnumerable<MasterFinPeriod> periods = (IEnumerable<MasterFinPeriod>) null;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass580.year != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (((PXSelectBase) this.Periods).Cache.IsInsertedUpdatedDeleted && !PXLongOperation.IsLongOperationContext() && this.IsYearPeriodsNotMatch(cDisplayClass580.year.Year))
          ((PXSelectBase<MasterFinPeriod>) this.Periods).Ask("Modify Financial Periods", "Financial periods for the selected year exist in the fixed asset posting book and do not match the periods in the general ledger. To amend the periods in the posting book based on the periods in the general ledger, on the Book Calendars (FA304000) form, click Synchronize FA Calendar with GL.", (MessageButtons) 0, (MessageIcon) 3);
        // ISSUE: reference to a compiler-generated field
        cDisplayClass580.lastPeriod = (MasterFinPeriod) null;
        PXResultset<MasterFinPeriod> pxResultset = ((PXSelectBase<MasterFinPeriod>) this.Periods).Select(Array.Empty<object>());
        foreach (PXResult<MasterFinPeriod> pxResult in pxResultset)
        {
          MasterFinPeriod masterFinPeriod = PXResult<MasterFinPeriod>.op_Implicit(pxResult);
          DateTime? endDate = masterFinPeriod.EndDate;
          DateTime? nullable = masterFinPeriod.StartDate;
          if ((endDate.HasValue & nullable.HasValue ? (endDate.GetValueOrDefault() < nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            throw new PXException("End Date may not be less than Start Date");
          // ISSUE: reference to a compiler-generated field
          if (cDisplayClass580.lastPeriod != null)
          {
            nullable = masterFinPeriod.StartDate;
            DateTime dateTime1 = nullable.Value;
            // ISSUE: reference to a compiler-generated field
            nullable = cDisplayClass580.lastPeriod.StartDate;
            DateTime dateTime2 = nullable.Value;
            if (!(dateTime1 > dateTime2))
            {
              nullable = masterFinPeriod.EndDate;
              DateTime dateTime3 = nullable.Value;
              // ISSUE: reference to a compiler-generated field
              nullable = cDisplayClass580.lastPeriod.EndDate;
              DateTime dateTime4 = nullable.Value;
              // ISSUE: reference to a compiler-generated field
              if (!(dateTime3 > dateTime4) && int.Parse(masterFinPeriod.FinPeriodID) <= int.Parse(cDisplayClass580.lastPeriod.FinPeriodID))
                continue;
            }
          }
          // ISSUE: reference to a compiler-generated field
          cDisplayClass580.lastPeriod = masterFinPeriod;
        }
        // ISSUE: reference to a compiler-generated field
        if (cDisplayClass580.lastPeriod == null)
          throw new PXException("At least one period should be defined.");
        foreach (PXResult<MasterFinPeriod> pxResult in pxResultset)
        {
          MasterFinPeriod masterFinPeriod = PXResult<MasterFinPeriod>.op_Implicit(pxResult);
          DateTime? startDate = masterFinPeriod.StartDate;
          DateTime? endDate = masterFinPeriod.EndDate;
          // ISSUE: reference to a compiler-generated field
          if ((startDate.HasValue == endDate.HasValue ? (startDate.HasValue ? (startDate.GetValueOrDefault() == endDate.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && masterFinPeriod != cDisplayClass580.lastPeriod)
          {
            ((PXSelectBase) this.Periods).Cache.RaiseExceptionHandling<MasterFinPeriod.endDateUI>((object) masterFinPeriod, (object) ((PXSelectBase<MasterFinPeriod>) this.Periods).Current.EndDateUI, (Exception) new PXSetPropertyException("Adjustment period should be the last period of the financial year.", (PXErrorLevel) 5));
            throw new PXException("Adjustment period should be the last period of the financial year.");
          }
        }
        // ISSUE: reference to a compiler-generated field
        DateTime? endDate1 = cDisplayClass580.year.EndDate;
        // ISSUE: reference to a compiler-generated field
        DateTime? endDate2 = cDisplayClass580.lastPeriod.EndDate;
        if ((endDate1.HasValue == endDate2.HasValue ? (endDate1.HasValue ? (endDate1.GetValueOrDefault() != endDate2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        {
          // ISSUE: method pointer
          if (!this.SaveDialog.AskExtFullyValid(new PXView.InitializePanel((object) cDisplayClass580, __methodptr(\u003CPersist\u003Eb__0)), (DialogAnswerType) 1, true))
            return;
          this.ModifyEndYear();
        }
        else
          periods = this.GetUpdatedPeriods();
        this.SynchronizeMasterAndOrganizationPeriods();
      }
      ((PXGraph) this).Persist();
      if (periods != null)
        this.UpdateTaxTranFinDate(periods);
      transactionScope.Complete();
    }
  }

  private string ComposeDialogMessage(
    FinPeriodSaveDialog row,
    MasterFinPeriod lastPeriod,
    MasterFinYear year)
  {
    object[] objArray1 = new object[3];
    object[] objArray2 = new object[2];
    DateTime dateTime1 = lastPeriod.EndDate.Value;
    dateTime1 = dateTime1.AddDays(-1.0);
    objArray2[0] = (object) dateTime1.ToShortDateString();
    DateTime dateTime2 = year.EndDate.Value;
    dateTime2 = dateTime2.AddDays(-1.0);
    objArray2[1] = (object) dateTime2.ToShortDateString();
    objArray1[0] = (object) PXMessages.LocalizeFormatNoPrefix("The end date of the last financial period ({0}) is not the same as the end date of the financial year ({1}). To proceed, you need to modify the financial year configuration.", objArray2);
    objArray1[1] = (object) PXMessages.LocalizeNoPrefix("With the selected method");
    objArray1[2] = (object) row.MethodDescription;
    return PXMessages.LocalizeFormatNoPrefix("{0}\n\r{1}, {2}.", objArray1);
  }

  private bool IsYearPeriodsNotMatch(string year)
  {
    foreach (PXResult<FABookPeriod> pxResult in PXSelectBase<FABookPeriod, PXViewOf<FABookPeriod>.BasedOn<SelectFromBase<FABookPeriod, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FABook>.On<BqlOperand<FABook.bookID, IBqlInt>.IsEqual<FABookPeriod.bookID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.organizationID, Equal<FinPeriod.organizationID.masterValue>>>>, And<BqlOperand<FABookPeriod.startDate, IBqlDateTime>.IsNotEqual<FABookPeriod.endDate>>>, And<BqlOperand<FABook.updateGL, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<FABookPeriod.finYear, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) year
    }))
    {
      FABookPeriod bookPeriod = PXResult<FABookPeriod>.op_Implicit(pxResult);
      MasterFinPeriod masterFinPeriod1 = ((PXSelectBase) this.Periods).Cache.Updated.Cast<MasterFinPeriod>().FirstOrDefault<MasterFinPeriod>((Func<MasterFinPeriod, bool>) (p => p.FinPeriodID == bookPeriod.FinPeriodID));
      DateTime? nullable1;
      DateTime? nullable2;
      if (masterFinPeriod1 != null)
      {
        nullable1 = masterFinPeriod1.StartDate;
        nullable2 = bookPeriod.StartDate;
        if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
        {
          nullable2 = masterFinPeriod1.EndDate;
          nullable1 = bookPeriod.EndDate;
          if ((nullable2.HasValue == nullable1.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
            goto label_6;
        }
        return true;
      }
label_6:
      MasterFinPeriod masterFinPeriod2 = ((PXSelectBase) this.Periods).Cache.Inserted.Cast<MasterFinPeriod>().FirstOrDefault<MasterFinPeriod>((Func<MasterFinPeriod, bool>) (p => p.FinPeriodID == bookPeriod.FinPeriodID));
      if (masterFinPeriod2 != null)
      {
        nullable1 = masterFinPeriod2.StartDate;
        nullable2 = bookPeriod.StartDate;
        if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
        {
          nullable2 = masterFinPeriod2.EndDate;
          nullable1 = bookPeriod.EndDate;
          if ((nullable2.HasValue == nullable1.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
            continue;
        }
        return true;
      }
    }
    return false;
  }

  private IEnumerable<MasterFinPeriod> GetUpdatedPeriods()
  {
    return GraphHelper.RowCast<MasterFinPeriod>((IEnumerable) PXSelectBase<MasterFinPeriod, PXSelectReadonly<MasterFinPeriod, Where<MasterFinPeriod.finYear, Equal<Current<MasterFinYear.year>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Join<MasterFinPeriod, MasterFinPeriod, string, Tuple<MasterFinPeriod, MasterFinPeriod>>(GraphHelper.RowCast<MasterFinPeriod>((IEnumerable) ((PXSelectBase<MasterFinPeriod>) this.Periods).Select(Array.Empty<object>())), (Func<MasterFinPeriod, string>) (fp => fp.FinPeriodID), (Func<MasterFinPeriod, string>) (fp => fp.FinPeriodID), (Func<MasterFinPeriod, MasterFinPeriod, Tuple<MasterFinPeriod, MasterFinPeriod>>) ((o, n) => new Tuple<MasterFinPeriod, MasterFinPeriod>(o, n))).Where<Tuple<MasterFinPeriod, MasterFinPeriod>>((Func<Tuple<MasterFinPeriod, MasterFinPeriod>, bool>) (pair =>
    {
      DateTime? endDate1 = pair.Item1.EndDate;
      DateTime? endDate2 = pair.Item2.EndDate;
      if (endDate1.HasValue != endDate2.HasValue)
        return true;
      return endDate1.HasValue && endDate1.GetValueOrDefault() != endDate2.GetValueOrDefault();
    })).Select<Tuple<MasterFinPeriod, MasterFinPeriod>, MasterFinPeriod>((Func<Tuple<MasterFinPeriod, MasterFinPeriod>, MasterFinPeriod>) (pair => pair.Item2));
  }

  private void UpdateTaxTranFinDate(IEnumerable<MasterFinPeriod> periods)
  {
    foreach (MasterFinPeriod period in periods)
      PXUpdate<Set<TaxTran.finDate, Required<TaxTran.finDate>>, TaxTran, Where<TaxTran.finPeriodID, Equal<Required<MasterFinPeriod.finPeriodID>>, And<TaxTran.taxPeriodID, IsNull>>>.Update((PXGraph) this, new object[2]
      {
        (object) period.EndDate.Value.AddDays(-1.0),
        (object) period.FinPeriodID
      });
  }

  private bool CreateNextYear(MasterFinYear newYear)
  {
    PXSelectReadonly<FinYearSetup> pxSelectReadonly = new PXSelectReadonly<FinYearSetup>((PXGraph) this);
    ((PXSelectBase) pxSelectReadonly).View.Clear();
    return FiscalYearCreator<MasterFinYear, MasterFinPeriod>.CreateNextYear((IYearSetup) ((PXSelectBase) pxSelectReadonly).View.SelectSingle(Array.Empty<object>()), this.FindLatestYear(), newYear);
  }

  private bool CreatePrevYear(MasterFinYear newYear)
  {
    PXSelectReadonly<FinYearSetup> pxSelectReadonly = new PXSelectReadonly<FinYearSetup>((PXGraph) this);
    ((PXSelectBase) pxSelectReadonly).View.Clear();
    return FiscalYearCreator<MasterFinYear, MasterFinPeriod>.CreatePrevYear((IYearSetup) ((PXSelectBase) pxSelectReadonly).View.SelectSingle(Array.Empty<object>()), this.FindEarliestYear(), newYear);
  }

  private bool HasInsertedYear
  {
    get
    {
      return ((PXSelectBase) this.FiscalYear).Cache.Inserted.Cast<MasterFinYear>().Any<MasterFinYear>();
    }
  }

  private bool HasInsertedPeriods
  {
    get
    {
      return ((PXSelectBase) this.Periods).Cache.Inserted.Cast<MasterFinPeriod>().Any<MasterFinPeriod>();
    }
  }

  private bool HasAdjustmentPeriod
  {
    get
    {
      return GraphHelper.RowCast<MasterFinPeriod>((IEnumerable) ((PXSelectBase<MasterFinPeriod>) this.Periods).Select(Array.Empty<object>())).Any<MasterFinPeriod>((Func<MasterFinPeriod, bool>) (period => period.IsAdjustment.GetValueOrDefault()));
    }
  }

  private MasterFinYear FindLatestYear()
  {
    PXSelectReadonly3<MasterFinYear, OrderBy<Desc<MasterFinYear.year>>> pxSelectReadonly3 = new PXSelectReadonly3<MasterFinYear, OrderBy<Desc<MasterFinYear.year>>>((PXGraph) this);
    ((PXSelectBase) pxSelectReadonly3).View.Clear();
    return ((PXSelectBase) pxSelectReadonly3).View.SelectSingle(Array.Empty<object>()) as MasterFinYear;
  }

  private MasterFinYear FindEarliestYear()
  {
    PXSelectReadonly3<MasterFinYear, OrderBy<Asc<MasterFinYear.year>>> pxSelectReadonly3 = new PXSelectReadonly3<MasterFinYear, OrderBy<Asc<MasterFinYear.year>>>((PXGraph) this);
    ((PXSelectBase) pxSelectReadonly3).View.Clear();
    return ((PXSelectBase) pxSelectReadonly3).View.SelectSingle(Array.Empty<object>()) as MasterFinYear;
  }

  private MasterFinYear GetNextMasterFinYear(MasterFinYear aYear)
  {
    return (MasterFinYear) ((PXSelectBase) new PXSelect<MasterFinYear, Where<MasterFinYear.startDate, Greater<Required<MasterFinYear.startDate>>>, OrderBy<Asc<MasterFinPeriod.startDate>>>((PXGraph) this)).View.SelectSingle(new object[1]
    {
      (object) aYear.StartDate.Value
    });
  }

  private bool IsWeekBasedPeriod(string periodType)
  {
    return periodType == "WK" || periodType == "BW" || periodType == "FW" || periodType == "FF" || periodType == "FI" || periodType == "IF";
  }

  private bool IsCalendarBasedPeriod(string periodType)
  {
    return periodType == "BM" || periodType == "MO" || periodType == "QR";
  }

  private bool IsLeapDayPresent(DateTime date1, DateTime date2, int leapDay = 29)
  {
    bool flag = false;
    DateTime dateTime1 = DateTime.MinValue;
    DateTime dateTime2 = DateTime.MinValue;
    if (leapDay == 29)
    {
      if (DateTime.IsLeapYear(date1.Year))
        dateTime1 = new DateTime(date1.Year, 2, leapDay);
      if (DateTime.IsLeapYear(date2.Year))
        dateTime2 = new DateTime(date2.Year, 2, leapDay);
    }
    else
    {
      dateTime1 = new DateTime(date1.Year, 2, leapDay);
      dateTime2 = new DateTime(date2.Year, 2, leapDay);
    }
    if (date1 > date2 && dateTime1 > date2 && date1 >= dateTime1 || date1 < date2 && dateTime1 <= date2 && date1 < dateTime1 || date1 > date2 && dateTime2 > date2 && date1 >= dateTime2 || date1 < date2 && dateTime2 <= date2 && date1 < dateTime2)
      flag = true;
    return flag;
  }

  private MasterFinPeriod GetNextPeriod(MasterFinPeriod aPeriod)
  {
    return (MasterFinPeriod) ((PXSelectBase) new PXSelect<MasterFinPeriod, Where<MasterFinPeriod.startDate, Greater<Required<MasterFinPeriod.startDate>>>, OrderBy<Asc<MasterFinPeriod.startDate>>>((PXGraph) this)).View.SelectSingle(new object[1]
    {
      (object) aPeriod.StartDate.Value
    });
  }

  public class MasterFinPeriodStatusActionsGraphExtension : 
    FinPeriodStatusActionsGraphBaseExtension<MasterFinPeriodMaint, MasterFinYear>
  {
    public static bool IsActive()
    {
      return PXAccess.FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>();
    }
  }

  public class GenerateMasterCalendarExtension : 
    GenerateCalendarExtensionBase<MasterFinPeriodMaint, MasterFinYear>
  {
  }

  public class MassInsertingOfPeriodsScope : 
    FlaggedModeScopeBase<MasterFinPeriodMaint.MassInsertingOfPeriodsScope>
  {
  }

  public class InsertingOfPreviousYearScope : 
    FlaggedModeScopeBase<MasterFinPeriodMaint.InsertingOfPreviousYearScope>
  {
  }

  public class YearsGenerationScope : FlaggedModeScopeBase<MasterFinPeriodMaint.YearsGenerationScope>
  {
  }
}

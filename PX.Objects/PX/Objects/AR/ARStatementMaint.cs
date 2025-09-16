// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARStatementMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.Aging;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

public class ARStatementMaint : PXGraph<ARStatementMaint, ARStatementCycle>
{
  public PXSelect<ARStatementCycle> ARStatementCycleRecord;
  public PXAction<ARStatementCycle> RecreateLast;
  public PXAction<ARStatementCycle> DeleteLast;

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable recreateLast(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARStatementMaint.\u003C\u003Ec__DisplayClass2_0 cDisplayClass20 = new ARStatementMaint.\u003C\u003Ec__DisplayClass2_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.row = ((PXSelectBase<ARStatementCycle>) this.ARStatementCycleRecord).Current;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass20.row != null)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (ARStatementProcess.CheckForUnprocessedPPD((PXGraph) this, cDisplayClass20.row.StatementCycleId, cDisplayClass20.row.LastStmtDate, new int?()))
        throw new PXSetPropertyException("The report cannot be generated. There are documents with unprocessed cash discounts. To proceed, make sure that all documents are processed on the Generate AR Tax Adjustments (AR504500) form and appropriate VAT credit memos are released on the Release AR Documents (AR501000) form.");
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass20.row.LastStmtDate.HasValue)
      {
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass20, __methodptr(\u003CrecreateLast\u003Eb__0)));
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable deleteLast(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARStatementMaint.\u003C\u003Ec__DisplayClass4_0 cDisplayClass40 = new ARStatementMaint.\u003C\u003Ec__DisplayClass4_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass40.row = ((PXSelectBase<ARStatementCycle>) this.ARStatementCycleRecord).Current;
    // ISSUE: reference to a compiler-generated field
    ARStatementCycle row = cDisplayClass40.row;
    if ((row != null ? (row.LastStmtDate.HasValue ? 1 : 0) : 0) != 0)
    {
      Dictionary<WebDialogResult, string> dictionary = new Dictionary<WebDialogResult, string>()
      {
        {
          (WebDialogResult) 6,
          "Delete"
        },
        {
          (WebDialogResult) 7,
          "Cancel"
        }
      };
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (((PXSelectBase) this.ARStatementCycleRecord).View.Ask((object) cDisplayClass40.row, "Delete Customer Statements", $"The customer statements generated on the {cDisplayClass40.row.LastStmtDate:d} date for the {cDisplayClass40.row.StatementCycleId} statement cycle will be deleted. To proceed, click Delete.", (MessageButtons) 4, (IReadOnlyDictionary<WebDialogResult, string>) dictionary, (MessageIcon) 3) != 6)
        return adapter.Get();
    }
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass40, __methodptr(\u003CdeleteLast\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) new List<ARStatementCycle>()
    {
      cDisplayClass40.row
    };
  }

  public virtual void ARStatementCycle_Day01_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ARStatementCycle row = (ARStatementCycle) e.Row;
    if (row == null || !(row.PrepareOn == "C"))
      return;
    if (!ARStatementMaint.IsCorrectDayOfMonth((short?) e.NewValue))
      throw new PXSetPropertyException<ARStatementCycle.day01>("Day Of Month must be number between 1 and 31.");
    if (!ARStatementMaint.IsInCorrectForSomeMonth((short?) e.NewValue))
      return;
    cache.RaiseExceptionHandling<ARStatementCycle.day01>(e.Row, e.NewValue, (Exception) new PXSetPropertyException("If the day of a month is set to {0}, statements will be generated on the last day of a month for the months that are shorter than {0} days.", (PXErrorLevel) 2, new object[1]
    {
      e.NewValue
    }));
  }

  public virtual void ARStatementCycle_Day00_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ARStatementCycle row = (ARStatementCycle) e.Row;
    if (row == null || !(row.PrepareOn == "C") && !(row.PrepareOn == "F"))
      return;
    if (!ARStatementMaint.IsCorrectDayOfMonth((short?) e.NewValue))
      throw new PXSetPropertyException<ARStatementCycle.day00>("Day Of Month must be number between 1 and 31.");
    if (!ARStatementMaint.IsInCorrectForSomeMonth((short?) e.NewValue))
      return;
    cache.RaiseExceptionHandling<ARStatementCycle.day00>(e.Row, e.NewValue, (Exception) new PXSetPropertyException("If the day of a month is set to {0}, statements will be generated on the last day of a month for the months that are shorter than {0} days.", (PXErrorLevel) 2, new object[1]
    {
      e.NewValue
    }));
  }

  protected virtual void ARStatementCycle_AgeDays00_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ARStatementCycle row) || !e.ExternalCall)
      return;
    short? nullable1 = row.AgeDays00;
    int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    int num1 = 0;
    if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
      return;
    nullable1 = row.AgeDays01;
    nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    int num2 = 0;
    if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
      return;
    nullable1 = row.AgeDays02;
    nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    int num3 = 0;
    if (!(nullable2.GetValueOrDefault() == num3 & nullable2.HasValue) || row.AgeMsgCurrent != null || row.AgeMsg00 != null || row.AgeMsg01 != null || row.AgeMsg02 != null || row.AgeMsg03 != null)
      return;
    PXCache cache1 = cache;
    ARStatementCycle statementCycle = row;
    nullable1 = row.AgeDays00;
    int bucketInterval = (int) nullable1.Value;
    ARStatementMaint.FillBucketBoundaries(cache1, statementCycle, (short) bucketInterval);
    ARStatementMaint.FillBucketDescriptions(cache, row);
  }

  private static void FillBucketBoundaries(
    PXCache cache,
    ARStatementCycle statementCycle,
    short bucketInterval)
  {
    cache.SetValueExt<ARStatementCycle.ageDays01>((object) statementCycle, (object) (short) ((int) bucketInterval * 2));
    cache.SetValueExt<ARStatementCycle.ageDays02>((object) statementCycle, (object) (short) ((int) bucketInterval * 3));
  }

  private static void FillBucketDescriptions(PXCache cache, ARStatementCycle statementCycle)
  {
    string[] array = AgingEngine.GetDayAgingBucketDescriptions(AgingDirection.Backwards, (IEnumerable<int>) new int[4]
    {
      0,
      (int) statementCycle.AgeDays00.Value,
      (int) statementCycle.AgeDays01.Value,
      (int) statementCycle.AgeDays02.Value
    }, false).ToArray<string>();
    cache.SetValueExt<ARStatementCycle.ageMsgCurrent>((object) statementCycle, (object) array[0]);
    cache.SetValueExt<ARStatementCycle.ageMsg00>((object) statementCycle, (object) array[1]);
    cache.SetValueExt<ARStatementCycle.ageMsg01>((object) statementCycle, (object) array[2]);
    cache.SetValueExt<ARStatementCycle.ageMsg02>((object) statementCycle, (object) array[3]);
    cache.SetValueExt<ARStatementCycle.ageMsg03>((object) statementCycle, (object) array[4]);
  }

  private static bool IsCorrectDayOfMonth(short? day)
  {
    if (day.HasValue)
    {
      short? nullable1 = day;
      int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      int num1 = 0;
      if (nullable2.GetValueOrDefault() > num1 & nullable2.HasValue)
      {
        nullable1 = day;
        nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
        int num2 = 31 /*0x1F*/;
        return nullable2.GetValueOrDefault() <= num2 & nullable2.HasValue;
      }
    }
    return false;
  }

  private static bool IsInCorrectForSomeMonth(short? day)
  {
    if (!day.HasValue)
      return false;
    short? nullable1 = day;
    int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    int num = 28;
    return nullable2.GetValueOrDefault() > num & nullable2.HasValue;
  }

  public virtual void ARStatementCycle_PrepareOn_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    ARStatementCycle row = (ARStatementCycle) e.Row;
    if (row.PrepareOn == "E")
    {
      row.Day00 = new short?();
      row.Day01 = new short?();
    }
    else if (row.PrepareOn == "F")
    {
      row.Day01 = new short?();
      if (ARStatementMaint.IsCorrectDayOfMonth(row.Day00))
        return;
      row.Day00 = new short?((short) 1);
    }
    else
    {
      if (!(row.PrepareOn == "C"))
        return;
      if (!ARStatementMaint.IsCorrectDayOfMonth(row.Day00))
        row.Day00 = new short?((short) 1);
      if (ARStatementMaint.IsCorrectDayOfMonth(row.Day01))
        return;
      row.Day01 = new short?((short) 1);
    }
  }

  protected virtual void ARStatementCycle_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    ARStatementCycle row = (ARStatementCycle) e.Row;
    PXUIFieldAttribute.SetEnabled<ARStatementCycle.day00>(cache, (object) null, row.PrepareOn == "F" || row.PrepareOn == "C");
    PXUIFieldAttribute.SetEnabled<ARStatementCycle.day01>(cache, (object) null, row.PrepareOn == "C");
    PXUIFieldAttribute.SetEnabled<ARStatementCycle.finChargeID>(cache, (object) null, row.FinChargeApply.GetValueOrDefault());
    bool valueOrDefault = row.FinChargeApply.GetValueOrDefault();
    PXDefaultAttribute.SetPersistingCheck<ARStatementCycle.finChargeID>(cache, (object) row, valueOrDefault ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetRequired<ARStatementCycle.finChargeID>(cache, valueOrDefault);
    PXUIFieldAttribute.SetEnabled<ARStatementCycle.requireFinChargeProcessing>(cache, (object) null, row.FinChargeApply.GetValueOrDefault());
    bool flag = PXContext.PXIdentity.User.IsInRole(PredefinedRoles.FinancialSupervisor);
    PXAction<ARStatementCycle> deleteLast = this.DeleteLast;
    DateTime? lastStmtDate;
    int num1;
    if (flag)
    {
      lastStmtDate = row.LastStmtDate;
      num1 = lastStmtDate.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    ((PXAction) deleteLast).SetEnabled(num1 != 0);
    PXAction<ARStatementCycle> recreateLast = this.RecreateLast;
    lastStmtDate = row.LastStmtDate;
    int num2 = lastStmtDate.HasValue ? 1 : 0;
    ((PXAction) recreateLast).SetEnabled(num2 != 0);
  }

  protected virtual void ARStatementCycle_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    ARStatementCycle row = (ARStatementCycle) e.Row;
    if (row == null || e.Operation == 3)
      return;
    if (row.PrepareOn == "C" || row.PrepareOn == "F")
    {
      if (!ARStatementMaint.IsCorrectDayOfMonth(row.Day00))
      {
        cache.RaiseExceptionHandling<ARStatementCycle.day00>(e.Row, (object) row.Day00, (Exception) new PXSetPropertyException("Day Of Month must be number between 1 and 31.", (PXErrorLevel) 4));
        throw new PXSetPropertyException<ARStatementCycle.day00>("Day Of Month must be number between 1 and 31.");
      }
      if (ARStatementMaint.IsInCorrectForSomeMonth(row.Day00))
        cache.RaiseExceptionHandling<ARStatementCycle.day00>(e.Row, (object) row.Day00, (Exception) new PXSetPropertyException("If the day of a month is set to {0}, statements will be generated on the last day of a month for the months that are shorter than {0} days.", (PXErrorLevel) 2, new object[1]
        {
          (object) row.Day00
        }));
    }
    if (row.PrepareOn == "C")
    {
      if (!ARStatementMaint.IsCorrectDayOfMonth(row.Day01))
      {
        cache.RaiseExceptionHandling<ARStatementCycle.day01>(e.Row, (object) row.Day01, (Exception) new PXSetPropertyException("Day Of Month must be number between 1 and 31.", (PXErrorLevel) 4));
        throw new PXSetPropertyException<ARStatementCycle.day01>("Day Of Month must be number between 1 and 31.");
      }
      if (ARStatementMaint.IsInCorrectForSomeMonth(row.Day01))
        cache.RaiseExceptionHandling<ARStatementCycle.day01>(e.Row, (object) row.Day01, (Exception) new PXSetPropertyException("If the day of a month is set to {0}, statements will be generated on the last day of a month for the months that are shorter than {0} days.", (PXErrorLevel) 2, new object[1]
        {
          (object) row.Day01
        }));
    }
    ARStatementMaint.EnsureAgingBucketBoundariesConsistency(cache, row);
  }

  protected virtual void ARStatementCycle_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    if (e.Row == null)
      return;
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(cache, e.Row, typeof (ARStatement.statementCycleId), (Type) null, (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(cache, e.Row, typeof (Customer.statementCycleId), (Type) null, (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(cache, e.Row, typeof (CustomerClass.statementCycleId), (Type) null, (string) null);
  }

  protected virtual void ARStatementCycle_FinChargeApply_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    ARStatementCycle row = (ARStatementCycle) e.Row;
    if (row.FinChargeApply.GetValueOrDefault())
      return;
    row.FinChargeID = (string) null;
    row.RequireFinChargeProcessing = new bool?(false);
  }

  private static void EnsureAgingBucketBoundariesConsistency(
    PXCache cache,
    ARStatementCycle statementCycle)
  {
    if (statementCycle.UseFinPeriodForAging.GetValueOrDefault())
      return;
    int? nullable1 = statementCycle.Bucket01LowerInclusiveBound;
    short? nullable2 = statementCycle.AgeDays00;
    int? nullable3 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
    if (nullable1.GetValueOrDefault() > nullable3.GetValueOrDefault() & nullable1.HasValue & nullable3.HasValue)
      ARStatementMaint.DisplayBucketBoundaryError<ARStatementCycle.ageDays00>(cache, statementCycle);
    int? nullable4 = statementCycle.Bucket02LowerInclusiveBound;
    nullable2 = statementCycle.AgeDays01;
    nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
    if (nullable4.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable4.HasValue & nullable1.HasValue)
      ARStatementMaint.DisplayBucketBoundaryError<ARStatementCycle.ageDays01>(cache, statementCycle);
    nullable1 = statementCycle.Bucket03LowerInclusiveBound;
    nullable2 = statementCycle.AgeDays02;
    nullable4 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
    if (!(nullable1.GetValueOrDefault() > nullable4.GetValueOrDefault() & nullable1.HasValue & nullable4.HasValue))
      return;
    ARStatementMaint.DisplayBucketBoundaryError<ARStatementCycle.ageDays02>(cache, statementCycle);
  }

  private static void DisplayBucketBoundaryError<TField>(
    PXCache cache,
    ARStatementCycle statementCycle)
    where TField : IBqlField
  {
    cache.RaiseExceptionHandling<TField>((object) statementCycle, cache.GetValue((object) statementCycle, typeof (TField).Name), (Exception) new PXSetPropertyException<TField>("The end day of the aging period should not be earlier than its start day."));
  }
}

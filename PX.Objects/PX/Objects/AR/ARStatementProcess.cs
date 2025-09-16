// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARStatementProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARStatementProcess : PXGraph<
#nullable disable
ARStatementProcess>
{
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXCancel<ARStatementProcess.Parameters> Cancel;
  public PXFilter<ARStatementProcess.Parameters> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<ARStatementCycle, ARStatementProcess.Parameters> CyclesList;

  public ARStatementProcess()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    // ISSUE: method pointer
    ((PXProcessingBase<ARStatementCycle>) this.CyclesList).SetProcessDelegate<StatementCycleProcessBO>(new PXProcessingBase<ARStatementCycle>.ProcessItemDelegate<StatementCycleProcessBO>((object) null, __methodptr(ProcessCycles)));
  }

  protected virtual IEnumerable cycleslist()
  {
    ARStatementProcess statementProcess = this;
    PXResultset<PX.Objects.AR.ARSetup>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARSetup>) statementProcess.ARSetup).Select(Array.Empty<object>()));
    DateTime? nullable1 = (DateTime?) ((PXSelectBase<ARStatementProcess.Parameters>) statementProcess.Filter).Current?.StatementDate;
    DateTime statementDate = nullable1 ?? ((PXGraph) statementProcess).Accessinfo.BusinessDate.Value;
    foreach (PXResult<ARStatementCycle> pxResult in PXSelectBase<ARStatementCycle, PXSelect<ARStatementCycle>.Config>.Select((PXGraph) statementProcess, Array.Empty<object>()))
    {
      ARStatementCycle arStatementCycle1 = PXResult<ARStatementCycle>.op_Implicit(pxResult);
      DateTime? nullable2;
      try
      {
        ARStatementCycle arStatementCycle2 = arStatementCycle1;
        ARStatementProcess graph1 = statementProcess;
        DateTime aBeforeDate = statementDate;
        string prepareOn1 = arStatementCycle1.PrepareOn;
        short? day00 = arStatementCycle1.Day00;
        int? aDay00_1 = day00.HasValue ? new int?((int) day00.GetValueOrDefault()) : new int?();
        short? day01 = arStatementCycle1.Day01;
        int? aDay01_1 = day01.HasValue ? new int?((int) day01.GetValueOrDefault()) : new int?();
        int? dayOfWeek1 = arStatementCycle1.DayOfWeek;
        DateTime? nullable3 = new DateTime?(ARStatementProcess.CalcStatementDateBefore((PXGraph) graph1, aBeforeDate, prepareOn1, aDay00_1, aDay01_1, dayOfWeek1));
        arStatementCycle2.NextStmtDate = nullable3;
        nullable1 = arStatementCycle1.LastStmtDate;
        if (nullable1.HasValue)
        {
          nullable1 = arStatementCycle1.NextStmtDate;
          nullable2 = arStatementCycle1.LastStmtDate;
          if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            ARStatementCycle arStatementCycle3 = arStatementCycle1;
            ARStatementProcess graph2 = statementProcess;
            nullable2 = arStatementCycle1.LastStmtDate;
            DateTime aLastStmtDate = nullable2.Value;
            string prepareOn2 = arStatementCycle1.PrepareOn;
            short? nullable4 = arStatementCycle1.Day00;
            int? aDay00_2 = nullable4.HasValue ? new int?((int) nullable4.GetValueOrDefault()) : new int?();
            nullable4 = arStatementCycle1.Day01;
            int? aDay01_2 = nullable4.HasValue ? new int?((int) nullable4.GetValueOrDefault()) : new int?();
            int? dayOfWeek2 = arStatementCycle1.DayOfWeek;
            DateTime? nullable5 = ARStatementProcess.CalcNextStatementDate((PXGraph) graph2, aLastStmtDate, prepareOn2, aDay00_2, aDay01_2, dayOfWeek2);
            arStatementCycle3.NextStmtDate = nullable5;
          }
        }
      }
      catch (PXFinPeriodException ex)
      {
        arStatementCycle1.NextStmtDate = new DateTime?();
      }
      nullable2 = arStatementCycle1.NextStmtDate;
      DateTime dateTime = statementDate;
      if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() > dateTime ? 1 : 0) : 0) == 0)
      {
        ((PXSelectBase) statementProcess.CyclesList).Cache.SetStatus((object) arStatementCycle1, (PXEntryStatus) 1);
        yield return (object) arStatementCycle1;
      }
    }
  }

  public static bool CheckForUnprocessedPPD(
    PXGraph graph,
    string statementCycleID,
    DateTime? nextStmtDate,
    int? customerID)
  {
    PXSelectBase<ARInvoice> pxSelectBase = (PXSelectBase<ARInvoice>) new PXSelectJoin<ARInvoice, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>, InnerJoin<ARAdjust, On<ARAdjust.adjdDocType, Equal<ARInvoice.docType>, And<ARAdjust.adjdRefNbr, Equal<ARInvoice.refNbr>, And<ARAdjust.released, Equal<True>, And<ARAdjust.voided, NotEqual<True>, And<ARAdjust.pendingPPD, Equal<True>, And<ARAdjust.adjgDocDate, LessEqual<Required<ARAdjust.adjgDocDate>>>>>>>>>>, Where<ARInvoice.pendingPPD, Equal<True>, And<ARInvoice.released, Equal<True>, And<ARInvoice.openDoc, Equal<True>, And<Customer.statementCycleId, Equal<Required<Customer.statementCycleId>>>>>>>(graph);
    if (customerID.HasValue)
      pxSelectBase.WhereAnd<Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>();
    return pxSelectBase.SelectSingle(new object[3]
    {
      (object) nextStmtDate,
      (object) statementCycleID,
      (object) customerID
    }) != null;
  }

  /// <summary>
  /// Returns a boolean flag indicating whether the statement contains no details.
  /// For Balance Brought Forward statements, additionally checks that its
  /// forward balance is not zero.
  /// </summary>
  public static bool IsEmptyStatement(PXGraph graph, ARStatement statement)
  {
    if (statement.StatementType == "B" && (statement.BegBalance.IsNonZero() || statement.CuryBegBalance.IsNonZero() || statement.EndBalance.IsNonZero() ? 0 : (!statement.CuryBegBalance.IsNonZero() ? 1 : 0)) == 0)
      return false;
    IEnumerable<ARStatementDetail> statementDetails = GraphHelper.RowCast<ARStatementDetail>((IEnumerable) PXSelectBase<ARStatementDetail, PXSelect<ARStatementDetail, Where<ARStatementDetail.branchID, Equal<Required<ARStatementDetail.branchID>>, And<ARStatementDetail.curyID, Equal<Required<ARStatementDetail.curyID>>, And<ARStatementDetail.customerID, Equal<Required<ARStatementDetail.customerID>>, And<ARStatementDetail.statementDate, Equal<Required<ARStatementDetail.statementDate>>>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[4]
    {
      (object) statement.BranchID,
      (object) statement.CuryID,
      (object) statement.CustomerID,
      (object) statement.StatementDate
    }));
    return ARStatementProcess.IsEmptyStatement(statement, statementDetails);
  }

  public static bool IsEmptyStatement(
    ARStatement statement,
    IEnumerable<ARStatementDetail> statementDetails)
  {
    if (!(statement.StatementType == "B"))
      return !statementDetails.Any<ARStatementDetail>((Func<ARStatementDetail, bool>) (detail => detail.RefNbr != string.Empty));
    return (statement.BegBalance.IsNonZero() || statement.CuryBegBalance.IsNonZero() || statement.EndBalance.IsNonZero() ? 0 : (!statement.CuryEndBalance.IsNonZero() ? 1 : 0)) != 0 && !statementDetails.Any<ARStatementDetail>((Func<ARStatementDetail, bool>) (detail => detail.RefNbr != string.Empty));
  }

  private static bool CheckForOpenPayments(PXGraph aGraph, string aStatementCycleID)
  {
    return PXResultset<ARPayment>.op_Implicit(PXSelectBase<ARPayment, PXSelectJoin<ARPayment, InnerJoin<Customer, On<ARPayment.customerID, Equal<Customer.bAccountID>>>, Where<Customer.statementCycleId, Equal<Required<Customer.statementCycleId>>, And<ARPayment.openDoc, Equal<True>>>>.Config>.SelectWindowed(aGraph, 0, 1, new object[1]
    {
      (object) aStatementCycleID
    })) != null;
  }

  private static bool CheckForOverdueInvoices(
    PXGraph aGraph,
    string aStatementCycleID,
    DateTime aOpDate)
  {
    return PXResultset<ARBalances>.op_Implicit(PXSelectBase<ARBalances, PXSelectJoin<ARBalances, InnerJoin<Customer, On<ARBalances.customerID, Equal<Customer.bAccountID>>>, Where<Customer.statementCycleId, Equal<Required<Customer.statementCycleId>>, And<ARBalances.oldInvoiceDate, LessEqual<Required<ARBalances.oldInvoiceDate>>>>>.Config>.SelectWindowed(aGraph, 0, 1, new object[2]
    {
      (object) aStatementCycleID,
      (object) aOpDate
    })) != null;
  }

  public virtual void ARStatementCycle_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    ARStatementCycle row = (ARStatementCycle) e.Row;
    PX.Objects.AR.ARSetup arSetup = PXResultset<PX.Objects.AR.ARSetup>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Select(Array.Empty<object>()));
    PXCache.TryDispose((object) cache.GetAttributes(e.Row, (string) null));
    DateTime? nullable1 = row.NextStmtDate;
    if (!nullable1.HasValue)
      cache.DisplayFieldError<ARStatementCycle.nextStmtDate>((object) row, (PXErrorLevel) 3, "The next statement date cannot be determined for a statement cycle with the End of Period schedule type.");
    else if (ARStatementProcess.CheckForUnprocessedPPD((PXGraph) this, row.StatementCycleId, row.NextStmtDate, new int?()))
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
      cache.RaiseExceptionHandling<ARStatementCycle.selected>((object) row, (object) false, (Exception) new PXSetPropertyException("The report cannot be generated. There are documents with unprocessed cash discounts. To proceed, make sure that all documents are processed on the Generate AR Tax Adjustments (AR504500) form and appropriate VAT credit memos are released on the Release AR Documents (AR501000) form.", (PXErrorLevel) 5));
    }
    else
    {
      bool? nullable2 = new bool?();
      bool flag1 = false;
      bool flag2 = false;
      bool? nullable3 = row.RequirePaymentApplication;
      if (nullable3.GetValueOrDefault())
      {
        ref bool? local = ref nullable2;
        string statementCycleId = row.StatementCycleId;
        nullable1 = row.NextStmtDate;
        DateTime aOpDate = nullable1.Value;
        int num = ARStatementProcess.CheckForOverdueInvoices((PXGraph) this, statementCycleId, aOpDate) ? 1 : 0;
        local = new bool?(num != 0);
        if (nullable2.GetValueOrDefault() && ARStatementProcess.CheckForOpenPayments((PXGraph) this, row.StatementCycleId))
          flag1 = true;
      }
      nullable3 = row.FinChargeApply;
      if (nullable3.GetValueOrDefault())
      {
        nullable3 = row.RequireFinChargeProcessing;
        if (nullable3.GetValueOrDefault())
        {
          nullable3 = arSetup.DefFinChargeFromCycle;
          if (nullable3.GetValueOrDefault())
          {
            if (!nullable2.HasValue)
            {
              ref bool? local = ref nullable2;
              string statementCycleId = row.StatementCycleId;
              nullable1 = row.NextStmtDate;
              DateTime aOpDate = nullable1.Value;
              int num = ARStatementProcess.CheckForOverdueInvoices((PXGraph) this, statementCycleId, aOpDate) ? 1 : 0;
              local = new bool?(num != 0);
            }
            if (nullable2.Value)
            {
              nullable1 = row.LastFinChrgDate;
              if (nullable1.HasValue)
              {
                nullable1 = row.LastFinChrgDate;
                DateTime dateTime1 = nullable1.Value;
                nullable1 = row.NextStmtDate;
                DateTime dateTime2 = nullable1.Value;
                if (!(dateTime1 < dateTime2))
                  goto label_18;
              }
              flag2 = true;
            }
          }
        }
      }
label_18:
      if (flag2 & flag1)
        ((PXSelectBase) this.CyclesList).Cache.RaiseExceptionHandling<ARStatementCycle.statementCycleId>((object) row, (object) row.StatementCycleId, (Exception) new PXSetPropertyException("At least one customer with unapplied payments or overdue charges has been found. Before preparing the statement, apply payments and calculate any overdue charges. You can run the auto-application process on the Auto-Apply Payments (AR506000) form.", (PXErrorLevel) 3));
      else if (flag2)
      {
        ((PXSelectBase) this.CyclesList).Cache.RaiseExceptionHandling<ARStatementCycle.statementCycleId>((object) row, (object) row.StatementCycleId, (Exception) new PXSetPropertyException("One or more Customers with overdue documents has been found. It is recommended to run Calculate Overdue Charges process prior to this Statement Cycle closure.", (PXErrorLevel) 3));
      }
      else
      {
        if (!flag1)
          return;
        ((PXSelectBase) this.CyclesList).Cache.RaiseExceptionHandling<ARStatementCycle.statementCycleId>((object) row, (object) row.StatementCycleId, (Exception) new PXSetPropertyException("At least one customer with unapplied payment documents has been found. Before preparing the statement, apply all payments. You can run the auto-application process on the Auto-Apply Payments (AR506000) form.", (PXErrorLevel) 3));
      }
    }
  }

  public static DateTime? CalcNextStatementDate(
    PXGraph graph,
    DateTime aLastStmtDate,
    string aPrepareOn,
    int? aDay00,
    int? aDay01,
    int? dayofWeek)
  {
    DateTime? nullable1 = new DateTime?();
    switch (aPrepareOn)
    {
      case "F":
        nullable1 = new DateTime?(ARStatementProcess.getNextDate((DateTime) new PXDateTime(aLastStmtDate.Year, aLastStmtDate.Month, aDay00 ?? 1), aLastStmtDate, aDay00 ?? 1));
        break;
      case "E":
        DateTime dateTime1 = new DateTime(aLastStmtDate.Year, aLastStmtDate.Month, 1).AddMonths(1);
        nullable1 = dateTime1.Subtract(aLastStmtDate).Days >= 2 ? new DateTime?(dateTime1.AddDays(-1.0)) : new DateTime?(dateTime1.AddMonths(1).AddDays(-1.0));
        break;
      case "C":
        DateTime lhs1 = DateTime.MinValue;
        DateTime rhs1 = DateTime.MinValue;
        int num = !aDay00.HasValue ? 0 : (aDay01.HasValue ? 1 : 0);
        if (aDay00.HasValue)
          lhs1 = (DateTime) new PXDateTime(aLastStmtDate.Year, aLastStmtDate.Month, aDay00.Value);
        if (aDay01.HasValue)
          rhs1 = (DateTime) new PXDateTime(aLastStmtDate.Year, aLastStmtDate.Month, aDay01.Value);
        if (num != 0)
        {
          int lhs2 = aDay00.Value;
          int rhs2 = aDay01.Value;
          Utilities.SwapIfGreater<DateTime>(ref lhs1, ref rhs1);
          Utilities.SwapIfGreater<int>(ref lhs2, ref rhs2);
          nullable1 = !(aLastStmtDate < lhs1) ? (!(aLastStmtDate < rhs1) ? new DateTime?(PXDateTime.DatePlusMonthSetDay(lhs1, 1, lhs2)) : new DateTime?(rhs1)) : new DateTime?(lhs1);
          break;
        }
        DateTime aGuessDate = lhs1 != DateTime.MinValue ? lhs1 : rhs1;
        if (aGuessDate != DateTime.MinValue)
        {
          nullable1 = new DateTime?(ARStatementProcess.getNextDate(aGuessDate, aLastStmtDate, aDay00 ?? aDay01 ?? 1));
          break;
        }
        break;
      case "P":
        try
        {
          IFinPeriodRepository service = graph.GetService<IFinPeriodRepository>();
          string periodIdFromDate = service.GetPeriodIDFromDate(new DateTime?(aLastStmtDate), new int?(0));
          DateTime dateTime2 = service.PeriodEndDate(periodIdFromDate, new int?(0));
          nullable1 = !(dateTime2.Date > aLastStmtDate.Date) ? new DateTime?(service.PeriodEndDate(service.GetOffsetPeriodId(periodIdFromDate, 1, new int?(0)), new int?(0))) : new DateTime?(dateTime2);
          break;
        }
        catch (PXFinPeriodException ex)
        {
          throw new PXFinPeriodException($"{PXLocalizer.Localize("The next statement date cannot be determined for a statement cycle with the End of Period schedule type.")} {ex.MessageNoPrefix}");
        }
      case "W":
        DateTime dateTime3 = aLastStmtDate;
        int? nullable2;
        int dayOfWeek;
        int valueOrDefault;
        do
        {
          dateTime3 = dateTime3.AddDays(1.0);
          dayOfWeek = (int) dateTime3.DayOfWeek;
          nullable2 = dayofWeek;
          valueOrDefault = nullable2.GetValueOrDefault();
        }
        while (!(dayOfWeek == valueOrDefault & nullable2.HasValue));
        nullable1 = new DateTime?(dateTime3);
        break;
      default:
        throw new PXException("The '{0}' statement schedule type is not supported.", new object[1]
        {
          (object) GetLabel.For<ARStatementScheduleType>(aPrepareOn)
        });
    }
    return nullable1;
  }

  public static DateTime CalcStatementDateBefore(
    PXGraph graph,
    DateTime aBeforeDate,
    string aPrepareOn,
    int? aDay00,
    int? aDay01,
    int? dayOfWeek)
  {
    DateTime aBeforeDate1 = DateTime.MinValue;
    switch (aPrepareOn)
    {
      case "F":
        DateTime aGuessDate1 = (DateTime) new PXDateTime(aBeforeDate.Year, aBeforeDate.Month, aDay00 ?? 1);
        if (aGuessDate1.Date == aBeforeDate.Date)
          return aGuessDate1;
        aBeforeDate1 = ARStatementProcess.getPrevDate(aGuessDate1, aBeforeDate, aDay00 ?? 1);
        break;
      case "E":
        if (aBeforeDate.AddDays(1.0).Month != aBeforeDate.Month)
          return aBeforeDate;
        aBeforeDate1 = new DateTime(aBeforeDate.Year, aBeforeDate.Month, 1).AddDays(-1.0);
        break;
      case "C":
        DateTime lhs1 = DateTime.MinValue;
        DateTime rhs1 = DateTime.MinValue;
        int num = !aDay00.HasValue ? 0 : (aDay01.HasValue ? 1 : 0);
        if (aDay00.HasValue)
          lhs1 = (DateTime) new PXDateTime(aBeforeDate.Year, aBeforeDate.Month, aDay00.Value);
        if (aDay01.HasValue)
          rhs1 = (DateTime) new PXDateTime(aBeforeDate.Year, aBeforeDate.Month, aDay01.Value);
        if (num != 0)
        {
          int lhs2 = aDay00.Value;
          int rhs2 = aDay01.Value;
          Utilities.SwapIfGreater<DateTime>(ref lhs1, ref rhs1);
          Utilities.SwapIfGreater<int>(ref lhs2, ref rhs2);
          aBeforeDate1 = !(aBeforeDate >= rhs1) ? (!(aBeforeDate >= lhs1) ? PXDateTime.DatePlusMonthSetDay(rhs1, -1, rhs2) : lhs1) : rhs1;
          break;
        }
        DateTime aGuessDate2 = lhs1 != DateTime.MinValue ? lhs1 : rhs1;
        if (aGuessDate2 != DateTime.MinValue)
        {
          aBeforeDate1 = ARStatementProcess.getPrevDate(aGuessDate2, aBeforeDate1, aDay00 ?? aDay01 ?? 1);
          break;
        }
        break;
      case "P":
        try
        {
          IFinPeriodRepository service = graph.GetService<IFinPeriodRepository>();
          string periodIdFromDate = service.GetPeriodIDFromDate(new DateTime?(aBeforeDate), new int?(0));
          DateTime dateTime = service.PeriodEndDate(periodIdFromDate, new int?(0));
          if (dateTime.Date == aBeforeDate.Date)
            return dateTime;
          string offsetPeriodId = service.GetOffsetPeriodId(periodIdFromDate, -1, new int?(0));
          return service.PeriodEndDate(offsetPeriodId, new int?(0));
        }
        catch (PXFinPeriodException ex)
        {
          throw new PXFinPeriodException($"{PXLocalizer.Localize("The next statement date cannot be determined for a statement cycle with the End of Period schedule type.")} {ex.MessageNoPrefix}");
        }
      case "W":
        int dayOfWeek1 = (int) aBeforeDate.DayOfWeek;
        int? nullable1 = dayOfWeek;
        int valueOrDefault1 = nullable1.GetValueOrDefault();
        if (dayOfWeek1 == valueOrDefault1 & nullable1.HasValue)
          return aBeforeDate;
        DateTime dateTime1 = aBeforeDate;
        while (true)
        {
          int dayOfWeek2 = (int) dateTime1.DayOfWeek;
          int? nullable2 = dayOfWeek;
          int valueOrDefault2 = nullable2.GetValueOrDefault();
          if (!(dayOfWeek2 == valueOrDefault2 & nullable2.HasValue))
            dateTime1 = dateTime1.AddDays(-1.0);
          else
            break;
        }
        return dateTime1;
      default:
        throw new PXException("The '{0}' statement schedule type is not supported.", new object[1]
        {
          (object) GetLabel.For<ARStatementScheduleType>(aPrepareOn)
        });
    }
    return aBeforeDate1;
  }

  public static DateTime FindNextStatementDate(
    PXGraph graph,
    DateTime aBusinessDate,
    ARStatementCycle aCycle)
  {
    DateTime? nullable1;
    ref DateTime? local = ref nullable1;
    PXGraph graph1 = graph;
    DateTime aBeforeDate = aBusinessDate;
    string prepareOn1 = aCycle.PrepareOn;
    short? nullable2 = aCycle.Day00;
    int? aDay00_1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
    nullable2 = aCycle.Day01;
    int? aDay01_1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
    int? dayOfWeek1 = aCycle.DayOfWeek;
    DateTime dateTime = ARStatementProcess.CalcStatementDateBefore(graph1, aBeforeDate, prepareOn1, aDay00_1, aDay01_1, dayOfWeek1);
    local = new DateTime?(dateTime);
    DateTime? nullable3 = aCycle.LastStmtDate;
    if (nullable3.HasValue)
    {
      nullable3 = nullable1;
      DateTime? lastStmtDate = aCycle.LastStmtDate;
      if ((nullable3.HasValue & lastStmtDate.HasValue ? (nullable3.GetValueOrDefault() <= lastStmtDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        PXGraph graph2 = graph;
        lastStmtDate = aCycle.LastStmtDate;
        DateTime aLastStmtDate = lastStmtDate.Value;
        string prepareOn2 = aCycle.PrepareOn;
        nullable2 = aCycle.Day00;
        int? aDay00_2 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
        nullable2 = aCycle.Day01;
        int? aDay01_2 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
        int? dayOfWeek2 = aCycle.DayOfWeek;
        nullable1 = ARStatementProcess.CalcNextStatementDate(graph2, aLastStmtDate, prepareOn2, aDay00_2, aDay01_2, dayOfWeek2);
      }
    }
    return !nullable1.HasValue ? aBusinessDate : nullable1.Value;
  }

  public static DateTime FindNextStatementDateAfter(
    PXGraph graph,
    DateTime aBusinessDate,
    ARStatementCycle aCycle)
  {
    DateTime? nullable1 = new DateTime?();
    DateTime? nullable2;
    short? nullable3;
    if (aCycle.LastStmtDate.HasValue)
    {
      PXGraph graph1 = graph;
      nullable2 = aCycle.LastStmtDate;
      DateTime aLastStmtDate = nullable2.Value;
      string prepareOn = aCycle.PrepareOn;
      nullable3 = aCycle.Day00;
      int? aDay00 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
      nullable3 = aCycle.Day01;
      int? aDay01 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
      int? dayOfWeek = aCycle.DayOfWeek;
      nullable1 = ARStatementProcess.CalcNextStatementDate(graph1, aLastStmtDate, prepareOn, aDay00, aDay01, dayOfWeek);
      nullable2 = nullable1;
      DateTime dateTime = aBusinessDate;
      if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() >= dateTime ? 1 : 0) : 0) != 0)
        return nullable1.Value;
    }
    ref DateTime? local = ref nullable1;
    PXGraph graph2 = graph;
    DateTime aBeforeDate = aBusinessDate;
    string prepareOn1 = aCycle.PrepareOn;
    nullable3 = aCycle.Day00;
    int? aDay00_1 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
    nullable3 = aCycle.Day01;
    int? aDay01_1 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
    int? dayOfWeek1 = aCycle.DayOfWeek;
    DateTime dateTime1 = ARStatementProcess.CalcStatementDateBefore(graph2, aBeforeDate, prepareOn1, aDay00_1, aDay01_1, dayOfWeek1);
    local = new DateTime?(dateTime1);
    DateTime dateTime2;
    do
    {
      PXGraph graph3 = graph;
      DateTime aLastStmtDate = nullable1.Value;
      string prepareOn2 = aCycle.PrepareOn;
      nullable3 = aCycle.Day00;
      int? aDay00_2 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
      nullable3 = aCycle.Day01;
      int? aDay01_2 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
      int? dayOfWeek2 = aCycle.DayOfWeek;
      nullable1 = ARStatementProcess.CalcNextStatementDate(graph3, aLastStmtDate, prepareOn2, aDay00_2, aDay01_2, dayOfWeek2);
      if (nullable1.HasValue)
      {
        nullable2 = nullable1;
        dateTime2 = aBusinessDate;
      }
      else
        break;
    }
    while ((nullable2.HasValue ? (nullable2.GetValueOrDefault() < dateTime2 ? 1 : 0) : 0) != 0);
    return nullable1.Value;
  }

  protected static DateTime getNextDate(DateTime aGuessDate, DateTime aLastStatementDate, int Day)
  {
    return !(aLastStatementDate < aGuessDate) ? PXDateTime.DatePlusMonthSetDay(aGuessDate, 1, Day) : aGuessDate;
  }

  protected static DateTime getPrevDate(DateTime aGuessDate, DateTime aBeforeDate, int Day)
  {
    return !(aGuessDate < aBeforeDate) ? PXDateTime.DatePlusMonthSetDay(aGuessDate, -1, Day) : aGuessDate;
  }

  public class Parameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    /// <summary>
    /// Indicates the date on which the statements are generated.
    /// Defaults to the current business date.
    /// </summary>
    [PXDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Prepare For")]
    public virtual DateTime? StatementDate { get; set; }

    public abstract class statementDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARStatementProcess.Parameters.statementDate>
    {
    }
  }
}

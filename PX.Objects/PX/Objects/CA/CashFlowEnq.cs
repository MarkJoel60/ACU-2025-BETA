// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashFlowEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.CA;

[Serializable]
public class CashFlowEnq : PXGraph<
#nullable disable
CashFlowEnq>
{
  public PXCancel<CashFlowEnq.CashFlowFilter> Cancel;
  public PXAction<CashFlowEnq.CashFlowFilter> ViewReport;
  public PXAction<CashFlowEnq.CashFlowFilter> ViewReport2;
  public PXFilter<CashFlowEnq.CashFlowFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelect<CashFlowForecast> CashFlow;
  public PXSelect<PX.Objects.CA.Light.ARInvoice, Where<PX.Objects.CA.Light.ARInvoice.docType, Equal<Required<PX.Objects.CA.Light.ARInvoice.docType>>, And<PX.Objects.CA.Light.ARInvoice.refNbr, Equal<Required<PX.Objects.CA.Light.ARInvoice.refNbr>>>>> arInvoice;
  public PXSelect<PX.Objects.CA.Light.APInvoice, Where<PX.Objects.CA.Light.APInvoice.docType, Equal<Required<PX.Objects.CA.Light.APInvoice.docType>>, And<PX.Objects.CA.Light.APInvoice.refNbr, Equal<Required<PX.Objects.CA.Light.APInvoice.refNbr>>>>> apInvoice;
  public PXSelect<PX.Objects.GL.Schedule, Where<PX.Objects.GL.Schedule.scheduleID, NotEqual<PX.Objects.GL.Schedule.scheduleID>>> schedule;
  protected readonly string CashFlowReportName = "CA658000";
  protected readonly int AmountFieldsNumber = 30;
  protected DateTime startDate;
  protected DateTime endDate;
  protected PX.Objects.CM.Currency baseCurrency;
  protected PX.Objects.CM.Currency convertToCurrency;
  protected Dictionary<string, CurrencyRate> currencyRates;
  protected Dictionary<int, CurrencyRate> accountRates;
  protected string currencyRateType;
  protected CashAccount defaultCashAccount;

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewReport(PXAdapter adapter)
  {
    PXReportResultset pxReportResultset = new PXReportResultset(new Type[1]
    {
      typeof (CashFlowForecast)
    });
    PXResultset<CashFlowForecast> pxResultset = ((PXSelectBase<CashFlowForecast>) this.CashFlow).Select(Array.Empty<object>());
    Dictionary<int, string> dict = new Dictionary<int, string>();
    PXSelectBase<CashAccount> acctSelect = (PXSelectBase<CashAccount>) new PXSelectReadonly<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>((PXGraph) this);
    pxResultset.Sort((Comparison<PXResult<CashFlowForecast>>) ((o1, o2) =>
    {
      CashFlowForecast cashFlowForecast1 = PXResult<CashFlowForecast>.op_Implicit(o2);
      CashFlowForecast cashFlowForecast2 = PXResult<CashFlowForecast>.op_Implicit(o1);
      int? cashAccountId = cashFlowForecast2.CashAccountID;
      int num1 = cashAccountId.Value;
      cashAccountId = cashFlowForecast1.CashAccountID;
      int num2 = cashAccountId.Value;
      int? nullable;
      int num3;
      if (num1 <= num2)
      {
        int num4 = cashFlowForecast2.CashAccountID.Value;
        nullable = cashFlowForecast1.CashAccountID;
        int num5 = nullable.Value;
        num3 = num4 < num5 ? -1 : 0;
      }
      else
        num3 = 1;
      int num6 = num3;
      if (num6 != 0)
      {
        string strA = string.Empty;
        string strB = string.Empty;
        nullable = cashFlowForecast2.CashAccountID;
        if (nullable.HasValue)
        {
          Dictionary<int, string> dictionary1 = dict;
          nullable = cashFlowForecast2.CashAccountID;
          int key1 = nullable.Value;
          ref string local = ref strA;
          if (!dictionary1.TryGetValue(key1, out local))
          {
            PXSelectBase<CashAccount> pxSelectBase = acctSelect;
            object[] objArray = new object[1];
            nullable = cashFlowForecast2.CashAccountID;
            objArray[0] = (object) nullable.Value;
            CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(pxSelectBase.Select(objArray));
            if (cashAccount != null)
              strA = cashAccount.CashAccountCD;
            Dictionary<int, string> dictionary2 = dict;
            nullable = cashFlowForecast2.CashAccountID;
            int key2 = nullable.Value;
            string str = strA;
            dictionary2.Add(key2, str);
          }
        }
        nullable = cashFlowForecast1.CashAccountID;
        if (nullable.HasValue)
        {
          Dictionary<int, string> dictionary3 = dict;
          nullable = cashFlowForecast1.CashAccountID;
          int key3 = nullable.Value;
          ref string local = ref strB;
          if (!dictionary3.TryGetValue(key3, out local))
          {
            PXSelectBase<CashAccount> pxSelectBase = acctSelect;
            object[] objArray = new object[1];
            nullable = cashFlowForecast1.CashAccountID;
            objArray[0] = (object) nullable.Value;
            CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(pxSelectBase.Select(objArray));
            if (cashAccount != null)
              strB = cashAccount.CashAccountCD;
            Dictionary<int, string> dictionary4 = dict;
            nullable = cashFlowForecast1.CashAccountID;
            int key4 = nullable.Value;
            string str = strB;
            dictionary4.Add(key4, str);
          }
        }
        num6 = string.Compare(strA, strB);
      }
      if (num6 != 0)
        return num6;
      nullable = cashFlowForecast2.RecordType;
      int num7 = nullable.Value;
      nullable = cashFlowForecast1.RecordType;
      int num8 = nullable.Value;
      if (num7 == num8)
      {
        nullable = cashFlowForecast2.BAccountID;
        int num9 = nullable.HasValue ? 1 : 0;
        nullable = cashFlowForecast1.BAccountID;
        int num10 = nullable.HasValue ? 1 : 0;
        if (num9 == num10)
          return 0;
        nullable = cashFlowForecast2.BAccountID;
        return !nullable.HasValue ? 1 : -1;
      }
      nullable = cashFlowForecast2.RecordType;
      int num11 = 0;
      if (nullable.GetValueOrDefault() == num11 & nullable.HasValue)
        return -1;
      nullable = cashFlowForecast1.RecordType;
      if (nullable.Value == 0)
        return 1;
      nullable = cashFlowForecast1.RecordType;
      int num12 = nullable.Value;
      nullable = cashFlowForecast2.RecordType;
      int num13 = nullable.Value;
      return num12 - num13;
    }));
    foreach (PXResult<CashFlowForecast> pxResult in pxResultset)
    {
      CashFlowForecast cashFlowForecast = PXResult<CashFlowForecast>.op_Implicit(pxResult);
      pxReportResultset.Add(new object[1]
      {
        (object) cashFlowForecast
      });
    }
    throw new PXReportRequiredException((IPXResultset) pxReportResultset, this.CashFlowReportName, "Cash Flow Forecast Report", (CurrentLocalization) null);
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable viewReport2(PXAdapter adapter)
  {
    List<CashFlowForecast2> dest = new List<CashFlowForecast2>();
    PXReportResultset pxReportResultset = new PXReportResultset(new Type[1]
    {
      typeof (CashFlowForecast2)
    });
    foreach (PXResult<CashFlowForecast> pxResult in ((PXSelectBase<CashFlowForecast>) this.CashFlow).Select(Array.Empty<object>()))
    {
      CashFlowForecast src = PXResult<CashFlowForecast>.op_Implicit(pxResult);
      this.Convert(dest, src, true);
    }
    dest.Sort((Comparison<CashFlowForecast2>) ((op1, op2) =>
    {
      DateTime? tranDate1 = op1.TranDate;
      DateTime dateTime1 = tranDate1.Value;
      tranDate1 = op2.TranDate;
      DateTime dateTime2 = tranDate1.Value;
      if (dateTime1 > dateTime2)
        return 1;
      DateTime? tranDate2 = op1.TranDate;
      DateTime dateTime3 = tranDate2.Value;
      tranDate2 = op2.TranDate;
      DateTime dateTime4 = tranDate2.Value;
      if (dateTime3 < dateTime4)
        return -1;
      int? nullable = op1.CashAccountID;
      int num1 = nullable.Value;
      nullable = op2.CashAccountID;
      int num2 = nullable.Value;
      if (num1 > num2)
        return 1;
      nullable = op1.CashAccountID;
      int num3 = nullable.Value;
      nullable = op2.CashAccountID;
      int num4 = nullable.Value;
      if (num3 < num4)
        return -1;
      nullable = op1.RecordType;
      int num5 = nullable.Value;
      nullable = op2.RecordType;
      int num6 = nullable.Value;
      if (num5 == num6)
      {
        nullable = op1.BAccountID;
        int num7 = nullable.HasValue ? 1 : 0;
        nullable = op2.BAccountID;
        int num8 = nullable.HasValue ? 1 : 0;
        if (num7 == num8)
          return 0;
        nullable = op1.BAccountID;
        return !nullable.HasValue ? 1 : -1;
      }
      nullable = op1.RecordType;
      int num9 = 0;
      if (nullable.GetValueOrDefault() == num9 & nullable.HasValue)
        return -1;
      nullable = op2.RecordType;
      if (nullable.Value == 0)
        return 1;
      nullable = op2.RecordType;
      int num10 = nullable.Value;
      nullable = op1.RecordType;
      int num11 = nullable.Value;
      return num10 - num11;
    }));
    foreach (CashFlowForecast2 cashFlowForecast2 in dest)
      pxReportResultset.Add(new object[1]
      {
        (object) cashFlowForecast2
      });
    throw new PXReportRequiredException((IPXResultset) pxReportResultset, "CA660012", "Cash Flow Forecast Report", (CurrentLocalization) null);
  }

  public CashFlowEnq()
  {
    ((PXSelectBase) this.CashFlow).Cache.AllowDelete = false;
    ((PXSelectBase) this.CashFlow).Cache.AllowInsert = false;
    ((PXSelectBase) this.CashFlow).Cache.AllowUpdate = false;
  }

  protected virtual void CashFlowFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CashFlowEnq.CashFlowFilter row = (CashFlowEnq.CashFlowFilter) e.Row;
    if (row == null)
      return;
    DateTime? startDate = row.StartDate;
    bool? nullable;
    if (startDate.HasValue)
    {
      PXCache cach = ((PXGraph) this).Caches[typeof (CashFlowForecast)];
      nullable = row.SummaryOnly;
      bool flag1 = false;
      bool flag2 = nullable.GetValueOrDefault() == flag1 & nullable.HasValue;
      for (int index = 0; index < this.AmountFieldsNumber; ++index)
      {
        startDate = row.StartDate;
        DateTime dateTime = startDate.Value.AddDays((double) index);
        string str = "CuryAmountDay" + index.ToString();
        string displayName = PXUIFieldAttribute.GetDisplayName(cach, str);
        if (dateTime.ToString("yyyy-MM-dd") != displayName)
        {
          PXUIFieldAttribute.SetDisplayName(cach, str, dateTime.ToString("yyyy-MM-dd"));
          PXUIFieldAttribute.SetNeutralDisplayName(cach, str, " ");
        }
        PXUIFieldAttribute.SetVisible(cach, str, flag2 || index == 0);
      }
    }
    PXCache pxCache1 = sender;
    CashFlowEnq.CashFlowFilter cashFlowFilter = row;
    nullable = row.IncludeUnassignedDocs;
    int? cashAccountId;
    int num1;
    if (nullable.GetValueOrDefault())
    {
      cashAccountId = row.CashAccountID;
      num1 = !cashAccountId.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetEnabled<CashFlowEnq.CashFlowFilter.defaultAccountID>(pxCache1, (object) cashFlowFilter, num1 != 0);
    PXCache pxCache2 = sender;
    nullable = row.IncludeUnassignedDocs;
    int num2;
    if (nullable.GetValueOrDefault())
    {
      cashAccountId = row.CashAccountID;
      num2 = !cashAccountId.HasValue ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetRequired<CashFlowEnq.CashFlowFilter.defaultAccountID>(pxCache2, num2 != 0);
    bool flag = !string.IsNullOrEmpty(row.CuryID);
    PXUIFieldAttribute.SetEnabled<CashFlowEnq.CashFlowFilter.curyRateTypeID>(sender, (object) row, flag);
    PXUIFieldAttribute.SetRequired<CashFlowEnq.CashFlowFilter.curyRateTypeID>(sender, flag);
    nullable = row.AllCashAccounts;
    if (nullable.GetValueOrDefault())
      this.EnableAccountId(sender, row, false);
    else
      this.EnableAccountId(sender, row, true);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CashFlowEnq.CashFlowFilter, CashFlowEnq.CashFlowFilter.includeUnassignedDocs> e)
  {
    if (e.Row == null)
      return;
    bool? includeUnassignedDocs = e.Row.IncludeUnassignedDocs;
    bool flag = false;
    if (!(includeUnassignedDocs.GetValueOrDefault() == flag & includeUnassignedDocs.HasValue))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CashFlowEnq.CashFlowFilter, CashFlowEnq.CashFlowFilter.includeUnassignedDocs>>) e).Cache.SetValueExt<CashFlowEnq.CashFlowFilter.defaultAccountID>((object) e.Row, (object) null);
  }

  protected virtual void CashFlowFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    CashFlowEnq.CashFlowFilter row = (CashFlowEnq.CashFlowFilter) e.Row;
    if (row == null || !row.OrgBAccountID.HasValue)
      return;
    row.OrganizationBaseCuryID = PXOrgAccess.GetBaseCuryID(row.OrgBAccountID);
  }

  protected virtual void CashFlowFilter_OrgBAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<CashFlowEnq.CashFlowFilter.cashAccountID>(e.Row);
  }

  private void EnableAccountId(PXCache sender, CashFlowEnq.CashFlowFilter row, bool enable)
  {
    PXUIFieldAttribute.SetEnabled<CashFlowEnq.CashFlowFilter.cashAccountID>(sender, (object) row, enable);
    PXUIFieldAttribute.SetRequired<CashFlowEnq.CashFlowFilter.cashAccountID>(sender, enable);
  }

  protected virtual void CashFlowFilter_AllCashAccounts_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CashFlowEnq.CashFlowFilter row = e.Row as CashFlowEnq.CashFlowFilter;
    if (!row.AllCashAccounts.GetValueOrDefault())
      return;
    row.CashAccountID = new int?();
  }

  protected virtual void CashFlowFilter_IncludeScheduled_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    CashFlowEnq.CashFlowFilter row = (CashFlowEnq.CashFlowFilter) e.Row;
    if (!(bool) e.NewValue)
      return;
    this.startDate = row.StartDate ?? DateTime.Now;
    this.endDate = this.startDate.AddDays((double) this.AmountFieldsNumber);
    if (this.DetectPotentialScheduleBreak(this.endDate))
      throw new PXSetPropertyException("Financial Periods are not defined for the date range provided. Scheduled documents may not be included correctly");
  }

  protected virtual void CashFlowFilter_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CashFlowEnq.CashFlowFilter row = (CashFlowEnq.CashFlowFilter) e.Row;
    if (row != null && !string.IsNullOrEmpty(row.CuryID))
    {
      PXResult<CashAccount> pxResult1 = (PXResult<CashAccount>) null;
      foreach (PXResult<CashAccount> pxResult2 in PXSelectBase<CashAccount, PXSelectGroupBy<CashAccount, Where<CashAccount.curyRateTypeID, IsNotNull>, Aggregate<GroupBy<CashAccount.curyRateTypeID, Count>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
      {
        if (pxResult1 != null)
        {
          int? rowCount1 = ((PXResult) pxResult1).RowCount;
          int? rowCount2 = ((PXResult) pxResult2).RowCount;
          if (!(rowCount1.GetValueOrDefault() < rowCount2.GetValueOrDefault() & rowCount1.HasValue & rowCount2.HasValue))
            continue;
        }
        pxResult1 = pxResult2;
      }
      if (pxResult1 != null)
      {
        e.NewValue = (object) PXResult<CashAccount>.op_Implicit(pxResult1).CuryRateTypeID;
        ((CancelEventArgs) e).Cancel = true;
      }
      else
      {
        CMSetup cmSetup = PXResultset<CMSetup>.op_Implicit(PXSelectBase<CMSetup, PXSelect<CMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
        e.NewValue = (object) cmSetup?.CARateTypeDflt;
        ((CancelEventArgs) e).Cancel = true;
      }
    }
    else
    {
      e.NewValue = (object) null;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void CashFlowFilter_CuryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<CashFlowEnq.CashFlowFilter.curyRateTypeID>(e.Row);
  }

  public IEnumerable cashFlow()
  {
    CashFlowEnq cashFlowEnq = this;
    CashFlowEnq.CashFlowFilter current = ((PXSelectBase<CashFlowEnq.CashFlowFilter>) cashFlowEnq.Filter).Current;
    if (current != null && (!current.IncludeUnassignedDocs.GetValueOrDefault() || current.CashAccountID.HasValue || current.DefaultAccountID.HasValue))
    {
      bool? nullable;
      if (!current.CashAccountID.HasValue)
      {
        nullable = current.AllCashAccounts;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          yield break;
      }
      cashFlowEnq.startDate = current.StartDate ?? DateTime.Now;
      cashFlowEnq.endDate = cashFlowEnq.startDate.AddDays((double) cashFlowEnq.AmountFieldsNumber);
      nullable = current.IncludeScheduled;
      if ((!nullable.GetValueOrDefault() || !cashFlowEnq.DetectPotentialScheduleBreak(cashFlowEnq.endDate)) && (string.IsNullOrEmpty(current.CuryID) || !string.IsNullOrEmpty(current.CuryRateTypeID)))
      {
        string str = PXAccess.GetBranchByBAccountID(current.OrgBAccountID)?.BaseCuryID ?? ((PXAccess.Organization) PXAccess.GetOrganizationByBAccountID(current.OrgBAccountID))?.BaseCuryID;
        cashFlowEnq.baseCurrency = PXResultset<PX.Objects.CM.Currency>.op_Implicit(PXSelectBase<PX.Objects.CM.Currency, PXSelect<PX.Objects.CM.Currency, Where<PX.Objects.CM.Currency.curyID, Equal<Required<PX.Objects.GL.Branch.baseCuryID>>>>.Config>.Select((PXGraph) cashFlowEnq, new object[1]
        {
          (object) str
        }));
        cashFlowEnq.defaultCashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) cashFlowEnq, new object[1]
        {
          (object) (current.CashAccountID ?? current.DefaultAccountID)
        }));
        if (!string.IsNullOrEmpty(current.CuryID))
        {
          cashFlowEnq.convertToCurrency = PXResultset<PX.Objects.CM.Currency>.op_Implicit(PXSelectBase<PX.Objects.CM.Currency, PXSelectReadonly<PX.Objects.CM.Currency, Where<PX.Objects.CM.Currency.curyID, Equal<Required<PX.Objects.CM.Currency.curyID>>>>.Config>.Select((PXGraph) cashFlowEnq, new object[1]
          {
            (object) current.CuryID
          }));
          cashFlowEnq.currencyRateType = current.CuryRateTypeID;
        }
        Dictionary<int, CashFlowForecast> dictionary = new Dictionary<int, CashFlowForecast>(1);
        cashFlowEnq.accountRates = new Dictionary<int, CurrencyRate>(1);
        cashFlowEnq.currencyRates = new Dictionary<string, CurrencyRate>(1);
        cashFlowEnq.CalcCashAccountBalance(cashFlowEnq.startDate, current, dictionary);
        Dictionary<CashFlowEnq.FlowKey, CashFlowForecast> incomingFlow = new Dictionary<CashFlowEnq.FlowKey, CashFlowForecast>(1);
        cashFlowEnq.RetrieveAPInvoices(incomingFlow, current);
        nullable = current.IncludeScheduled;
        if (nullable.GetValueOrDefault())
          cashFlowEnq.RetrieveAPScheduled(incomingFlow, current);
        cashFlowEnq.RetrieveAPPayments(incomingFlow, current);
        cashFlowEnq.RetrieveARInvoices(incomingFlow, current);
        nullable = current.IncludeScheduled;
        if (nullable.GetValueOrDefault())
          cashFlowEnq.RetrieveARScheduled(incomingFlow, current);
        cashFlowEnq.RetrieveARPayments(incomingFlow, current);
        cashFlowEnq.RetriveCashForecasts(incomingFlow, current);
        cashFlowEnq.RecalcSummmary(dictionary, incomingFlow);
        foreach (object obj in dictionary.Values)
          yield return obj;
        foreach (CashFlowForecast cashFlowForecast in incomingFlow.Values)
        {
          if (!cashFlowForecast.IsZero())
            yield return (object) cashFlowForecast;
        }
      }
    }
  }

  protected virtual void RetrieveARInvoices(
    Dictionary<CashFlowEnq.FlowKey, CashFlowForecast> flow,
    CashFlowEnq.CashFlowFilter filter)
  {
    PXSelectBase<PX.Objects.CA.Light.ARInvoice> pxSelectBase = (PXSelectBase<PX.Objects.CA.Light.ARInvoice>) new PXSelectReadonly2<PX.Objects.CA.Light.ARInvoice, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.CA.Light.ARInvoice.curyInfoID>>, LeftJoin<CashAccount, On<CashAccount.cashAccountID, Equal<PX.Objects.CA.Light.ARInvoice.cashAccountID>>>>, Where<IIf<Where<PX.Objects.CA.Light.ARRegister.dueDate, IsNull, And<PX.Objects.CA.Light.ARInvoice.docType, Equal<ARDocType.creditMemo>>>, PX.Objects.CA.Light.ARInvoice.docDate, PX.Objects.CA.Light.ARRegister.dueDate>, Less<Required<PX.Objects.CA.Light.ARRegister.dueDate>>, And<PX.Objects.CA.Light.ARInvoice.docType, NotEqual<ARDocType.cashSale>, And<PX.Objects.CA.Light.ARInvoice.docType, NotEqual<ARDocType.cashReturn>, And<PX.Objects.CA.Light.ARInvoice.docType, NotEqual<ARDocType.prepaymentInvoice>, And2<Not<Where<PX.Objects.CA.Light.ARInvoice.docType, Equal<ARDocType.creditMemo>, And<PX.Objects.CA.Light.ARInvoice.origDocType, Equal<ARDocType.prepaymentInvoice>>>>, And<PX.Objects.CA.Light.ARInvoice.voided, Equal<False>, And<PX.Objects.CA.Light.ARRegister.scheduled, Equal<False>, And<PX.Objects.CA.Light.ARInvoice.openDoc, Equal<True>>>>>>>>>>((PXGraph) this);
    if (!filter.IncludeUnreleased.GetValueOrDefault())
      pxSelectBase.WhereAnd<Where<PX.Objects.CA.Light.ARInvoice.released, Equal<True>>>();
    if (filter.OrgBAccountID.HasValue)
      pxSelectBase.WhereAnd<Where<PX.Objects.AR.ARInvoice.branchID, InsideBranchesOf<Current<CashFlowEnq.CashFlowFilter.orgBAccountID>>>>();
    if (filter.CashAccountID.HasValue)
    {
      if (filter.StrictAccountCondition)
      {
        if (filter.IncludeUnassignedDocs.GetValueOrDefault())
          pxSelectBase.WhereAnd<Where<Where<PX.Objects.CA.Light.ARInvoice.cashAccountID, Equal<Required<PX.Objects.CA.Light.ARInvoice.cashAccountID>>, Or<PX.Objects.CA.Light.ARInvoice.cashAccountID, IsNull>>>>();
        else
          pxSelectBase.WhereAnd<Where<PX.Objects.CA.Light.ARInvoice.cashAccountID, Equal<Required<PX.Objects.CA.Light.ARInvoice.cashAccountID>>>>();
      }
      else
        pxSelectBase.WhereAnd<Where<Where<PX.Objects.CA.Light.ARInvoice.cashAccountID, Equal<Required<PX.Objects.CA.Light.ARInvoice.cashAccountID>>, Or<PX.Objects.CA.Light.ARInvoice.cashAccountID, IsNull>>>>();
    }
    foreach (PXResult<PX.Objects.CA.Light.ARInvoice, PX.Objects.CM.CurrencyInfo, CashAccount> pxResult in pxSelectBase.Select(new object[2]
    {
      (object) this.endDate,
      (object) filter.CashAccountID
    }))
    {
      PX.Objects.CA.Light.ARInvoice arInvoice = PXResult<PX.Objects.CA.Light.ARInvoice, PX.Objects.CM.CurrencyInfo, CashAccount>.op_Implicit(pxResult);
      PX.Objects.CM.CurrencyInfo aDocCuryInfo = PXResult<PX.Objects.CA.Light.ARInvoice, PX.Objects.CM.CurrencyInfo, CashAccount>.op_Implicit(pxResult);
      CashFlowForecast dst = (CashFlowForecast) null;
      CashAccount cashAccount = PXResult<PX.Objects.CA.Light.ARInvoice, PX.Objects.CM.CurrencyInfo, CashAccount>.op_Implicit(pxResult);
      int? nullable1 = cashAccount.AccountID;
      if (!nullable1.HasValue)
        cashAccount = this.findDefaultCashAccount(arInvoice);
      if (cashAccount != null)
      {
        nullable1 = cashAccount.AccountID;
        if (nullable1.HasValue)
          goto label_20;
      }
      bool? includeUnassignedDocs = filter.IncludeUnassignedDocs;
      bool flag = false;
      if (!(includeUnassignedDocs.GetValueOrDefault() == flag & includeUnassignedDocs.HasValue))
      {
        nullable1 = filter.DefaultAccountID;
        if (!nullable1.HasValue)
        {
          nullable1 = filter.CashAccountID;
          if (!nullable1.HasValue)
            continue;
        }
        cashAccount = this.defaultCashAccount;
      }
      else
        continue;
label_20:
      nullable1 = filter.CashAccountID;
      int? cashAccountId;
      if (nullable1.HasValue)
      {
        nullable1 = filter.CashAccountID;
        cashAccountId = cashAccount.CashAccountID;
        if (!(nullable1.GetValueOrDefault() == cashAccountId.GetValueOrDefault() & nullable1.HasValue == cashAccountId.HasValue))
          continue;
      }
      cashAccountId = cashAccount.CashAccountID;
      CashFlowEnq.FlowKey key = new CashFlowEnq.FlowKey(cashAccountId.Value, arInvoice);
      if (!flow.TryGetValue(key, out dst))
      {
        dst = this.Create(this.startDate, cashAccount, arInvoice);
        flow.Add(key, dst);
      }
      Decimal sign = arInvoice.DrCr == "C" ? 1M : -1M;
      Decimal? curyDocBal = arInvoice.CuryDocBal;
      Decimal? docBal = arInvoice.DocBal;
      DateTime? nullable2 = arInvoice.DueDate;
      DateTime dateTime;
      if (nullable2.HasValue || !(arInvoice.DocType == "CRM"))
      {
        nullable2 = arInvoice.DueDate;
        dateTime = nullable2.Value;
      }
      else
      {
        nullable2 = arInvoice.DocDate;
        dateTime = nullable2.Value;
      }
      DateTime docDate = dateTime;
      dst.CuryID = this.ConvertDocAmount(ref curyDocBal, ref docBal, aDocCuryInfo, cashAccount, filter);
      this.AddAmount(dst, docDate, curyDocBal, docBal, sign);
    }
  }

  protected virtual void RetrieveAPInvoices(
    Dictionary<CashFlowEnq.FlowKey, CashFlowForecast> flow,
    CashFlowEnq.CashFlowFilter filter)
  {
    PXSelectBase<PX.Objects.CA.Light.APInvoice> pxSelectBase = (PXSelectBase<PX.Objects.CA.Light.APInvoice>) new PXSelectReadonly2<PX.Objects.CA.Light.APInvoice, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.CA.Light.APInvoice.curyInfoID>>, LeftJoin<CashAccount, On<CashAccount.cashAccountID, Equal<PX.Objects.CA.Light.APInvoice.payAccountID>>>>, Where<PX.Objects.CA.Light.APInvoice.docType, NotEqual<APDocType.quickCheck>, And<PX.Objects.CA.Light.APInvoice.docType, NotEqual<APDocType.voidQuickCheck>, And<PX.Objects.CA.Light.APInvoice.voided, Equal<False>, And<PX.Objects.CA.Light.APRegister.scheduled, Equal<False>, And<PX.Objects.CA.Light.APInvoice.openDoc, Equal<True>, And<Where<PX.Objects.CA.Light.APInvoice.dueDate, Less<Required<PX.Objects.CA.Light.APInvoice.dueDate>>, Or<PX.Objects.CA.Light.APInvoice.payDate, Less<Required<PX.Objects.CA.Light.APInvoice.dueDate>>>>>>>>>>>((PXGraph) this);
    if (!filter.IncludeUnreleased.GetValueOrDefault())
      pxSelectBase.WhereAnd<Where<PX.Objects.CA.Light.APInvoice.released, Equal<True>>>();
    if (filter.OrgBAccountID.HasValue)
      pxSelectBase.WhereAnd<Where<PX.Objects.AP.APInvoice.branchID, InsideBranchesOf<Current<CashFlowEnq.CashFlowFilter.orgBAccountID>>>>();
    if (filter.CashAccountID.HasValue)
    {
      if (filter.StrictAccountCondition)
      {
        if (filter.IncludeUnassignedDocs.GetValueOrDefault())
          pxSelectBase.WhereAnd<Where<Where<PX.Objects.CA.Light.APInvoice.payAccountID, Equal<Required<PX.Objects.CA.Light.APInvoice.payAccountID>>, Or<PX.Objects.CA.Light.APInvoice.payAccountID, IsNull>>>>();
        else
          pxSelectBase.WhereAnd<Where<PX.Objects.CA.Light.APInvoice.payAccountID, Equal<Required<PX.Objects.CA.Light.APInvoice.payAccountID>>>>();
      }
      else
        pxSelectBase.WhereAnd<Where<Where<PX.Objects.CA.Light.APInvoice.payAccountID, Equal<Required<PX.Objects.CA.Light.APInvoice.payAccountID>>, Or<PX.Objects.CA.Light.APInvoice.payAccountID, IsNull>>>>();
    }
    foreach (PXResult<PX.Objects.CA.Light.APInvoice, PX.Objects.CM.CurrencyInfo, CashAccount> pxResult in pxSelectBase.Select(new object[3]
    {
      (object) this.endDate,
      (object) this.endDate,
      (object) filter.CashAccountID
    }))
    {
      PX.Objects.CA.Light.APInvoice apInvoice = PXResult<PX.Objects.CA.Light.APInvoice, PX.Objects.CM.CurrencyInfo, CashAccount>.op_Implicit(pxResult);
      CashFlowForecast cashFlowForecast = (CashFlowForecast) null;
      PX.Objects.CM.CurrencyInfo aDocCuryInfo = PXResult<PX.Objects.CA.Light.APInvoice, PX.Objects.CM.CurrencyInfo, CashAccount>.op_Implicit(pxResult);
      CashAccount cashAccount = PXResult<PX.Objects.CA.Light.APInvoice, PX.Objects.CM.CurrencyInfo, CashAccount>.op_Implicit(pxResult);
      int? nullable1 = cashAccount.AccountID;
      if (!nullable1.HasValue)
        cashAccount = this.findDefaultCashAccount(apInvoice);
      if (cashAccount != null)
      {
        nullable1 = cashAccount.AccountID;
        if (nullable1.HasValue)
          goto label_20;
      }
      bool? includeUnassignedDocs = filter.IncludeUnassignedDocs;
      bool flag = false;
      if (!(includeUnassignedDocs.GetValueOrDefault() == flag & includeUnassignedDocs.HasValue))
      {
        nullable1 = filter.DefaultAccountID;
        if (!nullable1.HasValue)
        {
          nullable1 = filter.CashAccountID;
          if (!nullable1.HasValue)
            continue;
        }
        cashAccount = this.defaultCashAccount;
      }
      else
        continue;
label_20:
      nullable1 = filter.CashAccountID;
      int? cashAccountId;
      if (nullable1.HasValue)
      {
        nullable1 = filter.CashAccountID;
        cashAccountId = cashAccount.CashAccountID;
        if (!(nullable1.GetValueOrDefault() == cashAccountId.GetValueOrDefault() & nullable1.HasValue == cashAccountId.HasValue))
          continue;
      }
      cashAccountId = cashAccount.CashAccountID;
      CashFlowEnq.FlowKey key = new CashFlowEnq.FlowKey(cashAccountId.Value, apInvoice);
      if (!flow.TryGetValue(key, out cashFlowForecast))
      {
        cashFlowForecast = this.Create(this.startDate, cashAccount, apInvoice);
        flow.Add(key, cashFlowForecast);
      }
      Decimal? curyDocBal = apInvoice.CuryDocBal;
      Decimal? docBal = apInvoice.DocBal;
      Decimal num = apInvoice.DrCr == "D" ? 1M : -1M;
      cashFlowForecast.CuryID = this.ConvertDocAmount(ref curyDocBal, ref docBal, aDocCuryInfo, cashAccount, filter);
      CashFlowForecast dst = cashFlowForecast;
      DateTime? nullable2 = apInvoice.PayDate;
      DateTime docDate;
      if (!nullable2.HasValue)
      {
        nullable2 = apInvoice.DueDate;
        docDate = nullable2.Value;
      }
      else
      {
        nullable2 = apInvoice.PayDate;
        docDate = nullable2.Value;
      }
      Decimal? curyValue = curyDocBal;
      Decimal? baseValue = docBal;
      Decimal sign = num;
      this.AddAmount(dst, docDate, curyValue, baseValue, sign);
    }
  }

  protected virtual void RetrieveAPPayments(
    Dictionary<CashFlowEnq.FlowKey, CashFlowForecast> flow,
    CashFlowEnq.CashFlowFilter filter)
  {
    PXSelectBase<PX.Objects.AP.APPayment> pxSelectBase = (PXSelectBase<PX.Objects.AP.APPayment>) new PXSelectReadonly2<PX.Objects.AP.APPayment, LeftJoin<PX.Objects.AP.APAdjust, On<PX.Objects.AP.APAdjust.adjgDocType, Equal<PX.Objects.AP.APPayment.docType>, And<PX.Objects.AP.APAdjust.adjgRefNbr, Equal<PX.Objects.AP.APPayment.refNbr>, And<PX.Objects.AP.APAdjust.released, Equal<True>>>>>, Where<PX.Objects.AP.APPayment.docDate, GreaterEqual<Required<PX.Objects.AP.APPayment.docDate>>, And<PX.Objects.AP.APPayment.docDate, Less<Required<PX.Objects.AP.APPayment.docDate>>, And<PX.Objects.AP.APPayment.voided, Equal<False>, And<PX.Objects.AP.APRegister.scheduled, Equal<False>, And<PX.Objects.AP.APPayment.docType, NotEqual<APDocType.voidCheck>, And<PX.Objects.AP.APPayment.docType, NotEqual<APDocType.voidQuickCheck>>>>>>>>((PXGraph) this);
    if (filter.CashAccountID.HasValue)
      pxSelectBase.WhereAnd<Where<PX.Objects.AP.APPayment.cashAccountID, Equal<Required<PX.Objects.AP.APPayment.cashAccountID>>>>();
    if (!filter.IncludeUnreleased.GetValueOrDefault())
      pxSelectBase.WhereAnd<Where<PX.Objects.AP.APPayment.released, Equal<True>>>();
    if (filter.OrgBAccountID.HasValue)
      pxSelectBase.WhereAnd<Where<PX.Objects.AP.APPayment.branchID, InsideBranchesOf<Current<CashFlowEnq.CashFlowFilter.orgBAccountID>>>>();
    PX.Objects.AP.APPayment apPayment1 = (PX.Objects.AP.APPayment) null;
    foreach (PXResult<PX.Objects.AP.APPayment, PX.Objects.AP.APAdjust> pxResult in pxSelectBase.Select(new object[3]
    {
      (object) this.startDate,
      (object) this.endDate,
      (object) filter.CashAccountID
    }))
    {
      PX.Objects.AP.APPayment apPayment2 = PXResult<PX.Objects.AP.APPayment, PX.Objects.AP.APAdjust>.op_Implicit(pxResult);
      PX.Objects.AP.APAdjust adj = PXResult<PX.Objects.AP.APPayment, PX.Objects.AP.APAdjust>.op_Implicit(pxResult);
      CashFlowForecast dst = (CashFlowForecast) null;
      if (apPayment2.CashAccountID.HasValue)
      {
        bool flag = (apPayment2.DocType == "QCK" ? 1 : (apPayment2.DocType == "VQC" ? 1 : 0)) != 0 || !string.IsNullOrEmpty(adj.AdjdDocType) && !string.IsNullOrEmpty(adj.AdjdRefNbr);
        if (filter.IncludeUnapplied.GetValueOrDefault() || flag)
        {
          CashFlowEnq.FlowKey key1 = new CashFlowEnq.FlowKey(apPayment2, flag);
          if (!flow.TryGetValue(key1, out dst))
          {
            dst = this.Create(this.startDate, apPayment2, flag);
            flow.Add(key1, dst);
          }
          this.AddAmount(dst, apPayment2, adj);
          if (filter.IncludeUnapplied.GetValueOrDefault())
          {
            Decimal? curyDocBal = apPayment2.CuryDocBal;
            Decimal num = 0M;
            if (!(curyDocBal.GetValueOrDefault() == num & curyDocBal.HasValue) && (apPayment1 == null || apPayment1.DocType != apPayment2.DocType || apPayment1.RefNbr != apPayment2.RefNbr))
            {
              CashFlowEnq.FlowKey key2 = new CashFlowEnq.FlowKey(apPayment2, false);
              if (!flow.TryGetValue(key2, out dst))
              {
                dst = this.Create(this.startDate, apPayment2, false);
                flow.Add(key2, dst);
              }
              this.AddAmount(dst, apPayment2);
            }
          }
          apPayment1 = apPayment2;
        }
      }
    }
  }

  protected virtual void RetrieveARPayments(
    Dictionary<CashFlowEnq.FlowKey, CashFlowForecast> flow,
    CashFlowEnq.CashFlowFilter filter)
  {
    PXSelectBase<PX.Objects.AR.ARPayment> pxSelectBase = (PXSelectBase<PX.Objects.AR.ARPayment>) new PXSelectReadonly2<PX.Objects.AR.ARPayment, LeftJoin<PX.Objects.AR.ARAdjust, On<PX.Objects.AR.ARAdjust.adjgDocType, Equal<PX.Objects.AR.ARPayment.docType>, And<PX.Objects.AR.ARAdjust.adjgRefNbr, Equal<PX.Objects.AR.ARPayment.refNbr>, And<PX.Objects.AR.ARAdjust.released, Equal<True>>>>>, Where<PX.Objects.AR.ARPayment.docDate, GreaterEqual<Required<PX.Objects.AR.ARPayment.docDate>>, And<PX.Objects.AR.ARPayment.voided, Equal<False>, And<PX.Objects.AR.ARRegister.scheduled, Equal<False>, And<PX.Objects.AR.ARPayment.docType, NotEqual<ARDocType.voidPayment>, And<PX.Objects.AR.ARPayment.docType, NotEqual<ARDocType.prepaymentInvoice>, And2<Not<Where<PX.Objects.AR.ARPayment.docType, Equal<ARDocType.creditMemo>, And<PX.Objects.AR.ARRegister.origDocType, Equal<ARDocType.prepaymentInvoice>>>>, And<PX.Objects.AR.ARPayment.docDate, Less<Required<PX.Objects.AR.ARPayment.docDate>>>>>>>>>>((PXGraph) this);
    if (!filter.IncludeUnreleased.GetValueOrDefault())
      pxSelectBase.WhereAnd<Where<PX.Objects.AR.ARPayment.released, Equal<True>>>();
    if (filter.CashAccountID.HasValue)
      pxSelectBase.WhereAnd<Where<PX.Objects.AR.ARPayment.cashAccountID, Equal<Required<PX.Objects.AR.ARPayment.cashAccountID>>>>();
    if (filter.OrgBAccountID.HasValue)
      pxSelectBase.WhereAnd<Where<PX.Objects.AR.ARPayment.branchID, InsideBranchesOf<Current<CashFlowEnq.CashFlowFilter.orgBAccountID>>>>();
    PX.Objects.AR.ARPayment arPayment1 = (PX.Objects.AR.ARPayment) null;
    foreach (PXResult<PX.Objects.AR.ARPayment, PX.Objects.AR.ARAdjust> pxResult in pxSelectBase.Select(new object[3]
    {
      (object) this.startDate,
      (object) this.endDate,
      (object) filter.CashAccountID
    }))
    {
      PX.Objects.AR.ARPayment arPayment2 = PXResult<PX.Objects.AR.ARPayment, PX.Objects.AR.ARAdjust>.op_Implicit(pxResult);
      PX.Objects.AR.ARAdjust adj = PXResult<PX.Objects.AR.ARPayment, PX.Objects.AR.ARAdjust>.op_Implicit(pxResult);
      CashFlowForecast dst = (CashFlowForecast) null;
      if (arPayment2.CashAccountID.HasValue)
      {
        bool flag = (arPayment2.DocType == "CSL" ? 1 : (arPayment2.DocType == "RCS" ? 1 : 0)) != 0 || !string.IsNullOrEmpty(adj.AdjdDocType) && !string.IsNullOrEmpty(adj.AdjdRefNbr);
        if (filter.IncludeUnapplied.GetValueOrDefault() || flag)
        {
          CashFlowEnq.FlowKey key1 = new CashFlowEnq.FlowKey(arPayment2, flag);
          if (!flow.TryGetValue(key1, out dst))
          {
            dst = this.Create(this.startDate, arPayment2, flag);
            flow.Add(key1, dst);
          }
          this.AddAmount(dst, arPayment2, adj);
          if (filter.IncludeUnapplied.GetValueOrDefault())
          {
            Decimal? curyDocBal = arPayment2.CuryDocBal;
            Decimal num = 0M;
            if (!(curyDocBal.GetValueOrDefault() == num & curyDocBal.HasValue) && (arPayment1 == null || arPayment1.DocType != arPayment2.DocType || arPayment1.RefNbr != arPayment2.RefNbr))
            {
              CashFlowEnq.FlowKey key2 = new CashFlowEnq.FlowKey(arPayment2, false);
              if (!flow.TryGetValue(key2, out dst))
              {
                dst = this.Create(this.startDate, arPayment2, false);
                flow.Add(key2, dst);
              }
              this.AddAmount(dst, arPayment2);
            }
          }
          arPayment1 = arPayment2;
        }
      }
    }
  }

  protected virtual void RetriveCashForecasts(
    Dictionary<CashFlowEnq.FlowKey, CashFlowForecast> flow,
    CashFlowEnq.CashFlowFilter filter)
  {
    PXSelectBase<CashForecastTran> pxSelectBase = (PXSelectBase<CashForecastTran>) new PXSelectJoin<CashForecastTran, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<CashForecastTran.cashAccountID>>>, Where<CashForecastTran.tranDate, GreaterEqual<Required<PX.Objects.AP.APPayment.docDate>>, And<CashForecastTran.tranDate, Less<Required<CashForecastTran.tranDate>>>>>((PXGraph) this);
    if (filter.OrgBAccountID.HasValue)
      pxSelectBase.WhereAnd<Where<CashAccount.branchID, InsideBranchesOf<Current<CashFlowEnq.CashFlowFilter.orgBAccountID>>>>();
    if (filter.CashAccountID.HasValue)
      pxSelectBase.WhereAnd<Where<CashForecastTran.cashAccountID, Equal<Required<CashForecastTran.cashAccountID>>>>();
    foreach (PXResult<CashForecastTran, CashAccount> pxResult in pxSelectBase.Select(new object[3]
    {
      (object) this.startDate,
      (object) this.endDate,
      (object) filter.CashAccountID
    }))
    {
      CashForecastTran cashForecastTran = PXResult<CashForecastTran, CashAccount>.op_Implicit(pxResult);
      CashAccount cashAccount = PXResult<CashForecastTran, CashAccount>.op_Implicit(pxResult);
      CashFlowEnq.FlowKey key1 = new CashFlowEnq.FlowKey(cashForecastTran);
      CashFlowForecast dst;
      if (!flow.TryGetValue(key1, out dst))
      {
        dst = this.Create(this.startDate, cashForecastTran);
        flow.Add(key1, dst);
      }
      Dictionary<int, CurrencyRate> accountRates1 = this.accountRates;
      int? cashAccountId = cashForecastTran.CashAccountID;
      int key2 = cashAccountId.Value;
      CurrencyRate curyRate;
      ref CurrencyRate local = ref curyRate;
      if (!accountRates1.TryGetValue(key2, out local))
      {
        curyRate = this.findCuryRate(cashAccount.CuryID, this.baseCurrency.CuryID, cashAccount.CuryRateTypeID, this.startDate);
        Dictionary<int, CurrencyRate> accountRates2 = this.accountRates;
        cashAccountId = cashAccount.CashAccountID;
        int key3 = cashAccountId.Value;
        CurrencyRate currencyRate = curyRate;
        accountRates2.Add(key3, currencyRate);
      }
      this.AddAmount(dst, cashForecastTran, curyRate, this.baseCurrency);
    }
  }

  protected virtual void RetrieveAPScheduled(
    Dictionary<CashFlowEnq.FlowKey, CashFlowForecast> flow,
    CashFlowEnq.CashFlowFilter filter)
  {
    foreach (PXResult<PX.Objects.CA.Light.APInvoice, PX.Objects.CM.CurrencyInfo, CashAccount, PX.Objects.GL.Schedule, PX.Objects.CS.Terms> pxResult in ((PXSelectBase<PX.Objects.CA.Light.APInvoice>) new PXSelectReadonly2<PX.Objects.CA.Light.APInvoice, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.CA.Light.APInvoice.curyInfoID>>, LeftJoin<CashAccount, On<CashAccount.cashAccountID, Equal<PX.Objects.CA.Light.APInvoice.payAccountID>>, InnerJoin<PX.Objects.GL.Schedule, On<PX.Objects.GL.Schedule.scheduleID, Equal<PX.Objects.CA.Light.APRegister.scheduleID>, And<PX.Objects.GL.Schedule.active, Equal<True>>>, InnerJoin<PX.Objects.CS.Terms, On<PX.Objects.CS.Terms.termsID, Equal<PX.Objects.CA.Light.APInvoice.termsID>>>>>>, Where<PX.Objects.CA.Light.APInvoice.voided, Equal<False>, And<PX.Objects.CA.Light.APRegister.scheduled, Equal<True>>>>((PXGraph) this)).Select(Array.Empty<object>()))
    {
      PX.Objects.CA.Light.APInvoice apInvoice = PXResult<PX.Objects.CA.Light.APInvoice, PX.Objects.CM.CurrencyInfo, CashAccount, PX.Objects.GL.Schedule, PX.Objects.CS.Terms>.op_Implicit(pxResult);
      CashFlowForecast dst = (CashFlowForecast) null;
      PX.Objects.CM.CurrencyInfo aDocCuryInfo = PXResult<PX.Objects.CA.Light.APInvoice, PX.Objects.CM.CurrencyInfo, CashAccount, PX.Objects.GL.Schedule, PX.Objects.CS.Terms>.op_Implicit(pxResult);
      CashAccount cashAccount = PXResult<PX.Objects.CA.Light.APInvoice, PX.Objects.CM.CurrencyInfo, CashAccount, PX.Objects.GL.Schedule, PX.Objects.CS.Terms>.op_Implicit(pxResult);
      PX.Objects.GL.Schedule schedule = PXResult<PX.Objects.CA.Light.APInvoice, PX.Objects.CM.CurrencyInfo, CashAccount, PX.Objects.GL.Schedule, PX.Objects.CS.Terms>.op_Implicit(pxResult);
      PX.Objects.CS.Terms terms = PXResult<PX.Objects.CA.Light.APInvoice, PX.Objects.CM.CurrencyInfo, CashAccount, PX.Objects.GL.Schedule, PX.Objects.CS.Terms>.op_Implicit(pxResult);
      int? nullable = cashAccount.AccountID;
      if (!nullable.HasValue)
        cashAccount = this.findDefaultCashAccount(apInvoice);
      if (cashAccount != null)
      {
        nullable = cashAccount.AccountID;
        if (nullable.HasValue)
          goto label_10;
      }
      bool? includeUnassignedDocs = filter.IncludeUnassignedDocs;
      bool flag = false;
      if (!(includeUnassignedDocs.GetValueOrDefault() == flag & includeUnassignedDocs.HasValue))
      {
        nullable = filter.DefaultAccountID;
        if (!nullable.HasValue)
        {
          nullable = filter.CashAccountID;
          if (!nullable.HasValue)
            continue;
        }
        cashAccount = this.defaultCashAccount;
      }
      else
        continue;
label_10:
      nullable = filter.CashAccountID;
      if (nullable.HasValue)
      {
        nullable = filter.CashAccountID;
        int? cashAccountId = cashAccount.CashAccountID;
        if (!(nullable.GetValueOrDefault() == cashAccountId.GetValueOrDefault() & nullable.HasValue == cashAccountId.HasValue))
          continue;
      }
      List<ScheduleDet> scheduleDetList = new List<ScheduleDet>();
      if (!string.IsNullOrEmpty(schedule.ScheduleID) && schedule.NextRunDate.HasValue && schedule.NextRunDate.Value <= this.endDate)
      {
        DateTime? lastDate = (DateTime?) PXResultset<PX.Objects.CA.Light.APInvoice>.op_Implicit(PXSelectBase<PX.Objects.CA.Light.APInvoice, PXSelect<PX.Objects.CA.Light.APInvoice, Where<PX.Objects.CA.Light.APRegister.scheduled, Equal<False>, And<PX.Objects.CA.Light.APRegister.scheduleID, Equal<Required<PX.Objects.CA.Light.APRegister.scheduleID>>>>, OrderBy<Desc<PX.Objects.CA.Light.APInvoice.docDate>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) schedule.ScheduleID
        }))?.DocDate;
        PX.Objects.GL.Schedule copy = (PX.Objects.GL.Schedule) ((PXSelectBase) this.schedule).Cache.CreateCopy((object) schedule);
        try
        {
          scheduleDetList = new Scheduler((PXGraph) this).MakeSchedule(copy, short.MaxValue, new DateTime?(this.endDate)).ToList<ScheduleDet>().FindAll((Predicate<ScheduleDet>) (op =>
          {
            if (lastDate.HasValue && op.ScheduledDate.Value < lastDate.Value)
              return false;
            DateTime? dueDate;
            TermsAttribute.CalcTermsDates(terms, op.ScheduledDate, out dueDate, out DateTime? _);
            return dueDate.HasValue && dueDate.Value < this.endDate;
          }));
        }
        catch (PXFinPeriodException ex)
        {
          object[] objArray = new object[4]
          {
            (object) "AP",
            (object) apInvoice.DocType,
            (object) apInvoice.RefNbr,
            (object) schedule.ScheduleID
          };
          throw new PXException((Exception) ex, "A scheduled document {0} {1} {2} assigned to the Schedule {3} needs a financial period, but it's not defined in the system", objArray);
        }
      }
      if (scheduleDetList.Count > 0)
      {
        CashFlowEnq.FlowKey key = new CashFlowEnq.FlowKey(cashAccount.CashAccountID.Value, apInvoice);
        if (!flow.TryGetValue(key, out dst))
        {
          dst = this.Create(this.startDate, cashAccount, apInvoice);
          flow.Add(key, dst);
        }
        Decimal? curyDocBal = apInvoice.CuryDocBal;
        Decimal? docBal = apInvoice.DocBal;
        Decimal sign = apInvoice.DrCr == "D" ? 1M : -1M;
        dst.CuryID = this.ConvertDocAmount(ref curyDocBal, ref docBal, aDocCuryInfo, cashAccount, filter);
        foreach (ScheduleDet scheduleDet in scheduleDetList)
        {
          DateTime? dueDate;
          TermsAttribute.CalcTermsDates(terms, scheduleDet.ScheduledDate, out dueDate, out DateTime? _);
          this.AddAmount(dst, dueDate.Value, curyDocBal, docBal, sign);
        }
      }
    }
  }

  protected virtual void RetrieveARScheduled(
    Dictionary<CashFlowEnq.FlowKey, CashFlowForecast> flow,
    CashFlowEnq.CashFlowFilter filter)
  {
    foreach (PXResult<PX.Objects.CA.Light.ARInvoice, PX.Objects.CM.CurrencyInfo, CashAccount, PX.Objects.GL.Schedule, PX.Objects.CS.Terms> pxResult in ((PXSelectBase<PX.Objects.CA.Light.ARInvoice>) new PXSelectReadonly2<PX.Objects.CA.Light.ARInvoice, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.CA.Light.ARInvoice.curyInfoID>>, LeftJoin<CashAccount, On<CashAccount.cashAccountID, Equal<PX.Objects.CA.Light.ARInvoice.cashAccountID>>, InnerJoin<PX.Objects.GL.Schedule, On<PX.Objects.GL.Schedule.scheduleID, Equal<PX.Objects.CA.Light.ARRegister.scheduleID>, And<PX.Objects.GL.Schedule.active, Equal<True>>>, InnerJoin<PX.Objects.CS.Terms, On<PX.Objects.CS.Terms.termsID, Equal<PX.Objects.CA.Light.ARInvoice.termsID>>>>>>, Where<PX.Objects.CA.Light.ARInvoice.voided, Equal<False>, And<PX.Objects.CA.Light.ARRegister.scheduled, Equal<True>>>>((PXGraph) this)).Select(Array.Empty<object>()))
    {
      PX.Objects.CA.Light.ARInvoice arInvoice = PXResult<PX.Objects.CA.Light.ARInvoice, PX.Objects.CM.CurrencyInfo, CashAccount, PX.Objects.GL.Schedule, PX.Objects.CS.Terms>.op_Implicit(pxResult);
      CashFlowForecast dst = (CashFlowForecast) null;
      PX.Objects.CM.CurrencyInfo aDocCuryInfo = PXResult<PX.Objects.CA.Light.ARInvoice, PX.Objects.CM.CurrencyInfo, CashAccount, PX.Objects.GL.Schedule, PX.Objects.CS.Terms>.op_Implicit(pxResult);
      CashAccount cashAccount = PXResult<PX.Objects.CA.Light.ARInvoice, PX.Objects.CM.CurrencyInfo, CashAccount, PX.Objects.GL.Schedule, PX.Objects.CS.Terms>.op_Implicit(pxResult);
      PX.Objects.GL.Schedule schedule = PXResult<PX.Objects.CA.Light.ARInvoice, PX.Objects.CM.CurrencyInfo, CashAccount, PX.Objects.GL.Schedule, PX.Objects.CS.Terms>.op_Implicit(pxResult);
      PX.Objects.CS.Terms terms = PXResult<PX.Objects.CA.Light.ARInvoice, PX.Objects.CM.CurrencyInfo, CashAccount, PX.Objects.GL.Schedule, PX.Objects.CS.Terms>.op_Implicit(pxResult);
      int? nullable = cashAccount.AccountID;
      if (!nullable.HasValue)
        cashAccount = this.findDefaultCashAccount(arInvoice);
      if (cashAccount != null)
      {
        nullable = cashAccount.AccountID;
        if (nullable.HasValue)
          goto label_10;
      }
      bool? includeUnassignedDocs = filter.IncludeUnassignedDocs;
      bool flag = false;
      if (!(includeUnassignedDocs.GetValueOrDefault() == flag & includeUnassignedDocs.HasValue))
      {
        nullable = filter.DefaultAccountID;
        if (!nullable.HasValue)
        {
          nullable = filter.CashAccountID;
          if (!nullable.HasValue)
            continue;
        }
        cashAccount = this.defaultCashAccount;
      }
      else
        continue;
label_10:
      nullable = filter.CashAccountID;
      if (nullable.HasValue)
      {
        nullable = filter.CashAccountID;
        int? cashAccountId = cashAccount.CashAccountID;
        if (!(nullable.GetValueOrDefault() == cashAccountId.GetValueOrDefault() & nullable.HasValue == cashAccountId.HasValue))
          continue;
      }
      List<ScheduleDet> scheduleDetList = new List<ScheduleDet>();
      if (!string.IsNullOrEmpty(schedule.ScheduleID) && schedule.NextRunDate.HasValue && schedule.NextRunDate.Value <= this.endDate)
      {
        DateTime? lastDate = (DateTime?) PXResultset<PX.Objects.CA.Light.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.CA.Light.ARInvoice, PXSelect<PX.Objects.CA.Light.ARInvoice, Where<PX.Objects.CA.Light.ARRegister.scheduled, Equal<False>, And<PX.Objects.CA.Light.ARRegister.scheduleID, Equal<Required<PX.Objects.CA.Light.ARRegister.scheduleID>>>>, OrderBy<Desc<PX.Objects.CA.Light.ARInvoice.docDate>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) schedule.ScheduleID
        }))?.DocDate;
        PX.Objects.GL.Schedule copy = (PX.Objects.GL.Schedule) ((PXSelectBase) this.schedule).Cache.CreateCopy((object) schedule);
        try
        {
          scheduleDetList = new Scheduler((PXGraph) this).MakeSchedule(copy, short.MaxValue, new DateTime?(this.endDate)).ToList<ScheduleDet>().FindAll((Predicate<ScheduleDet>) (op =>
          {
            if (lastDate.HasValue && op.ScheduledDate.Value < lastDate.Value)
              return false;
            DateTime? dueDate;
            TermsAttribute.CalcTermsDates(terms, op.ScheduledDate, out dueDate, out DateTime? _);
            return dueDate.HasValue && dueDate.Value < this.endDate;
          }));
        }
        catch (PXFinPeriodException ex)
        {
          object[] objArray = new object[4]
          {
            (object) "AR",
            (object) arInvoice.DocType,
            (object) arInvoice.RefNbr,
            (object) schedule.ScheduleID
          };
          throw new PXException((Exception) ex, "A scheduled document {0} {1} {2} assigned to the Schedule {3} needs a financial period, but it's not defined in the system", objArray);
        }
      }
      if (scheduleDetList.Count > 0)
      {
        CashFlowEnq.FlowKey key = new CashFlowEnq.FlowKey(cashAccount.CashAccountID.Value, arInvoice);
        if (!flow.TryGetValue(key, out dst))
        {
          dst = this.Create(this.startDate, cashAccount, arInvoice);
          flow.Add(key, dst);
        }
        Decimal? curyDocBal = arInvoice.CuryDocBal;
        Decimal? docBal = arInvoice.DocBal;
        Decimal sign = arInvoice.DrCr == "C" ? 1M : -1M;
        dst.CuryID = this.ConvertDocAmount(ref curyDocBal, ref docBal, aDocCuryInfo, cashAccount, filter);
        foreach (ScheduleDet scheduleDet in scheduleDetList)
        {
          DateTime? dueDate;
          TermsAttribute.CalcTermsDates(terms, scheduleDet.ScheduledDate, out dueDate, out DateTime? _);
          this.AddAmount(dst, dueDate.Value, curyDocBal, docBal, sign);
        }
      }
    }
  }

  protected virtual void RecalcSummmary(
    Dictionary<int, CashFlowForecast> accountBalances,
    Dictionary<CashFlowEnq.FlowKey, CashFlowForecast> flow)
  {
    foreach (CashFlowForecast row in accountBalances.Values)
    {
      Decimal num1 = 0M;
      Decimal num2 = 0M;
      for (int offset = 1; offset < this.AmountFieldsNumber + 1; ++offset)
      {
        this.SetValue(row, offset, num1, true);
        this.SetValue(row, offset, num2, false);
      }
    }
    foreach (CashFlowForecast row1 in flow.Values)
    {
      int? nullable1 = row1.CashAccountID;
      if (nullable1.HasValue)
      {
        Dictionary<int, CashFlowForecast> dictionary1 = accountBalances;
        nullable1 = row1.CashAccountID;
        int key1 = nullable1.Value;
        CashFlowForecast row2;
        ref CashFlowForecast local = ref row2;
        if (!dictionary1.TryGetValue(key1, out local))
        {
          row2 = new CashFlowForecast();
          row2.RecordType = new int?(0);
          Dictionary<int, CashFlowForecast> dictionary2 = accountBalances;
          nullable1 = row1.CashAccountID;
          int key2 = nullable1.Value;
          CashFlowForecast cashFlowForecast = row2;
          dictionary2.Add(key2, cashFlowForecast);
        }
        for (int offset = 0; offset < this.AmountFieldsNumber; ++offset)
        {
          Decimal? nullable2 = this.GetValue(row1, offset, true);
          Decimal? nullable3 = this.GetValue(row1, offset, false);
          nullable1 = row1.RecordType;
          int num3 = 0;
          Decimal num4;
          if (!(nullable1.GetValueOrDefault() < num3 & nullable1.HasValue))
          {
            nullable1 = row1.RecordType;
            int num5 = 0;
            num4 = nullable1.GetValueOrDefault() > num5 & nullable1.HasValue ? 1M : 0M;
          }
          else
            num4 = -1M;
          Decimal num6 = num4;
          this.AddValue(row2, offset + 1, nullable2.GetValueOrDefault() * num6, true);
          this.AddValue(row2, offset + 1, nullable3.GetValueOrDefault() * num6, false);
        }
      }
    }
    foreach (CashFlowForecast row in accountBalances.Values)
    {
      for (int offset = 1; offset < this.AmountFieldsNumber + 1; ++offset)
      {
        Decimal valueOrDefault1 = this.GetValue(row, offset - 1, true).GetValueOrDefault();
        Decimal valueOrDefault2 = this.GetValue(row, offset - 1, false).GetValueOrDefault();
        this.AddValue(row, offset, valueOrDefault1, true);
        this.AddValue(row, offset, valueOrDefault2, false);
      }
    }
  }

  protected bool DetectPotentialScheduleBreak(DateTime endDate)
  {
    return PXResultset<MasterFinPeriod>.op_Implicit(PXSelectBase<MasterFinPeriod, PXSelect<MasterFinPeriod, Where<MasterFinPeriod.startDate, Greater<Required<MasterFinPeriod.startDate>>, And<MasterFinPeriod.startDate, NotEqual<MasterFinPeriod.endDate>>>, OrderBy<Asc<MasterFinPeriod.startDate>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) endDate
    })) == null;
  }

  private void AddValue(CashFlowForecast row, int offset, Decimal value, bool isCury)
  {
    string str = $"{(isCury ? "Cury" : string.Empty)}AmountDay{offset.ToString()}";
    int fieldOrdinal = ((PXSelectBase) this.CashFlow).Cache.GetFieldOrdinal(str);
    if (fieldOrdinal < 0)
      fieldOrdinal = ((PXSelectBase) this.CashFlow).Cache.GetFieldOrdinal(str);
    Decimal? nullable = (Decimal?) ((PXSelectBase) this.CashFlow).Cache.GetValue((object) row, fieldOrdinal);
    nullable = new Decimal?(nullable.GetValueOrDefault() + value);
    ((PXSelectBase) this.CashFlow).Cache.SetValue((object) row, fieldOrdinal, (object) nullable);
  }

  private void SetValue(CashFlowForecast row, int offset, Decimal value, bool isCury)
  {
    string str = $"{(isCury ? "Cury" : string.Empty)}AmountDay{offset.ToString()}";
    int fieldOrdinal = ((PXSelectBase) this.CashFlow).Cache.GetFieldOrdinal(str);
    if (fieldOrdinal < 0)
      fieldOrdinal = ((PXSelectBase) this.CashFlow).Cache.GetFieldOrdinal(str);
    ((PXSelectBase) this.CashFlow).Cache.SetValue((object) row, fieldOrdinal, (object) value);
  }

  private Decimal? GetValue(CashFlowForecast row, int offset, bool isCury)
  {
    string str = $"{(isCury ? "Cury" : string.Empty)}AmountDay{offset.ToString()}";
    int fieldOrdinal = ((PXSelectBase) this.CashFlow).Cache.GetFieldOrdinal(str);
    if (fieldOrdinal < 0)
      fieldOrdinal = ((PXSelectBase) this.CashFlow).Cache.GetFieldOrdinal(str);
    return (Decimal?) ((PXSelectBase) this.CashFlow).Cache.GetValue((object) row, fieldOrdinal);
  }

  public void CalcCashAccountBalance(
    DateTime startDate,
    CashFlowEnq.CashFlowFilter filter,
    Dictionary<int, CashFlowForecast> result)
  {
    int? cashAccountId = filter.CashAccountID;
    CMSetup cmSetup = PXResultset<CMSetup>.op_Implicit(PXSelectBase<CMSetup, PXSelect<CMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    PXSelectBase<CashAccount> pxSelectBase = (PXSelectBase<CashAccount>) new PXSelectJoinGroupBy<CashAccount, LeftJoin<CADailySummary, On<CADailySummary.cashAccountID, Equal<CashAccount.cashAccountID>, And<CADailySummary.tranDate, Less<Required<CADailySummary.tranDate>>>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<CashAccount.branchID>>, InnerJoin<PX.Objects.CM.Currency, On<PX.Objects.CM.Currency.curyID, Equal<PX.Objects.GL.Branch.baseCuryID>>>>>, Where<Match<CashAccount, Current<AccessInfo.userName>>>, Aggregate<GroupBy<CashAccount.cashAccountID, Sum<CADailySummary.amtReleasedClearedCr, Sum<CADailySummary.amtReleasedUnclearedCr, Sum<CADailySummary.amtUnreleasedUnclearedCr, Sum<CADailySummary.amtUnreleasedClearedCr, Sum<CADailySummary.amtReleasedClearedDr, Sum<CADailySummary.amtReleasedUnclearedDr, Sum<CADailySummary.amtUnreleasedUnclearedDr, Sum<CADailySummary.amtUnreleasedClearedDr>>>>>>>>>>>((PXGraph) this);
    if (filter.OrgBAccountID.HasValue)
      pxSelectBase.WhereAnd<Where<CashAccount.branchID, InsideBranchesOf<Current<CashFlowEnq.CashFlowFilter.orgBAccountID>>>>();
    if (cashAccountId.HasValue)
      pxSelectBase.WhereAnd<Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>();
    foreach (PXResult<CashAccount, CADailySummary, PX.Objects.GL.Branch, PX.Objects.CM.Currency> pxResult in pxSelectBase.Select(new object[2]
    {
      (object) startDate,
      (object) cashAccountId
    }))
    {
      CashAccount account = PXResult<CashAccount, CADailySummary, PX.Objects.GL.Branch, PX.Objects.CM.Currency>.op_Implicit(pxResult);
      CADailySummary src = PXResult<CashAccount, CADailySummary, PX.Objects.GL.Branch, PX.Objects.CM.Currency>.op_Implicit(pxResult);
      PX.Objects.CM.Currency currency = PXResult<CashAccount, CADailySummary, PX.Objects.GL.Branch, PX.Objects.CM.Currency>.op_Implicit(pxResult);
      CashFlowForecast cashFlowForecast1;
      if (!result.TryGetValue(account.CashAccountID.Value, out cashFlowForecast1))
      {
        cashFlowForecast1 = this.Create(startDate, src, account);
        cashFlowForecast1.CuryID = account.CuryID;
        cashFlowForecast1.AcctCuryID = account.CuryID;
        if (!cashFlowForecast1.CashAccountID.HasValue)
        {
          cashFlowForecast1.CashAccountID = account.CashAccountID;
          cashFlowForecast1.CuryID = account.CuryID;
        }
        result.Add(account.CashAccountID.Value, cashFlowForecast1);
      }
      int num1 = account.CuryID != currency.CuryID ? 1 : 0;
      CashFlowForecast cashFlowForecast2 = cashFlowForecast1;
      Decimal? nullable1 = src.AmtReleasedClearedDr;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = src.AmtReleasedUnclearedDr;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      Decimal num2 = valueOrDefault1 + valueOrDefault2;
      nullable1 = src.AmtReleasedClearedCr;
      Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
      nullable1 = src.AmtReleasedUnclearedCr;
      Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
      Decimal num3 = valueOrDefault3 + valueOrDefault4;
      Decimal? nullable2 = new Decimal?(num2 - num3);
      cashFlowForecast2.CuryAmountDay0 = nullable2;
      cashFlowForecast1.AmountDay0 = cashFlowForecast1.CuryAmountDay0;
      if (num1 != 0)
      {
        CurrencyRate currencyRate = (CurrencyRate) null;
        if (string.IsNullOrEmpty(account.CuryRateTypeID) && (cmSetup == null || string.IsNullOrEmpty(cmSetup.CARateTypeDflt)))
          throw new PXException("A currency rate type is not defined to the Cash ccount {0} and no default is provided for CA Module in CM Setup", new object[1]
          {
            (object) account.CashAccountCD
          });
        if (!this.accountRates.TryGetValue(account.CashAccountID.Value, out currencyRate))
        {
          string aCuryRateType = string.IsNullOrEmpty(account.CuryRateTypeID) ? cmSetup.CARateTypeDflt : account.CuryRateTypeID;
          currencyRate = this.findCuryRate(account.CuryID, currency.CuryID, aCuryRateType, startDate);
          this.accountRates.Add(account.CashAccountID.Value, currencyRate);
        }
        if (currencyRate == null)
          throw new PXException("A currency rate for conversion from Currency {0} to Base Currency {1} is not found for account {2}", new object[3]
          {
            (object) account.CuryID,
            (object) currency.CuryID,
            (object) account.CashAccountCD
          });
        int valueOrDefault5 = (int) currency.DecimalPlaces.GetValueOrDefault();
        Decimal? curyAmountDay0 = cashFlowForecast1.CuryAmountDay0;
        Decimal? nullable3;
        ref Decimal? local = ref nullable3;
        nullable1 = currencyRate.CuryRate;
        Decimal CuryRate = nullable1.Value;
        string curyMultDiv = currencyRate.CuryMultDiv;
        int BasePrecision = valueOrDefault5;
        PaymentEntry.CuryConvBase(curyAmountDay0, out local, CuryRate, curyMultDiv, BasePrecision);
        cashFlowForecast1.AmountDay0 = nullable3;
      }
      if (!string.IsNullOrEmpty(filter.CuryID) && filter.CuryID != account.CuryID)
      {
        cashFlowForecast1.CuryAmountDay0 = this.ConvertFromBase(cashFlowForecast1.AmountDay0, this.convertToCurrency);
        cashFlowForecast1.CuryID = this.convertToCurrency.CuryID;
      }
    }
  }

  public void CalcCashAccountBalance(
    DateTime startDate,
    CashFlowEnq.CashFlowFilter filter,
    Dictionary<int, CashFlowForecast2> result,
    Dictionary<int, CurrencyRate> rates)
  {
    int? cashAccountId1 = filter.CashAccountID;
    CMSetup cmSetup = PXResultset<CMSetup>.op_Implicit(PXSelectBase<CMSetup, PXSelect<CMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    PXSelectBase<CashAccount> pxSelectBase = (PXSelectBase<CashAccount>) new PXSelectJoinGroupBy<CashAccount, LeftJoin<CADailySummary, On<CADailySummary.cashAccountID, Equal<CashAccount.cashAccountID>, And<CADailySummary.tranDate, Less<Required<CADailySummary.tranDate>>>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<CashAccount.branchID>>, InnerJoin<PX.Objects.CM.Currency, On<PX.Objects.CM.Currency.curyID, Equal<PX.Objects.GL.Branch.baseCuryID>>>>>, Where<Match<CashAccount, Current<AccessInfo.userName>>>, Aggregate<GroupBy<CADailySummary.cashAccountID, Sum<CADailySummary.amtReleasedClearedCr, Sum<CADailySummary.amtReleasedUnclearedCr, Sum<CADailySummary.amtUnreleasedUnclearedCr, Sum<CADailySummary.amtUnreleasedClearedCr, Sum<CADailySummary.amtReleasedClearedDr, Sum<CADailySummary.amtReleasedUnclearedDr, Sum<CADailySummary.amtUnreleasedUnclearedDr, Sum<CADailySummary.amtUnreleasedClearedDr>>>>>>>>>>>((PXGraph) this);
    if (cashAccountId1.HasValue)
      pxSelectBase.WhereAnd<Where<CADailySummary.cashAccountID, Equal<Required<CADailySummary.cashAccountID>>>>();
    foreach (PXResult<CashAccount, CADailySummary, PX.Objects.GL.Branch, PX.Objects.CM.Currency> pxResult in pxSelectBase.Select(new object[2]
    {
      (object) startDate,
      (object) cashAccountId1
    }))
    {
      CADailySummary caDailySummary = PXResult<CashAccount, CADailySummary, PX.Objects.GL.Branch, PX.Objects.CM.Currency>.op_Implicit(pxResult);
      CashAccount cashAccount = PXResult<CashAccount, CADailySummary, PX.Objects.GL.Branch, PX.Objects.CM.Currency>.op_Implicit(pxResult);
      PX.Objects.CM.Currency currency = PXResult<CashAccount, CADailySummary, PX.Objects.GL.Branch, PX.Objects.CM.Currency>.op_Implicit(pxResult);
      Dictionary<int, CashFlowForecast2> dictionary1 = result;
      int? cashAccountId2 = cashAccount.CashAccountID;
      int key1 = cashAccountId2.Value;
      CashFlowForecast2 cashFlowForecast2_1;
      ref CashFlowForecast2 local1 = ref cashFlowForecast2_1;
      if (!dictionary1.TryGetValue(key1, out local1))
      {
        cashFlowForecast2_1 = new CashFlowForecast2();
        cashFlowForecast2_1.RecordType = new int?(0);
        CashFlowForecast2 cashFlowForecast2_2 = cashFlowForecast2_1;
        cashAccountId2 = cashAccount.CashAccountID;
        int? nullable = new int?(cashAccountId2.Value);
        cashFlowForecast2_2.CashAccountID = nullable;
        cashFlowForecast2_1.TranDate = new DateTime?(startDate);
        cashFlowForecast2_1.CuryID = cashAccount.CuryID;
        Dictionary<int, CashFlowForecast2> dictionary2 = result;
        cashAccountId2 = caDailySummary.CashAccountID;
        int key2 = cashAccountId2.Value;
        CashFlowForecast2 cashFlowForecast2_3 = cashFlowForecast2_1;
        dictionary2[key2] = cashFlowForecast2_3;
      }
      int num = cashAccount.CuryID != currency.CuryID ? 1 : 0;
      cashFlowForecast2_1.CuryAmountDay = new Decimal?(caDailySummary.AmtReleasedClearedDr.GetValueOrDefault() + caDailySummary.AmtReleasedUnclearedDr.GetValueOrDefault() - (caDailySummary.AmtReleasedClearedCr.GetValueOrDefault() + caDailySummary.AmtReleasedUnclearedCr.GetValueOrDefault()));
      cashFlowForecast2_1.AmountDay = cashFlowForecast2_1.CuryAmountDay;
      if (num != 0)
      {
        if (string.IsNullOrEmpty(cashAccount.CuryRateTypeID) && (cmSetup == null || string.IsNullOrEmpty(cmSetup.CARateTypeDflt)))
          throw new PXException("A currency rate type is not defined to the Cash ccount {0} and no default is provided for CA Module in CM Setup", new object[1]
          {
            (object) cashAccount.CashAccountCD
          });
        Dictionary<int, CurrencyRate> accountRates1 = this.accountRates;
        cashAccountId2 = cashAccount.CashAccountID;
        int key3 = cashAccountId2.Value;
        CurrencyRate curyRate;
        ref CurrencyRate local2 = ref curyRate;
        if (!accountRates1.TryGetValue(key3, out local2))
        {
          if (!string.IsNullOrEmpty(cashAccount.CuryRateTypeID))
          {
            string curyRateTypeId = cashAccount.CuryRateTypeID;
          }
          else
          {
            string caRateTypeDflt = cmSetup.CARateTypeDflt;
          }
          curyRate = this.findCuryRate(cashAccount.CuryID, currency.CuryID, cashAccount.CuryRateTypeID, startDate);
          Dictionary<int, CurrencyRate> accountRates2 = this.accountRates;
          cashAccountId2 = cashAccount.CashAccountID;
          int key4 = cashAccountId2.Value;
          CurrencyRate currencyRate = curyRate;
          accountRates2.Add(key4, currencyRate);
        }
        if (curyRate == null)
          throw new PXException("A currency rate for conversion from Currency {0} to Base Currency {1} is not found for account {2}", new object[3]
          {
            (object) cashAccount.CuryID,
            (object) currency.CuryID,
            (object) cashAccount.CashAccountCD
          });
        int valueOrDefault = (int) currency.DecimalPlaces.GetValueOrDefault();
        Decimal? BaseAmt;
        PaymentEntry.CuryConvBase(cashFlowForecast2_1.CuryAmountDay, out BaseAmt, curyRate.CuryRate.Value, curyRate.CuryMultDiv, valueOrDefault);
        cashFlowForecast2_1.AmountDay = BaseAmt;
      }
      if (!string.IsNullOrEmpty(filter.CuryID) && filter.CuryID != cashAccount.CuryID)
      {
        cashFlowForecast2_1.CuryAmountDay = this.ConvertFromBase(cashFlowForecast2_1.AmountDay, this.convertToCurrency);
        cashFlowForecast2_1.CuryID = this.convertToCurrency.CuryID;
      }
    }
  }

  protected CurrencyRate findCuryRate(
    string fromCuryID,
    string toCuryID,
    string aCuryRateType,
    DateTime aDate)
  {
    CurrencyRate curyRate = PXResultset<CurrencyRate>.op_Implicit(PXSelectBase<CurrencyRate, PXSelectReadonly<CurrencyRate, Where<CurrencyRate.fromCuryID, Equal<Required<CurrencyRate.fromCuryID>>, And<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<CurrencyRate.curyRateType>>, And<CurrencyRate.curyEffDate, LessEqual<Required<CurrencyRate.curyEffDate>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) fromCuryID,
      (object) toCuryID,
      (object) aCuryRateType,
      (object) aDate
    }));
    if (curyRate == null)
      curyRate = PXResultset<CurrencyRate>.op_Implicit(PXSelectBase<CurrencyRate, PXSelectReadonly<CurrencyRate, Where<CurrencyRate.fromCuryID, Equal<Required<CurrencyRate.fromCuryID>>, And<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<CurrencyRate.curyRateType>>, And<CurrencyRate.curyEffDate, Greater<Required<CurrencyRate.curyEffDate>>>>>>, OrderBy<Asc<CurrencyRate.curyEffDate>>>.Config>.Select((PXGraph) this, new object[4]
      {
        (object) fromCuryID,
        (object) toCuryID,
        (object) aCuryRateType,
        (object) aDate
      }));
    return curyRate;
  }

  protected CashAccount findDefaultCashAccount(PX.Objects.CA.Light.ARInvoice aDoc)
  {
    CashAccount defaultCashAccount = (CashAccount) null;
    PXCache cache = ((PXSelectBase) this.arInvoice).Cache;
    PX.Objects.CA.Light.ARInvoice copy = (PX.Objects.CA.Light.ARInvoice) cache.CreateCopy((object) aDoc);
    if (string.IsNullOrEmpty(aDoc.PaymentMethodID))
    {
      object obj;
      cache.RaiseFieldDefaulting<PX.Objects.CA.Light.ARInvoice.paymentMethodID>((object) copy, ref obj);
      copy.PaymentMethodID = (string) obj;
    }
    if (!aDoc.PMInstanceID.HasValue)
    {
      object obj;
      cache.RaiseFieldDefaulting<PX.Objects.CA.Light.ARInvoice.pMInstanceID>((object) copy, ref obj);
      copy.PMInstanceID = obj as int?;
    }
    object obj1;
    cache.RaiseFieldDefaulting<PX.Objects.CA.Light.ARInvoice.cashAccountID>((object) copy, ref obj1);
    int? nullable = obj1 as int?;
    if (nullable.HasValue)
      defaultCashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) nullable
      }));
    return defaultCashAccount;
  }

  protected CashAccount findDefaultCashAccount(PX.Objects.CA.Light.APInvoice aDoc)
  {
    CashAccount defaultCashAccount = (CashAccount) null;
    PXCache cache = ((PXSelectBase) this.apInvoice).Cache;
    PX.Objects.CA.Light.APInvoice copy = (PX.Objects.CA.Light.APInvoice) cache.CreateCopy((object) aDoc);
    if (string.IsNullOrEmpty(aDoc.PayTypeID))
    {
      object obj;
      cache.RaiseFieldDefaulting<PX.Objects.CA.Light.APInvoice.payTypeID>((object) copy, ref obj);
      copy.PayTypeID = (string) obj;
    }
    object obj1;
    cache.RaiseFieldDefaulting<PX.Objects.CA.Light.APInvoice.payAccountID>((object) copy, ref obj1);
    int? nullable = obj1 as int?;
    if (nullable.HasValue)
      defaultCashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) nullable
      }));
    return defaultCashAccount;
  }

  protected CashFlowForecast Create(DateTime startDate, CashAccount aCashAccount, PX.Objects.CA.Light.ARInvoice src)
  {
    return new CashFlowForecast()
    {
      BAccountID = src.CustomerID,
      StartingDate = new DateTime?(startDate),
      RecordType = new int?(1),
      CashAccountID = aCashAccount.CashAccountID,
      CuryID = aCashAccount.CuryID,
      AcctCuryID = aCashAccount.CuryID
    };
  }

  protected CashFlowForecast Create(DateTime startDate, CashAccount aCashAccount, PX.Objects.CA.Light.APInvoice src)
  {
    return new CashFlowForecast()
    {
      BAccountID = src.VendorID,
      StartingDate = new DateTime?(startDate),
      RecordType = new int?(-1),
      CashAccountID = aCashAccount.CashAccountID,
      CuryID = aCashAccount.CuryID,
      AcctCuryID = aCashAccount.CuryID
    };
  }

  protected CashFlowForecast Create(DateTime startDate, PX.Objects.AP.APPayment src, bool applied)
  {
    return new CashFlowForecast()
    {
      BAccountID = src.VendorID,
      StartingDate = new DateTime?(startDate),
      RecordType = new int?(applied ? -1 : -2),
      CashAccountID = src.CashAccountID,
      CuryID = src.CuryID,
      AcctCuryID = src.CuryID
    };
  }

  protected CashFlowForecast Create(DateTime startDate, PX.Objects.AR.ARPayment src, bool applied)
  {
    return new CashFlowForecast()
    {
      BAccountID = src.CustomerID,
      StartingDate = new DateTime?(startDate),
      RecordType = new int?(applied ? 1 : 2),
      CashAccountID = src.CashAccountID,
      CuryID = src.CuryID,
      AcctCuryID = src.CuryID
    };
  }

  protected CashFlowForecast Create(DateTime startDate, CADailySummary src, CashAccount account)
  {
    return new CashFlowForecast()
    {
      StartingDate = new DateTime?(startDate),
      RecordType = new int?(0),
      CashAccountID = src.CashAccountID,
      CuryID = account.CuryID,
      AcctCuryID = account.CuryID
    };
  }

  protected CashFlowForecast Create(DateTime startDate, CashForecastTran src)
  {
    return new CashFlowForecast()
    {
      StartingDate = new DateTime?(startDate),
      RecordType = new int?(src.DrCr == "D" ? 1 : -1),
      CashAccountID = src.CashAccountID,
      EntryID = src.TranID,
      CuryID = src.CuryID,
      AcctCuryID = src.CuryID
    };
  }

  protected static CashFlowForecast2 Create(CashFlowForecast src, int offset)
  {
    return new CashFlowForecast2()
    {
      RecordType = src.RecordType,
      TranDate = new DateTime?(src.StartingDate.Value.AddDays((double) offset)),
      BAccountID = src.BAccountID,
      CashAccountID = src.CashAccountID,
      EntryID = src.EntryID,
      CuryID = src.CuryID,
      AcctCuryID = src.AcctCuryID
    };
  }

  protected CashFlowForecast AddAmount(CashFlowForecast dst, PX.Objects.AR.ARPayment doc)
  {
    Decimal sign = doc.DrCr == "D" ? 1M : -1M;
    Decimal? nullable1 = new Decimal?(0M);
    Decimal? baseValue = new Decimal?(0M);
    Decimal? nullable2;
    if (doc.DocType == "CSL" || doc.DocType == "RCS")
    {
      nullable2 = doc.CuryOrigDocAmt;
      baseValue = doc.OrigDocAmt;
    }
    else
    {
      nullable2 = doc.CuryDocBal;
      baseValue = doc.DocBal;
    }
    if (this.convertToCurrency != null && doc.CuryID != this.convertToCurrency.CuryID)
    {
      nullable2 = this.ConvertFromBase(nullable2, this.convertToCurrency);
      dst.CuryID = this.convertToCurrency.CuryID;
    }
    this.AddAmount(dst, doc.DocDate.Value, nullable2, baseValue, sign);
    return dst;
  }

  protected CashFlowForecast AddAmount(CashFlowForecast dst, PX.Objects.AR.ARPayment doc, PX.Objects.AR.ARAdjust adj)
  {
    DateTime? nullable1 = doc.DocDate;
    DateTime dateTime1 = nullable1.Value;
    nullable1 = dst.StartingDate;
    DateTime dateTime2 = nullable1.Value;
    int days = (dateTime1 - dateTime2).Days;
    Decimal sign = doc.DrCr == "D" ? 1M : -1M;
    Decimal? nullable2 = new Decimal?(0M);
    Decimal? nullable3 = new Decimal?(0M);
    Decimal? curyValue;
    Decimal? nullable4;
    if (doc.DocType == "CSL" || doc.DocType == "RCS")
    {
      curyValue = doc.CuryOrigDocAmt;
      nullable4 = doc.OrigDocAmt;
    }
    else
    {
      sign = adj.AdjgBalSign ?? 1M;
      curyValue = adj.CuryAdjgAmt;
      nullable4 = adj.AdjAmt;
    }
    if (this.convertToCurrency != null && doc.CuryID != this.convertToCurrency.CuryID)
    {
      curyValue = this.ConvertFromBase(nullable4, this.convertToCurrency);
      dst.CuryID = this.convertToCurrency.CuryID;
    }
    this.AddAmount(dst, doc.DocDate.Value, curyValue, nullable4, sign);
    return dst;
  }

  protected CashFlowForecast AddAmount(CashFlowForecast dst, PX.Objects.AP.APPayment doc, PX.Objects.AP.APAdjust adj)
  {
    DateTime? nullable1 = doc.DocDate;
    DateTime dateTime1 = nullable1.Value;
    nullable1 = dst.StartingDate;
    DateTime dateTime2 = nullable1.Value;
    int days = (dateTime1 - dateTime2).Days;
    Decimal sign = doc.DrCr == "C" ? 1M : -1M;
    Decimal? nullable2 = new Decimal?(0M);
    Decimal? nullable3 = new Decimal?(0M);
    Decimal? curyValue;
    Decimal? nullable4;
    if (doc.DocType == "QCK" || doc.DocType == "VQC")
    {
      curyValue = doc.CuryOrigDocAmt;
      nullable4 = doc.OrigDocAmt;
    }
    else
    {
      sign = adj.AdjgBalSign ?? 1M;
      curyValue = adj.CuryAdjgAmt;
      nullable4 = adj.AdjAmt;
    }
    if (this.convertToCurrency != null && doc.CuryID != this.convertToCurrency.CuryID)
    {
      curyValue = this.ConvertFromBase(nullable4, this.convertToCurrency);
      dst.CuryID = this.convertToCurrency.CuryID;
    }
    this.AddAmount(dst, doc.DocDate.Value, curyValue, nullable4, sign);
    return dst;
  }

  protected CashFlowForecast AddAmount(CashFlowForecast dst, PX.Objects.AP.APPayment doc)
  {
    int num = doc.DocType == "QCK" ? 1 : (doc.DocType == "VQC" ? 1 : 0);
    Decimal? curyValue = num != 0 ? doc.CuryOrigDocAmt : doc.CuryDocBal;
    Decimal? nullable = num != 0 ? doc.OrigDocAmt : doc.DocBal;
    if (this.convertToCurrency != null && doc.CuryID != this.convertToCurrency.CuryID)
    {
      curyValue = this.ConvertFromBase(nullable, this.convertToCurrency);
      dst.CuryID = this.convertToCurrency.CuryID;
    }
    Decimal sign = doc.DrCr == "C" ? 1M : -1M;
    this.AddAmount(dst, doc.DocDate.Value, curyValue, nullable, sign);
    return dst;
  }

  protected CashFlowForecast AddAmount(
    CashFlowForecast dst,
    DateTime docDate,
    Decimal? curyValue,
    Decimal? baseValue,
    Decimal sign)
  {
    int offset = (docDate - dst.StartingDate.Value).Days;
    if (offset < 0)
      offset = 0;
    this.AddValue(dst, offset, curyValue.GetValueOrDefault() * sign, true);
    this.AddValue(dst, offset, baseValue.GetValueOrDefault() * sign, false);
    return dst;
  }

  protected CashFlowForecast AddAmount(
    CashFlowForecast dst,
    CashForecastTran doc,
    CurrencyRate acctRate,
    PX.Objects.CM.Currency baseCurrency)
  {
    DateTime? nullable1 = doc.TranDate;
    DateTime dateTime1 = nullable1.Value;
    nullable1 = dst.StartingDate;
    DateTime dateTime2 = nullable1.Value;
    int days = (dateTime1 - dateTime2).Days;
    Decimal sign = 1M;
    Decimal? nullable2 = new Decimal?(doc.CuryTranAmt.Value);
    Decimal? curyValue = new Decimal?(doc.CuryTranAmt.Value);
    if (doc.CuryID != baseCurrency.CuryID)
    {
      if (acctRate == null)
        throw new PXException("A currency rate for conversion from Currency {0} to Base Currency {1} is not found for account {2}", new object[3]
        {
          (object) doc.CuryID,
          (object) baseCurrency.CuryID,
          (object) doc.CashAccountID
        });
      Decimal? nullable3 = doc.CuryTranAmt;
      Decimal? CuryAmt = new Decimal?(nullable3.Value);
      ref Decimal? local = ref nullable2;
      nullable3 = acctRate.CuryRate;
      Decimal CuryRate = nullable3.Value;
      string curyMultDiv = acctRate.CuryMultDiv;
      int BasePrecision = (int) baseCurrency.DecimalPlaces.Value;
      PaymentEntry.CuryConvBase(CuryAmt, out local, CuryRate, curyMultDiv, BasePrecision);
    }
    if (this.convertToCurrency != null && doc.CuryID != this.convertToCurrency.CuryID)
      curyValue = this.ConvertFromBase(nullable2, this.convertToCurrency);
    this.AddAmount(dst, doc.TranDate.Value, curyValue, nullable2, sign);
    return dst;
  }

  protected static void ConvertAmount(
    ref Decimal? aCuryValue,
    ref Decimal? aValue,
    string aSrcCuryID,
    PX.Objects.CM.Currency destCurrency,
    PX.Objects.CM.Currency baseCurrency,
    CurrencyRate destRate,
    CurrencyRate srcRate)
  {
    Decimal? nullable1 = aCuryValue;
    Decimal? nullable2 = aValue;
    Decimal? CuryDocBal = nullable1;
    Decimal? DocBal = nullable2;
    string curyId1 = destCurrency.CuryID;
    string DocCuryID = aSrcCuryID;
    string curyId2 = baseCurrency.CuryID;
    Decimal? curyRate;
    Decimal PayCuryRate;
    if (destRate == null)
    {
      PayCuryRate = 1M;
    }
    else
    {
      curyRate = destRate.CuryRate;
      PayCuryRate = curyRate.Value;
    }
    string PayCuryMultDiv = destRate != null ? destRate.CuryMultDiv : "M";
    Decimal DocCuryRate;
    if (srcRate == null)
    {
      DocCuryRate = 1M;
    }
    else
    {
      curyRate = srcRate.CuryRate;
      DocCuryRate = curyRate.Value;
    }
    string DocCuryMultDiv = srcRate != null ? srcRate.CuryMultDiv : "M";
    int CuryPrecision = (int) destCurrency.DecimalPlaces.Value;
    short? decimalPlaces = baseCurrency.DecimalPlaces;
    int BasePrecision1 = (int) decimalPlaces.Value;
    Decimal? nullable3 = PaymentEntry.CalcBalances(CuryDocBal, DocBal, curyId1, DocCuryID, curyId2, PayCuryRate, PayCuryMultDiv, DocCuryRate, DocCuryMultDiv, CuryPrecision, BasePrecision1);
    if (destCurrency.CuryID != baseCurrency.CuryID)
    {
      Decimal? CuryAmt = nullable3;
      Decimal? nullable4;
      ref Decimal? local = ref nullable4;
      curyRate = destRate.CuryRate;
      Decimal CuryRate = curyRate.Value;
      string curyMultDiv = destRate.CuryMultDiv;
      decimalPlaces = baseCurrency.DecimalPlaces;
      int BasePrecision2 = (int) decimalPlaces.Value;
      PaymentEntry.CuryConvBase(CuryAmt, out local, CuryRate, curyMultDiv, BasePrecision2);
      aValue = new Decimal?(nullable4.GetValueOrDefault());
    }
    else
      aValue = nullable3;
    aCuryValue = nullable3;
  }

  protected virtual Decimal? ConvertFromBase(Decimal? baseAmount, PX.Objects.CM.Currency convertToCurrency)
  {
    CurrencyRate currencyRate = (CurrencyRate) null;
    string curyId = convertToCurrency.CuryID;
    if (curyId == this.baseCurrency.CuryID)
      return baseAmount;
    if (!this.currencyRates.TryGetValue(curyId, out currencyRate))
    {
      currencyRate = this.findCuryRate(curyId, this.baseCurrency.CuryID, this.currencyRateType, this.startDate);
      this.currencyRates[curyId] = currencyRate != null ? currencyRate : throw new PXException("The currency rate is not defined. Specify it on the Currency Rates (CM301000) form.");
    }
    int valueOrDefault = (int) convertToCurrency.DecimalPlaces.GetValueOrDefault();
    Decimal? CuryAmt;
    PaymentEntry.CuryConvCury(baseAmount, out CuryAmt, currencyRate.CuryRate.Value, currencyRate.CuryMultDiv, valueOrDefault);
    return CuryAmt;
  }

  protected virtual string ConvertDocAmount(
    ref Decimal? curyValue,
    ref Decimal? value,
    PX.Objects.CM.CurrencyInfo aDocCuryInfo,
    CashAccount aAccount,
    CashFlowEnq.CashFlowFilter filter)
  {
    string curyId = filter.CuryID;
    bool flag = true;
    if (string.IsNullOrEmpty(curyId))
    {
      if (aAccount != null && aAccount.AccountID.HasValue)
        curyId = aAccount.CuryID;
      flag = false;
    }
    CurrencyRate destRate = (CurrencyRate) null;
    CurrencyRate curyRate = this.findCuryRate(aDocCuryInfo.CuryID, this.baseCurrency.CuryID, aDocCuryInfo.CuryRateTypeID, this.startDate);
    PX.Objects.CM.Currency destCurrency;
    if (!flag)
    {
      destCurrency = PXResultset<PX.Objects.CM.Currency>.op_Implicit(PXSelectBase<PX.Objects.CM.Currency, PXSelect<PX.Objects.CM.Currency, Where<PX.Objects.CM.Currency.curyID, Equal<Required<PX.Objects.CM.Currency.curyID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) curyId
      }));
      Dictionary<int, CurrencyRate> accountRates1 = this.accountRates;
      int? cashAccountId = aAccount.CashAccountID;
      int key1 = cashAccountId.Value;
      ref CurrencyRate local = ref destRate;
      if (!accountRates1.TryGetValue(key1, out local) && curyId != this.baseCurrency.CuryID)
      {
        string aCuryRateType = aAccount != null ? aAccount.CuryRateTypeID : string.Empty;
        if (string.IsNullOrEmpty(aCuryRateType))
        {
          CMSetup cmSetup = PXResultset<CMSetup>.op_Implicit(PXSelectBase<CMSetup, PXSelect<CMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
          aCuryRateType = cmSetup != null && !string.IsNullOrEmpty(cmSetup.CARateTypeDflt) ? cmSetup.CARateTypeDflt : throw new PXException("A currency rate type is not defined to the Cash ccount {0} and no default is provided for CA Module in CM Setup", new object[1]
          {
            (object) aAccount.CashAccountCD
          });
        }
        destRate = this.findCuryRate(aAccount.CuryID, this.baseCurrency.CuryID, aCuryRateType, this.startDate);
        Dictionary<int, CurrencyRate> accountRates2 = this.accountRates;
        cashAccountId = aAccount.CashAccountID;
        int key2 = cashAccountId.Value;
        CurrencyRate currencyRate = destRate;
        accountRates2[key2] = currencyRate;
      }
    }
    else
    {
      destCurrency = this.convertToCurrency;
      if (!this.currencyRates.TryGetValue(curyId, out destRate))
      {
        destRate = this.findCuryRate(curyId, this.baseCurrency.CuryID, this.currencyRateType, this.startDate);
        this.currencyRates[curyId] = destRate;
      }
    }
    CashFlowEnq.ConvertAmount(ref curyValue, ref value, aDocCuryInfo.CuryID, destCurrency, this.baseCurrency, destRate, curyRate);
    return curyId;
  }

  protected void Convert(List<CashFlowForecast2> dest, CashFlowForecast src, bool skipIfZero)
  {
    for (int offset = 0; offset < this.AmountFieldsNumber; ++offset)
    {
      Decimal? nullable1 = this.GetValue(src, offset, true);
      Decimal? nullable2 = this.GetValue(src, offset, false);
      if (!skipIfZero || !(nullable1.GetValueOrDefault() == 0M) || !(nullable2.GetValueOrDefault() == 0M))
      {
        CashFlowForecast2 cashFlowForecast2 = CashFlowEnq.Create(src, offset);
        cashFlowForecast2.CuryAmountDay = nullable1;
        cashFlowForecast2.AmountDay = nullable2;
        dest.Add(cashFlowForecast2);
      }
    }
  }

  [Serializable]
  public class CashFlowFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _OrganizationBaseCuryID;
    protected DateTime? _StartDate;
    protected bool? _IncludeUnassignedDocs;
    protected int? _DefaultAccountID;
    protected bool? _IncludeUnreleased;
    protected bool? _IncludeUnapplied;
    protected bool? _IncludeScheduled;
    protected bool? _SummaryOnly;
    protected string _CuryID;
    protected string _CuryRateTypeID;
    public bool StrictAccountCondition;

    [OrganizationTree(null, null, null, false)]
    [PXUIRequired(typeof (FeatureInstalled<FeaturesSet.multipleBaseCurrencies>))]
    [PXUIVisible(typeof (Where2<FeatureInstalled<FeaturesSet.branch>, Or<FeatureInstalled<FeaturesSet.multiCompany>>>))]
    [PXUIField(DisplayName = "Company/Branch")]
    public int? OrgBAccountID { get; set; }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXFormula(typeof (GetOrganizationBaseCuryID<CashFlowEnq.CashFlowFilter.orgBAccountID>))]
    public string OrganizationBaseCuryID
    {
      get => this._OrganizationBaseCuryID;
      set => this._OrganizationBaseCuryID = value;
    }

    [CashAccount(typeof (Search<CashAccount.cashAccountID, Where<CashAccount.branchID, InsideBranchesOf<Current<CashFlowEnq.CashFlowFilter.orgBAccountID>>>, OrderBy<Asc<CashAccount.cashAccountCD>>>))]
    [PXDefault(typeof (Search<CashAccount.cashAccountID, Where<CashAccount.branchID, InsideBranchesOf<Current<CashFlowEnq.CashFlowFilter.orgBAccountID>>>, OrderBy<Asc<CashAccount.cashAccountCD>>>))]
    public virtual int? CashAccountID { get; set; }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? StartDate
    {
      get => this._StartDate;
      set => this._StartDate = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Include Documents Without Cash Account")]
    public virtual bool? IncludeUnassignedDocs
    {
      get => this._IncludeUnassignedDocs;
      set => this._IncludeUnassignedDocs = value;
    }

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "All Cash Accounts")]
    public virtual bool? AllCashAccounts { get; set; }

    [CashAccount(DisplayName = "Default Cash Account")]
    public virtual int? DefaultAccountID
    {
      get => this._DefaultAccountID;
      set => this._DefaultAccountID = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Include Unreleased Documents")]
    public virtual bool? IncludeUnreleased
    {
      get => this._IncludeUnreleased;
      set => this._IncludeUnreleased = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Include Unapplied Payments")]
    public virtual bool? IncludeUnapplied
    {
      get => this._IncludeUnapplied;
      set => this._IncludeUnapplied = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Include Scheduled Documents")]
    public virtual bool? IncludeScheduled
    {
      get => this._IncludeScheduled;
      set => this._IncludeScheduled = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Show Summary Only")]
    public virtual bool? SummaryOnly
    {
      get => this._SummaryOnly;
      set => this._SummaryOnly = value;
    }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXUIField(DisplayName = "Forecast Currency", Required = false)]
    [PXDefault(typeof (Search<PX.Objects.GL.Account.curyID, Where<PX.Objects.GL.Account.accountID, Equal<Optional<CashFlowEnq.CashFlowFilter.cashAccountID>>>>))]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    [PXDBString(6, IsUnicode = true)]
    [PXDefault]
    [PXSelector(typeof (PX.Objects.CM.CurrencyRateType.curyRateTypeID))]
    [PXUIField(DisplayName = "Currency Rate Type")]
    public virtual string CuryRateTypeID
    {
      get => this._CuryRateTypeID;
      set => this._CuryRateTypeID = value;
    }

    public abstract class orgBAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CashFlowEnq.CashFlowFilter.orgBAccountID>
    {
    }

    public abstract class organizationBaseCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CashFlowEnq.CashFlowFilter.organizationBaseCuryID>
    {
    }

    public abstract class cashAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CashFlowEnq.CashFlowFilter.cashAccountID>
    {
    }

    public abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CashFlowEnq.CashFlowFilter.startDate>
    {
    }

    public abstract class includeUnassignedDocs : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CashFlowEnq.CashFlowFilter.includeUnassignedDocs>
    {
    }

    public abstract class allCashAccounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CashFlowEnq.CashFlowFilter.allCashAccounts>
    {
    }

    public abstract class defaultAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CashFlowEnq.CashFlowFilter.defaultAccountID>
    {
    }

    public abstract class includeUnreleased : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CashFlowEnq.CashFlowFilter.includeUnreleased>
    {
    }

    public abstract class includeUnapplied : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CashFlowEnq.CashFlowFilter.includeUnapplied>
    {
    }

    public abstract class includeScheduled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CashFlowEnq.CashFlowFilter.includeScheduled>
    {
    }

    public abstract class summaryOnly : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CashFlowEnq.CashFlowFilter.summaryOnly>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CashFlowEnq.CashFlowFilter.curyID>
    {
    }

    public abstract class curyRateTypeID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CashFlowEnq.CashFlowFilter.curyRateTypeID>
    {
    }
  }

  public enum EntityType
  {
    BAccountID,
    CashForecastTranID,
  }

  public class FlowKey : Quadplet<int, int, int, int>
  {
    public FlowKey(
      CashFlowForecastRecordType.RecordType recordType,
      int acctID,
      CashFlowEnq.EntityType entityType,
      int entityID)
      : base(acctID, (int) recordType, (int) entityType, entityID)
    {
    }

    public FlowKey(int acctID, PX.Objects.AP.APInvoice doc)
      : this(CashFlowForecastRecordType.RecordType.CashOut, acctID, CashFlowEnq.EntityType.BAccountID, doc.VendorID.Value)
    {
    }

    public FlowKey(int acctID, PX.Objects.AR.ARInvoice doc)
      : this(CashFlowForecastRecordType.RecordType.CashIn, acctID, CashFlowEnq.EntityType.BAccountID, doc.CustomerID.Value)
    {
    }

    public FlowKey(int acctID, PX.Objects.CA.Light.APInvoice doc)
      : this(CashFlowForecastRecordType.RecordType.CashOut, acctID, CashFlowEnq.EntityType.BAccountID, doc.VendorID.Value)
    {
    }

    public FlowKey(int acctID, PX.Objects.CA.Light.ARInvoice doc)
      : this(CashFlowForecastRecordType.RecordType.CashIn, acctID, CashFlowEnq.EntityType.BAccountID, doc.CustomerID.Value)
    {
    }

    public FlowKey(PX.Objects.AP.APPayment doc, bool isApplied)
    {
      int num = isApplied ? -1 : -2;
      int? nullable = doc.CashAccountID;
      int acctID = nullable.Value;
      nullable = doc.VendorID;
      int entityID = nullable.Value;
      // ISSUE: explicit constructor call
      this.\u002Ector((CashFlowForecastRecordType.RecordType) num, acctID, CashFlowEnq.EntityType.BAccountID, entityID);
    }

    public FlowKey(PX.Objects.AR.ARPayment doc, bool isApplied)
    {
      int num = isApplied ? 1 : 2;
      int? nullable = doc.CashAccountID;
      int acctID = nullable.Value;
      nullable = doc.CustomerID;
      int entityID = nullable.Value;
      // ISSUE: explicit constructor call
      this.\u002Ector((CashFlowForecastRecordType.RecordType) num, acctID, CashFlowEnq.EntityType.BAccountID, entityID);
    }

    public FlowKey(CashForecastTran doc)
      : this(doc.DrCr == "C" ? CashFlowForecastRecordType.RecordType.CashIn : CashFlowForecastRecordType.RecordType.CashOut, doc.CashAccountID.Value, CashFlowEnq.EntityType.CashForecastTranID, doc.TranID.Value)
    {
    }
  }
}

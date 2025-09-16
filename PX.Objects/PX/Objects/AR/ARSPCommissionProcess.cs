// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSPCommissionProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.BQLConstants;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
[Serializable]
public class ARSPCommissionProcess : PXGraph<
#nullable disable
ARSPCommissionProcess>
{
  public PXCancel<ARSPCommissionPeriod> Cancel;
  public PXAction<ARSPCommissionPeriod> ProcessAll;
  public PXSelect<ARSPCommissionPeriod> Filter;
  public PXAction<ARSPCommissionPeriod> reviewSPPeriod;
  [PXFilterable(new Type[] {})]
  public PXSelect<ARSPCommissionProcess.ARSalesPerTranExt> ToProcess;
  public PXSelect<ARSPCommissionPeriod, Where<ARSPCommissionPeriod.status, Equal<ARSPCommissionPeriodStatus.open>, And<ARSPCommissionPeriod.startDate, LessEqual<Required<ARSPCommissionPeriod.startDate>>, And<ARSPCommissionPeriod.endDate, Greater<Required<ARSPCommissionPeriod.endDate>>>>>, OrderBy<Asc<ARSPCommissionPeriod.endDate>>> ARSPCommnPeriod_Current;
  public PXSelect<ARSPCommissionPeriod, Where<ARSPCommissionPeriod.status, Equal<ARSPCommissionPeriodStatus.open>>, OrderBy<Asc<ARSPCommissionPeriod.endDate>>> ARSPCommnPeriod_Open;
  public PXSelect<ARSPCommissionYear, Where<ARSPCommissionYear.year, Equal<Required<ARSPCommissionYear.year>>>> ARSPCommnYear_Current;
  public PXSelectOrderBy<ARSPCommissionPeriod, OrderBy<Desc<ARSPCommissionPeriod.endDate>>> CommnPeriod_Last;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public ARSPCommissionProcess.DoProcess doProcess;
  private string[] postmessages;

  public ARSPCommissionProcess()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    PXCache cache = ((PXSelectBase) this.ToProcess).Cache;
    PXUIFieldAttribute.SetEnabled(cache, (string) null, false);
    cache.AllowInsert = false;
    cache.AllowDelete = false;
    this.doProcess = new ARSPCommissionProcess.DoProcess(ARSPCommissionProcess.ProcessSPCommissions);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Filter).Cache, (string) null, false);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ReviewSPPeriod(PXAdapter adapter)
  {
    if (((PXSelectBase<ARSPCommissionPeriod>) this.Filter).Current != null)
    {
      ARSPCommissionPeriod current = ((PXSelectBase<ARSPCommissionPeriod>) this.Filter).Current;
      ARSPCommissionReview instance = PXGraph.CreateInstance<ARSPCommissionReview>();
      ARSPCommissionPeriod commissionPeriod;
      if (current.Status == "N")
        commissionPeriod = PXResultset<ARSPCommissionPeriod>.op_Implicit(PXSelectBase<ARSPCommissionPeriod, PXSelect<ARSPCommissionPeriod, Where<ARSPCommissionPeriod.commnPeriodID, LessEqual<Required<ARSPCommissionPeriod.commnPeriodID>>, And<ARSPCommissionPeriod.status, NotEqual<ARSPCommissionPeriodStatus.open>>>, OrderBy<Desc<ARSPCommissionPeriod.commnPeriodID>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) current.CommnPeriodID
        }));
      else
        commissionPeriod = PXResultset<ARSPCommissionPeriod>.op_Implicit(PXSelectBase<ARSPCommissionPeriod, PXSelect<ARSPCommissionPeriod, Where<ARSPCommissionPeriod.commnPeriodID, Equal<Required<ARSPCommissionPeriod.commnPeriodID>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) current.CommnPeriodID
        }));
      if (commissionPeriod != null)
      {
        ((PXSelectBase<ARSPCommissionPeriod>) instance.Filter).Current = commissionPeriod;
        throw new PXRedirectRequiredException((PXGraph) instance, "Review");
      }
    }
    return adapter.Get();
  }

  protected virtual IEnumerable toprocess()
  {
    PXCache cache = ((PXSelectBase) this.ToProcess).Cache;
    using (new PXReadBranchRestrictedScope())
    {
      List<ARSPCommissionProcess.ARSalesPerTranExt> arSalesPerTranExtList = new List<ARSPCommissionProcess.ARSalesPerTranExt>();
      Dictionary<Tuple<int, string>, ARSPCommissionProcess.ARSalesPerTranExt> dictionary1 = new Dictionary<Tuple<int, string>, ARSPCommissionProcess.ARSalesPerTranExt>();
      ARSPCommissionPeriod current = ((PXSelectBase<ARSPCommissionPeriod>) this.Filter).Current;
      if (current == null)
        return (IEnumerable) arSalesPerTranExtList;
      PXSelectBase<ARSPCommissionProcess.ARSalesPerTranExt> pxSelectBase;
      if (((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.SPCommnCalcType == "P")
      {
        pxSelectBase = (PXSelectBase<ARSPCommissionProcess.ARSalesPerTranExt>) new PXSelectJoinGroupBy<ARSPCommissionProcess.ARSalesPerTranExt, InnerJoin<SalesPerson, On<ARSPCommissionProcess.ARSalesPerTranExt.salespersonID, Equal<SalesPerson.salesPersonID>>, InnerJoin<ARRegister, On<ARRegister.docType, Equal<ARSPCommissionProcess.ARSalesPerTranExt.docType>, And<ARRegister.refNbr, Equal<ARSPCommissionProcess.ARSalesPerTranExt.refNbr>, And<ARSalesPerTran.released, Equal<BitOn>>>>>>, Where<ARSPCommissionProcess.ARSalesPerTranExt.commnPaymntPeriod, IsNull, And<ARRegister.docDate, Less<Required<ARRegister.docDate>>, And<ARSPCommissionProcess.ARSalesPerTranExt.adjdDocType, NotEqual<ARDocType.undefined>, And<ARSPCommissionProcess.ARSalesPerTranExt.docType, NotEqual<ARDocType.smallBalanceWO>, And<ARSPCommissionProcess.ARSalesPerTranExt.adjdRefNbr, NotEqual<EmptyString>>>>>>, Aggregate<Sum<ARSPCommissionProcess.ARSalesPerTranExt.curyCommnblAmt, Sum<ARSPCommissionProcess.ARSalesPerTranExt.curyCommnAmt, Sum<ARSPCommissionProcess.ARSalesPerTranExt.commnblAmt, Sum<ARSPCommissionProcess.ARSalesPerTranExt.commnAmt, Max<ARSPCommissionProcess.ARSalesPerTranExt.commnPct, Min<ARSPCommissionProcess.ARSalesPerTranExt.minCommnPct, GroupBy<ARSPCommissionProcess.ARSalesPerTranExt.salespersonID, GroupBy<ARSPCommissionProcess.ARSalesPerTranExt.baseCuryID, GroupBy<SalesPerson.isActive, Count>>>>>>>>>>>((PXGraph) this);
        foreach (PXResult<ARSalesPerTran, ARSPCommissionProcess.ARSalesPerTranExt, ARRegister> pxResult in ((PXSelectBase<ARSalesPerTran>) new PXSelectJoinGroupBy<ARSalesPerTran, InnerJoin<ARSPCommissionProcess.ARSalesPerTranExt, On<ARSalesPerTran.salespersonID, Equal<ARSPCommissionProcess.ARSalesPerTranExt.salespersonID>, And<ARSalesPerTran.adjdDocType, Equal<ARSPCommissionProcess.ARSalesPerTranExt.docType>, And<ARSalesPerTran.adjdRefNbr, Equal<ARSPCommissionProcess.ARSalesPerTranExt.refNbr>, And<ARSPCommissionProcess.ARSalesPerTranExt.commnPaymntPeriod, IsNotNull, And<ARSPCommissionProcess.ARSalesPerTranExt.actuallyUsed, Equal<BitOn>>>>>>, InnerJoin<ARRegister, On<ARRegister.docType, Equal<ARSalesPerTran.docType>, And<ARRegister.refNbr, Equal<ARSalesPerTran.refNbr>, And<ARSalesPerTran.released, Equal<BitOn>>>>>>, Where<ARSalesPerTran.commnPaymntPeriod, IsNull, And<ARRegister.docDate, Less<Required<ARRegister.docDate>>>>, Aggregate<Sum<ARSalesPerTran.curyCommnblAmt, Sum<ARSalesPerTran.curyCommnAmt, Sum<ARSalesPerTran.commnblAmt, Sum<ARSalesPerTran.commnAmt, Max<ARSalesPerTran.commnPct, GroupBy<ARSPCommissionProcess.ARSalesPerTranExt.salespersonID, GroupBy<ARSPCommissionProcess.ARSalesPerTranExt.baseCuryID, Count>>>>>>>>>((PXGraph) this)).Select(new object[1]
        {
          (object) current.EndDate
        }))
        {
          ARSalesPerTran arSalesPerTran = PXResult<ARSalesPerTran, ARSPCommissionProcess.ARSalesPerTranExt, ARRegister>.op_Implicit(pxResult);
          ARSPCommissionProcess.ARSalesPerTranExt arSalesPerTranExt = new ARSPCommissionProcess.ARSalesPerTranExt();
          PXCache<ARSalesPerTran>.RestoreCopy((ARSalesPerTran) arSalesPerTranExt, arSalesPerTran);
          arSalesPerTranExt.DocCount = ((PXResult) pxResult).RowCount;
          dictionary1[new Tuple<int, string>(arSalesPerTranExt.SalespersonID.Value, arSalesPerTranExt.BaseCuryID)] = arSalesPerTranExt;
        }
      }
      else
        pxSelectBase = (PXSelectBase<ARSPCommissionProcess.ARSalesPerTranExt>) new PXSelectJoinGroupBy<ARSPCommissionProcess.ARSalesPerTranExt, InnerJoin<SalesPerson, On<ARSPCommissionProcess.ARSalesPerTranExt.salespersonID, Equal<SalesPerson.salesPersonID>>, InnerJoin<ARRegister, On<ARRegister.docType, Equal<ARSPCommissionProcess.ARSalesPerTranExt.docType>, And<ARRegister.refNbr, Equal<ARSPCommissionProcess.ARSalesPerTranExt.refNbr>, And<ARSalesPerTran.released, Equal<BitOn>>>>>>, Where<ARSPCommissionProcess.ARSalesPerTranExt.commnPaymntPeriod, IsNull, And<ARRegister.docDate, Less<Required<ARRegister.docDate>>, And<Where<ARSPCommissionProcess.ARSalesPerTranExt.adjdDocType, Equal<ARDocType.undefined>, And<ARSPCommissionProcess.ARSalesPerTranExt.adjdRefNbr, Equal<EmptyString>>>>>>, Aggregate<Sum<ARSPCommissionProcess.ARSalesPerTranExt.curyCommnblAmt, Sum<ARSPCommissionProcess.ARSalesPerTranExt.curyCommnAmt, Sum<ARSPCommissionProcess.ARSalesPerTranExt.commnblAmt, Sum<ARSPCommissionProcess.ARSalesPerTranExt.commnAmt, Max<ARSPCommissionProcess.ARSalesPerTranExt.commnPct, Min<ARSPCommissionProcess.ARSalesPerTranExt.minCommnPct, GroupBy<ARSPCommissionProcess.ARSalesPerTranExt.salespersonID, GroupBy<ARSPCommissionProcess.ARSalesPerTranExt.baseCuryID, GroupBy<SalesPerson.isActive, Count>>>>>>>>>>>((PXGraph) this);
      foreach (PXResult<ARSPCommissionProcess.ARSalesPerTranExt, SalesPerson> pxResult in pxSelectBase.Select(new object[1]
      {
        (object) current.EndDate
      }))
      {
        ARSPCommissionProcess.ARSalesPerTranExt copy = (ARSPCommissionProcess.ARSalesPerTranExt) cache.CreateCopy((object) PXResult<ARSPCommissionProcess.ARSalesPerTranExt, SalesPerson>.op_Implicit(pxResult));
        copy.DocCount = ((PXResult) pxResult).RowCount;
        Dictionary<Tuple<int, string>, ARSPCommissionProcess.ARSalesPerTranExt> dictionary2 = dictionary1;
        int? salespersonId = copy.SalespersonID;
        Tuple<int, string> key1 = new Tuple<int, string>(salespersonId.Value, copy.BaseCuryID);
        if (dictionary2.ContainsKey(key1))
        {
          Dictionary<Tuple<int, string>, ARSPCommissionProcess.ARSalesPerTranExt> dictionary3 = dictionary1;
          salespersonId = copy.SalespersonID;
          Tuple<int, string> key2 = new Tuple<int, string>(salespersonId.Value, copy.BaseCuryID);
          ARSPCommissionProcess.ARSalesPerTranExt aOp2 = dictionary3[key2];
          ARSPCommissionProcess.Substract(copy, aOp2);
        }
        arSalesPerTranExtList.Add(copy);
      }
      return (IEnumerable) arSalesPerTranExtList;
    }
  }

  protected virtual IEnumerable filter()
  {
    ARSPCommissionProcess graph = this;
    PXCache cache = ((PXSelectBase) graph.Filter).Cache;
    if (PXLongOperation.GetCustomInfo(((PXGraph) graph).UID) is ARSPCommissionPeriod customInfo)
    {
      yield return (object) customInfo;
    }
    else
    {
      using (IEnumerator<PXResult<ARSPCommissionPeriod>> enumerator = ((PXSelectBase<ARSPCommissionPeriod>) graph.ARSPCommnPeriod_Open).Select(Array.Empty<object>()).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          yield return (object) PXResult<ARSPCommissionPeriod>.op_Implicit(enumerator.Current);
          yield break;
        }
      }
      ARSPCommissionPeriod commissionPeriod1 = PXResultset<ARSPCommissionPeriod>.op_Implicit(((PXSelectBase<ARSPCommissionPeriod>) graph.CommnPeriod_Last).Select(Array.Empty<object>()));
      if (commissionPeriod1 != null)
      {
        ARSPCommissionPeriod commissionPeriod2 = SPCommissionCalendar.Create<ARSPCommissionPeriod>((PXGraph) graph, (PXSelectBase<ARSPCommissionYear>) graph.ARSPCommnYear_Current, (PXSelectBase<ARSPCommissionPeriod>) graph.ARSPCommnPeriod_Current, ((PXSelectBase<PX.Objects.AR.ARSetup>) graph.ARSetup).Current, commissionPeriod1.EndDate);
        ((PXGraph) graph).Caches[typeof (ARSPCommissionPeriod)].IsDirty = false;
        ((PXGraph) graph).Caches[typeof (ARSPCommissionYear)].IsDirty = false;
        if (commissionPeriod2 != null)
          yield return (object) commissionPeriod2;
      }
      else
      {
        PXResult<ARSalesPerTran, ARRegister> pxResult = (PXResult<ARSalesPerTran, ARRegister>) null;
        using (new PXReadBranchRestrictedScope())
          pxResult = (PXResult<ARSalesPerTran, ARRegister>) ((PXSelectBase) new PXSelectJoinOrderBy<ARSalesPerTran, InnerJoin<ARRegister, On<ARSalesPerTran.docType, Equal<ARRegister.docType>, And<ARSalesPerTran.refNbr, Equal<ARRegister.refNbr>>>>, OrderBy<Asc<ARRegister.docDate>>>((PXGraph) graph)).View.SelectSingle(Array.Empty<object>());
        if (pxResult != null)
        {
          ARSPCommissionPeriod commissionPeriod3 = SPCommissionCalendar.Create<ARSPCommissionPeriod>((PXGraph) graph, (PXSelectBase<ARSPCommissionYear>) graph.ARSPCommnYear_Current, (PXSelectBase<ARSPCommissionPeriod>) graph.ARSPCommnPeriod_Current, ((PXSelectBase<PX.Objects.AR.ARSetup>) graph.ARSetup).Current, PXResult<ARSalesPerTran, ARRegister>.op_Implicit(pxResult).DocDate);
          ((PXGraph) graph).Caches[typeof (ARSPCommissionPeriod)].IsDirty = false;
          ((PXGraph) graph).Caches[typeof (ARSPCommissionYear)].IsDirty = false;
          if (commissionPeriod3 != null)
            yield return (object) commissionPeriod3;
        }
      }
    }
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable processAll(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARSPCommissionProcess.\u003C\u003Ec__DisplayClass18_0 cDisplayClass180 = new ARSPCommissionProcess.\u003C\u003Ec__DisplayClass18_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass180.\u003C\u003E4__this = this;
    PXCache cach = ((PXGraph) this).Caches[typeof (ARSPCommissionProcess.ARSalesPerTranExt)];
    if (PXLongOperation.Exists(((PXGraph) this).UID))
      throw new ApplicationException("The previous operation has not been completed.");
    // ISSUE: reference to a compiler-generated field
    cDisplayClass180.processPeriod = ((PXSelectBase<ARSPCommissionPeriod>) this.Filter).Current;
    // ISSUE: reference to a compiler-generated field
    if (PXResultset<ARSPCommissionPeriod>.op_Implicit(PXSelectBase<ARSPCommissionPeriod, PXSelect<ARSPCommissionPeriod, Where<ARSPCommissionPeriod.commnPeriodID, Less<Required<ARSPCommissionPeriod.commnPeriodID>>, And<ARSPCommissionPeriod.status, NotEqual<ARSPCommissionPeriodStatus.closed>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) cDisplayClass180.processPeriod.CommnPeriodID
    })) != null)
      throw new ApplicationException("This Commission Period cannot be processed - all the previous commission periods must be closed first");
    DateTime? businessDate = ((PXGraph) this).Accessinfo.BusinessDate;
    // ISSUE: reference to a compiler-generated field
    DateTime? nullable1 = cDisplayClass180.processPeriod.StartDate;
    if ((businessDate.HasValue & nullable1.HasValue ? (businessDate.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new ApplicationException("Processing date is less than the start date for the selected commission period.");
    nullable1 = ((PXGraph) this).Accessinfo.BusinessDate;
    // ISSUE: reference to a compiler-generated field
    DateTime? nullable2 = cDisplayClass180.processPeriod.StartDate;
    if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      nullable2 = ((PXGraph) this).Accessinfo.BusinessDate;
      // ISSUE: reference to a compiler-generated field
      nullable1 = cDisplayClass180.processPeriod.EndDate;
      if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0 && ((PXSelectBase<ARSPCommissionPeriod>) this.Filter).Ask("Attention!", "If new documents for this period arrive, you will need to repeat the process of calculating commission or they will be included into the next commission period. Do you want to continue?", (MessageButtons) 1) != 1)
        return adapter.Get();
    }
    // ISSUE: reference to a compiler-generated field
    cDisplayClass180.list = new List<ARSPCommissionProcess.ARSalesPerTranExt>();
    foreach (ARSPCommissionProcess.ARSalesPerTranExt arSalesPerTranExt in this.getAll())
    {
      cach.Update((object) arSalesPerTranExt);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass180.list.Add(arSalesPerTranExt);
    }
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass180, __methodptr(\u003CprocessAll\u003Eb__0)));
    return adapter.Get();
  }

  private static void ProcessSPCommissions(
    List<ARSPCommissionProcess.ARSalesPerTranExt> aProcessList,
    PX.Objects.AR.ARSetup settings,
    ARSPCommissionPeriod aProcessPeriod,
    List<ARSPCommissionPeriod> aPeriods,
    List<ARSPCommissionYear> aYears)
  {
    string[] strArray = new string[aProcessList.Count > 0 ? aProcessList.Count : 1];
    bool flag = false;
    PXLongOperation.SetCustomInfo((object) strArray);
    ARSPCommissionUpdate instance = PXGraph.CreateInstance<ARSPCommissionUpdate>();
    PXDBDefaultAttribute.SetDefaultForUpdate<ARSalesPerTran.curyInfoID>(((PXSelectBase) instance.SPTrans).Cache, (object) null, false);
    bool aByPayment = settings.SPCommnCalcType == "P";
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      instance.InsertCommissionPeriods(aYears, aPeriods, aProcessPeriod);
      for (int index = 0; index < aProcessList.Count; ++index)
      {
        try
        {
          instance.ProcessSPCommission(aProcessList[index].SalespersonID, aProcessPeriod, aByPayment);
        }
        catch (Exception ex)
        {
          flag = true;
          strArray[index] = ex.Message;
        }
      }
      if (flag)
        throw new ApplicationException("Commission calculation process failed with one or more error.");
      instance.MarkPeriodAsPrepared(aProcessPeriod, true);
      ((PXGraph) instance).Actions.PressSave();
      strArray[0] = aProcessPeriod.CommnPeriodID;
      transactionScope.Complete();
    }
    PXLongOperation.SetCustomInfo((object) aProcessPeriod);
  }

  private List<ARSPCommissionProcess.ARSalesPerTranExt> getAll()
  {
    List<ARSPCommissionProcess.ARSalesPerTranExt> all = new List<ARSPCommissionProcess.ARSalesPerTranExt>();
    foreach (PXResult<ARSPCommissionProcess.ARSalesPerTranExt> pxResult in ((PXSelectBase<ARSPCommissionProcess.ARSalesPerTranExt>) this.ToProcess).Select(Array.Empty<object>()))
    {
      ARSPCommissionProcess.ARSalesPerTranExt arSalesPerTranExt = PXResult<ARSPCommissionProcess.ARSalesPerTranExt>.op_Implicit(pxResult);
      all.Add(arSalesPerTranExt);
    }
    return all;
  }

  private List<ARSPCommissionPeriod> getPeriodsToInsert()
  {
    List<ARSPCommissionPeriod> periodsToInsert = new List<ARSPCommissionPeriod>();
    foreach (ARSPCommissionPeriod commissionPeriod in ((PXSelectBase) this.ARSPCommnPeriod_Current).Cache.Inserted)
      periodsToInsert.Add(commissionPeriod);
    return periodsToInsert;
  }

  private List<ARSPCommissionYear> getYearsToInsert()
  {
    List<ARSPCommissionYear> yearsToInsert = new List<ARSPCommissionYear>();
    foreach (ARSPCommissionYear arspCommissionYear in ((PXSelectBase) this.ARSPCommnYear_Current).Cache.Inserted)
      yearsToInsert.Add(arspCommissionYear);
    return yearsToInsert;
  }

  private static void Substract(
    ARSPCommissionProcess.ARSalesPerTranExt aRes,
    ARSPCommissionProcess.ARSalesPerTranExt aOp2)
  {
    ARSPCommissionProcess.ARSalesPerTranExt arSalesPerTranExt1 = aRes;
    Decimal? nullable1 = arSalesPerTranExt1.CommnblAmt;
    Decimal? commnblAmt = aOp2.CommnblAmt;
    arSalesPerTranExt1.CommnblAmt = nullable1.HasValue & commnblAmt.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - commnblAmt.GetValueOrDefault()) : new Decimal?();
    ARSPCommissionProcess.ARSalesPerTranExt arSalesPerTranExt2 = aRes;
    Decimal? nullable2 = arSalesPerTranExt2.CommnAmt;
    nullable1 = aOp2.CommnAmt;
    arSalesPerTranExt2.CommnAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    ARSPCommissionProcess.ARSalesPerTranExt arSalesPerTranExt3 = aRes;
    nullable1 = arSalesPerTranExt3.CuryCommnblAmt;
    nullable2 = aOp2.CuryCommnblAmt;
    arSalesPerTranExt3.CuryCommnblAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    ARSPCommissionProcess.ARSalesPerTranExt arSalesPerTranExt4 = aRes;
    nullable2 = arSalesPerTranExt4.CuryCommnAmt;
    nullable1 = aOp2.CuryCommnAmt;
    arSalesPerTranExt4.CuryCommnAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    ARSPCommissionProcess.ARSalesPerTranExt arSalesPerTranExt5 = aRes;
    int? docCount1 = arSalesPerTranExt5.DocCount;
    int? docCount2 = aOp2.DocCount;
    arSalesPerTranExt5.DocCount = docCount1.HasValue & docCount2.HasValue ? new int?(docCount1.GetValueOrDefault() - docCount2.GetValueOrDefault()) : new int?();
  }

  [Serializable]
  public class ARSalesPerTranExt : ARSalesPerTran
  {
    protected Decimal? _MinCommnPct;
    protected int? _DocCount;

    [SalesPerson(Enabled = false, IsKey = true, DescriptionField = typeof (SalesPerson.descr))]
    public override int? SalespersonID
    {
      get => this._SalespersonID;
      set => this._SalespersonID = value;
    }

    [PXDecimal(6)]
    [PXUIField]
    public virtual Decimal? AveCommnPct
    {
      [PXDependsOnFields(new Type[] {typeof (ARSPCommissionProcess.ARSalesPerTranExt.commnblAmt), typeof (ARSPCommissionProcess.ARSalesPerTranExt.curyCommnAmt), typeof (ARSPCommissionProcess.ARSalesPerTranExt.curyCommnblAmt)})] get
      {
        return !(this._CommnblAmt.GetValueOrDefault() != 0M) ? new Decimal?() : new Decimal?(Math.Round(this._CuryCommnAmt.Value / this._CuryCommnblAmt.Value * 100.0M, 3));
      }
    }

    [PXDBDecimal(6)]
    [PXUIField]
    public override Decimal? CommnPct
    {
      get => this._CommnPct;
      set => this._CommnPct = value;
    }

    [PXDBDecimal(6, BqlField = typeof (ARSalesPerTran.commnPct))]
    [PXUIField]
    public virtual Decimal? MinCommnPct
    {
      get => this._MinCommnPct;
      set => this._MinCommnPct = value;
    }

    [PXInt]
    [PXUIField(DisplayName = "Documents", Visible = true)]
    public virtual int? DocCount
    {
      get => this._DocCount;
      set => this._DocCount = value;
    }

    [PXDBString(3, IsFixed = true)]
    public override string DocType
    {
      get => this._DocType;
      set => this._DocType = value;
    }

    [PXDBString(15, IsUnicode = true)]
    public override string RefNbr
    {
      get => this._RefNbr;
      set => this._RefNbr = value;
    }

    [PXDBInt]
    public override int? AdjNbr
    {
      get => this._AdjNbr;
      set => this._AdjNbr = value;
    }

    [PXDBString(3, IsFixed = true)]
    public override string AdjdDocType
    {
      get => this._AdjdDocType;
      set => this._AdjdDocType = value;
    }

    [PXDBString(15, IsUnicode = true)]
    public override string AdjdRefNbr
    {
      get => this._AdjdRefNbr;
      set => this._AdjdRefNbr = value;
    }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Commission Amount", Enabled = false)]
    public override Decimal? CommnAmt
    {
      get => this._CommnAmt;
      set => this._CommnAmt = value;
    }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Commissionable Amount", Enabled = false)]
    public override Decimal? CommnblAmt
    {
      get => this._CommnblAmt;
      set => this._CommnblAmt = value;
    }

    [PXDBString(5, IsUnicode = true)]
    [PXUIField(DisplayName = "Currency")]
    [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.multipleBaseCurrencies>))]
    public override string BaseCuryID { get; set; }

    public new abstract class salespersonID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARSPCommissionProcess.ARSalesPerTranExt.salespersonID>
    {
    }

    public abstract class aveCommnPct : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARSPCommissionProcess.ARSalesPerTranExt.aveCommnPct>
    {
    }

    public new abstract class commnPct : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARSPCommissionProcess.ARSalesPerTranExt.commnPct>
    {
    }

    public abstract class minCommnPct : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARSPCommissionProcess.ARSalesPerTranExt.minCommnPct>
    {
    }

    public abstract class docCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARSPCommissionProcess.ARSalesPerTranExt.docCount>
    {
    }

    public new abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARSPCommissionProcess.ARSalesPerTranExt.docType>
    {
    }

    public new abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARSPCommissionProcess.ARSalesPerTranExt.refNbr>
    {
    }

    public new abstract class adjNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARSPCommissionProcess.ARSalesPerTranExt.adjNbr>
    {
    }

    public new abstract class adjdDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARSPCommissionProcess.ARSalesPerTranExt.adjdDocType>
    {
    }

    public new abstract class adjdRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARSPCommissionProcess.ARSalesPerTranExt.adjdRefNbr>
    {
    }

    public new abstract class commnAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARSPCommissionProcess.ARSalesPerTranExt.commnAmt>
    {
    }

    public new abstract class curyCommnAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARSPCommissionProcess.ARSalesPerTranExt.curyCommnAmt>
    {
    }

    public new abstract class commnblAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARSPCommissionProcess.ARSalesPerTranExt.commnblAmt>
    {
    }

    public new abstract class curyCommnblAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARSPCommissionProcess.ARSalesPerTranExt.curyCommnblAmt>
    {
    }

    public new abstract class actuallyUsed : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARSPCommissionProcess.ARSalesPerTranExt.actuallyUsed>
    {
    }

    public new abstract class commnPaymntPeriod : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARSPCommissionProcess.ARSalesPerTranExt.commnPaymntPeriod>
    {
    }

    public new abstract class baseCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARSPCommissionProcess.ARSalesPerTranExt.baseCuryID>
    {
    }
  }

  public delegate void DoProcess(
    List<ARSPCommissionProcess.ARSalesPerTranExt> aToProcess,
    PX.Objects.AR.ARSetup settings,
    ARSPCommissionPeriod aCommnPeriod,
    List<ARSPCommissionPeriod> aPeriods,
    List<ARSPCommissionYear> aYears);
}

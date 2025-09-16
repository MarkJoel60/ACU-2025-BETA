// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSPCommissionUpdate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.BQLConstants;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

[PXHidden]
public class ARSPCommissionUpdate : PXGraph<ARSPCommissionUpdate>
{
  public PXSelect<ARSPCommissionYear, Where<ARSPCommissionYear.year, Equal<Required<ARSPCommissionYear.year>>>> CommnYear;
  public PXSelect<ARSPCommissionPeriod, Where<ARSPCommissionPeriod.commnPeriodID, Equal<Required<ARSPCommissionPeriod.commnPeriodID>>>> CommnPeriod;
  public PXSelectJoin<ARSalesPerTran, InnerJoin<ARRegister, On<ARSalesPerTran.docType, Equal<ARRegister.docType>, And<ARSalesPerTran.refNbr, Equal<ARRegister.refNbr>, And<ARSalesPerTran.released, Equal<BitOn>, And<ARRegister.docDate, Less<Required<ARRegister.docDate>>>>>>>, Where<ARSalesPerTran.salespersonID, Equal<Required<ARSalesPerTran.salespersonID>>, And<ARSalesPerTran.commnPaymntPeriod, IsNull>>> SPTrans;
  public PXSelect<ARSPCommnHistory, Where<ARSPCommnHistory.salesPersonID, Equal<Required<ARSPCommnHistory.salesPersonID>>, And<ARSPCommnHistory.branchID, Equal<Required<ARSPCommnHistory.branchID>>, And<ARSPCommnHistory.customerID, Equal<Required<ARSPCommnHistory.customerID>>, And<ARSPCommnHistory.customerLocationID, Equal<Required<ARSPCommnHistory.customerLocationID>>, And<ARSPCommnHistory.commnPeriod, Equal<Required<ARSPCommnHistory.commnPeriod>>>>>>>> SPHistory;

  public virtual void ProcessSPCommission(
    int? aSalesPersonID,
    ARSPCommissionPeriod aPeriod,
    bool aByPayment)
  {
    Dictionary<ARSPCommissionUpdate.SPHistoryKey, ARSPCommnHistory> dictionary = new Dictionary<ARSPCommissionUpdate.SPHistoryKey, ARSPCommnHistory>();
    foreach (PXResult<ARSalesPerTran, ARRegister> pxResult in ((PXSelectBase<ARSalesPerTran>) this.SPTrans).Select(new object[2]
    {
      (object) aPeriod.EndDate,
      (object) aSalesPersonID
    }))
    {
      ARSalesPerTran aSource1 = PXResult<ARSalesPerTran, ARRegister>.op_Implicit(pxResult);
      ARRegister aSource2 = PXResult<ARSalesPerTran, ARRegister>.op_Implicit(pxResult);
      bool flag;
      if (aByPayment)
      {
        flag = aSource1.DocType != "SMB" && aSource1.AdjdDocType != "UND" && !string.IsNullOrEmpty(aSource1.AdjdRefNbr);
        if (flag)
        {
          if (PXResultset<ARSalesPerTran>.op_Implicit(PXSelectBase<ARSalesPerTran, PXSelect<ARSalesPerTran, Where<ARSalesPerTran.docType, Equal<Required<ARSalesPerTran.docType>>, And<ARSalesPerTran.refNbr, Equal<Required<ARSalesPerTran.refNbr>>, And<ARSalesPerTran.salespersonID, Equal<Required<ARSalesPerTran.salespersonID>>, And<ARSalesPerTran.adjdDocType, Equal<ARDocType.undefined>, And<ARSalesPerTran.commnPaymntPeriod, IsNotNull, And<ARSalesPerTran.actuallyUsed, Equal<BitOn>>>>>>>>.Config>.Select((PXGraph) this, new object[3]
          {
            (object) aSource1.AdjdDocType,
            (object) aSource1.AdjdRefNbr,
            (object) aSource1.SalespersonID
          })) != null)
            flag = false;
        }
      }
      else
        flag = aSource1.AdjdDocType == "UND" && string.IsNullOrEmpty(aSource1.AdjdRefNbr);
      aSource1.ActuallyUsed = new bool?(flag);
      aSource1.CommnPaymntPeriod = aPeriod.CommnPeriodID;
      aSource1.CommnPaymntDate = ((PXGraph) this).Accessinfo.BusinessDate;
      if (flag)
      {
        int? nullable = aSource1.BranchID;
        int aA1 = nullable.Value;
        nullable = aSource2.CustomerID;
        int aA2 = nullable.Value;
        nullable = aSource2.CustomerLocationID;
        int aA3 = nullable.Value;
        ARSPCommissionUpdate.SPHistoryKey key = new ARSPCommissionUpdate.SPHistoryKey(aA1, aA2, aA3);
        if (dictionary.ContainsKey(key))
        {
          ARSPCommissionUpdate.Aggregate(dictionary[key], aSource1);
        }
        else
        {
          ARSPCommnHistory aDest = new ARSPCommnHistory();
          ARSPCommissionUpdate.Copy(aDest, aSource1);
          ARSPCommissionUpdate.Copy(aDest, aSource2);
          dictionary[key] = aDest;
        }
      }
      ((PXSelectBase<ARSalesPerTran>) this.SPTrans).Update(aSource1);
    }
    foreach (ARSPCommnHistory arspCommnHistory1 in dictionary.Values)
    {
      ARSPCommnHistory arspCommnHistory2 = PXResultset<ARSPCommnHistory>.op_Implicit(((PXSelectBase<ARSPCommnHistory>) this.SPHistory).Select(new object[5]
      {
        (object) arspCommnHistory1.SalesPersonID,
        (object) arspCommnHistory1.BranchID,
        (object) arspCommnHistory1.CustomerID,
        (object) arspCommnHistory1.CustomerLocationID,
        (object) arspCommnHistory1.CommnPeriod
      }));
      if (arspCommnHistory2 != null)
      {
        ARSPCommnHistory arspCommnHistory3 = arspCommnHistory2;
        Decimal? nullable1 = arspCommnHistory3.CommnAmt;
        Decimal? nullable2 = arspCommnHistory1.CommnAmt;
        arspCommnHistory3.CommnAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        ARSPCommnHistory arspCommnHistory4 = arspCommnHistory2;
        nullable2 = arspCommnHistory4.CommnblAmt;
        nullable1 = arspCommnHistory1.CommnblAmt;
        arspCommnHistory4.CommnblAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        ((PXSelectBase<ARSPCommnHistory>) this.SPHistory).Update(arspCommnHistory2);
      }
      else
        ((PXSelectBase<ARSPCommnHistory>) this.SPHistory).Insert(arspCommnHistory1);
    }
  }

  public virtual void InsertCommissionPeriods(
    List<ARSPCommissionYear> aYears,
    List<ARSPCommissionPeriod> aPeriods,
    ARSPCommissionPeriod aCurrent)
  {
    foreach (ARSPCommissionYear aYear in aYears)
    {
      if (PXResultset<ARSPCommissionYear>.op_Implicit(((PXSelectBase<ARSPCommissionYear>) this.CommnYear).Select(new object[1]
      {
        (object) aYear.Year
      })) == null)
        ((PXSelectBase<ARSPCommissionYear>) this.CommnYear).Insert(aYear);
    }
    DateTime? endDate1;
    DateTime? endDate2;
    foreach (ARSPCommissionPeriod aPeriod in aPeriods)
    {
      if (PXResultset<ARSPCommissionPeriod>.op_Implicit(((PXSelectBase<ARSPCommissionPeriod>) this.CommnPeriod).Select(new object[1]
      {
        (object) aPeriod.CommnPeriodID
      })) == null)
      {
        endDate1 = aPeriod.EndDate;
        endDate2 = aCurrent.EndDate;
        if ((endDate1.HasValue & endDate2.HasValue ? (endDate1.GetValueOrDefault() < endDate2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          aPeriod.Status = "P";
        ((PXSelectBase<ARSPCommissionPeriod>) this.CommnPeriod).Insert(aPeriod);
      }
      else
      {
        endDate2 = aPeriod.EndDate;
        endDate1 = aCurrent.EndDate;
        if ((endDate2.HasValue & endDate1.HasValue ? (endDate2.GetValueOrDefault() < endDate1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          this.MarkPeriodAsPrepared(aPeriod, false);
      }
    }
  }

  public virtual void MarkPeriodAsPrepared(ARSPCommissionPeriod aCurrent, bool doSelect)
  {
    int num = doSelect ? 1 : 0;
    aCurrent.Status = "P";
    aCurrent = ((PXSelectBase<ARSPCommissionPeriod>) this.CommnPeriod).Update(aCurrent);
  }

  public virtual void VoidReportProc(ARSPCommissionPeriod aCommnPeriod)
  {
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        PXDatabase.Update<ARSalesPerTran>(new PXDataFieldParam[4]
        {
          (PXDataFieldParam) new PXDataFieldAssign("CommnPaymntPeriod", (object) null),
          (PXDataFieldParam) new PXDataFieldAssign("CommnPaymntDate", (object) null),
          (PXDataFieldParam) new PXDataFieldAssign("ActuallyUsed", (object) false),
          (PXDataFieldParam) new PXDataFieldRestrict("CommnPaymntPeriod", (PXDbType) 3, new int?(6), (object) aCommnPeriod.CommnPeriodID, (PXComp) 0)
        });
        PXDatabase.Delete<ARSPCommnHistory>(new PXDataFieldRestrict[1]
        {
          new PXDataFieldRestrict("CommnPeriod", (PXDbType) 3, new int?(6), (object) aCommnPeriod.CommnPeriodID, (PXComp) 0)
        });
        foreach (PXResult<ARSPCommissionPeriod> pxResult in ((PXSelectBase<ARSPCommissionPeriod>) this.CommnPeriod).Select(new object[1]
        {
          (object) aCommnPeriod.CommnPeriodID
        }))
        {
          ARSPCommissionPeriod commissionPeriod = PXResult<ARSPCommissionPeriod>.op_Implicit(pxResult);
          commissionPeriod.Status = !(commissionPeriod.Status != "P") ? "N" : throw new PXException();
          ((PXSelectBase) this.CommnPeriod).Cache.Update((object) commissionPeriod);
        }
        ((PXSelectBase) this.CommnPeriod).Cache.Persist((PXDBOperation) 1);
        ((PXSelectBase) this.CommnPeriod).Cache.Persisted(false);
        transactionScope.Complete();
      }
    }
  }

  public virtual void ClosePeriodProc(ARSPCommissionPeriod aCurrent)
  {
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        foreach (PXResult<ARSPCommissionPeriod> pxResult in ((PXSelectBase<ARSPCommissionPeriod>) this.CommnPeriod).Select(new object[1]
        {
          (object) aCurrent.CommnPeriodID
        }))
        {
          ARSPCommissionPeriod commissionPeriod = PXResult<ARSPCommissionPeriod>.op_Implicit(pxResult);
          commissionPeriod.Status = !(commissionPeriod.Status != "P") ? "C" : throw new PXException();
          ((PXSelectBase) this.CommnPeriod).Cache.Update((object) commissionPeriod);
        }
        ((PXSelectBase) this.CommnPeriod).Cache.Persist((PXDBOperation) 1);
        ((PXSelectBase) this.CommnPeriod).Cache.Persisted(false);
        aCurrent.Status = "C";
        transactionScope.Complete();
      }
    }
  }

  public virtual void ReopenPeriodProc(ARSPCommissionPeriod aCurrent)
  {
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        foreach (PXResult<ARSPCommissionPeriod> pxResult in ((PXSelectBase<ARSPCommissionPeriod>) this.CommnPeriod).Select(new object[1]
        {
          (object) aCurrent.CommnPeriodID
        }))
        {
          ARSPCommissionPeriod commissionPeriod = PXResult<ARSPCommissionPeriod>.op_Implicit(pxResult);
          commissionPeriod.Status = !(commissionPeriod.Status != "C") ? "P" : throw new PXException("The commission period is not closed.");
          if (PXResultset<ARSPCommnHistory>.op_Implicit(PXSelectBase<ARSPCommnHistory, PXSelectReadonly<ARSPCommnHistory, Where<ARSPCommnHistory.commnPeriod, Equal<Required<ARSPCommnHistory.commnPeriod>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) commissionPeriod.CommnPeriodID
          })) == null)
          {
            if (PXResultset<ARSalesPerTran>.op_Implicit(PXSelectBase<ARSalesPerTran, PXSelectReadonly<ARSalesPerTran, Where<ARSalesPerTran.commnPaymntPeriod, Equal<Required<ARSalesPerTran.commnPaymntPeriod>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) commissionPeriod.CommnPeriodID
            })) == null)
              commissionPeriod.Status = "N";
          }
          ((PXSelectBase) this.CommnPeriod).Cache.Update((object) commissionPeriod);
        }
        ((PXSelectBase) this.CommnPeriod).Cache.Persist((PXDBOperation) 1);
        ((PXSelectBase) this.CommnPeriod).Cache.Persisted(false);
        aCurrent.Status = "P";
        transactionScope.Complete();
      }
    }
  }

  public static void Copy(ARSPCommnHistory aDest, ARSalesPerTran aSource)
  {
    aDest.BranchID = aSource.BranchID;
    aDest.SalesPersonID = aSource.SalespersonID;
    aDest.CommnAmt = new Decimal?(aSource.CommnAmt.GetValueOrDefault());
    aDest.CommnblAmt = new Decimal?(aSource.CommnblAmt.GetValueOrDefault());
    aDest.CommnPeriod = aSource.CommnPaymntPeriod;
  }

  public static void Copy(ARSPCommnHistory aDest, ARRegister aSource)
  {
    aDest.CustomerID = aSource.CustomerID;
    aDest.CustomerLocationID = aSource.CustomerLocationID;
  }

  public static void Aggregate(ARSPCommnHistory aDest, ARSalesPerTran aSource)
  {
    ARSPCommnHistory arspCommnHistory1 = aDest;
    Decimal? nullable = arspCommnHistory1.CommnAmt;
    Decimal valueOrDefault1 = aSource.CommnAmt.GetValueOrDefault();
    arspCommnHistory1.CommnAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
    ARSPCommnHistory arspCommnHistory2 = aDest;
    nullable = arspCommnHistory2.CommnblAmt;
    Decimal valueOrDefault2 = aSource.CommnblAmt.GetValueOrDefault();
    arspCommnHistory2.CommnblAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
  }

  public class SPHistoryKey(int aA1, int aA2, int aA3) : Triplet<int, int, int>(aA1, aA2, aA3)
  {
    public virtual int GetHashCode(ARSPCommissionUpdate.SPHistoryKey obj)
    {
      return obj.first + 13 * (obj.second + 67 * obj.third);
    }
  }

  public class KeyComparer : IEqualityComparer<ARSPCommissionUpdate.SPHistoryKey>
  {
    public virtual bool Equals(
      ARSPCommissionUpdate.SPHistoryKey x,
      ARSPCommissionUpdate.SPHistoryKey y)
    {
      return x.CompareTo((Triplet<int, int, int>) y) == 0;
    }

    public virtual int GetHashCode(ARSPCommissionUpdate.SPHistoryKey obj)
    {
      return obj.first + 13 * (obj.second + 67 * obj.third);
    }
  }
}

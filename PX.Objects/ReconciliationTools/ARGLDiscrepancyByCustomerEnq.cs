// Decompiled with JetBrains decompiler
// Type: ReconciliationTools.ARGLDiscrepancyByCustomerEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace ReconciliationTools;

[TableAndChartDashboardType]
public class ARGLDiscrepancyByCustomerEnq : 
  ARGLDiscrepancyEnqGraphBase<ARGLDiscrepancyByAccountEnq, ARGLDiscrepancyByCustomerEnqFilter, ARGLDiscrepancyByCustomerEnqResult>
{
  public PXAction<ARGLDiscrepancyByCustomerEnqFilter> viewDetails;

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Financial Period")]
  protected virtual void ARGLDiscrepancyByCustomerEnqFilter_PeriodFrom_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXDefault]
  protected virtual void ARGLDiscrepancyByCustomerEnqFilter_AccountID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXDefault]
  protected virtual void ARGLDiscrepancyByCustomerEnqFilter_SubCD_CacheAttached(PXCache sender)
  {
  }

  protected override List<ARGLDiscrepancyByCustomerEnqResult> SelectDetails()
  {
    List<ARGLDiscrepancyByCustomerEnqResult> customerEnqResultList = new List<ARGLDiscrepancyByCustomerEnqResult>();
    ARGLDiscrepancyByCustomerEnqFilter header = this.Filter.Current;
    if (header != null)
    {
      int? nullable1 = header.BranchID;
      if (nullable1.HasValue && header.PeriodFrom != null)
      {
        nullable1 = header.AccountID;
        if (nullable1.HasValue && header.SubCD != null)
        {
          ARCustomerBalanceEnq instance1 = PXGraph.CreateInstance<ARCustomerBalanceEnq>();
          ARCustomerBalanceEnq.ARHistoryFilter copy1 = PXCache<ARCustomerBalanceEnq.ARHistoryFilter>.CreateCopy(instance1.Filter.Current);
          copy1.BranchID = header.BranchID;
          copy1.CustomerID = header.CustomerID;
          copy1.Period = header.PeriodFrom;
          copy1.ARAcctID = header.AccountID;
          copy1.SubCD = header.SubCD;
          copy1.IncludeChildAccounts = new bool?(false);
          copy1.ShowWithBalanceOnly = new bool?(false);
          copy1.SplitByCurrency = new bool?(false);
          instance1.Filter.Update(copy1);
          Dictionary<int, ARGLDiscrepancyByCustomerEnqResult> dictionary = new Dictionary<int, ARGLDiscrepancyByCustomerEnqResult>();
          foreach (PXResult<ARCustomerBalanceEnq.ARHistoryResult> pxResult in instance1.History.Select())
          {
            ARCustomerBalanceEnq.ARHistoryResult copy2 = (ARCustomerBalanceEnq.ARHistoryResult) pxResult;
            nullable1 = copy2.CustomerID;
            int key = nullable1.Value;
            Decimal? nullable2 = copy2.Balance;
            Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
            nullable2 = copy2.Deposits;
            Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
            Decimal num1 = valueOrDefault1 - valueOrDefault2;
            ARGLDiscrepancyByCustomerEnqResult customerEnqResult1;
            if (dictionary.TryGetValue(key, out customerEnqResult1))
            {
              ARGLDiscrepancyByCustomerEnqResult customerEnqResult2 = customerEnqResult1;
              nullable2 = customerEnqResult2.XXTurnover;
              Decimal num2 = num1;
              customerEnqResult2.XXTurnover = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num2) : new Decimal?();
            }
            else
            {
              customerEnqResult1 = new ARGLDiscrepancyByCustomerEnqResult();
              customerEnqResult1.GLTurnover = new Decimal?(0M);
              customerEnqResult1.XXTurnover = new Decimal?(num1);
              PXCache<ARCustomerBalanceEnq.ARHistoryResult>.RestoreCopy((ARCustomerBalanceEnq.ARHistoryResult) customerEnqResult1, copy2);
              dictionary.Add(key, customerEnqResult1);
            }
          }
          AccountByPeriodEnq instance2 = PXGraph.CreateInstance<AccountByPeriodEnq>();
          AccountByPeriodFilter copy3 = PXCache<AccountByPeriodFilter>.CreateCopy(instance2.Filter.Current);
          instance2.Filter.Cache.SetDefaultExt<AccountByPeriodFilter.ledgerID>((object) copy3);
          copy3.BranchID = header.BranchID;
          copy3.StartPeriodID = header.PeriodFrom;
          copy3.EndPeriodID = header.PeriodFrom;
          copy3.AccountID = header.AccountID;
          copy3.SubID = header.SubCD;
          instance2.Filter.Update(copy3);
          foreach (GLTranR tran in instance2.GLTranEnq.Select().RowCast<GLTranR>().Where<GLTranR>((Func<GLTranR, bool>) (row =>
          {
            if (!(row.Module == "AR") || !row.ReferenceID.HasValue)
              return false;
            int? nullable3 = header.CustomerID;
            if (!nullable3.HasValue)
              return true;
            nullable3 = row.ReferenceID;
            int? customerId = header.CustomerID;
            return nullable3.GetValueOrDefault() == customerId.GetValueOrDefault() & nullable3.HasValue == customerId.HasValue;
          })))
          {
            int key = tran.ReferenceID.Value;
            Decimal num3 = this.CalcGLTurnover((PX.Objects.GL.GLTran) tran);
            ARGLDiscrepancyByCustomerEnqResult customerEnqResult3;
            if (dictionary.TryGetValue(key, out customerEnqResult3))
            {
              ARGLDiscrepancyByCustomerEnqResult customerEnqResult4 = customerEnqResult3;
              Decimal? glTurnover = customerEnqResult4.GLTurnover;
              Decimal num4 = num3;
              customerEnqResult4.GLTurnover = glTurnover.HasValue ? new Decimal?(glTurnover.GetValueOrDefault() + num4) : new Decimal?();
            }
            else
            {
              Customer customer = (Customer) PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) key);
              if (customer != null)
              {
                customerEnqResult3 = new ARGLDiscrepancyByCustomerEnqResult();
                customerEnqResult3.CustomerID = customer.BAccountID;
                customerEnqResult3.AcctCD = customer.AcctCD;
                customerEnqResult3.AcctName = customer.AcctName;
                customerEnqResult3.GLTurnover = new Decimal?(num3);
                customerEnqResult3.XXTurnover = new Decimal?(0M);
                dictionary.Add(key, customerEnqResult3);
              }
            }
          }
          customerEnqResultList.AddRange(dictionary.Values.Where<ARGLDiscrepancyByCustomerEnqResult>((Func<ARGLDiscrepancyByCustomerEnqResult, bool>) (result =>
          {
            if (!header.ShowOnlyWithDiscrepancy.GetValueOrDefault())
              return true;
            Decimal? discrepancy = result.Discrepancy;
            Decimal num = 0M;
            return !(discrepancy.GetValueOrDefault() == num & discrepancy.HasValue);
          })));
          return customerEnqResultList;
        }
      }
    }
    return customerEnqResultList;
  }

  [PXLookupButton]
  public virtual IEnumerable ViewDetails(PXAdapter adapter)
  {
    ARGLDiscrepancyByCustomerEnqFilter current1 = this.Filter.Current;
    ARGLDiscrepancyByCustomerEnqResult current2 = this.Rows.Current;
    if (current1 != null && current2 != null)
    {
      ARGLDiscrepancyByDocumentEnq instance = PXGraph.CreateInstance<ARGLDiscrepancyByDocumentEnq>();
      ARGLDiscrepancyByCustomerEnqFilter current3 = instance.Filter.Current;
      current3.BranchID = current1.BranchID;
      current3.CustomerID = current2.CustomerID;
      current3.PeriodFrom = current1.PeriodFrom;
      current3.AccountID = current1.AccountID;
      current3.SubCD = current1.SubCD;
      current3.ShowOnlyWithDiscrepancy = current1.ShowOnlyWithDiscrepancy;
      instance.Filter.Select();
      PXRedirectHelper.TryRedirect((PXGraph) instance, PXRedirectHelper.WindowMode.New);
    }
    return adapter.Get();
  }
}

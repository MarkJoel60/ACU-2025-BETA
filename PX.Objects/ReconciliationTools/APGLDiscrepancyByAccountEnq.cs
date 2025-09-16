// Decompiled with JetBrains decompiler
// Type: ReconciliationTools.APGLDiscrepancyByAccountEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace ReconciliationTools;

[TableAndChartDashboardType]
public class APGLDiscrepancyByAccountEnq : 
  APGLDiscrepancyEnqGraphBase<APGLDiscrepancyByAccountEnq, APGLDiscrepancyEnqFilter, DiscrepancyByAccountEnqResult>
{
  public PXAction<APGLDiscrepancyEnqFilter> viewDetails;

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXDefault]
  protected virtual void APGLDiscrepancyEnqFilter_AccountID_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "AP Turnover")]
  protected virtual void DiscrepancyByAccountEnqResult_XXTurnover_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Non-AP Transactions")]
  protected virtual void DiscrepancyByAccountEnqResult_NonXXTrans_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Financial Period")]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  protected virtual void DiscrepancyByAccountEnqResult_FinPeriodID_CacheAttached(PXCache sender)
  {
  }

  protected override List<DiscrepancyByAccountEnqResult> SelectDetails()
  {
    List<DiscrepancyByAccountEnqResult> accountEnqResultList = new List<DiscrepancyByAccountEnqResult>();
    APGLDiscrepancyEnqFilter header = this.Filter.Current;
    if (header != null)
    {
      int? nullable1 = header.BranchID;
      if (nullable1.HasValue && header.PeriodFrom != null && header.PeriodTo != null)
      {
        nullable1 = header.AccountID;
        if (nullable1.HasValue)
        {
          AccountByPeriodEnq instance1 = PXGraph.CreateInstance<AccountByPeriodEnq>();
          AccountByPeriodFilter copy = PXCache<AccountByPeriodFilter>.CreateCopy(instance1.Filter.Current);
          instance1.Filter.Cache.SetDefaultExt<AccountByPeriodFilter.ledgerID>((object) copy);
          copy.BranchID = header.BranchID;
          copy.SubID = header.SubCD;
          copy.StartPeriodID = header.PeriodFrom;
          copy.EndPeriodID = header.PeriodTo;
          copy.AccountID = header.AccountID;
          instance1.Filter.Update(copy);
          Dictionary<DiscrepancyByAccountEnqResultKey, DiscrepancyByAccountEnqResult> dictionary = new Dictionary<DiscrepancyByAccountEnqResultKey, DiscrepancyByAccountEnqResult>();
          foreach (PXResult<GLTranR> pxResult in instance1.GLTranEnq.Select())
          {
            GLTranR glTranR = (GLTranR) pxResult;
            DiscrepancyByAccountEnqResultKey key = new DiscrepancyByAccountEnqResultKey((PX.Objects.GL.GLTran) glTranR);
            int num1;
            if (glTranR.Module == "AP" && !string.IsNullOrEmpty(glTranR.TranType) && !string.IsNullOrEmpty(glTranR.RefNbr))
            {
              nullable1 = glTranR.ReferenceID;
              num1 = nullable1.HasValue ? 1 : 0;
            }
            else
              num1 = 0;
            Decimal num2 = this.CalcGLTurnover((PX.Objects.GL.GLTran) glTranR);
            Decimal num3 = num1 != 0 ? 0M : num2;
            DiscrepancyByAccountEnqResult accountEnqResult1;
            if (dictionary.TryGetValue(key, out accountEnqResult1))
            {
              DiscrepancyByAccountEnqResult accountEnqResult2 = accountEnqResult1;
              Decimal? glTurnover = accountEnqResult2.GLTurnover;
              Decimal num4 = num2;
              accountEnqResult2.GLTurnover = glTurnover.HasValue ? new Decimal?(glTurnover.GetValueOrDefault() + num4) : new Decimal?();
              DiscrepancyByAccountEnqResult accountEnqResult3 = accountEnqResult1;
              Decimal? nonXxTrans = accountEnqResult3.NonXXTrans;
              Decimal num5 = num3;
              accountEnqResult3.NonXXTrans = nonXxTrans.HasValue ? new Decimal?(nonXxTrans.GetValueOrDefault() + num5) : new Decimal?();
            }
            else
            {
              accountEnqResult1 = new DiscrepancyByAccountEnqResult();
              accountEnqResult1.GLTurnover = new Decimal?(num2);
              accountEnqResult1.XXTurnover = new Decimal?(0M);
              accountEnqResult1.NonXXTrans = new Decimal?(num3);
              instance1.GLTranEnq.Cache.RestoreCopy((object) accountEnqResult1, (object) glTranR);
              dictionary.Add(key, accountEnqResult1);
            }
          }
          APVendorBalanceEnq instance2 = PXGraph.CreateInstance<APVendorBalanceEnq>();
          APVendorBalanceEnq.APHistoryFilter apHistoryFilter = PXCache<APVendorBalanceEnq.APHistoryFilter>.CreateCopy(instance2.Filter.Current);
          apHistoryFilter.BranchID = header.BranchID;
          apHistoryFilter.ByFinancialPeriod = new bool?(true);
          apHistoryFilter.ShowWithBalanceOnly = new bool?(false);
          apHistoryFilter.SplitByCurrency = new bool?(false);
          foreach (KeyValuePair<DiscrepancyByAccountEnqResultKey, DiscrepancyByAccountEnqResult> keyValuePair in dictionary)
          {
            DiscrepancyByAccountEnqResultKey key = keyValuePair.Key;
            DiscrepancyByAccountEnqResult accountEnqResult4 = keyValuePair.Value;
            apHistoryFilter.FinPeriodID = keyValuePair.Key.FinPeriodID;
            apHistoryFilter.AccountID = keyValuePair.Key.AccountID;
            apHistoryFilter.SubID = this.GetSubCD(keyValuePair.Key.SubID);
            apHistoryFilter = instance2.Filter.Update(apHistoryFilter);
            foreach (PXResult<APVendorBalanceEnq.APHistoryResult> pxResult in instance2.History.Select())
            {
              APVendorBalanceEnq.APHistoryResult apHistoryResult = (APVendorBalanceEnq.APHistoryResult) pxResult;
              DiscrepancyByAccountEnqResult accountEnqResult5 = accountEnqResult4;
              Decimal? xxTurnover = accountEnqResult5.XXTurnover;
              Decimal? nullable2 = apHistoryResult.Balance;
              Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
              nullable2 = apHistoryResult.Deposits;
              Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
              Decimal num = valueOrDefault1 - valueOrDefault2;
              Decimal? nullable3;
              if (!xxTurnover.HasValue)
              {
                nullable2 = new Decimal?();
                nullable3 = nullable2;
              }
              else
                nullable3 = new Decimal?(xxTurnover.GetValueOrDefault() + num);
              accountEnqResult5.XXTurnover = nullable3;
            }
          }
          accountEnqResultList.AddRange(dictionary.Values.Where<DiscrepancyByAccountEnqResult>((Func<DiscrepancyByAccountEnqResult, bool>) (result =>
          {
            if (!header.ShowOnlyWithDiscrepancy.GetValueOrDefault())
              return true;
            Decimal? discrepancy = result.Discrepancy;
            Decimal num = 0M;
            return !(discrepancy.GetValueOrDefault() == num & discrepancy.HasValue);
          })));
          return accountEnqResultList;
        }
      }
    }
    return accountEnqResultList;
  }

  [PXLookupButton]
  public virtual IEnumerable ViewDetails(PXAdapter adapter)
  {
    APGLDiscrepancyEnqFilter current1 = this.Filter.Current;
    DiscrepancyByAccountEnqResult current2 = this.Rows.Current;
    if (current1 != null && current2 != null)
    {
      APGLDiscrepancyByVendorEnq instance = PXGraph.CreateInstance<APGLDiscrepancyByVendorEnq>();
      APGLDiscrepancyByVendorEnqFilter current3 = instance.Filter.Current;
      current3.BranchID = current2.BranchID;
      current3.PeriodFrom = current2.FinPeriodID;
      current3.AccountID = current2.AccountID;
      current3.SubCD = this.GetSubCD(current2.SubID);
      current3.ShowOnlyWithDiscrepancy = current1.ShowOnlyWithDiscrepancy;
      instance.Filter.Select();
      PXRedirectHelper.TryRedirect((PXGraph) instance, PXRedirectHelper.WindowMode.New);
    }
    return adapter.Get();
  }
}

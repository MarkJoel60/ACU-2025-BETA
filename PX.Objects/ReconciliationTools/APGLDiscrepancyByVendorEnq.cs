// Decompiled with JetBrains decompiler
// Type: ReconciliationTools.APGLDiscrepancyByVendorEnq
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
public class APGLDiscrepancyByVendorEnq : 
  APGLDiscrepancyEnqGraphBase<APGLDiscrepancyByAccountEnq, APGLDiscrepancyByVendorEnqFilter, APGLDiscrepancyByVendorEnqResult>
{
  public PXAction<APGLDiscrepancyByVendorEnqFilter> viewDetails;

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Financial Period")]
  protected virtual void APGLDiscrepancyByVendorEnqFilter_PeriodFrom_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXDefault]
  protected virtual void APGLDiscrepancyByVendorEnqFilter_AccountID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXDefault]
  protected virtual void APGLDiscrepancyByVendorEnqFilter_SubCD_CacheAttached(PXCache sender)
  {
  }

  protected override List<APGLDiscrepancyByVendorEnqResult> SelectDetails()
  {
    List<APGLDiscrepancyByVendorEnqResult> byVendorEnqResultList = new List<APGLDiscrepancyByVendorEnqResult>();
    APGLDiscrepancyByVendorEnqFilter header = this.Filter.Current;
    if (header != null)
    {
      int? nullable1 = header.BranchID;
      if (nullable1.HasValue && header.PeriodFrom != null)
      {
        nullable1 = header.AccountID;
        if (nullable1.HasValue && header.SubCD != null)
        {
          APVendorBalanceEnq instance1 = PXGraph.CreateInstance<APVendorBalanceEnq>();
          APVendorBalanceEnq.APHistoryFilter copy1 = PXCache<APVendorBalanceEnq.APHistoryFilter>.CreateCopy(instance1.Filter.Current);
          copy1.BranchID = header.BranchID;
          copy1.VendorID = header.VendorID;
          copy1.FinPeriodID = header.PeriodFrom;
          copy1.AccountID = header.AccountID;
          copy1.SubID = header.SubCD;
          copy1.ByFinancialPeriod = new bool?(true);
          copy1.ShowWithBalanceOnly = new bool?(false);
          copy1.SplitByCurrency = new bool?(false);
          instance1.Filter.Update(copy1);
          Dictionary<int, APGLDiscrepancyByVendorEnqResult> dictionary = new Dictionary<int, APGLDiscrepancyByVendorEnqResult>();
          foreach (PXResult<APVendorBalanceEnq.APHistoryResult> pxResult in instance1.History.Select())
          {
            APVendorBalanceEnq.APHistoryResult copy2 = (APVendorBalanceEnq.APHistoryResult) pxResult;
            nullable1 = copy2.VendorID;
            int key = nullable1.Value;
            Decimal? nullable2 = copy2.Balance;
            Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
            nullable2 = copy2.Deposits;
            Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
            Decimal num1 = valueOrDefault1 - valueOrDefault2;
            APGLDiscrepancyByVendorEnqResult byVendorEnqResult1;
            if (dictionary.TryGetValue(key, out byVendorEnqResult1))
            {
              APGLDiscrepancyByVendorEnqResult byVendorEnqResult2 = byVendorEnqResult1;
              nullable2 = byVendorEnqResult2.XXTurnover;
              Decimal num2 = num1;
              byVendorEnqResult2.XXTurnover = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num2) : new Decimal?();
            }
            else
            {
              byVendorEnqResult1 = new APGLDiscrepancyByVendorEnqResult();
              byVendorEnqResult1.GLTurnover = new Decimal?(0M);
              byVendorEnqResult1.XXTurnover = new Decimal?(num1);
              PXCache<APVendorBalanceEnq.APHistoryResult>.RestoreCopy((APVendorBalanceEnq.APHistoryResult) byVendorEnqResult1, copy2);
              dictionary.Add(key, byVendorEnqResult1);
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
            if (!(row.Module == "AP") || !row.ReferenceID.HasValue)
              return false;
            int? nullable3 = header.VendorID;
            if (!nullable3.HasValue)
              return true;
            nullable3 = row.ReferenceID;
            int? vendorId = header.VendorID;
            return nullable3.GetValueOrDefault() == vendorId.GetValueOrDefault() & nullable3.HasValue == vendorId.HasValue;
          })))
          {
            int key = tran.ReferenceID.Value;
            Decimal num3 = this.CalcGLTurnover((PX.Objects.GL.GLTran) tran);
            APGLDiscrepancyByVendorEnqResult byVendorEnqResult3;
            if (dictionary.TryGetValue(key, out byVendorEnqResult3))
            {
              APGLDiscrepancyByVendorEnqResult byVendorEnqResult4 = byVendorEnqResult3;
              Decimal? glTurnover = byVendorEnqResult4.GLTurnover;
              Decimal num4 = num3;
              byVendorEnqResult4.GLTurnover = glTurnover.HasValue ? new Decimal?(glTurnover.GetValueOrDefault() + num4) : new Decimal?();
            }
            else
            {
              Vendor vendor = (Vendor) PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.bAccountID, Equal<Required<Vendor.bAccountID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) key);
              if (vendor != null)
              {
                byVendorEnqResult3 = new APGLDiscrepancyByVendorEnqResult();
                byVendorEnqResult3.VendorID = vendor.BAccountID;
                byVendorEnqResult3.AcctCD = vendor.AcctCD;
                byVendorEnqResult3.AcctName = vendor.AcctName;
                byVendorEnqResult3.GLTurnover = new Decimal?(num3);
                byVendorEnqResult3.XXTurnover = new Decimal?(0M);
                dictionary.Add(key, byVendorEnqResult3);
              }
            }
          }
          byVendorEnqResultList.AddRange(dictionary.Values.Where<APGLDiscrepancyByVendorEnqResult>((Func<APGLDiscrepancyByVendorEnqResult, bool>) (result =>
          {
            if (!header.ShowOnlyWithDiscrepancy.GetValueOrDefault())
              return true;
            Decimal? discrepancy = result.Discrepancy;
            Decimal num = 0M;
            return !(discrepancy.GetValueOrDefault() == num & discrepancy.HasValue);
          })));
          return byVendorEnqResultList;
        }
      }
    }
    return byVendorEnqResultList;
  }

  [PXLookupButton]
  public virtual IEnumerable ViewDetails(PXAdapter adapter)
  {
    APGLDiscrepancyByVendorEnqFilter current1 = this.Filter.Current;
    APGLDiscrepancyByVendorEnqResult current2 = this.Rows.Current;
    if (current1 != null && current2 != null)
    {
      APGLDiscrepancyByDocumentEnq instance = PXGraph.CreateInstance<APGLDiscrepancyByDocumentEnq>();
      APGLDiscrepancyByVendorEnqFilter current3 = instance.Filter.Current;
      current3.BranchID = current1.BranchID;
      current3.VendorID = current2.VendorID;
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

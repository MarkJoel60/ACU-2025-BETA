// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ExpenseClaimDetailGraphExtBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.EP.DAC;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.ComponentModel;
using System.Web;

#nullable disable
namespace PX.Objects.EP;

/// <summary>Services only (and reduced handlers if needed).</summary>
public class ExpenseClaimDetailGraphExtBase<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public PXSetup<PX.Objects.EP.EPSetup> EPSetup;

  public virtual PXSelectBase<EPExpenseClaimDetails> Receipts { get; }

  public virtual PXSelectBase<EPExpenseClaim> Claim { get; }

  public virtual PXSelectBase<PX.Objects.CM.CurrencyInfo> CurrencyInfo { get; }

  public virtual EPEmployeeCorpCardLink GetFirstCreditCardForEmployeeAlphabeticallySorted(
    int employeeID)
  {
    return PXResultset<EPEmployeeCorpCardLink>.op_Implicit(PXSelectBase<EPEmployeeCorpCardLink, PXSelectJoin<EPEmployeeCorpCardLink, InnerJoin<CACorpCard, On<CACorpCard.corpCardID, Equal<EPEmployeeCorpCardLink.corpCardID>>>, Where<EPEmployeeCorpCardLink.employeeID, Equal<Required<EPExpenseClaimDetails.employeeID>>, And<CACorpCard.isActive, Equal<True>>>, OrderBy<Asc<CACorpCard.name>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
    {
      (object) employeeID
    }));
  }

  public virtual EPExpenseClaimDetails GetLastUsedCreditCardForEmployee(int employeeID)
  {
    return PXResultset<EPExpenseClaimDetails>.op_Implicit(PXSelectBase<EPExpenseClaimDetails, PXSelectJoin<EPExpenseClaimDetails, InnerJoin<EPEmployeeCorpCardLink, On<EPEmployeeCorpCardLink.employeeID, Equal<EPExpenseClaimDetails.employeeID>>, InnerJoin<CACorpCard, On<CACorpCard.corpCardID, Equal<EPEmployeeCorpCardLink.corpCardID>, And<CACorpCard.corpCardID, Equal<EPExpenseClaimDetails.corpCardID>>>>>, Where<CACorpCard.isActive, Equal<True>, And<EPEmployeeCorpCardLink.employeeID, Equal<Required<EPExpenseClaimDetails.employeeID>>>>, OrderBy<Desc<EPExpenseClaimDetails.lastModifiedDateTime>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
    {
      (object) employeeID
    }));
  }

  protected virtual Decimal? GetUnitCostByExpenseItem(PXCache cache, EPExpenseClaimDetails receipt)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PXSelectorAttribute.Select<PX.Objects.IN.InventoryItem.inventoryID>(cache, (object) receipt) as PX.Objects.IN.InventoryItem;
    PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<EPExpenseClaimDetails.curyInfoID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) receipt.CuryInfoID
    }));
    Decimal curyval = 0M;
    if (inventoryItem != null && currencyInfo != null && currencyInfo.CuryRate.HasValue)
    {
      InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this.Base, inventoryItem.InventoryID, currencyInfo.BaseCuryID);
      if (itemCurySettings != null)
      {
        DateTime? expenseDate = receipt.ExpenseDate;
        DateTime? stdCostDate = itemCurySettings.StdCostDate;
        Decimal baseval = (expenseDate.HasValue & stdCostDate.HasValue ? (expenseDate.GetValueOrDefault() >= stdCostDate.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? itemCurySettings.StdCost.GetValueOrDefault() : itemCurySettings.LastStdCost.GetValueOrDefault();
        PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.curyInfoID>(cache, (object) receipt, baseval, out curyval, true);
      }
    }
    return new Decimal?(curyval);
  }

  public virtual void ClearFieldsIfNeeded(PXCache cache, EPExpenseClaimDetails row)
  {
    if (!(row.PaidWith != "PersAcc"))
      cache.SetValueExt<EPExpenseClaimDetails.corpCardID>((object) row, (object) null);
    if (!(row.PaidWith != "CardPers"))
    {
      cache.SetValueExt<EPExpenseClaimDetails.taxZoneID>((object) row, (object) null);
      cache.SetValueExt<EPExpenseClaimDetails.taxCategoryID>((object) row, (object) null);
    }
    if (!(row.PaidWith == "CardPers"))
      return;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.nonProject, Equal<True>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object[]) null));
    cache.SetValueExt<EPExpenseClaimDetails.contractID>((object) row, (object) pmProject.ContractCD);
    cache.SetValueExt<EPExpenseClaimDetails.taskID>((object) row, (object) null);
    cache.SetValueExt<EPExpenseClaimDetails.costCodeID>((object) row, (object) null);
    cache.SetValueExt<EPExpenseClaimDetails.curyTipAmt>((object) row, (object) 0);
  }

  public virtual void DefaultCardCurrencyInfo(PXCache cache, EPExpenseClaimDetails document)
  {
    if (!document.CardCuryInfoID.HasValue)
      return;
    PX.Objects.CM.CurrencyInfo currencyInfo = CurrencyInfoAttribute.SetDefaults<EPExpenseClaimDetails.cardCuryInfoID>(cache, (object) document);
    if (currencyInfo == null)
      return;
    document.CardCuryID = currencyInfo.CuryID;
  }

  public virtual void SetCardCurrencyData(
    PXCache cache,
    EPExpenseClaimDetails document,
    int? corpCardID)
  {
    if (corpCardID.HasValue)
    {
      PX.Objects.CA.CashAccount cardCashAccount = CACorpCardsMaint.GetCardCashAccount((PXGraph) this.Base, corpCardID);
      cache.SetValueExt<EPExpenseClaimDetails.cardCuryID>((object) document, (object) cardCashAccount.CuryID);
    }
    else
      this.DefaultCardCurrencyInfo(cache, document);
  }

  public virtual void SetClaimCuryWhenNotInClaim(
    EPExpenseClaimDetails document,
    string claimRefNbr,
    int? corpCardID)
  {
    if (claimRefNbr != null)
      return;
    document.ClaimCuryInfoID = corpCardID.HasValue ? document.CardCuryInfoID : new long?();
  }

  internal static void DeleteLegacyTaxRows(PXGraph graph, string refNbr)
  {
    if (string.IsNullOrEmpty(refNbr))
      return;
    PXCache cach1 = graph.Caches[typeof (EPExpenseClaim)];
    PXCache cach2 = graph.Caches[typeof (EPTaxTran)];
    EPExpenseClaim epExpenseClaim1 = PXResultset<EPExpenseClaim>.op_Implicit(PXSelectBase<EPExpenseClaim, PXSelect<EPExpenseClaim, Where<EPExpenseClaim.refNbr, Equal<Required<EPExpenseClaim.refNbr>>>>.Config>.Select(graph, new object[1]
    {
      (object) refNbr
    }));
    bool flag = false;
    foreach (PXResult<EPTaxAggregate> pxResult in PXSelectBase<EPTaxAggregate, PXSelect<EPTaxAggregate, Where<EPTaxAggregate.refNbr, Equal<Required<EPExpenseClaim.refNbr>>>>.Config>.Select(graph, new object[1]
    {
      (object) refNbr
    }))
    {
      EPTaxAggregate epTaxAggregate = PXResult<EPTaxAggregate>.op_Implicit(pxResult);
      EPExpenseClaim epExpenseClaim2 = epExpenseClaim1;
      Decimal? nullable1 = epExpenseClaim2.TaxTotal;
      Decimal? nullable2 = epTaxAggregate.TaxAmt;
      epExpenseClaim2.TaxTotal = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      EPExpenseClaim epExpenseClaim3 = epExpenseClaim1;
      nullable2 = epExpenseClaim3.CuryTaxTotal;
      nullable1 = epTaxAggregate.CuryTaxAmt;
      epExpenseClaim3.CuryTaxTotal = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      flag = true;
      graph.Caches[typeof (EPTaxAggregate)].Delete((object) epTaxAggregate);
    }
    if (!flag)
      return;
    epExpenseClaim1.VatTaxableTotal = new Decimal?(0M);
    epExpenseClaim1.CuryVatTaxableTotal = new Decimal?(0M);
    epExpenseClaim1.CuryVatExemptTotal = new Decimal?(0M);
    epExpenseClaim1.VatExemptTotal = new Decimal?(0M);
    cach1.Update((object) epExpenseClaim1);
  }

  public static string GetTaxZoneID(PXGraph graph, EPEmployee employee)
  {
    string taxZoneId = employee?.ReceiptAndClaimTaxZoneID;
    if (string.IsNullOrEmpty(taxZoneId))
      taxZoneId = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.locationID, Equal<Required<EPEmployee.defLocationID>>>>.Config>.Select(graph, new object[1]
      {
        (object) (int?) employee?.DefLocationID
      }))?.VTaxZoneID;
    return taxZoneId;
  }

  public static void CheckAllowedUser(PXGraph graph)
  {
    if (PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Config>.Select(graph, Array.Empty<object>())) != null || PXGraph.ProxyIsActive)
      return;
    if (graph.IsExport || graph.IsImport)
      throw new PXException("User must be an Employee to use current screen.");
    Redirector.Redirect(HttpContext.Current, $"~/Frames/Error.aspx?exceptionID={"User must be an Employee to use current screen."}&typeID={"error"}", false);
  }

  public virtual void SubmitReceiptExt(
    PXCache claimsCache,
    PXCache receiptsCache,
    EPExpenseClaim claim,
    EPExpenseClaimDetails receipt)
  {
    receipt = receiptsCache.CreateCopy((object) receipt) as EPExpenseClaimDetails;
    string refNbr = receipt.RefNbr;
    receipt.RefNbr = claim.RefNbr;
    this.RefNbrUpdated(receiptsCache, claim, receipt, refNbr);
    receiptsCache.Update((object) receipt);
  }

  public virtual void RemoveReceipt(
    PXCache cache,
    EPExpenseClaimDetails receipt,
    bool skipReceiptCacheUpdate = false)
  {
    receipt = cache.CreateCopy((object) receipt) as EPExpenseClaimDetails;
    string refNbr = receipt.RefNbr;
    receipt.RefNbr = (string) null;
    this.RefNbrUpdated(cache, (EPExpenseClaim) null, receipt, refNbr);
    if (skipReceiptCacheUpdate)
      return;
    cache.Update((object) receipt);
  }

  public virtual void RefNbrUpdated(
    PXCache receiptsCache,
    EPExpenseClaim claim,
    EPExpenseClaimDetails receipt,
    string oldRefNbr)
  {
    this.SetClaimCuryWhenNotInClaim(receipt, claim?.RefNbr, receipt.CorpCardID);
    if (claim != null)
    {
      receipt.ClaimCuryInfoID = (long?) claim?.CuryInfoID;
      receipt.SubmitedDate = new DateTime?(DateTime.Now);
      EPExpenseClaim copy = (EPExpenseClaim) ((PXSelectBase) this.Claim).Cache.CreateCopy((object) claim);
      EPExpenseClaimDetails receiptAndClaimAmounts = this.CalculateReceiptAndClaimAmounts(receipt);
      if (ExpenseClaimDetailGraphExtBase<TGraph>.IsSumCalculationNeeded(claim, copy, receipt, receiptAndClaimAmounts, oldRefNbr) && (!receipt.IsPaidWithCard || receipt.IsPaidWithCard && receipt.CorpCardID.HasValue))
        this.SumClaimValues(claim, receipt, (EPExpenseClaimDetails) null);
      if (oldRefNbr != null && claim.RefNbr != oldRefNbr)
        this.RecalculateAmountsInOldClaim(receipt, oldRefNbr);
    }
    else
    {
      EPExpenseClaimDetails copy = (EPExpenseClaimDetails) receiptsCache.CreateCopy((object) receipt);
      receipt.SubmitedDate = new DateTime?();
      this.RecalcAmountInClaimCury(receipt);
      if (oldRefNbr != null)
      {
        EPExpenseClaim parentClaim = this.GetParentClaim(oldRefNbr);
        if (parentClaim != null)
          this.SumClaimValues(parentClaim, (EPExpenseClaimDetails) null, copy);
      }
    }
    foreach (PXResult<EPTaxTran> pxResult in PXSelectBase<EPTaxTran, PXSelect<EPTaxTran, Where<EPTaxTran.claimDetailID, Equal<Required<EPExpenseClaimDetails.claimDetailID>>>>.Config>.Select(receiptsCache.Graph, new object[1]
    {
      (object) receipt.ClaimDetailID
    }))
    {
      EPTaxTran epTaxTran = PXResult<EPTaxTran>.op_Implicit(pxResult);
      PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<EPTaxTran.curyInfoID>>>>.Config>.Select(receiptsCache.Graph, new object[1]
      {
        (object) epTaxTran.CuryInfoID
      }));
      object obj;
      receiptsCache.Graph.Caches[typeof (PX.Objects.CM.CurrencyInfo)].RaiseFieldDefaulting<PX.Objects.CM.CurrencyInfo.curyPrecision>((object) currencyInfo, ref obj);
      currencyInfo.CuryPrecision = obj as short?;
      epTaxTran.RefNbr = receipt.RefNbr;
      receiptsCache.Graph.Caches[typeof (EPTaxTran)].Update((object) epTaxTran);
    }
  }

  private static bool IsSumCalculationNeeded(
    EPExpenseClaim newClaim,
    EPExpenseClaim oldClaim,
    EPExpenseClaimDetails newReceipt,
    EPExpenseClaimDetails oldReceipt,
    string oldRefNbr)
  {
    bool flag1 = false;
    bool flag2 = false;
    Decimal? claimCuryTranAmt1 = oldReceipt.ClaimCuryTranAmt;
    Decimal? claimCuryTranAmt2 = newReceipt.ClaimCuryTranAmt;
    Decimal? nullable1;
    Decimal? nullable2;
    if (claimCuryTranAmt1.GetValueOrDefault() == claimCuryTranAmt2.GetValueOrDefault() & claimCuryTranAmt1.HasValue == claimCuryTranAmt2.HasValue)
    {
      nullable1 = oldReceipt.ClaimCuryTranAmtWithTaxes;
      Decimal? tranAmtWithTaxes = newReceipt.ClaimCuryTranAmtWithTaxes;
      if (nullable1.GetValueOrDefault() == tranAmtWithTaxes.GetValueOrDefault() & nullable1.HasValue == tranAmtWithTaxes.HasValue)
      {
        nullable2 = oldReceipt.ClaimCuryTaxTotal;
        nullable1 = newReceipt.ClaimCuryTaxTotal;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          nullable1 = oldReceipt.ClaimCuryTaxRoundDiff;
          nullable2 = newReceipt.ClaimCuryTaxRoundDiff;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = oldReceipt.ClaimCuryVatExemptTotal;
            nullable1 = newReceipt.ClaimCuryVatExemptTotal;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            {
              nullable1 = oldReceipt.ClaimCuryVatTaxableTotal;
              nullable2 = newReceipt.ClaimCuryVatTaxableTotal;
              if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
                goto label_7;
            }
          }
        }
      }
    }
    flag1 = true;
label_7:
    nullable2 = oldClaim.CuryDocBal;
    nullable1 = newClaim.CuryDocBal;
    if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
    {
      nullable1 = oldClaim.CuryTaxTotal;
      nullable2 = newClaim.CuryTaxTotal;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        nullable2 = oldClaim.CuryTaxRoundDiff;
        nullable1 = newClaim.CuryTaxRoundDiff;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          nullable1 = oldClaim.CuryVatExemptTotal;
          nullable2 = newClaim.CuryVatExemptTotal;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = oldClaim.CuryVatTaxableTotal;
            nullable1 = newClaim.CuryVatTaxableTotal;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
              goto label_13;
          }
        }
      }
    }
    flag2 = true;
label_13:
    return !(flag1 & flag2) && (flag1 && !flag2 || oldRefNbr == null || newClaim.RefNbr != oldRefNbr);
  }

  private void RecalculateAmountsInOldClaim(EPExpenseClaimDetails receipt, string oldRefNbr)
  {
    EPExpenseClaim parentClaim = this.GetParentClaim(oldRefNbr);
    if (parentClaim == null)
      return;
    this.SumClaimValues(parentClaim, (EPExpenseClaimDetails) null, receipt);
  }

  public virtual void RecalcAmountInClaimCury(EPExpenseClaimDetails receipt)
  {
    if (receipt == null || !receipt.TranAmt.HasValue || !receipt.TranAmtWithTaxes.HasValue)
      return;
    EPExpenseClaimDetails oldReceipt = this.RecalcAmountInClaimCuryForReceipt(receipt);
    if (this.Claim == null || receipt.RefNbr == null)
      return;
    this.SumClaimValues(this.GetParentClaim(receipt.RefNbr), receipt, oldReceipt);
  }

  public virtual EPExpenseClaimDetails CalculateReceiptAndClaimAmounts(EPExpenseClaimDetails receipt)
  {
    if (receipt == null || !receipt.TranAmt.HasValue || !receipt.TranAmtWithTaxes.HasValue)
      return receipt;
    EPExpenseClaimDetails oldReceipt = this.RecalcAmountInClaimCuryForReceipt(receipt);
    if (this.Claim != null && receipt.RefNbr != null)
      this.SumClaimValues(this.GetParentClaim(receipt.RefNbr), receipt, oldReceipt);
    return oldReceipt;
  }

  public virtual EPExpenseClaimDetails RecalcAmountInClaimCuryForReceipt(
    EPExpenseClaimDetails receipt)
  {
    EPExpenseClaimDetails expenseClaimDetails = (EPExpenseClaimDetails) null;
    if (receipt != null)
    {
      Decimal? nullable = receipt.TranAmt;
      if (nullable.HasValue)
      {
        nullable = receipt.TranAmtWithTaxes;
        if (nullable.HasValue)
        {
          Decimal num1 = 0M;
          Decimal num2 = 0M;
          Decimal num3 = 0M;
          Decimal num4 = 0M;
          Decimal num5 = 0M;
          Decimal num6 = 0M;
          if (!receipt.IsPaidWithCard || receipt.IsPaidWithCard && receipt.CorpCardID.HasValue)
          {
            PXCache cach = this.Base.Caches[typeof (EPTaxTran)];
            PX.Objects.CM.CurrencyInfo curyInfoA = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<EPExpenseClaimDetails.curyInfoID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
            {
              (object) receipt.CuryInfoID
            }));
            PX.Objects.CM.CurrencyInfo curyInfoB = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<EPExpenseClaimDetails.curyInfoID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
            {
              (object) receipt.ClaimCuryInfoID
            }));
            if (CurrencyHelper.IsSameCury(receipt.CuryInfoID, receipt.ClaimCuryInfoID, curyInfoA, curyInfoB))
            {
              nullable = receipt.CuryTranAmt;
              num1 = nullable.GetValueOrDefault();
              nullable = receipt.CuryTranAmtWithTaxes;
              num2 = nullable.GetValueOrDefault();
              nullable = receipt.CuryTaxTotal;
              num3 = nullable.GetValueOrDefault();
              nullable = receipt.CuryTaxRoundDiff;
              num4 = nullable.GetValueOrDefault();
              nullable = receipt.CuryVatExemptTotal;
              num5 = nullable.GetValueOrDefault();
              nullable = receipt.CuryVatTaxableTotal;
              num6 = nullable.GetValueOrDefault();
              foreach (PXResult<EPTaxTran> pxResult in PXSelectBase<EPTaxTran, PXSelect<EPTaxTran, Where<EPTaxTran.claimDetailID, Equal<Required<EPExpenseClaimDetails.claimDetailID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
              {
                (object) receipt.ClaimDetailID
              }))
              {
                EPTaxTran epTaxTran1 = PXResult<EPTaxTran>.op_Implicit(pxResult);
                if (cach.GetStatus((object) epTaxTran1) != 2)
                  cach.SetStatus((object) epTaxTran1, (PXEntryStatus) 1);
                PXCache pxCache1 = cach;
                EPTaxTran epTaxTran2 = epTaxTran1;
                nullable = epTaxTran1.CuryExpenseAmt;
                // ISSUE: variable of a boxed type
                __Boxed<Decimal> valueOrDefault1 = (ValueType) nullable.GetValueOrDefault();
                pxCache1.SetValue<EPTaxTran.claimCuryExpenseAmt>((object) epTaxTran2, (object) valueOrDefault1);
                PXCache pxCache2 = cach;
                EPTaxTran epTaxTran3 = epTaxTran1;
                nullable = epTaxTran1.CuryTaxableAmt;
                // ISSUE: variable of a boxed type
                __Boxed<Decimal> valueOrDefault2 = (ValueType) nullable.GetValueOrDefault();
                pxCache2.SetValue<EPTaxTran.claimCuryTaxableAmt>((object) epTaxTran3, (object) valueOrDefault2);
                PXCache pxCache3 = cach;
                EPTaxTran epTaxTran4 = epTaxTran1;
                nullable = epTaxTran1.CuryTaxAmt;
                // ISSUE: variable of a boxed type
                __Boxed<Decimal> valueOrDefault3 = (ValueType) nullable.GetValueOrDefault();
                pxCache3.SetValue<EPTaxTran.claimCuryTaxAmt>((object) epTaxTran4, (object) valueOrDefault3);
              }
            }
            else if (curyInfoB != null)
            {
              nullable = curyInfoB.CuryRate;
              if (nullable.HasValue)
              {
                PXCache cache1 = ((PXSelectBase) this.Receipts).Cache;
                EPExpenseClaimDetails row1 = receipt;
                nullable = receipt.TranAmt;
                Decimal valueOrDefault4 = nullable.GetValueOrDefault();
                ref Decimal local1 = ref num1;
                PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.claimCuryInfoID>(cache1, (object) row1, valueOrDefault4, out local1);
                PXCache cache2 = ((PXSelectBase) this.Receipts).Cache;
                EPExpenseClaimDetails row2 = receipt;
                nullable = receipt.TranAmtWithTaxes;
                Decimal valueOrDefault5 = nullable.GetValueOrDefault();
                ref Decimal local2 = ref num2;
                PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.claimCuryInfoID>(cache2, (object) row2, valueOrDefault5, out local2);
                PXCache cache3 = ((PXSelectBase) this.Receipts).Cache;
                EPExpenseClaimDetails row3 = receipt;
                nullable = receipt.TaxTotal;
                Decimal valueOrDefault6 = nullable.GetValueOrDefault();
                ref Decimal local3 = ref num3;
                PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.claimCuryInfoID>(cache3, (object) row3, valueOrDefault6, out local3);
                PXCache cache4 = ((PXSelectBase) this.Receipts).Cache;
                EPExpenseClaimDetails row4 = receipt;
                nullable = receipt.TaxRoundDiff;
                Decimal valueOrDefault7 = nullable.GetValueOrDefault();
                ref Decimal local4 = ref num4;
                PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.claimCuryInfoID>(cache4, (object) row4, valueOrDefault7, out local4);
                PXCache cache5 = ((PXSelectBase) this.Receipts).Cache;
                EPExpenseClaimDetails row5 = receipt;
                nullable = receipt.VatExemptTotal;
                Decimal valueOrDefault8 = nullable.GetValueOrDefault();
                ref Decimal local5 = ref num5;
                PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.claimCuryInfoID>(cache5, (object) row5, valueOrDefault8, out local5);
                PXCache cache6 = ((PXSelectBase) this.Receipts).Cache;
                EPExpenseClaimDetails row6 = receipt;
                nullable = receipt.VatTaxableTotal;
                Decimal valueOrDefault9 = nullable.GetValueOrDefault();
                ref Decimal local6 = ref num6;
                PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.claimCuryInfoID>(cache6, (object) row6, valueOrDefault9, out local6);
                foreach (PXResult<EPTaxTran> pxResult in PXSelectBase<EPTaxTran, PXSelect<EPTaxTran, Where<EPTaxTran.claimDetailID, Equal<Required<EPExpenseClaimDetails.claimDetailID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
                {
                  (object) receipt.ClaimDetailID
                }))
                {
                  EPTaxTran epTaxTran = PXResult<EPTaxTran>.op_Implicit(pxResult);
                  if (cach.GetStatus((object) epTaxTran) != 2)
                    cach.SetStatus((object) epTaxTran, (PXEntryStatus) 1);
                  PXCache cache7 = ((PXSelectBase) this.Receipts).Cache;
                  EPExpenseClaimDetails row7 = receipt;
                  nullable = epTaxTran.ExpenseAmt;
                  Decimal valueOrDefault10 = nullable.GetValueOrDefault();
                  Decimal num7;
                  ref Decimal local7 = ref num7;
                  PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.claimCuryInfoID>(cache7, (object) row7, valueOrDefault10, out local7);
                  cach.SetValue<EPTaxTran.claimCuryExpenseAmt>((object) epTaxTran, (object) num7);
                  PXCache cache8 = ((PXSelectBase) this.Receipts).Cache;
                  EPExpenseClaimDetails row8 = receipt;
                  nullable = epTaxTran.TaxableAmt;
                  Decimal valueOrDefault11 = nullable.GetValueOrDefault();
                  ref Decimal local8 = ref num7;
                  PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.claimCuryInfoID>(cache8, (object) row8, valueOrDefault11, out local8);
                  cach.SetValue<EPTaxTran.claimCuryTaxableAmt>((object) epTaxTran, (object) num7);
                  PXCache cache9 = ((PXSelectBase) this.Receipts).Cache;
                  EPExpenseClaimDetails row9 = receipt;
                  nullable = epTaxTran.TaxAmt;
                  Decimal valueOrDefault12 = nullable.GetValueOrDefault();
                  ref Decimal local9 = ref num7;
                  PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.claimCuryInfoID>(cache9, (object) row9, valueOrDefault12, out local9);
                  cach.SetValue<EPTaxTran.claimCuryTaxAmt>((object) epTaxTran, (object) num7);
                }
              }
            }
          }
          expenseClaimDetails = (EPExpenseClaimDetails) ((PXSelectBase) this.Receipts).Cache.CreateCopy((object) receipt);
          receipt.ClaimCuryTranAmt = new Decimal?(num1);
          receipt.ClaimCuryTranAmtWithTaxes = new Decimal?(num2);
          receipt.ClaimCuryTaxTotal = new Decimal?(num3);
          receipt.ClaimCuryTaxRoundDiff = new Decimal?(num4);
          receipt.ClaimCuryVatExemptTotal = new Decimal?(num5);
          receipt.ClaimCuryVatTaxableTotal = new Decimal?(num6);
        }
      }
    }
    return expenseClaimDetails;
  }

  public virtual void SumClaimValues(
    EPExpenseClaim claim,
    EPExpenseClaimDetails receipt,
    EPExpenseClaimDetails oldReceipt)
  {
    EPExpenseClaim copy = (EPExpenseClaim) ((PXSelectBase) this.Claim).Cache.CreateCopy((object) claim);
    SumCalc sumCalc = new SumCalc();
    Decimal? nullable1 = (Decimal?) sumCalc.Calculate<EPExpenseClaimDetails.claimCuryTranAmtWithTaxes>(((PXSelectBase) this.Receipts).Cache, (object) receipt, (object) oldReceipt, -1);
    EPExpenseClaim epExpenseClaim1 = copy;
    Decimal? nullable2 = epExpenseClaim1.CuryDocBal;
    Decimal? nullable3 = nullable1;
    epExpenseClaim1.CuryDocBal = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    EPExpenseClaim epExpenseClaim2 = copy;
    nullable3 = epExpenseClaim2.CuryTaxTotal;
    nullable2 = (Decimal?) sumCalc.Calculate<EPExpenseClaimDetails.claimCuryTaxTotal>(((PXSelectBase) this.Receipts).Cache, (object) receipt, (object) oldReceipt, -1);
    epExpenseClaim2.CuryTaxTotal = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    EPExpenseClaim epExpenseClaim3 = copy;
    nullable2 = epExpenseClaim3.CuryTaxRoundDiff;
    nullable3 = (Decimal?) sumCalc.Calculate<EPExpenseClaimDetails.claimCuryTaxRoundDiff>(((PXSelectBase) this.Receipts).Cache, (object) receipt, (object) oldReceipt, -1);
    epExpenseClaim3.CuryTaxRoundDiff = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    EPExpenseClaim epExpenseClaim4 = copy;
    nullable3 = epExpenseClaim4.CuryVatExemptTotal;
    nullable2 = (Decimal?) sumCalc.Calculate<EPExpenseClaimDetails.claimCuryVatExemptTotal>(((PXSelectBase) this.Receipts).Cache, (object) receipt, (object) oldReceipt, -1);
    epExpenseClaim4.CuryVatExemptTotal = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    EPExpenseClaim epExpenseClaim5 = copy;
    nullable2 = epExpenseClaim5.CuryVatTaxableTotal;
    nullable3 = (Decimal?) sumCalc.Calculate<EPExpenseClaimDetails.claimCuryVatTaxableTotal>(((PXSelectBase) this.Receipts).Cache, (object) receipt, (object) oldReceipt, -1);
    epExpenseClaim5.CuryVatTaxableTotal = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    this.Claim.Update(copy);
  }

  public virtual void AmtFieldUpdated(PXCache cache, PX.Data.Events.RowUpdated<EPExpenseClaimDetails> e)
  {
    if (e.Row == null)
      return;
    this.RecalcAmountInClaimCury(e.Row);
  }

  public virtual void AmtFieldUpdated(EPExpenseClaimDetails oldRow, EPExpenseClaimDetails newRow)
  {
    Decimal? curyTranAmt1 = oldRow.CuryTranAmt;
    Decimal? curyTranAmt2 = newRow.CuryTranAmt;
    if (curyTranAmt1.GetValueOrDefault() == curyTranAmt2.GetValueOrDefault() & curyTranAmt1.HasValue == curyTranAmt2.HasValue)
    {
      Decimal? tranAmtWithTaxes1 = oldRow.CuryTranAmtWithTaxes;
      Decimal? tranAmtWithTaxes2 = newRow.CuryTranAmtWithTaxes;
      if (tranAmtWithTaxes1.GetValueOrDefault() == tranAmtWithTaxes2.GetValueOrDefault() & tranAmtWithTaxes1.HasValue == tranAmtWithTaxes2.HasValue)
      {
        Decimal? tranAmtWithTaxes3 = oldRow.TranAmtWithTaxes;
        Decimal? tranAmtWithTaxes4 = newRow.TranAmtWithTaxes;
        if (tranAmtWithTaxes3.GetValueOrDefault() == tranAmtWithTaxes4.GetValueOrDefault() & tranAmtWithTaxes3.HasValue == tranAmtWithTaxes4.HasValue)
        {
          Decimal? curyTaxTotal1 = oldRow.CuryTaxTotal;
          Decimal? curyTaxTotal2 = newRow.CuryTaxTotal;
          if (curyTaxTotal1.GetValueOrDefault() == curyTaxTotal2.GetValueOrDefault() & curyTaxTotal1.HasValue == curyTaxTotal2.HasValue)
          {
            Decimal? curyTaxRoundDiff1 = oldRow.CuryTaxRoundDiff;
            Decimal? curyTaxRoundDiff2 = newRow.CuryTaxRoundDiff;
            if (curyTaxRoundDiff1.GetValueOrDefault() == curyTaxRoundDiff2.GetValueOrDefault() & curyTaxRoundDiff1.HasValue == curyTaxRoundDiff2.HasValue)
            {
              Decimal? curyVatTaxableTotal1 = oldRow.CuryVatTaxableTotal;
              Decimal? curyVatTaxableTotal2 = newRow.CuryVatTaxableTotal;
              if (curyVatTaxableTotal1.GetValueOrDefault() == curyVatTaxableTotal2.GetValueOrDefault() & curyVatTaxableTotal1.HasValue == curyVatTaxableTotal2.HasValue)
              {
                Decimal? curyVatExemptTotal1 = oldRow.CuryVatExemptTotal;
                Decimal? curyVatExemptTotal2 = newRow.CuryVatExemptTotal;
                if (curyVatExemptTotal1.GetValueOrDefault() == curyVatExemptTotal2.GetValueOrDefault() & curyVatExemptTotal1.HasValue == curyVatExemptTotal2.HasValue)
                  return;
              }
            }
          }
        }
      }
    }
    this.RecalcAmountInClaimCury(newRow);
  }

  public static object MakeSubAccountByMaskForReceipt<SubMaskField, CompanySubField, EmployeeSubField, ItemSubField, ProjectSubField, TaskSubField>(
    PXCache receiptCache,
    EPExpenseClaimDetails receipt,
    string subMask)
    where SubMaskField : IBqlField
    where CompanySubField : IBqlField
    where EmployeeSubField : IBqlField
    where ItemSubField : IBqlField
    where ProjectSubField : IBqlField
    where TaskSubField : IBqlField
  {
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select(receiptCache.Graph, new object[1]
    {
      (object) receipt.InventoryID
    }));
    PX.Objects.CR.Location location1 = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<EPExpenseClaimDetails.branchID>>>>.Config>.Select(receiptCache.Graph, new object[1]
    {
      (object) receipt
    }));
    PX.Objects.CT.Contract contract = PXResultset<PX.Objects.CT.Contract>.op_Implicit(PXSelectBase<PX.Objects.CT.Contract, PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.contractID, Equal<Required<PX.Objects.CT.Contract.contractID>>>>.Config>.Select(receiptCache.Graph, new object[1]
    {
      (object) receipt.ContractID
    }));
    PMTask dirty = PMTask.PK.FindDirty(receiptCache.Graph, receipt.ContractID, receipt.TaskID);
    PX.Objects.CR.Location location2 = (PX.Objects.CR.Location) PXSelectorAttribute.Select<EPExpenseClaimDetails.customerLocationID>(receiptCache, (object) receipt);
    EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee>.Config>.Search<EPEmployee.bAccountID>(receiptCache.Graph, (object) (int?) receipt?.EmployeeID, Array.Empty<object>()));
    int? nullable1 = (int?) receiptCache.Graph.Caches[typeof (EPEmployee)].GetValue<EmployeeSubField>((object) epEmployee);
    int? nullable2 = (int?) receiptCache.Graph.Caches[typeof (PX.Objects.IN.InventoryItem)].GetValue<ItemSubField>((object) inventoryItem);
    int? nullable3 = (int?) receiptCache.Graph.Caches[typeof (PX.Objects.CR.Location)].GetValue<CompanySubField>((object) location1);
    int? nullable4 = (int?) receiptCache.Graph.Caches[typeof (PX.Objects.CT.Contract)].GetValue<ProjectSubField>((object) contract);
    int? nullable5 = (int?) receiptCache.Graph.Caches[typeof (PMTask)].GetValue<TaskSubField>((object) dirty);
    int? nullable6 = (int?) receiptCache.Graph.Caches[typeof (PX.Objects.CR.Location)].GetValue<PX.Objects.CR.Location.cSalesSubID>((object) location2);
    return (object) SubAccountMaskAttribute.MakeSub<SubMaskField>(receiptCache.Graph, subMask, new object[6]
    {
      (object) nullable1,
      (object) nullable2,
      (object) nullable3,
      (object) nullable4,
      (object) nullable5,
      (object) nullable6
    }, new System.Type[6]
    {
      typeof (EmployeeSubField),
      typeof (ItemSubField),
      typeof (CompanySubField),
      typeof (ProjectSubField),
      typeof (TaskSubField),
      typeof (PX.Objects.CR.Location.cSalesSubID)
    });
  }

  public static void SalesSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e,
    string salesSubMask)
  {
    EPExpenseClaimDetails row = e.Row as EPExpenseClaimDetails;
    if (!row.SalesAccountID.HasValue)
      return;
    object obj = (object) null;
    if (row.Billable.GetValueOrDefault())
      obj = ExpenseClaimDetailGraphExtBase<TGraph>.MakeSubAccountByMaskForReceipt<PX.Objects.EP.EPSetup.salesSubMask, PX.Objects.CR.Location.cMPSalesSubID, EPEmployee.salesSubID, PX.Objects.IN.InventoryItem.salesSubID, PMProject.defaultSalesSubID, PMTask.defaultSalesSubID>(sender, row, salesSubMask);
    sender.RaiseFieldUpdating<EPExpenseClaimDetails.salesSubID>((object) row, ref obj);
    e.NewValue = (object) (int?) obj;
    ((CancelEventArgs) e).Cancel = true;
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public static void ExpenseSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e,
    string expenseSubMask)
  {
    PX.Objects.EP.EPSetup epSetup = (PX.Objects.EP.EPSetup) PXSelectBase<PX.Objects.EP.EPSetup, PXSelectReadonly<PX.Objects.EP.EPSetup>.Config>.SelectSingleBound(sender.Graph, (object[]) null);
    ExpenseClaimDetailGraphExtBase<TGraph>.ExpenseSubID_FieldDefaulting(sender, e, expenseSubMask, epSetup?.ExpenseSubMaskNB);
  }

  public static void ExpenseSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e,
    string expenseSubMask,
    string expenseSubMaskNB)
  {
    EPExpenseClaimDetails row = e.Row as EPExpenseClaimDetails;
    if (!row.ExpenseAccountID.HasValue)
      return;
    object newValue = row.Billable.GetValueOrDefault() ? ExpenseClaimDetailGraphExtBase<TGraph>.MakeSubAccountByMaskForReceipt<PX.Objects.EP.EPSetup.expenseSubMask, PX.Objects.CR.Location.cMPExpenseSubID, EPEmployee.expenseSubID, PX.Objects.IN.InventoryItem.cOGSSubID, PMProject.defaultExpenseSubID, PMTask.defaultExpenseSubID>(sender, row, expenseSubMask) : ExpenseClaimDetailGraphExtBase<TGraph>.MakeSubAccountByMaskForReceipt<PX.Objects.EP.EPSetup.expenseSubMaskNB, PX.Objects.CR.Location.cMPExpenseSubID, EPEmployee.expenseSubID, PX.Objects.IN.InventoryItem.cOGSSubID, PMProject.defaultExpenseSubID, PMTask.defaultExpenseSubID>(sender, row, expenseSubMaskNB);
    sender.RaiseFieldUpdating<EPExpenseClaimDetails.expenseSubID>((object) row, ref newValue);
    e.NewValue = (object) (int?) newValue;
    ((CancelEventArgs) e).Cancel = true;
  }

  public EPExpenseClaim GetParentClaim(string claimRefNbr)
  {
    return PXParentAttribute.SelectParent<EPExpenseClaim>(this.Receipts.Cache, (object) new EPExpenseClaimDetails()
    {
      RefNbr = claimRefNbr
    });
  }

  public virtual void VerifyClaimAndCorpCardCurrencies(
    int? corpCardID,
    EPExpenseClaim claim,
    System.Action substituteNewValue = null)
  {
    if (corpCardID.HasValue && claim != null && CACorpCardsMaint.GetCardCashAccount((PXGraph) ((PXGraphExtension<TGraph>) this).Base, corpCardID).CuryID != claim.CuryID)
    {
      if (substituteNewValue != null)
        substituteNewValue();
      throw new PXSetPropertyException("The corporate card currency and the claim currency must be equal.");
    }
  }

  public virtual void VerifyEmployeeAndClaimCurrenciesForCash(
    EPExpenseClaimDetails receipt,
    string receiptPaidWith,
    EPExpenseClaim claim,
    System.Action substituteNewValue = null)
  {
    if (claim == null || !(receiptPaidWith == "PersAcc"))
      return;
    EPEmployee epEmployee = (EPEmployee) PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>.Config>.Select((PXGraph) (object) ((PXGraphExtension<TGraph>) this).Base, (object) receipt.EmployeeID);
    if (!epEmployee.AllowOverrideCury.GetValueOrDefault() && epEmployee.CuryID != null && epEmployee.CuryID != claim.CuryID)
    {
      if (substituteNewValue != null)
        substituteNewValue();
      throw new PXSetPropertyException("The expense receipt cannot be added to the claim because the employee currency differs from the receipt currency and cannot be overridden. Enable currency override for the employee on the Employees (EP203000) form first.");
    }
  }

  public virtual void VerifyIsPositiveForCorpCardReceipt(string paidWith, Decimal? amount)
  {
    if (!(paidWith == "CardPers") || !amount.HasValue)
      return;
    Decimal? nullable = amount;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() < num & nullable.HasValue)
      throw new PXSetPropertyException("The amount of the expense receipt to be paid with the corporate card cannot be negative.");
  }

  public virtual void VerifyEmployeePartIsZeroForCorpCardReceipt(string paidWith, Decimal? amount)
  {
    if (!(paidWith == "CardComp") && !(paidWith == "CardPers") || !amount.HasValue)
      return;
    Decimal? nullable = amount;
    Decimal num = 0M;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      throw new PXSetPropertyException("The employee part of the expense receipt must be zero. You can submit a separate expense receipt for personal expenses paid with a corporate card.");
  }

  public virtual void VerifyEmployeePartNotExceedTotal(
    Decimal? totalAmount,
    Decimal? employeeAmount)
  {
    Decimal? nullable1 = totalAmount;
    Decimal? nullable2 = totalAmount;
    Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable4 = totalAmount;
    Decimal? nullable5 = employeeAmount;
    Decimal? nullable6 = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable7 = nullable3.HasValue & nullable6.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal num = 0M;
    if (nullable7.GetValueOrDefault() < num & nullable7.HasValue)
      throw new PXSetPropertyException("The employee part of the expense receipt must be zero. You can submit a separate expense receipt for personal expenses paid with a corporate card.");
  }

  public virtual void VerifyEmployeePartSign(Decimal? totalAmount, Decimal? employeeAmount)
  {
    Decimal? nullable1 = totalAmount;
    Decimal? nullable2 = employeeAmount;
    Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
    Decimal num = 0M;
    if (nullable3.GetValueOrDefault() < num & nullable3.HasValue)
      throw new PXSetPropertyException("The employee part must have the same sign as total amount.");
  }

  public virtual void VerifyExpenseRefNbrIsNotEmpty(EPExpenseClaimDetails receipt)
  {
    if (((PX.Objects.EP.EPSetup) ((PXSelectBase<PX.Objects.EP.EPSetup>) this.EPSetup).Select()).RequireRefNbrInExpenseReceipts.GetValueOrDefault() && string.IsNullOrEmpty(receipt.ExpenseRefNbr))
    {
      string name = typeof (EPExpenseClaimDetails.expenseRefNbr).Name;
      throw new PXRowPersistingException(name, (object) receipt.ExpenseRefNbr, "'{0}' cannot be empty.", new object[1]
      {
        (object) name
      });
    }
  }

  public ExpenseClaimDetailGraphExtBase()
    : this()
  {
  }
}

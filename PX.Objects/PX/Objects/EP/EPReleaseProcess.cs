// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPReleaseProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AP.Standalone;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.DAC.Abstract;
using PX.Objects.GL.FinPeriods;
using PX.Objects.PM;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public class EPReleaseProcess : PXGraph<EPReleaseProcess>
{
  public PXSelect<CABankTran> CABankTrans;
  public PXSelect<CABankTranMatch> CABankTranMatches;

  [PXMergeAttributes]
  [PXDBLong]
  protected virtual void CABankTran_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  public virtual void ReleaseDocProc(EPExpenseClaim claim)
  {
    ExpenseClaimEntry instance = PXGraph.CreateInstance<ExpenseClaimEntry>();
    ((PXGraph) instance).SelectTimeStamp();
    EPExpenseClaim epExpenseClaim = PXResultset<EPExpenseClaim>.op_Implicit(PXSelectBase<EPExpenseClaim, PXSelectReadonly<EPExpenseClaim, Where<EPExpenseClaim.refNbr, Equal<Required<EPExpenseClaim.refNbr>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) claim.RefNbr
    }));
    bool? nullable = !epExpenseClaim.Released.GetValueOrDefault() ? epExpenseClaim.Approved : throw new PXException("This document is already released.");
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      throw new PXException("The expense claim has to be approved by a responsible person before it can be released.");
    EPExpenseClaimDetails[] array = GraphHelper.RowCast<EPExpenseClaimDetails>((IEnumerable) PXSelectBase<EPExpenseClaimDetails, PXSelect<EPExpenseClaimDetails, Where<EPExpenseClaimDetails.refNbr, Equal<Required<EPExpenseClaim.refNbr>>, And<EPExpenseClaimDetails.released, Equal<False>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) claim.RefNbr
    })).ToArray<EPExpenseClaimDetails>();
    IFinPeriodUtils service = ((PXGraph) instance).GetService<IFinPeriodUtils>();
    if (claim.FinPeriodID != null)
      service.ValidateFinPeriod<EPExpenseClaimDetails>((IEnumerable<EPExpenseClaimDetails>) array, (Func<EPExpenseClaimDetails, string>) (m => claim.FinPeriodID), (Func<EPExpenseClaimDetails, int?[]>) (m => m.BranchID.SingleToArray<int?>()));
    List<PX.Objects.AP.APRegister> list = new List<PX.Objects.AP.APRegister>();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (((IEnumerable<EPExpenseClaimDetails>) array).Any<EPExpenseClaimDetails>())
      {
        foreach (IGrouping<string, EPExpenseClaimDetails> receiptGroup in ((IEnumerable<EPExpenseClaimDetails>) array).GroupBy<EPExpenseClaimDetails, string>((Func<EPExpenseClaimDetails, string>) (receipt => receipt.PaidWith)))
          list.AddRange((IEnumerable<PX.Objects.AP.APRegister>) this.ReleaseClaimDetailsReceipts(claim, instance, receiptGroup));
      }
      else
        list = this.ReleaseClaimDetails<PX.Objects.Common.GraphExtensions.Abstract.DAC.Invoice, InvoiceMapping, APInvoiceEntry, APInvoiceEntry.APInvoiceEntryDocumentExtension>(instance, claim, (IEnumerable<EPExpenseClaimDetails>) new EPExpenseClaimDetails[0], "PersAcc");
      transactionScope.Complete();
    }
    if (!PXResultset<EPSetup>.op_Implicit(PXSelectBase<EPSetup, PXSelectReadonly<EPSetup>.Config>.Select((PXGraph) this, Array.Empty<object>())).AutomaticReleaseAP.GetValueOrDefault())
      return;
    APDocumentRelease.ReleaseDoc(list, false);
  }

  private List<PX.Objects.AP.APRegister> ReleaseClaimDetailsReceipts(
    EPExpenseClaim claim,
    ExpenseClaimEntry expenseClaimGraph,
    IGrouping<string, EPExpenseClaimDetails> receiptGroup)
  {
    switch (receiptGroup.Key)
    {
      case "PersAcc":
        return this.ReleaseClaimDetails<PX.Objects.Common.GraphExtensions.Abstract.DAC.Invoice, InvoiceMapping, APInvoiceEntry, APInvoiceEntry.APInvoiceEntryDocumentExtension>(expenseClaimGraph, claim, (IEnumerable<EPExpenseClaimDetails>) receiptGroup, receiptGroup.Key);
      case "CardComp":
        return this.ReleaseClaimDetails<PaidInvoice, PaidInvoiceMapping, APQuickCheckEntry, APQuickCheckEntry.APQuickCheckEntryDocumentExtension>(expenseClaimGraph, claim, (IEnumerable<EPExpenseClaimDetails>) receiptGroup, receiptGroup.Key);
      case "CardPers":
        return this.ReleaseClaimDetails<PX.Objects.Common.GraphExtensions.Abstract.DAC.Invoice, InvoiceMapping, APInvoiceEntry, APInvoiceEntry.APInvoiceEntryDocumentExtension>(expenseClaimGraph, claim, (IEnumerable<EPExpenseClaimDetails>) receiptGroup, receiptGroup.Key);
      default:
        throw new NotImplementedException();
    }
  }

  public virtual List<PX.Objects.AP.APRegister> ReleaseClaimDetails<TAPDocument, TInvoiceMapping, TGraph, TAPDocumentGraphExtension>(
    ExpenseClaimEntry expenseClaimGraph,
    EPExpenseClaim claim,
    IEnumerable<EPExpenseClaimDetails> receipts,
    string receiptGroupPaidWithType)
    where TAPDocument : InvoiceBase, new()
    where TInvoiceMapping : IBqlMapping
    where TGraph : PXGraph, new()
    where TAPDocumentGraphExtension : InvoiceBaseGraphExtension<TGraph, TAPDocument, TInvoiceMapping>
  {
    TGraph instance = PXGraph.CreateInstance<TGraph>();
    EPSetup epSetup = PXResultset<EPSetup>.op_Implicit(PXSelectBase<EPSetup, PXSelectReadonly<EPSetup>.Config>.Select((PXGraph) instance, Array.Empty<object>()));
    TAPDocumentGraphExtension implementation = instance.FindImplementation<TAPDocumentGraphExtension>();
    List<List<EPExpenseClaimDetails>> expenseClaimDetailsListList = new List<List<EPExpenseClaimDetails>>();
    List<List<EPExpenseClaimDetails>> source1;
    bool? nullable1;
    switch (receiptGroupPaidWithType)
    {
      case "PersAcc":
        source1 = receipts.GroupBy(item => new
        {
          TaxZoneID = item.TaxZoneID,
          TaxCalcMode = item.TaxCalcMode
        }).Select<IGrouping<\u003C\u003Ef__AnonymousType46<string, string>, EPExpenseClaimDetails>, List<EPExpenseClaimDetails>>(group => group.ToList<EPExpenseClaimDetails>()).ToList<List<EPExpenseClaimDetails>>();
        break;
      case "CardComp":
        nullable1 = epSetup.PostSummarizedCorpCardExpenseReceipts;
        source1 = !nullable1.GetValueOrDefault() ? receipts.Select<EPExpenseClaimDetails, List<EPExpenseClaimDetails>>((Func<EPExpenseClaimDetails, List<EPExpenseClaimDetails>>) (receipt => receipt.SingleToList<EPExpenseClaimDetails>())).ToList<List<EPExpenseClaimDetails>>() : receipts.GroupBy(item => new
        {
          TaxZoneID = item.TaxZoneID,
          TaxCalcMode = item.TaxCalcMode,
          CorpCardID = item.CorpCardID,
          ExpenseDate = item.ExpenseDate,
          ExpenseRefNbr = item.ExpenseRefNbr
        }).Select<IGrouping<\u003C\u003Ef__AnonymousType47<string, string, int?, DateTime?, string>, EPExpenseClaimDetails>, List<EPExpenseClaimDetails>>(group => group.ToList<EPExpenseClaimDetails>()).ToList<List<EPExpenseClaimDetails>>();
        break;
      case "CardPers":
        source1 = new List<List<EPExpenseClaimDetails>>()
        {
          receipts.ToList<EPExpenseClaimDetails>()
        };
        break;
      default:
        throw new NotImplementedException();
    }
    if (!source1.Any<List<EPExpenseClaimDetails>>())
      source1.Add(new List<EPExpenseClaimDetails>());
    PXResultset<APSetup>.op_Implicit(PXSelectBase<APSetup, PXSelectReadonly<APSetup>.Config>.Select((PXGraph) instance, Array.Empty<object>()));
    EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Required<EPExpenseClaim.employeeID>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) claim.EmployeeID
    }));
    PX.Objects.CR.Location location1 = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<EPExpenseClaim.employeeID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<EPExpenseClaim.locationID>>>>>.Config>.Select((PXGraph) instance, new object[2]
    {
      (object) claim.EmployeeID,
      (object) claim.LocationID
    }));
    using (new PXReadDeletedScope(false))
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PX.Objects.GL.Account>(new PXDataField[3]
      {
        (PXDataField) new PXDataField<PX.Objects.GL.Account.accountCD>(),
        new PXDataField("DeletedDatabaseRecord"),
        (PXDataField) new PXDataFieldValue<PX.Objects.GL.Account.accountID>((PXDbType) 8, (object) location1.VAPAccountID)
      }))
      {
        if (pxDataRecord != null)
        {
          if (pxDataRecord != null)
          {
            nullable1 = pxDataRecord.GetBoolean(1);
            if (!nullable1.GetValueOrDefault())
              goto label_19;
          }
          else
            goto label_19;
        }
        throw new PXException("The {0} expense claim cannot be released because the {1} account specified in the employee details has been deleted. To be able to release the document, select another account for the {2} employee in the AP Account box on the Financial Settings tab of the Employees (EP203000) form.", new object[3]
        {
          (object) claim.RefNbr,
          pxDataRecord != null ? (object) pxDataRecord.GetString(0) : (object) string.Empty,
          (object) epEmployee.AcctCD
        });
      }
    }
label_19:
    List<PX.Objects.AP.APRegister> apRegisterList = new List<PX.Objects.AP.APRegister>();
    if (claim.FinPeriodID != null)
      this.FinPeriodUtils.ValidateFinPeriod((IEnumerable<IAccountable>) claim.SingleToArray<EPExpenseClaim>());
    foreach (List<EPExpenseClaimDetails> source2 in source1)
    {
      if (receiptGroupPaidWithType == "CardComp" && source2.Count > 1)
      {
        EPExpenseClaimDetails[] array = source2.Where<EPExpenseClaimDetails>((Func<EPExpenseClaimDetails, bool>) (receipt => receipt.BankTranDate.HasValue)).Take<EPExpenseClaimDetails>(11).ToArray<EPExpenseClaimDetails>();
        if (((IEnumerable<EPExpenseClaimDetails>) array).Any<EPExpenseClaimDetails>())
          throw new PXException("The following expense receipts cannot be summarized because they have been already matched to bank statement transactions: {0}. Please unmatch the expense receipts on the Import Bank Transactions (CA306500) form first.", new object[1]
          {
            (object) ((ICollection<string>) ((IEnumerable<PXResult<EPExpenseClaimDetails, CABankTranMatch, CABankTran>>) ((IEnumerable<PXResult<EPExpenseClaimDetails>>) PXSelectBase<EPExpenseClaimDetails, PXSelectJoin<EPExpenseClaimDetails, InnerJoin<CABankTranMatch, On<CABankTranMatch.docModule, Equal<BatchModule.moduleEP>, And<CABankTranMatch.docType, Equal<EPExpenseClaimDetails.docType>, And<CABankTranMatch.docRefNbr, Equal<EPExpenseClaimDetails.claimDetailCD>>>>, InnerJoin<CABankTran, On<CABankTran.tranID, Equal<CABankTranMatch.tranID>>>>, Where<EPExpenseClaimDetails.claimDetailCD, In<Required<EPExpenseClaimDetails.claimDetailCD>>>>.Config>.Select((PXGraph) expenseClaimGraph, (object[]) ((IEnumerable<EPExpenseClaimDetails>) array).Select<EPExpenseClaimDetails, string>((Func<EPExpenseClaimDetails, string>) (receipt => receipt.ClaimDetailCD)).ToArray<string>())).AsEnumerable<PXResult<EPExpenseClaimDetails>>().Cast<PXResult<EPExpenseClaimDetails, CABankTranMatch, CABankTran>>().ToArray<PXResult<EPExpenseClaimDetails, CABankTranMatch, CABankTran>>()).Select<PXResult<EPExpenseClaimDetails, CABankTranMatch, CABankTran>, string>((Func<PXResult<EPExpenseClaimDetails, CABankTranMatch, CABankTran>, string>) (row => $"{PXMessages.LocalizeNoPrefix("Receipt")} {PXResult<EPExpenseClaimDetails, CABankTranMatch, CABankTran>.op_Implicit(row).ClaimDetailCD} - {PXResult<EPExpenseClaimDetails, CABankTranMatch, CABankTran>.op_Implicit(row).GetFriendlyKeyImage(((PXGraph) this).Caches[typeof (CABankTran)])}")).ToArray<string>()).JoinIntoStringForMessageNoQuotes<string>(10)
          });
      }
      instance.Clear((PXClearOption) 3);
      instance.SelectTimeStamp();
      ((PXSelectBase<Contragent>) implementation.Contragent).Current = ((PXSelectBase) implementation.Contragent).Cache.GetExtension<Contragent>((object) epEmployee);
      implementation.Location.Current = location1;
      PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<EPExpenseClaim.curyInfoID>>>>.Config>.Select((PXGraph) instance, new object[1]
      {
        (object) claim.CuryInfoID
      }));
      if (currencyInfo != null && !currencyInfo.CuryPrecision.HasValue)
      {
        object obj;
        ((PXCache) GraphHelper.Caches<PX.Objects.CM.CurrencyInfo>((PXGraph) instance)).RaiseFieldDefaulting<PX.Objects.CM.CurrencyInfo.curyPrecision>((object) currencyInfo, ref obj);
        currencyInfo.CuryPrecision = obj as short?;
      }
      PX.Objects.CM.CurrencyInfo copy1 = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(currencyInfo);
      copy1.CuryInfoID = new long?();
      PX.Objects.CM.CurrencyInfo cm = implementation.CurrencyInfo.Insert(PX.Objects.CM.Extensions.CurrencyInfo.GetEX(copy1)).GetCM();
      TAPDocument apDocument = new TAPDocument();
      CABankTranMatch bankTranMatch = (CABankTranMatch) null;
      Decimal? nullable2;
      switch (receiptGroupPaidWithType)
      {
        case "PersAcc":
          // ISSUE: variable of a boxed type
          __Boxed<TAPDocument> local1 = (object) apDocument;
          nullable2 = source2.Sum<EPExpenseClaimDetails>((Func<EPExpenseClaimDetails, Decimal?>) (_ => _.ClaimCuryTranAmtWithTaxes));
          Decimal num1 = 0M;
          string str1 = nullable2.GetValueOrDefault() < num1 & nullable2.HasValue ? "ADR" : "INV";
          local1.DocType = str1;
          break;
        case "CardComp":
          EPExpenseClaimDetails expenseClaimDetails1 = source2.First<EPExpenseClaimDetails>();
          // ISSUE: variable of a boxed type
          __Boxed<TAPDocument> local2 = (object) apDocument;
          nullable2 = source2.Sum<EPExpenseClaimDetails>((Func<EPExpenseClaimDetails, Decimal?>) (_ => _.ClaimCuryTranAmtWithTaxes));
          Decimal num2 = 0M;
          string str2 = nullable2.GetValueOrDefault() < num2 & nullable2.HasValue ? "RQC" : "QCK";
          local2.DocType = str2;
          CACorpCard caCorpCard = CACorpCard.PK.Find((PXGraph) this, expenseClaimDetails1.CorpCardID);
          PaymentMethodAccount paymentMethodAccount = PXResultset<PaymentMethodAccount>.op_Implicit(PXSelectBase<PaymentMethodAccount, PXSelect<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Required<PaymentMethodAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) caCorpCard.CashAccountID
          }));
          apDocument.CashAccountID = caCorpCard.CashAccountID;
          apDocument.PaymentMethodID = paymentMethodAccount.PaymentMethodID;
          apDocument.ExtRefNbr = expenseClaimDetails1.ExpenseRefNbr;
          if (source2.Count == 1)
          {
            bankTranMatch = PXResultset<CABankTranMatch>.op_Implicit(PXSelectBase<CABankTranMatch, PXSelect<CABankTranMatch, Where<CABankTranMatch.docModule, Equal<BatchModule.moduleEP>, And<CABankTranMatch.docType, Equal<EPExpenseClaimDetails.docType>, And<CABankTranMatch.docRefNbr, Equal<Required<CABankTranMatch.docRefNbr>>>>>>.Config>.Select((PXGraph) expenseClaimGraph, new object[1]
            {
              (object) expenseClaimDetails1.ClaimDetailCD
            }));
            if (bankTranMatch != null)
            {
              CABankTran caBankTran = CABankTran.PK.Find((PXGraph) expenseClaimGraph, bankTranMatch.TranID);
              apDocument.ClearDate = caBankTran.ClearDate;
              apDocument.Cleared = new bool?(true);
              break;
            }
            break;
          }
          break;
        case "CardPers":
          apDocument.DocType = "ADR";
          break;
        default:
          throw new NotImplementedException();
      }
      apDocument.CuryInfoID = cm.CuryInfoID;
      apDocument.Hold = new bool?(true);
      apDocument.Released = new bool?(false);
      apDocument.Printed = new bool?(apDocument.DocType == "QCK" || apDocument.DocType == "RQC");
      apDocument.OpenDoc = new bool?(true);
      apDocument.HeaderDocDate = claim.DocDate;
      apDocument.FinPeriodID = claim.FinPeriodID;
      apDocument.InvoiceNbr = claim.RefNbr;
      apDocument.DocDesc = claim.DocDesc;
      apDocument.ContragentID = claim.EmployeeID;
      apDocument.CuryID = cm.CuryID;
      apDocument.ContragentLocationID = claim.LocationID;
      apDocument.ModuleAccountID = (int?) location1?.APAccountID;
      apDocument.ModuleSubID = (int?) location1?.APSubID;
      apDocument.TaxZoneID = source2.Any<EPExpenseClaimDetails>() ? source2.First<EPExpenseClaimDetails>().TaxZoneID : claim.TaxZoneID;
      apDocument.BranchID = claim.BranchID;
      apDocument.OrigModule = "EP";
      if (receiptGroupPaidWithType == "CardComp" && source2.Count == 1)
      {
        apDocument.OrigDocType = "ECD";
        apDocument.OrigRefNbr = source2.Single<EPExpenseClaimDetails>().ClaimDetailCD;
      }
      else
      {
        apDocument.OrigDocType = "ECL";
        apDocument.OrigRefNbr = claim.RefNbr;
      }
      bool flag1 = apDocument.DocType == "ADR" && receiptGroupPaidWithType == "PersAcc" || apDocument.DocType == "RQC";
      Decimal signOperation = (Decimal) (flag1 ? -1 : 1);
      apDocument.HeaderTranPeriodID = claim.FinPeriodID;
      apDocument.HeaderFinPeriodID = claim.FinPeriodID;
      TAPDocument invoice1 = ((PXSelectBase<TAPDocument>) implementation.Documents).Insert(apDocument);
      this.HandleInvoiceInMultiBaseCurrency<TAPDocument, TGraph>(instance, invoice1);
      if (((PXSelectBase) implementation.Documents).Cache is PXModelExtension<TAPDocument> cache1)
        cache1.UpdateExtensionMapping((object) invoice1, (MappingSyncDirection) 1);
      invoice1.BranchID = claim.BranchID;
      invoice1.TaxCalcMode = source2.Any<EPExpenseClaimDetails>() ? source2.First<EPExpenseClaimDetails>().TaxCalcMode : claim.TaxCalcMode;
      TAPDocument invoice2 = ((PXSelectBase<TAPDocument>) implementation.Documents).Update(invoice1);
      PXCache<PX.Objects.CM.CurrencyInfo>.RestoreCopy(cm, currencyInfo);
      cm.CuryInfoID = invoice2.CuryInfoID;
      PXCache cach1 = instance.Caches[typeof (EPExpenseClaim)];
      PXCache cach2 = instance.Caches[typeof (EPExpenseClaimDetails)];
      EPExpenseClaim epExpenseClaim = claim;
      PXCache cache2 = ((PXSelectBase) implementation.Documents).Cache;
      // ISSUE: variable of a boxed type
      __Boxed<TAPDocument> local3 = (object) invoice2;
      PXNoteAttribute.IPXCopySettings copyNoteSettings = epSetup.GetCopyNoteSettings<PXModule.ap>();
      PXNoteAttribute.CopyNoteAndFiles(cach1, (object) epExpenseClaim, cache2, (object) local3, copyNoteSettings);
      TaxBaseAttribute.SetTaxCalc<InvoiceTran.taxCategoryID>(((PXSelectBase) implementation.InvoiceTrans).Cache, (object) null, TaxCalc.ManualCalc);
      Decimal? nullable3 = new Decimal?(0M);
      Decimal? nullable4 = new Decimal?(0M);
      Decimal? nullable5;
      foreach (EPExpenseClaimDetails claimdetail in source2)
      {
        int num3 = flag1 ? 1 : 0;
        nullable2 = claimdetail.ClaimCuryTranAmtWithTaxes;
        Decimal num4 = 0M;
        int num5 = nullable2.GetValueOrDefault() < num4 & nullable2.HasValue ? 1 : 0;
        Decimal num6 = num3 != num5 ? -1M : 1M;
        PX.Objects.CT.Contract contract = PXResultset<PX.Objects.CT.Contract>.op_Implicit(PXSelectBase<PX.Objects.CT.Contract, PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.contractID, Equal<Required<EPExpenseClaimDetails.contractID>>>>.Config>.SelectSingleBound((PXGraph) instance, (object[]) null, new object[1]
        {
          (object) claimdetail.ContractID
        }));
        PX.Objects.IN.InventoryItem inventoryItem1 = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<EPExpenseClaimDetails.inventoryID>>>>.Config>.SelectSingleBound((PXGraph) instance, (object[]) null, new object[1]
        {
          (object) claimdetail.InventoryID
        }));
        if (claimdetail.TaskID.HasValue)
        {
          PMTask dirty = PMTask.PK.FindDirty((PXGraph) expenseClaimGraph, claimdetail.ContractID, claimdetail.TaskID);
          if (dirty != null && !dirty.VisibleInAP.Value)
            throw new PXException("Project Task '{0}' is invisible in {1} module.", new object[2]
            {
              (object) dirty.TaskCD,
              (object) "AP"
            });
        }
        InvoiceTran tran1 = new InvoiceTran();
        tran1.InventoryID = claimdetail.InventoryID;
        tran1.TranDesc = claimdetail.TranDesc;
        Decimal num7;
        Decimal num8;
        Decimal num9;
        Decimal num10;
        if (CurrencyHelper.IsSameCury((PXGraph) expenseClaimGraph, claimdetail.CuryInfoID, claimdetail.ClaimCuryInfoID))
        {
          nullable2 = claimdetail.CuryUnitCost;
          num7 = nullable2.GetValueOrDefault();
          nullable2 = claimdetail.CuryTaxableAmt;
          num8 = nullable2.GetValueOrDefault();
          nullable2 = claimdetail.CuryTaxableAmtFromTax;
          num9 = nullable2.GetValueOrDefault();
          nullable2 = claimdetail.CuryTaxAmt;
          num10 = nullable2.GetValueOrDefault();
        }
        else
        {
          nullable2 = claimdetail.CuryUnitCost;
          if (nullable2.HasValue)
          {
            nullable2 = claimdetail.CuryUnitCost;
            Decimal num11 = 0M;
            if (!(nullable2.GetValueOrDefault() == num11 & nullable2.HasValue))
            {
              PXCache cache3 = ((PXSelectBase) expenseClaimGraph.ExpenseClaimDetails).Cache;
              EPExpenseClaimDetails row = claimdetail;
              nullable2 = claimdetail.UnitCost;
              Decimal baseval = nullable2.Value;
              ref Decimal local4 = ref num7;
              PX.Objects.CM.PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.claimCuryInfoID>(cache3, (object) row, baseval, out local4);
              goto label_51;
            }
          }
          num7 = 0M;
label_51:
          nullable2 = claimdetail.CuryTaxableAmt;
          if (nullable2.HasValue)
          {
            nullable2 = claimdetail.CuryTaxableAmt;
            Decimal num12 = 0M;
            if (!(nullable2.GetValueOrDefault() == num12 & nullable2.HasValue))
            {
              PXCache cache4 = ((PXSelectBase) expenseClaimGraph.ExpenseClaimDetails).Cache;
              EPExpenseClaimDetails row = claimdetail;
              nullable2 = claimdetail.TaxableAmt;
              Decimal baseval = nullable2.Value;
              ref Decimal local5 = ref num8;
              PX.Objects.CM.PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.claimCuryInfoID>(cache4, (object) row, baseval, out local5);
              goto label_55;
            }
          }
          num8 = 0M;
label_55:
          nullable2 = claimdetail.CuryTaxableAmtFromTax;
          if (nullable2.HasValue)
          {
            nullable2 = claimdetail.CuryTaxableAmtFromTax;
            Decimal num13 = 0M;
            if (!(nullable2.GetValueOrDefault() == num13 & nullable2.HasValue))
            {
              PXCache cache5 = ((PXSelectBase) expenseClaimGraph.ExpenseClaimDetails).Cache;
              EPExpenseClaimDetails row = claimdetail;
              nullable2 = claimdetail.TaxableAmtFromTax;
              Decimal baseval = nullable2.Value;
              ref Decimal local6 = ref num9;
              PX.Objects.CM.PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.claimCuryInfoID>(cache5, (object) row, baseval, out local6);
              goto label_59;
            }
          }
          num9 = 0M;
label_59:
          nullable2 = claimdetail.CuryTaxAmt;
          if (nullable2.HasValue)
          {
            nullable2 = claimdetail.CuryTaxAmt;
            Decimal num14 = 0M;
            if (!(nullable2.GetValueOrDefault() == num14 & nullable2.HasValue))
            {
              PXCache cache6 = ((PXSelectBase) expenseClaimGraph.ExpenseClaimDetails).Cache;
              EPExpenseClaimDetails row = claimdetail;
              nullable2 = claimdetail.TaxAmt;
              Decimal baseval = nullable2.Value;
              ref Decimal local7 = ref num10;
              PX.Objects.CM.PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.claimCuryInfoID>(cache6, (object) row, baseval, out local7);
              goto label_63;
            }
          }
          num10 = 0M;
        }
label_63:
        tran1.ManualPrice = new bool?(true);
        tran1.CuryUnitCost = new Decimal?(num7);
        InvoiceTran invoiceTran1 = tran1;
        nullable2 = claimdetail.Qty;
        Decimal num15 = signOperation;
        Decimal? nullable6;
        if (!nullable2.HasValue)
        {
          nullable5 = new Decimal?();
          nullable6 = nullable5;
        }
        else
          nullable6 = new Decimal?(nullable2.GetValueOrDefault() * num15);
        invoiceTran1.Qty = nullable6;
        tran1.UOM = claimdetail.UOM;
        tran1.NonBillable = new bool?(!claimdetail.Billable.GetValueOrDefault());
        nullable2 = nullable3;
        nullable5 = claimdetail.ClaimCuryTaxRoundDiff;
        Decimal num16 = nullable5.GetValueOrDefault() * signOperation;
        Decimal? nullable7;
        if (!nullable2.HasValue)
        {
          nullable5 = new Decimal?();
          nullable7 = nullable5;
        }
        else
          nullable7 = new Decimal?(nullable2.GetValueOrDefault() + num16);
        nullable3 = nullable7;
        nullable2 = nullable4;
        nullable5 = claimdetail.ClaimTaxRoundDiff;
        Decimal num17 = nullable5.GetValueOrDefault() * signOperation;
        Decimal? nullable8;
        if (!nullable2.HasValue)
        {
          nullable5 = new Decimal?();
          nullable8 = nullable5;
        }
        else
          nullable8 = new Decimal?(nullable2.GetValueOrDefault() + num17);
        nullable4 = nullable8;
        tran1.Date = claimdetail.ExpenseDate;
        tran1.ProjectID = !(contract.BaseType == "P") ? ProjectDefaultAttribute.NonProject() : claimdetail.ContractID;
        tran1.TaskID = claimdetail.TaskID;
        tran1.CostCodeID = claimdetail.CostCodeID;
        if (receiptGroupPaidWithType == "CardPers")
        {
          PX.Objects.CA.CashAccount cashAccount = PX.Objects.CA.CashAccount.PK.Find((PXGraph) this, CACorpCard.PK.Find((PXGraph) this, claimdetail.CorpCardID).CashAccountID);
          tran1.AccountID = cashAccount.AccountID;
          tran1.SubID = cashAccount.SubID;
        }
        else
        {
          tran1.AccountID = claimdetail.ExpenseAccountID;
          tran1.SubID = claimdetail.ExpenseSubID;
        }
        tran1.BranchID = claimdetail.BranchID;
        InvoiceTran tran2 = this.InsertInvoiceTransaction(((PXSelectBase) implementation.InvoiceTrans).Cache, tran1, new EPReleaseProcess.InvoiceTranContext()
        {
          EPClaim = claim,
          EPClaimDetails = claimdetail
        });
        claimdetail.APLineNbr = tran2.LineNbr;
        if (inventoryItem1.StkItem.HasValue)
        {
          bool? stkItem = inventoryItem1.StkItem;
          bool flag2 = false;
          if (!(stkItem.GetValueOrDefault() == flag2 & stkItem.HasValue))
            goto label_78;
        }
        ((APTran) tran2.Base).AccrueCost = new bool?(false);
label_78:
        tran2.CuryLineAmt = new Decimal?(num8 * signOperation);
        tran2.CuryTaxAmt = new Decimal?(0M);
        tran2.CuryTaxableAmt = new Decimal?(num9 * signOperation);
        tran2.CuryTaxAmt = new Decimal?(num10 * signOperation);
        tran2.TaxCategoryID = claimdetail.TaxCategoryID;
        InvoiceTran tran3 = this.UpdateInvoiceTransaction(((PXSelectBase) implementation.InvoiceTrans).Cache, tran2, new EPReleaseProcess.InvoiceTranContext()
        {
          EPClaim = claim,
          EPClaimDetails = claimdetail
        });
        nullable2 = claimdetail.CuryTipAmt;
        if (nullable2.GetValueOrDefault() != 0M)
        {
          InvoiceTran tran4 = new InvoiceTran();
          if (!epSetup.NonTaxableTipItem.HasValue)
            throw new PXException("To be able to specify a nonzero tip amount in the expense receipt, specify an appropriate tip item in the Non-Taxable Tip Item box on the Time & Expenses Preferences (EP101000) form.");
          PX.Objects.IN.InventoryItem inventoryItem2 = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) instance, new object[1]
          {
            (object) epSetup.NonTaxableTipItem
          }));
          if (inventoryItem2 == null)
            throw new PXException("To be able to specify a nonzero tip amount in the expense receipt, specify an appropriate tip item in the Non-Taxable Tip Item box on the Time & Expenses Preferences (EP101000) form.");
          tran4.InventoryID = epSetup.NonTaxableTipItem;
          tran4.TranDesc = inventoryItem2.Descr;
          if (CurrencyHelper.IsSameCury((PXGraph) expenseClaimGraph, claimdetail.CuryInfoID, claimdetail.ClaimCuryInfoID))
          {
            InvoiceTran invoiceTran2 = tran4;
            nullable2 = claimdetail.CuryTipAmt;
            Decimal? nullable9 = new Decimal?(Math.Abs(nullable2.GetValueOrDefault()));
            invoiceTran2.CuryUnitCost = nullable9;
            InvoiceTran invoiceTran3 = tran4;
            nullable2 = claimdetail.CuryTipAmt;
            Decimal num18 = signOperation;
            Decimal? nullable10;
            if (!nullable2.HasValue)
            {
              nullable5 = new Decimal?();
              nullable10 = nullable5;
            }
            else
              nullable10 = new Decimal?(nullable2.GetValueOrDefault() * num18);
            invoiceTran3.CuryTranAmt = nullable10;
          }
          else
          {
            PXCache cache7 = ((PXSelectBase) expenseClaimGraph.ExpenseClaimDetails).Cache;
            EPExpenseClaimDetails row = claimdetail;
            nullable2 = claimdetail.TipAmt;
            Decimal baseval = nullable2.Value;
            Decimal num19;
            ref Decimal local8 = ref num19;
            PX.Objects.CM.PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.claimCuryInfoID>(cache7, (object) row, baseval, out local8);
            tran4.CuryUnitCost = new Decimal?(Math.Abs(num19));
            tran4.CuryTranAmt = new Decimal?(num19 * signOperation);
          }
          tran4.Qty = new Decimal?(num6);
          tran4.UOM = inventoryItem2.BaseUnit;
          tran4.NonBillable = new bool?(!claimdetail.Billable.GetValueOrDefault());
          tran4.Date = claimdetail.ExpenseDate;
          tran4.BranchID = claimdetail.BranchID;
          InvoiceTran tran5 = this.InsertInvoiceTipTransaction(((PXSelectBase) implementation.InvoiceTrans).Cache, tran4, new EPReleaseProcess.InvoiceTranContext()
          {
            EPClaim = claim,
            EPClaimDetails = claimdetail
          });
          if (epSetup.UseReceiptAccountForTips.GetValueOrDefault())
          {
            tran5.AccountID = claimdetail.ExpenseAccountID;
            tran5.SubID = claimdetail.ExpenseSubID;
          }
          else
          {
            tran5.AccountID = inventoryItem2.COGSAcctID;
            PX.Objects.CR.Location location2 = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<PX.Objects.AP.APInvoice.branchID>>>>.Config>.Select((PXGraph) instance, Array.Empty<object>()));
            PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.Select((PXGraph) instance, new object[2]
            {
              (object) claimdetail.ContractID,
              (object) claimdetail.TaskID
            }));
            PX.Objects.CR.Location location3 = (PX.Objects.CR.Location) PXSelectorAttribute.Select<EPExpenseClaimDetails.customerLocationID>(cach2, (object) claimdetail);
            int? nullable11 = (int?) instance.Caches[typeof (EPEmployee)].GetValue<EPEmployee.expenseSubID>((object) epEmployee);
            int? nullable12 = (int?) instance.Caches[typeof (PX.Objects.IN.InventoryItem)].GetValue<PX.Objects.IN.InventoryItem.cOGSSubID>((object) inventoryItem2);
            int? nullable13 = (int?) instance.Caches[typeof (PX.Objects.CR.Location)].GetValue<PX.Objects.CR.Location.cMPExpenseSubID>((object) location2);
            int? nullable14 = (int?) instance.Caches[typeof (PX.Objects.CT.Contract)].GetValue<PX.Objects.CT.Contract.defaultExpenseSubID>((object) contract);
            int? nullable15 = (int?) instance.Caches[typeof (PMTask)].GetValue<PMTask.defaultExpenseSubID>((object) pmTask);
            int? nullable16 = (int?) instance.Caches[typeof (PX.Objects.CR.Location)].GetValue<PX.Objects.CR.Location.cSalesSubID>((object) location3);
            object obj = (object) SubAccountMaskAttribute.MakeSub<EPSetup.expenseSubMask>((PXGraph) instance, epSetup.ExpenseSubMask, new object[6]
            {
              (object) nullable11,
              (object) nullable12,
              (object) nullable13,
              (object) nullable14,
              (object) nullable15,
              (object) nullable16
            }, new System.Type[6]
            {
              typeof (EPEmployee.expenseSubID),
              typeof (PX.Objects.IN.InventoryItem.cOGSSubID),
              typeof (PX.Objects.CR.Location.cMPExpenseSubID),
              typeof (PX.Objects.CT.Contract.defaultExpenseSubID),
              typeof (PMTask.defaultExpenseSubID),
              typeof (PX.Objects.CR.Location.cSalesSubID)
            });
            instance.Caches[typeof (APTran)].RaiseFieldUpdating<APTran.subID>((object) tran5, ref obj);
            tran5.SubID = (int?) obj;
          }
          InvoiceTran tran6 = this.UpdateInvoiceTipTransactionAccounts(((PXSelectBase) implementation.InvoiceTrans).Cache, tran5, new EPReleaseProcess.InvoiceTranContext()
          {
            EPClaim = claim,
            EPClaimDetails = claimdetail
          });
          tran6.TaxCategoryID = inventoryItem2.TaxCategoryID;
          tran6.ProjectID = tran3.ProjectID;
          tran6.TaskID = tran3.TaskID;
          tran6.CostCodeID = tran3.CostCodeID;
          InvoiceTran tran7 = this.AddTaxes<TAPDocument, TInvoiceMapping, TGraph, TAPDocumentGraphExtension>(implementation, instance, expenseClaimGraph, invoice2, signOperation, claimdetail, tran6, true);
          this.UpdateInvoiceTipTransactionTaxesAndProject(((PXSelectBase) implementation.InvoiceTrans).Cache, tran7, new EPReleaseProcess.InvoiceTranContext()
          {
            EPClaim = claim,
            EPClaimDetails = claimdetail
          });
        }
        PXNoteAttribute.CopyNoteAndFiles(cach2, (object) claimdetail, ((PXSelectBase) implementation.InvoiceTrans).Cache, (object) tran3, epSetup.GetCopyNoteSettings<PXModule.ap>());
        claimdetail.Released = new bool?(true);
        GraphHelper.MarkUpdated(((PXSelectBase) expenseClaimGraph.ExpenseClaimDetails).Cache, (object) claimdetail);
        if (receiptGroupPaidWithType != "CardPers")
          this.AddTaxes<TAPDocument, TInvoiceMapping, TGraph, TAPDocumentGraphExtension>(implementation, instance, expenseClaimGraph, invoice2, signOperation, claimdetail, tran3, false);
      }
      foreach (PXResult<EPTaxAggregate> pxResult in PXSelectBase<EPTaxAggregate, PXSelectReadonly<EPTaxAggregate, Where<EPTaxAggregate.refNbr, Equal<Required<EPExpenseClaim.refNbr>>>>.Config>.Select((PXGraph) instance, new object[1]
      {
        (object) claim.RefNbr
      }))
      {
        EPTaxAggregate epTaxAggregate = PXResult<EPTaxAggregate>.op_Implicit(pxResult);
        GenericTaxTran genericTaxTran1 = PXResultset<GenericTaxTran>.op_Implicit(((PXSelectBase<GenericTaxTran>) implementation.TaxTrans).Search<GenericTaxTran.taxID>((object) epTaxAggregate.TaxID, Array.Empty<object>()));
        if (genericTaxTran1 == null)
        {
          genericTaxTran1 = ((PXSelectBase<GenericTaxTran>) implementation.TaxTrans).Insert(new GenericTaxTran()
          {
            TaxID = epTaxAggregate.TaxID
          });
          if (genericTaxTran1 != null)
          {
            GenericTaxTran copy2 = (GenericTaxTran) ((PXSelectBase) implementation.TaxTrans).Cache.CreateCopy((object) genericTaxTran1);
            copy2.CuryTaxableAmt = new Decimal?(0M);
            copy2.CuryTaxAmt = new Decimal?(0M);
            copy2.CuryExpenseAmt = new Decimal?(0M);
            genericTaxTran1 = ((PXSelectBase<GenericTaxTran>) implementation.TaxTrans).Update(copy2);
          }
        }
        if (genericTaxTran1 != null)
        {
          GenericTaxTran copy3 = (GenericTaxTran) ((PXSelectBase) implementation.TaxTrans).Cache.CreateCopy((object) genericTaxTran1);
          copy3.TaxRate = epTaxAggregate.TaxRate;
          GenericTaxTran genericTaxTran2 = copy3;
          Decimal? nullable17 = copy3.CuryTaxableAmt;
          Decimal valueOrDefault1 = nullable17.GetValueOrDefault();
          nullable17 = epTaxAggregate.CuryTaxableAmt;
          Decimal num20 = signOperation;
          Decimal? nullable18 = nullable17.HasValue ? new Decimal?(nullable17.GetValueOrDefault() * num20) : new Decimal?();
          Decimal? nullable19;
          if (!nullable18.HasValue)
          {
            nullable17 = new Decimal?();
            nullable19 = nullable17;
          }
          else
            nullable19 = new Decimal?(valueOrDefault1 + nullable18.GetValueOrDefault());
          genericTaxTran2.CuryTaxableAmt = nullable19;
          GenericTaxTran genericTaxTran3 = copy3;
          nullable17 = copy3.CuryTaxAmt;
          Decimal valueOrDefault2 = nullable17.GetValueOrDefault();
          nullable17 = epTaxAggregate.CuryTaxAmt;
          Decimal num21 = signOperation;
          nullable18 = nullable17.HasValue ? new Decimal?(nullable17.GetValueOrDefault() * num21) : new Decimal?();
          Decimal? nullable20;
          if (!nullable18.HasValue)
          {
            nullable17 = new Decimal?();
            nullable20 = nullable17;
          }
          else
            nullable20 = new Decimal?(valueOrDefault2 + nullable18.GetValueOrDefault());
          genericTaxTran3.CuryTaxAmt = nullable20;
          GenericTaxTran genericTaxTran4 = copy3;
          nullable17 = copy3.CuryExpenseAmt;
          Decimal valueOrDefault3 = nullable17.GetValueOrDefault();
          nullable17 = epTaxAggregate.CuryExpenseAmt;
          Decimal num22 = signOperation;
          nullable18 = nullable17.HasValue ? new Decimal?(nullable17.GetValueOrDefault() * num22) : new Decimal?();
          Decimal? nullable21;
          if (!nullable18.HasValue)
          {
            nullable17 = new Decimal?();
            nullable21 = nullable17;
          }
          else
            nullable21 = new Decimal?(valueOrDefault3 + nullable18.GetValueOrDefault());
          genericTaxTran4.CuryExpenseAmt = nullable21;
          ((PXSelectBase<GenericTaxTran>) implementation.TaxTrans).Update(copy3);
        }
      }
      invoice2.CuryOrigDocAmt = invoice2.CuryDocBal;
      invoice2.CuryTaxAmt = invoice2.CuryTaxTotal;
      invoice2.Hold = new bool?(false);
      implementation.SuppressApproval();
      ((PXSelectBase<TAPDocument>) implementation.Documents).Update(invoice2);
      if (receiptGroupPaidWithType != "CardPers")
      {
        invoice2.CuryTaxRoundDiff = invoice2.CuryRoundDiff = invoice2.CuryRoundDiff = nullable3;
        invoice2.TaxRoundDiff = invoice2.RoundDiff = nullable4;
        bool flag3 = PXSelectBase<APTaxTran, PXSelectJoin<APTaxTran, InnerJoin<PX.Objects.TX.Tax, On<APTaxTran.taxID, Equal<PX.Objects.TX.Tax.taxID>>>, Where<APTaxTran.refNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>, And<APTaxTran.tranType, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<PX.Objects.TX.Tax.taxCalcLevel, Equal<CSTaxCalcLevel.inclusive>>>>>.Config>.Select((PXGraph) instance, new object[2]
        {
          (object) invoice2.RefNbr,
          (object) invoice2.DocType
        }).Count > 0;
        int num23;
        if (invoice2.TaxCalcMode == "G")
          num23 = PXSelectBase<APTaxTran, PXSelectJoin<APTaxTran, InnerJoin<PX.Objects.TX.Tax, On<APTaxTran.taxID, Equal<PX.Objects.TX.Tax.taxID>>>, Where<APTaxTran.refNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>, And<APTaxTran.tranType, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<PX.Objects.TX.Tax.taxCalcLevel, Equal<CSTaxCalcLevel.calcOnItemAmt>>>>>.Config>.Select((PXGraph) instance, new object[2]
          {
            (object) invoice2.RefNbr,
            (object) invoice2.DocType
          }).Count > 0 ? 1 : 0;
        else
          num23 = 0;
        int num24 = flag3 ? 1 : 0;
        if ((num23 | num24) != 0)
        {
          Decimal num25 = -invoice2.CuryTaxRoundDiff.GetValueOrDefault();
          Decimal? nullable22 = invoice2.CuryTaxAmt;
          Decimal valueOrDefault4 = nullable22.GetValueOrDefault();
          Decimal num26 = num25 + valueOrDefault4;
          nullable22 = invoice2.CuryDocBal;
          Decimal valueOrDefault5 = nullable22.GetValueOrDefault();
          Decimal num27 = num26 - valueOrDefault5;
          nullable22 = invoice2.TaxRoundDiff;
          Decimal num28 = -nullable22.GetValueOrDefault();
          nullable22 = invoice2.TaxAmt;
          Decimal valueOrDefault6 = nullable22.GetValueOrDefault();
          Decimal num29 = num28 + valueOrDefault6;
          nullable22 = invoice2.DocBal;
          Decimal valueOrDefault7 = nullable22.GetValueOrDefault();
          Decimal num30 = num29 - valueOrDefault7;
          foreach (PXResult<InvoiceTran> pxResult in ((PXSelectBase<InvoiceTran>) implementation.InvoiceTrans).Select(Array.Empty<object>()))
          {
            InvoiceTran invoiceTran = PXResult<InvoiceTran>.op_Implicit(pxResult);
            Decimal num31 = num27;
            nullable22 = invoiceTran.CuryTaxableAmt;
            Decimal valueOrDefault8;
            if (!(nullable22.GetValueOrDefault() == 0M))
            {
              nullable22 = invoiceTran.CuryTaxableAmt;
              valueOrDefault8 = nullable22.GetValueOrDefault();
            }
            else
            {
              nullable22 = invoiceTran.CuryTranAmt;
              valueOrDefault8 = nullable22.GetValueOrDefault();
            }
            num27 = num31 + valueOrDefault8;
            Decimal num32 = num30;
            nullable22 = invoiceTran.TaxableAmt;
            Decimal valueOrDefault9;
            if (!(nullable22.GetValueOrDefault() == 0M))
            {
              nullable22 = invoiceTran.TaxableAmt;
              valueOrDefault9 = nullable22.GetValueOrDefault();
            }
            else
            {
              nullable22 = invoiceTran.TranAmt;
              valueOrDefault9 = nullable22.GetValueOrDefault();
            }
            num30 = num32 + valueOrDefault9;
          }
          ref TAPDocument local9 = ref invoice2;
          // ISSUE: variable of a boxed type
          __Boxed<TAPDocument> local10 = (object) local9;
          Decimal? nullable23 = local9.CuryTaxRoundDiff;
          Decimal num33 = num27;
          Decimal? nullable24 = nullable23.HasValue ? new Decimal?(nullable23.GetValueOrDefault() + num33) : new Decimal?();
          local10.CuryTaxRoundDiff = nullable24;
          ref TAPDocument local11 = ref invoice2;
          // ISSUE: variable of a boxed type
          __Boxed<TAPDocument> local12 = (object) local11;
          nullable23 = local11.TaxRoundDiff;
          Decimal num34 = num30;
          Decimal? nullable25 = nullable23.HasValue ? new Decimal?(nullable23.GetValueOrDefault() + num34) : new Decimal?();
          local12.TaxRoundDiff = nullable25;
        }
      }
      invoice2 = ((PXSelectBase<TAPDocument>) implementation.Documents).Update(invoice2);
      instance.Actions.PressSave();
      if (receiptGroupPaidWithType == "CardComp" && source2.Count == 1 && bankTranMatch != null)
        CABankTransactionsMaint.RematchFromExpenseReceipt((PXGraph) this, bankTranMatch, invoice2.CATranID, invoice2.ContragentID, source2.Single<EPExpenseClaimDetails>());
      foreach (EPExpenseClaimDetails expenseClaimDetails2 in source2)
      {
        expenseClaimDetails2.APDocType = invoice2.DocType;
        expenseClaimDetails2.APRefNbr = invoice2.RefNbr;
        GraphHelper.MarkUpdated(((PXSelectBase) expenseClaimGraph.ExpenseClaimDetails).Cache, (object) expenseClaimDetails2);
      }
      claim.Status = "R";
      claim.Released = new bool?(true);
      ((PXSelectBase<EPExpenseClaim>) expenseClaimGraph.ExpenseClaim).Update(claim);
      EPHistory epHistory1 = (EPHistory) ((PXGraph) expenseClaimGraph).Caches[typeof (EPHistory)].Insert((object) new EPHistory()
      {
        EmployeeID = invoice2.ContragentID,
        FinPeriodID = invoice2.FinPeriodID
      });
      EPHistory epHistory2 = epHistory1;
      nullable2 = epHistory2.FinPtdClaimed;
      nullable5 = invoice2.DocBal;
      epHistory2.FinPtdClaimed = nullable2.HasValue & nullable5.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
      EPHistory epHistory3 = epHistory1;
      nullable5 = epHistory3.FinYtdClaimed;
      nullable2 = invoice2.DocBal;
      epHistory3.FinYtdClaimed = nullable5.HasValue & nullable2.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      if (invoice2.FinPeriodID == invoice2.HeaderTranPeriodID)
      {
        EPHistory epHistory4 = epHistory1;
        nullable2 = epHistory4.TranPtdClaimed;
        nullable5 = invoice2.DocBal;
        epHistory4.TranPtdClaimed = nullable2.HasValue & nullable5.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
        EPHistory epHistory5 = epHistory1;
        nullable5 = epHistory5.TranYtdClaimed;
        nullable2 = invoice2.DocBal;
        epHistory5.TranYtdClaimed = nullable5.HasValue & nullable2.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      }
      else
      {
        EPHistory epHistory6 = (EPHistory) ((PXGraph) expenseClaimGraph).Caches[typeof (EPHistory)].Insert((object) new EPHistory()
        {
          EmployeeID = invoice2.ContragentID,
          FinPeriodID = invoice2.HeaderTranPeriodID
        });
        EPHistory epHistory7 = epHistory6;
        nullable2 = epHistory7.TranPtdClaimed;
        nullable5 = invoice2.DocBal;
        epHistory7.TranPtdClaimed = nullable2.HasValue & nullable5.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
        EPHistory epHistory8 = epHistory6;
        nullable5 = epHistory8.TranYtdClaimed;
        nullable2 = invoice2.DocBal;
        epHistory8.TranYtdClaimed = nullable5.HasValue & nullable2.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      }
      ((PXGraph) expenseClaimGraph).Views.Caches.Add(typeof (EPHistory));
      ((PXAction) expenseClaimGraph.Save).Press();
      ((PXGraph) this).Actions.PressSave();
      apRegisterList.Add((PX.Objects.AP.APRegister) ((PXSelectBase<TAPDocument>) implementation.Documents).Current.Base);
    }
    return apRegisterList;
  }

  private void HandleInvoiceInMultiBaseCurrency<TAPDocument, TGraph>(
    TGraph docgraph,
    TAPDocument invoice)
    where TAPDocument : InvoiceBase, new()
    where TGraph : PXGraph, new()
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
      return;
    if (typeof (TGraph) == typeof (APInvoiceEntry))
    {
      ((PXSelectBase) ((object) docgraph as APInvoiceEntry).Document).Cache.RaiseFieldUpdated<PX.Objects.AP.APInvoice.branchID>((object) (PX.Objects.AP.APInvoice) invoice.Base, (object) invoice.BranchID);
    }
    else
    {
      if (!(typeof (TGraph) == typeof (APQuickCheckEntry)))
        return;
      ((PXSelectBase) ((object) docgraph as APQuickCheckEntry).Document).Cache.RaiseFieldUpdated<APQuickCheck.branchID>((object) (APQuickCheck) invoice.Base, (object) invoice.BranchID);
    }
  }

  private InvoiceTran AddTaxes<TAPDocument, TInvoiceMapping, TGraph, TAPDocumentGraphExtension>(
    TAPDocumentGraphExtension apDocumentGraphExtension,
    TGraph docgraph,
    ExpenseClaimEntry expenseClaimGraph,
    TAPDocument invoice,
    Decimal signOperation,
    EPExpenseClaimDetails claimdetail,
    InvoiceTran tran,
    bool isTipTran)
    where TAPDocument : InvoiceBase, new()
    where TInvoiceMapping : IBqlMapping
    where TGraph : PXGraph, new()
    where TAPDocumentGraphExtension : InvoiceBaseGraphExtension<TGraph, TAPDocument, TInvoiceMapping>
  {
    PXSelect<EPTaxTran, Where<EPTaxTran.claimDetailID, Equal<Required<EPTaxTran.claimDetailID>>>> pxSelect1 = new PXSelect<EPTaxTran, Where<EPTaxTran.claimDetailID, Equal<Required<EPTaxTran.claimDetailID>>>>((PXGraph) docgraph);
    PXSelect<EPTax, Where<EPTax.claimDetailID, Equal<Required<EPTax.claimDetailID>>, And<EPTax.taxID, Equal<Required<EPTax.taxID>>>>> pxSelect2 = new PXSelect<EPTax, Where<EPTax.claimDetailID, Equal<Required<EPTax.claimDetailID>>, And<EPTax.taxID, Equal<Required<EPTax.taxID>>>>>((PXGraph) docgraph);
    if (isTipTran)
    {
      ((PXSelectBase<EPTaxTran>) pxSelect1).WhereAnd<Where<EPTaxTran.isTipTax, Equal<True>>>();
      ((PXSelectBase<EPTax>) pxSelect2).WhereAnd<Where<EPTax.isTipTax, Equal<True>>>();
    }
    else
    {
      ((PXSelectBase<EPTaxTran>) pxSelect1).WhereAnd<Where<EPTaxTran.isTipTax, Equal<False>>>();
      ((PXSelectBase<EPTax>) pxSelect2).WhereAnd<Where<EPTax.isTipTax, Equal<False>>>();
    }
    PX.Objects.CM.CurrencyInfo curyInfoA = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<EPExpenseClaimDetails.curyInfoID>>>>.Config>.SelectSingleBound((PXGraph) docgraph, (object[]) null, new object[1]
    {
      (object) claimdetail.CuryInfoID
    }));
    PX.Objects.CM.CurrencyInfo curyInfoB = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<EPExpenseClaimDetails.curyInfoID>>>>.Config>.SelectSingleBound((PXGraph) docgraph, (object[]) null, new object[1]
    {
      (object) claimdetail.ClaimCuryInfoID
    }));
    foreach (PXResult<EPTaxTran> pxResult in ((PXSelectBase<EPTaxTran>) pxSelect1).Select(new object[1]
    {
      (object) claimdetail.ClaimDetailID
    }))
    {
      EPTaxTran epTaxTran = PXResult<EPTaxTran>.op_Implicit(pxResult);
      GenericTaxTran genericTaxTran1 = PXResultset<GenericTaxTran>.op_Implicit(((PXSelectBase<GenericTaxTran>) apDocumentGraphExtension.TaxTrans).Search<GenericTaxTran.taxID>((object) epTaxTran.TaxID, Array.Empty<object>()));
      EPTax epTax = PXResultset<EPTax>.op_Implicit(((PXSelectBase<EPTax>) pxSelect2).Select(new object[2]
      {
        (object) claimdetail.ClaimDetailID,
        (object) epTaxTran.TaxID
      }));
      if (epTax != null)
      {
        if (genericTaxTran1 == null)
        {
          GenericTaxTran genericTaxTran2 = new GenericTaxTran();
          genericTaxTran2.TaxID = epTaxTran.TaxID;
          TaxBaseAttribute.SetTaxCalc<InvoiceTran.taxCategoryID>(((PXSelectBase) apDocumentGraphExtension.InvoiceTrans).Cache, (object) null, TaxCalc.NoCalc);
          genericTaxTran1 = ((PXSelectBase<GenericTaxTran>) apDocumentGraphExtension.TaxTrans).Insert(genericTaxTran2);
          if (genericTaxTran1 != null)
          {
            GenericTaxTran copy = (GenericTaxTran) ((PXSelectBase) apDocumentGraphExtension.TaxTrans).Cache.CreateCopy((object) genericTaxTran1);
            copy.CuryTaxableAmt = new Decimal?(0M);
            copy.CuryTaxAmt = new Decimal?(0M);
            copy.CuryExpenseAmt = new Decimal?(0M);
            genericTaxTran1 = ((PXSelectBase<GenericTaxTran>) apDocumentGraphExtension.TaxTrans).Update(copy);
          }
        }
        if (genericTaxTran1 != null)
        {
          GenericTaxTran copy = (GenericTaxTran) ((PXSelectBase) apDocumentGraphExtension.TaxTrans).Cache.CreateCopy((object) genericTaxTran1);
          copy.TaxRate = epTaxTran.TaxRate;
          GenericTaxTran genericTaxTran3 = copy;
          Decimal? nullable1 = copy.CuryTaxableAmt;
          Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
          nullable1 = epTaxTran.ClaimCuryTaxableAmt;
          Decimal num1 = signOperation;
          Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num1) : new Decimal?();
          Decimal? nullable3;
          if (!nullable2.HasValue)
          {
            nullable1 = new Decimal?();
            nullable3 = nullable1;
          }
          else
            nullable3 = new Decimal?(valueOrDefault1 + nullable2.GetValueOrDefault());
          genericTaxTran3.CuryTaxableAmt = nullable3;
          GenericTaxTran genericTaxTran4 = copy;
          nullable1 = copy.CuryTaxAmt;
          Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
          nullable1 = epTaxTran.ClaimCuryTaxAmt;
          Decimal num2 = signOperation;
          nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num2) : new Decimal?();
          Decimal? nullable4;
          if (!nullable2.HasValue)
          {
            nullable1 = new Decimal?();
            nullable4 = nullable1;
          }
          else
            nullable4 = new Decimal?(valueOrDefault2 + nullable2.GetValueOrDefault());
          genericTaxTran4.CuryTaxAmt = nullable4;
          copy.CuryTaxAmtSumm = copy.CuryTaxAmt;
          GenericTaxTran genericTaxTran5 = copy;
          nullable1 = copy.CuryExpenseAmt;
          Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
          nullable1 = epTaxTran.ClaimCuryExpenseAmt;
          Decimal num3 = signOperation;
          nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num3) : new Decimal?();
          Decimal? nullable5;
          if (!nullable2.HasValue)
          {
            nullable1 = new Decimal?();
            nullable5 = nullable1;
          }
          else
            nullable5 = new Decimal?(valueOrDefault3 + nullable2.GetValueOrDefault());
          genericTaxTran5.CuryExpenseAmt = nullable5;
          copy.NonDeductibleTaxRate = epTaxTran.NonDeductibleTaxRate;
          TaxBaseAttribute.SetTaxCalc<InvoiceTran.taxCategoryID>(((PXSelectBase) apDocumentGraphExtension.InvoiceTrans).Cache, (object) null, TaxCalc.ManualCalc);
          GenericTaxTran genericTaxTran6 = ((PXSelectBase<GenericTaxTran>) apDocumentGraphExtension.TaxTrans).Update(copy);
          if (PXResultset<LineTax>.op_Implicit(((PXSelectBase<LineTax>) apDocumentGraphExtension.LineTaxes).Search<LineTax.lineNbr, LineTax.taxID>((object) tran.LineNbr, (object) genericTaxTran6.TaxID, Array.Empty<object>())) == null)
          {
            Decimal num4 = 0M;
            Decimal num5 = 0M;
            Decimal num6 = 0M;
            if (CurrencyHelper.IsSameCury(claimdetail.CuryInfoID, claimdetail.ClaimCuryInfoID, curyInfoA, curyInfoB))
            {
              nullable2 = epTax.CuryTaxableAmt;
              num4 = nullable2.GetValueOrDefault();
              nullable2 = epTax.CuryTaxAmt;
              num5 = nullable2.GetValueOrDefault();
              nullable2 = epTax.CuryExpenseAmt;
              num6 = nullable2.GetValueOrDefault();
            }
            else if (curyInfoB != null)
            {
              nullable2 = curyInfoB.CuryRate;
              if (nullable2.HasValue)
              {
                PXCache cache1 = ((PXSelectBase) expenseClaimGraph.ExpenseClaimDetails).Cache;
                EPExpenseClaimDetails row1 = claimdetail;
                nullable2 = epTax.TaxableAmt;
                Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
                ref Decimal local1 = ref num4;
                PX.Objects.CM.PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.claimCuryInfoID>(cache1, (object) row1, valueOrDefault4, out local1);
                PXCache cache2 = ((PXSelectBase) expenseClaimGraph.ExpenseClaimDetails).Cache;
                EPExpenseClaimDetails row2 = claimdetail;
                nullable2 = epTax.TaxAmt;
                Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
                ref Decimal local2 = ref num5;
                PX.Objects.CM.PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.claimCuryInfoID>(cache2, (object) row2, valueOrDefault5, out local2);
                PXCache cache3 = ((PXSelectBase) expenseClaimGraph.ExpenseClaimDetails).Cache;
                EPExpenseClaimDetails row3 = claimdetail;
                nullable2 = epTax.ExpenseAmt;
                Decimal valueOrDefault6 = nullable2.GetValueOrDefault();
                ref Decimal local3 = ref num6;
                PX.Objects.CM.PXCurrencyAttribute.CuryConvCury<EPExpenseClaimDetails.claimCuryInfoID>(cache3, (object) row3, valueOrDefault6, out local3);
              }
            }
            ((PXSelectBase<LineTax>) apDocumentGraphExtension.LineTaxes).Insert(new LineTax()
            {
              LineNbr = tran.LineNbr,
              TaxID = genericTaxTran6.TaxID,
              TaxRate = epTax.TaxRate,
              CuryTaxableAmt = new Decimal?(num4 * signOperation),
              CuryTaxAmt = new Decimal?(num5 * signOperation),
              CuryExpenseAmt = new Decimal?(num6 * signOperation)
            });
          }
          PX.Objects.TX.Tax tax = PXResultset<PX.Objects.TX.Tax>.op_Implicit(PXSelectBase<PX.Objects.TX.Tax, PXSelect<PX.Objects.TX.Tax, Where<PX.Objects.TX.Tax.taxID, Equal<Required<PX.Objects.TX.Tax.taxID>>>>.Config>.Select((PXGraph) docgraph, new object[1]
          {
            (object) genericTaxTran6.TaxID
          }));
          if (tax.TaxCalcLevel == "0" || invoice.TaxCalcMode == "G" && tax.TaxCalcLevel == "1")
          {
            nullable2 = tran.CuryTaxableAmt;
            if (nullable2.HasValue)
            {
              nullable2 = tran.CuryTaxableAmt;
              Decimal num7 = 0M;
              if (!(nullable2.GetValueOrDefault() == num7 & nullable2.HasValue))
                continue;
            }
            InvoiceTran invoiceTran1 = tran;
            nullable2 = epTaxTran.ClaimCuryTaxableAmt;
            Decimal num8 = signOperation;
            Decimal? nullable6;
            if (!nullable2.HasValue)
            {
              nullable1 = new Decimal?();
              nullable6 = nullable1;
            }
            else
              nullable6 = new Decimal?(nullable2.GetValueOrDefault() * num8);
            invoiceTran1.CuryTaxableAmt = nullable6;
            InvoiceTran invoiceTran2 = tran;
            nullable2 = epTaxTran.ClaimCuryTaxAmt;
            Decimal num9 = signOperation;
            Decimal? nullable7;
            if (!nullable2.HasValue)
            {
              nullable1 = new Decimal?();
              nullable7 = nullable1;
            }
            else
              nullable7 = new Decimal?(nullable2.GetValueOrDefault() * num9);
            invoiceTran2.CuryTaxAmt = nullable7;
            tran = ((PXSelectBase<InvoiceTran>) apDocumentGraphExtension.InvoiceTrans).Update(tran);
          }
        }
      }
    }
    return tran;
  }

  /// <summary>
  /// The method to insert invoice EP transactions
  /// for the <see cref="T:PX.Objects.EP.EPExpenseClaim" /> entity inside the
  /// <see cref="M:PX.Objects.EP.EPReleaseProcess.ReleaseClaimDetails``4(PX.Objects.EP.ExpenseClaimEntry,PX.Objects.EP.EPExpenseClaim,System.Collections.Generic.IEnumerable{PX.Objects.EP.EPExpenseClaimDetails},System.String)" />
  /// <see cref="T:PX.Objects.EP.EPReleaseProcess.InvoiceTranContext" /> class content:
  /// <see cref="P:PX.Objects.EP.EPReleaseProcess.InvoiceTranContext.EPClaim" />.
  /// <see cref="P:PX.Objects.EP.EPReleaseProcess.InvoiceTranContext.EPClaimDetails" />.
  /// </summary>
  public virtual InvoiceTran InsertInvoiceTransaction(
    PXCache InvoiceTrans,
    InvoiceTran tran,
    EPReleaseProcess.InvoiceTranContext invoiceTranInsertionContext)
  {
    return (InvoiceTran) InvoiceTrans.Insert((object) tran);
  }

  /// <summary>
  /// The method to update invoice EP transactions
  /// for the <see cref="T:PX.Objects.EP.EPExpenseClaim" /> entity inside the
  /// <see cref="M:PX.Objects.EP.EPReleaseProcess.ReleaseClaimDetails``4(PX.Objects.EP.ExpenseClaimEntry,PX.Objects.EP.EPExpenseClaim,System.Collections.Generic.IEnumerable{PX.Objects.EP.EPExpenseClaimDetails},System.String)" />
  /// <see cref="T:PX.Objects.EP.EPReleaseProcess.InvoiceTranContext" /> class content:
  /// <see cref="P:PX.Objects.EP.EPReleaseProcess.InvoiceTranContext.EPClaim" />.
  /// <see cref="P:PX.Objects.EP.EPReleaseProcess.InvoiceTranContext.EPClaimDetails" />.
  /// </summary>
  public virtual InvoiceTran UpdateInvoiceTransaction(
    PXCache InvoiceTrans,
    InvoiceTran tran,
    EPReleaseProcess.InvoiceTranContext invoiceTranUpdationContext)
  {
    return (InvoiceTran) InvoiceTrans.Update((object) tran);
  }

  /// <summary>
  /// The method to insert invoice EP transactions
  /// for the <see cref="T:PX.Objects.EP.EPExpenseClaim" /> entity inside the
  /// <see cref="M:PX.Objects.EP.EPReleaseProcess.ReleaseClaimDetails``4(PX.Objects.EP.ExpenseClaimEntry,PX.Objects.EP.EPExpenseClaim,System.Collections.Generic.IEnumerable{PX.Objects.EP.EPExpenseClaimDetails},System.String)" />
  /// <see cref="T:PX.Objects.EP.EPReleaseProcess.InvoiceTranContext" /> class content:
  /// <see cref="P:PX.Objects.EP.EPReleaseProcess.InvoiceTranContext.EPClaim" />.
  /// <see cref="P:PX.Objects.EP.EPReleaseProcess.InvoiceTranContext.EPClaimDetails" />.
  /// </summary>
  public virtual InvoiceTran InsertInvoiceTipTransaction(
    PXCache InvoiceTrans,
    InvoiceTran tran,
    EPReleaseProcess.InvoiceTranContext invoiceTranTipInsertionContext)
  {
    return (InvoiceTran) InvoiceTrans.Insert((object) tran);
  }

  /// <summary>
  /// The method to update invoice EP transactions tip
  /// for the <see cref="T:PX.Objects.EP.EPExpenseClaim" /> entity inside the
  /// <see cref="M:PX.Objects.EP.EPReleaseProcess.ReleaseClaimDetails``4(PX.Objects.EP.ExpenseClaimEntry,PX.Objects.EP.EPExpenseClaim,System.Collections.Generic.IEnumerable{PX.Objects.EP.EPExpenseClaimDetails},System.String)" />
  /// <see cref="T:PX.Objects.EP.EPReleaseProcess.InvoiceTranContext" /> class content:
  /// <see cref="P:PX.Objects.EP.EPReleaseProcess.InvoiceTranContext.EPClaim" />.
  /// <see cref="P:PX.Objects.EP.EPReleaseProcess.InvoiceTranContext.EPClaimDetails" />.
  /// </summary>
  public virtual InvoiceTran UpdateInvoiceTipTransactionAccounts(
    PXCache InvoiceTrans,
    InvoiceTran tran,
    EPReleaseProcess.InvoiceTranContext invoiceTranTipUpdationContext)
  {
    return (InvoiceTran) InvoiceTrans.Update((object) tran);
  }

  /// <summary>
  /// The method to update invoice EP transactions tip for the particular Project ID
  /// for the <see cref="T:PX.Objects.EP.EPExpenseClaim" /> entity inside the
  /// <see cref="M:PX.Objects.EP.EPReleaseProcess.ReleaseClaimDetails``4(PX.Objects.EP.ExpenseClaimEntry,PX.Objects.EP.EPExpenseClaim,System.Collections.Generic.IEnumerable{PX.Objects.EP.EPExpenseClaimDetails},System.String)" />
  /// <see cref="T:PX.Objects.EP.EPReleaseProcess.InvoiceTranContext" /> class content:
  /// <see cref="P:PX.Objects.EP.EPReleaseProcess.InvoiceTranContext.EPClaim" />.
  /// <see cref="P:PX.Objects.EP.EPReleaseProcess.InvoiceTranContext.EPClaimDetails" />.
  /// </summary>
  public virtual InvoiceTran UpdateInvoiceTipTransactionTaxesAndProject(
    PXCache InvoiceTrans,
    InvoiceTran tran,
    EPReleaseProcess.InvoiceTranContext invoiceTranTipUpdationContext)
  {
    return (InvoiceTran) InvoiceTrans.Update((object) tran);
  }

  public class InvoiceTranContext
  {
    public virtual EPExpenseClaim EPClaim { get; set; }

    public virtual EPExpenseClaimDetails EPClaimDetails { get; set; }
  }
}

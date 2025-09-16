// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPCustomerBillingProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL.FinPeriods;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public class EPCustomerBillingProcess : PXGraph<EPCustomerBillingProcess>
{
  public PXSelect<EPExpenseClaimDetails> Transactions;
  public PXSetup<EPSetup> Setup;

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  protected virtual void EPExpenseClaimDetails_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  public virtual void Bill(CustomersList customer, EPCustomerBilling.BillingFilter filter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    EPCustomerBillingProcess.\u003C\u003Ec__DisplayClass7_0 cDisplayClass70 = new EPCustomerBillingProcess.\u003C\u003Ec__DisplayClass7_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass70.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass70.filter = filter;
    ARInvoiceEntry instance1 = PXGraph.CreateInstance<ARInvoiceEntry>();
    RegisterEntry instance2 = PXGraph.CreateInstance<RegisterEntry>();
    ((PXGraph) instance1).Clear();
    ((PXGraph) instance2).Clear();
    PMRegister pmRegister = (PMRegister) null;
    PX.Objects.AR.ARInvoice arInvoice1 = (PX.Objects.AR.ARInvoice) null;
    List<PX.Objects.AR.ARRegister> list = new List<PX.Objects.AR.ARRegister>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass70.listOfDirectBilledClaims = new List<EPExpenseClaimDetails>();
    PXSelectJoin<EPExpenseClaimDetails, LeftJoin<PX.Objects.CT.Contract, On<EPExpenseClaimDetails.contractID, Equal<PX.Objects.CT.Contract.contractID>, And<Where<PX.Objects.CT.Contract.baseType, Equal<CTPRType.contract>, Or<PX.Objects.CT.Contract.nonProject, Equal<True>>>>>, LeftJoin<PX.Objects.GL.Account, On<EPExpenseClaimDetails.expenseAccountID, Equal<PX.Objects.GL.Account.accountID>>>>, Where<EPExpenseClaimDetails.released, Equal<boolTrue>, And<EPExpenseClaimDetails.billable, Equal<boolTrue>, And<EPExpenseClaimDetails.billed, Equal<boolFalse>, And<EPExpenseClaimDetails.customerID, Equal<Required<EPExpenseClaimDetails.customerID>>, And<EPExpenseClaimDetails.customerLocationID, Equal<Required<EPExpenseClaimDetails.customerLocationID>>, And<EPExpenseClaimDetails.expenseDate, LessEqual<Required<EPExpenseClaimDetails.expenseDate>>, And<Where<EPExpenseClaimDetails.contractID, Equal<PX.Objects.CT.Contract.contractID>, Or<EPExpenseClaimDetails.contractID, IsNull>>>>>>>>>, OrderBy<Asc<EPExpenseClaimDetails.branchID>>> pxSelectJoin = new PXSelectJoin<EPExpenseClaimDetails, LeftJoin<PX.Objects.CT.Contract, On<EPExpenseClaimDetails.contractID, Equal<PX.Objects.CT.Contract.contractID>, And<Where<PX.Objects.CT.Contract.baseType, Equal<CTPRType.contract>, Or<PX.Objects.CT.Contract.nonProject, Equal<True>>>>>, LeftJoin<PX.Objects.GL.Account, On<EPExpenseClaimDetails.expenseAccountID, Equal<PX.Objects.GL.Account.accountID>>>>, Where<EPExpenseClaimDetails.released, Equal<boolTrue>, And<EPExpenseClaimDetails.billable, Equal<boolTrue>, And<EPExpenseClaimDetails.billed, Equal<boolFalse>, And<EPExpenseClaimDetails.customerID, Equal<Required<EPExpenseClaimDetails.customerID>>, And<EPExpenseClaimDetails.customerLocationID, Equal<Required<EPExpenseClaimDetails.customerLocationID>>, And<EPExpenseClaimDetails.expenseDate, LessEqual<Required<EPExpenseClaimDetails.expenseDate>>, And<Where<EPExpenseClaimDetails.contractID, Equal<PX.Objects.CT.Contract.contractID>, Or<EPExpenseClaimDetails.contractID, IsNull>>>>>>>>>, OrderBy<Asc<EPExpenseClaimDetails.branchID>>>((PXGraph) this);
    // ISSUE: method pointer
    ((PXGraph) instance1).RowPersisted.AddHandler<PX.Objects.AR.ARInvoice>(new PXRowPersisted((object) cDisplayClass70, __methodptr(\u003CBill\u003Eb__0)));
    Decimal signOperation = 1M;
    // ISSUE: reference to a compiler-generated field
    object[] objArray = new object[3]
    {
      (object) customer.CustomerID,
      (object) customer.LocationID,
      (object) cDisplayClass70.filter.EndDate
    };
    IEnumerable<PXResult<EPExpenseClaimDetails>> pxResults = ((IEnumerable<PXResult<EPExpenseClaimDetails>>) ((PXSelectBase<EPExpenseClaimDetails>) pxSelectJoin).Select(objArray)).AsEnumerable<PXResult<EPExpenseClaimDetails>>();
    // ISSUE: reference to a compiler-generated method
    this.FinPeriodUtils.ValidateFinPeriod<EPExpenseClaimDetails>(GraphHelper.RowCast<EPExpenseClaimDetails>((IEnumerable) pxResults), new Func<EPExpenseClaimDetails, string>(cDisplayClass70.\u003CBill\u003Eb__1), (Func<EPExpenseClaimDetails, int?[]>) (m => m.BranchID.SingleToArray<int?>()));
    foreach (PXResult<EPExpenseClaimDetails, PX.Objects.CT.Contract, PX.Objects.GL.Account> res in pxResults)
    {
      EPExpenseClaimDetails row = PXResult<EPExpenseClaimDetails, PX.Objects.CT.Contract, PX.Objects.GL.Account>.op_Implicit(res);
      int? nullable1 = row.ContractID;
      Decimal? nullable2;
      if (nullable1.HasValue && !ProjectDefaultAttribute.IsNonProject(row.ContractID))
      {
        if (pmRegister == null)
        {
          EPExpenseClaim epExpenseClaim = PXResultset<EPExpenseClaim>.op_Implicit(PXSelectBase<EPExpenseClaim, PXSelect<EPExpenseClaim, Where<EPExpenseClaim.refNbr, Equal<Required<EPExpenseClaim.refNbr>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) row.RefNbr
          }));
          pmRegister = (PMRegister) ((PXSelectBase) instance2.Document).Cache.Insert();
          pmRegister.OrigDocType = "EC";
          pmRegister.OrigNoteID = epExpenseClaim.NoteID;
        }
        PMTran pmTran = this.InsertPMTran(instance2, res);
        if (pmTran.Released.GetValueOrDefault())
        {
          PXCache cache = ((PXSelectBase) instance2.Transactions).Cache;
          int? projectId = pmTran.ProjectID;
          int? inventoryId = pmTran.InventoryID;
          nullable2 = pmTran.BillableQty;
          Decimal valueOrDefault = nullable2.GetValueOrDefault();
          string uom = pmTran.UOM;
          UsageMaint.AddUsage(cache, projectId, inventoryId, valueOrDefault, uom);
        }
      }
      else
      {
        if (arInvoice1 != null)
        {
          nullable1 = arInvoice1.BranchID;
          int? branchId = row.BranchID;
          if (nullable1.GetValueOrDefault() == branchId.GetValueOrDefault() & nullable1.HasValue == branchId.HasValue)
            goto label_12;
        }
        if (arInvoice1 != null)
        {
          arInvoice1.CuryOrigDocAmt = arInvoice1.CuryDocBal;
          ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance1.Document).Update(arInvoice1);
          ((PXAction) instance1.Save).Press();
          // ISSUE: reference to a compiler-generated field
          cDisplayClass70.listOfDirectBilledClaims.Clear();
        }
        // ISSUE: reference to a compiler-generated field
        nullable2 = PXResultset<EPExpenseClaimDetails>.op_Implicit(PXSelectBase<EPExpenseClaimDetails, PXSelectJoinGroupBy<EPExpenseClaimDetails, LeftJoin<PX.Objects.CT.Contract, On<EPExpenseClaimDetails.contractID, Equal<PX.Objects.CT.Contract.contractID>, And<Where<PX.Objects.CT.Contract.baseType, Equal<CTPRType.contract>, Or<PX.Objects.CT.Contract.nonProject, Equal<True>>>>>>, Where<EPExpenseClaimDetails.released, Equal<boolTrue>, And<EPExpenseClaimDetails.billable, Equal<boolTrue>, And<EPExpenseClaimDetails.billed, Equal<boolFalse>, And<EPExpenseClaimDetails.customerID, Equal<Required<EPExpenseClaimDetails.customerID>>, And<EPExpenseClaimDetails.customerLocationID, Equal<Required<EPExpenseClaimDetails.customerLocationID>>, And<EPExpenseClaimDetails.expenseDate, LessEqual<Required<EPExpenseClaimDetails.expenseDate>>, And<EPExpenseClaimDetails.branchID, Equal<Required<EPExpenseClaimDetails.branchID>>, And<Where<PX.Objects.CT.Contract.nonProject, Equal<True>, Or<EPExpenseClaimDetails.contractID, IsNull>>>>>>>>>>, Aggregate<Sum<EPExpenseClaimDetails.curyTranAmt>>>.Config>.Select((PXGraph) this, new object[4]
        {
          (object) customer.CustomerID,
          (object) customer.LocationID,
          (object) cDisplayClass70.filter.EndDate,
          (object) row.BranchID
        })).CuryTranAmt;
        Decimal num1 = 0M;
        signOperation = (Decimal) (nullable2.GetValueOrDefault() < num1 & nullable2.HasValue ? -1 : 1);
        PXCache cache = ((PXSelectBase) instance1.Document).Cache;
        PX.Objects.AR.ARInvoice arInvoice2 = new PX.Objects.AR.ARInvoice();
        arInvoice2.OrigModule = "EP";
        arInvoice2.DocType = signOperation < 0M ? "CRM" : "INV";
        arInvoice2.CustomerID = row.CustomerID;
        arInvoice2.CustomerLocationID = row.CustomerLocationID;
        // ISSUE: reference to a compiler-generated field
        arInvoice2.DocDate = cDisplayClass70.filter.InvoiceDate;
        arInvoice2.OrigRefNbr = row.RefNbr;
        arInvoice1 = (PX.Objects.AR.ARInvoice) cache.Insert((object) arInvoice2);
        // ISSUE: reference to a compiler-generated field
        arInvoice1.FinPeriodID = cDisplayClass70.filter.InvFinPeriodID;
        list.Add((PX.Objects.AR.ARRegister) arInvoice1);
label_12:
        this.InsertARTran(instance1, row, signOperation);
        nullable2 = row.CuryTipAmt;
        if (nullable2.GetValueOrDefault() != 0M)
        {
          int num2 = signOperation < 0M ? 1 : 0;
          nullable2 = row.ClaimCuryTranAmtWithTaxes;
          Decimal num3 = 0M;
          int num4 = nullable2.GetValueOrDefault() < num3 & nullable2.HasValue ? 1 : 0;
          Decimal tipQty = (Decimal) (num2 == num4 ? 1 : -1);
          this.InsertARTran(instance1, row, signOperation, tipQty, true);
        }
        // ISSUE: reference to a compiler-generated field
        cDisplayClass70.listOfDirectBilledClaims.Add(row);
      }
      row.Billed = new bool?(true);
      ((PXSelectBase<EPExpenseClaimDetails>) this.Transactions).Update(row);
    }
    bool? nullable;
    if (arInvoice1 != null)
    {
      arInvoice1.CuryOrigDocAmt = arInvoice1.CuryDocBal;
      nullable = ((PXSelectBase<ARSetup>) instance1.ARSetup).Current.HoldEntry;
      bool flag = false;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      {
        nullable = ((PXSelectBase<EPSetup>) this.Setup).Current.AutomaticReleaseAR;
        if (!nullable.GetValueOrDefault())
          goto label_25;
      }
      PX.Objects.AR.ARInvoice copy = PXCache<PX.Objects.AR.ARInvoice>.CreateCopy(arInvoice1);
      copy.Hold = new bool?(false);
      arInvoice1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance1.Document).Update(copy);
label_25:
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance1.Document).Update(arInvoice1);
      ((PXAction) instance1.Save).Press();
    }
    if (pmRegister != null)
      ((PXAction) instance2.Save).Press();
    ((PXGraph) this).Persist(typeof (EPExpenseClaimDetails), (PXDBOperation) 1);
    nullable = ((PXSelectBase<EPSetup>) this.Setup).Current.AutomaticReleaseAR;
    if (!nullable.GetValueOrDefault())
      return;
    ARDocumentRelease.ReleaseDoc(list, false);
  }

  protected virtual void InsertARTran(
    ARInvoiceEntry arGraph,
    EPExpenseClaimDetails row,
    Decimal signOperation,
    Decimal tipQty = 1M,
    bool isTipTransaction = false)
  {
    PX.Objects.CM.CurrencyInfo curyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo>.Config>.Search<PX.Objects.CM.CurrencyInfo.curyInfoID>((PXGraph) arGraph, (object) row.CuryInfoID, Array.Empty<object>()));
    EPSetup epSetup = PXResultset<EPSetup>.op_Implicit(PXSelectBase<EPSetup, PXSelectReadonly<EPSetup>.Config>.Select((PXGraph) arGraph, Array.Empty<object>()));
    PX.Objects.AR.ARTran tran1 = ((PXSelectBase<PX.Objects.AR.ARTran>) arGraph.Transactions).Insert();
    PX.Objects.AR.ARTran tran2;
    if (isTipTransaction)
    {
      PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) arGraph, new object[1]
      {
        (object) epSetup.NonTaxableTipItem
      }));
      if (inventoryItem == null)
        throw new PXException("To be able to specify a nonzero tip amount in the expense receipt, specify an appropriate tip item in the Non-Taxable Tip Item box on the Time & Expenses Preferences (EP101000) form.");
      tran1.InventoryID = epSetup.NonTaxableTipItem;
      tran1.Qty = new Decimal?(tipQty);
      tran1.UOM = inventoryItem.BaseUnit;
      tran1.TranDesc = inventoryItem.Descr;
      EPCustomerBillingProcess.SetAmount(arGraph, row.CuryTipAmt, row.TipAmt, new Decimal?(tipQty), signOperation, curyInfo, tran1);
      tran2 = ((PXSelectBase<PX.Objects.AR.ARTran>) arGraph.Transactions).Update(tran1);
      if (epSetup.UseReceiptAccountForTips.GetValueOrDefault())
      {
        tran2.AccountID = row.SalesAccountID;
        tran2.SubID = row.SalesSubID;
      }
      else
      {
        PX.Objects.CR.Location location1 = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<PX.Objects.AR.ARTran.branchID>>>>.Config>.Select((PXGraph) arGraph, Array.Empty<object>()));
        PX.Objects.CT.Contract contract = PXResultset<PX.Objects.CT.Contract>.op_Implicit(PXSelectBase<PX.Objects.CT.Contract, PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.contractID, Equal<Required<PX.Objects.CT.Contract.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.ContractID
        }));
        PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.Select((PXGraph) arGraph, new object[2]
        {
          (object) row.ContractID,
          (object) row.TaskID
        }));
        EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee>.Config>.Search<EPEmployee.bAccountID>((PXGraph) this, (object) (int?) row?.EmployeeID, Array.Empty<object>()));
        PX.Objects.CR.Location location2 = (PX.Objects.CR.Location) PXSelectorAttribute.Select<EPExpenseClaimDetails.customerLocationID>(((PXGraph) arGraph).Caches[typeof (EPExpenseClaimDetails)], (object) row);
        int? nullable1 = (int?) ((PXGraph) arGraph).Caches[typeof (EPEmployee)].GetValue<EPEmployee.salesSubID>((object) epEmployee);
        int? nullable2 = (int?) ((PXGraph) arGraph).Caches[typeof (PX.Objects.IN.InventoryItem)].GetValue<PX.Objects.IN.InventoryItem.salesSubID>((object) inventoryItem);
        int? nullable3 = (int?) ((PXGraph) arGraph).Caches[typeof (PX.Objects.CR.Location)].GetValue<PX.Objects.CR.Location.cSalesSubID>((object) location1);
        int? nullable4 = (int?) ((PXGraph) arGraph).Caches[typeof (PX.Objects.CT.Contract)].GetValue<PX.Objects.CT.Contract.defaultSalesSubID>((object) contract);
        int? nullable5 = (int?) ((PXGraph) arGraph).Caches[typeof (PMTask)].GetValue<PMTask.defaultSalesSubID>((object) pmTask);
        int? nullable6 = (int?) ((PXGraph) arGraph).Caches[typeof (PX.Objects.CR.Location)].GetValue<PX.Objects.CR.Location.cSalesSubID>((object) location2);
        object obj = (object) SubAccountMaskAttribute.MakeSub<EPSetup.salesSubMask>((PXGraph) arGraph, epSetup.SalesSubMask, new object[6]
        {
          (object) nullable1,
          (object) nullable2,
          (object) nullable3,
          (object) nullable4,
          (object) nullable5,
          (object) nullable6
        }, new System.Type[6]
        {
          typeof (EPEmployee.salesSubID),
          typeof (PX.Objects.IN.InventoryItem.salesSubID),
          typeof (PX.Objects.CR.Location.cSalesSubID),
          typeof (PX.Objects.CT.Contract.defaultSalesSubID),
          typeof (PMTask.defaultSalesSubID),
          typeof (PX.Objects.CR.Location.cSalesSubID)
        });
        ((PXGraph) arGraph).Caches[typeof (PX.Objects.AR.ARTran)].RaiseFieldUpdating<PX.Objects.AR.ARTran.subID>((object) tran2, ref obj);
        tran2.SubID = (int?) obj;
      }
    }
    else
    {
      tran1.InventoryID = row.InventoryID;
      PX.Objects.AR.ARTran arTran = tran1;
      Decimal? nullable7 = row.Qty;
      Decimal num1 = signOperation;
      Decimal? nullable8 = nullable7.HasValue ? new Decimal?(nullable7.GetValueOrDefault() * num1) : new Decimal?();
      arTran.Qty = nullable8;
      tran1.UOM = row.UOM;
      PX.Objects.AR.ARTran tran3 = ((PXSelectBase<PX.Objects.AR.ARTran>) arGraph.Transactions).Update(tran1);
      tran3.AccountID = row.SalesAccountID;
      tran3.SubID = row.SalesSubID;
      tran3.TranDesc = row.TranDesc;
      EPTaxTran epTaxTran1 = (EPTaxTran) null;
      foreach (PXResult<EPTaxTran> pxResult in PXSelectBase<EPTaxTran, PXSelect<EPTaxTran, Where<EPTaxTran.claimDetailID, Equal<Required<EPTaxTran.claimDetailID>>, And<EPTaxTran.isTipTax, Equal<False>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.ClaimDetailID
      }))
      {
        EPTaxTran epTaxTran2 = PXResult<EPTaxTran>.op_Implicit(pxResult);
        if (epTaxTran1 != null)
        {
          nullable7 = epTaxTran1.CuryTaxableAmt;
          Decimal num2 = Math.Abs(nullable7.GetValueOrDefault());
          nullable7 = epTaxTran2.CuryTaxableAmt;
          Decimal num3 = Math.Abs(nullable7.GetValueOrDefault());
          if (!(num2 > num3))
            continue;
        }
        epTaxTran1 = epTaxTran2;
      }
      Decimal? nullable9 = (Decimal?) epTaxTran1?.CuryTaxableAmt;
      Decimal? curyTranAmt = nullable9 ?? row.CuryTaxableAmt;
      nullable9 = (Decimal?) epTaxTran1?.TaxableAmt;
      Decimal? tranAmt = nullable9 ?? row.TaxableAmt;
      EPCustomerBillingProcess.SetAmount(arGraph, curyTranAmt, tranAmt, row.Qty, signOperation, curyInfo, tran3);
      tran2 = ((PXSelectBase<PX.Objects.AR.ARTran>) arGraph.Transactions).Update(tran3);
      nullable9 = tran2.CuryTaxableAmt;
      Decimal num4 = 0M;
      if (!(nullable9.GetValueOrDefault() == num4 & nullable9.HasValue))
      {
        nullable9 = tran2.CuryTaxableAmt;
        Decimal? nullable10 = curyTranAmt;
        if (!(nullable9.GetValueOrDefault() == nullable10.GetValueOrDefault() & nullable9.HasValue == nullable10.HasValue))
        {
          Decimal? curyTaxableAmt = row.CuryTaxableAmt;
          Decimal? taxableAmt = row.TaxableAmt;
          EPCustomerBillingProcess.SetAmount(arGraph, curyTaxableAmt, taxableAmt, row.Qty, signOperation, curyInfo, tran2);
        }
      }
    }
    tran2.Date = row.ExpenseDate;
    tran2.ManualPrice = new bool?(true);
    PX.Objects.AR.ARTran arTran1 = ((PXSelectBase<PX.Objects.AR.ARTran>) arGraph.Transactions).Update(tran2);
    PXNoteAttribute.CopyNoteAndFiles(((PXGraph) this).Caches[typeof (EPExpenseClaimDetails)], (object) row, ((PXSelectBase) arGraph.Transactions).Cache, (object) arTran1, ((PXSelectBase<EPSetup>) this.Setup).Current.GetCopyNoteSettings<PXModule.ar>());
  }

  private static void SetAmount(
    ARInvoiceEntry arGraph,
    Decimal? curyTranAmt,
    Decimal? tranAmt,
    Decimal? qty,
    Decimal signOperation,
    PX.Objects.CM.CurrencyInfo curyInfo,
    PX.Objects.AR.ARTran tran)
  {
    Decimal num = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) arGraph.currencyinfo).Current == null || curyInfo == null || !(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) arGraph.currencyinfo).Current.CuryID == curyInfo.CuryID) ? ((PXGraph) arGraph).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo().CuryConvCury(tranAmt.GetValueOrDefault()) : curyTranAmt ?? 0M;
    tran.CuryTranAmt = new Decimal?(num * signOperation);
    tran.CuryUnitPrice = new Decimal?(Math.Abs(num / ((qty ?? 1M) != 0M ? qty ?? 1M : 1M)));
    tran.CuryExtPrice = new Decimal?(num * signOperation);
  }

  protected virtual PMTran InsertPMTran(
    RegisterEntry pmGraph,
    PXResult<EPExpenseClaimDetails, PX.Objects.CT.Contract, PX.Objects.GL.Account> res)
  {
    EPExpenseClaimDetails expenseClaimDetails = PXResult<EPExpenseClaimDetails, PX.Objects.CT.Contract, PX.Objects.GL.Account>.op_Implicit(res);
    PX.Objects.CT.Contract contract = PXResult<EPExpenseClaimDetails, PX.Objects.CT.Contract, PX.Objects.GL.Account>.op_Implicit(res);
    PX.Objects.GL.Account account = PXResult<EPExpenseClaimDetails, PX.Objects.CT.Contract, PX.Objects.GL.Account>.op_Implicit(res);
    if (!account.AccountGroupID.HasValue && contract.BaseType == "P")
      throw new PXException("Expense Account '{0}' is not included in any Account Group. Please assign an Account Group given Account and try again.", new object[1]
      {
        (object) account.AccountCD
      });
    bool flag = contract.BaseType == "C";
    PMTran pmTran1 = (PMTran) ((PXSelectBase) pmGraph.Transactions).Cache.Insert();
    pmTran1.AccountGroupID = account.AccountGroupID;
    pmTran1.BAccountID = expenseClaimDetails.CustomerID;
    pmTran1.LocationID = expenseClaimDetails.CustomerLocationID;
    pmTran1.ProjectID = expenseClaimDetails.ContractID;
    pmTran1.TaskID = expenseClaimDetails.TaskID;
    pmTran1.CostCodeID = expenseClaimDetails.CostCodeID;
    pmTran1.InventoryID = expenseClaimDetails.InventoryID;
    pmTran1.Qty = expenseClaimDetails.Qty;
    pmTran1.Billable = new bool?(true);
    pmTran1.BillableQty = expenseClaimDetails.Qty;
    pmTran1.UOM = expenseClaimDetails.UOM;
    pmTran1.TranCuryID = expenseClaimDetails.CuryID;
    pmTran1.BaseCuryInfoID = expenseClaimDetails.CuryInfoID;
    pmTran1.TranCuryAmount = expenseClaimDetails.ClaimCuryTranAmt;
    pmTran1.Amount = expenseClaimDetails.ClaimTranAmt;
    pmTran1.TranCuryUnitRate = expenseClaimDetails.CuryUnitCost;
    pmTran1.UnitRate = expenseClaimDetails.UnitCost;
    Decimal? nullable1 = expenseClaimDetails.CuryUnitCost;
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
    {
      nullable1 = expenseClaimDetails.Qty;
      Decimal num2 = 0M;
      if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
      {
        PMTran pmTran2 = pmTran1;
        nullable1 = expenseClaimDetails.ClaimCuryTranAmt;
        Decimal? nullable2 = expenseClaimDetails.Qty;
        Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?();
        pmTran2.TranCuryUnitRate = nullable3;
        PMTran pmTran3 = pmTran1;
        nullable2 = expenseClaimDetails.ClaimTranAmt;
        nullable1 = expenseClaimDetails.Qty;
        Decimal? nullable4 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() / nullable1.GetValueOrDefault()) : new Decimal?();
        pmTran3.UnitRate = nullable4;
      }
    }
    pmTran1.AccountID = expenseClaimDetails.ExpenseAccountID;
    pmTran1.SubID = expenseClaimDetails.ExpenseSubID;
    pmTran1.StartDate = expenseClaimDetails.ExpenseDate;
    pmTran1.EndDate = expenseClaimDetails.ExpenseDate;
    pmTran1.Date = expenseClaimDetails.ExpenseDate;
    pmTran1.ResourceID = expenseClaimDetails.EmployeeID;
    pmTran1.Released = new bool?(flag);
    PMTran pmTran4 = ((PXSelectBase<PMTran>) pmGraph.Transactions).Update(pmTran1);
    ((PXSelectBase<PMRegister>) pmGraph.Document).Current.Released = new bool?(flag);
    if (flag)
      ((PXSelectBase<PMRegister>) pmGraph.Document).Current.Status = "R";
    PXNoteAttribute.CopyNoteAndFiles(((PXGraph) this).Caches[typeof (EPExpenseClaimDetails)], (object) expenseClaimDetails, ((PXSelectBase) pmGraph.Transactions).Cache, (object) pmTran4, ((PXSelectBase<EPSetup>) this.Setup).Current.GetCopyNoteSettings<PXModule.pm>());
    return pmTran4;
  }
}

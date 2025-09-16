// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ExpenseClaimDetailMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.SM;
using PX.TM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.EP;

public class ExpenseClaimDetailMaint : PXGraph<
#nullable disable
ExpenseClaimDetailMaint>
{
  [PXCopyPasteHiddenView]
  public PXSetup<EPSetup> epsetup;
  public PXCancel<ExpenseClaimDetailMaint.ExpenseClaimDetailsFilter> Cancel;
  public PXAction<ExpenseClaimDetailMaint.ExpenseClaimDetailsFilter> AddNew;
  public PXAction<ExpenseClaimDetailMaint.ExpenseClaimDetailsFilter> EditDetail;
  public PXAction<ExpenseClaimDetailMaint.ExpenseClaimDetailsFilter> delete;
  public PXAction<ExpenseClaimDetailMaint.ExpenseClaimDetailsFilter> viewClaim;
  public PXFilter<ExpenseClaimDetailMaint.ExpenseClaimDetailsFilter> Filter;
  public PXSelect<PX.Objects.CT.Contract> DummyContracts;
  [PXFilterable(new System.Type[] {})]
  [PXViewName("ClaimDetails")]
  public PXFilteredProcessingJoin<EPExpenseClaimDetails, ExpenseClaimDetailMaint.ExpenseClaimDetailsFilter, LeftJoin<EPExpenseClaim, On<EPExpenseClaim.refNbr, Equal<EPExpenseClaimDetails.refNbr>>, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<EPExpenseClaimDetails.employeeID>>>>, Where<Current2<ExpenseClaimDetailMaint.ExpenseClaimDetailsFilter.employeeID>, IsNotNull, And<EPExpenseClaimDetails.employeeID, Equal<Current2<ExpenseClaimDetailMaint.ExpenseClaimDetailsFilter.employeeID>>, Or<Current2<ExpenseClaimDetailMaint.ExpenseClaimDetailsFilter.employeeID>, IsNull, And<Where<EPEmployee.defContactID, Equal<Current<AccessInfo.contactID>>, Or<EPExpenseClaimDetails.createdByID, Equal<Current<AccessInfo.userID>>, Or<EPExpenseClaimDetails.employeeID, WingmanUser<Current<AccessInfo.userID>, EPDelegationOf.expenses>, Or<EPEmployee.defContactID, IsSubordinateOfContact<Current<AccessInfo.contactID>>, Or<EPExpenseClaimDetails.noteID, Approver<Current<AccessInfo.contactID>>, Or<EPExpenseClaim.noteID, Approver<Current<AccessInfo.contactID>>>>>>>>>>>>, OrderBy<Desc<EPExpenseClaimDetails.claimDetailID>>> ClaimDetails;
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> baccount;
  [PXHidden]
  public PXSelect<PX.Objects.AP.Vendor> vendor;
  [PXHidden]
  public PXSelect<EPEmployee> employee;
  [PXHidden]
  public PXSelect<EPTax> TaxesRows;
  [PXHidden]
  public PXSelect<EPTaxTran> Taxes;

  [PXSelector(typeof (Users.pKID), SubstituteKey = typeof (Users.fullName), DescriptionField = typeof (Users.fullName), CacheGlobal = true)]
  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Created by", Enabled = false)]
  protected virtual void EPExpenseClaimDetails_CreatedByID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true)]
  protected virtual void EPExpenseClaimDetails_TaxCategoryID_CacheAttached(PXCache sender)
  {
  }

  [PXInsertButton]
  [PXUIField(DisplayName = "")]
  [PXEntryScreenRights(typeof (EPExpenseClaimDetails), "Insert")]
  protected virtual void addNew()
  {
    using (new PXPreserveScope())
    {
      ExpenseClaimDetailEntry instance = (ExpenseClaimDetailEntry) PXGraph.CreateInstance(typeof (ExpenseClaimDetailEntry));
      ((PXGraph) instance).Clear((PXClearOption) 3);
      ((PXSelectBase<EPExpenseClaimDetails>) instance.ClaimDetails).Insert((EPExpenseClaimDetails) ((PXSelectBase) instance.ClaimDetails).Cache.CreateInstance());
      ((PXSelectBase) instance.ClaimDetails).Cache.IsDirty = false;
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 4);
    }
  }

  [PXEditDetailButton]
  [PXUIField]
  protected virtual void editDetail()
  {
    EPExpenseClaimDetails current = ((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current;
    if (current == null)
      return;
    PXRedirectHelper.TryRedirect((PXGraph) this, (object) current, (PXRedirectHelper.WindowMode) 4);
  }

  [PXDeleteButton]
  [PXUIField(DisplayName = "")]
  [PXEntryScreenRights(typeof (EPExpenseClaimDetails))]
  protected void Delete()
  {
    if (((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current == null || !((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current.ClaimDetailID.HasValue)
      return;
    ExpenseClaimDetailEntry instance = (ExpenseClaimDetailEntry) PXGraph.CreateInstance(typeof (ExpenseClaimDetailEntry));
    foreach (PXResult<EPExpenseClaimDetails> pxResult in ((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Select(Array.Empty<object>()))
    {
      EPExpenseClaimDetails expenseClaimDetails = PXResult<EPExpenseClaimDetails>.op_Implicit(pxResult);
      if (expenseClaimDetails == ((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current || expenseClaimDetails.Selected.GetValueOrDefault())
      {
        if (expenseClaimDetails.RefNbr != null)
          throw new PXException("This receipt is submitted and can not be deleted.");
        ((PXGraph) instance).Clear((PXClearOption) 3);
        ((PXSelectBase<EPExpenseClaimDetails>) instance.ClaimDetails).Current = PXResultset<EPExpenseClaimDetails>.op_Implicit(((PXSelectBase<EPExpenseClaimDetails>) instance.ClaimDetails).Search<EPExpenseClaimDetails.claimDetailID>((object) expenseClaimDetails.ClaimDetailID, Array.Empty<object>()));
        ((PXAction) instance.Delete).Press();
        ((PXSelectBase) this.ClaimDetails).Cache.Delete((object) expenseClaimDetails);
        ((PXSelectBase) this.ClaimDetails).Cache.IsDirty = false;
      }
    }
  }

  [PXButton]
  protected virtual void ViewClaim()
  {
    if (((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current == null || ((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current.RefNbr == null)
      return;
    EPExpenseClaim epExpenseClaim = PXResultset<EPExpenseClaim>.op_Implicit(PXSelectBase<EPExpenseClaim, PXSelect<EPExpenseClaim, Where<EPExpenseClaim.refNbr, Equal<Required<EPExpenseClaimDetails.refNbr>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) ((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current.RefNbr
    }));
    if (epExpenseClaim == null)
      return;
    PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<ExpenseClaimEntry>(), (object) epExpenseClaim, (PXRedirectHelper.WindowMode) 3);
  }

  public ExpenseClaimDetailMaint()
  {
    // ISSUE: method pointer
    ((PXProcessingBase<EPExpenseClaimDetails>) this.ClaimDetails).SetProcessDelegate(new PXProcessingBase<EPExpenseClaimDetails>.ProcessListDelegate((object) null, __methodptr(ClaimDetail)));
    ((PXProcessing<EPExpenseClaimDetails>) this.ClaimDetails).SetProcessCaption("Claim");
    ((PXProcessing<EPExpenseClaimDetails>) this.ClaimDetails).SetProcessAllCaption("Claim All");
    ((PXProcessingBase<EPExpenseClaimDetails>) this.ClaimDetails).SetSelected<EPExpenseClaimDetails.selected>();
    ((PXSelectBase) this.ClaimDetails).AllowDelete = false;
    ((PXSelectBase) this.ClaimDetails).AllowInsert = false;
    ((PXSelectBase) this.ClaimDetails).AllowUpdate = true;
    PXUIFieldAttribute.SetVisible<EPExpenseClaimDetails.branchID>(((PXSelectBase) this.ClaimDetails).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<EPExpenseClaimDetails.createdByID>(((PXSelectBase) this.ClaimDetails).Cache, (object) null, false);
    PXCache cache = ((PXSelectBase) this.ClaimDetails).Cache;
    PXEntryScreenRightsAttribute screenRightsAttribute = new PXEntryScreenRightsAttribute(typeof (EPExpenseClaimDetails), "Claim");
    ((PXEventSubscriberAttribute) screenRightsAttribute).CacheAttached(cache);
    PXFieldSelectingEventArgs selectingEventArgs = new PXFieldSelectingEventArgs((object) null, (object) null, true, false);
    screenRightsAttribute.FieldSelecting(cache, selectingEventArgs);
    if (!(selectingEventArgs.ReturnState is PXFieldState returnState))
      return;
    ((PXProcessing<EPExpenseClaimDetails>) this.ClaimDetails).SetProcessEnabled(returnState.Enabled);
    ((PXProcessing<EPExpenseClaimDetails>) this.ClaimDetails).SetProcessVisible(returnState.Visible);
    ((PXProcessing<EPExpenseClaimDetails>) this.ClaimDetails).SetProcessAllEnabled(returnState.Enabled);
    ((PXProcessing<EPExpenseClaimDetails>) this.ClaimDetails).SetProcessAllVisible(returnState.Visible);
  }

  protected virtual void EPExpenseClaimDetails_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    this.SetDeleteButtonEnabled(row);
    if (row == null)
      return;
    bool? nullable = row.LegacyReceipt;
    int num;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.Released;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        num = !string.IsNullOrEmpty(row.TaxZoneID) ? 1 : 0;
        goto label_5;
      }
    }
    num = 0;
label_5:
    bool flag1 = num != 0;
    string taxZoneId = ExpenseClaimDetailGraphExtBase<ExpenseClaimDetailEntry>.GetTaxZoneID((PXGraph) this, PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Required<EPExpenseClaimDetails.employeeID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.EmployeeID
    })));
    bool flag2 = string.IsNullOrEmpty(row.TaxZoneID) && !string.IsNullOrEmpty(taxZoneId);
    UIState.RaiseOrHideError<EPExpenseClaimDetails.claimDetailID>(cache, (object) row, flag1 | flag2, flag2 ? "Because the employee who is claiming the expenses has a tax zone specified, you may need to also specify a tax zone for the receipt." : "This receipt was created in an old version of Acumatica ERP. Taxes for this receipt will be calculated upon an update of the Tax Zone, Tax Category, Tax Calculation Mode, or Amount field.", (PXErrorLevel) 3);
  }

  protected virtual void EPExpenseClaimDetails_Selected_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    if (row == null || row.RefNbr == null || !(bool) e.NewValue)
      return;
    sender.RaiseExceptionHandling<EPExpenseClaimDetails.selected>((object) row, e.NewValue, (Exception) new PXSetPropertyException("This receipt has already been claimed.", (PXErrorLevel) 3));
  }

  [CurrencyInfo(ModuleCode = "EP", CuryIDField = "curyID", CuryDisplayName = "Currency", Enabled = false)]
  [PXDBLong]
  protected virtual void EPExpenseClaimDetails_CuryInfoID_CacheAttached(PXCache cache)
  {
  }

  [PXDBLong]
  protected virtual void EPExpenseClaimDetails_ClaimCuryInfoID_CacheAttached(PXCache cache)
  {
  }

  private static void ClaimDetail(List<EPExpenseClaimDetails> details)
  {
    ExpenseClaimDetailMaint.ClaimDetail(details, false, false);
  }

  private static void ClaimDetail(
    List<EPExpenseClaimDetails> details,
    bool isApiContext,
    bool singleOperation)
  {
    ExpenseClaimEntry expenseClaimEntry = PXGraph.CreateInstance<ExpenseClaimEntry>();
    PXSetup<EPSetup> pxSetup = new PXSetup<EPSetup>(PXGraph.CreateInstance(typeof (ExpenseClaimDetailEntry)));
    int? nullable;
    int num1;
    if (PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>())
    {
      nullable = ((PXSelectBase<EPSetup>) pxSetup).Current.ClaimDetailsAssignmentMapID;
      num1 = nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag1 = num1 != 0;
    bool flag2 = false;
    bool flag3 = false;
    Dictionary<string, EPExpenseClaim> source = new Dictionary<string, EPExpenseClaim>();
    Dictionary<string, EPExpenseClaimDetails> dictionary = details.ToDictionary<EPExpenseClaimDetails, string>((Func<EPExpenseClaimDetails, string>) (d => d.ClaimDetailCD));
    details = details.Select<EPExpenseClaimDetails, EPExpenseClaimDetails>((Func<EPExpenseClaimDetails, EPExpenseClaimDetails>) (d => (EPExpenseClaimDetails) PrimaryKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.claimDetailCD>.Find((PXGraph) expenseClaimEntry, (EPExpenseClaimDetails.claimDetailCD) d, (PKFindOptions) 0))).ToList<EPExpenseClaimDetails>();
    IEnumerable<ExpenseClaimDetailMaint.Receipts> receiptses = !((PXSelectBase<EPSetup>) pxSetup).Current.AllowMixedTaxSettingInClaims.GetValueOrDefault() ? details.Where<EPExpenseClaimDetails>((Func<EPExpenseClaimDetails, bool>) (item => string.IsNullOrEmpty(item.RefNbr))).OrderBy<EPExpenseClaimDetails, int?>((Func<EPExpenseClaimDetails, int?>) (detail => detail.ClaimDetailID)).GroupBy(item => new
    {
      EmployeeID = item.EmployeeID,
      BranchID = item.BranchID,
      CustomerID = item.CustomerID,
      CustomerLocationID = item.CustomerLocationID,
      TaxZoneID = item.TaxZoneID,
      TaxCalcMode = item.TaxCalcMode,
      ClaimCuryID = ExpenseClaimDetailMaint.GetClaimCuryID((PXGraph) expenseClaimEntry, item)
    }, (key, item) => new ExpenseClaimDetailMaint.Receipts()
    {
      employee = key.EmployeeID,
      branch = key.BranchID,
      customer = key.CustomerID,
      customerLocation = key.CustomerLocationID,
      claimCuryID = key.ClaimCuryID,
      details = item
    }) : details.Where<EPExpenseClaimDetails>((Func<EPExpenseClaimDetails, bool>) (item => string.IsNullOrEmpty(item.RefNbr))).OrderBy<EPExpenseClaimDetails, int?>((Func<EPExpenseClaimDetails, int?>) (detail => detail.ClaimDetailID)).GroupBy(item => new
    {
      EmployeeID = item.EmployeeID,
      BranchID = item.BranchID,
      CustomerID = item.CustomerID,
      CustomerLocationID = item.CustomerLocationID,
      ClaimCuryID = ExpenseClaimDetailMaint.GetClaimCuryID((PXGraph) expenseClaimEntry, item)
    }, (key, item) => new ExpenseClaimDetailMaint.Receipts()
    {
      employee = key.EmployeeID,
      branch = key.BranchID,
      customer = key.CustomerID,
      customerLocation = key.CustomerLocationID,
      claimCuryID = key.ClaimCuryID,
      details = item
    });
    string str = (string) null;
    foreach (ExpenseClaimDetailMaint.Receipts receipts in receiptses)
    {
      flag2 = false;
      flag3 = false;
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        ((PXGraph) expenseClaimEntry).Clear();
        ((PXGraph) expenseClaimEntry).SelectTimeStamp();
        EPExpenseClaim instance = (EPExpenseClaim) ((PXSelectBase) expenseClaimEntry.ExpenseClaim).Cache.CreateInstance();
        instance.EmployeeID = receipts.employee;
        instance.BranchID = receipts.branch;
        instance.CustomerID = receipts.customer;
        instance.DocDesc = "Submitted Receipt(s)";
        EPExpenseClaim epExpenseClaim1 = ((PXSelectBase<EPExpenseClaim>) expenseClaimEntry.ExpenseClaim).Update(instance);
        epExpenseClaim1.CuryID = receipts.claimCuryID;
        EPExpenseClaim epExpenseClaim2 = ((PXSelectBase<EPExpenseClaim>) expenseClaimEntry.ExpenseClaim).Update(epExpenseClaim1);
        epExpenseClaim2.CustomerLocationID = receipts.customerLocation;
        epExpenseClaim2.TaxCalcMode = receipts.details.First<EPExpenseClaimDetails>().TaxCalcMode;
        epExpenseClaim2.TaxZoneID = receipts.details.First<EPExpenseClaimDetails>().TaxZoneID;
        foreach (EPExpenseClaimDetails detail in receipts.details)
        {
          EPExpenseClaimDetails expenseClaimDetails;
          if (dictionary.TryGetValue(detail.ClaimDetailCD, out expenseClaimDetails))
            PXProcessing<EPExpenseClaimDetails>.SetCurrentItem((object) expenseClaimDetails);
          else
            PXProcessing<EPExpenseClaimDetails>.SetCurrentItem((object) detail);
          if (detail.Approved.GetValueOrDefault())
          {
            try
            {
              if (detail.IsPaidWithCard)
              {
                EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>.Config>.Select((PXGraph) expenseClaimEntry, new object[1]
                {
                  (object) receipts.employee
                }));
                if (!epEmployee.AllowOverrideCury.GetValueOrDefault() && detail.CardCuryID != epEmployee.CuryID)
                {
                  str = PXMessages.Localize("A claim cannot be created for the expense receipt because the employee currency differs from the corporate card currency of the expense receipt and cannot be overridden. Enable currency override for the employee on the Employees (EP203000) form first.");
                  flag2 = true;
                }
              }
              if (!flag2)
              {
                Decimal? tipAmt = detail.TipAmt;
                Decimal num2 = 0M;
                if (!(tipAmt.GetValueOrDefault() == num2 & tipAmt.HasValue))
                {
                  nullable = ((PXSelectBase<EPSetup>) pxSetup).Current.NonTaxableTipItem;
                  if (!nullable.HasValue)
                  {
                    str = "To be able to specify a nonzero tip amount in the expense receipt, specify an appropriate tip item in the Non-Taxable Tip Item box on the Time & Expenses Preferences (EP101000) form.";
                    flag2 = true;
                  }
                }
              }
              if (!flag2)
              {
                expenseClaimEntry.ReceiptEntryExt.SubmitReceiptExt(((PXSelectBase) expenseClaimEntry.ExpenseClaim).Cache, ((PXSelectBase) expenseClaimEntry.ExpenseClaimDetails).Cache, ((PXSelectBase<EPExpenseClaim>) expenseClaimEntry.ExpenseClaim).Current, detail);
                ((PXAction) expenseClaimEntry.Save).Press();
                if (!source.ContainsKey(epExpenseClaim2.RefNbr))
                  source.Add(epExpenseClaim2.RefNbr, epExpenseClaim2);
                detail.RefNbr = epExpenseClaim2.RefNbr;
                if (expenseClaimDetails != null)
                  expenseClaimDetails.RefNbr = epExpenseClaim2.RefNbr;
                PXProcessing<EPExpenseClaimDetails>.SetProcessed();
              }
            }
            catch (Exception ex)
            {
              str = ex.Message;
              flag2 = true;
            }
          }
          else
          {
            str = flag1 ? "This receipt must be approved." : "This receipt must be taken off hold.";
            flag3 = true;
          }
          if (str != null)
            PXProcessing<EPExpenseClaimDetails>.SetError(str);
        }
        if (!flag2)
          transactionScope.Complete();
      }
    }
    if (!flag2 && !flag3)
    {
      if (source.Count != 1 || isApiContext)
        return;
      expenseClaimEntry = PXGraph.CreateInstance<ExpenseClaimEntry>();
      PXRedirectHelper.TryRedirect((PXGraph) expenseClaimEntry, (object) source.First<KeyValuePair<string, EPExpenseClaim>>().Value, (PXRedirectHelper.WindowMode) 4);
    }
    else
    {
      PXProcessing<EPExpenseClaimDetails>.SetCurrentItem((object) null);
      if (singleOperation)
        throw new PXException(str);
      throw new PXException("One or multiple errors occurred during the processing of receipts.");
    }
  }

  public static string GetClaimCuryID(PXGraph graph, EPExpenseClaimDetails receipt)
  {
    if (receipt.CorpCardID.HasValue)
      return CACorpCardsMaint.GetCardCashAccount(graph, receipt.CorpCardID).CuryID;
    return PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) receipt.EmployeeID
    })).CuryID ?? PXResultset<Company>.op_Implicit(PXSelectBase<Company, PXSelect<Company>.Config>.Select(graph, Array.Empty<object>())).BaseCuryID;
  }

  public static void ClaimSingleDetail(EPExpenseClaimDetails details, bool isApiContext = false)
  {
    ExpenseClaimDetailMaint.ClaimDetail(new List<EPExpenseClaimDetails>()
    {
      details
    }, isApiContext, true);
  }

  private void SetDeleteButtonEnabled(EPExpenseClaimDetails row)
  {
    if (row != null)
    {
      bool? selected = row.Selected;
      bool flag1 = false;
      if (!(selected.GetValueOrDefault() == flag1 & selected.HasValue))
      {
        if (PXResultset<EPExpenseClaim>.op_Implicit(PXSelectBase<EPExpenseClaim, PXSelect<EPExpenseClaim, Where<EPExpenseClaim.refNbr, Equal<Required<EPExpenseClaimDetails.refNbr>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1], new object[1]
        {
          (object) row.RefNbr
        })) != null)
        {
          ((PXAction) this.delete).SetEnabled(false);
          ((PXSelectBase) this.ClaimDetails).Cache.IsDirty = false;
          return;
        }
        bool flag2 = row.LegacyReceipt.GetValueOrDefault() && !string.IsNullOrEmpty(row.RefNbr);
        bool flag3 = PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && ((PXSelectBase<EPSetup>) this.epsetup).Current.ClaimDetailsAssignmentMapID.HasValue;
        ((PXAction) this.delete).SetEnabled((row.Hold.GetValueOrDefault() || !flag3) && !flag2);
        ((PXSelectBase) this.ClaimDetails).Cache.IsDirty = false;
        return;
      }
    }
    ((PXAction) this.delete).SetEnabled(true);
    ((PXSelectBase) this.ClaimDetails).Cache.IsDirty = false;
  }

  public class ExpenseClaimDetailMaintReceiptExt : 
    ExpenseClaimDetailGraphExtBase<ExpenseClaimDetailMaint>
  {
    public override PXSelectBase<EPExpenseClaimDetails> Receipts
    {
      get => (PXSelectBase<EPExpenseClaimDetails>) this.Base.ClaimDetails;
    }
  }

  private class Receipts
  {
    public int? employee;
    public int? branch;
    public int? customer;
    public int? customerLocation;
    public string claimCuryID;
    public IEnumerable<EPExpenseClaimDetails> details;
  }

  [Serializable]
  public class ExpenseClaimDetailsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt]
    [PXUIField(DisplayName = "Employee")]
    [PXDefault(typeof (Search<EPEmployee.bAccountID, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>))]
    [PXSubordinateAndWingmenSelector(typeof (EPDelegationOf.expenses))]
    [PXFieldDescription]
    public virtual int? EmployeeID { get; set; }

    public abstract class employeeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ExpenseClaimDetailMaint.ExpenseClaimDetailsFilter.employeeID>
    {
    }
  }
}

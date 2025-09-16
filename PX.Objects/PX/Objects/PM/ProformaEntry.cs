// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProformaEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.DependencyInjection;
using PX.Data.WorkflowAPI;
using PX.LicensePolicy;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.IN;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

#nullable enable
namespace PX.Objects.PM;

[Serializable]
public class ProformaEntry : PXGraph<
#nullable disable
ProformaEntry, PMProforma>, IGraphWithInitialization
{
  public const string ProformaInvoiceReport = "PM642000";
  public const string ProformaNotificationCD = "PROFORMA";
  public readonly ProformaEntry.ProformaTotalsCounter.AmountBaseKey PayByLineOffKey = new ProformaEntry.ProformaTotalsCounter.AmountBaseKey(0, CostCodeAttribute.DefaultCostCode.GetValueOrDefault(), PMInventorySelectorAttribute.EmptyInventoryID, 0);
  public readonly ProformaEntry.ProformaTotalsCounter TotalsCounter = new ProformaEntry.ProformaTotalsCounter();
  public bool IsCorrectionProcess;
  [PXViewName("Pro Forma Invoice")]
  public FbqlSelect<SelectFromBase<PMProforma, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PMProject>.On<BqlOperand<
  #nullable enable
  PMProject.contractID, IBqlInt>.IsEqual<
  #nullable disable
  PMProforma.projectID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMProject.contractID, 
  #nullable disable
  IsNull>>>>.Or<MatchUserFor<PMProject>>>>>.And<BqlOperand<
  #nullable enable
  PMProforma.corrected, IBqlBool>.IsNotEqual<
  #nullable disable
  True>>>, PMProforma>.View Document;
  public PXFilter<ProformaEntry.PMProformaOverflow> Overflow;
  public PXSelect<PMProforma, Where<PMProforma.refNbr, Equal<Current<PMProforma.refNbr>>, And<PMProforma.revisionID, Equal<Current<PMProforma.revisionID>>>>> DocumentSettings;
  [PXImport(typeof (PMProforma))]
  public ProgressLineSelect ProgressiveLines;
  [PXImport(typeof (PMProforma))]
  public TransactLineSelect TransactionLines;
  public PXSetup<PX.Objects.AR.ARSetup> arSetup;
  public PXSetup<PX.Objects.GL.Branch>.Where<BqlOperand<
  #nullable enable
  PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMProforma.branchID, IBqlInt>.AsOptional>> branch;
  public 
  #nullable disable
  PXSelect<PMProformaRevision, Where<PMProformaRevision.refNbr, Equal<Current<PMProforma.refNbr>>, And<PMProformaRevision.revisionID, NotEqual<Current<PMProforma.revisionID>>>>, OrderBy<Asc<PMProformaRevision.revisionID>>> Revisions;
  public PXOrderedSelect<PMProforma, PMProformaTransactLine, Where<PMProformaTransactLine.refNbr, Equal<Current<PMProforma.refNbr>>, And<PMProformaTransactLine.revisionID, Equal<Current<PMProforma.revisionID>>, And<PMProformaTransactLine.type, Equal<PMProformaLineType.transaction>>>>, OrderBy<Asc<PMProformaLine.sortOrder, Asc<PMProformaTransactLine.lineNbr>>>> Trans;
  public PXSelect<PMTran, Where<PMTran.proformaRefNbr, Equal<Current<PMProformaTransactLine.refNbr>>, And<PMTran.proformaLineNbr, Equal<Current<PMProformaTransactLine.lineNbr>>>>> Details;
  public PXSelect<PMTran, Where<PMTran.proformaRefNbr, Equal<Current<PMProformaTransactLine.refNbr>>>> AllReferencedTransactions;
  public PXSelect<PMTran> Unbilled;
  public PXSelect<PMBudgetAccum> Budget;
  public PXSelect<PMBillingRecord> BillingRecord;
  public PXSelect<PX.Objects.AR.ARInvoice> Invoices;
  [PXViewName("Project")]
  public PXSetup<PMProject>.Where<BqlOperand<
  #nullable enable
  PMProject.contractID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMProforma.projectID, IBqlInt>.FromCurrent>> Project;
  [PXViewName("Customer")]
  public 
  #nullable disable
  PXSetup<PX.Objects.AR.Customer>.Where<BqlOperand<
  #nullable enable
  PX.Objects.AR.Customer.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMProforma.customerID, IBqlInt>.AsOptional>> Customer;
  public 
  #nullable disable
  PXSetup<PX.Objects.CR.Location>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CR.Location.bAccountID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PMProforma.customerID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PX.Objects.CR.Location.locationID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMProforma.locationID, IBqlInt>.AsOptional>>> Location;
  [PXViewName("Approval")]
  public 
  #nullable disable
  EPApprovalAutomation<PMProforma, PMProforma.approved, PMProforma.rejected, PMProforma.hold, PMSetupProformaApproval> Approval;
  public PXSelect<PMAddress, Where<PMAddress.addressID, Equal<Current<PMProforma.billAddressID>>>> Billing_Address;
  public PXSelect<PMContact, Where<PMContact.contactID, Equal<Current<PMProforma.billContactID>>>> Billing_Contact;
  [PXViewName("PM Address")]
  public PXSelect<PMShippingAddress, Where<PMShippingAddress.addressID, Equal<Current<PMProforma.shipAddressID>>>> Shipping_Address;
  [PXViewName("Project Contact")]
  public PXSelect<PMShippingContact, Where<PMShippingContact.contactID, Equal<Current<PMProforma.shipContactID>>>> Shipping_Contact;
  [PXCopyPasteHiddenView]
  public PXSelect<PMTax, Where<PMTax.refNbr, Equal<Current<PMProforma.refNbr>>, And<PMTax.revisionID, Equal<Current<PMProforma.revisionID>>>>, OrderBy<Asc<PMTax.refNbr, Asc<PMTax.taxID>>>> Tax_Rows;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<PMTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<PMTaxTran.taxID>>>, Where<PMTaxTran.refNbr, Equal<Current<PMProforma.refNbr>>, And<PMTaxTran.revisionID, Equal<Current<PMProforma.revisionID>>>>> Taxes;
  public PXSetup<PMSetup> Setup;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXSetup<PX.Objects.TX.TaxZone>.Where<BqlOperand<
  #nullable enable
  PX.Objects.TX.TaxZone.taxZoneID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMProforma.taxZoneID, IBqlString>.FromCurrent>> taxzone;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public 
  #nullable disable
  PXSelect<PMUnbilledDailySummaryAccum> UnbilledSummary;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelect<PX.Objects.PM.Lite.PMBudget> BudgetProperties;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelect<PMRevenueBudget> dummyRevenueBudget;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelect<BAccountR> dummyAccountR;
  [PXViewName("Main Contact")]
  public PXSelect<PX.Objects.CR.Contact> DefaultCompanyContact;
  public Dictionary<int, List<PMTran>> cachedReferencedTransactions;
  public PXAction<PMProforma> release;
  public PXAction<PMProforma> proformaReport;
  public PXAction<PMProforma> send;
  public PXAction<PMProforma> viewTransactionDetails;
  public PXAction<PMProforma> autoApplyPrepayments;
  public PXAction<PMProforma> viewTranDocument;
  public PXAction<PMProforma> uploadUnbilled;
  public PXAction<PMProforma> appendSelected;
  public PXAction<PMProforma> viewProgressLineTask;
  public PXAction<PMProforma> viewTransactLineTask;
  public PXAction<PMProforma> viewProgressLineInventory;
  public PXAction<PMProforma> viewTransactLineInventory;
  public PXAction<PMProforma> viewVendor;
  public PXAction<PMProforma> viewARDocument;
  public PXAction<PMProforma> uploadFromBudget;
  public PXAction<PMProforma> removeHold;
  public PXAction<PMProforma> hold;
  public PXAction<PMProforma> validateAddresses;
  public PXAction<PMProforma> correct;
  public PXAction<PMProforma> mergeWithProgress;
  public PXAction<PMProforma> revertMergeWithProgress;
  public PXAction<PMProforma> fillRevenueTasks;
  public PXWorkflowEventHandler<PMProforma> OnRelease;
  private bool _accountIdUpdating;
  protected bool RecalculatingContractRetainage;
  private Dictionary<BudgetKeyTuple, ProformaEntry.PendingChange> progressiveLinesLookup;

  [PXMergeAttributes]
  [ProformaTransactLineStatus]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMProformaTransactLine.option> e)
  {
  }

  [PXDefault("CU")]
  protected virtual void _(PX.Data.Events.CacheAttached<BAccountR.type> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Actual Amount", Enabled = false, Visible = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMRevenueBudget.curyActualAmount> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Draft Invoice Amount", Enabled = false, Visible = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMRevenueBudget.curyInvoicedAmount> e)
  {
  }

  [PXDBDate]
  [PXDefault(typeof (PMProforma.invoiceDate))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.docDate> e)
  {
  }

  [PXDBInt]
  [PXDefault(typeof (PMProforma.customerID))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.bAccountID> e)
  {
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault(typeof (PMProforma.description))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.descr> e)
  {
  }

  [PXDBLong]
  [CurrencyInfo(typeof (PMProforma.curyInfoID))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.curyInfoID> e)
  {
  }

  [PXDBDecimal]
  [PXDefault(typeof (PMProforma.curyDocTotal))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.curyTotalAmount> e)
  {
  }

  [PXDBDecimal]
  [PXDefault(typeof (PMProforma.docTotal))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.totalAmount> e)
  {
  }

  [Account(null, typeof (Search2<PX.Objects.GL.Account.accountID, LeftJoin<PMAccountGroup, On<PMAccountGroup.groupID, Equal<Current<PMTran.accountGroupID>>>>, Where<PMAccountGroup.type, NotEqual<PMAccountType.offBalance>, And<PX.Objects.GL.Account.accountGroupID, Equal<Current<PMTran.accountGroupID>>, Or<PMAccountGroup.type, Equal<PMAccountType.offBalance>, Or<PMAccountGroup.groupID, IsNull>>>>>), DisplayName = "Debit Account", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.accountID> e)
  {
  }

  [SubAccount(typeof (PMTran.accountID), DisplayName = "Debit Subaccount", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.subID> e)
  {
  }

  [Account(DisplayName = "Credit Account", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.offsetAccountID> e)
  {
  }

  [SubAccount(typeof (PMTran.offsetAccountID), DisplayName = "Credit Subaccount", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.offsetSubID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Amount")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.projectCuryAmount> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Currency", FieldClass = "ProjectMultiCurrency")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.projectCuryID> e)
  {
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDBDefault(typeof (PMProforma.taxZoneID))]
  [PXUIField(DisplayName = "Customer Tax Zone", Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTaxTran.taxZoneID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBString(15, IsUnicode = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMProformaLine.taxCategoryID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.billAddressID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.billContactID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Project Currency", IsReadOnly = true, FieldClass = "ProjectMultiCurrency")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.curyID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Pro Forma Reference Nbr.")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMBillingRecord.proformaRefNbr> e)
  {
  }

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  [InjectDependency]
  private ICurrentUserInformationProvider _currentUserInformationProvider { get; set; }

  [InjectDependency]
  public IProjectMultiCurrency MultiCurrencyService { get; set; }

  public virtual IEnumerable transactionLines()
  {
    if (!this.IsLimitsEnabled())
      return (IEnumerable) ((PXSelectBase<PMProformaTransactLine>) this.Trans).Select(Array.Empty<object>());
    PXOrderedSelect<PMProforma, PMProformaTransactLine, Where<PMProformaTransactLine.refNbr, Equal<Current<PMProforma.refNbr>>, And<PMProformaTransactLine.revisionID, Equal<Current<PMProforma.revisionID>>, And<PMProformaTransactLine.type, Equal<PMProformaLineType.transaction>>>>, OrderBy<Asc<PMProformaLine.sortOrder, Asc<PMProformaTransactLine.lineNbr>>>> pxOrderedSelect = new PXOrderedSelect<PMProforma, PMProformaTransactLine, Where<PMProformaTransactLine.refNbr, Equal<Current<PMProforma.refNbr>>, And<PMProformaTransactLine.revisionID, Equal<Current<PMProforma.revisionID>>, And<PMProformaTransactLine.type, Equal<PMProformaLineType.transaction>>>>, OrderBy<Asc<PMProformaLine.sortOrder, Asc<PMProformaTransactLine.lineNbr>>>>((PXGraph) this);
    List<PXResult<PMProformaTransactLine>> pxResultList = new List<PXResult<PMProformaTransactLine>>();
    PXResultset<PMRevenueBudget> pxResultset1 = ((PXSelectBase<PMRevenueBudget>) new PXSelect<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Required<PMRevenueBudget.projectID>>, And<PMRevenueBudget.type, Equal<AccountType.income>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID
    });
    Dictionary<BudgetKeyTuple, Tuple<Decimal, Decimal>> dictionary1 = new Dictionary<BudgetKeyTuple, Tuple<Decimal, Decimal>>();
    Dictionary<BudgetKeyTuple, Decimal> dictionary2 = new Dictionary<BudgetKeyTuple, Decimal>();
    PXResultset<PMProformaTransactLine> pxResultset2 = ((PXSelectBase<PMProformaTransactLine>) pxOrderedSelect).Select(Array.Empty<object>());
    foreach (PXResult<PMProformaTransactLine> pxResult in pxResultset2)
    {
      PMProformaTransactLine line = PXResult<PMProformaTransactLine>.op_Implicit(pxResult);
      if (!line.IsPrepayment.GetValueOrDefault())
      {
        int? nullable = line.RevenueTaskID;
        int num1 = nullable ?? line.TaskID.GetValueOrDefault();
        BudgetKeyTuple key;
        ref BudgetKeyTuple local = ref key;
        nullable = line.ProjectID;
        int valueOrDefault1 = nullable.GetValueOrDefault();
        int projectTaskID = num1;
        nullable = this.GetProjectedAccountGroup((PMProformaLine) line);
        int valueOrDefault2 = nullable.GetValueOrDefault();
        int inventoryID;
        if (!(((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "I"))
        {
          inventoryID = PMInventorySelectorAttribute.EmptyInventoryID;
        }
        else
        {
          nullable = line.InventoryID;
          inventoryID = nullable ?? PMInventorySelectorAttribute.EmptyInventoryID;
        }
        int costCodeID;
        if (!(((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "T"))
        {
          nullable = line.CostCodeID;
          costCodeID = nullable ?? CostCodeAttribute.GetDefaultCostCode();
        }
        else
          costCodeID = CostCodeAttribute.GetDefaultCostCode();
        local = new BudgetKeyTuple(valueOrDefault1, projectTaskID, valueOrDefault2, inventoryID, costCodeID);
        Decimal num2;
        dictionary2[key] = !dictionary2.TryGetValue(key, out num2) ? this.GetAmountInProjectCurrency(line.CuryLineTotal) : num2 + this.GetAmountInProjectCurrency(line.CuryLineTotal);
      }
    }
    foreach (PXResult<PMRevenueBudget> pxResult in pxResultset1)
    {
      PMRevenueBudget pmRevenueBudget = PXResult<PMRevenueBudget>.op_Implicit(pxResult);
      if (pmRevenueBudget.LimitAmount.GetValueOrDefault())
      {
        BudgetKeyTuple key = BudgetKeyTuple.Create((IProjectFilter) pmRevenueBudget);
        Decimal actualAmountWithTaxes = this.GetCuryActualAmountWithTaxes(pmRevenueBudget);
        Decimal? nullable = pmRevenueBudget.CuryInvoicedAmount;
        Decimal valueOrDefault3 = nullable.GetValueOrDefault();
        Decimal num3 = actualAmountWithTaxes + valueOrDefault3 + this.CalculatePendingInvoicedAmount(pmRevenueBudget.ProjectID, pmRevenueBudget.ProjectTaskID, pmRevenueBudget.AccountGroupID, pmRevenueBudget.InventoryID, pmRevenueBudget.CostCodeID);
        Decimal num4;
        dictionary2.TryGetValue(key, out num4);
        Decimal num5 = num4;
        Decimal num6 = num3 - num5;
        nullable = pmRevenueBudget.CuryMaxAmount;
        Decimal valueOrDefault4 = nullable.GetValueOrDefault();
        nullable = pmRevenueBudget.CuryMaxAmount;
        Decimal num7 = Math.Max(0M, nullable.GetValueOrDefault() - num6);
        Tuple<Decimal, Decimal> tuple = new Tuple<Decimal, Decimal>(valueOrDefault4, num7);
        dictionary1.Add(key, tuple);
      }
    }
    Dictionary<int, Decimal> dictionary3 = new Dictionary<int, Decimal>();
    Decimal overflowTotal = 0M;
    foreach (PXResult<PMProformaTransactLine> pxResult in pxResultset2)
    {
      PMProformaTransactLine line = PXResult<PMProformaTransactLine>.op_Implicit(pxResult);
      if (!line.IsPrepayment.GetValueOrDefault())
      {
        int? nullable1 = line.RevenueTaskID;
        int num8 = nullable1 ?? line.TaskID.GetValueOrDefault();
        BudgetKeyTuple key1;
        ref BudgetKeyTuple local1 = ref key1;
        nullable1 = line.ProjectID;
        int valueOrDefault5 = nullable1.GetValueOrDefault();
        int projectTaskID = num8;
        nullable1 = this.GetProjectedAccountGroup((PMProformaLine) line);
        int valueOrDefault6 = nullable1.GetValueOrDefault();
        int inventoryID;
        if (!(((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "I") && !(((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "D"))
        {
          inventoryID = PMInventorySelectorAttribute.EmptyInventoryID;
        }
        else
        {
          nullable1 = line.InventoryID;
          inventoryID = nullable1 ?? PMInventorySelectorAttribute.EmptyInventoryID;
        }
        int costCodeID;
        if (!(((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "T"))
        {
          nullable1 = line.CostCodeID;
          costCodeID = nullable1 ?? CostCodeAttribute.GetDefaultCostCode();
        }
        else
          costCodeID = CostCodeAttribute.GetDefaultCostCode();
        local1 = new BudgetKeyTuple(valueOrDefault5, projectTaskID, valueOrDefault6, inventoryID, costCodeID);
        Tuple<Decimal, Decimal> tuple;
        if (dictionary1.TryGetValue(key1, out tuple))
        {
          line.CuryMaxAmount = new Decimal?(this.GetAmountInBillingCurrency(new Decimal?(tuple.Item1)));
          Decimal num9 = 0M;
          Dictionary<int, Decimal> dictionary4 = dictionary3;
          nullable1 = line.TaskID;
          int key2 = nullable1.Value;
          ref Decimal local2 = ref num9;
          dictionary4.TryGetValue(key2, out local2);
          line.CuryAvailableAmount = new Decimal?(Math.Max(0M, this.GetAmountInBillingCurrency(new Decimal?(tuple.Item2 + num9))));
          Decimal? nullable2;
          if (this.GetAmountInProjectCurrency(line.CuryLineTotal) > 0M)
          {
            nullable2 = line.CuryAvailableAmount;
            Decimal valueOrDefault7 = nullable2.GetValueOrDefault();
            nullable2 = line.CuryLineTotal;
            Decimal valueOrDefault8 = nullable2.GetValueOrDefault();
            Decimal num10 = valueOrDefault7 - valueOrDefault8;
            if (num10 >= num9)
            {
              line.CuryOverflowAmount = new Decimal?(0M);
              dictionary1[key1] = new Tuple<Decimal, Decimal>(tuple.Item1, this.GetAmountInProjectCurrency(new Decimal?(num10 - num9)));
            }
            else
            {
              line.CuryOverflowAmount = new Decimal?(num10 > 0M ? 0M : -num10);
              dictionary1[key1] = new Tuple<Decimal, Decimal>(tuple.Item1, this.GetAmountInProjectCurrency(new Decimal?(num10 - num9)));
              Decimal num11 = overflowTotal;
              nullable2 = line.CuryOverflowAmount;
              Decimal num12 = nullable2.Value;
              overflowTotal = num11 + num12;
            }
          }
          else
          {
            Dictionary<int, Decimal> dictionary5 = dictionary3;
            nullable1 = line.TaskID;
            int key3 = nullable1.Value;
            if (dictionary5.ContainsKey(key3))
            {
              Dictionary<int, Decimal> dictionary6 = dictionary3;
              nullable1 = line.TaskID;
              int key4 = nullable1.Value;
              Dictionary<int, Decimal> dictionary7 = dictionary6;
              int key5 = key4;
              Decimal num13 = dictionary6[key4];
              nullable2 = line.CuryLineTotal;
              Decimal num14 = -nullable2.GetValueOrDefault();
              Decimal num15 = num13 + num14;
              dictionary7[key5] = num15;
            }
            else
            {
              Dictionary<int, Decimal> dictionary8 = dictionary3;
              nullable1 = line.TaskID;
              int key6 = nullable1.Value;
              nullable2 = line.CuryLineTotal;
              Decimal num16 = -nullable2.GetValueOrDefault();
              dictionary8[key6] = num16;
            }
          }
        }
        pxResultList.Add(new PXResult<PMProformaTransactLine>(line));
      }
    }
    this.SetOverflowTotal(overflowTotal);
    return (IEnumerable) pxResultList;
  }

  private void SetOverflowTotal(Decimal overflowTotal)
  {
    ((PXSelectBase<ProformaEntry.PMProformaOverflow>) this.Overflow).Current.CuryOverflowTotal = new Decimal?(overflowTotal);
    if (overflowTotal == 0M)
      ((PXSelectBase<ProformaEntry.PMProformaOverflow>) this.Overflow).Current.OverflowTotal = new Decimal?(overflowTotal);
    else
      ((PXSelectBase<ProformaEntry.PMProformaOverflow>) this.Overflow).Current.OverflowTotal = new Decimal?(((PXGraph) this).GetExtension<ProformaEntry.MultiCurrency>().GetDefaultCurrencyInfo().CuryConvBase(overflowTotal));
    ((PXSelectBase) this.Overflow).View.RequestRefresh();
  }

  public virtual IEnumerable unbilled()
  {
    List<PMTran> pmTranList = new List<PMTran>();
    if (((PXSelectBase<PMProforma>) this.Document).Current == null)
      return (IEnumerable) pmTranList;
    PMBillEngine instance = PXGraph.CreateInstance<PMBillEngine>();
    PMProject project = PMProject.PK.Find((PXGraph) instance, ((PXSelectBase<PMProforma>) this.Document).Current.ProjectID);
    if (project != null)
    {
      List<PMTask> tasks = instance.SelectBillableTasks(project);
      DateTime dateTime = ((PXSelectBase<PMProforma>) this.Document).Current.InvoiceDate.Value.AddDays((double) (instance.IncludeTodaysTransactions ? 1 : 0));
      instance.PreSelectTasksTransactions(((PXSelectBase<PMProforma>) this.Document).Current.ProjectID, tasks, new DateTime?(dateTime));
      foreach (PMTask pmTask in tasks)
      {
        List<PMBillingRule> pmBillingRuleList;
        if (instance.billingRules.TryGetValue(pmTask.BillingID, out pmBillingRuleList))
        {
          foreach (PMBillingRule pmBillingRule in pmBillingRuleList)
          {
            if (pmBillingRule.Type == "T")
            {
              foreach (PMTran tran in instance.SelectBillingBase(pmTask.ProjectID, pmTask.TaskID, pmBillingRule.AccountGroupID, pmBillingRule.IncludeNonBillable.GetValueOrDefault()))
              {
                if (this.CanAddToUnbilledTransactionLookup(tran))
                  pmTranList.Add(tran);
              }
            }
          }
        }
      }
      HashSet<long> longSet = new HashSet<long>();
      foreach (PMTran pmTran1 in pmTranList)
      {
        longSet.Add(pmTran1.TranID.Value);
        PMTran pmTran2 = ((PXSelectBase<PMTran>) this.Unbilled).Locate(pmTran1);
        if (pmTran2 != null && (pmTran2.Billed.GetValueOrDefault() || pmTran2.ExcludedFromBilling.GetValueOrDefault()))
          pmTran1.Selected = new bool?(true);
      }
      foreach (PMTran pmTran in ((PXSelectBase) this.Unbilled).Cache.Updated)
      {
        bool? nullable = pmTran.Billed;
        if (!nullable.GetValueOrDefault())
        {
          nullable = pmTran.ExcludedFromBilling;
          if (!nullable.GetValueOrDefault() && !longSet.Contains(pmTran.TranID.Value))
            pmTranList.Add(pmTran);
        }
      }
    }
    return (IEnumerable) pmTranList;
  }

  protected virtual IEnumerable defaultCompanyContact()
  {
    return (IEnumerable) OrganizationMaint.GetDefaultContactForCurrentOrganization((PXGraph) this);
  }

  [InjectDependency]
  protected IFinPeriodUtils FinPeriodUtils { get; set; }

  [InjectDependency]
  protected IFinPeriodRepository FinPeriodRepository { get; set; }

  public bool SuppressRowSeleted { get; set; }

  public ProformaEntry()
  {
    ((PXSelectBase) this.Setup).Cache.Clear();
    OpenPeriodAttribute.SetValidatePeriod<PMProforma.finPeriodID>(((PXSelectBase) this.Document).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    ((PXAction) this.CopyPaste).SetVisible(false);
    PXCache cach1 = ((PXGraph) this).Caches[typeof (PMAddress)];
    PXCache cach2 = ((PXGraph) this).Caches[typeof (PMContact)];
    PXCache cach3 = ((PXGraph) this).Caches[typeof (PMShippingAddress)];
    PXCache cach4 = ((PXGraph) this).Caches[typeof (PMShippingContact)];
    (((PXSelectBase) this.TransactionLines).Attributes.Find((Predicate<System.Attribute>) (a => a is PXImportAttribute)) as PXImportAttribute).MappingPropertiesInit += new EventHandler<PXImportAttribute.MappingPropertiesInitEventArgs>(this.MappingPropertiesInit);
  }

  protected virtual void MappingPropertiesInit(
    object sender,
    PXImportAttribute.MappingPropertiesInitEventArgs e)
  {
    HashSet<string> stringSet = new HashSet<string>();
    stringSet.Add(((PXSelectBase) this.TransactionLines).Cache.GetField(typeof (PMProformaLine.merged)));
    stringSet.Add(((PXSelectBase) this.TransactionLines).Cache.GetField(typeof (PMProformaTransactLine.option)));
    for (int index = 0; index < e.Names.Count; ++index)
    {
      if (stringSet.Contains(e.Names[index]))
      {
        e.Names.RemoveAt(index);
        e.DisplayNames.RemoveAt(index);
        --index;
      }
    }
  }

  public bool IsMigrationMode()
  {
    return ((PXSelectBase<PMSetup>) this.Setup).Current.MigrationMode.GetValueOrDefault();
  }

  protected virtual void BeforeCommitHandler(PXGraph e)
  {
    string refNbr = ((PXSelectBase<PMProforma>) this.Document).Current?.RefNbr;
    int? revisionID = (int?) ((PXSelectBase<PMProforma>) this.Document).Current?.RevisionID;
    Action<PXGraph> checkerDelegate = this._licenseLimits.GetCheckerDelegate<PMProforma>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (PMProformaLine), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[2]
      {
        (PXDataFieldValue) new PXDataFieldValue<PMProformaLine.refNbr>((object) refNbr),
        (PXDataFieldValue) new PXDataFieldValue<PMProformaLine.revisionID>((object) revisionID)
      }))
    });
    try
    {
      checkerDelegate(e);
    }
    catch (ViolationRegisteredException ex)
    {
      throw new PXException("The total number of lines on the Progress Billing and Time and Material tabs has exceeded the limit set for the current license. Please reduce the number of lines to be able to save the document.");
    }
  }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    ((PXGraph) this).OnBeforeCommit += new Action<PXGraph>(this.BeforeCommitHandler);
  }

  [PXUIField(DisplayName = "Release")]
  [PXProcessButton]
  public IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ProformaEntry.\u003C\u003Ec__DisplayClass98_0 cDisplayClass980 = new ProformaEntry.\u003C\u003Ec__DisplayClass98_0();
    this.RecalculateExternalTaxesSync = true;
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass980.proforma = ((PXSelectBase<PMProforma>) this.Document).Current;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass980, __methodptr(\u003CRelease\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable ProformaReport(PXAdapter adapter)
  {
    this.OpenReport("PM642000", ((PXSelectBase<PMProforma>) this.Document).Current);
    return adapter.Get();
  }

  public virtual void OpenReport(string reportID, PMProforma doc)
  {
    if (doc != null)
    {
      string str = new NotificationUtility((PXGraph) this).SearchProjectReport(reportID, ((PXSelectBase<PMProject>) this.Project).Current.ContractID, ((PXSelectBase<PMProject>) this.Project).Current.DefaultBranchID);
      throw new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["RefNbr"] = doc.RefNbr
      }, str, str, (CurrentLocalization) null);
    }
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable Send(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ProformaEntry.\u003C\u003Ec__DisplayClass103_0 displayClass1030 = new ProformaEntry.\u003C\u003Ec__DisplayClass103_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1030.proforma = ((PXSelectBase<PMProforma>) this.Document).Current;
    // ISSUE: reference to a compiler-generated field
    if (displayClass1030.proforma == null)
      return adapter.Get();
    // ISSUE: reference to a compiler-generated field
    displayClass1030.isMassProcessing = adapter.MassProcess;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1030, __methodptr(\u003CSend\u003Eb__0)));
    return adapter.Get();
  }

  public virtual void SendReport(string notificationCD, PMProforma doc, bool massProcess = false)
  {
    if (doc == null)
      return;
    Dictionary<string, string> parameters = new Dictionary<string, string>();
    parameters["RefNbr"] = ((PXSelectBase<PMProforma>) this.Document).Current.RefNbr;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXGraph) this).GetExtension<ProformaEntry.ProformaEntry_ActivityDetailsExt>().SendNotification("Project", notificationCD, doc.BranchID, (IDictionary<string, string>) parameters, massProcess, (IList<Guid?>) null);
      ((PXAction) this.Save).Press();
      transactionScope.Complete();
    }
  }

  [PXUIField(DisplayName = "View Transaction Details")]
  [PXProcessButton]
  public IEnumerable ViewTransactionDetails(PXAdapter adapter)
  {
    ((PXSelectBase) this.Details).View.AskExt();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Apply Available Prepaid Amounts")]
  [PXProcessButton]
  public IEnumerable AutoApplyPrepayments(PXAdapter adapter)
  {
    this.ApplyPrepayment(((PXSelectBase<PMProforma>) this.Document).Current);
    yield return (object) ((PXSelectBase<PMProforma>) this.Document).Current;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewTranDocument(PXAdapter adapter)
  {
    RegisterEntry instance = PXGraph.CreateInstance<RegisterEntry>();
    ((PXSelectBase<PMRegister>) instance.Document).Current = PXResultset<PMRegister>.op_Implicit(((PXSelectBase<PMRegister>) instance.Document).Search<PMRegister.refNbr>((object) ((PXSelectBase<PMTran>) this.Details).Current.RefNbr, new object[1]
    {
      (object) ((PXSelectBase<PMTran>) this.Details).Current.TranType
    }));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Upload Unbilled Transactions")]
  [PXButton]
  public IEnumerable UploadUnbilled(PXAdapter adapter)
  {
    if (((PXSelectBase) this.Unbilled).View.AskExt() == 1)
      this.AppendUnbilled();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Upload")]
  [PXButton]
  public IEnumerable AppendSelected(PXAdapter adapter)
  {
    this.AppendUnbilled();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewProgressLineTask(PXAdapter adapter)
  {
    ProjectTaskEntry instance = PXGraph.CreateInstance<ProjectTaskEntry>();
    ((PXSelectBase<PMTask>) instance.Task).Current = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMProformaLine.projectID>>, And<PMTask.taskID, Equal<Current<PMProformaProgressLine.taskID>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, "View Task");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewTransactLineTask(PXAdapter adapter)
  {
    ProjectTaskEntry instance = PXGraph.CreateInstance<ProjectTaskEntry>();
    ((PXSelectBase<PMTask>) instance.Task).Current = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMProformaLine.projectID>>, And<PMTask.taskID, Equal<Current<PMProformaTransactLine.taskID>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, "View Task");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewProgressLineInventory(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToInventoryItemScreen(PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMProformaProgressLine.inventoryID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewTransactLineInventory(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToInventoryItemScreen(PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMProformaTransactLine.inventoryID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewVendor(PXAdapter adapter)
  {
    if (PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<PMProformaTransactLine.vendorID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())) != null)
    {
      VendorMaint instance = PXGraph.CreateInstance<VendorMaint>();
      ((PXSelectBase<VendorR>) instance.BAccount).Current = PXResultset<VendorR>.op_Implicit(PXSelectBase<VendorR, PXSelect<VendorR, Where<VendorR.bAccountID, Equal<Current<PMProformaTransactLine.vendorID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewARDocument(PXAdapter adapter)
  {
    PMProforma current = ((PXSelectBase<PMProforma>) this.Document).Current;
    ProjectAccountingService.NavigateToArInvoiceScreen(current?.ARInvoiceDocType, current?.ARInvoiceRefNbr);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Load Lines")]
  [PXProcessButton]
  public IEnumerable UploadFromBudget(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProforma>) this.Document).Current != null && ((PXSelectBase<PMProforma>) this.Document).Current.ProjectID.HasValue)
    {
      HashSet<string> hashSet = GraphHelper.RowCast<PMProformaProgressLine>((IEnumerable) ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Select(Array.Empty<object>())).Select<PMProformaProgressLine, string>(new Func<PMProformaProgressLine, string>(ProformaEntry.GetProformaLineKey)).ToHashSet<string>();
      foreach (PXResult<PMRevenueBudget, PMAccountGroup, PMTask, PMBilling> pxResult in PXSelectBase<PMRevenueBudget, PXViewOf<PMRevenueBudget>.BasedOn<SelectFromBase<PMRevenueBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMAccountGroup>.On<BqlOperand<PMAccountGroup.groupID, IBqlInt>.IsEqual<PMRevenueBudget.accountGroupID>>>, FbqlJoins.Inner<PMTask>.On<BqlOperand<PMTask.taskID, IBqlInt>.IsEqual<PMRevenueBudget.projectTaskID>>>, FbqlJoins.Inner<PMBilling>.On<BqlOperand<PMBilling.billingID, IBqlString>.IsEqual<PMTask.billingID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMRevenueBudget.projectID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMRevenueBudget.type, Equal<AccountType.income>>>>>.And<BqlOperand<PMTask.status, IBqlString>.IsIn<ProjectTaskStatus.active, ProjectTaskStatus.completed>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<PMProforma>) this.Document).Current.ProjectID
      }))
      {
        PMRevenueBudget budget = PXResult<PMRevenueBudget, PMAccountGroup, PMTask, PMBilling>.op_Implicit(pxResult);
        PMBilling pmBilling = PXResult<PMRevenueBudget, PMAccountGroup, PMTask, PMBilling>.op_Implicit(pxResult);
        PMProformaProgressLine line1 = new PMProformaProgressLine();
        line1.Type = "P";
        line1.BillableQty = new Decimal?(0M);
        line1.ProjectID = budget.ProjectID;
        line1.TaskID = budget.ProjectTaskID;
        line1.AccountGroupID = budget.AccountGroupID;
        line1.CostCodeID = budget.CostCodeID;
        line1.InventoryID = budget.InventoryID;
        if (!hashSet.Contains(ProformaEntry.GetProformaLineKey((PMProformaLine) line1)))
        {
          if (PXResultset<PMBillingRule>.op_Implicit(PXSelectBase<PMBillingRule, PXViewOf<PMBillingRule>.BasedOn<SelectFromBase<PMBillingRule, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMBillingRule.billingID, Equal<P.AsString>>>>>.And<BqlOperand<PMBillingRule.type, IBqlString>.IsEqual<PMBillingType.budget>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
          {
            (object) pmBilling.BillingID
          })) != null)
          {
            PMProformaProgressLine line2 = ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Insert(line1);
            if (line2 != null)
            {
              this.SetDefaultBranch(line2);
              this.SetDefaultAccountAndSubId(((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Locate(line2));
              this.SetDefaultValuesWithRevenueBudget(((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Locate(line2), budget);
              ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Update(((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Locate(line2));
            }
          }
        }
      }
    }
    return adapter.Get();
  }

  private static string GetProformaLineKey(PMProformaLine line)
  {
    return $"{line.TaskID}.{line.AccountGroupID}.{line.CostCodeID}.{line.InventoryID}";
  }

  private static string GetBudgetLineKey(PMBudget line)
  {
    return $"{line.TaskID}.{line.AccountGroupID}.{line.CostCodeID}.{line.InventoryID}";
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove Hold")]
  protected virtual IEnumerable RemoveHold(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProforma>) this.Document).Current != null)
    {
      this.ValidateLimitsOnUnhold(((PXSelectBase<PMProforma>) this.Document).Current);
      ((PXSelectBase<PMProforma>) this.Document).Current.Hold = new bool?(false);
      ((PXSelectBase<PMProforma>) this.Document).Update(((PXSelectBase<PMProforma>) this.Document).Current);
    }
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable Hold(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    ProformaEntry aGraph = this;
    foreach (PMProforma pmProforma in adapter.Get<PMProforma>())
    {
      if (pmProforma != null)
      {
        PMAddress aAddress1 = PXResultset<PMAddress>.op_Implicit(((PXSelectBase<PMAddress>) aGraph.Billing_Address).Select(Array.Empty<object>()));
        bool? nullable;
        if (aAddress1 != null)
        {
          nullable = aAddress1.IsDefaultAddress;
          bool flag1 = false;
          if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
          {
            nullable = aAddress1.IsValidated;
            bool flag2 = false;
            if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
              PXAddressValidator.Validate<PMAddress>((PXGraph) aGraph, aAddress1, true, true);
          }
        }
        PMShippingAddress aAddress2 = PXResultset<PMShippingAddress>.op_Implicit(((PXSelectBase<PMShippingAddress>) aGraph.Shipping_Address).Select(Array.Empty<object>()));
        if (aAddress2 != null)
        {
          nullable = aAddress2.IsDefaultAddress;
          bool flag3 = false;
          if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
          {
            nullable = aAddress2.IsValidated;
            bool flag4 = false;
            if (nullable.GetValueOrDefault() == flag4 & nullable.HasValue)
              PXAddressValidator.Validate<PMShippingAddress>((PXGraph) aGraph, aAddress2, true, true);
          }
        }
      }
      yield return (object) pmProforma;
    }
  }

  [PXUIField(DisplayName = "Correct")]
  [PXProcessButton]
  public IEnumerable Correct(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProforma>) this.Document).Current != null)
    {
      this.ValidateAndRaiseExceptionCanCorrect(((PXSelectBase<PMProforma>) this.Document).Current);
      if (((PXSelectBase<PMProject>) this.Project).Current.RetainageMode != "N" || ((PXSelectBase<PMProject>) this.Project).Current.SteppedRetainage.GetValueOrDefault())
      {
        string str1 = "Standard";
        switch (((PXSelectBase<PMProject>) this.Project).Current.RetainageMode)
        {
          case "C":
            str1 = "Contract Cap";
            break;
          case "L":
            str1 = "Contract Item Cap";
            break;
        }
        PXSelect<PMProforma, Where<PMProforma.projectID, Equal<Current<PMProforma.projectID>>, And<PMProforma.refNbr, Greater<Current<PMProforma.refNbr>>, And<PMProforma.corrected, Equal<False>>>>> pxSelect = new PXSelect<PMProforma, Where<PMProforma.projectID, Equal<Current<PMProforma.projectID>>, And<PMProforma.refNbr, Greater<Current<PMProforma.refNbr>>, And<PMProforma.corrected, Equal<False>>>>>((PXGraph) this);
        string empty = string.Empty;
        object[] objArray = Array.Empty<object>();
        foreach (PXResult<PMProforma> pxResult in ((PXSelectBase<PMProforma>) pxSelect).Select(objArray))
        {
          PMProforma pmProforma = PXResult<PMProforma>.op_Implicit(pxResult);
          empty += $" {pmProforma.RefNbr},";
        }
        if (empty != string.Empty)
        {
          string str2 = empty.TrimEnd(',');
          string str3 = ((PXSelectBase<PMProject>) this.Project).Current.SteppedRetainage.GetValueOrDefault() ? " with steps" : string.Empty;
          if (((PXSelectBase<PMProforma>) this.DocumentSettings).Ask("Correct Pro Forma Invoice", PXLocalizer.LocalizeFormat("The retainage mode of the project is {0}{1}. Making corrections to the pro forma invoice for the project may cause discrepancies in the calculation of the retainage amount in subsequent pro forma invoices and retainage values of the project on the Revenue Budget tab of the Projects (PM301000) form. You may be required to correct the following pro forma invoices to ensure the accurate recalculation of the retainage: {2}. Click OK to proceed.", new object[3]
          {
            (object) str1,
            (object) str3,
            (object) str2
          }), (MessageButtons) 1) == 2)
            return adapter.Get();
        }
      }
      List<Tuple<PMProformaProgressLine, string, Guid[]>> tupleList1 = new List<Tuple<PMProformaProgressLine, string, Guid[]>>();
      foreach (PXResult<PMProformaProgressLine> pxResult in ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Select(Array.Empty<object>()))
      {
        PMProformaProgressLine proformaProgressLine = PXResult<PMProformaProgressLine>.op_Implicit(pxResult);
        string note = PXNoteAttribute.GetNote(((PXSelectBase) this.ProgressiveLines).Cache, (object) proformaProgressLine);
        Guid[] fileNotes = PXNoteAttribute.GetFileNotes(((PXSelectBase) this.ProgressiveLines).Cache, (object) proformaProgressLine);
        tupleList1.Add(new Tuple<PMProformaProgressLine, string, Guid[]>(this.CreateCorrectionProformaProgressiveLine(proformaProgressLine), note, fileNotes));
        this.SubtractFromTotalRetained((PMProformaLine) proformaProgressLine);
        this.SubtractPerpaymentRemainder((PMProformaLine) proformaProgressLine, -1);
        ((PXSelectBase) this.ProgressiveLines).Cache.SetValue<PMProformaLine.corrected>((object) proformaProgressLine, (object) true);
        GraphHelper.MarkUpdated(((PXSelectBase) this.ProgressiveLines).Cache, (object) proformaProgressLine);
      }
      List<Tuple<PMProformaTransactLine, string, Guid[]>> tupleList2 = new List<Tuple<PMProformaTransactLine, string, Guid[]>>();
      foreach (PXResult<PMProformaTransactLine> pxResult in ((PXSelectBase<PMProformaTransactLine>) this.TransactionLines).Select(Array.Empty<object>()))
      {
        PMProformaTransactLine proformaTransactLine = PXResult<PMProformaTransactLine>.op_Implicit(pxResult);
        string note = PXNoteAttribute.GetNote(((PXSelectBase) this.TransactionLines).Cache, (object) proformaTransactLine);
        Guid[] fileNotes = PXNoteAttribute.GetFileNotes(((PXSelectBase) this.TransactionLines).Cache, (object) proformaTransactLine);
        tupleList2.Add(new Tuple<PMProformaTransactLine, string, Guid[]>(this.CreateCorrectionProformaTransactLine(proformaTransactLine), note, fileNotes));
        this.SubtractFromTotalRetained((PMProformaLine) proformaTransactLine);
        this.SubtractPerpaymentRemainder((PMProformaLine) proformaTransactLine, -1);
        ((PXSelectBase) this.TransactionLines).Cache.SetValue<PMProformaLine.corrected>((object) proformaTransactLine, (object) true);
        GraphHelper.MarkUpdated(((PXSelectBase) this.TransactionLines).Cache, (object) proformaTransactLine);
      }
      ProformaEntry instance = PXGraph.CreateInstance<ProformaEntry>();
      ((PXGraph) instance).Clear();
      instance.IsCorrectionProcess = true;
      ProformaAutoNumberAttribute.DisableAutonumbiring(((PXSelectBase) instance.Document).Cache);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PXFieldVerifying pxFieldVerifying = ProformaEntry.\u003C\u003Ec.\u003C\u003E9__138_0 ?? (ProformaEntry.\u003C\u003Ec.\u003C\u003E9__138_0 = new PXFieldVerifying((object) ProformaEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCorrect\u003Eb__138_0)));
      ((PXGraph) instance).FieldVerifying.AddHandler<PMProforma.finPeriodID>(pxFieldVerifying);
      OpenPeriodAttribute.SetValidatePeriod<PMProforma.finPeriodID>(((PXSelectBase) instance.Document).Cache, (object) null, PeriodValidation.Nothing);
      this.CorrectProforma(instance, ((PXSelectBase<PMProforma>) this.Document).Current);
      foreach (Tuple<PMProformaProgressLine, string, Guid[]> tuple in tupleList1)
      {
        PMProformaProgressLine proformaProgressLine = ((PXSelectBase<PMProformaProgressLine>) instance.ProgressiveLines).Insert(tuple.Item1);
        if (tuple.Item2 != null)
          PXNoteAttribute.SetNote(((PXSelectBase) instance.ProgressiveLines).Cache, (object) proformaProgressLine, tuple.Item2);
        if (tuple.Item3 != null && tuple.Item3.Length != 0)
          PXNoteAttribute.SetFileNotes(((PXSelectBase) instance.ProgressiveLines).Cache, (object) proformaProgressLine, tuple.Item3);
      }
      foreach (Tuple<PMProformaTransactLine, string, Guid[]> tuple in tupleList2)
      {
        PMProformaTransactLine proformaTransactLine = ((PXSelectBase<PMProformaTransactLine>) instance.TransactionLines).Insert(tuple.Item1);
        if (tuple.Item2 != null)
          PXNoteAttribute.SetNote(((PXSelectBase) instance.TransactionLines).Cache, (object) proformaTransactLine, tuple.Item2);
        if (tuple.Item3 != null && tuple.Item3.Length != 0)
          PXNoteAttribute.SetFileNotes(((PXSelectBase) instance.TransactionLines).Cache, (object) proformaTransactLine, tuple.Item3);
      }
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        ((PXAction) this.Save).Press();
        ((PXGraph) instance).SelectTimeStamp();
        ((PXAction) instance.Save).Press();
        transactionScope.Complete();
      }
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 0);
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Include in Progress Billing")]
  [PXButton]
  public virtual IEnumerable MergeWithProgress(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProforma>) this.Document).Current != null)
      this.MergeLines();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Remove from Progress Billing")]
  [PXButton]
  public virtual IEnumerable RevertMergeWithProgress(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProforma>) this.Document).Current != null)
      this.UnmergeLines();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Update Empty Revenue Tasks")]
  [PXButton]
  public virtual IEnumerable FillRevenueTasks(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProforma>) this.Document).Current != null)
      this.FillEmptyRevenueTasks();
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMProforma> e)
  {
    PMProformaRevision lastRevision = this.GetLastRevision();
    if (e.Row != null)
    {
      if (lastRevision == null)
      {
        foreach (PXResult<PMBillingRecord> pxResult in PXSelectBase<PMBillingRecord, PXSelect<PMBillingRecord, Where<PMBillingRecord.proformaRefNbr, Equal<Required<PMBillingRecord.proformaRefNbr>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) e.Row.RefNbr
        }))
          ((PXSelectBase<PMBillingRecord>) this.BillingRecord).Delete(PXResult<PMBillingRecord>.op_Implicit(pxResult));
      }
      foreach (PXResult<PMTran> pxResult in ((PXSelectBase<PMTran>) new PXSelect<PMTran, Where<PMTran.proformaRefNbr, Equal<Required<PMTran.proformaRefNbr>>, And<PMTran.aRRefNbr, IsNull>>>((PXGraph) this)).Select(new object[1]
      {
        (object) e.Row.RefNbr
      }))
      {
        PMTran tran = PXResult<PMTran>.op_Implicit(pxResult);
        if (lastRevision == null)
          this.ClearProformaReference(tran);
        this.Unbill(tran);
      }
    }
    if (lastRevision != null)
    {
      foreach (PXResult<PMProformaLine> pxResult in ((PXSelectBase<PMProformaLine>) new PXSelect<PMProformaLine, Where<PMProformaLine.refNbr, Equal<Required<PMProforma.refNbr>>, And<PMProformaLine.revisionID, Equal<Required<PMProforma.revisionID>>>>>((PXGraph) this)).Select(new object[2]
      {
        (object) lastRevision.RefNbr,
        (object) lastRevision.RevisionID
      }))
        this.AddToTotalRetained(PXResult<PMProformaLine>.op_Implicit(pxResult));
      PMBillingRecord pmBillingRecord = PXResultset<PMBillingRecord>.op_Implicit(PXSelectBase<PMBillingRecord, PXSelect<PMBillingRecord, Where<PMBillingRecord.proformaRefNbr, Equal<Required<PMBillingRecord.proformaRefNbr>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) e.Row.RefNbr
      }));
      pmBillingRecord.ARDocType = lastRevision.ARInvoiceDocType;
      pmBillingRecord.ARRefNbr = lastRevision.ARInvoiceRefNbr;
      ((PXSelectBase<PMBillingRecord>) this.BillingRecord).Update(pmBillingRecord);
    }
    string arInvoiceDocType = e.Row?.ARInvoiceDocType;
    string arInvoiceRefNbr = e.Row?.ARInvoiceRefNbr;
    if (string.IsNullOrEmpty(arInvoiceDocType) || string.IsNullOrEmpty(arInvoiceRefNbr))
      return;
    this.UpdateInvoice(arInvoiceDocType, arInvoiceRefNbr, false);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMProformaTransactLine> e)
  {
    ((PXSelectBase) this.Document).Cache.SetValue<PMProforma.enableTransactional>((object) ((PXSelectBase<PMProforma>) this.Document).Current, (object) true);
    this.AddToInvoiced((PMProformaLine) e.Row);
    this.AddToDraftRetained((PMProformaLine) e.Row);
    this.AddToTotalRetained((PMProformaLine) e.Row);
    this.SubtractPerpaymentRemainder((PMProformaLine) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMProformaProgressLine> e)
  {
    ((PXSelectBase) this.Document).Cache.SetValue<PMProforma.enableProgressive>((object) ((PXSelectBase<PMProforma>) this.Document).Current, (object) true);
    this.AddToInvoiced((PMProformaLine) e.Row);
    this.AddToDraftRetained((PMProformaLine) e.Row);
    this.AddToTotalRetained((PMProformaLine) e.Row);
    this.SubtractPerpaymentRemainder((PMProformaLine) e.Row);
    this.CalculateProgressLinePreviouslyInvoicedValues(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaLine.curyPrepaidAmount> e)
  {
    if (e.Row == null)
      return;
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaLine.curyPrepaidAmount>, PMProformaTransactLine, object>) e).NewValue;
    if (!newValue.HasValue)
      return;
    Decimal? curyPrepaidAmount = e.Row.CuryPrepaidAmount;
    Decimal num = 0M;
    if (!(curyPrepaidAmount.GetValueOrDefault() > num & curyPrepaidAmount.HasValue))
      return;
    curyPrepaidAmount = e.Row.CuryPrepaidAmount;
    Decimal? nullable = newValue;
    if (!(curyPrepaidAmount.GetValueOrDefault() < nullable.GetValueOrDefault() & curyPrepaidAmount.HasValue & nullable.HasValue))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaLine.curyPrepaidAmount>, PMProformaTransactLine, object>) e).NewValue = (object) e.Row.CuryPrepaidAmount;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaLine.curyPrepaidAmount>>) e).Cache.RaiseExceptionHandling<PMProformaLine.curyPrepaidAmount>((object) e.Row, (object) e.Row.CuryPrepaidAmount, (Exception) new PXSetPropertyException<PMProformaLine.curyPrepaidAmount>("The Prepaid Amount can only be decreased from the auto assigned value.", (PXErrorLevel) 2));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProformaProgressLine, PMProformaLine.curyPrepaidAmount> e)
  {
    if (e.Row == null)
      return;
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaProgressLine, PMProformaLine.curyPrepaidAmount>, PMProformaProgressLine, object>) e).NewValue;
    if (!newValue.HasValue)
      return;
    Decimal? curyPrepaidAmount = e.Row.CuryPrepaidAmount;
    Decimal num = 0M;
    if (!(curyPrepaidAmount.GetValueOrDefault() > num & curyPrepaidAmount.HasValue))
      return;
    curyPrepaidAmount = e.Row.CuryPrepaidAmount;
    Decimal? nullable = newValue;
    if (!(curyPrepaidAmount.GetValueOrDefault() < nullable.GetValueOrDefault() & curyPrepaidAmount.HasValue & nullable.HasValue))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaProgressLine, PMProformaLine.curyPrepaidAmount>, PMProformaProgressLine, object>) e).NewValue = (object) e.Row.CuryPrepaidAmount;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMProformaProgressLine, PMProformaLine.curyPrepaidAmount>>) e).Cache.RaiseExceptionHandling<PMProformaLine.curyPrepaidAmount>((object) e.Row, (object) e.Row.CuryPrepaidAmount, (Exception) new PXSetPropertyException<PMProformaLine.curyPrepaidAmount>("The Prepaid Amount can only be decreased from the auto assigned value.", (PXErrorLevel) 2));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaTransactLine.curyLineTotal> e)
  {
    PMProformaTransactLine row = e.Row;
    List<PMTran> pmTranList;
    if ((row != null ? (row.LineNbr.HasValue ? 1 : 0) : 0) == 0 || !(e.Row.Option == "U") || this.IsCorrectionProcess || !this.GetReferencedTransactions().TryGetValue(e.Row.LineNbr.Value, out pmTranList))
      return;
    PXSelect<PMTran, Where<PMTran.remainderOfTranID, Equal<Required<PMTran.remainderOfTranID>>, And<PMTran.billed, Equal<True>>>> pxSelect = new PXSelect<PMTran, Where<PMTran.remainderOfTranID, Equal<Required<PMTran.remainderOfTranID>>, And<PMTran.billed, Equal<True>>>>((PXGraph) this);
    foreach (PMTran pmTran1 in pmTranList)
    {
      PMTran pmTran2 = PXResultset<PMTran>.op_Implicit(((PXSelectBase<PMTran>) pxSelect).Select(new object[1]
      {
        (object) pmTran1.TranID
      }));
      if (pmTran2 != null)
        throw new PXSetPropertyException<PMProformaTransactLine.curyLineTotal>("The amount cannot be modified because the related remainder has already been billed in the {0} pro forma invoice.", new object[1]
        {
          (object) pmTran2.ProformaRefNbr
        });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProformaTransactLine, PMProformaTransactLine.curyLineTotal> e)
  {
    string proformaLineOption = ProformaTransactLineStatusAttribute.GetDefaultProformaLineOption(e.Row, (string) null);
    if (!(e.Row.Option != proformaLineOption))
      return;
    bool flag = false;
    if (e.Row.Option == "N")
      flag = !this.IsAdjustment(e.Row);
    else if (proformaLineOption == "N")
      flag = e.Row.Option == "U" || e.Row.Option == "C";
    if (!flag)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProformaTransactLine, PMProformaTransactLine.curyLineTotal>>) e).Cache.SetValueExt<PMProformaTransactLine.option>((object) e.Row, (object) proformaLineOption);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProformaTransactLine, PMProformaTransactLine.curyLineTotal>>) e).Cache.SetValuePending<PMProformaTransactLine.option>((object) e.Row, (object) proformaLineOption);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProformaProgressLine, PMProformaLine.completedPct> e)
  {
    PMRevenueBudget row1 = this.SelectRevenueBudget(e.Row);
    if (row1 == null)
      return;
    if (e.Row.ProgressBillingBase == "A")
    {
      Decimal inBillingCurrency = this.GetAmountInBillingCurrency(new Decimal?(this.CalculatePendingInvoicedAmount(e.Row)));
      Decimal num1 = row1.CuryRevisedAmount.GetValueOrDefault() * e.Row.CompletedPct.GetValueOrDefault() / 100M;
      Decimal actualAmountWithTaxes = this.GetCuryActualAmountWithTaxes(row1);
      Decimal? nullable = row1.CuryInvoicedAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      Decimal num2 = actualAmountWithTaxes + valueOrDefault1 + inBillingCurrency;
      nullable = e.Row.CuryLineTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal val2 = num1 - (num2 - valueOrDefault2 - this.GetLastInvoicedBeforeCorrection(e.Row));
      if (num1 > 0M)
        val2 = Math.Max(0M, val2);
      ProgressLineSelect progressiveLines = this.ProgressiveLines;
      PMProformaProgressLine row2 = e.Row;
      Decimal num3 = val2;
      nullable = e.Row.CuryMaterialStoredAmount;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      // ISSUE: variable of a boxed type
      __Boxed<Decimal> local = (ValueType) (num3 - valueOrDefault3);
      ((PXSelectBase<PMProformaProgressLine>) progressiveLines).SetValueExt<PMProformaLine.curyAmount>(row2, (object) local);
    }
    else
    {
      if (!(e.Row.ProgressBillingBase == "Q"))
        return;
      Decimal result;
      INUnitAttribute.TryConvertGlobalUnits((PXGraph) this, row1.UOM, e.Row.UOM, row1.RevisedQty.GetValueOrDefault(), INPrecision.QUANTITY, out result);
      ProgressLineSelect progressiveLines = this.ProgressiveLines;
      PMProformaProgressLine row3 = e.Row;
      Decimal num4 = result;
      Decimal? nullable = e.Row.CompletedPct;
      Decimal num5 = nullable.GetValueOrDefault() / 100M;
      Decimal num6 = num4 * num5;
      nullable = e.Row.PreviouslyInvoicedQty;
      Decimal valueOrDefault = nullable.GetValueOrDefault();
      // ISSUE: variable of a boxed type
      __Boxed<Decimal> local = (ValueType) (num6 - valueOrDefault);
      ((PXSelectBase<PMProformaProgressLine>) progressiveLines).SetValueExt<PMProformaLine.qty>(row3, (object) local);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProformaProgressLine, PMProformaLine.qty> e)
  {
    if (e.Row == null)
      return;
    this.CalculateCuryAmounts(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProformaProgressLine, PMProformaProgressLine.curyUnitPrice> e)
  {
    if (e.Row == null)
      return;
    this.CalculateCuryAmounts(e.Row);
  }

  protected virtual void CalculateCuryAmounts(PMProformaProgressLine line)
  {
    Decimal? nullable = line.Qty;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = line.CuryUnitPrice;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    Decimal num = valueOrDefault1 * valueOrDefault2;
    ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).SetValueExt<PMProformaLine.curyAmount>(line, (object) num);
    ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).SetValueExt<PMProformaProgressLine.curyLineTotal>(line, (object) (num + line.CuryMaterialStoredAmount.GetValueOrDefault()));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProformaProgressLine, PMProformaProgressLine.curyRetainage> e)
  {
    Decimal? curyLineTotal = e.Row.CuryLineTotal;
    Decimal num = 0M;
    if (!(curyLineTotal.GetValueOrDefault() > num & curyLineTotal.HasValue))
      return;
    e.Row.RetainagePct = new Decimal?(this.CalculateRetainagePct((PMProformaLine) e.Row));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProformaProgressLine, PMProformaProgressLine.accountID> e)
  {
    this._accountIdUpdating = true;
    try
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProformaProgressLine, PMProformaProgressLine.accountID>>) e).Cache.SetDefaultExt<PMProformaTransactLine.accountGroupID>((object) e.Row);
    }
    finally
    {
      this._accountIdUpdating = false;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProformaProgressLine, PMProformaProgressLine.taskID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProformaProgressLine, PMProformaProgressLine.taskID>>) e).Cache.SetDefaultExt<PMProformaProgressLine.progressBillingBase>((object) e.Row);
    if (object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMProformaProgressLine, PMProformaProgressLine.taskID>, PMProformaProgressLine, object>) e).OldValue) || this.IsBilling || ((PXGraph) this).IsImportFromExcel)
      return;
    this.SetDefaultBranch(e.Row);
    this.SetDefaultAccountAndSubId(e.Row);
    this.SetDefaultValuesWithRevenueBudget(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProformaProgressLine, PMProformaProgressLine.accountGroupID> e)
  {
    if (object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMProformaProgressLine, PMProformaProgressLine.accountGroupID>, PMProformaProgressLine, object>) e).OldValue) || this.IsBilling || ((PXGraph) this).IsImportFromExcel)
      return;
    if (!this._accountIdUpdating)
      this.SetDefaultAccountAndSubId(e.Row);
    this.SetDefaultValuesWithRevenueBudget(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProformaProgressLine, PMProformaProgressLine.inventoryID> e)
  {
    if (object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMProformaProgressLine, PMProformaProgressLine.inventoryID>, PMProformaProgressLine, object>) e).OldValue) || this.IsBilling || ((PXGraph) this).IsImportFromExcel)
      return;
    this.SetDefaultAccountAndSubId(e.Row);
    this.SetDefaultValuesWithRevenueBudget(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProformaProgressLine, PMProformaLine.costCodeID> e)
  {
    if (object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMProformaProgressLine, PMProformaLine.costCodeID>, PMProformaProgressLine, object>) e).OldValue) || this.IsBilling || ((PXGraph) this).IsImportFromExcel)
      return;
    this.SetDefaultValuesWithRevenueBudget(e.Row);
  }

  public bool IsBilling { get; set; }

  protected virtual void SetDefaultBranch(PMProformaProgressLine line)
  {
    if (line == null)
      return;
    PMProject current = ((PXSelectBase<PMProject>) this.Project).Current;
    if (current == null)
      return;
    PMTask task = PMTask.PK.Find((PXGraph) this, current.ContractID, line.TaskID);
    if (task == null)
      return;
    PMBillingRule rule = PMBillingRule.PK.Find((PXGraph) this, task.BillingID);
    if (rule == null)
      return;
    PX.Objects.AR.Customer customer = PX.Objects.AR.Customer.PK.Find((PXGraph) this, current.CustomerID);
    if (customer == null)
      return;
    int? targetBranchId = PMBillEngine.CalculateTargetBranchID((PXGraph) this, rule, current, task, (PMTran) null, customer, new int?());
    if (!targetBranchId.HasValue)
      return;
    int? nullable = targetBranchId;
    int? branchId = line.BranchID;
    if (nullable.GetValueOrDefault() == branchId.GetValueOrDefault() & nullable.HasValue == branchId.HasValue)
      return;
    ((PXSelectBase) this.ProgressiveLines).Cache.SetValueExt<PMProformaProgressLine.branchID>((object) line, (object) targetBranchId);
  }

  protected virtual void SetDefaultAccountAndSubId(PMProformaProgressLine line)
  {
    if (line == null)
      return;
    PMProject current = ((PXSelectBase<PMProject>) this.Project).Current;
    if (current == null)
      return;
    PMTask task = PMTask.PK.Find((PXGraph) this, current.ContractID, line.TaskID);
    if (task == null)
      return;
    PMBillingRule rule = PMBillingRule.PK.Find((PXGraph) this, task.BillingID);
    if (rule == null)
      return;
    PX.Objects.AR.Customer customer = PX.Objects.AR.Customer.PK.Find((PXGraph) this, current.CustomerID);
    if (customer == null)
      return;
    int? targetSalesAccountId = PMBillEngine.CalculateTargetSalesAccountID((PXGraph) this, rule, current, task, (PMTran) null, (PMProformaLine) line, customer);
    object valuePending1 = ((PXSelectBase) this.ProgressiveLines).Cache.GetValuePending<PMProformaProgressLine.accountID>((object) line);
    int? nullable = targetSalesAccountId;
    int? accountId = line.AccountID;
    if (!(nullable.GetValueOrDefault() == accountId.GetValueOrDefault() & nullable.HasValue == accountId.HasValue) && (valuePending1 == PXCache.NotSetValue || valuePending1 == null))
      ((PXSelectBase) this.ProgressiveLines).Cache.SetValueExt<PMProformaProgressLine.accountID>((object) line, (object) targetSalesAccountId);
    string salesSubaccountCd = PMBillEngine.CalculateTargetSalesSubaccountCD((PXGraph) this, rule, current, task, new int?(), new int?(), new int?(), line.InventoryID, customer);
    object valuePending2 = ((PXSelectBase) this.ProgressiveLines).Cache.GetValuePending<PMProformaProgressLine.subID>((object) line);
    if (salesSubaccountCd == null || valuePending2 != PXCache.NotSetValue && valuePending2 != null)
      return;
    ((PXSelectBase) this.ProgressiveLines).Cache.SetValueExt<PMProformaProgressLine.subID>((object) line, (object) salesSubaccountCd);
  }

  protected virtual void SetDefaultValuesWithRevenueBudget(PMProformaProgressLine line)
  {
    if (line == null)
      return;
    this.SetDefaultValuesWithRevenueBudget(line, this.SelectRevenueBudget(line));
  }

  protected virtual void SetDefaultValuesWithRevenueBudget(
    PMProformaProgressLine line,
    PMRevenueBudget budget)
  {
    if (line == null)
      return;
    if (budget != null)
    {
      ((PXSelectBase) this.ProgressiveLines).Cache.SetValueExt<PMProformaLine.description>((object) line, (object) budget.Description);
      ((PXSelectBase) this.ProgressiveLines).Cache.SetValueExt<PMProformaProgressLine.progressBillingBase>((object) line, (object) budget.ProgressBillingBase);
      ((PXSelectBase) this.ProgressiveLines).Cache.SetValueExt<PMProformaProgressLine.taxCategoryID>((object) line, (object) budget.TaxCategoryID);
      ((PXSelectBase) this.ProgressiveLines).Cache.SetValueExt<PMProformaProgressLine.uOM>((object) line, (object) budget.UOM);
      ((PXSelectBase) this.ProgressiveLines).Cache.SetValueExt<PMProformaLine.qty>((object) line, (object) budget.QtyToInvoice);
      ((PXSelectBase) this.ProgressiveLines).Cache.SetValueExt<PMProformaProgressLine.curyUnitPrice>((object) line, (object) this.GetValueInBillingCurrency(budget.CuryUnitRate));
      ((PXSelectBase) this.ProgressiveLines).Cache.SetValueExt<PMProformaLine.curyBillableAmount>((object) line, (object) this.GetValueInBillingCurrency(budget.CuryAmountToInvoice));
      ((PXSelectBase) this.ProgressiveLines).Cache.SetValueExt<PMProformaLine.curyAmount>((object) line, (object) line.CuryBillableAmount);
      ((PXSelectBase) this.ProgressiveLines).Cache.SetValueExt<PMProformaLine.retainagePct>((object) line, (object) budget.RetainagePct);
    }
    else
    {
      ((PXSelectBase) this.ProgressiveLines).Cache.SetDefaultExt<PMProformaLine.description>((object) line);
      ((PXSelectBase) this.ProgressiveLines).Cache.SetDefaultExt<PMProformaProgressLine.progressBillingBase>((object) line);
      ((PXSelectBase) this.ProgressiveLines).Cache.SetDefaultExt<PMProformaProgressLine.taxCategoryID>((object) line);
      ((PXSelectBase) this.ProgressiveLines).Cache.SetDefaultExt<PMProformaProgressLine.uOM>((object) line);
      ((PXSelectBase) this.ProgressiveLines).Cache.SetDefaultExt<PMProformaLine.qty>((object) line);
      ((PXSelectBase) this.ProgressiveLines).Cache.SetDefaultExt<PMProformaProgressLine.curyUnitPrice>((object) line);
      ((PXSelectBase) this.ProgressiveLines).Cache.SetDefaultExt<PMProformaLine.curyBillableAmount>((object) line);
      ((PXSelectBase) this.ProgressiveLines).Cache.SetDefaultExt<PMProformaLine.curyAmount>((object) line);
      ((PXSelectBase) this.ProgressiveLines).Cache.SetDefaultExt<PMProformaLine.retainagePct>((object) line);
    }
  }

  protected virtual Decimal? GetValueInBillingCurrency(Decimal? value)
  {
    return !value.HasValue ? value : new Decimal?(this.MultiCurrencyService.GetValueInBillingCurrency((PXGraph) this, ((PXSelectBase<PMProject>) this.Project).Current, ((PXGraph) this).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo(), value));
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProformaTransactLine, PMProformaLine.retainagePct> e)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, e.Row.ProjectID);
    if (pmProject == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProformaTransactLine, PMProformaLine.retainagePct>, PMProformaTransactLine, object>) e).NewValue = (object) pmProject.RetainagePct;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProformaProgressLine, PMProformaLine.retainagePct> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaProgressLine, PMProformaLine.retainagePct>, PMProformaProgressLine, object>) e).NewValue == null)
      return;
    Decimal newValue = (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaProgressLine, PMProformaLine.retainagePct>, PMProformaProgressLine, object>) e).NewValue;
    if (newValue < 0M || newValue > 100M)
      throw new PXSetPropertyException<PMProformaLine.retainagePct>("Percentage value should be between 0 and 100");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProformaProgressLine, PMProformaProgressLine.curyRetainage> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaProgressLine, PMProformaProgressLine.curyRetainage>, PMProformaProgressLine, object>) e).NewValue == null)
      return;
    Decimal newValue = (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaProgressLine, PMProformaProgressLine.curyRetainage>, PMProformaProgressLine, object>) e).NewValue;
    Decimal? curyLineTotal;
    if (newValue > 0M)
    {
      Decimal num = newValue;
      curyLineTotal = e.Row.CuryLineTotal;
      Decimal valueOrDefault = curyLineTotal.GetValueOrDefault();
      if (num > valueOrDefault & curyLineTotal.HasValue)
      {
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaProgressLine, PMProformaProgressLine.curyRetainage>, PMProformaProgressLine, object>) e).NewValue = (object) e.Row.CuryLineTotal;
        return;
      }
    }
    if (!(newValue < 0M))
      return;
    curyLineTotal = e.Row.CuryLineTotal;
    Decimal num1 = 0M;
    if (!(curyLineTotal.GetValueOrDefault() < num1 & curyLineTotal.HasValue))
      return;
    Decimal num2 = newValue;
    curyLineTotal = e.Row.CuryLineTotal;
    Decimal valueOrDefault1 = curyLineTotal.GetValueOrDefault();
    if (!(num2 < valueOrDefault1 & curyLineTotal.HasValue))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaProgressLine, PMProformaProgressLine.curyRetainage>, PMProformaProgressLine, object>) e).NewValue = (object) e.Row.CuryLineTotal;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaTransactLine.curyRetainage> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaTransactLine.curyRetainage>, PMProformaTransactLine, object>) e).NewValue == null)
      return;
    Decimal newValue = (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaTransactLine.curyRetainage>, PMProformaTransactLine, object>) e).NewValue;
    Decimal? curyLineTotal;
    if (newValue > 0M)
    {
      Decimal num = newValue;
      curyLineTotal = e.Row.CuryLineTotal;
      Decimal valueOrDefault = curyLineTotal.GetValueOrDefault();
      if (num > valueOrDefault & curyLineTotal.HasValue)
      {
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaTransactLine.curyRetainage>, PMProformaTransactLine, object>) e).NewValue = (object) e.Row.CuryLineTotal;
        return;
      }
    }
    if (!(newValue < 0M))
      return;
    curyLineTotal = e.Row.CuryLineTotal;
    Decimal num1 = 0M;
    if (!(curyLineTotal.GetValueOrDefault() < num1 & curyLineTotal.HasValue))
      return;
    Decimal num2 = newValue;
    curyLineTotal = e.Row.CuryLineTotal;
    Decimal valueOrDefault1 = curyLineTotal.GetValueOrDefault();
    if (!(num2 < valueOrDefault1 & curyLineTotal.HasValue))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaTransactLine.curyRetainage>, PMProformaTransactLine, object>) e).NewValue = (object) e.Row.CuryLineTotal;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProformaTransactLine, PMProformaTransactLine.curyRetainage> e)
  {
    Decimal? curyLineTotal = e.Row.CuryLineTotal;
    Decimal num = 0M;
    if (!(curyLineTotal.GetValueOrDefault() > num & curyLineTotal.HasValue))
      return;
    e.Row.RetainagePct = new Decimal?(this.CalculateRetainagePct((PMProformaLine) e.Row));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaLine.retainagePct> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaLine.retainagePct>, PMProformaTransactLine, object>) e).NewValue == null)
      return;
    Decimal newValue = (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaLine.retainagePct>, PMProformaTransactLine, object>) e).NewValue;
    if (newValue < 0M || newValue > 100M)
      throw new PXSetPropertyException<PMProformaLine.retainagePct>("Percentage value should be between 0 and 100");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProformaProgressLine, PMProformaLine.currentInvoicedPct> e)
  {
    PMRevenueBudget pmRevenueBudget = this.SelectRevenueBudget(e.Row);
    if (pmRevenueBudget == null)
      return;
    if (e.Row.ProgressBillingBase == "A")
    {
      Decimal inBillingCurrency = this.GetAmountInBillingCurrency(new Decimal?(pmRevenueBudget.CuryRevisedAmount.GetValueOrDefault() * e.Row.CurrentInvoicedPct.GetValueOrDefault() / 100M));
      ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).SetValueExt<PMProformaLine.curyAmount>(e.Row, (object) (inBillingCurrency - e.Row.CuryMaterialStoredAmount.GetValueOrDefault()));
    }
    else
    {
      if (!(e.Row.ProgressBillingBase == "Q"))
        return;
      Decimal result;
      INUnitAttribute.TryConvertGlobalUnits((PXGraph) this, pmRevenueBudget.UOM, e.Row.UOM, pmRevenueBudget.RevisedQty.GetValueOrDefault(), INPrecision.QUANTITY, out result);
      ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).SetValueExt<PMProformaLine.qty>(e.Row, (object) (result * (e.Row.CurrentInvoicedPct.GetValueOrDefault() / 100M)));
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProformaProgressLine, PMProformaProgressLine.inventoryID> e)
  {
    if (e.Row == null || e.Row.InventoryID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProformaProgressLine, PMProformaProgressLine.inventoryID>, PMProformaProgressLine, object>) e).NewValue = (object) PMInventorySelectorAttribute.EmptyInventoryID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProformaProgressLine, PMProformaLine.costCodeID> e)
  {
    if (e.Row == null || e.Row.CostCodeID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProformaProgressLine, PMProformaLine.costCodeID>, PMProformaProgressLine, object>) e).NewValue = (object) CostCodeAttribute.DefaultCostCode;
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PMProformaProgressLine, PMProformaLine.completedPct> e)
  {
    if (e.Row == null)
      return;
    PMRevenueBudget pmRevenueBudget = this.SelectRevenueBudget(e.Row);
    if (pmRevenueBudget == null)
      return;
    Decimal d = 0.0M;
    object valuePending = ((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PMProformaProgressLine, PMProformaLine.completedPct>>) e).Cache.GetValuePending<PMProformaLine.completedPct>((object) e.Row);
    if (valuePending == PXCache.NotSetValue || valuePending == null)
    {
      if (e.Row.ProgressBillingBase == "A")
      {
        if (pmRevenueBudget.CuryRevisedAmount.GetValueOrDefault() != 0.0M)
          d = 100M * (this.GetAmountInProjectCurrency(e.Row.CuryLineTotal) + this.GetAmountInProjectCurrency(e.Row.CuryPreviouslyInvoiced)) / pmRevenueBudget.CuryRevisedAmount.Value;
        d = Math.Round(d, 2);
      }
      else if (e.Row.ProgressBillingBase == "Q")
      {
        Decimal valueOrDefault1 = e.Row.PreviouslyInvoicedQty.GetValueOrDefault();
        Decimal? nullable = e.Row.Qty;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        Decimal result = valueOrDefault1 + valueOrDefault2;
        nullable = pmRevenueBudget.RevisedQty;
        if (nullable.GetValueOrDefault() != 0.0M && INUnitAttribute.TryConvertGlobalUnits((PXGraph) this, e.Row.UOM, pmRevenueBudget.UOM, result, INPrecision.QUANTITY, out result))
          d = 100.0M * result / pmRevenueBudget.RevisedQty.Value;
      }
    }
    PXFieldState instance = PXDecimalState.CreateInstance((object) d, new int?(2), "CompletedPct", new bool?(false), new int?(0), new Decimal?(Decimal.MinValue), new Decimal?(Decimal.MaxValue));
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMProformaProgressLine, PMProformaLine.completedPct>>) e).ReturnState = (object) instance;
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PMProformaProgressLine, PMProformaLine.currentInvoicedPct> e)
  {
    if (e.Row == null)
      return;
    PMRevenueBudget pmRevenueBudget = this.SelectRevenueBudget(e.Row);
    if (pmRevenueBudget == null)
      return;
    Decimal num = 0M;
    if (e.Row.ProgressBillingBase == "A")
    {
      if (pmRevenueBudget.CuryRevisedAmount.GetValueOrDefault() != 0M)
        num = Math.Round(100M * this.GetAmountInProjectCurrency(e.Row.CuryLineTotal) / pmRevenueBudget.CuryRevisedAmount.Value, 2);
    }
    else if (e.Row.ProgressBillingBase == "Q")
    {
      Decimal result = 0.0M;
      if (pmRevenueBudget.RevisedQty.GetValueOrDefault() != 0M && INUnitAttribute.TryConvertGlobalUnits((PXGraph) this, e.Row.UOM, pmRevenueBudget.UOM, e.Row.Qty.GetValueOrDefault(), INPrecision.QUANTITY, out result))
        num = Math.Round(100M * result / pmRevenueBudget.RevisedQty.Value, 2);
    }
    PXFieldState instance = PXDecimalState.CreateInstance((object) num, new int?(2), "CurrentInvoicedPct", new bool?(false), new int?(0), new Decimal?(Decimal.MinValue), new Decimal?(Decimal.MaxValue));
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMProformaProgressLine, PMProformaLine.currentInvoicedPct>>) e).ReturnState = (object) instance;
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PMProformaTransactLine, PMProformaTransactLine.option> e)
  {
    if (e.Row == null)
      return;
    string returnValue = (string) ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMProformaTransactLine, PMProformaTransactLine.option>>) e).ReturnValue;
    KeyValuePair<List<string>, List<string>> validStatusOptions = this.GetValidStatusOptions(e.Row, returnValue);
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMProformaTransactLine, PMProformaTransactLine.option>>) e).ReturnState = (object) PXStringState.CreateInstance(((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMProformaTransactLine, PMProformaTransactLine.option>>) e).ReturnValue, new int?(1), new bool?(false), typeof (PMProformaTransactLine.option).Name, new bool?(false), new int?(1), (string) null, validStatusOptions.Key.ToArray(), validStatusOptions.Value.ToArray(), new bool?(true), (string) null, (string[]) null);
  }

  protected virtual KeyValuePair<List<string>, List<string>> GetValidStatusOptions(
    PMProformaTransactLine line,
    string status)
  {
    return ProformaTransactLineStatusAttribute.GetValidStatusOptions(line, status);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaTransactLine.option> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaTransactLine.option>, PMProformaTransactLine, object>) e).NewValue == null)
      return;
    PMProformaTransactLine row = e.Row;
    int? lineNbr;
    int num;
    if (row == null)
    {
      num = 0;
    }
    else
    {
      lineNbr = row.LineNbr;
      num = lineNbr.HasValue ? 1 : 0;
    }
    if (num != 0 && e.Row.Option == "U" && ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaTransactLine.option>>) e).ExternalCall)
    {
      Dictionary<int, List<PMTran>> referencedTransactions = this.GetReferencedTransactions();
      lineNbr = e.Row.LineNbr;
      int key = lineNbr.Value;
      List<PMTran> pmTranList;
      ref List<PMTran> local = ref pmTranList;
      if (referencedTransactions.TryGetValue(key, out local))
      {
        PXSelect<PMTran, Where<PMTran.remainderOfTranID, Equal<Required<PMTran.remainderOfTranID>>>> pxSelect = new PXSelect<PMTran, Where<PMTran.remainderOfTranID, Equal<Required<PMTran.remainderOfTranID>>>>((PXGraph) this);
        foreach (PMTran pmTran1 in pmTranList)
        {
          PMTran pmTran2 = PXResultset<PMTran>.op_Implicit(((PXSelectBase<PMTran>) pxSelect).Select(new object[1]
          {
            (object) pmTran1.TranID
          }));
          if (pmTran2 != null && pmTran2.Billed.GetValueOrDefault())
            throw new PXSetPropertyException<PMProformaTransactLine.option>("The status cannot be modified because the related remainder has already been billed in the {0} pro forma invoice.", new object[1]
            {
              (object) pmTran2.ProformaRefNbr
            });
        }
      }
    }
    if (!(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaTransactLine.option>, PMProformaTransactLine, object>) e).NewValue.ToString() == "U"))
      return;
    Dictionary<int, List<PMTran>> referencedTransactions1 = this.GetReferencedTransactions();
    lineNbr = e.Row.LineNbr;
    if (!lineNbr.HasValue)
      return;
    Dictionary<int, List<PMTran>> dictionary = referencedTransactions1;
    lineNbr = e.Row.LineNbr;
    int key1 = lineNbr.Value;
    List<PMTran> pmTranList1;
    ref List<PMTran> local1 = ref pmTranList1;
    if (!dictionary.TryGetValue(key1, out local1))
      return;
    bool flag = false;
    foreach (PMTran pmTran in pmTranList1)
    {
      flag = !string.IsNullOrEmpty(pmTran.AllocationID);
      if (flag)
        break;
    }
    if (!flag || pmTranList1.Count <= 1)
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaTransactLine.option>>) e).Cache.RaiseExceptionHandling<PMProformaLine.option>((object) e.Row, (object) "U", (Exception) new PXSetPropertyException("The selected option is not available when a line represents a group of allocated transactions.", (PXErrorLevel) 4));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProformaTransactLine, PMProformaTransactLine.option> e)
  {
    this.ResetQtyAndAmountToInvoiceOnOptionChange(e.Row, e.Row.Option, (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMProformaTransactLine, PMProformaTransactLine.option>, PMProformaTransactLine, object>) e).OldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProformaTransactLine, PMProformaTransactLine.accountID> e)
  {
    PX.Objects.AR.Customer current = ((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Current;
    bool flag = (current != null ? (current.IsBranch.GetValueOrDefault() ? 1 : 0) : 0) != 0 && this.IsAdjustment(e.Row) && ((PXSelectBase<PX.Objects.AR.ARSetup>) this.arSetup).Current.IntercompanySalesAccountDefault == "L";
    if (e.Row == null)
      return;
    int num1;
    if (e.Row.InventoryID.HasValue)
    {
      int? inventoryId = e.Row.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      num1 = inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue ? 1 : 0;
    }
    else
      num1 = 1;
    int num2 = flag ? 1 : 0;
    if ((num1 | num2) == 0 || ((PXSelectBase<PX.Objects.CR.Location>) this.Location).Current == null)
      return;
    if (PXSelectorAttribute.Select<PMProformaTransactLine.accountID>(((PXSelectBase) this.TransactionLines).Cache, (object) e.Row, (object) ((PXSelectBase<PX.Objects.CR.Location>) this.Location).Current.CSalesAcctID) is PX.Objects.GL.Account account && account.AccountGroupID.HasValue)
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProformaTransactLine, PMProformaTransactLine.accountID>, PMProformaTransactLine, object>) e).NewValue = (object) ((PXSelectBase<PX.Objects.CR.Location>) this.Location).Current.CSalesAcctID;
    if (!(((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProformaTransactLine, PMProformaTransactLine.accountID>, PMProformaTransactLine, object>) e).NewValue != null | flag))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProformaTransactLine, PMProformaTransactLine.accountID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProformaTransactLine, PMProformaTransactLine.inventoryID> e)
  {
    if (e.Row == null || e.Row.InventoryID.HasValue || this.IsBilling || !this.IsAdjustment(e.Row))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProformaTransactLine, PMProformaTransactLine.inventoryID>, PMProformaTransactLine, object>) e).NewValue = (object) PMInventorySelectorAttribute.EmptyInventoryID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProformaTransactLine, PMProformaTransactLine.taxCategoryID> e)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null || e.Row == null)
      return;
    string taxCategoryId = PMTask.PK.FindDirty((PXGraph) this, e.Row.ProjectID, e.Row.TaskID)?.TaxCategoryID;
    if (string.IsNullOrEmpty(taxCategoryId) && e.Row.InventoryID.HasValue)
    {
      int? inventoryId = e.Row.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (!(inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue))
        taxCategoryId = ((PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<PMProformaTransactLine.inventoryID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMProformaTransactLine, PMProformaTransactLine.taxCategoryID>>) e).Cache, (object) e.Row))?.TaxCategoryID;
    }
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProformaTransactLine, PMProformaTransactLine.taxCategoryID>, PMProformaTransactLine, object>) e).NewValue = (object) taxCategoryId;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaTransactLine.taskID> e)
  {
    if (this.IsBilling || !this.IsAdjustment(e.Row))
      return;
    PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, e.Row.ProjectID, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaTransactLine.taskID>, PMProformaTransactLine, object>) e).NewValue);
    if (dirty != null && dirty.IsCompleted.GetValueOrDefault())
    {
      PXTaskIsCompletedException completedException = new PXTaskIsCompletedException(dirty.ProjectID, dirty.TaskID);
      completedException.ErrorValue = (object) dirty.TaskCD;
      throw completedException;
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMProformaProgressLine> e)
  {
    this.OnProformaLineDeleted((PMProformaLine) e.Row);
    this.SubtractFromInvoiced((PMProformaLine) e.Row);
    this.SubtractFromDraftRetained((PMProformaLine) e.Row);
    this.SubtractFromTotalRetained((PMProformaLine) e.Row);
    this.SubtractPerpaymentRemainder((PMProformaLine) e.Row, -1);
    this.RestoreValuesToInvoice(e.Row);
    if (!this.IsDocumentDeleted() && !this.RecalculatingContractRetainage)
      this.RecalculateRetainageOnDocument(((PXSelectBase<PMProject>) this.Project).Current);
    foreach (PXResult<PMProformaTransactLine> pxResult in ((PXSelectBase<PMProformaTransactLine>) new PXSelect<PMProformaTransactLine, Where<PMProformaTransactLine.refNbr, Equal<Current<PMProforma.refNbr>>, And<PMProformaTransactLine.revisionID, Equal<Current<PMProforma.revisionID>>, And<PMProformaLine.mergedToLineNbr, Equal<Required<PMProformaLine.mergedToLineNbr>>>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) e.Row.LineNbr
    }))
      this.UnmergeLine(PXResult<PMProformaTransactLine>.op_Implicit(pxResult), false);
  }

  private bool IsDocumentDeleted()
  {
    bool flag = false;
    if (((PXSelectBase<PMProforma>) this.Document).Current != null && ((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<PMProforma>) this.Document).Current) == 3)
      flag = true;
    return flag;
  }

  private ISet<int> GetUserBranches()
  {
    return (ISet<int>) new HashSet<int>(this._currentUserInformationProvider.GetActiveBranches().Select<BranchInfo, int>((Func<BranchInfo, int>) (b => b.Id)));
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PMProforma> e)
  {
    ISet<int> userBranches = this.GetUserBranches();
    using (new PXReadBranchRestrictedScope())
    {
      HashSet<int> other = new HashSet<int>((IEnumerable<int>) ((IQueryable<PXResult<PMProformaLine>>) PXSelectBase<PMProformaLine, PXViewOf<PMProformaLine>.BasedOn<SelectFromBase<PMProformaLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProformaLine.refNbr, Equal<BqlField<PMProforma.refNbr, IBqlString>.FromCurrent>>>>>.And<BqlOperand<PMProformaLine.revisionID, IBqlInt>.IsEqual<BqlField<PMProforma.revisionID, IBqlInt>.FromCurrent>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Select<PXResult<PMProformaLine>, int?>((Expression<Func<PXResult<PMProformaLine>, int?>>) (line => ((PMProformaLine) line).BranchID)).Where<int?>((Expression<Func<int?, bool>>) (branchID => branchID.HasValue)).Select<int?, int>((Expression<Func<int?, int>>) (branchID => branchID.Value)));
      foreach (int num in other)
      {
        if (PX.Objects.GL.Branch.PK.Find((PXGraph) this, new int?(num)) == null)
          userBranches.Add(num);
      }
      if (!userBranches.IsSupersetOf((IEnumerable<int>) other))
        throw new PXOperationCompletedSingleErrorException("You cannot delete the document with lines associated with the branches to which your user has no sufficient access rights.");
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PMProformaTransactLine> e)
  {
    ISet<int> userBranches = this.GetUserBranches();
    using (new PXReadBranchRestrictedScope())
    {
      PMProformaTransactLine row = e.Row;
      HashSet<int> other = new HashSet<int>((IEnumerable<int>) ((IQueryable<PXResult<PMTran>>) PXSelectBase<PMTran, PXViewOf<PMTran>.BasedOn<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.proformaRefNbr, Equal<P.AsString>>>>>.And<BqlOperand<PMTran.proformaLineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.RefNbr,
        (object) row.LineNbr
      })).Select<PXResult<PMTran>, int?>((Expression<Func<PXResult<PMTran>, int?>>) (line => ((PMTran) line).BranchID)).Where<int?>((Expression<Func<int?, bool>>) (branchID => branchID.HasValue)).Select<int?, int>((Expression<Func<int?, int>>) (branchID => branchID.Value)));
      foreach (int num in other)
      {
        if (PX.Objects.GL.Branch.PK.Find((PXGraph) this, new int?(num)) == null)
          userBranches.Add(num);
      }
      if (!userBranches.IsSupersetOf((IEnumerable<int>) other))
        throw new PXOperationCompletedSingleErrorException("You cannot delete the line linked to a project transaction associated with the branch to which your user has no sufficient access rights.");
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMProformaTransactLine> e)
  {
    this.OnProformaLineDeleted((PMProformaLine) e.Row);
    this.cachedReferencedTransactions = (Dictionary<int, List<PMTran>>) null;
    this.SubtractFromInvoiced((PMProformaLine) e.Row);
    this.SubtractFromDraftRetained((PMProformaLine) e.Row);
    this.SubtractFromTotalRetained((PMProformaLine) e.Row);
    this.SubtractPerpaymentRemainder((PMProformaLine) e.Row, -1);
    if (!e.Row.Merged.GetValueOrDefault())
      return;
    this.UnmergeLine(e.Row, false);
  }

  protected virtual void OnProformaLineDeleted(PMProformaLine line)
  {
    if (this.IsDocumentDeleted())
      return;
    foreach (PXResult<PMTran> pxResult in ((PXSelectBase<PMTran>) new PXSelect<PMTran, Where<PMTran.proformaRefNbr, Equal<Required<PMTran.proformaRefNbr>>, And<PMTran.proformaLineNbr, Equal<Required<PMTran.proformaLineNbr>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) line.RefNbr,
      (object) line.LineNbr
    }))
    {
      PMTran tran = PXResult<PMTran>.op_Implicit(pxResult);
      this.ClearProformaReference(tran);
      this.Unbill(tran);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PMTran, PMTran.projectCuryID> e)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null)
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMTran, PMTran.projectCuryID>>) e).ReturnValue = (object) ((PXSelectBase<PMProject>) this.Project).Current.CuryID;
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMTran> e)
  {
    PMProformaTransactLine proformaTransactLine1 = new PMProformaTransactLine();
    proformaTransactLine1.RefNbr = ((PXSelectBase<PMProforma>) this.Document).Current.RefNbr;
    proformaTransactLine1.RevisionID = ((PXSelectBase<PMProforma>) this.Document).Current.RevisionID;
    proformaTransactLine1.LineNbr = e.Row.ProformaLineNbr;
    PMProformaTransactLine proformaTransactLine2 = ((PXSelectBase<PMProformaTransactLine>) this.TransactionLines).Locate(proformaTransactLine1);
    if (proformaTransactLine2 != null)
    {
      PMProformaTransactLine proformaTransactLine3 = proformaTransactLine2;
      Decimal? curyBillableAmount = proformaTransactLine3.CuryBillableAmount;
      Decimal valueOrDefault1 = e.Row.ProjectCuryInvoicedAmount.GetValueOrDefault();
      proformaTransactLine3.CuryBillableAmount = curyBillableAmount.HasValue ? new Decimal?(curyBillableAmount.GetValueOrDefault() - valueOrDefault1) : new Decimal?();
      PMProformaTransactLine proformaTransactLine4 = proformaTransactLine2;
      Decimal? curyAmount = proformaTransactLine4.CuryAmount;
      Decimal valueOrDefault2 = e.Row.ProjectCuryInvoicedAmount.GetValueOrDefault();
      proformaTransactLine4.CuryAmount = curyAmount.HasValue ? new Decimal?(curyAmount.GetValueOrDefault() - valueOrDefault2) : new Decimal?();
      PMProformaTransactLine proformaTransactLine5 = proformaTransactLine2;
      Decimal? billableQty = proformaTransactLine5.BillableQty;
      Decimal valueOrDefault3 = e.Row.InvoicedQty.GetValueOrDefault();
      proformaTransactLine5.BillableQty = billableQty.HasValue ? new Decimal?(billableQty.GetValueOrDefault() - valueOrDefault3) : new Decimal?();
      PMProformaTransactLine proformaTransactLine6 = proformaTransactLine2;
      Decimal? qty = proformaTransactLine6.Qty;
      Decimal valueOrDefault4 = e.Row.InvoicedQty.GetValueOrDefault();
      proformaTransactLine6.Qty = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() - valueOrDefault4) : new Decimal?();
      ((PXSelectBase<PMProformaTransactLine>) this.TransactionLines).Update(proformaTransactLine2);
    }
    ((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<PMTran>>) e).Cache.SetStatus((object) e.Row, (PXEntryStatus) 1);
    e.Row.Billed = new bool?(false);
    e.Row.BilledDate = new DateTime?();
    e.Row.BillingID = (string) null;
    e.Row.ProformaRefNbr = (string) null;
    e.Row.ProformaLineNbr = new int?();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.baseCuryID> e)
  {
    int? branchId = ((PXGraph) this).Accessinfo.BranchID;
    if (((PXSelectBase<PMProforma>) this.Document).Current != null && ((PXSelectBase<PMProforma>) this.Document).Current.BranchID.HasValue)
      branchId = ((PXSelectBase<PMProforma>) this.Document).Current.BranchID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.baseCuryID>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue = (object) this.GetBaseCurency(branchId);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.baseCuryID>>) e).Cancel = true;
  }

  protected virtual string GetBaseCurency(int? branchID)
  {
    return ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()((PXGraph) this).BaseCuryID(branchID);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.CM.Extensions.CurrencyInfo> e)
  {
    Action<PXCache, PMProformaLine> action = (Action<PXCache, PMProformaLine>) ((cache, tran) =>
    {
      Decimal num1 = 0M;
      Decimal num2 = 0M;
      if (e.Row.CuryRate.HasValue)
      {
        num1 = e.Row.CuryConvBase(tran.CuryLineTotal.GetValueOrDefault());
        num2 = e.Row.CuryConvBase(tran.CuryPrepaidAmount.GetValueOrDefault());
      }
      PMProformaLine copy1 = cache.CreateCopy((object) tran) as PMProformaLine;
      copy1.LineTotal = new Decimal?(num1);
      copy1.PrepaidAmount = new Decimal?(num2);
      Decimal num3 = 0M;
      Decimal num4 = 0M;
      Decimal? nullable = e.OldRow.CuryRate;
      if (nullable.HasValue)
      {
        PX.Objects.CM.Extensions.CurrencyInfo oldRow1 = e.OldRow;
        nullable = tran.CuryLineTotal;
        Decimal valueOrDefault1 = nullable.GetValueOrDefault();
        num3 = oldRow1.CuryConvBase(valueOrDefault1);
        PX.Objects.CM.Extensions.CurrencyInfo oldRow2 = e.OldRow;
        nullable = tran.CuryPrepaidAmount;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        num4 = oldRow2.CuryConvBase(valueOrDefault2);
      }
      PMProformaLine copy2 = cache.CreateCopy((object) tran) as PMProformaLine;
      copy2.LineTotal = new Decimal?(num3);
      copy2.PrepaidAmount = new Decimal?(num4);
      this.SyncBudgets(cache, copy1, copy2);
    });
    foreach (PXResult<PMProformaTransactLine> pxResult in ((PXSelectBase<PMProformaTransactLine>) this.TransactionLines).Select(Array.Empty<object>()))
    {
      PMProformaLine pmProformaLine = (PMProformaLine) PXResult<PMProformaTransactLine>.op_Implicit(pxResult);
      action(((PXSelectBase) this.TransactionLines).Cache, pmProformaLine);
    }
    foreach (PXResult<PMProformaProgressLine> pxResult in ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Select(Array.Empty<object>()))
    {
      PMProformaLine pmProformaLine = (PMProformaLine) PXResult<PMProformaProgressLine>.op_Implicit(pxResult);
      action(((PXSelectBase) this.ProgressiveLines).Cache, pmProformaLine);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProformaTransactLine, PMProformaTransactLine.taskID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProformaTransactLine, PMProformaTransactLine.taskID>>) e).Cache.SetDefaultExt<PMProformaTransactLine.revenueTaskID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProformaTransactLine, PMProformaTransactLine.taskID>>) e).Cache.SetDefaultExt<PMProformaTransactLine.taxCategoryID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProformaTransactLine, PMProformaTransactLine.inventoryID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProformaTransactLine, PMProformaTransactLine.inventoryID>>) e).Cache.SetDefaultExt<PMProformaTransactLine.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProformaTransactLine, PMProformaTransactLine.inventoryID>>) e).Cache.SetDefaultExt<PMProformaTransactLine.taxCategoryID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProformaTransactLine, PMProformaTransactLine.inventoryID>>) e).Cache.SetDefaultExt<PMProformaTransactLine.accountID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProforma, PMProforma.locationID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProforma, PMProforma.locationID>>) e).Cache.SetDefaultExt<PMProforma.branchID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProforma, PMProforma.locationID>>) e).Cache.SetDefaultExt<PMProforma.workgroupID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProforma, PMProforma.locationID>>) e).Cache.SetDefaultExt<PMProforma.ownerID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProforma, PMProforma.locationID>>) e).Cache.SetDefaultExt<PMProforma.externalTaxExemptionNumber>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProforma, PMProforma.locationID>>) e).Cache.SetDefaultExt<PMProforma.avalaraCustomerUsageType>((object) e.Row);
    SharedRecordAttribute.DefaultRecord<PMProforma.shipAddressID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProforma, PMProforma.locationID>>) e).Cache, (object) e.Row);
    SharedRecordAttribute.DefaultRecord<PMProforma.shipContactID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProforma, PMProforma.locationID>>) e).Cache, (object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProforma, PMProforma.projectID> e)
  {
    if (e.Row == null)
      return;
    ((PXSelectBase) this.Project).Cache.Clear();
    PMProject current1 = ((PXSelectBase<PMProject>) this.Project).Current;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProforma, PMProforma.projectID>>) e).Cache.SetValueExt<PMProforma.customerID>((object) e.Row, (object) (int?) current1?.CustomerID);
    ((PXSelectBase) this.Customer).Cache.Clear();
    PX.Objects.AR.Customer current2 = ((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Current;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProforma, PMProforma.projectID>>) e).Cache.SetValueExt<PMProforma.locationID>((object) e.Row, (object) (int?) ((int?) current1?.LocationID ?? current2?.DefLocationID));
    if (current1 != null && current1.DefaultBranchID.HasValue)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProforma, PMProforma.projectID>>) e).Cache.SetValueExt<PMProforma.branchID>((object) e.Row, (object) current1.DefaultBranchID);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProforma, PMProforma.projectID>>) e).Cache.SetValueExt<PMProforma.curyID>((object) e.Row, (object) current1?.BillingCuryID);
    this.CopyProjectBillingInformation((int?) e.NewValue, e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProforma, PMProforma.projectID>>) e).Cache.SetDefaultExt<PMProforma.termsID>((object) e.Row);
    if (((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMProforma, PMProforma.projectID>, PMProforma, object>) e).OldValue == null)
      return;
    this.RemoveBillingRecords(((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMProforma, PMProforma.projectID>, PMProforma, object>) e).OldValue as int?);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProforma, PMProforma.aRInvoiceDocType> e)
  {
    if (e.Row == null)
      return;
    this.UpdateInvoice((string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMProforma, PMProforma.aRInvoiceDocType>, PMProforma, object>) e).OldValue, e.Row.ARInvoiceRefNbr, (string) e.NewValue, (string) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProforma, PMProforma.aRInvoiceDocType>>) e).Cache.SetValue<PMProforma.aRInvoiceRefNbr>((object) e.Row, (object) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProforma, PMProforma.aRInvoiceRefNbr> e)
  {
    if (e.Row == null)
      return;
    foreach (PXResult<PMBillingRecord> pxResult in PXSelectBase<PMBillingRecord, PXViewOf<PMBillingRecord>.BasedOn<SelectFromBase<PMBillingRecord, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMBillingRecord.proformaRefNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.RefNbr
    }))
    {
      PMBillingRecord pmBillingRecord = PXResult<PMBillingRecord>.op_Implicit(pxResult);
      pmBillingRecord.ARDocType = e.Row.ARInvoiceDocType;
      pmBillingRecord.ARRefNbr = e.Row.ARInvoiceRefNbr;
      ((PXSelectBase<PMBillingRecord>) this.BillingRecord).Update(pmBillingRecord);
    }
    this.UpdateInvoice(e.Row.ARInvoiceDocType, (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMProforma, PMProforma.aRInvoiceRefNbr>, PMProforma, object>) e).OldValue, e.Row.ARInvoiceDocType, (string) e.NewValue);
  }

  private void UpdateInvoice(string oldType, string oldNbr, string newType, string newNbr)
  {
    if (!string.IsNullOrEmpty(oldType) && !string.IsNullOrEmpty(oldNbr))
      this.UpdateInvoice(oldType, oldNbr, false);
    if (string.IsNullOrEmpty(newType) || string.IsNullOrEmpty(newNbr))
      return;
    this.UpdateInvoice(newType, newNbr, true);
  }

  private void UpdateInvoice(string arDocType, string arRefNbr, bool proformaExists)
  {
    PX.Objects.AR.ARInvoice arInvoice = PX.Objects.AR.ARInvoice.PK.Find((PXGraph) this, arDocType, arRefNbr);
    if (arInvoice == null)
      return;
    arInvoice.ProformaExists = new bool?(proformaExists);
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Invoices).Update(arInvoice);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMProforma> e)
  {
    if (e.Row == null || e.Operation == 3 || e.Row.FinPeriodID != null)
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMProforma>>) e).Cache.RaiseExceptionHandling<PMProforma.finPeriodID>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) "[finPeriodID]"
    }));
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PMProforma> e)
  {
    if (e.Row == null || e.TranStatus != null || e.Operation != 2)
      return;
    foreach (PMBillingRecord pmBillingRecord in ((PXSelectBase) this.BillingRecord).Cache.Inserted)
      pmBillingRecord.ProformaRefNbr = e.Row.RefNbr;
    foreach (PMProformaProgressLine line in ((PXSelectBase) this.ProgressiveLines).Cache.Inserted)
      this.CalculateProgressLinePreviouslyInvoicedValues(line);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProforma, PMProforma.branchID> e)
  {
    if (e.Row == null)
      return;
    if (((PXSelectBase) this.Location).View.SelectSingleBound(new object[1]
    {
      (object) e.Row
    }, Array.Empty<object>()) is PX.Objects.CR.Location location && location.CBranchID.HasValue)
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProforma, PMProforma.branchID>, PMProforma, object>) e).NewValue = (object) location.CBranchID;
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProforma, PMProforma.branchID>, PMProforma, object>) e).NewValue = (object) ((PXGraph) this).Accessinfo.BranchID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProforma, PMProforma.branchID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProforma, PMProforma.isMigratedRecord> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProforma, PMProforma.isMigratedRecord>, PMProforma, object>) e).NewValue = (object) this.IsMigrationMode();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProforma, PMProforma.taxZoneID> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProforma, PMProforma.taxZoneID>, PMProforma, object>) e).NewValue = (object) this.GetDefaultTaxZone(e.Row);
  }

  public virtual string GetDefaultTaxZone(PMProforma row)
  {
    string defaultTaxZone = (string) null;
    if (row != null)
    {
      if (((PXSelectBase) this.Location).View.SelectSingleBound(new object[1]
      {
        (object) row
      }, Array.Empty<object>()) is PX.Objects.CR.Location location1)
      {
        if (!string.IsNullOrEmpty(location1.CTaxZoneID))
        {
          defaultTaxZone = location1.CTaxZoneID;
        }
        else
        {
          PX.Objects.CR.BAccount baccount = (PX.Objects.CR.BAccount) PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelectJoin<BAccountR, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) row.BranchID
          }));
          if (baccount != null)
          {
            PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this, new object[2]
            {
              (object) baccount.BAccountID,
              (object) baccount.DefLocationID
            }));
            if (location != null)
              defaultTaxZone = location.VTaxZoneID;
          }
        }
      }
    }
    return defaultTaxZone;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMProforma> e)
  {
    if (this.SuppressRowSeleted)
      return;
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      PXUIFieldAttribute.SetVisible<PMProformaLine.costCodeID>(((PXSelectBase) this.ProgressiveLines).Cache, (object) null, ((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "C" || ((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "D");
      PXUIFieldAttribute.SetVisible<PMProformaLine.retainagePct>(((PXSelectBase) this.ProgressiveLines).Cache, (object) null, ((PXSelectBase<PMProject>) this.Project).Current.RetainageMode != "C");
      PXUIFieldAttribute.SetVisibility<PMProformaLine.retainagePct>(((PXSelectBase) this.ProgressiveLines).Cache, (object) null, ((PXSelectBase<PMProject>) this.Project).Current.RetainageMode != "C" ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
      PXUIFieldAttribute.SetEnabled<PMProformaProgressLine.curyRetainage>(((PXSelectBase) this.ProgressiveLines).Cache, (object) null, ((PXSelectBase<PMProject>) this.Project).Current.RetainageMode != "C");
      PXUIFieldAttribute.SetVisible<PMProformaLine.curyAllocatedRetainedAmount>(((PXSelectBase) this.ProgressiveLines).Cache, (object) null, ((PXSelectBase<PMProject>) this.Project).Current.RetainageMode == "C");
      PXUIFieldAttribute.SetVisibility<PMProformaLine.curyAllocatedRetainedAmount>(((PXSelectBase) this.ProgressiveLines).Cache, (object) null, ((PXSelectBase<PMProject>) this.Project).Current.RetainageMode == "C" ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
      PXUIFieldAttribute.SetVisible<PMProforma.curyAllocatedRetainedTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) null, ((PXSelectBase<PMProject>) this.Project).Current.RetainageMode == "C");
      PXUIFieldAttribute.SetVisible<PMProforma.retainagePct>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) null, ((PXSelectBase<PMProject>) this.Project).Current.RetainageMode == "C");
    }
    PXUIFieldAttribute.SetVisible<PMProformaTransactLine.curyMaxAmount>(((PXSelectBase) this.TransactionLines).Cache, (object) null, this.IsLimitsEnabled());
    PXUIFieldAttribute.SetVisible<PMProformaTransactLine.curyAvailableAmount>(((PXSelectBase) this.TransactionLines).Cache, (object) null, this.IsLimitsEnabled());
    PXUIFieldAttribute.SetVisible<PMProformaTransactLine.curyOverflowAmount>(((PXSelectBase) this.TransactionLines).Cache, (object) null, this.IsLimitsEnabled());
    PXUIFieldAttribute.SetVisibility<PMProformaTransactLine.curyMaxAmount>(((PXSelectBase) this.ProgressiveLines).Cache, (object) null, this.IsLimitsEnabled() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMProformaTransactLine.curyAvailableAmount>(((PXSelectBase) this.ProgressiveLines).Cache, (object) null, this.IsLimitsEnabled() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMProformaTransactLine.curyOverflowAmount>(((PXSelectBase) this.ProgressiveLines).Cache, (object) null, this.IsLimitsEnabled() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<PMProforma.curyID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) e.Row, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    PXUIFieldAttribute.SetVisible<PMProformaProgressLine.inventoryID>(((PXSelectBase) this.ProgressiveLines).Cache, (object) null, this.IsInventoryVisible());
    PXUIFieldAttribute.SetVisibility<PMProformaProgressLine.inventoryID>(((PXSelectBase) this.ProgressiveLines).Cache, (object) null, this.IsInventoryVisible() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXCache cache1 = ((PXSelectBase) this.ProgressiveLines).Cache;
    PMProject current1 = ((PXSelectBase<PMProject>) this.Project).Current;
    bool? nullable1;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      nullable1 = current1.PrepaymentEnabled;
      num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    int num2 = num1 != 0 ? 3 : 1;
    PXUIFieldAttribute.SetVisibility<PMProformaLine.curyPrepaidAmount>(cache1, (object) null, (PXUIVisibility) num2);
    PXCache cache2 = ((PXSelectBase) this.TransactionLines).Cache;
    PMProject current2 = ((PXSelectBase<PMProject>) this.Project).Current;
    int num3;
    if (current2 == null)
    {
      num3 = 0;
    }
    else
    {
      nullable1 = current2.PrepaymentEnabled;
      num3 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    int num4 = num3 != 0 ? 3 : 1;
    PXUIFieldAttribute.SetVisibility<PMProformaLine.curyPrepaidAmount>(cache2, (object) null, (PXUIVisibility) num4);
    PXCache cache3 = ((PXSelectBase) this.ProgressiveLines).Cache;
    PMProject current3 = ((PXSelectBase<PMProject>) this.Project).Current;
    int num5;
    if (current3 == null)
    {
      num5 = 0;
    }
    else
    {
      nullable1 = current3.PrepaymentEnabled;
      num5 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<PMProformaLine.curyPrepaidAmount>(cache3, (object) null, num5 != 0);
    PXCache cache4 = ((PXSelectBase) this.TransactionLines).Cache;
    PMProject current4 = ((PXSelectBase<PMProject>) this.Project).Current;
    int num6;
    if (current4 == null)
    {
      num6 = 0;
    }
    else
    {
      nullable1 = current4.PrepaymentEnabled;
      num6 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<PMProformaLine.curyPrepaidAmount>(cache4, (object) null, num6 != 0);
    PXCache cache5 = ((PXSelectBase) this.TransactionLines).Cache;
    PMProject current5 = ((PXSelectBase<PMProject>) this.Project).Current;
    int num7;
    if (current5 == null)
    {
      num7 = 0;
    }
    else
    {
      nullable1 = current5.PrepaymentEnabled;
      num7 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    int num8 = num7 != 0 ? 3 : 1;
    PXUIFieldAttribute.SetVisibility<PMProformaTransactLine.curyAmount>(cache5, (object) null, (PXUIVisibility) num8);
    PXCache cache6 = ((PXSelectBase) this.TransactionLines).Cache;
    PMProject current6 = ((PXSelectBase<PMProject>) this.Project).Current;
    int num9;
    if (current6 == null)
    {
      num9 = 0;
    }
    else
    {
      nullable1 = current6.PrepaymentEnabled;
      num9 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<PMProformaTransactLine.curyAmount>(cache6, (object) null, num9 != 0);
    PXUIFieldAttribute.SetVisible<PMProforma.externalTaxExemptionNumber>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>());
    PXUIFieldAttribute.SetVisible<PMProforma.avalaraCustomerUsageType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>());
    PXAction<PMProforma> applyPrepayments = this.autoApplyPrepayments;
    PMProject current7 = ((PXSelectBase<PMProject>) this.Project).Current;
    int num10;
    if (current7 == null)
    {
      num10 = 0;
    }
    else
    {
      nullable1 = current7.PrepaymentEnabled;
      num10 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    ((PXAction) applyPrepayments).SetEnabled(num10 != 0);
    if (e.Row == null)
      return;
    bool flag1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache.GetStatus((object) e.Row) == 2;
    nullable1 = e.Row.Hold;
    bool valueOrDefault1 = nullable1.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<PMProformaTransactLine.taskID>(((PXSelectBase) this.TransactionLines).Cache, (object) null, valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PMProformaTransactLine.inventoryID>(((PXSelectBase) this.TransactionLines).Cache, (object) null, valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PMProformaProgressLine.accountGroupID>(((PXSelectBase) this.ProgressiveLines).Cache, (object) null, valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PMProformaProgressLine.taskID>(((PXSelectBase) this.ProgressiveLines).Cache, (object) null, valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PMProformaProgressLine.inventoryID>(((PXSelectBase) this.ProgressiveLines).Cache, (object) null, valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PMProformaLine.merged>(((PXSelectBase) this.TransactionLines).Cache, (object) null, valueOrDefault1);
    nullable1 = e.Row.IsMigratedRecord;
    bool valueOrDefault2 = nullable1.GetValueOrDefault();
    bool flag2 = this.IsMigrationMode() & valueOrDefault2;
    PXUIFieldAttribute.SetVisible<PMProforma.isMigratedRecord>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) e.Row, valueOrDefault2);
    ((PXAction) this.uploadFromBudget).SetEnabled(valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PMProforma.aRInvoiceDocType>(((PXSelectBase) this.Document).Cache, (object) e.Row, flag2);
    PXUIFieldAttribute.SetEnabled<PMProforma.aRInvoiceRefNbr>(((PXSelectBase) this.Document).Cache, (object) e.Row, flag2);
    PXUIFieldAttribute.SetVisible<PMProforma.aRInvoiceRefNbr>(((PXSelectBase) this.Document).Cache, (object) e.Row, flag2);
    PXUIFieldAttribute.SetVisible<PMProforma.aRInvoiceRefName>(((PXSelectBase) this.Document).Cache, (object) e.Row, !flag2);
    int? nullable2;
    int num11;
    if (valueOrDefault1)
    {
      nullable2 = e.Row.NumberOfLines;
      int num12 = 0;
      num11 = nullable2.GetValueOrDefault() == num12 & nullable2.HasValue ? 1 : 0;
    }
    else
      num11 = 0;
    bool flag3 = num11 != 0;
    PXUIFieldAttribute.SetEnabled<PMProforma.projectID>(((PXSelectBase) this.Document).Cache, (object) e.Row, flag3);
    ((PXAction) this.uploadUnbilled).SetEnabled(valueOrDefault1 && !flag1 && !valueOrDefault2);
    ((PXAction) this.viewTransactionDetails).SetEnabled(!flag1);
    bool flag4 = this.CanEditDocument(e.Row);
    ((PXSelectBase) this.Document).Cache.AllowDelete = flag4;
    ((PXSelectBase) this.ProgressiveLines).Cache.AllowInsert = valueOrDefault1 | flag2;
    ((PXSelectBase) this.ProgressiveLines).Cache.AllowUpdate = valueOrDefault1;
    ((PXSelectBase) this.ProgressiveLines).Cache.AllowDelete = valueOrDefault1;
    ((PXSelectBase) this.TransactionLines).Cache.AllowInsert = valueOrDefault1 && !valueOrDefault2;
    ((PXSelectBase) this.TransactionLines).Cache.AllowUpdate = valueOrDefault1;
    ((PXSelectBase) this.TransactionLines).Cache.AllowDelete = valueOrDefault1 && !valueOrDefault2;
    ((PXSelectBase) this.Details).Cache.AllowDelete = valueOrDefault1;
    ((PXSelectBase) this.Billing_Address).Cache.AllowUpdate = flag4;
    ((PXSelectBase) this.Billing_Contact).Cache.AllowUpdate = flag4;
    ((PXSelectBase) this.Shipping_Address).Cache.AllowUpdate = flag4;
    ((PXSelectBase) this.Shipping_Contact).Cache.AllowUpdate = flag4;
    PXUIFieldAttribute.SetEnabled<PMProforma.invoiceDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<PMProforma.invoiceNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<PMProforma.finPeriodID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<PMProforma.description>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<PMProforma.curyID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<PMProforma.branchID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<PMProforma.taxZoneID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<PMProforma.termsID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<PMProforma.dueDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<PMProforma.discDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<PMProforma.externalTaxExemptionNumber>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<PMProforma.avalaraCustomerUsageType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<PMProforma.locationID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) e.Row, valueOrDefault1);
    PXUIFieldAttribute.SetDisplayName<PMRevenueBudget.curyRevisedAmount>(((PXGraph) this).Caches[typeof (PMRevenueBudget)], this.BillingInAnotherCurrency ? "Revised Budgeted Amount in Project Currency" : "Revised Budgeted Amount");
    PXUIFieldAttribute.SetDisplayName<PMRevenueBudget.curyInvoicedAmount>(((PXGraph) this).Caches[typeof (PMRevenueBudget)], this.BillingInAnotherCurrency ? "Draft Invoices Amount in Project Currency" : "Draft Invoice Amount");
    PXUIFieldAttribute.SetDisplayName<PMRevenueBudget.curyActualAmount>(((PXGraph) this).Caches[typeof (PMRevenueBudget)], this.BillingInAnotherCurrency ? "Actual Amount in Project Currency" : "Actual Amount");
    nullable2 = e.Row.CustomerID;
    if (nullable2.HasValue)
    {
      PMAddress pmAddress = PXResultset<PMAddress>.op_Implicit(((PXSelectBase<PMAddress>) this.Billing_Address).Select(Array.Empty<object>()));
      PMShippingAddress pmShippingAddress = PXResultset<PMShippingAddress>.op_Implicit(((PXSelectBase<PMShippingAddress>) this.Shipping_Address).Select(Array.Empty<object>()));
      int num13;
      if (PXAccess.FeatureInstalled<FeaturesSet.addressValidation>() && pmAddress != null)
      {
        nullable1 = pmAddress.IsDefaultAddress;
        bool flag5 = false;
        if (nullable1.GetValueOrDefault() == flag5 & nullable1.HasValue)
        {
          nullable1 = pmAddress.IsValidated;
          bool flag6 = false;
          num13 = nullable1.GetValueOrDefault() == flag6 & nullable1.HasValue ? 1 : 0;
          goto label_34;
        }
      }
      num13 = 0;
label_34:
      bool flag7 = num13 != 0;
      int num14;
      if (PXAccess.FeatureInstalled<FeaturesSet.addressValidation>() && pmShippingAddress != null)
      {
        nullable1 = pmShippingAddress.IsDefaultAddress;
        bool flag8 = false;
        if (nullable1.GetValueOrDefault() == flag8 & nullable1.HasValue)
        {
          nullable1 = pmShippingAddress.IsValidated;
          bool flag9 = false;
          num14 = nullable1.GetValueOrDefault() == flag9 & nullable1.HasValue ? 1 : 0;
          goto label_38;
        }
      }
      num14 = 0;
label_38:
      bool flag10 = num14 != 0;
      ((PXAction) this.validateAddresses).SetEnabled(flag7 | flag10);
    }
    ((PXAction) this.correct).SetEnabled(this.CanBeCorrected(e.Row));
    PXAction<PMProforma> mergeWithProgress1 = this.mergeWithProgress;
    nullable1 = e.Row.Hold;
    int num15 = nullable1.GetValueOrDefault() ? 1 : 0;
    ((PXAction) mergeWithProgress1).SetEnabled(num15 != 0);
    PXAction<PMProforma> mergeWithProgress2 = this.revertMergeWithProgress;
    nullable1 = e.Row.Hold;
    int num16 = nullable1.GetValueOrDefault() ? 1 : 0;
    ((PXAction) mergeWithProgress2).SetEnabled(num16 != 0);
    PXAction<PMProforma> fillRevenueTasks = this.fillRevenueTasks;
    nullable1 = e.Row.Hold;
    int num17 = nullable1.GetValueOrDefault() ? 1 : 0;
    ((PXAction) fillRevenueTasks).SetEnabled(num17 != 0);
  }

  public virtual bool CanEditDocument(PMProforma doc)
  {
    if (doc == null)
      return true;
    bool? nullable = doc.Released;
    if (nullable.GetValueOrDefault())
      return false;
    nullable = doc.Hold;
    if (nullable.GetValueOrDefault())
      return true;
    nullable = doc.Rejected;
    if (nullable.GetValueOrDefault())
      return false;
    nullable = doc.Approved;
    return nullable.GetValueOrDefault() && (!PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() || !((PXSelectBase<PMSetup>) this.Setup).Current.ProformaApprovalMapID.HasValue);
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<ProformaEntry.PMProformaOverflow> e)
  {
    PXUIFieldAttribute.SetVisible<ProformaEntry.PMProformaOverflow.curyOverflowTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ProformaEntry.PMProformaOverflow>>) e).Cache, (object) null, this.IsLimitsEnabled());
    Decimal? curyOverflowTotal = e.Row.CuryOverflowTotal;
    Decimal num = 0M;
    if (curyOverflowTotal.GetValueOrDefault() > num & curyOverflowTotal.HasValue)
      PXUIFieldAttribute.SetWarning<ProformaEntry.PMProformaOverflow.curyOverflowTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ProformaEntry.PMProformaOverflow>>) e).Cache, (object) e.Row, "The validation of the Max Limit Amount has failed.");
    else
      PXUIFieldAttribute.SetError<ProformaEntry.PMProformaOverflow.curyOverflowTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ProformaEntry.PMProformaOverflow>>) e).Cache, (object) e.Row, (string) null);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PMProformaProgressLine> e)
  {
    if (e.Row == null || e.Row.CuryPreviouslyInvoiced.HasValue)
      return;
    this.CalculateProgressLinePreviouslyInvoicedValues(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMProformaProgressLine> e)
  {
    if (e.Row == null)
      return;
    string progressBillingBase = e.Row.ProgressBillingBase;
    bool flag1 = progressBillingBase == "A";
    bool flag2 = progressBillingBase == "Q";
    PMRevenueBudget pmRevenueBudget = this.SelectRevenueBudget(e.Row);
    PXUIFieldAttribute.SetEnabled<PMProformaProgressLine.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProformaProgressLine>>) e).Cache, (object) e.Row, pmRevenueBudget != null);
    PXUIFieldAttribute.SetEnabled<PMProformaLine.qty>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProformaProgressLine>>) e).Cache, (object) e.Row, flag2);
    PXUIFieldAttribute.SetEnabled<PMProformaLine.curyAmount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProformaProgressLine>>) e).Cache, (object) e.Row, flag1);
    PXUIFieldAttribute.SetEnabled<PMProformaProgressLine.curyLineTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProformaProgressLine>>) e).Cache, (object) e.Row, flag1);
    PXUIFieldAttribute.SetEnabled<PMProformaProgressLine.curyUnitPrice>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProformaProgressLine>>) e).Cache, (object) e.Row, ((pmRevenueBudget == null ? 1 : (this.IsMigrationMode() ? 1 : 0)) & (flag2 ? 1 : 0)) != 0);
    PXUIFieldAttribute.SetEnabled<PMProformaProgressLine.progressBillingBase>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProformaProgressLine>>) e).Cache, (object) e.Row, this.IsMigrationMode());
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMProformaTransactLine> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<PMProformaLine.curyPrepaidAmount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProformaTransactLine>>) e).Cache, (object) e.Row, this.IsPrepaidAmountEnabled((PMProformaLine) e.Row));
    PXUIFieldAttribute.SetEnabled<PMProformaTransactLine.option>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProformaTransactLine>>) e).Cache, (object) e.Row, !e.Row.IsPrepayment.GetValueOrDefault());
    ((PXSelectBase) this.Details).AllowSelect = !e.Row.IsPrepayment.GetValueOrDefault();
    foreach (KeyValuePair<string, bool> keyValuePair in this.GetStateForTransactionLine(e.Row))
      PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProformaTransactLine>>) e).Cache, (object) e.Row, keyValuePair.Key, keyValuePair.Value);
  }

  protected virtual List<KeyValuePair<string, bool>> GetStateForTransactionLine(
    PMProformaTransactLine line)
  {
    List<KeyValuePair<string, bool>> forTransactionLine = new List<KeyValuePair<string, bool>>();
    bool flag = this.IsAdjustment(line);
    forTransactionLine.Add(new KeyValuePair<string, bool>("TaskID", flag));
    forTransactionLine.Add(new KeyValuePair<string, bool>("InventoryID", flag));
    forTransactionLine.Add(new KeyValuePair<string, bool>("Option", !flag && !line.Merged.GetValueOrDefault()));
    forTransactionLine.Add(new KeyValuePair<string, bool>("Qty", line.Option != "X" && !line.Merged.GetValueOrDefault()));
    forTransactionLine.Add(new KeyValuePair<string, bool>("UOM", !line.Merged.GetValueOrDefault()));
    bool? merged = line.Merged;
    forTransactionLine.Add(new KeyValuePair<string, bool>("CuryUnitPrice", (!merged.GetValueOrDefault() ? 1 : 0) != 0));
    merged = line.Merged;
    forTransactionLine.Add(new KeyValuePair<string, bool>("CuryAmount", (!merged.GetValueOrDefault() ? 1 : 0) != 0));
    int num;
    if (line.Option != "X")
    {
      merged = line.Merged;
      num = !merged.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    forTransactionLine.Add(new KeyValuePair<string, bool>("CuryLineTotal", num != 0));
    merged = line.Merged;
    forTransactionLine.Add(new KeyValuePair<string, bool>("RetainagePct", (!merged.GetValueOrDefault() ? 1 : 0) != 0));
    merged = line.Merged;
    forTransactionLine.Add(new KeyValuePair<string, bool>("CuryRetainage", (!merged.GetValueOrDefault() ? 1 : 0) != 0));
    merged = line.Merged;
    forTransactionLine.Add(new KeyValuePair<string, bool>("AccountID", (!merged.GetValueOrDefault() ? 1 : 0) != 0));
    merged = line.Merged;
    forTransactionLine.Add(new KeyValuePair<string, bool>("SubID", (!merged.GetValueOrDefault() ? 1 : 0) != 0));
    merged = line.Merged;
    forTransactionLine.Add(new KeyValuePair<string, bool>("RevenueTaskID", (!merged.GetValueOrDefault() ? 1 : 0) != 0));
    return forTransactionLine;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMProformaTransactLine> e)
  {
    this.SyncBudgets(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PMProformaTransactLine>>) e).Cache, (PMProformaLine) e.Row, (PMProformaLine) e.OldRow);
    if (!e.Row.CuryMaxAmount.HasValue || !(this.GetAmountInProjectCurrency(e.Row.CuryLineTotal) != this.GetAmountInProjectCurrency(e.OldRow.CuryLineTotal)))
      return;
    ((PXSelectBase) this.TransactionLines).View.RequestRefresh();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaLine.merged> e)
  {
    if (this.IsCorrectionProcess || !((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProformaTransactLine, PMProformaLine.merged>, PMProformaTransactLine, object>) e).NewValue).GetValueOrDefault())
      return;
    string mergeError = this.GetMergeError(e.Row);
    if (mergeError != null)
      throw new PXSetPropertyException<PMProformaLine.merged>(mergeError);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProformaTransactLine, PMProformaLine.merged> e)
  {
    if (this.IsCorrectionProcess)
      return;
    bool? nullable = e.Row.Merged;
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
    {
      nullable = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMProformaTransactLine, PMProformaLine.merged>, PMProformaTransactLine, object>) e).OldValue;
      if (nullable.GetValueOrDefault())
      {
        this.UnmergeLine(e.Row, true);
        return;
      }
    }
    nullable = e.Row.Merged;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMProformaTransactLine, PMProformaLine.merged>, PMProformaTransactLine, object>) e).OldValue;
    if (nullable.GetValueOrDefault())
      return;
    this.MergeLine(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProformaTransactLine, PMProformaTransactLine.revenueTaskID> e)
  {
    if (this.IsCorrectionProcess)
      return;
    int? defaultRevenueTask = this.GetDefaultRevenueTask(e.Row);
    if (!defaultRevenueTask.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProformaTransactLine, PMProformaTransactLine.revenueTaskID>, PMProformaTransactLine, object>) e).NewValue = (object) defaultRevenueTask;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMProformaProgressLine> e)
  {
    this.SyncBudgets(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PMProformaProgressLine>>) e).Cache, (PMProformaLine) e.Row, (PMProformaLine) e.OldRow);
    if (!this.RecalculatingContractRetainage)
    {
      Decimal? curyLineTotal1 = e.Row.CuryLineTotal;
      Decimal? curyLineTotal2 = e.OldRow.CuryLineTotal;
      if (!(curyLineTotal1.GetValueOrDefault() == curyLineTotal2.GetValueOrDefault() & curyLineTotal1.HasValue == curyLineTotal2.HasValue))
        this.RecalculateRetainageOnDocument(((PXSelectBase<PMProject>) this.Project).Current);
    }
    this.CalculateProgressLinePreviouslyInvoicedValues(e.Row);
  }

  protected virtual void CalculateProgressLinePreviouslyInvoicedValues(PMProformaProgressLine line)
  {
    if (line.TaskID.HasValue && line.ProgressBillingBase == "A")
    {
      ProformaEntry.ProformaTotalsCounter.AmountBaseTotals amountBaseTotals = this.TotalsCounter.GetAmountBaseTotals((PXGraph) this, ((PXSelectBase<PMProforma>) this.Document).Current.RefNbr, line);
      line.CuryPreviouslyInvoiced = new Decimal?(amountBaseTotals.CuryLineTotal);
      line.PreviouslyInvoiced = new Decimal?(amountBaseTotals.LineTotal);
      line.PreviouslyInvoicedQty = new Decimal?(0M);
      line.ActualQty = new Decimal?();
    }
    else
    {
      if (!line.TaskID.HasValue || !(line.ProgressBillingBase == "Q"))
        return;
      ProformaEntry.ProformaTotalsCounter.QuantityBaseTotals quantityBaseTotals = this.TotalsCounter.GetQuantityBaseTotals((PXGraph) this, ((PXSelectBase<PMProforma>) this.Document).Current.RefNbr, line);
      line.CuryPreviouslyInvoiced = new Decimal?(quantityBaseTotals.CuryLineTotal);
      line.PreviouslyInvoiced = new Decimal?(quantityBaseTotals.LineTotal);
      line.PreviouslyInvoicedQty = new Decimal?(quantityBaseTotals.QuantityTotal);
      line.ActualQty = new Decimal?();
      PMRevenueBudget pmRevenueBudget = this.SelectRevenueBudget(line);
      if (pmRevenueBudget == null)
        return;
      Decimal result;
      if (INUnitAttribute.TryConvertGlobalUnits((PXGraph) this, pmRevenueBudget.UOM, line.UOM, pmRevenueBudget.ActualQty.GetValueOrDefault(), INPrecision.QUANTITY, out result))
      {
        line.ActualQty = new Decimal?(result);
      }
      else
      {
        string str = PXMessages.LocalizeFormatNoPrefix("The quantity cannot be calculated because the unit conversion from {0} to {1} is not defined.", new object[2]
        {
          (object) pmRevenueBudget.UOM,
          (object) line.UOM
        });
        PXUIFieldAttribute.SetWarning<PMProformaLine.actualQty>(((PXSelectBase) this.ProgressiveLines).Cache, (object) line, str);
      }
    }
  }

  private void SyncBudgets(PXCache cache, PMProformaLine row, PMProformaLine oldRow)
  {
    PX.Objects.GL.Account account1 = PXSelectorAttribute.Select<PMProformaTransactLine.accountID>(cache, (object) row, (object) row.AccountID) as PX.Objects.GL.Account;
    if (PXSelectorAttribute.Select<PMProformaTransactLine.accountID>(cache, (object) oldRow, (object) oldRow.AccountID) is PX.Objects.GL.Account account2)
    {
      this.SubtractFromInvoiced(oldRow, account2.AccountGroupID);
      this.SubtractFromDraftRetained(oldRow, account2.AccountGroupID);
      this.SubtractFromTotalRetained(oldRow, account2.AccountGroupID);
    }
    if (account1 != null)
    {
      this.AddToInvoiced(row, account1.AccountGroupID);
      this.AddToDraftRetained(row, account1.AccountGroupID);
      this.AddToTotalRetained(row, account1.AccountGroupID);
    }
    Decimal? curyPrepaidAmount1 = row.CuryPrepaidAmount;
    Decimal? curyPrepaidAmount2 = oldRow.CuryPrepaidAmount;
    if (curyPrepaidAmount1.GetValueOrDefault() == curyPrepaidAmount2.GetValueOrDefault() & curyPrepaidAmount1.HasValue == curyPrepaidAmount2.HasValue)
    {
      Decimal? prepaidAmount1 = row.PrepaidAmount;
      Decimal? prepaidAmount2 = oldRow.PrepaidAmount;
      if (prepaidAmount1.GetValueOrDefault() == prepaidAmount2.GetValueOrDefault() & prepaidAmount1.HasValue == prepaidAmount2.HasValue)
        return;
    }
    this.SubtractPerpaymentRemainder(oldRow, -1);
    this.SubtractPerpaymentRemainder(row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProforma, PMProforma.branchID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProforma, PMProforma.branchID>>) e).Cache.SetDefaultExt<PMProforma.taxZoneID>((object) e.Row);
    foreach (PXResult<PMTaxTran> pxResult in ((PXSelectBase<PMTaxTran>) this.Taxes).Select(Array.Empty<object>()))
    {
      PMTaxTran pmTaxTran = PXResult<PMTaxTran>.op_Implicit(pxResult);
      if (((PXSelectBase) this.Taxes).Cache.GetStatus((object) pmTaxTran) == null)
        ((PXSelectBase) this.Taxes).Cache.SetStatus((object) pmTaxTran, (PXEntryStatus) 1);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMTaxTran, PMTaxTran.taxZoneID> e)
  {
    if (((PXSelectBase<PMProforma>) this.Document).Current == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMTaxTran, PMTaxTran.taxZoneID>, PMTaxTran, object>) e).NewValue = (object) ((PXSelectBase<PMProforma>) this.Document).Current.TaxZoneID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMTaxTran, PMTaxTran.taxZoneID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMTaxTran> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<ARTaxTran.taxID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTaxTran>>) e).Cache, (object) e.Row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTaxTran>>) e).Cache.GetStatus((object) e.Row) == 2);
  }

  protected virtual void _(PX.Data.Events.RowInserting<PMTaxTran> e)
  {
    PXParentAttribute.SetParent(((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<PMTaxTran>>) e).Cache, (object) e.Row, typeof (PMProforma), (object) ((PXSelectBase<PMProforma>) this.Document).Current);
  }

  protected virtual void _(
    PX.Data.Events.ExceptionHandling<PMTaxTran, PMTaxTran.taxZoneID> e)
  {
    Exception exception = (Exception) (((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<PMTaxTran, PMTaxTran.taxZoneID>>) e).Exception as PXSetPropertyException);
    if (exception == null)
      return;
    ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<PMProforma.taxZoneID>((object) ((PXSelectBase<PMProforma>) this.Document).Current, (object) null, exception);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PMBudgetAccum> e)
  {
    ((PXGraph) this).Caches[typeof (PMRevenueBudget)].Clear();
  }

  protected virtual void PMProforma_InvoiceDate_FieldUpdated(
    PX.Data.Events.FieldUpdated<PMProforma.invoiceDate> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProforma.invoiceDate>>) e).Cache.SetDefaultExt<PMProforma.finPeriodID>(e.Row);
  }

  protected virtual void PMProforma_BranchID_FieldUpdated(PX.Data.Events.FieldUpdated<PMProforma.branchID> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProforma.branchID>>) e).Cache.SetDefaultExt<PMProforma.finPeriodID>(e.Row);
  }

  public virtual void Clear()
  {
    this.cachedReferencedTransactions = (Dictionary<int, List<PMTran>>) null;
    ((PXGraph) this).Clear();
  }

  public virtual PMProforma CorrectProforma(ProformaEntry target, PMProforma doc)
  {
    PMProforma correctionProforma = this.CreateCorrectionProforma(doc);
    string note = PXNoteAttribute.GetNote(((PXSelectBase) this.Document).Cache, (object) doc);
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes(((PXSelectBase) this.Document).Cache, (object) doc);
    PX.Objects.AR.ARRegister reversingDocument = this.GetReversingDocument(doc.ARInvoiceDocType, doc.ARInvoiceRefNbr);
    ((PXSelectBase) this.Document).Cache.SetValue<PMProforma.corrected>((object) doc, (object) true);
    ((PXSelectBase) this.Document).Cache.SetValue<PMProforma.reversedARInvoiceDocType>((object) doc, (object) reversingDocument?.DocType);
    ((PXSelectBase) this.Document).Cache.SetValue<PMProforma.reversedARInvoiceRefNbr>((object) doc, (object) reversingDocument?.RefNbr);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Document).Cache, (object) doc);
    PMProforma pmProforma1 = ((PXSelectBase<PMProforma>) target.Document).Insert(correctionProforma);
    if (note != null)
      PXNoteAttribute.SetNote(((PXSelectBase) target.Document).Cache, (object) pmProforma1, note);
    if (fileNotes != null && fileNotes.Length != 0)
      PXNoteAttribute.SetFileNotes(((PXSelectBase) target.Document).Cache, (object) pmProforma1, fileNotes);
    foreach (PXResult<PMProforma> pxResult in ((PXSelectBase<PMProforma>) new PXSelect<PMProforma, Where<PMProforma.projectID, Equal<Required<PMProforma.projectID>>, And<PMProforma.refNbr, Greater<Required<PMProforma.refNbr>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) doc.ProjectID,
      (object) doc.RefNbr
    }))
    {
      PMProforma pmProforma2 = PXResult<PMProforma>.op_Implicit(pxResult);
      ((PXSelectBase) this.Document).Cache.SetValue<PMProforma.isAIAOutdated>((object) pmProforma2, (object) true);
      GraphHelper.MarkUpdated(((PXSelectBase) this.Document).Cache, (object) pmProforma2);
    }
    return pmProforma1;
  }

  protected virtual void ValidateAndRaiseExceptionCanCorrect(PMProforma proforma)
  {
    this.ValidateThereIsNoUnreleasedRetainageInvoices(proforma);
    this.ValidateThatThereAreNoPayments(proforma);
    this.ValidateThatInvoiceCanBeReverted(proforma);
  }

  protected virtual void ValidateThereIsNoUnreleasedRetainageInvoices(PMProforma proforma)
  {
    if (string.IsNullOrEmpty(proforma.ARInvoiceRefNbr))
      return;
    PX.Objects.AR.ARInvoice[] array = GraphHelper.RowCast<PX.Objects.AR.ARInvoice>((IEnumerable) PXSelectBase<PX.Objects.AR.ARInvoice, PXViewOf<PX.Objects.AR.ARInvoice>.BasedOn<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.isRetainageDocument, Equal<True>>>>, And<BqlOperand<PX.Objects.AR.ARInvoice.origDocType, IBqlString>.IsEqual<BqlField<PMProforma.aRInvoiceDocType, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.AR.ARInvoice.origRefNbr, IBqlString>.IsEqual<BqlField<PMProforma.aRInvoiceRefNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.released, IBqlBool>.IsEqual<False>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).ToArray<PX.Objects.AR.ARInvoice>();
    if (((IEnumerable<PX.Objects.AR.ARInvoice>) array).Any<PX.Objects.AR.ARInvoice>())
      throw new PXException("You cannot correct the pro forma invoice because the corresponding AR document ({0}) has at least one unreleased retainage document. Delete the following retainage document first: {1}.", new object[2]
      {
        (object) $"{((PXSelectBase<PMProforma>) this.Document).Current.ARInvoiceDocType} {((PXSelectBase<PMProforma>) this.Document).Current.ARInvoiceRefNbr}",
        (object) string.Join(", ", ((IEnumerable<PX.Objects.AR.ARInvoice>) array).Select<PX.Objects.AR.ARInvoice, string>((Func<PX.Objects.AR.ARInvoice, string>) (a => $"{a.DocType} {a.RefNbr}")))
      });
  }

  protected virtual void ValidateThatThereAreNoPayments(PMProforma proforma)
  {
    string reversingDocType = this.GetReversingDocType(proforma.ARInvoiceDocType);
    ARAdjust[] array = GraphHelper.RowCast<ARAdjust>((IEnumerable) PXSelectBase<ARAdjust, PXViewOf<ARAdjust>.BasedOn<SelectFromBase<ARAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjdDocType, Equal<P.AsString>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjdRefNbr, Equal<P.AsString>>>>>.And<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsNotEqual<P.AsString>>>>>.And<BqlOperand<ARAdjust.voided, IBqlBool>.IsEqual<False>>>.Aggregate<To<GroupBy<ARAdjust.adjgDocType>, GroupBy<ARAdjust.adjgRefNbr>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) proforma.ARInvoiceDocType,
      (object) proforma.ARInvoiceRefNbr,
      (object) reversingDocType
    })).ToArray<ARAdjust>();
    if (((IEnumerable<ARAdjust>) array).Any<ARAdjust>())
      throw new PXException("You cannot correct the pro forma invoice because the corresponding AR document ({0}) has at least one applied payment document. Reverse the payment application or void the following payment first: {1}.", new object[2]
      {
        (object) $"{((PXSelectBase<PMProforma>) this.Document).Current.ARInvoiceDocType} {((PXSelectBase<PMProforma>) this.Document).Current.ARInvoiceRefNbr}",
        (object) string.Join(", ", ((IEnumerable<ARAdjust>) array).Select<ARAdjust, string>((Func<ARAdjust, string>) (a => $"{a.AdjgDocType} {a.AdjgRefNbr}")))
      });
  }

  protected virtual void ValidateThatInvoiceCanBeReverted(PMProforma proforma)
  {
    PX.Objects.AR.ARInvoice arInvoice = PX.Objects.AR.ARInvoice.PK.Find((PXGraph) this, proforma.ARInvoiceDocType, proforma.ARInvoiceRefNbr);
    if ((arInvoice != null ? (!arInvoice.RetainageApply.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return;
    Decimal? curyRetainageTotal = arInvoice.CuryRetainageTotal;
    Decimal? retainageUnreleasedAmt = arInvoice.CuryRetainageUnreleasedAmt;
    if (!(curyRetainageTotal.GetValueOrDefault() == retainageUnreleasedAmt.GetValueOrDefault() & curyRetainageTotal.HasValue == retainageUnreleasedAmt.HasValue) && this.GetReversingDocument(arInvoice.DocType, arInvoice.RefNbr) == null)
      throw new PXException("You cannot correct the pro forma invoice because the corresponding AR document has at least one released retainage document that has not been reversed. To be able to make corrections, make the Original Retainage and Unreleased Retainage amounts equal in the {0} AR document first by reversing the retainage documents: {1}.", new object[2]
      {
        (object) $"{((PXSelectBase<PMProforma>) this.Document).Current.ARInvoiceDocType} {((PXSelectBase<PMProforma>) this.Document).Current.ARInvoiceRefNbr}",
        (object) string.Join(", ", ((IEnumerable<PX.Objects.AR.ARInvoice>) GraphHelper.RowCast<PX.Objects.AR.ARInvoice>((IEnumerable) PXSelectBase<PX.Objects.AR.ARInvoice, PXViewOf<PX.Objects.AR.ARInvoice>.BasedOn<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.isRetainageDocument, Equal<True>>>>, And<BqlOperand<PX.Objects.AR.ARInvoice.origDocType, IBqlString>.IsEqual<P.AsString.ASCII>>>, And<BqlOperand<PX.Objects.AR.ARInvoice.origRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.released, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) proforma.ARInvoiceDocType,
          (object) proforma.ARInvoiceRefNbr
        })).ToArray<PX.Objects.AR.ARInvoice>()).Select<PX.Objects.AR.ARInvoice, string>((Func<PX.Objects.AR.ARInvoice, string>) (a => $"{a.DocType} {a.RefNbr}")))
      });
  }

  public virtual PMProforma CreateCorrectionProforma(PMProforma original)
  {
    PMProforma copy = (PMProforma) ((PXSelectBase) this.Document).Cache.CreateCopy((object) original);
    int? revisionId = copy.RevisionID;
    copy.RevisionID = revisionId.HasValue ? new int?(revisionId.GetValueOrDefault() + 1) : new int?();
    copy.Status = (string) null;
    copy.Hold = new bool?();
    copy.Approved = new bool?();
    copy.Rejected = new bool?();
    copy.Released = new bool?();
    copy.Corrected = new bool?();
    copy.ARInvoiceDocType = (string) null;
    copy.ARInvoiceRefNbr = (string) null;
    copy.TransactionalTotal = new Decimal?();
    copy.CuryTransactionalTotal = new Decimal?();
    copy.ProgressiveTotal = new Decimal?();
    copy.CuryProgressiveTotal = new Decimal?();
    copy.RetainageDetailTotal = new Decimal?();
    copy.CuryRetainageDetailTotal = new Decimal?();
    copy.RetainageTaxTotal = new Decimal?();
    copy.CuryRetainageTaxTotal = new Decimal?();
    copy.TaxTotal = new Decimal?();
    copy.CuryTaxTotal = new Decimal?();
    copy.DocTotal = new Decimal?();
    copy.CuryDocTotal = new Decimal?();
    copy.CuryAllocatedRetainedTotal = new Decimal?();
    copy.AllocatedRetainedTotal = new Decimal?();
    copy.IsTaxValid = new bool?();
    copy.tstamp = (byte[]) null;
    copy.NoteID = new Guid?();
    copy.IsAIAOutdated = new bool?(true);
    copy.ReversedARInvoiceDocType = (string) null;
    copy.ReversedARInvoiceRefNbr = (string) null;
    return copy;
  }

  public virtual PMProformaTransactLine CreateCorrectionProformaTransactLine(
    PMProformaTransactLine original)
  {
    PMProformaTransactLine copy = (PMProformaTransactLine) ((PXSelectBase) this.TransactionLines).Cache.CreateCopy((object) original);
    copy.Released = new bool?();
    copy.Corrected = new bool?();
    copy.ARInvoiceDocType = (string) null;
    copy.ARInvoiceRefNbr = (string) null;
    copy.ARInvoiceLineNbr = new int?();
    copy.RevisionID = new int?();
    copy.NoteID = new Guid?();
    copy.tstamp = (byte[]) null;
    return copy;
  }

  public virtual PMProformaProgressLine CreateCorrectionProformaProgressiveLine(
    PMProformaProgressLine original)
  {
    PMProformaProgressLine copy = (PMProformaProgressLine) ((PXSelectBase) this.ProgressiveLines).Cache.CreateCopy((object) original);
    copy.Released = new bool?();
    copy.Corrected = new bool?();
    copy.ARInvoiceDocType = (string) null;
    copy.ARInvoiceRefNbr = (string) null;
    copy.ARInvoiceLineNbr = new int?();
    copy.RevisionID = new int?();
    copy.NoteID = new Guid?();
    copy.tstamp = (byte[]) null;
    return copy;
  }

  public virtual bool CanBeCorrected(PMProforma row)
  {
    if (!row.Released.GetValueOrDefault() || row.Corrected.GetValueOrDefault() || row.IsMigratedRecord.GetValueOrDefault())
      return false;
    PX.Objects.AR.ARInvoice arInvoice = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Current<PMProforma.aRInvoiceDocType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Current<PMProforma.aRInvoiceRefNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    return arInvoice == null || arInvoice.Released.GetValueOrDefault();
  }

  protected virtual void ValidateLimitsOnUnhold(PMProforma row)
  {
    Decimal? curyOverflowTotal = ((PXSelectBase<ProformaEntry.PMProformaOverflow>) this.Overflow).Current.CuryOverflowTotal;
    Decimal num = 0M;
    if (curyOverflowTotal.GetValueOrDefault() > num & curyOverflowTotal.HasValue && ((PXSelectBase<PMSetup>) this.Setup).Current.OverLimitErrorLevel == "E")
      throw new PXRowPersistingException(typeof (ProformaEntry.PMProformaOverflow.overflowTotal).Name, (object) null, "The validation of the Max Limit Amount value has failed. Do one of the following: adjust the amounts of the document, adjust the limits of the budget, or select Ignore in the Validate T&M Revenue Budget Limits box on the Project Preferences (PM101000) form.");
  }

  public virtual void ReleaseDocument(PMProforma doc)
  {
    bool? nullable1 = doc != null ? doc.Released : throw new ArgumentNullException();
    if (nullable1.GetValueOrDefault())
      throw new PXException("This document is already released.");
    if (this.IsMigrationMode())
    {
      nullable1 = doc.IsMigratedRecord;
      bool flag = false;
      if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
        throw new PXException("The document cannot be processed because it was created when migration mode was deactivated. To process the document, clear the Activate Migration Mode check box on the Projects Preferences (PM101000) form.");
    }
    if (!this.IsMigrationMode())
    {
      nullable1 = doc.IsMigratedRecord;
      if (nullable1.GetValueOrDefault())
        throw new PXException("The document cannot be processed because it was created when migration mode was activated. To process the document, activate migration mode on the Projects Preferences (PM101000) form.");
    }
    if (this.IsMigrationMode())
    {
      foreach (PXResult<PMProformaProgressLine> pxResult in ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Select(Array.Empty<object>()))
      {
        PMProformaProgressLine proformaProgressLine = PXResult<PMProformaProgressLine>.op_Implicit(pxResult);
        ((PXSelectBase) this.ProgressiveLines).Cache.SetValue<PMProformaLine.aRInvoiceDocType>((object) proformaProgressLine, (object) doc.ARInvoiceDocType);
        ((PXSelectBase) this.ProgressiveLines).Cache.SetValue<PMProformaLine.aRInvoiceRefNbr>((object) proformaProgressLine, (object) doc.ARInvoiceRefNbr);
        ((PXSelectBase) this.ProgressiveLines).Cache.SetValue<PMProformaLine.released>((object) proformaProgressLine, (object) true);
        GraphHelper.MarkUpdated(((PXSelectBase) this.ProgressiveLines).Cache, (object) proformaProgressLine, true);
      }
      doc.Released = new bool?(true);
      ((PXSelectBase<PMProforma>) this.Document).Update(doc);
      ((PXAction) this.Save).Press();
    }
    else
    {
      this.CheckMigrationMode();
      this.ValidateBeforeRelease(doc);
      this.ValidatePrecedingBeforeRelease(doc);
      this.ValidatePrecedingInvoicesBeforeRelease(doc);
      this.ValidateBranchBeforeRelease(doc);
      PMProject pmProject = (PMProject) ((PXSelectBase) this.Project).View.SelectSingleBound(new object[1]
      {
        (object) doc
      }, Array.Empty<object>());
      PX.Objects.AR.ARInvoice arInvoice1 = (PX.Objects.AR.ARInvoice) null;
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        PX.Objects.AR.ARInvoice arInvoice2 = this.ProcessRevision();
        this.ProcessReversingCreditMemo();
        RegisterEntry instance1 = PXGraph.CreateInstance<RegisterEntry>();
        ((PXGraph) instance1).Clear();
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        ((PXGraph) instance1).FieldVerifying.AddHandler<PMTran.projectID>(ProformaEntry.\u003C\u003Ec.\u003C\u003E9__251_0 ?? (ProformaEntry.\u003C\u003Ec.\u003C\u003E9__251_0 = new PXFieldVerifying((object) ProformaEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CReleaseDocument\u003Eb__251_0))));
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        ((PXGraph) instance1).FieldVerifying.AddHandler<PMTran.taskID>(ProformaEntry.\u003C\u003Ec.\u003C\u003E9__251_1 ?? (ProformaEntry.\u003C\u003Ec.\u003C\u003E9__251_1 = new PXFieldVerifying((object) ProformaEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CReleaseDocument\u003Eb__251_1))));
        PMRegister pmRegister = (PMRegister) ((PXSelectBase) instance1.Document).Cache.Insert();
        pmRegister.OrigDocType = "AR";
        pmRegister.Description = PXMessages.LocalizeNoPrefix("Allocation Reversal on AR Invoice Generation");
        ((PXSelectBase<PMRegister>) instance1.Document).Current = pmRegister;
        PMBillEngine instance2 = PXGraph.CreateInstance<PMBillEngine>();
        ARInvoiceEntry instance3 = PXGraph.CreateInstance<ARInvoiceEntry>();
        ((PXGraph) instance3).Clear();
        ((PXSelectBase<PX.Objects.AR.ARSetup>) instance3.ARSetup).Current.RequireControlTotal = new bool?(false);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        ((PXGraph) instance3).FieldVerifying.AddHandler<PX.Objects.AR.ARTran.projectID>(ProformaEntry.\u003C\u003Ec.\u003C\u003E9__251_2 ?? (ProformaEntry.\u003C\u003Ec.\u003C\u003E9__251_2 = new PXFieldVerifying((object) ProformaEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CReleaseDocument\u003Eb__251_2))));
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        ((PXGraph) instance3).FieldVerifying.AddHandler<PX.Objects.AR.ARInvoice.projectID>(ProformaEntry.\u003C\u003Ec.\u003C\u003E9__251_3 ?? (ProformaEntry.\u003C\u003Ec.\u003C\u003E9__251_3 = new PXFieldVerifying((object) ProformaEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CReleaseDocument\u003Eb__251_3))));
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        ((PXGraph) instance3).FieldVerifying.AddHandler<PX.Objects.AR.ARTran.taskID>(ProformaEntry.\u003C\u003Ec.\u003C\u003E9__251_4 ?? (ProformaEntry.\u003C\u003Ec.\u003C\u003E9__251_4 = new PXFieldVerifying((object) ProformaEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CReleaseDocument\u003Eb__251_4))));
        arInvoice1 = (PX.Objects.AR.ARInvoice) ((PXSelectBase) instance3.Document).Cache.CreateInstance();
        Decimal? nullable2 = doc.DocTotal;
        Decimal num1 = 0M;
        int mult = nullable2.GetValueOrDefault() >= num1 & nullable2.HasValue ? 1 : -1;
        arInvoice1.DocType = mult == 1 ? "INV" : "CRM";
        arInvoice1.DocDate = doc.InvoiceDate;
        arInvoice1.BranchID = doc.BranchID;
        arInvoice1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance3.Document).Insert(arInvoice1);
        arInvoice1.ProjectID = doc.ProjectID;
        arInvoice1.FinPeriodID = doc.FinPeriodID;
        arInvoice1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance3.Document).Update(arInvoice1);
        arInvoice1.CustomerID = doc.CustomerID;
        arInvoice1.DocDesc = doc.Description;
        PX.Objects.AR.ARInvoice arInvoice3 = arInvoice1;
        DateTime? nullable3;
        if (arInvoice1.DocType == "CRM")
        {
          nullable1 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.arSetup).Current.TermsInCreditMemos;
          if (!nullable1.GetValueOrDefault())
          {
            nullable3 = new DateTime?();
            goto label_24;
          }
        }
        nullable3 = doc.DueDate;
label_24:
        arInvoice3.DueDate = nullable3;
        PX.Objects.AR.ARInvoice arInvoice4 = arInvoice1;
        DateTime? nullable4;
        if (arInvoice1.DocType == "CRM")
        {
          nullable1 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.arSetup).Current.TermsInCreditMemos;
          if (!nullable1.GetValueOrDefault())
          {
            nullable4 = new DateTime?();
            goto label_28;
          }
        }
        nullable4 = doc.DiscDate;
label_28:
        arInvoice4.DiscDate = nullable4;
        PX.Objects.AR.ARInvoice arInvoice5 = arInvoice1;
        string str;
        if (arInvoice1.DocType == "CRM")
        {
          nullable1 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.arSetup).Current.TermsInCreditMemos;
          if (!nullable1.GetValueOrDefault())
          {
            str = (string) null;
            goto label_32;
          }
        }
        str = doc.TermsID;
label_32:
        arInvoice5.TermsID = str;
        arInvoice1.TaxZoneID = doc.TaxZoneID;
        arInvoice1.CuryID = doc.CuryID;
        arInvoice1.CuryInfoID = doc.CuryInfoID;
        arInvoice1.CustomerLocationID = doc.LocationID;
        arInvoice1.ProformaExists = new bool?(true);
        arInvoice1.InvoiceNbr = doc.InvoiceNbr;
        arInvoice1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance3.Document).Update(arInvoice1);
        nullable2 = doc.RetainageTotal;
        Decimal num2 = 0M;
        if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
          arInvoice1.RetainageApply = new bool?(true);
        if (((PXSelectBase<PMProject>) this.Project).Current.RetainageMode != "N")
          arInvoice1.PaymentsByLinesAllowed = new bool?(true);
        arInvoice1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance3.Document).Update(arInvoice1);
        arInvoice1.TaxCalcMode = "T";
        arInvoice1.ExternalTaxExemptionNumber = doc.ExternalTaxExemptionNumber;
        arInvoice1.AvalaraCustomerUsageType = doc.AvalaraCustomerUsageType;
        if (!string.IsNullOrEmpty(doc.FinPeriodID))
          ((PXSelectBase) instance3.Document).Cache.SetValueExt<PX.Objects.AR.ARInvoice.finPeriodID>((object) arInvoice1, (object) PeriodIDAttribute.FormatForDisplay(doc.FinPeriodID));
        ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) instance3.currencyinfo).Current = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) instance3.currencyinfo).Select(Array.Empty<object>()));
        PMAddress addressPM1 = PXResultset<PMAddress>.op_Implicit(PXSelectBase<PMAddress, PXSelect<PMAddress, Where<PMAddress.addressID, Equal<Required<PMProforma.billAddressID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) doc.BillAddressID
        }));
        if (addressPM1 != null)
        {
          nullable1 = addressPM1.IsDefaultAddress;
          if (!nullable1.GetValueOrDefault())
          {
            ARAddress arAddress = PXResultset<ARAddress>.op_Implicit(((PXSelectBase<ARAddress>) instance3.Billing_Address).Select(Array.Empty<object>()));
            ((PXSelectBase) instance3.Billing_Address).Cache.SetValueExt<ARAddress.overrideAddress>((object) arAddress, (object) true);
            this.CopyPMAddressToARInvoice(instance3, addressPM1, ((PXSelectBase<ARAddress>) instance3.Billing_Address).Current);
          }
        }
        PMShippingAddress addressPM2 = PXResultset<PMShippingAddress>.op_Implicit(PXSelectBase<PMShippingAddress, PXSelect<PMShippingAddress, Where<PMShippingAddress.addressID, Equal<Required<PMProforma.shipAddressID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) doc.ShipAddressID
        }));
        if (addressPM2 != null)
        {
          nullable1 = addressPM2.IsDefaultAddress;
          if (!nullable1.GetValueOrDefault())
          {
            ARShippingAddress arShippingAddress = PXResultset<ARShippingAddress>.op_Implicit(((PXSelectBase<ARShippingAddress>) instance3.Shipping_Address).Select(Array.Empty<object>()));
            ((PXSelectBase) instance3.Shipping_Address).Cache.SetValueExt<ARShippingAddress.overrideAddress>((object) arShippingAddress, (object) true);
            this.CopyPMAddressToARInvoice(instance3, (PMAddress) addressPM2, (ARAddress) ((PXSelectBase<ARShippingAddress>) instance3.Shipping_Address).Current);
          }
        }
        PMContact contactPM1 = PXResultset<PMContact>.op_Implicit(PXSelectBase<PMContact, PXSelect<PMContact, Where<PMContact.contactID, Equal<Required<PMProforma.billContactID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) doc.BillContactID
        }));
        if (contactPM1 != null)
        {
          nullable1 = contactPM1.IsDefaultContact;
          if (!nullable1.GetValueOrDefault())
          {
            ARContact arContact = PXResultset<ARContact>.op_Implicit(((PXSelectBase<ARContact>) instance3.Billing_Contact).Select(Array.Empty<object>()));
            ((PXSelectBase) instance3.Billing_Contact).Cache.SetValueExt<ARContact.overrideContact>((object) arContact, (object) true);
            this.CopyPMContactToARInvoice(instance3, contactPM1, ((PXSelectBase<ARContact>) instance3.Billing_Contact).Current);
          }
        }
        PMShippingContact contactPM2 = PXResultset<PMShippingContact>.op_Implicit(PXSelectBase<PMShippingContact, PXSelect<PMShippingContact, Where<PMShippingContact.contactID, Equal<Required<PMProforma.shipContactID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) doc.ShipContactID
        }));
        if (contactPM2 != null)
        {
          nullable1 = contactPM2.IsDefaultContact;
          if (!nullable1.GetValueOrDefault())
          {
            ARShippingContact arShippingContact = PXResultset<ARShippingContact>.op_Implicit(((PXSelectBase<ARShippingContact>) instance3.Shipping_Contact).Select(Array.Empty<object>()));
            ((PXSelectBase) instance3.Shipping_Contact).Cache.SetValueExt<ARShippingContact.overrideContact>((object) arShippingContact, (object) true);
            this.CopyPMContactToARInvoice(instance3, (PMContact) contactPM2, (ARContact) ((PXSelectBase<ARShippingContact>) instance3.Shipping_Contact).Current);
          }
        }
        if (string.IsNullOrEmpty(doc.TaxZoneID))
          TaxBaseAttribute.SetTaxCalc<PX.Objects.AR.ARTran.taxCategoryID>(((PXSelectBase) instance3.Transactions).Cache, (object) null, TaxCalc.NoCalc);
        else if (!this.RecalculateTaxesOnRelease())
          TaxBaseAttribute.SetTaxCalc<PX.Objects.AR.ARTran.taxCategoryID>(((PXSelectBase) instance3.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
        PX.Objects.AR.ARTran arTran1 = (PX.Objects.AR.ARTran) null;
        List<PMProformaProgressLine> proformaProgressLineList = new List<PMProformaProgressLine>();
        PXView view = ((PXSelectBase) this.ProgressiveLines).View;
        object[] objArray1 = (object[]) new PMProforma[1]
        {
          doc
        };
        object[] objArray2 = Array.Empty<object>();
        foreach (PXResult<PMProformaProgressLine, PMRevenueBudget> pxResult in view.SelectMultiBound(objArray1, objArray2))
        {
          PMProformaProgressLine line = PXResult<PMProformaProgressLine, PMRevenueBudget>.op_Implicit(pxResult);
          arTran1 = this.InsertTransaction(instance3, (PMProformaLine) line, mult);
          PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.ProgressiveLines).Cache, (object) line, ((PXSelectBase) instance3.Transactions).Cache, (object) arTran1, (PXNoteAttribute.IPXCopySettings) null);
          ((PXSelectBase) this.ProgressiveLines).Cache.SetValue<PMProformaLine.aRInvoiceLineNbr>((object) line, (object) arTran1.LineNbr);
          GraphHelper.MarkUpdated(((PXSelectBase) this.ProgressiveLines).Cache, (object) line, true);
          proformaProgressLineList.Add(line);
        }
        Dictionary<int, List<PMTran>> referencedTransactions = this.GetReferencedTransactions();
        List<PMProformaTransactLine> proformaTransactLineList = new List<PMProformaTransactLine>();
        foreach (PMProformaTransactLine line in GraphHelper.RowCast<PMProformaTransactLine>((IEnumerable) ((PXSelectBase) this.TransactionLines).View.SelectMultiBound((object[]) new PMProforma[1]
        {
          doc
        }, Array.Empty<object>())))
        {
          if (line.Option != "X")
          {
            PX.Objects.AR.ARTran arTran2 = this.InsertTransaction(instance3, (PMProformaLine) line, mult);
            if (arTran2 != null)
            {
              arTran1 = arTran2;
              PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.TransactionLines).Cache, (object) line, ((PXSelectBase) instance3.Transactions).Cache, (object) arTran1, (PXNoteAttribute.IPXCopySettings) null);
              ((PXSelectBase) this.TransactionLines).Cache.SetValue<PMProformaLine.aRInvoiceLineNbr>((object) line, (object) arTran1.LineNbr);
              GraphHelper.MarkUpdated(((PXSelectBase) this.TransactionLines).Cache, (object) line, true);
            }
            proformaTransactLineList.Add(line);
          }
          else
          {
            List<PMTran> pmTranList;
            if (referencedTransactions.TryGetValue(line.LineNbr.Value, out pmTranList))
            {
              foreach (PMTran tran in pmTranList)
              {
                if (string.IsNullOrEmpty(tran.AllocationID))
                {
                  RegisterReleaseProcess.SubtractFromUnbilledSummary((PXGraph) this, tran);
                  ((PXSelectBase<PMTran>) this.AllReferencedTransactions).Update(tran);
                }
              }
            }
          }
          List<PMTran> pmTranList1;
          if (referencedTransactions.TryGetValue(line.LineNbr.Value, out pmTranList1))
          {
            foreach (PMTran tran in pmTranList1)
            {
              if (tran != null && tran.Reverse == "B" && arInvoice2 == null)
              {
                foreach (PMTran transaction in (IEnumerable<PMTran>) instance2.ReverseTran(tran))
                {
                  transaction.Date = doc.InvoiceDate;
                  transaction.FinPeriodID = (string) null;
                  instance1.InsertTransactionWithManuallyChangedCurrencyInfo(transaction);
                }
              }
            }
          }
        }
        TaxBaseAttribute.SetTaxCalc<PX.Objects.AR.ARTran.taxCategoryID>(((PXSelectBase) instance3.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
        if (arTran1 != null)
          ((PXSelectBase) instance3.Transactions).Cache.RaiseRowUpdated((object) arTran1, (object) arTran1);
        if (!this.RecalculateTaxesOnRelease() && !this.IsExternalTax(((PXSelectBase<PMProforma>) this.Document).Current.TaxZoneID))
        {
          Dictionary<string, Decimal> dictionary = GraphHelper.RowCast<PMTax>((IEnumerable) ((PXSelectBase<PMTax>) this.Tax_Rows).Select(Array.Empty<object>())).GroupBy<PMTax, string>((Func<PMTax, string>) (x => x.TaxID)).ToDictionary<IGrouping<string, PMTax>, string, Decimal>((Func<IGrouping<string, PMTax>, string>) (x => x.Key), (Func<IGrouping<string, PMTax>, Decimal>) (x => x.Sum<PMTax>((Func<PMTax, Decimal>) (i => i.CuryRetainedTaxAmt.GetValueOrDefault()))));
          List<Tuple<ARTaxTran, PMTaxTran>> tupleList = new List<Tuple<ARTaxTran, PMTaxTran>>();
          foreach (PXResult<PMTaxTran> pxResult in ((PXSelectBase<PMTaxTran>) this.Taxes).Select(Array.Empty<object>()))
          {
            PMTaxTran pmTaxTran = PXResult<PMTaxTran>.op_Implicit(pxResult);
            ARTaxTran arTaxTran1 = new ARTaxTran();
            arTaxTran1.TaxID = pmTaxTran.TaxID;
            ARTaxTran arTaxTran2 = arTaxTran1;
            ARTaxTran arTaxTran3 = ((PXSelectBase<ARTaxTran>) instance3.Taxes).Insert(arTaxTran2);
            tupleList.Add(new Tuple<ARTaxTran, PMTaxTran>(arTaxTran3, pmTaxTran));
          }
          foreach (Tuple<ARTaxTran, PMTaxTran> tuple in tupleList)
          {
            if (tuple.Item1 != null)
            {
              tuple.Item1.TaxRate = tuple.Item2.TaxRate;
              ARTaxTran arTaxTran4 = tuple.Item1;
              Decimal num3 = (Decimal) mult;
              Decimal? nullable5 = tuple.Item2.CuryTaxableAmt;
              Decimal? nullable6 = nullable5.HasValue ? new Decimal?(num3 * nullable5.GetValueOrDefault()) : new Decimal?();
              arTaxTran4.CuryTaxableAmt = nullable6;
              ARTaxTran arTaxTran5 = tuple.Item1;
              Decimal num4 = (Decimal) mult;
              nullable5 = tuple.Item2.CuryExemptedAmt;
              Decimal? nullable7 = nullable5.HasValue ? new Decimal?(num4 * nullable5.GetValueOrDefault()) : new Decimal?();
              arTaxTran5.CuryExemptedAmt = nullable7;
              ARTaxTran arTaxTran6 = tuple.Item1;
              Decimal num5 = (Decimal) mult;
              nullable5 = tuple.Item2.CuryTaxAmt;
              Decimal? nullable8 = nullable5.HasValue ? new Decimal?(num5 * nullable5.GetValueOrDefault()) : new Decimal?();
              arTaxTran6.CuryTaxAmt = nullable8;
              ARTaxTran arTaxTran7 = tuple.Item1;
              Decimal num6 = (Decimal) mult;
              nullable5 = tuple.Item2.CuryRetainedTaxableAmt;
              Decimal valueOrDefault1 = nullable5.GetValueOrDefault();
              Decimal? nullable9 = new Decimal?(num6 * valueOrDefault1);
              arTaxTran7.CuryRetainedTaxableAmt = nullable9;
              ARTaxTran arTaxTran8 = tuple.Item1;
              Decimal num7 = (Decimal) mult;
              nullable5 = tuple.Item2.CuryRetainedTaxAmt;
              Decimal valueOrDefault2 = nullable5.GetValueOrDefault();
              Decimal? nullable10 = new Decimal?(num7 * valueOrDefault2);
              arTaxTran8.CuryRetainedTaxAmt = nullable10;
              Decimal num8;
              if (dictionary.TryGetValue(tuple.Item2.TaxID, out num8))
                tuple.Item1.CuryRetainedTaxAmtSumm = new Decimal?((Decimal) mult * num8);
              ((PXSelectBase<ARTaxTran>) instance3.Taxes).Update(tuple.Item1);
            }
          }
        }
        arInvoice1.Hold = new bool?(((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.HoldEntry.GetValueOrDefault());
        arInvoice1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance3.Document).Update(arInvoice1);
        ((PXAction) instance3.Save).Press();
        doc.ARInvoiceDocType = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance3.Document).Current.DocType;
        doc.ARInvoiceRefNbr = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance3.Document).Current.RefNbr;
        pmRegister.OrigNoteID = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance3.Document).Current.NoteID;
        doc.Released = new bool?(true);
        ((PXSelectBase<PMProforma>) this.Document).Update(doc);
        PMBillingRecord pmBillingRecord = PXResultset<PMBillingRecord>.op_Implicit(PXSelectBase<PMBillingRecord, PXSelect<PMBillingRecord, Where<PMBillingRecord.proformaRefNbr, Equal<Required<PMProforma.refNbr>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) doc.RefNbr
        }));
        if (pmBillingRecord != null)
        {
          pmBillingRecord.ARDocType = doc.ARInvoiceDocType;
          pmBillingRecord.ARRefNbr = doc.ARInvoiceRefNbr;
          ((PXSelectBase<PMBillingRecord>) this.BillingRecord).Update(pmBillingRecord);
        }
        foreach (PMProformaProgressLine proformaProgressLine in proformaProgressLineList)
        {
          ((PXSelectBase) this.ProgressiveLines).Cache.SetValue<PMProformaLine.aRInvoiceDocType>((object) proformaProgressLine, (object) doc.ARInvoiceDocType);
          ((PXSelectBase) this.ProgressiveLines).Cache.SetValue<PMProformaLine.aRInvoiceRefNbr>((object) proformaProgressLine, (object) doc.ARInvoiceRefNbr);
          ((PXSelectBase) this.ProgressiveLines).Cache.SetValue<PMProformaLine.released>((object) proformaProgressLine, (object) true);
          GraphHelper.MarkUpdated(((PXSelectBase) this.ProgressiveLines).Cache, (object) proformaProgressLine, true);
        }
        foreach (PMProformaTransactLine proformaTransactLine in proformaTransactLineList)
        {
          ((PXSelectBase) this.TransactionLines).Cache.SetValue<PMProformaLine.aRInvoiceDocType>((object) proformaTransactLine, (object) doc.ARInvoiceDocType);
          ((PXSelectBase) this.TransactionLines).Cache.SetValue<PMProformaLine.aRInvoiceRefNbr>((object) proformaTransactLine, (object) doc.ARInvoiceRefNbr);
          ((PXSelectBase) this.TransactionLines).Cache.SetValue<PMProformaLine.released>((object) proformaTransactLine, (object) true);
          GraphHelper.MarkUpdated(((PXSelectBase) this.TransactionLines).Cache, (object) proformaTransactLine, true);
          List<PMTran> pmTranList;
          if (referencedTransactions.TryGetValue(proformaTransactLine.LineNbr.Value, out pmTranList))
          {
            foreach (PMTran pmTran in pmTranList)
            {
              pmTran.ARTranType = proformaTransactLine.ARInvoiceDocType;
              pmTran.ARRefNbr = proformaTransactLine.ARInvoiceRefNbr;
              pmTran.RefLineNbr = proformaTransactLine.ARInvoiceLineNbr;
              ((PXSelectBase<PMTran>) this.AllReferencedTransactions).Update(pmTran);
            }
          }
        }
        if (((PXSelectBase) instance1.Transactions).Cache.IsDirty)
          ((PXAction) instance1.Save).Press();
        ((PXAction) this.Save).Press();
        transactionScope.Complete();
        arInvoice1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Invoices).Locate(arInvoice1) ?? arInvoice1;
      }
      bool? nullable11 = pmProject.AutomaticReleaseAR;
      if (nullable11.GetValueOrDefault())
      {
        nullable11 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.HoldEntry;
        if (!nullable11.GetValueOrDefault())
        {
          try
          {
            ARDocumentRelease.ReleaseDoc(new List<PX.Objects.AR.ARRegister>((IEnumerable<PX.Objects.AR.ARRegister>) new PX.Objects.AR.ARInvoice[1]
            {
              arInvoice1
            }), false);
          }
          catch (Exception ex)
          {
            throw new PXException("The system has failed to automatically release the AR document created on the release of a pro forma invoice. Try to release the AR document manually.", ex);
          }
        }
      }
    }
    PXResultset<PMProforma> pxResultset = PXSelectBase<PMProforma, PXViewOf<PMProforma>.BasedOn<SelectFromBase<PMProforma, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProforma.projectID, Equal<BqlField<PMProforma.projectID, IBqlInt>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProforma.refNbr, Greater<BqlField<PMProforma.refNbr, IBqlString>.FromCurrent>>>>>.And<BqlOperand<PMProforma.corrected, IBqlBool>.IsNotEqual<True>>>>.Order<PX.Data.BQL.Fluent.By<BqlField<PMProforma.refNbr, IBqlString>.Asc>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>());
    ProformaEntry instance = PXGraph.CreateInstance<ProformaEntry>();
    foreach (PXResult<PMProforma> pxResult1 in pxResultset)
    {
      PMProforma pmProforma = PXResult<PMProforma>.op_Implicit(pxResult1);
      ((PXGraph) instance).Clear();
      ((PXSelectBase<PMProforma>) instance.Document).Current = pmProforma;
      ((PXSelectBase) instance.Document).Cache.Update((object) ((PXSelectBase<PMProforma>) instance.Document).Current);
      foreach (PXResult<PMProformaProgressLine> pxResult2 in ((PXSelectBase<PMProformaProgressLine>) instance.ProgressiveLines).Select(Array.Empty<object>()))
      {
        PMProformaProgressLine line = PXResult<PMProformaProgressLine>.op_Implicit(pxResult2);
        instance.CalculateProgressLinePreviouslyInvoicedValues(line);
        ((PXSelectBase) instance.ProgressiveLines).Cache.Update((object) line);
      }
      ((PXAction) instance.Save).Press();
    }
  }

  protected virtual void ProcessReversingCreditMemo()
  {
    PMProformaRevision lastRevision = this.GetLastRevision();
    if (lastRevision == null)
      return;
    PX.Objects.AR.ARInvoice invoice = PX.Objects.AR.ARInvoice.PK.Find((PXGraph) this, lastRevision.ARInvoiceDocType, lastRevision.ARInvoiceRefNbr);
    PX.Objects.AR.ARInvoice creditMemo = PX.Objects.AR.ARInvoice.PK.Find((PXGraph) this, lastRevision.ReversedARInvoiceDocType, lastRevision.ReversedARInvoiceRefNbr);
    if (invoice == null || !invoice.PaymentsByLinesAllowed.GetValueOrDefault() || creditMemo == null)
      return;
    Decimal? applicationBalance = invoice.ApplicationBalance;
    Decimal num = 0M;
    if (applicationBalance.GetValueOrDefault() == num & applicationBalance.HasValue)
      return;
    this.CreatePayment(invoice, creditMemo, lastRevision.RefNbr);
  }

  private void CreatePayment(PX.Objects.AR.ARInvoice invoice, PX.Objects.AR.ARInvoice creditMemo, string proFormaRefNumber)
  {
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    instance.CreatePayment(invoice, "PMT");
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current.CuryOrigDocAmt = new Decimal?(0M);
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current.Hold = new bool?(false);
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current.DontApprove = new bool?(true);
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current.AdjFinPeriodID = creditMemo.FinPeriodID;
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).UpdateCurrent();
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current.DocDesc = PXMessages.LocalizeFormatNoPrefix("The reversal originating from corrections of the {0} pro forma invoice", new object[1]
    {
      (object) proFormaRefNumber
    });
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current.ExtRefNbr = "PFRev";
    PXResultset<PX.Objects.AR.ARTran> pxResultset = PXSelectBase<PX.Objects.AR.ARTran, PXViewOf<PX.Objects.AR.ARTran>.BasedOn<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.tranType, Equal<P.AsString>>>>>.And<BqlOperand<PX.Objects.AR.ARTran.refNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) creditMemo.DocType,
      (object) creditMemo.RefNbr
    });
    Dictionary<int, Decimal> dictionary = new Dictionary<int, Decimal>();
    foreach (PXResult<PX.Objects.AR.ARTran> pxResult in pxResultset)
    {
      PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran>.op_Implicit(pxResult);
      ARAdjust arAdjust = new ARAdjust()
      {
        AdjdDocType = creditMemo.DocType,
        AdjdRefNbr = creditMemo.RefNbr,
        AdjdLineNbr = arTran.LineNbr
      };
      int valueOrDefault = ((PXSelectBase<ARAdjust>) instance.Adjustments).Update(arAdjust).AdjdLineNbr.GetValueOrDefault();
      if (valueOrDefault > 0)
        dictionary[valueOrDefault] = arTran.CuryOrigTranAmt.GetValueOrDefault();
    }
    foreach (PXResult<ARAdjust> pxResult in ((PXSelectBase<ARAdjust>) instance.Adjustments).Select(Array.Empty<object>()))
    {
      ARAdjust arAdjust = PXResult<ARAdjust>.op_Implicit(pxResult);
      Decimal num;
      if (dictionary.TryGetValue(arAdjust.AdjdLineNbr.GetValueOrDefault(), out num))
      {
        ((PXSelectBase) instance.Adjustments).Cache.SetValueExt<ARAdjust.curyAdjgPPDAmt>((object) arAdjust, (object) 0M);
        ((PXSelectBase) instance.Adjustments).Cache.SetValueExt<ARAdjust.curyAdjgAmt>((object) arAdjust, (object) num);
      }
    }
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current.CuryApplAmt = new Decimal?(0M);
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current.CuryUnappliedBal = new Decimal?(0M);
    ((PXGraph) instance).Actions.PressSave();
    ((PXAction) instance.release).Press();
  }

  protected virtual PMProformaRevision GetLastRevision()
  {
    return new List<PMProformaRevision>(GraphHelper.RowCast<PMProformaRevision>((IEnumerable) ((PXSelectBase<PMProformaRevision>) this.Revisions).Select(Array.Empty<object>()))).LastOrDefault<PMProformaRevision>();
  }

  public virtual PX.Objects.AR.ARInvoice ProcessRevision()
  {
    PMProformaRevision lastRevision = this.GetLastRevision();
    if (lastRevision == null)
      return (PX.Objects.AR.ARInvoice) null;
    PX.Objects.AR.ARInvoice origDoc = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) lastRevision.ARInvoiceDocType,
      (object) lastRevision.ARInvoiceRefNbr
    }));
    if (this.IsReversingDocumentAlreadyExists(origDoc))
      return (PX.Objects.AR.ARInvoice) null;
    if (!origDoc.Released.GetValueOrDefault())
      throw new PXException("To correct the pro forma invoice, delete the associated {0} AR document first.", new object[1]
      {
        (object) lastRevision.ARInvoiceRefNbr
      });
    ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) lastRevision.ARInvoiceRefNbr, new object[1]
    {
      (object) lastRevision.ARInvoiceDocType
    });
    ProcessingResult period = this.FinPeriodUtils.CanPostToPeriod((IFinPeriod) this.FinPeriodRepository.GetByID(((PXSelectBase<PMProforma>) this.Document).Current.FinPeriodID, PXAccess.GetParentOrganizationID(((PXSelectBase<PMProforma>) this.Document).Current.BranchID)), typeof (FinPeriod.aRClosed));
    if (period.HasWarningOrError && period.MaxErrorLevel >= 4)
    {
      OpenPeriodAttribute.SetValidatePeriod<PX.Objects.AR.ARInvoice.finPeriodID>(((PXSelectBase) instance.Document).Cache, (object) null, PeriodValidation.Nothing);
      PXCacheEx.Adjust<AROpenPeriodAttribute>(((PXSelectBase) instance.Document).Cache, (object) null).For<PX.Objects.AR.ARInvoice.finPeriodID>((Action<AROpenPeriodAttribute>) (attr =>
      {
        attr.ValidatePeriod = PeriodValidation.Nothing;
        attr.RedefaultOnDateChanged = false;
      }));
      this.ReverseCurrentDocument(instance);
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current.DocDate = ((PXSelectBase<PMProforma>) this.Document).Current.InvoiceDate;
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current.FinPeriodID = ((PXSelectBase<PMProforma>) this.Document).Current.FinPeriodID;
    }
    else
    {
      this.ReverseCurrentDocument(instance);
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current.DocDesc = PXMessages.LocalizeFormatNoPrefix("The reversal originating from corrections of the {0} pro forma invoice", new object[1]
      {
        (object) ((PXSelectBase<PMProforma>) this.Document).Current.RefNbr
      });
    }
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current.DontApprove = new bool?(true);
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current.DontPrint = new bool?(true);
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current.DontEmail = new bool?(true);
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current.Hold = new bool?(false);
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current.ProformaExists = new bool?(true);
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).UpdateCurrent();
    ((PXAction) instance.Save).Press();
    PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current;
    PXContext.SetSlot<bool>(nameof (ProcessRevision), true);
    instance.ReleaseProcess(new List<PX.Objects.AR.ARRegister>((IEnumerable<PX.Objects.AR.ARRegister>) new PX.Objects.AR.ARInvoice[1]
    {
      current
    }));
    PXContext.ClearSlot(nameof (ProcessRevision));
    lastRevision.ReversedARInvoiceDocType = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current.DocType;
    lastRevision.ReversedARInvoiceRefNbr = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current.RefNbr;
    ((PXSelectBase<PMProformaRevision>) this.Revisions).Update(lastRevision);
    return ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current;
  }

  private void ReverseCurrentDocument(ARInvoiceEntry invoiceEntry)
  {
    ReverseInvoiceArgs reverseArgs = new ReverseInvoiceArgs();
    reverseArgs.DateOption = ReverseInvoiceArgs.CopyOption.Override;
    reverseArgs.DocumentDate = ((PXSelectBase<PMProforma>) this.Document).Current.InvoiceDate;
    reverseArgs.DocumentFinPeriodID = ((PXSelectBase<PMProforma>) this.Document).Current.FinPeriodID;
    PXAdapter adapter = new PXAdapter((PXView) new PXView.Dummy((PXGraph) invoiceEntry, ((PXSelectBase) invoiceEntry.Document).View.BqlSelect, new List<object>()
    {
      (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) invoiceEntry.Document).Current
    }));
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) invoiceEntry.Document).Current.PaymentsByLinesAllowed.GetValueOrDefault())
    {
      reverseArgs.ApplyToOriginalDocument = false;
      invoiceEntry.ReverseDocumentAndApplyToReversalIfNeeded(adapter, reverseArgs);
    }
    else
    {
      reverseArgs.ApplyToOriginalDocument = true;
      invoiceEntry.ReverseDocumentAndApplyToReversalIfNeeded(adapter, reverseArgs);
    }
  }

  protected virtual bool IsReversingDocumentAlreadyExists(PX.Objects.AR.ARInvoice origDoc)
  {
    return this.GetReversingDocument(origDoc.DocType, origDoc.RefNbr) != null;
  }

  public virtual PX.Objects.AR.ARRegister GetReversingDocument(
    string originalDocType,
    string originalRefNbr)
  {
    return PXResultset<PX.Objects.AR.ARRegister>.op_Implicit(PXSelectBase<PX.Objects.AR.ARRegister, PXSelect<PX.Objects.AR.ARRegister, Where<PX.Objects.AR.ARRegister.docType, Equal<Required<PX.Objects.AR.ARRegister.docType>>, And<PX.Objects.AR.ARRegister.origDocType, Equal<Required<PX.Objects.AR.ARRegister.origDocType>>, And<PX.Objects.AR.ARRegister.origRefNbr, Equal<Required<PX.Objects.AR.ARRegister.origRefNbr>>, And<PX.Objects.AR.ARRegister.isRetainageDocument, NotEqual<True>>>>>, OrderBy<Desc<PX.Objects.AR.ARRegister.createdDateTime>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[3]
    {
      (object) this.GetReversingDocType(originalDocType),
      (object) originalDocType,
      (object) originalRefNbr
    }));
  }

  public virtual string GetReversingDocType(string docType)
  {
    switch (docType)
    {
      case "INV":
      case "DRM":
        docType = "CRM";
        break;
      case "CRM":
        docType = "DRM";
        break;
    }
    return docType;
  }

  protected virtual bool PrePersist()
  {
    this.ValidateProgressLineProjectKeys();
    if (!this.IsBilling)
    {
      if (!this.IsMigrationMode())
      {
        PMProforma current = ((PXSelectBase<PMProforma>) this.Document).Current;
        if ((current != null ? (!current.IsMigratedRecord.GetValueOrDefault() ? 1 : 0) : 1) != 0)
        {
          this.CreateBillingRecordIfNeeded();
          this.CreateProjectNumber();
        }
      }
      if (!((PXGraph) this).IsImportFromExcel)
        this.RemoveEmptyRevenueBudgetChanges();
      foreach (PMProformaProgressLine line in ((PXSelectBase) this.ProgressiveLines).Cache.Inserted)
        this.ClearValuesToInvoice(line);
    }
    return ((PXGraph) this).PrePersist();
  }

  protected virtual void CreateProjectNumber()
  {
    if (((PXSelectBase<PMProforma>) this.Document).Current == null || ((PXSelectBase<PMProject>) this.Project).Current == null || !string.IsNullOrWhiteSpace(((PXSelectBase<PMProforma>) this.Document).Current.ProjectNbr) || !PXAccess.FeatureInstalled<FeaturesSet.construction>() || !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.ProgressiveLines).Cache.Inserted))
      return;
    string str = PMBillEngine.IncLastProformaNumber(((PXSelectBase<PMProject>) this.Project).Current.LastProformaNumber);
    if (str.Length > 15)
      throw new PXSetPropertyException<PMProforma.projectNbr>("The pro forma invoice cannot be created because the automatically generated Application Nbr. exceeds the length limit ({0} symbols). Correct the Last Application Nbr. of the {1} project, and run project billing again.", new object[2]
      {
        (object) 15,
        (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD
      });
    try
    {
      ((PXSelectBase<PMProforma>) this.Document).Current.ProjectNbr = str;
      ((PXSelectBase<PMProforma>) this.Document).UpdateCurrent();
    }
    catch (PXSetPropertyException ex)
    {
      object[] objArray = Array.Empty<object>();
      throw new PXException((Exception) ex, "The pro forma invoice has not been generated as the application number is not valid. Change the Last Application Nbr. of the project.", objArray);
    }
    ((PXSelectBase<PMProject>) this.Project).Current.LastProformaNumber = str;
    ((PXSelectBase<PMProject>) this.Project).Update(((PXSelectBase<PMProject>) this.Project).Current);
  }

  protected virtual void CreateBillingRecordIfNeeded()
  {
    if (((PXSelectBase<PMProforma>) this.Document).Current == null || ((PXSelectBase<PMProject>) this.Project).Current == null || NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.BillingRecord).Cache.Inserted))
      return;
    bool flag = ((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<PMProforma>) this.Document).Current) == 2 && ((PXSelectBase<PMProforma>) this.Document).Current.RevisionID.GetValueOrDefault() < 2;
    if (!flag)
    {
      if (((IQueryable<PXResult<PMBillingRecord>>) PXSelectBase<PMBillingRecord, PXViewOf<PMBillingRecord>.BasedOn<SelectFromBase<PMBillingRecord, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMBillingRecord.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<PMBillingRecord.proformaRefNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) ((PXSelectBase<PMProject>) this.Project).Current.ProjectID,
        (object) ((PXSelectBase<PMProforma>) this.Document).Current.RefNbr
      })).Any<PXResult<PMBillingRecord>>())
        return;
    }
    ((PXSelectBase<PMProject>) this.Project).Current.BillingLineCntr = new int?(((PXSelectBase<PMProject>) this.Project).Current.BillingLineCntr.GetValueOrDefault() + 1);
    ((PXSelectBase<PMProject>) this.Project).Update(((PXSelectBase<PMProject>) this.Project).Current);
    PMBillingRecord instance = (PMBillingRecord) ((PXSelectBase) this.BillingRecord).Cache.CreateInstance();
    instance.ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ContractID;
    instance.RecordID = ((PXSelectBase<PMProject>) this.Project).Current.BillingLineCntr;
    instance.BillingTag = PMBillEngine.GetProformaBillingTag("P");
    instance.ProformaRefNbr = !flag ? ((PXSelectBase<PMProforma>) this.Document).Current.RefNbr : (string) null;
    instance.Date = new DateTime?(((PXGraph) this).Accessinfo.BusinessDate ?? DateTime.Now);
    ((PXSelectBase<PMBillingRecord>) this.BillingRecord).Insert(instance);
  }

  protected virtual void RemoveBillingRecords(int? projectId)
  {
    if (!projectId.HasValue)
      return;
    foreach (PMBillingRecord pmBillingRecord in ((PXSelectBase) this.BillingRecord).Cache.Inserted)
      ((PXSelectBase<PMBillingRecord>) this.BillingRecord).Delete(pmBillingRecord);
    if ((((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<PMProforma>) this.Document).Current) != 2 ? 0 : (((PXSelectBase<PMProforma>) this.Document).Current.RevisionID.GetValueOrDefault() < 2 ? 1 : 0)) != 0)
      return;
    foreach (PXResult<PMBillingRecord> pxResult in PXSelectBase<PMBillingRecord, PXViewOf<PMBillingRecord>.BasedOn<SelectFromBase<PMBillingRecord, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMBillingRecord.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<PMBillingRecord.proformaRefNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) projectId,
      (object) ((PXSelectBase<PMProforma>) this.Document).Current.RefNbr
    }))
      ((PXSelectBase<PMBillingRecord>) this.BillingRecord).Delete(PXResult<PMBillingRecord>.op_Implicit(pxResult));
  }

  protected virtual void RemoveEmptyRevenueBudgetChanges()
  {
    if (!NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.Budget).Cache.Inserted))
      return;
    HashSet<string> hashSet1 = GraphHelper.RowCast<PMProformaProgressLine>((IEnumerable) ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Select(Array.Empty<object>())).Select<PMProformaProgressLine, string>(new Func<PMProformaProgressLine, string>(ProformaEntry.GetProformaLineKey)).Concat<string>(GraphHelper.RowCast<PMProformaTransactLine>((IEnumerable) ((PXSelectBase<PMProformaTransactLine>) this.TransactionLines).Select(Array.Empty<object>())).Select<PMProformaTransactLine, string>(new Func<PMProformaTransactLine, string>(ProformaEntry.GetProformaLineKey))).Distinct<string>().ToHashSet<string>();
    HashSet<string> hashSet2 = GraphHelper.RowCast<PMRevenueBudget>((IEnumerable) PXSelectBase<PMRevenueBudget, PXViewOf<PMRevenueBudget>.BasedOn<SelectFromBase<PMRevenueBudget, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMRevenueBudget.projectID, Equal<BqlField<PMProject.contractID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PMRevenueBudget.type, IBqlString>.IsEqual<AccountType.income>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>())).Select<PMRevenueBudget, string>(new Func<PMRevenueBudget, string>(ProformaEntry.GetBudgetLineKey)).Distinct<string>().ToHashSet<string>();
    foreach (KeyValuePair<string, (Decimal, PMBudgetAccum[])> keyValuePair in GraphHelper.RowCast<PMBudgetAccum>(((PXSelectBase) this.Budget).Cache.Inserted).Where<PMBudgetAccum>((Func<PMBudgetAccum, bool>) (x => x.Type == "I")).GroupBy<PMBudgetAccum, string>(new Func<PMBudgetAccum, string>(ProformaEntry.GetBudgetLineKey)).ToDictionary<IGrouping<string, PMBudgetAccum>, string, (Decimal, PMBudgetAccum[])>((Func<IGrouping<string, PMBudgetAccum>, string>) (x => x.Key), (Func<IGrouping<string, PMBudgetAccum>, (Decimal, PMBudgetAccum[])>) (x => (x.Sum<PMBudgetAccum>((Func<PMBudgetAccum, Decimal>) (i => i.CuryInvoicedAmount.GetValueOrDefault())), x.ToArray<PMBudgetAccum>()))))
    {
      if (!hashSet1.Contains(keyValuePair.Key) && !hashSet2.Contains(keyValuePair.Key) && keyValuePair.Value.Item1 == 0M)
      {
        foreach (object obj in keyValuePair.Value.Item2)
          ((PXSelectBase) this.Budget).Cache.Remove(obj);
      }
    }
  }

  protected void ValidateProgressLineProjectKeys()
  {
    Dictionary<string, PMProformaProgressLine> existingLines = new Dictionary<string, PMProformaProgressLine>();
    List<PXException> source = new List<PXException>();
    foreach (PXResult<PMProformaProgressLine> pxResult in ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Select(Array.Empty<object>()))
    {
      PMProformaProgressLine line = PXResult<PMProformaProgressLine>.op_Implicit(pxResult);
      PXException pxException = this.ValidateProgressLineProjectKey(((PXSelectBase) this.ProgressiveLines).Cache, (IDictionary<string, PMProformaProgressLine>) existingLines, line);
      if (pxException != null)
        source.Add(pxException);
    }
    if (source.Any<PXException>())
      throw source.First<PXException>();
  }

  private PXException ValidateProgressLineProjectKey(
    PXCache cache,
    IDictionary<string, PMProformaProgressLine> existingLines,
    PMProformaProgressLine line)
  {
    if (!line.TaskID.HasValue || !line.AccountGroupID.HasValue)
      return (PXException) null;
    string proformaLineKey = ProformaEntry.GetProformaLineKey((PMProformaLine) line);
    if (!existingLines.ContainsKey(proformaLineKey))
    {
      existingLines[proformaLineKey] = line;
      return (PXException) null;
    }
    PMTask pmTask = PMTask.PK.Find((PXGraph) this, line.ProjectID, line.TaskID);
    PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) line, "A line with the same project budget key ({0}, {1}, {2}, {3}) already exists.", (PXErrorLevel) 5, new object[4]
    {
      (object) pmTask?.TaskCD,
      (object) PMAccountGroup.PK.Find((PXGraph) this, line.AccountGroupID)?.GroupCD,
      (object) PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, line.InventoryID)?.InventoryCD,
      (object) PMCostCode.PK.Find((PXGraph) this, line.CostCodeID)?.CostCodeCD
    });
    cache.RaiseExceptionHandling<PMProformaProgressLine.taskID>((object) line, (object) pmTask?.TaskCD, (Exception) propertyException);
    PXTrace.WriteError((Exception) propertyException);
    return (PXException) propertyException;
  }

  public virtual void Persist()
  {
    BranchBaseAttribute.VerifyFieldInPXCache<PMProformaProgressLine, PMProformaProgressLine.branchID>((PXGraph) this, ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Select(Array.Empty<object>()));
    BranchBaseAttribute.VerifyFieldInPXCache<PMProformaTransactLine, PMProformaTransactLine.branchID>((PXGraph) this, ((PXSelectBase<PMProformaTransactLine>) this.TransactionLines).Select(Array.Empty<object>()));
    PMProforma deletedDoc = (PMProforma) null;
    foreach (PMProforma pmProforma in ((PXSelectBase) this.Document).Cache.Deleted)
      deletedDoc = pmProforma;
    this.RollbackRevision(deletedDoc);
    this.RemoveObsoleteLines();
    ((PXGraph) this).Persist();
  }

  protected virtual void RollbackRevision(PMProforma deletedDoc)
  {
    if (deletedDoc == null)
      return;
    PMProforma pmProforma = PXResultset<PMProforma>.op_Implicit(PXSelectBase<PMProforma, PXSelect<PMProforma, Where<PMProforma.refNbr, Equal<Required<PMProforma.refNbr>>, And<PMProforma.corrected, Equal<True>>>, OrderBy<Desc<PMProforma.revisionID>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) deletedDoc.RefNbr
    }));
    if (pmProforma == null)
      return;
    ((PXSelectBase) this.Document).Cache.SetValue<PMProforma.corrected>((object) pmProforma, (object) false);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Document).Cache, (object) pmProforma, true);
    PXView view = ((PXSelectBase) this.ProgressiveLines).View;
    object[] objArray1 = (object[]) new PMProforma[1]
    {
      pmProforma
    };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<PMProformaProgressLine, PMRevenueBudget> pxResult in view.SelectMultiBound(objArray1, objArray2))
    {
      PMProformaProgressLine proformaProgressLine = PXResult<PMProformaProgressLine, PMRevenueBudget>.op_Implicit(pxResult);
      ((PXSelectBase) this.ProgressiveLines).Cache.SetValue<PMProformaLine.corrected>((object) proformaProgressLine, (object) false);
      GraphHelper.MarkUpdated(((PXSelectBase) this.ProgressiveLines).Cache, (object) proformaProgressLine, true);
    }
    foreach (PMProformaTransactLine proformaTransactLine in GraphHelper.RowCast<PMProformaTransactLine>((IEnumerable) ((PXSelectBase) this.TransactionLines).View.SelectMultiBound((object[]) new PMProforma[1]
    {
      pmProforma
    }, Array.Empty<object>())))
    {
      ((PXSelectBase) this.TransactionLines).Cache.SetValue<PMProformaLine.corrected>((object) proformaTransactLine, (object) false);
      GraphHelper.MarkUpdated(((PXSelectBase) this.TransactionLines).Cache, (object) proformaTransactLine, true);
    }
  }

  protected virtual void CopyPMAddressToARInvoice(
    ARInvoiceEntry invoiceEntry,
    PMAddress addressPM,
    ARAddress addressAR)
  {
    addressAR.BAccountAddressID = addressPM.BAccountAddressID;
    addressAR.BAccountID = addressPM.BAccountID;
    addressAR.RevisionID = addressPM.RevisionID;
    addressAR.IsDefaultAddress = addressPM.IsDefaultAddress;
    addressAR.AddressLine1 = addressPM.AddressLine1;
    addressAR.AddressLine2 = addressPM.AddressLine2;
    addressAR.AddressLine3 = addressPM.AddressLine3;
    addressAR.City = addressPM.City;
    addressAR.State = addressPM.State;
    addressAR.PostalCode = addressPM.PostalCode;
    addressAR.CountryID = addressPM.CountryID;
    addressAR.IsValidated = addressPM.IsValidated;
    addressAR.Department = addressPM.Department;
    addressAR.SubDepartment = addressPM.SubDepartment;
    addressAR.StreetName = addressPM.StreetName;
    addressAR.BuildingNumber = addressPM.BuildingNumber;
    addressAR.BuildingName = addressPM.BuildingName;
    addressAR.Floor = addressPM.Floor;
    addressAR.UnitNumber = addressPM.UnitNumber;
    addressAR.PostBox = addressPM.PostBox;
    addressAR.Room = addressPM.Room;
    addressAR.TownLocationName = addressPM.TownLocationName;
    addressAR.DistrictName = addressPM.DistrictName;
  }

  protected virtual void CopyPMContactToARInvoice(
    ARInvoiceEntry invoiceEntry,
    PMContact contactPM,
    ARContact contactAR)
  {
    contactAR.BAccountContactID = contactPM.BAccountContactID;
    contactAR.BAccountID = contactPM.BAccountID;
    contactAR.RevisionID = contactPM.RevisionID;
    contactAR.IsDefaultContact = contactPM.IsDefaultContact;
    contactAR.FullName = contactPM.FullName;
    contactAR.Attention = contactPM.Attention;
    contactAR.Salutation = contactPM.Salutation;
    contactAR.Title = contactPM.Title;
    contactAR.Phone1 = contactPM.Phone1;
    contactAR.Phone1Type = contactPM.Phone1Type;
    contactAR.Phone2 = contactPM.Phone2;
    contactAR.Phone2Type = contactPM.Phone2Type;
    contactAR.Phone3 = contactPM.Phone3;
    contactAR.Phone3Type = contactPM.Phone3Type;
    contactAR.Fax = contactPM.Fax;
    contactAR.FaxType = contactPM.FaxType;
    contactAR.Email = contactPM.Email;
  }

  protected virtual void CopyProjectBillingInformation(int? projectID, PMProforma proforma)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, projectID);
    if (pmProject == null)
      return;
    PMAddress source1 = PMBillingAddress.PK.Find((PXGraph) this, pmProject.BillAddressID);
    if (source1 != null)
    {
      PMAddress dest = new PMAddress();
      AddressAttribute.Copy((IAddress) dest, (IAddress) source1);
      ((PXSelectBase) this.Billing_Address).Cache.Clear();
      PMAddress pmAddress = ((PXSelectBase<PMAddress>) this.Billing_Address).Insert(dest);
      ((PXSelectBase) this.Document).Cache.SetValueExt<PMProforma.billAddressID>((object) proforma, (object) pmAddress.AddressID);
    }
    PMContact source2 = PMBillingContact.PK.Find((PXGraph) this, pmProject.BillContactID);
    if (source2 == null)
      return;
    PMContact dest1 = new PMContact();
    ContactAttribute.CopyContact((IContact) dest1, (IContact) source2);
    ((PXSelectBase) this.Billing_Contact).Cache.Clear();
    PMContact pmContact = ((PXSelectBase<PMContact>) this.Billing_Contact).Insert(dest1);
    ((PXSelectBase) this.Document).Cache.SetValueExt<PMProforma.billContactID>((object) proforma, (object) pmContact.ContactID);
  }

  public virtual PX.Objects.AR.ARTran InsertTransaction(
    ARInvoiceEntry invoiceEntry,
    PMProformaLine line,
    int mult)
  {
    if (line.Merged.GetValueOrDefault() && line.Option != "U" && line.Option != "X")
      return (PX.Objects.AR.ARTran) null;
    PX.Objects.AR.ARTran arTran1 = new PX.Objects.AR.ARTran();
    PX.Objects.AR.ARTran arTran2 = arTran1;
    int? inventoryId = line.InventoryID;
    int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
    int? nullable1 = inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue ? new int?() : line.InventoryID;
    arTran2.InventoryID = nullable1;
    arTran1.BranchID = line.BranchID;
    arTran1.TranDesc = line.Description;
    arTran1.ProjectID = line.ProjectID;
    arTran1.TaskID = line.TaskID;
    arTran1.CostCodeID = line.CostCodeID;
    arTran1.ExpenseDate = line.Date;
    arTran1.AccountID = line.AccountID;
    arTran1.SubID = line.SubID;
    arTran1.TaxCategoryID = line.TaxCategoryID;
    arTran1.UOM = line.UOM;
    PX.Objects.AR.ARTran arTran3 = arTran1;
    Decimal? nullable2 = line.Qty;
    Decimal num1 = (Decimal) mult;
    Decimal? nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num1) : new Decimal?();
    arTran3.Qty = nullable3;
    arTran1.CuryUnitPrice = line.CuryUnitPrice;
    PX.Objects.AR.ARTran arTran4 = arTran1;
    nullable2 = line.CuryLineTotal;
    Decimal num2 = (Decimal) mult;
    Decimal? nullable4 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num2) : new Decimal?();
    arTran4.CuryExtPrice = nullable4;
    arTran1.FreezeManualDisc = new bool?(true);
    arTran1.ManualPrice = new bool?(true);
    arTran1.PMDeltaOption = line.Option == "U" ? line.Option : "C";
    arTran1.DeferredCode = line.DefCode;
    arTran1.RetainagePct = line.RetainagePct;
    PX.Objects.AR.ARTran arTran5 = arTran1;
    nullable2 = line.Retainage;
    Decimal num3 = (Decimal) mult;
    Decimal? nullable5 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num3) : new Decimal?();
    arTran5.RetainageAmt = nullable5;
    PX.Objects.AR.ARTran arTran6 = arTran1;
    nullable2 = line.CuryRetainage;
    Decimal num4 = (Decimal) mult;
    Decimal? nullable6 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num4) : new Decimal?();
    arTran6.CuryRetainageAmt = nullable6;
    PX.Objects.AR.ARTran arTran7 = ((PXSelectBase<PX.Objects.AR.ARTran>) invoiceEntry.Transactions).Insert(arTran1);
    bool flag = false;
    if (line.TaxCategoryID != arTran7.TaxCategoryID)
    {
      arTran7.TaxCategoryID = line.TaxCategoryID;
      flag = true;
    }
    nullable2 = line.CuryLineTotal;
    Decimal num5 = 0M;
    if (nullable2.GetValueOrDefault() == num5 & nullable2.HasValue)
    {
      arTran7.CuryExtPrice = new Decimal?(0M);
      flag = true;
    }
    if (flag)
      arTran7 = ((PXSelectBase<PX.Objects.AR.ARTran>) invoiceEntry.Transactions).Update(arTran7);
    return arTran7;
  }

  protected virtual void AddToInvoiced(PMProformaLine line)
  {
    int? revenueAccountGroup = line.AccountGroupID;
    if (line.Type == "T")
      revenueAccountGroup = this.GetProjectedAccountGroup(line);
    this.AddToInvoiced(line, revenueAccountGroup);
  }

  protected virtual void SubtractFromInvoiced(PMProformaLine line)
  {
    Decimal? lineTotal = line.LineTotal;
    Decimal num1 = 0M;
    if (lineTotal.GetValueOrDefault() == num1 & lineTotal.HasValue)
    {
      Decimal? qty = line.Qty;
      Decimal num2 = 0M;
      if (qty.GetValueOrDefault() == num2 & qty.HasValue)
        return;
    }
    int? revenueAccountGroup = line.AccountGroupID;
    if (line.Type == "T")
      revenueAccountGroup = this.GetProjectedAccountGroup(line);
    this.AddToInvoiced(line, revenueAccountGroup, -1);
  }

  protected virtual void SubtractFromInvoiced(PMProformaLine line, int? revenueAccountGroup)
  {
    this.AddToInvoiced(line, revenueAccountGroup, -1);
  }

  private Decimal GetBaseValueForBudget(PMProject project, Decimal curyValue)
  {
    return project.CuryID == project.BaseCuryID ? curyValue : ((PXGraph) this).GetExtension<ProformaEntry.MultiCurrency>().GetCurrencyInfo(project.CuryInfoID).CuryConvBase(curyValue);
  }

  protected virtual void AddToInvoiced(PMProformaLine line, int? revenueAccountGroup, int mult = 1)
  {
    if (!revenueAccountGroup.HasValue || !line.ProjectID.HasValue || !line.TaskID.HasValue || !(line.CuryLineTotal.GetValueOrDefault() != 0M) && !(line.Qty.GetValueOrDefault() != 0M) || !this.CanCreateRevenueBudgetLine(line))
      return;
    Decimal curyValue = (Decimal) mult * this.GetAmountInProjectCurrency(line.CuryLineTotal);
    PMBudgetAccum pmBudgetAccum1 = ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(this.GetTargetBudget(revenueAccountGroup, line));
    Decimal result;
    INUnitAttribute.TryConvertGlobalUnits((PXGraph) this, line.UOM, pmBudgetAccum1.UOM, line.Qty.GetValueOrDefault(), INPrecision.QUANTITY, out result);
    PMProject project = PMProject.PK.Find((PXGraph) this, line.ProjectID);
    PMBudgetAccum pmBudgetAccum2 = pmBudgetAccum1;
    Decimal? nullable = pmBudgetAccum2.CuryInvoicedAmount;
    Decimal num1 = curyValue;
    pmBudgetAccum2.CuryInvoicedAmount = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num1) : new Decimal?();
    PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum1;
    nullable = pmBudgetAccum3.InvoicedAmount;
    Decimal baseValueForBudget1 = this.GetBaseValueForBudget(project, curyValue);
    pmBudgetAccum3.InvoicedAmount = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + baseValueForBudget1) : new Decimal?();
    PMBudgetAccum pmBudgetAccum4 = pmBudgetAccum1;
    nullable = pmBudgetAccum4.InvoicedQty;
    Decimal num2 = (Decimal) mult * result;
    pmBudgetAccum4.InvoicedQty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num2) : new Decimal?();
    if (!line.IsPrepayment.GetValueOrDefault())
      return;
    PMBudgetAccum pmBudgetAccum5 = pmBudgetAccum1;
    nullable = pmBudgetAccum5.CuryPrepaymentInvoiced;
    Decimal num3 = curyValue;
    pmBudgetAccum5.CuryPrepaymentInvoiced = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num3) : new Decimal?();
    PMBudgetAccum pmBudgetAccum6 = pmBudgetAccum1;
    nullable = pmBudgetAccum6.PrepaymentInvoiced;
    Decimal baseValueForBudget2 = this.GetBaseValueForBudget(project, curyValue);
    pmBudgetAccum6.PrepaymentInvoiced = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + baseValueForBudget2) : new Decimal?();
  }

  protected virtual void SubtractFromDraftRetained(PMProformaLine line)
  {
    Decimal? lineTotal = line.LineTotal;
    Decimal num = 0M;
    if (lineTotal.GetValueOrDefault() == num & lineTotal.HasValue)
      return;
    int? revenueAccountGroup = line.AccountGroupID;
    if (line.Type == "T")
      revenueAccountGroup = this.GetProjectedAccountGroup(line);
    this.SubtractFromDraftRetained(line, revenueAccountGroup);
  }

  protected virtual void SubtractFromDraftRetained(PMProformaLine line, int? revenueAccountGroup)
  {
    this.AddToDraftRetained(line, revenueAccountGroup, -1);
  }

  protected virtual void AddToDraftRetained(PMProformaLine line)
  {
    int? revenueAccountGroup = line.AccountGroupID;
    if (line.Type == "T")
      revenueAccountGroup = this.GetProjectedAccountGroup(line);
    this.AddToDraftRetained(line, revenueAccountGroup);
  }

  protected virtual void AddToDraftRetained(
    PMProformaLine line,
    int? revenueAccountGroup,
    int mult = 1)
  {
    if (!revenueAccountGroup.HasValue || !line.ProjectID.HasValue || !line.TaskID.HasValue || !this.CanCreateRevenueBudgetLine(line))
      return;
    Decimal curyValue = (Decimal) mult * this.GetAmountInProjectCurrency(line.CuryRetainage);
    PMBudgetAccum pmBudgetAccum1 = ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(this.GetTargetBudget(revenueAccountGroup, line));
    PMBudgetAccum pmBudgetAccum2 = pmBudgetAccum1;
    Decimal? draftRetainedAmount1 = pmBudgetAccum2.CuryDraftRetainedAmount;
    Decimal num = curyValue;
    pmBudgetAccum2.CuryDraftRetainedAmount = draftRetainedAmount1.HasValue ? new Decimal?(draftRetainedAmount1.GetValueOrDefault() + num) : new Decimal?();
    PMProject project = PMProject.PK.Find((PXGraph) this, line.ProjectID);
    PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum1;
    Decimal? draftRetainedAmount2 = pmBudgetAccum3.DraftRetainedAmount;
    Decimal baseValueForBudget = this.GetBaseValueForBudget(project, curyValue);
    pmBudgetAccum3.DraftRetainedAmount = draftRetainedAmount2.HasValue ? new Decimal?(draftRetainedAmount2.GetValueOrDefault() + baseValueForBudget) : new Decimal?();
  }

  public virtual void SubtractFromTotalRetained(PMProformaLine line)
  {
    Decimal? lineTotal = line.LineTotal;
    Decimal num = 0M;
    if (lineTotal.GetValueOrDefault() == num & lineTotal.HasValue)
      return;
    int? revenueAccountGroup = line.AccountGroupID;
    if (line.Type == "T")
      revenueAccountGroup = this.GetProjectedAccountGroup(line);
    this.SubtractFromTotalRetained(line, revenueAccountGroup);
  }

  protected virtual void SubtractFromTotalRetained(PMProformaLine line, int? revenueAccountGroup)
  {
    this.AddToTotalRetained(line, revenueAccountGroup, -1);
  }

  public virtual void AddToTotalRetained(PMProformaLine line)
  {
    int? revenueAccountGroup = line.AccountGroupID;
    if (line.Type == "T")
      revenueAccountGroup = this.GetProjectedAccountGroup(line);
    this.AddToTotalRetained(line, revenueAccountGroup);
  }

  protected virtual void AddToTotalRetained(
    PMProformaLine line,
    int? revenueAccountGroup,
    int mult = 1)
  {
    if (!revenueAccountGroup.HasValue || !line.ProjectID.HasValue || !line.TaskID.HasValue || !this.CanCreateRevenueBudgetLine(line))
      return;
    Decimal curyValue = (Decimal) mult * this.GetAmountInProjectCurrency(line.CuryRetainage);
    PMBudgetAccum pmBudgetAccum1 = ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(this.GetTargetBudget(revenueAccountGroup, line));
    PMBudgetAccum pmBudgetAccum2 = pmBudgetAccum1;
    Decimal? totalRetainedAmount1 = pmBudgetAccum2.CuryTotalRetainedAmount;
    Decimal num = curyValue;
    pmBudgetAccum2.CuryTotalRetainedAmount = totalRetainedAmount1.HasValue ? new Decimal?(totalRetainedAmount1.GetValueOrDefault() + num) : new Decimal?();
    PMProject project = PMProject.PK.Find((PXGraph) this, line.ProjectID);
    PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum1;
    Decimal? totalRetainedAmount2 = pmBudgetAccum3.TotalRetainedAmount;
    Decimal baseValueForBudget = this.GetBaseValueForBudget(project, curyValue);
    pmBudgetAccum3.TotalRetainedAmount = totalRetainedAmount2.HasValue ? new Decimal?(totalRetainedAmount2.GetValueOrDefault() + baseValueForBudget) : new Decimal?();
  }

  protected virtual PMBudgetAccum GetTargetBudget(int? accountGroupID, PMProformaLine line)
  {
    PMAccountGroup ag = PMAccountGroup.PK.Find((PXGraph) this, accountGroupID);
    PMProject project = PMProject.PK.Find((PXGraph) this, line.ProjectID);
    bool isExisting;
    PX.Objects.PM.Lite.PMBudget pmBudget = new BudgetService((PXGraph) this).SelectProjectBalance(ag, project, line.RevenueTaskID ?? line.TaskID, line.InventoryID, line.CostCodeID, out isExisting);
    string str = (line.ProgressBillingBase ?? pmBudget.ProgressBillingBase) ?? PMTask.PK.Find((PXGraph) this, pmBudget.ProjectID, pmBudget.ProjectTaskID)?.ProgressBillingBase;
    PMBudgetAccum pmBudgetAccum = new PMBudgetAccum();
    pmBudgetAccum.Type = pmBudget.Type;
    pmBudgetAccum.ProjectID = pmBudget.ProjectID;
    pmBudgetAccum.ProjectTaskID = pmBudget.TaskID;
    pmBudgetAccum.AccountGroupID = pmBudget.AccountGroupID;
    pmBudgetAccum.InventoryID = pmBudget.InventoryID;
    pmBudgetAccum.CostCodeID = pmBudget.CostCodeID;
    pmBudgetAccum.UOM = pmBudget.UOM;
    pmBudgetAccum.Description = pmBudget.Description;
    pmBudgetAccum.CuryInfoID = project.CuryInfoID;
    pmBudgetAccum.ProgressBillingBase = str;
    pmBudgetAccum.RetainagePct = project.RetainagePct;
    PMBudgetAccum targetBudget = pmBudgetAccum;
    if (!isExisting)
    {
      PMTask pmTask = PMTask.PK.Find((PXGraph) this, pmBudget.ProjectID, pmBudget.ProjectTaskID);
      targetBudget.TaxCategoryID = pmTask.TaxCategoryID;
    }
    return targetBudget;
  }

  protected virtual bool CanCreateRevenueBudgetLine(PMProformaLine line)
  {
    return !(PMTask.PK.Find((PXGraph) this, line.ProjectID, line.TaskID).Type == "Cost");
  }

  public virtual void SubtractPerpaymentRemainder(PMProformaLine line, int mult = 1)
  {
    int? nullable1 = new int?();
    int? accountGroupID = !(line.Type == "T") ? line.AccountGroupID : this.GetProjectedAccountGroup(line);
    if (!accountGroupID.HasValue)
      return;
    int? nullable2 = line.ProjectID;
    if (!nullable2.HasValue)
      return;
    nullable2 = line.TaskID;
    if (!nullable2.HasValue || !this.CanCreateRevenueBudgetLine(line))
      return;
    Decimal curyValue = (Decimal) mult * this.GetAmountInProjectCurrency(line.CuryPrepaidAmount);
    PMBudgetAccum pmBudgetAccum1 = ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(this.GetTargetBudget(accountGroupID, line));
    PMBudgetAccum pmBudgetAccum2 = pmBudgetAccum1;
    Decimal? prepaymentAvailable = pmBudgetAccum2.CuryPrepaymentAvailable;
    Decimal num = curyValue;
    pmBudgetAccum2.CuryPrepaymentAvailable = prepaymentAvailable.HasValue ? new Decimal?(prepaymentAvailable.GetValueOrDefault() - num) : new Decimal?();
    PMProject project = PMProject.PK.Find((PXGraph) this, line.ProjectID);
    PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum1;
    prepaymentAvailable = pmBudgetAccum3.PrepaymentAvailable;
    Decimal baseValueForBudget = this.GetBaseValueForBudget(project, curyValue);
    pmBudgetAccum3.PrepaymentAvailable = prepaymentAvailable.HasValue ? new Decimal?(prepaymentAvailable.GetValueOrDefault() - baseValueForBudget) : new Decimal?();
  }

  protected virtual void ClearValuesToInvoice(PMProformaProgressLine line)
  {
    if (line == null || line.RevisionID.GetValueOrDefault() != 1 || ((PXSelectBase) this.ProgressiveLines).Cache.GetStatus((object) line) != 2)
      return;
    PMRevenueBudget pmRevenueBudget = this.SelectRevenueBudget(line);
    if (pmRevenueBudget == null)
      return;
    this.SubtractValuesToInvoice(line, line.CuryLineTotal, line.Qty, pmRevenueBudget.CuryAmountToInvoice, pmRevenueBudget.QtyToInvoice);
  }

  protected virtual void RestoreValuesToInvoice(PMProformaProgressLine line)
  {
    if (line == null || ((PXSelectBase) this.ProgressiveLines).Cache.GetStatus((object) line) == 4)
      return;
    PMProformaProgressLine line1 = line;
    Decimal num = -line.CuryLineTotal.GetValueOrDefault();
    Decimal? nullable = line.CuryTimeMaterialAmount;
    Decimal valueOrDefault = nullable.GetValueOrDefault();
    Decimal? amount = new Decimal?(num + valueOrDefault);
    nullable = line.Qty;
    Decimal? qty = nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?();
    this.SubtractValuesToInvoice(line1, amount, qty);
  }

  public virtual void SubtractValuesToInvoice(
    PMProformaProgressLine line,
    Decimal? amount,
    Decimal? qty)
  {
    this.SubtractValuesToInvoice(line, amount, qty, new Decimal?(), new Decimal?());
  }

  protected virtual void SubtractValuesToInvoice(
    PMProformaProgressLine line,
    Decimal? amount,
    Decimal? qty,
    Decimal? budgetAmount,
    Decimal? budgetQty)
  {
    if (line == null || line.IsPrepayment.GetValueOrDefault() || !line.AccountGroupID.HasValue || !line.ProjectID.HasValue || !line.TaskID.HasValue || !this.CanCreateRevenueBudgetLine((PMProformaLine) line))
      return;
    Decimal num1 = this.GetAmountInProjectCurrency(amount);
    if (budgetAmount.HasValue)
      num1 = Math.Min(num1, budgetAmount.GetValueOrDefault());
    PMBudgetAccum pmBudgetAccum1 = ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(this.GetTargetBudget(line.AccountGroupID, (PMProformaLine) line));
    PMBudgetAccum pmBudgetAccum2 = pmBudgetAccum1;
    Decimal? nullable = pmBudgetAccum2.CuryAmountToInvoice;
    Decimal num2 = num1;
    pmBudgetAccum2.CuryAmountToInvoice = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - num2) : new Decimal?();
    PMProject project = PMProject.PK.Find((PXGraph) this, line.ProjectID);
    PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum1;
    nullable = pmBudgetAccum3.AmountToInvoice;
    Decimal baseValueForBudget = this.GetBaseValueForBudget(project, num1);
    pmBudgetAccum3.AmountToInvoice = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - baseValueForBudget) : new Decimal?();
    if (!(pmBudgetAccum1.ProgressBillingBase == "Q"))
      return;
    Decimal result;
    INUnitAttribute.TryConvertGlobalUnits((PXGraph) this, line.UOM, pmBudgetAccum1.UOM, qty.GetValueOrDefault(), INPrecision.QUANTITY, out result);
    if (budgetQty.HasValue)
      result = Math.Min(result, budgetQty.GetValueOrDefault());
    PMBudgetAccum pmBudgetAccum4 = pmBudgetAccum1;
    nullable = pmBudgetAccum4.QtyToInvoice;
    Decimal num3 = result;
    pmBudgetAccum4.QtyToInvoice = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - num3) : new Decimal?();
  }

  public virtual void RemoveObsoleteLines()
  {
    HashSet<BudgetKeyTuple> budgetKeyTupleSet = new HashSet<BudgetKeyTuple>();
    foreach (PMProformaProgressLine line in ((PXSelectBase) this.ProgressiveLines).Cache.Inserted)
    {
      int? nullable = line.AccountGroupID;
      if (nullable.HasValue)
      {
        nullable = line.TaskID;
        if (nullable.HasValue)
        {
          BudgetKeyTuple budgetKeyTuple = BudgetKeyTuple.Create((IProjectFilter) this.GetTargetBudget(line.AccountGroupID, (PMProformaLine) line));
          budgetKeyTupleSet.Add(budgetKeyTuple);
        }
      }
    }
    foreach (PMBudgetAccum budget in ((PXSelectBase) this.Budget).Cache.Inserted)
    {
      Decimal? nullable = budget.CuryInvoicedAmount;
      if (nullable.GetValueOrDefault() == 0M)
      {
        nullable = budget.InvoicedQty;
        if (nullable.GetValueOrDefault() == 0M)
        {
          nullable = budget.CuryDraftRetainedAmount;
          if (nullable.GetValueOrDefault() == 0M)
          {
            nullable = budget.CuryTotalRetainedAmount;
            if (nullable.GetValueOrDefault() == 0M)
            {
              BudgetKeyTuple budgetKeyTuple = BudgetKeyTuple.Create((IProjectFilter) budget);
              if (!budgetKeyTupleSet.Contains(budgetKeyTuple))
                ((PXSelectBase) this.Budget).Cache.Remove((object) budget);
            }
          }
        }
      }
    }
  }

  public virtual bool IsLimitsEnabled()
  {
    return ((PXSelectBase<PMProject>) this.Project).Current != null && ((PXSelectBase<PMProject>) this.Project).Current.LimitsEnabled.GetValueOrDefault();
  }

  public virtual bool IsAdjustment(PMProformaTransactLine line)
  {
    if (line == null)
      return false;
    int? lineNbr = line.LineNbr;
    if (!lineNbr.HasValue)
      return true;
    if (((PXGraph) this).UnattendedMode)
      return false;
    Dictionary<int, List<PMTran>> referencedTransactions = this.GetReferencedTransactions();
    lineNbr = line.LineNbr;
    int key = lineNbr.Value;
    return !referencedTransactions.ContainsKey(key);
  }

  public virtual bool IsInventoryVisible()
  {
    return ((PXSelectBase<PMProject>) this.Project).Current == null || this.IsMigrationMode() || EnumerableExtensions.IsIn<string>(((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel, "I", "D");
  }

  public virtual Dictionary<int, List<PMTran>> GetReferencedTransactions()
  {
    if (this.cachedReferencedTransactions == null)
    {
      this.cachedReferencedTransactions = new Dictionary<int, List<PMTran>>();
      foreach (PMTran pmTran in GraphHelper.RowCast<PMTran>((IEnumerable) ((PXSelectBase) this.AllReferencedTransactions).View.SelectMultiBound((object[]) new PMProforma[1]
      {
        ((PXSelectBase<PMProforma>) this.Document).Current
      }, Array.Empty<object>())))
      {
        int? proformaLineNbr = pmTran.ProformaLineNbr;
        if (proformaLineNbr.HasValue)
        {
          Dictionary<int, List<PMTran>> referencedTransactions1 = this.cachedReferencedTransactions;
          proformaLineNbr = pmTran.ProformaLineNbr;
          int key1 = proformaLineNbr.Value;
          List<PMTran> pmTranList1;
          ref List<PMTran> local = ref pmTranList1;
          if (!referencedTransactions1.TryGetValue(key1, out local))
          {
            pmTranList1 = new List<PMTran>();
            Dictionary<int, List<PMTran>> referencedTransactions2 = this.cachedReferencedTransactions;
            proformaLineNbr = pmTran.ProformaLineNbr;
            int key2 = proformaLineNbr.Value;
            List<PMTran> pmTranList2 = pmTranList1;
            referencedTransactions2.Add(key2, pmTranList2);
          }
          pmTranList1.Add(pmTran);
        }
      }
    }
    return this.cachedReferencedTransactions;
  }

  public bool IsAllocated(PMProformaTransactLine row) => false;

  public virtual int? GetProjectedAccountGroup(PMProformaLine line)
  {
    int? projectedAccountGroup = new int?();
    int? accountId = line.AccountID;
    if (accountId.HasValue)
    {
      PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, accountId);
      if (account != null)
      {
        if (!account.AccountGroupID.HasValue)
          throw new PXException("Revenue Account {0} is not mapped to Account Group.", new object[1]
          {
            (object) account.AccountCD
          });
        projectedAccountGroup = account.AccountGroupID;
      }
    }
    return projectedAccountGroup;
  }

  public virtual PMRevenueBudget SelectRevenueBudget(PMProformaProgressLine row)
  {
    PMRevenueBudget pmRevenueBudget = PMRevenueBudget.PK.Find((PXGraph) this, row.ProjectID, row.TaskID, row.AccountGroupID, row.CostCodeID, row.InventoryID);
    if (pmRevenueBudget == null)
    {
      PX.Objects.PM.Lite.PMBudget pmBudget = new BudgetService((PXGraph) this).SelectExistingProjectBalance(row.ProjectID, row.TaskID, row.AccountGroupID, row.CostCodeID, row.InventoryID);
      if (pmBudget != null)
        pmRevenueBudget = PMRevenueBudget.PK.Find((PXGraph) this, pmBudget.ProjectID, pmBudget.TaskID, pmBudget.AccountGroupID, pmBudget.CostCodeID, pmBudget.InventoryID);
    }
    return pmRevenueBudget;
  }

  /// <summary>
  /// Returns Pending InvoicedAmount calculated for current document in Project Currency.
  /// </summary>
  public virtual Decimal CalculatePendingInvoicedAmount(PMProformaProgressLine row)
  {
    return this.CalculatePendingInvoicedAmount(row.ProjectID, row.TaskID, row.AccountGroupID, row.InventoryID, row.CostCodeID);
  }

  /// <summary>
  /// Returns Pending InvoicedAmount calculated for current document in Project Currency.
  /// </summary>
  public virtual Decimal CalculatePendingInvoicedAmount(
    int? projectID,
    int? taskID,
    int? accountGroupID,
    int? inventoryID,
    int? costCodeID)
  {
    Decimal pendingInvoicedAmount = 0M;
    foreach (PMBudgetAccum pmBudgetAccum in ((PXSelectBase) this.Budget).Cache.Inserted)
    {
      int? nullable1 = pmBudgetAccum.ProjectID;
      int? nullable2 = projectID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        nullable2 = pmBudgetAccum.ProjectTaskID;
        nullable1 = taskID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          nullable1 = pmBudgetAccum.AccountGroupID;
          nullable2 = accountGroupID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = pmBudgetAccum.InventoryID;
            nullable1 = inventoryID;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            {
              nullable1 = pmBudgetAccum.CostCodeID;
              nullable2 = costCodeID;
              if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
                pendingInvoicedAmount += pmBudgetAccum.CuryInvoicedAmount.GetValueOrDefault();
            }
          }
        }
      }
    }
    return pendingInvoicedAmount;
  }

  /// <summary>Returns Prevously invoiced Amount in Project Currency</summary>
  public virtual Dictionary<BudgetKeyTuple, Decimal> CalculatePreviouslyInvoicedAmounts(
    PMProforma document)
  {
    Dictionary<BudgetKeyTuple, Decimal> previouslyInvoicedAmounts = new Dictionary<BudgetKeyTuple, Decimal>();
    PXResultset<PMProformaProgressLine> pxResultset1 = ((PXSelectBase<PMProformaProgressLine>) new PXSelect<PMProformaProgressLine, Where<PMProformaProgressLine.type, Equal<PMProformaLineType.progressive>, And<PMProformaLine.projectID, Equal<Required<PMProforma.projectID>>, And<PMProformaProgressLine.refNbr, GreaterEqual<Required<PMProformaProgressLine.refNbr>>, And<PMProformaLine.corrected, NotEqual<True>>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) document.ProjectID,
      (object) document.RefNbr
    });
    PXResultset<PMProformaTransactLine> pxResultset2 = ((PXSelectBase<PMProformaTransactLine>) new PXSelect<PMProformaTransactLine, Where<PMProformaTransactLine.type, Equal<PMProformaLineType.transaction>, And<PMProformaLine.projectID, Equal<Required<PMProforma.projectID>>, And<PMProformaTransactLine.refNbr, GreaterEqual<Required<PMProformaTransactLine.refNbr>>, And<PMProformaLine.corrected, NotEqual<True>>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) document.ProjectID,
      (object) document.RefNbr
    });
    foreach (PXResult<PMRevenueBudget> pxResult in ((PXSelectBase<PMRevenueBudget>) new PXSelect<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Required<PMRevenueBudget.projectID>>, And<PMRevenueBudget.type, Equal<AccountType.income>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) document.ProjectID
    }))
    {
      PMRevenueBudget pmRevenueBudget = PXResult<PMRevenueBudget>.op_Implicit(pxResult);
      BudgetKeyTuple key = BudgetKeyTuple.Create((IProjectFilter) pmRevenueBudget);
      Decimal num = this.GetCuryActualAmountWithTaxes(pmRevenueBudget) + pmRevenueBudget.CuryInvoicedAmount.GetValueOrDefault();
      if (previouslyInvoicedAmounts.ContainsKey(key))
        previouslyInvoicedAmounts[key] += num;
      else
        previouslyInvoicedAmounts[key] = num;
    }
    foreach (PMBudgetAccum budget in ((PXSelectBase) this.Budget).Cache.Inserted)
    {
      BudgetKeyTuple key1 = BudgetKeyTuple.Create((IProjectFilter) budget);
      Decimal? curyInvoicedAmount;
      if (previouslyInvoicedAmounts.ContainsKey(key1))
      {
        Dictionary<BudgetKeyTuple, Decimal> dictionary1 = previouslyInvoicedAmounts;
        BudgetKeyTuple key2 = key1;
        Dictionary<BudgetKeyTuple, Decimal> dictionary2 = dictionary1;
        BudgetKeyTuple key3 = key2;
        Decimal num1 = dictionary1[key2];
        curyInvoicedAmount = budget.CuryInvoicedAmount;
        Decimal valueOrDefault = curyInvoicedAmount.GetValueOrDefault();
        Decimal num2 = num1 + valueOrDefault;
        dictionary2[key3] = num2;
      }
      else
      {
        Dictionary<BudgetKeyTuple, Decimal> dictionary = previouslyInvoicedAmounts;
        BudgetKeyTuple key4 = key1;
        curyInvoicedAmount = budget.CuryInvoicedAmount;
        Decimal valueOrDefault = curyInvoicedAmount.GetValueOrDefault();
        dictionary[key4] = valueOrDefault;
      }
    }
    foreach (PXResult<PMProformaProgressLine> pxResult in pxResultset1)
    {
      PMProformaProgressLine budget = PXResult<PMProformaProgressLine>.op_Implicit(pxResult);
      BudgetKeyTuple key = BudgetKeyTuple.Create((IProjectFilter) budget);
      if (previouslyInvoicedAmounts.ContainsKey(key))
        previouslyInvoicedAmounts[key] -= this.GetAmountInProjectCurrency(budget.CuryLineTotal);
    }
    foreach (PXResult<PMProformaTransactLine> pxResult in pxResultset2)
    {
      PMProformaTransactLine line = PXResult<PMProformaTransactLine>.op_Implicit(pxResult);
      BudgetKeyTuple budgetKey = this.GetBudgetKey(line);
      if (previouslyInvoicedAmounts.ContainsKey(budgetKey))
      {
        previouslyInvoicedAmounts[budgetKey] -= this.GetAmountInProjectCurrency(line.CuryLineTotal);
      }
      else
      {
        BudgetKeyTuple key = new BudgetKeyTuple(budgetKey.ProjectID, budgetKey.ProjectTaskID, budgetKey.AccountGroupID, PMInventorySelectorAttribute.EmptyInventoryID, budgetKey.CostCodeID);
        if (previouslyInvoicedAmounts.ContainsKey(key))
          previouslyInvoicedAmounts[key] -= this.GetAmountInProjectCurrency(line.CuryLineTotal);
      }
    }
    return previouslyInvoicedAmounts;
  }

  protected virtual void ResetQtyAndAmountToInvoiceOnOptionChange(
    PMProformaTransactLine line,
    string optionValue,
    string oldOptionValue)
  {
    switch (optionValue)
    {
      case "X":
        ((PXSelectBase) this.TransactionLines).Cache.SetValueExt<PMProformaLine.curyPrepaidAmount>((object) line, (object) 0M);
        ((PXSelectBase) this.TransactionLines).Cache.SetValueExt<PMProformaLine.qty>((object) line, (object) 0M);
        ((PXSelectBase) this.TransactionLines).Cache.SetValueExt<PMProformaTransactLine.curyLineTotal>((object) line, (object) 0M);
        break;
      case "N":
        Decimal? curyLineTotal = line.CuryLineTotal;
        Decimal num = 0M;
        if (!(curyLineTotal.GetValueOrDefault() >= num & curyLineTotal.HasValue))
          break;
        ((PXSelectBase) this.TransactionLines).Cache.SetValueExt<PMProformaLine.qty>((object) line, (object) line.BillableQty);
        ((PXSelectBase) this.TransactionLines).Cache.SetValueExt<PMProformaTransactLine.curyLineTotal>((object) line, (object) line.CuryBillableAmount);
        break;
    }
  }

  private Decimal GetAmountInProjectCurrency(Decimal? value)
  {
    return this.MultiCurrencyService.GetValueInProjectCurrency((PXGraph) this, ((PXSelectBase<PMProject>) this.Project).Current, ((PXSelectBase<PMProforma>) this.Document).Current.CuryID, ((PXSelectBase<PMProforma>) this.Document).Current.InvoiceDate, value);
  }

  private Decimal GetAmountInBillingCurrency(Decimal? value)
  {
    return this.MultiCurrencyService.GetValueInBillingCurrency((PXGraph) this, ((PXSelectBase<PMProject>) this.Project).Current, ((PXGraph) this).GetExtension<ProformaEntry.MultiCurrency>().GetDefaultCurrencyInfo(), value);
  }

  private Decimal GetLastInvoicedBeforeCorrection(PMProformaProgressLine row)
  {
    if (row.RevisionID.GetValueOrDefault() < 2)
      return 0M;
    PMProformaProgressLine proformaProgressLine = ((PXSelectBase<PMProformaProgressLine>) new PXSelect<PMProformaProgressLine, Where<PMProformaProgressLine.refNbr, Equal<Required<PMProformaProgressLine.refNbr>>, And<PMProformaProgressLine.lineNbr, Equal<Required<PMProformaProgressLine.lineNbr>>, And<PMProformaLine.corrected, Equal<True>>>>, OrderBy<Desc<PMProformaProgressLine.revisionID>>>((PXGraph) this)).SelectSingle(new object[2]
    {
      (object) row.RefNbr,
      (object) row.LineNbr
    });
    return proformaProgressLine != null ? proformaProgressLine.CuryLineTotal.GetValueOrDefault() : 0M;
  }

  public virtual bool IsPrepaidAmountEnabled(PMProformaLine line)
  {
    return !line.IsPrepayment.GetValueOrDefault();
  }

  public virtual void ApplyPrepayment(PMProforma doc)
  {
    PXSelect<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Required<PMRevenueBudget.projectID>>, And<PMRevenueBudget.curyPrepaymentAvailable, Greater<decimal0>>>> pxSelect = new PXSelect<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Required<PMRevenueBudget.projectID>>, And<PMRevenueBudget.curyPrepaymentAvailable, Greater<decimal0>>>>((PXGraph) this);
    Dictionary<BudgetKeyTuple, Decimal> dictionary1 = new Dictionary<BudgetKeyTuple, Decimal>();
    object[] objArray = new object[1]
    {
      (object) doc.ProjectID
    };
    foreach (PXResult<PMRevenueBudget> pxResult in ((PXSelectBase<PMRevenueBudget>) pxSelect).Select(objArray))
    {
      PMRevenueBudget budget = PXResult<PMRevenueBudget>.op_Implicit(pxResult);
      BudgetKeyTuple key1 = BudgetKeyTuple.Create((IProjectFilter) budget);
      Decimal? prepaymentAvailable;
      if (dictionary1.ContainsKey(key1))
      {
        Dictionary<BudgetKeyTuple, Decimal> dictionary2 = dictionary1;
        BudgetKeyTuple key2 = key1;
        Dictionary<BudgetKeyTuple, Decimal> dictionary3 = dictionary2;
        BudgetKeyTuple key3 = key2;
        Decimal num1 = dictionary2[key2];
        prepaymentAvailable = budget.CuryPrepaymentAvailable;
        Decimal valueOrDefault = prepaymentAvailable.GetValueOrDefault();
        Decimal num2 = num1 + valueOrDefault;
        dictionary3[key3] = num2;
      }
      else
      {
        Dictionary<BudgetKeyTuple, Decimal> dictionary4 = dictionary1;
        BudgetKeyTuple key4 = key1;
        prepaymentAvailable = budget.CuryPrepaymentAvailable;
        Decimal valueOrDefault = prepaymentAvailable.GetValueOrDefault();
        dictionary4[key4] = valueOrDefault;
      }
    }
    foreach (PMBudgetAccum budget in ((PXSelectBase) this.Budget).Cache.Inserted)
    {
      BudgetKeyTuple key = BudgetKeyTuple.Create((IProjectFilter) budget);
      if (dictionary1.ContainsKey(key))
        dictionary1[key] += budget.CuryPrepaymentAvailable.GetValueOrDefault();
    }
    foreach (PXResult<PMProformaProgressLine> pxResult in ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Select(Array.Empty<object>()))
    {
      PMProformaProgressLine budget = PXResult<PMProformaProgressLine>.op_Implicit(pxResult);
      if (!budget.IsPrepayment.GetValueOrDefault())
      {
        BudgetKeyTuple key = BudgetKeyTuple.Create((IProjectFilter) budget);
        if (((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "T")
          key = new BudgetKeyTuple(key.ProjectID, key.ProjectTaskID, key.AccountGroupID, key.InventoryID, CostCodeAttribute.DefaultCostCode.GetValueOrDefault());
        if (((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "I" || ((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "D")
        {
          int? inventoryId = budget.InventoryID;
          int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
          if (!(inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue) && !dictionary1.ContainsKey(key))
            key = new BudgetKeyTuple(key.ProjectID, key.ProjectTaskID, key.AccountGroupID, PMInventorySelectorAttribute.EmptyInventoryID, key.CostCodeID);
        }
        Decimal num3 = 0M;
        if (dictionary1.TryGetValue(key, out num3) && num3 > 0M)
        {
          Decimal? curyAmount = budget.CuryAmount;
          Decimal num4 = 0M;
          if (curyAmount.GetValueOrDefault() > num4 & curyAmount.HasValue)
          {
            Decimal? curyPrepaidAmount = budget.CuryPrepaidAmount;
            Decimal num5 = 0M;
            if (curyPrepaidAmount.GetValueOrDefault() == num5 & curyPrepaidAmount.HasValue)
            {
              Decimal inBillingCurrency = this.GetAmountInBillingCurrency(new Decimal?(num3));
              budget.CuryPrepaidAmount = new Decimal?(Math.Min(inBillingCurrency, budget.CuryAmount.Value));
              ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Update(budget);
              dictionary1[key] -= this.GetAmountInProjectCurrency(budget.CuryPrepaidAmount);
            }
          }
        }
      }
    }
    foreach (PXResult<PMProformaTransactLine> pxResult in ((PXSelectBase<PMProformaTransactLine>) this.TransactionLines).Select(Array.Empty<object>()))
    {
      PMProformaTransactLine budget = PXResult<PMProformaTransactLine>.op_Implicit(pxResult);
      if (!budget.IsPrepayment.GetValueOrDefault())
      {
        BudgetKeyTuple key = BudgetKeyTuple.Create((IProjectFilter) budget);
        if (((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "T")
          key = new BudgetKeyTuple(key.ProjectID, key.ProjectTaskID, key.AccountGroupID, key.InventoryID, CostCodeAttribute.DefaultCostCode.GetValueOrDefault());
        if (((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "I" || ((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "D")
        {
          int? inventoryId = budget.InventoryID;
          int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
          if (!(inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue) && !dictionary1.ContainsKey(key))
            key = new BudgetKeyTuple(key.ProjectID, key.ProjectTaskID, key.AccountGroupID, PMInventorySelectorAttribute.EmptyInventoryID, key.CostCodeID);
        }
        Decimal num6 = 0M;
        if (dictionary1.TryGetValue(key, out num6) && num6 > 0M)
        {
          Decimal? nullable1 = budget.CuryAmount;
          Decimal num7 = 0M;
          if (nullable1.GetValueOrDefault() > num7 & nullable1.HasValue)
          {
            nullable1 = budget.CuryPrepaidAmount;
            if (nullable1.GetValueOrDefault() == 0M)
            {
              Decimal inBillingCurrency = this.GetAmountInBillingCurrency(new Decimal?(num6));
              PMProformaTransactLine proformaTransactLine = budget;
              Decimal val1 = inBillingCurrency;
              nullable1 = budget.CuryAmount;
              Decimal valueOrDefault = nullable1.GetValueOrDefault();
              Decimal? nullable2 = new Decimal?(Math.Min(val1, valueOrDefault));
              proformaTransactLine.CuryPrepaidAmount = nullable2;
              ((PXSelectBase<PMProformaTransactLine>) this.TransactionLines).Update(budget);
              dictionary1[key] -= this.GetAmountInProjectCurrency(budget.CuryPrepaidAmount);
            }
          }
        }
      }
    }
  }

  public virtual IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    return ((PXGraph) this).ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
  }

  public virtual void ValidateBeforeRelease(PMProforma doc)
  {
  }

  protected virtual bool CanAddToUnbilledTransactionLookup(PMTran tran)
  {
    if (!tran.RemainderOfTranID.HasValue)
      return true;
    PMTran pmTran = PMTran.PK.Find((PXGraph) this, tran.RemainderOfTranID);
    return pmTran == null || ((PXSelectBase<PMProforma>) this.Document).Current == null || string.Compare(pmTran.ProformaRefNbr, ((PXSelectBase<PMProforma>) this.Document).Current.RefNbr, true) < 0;
  }

  public virtual void ValidatePrecedingBeforeRelease(PMProforma doc)
  {
    PXResultset<PMBillingRecord> pxResultset = ((PXSelectBase<PMBillingRecord>) new PXSelectJoin<PMBillingRecord, InnerJoin<PMBillingRecordEx, On<PMBillingRecord.projectID, Equal<PMBillingRecordEx.projectID>, And<PMBillingRecord.billingTag, Equal<PMBillingRecordEx.billingTag>, And<PMBillingRecord.proformaRefNbr, Greater<PMBillingRecordEx.proformaRefNbr>, And<PMBillingRecordEx.aRRefNbr, IsNull>>>>>, Where<PMBillingRecord.projectID, Equal<Required<PMBillingRecord.projectID>>, And<PMBillingRecord.proformaRefNbr, Equal<Required<PMBillingRecord.proformaRefNbr>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) doc.ProjectID,
      (object) doc.RefNbr
    });
    if (pxResultset.Count > 0)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (PXResult<PMBillingRecord, PMBillingRecordEx> pxResult in pxResultset)
      {
        PMBillingRecordEx pmBillingRecordEx = PXResult<PMBillingRecord, PMBillingRecordEx>.op_Implicit(pxResult);
        stringBuilder.AppendFormat("{0},", (object) pmBillingRecordEx.ProformaRefNbr);
      }
      throw new PXException("Pro Forma documents should be released in the sequence they were created. You cannot release the document until you release the following documents that precede the current one: {0}.", new object[1]
      {
        (object) stringBuilder.ToString().TrimEnd(',')
      });
    }
  }

  public virtual void ValidatePrecedingInvoicesBeforeRelease(PMProforma doc)
  {
    PXResultset<PMBillingRecord> pxResultset = ((PXSelectBase<PMBillingRecord>) new PXSelectJoin<PMBillingRecord, InnerJoin<PMBillingRecordEx, On<PMBillingRecord.projectID, Equal<PMBillingRecordEx.projectID>, And<PMBillingRecord.billingTag, Equal<PMBillingRecordEx.billingTag>, And<PMBillingRecord.proformaRefNbr, Greater<PMBillingRecordEx.proformaRefNbr>, And<PMBillingRecordEx.aRRefNbr, IsNotNull>>>>, InnerJoin<PX.Objects.AR.ARRegister, On<PX.Objects.AR.ARRegister.docType, Equal<PMBillingRecordEx.aRDocType>, And<PX.Objects.AR.ARRegister.refNbr, Equal<PMBillingRecordEx.aRRefNbr>>>>>, Where<PMBillingRecord.projectID, Equal<Required<PMBillingRecord.projectID>>, And<PMBillingRecord.proformaRefNbr, Equal<Required<PMBillingRecord.proformaRefNbr>>>>, OrderBy<Desc<PMBillingRecordEx.recordID>>>((PXGraph) this)).SelectWindowed(0, 1, new object[2]
    {
      (object) doc.ProjectID,
      (object) doc.RefNbr
    });
    if (pxResultset.Count <= 0)
      return;
    PX.Objects.AR.ARRegister arRegister = PXResult.Unwrap<PX.Objects.AR.ARRegister>((object) pxResultset[0]);
    if (arRegister != null && !arRegister.Released.GetValueOrDefault())
      throw new PXException("You cannot release the pro forma invoice until you release the {1} {0} on the Invoices and Memos (AR301000) form.", new object[2]
      {
        (object) arRegister.DocType,
        (object) arRegister.RefNbr
      });
  }

  public virtual void ValidateBranchBeforeRelease(PMProforma doc)
  {
    if (PX.Objects.GL.Branch.PK.Find((PXGraph) this, doc.BranchID) != null)
      return;
    using (new PXReadDeletedScope(false))
    {
      PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this, doc.BranchID);
      if (branch != null)
        throw new PXException("The pro forma invoice cannot be released because the {0} branch is inactive or does not exist in the system. To be able to process the pro forma invoice, activate or create the {0} branch on the Branches (CS102000) form.", new object[1]
        {
          (object) branch.BranchCD.Trim()
        });
    }
  }

  public virtual void AppendUnbilled()
  {
    if (((PXSelectBase<PMProforma>) this.Document).Current == null)
      return;
    string billingTag = PXResultset<PMBillingRecord>.op_Implicit(PXSelectBase<PMBillingRecord, PXSelect<PMBillingRecord, Where<PMBillingRecord.proformaRefNbr, Equal<Required<PMProforma.refNbr>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<PMProforma>) this.Document).Current.RefNbr
    }))?.BillingTag;
    DateTime billingDate = ((PXSelectBase<PMProforma>) this.Document).Current.InvoiceDate.Value;
    ProformaAppender instance = PXGraph.CreateInstance<ProformaAppender>();
    instance.SetProformaEntry(this);
    List<PMTask> tasks = instance.SelectBillableTasks(((PXSelectBase<PMProject>) this.Project).Current);
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<PMProforma>) this.Document).Current.CustomerID
    }));
    DateTime dateTime = billingDate.AddDays((double) (instance.IncludeTodaysTransactions ? 1 : 0));
    instance.PreSelectTasksTransactions(((PXSelectBase<PMProforma>) this.Document).Current.ProjectID, tasks, new DateTime?(dateTime));
    HashSet<string> source1 = new HashSet<string>();
    foreach (PMTask pmTask in tasks)
    {
      if (!string.IsNullOrEmpty(pmTask.RateTableID))
        source1.Add(pmTask.RateTableID);
    }
    HashSet<string> source2 = new HashSet<string>();
    foreach (List<PMBillingRule> pmBillingRuleList in instance.billingRules.Values)
    {
      foreach (PMBillingRule pmBillingRule in pmBillingRuleList)
      {
        if (!string.IsNullOrEmpty(pmBillingRule.RateTypeID))
          source2.Add(pmBillingRule.RateTypeID);
      }
    }
    instance.InitRateEngine((IList<string>) source1.ToList<string>(), (IList<string>) source2.ToList<string>());
    List<PMTran> pmTranList = new List<PMTran>();
    List<PMBillEngine.BillingData> unbilled = new List<PMBillEngine.BillingData>();
    Dictionary<int, Decimal> availableQty = new Dictionary<int, Decimal>();
    Dictionary<int, PMRecurringItem> billingItems = new Dictionary<int, PMRecurringItem>();
    foreach (PMTask task in tasks)
    {
      List<PMBillingRule> pmBillingRuleList;
      if (!task.WipAccountGroupID.HasValue && instance.billingRules.TryGetValue(task.BillingID, out pmBillingRuleList))
      {
        foreach (PMBillingRule rule in pmBillingRuleList)
        {
          if (rule.Type == "T")
            unbilled.AddRange((IEnumerable<PMBillEngine.BillingData>) instance.BillTask(((PXSelectBase<PMProject>) this.Project).Current, customer, task, rule, billingDate, availableQty, billingItems, true));
        }
      }
    }
    bool isBilling = this.IsBilling;
    this.IsBilling = true;
    try
    {
      instance.InsertTransactionsInProforma(((PXSelectBase<PMProject>) this.Project).Current, unbilled);
    }
    finally
    {
      this.IsBilling = isBilling;
    }
    this.cachedReferencedTransactions = (Dictionary<int, List<PMTran>>) null;
    foreach (PMBillEngine.BillingData billingData in unbilled)
    {
      foreach (PMTran transaction in billingData.Transactions)
      {
        transaction.Billed = new bool?(true);
        transaction.BilledDate = new DateTime?(billingDate);
        ((PXSelectBase<PMTran>) this.Unbilled).Update(transaction);
        RegisterReleaseProcess.SubtractFromUnbilledSummary((PXGraph) this, transaction);
      }
    }
  }

  /// <summary>
  /// If false during the release of proforma documet taxes are copied as is; otherwise taxes are recalculated automaticaly on the ARInvoice.
  /// Default value is false.
  /// </summary>
  public virtual bool RecalculateTaxesOnRelease()
  {
    return ((PXSelectBase<PMProforma>) this.Document).Current != null && ((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Current != null && ((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Current.PaymentsByLinesAllowed.GetValueOrDefault();
  }

  public virtual BudgetKeyTuple GetBudgetKey(PMProformaTransactLine line)
  {
    int? projectedAccountGroup = this.GetProjectedAccountGroup((PMProformaLine) line);
    int inventoryID = line.InventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID;
    if (((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel != "I")
      inventoryID = PMInventorySelectorAttribute.EmptyInventoryID;
    BudgetKeyTuple budgetKeyTuple = BudgetKeyTuple.Create((IProjectFilter) line);
    return new BudgetKeyTuple(budgetKeyTuple.ProjectID, (line.RevenueTaskID ?? line.TaskID).GetValueOrDefault(), projectedAccountGroup.GetValueOrDefault(), inventoryID, budgetKeyTuple.CostCodeID);
  }

  protected virtual Decimal CalculateRetainagePct(PMProformaLine line)
  {
    Decimal? nullable = line.CuryRetainage;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = line.CuryLineTotal;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    return Math.Min(100M, Math.Round(100M * Math.Abs(valueOrDefault1 / valueOrDefault2), 6));
  }

  protected virtual void RecalculateRetainageOnDocument(PMProject project)
  {
    if (!(project?.RetainageMode == "C"))
      return;
    PMProjectRevenueTotal budget = PXResultset<PMProjectRevenueTotal>.op_Implicit(PXSelectBase<PMProjectRevenueTotal, PXSelectReadonly<PMProjectRevenueTotal, Where<PMProjectRevenueTotal.projectID, Equal<Required<PMProjectRevenueTotal.projectID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) project.ContractID
    }));
    Decimal retainageOnInvoice = this.GetTotalRetainageOnInvoice(project.RetainagePct.GetValueOrDefault());
    Decimal retainageUptoDate1 = this.GetTotalRetainageUptoDate(budget);
    Decimal contractAmount = this.GetContractAmount(project, budget);
    this.RecalculateContractRetainage(project, retainageUptoDate1, contractAmount, retainageOnInvoice);
    Decimal totalInvoiced = this.GetTotalInvoiced(budget);
    Decimal retainageUptoDate2 = this.GetTotalRetainageUptoDate(budget, true);
    Decimal retainageToDateTotal = this.GetBilledRetainageToDateTotal(((PXSelectBase<PMProforma>) this.Document).Current.InvoiceDate);
    this.ReAllocateContractRetainage(project, totalInvoiced, retainageUptoDate2 - retainageToDateTotal);
    ((PXSelectBase) this.ProgressiveLines).View.RequestRefresh();
  }

  /// <summary>
  /// Returns Total Retainage accumulated upto date excluding current document in project currency
  /// </summary>
  private Decimal GetTotalRetainageUptoDate(PMProjectRevenueTotal budget)
  {
    return this.GetTotalRetainageUptoDate(budget, false);
  }

  /// <summary>
  /// Returns Total Retainage accumulated upto date in project currency
  /// </summary>
  private Decimal GetTotalRetainageUptoDate(
    PMProjectRevenueTotal budget,
    bool includeCurrentDocument)
  {
    Decimal valueOrDefault = budget.CuryTotalRetainedAmount.GetValueOrDefault();
    foreach (PMBudgetAccum pmBudgetAccum in ((PXSelectBase) this.Budget).Cache.Inserted)
      valueOrDefault += pmBudgetAccum.CuryTotalRetainedAmount.GetValueOrDefault();
    if (!includeCurrentDocument)
    {
      foreach (PXResult<PMProformaProgressLine> pxResult in ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Select(Array.Empty<object>()))
      {
        PMProformaProgressLine proformaProgressLine = PXResult<PMProformaProgressLine>.op_Implicit(pxResult);
        valueOrDefault -= this.GetAmountInProjectCurrency(new Decimal?(proformaProgressLine.CuryRetainage.GetValueOrDefault()));
      }
    }
    return valueOrDefault;
  }

  public virtual void RecalculateRetainage()
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null || ((PXSelectBase<PMProforma>) this.Document).Current == null || ((PXSelectBase<PMProforma>) this.Document).Current.Released.GetValueOrDefault())
      return;
    if (((PXSelectBase<PMProject>) this.Project).Current.RetainageMode == "C")
    {
      PMProjectRevenueTotal budget = PXResultset<PMProjectRevenueTotal>.op_Implicit(PXSelectBase<PMProjectRevenueTotal, PXSelectReadonly<PMProjectRevenueTotal, Where<PMProjectRevenueTotal.projectID, Equal<Required<PMProjectRevenueTotal.projectID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID
      }));
      this.RecalculateContractRetainage(((PXSelectBase<PMProject>) this.Project).Current, budget);
      this.ReAllocateContractRetainage(((PXSelectBase<PMProject>) this.Project).Current, budget);
    }
    else
    {
      if (!(((PXSelectBase<PMProject>) this.Project).Current.RetainageMode == "L"))
        return;
      this.RecalculateLineRetainage(((PXSelectBase<PMProject>) this.Project).Current);
    }
  }

  protected virtual void RecalculateContractRetainage(
    PMProject project,
    PMProjectRevenueTotal budget)
  {
    Decimal inProjectCurrency = this.GetAmountInProjectCurrency(new Decimal?(this.GetTotalRetainageOnInvoice(project.RetainagePct.GetValueOrDefault())));
    Decimal valueOrDefault = budget.CuryTotalRetainedAmount.GetValueOrDefault();
    Decimal contractAmount = this.GetContractAmount(project, budget);
    this.RecalculateContractRetainage(project, valueOrDefault, contractAmount, inProjectCurrency);
  }

  protected virtual void RecalculateContractRetainage(
    PMProject project,
    Decimal totalRetainageUptoDate,
    Decimal contractAmount,
    Decimal totalRetainageOnInvoice)
  {
    this.RecalculatingContractRetainage = true;
    try
    {
      Decimal roundingOverflow = 0M;
      foreach (PXResult<PMProformaProgressLine, PMRevenueBudget> pxResult in ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Select(Array.Empty<object>()))
      {
        PMProformaProgressLine copy = (PMProformaProgressLine) ((PXSelectBase) this.ProgressiveLines).Cache.CreateCopy((object) PXResult<PMProformaProgressLine, PMRevenueBudget>.op_Implicit(pxResult));
        Tuple<Decimal, Decimal> contractRetainageOnLine = this.CalculateContractRetainageOnLine(project, copy, totalRetainageUptoDate, contractAmount, totalRetainageOnInvoice, roundingOverflow);
        copy.CuryRetainage = new Decimal?(this.GetAmountInBillingCurrency(new Decimal?(contractRetainageOnLine.Item1)));
        roundingOverflow = contractRetainageOnLine.Item2;
        ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Update(copy);
      }
    }
    finally
    {
      this.RecalculatingContractRetainage = false;
    }
  }

  /// <summary>effectiveRetainage in project currency</summary>
  protected virtual Tuple<Decimal, Decimal> CalculateContractRetainageOnLine(
    PMProject project,
    PMProformaProgressLine line,
    Decimal totalRetainageUptoDate,
    Decimal contractAmount,
    Decimal totalRetainageOnInvoice,
    Decimal roundingOverflow)
  {
    Decimal num1 = Decimal.Round(contractAmount * 0.01M * project.RetainagePct.GetValueOrDefault() * project.RetainageMaxPct.GetValueOrDefault() * 0.01M, 2);
    Decimal? nullable = line.CuryLineTotal;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = project.RetainagePct;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    Decimal inProjectCurrency = this.GetAmountInProjectCurrency(new Decimal?(Decimal.Round(valueOrDefault1 * valueOrDefault2 * 0.01M, 2)));
    Decimal num2;
    if (totalRetainageOnInvoice <= 0M || totalRetainageOnInvoice + totalRetainageUptoDate <= num1)
    {
      num2 = inProjectCurrency;
    }
    else
    {
      Decimal num3 = (totalRetainageOnInvoice + totalRetainageUptoDate - num1) * (inProjectCurrency / totalRetainageOnInvoice);
      Decimal num4 = Math.Max(0M, Decimal.Round(inProjectCurrency - num3 + roundingOverflow, 2));
      roundingOverflow = inProjectCurrency - num3 + roundingOverflow - num4;
      num2 = num4;
    }
    return new Tuple<Decimal, Decimal>(num2, roundingOverflow);
  }

  private Decimal GetTotalRetainageOnInvoice(Decimal retainagePct)
  {
    Decimal retainageOnInvoice = 0M;
    foreach (PXResult<PMProformaProgressLine, PMRevenueBudget> pxResult in ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Select(Array.Empty<object>()))
    {
      Decimal num = PXResult<PMProformaProgressLine, PMRevenueBudget>.op_Implicit(pxResult).CuryLineTotal.GetValueOrDefault() * retainagePct * 0.01M;
      if (num > 0M)
        retainageOnInvoice += num;
    }
    return retainageOnInvoice;
  }

  protected virtual void ReAllocateContractRetainage(
    PMProject project,
    PMProjectRevenueTotal budget)
  {
    Decimal totalInvoiced = this.GetTotalInvoiced(budget);
    Decimal retainageUptoDate = this.GetTotalRetainageUptoDate(budget, true);
    Decimal retainageToDateTotal = this.GetBilledRetainageToDateTotal(((PXSelectBase<PMProforma>) this.Document).Current.InvoiceDate);
    this.ReAllocateContractRetainage(project, totalInvoiced, retainageUptoDate - this.GetAmountInProjectCurrency(new Decimal?(retainageToDateTotal)));
  }

  protected virtual void ReAllocateContractRetainage(
    PMProject project,
    Decimal totalInvoiceUptoDate,
    Decimal retainageToAllocate)
  {
    Decimal num1 = 0M;
    foreach (PXResult<PMProformaProgressLine, PMRevenueBudget> pxResult in ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Select(Array.Empty<object>()))
    {
      PMProformaProgressLine proformaProgressLine = PXResult<PMProformaProgressLine, PMRevenueBudget>.op_Implicit(pxResult);
      Decimal invoicedAmount = this.GetInvoicedAmount(PXResult<PMProformaProgressLine, PMRevenueBudget>.op_Implicit(pxResult));
      Decimal d = num1 + retainageToAllocate * invoicedAmount / totalInvoiceUptoDate;
      Decimal num2 = Decimal.Round(d, 2);
      num1 = d - num2;
      ((PXSelectBase) this.ProgressiveLines).Cache.SetValue<PMProformaLine.curyAllocatedRetainedAmount>((object) proformaProgressLine, (object) this.GetAmountInBillingCurrency(new Decimal?(num2)));
    }
    ((PXSelectBase) this.Document).Cache.SetValue<PMProforma.curyAllocatedRetainedTotal>((object) ((PXSelectBase<PMProforma>) this.Document).Current, (object) this.GetAmountInBillingCurrency(new Decimal?(retainageToAllocate)));
  }

  private Decimal GetInvoicedAmount(PMRevenueBudget budget)
  {
    Decimal? nullable1 = budget.CuryInvoicedAmount;
    Decimal num1 = nullable1.GetValueOrDefault() + this.GetCuryActualAmountWithTaxes(budget);
    nullable1 = budget.CuryAmountToInvoice;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    Decimal invoicedAmount = num1 + valueOrDefault1;
    foreach (PMBudgetAccum pmBudgetAccum in ((PXSelectBase) this.Budget).Cache.Inserted)
    {
      int? nullable2 = pmBudgetAccum.ProjectTaskID;
      int? projectTaskId = budget.ProjectTaskID;
      if (nullable2.GetValueOrDefault() == projectTaskId.GetValueOrDefault() & nullable2.HasValue == projectTaskId.HasValue)
      {
        int? accountGroupId = pmBudgetAccum.AccountGroupID;
        nullable2 = budget.AccountGroupID;
        if (accountGroupId.GetValueOrDefault() == nullable2.GetValueOrDefault() & accountGroupId.HasValue == nullable2.HasValue)
        {
          nullable2 = pmBudgetAccum.InventoryID;
          int? inventoryId = budget.InventoryID;
          if (nullable2.GetValueOrDefault() == inventoryId.GetValueOrDefault() & nullable2.HasValue == inventoryId.HasValue)
          {
            int? costCodeId = pmBudgetAccum.CostCodeID;
            nullable2 = budget.CostCodeID;
            if (costCodeId.GetValueOrDefault() == nullable2.GetValueOrDefault() & costCodeId.HasValue == nullable2.HasValue)
            {
              Decimal num2 = invoicedAmount;
              Decimal? nullable3 = pmBudgetAccum.CuryInvoicedAmount;
              Decimal valueOrDefault2 = nullable3.GetValueOrDefault();
              nullable3 = pmBudgetAccum.CuryAmountToInvoice;
              Decimal valueOrDefault3 = nullable3.GetValueOrDefault();
              Decimal num3 = valueOrDefault2 + valueOrDefault3;
              invoicedAmount = num2 + num3;
            }
          }
        }
      }
    }
    return invoicedAmount;
  }

  protected virtual Decimal GetCuryActualAmountWithTaxes(PMRevenueBudget row)
  {
    Decimal? nullable = row.CuryActualAmount;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = row.CuryInclTaxAmount;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    return valueOrDefault1 + valueOrDefault2;
  }

  protected virtual Decimal GetCuryActualAmountWithTaxes(PMProjectRevenueTotal row)
  {
    Decimal? nullable = row.CuryActualAmount;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = row.CuryInclTaxAmount;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    return valueOrDefault1 + valueOrDefault2;
  }

  private Decimal GetTotalInvoiced(PMProjectRevenueTotal budget)
  {
    Decimal totalInvoiced = this.GetCuryActualAmountWithTaxes(budget) + budget.CuryInvoicedAmount.GetValueOrDefault();
    foreach (PMBudgetAccum pmBudgetAccum in ((PXSelectBase) this.Budget).Cache.Inserted)
      totalInvoiced += pmBudgetAccum.CuryInvoicedAmount.GetValueOrDefault();
    return totalInvoiced;
  }

  protected virtual void RecalculateLineRetainage(PMProject project)
  {
    foreach (PXResult<PMProformaProgressLine, PMRevenueBudget> pxResult in ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Select(Array.Empty<object>()))
    {
      PMProformaProgressLine proformaProgressLine1 = PXResult<PMProformaProgressLine, PMRevenueBudget>.op_Implicit(pxResult);
      PMRevenueBudget budget = PXResult<PMProformaProgressLine, PMRevenueBudget>.op_Implicit(pxResult);
      Decimal? nullable1 = proformaProgressLine1.CuryLineTotal;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = proformaProgressLine1.RetainagePct;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      Decimal num1 = valueOrDefault1 * valueOrDefault2 * 0.01M;
      Decimal lineAmount = this.GetLineAmount(project, (PMBudget) budget);
      nullable1 = budget.RetainagePct;
      Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
      Decimal num2 = lineAmount * valueOrDefault3 * 0.01M;
      nullable1 = budget.RetainageMaxPct;
      Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
      Decimal num3 = num2 * valueOrDefault4 * 0.01M;
      Decimal num4 = num1;
      nullable1 = budget.CuryTotalRetainedAmount;
      Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
      if (num4 + valueOrDefault5 <= num3)
      {
        proformaProgressLine1.CuryRetainage = new Decimal?(num1);
      }
      else
      {
        PMProformaProgressLine proformaProgressLine2 = proformaProgressLine1;
        Decimal num5 = num3;
        nullable1 = budget.CuryTotalRetainedAmount;
        Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
        Decimal? nullable2 = new Decimal?(num5 - valueOrDefault6);
        proformaProgressLine2.CuryRetainage = nullable2;
      }
      ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Update(proformaProgressLine1);
    }
  }

  private Decimal GetBilledRetainageToDateTotal(DateTime? cutoffDate)
  {
    Decimal retainageToDateTotal = 0M;
    foreach (Decimal num in this.GetBilledRetainageToDate(cutoffDate).Values)
      retainageToDateTotal += num;
    return retainageToDateTotal;
  }

  public virtual Dictionary<ProformaEntry.ProformaTotalsCounter.AmountBaseKey, Decimal> GetBilledRetainageToDate(
    DateTime? cutoffDate)
  {
    Dictionary<ProformaEntry.ProformaTotalsCounter.AmountBaseKey, Decimal> billedRetainageToDate = new Dictionary<ProformaEntry.ProformaTotalsCounter.AmountBaseKey, Decimal>();
    foreach (PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARInvoice, ProformaEntry.RetainageOriginalARTran, PX.Objects.GL.Account> pxResult in ((PXSelectBase<PX.Objects.AR.ARTran>) new PXSelectJoinGroupBy<PX.Objects.AR.ARTran, InnerJoin<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARTran.tranType, Equal<PX.Objects.AR.ARInvoice.docType>, And<PX.Objects.AR.ARTran.refNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>>>, InnerJoin<ProformaEntry.RetainageOriginalARTran, On<PX.Objects.AR.ARTran.origDocType, Equal<ProformaEntry.RetainageOriginalARTran.tranType>, And<PX.Objects.AR.ARTran.origRefNbr, Equal<ProformaEntry.RetainageOriginalARTran.refNbr>, And<PX.Objects.AR.ARTran.origLineNbr, Equal<ProformaEntry.RetainageOriginalARTran.lineNbr>>>>, InnerJoin<PX.Objects.GL.Account, On<ProformaEntry.RetainageOriginalARTran.accountID, Equal<PX.Objects.GL.Account.accountID>>>>>, Where<PX.Objects.AR.ARInvoice.isRetainageDocument, Equal<True>, And<PX.Objects.AR.ARInvoice.released, Equal<True>, And<PX.Objects.AR.ARInvoice.paymentsByLinesAllowed, Equal<True>, And<PX.Objects.AR.ARInvoice.docDate, LessEqual<Required<PX.Objects.AR.ARInvoice.docDate>>, And<PX.Objects.AR.ARInvoice.projectID, Equal<Current<PMProforma.projectID>>>>>>>, Aggregate<GroupBy<PX.Objects.AR.ARTran.tranType, GroupBy<PX.Objects.AR.ARTran.taskID, GroupBy<PX.Objects.AR.ARTran.costCodeID, GroupBy<PX.Objects.AR.ARTran.inventoryID, GroupBy<PX.Objects.GL.Account.accountGroupID, Sum<PX.Objects.AR.ARTran.curyTranAmt>>>>>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) cutoffDate
    }))
    {
      PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARInvoice, ProformaEntry.RetainageOriginalARTran, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARInvoice, ProformaEntry.RetainageOriginalARTran, PX.Objects.GL.Account>.op_Implicit(pxResult);
      int? nullable = arTran.TaskID;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = arTran.CostCodeID;
      int costCodeID = nullable ?? CostCodeAttribute.DefaultCostCode.GetValueOrDefault();
      nullable = arTran.InventoryID;
      int inventoryID = nullable ?? PMInventorySelectorAttribute.EmptyInventoryID;
      nullable = account.AccountGroupID;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      ProformaEntry.ProformaTotalsCounter.AmountBaseKey key = new ProformaEntry.ProformaTotalsCounter.AmountBaseKey(valueOrDefault1, costCodeID, inventoryID, valueOrDefault2);
      Decimal num = arTran.CuryTranAmt.GetValueOrDefault() * (ARDocType.SignAmount(arTran.TranType) ?? 1M);
      if (billedRetainageToDate.ContainsKey(key))
        billedRetainageToDate[key] += num;
      else
        billedRetainageToDate.Add(key, num);
    }
    PXSelectGroupBy<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.isRetainageDocument, Equal<True>, And<PX.Objects.AR.ARInvoice.released, Equal<True>, And<PX.Objects.AR.ARInvoice.paymentsByLinesAllowed, Equal<False>, And<PX.Objects.AR.ARInvoice.docDate, LessEqual<Required<PX.Objects.AR.ARInvoice.docDate>>, And<PX.Objects.AR.ARInvoice.projectID, Equal<Current<PMProforma.projectID>>>>>>>, Aggregate<GroupBy<PX.Objects.AR.ARInvoice.docType, Sum<PX.Objects.AR.ARInvoice.curyLineTotal>>>> pxSelectGroupBy = new PXSelectGroupBy<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.isRetainageDocument, Equal<True>, And<PX.Objects.AR.ARInvoice.released, Equal<True>, And<PX.Objects.AR.ARInvoice.paymentsByLinesAllowed, Equal<False>, And<PX.Objects.AR.ARInvoice.docDate, LessEqual<Required<PX.Objects.AR.ARInvoice.docDate>>, And<PX.Objects.AR.ARInvoice.projectID, Equal<Current<PMProforma.projectID>>>>>>>, Aggregate<GroupBy<PX.Objects.AR.ARInvoice.docType, Sum<PX.Objects.AR.ARInvoice.curyLineTotal>>>>((PXGraph) this);
    Decimal? nullable1 = new Decimal?();
    object[] objArray = new object[1]{ (object) cutoffDate };
    foreach (PXResult<PX.Objects.AR.ARInvoice> pxResult in ((PXSelectBase<PX.Objects.AR.ARInvoice>) pxSelectGroupBy).Select(objArray))
    {
      PX.Objects.AR.ARInvoice arInvoice = PXResult<PX.Objects.AR.ARInvoice>.op_Implicit(pxResult);
      Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
      Decimal? nullable2 = arInvoice.CuryLineTotal;
      Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
      nullable2 = ARDocType.SignAmount(arInvoice.DocType);
      Decimal num1 = nullable2 ?? 1M;
      Decimal num2 = valueOrDefault4 * num1;
      nullable1 = new Decimal?(valueOrDefault3 + num2);
    }
    foreach (PXResult<PX.Objects.AR.ARTran> pxResult in PXSelectBase<PX.Objects.AR.ARTran, PXViewOf<PX.Objects.AR.ARTran>.BasedOn<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.ARInvoice>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.docType, Equal<PX.Objects.AR.ARTran.tranType>>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.refNbr, IBqlString>.IsEqual<PX.Objects.AR.ARTran.refNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.isRetainageDocument, Equal<True>>>>, And<BqlOperand<PX.Objects.AR.ARInvoice.released, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PX.Objects.AR.ARInvoice.paymentsByLinesAllowed, IBqlBool>.IsEqual<True>>>, And<Where<PX.Objects.AR.ARTran.origLineNbr, Equal<Zero>>>>, And<BqlOperand<PX.Objects.AR.ARInvoice.docDate, IBqlDateTime>.IsLessEqual<P.AsDateTime>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.projectID, IBqlInt>.IsEqual<BqlField<PMProforma.projectID, IBqlInt>.FromCurrent>>>.Aggregate<To<GroupBy<PX.Objects.AR.ARTran.tranType>, GroupBy<PX.Objects.AR.ARTran.taskID>, GroupBy<PX.Objects.AR.ARTran.costCodeID>, GroupBy<PX.Objects.AR.ARTran.inventoryID>, Sum<PX.Objects.AR.ARTran.curyTranAmt>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) cutoffDate
    }))
    {
      PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran>.op_Implicit(pxResult);
      Decimal num = ARDocType.SignAmount(arTran.TranType) ?? 1M;
      nullable1 = new Decimal?(nullable1.GetValueOrDefault() + num * arTran.CuryTranAmt.GetValueOrDefault());
    }
    if (nullable1.HasValue && nullable1.GetValueOrDefault() != 0M)
      billedRetainageToDate.Add(this.PayByLineOffKey, nullable1.Value);
    return billedRetainageToDate;
  }

  protected virtual Decimal GetContractAmount(PMProject project, PMProjectRevenueTotal budget)
  {
    return project.IncludeCO.GetValueOrDefault() ? budget.CuryRevisedAmount.GetValueOrDefault() : budget.CuryAmount.GetValueOrDefault();
  }

  protected virtual Decimal GetLineAmount(PMProject project, PMBudget budget)
  {
    return project.IncludeCO.GetValueOrDefault() ? budget.CuryRevisedAmount.GetValueOrDefault() : budget.CuryAmount.GetValueOrDefault();
  }

  public virtual void MergeLines()
  {
    PMProject current = ((PXSelectBase<PMProject>) this.Project).Current;
    ProjectBalanceValidationProcess.BudgetServiceMassUpdate serviceMassUpdate = new ProjectBalanceValidationProcess.BudgetServiceMassUpdate((PXGraph) this, current);
    Dictionary<BudgetKeyTuple, ProformaEntry.PendingChange> progessiveLinesLookup = this.GetProgessiveLinesLookup();
    PXSelect<PMTax, Where<PMTax.refNbr, Equal<Current<PMProforma.refNbr>>, And<PMTax.revisionID, Equal<Current<PMProforma.revisionID>>>>> pxSelect = new PXSelect<PMTax, Where<PMTax.refNbr, Equal<Current<PMProforma.refNbr>>, And<PMTax.revisionID, Equal<Current<PMProforma.revisionID>>>>>((PXGraph) this);
    Dictionary<int, List<PMTax>> dictionary1 = new Dictionary<int, List<PMTax>>();
    object[] objArray = Array.Empty<object>();
    foreach (PXResult<PMTax> pxResult in ((PXSelectBase<PMTax>) pxSelect).Select(objArray))
    {
      PMTax pmTax = PXResult<PMTax>.op_Implicit(pxResult);
      List<PMTax> pmTaxList;
      if (!dictionary1.TryGetValue(pmTax.LineNbr.Value, out pmTaxList))
      {
        pmTaxList = new List<PMTax>();
        dictionary1.Add(pmTax.LineNbr.Value, pmTaxList);
      }
      pmTaxList.Add(pmTax);
    }
    PXFilterRow[] externalFilters = ((PXSelectBase) this.TransactionLines).View.GetExternalFilters();
    int startRow = PXView.StartRow;
    int num = 0;
    foreach (PMProformaTransactLine proformaTransactLine in GraphHelper.RowCast<PMProformaTransactLine>((IEnumerable) ((PXSelectBase) this.TransactionLines).View.Select(PXView.Currents, new object[1]
    {
      (object) ((PXSelectBase<PMProforma>) this.Document).Current
    }, PXView.Searches, PXView.SortColumns, PXView.Descendings, externalFilters, ref startRow, PXView.MaximumRows, ref num)))
    {
      int? nullable1 = proformaTransactLine.RevenueTaskID;
      if (nullable1.HasValue && proformaTransactLine.Option != null)
      {
        bool? merged = proformaTransactLine.Merged;
        string mergeError = !merged.GetValueOrDefault() ? this.GetMergeError(proformaTransactLine) : (string) null;
        if (mergeError == null)
        {
          int? projectedAccountGroup = this.GetProjectedAccountGroup((PMProformaLine) proformaTransactLine);
          if (projectedAccountGroup.HasValue)
          {
            bool isExisting;
            PX.Objects.PM.Lite.PMBudget budget = serviceMassUpdate.SelectProjectBalance(PMAccountGroup.PK.Find((PXGraph) this, projectedAccountGroup), current, proformaTransactLine.RevenueTaskID, proformaTransactLine.InventoryID, proformaTransactLine.CostCodeID, out isExisting);
            ProformaEntry.PendingChange pendingChange1;
            if (isExisting && progessiveLinesLookup.TryGetValue(BudgetKeyTuple.Create((IProjectFilter) budget), out pendingChange1))
            {
              ProformaEntry.PendingChange pendingChange2 = pendingChange1;
              Decimal amount = pendingChange2.Amount;
              merged = proformaTransactLine.Merged;
              Decimal? nullable2;
              Decimal valueOrDefault;
              if (merged.GetValueOrDefault())
              {
                nullable2 = proformaTransactLine.CuryMergedAmount;
                valueOrDefault = nullable2.GetValueOrDefault();
              }
              else
              {
                nullable2 = proformaTransactLine.CuryLineTotal;
                valueOrDefault = nullable2.GetValueOrDefault();
              }
              pendingChange2.Amount = amount + valueOrDefault;
              merged = proformaTransactLine.Merged;
              if (!merged.GetValueOrDefault())
              {
                TaxBaseAttribute.SetTaxCalc<PMProformaTransactLine.taxCategoryID>(((PXSelectBase) this.TransactionLines).Cache, (object) proformaTransactLine, TaxCalc.NoCalc);
                proformaTransactLine.CuryMergedAmount = proformaTransactLine.CuryLineTotal;
                proformaTransactLine.CuryLineTotal = new Decimal?(0M);
                proformaTransactLine.MergedToLineNbr = pendingChange1.Line.LineNbr;
                ((PXSelectBase<PMProformaTransactLine>) this.TransactionLines).Update(proformaTransactLine).Merged = new bool?(true);
                Dictionary<int, List<PMTax>> dictionary2 = dictionary1;
                nullable1 = proformaTransactLine.LineNbr;
                int key = nullable1.Value;
                List<PMTax> pmTaxList;
                ref List<PMTax> local = ref pmTaxList;
                if (dictionary2.TryGetValue(key, out local))
                {
                  foreach (PMTax pmTax in pmTaxList)
                    ((PXSelectBase<PMTax>) this.Tax_Rows).Delete(pmTax);
                }
                TaxBaseAttribute.SetTaxCalc<PMProformaTransactLine.taxCategoryID>(((PXSelectBase) this.TransactionLines).Cache, (object) proformaTransactLine, TaxCalc.Calc);
              }
            }
          }
        }
        else
          PXUIFieldAttribute.SetError<PMProformaLine.merged>(((PXSelectBase) this.TransactionLines).Cache, (object) proformaTransactLine, mergeError);
      }
    }
    foreach (ProformaEntry.PendingChange pendingChange in progessiveLinesLookup.Values)
    {
      if (pendingChange.Amount != 0M)
      {
        pendingChange.Line.CuryTimeMaterialAmount = new Decimal?(pendingChange.Amount);
        ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Update(pendingChange.Line);
      }
    }
  }

  protected virtual void MergeLine(PMProformaTransactLine line)
  {
    PMProformaProgressLine targetProgressLine = this.GetTargetProgressLine(line);
    if (targetProgressLine == null)
      return;
    line.CuryMergedAmount = line.CuryLineTotal;
    line.MergedToLineNbr = targetProgressLine.LineNbr;
    ((PXSelectBase) this.TransactionLines).Cache.SetValueExt<PMProformaTransactLine.curyLineTotal>((object) line, (object) 0M);
    PMProformaProgressLine proformaProgressLine = targetProgressLine;
    Decimal? timeMaterialAmount = proformaProgressLine.CuryTimeMaterialAmount;
    Decimal valueOrDefault = line.CuryMergedAmount.GetValueOrDefault();
    proformaProgressLine.CuryTimeMaterialAmount = timeMaterialAmount.HasValue ? new Decimal?(timeMaterialAmount.GetValueOrDefault() + valueOrDefault) : new Decimal?();
    ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Update(targetProgressLine);
  }

  public virtual void UnmergeLines()
  {
    PXFilterRow[] externalFilters = ((PXSelectBase) this.TransactionLines).View.GetExternalFilters();
    int startRow = PXView.StartRow;
    int num = 0;
    foreach (PMProformaTransactLine proformaTransactLine in GraphHelper.RowCast<PMProformaTransactLine>((IEnumerable) ((PXSelectBase) this.TransactionLines).View.Select(PXView.Currents, new object[1]
    {
      (object) ((PXSelectBase<PMProforma>) this.Document).Current
    }, PXView.Searches, PXView.SortColumns, PXView.Descendings, externalFilters, ref startRow, PXView.MaximumRows, ref num)))
    {
      if (proformaTransactLine.Merged.GetValueOrDefault())
      {
        PMProformaTransactLine copy = PXCache<PMProformaTransactLine>.CreateCopy(proformaTransactLine);
        copy.Merged = new bool?(false);
        ((PXSelectBase<PMProformaTransactLine>) this.TransactionLines).Update(copy);
      }
    }
  }

  public virtual void FillEmptyRevenueTasks()
  {
    foreach (PXResult<PMProformaTransactLine> pxResult in ((PXSelectBase<PMProformaTransactLine>) this.TransactionLines).Select(Array.Empty<object>()))
    {
      PMProformaTransactLine line = PXResult<PMProformaTransactLine>.op_Implicit(pxResult);
      int? nullable = line.OrigAccountGroupID;
      if (nullable.HasValue)
      {
        nullable = line.RevenueTaskID;
        if (!nullable.HasValue)
        {
          int? defaultRevenueTask = this.GetDefaultRevenueTask(line);
          if (defaultRevenueTask.HasValue)
          {
            line.RevenueTaskID = defaultRevenueTask;
            ((PXSelectBase<PMProformaTransactLine>) this.TransactionLines).Update(line);
          }
        }
      }
    }
    ((PXSelectBase) this.TransactionLines).View.RequestRefresh();
  }

  /// <summary>
  /// Unmerge transaction line from corresponding progress line.
  /// </summary>
  /// <param name="line">transaction line</param>
  /// <param name="skipUpdateEvent">Set to true if this method is called from a field event - The update event will be called automatically.</param>
  protected virtual void UnmergeLine(PMProformaTransactLine line, bool skipUpdateEvent)
  {
    PMProformaProgressLine proformaProgressLine1 = ((PXSelectBase<PMProformaProgressLine>) new PXSelect<PMProformaProgressLine, Where<PMProformaProgressLine.refNbr, Equal<Required<PMProformaTransactLine.refNbr>>, And<PMProformaProgressLine.revisionID, Equal<Required<PMProformaTransactLine.revisionID>>, And<PMProformaProgressLine.lineNbr, Equal<Required<PMProformaLine.mergedToLineNbr>>>>>>((PXGraph) this)).SelectSingle(new object[3]
    {
      (object) line.RefNbr,
      (object) line.RevisionID,
      (object) line.MergedToLineNbr
    });
    bool flag = false;
    if (proformaProgressLine1 == null)
    {
      foreach (PMProformaProgressLine proformaProgressLine2 in ((PXSelectBase) this.ProgressiveLines).Cache.Deleted)
      {
        if (line.RefNbr == proformaProgressLine2.RefNbr)
        {
          int? nullable1 = line.RevisionID;
          int? nullable2 = proformaProgressLine2.RevisionID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = line.MergedToLineNbr;
            nullable1 = proformaProgressLine2.LineNbr;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            {
              flag = true;
              proformaProgressLine1 = proformaProgressLine2;
              break;
            }
          }
        }
      }
    }
    if (proformaProgressLine1 == null)
      return;
    if (!flag)
    {
      PMProformaProgressLine proformaProgressLine3 = proformaProgressLine1;
      Decimal? timeMaterialAmount = proformaProgressLine3.CuryTimeMaterialAmount;
      Decimal? curyMergedAmount = line.CuryMergedAmount;
      proformaProgressLine3.CuryTimeMaterialAmount = timeMaterialAmount.HasValue & curyMergedAmount.HasValue ? new Decimal?(timeMaterialAmount.GetValueOrDefault() - curyMergedAmount.GetValueOrDefault()) : new Decimal?();
      ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Update(proformaProgressLine1);
    }
    if (skipUpdateEvent)
    {
      ((PXSelectBase) this.TransactionLines).Cache.SetValueExt<PMProformaTransactLine.curyLineTotal>((object) line, (object) line.CuryMergedAmount);
    }
    else
    {
      line.CuryLineTotal = line.CuryMergedAmount;
      if (((PXSelectBase) this.TransactionLines).Cache.GetStatus((object) line) != 3)
        line = ((PXSelectBase<PMProformaTransactLine>) this.TransactionLines).Update(line);
    }
    line.CuryMergedAmount = new Decimal?(0M);
    line.MergedToLineNbr = new int?();
    line.Merged = new bool?(false);
  }

  protected virtual string GetMergeError(PMProformaTransactLine line)
  {
    PMProformaProgressLine targetProgressLine = this.GetTargetProgressLine(line);
    if (targetProgressLine == null)
      return "The line cannot be included because there is no corresponding progress billing line.";
    if (targetProgressLine.ProgressBillingBase == "Q")
      return "The lines cannot be included in progress billing lines billed by quantity.";
    if (line.Option == "U")
      return "The lines with the Hold Remainder status cannot be included in progress billing lines.";
    Decimal? curyLineTotal = line.CuryLineTotal;
    Decimal num = 0M;
    if (curyLineTotal.GetValueOrDefault() < num & curyLineTotal.HasValue)
      return "The line cannot be included because it has a negative value in the Amount to Invoice box.";
    List<PMTran> pmTranList;
    if (this.GetReferencedTransactions().TryGetValue(line.LineNbr.Value, out pmTranList))
    {
      foreach (PMTran pmTran in pmTranList)
      {
        if (pmTran.Reverse == "I")
          return "The line cannot be included because the corresponding transaction uses an unsupported configuration of allocation reversal.";
        if (line.Option == "X" && pmTran.RemainderOfTranID.HasValue)
          return "The line with the Write Off status cannot be included because it is based on the unbilled remainder transaction.";
      }
    }
    return (string) null;
  }

  private PMProformaProgressLine GetTargetProgressLine(PMProformaTransactLine line)
  {
    PMProject current = ((PXSelectBase<PMProject>) this.Project).Current;
    BudgetService budgetService = new BudgetService((PXGraph) this);
    Dictionary<BudgetKeyTuple, ProformaEntry.PendingChange> progessiveLinesLookup = this.GetProgessiveLinesLookup();
    if (line.RevenueTaskID.HasValue && line.Option != null)
    {
      int? projectedAccountGroup = this.GetProjectedAccountGroup((PMProformaLine) line);
      if (projectedAccountGroup.HasValue)
      {
        PMAccountGroup ag = PMAccountGroup.PK.Find((PXGraph) this, projectedAccountGroup);
        bool isExisting;
        PX.Objects.PM.Lite.PMBudget budget = budgetService.SelectProjectBalance(ag, current, line.RevenueTaskID, line.InventoryID, line.CostCodeID, out isExisting);
        ProformaEntry.PendingChange pendingChange;
        if (isExisting && progessiveLinesLookup.TryGetValue(BudgetKeyTuple.Create((IProjectFilter) budget), out pendingChange))
          return pendingChange.Line;
      }
    }
    return (PMProformaProgressLine) null;
  }

  private Dictionary<BudgetKeyTuple, ProformaEntry.PendingChange> GetProgessiveLinesLookup()
  {
    if (this.progressiveLinesLookup == null)
    {
      this.progressiveLinesLookup = new Dictionary<BudgetKeyTuple, ProformaEntry.PendingChange>();
      foreach (PXResult<PMProformaProgressLine, PMRevenueBudget> pxResult in ((PXSelectBase<PMProformaProgressLine>) this.ProgressiveLines).Select(Array.Empty<object>()))
      {
        PMRevenueBudget budget = PXResult<PMProformaProgressLine, PMRevenueBudget>.op_Implicit(pxResult);
        PMProformaProgressLine proformaProgressLine = PXResult<PMProformaProgressLine, PMRevenueBudget>.op_Implicit(pxResult);
        if (budget.TaskID.HasValue)
          this.progressiveLinesLookup.Add(BudgetKeyTuple.Create((IProjectFilter) budget), new ProformaEntry.PendingChange()
          {
            Line = proformaProgressLine
          });
      }
    }
    return this.progressiveLinesLookup;
  }

  private int? GetDefaultRevenueTask(PMProformaTransactLine line)
  {
    if (line != null)
    {
      if (line.OrigAccountGroupID.HasValue)
      {
        bool isExisting;
        PX.Objects.PM.Lite.PMBudget pmBudget1 = new BudgetService((PXGraph) this).SelectProjectBalance(PMAccountGroup.PK.Find((PXGraph) this, line.OrigAccountGroupID), PMProject.PK.Find((PXGraph) this, line.ProjectID), line.TaskID, line.InventoryID, line.CostCodeID, out isExisting);
        if (isExisting)
        {
          if (pmBudget1.RevenueTaskID.HasValue)
            return pmBudget1.RevenueTaskID;
          PX.Objects.PM.Lite.PMBudget pmBudget2 = (PX.Objects.PM.Lite.PMBudget) ((PXSelectBase) this.BudgetProperties).Cache.Locate((object) pmBudget1);
          if (pmBudget2 != null && pmBudget2.RevenueTaskID.HasValue)
            return pmBudget2.RevenueTaskID;
        }
      }
      PMTask pmTask = PMTask.PK.Find((PXGraph) this, line.ProjectID, line.TaskID);
      if (pmTask != null && pmTask.Type == "CostRev")
        return pmTask.TaskID;
    }
    return new int?();
  }

  private bool BillingInAnotherCurrency
  {
    get
    {
      return ((PXSelectBase<PMProforma>) this.Document).Current != null && ((PXSelectBase<PMProforma>) this.Document).Current.ProjectID.HasValue && ((PXSelectBase<PMProject>) this.Project).Current.CuryID != ((PXSelectBase<PMProject>) this.Project).Current.BillingCuryID;
    }
  }

  public virtual void CheckMigrationMode()
  {
    if (((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.MigrationMode.GetValueOrDefault())
      throw new PXException("The operation is not available because the migration mode is enabled for accounts receivable.");
  }

  private void Unbill(PMTran tran)
  {
    if (tran.Billed.GetValueOrDefault())
    {
      tran.Billed = new bool?(false);
      tran.BilledDate = new DateTime?();
      tran.BillingID = (string) null;
      tran.Selected = new bool?(false);
      RegisterReleaseProcess.AddToUnbilledSummary((PXGraph) this, tran);
    }
    ((PXSelectBase<PMTran>) this.Details).Update(tran);
  }

  private void ClearProformaReference(PMTran tran)
  {
    tran.ProformaRefNbr = (string) null;
    tran.ProformaLineNbr = new int?();
  }

  public bool RecalculateExternalTaxesSync { get; set; }

  public virtual bool IsExternalTax(string taxZoneID) => false;

  public virtual PMProforma CalculateExternalTax(PMProforma doc) => doc;

  public class MultiCurrency : MultiCurrencyGraph<ProformaEntry, PMProforma>
  {
    protected override string Module => "PM";

    protected override MultiCurrencyGraph<ProformaEntry, PMProforma>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<ProformaEntry, PMProforma>.CurySourceMapping(typeof (PX.Objects.AR.Customer));
    }

    protected override CurySource CurrentSourceSelect()
    {
      CurySource curySource = base.CurrentSourceSelect();
      if (curySource != null)
        curySource.AllowOverrideCury = curySource.AllowOverrideRate;
      return curySource;
    }

    protected override MultiCurrencyGraph<ProformaEntry, PMProforma>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<ProformaEntry, PMProforma>.DocumentMapping(typeof (PMProforma))
      {
        BAccountID = typeof (PMProforma.customerID),
        DocumentDate = typeof (PMProforma.invoiceDate),
        CuryID = typeof (PMProforma.curyID)
      };
    }

    protected override bool AllowOverrideCury()
    {
      return this.Base.CanEditDocument(((PXSelectBase<PMProforma>) this.Base.Document).Current);
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[6]
      {
        (PXSelectBase) this.Base.Document,
        (PXSelectBase) this.Base.ProgressiveLines,
        (PXSelectBase) this.Base.TransactionLines,
        (PXSelectBase) this.Base.Overflow,
        (PXSelectBase) this.Base.Taxes,
        (PXSelectBase) this.Base.Tax_Rows
      };
    }

    protected override PXSelectBase[] GetTrackedExceptChildren()
    {
      return new PXSelectBase[1]
      {
        (PXSelectBase) this.Base.dummyRevenueBudget
      };
    }

    protected override void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyID> e)
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      {
        if (string.IsNullOrEmpty(((PXSelectBase<PMProject>) this.Base.Project).Current?.BillingCuryID))
          return;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyID>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue = (object) ((PXSelectBase<PMProject>) this.Base.Project).Current.BillingCuryID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyID>>) e).Cancel = true;
      }
      else
        base._(e);
    }

    protected override void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID> e)
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() && !string.IsNullOrEmpty(((PXSelectBase<PMProject>) this.Base.Project).Current?.RateTypeID))
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue = (object) ((PXSelectBase<PMProject>) this.Base.Project).Current.RateTypeID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>>) e).Cancel = true;
      }
      else
        base._(e);
    }

    protected override void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate> e)
    {
      if (((PXSelectBase) this.Base.Document).Cache.Current == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue = (object) ((PMProforma) ((PXSelectBase) this.Base.Document).Cache.Current).InvoiceDate;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>>) e).Cancel = true;
    }

    protected virtual void _(PX.Data.Events.RowInserting<PMProformaTransactLine> e)
    {
      this.recalculateRowBaseValues(((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<PMProformaTransactLine>>) e).Cache, (object) e.Row, (IEnumerable<CuryField>) this.TrackedItems[((PXSelectBase) this.Base.TransactionLines).Cache.GetItemType()]);
    }

    protected virtual void _(PX.Data.Events.RowInserting<PMProformaProgressLine> e)
    {
      this.recalculateRowBaseValues(((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<PMProformaProgressLine>>) e).Cache, (object) e.Row, (IEnumerable<CuryField>) this.TrackedItems[((PXSelectBase) this.Base.ProgressiveLines).Cache.GetItemType()]);
    }

    protected override void _(PX.Data.Events.FieldVerifying<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.curyID> e)
    {
      this.ThrowIfCuryIDCannotBeChangedDueTo((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.curyID>, PX.Objects.Extensions.MultiCurrency.Document, object>) e).NewValue);
      base._(e);
    }

    private void ThrowIfCuryIDCannotBeChangedDueTo(string newValue)
    {
      if (((PXSelectBase<PMProject>) this.Base.Project).Current != null && !(((PXSelectBase<PMProject>) this.Base.Project).Current.BillingCuryID == newValue))
        throw new PXSetPropertyException("An invoice can be created for the project only in the billing currency of the project ({0}).", new object[1]
        {
          (object) ((PXSelectBase<PMProject>) this.Base.Project).Current.BillingCuryID
        })
        {
          ErrorValue = (object) newValue
        };
    }
  }

  public class ProformaEntry_ActivityDetailsExt : 
    PMActivityDetailsExt<ProformaEntry, PMProforma, PMProforma.noteID>
  {
    public override System.Type GetBAccountIDCommand()
    {
      return typeof (Select<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PMProforma.customerID>>>>);
    }

    public override System.Type GetEmailMessageTarget()
    {
      return typeof (Select2<PX.Objects.CR.Contact, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<PX.Objects.AR.Customer.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PMProforma.customerID>>>>);
    }
  }

  private class PendingChange
  {
    public PMProformaProgressLine Line;
    public Decimal Amount;
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class PMProformaOverflow : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBLong]
    [CurrencyInfo(typeof (PMProforma.curyInfoID))]
    public virtual long? CuryInfoID { get; set; }

    [PXCurrency(typeof (ProformaEntry.PMProformaOverflow.curyInfoID), typeof (ProformaEntry.PMProformaOverflow.overflowTotal), BaseCalc = false)]
    [PXUnboundDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Over-Limit Total", Enabled = false, Visible = false)]
    public virtual Decimal? CuryOverflowTotal { get; set; }

    [PXBaseCury]
    [PXUnboundDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Overflow Total in Base Currency", Enabled = false, Visible = false)]
    public virtual Decimal? OverflowTotal { get; set; }

    public abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      ProformaEntry.PMProformaOverflow.curyInfoID>
    {
    }

    public abstract class curyOverflowTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ProformaEntry.PMProformaOverflow.curyOverflowTotal>
    {
    }

    public abstract class overflowTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ProformaEntry.PMProformaOverflow.overflowTotal>
    {
    }
  }

  public class ProformaTotalsCounter
  {
    private Dictionary<ProformaEntry.ProformaTotalsCounter.AmountBaseKey, ProformaEntry.ProformaTotalsCounter.AmountBaseTotals> AmtBaseTotals;
    private Dictionary<ProformaEntry.ProformaTotalsCounter.QuantityBaseKey, ProformaEntry.ProformaTotalsCounter.QuantityBaseTotals> QtyBaseTotals;
    private string LastTotalsKey;

    private string[] GetEarlyProformaRefNbrs(PXGraph graph)
    {
      return ProformaEntry.ProformaTotalsCounter.GetEarlyProformaRefNbrs(graph, graph.Caches[typeof (PMProforma)]?.Current as PMProforma);
    }

    public static string[] GetEarlyProformaRefNbrs(PXGraph graph, PMProforma proforma)
    {
      if (proforma == null)
        return Array.Empty<string>();
      return graph.Select<PMProforma>().Where<PMProforma>((Expression<Func<PMProforma, bool>>) (x => x.ProjectID == proforma.ProjectID && x.Corrected == (bool?) false && x.RefNbr != proforma.RefNbr)).Select<PMProforma, string>((Expression<Func<PMProforma, string>>) (x => x.RefNbr)).AsEnumerable<string>().Distinct<string>().Where<string>((Func<string, bool>) (x => ProformaEntry.ProformaTotalsCounter.CompareRefNbrs(x, proforma.RefNbr) < 0)).ToArray<string>();
    }

    private static int CompareRefNbrs(string refNbr1, string refNbr2)
    {
      string significantPart1 = ProformaEntry.ProformaTotalsCounter.GetSignificantPart(refNbr1);
      string significantPart2 = ProformaEntry.ProformaTotalsCounter.GetSignificantPart(refNbr2);
      if (significantPart1.Length == significantPart2.Length)
        return string.Compare(significantPart1, significantPart2, StringComparison.OrdinalIgnoreCase);
      return significantPart1.Length >= significantPart2.Length ? 1 : -1;
    }

    private static string GetSignificantPart(string refNbr)
    {
      if (string.IsNullOrWhiteSpace(refNbr))
        return string.Empty;
      int num = 0;
      int length = refNbr.Length;
      while (num < length && (!char.IsDigit(refNbr[num]) || refNbr[num] == '0'))
        ++num;
      return num == length ? string.Empty : refNbr.Substring(num);
    }

    public ProformaEntry.ProformaTotalsCounter.QuantityBaseTotals GetQuantityBaseTotals(
      PXGraph graph,
      string proformaRefNbr,
      PMProformaProgressLine progressLine)
    {
      if (this.QtyBaseTotals == null || proformaRefNbr != this.LastTotalsKey)
      {
        this.LastTotalsKey = proformaRefNbr;
        this.QtyBaseTotals = GraphHelper.RowCast<PMProformaLine>((IEnumerable) ((PXSelectBase<PMProformaLine>) new PXSelectJoinGroupBy<PMProformaLine, InnerJoin<PMProforma, On<PMProformaLine.refNbr, Equal<PMProforma.refNbr>, And<PMProformaLine.revisionID, Equal<PMProforma.revisionID>, And<PMProforma.curyID, Equal<Current<PMProforma.curyID>>>>>>, Where<PMProformaLine.refNbr, In<Required<PMProforma.refNbr>>, And<PMProformaLine.projectID, Equal<Current<PMProforma.projectID>>, And<PMProformaLine.type, Equal<PMProformaLineType.progressive>, And<PMProformaLine.corrected, NotEqual<True>>>>>, Aggregate<GroupBy<PMProformaLine.taskID, GroupBy<PMProformaLine.costCodeID, GroupBy<PMProformaLine.inventoryID, GroupBy<PMProformaLine.accountGroupID, GroupBy<PMProformaLine.uOM, Sum<PMProformaLine.curyRetainage, Sum<PMProformaLine.retainage, Sum<PMProformaLine.curyLineTotal, Sum<PMProformaLine.lineTotal, Sum<PMProformaLine.qty>>>>>>>>>>>>(graph)).Select(new object[1]
        {
          (object) this.GetEarlyProformaRefNbrs(graph)
        })).Select<PMProformaLine, ProformaEntry.ProformaTotalsCounter.QuantityBaseTotals>((Func<PMProformaLine, ProformaEntry.ProformaTotalsCounter.QuantityBaseTotals>) (line => new ProformaEntry.ProformaTotalsCounter.QuantityBaseTotals()
        {
          Key = new ProformaEntry.ProformaTotalsCounter.QuantityBaseKey(line),
          CuryRetainage = line.CuryRetainage.GetValueOrDefault(),
          Retainage = line.Retainage.GetValueOrDefault(),
          CuryLineTotal = line.CuryLineTotal.GetValueOrDefault(),
          LineTotal = line.LineTotal.GetValueOrDefault(),
          QuantityTotal = line.Qty.GetValueOrDefault()
        })).ToDictionary<ProformaEntry.ProformaTotalsCounter.QuantityBaseTotals, ProformaEntry.ProformaTotalsCounter.QuantityBaseKey>((Func<ProformaEntry.ProformaTotalsCounter.QuantityBaseTotals, ProformaEntry.ProformaTotalsCounter.QuantityBaseKey>) (t => t.Key));
      }
      ProformaEntry.ProformaTotalsCounter.QuantityBaseTotals quantityBaseTotals;
      this.QtyBaseTotals.TryGetValue(new ProformaEntry.ProformaTotalsCounter.QuantityBaseKey((PMProformaLine) progressLine), out quantityBaseTotals);
      return quantityBaseTotals;
    }

    public virtual ProformaEntry.ProformaTotalsCounter.AmountBaseTotals GetAmountBaseTotals(
      PXGraph graph,
      string proformaRefNbr,
      PMProformaProgressLine progressLine)
    {
      if (this.AmtBaseTotals == null || proformaRefNbr != this.LastTotalsKey)
      {
        this.LastTotalsKey = proformaRefNbr;
        this.AmtBaseTotals = new Dictionary<ProformaEntry.ProformaTotalsCounter.AmountBaseKey, ProformaEntry.ProformaTotalsCounter.AmountBaseTotals>();
        foreach (PXResult<PMProformaLine> pxResult in ((PXSelectBase<PMProformaLine>) new PXSelectJoinGroupBy<PMProformaLine, InnerJoin<PMProforma, On<PMProformaLine.refNbr, Equal<PMProforma.refNbr>, And<PMProformaLine.revisionID, Equal<PMProforma.revisionID>, And<PMProforma.curyID, Equal<Current<PMProforma.curyID>>>>>>, Where<PMProformaLine.refNbr, In<Required<PMProforma.refNbr>>, And<PMProformaLine.projectID, Equal<Current<PMProforma.projectID>>, And<PMProformaLine.type, Equal<PMProformaLineType.progressive>, And<PMProformaLine.corrected, NotEqual<True>>>>>, Aggregate<GroupBy<PMProformaLine.taskID, GroupBy<PMProformaLine.costCodeID, GroupBy<PMProformaLine.inventoryID, GroupBy<PMProformaLine.accountGroupID, Sum<PMProformaLine.curyRetainage, Sum<PMProformaLine.retainage, Sum<PMProformaLine.curyLineTotal, Sum<PMProformaLine.lineTotal, Sum<PMProformaLine.qty>>>>>>>>>>>(graph)).Select(new object[1]
        {
          (object) this.GetEarlyProformaRefNbrs(graph)
        }))
        {
          PMProformaLine line = PXResult<PMProformaLine>.op_Implicit(pxResult);
          ProformaEntry.ProformaTotalsCounter.AmountBaseTotals amountBaseTotals = new ProformaEntry.ProformaTotalsCounter.AmountBaseTotals();
          amountBaseTotals.Key = new ProformaEntry.ProformaTotalsCounter.AmountBaseKey(line);
          ref ProformaEntry.ProformaTotalsCounter.AmountBaseTotals local1 = ref amountBaseTotals;
          Decimal? nullable = line.CuryRetainage;
          Decimal valueOrDefault1 = nullable.GetValueOrDefault();
          local1.CuryRetainage = valueOrDefault1;
          ref ProformaEntry.ProformaTotalsCounter.AmountBaseTotals local2 = ref amountBaseTotals;
          nullable = line.Retainage;
          Decimal valueOrDefault2 = nullable.GetValueOrDefault();
          local2.Retainage = valueOrDefault2;
          amountBaseTotals.CuryLineTotal = line.CuryLineTotal.GetValueOrDefault();
          amountBaseTotals.LineTotal = line.LineTotal.GetValueOrDefault();
          this.AmtBaseTotals.Add(amountBaseTotals.Key, amountBaseTotals);
        }
      }
      ProformaEntry.ProformaTotalsCounter.AmountBaseTotals amountBaseTotals1;
      this.AmtBaseTotals.TryGetValue(new ProformaEntry.ProformaTotalsCounter.AmountBaseKey((PMProformaLine) progressLine), out amountBaseTotals1);
      return amountBaseTotals1;
    }

    [DebuggerDisplay("{TaskID}.{CostCodeID}.{InventoryID}.{AccountGroupID}")]
    [ExcludeFromCodeCoverage]
    public class AmountBaseKey
    {
      public readonly int TaskID;
      public readonly int InventoryID;
      public readonly int CostCodeID;
      public readonly int AccountGroupID;

      public AmountBaseKey(int taskID, int costCodeID, int inventoryID, int accountGroupID)
      {
        this.TaskID = taskID;
        this.InventoryID = inventoryID;
        this.CostCodeID = costCodeID;
        this.AccountGroupID = accountGroupID;
      }

      public AmountBaseKey(PMProformaLine line)
      {
        int? nullable = line.TaskID;
        int valueOrDefault1 = nullable.GetValueOrDefault();
        nullable = line.CostCodeID;
        int costCodeID = nullable ?? CostCodeAttribute.GetDefaultCostCode();
        nullable = line.InventoryID;
        int inventoryID = nullable ?? PMInventorySelectorAttribute.EmptyInventoryID;
        nullable = line.AccountGroupID;
        int valueOrDefault2 = nullable.GetValueOrDefault();
        // ISSUE: explicit constructor call
        this.\u002Ector(valueOrDefault1, costCodeID, inventoryID, valueOrDefault2);
      }

      public override int GetHashCode()
      {
        return (((17 * 23 + this.TaskID.GetHashCode()) * 23 + this.InventoryID.GetHashCode()) * 23 + this.CostCodeID.GetHashCode()) * 23 + this.AccountGroupID.GetHashCode();
      }

      public override bool Equals(object obj)
      {
        if (!(obj is ProformaEntry.ProformaTotalsCounter.AmountBaseKey amountBaseKey))
          return false;
        if (this == amountBaseKey)
          return true;
        return amountBaseKey.AccountGroupID == this.AccountGroupID && amountBaseKey.TaskID == this.TaskID && amountBaseKey.CostCodeID == this.CostCodeID && amountBaseKey.InventoryID == this.InventoryID;
      }
    }

    public struct AmountBaseTotals
    {
      public ProformaEntry.ProformaTotalsCounter.AmountBaseKey Key { get; set; }

      public Decimal CuryRetainage { get; set; }

      public Decimal Retainage { get; set; }

      public Decimal CuryLineTotal { get; set; }

      public Decimal LineTotal { get; set; }
    }

    [DebuggerDisplay("{TaskID}.{CostCodeID}.{InventoryID}.{AccountGroupID}.{UOM}")]
    [ExcludeFromCodeCoverage]
    public class QuantityBaseKey : ProformaEntry.ProformaTotalsCounter.AmountBaseKey
    {
      public readonly string UOM;

      public QuantityBaseKey(PMProformaLine line)
        : base(line)
      {
        this.UOM = line.UOM ?? string.Empty;
      }

      public QuantityBaseKey(
        int taskID,
        int costCodeID,
        int inventoryID,
        int accountGroupID,
        string uom)
        : base(taskID, costCodeID, inventoryID, accountGroupID)
      {
        this.UOM = uom ?? string.Empty;
      }

      public override int GetHashCode() => base.GetHashCode() * 23 + this.UOM.GetHashCode();

      public override bool Equals(object obj)
      {
        return obj is ProformaEntry.ProformaTotalsCounter.QuantityBaseKey quantityBaseKey && base.Equals((object) quantityBaseKey) && quantityBaseKey.UOM == this.UOM;
      }
    }

    public struct QuantityBaseTotals
    {
      public ProformaEntry.ProformaTotalsCounter.QuantityBaseKey Key { get; set; }

      public Decimal CuryRetainage { get; set; }

      public Decimal Retainage { get; set; }

      public Decimal CuryLineTotal { get; set; }

      public Decimal LineTotal { get; set; }

      public Decimal QuantityTotal { get; set; }
    }
  }

  [PXHidden]
  public class RetainageOriginalARTran : PX.Objects.AR.ARTran
  {
    public new abstract class tranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProformaEntry.RetainageOriginalARTran.tranType>
    {
    }

    public new abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProformaEntry.RetainageOriginalARTran.refNbr>
    {
    }

    public new abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProformaEntry.RetainageOriginalARTran.lineNbr>
    {
    }

    public new abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProformaEntry.RetainageOriginalARTran.accountID>
    {
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AllocationMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

public class AllocationMaint : 
  PXGraph<AllocationMaint, GLAllocation>,
  PXImportAttribute.IPXPrepareItems
{
  public PXSelect<GLAllocation> AllocationHeader;
  public PXSelect<GLAllocation, Where<GLAllocation.gLAllocationID, Equal<Current<GLAllocation.gLAllocationID>>>> Allocation;
  [PXImport(typeof (GLAllocation))]
  public PXSelect<GLAllocationSource, Where<GLAllocationSource.gLAllocationID, Equal<Current<GLAllocation.gLAllocationID>>>, OrderBy<Asc<GLAllocationSource.lineID>>> Source;
  [PXImport(typeof (GLAllocation))]
  public PXSelect<GLAllocationDestination, Where<GLAllocationDestination.gLAllocationID, Equal<Current<GLAllocation.gLAllocationID>>>, OrderBy<Asc<GLAllocationDestination.lineID>>> Destination;
  public PXSelectJoin<Batch, InnerJoin<GLAllocationHistory, On<Batch.batchNbr, Equal<GLAllocationHistory.batchNbr>, And<Batch.module, Equal<GLAllocationHistory.module>>>>, Where<GLAllocationHistory.gLAllocationID, Equal<Current<GLAllocation.gLAllocationID>>>, OrderBy<Desc<Batch.tranPeriodID, Desc<Batch.batchNbr>>>> Batches;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  public PXAction<GLAllocation> viewBatch;
  private int? justInserted;
  private bool isMassDelete;

  public AllocationMaint()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Batches).Cache, (string) null, false);
    ((PXSelectBase) this.Batches).Cache.AllowInsert = false;
    ((PXSelectBase) this.Batches).Cache.AllowDelete = false;
    ((PXSelectBase) this.Batches).Cache.AllowUpdate = false;
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    Batch current = ((PXSelectBase<Batch>) this.Batches).Current;
    if (current != null)
    {
      JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
      ((PXSelectBase<Batch>) instance.BatchModule).Current = current;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Batch");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  protected virtual void GLAllocation_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    GLAllocation row = (GLAllocation) e.Row;
    if ((e.Operation & 3) == 3)
      return;
    bool? active = row.Active;
    if (active.GetValueOrDefault() && ((PXSelectBase<GLAllocationSource>) this.Source).Select(Array.Empty<object>()).Count == 0)
      throw new PXException("You have to specify one or more source GL Accounts.");
    active = row.Active;
    if (active.GetValueOrDefault() && row.AllocMethod != "E" && ((PXSelectBase<GLAllocationDestination>) this.Destination).Select(Array.Empty<object>()).Count == 0)
      throw new PXException("You have to specify one or more destination GL Accounts.");
    if (row.StartFinPeriodID != null && row.EndFinPeriodID != null)
    {
      int year1;
      int periodNbr1;
      if (!FinPeriodUtils.TryParse(row.StartFinPeriodID, out year1, out periodNbr1))
        cache.RaiseExceptionHandling<GLAllocation.startFinPeriodID>(e.Row, (object) row.StartFinPeriodID, (Exception) new PXSetPropertyException("Start Period has incorrect format"));
      int year2;
      int periodNbr2;
      if (!FinPeriodUtils.TryParse(row.EndFinPeriodID, out year2, out periodNbr2))
        cache.RaiseExceptionHandling<GLAllocation.endFinPeriodID>(e.Row, (object) row.EndFinPeriodID, (Exception) new PXSetPropertyException("End Period has incorrect format"));
      if ((year2 < year1 ? 1 : (year2 != year1 ? 0 : (periodNbr1 > periodNbr2 ? 1 : 0))) != 0)
        cache.RaiseExceptionHandling<GLAllocation.endFinPeriodID>(e.Row, (object) row.EndFinPeriodID, (Exception) new PXSetPropertyException("End Period should be later then Starting Period"));
    }
    if (!this.ValidateSrcAccountsForCurrency())
      throw new PXException("One or more source GL accounts are denominated in foreign currencies. These GL accounts cannot be used for GL allocation.");
    GLAllocationSource aRow;
    if (!this.ValidateSrcAccountsForInterlacing(out aRow))
      ((PXSelectBase) this.Source).Cache.RaiseExceptionHandling<GLAllocationSource.accountCD>((object) aRow, (object) aRow.AccountCD, (Exception) new PXSetPropertyException("Account-Sub combination can not be included into several source lines.", (PXErrorLevel) 5));
    active = row.Active;
    if (!active.GetValueOrDefault() || !this.isWeigthRecalcRequired())
      return;
    Decimal num1 = 0.00M;
    GLAllocationDestination allocationDestination1 = (GLAllocationDestination) null;
    foreach (PXResult<GLAllocationDestination> pxResult in ((PXSelectBase<GLAllocationDestination>) this.Destination).Select(Array.Empty<object>()))
    {
      GLAllocationDestination allocationDestination2 = PXResult<GLAllocationDestination>.op_Implicit(pxResult);
      if (allocationDestination2.Weight.HasValue)
      {
        num1 += allocationDestination2.Weight.Value;
        Decimal num2 = allocationDestination2.Weight.Value;
        if (num2 <= 0.00M || num2 > 100.00M)
        {
          ((PXSelectBase) this.Destination).Cache.RaiseExceptionHandling<GLAllocationDestination.weight>((object) allocationDestination2, (object) allocationDestination2.Weight, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("Weight value must be in the range of {0}-{1}.", new object[2]
          {
            (object) 0,
            (object) 100
          })));
          return;
        }
      }
      if (allocationDestination1 == null)
        allocationDestination1 = allocationDestination2;
    }
    if (!(Math.Abs(num1 - 100.0M) >= 0.000001M))
      return;
    if (allocationDestination1 == null)
      throw new PXException("The total percentage for destination GL Accounts must equal 100%.");
    ((PXSelectBase) this.Destination).Cache.RaiseExceptionHandling<GLAllocationDestination.weight>((object) allocationDestination1, (object) allocationDestination1.Weight, (Exception) new PXSetPropertyException("The total percentage for destination GL Accounts must equal 100%."));
  }

  protected virtual void GLAllocation_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    GLAllocation row = (GLAllocation) e.Row;
    bool flag1 = row.AllocMethod == "P" || row.AllocMethod == "Y";
    PXUIFieldAttribute.SetEnabled<GLAllocation.basisLederID>(cache, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<GLAllocationDestination.basisBranchID>(((PXSelectBase) this.Destination).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<GLAllocationDestination.basisAccountCD>(((PXSelectBase) this.Destination).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<GLAllocationDestination.basisSubCD>(((PXSelectBase) this.Destination).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<GLAllocationDestination.basisBranchID>(((PXSelectBase) this.Destination).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<GLAllocationDestination.basisAccountCD>(((PXSelectBase) this.Destination).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<GLAllocationDestination.basisSubCD>(((PXSelectBase) this.Destination).Cache, (object) null, flag1);
    bool flag2 = row.AllocMethod == "W" || row.AllocMethod == "C";
    PXUIFieldAttribute.SetEnabled<GLAllocationDestination.weight>(((PXSelectBase) this.Destination).Cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<GLAllocationDestination.weight>(((PXSelectBase) this.Destination).Cache, (object) null, flag2);
    PXCache cache1 = ((PXSelectBase) this.Destination).Cache;
    bool? allocateSeparately = row.AllocateSeparately;
    int num1 = !allocateSeparately.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetRequired<GLAllocationDestination.accountCD>(cache1, num1 != 0);
    PXCache cache2 = ((PXSelectBase) this.Destination).Cache;
    allocateSeparately = row.AllocateSeparately;
    int num2 = !allocateSeparately.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetRequired<GLAllocationDestination.subCD>(cache2, num2 != 0);
  }

  protected virtual void GLAllocation_AllocMethod_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    GLAllocation row = (GLAllocation) e.Row;
    if (((string) e.OldValue == "P" || (string) e.OldValue == "Y") && (row.AllocMethod == "P" ? 1 : (row.AllocMethod == "Y" ? 1 : 0)) == 0)
    {
      row.BasisLederID = new int?();
      foreach (PXResult<GLAllocationDestination> pxResult in ((PXSelectBase<GLAllocationDestination>) this.Destination).Select(Array.Empty<object>()))
      {
        GLAllocationDestination allocationDestination = PXResult<GLAllocationDestination>.op_Implicit(pxResult);
        allocationDestination.BasisAccountCD = allocationDestination.BasisSubCD = (string) null;
        ((PXSelectBase) this.Destination).Cache.Update((object) allocationDestination);
      }
    }
    if (((string) e.OldValue == "C" || (string) e.OldValue == "W") && (row.AllocMethod == "W" ? 1 : (row.AllocMethod == "C" ? 1 : 0)) == 0)
    {
      foreach (PXResult<GLAllocationDestination> pxResult in ((PXSelectBase<GLAllocationDestination>) this.Destination).Select(Array.Empty<object>()))
      {
        GLAllocationDestination allocationDestination = PXResult<GLAllocationDestination>.op_Implicit(pxResult);
        allocationDestination.Weight = new Decimal?();
        ((PXSelectBase) this.Destination).Cache.Update((object) allocationDestination);
      }
    }
    if (!(row.AllocMethod == "P") && !(row.AllocMethod == "Y"))
      return;
    row.BasisLederID = row.AllocLedgerID;
  }

  public virtual void GLAllocation_StartFinPeriodID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    FinPeriod finPeriodByDate = this.FinPeriodRepository.FindFinPeriodByDate(((PXGraph) this).Accessinfo.BusinessDate, new int?(0));
    if (finPeriodByDate == null)
      return;
    e.NewValue = (object) FinPeriodIDFormattingAttribute.FormatForDisplay(finPeriodByDate.FinPeriodID);
  }

  protected virtual void GLAllocation_AllocLedgerID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    GLAllocation row = (GLAllocation) e.Row;
    if (row != null)
    {
      row.SourceLedgerID = row.AllocLedgerID;
      if (row.AllocMethod == "P" || row.AllocMethod == "Y")
        row.BasisLederID = row.AllocLedgerID;
    }
    BranchBaseAttribute.VerifyFieldInPXCache<GLAllocationDestination, GLAllocationDestination.branchID>((PXGraph) this, ((PXSelectBase<GLAllocationDestination>) this.Destination).Select(Array.Empty<object>()));
  }

  protected virtual void _(Events.FieldUpdated<GLAllocation.branchID> e)
  {
    if (e.Row == null)
      return;
    GLAllocation row = e.Row as GLAllocation;
    if (((PXSelectBase) this.AllocationHeader).Cache.GetStatus(e.Row) == 2)
    {
      object obj = (object) null;
      ((PXSelectBase) this.AllocationHeader).Cache.RaiseFieldDefaulting<GLAllocation.allocLedgerID>(e.Row, ref obj);
      row.AllocLedgerID = new int?((int) obj);
      ((PXSelectBase) this.AllocationHeader).Cache.RaiseFieldDefaulting<GLAllocation.sourceLedgerID>(e.Row, ref obj);
      row.SourceLedgerID = new int?((int) obj);
      ((PXSelectBase) this.AllocationHeader).Cache.RaiseFieldDefaulting<GLAllocation.allocLedgerBalanceType>(e.Row, ref obj);
      row.AllocLedgerBalanceType = (string) obj;
      ((PXSelectBase) this.AllocationHeader).Cache.RaiseFieldDefaulting<GLAllocation.allocLedgerBaseCuryID>(e.Row, ref obj);
      row.AllocLedgerBaseCuryID = (string) obj;
    }
    else
    {
      object allocLedgerId = (object) row.AllocLedgerID;
      try
      {
        ((PXSelectBase) this.AllocationHeader).Cache.RaiseFieldVerifying<GLAllocation.allocLedgerID>(e.Row, ref allocLedgerId);
      }
      catch (PXSetPropertyException ex)
      {
        ((PXSelectBase) this.AllocationHeader).Cache.RaiseExceptionHandling<GLAllocation.allocLedgerID>(e.Row, allocLedgerId, (Exception) ex);
      }
    }
    this.ValidateBranchesForLedgers();
  }

  protected virtual void _(Events.FieldUpdated<GLAllocation.sourceLedgerID> e)
  {
    BranchBaseAttribute.VerifyFieldInPXCache<GLAllocationSource, GLAllocationSource.branchID>((PXGraph) this, ((PXSelectBase<GLAllocationSource>) this.Source).Select(Array.Empty<object>()));
  }

  protected virtual void _(Events.FieldUpdated<GLAllocation.basisLederID> e)
  {
    BranchBaseAttribute.VerifyFieldInPXCache<GLAllocationDestination, GLAllocationDestination.basisBranchID>((PXGraph) this, ((PXSelectBase<GLAllocationDestination>) this.Destination).Select(Array.Empty<object>()));
  }

  protected virtual void GLAllocationSource_LimitPercent_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    GLAllocationSource row = (GLAllocationSource) e.Row;
    GLAllocationSource allocationSource = row;
    Decimal? limitPercent = row.LimitPercent;
    Decimal num = 0M;
    Decimal? nullable = limitPercent.GetValueOrDefault() == num & limitPercent.HasValue ? row.LimitAmount : new Decimal?(0M);
    allocationSource.LimitAmount = nullable;
  }

  protected virtual void GLAllocationSource_LimitAmount_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    GLAllocationSource row = (GLAllocationSource) e.Row;
    Decimal? limitAmount = row.LimitAmount;
    Decimal num = 0M;
    row.LimitPercent = new Decimal?(limitAmount.GetValueOrDefault() == num & limitAmount.HasValue ? 100.00M : 0M);
  }

  protected virtual void GLAllocationSource_SubCD_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLAllocationSource_AccountCD_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    this.VerifyAccountIDToBeNoControl<GLAllocationSource.accountCD, GLAllocationSource.accountCD>(cache, (EventArgs) e, e.NewValue, (int?) ((PXSelectBase<GLAllocation>) this.Allocation).Current?.SourceLedgerID);
  }

  protected virtual void GLAllocationSource_ContrAccountID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    this.VerifyAccountIDToBeNoControl<GLAllocationSource.contrAccountID, Account.accountID>(cache, (EventArgs) e, e.NewValue, (int?) ((PXSelectBase<GLAllocation>) this.Allocation).Current?.SourceLedgerID);
  }

  private void VerifyAccountIDToBeNoControl<T, A>(
    PXCache cache,
    EventArgs e,
    object accountID,
    int? ledgerID)
    where T : IBqlField
    where A : IBqlField
  {
    if (accountID == null || Ledger.PK.Find((PXGraph) this, ledgerID)?.BalanceType != nameof (A))
      return;
    Account account = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account>.Config>.Search<A>((PXGraph) this, accountID, Array.Empty<object>()));
    AccountAttribute.VerifyAccountIsNotControl<T>(cache, e, account);
  }

  protected virtual void GLAllocationSource_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    this.UpdateParentStatus();
  }

  protected virtual void GLAllocationSource_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    this.UpdateParentStatus();
  }

  protected virtual void GLAllocationSource_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    this.UpdateParentStatus();
  }

  protected virtual void GLAllocationSource_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    GLAllocationSource row = (GLAllocationSource) e.Row;
    if (string.IsNullOrEmpty(row.AccountCD.Trim()))
    {
      if (cache.RaiseExceptionHandling<GLAllocationSource.accountCD>(e.Row, (object) row.AccountCD, (Exception) new PXSetPropertyException("Source Account mask cannot be empty.", (PXErrorLevel) 4)))
        throw new PXRowPersistingException(typeof (GLAllocationSource.accountCD).Name, (object) row.AccountCD, "Source Account mask cannot be empty.");
    }
    else
    {
      this.VerifyAccountIDToBeNoControl<GLAllocationSource.accountCD, GLAllocationSource.accountCD>(cache, (EventArgs) e, (object) row.AccountCD, (int?) ((PXSelectBase<GLAllocation>) this.Allocation).Current?.SourceLedgerID);
      this.VerifyAccountIDToBeNoControl<GLAllocationSource.contrAccountID, Account.accountID>(cache, (EventArgs) e, (object) row.ContrAccountID, (int?) ((PXSelectBase<GLAllocation>) this.Allocation).Current?.SourceLedgerID);
    }
  }

  protected virtual void GLAllocationDestination_RowInserting(
    PXCache cache,
    PXRowInsertingEventArgs e)
  {
    if (this.isWeigthRecalcRequired())
    {
      GLAllocationDestination row = (GLAllocationDestination) e.Row;
      if (row.Weight.HasValue)
        return;
      row.Weight = new Decimal?((Decimal) 100);
      foreach (PXResult<GLAllocationDestination> pxResult in ((PXSelectBase<GLAllocationDestination>) this.Destination).Select(Array.Empty<object>()))
      {
        GLAllocationDestination allocationDestination1 = PXResult<GLAllocationDestination>.op_Implicit(pxResult);
        if (allocationDestination1 != row)
        {
          GLAllocationDestination allocationDestination2 = row;
          Decimal? weight1 = allocationDestination2.Weight;
          Decimal? weight2 = allocationDestination1.Weight;
          allocationDestination2.Weight = weight1.HasValue & weight2.HasValue ? new Decimal?(weight1.GetValueOrDefault() - weight2.GetValueOrDefault()) : new Decimal?();
        }
      }
      this.justInserted = row.LineID;
    }
    else
      this.justInserted = new int?();
  }

  protected virtual void GLAllocationDestination_Weight_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (!this.isWeigthRecalcRequired())
      return;
    GLAllocationDestination row = (GLAllocationDestination) e.Row;
    Decimal newValue = (Decimal) e.NewValue;
    if (newValue <= 0.00M || newValue > 100.00M)
      throw new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("Weight value must be in the range of {0}-{1}.", new object[2]
      {
        (object) 0,
        (object) 100
      }));
  }

  protected virtual void GLAllocationDestination_BasisSubCD_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLAllocationDestination_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (this.isWeigthRecalcRequired() && ((PXSelectBase) this.Allocation).Cache.GetStatus((object) ((PXSelectBase<GLAllocation>) this.Allocation).Current) == null)
      ((PXSelectBase) this.Allocation).Cache.SetStatus((object) ((PXSelectBase<GLAllocation>) this.Allocation).Current, (PXEntryStatus) 1);
    this.UpdateParentStatus();
  }

  protected virtual void GLAllocationDestination_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    if (((PXSelectBase) this.Allocation).Cache.GetStatus((object) ((PXSelectBase<GLAllocation>) this.Allocation).Current) == 3 || !this.isWeigthRecalcRequired())
      return;
    GLAllocationDestination row = (GLAllocationDestination) e.Row;
    if (this.isMassDelete)
      return;
    if (this.justInserted.HasValue)
    {
      int num = this.justInserted.Value;
      int? lineId = row.LineID;
      int valueOrDefault = lineId.GetValueOrDefault();
      if (num == valueOrDefault & lineId.HasValue)
        goto label_5;
    }
    this.distributePercentOf(row);
    ((PXSelectBase) this.Destination).View.RequestRefresh();
label_5:
    ((PXSelectBase) this.Allocation).Cache.Update((object) ((PXSelectBase<GLAllocation>) this.Allocation).Current);
  }

  protected virtual void GLAllocationDestination_RowInserted(
    PXCache cache,
    PXRowInsertedEventArgs e)
  {
    this.UpdateParentStatus();
  }

  protected virtual void GLAllocationDestination_RowPersisting(
    PXCache cache,
    PXRowPersistingEventArgs e)
  {
    GLAllocationDestination row = (GLAllocationDestination) e.Row;
    if ((e.Operation & 3) == 3)
      return;
    this.VerifyAccountIDToBeNoControl<GLAllocationDestination.accountCD, Account.accountCD>(cache, (EventArgs) e, (object) row.AccountCD, (int?) ((PXSelectBase<GLAllocation>) this.Allocation).Current?.AllocLedgerID);
    this.VerifyAccountIDToBeNoControl<GLAllocationDestination.basisAccountCD, Account.accountCD>(cache, (EventArgs) e, (object) row.BasisAccountCD, (int?) ((PXSelectBase<GLAllocation>) this.Allocation).Current?.AllocLedgerID);
    GLAllocation current = ((PXSelectBase<GLAllocation>) this.Allocation).Current;
    List<GLAllocationDestination> duplicated = this.FindDuplicated(row, current);
    PXDefaultAttribute.SetPersistingCheck<GLAllocationDestination.accountCD>(((PXSelectBase) this.Destination).Cache, e.Row, current.AllocateSeparately.GetValueOrDefault() ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXDefaultAttribute.SetPersistingCheck<GLAllocationDestination.subCD>(((PXSelectBase) this.Destination).Cache, e.Row, current.AllocateSeparately.GetValueOrDefault() ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    if (duplicated.Count <= 0)
      return;
    duplicated.Add(row);
    foreach (GLAllocationDestination allocationDestination in duplicated)
    {
      PXErrorLevel pxErrorLevel = current.AllocMethod == "P" || current.AllocMethod == "Y" ? (PXErrorLevel) 5 : (PXErrorLevel) 3;
      string str = current.AllocMethod == "P" || current.AllocMethod == "Y" ? "Destination accounts may not be duplicated for this allocation type" : "These Allocation destinations have identical accounts settings. Probably, they should be merged.";
      cache.RaiseExceptionHandling<GLAllocationDestination.accountCD>((object) allocationDestination, (object) allocationDestination.AccountCD, (Exception) new PXSetPropertyException(str, pxErrorLevel));
    }
  }

  protected virtual void GLAllocationDestination_AccountCD_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    this.VerifyAccountIDToBeNoControl<GLAllocationDestination.accountCD, Account.accountCD>(cache, (EventArgs) e, e.NewValue, (int?) ((PXSelectBase<GLAllocation>) this.Allocation).Current?.AllocLedgerID);
    this.ValidateDestAccountCDMask(e.Row as GLAllocationDestination, e.NewValue?.ToString());
  }

  protected virtual void GLAllocationDestination_SubCD_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    this.ValidateDestSubCDMask(e.Row as GLAllocationDestination, e.NewValue?.ToString(), out bool _);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLAllocationDestination_BasisAccountCD_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    this.VerifyAccountIDToBeNoControl<GLAllocationDestination.basisAccountCD, Account.accountCD>(cache, (EventArgs) e, e.NewValue, (int?) ((PXSelectBase<GLAllocation>) this.Allocation).Current?.AllocLedgerID);
  }

  protected virtual void UpdateParentStatus()
  {
    if (((PXSelectBase<GLAllocation>) this.Allocation).Current == null)
      return;
    GraphHelper.MarkUpdated(((PXSelectBase) this.Allocation).Cache, (object) ((PXSelectBase<GLAllocation>) this.Allocation).Current);
  }

  protected virtual void distributePercentOf(GLAllocationDestination row)
  {
    if (row == null || !row.Weight.HasValue || !row.Weight.HasValue)
      return;
    GLAllocationDestination allocationDestination1 = (GLAllocationDestination) null;
    Decimal num1 = 0M;
    foreach (PXResult<GLAllocationDestination> pxResult in ((PXSelectBase<GLAllocationDestination>) this.Destination).Select(Array.Empty<object>()))
    {
      GLAllocationDestination allocationDestination2 = PXResult<GLAllocationDestination>.op_Implicit(pxResult);
      if (allocationDestination2 != row)
      {
        num1 += allocationDestination2.Weight.GetValueOrDefault();
        if (allocationDestination1 == null || allocationDestination2.LineID.HasValue && allocationDestination2.LineID.Value > allocationDestination1.LineID.Value)
          allocationDestination1 = allocationDestination2;
      }
    }
    Decimal num2 = 100.0M - num1;
    Decimal num3 = num2;
    Decimal? weight = row.Weight;
    Decimal num4 = weight.Value;
    Decimal num5;
    if (!(num3 < num4))
    {
      weight = row.Weight;
      num5 = weight.Value;
    }
    else
      num5 = num2;
    Decimal num6 = num5;
    if (allocationDestination1 == null || !(num6 > 0M))
      return;
    GLAllocationDestination allocationDestination3 = allocationDestination1;
    weight = allocationDestination3.Weight;
    Decimal num7 = num6;
    allocationDestination3.Weight = weight.HasValue ? new Decimal?(weight.GetValueOrDefault() + num7) : new Decimal?();
  }

  protected virtual bool ValidateSrcAccountsForCurrency()
  {
    Branch branch = (Branch) PXSelectorAttribute.Select<GLAllocation.branchID>(((PXSelectBase) this.AllocationHeader).Cache, ((PXSelectBase) this.AllocationHeader).Cache.Current);
    PXSelectBase<Account> pxSelectBase = (PXSelectBase<Account>) new PXSelect<Account, Where<Account.accountCD, Like<Required<Account.accountCD>>, And<Account.curyID, IsNotNull, And<Account.curyID, NotEqual<Required<Branch.baseCuryID>>>>>>((PXGraph) this);
    foreach (PXResult<GLAllocationSource> pxResult1 in ((PXSelectBase<GLAllocationSource>) this.Source).Select(Array.Empty<object>()))
    {
      string subCdWildcard = SubCDUtils.CreateSubCDWildcard(PXResult<GLAllocationSource>.op_Implicit(pxResult1).AccountCD, "ACCOUNT");
      foreach (PXResult<Account> pxResult2 in pxSelectBase.Select(new object[2]
      {
        (object) subCdWildcard,
        (object) branch.BaseCuryID
      }))
      {
        if (PXResult<Account>.op_Implicit(pxResult2) != null)
          return false;
      }
    }
    return true;
  }

  protected virtual bool ValidateBranchesForLedgers()
  {
    bool flag = true;
    foreach (PXResult<GLAllocationSource> pxResult in ((PXSelectBase<GLAllocationSource>) this.Source).Select(Array.Empty<object>()))
    {
      GLAllocationSource allocationSource = PXResult<GLAllocationSource>.op_Implicit(pxResult);
      object branchId = (object) allocationSource.BranchID;
      try
      {
        ((PXSelectBase) this.Source).Cache.RaiseFieldVerifying<GLAllocationSource.branchID>((object) allocationSource, ref branchId);
      }
      catch (PXSetPropertyException ex)
      {
        ((PXSelectBase) this.Source).Cache.RaiseExceptionHandling<GLAllocationSource.branchID>((object) allocationSource, branchId, (Exception) ex);
        flag = false;
      }
    }
    foreach (PXResult<GLAllocationDestination> pxResult in ((PXSelectBase<GLAllocationDestination>) this.Destination).Select(Array.Empty<object>()))
    {
      GLAllocationDestination allocationDestination = PXResult<GLAllocationDestination>.op_Implicit(pxResult);
      object branchId = (object) allocationDestination.BranchID;
      try
      {
        ((PXSelectBase) this.Destination).Cache.RaiseFieldVerifying<GLAllocationDestination.branchID>((object) allocationDestination, ref branchId);
      }
      catch (PXSetPropertyException ex)
      {
        ((PXSelectBase) this.Destination).Cache.RaiseExceptionHandling<GLAllocationDestination.branchID>((object) allocationDestination, branchId, (Exception) ex);
        flag = false;
      }
    }
    if (((PXSelectBase<GLAllocation>) this.AllocationHeader).Current?.AllocMethod == "P" || ((PXSelectBase<GLAllocation>) this.AllocationHeader).Current?.AllocMethod == "Y")
    {
      foreach (PXResult<GLAllocationDestination> pxResult in ((PXSelectBase<GLAllocationDestination>) this.Destination).Select(Array.Empty<object>()))
      {
        GLAllocationDestination allocationDestination = PXResult<GLAllocationDestination>.op_Implicit(pxResult);
        object basisBranchId = (object) allocationDestination.BasisBranchID;
        try
        {
          ((PXSelectBase) this.Destination).Cache.RaiseFieldVerifying<GLAllocationDestination.basisBranchID>((object) allocationDestination, ref basisBranchId);
        }
        catch (PXSetPropertyException ex)
        {
          ((PXSelectBase) this.Destination).Cache.RaiseExceptionHandling<GLAllocationDestination.basisBranchID>((object) allocationDestination, basisBranchId, (Exception) ex);
          flag = false;
        }
      }
    }
    return flag;
  }

  protected virtual bool ValidateSrcAccountsForInterlacing(out GLAllocationSource aRow)
  {
    aRow = (GLAllocationSource) null;
    Dictionary<AllocationProcess.BranchAccountSubKey, AllocationMaint.AllocationSourceDetail> aSrcDict = new Dictionary<AllocationProcess.BranchAccountSubKey, AllocationMaint.AllocationSourceDetail>();
    foreach (PXResult<GLAllocationSource> pxResult in ((PXSelectBase<GLAllocationSource>) this.Source).Select(Array.Empty<object>()))
    {
      GLAllocationSource aSrc = PXResult<GLAllocationSource>.op_Implicit(pxResult);
      if (this.IsSourceInterlacingWithDictionary(aSrc, aSrcDict))
      {
        aRow = aSrc;
        return false;
      }
    }
    return true;
  }

  protected virtual bool IsSourceInterlacingWithDictionary(
    GLAllocationSource aSrc,
    Dictionary<AllocationProcess.BranchAccountSubKey, AllocationMaint.AllocationSourceDetail> aSrcDict)
  {
    string subCdWildcard1 = SubCDUtils.CreateSubCDWildcard(aSrc.AccountCD, "ACCOUNT");
    string subCdWildcard2 = SubCDUtils.CreateSubCDWildcard(aSrc.SubCD, "SUBACCOUNT");
    foreach (PXResult<Account> pxResult1 in PXSelectBase<Account, PXSelect<Account, Where<Account.accountCD, Like<Required<Account.accountCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) subCdWildcard1
    }))
    {
      Account account = PXResult<Account>.op_Implicit(pxResult1);
      foreach (PXResult<Sub> pxResult2 in PXSelectBase<Sub, PXSelect<Sub, Where<Sub.subCD, Like<Required<Sub.subCD>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) subCdWildcard2
      }))
      {
        Sub sub = PXResult<Sub>.op_Implicit(pxResult2);
        int aFirst = aSrc.BranchID.Value;
        int? nullable = account.AccountID;
        int aSecond = nullable.Value;
        nullable = sub.SubID;
        int aThird = nullable.Value;
        AllocationProcess.BranchAccountSubKey key = new AllocationProcess.BranchAccountSubKey(aFirst, aSecond, aThird);
        if (aSrcDict.ContainsKey(key))
          return true;
        AllocationMaint.AllocationSourceDetail allocationSourceDetail = new AllocationMaint.AllocationSourceDetail(aSrc);
        allocationSourceDetail.AccountID = account.AccountID;
        allocationSourceDetail.SubID = sub.SubID;
        if (allocationSourceDetail.ContraAccountID.HasValue && !allocationSourceDetail.ContraSubID.HasValue)
          allocationSourceDetail.ContraSubID = allocationSourceDetail.SubID;
        aSrcDict[key] = allocationSourceDetail;
      }
    }
    return false;
  }

  protected virtual bool isWeigthRecalcRequired()
  {
    return ((PXSelectBase<GLAllocation>) this.Allocation).Current.AllocMethod == "C";
  }

  protected virtual List<GLAllocationDestination> FindDuplicated(
    GLAllocationDestination aDest,
    GLAllocation aDefinition)
  {
    List<GLAllocationDestination> duplicated = new List<GLAllocationDestination>();
    int num = aDefinition.AllocMethod == "P" ? 1 : (aDefinition.AllocMethod == "Y" ? 1 : 0);
    foreach (PXResult<GLAllocationDestination> pxResult in ((PXSelectBase<GLAllocationDestination>) this.Destination).Select(Array.Empty<object>()))
    {
      GLAllocationDestination allocationDestination = PXResult<GLAllocationDestination>.op_Implicit(pxResult);
      if (aDest != allocationDestination)
      {
        int? nullable1;
        int? nullable2;
        if (aDest.GLAllocationID == allocationDestination.GLAllocationID)
        {
          nullable1 = aDest.LineID;
          nullable2 = allocationDestination.LineID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
            continue;
        }
        if (aDest.AccountCD == allocationDestination.AccountCD && aDest.SubCD == allocationDestination.SubCD)
        {
          nullable2 = aDest.BranchID;
          nullable1 = allocationDestination.BranchID;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            duplicated.Add(allocationDestination);
        }
      }
    }
    return duplicated;
  }

  protected virtual bool ValidateDestinationMasks()
  {
    bool flag = true;
    if (((PXSelectBase<GLAllocation>) this.Allocation).Current == null)
      return flag;
    SubAccountMaint instance = PXGraph.CreateInstance<SubAccountMaint>();
    foreach (PXResult<GLAllocationDestination> pxResult in ((PXSelectBase<GLAllocationDestination>) this.Destination).Select(Array.Empty<object>()))
    {
      GLAllocationDestination dest = PXResult<GLAllocationDestination>.op_Implicit(pxResult);
      flag = this.ValidateDestAccountCDMask(dest, dest.AccountCD) & flag;
      bool createNewSubAccount = false;
      flag = this.ValidateDestSubCDMask(dest, dest.SubCD, out createNewSubAccount) & flag;
      if (createNewSubAccount)
        ((PXSelectBase<Sub>) instance.SubRecords).Insert(new Sub()
        {
          SubCD = dest.SubCD,
          Active = new bool?(true)
        });
    }
    if (flag)
      ((PXAction) instance.Save).Press();
    return flag;
  }

  protected virtual bool ValidateDestAccountCDMask(GLAllocationDestination dest, string newValue)
  {
    bool flag = true;
    GLAllocation current = ((PXSelectBase<GLAllocation>) this.Allocation).Current;
    if (current == null)
      return flag;
    if (newValue == null)
    {
      if ((current != null ? (!current.AllocateSeparately.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      {
        ((PXSelectBase) this.Destination).Cache.RaiseExceptionHandling<GLAllocationDestination.accountCD>((object) dest, (object) newValue, (Exception) new PXSetPropertyException("Destination Account cannot be empty.", (PXErrorLevel) 4));
        flag = false;
      }
    }
    else
    {
      Account account = Account.UK.Find((PXGraph) this, newValue);
      if (account == null)
      {
        ((PXSelectBase) this.Destination).Cache.RaiseExceptionHandling<GLAllocationDestination.accountCD>((object) dest, (object) newValue, (Exception) new PXSetPropertyException("Cannot find account {0} in the source company. Verify account mapping on the Chart of Accounts (GL202500) form.", (PXErrorLevel) 4, new object[1]
        {
          (object) newValue
        }));
        flag = false;
      }
      else
      {
        Ledger ledger = PXSelectorAttribute.Select<GLAllocation.allocLedgerID>(((PXSelectBase) this.Allocation).Cache, (object) ((PXSelectBase<GLAllocation>) this.Allocation).Current) as Ledger;
        if (account != null)
        {
          if (account.Active.GetValueOrDefault() && (account.CuryID == null || !(account.CuryID != ledger.BaseCuryID)))
          {
            int? ytdNetIncAccountId = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current.YtdNetIncAccountID;
            int? accountId = account.AccountID;
            if (!(ytdNetIncAccountId.GetValueOrDefault() == accountId.GetValueOrDefault() & ytdNetIncAccountId.HasValue == accountId.HasValue))
              goto label_11;
          }
          ((PXSelectBase) this.Destination).Cache.RaiseExceptionHandling<GLAllocationDestination.subCD>((object) dest, (object) newValue, (Exception) new PXSetPropertyException("'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.", (PXErrorLevel) 4, new object[1]
          {
            (object) "ACCOUNT"
          }));
        }
      }
    }
label_11:
    return flag;
  }

  protected virtual bool ValidateDestSubCDMask(
    GLAllocationDestination dest,
    string newValue,
    out bool createNewSubAccount)
  {
    bool flag = true;
    createNewSubAccount = false;
    GLAllocation current = ((PXSelectBase<GLAllocation>) this.Allocation).Current;
    if (current == null)
      return flag;
    if (newValue == null)
    {
      if ((current != null ? (!current.AllocateSeparately.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      {
        ((PXSelectBase) this.Destination).Cache.RaiseExceptionHandling<GLAllocationDestination.subCD>((object) dest, (object) newValue, (Exception) new PXSetPropertyException("Destination Subaccount cannot be empty.", (PXErrorLevel) 4));
        flag = false;
      }
    }
    else if (Sub.UK.Find((PXGraph) this, newValue) == null)
    {
      string errorMessage = (string) null;
      bool hasBlanks = false;
      flag = this.CheckSegments(((PXSelectBase) this.Destination).Cache, newValue, out errorMessage, out hasBlanks, out createNewSubAccount);
      if (errorMessage != null)
        ((PXSelectBase) this.Destination).Cache.RaiseExceptionHandling<GLAllocationDestination.subCD>((object) dest, (object) newValue, (Exception) new PXSetPropertyException(errorMessage, (PXErrorLevel) 4, new object[1]
        {
          (object) dest.SubCD
        }));
      else if (hasBlanks && (current != null ? (!current.AllocateSeparately.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      {
        ((PXSelectBase) this.Destination).Cache.RaiseExceptionHandling<GLAllocationDestination.subCD>((object) dest, (object) newValue, (Exception) new PXSetPropertyException("'{0}' cannot be found in the system.", (PXErrorLevel) 4, new object[1]
        {
          (object) "SUBACCOUNT"
        }));
        flag = false;
      }
    }
    return flag;
  }

  protected virtual bool CheckSegments(
    PXCache cache,
    string subCD,
    out string errorMessage,
    out bool hasBlanks,
    out bool createNewSubAccount)
  {
    bool flag = true;
    errorMessage = (string) null;
    hasBlanks = false;
    createNewSubAccount = false;
    PXDimensionAttribute.Definition slot = PXContext.GetSlot<PXDimensionAttribute.Definition>();
    DimensionLookupMode lookupMode = slot.LookupModes["SUBACCOUNT"];
    int num = ((IEnumerable<PXSegment>) slot.Dimensions["SUBACCOUNT"]).Count<PXSegment>();
    int startIndex = 0;
    for (int index = 1; index <= num; ++index)
    {
      List<string> list = ((IEnumerable<string>) PXDimensionAttribute.GetSegmentValues("SUBACCOUNT", index)).ToList<string>();
      string str = subCD.Substring(startIndex, subCD.Length < startIndex + list[0].Length ? subCD.Length - startIndex : list[0].Length);
      startIndex += list[0].Length;
      if (str.Trim() == "")
        hasBlanks = true;
      else if (!list.Contains(str))
      {
        flag = false;
        errorMessage = $"'{slot.Dimensions["SUBACCOUNT"][index - 1].Descr}' of '{"SUBACCOUNT"}' does not exist in the system.";
      }
    }
    if (errorMessage == null && !hasBlanks)
    {
      if (lookupMode == 1)
      {
        createNewSubAccount = true;
      }
      else
      {
        createNewSubAccount = false;
        errorMessage = $"'{"SUBACCOUNT"}' cannot be found in the system.";
      }
    }
    return flag;
  }

  public virtual void Persist()
  {
    if (!this.ValidateBranchesForLedgers() || !this.ValidateDestinationMasks())
      return;
    ((PXGraph) this).Persist();
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values) => true;

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public class AllocationSourceDetail
  {
    public int? LineID;
    public int? BranchID;
    public int? AccountID;
    public int? SubID;
    public int? ContraAccountID;
    public int? ContraSubID;
    public Decimal? LimitAmount;
    public Decimal? LimitPercent;

    public AllocationSourceDetail()
    {
    }

    public AllocationSourceDetail(GLAllocationSource aSrc) => this.CopyFrom(aSrc);

    public virtual void CopyFrom(GLAllocationSource aSrc)
    {
      this.LineID = aSrc.LineID;
      this.BranchID = aSrc.BranchID;
      this.ContraAccountID = aSrc.ContrAccountID;
      this.ContraSubID = aSrc.ContrSubID;
      this.LimitAmount = aSrc.LimitAmount;
      this.LimitPercent = aSrc.LimitPercent;
    }
  }
}

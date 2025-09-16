// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.JournalEntryState.StateControllerBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

#nullable disable
namespace PX.Objects.GL.JournalEntryState;

public abstract class StateControllerBase
{
  protected readonly JournalEntry JournalEntry;

  protected PXCache<Batch> BatchCache
  {
    get => (PXCache<Batch>) ((PXSelectBase) this.JournalEntry.BatchModule).Cache;
  }

  protected PXCache<GLTran> GLTranCache
  {
    get => (PXCache<GLTran>) ((PXSelectBase) this.JournalEntry.GLTranModuleBatNbr).Cache;
  }

  protected StateControllerBase(JournalEntry journalEntry) => this.JournalEntry = journalEntry;

  public virtual void Batch_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    Batch row = e.Row as Batch;
    bool flag1 = !row.Released.GetValueOrDefault();
    bool valueOrDefault = row.Posted.GetValueOrDefault();
    row.Voided.GetValueOrDefault();
    int num1 = row.Module == "GL" ? 1 : 0;
    bool flag2 = cache.GetStatus((object) row) == 2;
    bool flag3 = row.BatchType == "RCL";
    int num2 = row.BatchType == "T" ? 1 : 0;
    int num3 = row.BatchType == "A" ? 1 : 0;
    bool flag4 = PXAccess.FeatureInstalled<FeaturesSet.taxEntryFromGL>() && row != null && row.Module == "GL";
    bool flag5 = true;
    if (row.Module == "GL" && row.BatchType != "T" || row.Module == "CM" || row.Module == "FA")
      flag5 = false;
    ((PXAction) this.JournalEntry.viewDocument).SetEnabled(flag5);
    PXUIFieldAttribute.SetVisible<Batch.curyID>(cache, (object) row, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    PXUIFieldAttribute.SetVisible<Batch.createTaxTrans>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetEnabled<Batch.createTaxTrans>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<Batch.autoReverseCopy>(cache, (object) row, false);
    if (!((PXGraph) this.JournalEntry).IsImport || HttpContext.Current != null)
    {
      PXUIFieldAttribute.SetVisible<GLTran.tranDate>((PXCache) this.GLTranCache, (object) null, row.Module != "GL");
      PXUIFieldAttribute.SetVisible<GLTran.taxID>((PXCache) this.GLTranCache, (object) null, this.ShouldCreateTaxTrans(row));
      PXUIFieldAttribute.SetVisible<GLTran.taxCategoryID>((PXCache) this.GLTranCache, (object) null, this.ShouldCreateTaxTrans(row));
    }
    PXUIFieldAttribute.SetEnabled<Batch.module>(cache, (object) row);
    PXUIFieldAttribute.SetEnabled<Batch.batchNbr>(cache, (object) row);
    PXUIFieldAttribute.SetVisible<Batch.scheduleID>(cache, (object) row, false);
    PXUIFieldAttribute.SetVisible<Batch.curyControlTotal>(cache, (object) row, ((PXSelectBase<GLSetup>) this.JournalEntry.glsetup).Current.RequireControlTotal.Value);
    PXUIFieldAttribute.SetEnabled<Batch.skipTaxValidation>(cache, (object) row, row.CreateTaxTrans.GetValueOrDefault());
    ((PXAction) this.JournalEntry.batchRegisterDetails).SetEnabled(!flag1);
    ((PXAction) this.JournalEntry.glEditDetails).SetEnabled(flag1 && !valueOrDefault && !flag2);
    ((PXAction) this.JournalEntry.reverseBatch).SetEnabled(this.CanReverseBatch(row));
    PXAction<Batch> reversingBatches = this.JournalEntry.glReversingBatches;
    int? reverseCount1 = row.ReverseCount;
    int num4 = 0;
    int num5 = reverseCount1.GetValueOrDefault() > num4 & reverseCount1.HasValue ? 1 : 0;
    ((PXAction) reversingBatches).SetEnabled(num5 != 0);
    PXCache pxCache = cache;
    Batch batch = row;
    int? reverseCount2 = row.ReverseCount;
    int num6 = 0;
    int num7 = reverseCount2.GetValueOrDefault() > num6 & reverseCount2.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<Batch.reverseCount>(pxCache, (object) batch, num7 != 0);
    bool alreadyReclassified;
    this.SetReclassifyButtonState(row, out alreadyReclassified);
    PXUIFieldAttribute.SetVisible<GLTran.curyReclassRemainingAmt>((PXCache) this.GLTranCache, (object) null, row.HasRamainingAmount.GetValueOrDefault());
    PXUIFieldAttribute.SetVisible<GLTran.origBatchNbr>((PXCache) this.GLTranCache, (object) null, flag3);
    ((PXAction) this.JournalEntry.editReclassBatch).SetVisible(flag3);
    PXUIFieldAttribute.SetVisible<GLTran.reclassBatchNbr>(((PXSelectBase) this.JournalEntry.GLTranModuleBatNbr).Cache, (object) null, alreadyReclassified);
  }

  public virtual void GLTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e, Batch batch)
  {
    GLTran row = e.Row as GLTran;
    JournalEntry.SetReclassTranWarningsIfNeed(sender, row);
    this.SetGLTranRefNbrRequired(row, batch);
    if (this.ShouldCreateTaxTrans(batch))
    {
      PXFieldState stateExt = (PXFieldState) sender.GetStateExt<GLTran.taxID>((object) row);
      if (string.IsNullOrEmpty(stateExt.Error) || stateExt.IsWarning)
        this.WarnIfMissingTaxID(row);
    }
    row.IncludedInReclassHistory = new bool?(JournalEntry.CanShowReclassHistory(row, batch.BatchType));
  }

  public virtual void Batch_CreateTaxTrans_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.SetGLTranRefNbrRequired((GLTran) null, (Batch) e.Row);
  }

  public virtual void GLTran_TaxCategoryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e,
    Batch batch)
  {
    if (this.ShouldCreateTaxTrans(batch))
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected bool IsTaxTranCreationAllowed(Batch batch)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.taxEntryFromGL>() && batch != null && batch.Module == "GL";
  }

  public bool ShouldCreateTaxTrans(Batch batch)
  {
    return this.IsTaxTranCreationAllowed(batch) && batch.CreateTaxTrans.GetValueOrDefault();
  }

  private void SetGLTranRefNbrRequired(GLTran tran, Batch batch)
  {
    PXPersistingCheck pxPersistingCheck = (!((PXSelectBase<GLSetup>) this.JournalEntry.glsetup).Current.RequireRefNbrForTaxEntry.GetValueOrDefault() || !batch.CreateTaxTrans.GetValueOrDefault() ? 0 : (PXAccess.FeatureInstalled<FeaturesSet.taxEntryFromGL>() ? 1 : 0)) != 0 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2;
    PXDefaultAttribute.SetPersistingCheck<GLTran.refNbr>((PXCache) this.GLTranCache, (object) tran, pxPersistingCheck);
  }

  private void SetReclassifyButtonState(Batch batch, out bool alreadyReclassified)
  {
    alreadyReclassified = false;
    if (((PXGraph) this.JournalEntry).UnattendedMode)
      return;
    PX.Objects.GL.Ledger ledger = PX.Objects.GL.Ledger.PK.Find((PXGraph) this.JournalEntry, batch.LedgerID);
    bool flag = ledger != null && JournalEntry.IsBatchReclassifiable(batch, ledger);
    if (flag)
    {
      IEnumerable<GLTran> source = GraphHelper.RowCast<GLTran>((IEnumerable) ((PXSelectBase<GLTran>) this.JournalEntry.GLTranModuleBatNbr).Select(Array.Empty<object>()));
      alreadyReclassified = source.Any<GLTran>((Func<GLTran, bool>) (tran => tran.ReclassBatchNbr != null));
      batch.HasRamainingAmount = new bool?(source.Any<GLTran>((Func<GLTran, bool>) (tran => tran.ReclassRemainingAmt.GetValueOrDefault() != 0M)));
      flag = source.Any<GLTran>((Func<GLTran, bool>) (tran => JournalEntry.IsTransactionReclassifiable(tran, batch.BatchType, ledger.BalanceType, ProjectDefaultAttribute.NonProject())));
    }
    ((PXAction) this.JournalEntry.reclassify).SetEnabled(flag);
  }

  private void WarnIfMissingTaxID(GLTran tran)
  {
    bool flag = false;
    if (tran != null)
    {
      int? nullable = tran.AccountID;
      if (nullable.HasValue)
      {
        nullable = tran.SubID;
        if (nullable.HasValue && tran.TaxID == null)
        {
          if (PXSelectBase<PX.Objects.TX.Tax, PXSelect<PX.Objects.TX.Tax, Where2<Where<PX.Objects.TX.Tax.purchTaxAcctID, Equal<Required<GLTran.accountID>>, And<PX.Objects.TX.Tax.purchTaxSubID, Equal<Required<GLTran.subID>>>>, Or<Where<PX.Objects.TX.Tax.salesTaxAcctID, Equal<Required<GLTran.accountID>>, And<PX.Objects.TX.Tax.salesTaxSubID, Equal<Required<GLTran.subID>>>>>>>.Config>.Select((PXGraph) this.JournalEntry, new object[4]
          {
            (object) tran.AccountID,
            (object) tran.SubID,
            (object) tran.AccountID,
            (object) tran.SubID
          }).Count > 0 && tran.TaxID == null)
            flag = true;
        }
      }
    }
    if (tran == null)
      return;
    if (flag)
      ((PXCache) this.GLTranCache).RaiseExceptionHandling<GLTran.taxID>((object) tran, (object) null, (Exception) new PXSetPropertyException("Account is associated with one or more taxes, but Tax ID is not specified", (PXErrorLevel) 2));
    else
      ((PXCache) this.GLTranCache).RaiseExceptionHandling<GLTran.taxID>((object) tran, (object) null, (Exception) null);
  }

  public bool CanReverseBatch(Batch batch)
  {
    GLTran glTran1 = (GLTran) null;
    bool? nullable;
    if (batch.Module == "CM")
    {
      nullable = batch.AutoReverse;
      if (!nullable.GetValueOrDefault())
      {
        nullable = batch.AutoReverseCopy;
        if (!nullable.GetValueOrDefault())
          glTran1 = PXResultset<GLTran>.op_Implicit(PXSelectBase<GLTran, PXSelect<GLTran, Where<GLTran.module, Equal<Required<Batch.module>>, And<GLTran.batchNbr, Equal<Required<Batch.batchNbr>>, And<GLTran.tranType, Equal<Required<GLTran.tranType>>>>>>.Config>.Select((PXGraph) this.JournalEntry, new object[3]
          {
            (object) batch.Module,
            (object) batch.BatchNbr,
            (object) "REV"
          }));
      }
    }
    nullable = batch.Released;
    bool flag = nullable.GetValueOrDefault() && (batch.Module != "CM" || glTran1 != null) && batch.BatchType != "T";
    if (batch.BatchType == "RCL")
    {
      GLTran glTran2 = PXResultset<GLTran>.op_Implicit(PXSelectBase<GLTran, PXSelect<GLTran, Where<GLTran.module, Equal<Required<Batch.module>>, And<GLTran.batchNbr, Equal<Required<Batch.batchNbr>>, And<GLTran.reclassBatchNbr, IsNotNull>>>>.Config>.Select((PXGraph) this.JournalEntry, new object[2]
      {
        (object) batch.Module,
        (object) batch.BatchNbr
      }));
      flag = flag && glTran2 == null;
    }
    return flag;
  }
}

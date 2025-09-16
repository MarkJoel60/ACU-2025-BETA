// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DraftScheduleMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.DR;

[Serializable]
public class DraftScheduleMaint : PXGraph<
#nullable disable
DraftScheduleMaint, DRSchedule>
{
  public PXSelect<DRSchedule> Schedule;
  public PXSelect<DRSchedule, Where<DRSchedule.scheduleID, Equal<Current<DRSchedule.scheduleID>>>> DocumentProperties;
  public PXSelectJoin<DRScheduleDetail, LeftJoin<DRSchedule, On<DRScheduleDetail.scheduleID, Equal<DRSchedule.scheduleID>>>, Where<DRScheduleDetail.scheduleID, Equal<Current<DRSchedule.scheduleID>>, And<Where<DRScheduleDetail.isResidual, Equal<False>, Or2<Where<DRScheduleDetail.defCode, IsNotNull>, And<FeatureInstalled<FeaturesSet.aSC606>>>>>>> Components;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<PX.Objects.AR.ARTran, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.AR.ARTran.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>, LeftJoin<INComponent, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<INComponent.inventoryID>>, LeftJoin<DRScheduleDetail, On<PX.Objects.AR.ARTran.tranType, Equal<DRScheduleDetail.docType>, And<PX.Objects.AR.ARTran.refNbr, Equal<DRScheduleDetail.refNbr>, And<PX.Objects.AR.ARTran.lineNbr, Equal<DRScheduleDetail.lineNbr>, And<Where<INComponent.componentID, Equal<DRScheduleDetail.componentID>, Or<PX.Objects.IN.InventoryItem.inventoryID, Equal<DRScheduleDetail.componentID>>>>>>>>>>, Where<DRScheduleDetail.scheduleID, Equal<Current<DRSchedule.scheduleID>>, And<PX.Objects.AR.ARTran.tranType, Equal<Current<DRSchedule.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<PX.Objects.AR.ARTran.deferredCode, IsNotNull, And<Current<DRSchedule.module>, Equal<BatchModule.moduleAR>, And<Current<DRSchedule.refNbr>, IsNotNull, And<Current<DRSchedule.lineNbr>, IsNull, And<Current<DRSchedule.isOverridden>, NotEqual<True>>>>>>>>>, OrderBy<Asc<DRScheduleDetail.detailLineNbr>>> ReallocationPool;
  public PXSelectJoin<DRScheduleDetail, LeftJoin<PX.Objects.AR.ARTran, On<DRScheduleDetail.docType, Equal<PX.Objects.AR.ARTran.tranType>, And<DRScheduleDetail.refNbr, Equal<PX.Objects.AR.ARTran.refNbr>, And<DRScheduleDetail.lineNbr, Equal<PX.Objects.AR.ARTran.lineNbr>>>>, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.AR.ARTran.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>, LeftJoin<INComponent, On<DRScheduleDetail.componentID, Equal<INComponent.componentID>, And<PX.Objects.IN.InventoryItem.inventoryID, Equal<INComponent.inventoryID>>>>>>, Where<DRScheduleDetail.scheduleID, Equal<Current<DRSchedule.scheduleID>>, And<PX.Objects.AR.ARTran.tranType, Equal<Current<DRSchedule.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<PX.Objects.AR.ARTran.deferredCode, IsNotNull, And<Current<DRSchedule.module>, Equal<BatchModule.moduleAR>, And<Current<DRSchedule.refNbr>, IsNotNull, And<Current<DRSchedule.lineNbr>, IsNull, And<Current<DRSchedule.isOverridden>, NotEqual<True>>>>>>>>>, OrderBy<Asc<DRScheduleDetail.detailLineNbr>>> ReallocationPool_DR;
  public PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Current<DRSchedule.scheduleID>>, And<DRScheduleDetail.isResidual, Equal<True>>>> ResidualComponent;
  [PXImport(typeof (DRSchedule))]
  public PXSelect<DRScheduleTran, Where<DRScheduleTran.scheduleID, Equal<Optional<DRScheduleDetail.scheduleID>>, And<DRScheduleTran.componentID, Equal<Optional<DRScheduleDetail.componentID>>, And<DRScheduleTran.detailLineNbr, Equal<Optional<DRScheduleDetail.detailLineNbr>>, And<DRScheduleTran.lineNbr, NotEqual<Optional<DRScheduleDetail.creditLineNbr>>>>>>> Transactions;
  public PXSelect<DRScheduleTran, Where<DRScheduleTran.scheduleID, Equal<Current<DRScheduleDetail.scheduleID>>, And<DRScheduleTran.componentID, Equal<Current<DRScheduleDetail.componentID>>, And<DRScheduleTran.detailLineNbr, Equal<Current<DRScheduleDetail.detailLineNbr>>, And<DRScheduleTran.status, Equal<DRScheduleTranStatus.OpenStatus>, And<DRScheduleTran.lineNbr, NotEqual<Current<DRScheduleDetail.creditLineNbr>>>>>>>> OpenTransactions;
  public PXSelect<DRScheduleTran, Where<DRScheduleTran.scheduleID, Equal<Current<DRScheduleDetail.scheduleID>>, And<DRScheduleTran.componentID, Equal<Current<DRScheduleDetail.componentID>>, And<DRScheduleTran.detailLineNbr, Equal<Current<DRScheduleDetail.detailLineNbr>>, And<DRScheduleTran.status, Equal<DRScheduleTranStatus.ProjectedStatus>, And<DRScheduleTran.lineNbr, NotEqual<Current<DRScheduleDetail.creditLineNbr>>>>>>>> ProjectedTransactions;
  public PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Current<DRScheduleDetail.defCode>>>> DeferredCode;
  public PXSelect<DRExpenseBalance> ExpenseBalance;
  public PXSelect<DRExpenseProjectionAccum> ExpenseProjection;
  public PXSelect<DRRevenueBalance> RevenueBalance;
  public PXSelect<DRRevenueProjectionAccum> RevenueProjection;
  public PXSelectReadonly<DraftScheduleMaint.DRScheduleEx> Associated;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<DRSchedule.curyInfoID>>>> CurrencyInfo;
  public PXSelectJoin<DRScheduleDetail, InnerJoin<DRDeferredCode, On<DRScheduleDetail.defCode, Equal<DRDeferredCode.deferredCodeID>>>, Where2<Where<DRScheduleDetail.scheduleID, Equal<Current<DRSchedule.scheduleID>>>, And<Where<DRDeferredCode.method, Equal<DeferredMethodType.flexibleExactDays>, Or<DRDeferredCode.method, Equal<DeferredMethodType.flexibleProrateDays>>>>>> ComponentsWithFlexibleCodes;
  public PXSetup<DRSetup> Setup;
  [PXHidden]
  public PXSelect<INComponent> _dummyINComponent;
  [PXHidden]
  public PXSelect<PX.Objects.AR.ARTran> _dummyARTran;
  public PXAction<DRSchedule> viewDoc;
  public PXAction<DRSchedule> viewSchedule;
  public PXAction<DRSchedule> viewBatch;
  public PXAction<DRSchedule> release;
  public PXAction<DRSchedule> recalculate;
  public PXAction<DRSchedule> generateTransactions;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public DraftScheduleMaint.Context CurrentContext { get; set; }

  public DraftScheduleMaint()
  {
    DRSetup current = ((PXSelectBase<DRSetup>) this.Setup).Current;
    ((PXSelectBase) this.Transactions).Cache.AllowInsert = false;
    OpenPeriodAttribute.SetValidatePeriod<DRSchedule.finPeriodID>(((PXSelectBase) this.DocumentProperties).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    PXImportAttribute attribute = ((PXSelectBase) this.Transactions).GetAttribute<PXImportAttribute>();
    if (attribute == null)
      return;
    attribute.RowImporting += new EventHandler<PXImportAttribute.RowImportingEventArgs>(this.DRScheduleTranRowImporting);
  }

  public virtual IEnumerable associated([PXDBString] string scheduleNbr)
  {
    IEnumerable enumerable = (IEnumerable) new List<DraftScheduleMaint.DRScheduleEx>();
    if (scheduleNbr == null)
      return enumerable;
    DRSchedule drSchedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.scheduleID, Equal<Current<DRScheduleDetail.scheduleID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (drSchedule == null)
      return enumerable;
    if (drSchedule.Module == "AR")
    {
      if (drSchedule.DocType == "CRM")
      {
        PX.Objects.AR.ARTran arTran = PXResultset<PX.Objects.AR.ARTran>.op_Implicit(PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<DRSchedule.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<PX.Objects.AR.ARTran.lineNbr, Equal<Required<DRSchedule.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) drSchedule.LineNbr
        }));
        if (arTran != null)
          return (IEnumerable) PXSelectBase<DraftScheduleMaint.DRScheduleEx, PXSelect<DraftScheduleMaint.DRScheduleEx, Where<DRSchedule.scheduleID, Equal<Required<DRSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) arTran.DefScheduleID
          });
      }
      else if (drSchedule.DocType == "INV" || drSchedule.DocType == "DRM")
      {
        List<DraftScheduleMaint.DRScheduleEx> drScheduleExList = new List<DraftScheduleMaint.DRScheduleEx>();
        foreach (PXResult<PX.Objects.AR.ARTran> pxResult in PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.defScheduleID, Equal<Current<DRScheduleDetail.scheduleID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
        {
          PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran>.op_Implicit(pxResult);
          DraftScheduleMaint.DRScheduleEx drScheduleEx = PXResultset<DraftScheduleMaint.DRScheduleEx>.op_Implicit(PXSelectBase<DraftScheduleMaint.DRScheduleEx, PXSelect<DraftScheduleMaint.DRScheduleEx, Where<DRSchedule.module, Equal<BatchModule.moduleAR>, And<DRSchedule.docType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<DRSchedule.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>, And<DRSchedule.lineNbr, Equal<Required<PX.Objects.AR.ARTran.lineNbr>>>>>>>.Config>.Select((PXGraph) this, new object[3]
          {
            (object) arTran.TranType,
            (object) arTran.RefNbr,
            (object) arTran.LineNbr
          }));
          if (drScheduleEx != null)
            drScheduleExList.Add(drScheduleEx);
        }
        return (IEnumerable) drScheduleExList;
      }
    }
    else if (drSchedule.Module == "AP")
    {
      if (drSchedule.DocType == "ADR")
      {
        APTran apTran = PXResultset<APTran>.op_Implicit(PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Current<DRScheduleDetail.docType>>, And<APTran.refNbr, Equal<Current<DRScheduleDetail.refNbr>>, And<APTran.lineNbr, Equal<Required<DRSchedule.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) drSchedule.LineNbr
        }));
        if (apTran != null)
          return (IEnumerable) PXSelectBase<DraftScheduleMaint.DRScheduleEx, PXSelect<DraftScheduleMaint.DRScheduleEx, Where<DRSchedule.scheduleID, Equal<Required<DRSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) apTran.DefScheduleID
          });
      }
      else if (drSchedule.DocType == "INV" || drSchedule.DocType == "ACR")
      {
        List<DraftScheduleMaint.DRScheduleEx> drScheduleExList = new List<DraftScheduleMaint.DRScheduleEx>();
        foreach (PXResult<APTran> pxResult in PXSelectBase<APTran, PXSelect<APTran, Where<APTran.defScheduleID, Equal<Current<DRScheduleDetail.scheduleID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
        {
          APTran apTran = PXResult<APTran>.op_Implicit(pxResult);
          DraftScheduleMaint.DRScheduleEx drScheduleEx = PXResultset<DraftScheduleMaint.DRScheduleEx>.op_Implicit(PXSelectBase<DraftScheduleMaint.DRScheduleEx, PXSelect<DraftScheduleMaint.DRScheduleEx, Where<DRSchedule.module, Equal<BatchModule.moduleAP>, And<DRSchedule.docType, Equal<Required<APTran.tranType>>, And<DRSchedule.refNbr, Equal<Required<APTran.refNbr>>, And<DRSchedule.lineNbr, Equal<Required<APTran.lineNbr>>>>>>>.Config>.Select((PXGraph) this, new object[3]
          {
            (object) apTran.TranType,
            (object) apTran.RefNbr,
            (object) apTran.LineNbr
          }));
          drScheduleExList.Add(drScheduleEx);
        }
        return (IEnumerable) drScheduleExList;
      }
    }
    return enumerable;
  }

  public virtual IEnumerable reallocationPool()
  {
    if (((PXSelectBase<DRSchedule>) this.Schedule).Current.IsRecalculated.GetValueOrDefault())
    {
      bool? isOverridden = ((PXSelectBase<DRSchedule>) this.Schedule).Current.IsOverridden;
      bool flag = false;
      if (isOverridden.GetValueOrDefault() == flag & isOverridden.HasValue)
        return this.reallocationPoolDR();
    }
    return GraphHelper.QuickSelect(((PXSelectBase) this.ReallocationPool).View);
  }

  private IEnumerable reallocationPoolDR()
  {
    foreach (PXResult<DRScheduleDetail, PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, INComponent> pxResult in GraphHelper.QuickSelect(((PXSelectBase) this.ReallocationPool_DR).View))
      yield return (object) new PXResult<PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, INComponent, DRScheduleDetail>(PXResult<DRScheduleDetail, PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, INComponent>.op_Implicit(pxResult), PXResult<DRScheduleDetail, PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, INComponent>.op_Implicit(pxResult), PXResult<DRScheduleDetail, PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, INComponent>.op_Implicit(pxResult), PXResult<DRScheduleDetail, PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem, INComponent>.op_Implicit(pxResult));
  }

  [PXUIField(DisplayName = "View Document", Enabled = false)]
  [PXButton(IsLockedOnToolbar = true)]
  public virtual IEnumerable ViewDoc(PXAdapter adapter)
  {
    if (((PXSelectBase<DRSchedule>) this.Schedule).Current != null)
      DRRedirectHelper.NavigateToOriginalDocument((PXGraph) this, ((PXSelectBase<DRSchedule>) this.Schedule).Current);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Schedule")]
  [PXButton]
  public virtual IEnumerable ViewSchedule(PXAdapter adapter)
  {
    if (((PXSelectBase<DraftScheduleMaint.DRScheduleEx>) this.Associated).Current != null)
    {
      DraftScheduleMaint instance = PXGraph.CreateInstance<DraftScheduleMaint>();
      ((PXGraph) instance).Clear();
      ((PXSelectBase<DRSchedule>) instance.Schedule).Current = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.scheduleID, Equal<Required<DRSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<DraftScheduleMaint.DRScheduleEx>) this.Associated).Current.ScheduleID
      }));
      throw new PXRedirectRequiredException((PXGraph) instance, "View Referenced Schedule");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View GL Batch")]
  [PXButton(ImageKey = "DataEntry")]
  public virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
    ((PXGraph) instance).Clear();
    Batch batch = PXResultset<Batch>.op_Implicit(PXSelectBase<Batch, PXSelect<Batch, Where<Batch.module, Equal<BatchModule.moduleDR>, And<Batch.batchNbr, Equal<Current<DRScheduleTran.batchNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (batch != null)
    {
      ((PXSelectBase<Batch>) instance.BatchModule).Current = batch;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, nameof (ViewBatch));
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Release", Enabled = false)]
  [PXButton(IsLockedOnToolbar = true)]
  public virtual IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    DraftScheduleMaint.\u003C\u003Ec__DisplayClass40_0 cDisplayClass400 = new DraftScheduleMaint.\u003C\u003Ec__DisplayClass40_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass400.scheduleList = adapter.Get<DRSchedule>().Where<DRSchedule>((Func<DRSchedule, bool>) (schedule => schedule.IsDraft.GetValueOrDefault())).ToList<DRSchedule>();
    // ISSUE: reference to a compiler-generated field
    if (!cDisplayClass400.scheduleList.Any<DRSchedule>())
      throw new PXException("The schedule is already released.");
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass400, __methodptr(\u003CRelease\u003Eb__1)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass400.scheduleList;
  }

  [PXUIField(DisplayName = "Recalculate", Visible = false, Enabled = false)]
  [PXButton]
  public virtual IEnumerable Recalculate(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "Generate Transactions", Enabled = false)]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable GenerateTransactions(PXAdapter adapter)
  {
    if (((PXSelectBase<DRScheduleDetail>) this.Components).Current != null)
    {
      DRDeferredCode deferralCode = PXResultset<DRDeferredCode>.op_Implicit(((PXSelectBase<DRDeferredCode>) this.DeferredCode).Select(Array.Empty<object>()));
      if (deferralCode != null)
      {
        IEnumerable<PXResult<DRScheduleTran>> source = ((IEnumerable<PXResult<DRScheduleTran>>) ((PXSelectBase<DRScheduleTran>) this.Transactions).Select(Array.Empty<object>())).AsEnumerable<PXResult<DRScheduleTran>>();
        if (!source.Any<PXResult<DRScheduleTran>>())
        {
          this.CreateTransactions(deferralCode);
        }
        else
        {
          if (((PXSelectBase<DRScheduleDetail>) this.Components).Current.Status != "D" && GraphHelper.RowCast<DRScheduleTran>((IEnumerable) source).Any<DRScheduleTran>((Func<DRScheduleTran, bool>) (t => t.Status == "P")))
            throw new PXException("The transactions cannot be regenerated because some transactions have been already posted.");
          if (((PXSelectBase) this.Components).View.Ask((object) ((PXSelectBase<DRScheduleDetail>) this.Components).Current, "Confirmation", "Transactions already exist. Do you want to recreate them?", (MessageButtons) 4, (MessageIcon) 2) == 6)
            this.CreateTransactions(deferralCode);
        }
      }
    }
    return adapter.Get();
  }

  [PXMergeAttributes]
  [PXDefault]
  protected virtual void DRScheduleDetail_ComponentID_CacheAttached(PXCache sender)
  {
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    if (viewName == "Schedule" && keys[(object) "ScheduleID"] == null)
    {
      foreach (DRSchedule drSchedule in ((PXSelectBase) this.Schedule).Cache.Inserted)
        keys[(object) "ScheduleID"] = (object) drSchedule.ScheduleID;
    }
    return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
  }

  /// <summary>
  /// Using the document type and reference number specified in the
  /// provided schedule object, searches for the existing AR / AP
  /// document. If it exists, returns the business account ID
  /// that is specified in the document.
  /// </summary>
  private int? GetDocumentBusinessAccountID(DRSchedule schedule)
  {
    int? businessAccountId = new int?();
    if (schedule.Module == "AR")
      businessAccountId = (int?) PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Current<DRSchedule.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Current<DRSchedule.refNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))?.CustomerID;
    else if (schedule.Module == "AP")
      businessAccountId = (int?) PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(PXSelectBase<PX.Objects.AP.APInvoice, PXSelect<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APInvoice.docType, Equal<Current<DRSchedule.docType>>, And<PX.Objects.AP.APInvoice.refNbr, Equal<Current<DRSchedule.refNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))?.VendorID;
    return businessAccountId;
  }

  /// <summary>
  /// Checks that there are no other deferral schedules with the same
  /// combination of document type, reference number, and line number.
  /// If there are such schedules, throws an error.
  /// </summary>
  /// <param name="sender">A cache of <see cref="T:PX.Objects.DR.DRSchedule" /> objects.</param>
  /// <param name="deferralSchedule">The deferral schedule record to validate.</param>
  /// <param name="lineNumberToCheck">
  /// Optional line number to check. May be different from the line
  /// number in the <paramref name="deferralSchedule" /> object
  /// in case when this method is called e.g. from within the
  /// FieldUpdating event. If <c>null</c>, then the line number
  /// value held by <paramref name="deferralSchedule" /> will be used.
  /// </param>
  private void VerifyNoDuplicateSchedules(
    PXCache sender,
    DRSchedule deferralSchedule,
    int? lineNumberToCheck = null)
  {
    lineNumberToCheck = lineNumberToCheck ?? deferralSchedule.LineNbr;
    DRSchedule drSchedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.module, Equal<Required<DRSchedule.module>>, And<DRSchedule.docType, Equal<Required<DRSchedule.docType>>, And<DRSchedule.refNbr, Equal<Required<DRSchedule.refNbr>>, And<DRSchedule.lineNbr, Equal<Required<DRSchedule.lineNbr>>, And<DRSchedule.scheduleID, NotEqual<Required<DRSchedule.scheduleID>>>>>>>>.Config>.Select((PXGraph) this, new object[5]
    {
      (object) deferralSchedule.Module,
      (object) deferralSchedule.DocType,
      (object) deferralSchedule.RefNbr,
      (object) lineNumberToCheck,
      (object) deferralSchedule.ScheduleID
    }));
    if (drSchedule == null)
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException("For the selected line, the deferral schedule {0} already exists.", new object[1]
    {
      (object) drSchedule.ScheduleNbr
    });
    if (sender.RaiseExceptionHandling<DRSchedule.lineNbr>((object) deferralSchedule, (object) lineNumberToCheck, (Exception) propertyException))
      throw propertyException;
  }

  /// <summary>
  /// When both document reference number and line number
  /// are specified, checks that there exists a document line
  /// with such number. Note that this method does not throw an
  /// error if the line number is <c>null</c> -- use a separate
  /// check if needed.
  /// </summary>
  /// <param name="sender">A cache of <see cref="T:PX.Objects.DR.DRSchedule" /> objects.</param>
  /// <param name="deferralSchedule">The deferral schedule record to validate.</param>
  /// <param name="lineNumberToCheck">
  /// Optional line number to check. May be different from the line
  /// number in the <paramref name="deferralSchedule" /> object
  /// in case when this method is called e.g. from within the
  /// FieldUpdating event. If <c>null</c>, then the line number
  /// value held by <paramref name="deferralSchedule" /> will be used.
  /// </param>
  private void VerifyDocumentLineExists(
    PXCache sender,
    DRSchedule deferralSchedule,
    int? lineNumberToCheck = null)
  {
    lineNumberToCheck = lineNumberToCheck ?? deferralSchedule.LineNbr;
    if (!lineNumberToCheck.HasValue)
      return;
    BqlCommand instance;
    if (deferralSchedule.Module == "AP")
    {
      instance = BqlCommand.CreateInstance(new System.Type[1]
      {
        typeof (Select<APTran, Where<APTran.refNbr, Equal<Required<APTran.refNbr>>, And<APTran.lineNbr, Equal<Required<APTran.lineNbr>>>>>)
      });
    }
    else
    {
      if (!(deferralSchedule.Module == "AR"))
        throw new PXException("Unexpected module specified. Only AP and AR are supported.");
      instance = BqlCommand.CreateInstance(new System.Type[1]
      {
        typeof (Select<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>, And<PX.Objects.AR.ARTran.lineNbr, Equal<Required<PX.Objects.AR.ARTran.lineNbr>>>>>)
      });
    }
    if (new PXView((PXGraph) this, true, instance).SelectSingle(new object[2]
    {
      (object) deferralSchedule.RefNbr,
      (object) lineNumberToCheck
    }) != null)
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException("The specified line number does not exist.");
    if (sender.RaiseExceptionHandling<DRSchedule.lineNbr>((object) deferralSchedule, (object) lineNumberToCheck, (Exception) propertyException))
      throw propertyException;
  }

  protected int? FillBranch(DRScheduleDetail scheduleDetail)
  {
    int? branchId = scheduleDetail.BranchID;
    if (((PXSelectBase<DRSchedule>) this.Schedule).Current == null)
      return scheduleDetail.BranchID;
    if (((PXSelectBase<DRSchedule>) this.Schedule).Current.RefNbr == null)
    {
      if (!scheduleDetail.BranchID.HasValue)
        branchId = ((PXGraph) this).Accessinfo.BranchID;
      return branchId;
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.aSC606>() && ((PXSelectBase<DRSchedule>) this.Schedule).Current.IsCustom.GetValueOrDefault() && ((PXSelectBase<DRSchedule>) this.Schedule).Current.RefNbr != null)
    {
      if (!scheduleDetail.BranchID.HasValue)
        branchId = ((PXGraph) this).Accessinfo.BranchID;
      return branchId;
    }
    if (((PXSelectBase<DRSchedule>) this.Schedule).Current.Module == "AP")
      return (int?) PXResultset<APTran>.op_Implicit(PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Current<DRSchedule.docType>>, And<APTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<APTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))?.BranchID;
    if (((PXSelectBase<DRSchedule>) this.Schedule).Current.Module == "AR")
      return (int?) PXResultset<PX.Objects.AR.ARTran>.op_Implicit(PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<DRSchedule.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<PX.Objects.AR.ARTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))?.BranchID;
    throw new PXException("Unexpected module specified. Only AP and AR are supported.");
  }

  protected virtual void DRSchedule_RefNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DRSchedule row))
      return;
    int? businessAccountId = this.GetDocumentBusinessAccountID(row);
    if (businessAccountId.HasValue)
      sender.SetValueExt<DRSchedule.bAccountID>((object) row, (object) businessAccountId);
    sender.SetValueExt<DRSchedule.lineNbr>((object) row, (object) null);
    PX.Objects.CM.CurrencyInfo currencyInfo1 = (PX.Objects.CM.CurrencyInfo) null;
    if (row.RefNbr != null && row.Module == "AP")
      currencyInfo1 = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelectJoin<PX.Objects.CM.CurrencyInfo, InnerJoin<PX.Objects.AP.APRegister, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.AP.APRegister.curyInfoID>>>, Where<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.DocType,
        (object) row.RefNbr
      }));
    if (row.RefNbr != null && row.Module == "AR")
      currencyInfo1 = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelectJoin<PX.Objects.CM.CurrencyInfo, InnerJoin<PX.Objects.AR.ARRegister, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.AR.ARRegister.curyInfoID>>>, Where<PX.Objects.AR.ARRegister.docType, Equal<Required<PX.Objects.AR.ARRegister.docType>>, And<PX.Objects.AR.ARRegister.refNbr, Equal<Required<PX.Objects.AR.ARRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.DocType,
        (object) row.RefNbr
      }));
    if (currencyInfo1 != null && row.BaseCuryID != currencyInfo1.BaseCuryID)
      sender.SetValue<DRSchedule.baseCuryID>((object) row, (object) (currencyInfo1?.BaseCuryID ?? ((PXGraph) this).Accessinfo.BaseCuryID));
    if (currencyInfo1 != null)
    {
      ((PXSelectBase) this.CurrencyInfo).Cache.Clear();
      PX.Objects.CM.CurrencyInfo copy1 = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(currencyInfo1);
      copy1.CuryInfoID = new long?();
      copy1.IsReadOnly = new bool?(false);
      PX.Objects.CM.CurrencyInfo copy2 = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.CurrencyInfo).Insert(copy1));
      DRSchedule copy3 = (DRSchedule) ((PXSelectBase) this.Schedule).Cache.CreateCopy((object) row);
      copy3.CuryInfoID = copy2.CuryInfoID;
      ((PXSelectBase<DRSchedule>) this.Schedule).Update(copy3).CuryID = copy2.CuryID;
    }
    else
    {
      if (e.OldValue == null)
        return;
      ((PXSelectBase) this.CurrencyInfo).Cache.Clear();
      PX.Objects.CM.CurrencyInfo instance = (PX.Objects.CM.CurrencyInfo) ((PXSelectBase) this.CurrencyInfo).Cache.CreateInstance();
      instance.IsReadOnly = new bool?(false);
      row.CuryID = ((PXGraph) this).Accessinfo.BaseCuryID;
      row.BaseCuryID = ((PXGraph) this).Accessinfo.BaseCuryID;
      PX.Objects.CM.CurrencyInfo currencyInfo2 = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.CurrencyInfo).Insert(instance);
      row.CuryInfoID = currencyInfo2.CuryInfoID;
      row.CuryID = currencyInfo2.CuryID;
      row.BaseCuryID = currencyInfo2.BaseCuryID;
    }
  }

  protected virtual void DRSchedule_DocDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    CurrencyInfoAttribute.SetEffectiveDate<DRSchedule.docDate>(sender, e);
  }

  protected virtual void DRSchedule_LineNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DRSchedule))
      return;
    foreach (PXResult<DRScheduleDetail, DRSchedule> pxResult in ((PXSelectBase<DRScheduleDetail>) this.Components).Select(Array.Empty<object>()))
    {
      DRScheduleDetail scheduleDetail = PXResult<DRScheduleDetail, DRSchedule>.op_Implicit(pxResult);
      scheduleDetail.BranchID = this.FillBranch(scheduleDetail);
      ((PXSelectBase) this.Components).Cache.Update((object) scheduleDetail);
    }
  }

  protected virtual void DRSchedule_BAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DRSchedule row))
      return;
    sender.SetDefaultExt<DRSchedule.bAccountLocID>((object) row);
    int? baccountId = row.BAccountID;
    int? businessAccountId = this.GetDocumentBusinessAccountID(row);
    if (baccountId.GetValueOrDefault() == businessAccountId.GetValueOrDefault() & baccountId.HasValue == businessAccountId.HasValue)
      return;
    sender.SetDefaultExt<DRSchedule.refNbr>((object) row);
  }

  protected virtual void DRSchedule_DocDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is DRSchedule))
      return;
    e.NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
  }

  protected virtual void DRSchedule_DocumentType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DRSchedule row))
      return;
    row.DocType = DRScheduleDocumentType.ExtractDocType(row.DocumentType);
    sender.SetValueExt<DRSchedule.module>((object) row, (object) DRScheduleDocumentType.ExtractModule(row.DocumentType));
    if (row.Module == "AR")
    {
      row.BAccountType = "CU";
    }
    else
    {
      if (!(row.Module == "AP"))
        return;
      row.BAccountType = "VE";
    }
  }

  /// <summary>
  /// If the schedule's module has changed, force deletion of all
  /// schedule components.
  /// </summary>
  protected virtual void DRSchedule_Module_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    DRSchedule row = e.Row as DRSchedule;
    string oldValue = e.OldValue as string;
    if (row == null || oldValue == null || !(row.Module != oldValue))
      return;
    EnumerableExtensions.ForEach<DRScheduleDetail>(GraphHelper.RowCast<DRScheduleDetail>((IEnumerable) ((PXSelectBase<DRScheduleDetail>) this.Components).Select(Array.Empty<object>())), (Action<DRScheduleDetail>) (scheduleDetail => ((PXSelectBase<DRScheduleDetail>) this.Components).Delete(scheduleDetail)));
  }

  protected virtual void DRSchedule_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is DRSchedule row))
      return;
    if (string.IsNullOrEmpty(row.Module))
      ((PXSelectBase) this.Components).Cache.RaiseExceptionHandling<DRScheduleDetail.documentType>((object) ((PXSelectBase<DRScheduleDetail>) this.Components).Current, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[documentType]"
      }));
    if (string.IsNullOrEmpty(row.FinPeriodID))
      ((PXSelectBase) this.Components).Cache.RaiseExceptionHandling<DRScheduleDetail.finPeriodID>((object) ((PXSelectBase<DRScheduleDetail>) this.Components).Current, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[finPeriodID]"
      }));
    this.VerifyNoDuplicateSchedules(sender, row);
    this.VerifyDocumentLineExists(sender, row);
    if (!PXAccess.FeatureInstalled<FeaturesSet.aSC606>())
      this.VerifyFlexibleComponentsConsistency(sender, row);
    if (PXAccess.FeatureInstalled<FeaturesSet.aSC606>() && row.Module == "AR")
      return;
    this.VerifyAndAdjustComponentAmounts();
  }

  protected virtual void DRSchedule_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is DRSchedule row))
      return;
    row.IsCustom = new bool?(true);
    row.IsDraft = new bool?(true);
  }

  protected virtual void DRSchedule_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    DRSchedule deferralSchedule = e.Row as DRSchedule;
    if (deferralSchedule == null)
      return;
    deferralSchedule.DocumentType = DRScheduleDocumentType.BuildDocumentType(deferralSchedule.Module, deferralSchedule.DocType);
    deferralSchedule.OrigLineAmt = new Decimal?();
    if (deferralSchedule.Module == "AR")
    {
      deferralSchedule.BAccountType = "CU";
      this.GetPostingAmount(deferralSchedule);
    }
    else
    {
      deferralSchedule.BAccountType = "VE";
      APTran documentLine = PXResultset<APTran>.op_Implicit(PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Current<DRSchedule.docType>>, And<APTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<APTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (documentLine != null)
        deferralSchedule.OrigLineAmt = new Decimal?(APReleaseProcess.GetExpensePostingAmount((PXGraph) this, documentLine).Base.GetValueOrDefault());
    }
    int num1 = !deferralSchedule.IsDraft.GetValueOrDefault() ? 1 : 0;
    bool valueOrDefault = deferralSchedule.IsCustom.GetValueOrDefault();
    bool flag1 = num1 == 0 & valueOrDefault;
    bool flag2 = deferralSchedule.RefNbr != null;
    bool hasValue = deferralSchedule.LineNbr.HasValue;
    ((PXAction) this.release).SetVisible(valueOrDefault);
    ((PXAction) this.release).SetEnabled(flag1 && this.AllComponentsHaveTransactions());
    ((PXAction) this.viewDoc).SetEnabled(flag2);
    PXUIFieldAttribute.SetDisplayName<DRSchedule.projectID>(sender, "Project/Contract");
    PXUIFieldAttribute.SetEnabled<DRSchedule.documentType>(sender, (object) deferralSchedule, flag1);
    PXUIFieldAttribute.SetEnabled<DRSchedule.finPeriodID>(sender, (object) deferralSchedule, flag1);
    PXUIFieldAttribute.SetEnabled<DRSchedule.refNbr>(sender, (object) deferralSchedule, flag1);
    PXUIFieldAttribute.SetEnabled<DRSchedule.lineNbr>(sender, (object) deferralSchedule, flag1 & flag2);
    PXUIFieldAttribute.SetEnabled<DRSchedule.docDate>(sender, (object) deferralSchedule, flag1);
    PXUIFieldAttribute.SetEnabled<DRSchedule.bAccountID>(sender, (object) deferralSchedule, flag1);
    PXUIFieldAttribute.SetEnabled<DRSchedule.bAccountLocID>(sender, (object) deferralSchedule, flag1);
    PXUIFieldAttribute.SetEnabled<DRSchedule.projectID>(sender, (object) deferralSchedule, flag1);
    PXUIFieldAttribute.SetEnabled<DRSchedule.taskID>(sender, (object) deferralSchedule, flag1);
    PXUIFieldAttribute.SetEnabled<DRSchedule.termStartDate>(sender, (object) deferralSchedule, flag1);
    PXUIFieldAttribute.SetEnabled<DRSchedule.termEndDate>(sender, (object) deferralSchedule, flag1);
    PXUIFieldAttribute.SetVisible<DRSchedule.origLineAmt>(sender, (object) deferralSchedule, hasValue);
    PXUIFieldAttribute.SetVisible<DRSchedule.finPeriodID>(sender, (object) deferralSchedule, !PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>());
    PXUIFieldAttribute.SetVisible<DRScheduleDetail.finPeriodID>(((PXSelectBase) this.Components).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>());
    bool flag3 = ((PXSelectBase<DRScheduleDetail>) this.ComponentsWithFlexibleCodes).Any<DRScheduleDetail>();
    PXUIFieldAttribute.SetRequired<DRSchedule.termStartDate>(sender, flag3);
    PXUIFieldAttribute.SetRequired<DRSchedule.termEndDate>(sender, flag3);
    sender.AllowDelete = deferralSchedule.IsDraft.GetValueOrDefault();
    PXCache cache1 = ((PXSelectBase) this.Components).Cache;
    bool? isDraft = deferralSchedule.IsDraft;
    int num2 = isDraft.GetValueOrDefault() ? 1 : 0;
    cache1.AllowInsert = num2 != 0;
    PXCache cache2 = ((PXSelectBase) this.Components).Cache;
    isDraft = deferralSchedule.IsDraft;
    int num3 = isDraft.GetValueOrDefault() ? 1 : 0;
    cache2.AllowUpdate = num3 != 0;
    PXCache cache3 = ((PXSelectBase) this.Components).Cache;
    isDraft = deferralSchedule.IsDraft;
    int num4 = isDraft.GetValueOrDefault() ? 1 : 0;
    cache3.AllowDelete = num4 != 0;
    EnumerableExtensions.ForEach<DRDocumentSelectorAttribute>(sender.GetAttributes<DRSchedule.refNbr>((object) deferralSchedule).OfType<DRDocumentSelectorAttribute>(), (Action<DRDocumentSelectorAttribute>) (attribute => attribute.ExcludeUnreleased = deferralSchedule.IsCustom.GetValueOrDefault()));
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.branchID>(((PXSelectBase) this.Components).Cache, (object) null, flag1 && !flag2 && !hasValue);
    PXUIFieldAttribute.SetReadOnly<DRScheduleDetail.branchID>(((PXSelectBase) this.Components).Cache, (object) null, !flag1 || flag2 || hasValue);
    this.SetScheduleStatus(deferralSchedule);
  }

  private void SetScheduleStatus(DRSchedule schedule)
  {
    if (schedule.IsDraft.GetValueOrDefault())
    {
      schedule.Status = "D";
    }
    else
    {
      schedule.Status = "C";
      foreach (PXResult<DRScheduleDetail, DRSchedule> pxResult in ((PXSelectBase<DRScheduleDetail>) this.Components).Select(Array.Empty<object>()))
      {
        DRScheduleDetail drScheduleDetail = PXResult<DRScheduleDetail, DRSchedule>.op_Implicit(pxResult);
        if ((drScheduleDetail != null ? (drScheduleDetail.IsOpen.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          schedule.Status = "O";
          break;
        }
      }
    }
  }

  private void GetPostingAmount(DRSchedule schedule)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.aSC606>())
    {
      ARReleaseProcess.Amount amount = !schedule.IsCustom.GetValueOrDefault() ? ASC606Helper.CalculateNetAmount((PXGraph) this, schedule) : ASC606Helper.CalculateSalesPostingAmount((PXGraph) this, schedule);
      schedule.OrigLineAmt = new Decimal?(0M);
      schedule.CuryNetTranPrice = amount.Cury;
      schedule.NetTranPrice = amount.Base;
    }
    else
    {
      PX.Objects.AR.ARTran documentLine = PXResultset<PX.Objects.AR.ARTran>.op_Implicit(PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<DRSchedule.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<PX.Objects.AR.ARTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (documentLine != null)
        schedule.OrigLineAmt = new Decimal?(ARReleaseProcess.GetSalesPostingAmount((PXGraph) this, documentLine).Base.GetValueOrDefault());
      else
        schedule.OrigLineAmt = new Decimal?(0M);
    }
  }

  protected virtual void DRSchedule_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    DRSchedule row = e.Row as DRSchedule;
    if (sender.ObjectsEqual<DRSchedule.documentType, DRSchedule.refNbr, DRSchedule.lineNbr, DRSchedule.bAccountID, DRSchedule.finPeriodID, DRSchedule.docDate>(e.Row, e.OldRow))
      return;
    foreach (PXResult<DRScheduleDetail, DRSchedule> pxResult in ((PXSelectBase<DRScheduleDetail>) this.Components).Select(Array.Empty<object>()))
    {
      DRScheduleDetail copy = (DRScheduleDetail) ((PXGraph) this).Caches[typeof (DRScheduleDetail)].CreateCopy((object) PXResult<DRScheduleDetail, DRSchedule>.op_Implicit(pxResult));
      this.SynchronizeDetailPropertiesFromSchedule(row, copy);
      ((PXSelectBase<DRScheduleDetail>) this.Components).Update(copy);
    }
  }

  protected virtual void DRSchedule_LineNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is DRSchedule row) || row.RefNbr == null)
      return;
    this.VerifyNoDuplicateSchedules(sender, row, e.NewValue as int?);
    this.VerifyDocumentLineExists(sender, row, e.NewValue as int?);
  }

  private void SynchronizeDetailPropertiesFromSchedule(
    DRSchedule deferralSchedule,
    DRScheduleDetail scheduleDetail)
  {
    scheduleDetail.Module = deferralSchedule.Module;
    scheduleDetail.DocumentType = deferralSchedule.DocumentType;
    scheduleDetail.DocType = deferralSchedule.DocType;
    scheduleDetail.RefNbr = deferralSchedule.RefNbr;
    if (!PXAccess.FeatureInstalled<FeaturesSet.aSC606>() || !(deferralSchedule.Module == "AR"))
      scheduleDetail.LineNbr = deferralSchedule.LineNbr;
    scheduleDetail.BAccountID = deferralSchedule.BAccountID;
    if (deferralSchedule.RefNbr == null)
      FinPeriodIDAttribute.SetPeriodsByMaster<DRScheduleDetail.finPeriodID>(((PXSelectBase) this.Components).Cache, (object) scheduleDetail, deferralSchedule.FinPeriodID);
    scheduleDetail.DocDate = deferralSchedule.DocDate;
  }

  private static void SynchronizeDeferralAmountWithTotalAmount(DRScheduleDetail scheduleDetail)
  {
    if (scheduleDetail == null || !(scheduleDetail.Status == "D") || scheduleDetail.IsResidual.GetValueOrDefault())
      return;
    scheduleDetail.CuryDefAmt = scheduleDetail.CuryTotalAmt;
    scheduleDetail.DefAmt = scheduleDetail.TotalAmt;
  }

  protected virtual void DRScheduleDetail_DocumentType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row))
      return;
    string module = DRScheduleDocumentType.ExtractModule(row.DocumentType);
    row.DocType = DRScheduleDocumentType.ExtractDocType(row.DocumentType);
    if (row.Module != module)
    {
      row.Module = module;
      row.DefCode = (string) null;
      row.DefAcctID = new int?();
      row.DefSubID = new int?();
      row.BAccountID = new int?();
      row.AccountID = new int?();
      row.SubID = new int?();
      PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<DRScheduleDetail.componentID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.ComponentID
      }));
      if (inventoryItem != null)
      {
        row.AccountID = row.Module == "AP" ? inventoryItem.COGSAcctID : inventoryItem.SalesAcctID;
        row.SubID = row.Module == "AP" ? inventoryItem.COGSSubID : inventoryItem.SalesSubID;
      }
    }
    row.RefNbr = (string) null;
  }

  /// <summary>
  /// Fills the schedule detail's amount as a residual between the
  /// original line amount and the sum of all other details' total amounts.
  /// </summary>
  protected virtual void DRScheduleDetail_TotalAmt_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail) || !((PXSelectBase<DRSchedule>) this.Schedule).Current.IsCustom.GetValueOrDefault())
      return;
    Decimal num = GraphHelper.RowCast<DRScheduleDetail>((IEnumerable) ((PXSelectBase<DRScheduleDetail>) this.Components).Select(Array.Empty<object>())).Select<DRScheduleDetail, Decimal>((Func<DRScheduleDetail, Decimal>) (detail => detail.TotalAmt.GetValueOrDefault())).Aggregate<Decimal, Decimal>(((PXSelectBase<DRSchedule>) this.Schedule).Current.OrigLineAmt.GetValueOrDefault(), (Func<Decimal, Decimal, Decimal>) ((accumulator, detailAmount) => accumulator - detailAmount));
    if (num < 0M)
      num = 0M;
    e.NewValue = (object) num;
  }

  protected virtual void DRScheduleDetail_CuryTotalAmt_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row) || !((PXSelectBase<DRSchedule>) this.Schedule).Current.IsCustom.GetValueOrDefault())
      return;
    Decimal? totalAmt = row.TotalAmt;
    if (!totalAmt.HasValue)
      return;
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    PXCache sender1 = sender;
    DRScheduleDetail scheduleDetail = row;
    totalAmt = row.TotalAmt;
    Decimal remainingAmount = totalAmt.Value;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal> curyTotalAmt = (ValueType) this.GetCuryTotalAmt(sender1, scheduleDetail, remainingAmount);
    defaultingEventArgs.NewValue = (object) curyTotalAmt;
  }

  protected virtual Decimal GetCuryTotalAmt(
    PXCache sender,
    DRScheduleDetail scheduleDetail,
    Decimal remainingAmount)
  {
    Decimal curyval = 0M;
    PXCurrencyAttribute.CuryConvCury<DRScheduleDetail.curyInfoID>(sender, (object) scheduleDetail, remainingAmount, out curyval);
    return curyval;
  }

  protected virtual void DRScheduleDetail_TotalAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row))
      return;
    Decimal? totalAmt = row.TotalAmt;
    if (totalAmt.HasValue)
    {
      DRScheduleDetail drScheduleDetail = row;
      PXCache sender1 = sender;
      DRScheduleDetail scheduleDetail = row;
      totalAmt = row.TotalAmt;
      Decimal remainingAmount = totalAmt.Value;
      Decimal? nullable = new Decimal?(this.GetCuryTotalAmt(sender1, scheduleDetail, remainingAmount));
      drScheduleDetail.CuryTotalAmt = nullable;
    }
    DraftScheduleMaint.SynchronizeDeferralAmountWithTotalAmount(row);
  }

  /// <summary>
  /// This is auto setting of the <see cref="T:PX.Objects.DR.DRScheduleDetail.curyDefAmt" /> field because of only the base currency are allowed for DR Schedules for now.
  /// </summary>
  protected virtual void DRScheduleDetail_DefAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row1))
      return;
    Decimal? nullable = row1.DefAmt;
    if (!nullable.HasValue)
      return;
    nullable = row1.CuryDefAmt;
    if (nullable.HasValue)
      return;
    Decimal num = 0M;
    PXCache sender1 = sender;
    DRScheduleDetail row2 = row1;
    nullable = row1.DefAmt;
    Decimal baseval = nullable.Value;
    ref Decimal local = ref num;
    PXCurrencyAttribute.CuryConvCury<DRScheduleDetail.curyInfoID>(sender1, (object) row2, baseval, out local);
    row1.CuryDefAmt = new Decimal?(num);
  }

  protected virtual void DRScheduleDetail_DefCode_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row))
      return;
    DRDeferredCode drDeferredCode = PXResultset<DRDeferredCode>.op_Implicit(PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Required<DRScheduleDetail.defCode>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.DefCode
    }));
    if (drDeferredCode == null)
      return;
    row.DefCode = drDeferredCode.DeferredCodeID;
    row.DefAcctID = drDeferredCode.AccountID;
    row.DefSubID = drDeferredCode.SubID;
  }

  protected virtual void DRScheduleDetail_DocDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail))
      return;
    e.NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
  }

  protected virtual void DRScheduleDetail_DefCodeType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<DRSchedule>) this.Schedule).Current == null)
      return;
    e.NewValue = ((PXSelectBase<DRSchedule>) this.Schedule).Current.Module == "AP" ? (object) "E" : (object) "I";
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void DRScheduleDetail_BAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    int? nullable;
    int num1;
    if (!(e.Row is DRScheduleDetail row))
    {
      num1 = 1;
    }
    else
    {
      nullable = row.BAccountID;
      num1 = !nullable.HasValue ? 1 : 0;
    }
    if (num1 != 0)
      return;
    nullable = row.ComponentID;
    if (nullable.HasValue)
    {
      nullable = row.ComponentID;
      int num2 = 0;
      if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
      {
        nullable = row.AccountID;
        if (nullable.HasValue)
          return;
      }
    }
    if (row.Module == "AP")
    {
      PX.Objects.CR.Location location = (PX.Objects.CR.Location) ((PXResult) PXSelectBase<PX.Objects.AP.Vendor, PXSelectJoin<PX.Objects.AP.Vendor, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.AP.Vendor.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>>, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<DRScheduleDetail.bAccountID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())[0])[1];
      nullable = location.VExpenseAcctID;
      if (!nullable.HasValue)
        return;
      row.AccountID = location.VExpenseAcctID;
      row.SubID = location.VExpenseSubID;
    }
    else
    {
      if (!(row.Module == "AR"))
        return;
      PX.Objects.CR.Location location = (PX.Objects.CR.Location) ((PXResult) PXSelectBase<PX.Objects.AR.Customer, PXSelectJoin<PX.Objects.AR.Customer, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.AR.Customer.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<DRScheduleDetail.bAccountID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())[0])[1];
      nullable = location.CSalesAcctID;
      if (!nullable.HasValue)
        return;
      row.AccountID = location.CSalesAcctID;
      row.SubID = location.CSalesSubID;
    }
  }

  protected virtual void DRScheduleDetail_ScheduleID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail))
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void DRScheduleDetail_BranchID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    DRScheduleDetail row = e.Row as DRScheduleDetail;
    e.NewValue = (object) this.FillBranch(row);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void DRScheduleDetail_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row) || ((PXSelectBase<DRSchedule>) this.Schedule).Current == null)
      return;
    row.ComponentID = new int?(row.ComponentID.GetValueOrDefault());
    row.ScheduleID = ((PXSelectBase<DRSchedule>) this.Schedule).Current.ScheduleID;
    if (PXResultset<DRScheduleDetail>.op_Implicit(PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Current<DRSchedule.scheduleID>>, And<DRScheduleDetail.componentID, Equal<Required<DRScheduleDetail.componentID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ComponentID.GetValueOrDefault()
    })) == null)
      return;
    object valueExt = sender.GetValueExt<DRScheduleDetail.componentID>((object) row);
    sender.RaiseExceptionHandling<DRScheduleDetail.componentID>((object) row, (object) valueExt?.ToString(), (Exception) new PXSetPropertyException("Component ID must be unique within components."));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void DRScheduleDetail_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (this.CurrentContext == DraftScheduleMaint.Context.Recalculate || !(e.Row is DRScheduleDetail row) || ((PXSelectBase<DRSchedule>) this.Schedule).Current == null)
      return;
    row.Status = "D";
    row.IsCustom = new bool?(true);
    this.SynchronizeDetailPropertiesFromSchedule(((PXSelectBase<DRSchedule>) this.Schedule).Current, row);
    DraftScheduleMaint.SynchronizeDeferralAmountWithTotalAmount(row);
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<DRScheduleDetail.componentID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ComponentID
    }));
    if (inventoryItem == null)
      return;
    row.AccountID = row.Module == "AP" ? inventoryItem.COGSAcctID : inventoryItem.SalesAcctID;
    row.SubID = row.Module == "AP" ? inventoryItem.COGSSubID : inventoryItem.SalesSubID;
    DRDeferredCode drDeferredCode = PXResultset<DRDeferredCode>.op_Implicit(PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Required<DRScheduleDetail.defCode>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) inventoryItem.DeferredCode
    }));
    if (drDeferredCode == null)
      return;
    row.DefCode = drDeferredCode.DeferredCodeID;
    row.DefAcctID = drDeferredCode.AccountID;
    row.DefSubID = drDeferredCode.SubID;
  }

  protected virtual void DRScheduleDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!((PXGraph) this).IsCopyPasteContext)
      return;
    DRScheduleDetail row = e.Row as DRScheduleDetail;
    sender.SetValue<DRScheduleDetail.status>((object) row, (object) "D");
  }

  protected virtual void DRScheduleDetail_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row) || row.IsResidual.GetValueOrDefault() || !string.IsNullOrEmpty(row.DefCode))
      return;
    sender.RaiseExceptionHandling<DRScheduleDetail.defCode>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
  }

  protected virtual void DRScheduleDetail_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row))
      return;
    row.DocumentType = DRScheduleDocumentType.BuildDocumentType(row.Module, row.DocType);
    row.DefTotal = new Decimal?(this.SumOpenAndProjectedTransactions(row));
    GraphHelper.MarkUpdated(sender, (object) row);
    bool valueOrDefault = row.IsCustom.GetValueOrDefault();
    int? componentId = row.ComponentID;
    componentId.GetValueOrDefault();
    int num = componentId.HasValue ? 1 : 0;
    bool flag1 = !row.ComponentID.HasValue;
    bool flag2 = ((PXSelectBase) this.Transactions).View.SelectSingleBound(new object[1]
    {
      (object) row
    }, Array.Empty<object>()) != null;
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.componentID>(sender, (object) row, flag1 || valueOrDefault && !flag2);
    bool flag3 = ((PXSelectBase<DRScheduleDetail>) this.Components).Any<DRScheduleDetail>();
    ((PXSelectBase) this.Transactions).Cache.AllowInsert = ((PXSelectBase) this.Transactions).Cache.AllowUpdate = ((PXSelectBase) this.Transactions).Cache.AllowDelete = row.Status != "C" & flag3;
    ((PXAction) this.generateTransactions).SetEnabled(!GraphHelper.RowCast<DRScheduleTran>((IEnumerable) ((PXSelectBase<DRScheduleTran>) this.Transactions).Select(Array.Empty<object>())).Any<DRScheduleTran>((Func<DRScheduleTran, bool>) (transaction => transaction.Status == "P")));
  }

  protected virtual void DRScheduleTran_RecDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DRScheduleTran row))
      return;
    FinPeriod finPeriodByDate = this.FinPeriodRepository.GetFinPeriodByDate(row.RecDate, PXAccess.GetParentOrganizationID(row.BranchID));
    row.FinPeriodID = finPeriodByDate.FinPeriodID;
    row.TranPeriodID = finPeriodByDate.MasterFinPeriodID;
  }

  protected virtual void DRScheduleTran_Amount_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.EnsureCurrentComponentIsUpdated();
  }

  protected virtual void DRScheduleTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is DRScheduleTran row))
      return;
    PXUIFieldAttribute.SetEnabled<DRScheduleTran.recDate>(sender, (object) row, row.Status == "O" || row.Status == "J");
    PXUIFieldAttribute.SetEnabled<DRScheduleTran.amount>(sender, (object) row, row.Status == "O" || row.Status == "J");
    PXUIFieldAttribute.SetEnabled<DRScheduleTran.accountID>(sender, (object) row, row.Status == "O" || row.Status == "J");
    PXUIFieldAttribute.SetEnabled<DRScheduleTran.subID>(sender, (object) row, row.Status == "O" || row.Status == "J");
    PXUIFieldAttribute.SetEnabled<DRScheduleTran.branchID>(sender, (object) row, false);
  }

  protected virtual void DRScheduleTran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    DRScheduleTran row = e.Row as DRScheduleTran;
    if (sender.ObjectsEqual<DRScheduleTran.finPeriodID, DRScheduleTran.accountID, DRScheduleTran.subID, DRScheduleTran.amount>(e.Row, e.OldRow) || ((PXSelectBase<DRScheduleDetail>) this.Components).Current == null || !(((PXSelectBase<DRScheduleDetail>) this.Components).Current.Status == "O"))
      return;
    this.Subtract((DRScheduleTran) e.OldRow);
    this.Add(row);
  }

  protected virtual void DRScheduleTranRowImporting(
    object sender,
    PXImportAttribute.RowImportingEventArgs e)
  {
    if (e.Mode != null || !e.Keys.Contains((object) "LineNbr"))
      return;
    DRScheduleTran drScheduleTran = PXResultset<DRScheduleTran>.op_Implicit(((PXSelectBase<DRScheduleTran>) this.Transactions).Search<DRScheduleTran.lineNbr>(e.Keys[(object) "LineNbr"], Array.Empty<object>()));
    if (drScheduleTran == null || !(drScheduleTran.Status != "O") || !(drScheduleTran.Status != "J"))
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void DRScheduleTran_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is DRScheduleTran row) || ((PXSelectBase<DRScheduleDetail>) this.Components).Current == null)
      return;
    row.BranchID = ((PXSelectBase<DRScheduleDetail>) this.Components).Current.BranchID;
  }

  protected virtual void DRScheduleTran_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    DRScheduleTran row = e.Row as DRScheduleTran;
    this.EnsureCurrentComponentIsUpdated();
    if (row == null || ((PXSelectBase<DRScheduleDetail>) this.Components).Current == null || !(((PXSelectBase<DRScheduleDetail>) this.Components).Current.Status == "O"))
      return;
    this.Add(row);
  }

  protected virtual void DRScheduleTran_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (e.Row is DRScheduleTran row && row.Status == "P")
      throw new PXException("Posted transactions cannot be deleted from the schedule.");
  }

  protected virtual void DRScheduleTran_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    DRScheduleTran row = e.Row as DRScheduleTran;
    this.EnsureCurrentComponentIsUpdated();
    if (row == null || ((PXSelectBase<DRScheduleDetail>) this.Components).Current == null || !(((PXSelectBase<DRScheduleDetail>) this.Components).Current.Status == "O"))
      return;
    this.Subtract(row);
  }

  private void EnsureCurrentComponentIsUpdated()
  {
    if (((PXSelectBase<DRScheduleDetail>) this.Components).Current == null)
      return;
    GraphHelper.MarkUpdated(((PXSelectBase) this.Components).Cache, (object) ((PXSelectBase<DRScheduleDetail>) this.Components).Current);
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Document Line Quantity")]
  protected virtual void ARTran_Qty_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "MDA Component Quantity")]
  protected virtual void INComponent_Qty_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "MDA Component UOM")]
  protected virtual void INComponent_UOM_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "MDA Component ID")]
  protected virtual void INComponent_ComponentID_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Currency Ext. Price")]
  protected virtual void ARTran_CuryExtPrice_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Currency Amount")]
  protected virtual void ARTran_CuryTranAmt_CacheAttached(PXCache sender)
  {
  }

  private bool AllComponentsHaveTransactions()
  {
    bool flag = ((PXSelectBase<DRScheduleDetail>) this.Components).Any<DRScheduleDetail>();
    foreach (PXResult<DRScheduleDetail, DRSchedule> pxResult in ((PXSelectBase<DRScheduleDetail>) this.Components).Select(Array.Empty<object>()))
    {
      DRScheduleDetail drScheduleDetail = PXResult<DRScheduleDetail, DRSchedule>.op_Implicit(pxResult);
      if (!((PXSelectBase<DRScheduleTran>) this.Transactions).Any<DRScheduleTran>((object) drScheduleDetail.ScheduleID, (object) drScheduleDetail.ComponentID, (object) drScheduleDetail.DetailLineNbr, (object) drScheduleDetail.CreditLineNbr))
        return false;
    }
    return flag;
  }

  /// <summary>
  /// Performs a check if any of the schedule components are flexible. If so,
  /// verifies that term start date and term end date are present and consistent
  /// with each other, as well as marks them as required via <see cref="T:PX.Data.PXUIFieldAttribute" />.
  /// </summary>
  /// <param name="cache">A cache of <see cref="T:PX.Objects.DR.DRSchedule" /> records.</param>
  /// <param name="deferralSchedule">The current deferral schedule record.</param>
  private void VerifyFlexibleComponentsConsistency(PXCache cache, DRSchedule deferralSchedule)
  {
    bool flag = ((PXSelectBase<DRScheduleDetail>) this.ComponentsWithFlexibleCodes).Any<DRScheduleDetail>();
    if (flag && !deferralSchedule.TermStartDate.HasValue)
      cache.RaiseExceptionHandling<DRSchedule.termStartDate>((object) deferralSchedule, (object) null, (Exception) new PXSetPropertyException<DRSchedule.termStartDate>("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) "[termStartDate]"
      }));
    if (flag && !deferralSchedule.TermEndDate.HasValue)
      cache.RaiseExceptionHandling<DRSchedule.termEndDate>((object) deferralSchedule, (object) null, (Exception) new PXSetPropertyException<DRSchedule.termEndDate>("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) "[termEndDate]"
      }));
    if (!flag)
      return;
    DateTime? termStartDate = deferralSchedule.TermStartDate;
    DateTime? termEndDate = deferralSchedule.TermEndDate;
    if ((termStartDate.HasValue & termEndDate.HasValue ? (termStartDate.GetValueOrDefault() > termEndDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    cache.RaiseExceptionHandling<DRSchedule.termEndDate>((object) deferralSchedule, (object) null, (Exception) new PXSetPropertyException<DRSchedule.termEndDate>("Term End Date ({0:d}) cannot be earlier than Term Start Date ({1:d}).", (PXErrorLevel) 4, new object[2]
    {
      (object) deferralSchedule.TermEndDate,
      (object) deferralSchedule.TermStartDate
    }));
  }

  private void VerifyAndAdjustComponentAmounts()
  {
    if (((PXSelectBase<DRSchedule>) this.Schedule).Current?.RefNbr == null)
      return;
    Decimal? nullable1 = ((PXSelectBase<DRSchedule>) this.Schedule).Current.OrigLineAmt;
    if (nullable1.GetValueOrDefault() == 0M)
      return;
    Decimal num1 = 0M;
    foreach (PXResult<DRScheduleDetail, DRSchedule> pxResult in ((PXSelectBase<DRScheduleDetail>) this.Components).Select(Array.Empty<object>()))
    {
      DRScheduleDetail row = PXResult<DRScheduleDetail, DRSchedule>.op_Implicit(pxResult);
      Decimal num2 = num1;
      nullable1 = row.TotalAmt;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      num1 = num2 + valueOrDefault1;
      DRDeferredCode drDeferredCode = PXResultset<DRDeferredCode>.op_Implicit(PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Current<DRScheduleDetail.defCode>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if ((drDeferredCode == null ? 1 : (drDeferredCode.Method != "C" ? 1 : 0)) != 0)
      {
        Decimal num3 = this.SumOpenAndProjectedTransactions(row);
        Decimal num4 = num3;
        nullable1 = row.DefAmt;
        Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
        if (!(num4 == valueOrDefault2 & nullable1.HasValue) && ((PXSelectBase) this.Components).Cache.RaiseExceptionHandling<DRScheduleDetail.defTotal>((object) row, (object) num3, (Exception) new PXSetPropertyException("Sum of all open deferred transactions must be equal to Deferred Amount.")))
          throw new PXRowPersistingException(typeof (DRScheduleDetail.defTotal).Name, (object) num3, "Sum of all open deferred transactions must be equal to Deferred Amount.");
      }
    }
    DRScheduleDetail drScheduleDetail = ((PXSelectBase<DRScheduleDetail>) this.ResidualComponent).SelectSingle(Array.Empty<object>());
    Decimal? nullable2;
    if (drScheduleDetail != null)
    {
      ((PXSelectBase) this.ResidualComponent).Cache.RaiseRowSelected((object) drScheduleDetail);
      Decimal num5 = ((PXSelectBase<DRSchedule>) this.Schedule).Current.OrigLineAmt.GetValueOrDefault() - num1;
      if (num5 < 0M)
        throw new PXException("Can't update the schedule - residual amount will go negative. Please, adjust amounts for components.");
      nullable2 = drScheduleDetail.TotalAmt;
      Decimal num6 = num5;
      if (!(nullable2.GetValueOrDefault() == num6 & nullable2.HasValue))
      {
        drScheduleDetail.TotalAmt = new Decimal?(num5);
        drScheduleDetail = ((PXSelectBase<DRScheduleDetail>) this.ResidualComponent).Update(drScheduleDetail);
      }
      Decimal num7 = num1;
      nullable2 = drScheduleDetail.TotalAmt;
      Decimal valueOrDefault = nullable2.GetValueOrDefault();
      num1 = num7 + valueOrDefault;
    }
    nullable2 = ((PXSelectBase<DRSchedule>) this.Schedule).Current.OrigLineAmt;
    Decimal num8 = num1;
    if (!(nullable2.GetValueOrDefault() == num8 & nullable2.HasValue))
      throw new PXException("The sum of total amounts of all components must match the line amount.");
  }

  private void CreateTransactions(DRDeferredCode deferralCode)
  {
    if (((PXSelectBase<DRScheduleDetail>) this.Components).Current == null)
      return;
    foreach (PXResult<DRScheduleTran> pxResult in ((PXSelectBase<DRScheduleTran>) this.Transactions).Select(Array.Empty<object>()))
    {
      DRScheduleTran drScheduleTran = PXResult<DRScheduleTran>.op_Implicit(pxResult);
      if (((PXSelectBase<DRScheduleDetail>) this.Components).Current.Status != "D" && drScheduleTran.Status == "P")
        throw new PXException("The transactions cannot be regenerated because some transactions have been already posted.");
      ((PXSelectBase<DRScheduleTran>) this.Transactions).Delete(drScheduleTran);
    }
    if (((PXSelectBase<DRSchedule>) this.Schedule).Current == null || ((PXSelectBase<DRScheduleDetail>) this.Components).Current == null || deferralCode == null)
      return;
    foreach (DRScheduleTran transaction in (IEnumerable<DRScheduleTran>) this.GetTransactionsGenerator(deferralCode).GenerateTransactions(((PXSelectBase<DRSchedule>) this.Schedule).Current, ((PXSelectBase<DRScheduleDetail>) this.Components).Current))
      ((PXSelectBase<DRScheduleTran>) this.Transactions).Insert(transaction);
  }

  private void Subtract(DRScheduleTran tran)
  {
    if (PXResultset<DRDeferredCode>.op_Implicit(((PXSelectBase<DRDeferredCode>) this.DeferredCode).Select(Array.Empty<object>())).AccountType == "E")
    {
      this.SubtractExpenseFromProjection(tran);
      this.SubtractExpenseFromBalance(tran);
    }
    else
    {
      this.SubtractRevenueFromProjection(tran);
      this.SubtractRevenueFromBalance(tran);
    }
  }

  private void SubtractRevenueFromProjection(DRScheduleTran tran)
  {
    if (tran.FinPeriodID == null || tran.TranPeriodID == null)
      return;
    DRRevenueProjectionAccum revenueProjectionAccum1 = this.CreateDRRevenueProjectionAccum(tran.FinPeriodID);
    DRRevenueProjectionAccum revenueProjectionAccum2 = this.CreateDRRevenueProjectionAccum(tran.TranPeriodID);
    Decimal? nullable1 = revenueProjectionAccum1.PTDProjected;
    Decimal? nullable2 = tran.Amount;
    revenueProjectionAccum1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    DRRevenueProjectionAccum revenueProjectionAccum3 = revenueProjectionAccum2;
    nullable2 = revenueProjectionAccum3.TranPTDProjected;
    nullable1 = tran.Amount;
    revenueProjectionAccum3.TranPTDProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private void SubtractExpenseFromProjection(DRScheduleTran tran)
  {
    if (tran.FinPeriodID == null || tran.TranPeriodID == null)
      return;
    DRExpenseProjectionAccum expenseProjectionAccum1 = this.CreateDRExpenseProjectionAccum(tran.FinPeriodID);
    DRExpenseProjectionAccum expenseProjectionAccum2 = this.CreateDRExpenseProjectionAccum(tran.TranPeriodID);
    Decimal? nullable1 = expenseProjectionAccum1.PTDProjected;
    Decimal? nullable2 = tran.Amount;
    expenseProjectionAccum1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    DRExpenseProjectionAccum expenseProjectionAccum3 = expenseProjectionAccum2;
    nullable2 = expenseProjectionAccum3.TranPTDProjected;
    nullable1 = tran.Amount;
    expenseProjectionAccum3.TranPTDProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private void SubtractRevenueFromBalance(DRScheduleTran tran)
  {
    if (tran.FinPeriodID == null || tran.TranPeriodID == null)
      return;
    DRRevenueBalance drRevenueBalance1 = this.CreateDRRevenueBalance(tran.FinPeriodID);
    DRRevenueBalance drRevenueBalance2 = this.CreateDRRevenueBalance(tran.TranPeriodID);
    Decimal? nullable1 = drRevenueBalance1.PTDProjected;
    Decimal? nullable2 = tran.Amount;
    drRevenueBalance1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    nullable2 = drRevenueBalance1.EndProjected;
    nullable1 = tran.Amount;
    drRevenueBalance1.EndProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    DRRevenueBalance drRevenueBalance3 = drRevenueBalance2;
    nullable1 = drRevenueBalance3.TranPTDProjected;
    nullable2 = tran.Amount;
    drRevenueBalance3.TranPTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    DRRevenueBalance drRevenueBalance4 = drRevenueBalance2;
    nullable2 = drRevenueBalance4.TranEndProjected;
    nullable1 = tran.Amount;
    drRevenueBalance4.TranEndProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private void SubtractExpenseFromBalance(DRScheduleTran tran)
  {
    if (tran.FinPeriodID == null || tran.TranPeriodID == null)
      return;
    DRExpenseBalance drExpenseBalance1 = this.CreateDRExpenseBalance(tran.FinPeriodID);
    DRExpenseBalance drExpenseBalance2 = this.CreateDRExpenseBalance(tran.TranPeriodID);
    Decimal? nullable1 = drExpenseBalance1.PTDProjected;
    Decimal? nullable2 = tran.Amount;
    drExpenseBalance1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    nullable2 = drExpenseBalance1.EndProjected;
    nullable1 = tran.Amount;
    drExpenseBalance1.EndProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    DRExpenseBalance drExpenseBalance3 = drExpenseBalance2;
    nullable1 = drExpenseBalance3.PTDProjected;
    nullable2 = tran.Amount;
    drExpenseBalance3.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    DRExpenseBalance drExpenseBalance4 = drExpenseBalance2;
    nullable2 = drExpenseBalance4.EndProjected;
    nullable1 = tran.Amount;
    drExpenseBalance4.EndProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private void Add(DRScheduleTran tran)
  {
    if (PXResultset<DRDeferredCode>.op_Implicit(((PXSelectBase<DRDeferredCode>) this.DeferredCode).Select(Array.Empty<object>())).AccountType == "E")
    {
      this.AddExpenseToProjection(tran);
      this.AddExpenseToBalance(tran);
    }
    else
    {
      this.AddRevenueToProjection(tran);
      this.AddRevenueToBalance(tran);
    }
  }

  private void AddRevenueToProjection(DRScheduleTran tran)
  {
    if (tran.FinPeriodID == null || tran.TranPeriodID == null)
      return;
    DRRevenueProjectionAccum revenueProjectionAccum1 = this.CreateDRRevenueProjectionAccum(tran.FinPeriodID);
    DRRevenueProjectionAccum revenueProjectionAccum2 = this.CreateDRRevenueProjectionAccum(tran.TranPeriodID);
    Decimal? nullable1 = revenueProjectionAccum1.PTDProjected;
    Decimal? nullable2 = tran.Amount;
    revenueProjectionAccum1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    DRRevenueProjectionAccum revenueProjectionAccum3 = revenueProjectionAccum2;
    nullable2 = revenueProjectionAccum3.TranPTDProjected;
    nullable1 = tran.Amount;
    revenueProjectionAccum3.TranPTDProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private void AddExpenseToProjection(DRScheduleTran tran)
  {
    if (tran.FinPeriodID == null || tran.TranPeriodID == null)
      return;
    DRExpenseProjectionAccum expenseProjectionAccum1 = this.CreateDRExpenseProjectionAccum(tran.FinPeriodID);
    DRExpenseProjectionAccum expenseProjectionAccum2 = this.CreateDRExpenseProjectionAccum(tran.TranPeriodID);
    Decimal? nullable1 = expenseProjectionAccum1.PTDProjected;
    Decimal? nullable2 = tran.Amount;
    expenseProjectionAccum1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    DRExpenseProjectionAccum expenseProjectionAccum3 = expenseProjectionAccum2;
    nullable2 = expenseProjectionAccum3.TranPTDProjected;
    nullable1 = tran.Amount;
    expenseProjectionAccum3.TranPTDProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private void AddRevenueToBalance(DRScheduleTran tran)
  {
    if (tran.FinPeriodID == null || tran.TranPeriodID == null)
      return;
    DRRevenueBalance drRevenueBalance1 = this.CreateDRRevenueBalance(tran.FinPeriodID);
    DRRevenueBalance drRevenueBalance2 = this.CreateDRRevenueBalance(tran.TranPeriodID);
    Decimal? nullable1 = drRevenueBalance1.PTDProjected;
    Decimal? nullable2 = tran.Amount;
    drRevenueBalance1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    nullable2 = drRevenueBalance1.EndProjected;
    nullable1 = tran.Amount;
    drRevenueBalance1.EndProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    DRRevenueBalance drRevenueBalance3 = drRevenueBalance2;
    nullable1 = drRevenueBalance3.TranPTDProjected;
    nullable2 = tran.Amount;
    drRevenueBalance3.TranPTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    DRRevenueBalance drRevenueBalance4 = drRevenueBalance2;
    nullable2 = drRevenueBalance4.TranEndProjected;
    nullable1 = tran.Amount;
    drRevenueBalance4.TranEndProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private void AddExpenseToBalance(DRScheduleTran tran)
  {
    if (tran.FinPeriodID == null || tran.TranPeriodID == null)
      return;
    DRExpenseBalance drExpenseBalance1 = this.CreateDRExpenseBalance(tran.FinPeriodID);
    DRExpenseBalance drExpenseBalance2 = this.CreateDRExpenseBalance(tran.TranPeriodID);
    Decimal? nullable1 = drExpenseBalance1.PTDProjected;
    Decimal? nullable2 = tran.Amount;
    drExpenseBalance1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    nullable2 = drExpenseBalance1.EndProjected;
    nullable1 = tran.Amount;
    drExpenseBalance1.EndProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    DRExpenseBalance drExpenseBalance3 = drExpenseBalance2;
    nullable1 = drExpenseBalance3.TranPTDProjected;
    nullable2 = tran.Amount;
    drExpenseBalance3.TranPTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    DRExpenseBalance drExpenseBalance4 = drExpenseBalance2;
    nullable2 = drExpenseBalance4.TranEndProjected;
    nullable1 = tran.Amount;
    drExpenseBalance4.TranEndProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private DRExpenseBalance CreateDRExpenseBalance(string periodID)
  {
    return ((PXSelectBase<DRExpenseBalance>) this.ExpenseBalance).Insert(new DRExpenseBalance()
    {
      FinPeriodID = periodID,
      BranchID = ((PXSelectBase<DRScheduleDetail>) this.Components).Current.BranchID,
      AcctID = ((PXSelectBase<DRScheduleDetail>) this.Components).Current.DefAcctID,
      SubID = ((PXSelectBase<DRScheduleDetail>) this.Components).Current.DefSubID,
      ComponentID = new int?(((PXSelectBase<DRScheduleDetail>) this.Components).Current.ComponentID.GetValueOrDefault()),
      ProjectID = new int?(((PXSelectBase<DRScheduleDetail>) this.Components).Current.ProjectID.GetValueOrDefault()),
      VendorID = new int?(((PXSelectBase<DRScheduleDetail>) this.Components).Current.BAccountID.GetValueOrDefault())
    });
  }

  private DRRevenueBalance CreateDRRevenueBalance(string periodID)
  {
    return ((PXSelectBase<DRRevenueBalance>) this.RevenueBalance).Insert(new DRRevenueBalance()
    {
      FinPeriodID = periodID,
      BranchID = ((PXSelectBase<DRScheduleDetail>) this.Components).Current.BranchID,
      AcctID = ((PXSelectBase<DRScheduleDetail>) this.Components).Current.DefAcctID,
      SubID = ((PXSelectBase<DRScheduleDetail>) this.Components).Current.DefSubID,
      ComponentID = new int?(((PXSelectBase<DRScheduleDetail>) this.Components).Current.ComponentID.GetValueOrDefault()),
      ProjectID = new int?(((PXSelectBase<DRScheduleDetail>) this.Components).Current.ProjectID.GetValueOrDefault()),
      CustomerID = new int?(((PXSelectBase<DRScheduleDetail>) this.Components).Current.BAccountID.GetValueOrDefault())
    });
  }

  private DRExpenseProjectionAccum CreateDRExpenseProjectionAccum(string periodID)
  {
    DRExpenseProjectionAccum expenseProjectionAccum = new DRExpenseProjectionAccum();
    expenseProjectionAccum.FinPeriodID = periodID;
    expenseProjectionAccum.BranchID = ((PXSelectBase<DRScheduleDetail>) this.Components).Current.BranchID;
    expenseProjectionAccum.AcctID = ((PXSelectBase<DRScheduleDetail>) this.Components).Current.AccountID;
    expenseProjectionAccum.SubID = ((PXSelectBase<DRScheduleDetail>) this.Components).Current.SubID;
    expenseProjectionAccum.ComponentID = new int?(((PXSelectBase<DRScheduleDetail>) this.Components).Current.ComponentID.GetValueOrDefault());
    expenseProjectionAccum.ProjectID = new int?(((PXSelectBase<DRScheduleDetail>) this.Components).Current.ProjectID.GetValueOrDefault());
    expenseProjectionAccum.VendorID = new int?(((PXSelectBase<DRScheduleDetail>) this.Components).Current.BAccountID.GetValueOrDefault());
    return ((PXSelectBase<DRExpenseProjectionAccum>) this.ExpenseProjection).Insert(expenseProjectionAccum);
  }

  private DRRevenueProjectionAccum CreateDRRevenueProjectionAccum(string periodID)
  {
    DRRevenueProjectionAccum revenueProjectionAccum = new DRRevenueProjectionAccum();
    revenueProjectionAccum.FinPeriodID = periodID;
    revenueProjectionAccum.BranchID = ((PXSelectBase<DRScheduleDetail>) this.Components).Current.BranchID;
    revenueProjectionAccum.AcctID = ((PXSelectBase<DRScheduleDetail>) this.Components).Current.AccountID;
    revenueProjectionAccum.SubID = ((PXSelectBase<DRScheduleDetail>) this.Components).Current.SubID;
    revenueProjectionAccum.ComponentID = new int?(((PXSelectBase<DRScheduleDetail>) this.Components).Current.ComponentID.GetValueOrDefault());
    revenueProjectionAccum.ProjectID = new int?(((PXSelectBase<DRScheduleDetail>) this.Components).Current.ProjectID.GetValueOrDefault());
    revenueProjectionAccum.CustomerID = new int?(((PXSelectBase<DRScheduleDetail>) this.Components).Current.BAccountID.GetValueOrDefault());
    return ((PXSelectBase<DRRevenueProjectionAccum>) this.RevenueProjection).Insert(revenueProjectionAccum);
  }

  private Decimal SumOpenAndProjectedTransactions(DRScheduleDetail row)
  {
    Decimal num = 0M;
    PXView view1 = ((PXSelectBase) this.OpenTransactions).View;
    object[] objArray1 = new object[1]{ (object) row };
    object[] objArray2 = Array.Empty<object>();
    foreach (DRScheduleTran drScheduleTran in view1.SelectMultiBound(objArray1, objArray2))
      num += drScheduleTran.Amount.Value;
    PXView view2 = ((PXSelectBase) this.ProjectedTransactions).View;
    object[] objArray3 = new object[1]{ (object) row };
    object[] objArray4 = Array.Empty<object>();
    foreach (DRScheduleTran drScheduleTran in view2.SelectMultiBound(objArray3, objArray4))
      num += drScheduleTran.Amount.Value;
    return num;
  }

  protected virtual TransactionsGenerator GetTransactionsGenerator(DRDeferredCode deferralCode)
  {
    return new TransactionsGenerator((PXGraph) this, deferralCode);
  }

  public enum Context
  {
    Normal,
    Recalculate,
  }

  [PXCacheName("Associated Schedule")]
  [Serializable]
  public class DRScheduleEx : DRSchedule
  {
    [PXDBString(15, IsUnicode = true, IsKey = true)]
    [PXUIField(DisplayName = "Schedule Number")]
    public override string ScheduleNbr { get; set; }

    public new abstract class scheduleNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DraftScheduleMaint.DRScheduleEx.scheduleNbr>
    {
    }
  }
}
